using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;
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
    public partial class frmChallenge : EditorForm
    {
        private List<ChallengeDescriptor> mChanged = new List<ChallengeDescriptor>();

        private ChallengeDescriptor mEditorItem;

        private string mCopiedItem;

        private bool mPopulating = false;

        private List<string> mKnownFolders = new List<string>();

        private GameObjectType ParamType;

        public frmChallenge()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                grpEditor.Hide();
            }

            cmbChallengeType.Items.Clear();
            cmbChallengeType.Items.AddRange(EnumExtensions.GetDescriptions(typeof(ChallengeType)));

            cmbEvent.Items.Clear();
            cmbEvent.Items.Add(Strings.General.none);
            cmbEvent.Items.AddRange(EventBase.Names);

            cmbSpell.Items.Clear();
            cmbSpell.Items.Add(Strings.General.none);
            cmbSpell.Items.AddRange(SpellBase.Names);

            lstGameObjects.Init(UpdateToolStripItems, 
                AssignEditorItem, 
                toolStripItemNew_Click_1, 
                toolStripItemCopy_Click, 
                toolStripItemUndo_Click, 
                toolStripItemPaste_Click, 
                toolStripItemDelete_Click);
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<ChallengeDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                ChallengeDescriptor.Lookup,
                btnAlphabetical
            );
        }

        private void UpdateFields()
        {
            txtName.Text = mEditorItem.Name;
            cmbFolder.Text = mEditorItem?.Folder ?? string.Empty;

            cmbChallengeType.SelectedIndex = (int)mEditorItem.Type;
            cmbSpell.SelectedIndex = SpellBase.ListIndex(mEditorItem.SpellUnlockId) + 1;
            cmbEvent.SelectedIndex = EventBase.ListIndex(mEditorItem.CompletionEventId) + 1;

            txtStartDesc.Text = mEditorItem.EventDescription;
            nudSets.Value = mEditorItem.Sets;
            nudReps.Value = mEditorItem.Reps;
            nudParam.Value = mEditorItem.Param;

            if ((ChallengeParamType)mEditorItem.ChallengeParamType != ChallengeParamType.None)
            {
                ParamType = ((ChallengeParamType)mEditorItem.ChallengeParamType).GetRelatedTable();
                cmbParamSelector.SelectedIndex = ParamType.ListIndex(mEditorItem.ChallengeParamId) + 1;
            }
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
            mEditorItem = ChallengeDescriptor.Get(id);
            UpdateEditor();
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, 
                ref toolStripItemPaste, 
                ref toolStripItemUndo, 
                ref toolStripItemDelete, 
                mCopiedItem, 
                mEditorItem, 
                lstGameObjects);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormHelpers.SaveClicked(ref mChanged, Hide, Dispose);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHelpers.CancelClicked(ref mChanged, Hide, Dispose);
        }

        private void toolStripItemNew_Click_1(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.Challenge);
        }

        private void cmbChallengeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Type = (ChallengeType)cmbChallengeType.SelectedIndex;
            UpdateChallengeLabels(mEditorItem.Type);
        }

        private void UpdateChallengeLabels(ChallengeType challengeType)
        {
            lblReps.Text = "Repetitions";
            lblSets.Text = "Sets";
            lblParam.Text = "Addtl. Param";
            nudReps.Enabled = true;
            nudSets.Visible = true;
            nudParam.Enabled = false;
            nudParam.Visible = true;
            cmbParamSelector.Visible = false;
            mEditorItem.ChallengeParamType = (int)ChallengeParamType.None;

            switch (challengeType)
            {
                case ChallengeType.ComboEarned:
                    lblReps.Text = "Combo";
                    break;
                case ChallengeType.BeastsKilledOverTime:
                    lblReps.Text = "# Beasts";
                    nudParam.Enabled = true;
                    lblParam.Text = "Time (ms)";
                    break;
                case ChallengeType.DamageAtRange:
                    lblReps.Text = "Damage";
                    nudParam.Enabled = true;
                    lblParam.Text = "Range (tiles)";
                    break;
                case ChallengeType.DamageHealedAtHealth:
                    lblReps.Text = "Damage"; 
                    nudParam.Enabled = true;
                    lblParam.Text = "Health %";
                    break;
                case ChallengeType.DamageOverTime:
                    lblReps.Text = "Damage";
                    nudParam.Enabled = true;
                    lblParam.Text = "Time (ms)";
                    break;
                case ChallengeType.DamageTakenOverTime:
                    lblReps.Text = "Damage";
                    nudParam.Enabled = true;
                    lblParam.Text = "Time (ms)";
                    break;
                case ChallengeType.HitFreeStreak:
                    lblReps.Text = "Hits w/o receiving";
                    break;
                case ChallengeType.MaxHit:
                    lblReps.Text = "Dmg Threshold";
                    break;
                case ChallengeType.MissFreeStreak:
                    lblReps.Text = "Successful Hits";
                    break;
                default:
                    throw new NotImplementedException();
            }

            cmbParamSelector.Visible = false;
            if (mEditorItem.ChallengeParamType != (int)ChallengeParamType.None)
            {
                cmbParamSelector.Visible = true;
                ParamType = ((ChallengeParamType)mEditorItem.ChallengeParamType).GetRelatedTable();
                cmbParamSelector.Items.Clear();
                cmbParamSelector.Items.Add(Strings.General.none);
                cmbParamSelector.Items.AddRange(ParamType.Names());
            }
        }

        private void cmbSpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.SpellUnlockId = SpellBase.IdFromList(cmbSpell.SelectedIndex - 1);
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CompletionEventId = EventBase.IdFromList(cmbEvent.SelectedIndex - 1);
        }

        private void txtStartDesc_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.EventDescription = txtStartDesc.Text;
        }

        private void nudReps_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Reps = (int)nudReps.Value;
        }

        private void nudSets_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Sets = (int)nudSets.Value;
        }

        private void cmbParamSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ChallengeParamId = ParamType.IdFromList(cmbParamSelector.SelectedIndex - 1);
        }

        private void nudParam_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Param = (int)nudParam.Value;
        }
    }
}
