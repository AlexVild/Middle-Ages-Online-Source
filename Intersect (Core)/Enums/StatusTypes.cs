using System.Collections.Generic;
using System.ComponentModel;

namespace Intersect.Enums
{
    public static class StatusHelpers
    {
        public static List<StatusTypes> TenacityExcluded = new List<StatusTypes>()
        {
            StatusTypes.None,
            StatusTypes.Stealth,
            StatusTypes.Cleanse,
            StatusTypes.Invulnerable,
            StatusTypes.OnHit,
            StatusTypes.Shield,
            StatusTypes.Transform,
            StatusTypes.Swift,
            StatusTypes.Accurate,
            StatusTypes.Haste,
            StatusTypes.Steady,
            StatusTypes.Attuned,
            StatusTypes.ExpBoost,
            StatusTypes.TemporaryAngler,
            StatusTypes.TemporaryLumberjack,
            StatusTypes.TemporaryProspector,
        };

        public static List<EffectType> ResistanceEffects = new List<EffectType>()
        {
            EffectType.SilenceResist,
            EffectType.StunResistance,
            EffectType.SnareResistance,
            EffectType.BlindResistance,
            EffectType.SleepResistance,
            EffectType.SlowedResistance,
            EffectType.EnfeebledResistance,
            EffectType.ConfusionResistance,
            EffectType.KnockbackResistance,
        };

        public static Dictionary<StatusTypes, EffectType> ResistanceMap = new Dictionary<StatusTypes, EffectType>()
        {
            { StatusTypes.Silence, EffectType.SilenceResist },
            { StatusTypes.Stun, EffectType.StunResistance },
            { StatusTypes.Snare, EffectType.SnareResistance },
            { StatusTypes.Blind, EffectType.BlindResistance },
            { StatusTypes.Sleep, EffectType.SleepResistance },
            { StatusTypes.Slowed, EffectType.SlowedResistance },
            { StatusTypes.Enfeebled, EffectType.EnfeebledResistance },
            { StatusTypes.Confused, EffectType.ConfusionResistance },
        };

    }

    public enum StatusTypes
    {
        [Description("None")]
        None = 0,

        [Description("Silence")]
        Silence = 1,

        [Description("Stun")]
        Stun = 2,

        [Description("Snare")]
        Snare = 3,

        [Description("Blind")]
        Blind = 4,

        [Description("Invisibility")]
        Stealth = 5,

        [Description("Transform")]
        Transform = 6,

        [Description("Cleanse")]
        Cleanse = 7,

        [Description("Invulnerable")]
        Invulnerable = 8,

        [Description("Shield")]
        Shield = 9,

        [Description("Sleep")]
        Sleep = 10,

        [Description("On-Hit")]
        OnHit = 11,

        [Description("Taunt")]
        Taunt = 12,

        [Description("Swift")]
        Swift = 13,

        [Description("Accurate")]
        Accurate = 14,

        [Description("Haste")]
        Haste = 15,

        [Description("Slowed")]
        Slowed = 16,

        [Description("Confused")]
        Confused = 17,

        [Description("Steady")]
        Steady = 18,

        [Description("Attuned")]
        Attuned = 19,

        [Description("Enfeebled")]
        Enfeebled = 20,

        [Description("EXP Boost")]
        ExpBoost = 21,

        [Description("Temporary Angler")]
        TemporaryAngler = 22,

        [Description("Temporary Lumberjack")]
        TemporaryLumberjack = 23,

        [Description("Temporary Prospector")]
        TemporaryProspector = 24,
    }

}
