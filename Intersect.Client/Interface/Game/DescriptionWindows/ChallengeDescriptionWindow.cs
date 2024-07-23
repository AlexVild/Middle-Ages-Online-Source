using Intersect.GameObjects;
using Intersect.Client.General;
using System;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class ChallengeDescriptionWindow : DescriptionWindowBase
    {
        protected ChallengeDescriptor Challenge;

        protected SpellDescriptionWindow mSpellDescWindow;

        public ChallengeDescriptionWindow(Guid challengeId, int x, int y) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            Challenge = ChallengeDescriptor.Get(challengeId);
            if (Challenge == null)
            {
                return;
            }

            GenerateComponents();
            SetupDescriptionWindow();

            if (Challenge.SpellUnlock != default)
            {
                mSpellDescWindow = new SpellDescriptionWindow(Challenge.SpellUnlockId, x, y);
            }

            if (mSpellDescWindow != default)
            {
                x -= mSpellDescWindow.Container.Width + 4;
            }
            
            SetPosition(x, y);
        }

        protected void SetupDescriptionWindow()
        {
            if (Challenge == default)
            {
                return;
            }

            // Set up our header information.
            SetupHeader();

            // Add the actual description.
            var description = AddDescription();
            description.AddText(Challenge.GetDescription(), Color.White);

            SetupExtraInfo();

            if (Challenge.HasStatBoosts())
            {
                SetupBoosts();
            }

            // Resize the container, correct the display and position our window.
            FinalizeWindow();
        }

        protected void SetupHeader()
        {
            // Create our header, but do not load our layout yet since we're adding components manually.
            var header = AddHeader();

            // Set up the icon, if we can load it.
            var tex = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Challenge, Challenge.Icon);
            if (tex != null)
            {
                header.SetIcon(tex, Color.White);
            }

            // Set up the header as the item name.
            header.SetTitle(Challenge.Name, Color.White);

            header.SizeToChildren(true, false);
        }

        protected void SetupExtraInfo()
        {
            // Display only if this spell is bound.
            var evt = Challenge.CompletionEventId;
            var spell = Challenge.SpellUnlock;
            var enhancement = Challenge.UnlockedEnhancement;
            
            // no unlocks
            if (spell == default && evt == Guid.Empty)
            {
                return;
            }

            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            rows.AddKeyValueRow("Unlocks:", string.Empty);

            if (spell != default)
            {
                // Add a divider.
                AddDivider();

                // Display shop value.
                rows.AddKeyValueRow("Skill:", spell.Name);
            }

            if (enhancement != default)
            {
                // Add a divider.
                AddDivider();

                // Display shop value.
                rows.AddKeyValueRow("Wep. Enhancement:", enhancement.Name);
            }

            rows.SizeToChildren(true, true);

            if (evt != Guid.Empty && !string.IsNullOrWhiteSpace(Challenge.EventDescription))
            {
                // Add a divider.
                AddDivider();

                // Add the actual description.
                var description = AddDescription();
                description.AddText(Challenge.EventDescription, CustomColors.ItemDesc.Muted);
            }
        }

        protected void SetupBoosts()
        {
            // Add a divider.
            AddDivider();

            // Add a row component.
            var rows = AddRowContainer();

            rows.AddKeyValueRow("Permanent Boosts:", string.Empty, CustomColors.ItemDesc.Special, null);
            
            if (Challenge.StatBoosts != null)
            {
                foreach (var statBoost in Challenge.StatBoosts)
                {
                    if (statBoost.Value == 0)
                    {
                        continue;
                    }

                    var symbol = Math.Sign(statBoost.Value) == 1 ? "+" : "-";
                    rows.AddKeyValueRow(statBoost.Key.GetDescription(), $"{symbol}{statBoost.Value.ToString("N0")}");
                }
            }

            if (Challenge.VitalBoosts != null)
            {
                foreach (var vitalBoost in Challenge.VitalBoosts)
                {
                    if (vitalBoost.Value == 0)
                    {
                        continue;
                    }

                    var symbol = Math.Sign(vitalBoost.Value) == 1 ? "+" : "-";
                    rows.AddKeyValueRow(vitalBoost.Key.GetDescription(), $"{symbol}{vitalBoost.Value.ToString("N0")}");
                }
            }

            if (Challenge.BonusEffects != null)
            {
                foreach (var bonus in Challenge.BonusEffects)
                {
                    if (bonus.Percentage == 0)
                    {
                        continue;
                    }

                    var symbol = Math.Sign(bonus.Percentage) == 1 ? "+" : "-";
                    rows.AddKeyValueRow(bonus.Type.GetDescription(), $"{symbol}{bonus.Percentage.ToString("N0")}%");
                }
            }

            rows.SizeToChildren(true, true);
        }

        public override void Dispose()
        {
            base.Dispose();
            mSpellDescWindow?.Dispose();
        }
    }
}
