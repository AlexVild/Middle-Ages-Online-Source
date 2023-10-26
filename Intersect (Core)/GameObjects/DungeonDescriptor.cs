using Intersect.GameObjects.Timers;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        /// <summary>
        /// A mapping of treasure level -> Loot
        /// </summary>
        [NotMapped] public Dictionary<int, List<LootRoll>> Treasure = new Dictionary<int, List<LootRoll>>();

        [Column("Treasure")]
        [JsonIgnore]
        public string TreasureJson
        {
            get => JsonConvert.SerializeObject(Treasure);
            set => Treasure = JsonConvert.DeserializeObject<Dictionary<int, List<LootRoll>>>(value ?? string.Empty) ?? new Dictionary<int, List<LootRoll>>();
        }

        /// <summary>
        /// A mapping of treasure level -> Loot
        /// </summary>
        [NotMapped] public List<LootRoll> GnomeTreasure = new List<LootRoll>();

        [Column("GnomeTreasure")]
        [JsonIgnore]
        public string GnomeTreasureJson
        {
            get => JsonConvert.SerializeObject(GnomeTreasure);
            set => GnomeTreasure = JsonConvert.DeserializeObject<List<LootRoll>>(value ?? string.Empty) ?? new List<LootRoll> ();
        }

        /// <summary>
        /// A mapping of treasure level -> Earned exp
        /// </summary>
        [NotMapped] public Dictionary<int, long> ExpRewards = new Dictionary<int, long>();

        [Column("ExpRewards")]
        [JsonIgnore]
        public string ExpRewardsJson
        {
            get => JsonConvert.SerializeObject(ExpRewards);
            set => ExpRewards = JsonConvert.DeserializeObject<Dictionary<int, long>>(value ?? string.Empty) ?? new Dictionary<int, long>();
        }

        [NotMapped] public List<TimeRequirement> TimeRequirements = new List<TimeRequirement>();

        [NotMapped, JsonIgnore]
        public TimeRequirement[] SortedTimeRequirements
        {
            get
            {
                return TimeRequirements
                    .OrderByDescending(req => req.Participants)
                    .ToArray();
            }
        }

        [Column("TimeRequirements")]
        [JsonIgnore]
        public string TimeRequirementsJson
        {
            get => JsonConvert.SerializeObject(TimeRequirements);
            set => TimeRequirements = JsonConvert.DeserializeObject<List<TimeRequirement>>(value ?? string.Empty) ?? new List<TimeRequirement>();
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public bool IgnoreCompletionEvents { get; set; }
        
        public bool IgnoreStartEvents { get; set; }

        public bool StoreLongestTime { get; set; }

        public bool ApplyStatCeiling { get; set; }

        public int StatCeilingTier { get; set; }
    }

    public class TimeRequirement
    {
        /// <summary>
        /// How many participants the time req is valid for
        /// </summary>
        public int Participants { get; set; }

        /// <summary>
        /// A mapping of TreasureLevel -> Time required (ms)
        /// </summary>
        public List<long> Requirements = new List<long>();

        public TimeRequirement(int participants)
        {
            Participants = participants;
        }
    }
}
