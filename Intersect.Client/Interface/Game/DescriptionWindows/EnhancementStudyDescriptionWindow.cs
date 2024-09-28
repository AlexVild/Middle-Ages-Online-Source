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
    public class EnhancementStudyDescriptionWindow : DescriptionWindowBase
    {
        protected EnhancementDescriptor Enhancement { get; set; }
        protected ItemBase Item { get; set; }

        public EnhancementStudyDescriptionWindow(Guid itemId, Guid enhancementId, int x, int y) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            Enhancement = EnhancementDescriptor.Get(enhancementId);
            Item = ItemBase.Get(itemId);

            if (Enhancement == default || Item == default)
            {
                return;
            }

            GenerateComponents();
            SetupDescriptionWindow();

            SetPosition(x, y);
        }

        protected void SetupDescriptionWindow()
        {
            if (Enhancement == default)
            {
                return;
            }

            SetupHeader();

            AddDivider();

            SetupRequirements();

            AddDivider();

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
                header.SetIcon(tex, Color.White);
            }

            var rarityColor = Color.White;
            if (Item.Rarity > 0)
            {
                CustomColors.Items.Rarities.TryGetValue(Item.Rarity, out rarityColor);
            }
            header.SetTitle(Item.Name, rarityColor);

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

            if (Item.ItemType != ItemTypes.Equipment)
            {
                var desc = AddDescription();
                desc.AddText(Item.Description, Color.White);
            }
        }

        protected void SetupRequirements()
        {
            AddDivider();
            if (Item.ItemType == ItemTypes.Enhancement)
            {
                var rows = AddRowContainer();
                rows.AddKeyValueRow("Teaches Enhancement on Use", string.Empty, CustomColors.ItemDesc.Primary, CustomColors.ItemDesc.Primary);


                rows.SizeToChildren(true, true);
            }
            else
            {
                var rows = AddRowContainer();
                rows.AddKeyValueRow("Chance to Learn on Craft/Deconstruct:", string.Empty, CustomColors.ItemDesc.Primary, CustomColors.ItemDesc.Primary);
                rows.AddKeyValueRow($"{Item.StudyChance.ToString("N2")}%", string.Empty, CustomColors.ItemDesc.Better, CustomColors.ItemDesc.Better);

                rows.SizeToChildren(true, true);
            }
        }
    }
}
