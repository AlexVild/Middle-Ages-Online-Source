using Intersect.Client.Core;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Networking;
using Intersect.Client.Utilities;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterEnhancementsPanelController
    {
        public static bool Loading = false;

        public static bool Refresh { get; set; } = false;

        public static List<Guid> KnownEnhancements => Globals.Me?.KnownEnhancements ?? new List<Guid>();

        public static bool IsOpen { get; set; } = false;

        public static Guid WeaponTypeFilter { get; set; } = Guid.Empty;

        public static List<EnhancementMenuItem> Enhancements = new List<EnhancementMenuItem>();

        public static bool FiltersLoaded { get; set; } = false;

        public static void UpdateEnhancementList(string searchTerm = "")
        {
            Enhancements.Clear();

            var filteredEnhancemnets = EnhancementDescriptor.GetAll()
                .Where((enhancement) =>
                {
                    // Guid.Empty == No filter
                    if (WeaponTypeFilter == Guid.Empty)
                    {
                        return true;
                    }
                    
                    return enhancement.ValidWeaponTypes.ContainsKey(WeaponTypeFilter);
                })
                .Where((enhancement) =>
                {
                    return SearchHelper.IsSearchable(enhancement.Name, searchTerm);
                })
                .Select(enhancement => new EnhancementMenuItem(enhancement.Id, KnownEnhancements.Contains(enhancement.Id)))
                .ToArray();

            Enhancements.AddRange(filteredEnhancemnets);
            Loading = false;
        }
    }

    public class CharacterEnhancementsPanel : SearchableCharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Enhancements;

        private EnhancementMenuItem mSelectedEnhancement { get; set; }
        private EnhancementDescriptionWindow EnhancementDescription { get; set; }

        List<EnhancementMenuItem> Enhancements => CharacterEnhancementsPanelController.Enhancements;

        public ImagePanel EnhancementListBg { get; set; }
        public ListBox EnhancementList { get; set; }

        public ImagePanel EnhancementDescriptionBg { get; set; }
        public Label EnhancementNameLabel { get; set; }

        private ImagePanel FilterBackground { get; set; }
        private Label FilterLabel { get; set; }
        private ComboBox FilterSelector { get; set; }

        private ItemBase[] CachedItems { get; set; }

        private ImagePanel EnhancementItemBg { get; set; }
        private Label UnlockStatusLabel { get; set; }
        private Label PrerequisiteLabel { get; set; }
        private Label Prerequisites { get; set; }
        private Label ItemsTaughtLabel { get; set; }
        private ScrollControl EnhancementItemContainer { get; set; }
        private ComponentList<EnhancementItemComponent> EnhancementItems = new ComponentList<EnhancementItemComponent>();

        private GameTexture PrereqMetTexture;
        private GameTexture PrereqNotMetTexture;

        public CharacterEnhancementsPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Enhancements");

            PrereqMetTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "character_enhancement_items_bg.png");
            PrereqNotMetTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "character_enhancement_items_locked_bg.png");

            Initialize();

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            Enhancements.Clear();
            CharacterEnhancementsPanelController.IsOpen = true;
            CharacterEnhancementsPanelController.Loading = true;
            PacketSender.SendRequestKnownEnhancements();
            CachedItems = ItemBase.GetAll();

            if (!CharacterEnhancementsPanelController.FiltersLoaded)
            {
                InitializeFilterOptions();
                CharacterEnhancementsPanelController.FiltersLoaded = true;
            }

            ResetSelection();

            base.Show();
        }

        private void ResetSelection()
        {
            EnhancementList.ScrollToTop();
            EnhancementList.UnselectAll();
            mSelectedEnhancement = null;
            ClearEnhancementItems();
            EnhancementItemBg.Hide();
        }

        private void InitializeFilterOptions()
        {
            // Only gets weapon types for which there exist enhancements
            List<Guid> weaponTypes = EnhancementDescriptor.GetAll()
                .Select(en => en.ValidWeaponTypes.Keys.ToArray())
                .Aggregate(new List<Guid>(), (List<Guid> prev, Guid[] next) =>
                {
                    prev.AddRange(next);
                    return prev.Distinct().ToList();
                });
            foreach (Guid weaponTypeId in weaponTypes)
            {
                var descriptor = WeaponTypeDescriptor.Get(weaponTypeId);
                FilterSelector.AddItem(descriptor.VisibleName, userData: weaponTypeId);
            }
        }

        protected override void Initialize()
        {
            // Initialize search bar
            base.Initialize();

            FilterBackground = new ImagePanel(mBackground, "FilterBackground");
            FilterLabel = new Label(FilterBackground, "FilterLabel");
            FilterLabel.SetText("Weapon Type");

            FilterSelector = new ComboBox(FilterBackground, "FilterSelector");

            FilterSelector.AddItem("[All]", userData: Guid.Empty);

            FilterSelector.ItemSelected += FilterSelector_ItemSelected;

            EnhancementListBg = new ImagePanel(mBackground, "EnhancementListBackground");
            EnhancementList = new ListBox(EnhancementListBg, "EnhancementList");

            EnhancementItemBg = new ImagePanel(mBackground, "EnhancementItemBg")
            {
                IsHidden = true
            };
            EnhancementItemContainer = new ScrollControl(EnhancementItemBg, "EnhancementItemContainer");
            UnlockStatusLabel = new Label(EnhancementItemBg, "UnlockStatus");
            PrerequisiteLabel = new Label(EnhancementItemBg, "Prerequisites")
            {
                Text = "NOT YET LEARNABLE"
            };
            Prerequisites = new Label(EnhancementItemBg, "PrerequisiteList");
            ItemsTaughtLabel = new Label(EnhancementItemBg, "ItemsTaught")
            {
                Text = "STUDY ITEMS"
            };
        }

        private void FilterSelector_ItemSelected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            Guid id = (Guid)FilterSelector.SelectedItem.UserData;

            CharacterEnhancementsPanelController.WeaponTypeFilter = id;
            Refresh();
            ResetSelection();
        }

        public override void Hide()
        {
            CharacterEnhancementsPanelController.IsOpen = false;
            EnhancementList.RemoveAllRows();
            EnhancementDescription?.Dispose();
            base.Hide();
        }

        public override void Update()
        {
            if (IsHidden)
            {
                return;
            }

            if (!CharacterEnhancementsPanelController.Loading && EnhancementList.RowCount == 0)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            CharacterEnhancementsPanelController.UpdateEnhancementList(SearchTerm);
            
            EnhancementList.Clear();
            foreach (var enhancement in Enhancements.OrderBy(en => en.Descriptor.Name).ToArray())
            {
                var tmpRow = EnhancementList.AddRow($"{enhancement?.Descriptor?.Name ?? "NOT FOUND"}");
                tmpRow.UserData = enhancement;

                if (enhancement.Known)
                {
                    tmpRow.SetTextColor(new Color(255, 30, 74, 157));
                }
                else
                {
                    if (enhancement.Descriptor.PrerequisitesMet(Globals.Me.KnownEnhancements))
                    {
                        tmpRow.SetTextColor(new Color(50, 19, 0));
                    }
                    else
                    {
                        tmpRow.SetTextColor(new Color(255, 100, 100, 100));
                    }
                }

                tmpRow.RenderColor = new Color(100, 232, 208, 170);
                tmpRow.Selected += Enhancement_Selected;
                tmpRow.HoverEnter += Enhancement_HoverEnter;
                tmpRow.HoverLeave += Enhancement_HoverLeave;
            }
            EnhancementList.ScrollToTop();
        }


        private void Enhancement_HoverLeave(Base sender, EventArgs arguments)
        {
            EnhancementDescription?.Dispose();
        }

        private void Enhancement_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            var hoveredEnhancement = (EnhancementMenuItem)((ListBoxRow)sender).UserData;
            EnhancementDescription?.Dispose();

            var x = mBackground?.Parent?.Parent.X ?? 0;
            var y = (mBackground?.Parent?.Parent?.Y ?? 0) + (mBackground.Y + EnhancementListBg.Y);
            EnhancementDescription = new EnhancementDescriptionWindow(hoveredEnhancement.Id, string.Empty, x, y);
            EnhancementDescription.Show();
        }

        private void Enhancement_Selected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            mSelectedEnhancement = (EnhancementMenuItem)((ListBoxRow)sender).UserData;
            ClearEnhancementItems();

            if (!mSelectedEnhancement.Descriptor.PrerequisitesMet(Globals.Me.KnownEnhancements))
            {
                EnhancementItemBg.Texture = PrereqNotMetTexture;

                PrerequisiteLabel.Show();
                Prerequisites.Show();

                var prereqs = mSelectedEnhancement.Descriptor.PrerequisiteEnhancements
                    .Where(enId => !Globals.Me.KnownEnhancements.Contains(enId))
                    .Select(enId => EnhancementDescriptor.GetName(enId));

                Prerequisites.SetText($"Requires: {string.Join(", ", prereqs)}");

                EnhancementItemContainer.Hide();
                UnlockStatusLabel.Hide();
                ItemsTaughtLabel.Hide();

                return;
            }

            EnhancementItemContainer.ScrollToTop();
            PrerequisiteLabel.Hide();
            Prerequisites.Hide();
            EnhancementItemContainer.Show();
            UnlockStatusLabel.Show();
            ItemsTaughtLabel.Show();
            EnhancementItemBg.Texture = PrereqMetTexture;

            var studyItems = CachedItems
                .Where((item) =>
                {
                    if (item.ItemType == Enums.ItemTypes.Enhancement)
                    {
                        return item.EnhancementId == mSelectedEnhancement.Id;
                    }
                    else if (item.ItemType == Enums.ItemTypes.Equipment && item.EquipmentSlot == Options.WeaponIndex)
                    {
                        return item.StudyEnhancement == mSelectedEnhancement.Id;
                    }

                    return false;
                })
                .OrderByDescending(item => item.StudyChance);

            var idx = 0;
            var x = mBackground?.Parent?.Parent.X ?? 0;
            var y = (mBackground?.Parent?.Parent?.Y ?? 0) + (mBackground.Y + EnhancementItemBg.Y);
            foreach (var item in studyItems)
            {
                if (item.Id == Guid.Empty)
                {
                    continue;
                }

                var enhancementItem = new EnhancementItemComponent(EnhancementItemContainer, 
                    $"EnhancementItem_{idx}", 
                    "inventoryitem.png", 
                    item.Icon, 
                    Framework.File_Management.GameContentManager.TextureType.Item, 
                    1, 
                    4, 
                    item.Id, 
                    mSelectedEnhancement.Id, x, y);
                enhancementItem.Initialize();

                var xPadding = 16;
                var yPadding = 16;

                enhancementItem.SetPosition(
                    idx %
                    (EnhancementItemContainer.GetContentWidth() / (enhancementItem.Width + xPadding)) *
                    (enhancementItem.Width + xPadding) +
                    xPadding,
                    idx /
                    (EnhancementItemContainer.GetContentWidth() / (enhancementItem.Width + xPadding)) *
                    (enhancementItem.Height + yPadding) +
                    yPadding
                );

                EnhancementItems.Add(enhancementItem);

                idx++;
            }

            UnlockStatusLabel.SetText(
                mSelectedEnhancement.Known ?
                "Enhancement Known!" :
                ""
                );

            EnhancementItemBg.Show();
        }

        private void ClearEnhancementItems()
        {
            EnhancementItemContainer.ClearCreatedChildren();
            EnhancementItems.DisposeAll();
        }

        protected override void SearchTextChanged()
        {
            Refresh();
        }

        protected override void ClearSearchClicked()
        {
            Refresh();
        }
    }

    public class EnhancementMenuItem
    {
        public EnhancementDescriptor Descriptor => EnhancementDescriptor.Get(Id);
        public Guid Id { get; set; }
        public bool Known { get; set; }

        public EnhancementMenuItem(Guid id, bool known)
        {
            Id = id;
            Known = known;
        }
    }
}
