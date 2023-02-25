using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.General.UpgradeStation
{
    public sealed class UpgradeStationInterface
    {
        public Guid CurrencyId { get; set; }
        public ItemBase Currency => ItemBase.Get(CurrencyId);
        public float CostMultiplier { get; set; }
        
        public bool RefreshUi { get; set; }
        public bool IsOpen { get; set; }

        public ItemBase UpgradeItem { get; set; }
        public ItemProperties UpgradeItemProperties { get; set; }

        public Guid[] Crafts { get; set; }

        public Guid SelectedCraftId { get; set; }

        public void Open(Guid currencyId, float costMulti, Guid[] crafts, ItemBase upgradeItem, ItemProperties properties)
        {
            UpgradeItem = upgradeItem;
            UpgradeItemProperties = new ItemProperties(properties);
            CostMultiplier = costMulti;
            CurrencyId = currencyId;
            Crafts = crafts;

            IsOpen = true;
            Interface.Interface.GameUi?.UpgradeStationWindow?.Show();
            RefreshUi = true;
        }

        public void Close()
        {
            RefreshUi = false;
            IsOpen = false;
        }
    }
}
