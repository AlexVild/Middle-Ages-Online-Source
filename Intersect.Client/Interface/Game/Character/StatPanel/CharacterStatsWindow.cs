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
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Label = Intersect.Client.Framework.Gwen.Control.Label;

namespace Intersect.Client.Interface.Game.Character.StatPanel
{
    public partial class CharacterStatsWindow : CharacterWindowPanel
    {
        public CharacterPanelType Type { get; } = CharacterPanelType.Stats;

        Label mHpCurrent { get; set; }
        Label mHpTotal { get; set; }

        Label mMpCurrent { get; set; }
        Label mMpTotal { get; set; }

        Label mEvasionBase { get; set; }
        Label mEvasionEqp { get; set; }
        Label mEvasionTotal { get; set; }

        Label mAccuracyBase { get; set; }
        Label mAccuracyEqp { get; set; }
        Label mAccuracyTotal { get; set; }

        Label mSpeedBase { get; set; }
        Label mSpeedEqp { get; set; }
        Label mSpeedTotal { get; set; }

        public CharacterStatsWindow(ImagePanel characterWindow)
        {
            mParentContainer = characterWindow;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Equipment");

            InitializeStats();

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Update()
        {
            if (Me == null || IsHidden)
            {
                return;
            }

            PopulateStats();
        }

        private void InitializeStats()
        {
            mHpCurrent = GenerateLabel("HpBase");
            mHpTotal = GenerateLabel("HpTotal");

            mMpCurrent = GenerateLabel("MpBase");
            mMpTotal = GenerateLabel("MpTotal");

            mEvasionBase = GenerateLabel("EvasionBase");
            mEvasionEqp = GenerateLabel("EvasionEqp");
            mEvasionTotal = GenerateLabel("EvasionTotal");

            mAccuracyBase = GenerateLabel("AccuracyBase");
            mAccuracyEqp = GenerateLabel("AccuracyEqp");
            mAccuracyTotal = GenerateLabel("AccuracyTotal");

            mSpeedBase = GenerateLabel("SpeedBase");
            mSpeedEqp = GenerateLabel("SpeedEqp");
            mSpeedTotal = GenerateLabel("SpeedTotal");
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

            mHpCurrent.SetText(currHp);
            mHpTotal.SetText(maxHp);
            mMpCurrent.SetText(currMp);
            mMpTotal.SetText(maxMp);

            mEvasionBase.SetText(evasionRow.Base);
            mEvasionEqp.SetText(evasionRow.Equip);
            mEvasionTotal.SetText(evasionRow.Total);

            mAccuracyBase.SetText(accuracyRow.Base);
            mAccuracyEqp.SetText(accuracyRow.Equip);
            mAccuracyTotal.SetText(accuracyRow.Total);

            mSpeedBase.SetText(speedRow.Base);
            mSpeedEqp.SetText(speedRow.Equip);
            mSpeedTotal.SetText(speedRow.Total);
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
