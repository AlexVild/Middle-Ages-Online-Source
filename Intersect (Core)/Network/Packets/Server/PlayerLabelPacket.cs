using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PlayerLabelPacket : IntersectPacket
    {
        [Key(0)]
        public Guid DescriptorId { get; set; }

        [Key(1)]
        public bool IsNew { get; set; }

        public PlayerLabelPacket(Guid descriptorId, bool isNew)
        {
            DescriptorId = descriptorId;
            IsNew = isNew;
        }
    }

    [MessagePackObject]
    public class PlayerLabelPackets : IntersectPacket
    {
        [Key(0)]
        public List<PlayerLabelPacket> Packets { get; set; }

        public PlayerLabelPackets(List<PlayerLabelPacket> packets)
        {
            Packets = packets;
        }
    }
}
