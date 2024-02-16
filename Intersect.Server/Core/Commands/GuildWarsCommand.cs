using Intersect.Server.Core.CommandParsing;
using Intersect.Server.Core.CommandParsing.Arguments;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Database;
using System;
using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Database.GameData;
using Intersect.Server.Entities;
using Intersect.Server.Core.Games.ClanWars;

namespace Intersect.Server.Core.Commands
{
    class GuildWarsCommand : ServerCommand
    {
        public GuildWarsCommand() : base(
            Strings.Commands.GuildWars,
            new EnumArgument<string>(
                Strings.Commands.Arguments.GuildWars, RequiredIfNotHelp, true,
                Strings.Commands.Arguments.GuildWarsStart.ToString(), Strings.Commands.Arguments.GuildWarsEnd.ToString()
            )
        )
        {
        }

        private EnumArgument<string> Operation => FindArgumentOrThrow<EnumArgument<string>>();

        protected override void HandleValue(ServerContext context, ParserResult result)
        {
            var operation = result.Find(Operation);
            if (operation == Strings.Commands.Arguments.GuildWarsStart)
            {
                ClanWarManager.StartClanWar();
            }
            else if (operation == Strings.Commands.Arguments.GuildWarsEnd)
            {
                ClanWarManager.EndAllClanWars();
            }
        }
    }
}
