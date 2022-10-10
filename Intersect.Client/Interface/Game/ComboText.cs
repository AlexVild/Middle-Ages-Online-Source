using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intersect.Client.Localization;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Core;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game
{
    class ComboText
    {
        private Canvas mCanvas { get; set; }

        private string mSize { get; set; }

        private int mFlashCounter { get; set; }

        private Color mDrawColor { get; set; }

        private Color mExpDrawColor { get; set; }

        private bool mIsFlashing { get; set; }

        private long MaxComboWindow => Globals.Me?.MaxComboWindow ?? -1;
        
        private long LastComboUpdate => Globals.Me?.LastComboUpdate ?? -1;

        private Label mComboLabel;

        private Label mExpLabel;

        private ImagePanel mComboContainer;

        private int mAlpha = 0;

        public ComboText(Canvas canvas)
        {
            mCanvas = canvas;
            mComboContainer = new ImagePanel(canvas, "ComboContainer");
            mComboLabel = new Label(mComboContainer, "ComboText");
            mComboLabel.Hide();
            mExpLabel = new Label(mComboContainer, "ExpText");
            mExpLabel.Hide();

            mComboContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Update()
        {
            // if there's no combo, don't bother drawing
            if (Globals.Me.CurrentCombo <= 1)
            {
                mComboLabel.Hide();
                mExpLabel.Hide();
                mFlashCounter = 0;
                return;
            }
            else
            {
                mComboLabel.Show();
                if (Globals.Me.ComboExp > 0)
                {
                    mExpLabel.Show();
                }
            }

            mFlashCounter++;
            // else, update members
            mSize = Globals.Me.CurrentCombo.ToString();

            mAlpha = mGenerateTextAlpha();

            if (mFlashCounter % 100 == 0) // flash every 100 ticks-ish?
            {
                mIsFlashing = !mIsFlashing;
                if (mIsFlashing)
                {
                    mDrawColor = Color.White;
                }
                else
                {
                    mDrawColor = Color.Yellow;
                }
                mDrawColor = new Color(mAlpha, mDrawColor.R, mDrawColor.G, mDrawColor.B);
            }

            mExpDrawColor = Color.Cyan;
            mExpDrawColor = new Color(mAlpha, mExpDrawColor.R, mExpDrawColor.G, mExpDrawColor.B);

            var message = mSize + "x " + Strings.Combat.combo + "!";
            mComboLabel.SetText(message);
            mComboLabel.SetTextColor(mDrawColor, Label.ControlState.Normal);

            var expMessage = "+" + Globals.Me.ComboExp.ToString() + " Bonus " + Strings.Combat.exp;
            mExpLabel.SetText(expMessage);
            mExpLabel.SetTextColor(mExpDrawColor, Label.ControlState.Normal);
        }

        private int mGenerateTextAlpha()
        {
            var diff = (LastComboUpdate + MaxComboWindow) - Timing.Global.Milliseconds;
            if (diff <= 0)
            {
                return 0;
            }

            diff = MathHelper.Clamp(diff, 0, MaxComboWindow);

            var amount = ((double)diff / MaxComboWindow) * 255;
            return (int)amount;
        }
    }
}
