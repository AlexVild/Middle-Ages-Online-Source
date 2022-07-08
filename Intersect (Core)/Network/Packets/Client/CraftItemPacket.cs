using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class CraftItemPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public CraftItemPacket()
        {
        }

        public CraftItemPacket(Guid craftId, int amount)
        {
            CraftId = craftId;
            Amount = amount;
        }

        [Key(0)]
        public Guid CraftId { get; set; }

        [Key(1)]
        public int Amount { get; set; }
    }

}
