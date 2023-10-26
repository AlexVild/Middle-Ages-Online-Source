using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Timers;
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
    public partial class frmDungeon : EditorForm
    {
        private List<DungeonDescriptor> mChanged = new List<DungeonDescriptor>();

        private DungeonDescriptor mEditorItem;

        private string mCopiedItem;

        private bool mPopulating = false;

        private List<string> mKnownFolders = new List<string>();

        private TimeRequirement SelectedRequirement
        {
            get
            {
                var selectedIdx = lstPlayerCounts.SelectedIndex;
                if (mEditorItem == default || mEditorItem.TimeRequirements.Count <= selectedIdx)
                {
                    return default;
                }

                return mEditorItem.TimeRequirements[selectedIdx];
            }
        }

        private List<LootRoll> SelectedTreasure
        {
            get
            {
                var selectedIdx = lstTreasureLevels.SelectedIndex;
                if (mEditorItem == default || !mEditorItem.Treasure.TryGetValue(selectedIdx, out var treasure))
                {
                    return default;
                }

                return treasure;
            }
        }

        public frmDungeon()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                grpEditor.Hide();
            }

            cmbGnomeLootTable.Items.Clear();
            cmbGnomeLootTable.Items.AddRange(LootTableDescriptor.Names);

            cmbLootTable.Items.Clear();
            cmbLootTable.Items.AddRange(LootTableDescriptor.Names);

            cmbTimer.Items.Clear();
            cmbTimer.Items.AddRange(TimerDescriptor.Names);

            cmbStatCeiling.Items.Clear();
            // start at 1 because in MAO rarity 0 == 'none'
            for (var i = 1; i < Strings.ItemEditor.rarity.Count; i++)
            {
                cmbStatCeiling.Items.Add(Strings.ItemEditor.rarity[i]);
            }

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<DungeonDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                DungeonDescriptor.Lookup,
                btnAlphabetical
            );
        }

        private void UpdateFields()
        {
            grpTimes.Hide();
            btnAddTable.Enabled = false;
            btnRemoveTable.Enabled = false;

            mPopulating = true;

            txtName.Text = mEditorItem.Name;
            txtDisplayName.Text = mEditorItem.DisplayName;
            cmbFolder.Text = mEditorItem.Folder;

            nudTimerPlayers.Value = 1;
            nudHours.Value = 0;
            nudMinutes.Value = 0;
            nudSeconds.Value = 0;

            nudTreasureRolls.Value = 1;
            nudTreasureLevels.Value = 0;
            nudGnomeRolls.Value = 1;

            nudGnomeLocations.Value = mEditorItem.GnomeLocations;

            cmbTimer.SelectedIndex = TimerDescriptor.ListIndex(mEditorItem.TimerId);
            RefreshPlayerCounts(false);
            RefreshTreasureLevels(false);
            RefreshGnomeLoot(false);

            chkIgnoreStart.Checked = mEditorItem.IgnoreStartEvents;
            chkIgnoreCompletion.Checked = mEditorItem.IgnoreCompletionEvents;
            chkStoreLongest.Checked = mEditorItem.StoreLongestTime;

            chkStatCeiling.Checked = mEditorItem.ApplyStatCeiling;
            cmbStatCeiling.SelectedIndex = mEditorItem.StatCeilingTier;

            cmbStatCeiling.Enabled = chkStatCeiling.Checked;

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
            mEditorItem = DungeonDescriptor.Get(id);
            UpdateEditor();
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, ref toolStripItemPaste, ref toolStripItemUndo, ref toolStripItemDelete, mCopiedItem, mEditorItem, lstGameObjects);
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.Dungeon);
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

        private void darkTextBox1_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtDisplayName.Text;
        }

        private void cmbTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            mEditorItem.TimerId = TimerDescriptor.IdFromList(cmbTimer.SelectedIndex);
        }

        private void btnAddRequirement_Click(object sender, EventArgs e)
        {
            var participants = (int)nudTimerPlayers.Value;
            if (mEditorItem.TimeRequirements.Find(req => req.Participants == participants) != default)
            {
                return;
            }

            mEditorItem.TimeRequirements.Add(new TimeRequirement(participants));
            RefreshPlayerCounts(true);
        }

        private void RefreshPlayerCounts(bool savePos = false)
        {
            lstPlayerCounts.Items.Clear();
            var pos = -1;
            if (savePos)
            {
                pos = lstPlayerCounts.SelectedIndex;
            }

            foreach(var req in mEditorItem.TimeRequirements)
            {
                lstPlayerCounts.Items.Add($"{req.Participants}+ players");
            }

            if (pos < lstPlayerCounts.Items.Count)
            {
                lstPlayerCounts.SelectedIndex = pos;
            }
        }

        private void lstPlayerCounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayerCounts.SelectedIndex == -1)
            {
                grpTimes.Hide();
                return;
            }

            grpTimes.Show();
            RefreshTimes();
        }

        private void RefreshTimes(bool savePos = false)
        {
            lstRequirement.Items.Clear();
            if (SelectedRequirement == default)
            {
                return;
            }

            var pos = -1;
            if (savePos)
            {
                pos = lstRequirement.SelectedIndex;
            }

            foreach(var req in SelectedRequirement.Requirements)
            {
                var prettyStr = TextUtils.GetTimeElapsedString(req, Strings.DungeonEditor.ElapsedMinutes, Strings.DungeonEditor.ElapsedHours, Strings.DungeonEditor.ElapsedDays);
                lstRequirement.Items.Add(prettyStr);
            }

            if (pos < lstRequirement.Items.Count)
            {
                lstRequirement.SelectedIndex = pos;
            }
        }

        private void addTime_Click(object sender, EventArgs e)
        {
            if (SelectedRequirement == default)
            {
                return;
            }

            long time = (int)nudSeconds.Value * 1000;
            time += (int)nudMinutes.Value * 1000 * 60;
            time += (int)nudHours.Value * 1000 * 60 * 60;

            if (SelectedRequirement.Requirements.Contains(time))
            {
                return;
            }

            SelectedRequirement.Requirements.Add(time);

            RefreshTimes();
        }

        private void removeTime_Click(object sender, EventArgs e)
        {
            if (SelectedRequirement == default)
            {
                return;
            }

            var idx = lstRequirement.SelectedIndex;
            if (SelectedRequirement.Requirements.Count <= idx || idx < 0)
            {
                return;
            }

            SelectedRequirement.Requirements.RemoveAt(idx);

            RefreshTimes(true);
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            lstRequirement.SelectedIndex = -1;
        }

        private void btnRemoveRequirement_Click(object sender, EventArgs e)
        {
            var selectedIdx = lstPlayerCounts.SelectedIndex;
            if (selectedIdx < 0 || mEditorItem.TimeRequirements.Count <= selectedIdx)
            {
                return;
            }

            mEditorItem.TimeRequirements.RemoveAt(selectedIdx);
            RefreshPlayerCounts(true);
        }

        private void btnSortPlayerCounts_Click(object sender, EventArgs e)
        {
            var sortedReqs = mEditorItem.TimeRequirements
                    .OrderByDescending(req => req.Participants)
                    .ToArray();
            mEditorItem.TimeRequirements.Clear();
            mEditorItem.TimeRequirements.AddRange(sortedReqs);

            RefreshPlayerCounts();
        }

        private void btnSortReqs_Click(object sender, EventArgs e)
        {
            if (SelectedRequirement == default)
            {
                return;
            }

            var sortedTimes = SelectedRequirement.Requirements
                    .OrderBy(req => req)
                    .ToArray();
            SelectedRequirement.Requirements.Clear();
            SelectedRequirement.Requirements.AddRange(sortedTimes);

            RefreshTimes();
        }

        private void RefreshTreasureLevels(bool savePos = false)
        {
            var pos = -1;
            if (savePos)
            {
                pos = lstTreasureLevels.SelectedIndex;
            }

            lstTreasureLevels.Items.Clear();
            foreach(var treasureLevel in mEditorItem.Treasure.Keys.OrderBy(k => k).ToArray())
            {
                lstTreasureLevels.Items.Add($"Treasure Level {treasureLevel}");
            }

            if (pos < lstTreasureLevels.Items.Count)
            {
                lstTreasureLevels.SelectedIndex = pos;
            }
        }

        private void RefreshLoot(bool savePos = false)
        {
            var pos = -1;
            if (savePos)
            {
                pos = lstTreasures.SelectedIndex;
            }

            lstTreasures.Items.Clear();
            if (SelectedTreasure == null)
            {
                return;
            }
            
            foreach (var treasure in SelectedTreasure)
            {
                var tableName = LootTableDescriptor.GetName(treasure.DescriptorId);
                lstTreasures.Items.Add($"{tableName} x{treasure.Rolls}");
            }

            if (pos < lstTreasures.Items.Count)
            {
                lstTreasures.SelectedIndex = pos;
            }
        }

        private void lstTreasureLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddTable.Enabled = false;
            btnRemoveTable.Enabled = false;
            if (lstTreasureLevels.SelectedIndex < 0 || lstTreasureLevels.SelectedIndex >= mEditorItem.Treasure.Count)
            {
                mPopulating = true;
                nudExp.Value = 0;
                mPopulating = false;
                return;
            }

            if (!mEditorItem.ExpRewards.ContainsKey(lstTreasureLevels.SelectedIndex))
            {
                nudExp.Value = 0L;
            }
            else
            {
                nudExp.Value = mEditorItem.ExpRewards[lstTreasureLevels.SelectedIndex];
            }

            RefreshLoot();
            btnAddTable.Enabled = true;
            btnRemoveTable.Enabled = true;
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (SelectedTreasure == null)
            {
                return;
            }

            var id = LootTableDescriptor.IdFromList(cmbLootTable.SelectedIndex);
            if (id == Guid.Empty)
            {
                return;
            }

            var roll = new LootRoll(id, (int)nudTreasureRolls.Value);
            if (SelectedTreasure.Contains(roll))
            {
                return;
            }

            SelectedTreasure.Add(roll);
            RefreshLoot();
        }

        private void btnRemoveTable_Click(object sender, EventArgs e)
        {
            if (lstTreasures.SelectedIndex < 0 || lstTreasures.SelectedIndex >= SelectedTreasure.Count)
            {
                return;
            }

            SelectedTreasure.RemoveAt(lstTreasures.SelectedIndex);
            RefreshLoot(true);
        }

        private void nudGnomeLocations_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.GnomeLocations = (int)nudGnomeLocations.Value;
        }

        private void btnAddTreasureLevel_Click(object sender, EventArgs e)
        {
            if (mEditorItem.Treasure.TryGetValue((int)nudTreasureLevels.Value, out _))
            {
                return;
            }

            mEditorItem.Treasure[(int)nudTreasureLevels.Value] = new List<LootRoll>();
            RefreshTreasureLevels();
        }

        private void btnRemoveTreasureLevel_Click(object sender, EventArgs e)
        {
            if (!mEditorItem.Treasure.TryGetValue(lstTreasureLevels.SelectedIndex, out _))
            {
                return;
            }

            mEditorItem.Treasure.Remove(lstTreasureLevels.SelectedIndex);
            RefreshTreasureLevels(true);
        }

        private void RefreshGnomeLoot(bool savePos = false)
        {
            var pos = -1;
            if (savePos)
            {
                pos = lstGnomeLoot.SelectedIndex;
            }

            lstGnomeLoot.Items.Clear();
            foreach (var loot in mEditorItem.GnomeTreasure)
            {
                var tableName = LootTableDescriptor.GetName(loot.DescriptorId);
                lstGnomeLoot.Items.Add($"{tableName} x{loot.Rolls}");
            }

            if (pos < lstGnomeLoot.Items.Count)
            {
                lstGnomeLoot.SelectedIndex = pos;
            }
        }

        private void btnAddGnomeLoot_Click(object sender, EventArgs e)
        {
            var id = LootTableDescriptor.IdFromList(cmbGnomeLootTable.SelectedIndex);
            if (id == Guid.Empty)
            {
                return;
            }

            var roll = new LootRoll(id, (int)nudGnomeRolls.Value);
            if (mEditorItem.GnomeTreasure.Contains(roll))
            {
                return;
            }

            mEditorItem.GnomeTreasure.Add(roll);
            RefreshGnomeLoot();
        }

        private void btnRemoveGnomeLoot_Click(object sender, EventArgs e)
        {
            if (lstGnomeLoot.SelectedIndex < 0 || lstGnomeLoot.SelectedIndex >= mEditorItem.GnomeTreasure.Count)
            {
                return;
            }

            mEditorItem.GnomeTreasure.RemoveAt(lstGnomeLoot.SelectedIndex);
            RefreshGnomeLoot(true);
        }

        private void nudExp_ValueChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }
            mEditorItem.ExpRewards[lstTreasureLevels.SelectedIndex] = (long)nudExp.Value;
        }

        private void chkIgnoreStart_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IgnoreStartEvents = chkIgnoreStart.Checked;
        }

        private void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IgnoreCompletionEvents = chkIgnoreCompletion.Checked;
        }

        private void chkStoreLongest_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.StoreLongestTime = chkStoreLongest.Checked;
        }

        private void chkStatCeiling_CheckedChanged(object sender, EventArgs e)
        {
            cmbStatCeiling.Enabled = chkStatCeiling.Checked;
            mEditorItem.ApplyStatCeiling = chkStatCeiling.Checked;
        }

        private void cmbStatCeiling_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.StatCeilingTier = cmbStatCeiling.SelectedIndex;
        }
    }
}
