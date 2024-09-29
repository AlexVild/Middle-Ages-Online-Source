using System;
using System.Collections.Generic;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Core.Instancing.Controller.Components;
using Intersect.Utilities;
using Intersect.Server.Networking;
using Intersect.Enums;
using Intersect.Server.Database.PlayerData;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        public Dungeon Dungeon { get; set; }

        private static readonly object DungeonLock = new object();

        public string DungeonName => DungeonDescriptor.GetName(DungeonId);
        public DungeonDescriptor DungeonDescriptor => Dungeon?.Descriptor;
        public Guid DungeonId => Dungeon?.DescriptorId ?? Guid.Empty;

        public bool InstanceIsDungeon => Dungeon != null;

        public int DungeonParticipants => Dungeon?.Participants?.Count ?? 0;

        public void InitializeDungeon(Guid dungeonId)
        {
            if (Dungeon != null && (Dungeon.State == DungeonState.Active || Dungeon.State == DungeonState.Complete))
            {
                Logging.Log.Info($"DUNGEON: Tried to initialize {dungeonId} but it was either active or completed.");
                return;
            }

            lock (DungeonLock)
            {
                Dungeon = new Dungeon(dungeonId);
                if (DungeonDescriptor == default)
                {
                    Logging.Log.Warn($"DUNGEON: Failed to create a dungeon with ID {dungeonId}! Dungeon does not exist in the Game DB");
                    return;
                }

                Logging.Log.Info($"DUNGEON: Initializing dungeon: {DungeonName}...");
                Dungeon.GnomeLocation = Randomization.Next(DungeonDescriptor.GnomeLocations);
            }
        }

        public void StartDungeon(Player player)
        {
            if (Dungeon == null)
            {
                Logging.Log.Warn($"{player?.Name} tried to start {DungeonDescriptor?.Name} but it wasn't initialized!");
                return;
            }

            if (Dungeon.State == DungeonState.Complete)
            {
                Logging.Log.Warn($"{player?.Name} tried to start {DungeonDescriptor?.Name} but it was already completed!");
                return;
            }

            lock (DungeonLock)
            {
                Logging.Log.Info($"DUNGEON: {DungeonDescriptor.GetName(Dungeon.DescriptorId)} started by {player?.Name}, state is {Dungeon?.State} and will become Active");

                StartDungeonTimer(player);

                if (!DungeonDescriptor.IgnoreStartEvents)
                {
                    Player.StartCommonEventsWithTriggerForAllOnInstance(CommonEventTrigger.DungeonStart, InstanceId);
                }

                if (Dungeon.IsSolo)
                {
                    BroadcastDungeonMessage($"You've started a solo run of {DungeonDescriptor.DisplayName}!");
                }
                else
                {
                    BroadcastDungeonMessage($"{player.Name} has started {DungeonDescriptor.DisplayName}!");
                }

                Dungeon.State = DungeonState.Active;
            }
        }

        public void CompleteDungeon(Player player)
        {
            if (Dungeon == null)
            {
                Logging.Log.Warn($"DUNGEON: {player?.Name} tried to complete a Dungeon that was never created");
                return;
            }

            if (Dungeon.State != DungeonState.Active)
            {
                Logging.Log.Error($"DUNGEON: {player?.Name} Tried to complete a dungeon, but the dungeon was never active! Dungeon state was {Dungeon.State}");
                return;
            }

            lock (DungeonLock)
            {
                Logging.Log.Info($"Player {player.Name} completed Dungeon {Dungeon.DescriptorId}");
                Dungeon.State = DungeonState.Complete;

                if (!DungeonDescriptor.IgnoreCompletionEvents)
                {
                    Player.StartCommonEventsWithTriggerForAllOnInstance(CommonEventTrigger.DungeonComplete, InstanceId);
                }

                // Complete the dungeon timer & fire its events
                var timer = DungeonDescriptor.Timer;
                TimerProcessor.TryGetOwnerId(timer.OwnerType, timer.Id, player, out var ownerId);
                TimerProcessor.TryGetActiveTimer(timer.Id, ownerId, out var activeTimer);
                var isTimed = timer != default && ownerId != default && activeTimer != default;

                if (isTimed)
                {
                    SetCompletionTime(activeTimer);
                }

                _ = DungeonDescriptor.ExpRewards.TryGetValue(GetDungeonTreasureLevel(), out var dungeonExp);
                foreach (var pl in Dungeon.Participants.ToArray())
                {
                    pl.FullHeal();
                    pl.ClearHostileDoTs();
                    pl.GiveExperience(dungeonExp);

                    if (Dungeon.RecordsVoid)
                    {
                        PacketSender.SendChatMsg(pl, $"Your ability to set records for this run has been voided due to admin warping.", ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                    }
                    else
                    {
                        pl.TrackDungeonCompletion(Dungeon.DescriptorId, DungeonParticipants, Dungeon.CompletionTime);
                    }

                    if (!isTimed)
                    {
                        continue;
                    }

                    pl.EnqueueStartCommonEvent(timer.CompletionEvent);
                    PacketSender.SendChatMsg(pl, $"You completed {DungeonDescriptor.DisplayName} in {Dungeon.CompletionTimeString}", ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                }

                // Reset admin warp record void as the dungeon timer has been stopped and player records have been logged
                Dungeon.RecordsVoid = false;
            }
        }

        public bool TryAddPlayerToDungeon(Player player)
        {
            if (Dungeon == null)
            {
                Logging.Log.Warn($"DUNGEON: {player.Name} tried to join a dungeon that did not exist");
                return false;
            }

            if (Dungeon.State == DungeonState.Complete)
            {
                PacketSender.SendChatMsg(player, $"This dungeon has already been completed.", ChatMessageType.Notice, CustomColors.General.GeneralWarning);
                Logging.Log.Trace($"Player {player.Name} tried to join Dungeon {DungeonName} but it was already completed.");
                return false;
            }

            if (Dungeon.Participants.Contains(player))
            {
                Logging.Log.Trace($"DUNGEON: {player.Name} tried to join {DungeonName} but was already there");
                return false;
            }

            lock (DungeonLock)
            {
                Logging.Log.Info($"DUNGEON: {player.Name} joined dungeon: {DungeonName}");
                Dungeon.Participants.Add(player);

                if (DungeonParticipants == 1 && (Dungeon.Participants[0].Party == null || Dungeon.Participants[0].Party.Count < 2))
                {
                    Dungeon.IsSolo = true;
                }
            }
            return true;
        }

        public void RemovePlayerFromDungeon(Guid playerId)
        {
            if (Dungeon == null)
            {
                Logging.Log.Error($"DUNGEON: Player {playerId} tried to leave a dungeon that was never initialized");
                return;
            }

            var player = Player.FindOnline(playerId);
            if (player != null)
            {
                Logging.Log.Info($"DUNGEON: {player.Name} has left {DungeonName}");
            }

            lock (DungeonLock)
            {
                Dungeon.Participants.RemoveAll(pl => pl.Id == playerId);
            }
        }

        public List<LootRoll> GetDungeonLoot()
        {
            var loot = new List<LootRoll>();
            if (Dungeon?.State != DungeonState.Complete || DungeonDescriptor == default)
            {
                return loot;
            }

            if (Dungeon.GnomeObtained)
            {
                loot.AddRange(DungeonDescriptor.GnomeTreasure);
            }

            if (!DungeonDescriptor.Treasure.TryGetValue(GetDungeonTreasureLevel(), out var treasure))
            {
                return loot;
            }

            loot.AddRange(treasure);

            return loot;
        }

        public int GetDungeonTreasureLevel()
        {
            var treasureLevel = 0;
            if (Dungeon == null || DungeonDescriptor == null || DungeonDescriptor.Timer == null)
            {
                return treasureLevel;
            }

            foreach(var timeReq in DungeonDescriptor.SortedTimeRequirements)
            {
                if (timeReq.Participants > DungeonParticipants)
                {
                    continue;
                }

                foreach (var requiredTime in timeReq.Requirements)
                {
                    if (Dungeon.CompletionTime < requiredTime)
                    {
                        treasureLevel++;
                    }
                }
                break;
            }

            return treasureLevel;
        }

        public void RemoveDungeon(Player player)
        {
            lock (DungeonLock)
            {
                if (DungeonDescriptor.Timer != null &&
                    TimerProcessor.TryGetOwnerId(DungeonDescriptor.Timer.OwnerType, DungeonDescriptor.Timer.Id, player, out var ownerId) &&
                    TimerProcessor.TryGetActiveTimer(DungeonDescriptor.Timer.Id, ownerId, out var activeTimer))
                {
                    TimerProcessor.RemoveTimer(activeTimer);
                }

                Dungeon = null;
                Logging.Log.Trace($"DUNGEON: Removing dungeon on instance {InstanceId}");
            }
        }

        public void ResetDungeon()
        {
            if (Dungeon == null)
            {
                Logging.Log.Warn($"DUNGEON: Tried to reset a dungeon that was never initialized!");
                return;
            }

            var participants = Dungeon.Participants.ToArray();
            var dungeonId = Dungeon.DescriptorId;

            InitializeDungeon(dungeonId);
            
            foreach(var participant in participants)
            {
                _ = TryAddPlayerToDungeon(participant);
            }
        }

        public void GetGnome()
        {
            if (Dungeon.State != DungeonState.Active || Dungeon.GnomeObtained)
            {
                return;
            }

            lock (DungeonLock)
            {
                Dungeon.GnomeObtained = true;
                Player.StartCommonEventsWithTriggerForAllOnInstance(CommonEventTrigger.GnomeCaptured, InstanceId);

                if (InstanceLives < Options.Instance.Instancing.MaxSharedInstanceLives)
                {
                    InstanceLives++;
                    SendInstanceLifeUpdate(InstanceLives, noChat: true);
                    BroadcastDungeonMessage("The treasure gnome awards your party with an additional life!");
                }
            }

            BroadcastDungeonMessage("Your party found the treasure gnome! You will receive greater rewards for completion of this dungeon.");
        }

        #region Messaging
        private void BroadcastDungeonMessageTo(Player player, string message, bool sendToast = false, ChatMessageType chatMessageTypeOverride = ChatMessageType.Party)
        {
            PacketSender.SendChatMsg(player, message, chatMessageTypeOverride, CustomColors.General.GeneralWarning, sendToast: sendToast);
        }

        private void BroadcastDungeonMessage(string message, bool sendToast = false, ChatMessageType chatMessageTypeOverride = ChatMessageType.Party)
        {
            foreach (var participant in Dungeon.Participants.ToArray())
            {
                PacketSender.SendChatMsg(participant, message, chatMessageTypeOverride, CustomColors.General.GeneralWarning, sendToast: sendToast);
            }
        }

        private void BroadcastDungeonMessages(List<string> messages, bool sendToast = false, ChatMessageType chatMessageTypeOverride = ChatMessageType.Party)
        {
            foreach (var participant in Dungeon.Participants.ToArray())
            {
                foreach (var message in messages)
                {
                    PacketSender.SendChatMsg(participant, message, chatMessageTypeOverride, CustomColors.General.GeneralWarning, sendToast: sendToast);
                }
            }
        }
        #endregion

        private void StartDungeonTimer(Player player)
        {
            lock (DungeonLock)
            {
                var timer = DungeonDescriptor.Timer;
                if (timer != default
                    && TimerProcessor.TryGetOwnerId(timer.OwnerType, timer.Id, player, out var ownerId)
                    && !TimerProcessor.TryGetActiveTimer(timer.Id, ownerId, out _))
                {
                    TimerProcessor.AddTimer(timer.Id, ownerId, Timing.Global.MillisecondsUtc);
                }
            }
        }

        private void SetCompletionTime(TimerInstance timerInstance)
        {
            if (timerInstance == null)
            {
                return;
            }

            lock (DungeonLock)
            {
                Dungeon.CompletionTime = timerInstance.ElapsedTime;
            }

            TimerProcessor.RemoveTimer(timerInstance, true);
        }
    }
}
