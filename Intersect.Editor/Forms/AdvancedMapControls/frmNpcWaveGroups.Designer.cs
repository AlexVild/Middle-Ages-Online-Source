namespace Intersect.Editor.Forms.AdvancedMapControls
{
    partial class frmNpcWaveGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNpcWaveGroups));
            this.lstWaveGroups = new System.Windows.Forms.ListBox();
            this.btnAddGroup = new DarkUI.Controls.DarkButton();
            this.btnRemoveGroup = new DarkUI.Controls.DarkButton();
            this.grpWaveGroup = new DarkUI.Controls.DarkGroupBox();
            this.grpWave = new DarkUI.Controls.DarkGroupBox();
            this.btnRemoveWave = new DarkUI.Controls.DarkButton();
            this.btnAddWave = new DarkUI.Controls.DarkButton();
            this.lbllWaves = new System.Windows.Forms.Label();
            this.lstWaves = new System.Windows.Forms.ListBox();
            this.nudStartWave = new DarkUI.Controls.DarkNumericUpDown();
            this.lblStartWave = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoAdvance = new DarkUI.Controls.DarkCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAdvancement = new DarkUI.Controls.DarkGroupBox();
            this.nudAdvancetimeout = new DarkUI.Controls.DarkNumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.grpEvents = new DarkUI.Controls.DarkGroupBox();
            this.lblStartEvent = new System.Windows.Forms.Label();
            this.lblEndEvent = new System.Windows.Forms.Label();
            this.cmbStartEvent = new DarkUI.Controls.DarkComboBox();
            this.cmbEndEvent = new DarkUI.Controls.DarkComboBox();
            this.lblLoop = new System.Windows.Forms.Label();
            this.nudLoopIterations = new DarkUI.Controls.DarkNumericUpDown();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.lblTransitionTime = new System.Windows.Forms.Label();
            this.nudTransitionTime = new DarkUI.Controls.DarkNumericUpDown();
            this.grpWaveGroup.SuspendLayout();
            this.grpWave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartWave)).BeginInit();
            this.grpAdvancement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvancetimeout)).BeginInit();
            this.grpEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoopIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTransitionTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lstWaveGroups
            // 
            this.lstWaveGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstWaveGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstWaveGroups.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstWaveGroups.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstWaveGroups.FormattingEnabled = true;
            this.lstWaveGroups.ItemHeight = 20;
            this.lstWaveGroups.Location = new System.Drawing.Point(13, 39);
            this.lstWaveGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstWaveGroups.Name = "lstWaveGroups";
            this.lstWaveGroups.Size = new System.Drawing.Size(586, 102);
            this.lstWaveGroups.TabIndex = 127;
            this.lstWaveGroups.SelectedIndexChanged += new System.EventHandler(this.lstWaveGroups_SelectedIndexChanged);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(249, 151);
            this.btnAddGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Padding = new System.Windows.Forms.Padding(8);
            this.btnAddGroup.Size = new System.Drawing.Size(165, 35);
            this.btnAddGroup.TabIndex = 128;
            this.btnAddGroup.Text = "Add";
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Location = new System.Drawing.Point(430, 151);
            this.btnRemoveGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Padding = new System.Windows.Forms.Padding(8);
            this.btnRemoveGroup.Size = new System.Drawing.Size(169, 35);
            this.btnRemoveGroup.TabIndex = 129;
            this.btnRemoveGroup.Text = "Remove";
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // grpWaveGroup
            // 
            this.grpWaveGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWaveGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWaveGroup.Controls.Add(this.grpWave);
            this.grpWaveGroup.Controls.Add(this.btnRemoveWave);
            this.grpWaveGroup.Controls.Add(this.btnAddWave);
            this.grpWaveGroup.Controls.Add(this.lbllWaves);
            this.grpWaveGroup.Controls.Add(this.lstWaves);
            this.grpWaveGroup.Controls.Add(this.nudStartWave);
            this.grpWaveGroup.Controls.Add(this.lblStartWave);
            this.grpWaveGroup.Controls.Add(this.txtName);
            this.grpWaveGroup.Controls.Add(this.lblName);
            this.grpWaveGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWaveGroup.Location = new System.Drawing.Point(13, 196);
            this.grpWaveGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpWaveGroup.Name = "grpWaveGroup";
            this.grpWaveGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpWaveGroup.Size = new System.Drawing.Size(586, 797);
            this.grpWaveGroup.TabIndex = 130;
            this.grpWaveGroup.TabStop = false;
            this.grpWaveGroup.Text = "Wave Group";
            // 
            // grpWave
            // 
            this.grpWave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWave.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWave.Controls.Add(this.grpEvents);
            this.grpWave.Controls.Add(this.grpAdvancement);
            this.grpWave.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWave.Location = new System.Drawing.Point(12, 309);
            this.grpWave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpWave.Name = "grpWave";
            this.grpWave.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpWave.Size = new System.Drawing.Size(562, 478);
            this.grpWave.TabIndex = 134;
            this.grpWave.TabStop = false;
            this.grpWave.Text = "Wave";
            // 
            // btnRemoveWave
            // 
            this.btnRemoveWave.Location = new System.Drawing.Point(405, 268);
            this.btnRemoveWave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveWave.Name = "btnRemoveWave";
            this.btnRemoveWave.Padding = new System.Windows.Forms.Padding(8);
            this.btnRemoveWave.Size = new System.Drawing.Size(169, 35);
            this.btnRemoveWave.TabIndex = 132;
            this.btnRemoveWave.Text = "Remove";
            this.btnRemoveWave.Click += new System.EventHandler(this.btnRemoveWave_Click);
            // 
            // btnAddWave
            // 
            this.btnAddWave.Location = new System.Drawing.Point(223, 268);
            this.btnAddWave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddWave.Name = "btnAddWave";
            this.btnAddWave.Padding = new System.Windows.Forms.Padding(8);
            this.btnAddWave.Size = new System.Drawing.Size(165, 35);
            this.btnAddWave.TabIndex = 133;
            this.btnAddWave.Text = "Add";
            this.btnAddWave.Click += new System.EventHandler(this.btnAddWave_Click);
            // 
            // lbllWaves
            // 
            this.lbllWaves.AutoSize = true;
            this.lbllWaves.Location = new System.Drawing.Point(17, 131);
            this.lbllWaves.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbllWaves.Name = "lbllWaves";
            this.lbllWaves.Size = new System.Drawing.Size(57, 20);
            this.lbllWaves.TabIndex = 132;
            this.lbllWaves.Text = "Waves";
            // 
            // lstWaves
            // 
            this.lstWaves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstWaves.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstWaves.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstWaves.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstWaves.FormattingEnabled = true;
            this.lstWaves.ItemHeight = 20;
            this.lstWaves.Location = new System.Drawing.Point(21, 156);
            this.lstWaves.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstWaves.Name = "lstWaves";
            this.lstWaves.Size = new System.Drawing.Size(553, 102);
            this.lstWaves.TabIndex = 131;
            this.lstWaves.SelectedIndexChanged += new System.EventHandler(this.lstWaves_SelectedIndexChanged);
            // 
            // nudStartWave
            // 
            this.nudStartWave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStartWave.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStartWave.Location = new System.Drawing.Point(21, 90);
            this.nudStartWave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudStartWave.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudStartWave.Name = "nudStartWave";
            this.nudStartWave.Size = new System.Drawing.Size(134, 26);
            this.nudStartWave.TabIndex = 81;
            this.nudStartWave.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStartWave.ValueChanged += new System.EventHandler(this.nudStartWave_ValueChanged);
            // 
            // lblStartWave
            // 
            this.lblStartWave.AutoSize = true;
            this.lblStartWave.Location = new System.Drawing.Point(17, 64);
            this.lblStartWave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartWave.Name = "lblStartWave";
            this.lblStartWave.Size = new System.Drawing.Size(119, 20);
            this.lblStartWave.TabIndex = 57;
            this.lblStartWave.Text = "Autostart Wave";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(181, 27);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(397, 26);
            this.txtName.TabIndex = 22;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(17, 29);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 20);
            this.lblName.TabIndex = 21;
            this.lblName.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(9, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 131;
            this.label2.Text = "Wave Groups";
            // 
            // chkAutoAdvance
            // 
            this.chkAutoAdvance.AutoSize = true;
            this.chkAutoAdvance.Location = new System.Drawing.Point(8, 29);
            this.chkAutoAdvance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAutoAdvance.Name = "chkAutoAdvance";
            this.chkAutoAdvance.Size = new System.Drawing.Size(430, 24);
            this.chkAutoAdvance.TabIndex = 54;
            this.chkAutoAdvance.Text = "Advance when all permadead NPCs on group are dead?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 20);
            this.label1.TabIndex = 58;
            this.label1.Text = "Advance on Timeout (ms)";
            // 
            // grpAdvancement
            // 
            this.grpAdvancement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAdvancement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAdvancement.Controls.Add(this.nudTransitionTime);
            this.grpAdvancement.Controls.Add(this.lblTransitionTime);
            this.grpAdvancement.Controls.Add(this.nudLoopIterations);
            this.grpAdvancement.Controls.Add(this.lblLoop);
            this.grpAdvancement.Controls.Add(this.label3);
            this.grpAdvancement.Controls.Add(this.nudAdvancetimeout);
            this.grpAdvancement.Controls.Add(this.chkAutoAdvance);
            this.grpAdvancement.Controls.Add(this.label1);
            this.grpAdvancement.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAdvancement.Location = new System.Drawing.Point(9, 36);
            this.grpAdvancement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAdvancement.Name = "grpAdvancement";
            this.grpAdvancement.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAdvancement.Size = new System.Drawing.Size(545, 234);
            this.grpAdvancement.TabIndex = 135;
            this.grpAdvancement.TabStop = false;
            this.grpAdvancement.Text = "Wave Advancement";
            // 
            // nudAdvancetimeout
            // 
            this.nudAdvancetimeout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAdvancetimeout.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAdvancetimeout.Location = new System.Drawing.Point(287, 67);
            this.nudAdvancetimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudAdvancetimeout.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudAdvancetimeout.Name = "nudAdvancetimeout";
            this.nudAdvancetimeout.Size = new System.Drawing.Size(250, 26);
            this.nudAdvancetimeout.TabIndex = 82;
            this.nudAdvancetimeout.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAdvancetimeout.ValueChanged += new System.EventHandler(this.nudAdvancetimeout_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(357, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 83;
            this.label3.Text = "(Active if > 0)";
            // 
            // grpEvents
            // 
            this.grpEvents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEvents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEvents.Controls.Add(this.cmbEndEvent);
            this.grpEvents.Controls.Add(this.cmbStartEvent);
            this.grpEvents.Controls.Add(this.lblEndEvent);
            this.grpEvents.Controls.Add(this.lblStartEvent);
            this.grpEvents.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEvents.Location = new System.Drawing.Point(9, 291);
            this.grpEvents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEvents.Name = "grpEvents";
            this.grpEvents.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEvents.Size = new System.Drawing.Size(545, 163);
            this.grpEvents.TabIndex = 136;
            this.grpEvents.TabStop = false;
            this.grpEvents.Text = "Events";
            // 
            // lblStartEvent
            // 
            this.lblStartEvent.AutoSize = true;
            this.lblStartEvent.Location = new System.Drawing.Point(8, 24);
            this.lblStartEvent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartEvent.Name = "lblStartEvent";
            this.lblStartEvent.Size = new System.Drawing.Size(115, 20);
            this.lblStartEvent.TabIndex = 59;
            this.lblStartEvent.Text = "On-Start Event";
            // 
            // lblEndEvent
            // 
            this.lblEndEvent.AutoSize = true;
            this.lblEndEvent.Location = new System.Drawing.Point(8, 90);
            this.lblEndEvent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndEvent.Name = "lblEndEvent";
            this.lblEndEvent.Size = new System.Drawing.Size(109, 20);
            this.lblEndEvent.TabIndex = 60;
            this.lblEndEvent.Text = "On-End Event";
            // 
            // cmbStartEvent
            // 
            this.cmbStartEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStartEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStartEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStartEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStartEvent.DrawDropdownHoverOutline = false;
            this.cmbStartEvent.DrawFocusRectangle = false;
            this.cmbStartEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStartEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStartEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStartEvent.FormattingEnabled = true;
            this.cmbStartEvent.Location = new System.Drawing.Point(12, 49);
            this.cmbStartEvent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbStartEvent.Name = "cmbStartEvent";
            this.cmbStartEvent.Size = new System.Drawing.Size(525, 27);
            this.cmbStartEvent.TabIndex = 61;
            this.cmbStartEvent.Text = null;
            this.cmbStartEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbStartEvent.SelectedIndexChanged += new System.EventHandler(this.cmbStartEvent_SelectedIndexChanged);
            // 
            // cmbEndEvent
            // 
            this.cmbEndEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEndEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEndEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEndEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEndEvent.DrawDropdownHoverOutline = false;
            this.cmbEndEvent.DrawFocusRectangle = false;
            this.cmbEndEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEndEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEndEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEndEvent.FormattingEnabled = true;
            this.cmbEndEvent.Location = new System.Drawing.Point(12, 115);
            this.cmbEndEvent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEndEvent.Name = "cmbEndEvent";
            this.cmbEndEvent.Size = new System.Drawing.Size(525, 27);
            this.cmbEndEvent.TabIndex = 62;
            this.cmbEndEvent.Text = null;
            this.cmbEndEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEndEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEndEvent_SelectedIndexChanged);
            // 
            // lblLoop
            // 
            this.lblLoop.AutoSize = true;
            this.lblLoop.Location = new System.Drawing.Point(8, 128);
            this.lblLoop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLoop.Name = "lblLoop";
            this.lblLoop.Size = new System.Drawing.Size(116, 20);
            this.lblLoop.TabIndex = 84;
            this.lblLoop.Text = "Loop Iterations";
            // 
            // nudLoopIterations
            // 
            this.nudLoopIterations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudLoopIterations.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudLoopIterations.Location = new System.Drawing.Point(287, 130);
            this.nudLoopIterations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudLoopIterations.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudLoopIterations.Name = "nudLoopIterations";
            this.nudLoopIterations.Size = new System.Drawing.Size(250, 26);
            this.nudLoopIterations.TabIndex = 85;
            this.nudLoopIterations.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudLoopIterations.ValueChanged += new System.EventHandler(this.nudLoopIterations_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(261, 1013);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8);
            this.btnSave.Size = new System.Drawing.Size(165, 35);
            this.btnSave.TabIndex = 132;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(434, 1013);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(8);
            this.btnCancel.Size = new System.Drawing.Size(165, 35);
            this.btnCancel.TabIndex = 133;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTransitionTime
            // 
            this.lblTransitionTime.AutoSize = true;
            this.lblTransitionTime.Location = new System.Drawing.Point(8, 181);
            this.lblTransitionTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTransitionTime.Name = "lblTransitionTime";
            this.lblTransitionTime.Size = new System.Drawing.Size(195, 20);
            this.lblTransitionTime.TabIndex = 86;
            this.lblTransitionTime.Text = "Wave Transition Time (ms)";
            // 
            // nudTransitionTime
            // 
            this.nudTransitionTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTransitionTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTransitionTime.Location = new System.Drawing.Point(287, 179);
            this.nudTransitionTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudTransitionTime.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudTransitionTime.Name = "nudTransitionTime";
            this.nudTransitionTime.Size = new System.Drawing.Size(250, 26);
            this.nudTransitionTime.TabIndex = 87;
            this.nudTransitionTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTransitionTime.ValueChanged += new System.EventHandler(this.nudTransitionTime_ValueChanged);
            // 
            // frmNpcWaveGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(615, 1062);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpWaveGroup);
            this.Controls.Add(this.btnRemoveGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.lstWaveGroups);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNpcWaveGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NPC Wave Group Editor";
            this.grpWaveGroup.ResumeLayout(false);
            this.grpWaveGroup.PerformLayout();
            this.grpWave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudStartWave)).EndInit();
            this.grpAdvancement.ResumeLayout(false);
            this.grpAdvancement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdvancetimeout)).EndInit();
            this.grpEvents.ResumeLayout(false);
            this.grpEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoopIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTransitionTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstWaveGroups;
        private DarkUI.Controls.DarkButton btnAddGroup;
        private DarkUI.Controls.DarkButton btnRemoveGroup;
        private DarkUI.Controls.DarkGroupBox grpWaveGroup;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private System.Windows.Forms.Label lblStartWave;
        private DarkUI.Controls.DarkNumericUpDown nudStartWave;
        private System.Windows.Forms.Label lbllWaves;
        private System.Windows.Forms.ListBox lstWaves;
        private System.Windows.Forms.Label label2;
        private DarkUI.Controls.DarkButton btnRemoveWave;
        private DarkUI.Controls.DarkButton btnAddWave;
        private DarkUI.Controls.DarkGroupBox grpWave;
        private DarkUI.Controls.DarkCheckBox chkAutoAdvance;
        private System.Windows.Forms.Label label1;
        private DarkUI.Controls.DarkGroupBox grpAdvancement;
        private DarkUI.Controls.DarkNumericUpDown nudAdvancetimeout;
        private System.Windows.Forms.Label label3;
        private DarkUI.Controls.DarkGroupBox grpEvents;
        private System.Windows.Forms.Label lblStartEvent;
        private System.Windows.Forms.Label lblEndEvent;
        private DarkUI.Controls.DarkComboBox cmbStartEvent;
        private DarkUI.Controls.DarkComboBox cmbEndEvent;
        private System.Windows.Forms.Label lblLoop;
        private DarkUI.Controls.DarkNumericUpDown nudLoopIterations;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private System.Windows.Forms.Label lblTransitionTime;
        private DarkUI.Controls.DarkNumericUpDown nudTransitionTime;
    }
}