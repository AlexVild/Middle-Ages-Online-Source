using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Enhancement
{
    struct EnhancementRow
    {
        public EnhancementDescriptionWindow DescriptionWindow;
        public Guid EnhancementId;

        public EnhancementRow(EnhancementDescriptionWindow descriptionWindow, Guid enhancementId)
        {
            DescriptionWindow = descriptionWindow;
            EnhancementId = enhancementId;
        }
    }

    public class EnhancementWindow : GameWindow
    {
        protected override string FileName => "EnhancementWindow";

        protected override string Title => Strings.EnhancementWindow.Title;

        Label WeaponLabel { get; set; }

        ImagePanel ItemBg { get; set; }
        EnhancementItem EnhancementItem { get; set; }
        ItemBase EnhancementItemDescriptor { get; set; }

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

        Label EPBarLabel { get; set; }

        ImagePanel EnhancementBackground { get; set; }
        Label EnhancementHeader { get; set; }
        Label EPHeader { get; set; }
        ListBox EnhancementContainer { get; set; }

        ImagePanel AppliedEnhancementsBg { get; set; }
        Label AppliedEnhancementsLabel { get; set; }
        ListBox AppliedEnhancementsContainer { get; set; }

        Button AddEnhancementButton { get; set; }
        Button RemoveEnhancementButton { get; set; }

        Button RemoveAllButton { get; set; }
        Button ApplyButton { get; set; }
        Button CancelButton { get; set; }

        bool IsFlashing {get; set;}
        bool ThresholdPassed {get; set;}
        
        Item EquippedItem;
        General.Enhancement.Enhancement Enhancement => Globals.Me?.Enhancement;
        List<Guid> KnownEhancements => Globals.Me?.KnownEnhancements ?? new List<Guid>();

        private Guid SelectedEnhancementId { get; set; }
        private Guid SelectedAppliedEnhancementId { get; set; }

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
            EnhancementItemDescriptor = ItemBase.Get(equippedWeapon.ItemId);

            EnhancementItem.Update(EquippedItem.ItemId, EquippedItem.ItemProperties);
            WeaponLabel.SetText($"{ItemBase.GetName(EquippedItem.ItemId)} Enhancement");

            // TODO make this real
            EPBarLabel.SetText(
                Strings.EnhancementWindow.EPRemaining.ToString(EnhancementItemDescriptor.EnhancementThreshold)
            );

            base.Show();
        }

        void UpdateKnownEnhancements()
        {
            EnhancementContainer.Clear();
            foreach (var enhancementId in KnownEhancements)
            {
                var enhancement = EnhancementDescriptor.Get(enhancementId);
                if (enhancement == default)
                {
                    continue;
                }

                var tmpRow = EnhancementContainer.AddRow($"{EnhancementDescriptor.GetName(enhancementId)}");

                tmpRow.UserData = new EnhancementRow(
                    new EnhancementDescriptionWindow(enhancementId, EnhancementItemDescriptor.Icon, Background.X, Background.Y), 
                    enhancementId);
                tmpRow.SetTextColor(new Color(50, 19, 0));
                tmpRow.RenderColor = new Color(100, 232, 208, 170);
                tmpRow.Clicked += Enhancement_Clicked;
                tmpRow.HoverEnter += Enhancement_Hover;
                tmpRow.HoverLeave += Enhancement_Leave;
            }
        }

        private void Enhancement_Leave(Base sender, EventArgs arguments)
        {
            var row = (EnhancementRow)((ListBoxRow)sender).UserData;
            row.DescriptionWindow.Hide();
        }

        private void Enhancement_Hover(Base sender, EventArgs arguments)
        {
            var row = (EnhancementRow)((ListBoxRow)sender).UserData;
            row.DescriptionWindow.SetPosition(Background.X + 6, Background.Y + EnhancementBackground.Y + 40);
            row.DescriptionWindow.Show();
        }

        private void Enhancement_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            var row = (EnhancementRow)((ListBoxRow)sender).UserData;
            SelectedEnhancementId = row.EnhancementId;
        }

        public override void UpdateShown()
        {
            if (Enhancement == null || !Enhancement.IsOpen)
            {
                Close();
                return;
            }

            // Update location of item hover to wherever the window is being drawn
            EnhancementItem.SetHoverPanelLocation(Background.X + 6, Background.Y);

            if (!Enhancement.RefreshUi)
            {
                return;
            }

            UpdateKnownEnhancements();

            Enhancement.RefreshUi = false;
        }

        protected override void PostInitialization()
        {
            EnhancementItem.Setup();
        }

        protected override void PreInitialization()
        {
            WeaponLabel = new Label(Background, "WeaponLabel");

            ItemBg = new ImagePanel(Background, "ItemIcon");
            EnhancementItem = new EnhancementItem(0, ItemBg, Background.X, Background.Y);

            EPBarBg = new ImagePanel(Background, "EPBar");
            EPBarPrevious = new ImagePanel(EPBarBg);
            EPBarNew = new ImagePanel(EPBarBg);

            EPBarLabel = new Label(Background, "EPBarLabel");

            EnhancementBackground = new ImagePanel(Background, "EnhancementsBg");
            EnhancementHeader = new Label(EnhancementBackground, "EnhancementsHeader")
            {
                Text = Strings.EnhancementWindow.Enhancements
            };
            EPHeader = new Label(EnhancementBackground, "EPHeader")
            {
                Text = Strings.EnhancementWindow.EP
            };
            EnhancementContainer = new ListBox(EnhancementBackground, "EnhancementsContainer");

            AppliedEnhancementsBg = new ImagePanel(Background, "AppliedEnhancementsBg");
            AppliedEnhancementsLabel = new Label(AppliedEnhancementsBg, "AppliedEnhancementsHeader")
            {
                Text = Strings.EnhancementWindow.AppliedEnhancemnets
            };
            AppliedEnhancementsContainer = new ListBox(AppliedEnhancementsBg, "AppliedEnhancementsContainer");

            AddEnhancementButton = new Button(Background, "AddEnhancementButton")
            {
                Text = "Add"
            };
            RemoveEnhancementButton = new Button(Background, "RemoveEnhancementButton")
            {
                Text = "Remove"
            };

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

            CancelButton.Clicked += CancelButton_Clicked;
        }

        private void CancelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Close();
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
