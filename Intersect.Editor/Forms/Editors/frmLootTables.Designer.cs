
namespace Intersect.Editor.Forms.Editors
{
    partial class frmLootTables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLootTables));
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
            this.grpTables = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.pnlTableSettings = new System.Windows.Forms.Panel();
            this.grpProperties = new DarkUI.Controls.DarkGroupBox();
            this.grpDrops = new DarkUI.Controls.DarkGroupBox();
            this.btnUnselectItem = new DarkUI.Controls.DarkButton();
            this.grpTypes = new DarkUI.Controls.DarkGroupBox();
            this.rdoTable = new DarkUI.Controls.DarkRadioButton();
            this.rdoItem = new DarkUI.Controls.DarkRadioButton();
            this.btnDynamicRequirements = new DarkUI.Controls.DarkButton();
            this.btnDropRemove = new DarkUI.Controls.DarkButton();
            this.btnDropAdd = new DarkUI.Controls.DarkButton();
            this.lstDrops = new System.Windows.Forms.ListBox();
            this.nudDropAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDropChance = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbDropItem = new DarkUI.Controls.DarkComboBox();
            this.lblDropAmount = new System.Windows.Forms.Label();
            this.lblDropChance = new System.Windows.Forms.Label();
            this.lblDropItem = new System.Windows.Forms.Label();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.txtDisplayName = new DarkUI.Controls.DarkTextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.toolStrip.SuspendLayout();
            this.grpTables.SuspendLayout();
            this.pnlTableSettings.SuspendLayout();
            this.grpProperties.SuspendLayout();
            this.grpDrops.SuspendLayout();
            this.grpTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropChance)).BeginInit();
            this.grpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
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
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripItemNew
            // 
            this.toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.toolStripItemNew, "toolStripItemNew");
            this.toolStripItemNew.Name = "toolStripItemNew";
            this.toolStripItemNew.Click += new System.EventHandler(this.toolStripItemNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripItemDelete
            // 
            this.toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripItemDelete, "toolStripItemDelete");
            this.toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemDelete.Name = "toolStripItemDelete";
            this.toolStripItemDelete.Click += new System.EventHandler(this.toolStripItemDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnAlphabetical
            // 
            this.btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.btnAlphabetical, "btnAlphabetical");
            this.btnAlphabetical.Name = "btnAlphabetical";
            this.btnAlphabetical.Click += new System.EventHandler(this.btnAlphabetical_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripItemCopy
            // 
            this.toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripItemCopy, "toolStripItemCopy");
            this.toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemCopy.Name = "toolStripItemCopy";
            this.toolStripItemCopy.Click += new System.EventHandler(this.toolStripItemCopy_Click);
            // 
            // toolStripItemPaste
            // 
            this.toolStripItemPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripItemPaste, "toolStripItemPaste");
            this.toolStripItemPaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemPaste.Name = "toolStripItemPaste";
            this.toolStripItemPaste.Click += new System.EventHandler(this.toolStripItemPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripItemUndo
            // 
            this.toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripItemUndo, "toolStripItemUndo");
            this.toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemUndo.Name = "toolStripItemUndo";
            this.toolStripItemUndo.Click += new System.EventHandler(this.toolStripItemUndo_Click);
            // 
            // grpTables
            // 
            this.grpTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTables.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTables.Controls.Add(this.btnClearSearch);
            this.grpTables.Controls.Add(this.txtSearch);
            this.grpTables.Controls.Add(this.lstGameObjects);
            this.grpTables.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.grpTables, "grpTables");
            this.grpTables.Name = "grpTables";
            this.grpTables.TabStop = false;
            // 
            // btnClearSearch
            // 
            resources.ApplyResources(this.btnClearSearch, "btnClearSearch");
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lstGameObjects
            // 
            this.lstGameObjects.AllowDrop = true;
            this.lstGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstGameObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstGameObjects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstGameObjects.HideSelection = false;
            resources.ApplyResources(this.lstGameObjects, "lstGameObjects");
            this.lstGameObjects.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lstGameObjects.Name = "lstGameObjects";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlTableSettings
            // 
            this.pnlTableSettings.Controls.Add(this.grpProperties);
            this.pnlTableSettings.Controls.Add(this.grpGeneral);
            resources.ApplyResources(this.pnlTableSettings, "pnlTableSettings");
            this.pnlTableSettings.Name = "pnlTableSettings";
            // 
            // grpProperties
            // 
            this.grpProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpProperties.Controls.Add(this.grpDrops);
            this.grpProperties.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.grpProperties, "grpProperties");
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.TabStop = false;
            // 
            // grpDrops
            // 
            this.grpDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDrops.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDrops.Controls.Add(this.btnUnselectItem);
            this.grpDrops.Controls.Add(this.grpTypes);
            this.grpDrops.Controls.Add(this.btnDynamicRequirements);
            this.grpDrops.Controls.Add(this.btnDropRemove);
            this.grpDrops.Controls.Add(this.btnDropAdd);
            this.grpDrops.Controls.Add(this.lstDrops);
            this.grpDrops.Controls.Add(this.nudDropAmount);
            this.grpDrops.Controls.Add(this.nudDropChance);
            this.grpDrops.Controls.Add(this.cmbDropItem);
            this.grpDrops.Controls.Add(this.lblDropAmount);
            this.grpDrops.Controls.Add(this.lblDropChance);
            this.grpDrops.Controls.Add(this.lblDropItem);
            this.grpDrops.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.grpDrops, "grpDrops");
            this.grpDrops.Name = "grpDrops";
            this.grpDrops.TabStop = false;
            // 
            // btnUnselectItem
            // 
            resources.ApplyResources(this.btnUnselectItem, "btnUnselectItem");
            this.btnUnselectItem.Name = "btnUnselectItem";
            this.btnUnselectItem.Click += new System.EventHandler(this.btnUnselectItem_Click);
            // 
            // grpTypes
            // 
            this.grpTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTypes.Controls.Add(this.rdoTable);
            this.grpTypes.Controls.Add(this.rdoItem);
            this.grpTypes.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.grpTypes, "grpTypes");
            this.grpTypes.Name = "grpTypes";
            this.grpTypes.TabStop = false;
            // 
            // rdoTable
            // 
            resources.ApplyResources(this.rdoTable, "rdoTable");
            this.rdoTable.Name = "rdoTable";
            this.rdoTable.CheckedChanged += new System.EventHandler(this.rdoTable_CheckedChanged);
            // 
            // rdoItem
            // 
            resources.ApplyResources(this.rdoItem, "rdoItem");
            this.rdoItem.Checked = true;
            this.rdoItem.Name = "rdoItem";
            this.rdoItem.TabStop = true;
            this.rdoItem.CheckedChanged += new System.EventHandler(this.rdoItem_CheckedChanged);
            // 
            // btnDynamicRequirements
            // 
            resources.ApplyResources(this.btnDynamicRequirements, "btnDynamicRequirements");
            this.btnDynamicRequirements.Name = "btnDynamicRequirements";
            this.btnDynamicRequirements.Click += new System.EventHandler(this.btnDynamicRequirements_Click);
            // 
            // btnDropRemove
            // 
            resources.ApplyResources(this.btnDropRemove, "btnDropRemove");
            this.btnDropRemove.Name = "btnDropRemove";
            this.btnDropRemove.Click += new System.EventHandler(this.btnDropRemove_Click);
            // 
            // btnDropAdd
            // 
            resources.ApplyResources(this.btnDropAdd, "btnDropAdd");
            this.btnDropAdd.Name = "btnDropAdd";
            this.btnDropAdd.Click += new System.EventHandler(this.btnDropAdd_Click);
            // 
            // lstDrops
            // 
            this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstDrops.FormattingEnabled = true;
            resources.ApplyResources(this.lstDrops, "lstDrops");
            this.lstDrops.Name = "lstDrops";
            this.lstDrops.SelectedIndexChanged += new System.EventHandler(this.lstDrops_SelectedIndexChanged);
            // 
            // nudDropAmount
            // 
            this.nudDropAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDropAmount.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.nudDropAmount, "nudDropAmount");
            this.nudDropAmount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudDropAmount.Name = "nudDropAmount";
            this.nudDropAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDropAmount.ValueChanged += new System.EventHandler(this.nudDropAmount_ValueChanged);
            // 
            // nudDropChance
            // 
            this.nudDropChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDropChance.DecimalPlaces = 2;
            this.nudDropChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDropChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            resources.ApplyResources(this.nudDropChance, "nudDropChance");
            this.nudDropChance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDropChance.Name = "nudDropChance";
            this.nudDropChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDropChance.ValueChanged += new System.EventHandler(this.nudDropChance_ValueChanged);
            // 
            // cmbDropItem
            // 
            this.cmbDropItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDropItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDropItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDropItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDropItem.DrawDropdownHoverOutline = false;
            this.cmbDropItem.DrawFocusRectangle = false;
            this.cmbDropItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDropItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbDropItem, "cmbDropItem");
            this.cmbDropItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDropItem.FormattingEnabled = true;
            this.cmbDropItem.Name = "cmbDropItem";
            this.cmbDropItem.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDropItem.SelectedIndexChanged += new System.EventHandler(this.cmbDropItem_SelectedIndexChanged);
            // 
            // lblDropAmount
            // 
            resources.ApplyResources(this.lblDropAmount, "lblDropAmount");
            this.lblDropAmount.Name = "lblDropAmount";
            // 
            // lblDropChance
            // 
            resources.ApplyResources(this.lblDropChance, "lblDropChance");
            this.lblDropChance.Name = "lblDropChance";
            // 
            // lblDropItem
            // 
            resources.ApplyResources(this.lblDropItem, "lblDropItem");
            this.lblDropItem.Name = "lblDropItem";
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.txtDisplayName);
            this.grpGeneral.Controls.Add(this.lblDisplayName);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.grpGeneral, "grpGeneral");
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.TabStop = false;
            // 
            // btnAddFolder
            // 
            resources.ApplyResources(this.btnAddFolder, "btnAddFolder");
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.txtDisplayName, "txtDisplayName");
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // lblDisplayName
            // 
            resources.ApplyResources(this.lblDisplayName, "lblDisplayName");
            this.lblDisplayName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDisplayName.Name = "lblDisplayName";
            // 
            // lblFolder
            // 
            resources.ApplyResources(this.lblFolder, "lblFolder");
            this.lblFolder.Name = "lblFolder";
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
            resources.ApplyResources(this.cmbFolder, "cmbFolder");
            this.cmbFolder.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // frmLootTables
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.pnlTableSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpTables);
            this.Controls.Add(this.toolStrip);
            this.Name = "frmLootTables";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpTables.ResumeLayout(false);
            this.grpTables.PerformLayout();
            this.pnlTableSettings.ResumeLayout(false);
            this.grpProperties.ResumeLayout(false);
            this.grpDrops.ResumeLayout(false);
            this.grpDrops.PerformLayout();
            this.grpTypes.ResumeLayout(false);
            this.grpTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropChance)).EndInit();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
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
        private DarkUI.Controls.DarkGroupBox grpTables;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private System.Windows.Forms.Panel pnlTableSettings;
        private DarkUI.Controls.DarkGroupBox grpProperties;
        private DarkUI.Controls.DarkGroupBox grpGeneral;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private DarkUI.Controls.DarkTextBox txtDisplayName;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private DarkUI.Controls.DarkGroupBox grpDrops;
        private DarkUI.Controls.DarkButton btnDropRemove;
        private DarkUI.Controls.DarkButton btnDropAdd;
        private System.Windows.Forms.ListBox lstDrops;
        private DarkUI.Controls.DarkNumericUpDown nudDropAmount;
        private DarkUI.Controls.DarkNumericUpDown nudDropChance;
        private DarkUI.Controls.DarkComboBox cmbDropItem;
        private System.Windows.Forms.Label lblDropAmount;
        private System.Windows.Forms.Label lblDropChance;
        private System.Windows.Forms.Label lblDropItem;
        private DarkUI.Controls.DarkButton btnDynamicRequirements;
        private DarkUI.Controls.DarkGroupBox grpTypes;
        private DarkUI.Controls.DarkRadioButton rdoTable;
        private DarkUI.Controls.DarkRadioButton rdoItem;
        private DarkUI.Controls.DarkButton btnUnselectItem;
    }
}