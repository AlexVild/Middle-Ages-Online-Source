using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class DungeonTrackerInstance : IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Player Player { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        public Guid DungeonId { get; set; }

        public DungeonDescriptor Dungeon => DungeonDescriptor.Get(DungeonId);

        public int SoloCompletions { get; set; } = 0;

        public int GroupCompletions { get; set; } = 0;

        public int Failures { get; set; } = 0;

        [NotMapped, JsonIgnore]
        public int TotalCompletions => SoloCompletions + GroupCompletions;

        public long BestGroupTime { get; set; } = -1L;
        
        public long BestSoloTime { get; set; } = -1L;

        public DungeonTrackerInstance(Guid playerId, Guid dungeonId)
        {
            PlayerId = playerId;
            DungeonId = dungeonId;
        }

        public override bool Equals(object obj)
        {
            var other = obj as DungeonTrackerInstance;
            if (other == null)
            {
                return false;
            }
            return PlayerId == other.PlayerId && DungeonId == other.DungeonId;
        }

        public void IncrementCompletion(int partySize)
        {
            if (partySize > 1)
            {
                GroupCompletions = 1;
            }
            else
            {
                SoloCompletions = 1;
            }
        }

        public bool TryUpdateTimeRecord(long completionTime, int partySize)
        {
            if (partySize > 1)
            {
                if (BestGroupTime < 0)
                {
                    BestGroupTime = completionTime;
                    return true;
                }

                if (completionTime >= BestGroupTime || completionTime == 0)
                {
                    return false;
                }

                BestGroupTime = completionTime;

                return true;
            }
            else
            {
                if (BestSoloTime < 0)
                {
                    BestSoloTime = completionTime;
                    return true;
                }

                if (completionTime >= BestSoloTime || completionTime == 0)
                {
                    return false;
                }

                BestSoloTime = completionTime;

                return true;
            }
        }

        public DungeonTrackerInstance()
        {
        }
    }
}
