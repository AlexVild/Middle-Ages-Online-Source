using DarkUI.Forms;
using Intersect.Editor.Forms.Helpers;
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

    public enum DropType
    {
        Item,
        Table,
    }

    public partial class frmLootTables : EditorForm
    {
        private List<string> mExpandedFolders = new List<string>();

        private List<string> mKnownFolders = new List<string>();

        private List<LootTableDescriptor> mChanged = new List<LootTableDescriptor>();

        private string mCopiedItem;

        private LootTableDescriptor mEditorItem;

        private bool mPopulating = false;

        private bool mDropTypeUpdate = false;

        public frmLootTables()
        {
            ApplyHooks();
            InitializeComponent();

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        public override void InitEditor()
        {
            // Fill in combo boxes (event boxes are filled on editor "_load" event)
            FillItemCombobox(DropType.Item);
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
            InitEditor();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            FormHelpers.ClearSearchPressed(ref txtSearch);
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

            var totalWeight = LootTableHelpers.GetTotalWeight(mEditorItem.Drops);
            for (var i = 0; i < mEditorItem.Drops.Count; i++)
            {
                var prettyChance = LootTableHelpers.GetPrettyChance(mEditorItem.Drops[i].Chance, totalWeight);
                if (mEditorItem.Drops[i].ItemId != Guid.Empty)
                {
                    lstDrops.Items.Add(
                        $"{ItemBase.GetName(mEditorItem.Drops[i].ItemId)} x{mEditorItem.Drops[i].Quantity}: {prettyChance} chance"
                    );
                }
                else
                {
                    if (mEditorItem.Drops[i].LootTableId != Guid.Empty)
                    {
                        lstDrops.Items.Add(
                            Strings.NpcEditor.LootTableDrop.ToString(
                                LootTableDescriptor.GetName(mEditorItem.Drops[i].LootTableId),
                                prettyChance
                            )
                        );
                    } else
                    {
                        lstDrops.Items.Add(
                            $"{TextUtils.None} x{mEditorItem.Drops[i].Quantity}: {prettyChance} chance"
                        );
                    }
                }
            }

            if (keepIndex && index < lstDrops.Items.Count)
            {
                lstDrops.SelectedIndex = index;
            }
        }

        private void lstDrops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mDropTypeUpdate)
            {
                return;
            }
            mDropTypeUpdate = true;
            if (lstDrops.SelectedIndex > -1)
            {
                if (mEditorItem.Drops[lstDrops.SelectedIndex].ItemId != Guid.Empty)
                {
                    if (!rdoItem.Checked)
                    {
                        rdoItem.Checked = true;
                    }
                    cmbDropItem.SelectedIndex = ItemBase.ListIndex(mEditorItem.Drops[lstDrops.SelectedIndex].ItemId) + 1;
                } 
                else
                {
                    if (mEditorItem.Drops[lstDrops.SelectedIndex].LootTableId != Guid.Empty)
                    {
                        if (!rdoTable.Checked)
                        {
                            rdoTable.Checked = true;
                        }
                        cmbDropItem.SelectedIndex = LootTableDescriptor.ListIndex(mEditorItem.Drops[lstDrops.SelectedIndex].LootTableId);
                    }
                    else
                    {
                        if (!rdoItem.Checked)
                        {
                            rdoItem.Checked = true;
                        }
                        cmbDropItem.SelectedIndex = 0; // none
                    }
                }
                nudDropAmount.Value = mEditorItem.Drops[lstDrops.SelectedIndex].Quantity;
                nudDropChance.Value = (decimal)mEditorItem.Drops[lstDrops.SelectedIndex].Chance;
            }
            mDropTypeUpdate = false;
        }

        private void cmbDropItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mDropTypeUpdate)
            {
                return;
            }

            if (lstDrops.SelectedIndex > -1 && lstDrops.SelectedIndex < mEditorItem.Drops.Count)
            {
                if (rdoItem.Checked)
                {
                    mEditorItem.Drops[lstDrops.SelectedIndex].ItemId = ItemBase.IdFromList(cmbDropItem.SelectedIndex - 1);
                }
                else
                {
                    mEditorItem.Drops[lstDrops.SelectedIndex].LootTableId = LootTableDescriptor.IdFromList(cmbDropItem.SelectedIndex);
                }
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
            if (rdoItem.Checked)
            {
                if (nudDropAmount.Value <= 0 || nudDropChance.Value <= 0)
                {
                    return;
                }
                mEditorItem.Drops.Add(new BaseDrop());
                mEditorItem.Drops[mEditorItem.Drops.Count - 1].ItemId = ItemBase.IdFromList(cmbDropItem.SelectedIndex - 1);
                mEditorItem.Drops[mEditorItem.Drops.Count - 1].LootTableId = Guid.Empty;
                mEditorItem.Drops[mEditorItem.Drops.Count - 1].Quantity = (int)nudDropAmount.Value;
            }
            else
            {
                // Don't allow adding a loot table to itself
                var lootTableId = LootTableDescriptor.IdFromList(cmbDropItem.SelectedIndex);
                if (lootTableId == mEditorItem.Id || nudDropChance.Value <= 0)
                {
                    return;
                }
                mEditorItem.Drops.Add(new BaseDrop());
                mEditorItem.Drops[mEditorItem.Drops.Count - 1].ItemId = Guid.Empty;
                mEditorItem.Drops[mEditorItem.Drops.Count - 1].LootTableId = LootTableDescriptor.IdFromList(cmbDropItem.SelectedIndex);
                mEditorItem.Drops[mEditorItem.Drops.Count - 1].Quantity = (int)nudDropAmount.Value;
            }
            
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

        private void rdoItem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDropType(DropType.Item);
        }

        private void FillItemCombobox(DropType dropType)
        {
            mDropTypeUpdate = true;
            cmbDropItem.SelectedIndex = -1;
            if (dropType == DropType.Item)
            {
                cmbDropItem.Items.Clear();
                cmbDropItem.Items.Add(Strings.General.none);
                cmbDropItem.Items.AddRange(ItemBase.Names);
            }
            else
            {
                cmbDropItem.Items.Clear();
                cmbDropItem.Items.AddRange(LootTableDescriptor.Names);
            }
            if (cmbDropItem.Items.Count > 0)
            {
                cmbDropItem.SelectedIndex = 0;
            }
            mDropTypeUpdate = false;
        }

        private void ChangeDropType(DropType dropType)
        {
            FillItemCombobox(dropType);
            if (dropType == DropType.Item)
            {
                nudDropAmount.Enabled = true;
            }
            else
            {
                nudDropAmount.Enabled = false;
            }
        }

        private void rdoTable_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDropType(DropType.Table);
        }

        private void btnUnselectItem_Click(object sender, EventArgs e)
        {
            lstDrops.SelectedIndex = -1;
        }
    }
}
