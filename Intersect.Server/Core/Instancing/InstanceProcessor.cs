using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Server.Maps;
using Intersect.Server.Core.Instancing.Controller;

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
                Logging.Log.Debug($"Removing instance controller {id}");
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
                instanceController.AddInstanceVariable(instanceVarId);
            }
        }
    }
}
