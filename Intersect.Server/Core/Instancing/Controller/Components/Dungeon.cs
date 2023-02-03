using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Instancing.Controller.Components
{
    sealed class Dungeon
    {
        public Dungeon(Guid dungeonId)
        {
            DescriptorId = dungeonId;
            State = DungeonState.Null;
        }

        public Guid DescriptorId { get; set; }

        public int GnomeLocation { get; set; }

        public bool GnomeObtained { get; set; }

        public DungeonState State { get; set; }

        public List<Player> Participants { get; set; } = new List<Player>();

        public bool IsSolo => Participants.Count == 1 && (Participants[0].Party == null || Participants[0].Party.Count < 2);

        public int TreasureLevel = 0;

        public long CompletionTime { get; set; }

        public string CompletionTimeString => CompletionTime <= 0 ?
            string.Empty : 
            TextUtils.GetTimeElapsedString(CompletionTime, Strings.Events.ElapsedMinutes, Strings.Events.ElapsedHours, Strings.Events.ElapsedDays);
    }
}
