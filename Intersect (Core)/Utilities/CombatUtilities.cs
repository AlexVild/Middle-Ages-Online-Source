﻿using Intersect.Enums;
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
            int[] attackerStats,
            int[] defenderStats,
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

            if (attackTypes.Count == 0)
            {
                // Default to blunt handling if nothing given, backwards compatible with old logic (def/atk used)
                attackTypes.Add(AttackTypes.Blunt);
            }

            // Go through each of the attack types that apply to the damage
            var totalDamage = 0;
            float decScaling = (float)scaling / 100; // scaling comes into this function as a percent number, i.e 110%, so we need that to be 1.1
            foreach (var element in attackTypes)
            {
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

                // = MEDIAN(0, FLOOR(G6 - (G6 * (0.1 + K6))), 10000)
                var baseVariance = 0.1;
                var lowVariance = baseVariance + resistanceMod;
                var highVariance = baseVariance - resistanceMod;

                var lowestDmg = dmg - (int)Math.Floor(dmg * lowVariance);
                var highestDmg = dmg + (int)Math.Ceiling(dmg * highVariance);

                if (highestDmg > maxHit)
                {
                    maxHit = highestDmg;
                }

                totalDamage += Randomization.Next(lowestDmg, highestDmg + 1);
            }

            return (int)Math.Round(totalDamage * critMultiplier);
        }

        /// <summary>
        /// Checks whether an attack misses, when given an attacker's accuracy vs. a defender's evasion
        /// </summary>
        /// <param name="accuracy">The attacker's accuracy stat</param>
        /// <param name="evasion">The defender's evasion stat</param>
        /// <returns>True if the attack misses the defender.</returns>
        public static bool AttackMisses(int accuracy, int evasion)
        {
            var missFactor = CalculateMissFactor(accuracy, evasion);
            var missChance = MissChance(missFactor);

            if (missChance <= 0)
            {
                return false;
            }
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
            if (playerMagicPwr > playerMeleePwr)
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
                return spell.Combat.DamageTypes.Count == 0 ? new List<AttackTypes>() { AttackTypes.Blunt } : spell.Combat.DamageTypes;
            }

            var types = spell.Combat.DamageTypes.Union(equippedItem?.AttackTypes ?? new List<AttackTypes>()).ToList();

            if (types.Count == 0)
            {
                return new List<AttackTypes>() { AttackTypes.Blunt };
            }

            return types;
        }
    }
}
