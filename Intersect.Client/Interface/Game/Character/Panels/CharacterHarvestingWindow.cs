using System;
using System.Collections.Generic;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Equipment;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Interface.Objects;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Label = Intersect.Client.Framework.Gwen.Control.Label;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterHarvestingWindowController
    {
        public static bool WaitingOnServer { get; set; }
    }

    public class CharacterHarvestingWindow : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Harvesting;

        private bool WaitingOnServer => CharacterHarvestingWindowController.WaitingOnServer;

        private Color StatLabelColor => new Color(255, 166, 167, 37);
        private Color StatColor => new Color(255, 255, 255, 255);

        private ImagePanel TiersContainer { get; set; }
        private ImagePanel MiningIcon { get; set; }
        private ImagePanel WoodcutIcon { get; set; }
        private ImagePanel FishingIcon { get; set; }

        private Button MiningButton { get; set; }
        private Button FishingButton { get; set; }
        private Button WoodcuttingButton { get; set; }

        private Label NameHeader { get; set; }
        private Label ProgressHeader { get; set; }

        private ImagePanel ProgressContainer { get; set; }
        private ScrollControl ProgressScrollContainer { get; set; }

        private NumberContainerComponent MiningLevel { get; set; }
        private NumberContainerComponent WoodcutLevel { get; set; }
        private NumberContainerComponent FishingLevel { get; set; }
        private int ToolDisplay { get; set; }
        
        const int Pickaxe = 1;
        const int Axe = 0;
        const int FishingRod = 3;

        private Label LoadingText { get; set; }

        private ComponentList<IGwenComponent> ContainerComponents { get; set; }
        
        private ComponentList<IGwenComponent> ProgressComponents { get; set; }

        public CharacterHarvestingWindow(ImagePanel panelBackground)
        {
            ToolDisplay = Pickaxe;

            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Harvesting");

            TiersContainer = new ImagePanel(mBackground, "TiersContainer");
            MiningIcon = new ImagePanel(TiersContainer, "MiningIcon");
            WoodcutIcon = new ImagePanel(TiersContainer, "WoodcutIcon");
            FishingIcon = new ImagePanel(TiersContainer, "FishingIcon");

            MiningButton = new Button(mBackground, "MiningButton")
            {
                Text = "Ore"
            };
            MiningButton.Clicked += MiningButton_Clicked;

            WoodcuttingButton = new Button(mBackground, "WoodcuttingButton")
            {
                Text = "Wood"
            };
            WoodcuttingButton.Clicked += WoodcutButton_Clicked;

            FishingButton = new Button(mBackground, "FishingButton")
            {
                Text = "Fish"
            };
            FishingButton.Clicked += FishingButton_Clicked;

            ContainerComponents = new ComponentList<IGwenComponent>();
            ProgressComponents = new ComponentList<IGwenComponent>();

            MiningLevel = new NumberContainerComponent(TiersContainer, "MiningContainer", StatLabelColor, StatColor, "TIER", "Your mining tier-level.", ContainerComponents);
            WoodcutLevel = new NumberContainerComponent(TiersContainer, "WoodcutContainer", StatLabelColor, StatColor, "TIER", "Your woodcutting tier-level.", ContainerComponents);
            FishingLevel = new NumberContainerComponent(TiersContainer, "FishingContainer", StatLabelColor, StatColor, "TIER", "Your fishing tier-level.", ContainerComponents);

            ProgressContainer = new ImagePanel(mBackground, "ProgressContainer");
            NameHeader = new Label(ProgressContainer, "NameHeader")
            {
                Text = "Resource"
            };
            ProgressHeader = new Label(ProgressContainer, "ProgressHeader")
            {
                Text = "Harvest Lvl."
            };

            ProgressScrollContainer = new ScrollControl(ProgressContainer, "ScrollContainer");
            ProgressScrollContainer.EnableScroll(false, true);

            LoadingText = new Label(ProgressScrollContainer, "LoadingText")
            {
                Text = "Loading..."
            };

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            ContainerComponents.InitializeAll();

            EnableButtons();
            MiningButton.Disable(); // On by default
        }

        public override void Show()
        {
            PacketSender.SendRequestResourceInfo(ToolDisplay);
            base.Show();
        }

        public override void Hide()
        {
            ClearProgressComponents();
            base.Hide();
        }

        public override void Update()
        {
            if (Me == null || IsHidden)
            {
                return;
            }

            if (WaitingOnServer)
            {
                LoadingText.Show();
            }
            else
            {
                LoadingText.Hide();
            }

            var miningTier = Me.MiningTier.ToString();
            var woodcutTier = Me.WoodcutTier.ToString();
            var fishingTier = Me.FishingTier.ToString();

            MiningLevel.SetValue(miningTier);
            WoodcutLevel.SetValue(woodcutTier);
            FishingLevel.SetValue(fishingTier);

            if (!WaitingOnServer && HarvestInfoRows.CurrentRows.Count != 0)
            {
                LoadProgressComponents();
            }
        }

        private void ClearProgressComponents()
        {
            ProgressComponents?.DisposeAll();
            foreach (var child in ProgressScrollContainer.Children.ToArray())
            {
                ProgressScrollContainer.RemoveChild(child, false);
            }
        }

        private void LoadProgressComponents()
        {
            ClearProgressComponents();
            var idx = 0;
            var yPadding = 56;
            foreach(var harvestRow in HarvestInfoRows.CurrentRows)
            {
                var row = new HarvestProgressRowComponent(
                    ProgressScrollContainer,
                    "HarvestProgressRow",
                    harvestRow.ResourceTexture,
                    harvestRow.ResourceName,
                    harvestRow.HarvestLevel,
                    harvestRow.HarvestLevel + 1,
                    harvestRow.Remaining,
                    harvestRow.PercentRemaining,
                    harvestRow.Harvestable,
                    harvestRow.CannotHarvestMessage
                );

                ProgressComponents.Add(row);
                row.Initialize();
                row.SetPosition(row.X, row.Y + (yPadding * idx));
                if (idx % 2 == 1)
                {
                    row.SetBanding();
                }

                idx++;
            }
        }

        private void EnableButtons()
        {
            MiningButton.Enable();
            WoodcuttingButton.Enable();
            FishingButton.Enable();
        }

        private void MiningButton_Clicked(Base control, EventArgs args)
        {
            ToolDisplay = Pickaxe;
            PacketSender.SendRequestResourceInfo(ToolDisplay);
            ClearProgressComponents();
            
            EnableButtons();
            MiningButton.Disable();
        }

        private void WoodcutButton_Clicked(Base control, EventArgs args)
        {
            ToolDisplay = Axe;
            PacketSender.SendRequestResourceInfo(ToolDisplay);
            ClearProgressComponents();

            EnableButtons();
            WoodcuttingButton.Disable();
        }

        private void FishingButton_Clicked(Base control, EventArgs args)
        {
            ToolDisplay = FishingRod;
            PacketSender.SendRequestResourceInfo(ToolDisplay);
            ClearProgressComponents();

            EnableButtons();
            FishingButton.Disable();
        }
    }
}
