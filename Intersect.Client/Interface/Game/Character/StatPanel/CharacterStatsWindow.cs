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

        ImagePanel HpLabelContainer { get; set; }
        ImageLabelComponent HpLabel { get; set; }

        ImagePanel MpLabelContainer { get; set; }
        ImageLabelComponent MpLabel { get; set; }

        ImagePanel EvasionLabelContainer { get; set; }
        ImageLabelComponent EvasionLabel { get; set; }

        ImagePanel AccuracyLabelContainer { get; set; }
        ImageLabelComponent AccuracyLabel { get; set; }

        ImagePanel SpeedLabelContainer { get; set; }
        ImageLabelComponent SpeedLabel { get; set; }

        ImagePanel HpContainer { get; set; }
        NumberContainerComponent mHpCurrent { get; set; }
        ImagePanel HpTotalContainer { get; set; }
        NumberContainerComponent mHpTotal { get; set; }

        ImagePanel MpContainer { get; set; }
        NumberContainerComponent mMpCurrent { get; set; }
        ImagePanel MpTotalContainer { get; set; }
        NumberContainerComponent mMpTotal { get; set; }

        ImagePanel EvasionBaseContainer { get; set; }
        NumberContainerComponent mEvasionBase { get; set; }
        ImagePanel EvasionEqpContainer { get; set; }
        NumberContainerComponent mEvasionEqp { get; set; }
        ImagePanel EvasionTotalContainer { get; set; }
        NumberContainerComponent mEvasionTotal { get; set; }

        ImagePanel AccuracyBaseContainer { get; set; }
        ImagePanel AccuracyEqpContainer { get; set; }
        ImagePanel AccuracyTotalContainer { get; set; }
        NumberContainerComponent mAccuracyBase { get; set; }
        NumberContainerComponent mAccuracyEqp { get; set; }
        NumberContainerComponent mAccuracyTotal { get; set; }

        ImagePanel SpeedBaseContainer { get; set; }
        ImagePanel SpeedEqpContainer { get; set; }
        ImagePanel SpeedTotalContainer { get; set; }
        NumberContainerComponent mSpeedBase { get; set; }
        NumberContainerComponent mSpeedEqp { get; set; }
        NumberContainerComponent mSpeedTotal { get; set; }

        public CharacterStatsWindow(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Stats");

            InitializeStatContainers();

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            InitializeStats();
        }

        public override void Update()
        {
            if (Me == null || IsHidden)
            {
                return;
            }

            PopulateStats();
        }

        private void InitializeStatContainers()
        {
            HpLabelContainer = new ImagePanel(mBackground, "HPLabel");
            HpContainer = new ImagePanel(mBackground, "HP");
            HpTotalContainer = new ImagePanel(mBackground, "HPTotal");

            MpLabelContainer = new ImagePanel(mBackground, "MPLabel");
            MpContainer = new ImagePanel(mBackground, "MP");
            MpTotalContainer = new ImagePanel(mBackground, "MPTotal");

            EvasionLabelContainer = new ImagePanel(mBackground, "EvasionLabel");
            EvasionBaseContainer = new ImagePanel(mBackground, "EvasionBase");
            EvasionEqpContainer = new ImagePanel(mBackground, "EvasionEqp");
            EvasionTotalContainer = new ImagePanel(mBackground, "EvasionTotal");

            AccuracyLabelContainer = new ImagePanel(mBackground, "AccuracyLabel");
            AccuracyBaseContainer = new ImagePanel(mBackground,  "AccuracyBase");
            AccuracyEqpContainer = new ImagePanel(mBackground,   "AccuracyEqp");
            AccuracyTotalContainer = new ImagePanel(mBackground, "AccuracyTotal");

            SpeedLabelContainer = new ImagePanel(mBackground, "SpeedLabel");
            SpeedBaseContainer = new ImagePanel(mBackground,  "SpeedBase");
            SpeedEqpContainer = new ImagePanel(mBackground,   "SpeedEqp");
            SpeedTotalContainer = new ImagePanel(mBackground, "SpeedTotal");
        }

        private void InitializeStats()
        {
            mHpCurrent = new NumberContainerComponent(HpContainer);
            mHpTotal = new NumberContainerComponent(HpTotalContainer);

            mMpCurrent = new NumberContainerComponent(MpContainer);
            mMpTotal = new NumberContainerComponent(MpTotalContainer);

            mEvasionBase = new NumberContainerComponent(EvasionBaseContainer);
            mEvasionEqp = new NumberContainerComponent(EvasionEqpContainer);
            mEvasionTotal = new NumberContainerComponent(EvasionTotalContainer);

            mAccuracyBase = new NumberContainerComponent(AccuracyBaseContainer);
            mAccuracyEqp = new NumberContainerComponent(AccuracyEqpContainer);
            mAccuracyTotal = new NumberContainerComponent(AccuracyTotalContainer);

            mSpeedBase = new NumberContainerComponent(SpeedBaseContainer);
            mSpeedEqp = new NumberContainerComponent(SpeedEqpContainer);
            mSpeedTotal = new NumberContainerComponent(SpeedTotalContainer);

            HpLabel = new ImageLabelComponent(HpLabelContainer);
            HpLabel.Initialize(LabelColor, LabelHoverColor, "character_stats_health.png", "HEALTH", "Your health pool.");
            mHpCurrent.Initialize(StatLabelColor, StatColor, "CURR", string.Empty);
            mHpTotal.Initialize(StatLabelColor, StatColor, "MAX", string.Empty);

            MpLabel = new ImageLabelComponent(MpLabelContainer);
            MpLabel.Initialize(LabelColor, LabelHoverColor, "character_stats_mana.png", "MANA", "Your mana pool.");
            mMpCurrent.Initialize(StatLabelColor, StatColor, "CURR", string.Empty);
            mMpTotal.Initialize(StatLabelColor, StatColor, "MAX", string.Empty);

            EvasionLabel = new ImageLabelComponent(EvasionLabelContainer);
            EvasionLabel.Initialize(LabelColor, LabelHoverColor, "character_stats_evasion.png", "EVASION", "Dodge chance vs. opponent's accuracy.");
            mEvasionBase.Initialize(StatLabelColor, StatColor, "BASE", string.Empty);
            mEvasionEqp.Initialize(StatLabelColor, StatColor, "EQP", string.Empty);
            mEvasionTotal.Initialize(StatLabelColor, StatColor, "TOTAL", string.Empty);

            AccuracyLabel = new ImageLabelComponent(AccuracyLabelContainer);
            AccuracyLabel.Initialize(LabelColor, LabelHoverColor, "character_stats_accuracy.png", "ACCURACY", "Hit chance vs. opponent's evasion.");
            mAccuracyBase.Initialize(StatLabelColor, StatColor, "BASE", string.Empty);
            mAccuracyEqp.Initialize(StatLabelColor, StatColor, "EQP", string.Empty);
            mAccuracyTotal.Initialize(StatLabelColor, StatColor, "TOTAL", string.Empty);

            SpeedLabel = new ImageLabelComponent(SpeedLabelContainer);
            SpeedLabel.Initialize(LabelColor, LabelHoverColor, "character_stats_speed.png", "SPEED", "Determines movement speed.");
            mSpeedBase.Initialize(StatLabelColor, StatColor, "BASE", string.Empty);
            mSpeedEqp.Initialize(StatLabelColor, StatColor, "EQP", string.Empty);
            mSpeedTotal.Initialize(StatLabelColor, StatColor, "TOTAL", string.Empty);
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
