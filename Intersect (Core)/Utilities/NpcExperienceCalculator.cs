using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Utilities
{
    public static class NpcExperienceCalculator
    {
        readonly static int[] DummyStats = new int[(int) Stats.StatCount];

        readonly static float ResistanceMod = 5.0f;

        readonly static float DodgeMod = 15.0f;

        public static long Calculate(NpcBase npc)
        {
            float exp = 0f;

            // HP
            exp += (float)npc.MaxVital[(int)Vitals.Health] / 10;

            if (npc.Spells.Count > 0) 
            {
                var magAtkMod = (float)npc.Stats[(int)Stats.AbilityPower] / 10;
                exp += (float)Math.Floor(npc.Spells.Count * magAtkMod);
            }

            // Add exp based on damage
            CombatUtilities.CalculateDamage(npc.AttackTypes, 1, 100, npc.Stats, DummyStats, out var maxHit);
            exp += maxHit;

            // Resistances
            exp += npc.Stats[(int)Stats.Defense] / ResistanceMod;
            exp += npc.Stats[(int)Stats.MagicResist] / ResistanceMod;
            exp += npc.Stats[(int)Stats.PierceResistance] / ResistanceMod;
            exp += npc.Stats[(int)Stats.SlashResistance] / ResistanceMod;

            // Dodging
            exp += npc.Stats[(int)Stats.Accuracy] / DodgeMod;
            exp += npc.Stats[(int)Stats.Evasion] / DodgeMod;

            // Crit
            var crit = ((float)npc.CritChance / 100) * npc.CritMultiplier;
            var critBonus = exp * crit;
            exp += (float)critBonus;

            return (long)Math.Ceiling(exp);
        }
    }
}
