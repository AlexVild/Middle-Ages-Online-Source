using System;
using System.Collections.Generic;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Character.Equipment;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Interface.Game.Character.StatPanel;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Enums;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Character
{
    public class CharacterWindowPanelController
    {
        private ImagePanel Container { get; set; }
        private ImagePanel PanelContainer { get; set; }
        private Button PanelHideButton;

        private List<Button> PanelSelectors { get; set; }
        private Button StatsButton { get; set; }
        private Button BonusesButton { get; set; }
        private Button HarvestingButton { get; set; }
        private Button RecipesButton { get; set; }
        private Button DecorButton { get; set; }
        private Button CosmeticsButton { get; set; }
        private Button SkillsButton { get; set; }
        private Button ChallengesButton { get; set; }
        private Button LoadoutsButton { get; set; }
        private Button EnhancementsButton { get; set; }

        private CharacterWindow Parent { get; set; }

        private CharacterWindowPanel CurrentPanel { get; set; }

        private CharacterStatsWindow StatsPanel { get; set; }
        private CharacterHarvestingWindow HarvestingPanel { get; set; }
        private CharacterBonusesPanel BonusesPanel { get; set; }
        private CharacterNameTagPanel NameTagPanel { get; set; }
        private CharacterCosmeticsPanel CosmeticsPanel { get; set; }
        private CharacterRecipePanel RecipesPanel { get; set; }
        private SkillsPanel SkillsPanel { get; set; }
        private CharacterChallengesPanel ChallengesPanel { get; set; }
        private CharacterLoadoutsPanel LoadoutsPanel { get; set; }
        private CharacterEnhancementsPanel EnhancementsPanel { get; set; }

        public CharacterWindowPanelController(Canvas gameCanvas, CharacterWindow parent)
        {
            Parent = parent;

            Container = new ImagePanel(gameCanvas, "CharacterWindowPanelContainer");

            InitializePanelSelectors();
            
            PanelContainer = new ImagePanel(Container, "PanelContainer");
            PanelHideButton = new Button(PanelContainer, "HidePanelButton");
            PanelHideButton.Clicked += HidePanelClicked;

            Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            StatsPanel = new CharacterStatsWindow(PanelContainer, Parent);
            StatsPanel.Hide();
            
            HarvestingPanel = new CharacterHarvestingWindow(PanelContainer);
            HarvestingPanel.Hide();

            BonusesPanel = new CharacterBonusesPanel(PanelContainer);
            BonusesPanel.Hide();

            NameTagPanel = new CharacterNameTagPanel(PanelContainer);
            NameTagPanel.Hide();

            CosmeticsPanel = new CharacterCosmeticsPanel(PanelContainer);
            CosmeticsPanel.Hide();

            RecipesPanel = new CharacterRecipePanel(PanelContainer);
            RecipesPanel.Hide();

            SkillsPanel = new SkillsPanel(PanelContainer);
            SkillsPanel.Hide();

            ChallengesPanel = new CharacterChallengesPanel(PanelContainer);
            ChallengesPanel.Hide();

            LoadoutsPanel = new CharacterLoadoutsPanel(PanelContainer);
            LoadoutsPanel.Hide();

            EnhancementsPanel = new CharacterEnhancementsPanel(PanelContainer);
            EnhancementsPanel.Hide();

            PositionToParent();
            Hide();
            CurrentPanel = StatsPanel; // Default to equipment
        }

        private void InitializePanelSelectors()
        {
            StatsButton = new Button(Container, "StatsPanelSelector")
            {
                Text = "STATS"
            };
            StatsButton.Clicked += StatsClicked;
            
            BonusesButton = new Button(Container, "BonusesPanelSelector")
            {
                Text = "BONUSES"
            };
            BonusesButton.Clicked += BonusesClicked;

            HarvestingButton = new Button(Container, "HarvestingPanelSelector")
            {
                Text = "GATHERING"
            };
            HarvestingButton.Clicked += HarvestingClicked;

            RecipesButton = new Button(Container, "RecipesPanelSelector")
            {
                Text = "RECIPES"
            };
            RecipesButton.Clicked += RecipesClicked;

            DecorButton = new Button(Container, "DecorPanelSelector")
            {
                Text = "NAME TAGS"
            };
            DecorButton.Clicked += DecorClicked;

            CosmeticsButton = new Button(Container, "CosmeticsPanelSelector")
            {
                Text = "COSMETICS"
            };
            CosmeticsButton.Clicked += CosmeticsClicked;

            SkillsButton = new Button(Container, "SkillsPanelSelector")
            {
                Text = "SKILLS"
            };
            SkillsButton.Clicked += SkillsButton_Clicked;

            LoadoutsButton = new Button(Container, "LoadoutsSelector")
            {
                Text = "LOADOUTS"
            };
            LoadoutsButton.Clicked += LoadoutsButton_Clicked;

            ChallengesButton = new Button(Container, "ChallengesPanelSelector")
            {
                Text = "CHALLENGES"
            };
            ChallengesButton.Clicked += ChallengesButton_Clicked;

            EnhancementsButton = new Button(Container, "EnhancementsSelector")
            {
                Text = "ENHANCEMENT"
            };
            EnhancementsButton.Clicked += EnhancementsButton_Clicked;

            PanelSelectors = new List<Button>
            {
                StatsButton,
                BonusesButton,
                HarvestingButton,
                RecipesButton,
                DecorButton,
                CosmeticsButton,
                SkillsButton,
                LoadoutsButton,
                ChallengesButton,
                EnhancementsButton,
            };
        }

        private void EnhancementsButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ChangePanel(CharacterPanelType.Enhancements);
        }

        private void LoadoutsButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ChangePanel(CharacterPanelType.Loadouts);
        }

        private void ChallengesButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ChangePanel(CharacterPanelType.Challenges);
        }

        private void SkillsButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ChangePanel(CharacterPanelType.Skills);
        }

        private void PositionToParent()
        {
            Container.X = Parent.X + Parent.Width - 4;
            Container.Y = Parent.Y - 4;
            PositionSelectors();
        }

        private void PositionSelectors()
        {
            var initY = 48;
            var yPadding = 40;
            var idx = 0;
            foreach (var selector in PanelSelectors)
            {
                selector.Y = initY + (yPadding * idx);
                selector.X = PanelContainer.IsHidden ? 
                    0 : 
                    PanelContainer.Width;

                idx++;
            }
        }

        public void Update()
        {
            if (!Parent.IsVisible())
            {
                Hide();
            }

            if (Container.IsHidden)
            {
                return;
            }

            CurrentPanel?.Update();

            PositionToParent();
        }

        public void ChangePanel(CharacterPanelType type)
        {
            PanelContainer.Show();
            CurrentPanel?.Hide();

            EnableNav();
            switch (type)
            {
                case CharacterPanelType.Stats:
                    StatsButton.Disable();
                    CurrentPanel = StatsPanel;
                    break;
                case CharacterPanelType.Bonuses:
                    BonusesButton.Disable();
                    CurrentPanel = BonusesPanel;
                    break;
                case CharacterPanelType.Harvesting:
                    HarvestingButton.Disable();
                    CurrentPanel = HarvestingPanel;
                    break;
                case CharacterPanelType.Recipes:
                    RecipesButton.Disable();
                    CurrentPanel = RecipesPanel;
                    break;
                case CharacterPanelType.NameTag:
                    DecorButton.Disable();
                    CurrentPanel = NameTagPanel;
                    break;
                case CharacterPanelType.Cosmetics:
                    CosmeticsButton.Disable();
                    CurrentPanel = CosmeticsPanel;
                    break;
                case CharacterPanelType.Skills:
                    SkillsButton.Disable();
                    CurrentPanel = SkillsPanel;
                    break;
                case CharacterPanelType.Challenges:
                    ChallengesButton.Disable();
                    CurrentPanel = ChallengesPanel;
                    break;
                case CharacterPanelType.Loadouts:
                    LoadoutsButton.Disable();
                    CurrentPanel = LoadoutsPanel;
                    break;
                case CharacterPanelType.Enhancements:
                    EnhancementsButton.Disable();
                    CurrentPanel = EnhancementsPanel;
                    break;
                default:
                    throw new ArgumentException($"Invalid enum for {nameof(type)}");
            }

            Interface.GameUi.CurrentCharacterPanel = type;

            CurrentPanel?.Show();
        }

        public void Hide()
        {
            HidePanel();
            Container.Hide();
            CurrentPanel?.Hide();
        }

        public void Show()
        {
            Container.Show();
        }

        public void HidePanel()
        {
            EnableNav();
            PanelContainer.Hide();
            CurrentPanel?.Hide();
        }

        public void ShowPanel()
        {
            PanelContainer.Show();
        }

        private void EnableNav()
        {
            foreach(var selector in PanelSelectors)
            {
                selector.Enable();
            }
        }

        private void HidePanelClicked(Base control, EventArgs args)
        {
            HidePanel();
        }

        private void StatsClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.Stats);
        }

        private void BonusesClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.Bonuses);
        }

        private void HarvestingClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.Harvesting);
        }

        private void RecipesClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.Recipes);
        }

        private void DecorClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.NameTag);
        } 
        
        private void CosmeticsClicked(Base control, EventArgs args)
        {
            ChangePanel(CharacterPanelType.Cosmetics);
        }
    }
}
