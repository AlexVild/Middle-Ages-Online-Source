using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Interface.Game.Components;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Network.Packets.Server;

namespace Intersect.Client.Interface.Game.Enhancement
{
    sealed class EnhancementCompleteWindow
    {
        public EnhancementWindow Parent { get; set; }

        public ImagePanel Background { get; set; }

        public ScrollControl EnhancementsContainer { get; set; }

        public Button OkayButton { get; set; }

        public ItemProperties NewProperties { get; set; }

        private const int YPadding = 32;

        public EnhancementCompleteWindow(EnhancementWindow parent, Base gameCanvas)
        {
            Parent = parent;

            Background = new ImagePanel(gameCanvas, "EnhancementCompleteWindow");

            EnhancementsContainer = new ScrollControl(Background, "EnhancementsContainer");

            OkayButton = new Button(Background, "OkayButton") { Text = "Okay" };
            OkayButton.Clicked += OkayButton_Clicked;

            Background.LoadJsonUi(Framework.File_Management.GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void OkayButton_Clicked(Base sender, Framework.Gwen.Control.EventArguments.ClickedEventArgs arguments)
        {
            Parent.ForceClose(); // hides this
        }

        public void Hide()
        {
            EnhancementsContainer.ClearCreatedChildren();
            Background.Hide();
        }

        public void Show(ItemProperties newProperties)
        {
            NewProperties = newProperties;

            EnhancementsContainer.ClearCreatedChildren();

            var idx = 0;

            var statIdx = 0;
            foreach (var statAmt in newProperties.StatEnhancements)
            {
                if (statAmt == 0)
                {
                    statIdx++;
                    continue;
                }

                Strings.ItemDescription.Stats.TryGetValue(statIdx, out var effectName);

                var statStr = $"+{statAmt}";
                if (statAmt < 0)
                {
                    statStr = $"{statAmt}";
                }

                var row = new EnhancementRowComponent(EnhancementsContainer, "EnhancementRow", effectName, statStr, string.Empty);

                row.SetPosition(row.X, row.Y + (YPadding * idx));
                row.Initialize();

                idx++;
                statIdx++;
            }

            var vitalIdx = 0;
            foreach (var vitalAmt in newProperties.VitalEnhancements)
            {
                if (vitalAmt == 0)
                {
                    vitalIdx++;
                    continue;
                }

                Strings.ItemDescription.Vitals.TryGetValue(vitalIdx, out var effectName);

                var statStr = $"+{vitalAmt}";
                if (vitalAmt < 0)
                {
                    statStr = $"{vitalAmt}";
                }

                var row = new EnhancementRowComponent(EnhancementsContainer, "EnhancementRow", effectName, statStr, string.Empty);

                row.SetPosition(row.X, row.Y + (YPadding * idx));
                row.Initialize();

                idx++;
                statIdx++;
            }

            var effectIdx = 0;
            foreach (var effectAmt in newProperties.EffectEnhancements)
            {
                if (effectAmt == 0)
                {
                    effectIdx++;
                    continue;
                }

                Strings.ItemDescription.BonusEffects.TryGetValue(effectIdx, out var effectName);

                BonusEffectHelper.BonusEffectDescriptions.TryGetValue((EffectType)effectIdx, out var tooltip);

                var statStr = $"+{effectAmt}%";
                if (effectAmt < 0)
                {
                    statStr = $"{effectAmt}%";
                }

                var row = new EnhancementRowComponent(EnhancementsContainer, "EnhancementRow", effectName, statStr, tooltip.Description ?? string.Empty);

                row.SetPosition(row.X, row.Y + (YPadding * idx));
                row.Initialize();

                idx++;
                effectIdx++;
            }

            foreach (var spellEnhancement in newProperties.SpellEnhancements)
            {
                var spell = SpellBase.Get(spellEnhancement.SpellId);
                if (spell == default)
                {
                    effectIdx++;
                    continue;
                }

                var spellName = spell.Name;

                var statStr = $"+{spellEnhancement.Value}%";
                if (spellEnhancement.Value < 0)
                {
                    statStr = $"{spellEnhancement.Value}%";
                }

                var row = new EnhancementRowComponent(EnhancementsContainer, "EnhancementRow", spellName, statStr, "Chance to cast spell on hit");

                row.SetPosition(row.X, row.Y + (YPadding * idx));
                row.Initialize();

                idx++;
                effectIdx++;
            }

            Background.Show();
        }
    }
}
