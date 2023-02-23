using System;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Localization;
using System.Linq;
using Intersect.Utilities;
using Intersect.Client.Interface.Game.DescriptionWindows.Components;
using Intersect.Client.Utilities;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public partial class ItemDescriptionWindow : DescriptionWindowBase
    {
        readonly Color _statLabelColor = CustomColors.ItemDesc.Muted;
        readonly Color _statLabelColorBanded = CustomColors.ItemDesc.MutedBanded;

        public bool Banded = false;

        Color StatLabelColor => Banded ? _statLabelColorBanded : _statLabelColor;
        
        readonly Color StatValueColor = CustomColors.ItemDesc.Primary;
        readonly Color StatHeaderColor = Color.White;

        protected void SetupEquipmentInfo()
        {
            AddDivider();
            if (mItem == default)
            {
                return;
            }

            if (mItem.EquipmentSlot == Options.WeaponIndex)
            {
                SetupWeaponInfo();
                AddDivider();
            }

            if (DisplayVitalBonuses())
            {
                SetupVitalBreakdown();
                AddDivider();
            }

            if (DisplayStatBonuses())
            {
                SetupStatBreakdown();
                AddDivider();
            }

            if (mItem.EquipmentSlot == Options.WeaponIndex)
            {
                SetupCritBreakdown();
                AddDivider();
                SetupAttackSpeedBreakdown();
                AddDivider();
            }

            if (DisplayEffectBonuses())
            {
                SetupBonusBreakdown();
                AddDivider();
            }
        }

        private void SetupWeaponInfo()
        {
            AddDivider();

            var baseRows = AddRowContainer();
            // Special attack
            if (mItem.SpecialAttack.SpellId != default)
            {
                baseRows.AddKeyValueRow("Special Attack:",
                    SpellBase.GetName(mItem.SpecialAttack.SpellId),
                    CustomColors.ItemDesc.Special,
                    CustomColors.ItemDesc.Special);
            }

            // Display weapon type levels
            foreach (var kv in mItem.MaxWeaponLevels)
            {
                var weaponTypeId = kv.Key;
                var level = kv.Value;

                var wepTypeName = WeaponTypeDescriptor.Get(weaponTypeId)?.VisibleName ?? "NOT FOUND";

                baseRows.AddKeyValueRow($"Lvl. {level} {wepTypeName} type",
                    string.Empty,
                    CustomColors.ItemDesc.WeaponType,
                    Color.White);
            }

            var idx = 0;
            foreach (var attackType in mItem.AttackTypes)
            {
                if (idx == 0)
                {
                    baseRows.AddKeyValueRow("Damage Types:", attackType.GetDescription(), StatLabelColor, StatValueColor);
                }
                else
                {
                    baseRows.AddKeyValueRow(string.Empty, attackType.GetDescription(), StatLabelColor, StatValueColor);
                }
                idx++;
            }

            baseRows.SizeToChildren(true, true);

            if (mItem.Tool >= 0)
            {
                AddDivider();
                var toolRows = AddRowContainer();
                var toolName = Options.ToolTypes[mItem.Tool];
                toolRows.AddKeyValueRow($"   {toolName}", "", StatHeaderColor, Color.White);

                // Base damage is now just used for harvesting
                AddEquipmentRow(toolRows, "Harvest Power:", mItem.Damage, EquippedItemDesc?.Damage, false, StatLabelColor, StatValueColor);

                toolRows.SizeToChildren(true, true);
            }
        }

        private void SetupStatBreakdown()
        {
            var statRows = AddRowContainer();

            statRows.AddKeyValueRow("   Stats", "", StatHeaderColor, Color.White);
            // Display stat upgrades
            foreach (Stats stat in Enum.GetValues(typeof(Stats)))
            {
                if (stat == Stats.StatCount)
                {
                    continue;
                }

                int baseStatValue = mItem.StatsGiven.ElementAtOrDefault((int)stat);
                int? baseCompareStatValue = EquippedItemDesc?.StatsGiven.ElementAtOrDefault((int)stat) ?? 0;

                AddEquipmentRow(statRows,
                    Strings.ItemDescription.Stats[(int)stat],
                    baseStatValue,
                    baseCompareStatValue,
                    false,
                    StatLabelColor,
                    StatValueColor,
                    ItemInstanceHelper.GetStatBoost(mItemProperties, stat),
                    ItemInstanceHelper.GetStatBoost(EquippedItem?.ItemProperties, stat));

                AddEquipmentRow(statRows,
                    Strings.ItemDescription.StatPercentages[(int)stat],
                    mItem.PercentageStatsGiven[(int)stat],
                    EquippedItemDesc?.PercentageStatsGiven[(int)stat],
                    false,
                    StatLabelColor,
                    StatValueColor);
            }

            statRows.SizeToChildren(true, true);
        }

        private void SetupCritBreakdown()
        {
            var critRows = AddRowContainer();
            var compareCritChance = 0;
            var compareCritMulti = 1.0;
            if (EquippedItem == null)
            {
                var cls = ClassBase.Get(Globals.Me.Class);

                if (cls != default)
                {
                    compareCritChance = cls.CritChance;
                    compareCritMulti = cls.CritMultiplier;
                }
            }
            else
            {
                compareCritChance = EquippedItemDesc?.CritChance ?? 0;
                compareCritMulti = EquippedItemDesc?.CritMultiplier ?? 0.0;
            }
            AddEquipmentRow(critRows,
                Strings.ItemDescription.CritChance,
                mItem.CritChance,
                compareCritChance,
                false,
                StatLabelColor,
                StatValueColor,
                unit: "%");
            AddEquipmentRow(critRows,
                    Strings.ItemDescription.CritMultiplier,
                    mItem.CritMultiplier,
                    compareCritMulti,
                    false,
                    StatLabelColor,
                    StatValueColor,
                    unit: "x");
            if (mItem.CanBackstab || (EquippedItemDesc?.CanBackstab ?? false))
            {
                AddEquipmentRow(critRows,
                    Strings.ItemDescription.BackstabMultiplier,
                    mItem.CanBackstab ? mItem.BackstabMultiplier : 0.0,
                    (EquippedItemDesc?.CanBackstab ?? false) ? EquippedItemDesc?.BackstabMultiplier ?? 0.0 : 0.0,
                    false,
                    StatLabelColor,
                    StatValueColor,
                    unit: "x");
            }

            critRows.SizeToChildren(true, true);
        }

        private void SetupAttackSpeedBreakdown()
        {
            // Don't handle non-static, MAO doesn't do that
            if (mItem.AttackSpeedModifier != 1)
            {
                return;
            }

            var comparedAttackSpeed = 1000;
            if (EquippedItem == null)
            {
                var cls = ClassBase.Get(Globals.Me.Class);
                comparedAttackSpeed = cls?.AttackSpeedValue ?? 1000;
            }
            else
            {
                comparedAttackSpeed = EquippedItemDesc?.AttackSpeedValue ?? 1000;
            }
         
            var atkSpeedRows = AddRowContainer();
            AddEquipmentRow(atkSpeedRows,
                Strings.ItemDescription.AttackSpeed,
                mItem.AttackSpeedValue,
                comparedAttackSpeed,
                true,
                StatLabelColor,
                StatValueColor,
                unit: "ms");

            atkSpeedRows.SizeToChildren(true, true);
        }

        private void SetupBonusBreakdown()
        {
            var effectRows = AddRowContainer();

            effectRows.AddKeyValueRow("   Effects", "", StatHeaderColor, Color.White);
            foreach (EffectType effect in Enum.GetValues(typeof(EffectType)))
            {
                int baseStatValue = mItem.GetEffectPercentage(effect);
                int? baseCompareStatValue = EquippedItemDesc?.GetEffectPercentage(effect) ?? 0;

                var lowerIsBetter = BonusEffectHelper.LowerIsBetterEffects.Contains(effect);

                AddEquipmentRow(effectRows,
                    Strings.ItemDescription.BonusEffects[(int)effect],
                    baseStatValue,
                    baseCompareStatValue,
                    lowerIsBetter,
                    StatLabelColor,
                    StatValueColor,
                    ItemInstanceHelper.GetEffectBoost(mItemProperties, effect),
                    ItemInstanceHelper.GetEffectBoost(EquippedItem?.ItemProperties, effect),
                    "%");
            }

            var strafeBonus = (mItem.StrafeBonus + mItem.BackstepBonus) / 2f;
            var equippedStrafeBonus = EquippedItemDesc == null ? 0 : (EquippedItemDesc.StrafeBonus + EquippedItemDesc.BackstepBonus) / 2f;
            
            AddEquipmentRow(effectRows,
                  Strings.ItemDescription.StrafeSpeedBonus,
                  strafeBonus,
                  equippedStrafeBonus,
                  false,
                  StatLabelColor,
                  StatValueColor,
                  unit: "%");

            effectRows.SizeToChildren(true, true);
        }

        private void SetupVitalBreakdown()
        {
            var vitalRows = AddRowContainer();

            // Display vital upgrades
            vitalRows.AddKeyValueRow("   Vital Bonuses", "", StatHeaderColor, Color.White);
            foreach (Vitals vital in Enum.GetValues(typeof(Vitals)))
            {
                if (vital == Vitals.VitalCount)
                {
                    continue;
                }

                int baseStatValue = mItem.VitalsGiven.ElementAtOrDefault((int)vital);
                int? baseCompareStatValue = EquippedItemDesc?.VitalsGiven.ElementAtOrDefault((int)vital) ?? 0;

                AddEquipmentRow(vitalRows,
                    Strings.ItemDescription.Vitals[(int)vital],
                    baseStatValue,
                    baseCompareStatValue,
                    false,
                    StatLabelColor,
                    StatValueColor,
                    ItemInstanceHelper.GetVitalBoost(mItemProperties, vital),
                    ItemInstanceHelper.GetVitalBoost(EquippedItem?.ItemProperties, vital));
                
                AddEquipmentRow(vitalRows,
                    Strings.ItemDescription.Vitals[(int)vital],
                    mItem.PercentageVitalsGiven[(int)vital],
                    EquippedItemDesc?.PercentageVitalsGiven[(int)vital],
                    false,
                    StatLabelColor,
                    StatValueColor);
            }
            foreach (Vitals vital in Enum.GetValues(typeof(Vitals)))
            {
                if (vital == Vitals.VitalCount)
                {
                    continue;
                }

                AddEquipmentRow(vitalRows,
                    Strings.ItemDescription.VitalRegens[(int)vital],
                    mItem.VitalsRegen[(int)vital],
                    EquippedItemDesc?.VitalsRegen[(int)vital],
                    false,
                    StatLabelColor,
                    StatValueColor,
                    unit: "%");
            }

            vitalRows.SizeToChildren(true, true);
        }

        private void AddEquipmentRow(RowContainerComponent rows,
            string statName,
            int stat,
            int? comparisonStatOption,
            bool lowerIsBetter,
            Color labelColor,
            Color defaultStatColor,
            int mod = 0,
            int compareMod = 0,
            string unit = "")
        {
            var statLowerColor = lowerIsBetter ? CustomColors.ItemDesc.Better : CustomColors.ItemDesc.Worse;
            var statHigherColor = lowerIsBetter ? CustomColors.ItemDesc.Worse : CustomColors.ItemDesc.Better;

            var comparisonStat = comparisonStatOption.GetValueOrDefault();

            // Don't bother displaying if neither our current equipment nor the new equipment has any value for this stat
            if (comparisonStat + compareMod == 0 && stat + mod == 0)
            {
                return;
            }

            var diff = (stat + mod) - (comparisonStat + compareMod);

            if (!ShowEnhancementBreakdown)
            {
                stat += mod;
            }

            var statString = $"{stat}";

            if (ShowEnhancementBreakdown)
            {
                if (Math.Sign(mod) > 0)
                {
                    statString = $"{stat}+{mod}{unit}";
                }
                else if (Math.Sign(mod) < 0)
                {
                    statString = $"{stat}{mod}{unit}";
                }
            }

            statName = statName.Replace(":", "");
            if (Math.Sign(diff) < 0)
            {
                rows.AddKeyValueRow(statName, $"{statString}{unit} ({diff}{unit})", labelColor, statLowerColor);
            }
            else if (Math.Sign(diff) > 0)
            {
                rows.AddKeyValueRow(statName, $"{statString}{unit} (+{diff}{unit})", labelColor, statHigherColor);
            }
            else
            {
                rows.AddKeyValueRow(statName, $"{statString}{unit}", labelColor, defaultStatColor);
            }

            Banded = !Banded;
        }

        private void AddEquipmentRow(RowContainerComponent rows,
            string statName,
            double stat,
            double? comparisonStatOption,
            bool lowerIsBetter,
            Color labelColor,
            Color defaultStatColor,
            int mod = 0,
            int compareMod = 0,
            string unit = "")
        {
            var statLowerColor = lowerIsBetter ? CustomColors.ItemDesc.Better : CustomColors.ItemDesc.Worse;
            var statHigherColor = lowerIsBetter ? CustomColors.ItemDesc.Worse : CustomColors.ItemDesc.Better;

            var comparisonStat = comparisonStatOption.GetValueOrDefault();

            // Don't bother displaying if neither our current equipment nor the new equipment has any value for this stat
            if (comparisonStat + compareMod == 0 && stat + mod == 0)
            {
                return;
            }

            var diff = (stat + mod) - (comparisonStat + compareMod);

            if (!ShowEnhancementBreakdown)
            {
                stat += mod;
            }

            var statString = $"{stat.ToString("N2")}";

            if (ShowEnhancementBreakdown)
            {
                if (Math.Sign(mod) > 0)
                {
                    statString = $"{stat.ToString("N2")}+{mod}{unit}";
                }
                else if (Math.Sign(mod) < 0)
                {
                    statString = $"{stat.ToString("N2")}{mod}{unit}";
                }
            }

            statName = statName.Replace(":", "");
            if (Math.Sign(diff) < 0)
            {
                rows.AddKeyValueRow(statName, $"{statString}{unit} ({diff.ToString("N2")}{unit})", labelColor, statLowerColor);
            }
            else if (Math.Sign(diff) > 0)
            {
                rows.AddKeyValueRow(statName, $"{statString}{unit} (+{diff.ToString("N2")}{unit})", labelColor, statHigherColor);
            }
            else
            {
                rows.AddKeyValueRow(statName, $"{statString}{unit}", labelColor, defaultStatColor);
            }

            Banded = !Banded;
        }

        private bool DisplayVitalBonuses()
        {
            if (mItem.VitalsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (mItem.VitalsRegen.Any(v => v != 0))
            {
                return true;
            }

            if (mItemProperties.VitalEnhancements.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItem == null)
            {
                return false;
            }

            if (EquippedItemDesc.VitalsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItemDesc.VitalsRegen.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItem.ItemProperties.VitalEnhancements.Any(v => v != 0))
            {
                return true;
            }

            return false;
        }

        private bool DisplayStatBonuses()
        {
            if (mItem.StatsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (mItemProperties.StatModifiers.Any(v => v != 0))
            {
                return true;
            }

            if (mItemProperties.StatEnhancements.Any(v => v != 0))
            {
                return true;
            }

            if (mItem.PercentageStatsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItem == null)
            {
                return false;
            }

            if (EquippedItemDesc.StatsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItem.ItemProperties.StatModifiers.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItem.ItemProperties.StatEnhancements.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItemDesc.PercentageStatsGiven.Any(v => v != 0))
            {
                return true;
            }

            return false;
        }

        private bool DisplayEffectBonuses()
        {
            if (mItem.Effects.Any(v => v.Percentage != 0))
            {
                return true;
            }

            if (mItemProperties.EffectEnhancements.Any(v => v != 0))
            {
                return true;
            }

            if (mItem.StrafeBonus != 0 || mItem.BackstepBonus != 0)
            {
                return true;
            }

            if (EquippedItem == null)
            {
                return false;
            }

            if (EquippedItemDesc.Effects.Any(v => v.Percentage != 0))
            {
                return true;
            }

            if (EquippedItem.ItemProperties.EffectEnhancements.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItemDesc.StrafeBonus != 0 || EquippedItemDesc.BackstepBonus != 0)
            {
                return true;
            }

            return false;
        }
    }
}
