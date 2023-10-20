using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Intersect.Server.Networking;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public bool EnhancementOpen => Enhancement != default;

        [NotMapped, JsonIgnore]
        public EnhancementInterface Enhancement { get; set; }

        [NotMapped, JsonIgnore]
        public bool UpgradeStationOpen => UpgradeStation != default;

        [NotMapped, JsonIgnore]
        public ItemUpgradeInterface UpgradeStation { get; set; }

        public bool EnhancementTutorialDone { get; set; } = false;

        public void OpenEnhancement(Guid currencyId, float multiplier)
        {
            Enhancement = new EnhancementInterface(this, currencyId, multiplier);
            PacketSender.SendOpenEnhancementWindow(this, currencyId, multiplier);
        }

        public void CloseEnhancement()
        {
            Enhancement = null;
        }

        public void OpenUpgradeStation(Guid currencyId, float multiplier)
        {
            UpgradeStation = new ItemUpgradeInterface(currencyId, multiplier, this);
            PacketSender.SendOpenUpgradeStation(this, currencyId, multiplier);
        }

        public void CloseUpgradeStation()
        {
            UpgradeStation = null;
        }
    }
}

