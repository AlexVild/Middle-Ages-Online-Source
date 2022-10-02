using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_UnlockLabel : UserControl
    {
        protected UnlockLabelCommand mMyCommand;
        protected bool IsLoading = false;
        protected readonly FrmEvent mEventEditor;

        public EventCommand_UnlockLabel(UnlockLabelCommand refCommand, FrmEvent editor)
        {
            mMyCommand = refCommand;
            mEventEditor = editor;
            InitializeComponent();

            IsLoading = true;
            InitEditor();
            IsLoading = false;
        }

        private void InitEditor()
        {
            cmbLabels.Items.AddRange(LabelDescriptor.GetNameList());
            PopulateEditor();
        }

        private void PopulateEditor()
        {
            if (cmbLabels.Items.Count > 0)
            {
                var selLabel = LabelDescriptor.Get(mMyCommand.LabelId);
                if (selLabel != default)
                {
                    cmbLabels.SelectedIndex = LabelDescriptor.ListIndex(mMyCommand.LabelId);
                }
                else
                {
                    cmbLabels.SelectedIndex = 0;
                }
            }

            rdoUnlock.Checked = mMyCommand.Status == UnlockLabelCommand.LabelUnlockStatus.Unlock;
            rdoRemove.Checked = mMyCommand.Status == UnlockLabelCommand.LabelUnlockStatus.Remove;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.LabelId = LabelDescriptor.IdFromList(cmbLabels.SelectedIndex);
            mMyCommand.Status = rdoUnlock.Checked ? UnlockLabelCommand.LabelUnlockStatus.Unlock : UnlockLabelCommand.LabelUnlockStatus.Remove;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
