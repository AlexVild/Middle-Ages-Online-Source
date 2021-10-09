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
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game
{
    class WarningWindow
    {
        private const int MAX_ALPHA = 255;
        
        private ImagePanel mMPWarningContainer;
        private Label mMPWarningLabel;
        private int mMPWarningAlpha = 0;
        private long mMPAlphaCounter = 0;
        private long mMPFlashCounter = 0;
        private Color mMPWarningColor = Color.White;
        private bool mMPIsFlashed = false;

        private ImagePanel mHPWarningContainer;
        private Label mHPWarningLabel;
        private int mHPWarningAlpha = 0;
        private long mHPAlphaCounter = 0;
        private long mHPFlashCounter = 0;
        private Color mHPWarningColor = Color.White;
        private bool mHPIsFlashed = false;
        private bool fadeHPWarning = false;

        public WarningWindow(Canvas canvas)
        {
            mMPWarningContainer = new ImagePanel(canvas, "WarningContainer");
            mMPWarningLabel = new Label(mMPWarningContainer, "MPWarningLabel");

            mHPWarningContainer = new ImagePanel(canvas, "HPWarningContainer");
            mHPWarningLabel = new Label(mHPWarningContainer, "HPWarningLabel");

            mMPWarningLabel.Text = Strings.Combat.notenoughmp;
            mHPWarningLabel.Text = Strings.Combat.lowhealth;

            mMPWarningLabel.Hide(); // Hide warnings on init
            if (Globals.Me.HPWarning) // show if warning active on init
            {
                mHPWarningLabel.Show();
            } else
            {
                mHPWarningLabel.Hide();
            }

            mMPWarningContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            mHPWarningContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Update()
        {
            var time = Timing.Global.Milliseconds;
            if (Globals.Me.MPWarning)
            {
                Globals.Me.MPWarning = false; // the warning is always in a state of fading out. Each new packet just refreshes the fade
                ResetMPWarningState(time);
            }
            FadeoutMPWarning(time);

            if (Globals.Me.HPWarning && mHPWarningLabel.IsHidden)
            {
                ResetHPWarningState(time); // first time, display
            } else if (!Globals.Me.HPWarning && mHPWarningLabel.IsVisible)
            {
                FadeoutHPWarning(time); // warning over, start fade
            } else if (Globals.Me.HPWarning && mHPWarningLabel.IsVisible)
            {
                DisplayHPWarning(time); // Display and flash the HP warning - warning is active
            }
        }

        private void DisplayWarningWithSound(ref Label labelToDisplay, string soundPath)
        {
            labelToDisplay.Show();
            if (soundPath != null)
            {
                Audio.AddGameSound(soundPath, false);
            }
        }

        private void ResetMPWarningState(long time)
        {
            if (mMPWarningLabel.IsHidden)
            {
                DisplayWarningWithSound(ref mMPWarningLabel, ClientConfiguration.GUI_CANCEL_SFX);
            }
            mMPWarningAlpha = MAX_ALPHA;
            mMPAlphaCounter = time + Options.Combat.MPWarningDisplayTime;
            mMPFlashCounter = time + Options.Combat.WarningFlashRate;
            mMPIsFlashed = false;
            mMPWarningColor = Color.White;
        }

        private void ResetHPWarningState(long time)
        {
            DisplayWarningWithSound(ref mHPWarningLabel, ClientConfiguration.HEALTH_WARNING_SFX);
            mHPWarningAlpha = MAX_ALPHA;
            fadeHPWarning = false;
        }

        private void FadeoutMPWarning(long time)
        {
            if (mMPWarningLabel.IsVisible)
            {
                // start detracting from its alpha
                float timeDiff = (float)Utilities.MathHelper.Clamp(mMPAlphaCounter - time, 0, mMPAlphaCounter) / Options.Combat.MPWarningDisplayTime;
                mMPWarningAlpha = (int)(timeDiff * MAX_ALPHA);

                if (mMPFlashCounter < time)
                {
                    mMPFlashCounter = time + Options.Combat.WarningFlashRate;
                    mMPIsFlashed = !mMPIsFlashed;
                    if (mMPIsFlashed)
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

        private void FadeoutHPWarning(long time)
        {
            if (mHPWarningLabel.IsVisible) // if this is visible, it means the user WAS low on HP, but now reached a "safe" threshold, so we need to clear the warning
            {
                if (!fadeHPWarning) // initialize things for fade
                {
                    mHPAlphaCounter = time + Options.Combat.HPWarningFadeTime;
                    mHPFlashCounter = time + Options.Combat.WarningFlashRate;
                    mHPIsFlashed = false;

                    fadeHPWarning = true; // start fade processing
                }

                // start detracting from its alpha
                float timeDiff = (float)Utilities.MathHelper.Clamp(mHPAlphaCounter - time, 0, mHPAlphaCounter) / Options.Combat.HPWarningFadeTime;
                mHPWarningAlpha = (int)(timeDiff * MAX_ALPHA);

                if (mHPFlashCounter < time)
                {
                    mHPFlashCounter = time + Options.Combat.WarningFlashRate;
                    mHPIsFlashed = !mHPIsFlashed;
                    if (mHPIsFlashed)
                    {
                        mHPWarningColor = Color.Red;
                    }
                    else
                    {
                        mHPWarningColor = Color.LightCoral;
                    }
                }
                mHPWarningLabel.SetTextColor(new Color(mHPWarningAlpha, mHPWarningColor.R, mHPWarningColor.G, mHPWarningColor.B), Label.ControlState.Normal);

                if (mHPWarningAlpha <= 0)
                {
                    mHPWarningLabel.Hide();
                }
            }
        }

        private void DisplayHPWarning(long time)
        {
            if (mHPFlashCounter < time)
            {
                mHPFlashCounter = time + Options.Combat.WarningFlashRate;
                mHPIsFlashed = !mHPIsFlashed;
                if (mHPIsFlashed)
                {
                    mHPWarningColor = Color.Red;
                }
                else
                {
                    mHPWarningColor = Color.LightCoral;
                }
            }
            mHPWarningLabel.SetTextColor(new Color(255, mHPWarningColor.R, mHPWarningColor.G, mHPWarningColor.B), Label.ControlState.Normal);
        }
    }
}
