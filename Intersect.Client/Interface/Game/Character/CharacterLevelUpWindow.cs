using System;
using System.Collections.Generic;
using System.Linq;
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
using Intersect.GameObjects.Events;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Character
{
    public class CharacterLevelUpWindow
    {
        CharacterWindow Parent { get; set; }

        Canvas GameCanvas { get; set; }

        ImagePanel Background { get; set; }
        Label PointsRemainingLabel { get; set; }
        ImagePanel StatRowContainer { get; set; }
        Dictionary<LevelUpAssignments, LevelUpRow> StatRows { get; set; } = new Dictionary<LevelUpAssignments, LevelUpRow>();
        Button ApplyButton { get; set; }
        Button CancelButton { get; set; }
        Button ResetButton { get; set; }

        public Dictionary<LevelUpAssignments, int> Assignments { get; set; } = new Dictionary<LevelUpAssignments, int>();
        public Dictionary<LevelUpAssignments, int> StatValues { get; set; } = new Dictionary<LevelUpAssignments, int>();

        public int PointsRemaining => MathHelper.Clamp((Globals.Me?.StatPoints ?? 0) - (Assignments?.Values.Sum() ?? 0), 0, byte.MaxValue);

        public bool IsVisible => Background.IsVisible;

        public CharacterLevelUpWindow(CharacterWindow parent, Canvas gameCanvas)
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

            ResetButton = new Button(Background, "ResetButton")
            {
                Text = "RESET"
            };
            ResetButton.Clicked += ResetButton_Clicked;

            RefreshAssignments();
            StatRows[LevelUpAssignments.Health] = new LevelUpRow(StatRowContainer, "HEALTH", StatValues[LevelUpAssignments.Health], Options.Instance.PlayerOpts.BaseVitalPointIncrease, new Color(222, 124, 112), new Color(241, 199, 194), "Your health pool.", this, LevelUpAssignments.Health);
            StatRows[LevelUpAssignments.Mana] = new LevelUpRow(StatRowContainer, "MANA", Globals.Me.MaxVital[(int)Vitals.Mana], Options.Instance.PlayerOpts.BaseVitalPointIncrease, new Color(137, 135, 255), new Color(204, 204, 255), "Your mana pool.", this, LevelUpAssignments.Mana);
            StatRows[LevelUpAssignments.Evasion] = new LevelUpRow(StatRowContainer, "EVASION", Globals.Me.Stat[(int)Stats.Evasion], Options.Instance.PlayerOpts.BaseStatSkillIncrease, new Color(86, 179, 192), new Color(181, 223, 228), "Miss chance vs. opponent's accuracy.", this, LevelUpAssignments.Evasion);
            StatRows[LevelUpAssignments.Accuracy] = new LevelUpRow(StatRowContainer, "ACCURACY", Globals.Me.Stat[(int)Stats.Accuracy], Options.Instance.PlayerOpts.BaseStatSkillIncrease, new Color(99, 196, 70), new Color(188, 230, 174), "Hit chance vs. opponent's evasion.", this, LevelUpAssignments.Accuracy);
            StatRows[LevelUpAssignments.Speed] = new LevelUpRow(StatRowContainer, "SPEED", Globals.Me.Stat[(int)Stats.Speed], Options.Instance.PlayerOpts.BaseStatSkillIncrease, new Color(200, 145, 62), new Color(232, 208, 170), "Determines movement speed.", this, LevelUpAssignments.Speed);

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

        private void ResetButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            RefreshAssignments();
        }

        public void RefreshAssignments()
        {
            foreach (LevelUpAssignments val in Enum.GetValues(typeof(LevelUpAssignments)))
            {
                Assignments[val] = 0;
                
                switch (val)
                {
                    case LevelUpAssignments.Health:
                        StatValues[val] = Globals.Me.MaxVital[(int)Vitals.Health];
                        break;
                    case LevelUpAssignments.Mana:
                        StatValues[val] = Globals.Me.MaxVital[(int)Vitals.Mana];
                        break;
                    case LevelUpAssignments.Evasion:
                        StatValues[val] = Globals.Me.TrueStats[(int)Stats.Evasion];
                        break;
                    case LevelUpAssignments.Accuracy:
                        StatValues[val] = Globals.Me.TrueStats[(int)Stats.Accuracy];
                        break;
                    case LevelUpAssignments.Speed:
                        StatValues[val] = Globals.Me.TrueStats[(int)Stats.Speed];
                        break;
                }
            }
        }

        private void ApplyButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            PacketSender.SendUpdgradeStatsPacket(Assignments);
            Hide();
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
            RefreshAssignments();
            if (Globals.LastLevelJinglePlayed < Timing.Global.Milliseconds && PointsRemaining > 0)
            {
                Globals.LastLevelJinglePlayed = Timing.Global.Milliseconds + 60000;
                Audio.AddGameSound("al_level_up_jingle.wav", false);
            }
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

                row.Value.Update(StatValues[stat]);
            }

            ResetButton.IsHidden = PointsRemaining >= Globals.Me.StatPoints;
            ApplyButton.IsDisabled = PointsRemaining >= Globals.Me.StatPoints;
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

        public CharacterLevelUpWindow LevelUpWindow;

        LevelUpAssignments Stat { get; set; }

        public bool IsVitalStat => Stat == LevelUpAssignments.Health || Stat == LevelUpAssignments.Mana;
        public int MaxStat => IsVitalStat ? Options.Instance.PlayerOpts.MaxVital : Options.Player.MaxStat;

        public LevelUpRow(Base parent, string statName, int statValue, int increaseValue, Color statColor, Color statHoverColor, string tooltip, CharacterLevelUpWindow levelUpWindow, LevelUpAssignments stat)
        {
            Parent = parent;
            StatName = statName;
            StatValue = statValue;
            IncreaseValue = increaseValue;
            StatColor = statColor;
            StatHoverColor = statHoverColor;
            Tooltip = tooltip;
            LevelUpWindow = levelUpWindow;
            Stat = stat;
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
                Text = $"{IncreaseValue}"
            };

            Background.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            StatNameLabel.SetTextColor(StatColor, Label.ControlState.Normal);
            StatNameLabel.SetTextColor(StatHoverColor, Label.ControlState.Hovered);
            StatNameLabel.SetToolTipText(Tooltip);
        }

        private void DecreasePointButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (LevelUpWindow.Assignments[Stat] <= 0)
            {
                return;
            }
            LevelUpWindow.Assignments[Stat]--;
            LevelUpWindow.StatValues[Stat] = MathHelper.Clamp(LevelUpWindow.StatValues[Stat] - IncreaseValue, 0, MaxStat);
        }

        private void IncreasePointButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (LevelUpWindow.Assignments[Stat] >= Globals.Me.StatPoints)
            {
                return;
            }
            LevelUpWindow.Assignments[Stat]++;
            LevelUpWindow.StatValues[Stat] = MathHelper.Clamp(LevelUpWindow.StatValues[Stat] + IncreaseValue, 0, MaxStat);
        }

        public void Update(int statValue)
        {
            IncreasePointButton.Enable();
            DecreasePointButton.Enable();
            StatValueLabel.SetTextColor(new Color(232, 208, 170), Label.ControlState.Normal);
            if (!IsVitalStat && statValue >= MaxStat)
            {
                StatValueLabel.SetTextColor(new Color(200, 145, 62), Label.ControlState.Normal);
                IncreasePointButton.Disable();
            }
            else if (statValue <= 0)
            {
                DecreasePointButton.Disable();
            }

            if (LevelUpWindow.Assignments[Stat] <= 0)
            {
                StatValueLabel.Text = statValue.ToString();
            }
            else
            {
                StatValueLabel.SetText($"{statValue} (+{LevelUpWindow.Assignments[Stat] * IncreaseValue})");
            }

            SetChangeButtonAvailability(LevelUpWindow.PointsRemaining > 0, LevelUpWindow.Assignments[Stat] > 0);
        }

        public void SetPosition(int x, int y)
        {
            Background.SetPosition(x, y);
        }

        void SetChangeButtonAvailability(bool increase, bool decrease)
        {
            IncreasePointButton.IsHidden = !increase;
            DecreasePointButton.IsHidden = !decrease;
        }
    }
}
