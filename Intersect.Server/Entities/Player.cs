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

namespace Intersect.Server.Entities
{

    public partial class Player : Entity
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
        public virtual List<Variable> Variables { get; set; } = new List<Variable>();

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
        public int MaxComboWindow { get; set; } = Options.BaseComboTime;

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

        [NotMapped, JsonIgnore]
        public int InstanceLives { get; set; }

        public bool InVehicle { get; set; } = false;

        public string VehicleSprite { get; set; } = string.Empty;

        public long VehicleSpeed { get; set; } = 0L;

        public long InspirationTime { get; set; } = 0L;

        [JsonIgnore]
        public virtual List<PlayerRecord> PlayerRecords { get; set; } = new List<PlayerRecord>();

        /// <summary>
        /// Used to determine if the player is performing an attack out of stealth
        /// </summary>
        [NotMapped, JsonIgnore]
        public bool StealthAttack = false;

        // Class Rank Vars
        // Contains a mapping of a Class' GUID -> the class info for this player
        [NotMapped, JsonIgnore]
        public Dictionary<Guid, PlayerClassStats> ClassInfo = new Dictionary<Guid, PlayerClassStats>();

        [JsonIgnore]
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
            changes |= SlotHelper.ValidateSlots(Bank, Options.MaxBankSlots);

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
            LoadRecords();

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

