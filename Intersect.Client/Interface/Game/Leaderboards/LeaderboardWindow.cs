﻿using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.General.Leaderboards;
using Intersect.Client.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Leaderboards
{
    public class LeaderboardWindow : Base
    {
        private Canvas GameCanvas;

        private WindowControl Background;

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

        private Label LoadingLabel;

        private Leaderboard CurrentLeaderboard => Globals.Me?.Leaderboard ?? null;

        public LeaderboardWindow(Canvas gameCanvas)
        {
            GameCanvas = gameCanvas;
            Background = new WindowControl(gameCanvas, string.Empty, false, "LeaderboardWindow", onClose: OnClose);
            Background.DisableResizing();

            SearchBackground = new ImagePanel(Background, "SearchBackground");
            Search = new TextBox(SearchBackground, "SearchField");
            SearchButton = new Button(Background, "SearchButton")
            {
                Text = Strings.Leaderboard.SearchButton
            };

            HeadingHolder = new Label(Background, "HeadingHolder")
            {
                Text = Strings.Leaderboard.RecordHolder
            };
            HeadingValue = new Label(Background, "HeadingValue")
            {
                Text = Strings.Leaderboard.Record
            };
            RecordContainer = new ScrollControl(Background, "RecordContainer");
            Records = new List<LeaderboardRecord>();

            NextPage = new Button(Background, "NextPageButton")
            {
                Text = Strings.Leaderboard.Next
            };
            NextPage.Clicked += NextPageClicked;
            CurrentPage = new Label(Background, "CurrentPageLabel")
            {
                Text = Strings.Leaderboard.Page.ToString(Globals.Me?.Leaderboard?.Page + 1 ?? 1)
            };
            PrevPage = new Button(Background, "PrevPageButton")
            {
                Text = Strings.Leaderboard.Prev
            };
            PrevPage.Clicked += PrevPageClicked;

            LoadingLabel = new Label(Background, "LoadingLabel")
            {
                Text = Strings.Leaderboard.Loading
            };

            Interface.FocusElements.Add(Search);

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
                Interface.InputBlockingElements.Remove(this);
                return;
            }
            else if (!Interface.InputBlockingElements.Contains(this))
            {
                Interface.InputBlockingElements.Add(this);
            }

            LoadingLabel.IsHidden = !CurrentLeaderboard.Loading;
            if (Globals.Me.Leaderboard.Records.Count == 0)
            {
                LoadingLabel.IsHidden = false;
                LoadingLabel.Text = Strings.Leaderboard.NoRecords;
            }
            else
            {
                LoadingLabel.Text = Strings.Leaderboard.Loading;
            }

            RecordContainer.IsHidden = CurrentLeaderboard.Loading && CurrentLeaderboard.Records.Count > 0;

            SearchButton.IsDisabled = CurrentLeaderboard.Loading || string.IsNullOrEmpty(Search.Text);
            NextPage.IsDisabled = (CurrentLeaderboard.Records.Count != Options.Instance.RecordOpts.RecordsPerLeaderboardPage) || CurrentLeaderboard.Loading;
            PrevPage.IsDisabled = CurrentLeaderboard.Page == 0 || CurrentLeaderboard.Loading;

            Background.Title = CurrentLeaderboard.DisplayName;
            CurrentPage.Text = Strings.Leaderboard.Page.ToString(CurrentLeaderboard?.Page + 1 ?? 1);
        }

        private void ClearRecords()
        {
            foreach (var child in RecordContainer.Children.ToArray())
            {
                RecordContainer.RemoveChild(child, false);
            }
            Records.Clear();
        }

        public void LoadRecords()
        {
            ClearRecords();
            var idx = 0;
            foreach (var record in CurrentLeaderboard.Records)
            {
                var row = new LeaderboardRecord(RecordContainer, record);
                Records.Add(row);
                row.Initialize();
                
                if (idx % 2 == 1)
                {
                    row.RecordBackground.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "leaderboard_banding.png");
                }

                var holders = record.Holder.Split(' ');
                foreach(var holder in holders)
                {
                    var username = holder.Replace(",", string.Empty).Trim();
                    if (username == Globals.Me.Name)
                    {
                        row.RecordBackground.Texture = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "leaderboard_banding_self.png");
                        break;
                    }
                }

                row.RecordBackground.Y += idx * row.RecordBackground.Height;
                idx++;
            }
        }

        private void OnClose()
        {
            CurrentLeaderboard.IsOpen = false;
        }

        private void NextPageClicked(Base sender, ClickedEventArgs arguments)
        {
            CurrentLeaderboard.NextPage();
        }

        private void PrevPageClicked(Base sender, ClickedEventArgs arguments)
        {
            CurrentLeaderboard.PreviousPage();
        }

        public void Dispose()
        {
            ClearRecords();
            Background.Dispose();
        }
    }
}
