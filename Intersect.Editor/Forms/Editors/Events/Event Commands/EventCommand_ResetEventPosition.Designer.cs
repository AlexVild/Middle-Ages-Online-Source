namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ResetEventPosition
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
            this.grpReset = new DarkUI.Controls.DarkGroupBox();
            this.cmbEntities = new DarkUI.Controls.DarkComboBox();
            this.lblEntity = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpReset.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReset
            // 
            this.grpReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpReset.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpReset.Controls.Add(this.cmbEntities);
            this.grpReset.Controls.Add(this.lblEntity);
            this.grpReset.Controls.Add(this.btnCancel);
            this.grpReset.Controls.Add(this.btnSave);
            this.grpReset.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpReset.Location = new System.Drawing.Point(4, 5);
            this.grpReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpReset.Name = "grpReset";
            this.grpReset.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpReset.Size = new System.Drawing.Size(278, 154);
            this.grpReset.TabIndex = 18;
            this.grpReset.TabStop = false;
            this.grpReset.Text = "Reset Event Position";
            // 
            // cmbEntities
            // 
            this.cmbEntities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEntities.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEntities.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEntities.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEntities.DrawDropdownHoverOutline = false;
            this.cmbEntities.DrawFocusRectangle = false;
            this.cmbEntities.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEntities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEntities.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEntities.FormattingEnabled = true;
            this.cmbEntities.Location = new System.Drawing.Point(8, 59);
            this.cmbEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEntities.Name = "cmbEntities";
            this.cmbEntities.Size = new System.Drawing.Size(262, 27);
            this.cmbEntities.TabIndex = 22;
            this.cmbEntities.Text = null;
            this.cmbEntities.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.Location = new System.Drawing.Point(6, 34);
            this.lblEntity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(53, 20);
            this.lblEntity.TabIndex = 21;
            this.lblEntity.Text = "Entity:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(134, 109);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(8);
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 109);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8);
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ResetEventPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpReset);
            this.Name = "EventCommand_ResetEventPosition";
            this.Size = new System.Drawing.Size(295, 169);
            this.grpReset.ResumeLayout(false);
            this.grpReset.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpReset;
        private DarkUI.Controls.DarkComboBox cmbEntities;
        private System.Windows.Forms.Label lblEntity;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
