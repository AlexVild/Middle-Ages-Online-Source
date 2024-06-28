using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Entities.Events;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Server.Utilities;
using Intersect.Utilities;
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
                    harvestBonus = Utilities.HarvestBonusHelper.CalculateHarvestBonus(this, resource.Base?.Id ?? Guid.Empty);
                    progressUntilNextBonus = Utilities.HarvestBonusHelper.GetHarvestsUntilNextBonus(this, resource.Base?.Id ?? Guid.Empty);
                }

                PacketSender.SendResourceLockPacket(this, val, harvestBonus, progressUntilNextBonus, resource?.Base?.Id ?? Guid.Empty);
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
            if (descriptor.Tool != tool && descriptor.Tool != -1)
            {
                // Funky little override to give better hints for using bombs as a resource
                if (descriptor.Tool == 5 && !string.IsNullOrEmpty(descriptor.CannotHarvestMessage))
                {
                    PacketSender.SendChatMsg(
                            this, descriptor.CannotHarvestMessage, ChatMessageType.Error
                        );
                }
                else
                {
                    PacketSender.SendChatMsg(
                        this, Strings.Combat.toolrequired.ToString(Options.ToolTypes[descriptor.Tool]), ChatMessageType.Error
                    );
                }
                

                return false;
            }

            return true;
        }

        private bool TryHarvestResourceProjectile(Resource target, Projectile projectile, SpellBase parentSpell)
        {
            if (target == null || target.Base == null || projectile == null || projectile.Base == null)
            {
                return false;
            }

            var tool = projectile.Base.Tool;
            // Don't bother with messaging if we're not using a tool
            if (tool == -1 && target.Base.Tool > -1)
            {
                return false;
            }

            if (!CanHarvestWithTool(target, tool))
            {
                return false;
            }

            var dmg = ClassBase.Get(ClassId)?.Damage ?? 1;
            if (projectile.Item != null)
            {
                // Item is a specified tool?
                if (projectile.Item.Tool != -1)
                {
                    dmg = projectile.Item.Damage;
                }
                // Item is normally just a weapon?
                else
                {
                    CombatUtilities.CalculateDamage(projectile.Item.AttackTypes, 1.0, 100, StatVals, new int[(int)Stats.StatCount], out dmg);
                }

                if (projectile.Spell == null)
                {
                    PacketSender.SendAnimationToProximity(projectile.Item.AttackAnimationId, 1, target.Id, target.MapId, (byte)target.X, (byte)target.Y, (sbyte)target.Dir, target.MapInstanceId, true);
                }
            }
            
            // If this is a spell-born projectile attacking a non-tool resource, attack it as normal
            if (parentSpell != null)
            {
                CombatUtilities.CalculateDamage(parentSpell.Combat.DamageTypes, 1.0, 100, StatVals, new int[(int)Stats.StatCount], out dmg);
            }

            projectile.HandleProjectileSpell(target, true, tool);
            DealTrueDamageTo(target, 100, dmg, false, true);

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
