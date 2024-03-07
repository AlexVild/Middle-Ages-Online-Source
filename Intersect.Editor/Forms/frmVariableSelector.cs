using System;
using System.Linq;
using System.Windows.Forms;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.Extensions;

namespace Intersect.Editor.Forms
{
    public partial class FrmVariableSelector : Form
    {
        private Guid mSelectedVariableId { get; set; }

        private VariableTypes mSelectedVariableTypes { get; set; }

        private bool mResult { get; set; }

        private bool mPopulating { get; set; }

        private VariableDataTypes mFilterType { get; set; }

        private VariableSelection mSelection { get; set; }

        public FrmVariableSelector(VariableTypes VariableTypes, Guid variableId, VariableDataTypes filterType)
        {
            PreInit();

            mSelectedVariableId = variableId;
            mSelectedVariableTypes = VariableTypes;
            mFilterType = filterType;

            PostInit();
        }

        public FrmVariableSelector()
        {
            PreInit();
            PostInit();
        }

        private void PreInit()
        {
            InitializeComponent();
        }

        private void PostInit()
        {
            mPopulating = true;

            InitLocalization();

            PopulateForm();
        }

        private void InitLocalization()
        {
            Text = Strings.VariableSelector.Title;

            grpSelection.Text = Strings.VariableSelector.LabelGroup;
            grpVariableType.Text = Strings.VariableSelector.LabelVariableType;
            grpVariable.Text = Strings.VariableSelector.LabelVariableValue;

            cmbVariableType.Items.Clear();

            cmbVariableType.Items.AddRange(Strings.VariableSelector.VariableTypes.Values.ToArray());
        }

        private void PopulateForm()
        {
            cmbVariableType.SelectedIndex = (int)mSelectedVariableTypes;

            ReloadVariablesOf(mSelectedVariableTypes);

            cmbVariables.SelectedIndex = mSelectedVariableTypes.GetVariableTable().ListIndex(mSelectedVariableId, mFilterType);

            mPopulating = false;
        }

        private void ReloadVariablesOf(VariableTypes type)
        {
            cmbVariables.Items.Clear();

            cmbVariables.Items.AddRange(type.GetVariableTable().Names(mFilterType));
        }

        private void cmbVariableTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            mSelectedVariableTypes = (VariableTypes)cmbVariableType.SelectedIndex;
            ReloadVariablesOf(mSelectedVariableTypes);

            // Force reset the variable selection
            if (cmbVariables.Items.Count > 0)
            {
                cmbVariables.SelectedIndex = 0;
                mSelectedVariableId = mSelectedVariableTypes.GetVariableTable().IdFromList(0, mFilterType);
            }
            else
            {
                cmbVariables.SelectedIndex = -1;
                mSelectedVariableId = Guid.Empty;
            }
        }

        public bool GetResult()
        {
            return mResult;
        }

        public VariableSelection GetSelection()
        {
            return mSelection;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            mResult = true;
            mSelection = new VariableSelection(mSelectedVariableTypes, mSelectedVariableId);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            mSelectedVariableId = mSelectedVariableTypes.GetVariableTable().IdFromList(cmbVariables.SelectedIndex, mFilterType);
        }
    }

    public class VariableSelection
    {
        public VariableSelection(VariableTypes variableTypes, Guid variableId)
        {
            VariableType = variableTypes;
            VariableId = variableId;
        }

        public VariableTypes VariableType { get; set; }

        public Guid VariableId { get; set; }
    }
}