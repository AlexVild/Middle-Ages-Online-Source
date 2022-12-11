using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.Utilities;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ChangeBestiary : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeBestiaryCommand mMyCommand;

        public EventCommand_ChangeBestiary(ChangeBestiaryCommand refCommand, FrmEvent editor)
        {
            mMyCommand = refCommand;
            mEventEditor = editor;

            InitializeComponent();

            InitEditor();
        }

        public void InitEditor()
        {
            cmbBeasts.Items.Clear();
            cmbUnlocks.Items.Clear();
            cmbBeasts.Items.AddRange(NpcBase.Names);
            cmbUnlocks.Items.AddRange(EnumExtensions.GetDescriptions(typeof(BestiaryUnlock)));

            if (mMyCommand.NpcId != default && mMyCommand.NpcId != Guid.Empty)
            {
                cmbBeasts.SelectedIndex = NpcBase.ListIndex(mMyCommand.NpcId);
            }

            if ((int)mMyCommand.UnlockType >= 0)
            {
                cmbUnlocks.SelectedIndex = (int)mMyCommand.UnlockType;
            }

            chkUnlock.Checked = mMyCommand.UnlockStatus;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBeasts.SelectedIndex >= 0) 
            { 
                mMyCommand.NpcId = NpcBase.IdFromList(cmbBeasts.SelectedIndex);
            }

            if (cmbUnlocks.SelectedIndex >= 0)
            {
                mMyCommand.UnlockType = (BestiaryUnlock)cmbUnlocks.SelectedIndex;
            }

            mMyCommand.UnlockStatus = chkUnlock.Checked;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
