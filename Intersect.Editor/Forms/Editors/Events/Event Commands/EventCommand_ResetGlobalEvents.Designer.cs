
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ResetGlobalEvents
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
            this.grpResetPermadeads = new DarkUI.Controls.DarkGroupBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMap = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpResetPermadeads.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResetPermadeads
            // 
            this.grpResetPermadeads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpResetPermadeads.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpResetPermadeads.Controls.Add(this.lblMap);
            this.grpResetPermadeads.Controls.Add(this.cmbMap);
            this.grpResetPermadeads.Controls.Add(this.btnCancel);
            this.grpResetPermadeads.Controls.Add(this.btnSave);
            this.grpResetPermadeads.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpResetPermadeads.Location = new System.Drawing.Point(3, 3);
            this.grpResetPermadeads.Name = "grpResetPermadeads";
            this.grpResetPermadeads.Size = new System.Drawing.Size(323, 79);
            this.grpResetPermadeads.TabIndex = 23;
            this.grpResetPermadeads.TabStop = false;
            this.grpResetPermadeads.Text = "Change Spawn Group";
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(6, 22);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(28, 13);
            this.lblMap.TabIndex = 23;
            this.lblMap.Text = "Map";
            // 
            // cmbMap
            // 
            this.cmbMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMap.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMap.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMap.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMap.DrawDropdownHoverOutline = false;
            this.cmbMap.DrawFocusRectangle = false;
            this.cmbMap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMap.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMap.FormattingEnabled = true;
            this.cmbMap.Location = new System.Drawing.Point(62, 19);
            this.cmbMap.Name = "cmbMap";
            this.cmbMap.Size = new System.Drawing.Size(251, 21);
            this.cmbMap.TabIndex = 22;
            this.cmbMap.Text = null;
            this.cmbMap.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(238, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(157, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ResetGlobalEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpResetPermadeads);
            this.Name = "EventCommand_ResetGlobalEvents";
            this.Size = new System.Drawing.Size(331, 90);
            this.grpResetPermadeads.ResumeLayout(false);
            this.grpResetPermadeads.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpResetPermadeads;
        private System.Windows.Forms.Label lblMap;
        private DarkUI.Controls.DarkComboBox cmbMap;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
