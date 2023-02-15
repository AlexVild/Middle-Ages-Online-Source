using System;
using System.Collections.Generic;

using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Items;
using Intersect.Localization;
using System.Linq;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class ItemDescriptionWindow : DescriptionWindowBase
    {
        protected ItemBase mItem;

        protected int mAmount;

        protected ItemProperties mItemProperties;

        protected string mTitleOverride;

        protected string mValueLabel;

        protected string mDropChance;

        protected double mTableChance;

        protected SpellDescriptionWindow mSpellDescWindow;

        public ItemDescriptionWindow(
            ItemBase item,
            int amount,
            int x,
            int y,
            ItemProperties itemProperties,
            string titleOverride = "",
            string valueLabel = "",
            string dropChance = "",
            double tableChance = 0d
        ) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            mItem = item;
            mAmount = amount;
            mItemProperties = itemProperties;
            if (mItemProperties == null)
            {
                mItemProperties = new ItemProperties();
            }

            mTitleOverride = titleOverride;
            mValueLabel = valueLabel;
            mDropChance = dropChance;
            mTableChance = tableChance;

            GenerateComponents();
            SetupDescriptionWindow();

            // If a spell, also display the spell description!
            if (mItem.ItemType == ItemTypes.Spell && !mItem.QuickCast)
            {
                mSpellDescWindow = new SpellDescriptionWindow(mItem.SpellId, x, y);
            }
            if (mItem.ItemType == ItemTypes.Equipment && mItem.EquipmentSlot == Options.PrayerIndex)
            {
                mSpellDescWindow = new SpellDescriptionWindow(mItem.ComboSpellId, x, y);
            }
            if (mItem.SpecialAttack.SpellId != default)
            {
                mSpellDescWindow = new SpellDescriptionWindow(mItem.SpecialAttack.SpellId, x, y);
            }

            if (mSpellDescWindow != default)
            {
                x -= mSpellDescWindow.Container.Width + 4;
            }

            SetPosition(x, y);
        }

        protected void SetupDescriptionWindow()
        {
            if (mItem == null)
            {
                return;
            }

            // Set up our header information.
            SetupHeader();

            // Set up our item limit information.
            // SetupItemLimits(); - Alex: Didn't want

            // Display item drop chance if configured.
            if (mItem.DestroyOnInstanceChange)
            {
                var instanceItemDesc = AddDescription();
                instanceItemDesc.AddText(Strings.ItemDescription.DestroyOnInstance, CustomColors.ItemDesc.Special);
            }

            // if we have a description, set that up.
            if (!string.IsNullOrWhiteSpace(mItem.Description))
            {
                SetupDescription();
            }

            // if we have usage restrictions, display those
            if (mItem.RestrictionStrings.Count > 0)
            {
                SetupRestrictionInfo();
            }

            // Set up information depending on the item type.
            switch (mItem.ItemType)
            {
                case ItemTypes.Equipment:
                    SetupEquipmentInfo();
                    break;

                case ItemTypes.Consumable:
                    SetupConsumableInfo();
                    break;

                case ItemTypes.Spell:
                    SetupSpellInfo();
                    break;

                case ItemTypes.Bag:
                    SetupBagInfo();
                    break;
                
                case ItemTypes.Cosmetic:
                    SetupCosmeticInfo();
                    break;
            }

            // Set up additional information such as amounts and shop values.
            SetupExtraInfo();

            // Resize the container, correct the display and position our window.
            FinalizeWindow();
        }

        protected void SetupHeader()
        {
            // Create our header, but do not load our layout yet since we're adding components manually.
            var header = AddHeader();

            // Set up the icon, if we can load it.
            var tex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Item, mItem.Icon);
            if (tex != null)
            {
                header.SetIcon(tex, mItem.Color);
            }

            // Set up the header as the item name.
            CustomColors.Items.Rarities.TryGetValue(mItem.Rarity, out var rarityColor);

            var itemName = mItem.ItemType == ItemTypes.Cosmetic ? 
                string.IsNullOrEmpty(mItem.CosmeticDisplayName) ? 
                    mItem.Name : 
                    mItem.CosmeticDisplayName : 
                mItem.Name;

            var name = !string.IsNullOrWhiteSpace(mTitleOverride) ? mTitleOverride : itemName;
            header.SetTitle(name, rarityColor ?? Color.White);

            // Set up the description telling us what type of item this is.
            // if equipment, also list what kind.
            LocalizedString typeDesc;
            if (!string.IsNullOrEmpty(mItem.TypeDisplayOverride))
            {
                typeDesc = mItem.TypeDisplayOverride;
            }
            else
            {
                Strings.ItemDescription.ItemTypes.TryGetValue((int)mItem.ItemType, out typeDesc);
            }

            if (mItem.ItemType == ItemTypes.Equipment)
            {
                var equipSlot = Options.Equipment.Slots[mItem.EquipmentSlot];
                var extraInfo = equipSlot;
                if (mItem.EquipmentSlot == Options.WeaponIndex && mItem.TwoHanded)
                {
                    extraInfo = $"{Strings.ItemDescription.TwoHand} {equipSlot}";
                }
                header.SetSubtitle($"{typeDesc} - {extraInfo}", Color.White);
            }
            else
            {
                header.SetSubtitle(typeDesc, Color.White);
            }

            // Set up the item rarity label.
            Strings.ItemDescription.Rarity.TryGetValue(mItem.Rarity, out var rarityDesc);
            header.SetDescription(rarityDesc, rarityColor ?? Color.White);

            header.SizeToChildren(true, false);
        }

        protected void SetupItemLimits()
        {
            // Gather up what limitations apply to this item.
            var limits = new List<string>();
            if (!mItem.CanBank)
            {
                limits.Add(Strings.ItemDescription.Banked);
            }
            if (!mItem.CanGuildBank)
            {
                limits.Add(Strings.ItemDescription.GuildBanked);
            }
            if (!mItem.CanBag)
            {
                limits.Add(Strings.ItemDescription.Bagged);
            }
            if (!mItem.CanTrade)
            {
                limits.Add(Strings.ItemDescription.Traded);
            }
            if (!mItem.CanDrop)
            {
                limits.Add(Strings.ItemDescription.Dropped);
            }
            if (!mItem.CanSell)
            {
                limits.Add(Strings.ItemDescription.Sold);
            }

            // Do we have any limitations? If so, generate a display for it.
            if (limits.Count > 0)
            {
                // Add a divider.
                AddDivider();

                // Add the actual description.
                var description = AddDescription();

                // Commbine our lovely limitations to a single line and display them.
                description.AddText(Strings.ItemDescription.ItemLimits.ToString(string.Join(", ", limits)), Color.White);
            }
        }

        protected void SetupDescription()
        {
            // Add a divider.
            AddDivider();

            // Add the actual description.
            var description = AddDescription();
            description.AddText(Strings.ItemDescription.Description.ToString(mItem.Description), Color.White);
        }

        private int GetStatDifference(int statIndex)
        {
            var slot = mItem.EquipmentSlot;
            var flatStat = mItem.StatsGiven[statIndex];
            if (mItemProperties?.StatModifiers != null) 
            {
                flatStat += mItemProperties.StatModifiers[statIndex];
            }
            
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]];

                if (equippedItem != null && equippedItem.Base != null)
                {
                    var equippedStat = equippedItem.Base.StatsGiven[statIndex];
                    if (equippedItem.ItemProperties?.StatModifiers != null)
                    {
                        equippedStat += equippedItem.ItemProperties.StatModifiers[statIndex];
                    }

                    return flatStat - equippedStat;
                }
                else
                {
                    return flatStat;
                }
            }
            else
            {
                return flatStat;
            }
        }

        private int GetStatPercentageDifference(int statIndex)
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.PercentageStatsGiven[statIndex] - equippedItem.PercentageStatsGiven[statIndex];
                }
                else
                {
                    return mItem.PercentageStatsGiven[statIndex];
                }
            }
            else
            {
                return mItem.PercentageStatsGiven[statIndex];
            }
        }

        private (int stat, int percent) GetStatPercentageAndFlatDifference(int statIndex)
        {
            var slot = mItem.EquipmentSlot;
            var flatStat = mItem.StatsGiven[statIndex];
            if (mItemProperties?.StatModifiers != null)
            {
                flatStat += mItemProperties.StatModifiers[statIndex];
            }

            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]];

                if (equippedItem != null && equippedItem.Base != null)
                {
                    var equippedStat = equippedItem.Base.StatsGiven[statIndex];
                    if (equippedItem.ItemProperties?.StatModifiers != null)
                    {
                        equippedStat += equippedItem.ItemProperties.StatModifiers[statIndex];
                    }
                    var statDiff = flatStat - equippedStat;
                    var percentDiff = mItem.PercentageStatsGiven[statIndex] - equippedItem.Base.PercentageStatsGiven[statIndex];
                    return (statDiff, percentDiff);
                }
                else
                {
                    return (flatStat, mItem.PercentageStatsGiven[statIndex]);
                }
            }
            else
            {
                return (flatStat, mItem.PercentageStatsGiven[statIndex]);
            }
        }

        private int GetBaseDamageDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.Damage - equippedItem.Damage;
                }
                else
                {
                    return mItem.Damage;
                }
            }
            else
            {
                return mItem.Damage;
            }
        }

        private int GetScalingDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null && mItem.ScalingStat == equippedItem.ScalingStat)
                {
                    return mItem.Scaling - equippedItem.Scaling;
                }
                else
                {
                    return mItem.Scaling;
                }
            }
            else
            {
                return mItem.Scaling;
            }
        }

        private int GetCritChanceDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.CritChance - equippedItem.CritChance;
                }
                else
                {
                    return mItem.CritChance;
                }
            }
            else
            {
                return mItem.CritChance;
            }
        }

        private double GetCritMultiplierDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.CritMultiplier - equippedItem.CritMultiplier;
                }
                else
                {
                    return mItem.CritMultiplier;
                }
            }
            else
            {
                return mItem.CritMultiplier;
            }
        }

        private int GetAttackSpeedDifference()
        {
            var slot = mItem.EquipmentSlot;
            var retVal = 0f;
            var classAttack = ClassBase.Get(Globals.Me.Class).AttackSpeedValue;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    // Reversed - a lower value is better
                    retVal = equippedItem.AttackSpeedValue - mItem.AttackSpeedValue;
                }
                else
                {
                    retVal = classAttack - mItem.AttackSpeedValue;
                }
            }
            else
            {
                retVal = classAttack - mItem.AttackSpeedValue;
            }

            return (int)retVal;
        }

        private decimal GetBackstabDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null && equippedItem.CanBackstab)
                {
                    return (decimal)(mItem.BackstabMultiplier - equippedItem.BackstabMultiplier);
                }
                else
                {
                    return (decimal)mItem.BackstabMultiplier;
                }
            }
            else
            {
                return (decimal)mItem.BackstabMultiplier;
            }
        }

        private int GetBackstepBonusDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null && equippedItem.BackstepBonus > 0)
                {
                    return mItem.BackstepBonus - equippedItem.BackstepBonus;
                }
                else
                {
                    return mItem.BackstepBonus;
                }
            }
            else
            {
                return mItem.BackstepBonus;
            }
        }

        private int GetStrafeBonusDifference()
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null && equippedItem.StrafeBonus > 0)
                {
                    return mItem.StrafeBonus - equippedItem.StrafeBonus;
                }
                else
                {
                    return mItem.StrafeBonus;
                }
            }
            else
            {
                return mItem.StrafeBonus;
            }
        }

        private int GetVitalDifference(int vitalIndex)
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.VitalsGiven[vitalIndex] - equippedItem.VitalsGiven[vitalIndex];
                }
                else
                {
                    return mItem.VitalsGiven[vitalIndex];
                }
            }
            else
            {
                return mItem.VitalsGiven[vitalIndex];
            }
        }

        private int GetVitalRegenDifference(int vitalIndex)
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.VitalsRegen[vitalIndex] - equippedItem.VitalsRegen[vitalIndex];
                }
                else
                {
                    return mItem.VitalsRegen[vitalIndex];
                }
            }
            else
            {
                return mItem.VitalsRegen[vitalIndex];
            }
        }

        private int GetPercentageVitalDifference(int vitalIndex)
        {
            var slot = mItem.EquipmentSlot;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    return mItem.PercentageVitalsGiven[vitalIndex] - equippedItem.PercentageVitalsGiven[vitalIndex];
                }
                else
                {
                    return mItem.PercentageVitalsGiven[vitalIndex];
                }
            }
            else
            {
                return mItem.PercentageVitalsGiven[vitalIndex];
            }
        }

        private (int stat, int percent) GetVitalPercentageAndFlatDifference(int statIndex)
        {
            var slot = mItem.EquipmentSlot;

            if (Globals.Me.MyEquipment[slot] != -1)
            {
                var equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]].Base;

                if (equippedItem != null)
                {
                    var statDiff = mItem.VitalsGiven[statIndex] - equippedItem.VitalsGiven[statIndex];
                    var percentDiff = mItem.PercentageVitalsGiven[statIndex] - equippedItem.PercentageVitalsGiven[statIndex];
                    return (statDiff, percentDiff);
                }
                else
                {
                    return (mItem.VitalsGiven[statIndex], mItem.PercentageVitalsGiven[statIndex]);
                }
            }
            else
            {
                return (mItem.VitalsGiven[statIndex], mItem.PercentageVitalsGiven[statIndex]);
            }
        }

        private int GetBonusEffectDifference(ItemBase itm1, ItemBase itm2, EffectType effectType, bool inverse = false)
        {
            var itm1EffectVals = itm1?.GetEffectPercentage(effectType) ?? 0;
            var itm2EffectVals = itm2?.GetEffectPercentage(effectType) ?? 0;

            var multiplier = inverse ? -1 : 1;

            if (itm1 == null && itm2 == null)
            {
                return 0 * multiplier;
            }

            if (itm1 == null)
            {
                return itm2EffectVals * multiplier;
            }

            if (itm2 == null)
            {
                return itm2EffectVals * multiplier;
            }
            
            return (itm1EffectVals - itm2EffectVals) * multiplier;
        }

        protected void SetupEquipmentInfo()
        {
            var slot = mItem.EquipmentSlot;
            Item equippedItem = null;
            ItemBase equippedItemBase = null;
            var hasEquippedSlot = false;
            if (Globals.Me.MyEquipment[slot] != -1)
            {
                equippedItem = Globals.Me.Inventory[Globals.Me.MyEquipment[slot]];
                equippedItemBase = equippedItem.Base;

                hasEquippedSlot = equippedItem != null && equippedItemBase != null;
            }

            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            // Is this a weapon?
            if (mItem.EquipmentSlot == Options.WeaponIndex)
            {
                // Special attack
                if (mItem.SpecialAttack.SpellId != default)
                {
                    var attackName = SpellBase.GetName(mItem.SpecialAttack.SpellId);
                    rows.AddKeyValueRow("Special Attack:", attackName, CustomColors.ItemDesc.Special, CustomColors.ItemDesc.Special);
                }

                // Base Damage:
                DisplayKeyValueRowWithDifference(GetBaseDamageDifference(), Strings.ItemDescription.BaseDamage, mItem.Damage.ToString(), rows);

                // Damage Type:
                Strings.ItemDescription.DamageTypes.TryGetValue(mItem.DamageType, out var damageType);
                rows.AddKeyValueRow(Strings.ItemDescription.BaseDamageType, damageType);

                if (mItem.Scaling > 0)
                {
                    Strings.ItemDescription.Stats.TryGetValue(mItem.ScalingStat, out var stat);
                    rows.AddKeyValueRow(Strings.ItemDescription.ScalingStat, stat);
                    DisplayKeyValueRowWithDifference(GetScalingDifference(), Strings.ItemDescription.ScalingPercentage, Strings.ItemDescription.Percentage.ToString(mItem.Scaling), rows, "%");
                }

                // Crit Chance
                if (mItem.CritChance > 0)
                {
                    DisplayKeyValueRowWithDifference(GetCritChanceDifference(), Strings.ItemDescription.CritChance, Strings.ItemDescription.Percentage.ToString(mItem.CritChance), rows, "%");
                    DisplayKeyValueRowWithDifference(Decimal.Round((decimal)GetCritMultiplierDifference(), 2), Strings.ItemDescription.CritMultiplier, Strings.ItemDescription.Multiplier.ToString(mItem.CritMultiplier), rows, "x");
                }

                if (mItem.CanBackstab)
                {
                    DisplayKeyValueRowWithDifference(Decimal.Round(GetBackstabDifference(), 2), Strings.ItemDescription.BackstabMultiplier, Strings.ItemDescription.Multiplier.ToString(mItem.BackstabMultiplier), rows, "x");
                }

                if (mItem.StrafeBonus > 0 || equippedItem?.Base?.StrafeBonus > 0)
                {
                    DisplayKeyValueRowWithDifference(GetStrafeBonusDifference(), Strings.ItemDescription.StrafeSpeedBonus, Strings.ItemDescription.Percentage.ToString(mItem.StrafeBonus), rows, "%");
                }
                if (mItem.BackstepBonus > 0 || equippedItem?.Base?.BackstepBonus > 0)
                {
                    DisplayKeyValueRowWithDifference(GetBackstepBonusDifference(), Strings.ItemDescription.BackstepSpeedBonus, Strings.ItemDescription.Percentage.ToString(mItem.BackstepBonus), rows, "%");
                }

                // Attack Speed
                // Are we supposed to change our attack time based on a modifier?
                if (mItem.AttackSpeedModifier == 0)
                {
                    // No modifier, assuming base attack rate? We have to calculate the speed stat manually here though..!
                    var speed = Globals.Me.Stat[(int)Stats.Speed];

                    // Remove currently equipped weapon stats.. We want to create a fair display!
                    var weaponSlot = Globals.Me.MyEquipment[Options.WeaponIndex];
                    if (weaponSlot != -1)
                    {
                        var statBuffs = Globals.Me.Inventory[weaponSlot].ItemProperties.StatModifiers;
                        var weapon = ItemBase.Get(Globals.Me.Inventory[weaponSlot].ItemId);
                        if (weapon != null && statBuffs != null)
                        {
                            speed = (int) Math.Round(speed / ((100 + weapon.PercentageStatsGiven[(int)Stats.Speed]) / 100f));
                            speed -= weapon.StatsGiven[(int)Stats.Speed];
                            speed -= statBuffs[(int)Stats.Speed];
                        }
                    }

                    // Add current item's speed stats!
                    if (mItemProperties?.StatModifiers != null)
                    {
                        speed += mItem.StatsGiven[(int) Stats.Speed];
                        speed += mItemProperties.StatModifiers[(int) Stats.Speed];
                        speed += (int) Math.Floor(speed * (mItem.PercentageStatsGiven[(int)Stats.Speed] / 100f));
                    }

                    // Display the actual speed this weapon would have based off of our calculated speed stat.
                    rows.AddKeyValueRow(Strings.ItemDescription.AttackSpeed, Strings.ItemDescription.Seconds.ToString(Globals.Me.CalculateAttackTime(speed) / 1000f));
                }
                else if (mItem.AttackSpeedModifier == 1)
                {
                    // Static, so this weapon's attack speed.
                    DisplayKeyValueRowWithDifference(GetAttackSpeedDifference(), Strings.ItemDescription.AttackSpeed, Strings.ItemDescription.Seconds.ToString(mItem.AttackSpeedValue / 1000f), rows, "ms", "-");
                }
                else if (mItem.AttackSpeedModifier == 2)
                {
                    // Percentage based.
                    DisplayKeyValueRowWithDifference(GetAttackSpeedDifference(), Strings.ItemDescription.AttackSpeed, Strings.ItemDescription.Percentage.ToString(mItem.AttackSpeedValue), rows, "%", "-");
                }
            }

            // Vitals
            for (var i = 0; i < (int)Vitals.VitalCount; i++)
            {
                var equippedVitals = equippedItemBase?.VitalsGiven ?? new int[] { 0, 0 };
                var equippedVitalPercent = equippedItemBase?.PercentageVitalsGiven ?? new int[] { 0, 0 };
                var equippedVitalRegen = equippedItemBase?.VitalsRegen ?? new int[] { 0, 0 };

                if ((mItem.VitalsGiven[i] != 0 && mItem.PercentageVitalsGiven[i] != 0) || (equippedVitals[i] != 0 && equippedVitalPercent[i] != 0))
                {
                    var (vitalDiff, percentDiff) = GetVitalPercentageAndFlatDifference(i);
                    DisplayKeyValueRowWithDifferenceAndPercent(vitalDiff, percentDiff, Strings.ItemDescription.Vitals[i], Strings.ItemDescription.RegularAndPercentage.ToString(mItem.VitalsGiven[i], mItem.PercentageVitalsGiven[i]), rows);
                }
                else if (mItem.VitalsGiven[i] != 0 || equippedVitals[i] != 0)
                {
                    DisplayKeyValueRowWithDifference(GetVitalDifference(i), Strings.ItemDescription.Vitals[i], mItem.VitalsGiven[i].ToString(), rows);
                }
                else if (mItem.PercentageVitalsGiven[i] != 0 || equippedVitalPercent[i] != 0)
                {
                    DisplayKeyValueRowWithDifference(GetPercentageVitalDifference(i), Strings.ItemDescription.Vitals[i], Strings.ItemDescription.Percentage.ToString(mItem.PercentageVitalsGiven[i]), rows, "%");
                }

                // Regen
                if (mItem.VitalsRegen[i] != 0 || equippedVitalRegen[i] != 0)
                {
                    DisplayKeyValueRowWithDifference(GetVitalRegenDifference(i), Strings.ItemDescription.VitalRegens[i], mItem.VitalsRegen[i].ToString(), rows, "/s");
                }
            }

            // Stats
            if (mItemProperties?.StatModifiers != null)
            {
                for (var i = 0; i < (int)Stats.StatCount; i++)
                {
                    // bcause we want to separate the stat buff from the stat in the display
                    var stat = mItem.StatsGiven[i];
                    var statString = stat.ToString();

                    if (Math.Sign(mItemProperties.StatModifiers[i]) == -1)
                    {
                        if (stat == 0)
                        {
                            statString = Strings.ItemDescription.StatAndBuff.ToString("", "", mItemProperties.StatModifiers[i]);
                        }
                        else
                        {
                            statString = Strings.ItemDescription.StatAndBuff.ToString(stat, "", mItemProperties.StatModifiers[i]);
                        }
                        
                    }
                    else if (Math.Sign(mItemProperties.StatModifiers[i]) == 1)
                    {
                        if (stat == 0)
                        {
                            statString = Strings.ItemDescription.StatAndBuff.ToString("", "+", mItemProperties.StatModifiers[i]);
                        }
                        else
                        {
                            statString = Strings.ItemDescription.StatAndBuff.ToString(stat, "+", mItemProperties.StatModifiers[i]);
                        }
                    }

                    var calculatedStat = stat + mItemProperties.StatModifiers[i];

                    if (calculatedStat != 0 && mItem.PercentageStatsGiven[i] != 0)
                    {
                        var (flatStatDiff, percentStatDiff) = GetStatPercentageAndFlatDifference(i);
                        DisplayKeyValueRowWithDifferenceAndPercent(flatStatDiff, percentStatDiff, Strings.ItemDescription.StatCounts[i], Strings.ItemDescription.RegularAndPercentage.ToString(statString, mItem.PercentageStatsGiven[i]), rows);
                    }
                    else if (calculatedStat != 0)
                    {
                        DisplayKeyValueRowWithDifference(GetStatDifference(i), Strings.ItemDescription.StatCounts[i], statString.ToString(), rows);
                    }
                    else if (mItem.PercentageStatsGiven[i] != 0)
                    {
                        DisplayKeyValueRowWithDifference(GetStatPercentageDifference(i), Strings.ItemDescription.StatCounts[i], Strings.ItemDescription.Percentage.ToString(mItem.PercentageStatsGiven[i]), rows, "%");
                    }
                    // because we want to display stats that the highlighted item doesn't have that our equipment does
                    else if (equippedItem != null && equippedItem.Base != null)
                    {
                        var equippedStat = equippedItem.Base.StatsGiven[i];
                        var calcualtedEquippedStat = equippedStat + equippedItem.ItemProperties.StatModifiers[i];
                        var equippedStatString = "0";

                        var equippedPerecentage = equippedItem.Base.PercentageStatsGiven[i];
                        if (calcualtedEquippedStat != 0 && equippedPerecentage != 0)
                        {
                            var (flatStatDiff, percentStatDiff) = GetStatPercentageAndFlatDifference(i);
                            DisplayKeyValueRowWithDifferenceAndPercent(flatStatDiff, percentStatDiff, Strings.ItemDescription.StatCounts[i], Strings.ItemDescription.RegularAndPercentage.ToString(equippedStatString, mItem.PercentageStatsGiven[i]), rows);
                        }
                        else if (calcualtedEquippedStat != 0)
                        {
                            DisplayKeyValueRowWithDifference(GetStatDifference(i), Strings.ItemDescription.StatCounts[i], equippedStatString.ToString(), rows);
                        }
                        else if (equippedPerecentage != 0)
                        {
                            DisplayKeyValueRowWithDifference(GetStatPercentageDifference(i), Strings.ItemDescription.StatCounts[i], Strings.ItemDescription.Percentage.ToString(mItem.PercentageStatsGiven[i]), rows, "%");
                        }
                    }
                }
            }

            // Bonus Effect

            // Collect all bonus effects from each item
            var effects = new List<EffectData>();
            if (mItem != null)
            {
                effects.AddRange(mItem.Effects.FindAll(effect => effect.Type != EffectType.None));
            }
            if (equippedItemBase != null)
            {
                effects.AddRange(equippedItemBase.Effects.FindAll(effect => effect.Type != EffectType.None));
            }
            if (effects.Count > 0)
            {
                List<EffectType> typesDisplayed = new List<EffectType>();
                foreach(var effect in effects)
                {
                    if (typesDisplayed.Contains(effect.Type))
                    {
                        continue;
                    }
                    // Logic to make sure we don't show the same thing twice
                    typesDisplayed.Add(effect.Type);

                    if (mItem.Effects.Find(newEffect => newEffect.Type == effect.Type) != default)
                    {
                        var amt = mItem.GetEffectPercentage(effect.Type);
                        DisplayKeyValueRowWithDifference(GetBonusEffectDifference(mItem, equippedItemBase, effect.Type), Strings.ItemDescription.BonusEffects[(int)effect.Type], Strings.ItemDescription.Percentage.ToString(amt), rows, "%");
                    }
                    else
                    {
                        // If we're _LOSING_ an effect, display it in the inverse
                        DisplayKeyValueRowWithDifference(GetBonusEffectDifference(equippedItemBase, mItem, effect.Type, true), Strings.ItemDescription.BonusEffects[(int)effect.Type], Strings.ItemDescription.Percentage.ToString(0), rows, "%");
                    }
                }
            }
            
            // Resize the container.
            rows.SizeToChildren(true, true);
        }

        private void DisplayKeyValueRowWithDifference(int statDiff, string keyString, string valueString, Components.RowContainerComponent rows, string unit = "", string sign = "+")
        {
            if (statDiff != 0)
            {
                if (Math.Sign(statDiff) > 0)
                {
                    rows.AddKeyValueRow(keyString, $"{valueString} ({sign}{statDiff.ToString()}{unit})", CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Better);
                }
                else
                {
                    rows.AddKeyValueRow(keyString, $"{valueString} ({statDiff.ToString()}{unit})", CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Worse);
                }
            }
            else
            {
                rows.AddKeyValueRow(keyString, $"{valueString}");
            }
        }

        private void DisplayKeyValueRowWithDifferenceAndPercent(int statDiff, int percentDiff, string keyString, string valueString, Components.RowContainerComponent rows)
        {
            string statString;
            string percentString;

            if (Math.Sign(percentDiff) > 0)
            {
                percentString = $"+{percentDiff}%";
            }
            else if (Math.Sign(percentDiff) == 0)
            {
                percentString = $"";
            }
            else
            {
                percentString = $"-{percentDiff}%";
            }

            if (Math.Sign(statDiff) > 0)
            {
                statString = $"+{statDiff}%";
            }
            else if (Math.Sign(statDiff) == 0)
            {
                statString = $"";
            }
            else
            {
                statString = $"-{statDiff}%";
            }

            if (statDiff != 0)
            {
                rows.AddKeyValueRow(keyString, $"{valueString} ({statString}, {percentString})");
            }
            else
            {
                rows.AddKeyValueRow(keyString, $"{valueString}");
            }
        }

        private void DisplayKeyValueRowWithDifference(decimal statDiff, string keyString, string valueString, Components.RowContainerComponent rows, string unit = "")
        {
            if (statDiff != 0)
            {
                if (Math.Sign(statDiff) > 0)
                {
                    rows.AddKeyValueRow(keyString, $"{valueString} (+{statDiff.ToString()}{unit})", CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Better);
                }
                else
                {
                    rows.AddKeyValueRow(keyString, $"{valueString} ({statDiff.ToString()}{unit})", CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Worse);
                }
            }
            else
            {
                rows.AddKeyValueRow(keyString, $"{valueString}");
            }
        }

        protected void SetupConsumableInfo()
        {
            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            // Consumable data.
            if (mItem.Consumable != null)
            {
                if (mItem.Consumable.Value > 0 && mItem.Consumable.Percentage > 0)
                {
                    rows.AddKeyValueRow(Strings.ItemDescription.ConsumableTypes[(int) mItem.Consumable.Type], Strings.ItemDescription.RegularAndPercentage.ToString(mItem.Consumable.Value, mItem.Consumable.Percentage));
                }
                else if (mItem.Consumable.Value > 0)
                {
                    rows.AddKeyValueRow(Strings.ItemDescription.ConsumableTypes[(int)mItem.Consumable.Type], mItem.Consumable.Value.ToString());
                }
                else if (mItem.Consumable.Percentage > 0)
                {
                    rows.AddKeyValueRow(Strings.ItemDescription.ConsumableTypes[(int)mItem.Consumable.Type], Strings.ItemDescription.Percentage.ToString(mItem.Consumable.Percentage));
                }
            }

            // Resize and position the container.
            rows.SizeToChildren(true, true);
        }

        protected void SetupSpellInfo()
        {
            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            // Spell data.
            if (mItem.Spell != null)
            {
                if (mItem.QuickCast)
                {
                    rows.AddKeyValueRow(Strings.ItemDescription.CastSpell.ToString(mItem.Spell.Name), string.Empty);
                }
                else
                {
                    rows.AddKeyValueRow(Strings.ItemDescription.TeachSpell.ToString(mItem.Spell.Name), string.Empty);
                }

                if (mItem.SingleUse)
                {
                    rows.AddKeyValueRow(Strings.ItemDescription.SingleUse, string.Empty);
                }
            }

            // Resize and position the container.
            rows.SizeToChildren(true, true);
        }

        protected void SetupBagInfo()
        {
            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            // Bag data.
            rows.AddKeyValueRow(Strings.ItemDescription.BagSlots, mItem.SlotCount.ToString());

            // Resize and position the container.
            rows.SizeToChildren(true, true);
        }

        protected void SetupExtraInfo()
        {
            // Our list of data to add, should we need to.
            var data = new List<Tuple<string, string>>();

            // Display our amount, but only if we are stackable and have more than one.
            if (mItem.IsStackable && mAmount > 1)
            {
                data.Add(new Tuple<string, string>(Strings.ItemDescription.Amount, mAmount.ToString("N0").Replace(",", Strings.Numbers.comma)));
            }

            // Display item drop chance if configured.
            if (mItem.DropChanceOnDeath > 0 && string.IsNullOrEmpty(mDropChance))
            {
                data.Add(new Tuple<string, string>(Strings.ItemDescription.DropOnDeath, Strings.ItemDescription.Percentage.ToString(mItem.DropChanceOnDeath)));
            }

            // Bestiary drop chances
            if (mTableChance > 0 && !string.IsNullOrEmpty(mDropChance))
            {
                data.Add(new Tuple<string, string>(Strings.ItemDescription.BestiaryTableChance, $"{mTableChance.ToString("N2")}%"));
                data.Add(new Tuple<string, string>(Strings.ItemDescription.BestiaryDropChanceTable, mDropChance));
            }
            else if (!string.IsNullOrEmpty(mDropChance))
            {
                data.Add(new Tuple<string, string>(Strings.ItemDescription.BestiaryDropChance, mDropChance));
            }

            // Display shop value if we have one.
            if (!string.IsNullOrWhiteSpace(mValueLabel))
            {
                data.Add(new Tuple<string, string>(mValueLabel, string.Empty));
                if (!mValueLabel.Equals(Strings.Shop.wontbuy.ToString()))
                {
                    if (mItem.Stackable)
                    {
                        data.Add(new Tuple<string, string>(Strings.Shop.QuickSellAll, string.Empty));
                    }
                    else
                    {
                        data.Add(new Tuple<string, string>(Strings.Shop.QuickSell, string.Empty));
                    }
                }
            }

            // Do we have any data to display? If so, generate the element and add the data to it.
            if (data.Count > 0)
            {
                // Add a divider.
                AddDivider();

                // Add a row component.
                var rows = AddRowContainer();

                foreach (var item in data)
                {
                    rows.AddKeyValueRow(item.Item1, item.Item2);
                }

                // Resize and position the container.
                rows.SizeToChildren(true, true);
            }
        }

        protected void SetupRestrictionInfo()
        {
            // Our list of data to add, should we need to.
            var data = new List<Tuple<string, string>>();

            // Display each condition list as returned to us by the server
            data.Add(new Tuple<string, string>(Strings.ItemDescription.Restriction, String.Empty));
            for (var i = 0; i < mItem.RestrictionStrings.Count; i++)
            {
                var restriction = mItem.RestrictionStrings[i];
                if (i == 0)
                {
                    data.Add(new Tuple<string, string>(restriction, String.Empty));
                }
                else
                {
                    data.Add(new Tuple<string, string>(Strings.ItemDescription.RestrictionOr.ToString(restriction), String.Empty));
                }
            }

            // Do we have any data to display? If so, generate the element and add the data to it.
            if (data.Count > 0)
            {
                // Add a divider.
                AddDivider();

                // Add a row component.
                var rows = AddRowContainer();

                foreach (var item in data)
                {
                    rows.AddKeyValueRow(item.Item1, item.Item2, CustomColors.ItemDesc.Notice, Color.White);
                }

                // Resize and position the container.
                rows.SizeToChildren(true, true);
            }
        }

        protected void SetupCosmeticInfo()
        {
            AddDivider();

            var rows = AddRowContainer();

            rows.AddKeyValueRow(Strings.ItemDescription.CosmeticDesc, string.Empty);

            rows.SizeToChildren(true, true);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            mSpellDescWindow?.Dispose();
        }
    }
}
