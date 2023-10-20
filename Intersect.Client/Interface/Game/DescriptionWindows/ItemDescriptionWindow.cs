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
    public partial class ItemDescriptionWindow : DescriptionWindowBase
    {
        protected ItemBase mItem;

        protected int mAmount;

        protected ItemProperties mItemProperties;

        protected string mTitleOverride;

        protected string mValueLabel;

        protected string mDropChance;

        protected double mTableChance;

        protected int mTableQuantity;

        protected SpellDescriptionWindow mSpellDescWindow;
        
        protected EnhancementDescriptionWindow mEnhancementDescWindow;

        public Item EquippedItem;

        public ItemBase EquippedItemDesc => EquippedItem?.Base;

        public bool EquipmentComparison => EquippedItem != default;

        public bool ShowEnhancementBreakdown = false;

        public bool FromEnhancementWindow = false;

        public ItemDescriptionWindow(
            ItemBase item,
            int amount,
            int x,
            int y,
            ItemProperties itemProperties,
            string titleOverride = "",
            string valueLabel = "",
            string dropChance = "",
            double tableChance = 0d,
            int tableQuantity = 0,
            bool enhancementWindow = false
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
            mTableQuantity = tableQuantity;
            FromEnhancementWindow = enhancementWindow;

            if (mItem != null && mItem.ItemType == ItemTypes.Equipment)
            {
                var itemSlot = Globals.Me.MyEquipment[mItem.EquipmentSlot];
                EquippedItem = Globals.Me.Inventory.ElementAtOrDefault(itemSlot);
            }

            GenerateComponents();
            SetupDescriptionWindow();

            if (!FromEnhancementWindow)
            {
                // If a spell, also display the spell description!
                if (mItem.ItemType == ItemTypes.Spell)
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
                if (mItem.ProcSpellId != default)
                {
                    mSpellDescWindow = new SpellDescriptionWindow(mItem.ProcSpellId, x, y);
                }
                if (mItem.ItemType == ItemTypes.Enhancement)
                {
                    mEnhancementDescWindow = new EnhancementDescriptionWindow(mItem.EnhancementId, mItem.Icon, x, y, isLearnable: true);
                    mEnhancementDescWindow.Show();
                }
                else if ((Interface.GameUi.CraftingWindowOpen() || Interface.GameUi.DeconstructorWindow.IsVisible()) && mItem.StudyEnhancement != Guid.Empty)
                {
                    if (!Globals.Me.KnownEnhancements.Contains(mItem.StudyEnhancement))
                    {
                        mEnhancementDescWindow = new EnhancementDescriptionWindow(mItem.StudyEnhancement, mItem.Icon, x - (mSpellDescWindow?.Width ?? 0), y, (float)mItem.StudyChance, isLearnable: true);
                        mEnhancementDescWindow.Show();
                    }
                }

                if (mSpellDescWindow != default)
                {
                    x -= mSpellDescWindow.Container.Width + 4;
                }
                if (mEnhancementDescWindow != default)
                {
                    x -= mEnhancementDescWindow.Container.Width + 4;
                }
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
                    SetupItemEnhancementInfo();
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

                case ItemTypes.Enhancement:
                    SetupEnhancementInfo();
                    break;
                
                case ItemTypes.Permabuff:
                    SetupPermabuffInfo();
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
            var rarityColor = Color.White;
            if (mItem.Rarity > 0)
            {
                CustomColors.Items.Rarities.TryGetValue(mItem.Rarity, out rarityColor);
            }

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
            if (mItem.Rarity > 0)
            {
                Strings.ItemDescription.Rarity.TryGetValue(mItem.Rarity, out var rarityDesc);
                header.SetDescription(rarityDesc, rarityColor ?? Color.White);
            }

            header.SizeToChildren(true, false);
        }

        [Obsolete("Not currently used, muddies UI IMO")]
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

        protected void SetupItemEnhancementInfo()
        {

            if (string.IsNullOrEmpty(mItemProperties?.EnhancedBy))
            {
                return;
            }

            AddDivider();

            var rows = AddRowContainer();

            rows.AddKeyValueRow("Enhanced by:", mItemProperties.EnhancedBy, CustomColors.ItemDesc.Notice, CustomColors.ItemDesc.Notice);

            rows.SizeToChildren(true, true);

            if (mItemProperties.AppliedEnhancementIds.Count <= 0)
            {
                return;
            }

            //return; // Alex - Remove this to display enhancement info

            var description = AddDescription();
            Dictionary<string, int> enhancementApplications = new Dictionary<string, int>();
            foreach (var enId in mItemProperties.AppliedEnhancementIds)
            {
                var name = EnhancementDescriptor.GetName(enId);
                if (name == EnhancementDescriptor.Deleted)
                {
                    continue;
                }
                if (enhancementApplications.ContainsKey(name))
                {
                    enhancementApplications[name]++;
                }
                else
                {
                    enhancementApplications[name] = 1;
                }
            }

            List<string> appliedEnhancements = new List<string>();
            foreach (var kv in enhancementApplications)
            {
                var enhancementName = kv.Key;
                var enhancementAmount = kv.Value;

                if (enhancementAmount > 1)
                {
                    appliedEnhancements.Add($"{enhancementName} x{enhancementAmount}");
                }
                else
                {
                    appliedEnhancements.Add($"{enhancementName}");
                }
            }

            if (appliedEnhancements.Count > 0)
            {
                description.AddText($"Enhancements: {string.Join(", ", appliedEnhancements)}", CustomColors.ItemDesc.Muted);
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

            if (mTableQuantity > 0)
            {
                data.Add(new Tuple<string, string>(Strings.ItemDescription.BestiaryDropQuantity, mTableQuantity.ToString()));
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

            if (mItem.Fuel > 0)
            {
                data.Add(new Tuple<string, string>("Fuel Stored", mItem.Fuel.ToString("N0")));
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
            var description = AddDescription();

            description.AddText(Strings.ItemDescription.Restriction, CustomColors.ItemDesc.Notice);
            description.AddLineBreak();
            description.AddText(string.Join(" ", mItem.RestrictionStrings), CustomColors.ItemDesc.Notice);
        }

        protected void SetupCosmeticInfo()
        {
            AddDivider();

            var rows = AddRowContainer();

            rows.AddKeyValueRow(Strings.ItemDescription.CosmeticDesc, string.Empty);

            rows.SizeToChildren(true, true);
        }

        protected void SetupEnhancementInfo()
        {
            AddDivider();

            var rows = AddRowContainer();

            rows.AddKeyValueRow(Strings.ItemDescription.EnhancementDesc, string.Empty);

            if (Globals.Me.KnownEnhancements.Contains(mItem.EnhancementId))
            {
                rows.AddKeyValueRow(Strings.ItemDescription.EnhancementKnown, null, CustomColors.ItemDesc.Notice, Color.White);
            }

            rows.SizeToChildren(true, true);
        }

        protected void SetupPermabuffInfo()
        {
            AddDivider();

            var rows = AddRowContainer();

            rows.AddKeyValueRow(Strings.ItemDescription.PermabuffDesc, string.Empty);

            if (mItem.SkillPoints > 0)
            {
                rows.AddKeyValueRow(Strings.ItemDescription.PermabuffSkillPoints, mItem.SkillPoints.ToString());
            }

            var statIdx = 0;
            foreach (var stat in mItem.StatsGiven)
            {
                if (stat == 0)
                {
                    statIdx++;
                    continue;
                }

                rows.AddKeyValueRow(Strings.ItemDescription.Stats[statIdx], stat.ToString("N0"));
                statIdx++;
            }

            var vitalIdx = 0;
            foreach (var vital in mItem.VitalsGiven)
            {
                if (vital == 0)
                {
                    vitalIdx++;
                    continue;
                }

                rows.AddKeyValueRow(Strings.ItemDescription.Vitals[vitalIdx], vital.ToString("N0"));
                vitalIdx++;
            }

            rows.SizeToChildren(true, true);

            AddDivider();

            rows = AddRowContainer();

            if (Globals.UsedPermabuffs.Contains(mItem.Id))
            {
                rows.AddKeyValueRow(Strings.ItemDescription.PermabuffUsed, null, CustomColors.ItemDesc.Notice, Color.White);
            }
            else
            {
                rows.AddKeyValueRow(Strings.ItemDescription.PermabuffUnused, null, CustomColors.ItemDesc.Muted, Color.White);
            }

            rows.SizeToChildren(true, true);
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            base.Dispose();
            mSpellDescWindow?.Dispose();
            mEnhancementDescWindow?.Dispose();
        }
    }
}
