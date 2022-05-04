
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
            this.grpTimerOptions = new DarkUI.Controls.DarkGroupBox();
            this.lblCompletionBehavior = new System.Windows.Forms.Label();
            this.cmbCompletionBehavior = new DarkUI.Controls.DarkComboBox();
            this.chkContinueTimeout = new DarkUI.Controls.DarkCheckBox();
            this.Events = new DarkUI.Controls.DarkGroupBox();
            this.cmbCompletionEvent = new DarkUI.Controls.DarkComboBox();
            this.lblOnCompletion = new System.Windows.Forms.Label();
            this.cmbExpirationEvent = new DarkUI.Controls.DarkComboBox();
            this.lblExpiredEvent = new System.Windows.Forms.Label();
            this.cmbCancelledEvent = new DarkUI.Controls.DarkComboBox();
            this.lblCancelled = new System.Windows.Forms.Label();
            this.grpSettings = new DarkUI.Controls.DarkGroupBox();
            this.lblRepetitions = new System.Windows.Forms.Label();
            this.lblRepeat = new System.Windows.Forms.Label();
            this.cmbRepetitionType = new DarkUI.Controls.DarkComboBox();
            this.nudRepetitions = new DarkUI.Controls.DarkNumericUpDown();
            this.rdoCountdown = new DarkUI.Controls.DarkRadioButton();
            this.nudTimeLimit = new DarkUI.Controls.DarkNumericUpDown();
            this.lblTimeLimit = new System.Windows.Forms.Label();
            this.rdoStopwatch = new DarkUI.Controls.DarkRadioButton();
            this.rdoScheduler = new DarkUI.Controls.DarkRadioButton();
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
            this.rdoInstanceTimers = new DarkUI.Controls.DarkRadioButton();
            this.rdoPlayerTimers = new DarkUI.Controls.DarkRadioButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.toolStrip.SuspendLayout();
            this.grpTimers.SuspendLayout();
            this.pnlTimerSettings.SuspendLayout();
            this.grpTimerOptions.SuspendLayout();
            this.Events.SuspendLayout();
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
            this.toolStrip.Size = new System.Drawing.Size(532, 25);
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
            this.grpTimers.Size = new System.Drawing.Size(202, 395);
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
            this.lstGameObjects.Size = new System.Drawing.Size(190, 343);
            this.lstGameObjects.TabIndex = 2;
            // 
            // pnlTimerSettings
            // 
            this.pnlTimerSettings.Controls.Add(this.grpTimerOptions);
            this.pnlTimerSettings.Controls.Add(this.Events);
            this.pnlTimerSettings.Controls.Add(this.grpSettings);
            this.pnlTimerSettings.Controls.Add(this.grpDisplay);
            this.pnlTimerSettings.Controls.Add(this.grpGeneral);
            this.pnlTimerSettings.Location = new System.Drawing.Point(229, 23);
            this.pnlTimerSettings.Name = "pnlTimerSettings";
            this.pnlTimerSettings.Size = new System.Drawing.Size(291, 560);
            this.pnlTimerSettings.TabIndex = 47;
            this.pnlTimerSettings.Visible = false;
            // 
            // grpTimerOptions
            // 
            this.grpTimerOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTimerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTimerOptions.Controls.Add(this.lblCompletionBehavior);
            this.grpTimerOptions.Controls.Add(this.cmbCompletionBehavior);
            this.grpTimerOptions.Controls.Add(this.chkContinueTimeout);
            this.grpTimerOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTimerOptions.Location = new System.Drawing.Point(10, 472);
            this.grpTimerOptions.Name = "grpTimerOptions";
            this.grpTimerOptions.Size = new System.Drawing.Size(274, 76);
            this.grpTimerOptions.TabIndex = 106;
            this.grpTimerOptions.TabStop = false;
            this.grpTimerOptions.Text = "Timer Options";
            // 
            // lblCompletionBehavior
            // 
            this.lblCompletionBehavior.AutoSize = true;
            this.lblCompletionBehavior.Location = new System.Drawing.Point(3, 45);
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
            this.cmbCompletionBehavior.Location = new System.Drawing.Point(123, 42);
            this.cmbCompletionBehavior.Name = "cmbCompletionBehavior";
            this.cmbCompletionBehavior.Size = new System.Drawing.Size(140, 21);
            this.cmbCompletionBehavior.TabIndex = 108;
            this.cmbCompletionBehavior.Text = null;
            this.cmbCompletionBehavior.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCompletionBehavior.SelectedIndexChanged += new System.EventHandler(this.cmbCompletionBehavior_SelectedIndexChanged);
            // 
            // chkContinueTimeout
            // 
            this.chkContinueTimeout.AutoSize = true;
            this.chkContinueTimeout.Location = new System.Drawing.Point(9, 19);
            this.chkContinueTimeout.Name = "chkContinueTimeout";
            this.chkContinueTimeout.Size = new System.Drawing.Size(137, 17);
            this.chkContinueTimeout.TabIndex = 101;
            this.chkContinueTimeout.Text = "Continue On Time-Out?";
            this.chkContinueTimeout.CheckedChanged += new System.EventHandler(this.chkContinueTimeout_CheckedChanged);
            // 
            // Events
            // 
            this.Events.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Events.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.Events.Controls.Add(this.cmbCompletionEvent);
            this.Events.Controls.Add(this.lblOnCompletion);
            this.Events.Controls.Add(this.cmbExpirationEvent);
            this.Events.Controls.Add(this.lblExpiredEvent);
            this.Events.Controls.Add(this.cmbCancelledEvent);
            this.Events.Controls.Add(this.lblCancelled);
            this.Events.ForeColor = System.Drawing.Color.Gainsboro;
            this.Events.Location = new System.Drawing.Point(10, 323);
            this.Events.Name = "Events";
            this.Events.Size = new System.Drawing.Size(274, 143);
            this.Events.TabIndex = 101;
            this.Events.TabStop = false;
            this.Events.Text = "Events";
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
            this.grpSettings.Controls.Add(this.lblRepetitions);
            this.grpSettings.Controls.Add(this.lblRepeat);
            this.grpSettings.Controls.Add(this.cmbRepetitionType);
            this.grpSettings.Controls.Add(this.nudRepetitions);
            this.grpSettings.Controls.Add(this.rdoCountdown);
            this.grpSettings.Controls.Add(this.nudTimeLimit);
            this.grpSettings.Controls.Add(this.lblTimeLimit);
            this.grpSettings.Controls.Add(this.rdoStopwatch);
            this.grpSettings.Controls.Add(this.rdoScheduler);
            this.grpSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSettings.Location = new System.Drawing.Point(10, 89);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(274, 141);
            this.grpSettings.TabIndex = 101;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            // 
            // lblRepetitions
            // 
            this.lblRepetitions.AutoSize = true;
            this.lblRepetitions.Location = new System.Drawing.Point(6, 107);
            this.lblRepetitions.Name = "lblRepetitions";
            this.lblRepetitions.Size = new System.Drawing.Size(60, 13);
            this.lblRepetitions.TabIndex = 106;
            this.lblRepetitions.Text = "Repetitions";
            // 
            // lblRepeat
            // 
            this.lblRepeat.AutoSize = true;
            this.lblRepeat.Location = new System.Drawing.Point(6, 79);
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
            this.cmbRepetitionType.Location = new System.Drawing.Point(135, 76);
            this.cmbRepetitionType.Name = "cmbRepetitionType";
            this.cmbRepetitionType.Size = new System.Drawing.Size(128, 21);
            this.cmbRepetitionType.TabIndex = 24;
            this.cmbRepetitionType.Text = null;
            this.cmbRepetitionType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbRepetitionType.SelectedIndexChanged += new System.EventHandler(this.cmbRepetitionType_SelectedIndexChanged);
            // 
            // nudRepetitions
            // 
            this.nudRepetitions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRepetitions.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRepetitions.Location = new System.Drawing.Point(135, 105);
            this.nudRepetitions.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudRepetitions.Name = "nudRepetitions";
            this.nudRepetitions.Size = new System.Drawing.Size(128, 20);
            this.nudRepetitions.TabIndex = 104;
            this.nudRepetitions.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRepetitions.ValueChanged += new System.EventHandler(this.nudRepetitions_ValueChanged);
            // 
            // rdoCountdown
            // 
            this.rdoCountdown.AutoSize = true;
            this.rdoCountdown.Location = new System.Drawing.Point(180, 19);
            this.rdoCountdown.Name = "rdoCountdown";
            this.rdoCountdown.Size = new System.Drawing.Size(79, 17);
            this.rdoCountdown.TabIndex = 103;
            this.rdoCountdown.Text = "Countdown";
            this.rdoCountdown.CheckedChanged += new System.EventHandler(this.rdoCountdown_CheckedChanged);
            // 
            // nudTimeLimit
            // 
            this.nudTimeLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTimeLimit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTimeLimit.Location = new System.Drawing.Point(135, 50);
            this.nudTimeLimit.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTimeLimit.Name = "nudTimeLimit";
            this.nudTimeLimit.Size = new System.Drawing.Size(128, 20);
            this.nudTimeLimit.TabIndex = 102;
            this.nudTimeLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTimeLimit.ValueChanged += new System.EventHandler(this.nudTimeLimit_ValueChanged);
            // 
            // lblTimeLimit
            // 
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Location = new System.Drawing.Point(6, 52);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(91, 13);
            this.lblTimeLimit.TabIndex = 101;
            this.lblTimeLimit.Text = "Interval (seconds)";
            // 
            // rdoStopwatch
            // 
            this.rdoStopwatch.AutoSize = true;
            this.rdoStopwatch.Location = new System.Drawing.Point(92, 19);
            this.rdoStopwatch.Name = "rdoStopwatch";
            this.rdoStopwatch.Size = new System.Drawing.Size(76, 17);
            this.rdoStopwatch.TabIndex = 6;
            this.rdoStopwatch.Text = "Stopwatch";
            this.rdoStopwatch.CheckedChanged += new System.EventHandler(this.rdoStopwatch_CheckedChanged);
            // 
            // rdoScheduler
            // 
            this.rdoScheduler.AutoSize = true;
            this.rdoScheduler.Checked = true;
            this.rdoScheduler.Location = new System.Drawing.Point(8, 19);
            this.rdoScheduler.Name = "rdoScheduler";
            this.rdoScheduler.Size = new System.Drawing.Size(73, 17);
            this.rdoScheduler.TabIndex = 5;
            this.rdoScheduler.TabStop = true;
            this.rdoScheduler.Text = "Scheduler";
            this.rdoScheduler.CheckedChanged += new System.EventHandler(this.rdoScheduler_CheckedChanged);
            // 
            // grpDisplay
            // 
            this.grpDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDisplay.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDisplay.Controls.Add(this.chkHidden);
            this.grpDisplay.Controls.Add(this.lblDisplayName);
            this.grpDisplay.Controls.Add(this.txtDisplayName);
            this.grpDisplay.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDisplay.Location = new System.Drawing.Point(10, 236);
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
            this.grpTypes.Controls.Add(this.rdoInstanceTimers);
            this.grpTypes.Controls.Add(this.rdoPlayerTimers);
            this.grpTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTypes.Location = new System.Drawing.Point(18, 28);
            this.grpTypes.Name = "grpTypes";
            this.grpTypes.Size = new System.Drawing.Size(202, 45);
            this.grpTypes.TabIndex = 20;
            this.grpTypes.TabStop = false;
            this.grpTypes.Text = "Timer Type";
            // 
            // rdoInstanceTimers
            // 
            this.rdoInstanceTimers.AutoSize = true;
            this.rdoInstanceTimers.Location = new System.Drawing.Point(106, 18);
            this.rdoInstanceTimers.Name = "rdoInstanceTimers";
            this.rdoInstanceTimers.Size = new System.Drawing.Size(66, 17);
            this.rdoInstanceTimers.TabIndex = 4;
            this.rdoInstanceTimers.Text = "Instance";
            this.rdoInstanceTimers.CheckedChanged += new System.EventHandler(this.rdoInstanceTimers_CheckedChanged);
            // 
            // rdoPlayerTimers
            // 
            this.rdoPlayerTimers.AutoSize = true;
            this.rdoPlayerTimers.Checked = true;
            this.rdoPlayerTimers.Location = new System.Drawing.Point(20, 19);
            this.rdoPlayerTimers.Name = "rdoPlayerTimers";
            this.rdoPlayerTimers.Size = new System.Drawing.Size(54, 17);
            this.rdoPlayerTimers.TabIndex = 1;
            this.rdoPlayerTimers.TabStop = true;
            this.rdoPlayerTimers.Text = "Player";
            this.rdoPlayerTimers.CheckedChanged += new System.EventHandler(this.rdoPlayerTimers_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(239, 608);
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
            this.btnCancel.Location = new System.Drawing.Point(388, 608);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(132, 27);
            this.btnCancel.TabIndex = 55;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmTimers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(532, 651);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpTypes);
            this.Controls.Add(this.pnlTimerSettings);
            this.Controls.Add(this.grpTimers);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTimers";
            this.Text = "Timers";
            this.Load += new System.EventHandler(this.frmTimers_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpTimers.ResumeLayout(false);
            this.grpTimers.PerformLayout();
            this.pnlTimerSettings.ResumeLayout(false);
            this.grpTimerOptions.ResumeLayout(false);
            this.grpTimerOptions.PerformLayout();
            this.Events.ResumeLayout(false);
            this.Events.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepetitions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeLimit)).EndInit();
            this.grpDisplay.ResumeLayout(false);
            this.grpDisplay.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpTypes.ResumeLayout(false);
            this.grpTypes.PerformLayout();
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
        private DarkUI.Controls.DarkRadioButton rdoInstanceTimers;
        private DarkUI.Controls.DarkRadioButton rdoPlayerTimers;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpDisplay;
        private System.Windows.Forms.Label lblDisplayName;
        private DarkUI.Controls.DarkTextBox txtDisplayName;
        private DarkUI.Controls.DarkCheckBox chkHidden;
        private DarkUI.Controls.DarkGroupBox grpSettings;
        private DarkUI.Controls.DarkRadioButton rdoStopwatch;
        private DarkUI.Controls.DarkRadioButton rdoScheduler;
        private System.Windows.Forms.Label lblTimeLimit;
        private DarkUI.Controls.DarkNumericUpDown nudTimeLimit;
        private DarkUI.Controls.DarkGroupBox Events;
        private System.Windows.Forms.Label lblCancelled;
        private DarkUI.Controls.DarkComboBox cmbExpirationEvent;
        private System.Windows.Forms.Label lblExpiredEvent;
        private DarkUI.Controls.DarkComboBox cmbCancelledEvent;
        private DarkUI.Controls.DarkGroupBox grpTimerOptions;
        private DarkUI.Controls.DarkCheckBox chkContinueTimeout;
        private DarkUI.Controls.DarkRadioButton rdoCountdown;
        private System.Windows.Forms.Label lblRepetitions;
        private System.Windows.Forms.Label lblRepeat;
        private DarkUI.Controls.DarkComboBox cmbRepetitionType;
        private DarkUI.Controls.DarkNumericUpDown nudRepetitions;
        private DarkUI.Controls.DarkComboBox cmbCompletionEvent;
        private System.Windows.Forms.Label lblOnCompletion;
        private System.Windows.Forms.Label lblCompletionBehavior;
        private DarkUI.Controls.DarkComboBox cmbCompletionBehavior;
    }
}