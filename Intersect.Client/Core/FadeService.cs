using Intersect.Client.General;
using System;
using static Intersect.Client.Core.Fade;

namespace Intersect.Client.Core
{
    public static class FadeService
    {
        public static bool FadeInstead => (Globals.Database?.FadeTransitions ?? false) || Globals.GameState == GameStates.Intro;

        public static void FadeIn(bool fast = false, Action callback = null)
        {
            if (GetFade() == 0)
            {
                if (Globals.WaitFade)
                {
                    Networking.PacketSender.SendFadeFinishPacket();
                }
                if (callback != null)
                {
                    callback();
                }
                return;
            }
            if (FadeInstead)
            {
                Fade.FadeIn(fast, callback);
            }
            else
            {
                Wipe.FadeIn(fast, callback);
            }
        }

        public static void SetFade(float amt, bool both = false)
        {
            if (FadeInstead || both)
            {
                Fade.FadeAmt = amt;
                Fade.CurrentAction = FadeType.None;
                if (!both)
                {
                    return;
                }
            }
            float maxWidth = Graphics.CurrentView.Width / 2;
            Wipe.FadeAmt = (maxWidth / 255) * amt;
            Wipe.InvertFadeAmt = maxWidth - Wipe.FadeAmt;
            Wipe.CurrentAction = FadeType.In;
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false, Action callback = null)
        {
            if (GetFade() == 255f)
            {
                if (Globals.WaitFade)
                {
                    Networking.PacketSender.SendFadeFinishPacket();
                }
                if (alertServerWhenFaded)
                {
                    Networking.PacketSender.SendMapTransitionReady();
                }

                if (callback != null)
                {
                    callback();
                }
                return;
            }
            if (FadeInstead)
            {
                Fade.FadeOut(alertServerWhenFaded, fast, callback);
            }
            else
            {
                Wipe.FadeOut(alertServerWhenFaded, fast, callback);
            }
        }

        public static bool DoneFading()
        {
            if (FadeInstead)
            {
                return Fade.DoneFading();
            }
            else
            {
                return Wipe.DoneFading();
            }
        }

        public static FadeType FadeType
        {
            get
            {
                if (FadeInstead)
                {
                    return Fade.CurrentAction;
                }
                else
                {
                    return Wipe.CurrentAction;
                }
            }
        }

        public static float GetFade()
        {
            if (FadeInstead)
            {
                return Fade.GetFade();
            }
            else
            {
                return Wipe.GetFade();
            }
        }
    }
}
