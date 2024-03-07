using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.ScreenAnimations;
using Intersect.Extensions;

namespace Intersect.Client.Interface.Game.ClanWars
{
    public class ClanWarCompletionWindow : GamePanel
    {
        public ClanWarCompleteAnimation Animation { get; set; }

        public bool Opening = false;

        private ImagePanel mPayoutContainer { get; set; }
        private Label mPlacementLabel { get; set; }
        private Label mPayoutLabel { get; set; }

        private Button ConfirmButton { get; set; }

        protected override string FileName => "ClanWarCompletion";

        public ClanWarCompletionWindow(Base gameCanvas) : base(gameCanvas)
        {

        }

        public override void Show()
        {
            if (Animation.Done)
            {
                Animation.ResetAnimation();
            }
            Opening = true;
        }

        public override void UpdateHidden()
        {
            if (Opening) 
            {
                Animation.Draw();
            }
            base.UpdateHidden();
        }

        public override void UpdateShown()
        {
        }

        public void Show(int placement, int payout)
        {
            mPlacementLabel.Text = $"{placement.ToOrdinal()} place";
            mPayoutLabel.Text = $"{payout} Valor Coin(s)";

            Show();
        }

        protected override void PostInitialization()
        {
            ConfirmButton.SetText("Okay");
            ConfirmButton.StyleMAO();
            ConfirmButton.Clicked += ConfirmButton_Clicked;

            mPayoutLabel.SetToolTipText("Valor Coins are automatically delivered to your clan's Clan Bank.");
            mPayoutLabel.SetTooltipGraphicsMAO();
        }

        private void ConfirmButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Hide();
        }

        protected override void PreInitialization()
        {
            Animation = new ClanWarCompleteAnimation(() =>
            {
                Opening = false;
                Flash.FlashScreen(1000, new Color(255, 201, 226, 158), 150);
                Background.Show();
            });

            mPayoutContainer = new ImagePanel(Background, "PayoutContainer");
            mPlacementLabel = new Label(mPayoutContainer, "Placement");
            mPayoutLabel = new Label(mPayoutContainer, "Payout");

            ConfirmButton = new Button(Background, "ConfirmButton");
        }
    }
}
