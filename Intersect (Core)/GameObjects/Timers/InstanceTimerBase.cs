using Intersect.GameObjects.Timers;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects
{
    /// <summary>
    /// Timers are used in the engine to fire off common events after a configured
    /// number of time has passed.
    /// 
    /// Timers can be set to personal, running on a single player, or instanced, running on
    /// a MapInstance
    /// </summary>
    public class InstanceTimerBase : Timer<InstanceTimerBase>, IFolderable
    {
        // EF
        public InstanceTimerBase()
        {
            Name = "New Timer";
        }

        [JsonConstructor]
        public InstanceTimerBase(Guid id) : base(id)
        {
        }

        /// <summary>
        /// A <see cref="InstanceVariableBase"/> to store the stopped time in
        /// </summary>
        [Column("InstanceVariableStore")]
        public Guid InstanceVariableStoreId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public InstanceVariableBase InstanceVariableStore
        {
            get => InstanceVariableBase.Get(InstanceVariableStoreId);
            set => InstanceVariableStoreId = value?.Id ?? Guid.Empty;
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
