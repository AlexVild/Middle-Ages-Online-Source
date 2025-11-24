using Intersect.GameObjects.Maps;
using Intersect.Utilities;
using System;
using System.Collections.Generic;

namespace Intersect.Server.Maps
{
    public class NpcWaveGroupInstance
    {
        public MapInstance Map { get; set; }
        public NpcWaveGroupDescriptor Descriptor { get; set; }
        public long StartTime { get; set; }
        public NpcWaveInstance CurrentWave { get; set; }
        public Queue<NpcWaveInstance> WaveQueue { get; set; }
        public bool Completed { get; set; }
        public Action OnComplete { get; set; }
        public long StartNextWaveTimestamp { get; set; }

        public NpcWaveGroupInstance(MapInstance map, NpcWaveGroupDescriptor descriptor, Action onComplete = null)
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
            if (Map == null || Descriptor == null || WaveQueue.Count == 0 || Timing.Global.Milliseconds < StartNextWaveTimestamp)
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

            if (currentWave.Descriptor.AdvanceOnTimeout > 0 && currentWave.WaveTimeoutExceeded())
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

        public void NextWave(NpcWaveInstance prevWave = null)
        {
            if (prevWave != null)
            {
                prevWave.End();
                StartNextWaveTimestamp = Timing.Global.Milliseconds + prevWave.Descriptor.TimeBetweenMs;
            }

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
}
