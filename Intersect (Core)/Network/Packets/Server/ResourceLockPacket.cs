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

        public ResourceLockPacket(bool resourceLock, double harvestBonus, int harvestsRemaining, Guid resourceId)
        {
            ResourceLock = resourceLock;
            HarvestBonus = harvestBonus;
            HarvestsRemaining = harvestsRemaining;
            ResourceId = resourceId;
        }

        [Key(0)]
        public bool ResourceLock { get; set; }

        [Key(1)]
        public double HarvestBonus { get; set; }

        [Key(2)]
        public int HarvestsRemaining { get; set; }

        [Key(3)]
        public Guid ResourceId { get; set; }
    }
}
