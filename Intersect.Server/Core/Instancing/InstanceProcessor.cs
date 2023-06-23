﻿using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Server.Maps;
using Intersect.Server.Core.Instancing.Controller;
using Intersect.GameObjects.Events;

namespace Intersect.Server.Core
{
    public static class InstanceProcessor
    {
        private static Dictionary<Guid, InstanceController> InstanceControllers = new Dictionary<Guid, InstanceController>();

        public static Guid[] CurrentControllers => InstanceControllers.Keys.ToArray();

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
                Logging.Log.Debug($"Removing instance controller {id}");
            }
        }

        public static void AddInstanceController(Guid mapInstanceId)
        {
            if (!InstanceControllers.ContainsKey(mapInstanceId))
            {
                InstanceControllers[mapInstanceId] = new InstanceController(mapInstanceId);
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

            Dictionary<Guid, List<MapInstance>> mapsAndInstances = activeMaps
                .GroupBy(m => m.MapInstanceId)
                .ToDictionary(m => m.Key, m => m.ToList());

            // For each instance...
            foreach (var mapInstanceGroup in mapsAndInstances)
            {
                // Fetch our instance controller
                var instanceId = mapInstanceGroup.Key;
                if (!InstanceControllers.ContainsKey(instanceId))
                {
                    return;
                }
                var instanceController = InstanceControllers[instanceId];

                // Cleanup map spawn groups/permadead NPCs; accumulate total player count
                instanceController.PlayerCount = 0;
                // Do things that need to occur for _each map_ here
                foreach (var map in mapInstanceGroup.Value)
                {
                    instanceController.PlayerCount += map.GetPlayers().Count;
                }

                instanceController.CleanupSpawnGroups();

                // Process the melee pit random matchmake logic
                instanceController.ProcessOpenMelee();

                // If instance was a dungeon but no one is around to hear it, remove the dungeon
                // TODO Alex : Gonna try commenting this out and see if it fixes dungeon bug
                /*
                if (instanceController.InstanceIsDungeon && instanceController.PlayerCount == 0)
                {
                    instanceController.RemoveDungeon();
                }
                */
            }
        }

        public static void AddNewInstanceVariable(Guid instanceVarId)
        {
            foreach(var instanceController in InstanceControllers.Values)
            {
                instanceController.AddInstanceVariable(instanceVarId);
            }
        }
    }
}
