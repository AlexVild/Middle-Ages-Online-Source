using DarkUI.Forms;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Timers;
using Intersect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmTimers : EditorForm
    {
        private List<string> mExpandedFolders = new List<string>();

        private List<string> mKnownFolders = new List<string>();

        private List<TimerDescriptor> mChanged = new List<TimerDescriptor>();

        private string mCopiedItem;

        private TimerDescriptor mEditorItem;

        public frmTimers()
        {
            ApplyHooks();
            InitializeComponent();
            InitLocalization();

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        #region Editor lifecycle
        public void InitEditor()
        {
            rdoPlayerTimers.Checked = true;

            // Fill in combo boxes (event boxes are filled on editor "_load" event)
            cmbCompletionBehavior.Items.AddRange(Strings.TimerEditor.CompletionTypes.Values.ToArray());
            cmbRepetitionType.Items.AddRange(Strings.TimerEditor.RepetitionOptions.Values.ToArray());

            RefreshEditorListItems();
        }

        public void RefreshEditorListItems()
        {
            // Get timers of the selected type
            var timers = TimerDescriptor.Lookup
                .OrderBy(p => p.Value?.Name)
                .Where(timer => ((TimerDescriptor)timer.Value)?.OwnerType == GetTimerTypeFromRadioSelection());

            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in timers)
            {
                var folder = ((TimerDescriptor)itm.Value).Folder;
                if (!string.IsNullOrEmpty(folder) && !mFolders.Contains(folder))
                {
                    mFolders.Add(folder);
                    if (!mKnownFolders.Contains(folder))
                    {
                        mKnownFolders.Add(folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            // Get & Sort timers for the list component
            var listItems = timers
                .Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key, new KeyValuePair<string, string>(((TimerDescriptor)pair.Value)?.Name
                    ?? DatabaseObject<TimerDescriptor>.Deleted, ((TimerDescriptor)pair.Value)?.Folder ?? "")))
                .ToArray();

            lstGameObjects.Repopulate(listItems, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void UpdateEditor()
        {
            if (mEditorItem != null)
            {
                pnlTimerSettings.Show();
                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                RefreshEditorListItems();
                UpdateControlAvailability();

                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }
            }
            else
            {
                pnlTimerSettings.Hide();
            }

            UpdateToolStripItems();
        }

        private void InitLocalization()
        {
            Name = Strings.TimerEditor.EditorName;

            toolStripItemNew.ToolTipText = Strings.TimerEditor.New;
            toolStripItemDelete.ToolTipText = Strings.TimerEditor.Delete;
            btnAlphabetical.ToolTipText = Strings.TimerEditor.SortAlphabetically;
            toolStripItemCopy.ToolTipText = Strings.TimerEditor.Copy;
            toolStripItemPaste.ToolTipText = Strings.TimerEditor.Paste;
            toolStripItemUndo.ToolTipText = Strings.TimerEditor.Undo;

            grpTimerOptions.Text = Strings.TimerEditor.TimerOptions;

            btnCancel.Text = Strings.TimerEditor.Cancel;
            btnSave.Text = Strings.TimerEditor.Save;
        }
        #endregion

        #region Folders
        public void ExpandFolder(string name)
        {
            mExpandedFolders.Add(name);
        }

        public void ClearExpandedFolders()
        {
            mExpandedFolders.Clear();
        }
        #endregion

        #region Toolstrip
        private void UpdateToolStripItems()
        {
            toolStripItemCopy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemPaste.Enabled = mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused;
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = TimerDescriptor.Get(id);
            UpdateEditor();
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.Timer);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.TimerEditor.DeleteCaption, Strings.TimerEditor.DeletePrompt,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    PacketSender.SendDeleteObject(mEditorItem);
                }
            }
        }

        private void btnAlphabetical_Click(object sender, EventArgs e)
        {
            btnAlphabetical.Checked = !btnAlphabetical.Checked;
            RefreshEditorListItems();
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                mCopiedItem = mEditorItem.JsonData;
                toolStripItemPaste.Enabled = true;
            }
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            if (mChanged.Contains(mEditorItem) && mEditorItem != null)
            {
                mEditorItem.RestoreBackup();
                UpdateEditor();
            }
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused)
            {
                mEditorItem.Load(mCopiedItem, true);

                // Go to the correct timer category after pasting
                SelectOwnerRadioFromEditorItem(mEditorItem);
                RefreshEditorListItems();
            }
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.TimerEditor.SearchPlaceHolder;
        }
        #endregion

        #region Helpers
        private void SelectOwnerRadioFromEditorItem(TimerDescriptor descriptor)
        {
            switch (descriptor?.OwnerType)
            {
                case TimerOwnerType.Player:
                    rdoPlayerTimers.Select();
                    break;
                case TimerOwnerType.Instance:
                    rdoInstanceTimers.Select();
                    break;
            }
        }

        public TimerOwnerType GetTimerTypeFromRadioSelection()
        {
            if (rdoPlayerTimers.Checked)
            {
                return TimerOwnerType.Player;
            }
            if (rdoInstanceTimers.Checked)
            {
                return TimerOwnerType.Instance;
            }

            return TimerOwnerType.Player;
        }
        #endregion

        #region Form Control Availability
        private void UpdateControlAvailability()
        {
            UpdateSettingControls();
            UpdateDisplayNameControls();
        }

        private void UpdateSettingControls()
        {
            lblTimeLimit.Text = mEditorItem.Type == TimerType.Scheduler ?
                Strings.TimerEditor.Interval :
                Strings.TimerEditor.TimeLimit;

            nudRepetitions.Enabled = cmbRepetitionType.SelectedIndex == (int)TimerRepetitionTypes.Repeat;
        }

        private void UpdateDisplayNameControls()
        {
            txtDisplayName.Enabled = !mEditorItem.Hidden;
        }
        #endregion

        private void rdoPlayerTimers_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEditorListItems();
        }

        private void rdoInstanceTimers_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEditorListItems();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshEditorListItems();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = Strings.TimerEditor.SearchPlaceHolder;
            }
        }

        private void frmTimers_Load(object sender, EventArgs e)
        {
            cmbCancelledEvent.Items.Clear();
            cmbCancelledEvent.Items.Add(Strings.General.none);
            cmbCancelledEvent.Items.AddRange(EventBase.Names);

            cmbExpirationEvent.Items.Clear();
            cmbExpirationEvent.Items.Add(Strings.General.none);
            cmbExpirationEvent.Items.AddRange(EventBase.Names);

            cmbCompletionEvent.Items.Clear();
            cmbCompletionEvent.Items.Add(Strings.General.none);
            cmbCompletionEvent.Items.AddRange(EventBase.Names);

            InitLocalization();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.TimerEditor.SearchPlaceHolder;
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Folder = cmbFolder.Text;
            InitEditor();
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.TimerEditor.FolderPrompt, Strings.TimerEditor.FolderPrompt, ref folderName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
            {
                if (!cmbFolder.Items.Contains(folderName))
                {
                    mEditorItem.Folder = folderName;
                    lstGameObjects.ExpandFolder(folderName);
                    InitEditor();
                    cmbFolder.Text = folderName;
                }
            }
        }


        enum TimerRepetitionTypes
        {
            NoRepeat,
            Repeat,
            Indefinite,
        }

        private void cmbRepetitionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbRepetitionType.SelectedIndex)
            {
                case (int) TimerRepetitionTypes.NoRepeat:
                    mEditorItem.Repetitions = -1;
                    break;
                case (int)TimerRepetitionTypes.Repeat:
                    mEditorItem.Repetitions = (int)nudRepetitions.Value;
                    break;
                case (int)TimerRepetitionTypes.Indefinite:
                    mEditorItem.Repetitions = int.MinValue;
                    break;
            }

            UpdateSettingControls();
        }

        private void rdoScheduler_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Type = TimerType.Scheduler;
            UpdateSettingControls();
        }

        private void rdoStopwatch_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Type = TimerType.Stopwatch;
            UpdateSettingControls();
        }

        private void rdoCountdown_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Type = TimerType.Countdown;
            UpdateSettingControls();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
        }

        private void chkHidden_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Hidden = chkHidden.Checked;
            UpdateDisplayNameControls();
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtDisplayName.Text;
        }

        private void cmbCancelledEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CancellationEventId = EventBase.IdFromList(cmbCancelledEvent.SelectedIndex + 1);
        }

        private void cmbExpirationEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ExpirationEventId = EventBase.IdFromList(cmbExpirationEvent.SelectedIndex + 1);
        }

        private void cmbCompletionEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CompletionEventId = EventBase.IdFromList(cmbCompletionEvent.SelectedIndex + 1);
        }

        private void chkContinueTimeout_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ContinueOnTimeout = chkContinueTimeout.Checked;
        }

        private void cmbCompletionBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CompletionBehavior = (TimerCompletionBehavior)cmbCompletionBehavior.SelectedIndex;
        }

        private void nudRepetitions_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Repetitions = (int)nudRepetitions.Value;
        }

        private void nudTimeLimit_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.TimeLimit = (int)nudTimeLimit.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Send Changed items
            foreach (var item in mChanged)
            {
                PacketSender.SendSaveObject(item);
                item.DeleteBackup();
            }

            Hide();
            Globals.CurrentEditor = -1;
            Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (var item in mChanged)
            {
                item.RestoreBackup();
                item.DeleteBackup();
            }

            Hide();
            Globals.CurrentEditor = -1;
            Dispose();
        }
    }
}
