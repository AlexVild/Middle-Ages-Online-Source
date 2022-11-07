using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
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
    public abstract partial class AttackingEntity : Entity
    {
        protected bool IsStunnedOrSleeping => CachedStatuses.Any(PredicateStunnedOrSleeping);

        protected bool IsImmobile => CachedStatuses.Any(PredicateCantMove);

        private static bool PredicateStunnedOrSleeping(Status status)
        {
            switch (status?.Type)
            {
                case StatusTypes.Sleep:
                case StatusTypes.Stun:
                    return true;
                default:
                    return false;
            }
        }

        private static bool PredicateCantMove(Status status)
        {
            switch (status?.Type)
            {
                case StatusTypes.Sleep:
                case StatusTypes.Stun:
                case StatusTypes.Snare:
                    return true;
                default:
                    return false;
            }
        }

        public abstract void MeleeAttack(Entity enemy, bool ignoreEvasion);

        public abstract void SendAttackAnimation(Entity enemy);

        public virtual void ProjectileAttack(Entity enemy,
            Projectile projectile,
            SpellBase parentSpell,
            ItemBase parentWeapon,
            byte projectileDir)
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
            // Handle the _projectile's_ spell
            projectile.HandleProjectileSpell(enemy, false);

            // Handle the spell cast from the parent
            if (parentSpell != null)
            {
                SpellAttack(enemy, parentSpell, (sbyte)projectile.Dir, projectile);
            }
            // Otherwise, handle the weapon
            else if (parentWeapon != null)
            {
                if (!TryDealDamageTo(enemy, parentWeapon.AttackTypes, 100, 1.0, parentWeapon, null, out int weaponDamage))
                {
                    return;
                }
            }

            var animation = parentWeapon?.AttackAnimationId ?? parentSpell?.HitAnimationId ?? Guid.Empty;
            PacketSender.SendAnimationToProximity(
                animation, -1, Id, enemy.MapId, (byte)enemy.X, (byte)enemy.Y, (sbyte)projectileDir, MapInstanceId
            );

            if (descriptor.Knockback > 0 && projectileDir < 4 && !enemy.IsImmuneTo(Immunities.Knockback) && !StatusActive(StatusTypes.Steady))
            {
                if (!(enemy is AttackingEntity) || enemy.IsDead() || enemy.IsDisposed)
                {
                    return;
                }

                ((AttackingEntity)enemy).Knockback(projectileDir, descriptor.Knockback);
            }
        }

        /// <summary>
        /// Determines whether or not an entity should even be allowed to begin a melee action
        /// </summary>
        /// <returns>True if the player can initiate a melee attack</returns>
        public virtual bool MeleeAvailable()
        {
            if (AttackTimer > Timing.Global.Milliseconds)
            {
                return false;
            }

            if (CastTime > Timing.Global.Milliseconds)
            {
                return false;
            }

            return true;
        }

        public virtual bool CanRangeTarget(Entity target)
        {
            if (target == null || target is EventPageInstance)
            {
                return false;
            }

            if (IsInvalidTauntTarget(target))
            {
                PacketSender.SendActionMsg(this, Strings.Combat.miss, CustomColors.Combat.Missed);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether or not an entity can do a melee attack to another entity
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual bool CanMeleeTarget(Entity target)
        {
            if (target == null || target is EventPageInstance)
            {
                return false;
            }

            if (IsInvalidTauntTarget(target))
            {
                PacketSender.SendActionMsg(this, Strings.Combat.miss, CustomColors.Combat.Missed);

                return false;
            }

            // "attempt" an attack if status active that prevents it, but miss 100% of the time
            if (IsOneBlockAway(target) && StatusPreventsAttack())
            {
                PacketSender.SendActionMsg(this, Strings.Combat.miss, CustomColors.Combat.Missed);
                PacketSender.SendEntityAttack(this, CalculateAttackTime());

                return false;
            }

            return true;
        }

        public void IncrementAttackTimer()
        {
            AttackTimer = Timing.Global.Milliseconds + CalculateAttackTime();
        }

        public virtual bool IsCriticalHit(int critChance)
        {
            if (StatusActive(StatusTypes.Accurate))
            {
                critChance *= Options.Instance.CombatOpts.AccurateCritChanceMultiplier;
            }

            if (Randomization.Next(1, 101) > critChance)
            {
                return false;
            }

            return true;
        }

        public void Knockback(byte dir, int amount)
        {
            _ = new Dash(this, amount, dir, false, false, false, false);
        }

        protected TileHelper GetMeleeAttackTile()
        {
            var attackingTile = new TileHelper(MapId, X, Y);
            switch (Dir)
            {
                case 0:
                    attackingTile.Translate(0, -1);

                    break;

                case 1:
                    attackingTile.Translate(0, 1);

                    break;

                case 2:
                    attackingTile.Translate(-1, 0);

                    break;

                case 3:
                    attackingTile.Translate(1, 0);

                    break;
            }

            return attackingTile;
        }

        public virtual bool TryDealDamageTo(Entity enemy,
            List<AttackTypes> attackTypes,
            int dmgScaling,
            double critMultiplier,
            ItemBase weapon,
            SpellBase spell,
            out int damage)
        {
            damage = 0;
            if (enemy == null || enemy.IsDisposed || enemy.IsDead())
            {
                return false;
            }

            // Invulnerable?
            if (enemy.CachedStatuses.Any(status => status.Type == StatusTypes.Invulnerable))
            {
                // TODO message
                PacketSender.SendActionMsg(enemy, Strings.Combat.invulnerable, CustomColors.Combat.Invulnerable, Options.BlockSound);
                return false;
            }

            // Enemy doesn't have any health
            if (!enemy.HasVital(Vitals.Health))
            {
                return false;
            }

            DealDamageTo(enemy, attackTypes, dmgScaling, critMultiplier, weapon, out damage);
            return true;
        }

        private void DealDamageTo(Entity enemy,
            List<AttackTypes> damageTypes,
            int scaling,
            double critMultiplier,
            ItemBase weaponMetadata,
            out int damage)
        {
            // TODO secondary damage?
            damage = 0;
            if (enemy == null)
            {
                return;
            }

            UpdateCombatTimers(this, enemy);

            damage = Formulas.CalculateDamageMAO(damageTypes, critMultiplier, scaling, this, enemy);

            if (damage != 0)
            {
                if (weaponMetadata != null && weaponMetadata != null)
                {
                    damage = CalculateSpecialDamage(damage, weaponMetadata, enemy);
                }
                SendCombatEffects(enemy, critMultiplier > 1, damage);

                PacketSender.SendCombatNumber(DetermineCombatNumberType(damage, false, false, critMultiplier), enemy, damage);
                enemy.TakeDamage(this, damage);
            }
        }

        protected static CombatNumberType DetermineCombatNumberType(int damage, bool isSecondary, bool isNeutral, double critMultiplier)
        {
            if (isNeutral)
            {
                return CombatNumberType.Neutral;
            }
            if (critMultiplier > 1.0f && damage > 0)
            {
                return CombatNumberType.DamageCritical;
            }
            if (!isSecondary)
            {
                if (damage >= 0)
                {
                    return CombatNumberType.DamageHealth;
                }
                else
                {
                    return CombatNumberType.HealHealth;
                }
            }
            else
            {
                if (damage >= 0)
                {
                    return CombatNumberType.DamageMana;
                }
                else
                {
                    return CombatNumberType.HealMana;
                }
            }
        }

        public static void UpdateCombatTimers(Entity attacker, Entity defender)
        {
            if (attacker != null)
            {
                attacker.CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
            }
            if (defender != null)
            {
                defender.CombatTimer = Timing.Global.Milliseconds + Options.CombatTime;
            }
        }
    }
}
