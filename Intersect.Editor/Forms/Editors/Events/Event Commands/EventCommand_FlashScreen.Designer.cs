
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_FlashScreen
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
            this.lblDuration = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.lblIntensity = new System.Windows.Forms.Label();
            this.nudIntensity = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.grpFlashScreen = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(22, 24);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 0;
            this.lblDuration.Text = "Duration:";
            // 
            // nudDuration
            // 
            this.nudDuration.Location = new System.Drawing.Point(78, 22);
            this.nudDuration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(120, 20);
            this.nudDuration.TabIndex = 1;
            this.nudDuration.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(22, 59);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 2;
            this.lblColor.Text = "Color:";
            // 
            // cmbColor
            // 
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Items.AddRange(new object[] {
            "White",
            "Black",
            "Red",
            "Blue",
            "Green",
            "Yellow",
            "Cyan",
            "Orange",
            "Coral"});
            this.cmbColor.Location = new System.Drawing.Point(77, 56);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(121, 21);
            this.cmbColor.TabIndex = 3;
            // 
            // lblIntensity
            // 
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Location = new System.Drawing.Point(22, 95);
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(49, 13);
            this.lblIntensity.TabIndex = 4;
            this.lblIntensity.Text = "Intensity:";
            // 
            // nudIntensity
            // 
            this.nudIntensity.Location = new System.Drawing.Point(77, 93);
            this.nudIntensity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudIntensity.Name = "nudIntensity";
            this.nudIntensity.Size = new System.Drawing.Size(120, 20);
            this.nudIntensity.TabIndex = 5;
            this.nudIntensity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(122, 148);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(25, 148);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "Ok";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // grpFlashScreen
            // 
            this.grpFlashScreen.Location = new System.Drawing.Point(3, 3);
            this.grpFlashScreen.Name = "grpFlashScreen";
            this.grpFlashScreen.Size = new System.Drawing.Size(216, 178);
            this.grpFlashScreen.TabIndex = 8;
            this.grpFlashScreen.TabStop = false;
            this.grpFlashScreen.Text = "Flash Screen";
            // 
            // EventCommand_FlashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.nudIntensity);
            this.Controls.Add(this.lblIntensity);
            this.Controls.Add(this.cmbColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.grpFlashScreen);
            this.Name = "EventCommand_FlashScreen";
            this.Size = new System.Drawing.Size(222, 184);
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.Label lblIntensity;
        private System.Windows.Forms.NumericUpDown nudIntensity;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.GroupBox grpFlashScreen;
    }
}