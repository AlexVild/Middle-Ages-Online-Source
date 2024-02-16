﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.Logging;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Intersect.Server.Entities;
using Intersect.Server.Classes.Maps;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Server.Core;
using Intersect.Server.Localization;
using Intersect.Server.Core.Games.ClanWars;
using Intersect.Server.Database.GameData;
using static Intersect.GameObjects.Maps.MapBase;

namespace Intersect.Server.Maps
{
    /// <summary>
    /// A <see cref="MapInstance"/> exists to process map updates, but on a "layered" system, where a single map can be processing 
    /// differently for each instance that exists upon it.
    /// <remarks>
    /// <para>
    /// A <see cref="MapController"/> contains a list <see cref="MapController.mInstances"/>, each one responsible for sending the necessary
    /// packet updates to the players that exist on that instance.
    /// </para>
    /// <para>
    /// By taking the processing out of the <see cref="MapController"/> itself, we can support map "Instancing" - ergo, the ability to process
    /// the same map differently for different players/parties. This allows for concepts such as dungeons and personal cutscenes, minigames,
    /// duel arenas, etc etc.
    /// </para>
    /// <para>
    /// A <see cref="Player"/> is the sole creator of MapInstances. A new instance will be created (via a call to <see cref="MapController.TryCreateInstance(Guid, out MapInstance)"/>)
    /// when a player:
    /// <list type="number">
    /// <item>
    /// Warps to a new map, or their <see cref="Entity.MapInstanceId"/> has otherwise changed.
    /// </item> 
    /// <item>
    /// Walks across a map boundary and fetches new maps from <see cref="MapController.GetSurroundingMaps(bool)"/>.
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// A <see cref="MapInstance"/> needs to know when it is ready to be cleaned up. A MapController can be marked for cleanup if there are no players on itself
    /// or any of its surrounding layers, and at least <see cref="Options.TimeUntilMapCleanup"/> ms have passed since the last player was on the map/its neighbors.
    /// </para>
    /// </remarks>
    /// </summary>
    public partial class MapInstance : IDisposable
    {
        /// <summary>
        /// Reference to stay consistent/easy-to-read with overworld behavior
        /// </summary>
        public static readonly Guid OverworldInstanceId = Guid.Empty;

        private MapController mMapController;

        /// <summary>
        /// The last time the instance's properties were updated and packets were sent to players
        /// </summary>
        private long mLastUpdateTime;

        /// <summary>
        /// Used to determine when to process the instance (update properties & send packets to players.
        /// <remarks>
        /// This is flipped whenever we have NO players on this or surrounding instances.
        /// </remarks>
        /// </summary>
        private bool mIsProcessing;

        /// <summary>
        /// Used as a timestamp for the last time a player was seen on this or a neighboring map
        /// </summary>
        private long mLastPlayerTime { get; set; }

        //SyncLock
        protected object mMapProcessLock = new object();

        /// <summary>
        /// A unique identifier referring to this processing instance alone.
        /// <remarks>
        /// Note that this is NOT the Instance instance identifier - that is <see cref="MapInstanceId"/>
        /// </remarks>
        /// </summary>
        public Guid Id;

        /// <summary>
        /// An ID referring to which instance this processer belongs to.
        /// <remarks>
        /// Entities/Items/Etc with that share an <see cref="Entity.MapInstanceId"/> with <see cref="MapInstance.MapInstanceId"/> 
        /// will be processed and fed packets by that processer.
        /// </remarks>
        /// </summary>
        public Guid MapInstanceId;

        /// <summary>
        /// The last time the <see cref="Core.LogicService.LogicThread"/> made a call to <see cref="Update(long)"/>.
        /// <remarks>
        /// This is used to determine when enough time has passed to cleanup this instance (remove it from it's parent <see cref="MapController"/>).
        /// </remarks>
        /// </summary>
        public long LastRequestedUpdateTime;

        /// <summary>
        /// When the <see cref="Core.LogicService.LogicThread"/> started processing this instance.
        /// </summary>
        public long UpdateQueueStart = -1;

        // Players & Entities
        private ConcurrentDictionary<Guid, Player> mPlayers = new ConcurrentDictionary<Guid, Player>();
        private readonly ConcurrentDictionary<Guid, Entity> mEntities = new ConcurrentDictionary<Guid, Entity>();
        private Entity[] mCachedEntities = new Entity[0];
        private MapEntityMovements mEntityMovements = new MapEntityMovements();

