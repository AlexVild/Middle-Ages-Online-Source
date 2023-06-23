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

        public const long InstanceDuelCooldown = 15000;

        public const long PlayerDuelCooldown = 60000;

        private const long MatchMakeEveryMs = 5000;
        private long LastMatchMakeAttemptTimestamp = 0L;

        public Queue<Duel> MatchQueue { get; set; } = new Queue<Duel>();

        public DuelQueue DuelPool { get; set; } = new DuelQueue();

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
            // Poll for new matchmake attempt every X seconds
            var now = Timing.Global.MillisecondsUtc;
            if (now < LastMatchMakeAttemptTimestamp)
            {
                return;
            }
            LastMatchMakeAttemptTimestamp = now + MatchMakeEveryMs;

            // Prune duel pool
            var duelPool = DuelPool.Where(pl => pl.CanDuel).ToList();

            // Not enough people to do random dueling
            if (duelPool.Count < MinimumParticipants)
            {
                return;
            }

            // Not enough time since the last duel
            if (Timing.Global.MillisecondsUtc < NextDuelTimestamp)
            {
                return;
            }

            // first, attempt to get a list of players in the pool who have NOT recently dueled
            var pool = duelPool.Where(duelist => duelist.LastDuelTimestamp + PlayerDuelCooldown < now).ToArray();
            if (pool.Length < MinimumParticipants)
            {
                // If that failed, then just include all potential fighters
                pool = duelPool.ToArray();
            }

            // Prioritize the front of the pool - they've effectively been waiting the longest
            var contestent1 = duelPool.FirstOrDefault();
            if (contestent1 == default)
            {
                // Something went wrong, abort and try again next cycle
                return;
            }

            var contestent2 = duelPool.ElementAtOrDefault(Randomization.Next(1, DuelPool.Count));
            if (contestent2 == default)
            {
                // Something went wrong, abort and try again next cycle
                return;
            }

            // We have a match! Create it and put it in the queue so that we can watch it
            var newDuel = new Duel(new List<Player> { contestent1, contestent2 });

            MatchQueue.Enqueue(newDuel);

            // Move the two combatants to the back of the list to make room for other players to be priority
            DuelPool.SendToBack(contestent1, contestent2);
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
                IncrementCooldown(currentMatch.MatchEndedTimestamp);
                MatchQueue.Dequeue();
            }
            else if (currentMatch.Duelers.Count < 2)
            {
                // Warp out remaining duelers and mark match as finished
                currentMatch.Forfeit();
            }
            else if (currentMatch.Status == DuelStatus.Paused)
            {
                // The match has been created, let's warp the combatants and get going!
                currentMatch.Start();
            }
        }

        public void IncrementCooldown(long now)
        {
            NextDuelTimestamp = now + InstanceDuelCooldown;
        }

        public void LeaveDuelPool(Player player)
        {
            DuelPool.Remove(player);
        }
    }
}
