
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeMapSpawnGroup
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
            this.grpChangeSpawnGroup = new DarkUI.Controls.DarkGroupBox();
            this.chkResetNpcs = new System.Windows.Forms.CheckBox();
            this.nudSpawnGroup = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSpawnGroup = new System.Windows.Forms.Label();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMap = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.chkSurrounding = new System.Windows.Forms.CheckBox();
            this.grpChangeSpawnGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // grpChangeSpawnGroup
            // 
            this.grpChangeSpawnGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeSpawnGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeSpawnGroup.Controls.Add(this.chkSurrounding);
            this.grpChangeSpawnGroup.Controls.Add(this.chkResetNpcs);
            this.grpChangeSpawnGroup.Controls.Add(this.nudSpawnGroup);
            this.grpChangeSpawnGroup.Controls.Add(this.lblSpawnGroup);
            this.grpChangeSpawnGroup.Controls.Add(this.lblMap);
            this.grpChangeSpawnGroup.Controls.Add(this.cmbMap);
            this.grpChangeSpawnGroup.Controls.Add(this.btnCancel);
            this.grpChangeSpawnGroup.Controls.Add(this.btnSave);
            this.grpChangeSpawnGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeSpawnGroup.Location = new System.Drawing.Point(3, 3);
            this.grpChangeSpawnGroup.Name = "grpChangeSpawnGroup";
            this.grpChangeSpawnGroup.Size = new System.Drawing.Size(319, 161);
            this.grpChangeSpawnGroup.TabIndex = 21;
            this.grpChangeSpawnGroup.TabStop = false;
            this.grpChangeSpawnGroup.Text = "Change Spawn Group";
            // 
            // chkResetNpcs
            // 
            this.chkResetNpcs.AutoSize = true;
            this.chkResetNpcs.Location = new System.Drawing.Point(8, 109);
            this.chkResetNpcs.Name = "chkResetNpcs";
            this.chkResetNpcs.Size = new System.Drawing.Size(115, 17);
            this.chkResetNpcs.TabIndex = 64;
            this.chkResetNpcs.Text = "Force NPC Reset?";
            this.chkResetNpcs.UseVisualStyleBackColor = true;
            // 
            // nudSpawnGroup
            // 
            this.nudSpawnGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpawnGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpawnGroup.Location = new System.Drawing.Point(180, 79);
            this.nudSpawnGroup.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSpawnGroup.Name = "nudSpawnGroup";
            this.nudSpawnGroup.Size = new System.Drawing.Size(133, 20);
            this.nudSpawnGroup.TabIndex = 23;
            this.nudSpawnGroup.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblSpawnGroup
            // 
            this.lblSpawnGroup.AutoSize = true;
            this.lblSpawnGroup.Location = new System.Drawing.Point(6, 81);
            this.lblSpawnGroup.Name = "lblSpawnGroup";
            this.lblSpawnGroup.Size = new System.Drawing.Size(72, 13);
            this.lblSpawnGroup.TabIndex = 24;
            this.lblSpawnGroup.Text = "Spawn Group";
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
            this.btnCancel.Location = new System.Drawing.Point(223, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(142, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkSurrounding
            // 
            this.chkSurrounding.AutoSize = true;
            this.chkSurrounding.Location = new System.Drawing.Point(9, 51);
            this.chkSurrounding.Name = "chkSurrounding";
            this.chkSurrounding.Size = new System.Drawing.Size(118, 17);
            this.chkSurrounding.TabIndex = 65;
            this.chkSurrounding.Text = "Surrounding Maps?";
            this.chkSurrounding.UseVisualStyleBackColor = true;
            // 
            // EventCommand_ChangeMapSpawnGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeSpawnGroup);
            this.Name = "EventCommand_ChangeMapSpawnGroup";
            this.Size = new System.Drawing.Size(331, 167);
            this.grpChangeSpawnGroup.ResumeLayout(false);
            this.grpChangeSpawnGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeSpawnGroup;
        private DarkUI.Controls.DarkNumericUpDown nudSpawnGroup;
        private System.Windows.Forms.Label lblSpawnGroup;
        private System.Windows.Forms.Label lblMap;
        private DarkUI.Controls.DarkComboBox cmbMap;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private System.Windows.Forms.CheckBox chkResetNpcs;
        private System.Windows.Forms.CheckBox chkSurrounding;
    }
}
