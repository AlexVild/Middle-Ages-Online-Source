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
        private static FadeType CurrentAction
        {
            get => sCurrentAction;

            set
            {
                sCurrentAction = value;
                if (sCurrentAction == FadeType.None && CompleteCallback != null)
                {
                    CompleteCallback();
                }
            }
        }

        private static float sFadeAmt;

        private static float sFadeRate = STANDARD_FADE_RATE;

        private static long sLastUpdate;

        private static bool sAlertServerWhenFaded;

        private static Action CompleteCallback;

        public static void FadeIn(bool fast = false, Action callback = null)
        {
            if (!Globals.Database.FadeTransitions)
            {
                Wipe.FadeIn(fast);
                return;
            }

            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;

            CurrentAction = FadeType.In;
            sFadeAmt = 255f;
            sLastUpdate = Timing.Global.Milliseconds;

            CompleteCallback = callback;
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false, Action callback = null)
        {
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;
            
            CurrentAction = FadeType.Out;
            sFadeAmt = 0f;
            sLastUpdate = Timing.Global.Milliseconds;
            sAlertServerWhenFaded = alertServerWhenFaded;

            CompleteCallback = callback;
        }

        public static bool DoneFading()
        {
            if (!Globals.Database.FadeTransitions)
            {
                return Wipe.DoneFading();
            }
            else
            {
                return CurrentAction == FadeType.None;
            }
        }

        public static float GetFade()
        {
            return sFadeAmt;
        }

        public static void Update()
        {
            if (!Globals.Database.FadeTransitions)
            {
                Wipe.Update();
                return;
            }

            if (CurrentAction == FadeType.In)
            {
                sFadeAmt -= (Timing.Global.Milliseconds - sLastUpdate) / sFadeRate * 255f;
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
                sFadeAmt += (Timing.Global.Milliseconds - sLastUpdate) / sFadeRate * 255f;
                if (sFadeAmt >= 255f)
                {
                    CurrentAction = FadeType.None;
                    if (Globals.WaitFade)
                    {
                        Networking.PacketSender.SendFadeFinishPacket();
                    }
                    if (sAlertServerWhenFaded)
                    {
                        Networking.PacketSender.SendMapTransitionReady(Globals.futureWarpMapId, Globals.futureWarpX, Globals.futureWarpY, Globals.futureWarpDir, Globals.futureWarpInstanceType);
                    }
                    sAlertServerWhenFaded = false;
                    sFadeAmt = 255f;
                }
            }

            sLastUpdate = Timing.Global.Milliseconds;
        }

    }

}
