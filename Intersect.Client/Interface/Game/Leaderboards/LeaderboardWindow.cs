using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.Leaderboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Leaderboards
{
    public class LeaderboardWindow : IDisposable
    {
        private Canvas GameCanvas;

        private WindowControl Background;

        private Label HeadingIndex;
        private Label HeadingValue;
        private Label HeadingHolder;

        private ScrollControl RecordContainer;
        private List<LeaderboardRecord> Records;

        private Button NextPage;
        private Label CurrentPage;
        private Button PrevPage;

        private Label SearchLabel;
        private ImagePanel SearchBackground;
        private TextBox Search;
        private Button SearchButton;

        private Leaderboard CurrentLeaderboard => Globals.Me?.Leaderboard ?? null;

        public LeaderboardWindow(Canvas gameCanvas)
        {
            GameCanvas = gameCanvas;
            Background = new WindowControl(gameCanvas, string.Empty, false, "LeaderboardWindow");
            Background.DisableResizing();

            SearchLabel = new Label(Background, "SearchLabel");
            SearchBackground = new ImagePanel(Background, "SearchBackground");
            Search = new TextBox(SearchBackground, "SearchField");
            SearchButton = new Button(Background, "SearchButton");

            RecordContainer = new ScrollControl(SearchBackground, "RecordContainer");
            Records = new List<LeaderboardRecord>();

            NextPage = new Button(Background, "NextPageButton");
            CurrentPage = new Label(Background, "CurrentPageLabel");
            PrevPage = new Button(Background, "PrevPageButton");

            Background.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Update()
        {
            if (CurrentLeaderboard == null)
            {
                Dispose();
            }

            Background.IsHidden = !CurrentLeaderboard.IsOpen;

            if (Background.IsHidden)
            {
                return;
            }

            Background.Title = CurrentLeaderboard.DisplayName;
        }

        public void Open()
        {
            ClearRecords();
            CurrentLeaderboard.Loading = true;
            CurrentLeaderboard.IsOpen = true;
            // Request leaderboard information
        }

        private void ClearRecords()
        {
            foreach (var record in Records)
            {
                record.Dispose();
            }
            Records.Clear();
        }

        public void LoadRecords()
        {
            ClearRecords();
            foreach (var record in CurrentLeaderboard.Records)
            {
                var row = new LeaderboardRecord(RecordContainer, record);
                Records.Add(row);
                row.Initialize();
            }
        }

        public void Dispose()
        {
            ClearRecords();
            Background.Dispose();
        }
    }
}
