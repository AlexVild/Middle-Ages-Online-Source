
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeDungeon
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
            this.grpChangeDungeon = new DarkUI.Controls.DarkGroupBox();
            this.cmbState = new DarkUI.Controls.DarkComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpChangeDungeon.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChangeDungeon
            // 
            this.grpChangeDungeon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChangeDungeon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChangeDungeon.Controls.Add(this.cmbState);
            this.grpChangeDungeon.Controls.Add(this.lblState);
            this.grpChangeDungeon.Controls.Add(this.btnCancel);
            this.grpChangeDungeon.Controls.Add(this.btnSave);
            this.grpChangeDungeon.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChangeDungeon.Location = new System.Drawing.Point(3, 3);
            this.grpChangeDungeon.Name = "grpChangeDungeon";
            this.grpChangeDungeon.Size = new System.Drawing.Size(214, 95);
            this.grpChangeDungeon.TabIndex = 20;
            this.grpChangeDungeon.TabStop = false;
            this.grpChangeDungeon.Text = "Change Dungeon";
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbState.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbState.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbState.DrawDropdownHoverOutline = false;
            this.cmbState.DrawFocusRectangle = false;
            this.cmbState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbState.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(8, 37);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(194, 21);
            this.cmbState.TabIndex = 22;
            this.cmbState.Text = null;
            this.cmbState.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(5, 21);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(79, 13);
            this.lblState.TabIndex = 21;
            this.lblState.Text = "Dungeon State";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(127, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 64);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ChangeDungeon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpChangeDungeon);
            this.Name = "EventCommand_ChangeDungeon";
            this.Size = new System.Drawing.Size(220, 103);
            this.grpChangeDungeon.ResumeLayout(false);
            this.grpChangeDungeon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpChangeDungeon;
        private DarkUI.Controls.DarkComboBox cmbState;
        private System.Windows.Forms.Label lblState;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
