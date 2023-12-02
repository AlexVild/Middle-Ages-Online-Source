
namespace Intersect.Editor.Forms.Editors
{
    partial class frmRecipe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecipe));
            this.toolStrip = new DarkUI.Controls.DarkToolStrip();
            this.toolStripItemNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAlphabetical = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripItemPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemUndo = new System.Windows.Forms.ToolStripButton();
            this.grpRecipes = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpProperties = new DarkUI.Controls.DarkGroupBox();
            this.grpRequirements = new DarkUI.Controls.DarkGroupBox();
            this.chkRequireTrue = new DarkUI.Controls.DarkCheckBox();
            this.btnRemoveAll = new DarkUI.Controls.DarkButton();
            this.btnClearSelection = new DarkUI.Controls.DarkButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRequireHint = new DarkUI.Controls.DarkTextBox();
            this.btnRemoveReq = new DarkUI.Controls.DarkButton();
            this.btnAddReq = new DarkUI.Controls.DarkButton();
            this.lblValue = new System.Windows.Forms.Label();
            this.nudAmt = new DarkUI.Controls.DarkNumericUpDown();
            this.lstRequirements = new System.Windows.Forms.ListBox();
            this.cmbTriggerParams = new DarkUI.Controls.DarkComboBox();
            this.lblTriggerType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTriggerType = new DarkUI.Controls.DarkComboBox();
            this.grpLogic = new DarkUI.Controls.DarkGroupBox();
            this.btnDynamicRequirements = new DarkUI.Controls.DarkButton();
            this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.chkHidden = new DarkUI.Controls.DarkCheckBox();
            this.cmbImage = new DarkUI.Controls.DarkComboBox();
            this.lblCraftImage = new System.Windows.Forms.Label();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.cmbCraftType = new DarkUI.Controls.DarkComboBox();
            this.lblCraftType = new System.Windows.Forms.Label();
            this.txtHint = new DarkUI.Controls.DarkTextBox();
            this.txtLabel = new DarkUI.Controls.DarkTextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.nudMinCr = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMinimumCr = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.grpRecipes.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.grpRequirements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).BeginInit();
            this.grpLogic.SuspendLayout();
            this.darkGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinCr)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripItemNew,
            this.toolStripSeparator1,
            this.toolStripItemDelete,
            this.toolStripSeparator2,
            this.btnAlphabetical,
            this.toolStripSeparator4,
            this.toolStripItemCopy,
            this.toolStripItemPaste,
            this.toolStripSeparator3,
            this.toolStripItemUndo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(1021, 25);
            this.toolStrip.TabIndex = 48;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripItemNew
            // 
            this.toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemNew.Image")));
            this.toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemNew.Name = "toolStripItemNew";
            this.toolStripItemNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemNew.Text = "New";
            this.toolStripItemNew.Click += new System.EventHandler(this.toolStripItemNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemDelete
            // 
            this.toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemDelete.Enabled = false;
            this.toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemDelete.Image")));
            this.toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemDelete.Name = "toolStripItemDelete";
            this.toolStripItemDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemDelete.Text = "Delete";
            this.toolStripItemDelete.Click += new System.EventHandler(this.toolStripItemDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAlphabetical
            // 
            this.btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnAlphabetical.Image = ((System.Drawing.Image)(resources.GetObject("btnAlphabetical.Image")));
            this.btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlphabetical.Name = "btnAlphabetical";
            this.btnAlphabetical.Size = new System.Drawing.Size(23, 22);
            this.btnAlphabetical.Text = "Order Chronologically";
            this.btnAlphabetical.Click += new System.EventHandler(this.btnAlphabetical_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemCopy
            // 
            this.toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemCopy.Enabled = false;
            this.toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemCopy.Image")));
            this.toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemCopy.Name = "toolStripItemCopy";
            this.toolStripItemCopy.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemCopy.Text = "Copy";
            this.toolStripItemCopy.Click += new System.EventHandler(this.toolStripItemCopy_Click);
            // 
            // toolStripItemPaste
            // 
            this.toolStripItemPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemPaste.Enabled = false;
            this.toolStripItemPaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemPaste.Image")));
            this.toolStripItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemPaste.Name = "toolStripItemPaste";
            this.toolStripItemPaste.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemPaste.Text = "Paste";
            this.toolStripItemPaste.Click += new System.EventHandler(this.toolStripItemPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemUndo
            // 
            this.toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemUndo.Enabled = false;
            this.toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemUndo.Image")));
            this.toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemUndo.Name = "toolStripItemUndo";
            this.toolStripItemUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemUndo.Text = "Undo";
            this.toolStripItemUndo.Click += new System.EventHandler(this.toolStripItemUndo_Click);
            // 
            // grpRecipes
            // 
            this.grpRecipes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRecipes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRecipes.Controls.Add(this.btnClearSearch);
            this.grpRecipes.Controls.Add(this.txtSearch);
            this.grpRecipes.Controls.Add(this.lstGameObjects);
            this.grpRecipes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRecipes.Location = new System.Drawing.Point(12, 28);
            this.grpRecipes.Name = "grpRecipes";
            this.grpRecipes.Size = new System.Drawing.Size(203, 493);
            this.grpRecipes.TabIndex = 49;
            this.grpRecipes.TabStop = false;
            this.grpRecipes.Text = "Recipes";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 18);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(18, 20);
            this.btnClearSearch.TabIndex = 34;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(6, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 20);
            this.txtSearch.TabIndex = 33;
            this.txtSearch.Text = "Search...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lstGameObjects
            // 
            this.lstGameObjects.AllowDrop = true;
            this.lstGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstGameObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstGameObjects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstGameObjects.HideSelection = false;
            this.lstGameObjects.ImageIndex = 0;
            this.lstGameObjects.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lstGameObjects.Location = new System.Drawing.Point(6, 44);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(191, 443);
            this.lstGameObjects.TabIndex = 32;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(734, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(135, 27);
            this.btnSave.TabIndex = 51;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(876, 527);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(139, 27);
            this.btnCancel.TabIndex = 52;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpProperties
            // 
            this.grpProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpProperties.Controls.Add(this.grpRequirements);
            this.grpProperties.Controls.Add(this.grpLogic);
            this.grpProperties.Controls.Add(this.darkGroupBox1);
            this.grpProperties.Controls.Add(this.btnAddFolder);
            this.grpProperties.Controls.Add(this.lblFolder);
            this.grpProperties.Controls.Add(this.cmbFolder);
            this.grpProperties.Controls.Add(this.lblName);
            this.grpProperties.Controls.Add(this.txtName);
            this.grpProperties.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpProperties.Location = new System.Drawing.Point(221, 12);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(794, 482);
            this.grpProperties.TabIndex = 53;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Recipe Properties";
            // 
            // grpRequirements
            // 
            this.grpRequirements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRequirements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRequirements.Controls.Add(this.chkRequireTrue);
            this.grpRequirements.Controls.Add(this.btnRemoveAll);
            this.grpRequirements.Controls.Add(this.btnClearSelection);
            this.grpRequirements.Controls.Add(this.label2);
            this.grpRequirements.Controls.Add(this.txtRequireHint);
            this.grpRequirements.Controls.Add(this.btnRemoveReq);
            this.grpRequirements.Controls.Add(this.btnAddReq);
            this.grpRequirements.Controls.Add(this.lblValue);
            this.grpRequirements.Controls.Add(this.nudAmt);
            this.grpRequirements.Controls.Add(this.lstRequirements);
            this.grpRequirements.Controls.Add(this.cmbTriggerParams);
            this.grpRequirements.Controls.Add(this.lblTriggerType);
            this.grpRequirements.Controls.Add(this.label1);
            this.grpRequirements.Controls.Add(this.cmbTriggerType);
            this.grpRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRequirements.Location = new System.Drawing.Point(279, 19);
            this.grpRequirements.Name = "grpRequirements";
            this.grpRequirements.Size = new System.Drawing.Size(505, 377);
            this.grpRequirements.TabIndex = 60;
            this.grpRequirements.TabStop = false;
            this.grpRequirements.Text = "Requirements";
            // 
            // chkRequireTrue
            // 
            this.chkRequireTrue.AutoSize = true;
            this.chkRequireTrue.Location = new System.Drawing.Point(9, 153);
            this.chkRequireTrue.Name = "chkRequireTrue";
            this.chkRequireTrue.Size = new System.Drawing.Size(61, 17);
            this.chkRequireTrue.TabIndex = 61;
            this.chkRequireTrue.Text = "Is true?";
            this.chkRequireTrue.CheckedChanged += new System.EventHandler(this.chkRequireTrue_CheckedChanged);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(390, 345);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveAll.Size = new System.Drawing.Size(103, 23);
            this.btnRemoveAll.TabIndex = 117;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(9, 345);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSelection.Size = new System.Drawing.Size(103, 23);
            this.btnClearSelection.TabIndex = 116;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Hint";
            // 
            // txtRequireHint
            // 
            this.txtRequireHint.AcceptsReturn = true;
            this.txtRequireHint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txtRequireHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRequireHint.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtRequireHint.Location = new System.Drawing.Point(257, 35);
            this.txtRequireHint.Multiline = true;
            this.txtRequireHint.Name = "txtRequireHint";
            this.txtRequireHint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRequireHint.Size = new System.Drawing.Size(236, 137);
            this.txtRequireHint.TabIndex = 61;
            this.txtRequireHint.TextChanged += new System.EventHandler(this.txtRequireHint_TextChanged);
            // 
            // btnRemoveReq
            // 
            this.btnRemoveReq.Location = new System.Drawing.Point(96, 191);
            this.btnRemoveReq.Name = "btnRemoveReq";
            this.btnRemoveReq.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveReq.Size = new System.Drawing.Size(81, 23);
            this.btnRemoveReq.TabIndex = 115;
            this.btnRemoveReq.Text = "Remove";
            this.btnRemoveReq.Click += new System.EventHandler(this.btnRemoveReq_Click);
            // 
            // btnAddReq
            // 
            this.btnAddReq.Location = new System.Drawing.Point(9, 191);
            this.btnAddReq.Name = "btnAddReq";
            this.btnAddReq.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddReq.Size = new System.Drawing.Size(81, 23);
            this.btnAddReq.TabIndex = 34;
            this.btnAddReq.Text = "Add";
            this.btnAddReq.Click += new System.EventHandler(this.btnAddReq_Click);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(6, 136);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 114;
            this.lblValue.Text = "Value";
            // 
            // nudAmt
            // 
            this.nudAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAmt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAmt.Location = new System.Drawing.Point(9, 152);
            this.nudAmt.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudAmt.Name = "nudAmt";
            this.nudAmt.Size = new System.Drawing.Size(229, 20);
            this.nudAmt.TabIndex = 113;
            this.nudAmt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAmt.ValueChanged += new System.EventHandler(this.nudAmt_ValueChanged);
            // 
            // lstRequirements
            // 
            this.lstRequirements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstRequirements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRequirements.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstRequirements.FormattingEnabled = true;
            this.lstRequirements.Location = new System.Drawing.Point(9, 220);
            this.lstRequirements.Name = "lstRequirements";
            this.lstRequirements.Size = new System.Drawing.Size(484, 119);
            this.lstRequirements.TabIndex = 112;
            this.lstRequirements.SelectedIndexChanged += new System.EventHandler(this.lstRequirements_SelectedIndexChanged);
            // 
            // cmbTriggerParams
            // 
            this.cmbTriggerParams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTriggerParams.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTriggerParams.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTriggerParams.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTriggerParams.DrawDropdownHoverOutline = false;
            this.cmbTriggerParams.DrawFocusRectangle = false;
            this.cmbTriggerParams.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTriggerParams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTriggerParams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTriggerParams.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTriggerParams.FormattingEnabled = true;
            this.cmbTriggerParams.Location = new System.Drawing.Point(9, 93);
            this.cmbTriggerParams.Name = "cmbTriggerParams";
            this.cmbTriggerParams.Size = new System.Drawing.Size(229, 21);
            this.cmbTriggerParams.TabIndex = 59;
            this.cmbTriggerParams.Text = null;
            this.cmbTriggerParams.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTriggerParams.SelectedIndexChanged += new System.EventHandler(this.cmbTriggerParams_SelectedIndexChanged);
            // 
            // lblTriggerType
            // 
            this.lblTriggerType.AutoSize = true;
            this.lblTriggerType.Location = new System.Drawing.Point(6, 19);
            this.lblTriggerType.Name = "lblTriggerType";
            this.lblTriggerType.Size = new System.Drawing.Size(67, 13);
            this.lblTriggerType.TabIndex = 57;
            this.lblTriggerType.Text = "Trigger Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Trigger Param";
            // 
            // cmbTriggerType
            // 
            this.cmbTriggerType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTriggerType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTriggerType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTriggerType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTriggerType.DrawDropdownHoverOutline = false;
            this.cmbTriggerType.DrawFocusRectangle = false;
            this.cmbTriggerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTriggerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTriggerType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTriggerType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTriggerType.FormattingEnabled = true;
            this.cmbTriggerType.Location = new System.Drawing.Point(9, 35);
            this.cmbTriggerType.Name = "cmbTriggerType";
            this.cmbTriggerType.Size = new System.Drawing.Size(229, 21);
            this.cmbTriggerType.TabIndex = 57;
            this.cmbTriggerType.Text = null;
            this.cmbTriggerType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTriggerType.SelectedIndexChanged += new System.EventHandler(this.cmbTriggerType_SelectedIndexChanged);
            // 
            // grpLogic
            // 
            this.grpLogic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpLogic.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpLogic.Controls.Add(this.lblMinimumCr);
            this.grpLogic.Controls.Add(this.nudMinCr);
            this.grpLogic.Controls.Add(this.btnDynamicRequirements);
            this.grpLogic.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpLogic.Location = new System.Drawing.Point(9, 387);
            this.grpLogic.Name = "grpLogic";
            this.grpLogic.Size = new System.Drawing.Size(264, 83);
            this.grpLogic.TabIndex = 57;
            this.grpLogic.TabStop = false;
            this.grpLogic.Text = "Unlock Logic";
            // 
            // btnDynamicRequirements
            // 
            this.btnDynamicRequirements.Location = new System.Drawing.Point(20, 21);
            this.btnDynamicRequirements.Name = "btnDynamicRequirements";
            this.btnDynamicRequirements.Padding = new System.Windows.Forms.Padding(5);
            this.btnDynamicRequirements.Size = new System.Drawing.Size(229, 23);
            this.btnDynamicRequirements.TabIndex = 33;
            this.btnDynamicRequirements.Text = "Requirements";
            this.btnDynamicRequirements.Click += new System.EventHandler(this.btnDynamicRequirements_Click);
            // 
            // darkGroupBox1
            // 
            this.darkGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.darkGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.darkGroupBox1.Controls.Add(this.chkHidden);
            this.darkGroupBox1.Controls.Add(this.cmbImage);
            this.darkGroupBox1.Controls.Add(this.lblCraftImage);
            this.darkGroupBox1.Controls.Add(this.picItem);
            this.darkGroupBox1.Controls.Add(this.cmbCraftType);
            this.darkGroupBox1.Controls.Add(this.lblCraftType);
            this.darkGroupBox1.Controls.Add(this.txtHint);
            this.darkGroupBox1.Controls.Add(this.txtLabel);
            this.darkGroupBox1.Controls.Add(this.lblHint);
            this.darkGroupBox1.Controls.Add(this.lblDisplayName);
            this.darkGroupBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.darkGroupBox1.Location = new System.Drawing.Point(9, 74);
            this.darkGroupBox1.Name = "darkGroupBox1";
            this.darkGroupBox1.Size = new System.Drawing.Size(264, 307);
            this.darkGroupBox1.TabIndex = 54;
            this.darkGroupBox1.TabStop = false;
            this.darkGroupBox1.Text = "In-Game Display";
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Location = new System.Drawing.Point(9, 284);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(139, 17);
            this.chkHidden.TabIndex = 60;
            this.chkHidden.Text = "Hidden Until Unlocked?";
            this.chkHidden.CheckedChanged += new System.EventHandler(this.chkHidden_CheckedChanged);
            // 
            // cmbImage
            // 
            this.cmbImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbImage.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbImage.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbImage.DrawDropdownHoverOutline = false;
            this.cmbImage.DrawFocusRectangle = false;
            this.cmbImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbImage.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbImage.FormattingEnabled = true;
            this.cmbImage.Items.AddRange(new object[] {
            "None"});
            this.cmbImage.Location = new System.Drawing.Point(49, 257);
            this.cmbImage.Name = "cmbImage";
            this.cmbImage.Size = new System.Drawing.Size(171, 21);
            this.cmbImage.TabIndex = 59;
            this.cmbImage.Text = "None";
            this.cmbImage.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbImage.SelectedIndexChanged += new System.EventHandler(this.cmbImage_SelectedIndexChanged);
            // 
            // lblCraftImage
            // 
            this.lblCraftImage.AutoSize = true;
            this.lblCraftImage.Location = new System.Drawing.Point(6, 260);
            this.lblCraftImage.Name = "lblCraftImage";
            this.lblCraftImage.Size = new System.Drawing.Size(36, 13);
            this.lblCraftImage.TabIndex = 58;
            this.lblCraftImage.Text = "Image";
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.Black;
            this.picItem.Location = new System.Drawing.Point(226, 251);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(32, 32);
            this.picItem.TabIndex = 57;
            this.picItem.TabStop = false;
            // 
            // cmbCraftType
            // 
            this.cmbCraftType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCraftType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCraftType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCraftType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCraftType.DrawDropdownHoverOutline = false;
            this.cmbCraftType.DrawFocusRectangle = false;
            this.cmbCraftType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCraftType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCraftType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCraftType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCraftType.FormattingEnabled = true;
            this.cmbCraftType.Location = new System.Drawing.Point(84, 224);
            this.cmbCraftType.Name = "cmbCraftType";
            this.cmbCraftType.Size = new System.Drawing.Size(174, 21);
            this.cmbCraftType.TabIndex = 55;
            this.cmbCraftType.Text = null;
            this.cmbCraftType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCraftType.SelectedIndexChanged += new System.EventHandler(this.cmbCraftType_SelectedIndexChanged);
            // 
            // lblCraftType
            // 
            this.lblCraftType.AutoSize = true;
            this.lblCraftType.Location = new System.Drawing.Point(6, 227);
            this.lblCraftType.Name = "lblCraftType";
            this.lblCraftType.Size = new System.Drawing.Size(56, 13);
            this.lblCraftType.TabIndex = 55;
            this.lblCraftType.Text = "Craft Type";
            // 
            // txtHint
            // 
            this.txtHint.AcceptsReturn = true;
            this.txtHint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txtHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHint.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtHint.Location = new System.Drawing.Point(9, 62);
            this.txtHint.Multiline = true;
            this.txtHint.Name = "txtHint";
            this.txtHint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHint.Size = new System.Drawing.Size(249, 150);
            this.txtHint.TabIndex = 56;
            this.txtHint.TextChanged += new System.EventHandler(this.txtHint_TextChanged);
            // 
            // txtLabel
            // 
            this.txtLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtLabel.Location = new System.Drawing.Point(84, 19);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(174, 20);
            this.txtLabel.TabIndex = 55;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(6, 46);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(26, 13);
            this.lblHint.TabIndex = 51;
            this.lblHint.Text = "Hint";
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(6, 21);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(72, 13);
            this.lblDisplayName.TabIndex = 19;
            this.lblDisplayName.Text = "Display Name";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(255, 43);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 52;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 46);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 51;
            this.lblFolder.Text = "Folder";
            // 
            // cmbFolder
            // 
            this.cmbFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFolder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFolder.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFolder.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFolder.DrawDropdownHoverOutline = false;
            this.cmbFolder.DrawFocusRectangle = false;
            this.cmbFolder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFolder.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Location = new System.Drawing.Point(57, 43);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(192, 21);
            this.cmbFolder.TabIndex = 50;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 21);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(57, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(216, 20);
            this.txtName.TabIndex = 18;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // nudMinCr
            // 
            this.nudMinCr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinCr.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinCr.Location = new System.Drawing.Point(128, 50);
            this.nudMinCr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMinCr.Name = "nudMinCr";
            this.nudMinCr.Size = new System.Drawing.Size(121, 20);
            this.nudMinCr.TabIndex = 95;
            this.nudMinCr.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMinCr.ValueChanged += new System.EventHandler(this.nudMinCr_ValueChanged);
            // 
            // lblMinimumCr
            // 
            this.lblMinimumCr.AutoSize = true;
            this.lblMinimumCr.Location = new System.Drawing.Point(17, 52);
            this.lblMinimumCr.Name = "lblMinimumCr";
            this.lblMinimumCr.Size = new System.Drawing.Size(105, 13);
            this.lblMinimumCr.TabIndex = 96;
            this.lblMinimumCr.Text = "Minimum Class Rank";
            // 
            // frmRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1021, 566);
            this.Controls.Add(this.grpProperties);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpRecipes);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecipe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recipe Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpRecipes.ResumeLayout(false);
            this.grpRecipes.PerformLayout();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.grpRequirements.ResumeLayout(false);
            this.grpRequirements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).EndInit();
            this.grpLogic.ResumeLayout(false);
            this.grpLogic.PerformLayout();
            this.darkGroupBox1.ResumeLayout(false);
            this.darkGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinCr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ToolStripButton toolStripItemCopy;
        public System.Windows.Forms.ToolStripButton toolStripItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripItemUndo;
        private DarkUI.Controls.DarkGroupBox grpRecipes;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpProperties;
        private DarkUI.Controls.DarkGroupBox darkGroupBox1;
        private DarkUI.Controls.DarkComboBox cmbCraftType;
        private System.Windows.Forms.Label lblCraftType;
        private DarkUI.Controls.DarkTextBox txtHint;
        private DarkUI.Controls.DarkTextBox txtLabel;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblDisplayName;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private DarkUI.Controls.DarkGroupBox grpLogic;
        private DarkUI.Controls.DarkButton btnDynamicRequirements;
        private System.Windows.Forms.Label lblTriggerType;
        private DarkUI.Controls.DarkComboBox cmbTriggerType;
        private DarkUI.Controls.DarkComboBox cmbTriggerParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCraftImage;
        private System.Windows.Forms.PictureBox picItem;
        private DarkUI.Controls.DarkComboBox cmbImage;
        private DarkUI.Controls.DarkCheckBox chkHidden;
        private DarkUI.Controls.DarkGroupBox grpRequirements;
        private System.Windows.Forms.ListBox lstRequirements;
        private System.Windows.Forms.Label lblValue;
        private DarkUI.Controls.DarkNumericUpDown nudAmt;
        private DarkUI.Controls.DarkButton btnRemoveReq;
        private DarkUI.Controls.DarkButton btnAddReq;
        private System.Windows.Forms.Label label2;
        private DarkUI.Controls.DarkTextBox txtRequireHint;
        private DarkUI.Controls.DarkButton btnClearSelection;
        private DarkUI.Controls.DarkButton btnRemoveAll;
        private DarkUI.Controls.DarkCheckBox chkRequireTrue;
        private System.Windows.Forms.Label lblMinimumCr;
        private DarkUI.Controls.DarkNumericUpDown nudMinCr;
    }
}