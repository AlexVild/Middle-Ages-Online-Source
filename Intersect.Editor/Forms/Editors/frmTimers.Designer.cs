
namespace Intersect.Editor.Forms.Editors
{
    partial class frmTimers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTimers));
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
            this.grpTimers = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.pnlTimerSettings = new System.Windows.Forms.Panel();
            this.chkActionsEnabled = new DarkUI.Controls.DarkCheckBox();
            this.grpInstanceControl = new DarkUI.Controls.DarkGroupBox();
            this.rdoOnNth = new DarkUI.Controls.DarkRadioButton();
            this.rdoEveryNth = new DarkUI.Controls.DarkRadioButton();
            this.lblActionVal = new System.Windows.Forms.Label();
            this.lblActions = new System.Windows.Forms.Label();
            this.nudN = new DarkUI.Controls.DarkNumericUpDown();
            this.grpInstanceVarMod = new DarkUI.Controls.DarkGroupBox();
            this.lblVarVal = new System.Windows.Forms.Label();
            this.nudVarVal = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbVarAction = new DarkUI.Controls.DarkComboBox();
            this.lblInstanceVarAction = new System.Windows.Forms.Label();
            this.cmbInstanceVar = new DarkUI.Controls.DarkComboBox();
            this.lblInstanceVar = new System.Windows.Forms.Label();
            this.grpTimerOptions = new DarkUI.Controls.DarkGroupBox();
            this.grpMapExclusivity = new DarkUI.Controls.DarkGroupBox();
            this.lstExclusiveMaps = new System.Windows.Forms.ListBox();
            this.btnRemoveMap = new DarkUI.Controls.DarkButton();
            this.btnAddMap = new DarkUI.Controls.DarkButton();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMaps = new DarkUI.Controls.DarkComboBox();
            this.chkContinueOnInstanceChange = new DarkUI.Controls.DarkCheckBox();
            this.chkContinueOnDeath = new DarkUI.Controls.DarkCheckBox();
            this.chkStartWithServer = new DarkUI.Controls.DarkCheckBox();
            this.cmbVariable = new DarkUI.Controls.DarkComboBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.cmbLogoutBehavior = new DarkUI.Controls.DarkComboBox();
            this.lblLogoutBehavior = new System.Windows.Forms.Label();
            this.chkContinue = new DarkUI.Controls.DarkCheckBox();
            this.lblCompletionBehavior = new System.Windows.Forms.Label();
            this.cmbCompletionBehavior = new DarkUI.Controls.DarkComboBox();
            this.grpEvents = new DarkUI.Controls.DarkGroupBox();
            this.chkSinglePlayerCompletion = new DarkUI.Controls.DarkCheckBox();
            this.chkSinglePlayerExpire = new DarkUI.Controls.DarkCheckBox();
            this.chkSinglePlayerCancel = new DarkUI.Controls.DarkCheckBox();
            this.cmbCompletionEvent = new DarkUI.Controls.DarkComboBox();
            this.lblOnCompletion = new System.Windows.Forms.Label();
            this.cmbExpirationEvent = new DarkUI.Controls.DarkComboBox();
            this.lblExpiredEvent = new System.Windows.Forms.Label();
            this.cmbCancelledEvent = new DarkUI.Controls.DarkComboBox();
            this.lblCancelled = new System.Windows.Forms.Label();
            this.grpSettings = new DarkUI.Controls.DarkGroupBox();
            this.chkRunIndefinite = new DarkUI.Controls.DarkCheckBox();
            this.cmbTimerType = new DarkUI.Controls.DarkComboBox();
            this.lblTimerType = new System.Windows.Forms.Label();
            this.lblRepetitions = new System.Windows.Forms.Label();
            this.lblRepeat = new System.Windows.Forms.Label();
            this.cmbRepetitionType = new DarkUI.Controls.DarkComboBox();
            this.nudRepetitions = new DarkUI.Controls.DarkNumericUpDown();
            this.nudTimeLimit = new DarkUI.Controls.DarkNumericUpDown();
            this.lblTimeLimit = new System.Windows.Forms.Label();
            this.grpDisplay = new DarkUI.Controls.DarkGroupBox();
            this.chkHidden = new DarkUI.Controls.DarkCheckBox();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtDisplayName = new DarkUI.Controls.DarkTextBox();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.grpTypes = new DarkUI.Controls.DarkGroupBox();
            this.cmbOwnerType = new DarkUI.Controls.DarkComboBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.chkOnlyOnExclusiveMaps = new DarkUI.Controls.DarkCheckBox();
            this.toolStrip.SuspendLayout();
            this.grpTimers.SuspendLayout();
            this.pnlTimerSettings.SuspendLayout();
            this.grpInstanceControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudN)).BeginInit();
            this.grpInstanceVarMod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVarVal)).BeginInit();
            this.grpTimerOptions.SuspendLayout();
            this.grpMapExclusivity.SuspendLayout();
            this.grpEvents.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepetitions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeLimit)).BeginInit();
            this.grpDisplay.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.grpTypes.SuspendLayout();
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
            this.toolStrip.TabIndex = 45;
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
            // grpTimers
            // 
            this.grpTimers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTimers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTimers.Controls.Add(this.btnClearSearch);
            this.grpTimers.Controls.Add(this.txtSearch);
            this.grpTimers.Controls.Add(this.lstGameObjects);
            this.grpTimers.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTimers.Location = new System.Drawing.Point(18, 79);
            this.grpTimers.Name = "grpTimers";
            this.grpTimers.Size = new System.Drawing.Size(202, 464);
            this.grpTimers.TabIndex = 46;
            this.grpTimers.TabStop = false;
            this.grpTimers.Text = "Timers";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 19);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(17, 17);
            this.btnClearSearch.TabIndex = 19;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(6, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(166, 20);
            this.txtSearch.TabIndex = 18;
            this.txtSearch.Text = "Search...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
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
            this.lstGameObjects.Location = new System.Drawing.Point(6, 46);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(190, 412);
            this.lstGameObjects.TabIndex = 2;
            // 
            // pnlTimerSettings
            // 
            this.pnlTimerSettings.Controls.Add(this.chkActionsEnabled);
            this.pnlTimerSettings.Controls.Add(this.grpInstanceControl);
            this.pnlTimerSettings.Controls.Add(this.grpTimerOptions);
            this.pnlTimerSettings.Controls.Add(this.grpEvents);
            this.pnlTimerSettings.Controls.Add(this.grpSettings);
            this.pnlTimerSettings.Controls.Add(this.grpDisplay);
            this.pnlTimerSettings.Controls.Add(this.grpGeneral);
            this.pnlTimerSettings.Location = new System.Drawing.Point(229, 23);
            this.pnlTimerSettings.Name = "pnlTimerSettings";
            this.pnlTimerSettings.Size = new System.Drawing.Size(780, 572);
            this.pnlTimerSettings.TabIndex = 47;
            this.pnlTimerSettings.Visible = false;
            // 
            // chkActionsEnabled
            // 
            this.chkActionsEnabled.AutoSize = true;
            this.chkActionsEnabled.Location = new System.Drawing.Point(10, 366);
            this.chkActionsEnabled.Name = "chkActionsEnabled";
            this.chkActionsEnabled.Size = new System.Drawing.Size(186, 17);
            this.chkActionsEnabled.TabIndex = 107;
            this.chkActionsEnabled.Text = "Enable instance controller action?";
            this.chkActionsEnabled.CheckedChanged += new System.EventHandler(this.chkActionsEnabled_CheckedChanged);
            // 
            // grpInstanceControl
            // 
            this.grpInstanceControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpInstanceControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpInstanceControl.Controls.Add(this.rdoOnNth);
            this.grpInstanceControl.Controls.Add(this.rdoEveryNth);
            this.grpInstanceControl.Controls.Add(this.lblActionVal);
            this.grpInstanceControl.Controls.Add(this.lblActions);
            this.grpInstanceControl.Controls.Add(this.nudN);
            this.grpInstanceControl.Controls.Add(this.grpInstanceVarMod);
            this.grpInstanceControl.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInstanceControl.Location = new System.Drawing.Point(10, 386);
            this.grpInstanceControl.Name = "grpInstanceControl";
            this.grpInstanceControl.Size = new System.Drawing.Size(563, 174);
            this.grpInstanceControl.TabIndex = 101;
            this.grpInstanceControl.TabStop = false;
            this.grpInstanceControl.Text = "Instance Control";
            // 
            // rdoOnNth
            // 
            this.rdoOnNth.AutoSize = true;
            this.rdoOnNth.Location = new System.Drawing.Point(7, 58);
            this.rdoOnNth.Name = "rdoOnNth";
            this.rdoOnNth.Size = new System.Drawing.Size(77, 17);
            this.rdoOnNth.TabIndex = 130;
            this.rdoOnNth.Text = "On the Nth";
            this.rdoOnNth.CheckedChanged += new System.EventHandler(this.rdoOnNth_CheckedChanged);
            // 
            // rdoEveryNth
            // 
            this.rdoEveryNth.AutoSize = true;
            this.rdoEveryNth.Location = new System.Drawing.Point(7, 35);
            this.rdoEveryNth.Name = "rdoEveryNth";
            this.rdoEveryNth.Size = new System.Drawing.Size(72, 17);
            this.rdoEveryNth.TabIndex = 129;
            this.rdoEveryNth.Text = "Every Nth";
            this.rdoEveryNth.CheckedChanged += new System.EventHandler(this.rdoEveryNth_CheckedChanged);
            // 
            // lblActionVal
            // 
            this.lblActionVal.AutoSize = true;
            this.lblActionVal.Location = new System.Drawing.Point(4, 91);
            this.lblActionVal.Name = "lblActionVal";
            this.lblActionVal.Size = new System.Drawing.Size(57, 13);
            this.lblActionVal.TabIndex = 128;
            this.lblActionVal.Text = "Where n =";
            // 
            // lblActions
            // 
            this.lblActions.AutoSize = true;
            this.lblActions.Location = new System.Drawing.Point(6, 19);
            this.lblActions.Name = "lblActions";
            this.lblActions.Size = new System.Drawing.Size(89, 13);
            this.lblActions.TabIndex = 125;
            this.lblActions.Text = "Perform actions...";
            // 
            // nudN
            // 
            this.nudN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudN.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudN.Location = new System.Drawing.Point(67, 89);
            this.nudN.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudN.Name = "nudN";
            this.nudN.Size = new System.Drawing.Size(80, 20);
            this.nudN.TabIndex = 124;
            this.nudN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudN.ValueChanged += new System.EventHandler(this.nudN_ValueChanged);
            // 
            // grpInstanceVarMod
            // 
            this.grpInstanceVarMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpInstanceVarMod.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpInstanceVarMod.Controls.Add(this.lblVarVal);
            this.grpInstanceVarMod.Controls.Add(this.nudVarVal);
            this.grpInstanceVarMod.Controls.Add(this.cmbVarAction);
            this.grpInstanceVarMod.Controls.Add(this.lblInstanceVarAction);
            this.grpInstanceVarMod.Controls.Add(this.cmbInstanceVar);
            this.grpInstanceVarMod.Controls.Add(this.lblInstanceVar);
            this.grpInstanceVarMod.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInstanceVarMod.Location = new System.Drawing.Point(156, 19);
            this.grpInstanceVarMod.Name = "grpInstanceVarMod";
            this.grpInstanceVarMod.Size = new System.Drawing.Size(214, 136);
            this.grpInstanceVarMod.TabIndex = 123;
            this.grpInstanceVarMod.TabStop = false;
            this.grpInstanceVarMod.Text = "Instance Variable Mod";
            // 
            // lblVarVal
            // 
            this.lblVarVal.AutoSize = true;
            this.lblVarVal.Location = new System.Drawing.Point(7, 110);
            this.lblVarVal.Name = "lblVarVal";
            this.lblVarVal.Size = new System.Drawing.Size(34, 13);
            this.lblVarVal.TabIndex = 122;
            this.lblVarVal.Text = "Value";
            // 
            // nudVarVal
            // 
            this.nudVarVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudVarVal.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudVarVal.Location = new System.Drawing.Point(68, 108);
            this.nudVarVal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudVarVal.Minimum = new decimal(new int[] {
            1000000,
            0,
            0,
            -2147483648});
            this.nudVarVal.Name = "nudVarVal";
            this.nudVarVal.Size = new System.Drawing.Size(136, 20);
            this.nudVarVal.TabIndex = 121;
            this.nudVarVal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudVarVal.ValueChanged += new System.EventHandler(this.nudVarVal_ValueChanged);
            // 
            // cmbVarAction
            // 
            this.cmbVarAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVarAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVarAction.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVarAction.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVarAction.DrawDropdownHoverOutline = false;
            this.cmbVarAction.DrawFocusRectangle = false;
            this.cmbVarAction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVarAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVarAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVarAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVarAction.FormattingEnabled = true;
            this.cmbVarAction.Location = new System.Drawing.Point(7, 72);
            this.cmbVarAction.Name = "cmbVarAction";
            this.cmbVarAction.Size = new System.Drawing.Size(197, 21);
            this.cmbVarAction.TabIndex = 120;
            this.cmbVarAction.Text = null;
            this.cmbVarAction.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVarAction.SelectedIndexChanged += new System.EventHandler(this.cmbVarAction_SelectedIndexChanged);
            // 
            // lblInstanceVarAction
            // 
            this.lblInstanceVarAction.AutoSize = true;
            this.lblInstanceVarAction.Location = new System.Drawing.Point(7, 56);
            this.lblInstanceVarAction.Name = "lblInstanceVarAction";
            this.lblInstanceVarAction.Size = new System.Drawing.Size(37, 13);
            this.lblInstanceVarAction.TabIndex = 119;
            this.lblInstanceVarAction.Text = "Action";
            // 
            // cmbInstanceVar
            // 
            this.cmbInstanceVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbInstanceVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbInstanceVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbInstanceVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbInstanceVar.DrawDropdownHoverOutline = false;
            this.cmbInstanceVar.DrawFocusRectangle = false;
            this.cmbInstanceVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInstanceVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInstanceVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbInstanceVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbInstanceVar.FormattingEnabled = true;
            this.cmbInstanceVar.Location = new System.Drawing.Point(7, 32);
            this.cmbInstanceVar.Name = "cmbInstanceVar";
            this.cmbInstanceVar.Size = new System.Drawing.Size(197, 21);
            this.cmbInstanceVar.TabIndex = 118;
            this.cmbInstanceVar.Text = null;
            this.cmbInstanceVar.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbInstanceVar.SelectedIndexChanged += new System.EventHandler(this.cmbInstanceVar_SelectedIndexChanged);
            // 
            // lblInstanceVar
            // 
            this.lblInstanceVar.AutoSize = true;
            this.lblInstanceVar.Location = new System.Drawing.Point(7, 16);
            this.lblInstanceVar.Name = "lblInstanceVar";
            this.lblInstanceVar.Size = new System.Drawing.Size(89, 13);
            this.lblInstanceVar.TabIndex = 117;
            this.lblInstanceVar.Text = "Instance Variable";
            // 
            // grpTimerOptions
            // 
            this.grpTimerOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTimerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTimerOptions.Controls.Add(this.grpMapExclusivity);
            this.grpTimerOptions.Controls.Add(this.chkContinueOnInstanceChange);
            this.grpTimerOptions.Controls.Add(this.chkContinueOnDeath);
            this.grpTimerOptions.Controls.Add(this.chkStartWithServer);
            this.grpTimerOptions.Controls.Add(this.cmbVariable);
            this.grpTimerOptions.Controls.Add(this.lblVariable);
            this.grpTimerOptions.Controls.Add(this.cmbLogoutBehavior);
            this.grpTimerOptions.Controls.Add(this.lblLogoutBehavior);
            this.grpTimerOptions.Controls.Add(this.chkContinue);
            this.grpTimerOptions.Controls.Add(this.lblCompletionBehavior);
            this.grpTimerOptions.Controls.Add(this.cmbCompletionBehavior);
            this.grpTimerOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTimerOptions.Location = new System.Drawing.Point(299, 159);
            this.grpTimerOptions.Name = "grpTimerOptions";
            this.grpTimerOptions.Size = new System.Drawing.Size(478, 223);
            this.grpTimerOptions.TabIndex = 106;
            this.grpTimerOptions.TabStop = false;
            this.grpTimerOptions.Text = "Timer Options";
            // 
            // grpMapExclusivity
            // 
            this.grpMapExclusivity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpMapExclusivity.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpMapExclusivity.Controls.Add(this.lstExclusiveMaps);
            this.grpMapExclusivity.Controls.Add(this.btnRemoveMap);
            this.grpMapExclusivity.Controls.Add(this.btnAddMap);
            this.grpMapExclusivity.Controls.Add(this.lblMap);
            this.grpMapExclusivity.Controls.Add(this.cmbMaps);
            this.grpMapExclusivity.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpMapExclusivity.Location = new System.Drawing.Point(271, 15);
            this.grpMapExclusivity.Name = "grpMapExclusivity";
            this.grpMapExclusivity.Size = new System.Drawing.Size(201, 202);
            this.grpMapExclusivity.TabIndex = 101;
            this.grpMapExclusivity.TabStop = false;
            this.grpMapExclusivity.Text = "Exclusive to Maps...";
            // 
            // lstExclusiveMaps
            // 
            this.lstExclusiveMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstExclusiveMaps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstExclusiveMaps.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstExclusiveMaps.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstExclusiveMaps.FormattingEnabled = true;
            this.lstExclusiveMaps.Location = new System.Drawing.Point(6, 19);
            this.lstExclusiveMaps.Name = "lstExclusiveMaps";
            this.lstExclusiveMaps.Size = new System.Drawing.Size(189, 93);
            this.lstExclusiveMaps.TabIndex = 116;
            // 
            // btnRemoveMap
            // 
            this.btnRemoveMap.Location = new System.Drawing.Point(111, 124);
            this.btnRemoveMap.Name = "btnRemoveMap";
            this.btnRemoveMap.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveMap.Size = new System.Drawing.Size(84, 27);
            this.btnRemoveMap.TabIndex = 115;
            this.btnRemoveMap.Text = "Remove";
            this.btnRemoveMap.Click += new System.EventHandler(this.btnRemoveMap_Click);
            // 
            // btnAddMap
            // 
            this.btnAddMap.Location = new System.Drawing.Point(6, 124);
            this.btnAddMap.Name = "btnAddMap";
            this.btnAddMap.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddMap.Size = new System.Drawing.Size(85, 27);
            this.btnAddMap.TabIndex = 114;
            this.btnAddMap.Text = "Add";
            this.btnAddMap.Click += new System.EventHandler(this.btnAddMap_Click);
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(6, 155);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(28, 13);
            this.lblMap.TabIndex = 113;
            this.lblMap.Text = "Map";
            // 
            // cmbMaps
            // 
            this.cmbMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMaps.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMaps.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMaps.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMaps.DrawDropdownHoverOutline = false;
            this.cmbMaps.DrawFocusRectangle = false;
            this.cmbMaps.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMaps.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMaps.FormattingEnabled = true;
            this.cmbMaps.Location = new System.Drawing.Point(6, 175);
            this.cmbMaps.Name = "cmbMaps";
            this.cmbMaps.Size = new System.Drawing.Size(189, 21);
            this.cmbMaps.TabIndex = 108;
            this.cmbMaps.Text = null;
            this.cmbMaps.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // chkContinueOnInstanceChange
            // 
            this.chkContinueOnInstanceChange.AutoSize = true;
            this.chkContinueOnInstanceChange.Location = new System.Drawing.Point(6, 88);
            this.chkContinueOnInstanceChange.Name = "chkContinueOnInstanceChange";
            this.chkContinueOnInstanceChange.Size = new System.Drawing.Size(171, 17);
            this.chkContinueOnInstanceChange.TabIndex = 116;
            this.chkContinueOnInstanceChange.Text = "Continue on instance change?";
            this.chkContinueOnInstanceChange.CheckedChanged += new System.EventHandler(this.chkContinueOnInstanceChange_CheckedChanged);
            // 
            // chkContinueOnDeath
            // 
            this.chkContinueOnDeath.AutoSize = true;
            this.chkContinueOnDeath.Location = new System.Drawing.Point(6, 65);
            this.chkContinueOnDeath.Name = "chkContinueOnDeath";
            this.chkContinueOnDeath.Size = new System.Drawing.Size(119, 17);
            this.chkContinueOnDeath.TabIndex = 115;
            this.chkContinueOnDeath.Text = "Continue on death?";
            this.chkContinueOnDeath.CheckedChanged += new System.EventHandler(this.chkContinueOnDeath_CheckedChanged);
            // 
            // chkStartWithServer
            // 
            this.chkStartWithServer.AutoSize = true;
            this.chkStartWithServer.Location = new System.Drawing.Point(6, 42);
            this.chkStartWithServer.Name = "chkStartWithServer";
            this.chkStartWithServer.Size = new System.Drawing.Size(108, 17);
            this.chkStartWithServer.TabIndex = 114;
            this.chkStartWithServer.Text = "Start with server?";
            this.chkStartWithServer.CheckedChanged += new System.EventHandler(this.chkStartWithServer_CheckedChanged);
            // 
            // cmbVariable
            // 
            this.cmbVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVariable.DrawDropdownHoverOutline = false;
            this.cmbVariable.DrawFocusRectangle = false;
            this.cmbVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVariable.FormattingEnabled = true;
            this.cmbVariable.Location = new System.Drawing.Point(9, 198);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(256, 21);
            this.cmbVariable.TabIndex = 113;
            this.cmbVariable.Text = null;
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariable.SelectedIndexChanged += new System.EventHandler(this.cmbVariable_SelectedIndexChanged);
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(6, 179);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(174, 13);
            this.lblVariable.TabIndex = 112;
            this.lblVariable.Text = "Store Elapsed Time (ms) In Variable";
            // 
            // cmbLogoutBehavior
            // 
            this.cmbLogoutBehavior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbLogoutBehavior.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbLogoutBehavior.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbLogoutBehavior.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbLogoutBehavior.DrawDropdownHoverOutline = false;
            this.cmbLogoutBehavior.DrawFocusRectangle = false;
            this.cmbLogoutBehavior.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLogoutBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogoutBehavior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLogoutBehavior.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbLogoutBehavior.FormattingEnabled = true;
            this.cmbLogoutBehavior.Location = new System.Drawing.Point(117, 117);
            this.cmbLogoutBehavior.Name = "cmbLogoutBehavior";
            this.cmbLogoutBehavior.Size = new System.Drawing.Size(148, 21);
            this.cmbLogoutBehavior.TabIndex = 111;
            this.cmbLogoutBehavior.Text = null;
            this.cmbLogoutBehavior.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbLogoutBehavior.SelectedIndexChanged += new System.EventHandler(this.cmbLogoutBehavior_SelectedIndexChanged);
            // 
            // lblLogoutBehavior
            // 
            this.lblLogoutBehavior.AutoSize = true;
            this.lblLogoutBehavior.Location = new System.Drawing.Point(6, 120);
            this.lblLogoutBehavior.Name = "lblLogoutBehavior";
            this.lblLogoutBehavior.Size = new System.Drawing.Size(85, 13);
            this.lblLogoutBehavior.TabIndex = 110;
            this.lblLogoutBehavior.Text = "Logout Behavior";
            // 
            // chkContinue
            // 
            this.chkContinue.AutoSize = true;
            this.chkContinue.Location = new System.Drawing.Point(6, 19);
            this.chkContinue.Name = "chkContinue";
            this.chkContinue.Size = new System.Drawing.Size(146, 17);
            this.chkContinue.TabIndex = 109;
            this.chkContinue.Text = "Continue after expiration?";
            this.chkContinue.CheckedChanged += new System.EventHandler(this.chkContinue_CheckedChanged);
            // 
            // lblCompletionBehavior
            // 
            this.lblCompletionBehavior.AutoSize = true;
            this.lblCompletionBehavior.Location = new System.Drawing.Point(6, 153);
            this.lblCompletionBehavior.Name = "lblCompletionBehavior";
            this.lblCompletionBehavior.Size = new System.Drawing.Size(104, 13);
            this.lblCompletionBehavior.TabIndex = 108;
            this.lblCompletionBehavior.Text = "Completion Behavior";
            // 
            // cmbCompletionBehavior
            // 
            this.cmbCompletionBehavior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCompletionBehavior.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCompletionBehavior.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCompletionBehavior.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCompletionBehavior.DrawDropdownHoverOutline = false;
            this.cmbCompletionBehavior.DrawFocusRectangle = false;
            this.cmbCompletionBehavior.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompletionBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompletionBehavior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompletionBehavior.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCompletionBehavior.FormattingEnabled = true;
            this.cmbCompletionBehavior.Location = new System.Drawing.Point(117, 150);
            this.cmbCompletionBehavior.Name = "cmbCompletionBehavior";
            this.cmbCompletionBehavior.Size = new System.Drawing.Size(148, 21);
            this.cmbCompletionBehavior.TabIndex = 108;
            this.cmbCompletionBehavior.Text = null;
            this.cmbCompletionBehavior.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCompletionBehavior.SelectedIndexChanged += new System.EventHandler(this.cmbCompletionBehavior_SelectedIndexChanged);
            // 
            // grpEvents
            // 
            this.grpEvents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEvents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEvents.Controls.Add(this.chkSinglePlayerCompletion);
            this.grpEvents.Controls.Add(this.chkSinglePlayerExpire);
            this.grpEvents.Controls.Add(this.chkSinglePlayerCancel);
            this.grpEvents.Controls.Add(this.cmbCompletionEvent);
            this.grpEvents.Controls.Add(this.lblOnCompletion);
            this.grpEvents.Controls.Add(this.cmbExpirationEvent);
            this.grpEvents.Controls.Add(this.lblExpiredEvent);
            this.grpEvents.Controls.Add(this.cmbCancelledEvent);
            this.grpEvents.Controls.Add(this.lblCancelled);
            this.grpEvents.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEvents.Location = new System.Drawing.Point(299, 7);
            this.grpEvents.Name = "grpEvents";
            this.grpEvents.Size = new System.Drawing.Size(478, 143);
            this.grpEvents.TabIndex = 101;
            this.grpEvents.TabStop = false;
            this.grpEvents.Text = "Events";
            // 
            // chkSinglePlayerCompletion
            // 
            this.chkSinglePlayerCompletion.AutoSize = true;
            this.chkSinglePlayerCompletion.Location = new System.Drawing.Point(269, 113);
            this.chkSinglePlayerCompletion.Name = "chkSinglePlayerCompletion";
            this.chkSinglePlayerCompletion.Size = new System.Drawing.Size(134, 17);
            this.chkSinglePlayerCompletion.TabIndex = 112;
            this.chkSinglePlayerCompletion.Text = "Fire on a single player?";
            this.chkSinglePlayerCompletion.CheckedChanged += new System.EventHandler(this.chkSinglePlayerCompletion_CheckedChanged);
            // 
            // chkSinglePlayerExpire
            // 
            this.chkSinglePlayerExpire.AutoSize = true;
            this.chkSinglePlayerExpire.Location = new System.Drawing.Point(269, 74);
            this.chkSinglePlayerExpire.Name = "chkSinglePlayerExpire";
            this.chkSinglePlayerExpire.Size = new System.Drawing.Size(134, 17);
            this.chkSinglePlayerExpire.TabIndex = 111;
            this.chkSinglePlayerExpire.Text = "Fire on a single player?";
            this.chkSinglePlayerExpire.CheckedChanged += new System.EventHandler(this.chkSinglePlayerExpire_CheckedChanged);
            // 
            // chkSinglePlayerCancel
            // 
            this.chkSinglePlayerCancel.AutoSize = true;
            this.chkSinglePlayerCancel.Location = new System.Drawing.Point(269, 36);
            this.chkSinglePlayerCancel.Name = "chkSinglePlayerCancel";
            this.chkSinglePlayerCancel.Size = new System.Drawing.Size(134, 17);
            this.chkSinglePlayerCancel.TabIndex = 110;
            this.chkSinglePlayerCancel.Text = "Fire on a single player?";
            this.chkSinglePlayerCancel.CheckedChanged += new System.EventHandler(this.chkSinglePlayerCancel_CheckedChanged);
            // 
            // cmbCompletionEvent
            // 
            this.cmbCompletionEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCompletionEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCompletionEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCompletionEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCompletionEvent.DrawDropdownHoverOutline = false;
            this.cmbCompletionEvent.DrawFocusRectangle = false;
            this.cmbCompletionEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCompletionEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompletionEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompletionEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCompletionEvent.FormattingEnabled = true;
            this.cmbCompletionEvent.Location = new System.Drawing.Point(6, 112);
            this.cmbCompletionEvent.Name = "cmbCompletionEvent";
            this.cmbCompletionEvent.Size = new System.Drawing.Size(257, 21);
            this.cmbCompletionEvent.TabIndex = 107;
            this.cmbCompletionEvent.Text = null;
            this.cmbCompletionEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCompletionEvent.SelectedIndexChanged += new System.EventHandler(this.cmbCompletionEvent_SelectedIndexChanged);
            // 
            // lblOnCompletion
            // 
            this.lblOnCompletion.AutoSize = true;
            this.lblOnCompletion.Location = new System.Drawing.Point(5, 96);
            this.lblOnCompletion.Name = "lblOnCompletion";
            this.lblOnCompletion.Size = new System.Drawing.Size(76, 13);
            this.lblOnCompletion.TabIndex = 106;
            this.lblOnCompletion.Text = "On Completion";
            // 
            // cmbExpirationEvent
            // 
            this.cmbExpirationEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbExpirationEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbExpirationEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbExpirationEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbExpirationEvent.DrawDropdownHoverOutline = false;
            this.cmbExpirationEvent.DrawFocusRectangle = false;
            this.cmbExpirationEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbExpirationEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpirationEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbExpirationEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbExpirationEvent.FormattingEnabled = true;
            this.cmbExpirationEvent.Location = new System.Drawing.Point(6, 72);
            this.cmbExpirationEvent.Name = "cmbExpirationEvent";
            this.cmbExpirationEvent.Size = new System.Drawing.Size(257, 21);
            this.cmbExpirationEvent.TabIndex = 105;
            this.cmbExpirationEvent.Text = null;
            this.cmbExpirationEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbExpirationEvent.SelectedIndexChanged += new System.EventHandler(this.cmbExpirationEvent_SelectedIndexChanged);
            // 
            // lblExpiredEvent
            // 
            this.lblExpiredEvent.AutoSize = true;
            this.lblExpiredEvent.Location = new System.Drawing.Point(5, 56);
            this.lblExpiredEvent.Name = "lblExpiredEvent";
            this.lblExpiredEvent.Size = new System.Drawing.Size(59, 13);
            this.lblExpiredEvent.TabIndex = 104;
            this.lblExpiredEvent.Text = "On Expired";
            // 
            // cmbCancelledEvent
            // 
            this.cmbCancelledEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCancelledEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCancelledEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCancelledEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCancelledEvent.DrawDropdownHoverOutline = false;
            this.cmbCancelledEvent.DrawFocusRectangle = false;
            this.cmbCancelledEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCancelledEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCancelledEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCancelledEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCancelledEvent.FormattingEnabled = true;
            this.cmbCancelledEvent.Location = new System.Drawing.Point(6, 32);
            this.cmbCancelledEvent.Name = "cmbCancelledEvent";
            this.cmbCancelledEvent.Size = new System.Drawing.Size(257, 21);
            this.cmbCancelledEvent.TabIndex = 103;
            this.cmbCancelledEvent.Text = null;
            this.cmbCancelledEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCancelledEvent.SelectedIndexChanged += new System.EventHandler(this.cmbCancelledEvent_SelectedIndexChanged);
            // 
            // lblCancelled
            // 
            this.lblCancelled.AutoSize = true;
            this.lblCancelled.Location = new System.Drawing.Point(5, 16);
            this.lblCancelled.Name = "lblCancelled";
            this.lblCancelled.Size = new System.Drawing.Size(71, 13);
            this.lblCancelled.TabIndex = 19;
            this.lblCancelled.Text = "On Cancelled";
            // 
            // grpSettings
            // 
            this.grpSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSettings.Controls.Add(this.chkRunIndefinite);
            this.grpSettings.Controls.Add(this.cmbTimerType);
            this.grpSettings.Controls.Add(this.lblTimerType);
            this.grpSettings.Controls.Add(this.lblRepetitions);
            this.grpSettings.Controls.Add(this.lblRepeat);
            this.grpSettings.Controls.Add(this.cmbRepetitionType);
            this.grpSettings.Controls.Add(this.nudRepetitions);
            this.grpSettings.Controls.Add(this.nudTimeLimit);
            this.grpSettings.Controls.Add(this.lblTimeLimit);
            this.grpSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSettings.Location = new System.Drawing.Point(10, 89);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(274, 184);
            this.grpSettings.TabIndex = 101;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            // 
            // chkRunIndefinite
            // 
            this.chkRunIndefinite.AutoSize = true;
            this.chkRunIndefinite.Location = new System.Drawing.Point(159, 94);
            this.chkRunIndefinite.Name = "chkRunIndefinite";
            this.chkRunIndefinite.Size = new System.Drawing.Size(104, 17);
            this.chkRunIndefinite.TabIndex = 109;
            this.chkRunIndefinite.Text = "Run indefinitely?";
            this.chkRunIndefinite.CheckedChanged += new System.EventHandler(this.chkRunIndefinite_CheckedChanged);
            // 
            // cmbTimerType
            // 
            this.cmbTimerType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTimerType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTimerType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTimerType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTimerType.DrawDropdownHoverOutline = false;
            this.cmbTimerType.DrawFocusRectangle = false;
            this.cmbTimerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTimerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimerType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTimerType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTimerType.FormattingEnabled = true;
            this.cmbTimerType.Location = new System.Drawing.Point(123, 29);
            this.cmbTimerType.Name = "cmbTimerType";
            this.cmbTimerType.Size = new System.Drawing.Size(140, 21);
            this.cmbTimerType.TabIndex = 108;
            this.cmbTimerType.Text = null;
            this.cmbTimerType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimerType.SelectedIndexChanged += new System.EventHandler(this.cmbTimerType_SelectedIndexChanged);
            // 
            // lblTimerType
            // 
            this.lblTimerType.AutoSize = true;
            this.lblTimerType.Location = new System.Drawing.Point(7, 32);
            this.lblTimerType.Name = "lblTimerType";
            this.lblTimerType.Size = new System.Drawing.Size(31, 13);
            this.lblTimerType.TabIndex = 107;
            this.lblTimerType.Text = "Type";
            // 
            // lblRepetitions
            // 
            this.lblRepetitions.AutoSize = true;
            this.lblRepetitions.Location = new System.Drawing.Point(7, 148);
            this.lblRepetitions.Name = "lblRepetitions";
            this.lblRepetitions.Size = new System.Drawing.Size(60, 13);
            this.lblRepetitions.TabIndex = 106;
            this.lblRepetitions.Text = "Repetitions";
            // 
            // lblRepeat
            // 
            this.lblRepeat.AutoSize = true;
            this.lblRepeat.Location = new System.Drawing.Point(7, 120);
            this.lblRepeat.Name = "lblRepeat";
            this.lblRepeat.Size = new System.Drawing.Size(48, 13);
            this.lblRepeat.TabIndex = 105;
            this.lblRepeat.Text = "Repeat?";
            // 
            // cmbRepetitionType
            // 
            this.cmbRepetitionType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbRepetitionType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbRepetitionType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbRepetitionType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbRepetitionType.DrawDropdownHoverOutline = false;
            this.cmbRepetitionType.DrawFocusRectangle = false;
            this.cmbRepetitionType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRepetitionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRepetitionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRepetitionType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbRepetitionType.FormattingEnabled = true;
            this.cmbRepetitionType.Location = new System.Drawing.Point(124, 117);
            this.cmbRepetitionType.Name = "cmbRepetitionType";
            this.cmbRepetitionType.Size = new System.Drawing.Size(140, 21);
            this.cmbRepetitionType.TabIndex = 24;
            this.cmbRepetitionType.Text = null;
            this.cmbRepetitionType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbRepetitionType.SelectedIndexChanged += new System.EventHandler(this.cmbRepetitionType_SelectedIndexChanged);
            // 
            // nudRepetitions
            // 
            this.nudRepetitions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRepetitions.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRepetitions.Location = new System.Drawing.Point(124, 146);
            this.nudRepetitions.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudRepetitions.Name = "nudRepetitions";
            this.nudRepetitions.Size = new System.Drawing.Size(140, 20);
            this.nudRepetitions.TabIndex = 104;
            this.nudRepetitions.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRepetitions.ValueChanged += new System.EventHandler(this.nudRepetitions_ValueChanged);
            // 
            // nudTimeLimit
            // 
            this.nudTimeLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTimeLimit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTimeLimit.Location = new System.Drawing.Point(123, 68);
            this.nudTimeLimit.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTimeLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeLimit.Name = "nudTimeLimit";
            this.nudTimeLimit.Size = new System.Drawing.Size(140, 20);
            this.nudTimeLimit.TabIndex = 102;
            this.nudTimeLimit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeLimit.ValueChanged += new System.EventHandler(this.nudTimeLimit_ValueChanged);
            // 
            // lblTimeLimit
            // 
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Location = new System.Drawing.Point(7, 70);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(103, 13);
            this.lblTimeLimit.TabIndex = 101;
            this.lblTimeLimit.Text = "Time Limit (seconds)";
            // 
            // grpDisplay
            // 
            this.grpDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDisplay.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDisplay.Controls.Add(this.chkOnlyOnExclusiveMaps);
            this.grpDisplay.Controls.Add(this.chkHidden);
            this.grpDisplay.Controls.Add(this.lblDisplayName);
            this.grpDisplay.Controls.Add(this.txtDisplayName);
            this.grpDisplay.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDisplay.Location = new System.Drawing.Point(10, 279);
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.Size = new System.Drawing.Size(274, 81);
            this.grpDisplay.TabIndex = 37;
            this.grpDisplay.TabStop = false;
            this.grpDisplay.Text = "In-Game Display";
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Location = new System.Drawing.Point(8, 19);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(66, 17);
            this.chkHidden.TabIndex = 100;
            this.chkHidden.Text = "Hidden?";
            this.chkHidden.CheckedChanged += new System.EventHandler(this.chkHidden_CheckedChanged);
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDisplayName.Location = new System.Drawing.Point(5, 50);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(72, 13);
            this.lblDisplayName.TabIndex = 19;
            this.lblDisplayName.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDisplayName.Location = new System.Drawing.Point(86, 48);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(177, 20);
            this.txtDisplayName.TabIndex = 18;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(10, 7);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(274, 76);
            this.grpGeneral.TabIndex = 36;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(245, 44);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 23;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(4, 48);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(36, 13);
            this.lblFolder.TabIndex = 22;
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
            this.cmbFolder.Location = new System.Drawing.Point(57, 44);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(182, 21);
            this.cmbFolder.TabIndex = 21;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(4, 20);
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
            this.txtName.Location = new System.Drawing.Point(57, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(206, 20);
            this.txtName.TabIndex = 18;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // grpTypes
            // 
            this.grpTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTypes.Controls.Add(this.cmbOwnerType);
            this.grpTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTypes.Location = new System.Drawing.Point(18, 28);
            this.grpTypes.Name = "grpTypes";
            this.grpTypes.Size = new System.Drawing.Size(202, 45);
            this.grpTypes.TabIndex = 20;
            this.grpTypes.TabStop = false;
            this.grpTypes.Text = "Timer Type";
            // 
            // cmbOwnerType
            // 
            this.cmbOwnerType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbOwnerType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbOwnerType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbOwnerType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbOwnerType.DrawDropdownHoverOutline = false;
            this.cmbOwnerType.DrawFocusRectangle = false;
            this.cmbOwnerType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOwnerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOwnerType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOwnerType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbOwnerType.FormattingEnabled = true;
            this.cmbOwnerType.Location = new System.Drawing.Point(6, 18);
            this.cmbOwnerType.Name = "cmbOwnerType";
            this.cmbOwnerType.Size = new System.Drawing.Size(190, 21);
            this.cmbOwnerType.TabIndex = 109;
            this.cmbOwnerType.Text = null;
            this.cmbOwnerType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbOwnerType.SelectedIndexChanged += new System.EventHandler(this.cmbOwnerType_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(548, 601);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(132, 27);
            this.btnSave.TabIndex = 52;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(683, 601);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(132, 27);
            this.btnCancel.TabIndex = 55;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkOnlyOnExclusiveMaps
            // 
            this.chkOnlyOnExclusiveMaps.AutoSize = true;
            this.chkOnlyOnExclusiveMaps.Location = new System.Drawing.Point(86, 19);
            this.chkOnlyOnExclusiveMaps.Name = "chkOnlyOnExclusiveMaps";
            this.chkOnlyOnExclusiveMaps.Size = new System.Drawing.Size(145, 17);
            this.chkOnlyOnExclusiveMaps.TabIndex = 101;
            this.chkOnlyOnExclusiveMaps.Text = "Only on Exclusive Maps?";
            this.chkOnlyOnExclusiveMaps.CheckedChanged += new System.EventHandler(this.chkOnlyOnExclusiveMaps_CheckedChanged);
            // 
            // frmTimers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1021, 640);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpTypes);
            this.Controls.Add(this.pnlTimerSettings);
            this.Controls.Add(this.grpTimers);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTimers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Timers";
            this.Load += new System.EventHandler(this.frmTimers_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpTimers.ResumeLayout(false);
            this.grpTimers.PerformLayout();
            this.pnlTimerSettings.ResumeLayout(false);
            this.pnlTimerSettings.PerformLayout();
            this.grpInstanceControl.ResumeLayout(false);
            this.grpInstanceControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudN)).EndInit();
            this.grpInstanceVarMod.ResumeLayout(false);
            this.grpInstanceVarMod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVarVal)).EndInit();
            this.grpTimerOptions.ResumeLayout(false);
            this.grpTimerOptions.PerformLayout();
            this.grpMapExclusivity.ResumeLayout(false);
            this.grpMapExclusivity.PerformLayout();
            this.grpEvents.ResumeLayout(false);
            this.grpEvents.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepetitions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeLimit)).EndInit();
            this.grpDisplay.ResumeLayout(false);
            this.grpDisplay.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpTypes.ResumeLayout(false);
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
        private DarkUI.Controls.DarkGroupBox grpTimers;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private System.Windows.Forms.Panel pnlTimerSettings;
        private DarkUI.Controls.DarkGroupBox grpGeneral;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private DarkUI.Controls.DarkGroupBox grpTypes;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpDisplay;
        private System.Windows.Forms.Label lblDisplayName;
        private DarkUI.Controls.DarkTextBox txtDisplayName;
        private DarkUI.Controls.DarkCheckBox chkHidden;
        private DarkUI.Controls.DarkGroupBox grpSettings;
        private System.Windows.Forms.Label lblTimeLimit;
        private DarkUI.Controls.DarkNumericUpDown nudTimeLimit;
        private DarkUI.Controls.DarkGroupBox grpEvents;
        private System.Windows.Forms.Label lblCancelled;
        private DarkUI.Controls.DarkComboBox cmbExpirationEvent;
        private System.Windows.Forms.Label lblExpiredEvent;
        private DarkUI.Controls.DarkComboBox cmbCancelledEvent;
        private DarkUI.Controls.DarkGroupBox grpTimerOptions;
        private System.Windows.Forms.Label lblRepetitions;
        private System.Windows.Forms.Label lblRepeat;
        private DarkUI.Controls.DarkComboBox cmbRepetitionType;
        private DarkUI.Controls.DarkNumericUpDown nudRepetitions;
        private DarkUI.Controls.DarkComboBox cmbCompletionEvent;
        private System.Windows.Forms.Label lblOnCompletion;
        private System.Windows.Forms.Label lblCompletionBehavior;
        private DarkUI.Controls.DarkComboBox cmbCompletionBehavior;
        private DarkUI.Controls.DarkComboBox cmbTimerType;
        private System.Windows.Forms.Label lblTimerType;
        private DarkUI.Controls.DarkComboBox cmbOwnerType;
        private DarkUI.Controls.DarkCheckBox chkRunIndefinite;
        private DarkUI.Controls.DarkCheckBox chkContinue;
        private DarkUI.Controls.DarkComboBox cmbLogoutBehavior;
        private System.Windows.Forms.Label lblLogoutBehavior;
        private DarkUI.Controls.DarkComboBox cmbVariable;
        private System.Windows.Forms.Label lblVariable;
        private DarkUI.Controls.DarkCheckBox chkStartWithServer;
        private DarkUI.Controls.DarkCheckBox chkContinueOnDeath;
        private DarkUI.Controls.DarkCheckBox chkContinueOnInstanceChange;
        private DarkUI.Controls.DarkGroupBox grpInstanceControl;
        private DarkUI.Controls.DarkGroupBox grpInstanceVarMod;
        private System.Windows.Forms.Label lblVarVal;
        private DarkUI.Controls.DarkNumericUpDown nudVarVal;
        private DarkUI.Controls.DarkComboBox cmbVarAction;
        private System.Windows.Forms.Label lblInstanceVarAction;
        private DarkUI.Controls.DarkComboBox cmbInstanceVar;
        private System.Windows.Forms.Label lblInstanceVar;
        private System.Windows.Forms.Label lblActionVal;
        private System.Windows.Forms.Label lblActions;
        private DarkUI.Controls.DarkNumericUpDown nudN;
        internal DarkUI.Controls.DarkRadioButton rdoOnNth;
        internal DarkUI.Controls.DarkRadioButton rdoEveryNth;
        private DarkUI.Controls.DarkCheckBox chkActionsEnabled;
        private DarkUI.Controls.DarkCheckBox chkSinglePlayerCompletion;
        private DarkUI.Controls.DarkCheckBox chkSinglePlayerExpire;
        private DarkUI.Controls.DarkCheckBox chkSinglePlayerCancel;
        private DarkUI.Controls.DarkGroupBox grpMapExclusivity;
        private DarkUI.Controls.DarkButton btnRemoveMap;
        private DarkUI.Controls.DarkButton btnAddMap;
        private System.Windows.Forms.Label lblMap;
        private DarkUI.Controls.DarkComboBox cmbMaps;
        private System.Windows.Forms.ListBox lstExclusiveMaps;
        private DarkUI.Controls.DarkCheckBox chkOnlyOnExclusiveMaps;
    }
}