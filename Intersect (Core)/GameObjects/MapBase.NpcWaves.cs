using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Intersect.GameObjects.Maps
{
    /// <summary>
    /// Contains the information needed to control a wave of NPCs
    /// </summary>
    public class NpcWaveDescriptor
    {
        public Guid Id { get; set; }
        public long TimeBetweenMs { get; set; }
        public long AdvanceOnTimeoutMs { get; set; } = -1;
        public bool AdvanceOnPermadeadCompletion { get; set; }
        public Guid OnStartEventId { get; set; }
        public Guid OnEndEventId { get; set; }
        public int LoopIterations { get; set; }

        public NpcWaveDescriptor()
        {
            Id = Guid.NewGuid();
        }

        // Copy constructor
        public NpcWaveDescriptor(NpcWaveDescriptor other)
        {
            Id = other.Id;
            TimeBetweenMs = other.TimeBetweenMs;
            AdvanceOnTimeoutMs = other.AdvanceOnTimeoutMs;
            AdvanceOnPermadeadCompletion = other.AdvanceOnPermadeadCompletion;
            OnStartEventId = other.OnStartEventId;
            OnEndEventId = other.OnEndEventId;
            LoopIterations = other.LoopIterations;
        }
    }

    /// <summary>
    /// Bundles multiple NpcWaves together with common settings
    /// </summary>
    public class NpcWaveGroupDescriptor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<NpcWaveDescriptor> Waves { get; set; } = new List<NpcWaveDescriptor>();
        public int AutoStartWave { get; set; }

        public NpcWaveGroupDescriptor()
        {
            Id = Guid.NewGuid();
            Name = "New Wave Group";
        }

        // Copy constructor
        public NpcWaveGroupDescriptor(NpcWaveGroupDescriptor other)
        {
            Id = other.Id;
            Name = other.Name;
            AutoStartWave = other.AutoStartWave;
            Waves = other.Waves
                .Select(w => new NpcWaveDescriptor(w))
                .ToList();
        }
    }

    public partial class MapBase : DatabaseObject<MapBase>
    {
        [Column("NpcWaveGroups")]
        [JsonIgnore]
        public string NpcWaveGroupsJson
        {
            get => JsonConvert.SerializeObject(NpcWaveGroups);
            set => NpcWaveGroups = JsonConvert.DeserializeObject<List<NpcWaveGroupDescriptor>>(value ?? string.Empty) ?? new List<NpcWaveGroupDescriptor>();
        }

        [NotMapped]
        [JsonProperty]
        public List<NpcWaveGroupDescriptor> NpcWaveGroups { get; set; } = new List<NpcWaveGroupDescriptor>();
    }
}
