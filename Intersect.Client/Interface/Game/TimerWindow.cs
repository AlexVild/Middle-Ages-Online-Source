using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects.Timers;
using Intersect.Utilities;
using System;

namespace Intersect.Client.Interface.Game
{
    public class TimerWindow
    {
        //Controls
        private Canvas mGameCanvas;

        private WindowControl mBackground;

        private Label mTimer;

        private Button mNextButton;

        private Button mPrevButton;

        private int DisplayIndex; 

        public bool IsHidden
        {
            get { return mBackground.IsHidden; }
            set { mBackground.IsHidden = value; }
        }

        public TimerWindow(Canvas gameCanvas)
        {
            mGameCanvas = gameCanvas;
            mBackground = new WindowControl(gameCanvas, string.Empty, false, "TimerWindow");
            mBackground.DisableResizing();

            mTimer = new Label(mBackground, "Timer");

            mNextButton = new Button(mBackground, "NextButton");
            mPrevButton = new Button(mBackground, "PrevButton");
            mNextButton.Clicked += MNextButton_Clicked;
            mPrevButton.Clicked += MPrevButton_Clicked;

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void MPrevButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            var timers = Timers.DisplayableTimers;
            if (DisplayIndex == 0)
            {
                DisplayIndex = timers.Count - 1;
                return;
            }

            MathHelper.Clamp(DisplayIndex--, 0, timers.Count - 1);
        }

        private void MNextButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            var timers = Timers.DisplayableTimers;
            if (DisplayIndex == timers.Count - 1)
            {
                DisplayIndex = 0;
                return;
            }

            MathHelper.Clamp(DisplayIndex++, 0, timers.Count - 1);
        }

        public void Update()
        {
            var timers = Timers.DisplayableTimers;
            var timerCount = timers.Count;
            mBackground.IsHidden = timerCount <= 0;

            if (!mBackground.IsHidden)
            {
                while (DisplayIndex >= timerCount && timerCount > 0)
                {
                    DisplayIndex--; // Move to the previous display index if the old index is no longer valid
                }

                var activeTimer = timers[DisplayIndex];

                mBackground.Title = activeTimer.DisplayName;

                activeTimer.Update();
                mTimer.Text = activeTimer.Time;
                mTimer.IsHidden = activeTimer.IsHidden;

                // Only show next/previous buttons if more than one timer exists
                mNextButton.IsHidden = timerCount <= 1;
                mPrevButton.IsHidden = timerCount <= 1;
            }
        }

        public void ShowTimer(Guid descriptorId)
        {
            if (!TimerDescriptor.TryGet(descriptorId, out var timer) || Globals.Me == null || Globals.Me.MapInstance == null)
            {
                return;
            }

            if (timer.ContainsExclusiveMap(Globals.Me.MapInstance.Id))
            {
                DisplayIndex = MathHelper.Clamp(Timers.DisplayableTimers.FindIndex(t => t.DescriptorId == descriptorId), 0, int.MaxValue);
            }
        }
    }
}

