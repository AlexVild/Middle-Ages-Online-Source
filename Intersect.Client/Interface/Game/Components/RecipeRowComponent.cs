using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.Components
{
    public class RecipeRowComponent : GwenComponent
    {
        string Frame => Unlocked ? "character_resource_unlocked_bg.png" : "character_resource_disabled_bg.png";
        private ImageFrameComponent Image { get; set; }
        private ImagePanel RequirementsPanel { get; set; }

        private Label Title { get; set; }
        private Label HintTemplate { get; set; }
        private RichLabel Hint { get; set; }

        private Button ExpandButton { get; set; }

        private Color SecondaryColor => new Color(255, 169, 169, 169);
        private Color PrimaryColor => new Color(255, 255, 255, 255);
        private Color TitleColor => Unlocked ? PrimaryColor : SecondaryColor;
        private Color HintColor => SecondaryColor;

        public RecipeDescriptor Recipe;

        TextureType RecipeRextureType => TextureType.Item;

        Guid RecipeId { get; set; }

        public bool Unlocked { get; set; }

        public int X => ParentContainer.X;
        public int Y => ParentContainer.Y;

        public int Height => ParentContainer.Height;
        public int Width => ParentContainer.Width;

        private int OriginalWidth;
        private int OriginalHeight;

        private bool IsExpanded { get; set; }

        public List<RecipeRequirementPacket> Requirements => CharacterRecipePanelController.ExpandedRecipes.ContainsKey(RecipeId) ?
            CharacterRecipePanelController.ExpandedRecipes[RecipeId] : null;

        private ComponentList<RecipeRequirementComponent> RequirementComponents = new ComponentList<RecipeRequirementComponent>();

        private GameTexture BandingTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "character_harvest_banding.png");

        private GameTexture DownButtonTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "downarrownormal.png");
        private GameTexture DownButtonHoverTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "downarrowhover.png");
        private GameTexture DownButtonClickedTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "downarrowclicked.png");

        private GameTexture UpButtonTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "uparrownormal.png");
        private GameTexture UpButtonHoverTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "uparrowhover.png");
        private GameTexture UpButtonClickedTexture => Globals.ContentManager.GetTexture(TextureType.Gui, "uparrowclicked.png");

        public RecipeRowComponent(Base parent,
            string containerName,
            Guid recipeId,
            bool unlocked,
            ComponentList<GwenComponent> referenceList = null
            ) : base(parent, containerName, "RecipeRowComponent", referenceList)
        {
            RecipeId = recipeId;
            Unlocked = unlocked;

            Recipe = RecipeDescriptor.Get(RecipeId);
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);
            Image = new ImageFrameComponent(SelfContainer, "RecipeImage", Frame, Recipe.Image, RecipeRextureType, 1, 8);

            Title = new Label(SelfContainer, "RecipeName")
            {
                Text = Recipe?.DisplayName ?? Recipe?.Name ?? "NOT FOUND"
            };

            HintTemplate = new Label(SelfContainer, "Hint");
            Hint = new RichLabel(SelfContainer);

            ExpandButton = new Button(SelfContainer, "ToggleRequirementsButton");
            ExpandButton.SetToolTipText("Show/Hide Requirements");
            ExpandButton.Clicked += ExpandButton_Clicked;

            base.Initialize();
            FitParentToComponent();

            OriginalHeight = SelfContainer.Height;
            OriginalWidth = SelfContainer.Width;

            Title.SetTextColor(TitleColor, Label.ControlState.Normal);
            HintTemplate.SetTextColor(HintColor, Label.ControlState.Normal);

            Image.Initialize();
            if (!Unlocked)
            {
                Image.SetImageRenderColor(new Color(160, 255, 255, 255));
            }
            FormatHint();
        }

        private void ExpandButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            ToggleRequirements();
            CharacterRecipePanelController.ResetPositions = true;
        }

        public void SetBanding()
        {
            SelfContainer.Texture = BandingTexture;
        }

        public void ToggleRequirements()
        {
            if (RequirementsPanel == null)
            {
                var currentBottom = SelfContainer.Bottom;

                RequirementsPanel = new ImagePanel(SelfContainer, "RecipeImage");
                RequirementsPanel.SetPosition(SelfContainer.X, currentBottom);
                RequirementsPanel.SetSize(SelfContainer.Width, 64);

                FitParentToComponent();

                ExpandButton.SetImage(UpButtonTexture, UpButtonTexture.Name, Button.ControlState.Normal);
                ExpandButton.SetImage(UpButtonHoverTexture, UpButtonHoverTexture.Name, Button.ControlState.Hovered);
                ExpandButton.SetImage(UpButtonClickedTexture, UpButtonClickedTexture.Name, Button.ControlState.Clicked);
                
                PacketSender.SendRequestRecipeRequirements(RecipeId);
                return;
            }
            CollapseRequirements();
        }

        public void CollapseRequirements()
        {
            IsExpanded = false;
            ExpandButton.SetImage(DownButtonTexture, DownButtonTexture.Name, Button.ControlState.Normal);
            ExpandButton.SetImage(DownButtonHoverTexture, DownButtonHoverTexture.Name, Button.ControlState.Hovered);
            ExpandButton.SetImage(DownButtonClickedTexture, DownButtonClickedTexture.Name, Button.ControlState.Clicked);

            CharacterRecipePanelController.ExpandedRecipes.Remove(RecipeId);
            SelfContainer.SetSize(SelfContainer.Width, SelfContainer.Height - RequirementsPanel.Height);
            RequirementsPanel.Dispose();
            RequirementsPanel = null;
            FitParentToComponent();
        }

        public void Update()
        {
            if (RequirementsPanel != null && Requirements != null && !IsExpanded)
            {
                UpdateRequirements();
            }
            else if (!IsExpanded)
            {
                SelfContainer.SetSize(OriginalWidth, OriginalHeight);
                ClearRequirements();
                FitParentToComponent();
            }
        }

        private void ClearRequirements()
        {
            RequirementComponents.DisposeAll();
            RequirementComponents.Clear();
        }

        private void UpdateRequirements()
        {
            var yPos = 0;
            var width = 0;
            foreach(var requirement in Requirements)
            {
                var hint = GenerateRequirementHint(requirement);

                var row = new RecipeRequirementComponent(RequirementsPanel,
                    "RecipeRequirementRow",
                    hint,
                    requirement.Progress,
                    requirement.Amount,
                    requirement.Amount - requirement.Progress,
                    requirement.Completed
                    );
                RequirementComponents.Add(row);

                row.Initialize();

                row.SetPosition(row.X, yPos);
                yPos += row.Height;
                width = row.Width;
            }

            RequirementsPanel.SetSize(width, yPos);
            SelfContainer.SetSize(SelfContainer.Width, SelfContainer.Height + RequirementsPanel.Height);

            FitParentToComponent();
            IsExpanded = true;
        }

        private string GenerateRequirementHint(RecipeRequirementPacket requirement)
        {
            var recipe = RecipeDescriptor.Get(requirement.RecipeId);
            if (recipe == default)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(requirement.Hint))
            {
                return requirement.Hint;
            }

            try
            {
                GameObjectType triggerObj = requirement.TriggerType.GetRelatedTable();
                var trigger = triggerObj.GetLookup().Get(requirement.TriggerId);

                if (trigger == default)
                {
                    return string.Empty;
                }

                var pluralName = trigger.Name.Trim();
                if (pluralName[pluralName.Length - 1].ToString().ToLower() == "s")
                {
                    pluralName = pluralName.Remove(pluralName.Length - 1);
                }

                switch (requirement.TriggerType)
                {
                    case RecipeTrigger.EnemyKilled:
                        return requirement.Amount > 1 ?
                            $"Slay {requirement.Amount} {pluralName}s" :
                            $"Slay one {trigger.Name}";
                    case RecipeTrigger.CraftCrafted:
                        return requirement.Amount > 1 ?
                            $"Craft {requirement.Amount} {pluralName}s" :
                            $"Craft one {trigger.Name}";
                    case RecipeTrigger.ItemObtained:
                        return requirement.Amount > 1 ?
                            $"Obtain {requirement.Amount} {pluralName}s" :
                            $"Obtain one {trigger.Name}";
                    case RecipeTrigger.ResourceHarvested:
                        return requirement.Amount > 1 ?
                            $"Harvest {requirement.Amount} {pluralName}s" :
                            $"Harvest one {trigger.Name}";
                    case RecipeTrigger.SpellLearned:
                        return $"Learn spell: {trigger.Name}";
                    default:
                        return string.Empty;
                }
            }
            catch (ArgumentException e)
            {
                return string.Empty;
            }
        }

        private void FormatHint()
        {
            Hint.ClearText();
            Hint.Width = SelfContainer.Width - Title.X - 30;
            Hint.X = Title.X + 8;
            Hint.Y = Title.Bottom + 4;
            Hint.AddText(Recipe?.Hint ?? string.Empty, HintTemplate);
            Hint.SizeToChildren(false, true);
        }

        public void SetPosition(float x, float y)
        {
            ParentContainer.SetPosition(x, y);
        }
    }
}
