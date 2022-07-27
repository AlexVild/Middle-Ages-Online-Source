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

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
