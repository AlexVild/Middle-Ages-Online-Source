using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Intersect.Logging;

namespace Intersect.Client.Framework.Content
{
    public sealed class ThreadedContentLoader
    {
        private readonly object _lock = new object();

        private readonly int _threadCount;
        private readonly ContentLoaderThread[] _threads;

        private readonly ConcurrentQueue<Action> _availableWork = new ConcurrentQueue<Action>();

        private int _tasksExecuted;

        private int _completedThreadCount;

        private DateTime _startedAt;

        private DateTime _finishedAt;

        public TimeSpan Elapsed { get; private set; }

        public int TaskCount => _availableWork.Count;

        public ThreadedContentLoader()
        {
            _threadCount = Math.Max(1, Environment.ProcessorCount - 1);
            _threads = new ContentLoaderThread[_threadCount];

            var shouldYield = Environment.ProcessorCount < 2;
            for (var threadIndex = 0; threadIndex < _threadCount; ++threadIndex)
            {
                _threads[threadIndex] = new ContentLoaderThread(
                    threadIndex,
                    OnThreadCompleted,
                    RequestWork,
                    shouldYield
                );
            }
        }

        public event Action Completed;

        public void EnqueueWork(params Action[] workItems) => EnqueueWork(workItems.AsEnumerable());

        public void EnqueueWork(IEnumerable<Action> workItems)
        {
            lock (_lock)
            {
                if (_completedThreadCount >= _threadCount)
                {
                    throw new InvalidOperationException("Cannot add work after all threads have completed");
                }

                if (workItems == default)
                {
                    throw new ArgumentNullException(nameof(workItems));
                }

                foreach (var workItem in workItems)
                {
                    _availableWork.Enqueue(workItem);
                }
            }
        }

        private void OnThreadCompleted()
        {
            lock (_lock)
            {
                ++_completedThreadCount;
            }

            if (_completedThreadCount < _threadCount)
            {
                return;
            }

            _finishedAt = DateTime.UtcNow;
            Elapsed = _finishedAt - _startedAt;

            Log.Info(
                $"[Content] Finished running {_tasksExecuted} content loading tasks in {Elapsed.TotalMilliseconds}ms"
            );

            Completed?.Invoke();
        }

        private Action[] RequestWork()
        {
            lock (_lock)
            {
                var workCount = _threadCount < 2 ? TaskCount : 1;
                var workItems = new List<Action>(workCount);
                while (workItems.Count < workCount && _availableWork.TryDequeue(out var workItem))
                {
                    workItems.Add(workItem);
                }

                var requestedWork = workItems.ToArray();
                _tasksExecuted += requestedWork.Length;

                return requestedWork;
            }
        }

        public void Start(bool join = false)
        {
            Log.Info($"[Content] Running {TaskCount} tasks on {_threadCount} threads...");

            _startedAt = DateTime.UtcNow;

            foreach (var thread in _threads)
            {
                thread.Start();
            }

            if (!join)
            {
                return;
            }

            foreach (var thread in _threads)
            {
                thread.Join();
            }
        }
    }
}
