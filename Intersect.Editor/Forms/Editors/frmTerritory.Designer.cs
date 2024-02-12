namespace Intersect.Editor.Forms.Editors
{
    partial class frmTerritory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTerritory));
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
            this.grpTerritories = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpExtraControl = new DarkUI.Controls.DarkGroupBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.cmbIcon = new DarkUI.Controls.DarkComboBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.txtDisplayName = new DarkUI.Controls.DarkTextBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grpScoring = new DarkUI.Controls.DarkGroupBox();
            this.grpPoints = new DarkUI.Controls.DarkGroupBox();
            this.nudAttack = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDefend = new DarkUI.Controls.DarkNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDefend = new System.Windows.Forms.Label();
            this.nudCapture = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCapture = new System.Windows.Forms.Label();
            this.lblTick = new System.Windows.Forms.Label();
            this.nudTick = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCaptureTime = new System.Windows.Forms.Label();
            this.nudCaptureTime = new DarkUI.Controls.DarkNumericUpDown();
            this.grpTerritory = new DarkUI.Controls.DarkGroupBox();
            this.toolStrip.SuspendLayout();
            this.grpTerritories.SuspendLayout();
            this.grpExtraControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.grpScoring.SuspendLayout();
            this.grpPoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTick)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCaptureTime)).BeginInit();
            this.grpTerritory.SuspendLayout();
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
            this.toolStrip.Size = new System.Drawing.Size(543, 25);
            this.toolStrip.TabIndex = 53;
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
            // grpTerritories
            // 
            this.grpTerritories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTerritories.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTerritories.Controls.Add(this.btnClearSearch);
            this.grpTerritories.Controls.Add(this.txtSearch);
            this.grpTerritories.Controls.Add(this.lstGameObjects);
            this.grpTerritories.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTerritories.Location = new System.Drawing.Point(12, 28);
            this.grpTerritories.Name = "grpTerritories";
            this.grpTerritories.Size = new System.Drawing.Size(203, 364);
            this.grpTerritories.TabIndex = 54;
            this.grpTerritories.TabStop = false;
            this.grpTerritories.Text = "Territories";
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
            this.lstGameObjects.Size = new System.Drawing.Size(191, 311);
            this.lstGameObjects.TabIndex = 32;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(247, 398);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(135, 27);
            this.btnSave.TabIndex = 55;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(394, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(139, 27);
            this.btnCancel.TabIndex = 56;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpExtraControl
            // 
            this.grpExtraControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpExtraControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpExtraControl.Controls.Add(this.lblIcon);
            this.grpExtraControl.Controls.Add(this.picItem);
            this.grpExtraControl.Controls.Add(this.cmbIcon);
            this.grpExtraControl.Controls.Add(this.btnAddFolder);
            this.grpExtraControl.Controls.Add(this.lblFolder);
            this.grpExtraControl.Controls.Add(this.cmbFolder);
            this.grpExtraControl.Controls.Add(this.txtDisplayName);
            this.grpExtraControl.Controls.Add(this.lblDisplayName);
            this.grpExtraControl.Controls.Add(this.txtName);
            this.grpExtraControl.Controls.Add(this.lblName);
            this.grpExtraControl.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpExtraControl.Location = new System.Drawing.Point(6, 20);
            this.grpExtraControl.Name = "grpExtraControl";
            this.grpExtraControl.Size = new System.Drawing.Size(299, 173);
            this.grpExtraControl.TabIndex = 126;
            this.grpExtraControl.TabStop = false;
            this.grpExtraControl.Text = "Properties";
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new System.Drawing.Point(81, 123);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(28, 13);
            this.lblIcon.TabIndex = 129;
            this.lblIcon.Text = "Icon";
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.Black;
            this.picItem.Location = new System.Drawing.Point(43, 128);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(32, 32);
            this.picItem.TabIndex = 128;
            this.picItem.TabStop = false;
            // 
            // cmbIcon
            // 
            this.cmbIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbIcon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbIcon.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbIcon.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbIcon.DrawDropdownHoverOutline = false;
            this.cmbIcon.DrawFocusRectangle = false;
            this.cmbIcon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIcon.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbIcon.FormattingEnabled = true;
            this.cmbIcon.Items.AddRange(new object[] {
            "None"});
            this.cmbIcon.Location = new System.Drawing.Point(84, 139);
            this.cmbIcon.Name = "cmbIcon";
            this.cmbIcon.Size = new System.Drawing.Size(177, 21);
            this.cmbIcon.TabIndex = 127;
            this.cmbIcon.Text = "None";
            this.cmbIcon.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbIcon.SelectedIndexChanged += new System.EventHandler(this.cmbIcon_SelectedIndexChanged);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(267, 87);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 126;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 90);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 125;
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
            this.cmbFolder.Location = new System.Drawing.Point(84, 87);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(177, 21);
            this.cmbFolder.TabIndex = 124;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDisplayName.Location = new System.Drawing.Point(84, 52);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(201, 20);
            this.txtDisplayName.TabIndex = 123;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(6, 54);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(72, 13);
            this.lblDisplayName.TabIndex = 122;
            this.lblDisplayName.Text = "Display Name";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(84, 23);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(201, 20);
            this.txtName.TabIndex = 22;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 21;
            this.lblName.Text = "Name";
            // 
            // grpScoring
            // 
            this.grpScoring.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpScoring.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpScoring.Controls.Add(this.grpPoints);
            this.grpScoring.Controls.Add(this.lblCaptureTime);
            this.grpScoring.Controls.Add(this.nudCaptureTime);
            this.grpScoring.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpScoring.Location = new System.Drawing.Point(6, 199);
            this.grpScoring.Name = "grpScoring";
            this.grpScoring.Size = new System.Drawing.Size(299, 156);
            this.grpScoring.TabIndex = 130;
            this.grpScoring.TabStop = false;
            this.grpScoring.Text = "Scoring";
            // 
            // grpPoints
            // 
            this.grpPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPoints.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPoints.Controls.Add(this.nudAttack);
            this.grpPoints.Controls.Add(this.nudDefend);
            this.grpPoints.Controls.Add(this.label1);
            this.grpPoints.Controls.Add(this.lblDefend);
            this.grpPoints.Controls.Add(this.nudCapture);
            this.grpPoints.Controls.Add(this.lblCapture);
            this.grpPoints.Controls.Add(this.lblTick);
            this.grpPoints.Controls.Add(this.nudTick);
            this.grpPoints.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPoints.Location = new System.Drawing.Point(9, 58);
            this.grpPoints.Name = "grpPoints";
            this.grpPoints.Size = new System.Drawing.Size(276, 88);
            this.grpPoints.TabIndex = 131;
            this.grpPoints.TabStop = false;
            this.grpPoints.Text = "Points per...";
            // 
            // nudAttack
            // 
            this.nudAttack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAttack.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAttack.Location = new System.Drawing.Point(188, 53);
            this.nudAttack.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudAttack.Name = "nudAttack";
            this.nudAttack.Size = new System.Drawing.Size(70, 20);
            this.nudAttack.TabIndex = 136;
            this.nudAttack.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAttack.ValueChanged += new System.EventHandler(this.nudAttack_ValueChanged);
            // 
            // nudDefend
            // 
            this.nudDefend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDefend.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDefend.Location = new System.Drawing.Point(188, 23);
            this.nudDefend.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudDefend.Name = "nudDefend";
            this.nudDefend.Size = new System.Drawing.Size(70, 20);
            this.nudDefend.TabIndex = 135;
            this.nudDefend.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDefend.ValueChanged += new System.EventHandler(this.nudDefend_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 134;
            this.label1.Text = "Attack";
            // 
            // lblDefend
            // 
            this.lblDefend.AutoSize = true;
            this.lblDefend.Location = new System.Drawing.Point(140, 25);
            this.lblDefend.Name = "lblDefend";
            this.lblDefend.Size = new System.Drawing.Size(42, 13);
            this.lblDefend.TabIndex = 133;
            this.lblDefend.Text = "Defend";
            // 
            // nudCapture
            // 
            this.nudCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCapture.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCapture.Location = new System.Drawing.Point(64, 53);
            this.nudCapture.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudCapture.Name = "nudCapture";
            this.nudCapture.Size = new System.Drawing.Size(70, 20);
            this.nudCapture.TabIndex = 132;
            this.nudCapture.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCapture.ValueChanged += new System.EventHandler(this.nudCapture_ValueChanged);
            // 
            // lblCapture
            // 
            this.lblCapture.AutoSize = true;
            this.lblCapture.Location = new System.Drawing.Point(17, 55);
            this.lblCapture.Name = "lblCapture";
            this.lblCapture.Size = new System.Drawing.Size(44, 13);
            this.lblCapture.TabIndex = 131;
            this.lblCapture.Text = "Capture";
            // 
            // lblTick
            // 
            this.lblTick.AutoSize = true;
            this.lblTick.Location = new System.Drawing.Point(17, 25);
            this.lblTick.Name = "lblTick";
            this.lblTick.Size = new System.Drawing.Size(28, 13);
            this.lblTick.TabIndex = 130;
            this.lblTick.Text = "Tick";
            this.lblTick.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nudTick
            // 
            this.nudTick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTick.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTick.Location = new System.Drawing.Point(64, 23);
            this.nudTick.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudTick.Name = "nudTick";
            this.nudTick.Size = new System.Drawing.Size(70, 20);
            this.nudTick.TabIndex = 40;
            this.nudTick.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTick.ValueChanged += new System.EventHandler(this.nudTick_ValueChanged);
            // 
            // lblCaptureTime
            // 
            this.lblCaptureTime.AutoSize = true;
            this.lblCaptureTime.Location = new System.Drawing.Point(17, 25);
            this.lblCaptureTime.Name = "lblCaptureTime";
            this.lblCaptureTime.Size = new System.Drawing.Size(92, 13);
            this.lblCaptureTime.TabIndex = 130;
            this.lblCaptureTime.Text = "Capture Time (ms)";
            // 
            // nudCaptureTime
            // 
            this.nudCaptureTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCaptureTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCaptureTime.Location = new System.Drawing.Point(115, 23);
            this.nudCaptureTime.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudCaptureTime.Name = "nudCaptureTime";
            this.nudCaptureTime.Size = new System.Drawing.Size(170, 20);
            this.nudCaptureTime.TabIndex = 40;
            this.nudCaptureTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCaptureTime.ValueChanged += new System.EventHandler(this.nudCaptureTime_ValueChanged);
            // 
            // grpTerritory
            // 
            this.grpTerritory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTerritory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTerritory.Controls.Add(this.grpExtraControl);
            this.grpTerritory.Controls.Add(this.grpScoring);
            this.grpTerritory.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTerritory.Location = new System.Drawing.Point(221, 28);
            this.grpTerritory.Name = "grpTerritory";
            this.grpTerritory.Size = new System.Drawing.Size(312, 364);
            this.grpTerritory.TabIndex = 132;
            this.grpTerritory.TabStop = false;
            this.grpTerritory.Text = "Territory";
            // 
            // frmTerritory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(543, 437);
            this.Controls.Add(this.grpTerritory);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpTerritories);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTerritory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Territory Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpTerritories.ResumeLayout(false);
            this.grpTerritories.PerformLayout();
            this.grpExtraControl.ResumeLayout(false);
            this.grpExtraControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.grpScoring.ResumeLayout(false);
            this.grpScoring.PerformLayout();
            this.grpPoints.ResumeLayout(false);
            this.grpPoints.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTick)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCaptureTime)).EndInit();
            this.grpTerritory.ResumeLayout(false);
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
        private DarkUI.Controls.DarkGroupBox grpTerritories;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpExtraControl;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private System.Windows.Forms.Label lblDisplayName;
        private DarkUI.Controls.DarkTextBox txtDisplayName;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private DarkUI.Controls.DarkComboBox cmbIcon;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.PictureBox picItem;
        private DarkUI.Controls.DarkGroupBox grpScoring;
        private System.Windows.Forms.Label lblCaptureTime;
        private DarkUI.Controls.DarkNumericUpDown nudCaptureTime;
        private DarkUI.Controls.DarkGroupBox grpPoints;
        private DarkUI.Controls.DarkNumericUpDown nudCapture;
        private System.Windows.Forms.Label lblCapture;
        private System.Windows.Forms.Label lblTick;
        private DarkUI.Controls.DarkNumericUpDown nudTick;
        private System.Windows.Forms.Label lblDefend;
        private System.Windows.Forms.Label label1;
        private DarkUI.Controls.DarkNumericUpDown nudDefend;
        private DarkUI.Controls.DarkNumericUpDown nudAttack;
        private DarkUI.Controls.DarkGroupBox grpTerritory;
    }
}