using Intersect.Enums;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public class WeaponPickerInterface
    {
        public int SelectedInventorySlot { get; set; }

        public Item SelectedItem { get; set; }

        private WeaponPickerResult ResultType { get; set; }

        private Player Owner { get; set; }

        private Guid CurrencyId { get; set; }

        private float CostMultiplier { get; set; }

        public WeaponPickerInterface(Player owner, WeaponPickerResult resultType, Guid currencyId, float costMultiplier)
        {
            Owner = owner;
            ResultType = resultType;
            CurrencyId = currencyId;
            CostMultiplier = costMultiplier;
        }

        /// <summary>
        /// Called via a "WeaponPickerChoosePacket" to notify the server which interface to send the player to
        /// </summary>
        /// <param name="invSlot">The inventory slot the player selected</param>
        public bool TryChooseWeapon(int invSlot)
        {
            Item item = null;
            if (!Owner?.TryGetItemAt(invSlot, out item) ?? false)
            {
                Owner?.SendDialogNotice($"Failed to get a weapon at slot {invSlot}.");
                return false;
            }

            if (item.Descriptor.ItemType != ItemTypes.Equipment || item.Descriptor.EquipmentSlot != Options.WeaponIndex)
            {
                Owner.SendDialogNotice($"The item at slot {invSlot} is not a weapon!");
                Logging.Log.Error($"The item at slot {invSlot} is not a weapon for player {Owner.Name}!");
                return false;
            }

            SelectedInventorySlot = invSlot;
            SelectedItem = item;
            return true;
        }

        public void OpenNextInterface()
        {
            switch(ResultType)
            {
                case WeaponPickerResult.Enhancement:
                    Owner.OpenEnhancement(CurrencyId, CostMultiplier);
                    break;
                case WeaponPickerResult.Upgrade:
                    Owner.OpenUpgradeStation(CurrencyId, CostMultiplier);
                    break;
                default:
                    throw new ArgumentException($"{ResultType} is not a valid ResultType for the WeaponPicker");
            }
        }
    }
}
