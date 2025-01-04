using System;
using System.Threading;

namespace Intersect.Client.Framework.Content
{
    public sealed class ContentLoaderThread
    {
        private readonly Thread _thread;

        private readonly Action _onCompleted;

        private readonly Func<Action[]> _workRequestFunction;

        public ContentLoaderThread(int threadIndex, Action onCompleted, Func<Action[]> workRequestFunction, bool yield)
        {
            _thread = new Thread(yield ? RunWithYield : (ThreadStart)Run)
            {
                Name = $"Content Loader Thread {threadIndex}",
                Priority = ThreadPriority.AboveNormal
            };

            _onCompleted = onCompleted;
            _workRequestFunction = workRequestFunction;
        }

        public void Start() => _thread.Start();

        public void Join() => _thread.Join();

        private void Run()
        {
            Action[] workItems;
            do
            {
                workItems = _workRequestFunction();
                foreach (var workItem in workItems)
                {
                    workItem();
                }
            } while (workItems.Length > 0);

            _onCompleted();
        }

        private void RunWithYield()
        {
            Action[] workItems;
            do
            {
                workItems = _workRequestFunction();
                foreach (var workItem in workItems)
                {
                    workItem();
                    Thread.Yield();
                }
            } while (workItems.Length > 0);

            _onCompleted();
        }
    }
}