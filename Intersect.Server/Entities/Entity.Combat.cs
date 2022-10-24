using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
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
    public partial class Entity : IDisposable
    {
        public int Health => GetVital(Vitals.Health);
        public int Mana => GetVital(Vitals.Mana);


        protected bool IsStealthed => CachedStatuses.ToArray().Select(status => status.Type).Contains(StatusTypes.Stealth);

        protected void Unstealth()
        {
            foreach(var cachedStatus in CachedStatuses.Where(status => status.Type == StatusTypes.Stealth))
            {
                cachedStatus.RemoveStatus();
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

        protected virtual bool TryDealDamageTo(Entity enemy, 
            List<AttackTypes> attackTypes, 
            int dmgScaling, 
            double critMultiplier, 
            Item weapon, 
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
            Item weaponMetadata,
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

            if (damage > 0)
            {
                if (weaponMetadata != null && weaponMetadata.Descriptor != null)
                {
                    damage = CalculateSpecialDamage(damage, weaponMetadata.Descriptor, enemy);
                }
                SendCombatEffects(enemy, critMultiplier > 1, damage);
                enemy.TakeDamage(this, damage);
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

        /// <summary>
        /// Things that should happen TO some entity when being attacked FROM some entity
        /// </summary>
        /// <param name="attacker">The attacking entity dealing damage to this entity</param>
        /// <param name="damage">The amount of damage to take</param>
        public virtual void TakeDamage(Entity attacker, int damage)
        {
            RemoveStatusesOnDamage();
            NotifySwarm(attacker);

            if (damage > 0)
            {
                SubVital(Vitals.Health, damage);
            }
            else if (!IsFullVital(Vitals.Health))
            {
                AddVital(Vitals.Health, damage);
            }

            // You dead?
            if (Health <= 0)
            {
                lock (EntityLock)
                {
                    Die(true, attacker);
                }
                /* TODO this?
                if (deadAnimations != null)
                {
                    foreach (var anim in deadAnimations)
                    {
                        PacketSender.SendAnimationToProximity(
                            anim.Key, -1, Id, enemy.MapId, (byte)enemy.X, (byte)enemy.Y, anim.Value, MapInstanceId
                        );
                    }
                }*/
            }
            // Nah, but you need to be mad
            else
            {
                // TODO this
                /*if (aliveAnimations?.Count > 0)
                {
                    Animate(enemy, aliveAnimations, fromProjectile);
                }*/

                // TODO wtf is "isAutoAttack"?
                //attacker.CheckForOnhitAttack(this, isAutoAttack);
            }
        }

        private void RemoveStatusesOnDamage()
        {
            foreach (var status in CachedStatuses.ToArray())  // ToArray the Array since removing a status will.. you know, change the collection.
            {
                //Wake up any sleeping targets targets and take stealthed entities out of stealth
                if (status.Type == StatusTypes.Sleep || status.Type == StatusTypes.Stealth)
                {
                    status.RemoveStatus();
                }
            }
        }

        private bool StatusPreventsAttack()
        {
            return CachedStatuses
                .ToArray()
                .Select(status => status.Type)
                .Any(statusType =>
                {
                    return statusType == StatusTypes.Taunt ||
                           statusType == StatusTypes.Stun ||
                           statusType == StatusTypes.Blind ||
                           statusType == StatusTypes.Sleep;
                });
        }

        private bool IsInvalidTauntTarget(Entity target)
        {
            if (!CachedStatuses.ToArray().Select(status => status.Type).Contains(StatusTypes.Taunt))
            {
                return false;
            }

            if (target == null)
            {
                return false;
            }

            if (Target != target)
            {
                return true;
            }

            return false;
        }
    }
}
