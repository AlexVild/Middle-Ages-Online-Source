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
                HealVital(vital, damage * -1);
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

        public virtual void HealVital(Vitals vital, int amount)
        {
            AddVital(vital, amount);
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

        protected enum DamageBonus
        {
            None = 0,
            Backstab,
            Stealth
        };

        public virtual int CalculateSpecialDamage(int baseDamage, int range, ItemBase item, Entity target)
        {
            return baseDamage;
        }

        public virtual void ApplyStatus(SpellBase spell, Entity caster, int statBuffTime)
        {
            if (spell.Combat.Effect > 0) //Handle status effects
            {
                // If the entity is immune to some status, then just inform the client of such
                if (IsImmuneTo(StatusToImmunity(spell.Combat.Effect)))
                {
                    PacketSender.SendActionMsg(this, Strings.Combat.immunetoeffect, CustomColors.Combat.Status);
                }
                else if (spell.Combat.Friendly || !IsInvincibleTo(caster))
                {
                    // Else, apply the status
                    _ = new Status(
                        this, caster, spell, spell.Combat.Effect, spell.Combat.Duration,
                        spell.Combat.TransformSprite
                    );

                    PacketSender.SendActionMsg(
                        this, Strings.Combat.status[(int)spell.Combat.Effect], CustomColors.Combat.Status
                    );
                }
            }
            // Otherwise, add a status for the stat boost
            else
            {
                new Status(this, caster, spell, spell.Combat.Effect, statBuffTime, "");
            }

            if (!spell.Combat.Friendly)
            {
                ReactToCombat(caster);
            }
        }

        public virtual int GetBonusEffectTotal(EffectType effect, int startValue = 0)
        {
            return 0;
        }

        public virtual float GetBonusEffectPercent(EffectType effect, bool additive, int startValue = 0)
        {
            return 1f;
        }

        public int ApplyBonusEffectInt(int value, EffectType effect, bool additive, int startValue = 0)
        {
            var percent = GetBonusEffectPercent(effect, additive, startValue);
            return (int)Math.Ceiling(value * percent);
        }
    }
}
