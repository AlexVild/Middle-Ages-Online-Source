using Intersect.Server.Entities;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Instancing.Controller.Components
{
    public enum DuelStatus
    {
        Paused,
        Ongoing,
        Finished
    }

    public class Duel
    {
        public List<Player> Duelers { get; set; } = new List<Player>();

        public long MatchEndedTimestamp { get; set; }

        public DuelStatus Status { get; set; }

        public Duel(List<Player> duelers)
        {
            Duelers = duelers;
        }

        public void Start()
        {
            var i = 0;
            foreach (var dueler in Duelers.ToArray())
            {
                dueler.EnterDuel(this, i);
                i++;
            }
            Status = DuelStatus.Ongoing;
        }

        /// <summary>
        /// This method exists because we don't want to warp the loser - they will be at the respawn menu and will respawn themselves. We want to warp OTHER combatants at match-end, though
        /// </summary>
        /// <param name="loser">The player who lost the duel</param>
        public void Lost(Player loser)
        {
            foreach (var dueler in Duelers.ToArray())
            {
                Leave(dueler, dueler.Id != (loser?.Id ?? Guid.Empty) && !dueler.PlayerDead);
            }
            End();
        }

        /// <summary>
        /// If a match is forfeit, either due to time constraints or due to a disconnect, this method will kick everyone out and end their duels
        /// </summary>
        public void Forfeit()
        {
            foreach (var dueler in Duelers.ToArray())
            {
                Leave(dueler, !dueler.PlayerDead);
            }
            End();
        }

        public void End()
        {
            Status = DuelStatus.Finished;
            MatchEndedTimestamp = Timing.Global.MillisecondsUtc;
        }

        public void Leave(Player player, bool warp)
        {
            if (player == null)
            {
                return;
            }

            Duelers.Remove(player);

            player.LeaveDuel(warp);
        }
    }
}
