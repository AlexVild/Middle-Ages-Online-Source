using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Server.Utilities
{
    public static class NpcExperienceService
    {
        private static Dictionary<Guid, long> Experience = new Dictionary<Guid, long>();

        public static void Initialize()
        {
            Experience.Clear();

            var npcs = NpcBase.Lookup.Select(n => ((NpcBase)n.Value)).ToArray();

            foreach(var mob in npcs)
            {
                Experience[mob.Id] = NpcExperienceCalculator.Calculate(mob);
            }
        }

        public static long GetExp(Guid npcId)
        {
            if (!Experience.TryGetValue(npcId, out var exp))
            {
                return 0;
            }
            return exp;
        }

        public static void PrettyPrint()
        {
            var npcs = NpcBase.Lookup.Select(n => ((NpcBase)n.Value)).OrderBy(n => n.Name).ToArray();

            Console.WriteLine("===\t\tNPC Experience Chart\t\t===");
            foreach (var mob in npcs)
            {
                if (!Experience.TryGetValue(mob.Id, out var exp))
                {
                    continue;
                }

                var name = NpcBase.GetName(mob.Id);
                Console.WriteLine($"||\t{name}: {exp}");
            }
            Console.WriteLine("===\t\t END Experience Chart\t\t===");
        }
    }
}
