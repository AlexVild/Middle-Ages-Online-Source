using Intersect.Enums;
using Intersect.ErrorHandling;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.GameObjects.QuestList;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Maps.MapList;
using Intersect.Logging;
using Intersect.Models;
using Intersect.Network;
using Intersect.Network.Lidgren;
using Intersect.Network.Packets;
using Intersect.Network.Packets.Client;
using Intersect.Server.Admin.Actions;
using Intersect.Server.Core;
using Intersect.Server.Database;
using Intersect.Server.Database.Logging.Entities;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking.Lidgren;
using Intersect.Server.Notifications;
using Intersect.Utilities;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Intersect.GameObjects.Timers;
using Intersect.Server.Utilities;
using Intersect.Server.DTOs;
using Intersect.GameObjects.Switches_and_Variables;

namespace Intersect.Server.Networking
{
    internal sealed partial class PacketHandler
    {
        public IServerContext Context { get; }

        public Logger Logger => Context.Logger;

        public PacketHandlerRegistry Registry { get; }

        public static PacketHandler Instance { get; private set; }

        public static long ReceivedBytes => AcceptedBytes + DroppedBytes;

        public static long ReceivedPackets => AcceptedPackets + DroppedPackets;

        public static ConcurrentDictionary<string, long> AcceptedPacketTypes = new ConcurrentDictionary<string, long>();

        public static long AcceptedBytes { get; set; }

        public static long DroppedBytes { get; set; }

        public static long AcceptedPackets { get; set; }

        public static long DroppedPackets { get; set; }

        public static void ResetMetrics()
        {
            AcceptedBytes = 0;
            AcceptedPackets = 0;
            DroppedBytes = 0;
            DroppedPackets = 0;
        }

        public PacketHandler(IServerContext context, PacketHandlerRegistry packetHandlerRegistry)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Registry = packetHandlerRegistry ?? throw new ArgumentNullException(nameof(packetHandlerRegistry));

            if (!Registry.TryRegisterAvailableMethodHandlers(GetType(), this, false) || Registry.IsEmpty)
            {
                throw new InvalidOperationException("Failed to register method handlers, see logs for more details.");
            }

            Instance = this;
        }

        public bool PreProcessPacket(IConnection connection, long pSize)
        {
            if (ShouldAcceptPacket(connection, pSize))
            {
                AcceptedPackets++;
                AcceptedBytes += pSize;
                return true;
            }
            DroppedPackets++;
            DroppedBytes += pSize;
            return false;
        }

        public bool ShouldAcceptPacket(IConnection connection, long pSize)
        {
            var client = Client.FindBeta4Client(connection);
            if (client == null)
            {
                return false;
            }

            if (client.Banned || client.FloodKicked)
            {
                return false;
            }

            var packetOptions = Options.Instance.SecurityOpts?.PacketOpts;
            var thresholds = client.PacketFloodingThreshholds;


            if (pSize > thresholds.MaxPacketSize)
            {
                Log.Error(
                    Strings.Errors.floodsize.ToString(
                        pSize, client?.User?.Name ?? "", client?.Entity?.Name ?? "", client.GetIp()
                    )
                );

                client.FloodKicked = true;
                client.Disconnect("Flooding detected.");

                return false;
            }

            if (client.PacketTimer > Timing.Global.Milliseconds)
            {
                client.PacketCount++;
                if (client.PacketCount > thresholds.MaxPacketPerSec)
                {
                    Log.Error(
                        Strings.Errors.floodburst.ToString(
                            client.PacketCount, client?.User?.Name ?? "", client?.Entity?.Name ?? "", client.GetIp()
                        )
                    );

                    client.FloodKicked = true;
                    client.Disconnect("Flooding detected.");

                    return false;
                }
                else if (client.PacketCount > thresholds.KickAvgPacketPerSec && !client.PacketFloodDetect)
                {
                    client.FloodDetects++;
                    client.TotalFloodDetects++;
                    client.PacketFloodDetect = true;

                    if (client.FloodDetects > 3)
                    {
                        //Log.Error(
                        //    Strings.Errors.floodaverage.ToString(
                        //        client.TotalFloodDetects, client?.User?.Name ?? "", client?.Entity?.Name ?? "",
                        //        client.GetIp()
                        //    )
                        //);

                        client.FloodKicked = true;
                        client.Disconnect("Flooding detected.");

                        return false;
                    }

                    //TODO: Make this check a rolling average somehow to prevent constant flooding right below the threshholds.
                    if (client.TotalFloodDetects > 10)
                    {
                        //Log.Error(string.Format("[Flood]: Total Detections: {00} [User: {01} | Player: {02} | IP {03}]", client.TotalFloodDetects, client?.User?.Name ?? "", client?.Entity?.Name ?? "", client.GetIp()));
                        //client.Disconnect("Flooding detected.");
                        //return false;
                    }
                }
                else if (client.PacketCount < thresholds.KickAvgPacketPerSec / 2)
                {
                    if (client.FloodDetects > 1)
                    {
                        client.FloodDetects--;
                    }
                }
            }
            else
            {
                if (client.PacketFloodDetect)
                {
                    //Log.Error(string.Format("Possible Flood Detected: Packets in last second {00} [User: {01} | Player: {02} | IP {03}]", client.PacketCount, client?.User?.Name ?? "", client?.Entity?.Name ?? "", client.GetIp()));
                }

                client.PacketCount = 0;
                client.PacketTimer = Timing.Global.Milliseconds + 1000;
                client.PacketFloodDetect = false;
            }

            return true;
        }

        public bool HandlePacket(IConnection connection, IPacket packet)
        {
            packet.ReceiveTime = Timing.Global.Milliseconds;

            var client = Client.FindBeta4Client(connection);
            if (client == null)
            {
                Log.Error("Client was null when packet was being handled.");
                return false;
            }

            while (client.RecentPackets.Count > 75)
            {
                client.RecentPackets.TryDequeue(out IPacket pkt);
            }

            client.RecentPackets.Enqueue(packet);

            if (client.Banned)
            {
                return false;
            }

            switch (packet)
            {
                case Network.Packets.EditorPacket _ when !client.IsEditor:
                    return false;

                case null:
                    Log.Error($@"Received null packet from {client.Id} ({client.Name}).");
                    client.Disconnect("Error processing packet.");

                    return true;
            }

            if (!packet.IsValid)
            {
                return false;
            }

            try
            {
                var sanitizedFields = packet.Sanitize();
                if (sanitizedFields != null)
                {
                    var sanitizationBuilder = new StringBuilder(256, 8192);
                    sanitizationBuilder.Append("Received out-of-bounds values in '");
                    sanitizationBuilder.Append(packet.GetType().Name);
                    sanitizationBuilder.Append("' packet from '");
                    sanitizationBuilder.Append(client.GetIp());
                    sanitizationBuilder.Append("', '");
                    sanitizationBuilder.Append(client.Name);
                    sanitizationBuilder.AppendLine("': ");

                    foreach (var field in sanitizedFields)
                    {
                        sanitizationBuilder.Append(field.Key);
                        sanitizationBuilder.Append(" = ");
                        sanitizationBuilder.Append(field.Value.Before);
                        sanitizationBuilder.Append(" => ");
                        sanitizationBuilder.Append(field.Value.After);
                        sanitizationBuilder.AppendLine();
                    }

                    Log.Warn(sanitizationBuilder.ToString());
                }
            }
            catch (Exception exception)
            {
                Log.Error(
                    $"Client Packet Error! [Packet: {packet.GetType().Name} | User: {client.Name ?? ""} | Player: {client.Entity?.Name ?? ""} | IP {client.GetIp()}]"
                );

                Log.Error(exception);
                client.Disconnect("Error processing packet.");

                return false;
            }

            if (packet is AbstractTimedPacket timedPacket)
            {
                var ping = connection.Statistics.Ping;
                var ncPing = (long) Math.Ceiling(
                    (connection as LidgrenConnection).NetConnection.AverageRoundtripTime * 1000
                );

                var localAdjusted = Timing.Global.Ticks;
                var localAdjustedMs = localAdjusted / TimeSpan.TicksPerMillisecond;
                var localOffsetMs = Timing.Global.MillisecondsOffset;
                var localUtcMs = localOffsetMs + localAdjustedMs;

                var remoteAdjusted = timedPacket.Adjusted;
                var remoteAdjustedMs = remoteAdjusted / TimeSpan.TicksPerMillisecond;
                var remoteUtcMs = timedPacket.UTC / TimeSpan.TicksPerMillisecond;
                var remoteOffsetMs = timedPacket.Offset / TimeSpan.TicksPerMillisecond;

                var deltaAdjusted = localAdjustedMs - remoteAdjustedMs;
                var deltaWithPing = deltaAdjusted - ping;

                var configurableMininumPing = Options.Instance.SecurityOpts.PacketOpts.MinimumPing;
                var configurableErrorMarginFactor = Options.Instance.SecurityOpts.PacketOpts.ErrorMarginFactor;
                var configurableNaturalLowerMargin = Options.Instance.SecurityOpts.PacketOpts.NaturalLowerMargin;
                var configurableNaturalUpperMargin = Options.Instance.SecurityOpts.PacketOpts.NaturalUpperMargin;
                var configurableAllowedSpikePackets = Options.Instance.SecurityOpts.PacketOpts.AllowedSpikePackets;
                var configurableBaseDesyncForgiveness = Options.Instance.SecurityOpts.PacketOpts.BaseDesyncForegiveness;
                var configurablePingDesyncForgivenessFactor =
                    Options.Instance.SecurityOpts.PacketOpts.DesyncForgivenessFactor;

                var configurablePacketDesyncForgivenessInternal =
                    Options.Instance.SecurityOpts.PacketOpts.DesyncForgivenessInterval;

                var errorMargin = Math.Max(ping, configurableMininumPing) * configurableErrorMarginFactor;
                var errorRangeMinimum = ping - errorMargin;
                var errorRangeMaximum = ping + errorMargin;

                var deltaWithErrorMinimum = deltaAdjusted - errorRangeMinimum;
                var deltaWithErrorMaximum = deltaAdjusted - errorRangeMaximum;

                var natural = configurableNaturalLowerMargin < deltaAdjusted &&
                              deltaAdjusted < configurableNaturalUpperMargin;

                var naturalWithErrorMinimum = configurableNaturalLowerMargin < deltaWithErrorMinimum &&
                                              deltaWithErrorMinimum < configurableNaturalUpperMargin;

                var naturalWithErrorMaximum = configurableNaturalLowerMargin < deltaWithErrorMaximum &&
                                              deltaWithErrorMaximum < configurableNaturalUpperMargin;

                var naturalWithPing = configurableNaturalLowerMargin < deltaWithPing &&
                                      deltaWithPing < configurableNaturalUpperMargin;

                var adjustedDesync = Math.Abs(deltaAdjusted);
                var timeDesync = adjustedDesync >
                                 configurableBaseDesyncForgiveness +
                                 errorRangeMaximum * configurablePingDesyncForgivenessFactor;

                if (timeDesync && Timing.Global.MillisecondsUtc > client.LastPacketDesyncForgiven)
                {
                    client.LastPacketDesyncForgiven =
                        Timing.Global.MillisecondsUtc + configurablePacketDesyncForgivenessInternal;

                    PacketSender.SendPing(client, false);
                    timeDesync = false;
                }

                if (Debugger.IsAttached)
                {
                    Log.Debug(
                        "\n\t" +
                        $"Ping[Connection={ping}, NetConnection={ncPing}, Error={Math.Abs(ncPing - ping)}]\n\t" +
                        $"Error[G={Math.Abs(localAdjustedMs - remoteAdjustedMs)}, R={Math.Abs(localUtcMs - remoteUtcMs)}, O={Math.Abs(localOffsetMs - remoteOffsetMs)}]\n\t" +
                        $"Delta[Adjusted={deltaAdjusted}, AWP={deltaWithPing}, AWEN={deltaWithErrorMinimum}, AWEX={deltaWithErrorMaximum}]\n\t" +
                        $"Natural[A={natural} WP={naturalWithPing}, WEN={naturalWithErrorMinimum}, WEX={naturalWithErrorMaximum}]\n\t" +
                        $"Time Desync[{timeDesync}]\n\t" +
                        $"Packet[{packet.ToString()}]"
                    );
                }

                var naturalWithError = naturalWithErrorMinimum || naturalWithErrorMaximum;

                if (!(natural || naturalWithError || naturalWithPing) || timeDesync)
                {
                    //No matter what, let's send the ping to resync time.
                    PacketSender.SendPing(client, false);

                    if (client.TimedBufferPacketsRemaining-- < 1 || timeDesync)
                    {
                        //if (!(packet is PingPacket))
                        //{
                        //    Log.Warn(
                        //        "Dropping Packet. Time desync? Debug Info:\n\t" +
                        //        $"Ping[Connection={ping}, NetConnection={ncPing}, Error={Math.Abs(ncPing - ping)}]\n\t" +
                        //        $"Server Time[Ticks={Timing.Global.Ticks}, AdjustedMs={localAdjustedMs}, TicksUTC={Timing.Global.TicksUTC}, Offset={Timing.Global.TicksOffset}]\n\t" +
                        //        $"Client Time[Ticks={timedPacket.Adjusted}, AdjustedMs={remoteAdjustedMs}, TicksUTC={timedPacket.UTC}, Offset={timedPacket.Offset}]\n\t" +
                        //        $"Error[G={Math.Abs(localAdjustedMs - remoteAdjustedMs)}, R={Math.Abs(localUtcMs - remoteUtcMs)}, O={Math.Abs(localOffsetMs - remoteOffsetMs)}]\n\t" +
                        //        $"Delta[Adjusted={deltaAdjusted}, AWP={deltaWithPing}, AWEN={deltaWithErrorMinimum}, AWEX={deltaWithErrorMaximum}]\n\t" +
                        //        $"Natural[A={natural} WP={naturalWithPing}, WEN={naturalWithErrorMinimum}, WEX={naturalWithErrorMaximum}]\n\t" +
                        //        $"Time Desync[{timeDesync}]\n\t" +
                        //        $"Packet[{packet}]"
                        //    );
                        //}

                        try
                        {
                            HandleDroppedPacket(client, packet);
                        }
                        catch (Exception exception)
                        {
                            Log.Debug(
                                exception,
                                $"Exception thrown dropping packet ({packet.GetType().Name}/{client.GetIp()}/{client.Name ?? ""}/{client.Entity?.Name ?? ""})"
                            );
                        }

                        return false;
                    }
                }
                else if (natural && naturalWithPing && naturalWithError)
                {
                    client.TimedBufferPacketsRemaining = configurableAllowedSpikePackets;
                }
                else if (natural && naturalWithPing ||
                         naturalWithPing && naturalWithError ||
                         naturalWithError && natural)
                {
                    client.TimedBufferPacketsRemaining += (int) Math.Ceiling(
                        (configurableAllowedSpikePackets - client.TimedBufferPacketsRemaining) / 2.0
                    );
                }
                else
                {
                    client.TimedBufferPacketsRemaining = Math.Min(configurableAllowedSpikePackets, client.TimedBufferPacketsRemaining++);
                }
            }

            if (!AcceptedPacketTypes.ContainsKey(packet.GetType().Name))
            {
                AcceptedPacketTypes.TryAdd(packet.GetType().Name, 0);
            }
            AcceptedPacketTypes[packet.GetType().Name]++;

            client.HandlePacketQueue.Enqueue(packet);
            lock (client.HandlePacketQueue)
            {
                if (!client.PacketHandlingQueued)
                {
                    client.PacketHandlingQueued = true;
                    ServerNetwork.Pool.QueueWorkItem(client.HandlePackets);
                }
            }

            return true;
        }

        public bool ProcessPacket(IPacket packet, Client client)
        {
            if (!Registry.TryGetHandler(packet, out HandlePacketGeneric handler))
            {
                Logger.Error($"No registered handler for {packet.GetType().FullName}!");

                return false;
            }

            if (Registry.TryGetPreprocessors(packet, out var preprocessors))
            {
                if (!preprocessors.All(preprocessor => preprocessor.Handle(client, packet)))
                {
                    // Preprocessors are intended to be silent filter functions
                    return false;
                }
            }

            if (Registry.TryGetPreHooks(packet, out var preHooks))
            {
                if (!preHooks.All(hook => hook.Handle(client, packet)))
                {
                    // Hooks should not fail, if they do that's an error
                    Logger.Error($"PreHook handler failed for {packet.GetType().FullName}.");
                    return false;
                }
            }

            if (!handler(client, packet))
            {
                return false;
            }

            if (Registry.TryGetPostHooks(packet, out var postHooks))
            {
                if (!postHooks.All(hook => hook.Handle(client, packet)))
                {
                    // Hooks should not fail, if they do that's an error
                    Logger.Error($"PostHook handler failed for {packet.GetType().FullName}.");
                    return false;
                }
            }

            return true;
        }

