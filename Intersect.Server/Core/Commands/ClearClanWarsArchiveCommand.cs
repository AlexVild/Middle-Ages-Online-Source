using Intersect.Server.Core.CommandParsing;
using Intersect.Server.Core.Games.ClanWars;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Commands
{
    internal sealed class ClearClanWarsArchiveCommand : ServerCommand
    {
        public ClearClanWarsArchiveCommand() : base(Strings.Commands.ClearClanWarsCommand)
        {
        }

        protected override void HandleValue(ServerContext context, ParserResult result)
        {
            Console.WriteLine("Clearing archival clan wars data...");

            ClanWarInstance[] clanWars;
            using (var db = DbInterface.CreatePlayerContext())
            {
                clanWars = db.Clan_Wars.Where(cw => !cw.IsActive).ToArray();
            }
            
            DbInterface.Pool.QueueWorkItem(DeleteInactiveClanWars);
        }

        private void DeleteInactiveClanWars()
        {
            using (var context = DbInterface.CreatePlayerContext(false))
            {
                var clanWars = context.Clan_Wars.Where(cw => !cw.IsActive).ToArray();

                foreach (var clanWar in clanWars)
                {
                    var participants = context.Clan_War_Participants.Where(p => p.ClanWarId == clanWar.Id);
                    context.Clan_War_Participants.RemoveRange(participants);
                    context.Clan_Wars.Remove(clanWar);
                }

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }

            Console.WriteLine("Inactive Clan Wars have been pruned.");
        }
    }
}
