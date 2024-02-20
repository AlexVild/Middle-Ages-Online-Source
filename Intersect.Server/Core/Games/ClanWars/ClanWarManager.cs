using Intersect.Enums;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using System;
using System.Linq;
namespace Intersect.Server.Core.Games.ClanWars
{
    public static class ClanWarManager
    {
        public static void StartClanWar()
        {
            var prevState = ClanWarActive;
            using (var context = DbInterface.CreatePlayerContext(false))
            {
                if (context.Clan_Wars.Any(cw => cw.IsActive))
                {
                    Console.WriteLine("A clan war is already active in the DB");
                    return;
                }

                var clanWar = new ClanWarInstance();
                clanWar.Start();
                context.Clan_Wars.Add(clanWar);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();

                CurrentWar = clanWar;
            }

            if (prevState != ClanWarActive)
            {
                Player.StartCommonEventsWithTriggerForAll(CommonEventTrigger.ClanWarStarted);
            }
            Console.WriteLine(Strings.Commandoutput.guildwarsenabled);
        }

        public static void InitializeManager()
        {
            using (var context = DbInterface.CreatePlayerContext())
            {
                CurrentWar = context.Clan_Wars.FirstOrDefault(cw => cw.IsActive);
            }
        }

        public static void EndAllClanWars()
        {
            var prevState = ClanWarActive;
            using (var context = DbInterface.CreatePlayerContext(false))
            {
                CurrentWar?.End();
                CurrentWar = null;
                
                var activeClanWars = context.Clan_Wars.Where(cw => cw.IsActive).ToArray();
                foreach (var cw in activeClanWars)
                {
                    cw.End();
                }

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }

            if (prevState != ClanWarActive)
            {
                Player.StartCommonEventsWithTriggerForAll(CommonEventTrigger.ClanWarEnded);
            }
            Console.WriteLine(Strings.Commandoutput.guildwarsdisabled);
        }

        public static bool ClanWarActive => CurrentWar != null;

        public static Guid CurrentWarId => CurrentWar?.Id ?? Guid.Empty;

        public static ClanWarInstance _currentWar { get; set; }

        public static ClanWarInstance CurrentWar 
        {
            get => _currentWar; 
            set
            {
                _currentWar = value;
                StatusChange(null, ClanWarActive);
            }
        }

        public static event EventHandler<bool> StatusChange = delegate { };
    }

}
