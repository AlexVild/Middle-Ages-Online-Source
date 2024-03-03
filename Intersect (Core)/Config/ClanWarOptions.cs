using System.Runtime.Serialization;

namespace Intersect.Config
{
    public class ClanWarOptions
    {
        public long HealthTickMs { get; set; } = 100;

        public long HealthTickBonusMs { get; set; } = 20;

        public long HealthTickMaximumMs { get; set; } = 200;

        public long ScoreTickMs { get; set; } = 10000;


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
