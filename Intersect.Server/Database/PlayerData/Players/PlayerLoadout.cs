using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Server.Networking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
            SaveHotbarSlots(hotbarSlots);
        }

        public void SaveHotbarSlots(List<HotbarSlot> hotbarSlots)
        {
            if (HotbarSlots == null)
            {
                HotbarSlots = new List<HotbarSlot>();
            }
            else
            {
                HotbarSlots.Clear();
            }

            for (var idx = 0; idx < Options.MaxHotbar; idx++)
            {
                HotbarSlot slot = hotbarSlots.ElementAtOrDefault(idx);
                var newSlot = new HotbarSlot(idx);

                if (slot != default)
                {
                    slot.PreferredStatBuffs.CopyTo(newSlot.PreferredStatBuffs, 0);
                    newSlot.PreferredStatBuffs = slot.PreferredStatBuffs;
                    newSlot.ItemOrSpellId = slot.ItemOrSpellId;
                    newSlot.BagId = slot.BagId;
                }

                HotbarSlots.Add(newSlot);
            }
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

        public void SaveToDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                if (context.Player_Loadouts.Contains(this))
                {
                    context.Player_Loadouts.Update(this);
                }
                else
                {
                    context.Player_Loadouts.Add(this);
                }

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }

            var player = Globals.OnlineList.Find(pl => pl.Id == PlayerId);
            if (player != null)
            {
                PacketSender.SendLoadouts(player);
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

        public Loadout Packetize()
        {
            return new Loadout(Id, Name);
        }
    }
}
