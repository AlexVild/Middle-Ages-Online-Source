using System.ComponentModel;

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

        [Description("Prospector")]
        Prospector = 9,

        [Description("Angler")]
        Angler = 10,

        [Description("Lumberjack")]
        Lumberjack = 11,

        [Description("Assassin")]
        Assassin = 12,

        [Description("Sniper")]
        Sniper = 13,

        [Description("Berzerk")]
        Berzerk = 14,

        [Description("Manasteal")]
        Manasteal = 15,

        [Description("Phantom")]
        Phantom = 16,

        [Description("Vampire")]
        Vampire = 17,

        [Description("Junkrat")]
        Junkrat = 18,

        [Description("Block")]
        Block = 19,

        [Description("Healer")]
        Healer = 20,
        
        [Description("Foodie")]
        Foodie = 21,

        [Description("Manaflow")]
        Manaflow = 22,

        [Description("Swiftshot")]
        Swiftshot = 23,

        [Description("Ammo Saver")]
        AmmoSaver = 24,
    }
}
