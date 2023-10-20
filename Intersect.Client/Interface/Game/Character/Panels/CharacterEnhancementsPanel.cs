using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
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

        List<EnhancementMenuItem> Enhancements => CharacterEnhancementsPanelController.Enhancements;
        List<Guid> KnownEnhancements => CharacterEnhancementsPanelController.KnownEnhancements;
        Guid CurrentFilter => CharacterEnhancementsPanelController.WeaponTypeFilter;

        public ImagePanel EnhancementListBg { get; set; }
        public ListBox EnhancementList { get; set; }

        public ImagePanel EnhancementDescriptionBg { get; set; }
        public Label EnhancementNameLabel { get; set; }

        private ImagePanel FilterBackground { get; set; }
        private Label FilterLabel { get; set; }
        private ComboBox FilterSelector { get; set; }

        public CharacterEnhancementsPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Enhancements");

            Initialize();

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public override void Show()
        {
            Enhancements.Clear();
            CharacterEnhancementsPanelController.IsOpen = true;
            CharacterEnhancementsPanelController.Loading = true;
            PacketSender.SendRequestKnownEnhancements();

            if (!CharacterEnhancementsPanelController.FiltersLoaded)
            {
                InitializeFilterOptions();
                CharacterEnhancementsPanelController.FiltersLoaded = true;
            }

            EnhancementList.ScrollToTop();
            EnhancementList.UnselectAll();

            base.Show();
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
        }

        private void FilterSelector_ItemSelected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            Guid id = (Guid)FilterSelector.SelectedItem.UserData;

            CharacterEnhancementsPanelController.WeaponTypeFilter = id;
            Refresh();
        }

        public override void Hide()
        {
            CharacterEnhancementsPanelController.IsOpen = false;
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


                if (!enhancement.Known)
                {
                    tmpRow.SetTextColor(new Color(50, 19, 0));
                }
                else
                {
                    tmpRow.SetTextColor(new Color(255, 100, 100, 100));
                }

                tmpRow.RenderColor = new Color(100, 232, 208, 170);
            }
            EnhancementList.ScrollToTop();
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
