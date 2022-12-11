
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeBestiary
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
            this.grpChangeBestiary = new DarkUI.Controls.DarkGroupBox();
            this.chkUnlock = new System.Windows.Forms.CheckBox();
            this.lblUnlock = new System.Windows.Forms.Label();
            this.lblBeast = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.cmbBeasts = new DarkUI.Controls.DarkComboBox();
            this.cmbUnlocks = new DarkUI.Controls.DarkComboBox();
            this.grpChangeBestiary.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChangeBestiary
            // 
            this.grpChangeBestiary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeBestiary.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeBestiary.Controls.Add(this.cmbUnlocks);
            this.grpChangeBestiary.Controls.Add(this.cmbBeasts);
            this.grpChangeBestiary.Controls.Add(this.chkUnlock);
            this.grpChangeBestiary.Controls.Add(this.lblUnlock);
            this.grpChangeBestiary.Controls.Add(this.lblBeast);
            this.grpChangeBestiary.Controls.Add(this.btnCancel);
            this.grpChangeBestiary.Controls.Add(this.btnSave);
            this.grpChangeBestiary.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeBestiary.Location = new System.Drawing.Point(3, 3);
            this.grpChangeBestiary.Name = "grpChangeBestiary";
            this.grpChangeBestiary.Size = new System.Drawing.Size(192, 168);
            this.grpChangeBestiary.TabIndex = 18;
            this.grpChangeBestiary.TabStop = false;
            this.grpChangeBestiary.Text = "Change Bestiary";
            // 
            // chkUnlock
            // 
            this.chkUnlock.AutoSize = true;
            this.chkUnlock.Checked = true;
            this.chkUnlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUnlock.Location = new System.Drawing.Point(6, 114);
            this.chkUnlock.Name = "chkUnlock";
            this.chkUnlock.Size = new System.Drawing.Size(78, 17);
            this.chkUnlock.TabIndex = 73;
            this.chkUnlock.Text = "Unlocked?";
            this.chkUnlock.UseVisualStyleBackColor = true;
            // 
            // lblUnlock
            // 
            this.lblUnlock.AutoSize = true;
            this.lblUnlock.Location = new System.Drawing.Point(6, 71);
            this.lblUnlock.Name = "lblUnlock";
            this.lblUnlock.Size = new System.Drawing.Size(41, 13);
            this.lblUnlock.TabIndex = 23;
            this.lblUnlock.Text = "Unlock";
            // 
            // lblBeast
            // 
            this.lblBeast.AutoSize = true;
            this.lblBeast.Location = new System.Drawing.Point(4, 22);
            this.lblBeast.Name = "lblBeast";
            this.lblBeast.Size = new System.Drawing.Size(34, 13);
            this.lblBeast.TabIndex = 21;
            this.lblBeast.Text = "Beast";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(111, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 137);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbBeasts
            // 
            this.cmbBeasts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbBeasts.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbBeasts.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbBeasts.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbBeasts.DrawDropdownHoverOutline = false;
            this.cmbBeasts.DrawFocusRectangle = false;
            this.cmbBeasts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBeasts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeasts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBeasts.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbBeasts.FormattingEnabled = true;
            this.cmbBeasts.Location = new System.Drawing.Point(6, 38);
            this.cmbBeasts.Name = "cmbBeasts";
            this.cmbBeasts.Size = new System.Drawing.Size(180, 21);
            this.cmbBeasts.TabIndex = 74;
            this.cmbBeasts.Text = null;
            this.cmbBeasts.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbUnlocks
            // 
            this.cmbUnlocks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbUnlocks.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbUnlocks.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbUnlocks.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbUnlocks.DrawDropdownHoverOutline = false;
            this.cmbUnlocks.DrawFocusRectangle = false;
            this.cmbUnlocks.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbUnlocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnlocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUnlocks.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbUnlocks.FormattingEnabled = true;
            this.cmbUnlocks.Location = new System.Drawing.Point(6, 87);
            this.cmbUnlocks.Name = "cmbUnlocks";
            this.cmbUnlocks.Size = new System.Drawing.Size(180, 21);
            this.cmbUnlocks.TabIndex = 75;
            this.cmbUnlocks.Text = null;
            this.cmbUnlocks.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // EventCommand_ChangeBestiary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeBestiary);
            this.Name = "EventCommand_ChangeBestiary";
            this.Size = new System.Drawing.Size(200, 180);
            this.grpChangeBestiary.ResumeLayout(false);
            this.grpChangeBestiary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeBestiary;
        private System.Windows.Forms.Label lblUnlock;
        private System.Windows.Forms.Label lblBeast;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private System.Windows.Forms.CheckBox chkUnlock;
        private DarkUI.Controls.DarkComboBox cmbUnlocks;
        private DarkUI.Controls.DarkComboBox cmbBeasts;
    }
}
