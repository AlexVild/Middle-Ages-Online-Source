using Intersect.GameObjects.Maps;
using Intersect.Utilities;
using System;
using System.Collections.Generic;

namespace Intersect.Server.Maps
{
    public class NpcWaveGroup
    {
        public MapInstance Map { get; set; }
        public NpcWaveGroupDescriptor Descriptor { get; set; }
        private long StartTime { get; set; }
        private NpcWave CurrentWave { get; set; }
        public Queue<NpcWave> WaveQueue { get; set; }
        public bool Started { get; set; }
        public bool Completed { get; set; }
        public Action OnComplete { get; set; }
        public long StartNextWaveTimestamp { get; set; }

        public NpcWaveGroup(MapInstance map, NpcWaveGroupDescriptor descriptor, Action onComplete = null)
        {
            if (map == null || descriptor == null)
            {
                return;
            }

            Map = map;
            Descriptor = descriptor;
            StartTime = Timing.Global.Milliseconds;
            Completed = false;
            Started = false;

            WaveQueue = new Queue<NpcWave>();
            foreach (var waveDesc in descriptor.Waves)
            {
                WaveQueue.Enqueue(new NpcWave(map, waveDesc, this));
            }

            OnComplete = onComplete;
        }

        public void Start()
        {
            Started = true;
        }

        public void Process()
        {
            if (Map == null || 
                Descriptor == null || 
                WaveQueue.Count == 0 ||
                !Started ||
                Completed
            )
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

            var goToNextWave = (currentWave.Descriptor.AdvanceOnTimeoutMs > 0 && currentWave.WaveTimeoutExceeded()) ||
                (currentWave.Descriptor.AdvanceOnPermadeadCompletion && currentWave.PermadeadNpcsDead());

            if (goToNextWave)
            {
                if (currentWave.Descriptor.TimeBetweenMs > 0 && StartNextWaveTimestamp == default)
                {
                    StartNextWaveTimestamp = Timing.Global.Milliseconds + currentWave.Descriptor.TimeBetweenMs;
                }
                
                TryNextWave(currentWave);
                return;
            }
        }

        public void TryNextWave(NpcWave prevWave = null)
        {
            // If we're in a delay period, don't proceed to the next wave yet.
            if (Timing.Global.Milliseconds < StartNextWaveTimestamp)
            {
                return;
            }

            StartNextWaveTimestamp = default;

            Logging.Log.Debug($"--- PROCEEDING TO NEXT WAVE...");
            if (prevWave != null)
            {
                prevWave.End();

                // If this is a looping wave, do not dequeue the wave yet
                if (!prevWave.FinishedLoop)
                {
                    // Set the wave as un-started so it restarts itself
                    prevWave.StartTime = default;
                    return;
                }
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
            Logging.Log.Debug($"--- WAVE GROUP COMPLETED...");
        }

        public int WavesRemaining()
        {
            return WaveQueue?.Count ?? 0;
        }
    }
}
