using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandWarp
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
            this.grpWarp = new DarkUI.Controls.DarkGroupBox();
            this.lblCommonSounds = new System.Windows.Forms.Label();
            this.cmbCommonSounds = new DarkUI.Controls.DarkComboBox();
            this.lblSound = new System.Windows.Forms.Label();
            this.cmbSound = new DarkUI.Controls.DarkComboBox();
            this.chkChangeInstance = new System.Windows.Forms.CheckBox();
            this.grpInstanceSettings = new DarkUI.Controls.DarkGroupBox();
            this.lblDungeon = new System.Windows.Forms.Label();
            this.cmbDungeon = new DarkUI.Controls.DarkComboBox();
            this.cmbInstanceType = new DarkUI.Controls.DarkComboBox();
            this.lblInstanceType = new System.Windows.Forms.Label();
            this.chkMapFade = new System.Windows.Forms.CheckBox();
            this.btnVisual = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.scrlY = new DarkUI.Controls.DarkScrollBar();
            this.scrlX = new DarkUI.Controls.DarkScrollBar();
            this.cmbMap = new DarkUI.Controls.DarkComboBox();
            this.cmbDirection = new DarkUI.Controls.DarkComboBox();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblMap = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblLives = new System.Windows.Forms.Label();
            this.nudSharedLives = new DarkUI.Controls.DarkNumericUpDown();
            this.grpWarp.SuspendLayout();
            this.grpInstanceSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSharedLives)).BeginInit();
            this.SuspendLayout();
            // 
            // grpWarp
            // 
            this.grpWarp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpWarp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWarp.Controls.Add(this.lblCommonSounds);
            this.grpWarp.Controls.Add(this.cmbCommonSounds);
            this.grpWarp.Controls.Add(this.lblSound);
            this.grpWarp.Controls.Add(this.cmbSound);
            this.grpWarp.Controls.Add(this.chkChangeInstance);
            this.grpWarp.Controls.Add(this.grpInstanceSettings);
            this.grpWarp.Controls.Add(this.chkMapFade);
            this.grpWarp.Controls.Add(this.btnVisual);
            this.grpWarp.Controls.Add(this.btnCancel);
            this.grpWarp.Controls.Add(this.btnSave);
            this.grpWarp.Controls.Add(this.scrlY);
            this.grpWarp.Controls.Add(this.scrlX);
            this.grpWarp.Controls.Add(this.cmbMap);
            this.grpWarp.Controls.Add(this.cmbDirection);
            this.grpWarp.Controls.Add(this.lblDir);
            this.grpWarp.Controls.Add(this.lblY);
            this.grpWarp.Controls.Add(this.lblMap);
            this.grpWarp.Controls.Add(this.lblX);
            this.grpWarp.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWarp.Location = new System.Drawing.Point(3, 3);
            this.grpWarp.Name = "grpWarp";
            this.grpWarp.Size = new System.Drawing.Size(374, 313);
            this.grpWarp.TabIndex = 17;
            this.grpWarp.TabStop = false;
            this.grpWarp.Text = "saw";
            // 
            // lblCommonSounds
            // 
            this.lblCommonSounds.AutoSize = true;
            this.lblCommonSounds.Location = new System.Drawing.Point(95, 256);
            this.lblCommonSounds.Name = "lblCommonSounds";
            this.lblCommonSounds.Size = new System.Drawing.Size(90, 13);
            this.lblCommonSounds.TabIndex = 67;
            this.lblCommonSounds.Text = "Common Sounds:";
            // 
            // cmbCommonSounds
            // 
            this.cmbCommonSounds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCommonSounds.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCommonSounds.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCommonSounds.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCommonSounds.DrawDropdownHoverOutline = false;
            this.cmbCommonSounds.DrawFocusRectangle = false;
            this.cmbCommonSounds.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCommonSounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommonSounds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCommonSounds.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCommonSounds.FormattingEnabled = true;
            this.cmbCommonSounds.Location = new System.Drawing.Point(191, 253);
            this.cmbCommonSounds.Name = "cmbCommonSounds";
            this.cmbCommonSounds.Size = new System.Drawing.Size(175, 21);
            this.cmbCommonSounds.TabIndex = 66;
            this.cmbCommonSounds.Text = null;
            this.cmbCommonSounds.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCommonSounds.SelectedIndexChanged += new System.EventHandler(this.cmbCommonSounds_SelectedIndexChanged);
            // 
            // lblSound
            // 
            this.lblSound.AutoSize = true;
            this.lblSound.Location = new System.Drawing.Point(12, 229);
            this.lblSound.Name = "lblSound";
            this.lblSound.Size = new System.Drawing.Size(41, 13);
            this.lblSound.TabIndex = 65;
            this.lblSound.Text = "Sound:";
            // 
            // cmbSound
            // 
            this.cmbSound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSound.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSound.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSound.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSound.DrawDropdownHoverOutline = false;
            this.cmbSound.DrawFocusRectangle = false;
            this.cmbSound.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSound.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSound.FormattingEnabled = true;
            this.cmbSound.Location = new System.Drawing.Point(59, 226);
            this.cmbSound.Name = "cmbSound";
            this.cmbSound.Size = new System.Drawing.Size(307, 21);
            this.cmbSound.TabIndex = 64;
            this.cmbSound.Text = null;
            this.cmbSound.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // chkChangeInstance
            // 
            this.chkChangeInstance.AutoSize = true;
            this.chkChangeInstance.Location = new System.Drawing.Point(183, 63);
            this.chkChangeInstance.Name = "chkChangeInstance";
            this.chkChangeInstance.Size = new System.Drawing.Size(112, 17);
            this.chkChangeInstance.TabIndex = 63;
            this.chkChangeInstance.Text = "Change instance?";
            this.chkChangeInstance.UseVisualStyleBackColor = true;
            this.chkChangeInstance.CheckedChanged += new System.EventHandler(this.chkCreateInstance_CheckedChanged);
            // 
            // grpInstanceSettings
            // 
            this.grpInstanceSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpInstanceSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpInstanceSettings.Controls.Add(this.nudSharedLives);
            this.grpInstanceSettings.Controls.Add(this.lblLives);
            this.grpInstanceSettings.Controls.Add(this.lblDungeon);
            this.grpInstanceSettings.Controls.Add(this.cmbDungeon);
            this.grpInstanceSettings.Controls.Add(this.cmbInstanceType);
            this.grpInstanceSettings.Controls.Add(this.lblInstanceType);
            this.grpInstanceSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInstanceSettings.Location = new System.Drawing.Point(183, 86);
            this.grpInstanceSettings.Name = "grpInstanceSettings";
            this.grpInstanceSettings.Size = new System.Drawing.Size(183, 134);
            this.grpInstanceSettings.TabIndex = 62;
            this.grpInstanceSettings.TabStop = false;
            this.grpInstanceSettings.Text = "Instance Settings";
            this.grpInstanceSettings.Visible = false;
            // 
            // lblDungeon
            // 
            this.lblDungeon.AutoSize = true;
            this.lblDungeon.Location = new System.Drawing.Point(6, 88);
            this.lblDungeon.Name = "lblDungeon";
            this.lblDungeon.Size = new System.Drawing.Size(54, 13);
            this.lblDungeon.TabIndex = 66;
            this.lblDungeon.Text = "Dungeon:";
            // 
            // cmbDungeon
            // 
            this.cmbDungeon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDungeon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDungeon.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDungeon.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDungeon.DrawDropdownHoverOutline = false;
            this.cmbDungeon.DrawFocusRectangle = false;
            this.cmbDungeon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDungeon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDungeon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDungeon.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDungeon.FormattingEnabled = true;
            this.cmbDungeon.Location = new System.Drawing.Point(8, 104);
            this.cmbDungeon.Name = "cmbDungeon";
            this.cmbDungeon.Size = new System.Drawing.Size(169, 21);
            this.cmbDungeon.TabIndex = 65;
            this.cmbDungeon.Text = null;
            this.cmbDungeon.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // cmbInstanceType
            // 
            this.cmbInstanceType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbInstanceType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbInstanceType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbInstanceType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbInstanceType.DrawDropdownHoverOutline = false;
            this.cmbInstanceType.DrawFocusRectangle = false;
            this.cmbInstanceType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbInstanceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInstanceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbInstanceType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbInstanceType.FormattingEnabled = true;
            this.cmbInstanceType.Location = new System.Drawing.Point(8, 32);
            this.cmbInstanceType.Name = "cmbInstanceType";
            this.cmbInstanceType.Size = new System.Drawing.Size(169, 21);
            this.cmbInstanceType.TabIndex = 64;
            this.cmbInstanceType.Text = null;
            this.cmbInstanceType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbInstanceType.SelectedIndexChanged += new System.EventHandler(this.darkComboBox1_SelectedIndexChanged);
            // 
            // lblInstanceType
            // 
            this.lblInstanceType.AutoSize = true;
            this.lblInstanceType.Location = new System.Drawing.Point(6, 16);
            this.lblInstanceType.Name = "lblInstanceType";
            this.lblInstanceType.Size = new System.Drawing.Size(78, 13);
            this.lblInstanceType.TabIndex = 64;
            this.lblInstanceType.Text = "Instance Type:";
            // 
            // chkMapFade
            // 
            this.chkMapFade.AutoSize = true;
            this.chkMapFade.Location = new System.Drawing.Point(12, 43);
            this.chkMapFade.Name = "chkMapFade";
            this.chkMapFade.Size = new System.Drawing.Size(101, 17);
            this.chkMapFade.TabIndex = 22;
            this.chkMapFade.Text = "Fade transition?";
            this.chkMapFade.UseVisualStyleBackColor = true;
            this.chkMapFade.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnVisual
            // 
            this.btnVisual.Location = new System.Drawing.Point(12, 165);
            this.btnVisual.Name = "btnVisual";
            this.btnVisual.Padding = new System.Windows.Forms.Padding(5);
            this.btnVisual.Size = new System.Drawing.Size(155, 23);
            this.btnVisual.TabIndex = 21;
            this.btnVisual.Text = "Open Visual Interface";
            this.btnVisual.Click += new System.EventHandler(this.btnVisual_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(291, 284);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(210, 284);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // scrlY
            // 
            this.scrlY.Location = new System.Drawing.Point(46, 93);
            this.scrlY.Name = "scrlY";
            this.scrlY.ScrollOrientation = DarkUI.Controls.DarkScrollOrientation.Horizontal;
            this.scrlY.Size = new System.Drawing.Size(121, 17);
            this.scrlY.TabIndex = 18;
            this.scrlY.ValueChanged += new System.EventHandler<DarkUI.Controls.ScrollValueEventArgs>(this.scrlY_Scroll);
            // 
            // scrlX
            // 
            this.scrlX.Location = new System.Drawing.Point(46, 67);
            this.scrlX.Name = "scrlX";
            this.scrlX.ScrollOrientation = DarkUI.Controls.DarkScrollOrientation.Horizontal;
            this.scrlX.Size = new System.Drawing.Size(121, 17);
            this.scrlX.TabIndex = 17;
            this.scrlX.ValueChanged += new System.EventHandler<DarkUI.Controls.ScrollValueEventArgs>(this.scrlX_Scroll);
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
            this.cmbMap.Location = new System.Drawing.Point(46, 16);
            this.cmbMap.Name = "cmbMap";
            this.cmbMap.Size = new System.Drawing.Size(301, 21);
            this.cmbMap.TabIndex = 16;
            this.cmbMap.Text = null;
            this.cmbMap.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbMap.SelectedIndexChanged += new System.EventHandler(this.cmbMap_SelectedIndexChanged);
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
            this.cmbDirection.Location = new System.Drawing.Point(46, 132);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(121, 21);
            this.cmbDirection.TabIndex = 15;
            this.cmbDirection.Text = null;
            this.cmbDirection.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDirection.SelectedIndexChanged += new System.EventHandler(this.cmbDirection_SelectedIndexChanged);
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(9, 135);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(23, 13);
            this.lblDir.TabIndex = 14;
            this.lblDir.Text = "Dir:";
            this.lblDir.Click += new System.EventHandler(this.label23_Click);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(9, 93);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(26, 13);
            this.lblY.TabIndex = 13;
            this.lblY.Text = "Y: 0";
            this.lblY.Click += new System.EventHandler(this.lblY_Click);
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(9, 19);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(31, 13);
            this.lblMap.TabIndex = 8;
            this.lblMap.Text = "Map:";
            this.lblMap.Click += new System.EventHandler(this.label21_Click);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(9, 67);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(26, 13);
            this.lblX.TabIndex = 12;
            this.lblX.Text = "X: 0";
            this.lblX.Click += new System.EventHandler(this.lblX_Click);
            // 
            // lblLives
            // 
            this.lblLives.AutoSize = true;
            this.lblLives.Location = new System.Drawing.Point(6, 61);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(72, 13);
            this.lblLives.TabIndex = 67;
            this.lblLives.Text = "Shared Lives:";
            // 
            // nudSharedLives
            // 
            this.nudSharedLives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSharedLives.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSharedLives.Location = new System.Drawing.Point(84, 59);
            this.nudSharedLives.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSharedLives.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSharedLives.Name = "nudSharedLives";
            this.nudSharedLives.Size = new System.Drawing.Size(93, 20);
            this.nudSharedLives.TabIndex = 121;
            this.nudSharedLives.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // EventCommandWarp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpWarp);
            this.Name = "EventCommandWarp";
            this.Size = new System.Drawing.Size(383, 324);
            this.grpWarp.ResumeLayout(false);
            this.grpWarp.PerformLayout();
            this.grpInstanceSettings.ResumeLayout(false);
            this.grpInstanceSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSharedLives)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpWarp;
        private DarkComboBox cmbMap;
        private DarkComboBox cmbDirection;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.Label lblX;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkScrollBar scrlY;
        private DarkScrollBar scrlX;
        private DarkButton btnVisual;
        private System.Windows.Forms.CheckBox chkMapFade;
        private System.Windows.Forms.CheckBox chkChangeInstance;
        private DarkGroupBox grpInstanceSettings;
        private DarkComboBox cmbInstanceType;
        private System.Windows.Forms.Label lblInstanceType;
        private System.Windows.Forms.Label lblDungeon;
        private DarkComboBox cmbDungeon;
        private System.Windows.Forms.Label lblSound;
        private DarkComboBox cmbSound;
        private System.Windows.Forms.Label lblCommonSounds;
        private DarkComboBox cmbCommonSounds;
        private System.Windows.Forms.Label lblLives;
        private DarkNumericUpDown nudSharedLives;
    }
}
