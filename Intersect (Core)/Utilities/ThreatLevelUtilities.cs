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
            long npcAttackSpeed)
        {
            var maxEnemyHp = npcVitals[(int)Vitals.Health];
            var maxHp = playerVitals[(int)Vitals.Health];

            // This will make a guess at whether or not the NPC or player is a spell caster and override their attack types if so (since we don't
            // wan't to use their melee attack types in those cases)
            var playerAttackTypes = CombatUtilities.EstimateEntityAttackTypes(playerStats, playerMelee);
            var npcAttackTypes = CombatUtilities.EstimateEntityAttackTypes(npcStats, npcMelee);

            // If we've determined the player to be a spell caster, use global cool down instead of attack time
            if (playerAttackTypes.Count == 1 && playerAttackTypes[0] == AttackTypes.Magic)
            {
                playerAttackSpeed = Options.Combat.GlobalCooldownDuration;
            }

            CombatUtilities.CalculateDamage(playerAttackTypes, 1, 100, playerStats, npcStats, out var playerMax);
            CombatUtilities.CalculateDamage(npcAttackTypes, 1, 100, npcStats, playerStats, out var npcMax);

            var attacksToKill = Math.Ceiling((float)maxEnemyHp / playerMax);
            var attacksToDie = Math.Ceiling((float)maxHp / npcMax);

            // Make a best guess at how missed attacks might come in to play by elongating the amount of total attacks needed for death based on miss percentages
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
            var ratio = timeToKill / timeToDie;

            // Find the first threat level that is an appropriate match for our ratio
            foreach (var threatVal in Options.Instance.CombatOpts.ThreatLevelThresholds.OrderByDescending(v => v.Value).ToArray())
            {
                if (ratio >= threatVal.Value)
                {
                    return threatVal.Key;
                }
            }

            return ThreatLevel.Trivial;
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
            long npcAttackSpeed)
        {
            var partyHpPool = 0;
            var totalMembers = playerStats.Length;

            foreach(var memberVital in playerVitals)
            {
                partyHpPool += memberVital[(int)Vitals.Health];
            }
            var maxEnemyHp = npcVitals[(int)Vitals.Health];

            var npcAttackTypes = CombatUtilities.EstimateEntityAttackTypes(npcStats, npcMelee);

            var totalDamage = 0;
            var totalAttackSpeeds = 0L;
            var totalNpcDamage = 0;
            var totalMissChance = 0D;
            var totalNpcMissChance = 0D;

            // Calculate total values for every member of the party to use for average calculations later
            for (var i = 0; i < totalMembers; i++)
            {
                var memberAttackSpeed = playerAttackSpeed[i];
                var memberStats = playerStats[i];
                var memberMelee = CombatUtilities.EstimateEntityAttackTypes(memberStats, playerMelee[i]);
                if (memberMelee.Count == 1 && memberMelee[0] == AttackTypes.Magic)
                {
                    memberAttackSpeed = Options.Combat.GlobalCooldownDuration;
                }

                totalAttackSpeeds += memberAttackSpeed;
                CombatUtilities.CalculateDamage(memberMelee, 1, 100, memberStats, npcStats, out var memberMax);
                CombatUtilities.CalculateDamage(npcAttackTypes, 1, 100, npcStats, memberStats, out var npcMax);
                // Make a best guess at how missed attacks might come in to play by elongating the amount of total attacks needed for death based on miss percentages
                totalMissChance += CombatUtilities.MissChance(CombatUtilities.CalculateMissFactor(memberStats[(int)Stats.Accuracy], npcStats[(int)Stats.Evasion]));
                totalNpcMissChance += CombatUtilities.MissChance(CombatUtilities.CalculateMissFactor(npcStats[(int)Stats.Accuracy], memberStats[(int)Stats.Evasion]));
                totalDamage += memberMax;
                totalNpcDamage += npcMax;
            }
            
            // Create averages to use for remainder of calculation
            var partyAttackSpeed = (long)Math.Floor((float)totalAttackSpeeds / totalMembers);
            var partyDamage = (int)Math.Floor((float)totalDamage / totalMembers);
            var partyMissChance = totalMissChance / totalMembers;

            var npcDamage = (int)Math.Floor((float)totalNpcDamage / totalMembers);
            var npcMissChance = totalNpcMissChance / totalMembers;

            var attacksToKill = Math.Ceiling((float)maxEnemyHp / partyDamage);
            var attacksToDie = Math.Ceiling((float)partyHpPool / npcDamage);

            var attacksMissed = Math.Floor(attacksToKill * partyMissChance);
            var attacksDodged = Math.Floor(attacksToDie * npcMissChance);

            var timeToKill = (attacksToKill + attacksMissed) * partyAttackSpeed;
            var timeToDie = (attacksToDie + attacksDodged) * npcAttackSpeed;

            var ratio = timeToKill / timeToDie;

            // Find the first threat level that is an appropriate match for our ratio
            foreach (var threatVal in Options.Instance.CombatOpts.ThreatLevelPartyThresholds.OrderByDescending(v => v.Value).ToArray())
            {
                if (ratio >= threatVal.Value)
                {
                    return threatVal.Key;
                }
            }

            return ThreatLevel.Trivial;
        }
    }
}
