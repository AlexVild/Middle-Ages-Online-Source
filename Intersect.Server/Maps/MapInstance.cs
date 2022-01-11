﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Intersect.Compression;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.Logging;
using Intersect.Network.Packets.Server;
using Intersect.Server.Classes.Maps;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Networking;
using Intersect.Utilities;

using Newtonsoft.Json;

namespace Intersect.Server.Maps
{

    public partial class MapInstance : MapBase
    {
        private ConcurrentDictionary<Guid, MapProcessingLayer> mMapProcessingLayers = new ConcurrentDictionary<Guid, MapProcessingLayer>();

        private static MapInstances sLookup;

        [NotMapped] private readonly ConcurrentDictionary<Guid,Entity> mEntities = new ConcurrentDictionary<Guid, Entity>();

        private Entity[] mCachedEntities = new Entity[0];

        [JsonIgnore] [NotMapped]
        public ConcurrentDictionary<EventBase, Event> GlobalEventInstances = new ConcurrentDictionary<EventBase, Event>();

        [JsonIgnore] [NotMapped] public ConcurrentDictionary<Guid, MapItemSpawn> ItemRespawns = new ConcurrentDictionary<Guid, MapItemSpawn>();

        [JsonIgnore] [NotMapped] public long LastUpdateTime = -1;

        [JsonIgnore] [NotMapped] public long UpdateQueueStart = -1;

        //Location of Map in the current grid
        [JsonIgnore] [NotMapped] public int MapGrid;

        [JsonIgnore] [NotMapped] public int MapGridX = -1;

        [JsonIgnore] [NotMapped] public int MapGridY = -1;

        private ConcurrentDictionary<Guid, Player> mPlayers = new ConcurrentDictionary<Guid, Player>();

        //Temporary Values
        private Guid[] mSurroundingMapIds = new Guid[0];
        private Guid[] mSurroundingMapsIdsWithSelf = new Guid[0];
        private MapInstance[] mSurroundingMaps = new MapInstance[0];
        private MapInstance[] mSurroundingMapsWithSelf = new MapInstance[0];

        [JsonIgnore]
        [NotMapped]
        public Guid[] SurroundingMapIds
        {
            get => mSurroundingMapIds;

            set
            {
                lock (GetMapLock())
                {
                    mSurroundingMapIds = value;
                    var surroundingMapsIdsWithSelf = new List<Guid>(value);
                    surroundingMapsIdsWithSelf.Add(Id);
                    mSurroundingMapsIdsWithSelf = surroundingMapsIdsWithSelf.ToArray();
                }
            }
        }

        [JsonIgnore]
        [NotMapped]
        public MapInstance[] SurroundingMaps
        {
            get => mSurroundingMaps;

            set
            {
                lock (GetMapLock())
                {
                    mSurroundingMaps = value;
                    var surroundingMapsWithSelf = new List<MapInstance>(value);
                    surroundingMapsWithSelf.Add(this);
                    mSurroundingMapsWithSelf = surroundingMapsWithSelf.ToArray();
                }
            }
        }

        //EF
        public MapInstance() : base()
        {
            Name = "New Map";
            Layers = null;
        }

        //For New Maps!
        public MapInstance(bool newMap = false) : base(Guid.NewGuid())
        {
            Name = "New Map";
            Layers = null;
        }

        [JsonConstructor]
        public MapInstance(Guid id) : base(id)
        {
            if (id == Guid.Empty)
            {
                return;
            }

            Layers = null;
        }

        [JsonIgnore]
        [NotMapped]
        public ConcurrentDictionary<Guid, MapItem>[] TileItems { get; } = new ConcurrentDictionary<Guid, MapItem>[Options.Instance.MapOpts.Width * Options.Instance.MapOpts.Height];

        [JsonIgnore]
        [NotMapped]
        public ConcurrentDictionary<Guid, MapItem> AllMapItems { get; } = new ConcurrentDictionary<Guid, MapItem>();

        public new static MapInstances Lookup => sLookup = sLookup ?? new MapInstances(MapBase.Lookup);

