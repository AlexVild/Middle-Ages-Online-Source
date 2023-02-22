﻿using Intersect.Enums;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public partial class ItemProperties : IntersectPacket
    {
        public ItemProperties()
        {
        }

        public ItemProperties(ItemProperties other)
        {
            if (other == default)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Array.Copy(other.StatModifiers, StatModifiers, (int)Stats.StatCount);
            Array.Copy(other.StatEnhancements, StatEnhancements, (int)Stats.StatCount);
            Array.Copy(other.VitalEnhancements, VitalEnhancements, (int)Vitals.VitalCount);
            Array.Copy(other.EffectEnhancements, EffectEnhancements, Enum.GetNames(typeof(EffectType)).Length);

            EnhancedBy = other.EnhancedBy;

            AppliedEnhancementIds.Clear();
            AppliedEnhancementIds.AddRange(other.AppliedEnhancementIds);
        }

        [Key(0)]
        public int[] StatModifiers { get; set; } = new int[(int)Stats.StatCount];

        [Key(1)]
        public int[] StatEnhancements = new int[(int)Stats.StatCount];

        [Key(2)]
        public int[] VitalEnhancements = new int[(int)Vitals.VitalCount];

        [Key(3)]
        public int[] EffectEnhancements = new int[Enum.GetNames(typeof(EffectType)).Length];

        [Key(4)]
        public List<Guid> AppliedEnhancementIds = new List<Guid>();

        [Key(5)]
        public string EnhancedBy { get; set; }
    }

    [MessagePackObject]
    public class ItemEnhancement
    {
        [Key(0)]
        public Guid BaseEnhancementId { get; set; }

        [Key(1)]
        public int Value { get; set; }

        public ItemEnhancement(Guid baseEnhancementId, int value)
        {
            BaseEnhancementId = baseEnhancementId;
            Value = value;
        }
    }
}