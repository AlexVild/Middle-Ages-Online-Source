using System.Collections.Generic;

namespace Intersect.Enums
{
    public static class StatusHelpers
    {
        public static List<StatusTypes> TenacityExcluded = new List<StatusTypes>()
        {
            StatusTypes.None,
            StatusTypes.Stealth,
            StatusTypes.Cleanse,
            StatusTypes.Invulnerable,
            StatusTypes.OnHit,
            StatusTypes.Shield,
            StatusTypes.Transform,
            StatusTypes.Swift,
            StatusTypes.Accurate,
            StatusTypes.Haste,
        };
    }

    public enum StatusTypes
    {

        None = 0,

        Silence = 1,

        Stun = 2,

        Snare = 3,

        Blind = 4,

        Stealth = 5,

        Transform = 6,

        Cleanse = 7,

        Invulnerable = 8,

        Shield = 9,

        Sleep = 10,

        OnHit = 11,

        Taunt = 12,

        Swift = 13,

        Accurate = 14,

        Haste = 15,

        Slowed = 16,
        
        Confused = 17,

        Steady = 18,

    }

}
