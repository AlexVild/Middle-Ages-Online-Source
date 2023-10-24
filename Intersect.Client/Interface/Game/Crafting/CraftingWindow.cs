using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Network.Packets.Server;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.Crafting
{

    public partial class CraftingWindow
    {

        public bool Crafting;

        private ImagePanel mBar;

        private ImagePanel mBarContainer;

        private long mBarTimer;

        private RecipeItem mCombinedItem;

        private Label mCombinedValue;

        private Button mCraft;

        private Button mCraftAll;

        private Guid mCraftId;

        private int AmountRemaining;

        private long WishlistCooldown;

        private const long WishlistCooldownTime = 500;

        //Controls
        private WindowControl mCraftWindow;

        private bool mInitialized = false;

        public bool Refresh = false;

        private ScrollControl mItemContainer;

        private List<RecipeItem> mItems = new List<RecipeItem>();

        private Label mLblIngredients;

        private Label mLblProduct;

        private Label mLblRecipes;

        //Objects
        private ListBox mRecipes;

        private List<Label> mValues = new List<Label>();

        private Label NeedsRecipeLabel;

        private GameTexture CanCraftBg => Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "crafting.png");
        private GameTexture CantCraftBg => Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "crafting_locked_recipe.png");

        private GameTexture InWishlistTexture => Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "craft_favorite_active.png");
        private GameTexture NotWishlistTexture => Globals.ContentManager.GetTexture(GameContentManager.TextureType.Gui, "craft_favorite_inactive.png");

        private Button WishlishButton;

        public CraftingWindow(Canvas gameCanvas)
        {
            mCraftWindow = new WindowControl(gameCanvas, Globals.ActiveCraftingTable.Name.ToUpper(), false, "CraftingWindow");
            mCraftWindow.DisableResizing();

            mItemContainer = new ScrollControl(mCraftWindow, "IngredientsContainer");

            //Labels
            mLblRecipes = new Label(mCraftWindow, "RecipesTitle");
            mLblRecipes.Text = Strings.Crafting.recipes;

            mLblIngredients = new Label(mCraftWindow, "IngredientsTitle");
            mLblIngredients.Text = Strings.Crafting.ingredients;

            mLblProduct = new Label(mCraftWindow, "ProductLabel");
            mLblProduct.Text = Strings.Crafting.product;

            //Recipe list
            mRecipes = new ListBox(mCraftWindow, "RecipesList");

            //Progress Bar
            mBarContainer = new ImagePanel(mCraftWindow, "ProgressBarContainer");
            mBar = new ImagePanel(mBarContainer, "ProgressBar");

            //Load the craft button
            mCraft = new Button(mCraftWindow, "CraftButton");
            mCraft.SetText(Strings.Crafting.craft);
            mCraft.Clicked += craft_Clicked;

            //Craft all button
            mCraftAll = new Button(mCraftWindow, "CraftAllButton");
            mCraftAll.SetText(Strings.Crafting.craftall.ToString("1"));
            mCraftAll.Clicked += craftAll_Clicked;

            NeedsRecipeLabel = new Label(mCraftWindow, "NeedsRecipeLabel");

            WishlishButton = new Button(mCraftWindow, "WishlistButton");
            WishlishButton.Clicked += WishlishButton_Clicked;
            WishlishButton.SetToolTipText("Add/Remove from Wishlist");

            _CraftingWindow();

            mCraftWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            WishlishButton.SetTooltipGraphicsMAO();

            Interface.InputBlockingElements.Add(mCraftWindow);

            Globals.Me.InventoryUpdatedDelegate = () =>
            {
                //Refresh crafting window items
                LoadCraftItems(mCraftId);
            };
        }

        private void WishlishButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (mCraftId == Guid.Empty)
            {
                return;
            }

            if (WishlistCooldown > Timing.Global.Milliseconds)
            {
                return;
            }

            WishlistCooldown = Timing.Global.Milliseconds + WishlistCooldownTime;

            if (CharacterWishlistController.Wishlist.Contains(mCraftId))
            {
                PacketSender.SendRemoveWishlistItem(mCraftId);
            }
            else
            {
                PacketSender.SendAddWishlistItem(mCraftId);
            }
        }

        //Location
        public int X => mCraftWindow.X;

        public int Y => mCraftWindow.Y;

        private void LoadCraftItems(Guid id)
        {
            ClearComponents();

            //Combined item
            mCraftId = id;

            if (!Globals.ActiveCraftingTable.Crafts.Contains(id))
            {
                return;
            }

            var craft = Globals.ActiveCraftingTable.Crafts.Get(id);

            if (craft == null)
            {
                return;
            }

            mCombinedItem = new RecipeItem(mCraftWindow, new CraftIngredient(craft.ItemId, 0))
            {
                Container = new ImagePanel(mCraftWindow, "CraftedItem")
            };

            mCombinedItem.Setup("CraftedItemIcon");
            mCombinedValue = new Label(mCombinedItem.Container, "CraftedItemQuantity");

            mCombinedItem.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            mCombinedItem.LoadItem();
            mCombinedValue.Show();
            var quantity = Math.Max(craft.Quantity, 1);
            var itm = ItemBase.Get(craft.ItemId);
            if (itm == null || !itm.IsStackable)
            {
                quantity = 1;
            }

            mCombinedValue.Text = quantity.ToString();

            for (var i = 0; i < mItems.Count; i++)
            {
                //Clear the old item description box
                if (mItems[i].DescWindow != null)
                {
                    mItems[i].DescWindow.Dispose();
                }

                mItemContainer.RemoveChild(mItems[i].Container, true);
            }

            mItems.Clear();
            mValues.Clear();

            //Quickly Look through the inventory and create a catalog of what items we have, and how many
            var itemsAndQuantities = Globals.Me.GetInventoryItemsAndQuantities();

            var craftableQuantity = -1;

            var hasRecipe = true;
            if (craft.Recipe != Guid.Empty && !Globals.Me.UnlockedRecipes.Contains(craft.Recipe))
            {
                hasRecipe = false;
                mCraftWindow.SetImage(CantCraftBg, CantCraftBg.Name, WindowControl.ControlState.Active);
                mCraftWindow.SetImage(CantCraftBg, CantCraftBg.Name, WindowControl.ControlState.Inactive);
                mCraft.Disable();
                mCraftAll.Disable();
                NeedsRecipeLabel.Show();

                var recipe = RecipeDescriptor.Get(craft.Recipe);
                var recipeName = recipe.DisplayName ?? recipe.Name ?? "NOT FOUND";
                var craftType = recipe.CraftType;

                if (craftType == RecipeCraftType.Other)
                {
                    NeedsRecipeLabel.SetText($"Requires recipe: {recipeName}");
                }
                else
                {
                    NeedsRecipeLabel.SetText($"Requires {recipe.DisplayCraftType} recipe: {recipeName}");
                }
                mBar.Hide();
                mBarContainer.Hide();
            }
            else
            {
                mCraftWindow.SetImage(CanCraftBg, CanCraftBg.Name, WindowControl.ControlState.Active);
                mCraftWindow.SetImage(CanCraftBg, CanCraftBg.Name, WindowControl.ControlState.Inactive);
                mCraft.Enable();
                mCraftAll.Enable();
                NeedsRecipeLabel.Hide();
                mBar.Show();
                mBarContainer.Show();
            }

            for (var i = 0; i < craft.Ingredients.Count; i++)
            {
                var descriptor = ItemBase.Get(craft.Ingredients[i].ItemId);
                if (descriptor == default)
                {
                    continue;
                }

                var component = new CraftItemComponent(mItemContainer,
                    $"CraftItem_{i}", descriptor,
                    craft.Ingredients[i].Quantity,
                    mItemContainer.Parent.X,
                    mItemContainer.Parent.Y + mItemContainer.Parent.Y,
                    hasRecipe ? CustomColors.General.GeneralCompleted : CustomColors.General.GeneralMuted,
                    hasRecipe ? CustomColors.General.GeneralDisabled : CustomColors.General.GeneralMuted);

                CraftItemComponents.Add(component);

                component.Initialize();

                mItemContainer.AddContentTo(component.Background, i, 4, 8);
            }

            //Update craft buttons!
            DetermineCraftAllVisibility(craftableQuantity);
            mCraftAll.UserData = craftableQuantity;
        }

        private void DetermineCraftAllVisibility(int quantity)
        {
            if (quantity > 1)
            {
                mCraftAll.Show();
                mCraftAll.SetText(Strings.Crafting.craftall.ToString(quantity.ToString()));
            }
            else
            {
                mCraftAll.Hide();
            }
        }

        private void StopCrafting()
        {
            Crafting = false;
            mCraftWindow.IsClosable = true;
            mBar.Width = 0;
            AmountRemaining = 0;
            PacketSender.SendCraftItem(default, 0);
        }

        private void StartCrafting(int amountToCraft)
        {
            AmountRemaining = amountToCraft;
            Crafting = true;
            mBarTimer = Timing.Global.Milliseconds;
            mCraftWindow.IsClosable = false;
            mCraftAll.Hide();
            PacketSender.SendCraftItem(mCraftId, amountToCraft);
        }

        public void ReceiveStatusUpdate(int amountRemaining)
        {
            AmountRemaining = amountRemaining;
            mBarTimer = Timing.Global.Milliseconds;
        }

        public void Close()
        {
            if (Crafting == false)
            {
                mCraftWindow.Close();
            }
        }

        public bool IsVisible()
        {
            return !mCraftWindow.IsHidden;
        }

        public void Hide()
        {
            ClearComponents();
            if (Crafting == false)
            {
                mCraftWindow.IsHidden = true;
            }
        }

        //Load new recepie
        void tmpNode_DoubleClicked(Base sender, ClickedEventArgs arguments)
        {
            if (Crafting == false)
            {
                LoadCraftItems((Guid)((ListBoxRow)sender).UserData);
            }
        }

        private void ToggleStartCrafting(int amountToCraft = 1)
        {
            if (Crafting)
            {
                StopCrafting();

                LoadCraftItems(mCraftId);

                return;
            }

            if (Globals.Me.CanCraftItem(mCraftId))
            {
                StartCrafting(amountToCraft);

                return;
            }

            ChatboxMsg.AddMessage(new ChatboxMsg(Strings.Crafting.incorrectresources, CustomColors.Alerts.Declined, Enums.ChatMessageType.Crafting));
        }

        //Craft the item
        void craft_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ToggleStartCrafting(1);
        }

        //Craft all the items
        void craftAll_Clicked(Base sender, ClickedEventArgs arguments)
        {
            var amountToCraft = (int)mCraftAll.UserData;
            ToggleStartCrafting(amountToCraft);
        }

        //Update the crafting bar
        public void Update()
        {
            LoadCrafts();
            var resetSelection = false;

            WishlishButton.IsHidden = mCraftId == Guid.Empty;
            if (!WishlishButton.IsHidden)
            {
                if (CharacterWishlistController.Wishlist.Contains(mCraftId))
                {
                    WishlishButton.SetImage(InWishlistTexture, InWishlistTexture.Name, Button.ControlState.Normal);
                }
                else
                {
                    WishlishButton.SetImage(NotWishlistTexture, NotWishlistTexture.Name, Button.ControlState.Normal);
                }
            }

            if (Refresh)
            {
                // Clear things that were already populated
                mRecipes.Clear();
            }

            if (!mInitialized || Refresh)
            {
                var displayIndex = 0;
                for (var i = 0; i < mCrafts.Count; ++i)
                {
                    var activeCraft = CraftBase.Get(mCrafts[i]);
                    if (activeCraft == null)
                    {
                        continue;
                    }

                    if (Globals.ActiveCraftingTable.HiddenCrafts.Contains(activeCraft.Id))
                    {
                        if (activeCraft.Id == mCraftId)
                        {
                            // The craft the user had seelcted is no longer available - clear it from their selection
                            resetSelection = true;
                        }
                        continue;
                    }

                    displayIndex++;
                    var tmpRow = mRecipes?.AddRow(Strings.Crafting.recipe.ToString(displayIndex, activeCraft.Name));
                    if (tmpRow == null)
                    {
                        continue;
                    }

                    tmpRow.UserData = mCrafts[i];
                    tmpRow.DoubleClicked += tmpNode_DoubleClicked;
                    tmpRow.Clicked += tmpNode_DoubleClicked;
                    if (activeCraft.Recipe != Guid.Empty && !Globals.Me.UnlockedRecipes.Contains(activeCraft.Recipe))
                    {
                        tmpRow.SetTextColor(new Color(255, 100, 100, 100));
                    }
                    else
                    {
                        tmpRow.SetTextColor(new Color(255, 50, 19, 0));
                    }
                    tmpRow.RenderColor = new Color(100, 232, 208, 170);
                }

                //Load the craft data
                if ((Globals.ActiveCraftingTable?.Crafts?.Count > 0 && !Refresh) || resetSelection)
                {
                    // Don't initialize on a craft we can't do
                    var validCrafts = Globals.ActiveCraftingTable.Crafts
                        .ToList()
                        .Where(c => !Globals.ActiveCraftingTable.HiddenCrafts.Contains(c))
                        .ToArray();

                    // if we don't now HAVE any valid crafts...
                    if (validCrafts.Length <= 0)
                    {
                        // End the table
                        StopCrafting();
                        Close();
                    }

                    LoadCraftItems(validCrafts[0]);
                }
                mInitialized = true;
                Refresh = false;
            }

            if (!Crafting)
            {
                mCraft.SetText(Strings.Crafting.craft);
                mBar.Width = 0;

                return;
            }
            else
            {
                mCraft.SetText(Strings.Crafting.craftstop);
            }

            var craft = CraftBase.Get(mCraftId);
            if (craft == null)
            {
                return;
            }

            var delta = Timing.Global.Milliseconds - mBarTimer;
            if (delta > craft.Time)
            {
                delta = craft.Time;
                Crafting = false;
                if (mCraftWindow != null)
                {
                    mCraftWindow.IsClosable = true;
                }

                mBar.Width = 0;
            }

            var ratio = craft.Time == 0 ? 0 : Convert.ToDecimal(delta) / Convert.ToDecimal(craft.Time);
            var width = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)(ratio * mBarContainer?.Width ?? 0), 4);

            if (mBar == null)
            {
                return;
            }

            mBar.SetTextureRect(
                0, 0, Convert.ToInt32(ratio * mBar.Texture?.GetWidth() ?? 0), mBar.Texture?.GetHeight() ?? 0
            );

            mBar.Width = Convert.ToInt32(width);

            Crafting = AmountRemaining > 0;
        }
    }

    public partial class CraftingWindow
    {
        private TextBox mSearch;
        private ImagePanel mTextboxBg;
        private List<Guid> mCrafts;
        private Label mSearchLabel;
        private Button mClearButton;

        private ComponentList<CraftItemComponent> CraftItemComponents { get; set; } = new ComponentList<CraftItemComponent>();

        private void _CraftingWindow()
        {
            mTextboxBg = new ImagePanel(mCraftWindow, "Textbox");
            mSearch = new TextBox(mTextboxBg, "SearchBox");
            mSearchLabel = new Label(mCraftWindow, "SearchLabel");
            mSearchLabel.Text = "Search:";
            mClearButton = new Button(mCraftWindow, "ClearButton");
            mClearButton.Pressed += mClear_Pressed;
            mClearButton.Text = "Clear";
            mSearch.TextChanged += mSearch_textChanged;
        }

        private void mSearch_textChanged(Base control, EventArgs args)
        {
            LoadCrafts();
            Refresh = true;
            LoadCraftItems(mCraftId);
            mRecipes.ScrollToTop();
        }

        private void LoadCrafts()
        {
            var items = Globals.ActiveCraftingTable.Crafts
                .Where(craftId => CraftIsValid(craftId))
                .ToList();
            mCrafts = items;
        }

        private bool CraftIsValid(Guid craftId)
        {
            var craft = CraftBase.Get(craftId);

            return !Globals.ActiveCraftingTable.HiddenCrafts.Contains(craftId) && SearchHelper.IsSearchable(craft.Name, mSearch.Text);
        }

        private void mClear_Pressed(Base control, EventArgs args)
        {
            mSearch.Text = string.Empty;
            LoadCrafts();
            Refresh = true;
            LoadCraftItems(mCraftId);
        }

        public void ClearComponents()
        {
            mItemContainer?.ClearCreatedChildren();
            CraftItemComponents?.DisposeAll();

            if (mCombinedItem != null)
            {
                mCraftWindow.Children.Remove(mCombinedItem.Container);
            }

            //Clear the old item description box
            if (mCombinedItem != null && mCombinedItem.DescWindow != null)
            {
                mCombinedItem.DescWindow.Dispose();
            }
        }
    }
}
