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
    public abstract class AttackingEntity : Entity
    {
        public abstract void MeleeAttack(Entity enemy, bool ignoreEvasion);

        public abstract void SendAttackAnimation(Entity enemy);

        public abstract void ProjectileAttack(Entity enemy, 
            Projectile projectile, 
            SpellBase parentSpell, 
            ItemBase parentWeapon, 
            byte projectileDir);

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

        protected virtual bool TryDealDamageTo(Entity enemy,
            List<AttackTypes> attackTypes,
            int dmgScaling,
            double critMultiplier,
            ItemBase weapon,
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

            if (damage > 0)
            {
                if (weaponMetadata != null && weaponMetadata != null)
                {
                    damage = CalculateSpecialDamage(damage, weaponMetadata, enemy);
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

        #region spells
        public virtual void CastSpell(Guid spellId, int spellSlot = -1, bool prayerSpell = false, Entity prayerTarget = null, int prayerSpellDir = -1)
        {
            var spellBase = SpellBase.Get(spellId);
            if (spellBase == null)
            {
                return;
            }

            if (!CanCastSpell(spellBase, CastTarget))
            {
                if (this is Player)
                {
                    PacketSender.SendChatMsg((Player)this, "You could not cast the spell - the target may have moved out of range.", ChatMessageType.Spells, CustomColors.General.GeneralWarning);
                }
                SendMissedAttackMessage(this, DamageType.True);
                return;
            }

            if (spellBase.VitalCost[(int)Vitals.Mana] > 0)
            {
                if (!prayerSpell) // prayer spells dont cost anything
                {
                    SubVital(Vitals.Mana, spellBase.VitalCost[(int)Vitals.Mana]);
                }
            }
            else
            {
                AddVital(Vitals.Mana, -spellBase.VitalCost[(int)Vitals.Mana]);
            }

            if (spellBase.VitalCost[(int)Vitals.Health] > 0)
            {
                if (!prayerSpell) // prayer spells dont cost anything
                {
                    SubVital(Vitals.Health, spellBase.VitalCost[(int)Vitals.Health]);
                }
            }
            else
            {
                AddVital(Vitals.Health, -spellBase.VitalCost[(int)Vitals.Health]);
            }

            switch (spellBase.SpellType)
            {
                case SpellTypes.CombatSpell:
                case SpellTypes.Event:

                    switch (spellBase.Combat.TargetType)
                    {
                        case SpellTargetTypes.Self:
                            if (spellBase.HitAnimationId != Guid.Empty && spellBase.Combat.Effect != StatusTypes.OnHit)
                            {
                                PacketSender.SendAnimationToProximity(
                                    spellBase.HitAnimationId, 1, Id, MapId, 0, 0, (sbyte)Dir, MapInstanceId
                                ); //Target Type 1 will be global entity
                            }

                            TryAttackSpell(this, spellBase, out bool miss, out bool blocked);

                            break;
                        case SpellTargetTypes.Single:
                            if (CastTarget == null || prayerSpell)
                            {
                                return;
                            }

                            //If target has stealthed we cannot hit the spell.
                            foreach (var status in CastTarget.CachedStatuses)
                            {
                                if (status.Type == StatusTypes.Stealth)
                                {
                                    return;
                                }
                            }

                            if (spellBase.Combat.HitRadius > 0) //Single target spells with AoE hit radius'
                            {
                                HandleAoESpell(
                                    spellId, spellBase.Combat.HitRadius, CastTarget.MapId, CastTarget.X, CastTarget.Y,
                                    null, false, false, true
                                );
                            }
                            else
                            {
                                TryAttackSpell(CastTarget, spellBase, out bool spellMissed, out bool spellBlocked);
                            }

                            break;
                        case SpellTargetTypes.AoE:
                            if (prayerSpell) return;
                            HandleAoESpell(spellId, spellBase.Combat.HitRadius, MapId, X, Y, null);
                            break;
                        case SpellTargetTypes.Projectile:
                            var projectileBase = spellBase.Combat.Projectile;
                            if (projectileBase != null)
                            {
                                if (this is Player player)
                                {
                                    PacketSender.SendProjectileCastDelayPacket(player, Options.Instance.CombatOpts.ProjectileSpellMovementDelay);
                                }

                                if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
                                {
                                    if (prayerSpell && prayerTarget != null && prayerSpellDir >= 0)
                                    {
                                        mapInstance.SpawnMapProjectile(
                                                this, projectileBase, spellBase, null, prayerTarget.MapId, (byte)prayerTarget.X, (byte)prayerTarget.Y, (byte)prayerTarget.Z,
                                                (byte)prayerSpellDir, CastTarget
                                            );
                                    }
                                    else
                                    {
                                        mapInstance.SpawnMapProjectile(
                                            this, projectileBase, spellBase, null, MapId, (byte)X, (byte)Y, (byte)Z,
                                            (byte)Dir, CastTarget
                                        );
                                    }

                                    if (spellBase.TrapAnimationId != Guid.Empty)
                                    {
                                        PacketSender.SendAnimationToProximity(spellBase.TrapAnimationId, -1, Guid.Empty, MapId, (byte)X, (byte)Y, (sbyte)Dir, MapInstanceId);
                                    }
                                }
                            }

                            break;
                        case SpellTargetTypes.OnHit:
                            if (spellBase.Combat.Effect == StatusTypes.OnHit)
                            {
                                new Status(
                                    this, this, spellBase, StatusTypes.OnHit, spellBase.Combat.OnHitDuration,
                                    spellBase.Combat.TransformSprite
                                );

                                PacketSender.SendActionMsg(
                                    this, Strings.Combat.status[(int)spellBase.Combat.Effect],
                                    CustomColors.Combat.Status
                                );
                            }

                            break;
                        case SpellTargetTypes.Trap:
                            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
                            {
                                instance.SpawnTrap(this, spellBase, (byte)X, (byte)Y, (byte)Z);
                            }

                            break;
                        default:
                            break;
                    }

                    break;
                case SpellTypes.Warp:
                    if (this is Player)
                    {
                        Warp(
                            spellBase.Warp.MapId, spellBase.Warp.X, spellBase.Warp.Y,
                            spellBase.Warp.Dir - 1 == -1 ? (byte)this.Dir : (byte)(spellBase.Warp.Dir - 1)
                        );
                    }

                    break;
                case SpellTypes.WarpTo:
                    if (CastTarget != null)
                    {
                        HandleAoESpell(spellId, spellBase.Combat.CastRange, MapId, X, Y, CastTarget, false, false, true);
                    }
                    break;
                case SpellTypes.Dash:
                    PacketSender.SendActionMsg(this, Strings.Combat.dash, CustomColors.Combat.Dash);
                    var dash = new Dash(
                        this, spellBase.Combat.CastRange, (byte)Dir, Convert.ToBoolean(spellBase.Dash.IgnoreMapBlocks),
                        Convert.ToBoolean(spellBase.Dash.IgnoreActiveResources),
                        Convert.ToBoolean(spellBase.Dash.IgnoreInactiveResources),
                        Convert.ToBoolean(spellBase.Dash.IgnoreZDimensionAttributes),
                        Convert.ToBoolean(spellBase.Dash.IgnoreEntites),
                        spellBase.Dash.Spell
                    );

                    break;
                default:
                    break;
            }

            if (spellSlot >= 0 && spellSlot < Options.MaxPlayerSkills)
            {
                // Player cooldown handling is done elsewhere!
                if (this is Player player)
                {
                    player.UpdateCooldown(spellBase);

                    // Trigger the global cooldown, if we're allowed to.
                    if (!spellBase.IgnoreGlobalCooldown)
                    {
                        player.UpdateGlobalCooldown();
                    }
                }
                else
                {
                    SpellCooldowns[Spells[spellSlot].SpellId] =
                    Timing.Global.MillisecondsUtc + (int)(spellBase.CooldownDuration);
                }
            }

            if (GetVital(Vitals.Health) <= 0) // if the spell has killed the entity
            {
                Die();
            }
        }
        #endregion
    }
}
