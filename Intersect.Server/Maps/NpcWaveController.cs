using Intersect.GameObjects.Maps;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Maps
{
    /// <summary>
    /// Goes on the <see cref="MapInstance"/>
    /// </summary>
    public class NpcWaveController : IDisposable
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
            var activePlayers = Map.GetPlayers(true).Where(p => p.Online && !p.IsDead());

            foreach (var waveGroup in NpcWaveGroups.Where(group => !group.Started).ToArray())
            {
                if (Map.NpcSpawnGroup == waveGroup.Descriptor?.AutoStartWave)
                {
                    if (waveGroup.Descriptor.DisposeMapOnEmpty && !activePlayers.Any())
                    {
                        continue;
                    }
                    
                    waveGroup.Start();
                    break;
                }
            }

            var currentGroup = NpcWaveGroups.Find(group => !group.Completed && group.Started);
            if (currentGroup != null)
            {
                currentGroup.Process();
                if (currentGroup.Descriptor.DisposeMapOnEmpty && !activePlayers.Any())
                {
                    currentGroup.ResetToStart();
                }
            }
        }

        public void Dispose()
        {
            NpcWaveGroups.Clear();
        }
    }
}
