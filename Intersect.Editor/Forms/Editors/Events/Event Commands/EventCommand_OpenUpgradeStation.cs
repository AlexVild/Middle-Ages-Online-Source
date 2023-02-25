using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_OpenUpgradeStation : UserControl
    {
        FrmEvent mMyEditor { get; set; }

        OpenUpgradeStation mMyCommand { get; set; }

        public EventCommand_OpenUpgradeStation(OpenUpgradeStation refCommand, FrmEvent eventEditor)
        {
            mMyCommand = refCommand;
            mMyEditor = eventEditor;

            InitializeComponent();

            cmbItems.Items.Clear();
            cmbItems.Items.AddRange(ItemBase.Names);

            cmbItems.SelectedIndex = ItemBase.ListIndex(mMyCommand.CurrencyItemId);
            nudMultiplier.Value = (decimal)mMyCommand.CostMultiplier;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.CostMultiplier = (float)nudMultiplier.Value;
            mMyCommand.CurrencyItemId = ItemBase.IdFromList(cmbItems.SelectedIndex);

            mMyEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mMyEditor.CancelCommandEdit();
        }
    }
}
