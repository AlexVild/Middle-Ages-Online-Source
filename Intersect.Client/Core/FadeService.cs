using Intersect.Client.General;
using System;
using static Intersect.Client.Core.Fade;

namespace Intersect.Client.Core
{
    public static class FadeService
    {
        public static bool FadeInstead => Globals.Database?.FadeTransitions ?? false;

        public static void FadeIn(bool fast = false, Action callback = null)
        {
            if (Globals.Database.FadeTransitions)
            {
                Fade.FadeIn(fast, callback);
            }
            else
            {
                Wipe.FadeIn(fast, callback);
            }
        }

        public static void SetFade(float amt)
        {
            if (Globals.Database.FadeTransitions)
            {
                Fade.FadeAmt = amt;
            }
            else
            {
                float maxWidth = Graphics.CurrentView.Width / 2;
                Wipe.FadeAmt = (maxWidth / 255) * amt;
                Wipe.InvertFadeAmt = maxWidth - Wipe.FadeAmt;
            }
        }

        public static void FadeOut(bool alertServerWhenFaded = false, bool fast = false, Action callback = null)
        {
            if (Globals.Database.FadeTransitions)
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
            if (Globals.Database.FadeTransitions)
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
                if (Globals.Database.FadeTransitions)
                {
                    return Fade.CurrentAction;
                }
                else
                {
                    return Wipe.CurrentAction;
                }
            }
        }
    }
}
