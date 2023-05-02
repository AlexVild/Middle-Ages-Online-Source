using Intersect.GameObjects;
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
    public partial class EventCommand_RemovePermabuff : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private RemovePermabuffCommand mMyCommand;

        public EventCommand_RemovePermabuff(RemovePermabuffCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mMyCommand = refCommand;
            mEventEditor = editor;

            cmbItem.Items.AddRange(ItemBase.Names);
            cmbItem.SelectedIndex = ItemBase.ListIndex(mMyCommand.ItemId);
        }

        public EventCommand_RemovePermabuff()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.ItemId = ItemBase.IdFromList(cmbItem.SelectedIndex);
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
