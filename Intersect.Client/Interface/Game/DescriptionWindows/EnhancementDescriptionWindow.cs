using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.GameObjects;
using Intersect.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class EnhancementDescriptionWindow : DescriptionWindowBase
    {
        protected EnhancementDescriptor Enhancement { get; set; }

        protected SpellDescriptionWindow mSpellDescWindow;

        protected string Icon { get; set; }

        protected float StudyChance { get; set; }

        protected bool Learnable { get; set; }

        public EnhancementDescriptionWindow(Guid enhancementId, string icon, int x, int y, float studyChance = 0.0f, bool isLearnable = false, bool showSpellInfo = false) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            Enhancement = EnhancementDescriptor.Get(enhancementId);
            Icon = icon;
            StudyChance = studyChance;
            Learnable = isLearnable;

            GenerateComponents();
            SetupDescriptionWindow();

            if (Enhancement.SpellEnhancements.Count > 0 && showSpellInfo)
            {
                mSpellDescWindow = new SpellDescriptionWindow(Enhancement.SpellEnhancements[0].SpellId, x, y, abridged: true);
                x -= mSpellDescWindow.Container.Width + 4;
                mSpellDescWindow.Hide();
            }

            SetPosition(x, y);

            Hide();
        }

        public override void Show()
        {
            mSpellDescWindow?.Show();
            base.Show();
        }

        public override void Hide()
        {
            mSpellDescWindow?.Hide();
            base.Hide();
        }

        public override void SetPosition(int x, int y)
        {
            if (mSpellDescWindow != null)
            {
                mSpellDescWindow.SetPosition(x, y);
                x -= mSpellDescWindow.Container.Width + 4;
            }
            base.SetPosition(x, y);
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

            if (Enhancement.SpellEnhancements.Count > 0)
            {
                SetupProcSpellInfo(Enhancement.SpellEnhancements);
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

        private void SetupProcSpellInfo(List<SpellEnhancementDescriptor> enhancements)
        {
            if (enhancements.Count == 0)
            {
                return;
            }
            var procRow = AddRowContainer();

            if (enhancements.Count == 1)
            {
                procRow.AddKeyValueRow("   Spell on Hit", "", CustomColors.ItemDesc.Primary, Color.White);
            }
            else
            {
                procRow.AddKeyValueRow("   Spells on Hit", "", CustomColors.ItemDesc.Primary, Color.White);
            }

            foreach (var proc in enhancements)
            {
                var modColor = proc.MinValue > 0 ? CustomColors.ItemDesc.Better : CustomColors.ItemDesc.Worse;

                procRow.AddKeyValueRow("Spell:", SpellBase.GetName(proc.SpellId), CustomColors.ItemDesc.Primary, CustomColors.ItemDesc.Primary);
                procRow.AddKeyValueRow("Chance:", $"{proc.MinValue} - {proc.MaxValue}%", CustomColors.ItemDesc.Primary, modColor);

                procRow.SizeToChildren();
            }
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
            if (Learnable)
            {
                if (Enhancement.PrerequisitesMet(Globals.Me.KnownEnhancements))
                {
                    header.SetTitle($"LEARN: {EnhancementDescriptor.GetName(Enhancement.Id)}", CustomColors.ItemDesc.Notice);
                }
                else
                {
                    header.SetTitle($"LEARN: {EnhancementDescriptor.GetName(Enhancement.Id)}", CustomColors.ItemDesc.Worse);
                }
            }
            else
            {
                header.SetTitle($"{EnhancementDescriptor.GetName(Enhancement.Id)}", Color.White);
            }

            if (Learnable)
            {
                header.SetSubtitle("Unlearned Enhancement", Color.White);
            }
            else
            {
                header.SetSubtitle("Enhancement", Color.White);
            }

            if (StudyChance > 0f)
            {
                if (Enhancement.PrerequisitesMet(Globals.Me.KnownEnhancements))
                {
                    header.SetDescription($"Chance to learn: {StudyChance.ToString("N2")}%", CustomColors.ItemDesc.Notice);
                }
                else
                {
                    header.SetDescription($"Chance to learn: 0%", CustomColors.ItemDesc.Worse);
                }
            }

            if (Enhancement.PrerequisiteEnhancements.Count > 0 && !Enhancement.PrerequisitesMet(Globals.Me.KnownEnhancements))
            {
                AddDivider();
                var enhancementNames = Enhancement.PrerequisiteEnhancements
                    .Where(enId => !Globals.Me.KnownEnhancements.Contains(enId))
                    .Select(enId => EnhancementDescriptor.GetName(enId)).ToArray();

                var description = AddDescription();
                description.AddText($"The following enhancements must be learned before this enhancement can be studied: \n{string.Join(", ", enhancementNames)}\n", CustomColors.ItemDesc.Worse);
                AddDivider();
            }

            header.SizeToChildren(true, false);

            var rows = AddRowContainer();
            rows.AddKeyValueRow("Enhancement Points:", Enhancement.RequiredEnhancementPoints.ToString("N0"), CustomColors.ItemDesc.Muted, CustomColors.ItemDesc.Primary);

            rows.SizeToChildren(true, true);
        }

        public override void Dispose()
        {
            base.Dispose();
            mSpellDescWindow?.Dispose();
        }
    }
}
