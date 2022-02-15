using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public class NPCGuildOptions
    {
        /// <summary>
        /// A players max class rank in some class
        /// </summary>
        public int MaxClassRank = 4;

        /// <summary>
        /// There should always be MaxClassRank - 1 of these At max rank, you will no longer need to check for SAs
        /// </summary>
        public List<int> RequiredTasksPerClassRank = new List<int>()
        {
            2, // CR 0
            4, // CR 1
            4, // CR 2
        };

        /// <summary>
        /// Whether or not we want to have a special assignment count toward task cooldowns
        /// </summary>
        public bool SpecialAssignmentCountsTowardCooldown = false;

        /// <summary>
        /// Whether or not we want to have hooks into paying out a special assignment on completion
        /// </summary>
        public bool PayoutSpecialAssignments = false;

        /// <summary>
        /// How long until a new task can be picked up
        /// </summary>
        public long TaskCooldownMs = 600000L;

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            RequiredTasksPerClassRank.Clear();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            int lastClassRankWithSA = MaxClassRank - 1;
            if (RequiredTasksPerClassRank.Count > lastClassRankWithSA)
            {
                RequiredTasksPerClassRank = new List<int>(RequiredTasksPerClassRank.GetRange(0, lastClassRankWithSA));
            } 
            else if (RequiredTasksPerClassRank.Count < lastClassRankWithSA)
            {
                int lastVal = RequiredTasksPerClassRank.Last();
                // Populate missing requirements with whatever the last requirement's value was
                while (RequiredTasksPerClassRank.Count < lastClassRankWithSA)
                {
                    RequiredTasksPerClassRank.Add(lastVal);
                }
            }
        }
    }
}
