using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Intersect.Editor.Localization;
using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.QuestList;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandRandomQuest : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private RandomQuestCommand mMyCommand;

        public EventCommandRandomQuest(RandomQuestCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            InitLocalization();
            cmbQuestLists.Items.Clear();
            cmbQuestLists.Items.AddRange(QuestListBase.Names);
            cmbQuestLists.SelectedIndex = QuestBase.ListIndex(refCommand.QuestListId);
        }

        private void InitLocalization()
        {
            grpRandomQuest.Text = Strings.EventRandomQuest.title;
            lblQuestList.Text = Strings.EventRandomQuest.questlist;
            btnSave.Text = Strings.EventRandomQuest.okay;
            btnCancel.Text = Strings.EventRandomQuest.cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.QuestListId = QuestListBase.IdFromList(cmbQuestLists.SelectedIndex);

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
