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
    public class PlayerTimerBase : Timer<PlayerTimerBase>, IFolderable
    {
        // EF
        public PlayerTimerBase()
        {
            Name = "New Timer";
        }

        [JsonConstructor]
        public PlayerTimerBase(Guid id) : base(id)
        {
        }

        /// <summary>
        /// Whether or not a timer should keep going on login, if personal
        /// </summary>
        public bool ContinueOnLogin = false;

        /// <summary>
        /// Whether or not a timer's "Failed" event should run on login, if descending and personal
        /// </summary>
        public bool FailOnLogin = false;

        /// <summary>
        /// A <see cref="PlayerVariableBase"/> to store the stopped time in
        /// </summary>
        [Column("PlayerVariableStore")]
        public Guid PlayerVariableStoreId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public PlayerVariableBase PlayerVariableStore
        {
            get => PlayerVariableBase.Get(PlayerVariableStoreId);
            set => PlayerVariableStoreId = value?.Id ?? Guid.Empty;
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
