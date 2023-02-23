using System;
using System.ComponentModel;

namespace Intersect.Enums
{

    public enum Stats
    {
        [Description("Blunt Attack")]
        Attack = 0, // Blunt Attack

        [Description("Magic Attack")]
        AbilityPower, // Magic Attack

        [Description("Blunt Resistance")]
        Defense, // Blunt Resistance

        [Description("Magic Resistance")]
        MagicResist,

        [Description("Speed")]
        Speed,

        [Description("Slash Attack")]
        SlashAttack,

        [Description("Slash Resistance")]
        SlashResistance,

        [Description("Pierce Attack")]
        PierceAttack,

        [Description("Pierce Resistance")]
        PierceResistance,

        [Description("Evasion")]
        Evasion,

        [Description("Accuracy")]
        Accuracy,

        [Description("Stat Count")]
        StatCount

    }

    /// <summary>
    /// A convenience mapping from an attack type to a stat
    /// </summary>
    public enum AttackTypes
    {
        [Description("Blunt")]
        Blunt = 0,
        [Description("Magical")]
        Magic = 1,
        [Description("Slashing")]
        Slashing = 5,
        [Description("Piercing")]
        Piercing = 7,
    }

    public class StatHelpers
    {
        public static Stats GetResistanceStat(AttackTypes attackType)
        {
            switch(attackType)
            {
                case AttackTypes.Blunt:
                    return Stats.Defense;
                case AttackTypes.Magic:
                    return Stats.MagicResist;
                case AttackTypes.Piercing:
                    return Stats.PierceResistance;
                case AttackTypes.Slashing:
                    return Stats.SlashResistance;
                default:
                    throw new NotImplementedException($"Invalid attack type given: {attackType}");
            }
        }
    }

}
