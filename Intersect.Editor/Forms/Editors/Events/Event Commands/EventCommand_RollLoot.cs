using Intersect.Editor.Localization;
using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_RollLoot : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private List<LootRoll> mTables;
        private RollLootCommand mMyCommand;

        private bool populating = false;

        public EventCommand_RollLoot(RollLootCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mTables = new List<LootRoll>();
            if (mMyCommand.LootTables != null)
            {
                foreach(var table in mMyCommand.LootTables)
                {
                    mTables.Add(table.Clone());
                }
            }
            else
            {
                mMyCommand.LootTables = new List<LootRoll>();
            }
            
            mEventEditor = editor;

            UpdateTableList();
            cmbTable.Items.Clear();
            cmbTable.Items.AddRange(LootTableDescriptor.Names);
        }

        private void UpdateTableList(bool keepIndex = false)
        {
            var savedIdx = lstTables.SelectedIndex;
            lstTables.Items.Clear();
            if (mTables?.Count > 0)
            {
                lstTables.Items.AddRange(GetTableDisplayList());
            }

            if (keepIndex && lstTables.Items.Count > savedIdx) 
            {
                populating = true;
                lstTables.SelectedIndex = savedIdx;
                populating = false;
            }
        }

        private string[] GetTableDisplayList()
        {
            return mTables?.Select((roll) => GenerateTableListString(roll.LootTable.Name, roll.Rolls)).ToArray();
        }

        private string GenerateTableListString(string tableName, int rolls)
        {
            return Strings.EventCommandRollLoot.Table.ToString(tableName, rolls);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            mMyCommand.LootTables.Clear();
            foreach (var table in mTables)
            {
                mMyCommand.LootTables.Add(table.Clone());
            }
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void cmbTable_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var idx = lstTables.SelectedIndex;
            if (populating || idx < 0 || mMyCommand?.LootTables?.Count <= idx)
            {
                return;
            }

            mTables[idx].DescriptorId = LootTableDescriptor.IdFromList(cmbTable.SelectedIndex);
            UpdateTableList(true);
        }

        private void nudRolls_ValueChanged(object sender, System.EventArgs e)
        {
            if (lstTables.SelectedIndex < 0 || lstTables.Items.Count <= 0 || populating)
            {
                return;
            }

            var idx = lstTables.SelectedIndex;
            if (idx >= mMyCommand?.LootTables?.Count)
            {
                return;
            }
            mTables[idx].Rolls = (int)nudRolls.Value;

            UpdateTableList(true);
        }

        private void btnDropAdd_Click(object sender, System.EventArgs e)
        {
            var newRoll = new LootRoll(LootTableDescriptor.IdFromList(cmbTable.SelectedIndex), (int)nudRolls.Value);
            if (mTables== null)
            {
                mTables = new List<LootRoll>();
            }

            mTables.Add(newRoll);

            UpdateTableList();
            lstTables.SelectedIndex = -1;
        }

        private void btnDropRemove_Click(object sender, System.EventArgs e)
        {
            if (lstTables.SelectedIndex < 0)
            {
                return;
            }
            mTables.RemoveAt(lstTables.SelectedIndex);

            UpdateTableList();
        }

        private void lstTables_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstTables.SelectedIndex < 0 || lstTables.SelectedIndex >= mTables.Count || populating)
            {
                return;
            }
            var roll = mTables[lstTables.SelectedIndex];

            populating = true;
            
            cmbTable.SelectedIndex = LootTableDescriptor.ListIndex(roll.DescriptorId);
            nudRolls.Value = roll.Rolls;

            populating = false;
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            lstTables.SelectedIndex = -1;
        }
    }
}
