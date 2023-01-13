﻿using System.ComponentModel;

namespace Intersect.Enums
{

    public enum EffectType : byte
    {

        [Description("None")]
        None = 0,

        [Description("Cooldown Reduction")]
        CooldownReduction = 1,

        [Description("Lifesteal")]
        Lifesteal = 2,

        [Description("Tenacity")]
        Tenacity = 3,

        [Description("Luck")]
        Luck = 4,

        [Description("EXP Boost")]
        EXP = 5,

        [Description("Affinity")]
        Affinity = 6,

        [Description("Crit. Bonus")]
        CritBonus = 7,

        [Description("Swiftness")]
        Swiftness = 8,

    }

}
