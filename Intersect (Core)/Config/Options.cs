using System.Collections.Generic;
using System.IO;

using Intersect.Config;
using Intersect.Config.Guilds;
using Newtonsoft.Json;

namespace Intersect
{

    public partial class Options
    {

        //Caching Json
        private static string optionsCompressed = "";

        [JsonProperty("AdminOnly", Order = -3)]
        protected bool _adminOnly = false;

        //Constantly Animated Sprites
        [JsonProperty("AnimatedSprites")] protected List<string> _animatedSprites = new List<string>();

        [JsonProperty("BlockClientRegistrations", Order = -2)]
        protected bool _blockClientRegistrations = false;

        [JsonProperty("ValidPasswordResetTimeMinutes")]
        protected ushort _passResetExpirationMin = 30;

        [JsonProperty("OpenPortChecker", Order = 0)]
        protected bool _portChecker = true;

        [JsonProperty("MaxClientConnections")]
        protected int _maxConnections = 100;

        [JsonProperty("MaximumLoggedinUsers")]
        protected int _maxUsers = 50;

        [JsonProperty("UPnP", Order = -1)] protected bool _upnp = true;

        [JsonProperty("Chat")] public ChatOptions ChatOpts = new ChatOptions();

        [JsonProperty("Combat")] public CombatOptions CombatOpts = new CombatOptions();

        [JsonProperty("Equipment")] public EquipmentOptions EquipmentOpts = new EquipmentOptions();

        [JsonProperty("EventWatchdogKillThreshold")]
        public int EventKillTheshhold = 5000;

        public DatabaseOptions GameDatabase = new DatabaseOptions();

        [JsonProperty("Map")] public MapOptions MapOpts = new MapOptions();

        public DatabaseOptions PlayerDatabase = new DatabaseOptions();

        [JsonProperty("Player")] public PlayerOptions PlayerOpts = new PlayerOptions();

        [JsonProperty("Party")] public PartyOptions PartyOpts = new PartyOptions();

        [JsonProperty("Security")] public SecurityOptions SecurityOpts = new SecurityOptions();

        [JsonProperty("Loot")] public LootOptions LootOpts = new LootOptions();

        public ProcessingOptions Processing = new ProcessingOptions();

        public SpriteOptions Sprites = new SpriteOptions();

        [JsonProperty("Npc")] public NpcOptions NpcOpts = new NpcOptions();

        public MetricsOptions Metrics = new MetricsOptions();

        public PacketOptions Packets = new PacketOptions();

        public SmtpSettings SmtpSettings = new SmtpSettings();

        public QuestOptions Quest = new QuestOptions();

        public GuildOptions Guild = new GuildOptions();

        public LoggingOptions Logging = new LoggingOptions();
        
        public InstancingOptions Instancing = new InstancingOptions();
        
        public NPCGuildOptions NpcGuild = new NPCGuildOptions();

        public BankOptions Bank = new BankOptions();
        
        public ClanWarOptions ClanWar = new ClanWarOptions();

        public static Options Instance { get; private set; }

        [JsonIgnore]
        public bool SendingToClient { get; set; } = true;

        //Public Getters
        public static ushort ServerPort { get => Instance._serverPort; set => Instance._serverPort = value; }

        /// <summary>
        /// Defines the maximum amount of network connections our server is allowed to handle.
        /// </summary>
        public static int MaxConnections => Instance._maxConnections;

        /// <summary>
        /// Defines the maximum amount of logged in users our server is allowed to handle.
        /// </summary>
        public static int MaxLoggedinUsers => Instance._maxUsers;

        public static int MaxStatValue => Instance.PlayerOpts.MaxStat;
        
        public static int MaxNpcStat => Instance.CombatOpts.MaxNpcStat;

        public static int MaxLevel => Instance.PlayerOpts.MaxLevel;

        public static int MaxInvItems => Instance.PlayerOpts.MaxInventory;

        public static int MaxPlayerSkills => Instance.PlayerOpts.MaxSpells;

        public static int MaxCharacters => Instance.PlayerOpts.MaxCharacters;

        public static int ItemDropChance => Instance.PlayerOpts.ItemDropChance;

        public static int RequestTimeout => Instance.PlayerOpts.RequestTimeout;

        public static int TradeRange => Instance.PlayerOpts.TradeRange;

        public static int AttackHealthDivider => Instance.PlayerOpts.AttackHealthDivider;
        
        public static int AbilityPowerManaDivider => Instance.PlayerOpts.AbilityPowerManaDivider;

