
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_OpenUpgradeStation
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
            this.grpEditor = new DarkUI.Controls.DarkGroupBox();
            this.lblMulti = new System.Windows.Forms.Label();
            this.nudMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.cmbItems = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMultiplier)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEditor
            // 
            this.grpEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEditor.Controls.Add(this.lblMulti);
            this.grpEditor.Controls.Add(this.nudMultiplier);
            this.grpEditor.Controls.Add(this.lblCurrency);
            this.grpEditor.Controls.Add(this.cmbItems);
            this.grpEditor.Controls.Add(this.btnCancel);
            this.grpEditor.Controls.Add(this.btnSave);
            this.grpEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEditor.Location = new System.Drawing.Point(3, 3);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(300, 106);
            this.grpEditor.TabIndex = 23;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Open Upgrade Station";
            // 
            // lblMulti
            // 
            this.lblMulti.AutoSize = true;
            this.lblMulti.Location = new System.Drawing.Point(110, 48);
            this.lblMulti.Name = "lblMulti";
            this.lblMulti.Size = new System.Drawing.Size(72, 13);
            this.lblMulti.TabIndex = 24;
            this.lblMulti.Text = "Cost Multiplier";
            // 
            // nudMultiplier
            // 
            this.nudMultiplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMultiplier.DecimalPlaces = 2;
            this.nudMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMultiplier.Location = new System.Drawing.Point(188, 46);
            this.nudMultiplier.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMultiplier.Name = "nudMultiplier";
            this.nudMultiplier.Size = new System.Drawing.Size(101, 20);
            this.nudMultiplier.TabIndex = 23;
            this.nudMultiplier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(6, 22);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(49, 13);
            this.lblCurrency.TabIndex = 23;
            this.lblCurrency.Text = "Currency";
            // 
            // cmbItems
            // 
            this.cmbItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbItems.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbItems.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbItems.DrawDropdownHoverOutline = false;
            this.cmbItems.DrawFocusRectangle = false;
            this.cmbItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(62, 19);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(227, 21);
            this.cmbItems.TabIndex = 22;
            this.cmbItems.Text = null;
            this.cmbItems.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(214, 73);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(113, 73);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_OpenUpgradeStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpEditor);
            this.Name = "EventCommand_OpenUpgradeStation";
            this.Size = new System.Drawing.Size(306, 114);
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMultiplier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpEditor;
        private System.Windows.Forms.Label lblMulti;
        private DarkUI.Controls.DarkNumericUpDown nudMultiplier;
        private System.Windows.Forms.Label lblCurrency;
        private DarkUI.Controls.DarkComboBox cmbItems;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}
