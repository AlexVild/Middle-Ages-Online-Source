using Intersect.GameObjects;
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
    public partial class EventCommand_ChangeEnhancements : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeEnhancementsCommand mMyCommand;

        public EventCommand_ChangeEnhancements(ChangeEnhancementsCommand refCommand, FrmEvent editor)
        {
            mMyCommand = refCommand;
            mEventEditor = editor;

            InitializeComponent();

            InitEditor();
        }

        public void InitEditor()
        {
            cmbEnhancements.Items.Clear();
            cmbEnhancements.Items.AddRange(EnhancementDescriptor.Names);

            if (mMyCommand?.EnhancementId != default)
            {
                cmbEnhancements.SelectedIndex = EnhancementDescriptor.ListIndex(mMyCommand.EnhancementId);
            }

            chkForget.Checked = mMyCommand?.Forget ?? false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.EnhancementId = EnhancementDescriptor.IdFromList(cmbEnhancements.SelectedIndex);
            mMyCommand.Forget = chkForget.Checked;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
