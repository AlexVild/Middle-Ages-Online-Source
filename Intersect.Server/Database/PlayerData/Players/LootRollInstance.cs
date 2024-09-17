using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class LootRollInstance : IPlayerOwned
    {
        /// <summary>
        /// Locking context to prevent saving this recprd to the db twice at the same time
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        private object mLock = new object();

        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public LootRollInstance() { } // EF

        public LootRollInstance(Player player, Guid eventId, List<LootRoll> lootRolls, int totalScraps = 0)
        {
            PlayerId = player?.Id ?? Guid.Empty;
            EventId = eventId;

            Loot = new List<Item>(GenerateLootFor(player, lootRolls, totalScraps: totalScraps).Where(loot => loot != null));
        }

        public static List<Item> GenerateLootFor(Player player, List<LootRoll> lootRolls, int totalScraps = 0)
        {
            var loot = new List<Item>();
            if (lootRolls == null)
            {
                return loot;
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
                    var dropTable = LootTableServerHelpers.GenerateDropTable(table.Drops, player);
                    loot.Add(LootTableServerHelpers.GetItemFromTable(dropTable));
                }
            }

            if (totalScraps > 0)
            {
                loot.Insert(0, new Item(Guid.Parse(Options.Instance.DeconstructionOpts.ScrapItemId), totalScraps));
            }

            return loot.Where(item => item != null).ToList();
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

        public void SaveToContext()
        {
            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(readOnly: false))
                {
                    context.Loot_Rolls.Add(this);
                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }
    }
}
