using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class SetLabelPacket : IntersectPacket
    {
        [Key(0)]
        public Guid DescriptorId { get; set; }

        public SetLabelPacket(Guid descriptorId)
        {
            DescriptorId = descriptorId;
        }
    }
}