        // NPCs
        public ConcurrentDictionary<NpcSpawn, MapNpcSpawn> NpcSpawnInstances = new ConcurrentDictionary<NpcSpawn, MapNpcSpawn>();
        public int NpcSpawnGroup
        {
            get
            {
                if (InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
                {
                    if (instanceController.MapSpawnGroups == null)
                    {
                        return 0;
                    }
                    return instanceController.MapSpawnGroups.TryGetValue(mMapController?.Id ?? Guid.Empty, out var spawnGroup) ? spawnGroup.Group : 0;
                }
                else
                {
                    return 0;
                }
            }
        }

        // Items
        public ConcurrentDictionary<Guid, MapItemSpawn> ItemRespawns = new ConcurrentDictionary<Guid, MapItemSpawn>();
        public ConcurrentDictionary<Guid, MapItem>[] TileItems { get; } = new ConcurrentDictionary<Guid, MapItem>[Options.Instance.MapOpts.Width * Options.Instance.MapOpts.Height];
        public ConcurrentDictionary<Guid, MapItem> AllMapItems { get; } = new ConcurrentDictionary<Guid, MapItem>();

        // Resources
        public ConcurrentDictionary<ResourceSpawn, MapResourceSpawn> ResourceSpawnInstances = new ConcurrentDictionary<ResourceSpawn, MapResourceSpawn>();
        public ConcurrentDictionary<Guid, ResourceSpawn> ResourceSpawns { get; set; } = new ConcurrentDictionary<Guid, ResourceSpawn>();

        // Projectiles & Traps
        public ConcurrentDictionary<Guid, Projectile> MapProjectiles { get; } = new ConcurrentDictionary<Guid, Projectile>();
        public Projectile[] MapProjectilesCached = new Projectile[0];
        public long LastProjectileUpdateTime = -1;
        public ConcurrentDictionary<Guid, MapTrapInstance> MapTraps = new ConcurrentDictionary<Guid, MapTrapInstance>();
        public MapTrapInstance[] MapTrapsCached = new MapTrapInstance[0];

        // Collision
        private BytePoint[] mMapBlocks = Array.Empty<BytePoint>();
        private BytePoint[] mNpcMapBlocks = Array.Empty<BytePoint>();

        // Events
        /* Processing Layers have Global Events - these are global to the INSTANCE, NOT to the MapController.
         * As of the initial "Add Instancing" refactor, this is what the stock "Is Global?" event tick box in the editor
         * will enable - an "Instance-Global" event */
        public ConcurrentDictionary<EventBase, Event> GlobalEventInstances = new ConcurrentDictionary<EventBase, Event>();
        public List<EventBase> EventsCache = new List<EventBase>();

        // Animations & Text
        private MapActionMessages mActionMessages = new MapActionMessages();
        private MapAnimations mMapAnimations = new MapAnimations();

        public MapInstance(MapController map, Guid mapInstanceId, Player creator)
        {
            mMapController = map;
            MapInstanceId = mapInstanceId;
            Id = Guid.NewGuid();
            if (!InstanceProcessor.CurrentControllers.Contains(MapInstanceId))
            {
                InstanceProcessor.AddInstanceController(MapInstanceId, creator);
            }
        }

        public bool IsDisposed { get; protected set; }

        public bool IsOverworld => MapInstanceId == Guid.Empty;

        /// <summary>
        /// Initializes the processing instance for processing - called in the constructor. Essentially refreshes the instance
        /// so that it can give everything it has to offer to the player by the time they arrive in it
        /// </summary>
        public void Initialize()
        {
            lock (GetLock())
            {
                mIsProcessing = true;

                CacheMapBlocks();
                CacheTerritorySpawners();
                DespawnEverything();
                RespawnEverything();
                InitializeTerritories();

                // We don't want overworld maps bothering with clan wars, as they don't get
                // .Dispose() called and might otherwise have long-hanging subscriptions
                if (!IsOverworld)
                {
                    ClanWarManager.StatusChange += OnClanWarStatusChange;
                }
            }
        }

        public bool ShouldBeCleaned()
        {
            return (MapInstanceId != OverworldInstanceId && !ShouldBeActive());
        }

        public bool ShouldBeActive()
        {
            return (mIsProcessing || LastRequestedUpdateTime <= mLastUpdateTime + Options.Map.TimeUntilMapCleanup);
        }

        public void RemoveInstanceFromController()
        {
            lock (GetLock())
            {
                mMapController.DisposeInstanceWithId(MapInstanceId);
            }
        }

        public object GetLock()
        {
            return mMapProcessLock;
        }

        /// <summary>
        /// Gets our _parent's_ map lock.
        /// </summary>
        /// <returns>The parent map instance's map lock</returns>
        public object GetControllerLock()
        {
            return mMapController.GetMapLock();
        }

        public virtual void Dispose()
        {
            DestroyDanglingTimers();
            UnsubscribeFromClanWars();

            if (!IsDisposed)
            {
                IsDisposed = true;
            }
        }

        public void UnsubscribeFromClanWars()
        {
            ClearTerritoryInstances();
#if DEBUG
            Log.Debug($"{mMapController.Name} is unsubscribing from the ClanWarsManager...");
#endif
            ClanWarManager.StatusChange -= OnClanWarStatusChange;
        }

        public MapController GetController()
        {
            return mMapController;
        }

        /// <summary>
        /// Destroys the processable elements of the instance - RespawnEverything() to bring them back, and begin processing them
        /// in the update loop once more
        /// </summary>
        public void DespawnEverything()
        {
            DespawnNpcs();
            DespawnResources();
            DespawnProjectiles();
            DespawnTraps();
            DespawnItems();
            DespawnGlobalEvents();
        }

        /// <summary>
        /// Respawns everything - if this isn't called, the map will never begin processing anything in it's update loop, as
        /// it will have nothing to process. Note this also refreshes the event cache - refreshing events, global and local
        /// alike, when an Instance is refreshed.
        /// </summary>
        public void RespawnEverything()
        {
            SpawnMapNpcs();
            SpawnAttributeItems(); // This must be done before spawning items or resources
            SpawnMapResources();
            RefreshEventsCache();
            SpawnGlobalEvents();
        }

        /// <summary>
        /// The update loop - runs everytime the job in the LogicThread class executes it. Takes care of:
        /// - Checking if items need to spawn/be despawned
        /// - Move/process entity actions, regen
        /// - Determine if NPCs need respawned/regen'd
        /// - Determine if Resources need respawned/regen'd
        /// - Update global event processing
        /// </summary>
        /// <param name="timeMs">The current time the update was called. Used for determining updates to things such as respawn timers.</param>
        public void Update(long timeMs)
        {
            lock (GetLock())
            {
                if (mIsProcessing)
                {
                    if (Options.Instance.Processing.MapUpdateInterval == Options.Instance.Processing.ProjectileUpdateInterval)
                    {
                        UpdateProjectiles(timeMs);
                    }

                    UpdateItems(timeMs);
                    UpdateEntities(timeMs);
                    ProcessNpcRespawns();
                    ProcessResourceRespawns();
                    UpdateGlobalEvents(timeMs);

                    SendBatchedPacketsToPlayers();
                    mLastUpdateTime = timeMs;
                }

                // If there are no players on this or surrounding instances, stop processing updates.
                if (GetPlayers(true).Any())
                {
                    mLastPlayerTime = Timing.Global.Milliseconds + Options.Instance.Instancing.EmptyMapProcessingTime;
                    mIsProcessing = true;
                }
                else
                {
                    mIsProcessing = mLastPlayerTime >= Timing.Global.Milliseconds;
                }

                LastRequestedUpdateTime = timeMs;
            }
        }

        /// <summary>
        /// Adds an entity to the MapInstance so that the instance knows to process them.
        /// </summary>
        /// <param name="en">The entity to add.</param>
        public void AddEntity(Entity en)
        {
            var isDead = en.IsDead();
            // It's okay if a player is dead - we want to add them anyway since they're still 'there'
            if (en is Player)
            {
                isDead = false;
            }

            if (en != null && !isDead && en.MapInstanceId == MapInstanceId)
            {
                if (mEntities.ContainsKey(en.Id))
                {
                    return;
                }

                mEntities.TryAdd(en.Id, en);
                if (en is Player pl)
                {
                    mPlayers.TryAdd(pl.Id, pl);
                }
                mCachedEntities = mEntities.Values.ToArray();
            }
        }

        /// <summary>
        /// Removes an entity. After removal, that entity will no longer be updated by the processing instance.
        /// If this entity is a player, they will experience a complete desync from the server, as the map
        /// would not be sending them update packets any longer - so be mindful of what you're removing and when.
        /// </summary>
        /// <param name="en">The entity to remove.</param>
        public void RemoveEntity(Entity en)
        {
            mEntities.TryRemove(en.Id, out var result);
            if (mPlayers.ContainsKey(en.Id))
            {
                mPlayers.TryRemove(en.Id, out var pResult);
            }
            mCachedEntities = mEntities.Values.ToArray();
        }

        /// <summary>
        /// Returns a list of all the entities (NPCs, Resources, Players, EventPageInstances, Projectiles) being processed by this map.
        /// </summary>
        /// <param name="includeSurroundingMaps">Whether we should look at surrounding maps with a shared processing ID
        /// while acquiring this list.</param>
        /// <returns>A list of Entities</returns>
        public List<Entity> GetEntities(bool includeSurroundingMaps = false)
        {
            var entities = new List<Entity>(mEntities.Values.ToList());

            // ReSharper disable once InvertIf
            if (includeSurroundingMaps)
            {
                foreach (var mapInstance in MapController.GetSurroundingMapInstances(mMapController.Id, MapInstanceId, false))
                {
                    entities.AddRange(mapInstance.GetEntities());
                }
            }

            return entities;
        }

        /// <summary>
        /// Sends all the information a Player must receive when they've entered this Instance.
        /// </summary>
        /// <param name="player">The player who's entered</param>
        public void PlayerEnteredMap(Player player)
        {
            if (player == null) return;

            //Send Entity Info to Everyone and Everyone to the Entity
            player.Client?.SentMaps?.Clear();
            PacketSender.SendMapItems(player, mMapController.Id);

            AddEntity(player);
            player.LastMapEntered = mMapController.Id;

            // Send the entities/items of this current MapInstance to the player
            SendMapEntitiesTo(player);
            SendTrapsTo(player);

            // send the entities/items of the SURROUNDING maps on this instance to the player
            foreach (var surroundingMapInstance in MapController.GetSurroundingMapInstances(mMapController.Id, MapInstanceId, false))
            {
                surroundingMapInstance.SendMapEntitiesTo(player);
                surroundingMapInstance.SendTrapsTo(player);
                PacketSender.SendMapItems(player, surroundingMapInstance.GetController().Id);
            }
            PacketSender.SendEntityDataToProximity(player, player);

            player.MarkMapExplored(mMapController.Id);
        }

        /// <summary>
        /// Sends entities on the map to some player.
        /// </summary>
        /// <param name="player">The player to send entities to</param>
        public void SendMapEntitiesTo(Player player)
        {
            if (player != null)
            {
                PacketSender.SendMapEntitiesTo(player, mEntities);
                if (player.MapId == mMapController.Id && player.MapInstanceId == MapInstanceId)
                {
                    player.SendEvents();
                }
            }
        }

        /// <summary>
        /// Returns a list of all passable entities at some location
        /// </summary>
        /// <param name="x">The desired X coordinate</param>
        /// <param name="y">The desired Y coordinate</param>
        /// <param name="z">The desired Z coordinate</param>
        /// <returns>An array of entities existing at the point</returns>
        public Entity[] GetEntitiesAt(int x, int y, int z)
        {
            var tileEntities = new List<Entity>();
            foreach (var en in GetCachedEntities())
            {
                if (en.Id == Id) continue;
                if (en != null && en.X == x && en.Y == y && en.Z == z && !en.Passable)
                {
                    tileEntities.Add(en);
                }
            }

            return tileEntities.ToArray();
        }


        #region Players
        /// <summary>
        /// Gets all the players on a map instance
        /// </summary>
        /// <param name="includeSurrounding"></param>
        /// <returns></returns>
        public ICollection<Player> GetPlayers(bool includeSurrounding = false)
        {
            var allPlayers = new List<Player>(mPlayers.Values);

            if (includeSurrounding)
            {
                foreach (var mapInstance in MapController.GetSurroundingMapInstances(mMapController.Id, MapInstanceId, false))
                {
                    var adjoiningPlayers = mapInstance.GetPlayers(false);
                    if (adjoiningPlayers != null)
                    {
                        allPlayers.AddRange(adjoiningPlayers);
                    }
                }
            }

            return allPlayers;
        }
        #endregion

        #region NPCs
        /// <summary>
        /// Spawn map NPCs that the MapController has in it's list of spawns
        /// </summary>
        private void SpawnMapNpcs()
        {
            var spawns = mMapController.Spawns;
            for (var i = 0; i < spawns.Count; i++)
            {
                NpcSpawn spawn = spawns[i];
                if (NpcHasEnoughPlayersToSpawn(i) && CanSpawnInGroup(spawn) && !NpcSpawnInstances.TryGetValue(spawn, out _))
                {
                    SpawnMapNpc(i);
                }
            }
        }

        private bool CanSpawnInGroup(NpcSpawn spawn)
        {
            var spawns = mMapController.SpawnsInGroup(NpcSpawnGroup);
            return spawns.Count > 0 && spawns.Contains(spawn);
        }

        /// <summary>
        /// Spawns a map NPC from the list given to us by out MapController, at some
        /// given index.
        /// </summary>
        /// <param name="i">Index of the NPC in the Map Instance's Spawns list</param>
        private void SpawnMapNpc(int i)
        {
            var spawns = mMapController.Spawns;

            var spawn = spawns[i];
            var spawnId = spawn.NpcId;
            var npcBase = NpcBase.Get(spawnId);

            if (npcBase == null)
            {
                return;
            }

            // Should a champion spawn instead?
            var baseId = spawnId;
            if (AwaitingChampions.Contains(baseId) && !ActiveChampions.Contains(npcBase.ChampionId))
            {
                // This will now spawn the champion!
                lock (GetLock())
                {
                    spawnId = npcBase.ChampionId;
                    SpawnChampion(baseId, npcBase.ChampionId, npcBase.ChampionCooldownSeconds);
                }
            }

            MapNpcSpawn npcSpawnInstance;
            if (NpcSpawnInstances.ContainsKey(spawn))
            {
                npcSpawnInstance = NpcSpawnInstances[spawn];
            }
            else
            {
                npcSpawnInstance = new MapNpcSpawn();
                NpcSpawnInstances.TryAdd(spawn, npcSpawnInstance);
            }

            // Check to see if this NPC is on the permadeath list for this instance and, if so, do not spawn it
            // Unique identifier tying the spawning NPC to its map spawner
            string npcKey = $"{mMapController.Id}_{i}";
            if (spawn.PreventRespawn &&
                InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController) &&
                instanceController.PermadeadNpcs.Contains(npcKey))
            {
                return;
            }

            // Declared spawn
            FindNpcSpawnLocation(spawn, out var x, out var y, out var dir);
            if (TrySpawnNpc((byte)x, (byte)y, dir, spawnId, out Npc npc))
            {
                npc.InitializeToMap(mMapController.Id, spawn, npcSpawnInstance, npcKey);
            }
        }

