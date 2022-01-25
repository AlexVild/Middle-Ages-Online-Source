using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Models;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{
    public class InstanceVariableBase : DatabaseObject<InstanceVariableBase>, IFolderable
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

        /// <summary>
        /// Retrieve an array of variable names of the supplied data type.
        /// </summary>
        /// <param name="dataType">The data type to retrieve names of.</param>
        /// <returns>Returns an array of names.</returns>
        public static string[] GetNamesByType(VariableDataTypes dataType)
        {
            return Lookup.KeyList.OrderBy(pairs => Lookup[pairs]?.Name).Where(pairs => ((InstanceVariableBase)Lookup[pairs]).Type == dataType).Select(pairs => ((InstanceVariableBase)Lookup[pairs]).Name).ToArray();
        }

        /// <summary>
        /// Retrieve the list index of an Id within a specific data type list.
        /// </summary>
        /// <param name="id">The Id to look up.</param>
        /// <param name="dataType">The data type to search up.</param>
        /// <returns>Returns the list Index of the provided Id.</returns>
        public static int ListIndex(Guid id, VariableDataTypes dataType)
        {
            return Lookup.KeyList.OrderBy(pairs => Lookup[pairs]?.Name).Where(pairs => ((InstanceVariableBase)Lookup[pairs]).Type == dataType).Select(pairs => ((InstanceVariableBase)Lookup[pairs]).Id).ToList().IndexOf(id);
        }

        /// <summary>
        /// Retrieve the Id associated with a list index of a specific data type.
        /// </summary>
        /// <param name="listIndex">The list index to retrieve.</param>
        /// <param name="dataType">The data type to search up.</param>
        /// <returns>Returns the Id of the provided index.</returns>
        public static Guid IdFromList(int listIndex, VariableDataTypes dataType)
        {
            if (listIndex < 0 || listIndex > GetNamesByType(dataType).Length)
            {
                return Guid.Empty;
            }

            return Lookup.KeyList.OrderBy(pairs => Lookup[pairs]?.Name).Where(pairs => ((InstanceVariableBase)Lookup[pairs]).Type == dataType).Select(pairs => ((InstanceVariableBase)Lookup[pairs]).Id).ToArray()[listIndex];
        }
    }
}
