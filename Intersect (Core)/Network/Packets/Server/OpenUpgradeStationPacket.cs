using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class OpenUpgradeStationPacket : IntersectPacket
    {
        [Key(0)]
        public Guid CurrencyId { get; set; }

        [Key(1)]
        public float CostMultiplier { get; set; }

        [Key(2)]
        public Guid[] AvailableUpgrades { get; set; }

        [Key(3)]
        public Guid UpgradingGuid { get; set; }

        [Key(4)]
        public ItemProperties UpgradingProperties { get; set; }

        public OpenUpgradeStationPacket(Guid currencyId, float costMultiplier, Guid[] availableUpgrades, Guid upgradingGuid, ItemProperties upgradingProperties)
        {
            CurrencyId = currencyId;
            CostMultiplier = costMultiplier;
            AvailableUpgrades = availableUpgrades;
            UpgradingGuid = upgradingGuid;
            UpgradingProperties = upgradingProperties;
        }

        public OpenUpgradeStationPacket()
        {
        }
    }
}
