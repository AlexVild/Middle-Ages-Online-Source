
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeSpawn
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
            this.btnVisual = new DarkUI.Controls.DarkButton();
            this.cmbDirection = new DarkUI.Controls.DarkComboBox();
            this.lblDir = new System.Windows.Forms.Label();
            this.scrlY = new DarkUI.Controls.DarkScrollBar();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.scrlX = new DarkUI.Controls.DarkScrollBar();
            this.grpType = new DarkUI.Controls.DarkGroupBox();
            this.rdoDefault = new DarkUI.Controls.DarkRadioButton();
            this.rdoArena = new DarkUI.Controls.DarkRadioButton();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMap = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.chkResetSpawn = new System.Windows.Forms.CheckBox();
            this.grpChangeSpawnGroup.SuspendLayout();
            this.grpType.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChangeSpawnGroup
            // 
            this.grpChangeSpawnGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeSpawnGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeSpawnGroup.Controls.Add(this.chkResetSpawn);
            this.grpChangeSpawnGroup.Controls.Add(this.btnVisual);
            this.grpChangeSpawnGroup.Controls.Add(this.cmbDirection);
            this.grpChangeSpawnGroup.Controls.Add(this.lblDir);
            this.grpChangeSpawnGroup.Controls.Add(this.scrlY);
            this.grpChangeSpawnGroup.Controls.Add(this.lblY);
            this.grpChangeSpawnGroup.Controls.Add(this.lblX);
            this.grpChangeSpawnGroup.Controls.Add(this.scrlX);
            this.grpChangeSpawnGroup.Controls.Add(this.grpType);
            this.grpChangeSpawnGroup.Controls.Add(this.lblMap);
            this.grpChangeSpawnGroup.Controls.Add(this.cmbMap);
            this.grpChangeSpawnGroup.Controls.Add(this.btnCancel);
            this.grpChangeSpawnGroup.Controls.Add(this.btnSave);
            this.grpChangeSpawnGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeSpawnGroup.Location = new System.Drawing.Point(3, 3);
            this.grpChangeSpawnGroup.Name = "grpChangeSpawnGroup";
            this.grpChangeSpawnGroup.Size = new System.Drawing.Size(356, 237);
            this.grpChangeSpawnGroup.TabIndex = 22;
            this.grpChangeSpawnGroup.TabStop = false;
            this.grpChangeSpawnGroup.Text = "Change Spawn Group";
            // 
            // btnVisual
            // 
            this.btnVisual.Location = new System.Drawing.Point(15, 173);
            this.btnVisual.Name = "btnVisual";
            this.btnVisual.Padding = new System.Windows.Forms.Padding(5);
            this.btnVisual.Size = new System.Drawing.Size(155, 23);
            this.btnVisual.TabIndex = 74;
            this.btnVisual.Text = "Open Visual Interface";
            this.btnVisual.Click += new System.EventHandler(this.btnVisual_Click);
            // 
            // cmbDirection
            // 
            this.cmbDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDirection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDirection.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDirection.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDirection.DrawDropdownHoverOutline = false;
            this.cmbDirection.DrawFocusRectangle = false;
            this.cmbDirection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDirection.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDirection.FormattingEnabled = true;
            this.cmbDirection.Location = new System.Drawing.Point(221, 120);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(121, 21);
            this.cmbDirection.TabIndex = 73;
            this.cmbDirection.Text = null;
            this.cmbDirection.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(192, 123);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(23, 13);
            this.lblDir.TabIndex = 72;
            this.lblDir.Text = "Dir:";
            // 
            // scrlY
            // 
            this.scrlY.Location = new System.Drawing.Point(61, 150);
            this.scrlY.Name = "scrlY";
            this.scrlY.ScrollOrientation = DarkUI.Controls.DarkScrollOrientation.Horizontal;
            this.scrlY.Size = new System.Drawing.Size(121, 17);
            this.scrlY.TabIndex = 71;
            this.scrlY.ValueChanged += new System.EventHandler<DarkUI.Controls.ScrollValueEventArgs>(this.scrlY_ValueChanged);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(29, 150);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(26, 13);
            this.lblY.TabIndex = 23;
            this.lblY.Text = "Y: 0";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(29, 123);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(26, 13);
            this.lblX.TabIndex = 70;
            this.lblX.Text = "X: 0";
            // 
            // scrlX
            // 
            this.scrlX.Location = new System.Drawing.Point(61, 123);
            this.scrlX.Name = "scrlX";
            this.scrlX.ScrollOrientation = DarkUI.Controls.DarkScrollOrientation.Horizontal;
            this.scrlX.Size = new System.Drawing.Size(121, 20);
            this.scrlX.TabIndex = 23;
            this.scrlX.ValueChanged += new System.EventHandler<DarkUI.Controls.ScrollValueEventArgs>(this.scrlX_ValueChanged);
            // 
            // grpType
            // 
            this.grpType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpType.Controls.Add(this.rdoDefault);
            this.grpType.Controls.Add(this.rdoArena);
            this.grpType.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpType.Location = new System.Drawing.Point(100, 19);
            this.grpType.Name = "grpType";
            this.grpType.Size = new System.Drawing.Size(151, 55);
            this.grpType.TabIndex = 69;
            this.grpType.TabStop = false;
            this.grpType.Text = "Type";
            // 
            // rdoDefault
            // 
            this.rdoDefault.AutoSize = true;
            this.rdoDefault.Checked = true;
            this.rdoDefault.Location = new System.Drawing.Point(12, 27);
            this.rdoDefault.Name = "rdoDefault";
            this.rdoDefault.Size = new System.Drawing.Size(73, 17);
            this.rdoDefault.TabIndex = 66;
            this.rdoDefault.TabStop = true;
            this.rdoDefault.Text = "Overworld";
            // 
            // rdoArena
            // 
            this.rdoArena.AutoSize = true;
            this.rdoArena.Location = new System.Drawing.Point(91, 27);
            this.rdoArena.Name = "rdoArena";
            this.rdoArena.Size = new System.Drawing.Size(53, 17);
            this.rdoArena.TabIndex = 67;
            this.rdoArena.Text = "Arena";
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(17, 92);
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
            this.cmbMap.Location = new System.Drawing.Point(51, 89);
            this.cmbMap.Name = "cmbMap";
            this.cmbMap.Size = new System.Drawing.Size(291, 21);
            this.cmbMap.TabIndex = 22;
            this.cmbMap.Text = null;
            this.cmbMap.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(267, 206);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(176, 206);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkResetSpawn
            // 
            this.chkResetSpawn.AutoSize = true;
            this.chkResetSpawn.Location = new System.Drawing.Point(267, 46);
            this.chkResetSpawn.Name = "chkResetSpawn";
            this.chkResetSpawn.Size = new System.Drawing.Size(60, 17);
            this.chkResetSpawn.TabIndex = 75;
            this.chkResetSpawn.Text = "Reset?";
            this.chkResetSpawn.UseVisualStyleBackColor = true;
            this.chkResetSpawn.CheckedChanged += new System.EventHandler(this.chkResetSpawn_CheckedChanged);
            // 
            // EventCommand_ChangeSpawn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeSpawnGroup);
            this.Name = "EventCommand_ChangeSpawn";
            this.Size = new System.Drawing.Size(367, 246);
            this.grpChangeSpawnGroup.ResumeLayout(false);
            this.grpChangeSpawnGroup.PerformLayout();
            this.grpType.ResumeLayout(false);
            this.grpType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeSpawnGroup;
        private DarkUI.Controls.DarkGroupBox grpType;
        internal DarkUI.Controls.DarkRadioButton rdoDefault;
        internal DarkUI.Controls.DarkRadioButton rdoArena;
        private System.Windows.Forms.Label lblMap;
        private DarkUI.Controls.DarkComboBox cmbMap;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkScrollBar scrlX;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private DarkUI.Controls.DarkScrollBar scrlY;
        private System.Windows.Forms.Label lblDir;
        private DarkUI.Controls.DarkComboBox cmbDirection;
        private DarkUI.Controls.DarkButton btnVisual;
        private System.Windows.Forms.CheckBox chkResetSpawn;
    }
}
