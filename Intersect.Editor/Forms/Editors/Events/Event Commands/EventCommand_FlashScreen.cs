using System;
using System.Windows.Forms;
using Intersect.GameObjects.Events.Commands;
using Intersect.Editor.General;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_FlashScreen : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private FlashScreenCommand mMyCommand;

        public EventCommand_FlashScreen(FlashScreenCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;

            if (mMyCommand != null)
            {
                nudDuration.Value = (decimal)mMyCommand.Duration;
                nudIntensity.Value = (decimal)mMyCommand.Intensity;
            }

            cmbColor.Items.Clear();
            foreach (Color.ChatColor color in Enum.GetValues(typeof(Color.ChatColor)))
            {
                cmbColor.Items.Add(Globals.GetColorName(color));
            }

            cmbColor.SelectedIndex = cmbColor.Items.IndexOf(mMyCommand.FlashColor);
            if (cmbColor.SelectedIndex == -1)
            {
                cmbColor.SelectedIndex = 0;
            }

            mEventEditor = editor;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            mMyCommand.Duration = (float) nudDuration.Value;
            mMyCommand.Intensity = (float) nudIntensity.Value;
            mMyCommand.FlashColor = Color.FromName(cmbColor.Text);

            mEventEditor.FinishCommandEdit();
        }
    }
}
