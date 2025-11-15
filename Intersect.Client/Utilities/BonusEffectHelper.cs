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
            {EffectType.CooldownReduction, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CooldownReduction], "Reduces length of skill cooldowns by X%.")}, // Cooldown Reduction
            {EffectType.Lifesteal, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Lifesteal], "Gives life as X% of damage dealt.")}, // Lifesteal
            {EffectType.Tenacity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Tenacity], "Reduces negative status effect duration by X%.")}, // Tenacity
            {EffectType.Luck, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Luck], "Increases chances to hit additional mob drop tables; ammo recovery; keeping items in PvP.")}, // Luck
            {EffectType.EXP, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.EXP], "EXP earned through mobs and inspiration is increased by X%.")}, // Bonus Experience
            {EffectType.Affinity, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Affinity], "A flat X% addition to your crit & spell proc chances.")}, // Affinity
            {EffectType.CritBonus, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.CritBonus], "Increases a weapon's crit damage bonus multiplier by X%.")}, // Critical bonus
            {EffectType.Swiftness, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Swiftness], "Increases weapon attack speed by X% of its base speed.")}, // Swiftness
            {EffectType.Prospector, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Prospector], "Increases harvest speed when mining by X%.")}, // Prospector
            {EffectType.Angler, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Angler], "Increases harvest speed when fishing by X%.")}, // Angler
            {EffectType.Lumberjack, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Lumberjack], "Increases harvest speed when woodcutting by X%.")}, // Lumberjack
            {EffectType.Assassin, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Assassin], "Increases backstab/stealth attack modifier by X%.")}, // Assassin
            {EffectType.Sniper, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Sniper], $"Does X% more damage if attacks are at least {Options.Instance.CombatOpts.SniperRequiredRange} tiles away from target.")}, // Sniper
            {EffectType.Berzerk, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Berzerk], $"Increases damage by X% if at least {Options.Instance.CombatOpts.BerzerkMinMobs} enemies are aggro to you.")}, // Berzerk
            {EffectType.Manasteal, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Manasteal], "Gives mana as X% of damage dealt.")}, // Manasteal
            {EffectType.Phantom, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Phantom], "Reduces sight range of enemies and reduces aggro build-up from damage by X%.")}, // Phantom
            {EffectType.Vampire, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Vampire], "Increases effects of manasteal and lifesteal by X%.")}, // Vampire
            {EffectType.Junkrat, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Junkrat], "Increases scrap yield when deconstructing equipment by X%.")}, // Junkrat
            {EffectType.Block, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Block], "A flat-chance to nullify incoming physical damage. Does not apply to non-weapon spells.")}, // Block
            {EffectType.Healer, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Healer], "Increases potency of healing spells by X%.")}, // Healer
            {EffectType.Foodie, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Foodie], "Increases HP recovery of consumables by X%.")}, // Foodie
            {EffectType.Manaflow, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Manaflow], "Increases MP recovery of consumables by X%.")}, // Manaflow
            {EffectType.Swiftshot, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.Swiftshot], "Increases speed of projectiles by X%.")}, // Swiftshot
            {EffectType.AmmoSaver, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.AmmoSaver], "Chance to not consume ammunition by X%.")}, // Ammo Saver
            {EffectType.SilenceResist, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.SilenceResist], "Chance to resist the silence effect.")},
            {EffectType.StunResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.StunResistance], "Chance to resist the stun effect.")},
            {EffectType.SnareResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.SnareResistance], "Chance to resist the snare effect.")},
            {EffectType.BlindResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.BlindResistance], "Chance to resist the blind effect.")},
            {EffectType.SleepResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.SleepResistance], "Chance to resist the sleep effect.")},
            {EffectType.SlowedResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.SlowedResistance], "Chance to resist the slowed effect.")},
            {EffectType.EnfeebledResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.EnfeebledResistance], "Chance to resist the enfeebled effect.")},
            {EffectType.ConfusionResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.ConfusionResistance], "Chance to resist the confusion effect.")},
            {EffectType.KnockbackResistance, new CharacterBonusInfo(Strings.ItemDescription.BonusEffects[(int)EffectType.KnockbackResistance], "Chance to resist knockback.")},
        };

        public static readonly List<EffectType> LowerIsBetterEffects = new List<EffectType>
        {
        };
    }
}
