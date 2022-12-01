using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class RecipeDisplayPacket : IntersectPacket
    {
        [Key(0)]
        public Guid DescriptorId { get; set; }

        [Key(1)]
        public bool IsUnlocked { get; set; }

        public RecipeDisplayPacket() {}

        public RecipeDisplayPacket(Guid descriptorId, bool isUnlocked)
        {
            DescriptorId = descriptorId;
            IsUnlocked = isUnlocked;
        }
    }

    [MessagePackObject]
    public class RecipeDisplayPackets : IntersectPacket
    {
        [Key(0)]
        public List<RecipeDisplayPacket> Packets { get; set; }

        public RecipeDisplayPackets() {}

        public RecipeDisplayPackets(List<RecipeDisplayPacket> packets)
        {
            Packets = packets;
        }
    }
}
