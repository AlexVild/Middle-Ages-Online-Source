
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_SetVehicle
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpSetVehicle = new DarkUI.Controls.DarkGroupBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.chkInVehicle = new DarkUI.Controls.DarkCheckBox();
            this.nudSpeed = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblSprite = new System.Windows.Forms.Label();
            this.cmbSprites = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpSetVehicle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSetVehicle
            // 
            this.grpSetVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpSetVehicle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSetVehicle.Controls.Add(this.pnlPreview);
            this.grpSetVehicle.Controls.Add(this.chkInVehicle);
            this.grpSetVehicle.Controls.Add(this.nudSpeed);
            this.grpSetVehicle.Controls.Add(this.lblSpeed);
            this.grpSetVehicle.Controls.Add(this.lblSprite);
            this.grpSetVehicle.Controls.Add(this.cmbSprites);
            this.grpSetVehicle.Controls.Add(this.btnCancel);
            this.grpSetVehicle.Controls.Add(this.btnSave);
            this.grpSetVehicle.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetVehicle.Location = new System.Drawing.Point(8, 0);
            this.grpSetVehicle.Name = "grpSetVehicle";
            this.grpSetVehicle.Size = new System.Drawing.Size(319, 170);
            this.grpSetVehicle.TabIndex = 20;
            this.grpSetVehicle.TabStop = false;
            this.grpSetVehicle.Text = "Set Vehicle";
            // 
            // pnlPreview
            // 
            this.pnlPreview.Location = new System.Drawing.Point(228, 19);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(83, 101);
            this.pnlPreview.TabIndex = 55;
            // 
            // chkInVehicle
            // 
            this.chkInVehicle.AutoSize = true;
            this.chkInVehicle.Location = new System.Drawing.Point(48, 19);
            this.chkInVehicle.Name = "chkInVehicle";
            this.chkInVehicle.Size = new System.Drawing.Size(99, 17);
            this.chkInVehicle.TabIndex = 54;
            this.chkInVehicle.Text = "Get In Vehicle?";
            this.chkInVehicle.CheckedChanged += new System.EventHandler(this.chkInVehicle_CheckedChanged);
            // 
            // nudSpeed
            // 
            this.nudSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpeed.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpeed.Location = new System.Drawing.Point(89, 89);
            this.nudSpeed.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(133, 20);
            this.nudSpeed.TabIndex = 23;
            this.nudSpeed.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpeed.ValueChanged += new System.EventHandler(this.nudSpeed_ValueChanged);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(4, 91);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(79, 13);
            this.lblSpeed.TabIndex = 24;
            this.lblSpeed.Text = "Vehicle Speed:";
            // 
            // lblSprite
            // 
            this.lblSprite.AutoSize = true;
            this.lblSprite.Location = new System.Drawing.Point(4, 51);
            this.lblSprite.Name = "lblSprite";
            this.lblSprite.Size = new System.Drawing.Size(75, 13);
            this.lblSprite.TabIndex = 23;
            this.lblSprite.Text = "Vehicle Sprite:";
            // 
            // cmbSprites
            // 
            this.cmbSprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSprites.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSprites.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSprites.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSprites.DrawDropdownHoverOutline = false;
            this.cmbSprites.DrawFocusRectangle = false;
            this.cmbSprites.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSprites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSprites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSprites.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSprites.FormattingEnabled = true;
            this.cmbSprites.Location = new System.Drawing.Point(89, 48);
            this.cmbSprites.Name = "cmbSprites";
            this.cmbSprites.Size = new System.Drawing.Size(133, 21);
            this.cmbSprites.TabIndex = 22;
            this.cmbSprites.Text = null;
            this.cmbSprites.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSprites.SelectedIndexChanged += new System.EventHandler(this.cmbSprites_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(127, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(27, 128);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_SetVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpSetVehicle);
            this.Name = "EventCommand_SetVehicle";
            this.Size = new System.Drawing.Size(335, 175);
            this.grpSetVehicle.ResumeLayout(false);
            this.grpSetVehicle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpSetVehicle;
        private DarkUI.Controls.DarkComboBox cmbSprites;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblSprite;
        private DarkUI.Controls.DarkNumericUpDown nudSpeed;
        private DarkUI.Controls.DarkCheckBox chkInVehicle;
        private System.Windows.Forms.Panel pnlPreview;
    }
}