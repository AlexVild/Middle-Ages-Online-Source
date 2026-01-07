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

        // Editor shortcut chord state
        private bool ShortcutArmed => mEditorShortcutArmed;
        private bool mEditorShortcutArmed;
        private DateTime mEditorShortcutArmedAt;

        // How long we wait for the second key
        private static readonly TimeSpan EditorShortcutTimeout = TimeSpan.FromSeconds(1);

        private void HandleHotkeys(KeyEventArgs e)
        {
            HandleToolstripShortcuts(e);
            HandleLayerShortcuts(e);
            HandleAttributeShortcuts(e);
            HandleEditorOpenShortcuts(e);
            HandleToolShortcut(e);
        }

        private void HandleToolShortcut(KeyEventArgs e)
        {
            if (!EditingMap || ShortcutArmed)
            {
                return;
            }

            // We're on the tiles tab
            if (!TileLayers.Names.Contains(Globals.CurrentLayer))
            {
                return;
            }

            switch(e.KeyCode)
            {
                case Keys.F:
                    Globals.CurrentTool = (int)EditingTool.Fill;
                    e.Handled = true;
                    return;
                case Keys.B:
                    Globals.CurrentTool = (int)EditingTool.Pen;
                    e.Handled = true;
                    return;
                case Keys.R:
                    Globals.CurrentTool = (int)EditingTool.Rectangle;
                    e.Handled = true;
                    return;
                case Keys.I:
                    Globals.CurrentTool = (int)EditingTool.Droppler;
                    e.Handled = true;
                    return;
                case Keys.E:
                    Globals.CurrentTool = (int)EditingTool.Erase;
                    e.Handled = true;
                    return;
                case Keys.M:
                    Globals.CurrentTool = (int)EditingTool.Selection;
                    e.Handled = true;
                    return;
            }
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
            if (!e.Control || ShortcutArmed)
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
            if (!EditingMap || ShortcutArmed)
            {
                return;
            }

            var newLayer = Globals.CurrentLayer;

            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D2:
                        e.Handled = true;
                        Globals.MapLayersWindow.HotkeyToggleAttributes();
                        return;
                    case Keys.D3:
                        e.Handled = true;
                        Globals.MapLayersWindow.HotkeyToggleLights();
                        return;
                    case Keys.D4:
                        e.Handled = true;
                        Globals.MapLayersWindow.HotkeyToggleEvents();
                        return;
                    case Keys.D5:
                        e.Handled = true;
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

            e.Handled = true;
        }

        private void HandleAttributeShortcuts(KeyEventArgs e)
        {
            if (!EditingMap || Globals.CurrentLayer != LayerOptions.Attributes || ShortcutArmed)
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
            
            e.Handled = true;
        }

        private void HandleEditorOpenShortcuts(KeyEventArgs e)
        {
            // Handle quick-shortcuts
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.I:
                        TryOpenEditorMethod(GameObjectType.Item);
                        return;
                    case Keys.N:
                        TryOpenEditorMethod(GameObjectType.Npc);
                        return;
                    case Keys.C:
                        if (e.Shift)
                            TryOpenEditorMethod(GameObjectType.Event);
                        else if (e.Alt)
                            HotkeyOpenMapPropCopier();
                        else
                            TryOpenEditorMethod(GameObjectType.Crafts);
                        return;
                    case Keys.T:
                        TryOpenEditorMethod(GameObjectType.CraftTables);
                        return;
                    case Keys.M:
                        TryOpenEditorMethod(GameObjectType.Spell);
                        return;
                    case Keys.P:
                        TryOpenEditorMethod(GameObjectType.Projectile);
                        return;
                    case Keys.V:
                        TryOpenEditorMethod(GameObjectType.PlayerVariable);
                        return;
                }
            }

            // Step 1: Ctrl+E arms editor mode
            if (e.Control && !e.Shift && e.KeyCode == Keys.E)
            {
                toolstripLabelShortcutMode.Text = "Editor mode enabled, listening for key...";
                mEditorShortcutArmed = true;
                mEditorShortcutArmedAt = DateTime.UtcNow;
                e.Handled = true;
                return;
            }

            if (mEditorShortcutArmed && e.KeyCode == Keys.Escape)
            {
                mEditorShortcutArmed = false;
                e.Handled = true;
            }

            // Step 2: If not armed, do nothing
            if (!mEditorShortcutArmed)
            {
                return;
            }

            // Step 3: Timeout
            if (DateTime.UtcNow - mEditorShortcutArmedAt > EditorShortcutTimeout)
            {
                mEditorShortcutArmed = false;
                return;
            }

            // Step 4: Consume next key
            mEditorShortcutArmed = false;
            e.Handled = true;
            HandleOpenEditorShortcutKey(e.KeyCode, e.Shift, e.Control);
        }

        private void EditorModeShowHide(bool visible)
        {
            toolstripLabelShortcutMode.Visible = visible;
            toolstripSeparatorEditorMode.Visible = visible;
        }

        private void HandleOpenEditorShortcutKey(Keys key, bool shift, bool ctrl)
        {
            switch (key)
            {
                case Keys.A:
                    TryOpenEditorMethod(GameObjectType.Animation);
                    break;
                case Keys.I:
                    TryOpenEditorMethod(GameObjectType.Item);
                    break;
                case Keys.N:
                    TryOpenEditorMethod(GameObjectType.Npc);
                    break;
                case Keys.R:
                    TryOpenEditorMethod(GameObjectType.Resource);
                    break;
                case Keys.S:
                    TryOpenEditorMethod(GameObjectType.Spell);
                    break;
                case Keys.L:
                    TryOpenEditorMethod(GameObjectType.Class);
                    break;
                case Keys.B:
                    TryOpenEditorMethod(GameObjectType.Shop);
                    break;
                case Keys.V:
                    TryOpenEditorMethod(GameObjectType.PlayerVariable);
                    break;
                case Keys.E:
                    TryOpenEditorMethod(GameObjectType.Event);
                    break;
                case Keys.Q:
                    if (shift)
                        TryOpenEditorMethod(GameObjectType.QuestBoard);
                    else if (ctrl)
                        TryOpenEditorMethod(GameObjectType.QuestList);
                    else
                        TryOpenEditorMethod(GameObjectType.Quest);
                    break;
                case Keys.C:
                    if (shift)
                        TryOpenEditorMethod(GameObjectType.CraftTables);
                    else if (ctrl)
                        TryOpenEditorMethod(GameObjectType.Recipe);
                    else
                        TryOpenEditorMethod(GameObjectType.Crafts);
                    break;
                case Keys.H:
                    TryOpenEditorMethod(GameObjectType.Enhancement);
                    break;
                case Keys.D:
                    TryOpenEditorMethod(GameObjectType.Dungeon);
                    break;
                case Keys.W:
                    TryOpenEditorMethod(GameObjectType.WeaponType);
                    break;
                case Keys.J:
                    TryOpenEditorMethod(GameObjectType.Projectile);
                    break;
                case Keys.Y:
                    TryOpenEditorMethod(GameObjectType.Territory);
                    break;
                case Keys.O:
                    TryOpenEditorMethod(GameObjectType.LootTable);
                    break;
                case Keys.K:
                    TryOpenEditorMethod(GameObjectType.Label);
                    break;
                case Keys.T:
                    if (shift)
                        TryOpenEditorMethod(GameObjectType.Time);
                    else
                        TryOpenEditorMethod(GameObjectType.Timer);
                    break;
            }
        }

        private void HandleMapPanning()
        {
            if (!EditingMap || ShortcutArmed)
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
