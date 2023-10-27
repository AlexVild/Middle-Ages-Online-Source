using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Utilities
{
    public static class CombatUtilities
    {
        private const float MissHeight = 0.42f;
        private const float MissWidth = -0.012f;
        private const float MissDefaultPercent = 0.1f;

        // The points at which a miss factor becomes a problem
        private const int DangerMissFactor = 25;

        public static int CalculateDamage(
            List<AttackTypes> attackTypes,
            double critMultiplier,
            int scaling,
            in int[] attackerStats,
            in int[] defenderStats,
            out int maxHit)
        {
            maxHit = 0;
            if (attackerStats.Length != (int)Stats.StatCount)
            {
                throw new ArgumentException("Invalid attacker stats given", nameof(attackerStats));
            }
            if (defenderStats.Length != (int)Stats.StatCount)
            {
                throw new ArgumentException("Invalid defensive stats given", nameof(defenderStats));
            }

            var atkTypesCopy = new List<AttackTypes>();
            atkTypesCopy.AddRange(attackTypes);

            // Go through each of the attack types that apply to the damage
            var totalDamage = 0;
            float decScaling = (float)scaling / 100; // scaling comes into this function as a percent number, i.e 110% needs to be 1.1
            foreach (var element in atkTypesCopy)
            {
                if ((int)element < 0 || (int)element > attackerStats.Length)
                {
                    continue;
                }

                var dmg = (int)Math.Round(attackerStats[(int)element] * decScaling);
                // If we're not gonna be doing damage, dismiss
                if (dmg == 0)
                {
                    continue;
                }

                // Otherwise, 
                var resConst = 0.2;
                var resistance = resConst * defenderStats[(int)StatHelpers.GetResistanceStat(element)];
                var resistanceMod = MathHelper.Clamp(-1.0, resistance / dmg, 1.0);

                var baseVariance = 0.1;
                var lowVariance = baseVariance + resistanceMod;
                var highVariance = baseVariance - resistanceMod;

                var lowestDmg = dmg - (int)Math.Floor(dmg * lowVariance);
                var highestDmg = dmg + (int)Math.Ceiling(dmg * highVariance);

                if ((highestDmg + maxHit) > maxHit)
                {
                    maxHit += highestDmg;
                }

                var low = Math.Min(lowestDmg, highestDmg);
                var high = Math.Max(lowestDmg, highestDmg);

                totalDamage += Randomization.Next(low, high + 1);
            }

            // Cap critmult at 2.5x
            return (int)Math.Round(totalDamage * Math.Min(critMultiplier, 2.5f));
        }

        /// <summary>
        /// Does not take into account defense stats for friendly attacks
        /// </summary>
        /// <param name="attackTypes"></param>
        /// <param name="critMultiplier"></param>
        /// <param name="scaling"></param>
        /// <param name="attackerStats"></param>
        /// <param name="maxHit"></param>
        /// <returns></returns>
        public static int CalculateFriendlyDamage(List<AttackTypes> attackTypes,
            double critMultiplier,
            int scaling,
            in int[] attackerStats,
            out int maxHit)
        {
            maxHit = 0;
            if (attackerStats.Length != (int)Stats.StatCount)
            {
                throw new ArgumentException("Invalid attacker stats given", nameof(attackerStats));
            }

            var atkTypesCopy = new List<AttackTypes>();
            atkTypesCopy.AddRange(attackTypes);

            // Go through each of the attack types that apply to the damage
            var totalDamage = 0;
            float decScaling = (float)scaling / 100; // scaling comes into this function as a percent number, i.e 110% needs to be 1.1
            foreach (var element in atkTypesCopy)
            {
                if ((int)element < 0 || (int)element > attackerStats.Length)
                {
                    continue;
                }

                var dmg = (int)Math.Round(attackerStats[(int)element] * decScaling);
                // If we're not gonna be doing damage, dismiss
                if (dmg == 0)
                {
                    continue;
                }

                // Otherwise, 
                var baseVariance = 0.1;

                var lowestDmg = dmg - (int)Math.Floor(dmg * baseVariance);
                var highestDmg = dmg + (int)Math.Ceiling(dmg * baseVariance);

                if ((highestDmg + maxHit) > maxHit)
                {
                    maxHit += highestDmg;
                }

                var low = Math.Min(lowestDmg, highestDmg);
                var high = Math.Max(lowestDmg, highestDmg);

                totalDamage += Randomization.Next(low, high + 1);
            }

            if (totalDamage > 0)
            {
                totalDamage *= -1;
            }

            return (int)Math.Round(totalDamage * critMultiplier);
        }

        public static float CalculateDps(List<AttackTypes> attackTypes,
            double critMultiplier,
            int scaling,
            int[] attackerStats,
            int[] defenderStats,
            int attackTimeMs)
        {
            CalculateDamage(attackTypes, critMultiplier, scaling, attackerStats, defenderStats, out var maxHit);

            var hitsPerSecond = 1000.0f / attackTimeMs;

            return maxHit * hitsPerSecond;
        }

        /// <summary>
        /// Checks whether an attack misses, when given an attacker's accuracy vs. a defender's evasion
        /// </summary>
        /// <param name="accuracy">The attacker's accuracy stat</param>
        /// <param name="evasion">The defender's evasion stat</param>
        /// <returns>True if the attack misses the defender.</returns>
        public static bool AttackMisses(int accuracy, int evasion, bool advantage)
        {
            var missFactor = CalculateMissFactor(accuracy, evasion);
            var missChance = MissChance(missFactor);

            if (missChance <= 0)
            {
                return false;
            }

            // Got rid of evasion, just this now
            return Randomization.NextDouble() <= missChance;
        }

        /// <summary>
        /// We use ATan here because it allows for a diminishing return on both sides of accuracy/evasion increases
        /// </summary>
        /// <param name="missFactor">A number representing how strong a miss is to occur - a negative number favors evade</param>
        /// <returns>A percentage the attack missed</returns>
        public static double MissChance(int missFactor)
        {
            var radians = Math.Atan(MissWidth * missFactor);
            var missChance = MissHeight * radians + MissDefaultPercent;
            return Math.Max(missChance, 0);
        }

        public static int CalculateMissFactor(int accuracy, int evasion)
        {
            return accuracy - evasion;
        }

        /// <summary>
        /// Makes a guess as to whether or not some entity is a spell caster, and, if so, uses their ability power in threat
        /// estimation instead of their melee stats
        /// </summary>
        /// <param name="stats">The stats of the entity in question</param>
        /// <returns>A list of relevant attack types</returns>
        public static List<AttackTypes> EstimateEntityAttackTypes(int[] stats, List<AttackTypes> meleeTypes)
        {
            var atkTypes = new List<AttackTypes>();
            var playerMagicPwr = stats[(int)Stats.AbilityPower];

            var playerBluntDmg = stats[(int)Stats.Attack];
            var playerPierceDmg = stats[(int)Stats.PierceAttack];
            var playerSlashDmg = stats[(int)Stats.SlashAttack];
            var playerMeleePwr = playerBluntDmg + playerPierceDmg + playerSlashDmg;

            // Chooses magic as attack type if player's magic is superior to other offensive stats
            if (playerMagicPwr >= playerMeleePwr)
            {
                atkTypes.Add(AttackTypes.Magic);
            }
            else
            {
                atkTypes.AddRange(meleeTypes);
            }

            return atkTypes;
        }

        public static List<AttackTypes> GetSpellAttackTypes(SpellBase spell, ItemBase equippedItem)
        {
            if (spell == default)
            {
                return new List<AttackTypes>();
            }

            if (!spell.WeaponSpell)
            {
                return spell.Combat.DamageTypes.Count == 0 ? new List<AttackTypes>() : spell.Combat.DamageTypes;
            }

            var types = spell.Combat.DamageTypes.Union(equippedItem?.AttackTypes ?? new List<AttackTypes>()).ToList();

            if (types.Count == 0)
            {
                return new List<AttackTypes>();
            }

            return types;
        }

        const int DAMAGE_FLOOR = 4;
        public static float TierToDamageFormula(int equipmentTier)
        {
            if (equipmentTier <= 1)
            {
                return DAMAGE_FLOOR;
            }

            return (TierToDamageFormula(equipmentTier - 1) + 2) * 1.1f;
        }


        const float HIGH_ARMOR_RES = 0.6f;
        const float MED_ARMOR_RES = 0.45f;
        const float LOW_ARMOR_RES = 0.20f;
        const float ARMOR_MULT = 0.45f;
        const float HELMET_MULT = 0.35f;
        const float BOOTS_MULT = 0.2f;

        public enum ResistanceLevel 
        {
            High,
            Medium,
            Low 
        }
        
        private static Dictionary<ResistanceLevel, float> ArmorResistanceConstants = new Dictionary<ResistanceLevel, float>()
        {
            {  ResistanceLevel.High, HIGH_ARMOR_RES },
            {  ResistanceLevel.Medium, MED_ARMOR_RES },
            {  ResistanceLevel.Low, LOW_ARMOR_RES },
        };

        public static float TierAndSlotToArmorRatingFormula(int equipmentTier, int slot, ResistanceLevel resistanceLevel)
        {
            if (!ArmorResistanceConstants.ContainsKey(resistanceLevel))
            {
                return 0f;
            }

            float slotMultiplier;
            if (slot == Options.Instance.EquipmentOpts.ArmorSlot)
            { 
                slotMultiplier = ARMOR_MULT;
            }
            else if (slot == Options.Instance.EquipmentOpts.HelmetSlot)
            {
                slotMultiplier = HELMET_MULT;
            }
            else if (slot == Options.Instance.EquipmentOpts.BootsSlot)
            {
                slotMultiplier = BOOTS_MULT;
            }
            else
            {
                slotMultiplier = 1.0f;
            }

            var damageAtTier = TierToDamageFormula(equipmentTier);
            var fullResPoints = damageAtTier * 5 * ArmorResistanceConstants[resistanceLevel];

            return slotMultiplier * fullResPoints;
        }

        public static int MaxDefenseAtTier(int tier)
        {
            var maxHelmet = TierAndSlotToArmorRatingFormula(tier, Options.Equipment.HelmetSlot, ResistanceLevel.High);
            var maxArmor = TierAndSlotToArmorRatingFormula(tier, Options.Equipment.ArmorSlot, ResistanceLevel.High);
            var maxBoots = TierAndSlotToArmorRatingFormula(tier, Options.Equipment.BootsSlot, ResistanceLevel.High);

            return (int)Math.Ceiling(maxHelmet + maxArmor + maxBoots);
        }

        public static bool TryCapStatsToTier(int tier, int[] statVals, out int[] scaledStats)
        {
            var clonedStats = CloneStats(statVals);
            scaledStats = statVals.Select((statVal, statIdx) =>
            {
                switch ((Stats)statIdx)
                {
                    case Stats.Attack:
                    case Stats.SlashAttack:
                    case Stats.PierceAttack:
                    case Stats.AbilityPower:
                        return (int)Math.Ceiling(TierToDamageFormula(tier));
                    case Stats.Defense:
                    case Stats.SlashResistance:
                    case Stats.PierceResistance:
                    case Stats.MagicResist:
                        return MaxDefenseAtTier(tier);
                    default:
                        return statVal;
                }
            }).ToArray();

            return !Enumerable.SequenceEqual(clonedStats, scaledStats);
        }

        public static bool TryCapStatToTier(int tier, Stats stat, ref int scaledStat)
        {
            var originalValue = scaledStat;

            var maxStat = scaledStat;

            switch (stat)
            {
                case Stats.Attack:
                case Stats.SlashAttack:
                case Stats.PierceAttack:
                case Stats.AbilityPower:
                    maxStat = (int)Math.Ceiling(TierToDamageFormula(tier));
                    break;
                case Stats.Defense:
                case Stats.SlashResistance:
                case Stats.PierceResistance:
                case Stats.MagicResist:
                    maxStat =  MaxDefenseAtTier(tier);
                    break;
            }

            scaledStat = Math.Min(maxStat, scaledStat);

            return originalValue != scaledStat;
        }

        public static int[] CloneStats(int[] stats)
        {
            var clonedStats = new int[(int)Stats.StatCount];
            Array.Copy(stats, clonedStats, clonedStats.Length);

            return clonedStats;
        }

        public static int[] GetOverriddenStats(Dictionary<int, int> overrides, int[] stats)
        {
            var atkStats = new int[(int)Stats.StatCount];
            Array.Copy(stats, atkStats, atkStats.Length);
            if (overrides != default)
            {
                foreach (var damOverrideKv in overrides)
                {
                    if (damOverrideKv.Value == 0)
                    {
                        continue;
                    }

                    int stat = damOverrideKv.Key;
                    int amount = damOverrideKv.Value;

                    if (stat >= 0 && stat < atkStats.Length)
                    {
                        atkStats[stat] = amount;
                    }
                }
            }

            return atkStats;
        }
    }
}
