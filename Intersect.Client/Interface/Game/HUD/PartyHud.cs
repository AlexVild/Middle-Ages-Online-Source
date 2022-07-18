using Intersect.Client.Core;
using Intersect.Client.Entities;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using static Intersect.Client.Framework.File_Management.GameContentManager;

namespace Intersect.Client.Interface.Game.HUD
{
    public class PartyHud
    {
        private float Left => Graphics.CurrentView.Left;
        private float Width => Graphics.CurrentView.Width;
        private float Top => Graphics.CurrentView.Top;
        private float Bottom => Graphics.CurrentView.Bottom;

        private GameTexture BarBgTexture;
        private GameTexture HealthTexture;
        private GameTexture ManaTexture;
        private GameTexture ShieldTexture;
        private GameFont MemberFont;
        private Color MemberColor => new Color(Opacity, 188, 188, 188);
        private Color MemberHighlightColor => new Color(Opacity, 255, 255, 255);
        private int Opacity = 255;
        private Color RenderColor => new Color(Opacity, 255, 255, 255);
        private readonly Pointf Dimensions = new Pointf(168, 60);
        private PartyMember HoveredMember;
        private Pointf MousePos => UiHelper.GetViewMousePos();
        private const int BottomPadding = 16;
        private const int BarXPadding = 8;
        private const int NameXPadding = 4;
        private const int VitalBgBorderWidth = 4;
        private const int StatusPadding = 4;
        private bool IsClicking;

        public PartyHud()
        {
            BarBgTexture = Globals.ContentManager.GetTexture(TextureType.Gui, "party_bar.png");
            HealthTexture = Globals.ContentManager.GetTexture(TextureType.Gui, "party_bar_health.png");
            ManaTexture = Globals.ContentManager.GetTexture(TextureType.Gui, "party_bar_magic.png");
            ShieldTexture = Globals.ContentManager.GetTexture(TextureType.Gui, "party_bar_shield.png");
            MemberFont = Graphics.HUDFontSmall;
        }

        public void Draw()
        {
            if (!Globals.Database.DisplayPartyInfo || Globals.Me == null || !Globals.Me.IsInParty())
            {
                return;
            }

            var x = Left + 28;
            var y = Bottom - (Interface.GameUi.GetChatboxHeight() + BottomPadding + Dimensions.Y);

            var totalHeight = Dimensions.Y * (Globals.Me.Party.Count - 1);
            var currBounds = new FloatRect(Left + x, y + Dimensions.Y - totalHeight, Dimensions.X, totalHeight);
            Opacity = Globals.Me.GetCurrentTileRectangle().IntersectsWith(currBounds) ? 100 : 255;

            HoveredMember = null;
            foreach(var member in Globals.Me.Party)
            {
                if (member.Id == Globals.Me.Id)
                {
                    continue;
                }

                DrawMemberDetails(member, x, y);
                y -= Dimensions.Y;
            }

            HandleMouseInput();
        }

        private void HandleMouseInput()
        {
            if (HoveredMember == null || Globals.Me == null)
            {
                return;
            }

            if (!(Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Left) && Globals.Database.LeftClickTarget) && !Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Right))
            {
                IsClicking = false;
                return;
            }

            if (!Globals.Entities.TryGetValue(HoveredMember?.Id ?? default, out var member) || member == null)
            {
                return;
            }

            if (IsClicking)
            {
                return;
            }

