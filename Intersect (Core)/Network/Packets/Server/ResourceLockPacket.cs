using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ResourceLockPacket : IntersectPacket
    {
        public ResourceLockPacket()
        {
        }

        public ResourceLockPacket(bool resourceLock)
        {
            ResourceLock = resourceLock;
        }

        [Key(0)]
        public bool ResourceLock { get; set; }
    }
}
