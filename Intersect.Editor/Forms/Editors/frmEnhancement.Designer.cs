
namespace Intersect.Editor.Forms.Editors
{
    partial class frmEnhancement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnhancement));
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
            this.toolStrip = new DarkUI.Controls.DarkToolStrip();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.grpEnhancements = new DarkUI.Controls.DarkGroupBox();
            this.grpProps = new DarkUI.Controls.DarkGroupBox();
            this.grpPrereqs = new DarkUI.Controls.DarkGroupBox();
            this.btnAddPrereq = new DarkUI.Controls.DarkButton();
            this.btnRemovePrereq = new DarkUI.Controls.DarkButton();
            this.lstPrereqs = new System.Windows.Forms.ListBox();
            this.cmbEnhancementPrereq = new DarkUI.Controls.DarkComboBox();
            this.lblPrereqEnhancement = new System.Windows.Forms.Label();
            this.grpWeaponTypes = new DarkUI.Controls.DarkGroupBox();
            this.lblMinLvl = new System.Windows.Forms.Label();
            this.nudMinWeaponLevel = new DarkUI.Controls.DarkNumericUpDown();
            this.btnAddWeaponType = new DarkUI.Controls.DarkButton();
            this.btnRemoveWeaponType = new DarkUI.Controls.DarkButton();
            this.lstWeaponTypes = new System.Windows.Forms.ListBox();
            this.cmbWeaponTypes = new DarkUI.Controls.DarkComboBox();
            this.lblWeaponType = new System.Windows.Forms.Label();
            this.nudReqEp = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCost = new System.Windows.Forms.Label();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.grpBonuses = new DarkUI.Controls.DarkGroupBox();
            this.lstBonuses = new System.Windows.Forms.ListBox();
            this.cmbEffect = new DarkUI.Controls.DarkComboBox();
            this.btnRemoveBonus = new DarkUI.Controls.DarkButton();
            this.btnAddBonus = new DarkUI.Controls.DarkButton();
            this.label3 = new System.Windows.Forms.Label();
            this.nudBonusMax = new DarkUI.Controls.DarkNumericUpDown();
            this.nudBonusMin = new DarkUI.Controls.DarkNumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEffect = new System.Windows.Forms.Label();
            this.grpVitalMods = new DarkUI.Controls.DarkGroupBox();
            this.lstVitalMods = new System.Windows.Forms.ListBox();
            this.cmbVitals = new DarkUI.Controls.DarkComboBox();
            this.btnRemoveVital = new DarkUI.Controls.DarkButton();
            this.btnAddVital = new DarkUI.Controls.DarkButton();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMaxVital = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMinVital = new DarkUI.Controls.DarkNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVital = new System.Windows.Forms.Label();
            this.grpStats = new DarkUI.Controls.DarkGroupBox();
            this.lstStatBuffs = new System.Windows.Forms.ListBox();
            this.cmbStats = new DarkUI.Controls.DarkComboBox();
            this.btnRemoveStat = new DarkUI.Controls.DarkButton();
            this.btnAddStat = new DarkUI.Controls.DarkButton();
            this.lblMax = new System.Windows.Forms.Label();
            this.nudMaxStat = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMinStat = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblStat = new System.Windows.Forms.Label();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpSpellProcs = new DarkUI.Controls.DarkGroupBox();
            this.lstSpellEffects = new System.Windows.Forms.ListBox();
            this.cmbSpells = new DarkUI.Controls.DarkComboBox();
            this.btnRemoveSpell = new DarkUI.Controls.DarkButton();
            this.btnAddSpell = new DarkUI.Controls.DarkButton();
            this.label5 = new System.Windows.Forms.Label();
            this.nudMaxSpell = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMinSpell = new DarkUI.Controls.DarkNumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSpell = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.grpEnhancements.SuspendLayout();
            this.grpProps.SuspendLayout();
            this.grpPrereqs.SuspendLayout();
            this.grpWeaponTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinWeaponLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReqEp)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.grpBonuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusMin)).BeginInit();
            this.grpVitalMods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVital)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinVital)).BeginInit();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinStat)).BeginInit();
            this.grpSpellProcs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSpell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinSpell)).BeginInit();
            this.SuspendLayout();
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
            this.toolStrip.Size = new System.Drawing.Size(994, 25);
            this.toolStrip.TabIndex = 44;
            this.toolStrip.Text = "toolStrip1";
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
            this.lstGameObjects.Location = new System.Drawing.Point(6, 39);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(191, 468);
            this.lstGameObjects.TabIndex = 26;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(6, 13);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 20);
            this.txtSearch.TabIndex = 27;
            this.txtSearch.Text = "Search...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 13);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(18, 20);
            this.btnClearSearch.TabIndex = 28;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // grpEnhancements
            // 
            this.grpEnhancements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEnhancements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEnhancements.Controls.Add(this.btnClearSearch);
            this.grpEnhancements.Controls.Add(this.txtSearch);
            this.grpEnhancements.Controls.Add(this.lstGameObjects);
            this.grpEnhancements.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEnhancements.Location = new System.Drawing.Point(12, 28);
            this.grpEnhancements.Name = "grpEnhancements";
            this.grpEnhancements.Size = new System.Drawing.Size(203, 513);
            this.grpEnhancements.TabIndex = 45;
            this.grpEnhancements.TabStop = false;
            this.grpEnhancements.Text = "Enhancements";
            // 
            // grpProps
            // 
            this.grpProps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpProps.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpProps.Controls.Add(this.grpPrereqs);
            this.grpProps.Controls.Add(this.grpWeaponTypes);
            this.grpProps.Controls.Add(this.nudReqEp);
            this.grpProps.Controls.Add(this.lblCost);
            this.grpProps.Controls.Add(this.btnAddFolder);
            this.grpProps.Controls.Add(this.cmbFolder);
            this.grpProps.Controls.Add(this.lblFolder);
            this.grpProps.Controls.Add(this.txtName);
            this.grpProps.Controls.Add(this.lblName);
            this.grpProps.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpProps.Location = new System.Drawing.Point(3, 3);
            this.grpProps.Name = "grpProps";
            this.grpProps.Size = new System.Drawing.Size(359, 371);
            this.grpProps.TabIndex = 46;
            this.grpProps.TabStop = false;
            this.grpProps.Text = "Properties";
            // 
            // grpPrereqs
            // 
            this.grpPrereqs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPrereqs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPrereqs.Controls.Add(this.btnAddPrereq);
            this.grpPrereqs.Controls.Add(this.btnRemovePrereq);
            this.grpPrereqs.Controls.Add(this.lstPrereqs);
            this.grpPrereqs.Controls.Add(this.cmbEnhancementPrereq);
            this.grpPrereqs.Controls.Add(this.lblPrereqEnhancement);
            this.grpPrereqs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPrereqs.Location = new System.Drawing.Point(11, 233);
            this.grpPrereqs.Name = "grpPrereqs";
            this.grpPrereqs.Size = new System.Drawing.Size(342, 128);
            this.grpPrereqs.TabIndex = 60;
            this.grpPrereqs.TabStop = false;
            this.grpPrereqs.Text = "Prerequisite Enhancements";
            // 
            // btnAddPrereq
            // 
            this.btnAddPrereq.Location = new System.Drawing.Point(163, 89);
            this.btnAddPrereq.Name = "btnAddPrereq";
            this.btnAddPrereq.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddPrereq.Size = new System.Drawing.Size(79, 23);
            this.btnAddPrereq.TabIndex = 59;
            this.btnAddPrereq.Text = "Add";
            this.btnAddPrereq.Click += new System.EventHandler(this.btnAddPrereq_Click);
            // 
            // btnRemovePrereq
            // 
            this.btnRemovePrereq.Location = new System.Drawing.Point(249, 89);
            this.btnRemovePrereq.Name = "btnRemovePrereq";
            this.btnRemovePrereq.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemovePrereq.Size = new System.Drawing.Size(79, 23);
            this.btnRemovePrereq.TabIndex = 58;
            this.btnRemovePrereq.Text = "Remove";
            this.btnRemovePrereq.Click += new System.EventHandler(this.btnRemovePrereq_Click);
            // 
            // lstPrereqs
            // 
            this.lstPrereqs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstPrereqs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPrereqs.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstPrereqs.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstPrereqs.FormattingEnabled = true;
            this.lstPrereqs.Location = new System.Drawing.Point(9, 42);
            this.lstPrereqs.Name = "lstPrereqs";
            this.lstPrereqs.Size = new System.Drawing.Size(319, 41);
            this.lstPrereqs.TabIndex = 57;
            // 
            // cmbEnhancementPrereq
            // 
            this.cmbEnhancementPrereq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEnhancementPrereq.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEnhancementPrereq.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEnhancementPrereq.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEnhancementPrereq.DrawDropdownHoverOutline = false;
            this.cmbEnhancementPrereq.DrawFocusRectangle = false;
            this.cmbEnhancementPrereq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEnhancementPrereq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnhancementPrereq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEnhancementPrereq.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEnhancementPrereq.FormattingEnabled = true;
            this.cmbEnhancementPrereq.Location = new System.Drawing.Point(109, 15);
            this.cmbEnhancementPrereq.Name = "cmbEnhancementPrereq";
            this.cmbEnhancementPrereq.Size = new System.Drawing.Size(217, 21);
            this.cmbEnhancementPrereq.TabIndex = 56;
            this.cmbEnhancementPrereq.Text = null;
            this.cmbEnhancementPrereq.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblPrereqEnhancement
            // 
            this.lblPrereqEnhancement.AutoSize = true;
            this.lblPrereqEnhancement.Location = new System.Drawing.Point(6, 18);
            this.lblPrereqEnhancement.Name = "lblPrereqEnhancement";
            this.lblPrereqEnhancement.Size = new System.Drawing.Size(73, 13);
            this.lblPrereqEnhancement.TabIndex = 21;
            this.lblPrereqEnhancement.Text = "Enhancement";
            // 
            // grpWeaponTypes
            // 
            this.grpWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponTypes.Controls.Add(this.lblMinLvl);
            this.grpWeaponTypes.Controls.Add(this.nudMinWeaponLevel);
            this.grpWeaponTypes.Controls.Add(this.btnAddWeaponType);
            this.grpWeaponTypes.Controls.Add(this.btnRemoveWeaponType);
            this.grpWeaponTypes.Controls.Add(this.lstWeaponTypes);
            this.grpWeaponTypes.Controls.Add(this.cmbWeaponTypes);
            this.grpWeaponTypes.Controls.Add(this.lblWeaponType);
            this.grpWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponTypes.Location = new System.Drawing.Point(11, 99);
            this.grpWeaponTypes.Name = "grpWeaponTypes";
            this.grpWeaponTypes.Size = new System.Drawing.Size(342, 128);
            this.grpWeaponTypes.TabIndex = 51;
            this.grpWeaponTypes.TabStop = false;
            this.grpWeaponTypes.Text = "Applicable Weapon Types";
            // 
            // lblMinLvl
            // 
            this.lblMinLvl.AutoSize = true;
            this.lblMinLvl.Location = new System.Drawing.Point(6, 102);
            this.lblMinLvl.Name = "lblMinLvl";
            this.lblMinLvl.Size = new System.Drawing.Size(73, 13);
            this.lblMinLvl.TabIndex = 52;
            this.lblMinLvl.Text = "Min. Wep. Lvl";
            // 
            // nudMinWeaponLevel
            // 
            this.nudMinWeaponLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinWeaponLevel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinWeaponLevel.Location = new System.Drawing.Point(85, 100);
            this.nudMinWeaponLevel.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudMinWeaponLevel.Name = "nudMinWeaponLevel";
            this.nudMinWeaponLevel.Size = new System.Drawing.Size(72, 20);
            this.nudMinWeaponLevel.TabIndex = 53;
            this.nudMinWeaponLevel.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMinWeaponLevel.ValueChanged += new System.EventHandler(this.nudMinWeaponLevel_ValueChanged);
            // 
            // btnAddWeaponType
            // 
            this.btnAddWeaponType.Location = new System.Drawing.Point(163, 100);
            this.btnAddWeaponType.Name = "btnAddWeaponType";
            this.btnAddWeaponType.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddWeaponType.Size = new System.Drawing.Size(79, 23);
            this.btnAddWeaponType.TabIndex = 59;
            this.btnAddWeaponType.Text = "Add";
            this.btnAddWeaponType.Click += new System.EventHandler(this.btnAddWeaponType_Click);
            // 
            // btnRemoveWeaponType
            // 
            this.btnRemoveWeaponType.Location = new System.Drawing.Point(249, 100);
            this.btnRemoveWeaponType.Name = "btnRemoveWeaponType";
            this.btnRemoveWeaponType.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveWeaponType.Size = new System.Drawing.Size(79, 23);
            this.btnRemoveWeaponType.TabIndex = 58;
            this.btnRemoveWeaponType.Text = "Remove";
            this.btnRemoveWeaponType.Click += new System.EventHandler(this.btnRemoveWeaponType_Click);
            // 
            // lstWeaponTypes
            // 
            this.lstWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstWeaponTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstWeaponTypes.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstWeaponTypes.FormattingEnabled = true;
            this.lstWeaponTypes.Location = new System.Drawing.Point(9, 42);
            this.lstWeaponTypes.Name = "lstWeaponTypes";
            this.lstWeaponTypes.Size = new System.Drawing.Size(319, 54);
            this.lstWeaponTypes.TabIndex = 57;
            // 
            // cmbWeaponTypes
            // 
            this.cmbWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbWeaponTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbWeaponTypes.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbWeaponTypes.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbWeaponTypes.DrawDropdownHoverOutline = false;
            this.cmbWeaponTypes.DrawFocusRectangle = false;
            this.cmbWeaponTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeaponTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbWeaponTypes.FormattingEnabled = true;
            this.cmbWeaponTypes.Location = new System.Drawing.Point(109, 15);
            this.cmbWeaponTypes.Name = "cmbWeaponTypes";
            this.cmbWeaponTypes.Size = new System.Drawing.Size(217, 21);
            this.cmbWeaponTypes.TabIndex = 56;
            this.cmbWeaponTypes.Text = null;
            this.cmbWeaponTypes.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblWeaponType
            // 
            this.lblWeaponType.AutoSize = true;
            this.lblWeaponType.Location = new System.Drawing.Point(6, 18);
            this.lblWeaponType.Name = "lblWeaponType";
            this.lblWeaponType.Size = new System.Drawing.Size(75, 13);
            this.lblWeaponType.TabIndex = 21;
            this.lblWeaponType.Text = "Weapon Type";
            // 
            // nudReqEp
            // 
            this.nudReqEp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudReqEp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudReqEp.Location = new System.Drawing.Point(157, 74);
            this.nudReqEp.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudReqEp.Name = "nudReqEp";
            this.nudReqEp.Size = new System.Drawing.Size(129, 20);
            this.nudReqEp.TabIndex = 50;
            this.nudReqEp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudReqEp.ValueChanged += new System.EventHandler(this.nudReqEp_ValueChanged);
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(93, 76);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(47, 13);
            this.lblCost.TabIndex = 49;
            this.lblCost.Text = "Req. EP";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(292, 42);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 48;
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
            this.cmbFolder.Location = new System.Drawing.Point(60, 43);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(226, 21);
            this.cmbFolder.TabIndex = 47;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 46);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 46;
            this.lblFolder.Text = "Folder";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(60, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 21;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 20;
            this.lblName.Text = "Name";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.grpSpellProcs);
            this.pnlContainer.Controls.Add(this.grpBonuses);
            this.pnlContainer.Controls.Add(this.grpVitalMods);
            this.pnlContainer.Controls.Add(this.grpStats);
            this.pnlContainer.Controls.Add(this.grpProps);
            this.pnlContainer.Location = new System.Drawing.Point(221, 28);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(763, 513);
            this.pnlContainer.TabIndex = 47;
            this.pnlContainer.Visible = false;
            // 
            // grpBonuses
            // 
            this.grpBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBonuses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBonuses.Controls.Add(this.lstBonuses);
            this.grpBonuses.Controls.Add(this.cmbEffect);
            this.grpBonuses.Controls.Add(this.btnRemoveBonus);
            this.grpBonuses.Controls.Add(this.btnAddBonus);
            this.grpBonuses.Controls.Add(this.label3);
            this.grpBonuses.Controls.Add(this.nudBonusMax);
            this.grpBonuses.Controls.Add(this.nudBonusMin);
            this.grpBonuses.Controls.Add(this.label4);
            this.grpBonuses.Controls.Add(this.lblEffect);
            this.grpBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBonuses.Location = new System.Drawing.Point(374, 367);
            this.grpBonuses.Name = "grpBonuses";
            this.grpBonuses.Size = new System.Drawing.Size(359, 171);
            this.grpBonuses.TabIndex = 58;
            this.grpBonuses.TabStop = false;
            this.grpBonuses.Text = "Bonus Effects";
            // 
            // lstBonuses
            // 
            this.lstBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBonuses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBonuses.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBonuses.FormattingEnabled = true;
            this.lstBonuses.Location = new System.Drawing.Point(9, 41);
            this.lstBonuses.Name = "lstBonuses";
            this.lstBonuses.Size = new System.Drawing.Size(255, 54);
            this.lstBonuses.TabIndex = 56;
            this.lstBonuses.SelectedIndexChanged += new System.EventHandler(this.lstBonuses_SelectedIndexChanged);
            // 
            // cmbEffect
            // 
            this.cmbEffect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEffect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEffect.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEffect.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEffect.DrawDropdownHoverOutline = false;
            this.cmbEffect.DrawFocusRectangle = false;
            this.cmbEffect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEffect.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEffect.FormattingEnabled = true;
            this.cmbEffect.Location = new System.Drawing.Point(47, 14);
            this.cmbEffect.Name = "cmbEffect";
            this.cmbEffect.Size = new System.Drawing.Size(217, 21);
            this.cmbEffect.TabIndex = 55;
            this.cmbEffect.Text = null;
            this.cmbEffect.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnRemoveBonus
            // 
            this.btnRemoveBonus.Location = new System.Drawing.Point(270, 72);
            this.btnRemoveBonus.Name = "btnRemoveBonus";
            this.btnRemoveBonus.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveBonus.Size = new System.Drawing.Size(79, 23);
            this.btnRemoveBonus.TabIndex = 54;
            this.btnRemoveBonus.Text = "Remove";
            this.btnRemoveBonus.Click += new System.EventHandler(this.btnRemoveBonus_Click);
            // 
            // btnAddBonus
            // 
            this.btnAddBonus.Location = new System.Drawing.Point(270, 142);
            this.btnAddBonus.Name = "btnAddBonus";
            this.btnAddBonus.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddBonus.Size = new System.Drawing.Size(79, 23);
            this.btnAddBonus.TabIndex = 53;
            this.btnAddBonus.Text = "Add/Replace";
            this.btnAddBonus.Click += new System.EventHandler(this.btnAddBonus_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(180, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Max";
            // 
            // nudBonusMax
            // 
            this.nudBonusMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBonusMax.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBonusMax.Location = new System.Drawing.Point(210, 117);
            this.nudBonusMax.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudBonusMax.Name = "nudBonusMax";
            this.nudBonusMax.Size = new System.Drawing.Size(129, 20);
            this.nudBonusMax.TabIndex = 51;
            this.nudBonusMax.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudBonusMin
            // 
            this.nudBonusMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBonusMin.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBonusMin.Location = new System.Drawing.Point(38, 117);
            this.nudBonusMin.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudBonusMin.Name = "nudBonusMin";
            this.nudBonusMin.Size = new System.Drawing.Size(129, 20);
            this.nudBonusMin.TabIndex = 50;
            this.nudBonusMin.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Min";
            // 
            // lblEffect
            // 
            this.lblEffect.AutoSize = true;
            this.lblEffect.Location = new System.Drawing.Point(8, 17);
            this.lblEffect.Name = "lblEffect";
            this.lblEffect.Size = new System.Drawing.Size(35, 13);
            this.lblEffect.TabIndex = 20;
            this.lblEffect.Text = "Effect";
            // 
            // grpVitalMods
            // 
            this.grpVitalMods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpVitalMods.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVitalMods.Controls.Add(this.lstVitalMods);
            this.grpVitalMods.Controls.Add(this.cmbVitals);
            this.grpVitalMods.Controls.Add(this.btnRemoveVital);
            this.grpVitalMods.Controls.Add(this.btnAddVital);
            this.grpVitalMods.Controls.Add(this.label1);
            this.grpVitalMods.Controls.Add(this.nudMaxVital);
            this.grpVitalMods.Controls.Add(this.nudMinVital);
            this.grpVitalMods.Controls.Add(this.label2);
            this.grpVitalMods.Controls.Add(this.lblVital);
            this.grpVitalMods.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVitalMods.Location = new System.Drawing.Point(374, 190);
            this.grpVitalMods.Name = "grpVitalMods";
            this.grpVitalMods.Size = new System.Drawing.Size(359, 171);
            this.grpVitalMods.TabIndex = 57;
            this.grpVitalMods.TabStop = false;
            this.grpVitalMods.Text = "Vital Mods";
            // 
            // lstVitalMods
            // 
            this.lstVitalMods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstVitalMods.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstVitalMods.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstVitalMods.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstVitalMods.FormattingEnabled = true;
            this.lstVitalMods.Location = new System.Drawing.Point(9, 41);
            this.lstVitalMods.Name = "lstVitalMods";
            this.lstVitalMods.Size = new System.Drawing.Size(255, 54);
            this.lstVitalMods.TabIndex = 56;
            this.lstVitalMods.SelectedIndexChanged += new System.EventHandler(this.lstVitalMods_SelectedIndexChanged);
            // 
            // cmbVitals
            // 
            this.cmbVitals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVitals.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVitals.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVitals.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVitals.DrawDropdownHoverOutline = false;
            this.cmbVitals.DrawFocusRectangle = false;
            this.cmbVitals.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVitals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVitals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVitals.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVitals.FormattingEnabled = true;
            this.cmbVitals.Location = new System.Drawing.Point(38, 14);
            this.cmbVitals.Name = "cmbVitals";
            this.cmbVitals.Size = new System.Drawing.Size(226, 21);
            this.cmbVitals.TabIndex = 55;
            this.cmbVitals.Text = null;
            this.cmbVitals.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnRemoveVital
            // 
            this.btnRemoveVital.Location = new System.Drawing.Point(270, 72);
            this.btnRemoveVital.Name = "btnRemoveVital";
            this.btnRemoveVital.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveVital.Size = new System.Drawing.Size(79, 23);
            this.btnRemoveVital.TabIndex = 54;
            this.btnRemoveVital.Text = "Remove";
            this.btnRemoveVital.Click += new System.EventHandler(this.btnRemoveVital_Click);
            // 
            // btnAddVital
            // 
            this.btnAddVital.Location = new System.Drawing.Point(270, 142);
            this.btnAddVital.Name = "btnAddVital";
            this.btnAddVital.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddVital.Size = new System.Drawing.Size(79, 23);
            this.btnAddVital.TabIndex = 53;
            this.btnAddVital.Text = "Add/Replace";
            this.btnAddVital.Click += new System.EventHandler(this.btnAddVital_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Max";
            // 
            // nudMaxVital
            // 
            this.nudMaxVital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxVital.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxVital.Location = new System.Drawing.Point(210, 117);
            this.nudMaxVital.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudMaxVital.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.nudMaxVital.Name = "nudMaxVital";
            this.nudMaxVital.Size = new System.Drawing.Size(129, 20);
            this.nudMaxVital.TabIndex = 51;
            this.nudMaxVital.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudMinVital
            // 
            this.nudMinVital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinVital.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinVital.Location = new System.Drawing.Point(38, 117);
            this.nudMinVital.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudMinVital.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.nudMinVital.Name = "nudMinVital";
            this.nudMinVital.Size = new System.Drawing.Size(129, 20);
            this.nudMinVital.TabIndex = 50;
            this.nudMinVital.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Min";
            // 
            // lblVital
            // 
            this.lblVital.AutoSize = true;
            this.lblVital.Location = new System.Drawing.Point(6, 17);
            this.lblVital.Name = "lblVital";
            this.lblVital.Size = new System.Drawing.Size(27, 13);
            this.lblVital.TabIndex = 20;
            this.lblVital.Text = "Vital";
            // 
            // grpStats
            // 
            this.grpStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStats.Controls.Add(this.lstStatBuffs);
            this.grpStats.Controls.Add(this.cmbStats);
            this.grpStats.Controls.Add(this.btnRemoveStat);
            this.grpStats.Controls.Add(this.btnAddStat);
            this.grpStats.Controls.Add(this.lblMax);
            this.grpStats.Controls.Add(this.nudMaxStat);
            this.grpStats.Controls.Add(this.nudMinStat);
            this.grpStats.Controls.Add(this.lblMin);
            this.grpStats.Controls.Add(this.lblStat);
            this.grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStats.Location = new System.Drawing.Point(374, 13);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(359, 171);
            this.grpStats.TabIndex = 51;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Stat Mods";
            // 
            // lstStatBuffs
            // 
            this.lstStatBuffs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstStatBuffs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstStatBuffs.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstStatBuffs.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstStatBuffs.FormattingEnabled = true;
            this.lstStatBuffs.Location = new System.Drawing.Point(9, 41);
            this.lstStatBuffs.Name = "lstStatBuffs";
            this.lstStatBuffs.Size = new System.Drawing.Size(255, 54);
            this.lstStatBuffs.TabIndex = 56;
            this.lstStatBuffs.SelectedIndexChanged += new System.EventHandler(this.lstStatBuffs_SelectedIndexChanged);
            // 
            // cmbStats
            // 
            this.cmbStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStats.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStats.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStats.DrawDropdownHoverOutline = false;
            this.cmbStats.DrawFocusRectangle = false;
            this.cmbStats.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStats.FormattingEnabled = true;
            this.cmbStats.Location = new System.Drawing.Point(38, 14);
            this.cmbStats.Name = "cmbStats";
            this.cmbStats.Size = new System.Drawing.Size(226, 21);
            this.cmbStats.TabIndex = 55;
            this.cmbStats.Text = null;
            this.cmbStats.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnRemoveStat
            // 
            this.btnRemoveStat.Location = new System.Drawing.Point(270, 72);
            this.btnRemoveStat.Name = "btnRemoveStat";
            this.btnRemoveStat.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveStat.Size = new System.Drawing.Size(79, 23);
            this.btnRemoveStat.TabIndex = 54;
            this.btnRemoveStat.Text = "Remove";
            this.btnRemoveStat.Click += new System.EventHandler(this.btnRemoveStat_Click);
            // 
            // btnAddStat
            // 
            this.btnAddStat.Location = new System.Drawing.Point(270, 142);
            this.btnAddStat.Name = "btnAddStat";
            this.btnAddStat.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddStat.Size = new System.Drawing.Size(79, 23);
            this.btnAddStat.TabIndex = 53;
            this.btnAddStat.Text = "Add/Replace";
            this.btnAddStat.Click += new System.EventHandler(this.btnAddStat_Click);
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(180, 119);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(27, 13);
            this.lblMax.TabIndex = 52;
            this.lblMax.Text = "Max";
            // 
            // nudMaxStat
            // 
            this.nudMaxStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxStat.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxStat.Location = new System.Drawing.Point(210, 117);
            this.nudMaxStat.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudMaxStat.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.nudMaxStat.Name = "nudMaxStat";
            this.nudMaxStat.Size = new System.Drawing.Size(129, 20);
            this.nudMaxStat.TabIndex = 51;
            this.nudMaxStat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudMinStat
            // 
            this.nudMinStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinStat.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinStat.Location = new System.Drawing.Point(38, 117);
            this.nudMinStat.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudMinStat.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.nudMinStat.Name = "nudMinStat";
            this.nudMinStat.Size = new System.Drawing.Size(129, 20);
            this.nudMinStat.TabIndex = 50;
            this.nudMinStat.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(8, 119);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(24, 13);
            this.lblMin.TabIndex = 49;
            this.lblMin.Text = "Min";
            // 
            // lblStat
            // 
            this.lblStat.AutoSize = true;
            this.lblStat.Location = new System.Drawing.Point(6, 17);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(26, 13);
            this.lblStat.TabIndex = 20;
            this.lblStat.Text = "Stat";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(630, 547);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(169, 27);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(805, 547);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(172, 27);
            this.btnCancel.TabIndex = 49;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpSpellProcs
            // 
            this.grpSpellProcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpellProcs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpellProcs.Controls.Add(this.lstSpellEffects);
            this.grpSpellProcs.Controls.Add(this.cmbSpells);
            this.grpSpellProcs.Controls.Add(this.btnRemoveSpell);
            this.grpSpellProcs.Controls.Add(this.btnAddSpell);
            this.grpSpellProcs.Controls.Add(this.label5);
            this.grpSpellProcs.Controls.Add(this.nudMaxSpell);
            this.grpSpellProcs.Controls.Add(this.nudMinSpell);
            this.grpSpellProcs.Controls.Add(this.label6);
            this.grpSpellProcs.Controls.Add(this.lblSpell);
            this.grpSpellProcs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpellProcs.Location = new System.Drawing.Point(374, 544);
            this.grpSpellProcs.Name = "grpSpellProcs";
            this.grpSpellProcs.Size = new System.Drawing.Size(359, 171);
            this.grpSpellProcs.TabIndex = 59;
            this.grpSpellProcs.TabStop = false;
            this.grpSpellProcs.Text = "Spell Effects";
            // 
            // lstSpellEffects
            // 
            this.lstSpellEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstSpellEffects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSpellEffects.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstSpellEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstSpellEffects.FormattingEnabled = true;
            this.lstSpellEffects.Location = new System.Drawing.Point(9, 41);
            this.lstSpellEffects.Name = "lstSpellEffects";
            this.lstSpellEffects.Size = new System.Drawing.Size(255, 54);
            this.lstSpellEffects.TabIndex = 56;
            // 
            // cmbSpells
            // 
            this.cmbSpells.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpells.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpells.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpells.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpells.DrawDropdownHoverOutline = false;
            this.cmbSpells.DrawFocusRectangle = false;
            this.cmbSpells.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpells.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpells.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpells.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpells.FormattingEnabled = true;
            this.cmbSpells.Location = new System.Drawing.Point(47, 14);
            this.cmbSpells.Name = "cmbSpells";
            this.cmbSpells.Size = new System.Drawing.Size(217, 21);
            this.cmbSpells.TabIndex = 55;
            this.cmbSpells.Text = null;
            this.cmbSpells.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnRemoveSpell
            // 
            this.btnRemoveSpell.Location = new System.Drawing.Point(270, 72);
            this.btnRemoveSpell.Name = "btnRemoveSpell";
            this.btnRemoveSpell.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveSpell.Size = new System.Drawing.Size(79, 23);
            this.btnRemoveSpell.TabIndex = 54;
            this.btnRemoveSpell.Text = "Remove";
            this.btnRemoveSpell.Click += new System.EventHandler(this.btnRemoveSpell_Click);
            // 
            // btnAddSpell
            // 
            this.btnAddSpell.Location = new System.Drawing.Point(270, 142);
            this.btnAddSpell.Name = "btnAddSpell";
            this.btnAddSpell.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddSpell.Size = new System.Drawing.Size(79, 23);
            this.btnAddSpell.TabIndex = 53;
            this.btnAddSpell.Text = "Add/Replace";
            this.btnAddSpell.Click += new System.EventHandler(this.btnAddSpell_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Max";
            // 
            // nudMaxSpell
            // 
            this.nudMaxSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxSpell.Location = new System.Drawing.Point(210, 117);
            this.nudMaxSpell.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudMaxSpell.Name = "nudMaxSpell";
            this.nudMaxSpell.Size = new System.Drawing.Size(129, 20);
            this.nudMaxSpell.TabIndex = 51;
            this.nudMaxSpell.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudMinSpell
            // 
            this.nudMinSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinSpell.Location = new System.Drawing.Point(38, 117);
            this.nudMinSpell.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudMinSpell.Name = "nudMinSpell";
            this.nudMinSpell.Size = new System.Drawing.Size(129, 20);
            this.nudMinSpell.TabIndex = 50;
            this.nudMinSpell.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Min";
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(6, 17);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(30, 13);
            this.lblSpell.TabIndex = 20;
            this.lblSpell.Text = "Spell";
            // 
            // frmEnhancement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(994, 586);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.grpEnhancements);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEnhancement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enhancement Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpEnhancements.ResumeLayout(false);
            this.grpEnhancements.PerformLayout();
            this.grpProps.ResumeLayout(false);
            this.grpProps.PerformLayout();
            this.grpPrereqs.ResumeLayout(false);
            this.grpPrereqs.PerformLayout();
            this.grpWeaponTypes.ResumeLayout(false);
            this.grpWeaponTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinWeaponLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReqEp)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.grpBonuses.ResumeLayout(false);
            this.grpBonuses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusMin)).EndInit();
            this.grpVitalMods.ResumeLayout(false);
            this.grpVitalMods.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxVital)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinVital)).EndInit();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinStat)).EndInit();
            this.grpSpellProcs.ResumeLayout(false);
            this.grpSpellProcs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxSpell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinSpell)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

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
        private DarkUI.Controls.DarkToolStrip toolStrip;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkGroupBox grpEnhancements;
        private DarkUI.Controls.DarkGroupBox grpProps;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblCost;
        private DarkUI.Controls.DarkNumericUpDown nudReqEp;
        private DarkUI.Controls.DarkGroupBox grpStats;
        private DarkUI.Controls.DarkNumericUpDown nudMinStat;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.Label lblMax;
        private DarkUI.Controls.DarkNumericUpDown nudMaxStat;
        private DarkUI.Controls.DarkButton btnAddStat;
        private DarkUI.Controls.DarkComboBox cmbStats;
        private DarkUI.Controls.DarkButton btnRemoveStat;
        private System.Windows.Forms.ListBox lstStatBuffs;
        private DarkUI.Controls.DarkGroupBox grpVitalMods;
        private System.Windows.Forms.ListBox lstVitalMods;
        private DarkUI.Controls.DarkComboBox cmbVitals;
        private DarkUI.Controls.DarkButton btnRemoveVital;
        private DarkUI.Controls.DarkButton btnAddVital;
        private System.Windows.Forms.Label label1;
        private DarkUI.Controls.DarkNumericUpDown nudMaxVital;
        private DarkUI.Controls.DarkNumericUpDown nudMinVital;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVital;
        private DarkUI.Controls.DarkGroupBox grpBonuses;
        private System.Windows.Forms.ListBox lstBonuses;
        private DarkUI.Controls.DarkComboBox cmbEffect;
        private DarkUI.Controls.DarkButton btnRemoveBonus;
        private DarkUI.Controls.DarkButton btnAddBonus;
        private System.Windows.Forms.Label label3;
        private DarkUI.Controls.DarkNumericUpDown nudBonusMax;
        private DarkUI.Controls.DarkNumericUpDown nudBonusMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEffect;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpWeaponTypes;
        private DarkUI.Controls.DarkButton btnRemoveWeaponType;
        private System.Windows.Forms.ListBox lstWeaponTypes;
        private DarkUI.Controls.DarkComboBox cmbWeaponTypes;
        private System.Windows.Forms.Label lblWeaponType;
        private DarkUI.Controls.DarkButton btnAddWeaponType;
        private DarkUI.Controls.DarkNumericUpDown nudMinWeaponLevel;
        private System.Windows.Forms.Label lblMinLvl;
        private DarkUI.Controls.DarkGroupBox grpPrereqs;
        private DarkUI.Controls.DarkButton btnAddPrereq;
        private DarkUI.Controls.DarkButton btnRemovePrereq;
        private System.Windows.Forms.ListBox lstPrereqs;
        private DarkUI.Controls.DarkComboBox cmbEnhancementPrereq;
        private System.Windows.Forms.Label lblPrereqEnhancement;
        private DarkUI.Controls.DarkGroupBox grpSpellProcs;
        private System.Windows.Forms.ListBox lstSpellEffects;
        private DarkUI.Controls.DarkComboBox cmbSpells;
        private DarkUI.Controls.DarkButton btnRemoveSpell;
        private DarkUI.Controls.DarkButton btnAddSpell;
        private System.Windows.Forms.Label label5;
        private DarkUI.Controls.DarkNumericUpDown nudMaxSpell;
        private DarkUI.Controls.DarkNumericUpDown nudMinSpell;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSpell;
    }
}