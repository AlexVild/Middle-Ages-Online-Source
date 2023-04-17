using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General.Leaderboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Leaderboards
{
    public class LeaderboardRecord : Base
    {
        public Base Container { get; set; }
        private Record CurrentRecord { get; set; }

        public ImagePanel RecordBackground { get; set; }
        private Label RecordIndex { get; set; }
        private Label RecordValue { get; set; }
        private Label RecordHolderTemplate { get; set; }
        private RichLabel RecordHolder { get; set; }

        public LeaderboardRecord(Base container, Record record)
        {
            Container = container;
            CurrentRecord = record;
            
            RecordBackground = new ImagePanel(Container, "LeaderboardRecord");

            RecordIndex = new Label(RecordBackground, "RecordIndex");

            RecordValue = new Label(RecordBackground, "RecordValue");

            RecordHolderTemplate = new Label(RecordBackground, "RecordHolder");
            RecordHolder = new RichLabel(RecordBackground);
        }

        public void Initialize(Record record)
        {
            RecordBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            SetRecord(record);
            ResizeBackground();
        }

        public void Dispose()
        {
            RecordBackground.Dispose();
        }

        public void SetRecord(Record record)
        {
            CurrentRecord = record;
            RecordIndex.Text = (record.Index + 1).ToString();
            RecordValue.Text = record.Value;
            RecordHolder.SetText(record.Holder, RecordHolderTemplate, 196);
            RecordHolder.Y += RecordHolderTemplate.Padding.Top;
        }

        /// <summary>
        /// Resizes the background based on how many lines the holder label has generated
        /// </summary>
        public void ResizeBackground()
        {
            var lines = RecordHolder.Labels.Count;
            if (lines > 1)
            {
                for (var i = 1; i < lines; i++)
                {
                    var line = RecordHolder.Labels[i];

                    var lineHeight = line.TextHeight;
                    RecordBackground.Height += lineHeight;
                }
            }
        }
    }
}
