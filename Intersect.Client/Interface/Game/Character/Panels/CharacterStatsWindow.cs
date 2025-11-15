using System;
using System.Collections.Generic;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Equipment;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Label = Intersect.Client.Framework.Gwen.Control.Label;

namespace Intersect.Client.Interface.Game.Character.StatPanel
{
    public partial class CharacterStatsWindow : CharacterWindowPanel
    {
        private Color LabelColor => new Color(255, 50, 19, 0);
        private Color LabelHoverColor => new Color(255, 111, 63, 0);
        private Color StatLabelColor => new Color(255, 166, 167, 37);
        private Color StatColor => new Color(255, 255, 255, 255);

        public CharacterPanelType Type { get; } = CharacterPanelType.Stats;

        private Button LevelUpButton { get; set; }

        NumberContainerComponent mHpCurrent { get; set; }
        NumberContainerComponent mHpTotal { get; set; }

        NumberContainerComponent mMpCurrent { get; set; }
        NumberContainerComponent mMpTotal { get; set; }

        NumberContainerComponent mEvasionBase { get; set; }
        NumberContainerComponent mEvasionEqp { get; set; }
        NumberContainerComponent mEvasionTotal { get; set; }

        NumberContainerComponent mAccuracyBase { get; set; }
        NumberContainerComponent mAccuracyEqp { get; set; }
        NumberContainerComponent mAccuracyTotal { get; set; }

        NumberContainerComponent mSpeedBase { get; set; }
        NumberContainerComponent mSpeedEqp { get; set; }
        NumberContainerComponent mSpeedTotal { get; set; }

        private ComponentList<IGwenComponent> ContainerComponents { get; set; }
        private ComponentList<IGwenComponent> ImageLabelComponents { get; set; }

        private CharacterWindow CharacterWindow;

        public CharacterStatsWindow(ImagePanel panelBackground, CharacterWindow characterWindow)
        {
            CharacterWindow = characterWindow;
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Stats");
            LevelUpButton = new Button(mBackground, "LevelUpButton") 
            {
                Text = "LEVEL UP!"
            };
            LevelUpButton.Clicked += LevelUpButton_Clicked;

            InitializeStatContainers();

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            InitializeStats();
        }

        private void LevelUpButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            CharacterWindow?.LevelUpWindow?.Show();
        }

        public override void Update()
        {
            if (Me == null || IsHidden)
            {
                return;
            }

            LevelUpButton.IsHidden = Globals.Me.StatPoints == 0;
            PopulateStats();
        }