            if (InspirationTime > Timing.Global.MillisecondsUtc)
            {
                SendInspirationUpdateText(-1);
            }
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
        }

        public void TryLogout(bool force = false, bool softLogout = false)
        {
            LastOnline = DateTime.Now;
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

        private void Logout(bool softLogout = false)
        {
            if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var instance))
            {
                instance.RemoveEntity(this);
            }

            //Update parties
            LeaveParty(true);

            // End combo
            EndCombo();

            //Update trade
            CancelTrade();

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

            lock (mEventLock)
            {
                EventLookup.Clear();
                EventBaseIdLookup.Clear();
                GlobalPageInstanceLookup.Clear();
                EventTileLookup.Clear();
            }

            InGame = false;
            mSentMap = false;
            mCommonEventLaunches = 0;
            LastMapEntered = Guid.Empty;
            ChatTarget = null;
            QuestOffers.Clear();
            CraftingTableId = Guid.Empty;
            CraftId = Guid.Empty;
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

        //Update
        public override void Update(long timeMs)
        {
            if (!InGame || MapId == Guid.Empty)
            {
                return;
            }

            var lockObtained = false;
            try
            {
                Monitor.TryEnter(EntityLock, ref lockObtained);
                if (lockObtained)
                {
                    if (Client == null) //Client logged out
                    {
                        if (CombatTimer < Timing.Global.Milliseconds)
                        {
                            Logout();

                            return;
                        }
                    }
                    else
                    {
                        if (SaveTimer < Timing.Global.Milliseconds)
                        {
                            var user = User;
                            if (user != null)
                            {
                                DbInterface.Pool.QueueWorkItem(user.Save, false);
                            }
                            SaveTimer = Timing.Global.Milliseconds + Options.Instance.Processing.PlayerSaveInterval;
                        }
                    }

                    if (CraftingTableId != Guid.Empty && CraftId != Guid.Empty)
                    {
                        var b = CraftingTableBase.Get(CraftingTableId);
                        if (b.Crafts.Contains(CraftId))
                        {
                            if (CraftTimer + CraftBase.Get(CraftId).Time < timeMs)
                            {
                                CraftItem(CraftId);
                            }
                            else
                            {
                                if (!CheckCrafting(CraftId))
                                {
                                    CraftId = Guid.Empty;
                                }
                            }
                        }
                        else
                        {
                            CraftId = Guid.Empty;
                        }
                    }

                    // Check to see if combos expired
                    if (ComboWindow > 0)
                    {
                        // Detract from the window
                        ComboWindow = (int)(ComboTimestamp - Timing.Global.Milliseconds);
                        if (ComboWindow < 0)
                        {
                            EndCombo(); // This will also send a packet - this way, we're not flooding the client with packets when there's no active combo
                        } else
                        {
                            PacketSender.SendComboPacket(Client, CurrentCombo, ComboWindow, ComboExp, MaxComboWindow);
                        }
                    }

                    // Check if the resource we're locked to has died - if so, alert client
                    if (resourceLock != null && resourceLock.IsDead())
                    {
                        SetResourceLock(false);
                    }

                    base.Update(timeMs);

                    if (mAutorunCommonEventTimer < Timing.Global.Milliseconds)
                    {
                        var autorunEvents = 0;
                        //Check for autorun common events and run them
                        foreach (var obj in EventBase.Lookup)
                        {
                            var evt = obj.Value as EventBase;
                            if (evt != null && evt.CommonEvent)
                            {
                                if (Options.Instance.Metrics.Enable)
                                {
                                    autorunEvents += evt.Pages.Count(p => p.CommonTrigger == CommonEventTrigger.Autorun);
                                }
                                StartCommonEvent(evt, CommonEventTrigger.Autorun);
                            }
                        }

                        mAutorunCommonEventTimer = Timing.Global.Milliseconds + Options.Instance.Processing.CommonEventAutorunStartInterval;
                        CommonAutorunEvents = autorunEvents;
                    }

                    //If we have a move route then let's process it....
                    if (MoveRoute != null && MoveTimer < timeMs)
                    {
                        //Check to see if the event instance is still active for us... if not then let's remove this route
                        var foundEvent = false;
                        foreach (var evt in EventLookup)
                        {
                            if (evt.Value.PageInstance == MoveRouteSetter)
                            {
                                foundEvent = true;
                                if (MoveRoute.ActionIndex < MoveRoute.Actions.Count)
                                {
                                    ProcessMoveRoute(this, timeMs);
                                }
                                else
                                {
                                    if (MoveRoute.Complete && !MoveRoute.RepeatRoute)
                                    {
                                        MoveRoute = null;
                                        MoveRouteSetter = null;
                                        PacketSender.SendMoveRouteToggle(this, false);
                                    }
                                }

                                break;
                            }
                        }

                        if (!foundEvent)
                        {
                            MoveRoute = null;
                            MoveRouteSetter = null;
                            PacketSender.SendMoveRouteToggle(this, false);
                        }
                    }

                    //If we switched maps, lets update the maps
                    if (LastMapEntered != MapId)
                    {
                        if (MapController.TryGetInstanceFromMap(LastMapEntered, MapInstanceId, out var oldMapInstance))
                        {
                            oldMapInstance.RemoveEntity(this);
                        }

                        if (MapId != Guid.Empty)
                        {
                            if (!MapController.Lookup.Keys.Contains(MapId))
                            {
                                WarpToSpawn();
                            }
                            else
                            {
                                if (MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var newMapInstance))
                                {
                                    newMapInstance.PlayerEnteredMap(this);
                                }
                            }
                        }
                    }

                    var map = MapController.Get(MapId);
                    foreach (var surrMap in map.GetSurroundingMaps(true))
                    {
                        if (surrMap == null)
                        {
                            continue;
                        }

                        MapInstance mapInstance;
                        // If the map does not yet have a MapInstance matching this player's instanceId, create one.
                        lock (EntityLock)
                        {
                            if (!surrMap.TryGetInstance(MapInstanceId, out mapInstance))
                            {
                                surrMap.TryCreateInstance(MapInstanceId, out mapInstance);
                            }
                        }

                        //Check to see if we can spawn events, if already spawned.. update them.
                        lock (mEventLock)
                        {
                            var autorunEvents = 0;
                            foreach (var mapEvent in mapInstance.EventsCache)
                            {
                                if (mapEvent != null)
                                {
                                    //Look for event
                                    var loc = new MapTileLoc(surrMap.Id, mapEvent.SpawnX, mapEvent.SpawnY);
                                    var foundEvent = EventExists(loc);
                                    if (foundEvent == null)
                                    {
                                        var tmpEvent = new Event(Guid.NewGuid(), surrMap, this, mapEvent)
                                        {
                                            Global = mapEvent.Global,
                                            MapId = surrMap.Id,
                                            SpawnX = mapEvent.SpawnX,
                                            SpawnY = mapEvent.SpawnY
                                        };

                                        EventLookup.AddOrUpdate(tmpEvent.Id, tmpEvent, (key, oldValue) => tmpEvent);
                                        EventBaseIdLookup.AddOrUpdate(mapEvent.Id, tmpEvent, (key, oldvalue) => tmpEvent);
                                        //var newTileLookup = new Dictionary<MapTileLoc, Event>(EventTileLookup);
                                        ////If we get a collision here we need to rethink the MapTileLoc struct..
                                        ////We want a fast lookup through this dictionary and this is hopefully a solution over using a slow Tuple.
                                        //newTileLookup.Add(loc, tmpEvent);
                                        //EventTileLookup = newTileLookup;

                                        EventTileLookup.AddOrUpdate(loc, tmpEvent, (key, oldvalue) => tmpEvent);
                                    }
                                    else
                                    {
                                        foundEvent.Update(timeMs, foundEvent.MapController);
                                    }
                                    if (Options.Instance.Metrics.Enable)
                                    {
                                        autorunEvents += mapEvent.Pages.Count(p => p.Trigger == EventTrigger.Autorun);
                                    }
                                }
                            }
                            MapAutorunEvents = autorunEvents;
                        }
                    }

                    //Check to see if we can spawn events, if already spawned.. update them.
                    lock (mEventLock)
                    {
                        foreach (var evt in EventLookup)
                        {
                            if (evt.Value == null)
                            {
                                continue;
                            }

                            var eventFound = false;
                            var eventMap = map;

                            if (evt.Value.MapId != Guid.Empty)
                            {
                                if (evt.Value.MapId != MapId)
                                {
                                    eventMap = evt.Value.MapController;
                                    eventFound = map.SurroundingMapIds.Contains(eventMap.Id);
                                }
                                else
                                {
                                    eventFound = true;
                                }
                            }


                            if (evt.Value.MapId == Guid.Empty)
                            {
                                evt.Value.Update(timeMs, eventMap);
                                if (evt.Value.CallStack.Count > 0)
                                {
                                    eventFound = true;
                                }
                            }


                            if (eventFound)
                            {
                                continue;
                            }

                            RemoveEvent(evt.Value.Id);
                        }
                    }
                }
            }
            finally
            {
                if (lockObtained)
                {
                    Monitor.Exit(EntityLock);
                }
            }
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
            pkt.Stats = GetStatValues();

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
                ((PlayerEntityPacket) packet).Equipment =
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

            return pkt;
        }

        public int GetNonBuffedStat(Stats stat)
        {
            return Stat[(int)stat].BaseStat + StatPointAllocations[(int)stat];
        }

        public override EntityTypes GetEntityType()
        {
            return EntityTypes.Player;
        }

        //Spawning/Dying
        private void Respawn()
        {
            EndCombo();

            //Remove any damage over time effects
            DoT.Clear();
            CachedDots = new DoT[0];
            Statuses.Clear();
            CachedStatuses = new Status[0];

            CombatTimer = 0;

            var cls = ClassBase.Get(ClassId);
            if (cls != null)
            {
                WarpToSpawn();
            }
            else
            {
                Warp(Guid.Empty, 0, 0, 0);
            }

            PacketSender.SendEntityDataToProximity(this);

            //Search death common event trigger
            StartCommonEventsWithTrigger(CommonEventTrigger.OnRespawn);
        }

        public override void Die(bool dropItems = true, Entity killer = null)
        {
            var currentMapZoneType = MapController.Get(Map.Id).ZoneType;
            CastTime = 0;
            CastTarget = null;

            //Flag death to the client
            DestroyVehicle();
            PlayDeathAnimation();
            PacketSender.SendPlayerDeath(this);

            //Event trigger
            foreach (var evt in EventLookup)
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
            }

            // EXP Loss - don't lose in shared instance, or in an Arena zone
            if ((InstanceType != MapInstanceType.Shared || Options.Instance.Instancing.LoseExpOnInstanceDeath) && currentMapZoneType != MapZones.Arena)
            {
                if (Options.Instance.PlayerOpts.ExpLossOnDeathPercent > 0)
                {
                    if (Options.Instance.PlayerOpts.ExpLossFromCurrentExp)
                    {
                        var ExpLoss = (this.Exp * (Options.Instance.PlayerOpts.ExpLossOnDeathPercent / 100.0));
                        TakeExperience((long)ExpLoss);
                    }
                    else
                    {
                        var ExpLoss = (GetExperienceToNextLevel(this.Level) * (Options.Instance.PlayerOpts.ExpLossOnDeathPercent / 100.0));
                        TakeExperience((long)ExpLoss);
                    }
                }
            }
            PacketSender.SendEntityDie(this);
            Reset();
            Respawn();
            PacketSender.SendInventory(this);
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
            Debug.Assert(ClassBase.Lookup != null, "ClassBase.Lookup != null");

            var playerClass = ClassBase.Get(ClassId);
            if (playerClass?.VitalRegen == null)
            {
                return;
            }

            foreach (Vitals vital in Enum.GetValues(typeof(Vitals)))
            {
                if (vital >= Vitals.VitalCount)
                {
                    continue;
                }

                var vitalId = (int) vital;
                var vitalValue = GetVital(vital);
                var maxVitalValue = GetMaxVital(vital);
                if (vitalValue >= maxVitalValue)
                {
                    continue;
                }

                var vitalRegenRate = (playerClass.VitalRegen[vitalId] + GetEquipmentVitalRegen(vital)) / 100f;
                var regenValue = (int) Math.Max(1, maxVitalValue * vitalRegenRate) *
                                 Math.Abs(Math.Sign(vitalRegenRate));

                AddVital(vital, regenValue);
            }
        }

        public override int GetMaxVital(int vital)
        {
            var classDescriptor = ClassBase.Get(this.ClassId);
            var classVital = 20;
            if (classDescriptor != null)
            {
                if (classDescriptor.IncreasePercentage)
                {
                    classVital = (int) (classDescriptor.BaseVital[vital] *
                                        Math.Pow(1 + (double) classDescriptor.VitalIncrease[vital] / 100, Level - 1));
                }
                else
                {
                    classVital = classDescriptor.BaseVital[vital] + classDescriptor.VitalIncrease[vital] * (Level - 1);
                }
            }

            classVital += CalculateVitalStatBonus(vital);
            var baseVital = classVital;

            // TODO: Alternate implementation for the loop
            //            classVital += Equipment?.Select(equipment => ItemBase.Get(Items.ElementAt(equipment)?.ItemId ?? Guid.Empty))
            //                .Sum(
            //                    itemDescriptor => itemDescriptor.VitalsGiven[vital] +
            //                                      (itemDescriptor.PercentageVitalsGiven[vital] * baseVital) / 100
            //                ) ?? 0;
            // Loop through equipment and see if any items grant vital buffs
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] >= 0 && Equipment[i] < Options.MaxInvItems && Equipment[i] < Items.Count)
                {
                    if (Items[Equipment[i]].ItemId != Guid.Empty)
                    {
                        var item = ItemBase.Get(Items[Equipment[i]].ItemId);
                        if (item != null)
                        {
                            classVital += item.VitalsGiven[vital] + item.PercentageVitalsGiven[vital] * baseVital / 100;
                        }
                    }
                }
            }

            //Must have at least 1 hp and no less than 0 mp
            if (vital == (int) Vitals.Health)
            {
                classVital = Math.Max(classVital, 1);
            }
            else if (vital == (int) Vitals.Mana)
            {
                classVital = Math.Max(classVital, 0);
            }

            return classVital;
        }

        public int CalculateVitalStatBonus(int vital)
        {
            int bonus = 0;
            if (vital == (int)Vitals.Health)
            {
                bonus = GetStatValue(Stats.Attack) / Options.AttackHealthDivider;
            }
            if (vital == (int)Vitals.Mana)
            {
                bonus = GetStatValue(Stats.AbilityPower) / Options.AbilityPowerManaDivider;
            }
            return bonus;
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

            return StatPointAllocations[(int)stat] + statIncrease;
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

            Level = Math.Min(Options.MaxLevel, level);
            if (resetExperience)
            {
                Exp = 0;
            }

            RecalculateStatsAndPoints();
            UnequipInvalidItems();
            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendExperience(this);
        }

        public void LevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (Level < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetLevel(Level + 1, resetExperience);

                    //Let's pull up class - leveling info
                    var classDescriptor = ClassBase.Get(ClassId);
                    if (classDescriptor?.Spells == null)
                    {
                        continue;
                    }

                    foreach (var spell in classDescriptor.Spells)
                    {
                        if (spell.Level != Level)
                        {
                            continue;
                        }

                        var spellInstance = new Spell(spell.Id);
                        if (TryTeachSpell(spellInstance, true))
                        {
                            messages.Add(
                                Strings.Player.spelltaughtlevelup.ToString(SpellBase.GetName(spellInstance.SpellId))
                            );
                        }
                    }
                }
            }

            PacketSender.SendChatMsg(this, Strings.Player.levelup.ToString(Level), ChatMessageType.Experience, CustomColors.Combat.LevelUp, Name);
            PacketSender.SendActionMsg(this, Strings.Combat.levelup, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }

            if (StatPoints > 0)
            {
                PacketSender.SendChatMsg(
                    this, Strings.Player.statpoints.ToString(StatPoints), ChatMessageType.Experience, CustomColors.Combat.StatPoints, Name
                );
            }

            RecalculateStatsAndPoints();
            UnequipInvalidItems();
            PacketSender.SendExperience(this);
            PacketSender.SendPointsTo(this);
            PacketSender.SendEntityDataToProximity(this);

            //Search for level up activated events and run them
            StartCommonEventsWithTrigger(CommonEventTrigger.LevelUp);
        }

        public void GiveExperience(long amount, bool partyCombo = false, int opponentLevel = -1)
        {
            if (Level < Options.MaxLevel)
            {
                if (ShouldAwardExp(opponentLevel)) // Don't give exp at all if more than X levels between
                {
                    if (CurrentCombo > 0)
                    {
                        ComboExp = CalculateComboExperience(amount, partyCombo, opponentLevel);
                        Exp += ComboExp;
                    }

                    Exp += (int)(amount * GetEquipmentBonusEffect(EffectType.EXP, 100) / 100);
                    if (Exp < 0)
                    {
                        Exp = 0;
                    }

                    if (!CheckLevelUp())
                    {
                        PacketSender.SendExperience(this, ComboExp);
                    }
                }   
            }
        }

        private bool ShouldAwardExp(int opponentLevel)
        {
            if (opponentLevel < 0) // not awarded via enemy
            {
                return true;
            }
            return opponentLevel >= (Level - Options.Combat.MinComboExpLvlDiff);
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
                            var partyExperience = (int)(descriptor.Experience * multiplier) / partyMembersInXpRange.Length;
                            foreach (var partyMember in partyMembersInXpRange)
                            {
                                partyMember.GiveExperience(partyExperience, true, entity.Level);
                                partyMember.UpdateQuestKillTasks(entity);
                                partyMember.UpdateComboTime();
                            }

                            if (partyEvent != null)
                            {
                                foreach (var partyMember in Party)
                                {
                                    if ((Options.Party.NpcDeathCommonEventStartRange <= 0 || partyMember.InRangeOf(this, Options.Party.NpcDeathCommonEventStartRange)) && !(partyMember == this && playerEvent != null))
                                    {
                                        partyMember.StartCommonEvent(partyEvent);
                                    }
                                }
                            }
                        }
                        else
                        {
                            GiveExperience(descriptor.Experience, false, entity.Level);
                            UpdateComboTime();
                            UpdateQuestKillTasks(entity);
                        }

                        if (playerEvent != null)
                        {
                            StartCommonEvent(playerEvent);
                        }

                        if (Equipment[Options.PrayerIndex] >= 0)
                        {
                            var prayer = ItemBase.Get(Items[Equipment[Options.PrayerIndex]].ItemId);
                            var prayerSpellId = prayer.ComboSpellId;
                            if (prayerSpellId != Guid.Empty && prayer.ComboInterval > 0 && CurrentCombo % prayer.ComboInterval == 0) // If there's a spell and we're on the right combo interval (every 2 kills, etc)
                            {
                                CastSpell(prayerSpellId, -1, true, npc, Dir);
                            }
                        }

                        break;
                    }

                case Resource resource:
                    {
                        var descriptor = resource.Base;
                        if (descriptor?.Event != null)
                        {
                            StartCommonEvent(descriptor.Event);
                        }

                        break;
                    }
            }
        }

        #region Combo Stuff
        private int CalculateComboExperience(long baseAmount, bool partyCombo, int entityLevel)
        {
            if (!ShouldAwardExp(entityLevel)) // don't give exp if the level gap was too large
            {
                return 0;
            }

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

        public void UpdateComboTime()
        {
            ComboWindow = MaxComboWindow;
            ComboTimestamp = Timing.Global.Milliseconds + ComboWindow;
            CurrentCombo++;
            StartCommonEventsWithTrigger(CommonEventTrigger.ComboUp);
            StartCommonEventsWithTrigger(CommonEventTrigger.ComboReached, "", "", CurrentCombo);
        }

        public void EndCombo()
        {
            if (CurrentCombo > 0) // prevents flooding the client with useless combo packets
            {
                ComboTimestamp = -1;
                ComboWindow = -1;
                ComboExp = 0;
                CurrentCombo = 0;
                PacketSender.SendComboPacket(Client, CurrentCombo, ComboWindow, ComboExp, MaxComboWindow); // sends the final packet of the combo
                StartCommonEventsWithTrigger(CommonEventTrigger.ComboEnd);
            }
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

        public override void TryAttack(
            Entity target,
            ProjectileBase projectile,
            SpellBase parentSpell,
            ItemBase parentItem,
            byte projectileDir
        )
        {
            if (!CanAttack(target, parentSpell))
            {
                return;
            }

            //If Entity is resource, check for the correct tool and make sure its not a spell cast.
            if (target is Resource resource)
            {
                if (resource.IsDead())
                {
                    return;
                }

                // We don't here deal in them fancy projectile tools o'er in dis town!
                if (parentSpell != null && projectile.Tool != resource.Base.Tool)
                {
                    return;
                }

                // Check that a resource is actually required.
                var descriptor = resource.Base;

                //Check Dynamic Requirements
                if (!Conditions.MeetsConditionLists(descriptor.HarvestingRequirements, this, null))
                {
                    if (!string.IsNullOrWhiteSpace(descriptor.CannotHarvestMessage))
                    {
                        PacketSender.SendChatMsg(this, descriptor.CannotHarvestMessage, ChatMessageType.Error);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(this, Strings.Combat.resourcereqs, ChatMessageType.Error);
                    }

                    return;
                }

                if (descriptor.Tool > -1 && descriptor.Tool < Options.ToolTypes.Count)
                {
                    if (projectile != null)
                    {
                        if (projectile.Tool != descriptor.Tool)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (parentItem == null || descriptor.Tool != parentItem.Tool) 
                        {
                            PacketSender.SendChatMsg(
                               this, Strings.Combat.toolrequired.ToString(Options.ToolTypes[descriptor.Tool]), ChatMessageType.Error
                           );

                            return;
                        }
                    }
                }
            }

            base.TryAttack(target, projectile, parentSpell, parentItem, projectileDir);
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

        public void TryAttack(Entity target)
        {
            if (CastTime >= Timing.Global.Milliseconds)
            {
                if (Options.Combat.EnableCombatChatMessages)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.channelingnoattack, ChatMessageType.Combat);
                }

                return;
            }

            if (!IsOneBlockAway(target))
            {
                return;
            }

            if (!CanAttack(target, null))
            {
                SetResourceLock(false);
                return;
            }

            if (target is EventPage)
            {
                return;
            }

            ItemBase weapon = GetEquippedWeapon();

            //If Entity is resource, check for the correct tool and make sure its not a spell cast.
            if (target is Resource resource)
            {
                if (resource.IsDead())
                {
                    SetResourceLock(false);
                    return;
                }

                // Check that a resource is actually required.
                var descriptor = resource.Base;

                //Check Dynamic Requirements
                if (!Conditions.MeetsConditionLists(descriptor.HarvestingRequirements, this, null))
                {
                    if (!string.IsNullOrWhiteSpace(descriptor.CannotHarvestMessage))
                    {
                        PacketSender.SendChatMsg(this, descriptor.CannotHarvestMessage, ChatMessageType.Error);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(this, Strings.Combat.resourcereqs, ChatMessageType.Error);
                    }

                    SetResourceLock(false);

                    return;
                }

                if (descriptor.Tool > -1 && descriptor.Tool < Options.ToolTypes.Count)
                {
                    if (weapon == null || descriptor.Tool != weapon.Tool)
                    {
                        PacketSender.SendChatMsg(
                            this, Strings.Combat.toolrequired.ToString(Options.ToolTypes[descriptor.Tool]), ChatMessageType.Error
                        );
                        
                        SetResourceLock(false);

                        return;
                    }
                }

                if (!resource.IsDead())
                {
                    SetResourceLock(true, resource);
                }
            } else
            {
                SetResourceLock(false);
            }

            if (weapon != null)
            {
                base.TryAttack(
                    target, weapon.Damage, (DamageType) weapon.DamageType, (Stats) weapon.ScalingStat, weapon.Scaling,
                    weapon.CritChance, weapon.CritMultiplier, null, null, weapon
                );
            }
            else
            {
                var classBase = ClassBase.Get(ClassId);
                if (classBase != null)
                {
                    base.TryAttack(
                        target, classBase.Damage, (DamageType) classBase.DamageType, (Stats) classBase.ScalingStat,
                        classBase.Scaling, classBase.CritChance, classBase.CritMultiplier
                    );
                }
                else
                {
                    base.TryAttack(target, 1, DamageType.Physical, Stats.Attack, 100, 10, 1.5);
                }
            }
        }

        public override bool CanAttack(Entity entity, SpellBase spell)
        {
            // Do not allow spells while in a vehicle
            if (InVehicle && spell != null)
            {
                return false;
            }

            // If self-cast, AoE, Projectile, Trap, or Dash.. always accept.
            if (spell?.Combat.TargetType == SpellTargetTypes.Self ||
                spell?.Combat.TargetType == SpellTargetTypes.AoE ||
                spell?.Combat.TargetType == SpellTargetTypes.Projectile ||
                spell?.Combat.TargetType == SpellTargetTypes.Trap ||
                spell?.SpellType == SpellTypes.Dash
                )
            {
                return true;
            }

            if (!base.CanAttack(entity, spell))
            {
                return false;
            }

            if (entity is EventPage)
            {
                return false;
            }

            var friendly = spell?.Combat != null && spell.Combat.Friendly;
            if (entity is Player player)
            {
                if (player.InParty(this) || this == player || (!Options.Instance.Guild.AllowGuildMemberPvp && friendly != (player.Guild != null && player.Guild == this.Guild)))
                {
                    return friendly;
                }
            }

            if (entity is Resource)
            {
                if (spell != null)
                {
                    return false;
                }
            }

            if (entity is Npc npc)
            {   
                return !friendly && npc.CanPlayerAttack(this) || friendly && npc.IsAllyOf(this);
            }

            return true;
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
                            InRangeOf(npc, npc.Base.SightRange))
                        {
                            npc.AssignTarget(attacker);
                        }
                    }
                );
            }
        }

        public override int CalculateAttackTime()
        {
            ItemBase weapon = null;
            var attackTime = base.CalculateAttackTime();

            var cls = ClassBase.Get(ClassId);
            if (cls != null && cls.AttackSpeedModifier == 1) //Static
            {
                attackTime = cls.AttackSpeedValue;
            }

            if (Options.WeaponIndex > -1 &&
                Options.WeaponIndex < Equipment.Length &&
                Equipment[Options.WeaponIndex] >= 0)
            {
                weapon = ItemBase.Get(Items[Equipment[Options.WeaponIndex]].ItemId);
            }

            if (weapon != null)
            {
                if (weapon.AttackSpeedModifier == 1) // Static
                {
                    if (resourceLock != null)
                    {
                        var speedMod = (int) Math.Floor(weapon.AttackSpeedValue * resourceLock.CalculateHarvestBonus(this));

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
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                var equipment = Equipment[i];
                if (equipment >= 0 && equipment < Items.Count)
                {
                    var item = Items[equipment];
                    if (item.ItemId != Guid.Empty)
                    {
                        var descriptor = ItemBase.Get(item.ItemId);
                        if (descriptor != null)
                        {
                            flatStats += descriptor.StatsGiven[(int)statType] + item.StatBuffs[(int)statType];
                            percentageStats += descriptor.PercentageStatsGiven[(int)statType];
                        }
                    }
                }
            }

            return new Tuple<int, int>(flatStats, percentageStats);
        }

        public void RecalculateStatsAndPoints()
        {
            var playerClass = ClassBase.Get(ClassId);

            if (playerClass != null)
            {
                for (var i = 0; i < (int) Stats.StatCount; i++)
                {
                    var s = playerClass.BaseStat[i];

                    //Add class stat scaling
                    if (playerClass.IncreasePercentage) //% increase per level
                    {
                        s = (int) (s * Math.Pow(1 + (double) playerClass.StatIncrease[i] / 100, Level - 1));
                    }
                    else //Static value increase per level
                    {
                        s += playerClass.StatIncrease[i] * (Level - 1);
                    }

                    BaseStats[i] = s;
                }

                //Handle Changes in Points
                var currentPoints = StatPoints + StatPointAllocations.Sum();
                var expectedPoints = playerClass.BasePoints + playerClass.PointIncrease * (Level - 1);
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
                    while (removePoints > 0 && StatPointAllocations.Sum() > 0)
                    {
                        if (StatPointAllocations[i] > 0)
                        {
                            StatPointAllocations[i]--;
                            removePoints--;
                        }

                        i++;
                        if (i >= (int) Stats.StatCount)
                        {
                            i = 0;
                        }
                    }
                }
            }
        }

        //Warping
        public override void Warp(Guid newMapId, float newX, float newY, bool adminWarp = false)
        {
            EndCombo(); // Don't allow combos to transition between warps, I think? Maybe not.
            Warp(newMapId, newX, newY, (byte) Directions.Up, adminWarp, 0, false);
        }

        public void ForceInstanceChangeWarp(Guid newMapId, float newX, float newY, Guid newMapInstanceId, MapInstanceType instanceType, bool adminWarp = false)
        {
            PreviousMapInstanceId = MapInstanceId;
            PreviousMapInstanceType = InstanceType;

            MapInstanceId = newMapInstanceId;
            InstanceType = instanceType;
            EndCombo();
            // If we've warped the player out of their overworld, keep a reference to their overworld just in case.
            if (PreviousMapInstanceType == MapInstanceType.Overworld)
            {
                UpdateLastOverworldLocation(MapId, X, Y);
            }
            if (PreviousMapInstanceId != MapInstanceId)
            {
                PacketSender.SendChatMsg(this, Strings.Player.instanceupdate.ToString(PreviousMapInstanceId.ToString(), MapInstanceId.ToString()), ChatMessageType.Admin, CustomColors.Alerts.Info);
            }
            Warp(newMapId, newX, newY, (byte)Directions.Up, adminWarp, 0, false, false, MapInstanceType.NoChange, false, true);
        }

        public override void Warp(
            Guid newMapId,
            float newX,
            float newY,
            byte newDir,
            bool adminWarp = false,
            byte zOverride = 0,
            bool mapSave = false,
            bool fromWarpEvent = false,
            MapInstanceType mapInstanceType = MapInstanceType.NoChange,
            bool fromLogin = false,
            bool forceInstanceChange = false
        )
        {
            #region shortcircuit exits
            // First, deny the warp entirely if we CAN'T, for some reason, warp to the requested instance type. ONly do this if we're not forcing a change
            if (!forceInstanceChange && !CanChangeToInstanceType(mapInstanceType, fromLogin, newMapId))
            {
                return;
            }
            if (fromWarpEvent && Options.DebugAllowMapFades)
            {
                PacketSender.SendFadePacket(Client, false);
                PacketSender.SendUpdateFutureWarpPacket(Client, newMapId, newX, newY, newDir, mapInstanceType);
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
                    newMap.TryCreateInstance(MapInstanceId, out newMapInstance);
                    foreach (var surrMap in newSurroundingMaps)
                    {
                        MapController.Get(surrMap).TryCreateInstance(MapInstanceId, out var surrMapInstance);
                    }
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
                SendToNewMapInstance(newMap);
                // Clear all events - get fresh ones from the new instance to re-fresh event locations
                foreach (var evt in EventLookup)
                {
                    RemoveEvent(evt.Value.Id, false);
                }
            } else
            {
                // Clear events that are no longer on a surrounding map.
                foreach (var evt in EventLookup)
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
                newMapInstance.PlayerEnteredMap(this);
                PacketSender.SendEntityPositionToAll(this);

                //If map grid changed then send the new map grid
                if (!adminWarp && (oldMap == null || !oldMap.SurroundingMapIds.Contains(newMapId)) || fromLogin)
                {
                    PacketSender.SendMapGrid(this.Client, newMap.MapGrid, true);
                }

                mSentMap = true;
                    
                StartCommonEventsWithTrigger(CommonEventTrigger.MapChanged);
            }
            else // Player moved on same map?
            {
                if (onNewInstance)
                {
                    // But instance changed? Add player to the new instance (will also send stats thru SendEntityDataToProximity)
                    newMapInstance.PlayerEnteredMap(this);
                } else
                {
                    PacketSender.SendEntityStats(this);
                }
                PacketSender.SendEntityPositionToAll(this);
            }

            if (Options.DebugAllowMapFades)
            {
                PacketSender.SendFadePacket(Client, true); // fade in by default - either the player was faded out or was not
            }
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
            }
            else
            {
                if (!CanChangeToInstanceType(InstanceType, true, MapId))
                {
                    WarpToLastOverworldLocation(true);
                } else
                {
                    // Will warp to spawn if we fail to create an instance for the relevant map
                    Warp(
                        MapId, (byte)X, (byte)Y, (byte)Dir, false, (byte)Z, false, false, InstanceType, true
                    );
                }
            }
        }

        /// <summary>
        /// Warps the player to the last location they were at on the "Overworld" (empty Guid) map instance. Useful for kicking out of
        /// instances in a variety of situations.
        /// </summary>
        /// <param name="fromLogin">Whether or not we're coming to this method via the player login/join game flow</param>
        public void WarpToLastOverworldLocation(bool fromLogin)
        {
            Warp(
                LastOverworldMapId, (byte)LastOverworldX, (byte)LastOverworldY, (byte)Dir, false, (byte)Z, false, false, MapInstanceType.Overworld, fromLogin
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

        public void SendLivesRemainingMessage()
        {
            if (InstanceLives > 0)
            {
                PacketSender.SendChatMsg(this, Strings.Parties.instancelivesremaining.ToString(InstanceLives), ChatMessageType.Party, CustomColors.Chat.PartyChat);
            } else
            {
                PacketSender.SendChatMsg(this, Strings.Parties.nomorelivesremaining.ToString(InstanceLives), ChatMessageType.Party, CustomColors.Chat.PartyChat);
            }
        }

        public void WarpToSpawn(bool forceClassRespawn = false)
        {
            var mapId = Guid.Empty;
            byte x = 0, y = 0, dir = 0;

            if (Options.SharedInstanceRespawnInInstance && InstanceType == MapInstanceType.Shared && !forceClassRespawn)
            {
                if (SharedInstanceRespawn != null)
                {
                    if (Options.MaxSharedInstanceLives <= 0) // User has not configured shared instances to have lives
                    {
                        // Warp to the start of the shared instance - no concern for life total
                        Warp(SharedInstanceRespawnId, SharedInstanceRespawnX, SharedInstanceRespawnY, (Byte)SharedInstanceRespawnDir);
                    } else
                    {
                        // Check if the player/party have enough lives to spawn in-instance
                        if (InstanceLives > 0)
                        {
                            // If they do, subtract from this player's life total...
                            InstanceLives--;
                            SendLivesRemainingMessage();
                            // And the totals from any party members
                            if (Party != null && Party.Count > 1)
                            {
                                foreach (Player member in Party)
                                {
                                    if (member.Id != Id)
                                    {
                                        // Keep party member instance lives in sync
                                        member.InstanceLives--;
                                        if (member.InstanceType == MapInstanceType.Shared)
                                        {
                                            member.SendLivesRemainingMessage();
                                        }
                                    }
                                }
                            }

                            // And warp to the instance start
                            Warp(SharedInstanceRespawnId, SharedInstanceRespawnX, SharedInstanceRespawnY, (Byte)SharedInstanceRespawnDir);
                        } else
                        {
                            // The player has ran out of lives - too bad, back to instance entrance you go.
                            if (!Options.BootAllFromInstanceWhenOutOfLives || Party == null || Party.Count < 2)
                            {
                                WarpToLastOverworldLocation(false);
                            } else 
                            {
                                // Oh shit, hard mode enabled - boot ALL party members out of instance. No more lives.
                                foreach (Player member in Party)
                                {
                                    // Only warp players in the instance
                                    if (member.InstanceType == MapInstanceType.Shared)
                                    {
                                        lock (EntityLock)
                                        {
                                            member.WarpToLastOverworldLocation(false);
                                            PacketSender.SendChatMsg(member, Strings.Parties.instancefailed, ChatMessageType.Party, CustomColors.Chat.PartyChat);
                                        }
                                    }
                                }
                            }
                        }
                    }
                } else
                {
                    // invalid map - try overworld (which will throw to class spawn if itself is invalid)
                    WarpToLastOverworldLocation(false);
                }
            } else
            {
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
                case MapInstanceType.Guild:
                    if (Guild == null)
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
        private void SendToNewMapInstance(MapController newMap)
        {
            // Refresh the client's entity list
            var oldMap = MapController.Get(MapId);
            // Get the entities from the old map - we need to clear them off the player's global entities on their client
            if (oldMap != null && oldMap.TryGetInstance(PreviousMapInstanceId, out var oldMapInstance))
            {
                PacketSender.SendMapLayerChangedPacketTo(this, oldMap, PreviousMapInstanceId);
                oldMapInstance.ClearEntityTargetsOf(this); // Remove targets of this entity
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
                    if (!fromLogin) // If we're logging into a personal instance, we want to login to the SAME instance.
                    {
                        PersonalMapInstanceId = Guid.NewGuid();
                    }
                    newMapLayerId = PersonalMapInstanceId;
                    break;
                case MapInstanceType.Guild:
                    if (Guild != null)
                    {
                        newMapLayerId = Guild.GuildInstanceId;
                    } else
                    {
                        Log.Error($"Player {Name} requested a guild warp with no guild, and proceeded to warp to map anyway");
                        newMapLayerId = Guid.Empty;
                    }
                    break;
                case MapInstanceType.Shared:
                    bool isSolo = Party == null || Party.Count < 2;
                    bool isPartyLeader = Party != null && Party.Count > 0 && Party[0].Id == Id;

                    if (isSolo) // Solo instance initialization
                    {
                        if (Options.MaxSharedInstanceLives > 0)
                        {
                            InstanceLives = Options.MaxSharedInstanceLives;
                        }
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
                                if (Options.MaxSharedInstanceLives > 0)
                                {
                                    member.InstanceLives = Options.MaxSharedInstanceLives;
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

                            // And give your party members their instance lives - though this can be exploited when instances are rejoinable, so you'd really
                            // have to be a freak to have both options on
                            if (Options.MaxSharedInstanceLives > 0)
                            {
                                foreach (Player member in Party)
                                {
                                    member.InstanceLives = Options.MaxSharedInstanceLives;
                                }
                            }
                        }
                    }
                    // Use whatever your shared instance id is for the warp
                    newMapLayerId = SharedMapInstanceId;
                    
                    break;
                default:
                    Log.Error($"Player {Name} requested an instance type that is not supported. Their map instance settings will not change.");
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
        public bool TryGiveItem(Guid itemId, int quantity) => TryGiveItem(new Item(itemId, quantity), ItemHandling.Normal, false, true);

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
        public bool TryGiveItem(Item item, ItemHandling handler = ItemHandling.Normal, bool bankOverflow = false, bool sendUpdate = true, int overflowTileX = -1, int overflowTileY = -1)
        {
            var success = false;

            // Is this a valid item?
            if (item.Descriptor == null)
            {
                return false;
            }

            if (item.Quantity <= 0)
            {
                success = true;
            }

            // Get this information so we can use it later.
            var openSlots = FindOpenInventorySlots().Count;
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
                    else if (item.Descriptor.Stackable && openSlots < slotsRequired) // Is stackable, but no inventory space.
                    {
                        spawnAmount = item.Quantity;
                    }
                    else // Time to give them as much as they can take, and spawn the rest on the map!
                    {
                        spawnAmount = item.Quantity - openSlots;
                        if (openSlots > 0)
                        {
                            item.Quantity = openSlots;
                            GiveItem(item, sendUpdate);
                        }
                    }

                    // Do we have any items to spawn to the map?
                    if (spawnAmount > 0 && MapController.TryGetInstanceFromMap(Map.Id, MapInstanceId, out var instance))
                    {
                        instance.SpawnItem(overflowTileX > -1 ? overflowTileX : X, overflowTileY > -1 ? overflowTileY : Y, item, spawnAmount, Id, true, ItemSpawnType.Dropped);
                        success = spawnAmount != item.Quantity;
                    }

                    break;
                case ItemHandling.UpTo:
                    if (CanGiveItem(item)) // Can receive item under regular rules.
                    {
                        GiveItem(item, sendUpdate);
                        success = true;
                    }
                    else if (!item.Descriptor.Stackable && openSlots >= slotsRequired) // Is not stackable, has space for some.
                    {
                        item.Quantity = openSlots;
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
                StartCommonEventsWithTrigger(CommonEventTrigger.InventoryChanged);
                if (CraftingTableId != Guid.Empty) // Update our crafting table if we have one
                {
                    UpdateCraftingTable(CraftingTableId);
                }

                return success;
            }

            var bankInterface = new BankInterface(this, ((IEnumerable<Item>)Bank).ToList(), new object(), null, Options.MaxBankSlots);
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
                    for (var slot = 0; slot < Math.Ceiling((double)total / item.Descriptor.MaxInventoryStack); slot++)
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
                        openSlots[slot].Set(new Item(item.ItemId, quantity));
                        updateSlots.Add(openSlots[slot].Slot);
                    }
                }

            }

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
        public bool TryDropItemFrom(int slotIndex, int amount)
        {
            if (!TryGetItemAt(slotIndex, out var itemInSlot))
            {
                return false;
            }

            amount = Math.Min(amount, itemInSlot.Quantity);
            if (amount < 1)
            {
                // Abort if the amount we are trying to drop is below 1.
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
                PacketSender.SendInventoryItemUpdate(this, slotIndex);

                if (CraftingTableId != Guid.Empty) // Update our crafting table if we have one
                {
                    StartCommonEventsWithTrigger(CommonEventTrigger.InventoryChanged);
                    UpdateCraftingTable(CraftingTableId);
                }
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
            if (resourceLock != null)
            {
                SetResourceLock(false);
            }
            var equipped = false;
            var Item = Items[slot];
            var itemBase = ItemBase.Get(Item.ItemId);
            if (itemBase != null && Item.Quantity > 0)
            {

                //Check if the user is silenced or stunned
                foreach (var status in CachedStatuses)
                {
                    if (Options.Instance.CombatOpts.StunPreventsItems && status.Type == StatusTypes.Stun)
                    {
                        PacketSender.SendChatMsg(this, Strings.Items.stunned, ChatMessageType.Error);

                        return;
                    }

                    if (status.Type == StatusTypes.Sleep)
                    {
                        PacketSender.SendChatMsg(this, Strings.Items.sleep, ChatMessageType.Error);

                        return;
                    }
                }

                // Unequip items even if you do not meet the requirements.
                // (Need this for silly devs who give people items and then later add restrictions...)
                if (itemBase.ItemType == ItemTypes.Equipment)
                {
                    for (var i = 0; i < Options.EquipmentSlots.Count; i++)
                    {
                        if (Equipment[i] == slot)
                        {
                            Equipment[i] = -1;
                            FixVitals();
                            StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);
                            PacketSender.SendPlayerEquipmentToProximity(this);
                            PacketSender.SendEntityStats(this);

                            return;
                        }
                    }
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

                        switch (itemBase.Consumable.Type)
                        {
                            case ConsumableType.Health:
                                value = itemBase.Consumable.Value +
                                        GetMaxVital((int) itemBase.Consumable.Type) *
                                        itemBase.Consumable.Percentage /
                                        100;

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

                        var symbol = value < 0 ? Strings.Combat.removesymbol : Strings.Combat.addsymbol;
                        var number = $"{symbol}{Math.Abs(value)}";
                        PacketSender.SendActionMsg(this, number, color);

                        if (die)
                        {
                            lock (EntityLock)
                            {
                                Die(true, this);
                            }
                        }

                        TryTakeItem(Items[slot], 1);

                        break;
                    case ItemTypes.Equipment:
                        for (var i = 0; i < Options.EquipmentSlots.Count; i++)
                        {
                            if (Equipment[i] == slot)
                            {
                                Equipment[i] = -1;
                                equipped = true;
                            }
                        }

                        if (equipped)
                        {
                            FixVitals();
                            StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);
                            PacketSender.SendPlayerEquipmentToProximity(this);
                            PacketSender.SendEntityStats(this);

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
                            if (!CanSpellCast(itemBase.Spell, target, false))
                            {
                                return;
                            }

                            CastTarget = target;
                            CastSpell(itemBase.SpellId);
                        }
                        else if (!TryTeachSpell(new Spell(itemBase.SpellId)))
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
                        if (evt == null || !StartCommonEvent(evt))
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

        public void TryDestroyItem(int slot, int quantity)
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
        /// <returns></returns>
        public bool TryTakeItem(InventorySlot slot, int amount, ItemHandling handler = ItemHandling.Normal, bool sendUpdate = true)
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
            if (itemDescriptor.CanDestroy && !(Conditions.MeetsConditionLists(itemDescriptor.DestroyRequirements, this, null)))
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
            StartCommonEventsWithTrigger(CommonEventTrigger.InventoryChanged);

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

            // Update quest progress and we're done!
            UpdateGatherItemQuests(itemId);

            // Start common events related to inventory changes.
            StartCommonEventsWithTrigger(CommonEventTrigger.InventoryChanged);

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
            if (Equipment[Options.WeaponIndex] > -1 && Equipment[Options.WeaponIndex] < Options.MaxInvItems)
            {
                if (Items[Equipment[Options.WeaponIndex]].ItemId != Guid.Empty)
                {
                    var item = ItemBase.Get(Items[Equipment[Options.WeaponIndex]].ItemId);
                    if (item != null)
                    {
                        return item.Damage;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Gets the value of a bonus effect as granted by the currently equipped gear.
        /// </summary>
        /// <param name="effect">The <see cref="EffectType"/> to retrieve the amount for.</param>
        /// <param name="startValue">The starting value to which we're adding our gear amount.</param>
        /// <returns></returns>
        public int GetEquipmentBonusEffect(EffectType effect, int startValue = 0)
        {
            var value = startValue;

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] > -1)
                {
                    if (Items[Equipment[i]].ItemId != Guid.Empty)
                    {
                        var item = ItemBase.Get(Items[Equipment[i]].ItemId);
                        if (item != null)
                        {
                            if (item.Effect.Type == effect)
                            {
                                value += item.Effect.Percentage;
                            }
                        }
                    }
                }
            }

            return value;
        }

        public int GetEquipmentVitalRegen(Vitals vital)
        {
            var regen = 0;

            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] > -1)
                {
                    if (Items[Equipment[i]].ItemId != Guid.Empty)
                    {
                        var item = ItemBase.Get(Items[Equipment[i]].ItemId);
                        if (item != null)
                        {
                            regen += item.VitalsRegen[(int) vital];
                        }
                    }
                }
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

        public void SellItem(int slot, int amount)
        {
            var canSellItem = true;
            var rewardItemId = Guid.Empty;
            var rewardItemVal = 0;

            TryGetSlot(slot, out var itemInSlot, true);
            var sellItemNum = itemInSlot.ItemId;
            var shop = InShop;
            if (shop != null)
            {
                var itemDescriptor = Items[slot].Descriptor;
                if (itemDescriptor != null)
                {
                    if (!itemDescriptor.CanSell)
                    {
                        PacketSender.SendChatMsg(this, Strings.Shops.bound, ChatMessageType.Inventory, CustomColors.Items.Bound);

                        return;
                    }

                    //Check if this is a bag with items.. if so don't allow sale
                    if (itemDescriptor.ItemType == ItemTypes.Bag)
                    {
                        if (itemInSlot.TryGetBag(out var bag))
                        {
                            if (!bag.IsEmpty)
                            {
                                PacketSender.SendChatMsg(this, Strings.Bags.onlysellempty, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                                return;
                            }
                        }
                    }

                    if (!shop.BuysItem(itemDescriptor))
                    {
                        PacketSender.SendChatMsg(this, Strings.Shops.doesnotaccept, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                        return;
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
                        rewardItemVal = (int)Math.Floor(itemDescriptor.Price * shop.BuyMultiplier);
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

                    if (canSellItem)
                    {
                        TryGiveItem(rewardItemId, rewardItemVal * amount);

                        if (!TextUtils.IsNone(shop.SellSound))
                        {
                            PacketSender.SendPlaySound(this, shop.SellSound);
                        }
                    }

                    PacketSender.SendInventoryItemUpdate(this, slot);
                }
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

            return true;
        }

        public void CloseCraftingTable()
        {
            if (CraftingTableId != Guid.Empty && CraftId == Guid.Empty)
            {
                CraftingTableId = Guid.Empty;
                PacketSender.SendCloseCraftingTable(this);
            }
        }

        public void UpdateCraftingTable(Guid tableId)
        {
            var table = CraftingTableBase.Get(tableId);

            table.HiddenCrafts.Clear();
            foreach (Guid craftId in table.Crafts)
            {
                CraftBase craft = CraftBase.Get(craftId);
                if (!Conditions.MeetsConditionLists(craft.Requirements, this, null))
                {
                    table.HiddenCrafts.Add(craftId);
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

        //Craft a new item
        public void CraftItem(Guid id)
        {
            if (CraftingTableId != Guid.Empty)
            {
                var invbackup = new List<Item>();
                foreach (var item in Items)
                {
                    invbackup.Add(item.Clone());
                }

                //Quickly Look through the inventory and create a catalog of what items we have, and how many
                var itemdict = new Dictionary<Guid, int>();
                foreach (var item in Items)
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

                //Check the player actually has the items
                foreach (var c in CraftBase.Get(id).Ingredients)
                {
                    if (itemdict.ContainsKey(c.ItemId))
                    {
                        if (itemdict[c.ItemId] >= c.Quantity)
                        {
                            itemdict[c.ItemId] -= c.Quantity;
                        }
                        else
                        {
                            CraftId = Guid.Empty;

                            return;
                        }
                    }
                    else
                    {
                        CraftId = Guid.Empty;

                        return;
                    }
                }

                //Take the items
                foreach (var c in CraftBase.Get(id).Ingredients)
                {
                    if (!TryTakeItem(c.ItemId, c.Quantity))
                    {
                        for (var i = 0; i < invbackup.Count; i++)
                        {
                            Items[i].Set(invbackup[i]);
                        }

                        PacketSender.SendInventory(this);
                        CraftId = Guid.Empty;

                        return;
                    }
                }

                //Give them the craft
                var quantity = Math.Max(CraftBase.Get(id).Quantity, 1);
                var itm = ItemBase.Get(CraftBase.Get(id).ItemId);
                if (itm == null || !itm.IsStackable)
                {
                    quantity = 1;
                }

                if (TryGiveItem(CraftBase.Get(id).ItemId, quantity))
                {
                    var craftedItem = CraftBase.Get(id);
                    String itemName = ItemBase.GetName(craftedItem.ItemId);
                    PacketSender.SendChatMsg(
                        this, Strings.Crafting.crafted.ToString(itemName), ChatMessageType.Crafting,
                        CustomColors.Alerts.Success
                    );
                    
                    // Update our record of how many of this item we've crafted
                    int recordCrafted = IncrementRecord(RecordType.ItemCrafted, id);
                    if (Options.SendCraftingRecordUpdates && recordCrafted % Options.CraftingRecordUpdateInterval == 0)
                    {
                        SendRecordUpdate(Strings.Records.itemcrafted.ToString(recordCrafted, itemName));
                    }
                    GiveInspiredExperience(craftedItem.Experience);

                    if (CraftBase.Get(id).Event != null)
                        StartCommonEvent(craftedItem.Event);
                }
                else
                {
                    for (var i = 0; i < invbackup.Count; i++)
                    {
                        Items[i].Set(invbackup[i]);
                    }

                    PacketSender.SendInventory(this);
                    PacketSender.SendChatMsg(
                        this, Strings.Crafting.nospace.ToString(ItemBase.GetName(CraftBase.Get(id).ItemId)), ChatMessageType.Crafting,
                        CustomColors.Alerts.Error
                    );
                }

                CraftId = Guid.Empty;
            }
        }

        public bool CheckCrafting(Guid id)
        {
            //See if we have lost the items needed for our current craft, if so end the crafting session
            //Quickly Look through the inventory and create a catalog of what items we have, and how many
            var itemdict = new Dictionary<Guid, int>();
            foreach (var item in Items)
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

            //Check the player actually has the items
            foreach (var c in CraftBase.Get(id).Ingredients)
            {
                if (itemdict.ContainsKey(c.ItemId))
                {
                    if (itemdict[c.ItemId] >= c.Quantity)
                    {
                        itemdict[c.ItemId] -= c.Quantity;
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

        //Business
        public bool IsBusy()
        {
            return InShop != null ||
                InBank ||
                CraftingTableId != Guid.Empty ||
                Trading.Counterparty != null ||
                Trading.Requester != null ||
                PartyRequester != null ||
                FriendRequester != null;
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

            BankInterface = new BankInterface(this, bankItems, guild ? Guild.Lock : new object(), guild ? Guild : null, guild ? Guild.BankSlotsCount : Options.MaxBankSlots);

            GuildBank = guild;
            BankInterface.SendOpenBank();

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

        public void StoreBagItem(int slot, int amount, int bagSlot)
        {
            if (InBag == null || !HasBag(InBag))
            {
                return;
            }

            var itemBase = Items[slot].Descriptor;
            var bag = GetBag();
            if (itemBase != null && bag != null)
            {
                if (Items[slot].ItemId != Guid.Empty)
                {
                    if (!itemBase.CanBag)
                    {
                        PacketSender.SendChatMsg(this, Strings.Items.nobag, ChatMessageType.Inventory, CustomColors.Items.Bound);
                        return;
                    }

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

                    //Make Sure we are not Storing a Bag inside of itself
                    if (Items[slot].Bag == InBag)
                    {
                        PacketSender.SendChatMsg(this, Strings.Bags.baginself, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                        return;
                    }

                    if (itemBase.ItemType == ItemTypes.Bag)
                    {
                        PacketSender.SendChatMsg(this, Strings.Bags.baginbag, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                        return;
                    }

                    int currSlot = 0;
                    int count = bag.SlotCount;

                    if (bagSlot != -1)
                    {
                        currSlot = bagSlot;
                        count = bagSlot + 1;
                    }

                    //Find a spot in the bag for it!
                    if (itemBase.IsStackable)
                    {
                        for (var i = currSlot; i < count; i++)
                        {
                            if (bag.Slots[i] != null && bag.Slots[i].ItemId == Items[slot].ItemId)
                            {
                                amount = Math.Min(amount, int.MaxValue - bag.Slots[i].Quantity);
                                bag.Slots[i].Quantity += amount;

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

                                //LegacyDatabase.SaveBagItem(InBag, i, bag.Items[i]);
                                PacketSender.SendInventoryItemUpdate(this, slot);
                                PacketSender.SendBagUpdate(this, i, bag.Slots[i]);

                                return;
                            }
                        }
                    }

                    //Either a non stacking item, or we couldn't find the item already existing in the players inventory
                    for (var i = currSlot; i < count; i++)
                    {
                        if (bag.Slots[i] == null || bag.Slots[i].ItemId == Guid.Empty)
                        {
                            bag.Slots[i].Set(Items[slot]);
                            bag.Slots[i].Quantity = amount;

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

                            //LegacyDatabase.SaveBagItem(InBag, i, bag.Items[i]);
                            PacketSender.SendInventoryItemUpdate(this, slot);
                            PacketSender.SendBagUpdate(this, i, bag.Slots[i]);

                            return;
                        }
                    }

                    PacketSender.SendChatMsg(this, Strings.Bags.bagnospace, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                }
                else
                {
                    PacketSender.SendChatMsg(this, Strings.Bags.depositinvalid, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                }
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
            if (itemBase != null)
            {
                if (bag.Slots[slot] != null && bag.Slots[slot].ItemId != Guid.Empty)
                {
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

                    if (invSlot != -1)
                    {
                        if (itemBase.IsStackable && Items[invSlot] != null && Items[invSlot].ItemId == bag.Slots[slot].ItemId ||
                            Items[invSlot] == null || Items[invSlot].ItemId == Guid.Empty)
                        {
                            inventorySlot = invSlot;
                        }
                    }
                    else
                    {
                        //Find a spot in the inventory for it!
                        if (itemBase.IsStackable)
                        {
                            /* Find an existing stack */
                            for (var i = 0; i < Options.MaxInvItems; i++)
                            {
                                if (Items[i] != null && Items[i].ItemId == bag.Slots[slot].ItemId)
                                {
                                    inventorySlot = i;

                                    break;
                                }
                            }
                        }

                        if (inventorySlot < 0)
                        {
                            /* Find a free slot if we don't have one already */
                            for (var j = 0; j < Options.MaxInvItems; j++)
                            {
                                if (Items[j] == null || Items[j].ItemId == Guid.Empty)
                                {
                                    inventorySlot = j;

                                    break;
                                }
                            }
                        }
                    }
                    

                    /* If we don't have a slot send an error. */
                    if (inventorySlot < 0)
                    {
                        PacketSender.SendChatMsg(this, Strings.Bags.inventorynospace, ChatMessageType.Inventory, CustomColors.Alerts.Error);

                        return; //Panda forgot this :P
                    }

                    /* Move the items to the inventory */
                    amount = Math.Min(amount, int.MaxValue - Items[inventorySlot].Quantity);

                    if (Items[inventorySlot] == null ||
                        Items[inventorySlot].ItemId == Guid.Empty ||
                        Items[inventorySlot].Quantity < 0)
                    {
                        Items[inventorySlot].Set(bag.Slots[slot]);
                        Items[inventorySlot].Quantity = 0;
                    }

                    Items[inventorySlot].Quantity += amount;
                    if (amount >= bag.Slots[slot].Quantity)
                    {
                        bag.Slots[slot].Set(Item.None);
                    }
                    else
                    {
                        bag.Slots[slot].Quantity -= amount;
                    }

                    //LegacyDatabase.SaveBagItem(InBag, slot, bag.Items[slot]);

                    PacketSender.SendInventoryItemUpdate(this, inventorySlot);
                    PacketSender.SendBagUpdate(this, slot, bag.Slots[slot]);
                }
                else
                {
                    PacketSender.SendChatMsg(this, Strings.Bags.withdrawinvalid, ChatMessageType.Inventory, CustomColors.Alerts.Error);
                }
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

        public void OfferItem(int slot, int amount)
        {
            // TODO: Accessor cleanup
            if (Trading.Counterparty == null)
            {
                return;
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

                        return;
                    }

                    //Check if this is a bag with items.. if so don't allow sale
                    if (itemBase.ItemType == ItemTypes.Bag)
                    {
                        if (Items[slot].TryGetBag(out var bag))
                        {
                            if (!bag.IsEmpty)
                            {
                                PacketSender.SendChatMsg(this, Strings.Bags.onlytradeempty, ChatMessageType.Trading, CustomColors.Alerts.Error);
                                return;
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

                                return;
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

                            return;
                        }
                    }

                    PacketSender.SendChatMsg(this, Strings.Trading.tradenospace, ChatMessageType.Trading, CustomColors.Alerts.Error);
                }
                else
                {
                    PacketSender.SendChatMsg(this, Strings.Trading.offerinvalid, ChatMessageType.Trading, CustomColors.Alerts.Error);
                }
            }
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

            var inventorySlot = -1;
            var stackable = itemBase.IsStackable;
            if (stackable)
            {
                /* Find an existing stack */
                for (var i = 0; i < Options.MaxInvItems; i++)
                {
                    if (Items[i] != null && Items[i].ItemId == Trading.Offer[slot].ItemId)
                    {
                        inventorySlot = i;

                        break;
                    }
                }
            }

            if (inventorySlot < 0)
            {
                /* Find a free slot if we don't have one already */
                for (var j = 0; j < Options.MaxInvItems; j++)
                {
                    if (Items[j] == null || Items[j].ItemId == Guid.Empty)
                    {
                        inventorySlot = j;

                        break;
                    }
                }
            }

            /* If we don't have a slot send an error. */
            if (inventorySlot < 0)
            {
                PacketSender.SendChatMsg(this, Strings.Trading.inventorynospace, ChatMessageType.Trading, CustomColors.Alerts.Error);
            }

            if (amount > Trading.Offer[slot].Quantity)
            {
                amount = Trading.Offer[slot].Quantity;
            }

            /* Move the items to the inventory */
            amount = Math.Min(amount, int.MaxValue - Items[inventorySlot].Quantity);

            if (Items[inventorySlot] == null ||
                Items[inventorySlot].ItemId == Guid.Empty ||
                Items[inventorySlot].Quantity < 0)
            {
                Items[inventorySlot].Set(Trading.Offer[slot]);
                Items[inventorySlot].Quantity = amount;
            }
            else
            {
                Items[inventorySlot].Quantity += amount;
            }

            if (amount >= Trading.Offer[slot].Quantity)
            {
                Trading.Offer[slot] = null;
            }
            else
            {
                Trading.Offer[slot].Quantity -= amount;
            }

            PacketSender.SendInventoryItemUpdate(this, inventorySlot);
            PacketSender.SendTradeUpdate(this, this, slot);
            PacketSender.SendTradeUpdate(Trading.Counterparty, this, slot);
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
            if (Party.Count != 0)
            {
                PacketSender.SendChatMsg(fromPlayer, Strings.Parties.inparty.ToString(Name), ChatMessageType.Party, CustomColors.Alerts.Error);

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
            }
            else
            {
                if (Party[0] != this)
                {
                    PacketSender.SendChatMsg(this, Strings.Parties.leaderinvonly, ChatMessageType.Party, CustomColors.Alerts.Error);

                    return;
                }

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
                target.InstanceLives = InstanceLives;
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
                Party.Remove(this);

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

            if (SpellBase.Get(spell.SpellId) == null)
            {
                return false;
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
            if (!SpellBase.Get(Spells[spellSlot].SpellId).Bound)
            {
                Spells[spellSlot].Set(Spell.None);
                PacketSender.SendPlayerSpellUpdate(this, spellSlot);
            }
            else
            {
                PacketSender.SendChatMsg(this, Strings.Combat.tryforgetboundspell, ChatMessageType.Spells);
            }
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

        public virtual bool IsAllyOf(Player otherPlayer)
        {
            if (Guild != null && otherPlayer != null)
            {   
                return this.InParty(otherPlayer) || this.Guild.IsMember(otherPlayer.Id) || this == otherPlayer;
            }
            else if (otherPlayer != null)
            {
                return this.InParty(otherPlayer) || this == otherPlayer;
            }
            else
            {
                return false;
            }
        }

        public bool CanSpellCast(SpellBase spell, Entity target, bool checkVitalReqs)
        {
            if (!Conditions.MeetsConditionLists(spell.CastingRequirements, this, null))
            {
                if (!string.IsNullOrWhiteSpace(spell.CannotCastMessage))
                {
                    PacketSender.SendChatMsg(this, spell.CannotCastMessage, ChatMessageType.Error, "", true);
                }
                else
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.dynamicreq, ChatMessageType.Spells, CustomColors.Alerts.Error);
                }

                return false;
            }


            if (!CanAttack(target, spell))
            {
                return false;
            }

            //Check if the caster is silenced or stunned. Clense casts break the rule.
            if (spell.Combat.Effect != StatusTypes.Cleanse)
            {
                foreach (var status in CachedStatuses)
                {
                    if (status.Type == StatusTypes.Silence)
                    {
                        if (Options.Combat.EnableCombatChatMessages)
                        {
                            PacketSender.SendChatMsg(this, Strings.Combat.silenced, ChatMessageType.Combat);
                        }

                        return false;
                    }

                    if (status.Type == StatusTypes.Stun)
                    {
                        if (Options.Combat.EnableCombatChatMessages)
                        {
                            PacketSender.SendChatMsg(this, Strings.Combat.stunned, ChatMessageType.Combat);
                        }

                        return false;
                    }

                    if (status.Type == StatusTypes.Sleep)
                    {
                        if (Options.Combat.EnableCombatChatMessages)
                        {
                            PacketSender.SendChatMsg(this, Strings.Combat.sleep, ChatMessageType.Combat);
                        }

                        return false;
                    }
                }
            }

            if (target is Player)
            {
                //Only count safe zones and friendly fire if its a dangerous spell! (If one has been used)
                if (!spell.Combat.Friendly &&
                    (spell.Combat.TargetType != SpellTargetTypes.Self &&
                    spell.Combat.TargetType != SpellTargetTypes.AoE &&
                    spell.Combat.TargetType != SpellTargetTypes.Projectile &&
                    spell.SpellType == SpellTypes.CombatSpell
                    )
                 )
                {
                    // Check if either the attacker or the defender is in a "safe zone" (Only apply if combat is PVP)
                    if (MapController.Get(MapId).ZoneType == MapZones.Safe || MapController.Get(target.MapId).ZoneType == MapZones.Safe)
                    {
                        return false;
                    }

                    // Also consider this an issue if either player is in a different map zone type.
                    if (MapController.Get(MapId).ZoneType != MapController.Get(target.MapId).ZoneType)
                    {
                        return false;
                    }

                }
            }

            //Check if the caster has the right ammunition if a projectile
            if (spell.SpellType == SpellTypes.CombatSpell &&
                spell.Combat.TargetType == SpellTargetTypes.Projectile &&
                spell.Combat.ProjectileId != Guid.Empty)
            {
                var projectileBase = spell.Combat.Projectile;
                if (projectileBase == null)
                {
                    return false;
                }

                if (projectileBase.AmmoItemId != Guid.Empty)
                {
                    if (FindInventoryItemSlot(projectileBase.AmmoItemId, projectileBase.AmmoRequired) == null)
                    {
                        PacketSender.SendChatMsg(
                            this, Strings.Items.notenough.ToString(ItemBase.GetName(projectileBase.AmmoItemId)),
                            ChatMessageType.Inventory,
                            CustomColors.Alerts.Error
                        );

                        return false;
                    }
                }
            }

            //Check if snared and spell is a dash or warp
            if (spell.SpellType == SpellTypes.Dash || 
                spell.SpellType == SpellTypes.Warp ||
                spell.SpellType == SpellTypes.WarpTo)
            {
                // Don't cast if on snare status
                foreach (var status in CachedStatuses)
                {
                    if (status.Type == StatusTypes.Snare)
                    {
                        return false;
                    }
                }
            }

            var singleTargetSpell = (spell.SpellType == SpellTypes.CombatSpell && spell.Combat.TargetType == SpellTargetTypes.Single) || spell.SpellType == SpellTypes.WarpTo;

            if (target == null && singleTargetSpell)
            {
                return false;
            }

            if (target == this && spell.SpellType == SpellTypes.WarpTo)
            {
                return false;
            }

            if (target != null && singleTargetSpell)
            {
                if (! (MapController.Get(target.MapId)?.ZoneType == MapZones.Safe && MapController.Get(MapId)?.ZoneType == MapZones.Safe) )
                {
                    if (spell.Combat.Friendly != IsAllyOf(target))
                    {
                        return false;
                    }
                }
            }

            //Check for range of a single target spell
            if (singleTargetSpell && target != this)
            {
                if (!InRangeOf(target, spell.Combat.CastRange))
                {
                    if (Options.Combat.EnableCombatChatMessages)
                    {
                        PacketSender.SendChatMsg(this, Strings.Combat.targetoutsiderange, ChatMessageType.Combat);
                    }
                    return false;
                }
            }

            if (checkVitalReqs)
            {
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
            }

            return true;
        }

        public void UseSpell(int spellSlot, Entity target)
        {
            var spellNum = Spells[spellSlot].SpellId;
            Target = target;
            var spell = SpellBase.Get(spellNum);
            if (spell == null)
            {
                return;
            }

            if (!CanSpellCast(spell, target, true))
            {
                return;
            }

            // Reset stealth attack status
            StealthAttack = false;
            if (!SpellCooldowns.ContainsKey(Spells[spellSlot].SpellId) ||
                SpellCooldowns[Spells[spellSlot].SpellId] < Timing.Global.MillisecondsUtc)
            {
                if (CastTime == 0)
                {
                    CastTime = Timing.Global.Milliseconds + spell.CastDuration;

                    //Remove stealth status.
                    foreach (var status in CachedStatuses)
                    {
                        if (status.Type == StatusTypes.Stealth)
                        {
                            if (spell.WeaponSpell)
                            {
                                StealthAttack = true;
                            }
                            status.RemoveStatus();
                        }
                    }

                    SpellCastSlot = spellSlot;
                    CastTarget = Target;

                    //Check if the caster has the right ammunition if a projectile
                    if (spell.SpellType == SpellTypes.CombatSpell &&
                        spell.Combat.TargetType == SpellTargetTypes.Projectile &&
                        spell.Combat.ProjectileId != Guid.Empty)
                    {
                        var projectileBase = spell.Combat.Projectile;
                        if (projectileBase != null && projectileBase.AmmoItemId != Guid.Empty)
                        {
                            TryTakeItem(projectileBase.AmmoItemId, projectileBase.AmmoRequired);
                        }
                    }

                    if (spell.CastAnimationId != Guid.Empty)
                    {
                        PacketSender.SendAnimationToProximity(
                            spell.CastAnimationId, 1, base.Id, MapId, 0, 0, (sbyte) Dir, MapInstanceId
                        ); //Target Type 1 will be global entity
                    }

                    //Check if cast should be instance
                    if (Timing.Global.Milliseconds >= CastTime)
                    {
                        //Cast now!
                        CastTime = 0;
                        CastSpell(Spells[SpellCastSlot].SpellId, SpellCastSlot);
                        CastTarget = null;
                    }
                    else
                    {
                        //Tell the client we are channeling the spell
                        PacketSender.SendEntityCastTime(this, spellNum);
                    }
                }
                else
                {
                    if (Options.Combat.EnableCombatChatMessages)
                    {
                        PacketSender.SendChatMsg(this, Strings.Combat.channeling, ChatMessageType.Combat);
                    }
                }
            }
            else
            {
                if (Options.Combat.EnableCombatChatMessages)
                {
                    PacketSender.SendChatMsg(this, Strings.Combat.cooldown, ChatMessageType.Combat);
                }
            }
        }

        public override void CastSpell(Guid spellId, int spellSlot = -1, bool prayerSpell = false, Entity prayerTarget = null, int prayerSpellDir = -1)
        {
            if (resourceLock != null)
            {
                SetResourceLock(false);
            }
            var spellBase = SpellBase.Get(spellId);
            if (spellBase == null)
            {
                return;
            }

            CastingWeapon = GetEquippedWeapon();

            switch (spellBase.SpellType)
            {
                case SpellTypes.Event:
                    {
                        var evt = spellBase.Event;
                        if (evt != null)
                        {
                            StartCommonEvent(evt);
                        }

                        base.CastSpell(spellId, spellSlot, prayerSpell, prayerTarget, prayerSpellDir); //To get cooldown :P

                        break;
                    }
                default:
                    base.CastSpell(spellId, spellSlot, prayerSpell, prayerTarget, prayerSpellDir);

                    break;
            }
        }
        
        public int CalculateStealthDamage(int baseDamage, ItemBase item)
        {
            if (StealthAttack && item.ProjectileId == Guid.Empty)
            {
                return (int)Math.Floor(baseDamage * Options.Combat.SneakAttackMultiplier);
            }
            else
            {
                return baseDamage;
            }
        }

        //Equipment
        public void EquipItem(ItemBase itemBase, int slot = -1)
        {
            if (itemBase == null)
            {
                return;
            }

            if (itemBase.ItemType == ItemTypes.Equipment)
            {
                if (slot == -1)
                {
                    for (var i = 0; i < Options.MaxInvItems; i++)
                    {
                        var tempItemBase = Items[i].Descriptor;
                        if (itemBase != null)
                        {
                            if (itemBase == tempItemBase)
                            {
                                slot = i;

                                break;
                            }
                        }
                    }
                }

                if (slot != -1)
                {
                    if (itemBase.EquipmentSlot == Options.WeaponIndex)
                    {
                        if (Options.WeaponIndex > -1)
                        {
                            //If we are equipping a 2hand weapon, remove the shield
                            Equipment[Options.WeaponIndex] = slot;
                            if (itemBase.TwoHanded)
                            {
                                if (Options.ShieldIndex > -1 && Options.ShieldIndex < Equipment.Length)
                                {
                                    Equipment[Options.ShieldIndex] = -1;
                                }
                            }
                        }
                    }
                    else if (itemBase.EquipmentSlot == Options.ShieldIndex)
                    {
                        if (Options.ShieldIndex > -1)
                        {
                            if (Options.WeaponIndex > -1 && Equipment[Options.WeaponIndex] > -1)
                            {
                                //If we have a 2-hand weapon, remove it to equip this new shield
                                var item = Items[Equipment[Options.WeaponIndex]].Descriptor;
                                if (item != null && item.TwoHanded)
                                {
                                    Equipment[Options.WeaponIndex] = -1;
                                }
                            }

                            Equipment[Options.ShieldIndex] = slot;
                        }
                    }
                    else
                    {
                        Equipment[itemBase.EquipmentSlot] = slot;
                    }
                }

                FixVitals();
                StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);
                PacketSender.SendPlayerEquipmentToProximity(this);
                PacketSender.SendEntityStats(this);
            }
        }

        public void UnequipItem(Guid itemId, bool sendUpdate = true)
        {
            var updated = false;
            for (int i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                var itemSlot = Equipment[i];
                if (itemSlot > -1 && Items[itemSlot]?.ItemId == itemId)
                {
                    Equipment[i] = -1;
                    updated = true;
                }
            }
            if (updated)
            {
                FixVitals();
                StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);
                if (sendUpdate)
                {
                    PacketSender.SendPlayerEquipmentToProximity(this);
                    PacketSender.SendEntityStats(this);
                }
            }
        }

        public void UnequipItem(int slot, bool sendUpdate = true)
        {
            Equipment[slot] = -1;
            FixVitals();
            StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);

            if (sendUpdate)
            {
                PacketSender.SendPlayerEquipmentToProximity(this);
                PacketSender.SendEntityStats(this);
            }
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

            FixVitals();
            PacketSender.SendPlayerEquipmentToProximity(this);
            PacketSender.SendEntityStats(this);
        }

        public void EquipmentProcessItemLoss(int slot)
        {
            var changed = false;
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                if (Equipment[i] == slot)
                {
                    changed |= Equipment[i] != -1;
                    Equipment[i] = -1;
                }
            }

            
            if (changed)
            {
                FixVitals();
                StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);
                PacketSender.SendPlayerEquipmentToProximity(this);
                PacketSender.SendEntityStats(this);
            }
        }

        /// <summary>
        /// Unequips any items that the player is currently wearing in which they no longer meet the conditions for
        /// Also unequips any items that are not actually equipment anymore
        /// </summary>
        public void UnequipInvalidItems()
        {
            var updated = false;
            for (int i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                var itemSlot = Equipment[i];
                if (itemSlot < 0)
                {
                    continue;
                }

                var item = Items[itemSlot];
                var descriptor = item?.Descriptor;

                if (descriptor == default || 
                    descriptor.ItemType != ItemTypes.Equipment || 
                    !Conditions.MeetsConditionLists(descriptor.UsageRequirements, this, null))
                {
                    Equipment[i] = -1;
                    updated = true;
                }                
            }
            if (updated)
            {
                FixVitals();
                StartCommonEventsWithTrigger(CommonEventTrigger.EquipChange);
                PacketSender.SendPlayerEquipmentToProximity(this);
                PacketSender.SendEntityStats(this);
            }
        }

        public void StartCommonEventsWithTrigger(CommonEventTrigger trigger, string command = "", string param = "", int val = -1)
        {
            foreach (var value in EventBase.Lookup.Values)
            {
                if (value is EventBase eventDescriptor && eventDescriptor.Pages.Any(p => p.CommonTrigger == trigger))
                {
                    StartCommonEvent(eventDescriptor, trigger, command, param, val);
                }
            }
        }

        public static void StartCommonEventsWithTriggerForAll(CommonEventTrigger trigger, string command = "", string param = "")
        {
            var players = Player.OnlineList;
            foreach (var value in EventBase.Lookup.Values)
            {
                if (value is EventBase eventDescriptor && eventDescriptor.Pages.Any(p => p.CommonTrigger == trigger))
                {
                    foreach (var player in players)
                    {
                        player.StartCommonEvent(eventDescriptor, trigger, command, param);
                    }
                }
            }
        }

        public static void StartCommonEventsWithTriggerForAllOnInstance(CommonEventTrigger trigger, Guid instanceId, string command = "", string param = "")
        {
            var relevantPlayers = Player.OnlineList.ToList().Where(player => player.MapInstanceId == instanceId);

            foreach (var value in EventBase.Lookup.Values)
            {
                if (value is EventBase eventDescriptor && eventDescriptor.Pages.Any(p => p.CommonTrigger == trigger))
                {
                    foreach (var player in relevantPlayers)
                    {
                        player.StartCommonEvent(eventDescriptor, trigger, command, param);
                    }
                }
            }
        }

        //Stats
        public void UpgradeStat(int statIndex)
        {
            if (Stat[statIndex].BaseStat + StatPointAllocations[statIndex] < Options.MaxStatValue && StatPoints > 0)
            {
                StatPointAllocations[statIndex]++;
                StatPoints--;
                PacketSender.SendEntityStats(this);
                PacketSender.SendPointsTo(this);
            }
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
                    Hotbar[index].PreferredStatBuffs = item.StatBuffs;
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

                HandleSpecialQuestStart(quest);

                StartCommonEvent(EventBase.Get(quest.StartEventId));
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
                lock (mEventLock)
                {
                    QuestOffers.Remove(questId);
                    var quest = QuestBase.Get(questId);
                    if (quest != null)
                    {
                        StartQuest(quest);
                        foreach (var evt in EventLookup)
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
        }

        public void DeclineQuest(Guid questId, bool fromQuestBoard)
        {
            if (QuestOffers.Contains(questId))
            {
                lock (mEventLock)
                {
                    
                    QuestOffers.Remove(questId);
                    if (!fromQuestBoard) // don't alert the player otherwise
                    {
                        PacketSender.SendChatMsg(
                            this, Strings.Quests.declined.ToString(QuestBase.GetName(questId)), ChatMessageType.Quest, CustomColors.Quests.Declined
                        );
                    }

                    foreach (var evt in EventLookup)
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
                            //Run failure branch
                            var tmpStack = new CommandInstance(stackInfo.Page, stackInfo.BranchIds[1]);
                            stackInfo.WaitingForResponse = CommandInstance.EventResponse.None;
                            evt.Value.CallStack.Push(tmpStack);
                        }
                    }
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
                                    StartCommonEvent(quest.Tasks[i].CompletionEvent);
                                }

                                if (i == quest.Tasks.Count - 1)
                                {
                                    //Complete Quest
                                    MarkQuestComplete(quest, questProgress);
                                    StartCommonEvent(EventBase.Get(quest.EndEventId));
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
                        StartCommonEvent(EventBase.Get(quest.EndEventId));
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

        public void ResetQuest(Guid questId)
        {
            var quest = QuestBase.Get(questId);
            if (quest != null)
            {
                var questProgress = FindQuest(questId);
                if (questProgress != null)
                {
                    MarkQuestReset(quest, questProgress);

                    PacketSender.SendQuestsProgress(this);
                }
            }
        }

        private void MarkQuestReset(QuestBase quest, Quest questProgress)
        {
            // Handle quests that aren't "normal" and should do some management on completion
            if (quest.QuestType != QuestType.Normal)
            {
                HandleSpecialQuestReset(quest, questProgress);
            }

            //Complete Quest
            questProgress.Completed = false;
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
                                            CustomColors.Quests.Completed);
                                    } else
                                    {
                                        PacketSender.SendChatMsg(this, 
                                            Strings.Quests.tasksremaining.ToString(taskClassInfo.TasksRemaining.ToString(), ClassBase.Get(quest.RelatedClassId).Name),
                                            ChatMessageType.Quest, 
                                            CustomColors.Quests.TaskUpdated);
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
                                            CustomColors.Quests.Completed);
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
                            StartCommonEventsWithTrigger(Enums.CommonEventTrigger.ClassRankIncreased, "", quest.RelatedClassId.ToString());
                        }

                        // Assign the new amount of tasks remaining
                        assignmentClassInfo.TasksRemaining = TasksRemainingForClassRank(assignmentClassInfo.Rank);

                        PacketSender.SendChatMsg(this,
                            Strings.Quests.classrankincreased.ToString(ClassBase.Get(quest.RelatedClassId).Name, assignmentClassInfo.Rank.ToString()),
                            ChatMessageType.Quest,
                            CustomColors.Quests.Completed);

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
        private Variable GetSwitch(Guid id)
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
                s = new Variable(id);
                s.Value.Boolean = value;
                Variables.Add(s);
            }

            if (changed)
            {
                StartCommonEventsWithTrigger(CommonEventTrigger.PlayerVariableChange, "", id.ToString());
            }
        }

        public Variable GetVariable(Guid id, bool createIfNull = false)
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

        private Variable CreateVariable(Guid id)
        {
            if (PlayerVariableBase.Get(id) == null)
            {
                return null;
            }

            var variable = new Variable(id);
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

        public void SetVariableValue(Guid id, long value)
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
                v = new Variable(id);
                v.Value.Integer = value;
                Variables.Add(v);
            }

            if (changed)
            {
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
                v = new Variable(id);
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
            foreach (var evt in EventLookup)
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
            foreach (var evt in EventLookup)
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
            lock (mEventLock)
            {
                foreach (var evt in EventLookup)
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
                            var tmpStack = new CommandInstance(stackInfo.Page, stackInfo.BranchIds[responseId - 1]);
                            evt.Value.CallStack.Push(tmpStack);
                        }

                        return;
                    }
                }
            }
        }

        public void PictureClosed(Guid eventId)
        {
            lock (mEventLock)
            {
                foreach (var evt in EventLookup)
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
        }

        public void RespondToEventInput(Guid eventId, int newValue, string newValueString, bool canceled = false)
        {
            lock (mEventLock)
            {
                foreach (var evt in EventLookup)
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
            foreach (var evt in EventLookup)
            {
                if (evt.Value.PageInstance != null)
                {
                    evt.Value.PageInstance.SendToPlayer();
                }
            }
        }

        public bool StartCommonEvent(
            EventBase baseEvent,
            CommonEventTrigger trigger = CommonEventTrigger.None,
            string command = "",
            string param = "",
            int val = -1
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

            if (EventBaseIdLookup.ContainsKey(baseEvent.Id))
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

        public override int CanMove(int moveDir)
        {
            //If crafting or locked by event return blocked
            if (CraftingTableId != Guid.Empty && CraftId != Guid.Empty)
            {
                return -5;
            }

            foreach (var evt in EventLookup)
            {
                if (evt.Value.HoldingPlayer)
                {
                    return -5;
                }
            }

            return base.CanMove(moveDir);
        }

        protected override int IsTileWalkable(MapController map, int x, int y, int z)
        {
            if (base.IsTileWalkable(map, x, y, z) == -1)
            {
                foreach (var evt in EventLookup)
                {
                    if (evt.Value.PageInstance != null)
                    {
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
                            return (int) EntityTypes.Event;
                        }
                    }
                }
            }

            return -1;
        }

        public override void Move(int moveDir, Player forPlayer, bool dontUpdate = false, bool correction = false)
        {
            lock (EntityLock)
            {
                SetResourceLock(false);

                var oldMap = MapId;
                base.Move(moveDir, forPlayer, dontUpdate, correction);

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

                    Warp(warpAtt.MapId, warpAtt.X, warpAtt.Y, dir, false, 0, false, warpAtt.FadeOnWarp, instanceType);
                }

                foreach (var evt in EventLookup)
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

                // If we've changed maps, start relevant events!
                if (oldMap != MapId)
                {
                    StartCommonEventsWithTrigger(CommonEventTrigger.MapChanged);
                }
            }
        }

        public void TryBumpEvent(Guid mapId, Guid eventId)
        {
            foreach (var evt in EventLookup)
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
            var eventInstance = evt;
            if (evt.Player == null) //Global
            {
                eventInstance = null;
                foreach (var e in EventLookup)
                {
                    if (e.Value.BaseEvent.Id == evt.BaseEvent.Id)
                    {
                        if (e.Value.PageInstance.MyPage == e.Value.BaseEvent.Pages[pageNum])
                        {
                            eventInstance = e.Value;

                            break;
                        }
                    }
                }
            }

            if (eventInstance != null)
            {
                if (eventInstance.PageInstance.Trigger != EventTrigger.PlayerCollide)
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

                var cooldownReduction = 1 - (item.IgnoreCooldownReduction ? 0 : GetEquipmentBonusEffect(EffectType.CooldownReduction) / 100f);
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
                var cooldownReduction = 1 - (spell.IgnoreCooldownReduction ? 0 : GetEquipmentBonusEffect(EffectType.CooldownReduction) / 100f);
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
            var cooldownReduction = 1 - GetEquipmentBonusEffect(EffectType.CooldownReduction) / 100f;
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

            var cooldownReduction = 1 - (ignoreCdr ? 0 : GetEquipmentBonusEffect(EffectType.CooldownReduction) / 100f);

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

        public void SetResourceLock(bool val, Resource resource = null)
        {
            if (resource != null && resource.Base != null && resource.Base.DoNotRecord) return;

            val = (resource != null);
            if (resourceLock != resource) // change has occured
            {
                resourceLock = resource;

                double harvestBonus = 0.0f;
                int progressUntilNextBonus = 0;
                if (resource != null)
                {
                    harvestBonus = resource.CalculateHarvestBonus(this);
                    progressUntilNextBonus = resource.GetHarvestsUntilNextBonus(this);
                }

                PacketSender.SendResourceLockPacket(this, val, harvestBonus, progressUntilNextBonus);
            }
        }

        /// <summary>
        /// Caclulate crit chance based on the player's current affinity
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public int CalculateEffectBonus(int amount, EffectType effect)
        {
            int effectAmt = GetEquipmentBonusEffect(effect, 0);

            if (effectAmt <= 0) return amount;

            float effectMod = effectAmt / 100f;
            amount = (int) Math.Round(amount * (1 + effectMod));

            return amount;
        }

        /// <summary>
        /// Caclulate crit chance based on the player's current affinity
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public double CalculateEffectBonus(double amount, EffectType effect)
        {
            int effectAmt = GetEquipmentBonusEffect(effect, 0);

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

        [NotMapped, JsonIgnore] public long CraftTimer = 0;

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

        #region Player Records
        public int IncrementRecord(RecordType type, Guid recordId)
        {
            lock (EntityLock)
            {
                int recordAmt = 0;
                PlayerRecord matchingRecord = PlayerRecords.Find(record => record.Type == type && record.RecordId == recordId);
                if (matchingRecord != null)
                {
                    matchingRecord.Amount++;
                    recordAmt = matchingRecord.Amount;
                }
                else
                {
                    PlayerRecord newRecord = new PlayerRecord(Id, type, recordId, 1);
                    PlayerRecords.Add(newRecord);
                    recordAmt = newRecord.Amount;
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
                StartCommonEventsWithTrigger(evtTrigger, "", recordId.ToString(), recordAmt);

                return recordAmt;
            }
        }

        public void SendRecordUpdate(string message)
        {
            PacketSender.SendChatMsg(this, message, ChatMessageType.Experience);
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
                PacketSender.SendChatMsg(this, Strings.Combat.inspirationgainedminutes.ToString(minutes, endTimeStamp), ChatMessageType.Combat, CustomColors.Combat.LevelUp);
            }
            else if (seconds > 0)
            {
                PacketSender.SendChatMsg(this, Strings.Combat.inspirationgained.ToString(seconds, endTimeStamp), ChatMessageType.Combat, CustomColors.Combat.LevelUp);
            }
            else
            {
                PacketSender.SendChatMsg(this, Strings.Combat.stillinspired.ToString(endTimeStamp), ChatMessageType.Combat, CustomColors.Combat.LevelUp);
            }
        }
        #endregion
    }

}
