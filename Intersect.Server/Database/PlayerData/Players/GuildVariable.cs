using Intersect.GameObjects.Switches_and_Variables;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public partial class GuildVariable : Variable
    {
        public GuildVariable() : this(Guid.Empty) { }

        public GuildVariable(Guid id)
        {
            VariableId = id;
        }

        [NotMapped]
        public string VariableName => GuildVariableBase.GetName(VariableId);

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonIgnore]
        public Guid GuildId { get; protected set; }

        [JsonIgnore]
        public virtual Guild Guild { get; protected set; }
    }
}
