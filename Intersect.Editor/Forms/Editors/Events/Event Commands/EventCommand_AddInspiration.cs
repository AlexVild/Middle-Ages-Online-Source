using Intersect.GameObjects.Events.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_AddInspiration : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private AddInspirationCommand mMyCommand;

        public EventCommand_AddInspiration(AddInspirationCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            nudSeconds.Value = refCommand.Seconds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void nudSeconds_ValueChanged(object sender, EventArgs e)
        {
            mMyCommand.Seconds = (long) nudSeconds.Value;
        }
    }
}
