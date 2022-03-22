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

        public ResourceLockPacket(bool resourceLock, double harvestBonus)
        {
            ResourceLock = resourceLock;
            HarvestBonus = harvestBonus;
        }

        [Key(0)]
        public bool ResourceLock { get; set; }

        [Key(1)]
        public double HarvestBonus { get; set; }
    }
}
