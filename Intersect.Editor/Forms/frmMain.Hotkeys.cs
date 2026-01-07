using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

using DarkUI.Controls;
using DarkUI.Forms;
using Intersect.Compression;
using Intersect.Config;
using Intersect.Editor.Classes.ContentManagement;
using Intersect.Editor.Content;
using Intersect.Editor.Forms.DockingElements;
using Intersect.Editor.Forms.Editors;
using Intersect.Editor.Forms.Editors.Quest;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Maps;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Localization;
using Intersect.Network;
using Intersect.Updater;
using Intersect.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using WeifenLuo.WinFormsUI.Docking;

namespace Intersect.Editor.Forms
{
    public partial class FrmMain : Form
    {
        private bool EditingMap => MapEditorFocused() && !ComboboxFocused();

        private void HandleHotkeys(KeyEventArgs e)
        {
            HandleToolstripShortcuts(e);
            HandleLayerShortcuts(e);
            HandleAttributeShortcuts(e);
        }

        private void HandleHotkeyRemoval(KeyEventArgs e)
        {
            var blockAttrSelected = Globals.CurrentLayer == LayerOptions.Attributes && Globals.MapLayersWindow.BlockSelected();
            
            if (blockAttrSelected && !e.Shift)
            {
                Core.Graphics.TilePreviewUpdated = true;
            }
        }

        private void HandleToolstripShortcuts(KeyEventArgs e)
        {
            if (!e.Control)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Z:
                    toolStripBtnUndo_Click(null, null);
                    return;
                case Keys.Y:
                    toolStripBtnRedo_Click(null, null);
                    return;
                case Keys.X:
                    toolStripBtnCut_Click(null, null);
                    return;
                case Keys.C:
                    toolStripBtnCopy_Click(null, null);
                    return;
                case Keys.V:
                    toolStripBtnPaste_Click(null, null);
                    return;
                case Keys.S:
                    toolStripBtnSaveMap_Click(null, null);
                    return;
            }
        }

        private void HandleLayerShortcuts(KeyEventArgs e)
        {
            if (!EditingMap)
            {
                return;
            }

            var newLayer = Globals.CurrentLayer;

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D2:
                        Globals.MapLayersWindow.HotkeyToggleAttributes();
                        return;
                    case Keys.D3:
                        Globals.MapLayersWindow.HotkeyToggleLights();
                        return;
                    case Keys.D4:
                        Globals.MapLayersWindow.HotkeyToggleEvents();
                        return;
                    case Keys.D5:
                        Globals.MapLayersWindow.HotkeyToggleNpcs();
                        return;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        newLayer = TileLayers.MAP_LAYER_GROUND;
                        break;
                    case Keys.D2:
                        newLayer = TileLayers.MAP_LAYER_MASK_1;
                        break;
                    case Keys.D3:
                        newLayer = TileLayers.MAP_LAYER_MASK_2;
                        break;
                    case Keys.D4:
                        newLayer = TileLayers.MAP_LAYER_FRINGE_1;
                        break;
                    case Keys.D5:
                        newLayer = TileLayers.MAP_LAYER_FRINGE_2;
                        break;
                }
            }

            if (newLayer != Globals.CurrentLayer)
            {
                Globals.MapLayersWindow.HotkeyToggleTiles(newLayer);
                Globals.CurrentLayer = newLayer;
            }
        }

        private void HandleAttributeShortcuts(KeyEventArgs e)
        {
            if (!EditingMap || Globals.CurrentLayer != LayerOptions.Attributes)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.B:
                    Globals.MapLayersWindow.HotkeyBlockToggle();
                    break;
                case Keys.N:
                    Globals.MapLayersWindow.HotkeyNpcAvoidToggle();
                    break;
                case Keys.R:
                    Globals.MapLayersWindow.HotkeyResourceToggle();
                    break;
            }

            if (Globals.MapLayersWindow.BlockSelected() && e.Shift)
            {
                Core.Graphics.TilePreviewUpdated = true;
            }
        }

        private void HandleMapPanning()
        {
            if (!EditingMap)
            {
                return;
            }

            int moveAmt = Math.Max(Options.TileWidth, Options.TileHeight) / 2;
            if (KeysDown.Contains(Keys.ShiftKey))
            {
                moveAmt *= 2;
            }
            int xDiff = 0;
            int yDiff = 0;

            if (KeysDown.Contains(Keys.W) || KeysDown.Contains(Keys.Up))
                yDiff -= moveAmt;

            if (KeysDown.Contains(Keys.S) || KeysDown.Contains(Keys.Down))
                yDiff += moveAmt;

            if (KeysDown.Contains(Keys.A) || KeysDown.Contains(Keys.Left))
                xDiff -= moveAmt;

            if (KeysDown.Contains(Keys.D) || KeysDown.Contains(Keys.Right))
                xDiff += moveAmt;

            if (xDiff == 0 && yDiff == 0)
                return;

            var view = Core.Graphics.CurrentView;

            view.X = MathHelper.Clamp(
                view.X - xDiff,
                -Options.TileWidth * Options.MapWidth * 2 + Globals.MapEditorWindow.picMap.Width,
                Options.MapWidth * Options.TileWidth
            );

            view.Y = MathHelper.Clamp(
                view.Y - yDiff,
                -Options.TileHeight * Options.MapHeight * 2 + Globals.MapEditorWindow.picMap.Height,
                Options.MapHeight * Options.TileHeight
            );

            Core.Graphics.CurrentView = view;
        }

        private bool MapEditorFocused()
        {
            if (dockLeft.ActiveContent != Globals.MapEditorWindow &&
                !(dockLeft.ActiveContent == null &&
                  Globals.MapEditorWindow.DockPanel.ActiveDocument == Globals.MapEditorWindow))
                return false;

            return true;
        }

        private bool ComboboxFocused()
        {
            var hWnd = WindowFromPoint(MousePosition);
            if (hWnd != IntPtr.Zero)
            {
                var ctl = Control.FromHandle(hWnd);
                if (ctl is ComboBox || ctl is DarkComboBox)
                    return true;
            }

            return false;
        }
    }
}
