using System;
using System.Collections.Generic;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.General;
using Intersect.Utilities;

namespace Intersect.Server.Entities.Combat
{

    public partial class DoT
    {
        public Guid Id = Guid.NewGuid();

        public AttackingEntity Attacker;

        public int Count;

        private long mInterval;

        public SpellBase SpellBase;

        public DoT(AttackingEntity attacker, Guid spellId, Entity target)
        {
            SpellBase = SpellBase.Get(spellId);

            Attacker = attacker;
            Target = target;

            if (SpellBase == null || SpellBase.Combat.HotDotInterval < 1)
            {
                return;
            }

            // Does target have a cleanse buff? If so, do not allow this DoT when spell is unfriendly.
            if (!SpellBase.Combat.Friendly)
            {
                foreach (var status in Target.CachedStatuses)
                {
                    if (status.Type == StatusTypes.Cleanse)
                    {
                        return;
                    }
                }
            }
            

            mInterval = Timing.Global.Milliseconds + SpellBase.Combat.HotDotInterval;
            Count = SpellBase.Combat.Duration / SpellBase.Combat.HotDotInterval - 1;
            target.DoT.TryAdd(Id, this);
            target.CachedDots = target.DoT.Values.ToArray();

            //Subtract 1 since the first tick always occurs when the spell is cast.
        }

        public Entity Target { get; }

        public void Expire()
        {
            if (Target != null)
            {
                Target.DoT?.TryRemove(Id, out DoT val);
                Target.CachedDots = Target.DoT?.Values.ToArray() ?? new DoT[0];
            }
        }

        public bool CheckExpired()
        {
            if (Target != null && !Target.DoT.ContainsKey(Id))
            {
                return false;
            }

            if (SpellBase == null || Count > 0)
            {
                return false;
            }

            Expire();

            return true;
        }

        public void Tick()
        {
            if (CheckExpired())
            {
                return;
            }

            if (mInterval > Timing.Global.Milliseconds)
            {
                return;
            }

            var deadAnimations = new List<KeyValuePair<Guid, sbyte>>();
            var aliveAnimations = new List<KeyValuePair<Guid, sbyte>>();
            if (SpellBase.OverTimeAnimationId != Guid.Empty)
            {
                deadAnimations.Add(new KeyValuePair<Guid, sbyte>(SpellBase.OverTimeAnimationId, (sbyte)Directions.Up));
                aliveAnimations.Add(new KeyValuePair<Guid, sbyte>(SpellBase.OverTimeAnimationId, (sbyte)Directions.Up));
            } else if (SpellBase.HitAnimationId != Guid.Empty)
            {
                deadAnimations.Add(new KeyValuePair<Guid, sbyte>(SpellBase.HitAnimationId, (sbyte)Directions.Up));
                aliveAnimations.Add(new KeyValuePair<Guid, sbyte>(SpellBase.HitAnimationId, (sbyte)Directions.Up));
            }
            

            var damageHealth = SpellBase.Combat.VitalDiff[(int)Vitals.Health];
            var damageMana = SpellBase.Combat.VitalDiff[(int)Vitals.Mana];

            mInterval = Timing.Global.Milliseconds + SpellBase.Combat.HotDotInterval;
            Count--;

            var attackTypes = SpellBase.Combat?.DamageTypes;
            var scaling = SpellBase.Combat?.Scaling ?? 100;


            Attacker.SpellDoTAnimation(SpellBase, Target, (sbyte)Directions.Up);
            if (!Attacker.TryDealDamageTo(Target, attackTypes, scaling, 1.0, null, SpellBase, out int damage))
            {
                // Do somethin
            }
        }

    }

}
