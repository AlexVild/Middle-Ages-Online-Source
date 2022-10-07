
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ResetVariables
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
            this.grpResetVars = new DarkUI.Controls.DarkGroupBox();
            this.lblVarGroup = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.cmbGroups = new DarkUI.Controls.DarkComboBox();
            this.chkPartySync = new DarkUI.Controls.DarkCheckBox();
            this.chkTriggerCommon = new DarkUI.Controls.DarkCheckBox();
            this.grpResetVars.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResetVars
            // 
            this.grpResetVars.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpResetVars.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpResetVars.Controls.Add(this.chkTriggerCommon);
            this.grpResetVars.Controls.Add(this.chkPartySync);
            this.grpResetVars.Controls.Add(this.cmbGroups);
            this.grpResetVars.Controls.Add(this.lblVarGroup);
            this.grpResetVars.Controls.Add(this.btnCancel);
            this.grpResetVars.Controls.Add(this.btnSave);
            this.grpResetVars.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpResetVars.Location = new System.Drawing.Point(3, 3);
            this.grpResetVars.Name = "grpResetVars";
            this.grpResetVars.Size = new System.Drawing.Size(330, 130);
            this.grpResetVars.TabIndex = 22;
            this.grpResetVars.TabStop = false;
            this.grpResetVars.Text = "Reset Variables In...";
            // 
            // lblVarGroup
            // 
            this.lblVarGroup.AutoSize = true;
            this.lblVarGroup.Location = new System.Drawing.Point(6, 29);
            this.lblVarGroup.Name = "lblVarGroup";
            this.lblVarGroup.Size = new System.Drawing.Size(90, 13);
            this.lblVarGroup.TabIndex = 24;
            this.lblVarGroup.Text = "Player Var Group:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(249, 101);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(168, 101);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbGroups
            // 
            this.cmbGroups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbGroups.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbGroups.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbGroups.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbGroups.DrawDropdownHoverOutline = false;
            this.cmbGroups.DrawFocusRectangle = false;
            this.cmbGroups.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGroups.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(102, 26);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(222, 21);
            this.cmbGroups.TabIndex = 25;
            this.cmbGroups.Text = null;
            this.cmbGroups.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // chkPartySync
            // 
            this.chkPartySync.AutoSize = true;
            this.chkPartySync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkPartySync.Location = new System.Drawing.Point(9, 59);
            this.chkPartySync.Name = "chkPartySync";
            this.chkPartySync.Size = new System.Drawing.Size(83, 17);
            this.chkPartySync.TabIndex = 43;
            this.chkPartySync.Text = "Party Sync?";
            // 
            // chkTriggerCommon
            // 
            this.chkTriggerCommon.AutoSize = true;
            this.chkTriggerCommon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.chkTriggerCommon.Location = new System.Drawing.Point(124, 57);
            this.chkTriggerCommon.Name = "chkTriggerCommon";
            this.chkTriggerCommon.Size = new System.Drawing.Size(145, 17);
            this.chkTriggerCommon.TabIndex = 44;
            this.chkTriggerCommon.Text = "Trigger Common Events?";
            // 
            // EventCommand_ResetVariables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpResetVars);
            this.Name = "EventCommand_ResetVariables";
            this.Size = new System.Drawing.Size(336, 136);
            this.grpResetVars.ResumeLayout(false);
            this.grpResetVars.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpResetVars;
        private System.Windows.Forms.Label lblVarGroup;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbGroups;
        private DarkUI.Controls.DarkCheckBox chkTriggerCommon;
        private DarkUI.Controls.DarkCheckBox chkPartySync;
    }
}
