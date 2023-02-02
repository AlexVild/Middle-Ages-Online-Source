using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.GameObjects;
using Intersect.Server.Maps;

namespace Intersect.Server.Core
{
    public static class InstanceProcessor
    {
        private static Dictionary<Guid, InstanceController> InstanceControllers = new Dictionary<Guid, InstanceController>();

        public static bool TryGetInstanceController(Guid instanceId, out InstanceController controller)
        {
            return InstanceControllers.TryGetValue(instanceId, out controller);
        }

        private static void CleanupOrphanedControllers(List<MapInstance> activeMaps)
        {
            var processingInstances = InstanceControllers.Keys;
            var processingMaps = activeMaps.ToArray().Select(map => map.MapInstanceId);

            foreach (var id in processingInstances.ToArray().Except(processingMaps))
            {
                InstanceControllers.Remove(id);
            }
        }

        public static void UpdateInstanceControllers(List<MapInstance> activeMaps)
        {
            if (activeMaps == null || activeMaps.Count == 0)
            {
                InstanceControllers.Clear();
                return;
            }

            // Cleanup inactive instances
            CleanupOrphanedControllers(activeMaps);

            foreach (var map in activeMaps)
            {
                var instanceId = map.MapInstanceId;
                if (!InstanceControllers.TryGetValue(instanceId, out var instance))
                {
                    InstanceControllers[instanceId] = new InstanceController(instanceId);
                    instance = InstanceControllers[instanceId];
                }

                instance.PlayerCount = map.GetPlayers().Count;

                instance.CleanupSpawnGroups();
            }
        }

        public static void AddNewInstanceVariable(Guid instanceVarId)
        {
            foreach(var instanceController in InstanceControllers.Values)
            {
                if (instanceController.InstanceVariables.ContainsKey(instanceVarId))
                {
                    continue;
                }

                VariableValue copyValue = new VariableValue();
                var instVar = (InstanceVariableBase)InstanceVariableBase.Lookup[instanceVarId];
                copyValue.SetValue(instVar.DefaultValue.Value);
                copyValue.Type = instVar.Type;
                instanceController.InstanceVariables[instanceVarId] = copyValue;
            }
        }
    }

    public class InstanceController
    {
        public InstanceController(Guid instanceId)
        {
            InstanceId = instanceId;

            InitializeInstanceVariables();
        }

        public Guid InstanceId { get; set; }

        public Dictionary<Guid, VariableValue> InstanceVariables { get; set; }

        public HashSet<string> PermadeadNpcs { get; set; } = new HashSet<string>();

        public int PlayerCount { get; set; }

        public Dictionary<Guid, SpawnInfo> MapSpawnGroups { get; set; } = new Dictionary<Guid, SpawnInfo>();

        public void CleanupSpawnGroups()
        {
            // Reset spawn groups that aren't meant to persist
            var needCleaned = MapSpawnGroups
                    .ToArray()
                    .Where(mapSpawnInfos => !mapSpawnInfos.Value.PersistCleanup && !MapController.TryGetInstanceFromMap(mapSpawnInfos.Key, InstanceId, out _));
            foreach (var mapInstance in needCleaned)
            {
                MapSpawnGroups.Remove(mapInstance.Key);
            }
        }

        public void InitializeInstanceVariables()
        {
            InstanceVariables = new Dictionary<Guid, VariableValue>();

            // If we do, go out and fetch the default values of all our instance variables
            foreach (InstanceVariableBase instVar in InstanceVariableBase.Lookup.Values)
            {
                VariableValue copyValue = new VariableValue();
                copyValue.SetValue(instVar.DefaultValue.Value);
                copyValue.Type = instVar.Type;
                InstanceVariables[instVar.Id] = copyValue;
            }
        }

        public void AddInstanceVariable(Guid instanceVarId)
        {
            // Create the instance var if it does not exist
            if (!InstanceVariables.ContainsKey(instanceVarId))
            {
                VariableValue copyValue = new VariableValue();
                var instVar = (InstanceVariableBase)InstanceVariableBase.Lookup[instanceVarId];
                copyValue.SetValue(instVar.DefaultValue.Value);
                copyValue.Type = instVar.Type;
                InstanceVariables[instanceVarId] = copyValue;
            }
        }

        public void ChangeSpawnGroup(Guid controllerId, int spawnGroup, bool persistCleanup)
        {
            if (MapSpawnGroups.ContainsKey(controllerId))
            {
                MapSpawnGroups[controllerId].Group = spawnGroup;
                MapSpawnGroups[controllerId].PersistCleanup = persistCleanup;
            }
            else
            {
                MapSpawnGroups.Add(controllerId, new SpawnInfo(spawnGroup, persistCleanup));
            }
        }

        public void ClearPermadeadNpcs(Guid mapControllerId, bool forceRespawn)
        {
            // TODO why did I think this was okay. Could just use a dictionary of controller IDs like for spawn groups
            PermadeadNpcs.RemoveWhere(key =>
            {
                var split = key.Split('_');
                if (split.Length <= 1)
                {
                    return false;
                }
                return split[0] == mapControllerId.ToString();
            });

            if (forceRespawn && MapController.TryGetInstanceFromMap(mapControllerId, InstanceId, out var mapInstance))
            {
                mapInstance.RefreshNpcs();
            }
        }
    }

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
}
