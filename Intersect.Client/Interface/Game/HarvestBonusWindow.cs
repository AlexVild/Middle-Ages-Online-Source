using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using System.Linq;

namespace Intersect.Client.Interface.Game
{
    class HarvestBonusWindow
    {
        private Canvas mCanvas { get; set; }

        private string mSize { get; set; }

        private int ComboWindow { get; set; }

        private int MaxComboWindow { get; set; }

        private Label mBonusLabel;

        private Label mToGoLabel;

        private ImagePanel mBonusWindow;

        private int mAlpha = 0;

        public HarvestBonusWindow(Canvas canvas)
        {
            mCanvas = canvas;
            mBonusWindow = new ImagePanel(canvas, "HarvestBonusWindow");
            mBonusLabel = new Label(mBonusWindow, "BonusText");
            mToGoLabel = new Label(mBonusWindow, "RemainderText");
            mBonusLabel.Hide();
            mToGoLabel.Hide();

            mBonusWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Update()
        {
            // if there's no combo, don't bother drawing
            if (!Globals.Me.ResourceLocked)
            {
                mBonusLabel.Hide();
                mToGoLabel.Hide();

                return;
            }
            else
            {
                mBonusLabel.Show();
                if (Globals.Me.CurrentHarvestBonus != Options.Instance.CombatOpts.HarvestBonuses.Last())
                {
                    mToGoLabel.Show();
                }
                else
                {
                    mToGoLabel.Hide();
                }
            }

            // else, update members
            ComboWindow = Globals.Me.ComboWindow;
            MaxComboWindow = Globals.Me.MaxComboWindow;
            mSize = Globals.Me.CurrentCombo.ToString();

            var bonusReadable = (int) (Globals.Me.CurrentHarvestBonus * 100);

            mBonusLabel.SetText(Strings.HarvestBonus.Bonus.ToString(bonusReadable));
            mBonusLabel.SetTextColor(CustomColors.HarvestBonus.HarvestBonusColor, Label.ControlState.Normal);

            mToGoLabel.SetText(Strings.HarvestBonus.Remaining.ToString(Globals.Me.HarvestsRemaining));
            mToGoLabel.SetTextColor(CustomColors.HarvestBonus.HarvestBonusRemainderColor, Label.ControlState.Normal);
        }
    }
}
