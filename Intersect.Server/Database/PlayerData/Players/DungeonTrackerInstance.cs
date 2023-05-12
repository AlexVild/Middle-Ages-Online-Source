using Intersect.GameObjects;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class DungeonTrackerInstance : IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Player Player { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        public Guid DungeonId { get; set; }

        public DungeonDescriptor Dungeon => DungeonDescriptor.Get(DungeonId);

        public int SoloCompletions { get; set; } = 0;

        public int GroupCompletions { get; set; } = 0;

        public int Failures { get; set; } = 0;

        [NotMapped, JsonIgnore]
        public int TotalCompletions => SoloCompletions + GroupCompletions;

        public long BestGroupTime { get; set; } = -1L;
        
        public long BestSoloTime { get; set; } = -1L;

        public string ElapsedSoloTime => TextUtils.GetTimeElapsedString(BestSoloTime, Strings.Events.ElapsedMinutes, Strings.Events.ElapsedHours, Strings.Events.ElapsedDays);
        
        public string ElapsedGroupTime => TextUtils.GetTimeElapsedString(BestGroupTime, Strings.Events.ElapsedMinutes, Strings.Events.ElapsedHours, Strings.Events.ElapsedDays);

        public string RecordDescription => Dungeon?.StoreLongestTime ?? false ? "longest" : "fastest";

        public GameObjects.Events.RecordScoring ScoreDirection => Dungeon?.StoreLongestTime ?? false ? GameObjects.Events.RecordScoring.High : GameObjects.Events.RecordScoring.Low;

        public DungeonTrackerInstance(Guid playerId, Guid dungeonId)
        {
            PlayerId = playerId;
            DungeonId = dungeonId;
        }

        public override bool Equals(object obj)
        {
            var other = obj as DungeonTrackerInstance;
            if (other == null)
            {
                return false;
            }
            return PlayerId == other.PlayerId && DungeonId == other.DungeonId;
        }

        public void IncrementCompletion(int partySize, Player player)
        {
            if (partySize > 1)
            {
                GroupCompletions++;
            }
            else
            {
                SoloCompletions++;
            }
            if (player?.TrySetRecord(GameObjects.Events.RecordType.TotalDungeonCompletions, DungeonId, TotalCompletions) ?? false)
            {
                var dungeonName = Dungeon?.DisplayName ?? "NOT FOUND";
                PacketSender.SendChatMsg(player, $"You've completed {dungeonName} {TotalCompletions} time(s)!", Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
            }
        }

        public bool TryUpdateTimeRecord(long completionTime, int partySize, Player player)
        {
            var dungeonName = Dungeon?.DisplayName ?? "NOT FOUND";
            if (partySize > 1)
            {
                if (BestGroupTime < 0)
                {
                    BestGroupTime = completionTime;
                    if (player?.TrySetRecord(GameObjects.Events.RecordType.GroupDungeonTimes, DungeonId, BestGroupTime, player.Party, scoreType: ScoreDirection) ?? false)
                    {
                        PacketSender.SendChatMsg(player, $"This is your {RecordDescription} {dungeonName} completion time with these party members!", Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                    }
                    return true;
                }

                if (ScoreDirection == GameObjects.Events.RecordScoring.High)
                {
                    if (completionTime < BestGroupTime || completionTime == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (completionTime >= BestGroupTime || completionTime == 0)
                    {
                        return false;
                    }
                }

                BestGroupTime = completionTime;
                if (player?.TrySetRecord(GameObjects.Events.RecordType.GroupDungeonTimes, DungeonId, BestGroupTime, player.Party, scoreType: ScoreDirection) ?? false)
                {
                    PacketSender.SendChatMsg(player, $"This is your {RecordDescription} {dungeonName} completion time with these party members!", Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                }
                return true;
            }
            else
            {
                if (BestSoloTime < 0)
                {
                    BestSoloTime = completionTime;
                    if (player?.TrySetRecord(GameObjects.Events.RecordType.SoloDungeonTimes, DungeonId, BestSoloTime, scoreType: ScoreDirection) ?? false)
                    {
                        PacketSender.SendChatMsg(player, $"You've broke your {dungeonName} solo time record!", Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                    }
                    return true;
                }

                if (ScoreDirection == GameObjects.Events.RecordScoring.High)
                {
                    if (completionTime < BestSoloTime || completionTime == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (completionTime >= BestSoloTime || completionTime == 0)
                    {
                        return false;
                    }
                }

                BestSoloTime = completionTime;
                if (player?.TrySetRecord(GameObjects.Events.RecordType.SoloDungeonTimes, DungeonId, BestSoloTime, scoreType: ScoreDirection) ?? false)
                {
                    PacketSender.SendChatMsg(player, $"You've broke your {dungeonName} solo time record!", Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted);
                }
                return true;
            }
        }

        public DungeonTrackerInstance()
        {
        }
    }
}
