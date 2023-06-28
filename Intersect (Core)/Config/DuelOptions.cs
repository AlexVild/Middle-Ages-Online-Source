using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Config
{
    public class DuelOptions
    {
        public int OpenMeleeMinParticipants { get; set; } = 2;
        
        public int OpenMeleeMinMedalParticipants { get; set; } = 3;

        public long MatchmakingCooldown { get; set; } = 15000;
        
        public long MatchmakingMinCooldown { get; set; } = 5000;

        public long PlayerDuelCooldown { get; set; } = 60000;

        public long MatchMakeEveryMs { get; set; } = 5000;

        public string MeleeMedalId { get; set; } = "9e9d4a60-f30b-41eb-8f57-bb9ebb88950d";
        
        public string MeleeHealItemId { get; set; } = "8a7a2f42-8bb7-4b2d-b9f0-29035cf3d69f";
        
        public string MeleeManaItemId { get; set; } = "f7c483a8-3ff4-4dac-a4ff-9ec52e89f7b9";
        
        public string WinAnimId { get; set; } = "c2a1dde4-d068-4f5c-8ef8-4f03afaa658c";
        
        public string EntranceAnimId{ get; set; } = "5fe83f3e-6f28-4ae9-9da2-c39405875490";
        
        public string MeleeMusic { get; set; } = "mini boss.ogg";

        public string DayOfWeekServerVarId { get; set; } = "4e78f9eb-4ead-4163-adf9-a07643da8091";

        public int DoubleMedalDayOfWeek { get; set; } = 1;

        public long MatchCooldownModifier { get; set; } = 3000;
    }
}
