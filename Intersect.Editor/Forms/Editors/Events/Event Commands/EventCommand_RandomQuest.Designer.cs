
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandRandomQuest
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
            this.grpRandomQuest = new DarkUI.Controls.DarkGroupBox();
            this.cmbQuestLists = new DarkUI.Controls.DarkComboBox();
            this.lblQuestList = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpRandomQuest.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRandomQuest
            // 
            this.grpRandomQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpRandomQuest.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRandomQuest.Controls.Add(this.cmbQuestLists);
            this.grpRandomQuest.Controls.Add(this.lblQuestList);
            this.grpRandomQuest.Controls.Add(this.btnCancel);
            this.grpRandomQuest.Controls.Add(this.btnSave);
            this.grpRandomQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRandomQuest.Location = new System.Drawing.Point(12, 12);
            this.grpRandomQuest.Name = "grpRandomQuest";
            this.grpRandomQuest.Size = new System.Drawing.Size(228, 90);
            this.grpRandomQuest.TabIndex = 19;
            this.grpRandomQuest.TabStop = false;
            this.grpRandomQuest.Text = "Random Quest";
            // 
            // cmbQuestLists
            // 
            this.cmbQuestLists.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuestLists.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuestLists.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuestLists.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuestLists.DrawDropdownHoverOutline = false;
            this.cmbQuestLists.DrawFocusRectangle = false;
            this.cmbQuestLists.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestLists.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestLists.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuestLists.FormattingEnabled = true;
            this.cmbQuestLists.Location = new System.Drawing.Point(79, 19);
            this.cmbQuestLists.Name = "cmbQuestLists";
            this.cmbQuestLists.Size = new System.Drawing.Size(143, 21);
            this.cmbQuestLists.TabIndex = 22;
            this.cmbQuestLists.Text = null;
            this.cmbQuestLists.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblQuestList
            // 
            this.lblQuestList.AutoSize = true;
            this.lblQuestList.Location = new System.Drawing.Point(4, 22);
            this.lblQuestList.Name = "lblQuestList";
            this.lblQuestList.Size = new System.Drawing.Size(57, 13);
            this.lblQuestList.TabIndex = 21;
            this.lblQuestList.Text = "Quest List:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(147, 55);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(30, 55);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EventCommandRandomQuest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpRandomQuest);
            this.Name = "EventCommandRandomQuest";
            this.Size = new System.Drawing.Size(248, 112);
            this.grpRandomQuest.ResumeLayout(false);
            this.grpRandomQuest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpRandomQuest;
        private DarkUI.Controls.DarkComboBox cmbQuestLists;
        private System.Windows.Forms.Label lblQuestList;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}