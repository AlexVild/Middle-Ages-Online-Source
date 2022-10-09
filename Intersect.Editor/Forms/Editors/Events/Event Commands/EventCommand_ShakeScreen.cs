using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ShakeScreen : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ShakeScreenCommand mMyCommand;

        public EventCommand_ShakeScreen(ShakeScreenCommand refCommand, FrmEvent eventEditor)
        {
            mMyCommand = refCommand;
            mEventEditor = eventEditor;

            InitializeComponent();

            InitEditor();
        }

        private void InitEditor()
        {
            nudIntensity.Value = (decimal)mMyCommand.Intensity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Intensity = (float)nudIntensity.Value;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
