using Intersect.GameObjects;
using Intersect.Client.General;
using System;

namespace Intersect.Client.Interface.Game.DescriptionWindows
{
    public class ChallengeDescriptionWindow : DescriptionWindowBase
    {
        protected ChallengeDescriptor Challenge;

        protected SpellDescriptionWindow mSpellDescWindow;

        public ChallengeDescriptionWindow(Guid challengeId, int x, int y) : base(Interface.GameUi.GameCanvas, "DescriptionWindow")
        {
            Challenge = ChallengeDescriptor.Get(challengeId);

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

            // Set up bind info, if applicable.
            SetupExtraInfo();

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
            var evt = Challenge.CompletionEvent;
            var spell = Challenge.SpellUnlock;
            
            // no unlocks
            if (spell == default && evt == default)
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

            rows.SizeToChildren(true, true);

            if (evt != default && !string.IsNullOrWhiteSpace(Challenge.EventDescription))
            {
                // Add a divider.
                AddDivider();

                // Add the actual description.
                var description = AddDescription();
                description.AddText(Challenge.EventDescription, CustomColors.ItemDesc.Muted);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            mSpellDescWindow?.Dispose();
        }
    }
}
