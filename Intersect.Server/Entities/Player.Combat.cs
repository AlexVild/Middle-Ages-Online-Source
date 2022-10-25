using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
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
        public override bool MeleeAvailable()
        {
            if (CastTime > Timing.Global.Milliseconds)
            {
                if (Options.Combat.EnableCombatChatMessages)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.channelingnoattack, ChatMessageType.Combat);
                }
            }

            return base.MeleeAvailable();
        }

        public override bool CanMeleeTarget(Entity target)
        {
            if (target == null || target.IsDisposed || target.IsDead())
            {
                return false;
            }

            if (target is Player pvpTarget && !CanPvpPlayer(pvpTarget))
            {
                return false;
            }

            return base.CanMeleeTarget(target);
        }

        public override bool IsCriticalHit(int critChance)
        {
            critChance = CalculateEffectBonus(critChance, EffectType.Affinity);

            return base.IsCriticalHit(critChance);
        }

        protected override bool TryDealDamageTo(Entity enemy,
            List<AttackTypes> attackTypes,
            int dmgScaling,
            double critMultiplier,
            ItemBase weapon,
            out int damage)
        {
            damage = 0;
            critMultiplier = 1.0; // override - determined by item or spell
            // If this is an unarmed attack, use class stats
            if (weapon == null)
            {
                var cls = ClassBase.Get(ClassId);
                if (cls == null)
                {
                    return false;
                }

                if (IsCriticalHit(cls.CritChance))
                {
                    critMultiplier = cls.CritChance;
                    critMultiplier = CalculateEffectBonus(critMultiplier, EffectType.CritBonus);
                }
            }
            else if (IsCriticalHit(weapon.CritChance))
            {
                critMultiplier = weapon.CritMultiplier;
                critMultiplier = CalculateEffectBonus(critMultiplier, EffectType.CritBonus);
            }

            // TODO evasion
            // if (!ignoreInvasion)...
            return base.TryDealDamageTo(enemy, attackTypes, dmgScaling, critMultiplier, weapon, out damage);
        }

        public override void MeleeAttack(Entity enemy, bool ignoreEvasion)
        {
            StealthAttack = IsStealthed;
            Unstealth();

            // Send an attack attempt to the client
            PacketSender.SendEntityAttack(this, CalculateAttackTime());

            // Attack literally missing
            if (!CanMeleeTarget(enemy))
            {
                return;
            }
            
            // Short-circuit out if resource and let resource harvesting logic go
            if (enemy is Resource targetResource)
            {
                HandleResourceMelee(targetResource);
                return;
            }
            // We're not attacking a resource - disable resource locking
            SetResourceLock(false);

            // If we're doing a player interaction, start those events
            if (enemy is Player pvpTarget)
            {
                pvpTarget.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerInteract, "", Name);
            }

            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            List<AttackTypes> attackTypes = new List<AttackTypes>();
            if (weapon?.Descriptor?.AttackTypes != null)
            {
                attackTypes.AddRange(weapon.Descriptor.AttackTypes);
            }

            if (!TryDealDamageTo(enemy, attackTypes, 100, 1.0, weapon?.Descriptor, out int damage))
            {
                return;
            }

            if (TryLifesteal(damage, enemy, out var healthRecovered))
            {
                PacketSender.SendActionMsg(
                    this, Strings.Combat.addsymbol + (int)healthRecovered, CustomColors.Combat.Heal
                );
            }
        }

        public override void ProjectileAttack(Entity enemy, Projectile projectile, SpellBase parentSpell, ItemBase parentWeapon, byte projectileDir)
        {
            if (projectile == null || projectile.Base == null)
            {
                return;
            }
            if (!CanRangeTarget(enemy))
            {
                return;
            }

            var descriptor = projectile.Base;

            if (enemy is Player pvpTarget)
            {
                pvpTarget.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerInteract, "", Name);
                if (!CanPvpPlayer(pvpTarget))
                {
                    return;
                }
            }
            else if (enemy is Resource resource)
            {
                _ = TryHarvestResourceProjectile(resource, projectile);
                return;
            }

            // Handle the _projectile's_ spell
            projectile.HandleProjectileSpell(enemy, false);

            // Handle the spell cast from the parent
            if (parentSpell != null)
            {
                TryAttackSpell(enemy, parentSpell, out bool spellMissed, out bool spellBlocked, (sbyte)projectileDir, true);
            }
            // Otherwise, handle the weapon
            else if (parentWeapon != null)
            {
                var deadAnimations = new List<KeyValuePair<Guid, sbyte>>();
                var aliveAnimations = new List<KeyValuePair<Guid, sbyte>>();

                if (parentWeapon.AttackAnimationId != Guid.Empty)
                {
                    deadAnimations.Add(new KeyValuePair<Guid, sbyte>(parentWeapon.AttackAnimationId, (sbyte)projectileDir));
                    aliveAnimations.Add(new KeyValuePair<Guid, sbyte>(parentWeapon.AttackAnimationId, (sbyte)projectileDir));
                }

                if (!TryDealDamageTo(enemy, parentWeapon.AttackTypes, 100, 1.0, parentWeapon, out int weaponDamage))
                {
                    return;
                }
            }

            if (descriptor.Knockback > 0 && projectileDir < 4 && !enemy.IsImmuneTo(Immunities.Knockback) && !StatusActive(StatusTypes.Steady))
            {
                if (!(enemy is AttackingEntity) || enemy.IsDead() || enemy.IsDisposed)
                {
                    return;
                }

                ((AttackingEntity)enemy).Knockback(projectileDir, descriptor.Knockback);
            }
        }

        private bool CanPvpPlayer(Player target)
        {
            return !(target == null
                || InParty(target)
                || Map.ZoneType == MapZones.Safe
                || target.Map?.ZoneType == MapZones.Safe);
        }

        private bool TryLifesteal(int damage, Entity target, out float recovered)
        {
            recovered = 0;
            if (damage <= 0 || target == null || target is Resource) 
            {
                return false;
            }

            var lifesteal = GetEquipmentBonusEffect(EffectType.Lifesteal) / 100f;
            var healthRecovered = lifesteal * damage;
            if (healthRecovered <= 0)
            {
                return false;
            }

            AddVital(Vitals.Health, (int)healthRecovered);
            recovered = healthRecovered;
            return true;
        }

        public override void SendAttackAnimation(Entity enemy)
        {
            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            List<AttackTypes> attackTypes = new List<AttackTypes>();
            if (weapon == default || weapon?.Descriptor == null)
            {
                // Unarmed attack
                attackTypes.Add(AttackTypes.Blunt);
                SendUnarmedAttackAnimation();
            }
            else
            {
                attackTypes.AddRange(weapon.Descriptor.AttackTypes);
                SendWeaponAttackAnimation(weapon.Descriptor);
            }
        }

        private void SendUnarmedAttackAnimation()
        {
            var classBase = ClassBase.Get(ClassId);
            var attackingTile = GetMeleeAttackTile();

            if (attackingTile.TryFix())
            {
                PacketSender.SendAnimationToProximity(
                    classBase.AttackAnimationId, -1, Id, attackingTile.GetMapId(), attackingTile.GetX(),
                    attackingTile.GetY(), (sbyte)Dir, MapInstanceId
                );
            }
        }

        private void SendWeaponAttackAnimation(ItemBase weaponItem)
        {
            var attackAnim = weaponItem.AttackAnimation;
            var attackingTile = GetMeleeAttackTile();
            if (attackAnim != null && attackingTile.TryFix() && weaponItem.ProjectileId == Guid.Empty)
            {
                PacketSender.SendAnimationToProximity(
                    attackAnim.Id, -1, Id, attackingTile.GetMapId(), attackingTile.GetX(),
                    attackingTile.GetY(), (sbyte)Dir, MapInstanceId
                );
            }
        }

        public bool TrySpawnWeaponProjectile(long latencyAdjustmentMs)
        {
            if (!TryGetEquippedItem(Options.WeaponIndex, out var weapon))
            {
                return false;
            }

            var projectile = weapon.Descriptor?.Projectile;
            if (projectile == default)
            {
                return false;
            }

            if (projectile.Ammo != default)
            {
                var itemSlot = FindInventoryItemSlot(
                    projectile.AmmoItemId, projectile.AmmoRequired
                );

                if (itemSlot == null)
                {
                    PacketSender.SendChatMsg(
                        this,
                        Strings.Items.notenough.ToString(ItemBase.GetName(projectile.AmmoItemId)),
                        ChatMessageType.Inventory,
                        CustomColors.Combat.NoAmmo
                    );

                    return false;
                }
            }

            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
            {
                mapInstance
                        .SpawnMapProjectile(
                            this, projectile, null, weapon.Descriptor, MapId,
                            (byte)X, (byte)Y, (byte)Z,
                            (byte)Dir, null
                        );

                AttackTimer = Timing.Global.Milliseconds +
                    latencyAdjustmentMs +
                    CalculateAttackTime();
            }

            return true;
        }
    }
}
