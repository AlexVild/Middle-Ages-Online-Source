using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Entities.Events;
using Intersect.Client.Entities.Projectiles;
using Intersect.Client.General;
using Intersect.Client.Interface.Game;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Menu;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Maps;
using Intersect.Core;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Maps.MapList;
using Intersect.Logging;
using Intersect.Network;
using Intersect.Network.Packets;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.GameObjects.Timers;
using Newtonsoft.Json.Linq;
using Intersect.Client.General.Leaderboards;
using Intersect.Client.Interface.ScreenAnimations;
using Intersect.Client.Entities.CombatNumbers;
using Intersect.Client.Interface.Objects;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Interface.Game.Toasts;
using Intersect.Client.General.Bestiary;
using Intersect.Client.Interface.Game.WeaponPicker;

namespace Intersect.Client.Networking
{

    internal sealed partial class PacketHandler
    {
        private sealed class VirtualPacketSender : IPacketSender
        {
            public IApplicationContext ApplicationContext { get; }

            public VirtualPacketSender(IApplicationContext applicationContext) =>
                ApplicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));

            #region Implementation of IPacketSender

            /// <inheritdoc />
            public bool Send(IPacket packet)
            {
                if (packet is IntersectPacket intersectPacket)
                {
                    Network.SendPacket(intersectPacket);
                    return true;
                }

                return false;
            }

