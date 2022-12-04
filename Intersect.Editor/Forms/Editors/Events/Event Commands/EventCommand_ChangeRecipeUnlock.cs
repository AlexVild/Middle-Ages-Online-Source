using Intersect.GameObjects;
using Intersect.GameObjects.Events.Commands;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ChangeRecipeUnlock : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ChangeRecipeStatusCommand mMyCommand;

        public EventCommand_ChangeRecipeUnlock(ChangeRecipeStatusCommand refCommand, FrmEvent eventEditor)
        {
            mMyCommand = refCommand;
            mEventEditor = eventEditor;

            InitializeComponent();

            InitEditor();
        }

        private void InitEditor()
        {
            cmbRecipes.Items.AddRange(RecipeDescriptor.Names);

            if (mMyCommand.RecipeId != Guid.Empty && mMyCommand.RecipeId != default)
            {
                cmbRecipes.SelectedIndex = RecipeDescriptor.ListIndex(mMyCommand.RecipeId);
            }

            rdoUnlock.Checked = mMyCommand.IsUnlocked;
            rdoLock.Checked = !mMyCommand.IsUnlocked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.RecipeId = RecipeDescriptor.IdFromList(cmbRecipes.SelectedIndex);
            mMyCommand.IsUnlocked = rdoUnlock.Checked;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
