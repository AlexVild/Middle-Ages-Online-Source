using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;

using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Models;
using Intersect.Extensions;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors
{

    public partial class FrmSwitchVariable : EditorForm
    {

        private List<IDatabaseObject> mChanged = new List<IDatabaseObject>();

        private IDatabaseObject mEditorItem;

        private List<string> mExpandedFolders = new List<string>();

        private List<string> mGlobalExpandedFolders = new List<string>();

        private List<string> mGlobalKnownFolders = new List<string>();

        private List<string> mInstanceExpandedFolders = new List<string>();

        private List<string> mInstanceKnownFolders = new List<string>();

        private List<string> mGuildExpandedFolders = new List<string>();

        private List<string> mGuildKnownFolders = new List<string>();

        private List<string> mKnownFolders = new List<string>();

        private List<string> mKnownGroups = new List<string>();

        private bool IsPopulating = false;

        public FrmSwitchVariable()
        {
            ApplyHooks();
            InitializeComponent();
            InitLocalization();
            nudVariableValue.Minimum = long.MinValue;
            nudVariableValue.Maximum = long.MaxValue;

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, null, toolStripItemUndo_Click, null, toolStripItemDelete_Click);
        }
        private void AssignEditorItem(Guid id)
        {
            if (id != Guid.Empty)
            {
                IDatabaseObject obj = null;
                if (rdoPlayerVariables.Checked)
                {
                    obj = PlayerVariableBase.Get(id);
                }
                else if (rdoGlobalVariables.Checked)
                {
                    obj = ServerVariableBase.Get(id);
                }
                else if (rdoInstanceVariables.Checked)
                {
                    obj = InstanceVariableBase.Get(id);
                }
                else if (rdoGuildVariables.Checked)
                {
                    obj = GuildVariableBase.Get(id);
                }

                if (obj != null)
                {
                    mEditorItem = obj;
                    if (!mChanged.Contains(obj))
                    {
                        mChanged.Add(obj);
                        obj.MakeBackup();
                    }
                }
            }
            UpdateEditor();
        }

        private void InitLocalization()
        {
            Text = Strings.VariableEditor.title;
            grpTypes.Text = Strings.VariableEditor.type;
            grpList.Text = Strings.VariableEditor.list;
            rdoPlayerVariables.Text = Strings.VariableEditor.playervariables;
            rdoGlobalVariables.Text = Strings.VariableEditor.globalvariables;
            rdoInstanceVariables.Text = Strings.VariableEditor.instancevariables;
            grpEditor.Text = Strings.VariableEditor.editor;
            lblName.Text = Strings.VariableEditor.name;
            grpValue.Text = Strings.VariableEditor.value;
            cmbBooleanValue.Items.Clear();
            cmbBooleanValue.Items.Add(Strings.VariableEditor.False);
            cmbBooleanValue.Items.Add(Strings.VariableEditor.True);
            cmbVariableType.Items.Clear();
            foreach (var itm in Strings.VariableEditor.types)
            {
                cmbVariableType.Items.Add(itm.Value);
            }

            toolStripItemNew.ToolTipText = Strings.VariableEditor.New;
            toolStripItemDelete.ToolTipText = Strings.VariableEditor.delete;
            toolStripItemUndo.ToolTipText = Strings.VariableEditor.undo;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.VariableEditor.sortalphabetically;
            txtSearch.Text = Strings.VariableEditor.searchplaceholder;
            lblFolder.Text = Strings.VariableEditor.folderlabel;

            btnSave.Text = Strings.VariableEditor.save;
            btnCancel.Text = Strings.VariableEditor.cancel;
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            InitEditor();
            if (mEditorItem != null && type.GetLookup().Values.Contains(mEditorItem))
            {
                mEditorItem = null;
                UpdateEditor();
            }
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            if (rdoPlayerVariables.Checked)
            {
                PacketSender.SendCreateObject(GameObjectType.PlayerVariable);
            }
            else if (rdoGlobalVariables.Checked)
            {
                PacketSender.SendCreateObject(GameObjectType.ServerVariable);
            }
            else if (rdoInstanceVariables.Checked)
            {
                PacketSender.SendCreateObject(GameObjectType.InstanceVariable);
            }
            else if (rdoGuildVariables.Checked)
            {
                PacketSender.SendCreateObject(GameObjectType.GuildVariable);
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

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.VariableEditor.deleteprompt, Strings.VariableEditor.deletecaption,
                        DarkDialogButton.YesNo, Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    PacketSender.SendDeleteObject(mEditorItem);
                }
            }
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

        private void rdoPlayerVariables_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem = null;
            lstGameObjects.ClearExpandedFolders();
            InitEditor();
        }

        private void rdoGlobalVariables_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem = null;
            lstGameObjects.ClearExpandedFolders();
            InitEditor();
            
        }

        private void UpdateToolStripItems()
        {
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        private void UpdateEditor()
        {
            IsPopulating = true;
            if (mEditorItem != null)
            {
                grpEditor.Show();
                if (rdoPlayerVariables.Checked)
                {
                    grpRecordOptions.Show();
                    var baseVar = (PlayerVariableBase)mEditorItem;
                    lblObject.Text = Strings.VariableEditor.playervariable;
                    txtObjectName.Text = baseVar.Name;
                    txtId.Text = baseVar.TextId;
                    cmbFolder.Text = baseVar.Folder;
                    cmbVariableType.SelectedIndex = (int)(baseVar.Type - 1);
                    chkRecordable.Checked = baseVar.Recordable;
                    chkRecordLow.Checked = baseVar.RecordLow;
                    chkSilent.Checked = baseVar.RecordSilently;
                    chkSoloOnly.Checked = baseVar.SoloRecordOnly;
                    chkRecordLow.Enabled = chkRecordable.Checked;
                    chkSilent.Enabled = chkRecordable.Checked;
                    chkSoloOnly.Enabled = chkRecordable.Checked;
                    cmbVariableGroup.Enabled = true;
                    btnAddGroup.Enabled = true;
                    if (cmbVariableGroup.Items.Contains(baseVar.VariableGroup))
                    {
                        cmbVariableGroup.SelectedIndex = cmbVariableGroup.Items.IndexOf(baseVar.VariableGroup);
                    }
                    else
                    {
                        cmbVariableGroup.SelectedIndex = 0;
                    }

                    grpValue.Hide();
                }
                else if (rdoGlobalVariables.Checked)
                {
                    grpRecordOptions.Hide();
                    cmbVariableGroup.Enabled = false;
                    btnAddGroup.Enabled = false;
                    lblObject.Text = Strings.VariableEditor.globalvariable;
                    txtObjectName.Text = ((ServerVariableBase) mEditorItem).Name;
                    txtId.Text = ((ServerVariableBase) mEditorItem).TextId;
                    cmbFolder.Text = ((ServerVariableBase) mEditorItem).Folder;
                    cmbVariableType.SelectedIndex = (int) (((ServerVariableBase) mEditorItem).Type - 1);
                    grpValue.Show();
                }
                else if (rdoInstanceVariables.Checked)
                {
                    grpRecordOptions.Hide();
                    cmbVariableGroup.Enabled = false;
                    btnAddGroup.Enabled = false;
                    lblObject.Text = Strings.VariableEditor.instancevariable;
                    txtObjectName.Text = ((InstanceVariableBase)mEditorItem).Name;
                    txtId.Text = ((InstanceVariableBase)mEditorItem).TextId;
                    cmbFolder.Text = ((InstanceVariableBase)mEditorItem).Folder;
                    cmbVariableType.SelectedIndex = (int)(((InstanceVariableBase)mEditorItem).Type - 1);
                    grpValue.Show();
                }
                else if (rdoGuildVariables.Checked)
                {
                    grpRecordOptions.Hide();
                    cmbVariableGroup.Enabled = false;
                    btnAddGroup.Enabled = false;
                    lblObject.Text = "Guild Variable";
                    txtObjectName.Text = ((GuildVariableBase)mEditorItem).Name;
                    txtId.Text = ((GuildVariableBase)mEditorItem).TextId;
                    cmbFolder.Text = ((GuildVariableBase)mEditorItem).Folder;
                    cmbVariableType.SelectedIndex = (int)(((GuildVariableBase)mEditorItem).Type - 1);
                    grpValue.Hide();
                }

                InitValueGroup();
            }
            else
            {
                grpEditor.Hide();
            }

            IsPopulating = false;
            UpdateToolStripItems();
        }

        private void UpdateSelection()
        {
            if (NothingSelected)
            {
                return;
            }

            grpEditor.Show();
            var selectedId = (Guid)lstGameObjects.SelectedNode.Tag;
            var obj = SelectedGameObject.GetLookup().Get(selectedId);
            lstGameObjects.SelectedNode.Text = obj.Name;

            UpdateValueVisibility();
        }

        private bool NothingSelected => lstGameObjects.SelectedNode == null || lstGameObjects.SelectedNode.Tag == null;

        private void UpdateValueVisibility()
        {
            grpValue.Visible = !rdoPlayerVariables.Checked && !rdoGuildVariables.Checked;
        }

        private void txtObjectName_TextChanged(object sender, EventArgs e)
        {
            if (NothingSelected)
            {
                return;
            }

            grpEditor.Show();
            SelectedObj.Name = txtObjectName.Text;
            lstGameObjects.UpdateText(SelectedObj.Name);

            UpdateValueVisibility();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == ' ';
        }

        IVariableBase SelectedObj => (IVariableBase)SelectedVariableType.GetVariableTable().GetLookup().Get((Guid)lstGameObjects.SelectedNode.Tag);

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            if (NothingSelected)
            {
                return;
            }

            SelectedObj.TextId = txtId.Text;
        }

        private void nudVariableValue_ValueChanged(object sender, EventArgs e)
        {
            if (NothingSelected)
            {
                return;
            }

            if (rdoGlobalVariables.Checked)
            {
                var obj = ServerVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                if (obj != null)
                {
                    obj.Value.Integer = (long)nudVariableValue.Value;
                    UpdateSelection();
                }
            }
            else if (rdoInstanceVariables.Checked)
            {
                var obj = InstanceVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                if (obj != null)
                {
                    obj.DefaultValue.Integer = (long)nudVariableValue.Value;
                }
            }
        }

        private void cmbVariableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NothingSelected)
            {
                return;
            }

            SelectedObj.Type = (VariableDataTypes)(cmbVariableType.SelectedIndex + 1);
            
            InitValueGroup();
            UpdateSelection();
        }

        private void InitValueGroup()
        {
            if (rdoPlayerVariables.Checked || rdoGuildVariables.Checked)
            {
                grpValue.Hide();
                return;
            }
            
            if (rdoGlobalVariables.Checked)
            {
                grpValue.Show();
                if (lstGameObjects.SelectedNode != null && lstGameObjects.SelectedNode.Tag != null)
                {
                    var obj = ServerVariableBase.Get((Guid) lstGameObjects.SelectedNode.Tag);
                    cmbBooleanValue.Hide();
                    nudVariableValue.Hide();
                    txtStringValue.Hide();
                    switch (obj.Type)
                    {
                        case VariableDataTypes.Boolean:
                            cmbBooleanValue.Show();
                            cmbBooleanValue.SelectedIndex = Convert.ToInt32(obj.Value.Boolean);

                            break;

                        case VariableDataTypes.Integer:
                            nudVariableValue.Show();
                            nudVariableValue.Value = obj.Value.Integer;

                            break;

                        case VariableDataTypes.Number:
                            break;

                        case VariableDataTypes.String:
                            txtStringValue.Show();
                            txtStringValue.Text = obj.Value.String;

                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else if (rdoInstanceVariables.Checked)
            {
                grpValue.Show();
                if (lstGameObjects.SelectedNode != null && lstGameObjects.SelectedNode.Tag != null)
                {
                    var obj = InstanceVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                    cmbBooleanValue.Hide();
                    nudVariableValue.Hide();
                    txtStringValue.Hide();
                    switch (obj.Type)
                    {
                        case VariableDataTypes.Boolean:
                            cmbBooleanValue.Show();
                            cmbBooleanValue.SelectedIndex = Convert.ToInt32(obj.DefaultValue.Boolean);
                            obj.DefaultValue.Boolean = Convert.ToBoolean(cmbBooleanValue.SelectedIndex);

                            break;

                        case VariableDataTypes.Integer:
                            nudVariableValue.Show();
                            nudVariableValue.Value = obj.DefaultValue.Integer;
                            obj.DefaultValue.Integer = (int) nudVariableValue.Value;

                            break;

                        case VariableDataTypes.Number:
                            break;

                        case VariableDataTypes.String:
                            txtStringValue.Show();
                            txtStringValue.Text = obj.DefaultValue.String;
                            obj.DefaultValue.String = txtStringValue.Text;

                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private void cmbBooleanValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NothingSelected)
            {
                return;
            }

            if (rdoGlobalVariables.Checked)
            {
                var obj = ServerVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                if (obj != null)
                {
                    obj.Value.Boolean = Convert.ToBoolean(cmbBooleanValue.SelectedIndex);
                }
            }
            else if (rdoInstanceVariables.Checked)
            {
                var obj = InstanceVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                if (obj != null)
                {
                    obj.DefaultValue.Boolean = Convert.ToBoolean(cmbBooleanValue.SelectedIndex);
                }
            }

            UpdateSelection();
        }

        private void txtStringValue_TextChanged(object sender, EventArgs e)
        {
            if (NothingSelected)
            {
                return;
            }

            if (rdoGlobalVariables.Checked)
            {
                var obj = ServerVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                if (obj != null)
                {
                    obj.Value.String = txtStringValue.Text;
                    UpdateSelection();
                }
            }
            else if (rdoInstanceVariables.Checked)
            {
                var obj = InstanceVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                if (obj != null)
                {
                    obj.DefaultValue.String = txtStringValue.Text;
                }
            }
        }

        private void rdoInstanceVariables_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem = null;
            lstGameObjects.ClearExpandedFolders();
            InitEditor();
        }

        #region "Item List - Folders, Searching, Sorting, Etc"

        public void InitEditor()
        {
            //Fix Title
            if (rdoPlayerVariables.Checked)
            {
                grpVariables.Text = rdoPlayerVariables.Text;
                grpValue.Text = Strings.VariableEditor.value;
            } else if (rdoGlobalVariables.Checked)
            {
                grpVariables.Text = rdoGlobalVariables.Text;
                grpValue.Text = Strings.VariableEditor.value;
            } else if (rdoInstanceVariables.Checked)
            {
                grpVariables.Text = rdoInstanceVariables.Text;
                grpValue.Text = Strings.VariableEditor.defaultvalue;
            }
            else if (rdoGuildVariables.Checked)
            {
                grpVariables.Text = rdoGuildVariables.Text;
                grpValue.Text = Strings.VariableEditor.defaultvalue;
            }

            grpEditor.Hide();
            cmbBooleanValue.Hide();
            nudVariableValue.Hide();
            txtStringValue.Hide();

            //Collect folders
            var mFolders = new List<string>();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");

            if (rdoPlayerVariables.Checked)
            {
                foreach (var itm in PlayerVariableBase.Lookup)
                {
                    var baseVar = (PlayerVariableBase)itm.Value;
                    var folder = baseVar.Folder;
                    var group = baseVar.VariableGroup;
                    if (!string.IsNullOrEmpty(folder) && !mFolders.Contains(folder))
                    {
                        mFolders.Add(folder);
                        if (!mKnownFolders.Contains(folder))
                        {
                            mKnownFolders.Add(folder);
                        }
                    }
                    if (!string.IsNullOrEmpty(group) && !mKnownGroups.Contains(group))
                    {
                        mKnownGroups.Add(group);
                    }
                }

                mKnownFolders.Sort();
                cmbFolder.Items.AddRange(mKnownFolders.ToArray());
                lblId.Text = Strings.VariableEditor.textidpv;

                InitializeGroups();
            }
            else if (rdoGlobalVariables.Checked)
            {
                foreach (var itm in ServerVariableBase.Lookup)
                {
                    if (!string.IsNullOrEmpty(((ServerVariableBase) itm.Value).Folder) &&
                        !mFolders.Contains(((ServerVariableBase) itm.Value).Folder))
                    {
                        mFolders.Add(((ServerVariableBase) itm.Value).Folder);
                        if (!mGlobalKnownFolders.Contains(((ServerVariableBase) itm.Value).Folder))
                        {
                            mGlobalKnownFolders.Add(((ServerVariableBase) itm.Value).Folder);
                        }
                    }
                }

                mGlobalKnownFolders.Sort();
                cmbFolder.Items.AddRange(mGlobalKnownFolders.ToArray());
                lblId.Text = Strings.VariableEditor.textidgv;
            } 
            else if (rdoInstanceVariables.Checked)
            {
                foreach(var itm in InstanceVariableBase.Lookup)
                {
                    if (!string.IsNullOrEmpty(((InstanceVariableBase)itm.Value).Folder) &&
                        !mFolders.Contains(((InstanceVariableBase)itm.Value).Folder))
                    {
                        mFolders.Add(((InstanceVariableBase)itm.Value).Folder);
                        if (!mInstanceKnownFolders.Contains(((InstanceVariableBase)itm.Value).Folder))
                        {
                            mInstanceKnownFolders.Add(((InstanceVariableBase)itm.Value).Folder);
                        }
                    }
                }

                mInstanceKnownFolders.Sort();
                cmbFolder.Items.AddRange(mInstanceKnownFolders.ToArray());
                lblId.Text = Strings.VariableEditor.textidiv;
            }
            else if (rdoGuildVariables.Checked)
            {
                foreach (var itm in GuildVariableBase.Lookup)
                {
                    if (!string.IsNullOrEmpty(((GuildVariableBase)itm.Value).Folder) &&
                        !mFolders.Contains(((GuildVariableBase)itm.Value).Folder))
                    {
                        mFolders.Add(((GuildVariableBase)itm.Value).Folder);
                        if (!mInstanceKnownFolders.Contains(((GuildVariableBase)itm.Value).Folder))
                        {
                            mInstanceKnownFolders.Add(((GuildVariableBase)itm.Value).Folder);
                        }
                    }
                }

                mInstanceKnownFolders.Sort();
                cmbFolder.Items.AddRange(mInstanceKnownFolders.ToArray());
                lblId.Text = Strings.VariableEditor.textidiv;
            }

            mFolders.Sort();

            KeyValuePair<Guid, KeyValuePair<string, string>>[] items = null;

            if (rdoPlayerVariables.Checked)
            {
                items = PlayerVariableBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                    new KeyValuePair<string, string>(((PlayerVariableBase)pair.Value)?.Name ?? Models.DatabaseObject<PlayerVariableBase>.Deleted, ((PlayerVariableBase)pair.Value)?.Folder ?? ""))).ToArray();
            } 
            else if (rdoGlobalVariables.Checked)
            {
                items = ServerVariableBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                    new KeyValuePair<string, string>(((ServerVariableBase)pair.Value)?.Name ?? Models.DatabaseObject<ServerVariableBase>.Deleted + " = " + ((ServerVariableBase)pair.Value)?.Value.ToString(((ServerVariableBase)pair.Value).Type) ?? "", ((ServerVariableBase)pair.Value)?.Folder ?? ""))).ToArray();
            } 
            else if (rdoInstanceVariables.Checked)
            {
                items = InstanceVariableBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                    new KeyValuePair<string, string>(((InstanceVariableBase)pair.Value)?.Name ?? Models.DatabaseObject<InstanceVariableBase>.Deleted, ((InstanceVariableBase)pair.Value)?.Folder ?? ""))).ToArray();
            }
            else if (rdoGuildVariables.Checked)
            {
                items = GuildVariableBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                    new KeyValuePair<string, string>(((GuildVariableBase)pair.Value)?.Name ?? Models.DatabaseObject<GuildVariableBase>.Deleted, ((GuildVariableBase)pair.Value)?.Folder ?? ""))).ToArray();
            }

            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);

            UpdateEditor();
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.VariableEditor.folderprompt, Strings.VariableEditor.foldertitle, ref folderName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
            {
                if (!cmbFolder.Items.Contains(folderName))
                {
                    if (lstGameObjects.SelectedNode != null && lstGameObjects.SelectedNode.Tag != null)
                    {
                        if (rdoPlayerVariables.Checked)
                        {
                            var obj = PlayerVariableBase.Get((Guid) lstGameObjects.SelectedNode.Tag);
                            obj.Folder = folderName;
                            mExpandedFolders.Add(folderName);
                        }
                        else if (rdoGlobalVariables.Checked)
                        {
                            var obj = ServerVariableBase.Get((Guid) lstGameObjects.SelectedNode.Tag);
                            obj.Folder = folderName;
                            mGlobalExpandedFolders.Add(folderName);
                        } 
                        else if (rdoInstanceVariables.Checked)
                        {
                            var obj = InstanceVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                            obj.Folder = folderName;
                            mInstanceExpandedFolders.Add(folderName);
                        }
                        else if (rdoGuildVariables.Checked)
                        {
                            var obj = GuildVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                            obj.Folder = folderName;
                            mGuildExpandedFolders.Add(folderName);
                        }

                        InitEditor();
                        cmbFolder.Text = folderName;
                    }
                }
            }
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGameObjects.SelectedNode != null && lstGameObjects.SelectedNode.Tag != null)
            {
                if (rdoPlayerVariables.Checked)
                {
                    var obj = PlayerVariableBase.Get((Guid) lstGameObjects.SelectedNode.Tag);
                    obj.Folder = cmbFolder.Text;
                }
                else if (rdoGlobalVariables.Checked)
                {
                    var obj = ServerVariableBase.Get((Guid) lstGameObjects.SelectedNode.Tag);
                    obj.Folder = cmbFolder.Text;
                }
                else if (rdoInstanceVariables.Checked)
                {
                    var obj = InstanceVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                    obj.Folder = cmbFolder.Text;
                }
                else if (rdoGuildVariables.Checked)
                {
                    var obj = GuildVariableBase.Get((Guid)lstGameObjects.SelectedNode.Tag);
                    obj.Folder = cmbFolder.Text;
                }

                InitEditor();
            }
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
                txtSearch.Text = Strings.VariableEditor.searchplaceholder;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.VariableEditor.searchplaceholder;
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.VariableEditor.searchplaceholder;
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == Strings.VariableEditor.searchplaceholder)
            {
                txtSearch.SelectAll();
            }
        }

        #endregion

        private void chkRecordable_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdoPlayerVariables.Checked)
            {
                return;
            }
            ((PlayerVariableBase)mEditorItem).Recordable = chkRecordable.Checked;
            chkRecordLow.Enabled = chkRecordable.Checked;
            chkSilent.Enabled = chkRecordable.Checked;
            chkSoloOnly.Enabled = chkRecordable.Checked;
        }

        private void chkRecordLow_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdoPlayerVariables.Checked)
            {
                return;
            }
            ((PlayerVariableBase)mEditorItem).RecordLow = chkRecordLow.Checked;
        }

        private void chkSilent_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdoPlayerVariables.Checked)
            {
                return;
            }
            ((PlayerVariableBase)mEditorItem).RecordSilently = chkSilent.Checked;
        }

        private void InitializeGroups()
        {
            mKnownGroups.Sort();
            cmbVariableGroup.Items.Clear();
            cmbVariableGroup.Items.Add(string.Empty);
            cmbVariableGroup.Items.AddRange(mKnownGroups.ToArray());
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (!rdoPlayerVariables.Checked)
            {
                return;
            }

            var groupName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.ItemEditor.newtagprompt, Strings.ItemEditor.newtagtitle, ref groupName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(groupName))
            {
                if (!cmbVariableGroup.Items.Contains(groupName))
                {
                    ((PlayerVariableBase)mEditorItem).VariableGroup = groupName;
                    mKnownGroups.Add(groupName);
                    // load/sort new tags
                    InitializeGroups();
                    cmbVariableGroup.Text = ((PlayerVariableBase)mEditorItem).VariableGroup;
                }
            }
        }

        private void cmbVariableGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!rdoPlayerVariables.Checked && IsPopulating)
            {
                return;
            }

            ((PlayerVariableBase)mEditorItem).VariableGroup = (string)cmbVariableGroup.SelectedItem;
        }

        private void chkSoloOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdoPlayerVariables.Checked)
            {
                return;
            }
            ((PlayerVariableBase)mEditorItem).SoloRecordOnly = chkSoloOnly.Checked;
        }

        private void rdoGuildVariables_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem = null;
            lstGameObjects.ClearExpandedFolders();
            InitEditor();
        }


        private GameObjectType SelectedGameObject
        {
            get
            {
                if (rdoPlayerVariables.Checked)
                {
                    return GameObjectType.PlayerVariable;
                }
                if (rdoGlobalVariables.Checked)
                {
                    return GameObjectType.ServerVariable;
                }
                if (rdoInstanceVariables.Checked)
                {
                    return GameObjectType.InstanceVariable;
                }
                if (rdoGuildVariables.Checked)
                {
                    return GameObjectType.GuildVariable;
                }

                return GameObjectType.PlayerVariable;
            }
        }

        private VariableTypes SelectedVariableType
        {
            get
            {
                if (rdoPlayerVariables.Checked)
                {
                    return VariableTypes.PlayerVariable;
                }
                if (rdoGlobalVariables.Checked)
                {
                    return VariableTypes.ServerVariable;
                }
                if (rdoInstanceVariables.Checked)
                {
                    return VariableTypes.InstanceVariable;
                }
                if (rdoGuildVariables.Checked)
                {
                    return VariableTypes.GuildVariable;
                }

                return VariableTypes.PlayerVariable;
            }
        }
    }

}
