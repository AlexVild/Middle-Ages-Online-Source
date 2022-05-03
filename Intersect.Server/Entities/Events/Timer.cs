using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities.Events
{
    /// <summary>
    /// Used to determine if a timer is:
    /// - <see cref="TimerState.Active"/>: Currently counting down/up
    /// - <see cref="TimerState.Paused"/>: Not progressing further
    /// - <see cref="TimerState.Quit"/>: Was quit by some event, and will execute its "quit" event
    /// - <see cref="TimerState.Finished"/>: Was quit by vi
    /// </summary>
    public enum TimerState
    {
        Active,
        Paused,
        Quit,
        Finished
    }

    class Timer
    {
        /// <summary>
        /// The id of the Timer this relates to
        /// </summary>
        public Guid Id;

        /// <summary>
        /// The UTC time this timer started at
        /// </summary>
        public long StartTimestamp;

        /// <summary>
        /// This timer's current <see cref="TimerState"/>
        /// </summary>
        public TimerState State;

        public Timer(Guid id, long startTimestamp)
        {
            Id = id;
            StartTimestamp = startTimestamp;
            State = TimerState.Active;
        }
    }
}
