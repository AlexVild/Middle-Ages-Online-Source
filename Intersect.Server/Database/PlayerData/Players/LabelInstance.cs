using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class LabelInstance : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        // EF
        public LabelInstance() { }

        public LabelInstance(Guid playerId, Guid descriptorId)
        {
            PlayerId = playerId;
            DescriptorId = descriptorId;
            IsNew = true;
        }

        public static LabelInstance Create(Guid playerId, Guid descriptorId)
        {
            LabelInstance label;
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                label = new LabelInstance(playerId, descriptorId);
                context.Player_Labels.Add(label);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                return label;
            }

            return null;
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public Guid DescriptorId { get; set; }

        [JsonIgnore, NotMapped]
        public LabelDescriptor Descriptor { get => LabelDescriptor.Get(DescriptorId) ?? null; }

        public bool IsNew { get; set; }
    }
}
