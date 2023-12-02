using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Localization;
using Intersect.Enums;
using System.Collections.Generic;

namespace Intersect.Client.Utilities
{
    public static class BonusEffectHelper
    {
        public static readonly Dictionary<EffectType, CharacterBonusInfo> BonusEffectDescriptions = new Dictionary<EffectType, CharacterBonusInfo>
        {
            {EffectType.None, new CharacterBonusInfo("None", "N/A")},
            {EffectType.CooldownReduction, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CooldownReduction], "Reduces length of skill cooldowns.")}, // Cooldown Reduction
            {EffectType.Lifesteal, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Lifesteal], "Gives life as a percentage of damage dealt.")}, // Lifesteal
            {EffectType.Tenacity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Tenacity], "Reduces negative status effect duration.")}, // Tenacity
            {EffectType.Luck, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Luck], "Increases chances for extra mob loot; ammo recovery; keeping items in PvP.")}, // Luck
            {EffectType.EXP, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.EXP], "Grants extra EXP when earned.")}, // Bonus Experience
            {EffectType.Affinity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Affinity], "Increases crit & weapon spell proc chance.")}, // Affinity
            {EffectType.CritBonus, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CritBonus], "Increases crit damage bonus.")}, // Critical bonus
            {EffectType.Swiftness, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Swiftness], "Increases weapon attack speed.")}, // Swiftness
            {EffectType.Prospector, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Prospector], "Increases harvest speed when mining.")}, // Prospector
            {EffectType.Angler, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Angler], "Increases harvest speed when fishing.")}, // Angler
            {EffectType.Lumberjack, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Lumberjack], "Increases harvest speed when woodcutting.")}, // Lumberjack
            {EffectType.Assassin, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Assassin], "Increases backstab/stealth attack modifier on weapons with backstab.")}, // Assassin
            {EffectType.Sniper, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Sniper], "Does more damage if attacks are at a longer range.")}, // Sniper
            {EffectType.Berzerk, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Berzerk], "Increases damage based on how many enemies are aggressive toward you.")}, // Berzerk
            {EffectType.Manasteal, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Manasteal], "Gives mana as a percentage of damage dealt.")}, // Manasteal
            {EffectType.Phantom, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Phantom], "Reduces sight range of enemies and reduces amount of aggro pulled from damage.")}, // Phantom
            {EffectType.Vampire, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Vampire], "Increases effects of manasteal and lifesteal.")}, // Vampire
            {EffectType.Junkrat, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Junkrat], "Increases scrap yield when deconstructing equipment.")}, // Junkrat
        };

        public static readonly List<EffectType> LowerIsBetterEffects = new List<EffectType>
        {
        };
    }
}
