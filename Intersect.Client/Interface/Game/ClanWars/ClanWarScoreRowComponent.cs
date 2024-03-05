using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Utilities;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.ClanWars
{
    internal class ClanWarScoreRowComponent : GwenComponent
    {
        public Label ClanName { get; set; }
        public Label Score { get; set; }

        public ClanWarScoreRowComponent(Base parent, string containerName, ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "ClanWarScoreRow", referenceList)
        {
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            ClanName = new Label(SelfContainer, "ClanName");
            Score = new Label(SelfContainer, "Score");

            base.Initialize();
            FitParentToComponent();
        }

        public void UpdateRow(ClanWarScore data, int placement)
        {
            var resolutionWidth = int.Parse(Graphics.Renderer.GetResolutionString().Split('x')[0]);
            var resolutionSizeKey = GameContentManager.GetUiSize(resolutionWidth);

            var guildName = data.Guild;
            guildName = UiHelper.TruncateString(guildName, ClanName.Font, ClanName.Width - 16);
            
            ClanName.Text = $"{placement}) {guildName}";

            if (resolutionSizeKey == "sm")
            {
                Score.Text = $"{data.Score.ToString("N0")}";
            }
            else
            {
                Score.Text = $"{data.Score.ToString("N0")} pts.";
            }
            
            SetColor(data.Guild);
        }

        public void ClearRow()
        {
            ClanName.Text = string.Empty;
            Score.Text = string.Empty;
        }

        private void SetColor(string guildName)
        {
            Color textColor = Globals.Me?.Guild == guildName ? CustomColors.General.GeneralPrimary : CustomColors.General.GeneralMuted;
            ClanName.SetTextColor(textColor, Label.ControlState.Normal);
            Score.SetTextColor(textColor, Label.ControlState.Normal);
        }
    }
}
