
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeWeaponTrack
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
            this.grpAction = new DarkUI.Controls.DarkGroupBox();
            this.lblAmt = new System.Windows.Forms.Label();
            this.nudAmt = new DarkUI.Controls.DarkNumericUpDown();
            this.rdoUnlearn = new DarkUI.Controls.DarkRadioButton();
            this.rdoGainExp = new DarkUI.Controls.DarkRadioButton();
            this.rdoGainLevel = new DarkUI.Controls.DarkRadioButton();
            this.rdoLoseLevel = new DarkUI.Controls.DarkRadioButton();
            this.rdoSetLevel = new DarkUI.Controls.DarkRadioButton();
            this.cmbWeaponType = new DarkUI.Controls.DarkComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpChangeItems.SuspendLayout();
            this.grpAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).BeginInit();
            this.SuspendLayout();
            // 
            // grpChangeItems
            // 
            this.grpChangeItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeItems.Controls.Add(this.grpAction);
            this.grpChangeItems.Controls.Add(this.cmbWeaponType);
            this.grpChangeItems.Controls.Add(this.lblType);
            this.grpChangeItems.Controls.Add(this.btnCancel);
            this.grpChangeItems.Controls.Add(this.btnSave);
            this.grpChangeItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeItems.Location = new System.Drawing.Point(3, 3);
            this.grpChangeItems.Name = "grpChangeItems";
            this.grpChangeItems.Size = new System.Drawing.Size(233, 258);
            this.grpChangeItems.TabIndex = 18;
            this.grpChangeItems.TabStop = false;
            this.grpChangeItems.Text = "Change Player Items:";
            // 
            // grpAction
            // 
            this.grpAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAction.Controls.Add(this.lblAmt);
            this.grpAction.Controls.Add(this.nudAmt);
            this.grpAction.Controls.Add(this.rdoUnlearn);
            this.grpAction.Controls.Add(this.rdoGainExp);
            this.grpAction.Controls.Add(this.rdoGainLevel);
            this.grpAction.Controls.Add(this.rdoLoseLevel);
            this.grpAction.Controls.Add(this.rdoSetLevel);
            this.grpAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAction.Location = new System.Drawing.Point(8, 64);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(208, 161);
            this.grpAction.TabIndex = 37;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Amount Type:";
            // 
            // lblAmt
            // 
            this.lblAmt.AutoSize = true;
            this.lblAmt.Location = new System.Drawing.Point(4, 137);
            this.lblAmt.Name = "lblAmt";
            this.lblAmt.Size = new System.Drawing.Size(43, 13);
            this.lblAmt.TabIndex = 41;
            this.lblAmt.Text = "Amount";
            // 
            // nudAmt
            // 
            this.nudAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAmt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAmt.Location = new System.Drawing.Point(65, 135);
            this.nudAmt.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudAmt.Minimum = new decimal(new int[] {
            10000000,
            0,
            0,
            -2147483648});
            this.nudAmt.Name = "nudAmt";
            this.nudAmt.Size = new System.Drawing.Size(130, 20);
            this.nudAmt.TabIndex = 40;
            this.nudAmt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rdoUnlearn
            // 
            this.rdoUnlearn.AutoSize = true;
            this.rdoUnlearn.Location = new System.Drawing.Point(10, 112);
            this.rdoUnlearn.Name = "rdoUnlearn";
            this.rdoUnlearn.Size = new System.Drawing.Size(62, 17);
            this.rdoUnlearn.TabIndex = 39;
            this.rdoUnlearn.Text = "Unlearn";
            // 
            // rdoGainExp
            // 
            this.rdoGainExp.AutoSize = true;
            this.rdoGainExp.Location = new System.Drawing.Point(9, 65);
            this.rdoGainExp.Name = "rdoGainExp";
            this.rdoGainExp.Size = new System.Drawing.Size(86, 17);
            this.rdoGainExp.TabIndex = 38;
            this.rdoGainExp.Text = "Change EXP";
            // 
            // rdoGainLevel
            // 
            this.rdoGainLevel.AutoSize = true;
            this.rdoGainLevel.Location = new System.Drawing.Point(9, 42);
            this.rdoGainLevel.Name = "rdoGainLevel";
            this.rdoGainLevel.Size = new System.Drawing.Size(87, 17);
            this.rdoGainLevel.TabIndex = 37;
            this.rdoGainLevel.Text = "Gain Level(s)";
            // 
            // rdoLoseLevel
            // 
            this.rdoLoseLevel.AutoSize = true;
            this.rdoLoseLevel.Location = new System.Drawing.Point(9, 89);
            this.rdoLoseLevel.Name = "rdoLoseLevel";
            this.rdoLoseLevel.Size = new System.Drawing.Size(77, 17);
            this.rdoLoseLevel.TabIndex = 36;
            this.rdoLoseLevel.Text = "Lose Level";
            // 
            // rdoSetLevel
            // 
            this.rdoSetLevel.AutoSize = true;
            this.rdoSetLevel.Checked = true;
            this.rdoSetLevel.Location = new System.Drawing.Point(9, 19);
            this.rdoSetLevel.Name = "rdoSetLevel";
            this.rdoSetLevel.Size = new System.Drawing.Size(70, 17);
            this.rdoSetLevel.TabIndex = 35;
            this.rdoSetLevel.TabStop = true;
            this.rdoSetLevel.Text = "Set Level";
            // 
            // cmbWeaponType
            // 
            this.cmbWeaponType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbWeaponType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbWeaponType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbWeaponType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbWeaponType.DrawDropdownHoverOutline = false;
            this.cmbWeaponType.DrawFocusRectangle = false;
            this.cmbWeaponType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeaponType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeaponType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbWeaponType.FormattingEnabled = true;
            this.cmbWeaponType.Location = new System.Drawing.Point(8, 37);
            this.cmbWeaponType.Name = "cmbWeaponType";
            this.cmbWeaponType.Size = new System.Drawing.Size(208, 21);
            this.cmbWeaponType.TabIndex = 22;
            this.cmbWeaponType.Text = null;
            this.cmbWeaponType.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(5, 21);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(75, 13);
            this.lblType.TabIndex = 21;
            this.lblType.Text = "Weapon Type";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(128, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(26, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ChangeWeaponTrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeItems);
            this.Name = "EventCommand_ChangeWeaponTrack";
            this.Size = new System.Drawing.Size(239, 264);
            this.grpChangeItems.ResumeLayout(false);
            this.grpChangeItems.PerformLayout();
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeItems;
        private DarkUI.Controls.DarkComboBox cmbWeaponType;
        private System.Windows.Forms.Label lblType;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkGroupBox grpAction;
        private DarkUI.Controls.DarkRadioButton rdoUnlearn;
        private DarkUI.Controls.DarkRadioButton rdoGainExp;
        private DarkUI.Controls.DarkRadioButton rdoGainLevel;
        private DarkUI.Controls.DarkRadioButton rdoLoseLevel;
        private DarkUI.Controls.DarkRadioButton rdoSetLevel;
        private System.Windows.Forms.Label lblAmt;
        private DarkUI.Controls.DarkNumericUpDown nudAmt;
    }
}
