using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.Localization;
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

        private WeaponLevel SelectedLevel => mEditorItem?.Unlocks?.ContainsKey(lstLevels.SelectedIndex + 1) ?? false ? mEditorItem.Unlocks[lstLevels.SelectedIndex + 1] : default;

        public frmWeaponType()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                grpEditor.Hide();
            }

            cmbChallenges.Items.Clear();
            cmbChallenges.Items.Add(Strings.General.none);
            cmbChallenges.Items.AddRange(ChallengeDescriptor.Names);

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        public override void InitEditor()
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
            mPopulating = true;

            txtName.Text = mEditorItem.Name;
            cmbFolder.Text = mEditorItem?.Folder ?? string.Empty;

            nudMaxLevel.Value = mEditorItem.MaxLevel;

            lstLevels.Items.Clear();
            nudReqExp.Value = 0;
            nudEpCost.Value = 0;
            cmbChallenges.SelectedIndex = 0;

            UpdateLevelList();
            
            mPopulating = false;
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

        private void UpdateLevelList(bool savePosition = false)
        {
            var oldPosition = 0;
            if (savePosition)
            {
                oldPosition = lstLevels.SelectedIndex;
            }

            lstLevels.Items.Clear();
            foreach (var unlock in mEditorItem.Unlocks)
            {
                var level = unlock.Key;
                var info = unlock.Value;
                lstLevels.Items.Add($"Lvl {level} -- { info }");
            }
            
            if (lstLevels.Items.Count > oldPosition)
            {
                lstLevels.SelectedIndex = oldPosition;
            }

            UpdateChallengeList();
        }

        private void UpdateChallengeList()
        {
            lstChallenges.Items.Clear();
            if (SelectedLevel == default)
            {
                return;
            }

            foreach (var challenge in SelectedLevel.ChallengeIds)
            {
                lstChallenges.Items.Add(ChallengeDescriptor.GetName(challenge));
            }
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

        private void nudMaxLevel_ValueChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            var currentMax = mEditorItem.MaxLevel;
            var newMaxLevel = (int)nudMaxLevel.Value;

            mEditorItem.MaxLevel = newMaxLevel;
            if (currentMax < newMaxLevel && !mEditorItem.Unlocks.TryGetValue(newMaxLevel, out _))
            {
                mEditorItem.Unlocks[newMaxLevel] = new WeaponLevel((int)nudReqExp.Value, (int)nudEpCost.Value);
            }
            else if (currentMax > newMaxLevel)
            {
                mEditorItem.Unlocks.Remove(currentMax);
            }

            UpdateLevelList(true);
        }

        private void nudReqExp_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedLevel == default)
            {
                return;
            }

            SelectedLevel.RequiredExp = (int)nudReqExp.Value;
            UpdateLevelList(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbChallenges.SelectedIndex <= 0 || SelectedLevel == default)
            {
                return;
            }

            var challengeId = ChallengeDescriptor.IdFromList(cmbChallenges.SelectedIndex - 1);
            if (SelectedLevel.ChallengeIds.Contains(challengeId))
            {
                return;
            }

            SelectedLevel.ChallengeIds.Add(challengeId);
            UpdateLevelList(true);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SelectedLevel == default || lstChallenges.SelectedIndex < 0)
            {
                return;
            }

            SelectedLevel.ChallengeIds.RemoveAt(lstChallenges.SelectedIndex);
            UpdateLevelList(true);
        }

        private void lstLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedLevel == default)
            {
                return;
            }
            
            nudReqExp.Value = SelectedLevel.RequiredExp;
            nudEpCost.Value = SelectedLevel.EnhancementCostPerPoint;
            UpdateChallengeList();
            lstChallenges.SelectedIndex = -1;
        }

        private void nudEpCost_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedLevel == default)
            {
                return;
            }

            SelectedLevel.EnhancementCostPerPoint = (int)nudEpCost.Value;
            UpdateLevelList(true);
        }
    }
}
