using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Intersect.Config
{
    public class ClanWarOptions
    {
        public long HealthTickMs { get; set; } = 100;

        public long HealthTickBonusMs { get; set; } = 20;

        public long HealthTickMaximumMs { get; set; } = 200;

        public long ScoreTickMs { get; set; } = 10000;

        /// <summary>
        /// Represents for how long after leaving a territory's bounds a player is considered attacking/defending that territory
        /// </summary>
        public long TerritoryLeaveTimer { get; set; } = 5000;

        public Guid ValorTokenItemId { get; set; }

        public int BaseValorReward { get; set; } = 5;

        public int MinimumValorReward { get; set; } = 1;

        public int MinimumParticipants { get; set; } = 2;

        public int BoostPerParticipant { get; set; } = 2;

        public int MinimumScore { get; set; } = 200;

        public int BasePointsPerKill { get; set; } = 1;

        /// <summary>
        /// A janky way to populate the clan war map before territories have been created on the server side.
        /// </summary>
        public List<Guid> MapsWithTerritories { get; set; } = new List<Guid>();


        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Validate();
        }

        public void Validate()
        {
        }
    }
}
