using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandVariable
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpSetVariable = new DarkUI.Controls.DarkGroupBox();
            this.chkInstanceSync = new DarkUI.Controls.DarkCheckBox();
            this.chkSyncParty = new DarkUI.Controls.DarkCheckBox();
            this.grpSelectVariable = new DarkUI.Controls.DarkGroupBox();
            this.rdoGuildVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoInstanceVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoPlayerVariable = new DarkUI.Controls.DarkRadioButton();
            this.cmbVariable = new DarkUI.Controls.DarkComboBox();
            this.rdoGlobalVariable = new DarkUI.Controls.DarkRadioButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpStringVariable = new DarkUI.Controls.DarkGroupBox();
            this.chkToLower = new DarkUI.Controls.DarkCheckBox();
            this.lblStringTextVariables = new System.Windows.Forms.Label();
            this.grpStringReplace = new DarkUI.Controls.DarkGroupBox();
            this.txtStringReplace = new DarkUI.Controls.DarkTextBox();
            this.txtStringFind = new DarkUI.Controls.DarkTextBox();
            this.lblStringReplace = new System.Windows.Forms.Label();
            this.lblStringFind = new System.Windows.Forms.Label();
            this.optReplaceString = new DarkUI.Controls.DarkRadioButton();
            this.optStaticString = new DarkUI.Controls.DarkRadioButton();
            this.grpStringSet = new DarkUI.Controls.DarkGroupBox();
            this.lblStringValue = new System.Windows.Forms.Label();
            this.txtStringValue = new DarkUI.Controls.DarkTextBox();
            this.grpNumericVariable = new DarkUI.Controls.DarkGroupBox();
            this.optNumericMod = new DarkUI.Controls.DarkRadioButton();
            this.rdoInventorySlots = new DarkUI.Controls.DarkRadioButton();
            this.rdoSpawnGroup = new DarkUI.Controls.DarkRadioButton();
            this.optEventX = new DarkUI.Controls.DarkRadioButton();
            this.optEventY = new DarkUI.Controls.DarkRadioButton();
            this.optNumericRightShift = new DarkUI.Controls.DarkRadioButton();
            this.optNumericLeftShift = new DarkUI.Controls.DarkRadioButton();
            this.optPlayerY = new DarkUI.Controls.DarkRadioButton();
            this.optNumericDivide = new DarkUI.Controls.DarkRadioButton();
            this.optPlayerX = new DarkUI.Controls.DarkRadioButton();
            this.optNumericMultiply = new DarkUI.Controls.DarkRadioButton();
            this.optPlayerLevel = new DarkUI.Controls.DarkRadioButton();
            this.optNumericSet = new DarkUI.Controls.DarkRadioButton();
            this.optNumericAdd = new DarkUI.Controls.DarkRadioButton();
            this.optNumericRandom = new DarkUI.Controls.DarkRadioButton();
            this.optNumericSubtract = new DarkUI.Controls.DarkRadioButton();
            this.optNumericSystemTime = new DarkUI.Controls.DarkRadioButton();
            this.grpNumericValues = new DarkUI.Controls.DarkGroupBox();
            this.cmbGuildVarNum = new DarkUI.Controls.DarkComboBox();
            this.rdoGuildVarNum = new DarkUI.Controls.DarkRadioButton();
            this.cmbNumericCloneInstanceVar = new DarkUI.Controls.DarkComboBox();
            this.optNumericCloneInstanceVar = new DarkUI.Controls.DarkRadioButton();
            this.nudNumericValue = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbNumericCloneGlobalVar = new DarkUI.Controls.DarkComboBox();
            this.cmbNumericClonePlayerVar = new DarkUI.Controls.DarkComboBox();
            this.optNumericCloneGlobalVar = new DarkUI.Controls.DarkRadioButton();
            this.optNumericClonePlayerVar = new DarkUI.Controls.DarkRadioButton();
            this.optNumericStaticVal = new DarkUI.Controls.DarkRadioButton();
            this.grpNumericRandom = new DarkUI.Controls.DarkGroupBox();
            this.nudHigh = new DarkUI.Controls.DarkNumericUpDown();
            this.nudLow = new DarkUI.Controls.DarkNumericUpDown();
            this.lblNumericRandomHigh = new System.Windows.Forms.Label();
            this.lblNumericRandomLow = new System.Windows.Forms.Label();
            this.grpBooleanVariable = new DarkUI.Controls.DarkGroupBox();
            this.cmbGuildVarBool = new DarkUI.Controls.DarkComboBox();
            this.rdoGuildVarBool = new DarkUI.Controls.DarkRadioButton();
            this.cmbBooleanInstanceGlobalVar = new DarkUI.Controls.DarkComboBox();
            this.optBooleanCloneInstanceVar = new DarkUI.Controls.DarkRadioButton();
            this.cmbBooleanCloneGlobalVar = new DarkUI.Controls.DarkComboBox();
            this.cmbBooleanClonePlayerVar = new DarkUI.Controls.DarkComboBox();
            this.optBooleanCloneGlobalVar = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanClonePlayerVar = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanTrue = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanFalse = new DarkUI.Controls.DarkRadioButton();
            this.optBooleanToggle = new DarkUI.Controls.DarkRadioButton();
            this.grpSetVariable.SuspendLayout();
            this.grpSelectVariable.SuspendLayout();
            this.grpStringVariable.SuspendLayout();
            this.grpStringReplace.SuspendLayout();
            this.grpStringSet.SuspendLayout();
            this.grpNumericVariable.SuspendLayout();
            this.grpNumericValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumericValue)).BeginInit();
            this.grpNumericRandom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLow)).BeginInit();
            this.grpBooleanVariable.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSetVariable
            // 
            this.grpSetVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSetVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSetVariable.Controls.Add(this.chkInstanceSync);
            this.grpSetVariable.Controls.Add(this.chkSyncParty);
            this.grpSetVariable.Controls.Add(this.grpBooleanVariable);
            this.grpSetVariable.Controls.Add(this.grpSelectVariable);
            this.grpSetVariable.Controls.Add(this.btnCancel);
            this.grpSetVariable.Controls.Add(this.btnSave);
            this.grpSetVariable.Controls.Add(this.grpStringVariable);
            this.grpSetVariable.Controls.Add(this.grpNumericVariable);
            this.grpSetVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetVariable.Location = new System.Drawing.Point(4, 5);
            this.grpSetVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSetVariable.Name = "grpSetVariable";
            this.grpSetVariable.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSetVariable.Size = new System.Drawing.Size(656, 677);
            this.grpSetVariable.TabIndex = 17;
            this.grpSetVariable.TabStop = false;
            this.grpSetVariable.Text = "Set Variable";
            // 
            // chkInstanceSync
            // 
            this.chkInstanceSync.AutoSize = true;
            this.chkInstanceSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkInstanceSync.Location = new System.Drawing.Point(18, 638);
            this.chkInstanceSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkInstanceSync.Name = "chkInstanceSync";
            this.chkInstanceSync.Size = new System.Drawing.Size(145, 24);
            this.chkInstanceSync.TabIndex = 42;
            this.chkInstanceSync.Text = "Instance Sync?";
            // 
            // chkSyncParty
            // 
            this.chkSyncParty.AutoSize = true;
            this.chkSyncParty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkSyncParty.Location = new System.Drawing.Point(177, 638);
            this.chkSyncParty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSyncParty.Name = "chkSyncParty";
            this.chkSyncParty.Size = new System.Drawing.Size(119, 24);
            this.chkSyncParty.TabIndex = 40;
            this.chkSyncParty.Text = "Party Sync?";
            // 
            // grpSelectVariable
            // 
            this.grpSelectVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSelectVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSelectVariable.Controls.Add(this.rdoGuildVariable);
            this.grpSelectVariable.Controls.Add(this.rdoInstanceVariable);
            this.grpSelectVariable.Controls.Add(this.rdoPlayerVariable);
            this.grpSelectVariable.Controls.Add(this.cmbVariable);
            this.grpSelectVariable.Controls.Add(this.rdoGlobalVariable);
            this.grpSelectVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSelectVariable.Location = new System.Drawing.Point(9, 29);
            this.grpSelectVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSelectVariable.Name = "grpSelectVariable";
            this.grpSelectVariable.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSelectVariable.Size = new System.Drawing.Size(638, 115);
            this.grpSelectVariable.TabIndex = 40;
            this.grpSelectVariable.TabStop = false;
            this.grpSelectVariable.Text = "Select Variable";
            // 
            // rdoGuildVariable
            // 
            this.rdoGuildVariable.AutoSize = true;
            this.rdoGuildVariable.Location = new System.Drawing.Point(480, 29);
            this.rdoGuildVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoGuildVariable.Name = "rdoGuildVariable";
            this.rdoGuildVariable.Size = new System.Drawing.Size(133, 24);
            this.rdoGuildVariable.TabIndex = 42;
            this.rdoGuildVariable.Text = "Guild Variable";
            // 
            // rdoInstanceVariable
            // 
            this.rdoInstanceVariable.AutoSize = true;
            this.rdoInstanceVariable.Location = new System.Drawing.Point(310, 29);
            this.rdoInstanceVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoInstanceVariable.Name = "rdoInstanceVariable";
            this.rdoInstanceVariable.Size = new System.Drawing.Size(158, 24);
            this.rdoInstanceVariable.TabIndex = 41;
            this.rdoInstanceVariable.Text = "Instance Variable";
            this.rdoInstanceVariable.CheckedChanged += new System.EventHandler(this.ResetForm);
            // 
            // rdoPlayerVariable
            // 
            this.rdoPlayerVariable.AutoSize = true;
            this.rdoPlayerVariable.Checked = true;
            this.rdoPlayerVariable.Location = new System.Drawing.Point(9, 29);
            this.rdoPlayerVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoPlayerVariable.Name = "rdoPlayerVariable";
            this.rdoPlayerVariable.Size = new System.Drawing.Size(139, 24);
            this.rdoPlayerVariable.TabIndex = 34;
            this.rdoPlayerVariable.TabStop = true;
            this.rdoPlayerVariable.Text = "Player Variable";
            this.rdoPlayerVariable.CheckedChanged += new System.EventHandler(this.ResetForm);
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
            this.cmbVariable.Location = new System.Drawing.Point(9, 65);
            this.cmbVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(600, 27);
            this.cmbVariable.TabIndex = 22;
            this.cmbVariable.Text = null;
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariable.SelectedIndexChanged += new System.EventHandler(this.cmbVariable_SelectedIndexChanged);
            // 
            // rdoGlobalVariable
            // 
            this.rdoGlobalVariable.AutoSize = true;
            this.rdoGlobalVariable.Location = new System.Drawing.Point(158, 29);
            this.rdoGlobalVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoGlobalVariable.Name = "rdoGlobalVariable";
            this.rdoGlobalVariable.Size = new System.Drawing.Size(142, 24);
            this.rdoGlobalVariable.TabIndex = 35;
            this.rdoGlobalVariable.Text = "Global Variable";
            this.rdoGlobalVariable.CheckedChanged += new System.EventHandler(this.ResetForm);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(525, 632);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(404, 632);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpStringVariable
            // 
            this.grpStringVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStringVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStringVariable.Controls.Add(this.chkToLower);
            this.grpStringVariable.Controls.Add(this.lblStringTextVariables);
            this.grpStringVariable.Controls.Add(this.grpStringReplace);
            this.grpStringVariable.Controls.Add(this.optReplaceString);
            this.grpStringVariable.Controls.Add(this.optStaticString);
            this.grpStringVariable.Controls.Add(this.grpStringSet);
            this.grpStringVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStringVariable.Location = new System.Drawing.Point(9, 154);
            this.grpStringVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpStringVariable.Name = "grpStringVariable";
            this.grpStringVariable.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpStringVariable.Size = new System.Drawing.Size(444, 308);
            this.grpStringVariable.TabIndex = 51;
            this.grpStringVariable.TabStop = false;
            this.grpStringVariable.Text = "String Variable:";
            this.grpStringVariable.Visible = false;
            // 
            // chkToLower
            // 
            this.chkToLower.AutoSize = true;
            this.chkToLower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkToLower.Location = new System.Drawing.Point(14, 232);
            this.chkToLower.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkToLower.Name = "chkToLower";
            this.chkToLower.Size = new System.Drawing.Size(256, 24);
            this.chkToLower.TabIndex = 69;
            this.chkToLower.Text = "To Lower? (For \"set\" operation)";
            // 
            // lblStringTextVariables
            // 
            this.lblStringTextVariables.AutoSize = true;
            this.lblStringTextVariables.BackColor = System.Drawing.Color.Transparent;
            this.lblStringTextVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStringTextVariables.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblStringTextVariables.Location = new System.Drawing.Point(9, 263);
            this.lblStringTextVariables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringTextVariables.Name = "lblStringTextVariables";
            this.lblStringTextVariables.Size = new System.Drawing.Size(399, 20);
            this.lblStringTextVariables.TabIndex = 68;
            this.lblStringTextVariables.Text = "Text variables work with strings. Click here for a list!";
            this.lblStringTextVariables.Click += new System.EventHandler(this.lblStringTextVariables_Click);
            // 
            // grpStringReplace
            // 
            this.grpStringReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStringReplace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStringReplace.Controls.Add(this.txtStringReplace);
            this.grpStringReplace.Controls.Add(this.txtStringFind);
            this.grpStringReplace.Controls.Add(this.lblStringReplace);
            this.grpStringReplace.Controls.Add(this.lblStringFind);
            this.grpStringReplace.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStringReplace.Location = new System.Drawing.Point(9, 85);
            this.grpStringReplace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpStringReplace.Name = "grpStringReplace";
            this.grpStringReplace.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpStringReplace.Size = new System.Drawing.Size(426, 138);
            this.grpStringReplace.TabIndex = 65;
            this.grpStringReplace.TabStop = false;
            this.grpStringReplace.Text = "Replace";
            // 
            // txtStringReplace
            // 
            this.txtStringReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtStringReplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStringReplace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtStringReplace.Location = new System.Drawing.Point(116, 80);
            this.txtStringReplace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStringReplace.Name = "txtStringReplace";
            this.txtStringReplace.Size = new System.Drawing.Size(300, 26);
            this.txtStringReplace.TabIndex = 64;
            // 
            // txtStringFind
            // 
            this.txtStringFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtStringFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStringFind.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtStringFind.Location = new System.Drawing.Point(116, 31);
            this.txtStringFind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStringFind.Name = "txtStringFind";
            this.txtStringFind.Size = new System.Drawing.Size(300, 26);
            this.txtStringFind.TabIndex = 63;
            // 
            // lblStringReplace
            // 
            this.lblStringReplace.AutoSize = true;
            this.lblStringReplace.Location = new System.Drawing.Point(14, 83);
            this.lblStringReplace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringReplace.Name = "lblStringReplace";
            this.lblStringReplace.Size = new System.Drawing.Size(68, 20);
            this.lblStringReplace.TabIndex = 39;
            this.lblStringReplace.Text = "Replace";
            // 
            // lblStringFind
            // 
            this.lblStringFind.AutoSize = true;
            this.lblStringFind.Location = new System.Drawing.Point(16, 42);
            this.lblStringFind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringFind.Name = "lblStringFind";
            this.lblStringFind.Size = new System.Drawing.Size(40, 20);
            this.lblStringFind.TabIndex = 40;
            this.lblStringFind.Text = "Find";
            // 
            // optReplaceString
            // 
            this.optReplaceString.AutoSize = true;
            this.optReplaceString.Location = new System.Drawing.Point(93, 29);
            this.optReplaceString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optReplaceString.Name = "optReplaceString";
            this.optReplaceString.Size = new System.Drawing.Size(93, 24);
            this.optReplaceString.TabIndex = 63;
            this.optReplaceString.Text = "Replace";
            this.optReplaceString.CheckedChanged += new System.EventHandler(this.optReplaceString_CheckedChanged);
            // 
            // optStaticString
            // 
            this.optStaticString.AutoSize = true;
            this.optStaticString.Location = new System.Drawing.Point(14, 29);
            this.optStaticString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optStaticString.Name = "optStaticString";
            this.optStaticString.Size = new System.Drawing.Size(59, 24);
            this.optStaticString.TabIndex = 51;
            this.optStaticString.Text = "Set";
            this.optStaticString.CheckedChanged += new System.EventHandler(this.optStaticString_CheckedChanged);
            // 
            // grpStringSet
            // 
            this.grpStringSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStringSet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStringSet.Controls.Add(this.lblStringValue);
            this.grpStringSet.Controls.Add(this.txtStringValue);
            this.grpStringSet.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStringSet.Location = new System.Drawing.Point(9, 85);
            this.grpStringSet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpStringSet.Name = "grpStringSet";
            this.grpStringSet.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpStringSet.Size = new System.Drawing.Size(426, 138);
            this.grpStringSet.TabIndex = 67;
            this.grpStringSet.TabStop = false;
            this.grpStringSet.Text = "Set";
            // 
            // lblStringValue
            // 
            this.lblStringValue.AutoSize = true;
            this.lblStringValue.Location = new System.Drawing.Point(9, 43);
            this.lblStringValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStringValue.Name = "lblStringValue";
            this.lblStringValue.Size = new System.Drawing.Size(54, 20);
            this.lblStringValue.TabIndex = 66;
            this.lblStringValue.Text = "Value:";
            // 
            // txtStringValue
            // 
            this.txtStringValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtStringValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStringValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtStringValue.Location = new System.Drawing.Point(106, 38);
            this.txtStringValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(310, 26);
            this.txtStringValue.TabIndex = 62;
            // 
            // grpNumericVariable
            // 
            this.grpNumericVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpNumericVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNumericVariable.Controls.Add(this.optNumericMod);
            this.grpNumericVariable.Controls.Add(this.rdoInventorySlots);
            this.grpNumericVariable.Controls.Add(this.rdoSpawnGroup);
            this.grpNumericVariable.Controls.Add(this.optEventX);
            this.grpNumericVariable.Controls.Add(this.optEventY);
            this.grpNumericVariable.Controls.Add(this.optNumericRightShift);
            this.grpNumericVariable.Controls.Add(this.optNumericLeftShift);
            this.grpNumericVariable.Controls.Add(this.optPlayerY);
            this.grpNumericVariable.Controls.Add(this.optNumericDivide);
            this.grpNumericVariable.Controls.Add(this.optPlayerX);
            this.grpNumericVariable.Controls.Add(this.optNumericMultiply);
            this.grpNumericVariable.Controls.Add(this.optPlayerLevel);
            this.grpNumericVariable.Controls.Add(this.optNumericSet);
            this.grpNumericVariable.Controls.Add(this.optNumericAdd);
            this.grpNumericVariable.Controls.Add(this.optNumericRandom);
            this.grpNumericVariable.Controls.Add(this.optNumericSubtract);
            this.grpNumericVariable.Controls.Add(this.optNumericSystemTime);
            this.grpNumericVariable.Controls.Add(this.grpNumericValues);
            this.grpNumericVariable.Controls.Add(this.grpNumericRandom);
            this.grpNumericVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNumericVariable.Location = new System.Drawing.Point(9, 154);
            this.grpNumericVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNumericVariable.Name = "grpNumericVariable";
            this.grpNumericVariable.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNumericVariable.Size = new System.Drawing.Size(628, 469);
            this.grpNumericVariable.TabIndex = 36;
            this.grpNumericVariable.TabStop = false;
            this.grpNumericVariable.Text = "Numeric Variable:";
            // 
            // optNumericMod
            // 
            this.optNumericMod.AutoSize = true;
            this.optNumericMod.Location = new System.Drawing.Point(444, 29);
            this.optNumericMod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericMod.Name = "optNumericMod";
            this.optNumericMod.Size = new System.Drawing.Size(86, 24);
            this.optNumericMod.TabIndex = 57;
            this.optNumericMod.Text = "Modulo";
            this.optNumericMod.CheckedChanged += new System.EventHandler(this.rdoMod_CheckedChanged);
            // 
            // rdoInventorySlots
            // 
            this.rdoInventorySlots.AutoSize = true;
            this.rdoInventorySlots.Location = new System.Drawing.Point(291, 138);
            this.rdoInventorySlots.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoInventorySlots.Name = "rdoInventorySlots";
            this.rdoInventorySlots.Size = new System.Drawing.Size(142, 24);
            this.rdoInventorySlots.TabIndex = 56;
            this.rdoInventorySlots.Text = "Open Inv. Slots";
            this.rdoInventorySlots.CheckedChanged += new System.EventHandler(this.rdoInventorySlots_CheckedChanged);
            // 
            // rdoSpawnGroup
            // 
            this.rdoSpawnGroup.AutoSize = true;
            this.rdoSpawnGroup.Location = new System.Drawing.Point(147, 138);
            this.rdoSpawnGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoSpawnGroup.Name = "rdoSpawnGroup";
            this.rdoSpawnGroup.Size = new System.Drawing.Size(132, 24);
            this.rdoSpawnGroup.TabIndex = 55;
            this.rdoSpawnGroup.Text = "Spawn Group";
            this.rdoSpawnGroup.CheckedChanged += new System.EventHandler(this.rdoSpawnGroup_CheckedChanged);
            // 
            // optEventX
            // 
            this.optEventX.AutoSize = true;
            this.optEventX.Location = new System.Drawing.Point(254, 103);
            this.optEventX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optEventX.Name = "optEventX";
            this.optEventX.Size = new System.Drawing.Size(90, 24);
            this.optEventX.TabIndex = 53;
            this.optEventX.Text = "Event X";
            this.optEventX.CheckedChanged += new System.EventHandler(this.optEventX_CheckedChanged);
            // 
            // optEventY
            // 
            this.optEventY.AutoSize = true;
            this.optEventY.Location = new System.Drawing.Point(357, 103);
            this.optEventY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optEventY.Name = "optEventY";
            this.optEventY.Size = new System.Drawing.Size(90, 24);
            this.optEventY.TabIndex = 54;
            this.optEventY.Text = "Event Y";
            this.optEventY.CheckedChanged += new System.EventHandler(this.optEventY_CheckedChanged);
            // 
            // optNumericRightShift
            // 
            this.optNumericRightShift.AutoSize = true;
            this.optNumericRightShift.Location = new System.Drawing.Point(93, 65);
            this.optNumericRightShift.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericRightShift.Name = "optNumericRightShift";
            this.optNumericRightShift.Size = new System.Drawing.Size(79, 24);
            this.optNumericRightShift.TabIndex = 43;
            this.optNumericRightShift.Text = "RShift";
            this.optNumericRightShift.CheckedChanged += new System.EventHandler(this.optNumericRightShift_CheckedChanged);
            // 
            // optNumericLeftShift
            // 
            this.optNumericLeftShift.AutoSize = true;
            this.optNumericLeftShift.Location = new System.Drawing.Point(14, 65);
            this.optNumericLeftShift.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericLeftShift.Name = "optNumericLeftShift";
            this.optNumericLeftShift.Size = new System.Drawing.Size(76, 24);
            this.optNumericLeftShift.TabIndex = 42;
            this.optNumericLeftShift.Text = "LShift";
            this.optNumericLeftShift.CheckedChanged += new System.EventHandler(this.optNumericLeftShift_CheckedChanged);
            // 
            // optPlayerY
            // 
            this.optPlayerY.AutoSize = true;
            this.optPlayerY.Location = new System.Drawing.Point(132, 103);
            this.optPlayerY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optPlayerY.Name = "optPlayerY";
            this.optPlayerY.Size = new System.Drawing.Size(92, 24);
            this.optPlayerY.TabIndex = 52;
            this.optPlayerY.Text = "Player Y";
            this.optPlayerY.CheckedChanged += new System.EventHandler(this.optPlayerY_CheckedChanged);
            // 
            // optNumericDivide
            // 
            this.optNumericDivide.AutoSize = true;
            this.optNumericDivide.Location = new System.Drawing.Point(352, 29);
            this.optNumericDivide.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericDivide.Name = "optNumericDivide";
            this.optNumericDivide.Size = new System.Drawing.Size(77, 24);
            this.optNumericDivide.TabIndex = 41;
            this.optNumericDivide.Text = "Divide";
            this.optNumericDivide.CheckedChanged += new System.EventHandler(this.optNumericDivide_CheckedChanged);
            // 
            // optPlayerX
            // 
            this.optPlayerX.AutoSize = true;
            this.optPlayerX.Location = new System.Drawing.Point(14, 103);
            this.optPlayerX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optPlayerX.Name = "optPlayerX";
            this.optPlayerX.Size = new System.Drawing.Size(92, 24);
            this.optPlayerX.TabIndex = 51;
            this.optPlayerX.Text = "Player X";
            this.optPlayerX.CheckedChanged += new System.EventHandler(this.optPlayerX_CheckedChanged);
            // 
            // optNumericMultiply
            // 
            this.optNumericMultiply.AutoSize = true;
            this.optNumericMultiply.Location = new System.Drawing.Point(254, 29);
            this.optNumericMultiply.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericMultiply.Name = "optNumericMultiply";
            this.optNumericMultiply.Size = new System.Drawing.Size(86, 24);
            this.optNumericMultiply.TabIndex = 40;
            this.optNumericMultiply.Text = "Multiply";
            this.optNumericMultiply.CheckedChanged += new System.EventHandler(this.optNumericMultiply_CheckedChanged);
            // 
            // optPlayerLevel
            // 
            this.optPlayerLevel.AutoSize = true;
            this.optPlayerLevel.Location = new System.Drawing.Point(14, 138);
            this.optPlayerLevel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optPlayerLevel.Name = "optPlayerLevel";
            this.optPlayerLevel.Size = new System.Drawing.Size(118, 24);
            this.optPlayerLevel.TabIndex = 50;
            this.optPlayerLevel.Text = "Player Level";
            this.optPlayerLevel.CheckedChanged += new System.EventHandler(this.optPlayerLevel_CheckedChanged);
            // 
            // optNumericSet
            // 
            this.optNumericSet.AutoSize = true;
            this.optNumericSet.Location = new System.Drawing.Point(14, 29);
            this.optNumericSet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericSet.Name = "optNumericSet";
            this.optNumericSet.Size = new System.Drawing.Size(59, 24);
            this.optNumericSet.TabIndex = 26;
            this.optNumericSet.Text = "Set";
            this.optNumericSet.CheckedChanged += new System.EventHandler(this.optNumericSet_CheckedChanged);
            // 
            // optNumericAdd
            // 
            this.optNumericAdd.AutoSize = true;
            this.optNumericAdd.Location = new System.Drawing.Point(75, 29);
            this.optNumericAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericAdd.Name = "optNumericAdd";
            this.optNumericAdd.Size = new System.Drawing.Size(63, 24);
            this.optNumericAdd.TabIndex = 25;
            this.optNumericAdd.Text = "Add";
            this.optNumericAdd.CheckedChanged += new System.EventHandler(this.optNumericAdd_CheckedChanged);
            // 
            // optNumericRandom
            // 
            this.optNumericRandom.AutoSize = true;
            this.optNumericRandom.Location = new System.Drawing.Point(174, 65);
            this.optNumericRandom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericRandom.Name = "optNumericRandom";
            this.optNumericRandom.Size = new System.Drawing.Size(95, 24);
            this.optNumericRandom.TabIndex = 23;
            this.optNumericRandom.Text = "Random";
            this.optNumericRandom.CheckedChanged += new System.EventHandler(this.optNumericRandom_CheckedChanged);
            // 
            // optNumericSubtract
            // 
            this.optNumericSubtract.AutoSize = true;
            this.optNumericSubtract.Location = new System.Drawing.Point(147, 29);
            this.optNumericSubtract.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericSubtract.Name = "optNumericSubtract";
            this.optNumericSubtract.Size = new System.Drawing.Size(95, 24);
            this.optNumericSubtract.TabIndex = 24;
            this.optNumericSubtract.Text = "Subtract";
            this.optNumericSubtract.CheckedChanged += new System.EventHandler(this.optNumericSubtract_CheckedChanged);
            // 
            // optNumericSystemTime
            // 
            this.optNumericSystemTime.Location = new System.Drawing.Point(272, 65);
            this.optNumericSystemTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericSystemTime.Name = "optNumericSystemTime";
            this.optNumericSystemTime.Size = new System.Drawing.Size(164, 26);
            this.optNumericSystemTime.TabIndex = 39;
            this.optNumericSystemTime.Text = "System Time (Ms)";
            this.optNumericSystemTime.CheckedChanged += new System.EventHandler(this.optNumericSystemTime_CheckedChanged);
            // 
            // grpNumericValues
            // 
            this.grpNumericValues.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpNumericValues.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNumericValues.Controls.Add(this.cmbGuildVarNum);
            this.grpNumericValues.Controls.Add(this.rdoGuildVarNum);
            this.grpNumericValues.Controls.Add(this.cmbNumericCloneInstanceVar);
            this.grpNumericValues.Controls.Add(this.optNumericCloneInstanceVar);
            this.grpNumericValues.Controls.Add(this.nudNumericValue);
            this.grpNumericValues.Controls.Add(this.cmbNumericCloneGlobalVar);
            this.grpNumericValues.Controls.Add(this.cmbNumericClonePlayerVar);
            this.grpNumericValues.Controls.Add(this.optNumericCloneGlobalVar);
            this.grpNumericValues.Controls.Add(this.optNumericClonePlayerVar);
            this.grpNumericValues.Controls.Add(this.optNumericStaticVal);
            this.grpNumericValues.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNumericValues.Location = new System.Drawing.Point(9, 174);
            this.grpNumericValues.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNumericValues.Name = "grpNumericValues";
            this.grpNumericValues.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNumericValues.Size = new System.Drawing.Size(610, 286);
            this.grpNumericValues.TabIndex = 37;
            this.grpNumericValues.TabStop = false;
            // 
            // cmbGuildVarNum
            // 
            this.cmbGuildVarNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbGuildVarNum.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbGuildVarNum.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbGuildVarNum.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbGuildVarNum.DrawDropdownHoverOutline = false;
            this.cmbGuildVarNum.DrawFocusRectangle = false;
            this.cmbGuildVarNum.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGuildVarNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGuildVarNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGuildVarNum.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbGuildVarNum.FormattingEnabled = true;
            this.cmbGuildVarNum.Location = new System.Drawing.Point(244, 214);
            this.cmbGuildVarNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbGuildVarNum.Name = "cmbGuildVarNum";
            this.cmbGuildVarNum.Size = new System.Drawing.Size(355, 27);
            this.cmbGuildVarNum.TabIndex = 51;
            this.cmbGuildVarNum.Text = null;
            this.cmbGuildVarNum.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // rdoGuildVarNum
            // 
            this.rdoGuildVarNum.AutoSize = true;
            this.rdoGuildVarNum.Location = new System.Drawing.Point(9, 215);
            this.rdoGuildVarNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoGuildVarNum.Name = "rdoGuildVarNum";
            this.rdoGuildVarNum.Size = new System.Drawing.Size(178, 24);
            this.rdoGuildVarNum.TabIndex = 50;
            this.rdoGuildVarNum.Text = "Guild Variable Value";
            this.rdoGuildVarNum.CheckedChanged += new System.EventHandler(this.rdoGuildVarNum_CheckedChanged);
            // 
            // cmbNumericCloneInstanceVar
            // 
            this.cmbNumericCloneInstanceVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNumericCloneInstanceVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNumericCloneInstanceVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNumericCloneInstanceVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNumericCloneInstanceVar.DrawDropdownHoverOutline = false;
            this.cmbNumericCloneInstanceVar.DrawFocusRectangle = false;
            this.cmbNumericCloneInstanceVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNumericCloneInstanceVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumericCloneInstanceVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNumericCloneInstanceVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNumericCloneInstanceVar.FormattingEnabled = true;
            this.cmbNumericCloneInstanceVar.Location = new System.Drawing.Point(244, 157);
            this.cmbNumericCloneInstanceVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbNumericCloneInstanceVar.Name = "cmbNumericCloneInstanceVar";
            this.cmbNumericCloneInstanceVar.Size = new System.Drawing.Size(355, 27);
            this.cmbNumericCloneInstanceVar.TabIndex = 49;
            this.cmbNumericCloneInstanceVar.Text = null;
            this.cmbNumericCloneInstanceVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optNumericCloneInstanceVar
            // 
            this.optNumericCloneInstanceVar.AutoSize = true;
            this.optNumericCloneInstanceVar.Location = new System.Drawing.Point(9, 160);
            this.optNumericCloneInstanceVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericCloneInstanceVar.Name = "optNumericCloneInstanceVar";
            this.optNumericCloneInstanceVar.Size = new System.Drawing.Size(207, 24);
            this.optNumericCloneInstanceVar.TabIndex = 48;
            this.optNumericCloneInstanceVar.Text = "Instance Variable Value:";
            this.optNumericCloneInstanceVar.CheckedChanged += new System.EventHandler(this.optNumericCloneInstanceVar_CheckedChanged);
            // 
            // nudNumericValue
            // 
            this.nudNumericValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudNumericValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudNumericValue.Location = new System.Drawing.Point(244, 14);
            this.nudNumericValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudNumericValue.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudNumericValue.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudNumericValue.Name = "nudNumericValue";
            this.nudNumericValue.Size = new System.Drawing.Size(357, 26);
            this.nudNumericValue.TabIndex = 47;
            this.nudNumericValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // cmbNumericCloneGlobalVar
            // 
            this.cmbNumericCloneGlobalVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNumericCloneGlobalVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNumericCloneGlobalVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNumericCloneGlobalVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNumericCloneGlobalVar.DrawDropdownHoverOutline = false;
            this.cmbNumericCloneGlobalVar.DrawFocusRectangle = false;
            this.cmbNumericCloneGlobalVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNumericCloneGlobalVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumericCloneGlobalVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNumericCloneGlobalVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNumericCloneGlobalVar.FormattingEnabled = true;
            this.cmbNumericCloneGlobalVar.Location = new System.Drawing.Point(244, 108);
            this.cmbNumericCloneGlobalVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbNumericCloneGlobalVar.Name = "cmbNumericCloneGlobalVar";
            this.cmbNumericCloneGlobalVar.Size = new System.Drawing.Size(355, 27);
            this.cmbNumericCloneGlobalVar.TabIndex = 45;
            this.cmbNumericCloneGlobalVar.Text = null;
            this.cmbNumericCloneGlobalVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbNumericClonePlayerVar
            // 
            this.cmbNumericClonePlayerVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNumericClonePlayerVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNumericClonePlayerVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNumericClonePlayerVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNumericClonePlayerVar.DrawDropdownHoverOutline = false;
            this.cmbNumericClonePlayerVar.DrawFocusRectangle = false;
            this.cmbNumericClonePlayerVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNumericClonePlayerVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumericClonePlayerVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNumericClonePlayerVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNumericClonePlayerVar.FormattingEnabled = true;
            this.cmbNumericClonePlayerVar.Location = new System.Drawing.Point(244, 58);
            this.cmbNumericClonePlayerVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbNumericClonePlayerVar.Name = "cmbNumericClonePlayerVar";
            this.cmbNumericClonePlayerVar.Size = new System.Drawing.Size(355, 27);
            this.cmbNumericClonePlayerVar.TabIndex = 44;
            this.cmbNumericClonePlayerVar.Text = null;
            this.cmbNumericClonePlayerVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optNumericCloneGlobalVar
            // 
            this.optNumericCloneGlobalVar.AutoSize = true;
            this.optNumericCloneGlobalVar.Location = new System.Drawing.Point(9, 108);
            this.optNumericCloneGlobalVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericCloneGlobalVar.Name = "optNumericCloneGlobalVar";
            this.optNumericCloneGlobalVar.Size = new System.Drawing.Size(191, 24);
            this.optNumericCloneGlobalVar.TabIndex = 43;
            this.optNumericCloneGlobalVar.Text = "Global Variable Value:";
            this.optNumericCloneGlobalVar.CheckedChanged += new System.EventHandler(this.optNumericCloneGlobalVar_CheckedChanged);
            // 
            // optNumericClonePlayerVar
            // 
            this.optNumericClonePlayerVar.AutoSize = true;
            this.optNumericClonePlayerVar.Location = new System.Drawing.Point(9, 58);
            this.optNumericClonePlayerVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericClonePlayerVar.Name = "optNumericClonePlayerVar";
            this.optNumericClonePlayerVar.Size = new System.Drawing.Size(188, 24);
            this.optNumericClonePlayerVar.TabIndex = 42;
            this.optNumericClonePlayerVar.Text = "Player Variable Value:";
            this.optNumericClonePlayerVar.CheckedChanged += new System.EventHandler(this.optNumericClonePlayerVar_CheckedChanged);
            // 
            // optNumericStaticVal
            // 
            this.optNumericStaticVal.AutoSize = true;
            this.optNumericStaticVal.Checked = true;
            this.optNumericStaticVal.Location = new System.Drawing.Point(9, 14);
            this.optNumericStaticVal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optNumericStaticVal.Name = "optNumericStaticVal";
            this.optNumericStaticVal.Size = new System.Drawing.Size(124, 24);
            this.optNumericStaticVal.TabIndex = 46;
            this.optNumericStaticVal.TabStop = true;
            this.optNumericStaticVal.Text = "Static Value:";
            // 
            // grpNumericRandom
            // 
            this.grpNumericRandom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpNumericRandom.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNumericRandom.Controls.Add(this.nudHigh);
            this.grpNumericRandom.Controls.Add(this.nudLow);
            this.grpNumericRandom.Controls.Add(this.lblNumericRandomHigh);
            this.grpNumericRandom.Controls.Add(this.lblNumericRandomLow);
            this.grpNumericRandom.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNumericRandom.Location = new System.Drawing.Point(9, 174);
            this.grpNumericRandom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNumericRandom.Name = "grpNumericRandom";
            this.grpNumericRandom.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNumericRandom.Size = new System.Drawing.Size(426, 149);
            this.grpNumericRandom.TabIndex = 39;
            this.grpNumericRandom.TabStop = false;
            this.grpNumericRandom.Text = "Random Number:";
            // 
            // nudHigh
            // 
            this.nudHigh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHigh.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHigh.Location = new System.Drawing.Point(66, 78);
            this.nudHigh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudHigh.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudHigh.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudHigh.Name = "nudHigh";
            this.nudHigh.Size = new System.Drawing.Size(336, 26);
            this.nudHigh.TabIndex = 42;
            this.nudHigh.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // nudLow
            // 
            this.nudLow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudLow.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudLow.Location = new System.Drawing.Point(66, 38);
            this.nudLow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudLow.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudLow.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudLow.Name = "nudLow";
            this.nudLow.Size = new System.Drawing.Size(336, 26);
            this.nudLow.TabIndex = 41;
            this.nudLow.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblNumericRandomHigh
            // 
            this.lblNumericRandomHigh.AutoSize = true;
            this.lblNumericRandomHigh.Location = new System.Drawing.Point(14, 83);
            this.lblNumericRandomHigh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumericRandomHigh.Name = "lblNumericRandomHigh";
            this.lblNumericRandomHigh.Size = new System.Drawing.Size(42, 20);
            this.lblNumericRandomHigh.TabIndex = 39;
            this.lblNumericRandomHigh.Text = "High";
            // 
            // lblNumericRandomLow
            // 
            this.lblNumericRandomLow.AutoSize = true;
            this.lblNumericRandomLow.Location = new System.Drawing.Point(16, 42);
            this.lblNumericRandomLow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumericRandomLow.Name = "lblNumericRandomLow";
            this.lblNumericRandomLow.Size = new System.Drawing.Size(38, 20);
            this.lblNumericRandomLow.TabIndex = 40;
            this.lblNumericRandomLow.Text = "Low";
            // 
            // grpBooleanVariable
            // 
            this.grpBooleanVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpBooleanVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBooleanVariable.Controls.Add(this.optBooleanToggle);
            this.grpBooleanVariable.Controls.Add(this.cmbGuildVarBool);
            this.grpBooleanVariable.Controls.Add(this.rdoGuildVarBool);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanInstanceGlobalVar);
            this.grpBooleanVariable.Controls.Add(this.optBooleanCloneInstanceVar);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanCloneGlobalVar);
            this.grpBooleanVariable.Controls.Add(this.cmbBooleanClonePlayerVar);
            this.grpBooleanVariable.Controls.Add(this.optBooleanCloneGlobalVar);
            this.grpBooleanVariable.Controls.Add(this.optBooleanClonePlayerVar);
            this.grpBooleanVariable.Controls.Add(this.optBooleanTrue);
            this.grpBooleanVariable.Controls.Add(this.optBooleanFalse);
            this.grpBooleanVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBooleanVariable.Location = new System.Drawing.Point(9, 154);
            this.grpBooleanVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBooleanVariable.Name = "grpBooleanVariable";
            this.grpBooleanVariable.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpBooleanVariable.Size = new System.Drawing.Size(620, 308);
            this.grpBooleanVariable.TabIndex = 40;
            this.grpBooleanVariable.TabStop = false;
            this.grpBooleanVariable.Text = "Boolean Variable:";
            // 
            // cmbGuildVarBool
            // 
            this.cmbGuildVarBool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbGuildVarBool.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbGuildVarBool.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbGuildVarBool.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbGuildVarBool.DrawDropdownHoverOutline = false;
            this.cmbGuildVarBool.DrawFocusRectangle = false;
            this.cmbGuildVarBool.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGuildVarBool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGuildVarBool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGuildVarBool.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbGuildVarBool.FormattingEnabled = true;
            this.cmbGuildVarBool.Location = new System.Drawing.Point(232, 257);
            this.cmbGuildVarBool.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbGuildVarBool.Name = "cmbGuildVarBool";
            this.cmbGuildVarBool.Size = new System.Drawing.Size(360, 27);
            this.cmbGuildVarBool.TabIndex = 53;
            this.cmbGuildVarBool.Text = null;
            this.cmbGuildVarBool.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // rdoGuildVarBool
            // 
            this.rdoGuildVarBool.AutoSize = true;
            this.rdoGuildVarBool.Location = new System.Drawing.Point(14, 257);
            this.rdoGuildVarBool.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoGuildVarBool.Name = "rdoGuildVarBool";
            this.rdoGuildVarBool.Size = new System.Drawing.Size(182, 24);
            this.rdoGuildVarBool.TabIndex = 52;
            this.rdoGuildVarBool.Text = "Guild Variable Value:";
            // 
            // cmbBooleanInstanceGlobalVar
            // 
            this.cmbBooleanInstanceGlobalVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanInstanceGlobalVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanInstanceGlobalVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanInstanceGlobalVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanInstanceGlobalVar.DrawDropdownHoverOutline = false;
            this.cmbBooleanInstanceGlobalVar.DrawFocusRectangle = false;
            this.cmbBooleanInstanceGlobalVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanInstanceGlobalVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanInstanceGlobalVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanInstanceGlobalVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanInstanceGlobalVar.FormattingEnabled = true;
            this.cmbBooleanInstanceGlobalVar.Location = new System.Drawing.Point(232, 211);
            this.cmbBooleanInstanceGlobalVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbBooleanInstanceGlobalVar.Name = "cmbBooleanInstanceGlobalVar";
            this.cmbBooleanInstanceGlobalVar.Size = new System.Drawing.Size(360, 27);
            this.cmbBooleanInstanceGlobalVar.TabIndex = 51;
            this.cmbBooleanInstanceGlobalVar.Text = null;
            this.cmbBooleanInstanceGlobalVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optBooleanCloneInstanceVar
            // 
            this.optBooleanCloneInstanceVar.AutoSize = true;
            this.optBooleanCloneInstanceVar.Location = new System.Drawing.Point(14, 212);
            this.optBooleanCloneInstanceVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optBooleanCloneInstanceVar.Name = "optBooleanCloneInstanceVar";
            this.optBooleanCloneInstanceVar.Size = new System.Drawing.Size(207, 24);
            this.optBooleanCloneInstanceVar.TabIndex = 50;
            this.optBooleanCloneInstanceVar.Text = "Instance Variable Value:";
            // 
            // cmbBooleanCloneGlobalVar
            // 
            this.cmbBooleanCloneGlobalVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanCloneGlobalVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanCloneGlobalVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanCloneGlobalVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanCloneGlobalVar.DrawDropdownHoverOutline = false;
            this.cmbBooleanCloneGlobalVar.DrawFocusRectangle = false;
            this.cmbBooleanCloneGlobalVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanCloneGlobalVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanCloneGlobalVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanCloneGlobalVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanCloneGlobalVar.FormattingEnabled = true;
            this.cmbBooleanCloneGlobalVar.Location = new System.Drawing.Point(232, 158);
            this.cmbBooleanCloneGlobalVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbBooleanCloneGlobalVar.Name = "cmbBooleanCloneGlobalVar";
            this.cmbBooleanCloneGlobalVar.Size = new System.Drawing.Size(360, 27);
            this.cmbBooleanCloneGlobalVar.TabIndex = 49;
            this.cmbBooleanCloneGlobalVar.Text = null;
            this.cmbBooleanCloneGlobalVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbBooleanClonePlayerVar
            // 
            this.cmbBooleanClonePlayerVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBooleanClonePlayerVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBooleanClonePlayerVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBooleanClonePlayerVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBooleanClonePlayerVar.DrawDropdownHoverOutline = false;
            this.cmbBooleanClonePlayerVar.DrawFocusRectangle = false;
            this.cmbBooleanClonePlayerVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBooleanClonePlayerVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBooleanClonePlayerVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBooleanClonePlayerVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBooleanClonePlayerVar.FormattingEnabled = true;
            this.cmbBooleanClonePlayerVar.Location = new System.Drawing.Point(232, 108);
            this.cmbBooleanClonePlayerVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbBooleanClonePlayerVar.Name = "cmbBooleanClonePlayerVar";
            this.cmbBooleanClonePlayerVar.Size = new System.Drawing.Size(360, 27);
            this.cmbBooleanClonePlayerVar.TabIndex = 48;
            this.cmbBooleanClonePlayerVar.Text = null;
            this.cmbBooleanClonePlayerVar.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // optBooleanCloneGlobalVar
            // 
            this.optBooleanCloneGlobalVar.AutoSize = true;
            this.optBooleanCloneGlobalVar.Location = new System.Drawing.Point(14, 158);
            this.optBooleanCloneGlobalVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optBooleanCloneGlobalVar.Name = "optBooleanCloneGlobalVar";
            this.optBooleanCloneGlobalVar.Size = new System.Drawing.Size(191, 24);
            this.optBooleanCloneGlobalVar.TabIndex = 47;
            this.optBooleanCloneGlobalVar.Text = "Global Variable Value:";
            // 
            // optBooleanClonePlayerVar
            // 
            this.optBooleanClonePlayerVar.AutoSize = true;
            this.optBooleanClonePlayerVar.Location = new System.Drawing.Point(14, 109);
            this.optBooleanClonePlayerVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optBooleanClonePlayerVar.Name = "optBooleanClonePlayerVar";
            this.optBooleanClonePlayerVar.Size = new System.Drawing.Size(188, 24);
            this.optBooleanClonePlayerVar.TabIndex = 46;
            this.optBooleanClonePlayerVar.Text = "Player Variable Value:";
            // 
            // optBooleanTrue
            // 
            this.optBooleanTrue.AutoSize = true;
            this.optBooleanTrue.Location = new System.Drawing.Point(14, 29);
            this.optBooleanTrue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optBooleanTrue.Name = "optBooleanTrue";
            this.optBooleanTrue.Size = new System.Drawing.Size(66, 24);
            this.optBooleanTrue.TabIndex = 26;
            this.optBooleanTrue.Text = "True";
            // 
            // optBooleanFalse
            // 
            this.optBooleanFalse.AutoSize = true;
            this.optBooleanFalse.Location = new System.Drawing.Point(14, 68);
            this.optBooleanFalse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optBooleanFalse.Name = "optBooleanFalse";
            this.optBooleanFalse.Size = new System.Drawing.Size(73, 24);
            this.optBooleanFalse.TabIndex = 25;
            this.optBooleanFalse.Text = "False";
            // 
            // optBooleanToggle
            // 
            this.optBooleanToggle.AutoSize = true;
            this.optBooleanToggle.Location = new System.Drawing.Point(93, 29);
            this.optBooleanToggle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.optBooleanToggle.Name = "optBooleanToggle";
            this.optBooleanToggle.Size = new System.Drawing.Size(82, 24);
            this.optBooleanToggle.TabIndex = 54;
            this.optBooleanToggle.Text = "Toggle";
            // 
            // EventCommandVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpSetVariable);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "EventCommandVariable";
            this.Size = new System.Drawing.Size(670, 694);
            this.grpSetVariable.ResumeLayout(false);
            this.grpSetVariable.PerformLayout();
            this.grpSelectVariable.ResumeLayout(false);
            this.grpSelectVariable.PerformLayout();
            this.grpStringVariable.ResumeLayout(false);
            this.grpStringVariable.PerformLayout();
            this.grpStringReplace.ResumeLayout(false);
            this.grpStringReplace.PerformLayout();
            this.grpStringSet.ResumeLayout(false);
            this.grpStringSet.PerformLayout();
            this.grpNumericVariable.ResumeLayout(false);
            this.grpNumericVariable.PerformLayout();
            this.grpNumericValues.ResumeLayout(false);
            this.grpNumericValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumericValue)).EndInit();
            this.grpNumericRandom.ResumeLayout(false);
            this.grpNumericRandom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLow)).EndInit();
            this.grpBooleanVariable.ResumeLayout(false);
            this.grpBooleanVariable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpSetVariable;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        internal DarkRadioButton optNumericRandom;
        internal DarkRadioButton optNumericSubtract;
        internal DarkRadioButton optNumericAdd;
        internal DarkRadioButton optNumericSet;
        internal DarkComboBox cmbVariable;
        private DarkRadioButton rdoGlobalVariable;
        private DarkRadioButton rdoPlayerVariable;
        private DarkGroupBox grpNumericVariable;
        internal DarkRadioButton optNumericSystemTime;
        private DarkGroupBox grpNumericValues;
        private DarkNumericUpDown nudNumericValue;
        internal DarkRadioButton optNumericStaticVal;
        internal DarkComboBox cmbNumericCloneGlobalVar;
        internal DarkComboBox cmbNumericClonePlayerVar;
        internal DarkRadioButton optNumericCloneGlobalVar;
        internal DarkRadioButton optNumericClonePlayerVar;
        private DarkGroupBox grpNumericRandom;
        private DarkNumericUpDown nudHigh;
        private DarkNumericUpDown nudLow;
        internal System.Windows.Forms.Label lblNumericRandomHigh;
        internal System.Windows.Forms.Label lblNumericRandomLow;
        private DarkCheckBox chkSyncParty;
        private DarkGroupBox grpSelectVariable;
        private DarkGroupBox grpBooleanVariable;
        internal DarkComboBox cmbBooleanCloneGlobalVar;
        internal DarkComboBox cmbBooleanClonePlayerVar;
        internal DarkRadioButton optBooleanCloneGlobalVar;
        internal DarkRadioButton optBooleanClonePlayerVar;
        internal DarkRadioButton optBooleanTrue;
        internal DarkRadioButton optBooleanFalse;
        private DarkGroupBox grpStringVariable;
        private DarkTextBox txtStringValue;
        internal System.Windows.Forms.Label lblStringValue;
        private DarkGroupBox grpStringReplace;
        private DarkTextBox txtStringReplace;
        private DarkTextBox txtStringFind;
        internal System.Windows.Forms.Label lblStringReplace;
        internal System.Windows.Forms.Label lblStringFind;
        private DarkRadioButton optReplaceString;
        private DarkRadioButton optStaticString;
        private DarkGroupBox grpStringSet;
        private System.Windows.Forms.Label lblStringTextVariables;
        internal DarkRadioButton optNumericRightShift;
        internal DarkRadioButton optNumericLeftShift;
        internal DarkRadioButton optNumericDivide;
        internal DarkRadioButton optNumericMultiply;
        private DarkRadioButton rdoInstanceVariable;
        internal DarkComboBox cmbNumericCloneInstanceVar;
        internal DarkRadioButton optNumericCloneInstanceVar;
        internal DarkComboBox cmbBooleanInstanceGlobalVar;
        internal DarkRadioButton optBooleanCloneInstanceVar;
        internal DarkRadioButton optEventY;
        internal DarkRadioButton optEventX;
        internal DarkRadioButton optPlayerY;
        internal DarkRadioButton optPlayerX;
        internal DarkRadioButton optPlayerLevel;
        internal DarkRadioButton rdoSpawnGroup;
        internal DarkRadioButton rdoInventorySlots;
        private DarkCheckBox chkInstanceSync;
        internal DarkComboBox cmbGuildVarBool;
        internal DarkRadioButton rdoGuildVarBool;
        internal DarkComboBox cmbGuildVarNum;
        internal DarkRadioButton rdoGuildVarNum;
        private DarkRadioButton rdoGuildVariable;
        internal DarkRadioButton optNumericMod;
        private DarkCheckBox chkToLower;
        internal DarkRadioButton optBooleanToggle;
    }
}
