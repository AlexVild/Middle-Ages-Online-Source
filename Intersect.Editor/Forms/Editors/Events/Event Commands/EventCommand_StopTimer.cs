using Intersect.Editor.Localization;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Timers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_StopTimer : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private StopTimerCommand mMyCommand;

        public EventCommand_StopTimer(StopTimerCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            InitLocalization();

            mMyCommand = refCommand;
            mEventEditor = editor;

            TimerCommandHelpers.InitializeSelectionFields(refCommand, ref cmbTimerType, ref cmbTimer);

            cmbStopType.SelectedIndex = (int) refCommand.StopType;
        }

        private void InitLocalization()
        {
            grpStopTimer.Text = Strings.EventStopTimer.Title;

            grpTimer.Text = Strings.EventStopTimer.TimerGroup;

            lblType.Text = Strings.EventStopTimer.OwnerType;
            lblTimer.Text = Strings.EventStopTimer.TimerSelect;

            grpEventType.Text = Strings.EventStopTimer.EventGroup;
            lblStopType.Text = Strings.EventStopTimer.StopType;

            cmbStopType.Items.AddRange(Strings.EventStopTimer.TimerStopCommands.Values.ToArray());

            btnOk.Text = Strings.EventStopTimer.ButtonOkay;
            btnCancel.Text = Strings.EventStopTimer.ButtonCancel;
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

        private void cmbStopType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.StopType = (TimerStopType)cmbStopType.SelectedIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
