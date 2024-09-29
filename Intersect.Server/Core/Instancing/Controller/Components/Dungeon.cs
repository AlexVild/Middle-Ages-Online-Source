using Intersect.GameObjects;
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
    public sealed class Dungeon
    {
        public Dungeon(Guid dungeonId)
        {
            DescriptorId = dungeonId;
            State = DungeonState.Inactive;
        }

        public Guid DescriptorId { get; set; }

        public DungeonDescriptor Descriptor => DungeonDescriptor.Get(DescriptorId);

        public int GnomeLocation { get; set; }

        public bool GnomeObtained { get; set; }

        public DungeonState State { get; set; }

        public List<Player> Participants { get; set; } = new List<Player>();

        public bool IsSolo { get; set; }

        public long CompletionTime { get; set; } = 0L;

        public bool RecordsVoid { get; set; }

        public string CompletionTimeString => CompletionTime <= 0 ?
            string.Empty : 
            TextUtils.GetTimeElapsedString(CompletionTime, Strings.Events.ElapsedMinutes, Strings.Events.ElapsedHours, Strings.Events.ElapsedDays);
    }
}
