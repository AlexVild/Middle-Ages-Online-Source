using Intersect.GameObjects;
using Intersect.GameObjects.Events;
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
    public partial class EventCommand_ChangeDungeon : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeDungeonCommand mMyCommand;

        public EventCommand_ChangeDungeon(ChangeDungeonCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mEventEditor = editor;
            mMyCommand = refCommand;

            cmbState.Items.Clear();
            cmbState.Items.AddRange(Enum.GetNames(typeof(DungeonState)));
            cmbState.SelectedIndex = (int)(mMyCommand?.State ?? 0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.State = (DungeonState)cmbState.SelectedIndex;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
