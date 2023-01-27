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
    public partial class EventCommand_ChangeChallenge : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeChallengeCommand mMyCommand;

        public EventCommand_ChangeChallenge(ChangeChallengeCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mEventEditor = editor;
            mMyCommand = refCommand;

            cmbWeaponType.Items.Clear();
            cmbWeaponType.Items.AddRange(ChallengeDescriptor.Names);
            cmbWeaponType.SelectedIndex = ChallengeDescriptor.ListIndex(mMyCommand?.ChallengeId ?? Guid.Empty);

            SetRadioValues();

            nudAmt.Value = mMyCommand.Amount;
        }

        void SetRadioValues()
        {
            var changeType = mMyCommand?.ChangeType ?? ChallengeUpdate.ChangeReps;

            rdoChangeReps.Checked = changeType == ChallengeUpdate.ChangeReps;
            rdoComplete.Checked = changeType == ChallengeUpdate.Complete;
            rdoReset.Checked = changeType == ChallengeUpdate.Reset;
        }

        ChallengeUpdate GetRadioValue()
        {
            if (rdoChangeReps.Checked)
            {
                return ChallengeUpdate.ChangeReps;
            }

            if (rdoComplete.Checked)
            {
                return ChallengeUpdate.Complete;
            }

            if (rdoReset.Checked)
            {
                return ChallengeUpdate.Reset;
            }

            return ChallengeUpdate.ChangeReps;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.ChallengeId = ChallengeDescriptor.IdFromList(cmbWeaponType.SelectedIndex);
            mMyCommand.ChangeType = GetRadioValue();
            mMyCommand.Amount = (int)nudAmt.Value;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
