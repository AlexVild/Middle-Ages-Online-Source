using System;

namespace Intersect.Server.Entities
{
    public class EnhancementInterface
    {
        public Player Owner { get; set; }

        public Guid CurrencyId { get; set; }
        
        public float CostMultiplier { get; set; }

        public EnhancementInterface(Player owner, Guid currencyId, float costMultiplier)
        {
            Owner = owner;
            CurrencyId = currencyId;
            CostMultiplier = costMultiplier;
        }
    }
}
