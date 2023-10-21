using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.General.UpgradeStation;
using Intersect.Client.Interface.Game.Crafting;
using Intersect.Client.Interface.Game.Enhancement;
using Intersect.Client.Interface.ScreenAnimations;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.UpgradeStation
{
    public class UpgradeStationWindow : GameWindow
    {
        protected override string FileName => "UpgradeStationWindow";
        protected override string Title => "UPGRADE STATION";

        private UpgradeCompleteWindow CompletionWindow { get; set; }

        private UpgradeStationInterface UpgradeStation => Globals.Me?.UpgradeStation;

        private Label ItemNameLabel { get; set; }

        private ImagePanel UpgradingBg { get; set; }
        private Label NoCraftsLabel { get; set; }

        private ImagePanel ItemIcon { get; set; }
        private EnhancementItemIcon ItemIconComponent { get; set;}
        private ImagePanel UpgradeListContainer { get; set; }
        private ListBox UpgradeList { get; set; }


        private ImagePanel UpgradeBg { get; set; }
        private ImagePanel UpgradeItemIcon { get; set; }
        private EnhancementItemIcon UpgradeItemIconComponent { get; set; }
        private ImagePanel RecipeBg { get; set; }
        private ScrollControl RecipeContainer { get; set; }
        private List<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
        private List<Label> RecipeValues = new List<Label>();

        private Button CraftButton { get; set; }
        private Button CancelButton { get; set; }

        private ImagePanel CurrencyIcon { get; set; }
        private Label CostLabel { get; set; }

        private AnvilAnim CraftAnimation { get; set; }

        public UpgradeStationWindow(Base gameCanvas) : base(gameCanvas)
        {
            CompletionWindow = new UpgradeCompleteWindow(this, gameCanvas);
        }

        Guid NewItemId { get; set; }
        ItemProperties NewProperties { get; set; }

        protected override void PreInitialization()
        {
            ItemNameLabel = new Label(Background, "ItemName");
            
            UpgradingBg = new ImagePanel(Background, "UpgradingBg");
            ItemIcon = new ImagePanel(UpgradingBg, "ItemIcon");
            ItemIconComponent = new EnhancementItemIcon(0, ItemIcon, Background.X, Background.Y + 40);
            UpgradeListContainer = new ImagePanel(UpgradingBg, "UpgradeListBg");
            UpgradeList = new ListBox(UpgradeListContainer, "Upgrades");
            NoCraftsLabel = new Label(UpgradeListContainer, "NoCraftsLabel")
            {
                Text = "No upgrades available!"
            };

            UpgradeBg = new ImagePanel(Background, "UpgradeBg");
            UpgradeItemIcon = new ImagePanel(UpgradeBg, "UpgradeItemIcon");
            UpgradeItemIconComponent = new EnhancementItemIcon(0, UpgradeItemIcon, Background.X, Background.Y + 40);
            RecipeBg = new ImagePanel(UpgradeBg, "RecipeBg");
            RecipeContainer = new ScrollControl(RecipeBg, "Recipes");

            CraftButton = new Button(Background, "CraftButton")
            {
                Text = "Craft"
            };
            CancelButton = new Button(Background, "CancelButton")
            {
                Text = "Cancel"
            };

            CurrencyIcon = new ImagePanel(Background, "CurrencyIcon");
            CostLabel = new Label(Background, "CostLabel");

            CancelButton.Clicked += CancelButton_Clicked;
            CraftButton.Clicked += CraftButton_Clicked;

            CraftAnimation = new AnvilAnim(ShowCompletionWindow);
        }

        private void CraftButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            PacketSender.SendRequestUpgrade(UpgradeStation.SelectedCraftId);
        }

        private void CancelButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Close();
        }

        protected override void PostInitialization()
        {
            ItemIconComponent.Setup();
            UpgradeItemIconComponent.Setup();
        }

        public override void Show()
        {
            CraftAnimation.ResetAnimation();
            if (UpgradeStation.UpgradeItem == default)
            {
#pragma warning disable CA2000 // Dispose objects before losing scope
                _ = new InputBox(
                    Strings.UpgradeStation.NoWeaponEquipped,
                    Strings.UpgradeStation.NoWeaponEquippedPrompt, true,
                    InputBox.InputType.OkayOnly, Close, null, null
                );
#pragma warning restore CA2000 // Dispose objects before losing scope
                UpgradeStation.IsOpen = false;
                return;
            }

            var itemId = UpgradeStation.UpgradeItem?.Id ?? Guid.Empty;
            ItemNameLabel.SetText(ItemBase.GetName(itemId));
            ItemIconComponent.Update(itemId, UpgradeStation.UpgradeItemProperties, 1);

            var currTxt = UpgradeStation.Currency?.Icon ?? string.Empty;
            CurrencyIcon.Texture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Item, currTxt);

            RefreshCraftList();

            base.Show();
        }

        public override void Hide()
        {
            ClearRecipes();
            UpgradeList.RemoveAllRows();
            base.Hide();
        }

        public override void UpdateShown()
        {
            if (!UpgradeStation.IsOpen)
            {
                Close();
                return;
            }

            CompletionWindow.Hide();
            ItemIconComponent.SetHoverPanelLocation(Background.X + 6, Background.Y + 40);
            UpgradeItemIconComponent.SetHoverPanelLocation(Background.X + 6, Background.Y + 40);

            UpgradeBg.IsHidden = UpgradeStation.SelectedCraftId == Guid.Empty;
            NoCraftsLabel.IsHidden = UpgradeStation.Crafts.Length > 0;
            UpgradeList.IsHidden = !NoCraftsLabel.IsHidden;

            if (!UpgradeStation.RefreshUi)
            {
                return;
            }

            if (!UpgradeBg.IsHidden)
            {
                RefreshCraftInfo();
            }

            RefreshCost();

            UpgradeStation.RefreshUi = false;
        }

        public override void UpdateHidden()
        {
            if (!UpgradeStation.IsOpen)
            {
                return;
            }

            if (CraftAnimation.Done)
            {
                return;
            }
            CraftAnimation.Draw();
            return;

            base.UpdateHidden();
        }

        private void RefreshCraftList()
        {
            UpgradeList.RemoveAllRows();
            foreach (var craftId in UpgradeStation.Crafts)
            {
                var craftName = CraftBase.GetName(craftId);
                var row = UpgradeList.AddRow(craftName);
                row.UserData = craftId;
                row.Selected += Craft_Selected;

                row.SetTextColor(new Color(50, 19, 0));
                row.RenderColor = new Color(100, 232, 208, 170);
            }
        }

        private void RefreshCost()
        {
            CostLabel.SetText("0");
            if (UpgradeStation.SelectedCraftId == Guid.Empty || !UpgradeStation.UpgradeItem.WeaponUpgrades.TryGetValue(UpgradeStation.SelectedCraftId, out var cost))
            {
                return;
            }

            CostLabel.SetText($"{(int)Math.Floor(cost * UpgradeStation.CostMultiplier)}");
        }

        private void RefreshCraftInfo() 
        {
            var craft = CraftBase.Get(UpgradeStation.SelectedCraftId);
            if (craft == default)
            {
                UpgradeStation.SelectedCraftId = Guid.Empty;
                return;
            }

            UpgradeItemIconComponent.Update(craft.ItemId, UpgradeStation.UpgradeItemProperties, 1);

            var itemsAndQuantities = Globals.Me.GetInventoryItemsAndQuantities();

            ClearRecipes();
            var craftableQuantity = -1;
            for (var i = 0; i < craft.Ingredients.Count; i++)
            {
                var ingredient = craft.Ingredients[i];
                var recipeItem = new RecipeItem(Background, ingredient);
                RecipeItems.Add(recipeItem);
                recipeItem.Container = new ImagePanel(RecipeContainer, "CraftingIngredient");
                recipeItem.Setup("IngredientItemIcon");

                var lblTemp = new Label(recipeItem.Container, "IngredientItemValue");
                var onHand = 0;
                if (itemsAndQuantities.ContainsKey(ingredient.ItemId))
                {
                    onHand = itemsAndQuantities[ingredient.ItemId];
                }

                lblTemp.Text = $"{onHand}/{ingredient.Quantity}";
                var possibleToCraft = (int)Math.Floor(onHand / (float)ingredient.Quantity);

                if (craftableQuantity == -1 || possibleToCraft < craftableQuantity)
                {
                    craftableQuantity = possibleToCraft;
                }

                RecipeValues.Add(lblTemp);

                recipeItem.Container.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                recipeItem.LoadItem();

                var xPadding = RecipeItems[i].Container.Margin.Left + RecipeItems[i].Container.Margin.Right;
                var yPadding = RecipeItems[i].Container.Margin.Top + RecipeItems[i].Container.Margin.Bottom;
                RecipeItems[i]
                    .Container.SetPosition(
                        i %
                        ((RecipeContainer.Width - RecipeContainer.GetVerticalScrollBar().Width) /
                         (RecipeItems[i].Container.Width + xPadding)) *
                        (RecipeItems[i].Container.Width + xPadding) +
                        xPadding,
                        i /
                        ((RecipeContainer.Width - RecipeContainer.GetVerticalScrollBar().Width) /
                         (RecipeItems[i].Container.Width + xPadding)) *
                        (RecipeItems[i].Container.Height + yPadding) +
                        yPadding
                    );
            }
        }

        private void ClearRecipes()
        {
            RecipeContainer.ClearCreatedChildren();
            RecipeItems.Clear();
            RecipeValues.Clear();
        }

        private void Craft_Selected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            var craftId = (Guid)((ListBoxRow)sender).UserData;
            UpgradeStation.SelectedCraftId = craftId;
        }

        public void ProcessCompletedUpgrade(Guid upgradedItemId, ItemProperties properties)
        {
            Hide();
            NewItemId = upgradedItemId;
            NewProperties = new ItemProperties(properties);
        }

        private void ShowCompletionWindow()
        {
            Flash.FlashScreen(1000, new Color(255, 255, 255, 255), 150);
            CompletionWindow.Show(NewItemId, NewProperties);
        }

        private void Close(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Close()
        {
            CompletionWindow.Hide();
            Globals.Me?.UpgradeStation?.Close();
            PacketSender.SendCloseUpgradeStation();
            base.Close();
        }
    }
}
