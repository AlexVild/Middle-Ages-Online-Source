using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Utilities
{
    public static class ItemInstanceHelper
    {
        public static int[] GetStatBoosts(ItemProperties itemProperties)
        {
            return itemProperties.StatModifiers.Select((modVal, idx) =>
            {
                if (idx < 0 || idx >= itemProperties.StatModifiers.Length)
                {
                    return modVal;
                }

                return modVal + itemProperties.StatEnhancements[idx];
            }).ToArray();
        }

        public static int GetStatBoost(ItemProperties itemProps, Stats stat)
        {
            if (itemProps == null)
            {
                return 0;
            }

            if (stat >= Stats.StatCount || (int)stat < 0 || itemProps == default)
            {
                return 0;
            }

            return itemProps.StatEnhancements.ElementAtOrDefault((int)stat) + itemProps.StatModifiers.ElementAtOrDefault((int)stat);
        }

        public static int GetEffectBoost(ItemProperties itemProps, EffectType effect)
        {
            if (itemProps == null)
            {
                return 0;
            }

            return itemProps?.EffectEnhancements?.ElementAtOrDefault((int)effect) ?? 0;
        }

        public static EffectData[] GetEnhancementEffectData(ItemProperties itemProps)
        {
            List<EffectData> effectDatas = new List<EffectData>();
            var idx = 0;
            foreach (var effectEnhancement in itemProps.EffectEnhancements)
            {
                if (effectEnhancement == 0)
                {
                    idx++;
                    continue;
                }
                effectDatas.Add(new EffectData((EffectType)idx, effectEnhancement));
                idx++;
            }

            return effectDatas.ToArray();
        }

        public static int GetVitalBoost(ItemProperties itemProps, Vitals vital)
        {
            return itemProps?.VitalEnhancements?.ElementAtOrDefault((int)vital) ?? 0;
        }

        public static bool SharesPropsWith(ItemProperties srcProps, ItemProperties otherProps)
        {
            if (otherProps == default)
            {
                return false;
            }

            for (var i = 0; i < srcProps.StatModifiers.Length; i++)
            {
                if (otherProps.StatModifiers[i] != srcProps.StatModifiers[i])
                {
                    return false;
                }
            }

            for (var i = 0; i < srcProps.StatEnhancements.Length; i++)
            {
                if (otherProps.StatEnhancements[i] != srcProps.StatEnhancements[i])
                {
                    return false;
                }
            }


            for (var i = 0; i < srcProps.VitalEnhancements.Length; i++)
            {
                if (otherProps.VitalEnhancements[i] != srcProps.VitalEnhancements[i])
                {
                    return false;
                }
            }

            for (var i = 0; i < srcProps.EffectEnhancements.Length; i++)
            {
                if (otherProps.EffectEnhancements[i] != srcProps.EffectEnhancements[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