        private void FindNpcSpawnLocation(NpcSpawn spawn, out int x, out int y, out byte dir)
        {
            dir = 0;
            x = 0;
            y = 0;

            if (spawn.Direction != NpcSpawnDirection.Random)
            {
                dir = (byte)(spawn.Direction - 1);
            }
            else
            {
                dir = (byte)Randomization.Next(0, 4);
            }

            // Declared spawn
            if (spawn.X >= 0 && spawn.Y >= 0)
            {
                x = spawn.X;
                y = spawn.Y;
            }
            // Random spawn
            else
            {
                for (var n = 0; n < 100; n++)
                {
                    x = (byte)Randomization.Next(0, Options.MapWidth);
                    y = (byte)Randomization.Next(0, Options.MapHeight);
                    if (mMapController.Attributes[x, y] == null || mMapController.Attributes[x, y].Type == (int)MapAttributes.Walkable)
                    {
                        break;
                    }

                    x = 0;
                    y = 0;
                }
            }
        }

        /// <summary>
        /// Physically places an NPC on our map instance
        /// </summary>
        /// <param name="tileX">X Spawn Co-ordinate</param>
        /// <param name="tileY">Y Spawn Co-ordinate</param>
        /// <param name="dir">Direction to face after spawn</param>
        /// <param name="npcId">NPC Entity ID to spawn</param>
        /// <param name="despawnable">Whether or not this NPC can be despawned (for example, if spawned via event command)</param>
        /// <returns></returns>
        public bool TrySpawnNpc(byte tileX, byte tileY, byte dir, Guid npcId, out Npc npc, bool despawnable = false)
        {
            npc = null;
            var npcBase = NpcBase.Get(npcId);
            if (npcBase == null)
            {
                return false;
            }

            var processLayer = MapInstanceId;
            npc = new Npc(npcBase, despawnable)
            {
                MapId = mMapController.Id,
                X = tileX,
                Y = tileY,
                Dir = dir,
                MapInstanceId = processLayer
            };

            AddEntity(npc);
            PacketSender.SendEntityDataToProximity(npc);

            return true;
        }

        /// <summary>
        /// Despawns all NPCs, and removes them from our dictionary of Spawn Instances.
        /// </summary>
        private void DespawnNpcs()
        {
            //Kill all npcs spawned from this map
            lock (GetLock())
            {
                ResetChampions();

                foreach (var npcSpawn in NpcSpawnInstances)
                {
                    if (npcSpawn.Value.Entity == null) continue;
                    
                    lock (npcSpawn.Value.Entity.EntityLock)
                    {
                        npcSpawn.Value.Entity.Die(false);
                    }
                }

                NpcSpawnInstances.Clear();

                //Kill any other npcs on this map (only players should remain)
                foreach (var entity in mEntities)
                {
                    if (entity.Value is Npc npc)
                    {
                        lock (npc.EntityLock)
                        {
                            npc.Die(false);
                        }
                    }
                }
            }
        }

