using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;
using Intersect.Editor.Extensions;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.QuestList;
using Intersect.Models;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmQuestList : EditorForm
    {
        private List<QuestListBase> mChanged = new List<QuestListBase>();

        private string mCopiedItem;

        private QuestListBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        public frmQuestList()
        {
            ApplyHooks();
            InitializeComponent();

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = QuestListBase.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.QuestList)
            {
                InitEditor();
                if (mEditorItem != null && !DatabaseObject<QuestListBase>.Lookup.Values.Contains(mEditorItem))
                {
                    mEditorItem = null;
                    UpdateEditor();
                }
            }
        }

        public override void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in QuestListBase.Lookup)
            {
                if (!string.IsNullOrEmpty(((QuestListBase)itm.Value).Folder) &&
                    !mFolders.Contains(((QuestListBase)itm.Value).Folder))
                {
                    mFolders.Add(((QuestListBase)itm.Value).Folder);
                    if (!mKnownFolders.Contains(((QuestListBase)itm.Value).Folder))
                    {
                        mKnownFolders.Add(((QuestListBase)itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            var items = QuestListBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((QuestListBase)pair.Value)?.Name ?? Models.DatabaseObject<QuestListBase>.Deleted, ((QuestListBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
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

        private void UpdateToolStripItems()
        {
            toolStripItemCopy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemPaste.Enabled = mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused;
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        public void UpdateList()
        {
            lstQuests.Items.Clear();
            foreach (var id in mEditorItem.Quests)
            {
                lstQuests.Items.Add(QuestBase.GetName(id));
            }
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.QuestListEditor.searchplaceholder;
        }

        private void frmQuestList_Load(object sender, EventArgs e)
        {
            cmbQuests.Items.Clear();
            cmbQuests.Items.AddRange(QuestBase.Names);

            InitLocalization();
        }

        private void InitLocalization()
        {
            Text = Strings.QuestListEditor.title;
            toolStripItemNew.Text = Strings.QuestListEditor.New;
            toolStripItemDelete.Text = Strings.QuestListEditor.delete;
            toolStripItemCopy.Text = Strings.QuestListEditor.copy;
            toolStripItemPaste.Text = Strings.QuestListEditor.paste;
            toolStripItemUndo.Text = Strings.QuestListEditor.undo;

            grpQuestList.Text = Strings.QuestListEditor.quests;
            grpQuests.Text = Strings.QuestListEditor.quests;

            lblAddQuest.Text = Strings.QuestListEditor.addquestlabel;
            btnAddQuest.Text = Strings.QuestListEditor.add;
            btnRemoveQuest.Text = Strings.QuestListEditor.remove;

            grpGeneral.Text = Strings.QuestListEditor.general;
            lblName.Text = Strings.QuestListEditor.name;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.QuestListEditor.sortalphabetically;
            txtSearch.Text = Strings.QuestListEditor.searchplaceholder;
            lblFolder.Text = Strings.QuestListEditor.folderlabel;

            btnSave.Text = Strings.QuestListEditor.save;
            btnCancel.Text = Strings.QuestListEditor.cancel;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
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

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.QuestList);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                if (MessageBoxExtensions.ShowWarning(
                        Strings.QuestListEditor.deleteprompt, Strings.QuestListEditor.delete,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    PacketSender.SendDeleteObject(mEditorItem);
                }
            }
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

        private void btnAddQuest_Click(object sender, EventArgs e)
        {
            var id = QuestBase.IdFromList(cmbQuests.SelectedIndex);
            var quest = QuestBase.Get(id);
            if (quest != null && !mEditorItem.Quests.Contains(id))
            {
                mEditorItem.Quests.Add(id);
                UpdateList();
            }
        }

        private void btnRemoveQuest_Click(object sender, EventArgs e)
        {
            if (lstQuests.SelectedIndex > -1)
            {
                mEditorItem.Quests.RemoveAt(lstQuests.SelectedIndex);
                UpdateList();
            }
        }

        private void btnQuestUp_Click(object sender, EventArgs e)
        {
            if (lstQuests.SelectedIndex > 0 && lstQuests.Items.Count > 1)
            {
                var index = lstQuests.SelectedIndex;
                var swapWith = mEditorItem.Quests[index - 1];
                mEditorItem.Quests[index - 1] = mEditorItem.Quests[index];
                mEditorItem.Quests[index] = swapWith;
                UpdateList();
                lstQuests.SelectedIndex = index - 1;
            }
        }

        private void btnQuestDown_Click(object sender, EventArgs e)
        {
            if (lstQuests.SelectedIndex > -1 && lstQuests.SelectedIndex + 1 != lstQuests.Items.Count)
            {
                var index = lstQuests.SelectedIndex;
                var swapWith = mEditorItem.Quests[index + 1];
                mEditorItem.Quests[index + 1] = mEditorItem.Quests[index];
                mEditorItem.Quests[index] = swapWith;
                UpdateList();
                lstQuests.SelectedIndex = index + 1;
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.QuestListEditor.folderprompt, Strings.QuestListEditor.foldertitle, ref folderName,
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

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Folder = cmbFolder.Text;
            InitEditor();
        }

        private void btnAlphabetical_Click(object sender, EventArgs e)
        {
            btnAlphabetical.Checked = !btnAlphabetical.Checked;
            InitEditor();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            InitEditor();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = Strings.QuestListEditor.searchplaceholder;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.QuestListEditor.searchplaceholder;
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            if (mChanged.Contains(mEditorItem) && mEditorItem != null)
            {
                if (MessageBoxExtensions.ShowWarning(
                        Strings.QuestListEditor.undoprompt, Strings.QuestListEditor.undotitle,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    mEditorItem.RestoreBackup();
                    UpdateEditor();
                }
            }
        }

        private void btnEditRequirements_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(mEditorItem.Requirements, RequirementType.QuestList);
            frm.ShowDialog();
        }
    }
}
