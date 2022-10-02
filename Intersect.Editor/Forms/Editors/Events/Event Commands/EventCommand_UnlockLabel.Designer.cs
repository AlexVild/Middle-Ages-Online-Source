
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_UnlockLabel
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
            this.grpLabelUnlock = new DarkUI.Controls.DarkGroupBox();
            this.grpStatus = new DarkUI.Controls.DarkGroupBox();
            this.rdoRemove = new DarkUI.Controls.DarkRadioButton();
            this.rdoUnlock = new DarkUI.Controls.DarkRadioButton();
            this.darkCheckBox2 = new DarkUI.Controls.DarkCheckBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.cmbLabels = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpLabelUnlock.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLabelUnlock
            // 
            this.grpLabelUnlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpLabelUnlock.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpLabelUnlock.Controls.Add(this.grpStatus);
            this.grpLabelUnlock.Controls.Add(this.lblLabel);
            this.grpLabelUnlock.Controls.Add(this.cmbLabels);
            this.grpLabelUnlock.Controls.Add(this.btnCancel);
            this.grpLabelUnlock.Controls.Add(this.btnSave);
            this.grpLabelUnlock.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpLabelUnlock.Location = new System.Drawing.Point(10, 3);
            this.grpLabelUnlock.Name = "grpLabelUnlock";
            this.grpLabelUnlock.Size = new System.Drawing.Size(280, 152);
            this.grpLabelUnlock.TabIndex = 18;
            this.grpLabelUnlock.TabStop = false;
            this.grpLabelUnlock.Text = "Label Unlock";
            // 
            // grpStatus
            // 
            this.grpStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpStatus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStatus.Controls.Add(this.rdoRemove);
            this.grpStatus.Controls.Add(this.rdoUnlock);
            this.grpStatus.Controls.Add(this.darkCheckBox2);
            this.grpStatus.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStatus.Location = new System.Drawing.Point(20, 66);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(249, 48);
            this.grpStatus.TabIndex = 113;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // rdoRemove
            // 
            this.rdoRemove.AutoSize = true;
            this.rdoRemove.Location = new System.Drawing.Point(88, 19);
            this.rdoRemove.Name = "rdoRemove";
            this.rdoRemove.Size = new System.Drawing.Size(65, 17);
            this.rdoRemove.TabIndex = 112;
            this.rdoRemove.Text = "Remove";
            // 
            // rdoUnlock
            // 
            this.rdoUnlock.AutoSize = true;
            this.rdoUnlock.Location = new System.Drawing.Point(13, 19);
            this.rdoUnlock.Name = "rdoUnlock";
            this.rdoUnlock.Size = new System.Drawing.Size(59, 17);
            this.rdoUnlock.TabIndex = 111;
            this.rdoUnlock.Text = "Unlock";
            // 
            // darkCheckBox2
            // 
            this.darkCheckBox2.AutoSize = true;
            this.darkCheckBox2.Location = new System.Drawing.Point(76, 312);
            this.darkCheckBox2.Name = "darkCheckBox2";
            this.darkCheckBox2.Size = new System.Drawing.Size(104, 17);
            this.darkCheckBox2.TabIndex = 110;
            this.darkCheckBox2.Text = "Run indefinitely?";
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(17, 32);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(33, 13);
            this.lblLabel.TabIndex = 52;
            this.lblLabel.Text = "Label";
            // 
            // cmbLabels
            // 
            this.cmbLabels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbLabels.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbLabels.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbLabels.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbLabels.DrawDropdownHoverOutline = false;
            this.cmbLabels.DrawFocusRectangle = false;
            this.cmbLabels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLabels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLabels.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbLabels.FormattingEnabled = true;
            this.cmbLabels.Location = new System.Drawing.Point(65, 29);
            this.cmbLabels.Name = "cmbLabels";
            this.cmbLabels.Size = new System.Drawing.Size(209, 21);
            this.cmbLabels.TabIndex = 51;
            this.cmbLabels.Text = null;
            this.cmbLabels.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(162, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(81, 120);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_UnlockLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpLabelUnlock);
            this.Name = "EventCommand_UnlockLabel";
            this.Size = new System.Drawing.Size(301, 161);
            this.grpLabelUnlock.ResumeLayout(false);
            this.grpLabelUnlock.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpLabelUnlock;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbLabels;
        private System.Windows.Forms.Label lblLabel;
        private DarkUI.Controls.DarkGroupBox grpStatus;
        private DarkUI.Controls.DarkRadioButton rdoRemove;
        private DarkUI.Controls.DarkRadioButton rdoUnlock;
        private DarkUI.Controls.DarkCheckBox darkCheckBox2;
    }
}
