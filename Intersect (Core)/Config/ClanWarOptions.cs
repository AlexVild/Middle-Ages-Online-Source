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
