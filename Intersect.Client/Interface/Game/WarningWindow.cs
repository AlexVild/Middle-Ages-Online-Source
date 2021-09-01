using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Core;
using Intersect.Configuration;

namespace Intersect.Client.Interface.Game
{
    class WarningWindow
    {
        private const long FLASH_RATE = 250; // ms

        private const int MAX_ALPHA = 255;
        
        private ImagePanel mMPWarningContainer;

        private Label mMPWarningLabel;

        private int mMPWarningAlpha = 0;

        private long mMPAlphaCounter = 0;

        private long mMPFlashCounter = 0;

        private bool mIsFlashed = false;

        private Color mMPWarningColor = Color.White;

        public WarningWindow(Canvas canvas)
        {
            mMPWarningContainer = new ImagePanel(canvas, "WarningContainer");
            mMPWarningLabel = new Label(mMPWarningContainer, "MPWarningLabel");

            mMPWarningLabel.Text = Strings.Combat.notenoughmp;

            mMPWarningContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Update()
        {
            var time = Globals.System.GetTimeMs();
            if (Globals.Me.MPWarning)
            {
                Globals.Me.MPWarning = false; // the warning is always in a state of fading out. Each new packet just refreshes the fade
                ResetMPWarningState(time);
            }

            DetermineMPWarningDisplay(time);
        }

        private void DisplayMPWarning()
        {
            mMPWarningLabel.Show();
            Audio.AddGameSound(ClientConfiguration.GUI_CANCEL_SFX, false);
        }

        private void ResetMPWarningState(long time)
        {
            if (mMPWarningLabel.IsHidden)
            {
                DisplayMPWarning();
            }
            mMPWarningAlpha = MAX_ALPHA;
            mMPAlphaCounter = time + Options.Combat.MPWarningDisplayTime;
            mMPFlashCounter = time + FLASH_RATE;
            mIsFlashed = false;
            mMPWarningColor = Color.White;
        }
        
        private void DetermineMPWarningDisplay(long time)
        {
            if (!mMPWarningLabel.IsHidden) // if the MP warning is currently visible
            {
                // start detracting from its alpha
                float timeDiff = (float)Utilities.MathHelper.Clamp(mMPAlphaCounter - time, 0, mMPAlphaCounter) / Options.Combat.MPWarningDisplayTime;
                mMPWarningAlpha = (int)(timeDiff * MAX_ALPHA);

                if (mMPFlashCounter < time)
                {
                    mMPFlashCounter = time + FLASH_RATE;
                    mIsFlashed = !mIsFlashed;
                    if (mIsFlashed)
                    {
                        mMPWarningColor = Color.Blue;
                    }
                    else
                    {
                        mMPWarningColor = Color.White;
                    }
                }
                mMPWarningLabel.SetTextColor(new Color(mMPWarningAlpha, mMPWarningColor.R, mMPWarningColor.G, mMPWarningColor.B), Label.ControlState.Normal);
            }

            if (mMPWarningAlpha <= 0)
            {
                mMPWarningLabel.Hide();
            }
        }
    }
}
