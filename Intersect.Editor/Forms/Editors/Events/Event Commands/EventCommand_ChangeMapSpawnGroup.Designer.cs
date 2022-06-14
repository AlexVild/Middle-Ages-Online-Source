
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
            this.chkPlayerMap = new System.Windows.Forms.CheckBox();
            this.lblBalue = new System.Windows.Forms.Label();
            this.grpOperators = new DarkUI.Controls.DarkGroupBox();
            this.optNumericSet = new DarkUI.Controls.DarkRadioButton();
            this.optNumericSubtract = new DarkUI.Controls.DarkRadioButton();
            this.optNumericAdd = new DarkUI.Controls.DarkRadioButton();
            this.chkSurrounding = new System.Windows.Forms.CheckBox();
            this.chkResetNpcs = new System.Windows.Forms.CheckBox();
            this.nudSpawnGroup = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMap = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpChangeSpawnGroup.SuspendLayout();
            this.grpOperators.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // grpChangeSpawnGroup
            // 
            this.grpChangeSpawnGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeSpawnGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeSpawnGroup.Controls.Add(this.chkPlayerMap);
            this.grpChangeSpawnGroup.Controls.Add(this.lblBalue);
            this.grpChangeSpawnGroup.Controls.Add(this.grpOperators);
            this.grpChangeSpawnGroup.Controls.Add(this.chkSurrounding);
            this.grpChangeSpawnGroup.Controls.Add(this.chkResetNpcs);
            this.grpChangeSpawnGroup.Controls.Add(this.nudSpawnGroup);
            this.grpChangeSpawnGroup.Controls.Add(this.lblMap);
            this.grpChangeSpawnGroup.Controls.Add(this.cmbMap);
            this.grpChangeSpawnGroup.Controls.Add(this.btnCancel);
            this.grpChangeSpawnGroup.Controls.Add(this.btnSave);
            this.grpChangeSpawnGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeSpawnGroup.Location = new System.Drawing.Point(3, 3);
            this.grpChangeSpawnGroup.Name = "grpChangeSpawnGroup";
            this.grpChangeSpawnGroup.Size = new System.Drawing.Size(319, 202);
            this.grpChangeSpawnGroup.TabIndex = 21;
            this.grpChangeSpawnGroup.TabStop = false;
            this.grpChangeSpawnGroup.Text = "Change Spawn Group";
            // 
            // chkPlayerMap
            // 
            this.chkPlayerMap.AutoSize = true;
            this.chkPlayerMap.Location = new System.Drawing.Point(131, 51);
            this.chkPlayerMap.Name = "chkPlayerMap";
            this.chkPlayerMap.Size = new System.Drawing.Size(105, 17);
            this.chkPlayerMap.TabIndex = 71;
            this.chkPlayerMap.Text = "Use player map?";
            this.chkPlayerMap.UseVisualStyleBackColor = true;
            this.chkPlayerMap.CheckedChanged += new System.EventHandler(this.chkPlayerMap_CheckedChanged);
            // 
            // lblBalue
            // 
            this.lblBalue.AutoSize = true;
            this.lblBalue.Location = new System.Drawing.Point(212, 82);
            this.lblBalue.Name = "lblBalue";
            this.lblBalue.Size = new System.Drawing.Size(34, 13);
            this.lblBalue.TabIndex = 70;
            this.lblBalue.Text = "Value";
            // 
            // grpOperators
            // 
            this.grpOperators.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOperators.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOperators.Controls.Add(this.optNumericSet);
            this.grpOperators.Controls.Add(this.optNumericSubtract);
            this.grpOperators.Controls.Add(this.optNumericAdd);
            this.grpOperators.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOperators.Location = new System.Drawing.Point(9, 74);
            this.grpOperators.Name = "grpOperators";
            this.grpOperators.Size = new System.Drawing.Size(197, 55);
            this.grpOperators.TabIndex = 69;
            this.grpOperators.TabStop = false;
            this.grpOperators.Text = "Operators";
            // 
            // optNumericSet
            // 
            this.optNumericSet.AutoSize = true;
            this.optNumericSet.Checked = true;
            this.optNumericSet.Location = new System.Drawing.Point(12, 27);
            this.optNumericSet.Name = "optNumericSet";
            this.optNumericSet.Size = new System.Drawing.Size(41, 17);
            this.optNumericSet.TabIndex = 66;
            this.optNumericSet.TabStop = true;
            this.optNumericSet.Text = "Set";
            this.optNumericSet.CheckedChanged += new System.EventHandler(this.optNumericSet_CheckedChanged);
            // 
            // optNumericSubtract
            // 
            this.optNumericSubtract.AutoSize = true;
            this.optNumericSubtract.Location = new System.Drawing.Point(109, 27);
            this.optNumericSubtract.Name = "optNumericSubtract";
            this.optNumericSubtract.Size = new System.Drawing.Size(65, 17);
            this.optNumericSubtract.TabIndex = 68;
            this.optNumericSubtract.Text = "Subtract";
            this.optNumericSubtract.CheckedChanged += new System.EventHandler(this.optNumericSubtract_CheckedChanged);
            // 
            // optNumericAdd
            // 
            this.optNumericAdd.AutoSize = true;
            this.optNumericAdd.Location = new System.Drawing.Point(59, 27);
            this.optNumericAdd.Name = "optNumericAdd";
            this.optNumericAdd.Size = new System.Drawing.Size(44, 17);
            this.optNumericAdd.TabIndex = 67;
            this.optNumericAdd.Text = "Add";
            this.optNumericAdd.CheckedChanged += new System.EventHandler(this.optNumericAdd_CheckedChanged);
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
            // chkResetNpcs
            // 
            this.chkResetNpcs.AutoSize = true;
            this.chkResetNpcs.Location = new System.Drawing.Point(9, 144);
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
            this.nudSpawnGroup.Location = new System.Drawing.Point(212, 98);
            this.nudSpawnGroup.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSpawnGroup.Name = "nudSpawnGroup";
            this.nudSpawnGroup.Size = new System.Drawing.Size(101, 20);
            this.nudSpawnGroup.TabIndex = 23;
            this.nudSpawnGroup.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
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
            this.btnCancel.Location = new System.Drawing.Point(238, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(131, 169);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ChangeMapSpawnGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeSpawnGroup);
            this.Name = "EventCommand_ChangeMapSpawnGroup";
            this.Size = new System.Drawing.Size(331, 211);
            this.grpChangeSpawnGroup.ResumeLayout(false);
            this.grpChangeSpawnGroup.PerformLayout();
            this.grpOperators.ResumeLayout(false);
            this.grpOperators.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeSpawnGroup;
        private DarkUI.Controls.DarkNumericUpDown nudSpawnGroup;
        private System.Windows.Forms.Label lblMap;
        private DarkUI.Controls.DarkComboBox cmbMap;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private System.Windows.Forms.CheckBox chkResetNpcs;
        private System.Windows.Forms.CheckBox chkSurrounding;
        internal DarkUI.Controls.DarkRadioButton optNumericSet;
        internal DarkUI.Controls.DarkRadioButton optNumericAdd;
        private DarkUI.Controls.DarkGroupBox grpOperators;
        internal DarkUI.Controls.DarkRadioButton optNumericSubtract;
        private System.Windows.Forms.Label lblBalue;
        private System.Windows.Forms.CheckBox chkPlayerMap;
    }
}
