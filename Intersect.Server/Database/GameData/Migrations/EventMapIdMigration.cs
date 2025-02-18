using Intersect.GameObjects.Events;
using Intersect.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Database.GameData.Migrations
{
    public class EventMapIdMigration
    {
        public static void Run(GameContext context)
        {
            FixEventMapIds(context);
        }

        public static void FixEventMapIds(GameContext context)
        {
            Log.Info("Checking for Events with incorrect MapIds...");
            var count = 0;
            // Go through each map
            foreach (var map in context.Maps)
            {
                // For every event on the map...
                foreach (var evtId in map.EventIds)
                {
                    var evnt = context.Events.Find(evtId);
                    if (evnt == null || evnt.CommonEvent || evnt.MapId == map.Id)
                    {
                        continue;
                    }

                    // If the event is 1) not common and 2) is not using this map's ID, fix it. It was copied wrong in the editor
                    var refMap = context.Maps.Find(evnt.MapId);
                    evnt.MapId = map.Id;
                    count++;
                    Log.Info($"PROBLEM EVENT! {evtId} on {map.Name}, was referencing {refMap?.Name ?? "null"}");
                }
            }

            // Track our changes and save them or the work we've just done is lost.
            context.ChangeTracker.DetectChanges();
            context.SaveChanges();

            Log.Info($"Done! Fixed {count} events with improper MapIDs.");
        }
    }
}
