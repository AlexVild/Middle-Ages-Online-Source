using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class PlayerEnhancementInstance : IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [JsonIgnore, NotMapped]
        public Player Player { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        [Column("Enhancement")]
        public Guid EnhancementId { get; set; }

        [NotMapped, JsonIgnore]
        public EnhancementDescriptor Enhancement => EnhancementDescriptor.Get(EnhancementId);

        public bool Unlocked { get; set; }

        public PlayerEnhancementInstance()
        {
        }

        public PlayerEnhancementInstance(Player player, Guid enhancementId)
        {
            if (player == default)
            {
                return;
            }

            PlayerId = player.Id;
            EnhancementId = enhancementId;
            Unlocked = true;
        }
    }
}
