using Intersect.Server.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class PlayerLoadout : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore, NotMapped]
        public virtual Player Player { get; private set; }

        public string Name { get; private set; }

        [NotMapped]
        public List<Guid> Spells { get; set; }

        [Column("Spells")]
        public string SpellsJson
        {
            get => JsonConvert.SerializeObject(Spells);
            set => Spells = JsonConvert.DeserializeObject<List<Guid>>(value ?? string.Empty);
        }

        public PlayerLoadout(Guid playerId, string name, List<Guid> spells, List<HotbarSlot> hotbarSlots)
        {
            PlayerId = playerId;
            Name = name;
            Spells = spells;
            HotbarSlots = hotbarSlots;
        }

        // EF
        public PlayerLoadout()
        {
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Loadouts.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        [NotMapped]
        public List<HotbarSlot> HotbarSlots { get; set; }

        [Column("HotbarSlots")]
        public string HotbarSlotsJson
        {
            get => JsonConvert.SerializeObject(HotbarSlots);
            set => HotbarSlots = JsonConvert.DeserializeObject<List<HotbarSlot>>(value ?? string.Empty);
        }
    }
}
