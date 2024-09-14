using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Entities.PlayerData
{
    public enum ChallengeUpdateWatcherType
    {
        RepsAndSets,
        Reps,
        Sets,
    }

    public abstract class ChallengeUpdate
    {
        /// <summary>
        /// Determines _when_ we should go out and update the player's challenge progress. Sometimes we might care
        /// for every set, every rep, or both.
        /// </summary>
        public abstract ChallengeUpdateWatcherType WatcherType { get; }
        
        /// <summary>
        /// The challenge type associated with this update
        /// </summary>
        public virtual ChallengeType Type { get; set; }

        /// <summary>
        /// The player who's challenges are being updated
        /// </summary>
        public Player Player { get; set; }
        
        /// <summary>
        /// A list of the player's challenge progression
        /// </summary>
        List<ChallengeProgress> ChallengeProgress { get; set; }

        /// <summary>
        /// Whether or not the challenge update proc'd an update to the player's progression.
        /// </summary>
        public bool ProgressMade { get; private set; } = false;

        public ChallengeUpdate(Player player)
        {
            if (player == null)
            {
                return;
            }

            Player = player;
            ChallengeProgress = new List<ChallengeProgress>(player.ChallengesInProgress);
            foreach (var challenge in ChallengeProgress)
            {
                switch (WatcherType)
                {
                    case ChallengeUpdateWatcherType.RepsAndSets:
                        challenge.SetsChanged += Challenge_ProgressMade;
                        challenge.RepsChanged += Challenge_ProgressMade;
                        break;
                    case ChallengeUpdateWatcherType.Reps:
                        challenge.RepsChanged += Challenge_ProgressMade;
                        break;
                    case ChallengeUpdateWatcherType.Sets:
                        challenge.SetsChanged += Challenge_ProgressMade;
                        break;
                    default:
                        throw new NotImplementedException(nameof(WatcherType));
                }
            }
        }

        private void Challenge_ProgressMade(int sets, int change, int required)
        {
            ProgressMade = true;
        }

        public IEnumerable<ChallengeProgress> Challenges => ChallengeProgress.Where(c => c.Type == Type);

        public bool NoUpdate => Player == null || Challenges.ToArray().Length == 0;
    }

    public class ComboEarnedUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.ComboEarned;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public ComboEarnedUpdate(Player player) : base(player) 
        {
        }
    }

    public class DamageOverTimeUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DamageOverTime;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int DamageDone { get; set; }

        public DamageOverTimeUpdate(Player player, int damage) : base(player)
        {
            DamageDone = damage;
        }
    }

    public class MaxHitUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.MaxHit;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int LastHit { get; set; }

        public MaxHitUpdate(Player player, int damage) : base(player)
        {
            LastHit = damage;
        }
    }

    public class MissFreeUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.MissFreeStreak;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public MissFreeUpdate(Player player) : base(player)
        {
        }
    }

    public class HitFreeUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.HitFreeStreak;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int CurrentStreak { get; set; }

        public HitFreeUpdate(Player player) : base(player)
        {
        }
    }

    public class DamageTakenOverTimeUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DamageTakenOverTime;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int DamageDone { get; set; }

        public DamageTakenOverTimeUpdate(Player player, int damage) : base(player)
        {
            DamageDone = damage;
        }
    }

    public class BeastsKilledOverTime : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.BeastsKilledOverTime;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public Guid BeastId { get; set; }

        public BeastsKilledOverTime(Player player, Guid beastId) : base(player)
        {
            BeastId = beastId;
        }
    }

    public class DamageAtRangeUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DamageAtRange;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int LastHit { get; set; }
        public int Range { get; set; }

        public DamageAtRangeUpdate(Player player, int damage, int range) : base(player)
        {
            LastHit = damage;
            Range = range;
        }
    }

    public class DamageHealedAtHealthUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DamageHealedAtHealth;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int HealAmt { get; set; }
        public int Percent { get; set; }

        public DamageHealedAtHealthUpdate(Player player, int heal, int percent) : base(player)
        {
            HealAmt = heal;
            Percent = percent;
        }
    }

    public class MissFreeAtRangeUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.MissFreeAtRange;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int CurrentStreak { get; set; }
        public int Range { get; set; }

        public MissFreeAtRangeUpdate(Player player, int range) : base(player)
        {
            Range = range;
        }
    }

    public class ComboExpEarned : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.ComboExpEarned;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int ComboExp { get; set; }

        public ComboExpEarned(Player player, int comboExp) : base(player)
        {
            ComboExp = comboExp;
        }
    }

    public class AoEHitsUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.AoEHits;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int EnemiesHit { get; set; }

        public AoEHitsUpdate(Player player, int enemiesHit) : base(player)
        {
            EnemiesHit = enemiesHit;
        }
    }

    public class BackstabDamageUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.BackstabDamage;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int TotalBackstabDamage { get; set; }

        public BackstabDamageUpdate(Player player, int totalBackstabDamage) : base(player)
        {
            TotalBackstabDamage = totalBackstabDamage;
        }
    }

    public class CriticalHitsUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.CriticalHits;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int TotalCrits { get; set; }

        public CriticalHitsUpdate(Player player, int totalCrits) : base(player)
        {
            TotalCrits = totalCrits;
        }
    }

    public class StatusApplyUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.StatusApply;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public StatusTypes Status { get; set; }

        public StatusApplyUpdate(Player player, StatusTypes status) : base(player)
        {
            Status = status;
        }
    }

    public class PierceManyUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.PierceMany;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.RepsAndSets;

        public int EnemiesPierced { get; set; }

        public PierceManyUpdate(Player player, int enemiesPierced) : base(player)
        {
            EnemiesPierced = enemiesPierced;
        }
    }

    public class DoTUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DoTDamage;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int DoTDamage { get; set; }

        public DoTUpdate(Player player, int doTDamage) : base(player)
        {
            DoTDamage = doTDamage;
        }
    }

    public class DamageDealtUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DealDamage;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int Damage { get; set; }

        public DamageDealtUpdate(Player player, int damage) : base(player)
        {
            Damage = damage;
        }
    }

    public class ManaDamageDealtUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.DealManaDamage;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int Damage { get; set; }

        public ManaDamageDealtUpdate(Player player, int damage) : base(player)
        {
            Damage = damage;
        }
    }

    public static class ChallengeUpdateProcesser
    {
        public static void UpdateChallengesOf(ChallengeUpdate update)
        {
            if (update?.NoUpdate ?? true)
            {
                return;
            }
            var player = update.Player;

            UpdateChallenge((dynamic)update);

            if (!update.ProgressMade)
            {
                return;
            }

            if (!player.TryGetEquippedItem(Options.WeaponIndex, out var equipped))
            {
                return;
            }
            foreach (var weaponType in equipped.Descriptor?.WeaponTypes ?? new List<Guid>())
            {
                player.TryProgressMastery(0, weaponType);
            }
        }

        public static void UpdateChallengesOf(ChallengeUpdate update, int enemyTier)
        {
            if (update?.NoUpdate ?? true)
            {
                return;
            }
            var player = update.Player;

            UpdateChallenge((dynamic)update, enemyTier);

            if (!update.ProgressMade)
            {
                return;
            }

            if (!player.TryGetEquippedItem(Options.WeaponIndex, out var equipped))
            {
                return;
            }
            foreach (var weaponType in equipped.Descriptor?.WeaponTypes ?? new List<Guid>())
            {
                player.TryProgressMastery(0, weaponType);
            }
        }

        private static void UpdateChallenge(ComboEarnedUpdate update, int enemyTier)
        {
            foreach (var challenge in update.Challenges)
            {
                if (challenge.Descriptor.MinTier > enemyTier)
                {
                    continue;
                }

                challenge.Streak++;
                if (challenge.Streak > 0 && challenge.Streak % challenge.Descriptor.Reps == 0)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(DamageOverTimeUpdate update, int enemyTier)
        {
            var player = update.Player;

            // First, prune stale data
            var now = Timing.Global.MillisecondsUtc;
            foreach (var map in player.DoTChallengeMap.Values)
            {
                while (map.Count > 0 && map.Peek().Key < now)
                {
                    map.Dequeue();
                }
            }

            // Now, go through the player's active challenges...
            foreach (var challenge in update.Challenges)
            {
                if (challenge.Descriptor.MinTier > enemyTier)
                {
                    continue;
                }

                // Then, add our new damage with its challenge-appropriate time stamp
                var expiryTime = now + challenge.Descriptor.Param;
                var data = new KeyValuePair<long, int>(expiryTime, update.DamageDone);

                // .. add our latest damage to the player's in-memory DoT tracker...
                if (!player.DoTChallengeMap.TryGetValue(challenge.ChallengeId, out var challengeDoT))
                {
                    player.DoTChallengeMap[challenge.ChallengeId] = new Queue<KeyValuePair<long, int>>();
                    challengeDoT = player.DoTChallengeMap[challenge.ChallengeId];
                }
                challengeDoT.Enqueue(data);

                // And add up the DoTs for each challenge. If they meet the challenges desires...
                if (challengeDoT.Sum(dot => dot.Value) >= challenge.Descriptor.Reps)
                {
                    // Then that's a set!
                    challenge.Sets++;
                    challengeDoT.Clear();
                }
            }
        }

        private static void UpdateChallenge(MaxHitUpdate update)
        {
            foreach (var challenge in update.Challenges)
            {
                if (challenge.Descriptor.Reps <= update.LastHit)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(MissFreeUpdate update, int enemyTier)
        {
            foreach (var challenge in update.Challenges)
            {
                if (challenge.Descriptor.MinTier > enemyTier)
                {
                    continue;
                }

                challenge.Streak++;
                if (challenge.Streak > 0 && challenge.Streak % challenge.Descriptor.Reps == 0)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(HitFreeUpdate update, int enemyTier)
        {
            foreach (var challenge in update.Challenges)
            {
                if (challenge.Descriptor.MinTier > enemyTier)
                {
                    continue;
                }

                challenge.Streak++;
                if (challenge.Streak > 0 && challenge.Streak % challenge.Descriptor.Reps == 0)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(DamageTakenOverTimeUpdate update)
        {
            var player = update.Player;

            var now = Timing.Global.MillisecondsUtc;
            foreach (var map in player.DamageTakenMap.Values)
            {
                while (map.Count > 0 && map.Peek().Key < now)
                {
                    map.Dequeue();
                }
            }

            foreach (var challenge in update.Challenges)
            {
                var expiryTime = now + challenge.Descriptor.Param;
                var data = new KeyValuePair<long, int>(expiryTime, update.DamageDone);

                if (!player.DamageTakenMap.TryGetValue(challenge.ChallengeId, out var damageTaken))
                {
                    player.DamageTakenMap[challenge.ChallengeId] = new Queue<KeyValuePair<long, int>>();
                    damageTaken = player.DamageTakenMap[challenge.ChallengeId];
                }
                damageTaken.Enqueue(data);

                if (damageTaken.Sum(dot => dot.Value) >= challenge.Descriptor.Reps)
                {
                    challenge.Sets++;
                    damageTaken.Clear();
                }
            }
        }

        private static void UpdateChallenge(BeastsKilledOverTime update, int enemyTier)
        {
            var player = update.Player;

            var now = Timing.Global.MillisecondsUtc;
            foreach (var map in player.BeastsKilledOverTime.Values)
            {
                while (map.Count > 0 && map.Peek().Key < now)
                {
                    map.Dequeue();
                }
            }

            foreach (var challenge in update.Challenges)
            {
                if (challenge.Descriptor.MinTier > enemyTier)
                {
                    continue;
                }

                var expiryTime = now + challenge.Descriptor.Param;
                var data = new KeyValuePair<long, Guid>(expiryTime, update.BeastId);

                if (!player.BeastsKilledOverTime.TryGetValue(challenge.ChallengeId, out var beastsKilled))
                {
                    player.BeastsKilledOverTime[challenge.ChallengeId] = new Queue<KeyValuePair<long, Guid>>();
                    beastsKilled = player.BeastsKilledOverTime[challenge.ChallengeId];
                }
                beastsKilled.Enqueue(data);

                if (beastsKilled.Count >= challenge.Descriptor.Reps)
                {
                    challenge.Sets++;
                    beastsKilled.Clear();
                }
            }
        }

        private static void UpdateChallenge(DamageAtRangeUpdate update)
        {
            foreach (var challenge in update.Challenges)
            {
                var descriptor = challenge.Descriptor;
                if (descriptor.Reps <= update.LastHit && descriptor.Param <= update.Range)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(DamageHealedAtHealthUpdate update)
        {
            foreach (var challenge in update.Challenges)
            {
                var descriptor = challenge.Descriptor;
                if (descriptor.Reps <= (update.HealAmt * -1) && descriptor.Param >= update.Percent)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(MissFreeAtRangeUpdate update, int enemyTier)
        {
            foreach (var challenge in update.Challenges)
            {
                var desc = challenge.Descriptor;
                if (desc.MinTier > enemyTier || update.Range < desc.Param)
                {
                    continue;
                }

                challenge.Streak++;
                if (challenge.Streak > 0 &&
                    challenge.Streak % desc.Reps == 0
                    && update.Range >= desc.Param)
                {
                    challenge.Sets++;
                }
            }
        }

        private static void UpdateChallenge(ComboExpEarned update)
        {
            foreach (var challenge in update.Challenges)
            {
                var desc = challenge.Descriptor;
                var timesComplete = (int)Math.Floor((float)update.ComboExp / desc.Reps);
                challenge.Sets += timesComplete;
            }
        }

        private static void UpdateChallenge(AoEHitsUpdate update)
        {
            foreach (var challenge in update.Challenges)
            {
                var desc = challenge.Descriptor;
                if (update.EnemiesHit > 0 && update.EnemiesHit >= desc.Reps)
                {
                    var timesComplete = (int)Math.Floor((float)update.EnemiesHit / desc.Reps);
                    challenge.Sets += timesComplete;
                }
            }
        }

        private static void UpdateChallenge(BackstabDamageUpdate update, int enemyTier)
        {
            foreach (var challenge in update.Challenges)
            {
                var desc = challenge.Descriptor;
                if (desc == null || enemyTier < challenge.Descriptor.MinTier)
                {
                    continue;
                }

                challenge.Sets += update.TotalBackstabDamage;
            }
        }

        private static void UpdateChallenge(CriticalHitsUpdate update)
        {
            CommonChallengeHandler(update, int.MaxValue, (challenge) =>
            {
                challenge.Sets += update.TotalCrits;
            });
        }

        private static void UpdateChallenge(StatusApplyUpdate update)
        {
            CommonChallengeHandler(update, int.MaxValue, (challenge) =>
            {
                var desc = challenge.Descriptor;
                if (desc.Param == (int)update.Status)
                {
                    challenge.Sets++;
                }
            });
        }

        private static void UpdateChallenge(PierceManyUpdate update)
        {
            CommonChallengeHandler(update, int.MaxValue, (challenge) =>
            {
                var desc = challenge.Descriptor;
                if (update.EnemiesPierced > 0 && update.EnemiesPierced >= desc.Reps)
                {
                    var timesComplete = (int)Math.Floor((float)update.EnemiesPierced / desc.Reps);
                    challenge.Sets += timesComplete;
                }
            });
        }

        private static void UpdateChallenge(DoTUpdate update)
        {
            CommonChallengeHandler(update, int.MaxValue, (challenge) =>
            {
                challenge.Sets += update.DoTDamage;
            });
        }

        private static void UpdateChallenge(DamageDealtUpdate update)
        {
            CommonChallengeHandler(update, int.MaxValue, (challenge) =>
            {
                challenge.Sets += update.Damage;
            });
        }

        private static void UpdateChallenge(ManaDamageDealtUpdate update)
        {
            CommonChallengeHandler(update, int.MaxValue, (challenge) =>
            {
                challenge.Sets += update.Damage;
            });
        }

        /// <summary>
        /// Does standard challenge validations and calls a callback func if they pass
        /// </summary>
        /// <param name="update">The challenge update to check for updates</param>
        /// <param name="enemyTier">The enemy tier</param>
        /// <param name="handler">The handler to call if validations succeed</param>
        private static void CommonChallengeHandler(ChallengeUpdate update, int enemyTier, Action<ChallengeProgress> handler)
        {
            foreach (var challenge in update.Challenges)
            {
                var desc = challenge.Descriptor;
                if (desc == null || enemyTier < challenge.Descriptor.MinTier)
                {
                    continue;
                }

                handler.Invoke(challenge);
            }
        }
    }
}
