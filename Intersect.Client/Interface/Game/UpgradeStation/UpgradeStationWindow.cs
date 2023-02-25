using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.UpgradeStation
{
    public class UpgradeStationWindow : GameWindow
    {
        public UpgradeStationWindow(Base gameCanvas) : base(gameCanvas)
        {
        }

        protected override string FileName => "UpgradeStationWindow";

        protected override string Title => "UPGRADE STATION";

        public override void UpdateShown()
        {
            //
        }

        protected override void PostInitialization()
        {
            //
        }

        protected override void PreInitialization()
        {
            //
        }

        protected override void Close()
        {
            Globals.Me?.UpgradeStation?.Close();
            PacketSender.SendCloseUpgradeStation();
            base.Close();
        }

        public void ForceClose()
        {
            Close();
        }
    }
}
