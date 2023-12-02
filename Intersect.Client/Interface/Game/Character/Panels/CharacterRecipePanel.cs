using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
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
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using Label = Intersect.Client.Framework.Gwen.Control.Label;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterRecipePanelController
    {
        public static bool Refresh { get; set; }
        
        public static bool ResetPositions { get; set; }

        public static List<RecipeDisplayPacket> Recipes { get; set; } = new List<RecipeDisplayPacket>();

        public static Dictionary<Guid, List<RecipeRequirementPacket>> ExpandedRecipes = new Dictionary<Guid, List<RecipeRequirementPacket>>();
    }

    public class CharacterRecipePanel : CharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Recipes;

        private ImagePanel SearchContainer { get; set; }
        private Label SearchLabel { get; set; }
        private ImagePanel SearchBg { get; set; }
        private TextBox SearchBar { get; set; }
        private string SearchTerm
        {
            get => SearchBar.Text;
            set => SearchBar.SetText(value);
        }
        private Button SearchClearButton { get; set; }

        private ImagePanel RecipeContainer { get; set; }
        private Label NoRecipesLabel { get; set; }
        private ScrollControl RecipeScrollContainer { get; set; }

        private int LastExpandedRowCount { get; set; }

        private ImagePanel CraftTypeBackground { get; set; }
        private Label CraftTypeLabel { get; set; }
        private ComboBox CraftTypeSelection { get; set; }

        private bool Refresh => CharacterRecipePanelController.Refresh;

        private ComponentList<GwenComponent> RecipeRows { get; set; } = new ComponentList<GwenComponent>();

        private List<RecipeDisplayPacket> Recipes => CharacterRecipePanelController.Recipes;

        private RecipeCraftType SelectedCraftType
        {
            get
            {
                var selectionStr = CraftTypeSelection.Text;
                return EnumExtensions.GetValueFromDescription<RecipeCraftType>(selectionStr);
            }
        }

        public CharacterRecipePanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Recipes");

            SearchContainer = new ImagePanel(mBackground, "SearchContainer");
            SearchLabel = new Label(SearchContainer, "SearchLabel")
            {
                Text = "Search:"
            };
            SearchBg = new ImagePanel(SearchContainer, "SearchBg");
            SearchBar = new TextBox(SearchBg, "SearchBar");
            SearchBar.TextChanged += SearchBar_TextChanged;
            SearchClearButton = new Button(SearchContainer, "ClearButton")
            {
                Text = "CLEAR"
            };
            SearchClearButton.Clicked += SearchClearButton_Clicked;

            RecipeContainer = new ImagePanel(mBackground, "RecipeContainer");
            NoRecipesLabel = new Label(RecipeContainer, "NoRecipesLabel");
            RecipeScrollContainer = new ScrollControl(RecipeContainer, "Recipes");

            CraftTypeBackground = new ImagePanel(mBackground, "CraftTypeBackground");
            CraftTypeLabel = new Label(CraftTypeBackground, "CraftTypeLabel");
            CraftTypeLabel.SetText("Craft Type");

            CraftTypeSelection = new ComboBox(CraftTypeBackground, "CraftTypeComboBox");

            var craftTypes = EnumExtensions.GetDescriptions(typeof(RecipeCraftType));
            foreach(string enVal in craftTypes.OrderBy(craft => craft).ToArray())
            {
                CraftTypeSelection.AddItem(enVal);
            }

            CraftTypeSelection.ItemSelected += CraftTypeSelection_ItemSelected;

            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void CraftTypeSelection_ItemSelected(Base sender, ItemSelectedEventArgs arguments)
        {
            PacketSender.SendRequestRecipes(SelectedCraftType);
        }

        public override void Show()
        {
            PacketSender.SendRequestRecipes(SelectedCraftType);
            Interface.InputBlockingElements.Add(SearchBar);
            SearchTerm = string.Empty;

            base.Show();
        }

        public override void Hide()
        {
            Interface.InputBlockingElements.Remove(SearchBar);

            ClearRecipes();

            base.Hide();
        }

        private void ClearRecipes()
        {
            CharacterRecipePanelController.ExpandedRecipes.Clear();
            RecipeScrollContainer.ClearCreatedChildren();
            RecipeRows?.DisposeAll();
        }

        public void CollapseAllRows()
        {
            foreach(var row in RecipeRows)
            {
                ((RecipeRowComponent)row).CollapseRequirements();
            }
        }

        public override void Update()
        {
            // A row has been expanded/unexpanded
            if (LastExpandedRowCount != CharacterRecipePanelController.ExpandedRecipes.Count)
            {
                // So update the rows
                foreach (var row in RecipeRows)
                {
                    ((RecipeRowComponent)row).Update();
                }
                RecalcContainerPositions();
            }
            LastExpandedRowCount = CharacterRecipePanelController.ExpandedRecipes.Count;

            if (!Refresh)
            {
                return;
            }

            NoRecipesLabel.IsHidden = (Recipes?.Count ?? 0) > 0;
            NoRecipesLabel.SetText($"No {SelectedCraftType.GetDescription()} recipes yet!");

            ClearRecipes();

            var idx = 0;
            foreach(var recipe in Recipes)
            {
                var descriptor = RecipeDescriptor.Get(recipe.DescriptorId);
                var name = descriptor.DisplayName ?? descriptor.Name ?? "NOT FOUND";
                if (!SearchHelper.IsSearchable(name, SearchTerm))
                {
                    continue;
                }

                var row = new RecipeRowComponent(RecipeScrollContainer, $"Recipe_{idx}", recipe.DescriptorId, recipe.IsUnlocked, recipe.Visible, RecipeRows);

                row.Initialize();
                row.SetPosition(row.X, row.Height * idx);

                if (idx % 2 == 1)
                {
                    row.SetBanding();
                }

                idx++;
            }

            CharacterRecipePanelController.Refresh = false;
        }

        public void RecalcContainerPositions()
        {
            var yPos = 0;
            foreach (var row in RecipeRows)
            {
                var castedRow = row as RecipeRowComponent;

                castedRow.SetPosition(castedRow.X, yPos);

                yPos += castedRow.Height;
            }

            CharacterRecipePanelController.ResetPositions = false;
        }

        private void SearchClearButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            SearchTerm = string.Empty;
        }

        private void SearchBar_TextChanged(Base sender, EventArgs arguments)
        {
            CollapseAllRows();
            CharacterRecipePanelController.Refresh = true;
        }
    }
}
