using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Intersect.Utilities;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{

    public partial class ItemBase : DatabaseObject<ItemBase>, IFolderable
    {

        [NotMapped] public ConditionLists UsageRequirements = new ConditionLists();

        [NotMapped] public ConditionLists DestroyRequirements = new ConditionLists();

        public string CannotUseMessage { get; set; } = "";

        public string CannotDestroyMessage { get; set; } = "";

        public ItemBase()
        {
            Initialize();
        }

        [JsonConstructor]
        public ItemBase(Guid id) : base(id)
        {
            Initialize();
        }

        [Column("Animation")]
        public Guid AnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase Animation
        {
            get => AnimationBase.Get(AnimationId);
            set => AnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("AttackAnimation")]
        [JsonProperty]
        public Guid AttackAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase AttackAnimation
        {
            get => AnimationBase.Get(AttackAnimationId);
            set => AttackAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("EquipmentAnimation")]
        public Guid EquipmentAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase EquipmentAnimation
        {
            get => AnimationBase.Get(EquipmentAnimationId);
            set => EquipmentAnimationId = value?.Id ?? Guid.Empty;
        }

        [NotMapped]
        public List<string> RestrictionStrings
        {
            get => UsageRequirements.ConditionListsToRequirementsString();
        }

        [NotMapped]
        public Dictionary<string, string> StatRestrictions = new Dictionary<string, string>();

        /// <summary>
        /// Defines whether or not this item can be dropped by a player.
        /// </summary>
        [Column("Bound")]   // Not exactly the cleanest solution, since CanDrop and Bound set to true will do the opposite.. But don't want to leave a bogus field!
        public bool CanDrop { get; set; } = true;

        /// <summary>
        /// Defines the percentage chance an item will drop upon player Death.
        /// </summary>
        public int DropChanceOnDeath { get; set; }

        /// <summary>
        /// Defines whether or not this item can be traded by a player to another player.
        /// </summary>
        public bool CanTrade { get; set; } = true;

        /// <summary>
        /// Defines whether or not this item can be sold by a player to a shop.
        /// </summary>
        public bool CanSell { get; set; } = true;

        /// <summary>
        /// Defines whether or not this item can be banked by a player.
        /// </summary>
        public bool CanBank { get; set; } = true;

        /// <summary>
        /// Defines whether or not this item can be guild banked by a player.
        /// </summary>
        public bool CanGuildBank { get; set; } = true;

        /// <summary>
        /// Defines whether or not this item can be placed in a bag by a player.
        /// </summary>
        public bool CanBag { get; set; } = true;

        /// <summary>
        /// Defines whether or not this item CAN be destroyed, if its requirements are met. Destroy takes precedence over drop.
        /// </summary>
        public bool CanDestroy { get; set; } = false;

        public int CritChance { get; set; }

        public double CritMultiplier { get; set; } = 1.5;

        public int Cooldown { get; set; }

        /// <summary>
        /// Defines which cooldown group this item belongs to.
        /// </summary>
        public string CooldownGroup { get; set; } = string.Empty;

        /// <summary>
        /// Defines which item tag this item belongs to.
        /// </summary>
        [NotMapped]
        public List<string> Tags { get; set; } = new List<string>();

        [Column("Tags")]
        [JsonIgnore]
        public string TagsJson
        {
            get => JsonConvert.SerializeObject(Tags);
            set => Tags = JsonConvert.DeserializeObject<List<string>>(value ?? "") ?? new List<string>();
        }

        [NotMapped]
        public Dictionary<Stats, bool> StatLocks { get; set; } = new Dictionary<Stats, bool>();

        [Column("StatLocks")]
        [JsonIgnore]
        public string StatLockJson
        {
            get => JsonConvert.SerializeObject(StatLocks);
            set => StatLocks = JsonConvert.DeserializeObject<Dictionary<Stats, bool>>(value ?? "") ?? new Dictionary<Stats, bool>();
        }

        /// <summary>
        /// Configures whether this should not trigger and be triggered by the global cooldown.
        /// </summary>
        public bool IgnoreGlobalCooldown { get; set; } = false;

        /// <summary>
        /// Configured whether the cooldown of this item should be reduced by the players cooldown reduction
        /// </summary>
        public bool IgnoreCooldownReduction { get; set; } = false;

        public int Damage { get; set; }

        public int DamageType { get; set; }

        public int AttackSpeedModifier { get; set; }

        public int AttackSpeedValue { get; set; }

        public ConsumableData Consumable { get; set; }

        public int EquipmentSlot { get; set; }

        public bool TwoHanded { get; set; }

        public int SlotCount { get; set; }

        [Column("Spell")]
        [JsonProperty]
        public Guid SpellId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public SpellBase Spell
        {
            get => SpellBase.Get(SpellId);
            set => SpellId = value?.Id ?? Guid.Empty;
        }


        [Column("ComboSpell")]
        [JsonProperty]
        public Guid ComboSpellId { get; set; }
        
        [NotMapped]
        [JsonIgnore]
        public SpellBase ComboSpell
        {
            get => SpellBase.Get(ComboSpellId);
            set => ComboSpellId = value?.Id ?? Guid.Empty;
        }

        [Column("ComboInterval")]
        [JsonProperty]
        public int ComboInterval { get; set; } = 1;

        [Column("ComboExpBoost")]
        [JsonProperty]
        public int ComboExpBoost { get; set; }

        public bool QuickCast { get; set; }

        [Column("DestroySpell")]
        public bool SingleUse { get; set; } = true;

        [Column("Event")]
        [JsonProperty]
        public Guid EventId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EventBase Event
        {
            get => EventBase.Get(EventId);
            set => EventId = value?.Id ?? Guid.Empty;
        }

        [Column("Enhancement")]
        [JsonProperty]
        public Guid EnhancementId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EnhancementDescriptor Enhancement
        {
            get => EnhancementDescriptor.Get(EnhancementId);
            set => EnhancementId = value?.Id ?? Guid.Empty;
        }

        public string Description { get; set; } = "";

        public string FemalePaperdoll { get; set; } = "";

        public ItemTypes ItemType { get; set; }

        public string MalePaperdoll { get; set; } = "";

        public string Icon { get; set; } = "";

        /// <summary>
        /// The database compatible version of <see cref="Color"/>
        /// </summary>
        [Column("Color")]
        [JsonIgnore]
        public string JsonColor
        {
            get => JsonConvert.SerializeObject(Color);
            set => Color = !string.IsNullOrWhiteSpace(value) ? JsonConvert.DeserializeObject<Color>(value) : Color.White;
        }

        /// <summary>
        /// Defines the ARGB color settings for this Item.
        /// </summary>
        [NotMapped]
        public Color Color { get; set; }

        public int Price { get; set; }

        public int Rarity { get; set; }

        [Column("Projectile")]
        [JsonProperty]
        public Guid ProjectileId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ProjectileBase Projectile
        {
            get => ProjectileBase.Get(ProjectileId);
            set => ProjectileId = value?.Id ?? Guid.Empty;
        }

        public int Scaling { get; set; }

        public int ScalingStat { get; set; }

        public int Speed { get; set; }

        public bool Stackable { get; set; }

        /// <summary>
        /// Defines how high the item can stack in a player's inventory before starting a new stack.
        /// </summary>
        public int MaxInventoryStack { get; set; } = 1000000;

        /// <summary>
        /// Defines how high the item can stack in a player/guild's bank before starting a new stack.
        /// </summary>
        public int MaxBankStack { get; set; } = 1000000;

        public int StatGrowth { get; set; }

        public int Tool { get; set; } = -1;

        /// <summary>
        /// Helmet: Hides hair decor doll when worn
        /// </summary>
        public bool HideHair { get; set; } = false;

        /// <summary>
        /// Helmet: Hides beard decor doll when worn
        /// </summary>
        public bool HideBeard { get; set; } = false;

        /// <summary>
        /// Helmet: Hides extra decor doll when worn
        /// </summary>
        public bool HideExtra { get; set; } = false;

        public bool DestroyOnInstanceChange { get; set; } = false;

        [Column("VitalsGiven")]
        [JsonIgnore]
        public string VitalsJson
        {
            get => DatabaseUtils.SaveIntArray(VitalsGiven, (int) Vitals.VitalCount);
            set => VitalsGiven = DatabaseUtils.LoadIntArray(value, (int) Vitals.VitalCount);
        }

        [NotMapped]
        public int[] VitalsGiven { get; set; }

        [Column("VitalsRegen")]
        [JsonIgnore]
        public string VitalsRegenJson
        {
            get => DatabaseUtils.SaveIntArray(VitalsRegen, (int) Vitals.VitalCount);
            set => VitalsRegen = DatabaseUtils.LoadIntArray(value, (int) Vitals.VitalCount);
        }

        [NotMapped]
        public int[] VitalsRegen { get; set; }

        [Column("PercentageVitalsGiven")]
        [JsonIgnore]
        public string PercentageVitalsJson
        {
            get => DatabaseUtils.SaveIntArray(PercentageVitalsGiven, (int) Vitals.VitalCount);
            set => PercentageVitalsGiven = DatabaseUtils.LoadIntArray(value, (int) Vitals.VitalCount);
        }

        [NotMapped]
        public int[] PercentageVitalsGiven { get; set; }

        [Column("StatsGiven")]
        [JsonIgnore]
        public string StatsJson
        {
            get => DatabaseUtils.SaveIntArray(StatsGiven, (int) Stats.StatCount);
            set => StatsGiven = DatabaseUtils.LoadIntArray(value, (int) Stats.StatCount);
        }

        [NotMapped]
        public int[] StatsGiven { get; set; }

        [Column("PercentageStatsGiven")]
        [JsonIgnore]
        public string PercentageStatsJson
        {
            get => DatabaseUtils.SaveIntArray(PercentageStatsGiven, (int) Stats.StatCount);
            set => PercentageStatsGiven = DatabaseUtils.LoadIntArray(value, (int) Stats.StatCount);
        }

        [NotMapped]
        public int[] PercentageStatsGiven { get; set; }

        [Column("UsageRequirements")]
        [JsonIgnore]
        public string JsonUsageRequirements
        {
            get => UsageRequirements.Data();
            set => UsageRequirements.Load(value);
        }

        [Column("DestroyRequirements")]
        [JsonIgnore]
        public string JsonDestroyRequirements
        {
            get => DestroyRequirements.Data();
            set => DestroyRequirements.Load(value);
        }

        [JsonIgnore, NotMapped]
        public bool IsStackable => (ItemType == ItemTypes.Currency || Stackable) &&
                                   ItemType != ItemTypes.Equipment &&
                                   ItemType != ItemTypes.Bag;

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public bool CanBackstab { get; set; } = false;
        
        public float BackstabMultiplier { get; set; } = 1.0f;

        /// <summary>
        /// Gets an array of all items sharing the provided cooldown group.
        /// </summary>
        /// <param name="cooldownGroup">The cooldown group to search for.</param>
        /// <returns>Returns an array of <see cref="ItemBase"/> containing all items with the supplied cooldown group.</returns>
        public static ItemBase[] GetCooldownGroup(string cooldownGroup)
        {
            cooldownGroup = cooldownGroup.Trim();

            // No point looking for nothing.
            if (string.IsNullOrWhiteSpace(cooldownGroup))
            {
                return Array.Empty<ItemBase>();
            }

            return Lookup.Where(i => ((ItemBase)i.Value).CooldownGroup.Trim() == cooldownGroup).Select(i => (ItemBase)i.Value).ToArray();
        }

        /// <summary>
        /// Gets all of the tags that have been created for all items in our collection
        /// </summary>
        /// <returns>Returns a list of <see cref="string"/> all tags that have been created in the item editor.</returns>
        public static string[] GetTags()
        {
            List<string> tags = new List<string>();
            ItemBase[] itemsWithTags = Lookup.Where(i => ((ItemBase)i.Value).Tags != null && ((ItemBase)i.Value).Tags.Count > 0).Select(i => (ItemBase)i.Value).ToArray();
            foreach (var item in itemsWithTags)
            {
                foreach (var tag in item.Tags)
                {
                    if (!tags.Contains(tag))
                    {
                        tags.Add(tag);
                    }
                }
            }
            
            return tags.ToArray();
        }

        private void Initialize()
        {
            Name = "New Item";
            Speed = 10; // Set to 10 by default.
            StatsGiven = new int[(int) Stats.StatCount];
            PercentageStatsGiven = new int[(int) Stats.StatCount];
            VitalsGiven = new int[(int) Vitals.VitalCount];
            VitalsRegen = new int[(int) Vitals.VitalCount];
            PercentageVitalsGiven = new int[(int) Vitals.VitalCount];
            Consumable = new ConsumableData();
            Effects = new List<EffectData>();
            Color = new Color(255, 255, 255, 255);
            SpecialAttack = new SpecialAttackData();
        }

    }

    [Owned]
    public class ConsumableData
    {

        public ConsumableType Type { get; set; }

        public int Value { get; set; }

        public int Percentage { get; set; }

    }

    [Owned]
    public class SpecialAttackData
    {
        public Guid SpellId { get; set; }

        public long ChargeTime { get; set; }

        public SpellBase SpecialAttack { get => SpellBase.Get(SpellId) ?? null; }

        public SpecialAttackData()
        {
            SpellId = Guid.Empty;
            ChargeTime = default;
        }
    }

    [Owned]
    public class EffectData
    {

        public EffectData()
        {
            Type = default;
            Percentage = default;
        }

        public EffectData(EffectType type, int percentage)
        {
            Type = type;
            Percentage = percentage;
        }

        public EffectType Type { get; set; }

        public int Percentage { get; set; }

    }

    public partial class ItemBase : DatabaseObject<ItemBase>, IFolderable
    {
        public string TypeDisplayOverride { get; set; } = string.Empty;

        public int StrafeBonus { get; set; }

        public int BackstepBonus { get; set; }

        public bool ShortHair { get; set; }

        public SpecialAttackData SpecialAttack { get; set; }

        [NotMapped]
        public List<EffectData> Effects { get; set; } = new List<EffectData>();

        [Column("Effects")]
        [JsonIgnore]
        public string EffectsJson
        {
            get => JsonConvert.SerializeObject(Effects);
            set => Effects = JsonConvert.DeserializeObject<List<EffectData>>(value ?? "") ?? new List<EffectData>();
        }

        [NotMapped]
        public List<AttackTypes> AttackTypes { get; set; } = new List<AttackTypes>();

        [Column("AttackTypes")]
        [JsonIgnore]
        public string AttackTypesJson
        {
            get => JsonConvert.SerializeObject(AttackTypes);
            set => AttackTypes = JsonConvert.DeserializeObject<List<AttackTypes>>(value ?? "") ?? new List<AttackTypes>();
        }

        public int GetEffectPercentage(EffectType type)
        {
            return Effects
                .FindAll(effect => effect.Type == type)
                .Aggregate(0, (int prev, EffectData next) => prev + next.Percentage);
        }
        
        [NotMapped, JsonIgnore]
        public EffectType[] EffectsEnabled
        {
            get => Effects.Select(effect => effect.Type).ToArray();
        }

        public void SetEffectOfType(EffectType type, int value)
        {
            Effects.FindAll(effect => effect.Type == type).Select(effect => effect.Percentage = value);
        }

        public string CosmeticDisplayName { get; set; }

        [NotMapped, JsonIgnore]
        public List<Guid> WeaponTypes { get; set; } = new List<Guid>();

        [Column("WeaponTypes")]
        public string WeaponTypesJson
        {
            get => JsonConvert.SerializeObject(WeaponTypes);
            set => WeaponTypes = JsonConvert.DeserializeObject<List<Guid>>(value ?? "") ?? new List<Guid>();
        }

        [NotMapped, JsonIgnore]
        public Dictionary<Guid, int> MaxWeaponLevels { get; set; } = new Dictionary<Guid, int>();
        
        [Column("MaxWeaponLevels")]
        public string MaxWeaponLevelsJson
        {
            get => JsonConvert.SerializeObject(MaxWeaponLevels);
            set => MaxWeaponLevels = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(value ?? "") ?? new Dictionary<Guid, int>();
        }

        public bool RareDrop { get; set; }

        [NotMapped, JsonIgnore]
        public Dictionary<Guid, int> WeaponUpgrades { get; set; } = new Dictionary<Guid, int>();

        [Column("WeaponUpgrades")]
        public string WeaponUpgradesJson
        {
            get => JsonConvert.SerializeObject(WeaponUpgrades);
            set => WeaponUpgrades = JsonConvert.DeserializeObject<Dictionary<Guid, int>>(value ?? "") ?? new Dictionary<Guid, int>();
        }

        public bool ReplaceCastingComponents { get; set; }

        public bool StudySuccessful(double luckModifier)
        {
            if (StudyChance <= 0)
            {
                return false;
            }

            if (StudyEnhancement == Guid.Empty)
            {
                return false;
            }

            //Npc drop rates
            var randomChance = Randomization.Next(1, 100001);
            if (randomChance >= (StudyChance * 1000) * luckModifier)
            {
                return false;
            }
            return true;
        }

        public bool ArmorCosmeticUnlocked(double luckModifier)
        {
            if (StudyChance <= 0 || StudyEnhancement != Guid.Empty)
            {
                return false;
            }

            if (ItemType != ItemTypes.Equipment || (string.IsNullOrWhiteSpace(MalePaperdoll) && string.IsNullOrWhiteSpace(FemalePaperdoll))) 
            {
                return false;
            }

            var randomChance = Randomization.Next(1, 100001);
            if (randomChance >= (StudyChance * 1000) * luckModifier)
            {
                return false;
            }
            return true;
        }

        public string SortName { get; set; } = string.Empty;

        public string GetSortName()
        {
            return string.IsNullOrWhiteSpace(SortName) ? Name : SortName;
        }

        public int SkillPoints { get; set; } = 0;

        public bool MeleeConsumable { get; set; }
        
        public bool ClanWarConsumable { get; set; }

        public Guid ProcSpellId { get; set; }

        public float ProcChance { get; set; }

        public Guid AmmoOverrideId { get; set; } = Guid.Empty;

        [NotMapped, JsonIgnore]
        public ItemBase AmmoOverride => Get(AmmoOverrideId);
    }

}
