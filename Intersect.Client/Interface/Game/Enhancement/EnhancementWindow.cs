using Intersect.Client.Core;
using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.General.Enhancement;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Interface.Game.WeaponPicker;
using Intersect.Client.Interface.ScreenAnimations;
using Intersect.Client.Items;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Enhancement
{
    class EnhancementRow
    {
        public EnhancementDescriptionWindow DescriptionWindow;
        public Guid EnhancementId;

        public EnhancementRow(EnhancementDescriptionWindow descriptionWindow, Guid enhancementId)
        {
            DescriptionWindow = descriptionWindow;
            EnhancementId = enhancementId;
        }

        public void Dispose()
        {
            DescriptionWindow.Dispose();
        }
    }

    public class EnhancementWindow : GameWindow
    {
        enum States
        {
            Create,
            Finished,
        }

        States CurrentState;

        protected override string FileName => "EnhancementWindow";

        protected override string Title => Strings.EnhancementWindow.Title;

        Label WeaponLabel { get; set; }

        ImagePanel ItemBg { get; set; }
        EnhancementItemIcon EnhancementItem { get; set; }
        ItemBase EnhancementItemDescriptor { get; set; }

        private EnhancementRow mSelectedRow { get; set; }

        Button ShowBreakdownButton { get; set; }

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

        ImagePanel CurrencyIcon { get; set; }
        Label CurrencyAmount { get; set; }

        bool IsFlashing {get; set;}
        readonly long FlashTime = 250;
        long LastFlash { get; set; }

        bool ThresholdPassed {get; set;}

        public int Width => Background.Width;
        public int X => Background.X;
        public int Y => Background.Y;
        
        Item EnhancingItem;
        EnhancementInterface EnhancementInterface => Globals.Me?.Enhancement;
        List<Guid> KnownEhancements => Globals.Me?.KnownEnhancements ?? new List<Guid>();

        private Guid SelectedEnhancementId { get; set; }
        private Guid SelectedAppliedEnhancementId { get; set; }

        private readonly Color Transparent = new Color(150, 255, 255, 255);

        EnhancementBreakdown BreakdownWindow;
        EnhancementCompleteWindow CompletionWindow;

        private AnvilAnim EnhancementAnimation;
        private ItemProperties NewProperties { get; set; }

        private Label NoEnhancementsLabel { get; set; }

        public EnhancementWindow(Base gameCanvas) : base(gameCanvas) 
        {
            var txtType = Framework.File_Management.GameContentManager.TextureType.Gui;
            _epBarPrevTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_previous.png");
            _epBarNewTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_new.png");
            _epBarNewFlashTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_new_flash.png");
            _epBarFullTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_expended.png");
            _epBarFullFlashTxt = Globals.ContentManager.GetTexture(txtType, "weapon_enhancement_ep_bar_expended_flash.png");
            LastFlash = Timing.Global.Milliseconds;

            BreakdownWindow = new EnhancementBreakdown(this, gameCanvas);
            CompletionWindow = new EnhancementCompleteWindow(this, gameCanvas);

            EnhancementAnimation = new AnvilAnim(() =>
            {
                Flash.FlashScreen(1000, new Color(255, 255, 255, 255), 150);
                CompletionWindow?.Show(NewProperties);
            });
        }

        public override void Show()
        {
            CurrentState = States.Create;
            if (Globals.Me == null)
            {
                return;
            }

            if (!WeaponPickerController.TryGetSelectedWeapon(out var equippedWeapon))
            {
#pragma warning disable CA2000 // Dispose objects before losing scope
                _ = new InputBox(
                    "No valid weapon",
                    "No valid weapon was selected! Please try again", true,
                    InputBox.InputType.OkayOnly, Close, null, null
                );
#pragma warning restore CA2000 // Dispose objects before losing scope
                return;
            }

            SelectedAppliedEnhancementId = Guid.Empty;
            SelectedEnhancementId = Guid.Empty;

            EnhancementContainer.UnselectAll();
            AppliedEnhancementsContainer.UnselectAll();

            EnhancingItem = equippedWeapon;
            EnhancementItemDescriptor = ItemBase.Get(equippedWeapon.ItemId);

            EnhancementItem.Update(EnhancingItem.ItemId, EnhancingItem.ItemProperties);
            WeaponLabel.SetText($"{ItemBase.GetName(EnhancingItem.ItemId)} Enhancement");

            var currencyIcon = EnhancementInterface.Currency?.Icon ?? string.Empty;
            CurrencyIcon.Texture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Item, currencyIcon);

            base.Show();
        }

        public override void Hide()
        {
            EnhancementContainer.Clear();
            AppliedEnhancementsContainer.Clear();
            BreakdownWindow.Hide();
            SelectedEnhancementId = Guid.Empty;
            SelectedAppliedEnhancementId = Guid.Empty;
            base.Hide();
        }

        void UpdateKnownEnhancements()
        {
            EnhancementContainer.Clear();

            var relevantEnhancements = KnownEhancements.Where(enhancementId =>
            {
                var enhancement = EnhancementDescriptor.Get(enhancementId);
                if (enhancement == default)
                {
                    return false;
                }

                if (!EnhancementHelper.ValidEnhancementForWeaponType(EnhancingItem.Base.MaxWeaponLevels, enhancement.ValidWeaponTypes))
                {
                    return false;
                }

                return true;
            }).ToArray();

            if (relevantEnhancements.Length <= 0)
            {
                NoEnhancementsLabel.Show();
                return;
            }

            NoEnhancementsLabel.Hide();
            foreach (var enhancementId in relevantEnhancements)
            {
                var enhancement = EnhancementDescriptor.Get(enhancementId);
                if (enhancement == default)
                {
                    continue;
                }

                var tmpRow = EnhancementContainer.AddRow($"{EnhancementDescriptor.GetName(enhancementId)}");

                tmpRow.UserData = enhancementId;

                if (EnhancementInterface.CanAddEnhancement(enhancement, out _))
                {
                    tmpRow.SetTextColor(new Color(50, 19, 0));
                }
                else
                {
                    tmpRow.SetTextColor(new Color(255, 100, 100, 100));
                }

                tmpRow.RenderColor = new Color(100, 232, 208, 170);
                tmpRow.Selected += Enhancement_Selected;
                tmpRow.HoverEnter += Enhancement_Hover;
                tmpRow.HoverLeave += Enhancement_Leave;
            }
        }

        readonly int EP_XPAD = 4;
        readonly int EP_YPAD = 4;

        void UpdateEpBarAdditive()
        {
            // Update flash
            if (LastFlash < Timing.Global.Milliseconds)
            {
                LastFlash = Timing.Global.Milliseconds + FlashTime;
                IsFlashing = !IsFlashing;
            }

            // First, update the "applied" bar
            EPBarPrevious.Texture = _epBarPrevTxt;
            EPBarPrevious.X = EP_XPAD;
            EPBarPrevious.Y = EP_YPAD;
            EPBarPrevious.Height = EPBarBg.Height - ((EP_YPAD * 2) + 4); // extra 4 pixels at bottom
            var maxWidth = EPBarBg.Width - (EP_XPAD * 2);

            var existingProportion = (int)Math.Floor((float)EnhancementInterface.EPSpent / EnhancementItemDescriptor.EnhancementThreshold * maxWidth);
            EPBarPrevious.Width = Math.Min(existingProportion, maxWidth);

            var selectedEnhancement = EnhancementDescriptor.Get(SelectedEnhancementId);
            if (selectedEnhancement == default)
            {
                EPBarNew.Texture = EPBarNewTxt;
                EPBarNew.Width = 0;
                EPBarNew.Height = EPBarBg.Height - ((EP_YPAD * 2) + 4); // extra 4 pixels at bottom
                return;
            }

            var newProportion = (int)Math.Floor((float)selectedEnhancement.RequiredEnhancementPoints / EnhancementItemDescriptor.EnhancementThreshold * maxWidth);

            maxWidth -= EPBarPrevious.Width;

            EPBarNew.X = EP_XPAD + EPBarPrevious.Width;
            EPBarNew.Y = EP_YPAD;

            ThresholdPassed = selectedEnhancement.RequiredEnhancementPoints > EnhancementInterface.EPFree;

            EPBarNew.Texture = EPBarNewTxt;
            EPBarNew.Width = Math.Min(newProportion, maxWidth);
            EPBarNew.Height = EPBarBg.Height - ((EP_YPAD * 2) + 4); // extra 4 pixels at bottom
            EPBarNew.RenderColor = Color.White;
        }

        void UpdateEpBarSubtractive()
        {
            // Update flash
            if (LastFlash < Timing.Global.Milliseconds)
            {
                LastFlash = Timing.Global.Milliseconds + FlashTime;
                IsFlashing = !IsFlashing;
            }

            // First, update the "applied" bar
            EPBarPrevious.Texture = _epBarPrevTxt;
            EPBarPrevious.X = EP_XPAD;
            EPBarPrevious.Y = EP_YPAD;
            EPBarPrevious.Height = EPBarBg.Height - ((EP_YPAD * 2) + 4); // extra 4 pixels at bottom
            var maxWidth = EPBarBg.Width - (EP_XPAD * 2);

            var enhancementToRemove = EnhancementDescriptor.Get(SelectedAppliedEnhancementId);
            if (enhancementToRemove == default)
            {
                EPBarNew.Texture = EPBarNewTxt;
                EPBarNew.Width = 0;
                EPBarNew.Height = EPBarBg.Height - ((EP_YPAD * 2) + 4); // extra 4 pixels at bottom
                return;
            }

            var existingProportion = (int)Math.Floor((float)(EnhancementInterface.EPSpent - enhancementToRemove.RequiredEnhancementPoints) / EnhancementItemDescriptor.EnhancementThreshold * maxWidth);
            EPBarPrevious.Width = Math.Max(existingProportion, 0);

            var newProportion = (int)Math.Floor((float)enhancementToRemove.RequiredEnhancementPoints / EnhancementItemDescriptor.EnhancementThreshold * maxWidth);

            maxWidth -= EPBarPrevious.Width;

            EPBarNew.X = EP_XPAD + EPBarPrevious.Width;
            EPBarNew.Y = EP_YPAD;

            ThresholdPassed = false;

            EPBarNew.Texture = EPBarNewTxt;
            EPBarNew.Width = Math.Max(newProportion, 0);
            EPBarNew.Height = EPBarBg.Height - ((EP_YPAD * 2) + 4); // extra 4 pixels at bottom
            EPBarNew.RenderColor = Transparent;
        }

        void UpdateAppliedEnhancements()
        {
            AppliedEnhancementsContainer.Clear();
            foreach (var enhancementItem in EnhancementInterface.EnhancementsApplied.ToArray())
            {
                var enhancementId = enhancementItem.EnhancementId;
                var enhancement = EnhancementDescriptor.Get(enhancementId);
                var removable = enhancementItem.Removable;

                if (enhancement == default)
                {
                    continue;
                }

                var tmpRow = AppliedEnhancementsContainer.AddRow($"{EnhancementDescriptor.GetName(enhancementId)}");

                tmpRow.UserData = enhancementId;

                if (enhancementItem.Removable)
                {
                    tmpRow.SetTextColor(new Color(50, 19, 0));
                } else
                {
                    tmpRow.SetTextColor(new Color(255, 100, 100, 100));
                }

                tmpRow.RenderColor = new Color(100, 232, 208, 170);
                tmpRow.Selected += AddedEnhancement_Selected;
                tmpRow.HoverEnter += AppliedEnhancement_Hover;
                tmpRow.HoverLeave += AppliedEnhancement_Leave;
            }
        }

        private void Enhancement_Leave(Base sender, EventArgs arguments)
        {
            mSelectedRow.Dispose();
            mSelectedRow = null;
        }

        private void Enhancement_Hover(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            var selectedId = (Guid)((ListBoxRow)sender).UserData;
            mSelectedRow = new EnhancementRow(
                new EnhancementDescriptionWindow(selectedId, EnhancementItemDescriptor.Icon, Background.X, Background.Y, showSpellInfo: true),
                selectedId);
            mSelectedRow.DescriptionWindow.SetPosition(Background.X + 6, Background.Y + EnhancementBackground.Y + 40);
            mSelectedRow.DescriptionWindow.Show();
        }

        private void AppliedEnhancement_Leave(Base sender, EventArgs arguments)
        {
            mSelectedRow.Dispose();
            mSelectedRow = null;
        }

        private void AppliedEnhancement_Hover(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            var selectedId = (Guid)((ListBoxRow)sender).UserData;
            mSelectedRow = new EnhancementRow(
                new EnhancementDescriptionWindow(selectedId, EnhancementItemDescriptor.Icon, Background.X, Background.Y, showSpellInfo: true),
                selectedId);
            mSelectedRow.DescriptionWindow.SetPositionRight(Background.X + Background.Width, Background.Y + EnhancementBackground.Y + 40);
            mSelectedRow.DescriptionWindow.Show();
        }

        private void Enhancement_Selected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            SelectedEnhancementId = (Guid)((ListBoxRow)sender).UserData;

            AppliedEnhancementsContainer.UnselectAll();
            SelectedAppliedEnhancementId = Guid.Empty;
            
            UpdateAddEnhancementAvailability();
        }

        private void AddedEnhancement_Selected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            SelectedAppliedEnhancementId = (Guid)((ListBoxRow)sender).UserData;

            EnhancementContainer.UnselectAll();
            SelectedEnhancementId = Guid.Empty;
            
            UpdateRemoveEnhancementAvailability();
        }

        public override void UpdateHidden()
        {
            if (EnhancementInterface == null || !EnhancementInterface.IsOpen)
            {
                return;
            }

            if (CurrentState == States.Finished)
            {
                if (EnhancementAnimation.Done)
                {
                    EnhancementAnimation.ResetAnimation();
                    CurrentState = States.Create;
                    return;
                }
                EnhancementAnimation.Draw();
                return;
            }
            base.UpdateHidden();
        }

        public override void UpdateShown()
        {
            if (EnhancementInterface == null || !EnhancementInterface.IsOpen)
            {
                Close();
                return;
            }

            // Update location of item hover to wherever the window is being drawn
            EnhancementItem.SetHoverPanelLocation(Background.X + 6, Background.Y);
            
            BreakdownWindow.Update();

            if (AppliedEnhancementsContainer.SelectedRowIndex == -1)
            {
                UpdateEpBarAdditive();
            }
            else
            {
                UpdateEpBarSubtractive();
            }

            if (!BreakdownWindow.Background.IsHidden)
            {
                ShowBreakdownButton.SetText("Hide Breakdown");
            }
            else
            {
                ShowBreakdownButton.SetText("Show Breakdown");
            }

            if (!EnhancementInterface.RefreshUi)
            {
                return;
            }

            ApplyButton.IsDisabled = EnhancementInterface.NewEnhancements.Length <= 0;
            RemoveAllButton.IsDisabled = EnhancingItem.ItemProperties.AppliedEnhancementIds.Count <= 0;

            CurrencyAmount.SetText(EnhancementHelper.GetEnhancementCostOnWeapon(EnhancementItemDescriptor, 
                EnhancementInterface.NewEnhancements.Select(en => en.EnhancementId).ToArray(), 
                EnhancementInterface.CostMultiplier).ToString("N0"));

            EPBarLabel.SetText(
                Strings.EnhancementWindow.EPRemaining.ToString(EnhancementInterface.EPFree)
            );

            UpdateKnownEnhancements();
            UpdateAppliedEnhancements();

            UpdateAddEnhancementAvailability();
            UpdateRemoveEnhancementAvailability();

            EnhancementInterface.RefreshUi = false;
        }

        void UpdateAddEnhancementAvailability()
        {
            if (EnhancementContainer.SelectedRowIndex == -1)
            {
                AddEnhancementButton.Disable();
            }
            else
            {
                AddEnhancementButton.IsDisabled = !EnhancementInterface.CanAddEnhancement(EnhancementDescriptor.Get(SelectedEnhancementId), out var addFailure);
                AddEnhancementButton.SetToolTipText(addFailure);
            }
        }

        void UpdateRemoveEnhancementAvailability()
        {
            var idx = AppliedEnhancementsContainer.SelectedRowIndex;
            if (idx == -1)
            {
                RemoveEnhancementButton.Disable();
            }
            else
            {
                RemoveEnhancementButton.IsDisabled = !EnhancementInterface.CanRemoveEnhancementAt(idx, out var removeFailure);
                RemoveEnhancementButton.SetToolTipText(removeFailure);
            }
        }

        protected override void PostInitialization()
        {
            EnhancementItem.Setup();
        }

        protected override void PreInitialization()
        {
            WeaponLabel = new Label(Background, "WeaponLabel");

            ItemBg = new ImagePanel(Background, "ItemIcon");
            EnhancementItem = new EnhancementItemIcon(0, ItemBg, Background.X, Background.Y);

            ShowBreakdownButton = new Button(Background, "ShowBreakdownButton");
            ShowBreakdownButton.Clicked += ShowBreakdownButton_Clicked;

            EPBarBg = new ImagePanel(Background, "EPBar");
            EPBarPrevious = new ImagePanel(EPBarBg);
            EPBarNew = new ImagePanel(EPBarBg);

            EPBarLabel = new Label(Background, "EPBarLabel");

            EnhancementBackground = new ImagePanel(Background, "EnhancementsBg");
            NoEnhancementsLabel = new Label(EnhancementBackground, "NoEnhancementLabel")
            {
                Text = Strings.EnhancementWindow.NoEnhancements
            };
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

            CurrencyIcon = new ImagePanel(Background, "CurrencyIcon");
            CurrencyAmount = new Label(Background, "CurrencyAmountLabel");

            RemoveAllButton.Clicked += RemoveAllButton_Clicked;
            ApplyButton.Clicked += ApplyButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            AddEnhancementButton.Clicked += AddEnhancementButton_Clicked;
            RemoveEnhancementButton.Clicked += RemoveEnhancementButton_Clicked;
        }

        private void RemoveAllButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (EnhancingItem.ItemProperties.AppliedEnhancementIds.Count <= 0)
            {
                return;
            }

            var cost = EnhancementHelper.GetEnhancementCostOnWeapon(EnhancingItem.Base, 
                EnhancingItem.ItemProperties.AppliedEnhancementIds.ToArray(), 
                EnhancementInterface.CostMultiplier * Options.Instance.DeconstructionOpts.RemoveEnhancementCostMod);

            _ = new InputBox(
                Strings.EnhancementWindow.RemoveEnhancements,
                Strings.EnhancementWindow.RemoveEnhancementsPrompt.ToString(cost, ItemBase.GetName(Guid.Parse(Options.Instance.PlayerOpts.GoldGuid))), true,
                InputBox.InputType.YesNo, RemoveAllEnhancements, null, null
            );
        }

        private void RemoveAllEnhancements(object sender, EventArgs e)
        {
            PacketSender.SendRequestWeaponEnhancementRemoval();
        }

        private void ApplyButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (EnhancementInterface.NewEnhancements.Length <= 0)
            {
                return;
            }

            PacketSender.SendApplyEnhancementPacket(EnhancementInterface.NewEnhancements.Select(en => en.EnhancementId).ToArray());
        }

        private void ShowBreakdownButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            EnhancementInterface.RefreshUi = true;
            if (BreakdownWindow.Background.IsHidden)
            {
                BreakdownWindow.Show();
            }
            else
            {
                BreakdownWindow.Hide();
            }
        }

        private void RemoveEnhancementButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            var idx = AppliedEnhancementsContainer.SelectedRowIndex;
            if (idx < 0)
            {
                return;
            }

            EnhancementInterface.TryRemoveEnhancementAt(idx);
            AppliedEnhancementsContainer.UnselectAll();
            SelectedAppliedEnhancementId = Guid.Empty;
        }

        private void AddEnhancementButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (SelectedEnhancementId == default || SelectedEnhancementId == Guid.Empty)
            {
                return;
            }

            EnhancementInterface.TryAddEnhancement(EnhancementDescriptor.Get(SelectedEnhancementId));
            EnhancementContainer.UnselectAll();
            SelectedEnhancementId = Guid.Empty;
        }

        private void CancelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Close();
        }

        private void Close(object sender, EventArgs e)
        {
            Close();
        }

        public void ProcessCompletedEnhancement(ItemProperties newProperties)
        {
            Hide();
            NewProperties = newProperties;
            CurrentState = States.Finished;
        }

        protected override void Close()
        {
            EnhancementInterface?.Close();
            CompletionWindow.Hide();
            PacketSender.SendCloseEnhancementPacket();
            mSelectedRow?.Dispose();
            Hide();
        }
    }
}
