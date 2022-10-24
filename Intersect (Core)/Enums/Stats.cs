using System;

namespace Intersect.Enums
{

    public enum Stats
    {

        Attack = 0, // Blunt Attack

        AbilityPower, // Magic Attack
        
        Defense, // Blunt Resistance

        MagicResist,

        Speed,

        SlashAttack,

        SlashResistance,

        PierceAttack,

        PierceResistance,

        Evasion,

        Accuracy,

        StatCount

    }

    /// <summary>
    /// A convenience mapping from an attack type to a stat
    /// </summary>
    public enum AttackTypes
    {
        Blunt = 0,
        Magic = 1,
        Slashing = 5,
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
