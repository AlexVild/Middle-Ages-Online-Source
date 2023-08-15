using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Intersect.Config
{
    public class InstancingOptions
    {
        public bool SharedInstanceRespawnInInstance = true;

        public bool RejoinableSharedInstances = false;

        public int MaxSharedInstanceLives = 3;

        public bool BootAllFromInstanceWhenOutOfLives = true;

        public bool LoseExpOnInstanceDeath = false;
        
        public bool RegenManaOnInstanceDeath = false;

        public long NpcSpawnGroupChangeMinimum = 1000;

        /// <summary>
        /// The longer this is, the more the map will have time to get back to a "true" state, but it affects efficiency
        /// </summary>
        public long EmptyMapProcessingTime = 20000;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Validate();
        }

        public void Validate()
        {
        }
    }
}
