
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_RollLoot
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
            this.grpRollTables = new DarkUI.Controls.DarkGroupBox();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.lblTables = new System.Windows.Forms.Label();
            this.btnDropRemove = new DarkUI.Controls.DarkButton();
            this.btnDropAdd = new DarkUI.Controls.DarkButton();
            this.nudRolls = new DarkUI.Controls.DarkNumericUpDown();
            this.lblRolls = new System.Windows.Forms.Label();
            this.cmbTable = new DarkUI.Controls.DarkComboBox();
            this.lstTables = new System.Windows.Forms.ListBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new DarkUI.Controls.DarkTextBox();
            this.grpRollTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRolls)).BeginInit();
            this.SuspendLayout();
            // 
            // grpRollTables
            // 
            this.grpRollTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpRollTables.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRollTables.Controls.Add(this.txtTitle);
            this.grpRollTables.Controls.Add(this.lblTitle);
            this.grpRollTables.Controls.Add(this.btnClear);
            this.grpRollTables.Controls.Add(this.lblTables);
            this.grpRollTables.Controls.Add(this.btnDropRemove);
            this.grpRollTables.Controls.Add(this.btnDropAdd);
            this.grpRollTables.Controls.Add(this.nudRolls);
            this.grpRollTables.Controls.Add(this.lblRolls);
            this.grpRollTables.Controls.Add(this.cmbTable);
            this.grpRollTables.Controls.Add(this.lstTables);
            this.grpRollTables.Controls.Add(this.lblTable);
            this.grpRollTables.Controls.Add(this.btnCancel);
            this.grpRollTables.Controls.Add(this.btnSave);
            this.grpRollTables.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRollTables.Location = new System.Drawing.Point(3, 3);
            this.grpRollTables.Name = "grpRollTables";
            this.grpRollTables.Size = new System.Drawing.Size(285, 398);
            this.grpRollTables.TabIndex = 22;
            this.grpRollTables.TabStop = false;
            this.grpRollTables.Text = "Add Inspiration";
            // 
            // btnClear
            // 
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(172, 60);
            this.btnClear.Name = "btnClear";
            this.btnClear.Padding = new System.Windows.Forms.Padding(5);
            this.btnClear.Size = new System.Drawing.Size(92, 23);
            this.btnClear.TabIndex = 69;
            this.btnClear.Text = "Clear Selection";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(18, 73);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(67, 13);
            this.lblTables.TabIndex = 68;
            this.lblTables.Text = "Tables to roll";
            // 
            // btnDropRemove
            // 
            this.btnDropRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDropRemove.Location = new System.Drawing.Point(180, 316);
            this.btnDropRemove.Name = "btnDropRemove";
            this.btnDropRemove.Padding = new System.Windows.Forms.Padding(5);
            this.btnDropRemove.Size = new System.Drawing.Size(75, 23);
            this.btnDropRemove.TabIndex = 67;
            this.btnDropRemove.Text = "Remove";
            this.btnDropRemove.Click += new System.EventHandler(this.btnDropRemove_Click);
            // 
            // btnDropAdd
            // 
            this.btnDropAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDropAdd.Location = new System.Drawing.Point(99, 316);
            this.btnDropAdd.Name = "btnDropAdd";
            this.btnDropAdd.Padding = new System.Windows.Forms.Padding(5);
            this.btnDropAdd.Size = new System.Drawing.Size(75, 23);
            this.btnDropAdd.TabIndex = 66;
            this.btnDropAdd.Text = "Add";
            this.btnDropAdd.Click += new System.EventHandler(this.btnDropAdd_Click);
            // 
            // nudRolls
            // 
            this.nudRolls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRolls.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRolls.Location = new System.Drawing.Point(21, 290);
            this.nudRolls.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRolls.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRolls.Name = "nudRolls";
            this.nudRolls.Size = new System.Drawing.Size(243, 20);
            this.nudRolls.TabIndex = 65;
            this.nudRolls.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRolls.ValueChanged += new System.EventHandler(this.nudRolls_ValueChanged);
            // 
            // lblRolls
            // 
            this.lblRolls.AutoSize = true;
            this.lblRolls.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblRolls.Location = new System.Drawing.Point(18, 274);
            this.lblRolls.Name = "lblRolls";
            this.lblRolls.Size = new System.Drawing.Size(62, 13);
            this.lblRolls.TabIndex = 23;
            this.lblRolls.Text = "No. of Rolls";
            // 
            // cmbTable
            // 
            this.cmbTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTable.DrawDropdownHoverOutline = false;
            this.cmbTable.DrawFocusRectangle = false;
            this.cmbTable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Location = new System.Drawing.Point(21, 250);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(243, 21);
            this.cmbTable.TabIndex = 64;
            this.cmbTable.Text = null;
            this.cmbTable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTable.SelectedIndexChanged += new System.EventHandler(this.cmbTable_SelectedIndexChanged);
            // 
            // lstTables
            // 
            this.lstTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lstTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTables.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstTables.FormattingEnabled = true;
            this.lstTables.Location = new System.Drawing.Point(21, 89);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(243, 132);
            this.lstTables.TabIndex = 63;
            this.lstTables.SelectedIndexChanged += new System.EventHandler(this.lstTables_SelectedIndexChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(18, 234);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(34, 13);
            this.lblTable.TabIndex = 24;
            this.lblTable.Text = "Table";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(131, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(50, 364);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(18, 29);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(27, 13);
            this.lblTitle.TabIndex = 29;
            this.lblTitle.Text = "Title";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtTitle.Location = new System.Drawing.Point(68, 27);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtTitle.Size = new System.Drawing.Size(196, 20);
            this.txtTitle.TabIndex = 70;
            // 
            // EventCommand_RollLoot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpRollTables);
            this.Name = "EventCommand_RollLoot";
            this.Size = new System.Drawing.Size(293, 404);
            this.grpRollTables.ResumeLayout(false);
            this.grpRollTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRolls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpRollTables;
        private System.Windows.Forms.Label lblTable;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
        private System.Windows.Forms.ListBox lstTables;
        private DarkUI.Controls.DarkComboBox cmbTable;
        private DarkUI.Controls.DarkButton btnDropAdd;
        private DarkUI.Controls.DarkButton btnDropRemove;
        private System.Windows.Forms.Label lblTables;
        private DarkUI.Controls.DarkNumericUpDown nudRolls;
        private System.Windows.Forms.Label lblRolls;
        private DarkUI.Controls.DarkButton btnClear;
        private System.Windows.Forms.Label lblTitle;
        private DarkUI.Controls.DarkTextBox txtTitle;
    }
}
