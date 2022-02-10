
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_AddInspiration
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
            this.grpAddInspiration = new DarkUI.Controls.DarkGroupBox();
            this.nudSeconds = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpAddInspiration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAddInspiration
            // 
            this.grpAddInspiration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpAddInspiration.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAddInspiration.Controls.Add(this.nudSeconds);
            this.grpAddInspiration.Controls.Add(this.lblSeconds);
            this.grpAddInspiration.Controls.Add(this.btnCancel);
            this.grpAddInspiration.Controls.Add(this.btnSave);
            this.grpAddInspiration.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAddInspiration.Location = new System.Drawing.Point(7, 0);
            this.grpAddInspiration.Name = "grpAddInspiration";
            this.grpAddInspiration.Size = new System.Drawing.Size(232, 98);
            this.grpAddInspiration.TabIndex = 21;
            this.grpAddInspiration.TabStop = false;
            this.grpAddInspiration.Text = "Add Inspiration";
            // 
            // nudSeconds
            // 
            this.nudSeconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSeconds.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSeconds.Location = new System.Drawing.Point(79, 27);
            this.nudSeconds.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeconds.Name = "nudSeconds";
            this.nudSeconds.Size = new System.Drawing.Size(145, 20);
            this.nudSeconds.TabIndex = 23;
            this.nudSeconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSeconds.ValueChanged += new System.EventHandler(this.nudSeconds_ValueChanged);
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(6, 29);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(52, 13);
            this.lblSeconds.TabIndex = 24;
            this.lblSeconds.Text = "Seconds:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(149, 60);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_AddInspiration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpAddInspiration);
            this.Name = "EventCommand_AddInspiration";
            this.Size = new System.Drawing.Size(248, 107);
            this.grpAddInspiration.ResumeLayout(false);
            this.grpAddInspiration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpAddInspiration;
        private DarkUI.Controls.DarkNumericUpDown nudSeconds;
        private System.Windows.Forms.Label lblSeconds;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}