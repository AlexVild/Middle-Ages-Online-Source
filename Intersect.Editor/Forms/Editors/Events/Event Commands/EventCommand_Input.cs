using Intersect.Editor.Localization;
using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Models;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{

    public partial class EventCommandInput : UserControl
    {

        private readonly FrmEvent mEventEditor;

        bool mLoading = false;

        private InputVariableCommand mMyCommand;

        public EventCommandInput(InputVariableCommand refCommand, FrmEvent editor)
        {
            mLoading = true;
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;

            txtText.Text = mMyCommand.Text;
            txtTitle.Text = mMyCommand.Title;
            nudMaxVal.Value = mMyCommand.Maximum;
            nudMinVal.Value = mMyCommand.Minimum;

            if (mMyCommand.VariableType == Enums.VariableTypes.PlayerVariable)
            {
                rdoPlayerVariables.Checked = true;
            }
            else if (mMyCommand.VariableType == Enums.VariableTypes.ServerVariable)
            {
                rdoGlobalVariables.Checked = true;
            }
            else if (mMyCommand.VariableType == Enums.VariableTypes.InstanceVariable)
            {
                rdoInstance.Checked = true;
            }
            else if (mMyCommand.VariableType == Enums.VariableTypes.GuildVariable)
            {
                rdoGuild.Checked = true;
            }

            LoadVariableList();
            InitLocalization();
            mLoading = false;
        }

        private void InitLocalization()
        {
            grpInput.Text = Strings.EventInput.title;
            lblText.Text = Strings.EventInput.text;
            lblTitle.Text = Strings.EventInput.titlestr;
            lblCommands.Text = Strings.EventInput.commands;
            btnSave.Text = Strings.EventInput.okay;
            btnCancel.Text = Strings.EventInput.cancel;
            rdoPlayerVariables.Text = Strings.EventInput.playervariable;
            rdoGlobalVariables.Text = Strings.EventInput.globalvariable;
        }

        private void LoadVariableList()
        {
            cmbVariable.Items.Clear();
            if (rdoPlayerVariables.Checked)
            {
                cmbVariable.Items.AddRange(PlayerVariableBase.Names);
                cmbVariable.SelectedIndex = PlayerVariableBase.ListIndex(mMyCommand.VariableId);

                if (cmbVariable.SelectedIndex != -1)
                {
                    UpdateMinMaxValues(
                        GetSelectedVariable().Type
                    );
                }
            }
            else if (rdoGlobalVariables.Checked)
            {
                cmbVariable.Items.AddRange(ServerVariableBase.Names);
                cmbVariable.SelectedIndex = ServerVariableBase.ListIndex(mMyCommand.VariableId);

                if (cmbVariable.SelectedIndex != -1)
                {
                    UpdateMinMaxValues(
                        GetSelectedVariable().Type
                    );
                }
            }
            else if (rdoInstance.Checked)
            {
                cmbVariable.Items.AddRange(InstanceVariableBase.Names);
                cmbVariable.SelectedIndex = InstanceVariableBase.ListIndex(mMyCommand.VariableId);

                if (cmbVariable.SelectedIndex != -1)
                {
                    UpdateMinMaxValues(
                        GetSelectedVariable().Type
                    );
                }
            }
            else if (rdoGuild.Checked)
            {
                cmbVariable.Items.AddRange(GuildVariableBase.Names);
                cmbVariable.SelectedIndex = GuildVariableBase.ListIndex(mMyCommand.VariableId);

                if (cmbVariable.SelectedIndex != -1)
                {
                    UpdateMinMaxValues(
                        GetSelectedVariable().Type
                    );
                }
            }
        }

        private void UpdateMinMaxValues(Enums.VariableDataTypes type)
        {
            lblMaxVal.Show();
            lblMinVal.Show();
            nudMaxVal.Show();
            nudMinVal.Show();

            switch (type)
            {
                case Enums.VariableDataTypes.Integer:
                case Enums.VariableDataTypes.Number:
                    lblMinVal.Text = Strings.EventInput.minval;
                    lblMaxVal.Text = Strings.EventInput.maxval;

                    break;
                case Enums.VariableDataTypes.String:
                    lblMinVal.Text = Strings.EventInput.minlength;
                    lblMaxVal.Text = Strings.EventInput.maxlength;

                    break;
                case Enums.VariableDataTypes.Boolean:
                    lblMaxVal.Hide();
                    lblMinVal.Hide();
                    nudMaxVal.Hide();
                    nudMinVal.Hide();

                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Text = txtText.Text;
            mMyCommand.Title = txtTitle.Text;
            var maxVal = (int)nudMaxVal.Value == 0 ? int.MaxValue : (int)nudMaxVal.Value;
            mMyCommand.Maximum = maxVal;
            mMyCommand.Minimum = (int) nudMinVal.Value;

            if (rdoPlayerVariables.Checked)
            {
                mMyCommand.VariableType = Enums.VariableTypes.PlayerVariable;
                mMyCommand.VariableId = PlayerVariableBase.IdFromList(cmbVariable.SelectedIndex);
            }
            else if (rdoGlobalVariables.Checked)
            {
                mMyCommand.VariableType = Enums.VariableTypes.ServerVariable;
                mMyCommand.VariableId = ServerVariableBase.IdFromList(cmbVariable.SelectedIndex);
            }
            else if (rdoInstance.Checked)
            {
                mMyCommand.VariableType = Enums.VariableTypes.InstanceVariable;
                mMyCommand.VariableId = InstanceVariableBase.IdFromList(cmbVariable.SelectedIndex);
            }
            else if (rdoGuild.Checked)
            {
                mMyCommand.VariableType = Enums.VariableTypes.InstanceVariable;
                mMyCommand.VariableId = GuildVariableBase.IdFromList(cmbVariable.SelectedIndex);
            }

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void lblCommands_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                "http://www.ascensiongamedev.com/community/topic/749-event-text-variables/"
            );
        }

        private void ChangeVariableType()
        {
            LoadVariableList();
            if (!mLoading && cmbVariable.Items.Count > 0)
            {
                cmbVariable.SelectedIndex = 0;
            }
        }

        private void rdoGlobalVariables_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVariableType();
        }

        private void rdoPlayerVariables_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVariableType();
        }

        private void cmbVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMinMaxValues(GetSelectedVariable().Type);
        }

        private IVariableBase GetSelectedVariable()
        {
            if (rdoPlayerVariables.Checked)
            {
                return PlayerVariableBase.Get(PlayerVariableBase.IdFromList(cmbVariable.SelectedIndex));
            }
            else if (rdoGlobalVariables.Checked)
            {
                return ServerVariableBase.Get(ServerVariableBase.IdFromList(cmbVariable.SelectedIndex));
            }
            else if (rdoInstance.Checked)
            {
                return InstanceVariableBase.Get(InstanceVariableBase.IdFromList(cmbVariable.SelectedIndex));
            }
            else
            {
                return GuildVariableBase.Get(GuildVariableBase.IdFromList(cmbVariable.SelectedIndex));
            }
        }

        private void rdoInstance_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVariableType();
        }

        private void rdoGuild_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVariableType();
        }
    }

}
