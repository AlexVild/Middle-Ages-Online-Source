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

        [Obsolete("Tucked away in req. weapon types instead")]
        public int MinimumWeaponLevel { get; set; }

        /// <summary>
        /// A mapping of weapon type ID => minimum level for that type
        /// </summary>
        [NotMapped]
        public Dictionary<Guid, int> ValidWeaponTypes { get; set; } = new Dictionary<Guid, int>();
        
        [Column("ValidWeaponTypes")]
        [JsonIgnore]
        public string ValidWeaponTypesJson
        {
            get => JsonConvert.SerializeObject(ValidWeaponTypes);
            set => ValidWeaponTypes = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(value ?? string.Empty) ?? new Dictionary<Guid, int>();
        }

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

        [NotMapped]
        public List<Guid> PrerequisiteEnhancements { get; set; } = new List<Guid>();

        [Column("PrerequisiteEnhancements")]
        [JsonIgnore]
        public string PrerequisiteEnhancementsJson
        {
            get => JsonConvert.SerializeObject(PrerequisiteEnhancements);
            set => PrerequisiteEnhancements = JsonConvert.DeserializeObject<List<Guid>>(value ?? string.Empty) ?? new List<Guid>();
        }

        public bool PrerequisitesMet(List<Guid> knownEnhancements)
        {
            if (PrerequisiteEnhancements.Count == 0)
            {
                return true;
            }
            return PrerequisiteEnhancements.All(enId => knownEnhancements.Contains(enId));
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

        public string GetRangeDisplay(bool percent, bool noEffectName = false)
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
                    range = $"{MinValue}% to {MaxValue}%";
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
                    range = $"{MinValue} to {MaxValue}";
                }
            }

            if (noEffectName)
            {
                return range;
            }

            return $"{effectName}: {range}";
        }
    }
}
