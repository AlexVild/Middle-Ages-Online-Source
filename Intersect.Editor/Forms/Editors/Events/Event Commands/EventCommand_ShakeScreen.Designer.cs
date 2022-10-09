
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ShakeScreen
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
            this.grpShake = new DarkUI.Controls.DarkGroupBox();
            this.nudIntensity = new DarkUI.Controls.DarkNumericUpDown();
            this.lblIntensity = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpShake.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // grpShake
            // 
            this.grpShake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpShake.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpShake.Controls.Add(this.nudIntensity);
            this.grpShake.Controls.Add(this.lblIntensity);
            this.grpShake.Controls.Add(this.btnCancel);
            this.grpShake.Controls.Add(this.btnSave);
            this.grpShake.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpShake.Location = new System.Drawing.Point(3, 3);
            this.grpShake.Name = "grpShake";
            this.grpShake.Size = new System.Drawing.Size(259, 79);
            this.grpShake.TabIndex = 18;
            this.grpShake.TabStop = false;
            this.grpShake.Text = "Shake Screen";
            // 
            // nudIntensity
            // 
            this.nudIntensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudIntensity.DecimalPlaces = 2;
            this.nudIntensity.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudIntensity.Location = new System.Drawing.Point(89, 20);
            this.nudIntensity.Name = "nudIntensity";
            this.nudIntensity.Size = new System.Drawing.Size(164, 20);
            this.nudIntensity.TabIndex = 22;
            this.nudIntensity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblIntensity
            // 
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Location = new System.Drawing.Point(4, 22);
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(46, 13);
            this.lblIntensity.TabIndex = 21;
            this.lblIntensity.Text = "Intensity";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(178, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 46);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ShakeScreenCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpShake);
            this.Name = "ShakeScreenCommand";
            this.Size = new System.Drawing.Size(266, 87);
            this.grpShake.ResumeLayout(false);
            this.grpShake.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntensity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpShake;
        private DarkUI.Controls.DarkNumericUpDown nudIntensity;
        private System.Windows.Forms.Label lblIntensity;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
