using Intersect.Server.Database;
using Intersect.Server.Database.GameData;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Utilities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Intersect.Server.Core.Games.ClanWars
{
    public static class ClanWarManager
    {
        public static void StartClanWar()
        {
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
                OnStatusChange();
            }
        }

        public static event EventHandler<bool> StatusChange = delegate { };

        private static void OnStatusChange()
        {
            StatusChange(null, ClanWarActive);
        }
    }

}
