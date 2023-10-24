using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class CraftItemDescriptionWindow : DescriptionWindowBase
    {
        protected int RequiredAmount { get; set; }
        
        protected ItemBase Item { get; set; }

        public CraftItemDescriptionWindow(ItemBase item, int amount, int x, int y) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            Item = item;
            RequiredAmount = amount;

            GenerateComponents();
            SetupDescriptionWindow();

            SetPosition(x, y);
        }

        protected void SetupDescriptionWindow()
        {
            SetupHeader();

            if (!string.IsNullOrWhiteSpace(Item.Description))
            {
                SetupDescription();
            }

            AddDivider();

            SetupRequired();

            FinalizeWindow();
        }

        protected void SetupHeader()
        {
            // Create our header, but do not load our layout yet since we're adding components manually.
            var header = AddHeader();

            // Set up the icon, if we can load it.
            var tex = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Item, Item.Icon);
            if (tex != null)
            {
                header.SetIcon(tex, Item.Color);
            }

            // Set up the header as the item name.
            var rarityColor = Color.White;
            if (Item.Rarity > 0)
            {
                CustomColors.Items.Rarities.TryGetValue(Item.Rarity, out rarityColor);
            }

            var itemName = Item.ItemType == ItemTypes.Cosmetic ?
                string.IsNullOrEmpty(Item.CosmeticDisplayName) ?
                    Item.Name :
                    Item.CosmeticDisplayName :
                Item.Name;

            header.SetTitle(itemName, rarityColor ?? Color.White);

            // Set up the description telling us what type of item this is.
            // if equipment, also list what kind.
            LocalizedString typeDesc;
            if (!string.IsNullOrEmpty(Item.TypeDisplayOverride))
            {
                typeDesc = Item.TypeDisplayOverride;
            }
            else
            {
                Strings.ItemDescription.ItemTypes.TryGetValue((int)Item.ItemType, out typeDesc);
            }

            if (Item.ItemType == ItemTypes.Equipment)
            {
                var equipSlot = Options.Equipment.Slots[Item.EquipmentSlot];
                var extraInfo = equipSlot;
                if (Item.EquipmentSlot == Options.WeaponIndex && Item.TwoHanded)
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
            if (Item.Rarity > 0)
            {
                Strings.ItemDescription.Rarity.TryGetValue(Item.Rarity, out var rarityDesc);
                header.SetDescription(rarityDesc, rarityColor ?? Color.White);
            }

            header.SizeToChildren(true, false);
        }

        protected void SetupDescription()
        {
            // Add a divider.
            AddDivider();

            // Add the actual description.
            var description = AddDescription();
            description.AddText(Strings.ItemDescription.Description.ToString(Item.Description), Color.White);

            description.SizeToChildren(true, false);
        }

        protected void SetupRequired()
        {
            var requirementRows = AddRowContainer();

            var color = CustomColors.ItemDesc.Worse;
            if ((Globals.Me?.GetTotalOfItem(Item.Id) ?? 0) >= RequiredAmount)
            {
                color = CustomColors.ItemDesc.Better;
            }

            requirementRows.AddKeyValueRow("Required:", $"{RequiredAmount}", CustomColors.ItemDesc.Notice, color);
        }
    }
}
