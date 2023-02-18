using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.General.Enhancement
{
    public sealed class Enhancement
    {
        public Guid CurrencyId { get; set; }
        public ItemBase Currency => ItemBase.Get(CurrencyId);

        public float CostMultiplier { get; set; }

        public void Open(Guid currencyId, float costMulti)
        {
            CurrencyId = currencyId;
            CostMultiplier = costMulti;

            Interface.Interface.GameUi?.EnhancementWindow?.Show();
        }

        public void Close()
        {

        }
    }
}
