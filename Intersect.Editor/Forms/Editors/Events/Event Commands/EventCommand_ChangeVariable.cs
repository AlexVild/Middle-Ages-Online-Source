using Intersect.Editor.Utilities;
using Intersect.Enums;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.Utilities;
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
    public partial class EventCommand_ChangeVariable : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private SetVariableCommand mMyCommand;

        private bool mLoading;

        private VariableTypes mSelectedVariableType = VariableTypes.PlayerVariable;
        private Guid mSelectedVariableId = Guid.Empty;

        private VariableTypes mSettingVariableType = VariableTypes.PlayerVariable;
        private Guid mSettingVariableId = Guid.Empty;

        private VariableDataTypes VariableTypeFilter => mSelectedVariableId != Guid.Empty ? mSelectedVariableType.GetRelatedTable().GetVariableType(mSelectedVariableId) : 0;

        private List<VariableOperators> CurrentOperators = new List<VariableOperators>();

        public EventCommand_ChangeVariable(SetVariableCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            mLoading = true;

            mSelectedVariableId = refCommand.VariableId;
            mSelectedVariableType = refCommand.VariableType;

            mLoading = false;
        }

        public void InitEditor()
        {
            chkSyncParty.Checked = mMyCommand.SyncParty;
            chkInstanceSync.Checked = mMyCommand.InstanceSync;
        }

        public void UpdateForm()
        {
            grpIntegerSet.Hide();

            UpdateOperators(VariableTypeFilter);
            TryLoadVariableMod(mMyCommand.Modification);

            switch (VariableTypeFilter)
            {
                case VariableDataTypes.Number:
                case VariableDataTypes.Integer:
                    grpIntegerSet.Show();
                    break;

                case VariableDataTypes.Boolean:
                    grpIntegerSet.Show();
                    break;

                case VariableDataTypes.String:
                    grpIntegerSet.Show();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateOperators(VariableDataTypes dataType)
        {
            CurrentOperators.Clear();
            switch (dataType)
            {
                case VariableDataTypes.Number:
                case VariableDataTypes.Integer:
                    cmbOperator.Items.Add(EnumExtensions.GetDescriptions(typeof(VariableOperators), "Replace"));
                    break;

                case VariableDataTypes.Boolean:
                    cmbOperator.Items.Add(VariableOperators.Set);
                    break;

                case VariableDataTypes.String:
                    cmbOperator.Items.Add(VariableOperators.Set);
                    cmbOperator.Items.Add(VariableOperators.Replace);
                    break;
            }
            cmbOperator.Items.Clear();
            cmbOperator.Items.Add(EnumExtensions.GetDescriptions(typeof(VariableOperators)));
        }

        private void TryLoadVariableMod(VariableMod variableMod)
        {
            if (variableMod == null)
            {
                variableMod = new IntegerVariableMod();
            }

            

            if (variableMod is IntegerVariableMod integerMod)
            {
                TryLoadIntegerMod(integerMod);
            }
        }

        private void TryLoadIntegerMod(IntegerVariableMod integerMod)
        {
            SetOperator(integerMod.ModType);
            if (integerMod.DuplicateVariableId != Guid.Empty)
            {

            }
            else
            {
                nudValue.Value = integerMod.Value;
            }
        }

        private void SetOperator(VariableMods mod)
        {
            if (VariableModUtils.SetMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.Set);
                return;
            }

            if (VariableModUtils.AddMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.Add);
                return;
            }

            if (VariableModUtils.SubMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.Subtract);
                return;
            }

            if (VariableModUtils.MultMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.Multiply);
                return;
            }

            if (VariableModUtils.DivideMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.Divide);
                return;
            }

            if (VariableModUtils.RShiftMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.RShift);
                return;
            }

            if (VariableModUtils.LShiftMods.Contains(mod))
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.LShift);
                return;
            }

            if (mod == VariableMods.Replace)
            {
                cmbOperator.SelectedValue = CurrentOperators.FindIndex(v => v == VariableOperators.Replace);
                return;
            }

            throw new ArgumentException("No valid operator was given!");
        }
    } 
}
