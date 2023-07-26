
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeChampionSettings
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
            this.grpChangeItems = new DarkUI.Controls.DarkGroupBox();
            this.grpAction = new DarkUI.Controls.DarkGroupBox();
            this.rdoDisable = new DarkUI.Controls.DarkRadioButton();
            this.rdoEnable = new DarkUI.Controls.DarkRadioButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpChangeItems.SuspendLayout();
            this.grpAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChangeItems
            // 
            this.grpChangeItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeItems.Controls.Add(this.grpAction);
            this.grpChangeItems.Controls.Add(this.btnCancel);
            this.grpChangeItems.Controls.Add(this.btnSave);
            this.grpChangeItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeItems.Location = new System.Drawing.Point(3, 3);
            this.grpChangeItems.Name = "grpChangeItems";
            this.grpChangeItems.Size = new System.Drawing.Size(214, 125);
            this.grpChangeItems.TabIndex = 20;
            this.grpChangeItems.TabStop = false;
            this.grpChangeItems.Text = "Change Champion Settings";
            // 
            // grpAction
            // 
            this.grpAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpAction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAction.Controls.Add(this.rdoDisable);
            this.grpAction.Controls.Add(this.rdoEnable);
            this.grpAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAction.Location = new System.Drawing.Point(8, 19);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(200, 66);
            this.grpAction.TabIndex = 37;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "Settings";
            // 
            // rdoDisable
            // 
            this.rdoDisable.AutoSize = true;
            this.rdoDisable.Location = new System.Drawing.Point(9, 42);
            this.rdoDisable.Name = "rdoDisable";
            this.rdoDisable.Size = new System.Drawing.Size(60, 17);
            this.rdoDisable.TabIndex = 38;
            this.rdoDisable.Text = "Disable";
            // 
            // rdoEnable
            // 
            this.rdoEnable.AutoSize = true;
            this.rdoEnable.Location = new System.Drawing.Point(9, 19);
            this.rdoEnable.Name = "rdoEnable";
            this.rdoEnable.Size = new System.Drawing.Size(58, 17);
            this.rdoEnable.TabIndex = 37;
            this.rdoEnable.Text = "Enable";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(133, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(52, 91);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ChangeChampionSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeItems);
            this.Name = "EventCommand_ChangeChampionSettings";
            this.Size = new System.Drawing.Size(228, 137);
            this.grpChangeItems.ResumeLayout(false);
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeItems;
        private DarkUI.Controls.DarkGroupBox grpAction;
        private DarkUI.Controls.DarkRadioButton rdoDisable;
        private DarkUI.Controls.DarkRadioButton rdoEnable;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
