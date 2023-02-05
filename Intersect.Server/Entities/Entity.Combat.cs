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
        public int Accuracy => Stat?.ElementAtOrDefault((int)Stats.Accuracy)?.Value() ?? 0;
        public int Evasion => Stat?.ElementAtOrDefault((int)Stats.Evasion)?.Value() ?? 0;

        public bool IsStealthed => CachedStatuses.ToArray().Select(status => status.Type).Contains(StatusTypes.Stealth);

        public virtual void Unstealth()
        {
            foreach(var cachedStatus in CachedStatuses.Where(status => status.Type == StatusTypes.Stealth))
            {
                cachedStatus.RemoveStatus();
            }
        }

        public bool CanHaveVitalDamaged(Vitals vital)
        {
            if (this == null || IsDisposed || IsDead())
            {
                return false;
            }

            // Enemy doesn't have any health
            if (!HasVital(vital))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Things that should happen TO some entity when being attacked FROM some entity
        /// </summary>
        /// <param name="attacker">The attacking entity dealing damage to this entity</param>
        /// <param name="damage">The amount of damage to take</param>
        /// <param name="vital">The affected vital</param>
        public virtual void TakeDamage(Entity attacker, int damage, Vitals vital = Vitals.Health)
        {
            if (damage > 0)
            {
                RemoveStatusesOnDamage();
            }

            if (damage > 0)
            {
                SubVital(vital, damage);
            }
            else if (!IsFullVital(vital))
            {
                AddVital(vital, damage * -1);
            }

            // You dead?
            if (Health <= 0)
            {
                lock (EntityLock)
                {
                    Die(true, attacker);
                }
            }
        }

        public virtual void ReactToCombat(Entity attacker)
        {

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

        private enum DamageBonus
        {
            None = 0,
            Backstab,
            Stealth
        };

        public int CalculateSpecialDamage(int baseDamage, ItemBase item, Entity target)
        {
            if (target is Resource) return baseDamage;
            if (item == null || target == null) return baseDamage;

            var canBackstab = true;
            var canStealth = true;
            if (target is Npc npc)
            {
                canBackstab = !npc?.Base?.NoBackstab ?? true;
                canStealth = !npc?.Base?.NoStealthBonus ?? true;
            }

            var damageBonus = DamageBonus.None;
            if (target.Dir == Dir) // Player is hitting something from behind
            {
                if (item.CanBackstab && canBackstab)
                {
                    baseDamage = (int)Math.Floor(baseDamage * item.BackstabMultiplier);
                    damageBonus = DamageBonus.Backstab;
                }
                if (this is Player player && player.StealthAttack && item.ProjectileId == Guid.Empty && canStealth) // Melee weapons only for stealth attacks
                {
                    baseDamage += player.CalculateStealthDamage(baseDamage, item);
                    damageBonus = DamageBonus.Stealth;
                }

                if (damageBonus == DamageBonus.Backstab)
                {
                    PacketSender.SendActionMsg(target, Strings.Combat.backstab, CustomColors.Combat.Backstab);
                }
                else if (damageBonus == DamageBonus.Stealth)
                {
                    PacketSender.SendActionMsg(target, Strings.Combat.stealthattack, CustomColors.Combat.Backstab);
                }
            }

            return baseDamage;
        }
    }
}
