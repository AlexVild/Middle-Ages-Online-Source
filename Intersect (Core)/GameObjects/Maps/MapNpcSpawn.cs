using System;

using Intersect.Enums;
using Intersect.GameObjects.Events;

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

        public bool ConditionalSpawning;

        public VariableTypes SpawnVariableType { get; set; } = VariableTypes.ServerVariable;
            
        public Guid SpawnVariableId { get; set; }

        public VariableCompaison SpawnVariableComparison { get; set; } = new VariableCompaison();

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
            ConditionalSpawning = copy.ConditionalSpawning;
            SpawnVariableType = copy.SpawnVariableType;
            SpawnVariableId = copy.SpawnVariableId;
            SpawnVariableComparison = copy.SpawnVariableComparison;
        }

    }

}
