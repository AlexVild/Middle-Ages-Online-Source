using Intersect.Client.General;
using Intersect.Utilities;

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

        private static float sFadeAmt;

        private static float sFadeRate = STANDARD_FADE_RATE;

        private static long sLastUpdate;

        private static bool sAlertServerWhenFaded;

        public static void FadeIn(bool fast = false)
        {
            if (!Globals.Database.FadeTransitions)
            {
                Wipe.FadeIn(fast);
                return;
            }

            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;

            sCurrentAction = FadeType.In;
            sFadeAmt = 255f;
            sLastUpdate = Timing.Global.Milliseconds;
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false)
        {
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;
            
            sCurrentAction = FadeType.Out;
            sFadeAmt = 0f;
            sLastUpdate = Timing.Global.Milliseconds;
            sAlertServerWhenFaded = alertServerWhenFaded;
        }

        public static bool DoneFading()
        {
            if (!Globals.Database.FadeTransitions)
            {
                return Wipe.DoneFading();
            }
            else
            {
                return sCurrentAction == FadeType.None;
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

            if (sCurrentAction == FadeType.In)
            {
                sFadeAmt -= (Timing.Global.Milliseconds - sLastUpdate) / sFadeRate * 255f;
                if (sFadeAmt <= 0f)
                {
                    if (Globals.WaitFade)
                    {
                        Networking.PacketSender.SendFadeFinishPacket();
                    }
                    sCurrentAction = FadeType.None;
                    sFadeAmt = 0f;
                }
            }
            else if (sCurrentAction == FadeType.Out)
            {
                sFadeAmt += (Timing.Global.Milliseconds - sLastUpdate) / sFadeRate * 255f;
                if (sFadeAmt >= 255f)
                {
                    sCurrentAction = FadeType.None;
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
