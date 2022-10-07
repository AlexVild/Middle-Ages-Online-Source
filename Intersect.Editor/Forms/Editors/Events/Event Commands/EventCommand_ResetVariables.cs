using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ResetVariables : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ResetVariableCommand mMyCommand;

        public EventCommand_ResetVariables(ResetVariableCommand refCommand, FrmEvent editor)
        {
            mMyCommand = refCommand;
            mEventEditor = editor;

            InitializeComponent();
            InitEditor();
        }

        private void InitEditor()
        {
            cmbGroups.Items.Clear();
            foreach (var playerVar in PlayerVariableBase.Lookup.Select(kv => (PlayerVariableBase)kv.Value).ToArray())
            {
                if (string.IsNullOrEmpty(playerVar.VariableGroup) || cmbGroups.Items.Contains(playerVar.VariableGroup))
                {
                    continue;
                }
                cmbGroups.Items.Add(playerVar.VariableGroup);
            }
            if (cmbGroups.Items.Count > 0)
            {
                cmbGroups.SelectedIndex = 0;
                if (!string.IsNullOrEmpty(mMyCommand.Group))
                {
                    var idx = cmbGroups.Items.IndexOf(mMyCommand.Group);
                    if (idx > 0 && idx < cmbGroups.Items.Count)
                    {
                        cmbGroups.SelectedIndex = idx;
                    }
                }
            }
            chkPartySync.Checked = mMyCommand.SyncParty;
            chkTriggerCommon.Checked = mMyCommand.FireCommonEvents;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Group = cmbGroups.SelectedItem.ToString();
            mMyCommand.FireCommonEvents = chkPartySync.Checked;
            mMyCommand.SyncParty = chkPartySync.Checked;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
