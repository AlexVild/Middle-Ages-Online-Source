
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommand_NPCGuildManagement
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
            this.grpManagement = new DarkUI.Controls.DarkGroupBox();
            this.nudClassRank = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbValue = new DarkUI.Controls.DarkComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cmbClass = new DarkUI.Controls.DarkComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblRank = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbSelection = new DarkUI.Controls.DarkComboBox();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassRank)).BeginInit();
            this.SuspendLayout();
            // 
            // grpManagement
            // 
            this.grpManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpManagement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpManagement.Controls.Add(this.nudClassRank);
            this.grpManagement.Controls.Add(this.cmbValue);
            this.grpManagement.Controls.Add(this.lblValue);
            this.grpManagement.Controls.Add(this.cmbClass);
            this.grpManagement.Controls.Add(this.lblClass);
            this.grpManagement.Controls.Add(this.lblRank);
            this.grpManagement.Controls.Add(this.lblType);
            this.grpManagement.Controls.Add(this.cmbSelection);
            this.grpManagement.Controls.Add(this.btnCancel);
            this.grpManagement.Controls.Add(this.btnSave);
            this.grpManagement.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpManagement.Location = new System.Drawing.Point(3, 3);
            this.grpManagement.Name = "grpManagement";
            this.grpManagement.Size = new System.Drawing.Size(232, 227);
            this.grpManagement.TabIndex = 21;
            this.grpManagement.TabStop = false;
            this.grpManagement.Text = "NPC Guild Management";
            // 
            // nudClassRank
            // 
            this.nudClassRank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudClassRank.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudClassRank.Location = new System.Drawing.Point(112, 115);
            this.nudClassRank.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudClassRank.Name = "nudClassRank";
            this.nudClassRank.Size = new System.Drawing.Size(114, 20);
            this.nudClassRank.TabIndex = 23;
            this.nudClassRank.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudClassRank.ValueChanged += new System.EventHandler(this.nudClassRank_ValueChanged);
            // 
            // cmbValue
            // 
            this.cmbValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbValue.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbValue.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbValue.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbValue.DrawDropdownHoverOutline = false;
            this.cmbValue.DrawFocusRectangle = false;
            this.cmbValue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cmbValue.Location = new System.Drawing.Point(112, 114);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(114, 21);
            this.cmbValue.TabIndex = 29;
            this.cmbValue.Text = "False";
            this.cmbValue.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbValue.SelectedIndexChanged += new System.EventHandler(this.cmbValue_SelectedIndexChanged);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(6, 117);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 28;
            this.lblValue.Text = "Value:";
            // 
            // cmbClass
            // 
            this.cmbClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbClass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbClass.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbClass.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbClass.DrawDropdownHoverOutline = false;
            this.cmbClass.DrawFocusRectangle = false;
            this.cmbClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClass.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(68, 73);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(158, 21);
            this.cmbClass.TabIndex = 27;
            this.cmbClass.Text = null;
            this.cmbClass.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(6, 76);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(35, 13);
            this.lblClass.TabIndex = 26;
            this.lblClass.Text = "Class:";
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Location = new System.Drawing.Point(4, 117);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(37, 13);
            this.lblRank.TabIndex = 24;
            this.lblRank.Text = "Value:";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(4, 35);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 23;
            this.lblType.Text = "Type:";
            // 
            // cmbSelection
            // 
            this.cmbSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSelection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSelection.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSelection.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSelection.DrawDropdownHoverOutline = false;
            this.cmbSelection.DrawFocusRectangle = false;
            this.cmbSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSelection.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSelection.FormattingEnabled = true;
            this.cmbSelection.Location = new System.Drawing.Point(68, 32);
            this.cmbSelection.Name = "cmbSelection";
            this.cmbSelection.Size = new System.Drawing.Size(158, 21);
            this.cmbSelection.TabIndex = 22;
            this.cmbSelection.Text = null;
            this.cmbSelection.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSelection.SelectedIndexChanged += new System.EventHandler(this.cmbSelection_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(151, 158);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommand_NPCGuildManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpManagement);
            this.Name = "EventCommand_NPCGuildManagement";
            this.Size = new System.Drawing.Size(238, 240);
            this.grpManagement.ResumeLayout(false);
            this.grpManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassRank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpManagement;
        private DarkUI.Controls.DarkComboBox cmbValue;
        private System.Windows.Forms.Label lblValue;
        private DarkUI.Controls.DarkComboBox cmbClass;
        private System.Windows.Forms.Label lblClass;
        private DarkUI.Controls.DarkNumericUpDown nudClassRank;
        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.Label lblType;
        private DarkUI.Controls.DarkComboBox cmbSelection;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}