            #endregion
        }

        public long Ping { get; set; } = 0;

        public long PingTime { get; set; }

        public IClientContext Context { get; }

        public Logger Logger => Context.Logger;

        public PacketHandlerRegistry Registry { get; }

        public IPacketSender VirtualSender { get; }

        public PacketHandler(IClientContext context, PacketHandlerRegistry packetHandlerRegistry)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Registry = packetHandlerRegistry ?? throw new ArgumentNullException(nameof(packetHandlerRegistry));

            if (!Registry.TryRegisterAvailableMethodHandlers(GetType(), this, false) || Registry.IsEmpty)
            {
                throw new InvalidOperationException("Failed to register method handlers, see logs for more details.");
            }

            VirtualSender = new VirtualPacketSender(context);
        }

        public bool HandlePacket(IPacket packet)
        {
            if (packet is AbstractTimedPacket timedPacket)
            {
                Timing.Global.Synchronize(timedPacket.UTC, timedPacket.Offset);
            }

            if (!(packet is IntersectPacket))
            {
                return false;
            }

            if (!packet.IsValid)
            {
                return false;
            }

            if (!Registry.TryGetHandler(packet, out HandlePacketGeneric handler))
            {
                Logger.Error($"No registered handler for {packet.GetType().FullName}!");

                return false;
            }

            if (Registry.TryGetPreprocessors(packet, out var preprocessors))
            {
                if (!preprocessors.All(preprocessor => preprocessor.Handle(VirtualSender, packet)))
                {
                    // Preprocessors are intended to be silent filter functions
                    return false;
                }
            }

            if (Registry.TryGetPreHooks(packet, out var preHooks))
            {
                if (!preHooks.All(hook => hook.Handle(VirtualSender, packet)))
                {
                    // Hooks should not fail, if they do that's an error
                    Logger.Error($"PreHook handler failed for {packet.GetType().FullName}.");
                    return false;
                }
            }

            if (!handler(VirtualSender, packet))
            {
                return false;
            }

            if (Registry.TryGetPostHooks(packet, out var postHooks))
            {
                if (!postHooks.All(hook => hook.Handle(VirtualSender, packet)))
                {
                    // Hooks should not fail, if they do that's an error
                    Logger.Error($"PostHook handler failed for {packet.GetType().FullName}.");
                    return false;
                }
            }

            return true;
        }

        //PingPacket
        public void HandlePacket(IPacketSender packetSender, PingPacket packet)
        {
            if (packet.RequestingReply)
            {
                PacketSender.SendPing();
                PingTime = Timing.Global.Milliseconds;
            }
            else
            {
                Network.Ping = (int)(Timing.Global.Milliseconds - PingTime) / 2;
            }
        }

        //ConfigPacket
        public void HandlePacket(IPacketSender packetSender, ConfigPacket packet)
        {
            Options.LoadFromServer(packet.Config);
            Graphics.InitInGame();
        }

        //JoinGamePacket
        public void HandlePacket(IPacketSender packetSender, JoinGamePacket packet)
        {
            Main.JoinGame();
            Globals.JoiningGame = true;
        }

        //MapAreaPacket
        public void HandlePacket(IPacketSender packetSender, MapAreaPacket packet)
        {
            foreach (var map in packet.Maps)
            {
                HandleMap(packetSender, map);
            }
        }

        //MapPacket
        private void HandleMap(IPacketSender packetSender, MapPacket packet)
        {
            var mapId = packet.MapId;
            var map = MapInstance.Get(mapId);
            if (map != null)
            {
                if (packet.Revision == map.Revision)
                {
                    return;
                }
                else
                {
                    map.Dispose(false, false);
                }
            }

            map = new MapInstance(mapId);
            MapInstance.Lookup.Set(mapId, map);
            lock (map.MapLock)
            {
                map.Load(packet.Data);
                map.LoadTileData(packet.TileData);
                map.AttributeData = packet.AttributeData;
                map.CreateMapSounds();
                if (mapId == Globals.Me.CurrentMap)
                {
                    Audio.PlayMusic(map.Music, 6f, 10f, true);
                }

                map.MapGridX = packet.GridX;
                map.MapGridY = packet.GridY;
                map.CameraHolds = packet.CameraHolds;
                map.Autotiles.InitAutotiles(map.GenerateAutotileGrid());

                //Process Entities and Items if provided in this packet
                if (packet.MapEntities != null)
                {
                    HandlePacket(packet.MapEntities);
                }

                if (packet.MapItems != null)
                {
                    HandlePacket(packet.MapItems);
                }

                map.DisposeTraps();
                if (packet.MapTrapPackets.Count > 0)
                {
                    foreach (var trapPacket in packet.MapTrapPackets)
                    {
                        HandlePacket(trapPacket);
                    }
                }

                if (Globals.PendingEvents.ContainsKey(mapId))
                {
                    foreach (var evt in Globals.PendingEvents[mapId])
                    {
                        map.AddEvent(evt.Key, evt.Value);
                    }

                    Globals.PendingEvents[mapId].Clear();
                }
            }

            MapInstance.OnMapLoaded?.Invoke(map);
        }

        //MapPacket
        public void HandlePacket(IPacketSender packetSender, MapPacket packet)
        {
            HandleMap(packetSender, packet);
            Globals.Me.FetchNewMaps();
        }

        //PlayerEntityPacket
        public void HandlePacket(IPacketSender packetSender, PlayerEntityPacket packet)
        {
            var en = Globals.GetEntity(packet.EntityId, EntityTypes.Player);
            if (en != null)
            {
                en.Load(packet);
                if (packet.IsSelf)
                {
                    Globals.Me = (Player)Globals.Entities[packet.EntityId];
                }
            }
            else
            {
                Globals.Entities.Add(packet.EntityId, new Player(packet.EntityId, packet));
                if (packet.IsSelf)
                {
                    Globals.Me = (Player)Globals.Entities[packet.EntityId];
                }
            }
        }

        //NpcEntityPacket
        public void HandlePacket(IPacketSender packetSender, NpcEntityPacket packet)
        {
            var en = Globals.GetEntity(packet.EntityId, EntityTypes.GlobalEntity);
            if (en != null)
            {
                en.Load(packet);
                en.Type = packet.Aggression;
                en.NpcId = packet.NpcId;
            }
            else
            {
                Globals.Entities.Add(packet.EntityId, new Entity(packet.EntityId, packet));
                Globals.Entities[packet.EntityId].Type = packet.Aggression;
                Globals.Entities[packet.EntityId].NpcId = packet.NpcId;
            }
        }

        //ResourceEntityPacket
        public void HandlePacket(IPacketSender packetSender, ResourceEntityPacket packet)
        {
            var en = Globals.GetEntity(packet.EntityId, EntityTypes.Resource);
            if (en != null)
            {
                en.Load(packet);
            }
            else
            {
                Globals.Entities.Add(packet.EntityId, new Resource(packet.EntityId, packet));
            }
        }

        //ProjectileEntityPacket
        public void HandlePacket(IPacketSender packetSender, ProjectileEntityPacket packet)
        {
            var en = Globals.GetEntity(packet.EntityId, EntityTypes.Projectile);
            if (en != null)
            {
                en.Load(packet);
            }
            else
            {
                Globals.Entities.Add(packet.EntityId, new Projectile(packet.EntityId, packet));
            }
        }

        //EventEntityPacket
        public void HandlePacket(IPacketSender packetSender, EventEntityPacket packet)
        {
            var map = MapInstance.Get(packet.MapId);
            if (map != null)
            {
                map?.AddEvent(packet.EntityId, packet);
            }
            else
            {
                var dict = Globals.PendingEvents.ContainsKey(packet.MapId)
                    ? Globals.PendingEvents[packet.MapId]
                    : new Dictionary<Guid, EventEntityPacket>();

                if (dict.ContainsKey(packet.EntityId))
                {
                    dict[packet.EntityId] = packet;
                }
                else
                {
                    dict.Add(packet.EntityId, packet);
                }

                if (!Globals.PendingEvents.ContainsKey(packet.MapId))
                {
                    Globals.PendingEvents.Add(packet.MapId, dict);
                }
            }
        }

        //MapEntitiesPacket
        public void HandlePacket(IPacketSender packetSender, MapEntitiesPacket packet)
        {
            var mapEntities = new Dictionary<Guid, List<Guid>>();
            foreach (var pkt in packet.MapEntities)
            {
                HandlePacket(pkt);

                if (!mapEntities.ContainsKey(pkt.MapId))
                {
                    mapEntities.Add(pkt.MapId, new List<Guid>());
                }

                mapEntities[pkt.MapId].Add(pkt.EntityId);
            }

            //Remove any entities on the map that shouldn't be there anymore!
            foreach (var entities in mapEntities)
            {
                foreach (var entity in Globals.Entities)
                {
                    if (entity.Value.CurrentMap == entities.Key && !entities.Value.Contains(entity.Key))
                    {
                        if (!Globals.EntitiesToDispose.Contains(entity.Key) && entity.Value != Globals.Me && !(entity.Value is Projectile))
                        {
                            Globals.EntitiesToDispose.Add(entity.Key);
                        }
                    }
                }
            }
        }

        // MapInstanceChanged Packet
        public void HandlePacket(IPacketSender packetSender, MapInstanceChangedPacket packet)
        {
            var disposingEntities = new List<Guid>();
            foreach (var pkt in packet.EntitiesToDispose)
            {
                HandlePacket(pkt);
                disposingEntities.Add(pkt.EntityId);
            }

            foreach (var entity in disposingEntities)
            {
                Globals.EntitiesToDispose.Add(entity);
            }

            foreach (var mapId in packet.MapIdsToRefresh)
            {
                var map = MapInstance.Get(mapId);
                if (map != null)
                {
                    Globals.EntitiesToDispose.AddRange(map.LocalEntities.Values
                        .ToList()
                        .Select(en => en.Id));
                    map.DisposeTraps();
                }
            }
        }

        //EntityPositionPacket
        public void HandlePacket(IPacketSender packetSender, EntityPositionPacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            Entity en;
            if (type != EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                if (MapInstance.Get(mapId) == null)
                {
                    return;
                }

                if (!MapInstance.Get(mapId).LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = MapInstance.Get(mapId).LocalEntities[id];
            }

            if (en == Globals.Me)
            {
                Log.Debug($"received epp: {Timing.Global.Milliseconds}");
            }

            if (en == Globals.Me &&
                (Globals.Me.DashQueue.Count > 0 || Globals.Me.DashTimer > Timing.Global.Milliseconds))
            {
                return;
            }

            if (en == Globals.Me && Globals.Me.CurrentMap != mapId)
            {
                Globals.Me.CurrentMap = mapId;
                Globals.NeedsMaps = true;
                Globals.Me.FetchNewMaps();
            }
            else
            {
                en.CurrentMap = mapId;
            }

            en.X = packet.X;
            en.Y = packet.Y;
            if (en is Player playerEn && packet.FaceDirection >= 0)
            {
                playerEn.FaceDirection = (byte)packet.FaceDirection;
            }
            en.Dir = packet.Direction;
            en.Passable = packet.Passable;
            en.HideName = packet.HideName;
        }

        //EntityLeftPacket
        public void HandlePacket(IPacketSender packetSender, EntityLeftPacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            if (id == Globals.Me?.Id && type < EntityTypes.Event)
            {
                return;
            }

            if (type != EntityTypes.Event)
            {
                if (Globals.Entities?.ContainsKey(id) ?? false)
                {
                    Globals.Entities[id]?.Dispose();
                    Globals.EntitiesToDispose?.Add(id);
                }
            }
            else
            {
                var map = MapInstance.Get(mapId);
                if (map?.LocalEntities?.ContainsKey(id) ?? false)
                {
                    map.LocalEntities[id]?.Dispose();
                    map.LocalEntities[id] = null;
                    map.LocalEntities.Remove(id);
                }
            }
        }

        //ChatMsgPacket
        public void HandlePacket(IPacketSender packetSender, ChatMsgPacket packet)
        {
            if (packet.Type == ChatMessageType.Admin ||
                packet.Type == ChatMessageType.Error ||
                packet.Type == ChatMessageType.Friend ||
                packet.Type == ChatMessageType.Global ||
                packet.Type == ChatMessageType.Local ||
                packet.Type == ChatMessageType.PM ||
                packet.Type == ChatMessageType.Trading ||
                packet.Type == ChatMessageType.Quest)
            {
                Interface.Interface.GameUi?.NotifyChat();
            }

            ChatboxMsg.AddMessage(
                new ChatboxMsg(
                    packet.Message ?? "", new Color(packet.Color.A, packet.Color.R, packet.Color.G, packet.Color.B), packet.Type,
                    packet.Target
                )
            );
        }

        //AnnouncementPacket
        public void HandlePacket(IPacketSender packetSender, AnnouncementPacket packet)
        {
            Interface.Interface.GameUi.AnnouncementWindow.ShowAnnouncement(packet.Message, packet.Duration);
        }

        //ActionMsgPackets
        public void HandlePacket(IPacketSender packetSender, ActionMsgPackets packet)
        {
            foreach (var pkt in packet.Packets)
            {
                HandlePacket(pkt);
            }
        }

        //ActionMsgPacket
        public void HandlePacket(IPacketSender packetSender, ActionMsgPacket packet)
        {
            var map = MapInstance.Get(packet.MapId);
            if (map != null)
            {
                map.ActionMsgs.Add(
                    new ActionMessage(
                        map, packet.X, packet.Y, packet.Message,
                        new Color(packet.Color.A, packet.Color.R, packet.Color.G, packet.Color.B), false
                    )
                );

                if (!string.IsNullOrEmpty(packet.MapSound))
                {
                    Audio.AddMapSound(packet.MapSound, packet.X, packet.Y, map.Id, false, 0, 10);
                }
            }
        }

        //GameDataPacket
        public void HandlePacket(IPacketSender packetSender, GameDataPacket packet)
        {
            foreach (var pkt in packet.GameObjects)
            {
                HandlePacket(pkt);
            }

            // After we've loaded NPC data, request their kill counts to popualte the bestiary
            PacketSender.SendRequestKillCounts();
            Globals.RefreshNpcSpellScalars();

            CustomColors.Load(packet.ColorsJson);
            Globals.HasGameData = true;
        }

        //MapListPacket
        public void HandlePacket(IPacketSender packetSender, MapListPacket packet)
        {
            MapList.List.JsonData = packet.MapListData;
            MapList.List.PostLoad(MapBase.Lookup, false, true);

            //TODO ? If admin window is open update it
        }

        //EntityMovementPackets
        public void HandlePacket(IPacketSender packetSender, EntityMovementPackets packet)
        {
            if (packet.GlobalMovements != null)
            {
                foreach (var pkt in packet.GlobalMovements)
                {
                    HandlePacket(pkt);
                }
            }

            if (packet.LocalMovements != null)
            {
                foreach (var pkt in packet.LocalMovements)
                {
                    HandlePacket(pkt);
                }
            }
        }

        //EntityMovePacket
        public void HandlePacket(IPacketSender packetSender, EntityMovePacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            Entity en;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var gameMap = MapInstance.Get(mapId);
                if (gameMap == null)
                {
                    return;
                }

                if (!gameMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = gameMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }

            var entityMap = MapInstance.Get(en.CurrentMap);
            if (entityMap == null)
            {
                return;
            }

            if (en.Dashing != null || en.DashQueue.Count > 0)
            {
                return;
            }

            var map = mapId;
            var x = packet.X;
            var y = packet.Y;
            var dir = packet.Direction;
            var correction = packet.Correction;
            if ((en.CurrentMap != map || en.X != x || en.Y != y) &&
                (en != Globals.Me || en == Globals.Me && correction) &&
                en.Dashing == null)
            {
                en.CurrentMap = map;
                en.X = x;
                en.Y = y;
                en.Dir = dir;
                if (en is Player p)
                {
                    p.MoveDir = dir;
                    p.CombatMode = packet.CombatMode;
                    if (p.CombatMode)
                    {
                        p.FaceDirection = packet.FaceDirection;
                    }
                }
                en.IsMoving = true;

                switch (en.Dir)
                {
                    case 0:
                        en.OffsetY = Options.TileWidth;
                        en.OffsetX = 0;

                        break;
                    case 1:
                        en.OffsetY = -Options.TileWidth;
                        en.OffsetX = 0;

                        break;
                    case 2:
                        en.OffsetY = 0;
                        en.OffsetX = Options.TileWidth;

                        break;
                    case 3:
                        en.OffsetY = 0;
                        en.OffsetX = -Options.TileWidth;

                        break;
                }
            }

            // Set the Z-Dimension if the player has moved up or down a dimension.
            if (entityMap.Attributes[en.X, en.Y] != null &&
                entityMap.Attributes[en.X, en.Y].Type == MapAttributes.ZDimension)
            {
                if (((MapZDimensionAttribute)entityMap.Attributes[en.X, en.Y]).GatewayTo > 0)
                {
                    en.Z = (byte)(((MapZDimensionAttribute)entityMap.Attributes[en.X, en.Y]).GatewayTo - 1);
                }
            }
        }

        public void HandlePacket(IPacketSender packetSender, MapEntityVitalsPacket packet)
        {
            // Get our map, cancel out if it doesn't exist.
            var map = MapInstance.Get(packet.MapId);
            if (map == null)
            {
                return;
            }

            foreach (var en in packet.EntityUpdates)
            {
                Entity entity = null;

                if (en.Type < EntityTypes.Event)
                {
                    if (!Globals.Entities.ContainsKey(en.Id))
                    {
                        return;
                    }

                    entity = Globals.Entities[en.Id];
                }
                else
                {
                    if (!map.LocalEntities.ContainsKey(en.Id))
                    {
                        return;
                    }

                    entity = map.LocalEntities[en.Id];
                }

                if (entity == null)
                {
                    return;
                }

                entity.Vital = en.Vitals;
                entity.MaxVital = en.MaxVitals;

                if (entity == Globals.Me)
                {
                    if (en.CombatTimeRemaining > 0)
                    {
                        Globals.Me.CombatTimer = Timing.Global.Milliseconds + en.CombatTimeRemaining;
                    }
                }
            }
        }

        public void HandlePacket(IPacketSender packetSender, MapEntityStatusPacket packet)
        {
            // Get our map, cancel out if it doesn't exist.
            var map = MapInstance.Get(packet.MapId);
            if (map == null)
            {
                return;
            }

            foreach (var en in packet.EntityUpdates)
            {
                Entity entity = null;

                if (en.Type < EntityTypes.Event)
                {
                    if (!Globals.Entities.ContainsKey(en.Id))
                    {
                        return;
                    }

                    entity = Globals.Entities[en.Id];
                }
                else
                {
                    if (!map.LocalEntities.ContainsKey(en.Id))
                    {
                        return;
                    }

                    entity = map.LocalEntities[en.Id];
                }

                if (entity == null)
                {
                    return;
                }

                //Update status effects
                entity.Status.Clear();
                foreach (var status in en.Statuses)
                {
                    var instance = new Status(
                        status.SpellId, status.Type, status.TransformSprite, status.TimeRemaining, status.TotalDuration
                    );

                    entity.Status.Add(instance);
                    
                    if (instance.Type == StatusTypes.Shield)
                    {
                        instance.Shield = status.VitalShields;
                    }
                }

                entity.SortStatuses();

                if (Interface.Interface.GameUi != null && en.Id == Globals.Me.TargetIndex && Globals.Me.TargetBox != null)
                {
                    Globals.Me.TargetBox.UpdateStatuses = true;
                }
            }
        }

        //EntityVitalsPacket
        public void HandlePacket(IPacketSender packetSender, EntityVitalsPacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            Entity en = null;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var entityMap = MapInstance.Get(mapId);
                if (entityMap == null)
                {
                    return;
                }

                if (!entityMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = entityMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }

            en.Vital = packet.Vitals;
            en.MaxVital = packet.MaxVitals;

            if (en == Globals.Me)
            {
                if (packet.CombatTimeRemaining > 0)
                {
                    Globals.Me.CombatTimer = Timing.Global.Milliseconds + packet.CombatTimeRemaining;
                }
            }

            //Update status effects
            en.Status.Clear();
            foreach (var status in packet.StatusEffects)
            {
                var instance = new Status(
                    status.SpellId, status.Type, status.TransformSprite, status.TimeRemaining, status.TotalDuration
                );

                en.Status.Add(instance);

                if (instance.Type == StatusTypes.Shield)
                {
                    instance.Shield = status.VitalShields;
                }
            }

            en.SortStatuses();

            if (Interface.Interface.GameUi != null && id == Globals.Me.TargetIndex && Globals.Me.TargetBox != null)
            {
                Globals.Me.TargetBox.UpdateStatuses = true;
            }
        }

        //EntityStatsPacket
        public void HandlePacket(IPacketSender packetSender, EntityStatsPacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            Entity en = null;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var entityMap = MapInstance.Get(mapId);
                if (entityMap == null)
                {
                    return;
                }

                if (!entityMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = entityMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }

            en.Stat = packet.Stats;
        }

        //PlayerStatsPacket
        public void HandlePacket(IPacketSender packetSender, PlayerStatsPacket packet)
        {
            var id = packet.Id;
            Entity player = null;

            if (!Globals.Entities.ContainsKey(id))
            {
                return;
            }

            player = Globals.Entities[id];
            if (player == null)
            {
                return;
            }

            player.Stat = packet.Stats;
            player.TrueStats = packet.TrueStats;
            player.ActivePassives = packet.ActivePassives?.ToList() ?? new List<Guid>();
        }

        //EntityDirectionPacket
        public void HandlePacket(IPacketSender packetSender, EntityDirectionPacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            Entity en = null;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var entityMap = MapInstance.Get(mapId);
                if (entityMap == null)
                {
                    return;
                }

                if (!entityMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = entityMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }


            if (en is Player player && player.CombatMode)
            {
                player.FaceDirection = packet.Direction;
            }
            else
            {
                en.Dir = packet.Direction;
            }
        }

        //EntityAttackPacket
        public void HandlePacket(IPacketSender packetSender, EntityAttackPacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;
            var attackTimer = packet.AttackTimer;

            Entity en = null;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var entityMap = MapInstance.Get(mapId);
                if (entityMap == null)
                {
                    return;
                }

                if (!entityMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = entityMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }

            if (attackTimer > -1 && en != Globals.Me)
            {
                en.AttackTimer = Timing.Global.Ticks / TimeSpan.TicksPerMillisecond + attackTimer;
                en.AttackTime = attackTimer;
            }
        }

        //EntityDiePacket
        public void HandlePacket(IPacketSender packetSender, EntityDiePacket packet)
        {
            var id = packet.Id;
            var type = packet.Type;
            var mapId = packet.MapId;

            Entity en = null;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var entityMap = MapInstance.Get(mapId);
                if (entityMap == null)
                {
                    return;
                }

                if (!entityMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = entityMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }

            en.ClearAnimations(null);

            if (Globals.Me.TargetIndex == id)
            {
                Globals.Me.ClearTarget(true, en.Id);
                if (Globals.Database.ChangeTargetOnDeath)
                {
                    Globals.Me.TryAutoTarget(false);
                }
            }
        }

        //EventDialogPacket
        public void HandlePacket(IPacketSender packetSender, EventDialogPacket packet)
        {
            var ed = new Dialog();
            ed.Prompt = packet.Prompt;
            ed.Face = packet.Face;
            if (packet.Type != 0)
            {
                ed.Opt1 = packet.Responses[0];
                ed.Opt2 = packet.Responses[1];
                ed.Opt3 = packet.Responses[2];
                ed.Opt4 = packet.Responses[3];
            }

            ed.EventId = packet.EventId;
            Interface.Interface.GameUi.SetDialogResponses(ed.Opt1, ed.Opt2, ed.Opt3, ed.Opt4);
            Globals.EventDialogs.Add(ed);
        }

        //InputVariablePacket
        public void HandlePacket(IPacketSender packetSender, InputVariablePacket packet)
        {
            var type = InputBox.InputType.NumericInput;
            switch (packet.Type)
            {
                case VariableDataTypes.String:
                    type = InputBox.InputType.TextInput;

                    break;
                case VariableDataTypes.Integer:
                case VariableDataTypes.Number:
                    type = InputBox.InputType.NumericInput;

                    break;
                case VariableDataTypes.Boolean:
                    type = InputBox.InputType.YesNo;

                    break;
            }

            var iBox = new InputBox(
                packet.Title, packet.Prompt, true, type, PacketSender.SendEventInputVariable,
                PacketSender.SendEventInputVariableCancel, packet.EventId
            );
        }

        //ErrorMessagePacket
        public void HandlePacket(IPacketSender packetSender, ErrorMessagePacket packet)
        {
            FadeService.FadeIn();
            Globals.WaitingOnServer = false;
            Interface.Interface.MsgboxErrors.Add(new KeyValuePair<string, string>(packet.Header, packet.Error));

            if (packet.ResetUi)
            {
                Interface.Interface.MenuUi.Reset();
            }
        }

        //MapItemsPacket
        public void HandlePacket(IPacketSender packetSender, MapItemsPacket packet)
        {
            var map = MapInstance.Get(packet.MapId);
            if (map == null)
            {
                return;
            }

            map.MapItems.Clear();
            foreach (var item in packet.Items)
            {
                var mapItem = new MapItemInstance(item.TileIndex, item.Id, item.ItemId, item.BagId, item.Quantity, item.ItemProperties);

                if (!map.MapItems.ContainsKey(mapItem.TileIndex))
                {
                    map.MapItems.Add(mapItem.TileIndex, new List<MapItemInstance>());
                }

                map.MapItems[mapItem.TileIndex].Add(mapItem);
            }
        }

        //MapItemUpdatePacket
        public void HandlePacket(IPacketSender packetSender, MapItemUpdatePacket packet)
        {
            var map = MapInstance.Get(packet.MapId);
            if (map == null)
            {
                return;
            }

            // Are we deleting this item?
            if (packet.ItemId == Guid.Empty)
            {
                // Find our item based on our unique Id and remove it.
                foreach (var location in map.MapItems.Keys)
                {
                    var tempItem = map.MapItems[location].Where(item => item.UniqueId == packet.Id).SingleOrDefault();
                    if (tempItem != null)
                    {
                        map.MapItems[location].Remove(tempItem);
                    }
                }
            }
            else
            {
                if (!map.MapItems.ContainsKey(packet.TileIndex))
                {
                    map.MapItems.Add(packet.TileIndex, new List<MapItemInstance>());
                }

                // Check if the item already exists, if it does replace it. Otherwise just add it.
                var mapItem = new MapItemInstance(packet.TileIndex, packet.Id, packet.ItemId, packet.BagId, packet.Quantity, packet.ItemProperties);
                if (map.MapItems[packet.TileIndex].Any(item => item.UniqueId == mapItem.UniqueId))
                {
                    for (var index = 0; index < map.MapItems[packet.TileIndex].Count; index++)
                    {
                        if (map.MapItems[packet.TileIndex][index].UniqueId == mapItem.UniqueId)
                        {
                            map.MapItems[packet.TileIndex][index] = mapItem;
                        }
                    }
                }
                else
                {
                    // Reverse the array again to match server, add item.. then  reverse again to get the right render order.
                    map.MapItems[packet.TileIndex].Add(mapItem);
                }
            }
        }

        //InventoryPacket
        public void HandlePacket(IPacketSender packetSender, InventoryPacket packet)
        {
            foreach (var inv in packet.Slots)
            {
                HandlePacket(inv);
            }
        }

        //InventoryUpdatePacket
        public void HandlePacket(IPacketSender packetSender, InventoryUpdatePacket packet)
        {
            if (Globals.Me != null)
            {
                Globals.Me.Inventory[packet.Slot].Load(packet.ItemId, packet.Quantity, packet.BagId, packet.ItemProperties);
                Globals.Me.InventoryUpdatedDelegate?.Invoke();
            }
        }

        //SpellsPacket
        public void HandlePacket(IPacketSender packetSender, SpellsPacket packet)
        {
            foreach (var spl in packet.Slots)
            {
                HandlePacket(spl);
            }
        }

        //SpellUpdatePacket
        public void HandlePacket(IPacketSender packetSender, SpellUpdatePacket packet)
        {
            if (Globals.Me != null)
            {
                Globals.Me.Spells[packet.Slot].Load(packet.SpellId);
            }
        }

        //EquipmentPacket
        public void HandlePacket(IPacketSender packetSender, EquipmentPacket packet)
        {
            var entityId = packet.EntityId;
            if (Globals.Entities.ContainsKey(entityId))
            {
                var entity = Globals.Entities[entityId];
                if (entity != null)
                {
                    if (entity == Globals.Me && packet.InventorySlots != null)
                    {
                        entity.MyEquipment = packet.InventorySlots;
                    }
                    else if (packet.ItemIds != null)
                    {
                        entity.Equipment = packet.ItemIds;
                    }
                    entity.MyDecors = packet.Decor;
                    if (entity is Player pl)
                    {
                        pl.Cosmetics = packet.CosmeticItemIds;
                    }
                }
            }

            if (entityId == Globals.Me?.Id && Interface.Interface.GameUi?.CurrentCharacterPanel == Interface.Game.Character.CharacterPanelType.Bonuses)
            {
                CharacterBonusesPanelController.Refresh = true;
            }

            if (entityId == Globals.Me?.Id && Interface.Interface.GameUi?.CurrentCharacterPanel == Interface.Game.Character.CharacterPanelType.Cosmetics)
            {
                Globals.Me?.CosmeticsUpdateDelegate?.Invoke();
            }
            if (entityId == Globals.Me?.Id)
            {
                Globals.Me?.ChallengeUpdateDelegate?.Invoke();
            }
        }

        //StatPointsPacket
        public void HandlePacket(IPacketSender packetSender, StatPointsPacket packet)
        {
            if (Globals.Me != null)
            {
                Globals.Me.StatPoints = packet.Points;
            }
        }

        //HotbarPacket
        public void HandlePacket(IPacketSender packetSender, HotbarPacket packet)
        {
            for (var i = 0; i < Options.MaxHotbar; i++)
            {
                if (Globals.Me == null)
                {
                    Log.Debug("Can't set hotbar, Globals.Me is null!");

                    break;
                }

                if (Globals.Me.Hotbar == null)
                {
                    Log.Debug("Can't set hotbar, hotbar is null!");

                    break;
                }

                var hotbarEntry = Globals.Me.Hotbar[i];
                hotbarEntry.Load(packet.SlotData[i]);


            }
        }

        //CharacterCreationPacket
        public void HandlePacket(IPacketSender packetSender, CharacterCreationPacket packet)
        {
            Globals.WaitingOnServer = false;
            Interface.Interface.MenuUi.MainMenu.NotifyOpenCharacterCreation();
        }

        //AdminPanelPacket
        public void HandlePacket(IPacketSender packetSender, AdminPanelPacket packet)
        {
            Interface.Interface.GameUi.NotifyOpenAdminWindow();
        }

        //SpellCastPacket
        public void HandlePacket(IPacketSender packetSender, SpellCastPacket packet)
        {
            var entityId = packet.EntityId;
            var spellId = packet.SpellId;
            var spell = SpellBase.Get(spellId);
            if (spell != null && Globals.Entities.TryGetValue(entityId, out var entity))
            {
                entity.SetSpellCast(spell, packet.TargetId);
            }
        }

        //SpellCooldownPacket
        public void HandlePacket(IPacketSender packetSender, SpellCooldownPacket packet)
        {
            foreach (var cd in packet.SpellCds)
            {
                var time = Timing.Global.Milliseconds + cd.Value;
                if (!Globals.Me.SpellCooldowns.ContainsKey(cd.Key))
                {
                    Globals.Me.SpellCooldowns.Add(cd.Key, time);
                }
                else
                {
                    Globals.Me.SpellCooldowns[cd.Key] = time;
                }
            }
        }

        //ItemCooldownPacket
        public void HandlePacket(IPacketSender packetSender, ItemCooldownPacket packet)
        {
            foreach (var cd in packet.ItemCds)
            {
                var time = Timing.Global.Milliseconds + cd.Value;
                if (!Globals.Me.ItemCooldowns.ContainsKey(cd.Key))
                {
                    Globals.Me.ItemCooldowns.Add(cd.Key, time);
                }
                else
                {
                    Globals.Me.ItemCooldowns[cd.Key] = time;
                }
            }
        }

        //ExperiencePacket
        public void HandlePacket(IPacketSender packetSender, ExperiencePacket packet)
        {
            if (Globals.Me != null)
            {
                Globals.Me.Experience = packet.Experience;
                Globals.Me.ExperienceToNextLevel = packet.ExperienceToNextLevel;
                Globals.Me.WeaponExp = packet.WeaponExp;
                Globals.Me.WeaponExpTnl = packet.WeaponExpTnl;
                Globals.Me.TrackedWeaponLevel = packet.WeaponLevel;
                Globals.Me.TrackedWeaponTypeId = packet.TrackedWeaponId;
                if (packet.AccumulatedComboExp > 0)
                {
                    Globals.Me.ComboExp = packet.AccumulatedComboExp;
                }

                Globals.Me?.ChallengeUpdateDelegate?.Invoke();
            }
        }

        //ProjectileDeadPacket
        public void HandlePacket(IPacketSender packetSender, ProjectileDeadPacket packet)
        {
            if (packet.ProjectileDeaths != null)
            {
                foreach (var projDeath in packet.ProjectileDeaths)
                {
                    if (Globals.Entities.ContainsKey(projDeath) && Globals.Entities[projDeath].GetType() == typeof(Projectile))
                    {
                        Globals.Entities[projDeath]?.Dispose();
                        Globals.EntitiesToDispose?.Add(projDeath);
                    }
                }
            }
            if (packet.SpawnDeaths != null)
            {
                foreach (var spawnDeath in packet.SpawnDeaths)
                {
                    if (Globals.Entities.ContainsKey(spawnDeath.Key) && Globals.Entities[spawnDeath.Key].GetType() == typeof(Projectile))
                    {
                        ((Projectile)Globals.Entities[spawnDeath.Key]).SpawnDead(spawnDeath.Value);
                    }
                }
            }
        }

        //PlayAnimationPackets
        public void HandlePacket(IPacketSender sender, PlayAnimationPackets packet)
        {
            foreach (var pkt in packet.Packets)
            {
                HandlePacket(pkt);
            }
        }

        //PlayAnimationPacket
        public void HandlePacket(IPacketSender packetSender, PlayAnimationPacket packet)
        {
            var mapId = packet.MapId;
            var animId = packet.AnimationId;
            var targetType = packet.TargetType;
            var entityId = packet.EntityId;
            if (targetType == -1)
            {
                var map = MapInstance.Get(mapId);
                if (map != null)
                {
                    map.AddTileAnimation(animId, packet.X, packet.Y, packet.Direction);
                }
            }
            else if (targetType == 1)
            {
                if (Globals.Entities.ContainsKey(entityId))
                {
                    if (Globals.Entities[entityId] != null && !Globals.EntitiesToDispose.Contains(entityId))
                    {
                        var animBase = AnimationBase.Get(animId);
                        if (animBase != null)
                        {
                            var animInstance = new Animation(
                                animBase, false, packet.Direction != -1 && !packet.ProjectileHitAnim, -1, Globals.Entities[entityId]
                            );

                            if (packet.Direction > -1)
                            {
                                animInstance.SetDir(packet.Direction);
                            }

                            Globals.Entities[entityId].Animations.Add(animInstance);
                        }
                    }
                }
            }
            else if (targetType == 2)
            {
                var map = MapInstance.Get(mapId);
                if (map != null)
                {
                    if (map.LocalEntities.ContainsKey(entityId))
                    {
                        if (map.LocalEntities[entityId] != null)
                        {
                            var animBase = AnimationBase.Get(animId);
                            if (animBase != null)
                            {
                                var animInstance = new Animation(
                                    animBase, false, packet.Direction == -1, -1,
                                    map.LocalEntities[entityId]
                                );

                                if (packet.Direction > -1)
                                {
                                    animInstance.SetDir(packet.Direction);
                                }

                                map.LocalEntities[entityId].Animations.Add(animInstance);
                            }
                        }
                    }
                }
            }
        }

        //HoldPlayerPacket
        public void HandlePacket(IPacketSender packetSender, HoldPlayerPacket packet)
        {
            var eventId = packet.EventId;
            var mapId = packet.MapId;
            if (!packet.Releasing)
            {
                if (!Globals.EventHolds.ContainsKey(eventId))
                {
                    Globals.EventHolds.Add(eventId, mapId);
                    if (Globals.EventHolds.Count == 1)
                    {
                        Globals.EventHoldWidescreenTime = Timing.Global.Milliseconds + 60;
                    }
                }
            }
            else
            {
                if (Globals.EventHolds.ContainsKey(eventId))
                {
                    Globals.EventHolds.Remove(eventId);
                }
            }
        }

        //PlayMusicPacket
        public void HandlePacket(IPacketSender packetSender, PlayMusicPacket packet)
        {
            Audio.PlayMusic(packet.BGM, 6f, 10f, true);
        }

        //StopMusicPacket
        public void HandlePacket(IPacketSender packetSender, StopMusicPacket packet)
        {
            Audio.StopMusic(6f);
        }

        //PlaySoundPacket
        public void HandlePacket(IPacketSender packetSender, PlaySoundPacket packet)
        {
            Audio.AddGameSound(packet.Sound, false);
        }

        //StopSoundsPacket
        public void HandlePacket(IPacketSender packetSender, StopSoundsPacket packet)
        {
            Audio.StopAllSounds();
        }

        //ShowPicturePacket
        public void HandlePacket(IPacketSender packetSender, ShowPicturePacket packet)
        {
            PacketSender.SendClosePicture(Globals.Picture?.EventId ?? Guid.Empty);
            packet.ReceiveTime = Timing.Global.Milliseconds;
            Globals.Picture = packet;
        }

        //HidePicturePacket
        public void HandlePacket(IPacketSender packetSender, HidePicturePacket packet)
        {
            PacketSender.SendClosePicture(Globals.Picture?.EventId ?? Guid.Empty);
            Globals.Picture = null;
        }

        //ShopPacket
        public void HandlePacket(IPacketSender packetSender, ShopPacket packet)
        {
            if (Interface.Interface.GameUi == null)
            {
                throw new ArgumentNullException(nameof(Interface.Interface.GameUi));
            }

            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            if (packet.ShopData != null)
            {
                Globals.GameShop = new ShopBase();
                Globals.GameShop.Load(packet.ShopData);
                Interface.Interface.GameUi.NotifyOpenShop();
            }
            else
            {
                Globals.GameShop = null;
                Interface.Interface.GameUi.NotifyCloseShop();
            }
        }

        //CraftingTablePacket
        public void HandlePacket(IPacketSender packetSender, CraftingTablePacket packet)
        {
            if (!packet.Close)
            {
                Globals.ActiveCraftingTable = new CraftingTableBase();
                Globals.ActiveCraftingTable.Load(packet.TableData);
                if (!Globals.InCraft)
                {
                    Interface.Interface.GameUi.NotifyOpenCraftingTable();
                } else
                {
                    PacketSender.SendRequestUnlockedRecipes();
                }
            }
            else
            {
                Interface.Interface.GameUi.NotifyCloseCraftingTable();
            }
        }

        public void HandlePacket(IPacketSender packetSender, CraftStatusPacket packet)
        {
            Interface.Interface.GameUi.UpdateCraftStatus(packet.AmountRemaining);
        }

        //BankPacket
        public void HandlePacket(IPacketSender packetSender, BankPacket packet)
        {
            if (!packet.Close)
            {
                Globals.GuildBank = packet.Guild;
                Globals.Bank = new Item[packet.Slots];
                foreach (var itm in packet.Items)
                {
                    HandlePacket(itm);
                }
                Globals.BankSlots = packet.Slots;
                Interface.Interface.GameUi.NotifyOpenBank();
                Globals.BankValue = packet.BankValue;
            }
            else
            {
                Interface.Interface.GameUi.NotifyCloseBank();
            }
        }

        //BankUpdatePacket
        public void HandlePacket(IPacketSender packetSender, BankUpdatePacket packet)
        {
            var slot = packet.Slot;
            if (packet.ItemId != Guid.Empty)
            {
                Globals.Bank[slot] = new Item();
                Globals.Bank[slot].Load(packet.ItemId, packet.Quantity, packet.BagId, packet.ItemProperties);
            }
            else
            {
                Globals.Bank[slot] = null;
            }
            Interface.Interface.GameUi.RefreshBank();
        }

        //BankUpdateValuePacket
        public void HandlePacket(IPacketSender packetSender, BankUpdateValuePacket packet)
        {
            Globals.BankValue = packet.BankValue;
        }

        //GameObjectPacket
        public void HandlePacket(IPacketSender packetSender, GameObjectPacket packet)
        {
            var type = packet.Type;
            var id = packet.Id;
            var another = packet.AnotherFollowing;
            var deleted = packet.Deleted;
            var json = "";
            if (!deleted)
            {
                json = packet.Data;
            }

            switch (type)
            {
                case GameObjectType.Map:
                    //Handled in a different packet
                    break;
                case GameObjectType.Tileset:
                    var obj = new TilesetBase(id);
                    obj.Load(json);
                    TilesetBase.Lookup.Set(id, obj);
                    if (Globals.HasGameData && !another)
                    {
                        Globals.ContentManager.LoadTilesets(TilesetBase.GetNameList());
                    }

                    break;
                case GameObjectType.Event:
                    //Clients don't store event data, im an idiot.
                    break;
                default:
                    var lookup = type.GetLookup();
                    if (deleted)
                    {
                        lookup.Get(id)?.Delete();
                    }
                    else
                    {
                        lookup.DeleteAt(id);
                        var item = lookup.AddNew(type.GetObjectType(), id);
                        item.Load(json);
                    }

                    break;
            }

            // If we've recieved NPCs, we need to update the bestiary cache to reflect that
            if (type == GameObjectType.Npc)
            {
                Interface.Interface.GameUi?.GameMenu?.CloseBesitary();
                BestiaryController.RefreshBeastCache();
            }
        }

        //EntityDashPacket
        public void HandlePacket(IPacketSender packetSender, EntityDashPacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.EntityId))
            {
                Globals.Entities[packet.EntityId]
                    .DashQueue.Enqueue(
                        new Dash(
                            Globals.Entities[packet.EntityId], packet.EndMapId, packet.EndX, packet.EndY,
                            packet.DashTime, packet.Direction
                        )
                    );
            }
        }

        //MapGridPacket
        public void HandlePacket(IPacketSender packetSender, MapGridPacket packet)
        {
            Globals.MapGridWidth = packet.Grid.GetLength(0);
            Globals.MapGridHeight = packet.Grid.GetLength(1);
            var clearKnownMaps = packet.ClearKnownMaps;
            Globals.MapGrid = new Guid[Globals.MapGridWidth, Globals.MapGridHeight];
            Globals.GridNames = new Dictionary<Guid, string>();
            if (clearKnownMaps)
            {
                foreach (var map in MapInstance.Lookup.Values.ToArray())
                {
                    ((MapInstance)map).Dispose();
                }
            }

            Globals.NeedsMaps = true;
            Globals.GridMaps.Clear();
            for (var x = 0; x < Globals.MapGridWidth; x++)
            {
                for (var y = 0; y < Globals.MapGridHeight; y++)
                {
                    Globals.MapGrid[x, y] = packet.Grid[x, y];
                    if (Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        Globals.GridMaps.Add(Globals.MapGrid[x, y]);
                        Globals.GridNames.Add(Globals.MapGrid[x, y], packet.MapNames[x, y]);
                        MapInstance.UpdateMapRequestTime(Globals.MapGrid[x, y]);
                    }
                }
            }

            if (Globals.Me != null)
            {
                Globals.Me.FetchNewMaps();
            }

            Graphics.GridSwitched = true;
        }

        //TimePacket
        public void HandlePacket(IPacketSender packetSender, TimePacket packet)
        {
            Time.LoadTime(
                packet.Time, Color.FromArgb(packet.Color.A, packet.Color.R, packet.Color.G, packet.Color.B), packet.Rate
            );
        }

        //PartyPacket
        public void HandlePacket(IPacketSender packetSender, PartyPacket packet)
        {
            if (Globals.Me == null || Globals.Me.Party == null)
            {
                return;
            }

            Globals.Me.Party.Clear();
            for (var i = 0; i < packet.MemberData.Length; i++)
            {
                var mem = packet.MemberData[i];
                Globals.Me.Party.Add(new PartyMember(mem.Id, mem.Name, mem.Vital, mem.MaxVital, mem.Level, mem.Shield));
            }
        }

        //PartyUpdatePacket
        public void HandlePacket(IPacketSender packetSender, PartyUpdatePacket packet)
        {
            var index = packet.MemberIndex;
            if (index < Globals.Me.Party.Count)
            {
                var mem = packet.MemberData;
                Globals.Me.Party[index] = new PartyMember(mem.Id, mem.Name, mem.Vital, mem.MaxVital, mem.Level, mem.Shield);
            }
        }

        //PartyInvitePacket
        public void HandlePacket(IPacketSender packetSender, PartyInvitePacket packet)
        {
            var iBox = new InputBox(
                Strings.Parties.partyinvite, Strings.Parties.inviteprompt.ToString(packet.LeaderName), true,
                InputBox.InputType.YesNo, PacketSender.SendPartyAccept, PacketSender.SendPartyDecline, packet.LeaderId
            );
        }

        //ChatBubblePacket
        public void HandlePacket(IPacketSender packetSender, ChatBubblePacket packet)
        {
            var id = packet.EntityId;
            var type = packet.Type;
            var mapId = packet.MapId;
            Entity en = null;
            if (type < EntityTypes.Event)
            {
                if (!Globals.Entities.ContainsKey(id))
                {
                    return;
                }

                en = Globals.Entities[id];
            }
            else
            {
                var entityMap = MapInstance.Get(mapId);
                if (entityMap == null)
                {
                    return;
                }

                if (!entityMap.LocalEntities.ContainsKey(id))
                {
                    return;
                }

                en = entityMap.LocalEntities[id];
            }

            if (en == null)
            {
                return;
            }

            if (en is Player player)
            {
                if (packet.BubbleType == ChatBubbleType.Party && !Globals.Me.IsInMyParty(player))
                {
                    return;
                }
                if (packet.BubbleType == ChatBubbleType.Guild && Globals.Me.Guild != player.Guild)
                {
                    return;
                }
            }

            en.AddChatBubble(packet.Text, packet.BubbleType);
        }

        //QuestOfferPacket
        public void HandlePacket(IPacketSender packetSender, QuestOfferPacket packet)
        {
            if (!Globals.QuestOffers.Contains(packet.QuestId))
            {
                Globals.QuestOfferIndex = 0; // reset quest selection to 0
                Globals.QuestOffers.Add(packet.QuestId);
            }
        }

        //QuestProgressPacket
        public void HandlePacket(IPacketSender packetSender, QuestProgressPacket packet)
        {
            if (Globals.Me != null)
            {
                foreach (var quest in packet.Quests)
                {
                    if (quest.Value == null)
                    {
                        if (Globals.Me.QuestProgress.ContainsKey(quest.Key))
                        {
                            Globals.Me.QuestProgress.Remove(quest.Key);
                        }
                    }
                    else
                    {
                        if (Globals.Me.QuestProgress.ContainsKey(quest.Key))
                        {
                            Globals.Me.QuestProgress[quest.Key] = new QuestProgress(quest.Value);
                        }
                        else
                        {
                            Globals.Me.QuestProgress.Add(quest.Key, new QuestProgress(quest.Value));
                        }
                    }
                }

                Globals.Me.HiddenQuests = packet.HiddenQuests;

                if (Interface.Interface.GameUi != null)
                {
                    Interface.Interface.GameUi.NotifyQuestsUpdated();
                }
            }
        }

        //TradePacket
        public void HandlePacket(IPacketSender packetSender, TradePacket packet)
        {
            if (!string.IsNullOrEmpty(packet.TradePartner))
            {
                Globals.Trade = new Item[2, Options.MaxInvItems];

                //Gotta initialize the trade values
                for (var x = 0; x < 2; x++)
                {
                    for (var y = 0; y < Options.MaxInvItems; y++)
                    {
                        Globals.Trade[x, y] = new Item();
                    }
                }

                Interface.Interface.GameUi.NotifyOpenTrading(packet.TradePartner);
            }
            else
            {
                Interface.Interface.GameUi.NotifyCloseTrading();
            }
        }

        //TradeUpdatePacket
        public void HandlePacket(IPacketSender packetSender, TradeUpdatePacket packet)
        {
            var side = 0;

            if (packet.TraderId != Globals.Me.Id)
            {
                side = 1;
            }

            var slot = packet.Slot;
            if (packet.ItemId == Guid.Empty)
            {
                Globals.Trade[side, slot] = null;
            }
            else
            {
                Globals.Trade[side, slot] = new Item();
                Globals.Trade[side, slot].Load(packet.ItemId, packet.Quantity, packet.BagId, packet.ItemProperties);
            }
        }

        //TradeRequestPacket
        public void HandlePacket(IPacketSender packetSender, TradeRequestPacket packet)
        {
            var iBox = new InputBox(
                Strings.Trading.traderequest, Strings.Trading.requestprompt.ToString(packet.PartnerName), true,
                InputBox.InputType.YesNo, PacketSender.SendTradeRequestAccept, PacketSender.SendTradeRequestDecline,
                packet.PartnerId
            );
        }

        //NpcAggressionPacket
        public void HandlePacket(IPacketSender packetSender, NpcAggressionPacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.EntityId))
            {
                var entity = Globals.Entities[packet.EntityId];
                entity.Type = packet.Aggression;
                if (packet.Aggression < 0) // has some target
                {
                    entity.EntityTarget = packet.TargetId;
                }
                else
                {
                    entity.EntityTarget = Guid.Empty;
                }
            }
        }

        //PlayerDeathPacket
        public void HandlePacket(IPacketSender packetSender, PlayerDeathPacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.PlayerId))
            {
                //Clear all dashes.
                Globals.Entities[packet.PlayerId].DashQueue.Clear();
                Globals.Entities[packet.PlayerId].Dashing = null;
                Globals.Entities[packet.PlayerId].DashTimer = 0;
                Globals.Entities[packet.PlayerId].IsDead = true;
                if (packet.PlayerId == Globals.Me?.Id)
                {
                    Globals.Me.ToggleCombatMode();
                    Globals.Me.HPWarning = false;
                    Globals.Me.MPWarning = false;
                    Globals.Me.CombatTimer = Timing.Global.Milliseconds;
                    Flash.FlashScreen(1000, CustomColors.General.GeneralWarning, 150);
                }
            }
        }

        //PlayerRespawnPacket
        public void HandlePacket(IPacketSender packetSender, PlayerDeathUpdatePacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.PlayerId))
            {
                Globals.Entities[packet.PlayerId].IsDead = packet.IsDead;
            }
        }


        //EntityZDimensionPacket
        public void HandlePacket(IPacketSender packetSender, EntityZDimensionPacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.EntityId))
            {
                Globals.Entities[packet.EntityId].Z = packet.Level;
            }
        }

        //BagPacket
        public void HandlePacket(IPacketSender packetSender, BagPacket packet)
        {
            if (!packet.Close)
            {
                Globals.Bag = new Item[packet.Slots];
                Interface.Interface.GameUi.NotifyOpenBag();
            }
            else
            {
                Interface.Interface.GameUi.NotifyCloseBag();
            }
        }

        //BagUpdatePacket
        public void HandlePacket(IPacketSender packetSender, BagUpdatePacket packet)
        {
            if (packet.ItemId == Guid.Empty)
            {
                Globals.Bag[packet.Slot] = null;
            }
            else
            {
                Globals.Bag[packet.Slot] = new Item();
                Globals.Bag[packet.Slot].Load(packet.ItemId, packet.Quantity, packet.BagId, packet.ItemProperties);
            }
        }

        //MoveRoutePacket
        public void HandlePacket(IPacketSender packetSender, MoveRoutePacket packet)
        {
            Globals.Me.CombatMode = false;
            Globals.MoveRouteActive = packet.Active;
        }

        //FriendsPacket
        public void HandlePacket(IPacketSender packetSender, FriendsPacket packet)
        {
            Globals.Me.Friends.Clear();

            foreach (var friend in packet.OnlineFriends)
            {
                var f = new FriendInstance()
                {
                    Name = friend.Key,
                    Map = friend.Value,
                    Online = true
                };

                Globals.Me.Friends.Add(f);
            }

            foreach (var friend in packet.OfflineFriends)
            {
                var f = new FriendInstance()
                {
                    Name = friend,
                    Online = false
                };

                Globals.Me.Friends.Add(f);
            }

            Interface.Interface.GameUi.NotifyUpdateFriendsList();
        }

        //FriendRequestPacket
        public void HandlePacket(IPacketSender packetSender, FriendRequestPacket packet)
        {
            var iBox = new InputBox(
                Strings.Friends.request, Strings.Friends.requestprompt.ToString(packet.FriendName), true,
                InputBox.InputType.YesNo, PacketSender.SendFriendRequestAccept, PacketSender.SendFriendRequestDecline,
                packet.FriendId
            );
        }

        //CharactersPacket
        public void HandlePacket(IPacketSender packetSender, CharactersPacket packet)
        {
            var characters = new List<Character>();

            foreach (var chr in packet.Characters)
            {
                characters.Add(
                    new Character(chr.Id, chr.Name, chr.Sprite, chr.Face, chr.Level, chr.ClassName, chr.Equipment, chr.Decor, chr.HideHair, chr.HideBeard, chr.HideExtra)
                );
            }

            if (packet.FreeSlot)
            {
                characters.Add(null);
            }

            Globals.WaitingOnServer = false;
            Interface.Interface.MenuUi.MainMenu.NotifyOpenCharacterSelection(characters);
        }

        //PasswordResetResultPacket
        public void HandlePacket(IPacketSender packetSender, PasswordResetResultPacket packet)
        {
            if (packet.Succeeded)
            {
                //Show Success Message and Open Login Screen
                Interface.Interface.MsgboxErrors.Add(
                    new KeyValuePair<string, string>(Strings.ResetPass.success, Strings.ResetPass.successmsg)
                );

                Interface.Interface.MenuUi.MainMenu.NotifyOpenLogin();
            }
            else
            {
                //Show Error Message
                Interface.Interface.MsgboxErrors.Add(
                    new KeyValuePair<string, string>(Strings.ResetPass.fail, Strings.ResetPass.failmsg)
                );
            }

            Globals.WaitingOnServer = false;
        }

        //TargetOverridePacket
        public void HandlePacket(IPacketSender packetSender, TargetOverridePacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.TargetId))
            {
                Globals.Me.TryTarget(Globals.Entities[packet.TargetId], true);
            }
        }

        //EnteringGamePacket
        public void HandlePacket(IPacketSender packetSender, EnteringGamePacket packet)
        {
            // Update the graphics' last update timers
            Graphics.SetLastUpdate();
        }

        //CancelCastPacket
        public void HandlePacket(IPacketSender packetSender, CancelCastPacket packet)
        {
            if (Globals.Entities.ContainsKey(packet.EntityId))
            {
                Globals.Entities[packet.EntityId].CastTime = 0;
                Globals.Entities[packet.EntityId].SpellCast = Guid.Empty;
            }
        }

        //GuildPacket
        public void HandlePacket(IPacketSender packetSender, GuildPacket packet)
        {
            if (Globals.Me == null || Globals.Me.Guild == null)
            {
                return;
            }

            Globals.Me.GuildMembers = packet.Members.OrderByDescending(m => m.Online).ThenBy(m => m.Rank).ThenBy(m => m.Name).ToArray();

            Interface.Interface.GameUi?.NotifyUpdateGuildList();
        }


        //GuildInvitePacket
        public void HandlePacket(IPacketSender packetSender, GuildInvitePacket packet)
        {
            var iBox = new InputBox(
                Strings.Guilds.InviteRequestTitle, Strings.Guilds.InviteRequestPrompt.ToString(packet.Inviter, packet.GuildName), true,
                InputBox.InputType.YesNo, PacketSender.SendGuildInviteAccept, PacketSender.SendGuildInviteDecline,
                null
            );
        }

        // Map fading in/out packet
        public void HandlePacket(IPacketSender packetSender, MapFadePacket packet)
        {
            if (packet.FadeIn)
            {
                FadeService.FadeIn(true);

                Globals.InMapTransition = false;
            } else
            {
                FadeService.FadeOut(true, true);

                Globals.InMapTransition = true;
            }
        }

        // Update future warp position packet
        public void HandlePacket(IPacketSender packetSender, UpdateFutureWarpPacket packet)
        {
            Globals.futureWarpMapId = packet.NewMapId;
            Globals.futureWarpX = packet.X;
            Globals.futureWarpY = packet.Y;
            Globals.futureWarpDir = packet.Dir;
            Globals.futureWarpInstanceType = packet.InstanceType;
            Globals.futureDungeonId = packet.DungeonId;
        }

        // Combo handling packet
        public void HandlePacket(IPacketSender packetSender, ComboPacket packet)
        {
            if (Globals.Me == null) return;

            Globals.Me.CurrentCombo = packet.ComboSize;
            if (packet.ComboSize == 0)
            {
                Globals.Me.ComboExp = 0;
            }
            Globals.Me.MaxComboWindow = packet.MaxComboWindow;
            Globals.Me.LastComboUpdate = Timing.Global.Milliseconds;
        }

        // Crafting Info packet
        public void HandlePacket(IPacketSender packetSender, CraftingInfoPacket packet)
        {
            if (Globals.Me == null) return;

            Globals.Me.MiningTier = packet?.MiningTier;
            Globals.Me.FishingTier = packet?.FishingTier;
            Globals.Me.WoodcutTier = packet?.WoodcutTier;
            Globals.Me.ClassRanks = packet.ClassRanks;
        }

        public void HandlePacket(IPacketSender packetSender, QuestPointPacket packet)
        {
            if (Globals.Me == null) return;

            Globals.Me.QuestPoints = packet?.QuestPoints;
            Globals.Me.LifetimeQuestPoints = packet?.LifetimeQuestPoints;
        }

        public void HandlePacket(IPacketSender packetSender, GUINotificationPacket packet)
        {
            if (Globals.Me == null) return;

            switch (packet.Notification)
            {
                case GUINotification.NotEnoughMp:
                    Globals.Me.MPWarning = packet.DisplayNotification;
                    break;
                case GUINotification.LowHP:
                    Globals.Me.HPWarning = packet.DisplayNotification;
                    break;
                default:
                    break;
            }
        }

        // The client will flash the screen with the given params
        public void HandlePacket(IPacketSender packetSender, FlashScreenPacket packet)
        {
            if (Globals.Me == null) return;

            Flash.FlashScreen(packet.Duration, packet.FlashColor, packet.Intensity);

            var flashSound = packet.SoundFile;
            if (flashSound != null)
            {
                Audio.AddGameSound(flashSound, false);
            }
        }

        // Updates the client player to be locked to a resource
        public void HandlePacket(IPacketSender packetSender, ResourceLockPacket packet)
        {
            if (Globals.Me == null) return;

            Globals.Me.ResourceLocked = packet.ResourceLock;
            Globals.Me.CurrentHarvestBonus = packet.HarvestBonus;
            Globals.Me.HarvestsRemaining = packet.HarvestsRemaining;
            Globals.Me.HarvestingResource = packet.ResourceId;
        }

        // QuestBoardPacket
        public void HandlePacket(IPacketSender packetSender, QuestBoardPacket packet)
        {
            if (Globals.Me == null) return;

            if (!packet.Close)
            {
                Globals.QuestBoard = new QuestBoardBase();
                Globals.QuestBoard.Load(packet.QuestBoardData);
                Globals.QuestBoardRequirements = packet.RequirementsForQuestLists;
                Interface.Interface.GameUi.NotifyOpenQuestBoard();
            }
            else
            {
                Interface.Interface.GameUi.NotifyCloseQuestBoard();
            }
        }

        // QuestOfferListPacket
        public void HandlePacket(IPacketSender packetSender, QuestOfferListPacket packet)
        {
            if (Globals.Me == null) return;

            if (packet.Quests.Count > 0)
            {
                Globals.QuestOfferIndex = 0; // reset quest selection to 0
            }
            foreach (var quest in packet.Quests)
            {
                Globals.QuestOffers.Add(quest);
            }
        }

        // Screen Shake packet
        public void HandlePacket(IPacketSender packetSender, ShakeScreenPacket packet)
        {
            if (Globals.Me == null) return;

            if (Graphics.CurrentShake < packet.Intensity)
            {
                Graphics.CurrentShake = packet.Intensity;
            }
        }

        // Combat Effect packet
        public void HandlePacket(IPacketSender packetSender, CombatEffectPacket packet)
        {
            if (Globals.Me == null) return;

            var someTarget = packet.TargetId != null && packet.TargetId != Guid.Empty && Globals.Entities.ContainsKey(packet.TargetId);

            if (Graphics.CurrentShake < packet.ShakeAmount && packet.ShakeAmount > 0.0f && Globals.Database.CombatShake)
            {
                Graphics.CurrentShake = packet.ShakeAmount;
            }

            if (packet.FlashColor != null && Globals.Database.CombatFlash)
            {
                Flash.FlashScreen(packet.FlashDuration, packet.FlashColor, packet.FlashIntensity);
            }
            // Flash entity
            if (someTarget)
            {
                Entity affectedTarget = Globals.Entities[packet.TargetId];
                affectedTarget.Flash = true;
                affectedTarget.FlashColor = packet.EntityFlashColor;
                affectedTarget.FlashEndTime = Timing.Global.Milliseconds + 200; // TODO config
                if (!string.IsNullOrEmpty(packet.Sound))
                {
                    Audio.AddMapSound(packet.Sound, affectedTarget.X, affectedTarget.Y, affectedTarget.CurrentMap, false, 0, 10);
                }
            } else // If we don't have a specific target handle this as if the hit-effects are happening to the packet-receiving player
            {
                if (!string.IsNullOrEmpty(packet.Sound))
                {
                    Audio.AddGameSound(packet.Sound, false);
                }
                Globals.Me.Flash = true;
                Globals.Me.FlashColor = packet.EntityFlashColor;
                Globals.Me.FlashEndTime = Timing.Global.Milliseconds + 200; // TODO config
            }
        }

        //DestroyConditionPacket
        public void HandlePacket(IPacketSender packetSender, DestroyConditionPacket packet)
        {
            Globals.Me.TryDropItem(packet.Index, true, packet.CanDestroy);
        }

        //MapTrapPacket
        public void HandlePacket(IPacketSender packetSender, MapTrapPacket packet)
        {
            var map = MapInstance.Get(packet.MapId);
            if (map == null) return;

            if (!packet.Remove)
            {
                map.AddTrap(packet.TrapId, packet.AnimationId, packet.OwnerId, packet.X, packet.Y);
            }
            else
            {
                map.RemoveTrap(packet.TrapId);
            }
        }

        //ProjectileCastDelayPacket
        public void HandlePacket(IPacketSender packetSender, ProjectileCastDelayPacket packet)
        {
            //Globals.Me.LastProjectileCastTime = Timing.Global.Milliseconds + packet.DelayTime;
        }

        //TimerPacket
        public void HandlePacket(IPacketSender packetSender, TimerPacket packet)
        {
            var activeTimer = Timers.ActiveTimers.Find(t => t.DescriptorId == packet.DescriptorId);
            if (activeTimer != default)
            {
                // Timer already being shown - update it
                activeTimer.Timestamp = packet.Timestamp;
                activeTimer.StartTime = packet.StartTime;
                return;
            }

            var displayType = packet.Type == TimerType.Countdown ? TimerDisplayType.Descending : TimerDisplayType.Ascending;

            var timer = new Timer(packet.DescriptorId, packet.Timestamp, packet.StartTime, displayType, packet.DisplayName, packet.ContinueAfterExpiration);
            Timers.ActiveTimers.Add(timer);

            // Display the most recently added timer to the user
            if (Interface.Interface.GameUi != null)
            {
                Interface.Interface.GameUi.GoToTimer(timer.DescriptorId);
            }
        }

        //TimerStopPacket
        public void HandlePacket(IPacketSender packetSender, TimerStopPacket packet)
        {
            foreach (var timer in Timers.ActiveTimers.ToList().FindAll(t => t.DescriptorId == packet.DescriptorId))
            {
                timer.ElapsedTime = packet.ElapsedTime;
                timer.EndTimer();
                Timers.ActiveTimers.Remove(timer);
            }
        }

        // InstanceLivesPacket
        public void HandlePacket(IPacketSender packetSender, InstanceLivesPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.DungeonLives = packet.Amount;
            Globals.Me.InDungeon = !packet.ClearLives;
        }
    }

    internal sealed partial class PacketHandler
    {
        public void HandlePacket(IPacketSender packetSender, MapsExploredPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.MapsExplored = packet.Maps;
        }

        public void HandlePacket(IPacketSender packetSender, TradeAcceptedPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.TradeAccepted = true;
        }

        public void HandlePacket(IPacketSender packetSender, OpenLeaderboardPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.Leaderboard.Clear();
            Globals.Me.Leaderboard.DisplayName = packet.DisplayName;
            Globals.Me.Leaderboard.Page = 0;
            Globals.Me.Leaderboard.RecordId = packet.RecordId;
            Globals.Me.Leaderboard.ScoreType = packet.ScoreType;
            Globals.Me.Leaderboard.Type = packet.RecordType;
            Globals.Me.Leaderboard.DisplayMode = packet.DisplayMode;
            Globals.Me.Leaderboard.Open();
            Globals.Me.Leaderboard.RequestPlayersRecord();
        }
        
        public void HandlePacket(IPacketSender packetSender, LeaderboardPagePacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.Leaderboard.Records.Clear();
            var recordPage = new List<Record>();
            foreach (var record in packet.Records)
            {
                recordPage.Add(new Record(record.Participants, record.Index, record.RecordDisplay));
            }
            Globals.Me.Leaderboard.Page = packet.CurrentPage;
            Globals.Me.Leaderboard.Records = recordPage;
            Globals.Me.Leaderboard.HighlightedPlayer = packet.HighlightedPlayer;
            Interface.Interface.GameUi.LeaderboardWindow.LoadRecords();
            Globals.Me.Leaderboard.Loading = false;
        }

        public void HandlePacket(IPacketSender packetSender, OpenLootRollPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Interface.Interface.GameUi.LootRollWindow.SetTitle(packet.Title);
            Interface.Interface.GameUi.LootRollWindow.SetBanking(packet.DisableBanking);
            Globals.CurrentLootAnim = packet.AnimationType;
            Globals.Me.LoadRolledLoot(packet.Loot);
        }

        public void HandlePacket(IPacketSender packetSender, LootRollUpdatePacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            if (packet.UpdatedLoot == null || packet.UpdatedLoot.Count <= 0)
            {
                Globals.Me.ResetLoot();
                return;
            }

            Globals.Me.LoadRolledLoot(packet.UpdatedLoot);
        }

        public void HandlePacket(IPacketSender packetSender, ServerDisposedPacket packet)
        {
            Globals.WaitingOnServerDispose = false;
        }

        public void HandlePacket(IPacketSender packetSender, FadePacket packet)
        {
            if (packet.FadeIn)
            {
                if (Globals.Database.FadeTransitions)
                {
                    Fade.FadeIn();
                }
                else
                {
                    Wipe.FadeIn();
                }
            }
            else
            {
                if (Globals.Database.FadeTransitions)
                {
                    Fade.FadeOut();
                }
                else
                {
                    Wipe.FadeOut();
                }
            }

            Globals.WaitFade = true;
        }

        public void HandlePacket(IPacketSender packetSender, PlayerDeathTypePacket packet)
        {
            if (Globals.Me == null || Interface.Interface.GameUi == null)
            {
                return;
            }

            Interface.Interface.GameUi.CloseAllWindows();
            Interface.Interface.GameUi.RespawnWindow.SetType(packet.Type, packet.ExpLost, packet.ItemsLost);
        }

        public void HandlePacket(IPacketSender packetSender, RespawnFinishedPacket packet)
        {
            if (Globals.Me == null || Interface.Interface.GameUi == null)
            {
                return;
            }

            Interface.Interface.GameUi.RespawnWindow.ServerRespawned();
        }

        public void HandlePacket(IPacketSender packetSender, CombatNumberPackets packet)
        {
            foreach (var pkt in packet.Packets)
            {
                HandlePacket(pkt);
            }
        }

        public void HandlePacket(IPacketSender packetSender, CombatNumberPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Entity visibleTo = null;
            if (packet.VisibleTo != Guid.Empty && !Globals.Entities.TryGetValue(packet.VisibleTo, out visibleTo))
            {
                visibleTo = null;
            }

            CombatNumberManager.AddCombatNumber(packet.Target, packet.Value, packet.Type, packet.X, packet.Y, packet.MapId, visibleTo);
        }

        public void HandlePacket(IPacketSender packetSender, ResourceInfoPackets packet)
        {
            HarvestInfoRows.CurrentRows.Clear();
            foreach(var pkt in packet.Packets)
            {
                HarvestInfoRows.CurrentRows.Add(new HarvestInfo(pkt));
            }
            CharacterHarvestingWindowController.WaitingOnServer = false;
        }

        public void HandlePacket(IPacketSender packetSender, PlayerLabelPackets packet)
        {
            CharacterNameTagPanelController.UnlockedLabels.Clear();
            Globals.Me.LabelDescriptorId = Guid.Empty;
            foreach (var pkt in packet.Packets)
            {
                CharacterNameTagPanelController.UnlockedLabels[pkt.DescriptorId] = pkt.IsNew;
                if (pkt.IsSelected)
                {
                    Globals.Me.LabelDescriptorId = pkt.DescriptorId;
                }
            }
            CharacterNameTagPanelController.RefreshLabels = true;
        }

        public void HandlePacket(IPacketSender packetSender, ToastPacket packet)
        {
            ToastService.SetToast(new Toast(packet.Message));
        }

        public void HandlePacket(IPacketSender packetSender, CosmeticUnlocksPacket packet)
        {
            var unlockedCosmetics = CharacterCosmeticsPanelController.UnlockedCosmetics;
            unlockedCosmetics.Clear();
            foreach(var cosmetic in packet.UnlockedCosmetics)
            {
                var item = ItemBase.Get(cosmetic);
                var slot = item?.EquipmentSlot ?? -1;

                // Bounds checking
                if (item == default 
                    || slot < 0 
                    || slot >= Options.CosmeticSlots.Count)
                {
                    continue;
                }

                // If we haven't added any cosmetics for this slot yet
                if (!unlockedCosmetics.TryGetValue(slot, out var slotCosmetics))
                {
                    unlockedCosmetics[slot] = new List<Guid> { cosmetic };
                    continue;
                }

                // Otherwise, add to the collection
                slotCosmetics.Add(cosmetic);
            }

            // Sort the items in each collection by name/display name
            var sortedCollection = new Dictionary<int, List<Guid>>();
            foreach (var slot in unlockedCosmetics.Keys)
            {
                sortedCollection[slot] = unlockedCosmetics[slot]
                    .ToList()
                    .Select(c => ItemBase.Get(c))
                    .Where(c => c != default)
                    .OrderBy(c => string.IsNullOrEmpty(c?.CosmeticDisplayName) ? c.Name : c.CosmeticDisplayName)
                    .Select(c => c.Id)
                    .ToList();
            }
            CharacterCosmeticsPanelController.UnlockedCosmetics = sortedCollection;

            CharacterCosmeticsPanelController.RefreshCosmeticsPanel = true;
        }

        public void HandlePacket(IPacketSender packetSender, RecipeDisplayPackets packet)
        {
            CharacterRecipePanelController.Recipes.Clear();
            CharacterRecipePanelController.Recipes.AddRange(packet.Packets);
            CharacterRecipePanelController.Refresh = true;
        }

        public void HandlePacket(IPacketSender packetSender, RecipeRequirementPackets packet)
        {
            var descId = packet.Packets.FirstOrDefault()?.RecipeId ?? Guid.Empty;

            if (descId == Guid.Empty)
            {
                return;
            }

            CharacterRecipePanelController.ExpandedRecipes[descId] = packet.Packets;
        }

        public void HandlePacket(IPacketSender packetSender, UnlockedRecipesPacket packet)
        {
            Globals.Me?.UnlockedRecipes?.Clear();
            Globals.Me?.UnlockedRecipes?.AddRange(packet.UnlockedRecipes);

            Interface.Interface.GameUi.UpdateCraftingTable();
        }

        public void HandlePacket(IPacketSender packetSender, KillCountsPacket packet)
        {
            BestiaryController.KnownKillCounts.Clear();
            foreach(var beast in packet.KillCounts)
            {
                BestiaryController.KnownKillCounts[beast.Key] = beast.Value;
            }

            BestiaryController.RefreshUnlocks(true);
        }

        public void HandlePacket(IPacketSender packetSender, KillCountPacket packet)
        {
            BestiaryController.KnownKillCounts[packet.NpcId] = packet.KillCount;
            BestiaryController.MyBestiary.UpdateUnlocksFor(packet.NpcId, packet.KillCount, false);
        }

        public void HandlePacket(IPacketSender packetSender, UnlockedBestiaryEntriesPacket packet)
        {
            BestiaryController.KnownUnlocks.Clear();
            BestiaryController.KnownUnlocks = packet.BestiaryUnlocks;

            if (packet.Refresh)
            {
                BestiaryController.RefreshUnlocks(false);
            }
        }
        
        public void HandlePacket(IPacketSender packetSender, ExpToastPacket packet)
        {
            if (packet.ComboEnder)
            {
                Audio.AddGameSound("al_combo_end.wav", false);
                ExpToastService.CreateExpToast(packet.ExpStr, "COMBO!", packet.IsWeapon && !packet.IsExp);
            }
            else
            {
                ExpToastService.CreateExpToast(packet.ExpStr, weaponExp: packet.IsWeapon && !packet.IsExp);
            }
        }

        public void HandlePacket(IPacketSender packetSender, SkillbookPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.Skillbook = packet.SkillBook;
            SkillsPanelController.RefreshAvailableSkillTypes();
            SkillsPanelController.RefreshDisplay = true;
            SkillsPanelController.SkillPointsAvailable = packet.SkillPointsAvailable;
            SkillsPanelController.SkillPointTotal = packet.SkillPointTotal;
        }

        public void HandlePacket(IPacketSender packetSender, ChallengeProgressPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            CharacterChallengesController.WeaponTypeProgresses = packet.WeaponTypes;
            CharacterChallengesController.CurrentContractId = packet.CurrentContractId;
            CharacterBonusesPanelController.Refresh = true;
        }

        public void HandlePacket(IPacketSender packetSender, SkillUpdatePacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.SkillUpdateString = packet.UpdateText;
            Globals.SkillPointUpdate = true;
        }

        public void HandlePacket(IPacketSender packetSender, SendOpenDeconstructorPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.Fuel = packet.CurrentFuel;
            Globals.Me.Deconstructor?.Open(packet.FuelCostMultiplier);
        }

        public void HandlePacket(IPacketSender packetSender, PlayerFuelPacket packet)
        {
            Globals.Me.Fuel = packet.Fuel;
            if (Globals.Me.Deconstructor == default)
            {
                return;
            }

            Globals.Me.Deconstructor?.CloseFuelAddition();
            Globals.Me.Deconstructor?.PlayFuelAddEffect();
            Globals.Me.Deconstructor.WaitingOnServer = false;
        }

        public void HandlePacket(IPacketSender packetSender, CloseDeconstructorPacket packet)
        {
            Interface.Interface.GameUi?.DeconstructorWindow?.ForceClose();

            if (Globals.Me.Deconstructor == default)
            {
                return;
            }
            Globals.Me.Deconstructor.WaitingOnServer = false;
        }

        public void HandlePacket(IPacketSender packetSender, OpenEnhancementPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.SetKnownEnhancements(packet.KnownEnhancements);

            WeaponPickerController.TryGetSelectedWeapon(out var weapon);
            Globals.Me.Enhancement?.Open(packet.CurrencyId, packet.CostMultiplier, weapon);
        }

        public void HandlePacket(IPacketSender packetSender, EnhancementEndPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Interface.Interface.GameUi.EnhancementWindow.ProcessCompletedEnhancement(packet.NewProperties);
        }

        public void HandlePacket(IPacketSender packetSender, OpenUpgradeStationPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.UpgradeStation?.Open(packet.CurrencyId, 
                packet.CostMultiplier, 
                packet.AvailableUpgrades, 
                ItemBase.Get(packet.UpgradingGuid), 
                packet.UpgradingProperties);
        }
        
        public void HandlePacket(IPacketSender packetSender, CompleteUpgradePacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Interface.Interface.GameUi?.UpgradeStationWindow?.ProcessCompletedUpgrade(packet.ItemId, packet.Properties);
        }

        public void HandlePacket(IPacketSender packetSender, KnownEnhancementsPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.SetKnownEnhancements(packet.KnownEnhancements);
            if (CharacterEnhancementsPanelController.IsOpen)
            {
                CharacterEnhancementsPanelController.UpdateEnhancementList();
            }
        }
        
        public void HandlePacket(IPacketSender packetSender, UsedPermabuffsPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.UsedPermabuffs.Clear();
            foreach(var pb in packet.UsedPermabuffs)
            {
                Globals.UsedPermabuffs.Add(pb);
            }
        }

        public void HandlePacket(IPacketSender packetSender, LoadoutsPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            CharacterLoadoutsController.LoadLoadouts(packet.Loadouts);
        }

        public void HandlePacket(IPacketSender packetSender, ConfirmLoadoutOverwritePacket packet)
        {
            if (Globals.Me == null || !CharacterLoadoutsController.TryGetLoadout(packet.LoadoutId, out Loadout loadout))
            {
                return;
            }

            var iBox = new InputBox(
                Strings.Loadouts.OverwriteLoadoutTitle, Strings.Loadouts.OverwriteLoadoutPromptAlreadyExists.ToString(loadout.Name), true, InputBox.InputType.YesNo,
                CharacterLoadoutsController.RequestLoadoutOverwritePrompt, null, packet.LoadoutId
            );
        }

        public void HandlePacket(IPacketSender packetSender, SetDuelOpponentPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            Globals.Me.DuelingIds.Clear();
            Globals.Me.DuelingIds.AddRange(packet.Opponents.Where(id => id != Globals.Me.Id));
        }

        public void HandlePacket(IPacketSender packetSender, OpenWeaponPickerPacket packet)
        {
            if (Globals.Me == null)
            {
                return;
            }

            WeaponPickerController.ResultType = packet.ResultType;
            Interface.Interface.GameUi.ShowWeaponPicker();
        }

        public void HandlePacket(IPacketSender packetSender, CraftingWishlistPacket packet)
        {
            if (packet.Wishlist != default)
            {
                CharacterWishlistController.Wishlist = packet.Wishlist.ToList();
            }

            CharacterWishlistController.ServerUpdate = true;
        }

        //MapAreaPacket
        public void HandlePacket(IPacketSender packetSender, TerritoriesPacket packet)
        {
            foreach (var territory in packet.Territories)
            {
                HandleTerritory(packetSender, territory);
            }
        }

        public void HandlePacket(IPacketSender packetSender, TerritoryUpdatePacket packet)
        {
            HandleTerritory(packetSender, packet);
        }

        private void HandleTerritory(IPacketSender packetSender, TerritoryUpdatePacket packet)
        {
            var map = MapInstance.Get(packet.MapId);
            if (Globals.Me == null || map == null)
            {
                return;
            }

            if (!map.Territories.TryGetValue(packet.DescriptorId, out var territory))
            {
                return;
            }

            territory.HandleServerUpdate(packet.State, packet.Owner, packet.Conquerer);

            ChatboxMsg.DebugMessage($"Territory update: STATE == {packet.State}");
        }
    }
}
 