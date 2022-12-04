
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_ChangeRecipeUnlock
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
            this.cmbRecipes = new DarkUI.Controls.DarkComboBox();
            this.grpUnlockStatus = new DarkUI.Controls.DarkGroupBox();
            this.rdoLock = new DarkUI.Controls.DarkRadioButton();
            this.rdoUnlock = new DarkUI.Controls.DarkRadioButton();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpShake.SuspendLayout();
            this.grpUnlockStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpShake
            // 
            this.grpShake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpShake.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpShake.Controls.Add(this.cmbRecipes);
            this.grpShake.Controls.Add(this.grpUnlockStatus);
            this.grpShake.Controls.Add(this.lblRecipe);
            this.grpShake.Controls.Add(this.btnCancel);
            this.grpShake.Controls.Add(this.btnSave);
            this.grpShake.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpShake.Location = new System.Drawing.Point(3, 3);
            this.grpShake.Name = "grpShake";
            this.grpShake.Size = new System.Drawing.Size(304, 137);
            this.grpShake.TabIndex = 19;
            this.grpShake.TabStop = false;
            this.grpShake.Text = "Shake Screen";
            // 
            // cmbRecipes
            // 
            this.cmbRecipes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbRecipes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbRecipes.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbRecipes.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbRecipes.DrawDropdownHoverOutline = false;
            this.cmbRecipes.DrawFocusRectangle = false;
            this.cmbRecipes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRecipes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRecipes.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbRecipes.FormattingEnabled = true;
            this.cmbRecipes.Location = new System.Drawing.Point(62, 19);
            this.cmbRecipes.Name = "cmbRecipes";
            this.cmbRecipes.Size = new System.Drawing.Size(226, 21);
            this.cmbRecipes.TabIndex = 27;
            this.cmbRecipes.Text = null;
            this.cmbRecipes.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpUnlockStatus
            // 
            this.grpUnlockStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpUnlockStatus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpUnlockStatus.Controls.Add(this.rdoLock);
            this.grpUnlockStatus.Controls.Add(this.rdoUnlock);
            this.grpUnlockStatus.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpUnlockStatus.Location = new System.Drawing.Point(8, 46);
            this.grpUnlockStatus.Name = "grpUnlockStatus";
            this.grpUnlockStatus.Size = new System.Drawing.Size(280, 56);
            this.grpUnlockStatus.TabIndex = 23;
            this.grpUnlockStatus.TabStop = false;
            this.grpUnlockStatus.Text = "Unlock";
            // 
            // rdoLock
            // 
            this.rdoLock.AutoSize = true;
            this.rdoLock.Location = new System.Drawing.Point(184, 28);
            this.rdoLock.Name = "rdoLock";
            this.rdoLock.Size = new System.Drawing.Size(49, 17);
            this.rdoLock.TabIndex = 30;
            this.rdoLock.Text = "Lock";
            // 
            // rdoUnlock
            // 
            this.rdoUnlock.AutoSize = true;
            this.rdoUnlock.Checked = true;
            this.rdoUnlock.Location = new System.Drawing.Point(10, 28);
            this.rdoUnlock.Name = "rdoUnlock";
            this.rdoUnlock.Size = new System.Drawing.Size(59, 17);
            this.rdoUnlock.TabIndex = 29;
            this.rdoUnlock.TabStop = true;
            this.rdoUnlock.Text = "Unlock";
            // 
            // lblRecipe
            // 
            this.lblRecipe.AutoSize = true;
            this.lblRecipe.Location = new System.Drawing.Point(15, 22);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(41, 13);
            this.lblRecipe.TabIndex = 21;
            this.lblRecipe.Text = "Recipe";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(213, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(132, 108);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_ChangeRecipeUnlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpShake);
            this.Name = "EventCommand_ChangeRecipeUnlock";
            this.Size = new System.Drawing.Size(310, 148);
            this.grpShake.ResumeLayout(false);
            this.grpShake.PerformLayout();
            this.grpUnlockStatus.ResumeLayout(false);
            this.grpUnlockStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpShake;
        private DarkUI.Controls.DarkGroupBox grpUnlockStatus;
        private System.Windows.Forms.Label lblRecipe;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkComboBox cmbRecipes;
        private DarkUI.Controls.DarkRadioButton rdoLock;
        private DarkUI.Controls.DarkRadioButton rdoUnlock;
    }
}
