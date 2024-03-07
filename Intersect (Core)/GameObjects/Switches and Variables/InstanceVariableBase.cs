using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Models;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{
    public class InstanceVariableBase : VariableDescriptor<InstanceVariableBase>, IVariableBase
    {
        [JsonConstructor]
        public InstanceVariableBase(Guid id) : base(id)
        {
            Name = "New Instance Variable";
        }

        public InstanceVariableBase()
        {
            Name = "New Instance Variable";
        }

        public string TextId { get; set; }

        public VariableDataTypes Type { get; set; } = VariableDataTypes.Boolean;

        [NotMapped]
        [JsonIgnore]
        public VariableValue DefaultValue { get; set; } = new VariableValue();

        [NotMapped]
        [JsonProperty("Value")]
        public dynamic ValueData { get => DefaultValue.Value; set => DefaultValue.Value = value; }

        [Column(nameof(DefaultValue))]
        [JsonIgnore]
        public string Json
        {
            get => DefaultValue.Json.ToString(Formatting.None);
            private set
            {
                if (VariableValue.TryParse(value, out var json))
                {
                    DefaultValue.Json = json;
                }
            }
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
