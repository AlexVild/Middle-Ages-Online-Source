using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Intersect.Utilities;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{
    public class SpellCastingComponent
    {
        public Guid ItemId { get; set; }

        public ItemBase Item { get => ItemBase.Get(ItemId) ?? null; }

        public int Quantity { get; set; }

        public SpellCastingComponent(Guid itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }

    public partial class SpellBase : DatabaseObject<SpellBase>, IFolderable
    {

        [NotMapped] public int[] VitalCost = new int[(int) Vitals.VitalCount];

        [JsonConstructor]
        public SpellBase(Guid id) : base(id)
        {
            Name = "New Spell";
        }

        public SpellBase()
        {
            Name = "New Spell";
        }

        public SpellTypes SpellType { get; set; }

        public string Description { get; set; } = "";

        public string Icon { get; set; } = "";

        //Animations
        [Column("CastAnimation")]
        public Guid CastAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase CastAnimation
        {
            get => AnimationBase.Get(CastAnimationId);
            set => CastAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("HitAnimation")]
        public Guid HitAnimationId { get; set; }


        [NotMapped]
        [JsonIgnore]
        public AnimationBase HitAnimation
        {
            get => AnimationBase.Get(HitAnimationId);
            set => HitAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("OverTimeAnimation")]
        public Guid OverTimeAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase OverTimeAnimation
        {
            get => AnimationBase.Get(OverTimeAnimationId);
            set => OverTimeAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("TrapAnimation")]
        public Guid TrapAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase TrapAnimation
        {
            get => AnimationBase.Get(TrapAnimationId);
            set => TrapAnimationId = value?.Id ?? Guid.Empty;
        }

        //Spell Times
        public int CastDuration { get; set; }

        public int CooldownDuration { get; set; }

        /// <summary>
        /// Defines which cooldown group this spell belongs to.
        /// </summary>
        public string CooldownGroup { get; set; } = string.Empty;

        /// <summary>
        /// Configures whether this should not trigger and be triggered by the global cooldown.
        /// </summary>
        public bool IgnoreGlobalCooldown { get; set; } = false;

        /// <summary>
        /// Configured whether the cooldown of this spell should be reduced by the players cooldown reduction
        /// </summary>
        public bool IgnoreCooldownReduction { get; set; } = false;

        //Spell Bound
        public bool Bound { get; set; }

        //Spell uses weapon
        public bool WeaponSpell { get; set; }

        //Requirements
        [Column("CastRequirements")]
        [JsonIgnore]
        public string JsonCastRequirements
        {
            get => CastingRequirements.Data();
            set => CastingRequirements.Load(value);
        }

        [NotMapped]
        public List<string> RestrictionStrings
        {
            get => CastingRequirements.ConditionListsToRequirementsString();
        }

        [NotMapped]
        public ConditionLists CastingRequirements { get; set; } = new ConditionLists();

        public string CannotCastMessage { get; set; } = "";

        //Combat Info
        public SpellCombatData Combat { get; set; } = new SpellCombatData();

        //Warp Info
        public SpellWarpData Warp { get; set; } = new SpellWarpData();

        //Dash Info
        public SpellDashOpts Dash { get; set; } = new SpellDashOpts();

        //Event Info
        [Column("Event")]
        public Guid EventId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EventBase Event
        {
            get => EventBase.Get(EventId);
            set => EventId = value?.Id ?? Guid.Empty;
        }

        //Costs
        [Column("VitalCost")]
        [JsonIgnore]
        public string VitalCostJson
        {
            get => DatabaseUtils.SaveIntArray(VitalCost, (int) Vitals.VitalCount);
            set => VitalCost = DatabaseUtils.LoadIntArray(value, (int) Vitals.VitalCount);
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        /// <summary>
        /// Gets an array of all items sharing the provided cooldown group.
        /// </summary>
        /// <param name="cooldownGroup">The cooldown group to search for.</param>
        /// <returns>Returns an array of <see cref="ItemBase"/> containing all items with the supplied cooldown group.</returns>
        public static SpellBase[] GetCooldownGroup(string cooldownGroup)
        {
            cooldownGroup = cooldownGroup.Trim();

            // No point looking for nothing.
            if (string.IsNullOrWhiteSpace(cooldownGroup))
            {
                return Array.Empty<SpellBase>();
            }

            return Lookup.Where(i => ((SpellBase)i.Value).CooldownGroup.Trim() == cooldownGroup).Select(i => (SpellBase)i.Value).ToArray();
        }
    }

    [Owned]
    public class SpellCombatData
    {

        [NotMapped] public int[] VitalDiff = new int[(int)Vitals.VitalCount];

        [NotMapped, JsonIgnore]
        public bool IsDamaging => VitalDiff[(int)Vitals.Health] > 0 || VitalDiff[(int)Vitals.Mana] > 0;

        [NotMapped, JsonIgnore]
        public bool ChangesVitals => VitalDiff[(int)Vitals.Health] != 0 || VitalDiff[(int)Vitals.Mana] != 0;

        public int CritChance { get; set; }

        public double CritMultiplier { get; set; } = 1.0;

        public int DamageType { get; set; } = 1;

        [NotMapped]
        public List<AttackTypes> DamageTypes { get; set; } = new List<AttackTypes>();

        [Column("AttackTypes")]
        [JsonIgnore]
        public string DamageTypesJson
        {
            get => JsonConvert.SerializeObject(DamageTypes);
            set => DamageTypes = JsonConvert.DeserializeObject<List<AttackTypes>>(value ?? "") ?? new List<AttackTypes>();
        }

        public int HitRadius { get; set; }

        public bool Friendly { get; set; }

        public int CastRange { get; set; }

        //Extra Data, Teleport Coords, Custom Spells, Etc
        [Column("Projectile")]
        public Guid ProjectileId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ProjectileBase Projectile
        {
            get => ProjectileBase.Get(ProjectileId);
            set => ProjectileId = value?.Id ?? Guid.Empty;
        }

        //Heal/Damage
        [Column("VitalDiff")]
        [JsonIgnore]
        public string VitalDiffJson
        {
            get => DatabaseUtils.SaveIntArray(VitalDiff, (int)Vitals.VitalCount);
            set => VitalDiff = DatabaseUtils.LoadIntArray(value, (int)Vitals.VitalCount);
        }

        //Buff/Debuff Data
        [Column("StatDiff")]
        [JsonIgnore]
        public string StatDiffJson
        {
            get => DatabaseUtils.SaveIntArray(StatDiff, (int)Stats.StatCount);
            set => StatDiff = DatabaseUtils.LoadIntArray(value, (int)Stats.StatCount);
        }

        [NotMapped]
        public int[] StatDiff { get; set; } = new int[(int)Stats.StatCount];

        //Buff/Debuff Data
        [Column("PercentageStatDiff")]
        [JsonIgnore]
        public string PercentageStatDiffJson
        {
            get => DatabaseUtils.SaveIntArray(PercentageStatDiff, (int)Stats.StatCount);
            set => PercentageStatDiff = DatabaseUtils.LoadIntArray(value, (int)Stats.StatCount);
        }

        [NotMapped]
        public int[] PercentageStatDiff { get; set; } = new int[(int)Stats.StatCount];

        public int Scaling { get; set; } = 100;

        public int ScalingStat { get; set; }

        public SpellTargetTypes TargetType { get; set; }

        public AoeShape AoeShape { get; set; }

        public bool HoTDoT { get; set; }

        public int HotDotInterval { get; set; }

        public int Duration { get; set; }

        public StatusTypes Effect { get; set; }

        public string TransformSprite { get; set; }

        [Column("OnHit")]
        public int OnHitDuration { get; set; }

        public bool PersistWeaponSwap { get; set; }

        public bool PersistMissedAttack { get; set; }

        [Column("Trap")]
        public int TrapDuration { get; set; }

        public bool ManaSteal { get; set; }
        public bool LifeSteal { get; set; }

    }

    [Owned]
    public class SpellWarpData
    {

        public Guid MapId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Dir { get; set; }

    }

    [Owned]
    public class SpellDashOpts
    {

        public bool IgnoreMapBlocks { get; set; }

        public bool IgnoreActiveResources { get; set; }

        public bool IgnoreInactiveResources { get; set; }

        public bool IgnoreZDimensionAttributes { get; set; }

        public bool IgnoreEntites { get; set; }

        public Guid SpellId { get; set; }

        [NotMapped, JsonIgnore]
        public SpellBase Spell
        {
            get => SpellBase.Get(SpellId) ?? null;
            set => SpellId = value?.Id ?? Guid.Empty;
        }


        [Column("DashAnimation")]
        public Guid DashAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase DashAnimation
        {
            get => AnimationBase.Get(DashAnimationId);
            set => DashAnimationId = value?.Id ?? Guid.Empty;
        }
    }

    public partial class SpellBase : DatabaseObject<SpellBase>, IFolderable
    {
        [NotMapped, JsonIgnore]
        public bool RequiresAmmo => Combat != null &&
            Combat.TargetType == SpellTargetTypes.Projectile &&
            Combat.Projectile != null &&
            ((Combat.Projectile.AmmoItemId != null &&
            Combat.Projectile.AmmoItemId != Guid.Empty) ||
            Combat.Projectile.UseAmmoOverride);

        /// <summary>
        /// A mapping of some <see cref="ItemBase"/> ID mapped to its quantity
        /// </summary>
        [NotMapped]
        public List<SpellCastingComponent> CastingComponents { get; set; } = new List<SpellCastingComponent>();

        public string[] GetComponentDisplay()
        {
            var componentDisplays = new List<string>();
            foreach(var component in CastingComponents)
            {
                var notFound = "NOT FOUND";
                var itemName = ItemBase.Get(component.ItemId)?.Name;
                if (itemName.ToLower().Last() == 's' && component.Quantity > 1)
                {
                    itemName = itemName.Trim('s');
                }

                componentDisplays.Add($"{itemName ?? notFound} x{component.Quantity}");
            }

            return componentDisplays.ToArray();
        }

        [Column("CastingComponents")]
        [JsonIgnore]
        public string CastingComponentsJson
        {
            get => JsonConvert.SerializeObject(CastingComponents);
            set => CastingComponents = JsonConvert.DeserializeObject<List<SpellCastingComponent>>(value ?? "") ?? new List<SpellCastingComponent>();
        }

        public void AddCastingComponent(Guid itemId, int quantity)
        {
            if (CastingComponents.Select(component => component.ItemId).Contains(itemId)) 
            {
                var component = CastingComponents.Find(comp => comp.ItemId == itemId);
                component.Quantity = quantity;
                return;
            }
            CastingComponents.Add(new SpellCastingComponent(itemId, quantity));
        }

        public string SpellGroup { get; set; } = string.Empty;

        public int RequiredSkillPoints { get; set; }

        [NotMapped]
        public Dictionary<int, int> DamageOverrides { get; set; } = new Dictionary<int, int>();

        [Column("DamageOverrides")]
        [JsonIgnore]
        public string DamageOverridesJson
        {
            get => JsonConvert.SerializeObject(DamageOverrides);
            set => DamageOverrides = JsonConvert.DeserializeObject<Dictionary<int, int>>(value ?? "") ?? new Dictionary<int, int>();
        }

        [NotMapped]
        [JsonIgnore]
        public static List<SpellTargetTypes> BlockableSpellTypes = new List<SpellTargetTypes>() { SpellTargetTypes.Projectile, SpellTargetTypes.OnHit };

        public bool Blockable
        {
            get
            {
                // All weapon spells are blockable
                if (WeaponSpell)
                {
                    return true;
                }

                // If magic-only attack
                if (Combat.DamageTypes.Count == 1 && Combat.DamageTypes.Contains(AttackTypes.Magic))
                {
                    return false;
                }

                return BlockableSpellTypes.Contains(Combat.TargetType);
            }
        }

        public int InterruptThreshold { get; set; }

        public Guid UpgradeOfSpellId { get; set; }

        [NotMapped, JsonIgnore]
        public SpellBase UpgradeOfSpell 
        {
            get => Get(UpgradeOfSpellId);
            set
            {
                if (value?.Id == default)
                {
                    UpgradeOfSpellId = Guid.Empty;
                    return;
                }

                UpgradeOfSpellId = value.Id;
            }
        }
    }
}
