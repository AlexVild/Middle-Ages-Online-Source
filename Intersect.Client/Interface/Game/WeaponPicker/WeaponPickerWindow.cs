using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Items;
using Intersect.Client.Networking;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Game.WeaponPicker
{
    public static class WeaponPickerController
    {
        public static WeaponPickerResult ResultType { get; set; }
    }

    public class WeaponPickerWindow : GameWindow
    {
        protected override string FileName => "WeaponPicker";

        protected override string Title => "WEAPON PICKER";

        Label Prompt { get; set; }

        ScrollControl WeaponContainer { get; set; }

        ComponentList<WeaponPickerWeaponComponent> Weapons { get; set; }

        public WeaponPickerWindow(Base gameCanvas) : base(gameCanvas)
        {
        }

        protected override void PreInitialization()
        {
            Prompt = new Label(Background, "Prompt");
        }

        protected override void PostInitialization()
        {
            return;
        }

        public override void UpdateShown()
        {
            return;
        }

        public override void Show()
        {
            Prompt.SetText(
                WeaponPickerController.ResultType == WeaponPickerResult.Enhancement ?
                "Select a weapon to enhance..." :
                "Select a weapon to upgrade..."
            );
            
            RefreshWeaponSelection();

            base.Show();
        }

        protected override void Close()
        {
            ClearWeaponContainer();
            PacketSender.SendWeaponPickerResponse(-1);
            base.Close();
        }

        private void RefreshWeaponSelection()
        {
            ClearWeaponContainer();

            var playerWeapons = Globals.Me.Inventory
                .Select((invItem, i) => new KeyValuePair<int, Item>(i, invItem))
                .Where(item => item.Value?.Base?.ItemType == Enums.ItemTypes.Equipment && item.Value?.Base?.EquipmentSlot == Options.WeaponIndex)
                .ToArray();

            var idx = 0;
            foreach (var invItem in playerWeapons)
            {
                var weapon = invItem.Value;
                var invSlot = invItem.Key;
                if (weapon.Base.Id == Guid.Empty)
                {
                    continue;
                }

                var item = new WeaponPickerWeaponComponent(WeaponContainer,
                    $"EnhancementItem_{idx}",
                    "inventoryitem.png",
                    weapon.Base.Icon,
                    Framework.File_Management.GameContentManager.TextureType.Item,
                    1,
                    4,
                    weapon,
                    invSlot,
                    Background.X,
                    Background.Y
                );
                item.Initialize();

                WeaponContainer.AddContentTo(item.ParentContainer, idx, 16, 16);
                Weapons.Add(item);

                idx++;
            }
        }

        private void ClearWeaponContainer()
        {
            WeaponContainer.ClearCreatedChildren();
            Weapons.DisposeAll();
        }
    }
}
