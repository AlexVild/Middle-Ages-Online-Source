
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_StopTimer
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
            this.grpEventType = new DarkUI.Controls.DarkGroupBox();
            this.cmbStopType = new DarkUI.Controls.DarkComboBox();
            this.lblStopType = new System.Windows.Forms.Label();
            this.grpStopTimer = new DarkUI.Controls.DarkGroupBox();
            this.btnOk = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpTimer.SuspendLayout();
            this.grpEventType.SuspendLayout();
            this.grpStopTimer.SuspendLayout();
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
            this.grpTimer.Size = new System.Drawing.Size(249, 92);
            this.grpTimer.TabIndex = 24;
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
            this.cmbTimerType.Location = new System.Drawing.Point(72, 26);
            this.cmbTimerType.Name = "cmbTimerType";
            this.cmbTimerType.Size = new System.Drawing.Size(171, 21);
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
            this.cmbTimer.Location = new System.Drawing.Point(72, 59);
            this.cmbTimer.Name = "cmbTimer";
            this.cmbTimer.Size = new System.Drawing.Size(171, 21);
            this.cmbTimer.TabIndex = 26;
            this.cmbTimer.Text = null;
            this.cmbTimer.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTimer.SelectedIndexChanged += new System.EventHandler(this.cmbTimer_SelectedIndexChanged);
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
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 29);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 24;
            this.lblType.Text = "Type";
            // 
            // grpEventType
            // 
            this.grpEventType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpEventType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEventType.Controls.Add(this.cmbStopType);
            this.grpEventType.Controls.Add(this.lblStopType);
            this.grpEventType.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEventType.Location = new System.Drawing.Point(6, 117);
            this.grpEventType.Name = "grpEventType";
            this.grpEventType.Size = new System.Drawing.Size(249, 67);
            this.grpEventType.TabIndex = 28;
            this.grpEventType.TabStop = false;
            this.grpEventType.Text = "Event";
            // 
            // cmbStopType
            // 
            this.cmbStopType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStopType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStopType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStopType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStopType.DrawDropdownHoverOutline = false;
            this.cmbStopType.DrawFocusRectangle = false;
            this.cmbStopType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStopType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStopType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStopType.FormattingEnabled = true;
            this.cmbStopType.Location = new System.Drawing.Point(72, 26);
            this.cmbStopType.Name = "cmbStopType";
            this.cmbStopType.Size = new System.Drawing.Size(171, 21);
            this.cmbStopType.TabIndex = 27;
            this.cmbStopType.Text = null;
            this.cmbStopType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbStopType.SelectedIndexChanged += new System.EventHandler(this.cmbStopType_SelectedIndexChanged);
            // 
            // lblStopType
            // 
            this.lblStopType.AutoSize = true;
            this.lblStopType.Location = new System.Drawing.Point(6, 29);
            this.lblStopType.Name = "lblStopType";
            this.lblStopType.Size = new System.Drawing.Size(56, 13);
            this.lblStopType.TabIndex = 24;
            this.lblStopType.Text = "Stop Type";
            // 
            // grpStopTimer
            // 
            this.grpStopTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStopTimer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStopTimer.Controls.Add(this.btnCancel);
            this.grpStopTimer.Controls.Add(this.btnOk);
            this.grpStopTimer.Controls.Add(this.grpTimer);
            this.grpStopTimer.Controls.Add(this.grpEventType);
            this.grpStopTimer.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStopTimer.Location = new System.Drawing.Point(3, 3);
            this.grpStopTimer.Name = "grpStopTimer";
            this.grpStopTimer.Size = new System.Drawing.Size(263, 224);
            this.grpStopTimer.TabIndex = 28;
            this.grpStopTimer.TabStop = false;
            this.grpStopTimer.Text = "Stop Timer";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(99, 190);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(5);
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 29;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EventCommand_StopTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpStopTimer);
            this.Name = "EventCommand_StopTimer";
            this.Size = new System.Drawing.Size(272, 232);
            this.grpTimer.ResumeLayout(false);
            this.grpTimer.PerformLayout();
            this.grpEventType.ResumeLayout(false);
            this.grpEventType.PerformLayout();
            this.grpStopTimer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpTimer;
        private DarkUI.Controls.DarkComboBox cmbTimerType;
        private DarkUI.Controls.DarkComboBox cmbTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblType;
        private DarkUI.Controls.DarkGroupBox grpEventType;
        private DarkUI.Controls.DarkComboBox cmbStopType;
        private System.Windows.Forms.Label lblStopType;
        private DarkUI.Controls.DarkGroupBox grpStopTimer;
        private DarkUI.Controls.DarkButton btnOk;
        private DarkUI.Controls.DarkButton btnCancel;
    }
}
