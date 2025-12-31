using Intersect.GameObjects.Maps;
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
                }
            }

            foreach (var waveGroup in NpcWaveGroups.Where(group => !group.Completed && group.Started).ToArray())
            {
                waveGroup.Process();
            }
        }
    }
}
