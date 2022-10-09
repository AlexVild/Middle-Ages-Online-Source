using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.GameObjects;
using Intersect.Server.Maps;

namespace Intersect.Server.Core
{
    public class SpawnInfo
    {
        public SpawnInfo()
        {
            Group = 0;
            PersistCleanup = false;
        }

        public SpawnInfo(int group, bool persistCleanup)
        {
            Group = group;
            PersistCleanup = persistCleanup;
        }

        public int Group { get; set; }

        public bool PersistCleanup { get; set; }
    }

    public static class ProcessingInfo
    {
        /// <summary>
        /// A list of all active unique instance IDs
        /// </summary>
        private static List<Guid> ActiveMapInstanceIds = new List<Guid>();

        /// <summary>
        /// A dictionary from InstanceId => [InstanceVariableId => InstanceVariableValue]
        /// </summary>
        public static Dictionary<Guid, Dictionary<Guid, VariableValue>> InstanceVariables = new Dictionary<Guid, Dictionary<Guid, VariableValue>>();

        /// <summary>
        /// Keeps an active count of how many players are currently on any given instance
        /// </summary>
        public static Dictionary<Guid, int> PlayersInInstance = new Dictionary<Guid, int>();

        public static Dictionary<Guid, HashSet<string>> PermadeadNpcs = new Dictionary<Guid, HashSet<string>>();
        
        public static Dictionary<Guid, Dictionary<Guid, SpawnInfo>> MapSpawnGroups = new Dictionary<Guid, Dictionary<Guid, SpawnInfo>>();

        /// <summary>
        /// Maintains a list of unique instance Ids that players may be on
        /// </summary>
        public static void UpdateInstances(List<MapInstance> activeMaps)
        {
            // Update our list of unique active instance IDs
            ActiveMapInstanceIds.Clear();
            PlayersInInstance.Clear();
            ActiveMapInstanceIds = activeMaps
                .Select(mapInstance => {
                    if (!PlayersInInstance.ContainsKey(mapInstance.MapInstanceId)) // Alex - LOL, might as well do this here
                    {
                        PlayersInInstance.Add(mapInstance.MapInstanceId, mapInstance.GetPlayers().Count);
                    } else
                    {
                        PlayersInInstance[mapInstance.MapInstanceId] += mapInstance.GetPlayers().Count;
                    }
                    return mapInstance.MapInstanceId;
                })
                .Distinct()
                .ToList();

            foreach(var key in PermadeadNpcs.Keys.ToList())
            {
                if (!ActiveMapInstanceIds.Contains(key))
                {
                    Logging.Log.Info($"Cleaning up permadead NPCs for instance {key}");
                    PermadeadNpcs.Remove(key);
                }
            }

            foreach (var instanceId in MapSpawnGroups.Keys.ToList())
            {
                // Clean up spawn groups that are in orphaned instances
                if (!ActiveMapInstanceIds.Contains(instanceId))
                {
                    Logging.Log.Info($"Cleaning up permanent spawn groups for instance {instanceId}");
                    MapSpawnGroups.Remove(instanceId);
                    continue;
                }

                // Clean up spawn groups that are NOT in orphaned instances, but aren't meant to persist
                var needCleaned = MapSpawnGroups[instanceId]
                    .ToArray()
                    .Where(mapSpawnInfos => !mapSpawnInfos.Value.PersistCleanup && !MapController.TryGetInstanceFromMap(mapSpawnInfos.Key, instanceId, out _));
                foreach (var mapInstance in needCleaned)
                {
                    MapSpawnGroups[instanceId].Remove(mapInstance.Key);
                }
                // Clean up the groups if there's nothing left to keep track of
                if (MapSpawnGroups[instanceId].Count == 0)
                {
                    MapSpawnGroups.Remove(instanceId);
                }
            }

            // Clean up instance variables where the instance has been marked dead
            InstanceVariables.Keys.ToList()
                .Where(instanceId => !ActiveMapInstanceIds.Contains(instanceId)).ToList()
                .ForEach(inactiveInstanceId => InstanceVariables.Remove(inactiveInstanceId));

            // For each current active instance Id, check to make sure it has its instance variables initialized
            ActiveMapInstanceIds.ForEach(instanceId =>
            {
                if (!InstanceVariables.ContainsKey(instanceId))
                {
                    // If we do, go out and fetch the default values of all our instance variables
                    var instVarValues = new Dictionary<Guid, VariableValue>();
                    foreach (InstanceVariableBase instVar in InstanceVariableBase.Lookup.Values)
                    {
                        VariableValue copyValue = new VariableValue();
                        copyValue.SetValue(instVar.DefaultValue.Value);
                        copyValue.Type = instVar.Type;
                        instVarValues.Add(instVar.Id, copyValue);
                    }
                    // And link them to this instance
                    InstanceVariables.Add(instanceId, instVarValues);
                }
            });
        }

        public static void AddInstanceVariable(Guid instanceVarId)
        {
            foreach(var instance in ActiveMapInstanceIds)
            {
                if (InstanceVariables.ContainsKey(instance) && !InstanceVariables[instance].ContainsKey(instanceVarId))
                {
                    VariableValue copyValue = new VariableValue();
                    var instVar = (InstanceVariableBase)InstanceVariableBase.Lookup[instanceVarId];
                    copyValue.SetValue(instVar.DefaultValue.Value);
                    copyValue.Type = instVar.Type;
                    InstanceVariables[instance][instanceVarId] = copyValue;
                }
            }
        }

        public static void ChangeSpawnGroup(Guid instanceId, Guid controllerId, int spawnGroup, bool persistCleanup)
        {
            // If we already have spawn groups for this instance
            if (MapSpawnGroups.TryGetValue(instanceId, out var instanceGroups))
            {
                // Find the one for the map we want
                if (instanceGroups.ContainsKey(controllerId))
                {
                    // And update it
                    instanceGroups[controllerId].Group = spawnGroup;
                    instanceGroups[controllerId].PersistCleanup = persistCleanup;
                }
                else
                {
                    // And create the spawn group for the fresh map 
                    instanceGroups.Add(controllerId, new SpawnInfo(spawnGroup, persistCleanup));
                }
            }
            else
            {
                var newInstanceGroups = new Dictionary<Guid, SpawnInfo>();
                newInstanceGroups.Add(controllerId, new SpawnInfo(spawnGroup, persistCleanup));
                MapSpawnGroups.Add(instanceId, newInstanceGroups);
            }
        }
    }
}
