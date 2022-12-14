using Intersect.Enums;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Utilities
{
    public static class ThreatLevelUtilities
    {
        public static Dictionary<ThreatLevel, Color> ColorMapping = new Dictionary<ThreatLevel, Color>
        {
            { ThreatLevel.Midnight, new Color(255, 206, 109, 241) },
            { ThreatLevel.Extreme, new Color(255, 224, 112, 178) },
            { ThreatLevel.Deadly, new Color(255, 206, 109, 241) },
            { ThreatLevel.Threatening, new Color(255, 200, 145, 62) },
            { ThreatLevel.Fair, new Color(255, 206, 109, 241) },
            { ThreatLevel.Wimpy, new Color(255, 206, 109, 241) },
            { ThreatLevel.Trivial, new Color(255, 206, 109, 241) },
        };

        public static int GetThreatValue(int[] stats, int[] maxVitals)
        {
            if (stats.Length != (int)Stats.StatCount)
            {
                throw new ArgumentException($"Invalid stats array given for threat calculation. Expected {Stats.StatCount} elements but received {stats.Length}.");
            }
            if (maxVitals.Length != (int)Vitals.VitalCount)
            {
                throw new ArgumentException($"Invalid vitals array given for threat calculation. Expected {Vitals.VitalCount} elements but received {maxVitals.Length}.");
            }

            int score = 0;
            
            // Calculate attack stats
            score += stats[(int)Stats.Attack];
            score += stats[(int)Stats.PierceAttack];
            score += stats[(int)Stats.SlashAttack];
            score += stats[(int)Stats.AbilityPower];
            
            // Calculate defense
            score += stats[(int)Stats.Defense];
            score += stats[(int)Stats.PierceResistance];
            score += stats[(int)Stats.SlashResistance];
            score += stats[(int)Stats.MagicResist];

            // Calculate additional info
            score += stats[(int)Stats.Evasion];
            score += stats[(int)Stats.Accuracy];
            score += stats[(int)Stats.Speed];

            score += maxVitals[(int)Vitals.Health] / 10;
            score += maxVitals[(int)Vitals.Mana] / 10;

            return score;
        }
    }
}
