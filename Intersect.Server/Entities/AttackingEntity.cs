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
        protected bool IsStunnedOrSleeping => CachedStatuses.Any(PredicateStunnedOrSleeping);

        protected bool IsUnableToCastSpells => CachedStatuses.Any(PredicateUnableToCastSpells);

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

        private static bool PredicateUnableToCastSpells(Status status)
        {
            switch (status?.Type)
            {
                case StatusTypes.Silence:
                case StatusTypes.Sleep:
                case StatusTypes.Stun:
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
                AttackSpell(enemy, parentSpell, (sbyte)projectile.Dir, projectile);
            }
            // Otherwise, handle the weapon
            else if (parentWeapon != null)
            {
                if (!TryDealDamageTo(enemy, parentWeapon.AttackTypes, 100, 1.0, parentWeapon, null, out int weaponDamage))
                {
                    return;
                }
            }

            PacketSender.SendAnimationToProximity(
                parentWeapon?.AttackAnimationId ?? Guid.Empty, -1, Id, enemy.MapId, (byte)enemy.X, (byte)enemy.Y, (sbyte)projectileDir, MapInstanceId
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

        protected abstract bool EntityMeetsCastingRequirements(SpellBase spell);

        protected abstract bool EntityHasCastingMaterials(SpellBase spell);

        protected abstract bool TryConsumeCastingMaterials(SpellBase spell);

        public virtual bool CanCastSpell(SpellBase spell, Entity target)
        {
            if (!MeetsSpellVitalReqs(spell))
            {
                // Not enough vitals!
                return false;
            }

            if (spell.Combat != null &&
                SpellTypeHelpers.SpellTypeRequiresTarget(spell.Combat.TargetType) &&
                !CanAttack(target, spell))
            {
                // Spell requires valid target!
                return false;
            }

            if (SpellTypeHelpers.IsMovementSpell(spell.SpellType) && IsImmobile)
            {
                // Spell requires mobility!
                return false;
            }

            if (!EntityMeetsCastingRequirements(spell))
            {
                // The entity doesn't have extraneous requirements met!
                return false;
            }

            return true;
        }

        public virtual bool CanStartCast(long now, int spellSlot, Entity target)
        {
            // Invalid params
            if (spellSlot < 0 || spellSlot >= Spells.Count)
            {
                return false;
            }

            var spellId = Spells[spellSlot].SpellId;
            Target = target;
            var spell = SpellBase.Get(spellId);

            if (spell == null)
            {
                // No spell!
                return false;
            }

            if (CastTime != 0)
            {
                // Currently casting!
                return false;
            }

            if (SpellCooldowns.ContainsKey(spellId) && SpellCooldowns[spellId] >= Timing.Global.MillisecondsUtc)
            {
                // On cooldown!
                return false;
            }

            if (!CanCastSpell(spell, target))
            {
                return false;
            }

            return true;
        }

        public abstract bool MeetsSpellVitalReqs(SpellBase spell);

        public virtual void StartCast(int spellSlot, Entity target)
        {
            if (spellSlot < 0 && spellSlot >= Spells.Count)
            {
                return;
            }
            SpellCastSlot = spellSlot;
            var spell = SpellBase.Get(Spells[spellSlot].SpellId);

            StartCast(spell, target, spell.CastDuration <= 0);
        }

        public virtual void StartCast(SpellBase spell, Entity target, bool instant = false) 
        {
            if (spell == null)
            {
                return;
            }
            
            CastTime = Timing.Global.Milliseconds + spell.CastDuration;
            Target = target;
            Unstealth();

            if (spell.CastAnimationId != Guid.Empty)
            {
                PacketSender.SendAnimationToProximity(
                    spell.CastAnimationId, 1, base.Id, MapId, 0, 0, (sbyte)Dir, MapInstanceId
                );
            }

            if (instant)
            {
                UseSpell(spell, SpellCastSlot);
                CastingFinished();
            }
            else
            {
                PacketSender.SendEntityCastTime(this, spell.Id);
            }
        }

        public virtual void CancelCast() 
        {
            CastTime = 0;
            CastTarget = null;
            SpellCastSlot = -1;
            PacketSender.SendEntityCancelCast(this);
        }

        public virtual void CastingFinished() 
        {
            CastTime = 0;
            CastTarget = null;
            SpellCastSlot = -1;
        }

        private bool ValidateCast(SpellBase spell, Entity target, bool ignoreVitals) 
        { 
            if (spell == null)
            {
                return false;
            }

            if (!CanAttack(target, spell))
            {
                return false;
            }

            if (!TryConsumeCastingMaterials(spell))
            {
                return false;
            }

            if (!ignoreVitals)
            {
                if (!MeetsSpellVitalReqs(spell))
                {
                    return false;
                }
                HandleSpellVitalUpdate(spell);
            }

            return true;
        }
        
        private void HandleSpellVitalUpdate(SpellBase spell)
        {
            if (spell.VitalCost[(int)Vitals.Mana] > 0)
            {
                SubVital(Vitals.Mana, spell.VitalCost[(int)Vitals.Mana]);
            }
            else
            {
                AddVital(Vitals.Mana, -spell.VitalCost[(int)Vitals.Mana]);
            }

            if (spell.VitalCost[(int)Vitals.Health] > 0)
            {
                SubVital(Vitals.Health, spell.VitalCost[(int)Vitals.Health]);
            }
            else
            {
                AddVital(Vitals.Health, -spell.VitalCost[(int)Vitals.Health]);
            }
        }

        private void ApplySpellBuffsTo(SpellBase spell, Entity target)
        {
            var statBuffTime = -1;
            var expireTime = Timing.Global.Milliseconds + spell.Combat.Duration;
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                target.Stat[i]
                    .AddBuff(
                        new Buff(spell, spell.Combat.StatDiff[i], spell.Combat.PercentageStatDiff[i], expireTime)
                    );

                if (spell.Combat.StatDiff[i] != 0 || spell.Combat.PercentageStatDiff[i] != 0)
                {
                    statBuffTime = spell.Combat.Duration;
                }
            }

            if (statBuffTime == -1)
            {
                if (spell.Combat.HoTDoT && spell.Combat.HotDotInterval > 0)
                {
                    statBuffTime = spell.Combat.Duration;
                }
            }

            if (statBuffTime > -1)
            {
                if (!target.IsImmuneTo(StatusToImmunity(spell.Combat.Effect)))
                {
                    new Status(target, this, spell, spell.Combat.Effect, statBuffTime, "");
                }
                else
                {
                    PacketSender.SendActionMsg(target, Strings.Combat.immunetoeffect, CustomColors.Combat.Status);
                }
            }
        }
        
        public void ApplyDoT(SpellBase spell, Entity target)
        {
            if (spell.Combat.HoTDoT)
            {
                target.CachedDots.ToList()
                    .FindAll((DoT dot) => dot.SpellBase.Id == spell.Id && dot.Attacker == this)
                    .ForEach((DoT dot) => dot.Expire());

                new DoT(this, spell.Id, target);
            }
        }

        public void AttackSpell(Entity target, SpellBase spell, sbyte attackAnimDir, Projectile projectile)
        {
            if ((spell.Combat?.TargetType == SpellTargetTypes.AoE ||
                spell.Combat?.TargetType == SpellTargetTypes.Single) &&
                IsInvalidTauntTarget(target))
            {
                return;
            }

            ApplySpellBuffsTo(spell, target);
            ApplyDoT(spell, target);

            // Determine if we want to apply effects to otherwise invulnerable NPCs
            if (this is Player pl && target is Npc np && !pl.CanAttackNpc(np))
            {
                if (spell.Combat.IsDamaging)
                {
                    return;
                }
                else if (!Options.Instance.CombatOpts.InvulnerableNpcsAffectedByNonDamaging)
                {
                    return;
                }
            }

            var scaling = spell.Combat.Scaling;
            List<AttackTypes> attackTypes = new List<AttackTypes>(spell.Combat.DamageTypes);
            var critChance = spell.Combat.CritChance;
            var critMultiplier = spell.Combat.CritMultiplier;

            PopulateExtraSpellDamage(ref scaling, ref attackTypes, ref critChance, ref critMultiplier);

            int damage;
            if (this is Player player)
            {
                if (!TryDealDamageTo(target, attackTypes, scaling, critMultiplier, player.CastingWeapon, spell, out damage))
                {
                    return;
                }
            }
            else if (!TryDealDamageTo(target, attackTypes, scaling, critMultiplier, null, spell, out damage))
            {
                return;
            }
        }

        protected abstract void PopulateExtraSpellDamage(ref int scaling,
            ref List<AttackTypes> attackTypes,
            ref int critChance,
            ref double critMultiplier);

        public virtual void UseSpell(SpellBase spell, int spellSlot, bool ignoreVitals = false, bool prayerSpell = false, byte prayerSpellDir = 0, Entity prayerTarget = null) 
        {
            CastTarget = Target;
            // We're actually doing the spell now - use our mats and if we fail, end
            if (!ValidateCast(spell, CastTarget, ignoreVitals))
            {
                return;
            }

            switch (spell.SpellType)
            {
                case SpellTypes.CombatSpell:
                case SpellTypes.Event:

                    switch (spell.Combat.TargetType)
                    {
                        case SpellTargetTypes.Self:
                            if (spell.HitAnimationId != Guid.Empty && spell.Combat.Effect != StatusTypes.OnHit)
                            {
                                SpellHitAnimation(spell, this);
                            }
                            AttackSpell(this, spell, (sbyte)Dir, null);

                            break;
                        case SpellTargetTypes.Single:
                            if (CastTarget == null)
                            {
                                return;
                            }

                            //If target has stealthed we cannot hit the spell.
                            if (CastTarget is AttackingEntity castTarget && castTarget.IsStealthed)
                            {
                                return;
                            }

                            if (spell.Combat.HitRadius > 0) //Single target spells with AoE hit radius'
                            {
                                HandleAoESpell(
                                    spell.Id, spell.Combat.HitRadius, CastTarget.MapId, CastTarget.X, CastTarget.Y,
                                    null, false, false, true
                                );
                            }
                            else
                            {
                                AttackSpell(CastTarget, spell, (sbyte)Dir, null);
                                SpellHitAnimation(spell, CastTarget);
                            }

                            break;
                        case SpellTargetTypes.AoE:
                            HandleAoESpell(spell.Id, spell.Combat.HitRadius, MapId, X, Y, null);
                            break;
                        case SpellTargetTypes.Projectile:
                            var projectileBase = spell.Combat.Projectile;
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
                                                this, projectileBase, spell, null, prayerTarget.MapId, (byte)prayerTarget.X, (byte)prayerTarget.Y, (byte)prayerTarget.Z,
                                                prayerSpellDir, CastTarget
                                            );
                                    }
                                    else
                                    {
                                        mapInstance.SpawnMapProjectile(
                                            this, projectileBase, spell, null, MapId, (byte)X, (byte)Y, (byte)Z,
                                            (byte)Dir, CastTarget
                                        );
                                    }

                                    // Leveraging trap animation for projectile spawner animation
                                    if (spell.TrapAnimationId != Guid.Empty)
                                    {
                                        PacketSender.SendAnimationToProximity(spell.TrapAnimationId, -1, Guid.Empty, MapId, (byte)X, (byte)Y, (sbyte)Dir, MapInstanceId);
                                    }
                                }
                            }

                            break;
                        case SpellTargetTypes.OnHit:
                            if (spell.Combat.Effect == StatusTypes.OnHit)
                            {
                                new Status(
                                    this, this, spell, StatusTypes.OnHit, spell.Combat.OnHitDuration,
                                    spell.Combat.TransformSprite
                                );

                                PacketSender.SendActionMsg(
                                    this, Strings.Combat.status[(int)spell.Combat.Effect],
                                    CustomColors.Combat.Status
                                );
                                SpellHitAnimation(spell, this);
                            }

                            break;
                        case SpellTargetTypes.Trap:
                            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
                            {
                                instance.SpawnTrap(this, spell, (byte)X, (byte)Y, (byte)Z);
                            }

                            break;
                        default:
                            break;
                    }

                    break;
                case SpellTypes.Warp:
                    if (this is Player)
                    {
                        SpellHitAnimation(spell, this);
                        Warp(
                            spell.Warp.MapId, spell.Warp.X, spell.Warp.Y,
                            spell.Warp.Dir - 1 == -1 ? (byte)this.Dir : (byte)(spell.Warp.Dir - 1)
                        );
                    }

                    break;
                case SpellTypes.WarpTo:
                    if (CastTarget != null)
                    {
                        HandleAoESpell(spell.Id, spell.Combat.CastRange, MapId, X, Y, CastTarget, false, false, true);
                    }
                    break;
                case SpellTypes.Dash:
                    PacketSender.SendActionMsg(this, Strings.Combat.dash, CustomColors.Combat.Dash);
                    _ = new Dash(
                        this, spell.Combat.CastRange, (byte)Dir, Convert.ToBoolean(spell.Dash.IgnoreMapBlocks),
                        Convert.ToBoolean(spell.Dash.IgnoreActiveResources),
                        Convert.ToBoolean(spell.Dash.IgnoreInactiveResources),
                        Convert.ToBoolean(spell.Dash.IgnoreZDimensionAttributes),
                        Convert.ToBoolean(spell.Dash.IgnoreEntites),
                        spell.Dash.Spell
                    );

                    break;
                default:
                    break;
            }

            if (spellSlot >= 0 && spellSlot < Options.MaxPlayerSkills)
            {
                UpdateSpellCooldown(spellSlot);
            }

            if (GetVital(Vitals.Health) <= 0) // if the spell has killed the entity
            {
                Die();
            }
        }

        public override void CheckForSpellCast(long timeMs)
        {
            if (CastTime != 0 && CastTime < timeMs && SpellCastSlot < Spells.Count && SpellCastSlot >= 0)
            {
                var spell = SpellBase.Get(Spells[SpellCastSlot].SpellId);
                UseSpell(spell, SpellCastSlot);
                CastingFinished();
            }
        }

        public void HandleAoESpell(
            Guid spellId,
            int range,
            Guid startMapId,
            int startX,
            int startY,
            Entity spellTarget,
            bool ignoreEvasion = false,
            bool isProjectileTool = false,
            bool ignoreMissMessage = false
        )
        {
            var spellBase = SpellBase.Get(spellId);
            if (spellBase != null)
            {
                int entitiesHit = 0;
                var startMap = MapController.Get(startMapId);
                foreach (var instance in MapController.GetSurroundingMapInstances(startMapId, MapInstanceId, true))
                {
                    foreach (var entity in instance.GetCachedEntities())
                    {
                        if (entity == null || (!(entity is Player) && !(entity is Npc)))
                        {
                            continue;
                        }

                        if (spellTarget != null && spellTarget != entity)
                        {
                            continue;
                        }

                        if (entity.GetDistanceTo(startMap, startX, startY) > range)
                        {
                            continue;
                        }

                        //Check to handle a warp to spell
                        if (spellBase.SpellType == SpellTypes.WarpTo)
                        {
                            if (spellTarget != null)
                            {
                                //Spelltarget used to be Target. I don't know if this is correct or not.
                                int[] position = GetPositionNearTarget(spellTarget.MapId, spellTarget.X, spellTarget.Y, spellTarget.Dir);
                                Warp(spellTarget.MapId, (byte)position[0], (byte)position[1], (byte)Dir);
                                ChangeDir(DirToEnemy(spellTarget));
                            }
                        }

                        AttackSpell(entity, spellBase, (sbyte)Directions.Up, null); //Handle damage
                        SpellHitAnimation(spellBase, entity);
                        entitiesHit++;
                    }
                }
                if (!spellBase.Combat.Friendly && entitiesHit <= 1 && !isProjectileTool && !ignoreMissMessage) // Will count yourself - which is FINE in the case of a friendly spell, otherwise ignore it
                {
                    if (this is Player)
                    {
                        PacketSender.SendChatMsg((Player)this, "There weren't any targets in your spell's AoE range.", ChatMessageType.Spells, CustomColors.General.GeneralWarning);
                    }
                    SendMissedAttackMessage(this, DamageType.True);
                }
            }
        }

        public void SpellHitAnimation(SpellBase spell, Entity target, sbyte? dirOverride = null)
        {
            if (target == null)
            {
                return;
            }

            var dir = Dir;
            if (dirOverride.HasValue)
            {
                dir = dirOverride.Value;
            }

            PacketSender.SendAnimationToProximity(
                spell?.HitAnimationId ?? Guid.Empty, -1, Id, target.MapId, (byte)target.X, (byte)target.Y, (sbyte)dir, target.MapInstanceId
            );
        }

        public void SpellDoTAnimation(SpellBase spell, Entity target, sbyte? dirOverride = null)
        {
            if (target == null)
            {
                return;
            }

            var dir = Dir;
            if (dirOverride.HasValue)
            {
                dir = dirOverride.Value;
            }

            var animation = spell?.OverTimeAnimationId ?? spell?.HitAnimationId ?? Guid.Empty;

            PacketSender.SendAnimationToProximity(
                animation, -1, Id, target.MapId, (byte)target.X, (byte)target.Y, (sbyte)dir, target.MapInstanceId
            );
        }

        public abstract void UpdateSpellCooldown(int spellSlot);

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
        
        #endregion
    }
}
