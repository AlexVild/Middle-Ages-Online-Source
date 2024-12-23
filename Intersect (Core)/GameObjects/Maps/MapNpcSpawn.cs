using System;

using Intersect.Enums;

namespace Intersect.GameObjects.Maps
{

    public class NpcSpawn
    {

        public NpcSpawnDirection Direction;

        public Guid NpcId;

        public int X;

        public int Y;

        public int RequiredPlayersToSpawn;

        public bool PreventRespawn;

        public int SpawnGroup = 0;

        public bool CumulativeSpawning = false;

        public bool OverrideMovement = false;

        public NpcMovement OverriddenMovement;

        public bool OverrideRange = false;

        public int OverriddenRange;

        public bool WatchSpawn { get; set; }

        public int SpawnHealthWatchId { get; set; }

        public int SpawnHealthWatchThreshold { get; set; }

        public NpcSpawn()
        {
        }

        public NpcSpawn(NpcSpawn copy)
        {
            NpcId = copy.NpcId;
            X = copy.X;
            Y = copy.Y;
            Direction = copy.Direction;
            RequiredPlayersToSpawn = copy.RequiredPlayersToSpawn;
            PreventRespawn = copy.PreventRespawn;
            SpawnGroup = copy.SpawnGroup;
            CumulativeSpawning = copy.CumulativeSpawning;
            OverrideMovement = copy.OverrideMovement;
            OverriddenMovement = copy.OverriddenMovement;
            OverrideRange = copy.OverrideRange;
            OverriddenRange = copy.OverriddenRange;
            WatchSpawn = copy.WatchSpawn;
            SpawnHealthWatchId = copy.SpawnHealthWatchId;
            SpawnHealthWatchThreshold = copy.SpawnHealthWatchThreshold;
        }

    }

}
