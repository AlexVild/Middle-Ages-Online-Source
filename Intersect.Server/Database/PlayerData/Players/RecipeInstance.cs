using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class RecipeInstance : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        // EF
        public RecipeInstance() { }

        public RecipeInstance(Guid playerId, Guid descriptorId, bool unlocked = true, bool fromEvent = false)
        {
            PlayerId = playerId;
            DescriptorId = descriptorId;
            Unlocked = unlocked;
            FromEvent = fromEvent;
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public Guid DescriptorId { get; set; }

        [JsonIgnore, NotMapped]
        public RecipeDescriptor Descriptor { get => RecipeDescriptor.Get(DescriptorId) ?? null; }

        public bool Unlocked { get; set; }

        public bool FromEvent { get; set; }
    }
}
