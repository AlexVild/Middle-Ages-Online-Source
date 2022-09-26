using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class LootRollInstance : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public LootRollInstance() { } // EF

        public LootRollInstance(Player player, Guid eventId, List<LootRoll> lootRolls)
        {
            Id = new Guid();
            PlayerId = player?.Id ?? Guid.Empty;
            EventId = eventId;

            // Generate loot
            Loot = new List<Item>();
            if (lootRolls == null)
            {
                return;
            }
            foreach (var lootRoll in lootRolls)
            {
                var table = lootRoll.LootTable;
                if (table == null || !Conditions.MeetsConditionLists(table.DropConditions, player, null))
                {
                    continue;
                }

                for (var i = 0; i < lootRoll.Rolls; i++)
                {
                    var dropTable = LootTableServerHelpers.GenerateDropTable(table.Drops);
                    Loot.Add(LootTableServerHelpers.GetItemFromTable(dropTable));
                }
            }
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; private set; }

        [JsonIgnore]
        public virtual Player Player { get; private set; }

        public Guid EventId { get; private set; }

        [NotMapped, JsonIgnore]
        public List<Item> Loot { get; set; }

        [Column("Loot"), JsonIgnore]
        public string LootJson
        {
            get => JsonConvert.SerializeObject(Loot);
            set
            {
                Loot = JsonConvert.DeserializeObject<List<Item>>(value ?? "");
                if (Loot == null)
                {
                    Loot = new List<Item>();
                }
            }
        }
    }
}
