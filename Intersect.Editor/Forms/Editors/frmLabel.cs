using Intersect.Editor.Forms.Helpers;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmLabel : EditorForm
    {
        private List<LabelDescriptor> mChanged = new List<LabelDescriptor>();
        
        private LabelDescriptor mEditorItem;
        
        private string mCopiedItem;
        
        private List<string> mKnownFolders = new List<string>();

        public frmLabel()
        {
            ApplyHooks();
            InitializeComponent();
            
            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = LabelDescriptor.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(GameObjectType.Label, ref mEditorItem, InitEditor, UpdateEditor);
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, ref toolStripItemPaste, ref toolStripItemUndo, ref toolStripItemDelete, mCopiedItem, mEditorItem, lstGameObjects);
        }

        private void UpdateEditor()
        {
            FormHelpers.UpdateEditor(
                    ref mEditorItem,
                    ref mChanged,
                    ref grpProperties,
                    UpdateToolStripItems,
                    () =>
                    {
                        txtName.Text = mEditorItem.Name;
                        cmbFolder.Text = mEditorItem.Folder;
                        txtLabel.Text = mEditorItem.DisplayName;
                        txtHint.Text = mEditorItem.Hint;
                    }
                );
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<LabelDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                LabelDescriptor.Lookup,
                btnAlphabetical
            );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormHelpers.SaveClicked(ref mChanged, Hide, Dispose);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHelpers.CancelClicked(ref mChanged, Hide, Dispose);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.SearchTextChanged(InitEditor);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            FormHelpers.ClearSearchPressed(ref txtSearch);
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.Label);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripDeleteClicked(mEditorItem, lstGameObjects);
        }

        private void btnAlphabetical_Click(object sender, EventArgs e)
        {
            FormHelpers.AlphabeticalClicked(ref btnAlphabetical, InitEditor);
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripCopyClicked(ref mCopiedItem, mEditorItem, lstGameObjects, ref toolStripItemPaste);
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripPasteClicked(ref mEditorItem, mCopiedItem, lstGameObjects, UpdateEditor);
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripUndoClicked(mChanged, ref mEditorItem, UpdateEditor);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.EditorItemNameChange(ref mEditorItem, txtName, lstGameObjects);
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelpers.FolderChanged(ref mEditorItem, cmbFolder, InitEditor);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FormHelpers.AddFolder(ref mEditorItem, ref cmbFolder, ref lstGameObjects, InitEditor);
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtLabel.Text;
        }

        private void txtHint_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Hint = txtHint.Text;
        }
    }
}
