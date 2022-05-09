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

        public enum SpawnComparisonType
        {
            Boolean,
            Integer,
            String
        }

        public SpawnComparisonType SpawnConditionType { get; set; } = SpawnComparisonType.Boolean;

        public BooleanVariableComparison BooleanSpawnComparison { get; set; } = new BooleanVariableComparison();
        public IntegerVariableComparison IntegerSpawnComparison { get; set; } = new IntegerVariableComparison();
        public StringVariableComparison StringSpawnComparison { get; set; } = new StringVariableComparison();

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
            BooleanSpawnComparison = copy.BooleanSpawnComparison;
            IntegerSpawnComparison = copy.IntegerSpawnComparison;
            StringSpawnComparison = copy.StringSpawnComparison;
        }

    }

}