        private void InitializeStatContainers()
        {
            ContainerComponents = new ComponentList<IGwenComponent>();
            ImageLabelComponents = new ComponentList<IGwenComponent>();
            _ = new ImageLabelComponent(mBackground, "HPLabel", LabelColor, LabelHoverColor, "character_stats_health.png", "HEALTH", "Your health pool.", ImageLabelComponents);
            mHpCurrent = new NumberContainerComponent(mBackground, "HP", StatLabelColor, StatColor, "CURR", string.Empty, ContainerComponents);
            mHpTotal = new NumberContainerComponent(mBackground, "HPTotal", StatLabelColor, StatColor, "MAX", string.Empty, ContainerComponents);

            _ = new ImageLabelComponent(mBackground, "MPLabel", LabelColor, LabelHoverColor, "character_stats_mana.png", "MANA", "Your mana pool.", ImageLabelComponents);
            mMpCurrent = new NumberContainerComponent(mBackground, "MP", StatLabelColor, StatColor, "CURR", string.Empty, ContainerComponents);
            mMpTotal = new NumberContainerComponent(mBackground, "MPTotal", StatLabelColor, StatColor, "MAX", string.Empty, ContainerComponents);

            _ = new ImageLabelComponent(mBackground, "EvasionLabel", LabelColor, LabelHoverColor, "character_stats_evasion.png", "EVASION", $"Dodge chance vs. opponent's accuracy. Increases status resist by 1% every {Options.Instance.CombatOpts.EvasionStatusResistEveryMod} pts.", ImageLabelComponents);
            mEvasionBase = new NumberContainerComponent(mBackground, "EvasionBase", StatLabelColor, StatColor, "BASE", string.Empty, ContainerComponents);
            mEvasionEqp = new NumberContainerComponent(mBackground, "EvasionEqp", StatLabelColor, StatColor, "BNS", string.Empty, ContainerComponents);
            mEvasionTotal = new NumberContainerComponent(mBackground, "EvasionTotal", StatLabelColor, StatColor, "TOTAL", string.Empty, ContainerComponents);

            _ = new ImageLabelComponent(mBackground, "AccuracyLabel", LabelColor, LabelHoverColor, "character_stats_accuracy.png", "ACCURACY", $"Hit chance vs. opponent's evasion. Increases crit chance by 1% every {Options.Instance.CombatOpts.AccuracyCritEveryDivisor} pts.", ImageLabelComponents);
            mAccuracyBase = new NumberContainerComponent(mBackground, "AccuracyBase", StatLabelColor, StatColor, "BASE", string.Empty, ContainerComponents);
            mAccuracyEqp = new NumberContainerComponent(mBackground, "AccuracyEqp", StatLabelColor, StatColor, "BNS", string.Empty, ContainerComponents);
            mAccuracyTotal = new NumberContainerComponent(mBackground, "AccuracyTotal", StatLabelColor, StatColor, "TOTAL", string.Empty, ContainerComponents);

            _ = new ImageLabelComponent(mBackground, "SpeedLabel", LabelColor, LabelHoverColor, "character_stats_speed.png", "SPEED", "Determines movement speed. Decreases health & mana regen times.", ImageLabelComponents);
            mSpeedBase = new NumberContainerComponent(mBackground, "SpeedBase", StatLabelColor, StatColor, "BASE", string.Empty, ContainerComponents);
            mSpeedEqp = new NumberContainerComponent(mBackground, "SpeedEqp", StatLabelColor, StatColor, "BNS", string.Empty, ContainerComponents);
            mSpeedTotal = new NumberContainerComponent(mBackground, "SpeedTotal", StatLabelColor, StatColor, "TOTAL", string.Empty, ContainerComponents);
        }

        private void InitializeStats()
        {
            ImageLabelComponents.InitializeAll();
            ContainerComponents.InitializeAll();
        }

        private void PopulateStats()
        {
            var currHp = Me.Vital[(int)Vitals.Health].ToString();
            var currMp = Me.Vital[(int)Vitals.Mana].ToString();
            var maxHp = Me.MaxVital[(int)Vitals.Health].ToString();
            var maxMp = Me.MaxVital[(int)Vitals.Mana].ToString();

            var evasionRow = new StatRow(Me, Stats.Evasion);
            var accuracyRow = new StatRow(Me, Stats.Accuracy);
            var speedRow = new StatRow(Me, Stats.Speed);

            mHpCurrent.SetValue(currHp);
            mHpTotal.SetValue(maxHp);
            mMpCurrent.SetValue(currMp);
            mMpTotal.SetValue(maxMp);

            mEvasionBase.SetValue(evasionRow.Base);
            mEvasionEqp.SetValue(evasionRow.Equip);
            mEvasionTotal.SetValue(evasionRow.Total);

            mAccuracyBase.SetValue(accuracyRow.Base);
            mAccuracyEqp.SetValue(accuracyRow.Equip);
            mAccuracyTotal.SetValue(accuracyRow.Total);

            mSpeedBase.SetValue(speedRow.Base);
            mSpeedEqp.SetValue(speedRow.Equip);
            mSpeedTotal.SetValue(speedRow.Total);
        }

        struct StatRow
        {
            public StatRow(Player player, Stats stat)
            {
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player));
                }

                var statDiff = player.Stat[(int)stat] - player.TrueStats[(int)stat];
                Base = player.TrueStats[(int)stat].ToString();
                Equip = statDiff.ToString();
                Total = player.Stat[(int)stat].ToString();
            }

            public string Base { get; set; }
            public string Equip { get; set; }
            public string Total { get; set; }
        }
    }
}
