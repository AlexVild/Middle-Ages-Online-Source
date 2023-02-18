using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class OpenEnhancementPacket : IntersectPacket
    {
        [Key(0)]
        public Guid CurrencyId { get; set; }

        [Key(1)]
        public float CostMultiplier { get; set; }

        public OpenEnhancementPacket()
        {
        }

        public OpenEnhancementPacket(Guid currencyId, float costMultiplier)
        {
            CurrencyId = currencyId;
            CostMultiplier = costMultiplier;
        }
    }
}
