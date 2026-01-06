using Intersect.GameObjects.Maps;
using Intersect.Server.Core;
using Intersect.Server.Core.Instancing.Controller;
using Intersect.Server.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Maps
{
    /// <summary>
    /// Goes on the <see cref="MapInstance"/>
    /// </summary>
    public class NpcWaveController
    {
        public List<NpcWaveGroup> NpcWaveGroups { get; set; }
        public MapInstance Map { get; set; }
        public MapController MapController => Map.GetController();

        public NpcWaveController(MapInstance map)
        {
            Map = map;
        }

        public void Initialize()
        {
            if (!MapBase.TryGet(MapController.Id, out var mapBase))
            {
                return;
            }

            NpcWaveGroups = new List<NpcWaveGroup>();
            foreach (var groupDesc in mapBase.NpcWaveGroups)
            {
                NpcWaveGroups.Add(new NpcWaveGroup(Map, groupDesc));
            }
        }

        public void ProcessWaves()
        {
            foreach (var waveGroup in NpcWaveGroups.Where(group => !group.Started).ToArray())
            {
                if (Map.NpcSpawnGroup == waveGroup.Descriptor?.AutoStartWave)
                {
                    waveGroup.Start();
                    break;
                }
            }

            var currentGroup = NpcWaveGroups.Find(group => !group.Completed && group.Started);
            if (currentGroup != null)
            {
                currentGroup.Process();
                if (currentGroup.Descriptor.DisposeMapOnEmpty && Map.GetPlayers(true).Count == 0 && InstanceProcessor.TryGetInstanceController(Map.MapInstanceId, out var instanceController))
                {
                    instanceController.ChangeSpawnGroup(MapController.Id, currentGroup.Descriptor?.AutoStartWave ?? 0, false, false);
                    instanceController.ClearPermadeadNpcs(MapController.Id, true);
                    currentGroup.Started = false;
                }
            }
        }
    }
}
