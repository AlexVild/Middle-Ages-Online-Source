using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CosmeticUnlocksPacket : IntersectPacket
    {
        public CosmeticUnlocksPacket()
        {
        }

        [Key(0)]
        public List<Guid> UnlockedCosmetics { get; set; }

        public CosmeticUnlocksPacket(List<Guid> unlockedCosmetics)
        {
            UnlockedCosmetics = unlockedCosmetics;
        }
    }
}
