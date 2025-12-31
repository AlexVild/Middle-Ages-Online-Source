using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.Server.Core;
using Intersect.Utilities;
using System.Linq;

namespace Intersect.Server.Maps
{
    public class NpcWave
    {
        public MapInstance Map { get; set; }
        public NpcWaveDescriptor Descriptor { get; set; }
        public long StartTime { get; set; }
        public bool HasStarted => StartTime != default;
        public int IterationsCompleted { get; set; }
        public bool FinishedLoop => IterationsCompleted > (Descriptor?.LoopIterations ?? 0);

        public NpcWave(MapInstance map, NpcWaveDescriptor descriptor)
        {
            Map = map;
            Descriptor = descriptor;
            IterationsCompleted = 0;
            StartTime = 0;
        }

        public void Start()
        {
            StartTime = Timing.Global.Milliseconds;

            if (EventBase.TryGet(Descriptor.OnStartEventId, out var startEvent))
            {
                QueueEvent(startEvent);
            }
        }

        public void End()
        {
            IterationsCompleted++;
            Map.ChangeSpawnGroup(Map.NpcSpawnGroup + 1, true, true, false, false);

            if (FinishedLoop && EventBase.TryGet(Descriptor.OnEndEventId, out var endEvent))
            {
                QueueEvent(endEvent);
            }
        }

        public bool WaveTimeoutExceeded()
        {
            return Timing.Global.Milliseconds >= StartTime + Descriptor.AdvanceOnTimeoutMs;
        }

        public bool PermadeadNpcsDead()
        {
            if (Map == null ||
                !InstanceProcessor.TryGetInstanceController(Map.MapInstanceId, out var instanceController))
            {
                return false;
            }

            var currentSpawnGroup = Map.NpcSpawnGroup;
            var relevantSpawns = Map.NpcSpawnInstances
                .Where(spawnKv =>
                {
                    var spawnDetails = spawnKv.Key;
                    var spawnInstance = spawnKv.Value;

                    var validSpawnGroup = spawnDetails.SpawnGroup == currentSpawnGroup ||
                        (spawnDetails.CumulativeSpawning && spawnDetails.SpawnGroup <= currentSpawnGroup);

                    return spawnDetails.PreventRespawn && validSpawnGroup && Map.NpcHasEnoughPlayersToSpawn(spawnDetails);
                })
                .Select(spawn => spawn.Value.Entity?.PermadeathKey ?? string.Empty);

            return relevantSpawns.All(key => instanceController.PermadeadNpcs.Contains(key));
        }

        private void QueueEvent(EventBase evt)
        {
            foreach (var player in Map.GetPlayers(true))
            {
                player.EnqueueStartCommonEvent(evt);
            }
        }
    }
}
