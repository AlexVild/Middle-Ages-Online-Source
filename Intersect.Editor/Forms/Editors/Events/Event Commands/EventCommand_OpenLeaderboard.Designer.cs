
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_OpenLeaderboard
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
            this.grpBoardDetails = new DarkUI.Controls.DarkGroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtDisplayName = new DarkUI.Controls.DarkTextBox();
            this.grpDisplay = new DarkUI.Controls.DarkGroupBox();
            this.rdoTime = new DarkUI.Controls.DarkRadioButton();
            this.rdoVal = new DarkUI.Controls.DarkRadioButton();
            this.grpOrder = new DarkUI.Controls.DarkGroupBox();
            this.rdoDesc = new DarkUI.Controls.DarkRadioButton();
            this.rdoAsc = new DarkUI.Controls.DarkRadioButton();
            this.grpOpenLeaderboard.SuspendLayout();
            this.grpRecord.SuspendLayout();
            this.grpBoardDetails.SuspendLayout();
            this.grpDisplay.SuspendLayout();
            this.grpOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpenLeaderboard
            // 
            this.grpOpenLeaderboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOpenLeaderboard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOpenLeaderboard.Controls.Add(this.btnCancel);
            this.grpOpenLeaderboard.Controls.Add(this.btnOk);
            this.grpOpenLeaderboard.Controls.Add(this.grpRecord);
            this.grpOpenLeaderboard.Controls.Add(this.grpBoardDetails);
            this.grpOpenLeaderboard.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOpenLeaderboard.Location = new System.Drawing.Point(3, 0);
            this.grpOpenLeaderboard.Name = "grpOpenLeaderboard";
            this.grpOpenLeaderboard.Size = new System.Drawing.Size(314, 318);
            this.grpOpenLeaderboard.TabIndex = 29;
            this.grpOpenLeaderboard.TabStop = false;
            this.grpOpenLeaderboard.Text = "Open Leaderboard";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(233, 285);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(143, 285);
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
            this.grpRecord.Size = new System.Drawing.Size(301, 92);
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
            // grpBoardDetails
            // 
            this.grpBoardDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpBoardDetails.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBoardDetails.Controls.Add(this.lblName);
            this.grpBoardDetails.Controls.Add(this.txtDisplayName);
            this.grpBoardDetails.Controls.Add(this.grpDisplay);
            this.grpBoardDetails.Controls.Add(this.grpOrder);
            this.grpBoardDetails.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBoardDetails.Location = new System.Drawing.Point(6, 117);
            this.grpBoardDetails.Name = "grpBoardDetails";
            this.grpBoardDetails.Size = new System.Drawing.Size(301, 162);
            this.grpBoardDetails.TabIndex = 28;
            this.grpBoardDetails.TabStop = false;
            this.grpBoardDetails.Text = "Board Details";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 32);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(72, 13);
            this.lblName.TabIndex = 28;
            this.lblName.Text = "Display Name";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDisplayName.Location = new System.Drawing.Point(84, 30);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDisplayName.Size = new System.Drawing.Size(192, 20);
            this.txtDisplayName.TabIndex = 57;
            this.txtDisplayName.TextChanged += new System.EventHandler(this.txtDisplayName_TextChanged);
            // 
            // grpDisplay
            // 
            this.grpDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpDisplay.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDisplay.Controls.Add(this.rdoTime);
            this.grpDisplay.Controls.Add(this.rdoVal);
            this.grpDisplay.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDisplay.Location = new System.Drawing.Point(9, 113);
            this.grpDisplay.Name = "grpDisplay";
            this.grpDisplay.Size = new System.Drawing.Size(203, 43);
            this.grpDisplay.TabIndex = 31;
            this.grpDisplay.TabStop = false;
            this.grpDisplay.Text = "Display Mode";
            // 
            // rdoTime
            // 
            this.rdoTime.AutoSize = true;
            this.rdoTime.Location = new System.Drawing.Point(100, 19);
            this.rdoTime.Name = "rdoTime";
            this.rdoTime.Size = new System.Drawing.Size(48, 17);
            this.rdoTime.TabIndex = 29;
            this.rdoTime.Text = "Time";
            this.rdoTime.CheckedChanged += new System.EventHandler(this.rdoTime_CheckedChanged);
            // 
            // rdoVal
            // 
            this.rdoVal.AutoSize = true;
            this.rdoVal.Checked = true;
            this.rdoVal.Location = new System.Drawing.Point(6, 19);
            this.rdoVal.Name = "rdoVal";
            this.rdoVal.Size = new System.Drawing.Size(52, 17);
            this.rdoVal.TabIndex = 27;
            this.rdoVal.TabStop = true;
            this.rdoVal.Text = "Value";
            this.rdoVal.CheckedChanged += new System.EventHandler(this.rdoVal_CheckedChanged);
            // 
            // grpOrder
            // 
            this.grpOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOrder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOrder.Controls.Add(this.rdoDesc);
            this.grpOrder.Controls.Add(this.rdoAsc);
            this.grpOrder.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOrder.Location = new System.Drawing.Point(9, 58);
            this.grpOrder.Name = "grpOrder";
            this.grpOrder.Size = new System.Drawing.Size(203, 49);
            this.grpOrder.TabIndex = 30;
            this.grpOrder.TabStop = false;
            this.grpOrder.Text = "Sort Order";
            // 
            // rdoDesc
            // 
            this.rdoDesc.AutoSize = true;
            this.rdoDesc.Checked = true;
            this.rdoDesc.Location = new System.Drawing.Point(6, 19);
            this.rdoDesc.Name = "rdoDesc";
            this.rdoDesc.Size = new System.Drawing.Size(82, 17);
            this.rdoDesc.TabIndex = 29;
            this.rdoDesc.TabStop = true;
            this.rdoDesc.Text = "Descending";
            this.rdoDesc.CheckedChanged += new System.EventHandler(this.rdoDesc_CheckedChanged);
            // 
            // rdoAsc
            // 
            this.rdoAsc.AutoSize = true;
            this.rdoAsc.Location = new System.Drawing.Point(100, 19);
            this.rdoAsc.Name = "rdoAsc";
            this.rdoAsc.Size = new System.Drawing.Size(75, 17);
            this.rdoAsc.TabIndex = 27;
            this.rdoAsc.Text = "Ascending";
            this.rdoAsc.CheckedChanged += new System.EventHandler(this.rdoAsc_CheckedChanged);
            // 
            // EventCommand_OpenLeaderboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpOpenLeaderboard);
            this.Name = "EventCommand_OpenLeaderboard";
            this.Size = new System.Drawing.Size(320, 323);
            this.grpOpenLeaderboard.ResumeLayout(false);
            this.grpRecord.ResumeLayout(false);
            this.grpRecord.PerformLayout();
            this.grpBoardDetails.ResumeLayout(false);
            this.grpBoardDetails.PerformLayout();
            this.grpDisplay.ResumeLayout(false);
            this.grpDisplay.PerformLayout();
            this.grpOrder.ResumeLayout(false);
            this.grpOrder.PerformLayout();
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
        private DarkUI.Controls.DarkGroupBox grpBoardDetails;
        private DarkUI.Controls.DarkGroupBox grpDisplay;
        private DarkUI.Controls.DarkRadioButton rdoTime;
        private DarkUI.Controls.DarkRadioButton rdoVal;
        private DarkUI.Controls.DarkGroupBox grpOrder;
        private DarkUI.Controls.DarkRadioButton rdoDesc;
        private DarkUI.Controls.DarkRadioButton rdoAsc;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtDisplayName;
    }
}
