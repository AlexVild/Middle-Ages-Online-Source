using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Core.Controls;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;

namespace Intersect.Client.Interface.Game.Hotbar
{

    public class HotBarWindow
    {

        //Controls
        public ImagePanel HotbarWindow;

        //Item List
        public List<HotbarItem> Items = new List<HotbarItem>();

        //Init
        public HotBarWindow(Canvas gameCanvas)
        {
            HotbarWindow = new ImagePanel(gameCanvas, "HotbarWindow");
            HotbarWindow.ShouldCacheToTexture = true;
            InitHotbarItems();
            HotbarWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].EquipPanel.Texture == null)
                {
                    Items[i].EquipPanel.Texture = Graphics.Renderer.GetWhiteTexture();
                }
            }
        }

        private void InitHotbarItems()
        {
            var x = 12;
            for (var i = 0; i < Options.MaxHotbar; i++)
            {
                Items.Add(new HotbarItem((byte) i, HotbarWindow));
                Items[i].Pnl = new ImagePanel(HotbarWindow, "HotbarContainer" + i);
                Items[i].Setup();
            }
        }

        public void Update()
        {
            if (Globals.Me.InCutscene())
            {
                HotbarWindow.Hide();
            }
            else
            {
                HotbarWindow.Show();
            }

            if (Globals.Me == null)
            {
                return;
            }

            for (var i = 0; i < Options.MaxHotbar; i++)
            {
                Items[i].Update();
                DrawHotKey(i, Items[i]);
            }
        }

        private void DrawHotKey(int keyIdx, HotbarItem item)
        {
            if (Globals.Me == null || Globals.Me.InCutscene())
            {
                return;
            }

            var font = Graphics.HUDFontSmall;
            var key = Strings.Keys.keydict[
                Enum.GetName(
                    typeof(Keys), Controls.ActiveControls.ControlMapping[Control.Hotkey1 + keyIdx].Key1
                )
                .ToLower()].ToString().ToUpper();

            if (key.Length >= 3)
            {
                key = $"{key.Substring(0, 3)}.";
            }

            var hotbarBase = item.GetWindow();

            var x = hotbarBase.X;
            var y = hotbarBase.Bottom + 3;

            x += item.Pnl.X + (item.Pnl.Width / 2);

            var controlLength = Graphics.Renderer.MeasureText(key, font, 1).X;
            x = (int)(x - (controlLength / 2)); // center under hotkey item

            var hud = Interface.GameUi.GetHud();

            Graphics.Renderer.DrawString(
                key, font, x + Graphics.CurrentView.X, y + Graphics.CurrentView.Y, 1, hud.Secondary
            );
        }

        public FloatRect RenderBounds()
        {
            var rect = new FloatRect()
            {
                X = HotbarWindow.LocalPosToCanvas(new Point(0, 0)).X,
                Y = HotbarWindow.LocalPosToCanvas(new Point(0, 0)).Y,
                Width = HotbarWindow.Width,
                Height = HotbarWindow.Height
            };

            return rect;
        }

    }

}
