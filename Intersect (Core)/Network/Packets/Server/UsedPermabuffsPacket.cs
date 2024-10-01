using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class UsedPermabuffsPacket : IntersectPacket
    {
        [Key(0)]
        public Guid[] UsedPermabuffs { get; set; }

        //EF
        public UsedPermabuffsPacket()
        {
        }

        public UsedPermabuffsPacket(Guid[] usedPermabuffs)
        {
            UsedPermabuffs = usedPermabuffs;
        }
    }
}
