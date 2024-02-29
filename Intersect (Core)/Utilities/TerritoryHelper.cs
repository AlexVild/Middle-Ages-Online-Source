using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Utilities
{
    public static class TerritoryHelper
    {
        public static void TickHealth(ref long nextHealthTick, ref long health, int numInvaders, long currentTime, bool subtractive = false)
        {
            if (nextHealthTick > currentTime)
            {
                return;
            }

            var amount = Options.Instance.ClanWar.HealthTickMs + (numInvaders - 1 * Options.Instance.ClanWar.HealthTickBonusMs);
            amount = MathHelper.Clamp(amount, Options.Instance.ClanWar.HealthTickMs, Options.Instance.ClanWar.HealthTickMaximumMs);

            if (subtractive)
            {
                amount *= -1;
            }

            health += amount;
            nextHealthTick = currentTime + Options.Instance.ClanWar.HealthTickMs;
        }
    }
}
