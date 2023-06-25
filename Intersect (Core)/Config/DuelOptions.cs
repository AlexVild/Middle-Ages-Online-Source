using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public class DuelOptions
    {
        public int OpenMeleeMinParticipants { get; set; } = 3;

        public long MatchmakingCooldown { get; set; } = 15000;

        public long PlayerDuelCooldown { get; set; } = 60000;

        public long MatchMakeEveryMs { get; set; } = 5000;
    }
}
