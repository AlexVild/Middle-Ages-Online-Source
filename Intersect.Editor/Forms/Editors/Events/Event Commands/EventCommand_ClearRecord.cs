using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ClearRecord : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ClearRecordCommand mMyCommand;

        private bool IsLoading;

        public EventCommand_ClearRecord(ClearRecordCommand refCommand, FrmEvent editor)
        {
            IsLoading = true;
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            InitEditor();
            IsLoading = false;
        }

        private void InitEditor()
        {
            cmbType.Items.AddRange(Enum.GetNames(typeof(RecordType)));
            cmbType.SelectedIndex = (int)mMyCommand.RecordType < Enum.GetNames(typeof(RecordType)).Length ?
                (int)mMyCommand.RecordType :
                0;

            UpdateValues();
            PopulateValue();
        }

        private void UpdateValues()
        {
            cmbValue.Items.Clear();
            cmbValue.Enabled = true;
            var recordType = (RecordType)cmbType.SelectedIndex;
            switch (recordType)
            {
                case RecordType.NpcKilled:
                    cmbValue.Items.AddRange(NpcBase.Names);
                    break;
                case RecordType.ItemCrafted:
                    cmbValue.Items.AddRange(ItemBase.Names);
                    break;
                case RecordType.ResourceGathered:
                    cmbValue.Items.AddRange(ResourceBase.Names);
                    break;
                case RecordType.PlayerVariable:
                    cmbValue.Items.AddRange(PlayerVariableBase.GetNamesByType(VariableDataTypes.Integer));
                    break;
                case RecordType.Combo:
                    cmbValue.Enabled = false;
                    break;
                default:
                    break;
            }

            if (IsLoading)
            {
                return;
            }
            if (cmbValue.Items.Count > 0)
            {
                cmbValue.SelectedIndex = 0;
            }
            PopulateCommandValue();
        }

        private void PopulateValue()
        {
            var recordType = (RecordType)cmbType.SelectedIndex;
            switch (recordType)
            {
                case RecordType.NpcKilled:
                    cmbValue.SelectedIndex = NpcBase.ListIndex(mMyCommand.RecordId);
                    break;
                case RecordType.ItemCrafted:
                    cmbValue.SelectedIndex = ItemBase.ListIndex(mMyCommand.RecordId);
                    break;
                case RecordType.ResourceGathered:
                    cmbValue.SelectedIndex = ResourceBase.ListIndex(mMyCommand.RecordId);
                    break;
                case RecordType.PlayerVariable:
                    cmbValue.SelectedIndex = PlayerVariableBase.ListIndex(mMyCommand.RecordId, VariableDataTypes.Integer);
                    break;
                case RecordType.Combo:
                    cmbValue.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void PopulateCommandValue()
        {
            var recordType = (RecordType)cmbType.SelectedIndex;
            switch (recordType)
            {
                case RecordType.NpcKilled:
                    mMyCommand.RecordId = NpcBase.IdFromList(cmbValue.SelectedIndex);
                    break;
                case RecordType.ItemCrafted:
                    mMyCommand.RecordId = ItemBase.IdFromList(cmbValue.SelectedIndex);
                    break;
                case RecordType.ResourceGathered:
                    mMyCommand.RecordId = ResourceBase.IdFromList(cmbValue.SelectedIndex);
                    break;
                case RecordType.PlayerVariable:
                    mMyCommand.RecordId = PlayerVariableBase.IdFromList(cmbValue.SelectedIndex, VariableDataTypes.Integer);
                    break;
                case RecordType.Combo:
                    mMyCommand.RecordId = Guid.Empty;
                    break;
                default:
                    mMyCommand.RecordId = Guid.Empty;
                    break;
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mMyCommand.RecordType = (RecordType)cmbType.SelectedIndex;
            UpdateValues();
        }

        private void cmbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsLoading)
            {
                return;
            }
            PopulateCommandValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
