using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects.Events;
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

        public void Clear()
        {
            Records.Clear();
            Page = 0;
            SearchTerm = string.Empty;
        }

        public void RequestPage()
        {
            Loading = true;
            PacketSender.SendLeaderboardRequest(this);
        }

        public void NextPage()
        {
            Page++;
            RequestPage();
        }

        public void PreviousPage()
        {
            Page = MathHelper.Clamp(Page--, 0, int.MaxValue);
            RequestPage();
        }

        public void GotoPage(int page)
        {
            Page = page;
            RequestPage();
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
