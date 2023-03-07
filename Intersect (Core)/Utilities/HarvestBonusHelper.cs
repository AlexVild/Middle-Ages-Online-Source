using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Utilities
{
    public static class HarvestBonusHelper
    {
        public static EffectType GetBonusEffectForResource(Guid resourceId)
        {
            var resource = ResourceBase.Get(resourceId);
            if (resource == default)
            {
                return EffectType.None;
            }

            var tool = resource.Tool;

            if (tool == 0)
            {
                return EffectType.Lumberjack;
            }
            if (tool == 1)
            {
                return EffectType.Prospector;
            }
            if (tool == 3)
            {
                return EffectType.Angler;
            }

            return EffectType.None;
        }
    }
}
