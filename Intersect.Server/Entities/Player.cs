using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amib.Threading;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.QuestList;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Logging;
using Intersect.Network;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.Logging.Entities;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;

using Newtonsoft.Json;
using Intersect.Server.Entities.PlayerData;
using Intersect.Server.Database.PlayerData;
using static Intersect.Server.Maps.MapInstance;
using Intersect.Server.Core;
using Intersect.GameObjects.Timers;
using Intersect.Server.Utilities;
using System.Text;
using System.ComponentModel;
using Intersect.Server.Core.Games.ClanWars;
using Microsoft.EntityFrameworkCore.Internal;
using Intersect.Server.DTOs;
using Org.BouncyCastle.Bcpg;
using Intersect.Server.Core.Instancing.Controller;
using MimeKit.Cryptography;
using Microsoft.Diagnostics.Runtime.ICorDebug;

namespace Intersect.Server.Entities
{

    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public Guid PreviousMapInstanceId = Guid.Empty;

        //Online Players List
        private static readonly ConcurrentDictionary<Guid, Player> OnlinePlayers = new ConcurrentDictionary<Guid, Player>();

        public static Player[] OnlineList { get; private set; } = new Player[0];

        [NotMapped]
        public bool Online => OnlinePlayers.ContainsKey(Id);

        #region Chat

        [JsonIgnore] [NotMapped] public Player ChatTarget = null;

        #endregion

        [NotMapped, JsonIgnore] public long LastChatTime = -1;

        #region Quests

        [NotMapped, JsonIgnore] public List<Guid> QuestOffers = new List<Guid>();

        #endregion

        #region Event Spawned Npcs

        [JsonIgnore] [NotMapped] public List<Npc> SpawnedNpcs = new List<Npc>();

        #endregion

        public static int OnlineCount => OnlinePlayers.Count;

        [JsonProperty("MaxVitals"), NotMapped]
        public new int[] MaxVitals => GetMaxVitals();

        //Name, X, Y, Dir, Etc all in the base Entity Class
        public Guid ClassId { get; set; }

        [NotMapped]
        public string ClassName => ClassBase.GetName(ClassId);

        public Gender Gender { get; set; }

        public long Exp { get; set; }

        public int StatPoints { get; set; }

        [NotMapped]
        public Resource resourceLock { get; set; }

        [Column("Equipment"), JsonIgnore]
        public string EquipmentJson
        {
            get => DatabaseUtils.SaveIntArray(Equipment, Options.EquipmentSlots.Count);
            set => Equipment = DatabaseUtils.LoadIntArray(value, Options.EquipmentSlots.Count);
        }

        [NotMapped]
        public int[] Equipment { get; set; } = new int[Options.EquipmentSlots.Count];

        /// <summary>
        /// Returns a list of all equipped <see cref="Item"/>s
        /// </summary>
        [NotMapped, JsonIgnore]
        public List<Item> EquippedItems
        {
            get
            {
                var equippedItems = new List<Item>();
                for (var i = 0; i < Options.EquipmentSlots.Count; i++)
                {
                    if (!TryGetEquippedItem(i, out var item))
                    {
                        continue;
                    }
                    equippedItems.Add(item);
                }

                return equippedItems;
            }
        }

        [Column("Decor"), JsonIgnore]
        public string DecorJson
        {
            get => DatabaseUtils.SaveStringArray(Decor, Options.DecorSlots.Count);
            set => Decor = DatabaseUtils.LoadStringArray(value, Options.DecorSlots.Count);
        }

        [NotMapped]
        public string[] Decor { get; set; } = new string[Options.Player.DecorSlots.Count];

        public DateTime? LastOnline { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;

        private ulong mLoadedPlaytime { get; set; } = 0;

        public ulong PlayTimeSeconds
        {
            get
            {
                return mLoadedPlaytime + (ulong)(LoginTime != null ? (DateTime.UtcNow - (DateTime)LoginTime) : TimeSpan.Zero).TotalSeconds;
            }

            set
            {
                mLoadedPlaytime = value;
            }
        }

        [NotMapped]
        public TimeSpan OnlineTime => LoginTime != null ? DateTime.UtcNow - (DateTime)LoginTime : TimeSpan.Zero;

        [NotMapped]
        public DateTime? LoginTime { get; set; }

        //Bank
        [JsonIgnore]
        public virtual List<BankSlot> Bank { get; set; } = new List<BankSlot>();

        //Friends -- Not used outside of EF
        [JsonIgnore]
        public virtual List<Friend> Friends { get; set; } = new List<Friend>();

        //Local Friends
        [NotMapped, JsonProperty("Friends")]
        public virtual Dictionary<Guid, string> CachedFriends { get; set; } = new Dictionary<Guid, string>();

        //HotBar
        [JsonIgnore]
        public virtual List<HotbarSlot> Hotbar { get; set; } = new List<HotbarSlot>();

        //Quests
        [JsonIgnore]
        public virtual List<Quest> Quests { get; set; } = new List<Quest>();

        //Variables
        [JsonIgnore]
        public virtual List<PlayerVariable> Variables { get; set; } = new List<PlayerVariable>();

        [JsonIgnore, NotMapped]
        public bool IsValidPlayer => !IsDisposed && Client?.Entity == this;

        [NotMapped]
        public long ExperienceToNextLevel => GetExperienceToNextLevel(Level);

        [NotMapped, JsonIgnore]
        public long ClientAttackTimer { get; set; }

        [NotMapped, JsonIgnore]
        public long ClientMoveTimer { get; set; }

        private long mAutorunCommonEventTimer { get; set; }

        [NotMapped, JsonIgnore]
        public int CommonAutorunEvents { get; private set; }

        [NotMapped, JsonIgnore]
        public int MapAutorunEvents { get; private set; }

        public long ComboTimestamp { get; set; } = -1; // the timestamp that determines when a combo is no longer valid

        [NotMapped]
        public int ComboWindow { get; set; } = -1;

        [NotMapped]
        public int MaxComboWindow =>  Party?.Count > 1 ? Options.BasePartyComboTime : Options.BaseComboTime;

        [NotMapped]
        public int ComboExp { get; set; } = 0;

        [NotMapped]
        public int CurrentCombo { get; set; } = 0;

        [NotMapped]
        [JsonIgnore]
        public long MPWarningSent { get; set; } = 0; // timestamp

        [NotMapped]
        [JsonIgnore]
        public bool HPWarningSent { get; set; } = false;

        [NotMapped]
        [JsonIgnore]
        public ItemBase CastingWeapon { get; set; }

        /// <summary>
        /// References the in-memory copy of the guild for this player, reference this instead of the Guild property below.
        /// </summary>
        [NotMapped] [JsonIgnore] public Guild Guild { get; set; }

        /// <summary>
        /// This field is used for EF database fields only and should never be assigned to or used, instead the guild instance will be assigned to CachedGuild above
        /// </summary>
        [JsonIgnore] public Guild DbGuild { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Tuple<Player, Guild> GuildInvite { get; set; }

        public int GuildRank { get; set; }

        public DateTime GuildJoinDate { get; set; }

        /// <summary>
        /// Used to determine whether the player is operating in the guild bank vs player bank
        /// </summary>
        [NotMapped] public bool GuildBank;

        // Instancing
        public MapInstanceType InstanceType { get; set; } = MapInstanceType.Overworld;

        [NotMapped, JsonIgnore] public MapInstanceType PreviousMapInstanceType { get; set; } = MapInstanceType.Overworld;

        public Guid PersonalMapInstanceId { get; set; } = Guid.Empty;

        /// <summary>
        /// This instance Id is shared amongst members of a party. Party members will use the shared ID of the party leader.
        /// </summary>
        public Guid SharedMapInstanceId { get; set; } = Guid.Empty;

        public Guid LastClanWarId { get; set; } = Guid.Empty;

        /* This bundle of columns exists so that we have a "non-instanced" location to reference in case we need
         * to kick someone out of an instance for any reason */
        [Column("LastOverworldMapId")]
        [JsonProperty]
        public Guid LastOverworldMapId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public MapBase LastOverworldMap
        {
            get => MapBase.Get(LastOverworldMapId);
            set => LastOverworldMapId = value?.Id ?? Guid.Empty;
        }
        public int LastOverworldX { get; set; }
        public int LastOverworldY { get; set; }

        // For respawning in shared instances (configurable option)
        [Column("SharedInstanceRespawnId")]
        [JsonProperty]
        public Guid SharedInstanceRespawnId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public MapBase SharedInstanceRespawn
        {
            get => MapBase.Get(SharedInstanceRespawnId);
            set => SharedInstanceRespawnId = value?.Id ?? Guid.Empty;
        }
        public int SharedInstanceRespawnX { get; set; }
        public int SharedInstanceRespawnY { get; set; }
        public int SharedInstanceRespawnDir { get; set; }

        public bool InVehicle { get; set; } = false;

        public string VehicleSprite { get; set; } = string.Empty;

        public long VehicleSpeed { get; set; } = 0L;

        public long InspirationTime { get; set; } = 0L;

        [NotMapped]
        public bool CombatMode = false;

        [NotMapped]
        public int FaceDirection = 0;

        // Class Rank Vars
        // Contains a mapping of a Class' GUID -> the class info for this player
        [NotMapped, JsonIgnore]
        public Dictionary<Guid, PlayerClassStats> ClassInfo = new Dictionary<Guid, PlayerClassStats>();

        [NotMapped, JsonIgnore]
        public long ChatErrorLastSent;

        [Column("ClassInfo")]
        public string ClassInfoJson
        {
            get => JsonConvert.SerializeObject(ClassInfo);
            set
            {
                ClassInfo = JsonConvert.DeserializeObject<Dictionary<Guid, PlayerClassStats>>(value ?? "");
                if (ClassInfo == null)
                {
                    ClassInfo = new Dictionary<Guid, PlayerClassStats>();
                }
            }
        }

        public bool InOpenInstance => InstanceType != MapInstanceType.Personal && InstanceType != MapInstanceType.Shared && InstanceType != MapInstanceType.Party;

        public static Player FindOnline(Guid id)
        {
            return OnlinePlayers.ContainsKey(id) ? OnlinePlayers[id] : null;
        }

        public static Player FindOnline(string charName)
        {
            return OnlinePlayers.Values.FirstOrDefault(s => s.Name.ToLower().Trim() == charName.ToLower().Trim());
        }

        public bool ValidateLists()
        {
            var changes = false;

            changes |= SlotHelper.ValidateSlots(Spells, Options.MaxPlayerSkills);
            changes |= SlotHelper.ValidateSlots(Items, Options.MaxInvItems);
            changes |= SlotHelper.ValidateSlots(Bank, Options.Instance.PlayerOpts.InitialBankslots);

            if (Hotbar.Count < Options.MaxHotbar)
            {
                Hotbar.Sort((a, b) => a?.Slot - b?.Slot ?? 0);
                for (var i = Hotbar.Count; i < Options.MaxHotbar; i++)
                {
                    Hotbar.Add(new HotbarSlot(i));
                }

                changes = true;
            }

            return changes;
        }

        private long GetExperienceToNextLevel(int level)
        {
            if (level > Options.MaxLevel)
            {
                SetLevel(Options.MaxLevel, true);
            }
            if (level >= Options.MaxLevel)
            {
                return -1;
            }
            var classBase = ClassBase.Get(ClassId);

            return classBase?.ExperienceToNextLevel(level) ?? ClassBase.DEFAULT_BASE_EXPERIENCE;
        }

        public void SetOnline()
        {
            IsDisposed = false;
            mSentMap = false;
            if (OnlinePlayers.TryGetValue(Id, out var player))
            {
                if (player != this)
                {
                    throw new InvalidOperationException($@"A player with the id {Id} is already listed as online.");
                }
            }

            if (LoginTime == null)
            {
                LoginTime = DateTime.UtcNow;
            }

            if (User != null && User.LoginTime == null)
            {
                User.LoginTime = DateTime.UtcNow;
            }

            LoadFriends();
            LoadGuild();
            DbInterface.Pool.QueueWorkItem(LoadTimers);

            //Upon Sign In Remove Any Items/Spells that have been deleted
            foreach (var itm in Items)
            {
                if (itm.ItemId != Guid.Empty && ItemBase.Get(itm.ItemId) == null)
                {
                    itm.Set(new Item());
                }
            }

            foreach (var itm in Bank)
            {
                if (itm.ItemId != Guid.Empty && ItemBase.Get(itm.ItemId) == null)
                {
                    itm.Set(new Item());
                }
            }

            foreach (var spl in Spells)
            {
                if (spl.SpellId != Guid.Empty && SpellBase.Get(spl.SpellId) == null)
                {
                    spl.Set(new Spell());
                }
            }

            OnlinePlayers[Id] = this;
            OnlineList = OnlinePlayers.Values.ToArray();

            //Send guild list update to all members when coming online
            Guild?.UpdateMemberList();

            // Initialize Class Rank info for any new classes that have been added/underlying updates to CR stuff in Options
            InitClassRanks();

            TrackWeaponTypeProgress(TrackedWeaponType);
            SetMasteryProgress();
            SendPacket(GenerateChallengeProgressPacket());
            PacketSender.SendCraftingWishlist(this);

            // Refresh recipe unlock statuses in the event they've changed since the player last logged in
            RecipeUnlockWatcher.RefreshPlayer(this);

            if (InspirationTime > Timing.Global.MillisecondsUtc)
            {
                SendInspirationUpdateText(-1);
            }

            if (PlayerDead)
            {
                PacketSender.SendPlayerDeathType(this, DeathType.Safe);
            }

            if (InstanceType == MapInstanceType.ClanWar && LastClanWarId == ClanWarManager.CurrentWarId)
            {
                JoinClanWar();
            }

            if (!CraftingDataBackfilled)
            {
                BackfillHistoricalCraftingData();
            }

            ReprocessEnhancements();
            ValidateCurrentWeaponLevels();
        }

        public void SendPacket(IPacket packet, TransmissionMode mode = TransmissionMode.All)
        {
            Client?.Send(packet, mode);
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();

            PacketSender.SendServerDisposedPacket(ClientReference);
        }

        public void TryLogout(bool force = false, bool softLogout = false)
        {
            LastOnline = DateTime.Now;
            ClientReference = Client;
            Client = default;

            if (LoginTime != null)
            {
                PlayTimeSeconds += (ulong)(DateTime.UtcNow - (DateTime)LoginTime).TotalSeconds;
                LoginTime = null;
            }

            if (CombatTimer < Timing.Global.Milliseconds || force)
            {
                Logout(softLogout);
            }
        }
        
        private void RemoveFromInstanceController(Guid mapInstanceId)
        {
            if (InstanceProcessor.TryGetInstanceController(mapInstanceId, out var instanceController))
            {
                instanceController.RemovePlayer(Id);
            }
        }

        private void Logout(bool softLogout = false)
        {
            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
            {
                instance.RemoveEntity(this);
            }

            RemoveFromInstanceController(MapInstanceId);

            //Update parties
            LeaveParty(true);

            if (InstanceType == MapInstanceType.ClanWar)
            {
                LeaveClanWar(true);
            }

            // Update timers
            DbInterface.Pool.QueueWorkItem(LogoutPlayerTimers);

            // End combo
            EndCombo();

            StopCrafting();

            //Update trade
            CancelTrade();

            // Forfeit duel and withdraw from matchmaking
            ForfeitDuel(true);

            mSentMap = false;
            ChatTarget = null;

            //Clear all event spawned NPC's
            var entities = SpawnedNpcs.ToArray();
            foreach (var t in entities)
            {
                if (t == null || t.GetType() != typeof(Npc))
                {
                    continue;
                }

                if (t.Despawnable)
                {
                    lock (t.EntityLock)
                    {
                        t.Die();
                    }
                }
            }

            SpawnedNpcs.Clear();

            EventLookup.Clear();
            EventBaseIdLookup.Clear();
            GlobalPageInstanceLookup.Clear();
            EventTileLookup.Clear();

            InGame = false;
            mSentMap = false;
            mCommonEventLaunches = 0;
            LastMapEntered = Guid.Empty;
            ChatTarget = null;
            QuestOffers.Clear();
            CraftingTableId = Guid.Empty;
            CraftId = Guid.Empty;
            CraftAmount = 0;
            CraftTimer = 0;
            PartyRequester = null;
            PartyRequests.Clear();
            FriendRequester = null;
            FriendRequests.Clear();
            InBag = null;
            BankInterface?.Dispose();
            BankInterface = null;
            InShop = null;

            //Clear cooldowns that have expired
            var keys = SpellCooldowns.Keys.ToArray();
            foreach (var key in keys)
            {
                if (SpellCooldowns.TryGetValue(key, out var time) && time < Timing.Global.MillisecondsUtc)
                {
                    SpellCooldowns.TryRemove(key, out _);
                }
            }

            keys = ItemCooldowns.Keys.ToArray();
            foreach (var key in keys)
            {
                if (ItemCooldowns.TryGetValue(key, out var time) && time < Timing.Global.MillisecondsUtc)
                {
                    ItemCooldowns.TryRemove(key, out _);
                }
            }

            PacketSender.SendEntityLeave(this);

            if (!string.IsNullOrWhiteSpace(Strings.Player.left.ToString()))
            {
                PacketSender.SendGlobalMsg(Strings.Player.left.ToString(Name, Options.Instance.GameName));
            }

            //Remvoe this player from the online list
            if (OnlinePlayers?.ContainsKey(Id) ?? false)
            {
                OnlinePlayers.TryRemove(Id, out Player me);
                OnlineList = OnlinePlayers.Values.ToArray();
            }

            //Send guild update to all members when logging out
            Guild?.UpdateMemberList();
            Guild = null;
            GuildBank = false;

            //If our client has disconnected or logged out but we have kept the user logged in due to being in combat then we should try to logout the user now
            if (Client == null)
            {
                User?.TryLogout(softLogout);
            }

            DbInterface.Pool.QueueWorkItem(CompleteLogout);
        }

        public void CompleteLogout()
        {
            User?.Save();

            Dispose();
        }

        public void RemoveEvent(Guid id, bool sendLeave = true)
        {
            Event outInstance;
            EventLookup.TryRemove(id, out outInstance);
            if (outInstance != null)
            {
                EventBaseIdLookup.TryRemove(outInstance.BaseEvent.Id, out Event evt);
            }
            if (outInstance != null && outInstance.MapId != Guid.Empty)
            {
                //var newTileLookup = new Dictionary<MapTileLoc, Event>(EventTileLookup);
                //newTileLookup.Remove(new MapTileLoc(outInstance.MapId, outInstance.SpawnX, outInstance.SpawnY));
                //EventTileLookup = newTileLookup;
                EventTileLookup.TryRemove(new MapTileLoc(outInstance.MapId, outInstance.SpawnX, outInstance.SpawnY), out Event val);
            }
            if (outInstance?.PageInstance?.GlobalClone != null)
            {
                GlobalPageInstanceLookup.TryRemove(outInstance.PageInstance.GlobalClone, out Event val);
            }
            if (sendLeave && outInstance != null && outInstance.MapId != Guid.Empty)
            {
                PacketSender.SendEntityLeaveTo(this, outInstance);
            }
        }

        //Sending Data
        public override EntityPacket EntityPacket(EntityPacket packet = null, Player forPlayer = null)
        {
            if (packet == null)
            {
                packet = new PlayerEntityPacket();
            }

            packet = base.EntityPacket(packet, forPlayer);

            var pkt = (PlayerEntityPacket) packet;
            pkt.Gender = Gender;
            pkt.ClassId = ClassId;
            pkt.Stats = StatVals;

            if (Power.IsAdmin)
            {
                pkt.AccessLevel = (int) Access.Admin;
            }
            else if (Power.IsModerator)
            {
                pkt.AccessLevel = (int) Access.Moderator;
            }
            else
            {
                pkt.AccessLevel = 0;
            }

            if (CombatTimer > Timing.Global.Milliseconds)
            {
                pkt.CombatTimeRemaining = CombatTimer - Timing.Global.Milliseconds;
            }

            if (forPlayer != null && GetType() == typeof(Player))
            {
                pkt.Equipment =
                    PacketSender.GenerateEquipmentPacket(forPlayer, (Player) this);
            }

            pkt.Guild = Guild?.Name;
            pkt.GuildRank = GuildRank;
            pkt.VehicleSprite = VehicleSprite;
            pkt.VehicleSpeed = VehicleSpeed;
            pkt.InVehicle = InVehicle;

            int[] trueStats = new int[(int)Stats.StatCount];
            for (int i = 0; i < (int) Stats.StatCount; i++)
            {
                trueStats[i] = GetNonBuffedStat((Stats) i);
            }
            pkt.TrueStats = trueStats;

            pkt.IsScaledDown = IsScaledDown;
            pkt.ScaledTo = ScaledTo;

            pkt.MapType = GetCurrentMapType();
            pkt.ClanWarWinner = IsInGuild ? Guild.Id == ClanWarManager.LastWinningGuild : false;

            return pkt;
        }

        private MapType GetCurrentMapType()
        {
            if (Map == null)
            {
                return MapType.None;
            }

            var grid = DbInterface.GetGrid(Map.MapGrid);
            if (grid == null)
            {
                return MapType.None;
            }

            if (grid.HasMap(Options.Instance.MapOpts.FenwyndellMapId))
            {
                return MapType.Overworld;
            }

            if (grid.HasMap(Options.Instance.MapOpts.BattlelandsMapId))
            {
                return MapType.Battlelands;
            }

            return MapType.None;
        }

        public int GetNonBuffedStat(Stats stat)
        {
            return Stat[(int)stat].BaseStat + StatPointAllocations[(int)stat] + PermabuffedStats[(int)stat];
        }

        public override EntityTypes GetEntityType()
        {
            return EntityTypes.Player;
        }

        //Spawning/Dying
        private void Respawn()
        {
            var cls = ClassBase.Get(ClassId);
            if (InDuel)
            {
                LeaveDuel(true);
            }
            else if (cls != null)
            {
                WarpToSpawn();
            }
            else
            {
                Warp(Guid.Empty, 0, 0, 0);
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendRespawnFinished(this);

            //Search death common event trigger
            StartCommonEventsWithTrigger(CommonEventTrigger.OnRespawn);
        }

        public void SetInspirationStatus()
        {
            if (InspirationTime < Timing.Global.MillisecondsUtc)
            {
                return;
            }

            // Below is a silly hack for utilizing statuses to display Inspiration info, tee-hee
            try
            {
                var inspiredSpellId = Guid.Parse(Options.Combat.InspiredSpellId);
                var inspiredSpell = SpellBase.Get(inspiredSpellId);
                if (inspiredSpell != default)
                {
                    _ = new Status(this, this, inspiredSpell, StatusTypes.None, (int)(InspirationTime - Timing.Global.MillisecondsUtc), string.Empty);
                }
            }
            catch (FormatException e)
            {
                Console.Write(e.Message);
            }
        }

        public override void Die(bool dropItems = true, Entity killer = null, bool transform = false)
        {
            // Can't die twice
            if (PlayerDead)
            {
                return;
            }

            // End inspiration
            InspirationTime = Timing.Global.MillisecondsUtc;

            if (killer is Player playerKiller)
            {
                if (InDuel)
                {
                    // Declare a victor
                    playerKiller?.WinMeleeOver(this);

                    CurrentDuel.Lost(this);
                    dropItems = false;
                }
                else
                {
                    playerKiller.AddDeferredEvent(CommonEventTrigger.PVPKill, "", Name);
                    AddDeferredEvent(CommonEventTrigger.PVPDeath, "", killer?.Name);
                    playerKiller.ClanWarKill(this);
                }
            }

            if (InCurrentClanWar)
            {
                LeaveTerritory();
                LastTerritory = null;
                TerritoryLeaveTimer = Timing.Global.MillisecondsUtc;
            }

            var currentMapZoneType = MapController.Get(Map.Id).ZoneType;
            CancelCast();
            CastTarget = null;

            //Flag death to the client
            DestroyVehicle();
            PlayDeathAnimation();
            EndDeathTimers();

            // Force a fade in in case the player died during fade out
            PacketSender.SendFadePacket(Client, true);
            PacketSender.SendPlayerDeath(this);

            //Event trigger
            foreach (var evt in EventLookup.ToArray())
            {
                evt.Value.PlayerHasDied = true;
            }

            // Remove player from ALL threat lists.
            foreach (var instance in MapController.GetSurroundingMapInstances(Map.Id, MapInstanceId, true))
            {
                foreach (var entity in instance.GetCachedEntities())
                {
                    if (entity is Npc npc)
                    {
                        npc.RemoveFromDamageMap(this);
                    }
                }
            }

            lock (EntityLock)
            {
                base.Die(dropItems, killer);
                PlayerDead = true;
            }

            // EXP Loss - don't lose in shared instance, or in an Arena zone
            double expLoss = 0;
            if ((InstanceType != MapInstanceType.Shared || Options.Instance.Instancing.LoseExpOnInstanceDeath) && currentMapZoneType != MapZones.Arena && !InDuel)
            {
                if (Options.Instance.PlayerOpts.ExpLossOnDeathPercent > 0)
                {
                    if (Options.Instance.PlayerOpts.ExpLossFromCurrentExp)
                    {
                        expLoss = (this.Exp * (Options.Instance.PlayerOpts.ExpLossOnDeathPercent / 100.0));
                        TakeExperience((long)expLoss);
                    }
                    else
                    {
                        expLoss = (GetExperienceToNextLevel(this.Level) * (Options.Instance.PlayerOpts.ExpLossOnDeathPercent / 100.0));
                        TakeExperience((long)expLoss);
                    }
                    if (expLoss >= 1)
                    {
                        PacketSender.SendChatMsg(this, Strings.Player.DeathExp.ToString((int)expLoss), ChatMessageType.Notice, CustomColors.General.GeneralDisabled);
                    }
                }
            }
            CombatTimer = 0;
            EndCombo();

            // Subtract from instance lives if in a shared instance...
            if (InstanceType == MapInstanceType.Shared && Options.MaxSharedInstanceLives >= 0 && InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instCtrl))
            {
                // .. but only if the dungeon is active, unless...
                if (instCtrl.Dungeon != null && instCtrl.Dungeon.State == DungeonState.Active)
                {
                    instCtrl.LoseInstanceLife();
                }
                // We aren't in a dungeon but instead leveraging shared instances for a different use
                else if (instCtrl.Dungeon == null)
                {
                    instCtrl.LoseInstanceLife();
                }
            }

            //Remove any damage over time effects
            DoT.Clear();
            CachedDots = new DoT[0];
            Statuses.Clear();
            CachedStatuses = new Status[0];

            if (InDuel)
            {
                PacketSender.SendPlayerDeathType(this, DeathType.Duel);
            }
            else
            {
                PacketSender.SendPlayerDeathType(this, GetDeathType((long)expLoss), (long)expLoss, ItemsLost);
            }
            PacketSender.SendEntityDie(this);
            PacketSender.SendInventory(this);
        }

        public bool InCurrentClanWar => InstanceType == MapInstanceType.ClanWar 
            && ClanWarManager.CurrentWarId == MapInstanceId
            && ClanWarManager.IsInWar(this);

        public void ClanWarKill(Player slainPlayer)
        {
            lock (TerritoryLock)
            {
                if (!InCurrentClanWar || !IsInGuild || slainPlayer == null)
                {
                    return;
                }

                var pts = Options.Instance.ClanWar.BasePointsPerKill;
                if (DefendingTerritory && LastTerritory != null)
                {
                    pts += LastTerritory?.Territory?.PointsPerDefend ?? 0;
                }
                else if (slainPlayer.DefendingTerritory && slainPlayer.LastTerritory != null)
                {
                    pts += LastTerritory?.Territory?.PointsPerAttack ?? 0;
                }

                ClanWarManager.ChangePoints(Guild?.Id ?? Guid.Empty, pts);
            }
        }

        /// <summary>
        /// Ends all player timers associated with this player that are meant to end on death
        /// </summary>
        private void EndDeathTimers()
        {
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => !t.Descriptor.ContinueOnDeath
                && t.Descriptor.OwnerType == TimerOwnerType.Player
                && t.OwnerId == Id))
            {
                TimerProcessor.RemoveTimer(timer);
            }
        }

