using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.Entities.PlayerData;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
namespace Intersect.Server.Entities
{
    public abstract partial class AttackingEntity : Entity
    {
        protected bool IsUnableToCastSpells => CachedStatuses.Any(PredicateUnableToCastSpells);

        [NotMapped, JsonIgnore]
        public bool IsCasting => CastTime > Timing.Global.Milliseconds;

        [NotMapped, JsonIgnore]
        public int InterruptThreshold { get; set; } = -1; // <0 == no interrupt threshold set

        /// <summary>
        /// Whether an entity meets casting requirements (ammo & conditions) of some spell
        /// </summary>
        /// <param name="spell">The spell to cast</param>
        /// <returns>True if the entity meets the requirements</returns>
        protected abstract bool EntityMeetsCastingRequirements(SpellBase spell, bool instantCast = false);

        /// <summary>
        /// Whether an entity has the materials to cast a spell
        /// </summary>
        /// <param name="spell">The spell to cast</param>
        /// <returns>True if casting materials met</returns>
        protected abstract bool EntityHasCastingMaterials(SpellBase spell);

        /// <summary>
        /// Will check to see if we have appropriate casting materials and, if we do, will try to use them
        /// </summary>
        /// <param name="spell">The spell to cast</param>
        /// <returns>True if we successfully use the materials</returns>
        protected abstract bool TryConsumeCastingMaterials(SpellBase spell);

        /// <summary>
        /// Whether we meet the spell vital requirements
        /// </summary>
        /// <param name="spell">The spell to cast</param>
        /// <returns>True if we meet the HP/MP requirements for the spell</returns>
        public abstract bool MeetsSpellVitalReqs(SpellBase spell);

        /// <summary>
        /// An optional method that allows one to define a way to make further modifications to a spell's
        /// damage potential within the <see cref="DealDamageTo(Entity, List{AttackTypes}, int, double, ItemBase, out int)"/> method
        /// </summary>
        /// <param name="scaling"></param>
        /// <param name="attackTypes"></param>
        /// <param name="critChance"></param>
        /// <param name="critMultiplier"></param>
        protected abstract void PopulateExtraSpellDamage(ref int scaling,
            ref List<AttackTypes> attackTypes,
            ref int critChance,
            ref double critMultiplier);

        /// <summary>
        /// Whether this entity can cast some spell on some target
        /// </summary>
        /// <param name="spell">The spell to cast</param>
        /// <param name="target">The target to cast on</param>
        /// <returns>True if we can cast the spell</returns>
        public bool CanCastSpell(SpellBase spell, Entity target, bool ignoreVitals = false, bool instantCast = false)
        {
            if (spell == null)
            {
                return false;
            }

            if (spell.SpellType == SpellTypes.Passive)
            {
                return false;
            }

            if (!instantCast)
            {
                if (IsUnableToCastSpells)
                {
                    return false;
                }

                if (!ignoreVitals && !MeetsSpellVitalReqs(spell))
                {
                    // Not enough vitals!
                    return false;
                }
            }

            if (spell.Combat != null && spell.SpellType == SpellTypes.CombatSpell &&
                SpellTypeHelpers.SpellTypeRequiresTarget(spell.Combat.TargetType) &&
                !CanAttack(target, spell))
            {
                // Spell requires valid target!
                if (spell.Combat.Friendly && !IsAllyOf(target))
                {
                    Target = this;
                    return CanCastSpell(spell, this, ignoreVitals, instantCast);
                }

                return false;
            }

            if (SpellTypeHelpers.IsMovementSpell(spell.SpellType) && IsImmobile)
            {
                // Spell requires mobility!
                return false;
            }

            if (!EntityMeetsCastingRequirements(spell, instantCast))
            {
                // The entity doesn't have extraneous requirements met!
                return false;
            }

            return true;
        }

        /// <summary>
        /// Whether we can start a cast and set the entity as casting
        /// </summary>
        /// <param name="now">The timestamp we began casting at</param>
        /// <param name="spellSlot">The slot we're casting from, or -1 if not from a slot</param>
        /// <param name="target">The optional target we're casting to</param>
        /// <returns></returns>
        public bool CanStartCast(long now, int spellSlot, Entity target = null)
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

