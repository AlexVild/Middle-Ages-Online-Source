using Intersect.Logging;
using System;
using System.Collections.Generic;

namespace Intersect.Server.Database.PlayerData.Migrations
{
    public class LoadoutResetMigration
    {
        public static void Run(PlayerContext context)
        {
            Console.WriteLine("Removing all player loadouts...");

            foreach (var loadout in context.Player_Loadouts)
            {
                context.Remove(loadout);
            }
            
            context.ChangeTracker.DetectChanges();
            context.SaveChanges();
            Console.WriteLine("Complete!");
        }
    }
}
