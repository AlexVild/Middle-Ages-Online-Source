using Intersect.Editor.Forms.Helpers;
using Intersect.Enums;
using Intersect.GameObjects;
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
    public partial class frmWeaponType : EditorForm
    {
        private List<WeaponTypeDescriptor> mChanged = new List<WeaponTypeDescriptor>();

        private WeaponTypeDescriptor mEditorItem;

        private string mCopiedItem;

        private bool mPopulating = false;

        private List<string> mKnownFolders = new List<string>();

        public frmWeaponType()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                grpEditor.Hide();
            }

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<WeaponTypeDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                WeaponTypeDescriptor.Lookup,
                btnAlphabetical
            );
        }

        private void UpdateFields()
        {

        }

        private void UpdateEditor()
        {
            FormHelpers.UpdateEditor(
                ref mEditorItem,
                ref mChanged,
                ref grpEditor,
                UpdateToolStripItems,
                UpdateFields
            );
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = WeaponTypeDescriptor.Get(id);
            UpdateEditor();
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, ref toolStripItemPaste, ref toolStripItemUndo, ref toolStripItemDelete, mCopiedItem, mEditorItem, lstGameObjects);
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.WeaponType);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.SearchTextChanged(InitEditor);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            FormHelpers.ClearSearchPressed(ref txtSearch);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormHelpers.SaveClicked(ref mChanged, Hide, Dispose);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHelpers.CancelClicked(ref mChanged, Hide, Dispose);
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelpers.FolderChanged(ref mEditorItem, cmbFolder, InitEditor);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FormHelpers.AddFolder(ref mEditorItem, ref cmbFolder, ref lstGameObjects, InitEditor);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.EditorItemNameChange(ref mEditorItem, txtName, lstGameObjects);
        }
    }
}