            if (spell.SpellType == SpellTypes.Passive)
            {
                return false;
            }

            if (IsCasting)
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

        /// <summary>
        /// Starts a cast - sets the casting timer for some entity.
        /// This is a convenience override for <see cref="StartCast(SpellBase, Entity, bool)"/>
        /// </summary>
        /// <param name="spellSlot">The slot to cast from</param>
        /// <param name="target">The target entity</param>
        protected void StartCast(int spellSlot, Entity target)
        {
            if (spellSlot < 0 && spellSlot >= Spells.Count)
            {
                return;
            }
            SpellCastSlot = spellSlot;
            var spell = SpellBase.Get(Spells[spellSlot].SpellId);

            if ((spell.Combat?.Friendly ?? false) && !IsAllyOf(target))
            {
                target = this;
            }

            StartCast(spell, target, spell.CastDuration <= 0);
        }

        /// <summary>
        /// Starts a cast - sets the casting timer for some entity.
        /// </summary>
        /// <param name="spell">The spell we're casting</param>
        /// <param name="target">The target we're casting to</param>
        /// <param name="instant">Whether the spell should instantly cast, and not set casting timers</param>
        public void StartCast(SpellBase spell, Entity target, bool instant = false)
        {
            if (spell == null)
            {
                return;
            }

            if (spell.SpellType == SpellTypes.Passive) 
            {
                return;
            }

            if (spell.CastDuration > 0 && !instant)
            {
                CastTime = Timing.Global.Milliseconds + spell.CastDuration;
            }
            else
            {
                CastTime = 0;
            }
            Target = target;

            if (spell.CastAnimationId != Guid.Empty)
            {
                PacketSender.SendAnimationToProximity(
                    spell.CastAnimationId, 1, base.Id, MapId, 0, 0, (sbyte)Dir, MapInstanceId
                );
            }

            InterruptThreshold = -1;
            if (instant || CastTime == 0)
            {
                UseSpell(spell, SpellCastSlot, Target);
            }
            else
            {
                if (spell.CastDuration > 0 && spell.InterruptThreshold > 0)
                {
                    InterruptThreshold = spell.InterruptThreshold;
                    PacketSender.SendCombatNumber(CombatNumberType.Interrupt, this, 0, threshold: spell.InterruptThreshold);
                }
                PacketSender.SendEntityCastTime(this, spell.Id);
            }
        }

        /// <summary>
        /// Cancels casting and informs the client of such
        /// </summary>
        public virtual void CancelCast()
        {
            CastTime = 0;
            CastTarget = null;
            SpellCastSlot = -1;
            PacketSender.SendEntityCancelCast(this);
        }

