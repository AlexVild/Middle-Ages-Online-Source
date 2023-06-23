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

        public readonly string DuelMapId = "f79bfde0-5909-45f0-8e78-7510ddc5f6a1";
        public readonly string DuelEndMapId = "f79bfde0-5909-45f0-8e78-7510ddc5f6a1";

        public readonly int DuelMapX1 = 27;
        public readonly int DuelMapX2 = 27;
        public readonly int DuelMapY1 = 3;
        public readonly int DuelMapY2 = 7;
        public readonly int DuelEndX = 24;
        public readonly int DuelEndY = 3;

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

        public void Lost(Player loser)
        {
            foreach (var dueler in Duelers.ToArray())
            {
                Leave(dueler, dueler.Id != (loser?.Id ?? Guid.Empty) && !dueler.PlayerDead);
            }
            End();
        }

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
            
            if (warp)
            {
                player.WarpToDuelEnd();
            }
        }
    }
}
