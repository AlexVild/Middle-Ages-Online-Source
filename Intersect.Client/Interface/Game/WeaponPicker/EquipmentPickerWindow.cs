using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Interface.Components;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Items;
using Intersect.Client.Networking;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.WeaponPicker
{
    public static class EquipmentPickerController
    {
        public static WeaponPickerResult ResultType { get; set; }

        public static int SelectedInventorySlot { get; set; }

        public static bool TryGetSelectedWeapon(out Item selectedWeapon)
        {
            selectedWeapon = Globals.Me?.Inventory?.ElementAtOrDefault(SelectedInventorySlot);
            return selectedWeapon != default;
        }
    }

    public class EquipmentPickerWindow : GameWindow
    {
        protected override string FileName => "WeaponPicker";

        protected override string Title => "WEAPON PICKER";

        Label Prompt { get; set; }

        ScrollControl WeaponContainer { get; set; }

        ComponentList<WeaponPickerWeaponComponent> Weapons { get; set; } = new ComponentList<WeaponPickerWeaponComponent>();

        public EquipmentPickerWindow(Base gameCanvas) : base(gameCanvas)
        {
        }

        protected override void PreInitialization()
        {
            Prompt = new Label(Background, "Prompt");
            WeaponContainer = new ScrollControl(Background, "WeaponContainer");
        }

        protected override void PostInitialization()
        {
            return;
        }

        public override void UpdateShown()
        {
            foreach (var weapon in Weapons)
            {
                weapon.Update();
            }
            return;
        }

        public override void Show()
        {
            Prompt.SetText(
                EquipmentPickerController.ResultType == WeaponPickerResult.Enhancement ?
                "Enhanceable weapons..." :
                "Upgradeable equipment..."
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

            var weaponsOnly = EquipmentPickerController.ResultType == WeaponPickerResult.Enhancement;

            var playerEquipment = Globals.Me.Inventory
                .Select((invItem, i) => new KeyValuePair<int, Item>(i, invItem))
                .Where(item => item.Value?.Base?.ItemType == Enums.ItemTypes.Equipment && (!weaponsOnly || item.Value?.Base?.EquipmentSlot == Options.WeaponIndex))
                .Where(item =>
                {
                    if (item.Value?.Base == null)
                    {
                        return false;
                    }
                    var itemBase = item.Value.Base;
                    if (weaponsOnly)
                    {
                        return itemBase.EnhancementThreshold > 0;
                    }

                    return itemBase.WeaponUpgrades.Keys.Count > 0;
                })
                .ToArray();

            var idx = 0;
            foreach (var invItem in playerEquipment)
            {
                var equipmentItem = invItem.Value;
                var invSlot = invItem.Key;
                if (equipmentItem.Base.Id == Guid.Empty)
                {
                    continue;
                }

                var item = new WeaponPickerWeaponComponent(WeaponContainer,
                    $"EnhancementItem_{idx}",
                    "inventoryitem.png",
                    equipmentItem.Base.Icon,
                    Framework.File_Management.GameContentManager.TextureType.Item,
                    1,
                    4,
                    equipmentItem,
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
            WeaponContainer?.ClearCreatedChildren();
            Weapons.DisposeAll();
        }
    }
}
