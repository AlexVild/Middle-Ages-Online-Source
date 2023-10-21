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
        public bool EnhancementOpen => Enhancement != default || WeaponPicker != default;

        [NotMapped, JsonIgnore]
        public EnhancementInterface Enhancement { get; set; }

        [NotMapped, JsonIgnore]
        public bool UpgradeStationOpen => UpgradeStation != default || WeaponPicker != default;

        [NotMapped, JsonIgnore]
        public ItemUpgradeInterface UpgradeStation { get; set; }

        public bool EnhancementTutorialDone { get; set; } = false;

        [NotMapped, JsonIgnore]
        public WeaponPickerInterface WeaponPicker { get; set; }

        public bool TryGetWeaponPicked(out Item weaponPicked)
        {
            weaponPicked = null;

            if (WeaponPicker == null)
            {
                Logging.Log.Error($"Tried to get a weapon picked while WeaponPicker was null for player {Name}.");
                return false;
            }

            weaponPicked = WeaponPicker.SelectedItem;

            return WeaponPicker.SelectedItem != default;
        }

        public void OpenEnhancement(Guid currencyId, float multiplier)
        {
            Enhancement = new EnhancementInterface(this, currencyId, multiplier);
            PacketSender.SendOpenEnhancementWindow(this, currencyId, multiplier);
        }

        public void CloseEnhancement()
        {
            Enhancement = null;
            CloseWeaponPicker();
        }

        public void OpenUpgradeStation(Guid currencyId, float multiplier)
        {
            UpgradeStation = new ItemUpgradeInterface(currencyId, multiplier, this);
            PacketSender.SendOpenUpgradeStation(this, currencyId, multiplier);
        }

        public void CloseUpgradeStation()
        {
            UpgradeStation = null;
            CloseWeaponPicker();
        }

        public void OpenWeaponPicker(WeaponPickerResult resultType, Guid currencyId, float costMultiplier)
        {
            WeaponPicker = new WeaponPickerInterface(this, resultType, currencyId, costMultiplier);
            PacketSender.SendOpenWeaponPicker(this);
        }

        public void CloseWeaponPicker()
        {
            WeaponPicker = null;
        }
    }
}

