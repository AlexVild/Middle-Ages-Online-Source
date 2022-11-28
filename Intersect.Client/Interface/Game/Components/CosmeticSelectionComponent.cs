using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character;
using Intersect.Client.Interface.Game.Character.Panels;
using Intersect.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.Components
{
    public class CosmeticSelectionComponent : GwenComponent
    {
        private Label Title { get; set; }
        private string TitleText { get; set; }

        private ImagePanel ContainerImage { get; set; }
        private string ContainerImageName { get; set; }
        private GameTexture ContainerImageTexture { get; set; }

        private ImagePanel ScrollContainer { get; set; }
        private ScrollControl CosmeticItemContainer { get; set; }

        private List<CosmeticItem> Items { get; set; } = new List<CosmeticItem>();
        private int EquipmentSlot { get; set; }

        private Dictionary<int, List<Guid>> UnlockedCosmetics => CharacterCosmeticsPanelController.UnlockedCosmetics;
        private List<Guid> CosmeticItems;

        public int X => ParentContainer.X;
        public int Y
        {
            get => ParentContainer.Y;
            set => ParentContainer.SetPosition(ParentContainer.X, value);
        }

        public int Height => ParentContainer.Height;

        public CosmeticSelectionComponent(Base parent, 
            string containerName, 
            string title,
            string image,
            int equipmentSlot,
            ComponentList<GwenComponent> referenceList = null) 
            : base(parent, containerName, "CosmeticSelectionComponent", referenceList)
        {
            TitleText = title;
            ContainerImageName = image;
            EquipmentSlot = equipmentSlot;

            ContainerImageTexture = Globals.ContentManager.GetTexture(
                Framework.File_Management.GameContentManager.TextureType.Gui, 
                ContainerImageName
            );
        }

        public override void Initialize()
        {
            SelfContainer = new ImagePanel(ParentContainer, ComponentName);

            Title = new Label(SelfContainer, "Title")
            {
                Text = TitleText
            };

            ContainerImage = new ImagePanel(SelfContainer, "Image");

            ScrollContainer = new ImagePanel(SelfContainer, "ScrollParent");

            CosmeticItemContainer = new ScrollControl(ScrollContainer, "CosmeticItemContainer");

            base.Initialize();
            FitParentToComponent();

            ContainerImage.Texture = ContainerImageTexture;
        }

        public void SetPosition(float x, float y)
        {
            ParentContainer.SetPosition(x, y);
        }

        public void ClearCosmetics()
        {
            CosmeticItemContainer.ClearCreatedChildren();
            CosmeticItems?.Clear();
            Items?.Clear();
        }

        private void RefreshUnlockedCosmetics()
        {
            ClearCosmetics();
            if (!UnlockedCosmetics.TryGetValue(EquipmentSlot, out CosmeticItems))
            {
                return;
            }

            var idx = 0;
            foreach(var cosmetic in CosmeticItems)
            {
                var cosmeticItem = new CosmeticItem(idx, CosmeticItemContainer, cosmetic, EquipmentSlot);
                cosmeticItem.Setup();

                var xPadding = cosmeticItem.Pnl.Margin.Left + cosmeticItem.Pnl.Margin.Right;
                var yPadding = cosmeticItem.Pnl.Margin.Top + cosmeticItem.Pnl.Margin.Bottom;

                cosmeticItem.SetPosition(
                    idx %
                    ((CosmeticItemContainer.Width - CosmeticItemContainer.GetVerticalScrollBar().Width) / (cosmeticItem.Pnl.Width + xPadding)) *
                    (cosmeticItem.Pnl.Width + xPadding) +
                    xPadding,
                    idx /
                    ((CosmeticItemContainer.Width - CosmeticItemContainer.GetVerticalScrollBar().Width) / (cosmeticItem.Pnl.Width + xPadding)) *
                    (cosmeticItem.Pnl.Height + yPadding) +
                    yPadding
                );

                idx++;
                Items.Add(cosmeticItem);
            }
        }

        public void Update()
        {
            if (CharacterCosmeticsPanelController.RefreshCosmeticsPanel)
            {
                RefreshUnlockedCosmetics();
            }
        }

        public void UpdateEquippedStatus()
        {
            foreach (var item in Items)
            {
                item.UpdateEquipped();
            }
        }


        public void Search(string term)
        {
            var idx = 0;
            foreach(var item in Items)
            {
                if (!SearchHelper.IsSearchable(item.Name, term))
                {
                    item.Pnl.Hide();
                    continue;
                }

                item.Pnl.Show();
                var xPadding = item.Pnl.Margin.Left + item.Pnl.Margin.Right;
                var yPadding = item.Pnl.Margin.Top + item.Pnl.Margin.Bottom;

                item.SetPosition(
                    idx %
                    ((CosmeticItemContainer.Width - CosmeticItemContainer.GetVerticalScrollBar().Width) / (item.Pnl.Width + xPadding)) *
                    (item.Pnl.Width + xPadding) +
                    xPadding,
                    idx /
                    ((CosmeticItemContainer.Width - CosmeticItemContainer.GetVerticalScrollBar().Width) / (item.Pnl.Width + xPadding)) *
                    (item.Pnl.Height + yPadding) +
                    yPadding
                );

                idx++;
            }
        }
    }
}
