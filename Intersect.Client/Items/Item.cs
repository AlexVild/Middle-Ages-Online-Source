using System;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Items
{

    public class Item
    {

        public Guid? BagId;

        public Guid ItemId;

        public int Quantity;

        public ItemProperties ItemProperties;

        public ItemBase Base => ItemBase.Get(ItemId);

        public void Load(Guid id, int quantity, Guid? bagId, ItemProperties itemProperties)
        {
            ItemId = id;
            Quantity = quantity;
            BagId = bagId;
            ItemProperties = itemProperties;
        }

        public Item Clone()
        {
            var newItem = new Item()
            {
                ItemId = ItemId,
                Quantity = Quantity,
                BagId = BagId,
                ItemProperties = ItemProperties
            };

            return newItem;
        }

    }

}