        public void ResetNpcSpawns()
        {
            //Kill all npcs spawned from this map
            lock (GetLock())
            {
                foreach (var npcSpawn in NpcSpawnInstances)
                {
                    lock (npcSpawn.Value.Entity.EntityLock)
                    {
                        var npc = npcSpawn.Value.Entity as Npc;
                        if (!npc.Dead)
                        {
                            // If we keep track of reset radiuses, just reset it to that value.
                            if (Options.Npc.AllowResetRadius)
                            {
                                npc.Reset();
                            }
                            // If we don't.. Kill and respawn the Npc assuming it's in combat.
                            else
                            {
                                if (npc.Target != null || npc.CombatTimer > Timing.Global.Milliseconds)
                                {
                                    npc.Die(false);
                                    FindNpcSpawnLocation(npcSpawn.Key, out var x, out var y, out var dir);
                                    if (TrySpawnNpc((byte)x, (byte)y, dir, npcSpawn.Key.NpcId, out var newNpc))
                                    {
                                        npcSpawn.Value.Entity = newNpc;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method removes all NPCs whos spawn group is not valid
        /// </summary>
        private void RemoveInvalidSpawnGroups(int desiredGroup)
        {
            //Kill all npcs spawned from this map
            lock (GetLock())
            {
                foreach (var npcSpawn in NpcSpawnInstances.ToArray())
                {
                    var remove = false;
                    if (npcSpawn.Value.Entity == null) continue;

                    var spawn = npcSpawn.Key;

                    if (spawn.CumulativeSpawning && spawn.SpawnGroup > desiredGroup)
                    {
                        remove = true;
                    }
                    else if (spawn.SpawnGroup != desiredGroup && !spawn.CumulativeSpawning)
                    {
                        remove = true;
                    }

                    if (remove)
                    {
                        lock (npcSpawn.Value.Entity.EntityLock)
                        {
                            npcSpawn.Value.Entity.Die(false);
                            NpcSpawnInstances.TryRemove(npcSpawn.Key, out _);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears the given entity of any targets that an NPC has on them. For AI purposes.
        /// </summary>
        /// <param name="en">The entitiy to clear targets of.</param>
        public void ClearEntityTargetsOf(Entity en)
        {
            foreach (var entity in mEntities)
            {
                if (entity.Value is Npc npc && npc.Target == en)
                {
                    npc.RemoveTarget();
                }
            }
        }
        #endregion

        #region Resources
        /// <summary>
        /// Spawns a map resource attribute at its editor-given location
        /// </summary>
        /// <param name="x">X co-ordinate to spawn at</param>
        /// <param name="y">Y co-ordinate to spawn at</param>
        private void SpawnAttributeResource(byte x, byte y)
        {
            var tempResource = new ResourceSpawn()
            {
                ResourceId = ((MapResourceAttribute)mMapController.Attributes[x, y]).ResourceId,
                X = x,
                Y = y,
                Z = ((MapResourceAttribute)mMapController.Attributes[x, y]).SpawnLevel
            };

            ResourceSpawns.TryAdd(tempResource.Id, tempResource);
        }

        /// <summary>
        /// Freshly spawns all map resources within our list of Resource Spawns.
        /// </summary>
        private void SpawnMapResources()
        {
            foreach (var spawn in ResourceSpawns)
            {
                SpawnMapResource(spawn.Value);
            }
        }

        /// <summary>
        /// Spawns the given resource, and adds it to our list for processing
        /// </summary>
        /// <param name="spawn"></param>
        private void SpawnMapResource(ResourceSpawn spawn)
        {
            int x = spawn.X;
            int y = spawn.Y;
            var id = Guid.Empty;
            MapResourceSpawn resourceSpawnInstance;
            if (ResourceSpawnInstances.ContainsKey(spawn))
            {
                resourceSpawnInstance = ResourceSpawnInstances[spawn];
            }
            else
            {
                resourceSpawnInstance = new MapResourceSpawn();
                ResourceSpawnInstances.TryAdd(spawn, resourceSpawnInstance);
            }

            if (resourceSpawnInstance.Entity == null)
            {
                var resourceBase = ResourceBase.Get(spawn.ResourceId);
                if (resourceBase != null)
                {
                    var res = new Resource(resourceBase);
                    resourceSpawnInstance.Entity = res;
                    res.X = spawn.X;
                    res.Y = spawn.Y;
                    res.Z = spawn.Z;
                    res.MapId = mMapController.Id;
                    res.MapInstanceId = MapInstanceId;
                    id = res.Id;
                    AddEntity(res);
                }
            }
            else
            {
                id = resourceSpawnInstance.Entity.Id;
            }

            if (id != Guid.Empty)
            {
                resourceSpawnInstance.Entity.Spawn();
            }
        }

        /// <summary>
        /// Despawns all resources, and clears our list of spawn instances
        /// </summary>
        private void DespawnResources()
        {
            //Kill all resources spawned from this map
            lock (GetLock())
            {
                foreach (var resourceSpawn in ResourceSpawnInstances)
                {
                    if (resourceSpawn.Value != null && resourceSpawn.Value.Entity != null)
                    {
                        resourceSpawn.Value.Entity.Destroy();
                        RemoveEntity(resourceSpawn.Value.Entity);
                    }
                }

                ResourceSpawnInstances.Clear();
            }
        }
        #endregion

        #region Items
        /// <summary>
        /// Add a map item to this map.
        /// </summary>
        /// <param name="x">The X location of this item.</param>
        /// <param name="y">The Y location of this item.</param>
        /// <param name="item">The <see cref="MapItem"/> to add to the map.</param>
        private void AddItem(MapItem item)
        {
            AllMapItems.TryAdd(item.UniqueId, item);

            if (TileItems[item.TileIndex] == null)
            {
                TileItems[item.TileIndex] = new ConcurrentDictionary<Guid, MapItem>();
            }

            TileItems[item.TileIndex]?.TryAdd(item.UniqueId, item);
        }

        public enum ItemSpawnType
        {
            Normal,
            Dropped,
            PlayerDeath,
            LootRoll,
        }

        /// <summary>
        /// Spawn an item to this map instance.
        /// </summary>
        /// <param name="x">The horizontal location of this item</param>
        /// <param name="y">The vertical location of this item.</param>
        /// <param name="item">The <see cref="Item"/> to spawn on the map.</param>
        /// <param name="amount">The amount of times to spawn this item to the map. Set to the <see cref="Item"/> quantity, overwrites quantity if stackable!</param>
        public void SpawnItem(int x, int y, Item item, int amount) => SpawnItem(x, y, item, amount, Guid.Empty);

        /// <summary>
        /// Spawn an item to this map instance.
        /// </summary>
        /// <param name="x">The horizontal location of this item</param>
        /// <param name="y">The vertical location of this item.</param>
        /// <param name="item">The <see cref="Item"/> to spawn on the map.</param>
        /// <param name="amount">The amount of times to spawn this item to the map. Set to the <see cref="Item"/> quantity, overwrites quantity if stackable!</param>
        /// <param name="owner">The player Id that will be the temporary owner of this item.</param>
        public void SpawnItem(int x, int y, Item item, int amount, Guid owner, bool sendUpdate = true, ItemSpawnType spawnType = ItemSpawnType.Normal, long ownershipTimeOverride = -1)
        {
            if (item == null)
            {
                Log.Warn($"Tried to spawn {amount} of a null item at ({x}, {y}) in map {Id}.");

                return;
            }

            var ownershipTime = ownershipTimeOverride >= 0 ? ownershipTimeOverride : Options.Loot.ItemOwnershipTime;
            var itemDescriptor = ItemBase.Get(item.ItemId);
            if (itemDescriptor == null)
            {
                Log.Warn($"No item found for {item.ItemId}.");

                return;
            }

            var despawnTime = DetermineItemDespawnTime(spawnType);
            
            // Prevents overflow
            long checkedOwnershipTime;
            try
            {
                checked
                {
                    checkedOwnershipTime = Timing.Global.Milliseconds + ownershipTime;
                }
            }
            catch (OverflowException e)
            {
                checkedOwnershipTime = long.MaxValue;
            }

            // if we can stack this item or the user configured to drop items consolidated, simply spawn a single stack of it.
            // Does not count for Equipment and bags, these are ALWAYS their own separate item spawn. We don't want to lose data on that!
            if ((itemDescriptor.ItemType != ItemTypes.Equipment && itemDescriptor.ItemType != ItemTypes.Bag) &&
                (itemDescriptor.Stackable || Options.Loot.ConsolidateMapDrops))
            {
                // Does this item already exist on this tile? If so, get its value so we can simply consolidate the stack.
                var existingCount = 0;
                var existingItems = FindItemsAt(y * Options.MapWidth + x);
                var toRemove = new List<MapItem>();
                foreach (var exItem in existingItems)
                {
                    // If the Id and Owner matches, get its quantity and remove the item so we don't get multiple stacks.
                    if (exItem.ItemId == item.ItemId && exItem.Owner == owner)
                    {
                        existingCount += exItem.Quantity;
                        toRemove.Add(exItem);
                    }
                }

                var mapItem = new MapItem(item.ItemId, amount + existingCount, x, y, item.BagId, item.Bag, item.ItemProperties)
                {
                    DespawnTime = despawnTime,
                    Owner = owner,
                    OwnershipTime = checkedOwnershipTime,
                    VisibleToAll = Options.Loot.ShowUnownedItems || owner == Guid.Empty
                };

                if (mapItem.TileIndex > Options.MapHeight * Options.MapWidth || mapItem.TileIndex < 0)
                {
                    return;
                }

                // Remove existing items if we need to.
                foreach (var reItem in toRemove)
                {
                    RemoveItem(reItem);
                    if (sendUpdate)
                    {
                        PacketSender.SendMapItemUpdate(mMapController.Id, MapInstanceId, reItem, true);
                    }
                }

                // Drop the new item.
                AddItem(mapItem);
                if (sendUpdate)
                {
                    PacketSender.SendMapItemUpdate(mMapController.Id, MapInstanceId, mapItem, false);
                }
            }
            else
            {
                // Oh boy here we go! Set quantity to 1 and drop multiple!
                for (var i = 0; i < amount; i++)
                {
                    var mapItem = new MapItem(item.ItemId, amount, x, y, item.BagId, item.Bag, item.ItemProperties)
                    {
                        DespawnTime = despawnTime,
                        Owner = owner,
                        OwnershipTime = checkedOwnershipTime,
                        VisibleToAll = Options.Loot.ShowUnownedItems || owner == Guid.Empty
                    };

                    if (mapItem.TileIndex > Options.MapHeight * Options.MapWidth || mapItem.TileIndex < 0)
                    {
                        return;
                    }

                    AddItem(mapItem);
                }
                PacketSender.SendMapItemsToProximity(mMapController.Id, this);
            }
        }

        /// <summary>
        /// Returns a timestamp associated with when an item should be despawned, based upon the method it was spawned
        /// </summary>
        /// <param name="spawnType">A <see cref="ItemSpawnType"/> that determines in what way this item is being created</param>
        /// <returns></returns>
        public long DetermineItemDespawnTime(ItemSpawnType spawnType)
        {
            long despawnTime = Timing.Global.Milliseconds;
            switch (spawnType)
            {
                case ItemSpawnType.Dropped:
                    despawnTime += Options.Instance.LootOpts.PlayerDroppedItemDespawnTime;
                    break;
                case ItemSpawnType.PlayerDeath:
                    despawnTime += Options.Instance.LootOpts.PlayerDeathItemDespawnTime;
                    break;
                case ItemSpawnType.LootRoll:
                    despawnTime += Options.Instance.LootOpts.PlayerLootRollOverflowSpawnTime;
                    break;
                default:
                    despawnTime += Options.Instance.LootOpts.ItemDespawnTime;
                    break;
            }

            return despawnTime;
        }

        /// <summary>
        /// Find a Map Item on this map based on its Unique Id;
        /// </summary>
        /// <param name="uniqueId">The Unique Id of the Map Item to look for.</param>
        /// <returns>Returns a <see cref="MapItem"/> if one is found with the desired Unique Id.</returns>
        public MapItem FindItem(Guid uniqueId)
        {
            if (AllMapItems.TryGetValue(uniqueId, out MapItem item))
            {
                return item;
            }
            return null;
        }

        /// <summary>
        /// /// Find all map items at a specificed location.
        /// </summary>
        /// <param name="tileIndex">The integer value representation of the tile.</param>
        /// <returns>Returns a <see cref="ICollection"/> of <see cref="MapItem"/></returns>
        public ICollection<MapItem> FindItemsAt(int tileIndex)
        {
            if (tileIndex < 0 || tileIndex >= Options.MapWidth * Options.MapHeight || TileItems[tileIndex] == null)
            {
                return Array.Empty<MapItem>();
            }
            return TileItems[tileIndex].Values;
        }

        /// <summary>
        /// Removes some item from this processing instance
        /// </summary>
        /// <param name="item">The item to remove from the instance</param>
        /// <param name="respawn">Whether or not this item will respawn</param>
        public void RemoveItem(MapItem item, bool respawn = true)
        {
            if (item != null)
            {
                // Only try to handle respawns for items that have attribute spawn locations.
                if (item.AttributeSpawnX > -1)
                {
                    if (respawn)
                    {
                        var now = Timing.Global.Milliseconds;
                        var spawn = new MapItemSpawn()
                        {
                            AttributeSpawnX = item.X,
                            AttributeSpawnY = item.Y,
                            RespawnTime = now + Options.Map.ItemAttributeRespawnTime
                        };
                        if (item.UseCustomSpawnTime)
                        {
                            spawn.RespawnTime = now + item.CustomSpawnTime;
                        }
                        ItemRespawns.TryAdd(spawn.Id, spawn);
                    }
                }

                var oldOwner = item.Owner;
                AllMapItems.TryRemove(item.UniqueId, out MapItem removed);
                TileItems[item.TileIndex]?.TryRemove(item.UniqueId, out MapItem tileRemoved);
                if (TileItems[item.TileIndex]?.IsEmpty ?? false)
                {
                    TileItems[item.TileIndex] = null;
                }
                PacketSender.SendMapItemUpdate(mMapController.Id, MapInstanceId, item, true, item.VisibleToAll, oldOwner);
            }
        }

        /// <summary>
        /// Despawns all items, and removes them from our list of them.
        /// </summary>
        public void DespawnItems()
        {
            //Kill all items resting on map
            ItemRespawns.Clear();
            foreach (var item in AllMapItems.Values)
            {
                RemoveItem(item);
            }

            AllMapItems.Clear();
        }

        /// <summary>
        /// Spawns an item that was placed via editor Item map attribute
        /// </summary>
        /// <param name="x">X co-ordinate to spawn</param>
        /// <param name="y">Y co-ordinate to spawn</param>
        private void SpawnAttributeItem(int x, int y)
        {
            var itemAttr = (MapItemAttribute)mMapController.Attributes[x, y];
            var item = ItemBase.Get(itemAttr?.ItemId ?? Guid.Empty);
            if (item != null)
            {
                var mapItem = new MapItem(item.Id, itemAttr.Quantity, x, y);
                mapItem.DespawnTime = -1;
                mapItem.AttributeSpawnX = x;
                mapItem.AttributeSpawnY = y;
                if (item.ItemType == ItemTypes.Equipment)
                {
                    mapItem.Quantity = 1;
                }
                mapItem.UseCustomSpawnTime = itemAttr.UseCustomSpawnTime;
                if (mapItem.UseCustomSpawnTime)
                {
                    mapItem.CustomSpawnTime = itemAttr.CustomSpawnTime;
                }
                AddItem(mapItem);
                PacketSender.SendMapItemUpdate(mMapController.Id, MapInstanceId, mapItem, false);
            }
        }

        /// <summary>
        /// Spawns map atribute items/resources
        /// </summary>
        private void SpawnAttributeItems()
        {
            ResourceSpawns.Clear();
            for (byte x = 0; x < Options.MapWidth; x++)
            {
                for (byte y = 0; y < Options.MapHeight; y++)
                {
                    if (mMapController.Attributes[x, y] != null)
                    {
                        if (mMapController.Attributes[x, y].Type == MapAttributes.Item)
                        {
                            SpawnAttributeItem(x, y);
                        }
                        else if (mMapController.Attributes[x, y].Type == MapAttributes.Resource)
                        {
                            SpawnAttributeResource(x, y);
                        }
                    }
                }
            }
        }
        #endregion

        #region Projectiles & Traps
        /// <summary>
        /// Spawn a projectile on the map instance
        /// </summary>
        /// <param name="owner">Who spawned the projectile</param>
        /// <param name="projectile">Which projectile to spawn</param>
        /// <param name="parentSpell">If the projectile was spawned via spell</param>
        /// <param name="parentItem">If the projectile was spawned via item</param>
        /// <param name="mapId">What MapController the projectile was spawned on</param>
        /// <param name="x">X co-ordinate to spawn at</param>
        /// <param name="y">Y co-ordinate ot spawn at</param>
        /// <param name="z">Z co-ordinate to spawn at, if enabled in config</param>
        /// <param name="direction">Direction in which to spawn</param>
        /// <param name="target">The target of the projectil</param>
        public void SpawnMapProjectile(
            AttackingEntity owner,
            ProjectileBase projectile,
            SpellBase parentSpell,
            ItemBase parentItem,
            Guid mapId,
            byte x,
            byte y,
            byte z,
            byte direction,
            Entity target
        )
        {
            var proj = new Projectile(owner, parentSpell, parentItem, projectile, mMapController.Id, x, y, z, direction, target);
            proj.MapInstanceId = MapInstanceId;
            MapProjectiles.TryAdd(proj.Id, proj);
            MapProjectilesCached = MapProjectiles.Values.ToArray();
            PacketSender.SendEntityDataToProximity(proj);
        }

        /// <summary>
        /// Despawns all projectiles on the instance.
        /// </summary>
        public void DespawnProjectiles()
        {
            var guids = new List<Guid>();
            foreach (var proj in MapProjectiles)
            {
                if (proj.Value != null)
                {
                    guids.Add(proj.Value.Id);
                    proj.Value.Die();
                }
            }
            PacketSender.SendRemoveProjectileSpawns(mMapController.Id, MapInstanceId, guids.ToArray(), null);
            MapProjectiles.Clear();
            MapProjectilesCached = new Projectile[0];
        }

        public void RemoveProjectile(Projectile en)
        {
            MapProjectiles.TryRemove(en.Id, out Projectile removed);
            MapProjectilesCached = MapProjectiles.Values.ToArray();
        }

        public void SpawnTrap(AttackingEntity owner, SpellBase parentSpell, byte x, byte y, byte z)
        {
            var trap = new MapTrapInstance(owner, parentSpell, mMapController.Id, MapInstanceId, x, y, z);
            foreach(var player in GetPlayers(true))
            {
                PacketSender.SendMapTrapPacket(player, trap.MapId, trap.Id, trap.ParentSpell.TrapAnimationId, trap.Owner.Id, trap.X, trap.Y);
            }
            MapTraps.TryAdd(trap.Id, trap);
            MapTrapsCached = MapTraps.Values.ToArray();
        }

        public void SendTrapsTo(Player player)
        {
            foreach (var trap in MapTraps.Values)
            {
                PacketSender.SendMapTrapPacket(player, trap.MapId, trap.Id, trap.ParentSpell.TrapAnimationId, trap.Owner.Id, trap.X, trap.Y);
            }
        }

        public void DespawnTraps()
        {
            MapTraps.Clear();
            MapTrapsCached = new MapTrapInstance[0];
        }

        public void RemoveTrap(MapTrapInstance trap)
        {
            if (MapTraps.TryRemove(trap.Id, out MapTrapInstance removed))
            {
                foreach(var player in GetPlayers(true))
                {
                    PacketSender.SendMapTrapPacket(player, removed.MapId, removed.Id, removed.ParentSpell.CastAnimationId, removed.Owner.Id, removed.X, removed.Y, true);
                }
            }
            MapTrapsCached = MapTraps.Values.ToArray();
        }

        #endregion

        #region Events
        private void SpawnGlobalEvents()
        {
            GlobalEventInstances.Clear();
            foreach (var id in mMapController.EventIds)
            {
                var evt = EventBase.Get(id);
                if (evt != null && evt.Global)
                {
                    GlobalEventInstances.TryAdd(evt, new Event(evt.Id, evt, mMapController, MapInstanceId));
                }
            }
        }

        private void DespawnGlobalEvents()
        {
            //Kill global events on map (make sure processing stops for online players)
            //Gonna rely on GC for now
            foreach (var evt in GlobalEventInstances.ToArray())
            {
                foreach (var player in GetPlayers(true))
                {
                    player.RemoveEvent(evt.Value.BaseEvent.Id);
                }
            }

            GlobalEventInstances.Clear();
        }

        public List<Event> GetGlobalEventInstances()
        {
            return GlobalEventInstances.Values.ToList();
        }

        public Event GetGlobalEventInstance(EventBase baseEvent)
        {
            if (GlobalEventInstances.ContainsKey(baseEvent))
            {
                return GlobalEventInstances[baseEvent];
            }

            return null;
        }

        public bool FindEvent(EventBase baseEvent, EventPageInstance globalClone)
        {
            if (GlobalEventInstances.ContainsKey(baseEvent))
            {
                for (var i = 0; i < GlobalEventInstances[baseEvent].GlobalPageInstance.Length; i++)
                {
                    if (GlobalEventInstances[baseEvent].GlobalPageInstance[i] == globalClone)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void RefreshEventsCache()
        {
            var events = new List<EventBase>();
            foreach (var evt in mMapController.EventIds)
            {
                var itm = EventBase.Get(evt);
                if (itm != null)
                {
                    events.Add(itm);
                }
            }
            EventsCache = events;
        }
        #endregion

        #region Collision
        public bool TileBlocked(int x, int y)
        {
            if (mMapController.Attributes == null ||
                x < 0 || x >= mMapController.Attributes.GetLength(0) ||
                y < 0 || y >= mMapController.Attributes.GetLength(1))
            {
                return true;
            }

            //Check if tile is a blocked attribute
            if (mMapController.Attributes[x, y] != null && 
                (mMapController.Attributes[x, y].Type == MapAttributes.Blocked ||
                (mMapController.Attributes[x, y].Type == MapAttributes.Animation && ((MapAnimationAttribute)mMapController.Attributes[x, y]).IsBlock) ||
                (mMapController.Attributes[x, y].Type == MapAttributes.Item && ((MapItemAttribute)mMapController.Attributes[x, y]).IsBlock)))
            {
                return true;
            }

            //See if there are any entities in the way
            var entities = GetEntities();
            for (var i = 0; i < entities.Count; i++)
            {
                if (entities[i] != null && !(entities[i] is Projectile))
                {
                    //If Npc or Player then blocked.. if resource then check
                    if (!entities[i].Passable && entities[i].X == x && entities[i].Y == y)
                    {
                        return true;
                    }
                }
            }

            //Check Global Events
            foreach (var globalEvent in GlobalEventInstances)
            {
                if (globalEvent.Value != null && globalEvent.Value.PageInstance != null)
                {
                    if (!globalEvent.Value.PageInstance.Passable &&
                        globalEvent.Value.PageInstance.X == x &&
                        globalEvent.Value.PageInstance.Y == y)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region Packet Batching
        public void AddBatchedAnimation(PlayAnimationPacket packet)
        {
            mMapAnimations.Add(packet);
        }

        public void AddBatchedMovement(Entity en, bool correction, Player forPlayer)
        {
            mEntityMovements.Add(en, correction, forPlayer);
        }
        
        public void AddBatchedMovement(Player en, bool correction, int faceDirection, bool combatMode, Player forPlayer)
        {
            mEntityMovements.Add(en, correction, faceDirection, combatMode, forPlayer);
        }

        public void AddBatchedActionMessage(ActionMsgPacket packet)
        {
            mActionMessages.Add(packet);
        }

        public void AddBatchedCombatNumber(CombatNumberPacket packet)
        {
            mActionMessages.Add(packet);
        }
        #endregion

        #region Caching
        public void CacheMapBlocks()
        {
            var blocks = new List<BytePoint>();
            var npcBlocks = new List<BytePoint>();
            for (byte x = 0; x < Options.MapWidth; x++)
            {
                for (byte y = 0; y < Options.MapHeight; y++)
                {
                    if (mMapController.Attributes[x, y] != null)
                    {
                        if (mMapController.Attributes[x, y].Type == MapAttributes.Blocked ||
                            mMapController.Attributes[x, y].Type == MapAttributes.GrappleStone ||
                            (mMapController.Attributes[x, y].Type == MapAttributes.Animation && ((MapAnimationAttribute)mMapController.Attributes[x, y]).IsBlock) ||
                            (mMapController.Attributes[x, y].Type == MapAttributes.Item && ((MapItemAttribute)mMapController.Attributes[x, y]).IsBlock))
                        {
                            blocks.Add(new BytePoint(x, y));
                            npcBlocks.Add(new BytePoint(x, y));
                        }
                        else if (mMapController.Attributes[x, y].Type == MapAttributes.NpcAvoid)
                        {
                            npcBlocks.Add(new BytePoint(x, y));
                        }
                    }
                }
            }

            mMapBlocks = blocks.ToArray();
            mNpcMapBlocks = npcBlocks.ToArray();
        }

        public Entity[] GetCachedEntities()
        {
            return mCachedEntities;
        }

        public BytePoint[] GetCachedBlocks(bool isPlayer)
        {
            return isPlayer ? mMapBlocks : mNpcMapBlocks;
        }
        #endregion

        #region Updates
        private void UpdateEntities(long timeMs)
        {
            var surrMaps = mMapController.GetSurroundingMaps(true);

            // Keep a list of all entities with changed vitals and statusses.
            var vitalUpdates = new List<Entity>();
            var statusUpdates = new List<Entity>();

            foreach (var en in mEntities)
            {
                //Let's see if and how long this map has been inactive, if longer than X seconds, regenerate everything on the map
                if (timeMs > mLastUpdateTime + Options.Map.TimeUntilMapCleanup)
                {
                    //Regen Everything & Forget Targets
                    if (en.Value is Resource || en.Value is Npc)
                    {
                        en.Value.RestoreVital(Vitals.Health);
                        en.Value.RestoreVital(Vitals.Mana);

                        if (en.Value is Npc npc)
                        {
                            npc.AssignTarget(null);
                        }
                        else
                        {
                            en.Value.Target = null;
                        }
                    }
                }

                en.Value.Update(timeMs);

                // Check to see if we need to send any entity vital and status updates for this entity.
                if (en.Value.VitalsUpdated)
                {
                    vitalUpdates.Add(en.Value);

                    // Send a party update if we're a player with a party.
                    if (en.Value is Player player)
                    {
                        for (var i = 0; i < player.Party.Count; i++)
                        {
                            PacketSender.SendPartyUpdateTo(player.Party[i], player);
                        }
                    }

                    en.Value.VitalsUpdated = false;
                }

                if (en.Value.StatusesUpdated)
                {
                    statusUpdates.Add(en.Value);

                    en.Value.StatusesUpdated = false;
                }
            }

            if (vitalUpdates.Count > 0)
            {
                PacketSender.SendMapEntityVitalUpdate(mMapController, vitalUpdates.ToArray(), MapInstanceId);
            }

            if (statusUpdates.Count > 0)
            {
                PacketSender.SendMapEntitiesStatusUpdate(mMapController, statusUpdates.ToArray(), MapInstanceId);
            }
        }

        private void ProcessNpcRespawns()
        {
            var spawns = mMapController.Spawns;
            for (var i = 0; i < spawns.Count; i++)
            {
                if (NpcSpawnInstances.ContainsKey(spawns[i]) && CanSpawnInGroup(spawns[i]))
                {
                    var npcSpawnInstance = NpcSpawnInstances[spawns[i]];
                    // Entity can be null if the entity was never spawned (permadeath enabled on instance initialize)
                    if (npcSpawnInstance != null && npcSpawnInstance.Entity != null && npcSpawnInstance.Entity.Dead)
                    {
                        // Do not respawn NPCs that aren't supposed to respawn - unless on overworld
                        if (spawns[i].PreventRespawn && MapInstanceId != OverworldInstanceId) continue;

                        // If the entity is dead, but needs respawning, set its respawn time (or, wait for more players to show up before starting the timer)
                        if (npcSpawnInstance.RespawnTime == -1 || !NpcHasEnoughPlayersToSpawn(i))
                        {
                            npcSpawnInstance.RespawnTime = Timing.Global.Milliseconds +
                                                           ((Npc)npcSpawnInstance.Entity).Base.SpawnDuration -
                                                           (Timing.Global.Milliseconds - mLastUpdateTime);
                        }
                        // If we're passed the respawn time, and there's enough players on the instance to warrant a spawn
                        else if (npcSpawnInstance.RespawnTime < Timing.Global.Milliseconds && NpcHasEnoughPlayersToSpawn(i))
                        {
                            SpawnMapNpc(i);
                            npcSpawnInstance.RespawnTime = -1;
                        }
                    }
                } else if (NpcHasEnoughPlayersToSpawn(i) && CanSpawnInGroup(spawns[i]))
                {
                    SpawnMapNpc(i);
                }
            }
        }

        private bool NpcHasEnoughPlayersToSpawn(int spawnIndex)
        {
            int playersOnInstanceId;
            if (!InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
            {
                playersOnInstanceId = GetPlayers().Count; // Guaranteed to at LEAST have this map's count
            }
            else
            {
                playersOnInstanceId = instanceController.PlayerCount;
            }
            var spawns = mMapController.Spawns;

            if (spawnIndex >= spawns.Count || spawnIndex < 0)
            {
                return true;
            }

            return spawns[spawnIndex].RequiredPlayersToSpawn <= 1 || playersOnInstanceId >= spawns[spawnIndex].RequiredPlayersToSpawn;
        }

        /// <summary>
        /// Changes the spawn group of the map instance, and does extra processing if need be
        /// </summary>
        /// <param name="group">The group to change to</param>
        /// <param name="reset">Whether or not to despawn current NPCs on change</param>
        /// <param name="onlyInvalid">Whether we're only removing newly-invalid (based on spawn group) NPCs</param>
        /// <param name="instancePermanent">Whether or not this change should persist map cleanup so long as there are players on the instance</param>
        public void ChangeSpawnGroup(int group, bool reset, bool onlyInvalid, bool persistCleanup, bool surroundingMaps)
        {
            // Shit bad way of making it so two players don't request a spawn group change too close to one another
            if (Timing.Global.MillisecondsUtc < mSpawnGroupLastChangedAt + Options.Instance.Instancing.NpcSpawnGroupChangeMinimum)
            {
                return;
            }
            mSpawnGroupLastChangedAt = Timing.Global.MillisecondsUtc;

            // Can optionally get rid of all NPCs not belonging to this new group
            if (reset)
            {
                if (onlyInvalid)
                {
                    RemoveInvalidSpawnGroups(group);
                }
                else
                {
                    DespawnNpcs();
                }
            }
            // This will initialize spawn groups for the instance/map if needed. Saves processing time if we only keep track of
            // spawn groups that are actually ever being modified and assume 0 for all others.
            if (InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController))
            {
                instanceController.ChangeSpawnGroup(mMapController.Id, group, persistCleanup, surroundingMaps);
            }

            // Spawn the NPCs that belong to the new group
            SpawnMapNpcs();

            // Do the same thing for surrounding maps if necessary
            if (surroundingMaps)
            {
                foreach (var mapInstance in MapController.GetSurroundingMapInstances(mMapController.Id, MapInstanceId, false))
                {
                    mapInstance.ChangeSpawnGroup(group, reset, onlyInvalid, persistCleanup, false);
                }
            }
        }
        
        private void ProcessResourceRespawns()
        {
            foreach (var spawn in ResourceSpawns)
            {
                if (ResourceSpawnInstances.ContainsKey(spawn.Value))
                {
                    var resourceSpawnInstance = ResourceSpawnInstances[spawn.Value];
                    if (resourceSpawnInstance.Entity != null && resourceSpawnInstance.Entity.IsDead())
                    {
                        if (resourceSpawnInstance.RespawnTime == -1)
                        {
                            resourceSpawnInstance.RespawnTime = Timing.Global.Milliseconds +
                                                                resourceSpawnInstance.Entity.Base.SpawnDuration;
                        }
                        else if (resourceSpawnInstance.RespawnTime < Timing.Global.Milliseconds)
                        {
                            // Check to see if this resource can be respawned, if there's an Npc or Player on it we shouldn't let it respawn yet..
                            // Unless of course the resource is walkable regardless.
                            var canSpawn = false;
                            if (resourceSpawnInstance.Entity.Base.WalkableBefore)
                            {
                                canSpawn = true;
                            }
                            else
                            {
                                // Check if this resource is currently stepped on
                                var spawnBlockers = GetEntities().Where(x => x is Player || x is Npc).ToArray();
                                if (!spawnBlockers.Any(e => e.X == resourceSpawnInstance.Entity.X && e.Y == resourceSpawnInstance.Entity.Y))
                                {
                                    canSpawn = true;
                                }
                            }

                            if (canSpawn)
                            {
                                SpawnMapResource(spawn.Value);
                                resourceSpawnInstance.RespawnTime = -1;
                            }
                        }
                    }
                }
            }
        }

        public void UpdateProjectiles(long timeMs)
        {
            var spawnDeaths = new List<KeyValuePair<Guid, int>>();
            var projDeaths = new List<Guid>();

            //Process all of the projectiles
            foreach (var proj in MapProjectilesCached)
            {
                proj.Update(projDeaths, spawnDeaths);
            }

            if (projDeaths.Count > 0 || spawnDeaths.Count > 0)
            {
                PacketSender.SendRemoveProjectileSpawns(mMapController.Id, MapInstanceId, projDeaths.ToArray(), spawnDeaths.ToArray());
            }

            //Process all of the traps
            foreach (var trap in MapTrapsCached)
            {
                trap.Update();
            }

            LastProjectileUpdateTime = timeMs;
        }

        public void UpdateItems(long timeMs)
        {
            foreach (var mapItem in AllMapItems.Values)
            {
                // Should this item be visible to everyone now?
                if (!mapItem.VisibleToAll && mapItem.OwnershipTime < timeMs)
                {
                    mapItem.VisibleToAll = true;
                    PacketSender.SendMapItemUpdate(mMapController.Id, MapInstanceId, mapItem, false);
                }

                // Do we need to delete this item?
                if (mapItem.DespawnTime != -1 && mapItem.DespawnTime < timeMs)
                {
                    RemoveItem(mapItem);
                }
            }

            foreach (var itemRespawn in ItemRespawns.Values)
            {
                if (itemRespawn.RespawnTime < timeMs)
                {
                    SpawnAttributeItem(itemRespawn.AttributeSpawnX, itemRespawn.AttributeSpawnY);
                    ItemRespawns.TryRemove(itemRespawn.Id, out MapItemSpawn spawn);
                }
            }
        }

        private void UpdateGlobalEvents(long timeMs)
        {
            var evts = GlobalEventInstances.Values.ToList();
            for (var i = 0; i < evts.Count; i++)
            {
                //Only do movement processing on the first page.
                //This is because global events need to keep all of their pages at the same tile
                //Think about a global event moving randomly that needed to turn into a warewolf and back (separate pages)
                //If they were in different tiles the transition would make the event jump
                //Something like described here: https://www.ascensiongamedev.com/community/bug_tracker/intersect/events-randomly-appearing-and-disappearing-r983/
                for (var x = 0; x < evts[i].GlobalPageInstance.Length; x++)
                {
                    //Gotta figure out if any players are interacting with this event.
                    var active = false;
                    foreach (var player in GetPlayers(true))
                    {
                        var eventInstance = player.FindGlobalEventInstance(evts[i].GlobalPageInstance[x]);
                        if (eventInstance != null && eventInstance.CallStack.Count > 0)
                        {
                            active = true;
                        }
                    }

                    evts[i].GlobalPageInstance[x].Update(active, timeMs);
                }
            }
        }

        private void SendBatchedPacketsToPlayers()
        {
            var nearbyPlayers = new HashSet<Player>();

            // Get all players in surrounding and current maps
            foreach (var mapInstance in MapController.GetSurroundingMapInstances(mMapController.Id, MapInstanceId, true)) 
            {
                foreach (var plyr in mapInstance.GetPlayers())
                {
                    nearbyPlayers.Add(plyr);
                }
            }

            // And send batched packets out to all nearby players
            mEntityMovements.SendPackets(nearbyPlayers);
            mActionMessages.SendPackets(nearbyPlayers);
            mMapAnimations.SendPackets(nearbyPlayers);
        }
        #endregion

        #region Instance Variables
        public VariableValue GetInstanceVariable(Guid variableId)
        {
            var defaultVal = new VariableValue();
            defaultVal.SetValue(false);

            if (InstanceProcessor.TryGetInstanceController(MapInstanceId, out var instanceController) && instanceController.InstanceVariables.TryGetValue(variableId, out var instanceVariable))
            {
                return instanceVariable;
            } else
            {
                return defaultVal;
            }
        }

        public static void SetInstanceVariable(Guid variableId, VariableValue value, Guid mapInstanceId)
        {
            if (InstanceProcessor.TryGetInstanceController(mapInstanceId, out var instanceController))
            {
                instanceController.InstanceVariables[variableId] = value;
            }
            else
            {
                Log.Error($"Failed to set value for instance variable {variableId} in instance {mapInstanceId}");
            }
        }
        #endregion

        #region Timers
        /// <summary>
        /// Executes when the instance is cleaned up - gets rid of any timers that still exist for this instance.
        /// </summary>
        private void DestroyDanglingTimers()
        {
            foreach(var timer in TimerProcessor.ActiveTimers.Where(t => t.Descriptor.OwnerType == GameObjects.Timers.TimerOwnerType.Instance && t.OwnerId == Id).ToArray())
            {
                TimerProcessor.RemoveTimer(timer);
            }
        }
        #endregion
    }

    public partial class MapInstance : IDisposable
    {
        private long mSpawnGroupLastChangedAt { get; set; }

        private List<TerritoryInstance> ActiveTerritories { get; set; } = new List<TerritoryInstance>();
        
        private Guid[] mCachedTerritories { get; set; }

        public void RefreshNpcs()
        {
            DespawnNpcs();
            SpawnMapNpcs();
        }

        private void CacheTerritorySpawners()
        {
            var mapTerritories = new List<Guid>();
            for (byte x = 0; x < Options.MapWidth; x++)
            {
                for (byte y = 0; y < Options.MapHeight; y++)
                {
                    var attribute = mMapController.Attributes[x, y];
                    if (attribute == null || attribute.Type != MapAttributes.Territory)
                    {
                        continue;
                    }

                    try
                    {
                        var territoryAttr = attribute as MapTerritoryAttribute;
                        mapTerritories.Add(territoryAttr.TerritoryId);
                    }
                    catch (InvalidCastException e)
                    {
#if DEBUG
                        throw (e);
#else
                        Log.Error($"Invalid cast exception when caching map territories");
#endif
                    }
                }
            }

            mCachedTerritories = mapTerritories.ToArray();
        }

        public void ClearTerritoryInstances()
        {
            ActiveTerritories.Clear();
        }

        public void InitializeTerritories()
        {
            ClearTerritoryInstances();
            // Don't bother if a) overworld, b) map has no territory attributes or c) clan wars isn't active
            if (IsOverworld || mCachedTerritories?.Length == 0 || !ClanWarManager.ClanWarActive)
            {
                return;
            }

            lock (GetLock())
            {
                var clanWarId = ClanWarManager.CurrentWarId;

                using (var context = DbInterface.CreatePlayerContext(false))
                {
                    foreach (var territoryId in mCachedTerritories)
                    {
                        // Check to see if this territory has already been instantiated for the current CW
                        var territory = context.Territories.Find(territoryId, clanWarId);
                        if (territory != null)
                        {
                            territory.Initialize();
#if DEBUG
                            Log.Debug($"Loaded existing territory: {territory.Territory.Name} on map {mMapController.Name}");
#endif
                        }
                        else
                        {
                            // If not, make a new instance
                            territory = new TerritoryInstance(territoryId, clanWarId);
#if DEBUG
                            Log.Debug($"Loaded new territory: {territory.Territory.Name} on map {mMapController.Name}");
#endif
                            context.Territories.Add(territory);
                        }

                        // add to this map's list of territory instances
                        ActiveTerritories.Add(territory);
                    }

                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public void OnClanWarStatusChange(object sender, bool active)
        {
            if (active)
            {
                InitializeTerritories();
            }
            else
            {
                ClearTerritoryInstances();
            }
        }
    }

    #region Champions
    public partial class MapInstance : IDisposable
    {
        HashSet<Guid> AwaitingChampions { get; set; } = new HashSet<Guid>();
        HashSet<Guid> ActiveChampions { get; set; } = new HashSet<Guid>();

        Dictionary<Guid, long> ChampionCooldowns { get; set; } = new Dictionary<Guid, long>();

        public void ResetChampions()
        {
            AwaitingChampions.Clear();
            ActiveChampions.Clear();
            ChampionCooldowns.Clear();
        }

        public bool TryAddChampionOf(Guid npcId, Guid championId, Player killer)
        {
            // Only allow champions on the overworld
            if (MapInstanceId != Guid.Empty)
            {
                return false;
            }

            var descriptor = NpcBase.Get(npcId);
            if (descriptor == default || killer == default)
            {
                return false;
            }

            if (killer.DisableChampionSpawns)
            {
                var notificationChance = Randomization.Next(0, 4);
                if (notificationChance == 0)
                {
                    PacketSender.SendChatMsg(killer, "Reminder: You currently have champion spawns disabled. If you wish to re-enable them, speak to Slayer Syd at the Vennerwood Slayers Mansion.",
                        ChatMessageType.Notice, Color.FromName("Purple", Strings.Colors.presets));
                }
                return false;
            }

            var champ = NpcBase.Get(descriptor.ChampionId);
            if (champ == default)
            {
                return false;
            }

            if (AwaitingChampions.Contains(npcId))
            {
                return false;
            }

            if (ChampionCooldowns.TryGetValue(npcId, out var cooldown) && cooldown > Timing.Global.Milliseconds)
            {
                return false;
            }

            if (ActiveChampions.Contains(championId))
            {
                return false;
            }

            var luck = 1 + killer?.GetBonusEffectTotal(EffectType.Luck) / 100f;

            // Lucky enough for a champ spawn?
            var randomChance = Randomization.Next(1, 100001);
            if (randomChance >= (descriptor.ChampionSpawnChance * 1000) * luck)
            {
                return false;
            }
            
            AwaitingChampions.Add(npcId);

            return true;
        }

        public void RemoveActiveChampion(Guid npcId, bool killedByPlayer)
        {
            ActiveChampions.Remove(npcId);

            if (killedByPlayer)
            {
                if (ActiveChampions.Count <= 0)
                {
                    PacketSender.SendGlobalMsg($"A champion has been slain! ({mMapController.Name})", Color.FromName("Purple", Strings.Colors.presets));
                }
                else
                {
                    PacketSender.SendGlobalMsg($"One of the champions on {mMapController.Name} has been slain!", Color.FromName("Purple", Strings.Colors.presets));
                }
            }
        }

        public void SpawnChampion(Guid baseId, Guid championId, long cooldown)
        {
            AwaitingChampions.Remove(baseId); // allow respawning of this enemy without champion
            ChampionCooldowns[baseId] = Timing.Global.Milliseconds + (cooldown * 1000); // Trigger cooldown
            
            ActiveChampions.Add(championId); // don't allow another champion to spawn
        }
    }
    #endregion
}