        #region "Client Packets"

        public void HandleDroppedPacket(Client client, IPacket packet)
        {
            switch (packet)
            {
                case MovePacket _:
                    PacketSender.SendEntityPositionTo(client, client.Entity);

                    break;
            }
        }

        //PingPacket
        public void HandlePacket(Client client, PingPacket packet)
        {
            client.Pinged();
            if (!packet.Responding)
            {
                PacketSender.SendPing(client, false);
            }
        }

        //LoginPacket
        public void HandlePacket(Client client, LoginPacket packet)
        {
            if (client.AccountAttempts > 3 && client.TimeoutMs > Timing.Global.Milliseconds)
            {
                PacketSender.SendError(client, Strings.Errors.errortimeout, resetUi: false);
                client.ResetTimeout();

                return;
            }

            client.ResetTimeout();

            // Are we at capacity yet, or can this user still log in?
            if (Globals.OnlineList.Count >= Options.MaxLoggedinUsers)
            {
                PacketSender.SendError(client, Strings.Networking.ServerFull, resetUi: false);

                return;
            }

            var user = User.TryLogin(packet.Username, packet.Password);
            if (user == null)
            {
                UserActivityHistory.LogActivity(Guid.Empty, Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.FailedLogin, packet.Username);

                client.FailedAttempt();
                PacketSender.SendError(client, Strings.Account.badlogin, resetUi: false);

                return;
            }

            lock (Globals.ClientLock)
            {
                foreach (var cli in Globals.Clients.ToArray())
                {
                    if (cli == client)
                    {
                        continue;
                    }

                    if (cli?.IsEditor ?? false)
                    {
                        continue;
                    }

                    if (!string.Equals(cli?.Name, packet.Username, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }

                    cli?.Disconnect();
                }
            }

            client.SetUser(user);

            if (client.User != null)
            {
                //Logged In
                client.PacketFloodingThreshholds = Options.Instance.SecurityOpts.PacketOpts.PlayerThreshholds;

                if (client.User.Power.IsAdmin || client.User.Power.IsModerator)
                {
                    client.PacketFloodingThreshholds = Options.Instance.SecurityOpts.PacketOpts.ModAdminThreshholds;
                }
            }

            //Check for ban
            var isBanned = Ban.CheckBan(client.User, client.GetIp());
            if (isBanned != null)
            {
                client.SetUser(null);
                client.Banned = true;
                PacketSender.SendError(client, isBanned);

                return;
            }

            //Check that server is in admin only mode
            if (Options.AdminOnly)
            {
                if (client.Power == UserRights.None)
                {
                    PacketSender.SendError(client, Strings.Account.adminonly);

                    return;
                }
            }

            //Check Mute Status and Load into user property
            Mute.FindMuteReason(client.User, client.GetIp());

            UserActivityHistory.LogActivity(user?.Id ?? Guid.Empty, Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.Login, null);

            PacketSender.SendServerConfig(client);

            //Check if we already have a player online/stuck in combat.. if so we will login straight to him
            foreach (var chr in client.Characters)
            {
                if (Player.FindOnline(chr.Id) != null)
                {
                    client.LoadCharacter(chr);
                    client.Entity.SetOnline();

                    PacketSender.SendJoinGame(client);
                    return;
                }
            }

            //Otherwise proceed with login normally...
            if (Options.MaxCharacters > 1)
            {
                PacketSender.SendPlayerCharacters(client);
            }
            else if (client.Characters?.Count > 0)
            {
                client.LoadCharacter(client.Characters.First());
                client.Entity.SetOnline();

                PacketSender.SendJoinGame(client);
            }
            else
            {
                PacketSender.SendGameObjects(client, GameObjectType.Class);
                PacketSender.SendCreateCharacter(client);
            }
        }

        //LogoutPacket
        public void HandlePacket(Client client, LogoutPacket packet)
        {
            if (client != null)
            {
                UserActivityHistory.LogActivity(client?.User?.Id ?? Guid.Empty, Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, packet.ReturningToCharSelect ? UserActivityHistory.UserAction.SwitchPlayer : UserActivityHistory.UserAction.DisconnectLogout, $"{client?.Name},{client?.Entity?.Name}");

                if (Options.MaxCharacters > 1 && packet.ReturningToCharSelect)
                {
                    client.Entity?.TryLogout(false, true);
                    client.Entity = null;
                    PacketSender.SendPlayerCharacters(client);
                }
                else
                {
                    client?.Logout();
                }
            }
        }

        //NeedMapPacket
        public void HandlePacket(Client client, NeedMapPacket packet)
        {
            var player = client?.Entity;
            var map = MapController.Get(packet.MapId);
            if (map != null)
            {
                PacketSender.SendMap(client, packet.MapId);
                if (player != null && packet.MapId == player.MapId)
                {
                    PacketSender.SendMapGrid(client, map.MapGrid);
                }
            }
        }

        //MovePacket
        public void HandlePacket(Client client, MovePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            //check if player is stunned or snared, if so don't let them move.
            foreach (var status in player.CachedStatuses)
            {
                if (status.Type == StatusTypes.Stun ||
                    status.Type == StatusTypes.Snare ||
                    status.Type == StatusTypes.Sleep)
                {
                    return;
                }
            }

            if (!TileHelper.IsTileValid(packet.MapId, packet.X, packet.Y))
            {
                //POSSIBLE HACKING ATTEMPT!
                PacketSender.SendEntityPositionTo(client, client.Entity);

                return;
            }

            var clientTime = packet.Adjusted / TimeSpan.TicksPerMillisecond;
            if (player.ClientMoveTimer <= clientTime &&
                (Options.Instance.PlayerOpts.AllowCombatMovement || player.ClientAttackTimer <= clientTime))
            {
                var canMove = player.CanMove(packet.Dir);
                if ((canMove == -1 || canMove == -4) && client.Entity.MoveRoute == null)
                {
                    player.Move(packet.Dir, player, false, faceDirection: packet.FaceDir ?? -1);
                    var utcDeltaMs = (Timing.Global.TicksUTC - packet.UTC) / TimeSpan.TicksPerMillisecond;
                    var latencyAdjustmentMs = -(client.Ping + Math.Max(0, utcDeltaMs));
                    var currentMs = packet.ReceiveTime;
                    if (player.MoveTimer > currentMs)
                    {
                        player.MoveTimer = currentMs + latencyAdjustmentMs + (long) (player.GetMovementTime() * .75f);
                        player.ClientMoveTimer = clientTime + (long) player.GetMovementTime();
                    }
                }
                else
                {
                    PacketSender.SendEntityPositionTo(client, client.Entity);
                    return;
                }
            }
            else
            {
                PacketSender.SendEntityPositionTo(client, client.Entity);

                return;
            }

            if (packet.MapId != client.Entity.MapId || packet.X != client.Entity.X || packet.Y != client.Entity.Y)
            {
                PacketSender.SendEntityPositionTo(client, client.Entity);
            }
        }

        //ChatMsgPacket
        public void HandlePacket(Client client, ChatMsgPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            var msg = packet.Message;
            var channel = packet.Channel;
            
            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }
            
            if (client?.User.IsMuted ?? false) //Don't let the tongueless toxic kids speak.
            {
                PacketSender.SendChatMsg(player, client?.User?.Mute?.Reason, ChatMessageType.Notice);

                return;
            }

            if (player.LastChatTime > Timing.Global.MillisecondsUtc)
            {
                PacketSender.SendChatMsg(player, Strings.Chat.toofast, ChatMessageType.Notice);
                player.LastChatTime = Timing.Global.MillisecondsUtc + Options.MinChatInterval;

                return;
            }

            if (packet.Message.Length > Options.MaxChatLength)
            {
                return;
            }

            //If no /command, then use the designated channel.
            var cmd = "";
            if (!msg.StartsWith("/"))
            {
                switch (channel)
                {
                    case 0: //local
                        cmd = Strings.Chat.localcmd;

                        break;

                    case 1: //global
                        cmd = Strings.Chat.allcmd;

                        break;

                    case 2: //party
                        cmd = Strings.Chat.partycmd;

                        break;

                    case 3:
                        cmd = Strings.Guilds.guildcmd;
                        break;

                    case 4: //admin
                        cmd = Strings.Chat.admincmd;

                        break;

                    case 5: //private
                        PacketSender.SendChatMsg(player, msg, ChatMessageType.Local);

                        return;
                }
            }
            else
            {
                cmd = msg.Split()[0].ToLower();
                msg = msg.Remove(0, cmd.Length);
            }

            var msgSplit = msg.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (cmd == Strings.Chat.localcmd)
            {
                if (msg.Trim().Length == 0)
                {
                    return;
                }

                var chatColor = CustomColors.Chat.LocalChat;

                if (client?.Power.IsAdmin ?? false)
                {
                    chatColor = CustomColors.Chat.AdminLocalChat;
                }
                else if (client?.Power.IsModerator ?? false)
                {
                    chatColor = CustomColors.Chat.ModLocalChat;
                }

                PacketSender.SendProximityMsgToLayer(
                    Strings.Chat.local.ToString(player.Name, msg), ChatMessageType.Local, player.MapId, player.MapInstanceId, chatColor,
                    player.Name
                );
                PacketSender.SendChatBubble(player.Id, player.MapInstanceId, (int) EntityTypes.GlobalEntity, msg, player.MapId, Network.Packets.Server.ChatBubbleType.Public);
                ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.Local, Guid.Empty);
            }
            else if (cmd == Strings.Chat.allcmd || cmd == Strings.Chat.globalcmd)
            {
                if (msg.Trim().Length == 0)
                {
                    return;
                }

                var chatColor = CustomColors.Chat.GlobalChat;
                if (client?.Power.IsAdmin ?? false)
                {
                    chatColor = CustomColors.Chat.AdminGlobalChat;
                }
                else if (client?.Power.IsModerator ?? false)
                {
                    chatColor = CustomColors.Chat.ModGlobalChat;
                }

                PacketSender.SendGlobalMsg(Strings.Chat.Global.ToString(player.Name, msg), chatColor, player.Name);
                ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.Global, Guid.Empty);
            }
            else if (cmd == Strings.Chat.partycmd)
            {
                if (msg.Trim().Length == 0)
                {
                    return;
                }

                if (player.InParty(player))
                {
                    PacketSender.SendPartyMsg(
                        player, Strings.Chat.party.ToString(player.Name, msg), CustomColors.Chat.PartyChat, player.Name
                    );
                    PacketSender.SendChatBubble(player.Id, player.MapInstanceId, (int)EntityTypes.GlobalEntity, msg, player.MapId, Network.Packets.Server.ChatBubbleType.Party);
                    ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.Party, Guid.Empty);
                }
                else
                {
                    PacketSender.SendChatMsg(player, Strings.Parties.notinparty, ChatMessageType.Party, CustomColors.Alerts.Error);
                }
            }
            else if (cmd == Strings.Chat.admincmd)
            {
                if (msg.Trim().Length == 0)
                {
                    return;
                }

                if (client?.Power.IsModerator ?? false)
                {
                    PacketSender.SendAdminMsg(
                        Strings.Chat.admin.ToString(player.Name, msg), CustomColors.Chat.AdminChat, player.Name
                    );
                    ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.Admin, Guid.Empty);
                }
            }
            else if (cmd == Strings.Guilds.guildcmd)
            {
                if (player.Guild == null)
                {
                    PacketSender.SendChatMsg(player, Strings.Guilds.NotInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    return;
                }

                if (msg.Trim().Length == 0)
                {
                    return;
                }

                //Normalize Rank
                var rank = Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(player.GuildRank, Options.Instance.Guild.Ranks.Length - 1))].Title;
                PacketSender.SendGuildMsg(player, Strings.Guilds.guildchat.ToString(rank, player.Name, msg), CustomColors.Chat.GuildChat);
                PacketSender.SendChatBubble(player.Id, player.MapInstanceId, (int)EntityTypes.GlobalEntity, msg, player.MapId, Network.Packets.Server.ChatBubbleType.Guild);
                ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.Guild, player.Guild.Id);
            }
            else if (cmd == Strings.Chat.announcementcmd)
            {
                if (msg.Trim().Length == 0)
                {
                    return;
                }

                if (client?.Power.IsModerator ?? false)
                {
                    PacketSender.SendGlobalMsg(
                        Strings.Chat.announcement.ToString(player.Name, msg), CustomColors.Chat.AnnouncementChat,
                        player.Name
                    );

                    // Show an announcement banner if configured to do so as well!
                    if (Options.Chat.ShowAnnouncementBanners)
                    {
                        // TODO: Make the duration configurable through chat?
                        PacketSender.SendGameAnnouncement(msg, Options.Chat.AnnouncementDisplayDuration);
                    }

                    ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.Notice, Guid.Empty);
                }
            }
            else if (cmd == Strings.Chat.pmcmd || cmd == Strings.Chat.messagecmd)
            {
                if (msgSplit.Length < 2)
                {
                    return;
                }

                msg = msg.Remove(0, msgSplit[0].Length + 1); //Chop off the player name parameter

                if (msg.Trim().Length == 0)
                {
                    return;
                }

                var target = Player.FindOnline(msgSplit[0].ToLower());

                if (target == player)
                {
                    return;
                }

                if (target != null)
                {
                    PacketSender.SendChatMsg(
                        player, Strings.Chat.Private.ToString(player.Name, msg), ChatMessageType.PM, CustomColors.Chat.PrivateChat,
                        player.Name
                    );

                    PacketSender.SendChatMsg(
                        target, Strings.Chat.Private.ToString(player.Name, msg), ChatMessageType.PM,
                        CustomColors.Chat.PrivateChat, player.Name
                    );

                    target.ChatTarget = player;
                    player.ChatTarget = target;
                    ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.PM, target?.Id ?? Guid.Empty);
                }
                else
                {
                    PacketSender.SendChatMsg(player, Strings.Player.offline, ChatMessageType.PM, CustomColors.Alerts.Error);
                }
            }
            else if (cmd == Strings.Chat.replycmd || cmd == Strings.Chat.rcmd)
            {
                if (msg.Trim().Length == 0)
                {
                    return;
                }

                if (player.ChatTarget != null && Player.FindOnline(player.ChatTarget.Id) != null)
                {
                    PacketSender.SendChatMsg(
                        player, Strings.Chat.Private.ToString(player.Name, msg), ChatMessageType.PM, CustomColors.Chat.PrivateChat,
                        player.Name
                    );

                    PacketSender.SendChatMsg(
                        player.ChatTarget, Strings.Chat.Private.ToString(player.Name, msg), ChatMessageType.PM,
                        CustomColors.Chat.PrivateChat, player.Name
                    );

                    player.ChatTarget.ChatTarget = player;
                    ChatHistory.LogMessage(player, msg.Trim(), ChatMessageType.PM, player.ChatTarget?.Id ?? Guid.Empty);
                }
                else
                {
                    PacketSender.SendChatMsg(player, Strings.Player.offline, ChatMessageType.PM, CustomColors.Alerts.Error);
                }
            }
            else
            {
                //Search for command activated events and run them
                foreach (var evt in EventBase.Lookup)
                {
                    var eventDescriptor = evt.Value as EventBase;
                    if (eventDescriptor == default)
                    {
                        continue;
                    }

                    if (client.Entity.UnsafeStartCommonEvent(eventDescriptor, CommonEventTrigger.SlashCommand, cmd.TrimStart('/'), msg))
                    {
                        return; //Found our /command, exit now :)
                    }
                }

                //No common event /command, invalid command.
                PacketSender.SendChatMsg(player, Strings.Commands.invalid, ChatMessageType.Error, CustomColors.Alerts.Error);
            }
        }

        //BlockPacket
        public void HandlePacket(Client client, BlockPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }
        }

        //BumpPacket
        public void HandlePacket(Client client, BumpPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.TryBumpEvent(packet.MapId, packet.EventId);
        }

        //AttackPacket
        public void HandlePacket(Client client, AttackPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            var target = packet.Target;

            var clientTime = packet.Adjusted / TimeSpan.TicksPerMillisecond;
            if (player.ClientAttackTimer > clientTime ||
                (!Options.Instance.PlayerOpts.AllowCombatMovement && player.ClientMoveTimer > clientTime))
            {
                return;
            }

            if (!player.MeleeAvailable())
            {
                return;
            }

            var utcDeltaMs = (Timing.Global.TicksUTC - packet.UTC) / TimeSpan.TicksPerMillisecond;
            var latencyAdjustmentMs = -(client.Ping + Math.Max(0, utcDeltaMs));

            player.IncrementAttackTimer();
            player.ClientAttackTimer = clientTime + (long)player.CalculateAttackTime();
            
            // Make the client see the attack, even if it misses!
            PacketSender.SendEntityAttack(player, player.CalculateAttackTime());
            player.SendAttackAnimation(null);
            player.StealthAttack = player.IsStealthed;
            player.Unstealth();

            // Spawn projectiles if can/need be
            if (player.TrySpawnWeaponProjectile(latencyAdjustmentMs))
            {
                return;
            }

            // Else, melee attack the target
            var targetHit = false;
            foreach (var mapInstance in MapController.GetSurroundingMapInstances(player.Map.Id, player.MapInstanceId, true))
            {
                foreach (var entity in mapInstance.GetEntities())
                {
                    if (entity.Id == target)
                    {
                        player.MeleeAttack(entity, false);
                        targetHit = true;

                        break;
                    }
                }
            }

            if (!targetHit)
            {
                player.OnAttackMissed(null);
            }

            // lag compensation
            if (player.AttackTimer > Timing.Global.Milliseconds)
            {
                player.AttackTimer = Timing.Global.Milliseconds + latencyAdjustmentMs + player.CalculateAttackTime();
            }
        }

        //DirectionPacket
        public void HandlePacket(Client client, DirectionPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.ChangeDir(packet.Direction);
        }

        //EnterGamePacket
        public void HandlePacket(Client client, EnterGamePacket packet)
        {
        }

        //ActivateEventPacket
        public void HandlePacket(Client client, ActivateEventPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.TryActivateEvent(packet.EventId);
        }

        //EventResponsePacket
        public void HandlePacket(Client client, EventResponsePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.RespondToEvent(packet.EventId, packet.Response);
        }

        //EventInputVariablePacket
        public void HandlePacket(Client client, EventInputVariablePacket packet)
        {
            ((Player) client.Entity).RespondToEventInput(
                packet.EventId, packet.Value, packet.StringValue, packet.Canceled
            );
        }

        //CreateAccountPacket
        public void HandlePacket(Client client, CreateAccountPacket packet)
        {
            if (client.TimeoutMs > Timing.Global.Milliseconds)
            {
                PacketSender.SendError(client, Strings.Errors.errortimeout, resetUi: false);
                client.ResetTimeout();

                return;
            }

            client.ResetTimeout();

            if (Options.BlockClientRegistrations)
            {
                PacketSender.SendError(client, Strings.Account.registrationsblocked, resetUi: false);

                return;
            }

            if (!FieldChecking.IsValidUsername(packet.Username, Strings.Regex.username))
            {
                PacketSender.SendError(client, Strings.Account.invalidname, resetUi: false);

                return;
            }

            if (!FieldChecking.IsWellformedEmailAddress(packet.Email, Strings.Regex.email))
            {
                PacketSender.SendError(client, Strings.Account.invalidemail, resetUi: false);

                return;
            }

            //Check for ban
            var isBanned = Ban.CheckBan(client.GetIp());
            if (isBanned != null)
            {
                PacketSender.SendError(client, isBanned);

                return;
            }

            if (User.UserExists(packet.Username))
            {
                PacketSender.SendError(client, Strings.Account.exists);
            }
            else
            {
                if (User.UserExists(packet.Email))
                {
                    PacketSender.SendError(client, Strings.Account.emailexists);
                }
                else
                {

                    UserActivityHistory.LogActivity(client.User?.Id ?? Guid.Empty, Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.Create, client?.Name);

                    DbInterface.CreateAccount(client, packet.Username, packet.Password, packet.Email);

                    if (client.User != null)
                    {
                        //Logged In
                        client.PacketFloodingThreshholds = Options.Instance.SecurityOpts.PacketOpts.PlayerThreshholds;

                        if (client.User.Power.IsAdmin || client.User.Power.IsModerator)
                        {
                            client.PacketFloodingThreshholds = Options.Instance.SecurityOpts.PacketOpts.ModAdminThreshholds;
                        }
                    }

                    PacketSender.SendServerConfig(client);

                    //Check that server is in admin only mode
                    if (Options.AdminOnly)
                    {
                        if (client.Power == UserRights.None)
                        {
                            PacketSender.SendError(client, Strings.Account.adminonly);

                            return;
                        }
                    }

                    //Character selection if more than one.
                    if (Options.MaxCharacters > 1)
                    {
                        PacketSender.SendPlayerCharacters(client);
                    }
                    else
                    {
                        PacketSender.SendGameObjects(client, GameObjectType.Class);
                        PacketSender.SendCreateCharacter(client);
                    }
                }
            }
        }

        //CreateCharacterPacket
        public void HandlePacket(Client client, CreateCharacterPacket packet)
        {
            if (client.User == null)
            {
                return;
            }

            if (!FieldChecking.IsValidUsername(packet.Name, Strings.Regex.username))
            {
                PacketSender.SendError(client, Strings.Account.invalidname, resetUi: false);

                return;
            }

            var index = client.Id;
            var classBase = ClassBase.Get(packet.ClassId);
            if (classBase == null || classBase.Locked)
            {
                PacketSender.SendError(client, Strings.Account.invalidclass, resetUi: false);

                return;
            }

            if (Player.PlayerExists(packet.Name))
            {
                PacketSender.SendError(client, Strings.Account.characterexists, resetUi: false);
                return;
            }

            var newChar = new Player();
            newChar.Id = Guid.NewGuid();
            newChar.ValidateLists();
            for (var i = 0; i < Options.EquipmentSlots.Count; i++)
            {
                newChar.Equipment[i] = -1;
            }

            newChar.Name = packet.Name;
            newChar.ClassId = packet.ClassId;
            newChar.Level = 1;
            newChar.Decor = packet.Decors;

            if (classBase.Sprites.Count > 0)
            {
                var spriteIndex = Math.Max(0, Math.Min(classBase.Sprites.Count, packet.Sprite));
                newChar.Sprite = classBase.Sprites[spriteIndex].Sprite;
                newChar.Face = classBase.Sprites[spriteIndex].Face;
                newChar.Gender = classBase.Sprites[spriteIndex].Gender;
            }

            client.LoadCharacter(newChar);

            newChar.SetVital(Vitals.Health, classBase.BaseVital[(int) Vitals.Health]);
            newChar.SetVital(Vitals.Mana, classBase.BaseVital[(int) Vitals.Mana]);

            for (var i = 0; i < (int) Stats.StatCount; i++)
            {
                newChar.Stat[i].BaseStat = 0;
            }

            newChar.StatPoints = classBase.BasePoints;

            for (var i = 0; i < classBase.Spells.Count; i++)
            {
                if (classBase.Spells[i].Level <= 1)
                {
                    newChar.TryAddSkillToBook(classBase.Spells[i].Id);
                }
            }

            foreach (var item in classBase.Items)
            {
                if (ItemBase.Get(item.Id) != null)
                {
                    var tempItem = new Item(item.Id, item.Quantity);
                    newChar.TryGiveItem(tempItem, ItemHandling.Normal, false, false);
                }
            }

            UserActivityHistory.LogActivity(client?.User?.Id ?? Guid.Empty, client?.Entity?.Id ?? Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.CreatePlayer, $"{client?.Name},{client?.Entity?.Name}");

            client.User.AddCharacter(newChar);
            newChar.SetOnline();

            PacketSender.SendJoinGame(client);
        }

        //PickupItemPacket
        public void HandlePacket(Client client, PickupItemPacket packet)
        {
            var player = client.Entity;
            if (player == null || packet.TileIndex < 0 || packet.TileIndex >= Options.MapWidth * Options.MapHeight)
            {
                return;
            }

            player.TryPickupMapItem(packet.UniqueId, packet.MapId, packet.TileIndex);
        }

        //SwapInvItemsPacket
        public void HandlePacket(Client client, SwapInvItemsPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.SwapItems(packet.Slot1, packet.Slot2);
        }

        //DropItemPacket
        public void HandlePacket(Client client, DropItemPacket packet)
        {
            var player = client?.Entity;
            if (IsMultislotTransfer(packet, player))
            {
                player.MultislotTransfer(packet.Slots, packet.Quantity, Player.ItemMovementType.Drop);
                return;
            }
            else if (!player.TryDropItemFrom(packet.Slot, packet.Quantity))
            {
                player?.SendAlert();
            }
        }

        private bool IsMultislotTransfer(SlotQuantityPacket packet, Player player)
        {
            if (player == null || packet == null)
            {
                return false;
            }

            if (packet.Slots == default)
            {
                return false;
            }
            return true;
        }

        //DestroyItemPacket
        public void HandlePacket(Client client, DestroyItemPacket packet)
        {
            var player = client?.Entity;
            if (packet == null)
            {
                return;
            }

            // When a player tries to destroy an item, we first reach out to the server to check if we can destroy the item
            if (packet.CheckCanDrop)
            {
                // We let the client know if we CAN - if we can, we present a destroy interface, else, a drop interface.
                PacketSender.SendDestroyConditionPacket(player, player.CanDestroyItem(packet.Slot), packet.Slot);
            } else
            {
                player?.DestroyItem(packet.Slot, packet.Quantity);
            }
        }

        //UseItemPacket
        public void HandlePacket(Client client, UseItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            Entity target = null;
            if (packet.TargetId != Guid.Empty)
            {
                foreach (var mapInstance in MapController.GetSurroundingMapInstances(player.Map.Id, player.MapInstanceId, true))
                {
                    foreach (var en in mapInstance.GetEntities())
                    {
                        if (en.Id == packet.TargetId)
                        {
                            target = en;

                            break;
                        }
                    }
                }
            }

            player.UseItem(packet.Slot, target);
        }

        //SwapSpellsPacket
        public void HandlePacket(Client client, SwapSpellsPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.SwapSpells(packet.Slot1, packet.Slot2);
        }

        //ForgetSpellPacket
        public void HandlePacket(Client client, ForgetSpellPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.ForgetSpell(packet.Slot);
        }

        //UseSpellPacket
        public void HandlePacket(Client client, UseSpellPacket packet)
        {
            var player = client?.Entity;
            if (player == null || player.InVehicle)
            {
                return;
            }

            var casted = false;

            if (packet.TargetId != Guid.Empty)
            {
                foreach (var mapInstance in MapController.GetSurroundingMapInstances(player.Map.Id, player.MapInstanceId, true))
                {
                    foreach (var en in mapInstance.GetEntities())
                    {
                        if (en.Id == packet.TargetId)
                        {
                            player.UseSpellInHotbarSlot(packet.Slot, en);
                            casted = true;

                            break;
                        }
                    }
                }
            }

            if (!casted)
            {
                player.UseSpellInHotbarSlot(packet.Slot, null);
            }
            
            PacketSender.SendEntityPositionTo(client, client.Entity);
        }

        //UnequipItemPacket
        public void HandlePacket(Client client, UnequipItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.UnequipItem(packet.Slot);
        }

        //UpgradeStatPacket
        public void HandlePacket(Client client, UpgradeStatPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.UpgradeStat(packet.Stat);
        }

        //HotbarUpdatePacket
        public void HandlePacket(Client client, HotbarUpdatePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.HotbarChange(packet.HotbarSlot, packet.Type, packet.Index);
        }

        //HotbarSwapPacket
        public void HandlePacket(Client client, HotbarSwapPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.HotbarSwap(packet.Slot1, packet.Slot2);
        }

        //OpenAdminWindowPacket
        public void HandlePacket(Client client, OpenAdminWindowPacket packet)
        {
            if (client.Power.IsModerator)
            {
                PacketSender.SendMapList(client);
                PacketSender.SendOpenAdminWindow(client);
            }
        }

        //AdminActionPacket
        public void HandlePacket(Client client, AdminActionPacket packet)
        {
            var player = client?.Entity;
            if (!client.Power.Editor && !client.Power.IsModerator)
            {
                return;
            }

            ActionProcessing.ProcessAction(client, player, (dynamic) packet.Action);
        }

        //BuyItemPacket
        public void HandlePacket(Client client, BuyItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.BuyItem(packet.Slot, packet.Quantity);
        }

        //SellItemPacket
        public void HandlePacket(Client client, SellItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            if (IsMultislotTransfer(packet, player))
            {
                player?.MultislotTransfer(packet.Slots, packet.Quantity, Player.ItemMovementType.Sell);
                return;
            }
            else if (!player.TrySellItem(packet.Slot, packet.Quantity, out _, out _))
            {
                player?.SendAlert();
            }
        }

        //CloseShopPacket
        public void HandlePacket(Client client, CloseShopPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.CloseShop();
        }

        //CloseCraftingPacket
        public void HandlePacket(Client client, CloseCraftingPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.CloseCraftingTable();
        }

        //CraftItemPacket
        public void HandlePacket(Client client, CraftItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.CraftId = packet.CraftId;
            player.CraftAmount = packet.Amount;
            player.CraftTimer = Timing.Global.Milliseconds;
        }

        //CloseBankPacket
        public void HandlePacket(Client client, CloseBankPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.CloseBank();
        }

        //CloseQuestBoardPacket
        public void HandlePacket(Client client, CloseQuestBoardPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.CloseQuestBoard();
        }

        //DepositItemPacket
        public void HandlePacket(Client client, DepositItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            if (IsMultislotTransfer(packet, player))
            {
                player?.MultislotTransfer(packet.Slots, packet.Quantity, Player.ItemMovementType.Deposit);
                return;
            }
            else if (!player?.BankInterface?.TryDepositItem(packet.Slot, packet.Quantity) ?? true)
            {
                player?.SendAlert();
            }
        }

        //WithdrawItemPacket
        public void HandlePacket(Client client, WithdrawItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.BankInterface?.WithdrawItem(packet.Slot, packet.Quantity);
        }

        //MoveBankItemPacket
        public void HandlePacket(Client client, SwapBankItemsPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.BankInterface?.SwapBankItems(packet.Slot1, packet.Slot2);
        }

        //PartyInvitePacket
        public void HandlePacket(Client client, PartyInvitePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            var target = Player.FindOnline(packet.TargetId);

            if (target != null && target.Id != player.Id)
            {
                target.InviteToParty(player);

                return;
            }

            PacketSender.SendChatMsg(player, Strings.Parties.outofrange, ChatMessageType.Combat, CustomColors.Combat.NoTarget);
        }

        //PartyInviteResponsePacket
        public void HandlePacket(Client client, PartyInviteResponsePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            var leader = packet.PartyId;
            if (player.PartyRequester != null && player.PartyRequester.Id == leader)
            {
                if (packet.AcceptingInvite)
                {
                    if (player.PartyRequester.IsValidPlayer)
                    {
                        player.PartyRequester.AddParty(player);
                    }
                }
                else
                {
                    PacketSender.SendChatMsg(
                        player.PartyRequester, Strings.Parties.declined.ToString(client.Entity.Name),
                        ChatMessageType.Party,
                        CustomColors.Alerts.Declined
                    );

                    if (player.PartyRequests.ContainsKey(player.PartyRequester))
                    {
                        player.PartyRequests[player.PartyRequester] =
                            Timing.Global.Milliseconds + Options.RequestTimeout;
                    }
                    else
                    {
                        player.PartyRequests.Add(
                            player.PartyRequester, Timing.Global.Milliseconds + Options.RequestTimeout
                        );
                    }
                }

                player.PartyRequester = null;
            }
        }

        //PartyKickPacket
        public void HandlePacket(Client client, PartyKickPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.KickParty(packet.TargetId);
        }

        //PartyLeavePacket
        public void HandlePacket(Client client, PartyLeavePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.LeaveParty();
        }

        //QuestResponsePacket
        public void HandlePacket(Client client, QuestResponsePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            if (packet.AcceptingQuest)
            {
                player.AcceptQuest(packet.QuestId);
                if (packet.FromQuestBoard)
                {
                    player.CloseQuestBoard();
                }
            }
            else
            {
                player.DeclineQuest(packet.QuestId, packet.FromQuestBoard);
            }

            if (packet.FromQuestBoard) // Clear all quest offers, quietly
            {
                player.QuestOffers.Clear();
            }
        }

        //AbandonQuestPacket
        public void HandlePacket(Client client, AbandonQuestPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player.CancelQuest(packet.QuestId);
        }

        //TradeRequestPacket
        public void HandlePacket(Client client, TradeRequestPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            var target = Player.FindOnline(packet.TargetId);

            if (target != null && target.Id != player.Id && player.InRangeOf(target, Options.TradeRange))
            {
                if (player.InRangeOf(target, Options.TradeRange))
                {
                    target.InviteToTrade(player);

                    return;
                }
            }

            //Player Out of Range Or Offline
            PacketSender.SendChatMsg(player, Strings.Trading.outofrange.ToString(), ChatMessageType.Trading, CustomColors.Combat.NoTarget);
        }

        //TradeRequestResponsePacket
        public void HandlePacket(Client client, TradeRequestResponsePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            var target = packet.TradeId;
            if (player.Trading.Requester != null && player.Trading.Requester.Id == target)
            {
                if (player.Trading.Requester.IsValidPlayer)
                {
                    if (packet.AcceptingInvite)
                    {
                        if (player.Trading.Requester.Trading.Counterparty == null
                        ) //They could have accepted another trade since.
                        {
                            if (player.InRangeOf(player.Trading.Requester, Options.TradeRange))
                            {
                                //Check if still in range lolz
                                player.Trading.Requester.StartTrade(player);
                            }
                            else
                            {
                                PacketSender.SendChatMsg(
                                    player, Strings.Trading.outofrange.ToString(), ChatMessageType.Trading, CustomColors.Combat.NoTarget
                                );
                            }
                        }
                        else
                        {
                            PacketSender.SendChatMsg(
                                player, Strings.Trading.busy.ToString(player.Trading.Requester.Name), ChatMessageType.Trading, CustomColors.Alerts.Declined
                            );
                        }
                    }
                    else
                    {
                        PacketSender.SendChatMsg(
                            player.Trading.Requester, Strings.Trading.declined.ToString(player.Name),
                            ChatMessageType.Trading,
                            CustomColors.Alerts.Declined
                        );

                        if (player.Trading.Requests.ContainsKey(player.Trading.Requester))
                        {
                            player.Trading.Requests[player.Trading.Requester] =
                                Timing.Global.Milliseconds + Options.RequestTimeout;
                        }
                        else
                        {
                            player.Trading.Requests.Add(
                                player.Trading.Requester, Timing.Global.Milliseconds + Options.RequestTimeout
                            );
                        }
                    }
                }
            }

            player.Trading.Requester = null;
        }

        //OfferTradeItemPacket
        public void HandlePacket(Client client, OfferTradeItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null || player.Trading.Counterparty == null)
            {
                return;
            }

            if (IsMultislotTransfer(packet, player))
            {
                player.MultislotTransfer(packet.Slots, packet.Quantity, Player.ItemMovementType.Trade);
                return;
            }
            else if (!player.TryOfferItem(packet.Slot, packet.Quantity))
            {
                player?.SendAlert();
            }
        }

        //RevokeTradeItemPacket
        public void HandlePacket(Client client, RevokeTradeItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null || player.Trading.Counterparty == null)
            {
                return;
            }

            if (player.Trading.Counterparty.Trading.Accepted)
            {
                PacketSender.SendChatMsg(
                    player, Strings.Trading.RevokeNotAllowed.ToString(player.Trading.Counterparty.Name), ChatMessageType.Trading,
                    CustomColors.Alerts.Declined
                );
            }
            else
            {
                player?.RevokeItem(packet.Slot, packet.Quantity);
            }
        }

        //AcceptTradePacket
        public void HandlePacket(Client client, AcceptTradePacket packet)
        {
            var player = client?.Entity;
            if (player == null || player.Trading.Counterparty == null)
            {
                return;
            }

            player.Trading.Accepted = true;
            if (player.Trading.Counterparty.Trading.Accepted)
            {
                if (Options.Instance.Logging.Trade)
                {
                    //Duplicate the items we are trading because they are messed with in the ReturnTradeItems() function below
                    var tradeId = Guid.NewGuid();
                    var ourItems = player.Trading.Offer.Where(i => i != null && i.ItemId != Guid.Empty).Select(i => i.Clone()).ToArray();
                    var theirItems = player.Trading.Counterparty.Trading.Offer.Where(i => i != null && i.ItemId != Guid.Empty).Select(i => i.Clone()).ToArray();
                    TradeHistory.LogTrade(tradeId, player, player.Trading.Counterparty, ourItems, theirItems);
                    TradeHistory.LogTrade(tradeId, player.Trading.Counterparty, player, theirItems, ourItems);
                }
                //Swap the trade boxes over, then return the trade boxes to their new owners!
                var t = player.Trading.Offer;
                player.Trading.Offer = player.Trading.Counterparty.Trading.Offer;
                player.Trading.Counterparty.Trading.Offer = t;
                player.Trading.Counterparty.ReturnTradeItems();
                player.ReturnTradeItems();

                PacketSender.SendChatMsg(player, Strings.Trading.accepted, ChatMessageType.Trading, CustomColors.Alerts.Accepted);
                PacketSender.SendChatMsg(
                    player.Trading.Counterparty, Strings.Trading.accepted, ChatMessageType.Trading, CustomColors.Alerts.Accepted
                );

                PacketSender.SendTradeClose(player.Trading.Counterparty);
                PacketSender.SendTradeClose(player);
                player.Trading.Counterparty.Trading.Counterparty = null;
                player.Trading.Counterparty = null;
            }
            else
            {
                PacketSender.SendTradeAcceptPacketTo(player.Trading.Counterparty);
            }
        }

        //DeclineTradePacket
        public void HandlePacket(Client client, DeclineTradePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.CancelTrade();
        }

        //CloseBagPacket
        public void HandlePacket(Client client, CloseBagPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.CloseBag();
        }

        //StoreBagItemPacket
        public void HandlePacket(Client client, StoreBagItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.StoreBagItem(packet.Slot, packet.Quantity, packet.BagSlot);
        }

        //RetrieveBagItemPacket
        public void HandlePacket(Client client, RetrieveBagItemPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.RetrieveBagItem(packet.Slot, packet.Quantity, packet.InventorySlot);
        }

        //SwapBagItemPacket
        public void HandlePacket(Client client, SwapBagItemsPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            player?.SwapBagItems(packet.Slot1, packet.Slot2);
        }

        //RequestFriendsPacket
        public void HandlePacket(Client client, RequestFriendsPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            PacketSender.SendFriends(player);
        }

        //UpdateFriendsPacket
        public void HandlePacket(Client client, UpdateFriendsPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            if (packet.Adding)
            {
                //Don't add yourself!
                if (packet.Name.ToLower() == client.Entity.Name.ToLower())
                {
                    return;
                }

                if (client.Entity.GetFriendId(packet.Name) == Guid.Empty)
                {
                    var target = Player.FindOnline(packet.Name);
                    if (target != null)
                    {
                        if (target.CombatTimer < Timing.Global.Milliseconds)
                        {
                            target.FriendRequest(client.Entity);
                        }
                        else
                        {
                            PacketSender.SendChatMsg(player, Strings.Friends.busy.ToString(target.Name), ChatMessageType.Friend);
                        }
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Player.offline, ChatMessageType.Friend, CustomColors.Alerts.Error);
                    }
                }
                else
                {
                    PacketSender.SendChatMsg(
                        player, Strings.Friends.alreadyfriends.ToString(packet.Name), ChatMessageType.Friend, CustomColors.Alerts.Info
                    );
                }
            }
            else
            {
                //Check if we have this friend
                var friendId = player.GetFriendId(packet.Name);
                if (friendId != Guid.Empty)
                {
                    var otherPlayer = Player.FindOnline(friendId);
                    player.CachedFriends.Remove(friendId);
                    PacketSender.SendFriends(player);
                    PacketSender.SendChatMsg(player, Strings.Friends.remove, ChatMessageType.Friend, CustomColors.Alerts.Declined);

                    if (otherPlayer?.CachedFriends.ContainsKey(player.Id) ?? false)
                    {
                        otherPlayer.CachedFriends.Remove(player.Id);
                        PacketSender.SendFriends(otherPlayer);
                    }

                    Player.RemoveFriendship(player.Id, friendId);
                }
            }
        }

        //FriendRequestResponsePacket
        public void HandlePacket(Client client, FriendRequestResponsePacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }

            var target = Player.FindOnline(packet.FriendId);

            if (target == null || target.Id == player.Id)
            {
                return;
            }

            if (packet.AcceptingRequest)
            {
                if (!player.CachedFriends.ContainsKey(target.Id)) // Incase one user deleted friend then re-requested
                {
                    player.AddFriend(target);
                    PacketSender.SendChatMsg(
                        player, Strings.Friends.notification.ToString(target.Name), ChatMessageType.Friend, CustomColors.Alerts.Accepted
                    );

                    PacketSender.SendFriends(player);
                }

                if (!target.CachedFriends.ContainsKey(player.Id)) // Incase one user deleted friend then re-requested
                {
                    target.AddFriend(player);
                    PacketSender.SendChatMsg(
                        target, Strings.Friends.accept.ToString(player.Name), ChatMessageType.Friend, CustomColors.Alerts.Accepted
                    );

                    PacketSender.SendFriends(target);
                }
            }
            else
            {
                if (player.FriendRequester == target)
                {
                    if (player.FriendRequester.IsValidPlayer)
                    {
                        if (player.FriendRequests.ContainsKey(player.FriendRequester))
                        {
                            player.FriendRequests[player.FriendRequester] =
                                Timing.Global.Milliseconds + Options.RequestTimeout;
                        }
                        else
                        {
                            player.FriendRequests.Add(
                                client.Entity.FriendRequester, Timing.Global.Milliseconds + Options.RequestTimeout
                            );
                        }
                    }
                }
            }

            player.FriendRequester = null;
        }

        //SelectCharacterPacket
        public void HandlePacket(Client client, SelectCharacterPacket packet)
        {
            if (client.User == null)
                return;

            var character = DbInterface.GetUserCharacter(client.User, packet.CharacterId);
            if (character != null)
            {
                client.LoadCharacter(character);

                UserActivityHistory.LogActivity(client.User?.Id ?? Guid.Empty, client?.Entity?.Id ?? Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.SelectPlayer, $"{client?.Name},{client?.Entity?.Name}");

                try
                {
                    client.Entity?.SetOnline();
                    PacketSender.SendJoinGame(client);
                }
                catch (Exception exception)
                {
                    Log.Warn(exception);
                    PacketSender.SendError(client, Strings.Account.loadfail);
                    client.Logout();
                }
            }
        }

        //DeleteCharacterPacket
        public void HandlePacket(Client client, DeleteCharacterPacket packet)
        {
            if (client.User == null)
                return;

            if (Player.FindOnline(packet.CharacterId) != null)
            {
                PacketSender.SendError(client, Strings.Account.deletecharerror, Strings.Account.deletederror);
                PacketSender.SendPlayerCharacters(client);
                return;
            }

            var character = DbInterface.GetUserCharacter(client.User, packet.CharacterId);
            if (character != null)
            {
                character.LoadGuild();
                if (character.Guild != null && character.GuildRank == 0)
                {
                    PacketSender.SendError(client, Strings.Guilds.deleteguildleader, Strings.Account.deleted);
                    return;
                }

                foreach (var chr in client.Characters.ToArray())
                {
                    if (chr.Id == packet.CharacterId)
                    {
                        UserActivityHistory.LogActivity(client?.User?.Id ?? Guid.Empty, client?.Entity?.Id ?? Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Client, UserActivityHistory.UserAction.DeletePlayer, $"{client?.Name},{client?.Entity?.Name}");

                        client.User.DeleteCharacter(chr);
                    }
                }
            }

            PacketSender.SendError(client, Strings.Account.deletechar, Strings.Account.deleted);
            PacketSender.SendPlayerCharacters(client);
        }

        //NewCharacterPacket
        public void HandlePacket(Client client, NewCharacterPacket packet)
        {
            if (client?.Characters?.Count < Options.MaxCharacters)
            {
                PacketSender.SendGameObjects(client, GameObjectType.Class);
                PacketSender.SendCreateCharacter(client);
            }
            else
            {
                PacketSender.SendError(client, Strings.Account.maxchars, resetUi: false);
            }
        }

        //RequestPasswordResetPacket
        public void HandlePacket(Client client, RequestPasswordResetPacket packet)
        {
            if (client.TimeoutMs > Timing.Global.Milliseconds)
            {
                PacketSender.SendError(client, Strings.Errors.errortimeout);
                client.ResetTimeout();

                return;
            }

            if (Options.Instance.SmtpValid)
            {
                //Find account with that name or email
                var user = User.FindFromNameOrEmail(packet.NameOrEmail.Trim());
                if (user != null)
                {
                    var email = new PasswordResetEmail(user);
                    if (!email.Send())
                    {
                        PacketSender.SendError(client, Strings.Account.emailfail);
                    }
                }
                else
                {
                    client.FailedAttempt();
                }
            }
            else
            {
                client.FailedAttempt();
            }
        }

        //ResetPasswordPacket
        public void HandlePacket(Client client, ResetPasswordPacket packet)
        {
            //Find account with that name or email

            if (client.TimeoutMs > Timing.Global.Milliseconds)
            {
                PacketSender.SendError(client, Strings.Errors.errortimeout);
                client.ResetTimeout();

                return;
            }

            var success = false;
            var user = User.FindFromNameOrEmail(packet.NameOrEmail.Trim());
            if (user != null)
            {
                if (user.PasswordResetCode.ToLower().Trim() == packet.ResetCode.ToLower().Trim() &&
                    user.PasswordResetTime > DateTime.UtcNow)
                {
                    user.PasswordResetCode = "";
                    user.PasswordResetTime = DateTime.MinValue;
                    DbInterface.ResetPass(user, packet.NewPassword);
                    success = true;
                }
            }

            PacketSender.SendPasswordResetResult(client, success);
        }

        //RequestGuildPacket
        public void HandlePacket(Client client, RequestGuildPacket packet)
        {
            var player = client.Entity;
            if (player == null)
            {
                return;
            }
            PacketSender.SendGuild(player);
        }


        //UpdateGuildMemberPacket
        public void HandlePacket(Client client, UpdateGuildMemberPacket packet)
        {
            var player = client.Entity;
            if (player == null)
            {
                return;
            }

            var guild = player.Guild;

            // Are we in a guild?
            if (guild == null)
            {
                PacketSender.SendChatMsg(player, Strings.Guilds.NotInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                return;
            }

            var isOwner = player.GuildRank == 0;
            var rank = Options.Instance.Guild.Ranks[Math.Max(0, Math.Min(player.GuildRank, Options.Instance.Guild.Ranks.Length - 1))];
            Network.Packets.Server.GuildMember member = null;

            // Handle our desired action, assuming we're allowed to of course.
            switch (packet.Action)
            {
                case GuildMemberUpdateActions.Invite:
                    // Are we allowed to invite players?
                    var inviteRankIndex = Options.Instance.Guild.Ranks.Length - 1;
                    var inviteRank = Options.Instance.Guild.Ranks[inviteRankIndex];
                    if (!rank.Permissions.Invite)
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.NotAllowed, ChatMessageType.Guild, CustomColors.Alerts.Error);
                        return;
                    }

                    if (inviteRank.Limit > -1 && guild.Members.Where(m => m.Value.Rank == inviteRankIndex).Count() >= inviteRank.Limit)
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.RankLimitResponse.ToString(inviteRank.Title, player.Name), ChatMessageType.Guild, CustomColors.Alerts.Error);
                        return;
                    }

                    if (guild.GuildFull)
                    {
                        guild.SendGuildFullMessageTo(player);
                        return;
                    }

                    // Does our target player exist online?
                    var target = Player.FindOnline(packet.Name);
                    if (target != null)
                    {
                        if (!player.IsAllyOf(target))
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.PvpGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        if (target.InDuel)
                        {
                            PacketSender.SendChatMsg(player, Strings.Parties.GuildDuel, ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        // Are we already in a guild? or have a pending invite?
                        if (target.Guild == null && target.GuildInvite == null)
                        {
                            // Thank god, we can FINALLY get started!
                            // Set our invite and send our players the relevant messages.
                            target.GuildInvite = new Tuple<Player, Guild>(player, player.Guild);

                            PacketSender.SendChatMsg(player, Strings.Guilds.InviteSent.ToString(target.Name, player.Guild.Name), ChatMessageType.Guild, CustomColors.Alerts.Info);

                            PacketSender.SendGuildInvite(target, player);
                        }
                        else
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.InviteAlreadyInGuild, ChatMessageType.Guild, CustomColors.Alerts.Error);
                        }
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.InviteNotOnline, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                    break;
                case GuildMemberUpdateActions.Remove:
                    if (guild.Members.TryGetValue(packet.Id, out member))
                    {
                        if ((!rank.Permissions.Kick && !isOwner) || member.Rank <= player.GuildRank)
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.NotAllowed, ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        // Start common events for all online guild members that this one left
                        foreach (var mem in guild.FindOnlineMembers())
                        {
                            mem.StartCommonEventsWithTrigger(CommonEventTrigger.GuildMemberKicked, guild.Name, member.Name);
                        }

                        guild.RemoveMember(Player.Find(packet.Id), player, GuildHistory.GuildActivityType.Kicked);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.NoSuchPlayer, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                    break;
                case GuildMemberUpdateActions.Promote:
                    if (guild.Members.TryGetValue(packet.Id, out member))
                    {
                        var promotionRankIndex = Math.Max(0, Math.Min(packet.Rank, Options.Instance.Guild.Ranks.Length - 1));
                        var promotionRank = Options.Instance.Guild.Ranks[promotionRankIndex];
                        if ((!rank.Permissions.Promote && !isOwner) || member.Rank <= player.GuildRank || packet.Rank <= player.GuildRank || packet.Rank > member.Rank)
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.NotAllowed, ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        if (promotionRank.Limit > -1 && guild.Members.Where(m => m.Value.Rank == promotionRankIndex).Count() >= promotionRank.Limit)
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.RankLimitResponse.ToString(promotionRank.Title, player.Name), ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        guild.SetPlayerRank(packet.Id, packet.Rank, player);

                        PacketSender.SendGuildMsg(player, Strings.Guilds.Promoted.ToString(member.Name, promotionRank.Title), CustomColors.Alerts.Success);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.NoSuchPlayer, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                    break;
                case GuildMemberUpdateActions.Demote:
                    if (guild.Members.TryGetValue(packet.Id, out member))
                    {
                        var demotionRankIndex = Math.Max(0, Math.Min(packet.Rank, Options.Instance.Guild.Ranks.Length - 1));
                        var demotionRank = Options.Instance.Guild.Ranks[demotionRankIndex];
                        if ((!rank.Permissions.Demote && !isOwner) || member.Rank <= player.GuildRank || packet.Rank <= player.GuildRank || packet.Rank < member.Rank)
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.NotAllowed, ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        if (demotionRank.Limit > -1 && guild.Members.Where(m => m.Value.Rank == demotionRankIndex).Count() >= demotionRank.Limit)
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.RankLimitResponse.ToString(demotionRank.Title, player.Name), ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        guild.SetPlayerRank(packet.Id, packet.Rank, player);

                        PacketSender.SendGuildMsg(player, Strings.Guilds.Demoted.ToString(member.Name, demotionRank.Title), CustomColors.Alerts.Error);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.NoSuchPlayer, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                    break;
                case GuildMemberUpdateActions.Transfer:
                    if (guild.Members.TryGetValue(packet.Id, out member))
                    {
                        if (!isOwner)
                        {
                            PacketSender.SendChatMsg(player, Strings.Guilds.NotAllowed, ChatMessageType.Guild, CustomColors.Alerts.Error);
                            return;
                        }

                        guild.TransferOwnership(Player.Find(packet.Id));

                        PacketSender.SendGuildMsg(player, Strings.Guilds.Transferred.ToString(guild.Name, player.Name, member.Name), CustomColors.Alerts.Success);
                    }
                    else
                    {
                        PacketSender.SendChatMsg(player, Strings.Guilds.NoSuchPlayer, ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                    break;
                default:
                    /// ???
                    break;
            }
        }

        //GuildInviteAcceptPacket
        public void HandlePacket(Client client, GuildInviteAcceptPacket packet)
        {
            var player = client.Entity;
            if (player == null)
            {
                return;
            }

            var invitor = player?.GuildInvite?.Item1;
            var guild = player?.GuildInvite?.Item2;

            // Have we received an invite at all?
            if (guild == null || player.GuildInvite == null)
            {
                PacketSender.SendChatMsg(player, Strings.Guilds.NotReceivedInvite, ChatMessageType.Guild, CustomColors.Alerts.Error);
                return;
            }

            if (Options.Instance.Guild.Ranks[Options.Instance.Guild.Ranks.Length - 1].Limit > -1 && guild.Members.Where(m => m.Value.Rank == Options.Instance.Guild.Ranks.Length - 1).Count() >= Options.Instance.Guild.Ranks[Options.Instance.Guild.Ranks.Length - 1].Limit)
            {
                // Inform the inviter that the guild is full
                if (player.GuildInvite.Item1 != null)
                {
                    var onlinePlayer = Player.FindOnline(player.GuildInvite.Item1.Id);
                    if (onlinePlayer != null)
                    {
                        PacketSender.SendChatMsg(onlinePlayer, Strings.Guilds.RankLimitResponse.ToString(Options.Instance.Guild.Ranks[Options.Instance.Guild.Ranks.Length - 1].Title, player.Name), ChatMessageType.Guild, CustomColors.Alerts.Error);
                    }
                }

                //Inform the acceptor that they are actually not in the guild
                PacketSender.SendChatMsg(player, Strings.Guilds.RankLimit.ToString(player.GuildInvite.Item2.Name), ChatMessageType.Guild, CustomColors.Alerts.Error);

                player.GuildInvite = null;

                return;
            }

            // If guild is at max capacity
            if (guild.GuildFull && player != null)
            {
                var onlinePlayer = Player.FindOnline(player.GuildInvite.Item1.Id);
                guild.SendGuildFullMessageTo(onlinePlayer);
                player.GuildInvite = null;
                
                return;
            }

            // Accept our invite!
            guild.AddMember(player, Options.Instance.Guild.Ranks.Length - 1, invitor);
            player.GuildInvite = null;

            // Start common events for all online guild members that this one left
            foreach (var member in guild.FindOnlineMembers())
            {
                member.StartCommonEventsWithTrigger(CommonEventTrigger.GuildMemberJoined, guild.Name, player.Name);
            }

            // Send the updated data around.
            PacketSender.SendEntityDataToProximity(player);
        }

        //GuildInviteDeclinePacket
        public void HandlePacket(Client client, GuildInviteDeclinePacket packet)
        {
            var player = client.Entity;
            if (player == null)
            {
                return;
            }

            // Have we received an invite at all?
            if (player.GuildInvite == null)
            {
                PacketSender.SendChatMsg(player, Strings.Guilds.NotReceivedInvite, ChatMessageType.Guild, CustomColors.Alerts.Error);
                return;
            }

            // Politely decline our invite.
            if (player.GuildInvite.Item1 != null)
            {
                var onlinePlayer = Player.FindOnline(player.GuildInvite.Item1.Id);
                if (onlinePlayer != null)
                {
                    PacketSender.SendChatMsg(onlinePlayer, Strings.Guilds.InviteDeclinedResponse.ToString(player.Name, player.GuildInvite.Item2.Name), ChatMessageType.Guild, CustomColors.Alerts.Info);
                    PacketSender.SendChatMsg(player, Strings.Guilds.InviteDeclined.ToString(onlinePlayer.Name, player.GuildInvite.Item2.Name), ChatMessageType.Guild, CustomColors.Alerts.Info);
                }
                else
                {
                    PacketSender.SendChatMsg(player, Strings.Guilds.InviteDeclined.ToString(player.GuildInvite.Item2.Name), ChatMessageType.Guild, CustomColors.Alerts.Info);
                }

                player.GuildInvite = null;
            }
        }

        //GuildLeavePacket
        public void HandlePacket(Client client, GuildLeavePacket packet)
        {
            var player = client.Entity;
            if (player == null)
            {
                return;
            }

            var guild = player.Guild;

            // Are we in a guild at all?
            if (guild == null)
            {
                return;
            }

            // Are we the guild master? If so, they're not allowed to leave.
            if (player.GuildRank == 0)
            {
                PacketSender.SendChatMsg(player, Strings.Guilds.GuildLeaderLeave, ChatMessageType.Guild, CustomColors.Alerts.Error);
                return;
            }

            // Start common events for all online guild members that this one left
            foreach (var member in guild.FindOnlineMembers())
            {
                member.StartCommonEventsWithTrigger(CommonEventTrigger.GuildMemberLeft, guild.Name, player.Name);
            }

            guild.RemoveMember(player, null, GuildHistory.GuildActivityType.Left);

            // Send the newly updated player information to their surroundings.
            PacketSender.SendEntityDataToProximity(player);

        }


        //PictureClosedPacket
        public void HandlePacket(Client client, PictureClosedPacket packet)
        {
            var player = client?.Entity;
            if (player == null)
            {
                return;
            }
            player.PictureClosed(packet.EventId);
        }

        // The client has fully faded out while doing a map transition
        public void HandlePacket(Client client, MapTransitionReadyPacket packet)
        {
            var player = client?.Entity;

            if (player == null || client.IsEditor)
            {
                return;
            }

            player.FadeWarp = false;
            player.Warp(player.FadeMapId, player.FadeMapX, player.FadeMapY, player.FadeMapDir, false, 0, false, false, player.FadeMapInstanceType);
        }

        // BankSortPacket
        public void HandlePacket(Client client, BankSortPacket packet)
        {
            var player = client?.Entity;

            if (player == null || client.IsEditor)
            {
                return;
            }

            player.BankInterface.SortBank();
        }

        // the client is requesting 'crafting' information from the player's player variables collection
        public void HandlePacket(Client client, ClassInfoPacket packet)
        {
            var player = client?.Entity;

            if (player != null)
            {
                var craftingDict = new Dictionary<string, string>();
                craftingDict.Add("mining_tier", "0");
                craftingDict.Add("fishing_tier", "0");
                craftingDict.Add("woodcut_tier", "0");

                player.Variables.ForEach(variable =>
                {
                    if (variable.VariableName.Equals("Mining Tier"))
                    {
                        craftingDict["mining_tier"] = variable.Value;
                    } else if (variable.VariableName.Equals("Fishing Tier"))
                    {
                        craftingDict["fishing_tier"] = variable.Value;
                    } else if (variable.VariableName.Equals("Woodcut Tier"))
                    {
                        craftingDict["woodcut_tier"] = variable.Value;
                    }
                    else if (variable.VariableName.Equals("NPC_GUILD: current_npc_guild"))
                    {
                        craftingDict["npc_guild_name"] = variable.Value;
                    }
                });

                var classRanks = new Dictionary<string, int>();
                player.ClassInfo.Keys.ToList().ForEach(classId =>
                {
                    if (player.ClassInfo[classId].InGuild)
                    {
                        classRanks[ClassBase.GetName(classId)] = player.ClassInfo[classId].Rank;
                    }
                });

                PacketSender.SendCraftingInfoPacket(
                    client,
                    craftingDict["mining_tier"],
                    craftingDict["fishing_tier"],
                    craftingDict["woodcut_tier"],
                    classRanks
                );
            }
        }

        public void HandlePacket(Client client, SendQuestPointRequestPacket packet)
        {
            var player = client?.Entity;

            if (player != null)
            {
                var questPointDictionary = new Dictionary<string, string>();
                questPointDictionary.Add("lifetime_qp", "0");
                questPointDictionary.Add("quest_points", "0");

                player.Variables.ForEach(variable =>
                {
                    if (variable.VariableName.Equals("QP: lifetime_quest_points"))
                    {
                        questPointDictionary["lifetime_qp"] = variable.Value;
                    }
                    else if (variable.VariableName.Equals("Quest Points"))
                    {
                        questPointDictionary["quest_points"] = variable.Value;
                    }
                });
                
                PacketSender.SendQuestPointPacket(
                    client,
                    questPointDictionary["quest_points"],
                    questPointDictionary["lifetime_qp"]
                );
            }
        }

        // RequestQuestsFromListPacket - need to give the player their list of startable quests
        public void HandlePacket(Client client, RequestQuestsFromListPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.OfferQuestList(packet.QuestList);
        }

        public void HandlePacket(Client client, CancelPlayerCastPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.CancelCast();
            player.UpdateGlobalCooldown();
        }

        #endregion

        #region "Editor Packets"

        //PingPacket
        public void HandlePacket(Client client, Network.Packets.Editor.PingPacket packet)
        {
        }

        //LoginPacket
        public void HandlePacket(Client client, Network.Packets.Editor.LoginPacket packet)
        {
            if (client.AccountAttempts > 3 && client.TimeoutMs > Timing.Global.Milliseconds)
            {
                PacketSender.SendError(client, Strings.Errors.errortimeout, resetUi: false);
                client.ResetTimeout();

                return;
            }

            client.ResetTimeout();

            var user = User.TryLogin(packet.Username, packet.Password);
            if (user == null)
            {
                UserActivityHistory.LogActivity(Guid.Empty, Guid.Empty, client?.GetIp(), UserActivityHistory.PeerType.Editor, UserActivityHistory.UserAction.FailedLogin, packet.Username);

                client.FailedAttempt();
                PacketSender.SendError(client, Strings.Account.badlogin, resetUi: false);

                return;
            }

            if (!user.Power.Editor)
            {
                client.FailedAttempt();
                PacketSender.SendError(client, Strings.Account.badaccess);

                return;
            }

            client.IsEditor = true;
            if (client.IsEditor)
            {
                //Is Editor
                client.PacketFloodingThreshholds = Options.Instance.SecurityOpts.PacketOpts.EditorThreshholds;
            }


            client.SetUser(user);

            lock (Globals.ClientLock)
            {
                var clients = Globals.Clients.ToArray();
                foreach (var cli in clients)
                {
                    if (cli.Name != null &&
                        cli.Name.ToLower() == packet.Username.ToLower() &&
                        cli != client &&
                        cli.IsEditor)
                    {
                        cli.Disconnect();
                    }
                }
            }

            PacketSender.SendServerConfig(client);

            //Editor doesn't receive packet before login
            PacketSender.SendJoinGame(client);

            PacketSender.SendTimeBaseTo(client);
            PacketSender.SendMapList(client);
        }

        //MapPacket
        public void HandlePacket(Client client, Network.Packets.Editor.MapUpdatePacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var map = MapController.Get(packet.MapId);

            if (map == null)
            {
                return;
            }

            map.Load(packet.JsonData, MapController.Get(packet.MapId).Revision + 1);
            MapList.List.UpdateMap(packet.MapId);

            //Event Fixing
            var removedEvents = new List<Guid>();
            foreach (var id in map.EventIds)
            {
                if (!map.LocalEvents.ContainsKey(id))
                {
                    var evt = EventBase.Get(id);
                    if (evt != null)
                    {
                        DbInterface.DeleteGameObject(evt);
                    }

                    removedEvents.Add(id);
                }
            }

            foreach (var id in removedEvents)
            {
                map.EventIds.Remove(id);
            }

            foreach (var evt in map.LocalEvents)
            {
                var dbObj = EventBase.Get(evt.Key);
                if (dbObj == null)
                {
                    dbObj = (EventBase)DbInterface.AddGameObject(GameObjectType.Event, evt.Key);
                }

                dbObj.Load(evt.Value.JsonData);
                DbInterface.SaveGameObject(dbObj);
                if (!map.EventIds.Contains(evt.Key))
                {
                    map.EventIds.Add(evt.Key);
                }
            }

            map.LocalEvents.Clear();

            if (packet.TileData != null && map.TileData != null)
            {
                map.TileData = packet.TileData;
            }

            map.AttributeData = packet.AttributeData;

            DbInterface.SaveGameObject(map);

            map.Initialize();
            var players = new List<Player>();
            foreach (var surrMap in map.GetSurroundingMaps(true))
            {
                players.AddRange(surrMap.GetPlayersOnAllInstances().ToArray());
            }

            foreach (var plyr in players)
            {
                plyr.Warp(plyr.MapId, (byte) plyr.X, (byte) plyr.Y, (byte) plyr.Dir, false, (byte) plyr.Z, true);
                PacketSender.SendMap(plyr.Client, packet.MapId);
            }

            PacketSender.SendMap(client, packet.MapId, true); //Sends map to everyone/everything in proximity
            PacketSender.SendMapListToAll();
        }

        //CreateMapPacket
        public void HandlePacket(Client client, Network.Packets.Editor.CreateMapPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            lock (ServerContext.Instance.LogicService.LogicLock)
            {
                ServerContext.Instance.LogicService.LogicPool.WaitForIdle();
                var newMapId = Guid.Empty;
                MapController newMap = null;
                var tmpMap = new MapController(true);
                if (!packet.AttachedToMap)
                {
                    var destType = (int)packet.MapListParentType;
                    newMap = (MapController)DbInterface.AddGameObject(GameObjectType.Map);
                    newMapId = newMap.Id;
                    tmpMap = MapController.Get(newMapId);
                    DbInterface.GenerateMapGrids();
                    PacketSender.SendMap(client, newMapId, true);
                    PacketSender.SendMapGridToAll(tmpMap.MapGrid);

                    //FolderDirectory parent = null;
                    destType = -1;
                    if (destType == -1)
                    {
                        MapList.List.AddMap(newMapId, tmpMap.TimeCreated, MapBase.Lookup);
                    }

                    DbInterface.SaveMapList();

                    PacketSender.SendMapListToAll();
                    /*else if (destType == 0)
                    {
                        parent = Database.MapStructure.FindDir(bf.ReadInteger());
                        if (parent == null)
                        {
                            Database.MapStructure.AddMap(newMap);
                        }
                        else
                        {
                            parent.Children.AddMap(newMap);
                        }
                    }
                    else if (destType == 1)
                    {
                        var mapNum = bf.ReadInteger();
                        parent = Database.MapStructure.FindMapParent(mapNum, null);
                        if (parent == null)
                        {
                            Database.MapStructure.AddMap(newMap);
                        }
                        else
                        {
                            parent.Children.AddMap(newMap);
                        }
                    }*/
                }
                else
                {
                    var relativeMap = packet.MapId;
                    switch (packet.AttachDir)
                    {
                        case 0:
                            if (MapController.Get(MapController.Get(relativeMap).Up) == null)
                            {
                                newMap = (MapController)DbInterface.AddGameObject(GameObjectType.Map);
                                newMapId = newMap.Id;
                                tmpMap = MapController.Get(newMapId);
                                tmpMap.MapGrid = MapController.Get(relativeMap).MapGrid;
                                tmpMap.MapGridX = MapController.Get(relativeMap).MapGridX;
                                tmpMap.MapGridY = MapController.Get(relativeMap).MapGridY - 1;
                                MapController.Get(relativeMap).Up = newMapId;
                                DbInterface.SaveGameObject(MapController.Get(relativeMap));
                            }

                            break;

                        case 1:
                            if (MapController.Get(MapController.Get(relativeMap).Down) == null)
                            {
                                newMap = (MapController)DbInterface.AddGameObject(GameObjectType.Map);
                                newMapId = newMap.Id;
                                tmpMap = MapController.Get(newMapId);
                                tmpMap.MapGrid = MapController.Get(relativeMap).MapGrid;
                                tmpMap.MapGridX = MapController.Get(relativeMap).MapGridX;
                                tmpMap.MapGridY = MapController.Get(relativeMap).MapGridY + 1;
                                MapController.Get(relativeMap).Down = newMapId;
                                DbInterface.SaveGameObject(MapController.Get(relativeMap));
                            }

                            break;

                        case 2:
                            if (MapController.Get(MapController.Get(relativeMap).Left) == null)
                            {
                                newMap = (MapController)DbInterface.AddGameObject(GameObjectType.Map);
                                newMapId = newMap.Id;
                                tmpMap = MapController.Get(newMapId);
                                tmpMap.MapGrid = MapController.Get(relativeMap).MapGrid;
                                tmpMap.MapGridX = MapController.Get(relativeMap).MapGridX - 1;
                                tmpMap.MapGridY = MapController.Get(relativeMap).MapGridY;
                                MapController.Get(relativeMap).Left = newMapId;
                                DbInterface.SaveGameObject(MapController.Get(relativeMap));
                            }

                            break;

                        case 3:
                            if (MapController.Get(MapController.Get(relativeMap).Right) == null)
                            {
                                newMap = (MapController)DbInterface.AddGameObject(GameObjectType.Map);
                                newMapId = newMap.Id;
                                tmpMap = MapController.Get(newMapId);
                                tmpMap.MapGrid = MapController.Get(relativeMap).MapGrid;
                                tmpMap.MapGridX = MapController.Get(relativeMap).MapGridX + 1;
                                tmpMap.MapGridY = MapController.Get(relativeMap).MapGridY;
                                MapController.Get(relativeMap).Right = newMapId;
                                DbInterface.SaveGameObject(MapController.Get(relativeMap));
                            }

                            break;
                    }

                    if (newMapId != Guid.Empty)
                    {
                        var grid = DbInterface.GetGrid(tmpMap.MapGrid);
                        if (tmpMap.MapGridX >= 0 && tmpMap.MapGridX < grid.Width)
                        {
                            if (tmpMap.MapGridY + 1 < grid.Height)
                            {
                                tmpMap.Down = grid.MyGrid[tmpMap.MapGridX, tmpMap.MapGridY + 1];

                                if (tmpMap.Down != Guid.Empty)
                                {
                                    MapController.Get(tmpMap.Down).Up = newMapId;
                                    DbInterface.SaveGameObject(MapController.Get(tmpMap.Down));
                                }
                            }

                            if (tmpMap.MapGridY - 1 >= 0)
                            {
                                tmpMap.Up = grid.MyGrid[tmpMap.MapGridX, tmpMap.MapGridY - 1];

                                if (tmpMap.Up != Guid.Empty)
                                {
                                    MapController.Get(tmpMap.Up).Down = newMapId;
                                    DbInterface.SaveGameObject(MapController.Get(tmpMap.Up));
                                }
                            }
                        }

                        if (tmpMap.MapGridY >= 0 && tmpMap.MapGridY < grid.Height)
                        {
                            if (tmpMap.MapGridX - 1 >= 0)
                            {
                                tmpMap.Left = grid.MyGrid[tmpMap.MapGridX - 1, tmpMap.MapGridY];

                                if (tmpMap.Left != Guid.Empty)
                                {
                                    MapController.Get(tmpMap.Left).Right = newMapId;
                                    DbInterface.SaveGameObject(MapController.Get(tmpMap.Left));
                                }
                            }

                            if (tmpMap.MapGridX + 1 < grid.Width)
                            {
                                tmpMap.Right = grid.MyGrid[tmpMap.MapGridX + 1, tmpMap.MapGridY];

                                if (tmpMap.Right != Guid.Empty)
                                {
                                    MapController.Get(tmpMap.Right).Left = newMapId;
                                    DbInterface.SaveGameObject(MapController.Get(tmpMap.Right));
                                }
                            }
                        }

                        DbInterface.SaveGameObject(newMap);

                        DbInterface.GenerateMapGrids();
                        PacketSender.SendMap(client, newMapId, true);
                        PacketSender.SendMapGridToAll(MapController.Get(newMapId).MapGrid);
                        PacketSender.SendEnterMap(client, newMapId);
                        var folderDir = MapList.List.FindMapParent(relativeMap, null);
                        if (folderDir != null)
                        {
                            folderDir.Children.AddMap(newMapId, MapController.Get(newMapId).TimeCreated, MapBase.Lookup);
                        }
                        else
                        {
                            MapList.List.AddMap(newMapId, MapController.Get(newMapId).TimeCreated, MapBase.Lookup);
                        }

                        DbInterface.SaveMapList();
                        PacketSender.SendMapListToAll();
                    }
                }
            }
        }

        //MapListUpdatePacket
        public void HandlePacket(Client client, Network.Packets.Editor.MapListUpdatePacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            MapListFolder parent = null;
            var mapId = Guid.Empty;
            switch (packet.UpdateType)
            {
                case MapListUpdates.MoveItem:
                    MapList.List.HandleMove(packet.TargetType, packet.TargetId, packet.ParentType, packet.ParentId);
                    break;

                case MapListUpdates.AddFolder:
                    if (packet.ParentId == Guid.Empty)
                    {
                        MapList.List.AddFolder(Strings.Mapping.newfolder);
                    }
                    else if (packet.ParentType == 0)
                    {
                        parent = MapList.List.FindDir(packet.ParentId);
                        if (parent == null)
                        {
                            MapList.List.AddFolder(Strings.Mapping.newfolder);
                        }
                        else
                        {
                            parent.Children.AddFolder(Strings.Mapping.newfolder);
                        }
                    }
                    else if (packet.ParentType == 1)
                    {
                        mapId = packet.ParentId;
                        parent = MapList.List.FindMapParent(mapId, null);
                        if (parent == null)
                        {
                            MapList.List.AddFolder(Strings.Mapping.newfolder);
                        }
                        else
                        {
                            parent.Children.AddFolder(Strings.Mapping.newfolder);
                        }
                    }

                    break;

                case MapListUpdates.Rename:
                    if (packet.TargetType == 0)
                    {
                        parent = MapList.List.FindDir(packet.TargetId);
                        parent.Name = packet.Name;
                        PacketSender.SendMapListToAll();
                    }
                    else if (packet.TargetType == 1)
                    {
                        var mapListMap = MapList.List.FindMap(packet.TargetId);
                        mapListMap.Name = packet.Name;
                        MapController.Get(packet.TargetId).Name = packet.Name;
                        DbInterface.SaveGameObject(MapController.Get(packet.TargetId));
                        PacketSender.SendMapListToAll();
                    }

                    break;

                case MapListUpdates.Delete:
                    if (packet.TargetType == 0)
                    {
                        MapList.List.DeleteFolder(packet.TargetId);
                        PacketSender.SendMapListToAll();
                    }
                    else if (packet.TargetType == 1)
                    {
                        if (MapController.Lookup.Count == 1)
                        {
                            PacketSender.SendError(client, Strings.Mapping.lastmaperror, Strings.Mapping.lastmap);

                            return;
                        }

                        lock (ServerContext.Instance.LogicService.LogicLock)
                        {
                            ServerContext.Instance.LogicService.LogicPool.WaitForIdle();
                            mapId = packet.TargetId;
                            var players = MapController.Get(mapId).GetPlayersOnAllInstances();
                            MapList.List.DeleteMap(mapId);
                            DbInterface.DeleteGameObject(MapController.Get(mapId));
                            DbInterface.GenerateMapGrids();
                            PacketSender.SendMapListToAll();
                            foreach (var plyr in players)
                            {
                                plyr.WarpToSpawn();
                            }
                        }

                        PacketSender.SendMapToEditors(mapId);
                    }

                    break;
            }

            DbInterface.SaveMapList();
            PacketSender.SendMapListToAll();
        }

        //UnlinkMapPacket
        public void HandlePacket(Client client, Network.Packets.Editor.UnlinkMapPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var mapId = packet.MapId;
            var curMapId = packet.CurrentMapId;
            var mapGrid = 0;
            if (MapController.Lookup.Keys.Contains(mapId))
            {
                if (client.IsEditor)
                {
                    lock (ServerContext.Instance.LogicService.LogicLock)
                    {
                        ServerContext.Instance.LogicService.LogicPool.WaitForIdle();
                        var map = MapController.Get(mapId);
                        if (map != null)
                        {
                            map.ClearConnections();

                            var grid = DbInterface.GetGrid(map.MapGrid);
                            var gridX = map.MapGridX;
                            var gridY = map.MapGridY;

                            //Up
                            if (gridY - 1 >= 0 && grid.MyGrid[gridX, gridY - 1] != Guid.Empty)
                            {
                                MapController.Get(grid.MyGrid[gridX, gridY - 1])?.ClearConnections((int) Directions.Down);
                            }

                            //Down
                            if (gridY + 1 < grid.Height && grid.MyGrid[gridX, gridY + 1] != Guid.Empty)
                            {
                                MapController.Get(grid.MyGrid[gridX, gridY + 1])?.ClearConnections((int) Directions.Up);
                            }

                            //Left
                            if (gridX - 1 >= 0 && grid.MyGrid[gridX - 1, gridY] != Guid.Empty)
                            {
                                MapController.Get(grid.MyGrid[gridX - 1, gridY])
                                    ?.ClearConnections((int) Directions.Right);
                            }

                            //Right
                            if (gridX + 1 < grid.Width && grid.MyGrid[gridX + 1, gridY] != Guid.Empty)
                            {
                                MapController.Get(grid.MyGrid[gridX + 1, gridY]).ClearConnections((int) Directions.Left);
                            }

                            DbInterface.GenerateMapGrids();
                            if (MapController.Lookup.Keys.Contains(curMapId))
                            {
                                mapGrid = MapController.Get(curMapId).MapGrid;
                            }
                        }

                        PacketSender.SendMapGridToAll(mapGrid);
                    }
                }
            }
        }

        //LinkMapPacket
        public void HandlePacket(Client client, Network.Packets.Editor.LinkMapPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var adjacentMapId = packet.AdjacentMapId;
            var linkMapId = packet.LinkMapId;
            var adjacentMap = MapController.Get(packet.AdjacentMapId);
            var linkMap = MapController.Get(packet.LinkMapId);
            long gridX = packet.GridX;
            long gridY = packet.GridY;
            var canLink = true;

            lock (ServerContext.Instance.LogicService.LogicLock)
            {
                ServerContext.Instance.LogicService.LogicPool.WaitForIdle();
                if (adjacentMap != null && linkMap != null)
                {
                    //Clear to test if we can link.
                    var linkGrid = DbInterface.GetGrid(linkMap.MapGrid);
                    var adjacentGrid = DbInterface.GetGrid(adjacentMap.MapGrid);
                    if (linkGrid != adjacentGrid && linkGrid != null && adjacentGrid != null)
                    {
                        var xOffset = linkMap.MapGridX - gridX;
                        var yOffset = linkMap.MapGridY - gridY;
                        for (var x = 0; x < adjacentGrid.Width; x++)
                        {
                            for (var y = 0; y < adjacentGrid.Height; y++)
                            {
                                if (x + xOffset >= 0 &&
                                    x + xOffset < linkGrid.Width &&
                                    y + yOffset >= 0 &&
                                    y + yOffset < linkGrid.Height)
                                {
                                    if (adjacentGrid.MyGrid[x, y] != Guid.Empty &&
                                        linkGrid.MyGrid[x + xOffset, y + yOffset] != Guid.Empty)
                                    {
                                        //Incompatible Link!
                                        PacketSender.SendError(
                                            client,
                                            Strings.Mapping.linkfailerror.ToString(
                                                MapBase.GetName(linkMapId), MapBase.GetName(adjacentMapId),
                                                MapBase.GetName(adjacentGrid.MyGrid[x, y]),
                                                MapBase.GetName(linkGrid.MyGrid[x + xOffset, y + yOffset])
                                            ), Strings.Mapping.linkfail
                                        );

                                        return;
                                    }
                                }
                            }
                        }

                        if (canLink)
                        {
                            var updatedMaps = new HashSet<MapController>();
                            for (var x = -1; x < adjacentGrid.Width + 1; x++)
                            {
                                for (var y = -1; y < adjacentGrid.Height + 1; y++)
                                {
                                    if (x + xOffset >= 0 &&
                                        x + xOffset < linkGrid.Width &&
                                        y + yOffset >= 0 &&
                                        y + yOffset < linkGrid.Height)
                                    {
                                        if (linkGrid.MyGrid[x + xOffset, y + yOffset] != Guid.Empty)
                                        {
                                            var inXBounds = x > -1 && x < adjacentGrid.Width;
                                            var inYBounds = y > -1 && y < adjacentGrid.Height;
                                            if (inXBounds && inYBounds)
                                            {
                                                adjacentGrid.MyGrid[x, y] = linkGrid.MyGrid[x + xOffset, y + yOffset];
                                            }

                                            if (inXBounds && y - 1 >= 0 && adjacentGrid.MyGrid[x, y - 1] != Guid.Empty)
                                            {
                                                MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]).Up = adjacentGrid.MyGrid[x, y - 1];
                                                updatedMaps.Add(MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]));

                                                MapController.Get(adjacentGrid.MyGrid[x, y - 1]).Down = linkGrid.MyGrid[x + xOffset, y + yOffset];
                                                updatedMaps.Add(MapController.Get(adjacentGrid.MyGrid[x, y - 1]));
                                            }

                                            if (inXBounds &&
                                                y + 1 < adjacentGrid.Height &&
                                                adjacentGrid.MyGrid[x, y + 1] != Guid.Empty)
                                            {
                                                MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]).Down = adjacentGrid.MyGrid[x, y + 1];
                                                updatedMaps.Add(MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]));

                                                MapController.Get(adjacentGrid.MyGrid[x, y + 1]).Up = linkGrid.MyGrid[x + xOffset, y + yOffset];
                                                updatedMaps.Add(MapController.Get(adjacentGrid.MyGrid[x, y + 1]));
                                            }

                                            if (inYBounds && x - 1 >= 0 && adjacentGrid.MyGrid[x - 1, y] != Guid.Empty)
                                            {
                                                MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]).Left = adjacentGrid.MyGrid[x - 1, y];
                                                updatedMaps.Add(MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]));

                                                MapController.Get(adjacentGrid.MyGrid[x - 1, y]).Right = linkGrid.MyGrid[x + xOffset, y + yOffset];
                                                updatedMaps.Add(MapController.Get(adjacentGrid.MyGrid[x - 1, y]));
                                            }

                                            if (inYBounds &&
                                                x + 1 < adjacentGrid.Width &&
                                                adjacentGrid.MyGrid[x + 1, y] != Guid.Empty)
                                            {
                                                MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]).Right = adjacentGrid.MyGrid[x + 1, y];
                                                updatedMaps.Add(MapController.Get(linkGrid.MyGrid[x + xOffset, y + yOffset]));

                                                MapController.Get(adjacentGrid.MyGrid[x + 1, y]).Left = linkGrid.MyGrid[x + xOffset, y + yOffset];
                                                updatedMaps.Add(MapController.Get(adjacentGrid.MyGrid[x + 1, y]));
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (var map in updatedMaps)
                            {
                                DbInterface.SaveGameObject(map);
                            }

                            DbInterface.GenerateMapGrids();
                            PacketSender.SendMapGridToAll(adjacentMap.MapGrid);
                        }
                    }
                }
            }
        }

        //CreateGameObjectPacket
        public void HandlePacket(Client client, Network.Packets.Editor.CreateGameObjectPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var type = packet.Type;
            var obj = DbInterface.AddGameObject(type);

            var changed = false;
            switch(type)
            {
                case GameObjectType.Event:
                    ((EventBase)obj).CommonEvent = true;
                    changed = true;

                    break;
                case GameObjectType.Item:
                    ((ItemBase)obj).DropChanceOnDeath = Options.ItemDropChance;
                    changed = true;

                    break;
            }

            if (changed)
            {
                DbInterface.SaveGameObject(obj);

                // Update a player's Class Rank info to include this new class
                if (type == GameObjectType.Class)
                {
                    Globals.OnlineList.ForEach(player => player.InitClassRanks());
                }
            }

            PacketSender.CacheGameDataPacket();

            PacketSender.SendGameObjectToAll(obj);
        }

        //CreateTimerPacket
        public void HandlePacket(Client client, Network.Packets.Editor.CreateTimerPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var obj = DbInterface.AddNewTimerGameObject(packet.OwnerType);
            PacketSender.SendGameObjectToAll(obj);
        }

        //RequestOpenEditorPacket
        public void HandlePacket(Client client, Network.Packets.Editor.RequestOpenEditorPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var type = packet.Type;
            PacketSender.SendGameObjects(client, type);
            PacketSender.SendOpenEditor(client, type);
        }

        //DeleteGameObjectPacket
        public void HandlePacket(Client client, Network.Packets.Editor.DeleteGameObjectPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var type = packet.Type;
            var id = packet.Id;

            // TODO: YO COME DO THIS
            IDatabaseObject obj = null;
            switch (type)
            {
                case GameObjectType.Animation:
                    obj = AnimationBase.Get(id);

                    break;

                case GameObjectType.Class:
                    if (ClassBase.Lookup.Count == 1)
                    {
                        PacketSender.SendError(client, Strings.Classes.lastclasserror, Strings.Classes.lastclass);

                        return;
                    }

                    obj = DatabaseObject<ClassBase>.Lookup.Get(id);

                    break;

                case GameObjectType.Item:
                    obj = ItemBase.Get(id);

                    break;
                case GameObjectType.Npc:
                    obj = NpcBase.Get(id);

                    break;

                case GameObjectType.Projectile:
                    obj = ProjectileBase.Get(id);

                    break;

                case GameObjectType.Quest:
                    obj = QuestBase.Get(id);

                    break;

                case GameObjectType.Resource:
                    obj = ResourceBase.Get(id);

                    break;

                case GameObjectType.Shop:
                    obj = ShopBase.Get(id);

                    break;

                case GameObjectType.Spell:
                    obj = SpellBase.Get(id);

                    break;

                case GameObjectType.CraftTables:
                    obj = DatabaseObject<CraftingTableBase>.Lookup.Get(id);

                    break;

                case GameObjectType.Crafts:
                    obj = DatabaseObject<CraftBase>.Lookup.Get(id);

                    break;

                case GameObjectType.Map:
                    break;

                case GameObjectType.Event:
                    obj = EventBase.Get(id);

                    break;

                case GameObjectType.PlayerVariable:
                    obj = PlayerVariableBase.Get(id);

                    break;

                case GameObjectType.ServerVariable:
                    obj = ServerVariableBase.Get(id);

                    break;

                case GameObjectType.Tileset:
                    break;

                case GameObjectType.Time:
                    break;

                case GameObjectType.QuestList:
                    obj = DatabaseObject<QuestListBase>.Lookup.Get(id);

                    break;

                case GameObjectType.QuestBoard:
                    obj = DatabaseObject<QuestBoardBase>.Lookup.Get(id);

                    break;
                case GameObjectType.InstanceVariable:
                    obj = InstanceVariableBase.Get(id);

                    break;
                case GameObjectType.Timer:
                    obj = TimerDescriptor.Get(id);

                    // Remove the timer from processing
                    foreach (var timer in TimerProcessor.ActiveTimers.Where(t => t.DescriptorId == id).ToArray())
                    {
                        TimerProcessor.RemoveTimer(timer);
                    }

                    break;
                case GameObjectType.LootTable:
                    obj = LootTableDescriptor.Get(id);

                    break;
                case GameObjectType.Label:
                    obj = LabelDescriptor.Get(id);

                    break;
                case GameObjectType.Recipe:
                    obj = RecipeDescriptor.Get(id);

                    break;
                case GameObjectType.WeaponType:
                    obj = WeaponTypeDescriptor.Get(id);

                    break;
                case GameObjectType.Challenge:
                    obj = ChallengeDescriptor.Get(id);

                    break;
                case GameObjectType.Dungeon:
                    obj = DungeonDescriptor.Get(id);

                    break;
                
                case GameObjectType.Enhancement:
                    obj = EnhancementDescriptor.Get(id);

                    break;
                
                case GameObjectType.Territory:
                    obj = TerritoryDescriptor.Get(id);

                    break;

                case GameObjectType.GuildVariable:
                    obj = GuildVariableBase.Get(id);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (obj != null)
            {
                //if Item or Resource, kill all global entities of that kind
                if (type == GameObjectType.Item)
                {
                    Globals.KillItemsOf((ItemBase) obj);
                }
                else if (type == GameObjectType.Resource)
                {
                    Globals.KillResourcesOf((ResourceBase) obj);
                }
                else if (type == GameObjectType.Npc)
                {
                    Globals.KillNpcsOf((NpcBase) obj);
                    Globals.RefreshNpcSpellScalars();
                    if (Options.Instance.CombatOpts.UseGeneratedMobExp)
                    {
                        NpcExperienceService.Initialize();
#if DEBUG
                        NpcExperienceService.PrettyPrint();
#endif
                    }
                }
                else if (type == GameObjectType.Class)
                {
                    // Update a player's Class Rank info to remove this class
                    Globals.OnlineList.ForEach(player => player.ClassInfo.Remove(obj.Id));
                }

                DbInterface.DeleteGameObject(obj);

                PacketSender.CacheGameDataPacket();

                PacketSender.SendGameObjectToAll(obj, true);

                // Refresh challenges/weapon mastery progression if an update has been made
                if (type == GameObjectType.Challenge || type == GameObjectType.WeaponType)
                {
                    Globals.OnlineList.ForEach(player =>
                    {
                        player.ResetChallengeTracking();
                        player.SetMasteryProgress();
                    });
                }
                else if (type == GameObjectType.Recipe)
                {
                    Globals.RefreshGameObjectCache(GameObjectType.Recipe, Globals.CachedRecipes);
                    RecipeUnlockWatcher.QueueRefresh();
                }
                if (type == GameObjectType.Resource)
                {
                    Globals.RefreshGameObjectCache(GameObjectType.Resource, Globals.CachedResources);
                }
            }
        }

        //SaveGameObjectPacket
        public void HandlePacket(Client client, Network.Packets.Editor.SaveGameObjectPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var type = packet.Type;
            var id = packet.Id;
            IDatabaseObject obj = null;
            switch (type)
            {
                case GameObjectType.Animation:
                    obj = AnimationBase.Get(id);

                    break;

                case GameObjectType.Class:
                    obj = DatabaseObject<ClassBase>.Lookup.Get(id);

                    break;

                case GameObjectType.Item:
                    obj = ItemBase.Get(id);

                    break;
                case GameObjectType.Npc:
                    obj = NpcBase.Get(id);

                    break;

                case GameObjectType.Projectile:
                    obj = ProjectileBase.Get(id);

                    break;

                case GameObjectType.Quest:
                    obj = QuestBase.Get(id);

                    break;

                case GameObjectType.Resource:
                    obj = ResourceBase.Get(id);

                    break;

                case GameObjectType.Shop:
                    obj = ShopBase.Get(id);

                    break;

                case GameObjectType.Spell:
                    obj = SpellBase.Get(id);

                    break;

                case GameObjectType.CraftTables:
                    obj = DatabaseObject<CraftingTableBase>.Lookup.Get(id);

                    break;

                case GameObjectType.Crafts:
                    obj = DatabaseObject<CraftBase>.Lookup.Get(id);

                    break;

                case GameObjectType.Map:
                    break;

                case GameObjectType.Event:
                    obj = EventBase.Get(id);

                    break;

                case GameObjectType.PlayerVariable:
                    obj = PlayerVariableBase.Get(id);

                    break;

                case GameObjectType.ServerVariable:
                    obj = ServerVariableBase.Get(id);

                    break;

                case GameObjectType.InstanceVariable:
                    obj = InstanceVariableBase.Get(id);

                    break;

                case GameObjectType.Tileset:
                    break;

                case GameObjectType.Time:
                    break;
                
                case GameObjectType.QuestList:
                    obj = DatabaseObject<QuestListBase>.Lookup.Get(id);

                    break;
                
                case GameObjectType.QuestBoard:
                    obj = DatabaseObject<QuestBoardBase>.Lookup.Get(id);

                    break;
                case GameObjectType.Timer:
                    obj = DatabaseObject<TimerDescriptor>.Lookup.Get(id);

                    break;
                case GameObjectType.LootTable:
                    obj = DatabaseObject<LootTableDescriptor>.Lookup.Get(id);

                    break;
                case GameObjectType.Label:
                    obj = DatabaseObject<LabelDescriptor>.Lookup.Get(id);

                    break;
                case GameObjectType.Recipe:
                    obj = DatabaseObject<RecipeDescriptor>.Lookup.Get(id);

                    break;
                case GameObjectType.WeaponType:
                    obj = DatabaseObject<WeaponTypeDescriptor>.Lookup.Get(id);

                    break;
                case GameObjectType.Challenge:
                    obj = DatabaseObject<ChallengeDescriptor>.Lookup.Get(id);

                    break;
                case GameObjectType.Dungeon:
                    obj = DatabaseObject<DungeonDescriptor>.Lookup.Get(id);

                    break;

                case GameObjectType.Enhancement:
                    obj = DatabaseObject<EnhancementDescriptor>.Lookup.Get(id);

                    break;

                case GameObjectType.Territory:
                    obj = DatabaseObject<TerritoryDescriptor>.Lookup.Get(id);

                    break;

                case GameObjectType.GuildVariable:
                    obj = DatabaseObject<GuildVariableBase>.Lookup.Get(id);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (obj != null)
            {
                lock (ServerContext.Instance.LogicService.LogicLock)
                {
                    ServerContext.Instance.LogicService.LogicPool.WaitForIdle();
                    //if Item or Resource, kill all global entities of that kind
                    if (type == GameObjectType.Item)
                    {
                        Globals.KillItemsOf((ItemBase) obj);
                    }
                    else if (type == GameObjectType.Npc)
                    {
                        Globals.KillNpcsOf((NpcBase) obj);
                        Globals.RefreshNpcSpellScalars();
                        if (Options.Instance.CombatOpts.UseGeneratedMobExp)
                        {
                            NpcExperienceService.Initialize();
#if DEBUG
                            NpcExperienceService.PrettyPrint();
#endif
                        }
                    }
                    else if (type == GameObjectType.Projectile)
                    {
                        Globals.KillProjectilesOf((ProjectileBase) obj);
                    }
                    else if (type == GameObjectType.Resource)
                    {
                        Globals.KillResourcesOf((ResourceBase)obj);
                    }

                    obj.Load(packet.Data);

                    if (type == GameObjectType.Quest)
                    {
                        var qst = (QuestBase)obj;
                        foreach (var evt in qst.RemoveEvents)
                        {
                            var evtb = EventBase.Get(evt);
                            if (evtb != null)
                            {
                                DbInterface.DeleteGameObject(evtb);
                            }
                        }

                        foreach (var evt in qst.AddEvents)
                        {
                            var evtb = (EventBase)DbInterface.AddGameObject(GameObjectType.Event, evt.Key);
                            evtb.Load(evt.Value.JsonData);

                            foreach (var tsk in qst.Tasks)
                            {
                                if (tsk.Id == evt.Key)
                                {
                                    tsk.CompletionEvent = evtb;
                                }
                            }

                            DbInterface.SaveGameObject(evtb);
                        }

                        qst.AddEvents.Clear();
                        qst.RemoveEvents.Clear();
                    }
                    else if (type == GameObjectType.PlayerVariable)
                    {
                        DbInterface.CachePlayerVariableEventTextLookups();
                    }
                    else if (type == GameObjectType.ServerVariable)
                    {
                        Player.StartCommonEventsWithTriggerForAll(CommonEventTrigger.ServerVariableChange, "", obj.Id.ToString());
                        DbInterface.CacheServerVariableEventTextLookups();
                    }
                    else if (type == GameObjectType.InstanceVariable)
                    {

                        InstanceProcessor.AddNewInstanceVariable(obj.Id);
                        // Don't trigger a instance change common event, because the editor can not change instance variable values - only their defaults
                        DbInterface.CacheInstanceVariableEventTextLookups();
                    }
                    else if (type == GameObjectType.GuildVariable)
                    {
                        DbInterface.CacheGuildVariableEventTextLookups();
                    }

                    // Save the object. Resolves #915 - we needed to make sure we did this AFTER modifying the object if it was a quest
                    DbInterface.SaveGameObject(obj);

                    // Only replace the modified object
                    PacketSender.CacheGameDataPacket();

                    PacketSender.SendGameObjectToAll(obj, false);

                    if (type == GameObjectType.Npc)
                    {
                        foreach (var player in Globals.OnlineList)
                        {
                            PacketSender.SendKillCount(player, obj.Id);
                        }
                    }
                    else if (type == GameObjectType.Challenge || type == GameObjectType.WeaponType)
                    {
                        Globals.OnlineList.ForEach(player => player.SetMasteryProgress());
                    }
                    else if (type == GameObjectType.Resource)
                    {
                        Globals.RefreshGameObjectCache(GameObjectType.Resource, Globals.CachedResources);
                    }
                    else if (type == GameObjectType.Recipe)
                    {
                        Globals.RefreshGameObjectCache(GameObjectType.Recipe, Globals.CachedRecipes);
                        RecipeUnlockWatcher.QueueRefresh();
                    }
                }
            }
        }

        //SaveTimeDataPacket
        public void HandlePacket(Client client, Network.Packets.Editor.SaveTimeDataPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            TimeBase.GetTimeBase().LoadFromJson(packet.TimeJson);
            DbInterface.SaveTime();
            Time.Init();
            PacketSender.SendTimeBaseToAllEditors();
        }

        //AddTilesetsPacket
        public void HandlePacket(Client client, Network.Packets.Editor.AddTilesetsPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            foreach (var tileset in packet.Tilesets)
            {
                var value = tileset.Trim().ToLower();
                var found = false;
                foreach (var tset in TilesetBase.Lookup)
                {
                    if (tset.Value.Name.Trim().ToLower() == value)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    var obj = DbInterface.AddGameObject(GameObjectType.Tileset);
                    ((TilesetBase)obj).Name = value;
                    DbInterface.SaveGameObject(obj);
                    PacketSender.CacheGameDataPacket();
                    PacketSender.SendGameObjectToAll(obj);
                }
            }
        }

        //RequestGridPacket
        public void HandlePacket(Client client, Network.Packets.Editor.RequestGridPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            if (MapController.Lookup.Keys.Contains(packet.MapId))
            {
                if (client.IsEditor)
                {
                    PacketSender.SendMapGrid(client, MapController.Get(packet.MapId).MapGrid);
                }
            }
        }

        //OpenMapPacket
        public void HandlePacket(Client client, Network.Packets.Editor.EnterMapPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            client.EditorMap = packet.MapId;
        }

        //NeedMapPacket
        public void HandlePacket(Client client, Network.Packets.Editor.NeedMapPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var map = MapController.Get(packet.MapId);
            if (map != null)
            {
                PacketSender.SendMap(client, packet.MapId);
            }
        }

        #endregion
    }

    internal sealed partial class PacketHandler
    {
        public void HandlePacket(Client client, PartyInviteNamePacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            var target = Player.FindOnline(packet.Name);

            if (target == null)
            {
                PacketSender.SendChatMsg(player, Strings.Guilds.InviteNotOnline, ChatMessageType.Error);
                return;
            }

            target.InviteToParty(player);
        }

        public void HandlePacket(Client client, RequestLeaderboardPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            PlayerRecord.SendLeaderboardPageTo(player, packet.Type, packet.RecordId, packet.ScoreType, packet.Page, packet.DisplayMode, packet.Term);
        }

        public void HandlePacket(Client client, CloseLeaderboardPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.CloseLeaderboard = true;
        }

        public void HandlePacket(Client client, RequestLootUpdatePacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            var loot = player.CurrentLoot;
            Item lootAt = null;
            if (packet.Index > -1 && packet.Index >= loot.Count)
            {
#if DEBUG
                throw new IndexOutOfRangeException($"Requested loot index of {packet.Index} but loot count is {loot.Count}. Aborting.");
#else
                Log.Error($"Requested loot index of {packet.Index} but loot count is {loot.Count}. Aborting.");
                return;
#endif
            }
            else if (packet.Index > -1)
            {
                lootAt = loot[packet.Index];
            }

            var bankInterface = new BankInterface(player, ((IEnumerable<Item>)player.Bank).ToList(), new object(), null, Options.Instance.PlayerOpts.InitialBankslots);
            switch (packet.Type)
            {
                case LootUpdateType.TakeAt:
                    if (!player.TryGiveItem(lootAt, ItemHandling.Overflow, false, true, -1, -1, true))
                    {
                        return;
                    }

                    loot.RemoveAt(packet.Index);

                    if (lootAt.Descriptor.AnimationId != default && lootAt.Descriptor.ItemType == ItemTypes.Equipment && !string.IsNullOrEmpty(lootAt.Descriptor.Animation?.Sound))
                    {
                        PacketSender.SendPlaySound(player, lootAt.Descriptor.Animation?.Sound);
                    }
                    else
                    {
                        PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.TakeSound);
                    }
                    break;
                case LootUpdateType.TakeAll:
                    if (player.CurrentLoot == default)
                    {
                        break;
                    }

                    var preTakeCount = player.CurrentLoot.Count;
                    player.CurrentLoot.RemoveAll(item => player.TryGiveItem(item, ItemHandling.Overflow, false, true, -1, -1, true)); 

                    if (preTakeCount != player.CurrentLoot?.Count)
                    {
                        if (preTakeCount - player.CurrentLoot.Count == 1)
                        {
                            PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.TakeSound);
                        }
                        else
                        {
                            PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.TakeAllSound);
                        }
                    }
                    break;
                case LootUpdateType.BankAt:
                    if (!bankInterface.TryDepositItem(lootAt, false))
                    {
                        return;
                    }

                    loot.RemoveAt(packet.Index);
                    PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.BankSound);
                    break;
                case LootUpdateType.BankAll:
                    if (player.CurrentLoot == default)
                    {
                        break;
                    }

                    var preBankCount = player.CurrentLoot.Count;
                    player.CurrentLoot.RemoveAll(item => bankInterface.TryDepositItem(item, false));

                    if (preBankCount != player.CurrentLoot?.Count)
                    {
                        if (preBankCount - player.CurrentLoot?.Count == 1)
                        {
                            PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.BankSound);
                        }
                        else
                        {
                            PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.BankAllSound);
                        }
                    }
                    break;
                case LootUpdateType.DismissAt:
                    loot.RemoveAt(packet.Index);
                    PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.DismissSound);
                    break;
                case LootUpdateType.DismissAll:
                    if (player.CurrentLoot?.Count > 1)
                    {
                        PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.DismissAllSound);
                    }
                    else
                    {
                        PacketSender.SendPlaySound(player, Options.Instance.LootRollOpts.DismissSound);
                    }
                    player.CurrentLoot = new List<Item>();
                    break;
            }

            player.ClearLootIfDone();

            PacketSender.SendLootUpdatePacketTo(player);
        }

        public void HandlePacket(Client client, FinishedFadePacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.ClientAwaitingFadeCompletion = false;
        }

        public void HandlePacket(Client client, RequestRespawnPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            if (!player.PlayerDead)
            {
                // Maybe the client's out of sync?
                PacketSender.SendPlayerDeathInfoTo(player, player);
                PacketSender.SendRespawnFinished(player);
                return;
            }

            player.AcceptRespawn();
        }

        public void HandlePacket(Client client, RequestInstanceLeavePacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            if (!player.PlayerDead)
            {
                // Maybe the client's out of sync?
                PacketSender.SendPlayerDeathInfoTo(player, player);
            }
            else
            {
                player.WarpToLastOverworldLocation(false);
                player.Reset();
                player.SendPlayerDeathStatus();
            }

            PacketSender.SendRespawnFinished(player);
        }

        public void HandlePacket(Client client, ResourceInfoRequestPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            // If the players harvest info hasn't changed (new record or variable change), and
            // we have a cached value for the requested tool type
            /* if (player.UseCachedHarvestInfo && player.CachedHarvestInfo.TryGetValue(packet.Tool, out var cachedInfo))
             {
                 player?.SendPacket(cachedInfo);
                 return;
             }*/
            player.CacheHarvestInfo(packet.Tool);

            if (!player.CachedHarvestInfo.TryGetValue(packet.Tool, out var info))
            {
                return;
            }

            player?.SendPacket(info);
        }

        public void HandlePacket(Client client, RequestLabelsPacket packet)
        {
            var player = client?.Entity;

            player?.SendPacket(new Network.Packets.Server.PlayerLabelPackets(player.GetUnlockedLabels()));
            if (!player?.LabelTutorialDone ?? true)
            {
                PacketSender.SendEventDialog(player, Strings.Player.NametagTutorial1, default, default);
                PacketSender.SendEventDialog(player, Strings.Player.NametagTutorial2, default, default);
                player.LabelTutorialDone = true;
            }
        }

        public void HandlePacket(Client client, SetLabelPacket packet)
        {
            client?.Entity?.SetLabelTo(packet.DescriptorId);
            PacketSender.SendEntityDataToProximity(client?.Entity);
        }

        public void HandlePacket(Client client, RequestCosmeticsPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            PacketSender.SendUnlockedCosmeticsPacket(player, true);
        }

        public void HandlePacket(Client client, CosmeticChangePacket packet)
        {
            if (client?.Entity == null)
            {
                return;
            }
            
            client?.Entity?.SetCosmetic(packet.ItemId, packet.Slot);
            PacketSender.SendEntityDataToProximity(client?.Entity);
        }

        public void HandlePacket(Client client, RequestRecipesPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.SendPacket(player.GetRecipes(packet.CraftType));

            if (!player.RecipeTutorialDone)
            {
                PacketSender.SendEventDialog(player, Strings.Crafting.RecipeTutorial1, default, default);
                PacketSender.SendEventDialog(player, Strings.Crafting.RecipeTutorial2, default, default);
                PacketSender.SendEventDialog(player, Strings.Crafting.RecipeTutorial3, default, default);
                player.RecipeTutorialDone = true;
            }
        }

        public void HandlePacket(Client client, RequestRecipeRequirementsPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.SendPacket(player.GetRecipeProgress(packet.RecipeId));
        }

        public void HandlePacket(Client client, RequestUnlockedRecipesPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.SendPacket(new Network.Packets.Server.UnlockedRecipesPacket(player.UnlockedRecipeIds.ToList()));
        }

        public void HandlePacket(Client client, RequestKillCountsPacket packet)
        {
            PacketSender.SendBestiaryUnlocks(client?.Entity, false);
            PacketSender.SendKillCounts(client?.Entity);
        }

        public void HandlePacket(Client client, RequestKillCountPacket packet)
        {
            PacketSender.SendKillCount(client?.Entity, packet.NpcId);
        }

        public void HandlePacket(Client client, SkillPreparationPacket packet)
        {
            var player = client?.Entity;

            if (packet.Prepared)
            {
                player?.TryPrepareSkill(packet.SpellId);
            }
            else
            {
                player?.TryUnprepareSkill(packet.SpellId);
            }

            // Resend the skillbook so the client can reset the skill back to its actual prepared value
            PacketSender.SendSkillbookToClient(player);
        }
        public void HandlePacket(Client client, RequestSkillbookPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            PacketSender.SendSkillbookToClient(player);
        }

        public void HandlePacket(Client client, RequestChallengeProgressPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.SendPacket(player.GenerateChallengeProgressPacket());
        }

        public void HandlePacket(Client client, TrackWeaponTypePacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.TrackWeaponTypeProgress(packet.WeaponTypeId);
            PacketSender.SendExperience(player);
        }

        public void HandlePacket(Client client, UpgradeStatsPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.ReceiveStatChange(packet.VitalAssignments, packet.StatAssignments);
        }

        public void HandlePacket(Client client, CloseDeconstructorPacket packet)
        {
            client?.Entity?.CloseDeconstructor();
        }
        
        public void HandlePacket(Client client, AddFuelPacket packet)
        {
            client?.Entity?.Deconstructor?.CreateFuel(packet.SlotsAndQuantities);
        }

        public void HandlePacket(Client client, DeconstructItemsPacket packet)
        {
            client?.Entity?.Deconstructor?.DeconstructItemsInSlots(packet.Slots);
        }

        public void HandlePacket(Client client, CloseEnhancementPacket packet)
        {
            client?.Entity?.CloseEnhancement();
        }

        public void HandlePacket(Client client, ApplyEnhancementsPacket packet)
        {
            client?.Entity?.Enhancement?.TryApplyEnhancementsToWeapon(packet.Enhancements);
        }

        public void HandlePacket(Client client, RequestWeaponEnhancementRemovalPacket packet)
        {
            var player = client?.Entity;

            if (player == null)
            {
                return;
            }

            player.Enhancement?.TryRemoveEnhancementsOnItem();
        }

        public void HandlePacket(Client client, CloseUpgradeStationPacket packet)
        {
            client?.Entity?.CloseUpgradeStation();
        }

        public void HandlePacket(Client client, RequestUpgradePacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            var failure = "No item selected to upgrade!";
            player.TryGetWeaponPicked(out var weapon);
            if (weapon == default || (!player.UpgradeStation?.TryUpgradeItem(weapon, packet.CraftId, out failure) ?? false))
            {
                PacketSender.SendPlaySound(player, Options.UIDenySound);
                PacketSender.SendEventDialog(player, failure, string.Empty, Guid.Empty);
                return;
            }
        }

        public void HandlePacket(Client client, RequestChallengeContractPacket packet)
        {
            _ = client?.Entity?.TryAcceptChallengeContract(packet.ChallengeId);
        }

        public void HandlePacket(Client client, RequestLoadoutsPacket packet)
        {
            PacketSender.SendLoadouts(client?.Entity);
        }

        public void HandlePacket(Client client, SaveNewLoadoutPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            player.SaveNewLoadout(packet.LoadoutName);
        }

        public void HandlePacket(Client client, OverwriteLoadoutPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            player.OverwriteLoadout(packet.LoadoutId);
        }

        public void HandlePacket(Client client, SelectLoadoutPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            player.SelectLoadout(packet.LoadoutId);
        }

        public void HandlePacket(Client client, RemoveLoadoutPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            player.RemoveLoadout(packet.LoadoutId);
        }

        public void HandlePacket(Client client, RequestKnownEnhancementsPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            PacketSender.SendKnownEnhancementUpdate(player);
            if (!player.EnhancementTutorialDone)
            {
                player.SendDialogNotice(Strings.Crafting.EnhancementTutorial1);
                player.SendDialogNotice(Strings.Crafting.EnhancementTutorial2);
                player.SendDialogNotice(Strings.Crafting.EnhancementTutorial3);
                player.SendDialogNotice(Strings.Crafting.EnhancementTutorial4);
                player.EnhancementTutorialDone = true;
            }
        }

        public void HandlePacket(Client client, WeaponPickerResponsePacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            // Client sends -1 if the player just cancels the picker
            if (packet.SelectedSlot < 0)
            {
                player.CloseWeaponPicker();
                return;
            }

            if (player.WeaponPicker == default)
            {
                Log.Error($"No weapon picker existed for player {player.Name} when receiving a selection packet!");
            }

            if (!player.WeaponPicker.TryChooseWeapon(packet.SelectedSlot))
            {
                player.CloseWeaponPicker();
                return;
            }

            player.WeaponPicker.OpenNextInterface();
        }

        public void HandlePacket(Client client, WishlistAddPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            player.AddCraftToWishlist(packet.CraftId);
        }

        public void HandlePacket(Client client, WishlistRemovePacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            player.RemoveCraftFromWishlist(packet.CraftId);
        }

        public void HandlePacket(Client client, RequestTerritorySyncPacket packet)
        {
            var player = client?.Entity;
            if (player == default || !MapController.TryGetInstanceFromMap(packet.MapId, player.MapInstanceId, out var mapInstance))
            {
                return;
            }

            mapInstance.SendTerritoryUpdatesTo(player);
        }

        public void HandlePacket(Client client, Network.Packets.Editor.MapCopyPacket packet)
        {
            if (!client.IsEditor)
            {
                return;
            }

            var map = MapController.Get(packet.MapId);
            var copyingMap = MapController.Get(packet.CopyingMapId);

            if (map == null || copyingMap == null)
            {
                return;
            }

            map.Music = copyingMap.Music;
            map.Sound = copyingMap.Sound;
            map.ZoneType = copyingMap.ZoneType;
            
            map.Fog = copyingMap.Fog;
            map.FogTransparency = copyingMap.FogTransparency;
            map.FogXSpeed = copyingMap.FogXSpeed;
            map.FogYSpeed = copyingMap.FogYSpeed;
            
            map.Panorama = copyingMap.Panorama;
            
            map.OverlayGraphic = copyingMap.OverlayGraphic;
            map.AHue = copyingMap.AHue;
            map.RHue = copyingMap.RHue;
            map.GHue = copyingMap.GHue;
            map.BHue = copyingMap.BHue;

            map.IsIndoors = copyingMap.IsIndoors;

            map.Brightness = copyingMap.Brightness;
            map.PlayerLightColor = copyingMap.PlayerLightColor;
            map.PlayerLightExpand = copyingMap.PlayerLightExpand;
            map.PlayerLightIntensity = copyingMap.PlayerLightIntensity;
            map.PlayerLightSize = copyingMap.PlayerLightSize;

            map.WeatherAnimation = AnimationBase.Get(copyingMap.WeatherAnimationId);
            map.WeatherIntensity = copyingMap.WeatherIntensity;
            map.WeatherXSpeed = copyingMap.WeatherXSpeed;
            map.WeatherYSpeed = copyingMap.WeatherYSpeed;

            map.RegenType = copyingMap.RegenType;

            map.LoginEventId = copyingMap.LoginEventId;
            
            map.Revision += 1;

            MapList.List.UpdateMap(packet.MapId);
            DbInterface.SaveGameObject(map);

            var players = new List<Player>();
            foreach (var surrMap in map.GetSurroundingMaps(true))
            {
                players.AddRange(surrMap.GetPlayersOnAllInstances().ToArray());
            }

            foreach (var plyr in players)
            {
                plyr.Warp(plyr.MapId, (byte) plyr.X, (byte) plyr.Y, (byte) plyr.Dir, false, (byte) plyr.Z, true);
                PacketSender.SendMap(plyr.Client, packet.MapId);
            }

            PacketSender.SendMap(client, packet.MapId, true); //Sends map to everyone/everything in proximity
        }

        public void HandlePacket(Client client, RequestChallengeBonusesPacket packet)
        {
            var player = client?.Entity;
            if (player == default)
            {
                return;
            }

            List<int> values = new List<int>();
            foreach (EffectType effect in Enum.GetValues(typeof(EffectType))) 
            {
                values.Add(player.GetChallengeEffects(effect));
            }

            player.SendPacket(new Network.Packets.Server.ChallengeBonusesPacket(values.ToArray()));
        }
    }
}
