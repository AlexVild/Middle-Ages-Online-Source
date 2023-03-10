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
using Intersect.Server.Database;
using Intersect.Server.Localization;
using Intersect.Server.Core;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<PlayerSkillInstance> SkillBook { get; set; } = new List<PlayerSkillInstance>();

        [NotMapped, JsonIgnore]
        public int SkillPointsAvailable => SkillBook
            .ToArray()
            .Where(spell => spell.Equipped)
            .Aggregate(SkillPointTotal, (acc, spell) => acc -= spell.RequiredSkillPoints);

        public int SkillPointTotal { get; set; }

        public bool TryGetSkillInSkillbook(Guid spellId, out PlayerSkillInstance skill)
        {
            skill = SkillBook.ToList().Find(s => s.SpellId == spellId);
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

            if (InstanceType == Enums.MapInstanceType.Shared)
            {
                failureReason = "You can't prepare/unprepare skills while in a dungeon instance!";
                return false;
            }

            if (Map.ZoneType != Enums.MapZones.Safe)
            {
                failureReason = "You can't prepare/unprepare skills while in a PvP zone!";
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

            // Player can go ahead
            return true;
        }

        public void PrepareSkill(Guid spellId)
        {
            var descriptor = SpellBase.Get(spellId);
            if (!TryToggleSkillPrepare(spellId, true, out string failureReason))
            {
                PacketSender.SendChatMsg(this, failureReason, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                return;
            }

            if (!TryGetSkillInSkillbook(spellId, out var skill))
            {
                PacketSender.SendChatMsg(this, "This skill isn't in your skill book!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
            }

            if (descriptor?.SpellType == Enums.SpellTypes.Passive)
            {
                ActivatePassive(spellId);
            }
            else if (!TryTeachSpell(new Spell(spellId)))
            {
                PacketSender.SendChatMsg(this, "You already have this skill prepared!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
            }

            skill.Equipped = true;
        }

        public void UnprepareSkill(Guid spellId, bool force = false)
        {
            if (!force && !TryToggleSkillPrepare(spellId, false, out string failureReason))
            {
                PacketSender.SendChatMsg(this, failureReason, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                return;
            }

            if (!TryGetSkillInSkillbook(spellId, out var skill))
            {
                PacketSender.SendChatMsg(this, "This skill isn't in your skill book!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
            }

            var descriptor = SpellBase.Get(spellId);
            if (descriptor.SpellType == Enums.SpellTypes.Passive)
            {
                DeactivatePassive(spellId);
            } 
            else if (!TryForgetSpell(new Spell(spellId)))
            {
                PacketSender.SendChatMsg(this, "You never had this skill prepared!", Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
            }

            skill.Equipped = false;
        }

        public bool TryAddSkillToBook(Guid spellId)
        {
            if (SkillBook.Find(s => s.SpellId == spellId) != default)
            {
                return false;
            }

            var newSkill = new PlayerSkillInstance(spellId, this);
            SkillBook.Add(newSkill);

            PacketSender.SendSkillbookToClient(this);
            PacketSender.SendSkillStatusUpdate(this, "New skill(s)!");
            RecipeUnlockWatcher.EnqueueNewPlayer(this, spellId, RecipeTrigger.SpellLearned);

            return true;
        }

        public void SendSkillAddedMessage(Guid spellId)
        {
            var descriptor = SpellBase.Get(spellId);
            if (string.IsNullOrEmpty(descriptor.SpellGroup))
            {
                PacketSender.SendChatMsg(this,
                    Strings.Combat.SpellLearnedOther.ToString(descriptor?.Name),
                    Enums.ChatMessageType.Spells, CustomColors.General.GeneralCompleted, sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this, Strings.Combat.SpellLearned.ToString(descriptor?.SpellGroup, descriptor?.Name),
                    Enums.ChatMessageType.Spells, CustomColors.General.GeneralCompleted, sendToast: true);
            }
        }

        public void SendSkillLostMessage(Guid spellId)
        {
            var descriptor = SpellBase.Get(spellId);
            PacketSender.SendChatMsg(this, Strings.Combat.SpellLost.ToString(descriptor?.Name),
                    Enums.ChatMessageType.Spells, CustomColors.General.GeneralCompleted, sendToast: true);
        }

        public bool TryRemoveSkillFromSkillbook(Guid spellId)
        {
            var skills = SkillBook.FindAll(s => s.SpellId == spellId).ToList();
            if (skills?.Count <= 0)
            {
                return false;
            }

            foreach(var skill in skills)
            {
                TryForgetSpell(new Spell(skill.SpellId));
                SkillBook.Remove(skill);
                DbInterface.Pool.QueueWorkItem(skill.RemoveSkillbookEntryDb);
                if (SpellBase.Get(skill.SpellId)?.SpellType == Enums.SpellTypes.Passive)
                {
                    RemovePassive(skill.SpellId);
                }
            }

            PacketSender.SendSkillbookToClient(this);
            RecipeUnlockWatcher.EnqueueNewPlayer(this, spellId, RecipeTrigger.SpellLearned);

            return true;
        }

        public void UnprepareAllSkills()
        {
            var prevSpAvailable = SkillPointsAvailable;

            var change = false;
            foreach (var skill in SkillBook)
            {
                if (!skill.Equipped)
                {
                    continue;
                }

                change = true;
                UnprepareSkill(skill.SpellId, true);
            }

            if (change)
            {
                PacketSender.SendChatMsg(this, "You need to re-assign your skill points.", Enums.ChatMessageType.Experience, sendToast: true);
            }
        }
    }
}
