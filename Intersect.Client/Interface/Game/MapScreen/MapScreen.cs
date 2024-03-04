using Intersect.Client.Core;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Client.Utilities;
using Intersect.Utilities;
using System;
using System.Linq;

namespace Intersect.Client.Interface.Game.MapScreen
{
    public class MapScreen : IDisposable
    {
        public const int MapScreenGridWidth = 128;
        public const int MapScreenGridHeight = 104;
        private const int DefaultZoomIdx = 1;
        
        private long LastUpdate;

        private float FastSpeed = 0.8f;
        private float SlowSpeed = 0.35f;
        private float CurrentSpeed => Input.QuickModifierActive() ? FastSpeed : SlowSpeed;

        private int LastMouseScrollPos;

        private float Left => Graphics.CurrentView.Left;
        private float Width => Graphics.CurrentView.Width;
        private float Top => Graphics.CurrentView.Top;
        private float Height => Graphics.CurrentView.Height;
        private float CenterX => Graphics.CurrentView.CenterX;
        private float CenterY => Graphics.CurrentView.CenterY;
        private Color MapNameColor = new Color(50, 19, 0);

        private int MapWidth = 544;
        private int MapHeight = 416;

        private float CurrentX = 0;
        private float CurrentY = 0;
        private float MinX;
        private float MinY;
        private float MaxX;
        private float MaxY;
        private Pointf PanStartPos;
        private bool IsPanning;
        private bool IsZooming;
        private int CurrentZoom => ZoomLevels.ElementAtOrDefault(ZoomIdx) == default ? 1 : ZoomLevels[ZoomIdx];

        private int ZoomIdx = 0;
        private int[] ZoomLevels = { 1, 2, 4, 8 };

        private GameTexture MapTexture;
        private GameTexture BattlelandsTexture;
        private GameTexture CurrentMapTexture => Globals.Me?.MapType == GameObjects.Events.MapType.Battlelands ? BattlelandsTexture : MapTexture;
        private GameTexture BackgroundTexture;
        private GameTexture ForegroundTexture;
        private GameTexture LabelLeftTexture;
        private GameTexture LabelRightTexture;
        private GameTexture LabelMidTexture;
        private GameTexture FogTexture;
        private GameTexture LocationTexture;
        private GameTexture MeTexture;

        private (int, int) CurrGrid => (Globals.Me.MapInstance?.MapGridX ?? 0, Globals.Me.MapInstance?.MapGridY ?? 0);

        private GameRenderTexture MapRenderTexture;
        private GameRenderTexture BattlelandsRenderTexture;
        private GameRenderTexture CurrentRenderTexture => Globals.Me?.MapType == GameObjects.Events.MapType.Battlelands ? BattlelandsRenderTexture : MapRenderTexture;

        private Color BackgroundColor = Color.White;

        private string CurrentMap;

        public bool IsOpen;

        public bool NeedsGenerating = true;