        //GameObject Functions

        public object GetMapLock()
        {
            return mMapLock;
        }

        public override void Load(string json, bool keepCreationTime = true)
        {
            Load(json, -1);
        }

        public void Initialize()
        {
            lock (mMapLock)
            {
                DespawnEverything();
                RespawnEverything();

                var events = new List<EventBase>();
                foreach (var evt in EventIds)
                {
                    var itm = EventBase.Get(evt);
                    if (itm != null)
                    {
                        events.Add(itm);
                    }
                }
                EventsCache = events;
                foreach (var kv in mMapProcessingLayers)
                {
                    kv.Value.Initialize();
                }
            }
        }

        public void Load(string json, int keepRevision = -1)
        {
            lock (mMapLock)
            {
                DespawnEverything();
                base.Load(json);
                if (keepRevision > -1)
                {
                    Revision = keepRevision;
                }
            }
        }

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
        public void SpawnItem(int x, int y, Item item, int amount, Guid owner, bool sendUpdate = true)
        {
            if (item == null)
            {
                Log.Warn($"Tried to spawn {amount} of a null item at ({x}, {y}) in map {Id}.");

                return;
            }

            var itemDescriptor = ItemBase.Get(item.ItemId);
            if (itemDescriptor == null)
            {
                Log.Warn($"No item found for {item.ItemId}.");

                return;
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

                var mapItem = new MapItem(item.ItemId, amount + existingCount, x, y, item.BagId, item.Bag)
                {
                    DespawnTime = Timing.Global.Milliseconds + Options.Loot.ItemDespawnTime,
                    Owner = owner,
                    OwnershipTime = Timing.Global.Milliseconds + Options.Loot.ItemOwnershipTime,
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
                        PacketSender.SendMapItemUpdate(Id, reItem, true);
                    }
                }

                // Drop the new item.
                AddItem(mapItem);
                if (sendUpdate)
                {
                    PacketSender.SendMapItemUpdate(Id, mapItem, false);
                }
            }
            else
            {
                // Oh boy here we go! Set quantity to 1 and drop multiple!
                for (var i = 0; i < amount; i++)
                {
                    var mapItem = new MapItem(item.ItemId, amount, x, y, item.BagId, item.Bag) {
                        DespawnTime = Globals.Timing.Milliseconds + Options.Loot.ItemDespawnTime,
                        Owner = owner,
                        OwnershipTime = Globals.Timing.Milliseconds + Options.Loot.ItemOwnershipTime,
                        VisibleToAll = Options.Loot.ShowUnownedItems || owner == Guid.Empty
                    };

                    // If this is a piece of equipment, set up the stat buffs for it.
                    if (itemDescriptor.ItemType == ItemTypes.Equipment)
                    {
                        mapItem.SetupStatBuffs(item);
                    }

                    if (mapItem.TileIndex > Options.MapHeight * Options.MapWidth || mapItem.TileIndex < 0)
                    {
                        return;
                    }

                    AddItem(mapItem);
                }
                PacketSender.SendMapItemsToProximity(Id);
            }
        }

