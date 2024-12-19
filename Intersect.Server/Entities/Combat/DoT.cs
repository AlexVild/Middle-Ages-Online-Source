using System;
using System.Collections.Generic;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Entities.PlayerData;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
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

            mInterval = Timing.Global.Milliseconds + SpellBase.Combat.HotDotInterval;
            Count--;

            var attackTypes = SpellBase.Combat?.DamageTypes;
            var scaling = SpellBase.Combat?.Scaling ?? 100;

            var damageDealt = 0;
            if (Attacker is Player playerAttacker)
            {
                if (Target is Npc npc && !npc.CanPlayerAttack(playerAttacker))
                {
                    PacketSender.SendActionMsg(npc, Strings.Combat.invulnerable, CustomColors.Combat.Invulnerable, Options.BlockSound);
                    return;
                }

                playerAttacker.TryGetEquippedItem(Options.WeaponIndex, out var weapon);
                Attacker.TryDealDamageTo(Target, attackTypes, scaling, 1.0, weapon?.Descriptor, SpellBase, true, 1, out damageDealt);

                if (!SpellBase.Combat.Friendly && SpellBase.Combat.VitalDiff[(int)Vitals.Mana] == 0 && Target.ValidForChallenges)
                {
                    ChallengeUpdateProcesser.UpdateChallengesOf(new DoTUpdate(playerAttacker, damageDealt));
                }
            }
            else
            {
                Attacker.TryDealDamageTo(Target, attackTypes, scaling, 1.0, null, SpellBase, true, 1, out damageDealt);
            }

            if (SpellBase.Combat?.LifeSteal ?? false && Math.Abs(damageDealt) > 0)
            {
                Attacker.SpellLifesteal(damageDealt);
            }
            if (SpellBase.Combat?.ManaSteal ?? false && SpellBase.Combat.VitalDiff[(int)Vitals.Mana] > 0)
            {
                Attacker.SpellManasteal(damageDealt);
            }

            SendDoTAnimation(SpellBase, Target, (sbyte)Directions.Up);
        }

        private void SendDoTAnimation(SpellBase spell, Entity target, sbyte dir)
        {
            if (target == null || spell == null)
            {
                return;
            }

            var animation = Guid.Empty;
            if (spell.OverTimeAnimationId != Guid.Empty)
            {
                animation = spell.OverTimeAnimationId;
            }
            else
            {
                animation = spell.HitAnimationId;
            }

            var targetType = (target.IsDead() || target.IsDisposed) ? -1 : 1;

            PacketSender.SendAnimationToProximity(
                animation, targetType, target.Id, target.MapId, (byte)target.X, (byte)target.Y, (sbyte)dir, target.MapInstanceId
            );
        }

        /// <summary>
        /// Applies a spell's DoT
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        public static void AddSpellDoTsTo(SpellBase spell, AttackingEntity attacker, Entity target)
        {
            if (spell == null || attacker == null || target == null)
            {
                return;
            }

            if (spell.Combat.HoTDoT)
            {
                WipeDoTsFromTarget(spell.Id, attacker, target);

                _ = new DoT(attacker, spell.Id, target);
            }
        }

        /// <summary>
        /// Wipes all instances of a DoT that share the same properties of the given
        /// </summary>
        /// <param name="spellId"></param>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        private static void WipeDoTsFromTarget(Guid spellId, Entity attacker, Entity target)
        {
            target.CachedDots.ToList()
                    .FindAll((DoT dot) => dot.SpellBase.Id == spellId && dot.Attacker == attacker)
                    .ForEach((DoT dot) => dot.Expire());
        }
    }

}
