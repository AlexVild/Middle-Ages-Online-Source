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

        private NumberContainerComponent MiningLevel { get; set; }
        private NumberContainerComponent WoodcutLevel { get; set; }
        private NumberContainerComponent FishingLevel { get; set; }

        private ComponentList<NumberContainerComponent> ContainerComponents { get; set; }

        public CharacterHarvestingWindow(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Harvesting");

            TiersContainer = new ImagePanel(mBackground, "TiersContainer");
            MiningIcon = new ImagePanel(TiersContainer, "MiningIcon");
            WoodcutIcon = new ImagePanel(TiersContainer, "WoodcutIcon");
            FishingIcon = new ImagePanel(TiersContainer, "FishingIcon");

            ContainerComponents = new ComponentList<NumberContainerComponent>();
            MiningLevel = new NumberContainerComponent(TiersContainer, "MiningContainer", StatLabelColor, StatColor, "TIER", "Your mining tier-level.", ContainerComponents);
            WoodcutLevel = new NumberContainerComponent(TiersContainer, "WoodcutContainer", StatLabelColor, StatColor, "TIER", "Your mining tier-level.", ContainerComponents);
            FishingLevel = new NumberContainerComponent(TiersContainer, "FishingContainer", StatLabelColor, StatColor, "TIER", "Your fishing tier-level.", ContainerComponents);

            var x = new HarvestProgressRowComponent(
                mBackground, "HarvestProgressRow", "al_coal.png", "COAL", 1, 2, 23, 0.23f, true, "Can't harvest yet!"
            );
            var y = new HarvestProgressRowComponent(
                mBackground, "HarvestProgressRow2", "al_iron.png", "IRON", 1, 2, 23, 0f, false, "You must have a mining tier of at least 2+ to mine this resource!"
            );

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            x.Initialize();
            y.Initialize();

            ContainerComponents.InitializeAll();
        }

        public override void Show()
        {
            PacketSender.SendRequestResourceInfo(1);
            base.Show();
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
        }
    }
}
