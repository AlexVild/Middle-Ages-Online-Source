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

        public string MeleeMedalId { get; set; } = "9e9d4a60-f30b-41eb-8f57-bb9ebb88950d";
        
        public string MeleeHealItemId { get; set; } = "8a7a2f42-8bb7-4b2d-b9f0-29035cf3d69f";
        
        public string MeleeManaItemId { get; set; } = "f7c483a8-3ff4-4dac-a4ff-9ec52e89f7b9";
    }
}