        public MapScreen()
        {
            MapTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map.png");
            BattlelandsTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map_battlelands.png");
            BackgroundTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map_background.png");
            ForegroundTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map_foreground.png");
            MapRenderTexture = Graphics.Renderer.CreateRenderTexture(MapTexture.GetWidth(), MapTexture.GetHeight());
            BattlelandsRenderTexture = Graphics.Renderer.CreateRenderTexture(BattlelandsTexture.GetWidth(), BattlelandsTexture.GetHeight());
            LabelLeftTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "label_left.png");
            LabelRightTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "label_right.png");
            LabelMidTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "label_middle.png");
            FogTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map_fog.png");
            LocationTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map_current.png");
            MeTexture = Globals.ContentManager.GetTexture(Framework.File_Management.GameContentManager.TextureType.Gui, "map_me.png");

            LastMouseScrollPos = Globals.InputManager.GetMouseVScroll();
        }

        public void RegenerateMap()
        {
            NeedsGenerating = true;
        }

        public void ToggleOpen()
        {
            if (Globals.Me == null)
            {
                Dispose();
                return;
            }

            IsOpen = !IsOpen;
            if (IsOpen)
            {
                LastMouseScrollPos = Globals.InputManager.GetMouseVScroll();
                
                Audio.AddGameSound("al_book_turn.wav", false);
                GenerateMap();
                
                ZoomIdx = ZoomLevels.Length > DefaultZoomIdx ? DefaultZoomIdx : 0;
                ResetToCenterOfGrid();
            }
            else
            {
                Close();
            }
        }

        public void Close()
        {
            IsOpen = false;
            Audio.AddGameSound("al_book_drop.wav", false);
            NeedsGenerating = true;
        }

        private void ResetToCenterOfGrid()
        {
            var midPointX = MapWidth / 2 / CurrentZoom;
            var midPointY = MapHeight / 2 / CurrentZoom;
            var midMapX = MapScreenGridWidth / 2;
            var midMapY = MapScreenGridHeight / 2;

            CurrentX = (CurrGrid.Item1 * MapScreenGridWidth) - midPointX + midMapX;
            CurrentY = (CurrGrid.Item2 * MapScreenGridHeight) - midPointY + midMapY;

            ClampPanPosition();
        }

        public void Update()
        {
            if (!IsOpen || Globals.Me == null)
            {
                LastUpdate = Timing.Global.Milliseconds;
                return;
            }

            if (Globals.Me.InCutscene())
            {
                LastUpdate = Timing.Global.Milliseconds;
                Close();
            }

            Draw();
            HandleInput();
            LastUpdate = Timing.Global.Milliseconds;
        }

        private void HandleInput()
        {
            var mousePos = UiHelper.GetViewMousePos();
            ProcessPan(mousePos);
            ProcessZoom(mousePos);
            if (Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Middle))
            {
                ZoomIdx = ZoomLevels.Length > DefaultZoomIdx ? DefaultZoomIdx : 0;
                ResetToCenterOfGrid();
            }
            HandleDirectionalInput();
            CurrentMap = ProcessLocation(mousePos);
        }

        private void HandleDirectionalInput()
        {
            if (Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Left)) 
            {
                return;
            }

            var now = Timing.Global.Milliseconds;
            var time_delta = (now - LastUpdate);
            var move_delta = CurrentSpeed / CurrentZoom;
            var amt = time_delta * move_delta;
            if (Controls.KeyDown(Control.MoveLeft))
            {
                CurrentX -= amt;
            }
            if (Controls.KeyDown(Control.MoveRight))
            {
                CurrentX += amt;
            }
            if (Controls.KeyDown(Control.MoveDown))
            {
                CurrentY += amt;
            }
            if (Controls.KeyDown(Control.MoveUp))
            {
                CurrentY -= amt;
            }
            ClampPanPosition();
        }

        private bool MouseIsOverMap(Pointf mousePos)
        {
            if (mousePos.X < CenterX - (MapWidth / 2) || mousePos.X > CenterX + (MapWidth / 2))
            {
                return false;
            }
            if (mousePos.Y < CenterY - (MapHeight / 2) || mousePos.Y > CenterY + (MapHeight / 2))
            {
                return false;
            }

            return true;
        }

        private void ProcessPan(Pointf mousePos)
        {
            if (!Globals.InputManager.MouseButtonDown(Framework.Input.GameInput.MouseButtons.Left))
            {
                IsPanning = false;
                return;
            }

            if (!IsPanning && MouseIsOverMap(mousePos))
            {
                PanStartPos = new Pointf(mousePos.X, mousePos.Y);
                IsPanning = true;
            }
            else if (!IsPanning)
            {
                return;
            }

            var xDiff = (mousePos.X - PanStartPos.X) / CurrentZoom;
            var yDiff = (mousePos.Y - PanStartPos.Y) / CurrentZoom;

            CurrentX -= xDiff;
            CurrentY -= yDiff;

            ClampPanPosition();

            PanStartPos = mousePos;
        }

        private void ClampPanPosition()
        {
            // +- 2 because we want to still be able to center a barely-filled map
            var minX = Intersect.Utilities.MathHelper.Clamp(MinX - 1, 0, int.MaxValue);
            var maxX = Intersect.Utilities.MathHelper.Clamp(MaxX + 2, 0, Globals.MapGrid.GetLength(0));
            var minY = Intersect.Utilities.MathHelper.Clamp(MinY - 1, 0, int.MaxValue);
            var maxY = Intersect.Utilities.MathHelper.Clamp(MaxY + 2, 0, Globals.MapGrid.GetLength(1));

            minX = minX * MapScreenGridWidth;
            maxX = maxX * MapScreenGridWidth - (MapWidth / CurrentZoom);
            minY = minY * MapScreenGridHeight;
            maxY = maxY * MapScreenGridHeight - (MapHeight / CurrentZoom);
            CurrentX = (float)Intersect.Utilities.MathHelper.Clamp(CurrentX, minX, maxX);
            CurrentY = (float)Intersect.Utilities.MathHelper.Clamp(CurrentY, minY, maxY);
        }

        private void ProcessZoom(Pointf mousePos)
        {
            if (!MouseIsOverMap(mousePos)) 
            {
                return;
            }

            if (!Globals.InputManager.KeyDown(Keys.Add) && !Globals.InputManager.KeyDown(Keys.Subtract) && Globals.InputManager.GetMouseVScroll() == LastMouseScrollPos)
            {
                IsZooming = false;
                
                return;
            }

            if (!IsZooming)
            {
                bool zoomIn;
                if (Globals.InputManager.GetMouseVScroll() != LastMouseScrollPos)
                {
                    IsZooming = false;
                    var tmpScroll = Globals.InputManager.GetMouseVScroll();
                    zoomIn = tmpScroll > LastMouseScrollPos;
                    LastMouseScrollPos = Globals.InputManager.GetMouseVScroll();
                }
                else
                {
                    zoomIn = Globals.InputManager.KeyDown(Keys.Add);
                }

                var prevZoom = CurrentZoom;
                
                ZoomIdx = zoomIn ? ZoomIdx + 1 : ZoomIdx - 1;
                ZoomIdx = Intersect.Utilities.MathHelper.Clamp(ZoomIdx, 0, ZoomLevels.Length - 1);

                if (prevZoom == CurrentZoom)
                {
                    return;
                }

                var mousePosOnTexture = new Pointf();
                mousePosOnTexture.X = (mousePos.X - Left - Width / 2 + MapWidth / 2);
                mousePosOnTexture.Y = (mousePos.Y - Top - Height / 2 + MapHeight / 2);
                mousePosOnTexture.X += (CurrentX);
                mousePosOnTexture.Y += (CurrentY);
                var originDistanceX = mousePosOnTexture.X - CurrentX;
                var originDistanceY = mousePosOnTexture.Y - CurrentY;

                if (zoomIn)
                {
                    Audio.AddGameSound("ui_click.wav", false);
                    CurrentX += originDistanceX / CurrentZoom;
                    CurrentY += originDistanceY / CurrentZoom;
                }
                else
                {
                    Audio.AddGameSound("ui_release.wav", false);
                    CurrentX -= originDistanceX / prevZoom;
                    CurrentY -= originDistanceY / prevZoom;
                }
                
                ClampPanPosition();
                IsZooming = true;
            }
        }

        private Pointf GetMapRelativeMousePos(Pointf mousePos)
        {
            var newPos = new Pointf();
            newPos.X = (mousePos.X - Left - Width / 2 + MapWidth / 2);
            newPos.Y = (mousePos.Y - Top - Height / 2 + MapHeight / 2);
            
            newPos.X += (CurrentX * CurrentZoom);
            newPos.Y += (CurrentY * CurrentZoom);
            
            newPos.X = (float)Math.Floor(newPos.X / (MapScreenGridWidth * CurrentZoom));
            newPos.Y = (float)Math.Floor(newPos.Y / (MapScreenGridHeight * CurrentZoom));
            
            newPos.X = (float)Intersect.Utilities.MathHelper.Clamp(newPos.X, 0, Globals.MapGridWidth - 1);
            newPos.Y = (float)Intersect.Utilities.MathHelper.Clamp(newPos.Y, 0, Globals.MapGridHeight - 1);
           
            return newPos;
        }

        private string ProcessLocation(Pointf mousePos)
        {
            if (!MouseIsOverMap(mousePos))
            {
                return string.Empty;
            }

            var relMouse = GetMapRelativeMousePos(mousePos);

            if (!Globals.GridNames.TryGetValue(Globals.MapGrid[(int)relMouse.X, (int)relMouse.Y], out var mapName))
            {
                return string.Empty;
            }
            if (!Globals.Me.MapsExplored.Contains(Globals.MapGrid[(int)relMouse.X, (int)relMouse.Y]))
            {
                return string.Empty;
            }
            return mapName;
        }

        private void Draw()
        {
            var bgWidth = BackgroundTexture.GetWidth();
            var bgHeight = BackgroundTexture.GetHeight();

            var bgDest = new FloatRect(CenterX - bgWidth / 2, CenterY - bgHeight / 2, bgWidth, bgHeight);
            var mapDest = new FloatRect(CenterX - MapWidth / 2, CenterY - MapHeight / 2, MapWidth, MapHeight);

            // Render background elements
            Graphics.DrawFullScreenTexture(Graphics.Renderer.GetWhiteTexture(), new Color(100, 0, 0, 0));
            Graphics.DrawGameTexture(BackgroundTexture, new FloatRect(0, 0, bgWidth, bgHeight), bgDest, BackgroundColor);

            if (Globals.Me.MapType != GameObjects.Events.MapType.Overworld && Globals.Me.MapType != GameObjects.Events.MapType.Battlelands)
            {
                var errorMsg = "You're not on an overworld!";
                var errorMsgLen = Graphics.Renderer.MeasureText(errorMsg, Graphics.HUDFont, 1);
                var midX = bgDest.CenterX - errorMsgLen.X / 2;
                var midY = bgDest.CenterY - errorMsgLen.Y / 2;

                Graphics.Renderer.DrawString("You're not on an overworld!", Graphics.HUDFont, midX, midY, 1, MapNameColor);
                return;
            }

            // Render the map to MapRenderTexture, and draw the map in its revealed state
            Graphics.DrawGameTexture(CurrentRenderTexture, new FloatRect(CurrentX, CurrentY, mapDest.Width / CurrentZoom, mapDest.Height / CurrentZoom), mapDest, Color.White);

            // Render foreground elements
            Graphics.DrawGameTexture(ForegroundTexture, new FloatRect(0, 0, bgWidth, bgHeight), bgDest, BackgroundColor);

            if (string.IsNullOrEmpty(CurrentMap))
            {
                return;
            }

            DrawMapName();
        }

        private void DrawMapName()
        {
            var mousePos = UiHelper.GetViewMousePos();
            var nameWidth = Graphics.Renderer.MeasureText(CurrentMap, Graphics.HUDFont, 1);

            var x = mousePos.X - (nameWidth.X / 2);
            var y = mousePos.Y + nameWidth.Y * 2;

            Graphics.DrawGameTexture(LabelLeftTexture,
                new FloatRect(0, 0, LabelLeftTexture.GetWidth(), LabelLeftTexture.GetHeight()),
                new FloatRect(x, y, LabelLeftTexture.GetWidth(), LabelLeftTexture.GetHeight()), Color.White);
            Graphics.DrawGameTexture(LabelMidTexture,
                new FloatRect(0, 0, LabelMidTexture.GetWidth(), LabelMidTexture.GetHeight()),
                new FloatRect(x + LabelLeftTexture.GetWidth(), y, nameWidth.X, LabelMidTexture.GetHeight()), Color.White);
            Graphics.DrawGameTexture(LabelRightTexture,
                new FloatRect(0, 0, LabelRightTexture.GetWidth(), LabelRightTexture.GetHeight()),
                new FloatRect(x + LabelLeftTexture.GetWidth() + nameWidth.X, y, LabelRightTexture.GetWidth(), LabelRightTexture.GetHeight()), Color.White);
            Graphics.Renderer.DrawString(CurrentMap.Trim().ToUpper(), Graphics.HUDFont, x + LabelLeftTexture.GetWidth(), y + 8, 1, MapNameColor); ;
        }

        private void GenerateMap()
        {
            if (!NeedsGenerating)
            {
                return;
            }

            if (Globals.Me == null || Globals.Me.MapInstance == null)
            {
                return;
            }

            MapRenderTexture.Clear(Color.Black);
            BattlelandsRenderTexture.Clear(Color.Black);
            
            var fogBorderWidth = 8;
            var fogSrc = new FloatRect(0, 0, FogTexture.GetWidth(), FogTexture.GetHeight());
            var locSrc = new FloatRect(0, 0, LocationTexture.GetWidth(), LocationTexture.GetHeight());
            var meSrc = new FloatRect(0, 0, MeTexture.GetWidth(), MeTexture.GetHeight());

            var mapRect = new FloatRect(0, 0, CurrentMapTexture.GetWidth(), CurrentMapTexture.GetHeight());

            Graphics.DrawGameTexture(CurrentMapTexture, mapRect, mapRect, Color.White, renderTarget: CurrentRenderTexture);

            var minY = int.MaxValue;
            var minX = int.MaxValue;
            var maxY = 0;
            var maxX = 0;
            for (var y = 0; y < Globals.MapGrid.GetLength(1); y++)
            {
                var fogY = y * MapScreenGridHeight - fogBorderWidth;
                for (var x = 0; x < Globals.MapGrid.GetLength(0); x++)
                {
                    var fogX = x * MapScreenGridWidth - fogBorderWidth;
                    // Have we been here? If so, draw it
                    if (Globals.Me.MapsExplored.Contains(Globals.MapGrid[x, y]))
                    {
                        if (x == CurrGrid.Item1 && y == CurrGrid.Item2)
                        {
                            // Draw border around our current map
                            var locX = fogX + fogBorderWidth;
                            var locY = fogY + fogBorderWidth;
                            Graphics.DrawGameTexture(LocationTexture,
                                locSrc,
                                new FloatRect(locX, locY, LocationTexture.GetWidth(), LocationTexture.GetHeight()),
                                Color.White,
                                renderTarget: CurrentRenderTexture);

                            // Draw player's location within that map
                            var playerX = locX + (Globals.Me.X * Options.TileWidth / 16);
                            var playerY = locY + (Globals.Me.Y * Options.TileHeight / 16);

                            Graphics.DrawGameTexture(MeTexture,
                                meSrc,
                                new FloatRect(playerX, playerY, MeTexture.GetWidth(), MeTexture.GetHeight()),
                                Color.White,
                                renderTarget: CurrentRenderTexture);
                        }

                        // Determine the new min/max values of explored map, if they've been updated
                        minX = Math.Min(minX, x);
                        maxX = Math.Max(maxX, x);
                        minY = Math.Min(minY, y);
                        maxY = Math.Max(maxY, y);

                        continue;
                    }

                    // Otherwise, fog it
                    Graphics.DrawGameTexture(FogTexture,
                        fogSrc,
                        new FloatRect(fogX, fogY, FogTexture.GetWidth(), FogTexture.GetHeight()),
                        Color.White,
                        renderTarget: CurrentRenderTexture);
                }
            }

            // Set the bounds of the explored area of the map
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;

            NeedsGenerating = false;
        }

        public void Dispose()
        {
            MapRenderTexture.Dispose();
            BattlelandsRenderTexture.Dispose();
        }
    }
}
