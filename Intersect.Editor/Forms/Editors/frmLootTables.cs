using DarkUI.Forms;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Models;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmLootTables : EditorForm
    {
        private List<string> mExpandedFolders = new List<string>();

        private List<string> mKnownFolders = new List<string>();

        private List<LootTableDescriptor> mChanged = new List<LootTableDescriptor>();

        private string mCopiedItem;

        private LootTableDescriptor mEditorItem;

        private bool mPopulating = false;

        public frmLootTables()
        {
            ApplyHooks();
            InitializeComponent();

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        public void InitEditor()
        {
            // Fill in combo boxes (event boxes are filled on editor "_load" event)
            cmbDropItem.Items.Clear();
            cmbDropItem.Items.Add(Strings.General.none);
            cmbDropItem.Items.AddRange(ItemBase.Names);

            PopulateEditor();
            RefreshEditorListItems();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.LootTable)
            {
                RefreshEditorListItems();
                mEditorItem = null;
                PopulateEditor();
            }
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = LootTableDescriptor.Get(id);
            
            PopulateEditor();
        }

        private void PopulateEditor()
        {
            if (mEditorItem != null)
            {
                mPopulating = true;
                pnlTableSettings.Show();
                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                PopulateEditorItemValues();

                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }

                mPopulating = false;
            }
            else
            {
                pnlTableSettings.Hide();
            }

            UpdateToolStripItems();
        }

        private void PopulateEditorItemValues()
        {
            txtName.Text = mEditorItem.Name;
            cmbFolder.Text = mEditorItem.Folder;
            txtDisplayName.Text = mEditorItem.DisplayName;

            UpdateDropValues();
        }

        public void RefreshEditorListItems()
        {
            var tables = LootTableDescriptor.Lookup
                .OrderBy(p => p.Value?.Name).ToList();

            //Collect folders
            var mFolders = new List<string>();
            mKnownFolders.Clear();
            foreach (var itm in tables)
            {
                var folder = ((LootTableDescriptor)itm.Value).Folder;
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

            var listItems = tables
                .Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key, new KeyValuePair<string, string>(((LootTableDescriptor)pair.Value)?.Name
                    ?? DatabaseObject<LootTableDescriptor>.Deleted, ((LootTableDescriptor)pair.Value)?.Folder ?? "")))
                .ToArray();

            lstGameObjects.Repopulate(listItems, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(Enums.GameObjectType.LootTable);
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
                RefreshEditorListItems();
            }
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
                   txtSearch.Text != Strings.TimerEditor.SearchPlaceHolder;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {

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

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
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

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtDisplayName.Text;
        }

        private void btnDynamicRequirements_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(mEditorItem.DropConditions, RequirementType.LootTable);
            frm.TopMost = true;
            frm.ShowDialog();
        }

        private void UpdateDropValues(bool keepIndex = false)
        {
            var index = lstDrops.SelectedIndex;
            lstDrops.Items.Clear();

            var drops = mEditorItem.Drops.ToArray();

            for (var i = 0; i < mEditorItem.Drops.Count; i++)
            {
                if (mEditorItem.Drops[i].ItemId != Guid.Empty)
                {
                    lstDrops.Items.Add(
                        Strings.NpcEditor.dropdisplay.ToString(
                            ItemBase.GetName(mEditorItem.Drops[i].ItemId), mEditorItem.Drops[i].Quantity,
                            mEditorItem.Drops[i].Chance
                        )
                    );
                }
                else
                {
                    lstDrops.Items.Add(
                        Strings.NpcEditor.dropdisplay.ToString(
                            TextUtils.None, 1,
                            mEditorItem.Drops[i].Chance
                        )
                    );
                }
            }

            if (keepIndex && index < lstDrops.Items.Count)
            {
                lstDrops.SelectedIndex = index;
            }
        }

        private void lstDrops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex > -1)
            {
                cmbDropItem.SelectedIndex = ItemBase.ListIndex(mEditorItem.Drops[lstDrops.SelectedIndex].ItemId) + 1;
                nudDropAmount.Value = mEditorItem.Drops[lstDrops.SelectedIndex].Quantity;
                nudDropChance.Value = (decimal)mEditorItem.Drops[lstDrops.SelectedIndex].Chance;
            }
        }

        private void cmbDropItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex > -1 && lstDrops.SelectedIndex < mEditorItem.Drops.Count)
            {
                mEditorItem.Drops[lstDrops.SelectedIndex].ItemId = ItemBase.IdFromList(cmbDropItem.SelectedIndex - 1);
            }

            UpdateDropValues(true);
        }

        private void nudDropAmount_ValueChanged(object sender, EventArgs e)
        {
            // This should never be below 1. We shouldn't accept giving 0 items!
            nudDropAmount.Value = Math.Max(1, nudDropAmount.Value);

            if (lstDrops.SelectedIndex < lstDrops.Items.Count)
            {
                return;
            }

            mEditorItem.Drops[(int)lstDrops.SelectedIndex].Quantity = (int)nudDropAmount.Value;
            UpdateDropValues(true);
        }

        private void nudDropChance_ValueChanged(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex < lstDrops.Items.Count)
            {
                return;
            }

            mEditorItem.Drops[(int)lstDrops.SelectedIndex].Chance = (double)nudDropChance.Value;
            UpdateDropValues(true);
        }

        private void btnDropAdd_Click(object sender, EventArgs e)
        {
            if (nudDropAmount.Value <= 0 || nudDropChance.Value <= 0)
            {
                return;
            }
            mEditorItem.Drops.Add(new NpcDrop());
            mEditorItem.Drops[mEditorItem.Drops.Count - 1].ItemId = ItemBase.IdFromList(cmbDropItem.SelectedIndex - 1);
            mEditorItem.Drops[mEditorItem.Drops.Count - 1].Quantity = (int)nudDropAmount.Value;
            mEditorItem.Drops[mEditorItem.Drops.Count - 1].Chance = (double)nudDropChance.Value;

            UpdateDropValues();
        }

        private void btnDropRemove_Click(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex > -1)
            {
                var i = lstDrops.SelectedIndex;
                lstDrops.Items.RemoveAt(i);
                mEditorItem.Drops.RemoveAt(i);
            }

            UpdateDropValues(true);
        }
    }
}
