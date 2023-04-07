using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_OpenDeconstructor : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private OpenDeconstructorCommand mMyCommand;

        public EventCommand_OpenDeconstructor(OpenDeconstructorCommand refCommand, FrmEvent editor)
        {
            mMyCommand = refCommand;
            mEventEditor = editor;

            InitializeComponent();
            
            nudMultiplier.Value = (decimal)(mMyCommand?.FuelCostMultiplier ?? 1.00);
            chkDisableBank.Checked = mMyCommand.DisableBank;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.FuelCostMultiplier = (float)nudMultiplier.Value;
            mMyCommand.DisableBank = chkDisableBank.Checked;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
