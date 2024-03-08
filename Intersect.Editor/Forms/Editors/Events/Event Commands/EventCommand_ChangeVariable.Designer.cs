namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeVariable
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
            this.grpSelectVariable = new DarkUI.Controls.DarkGroupBox();
            this.lblCurrentSelection = new System.Windows.Forms.Label();
            this.lblSetValue = new System.Windows.Forms.Label();
            this.btnSetVariableSelect = new DarkUI.Controls.DarkButton();
            this.grpSetVariableEvent = new DarkUI.Controls.DarkGroupBox();
            this.grpSetToVariable = new DarkUI.Controls.DarkGroupBox();
            this.btnSettingVariableSelect = new DarkUI.Controls.DarkButton();
            this.lblSettingVariableValue = new System.Windows.Forms.Label();
            this.lblCurrentSelection2 = new System.Windows.Forms.Label();
            this.grpSetMode = new DarkUI.Controls.DarkGroupBox();
            this.rdoSetToValue = new DarkUI.Controls.DarkRadioButton();
            this.rdoSetToVariable = new DarkUI.Controls.DarkRadioButton();
            this.grpIntegerSet = new DarkUI.Controls.DarkGroupBox();
            this.grpOperator = new DarkUI.Controls.DarkGroupBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.rdoIntStatic = new DarkUI.Controls.DarkRadioButton();
            this.rdoIntDynamic = new DarkUI.Controls.DarkRadioButton();
            this.nudValue = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbOperator = new DarkUI.Controls.DarkComboBox();
            this.cmbIntDynamicOptions = new DarkUI.Controls.DarkComboBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.chkInstanceSync = new DarkUI.Controls.DarkCheckBox();
            this.chkSyncParty = new DarkUI.Controls.DarkCheckBox();
            this.grpSelectVariable.SuspendLayout();
            this.grpSetVariableEvent.SuspendLayout();
            this.grpSetToVariable.SuspendLayout();
            this.grpSetMode.SuspendLayout();
            this.grpIntegerSet.SuspendLayout();
            this.grpOperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSelectVariable
            // 
            this.grpSelectVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSelectVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSelectVariable.Controls.Add(this.btnSetVariableSelect);
            this.grpSelectVariable.Controls.Add(this.lblSetValue);
            this.grpSelectVariable.Controls.Add(this.lblCurrentSelection);
            this.grpSelectVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSelectVariable.Location = new System.Drawing.Point(6, 22);
            this.grpSelectVariable.Name = "grpSelectVariable";
            this.grpSelectVariable.Size = new System.Drawing.Size(362, 77);
            this.grpSelectVariable.TabIndex = 41;
            this.grpSelectVariable.TabStop = false;
            this.grpSelectVariable.Text = "Select Variable";
            // 
            // lblCurrentSelection
            // 
            this.lblCurrentSelection.AutoSize = true;
            this.lblCurrentSelection.Location = new System.Drawing.Point(6, 45);
            this.lblCurrentSelection.Name = "lblCurrentSelection";
            this.lblCurrentSelection.Size = new System.Drawing.Size(91, 13);
            this.lblCurrentSelection.TabIndex = 25;
            this.lblCurrentSelection.Text = "Current Selection:";
            // 
            // lblSetValue
            // 
            this.lblSetValue.AutoSize = true;
            this.lblSetValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetValue.Location = new System.Drawing.Point(21, 58);
            this.lblSetValue.Name = "lblSetValue";
            this.lblSetValue.Size = new System.Drawing.Size(37, 13);
            this.lblSetValue.TabIndex = 26;
            this.lblSetValue.Text = "None";
            // 
            // btnSetVariableSelect
            // 
            this.btnSetVariableSelect.Location = new System.Drawing.Point(9, 19);
            this.btnSetVariableSelect.Name = "btnSetVariableSelect";
            this.btnSetVariableSelect.Padding = new System.Windows.Forms.Padding(5);
            this.btnSetVariableSelect.Size = new System.Drawing.Size(347, 23);
            this.btnSetVariableSelect.TabIndex = 27;
            this.btnSetVariableSelect.Text = "Select a Variable...";
            // 
            // grpSetVariableEvent
            // 
            this.grpSetVariableEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSetVariableEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSetVariableEvent.Controls.Add(this.chkSyncParty);
            this.grpSetVariableEvent.Controls.Add(this.chkInstanceSync);
            this.grpSetVariableEvent.Controls.Add(this.btnCancel);
            this.grpSetVariableEvent.Controls.Add(this.btnSave);
            this.grpSetVariableEvent.Controls.Add(this.grpOperator);
            this.grpSetVariableEvent.Controls.Add(this.grpSetMode);
            this.grpSetVariableEvent.Controls.Add(this.grpSelectVariable);
            this.grpSetVariableEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetVariableEvent.Location = new System.Drawing.Point(3, 0);
            this.grpSetVariableEvent.Name = "grpSetVariableEvent";
            this.grpSetVariableEvent.Size = new System.Drawing.Size(374, 481);
            this.grpSetVariableEvent.TabIndex = 42;
            this.grpSetVariableEvent.TabStop = false;
            this.grpSetVariableEvent.Text = "Set Variable";
            // 
            // grpSetToVariable
            // 
            this.grpSetToVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSetToVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSetToVariable.Controls.Add(this.btnSettingVariableSelect);
            this.grpSetToVariable.Controls.Add(this.lblSettingVariableValue);
            this.grpSetToVariable.Controls.Add(this.lblCurrentSelection2);
            this.grpSetToVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetToVariable.Location = new System.Drawing.Point(6, 42);
            this.grpSetToVariable.Name = "grpSetToVariable";
            this.grpSetToVariable.Size = new System.Drawing.Size(350, 77);
            this.grpSetToVariable.TabIndex = 42;
            this.grpSetToVariable.TabStop = false;
            this.grpSetToVariable.Text = "Set to Variable";
            // 
            // btnSettingVariableSelect
            // 
            this.btnSettingVariableSelect.Location = new System.Drawing.Point(9, 19);
            this.btnSettingVariableSelect.Name = "btnSettingVariableSelect";
            this.btnSettingVariableSelect.Padding = new System.Windows.Forms.Padding(5);
            this.btnSettingVariableSelect.Size = new System.Drawing.Size(335, 23);
            this.btnSettingVariableSelect.TabIndex = 27;
            this.btnSettingVariableSelect.Text = "Select a Variable...";
            // 
            // lblSettingVariableValue
            // 
            this.lblSettingVariableValue.AutoSize = true;
            this.lblSettingVariableValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettingVariableValue.Location = new System.Drawing.Point(21, 58);
            this.lblSettingVariableValue.Name = "lblSettingVariableValue";
            this.lblSettingVariableValue.Size = new System.Drawing.Size(37, 13);
            this.lblSettingVariableValue.TabIndex = 26;
            this.lblSettingVariableValue.Text = "None";
            // 
            // lblCurrentSelection2
            // 
            this.lblCurrentSelection2.AutoSize = true;
            this.lblCurrentSelection2.Location = new System.Drawing.Point(6, 45);
            this.lblCurrentSelection2.Name = "lblCurrentSelection2";
            this.lblCurrentSelection2.Size = new System.Drawing.Size(91, 13);
            this.lblCurrentSelection2.TabIndex = 25;
            this.lblCurrentSelection2.Text = "Current Selection:";
            // 
            // grpSetMode
            // 
            this.grpSetMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSetMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSetMode.Controls.Add(this.grpIntegerSet);
            this.grpSetMode.Controls.Add(this.rdoSetToVariable);
            this.grpSetMode.Controls.Add(this.rdoSetToValue);
            this.grpSetMode.Controls.Add(this.grpSetToVariable);
            this.grpSetMode.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetMode.Location = new System.Drawing.Point(6, 172);
            this.grpSetMode.Name = "grpSetMode";
            this.grpSetMode.Size = new System.Drawing.Size(362, 227);
            this.grpSetMode.TabIndex = 43;
            this.grpSetMode.TabStop = false;
            this.grpSetMode.Text = "Set To...";
            // 
            // rdoSetToValue
            // 
            this.rdoSetToValue.AutoSize = true;
            this.rdoSetToValue.Checked = true;
            this.rdoSetToValue.Location = new System.Drawing.Point(9, 19);
            this.rdoSetToValue.Name = "rdoSetToValue";
            this.rdoSetToValue.Size = new System.Drawing.Size(82, 17);
            this.rdoSetToValue.TabIndex = 25;
            this.rdoSetToValue.TabStop = true;
            this.rdoSetToValue.Text = "Static Value";
            // 
            // rdoSetToVariable
            // 
            this.rdoSetToVariable.AutoSize = true;
            this.rdoSetToVariable.Location = new System.Drawing.Point(97, 19);
            this.rdoSetToVariable.Name = "rdoSetToVariable";
            this.rdoSetToVariable.Size = new System.Drawing.Size(93, 17);
            this.rdoSetToVariable.TabIndex = 26;
            this.rdoSetToVariable.Text = "Variable Value";
            // 
            // grpIntegerSet
            // 
            this.grpIntegerSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpIntegerSet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpIntegerSet.Controls.Add(this.cmbIntDynamicOptions);
            this.grpIntegerSet.Controls.Add(this.nudValue);
            this.grpIntegerSet.Controls.Add(this.rdoIntDynamic);
            this.grpIntegerSet.Controls.Add(this.rdoIntStatic);
            this.grpIntegerSet.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpIntegerSet.Location = new System.Drawing.Point(6, 125);
            this.grpIntegerSet.Name = "grpIntegerSet";
            this.grpIntegerSet.Size = new System.Drawing.Size(350, 96);
            this.grpIntegerSet.TabIndex = 44;
            this.grpIntegerSet.TabStop = false;
            this.grpIntegerSet.Text = "Integer Set";
            // 
            // grpOperator
            // 
            this.grpOperator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOperator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOperator.Controls.Add(this.cmbOperator);
            this.grpOperator.Controls.Add(this.lblOperator);
            this.grpOperator.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOperator.Location = new System.Drawing.Point(6, 105);
            this.grpOperator.Name = "grpOperator";
            this.grpOperator.Size = new System.Drawing.Size(362, 61);
            this.grpOperator.TabIndex = 45;
            this.grpOperator.TabStop = false;
            this.grpOperator.Text = "Operator";
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(12, 25);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(48, 13);
            this.lblOperator.TabIndex = 26;
            this.lblOperator.Text = "Operator";
            // 
            // rdoIntStatic
            // 
            this.rdoIntStatic.AutoSize = true;
            this.rdoIntStatic.Checked = true;
            this.rdoIntStatic.Location = new System.Drawing.Point(15, 19);
            this.rdoIntStatic.Name = "rdoIntStatic";
            this.rdoIntStatic.Size = new System.Drawing.Size(52, 17);
            this.rdoIntStatic.TabIndex = 26;
            this.rdoIntStatic.TabStop = true;
            this.rdoIntStatic.Text = "Static";
            this.rdoIntStatic.UseMnemonic = false;
            // 
            // rdoIntDynamic
            // 
            this.rdoIntDynamic.AutoSize = true;
            this.rdoIntDynamic.Location = new System.Drawing.Point(15, 55);
            this.rdoIntDynamic.Name = "rdoIntDynamic";
            this.rdoIntDynamic.Size = new System.Drawing.Size(66, 17);
            this.rdoIntDynamic.TabIndex = 27;
            this.rdoIntDynamic.Text = "Dynamic";
            this.rdoIntDynamic.UseMnemonic = false;
            // 
            // nudValue
            // 
            this.nudValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudValue.Location = new System.Drawing.Point(102, 19);
            this.nudValue.Maximum = new decimal(new int[] {
            -1593835521,
            466537709,
            54210,
            0});
            this.nudValue.Minimum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            -2147483648});
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(242, 20);
            this.nudValue.TabIndex = 28;
            this.nudValue.ThousandsSeparator = true;
            this.nudValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbOperator
            // 
            this.cmbOperator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbOperator.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbOperator.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbOperator.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbOperator.DrawDropdownHoverOutline = false;
            this.cmbOperator.DrawFocusRectangle = false;
            this.cmbOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOperator.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Items.AddRange(new object[] {
            "Set"});
            this.cmbOperator.Location = new System.Drawing.Point(66, 22);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(290, 21);
            this.cmbOperator.TabIndex = 45;
            this.cmbOperator.Text = "Set";
            this.cmbOperator.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbIntDynamicOptions
            // 
            this.cmbIntDynamicOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbIntDynamicOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbIntDynamicOptions.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbIntDynamicOptions.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbIntDynamicOptions.DrawDropdownHoverOutline = false;
            this.cmbIntDynamicOptions.DrawFocusRectangle = false;
            this.cmbIntDynamicOptions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbIntDynamicOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIntDynamicOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIntDynamicOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbIntDynamicOptions.FormattingEnabled = true;
            this.cmbIntDynamicOptions.Items.AddRange(new object[] {
            "Set"});
            this.cmbIntDynamicOptions.Location = new System.Drawing.Point(102, 55);
            this.cmbIntDynamicOptions.Name = "cmbIntDynamicOptions";
            this.cmbIntDynamicOptions.Size = new System.Drawing.Size(242, 21);
            this.cmbIntDynamicOptions.TabIndex = 46;
            this.cmbIntDynamicOptions.Text = "Set";
            this.cmbIntDynamicOptions.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(212, 448);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Ok";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(293, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            // 
            // chkInstanceSync
            // 
            this.chkInstanceSync.AutoSize = true;
            this.chkInstanceSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkInstanceSync.Location = new System.Drawing.Point(103, 452);
            this.chkInstanceSync.Name = "chkInstanceSync";
            this.chkInstanceSync.Size = new System.Drawing.Size(100, 17);
            this.chkInstanceSync.TabIndex = 48;
            this.chkInstanceSync.Text = "Instance Sync?";
            // 
            // chkSyncParty
            // 
            this.chkSyncParty.AutoSize = true;
            this.chkSyncParty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkSyncParty.Location = new System.Drawing.Point(12, 452);
            this.chkSyncParty.Name = "chkSyncParty";
            this.chkSyncParty.Size = new System.Drawing.Size(83, 17);
            this.chkSyncParty.TabIndex = 49;
            this.chkSyncParty.Text = "Party Sync?";
            // 
            // EventCommand_ChangeVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpSetVariableEvent);
            this.Name = "EventCommand_ChangeVariable";
            this.Size = new System.Drawing.Size(382, 484);
            this.grpSelectVariable.ResumeLayout(false);
            this.grpSelectVariable.PerformLayout();
            this.grpSetVariableEvent.ResumeLayout(false);
            this.grpSetVariableEvent.PerformLayout();
            this.grpSetToVariable.ResumeLayout(false);
            this.grpSetToVariable.PerformLayout();
            this.grpSetMode.ResumeLayout(false);
            this.grpSetMode.PerformLayout();
            this.grpIntegerSet.ResumeLayout(false);
            this.grpIntegerSet.PerformLayout();
            this.grpOperator.ResumeLayout(false);
            this.grpOperator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpSelectVariable;
        private System.Windows.Forms.Label lblSetValue;
        private System.Windows.Forms.Label lblCurrentSelection;
        private DarkUI.Controls.DarkButton btnSetVariableSelect;
        private DarkUI.Controls.DarkGroupBox grpSetVariableEvent;
        private DarkUI.Controls.DarkGroupBox grpSetMode;
        private DarkUI.Controls.DarkGroupBox grpSetToVariable;
        private DarkUI.Controls.DarkButton btnSettingVariableSelect;
        private System.Windows.Forms.Label lblSettingVariableValue;
        private System.Windows.Forms.Label lblCurrentSelection2;
        internal DarkUI.Controls.DarkRadioButton rdoSetToVariable;
        internal DarkUI.Controls.DarkRadioButton rdoSetToValue;
        private DarkUI.Controls.DarkGroupBox grpIntegerSet;
        private DarkUI.Controls.DarkGroupBox grpOperator;
        private System.Windows.Forms.Label lblOperator;
        internal DarkUI.Controls.DarkComboBox cmbOperator;
        internal DarkUI.Controls.DarkRadioButton rdoIntStatic;
        internal DarkUI.Controls.DarkRadioButton rdoIntDynamic;
        private DarkUI.Controls.DarkNumericUpDown nudValue;
        internal DarkUI.Controls.DarkComboBox cmbIntDynamicOptions;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkCheckBox chkInstanceSync;
        private DarkUI.Controls.DarkCheckBox chkSyncParty;
    }
}
