using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public class Deconstructor
    {
        public float FuelMultiplier { get; set; }

        public List<Item> Items { get; set; }

        public Player Owner { get; set; }

        public Deconstructor(float multiplier, Player owner)
        {
            FuelMultiplier = multiplier;
            Owner = owner;
        }

        /// <summary>
        /// Creates fuel out of valid items.
        /// </summary>
        /// <param name="fuelItems">A map of invSlot => quantity, of items</param>
        /// <returns>The amount of new fuel created</returns>
        public int CreateFuel(Dictionary<int, int> fuelItems)
        {
            int fuelCreated = 0;
            foreach (var kv in fuelItems)
            {
                var invSlot = kv.Key;
                var quantity = kv.Value;

                var slot = Owner.Items.ElementAtOrDefault(invSlot);
                if (slot == default || slot.ItemId == Guid.Empty)
                {
                    continue;
                }

                var item = ItemBase.Get(slot.ItemId);
                if (item.Fuel == 0)
                {
                    continue;
                }

                if (!Owner.TryTakeItem(slot, quantity, Enums.ItemHandling.Normal, sendUpdate: false))
                {
                    continue;
                }

                fuelCreated += item.Fuel * quantity;
            }

            Owner.Fuel += fuelCreated;
            PacketSender.SendInventory(Owner);
            PacketSender.SendFuelPacket(Owner);

            return fuelCreated;
        }
    }
}
