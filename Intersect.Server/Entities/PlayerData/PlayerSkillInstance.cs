using Intersect.GameObjects;
using Intersect.Server.Database;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Entities.PlayerData
{
    public class PlayerSkillInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public PlayerSkillInstance() // EF
        {
        }

        public PlayerSkillInstance(SpellBase spell, Player player, bool equipped = false) 
        {
            Spell = spell;
            PlayerId = player?.Id ?? Guid.Empty;
            Equipped = equipped;
        }

        public PlayerSkillInstance(Guid spellId, Player player, bool equipped = false)
        {
            SpellId = spellId;
            PlayerId = player?.Id ?? Guid.Empty;
            Equipped = equipped;
        }

        public Guid SpellId { get; set; }

        [NotMapped]
        public SpellBase Spell
        {
            get => SpellBase.Get(SpellId);
            set => SpellId = value?.Id ?? Guid.Empty;
        }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        public Player Player { get; set; }

        [NotMapped]
        public int RequiredSkillPoints => Spell?.RequiredSkillPoints ?? 0;

        public bool Equipped { get; set; }

        public void RemoveSkillbookEntryDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Unlocked_Skills.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