        public static List<string> DecorSlots => Instance.PlayerOpts.DecorSlots;

        public static int HairSlot => Instance.PlayerOpts.HairSlot;

        public static int BeardSlot => Instance.PlayerOpts.BeardSlot;

        public static int ExtraSlot => Instance.PlayerOpts.ExtraSlot;
        
        public static float AmmoRetrieveChance => Instance.PlayerOpts.AmmoRetrieveChance;
        
        public static string ClassRankVarGuid => Instance.PlayerOpts.ClassRankVarGuid;

        public static string TasksCompletedVarGuid => Instance.PlayerOpts.TasksCompletedVarGuid;

        public static string SpecialAssignmentAvailableGuid => Instance.PlayerOpts.SpecialAssignmentAvailableGuid;

        public static string OnSpecialAssignmentVarGuid => Instance.PlayerOpts.OnSpecialAssignmentVarGuid;

        public static string OnTaskVarGuid => Instance.PlayerOpts.OnTaskVarGuid;

        public static string InGuildVarGuid => Instance.PlayerOpts.InGuildVarGuid;
        
        public static string TaskCompletedVarGuid => Instance.PlayerOpts.TaskCompletedVarGuid;

        public static int Scale => Instance.Sprites.ScaleSize;

        public static int WeaponIndex => Instance.EquipmentOpts.WeaponSlot;

        public static int PrayerIndex => Instance.EquipmentOpts.PrayerSlot;

        public static int HelmetIndex => Instance.EquipmentOpts.HelmetSlot;

        public static int ShieldIndex => Instance.EquipmentOpts.ShieldSlot;

        public static List<string> EquipmentSlots => Instance.EquipmentOpts.Slots;
        
        public static List<string> CosmeticSlots => Instance.EquipmentOpts.Slots;

        public static List<string>[] PaperdollOrder => Instance.EquipmentOpts.Paperdoll.Directions;

        public static List<string> ToolTypes => Instance.EquipmentOpts.ToolTypes;

        public static List<string> AnimatedSprites => Instance._animatedSprites;

        public static int RegenTime => Instance.CombatOpts.RegenTime;

        public static int CombatTime => Instance.CombatOpts.CombatTime;

        public static int BaseComboTime => Instance.CombatOpts.BaseComboTime;
        
        public static int BasePartyComboTime => Instance.CombatOpts.BasePartyComboTime;

        public static float MaxComboExpModifier => Instance.CombatOpts.MaxComboExpModifier;

        public static float BaseComboExpModifier => Instance.CombatOpts.BaseComboExpModifier;

        public static float PartyComboModifier => Instance.CombatOpts.PartyComboModifier;

        public static float HPWarningThreshold => Instance.CombatOpts.HPWarningThreshold;

        public static int MinComboExpLvlDiff => Instance.CombatOpts.MinComboExpLvlDiff;
        
        public static long MPWarningDisplayTime => Instance.CombatOpts.MPWarningDisplayTime;
        
        public static long HPWarningFadeTime => Instance.CombatOpts.HPWarningFadeTime;
        
        public static long WarningFlashRate => Instance.CombatOpts.WarningFlashRate;

        public static bool CombatFlashes => Instance.CombatOpts.CombatFlashes;

        public static float CriticalHitFlashIntensity => Instance.CombatOpts.CriticalHitFlashIntensity;
        
        public static float HitFlashDuration => Instance.CombatOpts.HitFlashDuration;

        public static string CriticalHitReceivedSound => Instance.CombatOpts.CriticalHitReceivedSound;
        
        public static float DamageTakenFlashIntensity => Instance.CombatOpts.DamageTakenFlashIntensity;

        public static float DamageTakenShakeAmount => Instance.CombatOpts.DamageTakenShakeAmount;
        
        public static float DamageGivenShakeAmount => Instance.CombatOpts.DamageGivenShakeAmount;
        
        public static float MaxDamageShakeDistance => Instance.CombatOpts.MaxDamageShakeDistance;
        
        public static float ShakeDeltaDurationDivider => Instance.CombatOpts.ShakeDeltaDurationDivider;
        
        public static string GenericDamageGivenSound => Instance.CombatOpts.GenericDamageGivenSound;
        
        public static string GenericDamageReceivedSound => Instance.CombatOpts.GenericDamageReceivedSound;
        
        public static float ResourceDestroyedShakeAmount => Instance.CombatOpts.ResourceDestroyedShakeAmount;

        public static string CriticalHitDealtSound => Instance.CombatOpts.CriticalHitDealtSound;

