namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ForceNpcExhaustion
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
            this.grpEditor = new DarkUI.Controls.DarkGroupBox();
            this.lblNpc = new System.Windows.Forms.Label();
            this.cmbNpc = new DarkUI.Controls.DarkComboBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.nudWarpX = new DarkUI.Controls.DarkNumericUpDown();
            this.lblDuration = new System.Windows.Forms.Label();
            this.grpEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarpX)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEditor
            // 
            this.grpEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEditor.Controls.Add(this.lblDuration);
            this.grpEditor.Controls.Add(this.nudWarpX);
            this.grpEditor.Controls.Add(this.btnCancel);
            this.grpEditor.Controls.Add(this.btnSave);
            this.grpEditor.Controls.Add(this.cmbNpc);
            this.grpEditor.Controls.Add(this.lblNpc);
            this.grpEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEditor.Location = new System.Drawing.Point(3, 3);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(255, 133);
            this.grpEditor.TabIndex = 24;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Force NPC Exhaustion";
            // 
            // lblNpc
            // 
            this.lblNpc.AutoSize = true;
            this.lblNpc.Location = new System.Drawing.Point(6, 19);
            this.lblNpc.Name = "lblNpc";
            this.lblNpc.Size = new System.Drawing.Size(73, 13);
            this.lblNpc.TabIndex = 26;
            this.lblNpc.Text = "NPC On Map:";
            // 
            // cmbNpc
            // 
            this.cmbNpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNpc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNpc.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNpc.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNpc.DrawDropdownHoverOutline = false;
            this.cmbNpc.DrawFocusRectangle = false;
            this.cmbNpc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNpc.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNpc.FormattingEnabled = true;
            this.cmbNpc.Location = new System.Drawing.Point(9, 35);
            this.cmbNpc.Name = "cmbNpc";
            this.cmbNpc.Size = new System.Drawing.Size(240, 21);
            this.cmbNpc.TabIndex = 27;
            this.cmbNpc.Text = null;
            this.cmbNpc.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(93, 104);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(174, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nudWarpX
            // 
            this.nudWarpX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudWarpX.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudWarpX.Location = new System.Drawing.Point(93, 62);
            this.nudWarpX.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudWarpX.Name = "nudWarpX";
            this.nudWarpX.Size = new System.Drawing.Size(156, 20);
            this.nudWarpX.TabIndex = 34;
            this.nudWarpX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(7, 64);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(72, 13);
            this.lblDuration.TabIndex = 35;
            this.lblDuration.Text = "Duration  (ms)";
            // 
            // EventCommand_ForceNpcExhaustion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpEditor);
            this.Name = "EventCommand_ForceNpcExhaustion";
            this.Size = new System.Drawing.Size(261, 139);
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarpX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpEditor;
        private System.Windows.Forms.Label lblNpc;
        private DarkUI.Controls.DarkComboBox cmbNpc;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkNumericUpDown nudWarpX;
        private System.Windows.Forms.Label lblDuration;
    }
}
