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
    public partial class Player : AttackingEntity
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

        private bool CanHarvest(Resource target)
        {
            if (target == null || target.Base == null)
            {
                return false;
            }

            var descriptor = target.Base;
            if (target.IsDead())
            {
                return false;
            }

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

            return true;
        }

        private bool CanHarvestWithTool(Resource target, int tool)
        {
            if (!CanHarvest(target))
            {
                return false;
            }

            var descriptor = target.Base;
            if (descriptor.Tool != tool)
            {
                PacketSender.SendChatMsg(
                    this, Strings.Combat.toolrequired.ToString(Options.ToolTypes[descriptor.Tool]), ChatMessageType.Error
                );

                return false;
            }

            return true;
        }

        private bool TryHarvestResourceProjectile(Resource target, Projectile projectile)
        {
            if (target == null || target.Base == null || projectile == null || projectile.Base == null)
            {
                return false;
            }

            if (!CanHarvestWithTool(target, projectile.Base.Tool))
            {
                return false;
            }

            var dmg = ClassBase.Get(ClassId)?.Damage ?? 1;
            if (projectile.Item != null)
            {
                dmg = projectile.Item.Damage;
            }

            projectile.HandleProjectileSpell(target, true);
            DealTrueDamageTo(target, 0, dmg, false, true);

            return true;
        }

        private bool TryHarvestResourceMelee(Resource target)
        {
            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            if (!CanHarvestWithTool(target, weapon?.Descriptor?.Tool ?? -1))
            {
                return false;
            }
            
            var dmg = ClassBase.Get(ClassId)?.Damage ?? 1;
            if (weapon != null)
            {
                dmg = weapon.Descriptor?.Damage ?? 1;
            }

            DealTrueDamageTo(target, 100, dmg, false, true);

            return true;
        }
    }
}
