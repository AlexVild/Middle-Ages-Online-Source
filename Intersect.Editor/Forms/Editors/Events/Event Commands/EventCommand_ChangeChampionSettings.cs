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
    public partial class EventCommand_ChangeChampionSettings : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeChampSettingsCommand mMyCommand;

        public EventCommand_ChangeChampionSettings(ChangeChampSettingsCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mEventEditor = editor;
            mMyCommand = refCommand;

            rdoDisable.Checked = mMyCommand?.DisableSpawns ?? true;
            rdoEnable.Checked = !mMyCommand?.DisableSpawns ?? false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.DisableSpawns = rdoDisable.Checked && !rdoEnable.Checked;
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
