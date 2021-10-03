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
                var changed = UpdateGuildwarsValue(true);

                if (changed)
                {
                    Console.WriteLine(Strings.Commandoutput.guildwarsenabled);
                } else
                {
                    Console.WriteLine(Strings.Commandoutput.guildwarsenabledalready);
                }
            }
            else if (operation == Strings.Commands.Arguments.GuildWarsEnd)
            {
                var changed = UpdateGuildwarsValue(false);

                if (changed)
                {
                    Console.WriteLine(Strings.Commandoutput.guildwarsdisabled);
                }
                else
                {
                    Console.WriteLine(Strings.Commandoutput.guildwarsdisabledalready);
                }
            }
        }

        private static bool UpdateGuildwarsValue(bool val)
        {
            if (Options.GuildWarsGUID != "")
            {
                var variable = GameContext.Queries.ServerVariableById(new Guid(Options.GuildWarsGUID));

                if (variable == null)
                {
                    Console.WriteLine(Strings.Commandoutput.guildwarsinvalid);
                    return false;
                }

                var changed = true;
                if (variable.Value?.Value == val)
                {
                    changed = false;
                }
                variable.Value.Value = val;

                if (changed)
                {
                    Player.StartCommonEventsWithTriggerForAll(Enums.CommonEventTrigger.ServerVariableChange, "", Options.GuildWarsGUID.ToString());
                }
                DbInterface.UpdatedServerVariables.AddOrUpdate(variable.Id, variable, (key, oldValue) => variable);

                return changed;
            } else
            {
                Console.WriteLine(Strings.Commandoutput.guildwarsinvalid);
                return false;
            }
        }
    }
}
