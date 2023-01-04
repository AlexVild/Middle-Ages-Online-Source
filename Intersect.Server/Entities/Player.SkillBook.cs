using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Server.Entities.Events;

using Newtonsoft.Json;
using Intersect.Server.Entities.PlayerData;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<PlayerSkillInstance> SkillBook { get; set; }

        [NotMapped, JsonIgnore]
        public int SkillPointsAvailable => SkillBook
            .ToArray()
            .Where(spell => spell.Equipped)
            .Aggregate(0, (acc, spell) => acc += spell.RequiredSkillPoints);

        public int SkillPointTotal { get; set; }

        public bool SkillInSkillbook(Guid spellId)
        {
            return SkillBook.ToArray().FirstOrDefault(s => s.Id == spellId) != default;
        }

        public bool TryEquipSpell(Guid spellId, out string reason)
        {
            reason = string.Empty;
            var spellBook = SkillBook.ToArray();
            var spell = spellBook.FirstOrDefault(s => s.Id == spellId);

            // Spell not in spell book
            if (!SkillInSkillbook(spellId))
            {
                reason = "You haven't learned this skill yet!";
                return false;
            }

            // Not enough skill points
            if (SkillPointsAvailable < spell.RequiredSkillPoints)
            {
                reason = "You don't have enough skill points available to equip this skill!";
                return false;
            }

            if (!Conditions.MeetsConditionLists(spell.Spell.CastingRequirements, this, null))
            {
                if (!string.IsNullOrEmpty(spell.Spell.CannotCastMessage))
                {
                    reason = spell.Spell.CannotCastMessage;
                }
                else
                {
                    reason = "You lack some requirement(s) to equip this skill!";
                }
                
                return false;
            }

            // Player can go ahead
            return true;
        }
    }
}
