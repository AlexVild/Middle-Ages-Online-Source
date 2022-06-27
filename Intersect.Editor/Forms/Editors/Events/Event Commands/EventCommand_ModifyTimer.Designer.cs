
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ModifyTimer
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
            this.grpTimer = new DarkUI.Controls.DarkGroupBox();
            this.cmbTimerType = new DarkUI.Controls.DarkComboBox();
            this.cmbTimer = new DarkUI.Controls.DarkComboBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.grpMod = new DarkUI.Controls.DarkGroupBox();
            this.grpVariables = new DarkUI.Controls.DarkGroupBox();
            this.lblVarType = new System.Windows.Forms.Label();
            this.cmbVariable = new DarkUI.Controls.DarkComboBox();
            this.cmbVariableType = new DarkUI.Controls.DarkComboBox();
            this.lblVarVal = new System.Windows.Forms.Label();
            this.rdoVariable = new DarkUI.Controls.DarkRadioButton();
            this.nudSeconds = new DarkUI.Controls.DarkNumericUpDown();
            this.rdoStatic = new DarkUI.Controls.DarkRadioButton();
            this.grpOperators = new DarkUI.Controls.DarkGroupBox();
            this.rdoSubtract = new DarkUI.Controls.DarkRadioButton();
            this.rdoAdd = new DarkUI.Controls.DarkRadioButton();
            this.rdoSet = new DarkUI.Controls.DarkRadioButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpModifyTimer = new DarkUI.Controls.DarkGroupBox();
            this.grpTimer.SuspendLayout();
            this.grpMod.SuspendLayout();
            this.grpVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).BeginInit();
            this.grpOperators.SuspendLayout();
            this.grpModifyTimer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTimer
            // 
            this.grpTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpTimer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTimer.Controls.Add(this.cmbTimerType);
            this.grpTimer.Controls.Add(this.cmbTimer);
            this.grpTimer.Controls.Add(this.lblTimer);
            this.grpTimer.Controls.Add(this.lblType);
            this.grpTimer.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTimer.Location = new System.Drawing.Point(6, 19);
            this.grpTimer.Name = "grpTimer";
            this.grpTimer.Size = new System.Drawing.Size(306, 74);
            this.grpTimer.TabIndex = 23;
            this.grpTimer.TabStop = false;
            this.grpTimer.Text = "Timer";
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
            this.cmbTimerType.Location = new System.Drawing.Point(68, 19);
            this.cmbTimerType.Name = "cmbTimerType";
            this.cmbTimerType.Size = new System.Drawing.Size(226, 21);
            this.cmbTimerType.TabIndex = 27;
            this.cmbTimerType.Text = null;
            this.cmbTimerType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimerType.SelectedIndexChanged += new System.EventHandler(this.cmbTimerType_SelectedIndexChanged);
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
            this.cmbTimer.Location = new System.Drawing.Point(68, 46);
            this.cmbTimer.Name = "cmbTimer";
            this.cmbTimer.Size = new System.Drawing.Size(226, 21);
            this.cmbTimer.TabIndex = 26;
            this.cmbTimer.Text = null;
            this.cmbTimer.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimer.SelectedIndexChanged += new System.EventHandler(this.cmbTimer_SelectedIndexChanged);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(12, 49);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(33, 13);
            this.lblTimer.TabIndex = 25;
            this.lblTimer.Text = "Timer";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 24;
            this.lblType.Text = "Type";
            // 
            // grpMod
            // 
            this.grpMod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpMod.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpMod.Controls.Add(this.grpVariables);
            this.grpMod.Controls.Add(this.rdoVariable);
            this.grpMod.Controls.Add(this.nudSeconds);
            this.grpMod.Controls.Add(this.rdoStatic);
            this.grpMod.Controls.Add(this.grpOperators);
            this.grpMod.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpMod.Location = new System.Drawing.Point(6, 99);
            this.grpMod.Name = "grpMod";
            this.grpMod.Size = new System.Drawing.Size(306, 208);
            this.grpMod.TabIndex = 28;
            this.grpMod.TabStop = false;
            this.grpMod.Text = "Modification";
            // 
            // grpVariables
            // 
            this.grpVariables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpVariables.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVariables.Controls.Add(this.lblVarType);
            this.grpVariables.Controls.Add(this.cmbVariable);
            this.grpVariables.Controls.Add(this.cmbVariableType);
            this.grpVariables.Controls.Add(this.lblVarVal);
            this.grpVariables.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVariables.Location = new System.Drawing.Point(9, 121);
            this.grpVariables.Name = "grpVariables";
            this.grpVariables.Size = new System.Drawing.Size(285, 77);
            this.grpVariables.TabIndex = 30;
            this.grpVariables.TabStop = false;
            this.grpVariables.Text = "Variable Selection";
            // 
            // lblVarType
            // 
            this.lblVarType.AutoSize = true;
            this.lblVarType.Location = new System.Drawing.Point(26, 22);
            this.lblVarType.Name = "lblVarType";
            this.lblVarType.Size = new System.Drawing.Size(72, 13);
            this.lblVarType.TabIndex = 28;
            this.lblVarType.Text = "Variable Type";
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
            this.cmbVariable.Location = new System.Drawing.Point(110, 46);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(169, 21);
            this.cmbVariable.TabIndex = 34;
            this.cmbVariable.Text = null;
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariable.SelectedIndexChanged += new System.EventHandler(this.cmbVariable_SelectedIndexChanged);
            // 
            // cmbVariableType
            // 
            this.cmbVariableType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVariableType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVariableType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVariableType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVariableType.DrawDropdownHoverOutline = false;
            this.cmbVariableType.DrawFocusRectangle = false;
            this.cmbVariableType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVariableType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariableType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVariableType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVariableType.FormattingEnabled = true;
            this.cmbVariableType.Location = new System.Drawing.Point(110, 19);
            this.cmbVariableType.Name = "cmbVariableType";
            this.cmbVariableType.Size = new System.Drawing.Size(169, 21);
            this.cmbVariableType.TabIndex = 26;
            this.cmbVariableType.Text = null;
            this.cmbVariableType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariableType.SelectedIndexChanged += new System.EventHandler(this.cmbVariableType_SelectedIndexChanged);
            // 
            // lblVarVal
            // 
            this.lblVarVal.AutoSize = true;
            this.lblVarVal.Location = new System.Drawing.Point(23, 49);
            this.lblVarVal.Name = "lblVarVal";
            this.lblVarVal.Size = new System.Drawing.Size(75, 13);
            this.lblVarVal.TabIndex = 33;
            this.lblVarVal.Text = "Variable Value";
            // 
            // rdoVariable
            // 
            this.rdoVariable.AutoSize = true;
            this.rdoVariable.Location = new System.Drawing.Point(9, 98);
            this.rdoVariable.Name = "rdoVariable";
            this.rdoVariable.Size = new System.Drawing.Size(112, 17);
            this.rdoVariable.TabIndex = 32;
            this.rdoVariable.Text = "Variable (seconds)";
            this.rdoVariable.CheckedChanged += new System.EventHandler(this.rdoVariable_CheckedChanged);
            // 
            // nudSeconds
            // 
            this.nudSeconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSeconds.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSeconds.Location = new System.Drawing.Point(131, 75);
            this.nudSeconds.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeconds.Name = "nudSeconds";
            this.nudSeconds.Size = new System.Drawing.Size(163, 20);
            this.nudSeconds.TabIndex = 31;
            this.nudSeconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeconds.ValueChanged += new System.EventHandler(this.nudSeconds_ValueChanged);
            // 
            // rdoStatic
            // 
            this.rdoStatic.AutoSize = true;
            this.rdoStatic.Checked = true;
            this.rdoStatic.Location = new System.Drawing.Point(9, 75);
            this.rdoStatic.Name = "rdoStatic";
            this.rdoStatic.Size = new System.Drawing.Size(101, 17);
            this.rdoStatic.TabIndex = 30;
            this.rdoStatic.TabStop = true;
            this.rdoStatic.Text = "Static (seconds)";
            this.rdoStatic.CheckedChanged += new System.EventHandler(this.rdoStatic_CheckedChanged);
            // 
            // grpOperators
            // 
            this.grpOperators.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOperators.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOperators.Controls.Add(this.rdoSubtract);
            this.grpOperators.Controls.Add(this.rdoAdd);
            this.grpOperators.Controls.Add(this.rdoSet);
            this.grpOperators.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOperators.Location = new System.Drawing.Point(9, 19);
            this.grpOperators.Name = "grpOperators";
            this.grpOperators.Size = new System.Drawing.Size(291, 49);
            this.grpOperators.TabIndex = 29;
            this.grpOperators.TabStop = false;
            this.grpOperators.Text = "Operator";
            // 
            // rdoSubtract
            // 
            this.rdoSubtract.AutoSize = true;
            this.rdoSubtract.Location = new System.Drawing.Point(194, 19);
            this.rdoSubtract.Name = "rdoSubtract";
            this.rdoSubtract.Size = new System.Drawing.Size(91, 17);
            this.rdoSubtract.TabIndex = 29;
            this.rdoSubtract.Text = "Subtract Time";
            this.rdoSubtract.CheckedChanged += new System.EventHandler(this.rdoSubtract_CheckedChanged);
            // 
            // rdoAdd
            // 
            this.rdoAdd.AutoSize = true;
            this.rdoAdd.Location = new System.Drawing.Point(101, 19);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(70, 17);
            this.rdoAdd.TabIndex = 28;
            this.rdoAdd.Text = "Add Time";
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // rdoSet
            // 
            this.rdoSet.AutoSize = true;
            this.rdoSet.Checked = true;
            this.rdoSet.Location = new System.Drawing.Point(6, 19);
            this.rdoSet.Name = "rdoSet";
            this.rdoSet.Size = new System.Drawing.Size(67, 17);
            this.rdoSet.TabIndex = 27;
            this.rdoSet.TabStop = true;
            this.rdoSet.Text = "Set Time";
            this.rdoSet.CheckedChanged += new System.EventHandler(this.rdoSet_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(237, 313);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(156, 313);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpModifyTimer
            // 
            this.grpModifyTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpModifyTimer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpModifyTimer.Controls.Add(this.grpTimer);
            this.grpModifyTimer.Controls.Add(this.grpMod);
            this.grpModifyTimer.Controls.Add(this.btnSave);
            this.grpModifyTimer.Controls.Add(this.btnCancel);
            this.grpModifyTimer.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpModifyTimer.Location = new System.Drawing.Point(3, 0);
            this.grpModifyTimer.Name = "grpModifyTimer";
            this.grpModifyTimer.Size = new System.Drawing.Size(318, 344);
            this.grpModifyTimer.TabIndex = 29;
            this.grpModifyTimer.TabStop = false;
            this.grpModifyTimer.Text = "Modify Timer";
            // 
            // EventCommand_ModifyTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpModifyTimer);
            this.Name = "EventCommand_ModifyTimer";
            this.Size = new System.Drawing.Size(329, 348);
            this.grpTimer.ResumeLayout(false);
            this.grpTimer.PerformLayout();
            this.grpMod.ResumeLayout(false);
            this.grpMod.PerformLayout();
            this.grpVariables.ResumeLayout(false);
            this.grpVariables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).EndInit();
            this.grpOperators.ResumeLayout(false);
            this.grpOperators.PerformLayout();
            this.grpModifyTimer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpTimer;
        private DarkUI.Controls.DarkComboBox cmbTimerType;
        private DarkUI.Controls.DarkComboBox cmbTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblType;
        private DarkUI.Controls.DarkGroupBox grpMod;
        private DarkUI.Controls.DarkComboBox cmbVariableType;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkGroupBox grpModifyTimer;
        private DarkUI.Controls.DarkGroupBox grpOperators;
        private DarkUI.Controls.DarkRadioButton rdoSet;
        private DarkUI.Controls.DarkRadioButton rdoSubtract;
        private DarkUI.Controls.DarkRadioButton rdoAdd;
        private DarkUI.Controls.DarkRadioButton rdoStatic;
        private DarkUI.Controls.DarkNumericUpDown nudSeconds;
        private DarkUI.Controls.DarkComboBox cmbVariable;
        private System.Windows.Forms.Label lblVarVal;
        private System.Windows.Forms.Label lblVarType;
        private DarkUI.Controls.DarkRadioButton rdoVariable;
        private DarkUI.Controls.DarkGroupBox grpVariables;
    }
}
