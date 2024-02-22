using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Entities;
using Intersect.Client.Entities.CombatNumbers;
using Intersect.Client.Entities.Events;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.HUD;
using Intersect.Client.Maps;
using Intersect.Configuration;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Core
{

    public static partial class Graphics
    {

        public static GameFont ActionMsgFont;

        public static object AnimationLock = new object();

        //Darkness Stuff
        public static float BrightnessLevel;

        public static GameFont ChatBubbleFont;

        private static FloatRect _currentView;

        public static FloatRect CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                Renderer.SetView(_currentView);
            }
        }

        public static FloatRect Viewport => new FloatRect(CurrentView.Position, CurrentView.Size / (Globals.Database?.WorldZoom ?? 1));

        public static GameShader DefaultShader;

        //Rendering Variables
        public static int DrawCalls;

        public static int EntitiesDrawn;

        public static GameFont EntityNameFont;

        //Screen Values
        public static GameFont GameFont;

        public static object GfxLock = new object();

        //Grid Switched
        public static bool GridSwitched;

        public static int LightsDrawn;

        //Animations
        public static List<Animation> LiveAnimations = new List<Animation>();

        public static int MapsDrawn;

        //Overlay Stuff
        public static Color OverlayColor = Color.Transparent;

        public static ColorF PlayerLightColor = ColorF.White;

        //Game Renderer
        public static GameRenderer Renderer;

        //Cache the Y based rendering
        public static HashSet<Entity>[,] RenderingEntities;

        private static GameContentManager sContentManager;

        private static GameRenderTexture sDarknessTexture;

        private static List<LightBase> sLightQueue = new List<LightBase>();

        //Player Spotlight Values
        private static long sLightUpdate;

        private static int sOldHeight;

        //Resolution
        private static int sOldWidth;

        private static long sOverlayUpdate;

        private static float sPlayerLightExpand;

        private static float sPlayerLightIntensity = 255;

        private static float sPlayerLightSize;

        public static GameFont UIFont;

        public static float BaseWorldScale => Options.Instance?.MapOpts?.TileScale ?? 1;

        private static float _currentShake;
        public static float CurrentShake
        {
            get => _currentShake;
            set
            {
                _currentShake = value;
            }
        }

        private static float mShakeDecrement = 0.12f;

        private static long sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;

        private static long sCurrentCombatWidth = 0;

        private static long sLastCombatWidthUpdate;

        private static byte mCombatModeState = 0;

        private static long sCutsceneWidth = 0;

        private static long sCutsceneUpdate;

        private static byte sCutsceneState = 0;

        //Init Functions
        public static void InitGraphics()
        {
            Renderer.Init();
            sContentManager = Globals.ContentManager;
            sContentManager.LoadAll();
            GameFont = FindFont(ClientConfiguration.Instance.GameFont);
            UIFont = FindFont(ClientConfiguration.Instance.UIFont);
            EntityNameFont = FindFont(ClientConfiguration.Instance.EntityNameFont);
            ChatBubbleFont = FindFont(ClientConfiguration.Instance.ChatBubbleFont);
            ActionMsgFont = FindFont(ClientConfiguration.Instance.ActionMsgFont);
            HUDFont = FindFont(ClientConfiguration.Instance.HudFont);
            HUDFontSmall = FindFont(ClientConfiguration.Instance.HudFontSmall);
            ToastFont = FindFont(ClientConfiguration.Instance.ToastFont);
            ToastFontSmall = FindFont(ClientConfiguration.Instance.ToastFontSmall);
            DamageFont = FindFont(ClientConfiguration.Instance.DamageFont);
            MenuTexture = sContentManager.GetTexture(
                GameContentManager.TextureType.Gui, ClientConfiguration.Instance.MenuBackground
            );
            LogoTexture = sContentManager.GetTexture(
                GameContentManager.TextureType.Gui, ClientConfiguration.Instance.Logo
            );
            CombatNumberManager.CacheTextureRefs();
        }

        public static GameFont FindFont(string font)
        {
            var size = 8;

            if (font.IndexOf(',') > -1)
            {
                var parts = font.Split(',');
                font = parts[0];
                int.TryParse(parts[1], out size);
            }

            return sContentManager.GetFont(font, size);
        }

        public static void InitInGame()
        {
            RenderingEntities = new HashSet<Entity>[6, Options.MapHeight * 5];
            for (var z = 0; z < 6; z++)
            {
                for (var i = 0; i < Options.MapHeight * 5; i++)
                {
                    RenderingEntities[z, i] = new HashSet<Entity>();
                }
            }
        }

        public static void DrawIntro()
        {
            // Forces a delay before showing intro, just to give everything a second.
            if (Timing.Global.MillisecondsUtcUnsynced < Globals.IntroBlackDelay)
            {
                return;
            }

            var introImages = ClientConfiguration.Instance.IntroImages;

            if (Globals.IntroIndex < 0 || Globals.IntroIndex >= introImages.Count)
            {
                return;
            }

            var imageTex = sContentManager.GetTexture(
                GameContentManager.TextureType.Image, introImages[Globals.IntroIndex]
            );
            if (Globals.AnimatedIntro)
            {
                AnimateIntro();
                DrawFullScreenTextureFitMinimum(imageTex, Globals.IntroHFrames, Globals.IntroVFrames, IntroHFrame, IntroVFrame);
            }
            else
            {
                DrawFullScreenTextureFitMinimum(imageTex);
            }
        }

        public static void DrawMenu()
        {
            if (MenuTexture == null)
            {
                return;
            }
            AnimateMainMenu();
            DrawFullScreenTextureFitMinimum(MenuTexture, MenuHFrames, MenuVFrames, MenuHFrame, MenuVFrame);
            DrawScanlines();
            DrawLogo();
        }

        public static void DrawInGame()
        {
            var currentMap = Globals.Me.MapInstance;
            if (currentMap == null)
            {
                return;
            }

            if (Globals.NeedsMaps)
            {
                return;
            }

            if (GridSwitched)
            {
                //Brightness
                var brightnessTarget = (byte)(currentMap.Brightness / 100f * 255);
                BrightnessLevel = brightnessTarget;
                PlayerLightColor.R = currentMap.PlayerLightColor.R;
                PlayerLightColor.G = currentMap.PlayerLightColor.G;
                PlayerLightColor.B = currentMap.PlayerLightColor.B;
                sPlayerLightSize = currentMap.PlayerLightSize;
                sPlayerLightIntensity = currentMap.PlayerLightIntensity;
                sPlayerLightExpand = currentMap.PlayerLightExpand;

                //Overlay
                OverlayColor.A = (byte)currentMap.AHue;
                OverlayColor.R = (byte)currentMap.RHue;
                OverlayColor.G = (byte)currentMap.GHue;
                OverlayColor.B = (byte)currentMap.BHue;

                //Fog && Panorama
                currentMap.GridSwitched();
                GridSwitched = false;
            }

            var animations = LiveAnimations.ToArray();
            foreach (var animInstance in animations)
            {
                if (animInstance.ParentGone())
                {
                    animInstance.Dispose();
                }
            }

            ClearDarknessTexture();

            var gridX = currentMap.MapGridX;
            var gridY = currentMap.MapGridY;

            //Draw Panoramas First...
            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 &&
                        x < Globals.MapGridWidth &&
                        y >= 0 &&
                        y < Globals.MapGridHeight &&
                        Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        DrawMapPanorama(Globals.MapGrid[x, y]);
                    }
                }
            }

            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 &&
                        x < Globals.MapGridWidth &&
                        y >= 0 &&
                        y < Globals.MapGridHeight &&
                        Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        DrawMap(Globals.MapGrid[x, y], 0);
                    }
                }
            }

            // Draw territories
            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 &&
                        x < Globals.MapGridWidth &&
                        y >= 0 &&
                        y < Globals.MapGridHeight &&
                        Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        var map = MapInstance.Get(Globals.MapGrid[x, y]);
                        if (map == null)
                        {
                            continue;
                        }

                        foreach (var territory in map.Territories.Values)
                        {
                            territory.Draw();
                        }
                    }
                }
            }

            for (var y = 0; y < Options.MapHeight * 5; y++)
            {
                for (var x = 0; x < 6; x++)
                {
                    foreach (var entity in RenderingEntities[x, y]) 
                    { 
                        if (entity.SpellCast == default || entity.CurrentMap == default)
                        {
                            continue;
                        }

                        var castSpell = SpellBase.Get(entity.SpellCast);
                        var map = MapInstance.Get(entity.CurrentMap);
                        switch (castSpell.Combat.TargetType)
                        {
                            case SpellTargetTypes.AoE:
                                entity.DrawAoe(castSpell, SpellTargetTypes.AoE, map, entity.X, entity.Y, entity.IsAllyOf(Globals.Me), castSpell.Combat.HitRadius);
                                break;
                            case SpellTargetTypes.Single:
                                if (!Globals.Entities.TryGetValue(entity.EntityTarget, out var target) || target.CurrentMap == default)
                                {
                                    break;
                                }

                                // Either you're the target or you're the caster
                                if (target.Id == Globals.Me.Id || entity.Id == Globals.Me.Id)
                                {
                                    entity.DrawAoe(castSpell, SpellTargetTypes.Single, map, entity.X, entity.Y, entity.IsAllyOf(Globals.Me), castSpell.Combat.CastRange, entity.Id != Globals.Me.Id);
                                }
                                if (castSpell.Combat.HitRadius <= 0)
                                {
                                    break;
                                }

                                var entityX = entity.GetCurrentTileRectangle().CenterX;
                                var entityY = entity.GetCurrentTileRectangle().CenterY;
                                var targetX = target.GetCurrentTileRectangle().CenterX;
                                var targetY = target.GetCurrentTileRectangle().CenterY;

                                var distance = Math.Floor(MathHelper.CalculateDistanceToPoint(entityX, entityY, targetX, targetY) / Options.TileWidth);
                                if (distance > castSpell.Combat.CastRange)
                                {
                                    break;
                                }

                                var targetMap = MapInstance.Get(target.CurrentMap);
                                target.DrawAoe(castSpell, SpellTargetTypes.AoE, targetMap, target.X, target.Y, entity.IsAllyOf(Globals.Me), castSpell.Combat.HitRadius);
                                break;
                            case SpellTargetTypes.Projectile:
                                entity.DrawProjectileSpawns(castSpell, map, entity.X, entity.Y, entity.IsAllyOf(Globals.Me));
                                break;
                        }
                    }
                }
            }

            foreach (var animInstance in animations)
            {
                animInstance.Draw(false);
            }

            for (var y = 0; y < Options.MapHeight * 5; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    foreach (var entity in RenderingEntities[x, y])
                    {
                        entity.Draw();
                        EntitiesDrawn++;
                    }

                    if (x == 0 && y > 0 && y % Options.MapHeight == 0)
                    {
                        for (var x1 = gridX - 1; x1 <= gridX + 1; x1++)
                        {
                            var y1 = gridY - 2 + (int) Math.Floor(y / (float) Options.MapHeight);
                            if (x1 >= 0 &&
                                x1 < Globals.MapGridWidth &&
                                y1 >= 0 &&
                                y1 < Globals.MapGridHeight &&
                                Globals.MapGrid[x1, y1] != Guid.Empty)
                            {
                                var map = MapInstance.Get(Globals.MapGrid[x1, y1]);
                                if (map != null)
                                {
                                    map.DrawItemsAndLights();
                                }
                            }
                        }
                    }
                }
            }

            foreach (var animInstance in animations)
            {
                animInstance.Draw(false, true);
                animInstance.Draw(true, true);
            }

            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 &&
                        x < Globals.MapGridWidth &&
                        y >= 0 &&
                        y < Globals.MapGridHeight &&
                        Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        DrawMap(Globals.MapGrid[x, y], 1);
                    }
                }
            }

            for (var y = 0; y < Options.MapHeight * 5; y++)
            {
                for (var x = 3; x < 6; x++)
                {
                    foreach (var entity in RenderingEntities[x, y])
                    {
                        entity.Draw();
                        EntitiesDrawn++;
                    }
                }
            }

            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 &&
                        x < Globals.MapGridWidth &&
                        y >= 0 &&
                        y < Globals.MapGridHeight &&
                        Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        DrawMap(Globals.MapGrid[x, y], 2);
                    }
                }
            }

            foreach (var animInstance in animations)
            {
                animInstance.Draw(true);
            }
            

            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x >= 0 &&
                        x < Globals.MapGridWidth &&
                        y >= 0 &&
                        y < Globals.MapGridHeight &&
                        Globals.MapGrid[x, y] != Guid.Empty)
                    {
                        var map = MapInstance.Get(Globals.MapGrid[x, y]);
                        if (map != null)
                        {
                            map.DrawWeather();
                            map.DrawFog();
                            map.DrawOverlayGraphic();
                            map.DrawItemNames();
                        }
                    }
                }
            }

            //Draw the players targets
            Globals.Me.DrawTargets();

            DrawOverlay();

            GenerateLightMap();
            DrawDarkness();

            for (var y = 0; y < Options.MapHeight * 5; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    foreach (var entity in RenderingEntities[x, y])
                    {
                        entity.DrawName(null);
                        entity.DrawStatuses();
                        if (entity.GetType() != typeof(Event))
                        {
                            entity.DrawHpBar();
                            entity.DrawCastingBar();
                            entity.DrawAggroIndicator(12, entity.IsAllyOf(Globals.Me));
                        }

                        entity.DrawChatBubbles();
                    }
                }
            }

            for (var y = 0; y < Options.MapHeight * 5; y++)
            {
                for (var x = 3; x < 6; x++)
                {
                    foreach (var entity in RenderingEntities[x, y])
                    {
                        entity.DrawName(null);
                        entity.DrawStatuses();
                        if (entity.GetType() != typeof(Event))
                        {
                            entity.DrawHpBar();
                            entity.DrawCastingBar();
                        }

                        entity.DrawChatBubbles();
                    }
                }
            }

            //Draw action msg's
            for (var x = gridX - 1; x <= gridX + 1; x++)
            {
                for (var y = gridY - 1; y <= gridY + 1; y++)
                {
                    if (x < 0 ||
                        x >= Globals.MapGridWidth ||
                        y < 0 ||
                        y >= Globals.MapGridHeight ||
                        Globals.MapGrid[x, y] == Guid.Empty)
                    {
                        continue;
                    }

                    var map = MapInstance.Get(Globals.MapGrid[x, y]);
                    map?.DrawActionMsgs();
                }
            }

            CombatNumberManager.UpdateAndDrawCombatNumbers();

            foreach (var animInstance in animations)
            {
                animInstance.EndDraw();
            }

            DrawScanlines();
            
            // Because we want to render widescreen textures with different colors depending on the estimated background color of the map
            if (Globals.Me.MapInstance.IsIndoors && Globals.Me.MapInstance.Brightness <= 70 || Globals.Me.MapInstance.IsIndoors)
            {
                DrawWideScreen(Globals.ContentManager.GetTexture(GameContentManager.TextureType.Misc, "combatmode.png"), Globals.Me.CombatMode, Color.White,
                    ref mCombatModeState, ref sLastCombatWidthUpdate, ref sCurrentCombatWidth);
                DrawWideScreen(Renderer.GetWhiteTexture(), Globals.Me.InCutscene(), new Color(255, 20, 20, 20),
                    ref sCutsceneState, ref sCutsceneUpdate, ref sCutsceneWidth, false);
            }
            else 
            {
                // Use a _black_ background when in lighter areas
                DrawWideScreen(Globals.ContentManager.GetTexture(GameContentManager.TextureType.Misc, "combatmode.png"), Globals.Me.CombatMode, Color.Black,
                    ref mCombatModeState, ref sLastCombatWidthUpdate, ref sCurrentCombatWidth);
                DrawWideScreen(Renderer.GetWhiteTexture(), Globals.Me.InCutscene(), Color.Black,
                    ref sCutsceneState, ref sCutsceneUpdate, ref sCutsceneWidth, false);
            }
        }

        public static void DrawScanlines()
        {
            if (Globals.Database.EnableScanlines)
            {
                var imageTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Image, "scanlines.png");
                if (imageTex != null)
                {
                    DrawFullScreenTexture(imageTex, 1f);
                }
            }
        }

        //Game Rendering
        public static void Render(TimeSpan deltaTime, TimeSpan totalTime)
        {
            var takingScreenshot = false;
            if (Renderer?.ScreenshotRequests.Count > 0)
            {
                takingScreenshot = Renderer.BeginScreenshot();
            }
            
            if (Renderer == default)
            {
                return;
            }

            Renderer.Scale = Globals.GameState == GameStates.InGame ? Globals.Database.WorldZoom : 1.0f;

            if (!Renderer.Begin())
            {
                return;
            }

            if (Renderer.GetScreenWidth() != sOldWidth ||
                Renderer.GetScreenHeight() != sOldHeight ||
                Renderer.DisplayModeChanged())
            {
                sDarknessTexture = null;
                Interface.Interface.DestroyGwen();
                Interface.Interface.InitGwen();
                sOldWidth = Renderer.GetScreenWidth();
                sOldHeight = Renderer.GetScreenHeight();
            }

            Renderer.Clear(Color.Black);
            DrawCalls = 0;
            MapsDrawn = 0;
            EntitiesDrawn = 0;
            LightsDrawn = 0;

            UpdateView();

            switch (Globals.GameState)
            {
                case GameStates.Intro:
                    DrawIntro();

                    break;
                case GameStates.Menu:
                    DrawMenu();

                    break;
                case GameStates.Loading:
                    break;
                case GameStates.InGame:
                    DrawInGame();

                    break;
                case GameStates.Error:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Renderer.Scale = Globals.Database.UIScale;

            var fadeDrawn = false;
            if (Globals.GameState == GameStates.InGame || Globals.GameState == GameStates.Loading)
            {
                DrawFadeOrWipe();
                fadeDrawn = true;
            }

            DrawFlash();

            Interface.Interface.DrawGui();

            if (!fadeDrawn)
            {
                DrawFadeOrWipe();
            }

            // Draw our mousecursor at the very end, but not when taking screenshots.
            if (!takingScreenshot && !string.IsNullOrWhiteSpace(ClientConfiguration.Instance.MouseCursor))
            {
                var renderLoc = ConvertToWindowPoint(Globals.InputManager.GetMousePosition());
                DrawGameTexture(
                    Globals.ContentManager.GetTexture(GameContentManager.TextureType.Misc, ClientConfiguration.Instance.MouseCursor), renderLoc.X, renderLoc.Y
               );
            }
            
            Renderer.End();

            if (takingScreenshot)
            {
                Renderer.EndScreenshot();
            }
        }

        private static void DrawFlash()
        {
            DrawGameTexture(
                Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), CurrentView,
                new Color((int)Flash.GetFlash(), Flash.GetColor().R, Flash.GetColor().G,
                Flash.GetColor().B), null, GameBlendModes.None
            );
        }

        private static void DrawFadeOrWipe()
        {
            if (FadeService.FadeInstead)
            {
                // Draw the current Fade
                DrawGameTexture(
                    Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), CurrentView,
                    new Color((int)Fade.GetFade(), 0, 0, 0), null, GameBlendModes.None
                );
            }
            else // Wipe transition
            {
                // Left shutter
                DrawGameTexture(
                    Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), new FloatRect(CurrentView.X, CurrentView.Y, (int)Wipe.GetFade(), CurrentView.Height),
                    new Color(255, 0, 0, 0), null, GameBlendModes.None
                );
                // Right shutter
                DrawGameTexture(
                    Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), new FloatRect(CurrentView.X + (CurrentView.Width / 2 + (int)Wipe.GetFade(true)), CurrentView.Y, CurrentView.Width / 2, CurrentView.Height),
                    new Color(255, 0, 0, 0), null, GameBlendModes.None
                );
            }
        }

        private static void DrawMap(Guid mapId, int layer = 0)
        {
            var map = MapInstance.Get(mapId);
            if (map == null)
            {
                return;
            }

            if (!new FloatRect(
                map.GetX(), map.GetY(), Options.TileWidth * Options.MapWidth, Options.TileHeight * Options.MapHeight
            ).IntersectsWith(Viewport))
            {
                return;
            }

            map.Draw(layer);
            if (layer == 0)
            {
                MapsDrawn++;
            }
        }

        private static void DrawMapPanorama(Guid mapId)
        {
            var map = MapInstance.Get(mapId);
            if (map != null)
            {
                if (!new FloatRect(
                    map.GetX(), map.GetY(), Options.TileWidth * Options.MapWidth, Options.TileHeight * Options.MapHeight
                ).IntersectsWith(Viewport))
                {
                    return;
                }

                map.DrawPanorama();
            }
        }

        public static void DrawWideScreen(GameTexture texture, bool flag, Color drawColor, ref byte state, ref long lastUpdate, ref long size, bool regardHud = true)
        {
            // Zoom-independant
            var prevScale = Renderer.Scale;
            Renderer.Scale = 1;

            FloatRect top;
            FloatRect left;
            FloatRect right;
            FloatRect bottom;
            var textureWidth = texture.GetWidth();

            float currTop = !Interface.Interface.HideUi && regardHud ? CurrentView.Top + PlayerHud.Height : CurrentView.Top;

            switch (state)
            {
                // Empty
                case 0:
                    if (flag)
                    {
                        state = 1;
                    }
                    break;
                // reset
                case 1:
                    lastUpdate = Timing.Global.MillisecondsUtcUnsynced;
                    state = flag ? (byte)2 : (byte)4;
                    break;
                // fade in
                case 2:
                    size = MathHelper.Clamp((int)Math.Round(((Timing.Global.MillisecondsUtcUnsynced - lastUpdate) / 250f) * 64f), 0, 64);
                    for(var i = 0; i < CurrentView.Width / textureWidth; i++)
                    {
                        top = new FloatRect(CurrentView.Left + (i * textureWidth), currTop, textureWidth, size);
                        bottom = new FloatRect(CurrentView.Left + (i * textureWidth), CurrentView.Top + CurrentView.Height - size, textureWidth, size);

                        DrawGameTexture(texture, new FloatRect(0, 0, 64, size), top, drawColor, null);
                        DrawGameTexture(texture, new FloatRect(0, 0, 64, size), bottom, drawColor, null, rotationDegrees: 180.0f);
                    }

                    if (!flag)
                    {
                        var now = Timing.Global.MillisecondsUtcUnsynced;
                        var remainingTransition = 250 - (Timing.Global.MillisecondsUtcUnsynced - lastUpdate);
                        lastUpdate = now - remainingTransition;
                        state = 4;
                    }
                    if (size >= 64)
                    {
                        state = 3;
                    }
                    break;
                // Done
                case 3:
                    for (var i = 0; i < CurrentView.Width / textureWidth; i++)
                    {
                        top = new FloatRect(CurrentView.Left + (i * textureWidth), currTop, textureWidth, size);
                        bottom = new FloatRect(CurrentView.Left + (i * textureWidth), CurrentView.Top + CurrentView.Height - size, textureWidth, size);

                        DrawGameTexture(texture, new FloatRect(0, 0, 64, size), top, drawColor, null);
                        DrawGameTexture(texture, new FloatRect(0, 0, 64, size), bottom, drawColor, null, rotationDegrees: 180.0f);
                    }

                    if (!flag)
                    {
                        lastUpdate = Timing.Global.MillisecondsUtcUnsynced;
                        state = 4;
                    }
                    break;
                // Fade out
                case 4:
                    size = MathHelper.Clamp(64 - ((int)Math.Round(((Timing.Global.MillisecondsUtcUnsynced - lastUpdate) / 250f) * 64f)), 0, 64);
                    for (var i = 0; i < CurrentView.Width / textureWidth; i++)
                    {
                        top = new FloatRect(CurrentView.Left + (i * textureWidth), currTop, textureWidth, size);
                        bottom = new FloatRect(CurrentView.Left + (i * textureWidth), CurrentView.Top + CurrentView.Height - size, textureWidth, size);

                        DrawGameTexture(texture, new FloatRect(0, 0, 64, size), top, drawColor, null);
                        DrawGameTexture(texture, new FloatRect(0, 0, 64, size), bottom, drawColor, null, rotationDegrees: 180.0f);
                    }
                    if (flag)
                    {
                        var now = Timing.Global.MillisecondsUtcUnsynced;
                        var remainingTransition = 250 - (Timing.Global.MillisecondsUtcUnsynced - lastUpdate);
                        lastUpdate = now - remainingTransition;
                        state = 2;
                    }
                    if (size <= 0)
                    {
                        state = 0;
                    }
                    break;
                default:
                    state = 0;
                    break;
            }

            Renderer.Scale = prevScale;
        }

        public static void DrawOverlay()
        {
            var map = MapInstance.Get(Globals.Me.CurrentMap);
            if (map != null)
            {
                float ecTime = Timing.Global.MillisecondsUtcUnsynced - sOverlayUpdate;

                if (OverlayColor.A != map.AHue ||
                    OverlayColor.R != map.RHue ||
                    OverlayColor.G != map.GHue ||
                    OverlayColor.B != map.BHue)
                {
                    if (OverlayColor.A < map.AHue)
                    {
                        if (OverlayColor.A + (int) (255 * ecTime / 2000f) > map.AHue)
                        {
                            OverlayColor.A = (byte) map.AHue;
                        }
                        else
                        {
                            OverlayColor.A += (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.A > map.AHue)
                    {
                        if (OverlayColor.A - (int) (255 * ecTime / 2000f) < map.AHue)
                        {
                            OverlayColor.A = (byte) map.AHue;
                        }
                        else
                        {
                            OverlayColor.A -= (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.R < map.RHue)
                    {
                        if (OverlayColor.R + (int) (255 * ecTime / 2000f) > map.RHue)
                        {
                            OverlayColor.R = (byte) map.RHue;
                        }
                        else
                        {
                            OverlayColor.R += (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.R > map.RHue)
                    {
                        if (OverlayColor.R - (int) (255 * ecTime / 2000f) < map.RHue)
                        {
                            OverlayColor.R = (byte) map.RHue;
                        }
                        else
                        {
                            OverlayColor.R -= (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.G < map.GHue)
                    {
                        if (OverlayColor.G + (int) (255 * ecTime / 2000f) > map.GHue)
                        {
                            OverlayColor.G = (byte) map.GHue;
                        }
                        else
                        {
                            OverlayColor.G += (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.G > map.GHue)
                    {
                        if (OverlayColor.G - (int) (255 * ecTime / 2000f) < map.GHue)
                        {
                            OverlayColor.G = (byte) map.GHue;
                        }
                        else
                        {
                            OverlayColor.G -= (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.B < map.BHue)
                    {
                        if (OverlayColor.B + (int) (255 * ecTime / 2000f) > map.BHue)
                        {
                            OverlayColor.B = (byte) map.BHue;
                        }
                        else
                        {
                            OverlayColor.B += (byte) (255 * ecTime / 2000f);
                        }
                    }

                    if (OverlayColor.B > map.BHue)
                    {
                        if (OverlayColor.B - (int) (255 * ecTime / 2000f) < map.BHue)
                        {
                            OverlayColor.B = (byte) map.BHue;
                        }
                        else
                        {
                            OverlayColor.B -= (byte) (255 * ecTime / 2000f);
                        }
                    }
                }
            }

            DrawGameTexture(Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1), CurrentView, OverlayColor, null);
            sOverlayUpdate = Timing.Global.MillisecondsUtcUnsynced;
        }

        public static FloatRect GetSourceRect(GameTexture gameTexture, int hFrames = 1, int vFrames = 1, int currHframe = 0, int currVframe = 0)
        {
            var srcWidth = gameTexture.GetWidth() / hFrames;
            var srcHeight = gameTexture.GetHeight() / vFrames;
            var srcX = srcWidth * currHframe;
            var srcY = srcHeight * currVframe;

            return gameTexture == null
                ? new FloatRect()
                : new FloatRect(srcX, srcY, srcWidth, srcHeight);
        }

        public static void DrawFullScreenTexture(GameTexture tex, float alpha = 1f)
        {
            DrawFullScreenTexture(tex, new Color((int)(alpha * 255f), 255, 255, 255));
        }

        public static void DrawFullScreenTexture(GameTexture tex, Color color)
        {
            // Always a scale of 1 if we're drawing something fullscreen
            var prevScale = Renderer.Scale;
            Renderer.Scale = 1;

            var bgx = Renderer.GetScreenWidth() / 2 - tex.GetWidth() / 2;
            var bgy = Renderer.GetScreenHeight() / 2 - tex.GetHeight() / 2;
            var bgw = tex.GetWidth();
            var bgh = tex.GetHeight();
            var diff = 0;
            if (bgw < Renderer.GetScreenWidth())
            {
                diff = Renderer.GetScreenWidth() - bgw;
                bgx -= diff / 2;
                bgw += diff;
            }

            if (bgh < Renderer.GetScreenHeight())
            {
                diff = Renderer.GetScreenHeight() - bgh;
                bgy -= diff / 2;
                bgh += diff;
            }

            DrawGameTexture(
                tex, GetSourceRect(tex),
                new FloatRect(bgx + Renderer.GetView().X, bgy + Renderer.GetView().Y, bgw, bgh),
                color
            );

            Renderer.Scale = prevScale;
        }

        public static void DrawFullScreenTextureCutoff(GameTexture tex, float alpha = 1f)
        {
            var bgx = Renderer.GetScreenWidth() / 2 - tex.GetWidth() / 2;
            var bgy = Renderer.GetScreenHeight() / 2 - tex.GetHeight() / 2;
            var bgw = tex.GetWidth();
            var bgh = tex.GetHeight();

            DrawGameTexture(
                tex, GetSourceRect(tex),
                new FloatRect(bgx + Renderer.GetView().X, bgy + Renderer.GetView().Y, bgw, bgh),
                new Color((int)(alpha * 255f), 255, 255, 255)
            );
        }

        public static void DrawFullScreenTextureStretched(GameTexture tex, int hFrames = 1, int vFrames = 1, int currHframe = 0, int currVframe = 0)
        {
            DrawGameTexture(
                tex, GetSourceRect(tex, hFrames, vFrames, currHframe, currVframe),
                new FloatRect(
                    Renderer.GetView().X, Renderer.GetView().Y, Renderer.GetScreenWidth(), Renderer.GetScreenHeight()
                ), Color.White
            );
        }

        public static void DrawFullScreenTextureFitWidth(GameTexture tex, int hFrames = 1, int vFrames = 1, int currHframe = 0, int currVframe = 0)
        {
            var scale = Renderer.GetScreenWidth() / (float) tex.GetWidth();
            var scaledHeight = tex.GetHeight() * scale;
            var offsetY = (Renderer.GetScreenHeight() - tex.GetHeight()) / 2f;
            DrawGameTexture(
                tex, GetSourceRect(tex, hFrames, vFrames, currHframe, currVframe),
                new FloatRect(
                    Renderer.GetView().X, Renderer.GetView().Y + offsetY, Renderer.GetScreenWidth(), scaledHeight
                ), Color.White
            );
        }

        public static void DrawFullScreenTextureFitHeight(GameTexture tex, int hFrames = 1, int vFrames = 1, int currHframe = 0, int currVframe = 0)
        {
            var scale = Renderer.GetScreenHeight() / ((float) tex.GetHeight() / vFrames);
            var scaledWidth = (tex.GetWidth() / hFrames) * scale;
            var offsetX = (Renderer.GetScreenWidth() - scaledWidth) / 2f;
            var sourceRect = GetSourceRect(tex, hFrames, vFrames, currHframe, currVframe);
            var destRect = new FloatRect(
                Renderer.GetView().X + offsetX, Renderer.GetView().Y, scaledWidth, Renderer.GetScreenHeight()
            );
            
            DrawGameTexture(tex, sourceRect, destRect, Color.White);
        }

        public static void DrawFullScreenTextureFitMinimum(GameTexture tex, int hFrames = 1, int vFrames = 1, int currHframe = 0, int currVframe = 0)
        {
            if (Renderer.GetScreenWidth() > Renderer.GetScreenHeight())
            {
                DrawFullScreenTextureFitHeight(tex, hFrames, vFrames, currHframe, currVframe);
            }
            else
            {
                DrawFullScreenTextureFitWidth(tex, hFrames, vFrames, currHframe, currVframe);
            }
        }

        public static void DrawFullScreenTextureFitMaximum(GameTexture tex)
        {
            if (Renderer.GetScreenWidth() < Renderer.GetScreenHeight())
            {
                DrawFullScreenTextureFitHeight(tex);
            }
            else
            {
                DrawFullScreenTextureFitWidth(tex);
            }
        }

        private static void UpdateView()
        {
            var scale = Renderer.Scale;

            if (Globals.GameState != GameStates.InGame || !MapInstance.TryGet(Globals.Me?.MapInstance?.Id ?? Guid.Empty, out _))
            {
                var sw = Renderer.GetScreenWidth();
                var sh = Renderer.GetScreenHeight();
                var sx = 0;//sw - (sw / scale);
                var sy = 0;//sh - (sh / scale);
                CurrentView = new FloatRect(sx, sy, sw / scale, sh / scale);
                CurrentShake = 0.0f;
                SetLastUpdate();
                return;
            }

            var map = Globals.Me.MapInstance;
            if (map == default)
            {
                SetLastUpdate();
                return;
            }

            var mapWidth = Options.MapWidth * Options.TileWidth;
            var mapHeight = Options.MapHeight * Options.TileHeight;

            var en = Globals.Me;
            float x = mapWidth;
            float y = mapHeight;
            float x1 = mapWidth * 2;
            float y1 = mapHeight * 2;

            if (map.CameraHolds[(int)Directions.Left])
            {
                x -= mapWidth;
            }

            if (map.CameraHolds[(int)Directions.Up])
            {
                y -= mapHeight;
            }

            if (map.CameraHolds[(int)Directions.Right])
            {
                x1 -= mapWidth;
            }

            if (map.CameraHolds[(int)Directions.Down])
            {
                y1 -= mapHeight;
            }

            x = map.GetX() - x;
            y = map.GetY() - y;
            x1 = map.GetX() + x1;
            y1 = map.GetY() + y1;

            var w = x1 - x;
            var h = y1 - y;
            var restrictView = new FloatRect(
                x,
                y,
                w,
                h
            );


            // Screen Shake
            var shakeReduction = Math.Max((Timing.Global.MillisecondsUtcUnsynced - sLastUpdate) / Options.ShakeDeltaDurationDivider, 0);
            CurrentShake = Utilities.MathHelper.Clamp(CurrentShake - shakeReduction, 0.0f, 100.0f);

            var yShake = CurrentShake;
            var xShake = CurrentShake;
            if (CurrentShake > 0.0f)
            {
                // Randomize which directions we're shaking in
                if (Randomization.Next(0, 2) == 1)
                {
                    yShake *= -1;
                }
                if (Randomization.Next(0, 2) == 1)
                {
                    xShake *= -1;
                }
            }
            SetLastUpdate();

            var centeredX = (int)Math.Ceiling(en.GetCenterPos().X - Renderer.GetScreenWidth() / scale / 2f) + xShake;
            var centeredY = (int)Math.Ceiling(en.GetCenterPos().Y - Renderer.GetScreenHeight() / scale / 2f) + yShake;

            var newView = new FloatRect(
                centeredX,
                centeredY,
                Renderer.GetScreenWidth() / scale,
                Renderer.GetScreenHeight() / scale
            );

            if (restrictView.Width >= newView.Width)
            {
                if (newView.Left < restrictView.Left)
                {
                    newView.X = restrictView.Left;
                }

                if (newView.Right > restrictView.Right)
                {
                    newView.X = restrictView.Right - newView.Width;
                }
            }

            if (restrictView.Height >= newView.Height)
            {
                if (newView.Top < restrictView.Top)
                {
                    newView.Y = restrictView.Top;
                }

                if (newView.Bottom > restrictView.Bottom)
                {
                    newView.Y = restrictView.Bottom - newView.Height;
                }
            }

            CurrentView = new FloatRect(
                newView.X,
                newView.Y,
                newView.Width * scale,
                newView.Height * scale
            );
        }

        public static void SetLastUpdate()
        {
            sLastUpdate = Timing.Global.MillisecondsUtcUnsynced;
        }

        //Lighting
        private static void ClearDarknessTexture()
        {
            if (sDarknessTexture == null)
            {
                sDarknessTexture = Renderer.CreateRenderTexture(Renderer.GetScreenWidth(), Renderer.GetScreenHeight());
            }

            sDarknessTexture.Clear(Color.Black);
        }

        private static void GenerateLightMap()
        {
            if (!Globals.Database.DisplayLighting)
            {
                return;
            }

            var map = MapInstance.Get(Globals.Me.CurrentMap);
            if (map == null)
            {
                return;
            }

            if (sDarknessTexture == null)
            {
                return;
            }

            var destRect = new FloatRect(new Pointf(), new Pointf(sDarknessTexture.GetWidth() / Globals.Database.WorldZoom, sDarknessTexture.GetHeight() / Globals.Database.WorldZoom));
            if (map.IsIndoors)
            {
                DrawGameTexture(
                    Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1),
                    destRect,
                    new Color((byte) BrightnessLevel, 255, 255, 255), sDarknessTexture, GameBlendModes.Add
                );
            }
            else
            {
                // ALEX - Added this set so that darkness levels could easily be calculated when rendering animations
                BrightnessLevel = 255 - Time.GetTintColor().A;
                DrawGameTexture(
                    Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1),
                    destRect,
                    new Color(255, 255, 255, 255), sDarknessTexture, GameBlendModes.Add
                );

                DrawGameTexture(
                    Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1),
                    destRect,
                    new Color(
                        (int) Time.GetTintColor().A, (int) Time.GetTintColor().R, (int) Time.GetTintColor().G,
                        (int) Time.GetTintColor().B
                    ), sDarknessTexture, GameBlendModes.None
                );
            }

            AddLight(
                (int) Math.Ceiling(Globals.Me.GetCenterPos().X), (int) Math.Ceiling(Globals.Me.GetCenterPos().Y),
                (int) sPlayerLightSize, (byte) sPlayerLightIntensity, sPlayerLightExpand,
                Color.FromArgb(
                    (int) PlayerLightColor.A, (int) PlayerLightColor.R, (int) PlayerLightColor.G,
                    (int) PlayerLightColor.B
                )
            );

            DrawLights();
            sDarknessTexture.End();
        }

        public static void DrawDarkness()
        {
            if (!Globals.Database.DisplayLighting)
            {
                return;
            }

            var radialShader = Globals.ContentManager.GetShader("radialgradient");
            if (radialShader != null)
            {
                DrawGameTexture(
                   sDarknessTexture,
                   sDarknessTexture.Bounds,
                   Viewport,
                   Color.White,
                   blendMode: GameBlendModes.Multiply
               );
            }
        }

        public static void AddLight(int x, int y, int size, byte intensity, float expand, Color color)
        {
            if (!Globals.Database.DisplayLighting)
            {
                return;
            }

            if (size == 0)
            {
                return;
            }

            sLightQueue.Add(new LightBase(0, 0, x, y, intensity, size, expand, color));
            LightsDrawn++;
        }

        private static void DrawLights()
        {
            if (!Globals.Database.DisplayLighting)
            {
                return;
            }

            var radialShader = Globals.ContentManager.GetShader("radialgradient");
            if (radialShader != null)
            {
                foreach (var light in sLightQueue.GroupBy(c => c.GetHashCode()))
                {
                    foreach (var l in light)
                    {
                        var x = l.OffsetX - ((int)CurrentView.Left + l.Size);
                        var y = l.OffsetY - ((int)CurrentView.Top + l.Size);

                        radialShader.SetColor("LightColor", new Color(l.Intensity, l.Color.R, l.Color.G, l.Color.B));
                        radialShader.SetFloat("Expand", l.Expand / 100f);

                        DrawGameTexture(
                            Renderer.GetWhiteTexture(), new FloatRect(0, 0, 1, 1),
                            new FloatRect(x, y, l.Size * 2, l.Size * 2), new Color(255, 255, 255, 255), sDarknessTexture, GameBlendModes.Add, radialShader, 0, false
                        );

                    }
                }

                sLightQueue.Clear();
            }
        }

        public static void UpdatePlayerLight()
        {
            if (!Globals.Database.DisplayLighting)
            {
                return;
            }

            //Draw Light Around Player
            var map = MapInstance.Get(Globals.Me.CurrentMap);
            if (map != null)
            {
                float ecTime = Timing.Global.MillisecondsUtcUnsynced - sLightUpdate;
                var valChange = 255 * ecTime / 2000f;
                var brightnessTarget = (byte) (map.Brightness / 100f * 255);
                if (BrightnessLevel < brightnessTarget)
                {
                    if (BrightnessLevel + valChange > brightnessTarget)
                    {
                        BrightnessLevel = brightnessTarget;
                    }
                    else
                    {
                        BrightnessLevel += valChange;
                    }
                }

                if (BrightnessLevel > brightnessTarget)
                {
                    if (BrightnessLevel - valChange < brightnessTarget)
                    {
                        BrightnessLevel = brightnessTarget;
                    }
                    else
                    {
                        BrightnessLevel -= valChange;
                    }
                }

                if (PlayerLightColor.R != map.PlayerLightColor.R ||
                    PlayerLightColor.G != map.PlayerLightColor.G ||
                    PlayerLightColor.B != map.PlayerLightColor.B)
                {
                    if (PlayerLightColor.R < map.PlayerLightColor.R)
                    {
                        if (PlayerLightColor.R + valChange > map.PlayerLightColor.R)
                        {
                            PlayerLightColor.R = map.PlayerLightColor.R;
                        }
                        else
                        {
                            PlayerLightColor.R += valChange;
                        }
                    }

                    if (PlayerLightColor.R > map.PlayerLightColor.R)
                    {
                        if (PlayerLightColor.R - valChange < map.PlayerLightColor.R)
                        {
                            PlayerLightColor.R = map.PlayerLightColor.R;
                        }
                        else
                        {
                            PlayerLightColor.R -= valChange;
                        }
                    }

                    if (PlayerLightColor.G < map.PlayerLightColor.G)
                    {
                        if (PlayerLightColor.G + valChange > map.PlayerLightColor.G)
                        {
                            PlayerLightColor.G = map.PlayerLightColor.G;
                        }
                        else
                        {
                            PlayerLightColor.G += valChange;
                        }
                    }

                    if (PlayerLightColor.G > map.PlayerLightColor.G)
                    {
                        if (PlayerLightColor.G - valChange < map.PlayerLightColor.G)
                        {
                            PlayerLightColor.G = map.PlayerLightColor.G;
                        }
                        else
                        {
                            PlayerLightColor.G -= valChange;
                        }
                    }

                    if (PlayerLightColor.B < map.PlayerLightColor.B)
                    {
                        if (PlayerLightColor.B + valChange > map.PlayerLightColor.B)
                        {
                            PlayerLightColor.B = map.PlayerLightColor.B;
                        }
                        else
                        {
                            PlayerLightColor.B += valChange;
                        }
                    }

                    if (PlayerLightColor.B > map.PlayerLightColor.B)
                    {
                        if (PlayerLightColor.B - valChange < map.PlayerLightColor.B)
                        {
                            PlayerLightColor.B = map.PlayerLightColor.B;
                        }
                        else
                        {
                            PlayerLightColor.B -= valChange;
                        }
                    }
                }

                if (sPlayerLightSize != map.PlayerLightSize)
                {
                    if (sPlayerLightSize < map.PlayerLightSize)
                    {
                        if (sPlayerLightSize + 500 * ecTime / 2000f > map.PlayerLightSize)
                        {
                            sPlayerLightSize = map.PlayerLightSize;
                        }
                        else
                        {
                            sPlayerLightSize += 500 * ecTime / 2000f;
                        }
                    }

                    if (sPlayerLightSize > map.PlayerLightSize)
                    {
                        if (sPlayerLightSize - 500 * ecTime / 2000f < map.PlayerLightSize)
                        {
                            sPlayerLightSize = map.PlayerLightSize;
                        }
                        else
                        {
                            sPlayerLightSize -= 500 * ecTime / 2000f;
                        }
                    }
                }

                if (sPlayerLightIntensity < map.PlayerLightIntensity)
                {
                    if (sPlayerLightIntensity + valChange > map.PlayerLightIntensity)
                    {
                        sPlayerLightIntensity = map.PlayerLightIntensity;
                    }
                    else
                    {
                        sPlayerLightIntensity += valChange;
                    }
                }

                if (sPlayerLightIntensity > map.AHue)
                {
                    if (sPlayerLightIntensity - valChange < map.PlayerLightIntensity)
                    {
                        sPlayerLightIntensity = map.PlayerLightIntensity;
                    }
                    else
                    {
                        sPlayerLightIntensity -= valChange;
                    }
                }

                if (sPlayerLightExpand < map.PlayerLightExpand)
                {
                    if (sPlayerLightExpand + 100f * ecTime / 2000f > map.PlayerLightExpand)
                    {
                        sPlayerLightExpand = map.PlayerLightExpand;
                    }
                    else
                    {
                        sPlayerLightExpand += 100f * ecTime / 2000f;
                    }
                }

                if (sPlayerLightExpand > map.PlayerLightExpand)
                {
                    if (sPlayerLightExpand - 100f * ecTime / 2000f < map.PlayerLightExpand)
                    {
                        sPlayerLightExpand = map.PlayerLightExpand;
                    }
                    else
                    {
                        sPlayerLightExpand -= 100f * ecTime / 2000f;
                    }
                }

                // Cap instensity between 0 and 255 so as not to overflow (as it is an alpha value)
                sPlayerLightIntensity = (float) MathHelper.Clamp(sPlayerLightIntensity, 0f, 255f);
                sLightUpdate = Timing.Global.MillisecondsUtcUnsynced;
            }
        }

        //Helper Functions
        /// <summary>
        /// Convert a position on the screen to a position on the actual map for rendering.
        /// </summary>
        /// <param name="windowPoint">The point to convert.</param>
        /// <returns>The converted point.</returns>
        public static Pointf ConvertToWorldPoint(Pointf windowPoint)
        {
            return new Pointf(
                (int)Math.Floor(windowPoint.X / Globals.Database.WorldZoom + CurrentView.Left),
                (int)Math.Floor(windowPoint.Y / Globals.Database.WorldZoom + CurrentView.Top)
            );
        }

        public static Pointf ConvertToWindowPoint(Pointf windowPoint)
        {
            return new Pointf((int)Math.Floor(windowPoint.X + CurrentView.Left), (int)Math.Floor(windowPoint.Y + CurrentView.Top));
        }
        //Rendering Functions

        /// <summary>
        ///     Renders a specified texture onto a RenderTexture or the GameScreen (if renderTarget is passed as null) at the
        ///     coordinates given using a specified blending mode.
        /// </summary>
        /// <param name="tex">The texture to draw</param>
        /// <param name="x">X coordinate on the render target to draw to</param>
        /// <param name="y">Y coordinate on the render target to draw to</param>
        /// <param name="renderTarget">Where to draw to. If null it this will draw to the game screen.</param>
        /// <param name="blendMode">Which blend mode to use when rendering</param>
        public static void DrawGameTexture(
            GameTexture tex,
            float x,
            float y,
            GameRenderTexture renderTarget = null,
            GameBlendModes blendMode = GameBlendModes.None,
            GameShader shader = null,
            float rotationDegrees = 0.0f,
            bool drawImmediate = false
        )
        {
            var destRectangle = new FloatRect(x, y, tex.GetWidth(), tex.GetHeight());
            var srcRectangle = new FloatRect(0, 0, tex.GetWidth(), tex.GetHeight());
            DrawGameTexture(
                tex, srcRectangle, destRectangle, Color.White, renderTarget, blendMode, shader, rotationDegrees,
                drawImmediate
            );
        }

        /// <summary>
        ///     Renders a specified texture onto a RenderTexture or the GameScreen (if renderTarget is passed as null) at the
        ///     coordinates given using a specified blending mode.
        /// </summary>
        /// <param name="tex">The texture to draw</param>
        /// <param name="x">X coordinate on the render target to draw to</param>
        /// <param name="y">Y coordinate on the render target to draw to</param>
        /// <param name="renderColor">Color mask to draw with. Default is Color.White</param>
        /// <param name="renderTarget">Where to draw to. If null it this will draw to the game screen.</param>
        /// <param name="blendMode">Which blend mode to use when rendering</param>
        public static void DrawGameTexture(
            GameTexture tex,
            float x,
            float y,
            Color renderColor,
            GameRenderTexture renderTarget = null,
            GameBlendModes blendMode = GameBlendModes.None,
            GameShader shader = null,
            float rotationDegrees = 0.0f,
            bool drawImmediate = false
        )
        {
            var destRectangle = new FloatRect(x, y, tex.GetWidth(), tex.GetHeight());
            var srcRectangle = new FloatRect(0, 0, tex.GetWidth(), tex.GetHeight());
            DrawGameTexture(
                tex, srcRectangle, destRectangle, renderColor, renderTarget, blendMode, shader, rotationDegrees,
                drawImmediate
            );
        }

        /// <summary>
        ///     Renders a specified texture onto a RenderTexture or the GameScreen (if renderTarget is passed as null) at the
        ///     coordinates given using a specified blending mode.
        /// </summary>
        /// <param name="tex">The texture to draw</param>
        /// <param name="dx">X coordinate on the renderTarget to draw to.</param>
        /// <param name="dy">Y coordinate on the renderTarget to draw to.</param>
        /// <param name="sx">X coordinate on the source texture to grab from.</param>
        /// <param name="sy">Y coordinate on the source texture to grab from.</param>
        /// <param name="w">Width of the texture part we are rendering.</param>
        /// <param name="h">Height of the texture part we are rendering.</param>
        /// <param name="renderTarget">>Where to draw to. If null it this will draw to the game screen.</param>
        /// <param name="blendMode">Which blend mode to use when rendering</param>
        public static void DrawGameTexture(
            GameTexture tex,
            float dx,
            float dy,
            float sx,
            float sy,
            float w,
            float h,
            GameRenderTexture renderTarget = null,
            GameBlendModes blendMode = GameBlendModes.None,
            GameShader shader = null,
            float rotationDegrees = 0.0f,
            bool drawImmediate = false
        )
        {
            if (tex == null)
            {
                return;
            }

            Renderer.DrawTexture(
                tex, sx, sy, w, h, dx, dy, w, h, Color.White, renderTarget, blendMode, shader, rotationDegrees, false,
                drawImmediate
            );
        }

        public static void DrawGameTexture(
            GameTexture tex,
            FloatRect srcRectangle,
            FloatRect targetRect,
            Color renderColor,
            GameRenderTexture renderTarget = null,
            GameBlendModes blendMode = GameBlendModes.None,
            GameShader shader = null,
            float rotationDegrees = 0.0f,
            bool drawImmediate = false
        )
        {
            if (tex == null)
            {
                return;
            }

            Renderer.DrawTexture(
                tex, srcRectangle.X, srcRectangle.Y, srcRectangle.Width, srcRectangle.Height, targetRect.X,
                targetRect.Y, targetRect.Width, targetRect.Height,
                Color.FromArgb(renderColor.A, renderColor.R, renderColor.G, renderColor.B), renderTarget, blendMode,
                shader, rotationDegrees, false, drawImmediate
            );
        }

    }

    public static partial class Graphics
    {
        public static GameFont HUDFont;
        
        public static GameFont HUDFontSmall;

        public static GameFont ToastFont;
        public static GameFont ToastFontSmall;

        public static GameFont DamageFont;

        private static GameTexture MenuTexture;
        private static readonly int MenuHFrames = 4;
        private static readonly int MenuVFrames = 2;
        private static int MenuHFrame = 0;
        private static int MenuVFrame = 0;
        private static readonly int MenuFrameRate = 10;
        private static long LastMenuFrameUpdate = 0;
        public static long IntroUpdateTime = 0;

        public static int IntroHFrame = 0;
        public static int IntroVFrame = 0;

        public static bool HideLogo = false;
        public static int LogoAlpha = 0;
        private static long LogoAlphaUpdateTime = 0;
        private static readonly long LogoAlphaUpdateInterval = 100;
        private static readonly int LogoAlphaUpdateAmount = 15;
        private static GameTexture LogoTexture;
        private static readonly int MenuBackgroundAlpha = 80;

        public static long LogoDelayTime = 0;
        private static readonly long LogoDelayInterval = 1000;

        private static void AnimateIntro()
        {
            if (TryAnimate(ref IntroUpdateTime, ref IntroHFrame, Globals.IntroHFrames, ref IntroVFrame, Globals.IntroVFrames, Globals.IntroFps, false))
            {
                if (IntroHFrame == 1 && IntroVFrame == 0 && !Globals.JinglePlayed)
                {
                    Audio.AddGameSound("Grimhaus Jingle.wav", false);
                    Globals.JinglePlayed = true;
                }
            }
        }

        public static void ResetMainMenuAnimation()
        {
            MenuHFrame = 0;
            MenuVFrame = 0;
            LastMenuFrameUpdate = Timing.Global.MillisecondsUtcUnsynced + (MenuFrameRate * 10);
        }

        public static void AnimateMainMenu()
        {
            _ = TryAnimate(ref LastMenuFrameUpdate, ref MenuHFrame, MenuHFrames, ref MenuVFrame, MenuVFrames, MenuFrameRate, true);
        }

        public static void DrawLogo()
        {
            // Draw a background to tint the animation
            if (LogoAlpha > 0)
            {
                var bgAlphaRatio = LogoAlpha / 255f;
                var currAlpha = MathHelper.Clamp(MenuBackgroundAlpha * bgAlphaRatio, 0f, MenuBackgroundAlpha);
                DrawFullScreenTexture(Renderer.GetWhiteTexture(), new Color((int)currAlpha, 0, 0, 0));
            }

            if ((LogoAlpha < 255 && Timing.Global.MillisecondsUtcUnsynced < LogoDelayTime) || HideLogo)
            {
                return;
            }

            _ = TryFadeIn(ref LogoAlphaUpdateTime, ref LogoAlpha, LogoAlphaUpdateInterval, LogoAlphaUpdateAmount);
            DrawGameTexture(LogoTexture,
                new FloatRect(0, 0, LogoTexture.GetWidth(), LogoTexture.GetHeight()),
                new FloatRect(CurrentView.CenterX - (LogoTexture.GetWidth() / 2), CurrentView.CenterY - LogoTexture.GetHeight(), LogoTexture.GetWidth(), LogoTexture.GetHeight()),
                new Color(LogoAlpha, 255, 255, 255)
                );
        }

        /// <summary>
        /// Proceeds frames for an animation and returns whether or not an advancement was made.
        /// </summary>
        /// <param name="lastUpdate">The timestamp the animation was last updated at.</param>
        /// <param name="hFrame">The current horizontal frame, by reference</param>
        /// <param name="totalH">The total Hframes of the animation sheet</param>
        /// <param name="vFrame">The current vertical frame, by reference</param>
        /// <param name="totalV">The total VFrames of the animation sheet</param>
        /// <param name="frameRate">The frame rate, in FPS, of the animation</param>
        /// <param name="loop">Whether or not the animation should loop</param>
        /// <returns></returns>
        public static bool TryAnimate(ref long lastUpdate, ref int hFrame, int totalH, ref int vFrame, int totalV, long frameRate, bool loop)
        {
            // If the animation shouldn't progress yet - either not enough time has passed or a non-looping animation has finished
            if (lastUpdate >= Timing.Global.MillisecondsUtcUnsynced || (!loop && hFrame == totalH - 1 && vFrame == totalV - 1))
            {
                return false;
            }

            bool nextHFrame = totalH - 1 > hFrame;
            bool nextVFrame = totalV - 1 > vFrame;

            if (nextHFrame)
            {
                hFrame++;
            }
            else if (nextVFrame)
            {
                hFrame = 0;
                vFrame++;
            }
            else if (loop)
            {
                // reset
                hFrame = 0;
                vFrame = 0;
            }
            
            var fpsMillis = frameRate * 10;
            lastUpdate = Timing.Global.MillisecondsUtcUnsynced + fpsMillis;
            
            return true;
        }

        public static void ResetMenu()
        {
            LogoDelayTime = Timing.Global.MillisecondsUtcUnsynced + LogoDelayInterval;
            // We need to reset the wipe, as it may have wonky values due to the nature of forcing Fades for the intro processing
            Wipe.ResetToBlack();
            ResetMainMenuAnimation();
        }

        public static void ResetLogoFade()
        {
            LogoAlpha = 0;
            LogoAlphaUpdateTime = Timing.Global.MillisecondsUtcUnsynced + LogoAlphaUpdateInterval;
        }

        public static bool TryFadeIn(ref long lastFade, ref int alpha, long fadeRate, int fadeAmount)
        {
            if (Timing.Global.MillisecondsUtcUnsynced < lastFade || alpha >= 255)
            {
                // Alpha beyond this causes overflow behavior
                if (alpha > 255)
                {
                    alpha = 255;
                }
                return false;
            }

            alpha += fadeAmount;
            alpha = MathHelper.Clamp(alpha, 0, 255);

            lastFade = Timing.Global.MillisecondsUtcUnsynced + fadeRate;
            return true;
        }

        public static void EndLogoFade()
        {
            LogoDelayTime = Timing.Global.MillisecondsUtcUnsynced;
            LogoAlpha = 255;
        }
    }
}