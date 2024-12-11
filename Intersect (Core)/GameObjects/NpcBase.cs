using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Intersect.Utilities;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{
    /// <summary>
    /// Enumeration of the different immunity options
    /// </summary>
    public enum Immunities
    {
        /// <summary>
        /// Whether the NPC can be affected by knockback
        /// </summary>
        None = 0,
        /// <summary>
        /// Whether the NPC can be affected by knockback
        /// </summary>
        [Description("Knockback")]
        Knockback,
        /// <summary>
        /// Whether the NPC can be affected by silence
        /// </summary>
        [Description("Silence")]
        Silence,
        /// <summary>
        /// Whether the NPC can be affected by stun
        /// </summary>
        [Description("Stun")]
        Stun,
        /// <summary>
        /// Whether the NPC can be affected by snare
        /// </summary>
        [Description("Snare")]
        Snare,
        /// <summary>
        /// Whether the NPC can be affected by blind
        /// </summary>
        [Description("Blind")]
        Blind,
        /// <summary>
        /// Whether the NPC can be affected by transform
        /// </summary>
        [Description("Transform")]
        Transform,
        /// <summary>
        /// Whether the NPC can be affected by sleep
        /// </summary>
        [Description("Sleep")]
        Sleep,
        /// <summary>
        /// Whether the NPC can be affected by taunt
        /// </summary>
        [Description("Taunt")]
        Taunt,

        [Description("Slowness")]
        Slowed,

        [Description("Confusion")]
        Confused,

        [Description("Spellcasting")]
        Spellcasting,
    }

    public partial class NpcBase : DatabaseObject<NpcBase>, IFolderable
    {

        [NotMapped] public ConditionLists AttackOnSightConditions = new ConditionLists();

        [NotMapped] public List<BaseDrop> Drops = new List<BaseDrop>();

        [NotMapped] public int[] MaxVital = new int[(int) Vitals.VitalCount];

        [NotMapped] public ConditionLists PlayerCanAttackConditions = new ConditionLists();

        [NotMapped] public ConditionLists PlayerFriendConditions = new ConditionLists();

        [NotMapped] public int[] Stats = new int[(int) Enums.Stats.StatCount];

        [NotMapped] public int[] VitalRegen = new int[(int) Vitals.VitalCount];

        [NotMapped]
        public Dictionary<Immunities, bool> Immunities = new Dictionary<Immunities, bool>();

        [JsonIgnore]
        [Column("Immunities")]
        public string ImmunitiesJson
        {
            get => JsonConvert.SerializeObject(Immunities);
            set
            {
                Immunities = JsonConvert.DeserializeObject<Dictionary<Immunities, bool>>(value ?? "");
                if (Immunities == null)
                {
                    Immunities = new Dictionary<Immunities, bool>();
                }
            }
        }

        [JsonConstructor]
        public NpcBase(Guid id) : base(id)
        {
            Name = "New Npc";
        }

        //Parameterless constructor for EF
        public NpcBase()
        {
            Name = "New Npc";
        }

        [Column("AggroList")]
        [JsonIgnore]
        public string JsonAggroList
        {
            get => JsonConvert.SerializeObject(AggroList);
            set => AggroList = JsonConvert.DeserializeObject<List<Guid>>(value);
        }

        [NotMapped]
        public List<Guid> AggroList { get; set; } = new List<Guid>();

        public bool AttackAllies { get; set; }

        [Column("AttackAnimation")]
        public Guid AttackAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase AttackAnimation
        {
            get => AnimationBase.Get(AttackAnimationId);
            set => AttackAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("DeathAnimation")]
        public Guid DeathAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase DeathAnimation
        {
            get => AnimationBase.Get(DeathAnimationId);
            set => DeathAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("DeathTransformId")]
        public Guid DeathTransformId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public NpcBase DeathTransform
        {
            get => Get(DeathTransformId);
            set => DeathTransformId = value?.Id ?? Guid.Empty;
        }

        public Guid SpellAttackOverrideId { get; set; }

        //Behavior
        public bool Aggressive { get; set; }

        public byte Movement { get; set; }

        public bool Swarm { get; set; }

        public byte FleeHealthPercentage { get; set; }

        public bool FocusHighestDamageDealer { get; set; } = true;

        public int ResetRadius { get; set; }

        public bool StandStill { get; set; }

        //Conditions
        [Column("PlayerFriendConditions")]
        [JsonIgnore]
        public string PlayerFriendConditionsJson
        {
            get => PlayerFriendConditions.Data();
            set => PlayerFriendConditions.Load(value);
        }

        [Column("AttackOnSightConditions")]
        [JsonIgnore]
        public string AttackOnSightConditionsJson
        {
            get => AttackOnSightConditions.Data();
            set => AttackOnSightConditions.Load(value);
        }

        [Column("PlayerCanAttackConditions")]
        [JsonIgnore]
        public string PlayerCanAttackConditionsJson
        {
            get => PlayerCanAttackConditions.Data();
            set => PlayerCanAttackConditions.Load(value);
        }

        //Combat
        public int Damage { get; set; } = 1;

        public int DamageType { get; set; }

        public int CritChance { get; set; }

        public double CritMultiplier { get; set; } = 1.5;

        public double Tenacity { get; set; } = 0.0;

        public int AttackSpeedModifier { get; set; }

        public int AttackSpeedValue { get; set; }

        //Common Events
        [Column("OnDeathEvent")]
        public Guid OnDeathEventId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EventBase OnDeathEvent
        {
            get => EventBase.Get(OnDeathEventId);
            set => OnDeathEventId = value?.Id ?? Guid.Empty;
        }

        [Column("OnDeathPartyEvent")]
        public Guid OnDeathPartyEventId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public EventBase OnDeathPartyEvent
        {
            get => EventBase.Get(OnDeathPartyEventId);
            set => OnDeathPartyEventId = value?.Id ?? Guid.Empty;
        }

        //Drops
        [Column("Drops")]
        [JsonIgnore]
        public string JsonDrops
        {
            get => JsonConvert.SerializeObject(Drops);
            set => Drops = JsonConvert.DeserializeObject<List<BaseDrop>>(value);
        }

        /// <summary>
        /// If true this npc will drop individual loot for all of those who helped slay it.
        /// </summary>
        public bool IndividualizedLoot { get; set; }

        public long Experience { get; set; }

        public int Level { get; set; } = 1;

        //Vitals & Stats
        [Column("MaxVital")]
        [JsonIgnore]
        public string JsonMaxVital
        {
            get => DatabaseUtils.SaveIntArray(MaxVital, (int) Vitals.VitalCount);
            set => DatabaseUtils.LoadIntArray(ref MaxVital, value, (int) Vitals.VitalCount);
        }

        //NPC vs NPC Combat
        public bool NpcVsNpcEnabled { get; set; }

        public int Scaling { get; set; } = 100;

        public int ScalingStat { get; set; }

        public int SightRange { get; set; }

        //Basic Info
        public int SpawnDuration { get; set; }

        public int SpellFrequency { get; set; } = 2;

        //Spells
        [JsonIgnore]
        [Column("Spells")]
        public string CraftsJson
        {
            get => JsonConvert.SerializeObject(Spells, Formatting.None);
            protected set => Spells = JsonConvert.DeserializeObject<DbList<SpellBase>>(value);
        }

        [NotMapped]
        public DbList<SpellBase> Spells { get; set; } = new DbList<SpellBase>();

        public string Sprite { get; set; } = "";

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
        /// Defines the ARGB color settings for this Npc.
        /// </summary>
        [NotMapped]
        public Color Color { get; set; } = new Color(255, 255, 255, 255);

        [Column("Stats")]
        [JsonIgnore]
        public string JsonStat
        {
            get => DatabaseUtils.SaveIntArray(Stats, (int) Enums.Stats.StatCount);
            set => DatabaseUtils.LoadIntArray(ref Stats, value, (int) Enums.Stats.StatCount);
        }

        //Vital Regen %
        [JsonIgnore]
        [Column("VitalRegen")]
        public string RegenJson
        {
            get => DatabaseUtils.SaveIntArray(VitalRegen, (int) Vitals.VitalCount);
            set => VitalRegen = DatabaseUtils.LoadIntArray(value, (int) Vitals.VitalCount);
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";

        public SpellBase GetRandomSpell(Random random)
        {
            if (Spells == null || Spells.Count == 0)
            {
                return null;
            }

            var spellIndex = random.Next(0, Spells.Count);
            var spellId = Spells[spellIndex];

            return SpellBase.Get(spellId);
        }

    }

    public partial class NpcBase : DatabaseObject<NpcBase>, IFolderable
    {
        [NotMapped] public List<BaseDrop> SecondaryDrops = new List<BaseDrop>();

        [Column("SecondaryDrops")]
        [JsonIgnore]
        public string JsonSecondaryDrops
        {
            get => JsonConvert.SerializeObject(SecondaryDrops);
            set
            {
                SecondaryDrops = JsonConvert.DeserializeObject<List<BaseDrop>>(value ?? string.Empty);
                if (SecondaryDrops == null)
                {
                    SecondaryDrops = new List<BaseDrop>();
                }
            }
        }

        public double SecondaryChance { get; set; } = 0.0;

        [NotMapped] public List<BaseDrop> TertiaryDrops = new List<BaseDrop>();

        [Column("TertiaryDrops")]
        [JsonIgnore]
        public string JsonTertiaryDrops
        {
            get => JsonConvert.SerializeObject(TertiaryDrops);
            set
            {
                TertiaryDrops = JsonConvert.DeserializeObject<List<BaseDrop>>(value ?? string.Empty);
                if (TertiaryDrops == null)
                {
                    TertiaryDrops = new List<BaseDrop>();
                }
            }
        }

        public double TertiaryChance { get; set; } = 0.0;

        [NotMapped]
        public List<AttackTypes> AttackTypes { get; set; } = new List<AttackTypes>();

        [Column("AttackTypes")]
        [JsonIgnore]
        public string AttackTypesJson
        {
            get => JsonConvert.SerializeObject(AttackTypes);
            set => AttackTypes = JsonConvert.DeserializeObject<List<AttackTypes>>(value ?? "") ?? new List<AttackTypes>();
        }

        [NotMapped]
        public Dictionary<int, int> BestiaryUnlocks { get; set; } = new Dictionary<int, int>();

        public bool BeastCompleted(long kc)
        {
            return BestiaryUnlocks.Values.All(reqKc => kc >= reqKc);
        }

        /// <summary>
        /// The amount of bestiary unlocks available for this beast
        /// </summary>
        [NotMapped]
        public int BestiaryUnlockAmount => BestiaryUnlocks.Values.Where(val => val > 0).Count();

        [Column("BestiaryUnlocks")]
        [JsonIgnore]
        public string BestiaryUnlocksJson
        {
            get => JsonConvert.SerializeObject(BestiaryUnlocks);
            set
            {
                BestiaryUnlocks = JsonConvert.DeserializeObject<Dictionary<int, int>>(value ?? string.Empty);
                if (BestiaryUnlocks == null)
                {
                    BestiaryUnlocks = new Dictionary<int, int>();
                }
            }
        }

        public string Description { get; set; } = string.Empty;

        public bool NotInBestiary { get; set; }

        /// <summary>
        /// Determines whether or not the NPC can be passed through via Dash or other means
        /// </summary>
        public bool Impassable { get; set; }

        /// <summary>
        /// Controls whether or not an NPC can be backstabbed
        /// </summary>
        public bool NoBackstab { get; set; }
        public bool NoStealthBonus { get; set; }

        public bool IsSpellcaster { get; set; }

        // Used to have a cached reference to max spell damage when generating threat level
        public static Dictionary<Guid, int> GenerateNpcSpellScalarDictionary()
        {
            var cachedNpcSpellScalar = new Dictionary<Guid, int>();
            foreach (var kv in Lookup)
            {
                var npc = (NpcBase)kv.Value;

                if (!npc.IsSpellcaster)
                {
                    continue;
                }

                var magicTypeSpells = npc?.Spells?
                    .ToList()
                    .Select(id => SpellBase.Get(id))
                    .Where(s => s.Combat?.DamageTypes.Contains(Enums.AttackTypes.Magic) ?? false)
                    .OrderByDescending(s => s.Combat?.Scaling ?? 0)
                    .ToArray();

                if (magicTypeSpells.Length > 0)
                {
                    cachedNpcSpellScalar[npc.Id] = magicTypeSpells.First()?.Combat?.Scaling ?? 100;
                }
                else
                {
                    cachedNpcSpellScalar[npc.Id] = 100;
                }
            }

            return cachedNpcSpellScalar;
        }

        public bool CannotBeHealed { get; set; }

        public float VitalScaleModifier { get; set; } = 0.2f;

        public int NpcScaleType { get; set; } = (int)Events.NpcScaleType.None;

        public int ScaledTo { get; set; } = 1;

        public int MaxScaledTo { get; set; } = 50;

        public bool PlayerLockedLoot { get; set; }

        public int MeleeLifesteal { get; set; }

        public int MeleeManasteal { get; set; }

        public bool NeverSkipSpellCasting { get; set; }

        public bool HideName { get; set; }

        public bool AnimatedSprite { get; set; }

        public bool SequentialCasting { get; set; }
    }

}
