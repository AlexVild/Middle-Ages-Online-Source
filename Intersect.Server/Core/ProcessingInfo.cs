using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.GameObjects;
using Intersect.Server.Maps;

namespace Intersect.Server.Core
{
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

        /// <summary>
        /// Maintains a list of unique instance Ids that players may be on
        /// </summary>
        public static void UpdateActiveInstanceList(List<MapInstance> activeMaps)
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
    }
}
