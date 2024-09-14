
namespace Intersect.Editor.Forms.Editors
{
    partial class frmChallenge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChallenge));
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
            this.grpWeaponTypes = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.grpEditor = new DarkUI.Controls.DarkGroupBox();
            this.grpContractInfo = new DarkUI.Controls.DarkGroupBox();
            this.txtRequirementDescription = new DarkUI.Controls.DarkTextBox();
            this.lblRequirementDescription = new System.Windows.Forms.Label();
            this.btnDynamicRequirements = new DarkUI.Controls.DarkButton();
            this.chkRequiresContract = new DarkUI.Controls.DarkCheckBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.cmbPic = new DarkUI.Controls.DarkComboBox();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.grpDetails = new DarkUI.Controls.DarkGroupBox();
            this.nudMinTier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMinEnemyTier = new System.Windows.Forms.Label();
            this.lblChallengeDescription = new System.Windows.Forms.Label();
            this.txtDescription = new DarkUI.Controls.DarkTextBox();
            this.nudParam = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbParamSelector = new DarkUI.Controls.DarkComboBox();
            this.nudSets = new DarkUI.Controls.DarkNumericUpDown();
            this.nudReps = new DarkUI.Controls.DarkNumericUpDown();
            this.lblParam = new System.Windows.Forms.Label();
            this.lblSets = new System.Windows.Forms.Label();
            this.lblReps = new System.Windows.Forms.Label();
            this.cmbChallengeType = new DarkUI.Controls.DarkComboBox();
            this.lblChallengeType = new System.Windows.Forms.Label();
            this.grpUnlocks = new DarkUI.Controls.DarkGroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudBonusEffects = new DarkUI.Controls.DarkNumericUpDown();
            this.nudVitals = new DarkUI.Controls.DarkNumericUpDown();
            this.lblValue = new System.Windows.Forms.Label();
            this.nudStats = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBonusEffects = new System.Windows.Forms.Label();
            this.lstBonuses = new System.Windows.Forms.ListBox();
            this.lstVitals = new System.Windows.Forms.ListBox();
            this.lblVitalBonus = new System.Windows.Forms.Label();
            this.lstStats = new System.Windows.Forms.ListBox();
            this.lblStatBonuses = new System.Windows.Forms.Label();
            this.cmbEnhancement = new DarkUI.Controls.DarkComboBox();
            this.lblEnhancement = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartDesc = new DarkUI.Controls.DarkTextBox();
            this.cmbEvent = new DarkUI.Controls.DarkComboBox();
            this.lblEvent = new System.Windows.Forms.Label();
            this.cmbSpell = new DarkUI.Controls.DarkComboBox();
            this.lblSpell = new System.Windows.Forms.Label();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.lblParamLbl = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.grpWeaponTypes.SuspendLayout();
            this.grpEditor.SuspendLayout();
            this.grpContractInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReps)).BeginInit();
            this.grpUnlocks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusEffects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVitals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStats)).BeginInit();
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
            this.toolStrip.Size = new System.Drawing.Size(838, 25);
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
            this.toolStripItemNew.Click += new System.EventHandler(this.toolStripItemNew_Click_1);
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
            // grpWeaponTypes
            // 
            this.grpWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponTypes.Controls.Add(this.btnClearSearch);
            this.grpWeaponTypes.Controls.Add(this.txtSearch);
            this.grpWeaponTypes.Controls.Add(this.lstGameObjects);
            this.grpWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponTypes.Location = new System.Drawing.Point(12, 28);
            this.grpWeaponTypes.Name = "grpWeaponTypes";
            this.grpWeaponTypes.Size = new System.Drawing.Size(203, 675);
            this.grpWeaponTypes.TabIndex = 53;
            this.grpWeaponTypes.TabStop = false;
            this.grpWeaponTypes.Text = "Challenges";
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
            this.lstGameObjects.Size = new System.Drawing.Size(191, 623);
            this.lstGameObjects.TabIndex = 32;
            // 
            // grpEditor
            // 
            this.grpEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEditor.Controls.Add(this.grpContractInfo);
            this.grpEditor.Controls.Add(this.lblIcon);
            this.grpEditor.Controls.Add(this.cmbPic);
            this.grpEditor.Controls.Add(this.picItem);
            this.grpEditor.Controls.Add(this.grpDetails);
            this.grpEditor.Controls.Add(this.grpUnlocks);
            this.grpEditor.Controls.Add(this.btnAddFolder);
            this.grpEditor.Controls.Add(this.cmbFolder);
            this.grpEditor.Controls.Add(this.lblFolder);
            this.grpEditor.Controls.Add(this.txtName);
            this.grpEditor.Controls.Add(this.lblName);
            this.grpEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEditor.Location = new System.Drawing.Point(221, 28);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(578, 675);
            this.grpEditor.TabIndex = 55;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Weapon Type";
            // 
            // grpContractInfo
            // 
            this.grpContractInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpContractInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpContractInfo.Controls.Add(this.txtRequirementDescription);
            this.grpContractInfo.Controls.Add(this.lblRequirementDescription);
            this.grpContractInfo.Controls.Add(this.btnDynamicRequirements);
            this.grpContractInfo.Controls.Add(this.chkRequiresContract);
            this.grpContractInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpContractInfo.Location = new System.Drawing.Point(19, 261);
            this.grpContractInfo.Name = "grpContractInfo";
            this.grpContractInfo.Size = new System.Drawing.Size(518, 75);
            this.grpContractInfo.TabIndex = 122;
            this.grpContractInfo.TabStop = false;
            this.grpContractInfo.Text = "Contract";
            // 
            // txtRequirementDescription
            // 
            this.txtRequirementDescription.AcceptsReturn = true;
            this.txtRequirementDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txtRequirementDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRequirementDescription.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtRequirementDescription.Location = new System.Drawing.Point(248, 32);
            this.txtRequirementDescription.Multiline = true;
            this.txtRequirementDescription.Name = "txtRequirementDescription";
            this.txtRequirementDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRequirementDescription.Size = new System.Drawing.Size(254, 37);
            this.txtRequirementDescription.TabIndex = 122;
            this.txtRequirementDescription.TextChanged += new System.EventHandler(this.txtRequirementDescription_TextChanged);
            // 
            // lblRequirementDescription
            // 
            this.lblRequirementDescription.AutoSize = true;
            this.lblRequirementDescription.Location = new System.Drawing.Point(245, 16);
            this.lblRequirementDescription.Name = "lblRequirementDescription";
            this.lblRequirementDescription.Size = new System.Drawing.Size(123, 13);
            this.lblRequirementDescription.TabIndex = 120;
            this.lblRequirementDescription.Text = "Requirement Description";
            // 
            // btnDynamicRequirements
            // 
            this.btnDynamicRequirements.Location = new System.Drawing.Point(18, 42);
            this.btnDynamicRequirements.Name = "btnDynamicRequirements";
            this.btnDynamicRequirements.Padding = new System.Windows.Forms.Padding(5);
            this.btnDynamicRequirements.Size = new System.Drawing.Size(153, 23);
            this.btnDynamicRequirements.TabIndex = 62;
            this.btnDynamicRequirements.Text = "Contract Requirements";
            this.btnDynamicRequirements.Click += new System.EventHandler(this.btnDynamicRequirements_Click);
            // 
            // chkRequiresContract
            // 
            this.chkRequiresContract.AutoSize = true;
            this.chkRequiresContract.Location = new System.Drawing.Point(18, 19);
            this.chkRequiresContract.Name = "chkRequiresContract";
            this.chkRequiresContract.Size = new System.Drawing.Size(117, 17);
            this.chkRequiresContract.TabIndex = 61;
            this.chkRequiresContract.Text = "Requires Contract?";
            this.chkRequiresContract.CheckedChanged += new System.EventHandler(this.chkRequiresContract_CheckedChanged);
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new System.Drawing.Point(57, 41);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(28, 13);
            this.lblIcon.TabIndex = 60;
            this.lblIcon.Text = "Icon";
            // 
            // cmbPic
            // 
            this.cmbPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbPic.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPic.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbPic.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbPic.DrawDropdownHoverOutline = false;
            this.cmbPic.DrawFocusRectangle = false;
            this.cmbPic.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPic.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbPic.FormattingEnabled = true;
            this.cmbPic.Items.AddRange(new object[] {
            "None"});
            this.cmbPic.Location = new System.Drawing.Point(57, 60);
            this.cmbPic.Name = "cmbPic";
            this.cmbPic.Size = new System.Drawing.Size(216, 21);
            this.cmbPic.TabIndex = 59;
            this.cmbPic.Text = "None";
            this.cmbPic.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbPic.SelectedIndexChanged += new System.EventHandler(this.cmbPic_SelectedIndexChanged);
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.Black;
            this.picItem.Location = new System.Drawing.Point(19, 49);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(32, 32);
            this.picItem.TabIndex = 58;
            this.picItem.TabStop = false;
            // 
            // grpDetails
            // 
            this.grpDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDetails.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDetails.Controls.Add(this.lblParamLbl);
            this.grpDetails.Controls.Add(this.nudMinTier);
            this.grpDetails.Controls.Add(this.lblMinEnemyTier);
            this.grpDetails.Controls.Add(this.lblChallengeDescription);
            this.grpDetails.Controls.Add(this.txtDescription);
            this.grpDetails.Controls.Add(this.nudParam);
            this.grpDetails.Controls.Add(this.cmbParamSelector);
            this.grpDetails.Controls.Add(this.nudSets);
            this.grpDetails.Controls.Add(this.nudReps);
            this.grpDetails.Controls.Add(this.lblParam);
            this.grpDetails.Controls.Add(this.lblSets);
            this.grpDetails.Controls.Add(this.lblReps);
            this.grpDetails.Controls.Add(this.cmbChallengeType);
            this.grpDetails.Controls.Add(this.lblChallengeType);
            this.grpDetails.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDetails.Location = new System.Drawing.Point(19, 87);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(518, 168);
            this.grpDetails.TabIndex = 57;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Challenge Details";
            // 
            // nudMinTier
            // 
            this.nudMinTier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinTier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinTier.Location = new System.Drawing.Point(9, 135);
            this.nudMinTier.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudMinTier.Name = "nudMinTier";
            this.nudMinTier.Size = new System.Drawing.Size(89, 20);
            this.nudMinTier.TabIndex = 121;
            this.nudMinTier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMinTier.ValueChanged += new System.EventHandler(this.nudMinTier_ValueChanged);
            // 
            // lblMinEnemyTier
            // 
            this.lblMinEnemyTier.AutoSize = true;
            this.lblMinEnemyTier.Location = new System.Drawing.Point(6, 119);
            this.lblMinEnemyTier.Name = "lblMinEnemyTier";
            this.lblMinEnemyTier.Size = new System.Drawing.Size(104, 13);
            this.lblMinEnemyTier.TabIndex = 120;
            this.lblMinEnemyTier.Text = "Minimum Enemy Tier";
            // 
            // lblChallengeDescription
            // 
            this.lblChallengeDescription.AutoSize = true;
            this.lblChallengeDescription.Location = new System.Drawing.Point(6, 119);
            this.lblChallengeDescription.Name = "lblChallengeDescription";
            this.lblChallengeDescription.Size = new System.Drawing.Size(60, 13);
            this.lblChallengeDescription.TabIndex = 84;
            this.lblChallengeDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtDescription.Location = new System.Drawing.Point(6, 135);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(506, 27);
            this.txtDescription.TabIndex = 119;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // nudParam
            // 
            this.nudParam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudParam.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudParam.Location = new System.Drawing.Point(313, 91);
            this.nudParam.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudParam.Name = "nudParam";
            this.nudParam.Size = new System.Drawing.Size(89, 20);
            this.nudParam.TabIndex = 82;
            this.nudParam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudParam.ValueChanged += new System.EventHandler(this.nudParam_ValueChanged);
            // 
            // cmbParamSelector
            // 
            this.cmbParamSelector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbParamSelector.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbParamSelector.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbParamSelector.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbParamSelector.DrawDropdownHoverOutline = false;
            this.cmbParamSelector.DrawFocusRectangle = false;
            this.cmbParamSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbParamSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParamSelector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbParamSelector.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbParamSelector.FormattingEnabled = true;
            this.cmbParamSelector.Location = new System.Drawing.Point(313, 91);
            this.cmbParamSelector.Name = "cmbParamSelector";
            this.cmbParamSelector.Size = new System.Drawing.Size(199, 21);
            this.cmbParamSelector.TabIndex = 83;
            this.cmbParamSelector.Text = null;
            this.cmbParamSelector.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbParamSelector.SelectedIndexChanged += new System.EventHandler(this.cmbParamSelector_SelectedIndexChanged);
            // 
            // nudSets
            // 
            this.nudSets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSets.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSets.Location = new System.Drawing.Point(165, 91);
            this.nudSets.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudSets.Name = "nudSets";
            this.nudSets.Size = new System.Drawing.Size(89, 20);
            this.nudSets.TabIndex = 81;
            this.nudSets.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSets.ValueChanged += new System.EventHandler(this.nudSets_ValueChanged);
            // 
            // nudReps
            // 
            this.nudReps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudReps.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudReps.Location = new System.Drawing.Point(9, 91);
            this.nudReps.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudReps.Name = "nudReps";
            this.nudReps.Size = new System.Drawing.Size(89, 20);
            this.nudReps.TabIndex = 80;
            this.nudReps.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudReps.ValueChanged += new System.EventHandler(this.nudReps_ValueChanged);
            // 
            // lblParam
            // 
            this.lblParam.AutoSize = true;
            this.lblParam.Location = new System.Drawing.Point(310, 75);
            this.lblParam.Name = "lblParam";
            this.lblParam.Size = new System.Drawing.Size(37, 13);
            this.lblParam.TabIndex = 57;
            this.lblParam.Text = "Param";
            // 
            // lblSets
            // 
            this.lblSets.AutoSize = true;
            this.lblSets.Location = new System.Drawing.Point(162, 75);
            this.lblSets.Name = "lblSets";
            this.lblSets.Size = new System.Drawing.Size(28, 13);
            this.lblSets.TabIndex = 56;
            this.lblSets.Text = "Sets";
            // 
            // lblReps
            // 
            this.lblReps.AutoSize = true;
            this.lblReps.Location = new System.Drawing.Point(6, 75);
            this.lblReps.Name = "lblReps";
            this.lblReps.Size = new System.Drawing.Size(60, 13);
            this.lblReps.TabIndex = 55;
            this.lblReps.Text = "Repetitions";
            // 
            // cmbChallengeType
            // 
            this.cmbChallengeType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbChallengeType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbChallengeType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbChallengeType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbChallengeType.DrawDropdownHoverOutline = false;
            this.cmbChallengeType.DrawFocusRectangle = false;
            this.cmbChallengeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbChallengeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChallengeType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbChallengeType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbChallengeType.FormattingEnabled = true;
            this.cmbChallengeType.Location = new System.Drawing.Point(9, 41);
            this.cmbChallengeType.Name = "cmbChallengeType";
            this.cmbChallengeType.Size = new System.Drawing.Size(503, 21);
            this.cmbChallengeType.TabIndex = 54;
            this.cmbChallengeType.Text = null;
            this.cmbChallengeType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbChallengeType.SelectedIndexChanged += new System.EventHandler(this.cmbChallengeType_SelectedIndexChanged);
            // 
            // lblChallengeType
            // 
            this.lblChallengeType.AutoSize = true;
            this.lblChallengeType.Location = new System.Drawing.Point(6, 25);
            this.lblChallengeType.Name = "lblChallengeType";
            this.lblChallengeType.Size = new System.Drawing.Size(81, 13);
            this.lblChallengeType.TabIndex = 53;
            this.lblChallengeType.Text = "Challenge Type";
            // 
            // grpUnlocks
            // 
            this.grpUnlocks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpUnlocks.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpUnlocks.Controls.Add(this.label3);
            this.grpUnlocks.Controls.Add(this.label2);
            this.grpUnlocks.Controls.Add(this.nudBonusEffects);
            this.grpUnlocks.Controls.Add(this.nudVitals);
            this.grpUnlocks.Controls.Add(this.lblValue);
            this.grpUnlocks.Controls.Add(this.nudStats);
            this.grpUnlocks.Controls.Add(this.lblBonusEffects);
            this.grpUnlocks.Controls.Add(this.lstBonuses);
            this.grpUnlocks.Controls.Add(this.lstVitals);
            this.grpUnlocks.Controls.Add(this.lblVitalBonus);
            this.grpUnlocks.Controls.Add(this.lstStats);
            this.grpUnlocks.Controls.Add(this.lblStatBonuses);
            this.grpUnlocks.Controls.Add(this.cmbEnhancement);
            this.grpUnlocks.Controls.Add(this.lblEnhancement);
            this.grpUnlocks.Controls.Add(this.label1);
            this.grpUnlocks.Controls.Add(this.txtStartDesc);
            this.grpUnlocks.Controls.Add(this.cmbEvent);
            this.grpUnlocks.Controls.Add(this.lblEvent);
            this.grpUnlocks.Controls.Add(this.cmbSpell);
            this.grpUnlocks.Controls.Add(this.lblSpell);
            this.grpUnlocks.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpUnlocks.Location = new System.Drawing.Point(19, 342);
            this.grpUnlocks.Name = "grpUnlocks";
            this.grpUnlocks.Size = new System.Drawing.Size(543, 325);
            this.grpUnlocks.TabIndex = 56;
            this.grpUnlocks.TabStop = false;
            this.grpUnlocks.Text = "Unlocks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(392, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 133;
            this.label3.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 132;
            this.label2.Text = "Value";
            // 
            // nudBonusEffects
            // 
            this.nudBonusEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBonusEffects.Location = new System.Drawing.Point(436, 292);
            this.nudBonusEffects.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudBonusEffects.Name = "nudBonusEffects";
            this.nudBonusEffects.Size = new System.Drawing.Size(89, 20);
            this.nudBonusEffects.TabIndex = 131;
            this.nudBonusEffects.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBonusEffects.ValueChanged += new System.EventHandler(this.nudBonusEffects_ValueChanged);
            // 
            // nudVitals
            // 
            this.nudVitals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudVitals.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudVitals.Location = new System.Drawing.Point(261, 292);
            this.nudVitals.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudVitals.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.nudVitals.Name = "nudVitals";
            this.nudVitals.Size = new System.Drawing.Size(89, 20);
            this.nudVitals.TabIndex = 130;
            this.nudVitals.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudVitals.ValueChanged += new System.EventHandler(this.nudVitals_ValueChanged);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(39, 294);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 129;
            this.lblValue.Text = "Value";
            // 
            // nudStats
            // 
            this.nudStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStats.Location = new System.Drawing.Point(86, 292);
            this.nudStats.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudStats.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.nudStats.Name = "nudStats";
            this.nudStats.Size = new System.Drawing.Size(89, 20);
            this.nudStats.TabIndex = 128;
            this.nudStats.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStats.ValueChanged += new System.EventHandler(this.nudStats_ValueChanged);
            // 
            // lblBonusEffects
            // 
            this.lblBonusEffects.AutoSize = true;
            this.lblBonusEffects.Location = new System.Drawing.Point(353, 151);
            this.lblBonusEffects.Name = "lblBonusEffects";
            this.lblBonusEffects.Size = new System.Drawing.Size(73, 13);
            this.lblBonusEffects.TabIndex = 127;
            this.lblBonusEffects.Text = "Bonus Effects";
            // 
            // lstBonuses
            // 
            this.lstBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBonuses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBonuses.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBonuses.FormattingEnabled = true;
            this.lstBonuses.Location = new System.Drawing.Point(356, 167);
            this.lstBonuses.Name = "lstBonuses";
            this.lstBonuses.Size = new System.Drawing.Size(169, 119);
            this.lstBonuses.TabIndex = 126;
            this.lstBonuses.SelectedIndexChanged += new System.EventHandler(this.lstBonuses_SelectedIndexChanged);
            // 
            // lstVitals
            // 
            this.lstVitals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstVitals.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstVitals.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstVitals.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstVitals.FormattingEnabled = true;
            this.lstVitals.Location = new System.Drawing.Point(181, 167);
            this.lstVitals.Name = "lstVitals";
            this.lstVitals.Size = new System.Drawing.Size(169, 119);
            this.lstVitals.TabIndex = 125;
            this.lstVitals.SelectedIndexChanged += new System.EventHandler(this.lstVitals_SelectedIndexChanged);
            // 
            // lblVitalBonus
            // 
            this.lblVitalBonus.AutoSize = true;
            this.lblVitalBonus.Location = new System.Drawing.Point(178, 151);
            this.lblVitalBonus.Name = "lblVitalBonus";
            this.lblVitalBonus.Size = new System.Drawing.Size(71, 13);
            this.lblVitalBonus.TabIndex = 124;
            this.lblVitalBonus.Text = "Vital Bonuses";
            // 
            // lstStats
            // 
            this.lstStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstStats.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstStats.FormattingEnabled = true;
            this.lstStats.Location = new System.Drawing.Point(6, 167);
            this.lstStats.Name = "lstStats";
            this.lstStats.Size = new System.Drawing.Size(169, 119);
            this.lstStats.TabIndex = 123;
            this.lstStats.SelectedIndexChanged += new System.EventHandler(this.lstStats_SelectedIndexChanged);
            // 
            // lblStatBonuses
            // 
            this.lblStatBonuses.AutoSize = true;
            this.lblStatBonuses.Location = new System.Drawing.Point(3, 151);
            this.lblStatBonuses.Name = "lblStatBonuses";
            this.lblStatBonuses.Size = new System.Drawing.Size(70, 13);
            this.lblStatBonuses.TabIndex = 122;
            this.lblStatBonuses.Text = "Stat Bonuses";
            // 
            // cmbEnhancement
            // 
            this.cmbEnhancement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEnhancement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEnhancement.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEnhancement.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEnhancement.DrawDropdownHoverOutline = false;
            this.cmbEnhancement.DrawFocusRectangle = false;
            this.cmbEnhancement.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEnhancement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnhancement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEnhancement.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEnhancement.FormattingEnabled = true;
            this.cmbEnhancement.Location = new System.Drawing.Point(9, 72);
            this.cmbEnhancement.Name = "cmbEnhancement";
            this.cmbEnhancement.Size = new System.Drawing.Size(229, 21);
            this.cmbEnhancement.TabIndex = 121;
            this.cmbEnhancement.Text = null;
            this.cmbEnhancement.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEnhancement.SelectedIndexChanged += new System.EventHandler(this.cmbEnhancement_SelectedIndexChanged);
            // 
            // lblEnhancement
            // 
            this.lblEnhancement.AutoSize = true;
            this.lblEnhancement.Location = new System.Drawing.Point(6, 56);
            this.lblEnhancement.Name = "lblEnhancement";
            this.lblEnhancement.Size = new System.Drawing.Size(73, 13);
            this.lblEnhancement.TabIndex = 120;
            this.lblEnhancement.Text = "Enhancement";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Event Description";
            // 
            // txtStartDesc
            // 
            this.txtStartDesc.AcceptsReturn = true;
            this.txtStartDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txtStartDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartDesc.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtStartDesc.Location = new System.Drawing.Point(263, 72);
            this.txtStartDesc.Multiline = true;
            this.txtStartDesc.Name = "txtStartDesc";
            this.txtStartDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStartDesc.Size = new System.Drawing.Size(239, 56);
            this.txtStartDesc.TabIndex = 118;
            this.txtStartDesc.TextChanged += new System.EventHandler(this.txtStartDesc_TextChanged);
            // 
            // cmbEvent
            // 
            this.cmbEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEvent.DrawDropdownHoverOutline = false;
            this.cmbEvent.DrawFocusRectangle = false;
            this.cmbEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Location = new System.Drawing.Point(263, 32);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(239, 21);
            this.cmbEvent.TabIndex = 117;
            this.cmbEvent.Text = null;
            this.cmbEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
            // 
            // lblEvent
            // 
            this.lblEvent.AutoSize = true;
            this.lblEvent.Location = new System.Drawing.Point(260, 16);
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Size = new System.Drawing.Size(90, 13);
            this.lblEvent.TabIndex = 116;
            this.lblEvent.Text = "Completion Event";
            // 
            // cmbSpell
            // 
            this.cmbSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpell.DrawDropdownHoverOutline = false;
            this.cmbSpell.DrawFocusRectangle = false;
            this.cmbSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpell.FormattingEnabled = true;
            this.cmbSpell.Location = new System.Drawing.Point(9, 32);
            this.cmbSpell.Name = "cmbSpell";
            this.cmbSpell.Size = new System.Drawing.Size(229, 21);
            this.cmbSpell.TabIndex = 115;
            this.cmbSpell.Text = null;
            this.cmbSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSpell.SelectedIndexChanged += new System.EventHandler(this.cmbSpell_SelectedIndexChanged);
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(6, 16);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(30, 13);
            this.lblSpell.TabIndex = 112;
            this.lblSpell.Text = "Spell";
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
            this.txtName.Location = new System.Drawing.Point(57, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(216, 20);
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(519, 709);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(135, 27);
            this.btnSave.TabIndex = 56;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(660, 709);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(139, 27);
            this.btnCancel.TabIndex = 57;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblParamLbl
            // 
            this.lblParamLbl.AutoSize = true;
            this.lblParamLbl.Location = new System.Drawing.Point(408, 75);
            this.lblParamLbl.Name = "lblParamLbl";
            this.lblParamLbl.Size = new System.Drawing.Size(49, 13);
            this.lblParamLbl.TabIndex = 122;
            this.lblParamLbl.Text = "<Param>";
            // 
            // frmChallenge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(838, 751);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.grpWeaponTypes);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChallenge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Challenge Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpWeaponTypes.ResumeLayout(false);
            this.grpWeaponTypes.PerformLayout();
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            this.grpContractInfo.ResumeLayout(false);
            this.grpContractInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinTier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudParam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReps)).EndInit();
            this.grpUnlocks.ResumeLayout(false);
            this.grpUnlocks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusEffects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVitals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStats)).EndInit();
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
        private DarkUI.Controls.DarkGroupBox grpWeaponTypes;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkGroupBox grpEditor;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkTextBox txtName;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpUnlocks;
        private DarkUI.Controls.DarkGroupBox grpDetails;
        private System.Windows.Forms.Label lblParam;
        private System.Windows.Forms.Label lblSets;
        private System.Windows.Forms.Label lblReps;
        private DarkUI.Controls.DarkComboBox cmbChallengeType;
        private System.Windows.Forms.Label lblChallengeType;
        private DarkUI.Controls.DarkNumericUpDown nudParam;
        private DarkUI.Controls.DarkNumericUpDown nudSets;
        private DarkUI.Controls.DarkNumericUpDown nudReps;
        private System.Windows.Forms.Label lblSpell;
        private DarkUI.Controls.DarkComboBox cmbSpell;
        private DarkUI.Controls.DarkComboBox cmbEvent;
        private System.Windows.Forms.Label lblEvent;
        private System.Windows.Forms.Label label1;
        private DarkUI.Controls.DarkTextBox txtStartDesc;
        private DarkUI.Controls.DarkComboBox cmbParamSelector;
        private System.Windows.Forms.PictureBox picItem;
        private DarkUI.Controls.DarkComboBox cmbPic;
        private System.Windows.Forms.Label lblIcon;
        private DarkUI.Controls.DarkTextBox txtDescription;
        private System.Windows.Forms.Label lblChallengeDescription;
        private System.Windows.Forms.Label lblEnhancement;
        private DarkUI.Controls.DarkComboBox cmbEnhancement;
        private DarkUI.Controls.DarkGroupBox grpContractInfo;
        private DarkUI.Controls.DarkCheckBox chkRequiresContract;
        private DarkUI.Controls.DarkTextBox txtRequirementDescription;
        private System.Windows.Forms.Label lblRequirementDescription;
        private DarkUI.Controls.DarkButton btnDynamicRequirements;
        private DarkUI.Controls.DarkNumericUpDown nudMinTier;
        private System.Windows.Forms.Label lblMinEnemyTier;
        private System.Windows.Forms.Label lblStatBonuses;
        private System.Windows.Forms.Label lblBonusEffects;
        private System.Windows.Forms.ListBox lstBonuses;
        private System.Windows.Forms.ListBox lstVitals;
        private System.Windows.Forms.Label lblVitalBonus;
        private System.Windows.Forms.ListBox lstStats;
        private DarkUI.Controls.DarkNumericUpDown nudBonusEffects;
        private DarkUI.Controls.DarkNumericUpDown nudVitals;
        private System.Windows.Forms.Label lblValue;
        private DarkUI.Controls.DarkNumericUpDown nudStats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblParamLbl;
    }
}