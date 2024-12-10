using System.Collections.Generic;

namespace Intersect.Enums
{

    public enum SpellTargetTypes
    {

        Self = 0,

        Single = 1,

        AoE = 2,

        Projectile = 3,

        OnHit = 4,

        Trap = 5

    }

    public static class SpellTypeHelpers
    {
        public static bool IsMovementSpell(SpellTypes type) => MovementSpells.Contains(type);

        public static bool SpellTypeRequiresTarget(SpellTargetTypes type) => !NoTargetSpells.Contains(type);

        private static List<SpellTargetTypes> NoTargetSpells = new List<SpellTargetTypes>()
        {
            SpellTargetTypes.Self,
            SpellTargetTypes.Projectile,
            SpellTargetTypes.Trap,
            SpellTargetTypes.OnHit,
            SpellTargetTypes.AoE,
        };

        private static List<SpellTypes> MovementSpells = new List<SpellTypes>()
        {
            SpellTypes.Dash,
            SpellTypes.Warp,
            SpellTypes.WarpTo,
        };
    }

    public enum AoeShape
    {
        Circle,
        Rectangle,
    }

}
