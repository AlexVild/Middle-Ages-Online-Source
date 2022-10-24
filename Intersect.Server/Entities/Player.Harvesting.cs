using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Entities.Events;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public partial class Player : Entity
    {
        public void SetResourceLock(bool val, Resource resource = null)
        {
            if (PlayerDead)
            {
                return;
            }
            if (resource != null && resource.Base != null && resource.Base.DoNotRecord) return;

            val = (resource != null);
            if (resourceLock != resource) // change has occured
            {
                resourceLock = resource;

                double harvestBonus = 0.0f;
                long progressUntilNextBonus = 0;
                if (resource != null)
                {
                    harvestBonus = resource.CalculateHarvestBonus(this);
                    progressUntilNextBonus = resource.GetHarvestsUntilNextBonus(this);
                }

                PacketSender.SendResourceLockPacket(this, val, harvestBonus, progressUntilNextBonus);
            }
        }

        private void HandleResourceMelee(Resource targetResource)
        {
            if (TryHarvestResourceMelee(targetResource))
            {
                SetResourceLock(true, targetResource);
            }
            else
            {
                SetResourceLock(false);
            }
        }

        private bool TryHarvestResourceMelee(Resource target)
        {
            if (target == null || target.Base == null)
            {
                return false;
            }

            if (target.IsDead())
            {
                return false;
            }

            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            var descriptor = target.Base;
            if (!Conditions.MeetsConditionLists(descriptor.HarvestingRequirements, this, null))
            {
                if (!string.IsNullOrWhiteSpace(descriptor.CannotHarvestMessage))
                {
                    PacketSender.SendChatMsg(this, descriptor.CannotHarvestMessage, ChatMessageType.Error);
                }
                else
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.resourcereqs, ChatMessageType.Error);
                }

                return false;
            }
            if (descriptor.Tool > -1 && descriptor.Tool < Options.ToolTypes.Count)
            {
                if (weapon == null || descriptor.Tool != weapon.Descriptor.Tool)
                {
                    PacketSender.SendChatMsg(
                        this, Strings.Combat.toolrequired.ToString(Options.ToolTypes[descriptor.Tool]), ChatMessageType.Error
                    );

                    return false;
                }
            }

            var dmg = ClassBase.Get(ClassId)?.Damage ?? 1;
            if (weapon != null)
            {
                dmg = weapon.Descriptor?.Damage ?? 1;
            }

            target.TakeDamage(this, dmg);
            SendCombatEffects(target, false, dmg);

            return true;
        }
    }
}
