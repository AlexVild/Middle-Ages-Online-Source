using Intersect.Enums;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Utilities
{
    public static class ThreatLevelUtilities
    {
        /// <summary>
        /// Contains a mapping of threat levels to their client-side colors.
        /// </summary>
        public static Dictionary<ThreatLevel, Color> ColorMapping = new Dictionary<ThreatLevel, Color>
        {
            { ThreatLevel.Midnight, new Color(255, 174, 118, 255) },
            { ThreatLevel.Extreme, new Color(255, 224, 112, 178) },
            { ThreatLevel.Deadly, new Color(255, 222, 124, 112) },
            { ThreatLevel.Threatening, new Color(255, 200, 145, 62) },
            { ThreatLevel.Fair, new Color(255, 166, 167, 37) },
            { ThreatLevel.Wimpy, new Color(255, 99, 196, 70) },
            { ThreatLevel.Trivial, new Color(255, 86, 179, 192) },
        };

        /// <summary>
        /// Determines an NPC's <see cref="ThreatLevel"/> based on how quickly the player and NPC would fell eachother in battle. Makes some
        /// guesses as to whether each opponent is a spell caster or not, and also a best guess at how accuracy would come in to play.
        /// </summary>
        /// <param name="playerVitals">The max vital levels of the player</param>
        /// <param name="playerStats">The player's stats</param>
        /// <param name="npcVitals">The max vital levels of the NPC</param>
        /// <param name="npcStats">The NPC's stats</param>
        /// <param name="playerMelee">The attack types the player would use in melee combat</param>
        /// <param name="npcMelee">The attack types the NPC would use in melee combat</param>
        /// <param name="playerAttackSpeed">The player's base attack speed, unaffected by boons</param>
        /// <param name="npcAttackSpeed">The NPC's base attack speed</param>
        /// <returns>The associated <see cref="ThreatLevel"/> according to the ratio thresholds set in the server config.</returns>
        public static ThreatLevel DetermineNpcThreatLevel(int[] playerVitals, 
            int[] playerStats, 
            int[] npcVitals, 
            int[] npcStats, 
            List<AttackTypes> playerMelee, 
            List<AttackTypes> npcMelee, 
            long playerAttackSpeed, 
            long npcAttackSpeed,
            bool playerIsRanged,
            bool npcSpellCaster,
            int maxNpcScalar)
        {
            var ratio = GetThreatLevelRatio(playerVitals, playerStats, npcVitals, npcStats, playerMelee, npcMelee, playerAttackSpeed, npcAttackSpeed, playerIsRanged, npcSpellCaster);
            return GetThreatLevelFromRatio(ratio);
        }

        private static double GetThreatLevelRatio(int[] playerVitals,
            int[] playerStats,
            int[] npcVitals,
            int[] npcStats,
            List<AttackTypes> playerMelee,
            List<AttackTypes> npcMelee,
            long playerAttackSpeed,
            long npcAttackSpeed,
            bool playerIsRanged,
            bool npcSpellCaster,
            int maxNpcScalar = 100)
        {
            var maxEnemyHp = npcVitals[(int)Vitals.Health];
            var maxHp = playerVitals[(int)Vitals.Health];

            // This will make a guess at whether or not the NPC or player is a spell caster and override their attack types if so (since we don't
            // wan't to use their melee attack types in those cases)
            var playerAttackTypes = CombatUtilities.EstimateEntityAttackTypes(playerStats, playerMelee);
            
            var npcAttackTypes = npcMelee;
            if (npcSpellCaster)
            {
                npcAttackTypes = new List<AttackTypes>() { AttackTypes.Magic };
            }

            // If we've determined the player to be a spell caster, use global cool down instead of attack time
            if (playerAttackTypes.Count == 1 && playerAttackTypes[0] == AttackTypes.Magic)
            {
                playerAttackSpeed = Options.Combat.GlobalCooldownDuration;
            }

            playerIsRanged = playerIsRanged || (playerAttackTypes.Count == 1 && playerAttackTypes.Contains(AttackTypes.Magic));

            CombatUtilities.CalculateDamage(playerAttackTypes, 1, 100, playerStats, npcStats, out var playerMax);
            CombatUtilities.CalculateDamage(npcAttackTypes, 1, 100, npcStats, playerStats, out var npcMax);

            npcMax = (int)Math.Ceiling(maxNpcScalar / 100f * npcMax);

            var attacksToKill = Math.Ceiling((float)maxEnemyHp / playerMax);
            var attacksToDie = Math.Ceiling((float)maxHp / npcMax);

            // Make a best guess at how missed attacks might come in to play by elongating the amount of total attacks needed for death based on miss percentages
            if (playerIsRanged)
            {
                playerStats[(int)Stats.Evasion] += 10; // Evasion boost in calculations for ranged attackers
            }
            var playerMissChance = CombatUtilities.MissChance(CombatUtilities.CalculateMissFactor(playerStats[(int)Stats.Accuracy], npcStats[(int)Stats.Evasion]));
            var npcMissChance = CombatUtilities.MissChance(CombatUtilities.CalculateMissFactor(npcStats[(int)Stats.Accuracy], playerStats[(int)Stats.Evasion]));
            var attacksMissed = Math.Floor(attacksToKill * playerMissChance);
            var attacksDodged = Math.Floor(attacksToDie * npcMissChance);

            var timeToKill = (attacksToKill + attacksMissed) * playerAttackSpeed;
            var timeToDie = (attacksToDie + attacksDodged) * npcAttackSpeed;

            /* This ratio informs you how many times you could kill this enemy before it kills you. If 1 / X,
             * then a player could kill that mob X# of times before the player would die. If X > 0,
             * then the mob could kill the player Floor(X) times over.
             */
            return timeToKill / timeToDie;

            // Deprecated attempt at factoring in time to the equation
            /*var timeRatio = timeToKill / timeToDie;
            var timeDifference = Math.Abs(timeToDie - timeToKill);

            return timeRatio / (timeDifference / 3000);*/
        }

        /// <summary>
        /// Determines an NPC's <see cref="ThreatLevel"/> based on how quickly the player and NPC would fell eachother in battle. Makes some
        /// guesses as to whether each opponent is a spell caster or not, and also a best guess at how accuracy would come in to play.
        /// </summary>
        /// <param name="playerVitals">The max vital levels of the player</param>
        /// <param name="playerStats">The player's stats</param>
        /// <param name="npcVitals">The max vital levels of the NPC</param>
        /// <param name="npcStats">The NPC's stats</param>
        /// <param name="playerMelee">The attack types the player would use in melee combat</param>
        /// <param name="npcMelee">The attack types the NPC would use in melee combat</param>
        /// <param name="playerAttackSpeed">The player's base attack speed, unaffected by boons</param>
        /// <param name="npcAttackSpeed">The NPC's base attack speed</param>
        /// <returns>The associated <see cref="ThreatLevel"/> according to the ratio thresholds set in the server config.</returns>
        public static ThreatLevel DetermineNpcThreatLevelParty(int[][] playerVitals,
            int[][] playerStats,
            int[] npcVitals,
            int[] npcStats,
            List<AttackTypes>[] playerMelee,
            List<AttackTypes> npcMelee,
            long[] playerAttackSpeed,
            long npcAttackSpeed,
            bool[] rangedAttackers,
            bool npcSpellCaster,
            int maxNpcScalar,
            int partySize)
        {
            var ratios = new double[partySize];
            for (var i = 0; i < partySize; i++)
            {
                var ratio = GetThreatLevelRatio(playerVitals[i],
                    playerStats[i],
                    npcVitals,
                    npcStats,
                    playerMelee[i],
                    npcMelee,
                    playerAttackSpeed[i],
                    npcAttackSpeed,
                    rangedAttackers[i], 
                    npcSpellCaster) - (Options.Instance.CombatOpts.ThreatLevelDeductionPerPartyMember * partySize);
                ratios[i] = Math.Max(ratio, 0d);
            }

            return GetThreatLevelFromRatio(ratios.Min());
        }

        private static ThreatLevel GetThreatLevelFromRatio(double threatRatio)
        {
            foreach (var threatVal in Options.Instance.CombatOpts.ThreatLevelThresholds.OrderByDescending(v => v.Value).ToArray())
            {
                if (threatRatio >= threatVal.Value)
                {
                    return threatVal.Key;
                }
            }

            return ThreatLevel.Trivial;
        }
    }
}
