using Intersect.GameObjects;
using Intersect.GameObjects.Maps;
using Intersect.Server.Core;
using Intersect.Server.Core.Instancing.Controller;
using Intersect.Server.General;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Maps
{

    public class NpcWaveInstance
    {
        public MapInstance Map { get; set; }
        
        public NpcWaveDescriptor Descriptor { get; set; }

        public long StartTime { get; set; }

        public bool HasStarted => StartTime != default;

        public NpcWaveInstance(MapInstance map, NpcWaveDescriptor descriptor)
        {
            Map = map;
            Descriptor = descriptor;
        }

        public void Start()
        {
            StartTime = Timing.Global.Milliseconds;
        }

        public void End()
        {
            Map.ChangeSpawnGroup(Map.NpcSpawnGroup + 1, true, true, false, false);
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
                .Where(spawn => spawn.Key.PreventRespawn)
                .Where(spawn => spawn.Key.SpawnGroup == currentSpawnGroup || (spawn.Key.CumulativeSpawning && spawn.Key.SpawnGroup <= currentSpawnGroup))
                .Where(spawn => Map.NpcHasEnoughPlayersToSpawn(spawn.Key))
                .Select(spawn => spawn.Value.Entity?.PermadeathKey ?? string.Empty);

            return relevantSpawns.All(key => instanceController.PermadeadNpcs.Contains(key));
        }
    }

    public class NpcWaveGroupInstance
    {
        public MapInstance Map { get; set; }

        public NpcWaveGroupDescriptor Descriptor { get; set; }

        public long StartTime { get; set; }

        public NpcWaveInstance CurrentWave { get; set; }

        public Queue<NpcWaveInstance> WaveQueue { get; set; }

        public bool Completed { get; set; }

        public Action OnComplete { get; set; }

        public NpcWaveGroupInstance(MapInstance map, NpcWaveGroupDescriptor descriptor, Action onComplete   )
        {
            if (map == null || descriptor == null)
            {
                return;
            }

            Map = map;
            Descriptor = descriptor;
            StartTime = Timing.Global.Milliseconds;
            Completed = false;

            WaveQueue = new Queue<NpcWaveInstance>();
            foreach (var waveDesc in descriptor.Waves)
            {
                WaveQueue.Enqueue(new NpcWaveInstance(map, waveDesc));
            }

            OnComplete = onComplete;
        }

        public void Process()
        {
            if (Map == null || Descriptor == null || WaveQueue.Count == 0)
            {
                return;
            }

            var currentWave = WaveQueue.Peek();
            if (currentWave == null)
            {
                return;
            }

            if (!currentWave.HasStarted)
            {
                currentWave.Start();
            }

            var now = Timing.Global.Milliseconds;
            
            if (currentWave.Descriptor.AdvanceOnTimeout > 0 &&
                currentWave.Descriptor.AdvanceOnTimeout + currentWave.StartTime >= now)
            {
                NextWave(currentWave);
                return;
            }

            if (currentWave.Descriptor.AdvanceOnPermadeadCompletion && currentWave.PermadeadNpcsDead())
            {
                NextWave(currentWave);
                return;
            }
        }

        public void NextWave(NpcWaveInstance currentWave)
        {
            currentWave.End();
            WaveQueue.Dequeue();
            
            if (WaveQueue.Count <= 0)
            {
                Complete();
            }
        }

        public void Complete()
        {
            WaveQueue.Clear();
            Completed = true;
            if (OnComplete != null)
            {
                OnComplete.Invoke();
            }
        }
    }

    /// <summary>
    /// Goes on the <see cref="MapInstance"/>
    /// </summary>
    public class NpcWaveController
    {
        public List<NpcWaveGroupInstance> NpcWaveGroups { get; set; } = new List<NpcWaveGroupInstance>();

        public MapInstance Map { get; set; }

        public MapController MapController => Map.GetController();

        public Guid InstanceId { get; set; }

        public NpcWaveGroupInstance CurrentWaveGroup { get; set; }

        public void ProcessWaveGroup()
        {
            if (CurrentWaveGroup == null)
            {
                return;
            }

            CurrentWaveGroup.Process();
        }

        public void FindAutostartWaveGroup()
        {
            if (CurrentWaveGroup == null)
            {
                var autostartWaves = NpcWaveGroups.Where(waveGroup => waveGroup.Descriptor.AutoStartWave == Map.NpcSpawnGroup && !waveGroup.Completed);
                if (autostartWaves.Any())
                {
                    CurrentWaveGroup = autostartWaves.First();
                    return;
                }
            }
        }

        public void SetCurrentWaveGroup(Guid descriptorId)
        {
            if (!MapBase.TryGet(MapController.Id, out var mapDescriptor) || !mapDescriptor.NpcWaveGroups.TryGetValue(descriptorId, out var waveGroup))
            {
                return;
            }

            // WIP - CurrentWaveGroup = new NpcWaveGroupInstance(Map, waveGroup);
        }
    }
}
