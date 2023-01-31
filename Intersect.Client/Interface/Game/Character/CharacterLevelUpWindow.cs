using System;
using System.Collections.Generic;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character.Equipment;
using Intersect.Client.Interface.Game.Character.StatPanel;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Character
{
    public class CharacterLevelUpWindow
    {
        CharacterWindowMAO Parent { get; set; }

        Canvas GameCanvas { get; set; }

        ImagePanel Background { get; set; }
        Label PointsRemainingLabel { get; set; }
        ImagePanel StatRowContainer { get; set; }
        Dictionary<int, LevelUpRow> StatRows { get; set; } = new Dictionary<int, LevelUpRow>();
        Button ApplyButton { get; set; }
        Button CancelButton { get; set; }

        int PointsRemaining { get; set; }

        List<int> CurrentAssignments { get; set; } = new List<int>();

        public bool IsVisible => Background.IsVisible;

        public CharacterLevelUpWindow(CharacterWindowMAO parent, Canvas gameCanvas)
        {
            Parent = parent;
            GameCanvas = gameCanvas;

            Background = new ImagePanel(gameCanvas, "CharacterLevelUpWindow");
            PointsRemainingLabel = new Label(Background, "PointsRemaining");
            StatRowContainer = new ImagePanel(Background, "StatRows");

            ApplyButton = new Button(Background, "Apply Button")
            {
                Text = "APPLY"
            };
            ApplyButton.Clicked += ApplyButton_Clicked;
            
            CancelButton = new Button(Background, "Cancel Button")
            {
                Text = "CANCEL"
            };
            CancelButton.Clicked += CancelButton_Clicked;

            // Getting away with these two separate enums for now
            StatRows[(int)Vitals.Health] = new LevelUpRow(StatRowContainer, "HEALTH", Globals.Me.MaxVital[(int)Vitals.Health], 3, new Color(222, 124, 112), new Color(241, 199, 194), "Your health pool.");
            StatRows[(int)Vitals.Mana] = new LevelUpRow(StatRowContainer, "MANA", Globals.Me.MaxVital[(int)Vitals.Mana], 3, new Color(137, 135, 255), new Color(204, 204, 255), "Your mana pool.");
            StatRows[(int)Stats.Evasion] = new LevelUpRow(StatRowContainer, "EVASION", Globals.Me.Stat[(int)Stats.Evasion], 1, new Color(86, 179, 192), new Color(181, 223, 228), "Dodge chance vs. opponent's accuracy.");
            StatRows[(int)Stats.Accuracy] = new LevelUpRow(StatRowContainer, "ACCURACY", Globals.Me.Stat[(int)Stats.Accuracy], 1, new Color(99, 196, 70), new Color(188, 230, 174), "Hit chance vs. opponent's evasion.");
            StatRows[(int)Stats.Speed] = new LevelUpRow(StatRowContainer, "SPEED", Globals.Me.Stat[(int)Stats.Speed], 1, new Color(200, 145, 62), new Color(232, 208, 170), "Determines movement speed.");

            Background.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            var idx = 0;
            foreach(var row in StatRows.Values)
            {
                row.Initialize();
                row.SetPosition(0, (row.Height + 8) * idx);
                idx++;
            }

            Hide();
        }

        private void ApplyButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            // TODO this
        }

        private void CancelButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Hide();
        }

        public void Hide()
        {
            Background.Hide();
        }

        public void Show()
        {
            PointsRemaining = Globals.Me?.StatPoints ?? 0;
            if (Globals.LastLevelJinglePlayed < Timing.Global.Milliseconds && PointsRemaining > 0)
            {
                Globals.LastLevelJinglePlayed = Timing.Global.Milliseconds + 60000;
                Audio.AddGameSound("al_level_up_jingle.wav", false);
            }
            CurrentAssignments.Clear();
            Background.Show();
            Background.BringToFront();
        }

        public void Update()
        {
            if (!Background.IsVisible || Globals.Me == null)
            {
                return;
            }

            PointsRemainingLabel.SetText($"{PointsRemaining}");
            foreach (var row in StatRows)
            {
                var stat = row.Key;

                // Disgusting, lol
                if (stat <= (int)Vitals.Mana)
                {
                    row.Value.SetStatValue(Globals.Me.MaxVital[stat]);
                }
                else
                {
                    row.Value.SetStatValue(Globals.Me.TrueStats[stat]);
                }

                row.Value.SetChanges(Globals.Me.StatPoints > 0, CurrentAssignments.Contains(stat));
            }
        }
    }

    class LevelUpRow
    {
        public ImagePanel Background { get; set; }

        public int X => Background.X;

        public int Y => Background.Y;

        public int Height => Background.Height;

        public Label StatNameLabel { get; set; }
        public Label StatValueLabel { get; set; }

        public Button DecreasePointButton { get; set; }
        public Label IncreaseValueLabel { get; set; }
        public Button IncreasePointButton { get; set; }

        public string StatName { get; set; }
        public int StatValue { get; set; }
        public int IncreaseValue { get; set; }
        public Color StatColor { get; set; }
        public Color StatHoverColor { get; set; }
        public string Tooltip { get; set; }

        private Base Parent { get; set; }

        public LevelUpRow(Base parent, string statName, int statValue, int increaseValue, Color statColor, Color statHoverColor, string tooltip)
        {
            Parent = parent;
            StatName = statName;
            StatValue = statValue;
            IncreaseValue = increaseValue;
            StatColor = statColor;
            StatHoverColor = statHoverColor;
            Tooltip = tooltip;
        }

        public void Initialize()
        {
            Background = new ImagePanel(Parent, "CharacterLevelUpRowComponent");

            StatNameLabel = new Label(Background, "StatName")
            {
                Text = StatName,
                MouseInputEnabled = true
            };

            StatValueLabel = new Label(Background, "StatValue")
            {
                Text = StatValue.ToString()
            };

            IncreasePointButton = new Button(Background, "IncreaseButton");
            IncreasePointButton.Clicked += IncreasePointButton_Clicked;

            DecreasePointButton= new Button(Background, "DecreaseButton");
            DecreasePointButton.Clicked += DecreasePointButton_Clicked;

            IncreaseValueLabel = new Label(Background, "IncreaseValue")
            {
                Text = $"+{IncreaseValue}"
            };

            Background.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            StatNameLabel.SetTextColor(StatColor, Label.ControlState.Normal);
            StatNameLabel.SetTextColor(StatHoverColor, Label.ControlState.Hovered);
            StatNameLabel.SetToolTipText(Tooltip);
        }

        private void DecreasePointButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            throw new NotImplementedException();
        }

        private void IncreasePointButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            throw new NotImplementedException();
        }

        public void SetStatValue(int value)
        {
            StatValueLabel.Text = value.ToString();

            IncreasePointButton.Enable();
            DecreasePointButton.Enable();
            StatValueLabel.SetTextColor(new Color(232, 208, 170), Label.ControlState.Normal);
            if (value >= Options.Player.MaxStat)
            {
                StatValueLabel.SetTextColor(new Color(200, 145, 62), Label.ControlState.Normal);
                IncreasePointButton.Disable();
            }
            else if (value <= 0)
            {
                DecreasePointButton.Disable();
            }
        }

        public void SetPosition(int x, int y)
        {
            Background.SetPosition(x, y);
        }

        public void SetChanges(bool increase, bool decrease)
        {
            IncreasePointButton.IsHidden = !increase;
            DecreasePointButton.IsHidden = !decrease;
        }
    }
}
