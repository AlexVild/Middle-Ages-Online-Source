
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_OpenDeconstructor
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
            this.grpOpenDecon = new DarkUI.Controls.DarkGroupBox();
            this.lblFuelMultiplier = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.nudMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.grpOpenDecon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMultiplier)).BeginInit();
            this.SuspendLayout();
            // 
            // grpOpenDecon
            // 
            this.grpOpenDecon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOpenDecon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOpenDecon.Controls.Add(this.nudMultiplier);
            this.grpOpenDecon.Controls.Add(this.lblFuelMultiplier);
            this.grpOpenDecon.Controls.Add(this.btnCancel);
            this.grpOpenDecon.Controls.Add(this.btnSave);
            this.grpOpenDecon.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOpenDecon.Location = new System.Drawing.Point(3, 3);
            this.grpOpenDecon.Name = "grpOpenDecon";
            this.grpOpenDecon.Size = new System.Drawing.Size(169, 117);
            this.grpOpenDecon.TabIndex = 19;
            this.grpOpenDecon.TabStop = false;
            this.grpOpenDecon.Text = "Deconstructor";
            // 
            // lblFuelMultiplier
            // 
            this.lblFuelMultiplier.AutoSize = true;
            this.lblFuelMultiplier.Location = new System.Drawing.Point(4, 22);
            this.lblFuelMultiplier.Name = "lblFuelMultiplier";
            this.lblFuelMultiplier.Size = new System.Drawing.Size(95, 13);
            this.lblFuelMultiplier.TabIndex = 21;
            this.lblFuelMultiplier.Text = "Fuel Cost Multiplier";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(87, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 84);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // nudMultiplier
            // 
            this.nudMultiplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMultiplier.DecimalPlaces = 2;
            this.nudMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMultiplier.Location = new System.Drawing.Point(7, 46);
            this.nudMultiplier.Name = "nudMultiplier";
            this.nudMultiplier.Size = new System.Drawing.Size(155, 20);
            this.nudMultiplier.TabIndex = 24;
            this.nudMultiplier.Value = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            // 
            // EventCommand_OpenDeconstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpOpenDecon);
            this.Name = "EventCommand_OpenDeconstructor";
            this.Size = new System.Drawing.Size(175, 125);
            this.grpOpenDecon.ResumeLayout(false);
            this.grpOpenDecon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMultiplier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpOpenDecon;
        private System.Windows.Forms.Label lblFuelMultiplier;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkNumericUpDown nudMultiplier;
    }
}