        private void SpawnAttributeItem(int x, int y)
        {
            var item = ItemBase.Get(((MapItemAttribute) Attributes[x, y]).ItemId);
            if (item != null)
            {
                var mapItem = new MapItem(item.Id, ((MapItemAttribute)Attributes[x, y]).Quantity, x, y);
                mapItem.DespawnTime = -1;
                mapItem.AttributeSpawnX = x;
                mapItem.AttributeSpawnY = y;
                if (item.ItemType == ItemTypes.Equipment)
                {
                    mapItem.Quantity = 1;
                }
                AddItem(mapItem);
                PacketSender.SendMapItemUpdate(Id, mapItem, false);
            }
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

        public void RemoveItem(MapItem item, bool respawn = true)
        {
            if (item != null)
            {
                // Only try to handle respawns for items that have attribute spawn locations.
                if (item.AttributeSpawnX > -1)
                {
                    if (respawn)
                    {
                        var spawn = new MapItemSpawn()
                        {
                            AttributeSpawnX = item.X,
                            AttributeSpawnY = item.Y,
                            RespawnTime = Globals.Timing.Milliseconds + Options.Map.ItemAttributeRespawnTime
                        };
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
                PacketSender.SendMapItemUpdate(Id, item, true, item.VisibleToAll, oldOwner);
            }
        }

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

        public void DespawnNpcsOfAcrossLayers(NpcBase npcBase)
        {
            foreach (var entity in GetEntitiesOnAllLayers())
            {
                if (entity is Npc npc && npc.Base == npcBase)
                {
                    lock (npc.EntityLock)
                    {
                        npc.Die(false);
                    }
                }
            }
        }

        public void DespawnResourcesOfAcrossLayers(ResourceBase resourceBase)
        {
            foreach (var entity in GetEntitiesOnAllLayers())
            {
                if (entity is Resource res && res.Base == resourceBase)
                {
                    lock (res.EntityLock)
                    {
                        res.Die(false);
                    }
                }
            }
        }

        public void DespawnProjectilesOfAcrossLayers(ProjectileBase projectileBase)
        {
            var guids = new List<Guid>();
            foreach (var entity in GetEntitiesOnAllLayers())
            {
                if (entity is Projectile proj && proj.Base == projectileBase)
                {
                    lock (proj.EntityLock)
                    {
                        guids.Add(proj.Id);
                        proj.Die(false);
                    }
                }
            }
            PacketSender.SendRemoveProjectileSpawnsFromAllLayers(Id, guids.ToArray(), null);
        }

        public void DespawnItemsOf(ItemBase itemBase)
        {
            foreach (var item in AllMapItems.Values)
            {
                if (ItemBase.Get(item.ItemId) == itemBase)
                {
                    RemoveItem(item);
                }
            }
        }

        //Events
        private void SpawnGlobalEvents()
        {
            GlobalEventInstances.Clear();
            foreach (var id in EventIds)
            {
                var evt = EventBase.Get(id);
                if (evt != null && evt.Global)
                {
                    GlobalEventInstances.TryAdd(evt, new Event(evt.Id, evt, this));
                }
            }
        }

        private void DespawnGlobalEvents()
        {
            //Kill global events on map (make sure processing stops for online players)
            //Gonna rely on GC for now
            var players = new List<Player>();
            foreach (var map in GetSurroundingMaps(true))
            {
                players.AddRange(map.GetPlayersOnMap().ToArray());
            }

            foreach (var evt in GlobalEventInstances.ToArray())
            {
                foreach (var player in players)
                {
                    player.RemoveEvent(evt.Value.BaseEvent.Id);
                }
            }

            GlobalEventInstances.Clear();
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

        //Entity Processing
        public void AddEntity(Entity en)
        {
            if (en != null && !en.IsDead())
            {
                if (!mEntities.ContainsKey(en.Id))
                {
                    mEntities.TryAdd(en.Id, en);
                    if (en is Player plyr)
                    {
                        mPlayers.TryAdd(plyr.Id, plyr);
                    }
                    mCachedEntities = mEntities.Values.ToArray();
                }
            }
        }

        public void RemoveEntity(Entity en)
        {
            mEntities.TryRemove(en.Id, out var result);
            if (mPlayers.ContainsKey(en.Id))
            {
                mPlayers.TryRemove(en.Id, out var pResult);
            }
            mCachedEntities = mEntities.Values.ToArray();
        }

        //Update + Related Functions
        public void Update(long timeMs)
        {
            lock (GetMapLock())
            {
                var surrMaps = GetSurroundingMaps(true);

                //Process Items
                foreach (var mapItem in AllMapItems.Values)
                {
                    // Should this item be visible to everyone now?
                    if (!mapItem.VisibleToAll && mapItem.OwnershipTime < timeMs)
                    {
                        mapItem.VisibleToAll = true;
                        PacketSender.SendMapItemUpdate(Id, mapItem, false);
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

                // Keep a list of all entities with changed vitals and statusses.
                var vitalUpdates = new List<Entity>();
                var statusUpdates = new List<Entity>();

                //Process All Entites
                foreach (var en in mEntities)
                {
                    //Let's see if and how long this map has been inactive, if longer than 30 seconds, regenerate everything on the map
                    //TODO, take this 30 second value and throw it into the server config after I switch everything to json
                    if (timeMs > LastUpdateTime + 30000)
                    {
                        //Regen Everything & Forget Targets
                        if (en.Value is Resource)
                        {
                            en.Value.RestoreVital(Vitals.Health);
                            en.Value.RestoreVital(Vitals.Mana);
                            en.Value.Target = null;
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

                //Process all global events
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
                        foreach (var map in GetSurroundingMaps(true))
                        {
                            foreach (var player in map.GetPlayersOnMap())
                            {
                                var eventInstance = player.FindGlobalEventInstance(evts[i].GlobalPageInstance[x]);
                                if (eventInstance != null && eventInstance.CallStack.Count > 0)
                                {
                                    active = true;
                                }
                            }
                        }

                        evts[i].GlobalPageInstance[x].Update(active, timeMs);
                    }
                }

                UpdateProcessingInstances(Globals.Timing.Milliseconds);

                LastUpdateTime = timeMs;
            }
        }

        public void UpdateProjectiles(long timeMs)
        {
            // TODO Alex I don't think this will be necessary once processing layers are handled in their own jobs
            mMapProcessingLayers.Values.ToList().ForEach((mpl) =>
            {
                mpl.UpdateProjectiles(timeMs);
            });
        }

        public MapInstance[] GetSurroundingMaps(bool includingSelf = false)
        {
            return includingSelf ? mSurroundingMapsWithSelf : mSurroundingMaps;
        }

        public Guid[] GetSurroundingMapIds(bool includingSelf = false)
        {
            return includingSelf ? mSurroundingMapsIdsWithSelf : mSurroundingMapIds;
        }

        public ConcurrentDictionary<Guid, Entity> GetLocalEntities()
        {
            return mEntities;
        }

        public List<Entity> GetEntities(bool includeSurroundingMaps = false)
        {
            var entities = new List<Entity>();

            foreach (var en in mEntities)
                entities.Add(en.Value);

            // ReSharper disable once InvertIf
            if (includeSurroundingMaps)
            {
                foreach (var map in GetSurroundingMaps(false))
                {
                    entities.AddRange(map.GetEntities());
                }
            }

            return entities;
        }

        public Entity[] GetCachedEntities()
        {
            return mCachedEntities;
        }

        public ICollection<Player> GetPlayersOnMap()
        {
            return GetPlayersOnAllLayers(); // TODO Alex: Players are no longer held here
        }

        public ICollection<Entity> GetEntitiesOnAllLayers()
        {
            var entities = new List<Entity>();
            foreach (var mpl in mMapProcessingLayers)
            {
                entities.AddRange(mpl.Value.GetEntities());
            }
            return entities;
        }

        public Entity[] GetCachedEntitiesOnAllLayers()
        {
            var entities = new List<Entity>();
            foreach (var mpl in mMapProcessingLayers)
            {
                entities.AddRange(mpl.Value.GetCachedEntities());
            }
            return entities.ToArray();
        }

        // TODO Alex: I think this method will eventually be invalidated
        public ICollection<Player> GetPlayersOnAllLayers()
        {
            var players = new List<Player>();
            foreach (var layer in mMapProcessingLayers.Keys.ToList())
            {
                players.AddRange(mMapProcessingLayers[layer].GetPlayersOnMap());
            }
            return players;
        }

        public void ClearConnections(int side = -1)
        {
            if (side == -1 || side == (int) Directions.Up)
            {
                Up = Guid.Empty;
            }

            if (side == -1 || side == (int) Directions.Down)
            {
                Down = Guid.Empty;
            }

            if (side == -1 || side == (int) Directions.Left)
            {
                Left = Guid.Empty;
            }

            if (side == -1 || side == (int) Directions.Right)
            {
                Right = Guid.Empty;
            }

            DbInterface.SaveGameObject(this);
        }

        //Map Reseting Functions
        public void DespawnEverything()
        {
            foreach (var kv in mMapProcessingLayers)
            {
                kv.Value.DespawnEverything();
            }
            DespawnItems();
            DespawnGlobalEvents();
        }

        public void RespawnEverything()
        {
            foreach (var kv in mMapProcessingLayers)
            {
                kv.Value.RespawnEverything();
            }
            SpawnGlobalEvents();
        }

        public static MapInstance Get(Guid id)
        {
            return Lookup.Get<MapInstance>(id);
        }

        public void DestroyOrphanedLayers()
        {
            if (Layers == null && TileData != null)
            {
                Layers = JsonConvert.DeserializeObject<Dictionary<string, Tile[,]>>(LZ4.UnPickleString(TileData), mJsonSerializerSettings);
                foreach (var key in Layers.Keys.ToArray())
                {
                    if (!Options.Instance.MapOpts.Layers.All.Contains(key))
                    {
                        Layers.Remove(key);
                    }
                }
                TileData = LZ4.PickleString(JsonConvert.SerializeObject(Layers, Formatting.None, mJsonSerializerSettings));
                Layers = null;
                
            }
        }

        public override void Delete()
        {
            Lookup?.Delete(this);
        }

        public Dictionary<MapInstance, List<int>> FindSurroundingTiles(Point location, int distance)
        {
            // Loop through all locations surrounding us to get valid tiles.
            var locations = new Dictionary<MapInstance, List<int>>();
            for (var x = 0 - distance; x <= distance; x++)
            {
                for (var y = 0 - distance; y <= distance; y++)
                {
                    // Use these to keep track of our translation.
                    var currentMap = this;
                    var currentX = location.X + x;
                    var currentY = location.Y + y;

                    // Are we on a valid map at all?
                    if (currentMap == null)
                    {
                        break;
                    }

                    // Are we going to the map on our left?
                    if (currentX < 0)
                    {
                        var oldMap = currentMap;
                        if (currentMap.Left != Guid.Empty)
                        {
                            currentMap = MapInstance.Get(currentMap.Left);
                            if (currentMap == null)
                            {
                                currentMap = oldMap;
                                continue;
                            }

                            currentX = (Options.MapWidth + 1) + x;
                        }
                    }

                    // Are we going to the map on our right?
                    if (currentX >= Options.MapWidth)
                    {
                        var oldMap = currentMap;
                        if (currentMap.Right != Guid.Empty)
                        {
                            currentMap = MapInstance.Get(currentMap.Right);
                            if (currentMap == null)
                            {
                                currentMap = oldMap;
                                continue;
                            }

                            currentX = -1 + x;
                        }
                    }

                    // Are we going to the map up from us?
                    if (currentY < 0)
                    {
                        var oldMap = currentMap;
                        if (currentMap.Up != Guid.Empty)
                        {
                            currentMap = MapInstance.Get(currentMap.Up);
                            if (currentMap == null)
                            {
                                currentMap = oldMap;
                                continue;
                            }

                            currentY = (Options.MapHeight + 1) + y;
                        }
                    }

                    // Are we going to the map down from us?
                    if (currentY >= Options.MapHeight)
                    {
                        var oldMap = currentMap;
                        if (currentMap.Down != Guid.Empty)
                        {
                            currentMap = MapInstance.Get(currentMap.Down);
                            if (currentMap == null)
                            {
                                currentMap = oldMap;
                                continue;
                            }

                            currentY = -1 + y;
                        }
                    }

                    if (currentX < 0 || currentY < 0 || currentX >= Options.MapWidth || currentY >= Options.MapHeight)
                    {
                        continue;
                    }

                    if (!locations.ContainsKey(currentMap))
                    {
                        locations.Add(currentMap, new List<int>());
                    }
                    locations[currentMap].Add(currentY * Options.MapWidth + currentX);
                }
            }

            return locations;
        }

        /// <summary>
        /// Creates a processing layer
        /// </summary>
        /// <param name="processingLayerId"></param>
        /// <returns>Whether or not we needed to create a new processing layer</returns>
        public bool TryCreateProcessingInstance(Guid processingLayerId, out MapProcessingLayer newLayer)
        {
            newLayer = null;
            lock (GetMapLock())
            {
                if (!mMapProcessingLayers.ContainsKey(processingLayerId))
                {
                    Log.Debug($"Creating new processing layer {processingLayerId} for map {Name}");
                    mMapProcessingLayers[processingLayerId] = new MapProcessingLayer(this, processingLayerId);
                    newLayer = mMapProcessingLayers[processingLayerId];
                    return true;
                }

                return false;
            }
        }

        public bool TryUpdateProcessingLayer(Guid instanceId, long timeMs)
        {
            if (mMapProcessingLayers.TryGetValue(instanceId, out var mpl))
            {
                mpl.Update(timeMs);
                return true;
            } else
            {
                return false;
            }
        }

        public void UpdateProcessingInstances(long timeMs)
        {
            foreach (var instance in mMapProcessingLayers.Keys.ToList())
            {
                if (mMapProcessingLayers.TryGetValue(instance, out var mpl))
                {
                    mpl.Update(timeMs);
                }
            }
        }

        public bool TryGetRelevantProcessingLayer(Guid instanceLayer, out MapProcessingLayer mpl, bool createIfNew = false)
        {
            mpl = null;
            if (mMapProcessingLayers.TryGetValue(instanceLayer, out var processingLayer))
            {
                mpl = processingLayer;
                return true;
            }
            else
            {
                if (createIfNew)
                {
                    if (TryCreateProcessingInstance(instanceLayer, out var newProcessingLayer))
                    {
                        mpl = newProcessingLayer;
                        return true;
                    }
                }
            }
            return false;
        }

        public void RemoveEntityFromAllRelevantMapsInLayer(Entity entity, Guid instanceLayer)
        {
            foreach(var map in GetSurroundingMaps(true))
            {
                map?.RemoveEntityFromAllMapsInLayer(entity, instanceLayer);
            }
        }

        // TODO Alex rename
        public void RemoveEntityFromAllMapsInLayer(Entity entity, Guid instanceLayer)
        {
            if (TryGetRelevantProcessingLayer(instanceLayer, out var mapProcessingLayer))
            {
                mapProcessingLayer.RemoveEntity(entity);
            }
        }

        public void RemoveDeadProcessingLayers()
        {
            lock (GetMapLock())
            {
                // Removes all processing layers that don't have active players on themselves or any adjoining layers
                var deadLayers = mMapProcessingLayers.Where(kv => kv.Value.GetAllRelevantPlayers().Count <= 0).ToList();
                var layerCountPreCleanup = mMapProcessingLayers.Keys.Count;
                foreach (var instance in deadLayers)
                {
                    if (mMapProcessingLayers.TryRemove(instance.Key, out var removedLayer))
                    {
                        removedLayer.Dispose();
                        Log.Debug($"Cleaning up MPL {instance} for map: {Name}");
                    }
                }

                if (layerCountPreCleanup != mMapProcessingLayers.Keys.Count)
                {
                    Log.Debug($"There are now {mMapProcessingLayers.Keys.Count} layer(s) remaining for map: {Name}");
                }
            }
        }

        public List<Player> GetPlayersOnSharedLayers(Guid instanceLayer, Entity except)
        {
            var entitiesOnSharedLayer = new List<Player>();

            if (mMapProcessingLayers.TryGetValue(instanceLayer, out var layer))
            {
                foreach (var player in layer.GetPlayersOnMap())
                {
                    if (player != except && player.InstanceLayer == instanceLayer)
                    {
                        entitiesOnSharedLayer.Add(player);
                    }
                }
            }

            return entitiesOnSharedLayer;
        }

        public void ClearAllProcessingLayers()
        {
            mMapProcessingLayers.Clear();
        }
    }
}
