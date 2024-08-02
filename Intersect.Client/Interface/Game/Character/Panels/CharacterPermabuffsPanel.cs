using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.General;
using Intersect.Client.General.Enhancement;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Interface.Game.DescriptionWindows;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Character.Panels
{
    public static class CharacterPermabuffsPanelController
    {
        public static bool Refresh { get; set; } = false;

        public static HashSet<Guid> KnownPermabuffs => Globals.UsedPermabuffs ?? new HashSet<Guid>();

        public static bool IsOpen { get; set; } = false;

        public static List<PermabuffMenuItem> Permabuffs = new List<PermabuffMenuItem>();

        public static bool FiltersLoaded { get; set; } = false;

        public static void UpdatePermabuffList(string searchTerm = "")
        {
            Permabuffs.Clear();

            var filteredPermabuffs = ItemBase.GetAll()
                .Where((itm) =>
                {
                    return itm.ItemType == ItemTypes.Permabuff && SearchHelper.IsSearchable(itm.Name, searchTerm);
                })
                .Select(item => new PermabuffMenuItem(item.Id, KnownPermabuffs.Contains(item.Id)))
                .ToArray();

            Permabuffs.AddRange(filteredPermabuffs);
        }
    }

    public class CharacterPermabuffsPanel : SearchableCharacterWindowPanel
    {
        public CharacterPanelType Type = CharacterPanelType.Permabuffs;

        List<PermabuffMenuItem> Permabuffs => CharacterPermabuffsPanelController.Permabuffs;

        private PermabuffMenuItem SelectedPermabuff { get; set; }
        private ItemDescriptionWindow ItemDescription { get; set; }
        public ImagePanel PermabuffListBg { get; set; }
        public ListBox PermabuffList { get; set; }
        public ImagePanel PermabuffDescriptionBg { get; set; }
        public Label PermabuffNameLabel { get; set; }
        private Label UnlockStatusLabel { get; set; }
        private ScrollControl HintContainer { get; set; }
        private RichLabel Hint { get; set; }
        private Label HintTemplate { get; set; }

        public CharacterPermabuffsPanel(ImagePanel panelBackground)
        {
            mParentContainer = panelBackground;
            mBackground = new ImagePanel(mParentContainer, "CharacterWindowMAO_Permabuffs");

            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

            PermabuffListBg = new ImagePanel(mBackground, "PermabuffListBg");
            PermabuffList = new ListBox(PermabuffListBg, "PermabuffList");

            PermabuffDescriptionBg = new ImagePanel(mBackground, "PermabuffDescriptionBg");
            PermabuffNameLabel = new Label(PermabuffDescriptionBg, "PermabuffName");
            UnlockStatusLabel = new Label(PermabuffDescriptionBg, "UnlockStatusLabel");
            HintContainer = new ScrollControl(PermabuffDescriptionBg, "HintContainer");
            HintTemplate = new Label(HintContainer, "HintTemplate");
            Hint = new RichLabel(HintContainer);

            mBackground.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame,
                Graphics.Renderer.GetResolutionString());
        }

        private void Refresh()
        {
            CharacterPermabuffsPanelController.UpdatePermabuffList(SearchTerm);

            PermabuffList.Clear();
            foreach (var permabuff in Permabuffs.OrderBy(en => en.Descriptor.Name).ToArray())
            {
                var tmpRow = PermabuffList.AddRow($"{permabuff?.Descriptor?.Name ?? "NOT FOUND"}");
                tmpRow.UserData = permabuff;

                if (permabuff.Known)
                {
                    tmpRow.SetTextColor(new Color(255, 30, 74, 157));
                }
                else
                {
                    tmpRow.SetTextColor(new Color(50, 19, 0));
                }

                tmpRow.RenderColor = new Color(100, 232, 208, 170);
                tmpRow.Selected += PermaBuff_Selected;
                tmpRow.HoverEnter += PermaBuff_HoverEnter;
                tmpRow.HoverLeave += PermaBuff_HoverLeave;
            }
            PermabuffList.ScrollToTop();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Update() 
        { 
            if (IsHidden)
            {
                return;
            }

            if (PermabuffList.RowCount == 0)
            {
                Refresh();
            }
        }

        public override void Hide()
        {
            base.Hide();
        }

        protected override void SearchTextChanged()
        {
            Refresh();
        }

        protected override void ClearSearchClicked()
        {
            Refresh();
        }

        private void PermaBuff_HoverLeave(Base sender, EventArgs arguments)
        {
            ItemDescription?.Dispose();
        }

        private void PermaBuff_HoverEnter(Base sender, EventArgs arguments)
        {
            if (InputHandler.MouseFocus != null)
            {
                return;
            }

            var hoveredPermabuff = (PermabuffMenuItem)((ListBoxRow)sender).UserData;
            ItemDescription?.Dispose();

            var x = mBackground?.Parent?.Parent.X ?? 0;
            var y = (mBackground?.Parent?.Parent?.Y ?? 0) + (mBackground.Y + PermabuffListBg.Y);
            ItemDescription = new ItemDescriptionWindow(ItemBase.Get(hoveredPermabuff.Id), 0, x, y, null);
            ItemDescription.Show();
        }

        private void PermaBuff_Selected(Base sender, Framework.Gwen.Control.EventArguments.ItemSelectedEventArgs arguments)
        {
            SelectedPermabuff = (PermabuffMenuItem)((ListBoxRow)sender).UserData;

            HintContainer.ScrollToTop();
            PermabuffDescriptionBg.Show();

            var item = ItemBase.Get(SelectedPermabuff.Id);
            var hint = string.IsNullOrEmpty(item?.Hint) ? "HINT MISSING" : item.Hint;

            Hint.SetText(hint.Trim(), HintTemplate, HintContainer.Width - 32);

            UnlockStatusLabel.SetText(
                SelectedPermabuff.Known ?
                "Permabuff Unlocked!" :
                ""
                );
        }
    }

    public class PermabuffMenuItem
    {
        public Guid Id { get; set; }
        public bool Known { get; set; }

        public ItemBase Descriptor => ItemBase.Get(Id);

        public PermabuffMenuItem(Guid id, bool known)
        {
            Id = id;
            Known = known;
        }
    }
}
