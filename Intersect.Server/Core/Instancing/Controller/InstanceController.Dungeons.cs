using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.GameObjects;
using Intersect.Server.Maps;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Core.Instancing.Controller.Components;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        Dungeon Dungeon { get; set; } = new Dungeon(Guid.Empty);

        public bool TryInitializeOrJoinDungeon(Guid dungeonId, Player player)
        {
            StartDungeon(dungeonId);
            return TryAddPlayerToDungeon(player);
        }

        public void StartDungeon(Guid dungeonId)
        {
            if (Dungeon.State != DungeonState.Null)
            {
                return;
            }
            Dungeon = new Dungeon(dungeonId);
            Dungeon.State = DungeonState.Inactive;
        }

        public bool TryAddPlayerToDungeon(Player player)
        {
            if (Dungeon?.State == DungeonState.Null || Dungeon?.State == DungeonState.Complete)
            {
                return false;
            }

            Dungeon.Participants.Add(player);
            return true;
        }

        public void CompleteDungeon()
        {
            if (Dungeon?.State == DungeonState.Null)
            {
                return;
            }

            Dungeon.State = DungeonState.Complete;
        }

        public DungeonState CurrentDungeonState => Dungeon?.State ?? DungeonState.Null;
    }
}