        public static long DirChangeTimer => Instance.CombatOpts.DirChangeTimer;

        public static int MinAttackRate => Instance.CombatOpts.MinAttackRate;

        public static int MaxAttackRate => Instance.CombatOpts.MaxAttackRate;

        public static int MaxDashSpeed => Instance.CombatOpts.MaxDashSpeed;

        public static long FaceTargetPredictionTime => Instance.CombatOpts.FaceTargetPredictionTime;
        
        public static long ActionMessageTime => Instance.CombatOpts.ActionMessageTime;
        
        public static string PlayerDeathAnimationId => Instance.CombatOpts.PlayerDeathAnimationId;
        
        public static string MissSound => Instance.CombatOpts.MissSound;

        public static string BlockSound => Instance.CombatOpts.BlockSound;
        
        public static bool HideResourceHealthBars => Instance.CombatOpts.HideResourceHealthBars;

        public static float DefaultBackstabMultiplier => Instance.CombatOpts.DefaultBackstabMultiplier;
        
        public static float SneakAttackMultiplier => Instance.CombatOpts.SneakAttackMultiplier;

        public static int GameBorderStyle => Instance.MapOpts.GameBorderStyle;

        public static bool ZDimensionVisible => Instance.MapOpts.ZDimensionVisible;

        public static int MapWidth => Instance?.MapOpts?.Width ?? 32;

        public static int MapHeight => Instance?.MapOpts?.Height ?? 26;

        public static int TileWidth => Instance.MapOpts.TileWidth;

        public static int TileHeight => Instance.MapOpts.TileHeight;

        public static bool DebugAllowMapFades => Instance.MapOpts.DebugAllowMapFades;

        public static int EventWatchdogKillThreshhold => Instance.EventKillTheshhold;

        public static int MaxChatLength => Instance.ChatOpts.MaxChatLength;

        public static int MinChatInterval => Instance.ChatOpts.MinIntervalBetweenChats;

        public static long MenuNotificationFlashInterval => Instance.ChatOpts.MenuNotificationFlashInterval;
        
        public static string MenuCharacterIcon => Instance.ChatOpts.MenuCharacterIcon;
        
        public static string MenuCharacterIconFlashed => Instance.ChatOpts.MenuCharacterIconFlashed;

        public static string ChatSendSound => Instance.ChatOpts.ChatSendSound;
        
        public static string UIDenySound => Instance.ChatOpts.UIDenySound;
        
        public static string BankSortSound => Instance.ChatOpts.BankSortSound;

        public static string GuildWarsGUID => Instance.Guild.GuildWarsGUID;

        public static bool SharedInstanceRespawnInInstance => Instance.Instancing.SharedInstanceRespawnInInstance;
        
        public static bool RejoinableSharedInstances => Instance.Instancing.RejoinableSharedInstances;
        
        public static int MaxSharedInstanceLives => Instance.Instancing.MaxSharedInstanceLives;
        
        public static bool BootAllFromInstanceWhenOutOfLives => Instance.Instancing.BootAllFromInstanceWhenOutOfLives;
        
        public static int MaxClassRank => Instance.NpcGuild.MaxClassRank;
        
        public static List<int> RequiredTasksPerClassRank => Instance.NpcGuild.RequiredTasksPerClassRank;
        
        public static bool SpecialAssignmentCountsTowardCooldown => Instance.NpcGuild.SpecialAssignmentCountsTowardCooldown;
        
        public static bool PayoutSpecialAssignments => Instance.NpcGuild.PayoutSpecialAssignments;
        
        public static long TaskCooldown => Instance.NpcGuild.TaskCooldownMs;
        
        public static bool SendNpcRecordUpdates => Instance.RecordOpts.SendNpcRecordUpdates;
        
        public static int NpcRecordUpdateInterval => Instance.RecordOpts.NpcRecordUpdateInterval;

        public static bool SendResourceRecordUpdates => Instance.RecordOpts.SendResourceRecordUpdates;

        public static int ResourceRecordUpdateInterval => Instance.RecordOpts.ResourceRecordUpdateInterval;

        public static bool SendCraftingRecordUpdates => Instance.RecordOpts.SendCraftingRecordUpdates;

        public static int CraftingRecordUpdateInterval => Instance.RecordOpts.CraftingRecordUpdateInterval;

        public static LootOptions Loot => Instance.LootOpts;

        public static NpcOptions Npc => Instance.NpcOpts;

        public static PartyOptions Party => Instance.PartyOpts;

        public static ChatOptions Chat => Instance.ChatOpts;

