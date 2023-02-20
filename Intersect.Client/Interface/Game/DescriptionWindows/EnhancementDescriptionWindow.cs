using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects;
using Intersect.Localization;
using System;
using System.Collections.Generic;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class EnhancementDescriptionWindow : DescriptionWindowBase
    {
        protected EnhancementDescriptor Enhancement { get; set; }

        protected string Icon { get; set; }

        public EnhancementDescriptionWindow(Guid enhancementId, string icon, int x, int y) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            Enhancement = EnhancementDescriptor.Get(enhancementId);
            Icon = icon;

            GenerateComponents();
            SetupDescriptionWindow();

            SetPosition(x, y);

            Hide();
        }

        protected void SetupDescriptionWindow()
        {
            if (Enhancement == default)
            {
                return;
            }

            SetupHeader();

            AddDivider();

            SetupRequirements();

            AddDivider();

            if (Enhancement.StatMods.Count > 0)
            {
                SetupMods(Enhancement.StatMods, "Stats:", Strings.ItemDescription.Stats, false);
            }

            if (Enhancement.VitalMods.Count > 0)
            {
                SetupMods(Enhancement.VitalMods, "Vitals:", Strings.ItemDescription.Vitals, false);
            }

            if (Enhancement.EffectMods.Count > 0)
            {
                SetupMods(Enhancement.EffectMods, "Effects:", Strings.ItemDescription.BonusEffects, true);
            }

            FinalizeWindow();
        }

        protected void SetupRequirements()
        {
            var rows = AddRowContainer();
            rows.AddKeyValueRow("Can be applied to...", string.Empty, CustomColors.ItemDesc.Notice, Color.White);
            foreach (var kv in Enhancement.ValidWeaponTypes)
            {
                var weaponTypeId = kv.Key;
                var minLevel = kv.Value;
                var name = WeaponTypeDescriptor.Get(weaponTypeId)?.VisibleName ?? "NOT FOUND";

                rows.AddKeyValueRow($"{name}:", $"Lvl. {minLevel}+", CustomColors.ItemDesc.Notice, CustomColors.ItemDesc.Notice);
            }

            rows.SizeToChildren(true, true);
        }

        protected void SetupMods<T>(List<Enhancement<T>> enhancements, string title, Dictionary<int, LocalizedString> valueLookup, bool percentView) where T : Enum
        {
            var mods = enhancements.ToArray();
            var rows = AddRowContainer();
            foreach (var mod in mods)
            {
                valueLookup.TryGetValue(Convert.ToInt32(mod.EnhancementType), out LocalizedString modName);

                var modNameStr = modName.ToString();
                modNameStr = modNameStr.Replace(":", string.Empty);
                var modString = mod.GetRangeDisplay(percentView, true);

                var modColor = mod.MinValue > 0 ? CustomColors.ItemDesc.Better : CustomColors.ItemDesc.Worse;

                rows.AddKeyValueRow($"{modNameStr}:", modString, CustomColors.ItemDesc.Primary, modColor);
            }

            rows.SizeToChildren(true, true);
        }

        protected void SetupHeader()
        {
            // Create our header, but do not load our layout yet since we're adding components manually.
            var header = AddHeader();

            // Set up the icon, if we can load it.
            var tex = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Item, Icon);
            if (tex != null)
            {
                header.SetIcon(tex, Color.White);
            }

            // Set up the header as the item name.
            header.SetTitle(EnhancementDescriptor.GetName(Enhancement.Id), Color.White);
            
            header.SizeToChildren(true, false);
            
            var rows = AddRowContainer();
            rows.AddKeyValueRow("Enhancement Points:", Enhancement.RequiredEnhancementPoints.ToString("N0"), CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Primary);

            rows.SizeToChildren(true, true);
        }
    }
}
