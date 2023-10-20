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

        [Key(2)]
        public Guid[] KnownEnhancements { get; set; }

        public OpenEnhancementPacket()
        {
        }

        public OpenEnhancementPacket(Guid currencyId, float costMultiplier, Guid[] knownEhancements)
        { 
            CurrencyId = currencyId;
            CostMultiplier = costMultiplier;
            KnownEnhancements = knownEhancements;
        }
    }
}
