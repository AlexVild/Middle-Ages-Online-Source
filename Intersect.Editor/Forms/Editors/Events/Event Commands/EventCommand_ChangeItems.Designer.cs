using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandChangeItems
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
            this.grpChangeItems = new DarkUI.Controls.DarkGroupBox();
            this.chkStacks = new DarkUI.Controls.DarkCheckBox();
            this.grpManualAmount = new DarkUI.Controls.DarkGroupBox();
            this.nudGiveTakeAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAmount = new System.Windows.Forms.Label();
            this.grpVariableAmount = new DarkUI.Controls.DarkGroupBox();
            this.rdoInstanceVariable = new DarkUI.Controls.DarkRadioButton();
            this.cmbVariable = new DarkUI.Controls.DarkComboBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.rdoGlobalVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoPlayerVariable = new DarkUI.Controls.DarkRadioButton();
            this.grpAmountType = new DarkUI.Controls.DarkGroupBox();
            this.rdoVariable = new DarkUI.Controls.DarkRadioButton();
            this.rdoManual = new DarkUI.Controls.DarkRadioButton();
            this.cmbMethod = new DarkUI.Controls.DarkComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.cmbItem = new DarkUI.Controls.DarkComboBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.cmbAction = new DarkUI.Controls.DarkComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.rdoGuildVariable = new DarkUI.Controls.DarkRadioButton();
            this.grpChangeItems.SuspendLayout();
            this.grpManualAmount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiveTakeAmount)).BeginInit();
            this.grpVariableAmount.SuspendLayout();
            this.grpAmountType.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChangeItems
            // 
            this.grpChangeItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeItems.Controls.Add(this.chkStacks);
            this.grpChangeItems.Controls.Add(this.grpVariableAmount);
            this.grpChangeItems.Controls.Add(this.grpManualAmount);
            this.grpChangeItems.Controls.Add(this.grpAmountType);
            this.grpChangeItems.Controls.Add(this.cmbMethod);
            this.grpChangeItems.Controls.Add(this.lblMethod);
            this.grpChangeItems.Controls.Add(this.cmbItem);
            this.grpChangeItems.Controls.Add(this.lblItem);
            this.grpChangeItems.Controls.Add(this.cmbAction);
            this.grpChangeItems.Controls.Add(this.lblAction);
            this.grpChangeItems.Controls.Add(this.btnCancel);
            this.grpChangeItems.Controls.Add(this.btnSave);
            this.grpChangeItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeItems.Location = new System.Drawing.Point(4, 5);
            this.grpChangeItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpChangeItems.Name = "grpChangeItems";
            this.grpChangeItems.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpChangeItems.Size = new System.Drawing.Size(459, 572);
            this.grpChangeItems.TabIndex = 17;
            this.grpChangeItems.TabStop = false;
            this.grpChangeItems.Text = "Change Player Items:";
            // 
            // chkStacks
            // 
            this.chkStacks.Location = new System.Drawing.Point(40, 528);
            this.chkStacks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkStacks.Name = "chkStacks";
            this.chkStacks.Size = new System.Drawing.Size(147, 26);
            this.chkStacks.TabIndex = 60;
            this.chkStacks.Text = "Stacks?";
            // 
            // grpManualAmount
            // 
            this.grpManualAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpManualAmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpManualAmount.Controls.Add(this.nudGiveTakeAmount);
            this.grpManualAmount.Controls.Add(this.lblAmount);
            this.grpManualAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpManualAmount.Location = new System.Drawing.Point(9, 302);
            this.grpManualAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpManualAmount.Name = "grpManualAmount";
            this.grpManualAmount.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpManualAmount.Size = new System.Drawing.Size(438, 109);
            this.grpManualAmount.TabIndex = 37;
            this.grpManualAmount.TabStop = false;
            this.grpManualAmount.Text = "Manual Amount:";
            // 
            // nudGiveTakeAmount
            // 
            this.nudGiveTakeAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudGiveTakeAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudGiveTakeAmount.Location = new System.Drawing.Point(100, 49);
            this.nudGiveTakeAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudGiveTakeAmount.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.nudGiveTakeAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGiveTakeAmount.Name = "nudGiveTakeAmount";
            this.nudGiveTakeAmount.Size = new System.Drawing.Size(172, 26);
            this.nudGiveTakeAmount.TabIndex = 28;
            this.nudGiveTakeAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(12, 49);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(69, 20);
            this.lblAmount.TabIndex = 27;
            this.lblAmount.Text = "Amount:";
            // 
            // grpVariableAmount
            // 
            this.grpVariableAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpVariableAmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVariableAmount.Controls.Add(this.rdoGuildVariable);
            this.grpVariableAmount.Controls.Add(this.rdoInstanceVariable);
            this.grpVariableAmount.Controls.Add(this.cmbVariable);
            this.grpVariableAmount.Controls.Add(this.lblVariable);
            this.grpVariableAmount.Controls.Add(this.rdoGlobalVariable);
            this.grpVariableAmount.Controls.Add(this.rdoPlayerVariable);
            this.grpVariableAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVariableAmount.Location = new System.Drawing.Point(12, 305);
            this.grpVariableAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpVariableAmount.Name = "grpVariableAmount";
            this.grpVariableAmount.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpVariableAmount.Size = new System.Drawing.Size(438, 200);
            this.grpVariableAmount.TabIndex = 38;
            this.grpVariableAmount.TabStop = false;
            this.grpVariableAmount.Text = "Variable Amount:";
            this.grpVariableAmount.Visible = false;
            // 
            // rdoInstanceVariable
            // 
            this.rdoInstanceVariable.AutoSize = true;
            this.rdoInstanceVariable.Location = new System.Drawing.Point(9, 89);
            this.rdoInstanceVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoInstanceVariable.Name = "rdoInstanceVariable";
            this.rdoInstanceVariable.Size = new System.Drawing.Size(158, 24);
            this.rdoInstanceVariable.TabIndex = 40;
            this.rdoInstanceVariable.Text = "Instance Variable";
            this.rdoInstanceVariable.CheckedChanged += new System.EventHandler(this.rdoInstanceVariable_CheckedChanged);
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
            this.cmbVariable.Location = new System.Drawing.Point(100, 145);
            this.cmbVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(326, 27);
            this.cmbVariable.TabIndex = 39;
            this.cmbVariable.Text = null;
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblVariable
            // 
            this.lblVariable.AutoSize = true;
            this.lblVariable.Location = new System.Drawing.Point(24, 149);
            this.lblVariable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(67, 20);
            this.lblVariable.TabIndex = 38;
            this.lblVariable.Text = "Variable";
            // 
            // rdoGlobalVariable
            // 
            this.rdoGlobalVariable.AutoSize = true;
            this.rdoGlobalVariable.Location = new System.Drawing.Point(234, 29);
            this.rdoGlobalVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoGlobalVariable.Name = "rdoGlobalVariable";
            this.rdoGlobalVariable.Size = new System.Drawing.Size(142, 24);
            this.rdoGlobalVariable.TabIndex = 37;
            this.rdoGlobalVariable.Text = "Global Variable";
            this.rdoGlobalVariable.CheckedChanged += new System.EventHandler(this.rdoGlobalVariable_CheckedChanged);
            // 
            // rdoPlayerVariable
            // 
            this.rdoPlayerVariable.AutoSize = true;
            this.rdoPlayerVariable.Checked = true;
            this.rdoPlayerVariable.Location = new System.Drawing.Point(9, 29);
            this.rdoPlayerVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoPlayerVariable.Name = "rdoPlayerVariable";
            this.rdoPlayerVariable.Size = new System.Drawing.Size(139, 24);
            this.rdoPlayerVariable.TabIndex = 36;
            this.rdoPlayerVariable.TabStop = true;
            this.rdoPlayerVariable.Text = "Player Variable";
            this.rdoPlayerVariable.CheckedChanged += new System.EventHandler(this.rdoPlayerVariable_CheckedChanged);
            // 
            // grpAmountType
            // 
            this.grpAmountType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpAmountType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAmountType.Controls.Add(this.rdoVariable);
            this.grpAmountType.Controls.Add(this.rdoManual);
            this.grpAmountType.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAmountType.Location = new System.Drawing.Point(15, 177);
            this.grpAmountType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAmountType.Name = "grpAmountType";
            this.grpAmountType.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAmountType.Size = new System.Drawing.Size(172, 115);
            this.grpAmountType.TabIndex = 36;
            this.grpAmountType.TabStop = false;
            this.grpAmountType.Text = "Amount Type:";
            // 
            // rdoVariable
            // 
            this.rdoVariable.AutoSize = true;
            this.rdoVariable.Location = new System.Drawing.Point(14, 65);
            this.rdoVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoVariable.Name = "rdoVariable";
            this.rdoVariable.Size = new System.Drawing.Size(92, 24);
            this.rdoVariable.TabIndex = 36;
            this.rdoVariable.Text = "Variable";
            this.rdoVariable.CheckedChanged += new System.EventHandler(this.rdoVariable_CheckedChanged);
            // 
            // rdoManual
            // 
            this.rdoManual.AutoSize = true;
            this.rdoManual.Checked = true;
            this.rdoManual.Location = new System.Drawing.Point(14, 29);
            this.rdoManual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoManual.Name = "rdoManual";
            this.rdoManual.Size = new System.Drawing.Size(86, 24);
            this.rdoManual.TabIndex = 35;
            this.rdoManual.TabStop = true;
            this.rdoManual.Text = "Manual";
            this.rdoManual.CheckedChanged += new System.EventHandler(this.rdoManual_CheckedChanged);
            // 
            // cmbMethod
            // 
            this.cmbMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMethod.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMethod.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMethod.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMethod.DrawDropdownHoverOutline = false;
            this.cmbMethod.DrawFocusRectangle = false;
            this.cmbMethod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMethod.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Normal",
            "Allow Overflow",
            "Up to Amount"});
            this.cmbMethod.Location = new System.Drawing.Point(96, 112);
            this.cmbMethod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(343, 27);
            this.cmbMethod.TabIndex = 28;
            this.cmbMethod.Text = "Normal";
            this.cmbMethod.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(8, 117);
            this.lblMethod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(67, 20);
            this.lblMethod.TabIndex = 27;
            this.lblMethod.Text = "Method:";
            // 
            // cmbItem
            // 
            this.cmbItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbItem.DrawDropdownHoverOutline = false;
            this.cmbItem.DrawFocusRectangle = false;
            this.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(96, 71);
            this.cmbItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(343, 27);
            this.cmbItem.TabIndex = 24;
            this.cmbItem.Text = null;
            this.cmbItem.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(8, 74);
            this.lblItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(45, 20);
            this.lblItem.TabIndex = 23;
            this.lblItem.Text = "Item:";
            // 
            // cmbAction
            // 
            this.cmbAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAction.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAction.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAction.DrawDropdownHoverOutline = false;
            this.cmbAction.DrawFocusRectangle = false;
            this.cmbAction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Items.AddRange(new object[] {
            "Give",
            "Take"});
            this.cmbAction.Location = new System.Drawing.Point(96, 29);
            this.cmbAction.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(343, 27);
            this.cmbAction.TabIndex = 22;
            this.cmbAction.Text = "Give";
            this.cmbAction.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(8, 32);
            this.lblAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(58, 20);
            this.lblAction.TabIndex = 21;
            this.lblAction.Text = "Action:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(328, 528);
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
            this.btnSave.Location = new System.Drawing.Point(207, 528);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rdoGuildVariable
            // 
            this.rdoGuildVariable.AutoSize = true;
            this.rdoGuildVariable.Location = new System.Drawing.Point(234, 89);
            this.rdoGuildVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rdoGuildVariable.Name = "rdoGuildVariable";
            this.rdoGuildVariable.Size = new System.Drawing.Size(133, 24);
            this.rdoGuildVariable.TabIndex = 41;
            this.rdoGuildVariable.Text = "Guild Variable";
            this.rdoGuildVariable.CheckedChanged += new System.EventHandler(this.rdoGuildVariable_CheckedChanged);
            // 
            // EventCommandChangeItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeItems);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "EventCommandChangeItems";
            this.Size = new System.Drawing.Size(471, 591);
            this.grpChangeItems.ResumeLayout(false);
            this.grpChangeItems.PerformLayout();
            this.grpManualAmount.ResumeLayout(false);
            this.grpManualAmount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGiveTakeAmount)).EndInit();
            this.grpVariableAmount.ResumeLayout(false);
            this.grpVariableAmount.PerformLayout();
            this.grpAmountType.ResumeLayout(false);
            this.grpAmountType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpChangeItems;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkComboBox cmbAction;
        private System.Windows.Forms.Label lblAction;
        private DarkComboBox cmbItem;
        private System.Windows.Forms.Label lblItem;
        private DarkComboBox cmbMethod;
        private System.Windows.Forms.Label lblMethod;
        private DarkGroupBox grpAmountType;
        private DarkGroupBox grpManualAmount;
        private DarkNumericUpDown nudGiveTakeAmount;
        private System.Windows.Forms.Label lblAmount;
        private DarkGroupBox grpVariableAmount;
        private DarkRadioButton rdoVariable;
        private DarkRadioButton rdoManual;
        private DarkComboBox cmbVariable;
        private System.Windows.Forms.Label lblVariable;
        private DarkRadioButton rdoGlobalVariable;
        private DarkRadioButton rdoPlayerVariable;
        private DarkRadioButton rdoInstanceVariable;
        private DarkCheckBox chkStacks;
        private DarkRadioButton rdoGuildVariable;
    }
}
