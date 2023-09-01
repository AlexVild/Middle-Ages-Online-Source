using Intersect.Client.General;
using Intersect.Utilities;
using System;
using static Intersect.Client.Core.Fade;

namespace Intersect.Client.Core
{

    public static class Wipe
    {
        private const float STANDARD_FADE_RATE = 650f;
        private const float FAST_FADE_RATE = 650f;

        private static FadeType sCurrentAction;

        public static FadeType CurrentAction
        {
            get
            {
                return sCurrentAction;
            }
            set {
                sCurrentAction = value;
                if (sCurrentAction == FadeType.None && CompleteCallback != null)
                {
                    CompleteCallback();
                    CompleteCallback = null;
                }
            }
        }

        // Used to add some extra time when the wipe is "closed"
        private const int OUT_EXTRA_TIME = 100;
        private static long sTimeFadedOut;
        private static long sTimeFadedIn;
        private static bool sIsFaded = false;

        private static float sFadeAmt;
        public static float FadeAmt
        {
            get => sFadeAmt;
            set => sFadeAmt = value;
        }

        private static float sInvertFadeAmt = Graphics.CurrentView.Width / 2;
        public static float InvertFadeAmt
        {
            get => sInvertFadeAmt;
            set => sInvertFadeAmt = value;
        }

        private static float sFadeRate = STANDARD_FADE_RATE;

        private static long sLastUpdate;

        private static bool sAlertServerWhenFaded;

        public static Action CompleteCallback;

        public static void FadeIn(bool fast = false, Action callback = null)
        {
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;

            CurrentAction = FadeType.In;
            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
            CompleteCallback = callback;
            sTimeFadedIn = Timing.Global.MillisecondsUtcUnsynced + OUT_EXTRA_TIME;
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false, Action callback = null)
        {
            sIsFaded = false;
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;
            
            CurrentAction = FadeType.Out;
            sFadeAmt = 0f;
            sInvertFadeAmt = Graphics.CurrentView.Width / 2;
            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
            sAlertServerWhenFaded = alertServerWhenFaded;
            CompleteCallback = callback;
        }

        public static bool DoneFading()
        {
            return CurrentAction == FadeType.None;
        }

        public static float GetFade(bool inverted = false)
        {
            float maxWidth = Graphics.CurrentView.Width / 2;
            int transitionNum = 14;

            var fade = sFadeAmt;
            if (inverted) fade = sInvertFadeAmt;
            for (int i = transitionNum; i >= 1; i--)
            {
                if (fade >= maxWidth / transitionNum * i)
                {
                    return maxWidth / transitionNum * i;
                }
            }

            return 0f;
        }

        /// <summary>
        /// Used to hard-reset the wipe to its full state
        /// </summary>
        public static void ResetToBlack()
        {
            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
            float maxWidth = Graphics.CurrentView.Width / 2;
            sFadeAmt = maxWidth;
            sInvertFadeAmt = 0;
        }

        public static void Update()
        {
            if (Timing.Global.MillisecondsUtcUnsynced < sTimeFadedIn && CurrentAction == FadeType.In)
            {
                sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
                return;
            }

            float maxWidth = Graphics.CurrentView.Width / 2;
            var amountChange = (Timing.Global.MillisecondsUtcUnsynced - sLastUpdate) / sFadeRate * maxWidth;

            // If we're supposed to be done, but there's remnants remaining
            if (CurrentAction == FadeType.None 
                && ( (sInvertFadeAmt < maxWidth && sInvertFadeAmt > maxWidth / 2) || (sFadeAmt > 0 && sFadeAmt < maxWidth / 2) ))
            {
                // Clear them
                sFadeAmt = 0f;
                sInvertFadeAmt = maxWidth;
            }

            if (CurrentAction == FadeType.In)
            {
                sFadeAmt = (float)MathHelper.Clamp(sFadeAmt - amountChange, 0f, maxWidth);
                sInvertFadeAmt = (float)MathHelper.Clamp(sInvertFadeAmt + amountChange, 0f, maxWidth);

                if (sFadeAmt <= 0f && sInvertFadeAmt >= maxWidth)
                {
                    if (Globals.WaitFade)
                    {
                        Networking.PacketSender.SendFadeFinishPacket();
                    }
                    CurrentAction = FadeType.None;
                    sFadeAmt = 0f;
                    sInvertFadeAmt = maxWidth;
                }
            }
            else if (CurrentAction == FadeType.Out)
            {
                sFadeAmt = (float)MathHelper.Clamp(sFadeAmt + amountChange, 0f, maxWidth);
                sInvertFadeAmt = (float)MathHelper.Clamp(sInvertFadeAmt - amountChange, 0f, maxWidth);

                if (sFadeAmt >= maxWidth && sInvertFadeAmt <= 0f)
                {
                    if (!sIsFaded)
                    {
                        sIsFaded = true;
                        sTimeFadedOut = Timing.Global.MillisecondsUtcUnsynced + OUT_EXTRA_TIME;
                    }
                    if (Timing.Global.MillisecondsUtcUnsynced < sTimeFadedOut)
                    {
                        sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
                        return;
                    }
                    CurrentAction = FadeType.None;
                    if (Globals.WaitFade)
                    {
                        Networking.PacketSender.SendFadeFinishPacket();
                    }
                    if (sAlertServerWhenFaded)
                    {
                        Networking.PacketSender.SendMapTransitionReady(Globals.futureWarpMapId, Globals.futureWarpX, Globals.futureWarpY, Globals.futureWarpDir, Globals.futureWarpInstanceType, Globals.futureDungeonId);
                    }

                    sAlertServerWhenFaded = false;
                    sFadeAmt = maxWidth;

                    sInvertFadeAmt = 0f;
                }
            }
            
            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
        }

    }

}
