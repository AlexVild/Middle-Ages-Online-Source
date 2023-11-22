using System;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Localization;
using System.Linq;
using Intersect.Utilities;
using Intersect.Client.Interface.Game.DescriptionWindows.Components;
using Intersect.Client.Utilities;
using System.Collections.Generic;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Network.Packets.Server;
using Intersect.Client.Items;

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

            SetupStudyInfo();

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
                SetupEnhancementThresholdBreakdown();
                AddDivider();
                SetupProcSpellInfo();
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

                // Level no longer important?
                baseRows.AddKeyValueRow($"{wepTypeName} type",
                    string.Empty,
                    CustomColors.ItemDesc.WeaponType,
                    Color.White);

                /*baseRows.AddKeyValueRow($"Lvl. {level} {wepTypeName} type",
                    string.Empty,
                    CustomColors.ItemDesc.WeaponType,
                    Color.White);*/
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

            if (Interface.GameUi.CraftingWindowOpen() && mItem.CraftWeaponExp > 0)
            {
                AddDivider();
                var craftExpRow = AddRowContainer();
                craftExpRow.AddKeyValueRow("Craft Weapon EXP:", mItem.CraftWeaponExp.ToString(), CustomColors.ItemDesc.Notice, CustomColors.ItemDesc.Notice);
            }
            else if (Interface.GameUi.DeconstructorWindow.IsVisible() && mItem.CraftWeaponExp > 0)
            {
                AddDivider();
                var craftExpRow = AddRowContainer();
                var exp = DeconstructionUtils.GetDeconExp(mItem.CraftWeaponExp);
                craftExpRow.AddKeyValueRow("Decon. Weapon EXP:", exp.ToString(), CustomColors.ItemDesc.Notice, CustomColors.ItemDesc.Notice);
            }

            SetupDamageEstimations(baseRows);

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

            if (mItem.ReplaceCastingComponents)
            {
                AddDivider();
                var castingComponentRow = AddRowContainer();
                castingComponentRow.AddKeyValueRow("Spellcast Focus", "", CustomColors.ItemDesc.Special, StatValueColor);

                castingComponentRow.SizeToChildren(true, true);
            }

            
        }

        private void ApplyStats(ItemBase descriptor, ItemProperties itemProperties, int[] baseStats, ref int[] stats)
        {
            Array.Copy(baseStats, stats, (int)Stats.StatCount);
            if (descriptor == null || itemProperties == null)
            {
                return;
            }

            var statIdx = 0;
            var enhancementBoosts = ItemInstanceHelper.GetStatBoosts(itemProperties);
            foreach (var stat in stats)
            {
                var percentBoost = 0;
                if (descriptor.PercentageStatsGiven[statIdx] != 0)
                {
                    percentBoost = (int)Math.Ceiling(stat * (descriptor.PercentageStatsGiven[statIdx] / 100f));
                }
                stats[statIdx] += descriptor.StatsGiven[statIdx] + percentBoost + enhancementBoosts[statIdx];
                statIdx++;
            }
        }

        private void SetupDamageEstimations(RowContainerComponent estimationRows)
        {
            var emptyStats = new int[(int)Stats.StatCount];

            var currentItemStats = new int[(int)Stats.StatCount];
            var newItemStats = new int[(int)Stats.StatCount];
            var unarmedStats = new int[(int)Stats.StatCount];
            Array.Copy(Globals.Me.Stat, unarmedStats, (int)Stats.StatCount);
            var statIdx = 0;

            // Calculate stats without a weapon equipped
            if (EquippedItem != default)
            {
                var statBoosts = ItemInstanceHelper.GetStatBoosts(EquippedItem.ItemProperties);
                foreach (var stat in unarmedStats)
                {
                    unarmedStats[statIdx] -= (EquippedItemDesc?.StatsGiven[statIdx] ?? 0 ) + statBoosts[statIdx];
                    statIdx++;
                }
            }

            // Now, apply the comparing weapon's stats to the unarmed stats so we can compare the two
            ApplyStats(EquippedItemDesc, EquippedItem?.ItemProperties, unarmedStats, ref currentItemStats);
            ApplyStats(mItem, mItemProperties, unarmedStats, ref newItemStats);

            CombatUtilities.CalculateDamage(mItem.AttackTypes, 1.0, 100, newItemStats, emptyStats, out var maxHit);
            CombatUtilities.CalculateDamage(EquippedItem?.Base?.AttackTypes ?? new List<AttackTypes>(), 1.0, 100, currentItemStats, emptyStats, out var compareMaxHit);

            var hits = 1;
            var piercesTargets = false;
            if (mItem.Projectile != default)
            {
                hits = mItem.Projectile.Quantity;
                piercesTargets = mItem.Projectile.PierceTarget;
            }

            var atkSpeeds = GetAttackSpeedComparison();

            var thisDps = maxHit * (1000f / (atkSpeeds.Item1));
            var compareDps = compareMaxHit * (1000f / atkSpeeds.Item2);

            AddEquipmentRow(estimationRows, "Max Hit", maxHit, compareMaxHit, false, StatLabelColor, StatValueColor);
            AddEquipmentRow(estimationRows, "Damage Per Second", thisDps, compareDps, false, StatLabelColor, StatValueColor);

            if (hits > 1)
            {
                estimationRows.AddKeyValueRow("Multi-Attack", hits.ToString(), CustomColors.ItemDesc.Special, StatValueColor);
            }

            if (piercesTargets)
            {
                estimationRows.AddKeyValueRow("Pierces Targets", string.Empty, CustomColors.ItemDesc.Special, StatValueColor);
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
                    StatValueColor,
                    unit: "%");
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

        private Tuple<int, int> GetAttackSpeedComparison()
        {
            int comparedAttackSpeed;
            if (EquippedItem == null || EquippedItemDesc == null)
            {
                var cls = ClassBase.Get(Globals.Me.Class);
                comparedAttackSpeed = cls?.AttackSpeedValue ?? 1000;
            }
            else
            {
                comparedAttackSpeed = EquippedItemDesc?.AttackSpeedValue ?? 1000;
            }
            var itemAtkSpeed = mItem.AttackSpeedValue;
            var currentSwiftness = Globals.Me.GetBonusEffect(EffectType.Swiftness);
            var itemSwiftness = ItemInstanceHelper.GetEffectBoost(mItemProperties, EffectType.Swiftness) + mItem.GetEffectPercentage(EffectType.Swiftness);
            if (currentSwiftness != 0 || itemSwiftness != 0)
            {
                var equippedSwiftness = 0;
                if (EquippedItem != null && EquippedItemDesc != null)
                {
                    equippedSwiftness = ItemInstanceHelper.GetEffectBoost(EquippedItem.ItemProperties, EffectType.Swiftness) + EquippedItem.Base.GetEffectPercentage(EffectType.Swiftness);
                }

                var swiftDiff = itemSwiftness - equippedSwiftness;

                if (itemSwiftness == equippedSwiftness)
                {
                    var swiftMod = (100 - currentSwiftness) / 100f;
                    itemAtkSpeed = (int)Math.Floor(itemAtkSpeed * swiftMod);
                    comparedAttackSpeed = (int)Math.Floor(comparedAttackSpeed * swiftMod);
                }
                else
                {
                    var newSwiftMod = (100 - currentSwiftness - swiftDiff) / 100f;
                    var currSwiftMod = (100 - currentSwiftness) / 100f;
                    itemAtkSpeed = (int)Math.Floor(itemAtkSpeed * newSwiftMod);
                    comparedAttackSpeed = (int)Math.Floor(comparedAttackSpeed * currSwiftMod);
                }
            }

            return new Tuple<int, int>(itemAtkSpeed, comparedAttackSpeed);
        }

        private void SetupAttackSpeedBreakdown()
        {
            // Don't handle non-static, MAO doesn't do that
            if (mItem.AttackSpeedModifier != 1)
            {
                return;
            }

            int comparedAttackSpeed;
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

        private void SetupEnhancementThresholdBreakdown()
        {
            int comparedThresh = EquippedItemDesc?.EnhancementThreshold ?? 0;
            
            var enhancementRows = AddRowContainer();
            AddEquipmentRow(enhancementRows,
                Strings.ItemDescription.EnhancementThreshold,
                mItem.EnhancementThreshold,
                comparedThresh,
                false,
                StatLabelColor,
                StatValueColor);

            enhancementRows.SizeToChildren(true, true);
        }

        private void SetupProcSpellInfo()
        {
            if (mItem.ProcSpellId == default)
            {
                return;
            }

            var spell = SpellBase.Get(mItem.ProcSpellId);
            if (mItem.ProcChance > 0 && spell != default)
            {
                var procRow = AddRowContainer();

                procRow.AddKeyValueRow("   Spell on Hit", "", StatHeaderColor, Color.White);

                procRow.AddKeyValueRow("Spell:", spell.Name, StatLabelColor, StatValueColor);
                procRow.AddKeyValueRow("Chance:", $"{mItem.ProcChance.ToString("N2")}%", StatLabelColor, StatValueColor);
                
                procRow.SizeToChildren();
            }
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
                    Strings.ItemDescription.VitalPercentages[(int)vital],
                    mItem.PercentageVitalsGiven[(int)vital],
                    EquippedItemDesc?.PercentageVitalsGiven[(int)vital],
                    false,
                    StatLabelColor,
                    StatValueColor,
                    unit: "%");
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

            if (!double.IsNaN(diff))
            {
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
            }

            Banded = !Banded;
        }

        private void SetupStudyInfo()
        {
            if ((Interface.GameUi.CraftingWindowOpen() || Interface.GameUi.DeconstructorWindow.IsVisible()) && mItem.StudyChance > 0)
            {
                var enhancement = EnhancementDescriptor.GetName(mItem.StudyEnhancement);

                var leftText = mItem.StudyEnhancement != Guid.Empty ? Strings.ItemDescription.StudyOpportunity : Strings.ItemDescription.CosmeticOpportunity;
                var rightText = mItem.StudyEnhancement != Guid.Empty ? Strings.ItemDescription.StudyOpportunityText.ToString(enhancement, mItem.StudyChance.ToString("N2")) :
                    Strings.ItemDescription.CosmeticChance.ToString(mItem.StudyChance.ToString("N2"));

                var unlockedText = mItem.StudyEnhancement != Guid.Empty ? Strings.ItemDescription.Studied : Strings.ItemDescription.CosmeticKnown;

                var studyRow = AddRowContainer();

                // Enhancement study text
                if (mItem.StudyEnhancement != Guid.Empty && !Globals.Me.KnownEnhancements.Contains(mItem.StudyEnhancement))
                {
                    // commenting out
                    //studyRow.AddKeyValueRow(leftText, rightText, CustomColors.ItemDesc.Special, CustomColors.ItemDesc.Special);
                }
                // Cosmetic unlock text
                else if (mItem.StudyChance > 0)
                {
                    if (mItem.StudyEnhancement == Guid.Empty && (!CharacterCosmeticsPanelController.UnlockedCosmetics.TryGetValue(mItem.EquipmentSlot, out var cosmeticsInSlot) || !cosmeticsInSlot.Contains(mItem.Id)))
                    {
                        studyRow.AddKeyValueRow(leftText, rightText, CustomColors.ItemDesc.Special, CustomColors.ItemDesc.Special);
                    }
                    else
                    {
                        studyRow.AddKeyValueRow(unlockedText, "", CustomColors.ItemDesc.Special, CustomColors.ItemDesc.Special);
                    }
                }

                studyRow.SizeToChildren(true, true);
                
                AddDivider();
            }
        }

        private bool DisplayVitalBonuses()
        {
            if (mItem.VitalsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (mItem.PercentageVitalsGiven.Any(v => v != 0))
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

            if (EquippedItem == null || EquippedItemDesc == null)
            {
                return false;
            }

            if (EquippedItemDesc.VitalsGiven.Any(v => v != 0))
            {
                return true;
            }

            if (EquippedItemDesc.PercentageVitalsGiven.Any(v => v != 0))
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

            if (EquippedItem == null || EquippedItemDesc == null)
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

            if (EquippedItem == null || EquippedItemDesc == null)
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