        public static bool UPnP => Instance._upnp;

        public static bool OpenPortChecker => Instance._portChecker;

        public static SmtpSettings Smtp => Instance.SmtpSettings;

        public static int PasswordResetExpirationMinutes => Instance._passResetExpirationMin;

        public static bool AdminOnly { get => Instance._adminOnly; set => Instance._adminOnly = value; }

        public static bool BlockClientRegistrations
        {
            get => Instance._blockClientRegistrations;
            set => Instance._blockClientRegistrations = value;
        }

        public static DatabaseOptions PlayerDb
        {
            get => Instance.PlayerDatabase;
            set => Instance.PlayerDatabase = value;
        }

        public static DatabaseOptions GameDb
        {
            get => Instance.GameDatabase;
            set => Instance.GameDatabase = value;
        }

        public static PlayerOptions Player => Instance.PlayerOpts;

        public static EquipmentOptions Equipment => Instance.EquipmentOpts;

        public static CombatOptions Combat => Instance.CombatOpts;

        public static MapOptions Map => Instance.MapOpts;

        public static bool Loaded => Instance != null;

        [JsonProperty("GameName", Order = -5)]
        public string GameName { get; set; } = DEFAULT_GAME_NAME;

        [JsonProperty("ServerPort", Order = -4)]
        public ushort _serverPort { get; set; } = DEFAULT_SERVER_PORT;

        /// <summary>
        /// Passability configuration by map zone
        /// </summary>
        public Passability Passability { get; } = new Passability();

        public bool SmtpValid { get; set; }

        public static string OptionsData => optionsCompressed;

        public void FixAnimatedSprites()
        {
            for (var i = 0; i < _animatedSprites.Count; i++)
            {
                _animatedSprites[i] = _animatedSprites[i].ToLower();
            }
        }

        public static bool LoadFromDisk()
        {
            Instance = new Options();
            if (!Directory.Exists("resources"))
            {
                Directory.CreateDirectory("resources");
            }

            if (File.Exists("resources/config.json"))
            {
                Instance = JsonConvert.DeserializeObject<Options>(File.ReadAllText("resources/config.json"));
            }

            Instance.SmtpValid = Instance.SmtpSettings.IsValid();
            Instance.SendingToClient = false;
            Instance.FixAnimatedSprites();
            File.WriteAllText("resources/config.json", JsonConvert.SerializeObject(Instance, Formatting.Indented));
            Instance.SendingToClient = true;
            optionsCompressed = JsonConvert.SerializeObject(Instance);

            return true;
        }

        public static void SaveToDisk()
        {
            Instance.SendingToClient = false;
            File.WriteAllText("resources/config.json", JsonConvert.SerializeObject(Instance, Formatting.Indented));
            Instance.SendingToClient = true;
            optionsCompressed = JsonConvert.SerializeObject(Instance);
        }

        public static void LoadFromServer(string data)
        {
            Instance = JsonConvert.DeserializeObject<Options>(data);
        }

        // ReSharper disable once UnusedMember.Global
        public bool ShouldSerializePlayerDatabase()
        {
            return !SendingToClient;
        }

        // ReSharper disable once UnusedMember.Global
        public bool ShouldSerializeGameDatabase()
        {
            return !SendingToClient;
        }

        // ReSharper disable once UnusedMember.Global
        public bool ShouldSerializeSmtpSettings()
        {
            return !SendingToClient;
        }

        // ReSharper disable once UnusedMember.Global
        public bool ShouldSerializeSmtpValid()
        {
            return SendingToClient;
        }

        // ReSharper disable once UnusedMember.Global
        public bool ShouldSerializeSecurityOpts()
        {
            return !SendingToClient;
        }

        #region Constants

        // TODO: Clean these up
        //Values that cannot easily be changed:

        public const int MaxHotbar = 10;

        public const string DEFAULT_GAME_NAME = "Intersect";

        public const int DEFAULT_SERVER_PORT = 5400;

        #endregion

    }

    public partial class Options
    {
        [JsonProperty("Records")] public RecordOptions RecordOpts = new RecordOptions();

        [JsonProperty("LootRolling")] public LootRollOptions LootRollOpts = new LootRollOptions();
        
        [JsonProperty("DeconstructionOptions")] public DeconstructionOptions DeconstructionOpts = new DeconstructionOptions();

        [JsonProperty("DuelOptions")] public DuelOptions DuelOpts = new DuelOptions();

        [JsonProperty("MovementOptions")] public MovementOptions MovementOpts = new MovementOptions();
    }
}
