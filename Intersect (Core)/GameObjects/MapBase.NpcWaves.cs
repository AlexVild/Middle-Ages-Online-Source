using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects.Maps
{
    /// <summary>
    /// Contains the information needed to control a wave of NPCs
    /// </summary>
    public class NpcWaveDescriptor
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long TimeBetweenMs { get; set; }

        public long AdvanceOnTimeout { get; set; } = -1;

        public bool AdvanceOnPermadeadCompletion { get; set; }

        public Guid OnStartEventId { get; set; }

        public Guid OnEndEventId { get; set; }

        public int LoopIterations { get; set; }

        public NpcWaveDescriptor()
        {
            Id = Guid.NewGuid();
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

        public int EndWave { get; set; }

        public NpcWaveGroupDescriptor()
        {
            Id = Guid.NewGuid();
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
        public List<NpcWaveGroupDescriptor> NpcWaveGroups { get; private set; } = new List<NpcWaveGroupDescriptor>();
    }
}
