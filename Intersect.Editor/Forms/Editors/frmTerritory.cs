using Intersect.Editor.Content;
using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmTerritory : EditorForm
    {
        private List<TerritoryDescriptor> mChanged = new List<TerritoryDescriptor>();

        private TerritoryDescriptor mEditorItem;

        private string mCopiedItem;

        private bool mPopulating = false;

        private List<string> mKnownFolders = new List<string>();

        public frmTerritory()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                grpTerritories.Hide();
            }

            cmbIcon.Items.Clear();
            cmbIcon.Items.Add(Strings.General.none);
            cmbIcon.Items.AddRange(GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Item));

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<TerritoryDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                TerritoryDescriptor.Lookup,
                btnAlphabetical
            );
        }

        private void UpdateFields()
        {
            mPopulating = true;

            txtName.Text = mEditorItem.Name;
            txtDisplayName.Text = mEditorItem.DisplayName;
            cmbFolder.Text = mEditorItem.Folder;
            cmbIcon.SelectedIndex = cmbIcon.FindString(TextUtils.NullToNone(mEditorItem.Icon));

            nudCaptureTime.Value = mEditorItem.CaptureMs;
            nudTick.Value = mEditorItem.PointsPerTick;
            nudCapture.Value = mEditorItem.PointsPerCapture;
            nudDefend.Value = mEditorItem.PointsPerDefend;
            nudAttack.Value = mEditorItem.PointsPerAttack;

            if (cmbIcon.SelectedIndex > 0)
            {
                DrawIcon();
            }

            mPopulating = false;
        }

        private void DrawIcon()
        {
            FormHelpers.DrawInPictureBox(ref picItem, cmbIcon, "resources/items");
        }

        private void UpdateEditor()
        {
            FormHelpers.UpdateEditor(
                ref mEditorItem,
                ref mChanged,
                ref grpTerritories,
                UpdateToolStripItems,
                UpdateFields
            );
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = TerritoryDescriptor.Get(id);
            UpdateEditor();
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, ref toolStripItemPaste, ref toolStripItemUndo, ref toolStripItemDelete, mCopiedItem, mEditorItem, lstGameObjects);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.Territory);
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

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelpers.FolderChanged(ref mEditorItem, cmbFolder, InitEditor);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FormHelpers.AddFolder(ref mEditorItem, ref cmbFolder, ref lstGameObjects, InitEditor);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormHelpers.SaveClicked(ref mChanged, Hide, Dispose);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHelpers.CancelClicked(ref mChanged, Hide, Dispose);
        }

        private void cmbIcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Icon = cmbIcon.Text;
            DrawIcon();
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtDisplayName.Text;
        }

        private void nudCaptureTime_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CaptureMs = (long)nudCaptureTime.Value;
        }

        private void nudTick_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.PointsPerTick = (int)nudTick.Value;
        }

        private void nudDefend_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.PointsPerDefend = (int)nudDefend.Value;
        }

        private void nudCapture_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.PointsPerCapture = (int)nudCapture.Value;
        }

        private void nudAttack_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.PointsPerAttack = (int)nudAttack.Value;
        }
    }
}
