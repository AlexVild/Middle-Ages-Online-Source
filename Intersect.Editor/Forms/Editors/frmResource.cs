using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;

using Intersect.Editor.Content;
using Intersect.Editor.Extensions;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors
{

    public partial class FrmResource : EditorForm
    {

        private List<ResourceBase> mChanged = new List<ResourceBase>();

        private string mCopiedItem;

        private ResourceBase mEditorItem;

        private Bitmap mEndBitmap;

        private Bitmap mEndGraphic;

        private Bitmap mInitialBitmap;

        private Bitmap mInitialGraphic;

        private List<string> mKnownFolders = new List<string>();

        private bool mMouseDown;

        private bool mDropTypeUpdate = false;

        //General Editting Variables
        bool mTMouseDown;

        public FrmResource()
        {
            ApplyHooks();
            InitializeComponent();

            cmbToolType.Items.Clear();
            cmbToolType.Items.Add(Strings.General.none);
            cmbToolType.Items.AddRange(Options.ToolTypes.ToArray());
            cmbEvent.Items.Clear();
            cmbEvent.Items.Add(Strings.General.none);
            cmbEvent.Items.AddRange(EventBase.Names);

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }
        private void AssignEditorItem(Guid id)
        {
            mEditorItem = ResourceBase.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.Resource)
            {
                InitEditor();
                if (mEditorItem != null && !ResourceBase.Lookup.Values.Contains(mEditorItem))
                {
                    mEditorItem = null;
                    UpdateEditor();
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

        private void frmResource_Load(object sender, EventArgs e)
        {
            mInitialBitmap = new Bitmap(picInitialResource.Width, picInitialResource.Height);
            mEndBitmap = new Bitmap(picInitialResource.Width, picInitialResource.Height);

            cmbAnimation.Items.Clear();
            cmbAnimation.Items.Add(Strings.General.none);
            cmbAnimation.Items.AddRange(AnimationBase.Names);
            FillItemCombobox(DropType.Item);
            InitLocalization();
            UpdateEditor();
        }

        private void PopulateInitialGraphicList()
        {
            cmbInitialSprite.Items.Clear();
            cmbInitialSprite.Items.Add(Strings.General.none);
            var resources = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Resource);
            if (mEditorItem.Initial.GraphicFromTileset)
            {
                resources = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Tileset);
            }

            for (var i = 0; i < resources.Length; i++)
            {
                cmbInitialSprite.Items.Add(resources[i]);
            }

            if (mEditorItem != null)
            {
                if (mEditorItem.Initial.Graphic != null && cmbInitialSprite.Items.Contains(mEditorItem.Initial.Graphic))
                {
                    cmbInitialSprite.SelectedIndex = cmbInitialSprite.FindString(
                        TextUtils.NullToNone(TextUtils.NullToNone(mEditorItem.Initial.Graphic))
                    );

                    return;
                }
            }

            cmbInitialSprite.SelectedIndex = 0;
        }

        private void PopulateExhaustedGraphicList()
        {
            cmbEndSprite.Items.Clear();
            cmbEndSprite.Items.Add(Strings.General.none);
            var resources = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Resource);
            if (mEditorItem.Exhausted.GraphicFromTileset)
            {
                resources = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Tileset);
            }

            for (var i = 0; i < resources.Length; i++)
            {
                cmbEndSprite.Items.Add(resources[i]);
            }

            if (mEditorItem != null)
            {
                if (mEditorItem.Exhausted.Graphic != null && cmbEndSprite.Items.Contains(mEditorItem.Exhausted.Graphic))
                {
                    cmbEndSprite.SelectedIndex = cmbEndSprite.FindString(
                        TextUtils.NullToNone(TextUtils.NullToNone(mEditorItem.Exhausted.Graphic))
                    );

                    return;
                }
            }

            cmbEndSprite.SelectedIndex = 0;
        }

        private void InitLocalization()
        {
            Text = Strings.ResourceEditor.title;
            toolStripItemNew.Text = Strings.ResourceEditor.New;
            toolStripItemDelete.Text = Strings.ResourceEditor.delete;
            toolStripItemCopy.Text = Strings.ResourceEditor.copy;
            toolStripItemPaste.Text = Strings.ResourceEditor.paste;
            toolStripItemUndo.Text = Strings.ResourceEditor.undo;

            grpResources.Text = Strings.ResourceEditor.resources;

            grpGeneral.Text = Strings.ResourceEditor.general;
            lblName.Text = Strings.ResourceEditor.name;
            lblToolType.Text = Strings.ResourceEditor.tooltype;
            lblHP.Text = Strings.ResourceEditor.minhp;
            lblMaxHp.Text = Strings.ResourceEditor.maxhp;
            lblSpawnDuration.Text = Strings.ResourceEditor.spawnduration;
            lblAnimation.Text = Strings.ResourceEditor.animation;
            chkWalkableBefore.Text = Strings.ResourceEditor.walkablebefore;
            chkWalkableAfter.Text = Strings.ResourceEditor.walkableafter;

            grpDrops.Text = Strings.ResourceEditor.drops;
            lblDropItem.Text = Strings.ResourceEditor.dropitem;
            lblDropAmount.Text = Strings.ResourceEditor.dropamount;
            lblDropChance.Text = Strings.ResourceEditor.dropchance;
            btnDropAdd.Text = Strings.ResourceEditor.dropadd;
            btnDropRemove.Text = Strings.ResourceEditor.dropremove;

            grpRegen.Text = Strings.ResourceEditor.regen;
            lblHpRegen.Text = Strings.ResourceEditor.hpregen;
            lblRegenHint.Text = Strings.ResourceEditor.regenhint;

            grpGraphics.Text = Strings.ResourceEditor.graphics;
            lblPic.Text = Strings.ResourceEditor.initialgraphic;
            lblPic2.Text = Strings.ResourceEditor.exhaustedgraphic;

            chkExhaustedBelowEntities.Text = Strings.ResourceEditor.belowentities;
            chkInitialBelowEntities.Text = Strings.ResourceEditor.belowentities;
            chkInitialFromTileset.Text = Strings.ResourceEditor.fromtileset;
            chkExhaustedFromTileset.Text = Strings.ResourceEditor.fromtileset;

            grpCommonEvent.Text = Strings.ResourceEditor.commonevent;
            lblEvent.Text = Strings.ResourceEditor.harvestevent;

            grpRequirements.Text = Strings.ResourceEditor.requirementsgroup;
            lblCannotHarvest.Text = Strings.ResourceEditor.cannotharvest;
            btnRequirements.Text = Strings.ResourceEditor.requirements;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.ResourceEditor.sortalphabetically;
            txtSearch.Text = Strings.ResourceEditor.searchplaceholder;
            lblFolder.Text = Strings.ResourceEditor.folderlabel;
            
            lblExp.Text = Strings.ResourceEditor.inspirationexp;

            btnSave.Text = Strings.ResourceEditor.save;
            btnCancel.Text = Strings.ResourceEditor.cancel;
        }

        private void UpdateEditor()
        {
            if (mEditorItem != null)
            {
                pnlContainer.Show();

                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                cmbToolType.SelectedIndex = mEditorItem.Tool + 1;
                nudSpawnDuration.Value = mEditorItem.SpawnDuration;
                cmbAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.AnimationId) + 1;
                nudMinHp.Value = mEditorItem.MinHp;
                nudMaxHp.Value = mEditorItem.MaxHp;
                chkWalkableBefore.Checked = mEditorItem.WalkableBefore;
                chkWalkableAfter.Checked = mEditorItem.WalkableAfter;
                chkInitialFromTileset.Checked = mEditorItem.Initial.GraphicFromTileset;
                chkExhaustedFromTileset.Checked = mEditorItem.Exhausted.GraphicFromTileset;
                cmbEvent.SelectedIndex = EventBase.ListIndex(mEditorItem.EventId) + 1;
                chkInitialBelowEntities.Checked = mEditorItem.Initial.RenderBelowEntities;
                chkExhaustedBelowEntities.Checked = mEditorItem.Exhausted.RenderBelowEntities;
                txtCannotHarvest.Text = mEditorItem.CannotHarvestMessage;
                nudExp.Value = mEditorItem.Experience;
                chkDoNotRecord.Checked = mEditorItem.DoNotRecord;
                cmbResourceGroup.Text = mEditorItem.ResourceGroup;

                txtDisplayName.Text = mEditorItem.DisplayName;
                nudRP.Value = mEditorItem.RP;

                //Regen
                nudHpRegen.Value = mEditorItem.VitalRegen;
                PopulateInitialGraphicList();
                PopulateExhaustedGraphicList();
                UpdateDropValues();
                Render();
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
                    }
                    else
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

        private void nudSpawnDuration_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.SpawnDuration = (int) nudSpawnDuration.Value;
        }

        private void cmbToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Tool = cmbToolType.SelectedIndex - 1;
        }

        private void chkWalkableBefore_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.WalkableBefore = chkWalkableBefore.Checked;
        }

        private void chkWalkableAfter_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.WalkableAfter = chkWalkableAfter.Checked;
        }

        private void cmbInitialSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            mInitialGraphic?.Dispose();
            mInitialGraphic = null;
            if (cmbInitialSprite.SelectedIndex > 0)
            {
                mEditorItem.Initial.Graphic = cmbInitialSprite.Text;
                var graphic = Path.Combine(
                    "resources", mEditorItem.Initial.GraphicFromTileset ? "tilesets" : "resources",
                    cmbInitialSprite.Text
                );

                if (File.Exists(graphic))
                {
                    mInitialGraphic = (Bitmap) Image.FromFile(graphic);
                    picInitialResource.Width = mInitialGraphic.Width * Options.Scale;
                    picInitialResource.Height = mInitialGraphic.Height * Options.Scale;
                    mInitialBitmap = new Bitmap(picInitialResource.Width, picInitialResource.Height);
                }
            }
            else
            {
                mEditorItem.Initial.Graphic = null;
            }

            picInitialResource.Visible = mInitialGraphic != null;
            Render();
        }

        private void cmbEndSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEndGraphic?.Dispose();
            mEndGraphic = null;
            if (cmbEndSprite.SelectedIndex > 0)
            {
                mEditorItem.Exhausted.Graphic = cmbEndSprite.Text;
                var graphic = Path.Combine(
                    "resources", mEditorItem.Exhausted.GraphicFromTileset ? "tilesets" : "resources", cmbEndSprite.Text
                );

                if (File.Exists(graphic))
                {
                    mEndGraphic = (Bitmap) Image.FromFile(graphic);
                    picEndResource.Width = mEndGraphic.Width * Options.Scale;
                    picEndResource.Height = mEndGraphic.Height * Options.Scale;
                    mEndBitmap = new Bitmap(picEndResource.Width, picEndResource.Height);
                }
            }
            else
            {
                mEditorItem.Exhausted.Graphic = null;
            }

            picEndResource.Visible = mEndGraphic != null;
            Render();
        }

        public void Render()
        {
            if (mEditorItem == null)
            {
                return;
            }

            // Initial Sprite
            var gfx = Graphics.FromImage(mInitialBitmap);
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, picInitialResource.Width, picInitialResource.Height));
            if (cmbInitialSprite.SelectedIndex > 0 && mInitialGraphic != null)
            {
                gfx.DrawImage(
                    mInitialGraphic, new Rectangle(0, 0, mInitialGraphic.Width * Options.Scale, mInitialGraphic.Height * Options.Scale),
                    new Rectangle(0, 0, mInitialGraphic.Width, mInitialGraphic.Height), GraphicsUnit.Pixel
                );
            }

            if (mEditorItem.Initial.GraphicFromTileset)
            {
                var selX = mEditorItem.Initial.X;
                var selY = mEditorItem.Initial.Y;
                var selW = mEditorItem.Initial.Width;
                var selH = mEditorItem.Initial.Height;
                if (selW < 0)
                {
                    selX -= Math.Abs(selW);
                    selW = Math.Abs(selW);
                }

                if (selH < 0)
                {
                    selY -= Math.Abs(selH);
                    selH = Math.Abs(selH);
                }

                gfx.DrawRectangle(
                    new Pen(System.Drawing.Color.White, 2f),
                    new Rectangle(
                        selX * Options.TileWidth, selY * Options.TileHeight,
                        Options.TileWidth + selW * Options.TileWidth, Options.TileHeight + selH * Options.TileHeight
                    )
                );
            }

            gfx.Dispose();
            gfx = picInitialResource.CreateGraphics();
            gfx.DrawImageUnscaled(mInitialBitmap, new System.Drawing.Point(0, 0));

            gfx.Dispose();

            // End Sprite
            gfx = Graphics.FromImage(mEndBitmap);
            gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, picEndResource.Width, picEndResource.Height));
            if (cmbEndSprite.SelectedIndex > 0 && mEndGraphic != null)
            {
                gfx.DrawImage(
                    mEndGraphic, new Rectangle(0, 0, mEndGraphic.Width * Options.Scale, mEndGraphic.Height * Options.Scale),
                    new Rectangle(0, 0, mEndGraphic.Width, mEndGraphic.Height), GraphicsUnit.Pixel
                );
            }

            if (mEditorItem.Exhausted.GraphicFromTileset)
            {
                var selX = mEditorItem.Exhausted.X;
                var selY = mEditorItem.Exhausted.Y;
                var selW = mEditorItem.Exhausted.Width;
                var selH = mEditorItem.Exhausted.Height;
                if (selW < 0)
                {
                    selX -= Math.Abs(selW);
                    selW = Math.Abs(selW);
                }

                if (selH < 0)
                {
                    selY -= Math.Abs(selH);
                    selH = Math.Abs(selH);
                }

                gfx.DrawRectangle(
                    new Pen(System.Drawing.Color.White, 2f),
                    new Rectangle(
                        selX * Options.TileWidth, selY * Options.TileHeight,
                        Options.TileWidth + selW * Options.TileWidth, Options.TileHeight + selH * Options.TileHeight
                    )
                );
            }

            gfx.Dispose();
            gfx = picEndResource.CreateGraphics();
            gfx.DrawImageUnscaled(mEndBitmap, new System.Drawing.Point(0, 0));
            gfx.Dispose();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
        }

        private void frmResource_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.CurrentEditor = -1;
        }

        private void tmrRender_Tick(object sender, EventArgs e)
        {
            Render();
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.Resource);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                if (MessageBoxExtensions.ShowWarning(
                        Strings.ResourceEditor.deleteprompt, Strings.ResourceEditor.deletetitle, DarkDialogButton.YesNo,
                        Properties.Resources.Icon
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

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            if (mChanged.Contains(mEditorItem) && mEditorItem != null)
            {
                if (MessageBoxExtensions.ShowWarning(
                        Strings.ResourceEditor.undoprompt, Strings.ResourceEditor.undotitle, DarkDialogButton.YesNo,
                        Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    mEditorItem.RestoreBackup();
                    UpdateEditor();
                }
            }
        }

        private void UpdateToolStripItems()
        {
            toolStripItemCopy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemPaste.Enabled = mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused;
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.N)
                {
                    toolStripItemNew_Click(null, null);
                }
            }
        }

        private void btnRequirements_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(mEditorItem.HarvestingRequirements, RequirementType.Resource);
            frm.ShowDialog();
        }

        private void cmbAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Animation = AnimationBase.Get(AnimationBase.IdFromList(cmbAnimation.SelectedIndex - 1));
        }

        private void nudMinHp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MinHp = (int) nudMinHp.Value;
        }

        private void nudMaxHp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MaxHp = (int) nudMaxHp.Value;
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

            mEditorItem.Drops[(int) lstDrops.SelectedIndex].Quantity = (int) nudDropAmount.Value;
            UpdateDropValues(true);
        }

        private void lstDrops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mDropTypeUpdate)
            {
                return;
            }
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

        private void nudDropChance_ValueChanged(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex < lstDrops.Items.Count)
            {
                return;
            }

            mEditorItem.Drops[(int) lstDrops.SelectedIndex].Chance = (double) nudDropChance.Value;
            UpdateDropValues(true);
        }

        private void chkInitialFromTileset_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Initial.GraphicFromTileset = chkInitialFromTileset.Checked;
            PopulateInitialGraphicList();
        }

        private void chkExhaustedFromTileset_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Exhausted.GraphicFromTileset = chkExhaustedFromTileset.Checked;
            PopulateExhaustedGraphicList();
        }

        private void picInitialResource_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > picInitialResource.Width || e.Y > picInitialResource.Height)
            {
                return;
            }

            if (!chkInitialFromTileset.Checked)
            {
                return;
            }

            mMouseDown = true;
            mEditorItem.Initial.X = (int) Math.Floor((double) e.X / Options.TileWidth);
            mEditorItem.Initial.Y = (int) Math.Floor((double) e.Y / Options.TileHeight);
            mEditorItem.Initial.Width = 0;
            mEditorItem.Initial.Height = 0;
            if (mEditorItem.Initial.X < 0)
            {
                mEditorItem.Initial.X = 0;
            }

            if (mEditorItem.Initial.Y < 0)
            {
                mEditorItem.Initial.Y = 0;
            }

            Render();
        }

        private void picInitialResource_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X > picInitialResource.Width || e.Y > picInitialResource.Height)
            {
                return;
            }

            if (!chkInitialFromTileset.Checked)
            {
                return;
            }

            var selX = mEditorItem.Initial.X;
            var selY = mEditorItem.Initial.Y;
            var selW = mEditorItem.Initial.Width;
            var selH = mEditorItem.Initial.Height;
            if (selW < 0)
            {
                selX -= Math.Abs(selW);
                selW = Math.Abs(selW);
            }

            if (selH < 0)
            {
                selY -= Math.Abs(selH);
                selH = Math.Abs(selH);
            }

            mEditorItem.Initial.X = selX;
            mEditorItem.Initial.Y = selY;
            mEditorItem.Initial.Width = selW;
            mEditorItem.Initial.Height = selH;
            mMouseDown = false;
            Render();
        }

        private void picInitialResource_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > picInitialResource.Width || e.Y > picInitialResource.Height)
            {
                return;
            }

            if (!chkInitialFromTileset.Checked)
            {
                return;
            }

            if (mMouseDown)
            {
                var tmpX = (int) Math.Floor((double) e.X / Options.TileWidth);
                var tmpY = (int) Math.Floor((double) e.Y / Options.TileHeight);
                mEditorItem.Initial.Width = tmpX - mEditorItem.Initial.X;
                mEditorItem.Initial.Height = tmpY - mEditorItem.Initial.Y;
            }

            Render();
        }

        private void picExhustedResource_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > picEndResource.Width || e.Y > picEndResource.Height)
            {
                return;
            }

            if (!chkExhaustedFromTileset.Checked)
            {
                return;
            }

            mMouseDown = true;
            mEditorItem.Exhausted.X = (int) Math.Floor((double) e.X / Options.TileWidth);
            mEditorItem.Exhausted.Y = (int) Math.Floor((double) e.Y / Options.TileHeight);
            mEditorItem.Exhausted.Width = 0;
            mEditorItem.Exhausted.Height = 0;
            if (mEditorItem.Exhausted.X < 0)
            {
                mEditorItem.Exhausted.X = 0;
            }

            if (mEditorItem.Exhausted.Y < 0)
            {
                mEditorItem.Exhausted.Y = 0;
            }

            Render();
        }

        private void picExhaustedResource_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.X > picEndResource.Width || e.Y > picEndResource.Height)
            {
                return;
            }

            if (!chkExhaustedFromTileset.Checked)
            {
                return;
            }

            var selX = mEditorItem.Exhausted.X;
            var selY = mEditorItem.Exhausted.Y;
            var selW = mEditorItem.Exhausted.Width;
            var selH = mEditorItem.Exhausted.Height;
            if (selW < 0)
            {
                selX -= Math.Abs(selW);
                selW = Math.Abs(selW);
            }

            if (selH < 0)
            {
                selY -= Math.Abs(selH);
                selH = Math.Abs(selH);
            }

            mEditorItem.Exhausted.X = selX;
            mEditorItem.Exhausted.Y = selY;
            mEditorItem.Exhausted.Width = selW;
            mEditorItem.Exhausted.Height = selH;
            mMouseDown = false;
            Render();
        }

        private void picExhaustedResource_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > picEndResource.Width || e.Y > picEndResource.Height)
            {
                return;
            }

            if (!chkExhaustedFromTileset.Checked)
            {
                return;
            }

            if (mMouseDown)
            {
                var tmpX = (int) Math.Floor((double) e.X / Options.TileWidth);
                var tmpY = (int) Math.Floor((double) e.Y / Options.TileHeight);
                mEditorItem.Exhausted.Width = tmpX - mEditorItem.Exhausted.X;
                mEditorItem.Exhausted.Height = tmpY - mEditorItem.Exhausted.Y;
            }

            Render();
        }

        private void nudHpRegen_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalRegen = (int) nudHpRegen.Value;
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Event = EventBase.Get(EventBase.IdFromList(cmbEvent.SelectedIndex - 1));
        }

        private void chkInitialBelowEntities_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Initial.RenderBelowEntities = chkInitialBelowEntities.Checked;
        }

        private void chkExhaustedBelowEntities_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Exhausted.RenderBelowEntities = chkExhaustedBelowEntities.Checked;
        }

        private void txtCannotHarvest_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.CannotHarvestMessage = txtCannotHarvest.Text;
        }

        #region "Item List - Folders, Searching, Sorting, Etc"

        public override void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in ResourceBase.Lookup)
            {
                ResourceBase resource = (ResourceBase) itm.Value;
                if (!string.IsNullOrEmpty(resource.Folder) &&
                    !mFolders.Contains(resource.Folder))
                {
                    mFolders.Add(resource.Folder);
                    if (!mKnownFolders.Contains(resource.Folder))
                    {
                        mKnownFolders.Add(resource.Folder);
                    }
                }

                if (!string.IsNullOrWhiteSpace(resource.ResourceGroup) && !mKnownResourceGroups.Contains(resource.ResourceGroup))
                {
                    mKnownResourceGroups.Add(resource.ResourceGroup);
                }
            }

            mKnownResourceGroups.Sort();
            cmbResourceGroup.Items.Clear();
            cmbResourceGroup.Items.Add(string.Empty);
            cmbResourceGroup.Items.AddRange(mKnownResourceGroups.ToArray());

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            var items = ResourceBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((ResourceBase)pair.Value)?.Name ?? Models.DatabaseObject<ResourceBase>.Deleted, ((ResourceBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.ResourceEditor.folderprompt, Strings.ResourceEditor.foldertitle, ref folderName,
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
                txtSearch.Text = Strings.ResourceEditor.searchplaceholder;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.ResourceEditor.searchplaceholder;
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.ResourceEditor.searchplaceholder;
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == Strings.ResourceEditor.searchplaceholder)
            {
                txtSearch.SelectAll();
            }
        }

        #endregion

        private void nudExp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Experience = (long) nudExp.Value;
        }

        private void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.DoNotRecord = chkDoNotRecord.Checked;
        }

        private void btnAddResourceGroup_Click(object sender, EventArgs e)
        {
            var resourceGroupName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.ResourceEditor.ResourceGroupPrompt, Strings.ResourceEditor.ResourceGroupEditor, ref resourceGroupName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(resourceGroupName))
            {
                if (!cmbResourceGroup.Items.Contains(resourceGroupName))
                {
                    mEditorItem.ResourceGroup = resourceGroupName;
                    mKnownResourceGroups.Add(resourceGroupName);
                    InitEditor();
                    cmbResourceGroup.Text = resourceGroupName;
                }
            }
        }

        private List<string> mKnownResourceGroups = new List<string>();

        private void cmbResourceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ResourceGroup = cmbResourceGroup.Text;
        }

        private void rdoItem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDropType(DropType.Item);
        }

        private void rdoTable_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDropType(DropType.Table);
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

        private void FillItemCombobox(DropType dropType)
        {
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
        }

        private void btnUnselectItem_Click(object sender, EventArgs e)
        {
            lstDrops.SelectedIndex = -1;
        }

        private void nudRP_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.RP = (int)nudRP.Value;
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.DisplayName = txtDisplayName.Text;
        }
    }
}
