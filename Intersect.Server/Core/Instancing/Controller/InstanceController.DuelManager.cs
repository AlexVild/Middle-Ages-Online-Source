using Intersect.Server.Core.Instancing.Controller.Components;
using Intersect.Server.Entities;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        public const int MinimumParticipants = 3;

        public const int MatchmakeRetryCount = 2;

        public const long DuelCooldown = 10000;

        public Queue<Duel> MatchQueue { get; set; } = new Queue<Duel>();

        public List<Player> DuelPool { get; set; } = new List<Player>();

        public bool DuelIsActive { get; set; }

        public long NextDuelTimestamp { get; set; }

        public void JoinDuelPool(Player player)
        {
            if (DuelPool.Contains(player))
            {
                return;
            }
            DuelPool.Add(player);
        }

        public void WithdrawDuelPool(Player player)
        {
            DuelPool.Remove(player);
        }

        public void ProcessOpenMelee()
        {
            if (MatchQueue.Count == 0)
            {
                MatchMake();
            }
            else
            {
                MatchUpdate();
            }
        }

        public void MatchMake()
        {
            // Prune duel pool
            DuelPool = DuelPool.Where(pl => pl.CanDuel).ToList();

            // Not enough people to do random dueling
            if (DuelPool.Count < MinimumParticipants)
            {
                return;
            }

            // Not enough time since the last duel
            if (Timing.Global.MillisecondsUtc < NextDuelTimestamp)
            {
                return;
            }

            var contestent1 = DuelPool.ElementAtOrDefault(0);
            var contestent2 = DuelPool.ElementAtOrDefault(Randomization.Next(1, DuelPool.Count));

            var newDuel = new Duel(new List<Player> { contestent1, contestent2 });
            MatchQueue.Enqueue(newDuel);
        }

        private void MatchUpdate()
        {
            var currentMatch = MatchQueue.Peek();
            if (currentMatch == default)
            {
                return;
            }

            if (currentMatch.Status == DuelStatus.Finished)
            {
                // The match has ended naturally, clean up and prepare for the next match after cooldown
                NextDuelTimestamp = currentMatch.MatchEndedTimestamp + DuelCooldown;
                MatchQueue.Dequeue();
            }
            else if (currentMatch.Duelers.Count < 2)
            {
                // Warp out remaining duelers and mark match as finished
                currentMatch.Forfeit();
            }
            else if (currentMatch.Status == DuelStatus.Paused)
            {
                // The match has warped its combatants, start!
                currentMatch.Start();
            }
        }

        public void LeaveDuelPool(Player player)
        {
            DuelPool.Remove(player);
        }
    }
}
