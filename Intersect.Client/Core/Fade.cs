using Intersect.Client.General;

namespace Intersect.Client.Core
{

    public static class Fade
    {
        private const float STANDARD_FADE_RATE = 500f;
        private const float FAST_FADE_RATE = 500f;

        public enum FadeType
        {

            None = 0,

            In = 1,

            Out = 2,

        }

        private static FadeType sCurrentAction;

        private static float sFadeAmt;

        private static float sInvertFadeAmt = Graphics.CurrentView.Width / 2;

        private static float sFadeRate = STANDARD_FADE_RATE;

        private static long sLastUpdate;

        private static bool sAlertServerWhenFaded;

        public static void FadeIn(bool fast = false)
        {
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;

            sCurrentAction = FadeType.In;
            sFadeAmt = Graphics.CurrentView.Width / 2;
            sInvertFadeAmt = 0f;
            sLastUpdate = Globals.System.GetTimeMs();
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false)
        {
            sFadeRate = fast ? FAST_FADE_RATE : STANDARD_FADE_RATE;
            
            sCurrentAction = FadeType.Out;
            sFadeAmt = 0f;
            sInvertFadeAmt = Graphics.CurrentView.Width / 2;
            sLastUpdate = Globals.System.GetTimeMs();
            sAlertServerWhenFaded = alertServerWhenFaded;
        }

        public static bool DoneFading()
        {
            return sCurrentAction == FadeType.None;
        }

        public static float GetFade(bool inverted = false)
        {
            float maxWidth = Graphics.CurrentView.Width / 2;
            int transitionNum = 8;

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

        public static void Update()
        {
            float maxWidth = Graphics.CurrentView.Width / 2;
            var amountChange = (Globals.System.GetTimeMs() - sLastUpdate) / sFadeRate * maxWidth;

            if (sCurrentAction == FadeType.In)
            {
                sFadeAmt -= amountChange;
                sInvertFadeAmt += amountChange;
                
                if (sFadeAmt <= 0f && sInvertFadeAmt >= maxWidth)
                {
                    sCurrentAction = FadeType.None;
                    sFadeAmt = 0f;
                    sInvertFadeAmt = maxWidth;
                }
            }
            else if (sCurrentAction == FadeType.Out)
            {
                sFadeAmt += amountChange;
                sInvertFadeAmt -= amountChange;
                if (sFadeAmt >= maxWidth && sInvertFadeAmt <= 0f)
                {
                    sCurrentAction = FadeType.None;
                    if (sAlertServerWhenFaded)
                    {
                        Networking.PacketSender.SendMapTransitionReady(Globals.futureWarpMapId, Globals.futureWarpX, Globals.futureWarpY, Globals.futureWarpDir, Globals.futureWarpInstanceType);
                    }
                    sAlertServerWhenFaded = false;
                    sFadeAmt = maxWidth;

                    sInvertFadeAmt = 0f;
                }
            }

            sLastUpdate = Globals.System.GetTimeMs();
        }

    }

}