        /// <summary>
        /// Ends all player timers associated with this player that are meant to not persist beyond an instance change
        /// </summary>
        private void EndInstanceChangeTimers()
        {
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => !t.Descriptor.ContinueOnInstanceChange
                && t.Descriptor.OwnerType == TimerOwnerType.Player
                && t.OwnerId == Id))
            {
                TimerProcessor.RemoveTimer(timer);
            }
        }

        private void DestroyVehicle()
        {
            InVehicle = false;
            VehicleSprite = string.Empty;
            VehicleSpeed = 0L;
            PacketSender.SendEntityDataToProximity(this);
        }

        public override void ProcessRegen()
        {
            if (PlayerDead)
            {
                return;
            }

            Debug.Assert(ClassBase.Lookup != null, "ClassBase.Lookup != null");

            var playerClass = ClassBase.Get(ClassId);
            if (playerClass?.VitalRegen == null)
            {
                return;
            }

            base.ProcessRegen();
        }

        public override void ProcessManaRegen(long timeMs)
        {
            if (PlayerDead)
            {
                return;
            }
            base.ProcessManaRegen(timeMs);
        }

        public override float GetVitalRegenRate(int vital)
        {
            var playerClass = ClassBase.Get(ClassId);
            if (playerClass?.VitalRegen == null)
            {
                return 0f;
            }

            return (playerClass.VitalRegen[vital] + GetEquipmentVitalRegen((Vitals)vital)) / 100f;
        }

        public override int GetMaxVital(int vital)
        {
            var classDescriptor = ClassBase.Get(ClassId);
            var maxVital = 20;
            if (classDescriptor != null)
            {
                if (classDescriptor.IncreasePercentage)
                {
                    maxVital = (int) (classDescriptor.BaseVital[vital] *
                                        Math.Pow(1 + (double) classDescriptor.VitalIncrease[vital] / 100, Level - 1));
                }
                else
                {
                    maxVital = classDescriptor.BaseVital[vital] + classDescriptor.VitalIncrease[vital] * (Level - 1);
                }
            }

            maxVital += VitalPointAllocations[vital];
            var baseVital = maxVital;

            foreach(var item in EquippedItems)
            {
                var descriptor = item.Descriptor;
                if (descriptor != null)
                {
                    maxVital += descriptor.VitalsGiven[vital] + (int)Math.Ceiling(descriptor.PercentageVitalsGiven[vital] * baseVital / 100f);
                }

                if (item.ItemProperties != null)
                {
                    maxVital += item.ItemProperties.VitalEnhancements[vital];
                }
            }

            maxVital += PermabuffedVitals[vital];
            maxVital += GetChallengeVitalBuffs((Vitals)vital).Item1;

            //Must have at least 1 hp and no less than 0 mp
            if (vital == (int) Vitals.Health)
            {
                maxVital = Math.Max(maxVital, 1);
            }
            else if (vital == (int) Vitals.Mana)
            {
                maxVital = Math.Max(maxVital, 0);
            }

            return maxVital;
        }

        public int GetStatValue(Stats stat)
        {
            var playerClass = ClassBase.Get(ClassId);
            var statIncrease = BaseStats[(int)stat];
            if (playerClass.IncreasePercentage) //% increase per level
            {
                statIncrease = (int)(statIncrease * Math.Pow(1 + (double)playerClass.StatIncrease[(int)stat] / 100, Level - 1));
            }
            else //Static value increase per level
            {
                statIncrease += playerClass.StatIncrease[(int)stat] * (Level - 1);
            }

            var cappedStat = Math.Min(StatPointAllocations[(int)stat] + statIncrease, 5); 

            return cappedStat;
        }

        public override int GetMaxVital(Vitals vital)
        {
            return GetMaxVital((int) vital);
        }

        public void FixVitals()
        {
            //If add/remove equipment then our vitals might exceed the new max.. this should fix those cases.
            SetVital(Vitals.Health, GetVital(Vitals.Health));
            SetVital(Vitals.Mana, GetVital(Vitals.Mana));
        }

        //Leveling
        public void SetLevel(int level, bool resetExperience = false)
        {
            if (level < 1)
            {
                return;
            }

            var prevLevel = Level;
            Level = Math.Min(Options.MaxLevel, level);
            if (resetExperience)
            {
                Exp = 0;
            }

            RecalculateStatsAndPoints();
            UnequipInvalidItems();
            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendPointsTo(this);
            PacketSender.SendExperience(this);

            if (StatPoints > 0)
            {
                PacketSender.SendChatMsg(
                    this, Strings.Player.statpoints.ToString(StatPoints), ChatMessageType.Experience, CustomColors.Combat.StatPoints, Name
                );
            }
            
            var clsDescriptor = ClassBase.Get(ClassId);
            if (Level % clsDescriptor.SkillPointLevelModulo == 0)
            {
                PacketSender.SendChatMsg(
                    this, Strings.Player.SkillPoints.ToString(clsDescriptor.SkillPointsPerLevel), ChatMessageType.Experience, CustomColors.Combat.StatPoints, Name
                );
            }
            
            PacketSender.SendChatMsg(this, Strings.Player.levelup.ToString(Level), ChatMessageType.Experience, CustomColors.Combat.LevelUp, Name);
            PacketSender.SendActionMsg(this, Strings.Combat.levelup, CustomColors.Combat.LevelUp);
        }

        public void LevelUp(bool resetExperience = true, int levels = 1)
        {
            if (Level >= Options.MaxLevel)
            {
                return;
            }

            SetLevel(Level + levels, resetExperience);
            LearnLevelledClassSpells(ClassBase.Get(ClassId));
            UnequipInvalidItems();
            StartCommonEventsWithTrigger(CommonEventTrigger.LevelUp);
            RecipeUnlockWatcher.RefreshPlayer(this);
        }

        public void GiveExperience(long amount, bool partyCombo = false, Entity opponent = null, bool fromComboEnd = false, bool sendToast = true)
        {
            if (amount == 0)
            {
                return;
            }

            var threatLevelExpMod = GetThreatLevelExpMod(opponent);
            amount = (int)Math.Ceiling(threatLevelExpMod * amount);

            var bonusAmount = 0;

            if (opponent != null)
            {
                bonusAmount = GetBonusEffectTotal(EffectType.EXP, 0);

                var expBoostAmt = StatusCount(StatusTypes.ExpBoost);
                if (expBoostAmt > 0)
                {
                    bonusAmount += (Options.Instance.CombatOpts.ExpBoostStatusPercent * expBoostAmt);
                }
            }

            var expToGive = amount + (int)(amount * bonusAmount / 100);

            // Award combo EXP if opponent was NPC or player; do not reward if threat level is trivial
            if (CurrentCombo > 0 && (opponent is Npc || opponent is Player) && threatLevelExpMod != Options.Instance.CombatOpts.ThreatLevelExpRates[ThreatLevel.Trivial])
            {
                ComboExp += CalculateComboExperience(expToGive, partyCombo, opponent.TierLevel);

                // For ensuring that combo EXP challenge tracking remains truthful - avoids weapon switch exploit
                if (!InvalidateChallenge)
                {
                    WeaponComboExp += CalculateComboExperience(expToGive, partyCombo, opponent.TierLevel);
                }
                else
                {
                    WeaponComboExp = 0;
                }
            }

            if (Level < Options.MaxLevel)
            {
                Exp += expToGive;
                if (Exp < 0)
                {
                    Exp = 0;
                }
            }

            var weaponProgressed = false;
            if ((opponent is Npc || opponent is Player) || fromComboEnd)
            {
                var weapon = GetEquippedWeapon();
                foreach (var type in weapon?.WeaponTypes ?? new List<Guid>())
                {
                    if (InvalidateChallenge)
                    {
                        continue;
                    }
                    weaponProgressed = TryProgressMastery(expToGive, type) || weaponProgressed;
                    if (type == TrackedWeaponType)
                    {
                        TrackWeaponTypeProgress(type);
                    }
                }
            }

            if (fromComboEnd)
            {
                ChallengeUpdateProcesser.UpdateChallengesOf(new ComboExpEarned(this, WeaponComboExp));
                WeaponComboExp = 0;
                InvalidateChallenge = false;
            }

            if (expToGive > 0 && sendToast)
            {
                if (!fromComboEnd)
                {
                    PacketSender.SendExpToast(this, $"{expToGive} EXP", fromComboEnd, expToGive > 0, weaponProgressed);
                }
                else
                {
                    PacketSender.SendExpToast(this, $"COMBO! {expToGive} EXP", fromComboEnd, expToGive > 0, weaponProgressed);
                }
            }

            CheckLevelUp();
            PacketSender.SendExperience(this, ComboExp);
        }

        public void TakeExperience(long amount)
        {
            Exp -= amount;
            if (Exp < 0)
            {
                Exp = 0;
            }

            PacketSender.SendExperience(this);
        }

        private bool CheckLevelUp()
        {
            var levelCount = 0;
            while (Exp >= GetExperienceToNextLevel(Level + levelCount) &&
                   GetExperienceToNextLevel(Level + levelCount) > 0)
            {
                Exp -= GetExperienceToNextLevel(Level + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            LevelUp(false, levelCount);

            return true;
        }

        //Combat
        public override void KilledEntity(Entity entity)
        {
            switch (entity)
            {
                case Npc npc:
                {
                    var descriptor = npc.Base;
                    var playerEvent = descriptor.OnDeathEvent;
                    var partyEvent = descriptor.OnDeathPartyEvent;

                    // If in party, split the exp.
                    if (Party != null && Party.Count > 0)
                    {
                        var partyMembersInXpRange = Party.Where(partyMember => partyMember.InRangeOf(this, Options.Party.SharedXpRange)).ToArray();
                        float bonusExp = Options.Instance.PartyOpts.BonusExperiencePercentPerMember / 100;
                        var multiplier = 1.0f + (partyMembersInXpRange.Length * bonusExp);
                        var partyExperience = (int)(descriptor.Experience * multiplier) / (Math.Max(1, partyMembersInXpRange.Length));
                        foreach (var partyMember in partyMembersInXpRange)
                        {
                            if (partyMember.PlayerDead)
                            {
                                continue;
                            }
                            partyMember.GiveExperience(partyExperience, true, entity);
                            partyMember.UpdateQuestKillTasks(entity);
                            partyMember.UpdateComboTime(entity.TierLevel);
                        }

                        if (partyEvent != null)
                        {
                            foreach (var partyMember in Party)
                            {
                                if ((Options.Party.NpcDeathCommonEventStartRange <= 0 || partyMember.InRangeOf(this, Options.Party.NpcDeathCommonEventStartRange)) && !(partyMember == this && playerEvent != null))
                                {
                                    partyMember.EnqueueStartCommonEvent(partyEvent);
                                }
                            }
                        }
                    }
                    else
                    {
                        var mobExp = Options.Instance.CombatOpts.UseGeneratedMobExp ? NpcExperienceService.GetExp(descriptor.Id) : descriptor.Experience;

                        GiveExperience(mobExp, false, entity);
                        UpdateComboTime(entity.TierLevel);
                        UpdateQuestKillTasks(entity);
                    }

                    if (playerEvent != null)
                    {
                        EnqueueStartCommonEvent(playerEvent);
                    }

                    _ = TryUsePrayerSpell(npc);

                    break;
                }

                case Resource resource:
                {
                    var descriptor = resource.Base;
                    if (descriptor?.Event != null)
                    {
                        EnqueueStartCommonEvent(descriptor.Event);
                    }

                    break;
                }
            }
        }

        private bool TryUsePrayerSpell(Entity target)
        {
            if (Equipment[Options.PrayerIndex] < 0)
            {
                return false;
            }

            if (target.IsImmuneTo(Immunities.Spellcasting))
            {
                return false;
            }

            var prayer = ItemBase.Get(Items[Equipment[Options.PrayerIndex]].ItemId);
            var prayerSpell = SpellBase.Get(prayer.ComboSpellId);
            // If there's a spell and we're on the right combo interval (every 2 kills, etc)
            if (prayerSpell != default
                && prayer.ComboInterval > 0
                && CurrentCombo % prayer.ComboInterval == 0)
            {
                UseSpell(prayerSpell, -1, target, true, true, (byte)Dir, target);
                return true;
            }

            return false;
        }

        #region Combo Stuff
        private int CalculateComboExperience(long baseAmount, bool partyCombo, int tierLevel)
        {
            // Check to see if a prayer is equipped that modifies this
            var equipBonus = 0.0f;
            if (Equipment[Options.PrayerIndex] >= 0)
            {
                var prayer = ItemBase.Get(Items[Equipment[Options.PrayerIndex]].ItemId);
                equipBonus = prayer.ComboExpBoost / 100f;
            }
            // Cap bonus EXP at double the base exp from the enemy - to prevent anything absolutely bonkers
            var calculatedBonus = MathHelper.Clamp((Options.Combat.BaseComboExpModifier + equipBonus) * CurrentCombo, 0, Options.Combat.MaxComboExpModifier);
            var bonusExp = (int)Math.Floor(baseAmount * calculatedBonus);
            if (partyCombo)
            {
                bonusExp = (int) Math.Floor(bonusExp * Options.Combat.PartyComboModifier);
            }
            return bonusExp;
        }

        ConcurrentQueue<int> ComboEventQueue = new ConcurrentQueue<int>();

        public void UpdateComboTime(int enemyTier = -1)
        {
            lock (EntityLock)
            {
                ComboWindow = MaxComboWindow;
                ComboTimestamp = Timing.Global.Milliseconds + ComboWindow;
                CurrentCombo++;

                AddDeferredEvent(CommonEventTrigger.ComboUp, value: CurrentCombo);
                AddDeferredEvent(CommonEventTrigger.ComboReached, value: CurrentCombo);

                if (enemyTier > -1)
                {
                    ChallengeUpdateProcesser.UpdateChallengesOf(new ComboEarnedUpdate(this), enemyTier);
                }
                PacketSender.SendComboPacket(this, CurrentCombo, ComboWindow, ComboExp, MaxComboWindow);
            }
        }

        public void EndCombo()
        {
            // prevents flooding the client with useless combo packets
            ComboStreakEnd();
            if (CurrentCombo <= 0)
            {
                return;
            }

            var totalComboExp = ComboExp;

            if (TrySetRecord(RecordType.Combo, Guid.Empty, CurrentCombo))
            {
                PacketSender.SendChatMsg(this, Strings.Records.NewHighestCombo.ToString(CurrentCombo), ChatMessageType.Local, Color.FromName("Blue", Strings.Colors.presets));
            }

            ComboTimestamp = -1;
            ComboWindow = -1;
            CurrentCombo = 0;
            ComboExp = 0;

            PacketSender.SendComboPacket(this, CurrentCombo, ComboWindow, ComboExp, MaxComboWindow); // sends the final packet of the combo
            AddDeferredEvent(CommonEventTrigger.ComboEnd);
            
            GiveExperience(totalComboExp, fromComboEnd: true);
        }
        #endregion

        public void UpdateQuestKillTasks(Entity en)
        {
            //If any quests demand that this Npc be killed then let's handle it
            var npc = (Npc)en;
            foreach (var questProgress in Quests)
            {
                var questId = questProgress.QuestId;
                var quest = QuestBase.Get(questId);
                if (quest != null)
                {
                    if (questProgress.TaskId != Guid.Empty)
                    {
                        //Assume this quest is in progress. See if we can find the task in the quest
                        var questTask = quest.FindTask(questProgress.TaskId);
                        if (questTask != null)
                        {
                            if (questTask.Objective == QuestObjective.KillNpcs && questTask.TargetId == npc.Base.Id)
                            {
                                questProgress.TaskProgress++;
                                if (questProgress.TaskProgress >= questTask.Quantity)
                                {
                                    CompleteQuestTask(questId, questProgress.TaskId);
                                }
                                else
                                {
                                    PacketSender.SendQuestsProgress(this);
                                    PacketSender.SendChatMsg(
                                        this,
                                        Strings.Quests.npctask.ToString(
                                            quest.Name, questProgress.TaskProgress, questTask.Quantity,
                                            NpcBase.GetName(questTask.TargetId)
                                        ),
                                        ChatMessageType.Quest
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }

        private ItemBase GetEquippedWeapon()
        {
            if (Options.WeaponIndex > -1 &&
                Options.WeaponIndex < Equipment.Length &&
                Equipment[Options.WeaponIndex] >= 0)
            {
                return ItemBase.Get(Items[Equipment[Options.WeaponIndex]].ItemId);
            } else
            {
                return null;
            }
        }

        public override bool CanAttack(Entity entity, SpellBase spell)
        {
            if (PlayerDead)
            {
                return false;
            }
            // Do not allow spells while in a vehicle
            if (InVehicle && spell != null)
            {
                return false;
            }

            // If self-cast, AoE, Projectile, Trap, or Dash.. always accept.
            if (spell != null && spell?.Combat.TargetType != SpellTargetTypes.Single)
            {
                return true;
            }

            if (!base.CanAttack(entity, spell))
            {
                return false;
            }

            if (entity is EventPageInstance)
            {
                return false;
            }

            var friendly = spell?.Combat != null && spell.Combat.Friendly;
            if (entity is Player player)
            {
                if (player.PlayerDead)
                {
                    return false;
                }
                if (player.IsAllyOf(this))
                {
                    return friendly;
                }
            }

            if (entity is Resource && spell != null)
            {
                return false;
            }

            if (entity is Npc npc)
            {
                return CanAttackNpc(npc, spell);
            }

            return true;
        }

        public bool CanAttackNpc(Npc npc, SpellBase spell = null)
        {
            if (spell != default && npc.IsImmuneTo(Immunities.Spellcasting))
            {
                return false;
            }

            var friendly = spell?.Combat != null && spell.Combat.Friendly;
            if (!npc.CanPlayerAttack(this) && !npc.IsAllyOf(this))
            {
                PacketSender.SendActionMsg(npc, Strings.Combat.invulnerable, CustomColors.Combat.Invulnerable, Options.BlockSound);
            }

            return !friendly && npc.CanPlayerAttack(this) || friendly && npc.IsAllyOf(this);
        }

        public override void NotifySwarm(Entity attacker)
        {
            var mapController = MapController.Get(MapId);
            if (mapController == null) return;

            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
            {
                instance.GetEntities(true).ForEach(
                    entity =>
                    {
                        if (entity is Npc npc &&
                            npc.Target == null &&
                            npc.IsAllyOf(this) &&
                            InRangeOf(npc, npc.Range))
                        {
                            npc.AssignTarget(attacker);
                        }
                    }
                );
            }
        }

        public override int CalculateAttackTime()
        {
            var attackTime = base.CalculateAttackTime();

            var cls = ClassBase.Get(ClassId);
            if (cls != null && cls.AttackSpeedModifier == 1) //Static
            {
                attackTime = cls.AttackSpeedValue;
            }

            var weapon = TryGetEquippedItem(Options.WeaponIndex, out var item) ? item.Descriptor : null;

            if (weapon != null)
            {
                if (weapon.AttackSpeedModifier == 1) // Static
                {
                    if (resourceLock != null)
                    {
                        var resourceId = resourceLock.Base?.Id ?? Guid.Empty;
                        var harvestBonus = Utilities.HarvestBonusHelper.CalculateHarvestBonus(this, resourceId);
                        var effectType = Intersect.Utilities.HarvestBonusHelper.GetBonusEffectForResource(resourceId);
                        if (effectType != EffectType.None)
                        {
                            harvestBonus += GetBonusEffectTotal(effectType) * 0.01;
                        }

                        StatusTypes relevantStatus = Intersect.Utilities.HarvestBonusHelper.GetStatusTypeForResource(resourceId, out int bonus);

                        var statusCount = StatusCount(relevantStatus);
                        if (statusCount > 0)
                        {
                            harvestBonus += bonus * 0.01;
                        };

                        harvestBonus = Math.Min(harvestBonus, 0.8);
                        var speedMod = (int) Math.Floor(weapon.AttackSpeedValue * harvestBonus);

                        attackTime = weapon.AttackSpeedValue - speedMod;
                    }
                    else
                    {
                        attackTime = weapon.AttackSpeedValue;
                    }
                }
                else if (weapon.AttackSpeedModifier == 2) //Percentage
                {
                    attackTime = (int)(attackTime * (100f / weapon.AttackSpeedValue));
                }
            }

            var swiftBonus = (100 - GetBonusEffectTotal(EffectType.Swiftness)) / 100f;
            attackTime = (int)Math.Floor(attackTime * swiftBonus);
            
            if (StatusActive(StatusTypes.Swift))
            {
                attackTime = (int)Math.Floor(attackTime * Options.Instance.CombatOpts.SwiftAttackSpeedMod);
            }

            return
                attackTime -
                100; //subtracting 100 to account for a moderate ping to the server so some attacks dont get cancelled.
        }

        /// <summary>
        /// Get all StatBuffs for the relevant <see cref="Stats"/>
        /// </summary>
        /// <param name="statType">The <see cref="Stats"/> to retrieve the amounts for.</param>
        /// <returns>Returns a <see cref="Tuple"/> containing the Flat stats on Item1, and Percentage stats on Item2</returns>
        public Tuple<int, int> GetItemStatBuffs(Stats statType)
        {
            var flatStats = 0;
            var percentageStats = 0;

            //Add up player equipment values
            foreach (var equippedItem in EquippedItems)
            {
                var descriptor = equippedItem.Descriptor;
                if (descriptor != null)
                {
                    flatStats += descriptor.StatsGiven[(int)statType] + ItemInstanceHelper.GetStatBoost(equippedItem.ItemProperties, statType);
                    percentageStats += descriptor.PercentageStatsGiven[(int)statType];
                }
            }

            return new Tuple<int, int>(flatStats, percentageStats);
        }

        public void LearnLevelledClassSpells(ClassBase playerClass)
        {
            if (playerClass?.Spells?.Count == 0)
            {
                return;
            }

            var spells = playerClass.Spells.Where(clsSpell => clsSpell.Level <= Level).ToArray();
            if (spells.Length == 0)
            {
                return;
            }

            var spellsLearned = new List<string>();
            foreach(var spell in spells)
            {
                if (!TryAddSkillToBook(spell.Id))
                {
                    continue;
                }
                spellsLearned.Add(SpellBase.GetName(spell.Id));
            }

            if (spellsLearned.Count == 0)
            {
                return;
            }

            var spellsLearnedStr = Strings.Player.SpellsLearnedLevel.ToString(Level.ToString(), string.Join(", ", spellsLearned));
            PacketSender.SendChatMsg(this, spellsLearnedStr, ChatMessageType.Experience, CustomColors.General.GeneralCompleted, sendToast: true);
        }

        public void RecalculateStatsAndPoints()
        {
            var oldTotal = LevelSkillPoints;
            var playerClass = ClassBase.Get(ClassId);

            if (playerClass == null)
            {
                return;
            }

            LearnLevelledClassSpells(playerClass);

            LevelSkillPoints = playerClass.GetTotalSkillPointsAt(Level);

            // Validate permabuff'd skillpoints in case their items changed
            ItemSkillPoints = 0;
            PermabuffedStats = new int[(int)Stats.StatCount];
            PermabuffedVitals = new int[(int)Vitals.VitalCount];
            foreach(var permabuff in Permabuffs.Where(pb => pb.Used).ToArray())
            {
                if (permabuff.Item == default)
                {
                    continue;
                }

                ApplyPermabuffsToStats(permabuff.Item, false, false);
            }
            PacketSender.SendEntityStatsTo(Client, this);

            if (oldTotal < LevelSkillPoints)
            {
                PacketSender.SendSkillStatusUpdate(this, "Gained skill points!");
            }
            else if (oldTotal > LevelSkillPoints || SkillPointsAvailable < 0)
            {
                // The player should have less skills - reset them
                UnprepareAllSkills(false);
            }
            else if (oldTotal == Options.Instance.PlayerOpts.MaxSkillPoint)
            {
                PacketSender.SendChatMsg(this, "You can not earn any more skill points.", ChatMessageType.Experience);
            }

            // Send skill point update to client
            PacketSender.SendSkillbookToClient(this);

            var levelsWithoutStatBoosts = (int)Math.Floor((float)Level / playerClass.SkillPointLevelModulo);
            var levelsWithStatBoosts = Level - 1 - levelsWithoutStatBoosts;
            // Calculate stats changes
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                var s = playerClass.BaseStat[i];

                //Add class stat scaling
                if (playerClass.IncreasePercentage) //% increase per level
                {
                    s = (int)(s * Math.Pow(1 + (double)playerClass.StatIncrease[i] / 100, levelsWithStatBoosts));
                }
                else //Static value increase per level
                {
                    s += playerClass.StatIncrease[i] * levelsWithStatBoosts;
                }

                BaseStats[i] = s;
            }

            //Handle Changes in Points
            var currentPoints = StatPoints + 
                (StatPointAllocations.Sum() / Options.Instance.PlayerOpts.BaseStatSkillIncrease) + 
                (VitalPointAllocations.Sum() / Options.Instance.PlayerOpts.BaseVitalPointIncrease);

            var expectedPoints = playerClass.BasePoints + playerClass.PointIncrease * levelsWithStatBoosts;
            expectedPoints += (playerClass.PointIncrease - 1) * levelsWithoutStatBoosts;
            if (expectedPoints > currentPoints)
            {
                StatPoints += expectedPoints - currentPoints;
            }
            else if (expectedPoints < currentPoints)
            {
                var removePoints = currentPoints - expectedPoints;
                StatPoints -= removePoints;
                if (StatPoints < 0)
                {
                    removePoints = Math.Abs(StatPoints);
                    StatPoints = 0;
                }

                var i = 0;
                while (removePoints > 0 && StatPointAllocations.Sum() > 0 && i < (int)Stats.StatCount)
                {
                    if (StatPointAllocations[i] > 0)
                    {
                        StatPointAllocations[i]--;
                        removePoints--;
                    }

                    i++;
                }

                i = 0;
                while (removePoints > 0 && VitalPointAllocations.Sum() > 0 && i < (int)Vitals.VitalCount)
                {
                    if (VitalPointAllocations[i] > 0)
                    {
                        VitalPointAllocations[i]--;
                        removePoints--;
                    }

                    i++;
                }
            }

            // Reset vitals if they are now over their max
            foreach (Vitals vital in Enum.GetValues(typeof(Vitals)))
            {
                if (vital == Vitals.VitalCount)
                {
                    continue;
                }
                if (GetVital(vital) > GetMaxVital(vital))
                {
                    RestoreVital(vital);
                }
            }
        }

        //Warping
        public override void Warp(Guid newMapId, float newX, float newY, bool adminWarp = false)
        {
            EndCombo(); // Don't allow combos to transition between warps, I think? Maybe not.
            Warp(newMapId, newX, newY, (byte) Directions.Up, adminWarp, 0, false);
        }

        public void AdminWarp(Guid newMapId, float newX, float newY, Guid newMapInstanceId, MapInstanceType instanceType, bool force)
        {
            PreviousMapInstanceId = MapInstanceId;
            PreviousMapInstanceType = InstanceType;

            MapInstanceId = newMapInstanceId;
            InstanceType = instanceType;
            EndCombo();
            VoidCurrentDungeon();
            // If we've warped the player out of their overworld, keep a reference to their overworld just in case.
            if (PreviousMapInstanceType == MapInstanceType.Overworld)
            {
                UpdateLastOverworldLocation(MapId, X, Y);
            }
            if (PreviousMapInstanceId != MapInstanceId)
            {
                PacketSender.SendChatMsg(this, Strings.Player.instanceupdate.ToString(PreviousMapInstanceId.ToString(), MapInstanceId.ToString()), ChatMessageType.Admin, CustomColors.Alerts.Info);
            }
            Warp(newMapId, newX, newY, (byte)Directions.Up, forceInstanceChange: force);
        }

        [NotMapped, JsonIgnore]
        public Guid FadeMapId { get; set; }

        [NotMapped, JsonIgnore]
        public float FadeMapX { get; set; }

        [NotMapped, JsonIgnore]
        public float FadeMapY { get; set; }

        [NotMapped, JsonIgnore]
        public byte FadeMapDir { get; set; }

        [NotMapped, JsonIgnore]
        public MapInstanceType FadeMapInstanceType { get; set; }

        public override void Warp(
            Guid newMapId,
            float newX,
            float newY,
            byte newDir,
            bool adminWarp = false,
            byte zOverride = 0,
            bool mapSave = false,
            bool fade = false,
            MapInstanceType mapInstanceType = MapInstanceType.NoChange,
            bool fromLogin = false,
            bool forceInstanceChange = false,
            int instanceLives = 0
        )
        {
            #region shortcircuit exits
            // First, deny the warp entirely if we CAN'T, for some reason, warp to the requested instance type. Only do this if we're not forcing a change
            if (!forceInstanceChange && !CanChangeToInstanceType(mapInstanceType, fromLogin, newMapId))
            {
                return;
            }
            if (fade && Options.DebugAllowMapFades)
            {
                PacketSender.SendFadePacket(Client, false);
                FadeWarp = true;
                FadeMapId = newMapId;
                FadeMapX = newX;
                FadeMapY = newY;
                FadeMapDir = newDir;
                FadeMapInstanceType = mapInstanceType;

                // Tell the client to let us know when it's done fading
                SendPacket(new UpdateFutureWarpPacket());
                return;
            }
            #endregion

            // If we are leaving the overworld to go to a new instance, save the overworld location
            if (!fromLogin && InstanceType == MapInstanceType.Overworld && mapInstanceType != MapInstanceType.Overworld && mapInstanceType != MapInstanceType.NoChange)
            {
                UpdateLastOverworldLocation(MapId, X, Y);
            }
            // If we are moving TO a new shared instance, update the shared respawn point (if enabled)
            if (!fromLogin && mapInstanceType == MapInstanceType.Shared && Options.SharedInstanceRespawnInInstance && MapController.Get(newMapId) != null)
            {
                UpdateSharedInstanceRespawnLocation(newMapId, (int)newX, (int)newY, (int)newDir);
            }

            // Make sure we're heading to a map that exists - otherwise, to spawn you go
            var newMap = MapController.Get(newMapId);
            if (newMap == null)
            {
                WarpToSpawn();

                return;
            }

            X = (int)newX;
            Y = (int)newY;
            Z = zOverride;
            Dir = newDir;

            var newSurroundingMaps = newMap.GetSurroundingMapIds(true);

            #region Map instance traversal
            // Set up player properties if we have changed instance types
            bool onNewInstance = forceInstanceChange || ProcessMapInstanceChange(mapInstanceType, fromLogin);

            // Ensure there exists a map instance with the Player's InstanceId. A player is the sole entity that can create new map instances
            MapInstance newMapInstance;
            lock (EntityLock)
            {
                if (!newMap.TryGetInstance(MapInstanceId, out newMapInstance))
                {
                    // Create a new instance for the map we're on
                    newMap.TryCreateInstance(MapInstanceId, out newMapInstance, this);
                    foreach (var surrMap in newSurroundingMaps)
                    {
                        MapController.Get(surrMap).TryCreateInstance(MapInstanceId, out _, this);
                    }
                }
                else
                {
                    _ = TryAddToInstanceController();
                }
            }

            // An instance of the map MUST exist. Otherwise, head to spawn.
            if (newMapInstance == null)
            {
                Log.Error($"Player {Name} requested a new map Instance with ID {MapInstanceId} and failed to get it.");
                WarpToSpawn();

                return;
            }

            // If we've changed instances, send data to instance entities/entities to player
            if (onNewInstance || forceInstanceChange)
            {
                SendToNewMapInstance(newMap, instanceLives);
                // Clear all events - get fresh ones from the new instance to re-fresh event locations
                foreach (var evt in EventLookup.ToArray())
                {
                    RemoveEvent(evt.Value.Id, false);
                }
            } else
            {
                // Clear events that are no longer on a surrounding map.
                foreach (var evt in EventLookup.ToArray())
                {
                    // Remove events that aren't relevant (on a surrounding map) anymore
                    if (evt.Value.MapId != Guid.Empty && (!newSurroundingMaps.Contains(evt.Value.MapId) || mapSave))
                    {
                        RemoveEvent(evt.Value.Id, false);
                    }
                }
            }
            #endregion

            if (newMapId != MapId || mSentMap == false) // Player warped to a new map?
            {
                // Remove the entity from the old map instance
                var oldMap = MapController.Get(MapId);
                if (oldMap != null && oldMap.TryGetInstance(PreviousMapInstanceId, out var oldMapInstance))
                {
                    oldMapInstance.RemoveEntity(this);
                }

                PacketSender.SendEntityLeave(this); // We simply changed maps - leave the old one
                MapId = newMapId;
                
                // Handle map-exclusive instance timers
                SendInstanceMapExclusiveTimers(newMapId);
                if (oldMap != null)
                {
                    StopInstanceMapExclusiveTimers(oldMap.Id);
                }
                
                newMapInstance.PlayerEnteredMap(this);
                PacketSender.SendEntityPositionToAll(this);

                //If map grid changed then send the new map grid
                if (!adminWarp && (oldMap == null || !oldMap.SurroundingMapIds.Contains(newMapId)) || fromLogin)
                {
                    PacketSender.SendMapGrid(this.Client, newMap.MapGrid, true);
                }

                mSentMap = true;

                AddDeferredEvent(CommonEventTrigger.MapChanged);
            }
            else // Player moved on same map?
            {
                if (onNewInstance)
                {
                    // But instance changed? Add player to the new instance (will also send stats thru SendEntityDataToProximity)
                    newMapInstance.PlayerEnteredMap(this);
                } else
                {
                    PacketSender.SendEntityStatsToProximity(this);
                }
                PacketSender.SendEntityPositionToAll(this);
            }

            if (Options.DebugAllowMapFades && !adminWarp)
            {
                PacketSender.SendFadePacket(Client, true); // fade in by default - either the player was faded out or was not
            }

            if (adminWarp)
            {
                VoidCurrentDungeon();
            }

            // We do this so I can send a cute lil' "Clan War Complete" message to a player once they've warped out of the Clan War
            if (ClanWarComplete != default)
            {
                SendPacket(ClanWarComplete);
                ClanWarComplete = null;
            }

            if (NextDungeonId != Guid.Empty)
            {
                JoinNextDungeon();
            }

            UpdateTerritoryStatus();
        }

        public void JoinNextDungeon()
        {
            if (!InstanceProcessor.TryGetInstanceController(MapInstanceId, out var controller) || NextDungeonId == Guid.Empty)
            {
                if (NextDungeonId != Guid.Empty)
                {
                    Log.Error($"DUNGEON: Failed to create dungeon for {Name} -- could not get instance controller");
                }
                return;
            }

            if (controller.Dungeon == null)
            {
                controller.InitializeDungeon(NextDungeonId);
            }
            controller.TryAddPlayerToDungeon(this);

            NextDungeonId = Guid.Empty;
        }

        /// <summary>
        /// Warps the player on login, taking care of instance management depending on the instance type the player
        /// is attempting to login to.
        /// </summary>
        public void LoginWarp()
        {
            if (MapId == null || MapId == Guid.Empty)
            {
                WarpToSpawn();
                return;
            }
            
            if (!CanChangeToInstanceType(InstanceType, true, MapId))
            {
                WarpToLastOverworldLocation(true);
                return;
            }
            
            if (Map?.ZoneType == MapZones.Arena && !PlayerDead)
            {
                ArenaRespawn(instanceType: InstanceType, fromLogin: true);
                return;
            }

            if (InDuel)
            {
                LeaveDuel(true);
                return;
            }

            // Will warp to spawn if we fail to create an instance for the relevant map
            Warp(
                MapId, (byte)X, (byte)Y, (byte)Dir, false, (byte)Z, false, false, InstanceType, true
            );
        }

        private void ArenaRespawn(MapInstanceType instanceType = MapInstanceType.NoChange, bool fromLogin = false)
        {
            if (Map?.ZoneType == MapZones.Arena && ArenaRespawnMap != null)
            {
                Warp(ArenaRespawnMapId, ArenaRespawnX, ArenaRespawnY, (byte)ArenaRespawnDir, mapInstanceType: instanceType, fromLogin: fromLogin);
            }
            else
            {
                ClassRespawn();
            }
        }

        /// <summary>
        /// Warps the player to the last location they were at on the "Overworld" (empty Guid) map instance. Useful for kicking out of
        /// instances in a variety of situations.
        /// </summary>
        /// <param name="fromLogin">Whether or not we're coming to this method via the player login/join game flow</param>
        public void WarpToLastOverworldLocation(bool fromLogin, bool fade = false)
        {
            if (!fromLogin 
                && InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController)
                && instanceController.InstanceIsDungeon)
            {
                TrackDungeonFailure(instanceController.DungeonId);
            }

            Warp(
                LastOverworldMapId, (byte)LastOverworldX, (byte)LastOverworldY, (byte)Dir, false, (byte)Z, false, fade, MapInstanceType.Overworld, fromLogin
            );
            // If the player was forcibly warped, which they would have been here, we need to kick them out of any vehicle they were in in the instance
            LeaveVehicle();
        }

        public void LeaveVehicle()
        {
            InVehicle = false;
            VehicleSpeed = 0L;
            VehicleSprite = string.Empty;
        }

        public void WarpToSpawn(bool forceClassRespawn = false)
        {
            var mapId = Guid.Empty;
            byte x = 0, y = 0, dir = 0;

            if (!InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController)) 
            {
                ClassRespawn();
            }

            if (Options.SharedInstanceRespawnInInstance && InstanceType == MapInstanceType.Shared && !forceClassRespawn)
            {
                if (SharedInstanceRespawn != null)
                {
                    if (Options.MaxSharedInstanceLives <= 0) // User has not configured shared instances to have lives
                    {
                        // Warp to the start of the shared instance - no concern for life total
                        Warp(SharedInstanceRespawnId, SharedInstanceRespawnX, SharedInstanceRespawnY, (Byte)SharedInstanceRespawnDir);
                    } 
                    else
                    {
                        // Check if the player/party have enough lives to spawn in-instance
                        if (!instanceController.OutOfLives)
                        {
                            Warp(SharedInstanceRespawnId, SharedInstanceRespawnX, SharedInstanceRespawnY, (Byte)SharedInstanceRespawnDir);
                        } 
                        else
                        {
                            // The player has ran out of lives - too bad, back to instance entrance you go.
                            WarpToLastOverworldLocation(false);
                        }
                    }
                } 
                else
                {
                    // invalid map - try overworld (which will throw to class spawn if itself is invalid)
                    WarpToLastOverworldLocation(false);
                }

                return;
            } 
            else if (Map?.ZoneType == MapZones.Arena && ArenaRespawnMap != null)
            {
                ArenaRespawn();
                return;
            }
            else if (RespawnOverrideMap != null)
            {
                Warp(RespawnOverrideMapId, RespawnOverrideX, RespawnOverrideY, (byte)RespawnOverrideDir, mapInstanceType: MapInstanceType.Overworld);
                return;
            }
            ClassRespawn();
        }

        public void ClassRespawn()
        {
            var mapId = Guid.Empty;
            byte x = 0, y = 0, dir = 0;

            var cls = ClassBase.Get(ClassId);
            if (cls != null)
            {
                if (MapController.Lookup.Keys.Contains(cls.SpawnMapId))
                {
                    mapId = cls.SpawnMapId;
                }

                x = (byte)cls.SpawnX;
                y = (byte)cls.SpawnY;
                dir = (byte)cls.SpawnDir;
            }

            if (mapId == Guid.Empty)
            {
                using (var mapenum = MapController.Lookup.GetEnumerator())
                {
                    mapenum.MoveNext();
                    mapId = mapenum.Current.Value.Id;
                }
            }

            Warp(mapId, x, y, dir, false, 0, false, false, MapInstanceType.Overworld);
        }

        // Instancing

        /// <summary>
        /// Checks to see if we CAN go to the requested instance type
        /// </summary>
        /// <param name="instanceType">The instance type we're requesting a warp to</param>
        /// <param name="fromLogin">Whether or not this is from the login flow</param>
        /// <param name="newMapId">The map ID we will be warping to</param>
        /// <returns></returns>
        public bool CanChangeToInstanceType(MapInstanceType instanceType, bool fromLogin, Guid newMapId)
        {
            bool isValid = true;

            switch (instanceType)
            {
                case MapInstanceType.ClanWar:
                    // Either the war has ended, or you're trying to log into a new war
                    if ((!ClanWarManager.ClanWarActive) || (fromLogin && LastClanWarId != ClanWarManager.CurrentWarId))
                    {
                        isValid = false;
                        PacketSender.SendChatMsg(this, "The Clan War you were in is no longer available!", ChatMessageType.Error, CustomColors.Alerts.Error);
                    }
                    break;
                case MapInstanceType.Guild:
                    if (!IsInGuild)
                    {
                        isValid = false;

                        if (fromLogin)
                        {
                            PacketSender.SendChatMsg(this, Strings.Guilds.NoLongerAllowedInInstance, ChatMessageType.Guild, CustomColors.Alerts.Error);
                        }
                        else
                        {
                            PacketSender.SendChatMsg(this, Strings.Guilds.NotAllowedInInstance, ChatMessageType.Guild, CustomColors.Alerts.Error);
                        }
                    }
                    break;
                case MapInstanceType.Shared:
                    if (fromLogin)
                    {
                        isValid = false;
                        if (PlayerDead)
                        {
                            Reset();
                            SendPlayerDeathStatus();
                        }
                    }
                    if (Party != null && Party.Count > 0 && !Options.RejoinableSharedInstances) // Always valid warp if solo/instances are rejoinable
                    {
                        if (Party[0].Id == Id) // if we are the party leader
                        {
                            // And other players are using our shared instance, deny creation of a new instance until they are finished.
                            if (Party.FindAll((Player member) => member.Id != Id && member.InstanceType == MapInstanceType.Shared).Count > 0)
                            {
                                isValid = false;
                                PacketSender.SendChatMsg(this, Strings.Parties.instanceinuse, ChatMessageType.Party, CustomColors.Alerts.Error);
                            }
                        } else
                        {
                            // Otherwise, if the party leader hasn't yet created a shared instance, deny creation of a new one.
                            if (Party[0].InstanceType != MapInstanceType.Shared)
                            {
                                isValid = false;
                                PacketSender.SendChatMsg(this, Strings.Parties.cannotcreateinstance, ChatMessageType.Party, CustomColors.Alerts.Error);
                            } else if (Party[0].SharedMapInstanceId != SharedMapInstanceId)
                            {
                                isValid = false;
                                PacketSender.SendChatMsg(this, Strings.Parties.instanceinprogress, ChatMessageType.Party, CustomColors.Alerts.Error);
                            } else if (newMapId != Party[0].SharedInstanceRespawn.Id)
                            {
                                isValid = false;
                                PacketSender.SendChatMsg(this, Strings.Parties.wronginstance, ChatMessageType.Party, CustomColors.Alerts.Error);
                            }
                        }
                    }
                    break;
            }

            return isValid;
        }

        /// <summary>
        /// In charge of sending the necessary packet information on an instance change
        /// </summary>
        /// <param name="newMap">The <see cref="MapController"/> we are warping to</param>
        private void SendToNewMapInstance(MapController newMap, int lives = 0)
        {
            // Refresh the client's entity list
            var oldMap = MapController.Get(MapId);
            // Get the entities from the old map - we need to clear them off the player's global entities on their client
            if (oldMap != null && oldMap.TryGetInstance(PreviousMapInstanceId, out var oldMapInstance))
            {
                PacketSender.SendMapLayerChangedPacketTo(this, oldMap, PreviousMapInstanceId);
                oldMapInstance.ClearEntityTargetsOf(this); // Remove targets of this entity
                RemoveFromInstanceController(PreviousMapInstanceId);
            }
            
            // Clear events - we'll get them again from the map instance's event cache
            EventTileLookup.Clear();
            EventLookup.Clear();
            EventBaseIdLookup.Clear();
            Log.Debug($"Player {Name} has joined instance {MapInstanceId} of map: {newMap.Name}");
            Log.Info($"Previous instance was {PreviousMapInstanceId}");
            // We changed maps AND instance layers - remove from the old instance
            PacketSender.SendEntityLeaveInstanceOfMap(this, oldMap.Id, PreviousMapInstanceId);
            // Remove any trace of our player from the old instance's processing
            newMap.RemoveEntityFromAllSurroundingMapsInInstance(this, PreviousMapInstanceId);

            // Get any instance timers that are running and send them to the player
            StopInstanceTimers(PreviousMapInstanceId);
            SendInstanceTimers();

            // Remove items that are meant to only exist in an instance
            RemoveInstanceItems();
            // Update instance lives information for the client
            UpdateInstanceLives();
            // Remove timers that aren't meant to proceed beyond an instance change
            EndInstanceChangeTimers();
            // Clear common events from the previous instance
            ClearCommonEvents();
        }

        private void UpdateInstanceLives()
        {
            if (InstanceType != MapInstanceType.Shared || !InstanceProcessor.TryGetInstanceController(MapInstanceId, out var controller))
            {
                // non-shared instances don't have lives - tell the client to reset the lives display
                PacketSender.SendInstanceLivesPacket(this, 0, true);
                return;
            }

            controller.TryInitializeLives(NextInstanceLives, this);
        }

        private void RemoveInstanceItems()
        {
            for (var n = 0; n < Items.Count; n++)
            {
                if (Items[n] == null)
                {
                    continue;
                }

                // Don't mess with the actual object.
                var item = Items[n].Clone();

                var itemBase = ItemBase.Get(item.ItemId);
                if (itemBase == null)
                {
                    continue;
                }

                if (itemBase.DestroyOnInstanceChange)
                {
                    TryTakeItem(Items[n], item.Quantity);
                }
            }
        }

        /// <summary>
        /// Gets all instance timers that are on this player's instance and sends timer packets if necessary
        /// </summary>
        private void SendInstanceTimers()
        {
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => t.Descriptor.OwnerType == TimerOwnerType.Instance 
                && t.OwnerId == MapInstanceId
                && t.Descriptor.ContainsExclusiveMap(MapId)
            ))
            {
                timer.SendTimerPacketTo(this);
            }
        }

        private void SendInstanceMapExclusiveTimers(Guid newMapId)
        {
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => t.Descriptor.OwnerType == TimerOwnerType.Instance
                && t.OwnerId == MapInstanceId
                && t.IsExclusiveToMaps
                && t.ContainsExclusiveMap(newMapId)
            ))
            {
                timer.SendTimerPacketTo(this);
            }
        }

        /// <summary>
        /// Stop instance timers from a given instance ID
        /// </summary>
        /// <param name="previousInstanceId">Given instance ID</param>
        private void StopInstanceTimers(Guid previousInstanceId)
        {
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => t.Descriptor.OwnerType == TimerOwnerType.Instance && t.OwnerId == previousInstanceId))
            {
                timer.SendTimerStopPacketTo(this);
            }
        }

        private void StopInstanceMapExclusiveTimers(Guid previousMapId)
        {
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => t.Descriptor.OwnerType == TimerOwnerType.Instance
                && t.OwnerId == MapInstanceId
                && t.IsExclusiveToMaps
                && t.ContainsExclusiveMap(previousMapId)
            ))
            {
                timer.SendTimerPacketTo(this);
            }
        }

        /// <summary>
        /// Gets all party timers that belong to this player's party and sends timer packets if necessary
        /// </summary>
        private void SendPartyTimers()
        {
            if (Party == null || Party.Count <= 0)
            {
                return;
            }

            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => t.Descriptor.OwnerType == TimerOwnerType.Party && t.OwnerId == Party[0].Id))
            {
                timer.SendTimerPacketTo(this);
            }
        }

        private void StopPartyTimers()
        {
            if (Party == null || Party.Count <= 0)
            {
                return;
            }

            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => t.Descriptor.OwnerType == TimerOwnerType.Party && t.OwnerId == Party[0].Id))
            {
                timer.SendTimerStopPacketTo(this);
            }
        }

        /// <summary>
        /// Checks to see if the <see cref="MapInstanceType"/> we're warping to is different than what type we are currently
        /// on, and, if so, takes care of updating our instance settings.
        /// </summary>
        /// <param name="mapInstanceType">The <see cref="MapInstanceType"/> the player is currently on</param>
        /// <param name="fromLogin">Whether or not we're coming to this method via a login warp.</param>
        /// <returns></returns>
        public bool ProcessMapInstanceChange(MapInstanceType mapInstanceType, bool fromLogin)
        {
            // Save values before change for reference/emergency recall
            PreviousMapInstanceId = MapInstanceId;
            PreviousMapInstanceType = InstanceType;
            if (mapInstanceType != MapInstanceType.NoChange) // If we're requesting an instance type change
            {
                // Update our saved instance type - this helps us determine what to do on login, warps, etc
                InstanceType = mapInstanceType;
                // Requests a new instance id, using the type of instance to determine creation logic
                MapInstanceId = CreateNewInstanceIdFromType(mapInstanceType, fromLogin);

                // Clan war management
                if (mapInstanceType == MapInstanceType.ClanWar && ClanWarManager.ClanWarActive)
                {
                    JoinClanWar();
                }
                else if (PreviousMapInstanceType == MapInstanceType.ClanWar && mapInstanceType != MapInstanceType.NoChange)
                {
                    LeaveClanWar();
                }
            }

            return MapInstanceId != PreviousMapInstanceId;
        }

        /// <summary>
        /// Creates an instance id based on the type of instance we are heading to, and whether or not we should generate a fresh id or use a saved id.
        /// </summary>
        /// <remarks>
        /// Note that if we are coming to this method, we have already checked to see whether or not we CAN go to the requested instance.
        /// </remarks>
        /// <param name="mapInstanceType">The <see cref="MapInstanceType"/> we are switching to</param>
        /// <param name="fromLogin">Whether or not we are coming to this method via player login. We may prefer to use saved values instead of generate new
        /// values if this is the case.</param>
        /// <returns></returns>
        public Guid CreateNewInstanceIdFromType(MapInstanceType mapInstanceType, bool fromLogin)
        {
            Guid newMapLayerId = MapInstanceId;
            switch (mapInstanceType)
            {
                case MapInstanceType.Overworld:
                    ResetSavedInstanceIds();
                    newMapLayerId = Guid.Empty;
                    break;

                case MapInstanceType.Personal:
                case MapInstanceType.PersonalDungeon:
                    if (!fromLogin) // If we're logging into a personal instance, we want to login to the SAME instance.
                    {
                        PersonalMapInstanceId = Guid.NewGuid();
                    }
                    newMapLayerId = PersonalMapInstanceId;
                    break;

                case MapInstanceType.Guild:
                    if (IsInGuild)
                    {
                        newMapLayerId = Guild.GuildInstanceId;
                    } else
                    {
                        Log.Error($"Player {Name} requested a guild warp with no guild, and proceeded to warp to map anyway");
                        newMapLayerId = Guid.Empty;
                    }
                    break;

                case MapInstanceType.ClanWar:
                    newMapLayerId = ClanWarManager.CurrentWarId;

                    break;

                case MapInstanceType.Shared:
                    bool isSolo = Party == null || Party.Count < 2;
                    bool isPartyLeader = Party != null && Party.Count > 0 && Party[0].Id == Id;

                    if (isSolo) // Solo instance initialization
                    {
                        SharedMapInstanceId = Guid.NewGuid();
                        newMapLayerId = SharedMapInstanceId;
                    } else if (!Options.RejoinableSharedInstances && isPartyLeader) // Non-rejoinable instance initialization
                    {
                        // Generate a new instance
                        SharedMapInstanceId = Guid.NewGuid();
                        // If we are the leader, propogate your shared instance ID to all current members of the party.
                        if (isPartyLeader && !Options.RejoinableSharedInstances)
                        {
                            foreach (Player member in Party)
                            {
                                member.SharedMapInstanceId = SharedMapInstanceId;
                                // Alert party members that the leader has started the dungeon
                                if (member.Id != Id)
                                {
                                    PacketSender.SendChatMsg(member, $"{Name} has started a shared instance.", ChatMessageType.Party, sendToast: true);
                                }
                            }
                        }
                    } else if (Party != null && Party.Count > 0 && Options.RejoinableSharedInstances) // Joinable instance initialization
                    {
                        // Scan party members for an active shared instance - if one is found, use it
                        var memberInInstance = Party.Find((Player member) => member.SharedMapInstanceId != Guid.Empty);
                        if (memberInInstance != null)
                        {
                            SharedMapInstanceId = memberInInstance.SharedMapInstanceId;
                        } else
                        {
                            // Otherwise, if no one is on an instance, create a new instance
                            SharedMapInstanceId = Guid.NewGuid();

                            foreach (Player member in Party)
                            {
                                // Alert party members that the leader has started the dungeon
                                if (member.Id != Id)
                                {
                                    PacketSender.SendChatMsg(member, $"{Name} has started a shared instance.", ChatMessageType.Party, sendToast: true);
                                }
                            }
                        }
                    }
                    // Use whatever your shared instance id is for the warp
                    newMapLayerId = SharedMapInstanceId;

                    break;

                case MapInstanceType.Party:
                case MapInstanceType.PartyDungeon:
                    if (Party == null || Party.Count < 2)
                    {
                        newMapLayerId = Id;
                    }
                    else
                    {
                        newMapLayerId = Party[0].Id;
                    }
                    break;

                default:
                    Log.Error($"Player {Name} requested an instance type ({mapInstanceType}) on map {Map?.Name} that is not supported. Their map instance settings will not change.");
                    break;
            }

            return newMapLayerId;
        }

        /// <summary>
        /// /// Updates the player's last overworld location. Useful for warping out of instances if need be.
        /// </summary>
        /// <param name="overworldMapId">Which map we were on before the instance change</param>
        /// <param name="overworldX">X before instance change</param>
        /// <param name="overworldY">Y before instance change</param>
        public void UpdateLastOverworldLocation(Guid overworldMapId, int overworldX, int overworldY)
        {
            LastOverworldMapId = overworldMapId;
            LastOverworldX = overworldX;
            LastOverworldY = overworldY;
        }

        /// <summary>
        /// Updates the shared instance respawn location - for respawning on death in a shared instance (when this is enabled)
        /// </summary>
        /// <param name="respawnMapId"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dir"></param>
        public void UpdateSharedInstanceRespawnLocation(Guid respawnMapId, int x, int y, int dir)
        {
            SharedInstanceRespawnId = respawnMapId;
            SharedInstanceRespawnX = x;
            SharedInstanceRespawnY = y;
            SharedInstanceRespawnDir = dir;
        }

        /// <summary>
        /// Resets instance ids we've saved on the player. Generally called when going back to the overworld.
        /// </summary>
        public void ResetSavedInstanceIds()
        {
            PersonalMapInstanceId = Guid.Empty;
            SharedMapInstanceId = Guid.Empty;
        }

        /// <summary>
        /// Checks whether a player can or can not receive the specified item and its quantity.
        /// </summary>
        /// <param name="itemId">The item Id to check if the player can receive.</param>
        /// <param name="quantity">The amount of this item to check if the player can receive.</param>
        /// <returns></returns>
        public bool CanGiveItem(Guid itemId, int quantity) => CanGiveItem(new Item(itemId, quantity));

        //Inventory
        /// <summary>
        /// Checks whether a player can or can not receive the specified item and its quantity.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to check if this player can receive.</param>
        /// <returns></returns>
        public bool CanGiveItem(Item item)
        {
            if (item.Descriptor != null)
            {
                // Is the item stackable?
                if (item.Descriptor.IsStackable)
                {
                    // Does the user have this item already?
                    var itemSlots = FindInventoryItemSlots(item.ItemId);
                    var slotsRequired = Math.Ceiling((double)item.Quantity / item.Descriptor.MaxInventoryStack);

                    // User doesn't have this item yet.
                    if (itemSlots.Count == 0)
                    {
                        // Does the user have enough free space for these stacks?
                        if (slotsRequired <= FindOpenInventorySlots().Count)
                        {
                            return true;
                        }
                    }
                    else // We need to check to see how much space we'd have if we first filled all possible stacks
                    {
                        // Keep track of how much we have given to each stack
                        var giveRemainder = item.Quantity;

                        // For each stack while we still have items to give
                        for (var i = 0; i < itemSlots.Count && giveRemainder > 0; i++)
                        {
                            // Give as much as possible to this stack
                            giveRemainder -= item.Descriptor.MaxInventoryStack - itemSlots[i].Quantity;
                        }

                        // We don't have anymore stuff to give after filling up our available stacks - we good
                        bool roomInStacks = giveRemainder <= 0;
                        // We still have leftover even after maxing each of our current stacks. See if we have empty slots in the inventory.
                        bool roomInInventory = giveRemainder > 0 && Math.Ceiling((double)giveRemainder / item.Descriptor.MaxInventoryStack) <= FindOpenInventorySlots().Count;

                        return roomInStacks || roomInInventory;
                    }
                }
                // Not a stacking item, so can we contain the amount we want to give them?
                else
                {
                    if (FindOpenInventorySlots().Count >= item.Quantity)
                    {
                        return true;
                    }
                }
            }

            // Nothing matches in here, give up!
            return false;
        }

        /// <summary>
        /// Checks whether or not a player has enough items in their inventory to be taken.
        /// </summary>
        /// <param name="itemId">The ItemId to see if it can be taken away from the player.</param>
        /// <param name="quantity">The quantity of above item to see if we can take away from the player.</param>
        /// <returns>Whether or not the item can be taken away from the player in the requested quantity.</returns>
        public bool CanTakeItem(Guid itemId, int quantity) => FindInventoryItemQuantity(itemId) >= quantity;

        /// <summary>
        /// Checks whether or not a player has enough items in their inventory to be taken.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to see if it can be taken away from the player.</param>
        /// <returns>Whether or not the item can be taken away from the player.</returns>
        public bool CanTakeItem(Item item) => CanTakeItem(item.ItemId, item.Quantity);

        /// <summary>
        /// Gets the item at <paramref name="slotIndex"/> and stores it in <paramref name="slot"/>.
        /// </summary>
        /// <param name="slotIndex">the slot to load the <see cref="Item"/> from</param>
        /// <param name="slot">the <see cref="Item"/> at <paramref name="slotIndex"/></param>
        /// <param name="createSlotIfNull">if the slot is in an invalid state (<see langword="null"/>), set it</param>
        /// <returns>returns <see langword="false"/> if <paramref name="slot"/> is set to <see langword="null"/></returns>
        public bool TryGetSlot(int slotIndex, out InventorySlot slot, bool createSlotIfNull = false)
        {
            // ReSharper disable once AssignNullToNotNullAttribute Justification: slot is never null when this returns true.
            slot = Items[slotIndex];

            // ReSharper disable once InvertIf
            if (default == slot && createSlotIfNull)
            {
                var createdSlot = new InventorySlot(slotIndex);
                Log.Error("Creating inventory slot " + slotIndex + " for player " + Name + Environment.NewLine + Environment.StackTrace);
                Items[slotIndex] = createdSlot;
                slot = createdSlot;
            }

            return default != slot;
        }

        /// <summary>
        /// Gets the item at <paramref name="slotIndex"/> and stores it in <paramref name="item"/>.
        /// </summary>
        /// <param name="slotIndex">the slot to load the <see cref="Item"/> from</param>
        /// <param name="item">the <see cref="Item"/> at <paramref name="slotIndex"/></param>
        /// <returns>returns <see langword="false"/> if <paramref name="item"/> is set to <see langword="null"/></returns>
        public bool TryGetItemAt(int slotIndex, out Item item)
        {
            TryGetSlot(slotIndex, out var slot);
            item = slot;
            return default != item;
        }

        /// <summary>
        /// Attempts to give the player an item. Returns whether or not it succeeds.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to give to the player.</param>
        /// <returns>Whether the player received the item or not.</returns>
        public bool TryGiveItem(Item item) => TryGiveItem(item, ItemHandling.Normal, false, true);

        /// <summary>
        /// Attempts to give the player an item. Returns whether or not it succeeds.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to give to the player.</param>
        /// <param name="handler">The way to handle handing out this item.</param>
        /// <returns>Whether the player received the item or not.</returns>
        public bool TryGiveItem(Item item, ItemHandling handler) => TryGiveItem(item, handler, false, true);

        /// <summary>
        /// Attempts to give the player an item. Returns whether or not it succeeds.
        /// </summary>
        /// <param name="itemId">The Id for the item to be handed out to the player.</param>
        /// <param name="quantity">The quantity of items to be handed out to the player.</param>
        /// <returns>Whether the player received the item or not.</returns>
        public bool TryGiveItem(Guid itemId, int quantity, Player crafter = null) => TryGiveItem(new Item(itemId, quantity), ItemHandling.Normal, false, true, crafter: crafter);

        /// <summary>
        /// Attempts to give the player an item. Returns whether or not it succeeds.
        /// </summary>
        /// <param name="itemId">The Id for the item to be handed out to the player.</param>
        /// <param name="quantity">The quantity of items to be handed out to the player.</param>
        /// <param name="handler">The way to handle handing out this item.</param>
        /// <returns>Whether the player received the item or not.</returns>
        public bool TryGiveItem(Guid itemId, int quantity, ItemHandling handler) => TryGiveItem(new Item(itemId, quantity), handler, false, true);

        /// <summary>
        /// Attempts to give the player an item. Returns whether or not it succeeds.
        /// </summary>
        /// <param name="itemId">The Id for the item to be handed out to the player.</param>
        /// <param name="quantity">The quantity of items to be handed out to the player.</param>
        /// <param name="handler">The way to handle handing out this item.</param>
        /// <param name="bankOverflow">Should we allow the items to overflow into the player's bank when their inventory is full.</param>
        /// <param name="sendUpdate">Should we send an inventory update when we are done changing the player's items.</param>
        /// <returns>Whether the player received the item or not.</returns>
        public bool TryGiveItem(Guid itemId, int quantity, ItemHandling handler, bool bankOverflow = false, bool sendUpdate = true) => TryGiveItem(new Item(itemId, quantity), handler, bankOverflow, sendUpdate);

        /// <summary>
        /// Attempts to give the player an item. Returns whether or not it succeeds.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to give to the player.</param>
        /// <param name="handler">The way to handle handing out this item.</param>
        /// <param name="bankOverflow">Should we allow the items to overflow into the player's bank when their inventory is full.</param>
        /// <param name="sendUpdate">Should we send an inventory update when we are done changing the player's items.</param>
        /// <param name="overflowTileX">The x coordinate of the tile in which overflow should spawn on, if the player cannot hold the full amount.</param>
        /// <param name="overflowTileY">The y coordinate of the tile in which overflow should spawn on, if the player cannot hold the full amount.</param>
        /// <returns>Whether the player received the item or not.</returns>
        public bool TryGiveItem(Item item, ItemHandling handler = ItemHandling.Normal, bool bankOverflow = false, bool sendUpdate = true, int overflowTileX = -1, int overflowTileY = -1, bool fromLootRoll = false, Player crafter = null)
        {
            if (PlayerDead)
            {
                return false;
            }
            var success = false;

            // Is this a valid item?
            if (item.Descriptor == null)
            {
                return false;
            }

            if (item.Quantity <= 0)
            {
                return true;
            }

            if (crafter != null)
            {
                item.ItemProperties.CraftedBy = crafter.Name;
                item.ItemProperties.CraftedById = crafter.Id;
            }

            // Get this information so we can use it later.
            var openSlotCount = FindOpenInventorySlots().Count;
            var slotsRequired = (int)Math.Ceiling(item.Quantity / (double) item.Descriptor.MaxInventoryStack);

            int spawnAmount = 0;

            // How are we going to be handling this?
            switch (handler)
            {
                // Handle this item like normal, there's no special rules attached to this method.
                case ItemHandling.Normal:
                    if (CanGiveItem(item)) // Can receive item under regular rules.
                    {
                        GiveItem(item, sendUpdate);
                        success = true;
                    }

                    break;
                case ItemHandling.Overflow:
                    if (CanGiveItem(item)) // Can receive item under regular rules.
                    {
                        GiveItem(item, sendUpdate);
                        success = true;
                    }
                    else if (item.Descriptor.Stackable && openSlotCount < slotsRequired && item.Descriptor.MaxInventoryStack > 1) // Is stackable (> 1, else treat as unstackable), but no inventory space.
                    {
                        var itemDesc = item.Descriptor;
                        var slots = FindInventoryItemSlots(item.ItemId);
                        var openSlots = FindOpenInventorySlots();
                        slots.AddRange(openSlots);
                        var amountToGive = item.Quantity;
                        foreach (var slot in slots)
                        {
                            amountToGive -= itemDesc.MaxInventoryStack - slot.Quantity;
                        }

                        // Fill up any stacks we can
                        if (amountToGive != item.Quantity)
                        {
                            GiveItem(item, sendUpdate);
                        }

                        spawnAmount = amountToGive;
                    }
                    else // Time to give them as much as they can take, and spawn the rest on the map!
                    {
                        spawnAmount = item.Quantity - openSlotCount;
                        if (openSlotCount > 0)
                        {
                            item.Quantity = openSlotCount;
                            GiveItem(item, sendUpdate);
                        }
                    }

                    // Do we have any items to spawn to the map?
                    if (spawnAmount > 0 && MapController.TryGetInstanceFromMap(Map.Id, MapInstanceId, out var instance))
                    {
                        instance.SpawnItem(overflowTileX > -1 ? overflowTileX : X, overflowTileY > -1 ? overflowTileY : Y, item, spawnAmount, Id, true, fromLootRoll ? ItemSpawnType.LootRoll : ItemSpawnType.Dropped);
                        if (fromLootRoll)
                        {
                            success = true;
                        }
                        else
                        {
                            success = spawnAmount != item.Quantity;
                        }
                    }

                    break;
                case ItemHandling.UpTo:
                    if (CanGiveItem(item)) // Can receive item under regular rules.
                    {
                        GiveItem(item, sendUpdate);
                        success = true;
                    }
                    else if (!item.Descriptor.Stackable && openSlotCount >= slotsRequired) // Is not stackable, has space for some.
                    {
                        item.Quantity = openSlotCount;
                        GiveItem(item, sendUpdate);
                        success = true;
                    }

                    break;
                    // Did you forget to change this method when you added something? ;)
                default:
                    throw new NotImplementedException();
            }

            if (success)
            {
                RecipeUnlockWatcher.EnqueueNewPlayer(this, item.Descriptor.Id, RecipeTrigger.ItemObtained);
                AddDeferredEvent(CommonEventTrigger.InventoryChanged);
                if (CraftingTableId != Guid.Empty) // Update our crafting table if we have one
                {
                    UpdateCraftingTable(CraftingTableId);
                }

                return success;
            }

            var bankInterface = new BankInterface(this, ((IEnumerable<Item>)Bank).ToList(), new object(), null, Options.Instance.PlayerOpts.InitialBankslots);
            return bankOverflow && bankInterface.TryDepositItem(item, sendUpdate);
        }


        /// <summary>
        /// Gives the player an item. NOTE: This method MAKES ZERO CHECKS to see if this is possible!
        /// Use TryGiveItem where possible!
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sendUpdate"></param>
        private void GiveItem(Item item, bool sendUpdate)
        {
            if (PlayerDead)
            {
                return;
            }

            // Decide how we're going to handle this item.
            var existingSlots = FindInventoryItemSlots(item.Descriptor.Id);
            var updateSlots = new List<int>();
            if (item.Descriptor.Stackable && existingSlots.Count > 0) // Stackable, but already exists in the inventory.
            {
                // So this gets complicated.. First let's hand out the quantity we can hand out before we hit a stack limit.
                var toGive = item.Quantity;
                foreach (var slot in existingSlots)
                {
                    if (toGive == 0)
                    {
                        break;
                    }

                    if (slot.Quantity >= item.Descriptor.MaxInventoryStack)
                    {
                        continue;
                    }

                    var canAdd = item.Descriptor.MaxInventoryStack - slot.Quantity;
                    if (canAdd > toGive)
                    {
                        slot.Quantity += toGive;
                        updateSlots.Add(slot.Slot);
                        toGive = 0;
                    }
                    else
                    {
                        slot.Quantity += canAdd;
                        updateSlots.Add(slot.Slot);
                        toGive -= canAdd;
                    }
                }

                // Is there anything left to hand out? If so, hand out max stacks and what remains until we run out!
                if (toGive > 0)
                {
                    var openSlots = FindOpenInventorySlots();
                    var total = toGive; // Copy this as we're going to be editing toGive.
                    for (var slot = 0; slot < openSlots.Count && slot < Math.Ceiling((double)total / item.Descriptor.MaxInventoryStack); slot++)
                    {
                        var quantity = item.Descriptor.MaxInventoryStack <= toGive ?
                            item.Descriptor.MaxInventoryStack :
                            toGive;

                        toGive -= quantity;
                        openSlots[slot].Set(new Item(item.ItemId, quantity));
                        updateSlots.Add(openSlots[slot].Slot);
                    }
                }
            }
            else if (!item.Descriptor.Stackable && item.Quantity > 1) // Not stackable, but multiple items.
            {
                var openSlots = FindOpenInventorySlots();
                for (var slot = 0; slot < item.Quantity; slot++)
                {
                    openSlots[slot].Set(new Item(item.ItemId, 1));
                    updateSlots.Add(openSlots[slot].Slot);
                }
            }
            else // Hand out without any special treatment. Either a single item or a stackable item we don't have yet.
            {
                // If the item is not stackable, or the amount is below our stack cap just blindly hand it out.
                if (!item.Descriptor.Stackable || item.Quantity < item.Descriptor.MaxInventoryStack)
                {
                    var newSlot = FindOpenInventorySlot();
                    newSlot.Set(item);
                    updateSlots.Add(newSlot.Slot);
                }
                // The item is above our stack cap.. Let's start handing them phat stacks out!
                else
                {
                    var toGive = item.Quantity;
                    var openSlots = FindOpenInventorySlots();
                    for (var slot = 0; slot < Math.Ceiling((double) item.Quantity / item.Descriptor.MaxInventoryStack); slot++)
                    {
                        var quantity = item.Descriptor.MaxInventoryStack <= toGive ?
                            item.Descriptor.MaxInventoryStack :
                            toGive;

                        toGive -= quantity;
                        if (slot >= openSlots.Count)
                        {
                            break;
                        }
                        openSlots[slot].Set(new Item(item.ItemId, quantity));
                        updateSlots.Add(openSlots[slot].Slot);
                    }
                }

            }

            ItemsDiscovered.Add(new ItemDiscoveryInstance(Id, item.ItemId));

            // Do we need to update the player's inventory?
            if (sendUpdate)
            {
                foreach (var slot in updateSlots)
                {
                    PacketSender.SendInventoryItemUpdate(this, slot);
                }
            }

            // Update quests for this item.
            UpdateGatherItemQuests(item.ItemId);

        }

        /// <summary>
        /// Retrieves a list of open inventory slots for this player.
        /// </summary>
        /// <returns>A list of <see cref="InventorySlot"/></returns>
        public List<InventorySlot> FindOpenInventorySlots()
        {
            var slots = new List<InventorySlot>();
            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                var inventorySlot = Items[i];

                if (inventorySlot != null && inventorySlot.ItemId == Guid.Empty)
                {
                    slots.Add(inventorySlot);
                }
            }
            return slots;
        }

        /// <summary>
        /// Finds the first open inventory slot this player has.
        /// </summary>
        /// <returns>An <see cref="InventorySlot"/> instance, or null if none are found.</returns>
        public InventorySlot FindOpenInventorySlot() => FindOpenInventorySlots().FirstOrDefault();

        /// <summary>
        /// Swap items between <paramref name="fromSlotIndex"/> and <paramref name="toSlotIndex"/>.
        /// </summary>
        /// <param name="fromSlotIndex">the slot index to swap from</param>
        /// <param name="toSlotIndex">the slot index to swap to</param>
        public void SwapItems(int fromSlotIndex, int toSlotIndex)
        {
            TryGetSlot(fromSlotIndex, out var fromSlot, true);
            TryGetSlot(toSlotIndex, out var toSlot, true);

            var toSlotClone = toSlot.Clone();
            toSlot.Set(fromSlot);
            fromSlot.Set(toSlotClone);

            PacketSender.SendInventoryItemUpdate(this, fromSlotIndex);
            PacketSender.SendInventoryItemUpdate(this, toSlotIndex);
            EquipmentProcessItemSwap(fromSlotIndex, toSlotIndex);
        }

        /// <summary>
        /// Attempt to drop <paramref name="amount"/> of the item in the slot
        /// identified by <paramref name="slotIndex"/>, returning false if it
        /// is unable to drop the item for any reason.
        /// </summary>
        /// <param name="slotIndex">the slot to drop from</param>
        /// <param name="amount">the amount to drop</param>
        /// <returns>if an item was dropped</returns>
        public bool TryDropItemFrom(int slotIndex, int amount, bool sendUpdate = true)
        {
            if (PlayerDead)
            {
                return false;
            }

            if (!TryGetItemAt(slotIndex, out var itemInSlot))
            {
                return false;
            }

            amount = Math.Min(amount, itemInSlot.Quantity);
            if (amount < 1)
            {
                // Abort if the amount we are trying to drop is below 1.
                itemInSlot.Set(Item.None);
                if (sendUpdate)
                {
                    PacketSender.SendInventoryItemUpdate(this, slotIndex);
                }
                return false;
            }

            if (Equipment?.Any(equipmentSlotIndex => equipmentSlotIndex == slotIndex) ?? false)
            {
                PacketSender.SendChatMsg(this, Strings.Items.equipped, ChatMessageType.Inventory, CustomColors.Items.Bound);
                return false;
            }

            var itemDescriptor = itemInSlot.Descriptor;
            if (itemDescriptor == null)
            {
                return false;
            }

            if (!itemDescriptor.CanDrop)
            {
                PacketSender.SendChatMsg(this, Strings.Items.bound, ChatMessageType.Inventory, CustomColors.Items.Bound);
                return false;
            }

            if (itemInSlot.TryGetBag(out var bag) && !bag.IsEmpty)
            {
                PacketSender.SendChatMsg(this, Strings.Bags.dropnotempty, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                return false;
            }

            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
            {
                mapInstance.SpawnItem(X, Y, itemInSlot, itemDescriptor.IsStackable ? amount : 1, Id, true, ItemSpawnType.Dropped);

                itemInSlot.Quantity = Math.Max(0, itemInSlot.Quantity - amount);

                if (itemInSlot.Quantity == 0)
                {
                    itemInSlot.Set(Item.None);
                    EquipmentProcessItemLoss(slotIndex);
                }

                UpdateGatherItemQuests(itemDescriptor.Id);

                if (sendUpdate)
                {
                    PacketSender.SendInventoryItemUpdate(this, slotIndex);
                }

                if (CraftingTableId != Guid.Empty) // Update our crafting table if we have one
                {
                    UpdateCraftingTable(CraftingTableId);
                }
                AddDeferredEvent(CommonEventTrigger.InventoryChanged);
                return true;
            } else
            {
                if (Map != null)
                {
                    Log.Error($"Could not find map layer {MapInstanceId} for player '{Name}' on map {Map.Name}.");
                } else
                {
                    Log.Error($"Could not find map {MapId} for player '{Name}'.");
                }
                return false;
            }
        }

        /// <summary>
        /// Drops <paramref name="amount"/> of the item in the slot identified by <paramref name="slotIndex"/>.
        /// </summary>
        /// <param name="slotIndex">the slot to drop from</param>
        /// <param name="amount">the amount to drop</param>
        /// <see cref="TryDropItemFrom(int, int)"/>
        [Obsolete("Use TryDropItemFrom(int, int).")]
        public void DropItemFrom(int slotIndex, int amount) => TryDropItemFrom(slotIndex, amount);

        public void UseItem(int slot, Entity target = null)
        {
            if (PlayerDead)
            {
                return;
            }

            // Can not use items while sleeping
            if (StatusActive(StatusTypes.Sleep))
            {
                PacketSender.SendChatMsg(this, Strings.Items.sleep, ChatMessageType.Error);
                return;
            }

            if (resourceLock != null)
            {
                SetResourceLock(false);
            }
            var equipped = false;
            var Item = Items[slot];
            var itemBase = ItemBase.Get(Item.ItemId);
            if (itemBase != null && Item.Quantity > 0)
            {
                // Unequip items even if you do not meet the requirements.
                // (Need this for silly devs who give people items and then later add restrictions...)
                if (itemBase.ItemType == ItemTypes.Equipment && SlotIsEquipped(slot, out var equippedSlot))
                {
                    if (TryGetEquippedItem(equippedSlot, out var equippedItem))
                    {
                        UnlearnSpecialAttack(equippedItem);
                    }
                    UnequipItem(equippedSlot, true);
                    return;
                }

                if (!Conditions.MeetsConditionLists(itemBase.UsageRequirements, this, null))
                {
                    if (!string.IsNullOrWhiteSpace(itemBase.CannotUseMessage))
                    {
                        PacketSender.SendChatMsg(this, itemBase.CannotUseMessage, ChatMessageType.Error);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(this, Strings.Items.dynamicreq, ChatMessageType.Error);
                    }

                    return;
                }

                if (ItemCooldowns.ContainsKey(itemBase.Id) && ItemCooldowns[itemBase.Id] > Timing.Global.MillisecondsUtc)
                {
                    //Cooldown warning!
                    PacketSender.SendChatMsg(this, Strings.Items.cooldown, ChatMessageType.Error);

                    return;
                }

                switch (itemBase.ItemType)
                {
                    case ItemTypes.None:
                    case ItemTypes.Currency:
                        PacketSender.SendChatMsg(this, Strings.Items.cannotuse, ChatMessageType.Error);

                        return;
                    case ItemTypes.Consumable:
                        var value = 0;
                        var color = CustomColors.Items.ConsumeHp;
                        var die = false;

                        if (itemBase.MeleeConsumable && !InDuel)
                        {
                            PacketSender.SendChatMsg(this, "You can't use this item while not in a duel!", ChatMessageType.Notice, sound: true);
                            return;
                        }

                        if (!itemBase.MeleeConsumable && InDuel)
                        {
                            PacketSender.SendChatMsg(this, "You can't use this item while in a duel!", ChatMessageType.Notice, sound: true);
                            return;
                        }

                        if (itemBase.ClanWarConsumable && !InCurrentClanWar)
                        {
                            PacketSender.SendChatMsg(this, "You can't use this item while not in a Clan War!", ChatMessageType.Notice, sound: true);
                            return;
                        }

                        if (!itemBase.ClanWarConsumable && InCurrentClanWar)
                        {
                            PacketSender.SendChatMsg(this, "You can't use this item while in a Clan War!", ChatMessageType.Notice, sound: true);
                            return;
                        }

                        switch (itemBase.Consumable.Type)
                        {
                            case ConsumableType.Health:
                                value = itemBase.Consumable.Value +
                                        GetMaxVital((int) itemBase.Consumable.Type) *
                                        itemBase.Consumable.Percentage /
                                        100;

                                value = ApplyBonusEffectInt(value, EffectType.Foodie, true);

                                AddVital(Vitals.Health, value);
                                if (value < 0)
                                {
                                    color = CustomColors.Items.ConsumePoison;

                                    //Add a death handler for poison.
                                    die = !HasVital(Vitals.Health);
                                }

                                break;

                            case ConsumableType.Mana:
                                value = itemBase.Consumable.Value +
                                        GetMaxVital((int) itemBase.Consumable.Type) *
                                        itemBase.Consumable.Percentage /
                                        100;

                                value = ApplyBonusEffectInt(value, EffectType.Manaflow, true);

                                AddVital(Vitals.Mana, value);
                                color = CustomColors.Items.ConsumeMp;

                                break;

                            case ConsumableType.Experience:
                                value = itemBase.Consumable.Value +
                                        (int) (GetExperienceToNextLevel(Level) * itemBase.Consumable.Percentage / 100);

                                GiveExperience(value);
                                color = CustomColors.Items.ConsumeExp;

                                break;

                            default:
                                throw new IndexOutOfRangeException();
                        }

                        if (itemBase.Consumable.Type != ConsumableType.Experience)
                        {
                            // Reverse the value - a positive consumable value _gives_ health
                            var revValue = value * -1;
                            PacketSender.SendCombatNumber(DetermineCombatNumberType(revValue, itemBase.Consumable.Type == ConsumableType.Mana, false, 1.0), this, revValue);
                        }
                        else
                        {
                            var symbol = value < 0 ? Strings.Combat.removesymbol : Strings.Combat.addsymbol;
                            var number = $"{symbol}{Math.Abs(value)}";
                            PacketSender.SendActionMsg(this, number, color);
                        }

                        if (die)
                        {
                            lock (EntityLock)
                            {
                                Die(true, this);
                            }
                        }

                        TryTakeItem(Items[slot], 1);

                        StartCommonEventsWithTrigger(CommonEventTrigger.ConsumableUsed);

                        break;
                    case ItemTypes.Equipment:
                        if (SlotIsEquipped(slot, out var eqpSlot))
                        {
                            UnequipItem(eqpSlot, true);
                            return;
                        }

                        EquipItem(itemBase, slot);

                        break;
                    case ItemTypes.Spell:
                        if (itemBase.SpellId == Guid.Empty)
                        {
                            return;
                        }

                        if (itemBase.QuickCast)
                        {
                            if (!CanCastSpell(itemBase.Spell, target, true, true) || IsCasting)
                            {
                                return;
                            }
                            Target = target;
                            UseSpell(itemBase.Spell, -1, target, true, instantCast: true);
                        }
                        else if (!TryAddSkillToBook(itemBase.SpellId))
                        {
                            return;
                        }

                        if (itemBase.SingleUse)
                        {
                            TryTakeItem(Items[slot], 1);
                        }

                        break;
                    case ItemTypes.Event:
                        var evt = EventBase.Get(itemBase.EventId);
                        
                        if (InDuel)
                        {
                            PacketSender.SendChatMsg(this, "You can't use this item while in a Duel!", ChatMessageType.Notice, sound: true);
                            return;
                        }

                        if (evt == null || !UnsafeStartCommonEvent(evt))
                        {
                            return;
                        }

                        if (itemBase.SingleUse)
                        {
                            TryTakeItem(Items[slot], 1);
                        }

                        break;
                    case ItemTypes.Bag:
                        OpenBag(Item, itemBase);

                        break;
                    case ItemTypes.Cosmetic:
                        if (TryChangeCosmeticUnlockStatus(Item.ItemId, true))
                        {
                            TryTakeItem(Items[slot], 1);
                        }
                        else
                        {
                            PacketSender.SendChatMsg(this,
                                Strings.Player.CosmeticAlready,
                                Enums.ChatMessageType.Experience,
                                sound: true);
                        }

                        break;
                    case ItemTypes.Enhancement:
                        if (TryUnlockEnhancement(itemBase.EnhancementId))
                        {
                            TryTakeItem(Items[slot], 1);
                        }
                        else
                        {
                            PacketSender.SendChatMsg(this, Strings.Enhancements.AlreadyLearned, ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                        }

                        break;

                    case ItemTypes.Permabuff:
                        if (!TryUnlockPermabuff(itemBase.Id))
                        {
                            break;
                        }

                        if(!TryTakeItem(Items[slot], 1))
                        {
                            break;
                        }

                        ApplyPermabuffsToStats(itemBase);

                        if (itemBase.SkillPoints > 0)
                        {
                            PacketSender.SendChatMsg(this, Strings.Player.PermabuffSkillpoint.ToString(itemBase.SkillPoints), ChatMessageType.Experience, CustomColors.General.GeneralCompleted, sendToast: true);
                            // Send skill point update to client
                            PacketSender.SendSkillbookToClient(this);
                        }
                        var idx = 0;

                        #region Messaging
                        foreach (var statVal in itemBase.StatsGiven)
                        {
                            if (statVal > 0)
                            {
                                var stat = (Stats)idx;
                                
                                if (statVal < 0)
                                {
                                    PacketSender.SendChatMsg(this,
                                        $"You've gained a permanent stat loss: {statVal} {stat.GetDescription()}!",
                                        ChatMessageType.Experience,
                                        CustomColors.General.GeneralCompleted,
                                        sendToast: true);
                                }
                                else
                                {
                                    PacketSender.SendChatMsg(this, 
                                        $"You've gained a permanent boost: +{statVal} {stat.GetDescription()}!", 
                                        ChatMessageType.Experience, 
                                        CustomColors.General.GeneralCompleted, 
                                        sendToast: true);
                                }
                            }
                            idx++;
                        }

                        idx = 0;
                        foreach (var vitalVal in itemBase.VitalsGiven)
                        {
                            if (vitalVal > 0)
                            {
                                var vital = (Vitals)idx;

                                if (vitalVal < 0)
                                {
                                    PacketSender.SendChatMsg(this,
                                        $"You've gained a permanent stat loss: {vitalVal} {vital.GetDescription()}!",
                                        ChatMessageType.Experience,
                                        CustomColors.General.GeneralCompleted,
                                        sendToast: true);
                                }
                                else
                                {
                                    PacketSender.SendChatMsg(this,
                                        $"You've gained a permanent boost: +{vitalVal} {vital.GetDescription()}!",
                                        ChatMessageType.Experience,
                                        CustomColors.General.GeneralCompleted,
                                        sendToast: true);
                                }
                            }
                            idx++;
                        }
                        #endregion

                        PacketSender.SendAnimationToProximity(new Guid(Options.Instance.CombatOpts.SpellLearnedAnimGuid), 1, Id, MapId, (byte)X, (byte)Y, (sbyte)Dir, MapInstanceId);
                        PacketSender.SendUsedPermabuffs(this);
                        break;
                    default:
                        PacketSender.SendChatMsg(this, Strings.Items.notimplemented, ChatMessageType.Error);

                        return;
                }

                if (itemBase.Animation != null)
                {
                    PacketSender.SendAnimationToProximity(
                        itemBase.Animation.Id, 1, base.Id, MapId, 0, 0, (sbyte)Dir, MapInstanceId
                    ); //Target Type 1 will be global entity
                }

                // Does this item have a cooldown to process of its own?
                if (itemBase.Cooldown > 0)
                {
                    UpdateCooldown(itemBase);
                }

                // Update the global cooldown, if we can trigger it here.
                if (!itemBase.IgnoreGlobalCooldown)
                {
                    UpdateGlobalCooldown();
                }
            }
        }

        public bool CanDestroyItem(int slot)
        {
            return Conditions.MeetsConditionLists(Items[slot].Descriptor.DestroyRequirements, this, null);
        }

        public void DestroyItem(int slot, int quantity)
        {
            TryTakeItem(Items[slot], quantity);
        }

        /// <summary>
        /// Try to take an item away from the player by slot.
        /// </summary>
        /// <param name="slot">The inventory slot to take the item away from.</param>
        /// <param name="amount">The amount of this item we intend to take away from the player.</param>
        /// <param name="handler">The method in which we intend to handle taking away the item from our player.</param>
        /// <param name="sendUpdate">Do we need to send an inventory update after taking away the item.</param>
        /// <param name="forceTake">Do we force take and ignore destruction reqs?</param>
        /// <returns></returns>
        public bool TryTakeItem(InventorySlot slot, int amount, ItemHandling handler = ItemHandling.Normal, bool sendUpdate = true, bool forceTake = false)
        {
            if (Items == null || slot == Item.None || slot == null)
            {
                return false;
            }

            // Figure out how many we can take!
            var toTake = 0;
            switch (handler)
            {
                case ItemHandling.Normal:
                case ItemHandling.Overflow: // We can't overflow a take command, so process it as if it's a normal one.
                    if (slot.Quantity < amount) // Cancel out if we don't have enough items to cover for this.
                    {
                        return false;
                    }

                    // We can take all of the items we need!
                    toTake = amount;

                    break;
                case ItemHandling.UpTo:
                    // Can we take all our items or just some?
                    toTake = slot.Quantity >= amount ? amount : slot.Quantity;

                    break;

                // Did you forget something? ;)
                default:
                    throw new NotImplementedException();
            }

            // Can we actually take any?
            if (toTake == 0)
            {
                return false;
            }

            // Figure out what we're dealing with here.
            var itemDescriptor = slot.Descriptor;
            if (itemDescriptor.CanDestroy && !(Conditions.MeetsConditionLists(itemDescriptor.DestroyRequirements, this, null)) && !forceTake)
            {
                if (!string.IsNullOrEmpty(itemDescriptor.CannotDestroyMessage))
                {
                    PacketSender.SendChatMsg(this, itemDescriptor.CannotDestroyMessage, ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                } else
                {
                    PacketSender.SendChatMsg(this, Strings.Items.destroydefault, ChatMessageType.Error, CustomColors.General.GeneralDisabled);
                }

                return false;
            }

            // is this stackable? if so try to take as many as we can each time.
            if (itemDescriptor.Stackable)
            {
                if (slot.Quantity >= toTake)
                {
                    TakeItem(slot, toTake, sendUpdate);
                    toTake = 0;
                }
                else // Take away the entire quantity of the item and lower our items that we still need to take!
                {
                    toTake -= slot.Quantity;
                    TakeItem(slot, slot.Quantity, sendUpdate);
                }
            }
            else // Not stackable, so just take one item away.
            {
                toTake -= 1;
                TakeItem(slot, 1, sendUpdate);
            }

            // Update quest progress and we're done!
            UpdateGatherItemQuests(slot.ItemId);

            // Start common events related to inventory changes.
            AddDeferredEvent(CommonEventTrigger.InventoryChanged);

            return true;

        }

        /// <summary>
        /// Try to take away an item from the player by Id.
        /// </summary>
        /// <param name="itemId">The Id of the item we're trying to take away from the player.</param>
        /// <param name="amount">The amount of this item we intend to take away from the player.</param>
        /// <param name="handler">The method in which we intend to handle taking away the item from our player.</param>
        /// <param name="sendUpdate">Do we need to send an inventory update after taking away the item.</param>
        /// <returns>Whether the item was taken away successfully or not.</returns>
        public bool TryTakeItem(Guid itemId, int amount, ItemHandling handler = ItemHandling.Normal, bool sendUpdate = true)
        {
            if (itemId == Guid.Empty)
            {
                return true;
            }

            if (Items == null)
            {
                return false;
            }

            // Figure out how many we can take!
            var toTake = 0;
            switch (handler)
            {
                case ItemHandling.Normal:
                case ItemHandling.Overflow: // We can't overflow a take command, so process it as if it's a normal one.
                    if (!CanTakeItem(itemId, amount)) // Cancel out if we don't have enough items to cover for this.
                    {
                        return false;
                    }

                    // We can take all of the items we need!
                    toTake = amount;

                    break;
                case ItemHandling.UpTo:
                    // Can we take all our items or just some?
                    var itemCount = FindInventoryItemQuantity(itemId);
                    toTake = itemCount >= amount ? amount : itemCount;

                    break;

                // Did you forget something? ;)
                default:
                    throw new NotImplementedException();
            }

            // Can we actually take any?
            if (toTake == 0)
            {
                return false;
            }

            // Figure out what we're dealing with here.
            var itemDescriptor = ItemBase.Get(itemId);

            // Go through our inventory and take what we need!
            foreach (var slot in FindInventoryItemSlots(itemId))
            {
                // Do we still have items to take? If not leave the loop!
                if (toTake == 0)
                {
                    break;
                }

                // is this stackable? if so try to take as many as we can each time.
                if (itemDescriptor.Stackable)
                {
                    if (slot.Quantity >= toTake)
                    {
                        TakeItem(slot, toTake, sendUpdate);
                        toTake = 0;
                    }
                    else // Take away the entire quantity of the item and lower our items that we still need to take!
                    {
                        toTake -= slot.Quantity;
                        TakeItem(slot, slot.Quantity, sendUpdate);
                    }
                }
                else // Not stackable, so just take one item away.
                {
                    toTake -= 1;
                    TakeItem(slot, 1, sendUpdate);
                }
            }

            // Get any remaining items from bags
            foreach (var slot in FindBagSlotsInItemSlots(itemId))
            {
                if(!slot.Key.TryGetBag(out var bag))
                {
                    continue;
                }
                foreach (var bagSlot in slot.Value)
                {
                    var sendBagUpdate = false;
                    // Do we still have items to take? If not leave the loop!
                    if (toTake == 0)
                    {
                        break;
                    }

                    // is this stackable? if so try to take as many as we can each time.
                    if (itemDescriptor.Stackable)
                    {
                        if (bagSlot.Quantity >= toTake)
                        {
                            sendBagUpdate = bag.TryTakeItemFromSlot(bagSlot.Slot, toTake) || sendBagUpdate;
                            toTake = 0;
                        }
                        else // Take away the entire quantity of the item and lower our items that we still need to take!
                        {
                            toTake -= bagSlot.Quantity;
                            sendBagUpdate = bag.TryTakeItemFromSlot(bagSlot.Slot, toTake) || sendBagUpdate;
                        }
                    }
                    else // Not stackable, so just take one item away.
                    {
                        toTake -= 1;
                        sendBagUpdate = bag.TryTakeItemFromSlot(bagSlot.Slot, toTake) || sendBagUpdate;
                    }

                    if (sendBagUpdate && InBag != null && InBag.Id == bag.Id)
                    {
                        PacketSender.SendBagUpdate(this, bagSlot.Slot, bagSlot);
                    }
                }
            }

            // Update quest progress and we're done!
            UpdateGatherItemQuests(itemId);

            // Start common events related to inventory changes.
            AddDeferredEvent(CommonEventTrigger.InventoryChanged);

            return true;
        }

        /// <summary>
        /// Take an item away from the player, or an amount of it if they have more. NOTE: This method MAKES ZERO CHECKS to see if this is possible!
        /// Use TryTakeItem where possible!
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="amount"></param>
        /// <param name="sendUpdate"></param>
        private void TakeItem(InventorySlot slot, int amount, bool sendUpdate = true)
        {
            if (slot.Quantity > amount) // This slot contains more than what we're trying to take away here. Update the quantity.
            {
                slot.Quantity -= amount;
            }
            else // Take the entire thing away!
            {
                slot.Set(Item.None);
                EquipmentProcessItemLoss(slot.Slot);
            }

            AddDeferredEvent(CommonEventTrigger.InventoryChanged);
            if (sendUpdate)
            {
                PacketSender.SendInventoryItemUpdate(this, slot.Slot);
            }
        }

        /// <summary>
        /// Find the amount of a specific item a player has.
        /// </summary>
        /// <param name="itemId">The item Id to look for.</param>
        /// <returns>The amount of the requested item the player has on them.</returns>
        public int FindInventoryItemQuantity(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                return 0;
            }

            if (Items == null)
            {
                return 0;
            }

            long itemCount = 0;
            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                var item = Items[i];
                if (item.ItemId == itemId)
                {
                    itemCount = item.Descriptor.Stackable ? itemCount += item.Quantity : itemCount += 1;
                }
                else if (item.TryGetBag(out var bag))
                {
                    foreach (var slot in bag.Slots)
                    {
                        if (slot.ItemId == itemId)
                        {
                            itemCount = slot.Descriptor.Stackable ? itemCount += slot.Quantity : itemCount += 1;
                        }
                    }
                }
            }

            // TODO: Stop using Int32 for item quantities
            return itemCount >= Int32.MaxValue ? Int32.MaxValue : (int) itemCount;
        }

        /// <summary>
        /// Finds an inventory slot matching the desired item and quantity.
        /// </summary>
        /// <param name="itemId">The item Id to look for.</param>
        /// <param name="quantity">The quantity of the item to look for.</param>
        /// <returns>An <see cref="InventorySlot"/> that contains the item, or null if none are found.</returns>
        public InventorySlot FindInventoryItemSlot(Guid itemId, int quantity = 1) => FindInventoryItemSlots(itemId, quantity).FirstOrDefault();

        /// <summary>
        /// Finds the index of a given inventory slot
        /// </summary>
        /// <param name="slot">The <see cref="InventorySlot"/> to find</param>
        /// <returns>An <see cref="int"/>containing the relevant index, or -1 if not found</returns>
        public int FindInventoryItemSlotIndex(InventorySlot slot)
        {
            return Items.FindIndex(sl => sl.Id == slot.Id);
        }

        /// <summary>
        /// Finds all inventory slots matching the desired item and quantity.
        /// </summary>
        /// <param name="itemId">The item Id to look for.</param>
        /// <param name="quantity">The quantity of the item to look for.</param>
        /// <returns>A list of <see cref="InventorySlot"/> containing the requested item.</returns>
        public List<InventorySlot> FindInventoryItemSlots(Guid itemId, int quantity = 1)
        {
            var slots = new List<InventorySlot>();
            if (Items == null)
            {
                return slots;
            }

            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                var item = Items[i];
                if (item?.ItemId != itemId)
                {
                    continue;
                }

                if (item.Quantity >= quantity)
                {
                    slots.Add(Items[i]);
                }
            }

            return slots;
        }

        public List<KeyValuePair<InventorySlot, List<BagSlot>>> FindBagSlotsInItemSlots(Guid itemId, int quantity = 1)
        {
            var slots = new List<KeyValuePair<InventorySlot, List<BagSlot>>>();
            if (Items == null)
            {
                return slots;
            }

            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                var item = Items[i];
                if (!item.TryGetBag(out var bag))
                {
                    continue;
                }

                var bagItems = bag.FindBagItemSlots(itemId, quantity);

                if (bagItems.Count == 0)
                {
                    continue;
                }

                slots.Add(new KeyValuePair<InventorySlot, List<BagSlot>>(item, bagItems));
            }

            return slots;
        }

        public int CountItems(Guid itemId, bool inInventory = true, bool inBank = false)
        {
            if (inInventory == false && inBank == false)
            {
                throw new ArgumentException(
                    $@"At least one of either {nameof(inInventory)} or {nameof(inBank)} must be true to count items."
                );
            }

            var count = 0;

            int QuantityFromSlot(Item item)
            {
                return item?.ItemId == itemId ? Math.Max(1, item.Quantity) : 0;
            }

            if (inInventory)
            {
                count += Items.Sum(QuantityFromSlot);
            }

            if (inBank)
            {
                count += Bank.Sum(QuantityFromSlot);
            }

            return count;
        }

        public bool HasItemWithTag(string tag, bool inInventory = true, bool inBank = false)
        {
            if (inInventory == false && inBank == false)
            {
                throw new ArgumentException(
                    $@"At least one of either {nameof(inInventory)} or {nameof(inBank)} must be true to count items."
                );
            }

            if (inInventory)
            {
                foreach (var item in Items)
                {
                    if (item?.Descriptor?.Tags != null && item?.Descriptor?.Tags.Count > 0)
                    {
                        if (item.Descriptor.Tags.Contains(tag))
                        {
                            return true;
                        }
                    }
                }
            }

            if (inBank)
            {
                foreach (var item in Bank)
                {
                    if (item?.Descriptor?.Tags != null && item?.Descriptor?.Tags.Count > 0)
                    {
                        if (item.Descriptor.Tags.Contains(tag)) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override int GetWeaponDamage()
        {
            if(!TryGetEquippedItem(Options.WeaponIndex, out var item))
            {
                return 0;
            }

            return item.Descriptor.Damage;
        }

        /// <summary>
        /// Gets the value of a bonus effect as granted by the currently equipped gear.
        /// </summary>
        /// <param name="effect">The <see cref="EffectType"/> to retrieve the amount for.</param>
        /// <param name="startValue">The starting value to which we're adding our gear amount.</param>
        /// <returns></returns>
        public override int GetBonusEffectTotal(EffectType effect, int startValue = 0)
        {
            var value = startValue;

            foreach(var item in EquippedItems)
            {
                value += item.Descriptor.GetEffectPercentage(effect);
                value += ItemInstanceHelper.GetEffectBoost(item.ItemProperties, effect);
            }

            return value + PassiveEffectTotal(effect) + GetChallengeEffects(effect);
        }

        public override float GetBonusEffectPercent(EffectType effect, bool additive, int startValue = 0)
        {
            if (!additive)
            {
                return 1 - GetBonusEffectTotal(effect, startValue) / 100f;
            }
            else
            {
                return 1 + GetBonusEffectTotal(effect, startValue) / 100f;
            }
        }

        public int GetEquipmentVitalRegen(Vitals vital)
        {
            var regen = 0;

            foreach (var item in EquippedItems)
            {
                regen += item.Descriptor.VitalsRegen[(int)vital];
            }

            return regen;
        }

        //Shop
        public bool OpenShop(ShopBase shop)
        {
            if (IsBusy())
            {
                return false;
            }

            InShop = shop;
            PacketSender.SendOpenShop(this, shop);

            StartCommonEventsWithTrigger(CommonEventTrigger.OpenShop);

            return true;
        }

        public void CloseShop()
        {
            if (InShop != null)
            {
                InShop = null;
                PacketSender.SendCloseShop(this);
            }
        }

        public bool TrySellItem(int slot, int amount, out int rewardItemVal, out Guid rewardItemId, bool sendUpdate = true)
        {
            var canSellItem = true;
            rewardItemId = Guid.Empty;
            rewardItemVal = 0;

            TryGetSlot(slot, out var itemInSlot, true);
            var sellItemNum = itemInSlot.ItemId;
            var shop = InShop;
            if (shop == null)
            {
                // Player is not in a shop and can't sell items
                return false;
            }

            var itemDescriptor = itemInSlot.Descriptor;
            if (itemDescriptor == null)
            {
                // Item doesn't exist
                return false;
            }

            if (!itemDescriptor.CanSell)
            {
                PacketSender.SendChatMsg(this, Strings.Shops.bound, ChatMessageType.Inventory, CustomColors.Items.Bound);

                return false;
            }

            //Check if this is a bag with items.. if so don't allow sale
            if (itemDescriptor.ItemType == ItemTypes.Bag)
            {
                if (itemInSlot.TryGetBag(out var bag))
                {
                    if (!bag.IsEmpty)
                    {
                        PacketSender.SendChatMsg(this, Strings.Bags.onlysellempty, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                        return false;
                    }
                }
            }

            if (!shop.BuysItem(itemDescriptor))
            {
                PacketSender.SendChatMsg(this, Strings.Shops.doesnotaccept, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                return false;
            }

            // Always prefer specified sales to non-specified ones (blacklist, tag-whitelist) sales
            if (shop.BuyingWhitelist && shop.BuyingItems.Find(item => item.ItemId == itemDescriptor.Id) != null)
            {
                var itemBuyProps = shop.BuyingItems.Find(item => item.ItemId == itemDescriptor.Id);
                rewardItemId = itemBuyProps.CostItemId;
                rewardItemVal = itemBuyProps.CostItemQuantity;
            }
            else
            {
                // Give the default currency, with the bonus multiplier
                rewardItemVal = (int)Math.Ceiling(itemDescriptor.Price * shop.BuyMultiplier);
                rewardItemId = shop.DefaultCurrency.Id;
            }

            amount = Math.Min(itemInSlot.Quantity, amount);

            if (amount == itemInSlot.Quantity)
            {
                // Definitely can get reward.
                itemInSlot.Set(Item.None);
                EquipmentProcessItemLoss(slot);
            }
            else
            {
                //check if can get reward
                if (!CanGiveItem(rewardItemId, rewardItemVal))
                {
                    canSellItem = false;
                }
                else
                {
                    itemInSlot.Quantity -= amount;
                }
            }

            rewardItemVal *= amount;
            if (sendUpdate)
            {
                if (canSellItem)
                {
                    TryGiveItem(rewardItemId, rewardItemVal);

                    if (!TextUtils.IsNone(shop.SellSound))
                    {
                        PacketSender.SendPlaySound(this, shop.SellSound);
                    }
                }

                PacketSender.SendInventoryItemUpdate(this, slot);
            }
           
            return true;
        }

        public enum ItemMovementType
        {
            Drop,
            Deposit,
            Trade,
            Sell,
        }

        /// <summary>
        /// Used as a general place to transfer items across multiple inventory slots to another place
        /// </summary>
        /// <param name="slots">Which inventory slots are going to be moved</param>
        /// <param name="quantity">The total amount of items to be moved</param>
        /// <param name="movementType">The type of movement - bank, drop, etc.</param>
        public void MultislotTransfer(int[] slots, int quantity, ItemMovementType movementType)
        {
            int amountSold = 0;
            Guid itemGiven = Guid.Empty;

            if (slots.Length < 0)
            {
                return;
            }

            foreach (var slot in slots)
            {
                if (!TryGetItemAt(slot, out var item) || item.Descriptor == null)
                {
                    continue;
                }

                if (quantity <= 0)
                {
                    break;
                }

                var next = quantity - item.Quantity;
                var isStackable = item.Descriptor.IsStackable;
                var takeAmount = isStackable ?
                    Math.Min(item.Descriptor.MaxInventoryStack, quantity) :
                    1;

                switch (movementType)
                {
                    case ItemMovementType.Drop:
                        if (!TryDropItemFrom(slot, takeAmount, false))
                        {
                            SendAlert();
                            break;
                        }
                        break;
                    case ItemMovementType.Trade:
                        if (!TryOfferItem(slot, takeAmount))
                        {
                            SendAlert();
                            break;
                        }
                        break;
                    case ItemMovementType.Deposit:
                        if (!BankInterface?.TryDepositItem(slot, takeAmount, false) ?? false)
                        {
                            SendAlert();
                            break;
                        }
                        break;
                    case ItemMovementType.Sell:
                        if (!TrySellItem(slot, takeAmount, out var newAmountSold, out itemGiven, false))
                        {
                            SendAlert();
                            break;
                        }
                        amountSold += newAmountSold;
                        break;
                }

                if (isStackable)
                {
                    quantity = next;
                }
                else
                {
                    quantity--;
                }
            }

            PacketSender.SendInventory(this);

            if (movementType == ItemMovementType.Sell)
            {
                TryGiveItem(itemGiven, amountSold);

                if (!TextUtils.IsNone(InShop?.SellSound))
                {
                    PacketSender.SendPlaySound(this, InShop?.SellSound);
                }
            }

            if (movementType == ItemMovementType.Deposit)
            {
                BankInterface?.SendOpenBank();
            }
        }

        public void BuyItem(int slot, int amount)
        {
            var canSellItem = true;
            var buyItemNum = Guid.Empty;
            var buyItemAmt = 1;
            var shop = InShop;
            if (shop != null)
            {
                if (slot >= 0 && slot < shop.SellingItems.Count)
                {
                    var itemBase = ItemBase.Get(shop.SellingItems[slot].ItemId);
                    if (itemBase != null)
                    {
                        buyItemNum = shop.SellingItems[slot].ItemId;
                        if (itemBase.IsStackable)
                        {
                            buyItemAmt = Math.Max(1, amount);
                        }

                        if (shop.SellingItems[slot].CostItemQuantity == 0 ||
                            FindInventoryItemSlot(
                                shop.SellingItems[slot].CostItemId,
                                shop.SellingItems[slot].CostItemQuantity * buyItemAmt
                            ) !=
                            null)
                        {
                            if (CanGiveItem(buyItemNum, buyItemAmt))
                            {
                                if (shop.SellingItems[slot].CostItemQuantity > 0)
                                {
                                    TryTakeItem(
                                        FindInventoryItemSlot(
                                            shop.SellingItems[slot].CostItemId,
                                            shop.SellingItems[slot].CostItemQuantity * buyItemAmt
                                        ), shop.SellingItems[slot].CostItemQuantity * buyItemAmt
                                    );
                                }

                                TryGiveItem(buyItemNum, buyItemAmt);

                                if (!TextUtils.IsNone(shop.BuySound))
                                {
                                    PacketSender.SendPlaySound(this, shop.BuySound);
                                }
                            }
                            else
                            {
                                var itemInInventory = FindInventoryItemSlot(
                                    shop.SellingItems[slot].CostItemId,
                                    shop.SellingItems[slot].CostItemQuantity * buyItemAmt
                                );
                                if (itemInInventory == null)
                                {
                                    PacketSender.SendChatMsg(
                                        this, Strings.Shops.inventoryfull, ChatMessageType.Inventory, CustomColors.Alerts.Error, Name
                                    );
                                    return;
                                }

                                if (shop.SellingItems[slot].CostItemQuantity * buyItemAmt ==
                                    Items[itemInInventory.Slot].Quantity)
                                {
                                    TryTakeItem(
                                        FindInventoryItemSlot(
                                            shop.SellingItems[slot].CostItemId,
                                            shop.SellingItems[slot].CostItemQuantity * buyItemAmt
                                        ), shop.SellingItems[slot].CostItemQuantity * buyItemAmt
                                    );

                                    TryGiveItem(buyItemNum, buyItemAmt);

                                    if (!TextUtils.IsNone(shop.BuySound))
                                    {
                                        PacketSender.SendPlaySound(this, shop.BuySound);
                                    }
                                }
                                else
                                {
                                    PacketSender.SendChatMsg(
                                        this, Strings.Shops.inventoryfull, ChatMessageType.Inventory, CustomColors.Alerts.Error, Name
                                    );
                                }
                            }
                        }
                        else
                        {
                            PacketSender.SendChatMsg(this, Strings.Shops.cantafford, ChatMessageType.Inventory, CustomColors.Alerts.Error, Name);
                        }
                    }
                }
            }
        }

        //Crafting
        public bool OpenCraftingTable(Guid tableId)
        {
            if (IsBusy())
            {
                return false;
            }

            if (tableId != null && tableId != Guid.Empty)
            {
                UpdateCraftingTable(tableId);
            }

            PacketSender.SendKnownEnhancementUpdate(this);
            PacketSender.SendUnlockedCosmeticsPacket(this);
            return true;
        }

        public void CloseCraftingTable()
        {
            if (CraftingTableId != Guid.Empty && CraftId == Guid.Empty)
            {
                CraftingTableId = Guid.Empty;
                StopCrafting();
                PacketSender.SendCloseCraftingTable(this);
            }
        }

        public void UpdateCraftingTable(Guid tableId)
        {
            var table = CraftingTableBase.Get(tableId);

            table.HiddenCrafts.Clear();
            foreach (Guid craftId in table.Crafts)
            {
                if (!CraftBase.TryGet(craftId, out var craft))
                {
                    continue;
                }

                if (!Conditions.MeetsConditionLists(craft.Requirements, this, null))
                {
                    table.HiddenCrafts.Add(craftId);
                    continue;
                }

                // If the player doesn't have the recipe even _visible_ as an unlock, then don't show the craft, either
                var recipe = RecipeDescriptor.Get(craft.Recipe);
                if (recipe != default)
                {
                    if (!UnlockedRecipeIds.Contains(recipe.Id) && !Conditions.MeetsConditionLists(recipe.Requirements, this, null))
                    {
                        table.HiddenCrafts.Add(craftId);
                    }
                }
            }

            if (CanOpenCraftingTable(table))
            {
                CraftingTableId = table.Id;
                PacketSender.SendOpenCraftingTable(this, table);
            }
            else
            {
                PacketSender.SendChatMsg(this, Strings.Player.noviablecrafts, ChatMessageType.Local, CustomColors.Alerts.Declined);
                PacketSender.SendCloseCraftingTable(this);
            }
        }

        public bool CanOpenCraftingTable(CraftingTableBase table)
        {
            return table.HiddenCrafts.Count < table.Crafts.Count;
        }

        /// <summary>
        /// Creates a dictionary containing item IDs and their quantities
        /// </summary>
        /// <param name="items">A <see cref="List{InventorySlot}"/>containing inventory items</param>
        /// <returns>A <see cref="Dictionary{Guid, int}"/> containing item IDs and their quantities within the
        /// given list of inventory slots</returns>
        public static Dictionary<Guid, int> GetAllItemsAndQuantities(List<InventorySlot> items)
        {
            var itemdict = new Dictionary<Guid, int>();
            foreach (var item in items)
            {
                if (item != null)
                {
                    if (itemdict.ContainsKey(item.ItemId))
                    {
                        itemdict[item.ItemId] += item.Quantity;
                    }
                    else
                    {
                        itemdict.Add(item.ItemId, item.Quantity);
                    }
                }
            }

            return itemdict;
        }

        public bool CheckHasCraftIngredients(Guid craftId, Dictionary<Guid, int> itemsAndQuantities)
        {
            var craft = CraftBase.Get(craftId);
            foreach (var ingredient in craft.Ingredients)
            {
                if (itemsAndQuantities.ContainsKey(ingredient.ItemId))
                {
                    if (itemsAndQuantities[ingredient.ItemId] >= ingredient.Quantity)
                    {
                        itemsAndQuantities[ingredient.ItemId] -= ingredient.Quantity;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        //Craft a new item
        public bool TryCraftItem(Guid id)
        {
            if (PlayerDead)
            {
                return false;
            }

            // If we suddenly can't craft for any reason, abort
            if (!CheckCrafting(id))
            {
                return false;
            }

            // Make a backup of inventory items in case things go sour
            var invbackup = new List<Item>();
            foreach (var item in Items)
            {
                invbackup.Add(item.Clone());
            }

            var craft = CraftBase.Get(id);

            var ingredientUsages = new Dictionary<Guid, int>();
            //Take the items
            foreach (var c in craft.Ingredients)
            {
                // If we fail to take any of the items...
                if (!TryTakeItem(c.ItemId, c.Quantity))
                {
                    // Refund the backups
                    for (var i = 0; i < invbackup.Count; i++)
                    {
                        Items[i].Set(invbackup[i]);
                    }

                    // and alert the client
                    PacketSender.SendInventory(this);

                    return false;
                }

                if (ingredientUsages.ContainsKey(c.ItemId))
                {
                    ingredientUsages[c.ItemId] += c.Quantity;
                }
                else
                {
                    ingredientUsages[c.ItemId] = c.Quantity;
                }
            }

            //Give them the craft
            var quantity = Math.Max(craft.Quantity, 1);
            var itm = ItemBase.Get(craft.ItemId);
            if (itm == null || !itm.IsStackable)
            {
                quantity = 1;
            }

            if (craft.EventOnly || TryGiveItem(craft.ItemId, quantity, this))
            {
                // Tell the player about their new craft!
                string itemName = ItemBase.GetName(craft.ItemId);
                PacketSender.SendChatMsg(
                    this, Strings.Crafting.crafted.ToString(itemName), ChatMessageType.Crafting,
                    CustomColors.Alerts.Success
                );

                // Update our record of how many of this item we've crafted
                long recordCrafted = IncrementRecord(RecordType.ItemCrafted, id);

                // Update our record of how many times we've used certain items in crafting
                foreach (var usage in ingredientUsages)
                {
                    IncrementRecord(RecordType.ItemUsedInCraft, usage.Key, amount: usage.Value);
                }
                
                if (Options.SendCraftingRecordUpdates && recordCrafted % Options.CraftingRecordUpdateInterval == 0)
                {
                    SendRecordUpdate(Strings.Records.itemcrafted.ToString(recordCrafted, itemName));
                }

                // Give inspiration exp
                GiveInspiredExperience(craft.Experience);
                if (itm.EquipmentSlot == Options.WeaponIndex && itm.WeaponTypes.Count > 0)
                {
                    AddCraftWeaponExp(itm, 1.0f);
                }

                if (itm.StudySuccessful(GetLuckModifier()) && TryUnlockEnhancement(itm.StudyEnhancement))
                {
                    PacketSender.SendKnownEnhancementUpdate(this);
                }

                if (itm.ArmorCosmeticUnlocked(GetLuckModifier()) && TryChangeCosmeticUnlockStatus(itm.Id, true))
                {
                    PacketSender.SendUnlockedCosmeticsPacket(this);
                }

                // Start any related common events
                if (CraftBase.Get(id).Event != null)
                {
                    EnqueueStartCommonEvent(craft.Event);
                }

                return true;
            }
            else
            {
                // Refund
                for (var i = 0; i < invbackup.Count; i++)
                {
                    Items[i].Set(invbackup[i]);
                }

                // Tell the player the tragic story
                PacketSender.SendInventory(this);
                PacketSender.SendChatMsg(
                    this, Strings.Crafting.nospace.ToString(ItemBase.GetName(CraftBase.Get(id).ItemId)), ChatMessageType.Crafting,
                    CustomColors.Alerts.Error
                );

                return false;
            }
        }

        public bool CheckCrafting(Guid id)
        {
            if (CraftingTableId == default || id == default)
            {
                return false;
            }

            var table = CraftingTableBase.Get(CraftingTableId);
            if (!table.Crafts.Contains(id))
            {
                return false;
            }

            var craft = CraftBase.Get(id);
            // Make sure crafting requirements are still met
            if (!Conditions.MeetsConditionLists(craft.Requirements, this, null))
            {
                return false;
            }

            // Make sure we have any required recipe unlocked
            if (craft.Recipe != Guid.Empty && !UnlockedRecipeIds.Contains(craft.Recipe))
            {
                return false;
            }

            // Quickly look through the inventory and create a catalog of what items we have, and how many
            var itemsAndQuantities = GetAllItemsAndQuantities(Items);

            //Check the player actually has the items
            return CheckHasCraftIngredients(id, itemsAndQuantities);
        }

        //Business
        public bool IsBusy()
        {
            return InShop != null ||
                InBank ||
                CraftingTableId != Guid.Empty ||
                Trading.Counterparty != null ||
                Trading.Requester != null ||
                PartyRequester != null ||
                FriendRequester != null ||
                Deconstructor != null ||
                EnhancementOpen ||
                UpgradeStationOpen;
        }

        //Bank
        public bool OpenBank(bool guild = false)
        {
            if (IsBusy())
            {
                return false;
            }

            if (guild && Guild == null)
            {
                return false;
            }

            var bankItems = ((IEnumerable<Item>)Bank).ToList();

            if (guild)
            {
                bankItems = ((IEnumerable<Item>)Guild.Bank).ToList();
            }

            BankInterface = new BankInterface(this, bankItems, guild ? Guild.Lock : new object(), guild ? Guild : null, guild ? Guild.BankSlotsCount : Options.Instance.PlayerOpts.InitialBankslots);

            GuildBank = guild;
            BankInterface.SendOpenBank();

            StartCommonEventsWithTrigger(CommonEventTrigger.BankOpened);

            return true;
        }

        public void CloseBank()
        {
            if (InBank)
            {
                BankInterface.Dispose();
            }
        }

        // TODO: Document this. The TODO on bagItem == null needs to be resolved before this is.
        public bool OpenBag(Item bagItem, ItemBase itemDescriptor)
        {
            if (IsBusy())
            {
                return false;
            }

            // TODO: Figure out what to return in the event of a bad argument. An NRE would have happened anyway, and I don't have enough awareness of the bag feature to do this differently.
            if (bagItem == null)
            {
                throw new ArgumentNullException(nameof(bagItem));
            }

            // If the bag does not exist, create one.
            if (!bagItem.TryGetBag(out var bag))
            {
                var slotCount = itemDescriptor.SlotCount;
                if (slotCount < 1)
                {
                    slotCount = 1;
                }

                bag = new Bag(slotCount);
                bagItem.Bag = bag;
            }

            //Send the bag to the player (this will make it appear on screen)
            InBag = bag;
            PacketSender.SendOpenBag(this, bag.SlotCount, bag);

            return true;
        }

        public bool HasBag(Bag bag)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i] != null && Items[i].Bag == bag)
                {
                    return true;
                }
            }

            return false;
        }

        public Bag GetBag()
        {
            if (InBag != null)
            {
                return InBag;
            }

            return null;
        }

        public void CloseBag()
        {
            if (InBag != null)
            {
                InBag = null;
                PacketSender.SendCloseBag(this);
            }
        }

        private bool TryFillInventoryStacksOfItemFroTradeOffer(Item tradeItem, int offerIdx, List<InventorySlot> inventorySlots, ItemBase itemDescriptor, int amountToGive = 1)
        {
            int amountRemainder = amountToGive;
            foreach (var invItem in inventorySlots)
            {
                // If we've fulfilled our stacking desires, we're done
                if (amountRemainder <= 0 || FindOpenInventorySlots().Count <= 0)
                {
                    return amountRemainder <= 0;
                }
                var currSlot = FindInventoryItemSlotIndex(invItem);

                // Otherwise, first update how many of our item we still need to put in this current slot
                amountToGive = amountRemainder;
                var maxDiff = itemDescriptor.MaxInventoryStack - invItem.Quantity;
                amountRemainder = amountToGive - maxDiff;
                amountToGive = MathHelper.Clamp(amountToGive, 0, maxDiff);

                // Then, determine what the slots _new_ quantity should be
                var newQuantity = MathHelper.Clamp(invItem.Quantity + amountToGive, 0, itemDescriptor.MaxInventoryStack);
                // If the slot we're going to fill is empty, give it the item from the inventory
                if (invItem.ItemId == default)
                {
                    invItem.Set(tradeItem);
                }
                invItem.Quantity = newQuantity;

                // If we drained the inventory item's stack with that transaction, remove the inventory item from the inventory
                if (amountToGive >= tradeItem.Quantity)
                {
                    tradeItem.Set(Item.None);
                }
                // Otherwise, just reduce its quantity
                else
                {
                    tradeItem.Quantity -= amountToGive;
                }

                // Aaaand tell the client, provided any amount got given
                if (amountToGive > 0)
                {
                    PacketSender.SendInventoryItemUpdate(this, invItem.Slot);
                    PacketSender.SendTradeUpdate(this, this, offerIdx);
                    PacketSender.SendTradeUpdate(Trading.Counterparty, this, offerIdx);
                }
            } // repeat until we've either filled the bag or fulfilled our stack requirements

            return amountRemainder <= 0;
        }

        /// <summary>
        /// Fills an inventory with items from a <see cref="BagSlot"/>, mostly making sure stacks play along correctly
        /// </summary>
        /// <param name="bag">The <see cref="Bag"/> we're pulling from</param>
        /// <param name="bagSlotIdx">The index of the bag that we're choosing to withrdaw</param>
        /// <param name="inventorySlots">A list of inventory slots that are valid locations for the withdrawal</param>
        /// <param name="itemDescriptor">The <see cref="ItemBase"/> of the item that is getting moved around.</param>
        /// <param name="amountToGive">How many of the item that we're moving, if stackable.</param>
        /// <returns></returns>
        private bool TryFillInventoryStacksOfItemFromBagSlot(Bag bag, int bagSlotIdx, List<InventorySlot> inventorySlots, ItemBase itemDescriptor, int amountToGive = 1)
        {
            int amountRemainder = amountToGive;
            var bagSlots = bag.Slots;
            foreach (var inventorySlotsWithItem in inventorySlots)
            {
                // If we've fulfilled our stacking desires, we're done
                if (amountRemainder <= 0 || FindOpenInventorySlots().Count <= 0)
                {
                    return amountRemainder <= 0;
                }
                var currSlot = FindInventoryItemSlotIndex(inventorySlotsWithItem);

                // Otherwise, first update how many of our item we still need to put in this current slot
                amountToGive = amountRemainder;
                var maxDiff = itemDescriptor.MaxInventoryStack - inventorySlotsWithItem.Quantity;
                amountRemainder = amountToGive - maxDiff;
                amountToGive = MathHelper.Clamp(amountToGive, 0, maxDiff);

                // Then, determine what the slots _new_ quantity should be
                var newQuantity = MathHelper.Clamp(inventorySlotsWithItem.Quantity + amountToGive, 0, itemDescriptor.MaxInventoryStack);
                // If the slot we're going to fill is empty, give it the item from the inventory
                if (inventorySlotsWithItem.ItemId == default)
                {
                    inventorySlotsWithItem.Set(bagSlots[bagSlotIdx]);
                }
                inventorySlotsWithItem.Quantity = newQuantity;

                // If we drained the inventory item's stack with that transaction, remove the inventory item from the inventory
                if (amountToGive >= bagSlots[bagSlotIdx].Quantity)
                {
                    bagSlots[bagSlotIdx].Set(Item.None);
                }
                // Otherwise, just reduce its quantity
                else
                {
                    bagSlots[bagSlotIdx].Quantity -= amountToGive;
                }

                // Aaaand tell the client, provided any amount got given
                if (amountToGive > 0)
                {
                    PacketSender.SendInventoryItemUpdate(this, currSlot);
                    PacketSender.SendBagUpdate(this, bagSlotIdx, bagSlots[bagSlotIdx]);
                }
            } // repeat until we've either filled the bag or fulfilled our stack requirements

            return amountRemainder <= 0;
        }

        /// <summary>
        /// Fills a bag with items from a <see cref="InventorySlot"/>, mostly making sure stacks play along correctly
        /// </summary>
        /// <param name="bag">The <see cref="Bag"/> we're putting items into from</param>
        /// <param name="inventorySlotIdx">The index of the players inventory that we're withrdawing from</param>
        /// <param name="bagSlots">A list of valid bag slots that could ccontain the item</param>
        /// <param name="itemDescriptor">The <see cref="ItemBase"/> of the item that is getting moved around.</param>
        /// <param name="amountToGive">How many of the item that we're moving, if stackable.</param>
        /// <returns></returns>
        private bool TryFillBagStacksOfItemFromInventorySlot(Bag bag, int inventorySlotIdx, List<BagSlot> bagSlots, ItemBase itemDescriptor, int amountToGive = 1)
        {
            int amountRemainder = amountToGive;
            foreach (var bagSlotWithItem in bagSlots)
            {
                if (amountRemainder <= 0 || bag.FindOpenBagSlots().Count <= 0)
                {
                    return amountRemainder <= 0;
                }
                var currSlot = bag.FindSlotIndex(bagSlotWithItem);

                amountToGive = amountRemainder;
                var maxDiff = itemDescriptor.MaxInventoryStack - bagSlotWithItem.Quantity;
                amountRemainder = amountToGive - maxDiff;
                amountToGive = MathHelper.Clamp(amountToGive, 0, maxDiff);

                var newQuantity = MathHelper.Clamp(bagSlotWithItem.Quantity + amountToGive, 0, itemDescriptor.MaxInventoryStack);
                if (bagSlotWithItem.ItemId == default)
                {
                    bagSlotWithItem.Set(Items[inventorySlotIdx]);
                }
                bagSlotWithItem.Quantity = newQuantity;

                if (amountToGive >= Items[inventorySlotIdx].Quantity)
                {
                    Items[inventorySlotIdx].Set(Item.None);
                    EquipmentProcessItemLoss(inventorySlotIdx);
                }
                else
                {
                    Items[inventorySlotIdx].Quantity -= amountToGive;
                }

                if (amountToGive > 0)
                {
                    PacketSender.SendInventoryItemUpdate(this, inventorySlotIdx);
                    PacketSender.SendBagUpdate(this, currSlot, bagSlotWithItem);
                }
            }

            return amountRemainder <= 0;
        }

        public void StoreBagItem(int slot, int amount, int bagSlot)
        {
            if (InBag == null || !HasBag(InBag))
            {
                return;
            }

            var inventoryItem = Items[slot];
            var itemBase = inventoryItem.Descriptor;
            var bag = GetBag();

            if (itemBase == null || bag == null || inventoryItem.ItemId == default)
            {
                return;
            }

            if (!itemBase.CanBag || itemBase.DestroyOnInstanceChange)
            {
                PacketSender.SendChatMsg(this, Strings.Items.nobag, ChatMessageType.Inventory, CustomColors.Items.Bound);
                return;
            }

            //Make Sure we are not Storing a Bag inside of itself
            if (inventoryItem.Bag == InBag)
            {
                PacketSender.SendChatMsg(this, Strings.Bags.baginself, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                return;
            }
            if (itemBase.ItemType == ItemTypes.Bag)
            {
                PacketSender.SendChatMsg(this, Strings.Bags.baginbag, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                return;
            }

            bool specificSlot = bagSlot != -1;
            // Sanitize amount
            if (itemBase.IsStackable)
            {
                if (amount >= inventoryItem.Quantity)
                {
                    amount = Math.Min(inventoryItem.Quantity, inventoryItem.Descriptor.MaxInventoryStack);
                }
            }
            else
            {
                amount = 1;
            }

            // Sanitize currSlot - this is the slot we want to fill
            int currSlot = 0;
            // First, we'll get our slots in the order we wish to fill them - prioritizing the user's requested slot, otherwise going from the first instance of the item found
            var relevantSlots = new List<BagSlot>();
            if (specificSlot)
            {
                currSlot = bagSlot;
                var requestedSlot = bag.Slots[currSlot];
                // If the slot we're trying to fill is occupied...
                if (requestedSlot.ItemId != default && (!itemBase.IsStackable || requestedSlot.ItemId != itemBase.Id))
                {
                    // Alert the user
                    PacketSender.SendChatMsg(this, Strings.Bags.SlotOccupied, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                    return;
                }

                relevantSlots.Add(requestedSlot);
            }
            // If the item is stackable, add slots that contain that item into the mix
            if (itemBase.IsStackable)
            {
                relevantSlots.AddRange(bag.FindBagItemSlots(itemBase.Id));
            }
            // And last, add any and all open slots as valid locations for this item, if need be
            relevantSlots.AddRange(bag.FindOpenBagSlots());
            relevantSlots.Select(sl => sl).Distinct();

            // Otherwise, fill in the empty slots as much as possible
            if (!TryFillBagStacksOfItemFromInventorySlot(bag, slot, relevantSlots, itemBase, amount))
            {
                // If we're STILL not done, alert the user that we didn't have enough slots
                PacketSender.SendChatMsg(this, Strings.Bags.bagnospace, ChatMessageType.Inventory, CustomColors.Alerts.Error);
            }
        }

        public void RetrieveBagItem(int slot, int amount, int invSlot)
        {
            if (InBag == null || !HasBag(InBag))
            {
                return;
            }

            var bag = GetBag();
            if (bag == null || slot > bag.Slots.Count || bag.Slots[slot] == null)
            {
                return;
            }

            var itemBase = bag.Slots[slot].Descriptor;
            var inventorySlot = -1;
            if (itemBase == null || bag.Slots[slot] == null || bag.Slots[slot].ItemId == Guid.Empty)
            {
                return;
            }

            // Sanitize amounts
            if (itemBase.IsStackable)
            {
                if (amount >= bag.Slots[slot].Quantity)
                {
                    amount = bag.Slots[slot].Quantity;
                }
            }
            else
            {
                amount = 1;
            }

            // Sanitize currSlot - this is the slot we want to fill
            int currSlot = 0;
            // First, we'll get our slots in the order we wish to fill them - prioritizing the user's requested slot, otherwise going from the first instance of the item found
            var relevantSlots = new List<InventorySlot>();
            bool specificSlot = invSlot != -1;
            if (specificSlot)
            {
                currSlot = invSlot;
                var requestedSlot = Items[currSlot];
                // If the slot we're trying to fill is occupied...
                if (requestedSlot.ItemId != default && (!itemBase.IsStackable || requestedSlot.ItemId != itemBase.Id))
                {
                    // Alert the user
                    PacketSender.SendChatMsg(this, Strings.Bags.SlotOccupied, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                    return;
                }

                relevantSlots.Add(requestedSlot);
            }
            // If the item is stackable, add slots that contain that item into the mix
            if (itemBase.IsStackable)
            {
                relevantSlots.AddRange(FindInventoryItemSlots(itemBase.Id));
            }
            // And last, add any and all open slots as valid locations for this item, if need be
            relevantSlots.AddRange(FindOpenInventorySlots());
            relevantSlots.Select(sl => sl).Distinct();

            // Otherwise, fill in the empty slots as much as possible
            if (!TryFillInventoryStacksOfItemFromBagSlot(bag, slot, relevantSlots, itemBase, amount))
            {
                // If we're STILL not done, alert the user that we didn't have enough slots
                PacketSender.SendChatMsg(this, Strings.Bags.withdrawinvalid, ChatMessageType.Inventory, CustomColors.Alerts.Error);
            }
        }

        public void SwapBagItems(int item1, int item2)
        {
            if (InBag == null || !HasBag(InBag))
            {
                return;
            }

            var bag = GetBag();
            Item tmpInstance = null;
            if (bag.Slots[item2] != null)
            {
                tmpInstance = bag.Slots[item2].Clone();
            }

            if (bag.Slots[item1] != null)
            {
                bag.Slots[item2].Set(bag.Slots[item1]);
            }
            else
            {
                bag.Slots[item2].Set(Item.None);
            }

            if (tmpInstance != null)
            {
                bag.Slots[item1].Set(tmpInstance);
            }
            else
            {
                bag.Slots[item1].Set(Item.None);
            }

            PacketSender.SendBagUpdate(this, item1, bag.Slots[item1]);
            PacketSender.SendBagUpdate(this, item2, bag.Slots[item2]);
        }

        //Friends
        public void FriendRequest(Player fromPlayer)
        {
            if (Map?.ZoneType != MapZones.Safe && !fromPlayer.IsAllyOf(this))
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Friends.FriendEnemy, ChatMessageType.Friend, CustomColors.Alerts.Error);
                return;
            }

            if (InDuel)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Friends.FriendDuel, ChatMessageType.Friend, CustomColors.Alerts.Error);
                return;
            }

            if (fromPlayer.FriendRequests.ContainsKey(this))
            {
                fromPlayer.FriendRequests.Remove(this);
            }

            if (!FriendRequests.ContainsKey(fromPlayer) || !(FriendRequests[fromPlayer] > Timing.Global.Milliseconds))
            {
                if (!IsBusy())
                {
                    FriendRequester = fromPlayer;
                    PacketSender.SendFriendRequest(this, fromPlayer);
                    PacketSender.SendChatMsg(fromPlayer, Strings.Friends.sent, ChatMessageType.Friend, CustomColors.Alerts.RequestSent);
                }
                else
                {
                    PacketSender.SendChatMsg(
                        fromPlayer, Strings.Friends.busy.ToString(Name), ChatMessageType.Friend, CustomColors.Alerts.Error
                    );
                }
            }
        }

        public bool HasFriend(string name)
        {
            return CachedFriends.Values.Any(f => string.Equals(f, name,StringComparison.OrdinalIgnoreCase));
        }

        public Guid GetFriendId(string name)
        {
            var friend = CachedFriends.FirstOrDefault(f => string.Equals(f.Value, name, StringComparison.OrdinalIgnoreCase));
            if (friend.Value != null)
            {
                return friend.Key;
            }
            return Guid.Empty;
        }

        //Trading
        public void InviteToTrade(Player fromPlayer)
        {
            if (Trading.Requests == null)
            {
                Trading = new Trading(this);
            }

            if (Map?.ZoneType != MapZones.Safe && !fromPlayer.IsAllyOf(this))
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Trading.PvPTrade, ChatMessageType.Trading, CustomColors.Alerts.Error);
                return;
            }

            if (InDuel)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Trading.TradeDuel, ChatMessageType.Friend, CustomColors.Alerts.Error);
                return;
            }

            if (fromPlayer.Trading.Requests == null)
            {
                fromPlayer.Trading = new Trading(fromPlayer);
            }

            if (fromPlayer.Trading.Requests.ContainsKey(this))
            {
                fromPlayer.Trading.Requests.Remove(this);
            }

            if (Trading.Requests.ContainsKey(fromPlayer) && Trading.Requests[fromPlayer] > Timing.Global.Milliseconds)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Trading.alreadydenied, ChatMessageType.Trading, CustomColors.Alerts.Error);
            }
            else
            {
                if (!IsBusy())
                {
                    Trading.Requester = fromPlayer;
                    PacketSender.SendTradeRequest(this, fromPlayer);
                }
                else
                {
                    PacketSender.SendChatMsg(
                        fromPlayer, Strings.Trading.busy.ToString(Name), ChatMessageType.Trading, CustomColors.Alerts.Error
                    );
                }
            }
        }

        public bool TryOfferItem(int slot, int amount)
        {
            // TODO: Accessor cleanup
            if (Trading.Counterparty == null)
            {
                return false;
            }

            var itemBase = Items[slot].Descriptor;
            if (itemBase != null)
            {
                if (Items[slot].ItemId != Guid.Empty)
                {
                    if (itemBase.IsStackable)
                    {
                        if (amount >= Items[slot].Quantity)
                        {
                            amount = Items[slot].Quantity;
                        }
                    }
                    else
                    {
                        amount = 1;
                    }

                    //Check if the item is bound.. if so don't allow trade
                    if (!itemBase.CanTrade)
                    {
                        PacketSender.SendChatMsg(this, Strings.Bags.tradebound, ChatMessageType.Trading, CustomColors.Items.Bound);

                        return false;
                    }

                    //Check if this is a bag with items.. if so don't allow sale
                    if (itemBase.ItemType == ItemTypes.Bag)
                    {
                        if (Items[slot].TryGetBag(out var bag))
                        {
                            if (!bag.IsEmpty)
                            {
                                PacketSender.SendChatMsg(this, Strings.Bags.onlytradeempty, ChatMessageType.Trading, CustomColors.Alerts.Error);
                                return false;
                            }
                        }
                    }

                    //Find a spot in the trade for it!
                    if (itemBase.IsStackable)
                    {
                        for (var i = 0; i < Options.MaxInvItems; i++)
                        {
                            if (Trading.Offer[i] != null && Trading.Offer[i].ItemId == Items[slot].ItemId)
                            {
                                amount = Math.Min(amount, int.MaxValue - Trading.Offer[i].Quantity);
                                Trading.Offer[i].Quantity += amount;

                                //Remove Items from inventory send updates
                                if (amount >= Items[slot].Quantity)
                                {
                                    Items[slot].Set(Item.None);
                                    EquipmentProcessItemLoss(slot);
                                }
                                else
                                {
                                    Items[slot].Quantity -= amount;
                                }

                                PacketSender.SendInventoryItemUpdate(this, slot);
                                PacketSender.SendTradeUpdate(this, this, i);
                                PacketSender.SendTradeUpdate(Trading.Counterparty, this, i);

                                return true;
                            }
                        }
                    }

                    //Either a non stacking item, or we couldn't find the item already existing in the players inventory
                    for (var i = 0; i < Options.MaxInvItems; i++)
                    {
                        if (Trading.Offer[i] == null || Trading.Offer[i].ItemId == Guid.Empty)
                        {
                            Trading.Offer[i] = Items[slot].Clone();
                            Trading.Offer[i].Quantity = amount;

                            //Remove Items from inventory send updates
                            if (amount >= Items[slot].Quantity)
                            {
                                Items[slot].Set(Item.None);
                                EquipmentProcessItemLoss(slot);
                            }
                            else
                            {
                                Items[slot].Quantity -= amount;
                            }

                            PacketSender.SendInventoryItemUpdate(this, slot);
                            PacketSender.SendTradeUpdate(this, this, i);
                            PacketSender.SendTradeUpdate(Trading.Counterparty, this, i);

                            return true;
                        }
                    }

                    PacketSender.SendChatMsg(this, Strings.Trading.tradenospace, ChatMessageType.Trading, CustomColors.Alerts.Error);
                }
                else
                {
                    PacketSender.SendChatMsg(this, Strings.Trading.offerinvalid, ChatMessageType.Trading, CustomColors.Alerts.Error);
                }
            }

            return false;
        }

        public void RevokeItem(int slot, int amount)
        {
            if (Trading.Counterparty == null)
            {
                return;
            }

            if (slot < 0 || slot >= Trading.Offer.Length || Trading.Offer[slot] == null)
            {
                return;
            }

            var itemBase = Trading.Offer[slot].Descriptor;
            if (itemBase == null)
            {
                return;
            }

            if (Trading.Offer[slot] == null || Trading.Offer[slot].ItemId == Guid.Empty)
            {
                PacketSender.SendChatMsg(this, Strings.Trading.revokeinvalid, ChatMessageType.Trading, CustomColors.Alerts.Error);

                return;
            }

            // Sanitize amounts
            var inventorySlot = -1;
            var stackable = itemBase.IsStackable;
            var tradeItem = Trading.Offer[slot];
            if (stackable)
            {
                if (amount >= tradeItem.Quantity)
                {
                    amount = tradeItem.Quantity;
                }
            }
            else
            {
                amount = 1;
            }

            if (Trading.Counterparty.Trading.Accepted || Trading.Accepted)
            {
                PacketSender.SendChatMsg(this, Strings.Trading.RevokeNotAllowed, ChatMessageType.Trading, CustomColors.Alerts.Error);

                return;
            }

            // Sanitize currSlot - this is the slot we want to fill
            int currSlot = 0;
            // First, we'll get our slots in the order we wish to fill them - prioritizing the user's requested slot, otherwise going from the first instance of the item found
            var relevantSlots = new List<InventorySlot>();
            // If the item is stackable, add slots that contain that item into the mix
            if (itemBase.IsStackable)
            {
                relevantSlots.AddRange(FindInventoryItemSlots(itemBase.Id));
            }
            // And last, add any and all open slots as valid locations for this item, if need be
            relevantSlots.AddRange(FindOpenInventorySlots());
            relevantSlots.Select(sl => sl).Distinct();

            // Otherwise, fill in the empty slots as much as possible
            if (!TryFillInventoryStacksOfItemFroTradeOffer(tradeItem, slot, relevantSlots, itemBase, amount))
            {
                // If we're STILL not done, alert the user that we didn't have enough slots
                PacketSender.SendChatMsg(this, Strings.Bags.withdrawinvalid, ChatMessageType.Inventory, CustomColors.Alerts.Error);
            }
        }

        public void ReturnTradeItems()
        {
            if (Trading.Counterparty == null)
            {
                return;
            }

            foreach (var offer in Trading.Offer)
            {
                if (offer == null || offer.ItemId == Guid.Empty)
                {
                    continue;
                }

                if (!TryGiveItem(offer) && MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
                {
                    instance.SpawnItem(X, Y, offer, offer.Quantity, Id, true, ItemSpawnType.Dropped);
                    PacketSender.SendChatMsg(this, Strings.Trading.itemsdropped, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                }

                offer.ItemId = Guid.Empty;
                offer.Quantity = 0;
            }

            PacketSender.SendInventory(this);
        }

        public void CancelTrade()
        {
            if (Trading.Counterparty == null)
            {
                return;
            }

            Trading.Counterparty.ReturnTradeItems();
            PacketSender.SendChatMsg(Trading.Counterparty, Strings.Trading.declined, ChatMessageType.Trading, CustomColors.Alerts.Error);
            PacketSender.SendTradeClose(Trading.Counterparty);
            Trading.Counterparty.Trading.Counterparty = null;

            ReturnTradeItems();
            PacketSender.SendChatMsg(this, Strings.Trading.declined, ChatMessageType.Trading, CustomColors.Alerts.Error);
            PacketSender.SendTradeClose(this);
            Trading.Counterparty = null;
        }

        //Parties
        public void InviteToParty(Player fromPlayer)
        {
            if (fromPlayer == null)
            {
                return;
            }

            if (Map?.ZoneType == MapZones.Safe && !fromPlayer.IsAllyOf(this))
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Parties.PartyEnemy, ChatMessageType.Friend, CustomColors.Alerts.Error);
                return;
            }

            if (InDuel)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Parties.PartyDuel, ChatMessageType.Friend, CustomColors.Alerts.Error);
                return;
            }

            if (Party.Count != 0)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Parties.inparty.ToString(Name), ChatMessageType.Party, CustomColors.Alerts.Error);

                return;
            }

            if (!InOpenInstance || !fromPlayer.InOpenInstance)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Parties.ininstance, ChatMessageType.Party, CustomColors.Alerts.Error);

                return;
            }

            if (fromPlayer.PartyRequests.ContainsKey(this))
            {
                fromPlayer.PartyRequests.Remove(this);
            }

            if (PartyRequests.ContainsKey(fromPlayer) && PartyRequests[fromPlayer] > Timing.Global.Milliseconds)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Parties.alreadydenied, ChatMessageType.Party, CustomColors.Alerts.Error);
            }
            else
            {
                if (!IsBusy())
                {
                    PartyRequester = fromPlayer;
                    PacketSender.SendPartyInvite(this, fromPlayer);
                }
                else
                {
                    PacketSender.SendChatMsg(
                        fromPlayer, Strings.Parties.busy.ToString(Name), ChatMessageType.Party, CustomColors.Alerts.Error
                    );
                }
            }
        }

        public void AddParty(Player target)
        {
            //If a new party, make yourself the leader
            if (Party.Count == 0)
            {
                Party.Add(this);
                SendPartyTimers();
            }
            else
            {
                /* ALEX - Don't want?
                if (Party[0] != this)
                {
                    PacketSender.SendChatMsg(this, Strings.Parties.leaderinvonly, ChatMessageType.Party, CustomColors.Alerts.Error);

                    return;
                }
                */

                //Check for member being already in the party, if so cancel
                for (var i = 0; i < Party.Count; i++)
                {
                    if (Party[i] == target)
                    {
                        return;
                    }
                }
            }

            if (Party.Count < Options.Party.MaximumMembers)
            {
                target.LeaveParty();
                Party.Add(target);

                //Update all members of the party with the new list
                for (var i = 0; i < Party.Count; i++)
                {
                    Party[i].Party = Party;
                    PacketSender.SendParty(Party[i]);
                    PacketSender.SendChatMsg(
                        Party[i], Strings.Parties.joined.ToString(target.Name), ChatMessageType.Party, CustomColors.Alerts.Accepted
                    );
                }
                target.SendPartyTimers();
            }
            else
            {
                PacketSender.SendChatMsg(this, Strings.Parties.limitreached, ChatMessageType.Party, CustomColors.Alerts.Error);
            }
        }

        public void KickParty(Guid target)
        {
            if (Party.Count > 0 && Party[0] == this)
            {
                if (target != Guid.Empty)
                {
                    var oldMember = Party.Where(p => p.Id == target).FirstOrDefault();
                    if (oldMember != null)
                    {
                        oldMember.StopPartyTimers();
                        oldMember.Party = new List<Player>();
                        PacketSender.SendParty(oldMember);
                        PacketSender.SendChatMsg(oldMember, Strings.Parties.kicked, ChatMessageType.Party, CustomColors.Alerts.Error);
                        Party.Remove(oldMember);

                        // Warp the old member out of the shared instance
                        if (oldMember.InstanceType == MapInstanceType.Shared)
                        {
                            oldMember.WarpToLastOverworldLocation(false);
                        }

                        if (Party.Count > 1) //Need atleast 2 party members to function
                        {
                            //Update all members of the party with the new list
                            for (var i = 0; i < Party.Count; i++)
                            {
                                Party[i].Party = Party;
                                PacketSender.SendParty(Party[i]);
                                PacketSender.SendChatMsg(
                                    Party[i], Strings.Parties.memberkicked.ToString(oldMember.Name),
                                    ChatMessageType.Party,
                                    CustomColors.Alerts.Error
                                );
                            }
                        }
                        else if (Party.Count > 0) //Check if anyone is left on their own
                        {
                            var remainder = Party[0];
                            remainder.Party.Clear();
                            PacketSender.SendParty(remainder);
                            PacketSender.SendChatMsg(remainder, Strings.Parties.disbanded, ChatMessageType.Party, CustomColors.Alerts.Error);
                        }
                    }
                }
            }
        }

        public void LeaveParty(bool fromLogout = false)
        {
            if (Party.Count > 0 && Party.Contains(this))
            {
                var oldMember = this;

                // Remove any client timers from this player
                oldMember.StopPartyTimers();

                // Remove them from the party
                Party.Remove(this);

                // Check if any outstanding party timers exist for this party and, if so, update their owner ID to the new party owner
                if (Party.Count > 0)
                {
                    var partyTimers = TimerProcessor.ActiveTimers
                        .Where(timer => timer.Descriptor.OwnerType == TimerOwnerType.Party && timer.OwnerId == Party[0].Id)
                        .ToArray();

                    foreach (var timer in partyTimers)
                    {
                        timer.OwnerId = Party[0].Id;
                    }
                }

                if (Party.Count > 1) //Need atleast 2 party members to function
                {
                    //Update all members of the party with the new list
                    for (var i = 0; i < Party.Count; i++)
                    {
                        Party[i].Party = Party;
                        PacketSender.SendParty(Party[i]);
                        PacketSender.SendChatMsg(
                            Party[i], Strings.Parties.memberleft.ToString(oldMember.Name), ChatMessageType.Party, CustomColors.Alerts.Error
                        );
                    }
                }
                else if (Party.Count > 0) //Check if anyone is left on their own
                {
                    var remainder = Party[0];

                    remainder.StopPartyTimers();
                    // Nuke timers that existed for this disbanded party
                    foreach (var timer in TimerProcessor.ActiveTimers.Where(timer => timer.Descriptor.OwnerType == GameObjects.Timers.TimerOwnerType.Party && timer.OwnerId == Party[0].Id).ToArray())
                    {
                        TimerProcessor.RemoveTimer(timer);
                    }

                    remainder.Party.Clear();
                    PacketSender.SendParty(remainder);
                    PacketSender.SendChatMsg(remainder, Strings.Parties.disbanded, ChatMessageType.Party, CustomColors.Alerts.Error);
                }

                PacketSender.SendChatMsg(this, Strings.Parties.left, ChatMessageType.Party, CustomColors.Alerts.Error);

                // Warp the old member out of the shared instance
                if (!fromLogout && oldMember.InstanceType == MapInstanceType.Shared)
                {
                    oldMember.WarpToLastOverworldLocation(false);
                }
            }

            Party = new List<Player>();
            PacketSender.SendParty(this);
        }

        public bool InParty(Player member)
        {
            return Party.Contains(member);
        }

        public void StartTrade(Player target)
        {
            if (target?.Trading.Counterparty != null)
            {
                return;
            }

            // Set the status of both players to be in a trade
            Trading.Counterparty = target;
            target.Trading.Counterparty = this;
            Trading.Accepted = false;
            target.Trading.Accepted = false;
            Trading.Offer = new Item[Options.MaxInvItems];
            target.Trading.Offer = new Item[Options.MaxInvItems];

            for (var i = 0; i < Options.MaxInvItems; i++)
            {
                Trading.Offer[i] = new Item();
                target.Trading.Offer[i] = new Item();
            }

            //Send the trade confirmation to both players
            PacketSender.StartTrade(target, this);
            PacketSender.StartTrade(this, target);
        }

        //Spells
        public bool TryTeachSpell(Spell spell, bool sendUpdate = true)
        {
            if (spell == null || spell.SpellId == Guid.Empty)
            {
                return false;
            }

            if (KnowsSpell(spell.SpellId))
            {
                return false;
            }

            var descriptor = SpellBase.Get(spell.SpellId);

            if (descriptor == null)
            {
                return false;
            }

            if (descriptor.SpellType == SpellTypes.Passive)
            {
                ActivatePassive(spell.SpellId);
                return true;
            }

            for (var i = 0; i < Options.MaxPlayerSkills; i++)
            {
                if (Spells[i].SpellId == Guid.Empty)
                {
                    Spells[i].Set(spell);
                    if (sendUpdate)
                    {
                        PacketSender.SendPlayerSpellUpdate(this, i);
                    }

                    return true;
                }
            }

            return false;
        }

        public bool KnowsSpell(Guid spellId)
        {
            var descriptor = SpellBase.Get(spellId);

            if (descriptor == null)
            {
                return false;
            }

            if (descriptor.SpellType == SpellTypes.Passive)
            {
                return TryGetPassive(spellId, out var passive) && passive.IsActive;
            }

            for (var i = 0; i < Options.MaxPlayerSkills; i++)
            {
                if (Spells[i].SpellId == spellId)
                {
                    return true;
                }
            }

            return false;
        }

        public int FindSpell(Guid spellId)
        {
            for (var i = 0; i < Options.MaxPlayerSkills; i++)
            {
                if (Spells[i].SpellId == spellId)
                {
                    return i;
                }
            }

            return -1;
        }

        public void SwapSpells(int spell1, int spell2)
        {
            if (CastTime != 0)
            {
                PacketSender.SendChatMsg(this, "You can't swap spells while casting.", ChatMessageType.Error, CustomColors.Alerts.Error);
            }
            else
            {
                var tmpInstance = Spells[spell2].Clone();
                Spells[spell2].Set(Spells[spell1]);
                Spells[spell1].Set(tmpInstance);
                PacketSender.SendPlayerSpellUpdate(this, spell1);
                PacketSender.SendPlayerSpellUpdate(this, spell2);
            }
        }

        public void ForgetSpell(int spellSlot)
        {
            var spell = SpellBase.Get(Spells[spellSlot].SpellId);
            if (spell.Bound)
            {
                PacketSender.SendChatMsg(this, Strings.Combat.tryforgetboundspell, ChatMessageType.Spells);
                return;
            }
            if (spell.SpellType == SpellTypes.Passive)
            {
                DeactivatePassive(spell.Id);
                return;
            }

            Spells[spellSlot].Set(Spell.None);
            PacketSender.SendPlayerSpellUpdate(this, spellSlot);
        }

        public bool TryForgetSpell(Spell spell, bool sendUpdate = true)
        {
            Spell slot = null;
            var slotIndex = -1;

            for (var index = 0; index < Spells.Count; ++index)
            {
                var spellSlot = Spells[index];

                // Avoid continue;
                // ReSharper disable once InvertIf
                if (spellSlot?.SpellId == spell.SpellId)
                {
                    slot = spellSlot;
                    slotIndex = index;

                    break;
                }
            }

            if (slot == null)
            {
                return false;
            }

            var spellBase = SpellBase.Get(spell.SpellId);
            if (spellBase == null)
            {
                return false;
            }

            if (spellBase.Bound)
            {
                PacketSender.SendChatMsg(this, Strings.Combat.tryforgetboundspell, ChatMessageType.Spells);

                return false;
            }

            slot.Set(Spell.None);
            PacketSender.SendPlayerSpellUpdate(this, slotIndex);

            return true;
        }

        public override bool IsAllyOf(Entity otherEntity)
        {
            switch (otherEntity)
            {
                case Player otherPlayer:
                    return IsAllyOf(otherPlayer);
                case Npc otherNpc:
                    return otherNpc.IsAllyOf(this);
                default:
                    return base.IsAllyOf(otherEntity);
            }
        }

        public bool IsAllyOf(Player otherPlayer)
        {
            if (otherPlayer == null)
            {
                return false;
            }

            if (otherPlayer == this)
            {
                return true;
            }

            if (Dueling.Contains(otherPlayer))
            {
                return false;
            }

            var allies = InParty(otherPlayer) || (Guild?.IsMember(otherPlayer.Id) ?? false);

            if (allies)
            {
                return true;
            }
            else if (Map?.ZoneType == MapZones.Safe || otherPlayer.Map?.ZoneType == MapZones.Safe)
            {
                return true;
            }

            return false;
        }

        public void UseSpellInHotbarSlot(int spellSlot, Entity target)
        {
            if (CanStartCast(Timing.Global.Milliseconds, spellSlot, target))
            {
                StartCast(spellSlot, target);
            }
        }

        public int CalculateStealthDamage(int baseDamage, ItemBase item)
        {
            if (StealthAttack && item.ProjectileId == Guid.Empty)
            {
                return (int)Math.Ceiling(baseDamage * Options.Combat.SneakAttackMultiplier);
            }
            else
            {
                return baseDamage;
            }
        }

        public bool TryGetEquipmentSlot(int equipmentSlot, out int inventorySlot)
        {
            inventorySlot = -1;
            if (equipmentSlot > -1 && equipmentSlot < Equipment.Length)
            {
                inventorySlot = Equipment.ElementAtOrDefault(equipmentSlot);
            }

            return inventorySlot > -1;
        }

        /// <summary>
        /// Safely sets an equipment slot, if valid
        /// </summary>
        /// <param name="equipmentSlot">The slot in <see cref="Equipment"/> to set</param>
        /// <param name="inventorySlot">The inventory slot of the item</param>
        private void SetEquipmentSlot(int equipmentSlot, int inventorySlot)
        {
            if (equipmentSlot < 0 || equipmentSlot > Equipment.Length)
            {
                return;
            }

            Equipment[equipmentSlot] = inventorySlot;
        }

        /// <summary>
        /// Returns an equipped item from some equipment slot
        /// </summary>
        /// <param name="equipmentSlot">The slot in <see cref="Equipment"/> that we're checking for</param>
        /// <param name="equippedItem">The equipped <see cref="Item"/>, if found</param>
        /// <returns></returns>
        public bool TryGetEquippedItem(int equipmentSlot, out Item equippedItem)
        {
            equippedItem = null;
            if (equipmentSlot < -1 || equipmentSlot > Equipment.Length)
            {
                return false;
            }

            var itm = Items.ElementAtOrDefault(Equipment[equipmentSlot]);
            if (itm?.Descriptor == null)
            {
                return false;
            }

            equippedItem = itm;
            return true;
        }

        /// <summary>
        /// Determines whether or not a given inventory slot is currently equipped
        /// </summary>
        /// <param name="slot">The iventory slot of the item</param>
        /// <param name="equippedSlot">The equipment slot found, if any</param>
        /// <returns></returns>
        public bool SlotIsEquipped(int slot, out int equippedSlot)
        {
            equippedSlot = 0;
            foreach (var equipmentSlot in Equipment)
            {
                if (equipmentSlot == slot)
                {
                    return true;
                }
                equippedSlot++;
            }
            
            equippedSlot = -1;
            return false;
        }

        //Equipment
        public void EquipItem(ItemBase itemBase, int slot = -1)
        {
            if (itemBase == null || itemBase.ItemType != ItemTypes.Equipment)
            {
                return;
            }

            // Find the appropriate slot if not passed in
            if (slot == -1)
            {
                for (var i = 0; i < Options.MaxInvItems; i++)
                {
                    if (itemBase == Items[i].Descriptor)
                    {
                        slot = i;
                        break;
                    }
                }
            }

            if (slot != -1)
            {
                if (itemBase.EquipmentSlot == Options.WeaponIndex)
                {
                    //If we are equipping a 2hand weapon, remove the shield
                    if (itemBase.TwoHanded)
                    {
                        UnequipItem(Options.ShieldIndex, false);
                    }
                    ResetChallengeTracking();
                    RemoveWeaponSwapOnHits();
                }
                else if (itemBase.EquipmentSlot == Options.ShieldIndex)
                {
                    // If we are equipping a shield, remove any 2-handed weapon
                    if (TryGetEquippedItem(Options.WeaponIndex, out Item weapon) && weapon.Descriptor.TwoHanded)
                    {
                        UnequipItem(Options.WeaponIndex, false);
                    }
                }
                SetEquipmentSlot(itemBase.EquipmentSlot, slot);
            }

            // Void any enhancements that have been rendered invalid
            if (itemBase.EquipmentSlot == Options.WeaponIndex)
            {
                ReprocessEnhancements();
            }

            LearnSpecialAttack(itemBase);
            ProcessEquipmentUpdated(true);
        }

        public void ReprocessEnhancements()
        {
            if (!TryGetEquippedItem(Options.WeaponIndex, out var weapon))
            {
                return;
            }

            var appliedEnhancementIds = weapon.ItemProperties.AppliedEnhancementIds.ToList();
            if (appliedEnhancementIds.Count == 0)
            {
                return;
            }

            var enhanceThreshold = weapon.Descriptor.EnhancementThreshold;
            var usedEnhancementPts = 0;

            var reRoll = false;

            foreach (var enhancementId in appliedEnhancementIds.ToArray())
            {
                if (!EnhancementDescriptor.TryGet(enhancementId, out var enhancement))
                {
                    appliedEnhancementIds.Remove(enhancementId);
                    reRoll = true;
                    continue;
                }

                usedEnhancementPts += enhancement.RequiredEnhancementPoints;
                if (usedEnhancementPts > enhanceThreshold)
                {
                    appliedEnhancementIds.Remove(enhancement.Id);
                    reRoll = true;
                }
            }

            // TODO extend this method to also look to see if the underlying stat changes on an enhancement have been changed/exceeded
            if (reRoll)
            {
                // Create a temporary interface
                var tmpInterface = new EnhancementInterface(this, Guid.Empty, 0.0f);

                tmpInterface.RerollWeapon(weapon, appliedEnhancementIds.ToArray());
            }
        }

        public void UnequipItem(Guid itemId, bool sendUpdate = true)
        {
            var updated = false;
            for (int i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                var itemSlot = Equipment[i];
                if (Items.ElementAtOrDefault(itemSlot)?.ItemId == itemId)
                {
                    UnequipItem(i, false);
                    updated = true;
                }
            }
            if (!updated)
            {
                return;
            }

            ProcessEquipmentUpdated(sendUpdate);
        }

        public void UnequipItem(int equipmentSlot, bool sendUpdate = true)
        {
            if (equipmentSlot < 0 || equipmentSlot > Equipment.Length)
            {
                return;
            }

            Equipment[equipmentSlot] = -1;
            ProcessEquipmentUpdated(sendUpdate);
        }

        public void ProcessEquipmentUpdated(bool sendPackets, bool ignoreEvents = false)
        {
            FixVitals();
            if (!ignoreEvents)
            {
                AddDeferredEvent(CommonEventTrigger.EquipChange);
            }

            if (sendPackets)
            {
                PacketSender.SendPlayerEquipmentToProximity(this);
                PacketSender.SendEntityStatsToProximity(this);
            }

            SetMasteryProgress();
        }

        public void EquipmentProcessItemSwap(int item1, int item2)
        {
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] == item1)
                {
                    Equipment[i] = item2;
                }
                else if (Equipment[i] == item2)
                {
                    Equipment[i] = item1;
                }
            }

            ProcessEquipmentUpdated(true, true);
        }

        public void EquipmentProcessItemLoss(int slot)
        {
            if (SlotIsEquipped(slot, out var equippedSlot))
            {
                UnequipItem(equippedSlot, true);
            }
        }

        /// <summary>
        /// Unequips any items that the player is currently wearing in which they no longer meet the conditions for
        /// Also unequips any items that are not actually equipment anymore
        /// </summary>
        public void UnequipInvalidItems()
        {
            var updated = false;

            foreach (var item in EquippedItems)
            {
                var descriptor = item.Descriptor;
                if (descriptor == default ||
                    descriptor.ItemType != ItemTypes.Equipment ||
                    !Conditions.MeetsConditionLists(descriptor.UsageRequirements, this, null))
                {
                    UnequipItem(item.ItemId);
                }
            }

            if (updated)
            {
                ProcessEquipmentUpdated(true);
            }
        }

        public void StartCommonEventsWithTrigger(CommonEventTrigger trigger, string command = "", string param = "", long val = -1)
        {
            if (!Globals.CachedTriggeredEvents.TryGetValue(trigger, out var events))
            {
                return;
            }

            foreach (var eventDescriptor in events)
            {
                EnqueueStartCommonEvent(eventDescriptor, trigger, command, param, val);
            }

            // Run through challenge checker when these triggers are proc'd
            CheckContractsOnEventTrigger(trigger);
        }

        public static void StartCommonEventsWithTriggerForAll(CommonEventTrigger trigger, string command = "", string param = "")
        {
            var players = OnlineList;
            if (!Globals.CachedTriggeredEvents.TryGetValue(trigger, out var events))
            {
                return;
            }

            foreach (var eventDescriptor in events)
            {
                foreach (var player in players)
                {
                    player.EnqueueStartCommonEvent(eventDescriptor, trigger, command, param);
                }
            }
        }

        public static void StartCommonEventsWithTriggerForAllOnInstance(CommonEventTrigger trigger, Guid instanceId, string command = "", string param = "")
        {
            if (!InstanceProcessor.TryGetInstanceController(instanceId, out var instanceController) 
                || instanceController.PlayerCount == 0 
                || !Globals.CachedTriggeredEvents.TryGetValue(trigger, out var events))
            {
                return;
            }

            var relevantPlayers = instanceController.Players.ToArray();

            foreach (var eventDescriptor in events)
            {
                foreach (var player in relevantPlayers)
                {
                    player.EnqueueStartCommonEvent(eventDescriptor, trigger, command, param);
                }
            }
        }

        //Stats
        public void UpgradeStat(int statIndex, int amt = 1)
        {
            if (Stat[statIndex].BaseStat + StatPointAllocations[statIndex] >= Options.MaxStatValue || StatPoints <= 0)
            {
                return;
            }

            StatPointAllocations[statIndex] += amt;
            StatPoints -= amt / Options.Instance.PlayerOpts.BaseStatSkillIncrease;
        }

        public void UpgradeVital(int vitalIndex, int amt = 1)
        {
            if (GetMaxVital(vitalIndex) >= Options.Instance.PlayerOpts.MaxVital || StatPoints <= 0)
            {
                return;
            }

            VitalPointAllocations[vitalIndex] += amt;
            StatPoints -= amt / Options.Instance.PlayerOpts.BaseVitalPointIncrease;
        }

        public void ReceiveStatChange(int[] vitalsChange, int[] statsChange)
        {
            if (vitalsChange.Length < (int)Vitals.VitalCount || statsChange.Length < (int)Stats.StatCount)
            {
                return;
            }

            var idx = 0;
            foreach (var points in vitalsChange)
            {
                if (points <= 0)
                {
                    idx++;
                    continue;
                }
                UpgradeVital(idx, points * Options.Instance.PlayerOpts.BaseVitalPointIncrease);

                idx++;
            }

            idx = 0;
            foreach (var points in statsChange)
            {
                if (points <= 0)
                {
                    idx++;
                    continue;
                }
                UpgradeStat(idx, points * Options.Instance.PlayerOpts.BaseStatSkillIncrease);

                idx++;
            }

            PacketSender.SendEntityStatsToProximity(this);
            PacketSender.SendEntityVitals(this);
            PacketSender.SendPointsTo(this);
        }

        //HotbarSlot
        public void HotbarChange(int index, int type, int slot)
        {
            Hotbar[index].ItemOrSpellId = Guid.Empty;
            Hotbar[index].BagId = Guid.Empty;
            Hotbar[index].PreferredStatBuffs = new int[(int) Stats.StatCount];
            if (type == 0) //Item
            {
                var item = Items[slot];
                if (item != null)
                {
                    Hotbar[index].ItemOrSpellId = item.ItemId;
                    Hotbar[index].BagId = item.BagId ?? Guid.Empty;
                    Hotbar[index].PreferredStatBuffs = item.ItemProperties.StatModifiers;
                }
            }
            else if (type == 1) //Spell
            {
                var spell = Spells[slot];
                if (spell != null)
                {
                    Hotbar[index].ItemOrSpellId = spell.SpellId;
                }
            }
        }

        public void HotbarSwap(int index, int swapIndex)
        {
            var itemId = Hotbar[index].ItemOrSpellId;
            var bagId = Hotbar[index].BagId;
            var stats = Hotbar[index].PreferredStatBuffs;

            Hotbar[index].ItemOrSpellId = Hotbar[swapIndex].ItemOrSpellId;
            Hotbar[index].BagId = Hotbar[swapIndex].BagId;
            Hotbar[index].PreferredStatBuffs = Hotbar[swapIndex].PreferredStatBuffs;

            Hotbar[swapIndex].ItemOrSpellId = itemId;
            Hotbar[swapIndex].BagId = bagId;
            Hotbar[swapIndex].PreferredStatBuffs = stats;
        }

        // NPC Guilds
        public void JoinNpcGuildOfClass(Guid classId)
        {
            if (ClassInfo.ContainsKey(classId))
            {
                ClassInfo[classId].InGuild = true;
            } else
            {
                ClassInfo[classId] = new PlayerClassStats();
                ClassInfo[classId].InGuild = true;
            }
            PacketSender.SendChatMsg(this,
                Strings.Quests.npcguildjoin.ToString(ClassBase.Get(classId).Name),
                ChatMessageType.Quest,
                CustomColors.Quests.Completed);
        }

        public void LeaveNpcGuildOfClass(Guid classId)
        {
            if (ClassInfo.ContainsKey(classId))
            {
                // Do not allow a player to leave an NPC Guild if they're doing something for them
                if (ClassInfo[classId].OnTask || ClassInfo[classId].OnSpecialAssignment || ClassInfo[classId].TaskCompleted)
                {
                    PacketSender.SendChatMsg(this, Strings.Quests.taskinprogressleave, ChatMessageType.Error, CustomColors.General.GeneralWarning);
                }
                else
                {
                    // Reset their quest states, but keep other stuff around
                    ClassInfo[classId].OnSpecialAssignment = false;
                    ClassInfo[classId].OnTask = false;
                    ClassInfo[classId].TaskCompleted = false;
                    ClassInfo[classId].InGuild = false;

                    PacketSender.SendChatMsg(this,
                        Strings.Quests.npcguildleave.ToString(ClassBase.Get(classId).Name),
                        ChatMessageType.Quest,
                        CustomColors.Quests.Abandoned);
                }
            }
            else // Might as well backfill in the event this key never existed
            {
                ClassInfo[classId] = new PlayerClassStats();
            }
        }

        //Quests
        public bool CanStartQuest(QuestBase quest)
        {
            //Check and see if the quest is already in progress, or if it has already been completed and cannot be repeated.
            var questProgress = FindQuest(quest.Id);
            if (questProgress != null)
            {
                if (questProgress.TaskId != Guid.Empty && quest.GetTaskIndex(questProgress.TaskId) != -1)
                {
                    return false;
                }

                if (questProgress.Completed && !quest.Repeatable)
                {
                    return false;
                }
            }

            //So the quest isn't started or we can repeat it.. let's make sure that we meet requirements.
            if (!Conditions.MeetsConditionLists(quest.Requirements, this, null, true, quest))
            {
                return false;
            }

            if (quest.Tasks.Count == 0)
            {
                return false;
            }

            // Handle special quests
            if (quest.QuestType == QuestType.Task || quest.QuestType == QuestType.SpecialAssignment)
            {
                if (!ClassInfo.ContainsKey(quest.RelatedClassId))
                {
                    return false;
                }

                PlayerClassStats relevantInfo = ClassInfo[quest.RelatedClassId];

                if (!relevantInfo.InGuild)
                {
                    PacketSender.SendChatMsg(this,
                        Strings.Quests.notinguild.ToString(ClassBase.Get(quest.RelatedClassId).Name),
                        ChatMessageType.Quest,
                        CustomColors.Quests.Declined);
                    return false;
                }
                if (relevantInfo.OnTask || relevantInfo.OnSpecialAssignment)
                {
                    PacketSender.SendChatMsg(this,
                        Strings.Quests.taskinprogress,
                        ChatMessageType.Quest,
                        CustomColors.Quests.Declined);
                    return false;
                }
                if (relevantInfo.Rank < quest.QuestClassRank)
                {
                    PacketSender.SendChatMsg(this,
                        Strings.Quests.ranktoolow.ToString(quest.QuestClassRank.ToString()),
                        ChatMessageType.Quest,
                        CustomColors.Quests.Declined);
                    return false;
                }
                if (relevantInfo.LastTaskStartTime + Options.TaskCooldown > Timing.Global.MillisecondsUtc)
                {
                    PacketSender.SendChatMsg(this,
                        Strings.Quests.taskcooldown,
                        ChatMessageType.Quest,
                        CustomColors.Quests.Declined);
                    return false;
                }
            }

            return true;
        }

        public bool CanAccessQuestList(Guid questListId)
        {
            var questList = QuestListBase.Get(questListId);

            if (!Conditions.MeetsConditionLists(questList.Requirements, this, null))
            {
                return false;
            }

            return true;
        }

        public bool QuestCompleted(Guid questId)
        {
            var questProgress = FindQuest(questId);
            if (questProgress != null)
            {
                if (questProgress.Completed)
                {
                    return true;
                }
            }

            return false;
        }

        public bool QuestInProgress(Guid questId, QuestProgressState progress, Guid taskId)
        {
            var questProgress = FindQuest(questId);
            if (questProgress != null)
            {
                var quest = QuestBase.Get(questId);
                if (quest != null)
                {
                    if (questProgress.TaskId != Guid.Empty && quest.GetTaskIndex(questProgress.TaskId) != -1)
                    {
                        switch (progress)
                        {
                            case QuestProgressState.OnAnyTask:
                                return true;
                            case QuestProgressState.BeforeTask:
                                if (quest.GetTaskIndex(taskId) != -1)
                                {
                                    return quest.GetTaskIndex(taskId) > quest.GetTaskIndex(questProgress.TaskId);
                                }

                                break;
                            case QuestProgressState.OnTask:
                                if (quest.GetTaskIndex(taskId) != -1)
                                {
                                    return quest.GetTaskIndex(taskId) == quest.GetTaskIndex(questProgress.TaskId);
                                }

                                break;
                            case QuestProgressState.AfterTask:
                                if (quest.GetTaskIndex(taskId) != -1)
                                {
                                    return quest.GetTaskIndex(taskId) < quest.GetTaskIndex(questProgress.TaskId);
                                }

                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(progress), progress, null);
                        }
                    }
                }
            }

            return false;
        }

        public bool QuestInProgress(Guid questId)
        {
            var questProgress = FindQuest(questId);
            if (questProgress != null)
            {
                var quest = QuestBase.Get(questId);
                if (quest != null)
                {
                    return questProgress.TaskProgress >= 0;
                }
            }
            return false;
        }

        public void OfferQuest(QuestBase quest, bool randomQuest = false)
        {
            if (CanStartQuest(quest))
            {
                QuestOffers.Add(quest.Id);
                PacketSender.SendQuestOffer(this, quest.Id);
            }
        }

        public void OfferQuestList(Guid questListId)
        {
            QuestListBase questList = QuestListBase.Get(questListId);
            List<Guid> questsToSend = new List<Guid>();
            foreach (var quest in questList.Quests)
            {
                if ( CanStartQuest( QuestBase.Get(quest) )) {
                    QuestOffers.Add(quest);
                    questsToSend.Add(quest);
                }
            }

            if (questsToSend.Count > 0)
            {
                PacketSender.SendQuestOfferList(this, questsToSend);
            } else
            {
                PacketSender.SendChatMsg(this, Strings.Quests.reqsnotmetforlist.ToString(questList.Name), ChatMessageType.Local, CustomColors.Alerts.Declined);
            }
        }

        public bool OpenQuestBoard(QuestBoardBase questBoard)
        {
            if (IsBusy())
            {
                return false;
            }

            if (questBoard != null)
            {
                QuestBoardId = questBoard.Id;
                PacketSender.SendOpenQuestBoard(this, questBoard);
            }

            return true;
        }

        public void CloseQuestBoard()
        {
            if (QuestBoardId != Guid.Empty)
            {
                QuestBoardId = Guid.Empty;
                PacketSender.SendCloseQuestBoard(this);
            }
        }

        public Quest FindQuest(Guid questId)
        {
            foreach (var quest in Quests)
            {
                if (quest.QuestId == questId)
                {
                    return quest;
                }
            }

            return null;
        }

        public void StartQuest(QuestBase quest)
        {
            if (CanStartQuest(quest))
            {
                var questProgress = FindQuest(quest.Id);
                if (questProgress != null)
                {
                    questProgress.TaskId = quest.Tasks[0].Id;
                    questProgress.TaskProgress = 0;
                }
                else
                {
                    questProgress = new Quest(quest.Id)
                    {
                        TaskId = quest.Tasks[0].Id,
                        TaskProgress = 0
                    };

                    Quests.Add(questProgress);
                }

                if (quest.Tasks[0].Objective == QuestObjective.GatherItems) //Gather Items
                {
                    UpdateGatherItemQuests(quest.Tasks[0].TargetId);
                }

                EnqueueStartCommonEvent(EventBase.Get(quest.StartEventId));
                PacketSender.SendChatMsg(
                    this, Strings.Quests.started.ToString(quest.Name), ChatMessageType.Quest, CustomColors.Quests.Started
                );

                PacketSender.SendQuestsProgress(this);
            }
        }

        public void AcceptQuest(Guid questId)
        {
            if (QuestOffers.Contains(questId))
            {
                QuestOffers.Remove(questId);
                var quest = QuestBase.Get(questId);
                if (quest != null)
                {
                    StartQuest(quest);
                    foreach (var evt in EventLookup.ToArray())
                    {
                        if (evt.Value.CallStack.Count <= 0)
                        {
                            continue;
                        }

                        var stackInfo = evt.Value.CallStack.Peek();
                        if (stackInfo.WaitingForResponse != CommandInstance.EventResponse.Quest)
                        {
                            continue;
                        }

                        if (((StartQuestCommand) stackInfo.WaitingOnCommand).QuestId == questId)
                        {
                            var tmpStack = new CommandInstance(stackInfo.Page, stackInfo.BranchIds[0]);
                            evt.Value.CallStack.Peek().WaitingForResponse = CommandInstance.EventResponse.None;
                            evt.Value.CallStack.Push(tmpStack);
                        }
                    }
                }
            }
        }

        public void DeclineQuest(Guid questId, bool fromQuestBoard)
        {
            if (!QuestOffers.Contains(questId))
            {
                return;
            }

            QuestOffers.Remove(questId);
            if (!fromQuestBoard) // don't alert the player otherwise
            {
                PacketSender.SendChatMsg(
                    this, Strings.Quests.declined.ToString(QuestBase.GetName(questId)), ChatMessageType.Quest, CustomColors.Quests.Declined
                );
            }

            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.CallStack.Count <= 0)
                {
                    continue;
                }

                var stackInfo = evt.Value.CallStack.Peek();
                if (stackInfo.WaitingForResponse != CommandInstance.EventResponse.Quest)
                {
                    continue;
                }

                if (((StartQuestCommand)stackInfo.WaitingOnCommand).QuestId == questId)
                {
                    //Run failure branch
                    var tmpStack = new CommandInstance(stackInfo.Page, stackInfo.BranchIds[1]);
                    stackInfo.WaitingForResponse = CommandInstance.EventResponse.None;
                    evt.Value.CallStack.Push(tmpStack);
                }
            }
        }

        public void CancelQuest(Guid questId)
        {
            var quest = QuestBase.Get(questId);
            if (quest != null)
            {
                if (QuestInProgress(quest.Id, QuestProgressState.OnAnyTask, Guid.Empty))
                {
                    //Cancel the quest somehow...
                    if (quest.Quitable)
                    {
                        var questProgress = FindQuest(quest.Id);
                        questProgress.TaskId = Guid.Empty;
                        questProgress.TaskProgress = -1;

                        HandleSpecialQuestAbandon(quest);

                        PacketSender.SendChatMsg(
                            this, Strings.Quests.abandoned.ToString(QuestBase.GetName(questId)), ChatMessageType.Quest, CustomColors.Alerts.Declined
                        );

                        PacketSender.SendQuestsProgress(this);
                    }
                }
            }
        }

        public void CompleteQuestTask(Guid questId, Guid taskId, bool skipCompletion = false, bool noNotify = false)
        {
            var quest = QuestBase.Get(questId);
            if (quest != null)
            {
                var questProgress = FindQuest(questId);
                if (questProgress != null)
                {
                    if (questProgress.TaskId == taskId)
                    {
                        //Let's Advance this task or complete the quest
                        for (var i = 0; i < quest.Tasks.Count; i++)
                        {
                            if (quest.Tasks[i].Id == taskId)
                            {
                                if (!noNotify)
                                {
                                    PacketSender.SendChatMsg(this, Strings.Quests.taskcompleted, ChatMessageType.Quest);
                                }

                                if (!skipCompletion && quest.Tasks[i].CompletionEvent != null)
                                {
                                    EnqueueStartCommonEvent(quest.Tasks[i].CompletionEvent);
                                }

                                if (i == quest.Tasks.Count - 1)
                                {
                                    //Complete Quest
                                    MarkQuestComplete(quest, questProgress);
                                    EnqueueStartCommonEvent(EventBase.Get(quest.EndEventId));
                                    PacketSender.SendChatMsg(
                                        this, Strings.Quests.completed.ToString(quest.Name), ChatMessageType.Quest, CustomColors.Alerts.Accepted
                                    );
                                }
                                else
                                {
                                    //Advance Task
                                    questProgress.TaskId = quest.Tasks[i + 1].Id;
                                    questProgress.TaskProgress = 0;

                                    if (quest.Tasks[i + 1].Objective == QuestObjective.GatherItems)
                                    {
                                        UpdateGatherItemQuests(quest.Tasks[i + 1].TargetId);
                                    }

                                    if (!noNotify)
                                    {
                                        PacketSender.SendChatMsg(
                                            this, Strings.Quests.updated.ToString(quest.Name),
                                            ChatMessageType.Quest,
                                            CustomColors.Quests.TaskUpdated
                                        );
                                    }
                                }
                            }
                        }
                    }

                    PacketSender.SendQuestsProgress(this);
                }
            }
        }

        public void CompleteQuest(Guid questId, bool skipCompletionEvent)
        {
            var quest = QuestBase.Get(questId);
            if (quest != null)
            {
                var questProgress = FindQuest(questId);
                if (questProgress != null)
                {
                    MarkQuestComplete(quest, questProgress);
                    if (!skipCompletionEvent)
                    {
                        LastQuestCompleted = quest.Name;
                        EnqueueStartCommonEvent(EventBase.Get(quest.EndEventId));
                        PacketSender.SendChatMsg(this, Strings.Quests.completed.ToString(quest.Name), ChatMessageType.Quest, CustomColors.Alerts.Accepted);
                    }

                    PacketSender.SendQuestsProgress(this);
                }
            }
        }

        /// <summary>
        /// Performs common handling of quest/class info state on a quest being completed
        /// </summary>
        /// <param name="quest">Quest info from DB</param>
        /// <param name="questProgress">Players tracking of the quest, to mark as completed</param>
        private void MarkQuestComplete(QuestBase quest, Quest questProgress)
        {
            // Handle quests that aren't "normal" and should do some management on completion
            if (quest.QuestType != QuestType.Normal)
            {
                HandleSpecialQuestCompletion(quest, questProgress);
            }

            //Complete Quest
            questProgress.Completed = true;
            questProgress.TaskId = Guid.Empty;
            questProgress.TaskProgress = -1;
        }

        public void ResetQuest(Guid questId, bool agnosticCompletion)
        {
            var quest = QuestBase.Get(questId);
            if (quest != null)
            {
                var questProgress = FindQuest(questId);
                if (questProgress != null)
                {
                    MarkQuestReset(quest, questProgress, agnosticCompletion);

                    PacketSender.SendQuestsProgress(this);
                }
            }
        }

        private void MarkQuestReset(QuestBase quest, Quest questProgress, bool agnosticCompletion)
        {
            // Handle quests that aren't "normal" and should do some management on completion
            if (quest.QuestType != QuestType.Normal)
            {
                HandleSpecialQuestReset(quest, questProgress);
            }

            //Complete Quest
            if (!agnosticCompletion)
            {
                questProgress.Completed = false;
            }
            questProgress.TaskId = Guid.Empty;
            questProgress.TaskProgress = -1;
        }

        /// <summary>
        /// Sets <see cref="PlayerClassStats"/> state for a given quest at start time
        /// </summary>
        /// <param name="quest">The quest thats being started</param>
        private void HandleSpecialQuestStart(QuestBase quest)
        {
            switch(quest.QuestType)
            {
                case QuestType.Task:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var taskClassInfo))
                    {
                        taskClassInfo.OnTask = true;
                        taskClassInfo.LastTaskStartTime = Timing.Global.MillisecondsUtc;
                    }
                    break;
                case QuestType.SpecialAssignment:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var assignmentClassInfo))
                    {
                        assignmentClassInfo.OnTask = true;
                        assignmentClassInfo.OnSpecialAssignment = true;
                        if (Options.SpecialAssignmentCountsTowardCooldown)
                        {
                            assignmentClassInfo.LastTaskStartTime = Timing.Global.MillisecondsUtc;
                        }
                    }
                    break;
            }
        }

        private void HandleSpecialQuestAbandon(QuestBase quest)
        {
            switch (quest.QuestType)
            {
                case QuestType.Task:
                case QuestType.SpecialAssignment:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var taskClassInfo))
                    {
                        taskClassInfo.OnTask = false;
                        taskClassInfo.OnSpecialAssignment = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// It is imperative that this method be called BEFORE setting a <see cref="Quest"/> to Completed, so we
        /// can check for novel quest completion
        /// </summary>
        /// <param name="quest">The DB quest info</param>
        /// <param name="questProgress">The players personal quest progress</param>
        private void HandleSpecialQuestCompletion(QuestBase quest, Quest questProgress)
        {
            switch (quest.QuestType)
            {
                case QuestType.Task:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var taskClassInfo))
                    {
                        taskClassInfo.TaskCompleted = true; // The task can be turned in
                        taskClassInfo.OnTask = false;
                        if (!questProgress.Completed) // first time completing
                        {
                            // They have completed a new task - update their total
                            taskClassInfo.TotalTasksComplete++;
                            PacketSender.SendChatMsg(this,
                                           Strings.Quests.totaltaskscompleted.ToString(taskClassInfo.TotalTasksComplete.ToString(), ClassBase.Get(quest.RelatedClassId).Name),
                                           ChatMessageType.Quest,
                                           CustomColors.Quests.TaskUpdated);

                            // If this was a NEW task within their current rank, update SA progress
                            if (taskClassInfo.Rank == quest.QuestClassRank)
                            {
                                int tasksRequired = Options.RequiredTasksPerClassRank
                                    .ToArray()
                                    .ElementAtOrDefault(taskClassInfo.Rank);
                                if (tasksRequired <= 0)
                                {
                                    Log.Error($"Could not find CR Task requirement for player {Name} at CR {taskClassInfo.Rank}");
                                } else
                                {
                                    taskClassInfo.TasksRemaining = MathHelper.Clamp(taskClassInfo.TasksRemaining - 1, 0, tasksRequired);
                                    if (taskClassInfo.TasksRemaining == 0)
                                    {
                                        taskClassInfo.AssignmentAvailable = true;
                                        PacketSender.SendChatMsg(this,
                                            Strings.Quests.newspecialassignment.ToString(ClassBase.Get(quest.RelatedClassId).Name),
                                            ChatMessageType.Quest,
                                            CustomColors.Quests.Completed, sendToast: true);
                                    } else
                                    {
                                        PacketSender.SendChatMsg(this,
                                            Strings.Quests.tasksremaining.ToString(taskClassInfo.TasksRemaining.ToString(), ClassBase.Get(quest.RelatedClassId).Name),
                                            ChatMessageType.Quest,
                                            CustomColors.Quests.TaskUpdated, sendToast: true);
                                    }
                                }
                            }
                            else // Otherwise, check if they have an SA and alert them
                            {
                                if (taskClassInfo.Rank < Options.MaxClassRank)
                                {
                                    // Inform the player that this task will not count toward their next SA
                                    PacketSender.SendChatMsg(this,
                                            Strings.Quests.tasktoolow,
                                            ChatMessageType.Quest,
                                            CustomColors.Quests.TaskUpdated);
                                }
                                if (taskClassInfo.AssignmentAvailable)
                                {
                                    PacketSender.SendChatMsg(this,
                                            Strings.Quests.newspecialassignment.ToString(ClassBase.Get(quest.RelatedClassId).Name),
                                            ChatMessageType.Quest,
                                            CustomColors.Quests.Completed, sendToast: true);
                                }
                            }
                        }
                    }
                    break;
                case QuestType.SpecialAssignment:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var assignmentClassInfo))
                    {
                        // TODO Alex: Fire common event of type "Class Rank Increased" with class parameter
                        assignmentClassInfo.OnSpecialAssignment = false;
                        assignmentClassInfo.OnTask = false;
                        assignmentClassInfo.AssignmentAvailable = false;
                        if (Options.SpecialAssignmentCountsTowardCooldown)
                        {
                            assignmentClassInfo.LastTaskStartTime = Timing.Global.Milliseconds;
                        }
                        if (Options.PayoutSpecialAssignments)
                        {
                            assignmentClassInfo.TaskCompleted = true;
                        }
                        var oldRank = assignmentClassInfo.Rank;
                        assignmentClassInfo.Rank = MathHelper.Clamp(assignmentClassInfo.Rank + 1, 0, Options.MaxClassRank);
                        if (oldRank != assignmentClassInfo.Rank)
                        {
                            RecipeUnlockWatcher.RefreshPlayer(this);
                            AddDeferredEvent(CommonEventTrigger.ClassRankIncreased, "", quest.RelatedClassId.ToString());
                        }

                        // Assign the new amount of tasks remaining
                        assignmentClassInfo.TasksRemaining = TasksRemainingForClassRank(assignmentClassInfo.Rank);

                        PacketSender.SendChatMsg(this,
                            Strings.Quests.classrankincreased.ToString(ClassBase.Get(quest.RelatedClassId).Name, assignmentClassInfo.Rank.ToString()),
                            ChatMessageType.Quest,
                            CustomColors.Quests.Completed, sendToast: true);

                        if (assignmentClassInfo.TasksRemaining > 0)
                        {
                            PacketSender.SendChatMsg(this,
                                Strings.Quests.tasksremaining.ToString(assignmentClassInfo.TasksRemaining.ToString(), ClassBase.Get(quest.RelatedClassId).Name),
                                ChatMessageType.Quest,
                                CustomColors.Quests.TaskUpdated);
                        }
                        else if (assignmentClassInfo.TasksRemaining == 0)
                        {
                            PacketSender.SendChatMsg(this,
                                Strings.Quests.newspecialassignment.ToString(ClassBase.Get(quest.RelatedClassId).Name),
                                ChatMessageType.Quest,
                                CustomColors.Quests.Completed);
                        }
                    }
                    break;
            }
        }

        private void HandleSpecialQuestReset(QuestBase quest, Quest questProgress)
        {
            switch (quest.QuestType)
            {
                case QuestType.Task:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var taskClassInfo))
                    {
                        taskClassInfo.TaskCompleted = false; // The task can be turned in
                        taskClassInfo.OnTask = false;
                    }
                    break;
                case QuestType.SpecialAssignment:
                    if (quest.RelatedClassId != Guid.Empty && ClassInfo.TryGetValue(quest.RelatedClassId, out var assignmentClassInfo))
                    {
                        // TODO Alex: Fire common event of type "Class Rank Increased" with class parameter
                        assignmentClassInfo.OnSpecialAssignment = false;
                        assignmentClassInfo.OnTask = false;
                        assignmentClassInfo.TaskCompleted = false;
                    }
                    break;
            }
        }

        public int TasksRemainingForClassRank(int classRank)
        {
            if (classRank == Options.MaxClassRank)
            {
                /*
                 * Return a marker that we can use on login (since CR stuff can only be changed via server restart) to determine
                 * whether we need to refresh this value or not.
                 */
                return -1;
            }

            return Options.RequiredTasksPerClassRank.ToArray().ElementAtOrDefault(classRank);
        }

        private void UpdateGatherItemQuests(Guid itemId)
        {
            //If any quests demand that this item be gathered then let's handle it
            var item = ItemBase.Get(itemId);
            if (item != null)
            {
                foreach (var questProgress in Quests)
                {
                    var questId = questProgress.QuestId;
                    var quest = QuestBase.Get(questId);
                    if (quest != null)
                    {
                        if (questProgress.TaskId != Guid.Empty)
                        {
                            //Assume this quest is in progress. See if we can find the task in the quest
                            var questTask = quest.FindTask(questProgress.TaskId);
                            if (questTask?.Objective == QuestObjective.GatherItems && questTask.TargetId == item.Id)
                            {
                                if (questProgress.TaskProgress != CountItems(item.Id))
                                {
                                    questProgress.TaskProgress = CountItems(item.Id);
                                    if (questProgress.TaskProgress >= questTask.Quantity)
                                    {
                                        CompleteQuestTask(questId, questProgress.TaskId);
                                    }
                                    else
                                    {
                                        PacketSender.SendQuestsProgress(this);
                                        PacketSender.SendChatMsg(
                                            this,
                                            Strings.Quests.itemtask.ToString(
                                                quest.Name, questProgress.TaskProgress, questTask.Quantity,
                                                ItemBase.GetName(questTask.TargetId)
                                            ),
                                            ChatMessageType.Quest
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Switches and Variables
        private PlayerVariable GetSwitch(Guid id)
        {
            foreach (var s in Variables)
            {
                if (s.VariableId == id)
                {
                    return s;
                }
            }

            return null;
        }

        public bool GetSwitchValue(Guid id)
        {
            var s = GetSwitch(id);
            if (s == null)
            {
                return false;
            }

            return s.Value.Boolean;
        }

        public void SetSwitchValue(Guid id, bool value)
        {
            var s = GetSwitch(id);
            var changed = true;
            if (s != null)
            {
                if (s.Value?.Boolean == value)
                {
                    changed = false;
                }
                s.Value.Boolean = value;
            }
            else
            {
                s = new PlayerVariable(id);
                s.Value.Boolean = value;
                Variables.Add(s);
            }

            if (changed)
            {
                RecipeUnlockWatcher.EnqueueNewPlayer(this, id, RecipeTrigger.PlayerVarChange);
                StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", id.ToString());
            }
        }

        public PlayerVariable GetVariable(Guid id, bool createIfNull = false)
        {
            foreach (var v in Variables)
            {
                if (v.VariableId == id)
                {
                    return v;
                }
            }

            if (createIfNull)
            {
                return CreateVariable(id);
            }

            return null;
        }

        private PlayerVariable CreateVariable(Guid id)
        {
            if (PlayerVariableBase.Get(id) == null)
            {
                return null;
            }

            var variable = new PlayerVariable(id);
            Variables.Add(variable);

            return variable;
        }

        public VariableValue GetVariableValue(Guid id)
        {
            var v = GetVariable(id);
            if (v == null)
            {
                v = CreateVariable(id);
            }

            if (v == null)
            {
                return new VariableValue();
            }

            return v.Value;
        }

        public void SetVariableValue(Guid id, long value, bool setRecord)
        {
            var v = GetVariable(id);
            var changed = true;
            if (v != null)
            {
                if (v.Value?.Integer == value)
                {
                    changed = false;
                }
                v.Value.Integer = value;
            }
            else
            {
                v = new PlayerVariable(id);
                v.Value.Integer = value;
                Variables.Add(v);
            }

            if (changed)
            {
                if (setRecord && TrySetRecord(RecordType.PlayerVariable, v.VariableId, v.Value.Integer))
                {
                    PlayerRecord.SendNewVariableRecordMessage(id, true, this);
                }

                RecipeUnlockWatcher.EnqueueNewPlayer(this, id, RecipeTrigger.PlayerVarChange);

                StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", id.ToString());
            }
        }

        public void SetVariableValue(Guid id, string value)
        {
            var v = GetVariable(id);
            var changed = true;
            if (v != null)
            {
                if (v.Value?.String == value)
                {
                    changed = false;
                }
                v.Value.String = value;
            }
            else
            {
                v = new PlayerVariable(id);
                v.Value.String = value;
                Variables.Add(v);
            }

            if (changed)
            {
                StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", id.ToString());
            }
        }

        //Event Processing Methods
        public Event EventExists(MapTileLoc loc)
        {
            if (EventTileLookup.TryGetValue(loc, out Event val)) {
                return val;
            }

            return null;
        }

        public EventPageInstance EventAt(Guid mapId, int x, int y, int z)
        {
            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value != null && evt.Value.PageInstance != null)
                {
                    if (evt.Value.PageInstance.MapId == mapId &&
                        evt.Value.PageInstance.X == x &&
                        evt.Value.PageInstance.Y == y &&
                        evt.Value.PageInstance.Z == z)
                    {
                        return evt.Value.PageInstance;
                    }
                }
            }

            return null;
        }

        public void TryActivateEvent(Guid eventId)
        {
            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.PageInstance != null && evt.Value.PageInstance.Id == eventId)
                {
                    if (evt.Value.PageInstance.Trigger != EventTrigger.ActionButton)
                    {
                        return;
                    }

                    if (!IsEventOneBlockAway(evt.Value))
                    {
                        return;
                    }

                    if (evt.Value.CallStack.Count != 0)
                    {
                        return;
                    }

                    var newStack = new CommandInstance(evt.Value.PageInstance.MyPage);
                    evt.Value.CallStack.Push(newStack);
                    if (!evt.Value.Global)
                    {
                        evt.Value.PageInstance.TurnTowardsPlayer();
                    }
                    else
                    {
                        //Turn the global event opposite of the player
                        switch (Dir)
                        {
                            case 0:
                                evt.Value.PageInstance.GlobalClone.ChangeDir(1);

                                break;
                            case 1:
                                evt.Value.PageInstance.GlobalClone.ChangeDir(0);

                                break;
                            case 2:
                                evt.Value.PageInstance.GlobalClone.ChangeDir(3);

                                break;
                            case 3:
                                evt.Value.PageInstance.GlobalClone.ChangeDir(2);

                                break;
                        }
                    }
                }
            }
        }

        public void RespondToEvent(Guid eventId, int responseId)
        {
            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.PageInstance != null && evt.Value.PageInstance.Id == eventId)
                {
                    if (evt.Value.CallStack.Count <= 0)
                    {
                        return;
                    }

                    var stackInfo = evt.Value.CallStack.Peek();
                    if (stackInfo.WaitingForResponse != CommandInstance.EventResponse.Dialogue)
                    {
                        return;
                    }

                    stackInfo.WaitingForResponse = CommandInstance.EventResponse.None;
                    if (stackInfo.WaitingOnCommand != null &&
                        stackInfo.WaitingOnCommand.Type == EventCommandType.ShowOptions)
                    {
                        // Alex - Try this to fix Zelp crash on event dialog? Perhaps the client was sending a response before the server knew the options?
                        if (responseId <= 0 || responseId - 1 > stackInfo.BranchIds.Length)
                        {
                            return;
                        }

                        var tmpStack = new CommandInstance(stackInfo.Page, stackInfo.BranchIds[responseId - 1]);
                        evt.Value.CallStack.Push(tmpStack);
                    }

                    return;
                }
            }
        }

        public void PictureClosed(Guid eventId)
        {
            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.PageInstance != null && evt.Value.PageInstance.Id == eventId)
                {
                    if (evt.Value.CallStack.Count <= 0)
                    {
                        return;
                    }

                    var stackInfo = evt.Value.CallStack.Peek();
                    if (stackInfo.WaitingForResponse != CommandInstance.EventResponse.Picture)
                    {
                        return;
                    }

                    stackInfo.WaitingForResponse = CommandInstance.EventResponse.None;

                    return;
                }
            }
        }

        public void RespondToEventInput(Guid eventId, int newValue, string newValueString, bool canceled = false)
        {
            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.PageInstance != null && evt.Value.PageInstance.Id == eventId)
                {
                    if (evt.Value.CallStack.Count <= 0)
                    {
                        return;
                    }

                    var stackInfo = evt.Value.CallStack.Peek();
                    if (stackInfo.WaitingForResponse != CommandInstance.EventResponse.Dialogue)
                    {
                        return;
                    }

                    stackInfo.WaitingForResponse = CommandInstance.EventResponse.None;
                    if (stackInfo.WaitingOnCommand != null &&
                        stackInfo.WaitingOnCommand.Type == EventCommandType.InputVariable)
                    {
                        var cmd = (InputVariableCommand)stackInfo.WaitingOnCommand;
                        VariableValue value = null;
                        var type = VariableDataTypes.Boolean;
                        if (cmd.VariableType == VariableTypes.PlayerVariable)
                        {
                            var variable = PlayerVariableBase.Get(cmd.VariableId);
                            if (variable != null)
                            {
                                type = variable.Type;
                            }

                            value = GetVariableValue(cmd.VariableId);
                        }
                        else if (cmd.VariableType == VariableTypes.ServerVariable)
                        {
                            var variable = ServerVariableBase.Get(cmd.VariableId);
                            if (variable != null)
                            {
                                type = variable.Type;
                            }

                            value = ServerVariableBase.Get(cmd.VariableId)?.Value;
                        }

                        if (value == null)
                        {
                            value = new VariableValue();
                        }

                        var success = false;
                        var changed = false;

                        if (!canceled)
                        {
                            switch (type)
                            {
                                case VariableDataTypes.Integer:
                                    if (newValue >= cmd.Minimum && newValue <= cmd.Maximum)
                                    {
                                        if (value.Integer != newValue)
                                        {
                                            changed = true;
                                        }
                                        value.Integer = newValue;
                                        success = true;
                                    }

                                    break;
                                case VariableDataTypes.Number:
                                    if (newValue >= cmd.Minimum && newValue <= cmd.Maximum)
                                    {
                                        if (value.Number != newValue)
                                        {
                                            changed = true;
                                        }
                                        value.Number = newValue;
                                        success = true;
                                    }

                                    break;
                                case VariableDataTypes.String:
                                    if (newValueString.Length >= cmd.Minimum &&
                                        newValueString.Length <= cmd.Maximum)
                                    {
                                        if (value.String != newValueString)
                                        {
                                            changed = true;
                                        }
                                        value.String = newValueString;
                                        success = true;
                                    }

                                    break;
                                case VariableDataTypes.Boolean:
                                    if (value.Boolean != newValue > 0)
                                    {
                                        changed = true;
                                    }
                                    value.Boolean = newValue > 0;
                                    success = true;

                                    break;
                            }
                        }

                        //Reassign variable values in case they didnt already exist and we made them from scratch at the null check above
                        if (cmd.VariableType == VariableTypes.PlayerVariable)
                        {
                            var variable = GetVariable(cmd.VariableId);
                            if (changed)
                            {
                                variable.Value = value;
                                StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", cmd.VariableId.ToString());
                            }
                        }
                        else if (cmd.VariableType == VariableTypes.ServerVariable)
                        {
                            var variable = ServerVariableBase.Get(cmd.VariableId);
                            if (changed)
                            {
                                variable.Value = value;
                                StartCommonEventsWithTriggerForAll(CommonEventTrigger.ServerVariableChange, "", cmd.VariableId.ToString());
                                DbInterface.UpdatedServerVariables.AddOrUpdate(variable.Id, variable, (key, oldValue) => variable);
                            }
                        }

                        var tmpStack = success
                            ? new CommandInstance(stackInfo.Page, stackInfo.BranchIds[0])
                            : new CommandInstance(stackInfo.Page, stackInfo.BranchIds[1]);

                        evt.Value.CallStack.Push(tmpStack);
                    }

                    return;
                }
            }
        }

        static bool IsEventOneBlockAway(Event evt)
        {
            //todo this
            return true;
        }

        public Event FindGlobalEventInstance(EventPageInstance en)
        {
            if (GlobalPageInstanceLookup.TryGetValue(en, out Event val))
            {
                return val;
            }

            return null;
        }

        public void SendEvents()
        {
            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.PageInstance != null)
                {
                    evt.Value.PageInstance.SendToPlayer();
                }
            }
        }

        [NotMapped, JsonIgnore]
        private readonly ConcurrentQueue<StartCommonEventMetadata> _queueStartCommonEvent = new ConcurrentQueue<StartCommonEventMetadata>();

        public class StartCommonEventMetadata
        {
            public string Command { get; }

            public EventBase EventDescriptor { get; }

            public string Parameter { get; }

            public CommonEventTrigger Trigger { get; }

            public long Value { get; }

            public StartCommonEventMetadata(
                string command,
                EventBase eventDescriptor,
                string parameter,
                CommonEventTrigger trigger,
                long value
            )
            {
                Command = command;
                EventDescriptor = eventDescriptor;
                Parameter = parameter;
                Trigger = trigger;
                Value = value;
            }
        }

        public void EnqueueStartCommonEvent(
            EventBase eventDescriptor,
            CommonEventTrigger trigger = CommonEventTrigger.None,
            string command = default,
            string parameter = default,
            long value = -1
        ) 
        {
            _queueStartCommonEvent.Enqueue(new StartCommonEventMetadata(command ?? string.Empty, eventDescriptor, parameter ?? string.Empty, trigger, value));
        }

        public bool UnsafeStartCommonEvent(
            EventBase baseEvent,
            CommonEventTrigger trigger = CommonEventTrigger.None,
            string command = "",
            string param = "",
            long val = -1
        )
        {
            if (baseEvent == null)
            {
                return false;
            }

            if (!baseEvent.CommonEvent && baseEvent.MapId != Guid.Empty)
            {
                return false;
            }

            if (EventBaseIdLookup.ContainsKey(baseEvent.Id) && !baseEvent.CanRunInParallel)
            {
                return false;
            }

            lock (mEventLock)
            {
                mCommonEventLaunches++;
                var commonEventLaunch = mCommonEventLaunches;

                //Use Fake Ids for Common Events Since they are not tied to maps and such
                var evtId = Guid.NewGuid();
                var mapId = Guid.Empty;

                Event newEvent = null;

                //Try to Spawn a PageInstance.. if we can
                for (var i = baseEvent.Pages.Count - 1; i >= 0; i--)
                {
                    if ((trigger == CommonEventTrigger.None || baseEvent.Pages[i].CommonTrigger == trigger) && Conditions.CanSpawnPage(baseEvent.Pages[i], this, null))
                    {
                        if (trigger == CommonEventTrigger.SlashCommand && command.ToLower() != baseEvent.Pages[i].TriggerCommand.ToLower())
                        {
                            continue;
                        }

                        // If a var change event was triggered, but not for the var set for this Common Event, back out of processing
                        var varChangeEvent = (trigger == CommonEventTrigger.PlayerVariableChange || trigger == CommonEventTrigger.InstanceVariableChange || trigger == CommonEventTrigger.ServerVariableChange);
                        if (varChangeEvent && param != baseEvent.Pages[i].TriggerId.ToString())
                        {
                            continue;
                        }

                        if (varChangeEvent)
                        {
                            // A resource's harvestability might have changed - generate fresh info if requested
                            UseCachedHarvestInfo = false;
                        }

                        // If this is a class rank increase event, but not for the correct class type, back out of processing
                        if (trigger == CommonEventTrigger.ClassRankIncreased && param != baseEvent.Pages[i].TriggerId.ToString())
                        {
                            continue;
                        }

                        // If this is a record update, but does not count toward the relevant record item/count
                        if (trigger == CommonEventTrigger.NpcsDefeated || trigger == CommonEventTrigger.ResourcesGathered || trigger == CommonEventTrigger.CraftsCreated)
                        {
                            if (param != baseEvent.Pages[i].TriggerId.ToString() || val != baseEvent.Pages[i].TriggerVal)
                            {
                                continue;
                            }
                        }

                        // If this is a combo update, but not for the right number, back out
                        if (trigger == CommonEventTrigger.ComboReached)
                        {
                            if (val != baseEvent.Pages[i].TriggerVal)
                            {
                                continue;
                            }
                        }


                        newEvent = new Event(evtId, null, this, baseEvent)
                        {
                            MapId = mapId,
                            SpawnX = -1,
                            SpawnY = -1
                        };
                        newEvent.PageInstance = new EventPageInstance(
                            baseEvent, baseEvent.Pages[i], mapId, MapInstanceId, newEvent, this
                        );

                        newEvent.PageIndex = i;

                        if (trigger == CommonEventTrigger.SlashCommand)
                        {
                            //Split params up
                            var prams = param.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            for (var x = 0; x < prams.Length; x++)
                            {
                                newEvent.SetParam("slashParam" + x, prams[x]);
                            }
                        }

                        switch (trigger)
                        {
                            case CommonEventTrigger.None:
                                break;
                            case CommonEventTrigger.Login:
                                break;
                            case CommonEventTrigger.LevelUp:
                                break;
                            case CommonEventTrigger.OnRespawn:
                                break;
                            case CommonEventTrigger.SlashCommand:
                                break;
                            case CommonEventTrigger.Autorun:
                                break;
                            case CommonEventTrigger.PVPKill:
                                //Add victim as a parameter
                                newEvent.SetParam("victim", param);

                                break;
                            case CommonEventTrigger.PVPDeath:
                                //Add killer as a parameter
                                newEvent.SetParam("killer", param);

                                break;
                            case CommonEventTrigger.PlayerInteract:
                                //Interactee as a parameter
                                newEvent.SetParam("triggered", param);

                                break;
                            case CommonEventTrigger.GuildMemberJoined:
                            case CommonEventTrigger.GuildMemberKicked:
                            case CommonEventTrigger.GuildMemberLeft:
                                newEvent.SetParam("member", param);
                                newEvent.SetParam("guild", command);

                                break;
                        }

                        var newStack = new CommandInstance(newEvent.PageInstance.MyPage);
                        newEvent.CallStack.Push(newStack);

                        break;
                    }
                }

                if (newEvent != null)
                {
                    EventLookup.AddOrUpdate(evtId, newEvent, (key, oldValue) => newEvent);
                    EventBaseIdLookup.AddOrUpdate(baseEvent.Id, newEvent, (key, oldvalue) => newEvent);

                    return true;
                }
                return false;
            }
        }

        public override int CanMove(int moveDir, bool moveRouteRequest = false)
        {
            //If crafting or locked by event return blocked
            if (CraftingTableId != Guid.Empty && CraftId != Guid.Empty)
            {
                return -5;
            }

            if (!moveRouteRequest)
            {
                foreach (var evt in EventLookup.ToArray())
                {
                    if (evt.Value.HoldingPlayer)
                    {
                        return -5;
                    }
                }
            }

            if (PlayerDead)
            {
                return -5;
            }

            return base.CanMove(moveDir);
        }

        protected override int IsTileWalkable(MapController map, int x, int y, int z)
        {
            if (map == null)
            {
                return -5;
            }
            
            if (base.IsTileWalkable(map, x, y, z) != -1)
            {
                return -1;
            }

            // Check if our event cache has loaded in yet (we should have as many events as the
            // MapController defines)
            var loadedEventsOnMap = EventLookup.Where((ev) => ev.Value.MapId == map.Id).ToArray();
            if (map.EventIds.Count != loadedEventsOnMap.Length) 
            {
                return -5;
            }

            foreach (var evt in EventLookup.ToArray())
            {
                // If we have events that haven't yet completed their update loop, don't allow movement.
                if (!evt.Value.HasLoadedOnce && evt.Value.MapId == map.Id) 
                {
                    return -5;
                }

                if (evt.Value.PageInstance == null)
                {
                    continue;
                }
                
                var instance = evt.Value.PageInstance;
                if (instance.GlobalClone != null)
                {
                    instance = instance.GlobalClone;
                }

                if (instance.Map == map &&
                    instance.X == x &&
                    instance.Y == y &&
                    instance.Z == z &&
                    !instance.Passable)
                {
                    return (int)EntityTypes.Event;
                }
            }

            return -1;
        }

        public override void Move(int moveDir, Player forPlayer, bool dontUpdate = false, bool correction = false, int faceDirection = -1)
        {
            lock (EntityLock)
            {
                var prevX = X;
                var prevY = Y;
                SetResourceLock(false);

                var oldMap = MapId;
                FaceDirection = faceDirection;
                CombatMode = FaceDirection != -1;
                base.Move(moveDir, forPlayer, dontUpdate, correction, faceDirection);

                // Allows a player-based projectile safety net to cover up the client/server tile movement disconnect
                // (i.e, a player arrives at a tile long before the client shows this fact)
                // t / 2 here represents "allow the player to get half way to the next tile before we allow projectile
                // collisions", latency willing
                if (prevX != X || prevY != Y)
                {
                    TileMovementTime = Timing.Global.Milliseconds + (long)(GetMovementTime() / 2);
                }

                // Check for a warp, if so warp the player.
                var attribute = MapController.Get(MapId).Attributes[X, Y];
                if (attribute != null && attribute.Type == MapAttributes.Warp)
                {
                    var warpAtt = (MapWarpAttribute)attribute;
                    var dir = (byte)Dir;
                    if (warpAtt.Direction != WarpDirection.Retain)
                    {
                        dir = (byte)(warpAtt.Direction - 1);
                    }

                    var instanceType = MapInstanceType.NoChange;
                    if (warpAtt.ChangeInstance)
                    {
                        instanceType = warpAtt.InstanceType;
                    }

                    if (warpAtt.WarpSound != null)
                    {
                        PacketSender.SendPlaySound(this, warpAtt.WarpSound);
                    }

                    NextInstanceLives = warpAtt.SharedLives - 1;
                    NextDungeonId = warpAtt.DungeonId;
                    Warp(warpAtt.MapId, warpAtt.X, warpAtt.Y, dir, false, 0, false, warpAtt.FadeOnWarp, instanceType);
                }

                // Auto-pickup ammo & gold
                var currentProjectile = GetEquippedWeapon()?.Projectile;
                if (currentProjectile != default && currentProjectile.RequiresAmmo)
                {
                    var ammoId = GetProjectileAmmoId(currentProjectile);
                    AutoPickupItem(ammoId, false);
                }
                AutoPickupItem(Guid.Parse(Options.Player.GoldGuid), true);
                // Angry nuts
                AutoPickupItem(Guid.Parse("30a323c0-211d-4fae-bf53-6f045fc726ed"), false); // This SHOULD be a property on ItemBase at this point, but currently lazy

                foreach (var evt in EventLookup.ToArray())
                {
                    if (evt.Value.MapId == MapId)
                    {
                        if (evt.Value.PageInstance != null && evt.Value.PageInstance.MapId == MapId)
                        {
                            var x = evt.Value.PageInstance.GlobalClone?.X ?? evt.Value.PageInstance.X;
                            var y = evt.Value.PageInstance.GlobalClone?.Y ?? evt.Value.PageInstance.Y;
                            var z = evt.Value.PageInstance.GlobalClone?.Z ?? evt.Value.PageInstance.Z;
                            if (x == X && y == Y && z == Z)
                            {
                                HandleEventCollision(evt.Value, -1);
                            }
                        }
                    }
                }

                // Check if the player has moved around a territory
                UpdateTerritoryStatus();

                // If we've changed maps, start relevant events!
                if (oldMap != MapId)
                {
                    AddDeferredEvent(CommonEventTrigger.MapChanged);
                }
            }
        }

        public void AutoPickupItem(Guid itemId, bool onlyIfInInventory)
        {
            if (onlyIfInInventory && CountItems(itemId, true, false) <= 0)
            {
                return;
            }

            if (!MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
            {
                return;
            }

            var tileId = Y * Options.MapWidth + X;
            var mapItems = mapInstance.FindItemsAt(tileId);
            if (mapItems.Count == 0 || !mapInstance.FindItemsAt(tileId).Select(items => items.ItemId).Contains(itemId))
            {
                return;
            }

            var pickupItems = mapInstance.FindItemsAt(tileId).Where(mapItem => mapItem.ItemId == itemId).ToArray();
            if (pickupItems.Length == 0)
            {
                return;
            }

            foreach (var mapItem in pickupItems)
            {
                TryPickupMapItem(mapItem.UniqueId, MapId, tileId);
            }
        }

        public void TryBumpEvent(Guid mapId, Guid eventId)
        {
            if (PlayerDead)
            {
                return;
            }

            foreach (var evt in EventLookup.ToArray())
            {
                if (evt.Value.MapId == mapId)
                {
                    if (evt.Value.PageInstance != null && evt.Value.PageInstance.MapId == mapId && evt.Value.BaseEvent.Id == eventId)
                    {
                        var x = evt.Value.PageInstance.GlobalClone?.X ?? evt.Value.PageInstance.X;
                        var y = evt.Value.PageInstance.GlobalClone?.Y ?? evt.Value.PageInstance.Y;
                        var z = evt.Value.PageInstance.GlobalClone?.Z ?? evt.Value.PageInstance.Z;
                        if (IsOneBlockAway(mapId, x, y, z))
                        {
                            if (evt.Value.PageInstance.Trigger != EventTrigger.PlayerBump)
                            {
                                return;
                            }

                            if (evt.Value.CallStack.Count != 0)
                            {
                                return;
                            }

                            var newStack = new CommandInstance(evt.Value.PageInstance.MyPage);
                            evt.Value.CallStack.Push(newStack);
                        }
                    }
                }
            }
        }

        public void HandleEventCollision(Event evt, int pageNum)
        {
            if (PlayerDead)
            {
                return;
            }

            var eventInstance = evt;
            if (evt.Player == null) //Global
            {
                eventInstance = null;
                foreach (var e in EventLookup.ToArray())
                {
                    if (e.Value?.BaseEvent?.Id == evt?.BaseEvent?.Id)
                    {
                        if (e.Value?.PageInstance?.MyPage == e.Value?.BaseEvent?.Pages[pageNum])
                        {
                            eventInstance = e.Value;

                            break;
                        }
                    }
                }
            }

            if (eventInstance != null)
            {
                if (eventInstance.PageInstance.Trigger != EventTrigger.PlayerCollide && eventInstance.PageInstance.Trigger != EventTrigger.EventCollide)
                {
                    return;
                }

                if (eventInstance.CallStack.Count != 0)
                {
                    return;
                }

                var newStack = new CommandInstance(eventInstance.PageInstance.MyPage);
                eventInstance.CallStack.Push(newStack);
            }
        }

        /// <summary>
        /// Update the cooldown for a specific item.
        /// </summary>
        /// <param name="item">The <see cref="ItemBase"/> to update the cooldown for.</param>
        public void UpdateCooldown(ItemBase item)
        {
            if (item == null)
            {
                return;
            }

            // Are we dealing with a cooldown group?
            if (item.CooldownGroup.Trim().Length > 0)
            {
                // Yes, so handle it!
                UpdateCooldownGroup(GameObjectType.Item, item.CooldownGroup, item.Cooldown, item.IgnoreCooldownReduction);
            }
            else
            {
                // No, handle singular cooldown as normal.

                var cooldownReduction = 1 - (item.IgnoreCooldownReduction ? 0 : GetBonusEffectTotal(EffectType.CooldownReduction) / 100f);
                AssignItemCooldown(item.Id, Timing.Global.MillisecondsUtc + (long)(item.Cooldown * cooldownReduction));
                PacketSender.SendItemCooldown(this, item.Id);
            }
        }

        /// <summary>
        /// Update the cooldown for a specific spell.
        /// </summary>
        /// <param name="item">The <see cref="SpellBase"/> to update the cooldown for.</param>
        public void UpdateCooldown(SpellBase spell)
        {
            if (spell == null)
            {
                return;
            }

            // Are we dealing with a cooldown group?
            if (spell.CooldownGroup.Trim().Length > 0)
            {
                // Yes, so handle it!
                UpdateCooldownGroup(GameObjectType.Spell, spell.CooldownGroup, spell.CooldownDuration, spell.IgnoreCooldownReduction);
            }
            else
            {
                // No, handle singular cooldown as normal.
                var cooldownReduction = 1 - (spell.IgnoreCooldownReduction ? 0 : GetBonusEffectTotal(EffectType.CooldownReduction) / 100f);
                AssignSpellCooldown(spell.Id, Timing.Global.MillisecondsUtc + (long)(spell.CooldownDuration * cooldownReduction));
                PacketSender.SendSpellCooldown(this, spell.Id);
            }
        }

        /// <summary>
        /// Forces an update of the global cooldown.
        /// Does nothing when disabled by configuration.
        /// </summary>
        public void UpdateGlobalCooldown()
        {
            // Are we allowed to execute this code?
            if (!Options.Combat.EnableGlobalCooldowns)
            {
                return;
            }

            // Calculate our global cooldown.
            var cooldownReduction = 1 - GetBonusEffectTotal(EffectType.CooldownReduction) / 100f;
            var cooldown = Timing.Global.MillisecondsUtc + (long)(Options.Combat.GlobalCooldownDuration * cooldownReduction);

            // Go through each item and spell to assign this cooldown.
            // Do not allow this to overwrite things that are still on a cooldown above our new cooldown though, don't want us to lower cooldowns!
            // We do however want to overwrite lower cooldowns than our new one, it is a GLOBAL cooldown after all!
            foreach(var item in ItemBase.Lookup)
            {
                // Skip this item if it is unaffected by global cooldowns.
                if (((ItemBase)item.Value).IgnoreGlobalCooldown)
                {
                    continue;
                }

                if (!ItemCooldowns.ContainsKey(item.Key) || ItemCooldowns[item.Key] < cooldown)
                {
                    AssignItemCooldown(item.Key, cooldown);
                }
            }
            foreach (var spell in SpellBase.Lookup)
            {
                // Skip this item if it is unaffected by global cooldowns.
                if (((SpellBase)spell.Value).IgnoreGlobalCooldown)
                {
                    continue;
                }

                if (!SpellCooldowns.ContainsKey(spell.Key) || SpellCooldowns[spell.Key] < cooldown)
                {
                    AssignSpellCooldown(spell.Key, cooldown);
                }
            }

            // Send these cooldowns to the user!
            PacketSender.SendItemCooldowns(this);
            PacketSender.SendSpellCooldowns(this);
        }

        /// <summary>
        /// Update all cooldowns within the specified cooldown group on a type of object, or all when configured as such.
        /// </summary>
        /// <param name="type">The <see cref="GameObjectType"/> to set trigger the cooldown group for. Currently only accepts Items and Spells</param>
        /// <param name="group">The cooldown group to trigger.</param>
        /// <param name="cooldown">The base cooldown of the object that triggered this cooldown group.</param>
        /// <param name="ignoreCdr">Whether or not this item/spell is set to ignore cdr, in which case the group will ignore it too.</param>
        private void UpdateCooldownGroup(GameObjectType type, string group, int cooldown, bool ignoreCdr)
        {
            // We're only dealing with these two types for now.
            if (type != GameObjectType.Item && type != GameObjectType.Spell)
            {
                return;
            }

            var cooldownReduction = 1 - (ignoreCdr ? 0 : GetBonusEffectTotal(EffectType.CooldownReduction) / 100f);

            // Retrieve a list of all items and/or spells depending on our settings to set the cooldown for.
            var matchingItems = Array.Empty<ItemBase>();
            var matchingSpells = Array.Empty<SpellBase>();
            var itemsUpdated = false;
            var spellsUpdated = false;

            if (type == GameObjectType.Item || Options.Combat.LinkSpellAndItemCooldowns)
            {
                matchingItems = ItemBase.GetCooldownGroup(group);
            }
            if (type == GameObjectType.Spell || Options.Combat.LinkSpellAndItemCooldowns)
            {
                matchingSpells = SpellBase.GetCooldownGroup(group);
            }

            // Set our matched cooldown, should we need to use it.
            var matchedCooldowntime = cooldown;
            if (Options.Combat.MatchGroupCooldownHighest)
            {
                // Get our highest cooldown value from all available options.
                matchedCooldowntime = Math.Max(
                    matchingItems.Length > 0 ? matchingItems.Max(i => i.Cooldown) : 0,
                    matchingSpells.Length > 0 ? matchingSpells.Max(i => i.CooldownDuration) : 0);
            }

            // Set the cooldown for all items matching this cooldown group.
            var baseTime = Timing.Global.MillisecondsUtc;
            if (type == GameObjectType.Item || Options.Combat.LinkSpellAndItemCooldowns)
            {
                foreach (var item in matchingItems)
                {
                    // Do we have to match our cooldown times, or do we use each individual item cooldown?
                    var tempCooldown = Options.Combat.MatchGroupCooldowns ? matchedCooldowntime : item.Cooldown;

                    // Asign it! Assuming our cooldown isn't already going..
                    if (!ItemCooldowns.ContainsKey(item.Id) || ItemCooldowns[item.Id] < Timing.Global.MillisecondsUtc)
                    {
                        AssignItemCooldown(item.Id, baseTime + (long)(tempCooldown * cooldownReduction));
                        itemsUpdated = true;
                    }
                }
            }

            // Set the cooldown for all Spells matching this cooldown group.
            if (type == GameObjectType.Spell || Options.Combat.LinkSpellAndItemCooldowns)
            {
                foreach (var spell in matchingSpells)
                {
                    // Do we have to match our cooldown times, or do we use each individual item cooldown?
                    var tempCooldown = Options.Combat.MatchGroupCooldowns ? matchedCooldowntime : spell.CooldownDuration;

                    // Asign it! Assuming our cooldown isn't already going...
                    if (!SpellCooldowns.ContainsKey(spell.Id) || SpellCooldowns[spell.Id] < Timing.Global.MillisecondsUtc)
                    {
                        AssignSpellCooldown(spell.Id, baseTime + (long)(tempCooldown * cooldownReduction));
                        spellsUpdated = true;
                    }
                }
            }

            // Send all of our updated cooldowns.
            if (itemsUpdated)
            {
                PacketSender.SendItemCooldowns(this);
            }
            if (spellsUpdated)
            {
                PacketSender.SendSpellCooldowns(this);
            }
        }

        /// <summary>
        /// Assign a cooldown time to a specified item.
        /// WARNING: Makes no checks at all to see whether this SHOULD happen!
        /// </summary>
        /// <param name="itemId">The <see cref="ItemBase"/> id to assign the cooldown for.</param>
        /// <param name="cooldownTime">The cooldown time to assign.</param>
        private void AssignItemCooldown(Guid itemId, long cooldownTime)
        {
            // Do we already have a cooldown entry for this item?
            if (ItemCooldowns.ContainsKey(itemId))
            {
                ItemCooldowns[itemId] = cooldownTime;
            }
            else
            {
                ItemCooldowns.TryAdd(itemId, cooldownTime);
            }
        }

        /// <summary>
        /// Assign a cooldown time to a specified spell.
        /// WARNING: Makes no checks at all to see whether this SHOULD happen!
        /// </summary>
        /// <param name="itemId">The <see cref="SpellBase"/> id to assign the cooldown for.</param>
        /// <param name="cooldownTime">The cooldown time to assign.</param>
        private void AssignSpellCooldown(Guid spellId, long cooldownTime)
        {
            // Do we already have a cooldown entry for this item?
            if (SpellCooldowns.ContainsKey(spellId))
            {
                SpellCooldowns[spellId] = cooldownTime;
            }
            else
            {
                SpellCooldowns.TryAdd(spellId, cooldownTime);
            }
        }

        public bool TryChangeName(string newName)
        {
            // Is the name available?
            if (!FieldChecking.IsValidUsername(newName, Strings.Regex.username))
            {
                return false;
            }

            if (PlayerExists(newName))
            {
                return false;
            }

            // Change their name and force save it!
            var oldName = Name;
            Name = newName;
            User?.Save();

            // Log the activity.
            UserActivityHistory.LogActivity(UserId, Id, Client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.NameChange, $"Changing Character name from {oldName} to {newName}");

            // Send our data around!
            PacketSender.SendEntityDataToProximity(this);

            return true;

        }

        public bool HPThresholdHit(int currentHp)
        {
            return currentHp < (int)(GetMaxVital(Vitals.Health) * Options.Combat.HPWarningThreshold);
        }

        public void CheckForHPWarning(int currentHp)
        {
            var belowThreshold = HPThresholdHit(currentHp);
            if (HPWarningSent && !belowThreshold) // clear the warning
            {
                PacketSender.SendGUINotification(Client, GUINotification.LowHP, false);
                HPWarningSent = false;
            } else if (!HPWarningSent && belowThreshold) // send the warning
            {
                PacketSender.SendGUINotification(Client, GUINotification.LowHP, true);
                HPWarningSent = true;
            }
        }

        /// <summary>
        /// Caclulate crit chance based on the player's current affinity
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public int ApplyEffectBonusToValue(int amount, EffectType effect, bool subtractive = false)
        {
            int effectAmt = GetBonusEffectTotal(effect, 0);

            if (effectAmt <= 0) return amount;

            float effectMod = effectAmt / 100f;
            if (subtractive)
            {
                amount -= (int)Math.Round(amount * (1 + effectMod));
            }
            else
            {
                amount = (int)Math.Round(amount * (1 + effectMod));
            }

            return amount;
        }

        /// <summary>
        /// Caclulate crit chance based on the player's current affinity
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public double ApplyEffectBonusToValue(double amount, EffectType effect)
        {
            int effectAmt = GetBonusEffectTotal(effect, 0);

            if (effectAmt <= 0) return amount;

            float effectMod = effectAmt / 100f;
            amount *= (1 + effectMod);

            return amount;
        }

        //TODO: Clean all of this stuff up

        #region Temporary Values

        [NotMapped, JsonIgnore] public bool InGame;

        [NotMapped, JsonIgnore] public Guid LastMapEntered = Guid.Empty;

        [JsonIgnore, NotMapped] public Client Client { get; set; }

        [JsonIgnore, NotMapped] public Client ClientReference { get; set; }

        [JsonIgnore, NotMapped]
        public UserRights Power => Client?.Power ?? UserRights.None;

        [JsonIgnore, NotMapped] private bool mSentMap;

        [JsonIgnore, NotMapped] private int mCommonEventLaunches = 0;

        [JsonIgnore, NotMapped] private object mEventLock = new object();

        [JsonIgnore, NotMapped]
        public ConcurrentDictionary<Guid, Event> EventLookup = new ConcurrentDictionary<Guid, Event>();

        [JsonIgnore, NotMapped]
        public ConcurrentDictionary<MapTileLoc, Event> EventTileLookup = new ConcurrentDictionary<MapTileLoc, Event>();

        [JsonIgnore, NotMapped]
        public ConcurrentDictionary<Guid, Event> EventBaseIdLookup = new ConcurrentDictionary<Guid, Event>();

        [JsonIgnore, NotMapped]
        public ConcurrentDictionary<EventPageInstance, Event> GlobalPageInstanceLookup = new ConcurrentDictionary<EventPageInstance, Event>();

        #endregion

        #region Trading

        [JsonProperty(nameof(Trading))]
        private Guid JsonTradingId => Trading.Counterparty?.Id ?? Guid.Empty;

        [JsonIgnore, NotMapped] public Trading Trading;

        #endregion

        #region Crafting

        [NotMapped, JsonIgnore] public Guid CraftingTableId = Guid.Empty;

        [NotMapped, JsonIgnore] public Guid CraftId = Guid.Empty;

        [NotMapped, JsonIgnore] public int CraftAmount = 0;

        [NotMapped, JsonIgnore] public long CraftTimer = 0;

        [NotMapped, JsonIgnore] public bool IsCrafting => CraftingTableId != Guid.Empty && CraftId != Guid.Empty;

        #endregion

        #region Parties

        private List<Guid> JsonPartyIds => Party.Select(partyMember => partyMember?.Id ?? Guid.Empty).ToList();

        private Guid JsonPartyRequesterId => PartyRequester?.Id ?? Guid.Empty;

        private Dictionary<Guid, long> JsonPartyRequests => PartyRequests.ToDictionary(
            pair => pair.Key?.Id ?? Guid.Empty, pair => pair.Value
        );

        [JsonIgnore, NotMapped] public List<Player> Party = new List<Player>();

        [JsonIgnore, NotMapped] public Player PartyRequester;

        [JsonIgnore, NotMapped] public Dictionary<Player, long> PartyRequests = new Dictionary<Player, long>();

        #region Class Rank
        public void InitClassRanks()
        {
            foreach (var cls in ClassBase.Lookup.Values)
            {
                // If the player doesn't have any info on this class
                if (!ClassInfo.ContainsKey(cls.Id))
                {
                    // Migration - check to see if the player's NPC Guild was previous tracked via player var, and if so, fill in their info
                    if (cls.Id == ClassId && !String.IsNullOrEmpty(GetVariableValue(Guid.Parse(Options.InGuildVarGuid))))
                    {
                        ClassInfo[cls.Id] = GetClassInfoFromPlayerVariables();
                    }
                    else // Otherwise, simply instantiate a new CR instance
                    {

                        ClassInfo[cls.Id] = new PlayerClassStats();
                    }
                }
                else
                {
                    if (ClassInfo[cls.Id].TasksRemaining == -1)
                    {
                        // Check to see if our max class rank has changed on the player since they last played, and if so, update their tasks remaining
                        ClassInfo[cls.Id].TasksRemaining = TasksRemainingForClassRank(ClassInfo[cls.Id].Rank);
                    }
                    if (ClassInfo[cls.Id].AssignmentAvailable || ClassInfo[cls.Id].TasksRemaining == 0)
                    {
                        // Let the good people know they have a special assignment available to them
                        ClassInfo[cls.Id].AssignmentAvailable = true;
                        PacketSender.SendChatMsg(this,
                            Strings.Quests.newspecialassignment.ToString(cls.Name),
                            ChatMessageType.Quest,
                            CustomColors.Quests.Completed);
                    }
                }
            }
        }

        public PlayerClassStats GetClassInfoFromPlayerVariables()
        {
            var classStats = new PlayerClassStats();
            classStats.InGuild = !String.IsNullOrEmpty(GetVariableValue(Guid.Parse(Options.InGuildVarGuid)));
            classStats.Rank = (int)GetVariableValue(Guid.Parse(Options.ClassRankVarGuid));
            classStats.TotalTasksComplete = (int)GetVariableValue(Guid.Parse(Options.TasksCompletedVarGuid));
            classStats.OnTask = (bool)GetVariableValue(Guid.Parse(Options.OnTaskVarGuid));
            classStats.OnSpecialAssignment = (bool)GetVariableValue(Guid.Parse(Options.OnSpecialAssignmentVarGuid));
            classStats.AssignmentAvailable = (bool)GetVariableValue(Guid.Parse(Options.SpecialAssignmentAvailableGuid));
            classStats.TaskCompleted = (bool)GetVariableValue(Guid.Parse(Options.TaskCompletedVarGuid));

            Log.Info($"Player {Name} has had their class stats migrated from player variables. {ClassBase.Get(ClassId).Name} Rank {classStats.Rank}");
            return classStats;
        }

        #endregion

        #endregion

        #region Friends

        private Guid JsonFriendRequesterId => FriendRequester?.Id ?? Guid.Empty;

        private Dictionary<Guid, long> JsonFriendRequests => FriendRequests.ToDictionary(
            pair => pair.Key?.Id ?? Guid.Empty, pair => pair.Value
        );

        [JsonIgnore, NotMapped] public Player FriendRequester;

        [JsonIgnore, NotMapped] public Dictionary<Player, long> FriendRequests = new Dictionary<Player, long>();

        #endregion

        #region Bag/Shops/etc

        [JsonProperty(nameof(InBag))]
        private bool JsonInBag => InBag != null;

        [JsonProperty(nameof(InShop))]
        private bool JsonInShop => InShop != null;

        [JsonIgnore, NotMapped] public Bag InBag;

        [JsonIgnore, NotMapped] public ShopBase InShop;

        [NotMapped] public bool InBank => BankInterface != null;

        [NotMapped][JsonIgnore] public BankInterface BankInterface;

        #endregion

        #region Quest Boards
        [NotMapped, JsonIgnore] public Guid QuestBoardId = Guid.Empty;
        #endregion

        #region Item Cooldowns

        [JsonIgnore, Column("ItemCooldowns")]
        public string ItemCooldownsJson
        {
            get => JsonConvert.SerializeObject(ItemCooldowns);
            set => ItemCooldowns = JsonConvert.DeserializeObject<ConcurrentDictionary<Guid, long>>(value ?? "{}");
        }

        [JsonIgnore] public ConcurrentDictionary<Guid, long> ItemCooldowns = new ConcurrentDictionary<Guid, long>();

        #endregion
    }


    public partial class Player : AttackingEntity
    {
        [JsonIgnore]
        public virtual List<PlayerRecord> PlayerRecords { get; set; } = new List<PlayerRecord>();

        [JsonIgnore]
        public virtual List<MapExploredInstance> MapsExplored { get; set; } = new List<MapExploredInstance>();

        [NotMapped, JsonIgnore]
        public bool CloseLeaderboard { get; set; }

        [NotMapped, JsonIgnore]
        public string LastQuestCompleted { get; set; }

        [NotMapped, JsonIgnore]
        public bool ClientAwaitingFadeCompletion { get; set; }

        [NotMapped, JsonIgnore]
        public bool FadeWarp { get; set; }

        // For changing spawn points
        [Column("RespawnOverride")]
        [JsonProperty]
        public Guid RespawnOverrideMapId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public MapBase RespawnOverrideMap
        {
            get => MapBase.Get(RespawnOverrideMapId);
            set => RespawnOverrideMapId = value?.Id ?? Guid.Empty;
        }
        public int RespawnOverrideX { get; set; }
        public int RespawnOverrideY { get; set; }
        public int RespawnOverrideDir { get; set; }

        [NotMapped, JsonIgnore]
        public List<Item> ItemsLost { get; set; } = new List<Item>();

        // For respawning if killed in arena
        [Column("ArenaRespawn")]
        [JsonProperty]
        public Guid ArenaRespawnMapId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public MapBase ArenaRespawnMap
        {
            get => MapBase.Get(ArenaRespawnMapId);
            set => ArenaRespawnMapId = value?.Id ?? Guid.Empty;
        }
        public int ArenaRespawnX { get; set; }
        public int ArenaRespawnY { get; set; }
        public int ArenaRespawnDir { get; set; }

        public bool PlayerDead { get; set; }

        [NotMapped, JsonIgnore]
        public List<Item> CurrentLoot {
            get 
            {
                return LootRolls?.Find(roll => roll.EventId == LootEventId)?.Loot ?? null;
            }
            set
            {
                var currentRoll = LootRolls?.Find(roll => roll.EventId == LootEventId);

                // Couldn't find - make a new roll instance
                if (currentRoll == default)
                {
                    return;
                }

                currentRoll.Loot = value;
            }
        } 

        [NotMapped, JsonIgnore]
        public Guid LootEventId { get; set; }

        [JsonIgnore]
        public virtual List<LootRollInstance> LootRolls { get; set; } = new List<LootRollInstance>();

        [JsonIgnore]
        public virtual List<LabelInstance> UnlockedLabels { get; set; } = new List<LabelInstance>();

        public bool LabelTutorialDone { get; set; }

        public void MarkMapExplored(Guid mapId)
        {
            lock (EntityLock)
            {
                var thisMap = MapsExplored
                    .Where(mapExplore => mapExplore.MapId == mapId)
                    .ToArray();

                if (thisMap.Length > 0)
                {
                    return;
                }

                MapsExplored.Add(new MapExploredInstance(Id, mapId));

                PacketSender.SendMapsExploredPacketTo(this);
            }
        }

        #region Rolling Loot
        public void OpenLootRoll(Guid eventId, List<LootRoll> lootRolls, int totalScraps = 0)
        {
            LootEventId = eventId;
            GenerateCurrentLoot(eventId, lootRolls, totalScraps: totalScraps);
        }

        /// <summary>
        /// Creates or fetches a list of items that an event called for rolling for this player.
        /// </summary>
        /// <param name="eventId">The event that is requesting a roll of loot tables</param>
        /// <param name="lootRolls">The rolls the event wants to make</param>
        /// <returns></returns>
        private void GenerateCurrentLoot(Guid eventId, List<LootRoll> lootRolls, int totalScraps = 0)
        {
            // If we don't have a loot reference for the current event id...
            if (CurrentLoot == null && LootEventId != default)
            {
                // Make one
                var newRoll = new LootRollInstance(this, eventId, lootRolls, totalScraps: totalScraps);
                LootRolls.Add(newRoll);
                return;
            }

            // If the current reference is out of loot
            if (CurrentLoot.Count <= 0)
            {
                var reroll = LootRollInstance.GenerateLootFor(this, lootRolls, totalScraps: totalScraps);
                CurrentLoot = reroll;
            }
        }

        public void ClearLootIfDone()
        {
            if (CurrentLoot?.Count <= 0)
            {
                LootEventId = default;
            }
        }
        #endregion

        #region Player Records
        struct RecordEvent
        {
            public CommonEventTrigger Trigger;
            public long Amount;
            public Guid RecordId;

            public RecordEvent(CommonEventTrigger trigger, long amount, Guid recordId)
            {
                Trigger = trigger;
                Amount = amount;
                RecordId = recordId;
            }
        }

        public long IncrementRecord(RecordType type, Guid recordId, bool instantSave = false, int amount = 1)
        {
            PlayerRecord matchingRecord;
            long recordAmt = 0;

            var test = PlayerRecords.FindAll(rec => rec.Teammates?.Count > 0);

            lock (EntityLock)
            {
                matchingRecord = PlayerRecords.Find(record => record.Type == type && record.RecordId == recordId);
                if (matchingRecord != null)
                {
                    matchingRecord.Amount += amount;
                    recordAmt = matchingRecord.Amount;
                }
                else
                {
                    PlayerRecord newRecord = new PlayerRecord(Id, type, recordId, amount);
                    PlayerRecords.Add(newRecord);
                    recordAmt = newRecord.Amount;
                }

                // Search for relevant common events and fire them
                CommonEventTrigger evtTrigger = CommonEventTrigger.NpcsDefeated;
                switch (type)
                {
                    case RecordType.NpcKilled:
                        evtTrigger = CommonEventTrigger.NpcsDefeated;
                        RecipeUnlockWatcher.EnqueueNewPlayer(this, recordId, RecipeTrigger.EnemyKilled);
                        break;
                    case RecordType.ItemCrafted:
                        evtTrigger = CommonEventTrigger.CraftsCreated;
                        RecipeUnlockWatcher.EnqueueNewPlayer(this, recordId, RecipeTrigger.CraftCrafted);
                        break;
                    case RecordType.ResourceGathered:
                        evtTrigger = CommonEventTrigger.ResourcesGathered;
                        RecipeUnlockWatcher.EnqueueNewPlayer(this, recordId, RecipeTrigger.ResourceHarvested);
                        break;
                    default:
                        evtTrigger = CommonEventTrigger.NpcsDefeated;
                        break;
                }
                AddDeferredEvent(evtTrigger, param: recordId.ToString(), value: recordAmt);
            }
            if (matchingRecord != null && instantSave)
            {
                DbInterface.Pool.QueueWorkItem(matchingRecord.SaveToContext);
            }

            return recordAmt;
        }

        /// <summary>
        /// Sets a new record, if there is one, for the given type and teammembers
        /// </summary>
        /// <param name="type">The type of record</param>
        /// <param name="recordId">The ID that the record cares about - player var ID, NPC ID, etc</param>
        /// <param name="amount">The number we're trying to update to</param>
        /// <param name="teammates">Any teammates involved with setting of the record</param>
        /// <returns></returns>
        public bool TrySetRecord(RecordType type, Guid recordId, long amount, List<Player> teammates = null, bool instantSave = false, RecordScoring scoreType = RecordScoring.High)
        {
            PlayerRecord matchingRecord;
            var soloOnly = false;
            lock (EntityLock)
            {
                long recordAmt = 0;

                // If this is a player var record, but the var doesn't exist or isn't recordable, quit
                if (type == RecordType.PlayerVariable)
                {
                    var playerVar = PlayerVariableBase.Get(recordId);
                    if (playerVar == null)
                    {
                        return false;
                    }

                    if (!playerVar.Recordable)
                    {
                        return false;
                    }

                    if (playerVar.RecordLow)
                    {
                        scoreType = RecordScoring.Low;
                    }

                    soloOnly = playerVar.SoloRecordOnly;

                    // Short term fix to prevent mods/admins from setting hot times for dungeons
                    if (!Options.Instance.RecordOpts.EnableModVariableRecords)
                    {
                        if (Power != UserRights.None || (Party.Count > 0 && !Party.All(p => p.Power == UserRights.None)))
                        {
                            PacketSender.SendChatMsg(this, Strings.Records.VoidRecord, ChatMessageType.Notice, CustomColors.General.GeneralMuted);
                            return false;
                        }
                    }
                }

                // If this is a "team" record, find the record, if any, that contains the same team
                if (!soloOnly && teammates != null && teammates.Count > 0)
                {
                    var teamRecords = new List<PlayerRecord>();
                    foreach(var teammate in teammates)
                    {
                        teamRecords.AddRange(teammate.PlayerRecords);
                    }

                    matchingRecord = teamRecords.Find(record => record.Type == type && record.RecordId == recordId &&
                        teammates.All(t => record.Teammates.Select(tm => tm.PlayerId).Contains(t.Id)));
                }
                // Solo record
                else
                {
                    matchingRecord = PlayerRecords.Find(record => record.Type == type && record.RecordId == recordId && record.Teammates.Count == 0);
                }
                // We couldn't find a record that satisfies our needs, so create a new one
                if (matchingRecord == null)
                {
                    matchingRecord = new PlayerRecord(Id, type, recordId, amount, scoreType);

                    // Team record? If so, add the team mates
                    if (!soloOnly && teammates != null && teammates.Count > 0)
                    {
                        foreach (var member in teammates)
                        {
                            matchingRecord.Teammates.Add(new RecordTeammateInstance(matchingRecord.Id, member.Id));
                        }
                    }
                    PlayerRecords.Add(matchingRecord);
                }
                else if (scoreType == RecordScoring.High && matchingRecord.Amount >= amount ||
                    scoreType == RecordScoring.Low && matchingRecord.Amount <= amount)
                {
                    // Our record didn't improve
                    return false;
                }
                else
                {
                    matchingRecord.Amount = amount;
                }

                // Search for relevant common events and fire them
                CommonEventTrigger evtTrigger = CommonEventTrigger.NpcsDefeated;
                switch (type)
                {
                    case RecordType.NpcKilled:
                        evtTrigger = CommonEventTrigger.NpcsDefeated;
                        break;
                    case RecordType.ItemCrafted:
                        evtTrigger = CommonEventTrigger.CraftsCreated;
                        break;
                    case RecordType.ResourceGathered:
                        evtTrigger = CommonEventTrigger.ResourcesGathered;
                        break;
                    default:
                        evtTrigger = CommonEventTrigger.NpcsDefeated;
                        break;
                }
                AddDeferredEvent(evtTrigger, "", recordId.ToString(), recordAmt);
            }
            if (matchingRecord != null && instantSave)
            {
                DbInterface.Pool.QueueWorkItem(matchingRecord.SaveToContext);
            }
            return true;
        }

        public void SendRecordUpdate(string message)
        {
            PacketSender.SendChatMsg(this, message, ChatMessageType.Experience);
        }

        public void DeleteRecord(RecordType type, Guid recordId)
        {
            List<PlayerRecord> matchingRecords;
            matchingRecords = PlayerRecords.FindAll(record => record.Type == type && record.RecordId == recordId && record.Teammates.Count == 0);

            if (matchingRecords.Count == 0)
            {
                // Record doesn't exist
                return;
            }
            foreach(var record in matchingRecords)
            {
                PlayerRecord.Remove(record, this);
            }
        }
        #endregion


        #region inspiration
        public void GiveInspiredExperience(long amount)
        {
            if (InspirationTime > Timing.Global.MillisecondsUtc && amount > 0)
            {
                GiveExperience(amount);
                PacketSender.SendActionMsg(this, Strings.Combat.inspiredexp.ToString(amount), CustomColors.Combat.LevelUp);
            }
        }

        public void SendInspirationUpdateText(long seconds)
        {
            var endTimeStamp = (InspirationTime - Timing.Global.MillisecondsUtc) / 1000 / 60;
            if (seconds >= 60)
            {
                var minutes = seconds / 60;
                PacketSender.SendChatMsg(this, Strings.Combat.inspirationgainedminutes.ToString(minutes, endTimeStamp), ChatMessageType.Combat, CustomColors.Combat.LevelUp, sendToast: true);
            }
            else if (seconds > 0)
            {
                PacketSender.SendChatMsg(this, Strings.Combat.inspirationgained.ToString(seconds, endTimeStamp), ChatMessageType.Combat, CustomColors.Combat.LevelUp, sendToast: true);
            }
            else
            {
                PacketSender.SendChatMsg(this, Strings.Combat.stillinspired.ToString(endTimeStamp), ChatMessageType.Combat, CustomColors.Combat.LevelUp, sendToast: true);
            }

            SetInspirationStatus();
        }
        #endregion

        #region Timers
        private void LoadTimers()
        {
            // First, tell the TimerProcessor to begin processing this player's timers
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                var timers = context.Timers;

                foreach (var timer in timers.ToArray().Where(t => t.OwnerId == Id && t.Descriptor.OwnerType == TimerOwnerType.Player))
                {
                    // Check if the timer is already being processed - ignore it
                    if (TimerProcessor.ActiveTimers.Contains(timer))
                    {
                        continue;
                    }

                    var descriptor = timer.Descriptor;
                    var now = Timing.Global.MillisecondsUtc;

                    if (descriptor.LogoutBehavior == TimerLogoutBehavior.Pause)
                    {
                        timer.ExpiryTime = now + (descriptor.TimeLimitSeconds - timer.PausedTime);
                    }

                    // Add the timer back to the processing list
                    TimerProcessor.ActiveTimers.Add(timer);
                }

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }

            HashSet<TimerInstance> relevantTimers = new HashSet<TimerInstance>();

            // Get any instance timers that affect this player and send them to their client, if the timer is visible
            foreach (var timer in TimerProcessor.ActiveTimers.ToArray().Where(t => !t.Descriptor.Hidden && t.AffectsPlayer(this)))
            {
                timer.SendTimerPacketTo(this);
            }
        }

        /// <summary>
        /// Removes all timers from the <see cref="TimerProcessor.ActiveTimers"/> processing list that
        /// have this player as their owner ID. Also updates those timers in the DB so that, when they
        /// are refreshed, they are refreshed properly.
        /// </summary>
        private void LogoutPlayerTimers()
        {
            var now = Timing.Global.MillisecondsUtc;
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                foreach (var timer in TimerProcessor.ActiveTimers.Where(t => t.OwnerId == Id && t.Descriptor.OwnerType == TimerOwnerType.Player).ToArray())
                {
                    var descriptor = timer.Descriptor;
                    TimerProcessor.ActiveTimers.Remove(timer); // Remove from processing queue, not from DB

                    switch (descriptor.LogoutBehavior)
                    {
                        case TimerLogoutBehavior.Pause:
                            // Store how much time the timer has until its next expiry, so we can re-populate it on login
                            timer.PausedTime = timer.ElapsedTime;

                            break;
                        case TimerLogoutBehavior.Continue:
                            // Intentionally blank - leave as is, and it'll be processed when the player returns
                            break;
                        case TimerLogoutBehavior.CancelOnLogin:
                            timer.ExpiryTime = TimerConstants.TimerAborted; // Flags timer as aborted, so we know how to handle it when next processed
                            break;
                        default:
                            throw new NotImplementedException("Player timer has invalid logout behavior");
                    }
                    context.Timers.Update(timer);
                }
                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
        #endregion

        public override void DropItems(Entity killer, bool sendUpdate = true)
        {
            // Drop items
            var itemsLostString = new List<string>();
            ItemsLost = new List<Item>();

            if (Map.ZoneType != MapZones.Normal)
            {
                return; // Only drop items in PVP
            }

            for (var n = 0; n < Items.Count; n++)
            {
                if (Items[n] == null)
                {
                    continue;
                }

                // Don't mess with the actual object.
                var item = Items[n].Clone();

                var itemBase = ItemBase.Get(item.ItemId);
                if (itemBase == null)
                {
                    continue;
                }

                //Don't lose non-droppable items
                if (!itemBase.CanDrop)
                {
                    continue;
                }

                var playerKiller = killer as Player;

                Guid lootOwner = Guid.Empty;
                // Player has a 50/50 shot of keeping their item
                if (Randomization.Next(1, 101) <= 50 && itemBase.Id.ToString() != Options.Combat.BloodshedItemId)
                {
                    continue;
                }

                // It's a player, try and set ownership to the player that killed them.. If it was a player.
                // Otherwise set to self, so they can come and reclaim their items.
                lootOwner = playerKiller?.Id ?? Id;

                // Spawn the actual item!
                if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
                {
                    instance.SpawnItem(X, Y, item, item.Quantity, lootOwner, sendUpdate, ItemSpawnType.PlayerDeath);
                }

                // Remove the item from inventory if a player.
                // Additional conditional here to ensure we always drop bloodshed regardless of destruction reqs
                var taken = TryTakeItem(Items[n], item.Quantity, forceTake: itemBase.Id.ToString() == Options.Combat.BloodshedItemId);
                if (taken)
                {
                    ItemsLost.Add(item);
                    if (item.Quantity > 1)
                    {
                        var trimmedName = item.ItemName.TrimEnd('s');
                        itemsLostString.Add($"{item.Quantity} {trimmedName}s");
                    }
                    else
                    {
                        itemsLostString.Add(item.ItemName);
                    }
                }
            }

            if (itemsLostString.Count > 0)
            {
                PacketSender.SendChatMsg(this, Strings.Player.ItemsLost.ToString(string.Join(", ", itemsLostString.ToArray())), ChatMessageType.Inventory, CustomColors.General.GeneralWarning);
            }
        }

        public double GetLuckModifier()
        {
            return 1 + GetBonusEffectTotal(EffectType.Luck) / 100f;
        }

        #region Labels
        [NotMapped, JsonIgnore]
        private List<PlayerLabelPacket> CachedLabelPackets { get; set; }
        
        [NotMapped, JsonIgnore]
        private bool UseLabelCache { get; set; }

        public void SetLabelTo(Guid labelDescriptorId)
        {
            var prevLabelId = SelectedLabelId;
            SelectedLabelId = labelDescriptorId;
            UseLabelCache = SelectedLabelId == prevLabelId;

            // Process a "remove label" request
            if (labelDescriptorId == Guid.Empty)
            {
                HeaderLabel = new Label(string.Empty, Color.White);
                FooterLabel = new Label(string.Empty, Color.White);
                return;
            }

            var label = UnlockedLabels.Find(lbl => lbl.DescriptorId == labelDescriptorId);
            if (label == default)
            {
                return;
            }

            var descriptor = label.Descriptor;
            if (descriptor == default)
            {
                Log.Debug($"Could not get descriptor for {labelDescriptorId} and thus could not set label properties for player {Name}");
                HeaderLabel = new Label(string.Empty, Color.White);
                FooterLabel = new Label(string.Empty, Color.White);
                return;
            }

            var color = descriptor.Color;
            if (descriptor.MatchNameColor)
            {
                color = Color.Transparent;
            }

            if (descriptor.Position == LabelPosition.Header) // Header
            {
                HeaderLabel = new Label(descriptor.DisplayName, color);
                FooterLabel = new Label(string.Empty, Color.White);
            }
            else if (descriptor.Position == LabelPosition.Footer) // Footer
            {
                FooterLabel = new Label(descriptor.DisplayName, color);
                HeaderLabel = new Label(string.Empty, Color.White);
            }

            label.IsNew = false;
        }

        public void ChangeLabelUnlockStatus(Guid labelId, UnlockLabelCommand.LabelUnlockStatus status)
        {
            var descriptor = LabelDescriptor.Get(labelId);
            var all = LabelDescriptor.GetNameList();
            if (descriptor == default)
            {
                Log.Debug($"Could not find label descriptor for ID: {labelId} for player {Name}");
                return;
            }

            if (status == UnlockLabelCommand.LabelUnlockStatus.Unlock)
            {
                var label = UnlockedLabels.Find(lbl => lbl.DescriptorId == labelId);
                if (label != default)
                {
                    // Label is already unlocked
                    return;
                }

                UnlockedLabels.Add(new LabelInstance(Id, labelId));
                PacketSender.SendChatMsg(this, Strings.Labels.LabelUnlocked.ToString(descriptor?.DisplayName), ChatMessageType.Notice, CustomColors.General.GeneralCompleted, sendToast: true);

                UseLabelCache = false;
                return;
            }
            else if (status == UnlockLabelCommand.LabelUnlockStatus.Remove)
            {
                var preCount = UnlockedLabels.Count;
                UnlockedLabels.RemoveAll(label => label.DescriptorId == labelId);
                if (preCount > UnlockedLabels.Count)
                {
                    DbRemoveLabel(Id, labelId);
                    PacketSender.SendChatMsg(this, Strings.Labels.LabelRemoved.ToString(descriptor?.DisplayName), ChatMessageType.Notice, CustomColors.General.GeneralDisabled, sendToast: true);
                }

                UseLabelCache = false;
                return;
            }

            throw new NotImplementedException("ChangeLabelUnlockStatus did not contain a valid label unlock status");
        }

        public List<PlayerLabelPacket> GetUnlockedLabels()
        {
            if (UseLabelCache && CachedLabelPackets != null)
            {
                return CachedLabelPackets;
            }

            CachedLabelPackets = UnlockedLabels.Select(lbl => lbl.Packetize(SelectedLabelId)).ToList();
            UseLabelCache = true;
            return CachedLabelPackets;
        }
        #endregion

        public void AcceptRespawn()
        {
            Reset();
            Respawn();
            SendPlayerDeathStatus();
        }

        public void SendPlayerDeathStatus()
        {
            PacketSender.SendPlayerDeathInfoOf(this);
        }

        public override void Reset()
        {
            PlayerDead = false;
            base.Reset();
        }

        private DeathType GetDeathType(long expLoss)
        {
            if (ItemsLost.Count > 0)
            {
                return DeathType.PvP;
            }
            if (expLoss > 0)
            {
                return DeathType.PvE;
            }
            if (InstanceType == MapInstanceType.Shared)
            {
                return DeathType.Dungeon;
            }
            return DeathType.Safe;
        }

        public override bool IsPassable()
        {
            return base.IsPassable() || PlayerDead;
        }

        private bool WeaponIsSpellcastFocus()
        {
            var equippedWeapon = GetEquippedWeapon();
            return equippedWeapon != null && equippedWeapon.ReplaceCastingComponents;
        }

        private bool HasCastingComponents(List<SpellCastingComponent> castingComponents)
        {
            var equippedWeapon = GetEquippedWeapon();
            if (WeaponIsSpellcastFocus())
            {
                return true;
            }

            if (castingComponents.Count <= 0)
            {
                return true;
            }

            List<string> missingComponents = new List<string>();
            foreach (var component in castingComponents)
            {
                if (ItemNotInInventory(component.ItemId, component.Quantity))
                {
                    missingComponents.Add(ItemBase.GetName(component.ItemId));
                }
            }

            if (missingComponents.Count > 0)
            {
                var sb = new StringBuilder();
                var idx = 0;
                foreach (var componentName in missingComponents)
                {
                    var display = componentName;
                    if (componentName.Last() == 's')
                    {
                        display = componentName.TrimEnd('s');
                    }

                    if (idx == 0)
                    {
                        sb.Append($"{display}s");
                    }
                    else if (idx == 1 && idx == missingComponents.Count - 1)
                    {
                        sb.Append($" nor {display}s");
                    }
                    else if (idx < missingComponents.Count - 1)
                    {
                        sb.Append($", {display}s");
                    }
                    else
                    {
                        sb.Append($", nor {componentName}s");
                    }
                    idx++;
                }

                PacketSender.SendChatMsg(
                        this, Strings.Player.NotEnoughComponents.ToString(sb.ToString()),
                        ChatMessageType.Inventory,
                        CustomColors.Alerts.Error
                    );

                return false;
            }

            return true;
        }

        public bool ItemNotInInventory(Guid itemId, int quantity)
        {
            if (itemId == Guid.Empty)
            {
                return false;
            }
            return FindInventoryItemSlot(itemId, quantity) == null && FindBagSlotsInItemSlots(itemId, quantity).Count == 0;
        }

        public bool HasProjectileAmmo(ProjectileBase projectile)
        {
            var ammoId = GetProjectileAmmoId(projectile);

            if (ItemNotInInventory(ammoId, projectile.AmmoRequired))
            {
                PacketSender.SendChatMsg(
                    this, Strings.Items.notenough.ToString(ItemBase.GetName(ammoId)),
                    ChatMessageType.Inventory,
                    CustomColors.Alerts.Error
                );
                return false;
            }

            return true;
        }

        public Guid GetProjectileAmmoId(ProjectileBase projectile)
        {
            var ammoId = projectile.AmmoItemId;
            if (projectile.UseAmmoOverride && 
                TryGetEquippedItem(Options.WeaponIndex, out var weapon) && 
                weapon.Descriptor != null && 
                weapon.Descriptor.AmmoOverrideId != Guid.Empty)
            {
                ammoId = weapon.Descriptor.AmmoOverrideId;
            }

            return ammoId;
        }

        public bool TryConsumeProjectileAmmo(ProjectileBase projectile)
        {
            if (!projectile.RequiresAmmo)
            {
                return true;
            }

            // Don't take if don't have
            if (!HasProjectileAmmo(projectile))
            {
                return false;
            }

            // If AmmoSaver procs, don't consume ammo but say that you did.
            if (MathHelper.LuckRoll((float)GetBonusEffectTotal(EffectType.AmmoSaver)))
            {
                return true;
            }

            var ammoId = GetProjectileAmmoId(projectile);
            return TryTakeItem(ammoId, projectile.AmmoRequired);
        }

        public bool TryConsumeCastingComponents(SpellBase spell)
        {
            if (WeaponIsSpellcastFocus())
            {
                return true;
            }

            // Don't take if don't have
            if (!HasCastingComponents(spell.CastingComponents))
            {
                return false;
            }

            foreach (var component in spell.CastingComponents)
            {
                if (!TryTakeItem(component.ItemId, component.Quantity))
                {
                    PacketSender.SendChatMsg(
                        this, Strings.Items.notenough.ToString(ItemBase.GetName(component.ItemId)),
                        ChatMessageType.Inventory,
                        CustomColors.Alerts.Error
                    );

                    return false;
                }
            }

            return true;
        }

        public override bool IsDead()
        {
            return Dead || PlayerDead;
        }
    }

    public partial class Player : AttackingEntity
    {
        [JsonIgnore, NotMapped]
        public bool SilenceToasts = false;

        // Set to true so that accounts created after migration never backfill
        public bool CraftingDataBackfilled { get; set; } = true;

        [JsonIgnore, NotMapped]
        public bool UseCachedHarvestInfo = false;

        [JsonIgnore, NotMapped]
        public Dictionary<int, ResourceInfoPackets> CachedHarvestInfo = new Dictionary<int, ResourceInfoPackets>();

        [NotMapped]
        public int[] VitalPointAllocations { get; set; } = new int[(int)Vitals.VitalCount];

        [JsonIgnore, Column(nameof(VitalPointAllocations))]
        public string VitalPointsJson
        {
            get => DatabaseUtils.SaveIntArray(VitalPointAllocations, (int)Vitals.VitalCount);
            set => VitalPointAllocations = DatabaseUtils.LoadIntArray(value, (int)Vitals.VitalCount);
        }

        public bool TryPickupMapItem(Guid uniqueId, Guid mapId, int tileIndex)
        {
            if (!MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var mapInstance))
            {
                return false;
            }
            var map = MapController.Get(MapId);

            // Is our user within range of the item they are trying to pick up?
            if (GetDistanceTo(map, tileIndex % Options.MapWidth, (int)Math.Floor(tileIndex / (float)Options.MapWidth)) > Options.Loot.MaximumLootWindowDistance)
            {
                return false;
            }

            var giveItems = new Dictionary<MapController, List<MapItem>>();
            // Are we trying to pick up everything on this location or one specific item?
            if (uniqueId == Guid.Empty)
            {
                // GET IT ALL! BE GREEDY!
                foreach (var itemMap in map.FindSurroundingTiles(new Point(X, Y), Options.Loot.MaximumLootWindowDistance))
                {
                    var tempMap = itemMap.Key;
                    if (tempMap.TryGetInstance(MapInstanceId, out var tempMapInstance))
                    {
                        if (!giveItems.ContainsKey(itemMap.Key))
                        {
                            giveItems.Add(tempMap, new List<MapItem>());
                        }

                        foreach (var itemLoc in itemMap.Value)
                        {
                            giveItems[tempMap].AddRange(tempMapInstance.FindItemsAt(itemLoc));
                        }
                    }
                }
            }
            else
            {
                // One specific item.
                giveItems.Add(map, new List<MapItem>() { mapInstance.FindItem(uniqueId) });
            }

            // Go through each item we're trying to give our player and see if we can do so.
            foreach (var itemMap in giveItems)
            {
                var tempMap = itemMap.Key;
                if (tempMap.TryGetInstance(MapInstanceId, out var tmpInstance))
                {
                    var toRemove = new List<MapItem>();
                    foreach (var mapItem in itemMap.Value)
                    {
                        if (mapItem == null)
                        {
                            continue;
                        }

                        var canTake = false;
                        // Can we actually take this item?
                        if (mapItem.Owner == Guid.Empty || Timing.Global.Milliseconds > mapItem.OwnershipTime)
                        {
                            // The ownership time has run out, or there's no owner!
                            canTake = true;
                        }
                        else if (mapItem.Owner == Id)
                        {
                            // The current player is the owner.
                            canTake = true;
                        }

                        // Does this item still exist, or did it somehow get picked up before we got there?
                        if (tmpInstance.FindItem(mapItem.UniqueId) == null)
                        {
                            continue;
                        }

                        if (canTake)
                        {
                            //Remove the item from the map now, because otherwise the overflow would just add to the existing quantity
                            tmpInstance.RemoveItem(mapItem);

                            // Try to give the item to our player.
                            if (TryGiveItem(mapItem, ItemHandling.Overflow, false, true, mapItem.X, mapItem.Y))
                            {
                                var item = ItemBase.Get(mapItem.ItemId);
                                if (item != null)
                                {
                                    PacketSender.SendActionMsg(this, item.Name, CustomColors.Items.Rarities[item.Rarity]);
                                }
                            }
                            else
                            {
                                // We couldn't give the player their item, notify them.
                                PacketSender.SendChatMsg(this, Strings.Items.InventoryNoSpace, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                            }
                        }
                    }

                    // Remove all items that were picked up.
                    foreach (var item in toRemove)
                    {
                        tmpInstance.RemoveItem(item);
                    }
                }
            }

            return true;
        }

        [NotMapped, JsonIgnore]
        public override int TierLevel => (ClassInfo?.Count ?? 0) > 0 ?
            ClassInfo?.Values.ToArray()
                    .OrderByDescending(info => info.Rank)
                    .First().Rank ?? 0 :
            0;

        private void ClearCommonEvents()
        {
            while (_queueStartCommonEvent.TryDequeue(out _)) ;
        }

        public void SendFailureChatMsg(string message)
        {
            PacketSender.SendChatMsg(this, message, Enums.ChatMessageType.Error, CustomColors.General.GeneralDisabled);
        }

        public void SendDialogNotice(string message)
        {
            PacketSender.SendEventDialog(this, message, string.Empty, Guid.Empty);
        }

        public struct DeferredEvent
        {
            public CommonEventTrigger Trigger;
            public string Command;
            public string Param;
            public long Value;

            public DeferredEvent(CommonEventTrigger trigger, string command, string param, long value)
            {
                Trigger = trigger;
                Command = command;
                Param = param;
                Value = value;
            }
        }

        public void AddDeferredEvent(CommonEventTrigger trigger, string command = "", string param = "", long value = -1)
        {
            DeferredEventQueue.Enqueue(new DeferredEvent(trigger, command, param, value));
        }

        public ConcurrentQueue<DeferredEvent> DeferredEventQueue = new ConcurrentQueue<DeferredEvent>();

        public override void ResetCooldowns()
        {
            base.ResetCooldowns();
            PacketSender.SendSpellCooldowns(this);
        }

        public bool InvItemIsStackable(int slotIndex)
        {
            if (!TryGetItemAt(slotIndex, out var item))
            {
                return false;
            }

            return item.Descriptor?.IsStackable ?? false;
        }

        public void SendAlert()
        {
            PacketSender.SendPlaySound(this, Options.UIDenySound);
        }

        public bool TryAddToInstanceController()
        {
            if (InstanceProcessor.TryGetInstanceController(MapInstanceId, out var newInstController))
            {
                newInstController.AddPlayer(this);
                return true;
            }

            return false;
        }

        private Guid _nextDungeonId;

        [NotMapped, JsonIgnore]
        public Guid NextDungeonId {
            get => _nextDungeonId;
            set
            {
                _nextDungeonId = value;
            }
        }

        [NotMapped, JsonIgnore]
        public int NextInstanceLives { get; set; }

        [NotMapped, JsonIgnore]
        public bool StatCapActive => InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController)
            && instanceController.InstanceIsDungeon
            && instanceController.DungeonDescriptor.ApplyStatCeiling;

        [NotMapped, JsonIgnore]
        public int CurrentTierCap => InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController) ? instanceController.DungeonDescriptor.StatCeilingTier : 0;

        public bool RecipeIsVisible(RecipeDescriptor recipe)
        {
            if (recipe == null) { return false; }

            if (recipe.MinClassRank > HighestClassRank)
            {
                return false;
            }

            if (!Conditions.MeetsConditionLists(recipe.Requirements, this, null))
            {
                return false;
            }

            return true;
        }

        [NotMapped, JsonIgnore]
        public int HighestClassRank
        {
            get
            {
                if (ClassInfo.Count <= 0)
                {
                    return 0;
                }
                return ClassInfo?.Values?.ToList()
                    ?.OrderByDescending(info => info.Rank)
                    ?.FirstOrDefault()?.Rank ?? 0;
            }
        }

        [NotMapped][JsonIgnore] public bool IsInGuild => Guild != null && Guild.Id != Guid.Empty;

        [NotMapped, JsonIgnore]
        public bool IsInParty => Party != null && Party.Count > 1;

        public void JoinClanWar()
        {
            if (!ClanWarManager.ClanWarActive)
            {
                return;
            }
            ClanWarManager.CurrentWar?.AddPlayer(this);
            LastClanWarId = ClanWarManager.CurrentWarId;
        }

        public void LeaveClanWar(bool fromLogout = false)
        {
            LeaveTerritory();
            ClanWarManager.CurrentWar?.RemovePlayer(this, fromLogout);
        }

        [NotMapped, JsonIgnore]
        public TerritoryInstance LastTerritory { get; set; }

        [NotMapped, JsonIgnore]
        public long TerritoryLeaveTimer { get; set; }

        private void UpdateTerritoryStatus()
        {
            if (Dead || !IsInGuild || InstanceType != MapInstanceType.ClanWar || !MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
            {
                return;
            }

            if (instance.TerritoryTiles.TryGetValue(new BytePoint((byte)X, (byte)Y), out var territoryInstance))
            {
                JoinTerritory(territoryInstance);
            }
            else
            {
                LeaveTerritory();
            }
        }

        [NotMapped, JsonIgnore]
        public object TerritoryLock = new object();

        public bool DefendingTerritory => IsInGuild
            && LastTerritory != null
            && (TerritoryLeaveTimer < 0 || TerritoryLeaveTimer > Timing.Global.MillisecondsUtc)
            && LastTerritory.GuildId == Guild?.Id;

        private void JoinTerritory(TerritoryInstance territory)
        {
            lock (TerritoryLock)
            {
                LastTerritory = territory;
                LastTerritory.AddPlayer(this);
                TerritoryLeaveTimer = -1; // Player is in a territory. This is used for determining attacking/defending points
            }
        }

        public void LeaveTerritory()
        {
            lock (TerritoryLock)
            {
                LastTerritory?.RemovePlayer(this);
                TerritoryLeaveTimer = Timing.Global.MillisecondsUtc + Options.Instance.ClanWar.TerritoryLeaveTimer;
            }
        }

        [NotMapped, JsonIgnore]
        public ClanWarCompletePacket ClanWarComplete { get; set; }

        // TODO we never actually use this as a cache - we always fetch a fresh set
        public void CacheHarvestInfo(int tool)
        {
            var resources = ResourceBase.Lookup.Select(kv => (ResourceBase)kv.Value)
                .Where(resource => resource.Tool == tool && !resource.DoNotRecord)
                .ToArray();

            var nonGroupedResource = resources.Where(resource => string.IsNullOrEmpty(resource.ResourceGroup));

            // Manipulate group resources such that:
            // A) They use a display name if one is given in the group
            // B) They use a non-empty, non-tileset graphic if one is in the group
            var groupedResources = resources
                .GroupBy(resource => new { resource.ResourceGroup })
                .Select((resourceGroupings) =>
                {
                    var resourceToReturn = resourceGroupings.First();
                    if (!string.IsNullOrEmpty(resourceToReturn.DisplayName)
                        && !string.IsNullOrEmpty(resourceToReturn.Initial.Graphic)
                        && !resourceToReturn.Initial.GraphicFromTileset)
                    {
                        return resourceToReturn;
                    }

                    var texture = resourceToReturn.Initial.Graphic;
                    var displayName = resourceToReturn.Name;
                    // Do any of the others have a display name to use?
                    if (string.IsNullOrEmpty(resourceToReturn.DisplayName))
                    {
                        displayName = resourceGroupings.Where(resource => !string.IsNullOrEmpty(resource.DisplayName)).ToList().FirstOrDefault()?.DisplayName ?? resourceToReturn.Name;
                    }
                    if (string.IsNullOrEmpty(texture))
                    {
                        texture = resourceGroupings.Where(resource => !string.IsNullOrEmpty(resource.Initial.Graphic) && !resource.Initial.GraphicFromTileset).ToList().FirstOrDefault()?.Initial.Graphic ?? string.Empty;
                        if (!string.IsNullOrEmpty(texture))
                        {
                            resourceToReturn.Initial.GraphicFromTileset = false;
                        }
                    }

                    resourceToReturn.DisplayName = displayName;
                    resourceToReturn.Initial.Graphic = texture;

                    return resourceToReturn;
                });

            var validResources = new List<ResourceBase>();
            validResources.AddRange(nonGroupedResource);
            validResources.AddRange(groupedResources);
            validResources = validResources
                .GroupBy(resource => new { resource.Name })
                .Select(resourceGroupings => resourceGroupings.First())
                .OrderBy(resource =>
                {
                    var requiredTier = 0;
                    var varName = "";
                    // Axe
                    if (resource.Tool == 0)
                    {
                        varName = "Woodcut Tier";
                    }
                    // Mining
                    else if (resource.Tool == 1)
                    {
                        varName = "Mining Tier";
                    }
                    // Fishing
                    else if (resource.Tool == 3)
                    {
                        varName = "Fishing Tier";
                    }

                    foreach (var requirement in resource.HarvestingRequirements?.Lists.ToArray())
                    {
                        if (requirement.Conditions.Count <= 0)
                        {
                            continue;
                        }
                        foreach (var condition in requirement.Conditions.OfType<VariableIsCondition>().ToArray())
                        {
                            var variable = GetVariable(condition.VariableId);

                            if (variable.VariableName.Equals(varName) && condition.Comparison is IntegerVariableComparison intComparison)
                            {
                                requiredTier = Math.Max(requiredTier, (int)intComparison.Value);
                            }
                        }
                    }

                    return requiredTier;
                })
                .ThenBy(resource => resource.DisplayName ?? resource.Name)
                .ToList();

            var allInfo = new Network.Packets.Server.ResourceInfoPackets();

            foreach (var resource in validResources)
            {
                var dto = new PlayerHarvestDTO(this, resource);
                allInfo.Packets.Add(dto.Packetize());
            }

            CachedHarvestInfo[tool] = allInfo;
            UseCachedHarvestInfo = true;
        }

        private void BackfillHistoricalCraftingData()
        {
            SilenceToasts = true;

            var ingredients = new Dictionary<Guid, int>();

            foreach (var record in PlayerRecords.ToArray().Where(record => record.Type == RecordType.ItemCrafted))
            {
                if (!CraftBase.TryGet(record.RecordId, out var craft))
                {
                    continue;
                }

                foreach (var ingredient in craft.Ingredients)
                {
                    if (ingredients.ContainsKey(ingredient.ItemId))
                    {
                        ingredients[ingredient.ItemId] += ingredient.Quantity;
                    }
                    else
                    {
                        ingredients[ingredient.ItemId] = ingredient.Quantity;
                    }
                }
            }

            foreach (var ingredient in ingredients)
            {
                IncrementRecord(RecordType.ItemUsedInCraft, ingredient.Key, amount: ingredient.Value);
            }

            SilenceToasts = false;
            CraftingDataBackfilled = true;
        }

        public override Tuple<int, int> GetStatBonuses(Stats stat)
        {
            var flatStats = 0;
            var percentageStats = 0;
            var itemBuffs = GetItemStatBuffs(stat);
            var permaBuffs = GetPermabuffStat(stat);
            var challengeBuffs = GetChallengeStatBuffs(stat);

            flatStats += itemBuffs.Item1 + permaBuffs.Item1 + challengeBuffs.Item1;
            percentageStats += itemBuffs.Item2 + permaBuffs.Item2 + challengeBuffs.Item2;

            // Apply current buffs - these are the kinds of buffs that will get capped (items and level stats)
            flatStats = (int)Math.Ceiling(flatStats + (flatStats * (percentageStats / 100f)));

            if (StatCapActive)
            {
                // +1 to tier because of the "None" rarity type throwing some stuff off
                var statIsScaledDown = CombatUtilities.TryCapStatToTier(CurrentTierCap + 1, stat, ref flatStats);

                IsScaledDown = IsScaledDown || statIsScaledDown;
                ScaledTo = CurrentTierCap;
            }
            else
            {
                IsScaledDown = false;
            }

            // Reset so spell/passives can recalc for final value
            percentageStats = 0;
            var passiveBuffs = GetPassiveStatBuffs(stat);

            flatStats += passiveBuffs.Item1;
            percentageStats += passiveBuffs.Item2;
            return new Tuple<int, int>(flatStats, percentageStats);
        }

        public override bool TryDealManaDamageTo(Entity enemy,
            int dmg,
            int dmgScaling,
            double critMultiplier,
            bool vampire,
            out int damage)
        {
            var damageDealt = base.TryDealManaDamageTo(enemy, dmg, dmgScaling, critMultiplier, vampire, out damage);

            if (damageDealt && enemy.ValidForChallenges)
            {
                ChallengeUpdateProcesser.UpdateChallengesOf(new ManaDamageDealtUpdate(this, damage));
            }
            return damageDealt;
        }

        [NotMapped, JsonIgnore]
        public override float StrafeBonus 
        {
            get
            {
                if (!TryGetEquippedItem(Options.WeaponIndex, out var weapon))
                {
                    return 0.0f;
                }

                return weapon?.Descriptor?.StrafeBonus / 100f ?? 0.0f;
            }
        }

        [NotMapped, JsonIgnore]
        public override float BackstepBonus
        {
            get
            {
                if (!TryGetEquippedItem(Options.WeaponIndex, out var weapon))
                {
                    return 0.0f;
                }

                return weapon?.Descriptor?.BackstepBonus / 100f ?? 0.0f;
            }
        }

        [NotMapped, JsonIgnore]
        public override int Speed => InVehicle && VehicleSpeed > 0 ? (int)VehicleSpeed : base.Speed;

        public override bool GetCombatMode()
        {
            return CombatMode;
        }

        public override int GetFaceDirection()
        {
            return FaceDirection;
        }

        public override void NotifyExistingTrap()
        {
            PacketSender.SendChatMsg(this, "Someone else has laid a trap here!", ChatMessageType.Combat, CustomColors.General.GeneralDisabled);
            base.NotifyExistingTrap();
        }

        [NotMapped, JsonIgnore]
        public long TileMovementTime { get; set; }

        public override bool ProjectileSafetyTime => Timing.Global.Milliseconds < TileMovementTime;
    }
}