        /// <summary>
        /// Ensures that the spell we're trying to cast, at time of casting, can still be cast.
        /// Also consumes items if necessary.
        /// </summary>
        /// <param name="spell">The spell we're about to cast</param>
        /// <param name="target">The target we're casting to</param>
        /// <param name="ignoreVitals">Whether we want to ignore vital requirements for this cast</param>
        /// <returns>True if the cast is valid and is ready to occur</returns>
        protected virtual bool ValidateCast(SpellBase spell, Entity target, bool ignoreVitals)
        {
            if (spell == null)
            {
                return false;
            }

            if (spell.SpellType == SpellTypes.Passive)
            {
                return false;
            }

            if (spell.SpellType == SpellTypes.CombatSpell && !CanAttack(target, spell))
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

        /// <summary>
        /// Updates a caster's vitals as needed
        /// </summary>
        /// <param name="spell">The spell being cast</param>
        private void HandleSpellVitalUpdate(SpellBase spell)
        {
            var manaCost = spell.VitalCost[(int)Vitals.Mana];
            var healthCost = spell.VitalCost[(int)Vitals.Health];

            var attunementCount = StatusCount(StatusTypes.Attuned);
            if (attunementCount > 0)
            {
                manaCost = (int)Math.Floor(manaCost / (Options.Instance.CombatOpts.AttunedStatusDividend * attunementCount));
                healthCost = (int)Math.Floor(healthCost / (Options.Instance.CombatOpts.AttunedStatusDividend * attunementCount));
            }

            if (manaCost > 0)
            {
                SubVital(Vitals.Mana, manaCost);
            }
            else
            {
                AddVital(Vitals.Mana, -spell.VitalCost[(int)Vitals.Mana]);
            }

            if (healthCost > 0)
            {
                SubVital(Vitals.Health, healthCost);
            }
            else
            {
                AddVital(Vitals.Health, -spell.VitalCost[(int)Vitals.Health]);
            }
        }

        /// <summary>
        /// Applies any 1` a spell contains to some target entity
        /// </summary>
        /// <param name="spell">The spell being cast</param>
        /// <param name="target">The target being cast to</param>
        private void ApplySpellBuffsTo(SpellBase spell, Entity target)
        {
            if (spell == null || target == null || target.IsDead())
            {
                return;
            }

            if (spell.Combat?.Duration <= 0)
            {
                return;
            }

            var statBuffTime = -1;
            var expireTime = Timing.Global.Milliseconds + spell.Combat.Duration;
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                if (spell.Combat.StatDiff[i] == 0 && spell.Combat.PercentageStatDiff[i] == 0)
                {
                    continue;
                }

                var buff = new Buff(spell, spell.Combat.StatDiff[i], spell.Combat.PercentageStatDiff[i], expireTime);
                target.Stat[i].AddBuff(buff);
                statBuffTime = spell.Combat.Duration;
            }

            if (statBuffTime == -1 && spell.Combat.HoTDoT && spell.Combat.HotDotInterval > 0)
            {
                statBuffTime = spell.Combat.Duration;
            }

            // Is there an effect? If so, apply its status
            target.ApplyStatus(spell, this, statBuffTime);
        }

        /// <summary>
        /// Will apply spell buffs, DoTs, and damage if we can
        /// </summary>
        /// <param name="target">The target of the spell</param>
        /// <param name="spell">The spell that's being used</param>
        /// <param name="attackAnimDir">The direction of animation</param>
        /// <param name="projectile">A projectile attacked to the spell, if there is one</param>
        /// <returns>If attack landed</returns>
        public bool SpellAttack(Entity target, SpellBase spell, sbyte attackAnimDir, Projectile projectile, bool ignoreEvasion = false)
        {
            target?.ReactToCombat(this);
            if (spell?.Combat?.TargetType == SpellTargetTypes.Single && IsInvalidTauntTarget(target))
            {
                return false;
            }

            if (!ignoreEvasion && !spell.Combat.Friendly && target.Id != Id && spell.Combat.DamageType != (int)DamageType.True && CombatUtilities.AttackMisses(Accuracy, target.Evasion, this is Player))
            {
                if (spell?.Combat?.HotDotInterval > 0)
                {
                    SendMissedAttackMessage(target, DamageType.Magic);
                }
                else
                {
                    SendMissedAttackMessage(target, DamageType.Physical);
                }
                AttackMissed.Invoke(target);
                return false;
            }

            if (spell.Combat.Friendly || !target.IsInvincibleTo(this)) 
            {
                ApplySpellBuffsTo(spell, target);
                Combat.DoT.AddSpellDoTsTo(spell, this, target);
            }

            // Determine if we want to apply effects to otherwise invulnerable NPCs
            if (this is Player pl && target is Npc np && !pl.CanAttackNpc(np))
            {
                if (spell.Combat.IsDamaging)
                {
                    return false;
                }
                else if (!Options.Instance.CombatOpts.InvulnerableNpcsAffectedByNonDamaging)
                {
                    return false;
                }
            }

            var scaling = spell.Combat.Scaling;
            List<AttackTypes> attackTypes = new List<AttackTypes>(spell.Combat.DamageTypes);
            var critChance = spell.Combat.CritChance;
            var critMultiplier = spell.Combat.CritMultiplier;

            PopulateExtraSpellDamage(ref scaling, ref attackTypes, ref critChance, ref critMultiplier);

            int damage;
            if (this is Player player && spell.WeaponSpell)
            {
                if (!TryDealDamageTo(target, attackTypes, scaling, critMultiplier, player.CastingWeapon, spell, true, GetDistanceTo(target), out damage))
                {
                    // Only return false if the spell is _supposed_ to do damage, but didn't
                    return !spell.Combat.IsDamaging;
                }
            }
            else if (!TryDealDamageTo(target, attackTypes, scaling, IsCriticalHit(critChance) ? critMultiplier : 1.0, null, spell, true, GetDistanceTo(target), out damage))
            {
                // Only return false if the spell is _supposed_ to do damage, but didn't
                return !spell.Combat.IsDamaging;
            }

            if (spell.Combat.LifeSteal)
            {
                SpellLifesteal(damage);
            }

            return true;
        }

