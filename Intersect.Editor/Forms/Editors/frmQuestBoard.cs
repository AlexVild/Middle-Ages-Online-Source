using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;
using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.QuestList;
using Intersect.Models;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmQuestBoard : EditorForm
    {
        private List<QuestBoardBase> mChanged = new List<QuestBoardBase>();

        private string mCopiedItem;

        private QuestBoardBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        public frmQuestBoard()
        {
            ApplyHooks();
            InitializeComponent();

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = QuestBoardBase.Get(id);
            UpdateEditor();
        }
        
        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.QuestBoard)
            {
                InitEditor();
                if (mEditorItem != null && !DatabaseObject<QuestBoardBase>.Lookup.Values.Contains(mEditorItem))
                {
                    mEditorItem = null;
                    UpdateEditor();
                }
            }
        }

        private void UpdateEditor()
        {
            if (mEditorItem != null)
            {
                pnlContainer.Show();

                txtName.Text = mEditorItem.Name;

                cmbFolder.Text = mEditorItem.Folder;

                UpdateList();

                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }
            }
            else
            {
                pnlContainer.Hide();
            }

            UpdateToolStripItems();
        }

        public void UpdateList()
        {
            lstQuestLists.Items.Clear();
            foreach (var id in mEditorItem.QuestLists)
            {
                lstQuestLists.Items.Add(QuestListBase.GetName(id));
            }
        }

        public override void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in QuestBoardBase.Lookup)
            {
                if (!string.IsNullOrEmpty(((QuestBoardBase)itm.Value).Folder) &&
                    !mFolders.Contains(((QuestBoardBase)itm.Value).Folder))
                {
                    mFolders.Add(((QuestBoardBase)itm.Value).Folder);
                    if (!mKnownFolders.Contains(((QuestBoardBase)itm.Value).Folder))
                    {
                        mKnownFolders.Add(((QuestBoardBase)itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            var items = QuestBoardBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((QuestBoardBase)pair.Value)?.Name ?? Models.DatabaseObject<QuestBoardBase>.Deleted, ((QuestBoardBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void InitLocalization()
        {
            Text = Strings.QuestBoardEditor.title;
            toolStripItemNew.Text = Strings.QuestBoardEditor.New;
            toolStripItemDelete.Text = Strings.QuestBoardEditor.delete;
            toolStripItemCopy.Text = Strings.QuestBoardEditor.copy;
            toolStripItemPaste.Text = Strings.QuestBoardEditor.paste;
            toolStripItemUndo.Text = Strings.QuestBoardEditor.undo;

            grpQuestBoards.Text = Strings.QuestBoardEditor.questboards;
            grpQuestLists.Text = Strings.QuestBoardEditor.questlists;

            lblAddQuestList.Text = Strings.QuestBoardEditor.addquestlistlabel;
            btnAddQuestList.Text = Strings.QuestBoardEditor.add;
            btnRemoveQuestList.Text = Strings.QuestBoardEditor.remove;

            grpGeneral.Text = Strings.QuestBoardEditor.general;
            lblName.Text = Strings.QuestBoardEditor.name;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.QuestBoardEditor.sortalphabetically;
            txtSearch.Text = Strings.QuestBoardEditor.searchplaceholder;
            lblFolder.Text = Strings.QuestBoardEditor.folderlabel;

            btnSave.Text = Strings.QuestBoardEditor.save;
            btnCancel.Text = Strings.QuestBoardEditor.cancel;
        }
        private void UpdateToolStripItems()
        {
            toolStripItemCopy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemPaste.Enabled = mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused;
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.QuestBoardEditor.searchplaceholder;
        }

        private void frmQuestBoard_Load(object sender, EventArgs e)
        {
            cmbQuestLists.Items.Clear();
            cmbQuestLists.Items.AddRange(QuestListBase.Names);

            InitLocalization();
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.QuestBoard);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.QuestBoardEditor.deleteprompt, Strings.QuestBoardEditor.delete,
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
            InitEditor();
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                mCopiedItem = mEditorItem.JsonData;
                toolStripItemPaste.Enabled = true;
            }
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused)
            {
                mEditorItem.Load(mCopiedItem, true);
                UpdateEditor();
            }
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            if (mChanged.Contains(mEditorItem) && mEditorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.QuestBoardEditor.undoprompt, Strings.QuestBoardEditor.undotitle,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    mEditorItem.RestoreBackup();
                    UpdateEditor();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            InitEditor();
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = Strings.QuestBoardEditor.searchplaceholder;
            }
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
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
                Strings.QuestBoardEditor.folderprompt, Strings.QuestBoardEditor.foldertitle, ref folderName,
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

        private void btnQuestListUp_Click(object sender, EventArgs e)
        {
            if (lstQuestLists.SelectedIndex > 0 && lstQuestLists.Items.Count > 1)
            {
                var index = lstQuestLists.SelectedIndex;
                var swapWith = mEditorItem.QuestLists[index - 1];
                mEditorItem.QuestLists[index - 1] = mEditorItem.QuestLists[index];
                mEditorItem.QuestLists[index] = swapWith;
                UpdateList();
                lstQuestLists.SelectedIndex = index - 1;
            }
        }

        private void btnQuestListDown_Click(object sender, EventArgs e)
        {
            if (lstQuestLists.SelectedIndex > -1 && lstQuestLists.SelectedIndex + 1 != lstQuestLists.Items.Count)
            {
                var index = lstQuestLists.SelectedIndex;
                var swapWith = mEditorItem.QuestLists[index + 1];
                mEditorItem.QuestLists[index + 1] = mEditorItem.QuestLists[index];
                mEditorItem.QuestLists[index] = swapWith;
                UpdateList();
                lstQuestLists.SelectedIndex = index + 1;
            }
        }

        private void btnAddQuestList_Click(object sender, EventArgs e)
        {
            var id = QuestListBase.IdFromList(cmbQuestLists.SelectedIndex);
            var questList = QuestListBase.Get(id);
            if (questList != null && !mEditorItem.QuestLists.Contains(id))
            {
                mEditorItem.QuestLists.Add(id);
                UpdateList();
            }
        }

        private void btnRemoveQuestList_Click(object sender, EventArgs e)
        {
            if (lstQuestLists.SelectedIndex > -1)
            {
                mEditorItem.QuestLists.RemoveAt(lstQuestLists.SelectedIndex);
                UpdateList();
            }
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

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            FormHelpers.ClearSearchPressed(ref txtSearch);
        }
    }
}
