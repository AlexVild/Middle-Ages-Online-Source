using Intersect.GameObjects;
using Intersect.Server.Core;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Networking;
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
            var dungeonName = DungeonDescriptor.GetName(dungeonId);
            var idx = DungeonsTracked.FindIndex(dt => dt.DungeonId == dungeonTrack.DungeonId);

            var completions = 1;
            if (idx == -1)
            {
                dungeonTrack.IncrementCompletion(partySize, this);
                if (completionTime > 0)
                {
                    dungeonTrack.TryUpdateTimeRecord(completionTime, partySize, this);
                }
                DungeonsTracked.Add(dungeonTrack);
            }
            else
            {
                DungeonsTracked[idx].IncrementCompletion(partySize, this);
                if (completionTime > 0)
                {
                    DungeonsTracked[idx].TryUpdateTimeRecord(completionTime, partySize, this);
                }
            }
        }

        public void TrackDungeonFailure(Guid dungeonId)
        {
            var dungeonTrack = new DungeonTrackerInstance(Id, dungeonId);
            var idx = DungeonsTracked.FindIndex(dt => dt.DungeonId == dungeonTrack.DungeonId);

            if (idx == -1)
            {
                dungeonTrack.Failures++;
                DungeonsTracked.Add(dungeonTrack);
            }
            else
            {
                DungeonsTracked[idx].Failures++;
            }
        }

        public void VoidCurrentDungeon()
        {
            if (InstanceProcessor.TryGetInstanceController(MapInstanceId, out var controller) 
                && controller.Dungeon != null 
                && controller.Dungeon.State == GameObjects.Events.DungeonState.Active)
            {
                controller.Dungeon.RecordsVoid = true;
            }
        }
    }
}
