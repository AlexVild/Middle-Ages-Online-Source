using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using Intersect.Server.Networking;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public bool EnhancementOpen => Enhancement != default || EquipmentPicker != default;

        [NotMapped, JsonIgnore]
        public EnhancementInterface Enhancement { get; set; }

        [NotMapped, JsonIgnore]
        public bool UpgradeStationOpen => UpgradeStation != default || EquipmentPicker != default;

        [NotMapped, JsonIgnore]
        public ItemUpgradeInterface UpgradeStation { get; set; }

        public bool EnhancementTutorialDone { get; set; } = false;

        [NotMapped, JsonIgnore]
        public EquipmentPickerInterface EquipmentPicker { get; set; }

        public bool TryGetWeaponPicked(out Item weaponPicked)
        {
            weaponPicked = null;

            if (EquipmentPicker == null)
            {
                Logging.Log.Error($"Tried to get a weapon picked while WeaponPicker was null for player {Name}.");
                return false;
            }

            weaponPicked = EquipmentPicker.SelectedItem;

            return EquipmentPicker.SelectedItem != default;
        }

        public void OpenEnhancement(Guid currencyId, float multiplier)
        {
            Enhancement = new EnhancementInterface(this, currencyId, multiplier);
            PacketSender.SendOpenEnhancementWindow(this, currencyId, multiplier);
        }

        public void CloseEnhancement()
        {
            Enhancement = null;
            CloseEquipmentPicker();
        }

        public void OpenUpgradeStation(Guid currencyId, float multiplier)
        {
            UpgradeStation = new ItemUpgradeInterface(currencyId, multiplier, this);
            PacketSender.SendOpenUpgradeStation(this, currencyId, multiplier);
        }

        public void CloseUpgradeStation()
        {
            UpgradeStation = null;
            CloseEquipmentPicker();
        }

        public void OpenEquipmentPicker(WeaponPickerResult resultType, Guid currencyId, float costMultiplier)
        {
            EquipmentPicker = new EquipmentPickerInterface(this, resultType, currencyId, costMultiplier);
            PacketSender.SendOpenWeaponPicker(this, resultType);
        }

        public void CloseEquipmentPicker()
        {
            EquipmentPicker = null;
        }
    }
}

