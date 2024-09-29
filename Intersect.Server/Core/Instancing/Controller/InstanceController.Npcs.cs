using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Server.Maps;
using Intersect.Server.Core.Instancing.Controller.Components;
using Intersect.Utilities;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        public HashSet<string> PermadeadNpcs { get; set; } = new HashSet<string>();

        public Dictionary<Guid, SpawnInfo> MapSpawnGroups { get; set; } = new Dictionary<Guid, SpawnInfo>();

        public Debouncer SpawnGroupDebouncer = new Debouncer();

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

        public void ChangeSpawnGroup(Guid controllerId, int spawnGroup, bool persistCleanup, bool surroundingMaps)
        {
            SpawnGroupDebouncer.Debounce(() => {
                if (MapSpawnGroups.ContainsKey(controllerId))
                {
                    MapSpawnGroups[controllerId].Group = spawnGroup;
                    MapSpawnGroups[controllerId].PersistCleanup = persistCleanup;
                }
                else
                {
                    MapSpawnGroups.Add(controllerId, new SpawnInfo(spawnGroup, persistCleanup));
                }

                if (surroundingMaps)
                {
                    var map = MapController.Get(controllerId);
                    foreach (var surroundingMap in map.GetSurroundingMapIds(false))
                    {
                        ChangeSpawnGroup(surroundingMap, spawnGroup, persistCleanup, false);
                    }
                }
            }, 500, pessimistic: true);
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
}
