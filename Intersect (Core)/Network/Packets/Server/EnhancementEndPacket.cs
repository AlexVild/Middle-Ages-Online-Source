using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class EnhancementEndPacket : IntersectPacket
    {
        [Key(0)]
        public ItemProperties NewProperties { get; set; }

        public EnhancementEndPacket(ItemProperties newProperties)
        {
            NewProperties = newProperties;
        }
    }
}
