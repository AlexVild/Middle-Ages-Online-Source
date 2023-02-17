
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeEnhancements
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
            this.grpChangeEnhance = new DarkUI.Controls.DarkGroupBox();
            this.cmbEnhancements = new DarkUI.Controls.DarkComboBox();
            this.chkForget = new System.Windows.Forms.CheckBox();
            this.lblEnhance = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpChangeEnhance.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChangeEnhance
            // 
            this.grpChangeEnhance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeEnhance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeEnhance.Controls.Add(this.cmbEnhancements);
            this.grpChangeEnhance.Controls.Add(this.chkForget);
            this.grpChangeEnhance.Controls.Add(this.lblEnhance);
            this.grpChangeEnhance.Controls.Add(this.btnCancel);
            this.grpChangeEnhance.Controls.Add(this.btnSave);
            this.grpChangeEnhance.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeEnhance.Location = new System.Drawing.Point(3, 3);
            this.grpChangeEnhance.Name = "grpChangeEnhance";
            this.grpChangeEnhance.Size = new System.Drawing.Size(192, 120);
            this.grpChangeEnhance.TabIndex = 19;
            this.grpChangeEnhance.TabStop = false;
            this.grpChangeEnhance.Text = "Change Enhancement";
            // 
            // cmbEnhancements
            // 
            this.cmbEnhancements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEnhancements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEnhancements.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEnhancements.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEnhancements.DrawDropdownHoverOutline = false;
            this.cmbEnhancements.DrawFocusRectangle = false;
            this.cmbEnhancements.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEnhancements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnhancements.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEnhancements.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEnhancements.FormattingEnabled = true;
            this.cmbEnhancements.Location = new System.Drawing.Point(6, 38);
            this.cmbEnhancements.Name = "cmbEnhancements";
            this.cmbEnhancements.Size = new System.Drawing.Size(180, 21);
            this.cmbEnhancements.TabIndex = 74;
            this.cmbEnhancements.Text = null;
            this.cmbEnhancements.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // chkForget
            // 
            this.chkForget.AutoSize = true;
            this.chkForget.Checked = true;
            this.chkForget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForget.Location = new System.Drawing.Point(7, 65);
            this.chkForget.Name = "chkForget";
            this.chkForget.Size = new System.Drawing.Size(62, 17);
            this.chkForget.TabIndex = 73;
            this.chkForget.Text = "Forget?";
            this.chkForget.UseVisualStyleBackColor = true;
            // 
            // lblEnhance
            // 
            this.lblEnhance.AutoSize = true;
            this.lblEnhance.Location = new System.Drawing.Point(4, 22);
            this.lblEnhance.Name = "lblEnhance";
            this.lblEnhance.Size = new System.Drawing.Size(73, 13);
            this.lblEnhance.TabIndex = 21;
            this.lblEnhance.Text = "Enhancement";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(111, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 88);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ChangeEnhancements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeEnhance);
            this.Name = "EventCommand_ChangeEnhancements";
            this.Size = new System.Drawing.Size(205, 129);
            this.grpChangeEnhance.ResumeLayout(false);
            this.grpChangeEnhance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeEnhance;
        private DarkUI.Controls.DarkComboBox cmbEnhancements;
        private System.Windows.Forms.CheckBox chkForget;
        private System.Windows.Forms.Label lblEnhance;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
