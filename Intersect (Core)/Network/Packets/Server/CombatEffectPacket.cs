using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CombatEffectPacket : IntersectPacket
    {
        // Empty for EF
        public CombatEffectPacket() { }

        public CombatEffectPacket(Guid targetId, float shakeAmount, Color entityFlashColor, string sound, float flashIntensity, float flashDuration, Color flashColor)
        {
            TargetId = targetId;
            ShakeAmount = shakeAmount;
            EntityFlashColor = entityFlashColor;
            Sound = sound;
            FlashIntensity = flashIntensity;
            FlashDuration = flashDuration;
            FlashColor = flashColor;
        }

        [Key(0)]
        public float ShakeAmount { get; set; }

        [Key(1)]
        public Color EntityFlashColor { get; set; }

        [Key(2)]
        public string Sound { get; set; }

        [Key(3)]
        public float FlashIntensity { get; set; }

        [Key(4)]
        public float FlashDuration { get; set; }

        [Key(5)]
        public Color FlashColor { get; set; }

        [Key(6)]
        public Guid TargetId { get; set; }
    }
}
