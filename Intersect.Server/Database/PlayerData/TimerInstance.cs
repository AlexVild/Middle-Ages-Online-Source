using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Timers;
using Intersect.Server.Core;
using Intersect.Server.Core.Instancing.Controller;
using Intersect.Server.Entities;
using Intersect.Server.General;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData
{
    public class TimerInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public TimerInstance() { } // EF

        public TimerInstance(Guid descriptorId, Guid ownerId, long now, int completionCount)
        {
            DescriptorId = descriptorId;
            OwnerId = ownerId;
            ExpiryTime = now + (Descriptor.TimeLimit * 1000); // TimeLimit is in seconds, multiply accordingly
            CompletionCount = completionCount;
            StartTime = Timing.Global.MillisecondsUtc;
        }

        [ForeignKey(nameof(Descriptor))]
        public Guid DescriptorId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public TimerDescriptor Descriptor
        {
            get => TimerDescriptor.Get(DescriptorId);
            set => DescriptorId = value?.Id ?? Guid.Empty;
        }

        // Default if owner is server (global timer)
        public Guid OwnerId { get; set; }

        /// <summary>
        /// A UTC Timestamp of when the timer will next expire
        /// </summary>
        [Column("TimeRemaining")] // Deprecated solution, don't include [Column] to AGD (didn't wanna do a migration)
        public long ExpiryTime { get; set; }

        /// <summary>
        /// A MS calculation of how much time until we reach the expiry
        /// </summary>
        [NotMapped, JsonIgnore]
        public long TimeRemaining => ExpiryTime - Timing.Global.MillisecondsUtc;

        /// <summary>
        /// An amount of milliseconds that the timer was at when last paused. Used for repopulating a paused timer with correct values
        /// </summary>
        public long PausedTime { get; set; }

        /// <summary>
        /// How many times this timer has completed an interval
        /// </summary>
        public int CompletionCount { get; set; }

        public bool IsExpired => Timing.Global.MillisecondsUtc > ExpiryTime;

        /// <summary>
        /// Helper for determining if this timer has expired
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public bool IsCompleted => Descriptor.Repetitions != TimerConstants.TimerIndefiniteRepeat && CompletionCount >= Descriptor.Repetitions + 1;

        /// <summary>
        /// The timer's start time. Resets on expire.
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// Helper to calculate how long this timer has been running
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public long ElapsedTime => Timing.Global.MillisecondsUtc - StartTime;

        /// <summary>
        /// Increments a timers <see cref="CompletionCount"/> and fires events for the necessary players based on this timers <see cref="OwnerId"/>
        /// </summary>
        public void ExpireTimer(long now)
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                CompletionCount++;

                var affectedPlayers = GetAffectedPlayers(true);

                var hasFiredEvent = false;
                foreach (var player in affectedPlayers)
                {
                    if (!IsCompleted)
                    {
                        FireExpireEvent(player);
                        if (Descriptor?.SinglePlayerExpire ?? false)
                        {
                            break;
                        }
                    }
                    else
                    {
                        FireCompletionEvents(player);
                        if (Descriptor?.SinglePlayerCompletion ?? false)
                        {
                            break;
                        }
                    }
                }

                var descriptor = Descriptor;
                ProcessInstanceControllerActions(descriptor);

                if (!IsCompleted)
                {
                    ExpiryTime = now + (descriptor.TimeLimit * 1000); // Extend timer for next repetition
                    StartTime = now;
                }

                context.Timers.Update(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
            
            // If the timer doesn't need to be stopped yet, send timer packets signifying the expiry so that timer displays can be appropriately updated
            if (!IsCompleted)
            {
                SendTimerPackets();
            }
        }

        /// <summary>
        /// Processes actions on the instance controller level
        /// </summary>
        public void ProcessInstanceControllerActions(TimerDescriptor descriptor)
        {
            if (!descriptor.ActionsEnabled || !InstanceProcessor.TryGetInstanceController(OwnerId, out var instanceController))
            {
                return;
            }

            ProcessInstanceControllerVariableActions(descriptor, instanceController);
        }

        private void ProcessInstanceControllerVariableActions(TimerDescriptor descriptor, InstanceController instanceController)
        {
            if (descriptor.ActionVariable == default || !instanceController.InstanceVariables.TryGetValue(descriptor.ActionVariableId, out var instanceVar))
            {
                return;
            }

            if (instanceVar.Type != Enums.VariableDataTypes.Integer)
            {
                return;
            }

            bool iterate = false;
            if (descriptor.ActionType == (int)TimerInstanceActionType.EVERY)
            {
                try
                {
                    iterate = CompletionCount % descriptor.NValue == 0;
                }
                catch
                {
                    Console.WriteLine("Attempted to divide by zero when processing instance controller timer mod");
                    iterate = false;
                }
            }
            else if (descriptor.ActionType == (int)TimerInstanceActionType.ONCE)
            {
                iterate = CompletionCount == descriptor.NValue;
            }

            if (!iterate)
            {
                return;
            }

            try
            {
                switch ((VarActionModType)descriptor.InstanceVariableActionType)
                {
                    case VarActionModType.ADD:
                        instanceVar.Value += descriptor.ActionVariableChangeValue;
                        break;
                    case VarActionModType.DIV:
                        instanceVar.Value = instanceVar.Value / descriptor.ActionVariableChangeValue;
                        break;
                    case VarActionModType.MOD:
                        instanceVar.Value = instanceVar.Value % descriptor.ActionVariableChangeValue;
                        break;
                    case VarActionModType.MULT:
                        instanceVar.Value *= descriptor.ActionVariableChangeValue;
                        break;
                    case VarActionModType.SUB:
                        instanceVar.Value -= descriptor.ActionVariableChangeValue;
                        break;
                    case VarActionModType.TOGGLE:
                        if (instanceVar.Value == 0)
                        {
                            instanceVar.Value = 1;
                        }
                        else
                        {
                            instanceVar.Value = 0;
                        }
                        break;
                }
            } catch (DivideByZeroException e)
            {
                Console.WriteLine("Attempted to divide by zero when changing instance var value in timer action");
            }
        }

        /// <summary>
        /// Runs the cancellation event for all relevant players
        /// </summary>
        public void CancelTimer()
        {
            foreach (var player in GetAffectedPlayers())
            {
                player.EnqueueStartCommonEvent(Descriptor.CancellationEvent);
                if (Descriptor?.SinglePlayerCancellation ?? false)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Gets a list of players that should be affected by this timers completion event
        /// </summary>
        /// <returns>A list of <see cref="Player"/>s to be affected by timer expiration, based on the <see cref="TimerDescriptor.OwnerType"/></returns>
        public List<Player> GetAffectedPlayers(bool checkMapExclusivity = false)
        {
            var onlinePlayers = Globals.OnlineList;

            Func<Player, bool> isValid = (Player pl) => {
                return checkMapExclusivity ? Descriptor.ContainsExclusiveMap(pl.MapId) : true;
            };

            switch (Descriptor.OwnerType)
            {
                case TimerOwnerType.Global:
                    return onlinePlayers.Where(player => isValid(player)).ToList();

                case TimerOwnerType.Player:
                    return new List<Player> { onlinePlayers.Find(ply => ply.Id == OwnerId && isValid(ply)) };

                case TimerOwnerType.Instance:
                    return InstanceProcessor.TryGetInstanceController(OwnerId, out var instance) 
                        ? instance.Players.Where(ply => isValid(ply)).ToList()
                        : new List<Player>();

                case TimerOwnerType.Party:
                    return onlinePlayers.FindAll(ply => ply.Party != null && ply.Party.Count >= 1 && ply.Party[0].Id == OwnerId && isValid(ply));

                case TimerOwnerType.Guild:
                    return onlinePlayers.FindAll(ply => ply.Guild != null && ply.Guild.Id == OwnerId && isValid(ply));

                default:
                    throw new NotImplementedException($"{Enum.GetName(typeof(TimerOwnerType), Descriptor.OwnerType)} not implemented!");
            }
        }

        /// <summary>
        /// Returns whether or not this timer affects the given <see cref="Player"/>
        /// </summary>
        /// <param name="player">The player instance to check</param>
        /// <returns>True if the given player is affected by this timer</returns>
        public bool AffectsPlayer(Player player)
        {
            switch (Descriptor.OwnerType)
            {
                case TimerOwnerType.Global:
                    return true;

                case TimerOwnerType.Player:
                    return OwnerId == player.Id;

                case TimerOwnerType.Instance:
                    return player.MapInstanceId == OwnerId;

                case TimerOwnerType.Party:
                    return player.Party != null && player.Party.Count >= 1 && player.Party[0].Id == OwnerId;

                case TimerOwnerType.Guild:
                    return player.Guild != null && player.Guild.Id == OwnerId;

                default:
                    throw new NotImplementedException($"{Enum.GetName(typeof(TimerOwnerType), Descriptor.OwnerType)} not implemented!");
            }
        }

        /// <summary>
        /// Fires the appropriate timer event based on the timer's state
        /// </summary>
        /// <param name="player">The player that will experience the event.</param>
        private void FireExpireEvent(Player player)
        {
            if (player == default)
            {
                return;
            }

            player.EnqueueStartCommonEvent(EventBase.Get(Descriptor.ExpirationEventId));
        }

        private void FireCompletionEvents(Player player)
        {
            if (player == default)
            {
                return;
            }

            if (Descriptor.CompletionBehavior == TimerCompletionBehavior.ExpirationThenCompletion)
            {
                player.EnqueueStartCommonEvent(Descriptor.ExpirationEvent);
            }
            player.EnqueueStartCommonEvent(Descriptor.CompletionEvent);
        }

        /// <summary>
        /// Stores the timer's total elapsed time in a variable
        /// </summary>
        public void StoreElapsedTime()
        {
            // Log the elapsed time in a variable, if necessary
            var descriptor = Descriptor;
            if (descriptor.ElapsedTimeVariableId != default)
            {
                switch (descriptor.OwnerType)
                {
                    case TimerOwnerType.Global:
                        var globalVar = ServerVariableBase.Get(descriptor.ElapsedTimeVariableId);
                        if (globalVar != default)
                        {
                            globalVar.Value.Integer = ElapsedTime;
                        }
                        break;
                    case TimerOwnerType.Player:
                    case TimerOwnerType.Party:
                        foreach (var player in GetAffectedPlayers())
                        {
                            player.SetVariableValue(descriptor.ElapsedTimeVariableId, ElapsedTime, true);
                        }
                        break;
                    case TimerOwnerType.Instance:
                        MapInstance.SetInstanceVariable(descriptor.ElapsedTimeVariableId, ElapsedTime, OwnerId);
                        break;
                }
            }
        }

        public void SendTimerPackets()
        {
            if (!Descriptor.Hidden)
            {
                foreach (var player in GetAffectedPlayers())
                {
                    PacketSender.SendTimerPacket(player, this);
                }
            }
        }

        public bool ContainsExclusiveMap(Guid mapId)
        {
            return Descriptor?.ContainsExclusiveMap(mapId) ?? false;
        }

        [NotMapped]
        public bool IsExclusiveToMaps => Descriptor?.ExclusiveMaps?.Count > 0;

        public void SendTimerPacketTo(Player player)
        {
            if (!Descriptor.Hidden)
            {
                PacketSender.SendTimerPacket(player, this);
            }
        }

        public void SendTimerStopPackets()
        {
            if (!Descriptor.Hidden)
            {
                foreach (var player in GetAffectedPlayers())
                {
                    PacketSender.SendTimerStopPacket(player, this);
                }
            }
        }

        public void SendTimerStopPacketTo(Player player)
        {
            if (!Descriptor.Hidden)
            {
                PacketSender.SendTimerStopPacket(player, this);
            }
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Timers.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
