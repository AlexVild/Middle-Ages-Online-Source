using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Character;
using Intersect.Client.Interface.Game.Character.Panels;
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

        private List<CosmeticItem> Items { get; set; }
        private int EquipmentSlot { get; set; }

        private Dictionary<int, List<Guid>> UnlockedCosmetics => CharacterCosmeticsPanelController.UnlockedCosmetics;
        private List<Guid> CosmeticItems;

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

            RefreshUnlockedCosmetics();
        }

        private void ClearCosmetics()
        {
            CosmeticItemContainer.ClearCreatedChildren();
            CosmeticItems?.Clear();
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
                var cosmeticItem = new CosmeticItem(idx, CosmeticItemContainer);
                cosmeticItem.Setup();

                idx++;
            }
        }
    }
}