        public void SpellLifesteal(int amount)
        {
            HealVital(Vitals.Health, amount);
            PacketSender.SendCombatNumber(CombatNumberType.HealHealth, this, amount);
        }

        public void SpellManasteal(int amount)
        {
            HealVital(Vitals.Mana, amount);
            PacketSender.SendCombatNumber(CombatNumberType.HealMana, this, amount);
        }

        /// <summary>
        /// Validates the final casting of and handles the actions of a spell
        /// </summary>
        /// <param name="spell">The spell to use</param>
        /// <param name="spellSlot">The slot it is being used from, or -1 if not from a spell slot</param>
        /// <param name="ignoreVitals">Whether we want to ignore vital costs</param>
        /// <param name="prayerSpell">Whether this spell is from a prayer</param>
        /// <param name="prayerSpellDir">What direction the prayer was</param>
        /// <param name="prayerTarget">The prayer's target</param>
        public virtual void UseSpell(SpellBase spell, int spellSlot, Entity target, bool ignoreVitals = false, bool prayerSpell = false, byte prayerSpellDir = 0, Entity prayerTarget = null, bool instantCast = false)
        {
            if (spell?.SpellType == SpellTypes.Passive)
            {
                CancelCast();
                return;
            }

            CastTarget = target;
            // We're actually doing the spell now - use our mats and if we fail, end
            if (!prayerSpell && !instantCast && !ValidateCast(spell, CastTarget, ignoreVitals))
            {
                CancelCast();
                return;
            }

            // Without this, stealth spells... immediately unstealth you.
            if ((spell.Combat.Effect != StatusTypes.Stealth && !spell.Combat.Friendly) && spell.Combat.TargetType != SpellTargetTypes.OnHit)
            {
                Unstealth();
            }

            // Used because we don't want to dash until _after_ we've cancelled spell casting - otherwise, the dash won't move us since we're spellcasting
            var queueDashAfterSpellCancel = false;
            switch (spell.SpellType)
            {
                case SpellTypes.CombatSpell:
                case SpellTypes.Event:

                    switch (spell.Combat.TargetType)
                    {
                        case SpellTargetTypes.Self:
                            if (spell.HitAnimationId != Guid.Empty && spell.Combat.Effect != StatusTypes.OnHit)
                            {
                                SendSpellHitAnimation(spell, this, Id);
                            }
                            SpellAttack(this, spell, (sbyte)Dir, null);

                            break;
                        case SpellTargetTypes.Single:
                            bool inRange;
                            if (spell.Combat.MinRange > 0)
                            {
                                inRange = InRangeOf(CastTarget, spell.Combat.CastRange, spell.Combat.MinRange);
                            }
                            else
                            {
                                inRange = InRangeOf(CastTarget, spell.Combat.CastRange);
                            }

                            if (!inRange)
                            {
                                SendMissedAttackMessage(CastTarget, DamageType.Physical);
                            }
                            else
                            {
                                if (spell.Combat.HitRadius > 0) //Single target spells with AoE hit radius'
                                {
                                    HandleAoESpell(
                                        spell.Id, CastTarget?.MapId ?? Guid.Empty, CastTarget?.X ?? 0, CastTarget?.Y ?? 0,
                                        null, false, false, true
                                    );
                                }
                                else
                                {
                                    if (SpellAttack(CastTarget, spell, (sbyte)Dir, null))
                                    {
                                        SendSpellHitAnimation(spell, CastTarget, CastTarget?.Id ?? Guid.Empty);
                                    }
                                }
                            }

                            break;
                        case SpellTargetTypes.AoE:
                            HandleAoESpell(spell.Id, MapId, X, Y, null);
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
                            new Status(
                                    this, this, spell, StatusTypes.OnHit, spell.Combat.OnHitDuration,
                                    spell.Combat.TransformSprite
                                );

                            PacketSender.SendActionMsg(
                                this, Strings.Combat.status[(int)StatusTypes.OnHit],
                                CustomColors.Combat.Status
                            );

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
                        SendSpellHitAnimation(spell, this, Id);
                        Warp(
                            spell.Warp.MapId, spell.Warp.X, spell.Warp.Y,
                            spell.Warp.Dir - 1 == -1 ? (byte)this.Dir : (byte)(spell.Warp.Dir - 1)
                        );
                    }

                    break;
                case SpellTypes.WarpTo:
                    if (CastTarget != null)
                    {
                        HandleAoESpell(spell.Id, MapId, X, Y, CastTarget, false, false, true);
                    }
                    break;
                case SpellTypes.Dash:
                    queueDashAfterSpellCancel = true;
                    // Alex: removed this message, it was too noisy -- PacketSender.SendActionMsg(this, Strings.Combat.dash, CustomColors.Combat.Dash);

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
                Die(killer: this);
            }

            // Clear casting target info
            if (!instantCast && !prayerSpell)
            {
                CancelCast();
            }

            if (queueDashAfterSpellCancel)
            {
                _ = new Dash(
                        this, spell.Combat.CastRange, (byte)Dir,
                        Convert.ToBoolean(spell.Dash.IgnoreMapBlocks),
                        Convert.ToBoolean(spell.Dash.IgnoreActiveResources),
                        Convert.ToBoolean(spell.Dash.IgnoreInactiveResources),
                        Convert.ToBoolean(spell.Dash.IgnoreZDimensionAttributes),
                        Convert.ToBoolean(spell.Dash.IgnoreEntites),
                        spell.Dash.Spell,
                        spell.Dash.DashAnimation
                    );
            }
        }

