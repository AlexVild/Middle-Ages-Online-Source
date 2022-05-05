using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Timers;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ModifyTimer : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ModifyTimerCommand mMyCommand;

        public EventCommand_ModifyTimer(ModifyTimerCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            InitLocalization();

            mMyCommand = refCommand;
            mEventEditor = editor;

            TimerCommandHelpers.InitializeSelectionFields(refCommand, ref cmbTimerType, ref cmbTimer);

            switch(mMyCommand.Operator)
            {
                case TimerOperator.Set:
                    rdoSet.Select();
                    break;
                case TimerOperator.Add:
                    rdoAdd.Select();
                    break;
                case TimerOperator.Subtract:
                    rdoSubtract.Select();
                    break;
            }

            nudSeconds.Value = mMyCommand.Amount;
            if (mMyCommand.IsStatic)
            {
                rdoStatic.Select();
            }
            else
            {
                rdoVariable.Select();
            }

            RefreshVariableSelection();
            if (!mMyCommand.IsStatic)
            {
                cmbVariableType.SelectedIndex = (int) mMyCommand.VariableType;
                FetchVariableFromCommandType();
            }

            UpdateElementAvailability();
        }

        private void InitLocalization()
        {
            grpModifyTimer.Text = Strings.EventModifyTimer.Title;

            grpTimer.Text = Strings.EventModifyTimer.TimerGroup;
            lblType.Text = Strings.EventModifyTimer.OwnerType;
            lblTimer.Text = Strings.EventModifyTimer.TimerSelect;

            grpMod.Text = Strings.EventModifyTimer.ModifyGroup;
            grpOperators.Text = Strings.EventModifyTimer.Operator;
            rdoSet.Text = Strings.EventModifyTimer.OperatorSet;
            rdoAdd.Text = Strings.EventModifyTimer.OperatorAdd;
            rdoSubtract.Text = Strings.EventModifyTimer.OperatorSub;

            rdoStatic.Text = Strings.EventModifyTimer.Static;
            rdoVariable.Text = Strings.EventModifyTimer.Variable;

            grpVariables.Text = Strings.EventModifyTimer.VariableSelection;
            lblVarType.Text = Strings.EventModifyTimer.VariableType;
            lblVarVal.Text = Strings.EventModifyTimer.VariableValue;

            cmbVariableType.Items.Clear();
            cmbVariableType.Items.AddRange(Strings.EventModifyTimer.VarTypes.Values.ToArray());

            btnSave.Text = Strings.EventModifyTimer.ButtonOkay;
            btnCancel.Text = Strings.EventModifyTimer.ButtonCancel;
        }

        private void UpdateElementAvailability()
        {
            grpVariables.Visible = !mMyCommand.IsStatic;
            nudSeconds.Enabled = mMyCommand.IsStatic;
        }

        private void rdoSet_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.Operator = TimerOperator.Set;
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.Operator = TimerOperator.Add;
        }

        private void rdoSubtract_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.Operator = TimerOperator.Subtract;
        }

        private void rdoStatic_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.IsStatic = true;
            UpdateElementAvailability();
        }

        private void rdoVariable_CheckedChanged(object sender, EventArgs e)
        {
            mMyCommand.IsStatic = false;
            UpdateElementAvailability();
        }

        private void cmbTimerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimerCommandHelpers.RefreshTimerSelector(ref cmbTimer, (TimerOwnerType)cmbTimerType.SelectedIndex);
        }

        private void cmbTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.DescriptorId = TimerDescriptor.IdFromList(cmbTimerType.SelectedIndex);
        }

        private void RefreshVariableSelection()
        {
            cmbVariable.Items.Clear();
            switch (mMyCommand.VariableType)
            {
                case VariableTypes.PlayerVariable:
                    cmbVariable.Items.AddRange(PlayerVariableBase.Names);
                    break;
                case VariableTypes.ServerVariable:
                    cmbVariable.Items.AddRange(ServerVariableBase.Names);
                    break;
                case VariableTypes.InstanceVariable:
                    cmbVariable.Items.AddRange(InstanceVariableBase.Names);
                    break;
            }
            if (cmbVariable.Items.Count > 0)
            {
                cmbVariable.SelectedIndex = 0;
            }
        }

        private void FetchVariableFromCommandType()
        {
            switch (mMyCommand.VariableType)
            {
                case VariableTypes.PlayerVariable:
                    mMyCommand.VariableDescriptorId = PlayerVariableBase.IdFromList(cmbVariable.SelectedIndex);
                    break;
                case VariableTypes.ServerVariable:
                    mMyCommand.VariableDescriptorId = ServerVariableBase.IdFromList(cmbVariable.SelectedIndex);
                    break;
                case VariableTypes.InstanceVariable:
                    mMyCommand.VariableDescriptorId = InstanceVariableBase.IdFromList(cmbVariable.SelectedIndex);
                    break;
            }
        }

        private void cmbVariableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.VariableType = (VariableTypes)cmbVariableType.SelectedIndex;

            RefreshVariableSelection();
        }

        private void cmbVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            FetchVariableFromCommandType();
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
            mMyCommand.Amount = (int)nudSeconds.Value;
        }
    }
}
