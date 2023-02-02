using Intersect.GameObjects.Timers;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects
{
    public class DungeonDescriptor : DatabaseObject<DungeonDescriptor>, IFolderable
    {
        public DungeonDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public DungeonDescriptor(Guid id) : base(id)
        {
            Name = "New Dungeon";
        }

        public string DisplayName { get; set; }

        public Guid TimerId { get; set; }

        [NotMapped, JsonIgnore]
        public TimerDescriptor Timer => TimerDescriptor.Get(TimerId);

        public int GnomeLocations { get; set; }

        public Guid CompletionCounterId { get; set; }

        [NotMapped, JsonIgnore]
        public PlayerVariableBase CompletionCounter => PlayerVariableBase.Get(CompletionCounterId);

        [NotMapped] public Dictionary<int, List<BaseDrop>> Treasure = new Dictionary<int, List<BaseDrop>>();

        [Column("Treasure")]
        [JsonIgnore]
        public string TreasureJson
        {
            get => JsonConvert.SerializeObject(Treasure);
            set => Treasure = JsonConvert.DeserializeObject<Dictionary<int, List<BaseDrop>>>(value ?? string.Empty) ?? new Dictionary<int, List<BaseDrop>>();
        }

        [NotMapped] public Dictionary<int, long> ExpRewards = new Dictionary<int, long>();

        [Column("ExpRewards")]
        [JsonIgnore]
        public string ExpRewardsJson
        {
            get => JsonConvert.SerializeObject(ExpRewards);
            set => ExpRewards = JsonConvert.DeserializeObject<Dictionary<int, long>>(value ?? string.Empty) ?? new Dictionary<int, long>();
        }

        [NotMapped] public Dictionary<int, long> TimeRequirements = new Dictionary<int, long>();

        [Column("TimeRequirements")]
        [JsonIgnore]
        public string TimeRequirementsJson
        {
            get => JsonConvert.SerializeObject(TimeRequirements);
            set => TimeRequirements = JsonConvert.DeserializeObject<Dictionary<int, long>>(value ?? string.Empty) ?? new Dictionary<int, long>();
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