        /// <summary>
        /// Checks to see, during an entity's update loop, whether it's time to use some spell
        /// </summary>
        /// <param name="timeMs"></param>
        public override void CheckForSpellCast(long timeMs)
        {
            if (CastTime != 0 && !IsCasting && SpellCastSlot < Spells.Count && SpellCastSlot >= 0)
            {
                var spell = SpellBase.Get(Spells[SpellCastSlot].SpellId);
                UseSpell(spell, SpellCastSlot, Target);
            }
        }

        /// <summary>
        /// Checks the current map params for surrounding entities and, if we can cast the spell on them,
        /// casts it
        /// </summary>
        public void HandleAoESpell(
            Guid spellId,
            Guid startMapId,
            int startX,
            int startY,
            Entity spellTarget,
            bool ignoreEvasion = false,
            bool isProjectileTool = false,
            bool ignoreMissMessage = false,
            int tool = -1,
            bool detonation = false)
        {
            var spellBase = SpellBase.Get(spellId);
            var startMap = MapController.Get(startMapId);

            if (spellBase == null || spellBase.Combat == null || startMap == null)
            {
                return;
            }

            // First, calculate the relative offset positions
            var offsetX = spellBase.Combat.AoeXOffset;
            var offsetY = spellBase.Combat.AoeYOffset;
            if (spellBase.Combat.AoeRelativeOffset)
            {
                var aoeOffset = PositionUtilities.GetAoeOffset(
                    Dir, 
                    offsetX, 
                    offsetY, 
                    spellBase.Combat.AoeShape, 
                    spellBase.Combat.AoeRectWidth, 
                    spellBase.Combat.AoeRectHeight);

                offsetX = aoeOffset.X;
                offsetY = aoeOffset.Y;
            }

            // Then, translate these offsets from our spell's starting position
            var castingTile = new TileHelper(startMapId, startX, startY);
            if (!castingTile.Translate(offsetX, offsetY))
            {
                return;
            }

            var isTrap = spellBase.Combat.AoeTrapSpawner;
            // AoE attacks either attack outright...
            if (!isTrap || detonation) 
            {
                ProcessAoeTargets(spellBase, spellTarget, castingTile, tool, isProjectileTool, ignoreMissMessage, ignoreEvasion);
            }
            // ... or spawn traps within an area at a given intensity
            else if (isTrap)
            {
                SpawnAreaTraps(spellBase, castingTile);
            }
        }

