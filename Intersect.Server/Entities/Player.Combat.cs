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
using MimeKit.Cryptography;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public override bool MeleeAvailable()
        {
            if (CastTime > Timing.Global.Milliseconds)
            {
                if (Options.Combat.EnableCombatChatMessages)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.channelingnoattack, ChatMessageType.Combat);
                }
            }

            return base.MeleeAvailable();
        }

        public override bool CanMeleeTarget(Entity target)
        {
            if (target == null || target.IsDisposed || target.IsDead())
            {
                return false;
            }

            if (target is Player pvpTarget && !CanPvpPlayer(pvpTarget))
            {
                return false;
            }

            if (target is Npc npcTarget && !CanAttackNpc(npcTarget))
            {
                return false;
            }

            // Ranged weapon
            var equippedWeapon = GetEquippedWeapon();
            if (equippedWeapon != default && equippedWeapon.ProjectileId != Guid.Empty)
            {
                return false;
            }

            return base.CanMeleeTarget(target);
        }

        public override bool IsCriticalHit(int critChance)
        {
            var affinity = GetBonusEffectTotal(EffectType.Affinity);
            
            if (critChance > 0)
            {
                critChance += affinity;
            }

            return base.IsCriticalHit(critChance);
        }

        public override bool TryDealDamageTo(Entity enemy,
            List<AttackTypes> attackTypes,
            int dmgScaling,
            double critMultiplier,
            ItemBase weapon,
            SpellBase spell,
            bool ignoreEvasion,
            int range,
            out int damage)
        {
            damage = 0;
            critMultiplier = 1.0; // override - determined by item or spell
            // If this is an unarmed attack, use class stats
            if (weapon == null && spell == null)
            {
                var cls = ClassBase.Get(ClassId);
                if (cls == null)
                {
                    return false;
                }

                if (IsCriticalHit(cls.CritChance))
                {
                    critMultiplier = cls.CritChance;
                    critMultiplier = ApplyEffectBonusToValue(critMultiplier, EffectType.CritBonus);
                }
            }
            else if (weapon != null && spell == null && IsCriticalHit(weapon.CritChance))
            {
                critMultiplier = weapon.CritMultiplier;
                critMultiplier = ApplyEffectBonusToValue(critMultiplier, EffectType.CritBonus);
            } else if (spell != null && spell.Combat != null)
            {
                var spellCrit = false;
                if (weapon == null)
                {
                    spellCrit = IsCriticalHit(spell.Combat.CritChance);
                }
                else if (spell.WeaponSpell)
                {
                    spellCrit = IsCriticalHit(spell.Combat.CritChance + weapon.CritChance);
                }

                if (spellCrit)
                {
                    critMultiplier = spell.Combat.CritMultiplier;
                    if (spell.WeaponSpell && weapon != null)
                    {
                        critMultiplier += weapon.CritMultiplier;
                    }
                    critMultiplier = ApplyEffectBonusToValue(critMultiplier, EffectType.CritBonus);
                }
            }

            var targetHealthBefore = enemy.GetVital(Vitals.Health);
            var targetMaxHealth = enemy.GetMaxVital(Vitals.Health);

            bool damageWasDealt;
            if (spell != default)
            {
                damageWasDealt = base.TryDealDamageTo(enemy, CombatUtilities.GetSpellAttackTypes(spell, weapon), dmgScaling, critMultiplier, weapon, spell, ignoreEvasion, range, out damage);
            }
            else
            {
                damageWasDealt = base.TryDealDamageTo(enemy, weapon?.AttackTypes ?? new List<AttackTypes>() { AttackTypes.Blunt }, dmgScaling, critMultiplier, weapon, spell, ignoreEvasion, range, out damage);
            }

            // Update challenges
            if (damageWasDealt && damage > 0)
            {
                ChallengeUpdateProcesser.UpdateChallengesOf(new DamageOverTimeUpdate(this, damage), enemy.TierLevel);
                ChallengeUpdateProcesser.UpdateChallengesOf(new MissFreeUpdate(this), enemy.TierLevel);

                // For challenges where we don't want DoT values fudging the challenge - cheap fix
                if (LastWeaponSwitch <= Timing.Global.Milliseconds)
                {
                    ChallengeUpdateProcesser.UpdateChallengesOf(new MaxHitUpdate(this, damage));
                    ChallengeUpdateProcesser.UpdateChallengesOf(new DamageAtRangeUpdate(this, damage, range));
                    ChallengeUpdateProcesser.UpdateChallengesOf(new MissFreeAtRangeUpdate(this, range), enemy.TierLevel);
                    ChallengeUpdateProcesser.UpdateChallengesOf(new HitFreeUpdate(this), enemy.TierLevel);
                }
            }
            if (damage < 0)
            {
                var healingRatio = (int)Math.Floor(((float)targetHealthBefore / targetMaxHealth) * 100);
                ChallengeUpdateProcesser.UpdateChallengesOf(new DamageHealedAtHealthUpdate(this, damage, healingRatio));
            }

            return damageWasDealt;
        }

        public override void MeleeAttack(Entity enemy, bool ignoreEvasion)
        {
            enemy?.ReactToCombat(this);

            // Send an attack attempt to the client
            PacketSender.SendEntityAttack(this, CalculateAttackTime());

            // Attack literally missing
            if (!CanMeleeTarget(enemy))
            {
                OnAttackMissed(enemy);
                return;
            }
            
            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            // Short-circuit out if resource and let resource harvesting logic go
            if (enemy is Resource targetResource)
            {
                if (!CanHarvestWithTool(targetResource, weapon?.Descriptor?.Tool ?? -1))
                {
                    return;
                }
                if (targetResource.Base.Tool >= 0)
                {
                    HandleResourceMelee(targetResource);
                    return;
                }
            }
            // We're not attacking a resource - disable resource locking
            SetResourceLock(false);

            // If we're doing a player interaction, start those events
            if (enemy is Player pvpTarget)
            {
                pvpTarget.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerInteract, "", Name);
            }

            List<AttackTypes> attackTypes = new List<AttackTypes>();
            if (weapon?.Descriptor?.AttackTypes != null)
            {
                attackTypes.AddRange(weapon.Descriptor.AttackTypes);
            }

            // Check for backstab
            Backstab = false;
            if (weapon?.Descriptor?.CanBackstab ?? false && Dir == Target.Dir)
            {
                Backstab = true;
            }

            if (!TryDealDamageTo(enemy, attackTypes, 100, 1.0, weapon?.Descriptor, null, false, 1, out int damage))
            {
                return;
            }

            if (TryLifesteal(damage, enemy, out var healthRecovered))
            {
                PacketSender.SendCombatNumber(CombatNumberType.HealHealth, this, (int)healthRecovered);
            }
            if (TryManasteal(damage, enemy, out var manaRecovered))
            {
                PacketSender.SendCombatNumber(CombatNumberType.HealMana, this, (int)manaRecovered);
                PacketSender.SendCombatNumber(CombatNumberType.DamageMana, enemy, (int)manaRecovered);
            }

            CheckForOnhitAttack(enemy);
        }

        /// <summary>
        /// Ensures that the spell we're trying to cast, at time of casting, can still be cast.
        /// Also consumes items if necessary.
        /// </summary>
        /// <param name="spell">The spell we're about to cast</param>
        /// <param name="target">The target we're casting to</param>
        /// <param name="ignoreVitals">Whether we want to ignore vital requirements for this cast</param>
        /// <returns>True if the cast is valid and is ready to occur</returns>
        protected override bool ValidateCast(SpellBase spell, Entity target, bool ignoreVitals)
        {
            if (spell == null)
            {
                return false;
            }

            if (!SkillPrepared(spell.Id))
            {
                PacketSender.SendChatMsg(this, Strings.Combat.SpellNotPrepared, ChatMessageType.Error, "", true);
                return false;
            }

            return base.ValidateCast(spell, target, ignoreVitals);
        }

        public override void UseSpell(SpellBase spell, int spellSlot, Entity target, bool ignoreVitals = false, bool prayerSpell = false, byte prayerSpellDir = 0, Entity prayerTarget = null, bool instantCast = false)
        {
            if (spell == null)
            {
                return;
            }

            if (PlayerDead)
            {
                CancelCast();
                return;
            }

            if (target is Player spellTarget && spell.Combat.Friendly && spellTarget.Id != Id && spellTarget.InDuel && spell.Combat.TargetType != SpellTargetTypes.Self)
            {
                PacketSender.SendChatMsg(this, "You can not cast friendly spells on a dueling player.", ChatMessageType.Notice, CustomColors.General.GeneralDisabled);
                CancelCast();
                return;
            }

            if (resourceLock != null)
            {
                SetResourceLock(false);
            }

            CastingWeapon = GetEquippedWeapon();

            Backstab = false;
            if (CastingWeapon != null && CastingWeapon.CanBackstab && spell.WeaponSpell && target != null && target.Dir == Dir)
            {
                Backstab = true;
            }

            switch (spell.SpellType)
            {
                case SpellTypes.Event:
                    var evt = spell.Event;
                    if (evt != null)
                    {
                        EnqueueStartCommonEvent(evt);
                    }

                    base.UseSpell(spell, spellSlot, target, ignoreVitals, prayerSpell, prayerSpellDir, prayerTarget, instantCast);
                    break;

                default:
                    base.UseSpell(spell, spellSlot, target, ignoreVitals, prayerSpell, prayerSpellDir, prayerTarget, instantCast);
                    break;
            }
        }

        protected override void PopulateExtraSpellDamage(ref int scaling,
            ref List<AttackTypes> attackTypes,
            ref int critChance,
            ref double critMultiplier)
        {
            if (CastingWeapon == null)
            {
                return;
            }

            attackTypes.AddRange(CastingWeapon.AttackTypes);
            critChance += CastingWeapon.CritChance;
            critMultiplier += CastingWeapon.CritMultiplier;
        }

        public override bool MeetsSpellVitalReqs(SpellBase spell)
        {
            if (spell == null)
            {
                throw new ArgumentNullException(nameof(spell));
            }

            var cost = spell.VitalCost[(int)Vitals.Mana];

            var attunementCount = StatusCount(StatusTypes.Attuned);
            if (attunementCount > 0)
            {
                cost = (int)Math.Floor(cost / (Options.Instance.CombatOpts.AttunedStatusDividend * attunementCount));
            }

            if (spell.VitalCost[(int)Vitals.Mana] > GetVital(Vitals.Mana))
            {
                if (Options.Combat.EnableCombatChatMessages)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.lowmana, ChatMessageType.Combat);
                }
                if (MPWarningSent < Timing.Global.Milliseconds) // attempt to limit how often we send this notification
                {
                    MPWarningSent = Timing.Global.Milliseconds + Options.Combat.MPWarningDisplayTime;
                    PacketSender.SendGUINotification(Client, GUINotification.NotEnoughMp, true);
                }

                return false;
            }

            if (spell.VitalCost[(int)Vitals.Health] > GetVital(Vitals.Health))
            {
                if (Options.Combat.EnableCombatChatMessages)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.lowhealth, ChatMessageType.Combat);
                }

                return false;
            }

            return true;
        }

        protected override void UpdateSpellCooldown(int spellSlot)
        {
            if (spellSlot < 0 || spellSlot > Spells.Count)
            {
                return;
            }

            var spell = SpellBase.Get(Spells[spellSlot].SpellId);
            if (spell == null)
            {
                return;
            }

            UpdateCooldown(spell);

            // Trigger the global cooldown, if we're allowed to.
            if (!spell.IgnoreGlobalCooldown)
            {
                UpdateGlobalCooldown();
            }
        }

        protected override bool EntityMeetsCastingRequirements(SpellBase spell, bool instantCast = false)
        {
            if (spell == null)
            {
                throw new ArgumentNullException(nameof(spell));
            }

            if (!instantCast && !SkillPrepared(spell.Id))
            {
                if (Timing.Global.Milliseconds > ChatErrorLastSent)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.SpellNotPrepared, ChatMessageType.Error, "", true);
                    ChatErrorLastSent = Timing.Global.Milliseconds + 1000;
                }
                return false;
            }

            if (!EntityHasCastingMaterials(spell))
            {
                return false;
            }

            if (!Conditions.MeetsConditionLists(spell.CastingRequirements, this, null))
            {
                if (Timing.Global.Milliseconds > ChatErrorLastSent)
                {
                    if (!string.IsNullOrWhiteSpace(spell.CannotCastMessage))
                    {
                        PacketSender.SendChatMsg(this, spell.CannotCastMessage, ChatMessageType.Error, "", true);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(this, Strings.Combat.dynamicreq, ChatMessageType.Spells, CustomColors.Alerts.Error);
                    }
                    ChatErrorLastSent = Timing.Global.Milliseconds + 1000;
                }

                return false;
            }

            return true;
        }

        protected override bool EntityHasCastingMaterials(SpellBase spell)
        {
            if (spell == null)
            {
                throw new ArgumentNullException(nameof(spell));
            }

            if (spell.RequiresAmmo && !HasProjectileAmmo(spell.Combat.Projectile))
            {
                return false;
            }

            if (!HasCastingComponents(spell.CastingComponents))
            {
                return false;
            }

            return true;
        }

        protected override bool TryConsumeCastingMaterials(SpellBase spell)
        {
            if (spell.RequiresAmmo && !TryConsumeProjectileAmmo(spell.Combat.Projectile))
            {
                return false;
            }

            if (!TryConsumeCastingComponents(spell))
            {
                return false;
            }

            return true;
        }

        public override bool CanRangeTarget(Entity target)
        {
            if (IsInvalidTauntTarget(target))
            {
                PacketSender.SendActionMsg(this, Strings.Combat.miss, CustomColors.Combat.Missed);

                return false;
            }

            return base.CanRangeTarget(target);
        }

        public override void ProjectileAttack(Entity enemy, Projectile projectile, SpellBase parentSpell, ItemBase parentWeapon, bool ignoreEvasion, byte projectileDir)
        {
            if (projectile == null || projectile.Base == null)
            {
                return;
            }
            if (!CanRangeTarget(enemy))
            {
                return;
            }
            if (enemy is Npc npcTarget && !CanAttackNpc(npcTarget))
            {
                return;
            }

            if (enemy is Player pvpTarget)
            {
                pvpTarget.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerInteract, "", Name);
                if ((!parentSpell?.Combat?.Friendly ?? false) && !CanPvpPlayer(pvpTarget))
                {
                    return;
                }
            }
            else if (enemy is Resource resource)
            {
                _ = TryHarvestResourceProjectile(resource, projectile, parentSpell);
                return;
            }

            base.ProjectileAttack(enemy, projectile, parentSpell, parentWeapon, false, projectileDir);
        }

        private bool CanPvpPlayer(Player target)
        {
            return !(target == null
                || IsAllyOf(target)
                || Map.ZoneType == MapZones.Safe
                || target.Map?.ZoneType == MapZones.Safe) || Dueling.Contains(target);
        }

        protected override bool TryLifesteal(int damage, Entity target, out float recovered)
        {
            recovered = 0;

            if (IsDead())
            {
                return false;
            }

            if (damage <= 0 || target == null || target is Resource || target.IsDisposed || target.IsDead())
            {
                return false;
            }

            var lifesteal = GetBonusEffectTotal(EffectType.Lifesteal) / 100f;
            if (lifesteal <= 0)
            {
                return false;
            }

            var healthRecovered = Math.Max(1, lifesteal * damage);
            if (healthRecovered <= 0)
            {
                return false;
            }

            healthRecovered = Math.Min(target.GetVital((int)Vitals.Health), healthRecovered);
            if (healthRecovered == 0)
            {
                return false;
            }

            var vampire = GetBonusEffectPercent(EffectType.Vampire, true);

            AddVital(Vitals.Health, (int)Math.Round(healthRecovered * vampire));
            recovered = healthRecovered;
            return true;
        }

        protected override bool TryManasteal(int damage, Entity target, out float recovered)
        {
            recovered = 0;
            
            if (IsDead())
            {
                return false;
            }

            if (damage <= 0 || target == null || target is Resource || target.IsDisposed || target.IsDead())
            {
                return false;
            }

            var manasteal = GetBonusEffectTotal(EffectType.Manasteal) / 100f;
            if (manasteal <= 0)
            {
                return false;
            }

            var manaRecovered = Math.Max(1, manasteal * damage);
            if (manaRecovered <= 0)
            {
                return false;
            }

            manaRecovered = Math.Min(target.GetVital((int)Vitals.Mana), manaRecovered);
            if (manaRecovered == 0)
            {
                return false;
            }

            var vampire = GetBonusEffectPercent(EffectType.Vampire, true);

            AddVital(Vitals.Mana, (int)Math.Round(manaRecovered * vampire));
            target.SubVital(Vitals.Mana, (int)manaRecovered);
            recovered = manaRecovered;
            return true;
        }

        public override void SendAttackAnimation(Entity enemy)
        {
            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            List<AttackTypes> attackTypes = new List<AttackTypes>();
            if (weapon == default || weapon?.Descriptor == null)
            {
                // Unarmed attack
                attackTypes.Add(AttackTypes.Blunt);
                SendUnarmedAttackAnimation();
            }
            else
            {
                attackTypes.AddRange(weapon.Descriptor.AttackTypes);
                SendWeaponAttackAnimation(weapon.Descriptor);
            }
        }

        private void SendUnarmedAttackAnimation()
        {
            var classBase = ClassBase.Get(ClassId);
            var attackingTile = GetMeleeAttackTile();

            if (attackingTile.TryFix())
            {
                PacketSender.SendAnimationToProximity(
                    classBase.AttackAnimationId, -1, Id, attackingTile.GetMapId(), attackingTile.GetX(),
                    attackingTile.GetY(), (sbyte)Dir, MapInstanceId
                );
            }
        }

        private void SendWeaponAttackAnimation(ItemBase weaponItem)
        {
            var attackAnim = weaponItem.AttackAnimation;
            var attackingTile = GetMeleeAttackTile();

            var projectile = weaponItem.Projectile;

            if (attackAnim != null && attackingTile.TryFix() && (projectile == null || (projectile?.FakeMelee ?? false)))
            {
                PacketSender.SendAnimationToProximity(
                    attackAnim.Id, -1, Id, attackingTile.GetMapId(), attackingTile.GetX(),
                    attackingTile.GetY(), (sbyte)Dir, MapInstanceId
                );
            }
        }

        public bool TrySpawnWeaponProjectile(long latencyAdjustmentMs)
        {
            if (!TryGetEquippedItem(Options.WeaponIndex, out var weapon))
            {
                return false;
            }

            var projectile = weapon.Descriptor?.Projectile;
            if (projectile == default)
            {
                return false;
            }

            if (!TryConsumeProjectileAmmo(projectile))
            {
                PacketSender.SendChatMsg(
                           this,
                           Strings.Items.notenough.ToString(ItemBase.GetName(projectile.AmmoItemId)),
                           ChatMessageType.Inventory,
                           CustomColors.General.GeneralWarning
                       );
                PacketSender.SendPlaySound(this, Options.UIDenySound);
                return false;
            }

            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
            {
                mapInstance
                        .SpawnMapProjectile(
                            this, projectile, null, weapon.Descriptor, MapId,
                            (byte)X, (byte)Y, (byte)Z,
                            (byte)Dir, null
                        );

                AttackTimer = Timing.Global.Milliseconds +
                    latencyAdjustmentMs +
                    CalculateAttackTime();
            }

            return true;
        }

        public ThreatLevel GetThreatLevelInPartyFor(Npc npc)
        {
            var partyVitals = Party.Select(member => member.MaxVitals).ToArray();
            var partyStats = Party.Select(member => member.StatVals).ToArray();
            var partyAttackTypes = Party.Select(member => member.GetMeleeAttackTypes()).ToArray();
            var partyAttackSpeeds = Party.Select(member => GetRawAttackSpeed()).ToArray();
            var rangedPartyMembers = Party.Select(member =>
            {
                return (member.GetEquippedWeapon()?.ProjectileId ?? Guid.Empty) != Guid.Empty;
            }).ToArray();

            var damageScalar = 100;
            if (npc.Base.IsSpellcaster)
            {
                Globals.CachedNpcSpellScalar.TryGetValue(npc.Base.Id, out damageScalar);
            }

            return ThreatLevelUtilities.DetermineNpcThreatLevelParty(partyVitals,
               partyStats,
               npc.Base.MaxVital,
               npc.Base.Stats,
               partyAttackTypes,
               npc.Base.AttackTypes,
               partyAttackSpeeds,
               npc.Base.AttackSpeedValue,
               rangedPartyMembers,
               npc.Base.IsSpellcaster,
               damageScalar,
               Party.Count);
        }

        public double GetThreatLevelExpMod(Entity opponent)
        {
            if (opponent == null || !(opponent is Npc npc) || npc.Base == default)
            {
                return 1.0;
            }

            var attackTypes = GetEquippedWeapon()?.AttackTypes ?? new List<AttackTypes>() { AttackTypes.Blunt };

            ThreatLevel threatLevel = ThreatLevel.Fair;

            if (Party?.Count > 1)
            {
                threatLevel = GetThreatLevelInPartyFor(npc);
            }
            else
            {
                var playerProjectile = Guid.Empty;
                if (TryGetEquippedItem(Options.WeaponIndex, out var playerWeapon))
                {
                    playerProjectile = playerWeapon.Descriptor?.ProjectileId ?? Guid.Empty;
                }

                var damageScalar = 100;
                if (npc.Base.IsSpellcaster)
                {
                    Globals.CachedNpcSpellScalar.TryGetValue(npc.Base.Id, out damageScalar);
                }

                threatLevel = ThreatLevelUtilities.DetermineNpcThreatLevel(MaxVitals,
                   StatVals,
                   npc.Base.MaxVital,
                   npc.Base.Stats,
                   attackTypes,
                   npc.Base.AttackTypes,
                   GetRawAttackSpeed(),
                   npc.Base.AttackSpeedValue,
                   playerProjectile != Guid.Empty,
                   npc.Base.IsSpellcaster,
                   damageScalar);
            }

            if (!Options.Combat.ThreatLevelExpRates.TryGetValue(threatLevel, out var expRate))
            {
                return 1.0;
            }

            return expRate;
        }

        public long GetRawAttackSpeed()
        {
            var attackTime = Options.Combat.MinAttackRate;
            var cls = ClassBase.Get(ClassId);
            if (cls != null && cls.AttackSpeedModifier == 1) //Static
            {
                attackTime = cls.AttackSpeedValue;
            }

            var weapon = TryGetEquippedItem(Options.WeaponIndex, out var item) ? item.Descriptor : null;

            if (weapon == null)
            {
                return attackTime;
            }

            if (weapon.AttackSpeedModifier == 1) // Static
            {
                attackTime = weapon.AttackSpeedValue;
            }
            else if (weapon.AttackSpeedModifier == 2) //Percentage
            {
                attackTime = (int)(attackTime * (100f / weapon.AttackSpeedValue));
            }

            return attackTime;
        }

        public List<AttackTypes> GetMeleeAttackTypes()
        {
            var weapon = GetEquippedWeapon();
            if (GetEquippedWeapon() == default)
            {
                return new List<AttackTypes>() { AttackTypes.Blunt };
            }

            return weapon.AttackTypes;
        }

        protected override void AttackingEntity_AttackMissed(Entity target)
        {
            MissFreeStreakEnd();
            RemoveMissedOnHits();
        }

        protected override void AttackingEntity_DamageTaken(Entity aggressor, int damage)
        {
            if (damage > 0)
            {
                HitFreeStreakEnd();
            }
            ChallengeUpdateProcesser.UpdateChallengesOf(new DamageTakenOverTimeUpdate(this, damage));
        }

        public override bool IsNonTrivialTo(Player player)
        {
            return true;
        }

        public override int CalculateSpecialDamage(int baseDamage, int range, ItemBase item, Entity target)
        {
            var originalDamage = baseDamage;

            if (target is Resource) return baseDamage;

            var berzerk = GetBonusEffectTotal(EffectType.Berzerk);
            if (berzerk > 0 && Map.TryGetInstance(MapInstanceId, out var mapInstance))
            {
                var entities = mapInstance.GetCachedEntities();
                var currentAggrod = 0;
                foreach (var entity in entities)
                {
                    if (!(entity is Npc n))
                    {
                        continue;
                    }

                    if (n.Target == this)
                    {
                        currentAggrod++;
                    }
                }

                var berzerkDamageMultiplier = (int)Math.Round(originalDamage / Options.Instance.CombatOpts.BerzerkDamageDivider);
                var berzerkDamage = (int)Math.Round(berzerkDamageMultiplier * (currentAggrod - 1) * (berzerk / 100f));
                if (currentAggrod > 1)
                {
                    baseDamage += Math.Max(0, berzerkDamage);
                }
            }

            var sniper = GetBonusEffectTotal(EffectType.Sniper);
            if (range > 1 && sniper > 0)
            {
                var sniperDamageMultiplier = (int)Math.Round(originalDamage / Options.Instance.CombatOpts.SniperDamageDivider);
                var sniperDamage = (int)Math.Round(sniperDamageMultiplier * range * (sniper / 100f));
                baseDamage += Math.Max(0, sniperDamage);
            }

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
                if (Backstab && canBackstab)
                {
                    var backstabDamage = (int)Math.Floor(originalDamage * ApplyEffectBonusToValue(item.BackstabMultiplier, EffectType.Assassin)) - originalDamage;
                    baseDamage += Math.Max(0, backstabDamage);
                    damageBonus = DamageBonus.Backstab;
                }
                if (StealthAttack && item.ProjectileId == Guid.Empty && canStealth) // Melee weapons only for stealth attacks
                {
                    var stealthDamage = CalculateStealthDamage(originalDamage, item) - originalDamage;
                    baseDamage += Math.Max(0, stealthDamage);
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

            Backstab = false;

            return baseDamage;
        }

        [NotMapped, JsonIgnore]
        public bool StealthAttack { get; set; }

        [NotMapped, JsonIgnore]
        public bool Backstab { get; set; }

        public override void TakeDamage(Entity attacker, int damage, Vitals vital = Vitals.Health)
        {
            if (FadeWarp)
            {
                return;
            }

            base.TakeDamage(attacker, damage, vital);
        }

        public override void HandleOffensiveSpellProccing(Entity enemy)
        {
            if (IsDead() || enemy.IsDead())
            {
                return;
            }

            if (!TryGetEquippedItem(Options.WeaponIndex, out var weapon))
            {
                return;
            }

            var weaponDesc = weapon.Descriptor;
            if (weaponDesc == null)
            {
                return;
            }

            var spellProcs = ItemInstanceHelper.GetSpellProcs(weaponDesc, weapon.ItemProperties.SpellEnhancements);

            var success = false;
            foreach (var spellProc in spellProcs)
            {
                success = success || TryProcSpell(spellProc.Key, spellProc.Value, enemy);
            }
            
            if (success)
            {
                PacketSender.SendActionMsg(this, "PROC!", CustomColors.Combat.MagicDamage);
            }
            base.HandleOffensiveSpellProccing(enemy);
        }

        public bool TryProcSpell(Guid spellId, float chance, Entity enemy)
        {
            var procSpell = SpellBase.Get(spellId);
            var affinity = GetBonusEffectTotal(EffectType.Affinity, 0);

            var roll = Randomization.Next(0, 100001);

            if (roll >= ((chance * 1000) + (affinity * 1000)))
            {
                return false;
            }

            if (procSpell.Combat.TargetType == SpellTargetTypes.Self)
            {
                UseSpell(procSpell, -1, this, true, true, (byte)Dir, this, true);
            }
            else
            {
                HandleAoESpell(spellId, procSpell.Combat?.HitRadius ?? 0, enemy.MapId, enemy.X, enemy.Y, null, true);
            }
            return true;
        }

        public override void HandleDefensiveSpellProccing(Entity attacker)
        {
            if (IsDead() || attacker.IsDead())
            {
                return;
            }

            var spellProcs = new Dictionary<Guid, float>();
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (i == Options.WeaponIndex || !TryGetEquippedItem(i, out var equipment) || equipment == null || equipment.Descriptor == null)
                {
                    continue;
                }

                var newProcs = ItemInstanceHelper.GetSpellProcs(equipment.Descriptor, equipment.ItemProperties.SpellEnhancements);
                if (newProcs.Count == 0)
                {
                    continue;
                }
                
                foreach (var proc in newProcs)
                {
                    spellProcs[proc.Key] = proc.Value;
                }
            }

            var success = false;
            foreach (var spellProc in spellProcs)
            {
                success = success || TryProcSpell(spellProc.Key, spellProc.Value, attacker);
            }

            if (success)
            {
                PacketSender.SendActionMsg(this, "PROC!", CustomColors.Combat.MagicDamage);
            }
            base.HandleDefensiveSpellProccing(attacker);
        }
    }
}
