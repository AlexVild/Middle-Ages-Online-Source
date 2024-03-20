using Intersect.Client.General;
using Intersect.Utilities;
using System;

namespace Intersect.Client.Core
{

    public static class Fade
    {
        private const float STANDARD_FADE_RATE = 800f;
        private const float FAST_FADE_RATE = 800f;

        public enum FadeType
        {

            None = 0,

            In = 1,

            Out = 2,

        }

        private static FadeType sCurrentAction;
        public static FadeType CurrentAction
        {
            get => sCurrentAction;

            set
            {
                sCurrentAction = value;
                if (sCurrentAction == FadeType.None && CompleteCallback != null)
                {
                    CompleteCallback();
                    CompleteCallback = null;
                }
            }
        }

        private static float sFadeAmt;
        public static float FadeAmt
        {
            get => sFadeAmt;
            set => sFadeAmt = value;
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
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false, Action callback = null)
        {
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;
            
            CurrentAction = FadeType.Out;
            sFadeAmt = 0f;
            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
            sAlertServerWhenFaded = alertServerWhenFaded;

            CompleteCallback = callback;
        }

        public static bool DoneFading()
        {
            return CurrentAction == FadeType.None;
        }

        public static float GetFade()
        {
            return sFadeAmt;
        }

        public static void Update()
        {
            if (CurrentAction == FadeType.In)
            {
                sFadeAmt = (float)MathHelper.Clamp(sFadeAmt - (Timing.Global.MillisecondsUtcUnsynced - sLastUpdate) / sFadeRate * 255f, 0f, 255f);
                if (sFadeAmt <= 0f)
                {
                    if (Globals.WaitFade)
                    {
                        Networking.PacketSender.SendFadeFinishPacket();
                    }
                    CurrentAction = FadeType.None;
                    sFadeAmt = 0f;
                }
            }
            else if (CurrentAction == FadeType.Out)
            {
                sFadeAmt = (float)MathHelper.Clamp(sFadeAmt + (Timing.Global.MillisecondsUtcUnsynced - sLastUpdate) / sFadeRate * 255f, 0f, 255f);
                if (sFadeAmt >= 255f)
                {
                    CurrentAction = FadeType.None;
                    if (Globals.WaitFade)
                    {
                        Networking.PacketSender.SendFadeFinishPacket();
                    }
                    if (sAlertServerWhenFaded)
                    {
                        Networking.PacketSender.SendMapTransitionReady();
                    }
                    sAlertServerWhenFaded = false;
                    sFadeAmt = 255f;
                }
            }

            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
        }

    }

}
