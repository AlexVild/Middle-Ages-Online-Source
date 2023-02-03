using Intersect.Server.Database.PlayerData.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<DungeonTrackerInstance> DungeonsTracked { get; set; } = new List<DungeonTrackerInstance>();

        [NotMapped]
        public Guid DungeonId { get; set; }

        public void TrackDungeonCompletion(Guid dungeonId, int partySize, long completionTime)
        {
            var dungeonTrack = new DungeonTrackerInstance(Id, dungeonId);
            var idx = DungeonsTracked.FindIndex(dt => dt == dungeonTrack);

            if (idx == -1)
            {
                dungeonTrack.IncrementCompletion(partySize);
                dungeonTrack.TryUpdateTimeRecord(completionTime, partySize);
                DungeonsTracked.Add(dungeonTrack);
                return;
            }

            DungeonsTracked[idx].IncrementCompletion(partySize);
            DungeonsTracked[idx].TryUpdateTimeRecord(completionTime, partySize);
        }

        public void TrackDungeonFailure(Guid dungeonId)
        {
            var dungeonTrack = new DungeonTrackerInstance(Id, dungeonId);
            var idx = DungeonsTracked.FindIndex(dt => dt == dungeonTrack);

            if (idx == -1)
            {
                dungeonTrack.Failures++;
                DungeonsTracked.Add(dungeonTrack);
                return;
            }

            DungeonsTracked[idx].Failures++;
        }
    }
}
