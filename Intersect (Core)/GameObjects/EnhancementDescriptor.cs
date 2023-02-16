using Intersect.Enums;
using Intersect.Models;
using Intersect.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.GameObjects
{
    public class EnhancementDescriptor : DatabaseObject<EnhancementDescriptor>, IFolderable
    {
        public EnhancementDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public EnhancementDescriptor(Guid id) : base(id)
        {
            Name = "New Enhancement";
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public int RequiredEnhancementPoints { get; set; }

        [NotMapped]
        public List<Enhancement<Stats>> StatMods { get; set; } = new List<Enhancement<Stats>>();
        
        [Column("StatMods")]
        [JsonIgnore]
        public string StatModsJson
        {
            get => JsonConvert.SerializeObject(StatMods);
            set => StatMods = JsonConvert.DeserializeObject<List<Enhancement<Stats>>>(value ?? string.Empty) ?? new List<Enhancement<Stats>>();
        }

        [NotMapped]
        public List<Enhancement<Vitals>> VitalMods { get; set; } = new List<Enhancement<Vitals>>();

        [Column("VitalMods")]
        [JsonIgnore]
        public string VitalModsJson
        {
            get => JsonConvert.SerializeObject(VitalMods);
            set => VitalMods = JsonConvert.DeserializeObject<List<Enhancement<Vitals>>>(value ?? string.Empty) ?? new List<Enhancement<Vitals>>();
        }

        [NotMapped]
        public List<Enhancement<EffectType>> EffectMods { get; set; } = new List<Enhancement<EffectType>>();

        [Column("EffectMods")]
        [JsonIgnore]
        public string EffectModsJson
        {
            get => JsonConvert.SerializeObject(EffectMods);
            set => EffectMods = JsonConvert.DeserializeObject<List<Enhancement<EffectType>>>(value ?? string.Empty) ?? new List<Enhancement<EffectType>>();
        }
    }

    public class Enhancement<T> where T : Enum
    {
        public Enhancement(T enhancementType, int minValue, int maxValue)
        {
            EnhancementType = enhancementType;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public T EnhancementType { get; set; }

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string GetRangeDisplay(bool percent)
        {
            string effectName = string.IsNullOrEmpty(EnhancementType.GetDescription()) ? 
                Enum.GetName(typeof(T), EnhancementType) : 
                EnhancementType.GetDescription();
            
            string range;
            if (percent)
            {
                if (MinValue == MaxValue)
                {
                    range = $"{MinValue}%";
                }
                else
                {
                    range = $"{MinValue}% - {MaxValue}%";
                }
            }
            else
            {
                if (MinValue == MaxValue)
                {
                    range = $"{MinValue}";
                }
                else
                {
                    range = $"{MinValue} - {MaxValue}";
                }
            }

            return $"{effectName}: {range}";
        }
    }
}
