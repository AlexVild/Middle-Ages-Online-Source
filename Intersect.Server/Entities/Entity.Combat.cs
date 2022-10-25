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

        protected bool StatusPreventsAttack()
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

        protected bool IsInvalidTauntTarget(Entity target)
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
