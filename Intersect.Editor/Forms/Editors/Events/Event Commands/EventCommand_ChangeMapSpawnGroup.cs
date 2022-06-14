using Intersect.Editor.General;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps.MapList;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ChangeMapSpawnGroup : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeSpawnGroupCommand mMyCommand;

        public EventCommand_ChangeMapSpawnGroup(ChangeSpawnGroupCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            InitLocalization();
            SetupFormValues();
        }

        private void InitLocalization()
        {
            // stub
        }

        private void SetupFormValues()
        {
            cmbMap.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMap.Items.Add(MapList.OrderedMaps[i].Name);
                if (mMyCommand.MapId != default && MapList.OrderedMaps[i].MapId == mMyCommand.MapId)
                {
                    cmbMap.SelectedIndex = i;
                }
                else if (mMyCommand == default && MapList.OrderedMaps[i].MapId == Globals.CurrentMap.Id)
                {
                    cmbMap.SelectedIndex = i;
                }
            }

            btnSave.Enabled = cmbMap.Items.Count >= 1;

            switch(mMyCommand.Operator)
            {
                case ChangeSpawnOperator.SET:
                    optNumericSet.Checked = true;
                    break;
                case ChangeSpawnOperator.ADD:
                    optNumericAdd.Checked = true;
                    break;
                case ChangeSpawnOperator.SUBTRACT:
                    optNumericSubtract.Checked = true;
                    break;
            }

            chkSurrounding.Checked = mMyCommand.SurroundingMaps;
            nudSpawnGroup.Value = mMyCommand.SpawnGroup;
            chkResetNpcs.Checked = mMyCommand.ResetNpcs;
            chkPlayerMap.Checked = mMyCommand.UsePlayerMap;
        }

        private void SaveValues()
        {
            if (cmbMap.SelectedIndex >= 0)
            {
                mMyCommand.MapId = MapList.OrderedMaps[cmbMap.SelectedIndex].MapId;
            }
            mMyCommand.SpawnGroup = (int)nudSpawnGroup.Value;
            mMyCommand.ResetNpcs = chkResetNpcs.Checked;
            mMyCommand.SurroundingMaps = chkSurrounding.Checked;
            mMyCommand.UsePlayerMap = chkPlayerMap.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveValues();
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void optNumericSet_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.Operator = ChangeSpawnOperator.SET;
        }

        private void optNumericAdd_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.Operator = ChangeSpawnOperator.ADD;
        }

        private void optNumericSubtract_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.Operator = ChangeSpawnOperator.SUBTRACT;
        }

        private void chkPlayerMap_CheckedChanged(object sender, EventArgs e)
        {
            cmbMap.Enabled = !chkPlayerMap.Checked;
        }
    }
}
