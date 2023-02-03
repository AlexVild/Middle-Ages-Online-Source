using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.GameObjects;
using Intersect.Server.Maps;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Core.Instancing.Controller.Components;
using Intersect.Utilities;
using Intersect.Server.Networking;
using Intersect.Enums;

namespace Intersect.Server.Core.Instancing.Controller
{
    public sealed partial class InstanceController
    {
        Dungeon Dungeon { get; set; } = new Dungeon(Guid.Empty);

        DungeonDescriptor DungeonDescriptor { get; set; }
        
        bool DungeonActive => Dungeon?.State == DungeonState.Active;
        bool DungeonReady => Dungeon?.State == DungeonState.Inactive;
        bool InstanceIsDungeon => Dungeon?.State != DungeonState.Null;
        bool DungeonJoinable => Dungeon?.State == DungeonState.Null || Dungeon?.State == DungeonState.Complete;

        public int DungeonParticipants => Dungeon?.Participants?.Count ?? 0;

        public bool TryInitializeOrJoinDungeon(Guid dungeonId, Player player)
        {
            InitializeDungeon(dungeonId);
            return TryAddPlayerToDungeon(player);
        }

        void InitializeDungeon(Guid dungeonId)
        {
            if (Dungeon.State != DungeonState.Null)
            {
                return;
            }
            Dungeon = new Dungeon(dungeonId);
            
            DungeonDescriptor = DungeonDescriptor.Get(dungeonId);
            if (DungeonDescriptor == default)
            {
                return;
            }

            Dungeon.State = DungeonState.Inactive;
            Dungeon.GnomeLocation = Randomization.Next(DungeonDescriptor.GnomeLocations + 1);
        }

        public void StartDungeon(Player player)
        {
            if (!DungeonReady && DungeonDescriptor != default)
            {
                return;
            }

            var timer = DungeonDescriptor.Timer;
            if (timer != default 
                && TimerProcessor.TryGetOwnerId(timer.OwnerType, timer.Id, player, out var ownerId) 
                && !TimerProcessor.TryGetActiveTimer(timer.Id, ownerId, out _))
            {
                TimerProcessor.AddTimer(timer.Id, ownerId, Timing.Global.MillisecondsUtc);
            }

            foreach (var participant in Dungeon.Participants)
            {
                participant.StartCommonEventsWithTrigger(CommonEventTrigger.DungeonStart);
                PacketSender.SendChatMsg(participant, $"{player.Name} has started {DungeonDescriptor.DisplayName}!", ChatMessageType.Party, CustomColors.General.GeneralWarning);
            }

            Dungeon.State = DungeonState.Active;
        }

        bool TryAddPlayerToDungeon(Player player)
        {
            if (!DungeonJoinable)
            {
                PacketSender.SendChatMsg(player, $"This dungeon has already been completed.", ChatMessageType.Notice, CustomColors.General.GeneralWarning);
                return false;
            }

            Dungeon.Participants.Add(player);
            return true;
        }

        public void CompleteDungeon(Player player)
        {
            if (Dungeon?.State != DungeonState.Active)
            {
                return;
            }

            Dungeon.State = DungeonState.Complete;

            // Complete the dungeon timer & fire its events
            var timer = DungeonDescriptor.Timer;
            if (timer == default
                || !TimerProcessor.TryGetOwnerId(timer.OwnerType, timer.Id, player, out var ownerId)
                || !TimerProcessor.TryGetActiveTimer(timer.Id, ownerId, out var activeTimer))
            {
                return;
            }
            
            Dungeon.CompletionTime = activeTimer.ElapsedTime;
            GiveDungeonRewards();
                
            TimerProcessor.RemoveTimer(activeTimer);
            foreach (var pl in Dungeon.Participants)
            {
                pl.StartCommonEventsWithTrigger(CommonEventTrigger.DungeonComplete);
                pl.EnqueueStartCommonEvent(timer.CompletionEvent);

                // Mark a completion
                if (DungeonDescriptor.CompletionCounter != default)
                {
                    var completions = pl.GetVariableValue(DungeonDescriptor.CompletionCounterId);
                    pl.SetVariableValue(DungeonDescriptor.CompletionCounterId, completions + 1, DungeonDescriptor.CompletionCounter.Recordable);
                    PacketSender.SendChatMsg(pl, $"You've completed {DungeonDescriptor.DisplayName} {completions + 1} times!", ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                }
            }
        }

        void GiveDungeonRewards()
        {
            SetTreasureLevel();

            _ = DungeonDescriptor.ExpRewards.TryGetValue(Dungeon.TreasureLevel, out var dungeonExp);
            foreach (var player in Dungeon.Participants)
            {
                player.GiveExperience(dungeonExp);
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

            if (!DungeonDescriptor.Treasure.TryGetValue(Dungeon.TreasureLevel, out var treasure))
            {
                return loot;
            }

            treasure.AddRange(loot);

            return treasure;
        }

        void SetTreasureLevel()
        {
            if (!InstanceIsDungeon || DungeonDescriptor == default)
            {
                return;
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
                        Dungeon.TreasureLevel++;
                    }
                }
                break;
            }
        }

        public void RemoveDungeon()
        {
            Dungeon = new Dungeon(Guid.Empty);
            Logging.Log.Debug($"Removing dungeon on instance {InstanceId}");
        }

        public void ResetDungeon()
        {
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
            if (!DungeonActive || !Dungeon.GnomeObtained)
            {
                return;
            }

            Dungeon.GnomeObtained = true;
            foreach (var player in Dungeon.Participants)
            {
                player.StartCommonEventsWithTrigger(CommonEventTrigger.GnomeCaptured);
                PacketSender.SendChatMsg(player, "Your party found the treasure gnome! You will receive greater rewards for completion of this dungeon.", Enums.ChatMessageType.Party, CustomColors.General.GeneralPrimary);
                if (player.InstanceLives >= Options.Instance.Instancing.MaxSharedInstanceLives)
                {
                    break;
                }
                player.InstanceLives++;
                PacketSender.SendInstanceLivesPacket(player, (byte)player.InstanceLives);
                PacketSender.SendChatMsg(player, "The treasure gnome awards your party with an additional life!", Enums.ChatMessageType.Party, CustomColors.General.GeneralPrimary);
            }
        }

        public DungeonState CurrentDungeonState => Dungeon?.State ?? DungeonState.Null;
    }
}