        private void ProcessAoeTargets(SpellBase spellBase, Entity spellTarget, TileHelper castingTile, int tool, bool isProjectileTool, bool ignoreMissMessage, bool ignoreEvasion)
        {
            int entitiesHit = 0;
            foreach (var instance in MapController.GetSurroundingMapInstances(castingTile.MapId, MapInstanceId, true))
            {
                foreach (var entity in instance.GetCachedEntities())
                {
                    if (entity == null)
                    {
                        continue;
                    }

                    if (isProjectileTool && entity is Resource resource && resource.Base.Tool != tool)
                    {
                        continue;
                    }

                    if (!isProjectileTool && !(entity is AttackingEntity))
                    {
                        continue;
                    }

                    if (spellTarget != null && spellTarget != entity)
                    {
                        continue;
                    }

                    if (!IsInAoeRange(spellBase, entity, castingTile.GetMap(), castingTile.GetX(), castingTile.GetY()))
                    {
                        continue;
                    }

                    if (entity.Id == Id && !spellBase.Combat.Friendly)
                    {
                        continue;
                    }

                    if ((spellBase.Combat.Friendly && !IsAllyOf(entity)) || (!spellBase.Combat.Friendly && IsAllyOf(entity)))
                    {
                        continue;
                    }

                    if (spellBase.Combat.Friendly && entity.Id != Id && entity is Player dueler && dueler.InDuel)
                    {
                        continue;
                    }

                    if (entity.IsImmuneTo(Immunities.Spellcasting))
                    {
                        continue;
                    }

                    if (!spellBase.Combat.Friendly && entity.IsInvincibleTo(this))
                    {
                        entity.ReactToCombat(this);
                        SendBlockedAttackMessage(this);
                        continue;
                    }

                    //Check to handle a warp to spell
                    if (spellBase.SpellType == SpellTypes.WarpTo)
                    {
                        if (spellTarget != null)
                        {
                            // spellTarget used to be Target. I don't know if this is correct or not.
                            int[] position = GetPositionNearTarget(spellTarget.MapId, spellTarget.X, spellTarget.Y, spellTarget.Dir);
                            Warp(spellTarget.MapId, (byte)position[0], (byte)position[1], (byte)Dir);
                            ChangeDir(DirToEnemy(spellTarget));
                        }
                    }

                    SpellAttack(entity, spellBase, (sbyte)Directions.Up, null, ignoreEvasion); //Handle damage
                    SendSpellHitAnimation(spellBase, entity, entity.Id);
                    entitiesHit++;
                }
            }

            if (entitiesHit < 1 && !isProjectileTool && !ignoreMissMessage) // Will count yourself - which is FINE in the case of a friendly spell, otherwise ignore it
            {
                if (this is Player)
                {
                    PacketSender.SendChatMsg((Player)this, "There weren't any targets in your spell's AoE range.", ChatMessageType.Spells, CustomColors.General.GeneralWarning);
                }
                SendMissedAttackMessage(this, DamageType.True);
                AttackMissed.Invoke(null);
            }

            if (this is Player && entitiesHit > 1 && !spellBase.Combat.Friendly)
            {
                ChallengeUpdateProcesser.UpdateChallengesOf(new AoEHitsUpdate((Player)this, entitiesHit));
            }
        }

