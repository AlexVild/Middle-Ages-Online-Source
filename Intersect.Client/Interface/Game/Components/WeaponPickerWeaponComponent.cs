using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Items;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class WeaponPickerWeaponComponent : ImageFrameComponent
    {
        public ItemDescriptionWindow HoverWindow;

        private Item InventoryItem { get; set; }

        private int InventorySlot { get; set; }

        private Guid EnhancementId { get; set; }

        private int X { get; set; }
        private int Y { get; set; }

        public WeaponPickerWeaponComponent(Base parent,
            string containerName,
            string frameTexture,
            string imageTexture,
            TextureType imageTextureType,
            int scale,
            int borderWidth,
            Item item,
            int invSlot,
            int x,
            int y,
            ComponentList<GwenComponent> referenceList = null) : base(parent, containerName, frameTexture, imageTexture, imageTextureType, scale, borderWidth, referenceList)
        {
            InventoryItem = item;
            InventorySlot = invSlot;
            X = x;
            Y = y;
        }

        public override void Initialize()
        {
            base.Initialize();
            Image.HoverEnter += Image_HoverEnter;
            Image.HoverLeave += Image_HoverLeave;
            Image.Clicked += Image_Clicked;
        }

        private void Image_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Audio.AddGameSound("ui_press.wav", false);
            PacketSender.SendWeaponPickerResponse(InventorySlot);
        }

        private void Image_HoverLeave(Base sender, EventArgs arguments)
        {
            HoverWindow?.Dispose();
        }

        private void Image_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            HoverWindow = new ItemDescriptionWindow(InventoryItem.Base, 1, X, Y, InventoryItem.ItemProperties);
        }
    }
}
