using Intersect.Server.Database.GameData;
using System;

namespace Intersect.Server.Utilities
{
    public static class DayOfWeekUtils
    {
        /// <summary>
        /// Currently, this returns the <see cref="DayOfWeek"/> based on a server variable that is effected by cron job of its running machine
        /// </summary>
        /// <returns>The server's current day of the week</returns>
        public static WeekDay GetDayOfWeek()
        {
            try
            {
                int varVal = (int)(GameContext.Queries.ServerVariableById(new Guid(Options.Instance.DuelOpts.DayOfWeekServerVarId))?.Value?.Integer ?? 0);
                return (WeekDay)varVal;
            }
            catch (InvalidCastException e)
            {
                Logging.Log.Error("Failed to parse day of week correctly.");
                Logging.Log.Error(e.Message);
                Logging.Log.Error(e.StackTrace);

                return WeekDay.Sunday;
            }
        }

        public enum WeekDay
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }
    }
}
