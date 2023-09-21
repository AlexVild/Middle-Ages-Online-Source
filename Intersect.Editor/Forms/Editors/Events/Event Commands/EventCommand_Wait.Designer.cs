using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandWait
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
            this.grpWait = new DarkUI.Controls.DarkGroupBox();
            this.nudWait = new DarkUI.Controls.DarkNumericUpDown();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnShort = new DarkUI.Controls.DarkButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMedium = new DarkUI.Controls.DarkButton();
            this.btn1sec = new DarkUI.Controls.DarkButton();
            this.grpWait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWait)).BeginInit();
            this.SuspendLayout();
            // 
            // grpWait
            // 
            this.grpWait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpWait.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWait.Controls.Add(this.btn1sec);
            this.grpWait.Controls.Add(this.btnMedium);
            this.grpWait.Controls.Add(this.label1);
            this.grpWait.Controls.Add(this.btnShort);
            this.grpWait.Controls.Add(this.nudWait);
            this.grpWait.Controls.Add(this.lblWait);
            this.grpWait.Controls.Add(this.btnCancel);
            this.grpWait.Controls.Add(this.btnSave);
            this.grpWait.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWait.Location = new System.Drawing.Point(3, 3);
            this.grpWait.Name = "grpWait";
            this.grpWait.Size = new System.Drawing.Size(266, 146);
            this.grpWait.TabIndex = 17;
            this.grpWait.TabStop = false;
            this.grpWait.Text = "Wait:";
            // 
            // nudWait
            // 
            this.nudWait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudWait.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudWait.Location = new System.Drawing.Point(89, 22);
            this.nudWait.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudWait.Name = "nudWait";
            this.nudWait.Size = new System.Drawing.Size(171, 20);
            this.nudWait.TabIndex = 35;
            this.nudWait.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudWait.ValueChanged += new System.EventHandler(this.nudWait_ValueChanged);
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.Location = new System.Drawing.Point(4, 22);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(54, 13);
            this.lblWait.TabIndex = 21;
            this.lblWait.Text = "Wait (ms):";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(169, 117);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(88, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnShort
            // 
            this.btnShort.Location = new System.Drawing.Point(6, 61);
            this.btnShort.Name = "btnShort";
            this.btnShort.Padding = new System.Windows.Forms.Padding(5);
            this.btnShort.Size = new System.Drawing.Size(90, 23);
            this.btnShort.TabIndex = 36;
            this.btnShort.Text = "Dialog Pause";
            this.btnShort.Click += new System.EventHandler(this.btnShort_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Standardized Options:";
            // 
            // btnMedium
            // 
            this.btnMedium.Location = new System.Drawing.Point(102, 61);
            this.btnMedium.Name = "btnMedium";
            this.btnMedium.Padding = new System.Windows.Forms.Padding(5);
            this.btnMedium.Size = new System.Drawing.Size(158, 23);
            this.btnMedium.TabIndex = 38;
            this.btnMedium.Text = "After Movement/Animation";
            this.btnMedium.Click += new System.EventHandler(this.btnMedium_Click);
            // 
            // btn1sec
            // 
            this.btn1sec.Location = new System.Drawing.Point(7, 90);
            this.btn1sec.Name = "btn1sec";
            this.btn1sec.Padding = new System.Windows.Forms.Padding(5);
            this.btn1sec.Size = new System.Drawing.Size(87, 23);
            this.btn1sec.TabIndex = 39;
            this.btn1sec.Text = "Dramatic";
            this.btn1sec.Click += new System.EventHandler(this.btn1sec_Click);
            // 
            // EventCommandWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpWait);
            this.Name = "EventCommandWait";
            this.Size = new System.Drawing.Size(279, 154);
            this.grpWait.ResumeLayout(false);
            this.grpWait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpWait;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private System.Windows.Forms.Label lblWait;
        private DarkNumericUpDown nudWait;
        private DarkButton btn1sec;
        private DarkButton btnMedium;
        private System.Windows.Forms.Label label1;
        private DarkButton btnShort;
    }
}
