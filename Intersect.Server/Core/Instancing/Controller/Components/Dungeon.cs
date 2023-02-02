using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Instancing.Controller.Components
{
    public class Dungeon
    {
        public Dungeon(Guid dungeonId)
        {
            DescriptorId = dungeonId;
        }

        public Guid DescriptorId { get; set; }

        public int GnomeLocation { get; set; }

        public bool GnomeObtained { get; set; }

        public DungeonState State { get; set; } = DungeonState.Null;

        public List<Player> Participants { get; set; } = new List<Player>();

        public bool IsSolo => Participants.Count <= 0;

        public int TreasureLevel = 0;
    }
}
