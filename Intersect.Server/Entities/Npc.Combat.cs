using Intersect.Enums;
using Intersect.Server.Database;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public partial class Npc : Entity, AttackingEntity
    {
        public override void TakeDamage(Entity attacker, int damage)
        {
            AddToDamageAndLootMaps(attacker, damage);
            base.TakeDamage(attacker, damage);
        }

        public override bool CanMeleeTarget(Entity target)
        {
            if (!IsOneBlockAway(target) ||
                !IsFacingTarget(target))
            {
                return false;
            }

            return base.CanMeleeTarget(target);
        }

        protected virtual bool TryDealDamageTo(Entity enemy,
                    List<AttackTypes> attackTypes,
                    int dmgScaling,
                    double critMultiplier,
                    Item weapon,
                    out int damage)
        {
            damage = 0;
            if (enemy == null)
            {
                return false;
            }

            critMultiplier = 1.0; // override - determined by item or spell
            if (IsCriticalHit(Base.CritChance))
            {
                critMultiplier = Base.CritMultiplier;
            }
            
            SendAttackAnimation(enemy);
            return base.TryDealDamageTo(enemy, attackTypes, dmgScaling, critMultiplier, weapon, out damage);
        }

        public void MeleeAttack(Entity enemy, bool ignoreEvasion)
        {
            var spellOverride = Base?.SpellAttackOverrideId ?? default;
            if (spellOverride != default)
            {
                if (CanCastSpell(spellOverride, enemy) && Timing.Global.MillisecondsUtc > mLastOverrideAttack)
                {
                    CastSpell(spellOverride);
                    mLastOverrideAttack = Timing.Global.MillisecondsUtc + CalculateAttackTime();
                }

                return;
            }

            if (!MeleeAvailable())
            {
                return;
            }

            IncrementAttackTimer();
            List<AttackTypes> attackTypes = new List<AttackTypes>(Base.AttackTypes);
            
            // Attack literally misses
            if (!CanMeleeTarget(enemy))
            {
                return;
            }

            if (!TryDealDamageTo(enemy, attackTypes, 100, 1.0, null, out int damage))
            {
                return;
            }
        }

        public void SendAttackAnimation(Entity enemy)
        {
            PacketSender.SendEntityAttack(this, CalculateAttackTime());
            if (Base.AttackAnimation == null)
            {
                return;
            }
            
            PacketSender.SendAnimationToProximity(
                Base.AttackAnimationId, -1, Guid.Empty, enemy.MapId, (byte)enemy.X, (byte)enemy.Y,
                (sbyte)Dir, enemy.MapInstanceId
            );
        }
    }
}
