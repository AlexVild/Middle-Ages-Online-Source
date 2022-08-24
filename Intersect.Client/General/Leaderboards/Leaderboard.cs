using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Collections.Generic;

namespace Intersect.Client.General.Leaderboards
{
    public class Leaderboard
    {
        public List<Record> Records { get; set; } = new List<Record>();

        public int Page { get; set; } = 0;

        public string SearchTerm { get; set; } = string.Empty;

        public bool Loading { get; set; } = false;

        public bool IsOpen { get; set; } = false;

        public string DisplayName { get; set; } = "LEADERBOARD";

        public RecordType Type { get; set; }

        public Guid RecordId { get; set; }

        public RecordScoring ScoreType { get; set; }

        public LeaderboardDisplayMode DisplayMode { get; set; }

        public void Clear()
        {
            Records.Clear();
            Page = 0;
            SearchTerm = string.Empty;
        }

        public void RequestPage(int page)
        {
            Loading = true;
            PacketSender.SendLeaderboardRequest(this, page);
        }

        public void RequestPlayersRecord()
        {
            Loading = true;
            PacketSender.SendLeaderboardRequestForPlayer(this);
        }

        public void NextPage()
        {
            RequestPage(Page + 1);
        }

        public void PreviousPage()
        {
            RequestPage(Page - 1);
        }

        public void GotoPage(int page)
        {
            Page = page;
            RequestPage(page);
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
            Clear();
        }
    }
}
