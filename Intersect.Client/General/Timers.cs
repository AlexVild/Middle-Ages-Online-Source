using Intersect.Client.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.General
{
    public static class Timers
    {
        public static List<Timer> ActiveTimers = new List<Timer>();

        public static List<Timer> DisplayableTimers => ActiveTimers.Where(t => !t.Descriptor.OnlyDisplayOnExclusiveMaps || t.Descriptor.ContainsExclusiveMap(Globals.Me.MapInstance.Id)).ToList();
    }
}
