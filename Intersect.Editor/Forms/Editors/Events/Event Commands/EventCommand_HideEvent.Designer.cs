
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_HideEvent
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
            this.grpHideEvent = new DarkUI.Controls.DarkGroupBox();
            this.lblEvent = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.cmbTarget = new DarkUI.Controls.DarkComboBox();
            this.grpHideEvent.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpHideEvent
            // 
            this.grpHideEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpHideEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpHideEvent.Controls.Add(this.cmbTarget);
            this.grpHideEvent.Controls.Add(this.lblEvent);
            this.grpHideEvent.Controls.Add(this.btnCancel);
            this.grpHideEvent.Controls.Add(this.btnSave);
            this.grpHideEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpHideEvent.Location = new System.Drawing.Point(3, 0);
            this.grpHideEvent.Name = "grpHideEvent";
            this.grpHideEvent.Size = new System.Drawing.Size(245, 79);
            this.grpHideEvent.TabIndex = 18;
            this.grpHideEvent.TabStop = false;
            this.grpHideEvent.Text = "Hide Event:";
            // 
            // lblEvent
            // 
            this.lblEvent.AutoSize = true;
            this.lblEvent.Location = new System.Drawing.Point(4, 22);
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Size = new System.Drawing.Size(38, 13);
            this.lblEvent.TabIndex = 21;
            this.lblEvent.Text = "Event:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(164, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(83, 47);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbTarget
            // 
            this.cmbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTarget.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTarget.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTarget.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTarget.DrawDropdownHoverOutline = false;
            this.cmbTarget.DrawFocusRectangle = false;
            this.cmbTarget.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTarget.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTarget.FormattingEnabled = true;
            this.cmbTarget.Items.AddRange(new object[] {
            "Self"});
            this.cmbTarget.Location = new System.Drawing.Point(48, 20);
            this.cmbTarget.Name = "cmbTarget";
            this.cmbTarget.Size = new System.Drawing.Size(191, 21);
            this.cmbTarget.TabIndex = 22;
            this.cmbTarget.Text = "Self";
            this.cmbTarget.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // EventCommand_HideEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpHideEvent);
            this.Name = "EventCommand_HideEvent";
            this.Size = new System.Drawing.Size(251, 82);
            this.grpHideEvent.ResumeLayout(false);
            this.grpHideEvent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpHideEvent;
        private System.Windows.Forms.Label lblEvent;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbTarget;
    }
}
