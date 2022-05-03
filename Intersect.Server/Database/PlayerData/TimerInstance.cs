using Intersect.GameObjects.Timers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.PlayerData
{
    public class TimerInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public TimerInstance() { } // EF

        [ForeignKey(nameof(Descriptor))]
        public Guid DescriptorId { get; set; }

        [NotMapped]
        public TimerDescriptor Descriptor { get; set; }
        
        // Default if owner is server (global timer)
        public Guid OwnerId { get; set; }

        public long TimeRemaining { get; set; }

        public int CompletionCount { get; set; }
    }
}
