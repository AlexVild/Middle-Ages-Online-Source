using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Crafting;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Components
{
    public class CraftItemComponent : GwenComponent
    {
        public Color EnoughColor;

        public Color NotEnoughColor;

        public CraftItemComponent(Base parent, 
            string containerName, 
            ItemBase item,
            int amountRequired,
            int descX,
            int descY,
            Color enoughColor,
            Color notEnoughColor,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, "CraftItemComponent", referenceList)
        {
            Item = item;
            AmountRequired = amountRequired;
            DescX = descX;
            DescY = descY;
            EnoughColor = enoughColor;
            NotEnoughColor = notEnoughColor;
        }

        ItemBase Item { get; set; }

        int AmountRequired { get; set; }

        ImageFrameComponent ItemImage { get; set; }

        Label AmountLabel { get; set; }

        int DescX;
        int DescY;

        CraftItemDescriptionWindow DescriptionWindow { get; set; }

        public override void Initialize()
        {
            if (Globals.Me == default)
            {
                return;
            }

            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            ItemImage = new ImageFrameComponent(SelfContainer, "ImageFrame", "inventoryitem.png", Item.Icon, Framework.File_Management.GameContentManager.TextureType.Item, 1, 4);

            AmountLabel = new Label(SelfContainer, "AmountLabel");

            var amountOwned = Globals.Me.GetTotalOfItem(Item.Id);
            AmountLabel.SetText($"{amountOwned}/{AmountRequired}");

            base.Initialize();

            ItemImage.Initialize();

            FitParentToComponent();

            if (amountOwned >= AmountRequired)
            {
                AmountLabel.SetTextColor(EnoughColor, Label.ControlState.Normal);
            } else
            {
                AmountLabel.SetTextColor(NotEnoughColor, Label.ControlState.Normal);
            }

            ItemImage.Image.HoverEnter += Image_HoverEnter;
            ItemImage.Image.HoverLeave += Image_HoverLeave;
            ItemImage.SetImageRenderColor(Item.Color);
        }

        private void Image_HoverLeave(Base sender, EventArgs arguments)
        {
            DescriptionWindow?.Dispose();
        }

        private void Image_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            DescriptionWindow = new CraftItemDescriptionWindow(Item, AmountRequired, DescX, DescY);
        }

        public override void Dispose()
        {
            ItemImage?.Dispose();
            DescriptionWindow?.Dispose();
            base.Dispose();
        }
    }
}