            Globals.Me.TryTarget(member);
            IsClicking = true;
        }

        private void DrawMemberDetails(PartyMember member, float x, float y)
        {
            // check to see if we're hovering over this member
            FloatRect bounds = new FloatRect(x, y, Dimensions.X, Dimensions.Y);
            if (MousePos.X >= bounds.Left && MousePos.X <= bounds.Right && MousePos.Y >= bounds.Top && MousePos.Y <= bounds.Bottom)
            {
                HoveredMember = member;
            }

            var memberName = UiHelper.TruncateString(member.Name, MemberFont, 76);
            Graphics.Renderer.DrawString(memberName, MemberFont, x + NameXPadding, y, 1, GetMemberColor(member));
            var textHeight = Graphics.Renderer.MeasureText(memberName, MemberFont, 1).Y;

            // Health/Shield bar
            y += textHeight + 4;
            Graphics.DrawGameTexture(BarBgTexture,
                new FloatRect(0, 0, BarBgTexture.GetWidth(), BarBgTexture.GetHeight()),
                new FloatRect(x, y, BarBgTexture.GetWidth(), BarBgTexture.GetHeight()),
                RenderColor);
            var maxHealth = member.MaxVital[(int)Vitals.Health];
            var shield = member.Shield;

            if (maxHealth + shield > maxHealth)
            {
                maxHealth += shield;
            }
            
            var hpRatio = (float)member.Vital[(int)Vitals.Health] / maxHealth;
            var shieldRatio = (float)shield / maxHealth;
            hpRatio = (float)Intersect.Utilities.MathHelper.Clamp(hpRatio, 0f, 1f);
            shieldRatio = (float)Intersect.Utilities.MathHelper.Clamp(shieldRatio, 0f, 1f);

            var hpWidth = hpRatio * HealthTexture.GetWidth();
            var shieldWidth = shieldRatio * ShieldTexture.GetWidth();

            hpWidth = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)hpWidth, 4);
            hpWidth = Intersect.Utilities.MathHelper.Clamp((int)hpWidth, 0, HealthTexture.GetWidth());
            shieldWidth = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)shieldWidth, 4);
            shieldWidth = Intersect.Utilities.MathHelper.Clamp((int)shieldWidth, 0, ShieldTexture.GetWidth());

            Graphics.DrawGameTexture(HealthTexture,
                new FloatRect(0, 0, hpWidth, HealthTexture.GetHeight()),
                new FloatRect(x + VitalBgBorderWidth, y + VitalBgBorderWidth, hpWidth, HealthTexture.GetHeight()),
                RenderColor);

            if (shield > 0)
            {
                Graphics.DrawGameTexture(ShieldTexture,
                    new FloatRect(hpWidth, 0, shieldWidth, ShieldTexture.GetHeight()),
                    new FloatRect(x + VitalBgBorderWidth + hpWidth, y + VitalBgBorderWidth, shieldWidth, ShieldTexture.GetHeight()),
                    RenderColor);
            }

            // Mana bar
            x += BarBgTexture.GetWidth() + BarXPadding;
            Graphics.DrawGameTexture(BarBgTexture,
                new FloatRect(0, 0, BarBgTexture.GetWidth(), BarBgTexture.GetHeight()),
                new FloatRect(x, y, BarBgTexture.GetWidth(), BarBgTexture.GetHeight()),
                RenderColor);
            var mpRatio = Intersect.Utilities.MathHelper.Clamp((float)member.Vital[(int)Vitals.Mana] / member.MaxVital[(int)Vitals.Mana], 0f, 1f);
            var mpWidth = Intersect.Utilities.MathHelper.RoundNearestMultiple((int) (mpRatio * ManaTexture.GetWidth()), 4);
            mpWidth = Intersect.Utilities.MathHelper.Clamp(mpWidth, 0, ManaTexture.GetWidth());
            Graphics.DrawGameTexture(ManaTexture,
                new FloatRect(0, 0, mpWidth, ManaTexture.GetHeight()),
                new FloatRect(x + VitalBgBorderWidth, y + VitalBgBorderWidth, mpWidth, ManaTexture.GetHeight()),
                RenderColor);

            DrawStatuses(member, x + VitalBgBorderWidth, y - StatusPadding - 32);
        }

        private void DrawStatuses(PartyMember member, float x, float y)
        {
            if (!Globals.Entities.TryGetValue(member.Id, out var en))
            {
                return;
            }

            if (en.Status.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < en.Status.Count; i++)
            {
                var status = en.Status[i];
                if (status.SpellId == default)
                {
                    continue;
                }
                var spell = SpellBase.Get(status.SpellId);
                var texture = Globals.ContentManager.GetTexture(TextureType.Spell, spell.Icon);
                if (texture == null)
                {
                    continue;
                }

                var newX = x;
                if (i % 3 != 0)
                {
                    newX += texture.GetWidth() / 2 + (StatusPadding * 2);
                }

                Graphics.DrawGameTexture(texture, 
                    new FloatRect(0, 0, texture.GetWidth(), texture.GetHeight()),
                    new FloatRect(newX, y, texture.GetWidth() / 2, texture.GetHeight() / 2),
                    RenderColor);

                var remainingStr = Strings.EntityBox.cooldown.ToString((status.RemainingMs / 1000f).ToString("N0"));
                Graphics.Renderer.DrawString($"{remainingStr}", MemberFont, newX, y + texture.GetHeight() / 2 + 2, 0.5f, MemberColor);
            }
        }

        private Color GetMemberColor(PartyMember member)
        {
            return HoveredMember == member ?
                MemberHighlightColor :
                MemberColor;
        }
    }
}
