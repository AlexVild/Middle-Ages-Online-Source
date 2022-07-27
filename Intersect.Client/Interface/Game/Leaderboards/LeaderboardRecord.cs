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
    public class LeaderboardRecord : IDisposable
    {
        public Base Container { get; set; }
        private Record CurrentRecord { get; set; }

        private ImagePanel RecordBackground { get; set; }
        private Label RecordIndex { get; set; }
        private Label RecordValue { get; set; }
        private Label RecordHolder { get; set; }

        public LeaderboardRecord(Base container, Record record)
        {
            Container = container;
            CurrentRecord = record;
            
            RecordBackground = new ImagePanel(Container, "LeaderboardRecord");

            RecordIndex = new Label(RecordBackground, "RecordIndex")
            {
                Text = record.Index.ToString()
            };

            RecordValue = new Label(RecordBackground, "RecordValue")
            {
                Text = record.Value
            };

            RecordHolder = new Label(RecordBackground, "RecordHolder")
            {
                Text = record.Holder
            };
        }

        public void Initialize()
        {
            RecordBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Dispose()
        {
            RecordBackground.Dispose();
        }
    }
}
