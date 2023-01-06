using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Server.Entities.Events;

using Newtonsoft.Json;
using Intersect.Server.Entities.PlayerData;
using Intersect.Utilities;
using Intersect.Server.Networking;
using Intersect.GameObjects;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<PlayerSkillInstance> SkillBook { get; set; } = new List<PlayerSkillInstance>();

        [NotMapped, JsonIgnore]
        public int SkillPointsAvailable => SkillBook
            .ToArray()
            .Where(spell => spell.Equipped)
            .Aggregate(0, (acc, spell) => acc += spell.RequiredSkillPoints);

        public int SkillPointTotal { get; set; }

        public bool TryGetSkillInSkillbook(Guid spellId, out PlayerSkillInstance skill)
        {
            skill = SkillBook.ToArray().FirstOrDefault(s => s.Id == spellId);
            return skill != default;
        }

        public bool SkillPrepared(Guid spellId)
        {
            TryGetEquippedItem(Options.WeaponIndex, out var equippedWeapon);

            // Allow special attacks through
            if (spellId == equippedWeapon?.Descriptor?.SpecialAttack?.SpellId)
            {
                return true;
            }

            if (!TryGetSkillInSkillbook(spellId, out var skill))
            {
                return false;
            }

            return skill.Equipped;
        }

        public bool TryToggleSkillPrepare(Guid spellId, bool isPrepare, out string failureReason)
        {
            failureReason = string.Empty;
            
            if (CastTime > Timing.Global.Milliseconds)
            {
                failureReason = "You can't prepare/unprepare skills while casting!";
                return false;
            }

            if (CombatTimer > Timing.Global.Milliseconds)
            {
                failureReason = "You can't prepare/unprepare skills while in combat!";
                return false;
            }

            // Spell not in spell book
            if (!TryGetSkillInSkillbook(spellId, out var spell)) 
            {
                return false;
            }

            // The remainder of the logic only pertains to preparing a skill, so if we were un-preparing, we're done
            if (!isPrepare)
            {
                return true;
            }

            // Not enough skill points
            if (SkillPointsAvailable < spell.RequiredSkillPoints)
            {
                failureReason = "You don't have enough skill points available to equip this skill!";
                return false;
            }

            if (!Conditions.MeetsConditionLists(spell.Spell.CastingRequirements, this, null))
            {
                if (!string.IsNullOrEmpty(spell.Spell.CannotCastMessage))
                {
                    failureReason = spell.Spell.CannotCastMessage;
                }
                else
                {
                    failureReason = "You lack some requirement(s) to equip this skill!";
                }
                
                return false;
            }

            // Player can go ahead
            return true;
        }

        public void PrepareSkill(Guid spellId)
        {
            if (!TryToggleSkillPrepare(spellId, true, out string failureReason))
            {
                PacketSender.SendChatMsg(this, failureReason, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                return;
            }

            if (!TryTeachSpell(new Database.Spell(spellId)))
            {
                PacketSender.SendChatMsg(this, "You already have this skill prepared!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
            }
        }

        public void UnprepareSkill(Guid spellId)
        {
            if (!TryToggleSkillPrepare(spellId, false, out string failureReason))
            {
                PacketSender.SendChatMsg(this, failureReason, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                return;
            }

            if (!TryForgetSpell(new Database.Spell(spellId)))
            {
                PacketSender.SendChatMsg(this, "You never had this skill prepared!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
            }
        }
    }
}
