using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Interface.Game.Crafting;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Intersect.Client.Interface.Game.InputBox;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterWishlistController
    {
        public static List<Guid> Wishlist { get; set; } = new List<Guid>();

        public static bool ServerUpdate { get; set; }
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

        private ComponentList<CraftItemComponent> CraftItemComponents { get; set; } = new ComponentList<CraftItemComponent>();

        private ItemDescriptionWindow CraftItemDescription { get; set; }

        private Button RemoveFromWishlistButton { get; set; }

        public CharacterWishlistPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Wishlist");

            Initialize();
            mBackground.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
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

            RemoveFromWishlistButton = new Button(mBackground, "RemoveFromWishlistButton")
            {
                Text = "REMOVE"
            };
            RemoveFromWishlistButton.Clicked += RemoveFromWishlistButton_Clicked;

            CraftImage = new ImageFrameComponent(CraftBg, "CraftImage", "loot_item.png", string.Empty, GameContentManager.TextureType.Item, 1, 32);
        }

        private void RemoveFromWishlistButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            if (SelectedCraft == default)
            {
                return;
            }

            _ = new InputBox("REMOVE FROM WISHLIST",
                $"Do you want to remove {SelectedCraft.Name} from your wishlist?",
                false,
                InputType.YesNo,
                (snd, args) =>
                {
                    PacketSender.SendRemoveWishlistItem(SelectedCraft.Id);
                    ResetSelection();
                },
                null,
                null);
        }

        private void Image_HoverLeave(Base sender, EventArgs arguments)
        {
            CraftItemDescription?.Hide();
        }

        private void Image_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            CraftItemDescription?.Show();
        }

        public void PostInitialize()
        {
            CraftImage.Initialize();
            
            CraftImage.Image.HoverEnter += Image_HoverEnter;
            CraftImage.Image.HoverLeave += Image_HoverLeave;
        }

        public override void Show()
        {
            base.Show();

            ClearRecipes();
            ResetSelection();
            Refresh();
        }

        public override void Hide()
        {
            base.Hide();

            ClearRecipes();
        }

        public override void Update()
        {
            if (CharacterWishlistController.ServerUpdate)
            {
                Refresh();
            }

            var emptyWishlist = Crafts.Count <= 0;

            EmptyWishlistLabel.IsHidden = !emptyWishlist;
            WishlistBg.IsHidden = emptyWishlist;
            CraftBg.IsHidden = SelectedCraft == null;
            RemoveFromWishlistButton.IsHidden = SelectedCraft == null;

            CharacterWishlistController.ServerUpdate = false;
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
            SelectedCraft = null;
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
            CraftItemComponents.DisposeAll();
            CraftItemDescription?.Dispose();
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

                var component = new CraftItemComponent(IngredientContainer,
                    $"CraftItem_{idx}", descriptor,
                    item.Quantity,
                    mParentContainer.Parent.X,
                    mParentContainer.Parent.Y + IngredientContainer.Parent.Y,
                    CustomColors.General.GeneralCompleted,
                    CustomColors.General.GeneralDisabled);

                CraftItemComponents.Add(component);

                component.Initialize();

                IngredientContainer.AddContentTo(component.Background, idx, 4, 8);

                idx++;
            }

            var selectedCraftItem = ItemBase.Get(SelectedCraft.ItemId);
            CraftImage.SetImageTexture(selectedCraftItem.Icon);
            CraftItemDescription = new ItemDescriptionWindow(selectedCraftItem,
                1,
                mParentContainer.Parent.X,
                mParentContainer.Parent.Y + IngredientContainer.Parent.Y,
                null);
            CraftItemDescription.Hide();
        }
    }
}
