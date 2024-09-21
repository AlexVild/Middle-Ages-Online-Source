using Intersect.Client.Core;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Maps;
using Intersect.Client.Utilities;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intersect.Client.Interface.Game.HUD
{
    public class PlayerHud
    {
        public bool IsVisible;

        public static readonly int Height = 72;

        private float Left => Graphics.CurrentView.Left;
        private float Width => Graphics.CurrentView.Width;
        private float Top => Graphics.CurrentView.Top;

        private const int SmallBarPadding = 8;
        private const int LargeBarPadding = 16;
        private int BarPadding => UseLargeBars ? LargeBarPadding : SmallBarPadding;

        private GameTexture BarBackground;
        private GameTexture BarBackgroundTransparent;
        private GameTexture BarBackgroundNoRegen;
        private GameTexture HpTexture;
        private GameTexture HpTextureNoRegen;
        private GameTexture ManaTexture;
        private GameTexture ManaTextureNoRegen;

        private GameTexture _weaponExpTexture;
        private GameTexture WeaponExpTexture
        {
            get
            {
                if (!Globals.CanEarnWeaponExp)
                {
                    return WeaponExpLockedTexture;
                }
                return FlashWeaponExp ? WeaponExpGainTexture : _weaponExpTexture;
            }
            set => _weaponExpTexture = value;
        }
        private GameTexture WeaponExpGainTexture;
        private GameTexture WeaponExpLockedTexture;
        private GameTexture _expTexture;
        private bool FlashExp = false;
        private bool FlashWeaponExp = false;
        private GameTexture ExpTexture
        {
            get => FlashExp ? ExpGainTexture : _expTexture;
            set => _expTexture = value;
        }
        private GameTexture ExpGainTexture;
        private GameTexture MapNameTexture;
        private GameTexture ShieldTexture;
        private GameTexture PvpTexture;
        private GameTexture ArenaTexture;

        private List<GameTexture> ScaleDownTextures = new List<GameTexture>();

        private bool CenterBarsBetweenElements => Width <= 1280;

        private bool UseLargeBars => Width >= 1600;

        private float Opacity = 255f;

        private float NameWidth;

        public Color TextureColor => new Color((int)GetOpacity(), 255, 255, 255);
        public Color Primary => new Color((int)GetOpacity(), 255, 255, 255);
        public Color Secondary => new Color((int)GetOpacity(), 188, 188, 188);
        public Color MapNameColor => new Color((int)GetOpacity(), 50, 19, 0);
        public Color BackgroundColor => new Color((int)Opacity, 0, 0, 0);
        public Color BackgroundBorderColor => new Color((int)Opacity, 50, 19, 0);

        private string MapName => MapInstance.Get(Globals.Me?.CurrentMap ?? default)?.Name ?? string.Empty;
        private MapZones CurrentMapZone => MapInstance.Get(Globals.Me?.CurrentMap ?? default)?.ZoneType ?? MapZones.Normal;

        // Used so shields know where to draw
        private float HpX;
        private float HpY;
        private int HpWidth;

        public float ExpX { get; set; }
        public float ExpY { get; set; }
        public float ExpWidth { get; set; }

        public long LastExpFlash { get; set; }

        private const long ExpFlashDuration = 250;

        private RegenType CurrentRegenType => Globals.Me?.MapInstance?.RegenType ?? RegenType.Normal;

        public PlayerHud()
        {
            IsVisible = true;
            FlashExp = false;
            FlashWeaponExp = false;
            LastExpFlash = Timing.Global.Milliseconds;
            InitTextures();
        }

        private void InitTextures() 
        {
            BarBackground = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar.png");
            BarBackgroundNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_noregen.png");
            BarBackgroundTransparent = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_transparent.png");
            HpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_health.png");
            HpTextureNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_health_noregen.png");
            ManaTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_magic.png");
            ManaTextureNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_magic_noregen.png");
            ExpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_exp.png");
            ExpGainTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_exp_gain.png");
            MapNameTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hud_map.png");
            ShieldTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_shield.png");
            PvpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hud_pvp.png");
            ArenaTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hud_arena.png");
            WeaponExpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp.png");
            WeaponExpGainTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_gain.png");
            WeaponExpLockedTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_disabled.png");

            ScaleDownTextures.Clear();
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t0.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t1.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t2.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t3.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t4.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t5.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t6.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t7.png"));
            ScaleDownTextures.Add(Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Misc, "ceil_t8.png"));
        }

        private void DetermineTextureScaling()
        {
            if (UseLargeBars && !BarBackground.Name.Contains("_lg"))
            {
                BarBackground = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_lg.png");
                BarBackgroundNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_lg_noregen.png");
                BarBackgroundTransparent = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_lg_transparent.png");
                HpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_health_lg.png");
                HpTextureNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_health_lg_noregen.png");
                ManaTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_magic_lg.png");
                ManaTextureNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_magic_lg_noregen.png");
                ExpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_exp_lg.png");
                ExpGainTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_exp_gain_lg.png");
                ShieldTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_shield_lg.png");
                WeaponExpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_lg.png");
                WeaponExpGainTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_gain_lg.png");
                WeaponExpLockedTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_lg_disabled.png");
            }
            else if (BarBackground.Name.Contains("_lg") && !UseLargeBars)
            {
                BarBackground = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar.png");
                BarBackgroundNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_noregen.png");
                BarBackgroundTransparent = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_transparent.png");
                HpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_health.png");
                HpTextureNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_health_noregen.png");
                ManaTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_magic.png");
                ManaTextureNoRegen = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_magic_noregen.png");
                ExpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_exp.png");
                ExpGainTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_exp_gain.png");
                ShieldTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_shield.png");
                WeaponExpTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp.png");
                WeaponExpGainTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_gain.png");
                WeaponExpLockedTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "hudbar_weapon_exp_disabled.png");
            }
        }

        public void Draw()
        {
            if (Globals.Me == null)
            {
                return;
            }
            if (Globals.Me.InCutscene())
            {
                return;
            }

            Opacity = Globals.Me.GetCurrentTileRectangle().Bottom < Top + Height + Options.TileHeight ? 75 : 255;

            DetermineTextureScaling();

            DrawBackground(Height);

            DrawPlayerNameAndLevel(Left, Top + 12);
            // On small screens, center the bars between the hotbar and the name
            if (CenterBarsBetweenElements)
            {
                var nameEnd = Left + NameWidth;
                var hotBarStart = (float)Interface.GameUi.Hotbar.HotbarWindow.X;
                hotBarStart += Left;

                var width = hotBarStart - nameEnd;
                var centerPoint = nameEnd + width / 2;

                DrawBarContainer(centerPoint, Top + 4);
            }
            // Otherwise, center on the middle of the bar container
            else
            {
                DrawBarContainer(Left + Width / 2, Top + 4);
            }

            DrawScaleDownIndicator((Left + (Width / 2)) + (MapNameTexture.Width / 2) + 4, Top + Height - 3);
            DrawMapLabel(Left + Width / 2, Top + Height - 3, MapName);
        }

        public float GetOpacity()
        {
            return Opacity;
        }

        private void DrawBackground(int height)
        {
            var borderDimensions = new FloatRect(Left, Top, Graphics.Renderer.GetScreenWidth(), height);
            var backgroundDimensions = new FloatRect(Left, Top - 4, Graphics.Renderer.GetScreenWidth(), height);
            Graphics.DrawGameTexture(Graphics.Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), borderDimensions, BackgroundBorderColor);
            Graphics.DrawGameTexture(Graphics.Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), backgroundDimensions, BackgroundColor);
        }

        private void DrawPlayerNameAndLevel(float x, float y)
        {
            var font = Graphics.HUDFont;

            var playerName = Globals.Me.Name.ToUpper();
            var level = Globals.Me.Level.ToString();

            var levelString = $"LVL {level}".ToUpper();

            var playerNameWidth = Graphics.Renderer.MeasureText(playerName, font, 1).X;
            var playerNameHeight = Graphics.Renderer.MeasureText(playerName, font, 1).Y;
            var levelWidth = Graphics.Renderer.MeasureText(levelString, font, 1).X;
            var levelYPadding = 4;

            var xPadding = 8;
            var maxWidth = Math.Max(levelWidth, playerNameWidth);
            x += (maxWidth / 2) + xPadding;

            NameWidth = maxWidth + xPadding;

            // Name
            Graphics.Renderer.DrawString(
                playerName, font, (int)x - playerNameWidth / 2f, (int)y, 1, Primary
            );
            // Level & Class
            Graphics.Renderer.DrawString(
                levelString, font, (int)x - levelWidth / 2f, (int)y + playerNameHeight + levelYPadding, 1, Secondary
            );
        }

        private void DrawBarContainer(float x, float y)
        {
            var numberOfBars = 3;

            // because 8 is our padding between bars and 3 is t
            var width = (BarBackground.GetWidth() + BarPadding) * numberOfBars;
            var height = BarBackground.GetHeight() * 2; // leaves space for labels
            x -= width / 2; // Center container

            var currentHp = Globals.Me.Vital[(int)Vitals.Health];
            var maxHp = Globals.Me.MaxVital[(int)Vitals.Health];
            var currentShield = Globals.Me.GetShieldSize();
            if (currentHp + currentShield > maxHp)
            {
                maxHp = currentHp + currentShield;
            }

            var hpTexture = HpTexture;
            var hpBackground = BarBackground;
            if (CurrentRegenType == RegenType.NoHP || CurrentRegenType == RegenType.NoRegen)
            {
                hpTexture = HpTextureNoRegen;
                hpBackground = BarBackgroundNoRegen;
            }
            DrawBar(hpBackground, hpTexture, x ,y, width, height, "HP", $"{currentHp + currentShield} / {maxHp}", currentHp, maxHp, true);
            DrawShield(ShieldTexture, currentHp, maxHp, Globals.Me.GetShieldSize());

            var currentMana = Globals.Me.Vital[(int)Vitals.Mana];
            var maxMana = Globals.Me.MaxVital[(int)Vitals.Mana];
            x += BarBackground.GetWidth() + BarPadding;

            var manaTexture = ManaTexture;
            var manaBackground = BarBackground;
            if (CurrentRegenType == RegenType.NoMana || CurrentRegenType == RegenType.NoRegen || (Globals.Me != null && Globals.Me.StatusActive(StatusTypes.Enfeebled)))
            {
                manaTexture = ManaTextureNoRegen;
                manaBackground = BarBackgroundNoRegen;
            }
            DrawBar(manaBackground, manaTexture, x, y, width, height, "MP", $"{currentMana} / {maxMana}", currentMana, maxMana);

            var currentExp = Globals.Me.Experience;
            var tnlExp = Globals.Me.ExperienceToNextLevel;
            x += BarBackground.GetWidth() + BarPadding;

            ExpX = x - Left;
            ExpY = y - Top;
            ExpWidth = BarBackground.GetWidth();

            if (ExpToastService.Toasts.Count == 0)
            {
                LastExpFlash = Timing.Global.Milliseconds;
                FlashExp = false;
                FlashWeaponExp = false;
            }
            else
            {
                if (Timing.Global.Milliseconds > LastExpFlash)
                {
                    if (ExpToastService.WeaponExpLastFlash)
                    {
                        FlashExp = false;
                    }
                    else
                    {
                        FlashExp = !FlashExp;
                    }
                    FlashWeaponExp = !FlashWeaponExp;
                    LastExpFlash = Timing.Global.Milliseconds + ExpFlashDuration;
                }
            }

            if (Globals.Me.Level == Options.MaxLevel)
            {
                DrawBar(BarBackground, ExpTexture, x, y, width, height, "EXP", $"MAX LEVEL", 1, 1);
            }
            else
            {
                DrawBar(BarBackground, ExpTexture, x, y, width, height, "EXP", $"{currentExp} / {tnlExp}", currentExp, tnlExp);
            }

            if (Globals.Me.TrackedWeaponTypeId != Guid.Empty && Globals.Me.WeaponExpTnl > 0)
            {
                DrawBar(BarBackgroundTransparent, WeaponExpTexture, x, y + 45, width, height, string.Empty, string.Empty, Globals.Me.WeaponExp, Globals.Me.WeaponExpTnl);
            }
        }

        private void DrawBar(GameTexture bgTexture, GameTexture innerTexture, float x, float y, float width, float height, string label1, string label2, long currAmt, long maxAmt, bool isHp = false)
        {
            if (bgTexture == null || innerTexture == null)
            {
                return;
            }

            var font = Graphics.HUDFont;
            var font2 = Graphics.HUDFontSmall;
            var label1Clr = Color.White;
            label1Clr.A = (byte)Opacity;

            var lbl2Clr = new Color((int)Opacity, 188, 188, 188);

            var centerX = (bgTexture.GetWidth() / 2) + x;

            var lbl1width = Graphics.Renderer.MeasureText(label1, font, 1).X;
            var lbl1height = Graphics.Renderer.MeasureText(label1, font, 1).Y;
            var lbl2width = Graphics.Renderer.MeasureText(label2, font2, 1).X;
            var lbl2height = Graphics.Renderer.MeasureText(label2, font2, 1).Y;

            var lbl1X = centerX - lbl1width / 2;
            var lbl2X = centerX - lbl2width / 2;
            var lbl2y = y + lbl1height;

            // Name
            Graphics.Renderer.DrawString(
                label1, font, (int)lbl1X, (int)y, 1, label1Clr
            );
            // Level & Class
            Graphics.Renderer.DrawString(
                label2, font2, (int)lbl2X, (int)lbl2y, 1, lbl2Clr
            );

            var barY = lbl2y + lbl2height;
            var barX = centerX - bgTexture.GetWidth() / 2;

            Graphics.DrawGameTexture(bgTexture,
                new FloatRect(0, 0, bgTexture.GetWidth(), bgTexture.GetHeight()),
                new FloatRect(barX, barY, bgTexture.GetWidth(), bgTexture.GetHeight()),
                TextureColor);

            int barWidth = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)(currAmt / (float)maxAmt * innerTexture.GetWidth()), 4);
            barWidth = Intersect.Utilities.MathHelper.Clamp(barWidth, 0, innerTexture.GetWidth());

            // used for shield positioning
            if (isHp)
            {
                HpX = barX + 4;
                HpY = barY + 4;
                HpWidth = barWidth;
            }

            Graphics.DrawGameTexture(innerTexture,
                new FloatRect(0, 0, barWidth, innerTexture.GetHeight()),
                new FloatRect(barX + 4, barY + 4, barWidth, innerTexture.GetHeight()),
                TextureColor);
        }

        private void DrawShield(GameTexture shieldTexture, long currAmt, long maxAmt, int totalShield)
        {
            var shieldfillRatio = (float)totalShield / maxAmt;
            shieldfillRatio = (float)Intersect.Utilities.MathHelper.Clamp(shieldfillRatio, 0f, 1f);
            var shieldfillWidth = Intersect.Utilities.MathHelper.RoundNearestMultiple((int)(shieldfillRatio * shieldTexture.GetWidth()), 4);

            Graphics.DrawGameTexture(shieldTexture,
               new FloatRect(0, 0, shieldTexture.GetWidth(), shieldTexture.GetHeight()),
               new FloatRect(HpX + HpWidth, HpY, shieldfillWidth, shieldTexture.GetHeight()),
               TextureColor);
        }

        private void DrawMapLabel(float x, float y, string mapName)
        {
            // Draw background
            Graphics.DrawGameTexture(MapNameTexture,
                new FloatRect(0, 0, MapNameTexture.GetWidth(), MapNameTexture.GetHeight()),
                new FloatRect(x - MapNameTexture.GetWidth() / 2, y, MapNameTexture.GetWidth(), MapNameTexture.GetHeight()),
                TextureColor);

            // 56 because that is the distance on either side of the label texture's "bolts"
            var font = Graphics.HUDFont;
            var zoneType = CurrentMapZone;
            var maxNameWidth = zoneType == MapZones.Safe ? 56 : 100;

            mapName = UiHelper.TruncateString(mapName, Graphics.HUDFont, MapNameTexture.GetWidth() - maxNameWidth).ToUpper();
            
            var nameWidth = Graphics.Renderer.MeasureText(mapName, font, 1).X;
            var nameX = x - nameWidth / 2;
            var nameY = y + 4;

            Graphics.Renderer.DrawString(
                mapName, font, (int)nameX, (int)nameY, 1, MapNameColor
            );

            if (zoneType != MapZones.Safe)
            {
                var zoneTexture = zoneType == MapZones.Normal ? PvpTexture : ArenaTexture;
                var zoneTextCenterX = zoneTexture.GetWidth() / 2;
                var zoneTextCenterY = zoneTexture.GetHeight() / 2;
                Graphics.DrawGameTexture(zoneTexture,
                   new FloatRect(0, 0, zoneTexture.GetWidth(), zoneTexture.GetHeight()),
                   new FloatRect(x + (MapNameTexture.GetWidth() / 2) - zoneTextCenterX - 52, y + zoneTextCenterY - 3, zoneTexture.GetWidth() * 4, zoneTexture.GetHeight() * 4),
                   TextureColor);
            }
        }

        private void DrawScaleDownIndicator(float x, float y)
        {
            if (!Globals.Me.IsScaledDown)
            {
                return;
            }

            try
            {
                var texture = ScaleDownTextures.ElementAtOrDefault(Globals.Me.ScaledTo);
                Graphics.DrawGameTexture(texture,
                   new FloatRect(0, 0, texture.GetWidth(), texture.GetHeight()),
                   new FloatRect(x, y, texture.GetWidth() * 2, texture.GetHeight() * 2),
                   TextureColor);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return;
            }
        }
    }
}
