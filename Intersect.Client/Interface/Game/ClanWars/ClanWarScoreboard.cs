using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Items;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects.Events;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.ClanWars
{
    public static class ClanWarScoreboardController
    {
        public static List<ClanWarMapUpdate> MapUpdates { get; set; } = new List<ClanWarMapUpdate>();

        public static void UpdateScores(ClanWarScore[] scores)
        {
            Interface.GameUi?.ClanWarScorePanel?.UpdateScores(scores.ToList());
        }
    }

    public class ClanWarScoreboard : GamePanel
    {
        private const int NUM_ROWS = 3;

        protected override string FileName => "ClanWarScoreboard";

        private ImagePanel RowContainer { get; set; }
        private ImagePanel TooltipPanel { get; set; }

        private ComponentList<GwenComponent> Rows;

        public ClanWarScoreboard(Base gameCanvas) : base(gameCanvas)
        {
        }

        protected override void PreInitialization()
        {
            RowContainer = new ImagePanel(Background, "RowContainer");
        }

        protected override void PostInitialization()
        {
            Rows = new ComponentList<GwenComponent>();
            for (var i = 0; i < NUM_ROWS; i++)
            {
                var row = new ClanWarScoreRowComponent(RowContainer, $"ClanRow_{i}", Rows);
                row.Initialize();
                row.SetPosition(0, 25 * i);
            }

            Background.SetTooltipGraphicsMAO();

            TooltipPanel = new ImagePanel(Background);
            TooltipPanel.Width = Background.Width;
            TooltipPanel.Height = Background.Height;
            TooltipPanel.SetTooltipGraphicsMAO();
            TooltipPanel.Clicked += TooltipPanel_ClickPassthrough;
        }

        /// <summary>
        /// Used so that the tooltip functions, but players will still try attacks when clicking thru the scoreboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arguments"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TooltipPanel_ClickPassthrough(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (Globals.Me != null && Controls.ControlHasKey(Control.AttackInteract, Framework.GenericClasses.Keys.LButton) && Globals.Me.TryAttack())
            {
                Globals.Me.UpdateAttackTimer();
            }
        }

        public override void UpdateShown()
        {   
        }

        public void UpdateScores(List<ClanWarScore> scores)
        {
            var idx = 0;

            var selfPlacement = scores.FindIndex(score => !string.IsNullOrEmpty(score.Guild) && score.Guild == Globals.Me?.Guild);
            var first = MathHelper.Clamp(selfPlacement - 1, 0, int.MaxValue);

            // Last-place display when there are at least 3 competitors
            if (selfPlacement == scores.Count - 1 && scores.Count >= NUM_ROWS) 
            {
                first -= 1;
            }

            var scoresToDisplay = scores.Skip(first).Take(NUM_ROWS).ToArray();

            foreach (var row in Rows.Cast<ClanWarScoreRowComponent>()) 
            {
                if (idx >= scoresToDisplay?.Length)
                {
                    continue;
                }
                row.UpdateRow(scoresToDisplay.ElementAtOrDefault(idx), first + idx + 1);
                idx++;
            }

            if (scores.Count > 0)
            {
                TooltipPanel.SetToolTipText($"Current leader: {scores[0].Guild} with {scores[0].Score} pts.");
            }
        }
    }
}
