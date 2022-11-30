using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Intersect.Editor.Content;
using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmRecipe : EditorForm
    {
        private List<RecipeDescriptor> mChanged = new List<RecipeDescriptor>();

        private RecipeDescriptor mEditorItem;

        private string mCopiedItem;

        private bool mPopulating = false;

        private List<string> mKnownFolders = new List<string>();

        private List<RecipeRequirement> mRecipeReqs;

        public frmRecipe()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                grpProperties.Hide();
            }

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<RecipeDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                RecipeDescriptor.Lookup,
                btnAlphabetical
            );

            cmbCraftType.Items.Clear();
            cmbCraftType.Items.AddRange(EnumExtensions.GetDescriptions(typeof(RecipeCraftType)));

            cmbTriggerType.Items.Clear();
            cmbTriggerType.Items.AddRange(EnumExtensions.GetDescriptions(typeof(RecipeTrigger)));

            cmbImage.Items.Clear();
            cmbImage.Items.Add(Strings.General.none);
            var itemnames = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Item);
            cmbImage.Items.AddRange(itemnames);
        }

        private void LoadTriggerParams()
        {
            cmbTriggerParams.Items.Clear();
            var triggerType = (RecipeTrigger)cmbTriggerType.SelectedIndex;
            switch (triggerType)
            {
                case RecipeTrigger.None:
                    break;
                default:
                    cmbTriggerParams.Items.AddRange(triggerType.GetRelatedTable().Names());
                    break;
            }
            cmbTriggerParams.SelectedIndex = -1;
            cmbTriggerParams.Text = string.Empty;
        }

        private Guid GetTriggerParamValue()
        {
            var triggerType = (RecipeTrigger)cmbTriggerType.SelectedIndex;
            switch (triggerType)
            {
                case RecipeTrigger.None:
                    return Guid.Empty;
                default:
                    return triggerType.GetRelatedTable().IdFromList(cmbTriggerParams.SelectedIndex);
            }
        }

        private void SetTriggerParamValue()
        {
            var triggerType = (RecipeTrigger)cmbTriggerType.SelectedIndex;
            switch (triggerType)
            {
                case RecipeTrigger.None:
                    cmbTriggerParams.SelectedIndex = -1;
                    cmbTriggerParams.Text = string.Empty;
                    return;
                default:
                    cmbTriggerParams.SelectedIndex = triggerType.GetRelatedTable().ListIndex(mEditorItem.TriggerParam);
                    return;
            }
        }

        public void UpdateDisabled()
        {
            cmbTriggerParams.Enabled = cmbTriggerType.SelectedIndex != (int)RecipeTrigger.None;
        }

        private void UpdateFields()
        {
            mPopulating = true;

            mRecipeReqs = mEditorItem.RecipeRequirements;

            txtName.Text = mEditorItem.Name;
            cmbFolder.Text = mEditorItem.Folder;

            txtLabel.Text = mEditorItem.DisplayName;
            txtHint.Text = mEditorItem.Hint;
            cmbCraftType.SelectedIndex = mEditorItem.CraftTypeValue;
            cmbImage.SelectedIndex = cmbImage.FindString(TextUtils.NullToNone(mEditorItem.Image));
            picItem.BackgroundImage?.Dispose();
            picItem.BackgroundImage = null;
            if (cmbImage.SelectedIndex > 0)
            {
                DrawItemIcon();
            }
            chkHidden.Checked = mEditorItem.HiddenUntilUnlocked;

            cmbTriggerType.SelectedIndex = mEditorItem.TriggerValue;
            LoadTriggerParams();
            SetTriggerParamValue();

            RefreshRequirementsList();

            UpdateDisabled();

            mPopulating = false;
        }

        private void UpdateEditor()
        {
            FormHelpers.UpdateEditor(
                ref mEditorItem,
                ref mChanged,
                ref grpProperties,
                UpdateToolStripItems,
                UpdateFields
            );
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = RecipeDescriptor.Get(id);
            UpdateEditor();
        }

        #region Generics
        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, ref toolStripItemPaste, ref toolStripItemUndo, ref toolStripItemDelete, mCopiedItem, mEditorItem, lstGameObjects);
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.Recipe);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripDeleteClicked(mEditorItem, lstGameObjects);
        }

        private void btnAlphabetical_Click(object sender, EventArgs e)
        {
            FormHelpers.AlphabeticalClicked(ref btnAlphabetical, InitEditor);
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripCopyClicked(ref mCopiedItem, mEditorItem, lstGameObjects, ref toolStripItemPaste);
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripPasteClicked(ref mEditorItem, mCopiedItem, lstGameObjects, UpdateEditor);
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripUndoClicked(mChanged, ref mEditorItem, UpdateEditor);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.SearchTextChanged(InitEditor);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            FormHelpers.ClearSearchPressed(ref txtSearch);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormHelpers.SaveClicked(ref mChanged, Hide, Dispose);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHelpers.CancelClicked(ref mChanged, Hide, Dispose);
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelpers.FolderChanged(ref mEditorItem, cmbFolder, InitEditor);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FormHelpers.AddFolder(ref mEditorItem, ref cmbFolder, ref lstGameObjects, InitEditor);
        }
        #endregion

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtLabel.Text;
        }

        private void txtHint_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Hint = txtHint.Text;
        }

        private void cmbTriggerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            UpdateDisabled();
            LoadTriggerParams();
        }

        private void cmbTriggerParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            mEditorItem.TriggerParam = GetTriggerParamValue();
        }

        private void cmbCraftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CraftTypeValue = cmbCraftType.SelectedIndex;
        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Image = TextUtils.SanitizeNone(cmbImage.Text);
            DrawItemIcon();
        }

        private void chkHidden_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.HiddenUntilUnlocked = chkHidden.Checked;
        }

        private void btnDynamicRequirements_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(mEditorItem.Requirements, RequirementType.RecipeUnlock);
            frm.ShowDialog();
        }

        private void DrawItemIcon()
        {
            var picItemBmp = new Bitmap(picItem.Width, picItem.Height);
            var gfx = Graphics.FromImage(picItemBmp);
            gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, picItem.Width, picItem.Height));
            if (cmbImage.SelectedIndex > 0)
            {
                var img = Image.FromFile("resources/items/" + cmbImage.Text);

                gfx.DrawImage(
                    img, new Rectangle(0, 0, img.Width, img.Height),
                    0, 0, img.Width, img.Height, GraphicsUnit.Pixel
                );

                img.Dispose();
            }

            gfx.Dispose();

            picItem.BackgroundImage = picItemBmp;
        }

        private void btnAddReq_Click(object sender, EventArgs e)
        {
            var newReq = new RecipeRequirement(mEditorItem.Id, cmbTriggerType.SelectedIndex, (int)nudAmt.Value);
            RefreshRequirementsList();
        }

        private void RefreshRequirementsList()
        {
            lstRequirements.Items.Clear();
            foreach(var req in mEditorItem.RecipeRequirements)
            {
                lstRequirements.Items.Add(req.ToString());
            }
        }

        private void btnRemoveReq_Click(object sender, EventArgs e)
        {
            var idx = lstRequirements.SelectedIndex;

            if (idx < 0)
            {
                return;
            }

            mEditorItem.RecipeRequirements.RemoveAt(idx);
            RefreshRequirementsList();
        }
    }
}
