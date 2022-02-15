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
