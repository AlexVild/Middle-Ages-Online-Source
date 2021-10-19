using System;
using System.Windows.Forms;

using Intersect.Editor.Localization;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.Events.Commands;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommandOpenQuestBoard : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private OpenQuestBoardCommand mMyCommand;

        public EventCommandOpenQuestBoard(OpenQuestBoardCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            InitLocalization();
            cmbQuestBoards.Items.Clear();
            cmbQuestBoards.Items.AddRange(QuestBoardBase.Names);
            cmbQuestBoards.SelectedIndex = QuestBase.ListIndex(refCommand.QuestBoardId);
        }
        private void InitLocalization()
        {
            grpOpenQuestBoard.Text = Strings.EventOpenQuestBoard.title;
            lblQuestBoard.Text = Strings.EventOpenQuestBoard.questboard;
            btnSave.Text = Strings.EventOpenQuestBoard.okay;
            btnCancel.Text = Strings.EventOpenQuestBoard.cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.QuestBoardId = QuestBoardBase.IdFromList(cmbQuestBoards.SelectedIndex);

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
