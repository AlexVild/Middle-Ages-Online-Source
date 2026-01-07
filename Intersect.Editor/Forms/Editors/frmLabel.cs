using Intersect.Editor.Forms.Helpers;
using Intersect.Enums;
using Intersect.GameObjects;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmLabel : EditorForm
    {
        private List<LabelDescriptor> mChanged = new List<LabelDescriptor>();
        
        private LabelDescriptor mEditorItem;
        
        private string mCopiedItem;

        private bool mPopulating = false;
        
        private List<string> mKnownFolders = new List<string>();

        public frmLabel()
        {
            ApplyHooks();
            InitializeComponent();
            
            if (mEditorItem == null)
            {
                grpProperties.Hide();
            }

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = LabelDescriptor.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
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
                        mPopulating = true;

                        txtName.Text = mEditorItem.Name;
                        cmbFolder.Text = mEditorItem.Folder;
                        txtLabel.Text = mEditorItem.DisplayName;
                        txtHint.Text = mEditorItem.Hint;
                        
                        var color = mEditorItem.Color;
                        if (color == null)
                        {
                            color = Color.White;
                        }

                        pnlColor.BackColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
                        chkMatchColor.Checked = mEditorItem.MatchNameColor;
                        ToggleColorAvailability();
                        rdoHeader.Checked = mEditorItem.Position == LabelPosition.Header;
                        rdoFooter.Checked = mEditorItem.Position == LabelPosition.Footer;
                        chkOnlyShowUnlocked.Checked = mEditorItem.ShowOnlyUnlocked;

                        mPopulating = false;
                    }
                );
        }

        public override void InitEditor()
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

        private void ToggleColorAvailability()
        {
            btnSelectLightColor.Enabled = !mEditorItem.MatchNameColor;
        }

        private void btnSelectLightColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlColor.BackColor = colorDialog.Color;
                mEditorItem.Color = Color.FromArgb(
                    pnlColor.BackColor.A, pnlColor.BackColor.R, pnlColor.BackColor.G, pnlColor.BackColor.B
                );
            }
        }

        private void chkMatchColor_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }
            mEditorItem.MatchNameColor = chkMatchColor.Checked;
            ToggleColorAvailability();
        }

        private void rdoHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }
            mEditorItem.Position = LabelPosition.Header;
        }

        private void rdoFooter_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }
            mEditorItem.Position = LabelPosition.Footer;
        }

        private void chkOnlyShowUnlocked_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ShowOnlyUnlocked = chkOnlyShowUnlocked.Checked;
        }
    }
}
