using Intersect.Server.Core.CommandParsing;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Commands
{
    internal sealed class ClearRecordCacheCommand : ServerCommand
    {
        public ClearRecordCacheCommand() : base(Strings.Commands.ClearRecordsCacheCommand)
        {
        }

        protected override void HandleValue(ServerContext context, ParserResult result)
        {
            Console.WriteLine("Clearing records cache...");
            try
            {
                PlayerRecordCache.ClearCache();
                Console.WriteLine("Records cache cleared!");
            } catch (Exception e)
            {
                Console.WriteLine($"Failed! Exception was: {e.Message}");
            }
        }
    }
}
