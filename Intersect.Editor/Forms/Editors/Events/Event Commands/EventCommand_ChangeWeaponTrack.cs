using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ChangeWeaponTrack : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeWeaponTrackCommand mMyCommand;

        public EventCommand_ChangeWeaponTrack(ChangeWeaponTrackCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            cmbWeaponType.Items.Clear();
            cmbWeaponType.Items.AddRange(WeaponTypeDescriptor.Names);
            cmbWeaponType.SelectedIndex = WeaponTypeDescriptor.ListIndex(mMyCommand?.WeaponTypeId ?? Guid.Empty);

            SetRadioValues();

            nudAmt.Value = mMyCommand.Amount;
        }

        void SetRadioValues()
        {
            var changeType = mMyCommand?.ChangeType ?? WeaponTrackUpdate.SetLevel;
            
            rdoGainExp.Checked = changeType == WeaponTrackUpdate.ChangeExp;
            rdoGainLevel.Checked = changeType == WeaponTrackUpdate.GainLevel;
            rdoLoseLevel.Checked = changeType == WeaponTrackUpdate.LoseLevel;
            rdoSetLevel.Checked = changeType == WeaponTrackUpdate.SetLevel;
            rdoUnlearn.Checked = changeType == WeaponTrackUpdate.Unlearn;
        }

        WeaponTrackUpdate GetRadioValue()
        {
            if (rdoGainExp.Checked)
            {
                return WeaponTrackUpdate.ChangeExp;
            }

            if (rdoGainLevel.Checked)
            {
                return WeaponTrackUpdate.GainLevel;
            }

            if (rdoLoseLevel.Checked)
            {
                return WeaponTrackUpdate.LoseLevel;
            }

            if (rdoSetLevel.Checked)
            {
                return WeaponTrackUpdate.SetLevel;
            }

            if (rdoUnlearn.Checked)
            {
                return WeaponTrackUpdate.Unlearn;
            }

            return WeaponTrackUpdate.SetLevel;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.WeaponTypeId = WeaponTypeDescriptor.IdFromList(cmbWeaponType.SelectedIndex);
            mMyCommand.ChangeType = GetRadioValue();
            mMyCommand.Amount = (long)nudAmt.Value;

            mEventEditor.FinishCommandEdit();
        }
    }
}
