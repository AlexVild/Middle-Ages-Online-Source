using DarkUI.Controls;
using DarkUI.Forms;
using Intersect.Collections;
using Intersect.Editor.Forms.Controls;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Helpers
{
    public static class FormHelpers
    {
        public static void UpdateToolstripItems<T>(ref ToolStripButton copy, ref ToolStripButton paste, ref ToolStripButton undo, ref ToolStripButton delete, string copiedItem, T mEditorItem, GameObjectList lstGameObjects)
        {
            copy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            paste.Enabled = mEditorItem != null && copiedItem != null && lstGameObjects.Focused;
            delete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            undo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        public static bool CustomSearch(DarkTextBox txtSearch)
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.EditorGenerics.SearchPlaceHolder;
        }

        public static void GameObjectUpdatedDelegate<T>(ref T mEditorItem, Action initEditor, Action updateEditor) where T : DatabaseObject<T>
        {
            initEditor();
            if (mEditorItem != null && !DatabaseObject<T>.Lookup.Values.Contains(mEditorItem))
            {
                mEditorItem = null;
                updateEditor();
            }
        }

        public static void InitFoldersAndObjectList<T>(ref List<string> knownFolders, 
            ref DarkComboBox cmbFolder, 
            ref GameObjectList lstGameObjects,
            DarkTextBox txtSearch,
            DatabaseObjectLookup lookup,
            ToolStripButton btnAlphabetical) where T : DatabaseObject<T>, IFolderable
        {
            var mFolders = new List<string>();
            foreach (var itm in lookup)
            {
                if (!string.IsNullOrEmpty(((T)itm.Value).Folder) &&
                    !mFolders.Contains(((T)itm.Value).Folder))
                {
                    mFolders.Add(((T)itm.Value).Folder);
                    if (!knownFolders.Contains(((T)itm.Value).Folder))
                    {
                        knownFolders.Add(((T)itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            knownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(knownFolders.ToArray());

            var items = lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((T)pair.Value)?.Name ?? DatabaseObject<T>.Deleted, ((T)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(txtSearch), txtSearch.Text);
        }

        public static void UpdateEditor<T>(ref T editorItem, ref List<T> changedItems, ref DarkGroupBox propertiesGroup, Action updateToolStripItems, Action populateFields) where T : DatabaseObject<T>
        {
            if (editorItem != null)
            {
                propertiesGroup.Show();

                populateFields();

                if (changedItems.IndexOf(editorItem) == -1)
                {
                    changedItems.Add(editorItem);
                    editorItem.MakeBackup();
                }
            }
            else
            {
                propertiesGroup.Hide();
            }

            updateToolStripItems();
        }

        public static void UpdateEditor<T>(ref T editorItem, ref List<T> changedItems, ref Panel propertiesPanel, Action updateToolStripItems, Action populateFields) where T : DatabaseObject<T>
        {
            if (editorItem != null)
            {
                propertiesPanel.Show();

                populateFields();

                if (changedItems.IndexOf(editorItem) == -1)
                {
                    changedItems.Add(editorItem);
                    editorItem.MakeBackup();
                }
            }
            else
            {
                propertiesPanel.Hide();
            }

            updateToolStripItems();
        }

        public static void ToolStripNewClicked(GameObjectType objType)
        {
            PacketSender.SendCreateObject(objType);
        }

        public static void ToolStripDeleteClicked<T>(T editorItem, GameObjectList lstGameObjects) where T : DatabaseObject<T>
        {
            if (editorItem != null && lstGameObjects.Focused)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.QuestBoardEditor.deleteprompt, Strings.QuestBoardEditor.delete,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    PacketSender.SendDeleteObject(editorItem);
                }
            }
        }

        public static void AlphabeticalClicked(ref ToolStripButton btnAlphabetical, Action initEditor)
        {
            btnAlphabetical.Checked = !btnAlphabetical.Checked;
            initEditor();
        }

        public static void SearchTextChanged(Action initEditor)
        {
            initEditor();
        }

        public static void ToolStripUndoClicked<T>(List<T> changedItems, ref T editorItem, Action updateEditor) where T : DatabaseObject<T>
        {
            if (changedItems.Contains(editorItem) && editorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.QuestBoardEditor.undoprompt, Strings.QuestBoardEditor.undotitle,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    editorItem.RestoreBackup();
                    updateEditor();
                }
            }
        }

        public static void ToolStripCopyClicked<T>(ref string copiedItem, DatabaseObject<T> editorItem, GameObjectList lstGameObjects, ref ToolStripButton toolStripItemPaste) where T: DatabaseObject<T>
        {
            if (editorItem != null && lstGameObjects.Focused)
            {
                toolStripItemPaste.Enabled = true;
                copiedItem = editorItem.JsonData;
            }
        }

        public static void ToolStripPasteClicked<T>(ref T editorItem, string copiedItem, GameObjectList lstGameObjects, Action updateEditor) where T : DatabaseObject<T>
        {
            if (editorItem != null && copiedItem != null && lstGameObjects.Focused)
            {
                editorItem.Load(copiedItem, true);
                updateEditor();
            }
        }

        public static void EditorItemNameChange<T>(ref T editorItem, TextBox txtName, GameObjectList gameObjectList) where T : DatabaseObject<T>
        {
            editorItem.Name = txtName.Text;
            gameObjectList.UpdateText(txtName.Text);
        }

        public static void ClearSearchPressed(ref DarkTextBox txtSearch)
        {
            txtSearch.Text = Strings.EditorGenerics.SearchPlaceHolder;
        }

        public static void SaveClicked<T>(ref List<T> changedItems, Action hide, Action dispose) where T : DatabaseObject<T>
        {
            foreach (var item in changedItems)
            {
                PacketSender.SendSaveObject(item);
                item.DeleteBackup();
            }

            hide();
            Globals.CurrentEditor = -1;
            dispose();
        }

        public static void CancelClicked<T>(ref List<T> changedItems, Action hide, Action dispose) where T : DatabaseObject<T>
        {
            foreach (var item in changedItems)
            {
                item.RestoreBackup();
                item.DeleteBackup();
            }

            hide();
            Globals.CurrentEditor = -1;
            dispose();
        }

        public static void AddFolder<T>(ref T editorItem, ref DarkComboBox cmbFolder, ref GameObjectList lstGameObjects, Action initEditor) where T : DatabaseObject<T>, IFolderable
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.EditorGenerics.FolderLabel, Strings.EditorGenerics.FolderTitle, ref folderName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
            {
                if (!cmbFolder.Items.Contains(folderName))
                {
                    editorItem.Folder = folderName;
                    lstGameObjects.ExpandFolder(folderName);
                    initEditor();
                    cmbFolder.Text = folderName;
                }
            }
        }

        public static void FolderChanged<T>(ref T editorItem, ComboBox cmbFolder, Action initEditor) where T : DatabaseObject<T>, IFolderable
        {
            editorItem.Folder = cmbFolder.Text;
            initEditor();
        }

        public static void DrawInPictureBox(ref PictureBox pictureBox, ComboBox selector, string rootPath, float r = 255, float g = 255, float b = 255, float a = 255)
        {
            var picItemBmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            var gfx = Graphics.FromImage(picItemBmp);
            gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            if (selector.SelectedIndex > 0)
            {
                var img = Image.FromFile($"{rootPath}/{selector.Text}");
                var imgAttributes = new ImageAttributes();

                // Microsoft, what the heck is this crap?
                imgAttributes.SetColorMatrix(
                    new ColorMatrix(
                        new float[][]
                        {
                            new float[] { r / 255,  0,  0,  0, 0},  // Modify the red space
                            new float[] {0, g / 255,  0,  0, 0},    // Modify the green space
                            new float[] {0,  0, b / 255,  0, 0},    // Modify the blue space
                            new float[] {0,  0,  0, a / 255, 0},    // Modify the alpha space
                            new float[] {0, 0, 0, 0, 1}         // We're not adding any non-linear changes. Value of 1 at the end is a dummy value!
                        }
                    )
                );

                gfx.DrawImage(
                    img, new Rectangle(0, 0, img.Width, img.Height),
                    0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes
                );

                img.Dispose();
                imgAttributes.Dispose();
            }

            gfx.Dispose();

            pictureBox.BackgroundImage = picItemBmp;
        }
    }
}
