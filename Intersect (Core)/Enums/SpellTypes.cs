using System.ComponentModel;

namespace Intersect.Enums
{

    public enum SpellTypes
    {
        [Description("Combat")]
        CombatSpell = 0,

        [Description("Warp")]
        Warp = 1,

        [Description("Warp To")]
        WarpTo = 2,

        [Description("Dash")]
        Dash = 3,

        [Description("Event")]
        Event = 4,

        [Description("Passive")]
        Passive = 5,
    }

}
