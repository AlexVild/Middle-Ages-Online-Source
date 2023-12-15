using DarkUI.Forms;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Maps.MapList;
using Intersect.GameObjects.Timers;
using Intersect.Models;
using Intersect.Utilities;
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

        private bool mPopulating = false;

        private const int mMinimumIntervalScheduler = 5;
        
        private const int mMinimumIntervalOther = 1;

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
            // Fill in combo boxes (event boxes are filled on editor "_load" event)
            cmbOwnerType.Items.AddRange(Strings.TimerEditor.OwnerTypes.Values.ToArray());
            if (cmbOwnerType.Items.Count > 0)
            {
                cmbOwnerType.SelectedIndex = 0;
            }
            cmbTimerType.Items.AddRange(Strings.TimerEditor.TimerTypes.Values.ToArray());
            cmbRepetitionType.Items.AddRange(Strings.TimerEditor.RepetitionOptions.Values.ToArray());
            cmbLogoutBehavior.Items.AddRange(Strings.TimerEditor.LogoutBehaviors.Values.ToArray());
            cmbCompletionBehavior.Items.AddRange(Strings.TimerEditor.CompletionTypes.Values.ToArray());

            PopulateEditor();
            RefreshEditorListItems();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.Timer)
            {
                RefreshEditorListItems();
                mEditorItem = null;
                PopulateEditor();
            }
        }

        public void RefreshEditorListItems()
        {
            // Get timers of the selected type
            var timers = TimerDescriptor.Lookup
                .OrderBy(p => p.Value?.Name).ToList()
                .Where(timer => ((TimerDescriptor)timer.Value)?.OwnerType == (TimerOwnerType)cmbOwnerType.SelectedIndex);

            //Collect folders
            var mFolders = new List<string>();
            mKnownFolders.Clear();
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
            cmbFolder.Items.Add(string.Empty);
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            // Get & Sort timers for the list component
            var listItems = timers
                .Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key, new KeyValuePair<string, string>(((TimerDescriptor)pair.Value)?.Name
                    ?? DatabaseObject<TimerDescriptor>.Deleted, ((TimerDescriptor)pair.Value)?.Folder ?? "")))
                .ToArray();

            lstGameObjects.Repopulate(listItems, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void PopulateEditor()
        {
            if (mEditorItem != null)
            {
                mPopulating = true;
                pnlTimerSettings.Show();
                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                PopulateEditorItemValues();
                UpdateControlAvailability();

                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }

                mPopulating = false;
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

            grpGeneral.Text = Strings.TimerEditor.General;
            lblName.Text = Strings.TimerEditor.Name;
            lblFolder.Text = Strings.TimerEditor.Folder;

            grpSettings.Text = Strings.TimerEditor.Settings;
            chkRunIndefinite.Text = Strings.TimerEditor.RunIndefinitely;
            lblRepeat.Text = Strings.TimerEditor.Repeat;
            lblRepetitions.Text = Strings.TimerEditor.Repetitions;

            grpDisplay.Text = Strings.TimerEditor.DisplaySettings;
            chkHidden.Text = Strings.TimerEditor.Hidden;
            AddToolTip(chkHidden, Strings.TimerEditor.HiddenToolTip);
            lblDisplayName.Text = Strings.TimerEditor.DisplayName;

            grpEvents.Text = Strings.TimerEditor.Events;
            lblCancelled.Text = Strings.TimerEditor.OnCancelled;
            lblExpiredEvent.Text = Strings.TimerEditor.OnExpired;
            lblOnCompletion.Text = Strings.TimerEditor.OnCompletion;

            grpTimerOptions.Text = Strings.TimerEditor.TimerOptions;
            chkContinue.Text = Strings.TimerEditor.ContinueAfterExpire;
            lblLogoutBehavior.Text = Strings.TimerEditor.LogoutBehavior;
            AddToolTip(lblLogoutBehavior, Strings.TimerEditor.LogoutBehaviorTooltip);
            lblCompletionBehavior.Text = Strings.TimerEditor.CompletionBehavior;
            lblVariable.Text = Strings.TimerEditor.VariableLabel;
            chkStartWithServer.Text = Strings.TimerEditor.StartWithServer;
            chkContinueOnDeath.Text = Strings.TimerEditor.ContinueOnDeath;
            AddToolTip(chkContinueOnDeath, Strings.TimerEditor.ContinueOnDeathTooltip);
            chkContinueOnInstanceChange.Text = Strings.TimerEditor.ContineuOnInstance;
            AddToolTip(chkContinueOnDeath, Strings.TimerEditor.ContineuOnInstanceTooltip);

            btnCancel.Text = Strings.TimerEditor.Cancel;
            btnSave.Text = Strings.TimerEditor.Save;

            _InitLocalization();
        }

        private void _InitLocalization()
        {
            cmbInstanceVar.Items.Clear();
            cmbInstanceVar.Items.Add(Strings.EditorGenerics.None);
            cmbInstanceVar.Items.AddRange(InstanceVariableBase.GetNamesByType(VariableDataTypes.Integer));

            cmbVarAction.Items.Clear();
            cmbVarAction.Items.AddRange(EnumExtensions.GetDescriptions(typeof(VarActionModType)));

            cmbMaps.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMaps.Items.Add(MapList.OrderedMaps[i].Name);
            }
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
            PopulateEditor();
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateTimer((TimerOwnerType) cmbOwnerType.SelectedIndex);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.TimerEditor.DeletePrompt, Strings.TimerEditor.DeleteCaption,
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
                PopulateEditor();
                RefreshEditorListItems();
            }
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused)
            {
                mEditorItem.Load(mCopiedItem, true);

                PopulateEditor();
                // Go to the correct timer category after pasting
                cmbOwnerType.SelectedIndex = (int)mEditorItem.OwnerType;
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
        private void AddToolTip(Control control, string caption)
        {
            ToolTip toolTip = new ToolTip();

            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(control, caption);
        }
        #endregion

        #region Form population
        private void PopulateEditorItemValues()
        {
            cmbOwnerType.SelectedIndex = (int)mEditorItem.OwnerType;

            txtName.Text = mEditorItem.Name;
            cmbFolder.Text = mEditorItem.Folder;

            chkRunIndefinite.Checked = mEditorItem.TimeLimit == TimerConstants.TimerIndefiniteTimeLimit;
            cmbTimerType.SelectedIndex = (int)mEditorItem.Type;
            nudTimeLimit.Value = mEditorItem.TimeLimit;
            
            cmbRepetitionType.SelectedIndex = (int)SelectRepetitionType(mEditorItem.Repetitions);
            if (mEditorItem.Repetitions >= 0)
            {
                nudRepetitions.Value = mEditorItem.Repetitions;
            }

            chkHidden.Checked = mEditorItem.Hidden;
            txtDisplayName.Text = mEditorItem.DisplayName;

            cmbCancelledEvent.SelectedIndex = EventBase.ListIndex(mEditorItem.CancellationEventId) + 1;
            cmbExpirationEvent.SelectedIndex = EventBase.ListIndex(mEditorItem.ExpirationEventId) + 1;
            cmbCompletionEvent.SelectedIndex = EventBase.ListIndex(mEditorItem.CompletionEventId) + 1;

            chkContinue.Checked = mEditorItem.ContinueAfterExpiration;
            if (mEditorItem.LogoutBehavior >= 0 && (int)mEditorItem.LogoutBehavior < cmbLogoutBehavior.Items.Count)
            {
                cmbLogoutBehavior.SelectedIndex = (int) mEditorItem.LogoutBehavior;
            }
            cmbCompletionBehavior.SelectedIndex = (int)mEditorItem.CompletionBehavior;

            PopulateVariableOptions(false);

            chkStartWithServer.Checked = mEditorItem.StartWithServer;
            chkContinueOnDeath.Checked = mEditorItem.ContinueOnDeath;
            chkContinueOnInstanceChange.Checked = mEditorItem.ContinueOnInstanceChange;

            _PopulateEditorItemValues();
        }

        private void _PopulateEditorItemValues()
        {
            chkActionsEnabled.Checked = mEditorItem.ActionsEnabled;
            grpInstanceControl.Enabled = mEditorItem.ActionsEnabled;

            rdoEveryNth.Checked = mEditorItem.ActionType == (int)TimerInstanceActionType.EVERY;
            rdoOnNth.Checked = mEditorItem.ActionType == (int)TimerInstanceActionType.ONCE;
            nudN.Value = (int)mEditorItem.NValue;

            cmbInstanceVar.SelectedIndex = InstanceVariableBase.ListIndex(mEditorItem.ActionVariableId, VariableDataTypes.Integer) + 1;
            cmbVarAction.SelectedIndex = mEditorItem.InstanceVariableActionType;
            nudVarVal.Value = mEditorItem.ActionVariableChangeValue;

            chkSinglePlayerCancel.Checked = mEditorItem.SinglePlayerCancellation;
            chkSinglePlayerExpire.Checked = mEditorItem.SinglePlayerExpire;
            chkSinglePlayerCompletion.Checked = mEditorItem.SinglePlayerCompletion;

            chkOnlyOnExclusiveMaps.Checked = mEditorItem.OnlyDisplayOnExclusiveMaps;

            PopulateExclusiveMapList();
        }

        private static TimerRepetitionTypes SelectRepetitionType(int repetitions)
        {
            switch (repetitions)
            {
                case TimerConstants.DoNotRepeat:
                    return TimerRepetitionTypes.NoRepeat;
                case TimerConstants.TimerIndefiniteRepeat:
                    return TimerRepetitionTypes.Indefinite;
                default:
                    return TimerRepetitionTypes.Repeat;
            }
        }

        private void PopulateVariableOptions(bool newSelection)
        {
            cmbVariable.Items.Clear();
            cmbVariable.Items.Add(Strings.General.none);
            int listIdx = 0;
            switch(mEditorItem.OwnerType)
            {
                case TimerOwnerType.Global:
                    cmbVariable.Items.AddRange(ServerVariableBase.GetNamesByType(VariableDataTypes.Integer));
                    listIdx = ServerVariableBase.ListIndex(mEditorItem.ElapsedTimeVariableId, VariableDataTypes.Integer) + 1;
                    break;
                case TimerOwnerType.Player:
                case TimerOwnerType.Party:
                    cmbVariable.Items.AddRange(PlayerVariableBase.GetNamesByType(VariableDataTypes.Integer));
                    listIdx = PlayerVariableBase.ListIndex(mEditorItem.ElapsedTimeVariableId, VariableDataTypes.Integer) + 1;
                    break;
                case TimerOwnerType.Instance:
                    cmbVariable.Items.AddRange(InstanceVariableBase.GetNamesByType(VariableDataTypes.Integer));
                    listIdx = InstanceVariableBase.ListIndex(mEditorItem.ElapsedTimeVariableId, VariableDataTypes.Integer) + 1;
                    break;
            }

            if (!newSelection && listIdx < cmbVariable.Items.Count)
            {
                cmbVariable.SelectedIndex = listIdx;
            }
            else
            {
                cmbVariable.SelectedIndex = 0;
            }
        }
        #endregion

        #region Form Control Availability
        private void UpdateControlAvailability()
        {
            UpdateSettingControls();
            UpdateDisplayNameControls();
            UpdateOptionControls();
        }

        private void UpdateSettingControls()
        {
            lblTimeLimit.Text = mEditorItem.Type == TimerType.Scheduler ?
                Strings.TimerEditor.Interval :
                Strings.TimerEditor.TimeLimit;

            chkRunIndefinite.Enabled = mEditorItem.Type != TimerType.Scheduler;
            nudTimeLimit.Enabled = !chkRunIndefinite.Checked;
            cmbRepetitionType.Enabled = !chkRunIndefinite.Checked;
            nudRepetitions.Enabled = cmbRepetitionType.SelectedIndex == (int)TimerRepetitionTypes.Repeat && !chkRunIndefinite.Checked;
        }

        private void UpdateDisplayNameControls()
        {
            txtDisplayName.Enabled = !mEditorItem.Hidden;
        }

        private void UpdateOptionControls()
        {
            chkContinue.Enabled = mEditorItem.Type != TimerType.Scheduler;
            cmbLogoutBehavior.Enabled = mEditorItem.OwnerType == TimerOwnerType.Player;
            chkStartWithServer.Enabled = mEditorItem.OwnerType == TimerOwnerType.Global;
            chkContinueOnDeath.Enabled = mEditorItem.OwnerType == TimerOwnerType.Player;
            chkContinueOnInstanceChange.Enabled = mEditorItem.OwnerType == TimerOwnerType.Player;
        }

        public void ChangeTimerOwnerType()
        {
            mEditorItem = null;
            PopulateEditor();
            RefreshEditorListItems();
        }
        #endregion

        private void rdoPlayerTimers_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTimerOwnerType();
        }

        private void rdoInstanceTimers_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTimerOwnerType();
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
            PopulateEditor();
            RefreshEditorListItems();
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
                    RefreshEditorListItems();
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
            if (mPopulating)
            {
                return;
            }

            switch(cmbRepetitionType.SelectedIndex)
            {
                case (int) TimerRepetitionTypes.NoRepeat:
                    mEditorItem.Repetitions = TimerConstants.DoNotRepeat;
                    break;
                case (int)TimerRepetitionTypes.Repeat:
                    mEditorItem.Repetitions = (int)nudRepetitions.Value;
                    break;
                case (int)TimerRepetitionTypes.Indefinite:
                    mEditorItem.Repetitions = TimerConstants.TimerIndefiniteRepeat;
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
            lstGameObjects.UpdateText(txtName.Text);
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
            mEditorItem.CancellationEventId = EventBase.IdFromList(cmbCancelledEvent.SelectedIndex - 1);
        }

        private void cmbExpirationEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ExpirationEventId = EventBase.IdFromList(cmbExpirationEvent.SelectedIndex - 1);
        }

        private void cmbCompletionEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CompletionEventId = EventBase.IdFromList(cmbCompletionEvent.SelectedIndex - 1);
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
            if (mPopulating)
            {
                return;
            }

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

        private void cmbTimerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Type = (TimerType) cmbTimerType.SelectedIndex;

            nudTimeLimit.Minimum = mEditorItem.Type == TimerType.Scheduler ?
                mMinimumIntervalScheduler :
                mMinimumIntervalOther;

            if (mEditorItem.Type == TimerType.Scheduler)
            {
                chkRunIndefinite.Checked = false;
                chkContinue.Checked = false;
            }
            UpdateSettingControls();
            UpdateOptionControls();
            PopulateVariableOptions(true);
        }

        private void cmbOwnerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeTimerOwnerType();
        }

        private void cmbLogoutBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.LogoutBehavior = (TimerLogoutBehavior) cmbLogoutBehavior.SelectedIndex;
        }

        private void chkRunIndefinite_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            mEditorItem.TimeLimit = chkRunIndefinite.Checked ?
                (long) TimerConstants.TimerIndefiniteTimeLimit :
                (long) nudTimeLimit.Value;

            UpdateSettingControls();
        }

        private void chkContinue_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ContinueAfterExpiration = chkContinue.Checked;
        }

        private void cmbVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            Guid selection = default;
            switch(mEditorItem.OwnerType)
            {
                case TimerOwnerType.Global:
                    selection = ServerVariableBase.IdFromList(cmbVariable.SelectedIndex - 1, VariableDataTypes.Integer);
                    break;
                case TimerOwnerType.Player:
                case TimerOwnerType.Party:
                    selection = PlayerVariableBase.IdFromList(cmbVariable.SelectedIndex - 1, VariableDataTypes.Integer);
                    break;
                case TimerOwnerType.Instance:
                    selection = InstanceVariableBase.IdFromList(cmbVariable.SelectedIndex - 1, VariableDataTypes.Integer);
                    break;
            }

            mEditorItem.ElapsedTimeVariableId = selection;
        }

        private void chkStartWithServer_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.StartWithServer = chkStartWithServer.Checked;
        }

        private void chkContinueOnDeath_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ContinueOnDeath = chkContinueOnDeath.Checked;
        }

        private void chkContinueOnInstanceChange_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ContinueOnInstanceChange = chkContinueOnInstanceChange.Checked;
        }

        private void chkActionsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ActionsEnabled = chkActionsEnabled.Checked;
            if (mEditorItem.NValue == 0)
            {
                mEditorItem.NValue = 1;
                nudN.Value = mEditorItem.NValue;
            }
            grpInstanceControl.Enabled = mEditorItem.ActionsEnabled;
        }

        private void rdoEveryNth_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ActionType = (int)TimerInstanceActionType.EVERY;
        }

        private void rdoOnNth_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.ActionType = (int)TimerInstanceActionType.ONCE;
        }

        private void nudN_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.NValue = (int)nudN.Value;
        }

        private void cmbInstanceVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ActionVariableId = InstanceVariableBase.IdFromList(cmbInstanceVar.SelectedIndex - 1, VariableDataTypes.Integer);
        }

        private void cmbVarAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.InstanceVariableActionType = cmbVarAction.SelectedIndex;
        }

        private void nudVarVal_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.ActionVariableChangeValue = (int)nudVarVal.Value;
        }

        private void chkSinglePlayerCancel_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.SinglePlayerCancellation = chkSinglePlayerCancel.Checked;
        }

        private void chkSinglePlayerExpire_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.SinglePlayerExpire = chkSinglePlayerExpire.Checked;
        }

        private void chkSinglePlayerCompletion_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.SinglePlayerCompletion = chkSinglePlayerCompletion.Checked;
        }

        private void PopulateExclusiveMapList()
        {
            lstExclusiveMaps.Items.Clear();
            lstExclusiveMaps.Items.AddRange(mEditorItem.ExclusiveMaps.Select(id =>
            {
                var map = MapList.OrderedMaps.Find(m => m.MapId == id);
                if (map == default)
                {
                    return "ERR_DELETED";
                }

                return map.Name;
            }).ToArray());
        }

        private void btnAddMap_Click(object sender, EventArgs e)
        {
            if (cmbMaps.SelectedIndex < 0 || cmbMaps.SelectedIndex >= MapList.OrderedMaps.Count)
            {
                return;
            }

            var mapId = MapList.OrderedMaps[cmbMaps.SelectedIndex].MapId;
            if (mEditorItem.ExclusiveMaps.Contains(mapId))
            {
                return;
            }

            mEditorItem.ExclusiveMaps.Add(mapId);

            PopulateExclusiveMapList();
        }

        private void btnRemoveMap_Click(object sender, EventArgs e)
        {
            var selectedIndex = lstExclusiveMaps.SelectedIndex;

            if (selectedIndex < 0 || selectedIndex >= mEditorItem.ExclusiveMaps.Count) 
            { 
                return; 
            }

            mEditorItem.ExclusiveMaps.RemoveAt(selectedIndex);

            PopulateExclusiveMapList();
        }

        private void chkOnlyOnExclusiveMaps_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.OnlyDisplayOnExclusiveMaps = chkOnlyOnExclusiveMaps.Checked;
        }
    }
}
