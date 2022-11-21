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
    public class CharacterHarvestingWindow : CharacterWindowPanel
    {
        private Color StatLabelColor => new Color(255, 166, 167, 37);
        private Color StatColor => new Color(255, 255, 255, 255);

        private ImagePanel TiersContainer { get; set; }
        private ImagePanel MiningIcon { get; set; }
        private ImagePanel WoodcutIcon { get; set; }
        private ImagePanel FishingIcon { get; set; }

        private ImagePanel ProgressContainer { get; set; }
        private ScrollControl ProgressScrollContainer { get; set; }
        private bool RefreshProgress { get; set; }

        private NumberContainerComponent MiningLevel { get; set; }
        private NumberContainerComponent WoodcutLevel { get; set; }
        private NumberContainerComponent FishingLevel { get; set; }

        private ComponentList<IGwenComponent> ContainerComponents { get; set; }
        
        private ComponentList<IGwenComponent> ProgressComponents { get; set; }

        public CharacterHarvestingWindow(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Harvesting");

            TiersContainer = new ImagePanel(mBackground, "TiersContainer");
            MiningIcon = new ImagePanel(TiersContainer, "MiningIcon");
            WoodcutIcon = new ImagePanel(TiersContainer, "WoodcutIcon");
            FishingIcon = new ImagePanel(TiersContainer, "FishingIcon");

            ContainerComponents = new ComponentList<IGwenComponent>();
            ProgressComponents = new ComponentList<IGwenComponent>();

            MiningLevel = new NumberContainerComponent(TiersContainer, "MiningContainer", StatLabelColor, StatColor, "TIER", "Your mining tier-level.", ContainerComponents);
            WoodcutLevel = new NumberContainerComponent(TiersContainer, "WoodcutContainer", StatLabelColor, StatColor, "TIER", "Your woodcutting tier-level.", ContainerComponents);
            FishingLevel = new NumberContainerComponent(TiersContainer, "FishingContainer", StatLabelColor, StatColor, "TIER", "Your fishing tier-level.", ContainerComponents);

            ProgressContainer = new ImagePanel(mBackground, "ProgressContainer");
            ProgressScrollContainer = new ScrollControl(ProgressContainer, "ScrollContainer");
            ProgressScrollContainer.EnableScroll(false, true);

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            ContainerComponents.InitializeAll();

            RefreshProgress = true;
        }

        public override void Show()
        {
            PacketSender.SendRequestResourceInfo(1);
            RefreshProgress = true;
            base.Show();
        }

        public override void Hide()
        {
            ClearProgressComponents();
            // Forces a refresh of information from the server
            HarvestInfoRows.CurrentRows.Clear();
            base.Hide();
        }

        public override void Update()
        {
            if (Me == null || IsHidden)
            {
                return;
            }

            var miningTier = Me.MiningTier.ToString();
            var woodcutTier = Me.WoodcutTier.ToString();
            var fishingTier = Me.FishingTier.ToString();

            MiningLevel.SetValue(miningTier);
            WoodcutLevel.SetValue(woodcutTier);
            FishingLevel.SetValue(fishingTier);

            if (RefreshProgress && HarvestInfoRows.CurrentRows.Count != 0)
            {
                LoadProgressComponents();
            }

            // Constantly prep for a UI update if we haven't gotten our server response yet
            if (HarvestInfoRows.CurrentRows.Count == 0)
            {
                RefreshProgress = true;
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

            RefreshProgress = false;
        }
    }
}
