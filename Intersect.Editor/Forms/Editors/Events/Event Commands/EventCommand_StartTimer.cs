using Intersect.Editor.Localization;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Timers;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_StartTimer : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private StartTimerCommand mMyCommand;

        public EventCommand_StartTimer(StartTimerCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            InitLocalization();
            mMyCommand = refCommand;
            mEventEditor = editor;

            TimerCommandHelpers.InitializeSelectionFields(refCommand, ref cmbTimerType, ref cmbTimer);
        }

        private void InitLocalization()
        {
            grpStartTimer.Text = Strings.EventStartTimer.Title;

            lblType.Text = Strings.EventStartTimer.OwnerType;
            lblTimer.Text = Strings.EventStartTimer.TimerSelect;

            btnSave.Text = Strings.EventStartTimer.ButtonOkay;
            btnCancel.Text = Strings.EventStartTimer.ButtonCancel;
        }

        private void cmbTimerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimerCommandHelpers.RefreshTimerSelector(ref cmbTimer, (TimerOwnerType)cmbTimerType.SelectedIndex);
            mMyCommand.DescriptorId = TimerDescriptor.IdFromList(cmbTimer.SelectedIndex, (TimerOwnerType)cmbTimerType.SelectedIndex);
        }

        private void cmbTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.DescriptorId = TimerDescriptor.IdFromList(cmbTimer.SelectedIndex, (TimerOwnerType)cmbTimerType.SelectedIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
