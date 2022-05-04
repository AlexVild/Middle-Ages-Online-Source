
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_StartTimer
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
            this.grpStartTimer = new DarkUI.Controls.DarkGroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.lblTimer = new System.Windows.Forms.Label();
            this.cmbTimer = new DarkUI.Controls.DarkComboBox();
            this.cmbTimerType = new DarkUI.Controls.DarkComboBox();
            this.grpStartTimer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStartTimer
            // 
            this.grpStartTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStartTimer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStartTimer.Controls.Add(this.cmbTimerType);
            this.grpStartTimer.Controls.Add(this.cmbTimer);
            this.grpStartTimer.Controls.Add(this.lblTimer);
            this.grpStartTimer.Controls.Add(this.lblType);
            this.grpStartTimer.Controls.Add(this.btnCancel);
            this.grpStartTimer.Controls.Add(this.btnSave);
            this.grpStartTimer.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStartTimer.Location = new System.Drawing.Point(3, 0);
            this.grpStartTimer.Name = "grpStartTimer";
            this.grpStartTimer.Size = new System.Drawing.Size(232, 139);
            this.grpStartTimer.TabIndex = 22;
            this.grpStartTimer.TabStop = false;
            this.grpStartTimer.Text = "Start Timer";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 29);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 24;
            this.lblType.Text = "Type";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(151, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(65, 107);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(6, 62);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(33, 13);
            this.lblTimer.TabIndex = 25;
            this.lblTimer.Text = "Timer";
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
            this.cmbTimer.Location = new System.Drawing.Point(65, 59);
            this.cmbTimer.Name = "cmbTimer";
            this.cmbTimer.Size = new System.Drawing.Size(161, 21);
            this.cmbTimer.TabIndex = 26;
            this.cmbTimer.Text = null;
            this.cmbTimer.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimer.SelectedIndexChanged += new System.EventHandler(this.cmbTimer_SelectedIndexChanged);
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
            this.cmbTimerType.Location = new System.Drawing.Point(65, 26);
            this.cmbTimerType.Name = "cmbTimerType";
            this.cmbTimerType.Size = new System.Drawing.Size(161, 21);
            this.cmbTimerType.TabIndex = 27;
            this.cmbTimerType.Text = null;
            this.cmbTimerType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimerType.SelectedIndexChanged += new System.EventHandler(this.cmbTimerType_SelectedIndexChanged);
            // 
            // EventCommand_StartTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpStartTimer);
            this.Name = "EventCommand_StartTimer";
            this.Size = new System.Drawing.Size(243, 142);
            this.grpStartTimer.ResumeLayout(false);
            this.grpStartTimer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpStartTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblType;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbTimerType;
        private DarkUI.Controls.DarkComboBox cmbTimer;
    }
}
