using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Enhancement
{
    public class EnhancementWindow : GameWindow
    {
        protected override string FileName => "EnhancementWindow";

        protected override string Title => Strings.EnhancementWindow.Title;

        Label WeaponLabel { get; set; }

        EnhancementItem EnhancementItem { get; set; }

        ImagePanel EPBarBg { get; set; }
        ImagePanel EPBarPrevious { get; set; }
        ImagePanel EPBarNew { get; set; }
        private GameTexture _epBarPrevTxt { get; set; }
        private GameTexture _epBarNewTxt { get; set; }
        private GameTexture _epBarNewFlashTxt { get; set; }
        private GameTexture _epBarFullTxt { get; set; }
        private GameTexture _epBarFullFlashTxt { get; set; }
        GameTexture EPBarNewTxt => ThresholdPassed ? 
            (IsFlashing ? _epBarFullFlashTxt : _epBarFullTxt) :
            IsFlashing ? _epBarNewFlashTxt : _epBarNewTxt;

        ImagePanel EnhancementBackground { get; set; }
        Label EnhancementHeader { get; set; }
        Label EPHeader { get; set; }
        ScrollControl EnhancementContainer { get; set; }

        ImagePanel AppliedEnhancementsBg { get; set; }
        Label AppliedEnhancementsLabel { get; set; }
        ScrollControl AppliedEnhancementsContainer { get; set; }

        Button RemoveAllButton { get; set; }
        Button ApplyButton { get; set; }
        Button CancelButton { get; set; }

        bool IsFlashing {get; set;}
        bool ThresholdPassed {get; set;}
        
        Item EquippedItem;
        General.Enhancement.Enhancement Enhancement => Globals.Me?.Enhancement;

        public EnhancementWindow(Base gameCanvas) : base(gameCanvas) 
        {
            var txtType = Framework.File_Management.GameContentManager.TextureType.Gui;
            _epBarPrevTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_previous.png");
            _epBarNewTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_new.png");
            _epBarNewFlashTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_new_flash.png");
            _epBarFullTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_expended.png");
            _epBarFullFlashTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_expended_flash.png");
        }

        public override void Show()
        {
            if (Globals.Me == null)
            {
                return;
            }

            if (!Globals.Me.TryGetEquippedWeapon(out var equippedWeapon))
            {
#pragma warning disable CA2000 // Dispose objects before losing scope
                _ = new InputBox(
                    Strings.EnhancementWindow.NoWeaponEquipped,
                    Strings.EnhancementWindow.NoWeaponEquippedPrompt, true,
                    InputBox.InputType.OkayOnly, Close, null, null
                );
#pragma warning restore CA2000 // Dispose objects before losing scope
                return;
            }

            EquippedItem = equippedWeapon;
            EnhancementItem.Update(EquippedItem.ItemId, EquippedItem.ItemProperties);

            base.Show();
        }

        public override void UpdateShown()
        {
            //
        }

        protected override void PostInitialization()
        {
            EnhancementItem.Setup();
        }

        protected override void PreInitialization()
        {
            WeaponLabel = new Label(Background, "WeaponLabel");
            EnhancementItem = new EnhancementItem(0, Background, Background.X, Background.Y);

            EPBarBg = new ImagePanel(Background, "EPBar");
            EPBarPrevious = new ImagePanel(EPBarBg);
            EPBarNew = new ImagePanel(EPBarBg);

            EnhancementBackground = new ImagePanel(Background, "EnhancementsBg");
            EnhancementHeader = new Label(EnhancementBackground, "EnhancementsHeader")
            {
                Text = Strings.EnhancementWindow.Enhancements
            };
            EPHeader = new Label(EnhancementBackground, "EPHeader")
            {
                Text = Strings.EnhancementWindow.EP
            };
            EnhancementContainer = new ScrollControl(EnhancementBackground, "EnhancementsContainer");

            AppliedEnhancementsBg = new ImagePanel(Background, "AppliedEnhancementsBg");
            AppliedEnhancementsLabel = new Label(AppliedEnhancementsBg, "AppliedEnhancementsHeader")
            {
                Text = Strings.EnhancementWindow.AppliedEnhancemnets
            };
            AppliedEnhancementsContainer = new ScrollControl(AppliedEnhancementsBg, "AppliedEnhancementsContainer");

            RemoveAllButton = new Button(Background, "RemoveAllButton")
            {
                Text = "Reset All"
            };
            ApplyButton = new Button(Background, "ApplyButton")
            {
                Text = "Apply"
            };
            CancelButton = new Button(Background, "CancelButton")
            {
                Text = "Cancel"
            };
        }

        private void Close(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Close()
        {
            Enhancement?.Close();
            PacketSender.SendCloseEnhancementPacket();
            base.Close();
        }
    }
}
