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

namespace Intersect.Server.Entities
{

    public partial class Player : AttackingEntity
    {
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
                if (!lockObtained)
                {
                    return;
                }

                if (Client == null && CombatTimer < Timing.Global.Milliseconds) //Client logged out
                {
                    Logout();

                    return;
                }

                if (SaveTimer < Timing.Global.Milliseconds)
                {
                    SavePlayer();
                }

                // Are we craftin'?
                if (IsCrafting)
                {
                    UpdateCrafting(timeMs);
                }

                // Check if the resource we're locked to has died - if so, alert client
                if (resourceLock != null && resourceLock.IsDead())
                {
                    SetResourceLock(false);
                }

                base.Update(timeMs);

                if (mAutorunCommonEventTimer < Timing.Global.Milliseconds)
                {
                    UpdateAutorunEvents();
                }

                //If we have a move route then let's process it....
                if (MoveRoute != null && MoveTimer < timeMs)
                {
                    UpdateMoveRoute(timeMs);
                }

                //If we switched maps, lets update the maps
                if (LastMapEntered != MapId)
                {
                    UpdateMapChange();
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
                            surrMap.TryCreateInstance(MapInstanceId, out mapInstance, this);
                        }
                    }

                    //Check to see if we can spawn events, if already spawned.. update them.
                    RefreshAndAddEvents(surrMap, mapInstance, timeMs);
                }

                //Check to see if we can spawn events, if already spawned.. update them.
                UpdateEvents(map, timeMs);

                // Check to see if combos expired
                if (ComboWindow >= 0)
                {
                    // Detract from the window
                    ComboWindow = (int)(ComboTimestamp - Timing.Global.Milliseconds);
                    if (ComboWindow < 0)
                    {
                        EndCombo(); // This will also send a packet - this way, we're not flooding the client with packets when there's no active combo
                    }
                }

                ProcessEvents();
            }
            finally
            {
                if (lockObtained)
                {
                    Monitor.Exit(EntityLock);
                }
            }
        }

        private void UpdateEvents(MapController map, MapInstance mapInstance, long timeMs)
        {
            if (PlayerDead)
            {
                return;
            }

            var autorunEvents = 0;
            foreach (var mapEvent in mapInstance.EventsCache.ToArray())
            {
                if (mapEvent == null)
                {
                    continue;
                }

                lock (mEventLock)
                {
                    var loc = new MapTileLoc(map.Id, mapEvent.SpawnX, mapEvent.SpawnY);
                    var foundEvent = EventExists(loc);
                    if (foundEvent == null)
                    {
                        var tmpEvent = new Event(Guid.NewGuid(), map, this, mapEvent)
                        {
                            Global = mapEvent.Global,
                            MapId = map.Id,
                            SpawnX = mapEvent.SpawnX,
                            SpawnY = mapEvent.SpawnY
                        };

                        EventLookup.AddOrUpdate(tmpEvent.Id, tmpEvent, (key, oldValue) => tmpEvent);
                        EventBaseIdLookup.AddOrUpdate(mapEvent.Id, tmpEvent, (key, oldvalue) => tmpEvent);

                        EventTileLookup.AddOrUpdate(loc, tmpEvent, (key, oldvalue) => tmpEvent);
                    }
                    else
                    {
                        foundEvent.Update(timeMs, foundEvent.MapController);
                    }
                }

                if (Options.Instance.Metrics.Enable)
                {
                    autorunEvents += mapEvent.Pages.Count(p => p.Trigger == EventTrigger.Autorun);
                }
            }

            MapAutorunEvents = autorunEvents;
        }

        private void CollectEvents(MapController map, long timeMs)
        {
            if (PlayerDead)
            {
                return;
            }

            foreach (var evt in EventLookup.Values.ToArray())
            {
                if (evt == null)
                {
                    continue;
                }

                var eventFound = false;
                var eventMap = map;

                if (evt.MapId != Guid.Empty)
                {
                    if (evt.MapId != MapId)
                    {
                        eventMap = evt.MapController;
                        eventFound = map.SurroundingMapIds.Contains(eventMap.Id);
                    }
                    else
                    {
                        eventFound = true;
                    }
                }

                if (evt.MapId == Guid.Empty)
                {
                    evt.Update(timeMs, eventMap);
                    if (evt.CallStack.Count > 0)
                    {
                        eventFound = true;
                    }
                }

                if (!eventFound)
                {
                    RemoveEvent(evt.Id);
                }
            }
        }



        private void StopCrafting()
        {
            CraftId = Guid.Empty;
            CraftAmount = 0;
            PacketSender.SendCraftingStatusPacket(this, 0);
        }

        private void ContinueCrafting()
        {
            CraftTimer = Timing.Global.Milliseconds;
            PacketSender.SendCraftingStatusPacket(this, CraftAmount);
        }

        private void UpdateCrafting(long timeMs)
        {
            // Is it time to give the player the crafted item?
            if (CraftTimer + CraftBase.Get(CraftId).Time < timeMs)
            {
                // Try to give the player the item
                if (TryCraftItem(CraftId))
                {
                    // Update their craft amount
                    CraftAmount = MathHelper.Clamp(CraftAmount - 1, 0, int.MaxValue);
                    // If we've crafted all we've requested, we're done!
                    if (CraftAmount == 0)
                    {
                        StopCrafting();
                    }
                    // Otherwise, start the timer again for the next craft
                    else
                    {
                        ContinueCrafting();
                    }
                }
                else
                {
                    // If we couldn't craft the item, stop crafting
                    StopCrafting();
                }
            }
            else
            {
                // If at any point we can't craft the item anymore, cancel crafting
                if (!CheckCrafting(CraftId))
                {
                    StopCrafting();
                }
            }
        }

        private void SavePlayer()
        {
            var user = User;
            if (user != null)
            {
                DbInterface.Pool.QueueWorkItem(user.Save, false);
            }
            SaveTimer = Timing.Global.Milliseconds + Options.Instance.Processing.PlayerSaveInterval;
        }

        private void UpdateAutorunEvents()
        {
            var autorunEvents = 0;
            //Check for autorun common events and run them
            foreach (var obj in EventBase.Lookup)
            {
                var evt = obj.Value as EventBase;
                if (evt == null || !evt.CommonEvent)
                {
                    continue;
                }

                foreach (var page in evt.Pages)
                {
                    if (page.CommonTrigger != CommonEventTrigger.Autorun)
                    {
                        continue;
                    }

                    if (Options.Instance.Metrics.Enable)
                    {
                        autorunEvents += evt.Pages.Count(p => p.CommonTrigger == CommonEventTrigger.Autorun);
                    }
                    EnqueueStartCommonEvent(evt, CommonEventTrigger.Autorun);
                }
            }

            mAutorunCommonEventTimer = Timing.Global.Milliseconds + Options.Instance.Processing.CommonEventAutorunStartInterval;
            CommonAutorunEvents = autorunEvents;
        }

        private void UpdateMoveRoute(long timeMs)
        {
            //Check to see if the event instance is still active for us... if not then let's remove this route
            var foundEvent = false;
            foreach (var evt in EventLookup.ToArray())
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

        private void UpdateMapChange()
        {
            if (MapController.TryGetInstanceFromMap(LastMapEntered, MapInstanceId, out var oldMapInstance))
            {
                oldMapInstance.RemoveEntity(this);
            }

            if (MapId == Guid.Empty)
            {
                return;
            }

            if (!MapController.TryGetInstanceFromMap(MapId, MapInstanceId, out var newMapInstance))
            {
                WarpToSpawn();
                return;
            }

            newMapInstance.PlayerEnteredMap(this);
        }

        private void RefreshAndAddEvents(MapController map, MapInstance mapInstance, long timeMs)
        {
            if (PlayerDead)
            {
                return;
            }

            var autorunEvents = 0;
            foreach (var mapEvent in mapInstance.EventsCache.ToArray())
            {
                if (mapEvent == null)
                {
                    continue;
                }

                var loc = new MapTileLoc(map.Id, mapEvent.SpawnX, mapEvent.SpawnY);
                var foundEvent = EventExists(loc);
                if (foundEvent == null)
                {
                    var tmpEvent = new Event(Guid.NewGuid(), map, this, mapEvent)
                    {
                        Global = mapEvent.Global,
                        MapId = map.Id,
                        SpawnX = mapEvent.SpawnX,
                        SpawnY = mapEvent.SpawnY
                    };

                    EventLookup.AddOrUpdate(tmpEvent.Id, tmpEvent, (key, oldValue) => tmpEvent);
                    EventBaseIdLookup.AddOrUpdate(mapEvent.Id, tmpEvent, (key, oldvalue) => tmpEvent);

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

            MapAutorunEvents = autorunEvents;
        }

        private void UpdateEvents(MapController map, long timeMs)
        {
            if (PlayerDead)
            {
                return;
            }

            foreach (var evt in EventLookup.Values.ToArray())
            {
                if (evt == null)
                {
                    continue;
                }

                var eventFound = false;
                var eventMap = map;

                if (evt.MapId != Guid.Empty)
                {
                    if (evt.MapId != MapId)
                    {
                        eventMap = evt.MapController;
                        eventFound = map.SurroundingMapIds.Contains(eventMap.Id);
                    }
                    else
                    {
                        eventFound = true;
                    }
                }
                else
                {
                    evt.Update(timeMs, eventMap);
                    if (evt.CallStack.Count > 0)
                    {
                        eventFound = true;
                    }
                }

                if (!eventFound)
                {
                    RemoveEvent(evt.Id);
                }
            }
        }

        private void ProcessEvents()
        {
            // Deferred events are fired once-a-tick. They are for events that we don't care too much about being fired
            // at EXACTLY the right moment
            if (DeferredEventQueue.TryDequeue(out DeferredEvent evt))
            {
                StartCommonEventsWithTrigger(evt.Trigger, evt.Command, evt.Param, evt.Value);
            }

            lock (mEventLock)
            {
                // Any common events that have been queued up will finally fire here
                while (_queueStartCommonEvent.TryDequeue(out var startCommonEventMetadata))
                {
                    _ = UnsafeStartCommonEvent(
                        startCommonEventMetadata.EventDescriptor,
                        startCommonEventMetadata.Trigger,
                        startCommonEventMetadata.Command,
                        startCommonEventMetadata.Parameter,
                        startCommonEventMetadata.Value
                    );
                }
            }
        }
    }
}