        public void SpawnAreaTraps(SpellBase spell, TileHelper startTile)
        {
            if (spell == null || startTile == null || !startTile.IsValid() || spell.Combat == null)
            {
                return;
            }

            var faceDir = (Directions)Dir;
            var originalTile = new TileHelper(startTile); // Offsetted spawn tile

            var leftBound = new TileHelper(originalTile); // Bottom-left of AoE region
            var rightBound = new TileHelper(originalTile); // Top-right of AoE region

            // Figure out where our bounds should be based on the AoE shape
            if (spell.Combat.AoeShape == AoeShape.Circle)
            {
                leftBound.Translate(-spell.Combat.HitRadius, spell.Combat.HitRadius);
                rightBound.Translate(spell.Combat.HitRadius, -spell.Combat.HitRadius);
            }
            else if (spell.Combat.AoeShape == AoeShape.Rectangle)
            {
                if (faceDir == Directions.Right || faceDir == Directions.Left)
                {
                    // -1 because we are inclusive with tile size
                    rightBound.Translate(spell.Combat.AoeRectHeight - 1, -(spell.Combat.AoeRectWidth - 1));
                }
                else
                {
                    // -1 because we are inclusive with tile size
                    rightBound.Translate(spell.Combat.AoeRectWidth - 1, -(spell.Combat.AoeRectHeight - 1));
                }
            }

            // We're gonna traverse the possible trap tiles starting from the bottom left and ending at the top right.
            var spawnTile = new TileHelper(leftBound);
            var done = false;
            while (!done && spawnTile.IsValid())
            {
                if (!MapController.TryGet(spawnTile.MapId, out var currMap) || !MapController.TryGetInstanceFromMap(spawnTile.MapId, MapInstanceId, out var mapInstance))
                {
                    done = true;
                    continue;
                }

                if (spell.Combat.AoeShape != AoeShape.Circle ||
                    GetDistanceBetween(
                        MapController.Get(originalTile.MapId),
                        MapController.Get(spawnTile.MapId),
                        originalTile.X,
                        spawnTile.X,
                        originalTile.Y,
                        spawnTile.Y) <= spell.Combat.HitRadius)
                {
                    // Spawn a trap if the intensity allows it
                    if (spell.Combat.AoeTrapIntensity > 0 && Randomization.Next(0, 100) < spell.Combat.AoeTrapIntensity - 1)
                    {
                        mapInstance.SpawnTrap(this, spell, (byte)spawnTile.X, (byte)spawnTile.Y, (byte)Z);
                    }
                }

                var spawnWorld = GetWorldTile(spawnTile.X, spawnTile.Y, MapController.Get(spawnTile.MapId));
                var rightBoundWorld = GetWorldTile(rightBound.X, rightBound.Y, MapController.Get(rightBound.MapId));
                // If we've reached the right bound's X value, go back to the start X and up 1 Y
                if (spawnWorld.X == rightBoundWorld.X)
                {
                    if (spell.Combat.AoeShape == AoeShape.Circle)
                    {
                        spawnTile.Translate(-(spell.Combat.HitRadius * 2), -1);
                    }
                    else
                    {
                        if (faceDir == Directions.Right || faceDir == Directions.Left)
                        {
                            spawnTile.Translate(-(spell.Combat.AoeRectHeight - 1), -1);
                        }
                        else
                        {
                            spawnTile.Translate(-(spell.Combat.AoeRectWidth - 1), -1);
                        }
                    }
                }
                // Otherwise, just move one to the right
                else
                {
                    spawnTile.Translate(1, 0);
                }

                if (spawnWorld.X == rightBoundWorld.X && spawnWorld.Y == rightBoundWorld.Y)
                {
                    done = true;
                }
            }
        }

