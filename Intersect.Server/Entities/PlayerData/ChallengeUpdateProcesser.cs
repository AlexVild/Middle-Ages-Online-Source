using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract ChallengeUpdateWatcherType WatcherType { get; }
        
        public virtual ChallengeType Type { get; set; }
        public Player Player { get; set; }
        List<ChallengeProgress> ChallengeProgress { get; set; }
        public bool ProgressMade = false;

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

        private void Challenge_ProgressMade(int sets, int required)
        {
            ProgressMade = true;
        }

        public IEnumerable<ChallengeProgress> Challenges => ChallengeProgress.Where(c => c.Type == Type);
    }

    public class ComboEarnedUpdate : ChallengeUpdate
    {
        public override ChallengeType Type { get; set; } = ChallengeType.ComboEarned;
        public override ChallengeUpdateWatcherType WatcherType => ChallengeUpdateWatcherType.Sets;

        public int ComboAmt { get; set; }

        public ComboEarnedUpdate(Player player, int combo) : base(player) 
        {
            ComboAmt = combo;
        }
    }

    public static class ChallengeUpdateProcesser
    {
        public static void UpdateChallenges(ChallengeUpdate update)
        {
            var player = update?.Player;
            if (update == null || player == null)
            {
                return;
            }

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
                player.ProgressMastery(0, weaponType);
            }
        }

        public static void UpdateChallenge(ComboEarnedUpdate update)
        {
            if (update == null)
            {
                return;
            }

            foreach (var challenge in update.Challenges)
            {
                if (update.ComboAmt > 0 && update.ComboAmt % challenge.Descriptor.Reps == 0)
                {
                    challenge.Sets++;
                }
            }
        }
    }
}
