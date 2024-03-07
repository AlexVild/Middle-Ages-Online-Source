using System;
using Intersect.Enums;
using Intersect.GameObjects.Switches_and_Variables;
using Newtonsoft.Json;

namespace Intersect.GameObjects
{

    public partial class PlayerVariableBase : VariableDescriptor<PlayerVariableBase>, IVariableBase
    {

        [JsonConstructor]
        public PlayerVariableBase(Guid id) : base(id)
        {
            Name = "New Player Variable";
        }

        public PlayerVariableBase()
        {
            Name = "New Player Variable";
        }

        //Identifier used for event chat variables to display the value of this variable/switch.
        //See https://www.ascensiongamedev.com/topic/749-event-text-variables/ for usage info.
        public string TextId { get; set; }

        public VariableDataTypes Type { get; set; } = VariableDataTypes.Boolean;

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }

    public partial class PlayerVariableBase : VariableDescriptor<PlayerVariableBase>, IVariableBase
    {
        public bool Recordable { get; set; } = false;

        public bool RecordLow { get; set; } = false;

        public bool RecordSilently { get; set; } = false;

        public bool SoloRecordOnly { get; set; } = false;

        public string VariableGroup { get; set; } = string.Empty;
    }
}
