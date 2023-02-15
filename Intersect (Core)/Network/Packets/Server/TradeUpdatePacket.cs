using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class TradeUpdatePacket : InventoryUpdatePacket
    {
        //Parameterless Constructor for MessagePack
        public TradeUpdatePacket() : base(0, Guid.Empty, 0, null, null)
        {
        }

        public TradeUpdatePacket(Guid traderId, int slot, Guid id, int quantity, Guid? bagId, ItemProperties itemProperties) : base(
            slot, id, quantity, bagId, itemProperties
        )
        {
            TraderId = traderId;
        }

        [Key(6)]
        public Guid TraderId { get; set; }

    }

}
