using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Database;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
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

            if (target is Npc npcTarget && IsAllyOf(npcTarget))
            {
                return false;
            }

            return base.CanMeleeTarget(target);
        }

        public override bool IsCriticalHit(int critChance)
        {
            critChance = CalculateEffectBonus(critChance, EffectType.Affinity);

            return base.IsCriticalHit(critChance);
        }

        public override bool TryDealDamageTo(Entity enemy,
            List<AttackTypes> attackTypes,
            int dmgScaling,
            double critMultiplier,
            ItemBase weapon,
            SpellBase spell,
            bool ignoreEvasion,
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
                    critMultiplier = CalculateEffectBonus(critMultiplier, EffectType.CritBonus);
                }
            }
            else if (weapon != null && spell == null && IsCriticalHit(weapon.CritChance))
            {
                critMultiplier = weapon.CritMultiplier;
                critMultiplier = CalculateEffectBonus(critMultiplier, EffectType.CritBonus);
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
                    critMultiplier += spell.Combat.CritMultiplier;
                    critMultiplier = CalculateEffectBonus(critMultiplier, EffectType.CritBonus);
                }
            }

            // TODO evasion
            // if (!ignoreInvasion)...

            return base.TryDealDamageTo(enemy, attackTypes, dmgScaling, critMultiplier, weapon, spell, ignoreEvasion, out damage);
        }

        public override void MeleeAttack(Entity enemy, bool ignoreEvasion)
        {
            StealthAttack = IsStealthed;

            // Send an attack attempt to the client
            PacketSender.SendEntityAttack(this, CalculateAttackTime());

            // Attack literally missing
            if (!CanMeleeTarget(enemy))
            {
                return;
            }
            
            // Short-circuit out if resource and let resource harvesting logic go
            if (enemy is Resource targetResource && targetResource.Base.Tool >= 0)
            {
                HandleResourceMelee(targetResource);
                return;
            }
            // We're not attacking a resource - disable resource locking
            SetResourceLock(false);

            // If we're doing a player interaction, start those events
            if (enemy is Player pvpTarget)
            {
                pvpTarget.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerInteract, "", Name);
            }

            TryGetEquippedItem(Options.WeaponIndex, out var weapon);
            List<AttackTypes> attackTypes = new List<AttackTypes>();
            if (weapon?.Descriptor?.AttackTypes != null)
            {
                attackTypes.AddRange(weapon.Descriptor.AttackTypes);
            }

            if (!TryDealDamageTo(enemy, attackTypes, 100, 1.0, weapon?.Descriptor, null, false, out int damage))
            {
                return;
            }

            if (TryLifesteal(damage, enemy, out var healthRecovered))
            {
                PacketSender.SendActionMsg(
                    this, Strings.Combat.addsymbol + (int)healthRecovered, CustomColors.Combat.Heal
                );
            }
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

        public override void UseSpell(SpellBase spell, int spellSlot, bool ignoreVitals = false, bool prayerSpell = false, byte prayerSpellDir = 0, Entity prayerTarget = null)
        {
            if (PlayerDead)
            {
                return;
            }

            if (resourceLock != null)
            {
                SetResourceLock(false);
            }

            CastingWeapon = GetEquippedWeapon();

            switch (spell.SpellType)
            {
                case SpellTypes.Event:
                    var evt = spell.Event;
                    if (evt != null)
                    {
                        EnqueueStartCommonEvent(evt);
                    }

                    base.UseSpell(spell, spellSlot, ignoreVitals, prayerSpell, prayerSpellDir, prayerTarget);
                    break;

                default:
                    base.UseSpell(spell, spellSlot, ignoreVitals, prayerSpell, prayerSpellDir, prayerTarget);
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

            scaling += CastingWeapon.Scaling;
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

        protected override bool EntityMeetsCastingRequirements(SpellBase spell)
        {
            if (spell == null)
            {
                throw new ArgumentNullException(nameof(spell));
            }

            if (!SkillPrepared(spell.Id))
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

            if (enemy is Player pvpTarget)
            {
                pvpTarget.StartCommonEventsWithTrigger(CommonEventTrigger.PlayerInteract, "", Name);
                if (!CanPvpPlayer(pvpTarget))
                {
                    return;
                }
            }
            else if (enemy is Resource resource)
            {
                _ = TryHarvestResourceProjectile(resource, projectile);
                return;
            }

            base.ProjectileAttack(enemy, projectile, parentSpell, parentWeapon, false, projectileDir);
        }

        private bool CanPvpPlayer(Player target)
        {
            return !(target == null
                || IsAllyOf(target)
                || Map.ZoneType == MapZones.Safe
                || target.Map?.ZoneType == MapZones.Safe);
        }

        private bool TryLifesteal(int damage, Entity target, out float recovered)
        {
            recovered = 0;
            if (damage <= 0 || target == null || target is Resource) 
            {
                return false;
            }

            var lifesteal = GetEquipmentBonusEffect(EffectType.Lifesteal) / 100f;
            var healthRecovered = lifesteal * damage;
            if (healthRecovered <= 0)
            {
                return false;
            }

            AddVital(Vitals.Health, (int)healthRecovered);
            recovered = healthRecovered;
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
            if (attackAnim != null && attackingTile.TryFix() && weaponItem.ProjectileId == Guid.Empty)
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

            if (projectile.Ammo != default)
            {
                var itemSlot = FindInventoryItemSlot(
                    projectile.AmmoItemId, projectile.AmmoRequired
                );

                if (itemSlot == null)
                {
                    PacketSender.SendChatMsg(
                        this,
                        Strings.Items.notenough.ToString(ItemBase.GetName(projectile.AmmoItemId)),
                        ChatMessageType.Inventory,
                        CustomColors.Combat.NoAmmo
                    );

                    return false;
                }
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
                var partyVitals = Party.Select(member => member.MaxVitals).ToArray();
                var partyStats = Party.Select(member => member.StatVals).ToArray();
                var partyAttackTypes = Party.Select(member => member.GetMeleeAttackTypes()).ToArray();
                var partyAttackSpeeds = Party.Select(member => GetRawAttackSpeed()).ToArray();

                threatLevel = ThreatLevelUtilities.DetermineNpcThreatLevelParty(partyVitals,
                   partyStats,
                   npc.Base.MaxVital,
                   npc.Base.Stats,
                   partyAttackTypes,
                   npc.Base.AttackTypes,
                   partyAttackSpeeds,
                   npc.Base.AttackSpeedValue);
            }
            else
            {
               threatLevel = ThreatLevelUtilities.DetermineNpcThreatLevel(MaxVitals,
                   StatVals,
                   npc.Base.MaxVital,
                   npc.Base.Stats,
                   attackTypes,
                   npc.Base.AttackTypes,
                   GetRawAttackSpeed(),
                   npc.Base.AttackSpeedValue);
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
    }
}
