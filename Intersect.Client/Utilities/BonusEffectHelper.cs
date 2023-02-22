using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Localization;
using Intersect.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            {EffectType.Luck, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Luck], "Increases chances for extra mob loot, ammo recovery.")}, // Luck
            {EffectType.EXP, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.EXP], "Grants extra EXP when earned.")}, // Bonus Experience
            {EffectType.Affinity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Affinity], "Increases crit chance.")}, // Affinity
            {EffectType.CritBonus, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CritBonus], "Increases crit damage bonus.")}, // Critical bonus
            {EffectType.Swiftness, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Swiftness], "Increases weapon attack speed.")}, // Swiftness
        };

        public static readonly List<EffectType> LowerIsBetterEffects = new List<EffectType>
        {
        };
    }
}
