
namespace Intersect.Editor.Forms.Editors
{
    partial class frmDungeon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDungeon));
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
            this.grpDungeons = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpEditor = new DarkUI.Controls.DarkGroupBox();
            this.grpExtraControl = new DarkUI.Controls.DarkGroupBox();
            this.chkStoreLongest = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreCompletion = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreStart = new DarkUI.Controls.DarkCheckBox();
            this.grpRewards = new DarkUI.Controls.DarkGroupBox();
            this.grpGnome = new DarkUI.Controls.DarkGroupBox();
            this.nudGnomeRolls = new DarkUI.Controls.DarkNumericUpDown();
            this.lblRolls = new System.Windows.Forms.Label();
            this.btnRemoveGnomeLoot = new DarkUI.Controls.DarkButton();
            this.btnAddGnomeLoot = new DarkUI.Controls.DarkButton();
            this.cmbGnomeLootTable = new DarkUI.Controls.DarkComboBox();
            this.lblGnomeLootTable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstGnomeLoot = new System.Windows.Forms.ListBox();
            this.nudGnomeLocations = new DarkUI.Controls.DarkNumericUpDown();
            this.lblGnomeLocations = new System.Windows.Forms.Label();
            this.grpTreasure = new DarkUI.Controls.DarkGroupBox();
            this.lblExp = new System.Windows.Forms.Label();
            this.nudExp = new DarkUI.Controls.DarkNumericUpDown();
            this.nudTreasureRolls = new DarkUI.Controls.DarkNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLoot = new System.Windows.Forms.Label();
            this.btnRemoveTreasureLevel = new DarkUI.Controls.DarkButton();
            this.btnAddTreasureLevel = new DarkUI.Controls.DarkButton();
            this.btnRemoveTable = new DarkUI.Controls.DarkButton();
            this.btnAddTable = new DarkUI.Controls.DarkButton();
            this.cmbLootTable = new DarkUI.Controls.DarkComboBox();
            this.lblLootTable = new System.Windows.Forms.Label();
            this.lstTreasures = new System.Windows.Forms.ListBox();
            this.lstTreasureLevels = new System.Windows.Forms.ListBox();
            this.nudTreasureLevels = new DarkUI.Controls.DarkNumericUpDown();
            this.lblRewardLevel = new System.Windows.Forms.Label();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtDisplayName = new DarkUI.Controls.DarkTextBox();
            this.grpTime = new DarkUI.Controls.DarkGroupBox();
            this.grpTimeReqs = new DarkUI.Controls.DarkGroupBox();
            this.btnSortPlayerCounts = new DarkUI.Controls.DarkButton();
            this.grpTimes = new DarkUI.Controls.DarkGroupBox();
            this.btnSortReqs = new DarkUI.Controls.DarkButton();
            this.btnClearSelection = new DarkUI.Controls.DarkButton();
            this.removeTime = new DarkUI.Controls.DarkButton();
            this.addTime = new DarkUI.Controls.DarkButton();
            this.nudSeconds = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMinutes = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHours = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblHrs = new System.Windows.Forms.Label();
            this.lstRequirement = new System.Windows.Forms.ListBox();
            this.btnRemoveRequirement = new DarkUI.Controls.DarkButton();
            this.btnAddRequirement = new DarkUI.Controls.DarkButton();
            this.lblPlayerNumber = new System.Windows.Forms.Label();
            this.lstPlayerCounts = new System.Windows.Forms.ListBox();
            this.nudTimerPlayers = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbTimer = new DarkUI.Controls.DarkComboBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grpContentScaling = new DarkUI.Controls.DarkGroupBox();
            this.chkStatCeiling = new DarkUI.Controls.DarkCheckBox();
            this.lblTierCeiling = new System.Windows.Forms.Label();
            this.cmbStatCeiling = new DarkUI.Controls.DarkComboBox();
            this.toolStrip.SuspendLayout();
            this.grpDungeons.SuspendLayout();
            this.grpEditor.SuspendLayout();
            this.grpExtraControl.SuspendLayout();
            this.grpRewards.SuspendLayout();
            this.grpGnome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGnomeRolls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGnomeLocations)).BeginInit();
            this.grpTreasure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTreasureRolls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTreasureLevels)).BeginInit();
            this.grpTime.SuspendLayout();
            this.grpTimeReqs.SuspendLayout();
            this.grpTimes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimerPlayers)).BeginInit();
            this.grpContentScaling.SuspendLayout();
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
            this.toolStrip.Size = new System.Drawing.Size(936, 25);
            this.toolStrip.TabIndex = 52;
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
            // grpDungeons
            // 
            this.grpDungeons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDungeons.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDungeons.Controls.Add(this.btnClearSearch);
            this.grpDungeons.Controls.Add(this.txtSearch);
            this.grpDungeons.Controls.Add(this.lstGameObjects);
            this.grpDungeons.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDungeons.Location = new System.Drawing.Point(12, 28);
            this.grpDungeons.Name = "grpDungeons";
            this.grpDungeons.Size = new System.Drawing.Size(203, 609);
            this.grpDungeons.TabIndex = 53;
            this.grpDungeons.TabStop = false;
            this.grpDungeons.Text = "Dungeons";
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
            this.lstGameObjects.Size = new System.Drawing.Size(191, 559);
            this.lstGameObjects.TabIndex = 32;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(635, 683);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(135, 27);
            this.btnSave.TabIndex = 53;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(785, 683);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(139, 27);
            this.btnCancel.TabIndex = 54;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpEditor
            // 
            this.grpEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEditor.Controls.Add(this.grpExtraControl);
            this.grpEditor.Controls.Add(this.grpRewards);
            this.grpEditor.Controls.Add(this.lblDisplayName);
            this.grpEditor.Controls.Add(this.txtDisplayName);
            this.grpEditor.Controls.Add(this.grpTime);
            this.grpEditor.Controls.Add(this.btnAddFolder);
            this.grpEditor.Controls.Add(this.cmbFolder);
            this.grpEditor.Controls.Add(this.lblFolder);
            this.grpEditor.Controls.Add(this.txtName);
            this.grpEditor.Controls.Add(this.lblName);
            this.grpEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEditor.Location = new System.Drawing.Point(221, 28);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(703, 649);
            this.grpEditor.TabIndex = 55;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Dungeon";
            // 
            // grpExtraControl
            // 
            this.grpExtraControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpExtraControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpExtraControl.Controls.Add(this.grpContentScaling);
            this.grpExtraControl.Controls.Add(this.chkStoreLongest);
            this.grpExtraControl.Controls.Add(this.chkIgnoreCompletion);
            this.grpExtraControl.Controls.Add(this.chkIgnoreStart);
            this.grpExtraControl.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpExtraControl.Location = new System.Drawing.Point(19, 476);
            this.grpExtraControl.Name = "grpExtraControl";
            this.grpExtraControl.Size = new System.Drawing.Size(383, 102);
            this.grpExtraControl.TabIndex = 125;
            this.grpExtraControl.TabStop = false;
            this.grpExtraControl.Text = "Additional Properties";
            // 
            // chkStoreLongest
            // 
            this.chkStoreLongest.AutoSize = true;
            this.chkStoreLongest.Location = new System.Drawing.Point(15, 65);
            this.chkStoreLongest.Name = "chkStoreLongest";
            this.chkStoreLongest.Size = new System.Drawing.Size(124, 17);
            this.chkStoreLongest.TabIndex = 56;
            this.chkStoreLongest.Text = "Store Longest Time?";
            this.chkStoreLongest.CheckedChanged += new System.EventHandler(this.chkStoreLongest_CheckedChanged);
            // 
            // chkIgnoreCompletion
            // 
            this.chkIgnoreCompletion.AutoSize = true;
            this.chkIgnoreCompletion.Location = new System.Drawing.Point(15, 42);
            this.chkIgnoreCompletion.Name = "chkIgnoreCompletion";
            this.chkIgnoreCompletion.Size = new System.Drawing.Size(153, 17);
            this.chkIgnoreCompletion.TabIndex = 55;
            this.chkIgnoreCompletion.Text = "Ignore Completion Events?";
            this.chkIgnoreCompletion.CheckedChanged += new System.EventHandler(this.darkCheckBox1_CheckedChanged);
            // 
            // chkIgnoreStart
            // 
            this.chkIgnoreStart.AutoSize = true;
            this.chkIgnoreStart.Location = new System.Drawing.Point(15, 19);
            this.chkIgnoreStart.Name = "chkIgnoreStart";
            this.chkIgnoreStart.Size = new System.Drawing.Size(123, 17);
            this.chkIgnoreStart.TabIndex = 54;
            this.chkIgnoreStart.Text = "Ignore Start Events?";
            this.chkIgnoreStart.CheckedChanged += new System.EventHandler(this.chkIgnoreStart_CheckedChanged);
            // 
            // grpRewards
            // 
            this.grpRewards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRewards.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRewards.Controls.Add(this.grpGnome);
            this.grpRewards.Controls.Add(this.grpTreasure);
            this.grpRewards.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRewards.Location = new System.Drawing.Point(408, 70);
            this.grpRewards.Name = "grpRewards";
            this.grpRewards.Size = new System.Drawing.Size(289, 573);
            this.grpRewards.TabIndex = 122;
            this.grpRewards.TabStop = false;
            this.grpRewards.Text = "Rewards";
            // 
            // grpGnome
            // 
            this.grpGnome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGnome.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGnome.Controls.Add(this.nudGnomeRolls);
            this.grpGnome.Controls.Add(this.lblRolls);
            this.grpGnome.Controls.Add(this.btnRemoveGnomeLoot);
            this.grpGnome.Controls.Add(this.btnAddGnomeLoot);
            this.grpGnome.Controls.Add(this.cmbGnomeLootTable);
            this.grpGnome.Controls.Add(this.lblGnomeLootTable);
            this.grpGnome.Controls.Add(this.label1);
            this.grpGnome.Controls.Add(this.lstGnomeLoot);
            this.grpGnome.Controls.Add(this.nudGnomeLocations);
            this.grpGnome.Controls.Add(this.lblGnomeLocations);
            this.grpGnome.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGnome.Location = new System.Drawing.Point(6, 341);
            this.grpGnome.Name = "grpGnome";
            this.grpGnome.Size = new System.Drawing.Size(277, 226);
            this.grpGnome.TabIndex = 124;
            this.grpGnome.TabStop = false;
            this.grpGnome.Text = "Gnome";
            // 
            // nudGnomeRolls
            // 
            this.nudGnomeRolls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudGnomeRolls.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudGnomeRolls.Location = new System.Drawing.Point(51, 185);
            this.nudGnomeRolls.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGnomeRolls.Name = "nudGnomeRolls";
            this.nudGnomeRolls.Size = new System.Drawing.Size(95, 20);
            this.nudGnomeRolls.TabIndex = 140;
            this.nudGnomeRolls.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblRolls
            // 
            this.lblRolls.AutoSize = true;
            this.lblRolls.Location = new System.Drawing.Point(15, 187);
            this.lblRolls.Name = "lblRolls";
            this.lblRolls.Size = new System.Drawing.Size(30, 13);
            this.lblRolls.TabIndex = 139;
            this.lblRolls.Text = "Rolls";
            // 
            // btnRemoveGnomeLoot
            // 
            this.btnRemoveGnomeLoot.Location = new System.Drawing.Point(213, 182);
            this.btnRemoveGnomeLoot.Name = "btnRemoveGnomeLoot";
            this.btnRemoveGnomeLoot.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveGnomeLoot.Size = new System.Drawing.Size(58, 23);
            this.btnRemoveGnomeLoot.TabIndex = 138;
            this.btnRemoveGnomeLoot.Text = "Remove";
            this.btnRemoveGnomeLoot.Click += new System.EventHandler(this.btnRemoveGnomeLoot_Click);
            // 
            // btnAddGnomeLoot
            // 
            this.btnAddGnomeLoot.Location = new System.Drawing.Point(152, 182);
            this.btnAddGnomeLoot.Name = "btnAddGnomeLoot";
            this.btnAddGnomeLoot.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddGnomeLoot.Size = new System.Drawing.Size(55, 23);
            this.btnAddGnomeLoot.TabIndex = 137;
            this.btnAddGnomeLoot.Text = "Add";
            this.btnAddGnomeLoot.Click += new System.EventHandler(this.btnAddGnomeLoot_Click);
            // 
            // cmbGnomeLootTable
            // 
            this.cmbGnomeLootTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbGnomeLootTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbGnomeLootTable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbGnomeLootTable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbGnomeLootTable.DrawDropdownHoverOutline = false;
            this.cmbGnomeLootTable.DrawFocusRectangle = false;
            this.cmbGnomeLootTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGnomeLootTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGnomeLootTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGnomeLootTable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbGnomeLootTable.FormattingEnabled = true;
            this.cmbGnomeLootTable.Location = new System.Drawing.Point(74, 151);
            this.cmbGnomeLootTable.Name = "cmbGnomeLootTable";
            this.cmbGnomeLootTable.Size = new System.Drawing.Size(197, 21);
            this.cmbGnomeLootTable.TabIndex = 136;
            this.cmbGnomeLootTable.Text = null;
            this.cmbGnomeLootTable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblGnomeLootTable
            // 
            this.lblGnomeLootTable.AutoSize = true;
            this.lblGnomeLootTable.Location = new System.Drawing.Point(15, 154);
            this.lblGnomeLootTable.Name = "lblGnomeLootTable";
            this.lblGnomeLootTable.Size = new System.Drawing.Size(58, 13);
            this.lblGnomeLootTable.TabIndex = 135;
            this.lblGnomeLootTable.Text = "Loot Table";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 134;
            this.label1.Text = "Loot";
            // 
            // lstGnomeLoot
            // 
            this.lstGnomeLoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstGnomeLoot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstGnomeLoot.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstGnomeLoot.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstGnomeLoot.FormattingEnabled = true;
            this.lstGnomeLoot.Location = new System.Drawing.Point(9, 61);
            this.lstGnomeLoot.Name = "lstGnomeLoot";
            this.lstGnomeLoot.Size = new System.Drawing.Size(262, 80);
            this.lstGnomeLoot.TabIndex = 133;
            // 
            // nudGnomeLocations
            // 
            this.nudGnomeLocations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudGnomeLocations.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudGnomeLocations.Location = new System.Drawing.Point(102, 23);
            this.nudGnomeLocations.Name = "nudGnomeLocations";
            this.nudGnomeLocations.Size = new System.Drawing.Size(169, 20);
            this.nudGnomeLocations.TabIndex = 122;
            this.nudGnomeLocations.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudGnomeLocations.ValueChanged += new System.EventHandler(this.nudGnomeLocations_ValueChanged);
            // 
            // lblGnomeLocations
            // 
            this.lblGnomeLocations.AutoSize = true;
            this.lblGnomeLocations.Location = new System.Drawing.Point(6, 25);
            this.lblGnomeLocations.Name = "lblGnomeLocations";
            this.lblGnomeLocations.Size = new System.Drawing.Size(90, 13);
            this.lblGnomeLocations.TabIndex = 116;
            this.lblGnomeLocations.Text = "Gnome Locations";
            // 
            // grpTreasure
            // 
            this.grpTreasure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTreasure.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTreasure.Controls.Add(this.lblExp);
            this.grpTreasure.Controls.Add(this.nudExp);
            this.grpTreasure.Controls.Add(this.nudTreasureRolls);
            this.grpTreasure.Controls.Add(this.label2);
            this.grpTreasure.Controls.Add(this.lblLoot);
            this.grpTreasure.Controls.Add(this.btnRemoveTreasureLevel);
            this.grpTreasure.Controls.Add(this.btnAddTreasureLevel);
            this.grpTreasure.Controls.Add(this.btnRemoveTable);
            this.grpTreasure.Controls.Add(this.btnAddTable);
            this.grpTreasure.Controls.Add(this.cmbLootTable);
            this.grpTreasure.Controls.Add(this.lblLootTable);
            this.grpTreasure.Controls.Add(this.lstTreasures);
            this.grpTreasure.Controls.Add(this.lstTreasureLevels);
            this.grpTreasure.Controls.Add(this.nudTreasureLevels);
            this.grpTreasure.Controls.Add(this.lblRewardLevel);
            this.grpTreasure.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTreasure.Location = new System.Drawing.Point(6, 19);
            this.grpTreasure.Name = "grpTreasure";
            this.grpTreasure.Size = new System.Drawing.Size(277, 316);
            this.grpTreasure.TabIndex = 123;
            this.grpTreasure.TabStop = false;
            this.grpTreasure.Text = "Treasure";
            // 
            // lblExp
            // 
            this.lblExp.AutoSize = true;
            this.lblExp.Location = new System.Drawing.Point(15, 292);
            this.lblExp.Name = "lblExp";
            this.lblExp.Size = new System.Drawing.Size(60, 13);
            this.lblExp.TabIndex = 141;
            this.lblExp.Text = "Experience";
            // 
            // nudExp
            // 
            this.nudExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudExp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudExp.Location = new System.Drawing.Point(87, 290);
            this.nudExp.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudExp.Name = "nudExp";
            this.nudExp.Size = new System.Drawing.Size(184, 20);
            this.nudExp.TabIndex = 142;
            this.nudExp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudExp.ValueChanged += new System.EventHandler(this.nudExp_ValueChanged);
            // 
            // nudTreasureRolls
            // 
            this.nudTreasureRolls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTreasureRolls.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTreasureRolls.Location = new System.Drawing.Point(51, 248);
            this.nudTreasureRolls.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTreasureRolls.Name = "nudTreasureRolls";
            this.nudTreasureRolls.Size = new System.Drawing.Size(95, 20);
            this.nudTreasureRolls.TabIndex = 141;
            this.nudTreasureRolls.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 140;
            this.label2.Text = "Rolls";
            // 
            // lblLoot
            // 
            this.lblLoot.AutoSize = true;
            this.lblLoot.Location = new System.Drawing.Point(23, 117);
            this.lblLoot.Name = "lblLoot";
            this.lblLoot.Size = new System.Drawing.Size(28, 13);
            this.lblLoot.TabIndex = 132;
            this.lblLoot.Text = "Loot";
            // 
            // btnRemoveTreasureLevel
            // 
            this.btnRemoveTreasureLevel.Location = new System.Drawing.Point(213, 104);
            this.btnRemoveTreasureLevel.Name = "btnRemoveTreasureLevel";
            this.btnRemoveTreasureLevel.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveTreasureLevel.Size = new System.Drawing.Size(58, 23);
            this.btnRemoveTreasureLevel.TabIndex = 131;
            this.btnRemoveTreasureLevel.Text = "Remove";
            this.btnRemoveTreasureLevel.Click += new System.EventHandler(this.btnRemoveTreasureLevel_Click);
            // 
            // btnAddTreasureLevel
            // 
            this.btnAddTreasureLevel.Location = new System.Drawing.Point(152, 104);
            this.btnAddTreasureLevel.Name = "btnAddTreasureLevel";
            this.btnAddTreasureLevel.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddTreasureLevel.Size = new System.Drawing.Size(55, 23);
            this.btnAddTreasureLevel.TabIndex = 130;
            this.btnAddTreasureLevel.Text = "Add";
            this.btnAddTreasureLevel.Click += new System.EventHandler(this.btnAddTreasureLevel_Click);
            // 
            // btnRemoveTable
            // 
            this.btnRemoveTable.Location = new System.Drawing.Point(213, 246);
            this.btnRemoveTable.Name = "btnRemoveTable";
            this.btnRemoveTable.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveTable.Size = new System.Drawing.Size(58, 23);
            this.btnRemoveTable.TabIndex = 129;
            this.btnRemoveTable.Text = "Remove";
            this.btnRemoveTable.Click += new System.EventHandler(this.btnRemoveTable_Click);
            // 
            // btnAddTable
            // 
            this.btnAddTable.Location = new System.Drawing.Point(152, 246);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddTable.Size = new System.Drawing.Size(55, 23);
            this.btnAddTable.TabIndex = 128;
            this.btnAddTable.Text = "Add";
            this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
            // 
            // cmbLootTable
            // 
            this.cmbLootTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbLootTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbLootTable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbLootTable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbLootTable.DrawDropdownHoverOutline = false;
            this.cmbLootTable.DrawFocusRectangle = false;
            this.cmbLootTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLootTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLootTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLootTable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbLootTable.FormattingEnabled = true;
            this.cmbLootTable.Location = new System.Drawing.Point(74, 221);
            this.cmbLootTable.Name = "cmbLootTable";
            this.cmbLootTable.Size = new System.Drawing.Size(197, 21);
            this.cmbLootTable.TabIndex = 127;
            this.cmbLootTable.Text = null;
            this.cmbLootTable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblLootTable
            // 
            this.lblLootTable.AutoSize = true;
            this.lblLootTable.Location = new System.Drawing.Point(15, 224);
            this.lblLootTable.Name = "lblLootTable";
            this.lblLootTable.Size = new System.Drawing.Size(58, 13);
            this.lblLootTable.TabIndex = 126;
            this.lblLootTable.Text = "Loot Table";
            // 
            // lstTreasures
            // 
            this.lstTreasures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstTreasures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTreasures.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstTreasures.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstTreasures.FormattingEnabled = true;
            this.lstTreasures.Location = new System.Drawing.Point(18, 133);
            this.lstTreasures.Name = "lstTreasures";
            this.lstTreasures.Size = new System.Drawing.Size(253, 80);
            this.lstTreasures.TabIndex = 125;
            // 
            // lstTreasureLevels
            // 
            this.lstTreasureLevels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstTreasureLevels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTreasureLevels.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstTreasureLevels.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstTreasureLevels.FormattingEnabled = true;
            this.lstTreasureLevels.Location = new System.Drawing.Point(18, 18);
            this.lstTreasureLevels.Name = "lstTreasureLevels";
            this.lstTreasureLevels.Size = new System.Drawing.Size(253, 54);
            this.lstTreasureLevels.TabIndex = 118;
            this.lstTreasureLevels.SelectedIndexChanged += new System.EventHandler(this.lstTreasureLevels_SelectedIndexChanged);
            // 
            // nudTreasureLevels
            // 
            this.nudTreasureLevels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTreasureLevels.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTreasureLevels.Location = new System.Drawing.Point(87, 78);
            this.nudTreasureLevels.Name = "nudTreasureLevels";
            this.nudTreasureLevels.Size = new System.Drawing.Size(184, 20);
            this.nudTreasureLevels.TabIndex = 117;
            this.nudTreasureLevels.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblRewardLevel
            // 
            this.lblRewardLevel.AutoSize = true;
            this.lblRewardLevel.Location = new System.Drawing.Point(15, 80);
            this.lblRewardLevel.Name = "lblRewardLevel";
            this.lblRewardLevel.Size = new System.Drawing.Size(66, 13);
            this.lblRewardLevel.TabIndex = 116;
            this.lblRewardLevel.Text = "Treasure Lvl";
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(16, 46);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(72, 13);
            this.lblDisplayName.TabIndex = 121;
            this.lblDisplayName.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDisplayName.Location = new System.Drawing.Point(94, 44);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(179, 20);
            this.txtDisplayName.TabIndex = 120;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.darkTextBox1_TextChanged);
            // 
            // grpTime
            // 
            this.grpTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTime.Controls.Add(this.grpTimeReqs);
            this.grpTime.Controls.Add(this.cmbTimer);
            this.grpTime.Controls.Add(this.lblTimer);
            this.grpTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTime.Location = new System.Drawing.Point(19, 70);
            this.grpTime.Name = "grpTime";
            this.grpTime.Size = new System.Drawing.Size(383, 400);
            this.grpTime.TabIndex = 113;
            this.grpTime.TabStop = false;
            this.grpTime.Text = "Timing";
            // 
            // grpTimeReqs
            // 
            this.grpTimeReqs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTimeReqs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTimeReqs.Controls.Add(this.btnSortPlayerCounts);
            this.grpTimeReqs.Controls.Add(this.grpTimes);
            this.grpTimeReqs.Controls.Add(this.btnRemoveRequirement);
            this.grpTimeReqs.Controls.Add(this.btnAddRequirement);
            this.grpTimeReqs.Controls.Add(this.lblPlayerNumber);
            this.grpTimeReqs.Controls.Add(this.lstPlayerCounts);
            this.grpTimeReqs.Controls.Add(this.nudTimerPlayers);
            this.grpTimeReqs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTimeReqs.Location = new System.Drawing.Point(9, 50);
            this.grpTimeReqs.Name = "grpTimeReqs";
            this.grpTimeReqs.Size = new System.Drawing.Size(364, 339);
            this.grpTimeReqs.TabIndex = 116;
            this.grpTimeReqs.TabStop = false;
            this.grpTimeReqs.Text = "Requirements";
            // 
            // btnSortPlayerCounts
            // 
            this.btnSortPlayerCounts.Location = new System.Drawing.Point(187, 62);
            this.btnSortPlayerCounts.Name = "btnSortPlayerCounts";
            this.btnSortPlayerCounts.Padding = new System.Windows.Forms.Padding(5);
            this.btnSortPlayerCounts.Size = new System.Drawing.Size(58, 23);
            this.btnSortPlayerCounts.TabIndex = 119;
            this.btnSortPlayerCounts.Text = "Sort";
            this.btnSortPlayerCounts.Click += new System.EventHandler(this.btnSortPlayerCounts_Click);
            // 
            // grpTimes
            // 
            this.grpTimes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTimes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTimes.Controls.Add(this.btnSortReqs);
            this.grpTimes.Controls.Add(this.btnClearSelection);
            this.grpTimes.Controls.Add(this.removeTime);
            this.grpTimes.Controls.Add(this.addTime);
            this.grpTimes.Controls.Add(this.nudSeconds);
            this.grpTimes.Controls.Add(this.nudMinutes);
            this.grpTimes.Controls.Add(this.nudHours);
            this.grpTimes.Controls.Add(this.lblSeconds);
            this.grpTimes.Controls.Add(this.lblMinutes);
            this.grpTimes.Controls.Add(this.lblHrs);
            this.grpTimes.Controls.Add(this.lstRequirement);
            this.grpTimes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTimes.Location = new System.Drawing.Point(9, 120);
            this.grpTimes.Name = "grpTimes";
            this.grpTimes.Size = new System.Drawing.Size(331, 205);
            this.grpTimes.TabIndex = 118;
            this.grpTimes.TabStop = false;
            this.grpTimes.Text = "Requirements";
            // 
            // btnSortReqs
            // 
            this.btnSortReqs.Location = new System.Drawing.Point(224, 73);
            this.btnSortReqs.Name = "btnSortReqs";
            this.btnSortReqs.Padding = new System.Windows.Forms.Padding(5);
            this.btnSortReqs.Size = new System.Drawing.Size(101, 23);
            this.btnSortReqs.TabIndex = 125;
            this.btnSortReqs.Text = "Sort";
            this.btnSortReqs.Click += new System.EventHandler(this.btnSortReqs_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(224, 102);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSelection.Size = new System.Drawing.Size(101, 23);
            this.btnClearSelection.TabIndex = 124;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // removeTime
            // 
            this.removeTime.Location = new System.Drawing.Point(267, 173);
            this.removeTime.Name = "removeTime";
            this.removeTime.Padding = new System.Windows.Forms.Padding(5);
            this.removeTime.Size = new System.Drawing.Size(58, 23);
            this.removeTime.TabIndex = 123;
            this.removeTime.Text = "Remove";
            this.removeTime.Click += new System.EventHandler(this.removeTime_Click);
            // 
            // addTime
            // 
            this.addTime.Location = new System.Drawing.Point(7, 173);
            this.addTime.Name = "addTime";
            this.addTime.Padding = new System.Windows.Forms.Padding(5);
            this.addTime.Size = new System.Drawing.Size(86, 23);
            this.addTime.TabIndex = 122;
            this.addTime.Text = "Add";
            this.addTime.Click += new System.EventHandler(this.addTime_Click);
            // 
            // nudSeconds
            // 
            this.nudSeconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSeconds.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSeconds.Location = new System.Drawing.Point(223, 147);
            this.nudSeconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudSeconds.Name = "nudSeconds";
            this.nudSeconds.Size = new System.Drawing.Size(102, 20);
            this.nudSeconds.TabIndex = 121;
            this.nudSeconds.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudMinutes
            // 
            this.nudMinutes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinutes.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinutes.Location = new System.Drawing.Point(115, 147);
            this.nudMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            this.nudMinutes.Size = new System.Drawing.Size(102, 20);
            this.nudMinutes.TabIndex = 120;
            this.nudMinutes.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudHours
            // 
            this.nudHours.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHours.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHours.Location = new System.Drawing.Point(7, 147);
            this.nudHours.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudHours.Name = "nudHours";
            this.nudHours.Size = new System.Drawing.Size(102, 20);
            this.nudHours.TabIndex = 119;
            this.nudHours.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(243, 131);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(49, 13);
            this.lblSeconds.TabIndex = 118;
            this.lblSeconds.Text = "Seconds";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(139, 131);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(44, 13);
            this.lblMinutes.TabIndex = 117;
            this.lblMinutes.Text = "Minutes";
            // 
            // lblHrs
            // 
            this.lblHrs.AutoSize = true;
            this.lblHrs.Location = new System.Drawing.Point(37, 131);
            this.lblHrs.Name = "lblHrs";
            this.lblHrs.Size = new System.Drawing.Size(35, 13);
            this.lblHrs.TabIndex = 116;
            this.lblHrs.Text = "Hours";
            // 
            // lstRequirement
            // 
            this.lstRequirement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstRequirement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRequirement.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstRequirement.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstRequirement.FormattingEnabled = true;
            this.lstRequirement.Location = new System.Drawing.Point(6, 19);
            this.lstRequirement.Name = "lstRequirement";
            this.lstRequirement.Size = new System.Drawing.Size(211, 106);
            this.lstRequirement.TabIndex = 113;
            // 
            // btnRemoveRequirement
            // 
            this.btnRemoveRequirement.Location = new System.Drawing.Point(187, 91);
            this.btnRemoveRequirement.Name = "btnRemoveRequirement";
            this.btnRemoveRequirement.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveRequirement.Size = new System.Drawing.Size(58, 23);
            this.btnRemoveRequirement.TabIndex = 117;
            this.btnRemoveRequirement.Text = "Remove";
            this.btnRemoveRequirement.Click += new System.EventHandler(this.btnRemoveRequirement_Click);
            // 
            // btnAddRequirement
            // 
            this.btnAddRequirement.Location = new System.Drawing.Point(187, 19);
            this.btnAddRequirement.Name = "btnAddRequirement";
            this.btnAddRequirement.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddRequirement.Size = new System.Drawing.Size(58, 23);
            this.btnAddRequirement.TabIndex = 116;
            this.btnAddRequirement.Text = "Add";
            this.btnAddRequirement.Click += new System.EventHandler(this.btnAddRequirement_Click);
            // 
            // lblPlayerNumber
            // 
            this.lblPlayerNumber.AutoSize = true;
            this.lblPlayerNumber.Location = new System.Drawing.Point(6, 21);
            this.lblPlayerNumber.Name = "lblPlayerNumber";
            this.lblPlayerNumber.Size = new System.Drawing.Size(61, 13);
            this.lblPlayerNumber.TabIndex = 115;
            this.lblPlayerNumber.Text = "No. Players";
            // 
            // lstPlayerCounts
            // 
            this.lstPlayerCounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstPlayerCounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPlayerCounts.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstPlayerCounts.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstPlayerCounts.FormattingEnabled = true;
            this.lstPlayerCounts.Location = new System.Drawing.Point(6, 47);
            this.lstPlayerCounts.Name = "lstPlayerCounts";
            this.lstPlayerCounts.Size = new System.Drawing.Size(169, 67);
            this.lstPlayerCounts.TabIndex = 112;
            this.lstPlayerCounts.SelectedIndexChanged += new System.EventHandler(this.lstPlayerCounts_SelectedIndexChanged);
            // 
            // nudTimerPlayers
            // 
            this.nudTimerPlayers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTimerPlayers.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTimerPlayers.Location = new System.Drawing.Point(73, 21);
            this.nudTimerPlayers.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudTimerPlayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimerPlayers.Name = "nudTimerPlayers";
            this.nudTimerPlayers.Size = new System.Drawing.Size(102, 20);
            this.nudTimerPlayers.TabIndex = 113;
            this.nudTimerPlayers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbTimer
            // 
            this.cmbTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTimer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTimer.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTimer.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTimer.DrawDropdownHoverOutline = false;
            this.cmbTimer.DrawFocusRectangle = false;
            this.cmbTimer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTimer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTimer.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTimer.FormattingEnabled = true;
            this.cmbTimer.Location = new System.Drawing.Point(45, 24);
            this.cmbTimer.Name = "cmbTimer";
            this.cmbTimer.Size = new System.Drawing.Size(209, 21);
            this.cmbTimer.TabIndex = 115;
            this.cmbTimer.Text = null;
            this.cmbTimer.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimer.SelectedIndexChanged += new System.EventHandler(this.cmbTimer_SelectedIndexChanged);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(6, 27);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(33, 13);
            this.lblTimer.TabIndex = 114;
            this.lblTimer.Text = "Timer";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(519, 17);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 54;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
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
            this.cmbFolder.Location = new System.Drawing.Point(321, 17);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(192, 21);
            this.cmbFolder.TabIndex = 53;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(279, 22);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 52;
            this.lblFolder.Text = "Folder";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(94, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(179, 20);
            this.txtName.TabIndex = 21;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(16, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 20;
            this.lblName.Text = "Name";
            // 
            // grpContentScaling
            // 
            this.grpContentScaling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpContentScaling.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpContentScaling.Controls.Add(this.cmbStatCeiling);
            this.grpContentScaling.Controls.Add(this.lblTierCeiling);
            this.grpContentScaling.Controls.Add(this.chkStatCeiling);
            this.grpContentScaling.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpContentScaling.Location = new System.Drawing.Point(196, 10);
            this.grpContentScaling.Name = "grpContentScaling";
            this.grpContentScaling.Size = new System.Drawing.Size(177, 72);
            this.grpContentScaling.TabIndex = 126;
            this.grpContentScaling.TabStop = false;
            this.grpContentScaling.Text = "Content Scaling";
            // 
            // chkStatCeiling
            // 
            this.chkStatCeiling.AutoSize = true;
            this.chkStatCeiling.Location = new System.Drawing.Point(7, 19);
            this.chkStatCeiling.Name = "chkStatCeiling";
            this.chkStatCeiling.Size = new System.Drawing.Size(117, 17);
            this.chkStatCeiling.TabIndex = 55;
            this.chkStatCeiling.Text = "Player Stat Ceiling?";
            this.chkStatCeiling.CheckedChanged += new System.EventHandler(this.chkStatCeiling_CheckedChanged);
            // 
            // lblTierCeiling
            // 
            this.lblTierCeiling.AutoSize = true;
            this.lblTierCeiling.Location = new System.Drawing.Point(4, 45);
            this.lblTierCeiling.Name = "lblTierCeiling";
            this.lblTierCeiling.Size = new System.Drawing.Size(38, 13);
            this.lblTierCeiling.TabIndex = 127;
            this.lblTierCeiling.Text = "Cap at";
            // 
            // cmbStatCeiling
            // 
            this.cmbStatCeiling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStatCeiling.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStatCeiling.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStatCeiling.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStatCeiling.DrawDropdownHoverOutline = false;
            this.cmbStatCeiling.DrawFocusRectangle = false;
            this.cmbStatCeiling.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatCeiling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatCeiling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatCeiling.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStatCeiling.FormattingEnabled = true;
            this.cmbStatCeiling.Location = new System.Drawing.Point(50, 42);
            this.cmbStatCeiling.Name = "cmbStatCeiling";
            this.cmbStatCeiling.Size = new System.Drawing.Size(121, 21);
            this.cmbStatCeiling.TabIndex = 128;
            this.cmbStatCeiling.Text = null;
            this.cmbStatCeiling.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbStatCeiling.SelectedIndexChanged += new System.EventHandler(this.cmbStatCeiling_SelectedIndexChanged);
            // 
            // frmDungeon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(936, 722);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpDungeons);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDungeon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dungeon Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpDungeons.ResumeLayout(false);
            this.grpDungeons.PerformLayout();
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            this.grpExtraControl.ResumeLayout(false);
            this.grpExtraControl.PerformLayout();
            this.grpRewards.ResumeLayout(false);
            this.grpGnome.ResumeLayout(false);
            this.grpGnome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGnomeRolls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGnomeLocations)).EndInit();
            this.grpTreasure.ResumeLayout(false);
            this.grpTreasure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTreasureRolls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTreasureLevels)).EndInit();
            this.grpTime.ResumeLayout(false);
            this.grpTime.PerformLayout();
            this.grpTimeReqs.ResumeLayout(false);
            this.grpTimeReqs.PerformLayout();
            this.grpTimes.ResumeLayout(false);
            this.grpTimes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimerPlayers)).EndInit();
            this.grpContentScaling.ResumeLayout(false);
            this.grpContentScaling.PerformLayout();
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
        private DarkUI.Controls.DarkGroupBox grpDungeons;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpEditor;
        private System.Windows.Forms.Label lblDisplayName;
        private DarkUI.Controls.DarkTextBox txtDisplayName;
        private DarkUI.Controls.DarkGroupBox grpTime;
        private DarkUI.Controls.DarkNumericUpDown nudTimerPlayers;
        private System.Windows.Forms.ListBox lstPlayerCounts;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkTextBox txtName;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkComboBox cmbTimer;
        private System.Windows.Forms.Label lblTimer;
        private DarkUI.Controls.DarkGroupBox grpTimeReqs;
        private System.Windows.Forms.Label lblPlayerNumber;
        private DarkUI.Controls.DarkButton btnRemoveRequirement;
        private DarkUI.Controls.DarkButton btnAddRequirement;
        private DarkUI.Controls.DarkGroupBox grpTimes;
        private System.Windows.Forms.ListBox lstRequirement;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Label lblHrs;
        private System.Windows.Forms.Label lblSeconds;
        private DarkUI.Controls.DarkNumericUpDown nudMinutes;
        private DarkUI.Controls.DarkNumericUpDown nudHours;
        private DarkUI.Controls.DarkNumericUpDown nudSeconds;
        private DarkUI.Controls.DarkButton removeTime;
        private DarkUI.Controls.DarkButton addTime;
        private DarkUI.Controls.DarkButton btnClearSelection;
        private DarkUI.Controls.DarkGroupBox grpRewards;
        private System.Windows.Forms.Label lblRewardLevel;
        private DarkUI.Controls.DarkGroupBox grpGnome;
        private DarkUI.Controls.DarkGroupBox grpTreasure;
        private DarkUI.Controls.DarkNumericUpDown nudGnomeLocations;
        private System.Windows.Forms.Label lblGnomeLocations;
        private DarkUI.Controls.DarkNumericUpDown nudTreasureLevels;
        private System.Windows.Forms.ListBox lstTreasureLevels;
        private System.Windows.Forms.ListBox lstTreasures;
        private System.Windows.Forms.Label lblLootTable;
        private DarkUI.Controls.DarkComboBox cmbLootTable;
        private DarkUI.Controls.DarkButton btnAddTable;
        private DarkUI.Controls.DarkButton btnRemoveTable;
        private DarkUI.Controls.DarkButton btnRemoveTreasureLevel;
        private DarkUI.Controls.DarkButton btnAddTreasureLevel;
        private System.Windows.Forms.Label lblLoot;
        private DarkUI.Controls.DarkComboBox cmbGnomeLootTable;
        private System.Windows.Forms.Label lblGnomeLootTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstGnomeLoot;
        private DarkUI.Controls.DarkButton btnRemoveGnomeLoot;
        private DarkUI.Controls.DarkButton btnAddGnomeLoot;
        private DarkUI.Controls.DarkButton btnSortPlayerCounts;
        private DarkUI.Controls.DarkButton btnSortReqs;
        private System.Windows.Forms.Label lblRolls;
        private DarkUI.Controls.DarkNumericUpDown nudGnomeRolls;
        private System.Windows.Forms.Label label2;
        private DarkUI.Controls.DarkNumericUpDown nudTreasureRolls;
        private System.Windows.Forms.Label lblExp;
        private DarkUI.Controls.DarkNumericUpDown nudExp;
        private DarkUI.Controls.DarkGroupBox grpExtraControl;
        private DarkUI.Controls.DarkCheckBox chkStoreLongest;
        private DarkUI.Controls.DarkCheckBox chkIgnoreCompletion;
        private DarkUI.Controls.DarkCheckBox chkIgnoreStart;
        private DarkUI.Controls.DarkGroupBox grpContentScaling;
        private DarkUI.Controls.DarkComboBox cmbStatCeiling;
        private System.Windows.Forms.Label lblTierCeiling;
        private DarkUI.Controls.DarkCheckBox chkStatCeiling;
    }
}