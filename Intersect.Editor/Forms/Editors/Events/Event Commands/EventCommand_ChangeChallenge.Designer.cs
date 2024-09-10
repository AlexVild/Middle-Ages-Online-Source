
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeChallenge
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
            this.rdoComplete = new DarkUI.Controls.DarkRadioButton();
            this.rdoReset = new DarkUI.Controls.DarkRadioButton();
            this.rdoChangeReps = new DarkUI.Controls.DarkRadioButton();
            this.cmbWeaponType = new DarkUI.Controls.DarkComboBox();
            this.lblChallenge = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.chkNotInProgress = new DarkUI.Controls.DarkCheckBox();
            this.grpChangeItems.SuspendLayout();
            this.grpAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).BeginInit();
            this.SuspendLayout();
            // 
            // grpChangeItems
            // 
            this.grpChangeItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeItems.Controls.Add(this.chkNotInProgress);
            this.grpChangeItems.Controls.Add(this.grpAction);
            this.grpChangeItems.Controls.Add(this.cmbWeaponType);
            this.grpChangeItems.Controls.Add(this.lblChallenge);
            this.grpChangeItems.Controls.Add(this.btnCancel);
            this.grpChangeItems.Controls.Add(this.btnSave);
            this.grpChangeItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeItems.Location = new System.Drawing.Point(3, 2);
            this.grpChangeItems.Name = "grpChangeItems";
            this.grpChangeItems.Size = new System.Drawing.Size(214, 246);
            this.grpChangeItems.TabIndex = 19;
            this.grpChangeItems.TabStop = false;
            this.grpChangeItems.Text = "Change Player Challenges";
            // 
            // grpAction
            // 
            this.grpAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAction.Controls.Add(this.lblAmt);
            this.grpAction.Controls.Add(this.nudAmt);
            this.grpAction.Controls.Add(this.rdoComplete);
            this.grpAction.Controls.Add(this.rdoReset);
            this.grpAction.Controls.Add(this.rdoChangeReps);
            this.grpAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAction.Location = new System.Drawing.Point(8, 64);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(200, 121);
            this.grpAction.TabIndex = 37;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Amount Type:";
            // 
            // lblAmt
            // 
            this.lblAmt.AutoSize = true;
            this.lblAmt.Location = new System.Drawing.Point(15, 39);
            this.lblAmt.Name = "lblAmt";
            this.lblAmt.Size = new System.Drawing.Size(43, 13);
            this.lblAmt.TabIndex = 41;
            this.lblAmt.Text = "Amount";
            // 
            // nudAmt
            // 
            this.nudAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAmt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAmt.Location = new System.Drawing.Point(64, 37);
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
            // rdoComplete
            // 
            this.rdoComplete.AutoSize = true;
            this.rdoComplete.Location = new System.Drawing.Point(9, 92);
            this.rdoComplete.Name = "rdoComplete";
            this.rdoComplete.Size = new System.Drawing.Size(110, 17);
            this.rdoComplete.TabIndex = 38;
            this.rdoComplete.Text = "Mark as Complete";
            // 
            // rdoReset
            // 
            this.rdoReset.AutoSize = true;
            this.rdoReset.Location = new System.Drawing.Point(9, 69);
            this.rdoReset.Name = "rdoReset";
            this.rdoReset.Size = new System.Drawing.Size(53, 17);
            this.rdoReset.TabIndex = 37;
            this.rdoReset.Text = "Reset";
            // 
            // rdoChangeReps
            // 
            this.rdoChangeReps.AutoSize = true;
            this.rdoChangeReps.Checked = true;
            this.rdoChangeReps.Location = new System.Drawing.Point(9, 19);
            this.rdoChangeReps.Name = "rdoChangeReps";
            this.rdoChangeReps.Size = new System.Drawing.Size(90, 17);
            this.rdoChangeReps.TabIndex = 35;
            this.rdoChangeReps.TabStop = true;
            this.rdoChangeReps.Text = "Change Reps";
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
            this.cmbWeaponType.Size = new System.Drawing.Size(194, 21);
            this.cmbWeaponType.TabIndex = 22;
            this.cmbWeaponType.Text = null;
            this.cmbWeaponType.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblChallenge
            // 
            this.lblChallenge.AutoSize = true;
            this.lblChallenge.Location = new System.Drawing.Point(5, 21);
            this.lblChallenge.Name = "lblChallenge";
            this.lblChallenge.Size = new System.Drawing.Size(54, 13);
            this.lblChallenge.TabIndex = 21;
            this.lblChallenge.Text = "Challenge";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(133, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(52, 217);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkNotInProgress
            // 
            this.chkNotInProgress.Location = new System.Drawing.Point(8, 191);
            this.chkNotInProgress.Name = "chkNotInProgress";
            this.chkNotInProgress.Size = new System.Drawing.Size(185, 20);
            this.chkNotInProgress.TabIndex = 61;
            this.chkNotInProgress.Text = "Ignore In-Progress Check?";
            // 
            // EventCommand_ChangeChallenge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeItems);
            this.Name = "EventCommand_ChangeChallenge";
            this.Size = new System.Drawing.Size(220, 254);
            this.grpChangeItems.ResumeLayout(false);
            this.grpChangeItems.PerformLayout();
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeItems;
        private DarkUI.Controls.DarkGroupBox grpAction;
        private System.Windows.Forms.Label lblAmt;
        private DarkUI.Controls.DarkNumericUpDown nudAmt;
        private DarkUI.Controls.DarkRadioButton rdoComplete;
        private DarkUI.Controls.DarkRadioButton rdoReset;
        private DarkUI.Controls.DarkRadioButton rdoChangeReps;
        private DarkUI.Controls.DarkComboBox cmbWeaponType;
        private System.Windows.Forms.Label lblChallenge;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkCheckBox chkNotInProgress;
    }
}
