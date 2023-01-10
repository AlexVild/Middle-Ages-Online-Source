using Intersect.GameObjects;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class PassiveSpell
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SpellId { get; set; }

        public SpellBase Spell { get => SpellBase.Get(SpellId); }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        public Player Player { get; set; }

        public bool IsActive { get; set; }

        public PassiveSpell() // EF
        {
        }

        public PassiveSpell(Guid spellId, Guid playerId, bool isActive = false)
        {
            SpellId = spellId;
            PlayerId = playerId;
            IsActive = isActive;
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Passive_Spells.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
