using Intersect.GameObjects.Maps;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Intersect.Server.Maps
{
    /// <summary>
    /// Goes on the <see cref="MapInstance"/>
    /// </summary>
    public class NpcWaveController
    {
        public ConcurrentBag<NpcWaveGroupInstance> NpcWaveGroups { get; set; }
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

            NpcWaveGroups = new ConcurrentBag<NpcWaveGroupInstance>();
            foreach (var groupDesc in mapBase.NpcWaveGroups)
            {
                NpcWaveGroups.Add(new NpcWaveGroupInstance(Map, groupDesc));
            }
        }

        public void ProcessWaves()
        {
            foreach (var waveGroup in NpcWaveGroups.Where(group => !group.Completed).ToArray())
            {
                waveGroup.Process();
            }
        }
    }
}
