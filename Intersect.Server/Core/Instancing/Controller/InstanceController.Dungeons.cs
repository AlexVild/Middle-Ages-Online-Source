using System;
using System.Collections.Generic;
using Intersect.GameObjects;
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
        public Dungeon Dungeon { get; set; } = new Dungeon(Guid.Empty);

        public string DungeonName => DungeonDescriptor == null ? string.Empty : DungeonDescriptor?.DisplayName ?? DungeonDescriptor.GetName(DungeonId);
        public DungeonDescriptor DungeonDescriptor { get; set; }

        public Guid DungeonId => Dungeon?.DescriptorId ?? Guid.Empty;

        public DungeonState DungeonState => Dungeon?.State ?? DungeonState.Null;
        public bool DungeonActive => Dungeon?.State == DungeonState.Active;
        bool DungeonReady => Dungeon?.State == DungeonState.Inactive;
        public bool InstanceIsDungeon => Dungeon?.State != DungeonState.Null;
        bool DungeonJoinable => !(Dungeon?.State == DungeonState.Null || Dungeon?.State == DungeonState.Complete);

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
            Dungeon.GnomeLocation = Randomization.Next(DungeonDescriptor.GnomeLocations);
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
                if (!DungeonDescriptor.IgnoreStartEvents)
                {
                    participant.StartCommonEventsWithTrigger(CommonEventTrigger.DungeonStart);
                }
                if (Dungeon.IsSolo)
                {
                    PacketSender.SendChatMsg(participant, $"You've started a solo run of {DungeonDescriptor.DisplayName}!", ChatMessageType.Party, CustomColors.General.GeneralWarning);
                }
                else
                {
                    PacketSender.SendChatMsg(participant, $"{player.Name} has started {DungeonDescriptor.DisplayName}!", ChatMessageType.Party, CustomColors.General.GeneralWarning);
                }
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
            if (DungeonParticipants == 1 && (Dungeon.Participants[0].Party == null || Dungeon.Participants[0].Party.Count < 2))
            {
                Dungeon.IsSolo = true;
            }
            return true;
        }

        public void CompleteDungeon(Player player)
        {
            if (Dungeon?.State != DungeonState.Active)
            {
                Logging.Log.Error($"--- Dungeon issue for {player.Name}!");
                Logging.Log.Error($"--- Tried to complete a dungeon, but the dungeon was never active! Dungeon state was {Dungeon.State}");
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
                pl.FullHeal();
                pl.ClearHostileDoTs();

                if (!DungeonDescriptor.IgnoreCompletionEvents)
                {
                    pl.StartCommonEventsWithTrigger(CommonEventTrigger.DungeonComplete);
                }
                pl.EnqueueStartCommonEvent(timer.CompletionEvent);
                PacketSender.SendChatMsg(pl, $"You completed {DungeonDescriptor.DisplayName} in {Dungeon.CompletionTimeString}", ChatMessageType.Experience, CustomColors.General.GeneralCompleted);

                if (!Dungeon.RecordsVoid)
                {
                    pl.TrackDungeonCompletion(Dungeon.DescriptorId, DungeonParticipants, Dungeon.CompletionTime);
                }
                else
                {
                    PacketSender.SendChatMsg(pl, $"Your ability to set records for this run has been voided due to admin warping.", ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                }
            }

            Dungeon.RecordsVoid = false; // reset void
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

            loot.AddRange(treasure);

            return loot;
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
            if (!DungeonActive || Dungeon.GnomeObtained)
            {
                return;
            }

            Dungeon.GnomeObtained = true;
            foreach (var player in Dungeon.Participants.ToArray())
            {
                player.StartCommonEventsWithTrigger(CommonEventTrigger.GnomeCaptured);
                PacketSender.SendChatMsg(player, "Your party found the treasure gnome! You will receive greater rewards for completion of this dungeon.", Enums.ChatMessageType.Party, CustomColors.General.GeneralPrimary);
                if (InstanceLives >= Options.Instance.Instancing.MaxSharedInstanceLives)
                {
                    break;
                }
                
                InstanceLives++;
                PacketSender.SendInstanceLivesPacket(player, (byte)InstanceLives);
                PacketSender.SendChatMsg(player, "The treasure gnome awards your party with an additional life!", Enums.ChatMessageType.Party, CustomColors.General.GeneralPrimary);
            }
        }
    }
}
