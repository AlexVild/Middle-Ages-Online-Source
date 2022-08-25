
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ClearRecord
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
            this.grpOpenLeaderboard = new DarkUI.Controls.DarkGroupBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnOk = new DarkUI.Controls.DarkButton();
            this.grpRecord = new DarkUI.Controls.DarkGroupBox();
            this.cmbType = new DarkUI.Controls.DarkComboBox();
            this.cmbValue = new DarkUI.Controls.DarkComboBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.chkClearAll = new DarkUI.Controls.DarkCheckBox();
            this.grpOpenLeaderboard.SuspendLayout();
            this.grpRecord.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpenLeaderboard
            // 
            this.grpOpenLeaderboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOpenLeaderboard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOpenLeaderboard.Controls.Add(this.chkClearAll);
            this.grpOpenLeaderboard.Controls.Add(this.btnCancel);
            this.grpOpenLeaderboard.Controls.Add(this.btnOk);
            this.grpOpenLeaderboard.Controls.Add(this.grpRecord);
            this.grpOpenLeaderboard.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOpenLeaderboard.Location = new System.Drawing.Point(3, 0);
            this.grpOpenLeaderboard.Name = "grpOpenLeaderboard";
            this.grpOpenLeaderboard.Size = new System.Drawing.Size(314, 146);
            this.grpOpenLeaderboard.TabIndex = 30;
            this.grpOpenLeaderboard.TabStop = false;
            this.grpOpenLeaderboard.Text = "Open Leaderboard";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(232, 115);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(151, 115);
            this.btnOk.Name = "btnOk";
            this.btnOk.Padding = new System.Windows.Forms.Padding(5);
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 29;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grpRecord
            // 
            this.grpRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpRecord.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRecord.Controls.Add(this.cmbType);
            this.grpRecord.Controls.Add(this.cmbValue);
            this.grpRecord.Controls.Add(this.lblRecord);
            this.grpRecord.Controls.Add(this.lblType);
            this.grpRecord.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRecord.Location = new System.Drawing.Point(6, 19);
            this.grpRecord.Name = "grpRecord";
            this.grpRecord.Size = new System.Drawing.Size(301, 90);
            this.grpRecord.TabIndex = 24;
            this.grpRecord.TabStop = false;
            this.grpRecord.Text = "Record Details";
            // 
            // cmbType
            // 
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbType.DrawDropdownHoverOutline = false;
            this.cmbType.DrawFocusRectangle = false;
            this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(84, 26);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(211, 21);
            this.cmbType.TabIndex = 27;
            this.cmbType.Text = null;
            this.cmbType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbValue
            // 
            this.cmbValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbValue.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbValue.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbValue.DrawDropdownHoverOutline = false;
            this.cmbValue.DrawFocusRectangle = false;
            this.cmbValue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Location = new System.Drawing.Point(84, 59);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(211, 21);
            this.cmbValue.TabIndex = 26;
            this.cmbValue.Text = null;
            this.cmbValue.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbValue.SelectedIndexChanged += new System.EventHandler(this.cmbValue_SelectedIndexChanged);
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Location = new System.Drawing.Point(6, 62);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(42, 13);
            this.lblRecord.TabIndex = 25;
            this.lblRecord.Text = "Record";
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
            // chkClearAll
            // 
            this.chkClearAll.AutoSize = true;
            this.chkClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkClearAll.Location = new System.Drawing.Point(38, 119);
            this.chkClearAll.Name = "chkClearAll";
            this.chkClearAll.Size = new System.Drawing.Size(69, 17);
            this.chkClearAll.TabIndex = 56;
            this.chkClearAll.Text = "Clear all?";
            this.chkClearAll.CheckedChanged += new System.EventHandler(this.chkClearAll_CheckedChanged);
            // 
            // EventCommand_ClearRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpOpenLeaderboard);
            this.Name = "EventCommand_ClearRecord";
            this.Size = new System.Drawing.Size(322, 151);
            this.grpOpenLeaderboard.ResumeLayout(false);
            this.grpOpenLeaderboard.PerformLayout();
            this.grpRecord.ResumeLayout(false);
            this.grpRecord.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpOpenLeaderboard;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnOk;
        private DarkUI.Controls.DarkGroupBox grpRecord;
        private DarkUI.Controls.DarkComboBox cmbType;
        private DarkUI.Controls.DarkComboBox cmbValue;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Label lblType;
        private DarkUI.Controls.DarkCheckBox chkClearAll;
    }
}