        public bool IsInAoeRange(SpellBase spell, Entity target, MapController startMap, int startX, int startY)
        {
            if (spell == null || spell.Combat == null || target == null)
            {
                return false;
            }

            if (spell.SpellType == SpellTypes.WarpTo || spell.SpellType == SpellTypes.Dash)
            {
                return true;
            }

            // If this is coming from a trap spawned by an AoE Area Denial spell, use the trap radius override
            // instead of the spell's AoE radius. Hacky
            if (spell.Combat.AoeTrapSpawner)
            {
                return target.GetDistanceTo(startMap, startX, startY) <= spell.Combat.AoeTrapRadiusOverride;
            }

            var shape = spell.Combat.AoeShape;
            switch (shape)
            {
                case AoeShape.Circle:
                    if (spell.Combat.MinRange > 0)
                    {
                        return InRangeOf(target, spell.Combat.HitRadius, spell.Combat.MinRange);
                    }
                    return InRangeOf(target, spell.Combat.HitRadius);

                case AoeShape.Rectangle:
                    var faceDir = (Directions)Dir;
                    if (faceDir == Directions.Right || faceDir == Directions.Left)
                    {
                        return target.IsInBoundingBox(startMap, startX, startY, spell.Combat.AoeRectHeight, spell.Combat.AoeRectWidth);
                    }
                    else
                    {
                        return target.IsInBoundingBox(startMap, startX, startY, spell.Combat.AoeRectWidth, spell.Combat.AoeRectHeight);
                    }

                default:
                    Logging.Log.Warn($"Spell has invalid AoE range: {shape}");
#if DEBUG
                    throw new ArgumentOutOfRangeException(nameof(shape));
#endif
            }
        }

        /// <summary>
        /// Sends a hit animation to some entity using a spell's properties
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="target"></param>
        /// <param name="dirOverride"></param>
        public void SendSpellHitAnimation(SpellBase spell, Entity target, Guid entityId, sbyte? dirOverride = null)
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

            var anim = spell?.HitAnimationId ?? Guid.Empty;

            var targetType = (target.IsDead() || target.IsDisposed) ? -1 : 1;

            PacketSender.SendAnimationToProximity(
                anim, targetType, entityId, target.MapId, (byte)target.X, (byte)target.Y, (sbyte)dir, target.MapInstanceId
            );
        }

        /// <summary>
        /// Used to tell the entity how to update its spell cooldowns for some spell slot
        /// </summary>
        /// <param name="spellSlot">The slot under cooldown</param>
        protected abstract void UpdateSpellCooldown(int spellSlot);

        /// <summary>
        /// Used to determine if statuses are affecting our spellcasting abilities
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
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

        public virtual void CheckForOnhitAttack(Entity enemy)
        {
            foreach (var status in CachedStatuses)
            {
                if (status.Type != StatusTypes.OnHit)
                {
                    continue;
                }

                var spellFriendly = status.Spell?.Combat?.Friendly ?? false;
                if ((IsAllyOf(enemy) && spellFriendly) || (!IsAllyOf(enemy) && !spellFriendly))
                {
                    if (SpellAttack(enemy, status.Spell, (sbyte)Dir, null))
                    {
                        SendSpellHitAnimation(status.Spell, enemy, enemy?.Id ?? Guid.Empty);
                    }
                    status.RemoveStatus();
                }
            }
        }

        protected void RemoveMissedOnHits()
        {
            foreach (var status in CachedStatuses)
            {
                if (status.Type != StatusTypes.OnHit)
                {
                    continue;
                }

                if (!status.Spell?.Combat?.PersistMissedAttack ?? false)
                {
                    status.RemoveStatus();
                }
            }
        }

        protected void RemoveWeaponSwapOnHits()
        {
            foreach (var status in CachedStatuses)
            {
                if (status.Type != StatusTypes.OnHit)
                {
                    continue;
                }

                if (!status.Spell?.Combat?.PersistWeaponSwap ?? false)
                {
                    status.RemoveStatus();
                }
            }
        }
    }
}
