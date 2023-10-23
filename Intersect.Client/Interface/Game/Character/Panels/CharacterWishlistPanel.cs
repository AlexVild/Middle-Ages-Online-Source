using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Interface.Game.Crafting;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterWishlistController
    {
        public static List<Guid> Wishlist { get; set; } = new List<Guid>();
    }

    public class CharacterWishlistPanel : SearchableCharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Wishlist;

        private ImagePanel WishlistBg { get; set; }

        private ListBox Wishlist { get; set; }

        private List<Guid> Crafts => CharacterWishlistController.Wishlist;

        private CraftBase SelectedCraft;

        private Label EmptyWishlistLabel { get; set; }

        private ImagePanel CraftBg { get; set; }

        private ImageFrameComponent CraftImage { get; set; }

        private ScrollControl IngredientContainer { get; set; }

        private List<RecipeItem> IngredientImages { get; set; } = new List<RecipeItem>();

        public CharacterWishlistPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Wishlist");

            Initialize();
            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            PostInitialize();
        }

        protected override void Initialize()
        {
            // Initialize search bar
            base.Initialize();

            WishlistBg = new ImagePanel(mBackground, "WishlistBg");
            Wishlist = new ListBox(WishlistBg, "Wishlist");

            EmptyWishlistLabel = new Label(mBackground, "EmptyWishlistLabel")
            {
                Text = "Your craft wishlist is empty!"
            };

            CraftBg = new ImagePanel(mBackground, "CraftingBg");
            IngredientContainer = new ScrollControl(CraftBg, "IngredientContainer");
        }

        public void PostInitialize()
        {

        }

        public override void Show()
        {
            base.Show();

            ClearRecipes();
            ResetSelection();
            Refresh();
        }

        public override void Update()
        {
            return;
        }

        protected override void ClearSearchClicked()
        {
            ResetSelection();
            Refresh();
        }

        protected override void SearchTextChanged()
        {
            ResetSelection();
            Refresh();
        }

        private void ResetSelection()
        {
            Wishlist.ScrollToTop();
            Wishlist.UnselectAll();
        }

        private void Refresh()
        {
            var currentCrafts = Crafts
                .Select(craftId => CraftBase.Get(craftId))
                .Where(craft => SearchHelper.IsSearchable(craft.Name, SearchTerm))
                .OrderBy(craft => craft.Name)
                .ToArray();

            Wishlist.Clear();

            foreach (var craft in currentCrafts)
            {
                var craftRow = Wishlist.AddRow($"{craft?.Name ?? "NOT FOUND"}");
                craftRow.UserData = craft;

                craftRow.SetTextColor(new Color(50, 19, 0));
                craftRow.RenderColor = new Color(100, 232, 208, 170);

                craftRow.Selected += CraftRow_Selected;
            }

            var emptyWishlist = Crafts.Count <= 0;

            EmptyWishlistLabel.IsHidden = !emptyWishlist;
            WishlistBg.IsHidden = emptyWishlist;
            CraftBg.IsHidden = SelectedCraft == null;
        }

        private void CraftRow_Selected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            SelectedCraft = (CraftBase)((ListBoxRow)sender).UserData;

            CraftBg.IsHidden = false;

            LoadRecipe();
        }

        private void ClearRecipes()
        {
            IngredientContainer.ClearCreatedChildren();
            IngredientImages.Clear();
        }

        private void LoadRecipe()
        {
            ClearRecipes();
            
            var idx = 0;
            foreach (var item in SelectedCraft.Ingredients)
            {
                // TODO do this properly
                var descriptor = ItemBase.Get(item.ItemId);
                if (descriptor == default)
                {
                    continue;
                }

                var recipeItem = new RecipeItem(IngredientContainer, item);
                recipeItem.Setup($"ingredient_{idx}");
                IngredientContainer.AddContentTo(recipeItem.Pnl, idx, 16, 16);

                recipeItem.LoadItem();

                IngredientImages.Add(recipeItem);
                idx++;
            }
        }
    }
}
