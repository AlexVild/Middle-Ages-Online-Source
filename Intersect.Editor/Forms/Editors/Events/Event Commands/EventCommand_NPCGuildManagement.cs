using Intersect.Editor.Localization;
using Intersect.GameObjects;
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
    public partial class EventCommand_NPCGuildManagement : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private EventPage mCurrentPage;

        private NPCGuildManagementCommand mMyCommand;

        public EventCommand_NPCGuildManagement(NPCGuildManagementCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            Load();

            cmbSelection.SelectedIndex = (int)refCommand.Selection;
            cmbClass.SelectedIndex = ClassBase.ListIndex(refCommand.ClassId);
            nudClassRank.Maximum = Options.MaxClassRank;
            nudClassRank.Value = MathHelper.Clamp(refCommand.NewRank, 0, Options.MaxClassRank);
            if (refCommand.SelectionValue)
            {
                cmbValue.SelectedIndex = 1;
            }
            else
            {
                cmbValue.SelectedIndex = 0;
            }
            
            UpdateVisibilities();
            InitLocalization();
        }

        private void InitLocalization()
        {
            grpManagement.Text = Strings.EventNPCGuildManagement.title;
            lblType.Text = Strings.EventNPCGuildManagement.type;
            lblClass.Text = Strings.EventNPCGuildManagement.cls;
            lblValue.Text = Strings.EventNPCGuildManagement.value;
            lblRank.Text = Strings.EventNPCGuildManagement.rank;
            btnSave.Text = Strings.EventNPCGuildManagement.confirm;
            btnCancel.Text = Strings.EventNPCGuildManagement.cancel;
        }

        private void Load()
        {
            cmbSelection.Items.Clear();

            foreach (var selection in Enum.GetValues(typeof(NPCGuildManagementSelection)))
            {
                cmbSelection.Items.Add(selection);
            }
            if (cmbSelection.Items.Count > 0 && mMyCommand.Selection == 0)
            {
                cmbSelection.SelectedIndex = 0;
            }

            cmbClass.Items.Clear();
            cmbClass.Items.AddRange(ClassBase.Names);
            if (cmbClass.Items.Count > 0 && mMyCommand.ClassId == Guid.Empty)
            {
                cmbClass.SelectedIndex = 0;
            }
        }

        private void UpdateVisibilities()
        {
            lblValue.Hide();
            lblRank.Hide();
            nudClassRank.Hide();
            cmbValue.Hide();
            switch(mMyCommand.Selection)
            {
                case NPCGuildManagementSelection.ChangeComplete:
                case NPCGuildManagementSelection.ChangeGuildStatus:
                case NPCGuildManagementSelection.ChangeSpecialAssignment:
                    lblValue.Show();
                    cmbValue.Show();
                    break;
                case NPCGuildManagementSelection.ChangeRank:
                    lblRank.Show();
                    nudClassRank.Show();
                    break;
                case NPCGuildManagementSelection.ClearCooldown:
                    // Intentionally blank
                    break;
            }
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.ClassId = ClassBase.IdFromList(cmbClass.SelectedIndex);
        }

        private void cmbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.SelectionValue = Convert.ToBoolean(cmbValue.SelectedIndex);
        }

        private void nudClassRank_ValueChanged(object sender, EventArgs e)
        {
            mMyCommand.NewRank = (int) nudClassRank.Value;
        }

        private void cmbSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.Selection = (NPCGuildManagementSelection) cmbSelection.SelectedIndex;
            UpdateVisibilities();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
