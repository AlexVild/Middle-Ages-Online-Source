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

        private ImagePanel MiningIcon { get; set; }
        private ImagePanel WoodcutIcon { get; set; }
        private ImagePanel FishingIcon { get; set; }

        private ImagePanel MiningContainer { get; set; }
        private NumberContainerComponent MiningLevel { get; set; }
        
        private ImagePanel WoodcutContainer { get; set; }
        private NumberContainerComponent WoodcutLevel { get; set; }

        private ImagePanel FishingContainer { get; set; }
        private NumberContainerComponent FishingLevel { get; set; }

        public CharacterHarvestingWindow(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Harvesting");

            MiningIcon = new ImagePanel(mBackground, "MiningIcon");
            WoodcutIcon = new ImagePanel(mBackground, "WoodcutIcon");
            FishingIcon = new ImagePanel(mBackground, "FishingIcon");

            MiningContainer = new ImagePanel(mBackground, "MiningContainer");
            WoodcutContainer = new ImagePanel(mBackground, "WoodcutContainer");
            FishingContainer = new ImagePanel(mBackground, "FishingContainer");

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            MiningLevel = new NumberContainerComponent(MiningContainer);
            MiningLevel.Initialize(StatLabelColor, StatColor, "TIER", "Your mining tier-level.");

            WoodcutLevel = new NumberContainerComponent(WoodcutContainer);
            WoodcutLevel.Initialize(StatLabelColor, StatColor, "TIER", "Your woodcutting tier-level.");

            FishingLevel = new NumberContainerComponent(FishingContainer);
            FishingLevel.Initialize(StatLabelColor, StatColor, "TIER", "Your fishing tier-level.");
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
