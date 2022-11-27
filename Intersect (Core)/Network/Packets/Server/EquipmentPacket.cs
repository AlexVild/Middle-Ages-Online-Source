using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class EquipmentPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public EquipmentPacket()
        {
        }

        //We send item ids for the equipment of others, but we send the correlating inventory slots for wearers because we want the equipped icons to show in the inventory.
        public EquipmentPacket(Guid entityId, int[] invSlots, Guid[] itemIds, string[] decor, Guid[] cosmeticItemIds)
        {
            EntityId = entityId;
            InventorySlots = invSlots;
            ItemIds = itemIds;
            Decor = decor;
            CosmeticItemIds = cosmeticItemIds;
        }

        [Key(0)]
        public Guid EntityId { get; set; }

        [Key(1)]
        public int[] InventorySlots { get; set; }

        [Key(2)]
        public Guid[] ItemIds { get; set; }

        [Key(3)]
        public string[] Decor { get; set; }

        [Key(4)]
        public Guid[] CosmeticItemIds { get; set; }
    }

}
