using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ForceNpcExhaustion : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ForceNpcExhaustion mMyCommand;

        public EventCommand_ForceNpcExhaustion(ForceNpcExhaustion refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mEventEditor = editor;
            mMyCommand = refCommand;

            PopulateForm();
        }

        private void PopulateForm()
        {
            cmbNpc.Items.Clear();
            cmbNpc.Items.AddRange(NpcBase.Names);
            cmbNpc.SelectedIndex = 0;
            if (mMyCommand != null)
            {
                cmbNpc.SelectedIndex = NpcBase.ListIndex(mMyCommand.NpcId);
                nudWarpX.Value = mMyCommand.DurationMs;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.NpcId = NpcBase.IdFromList(cmbNpc.SelectedIndex);
            mMyCommand.DurationMs = (int)nudWarpX.Value;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
