
namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandOpenQuestBoard
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
            this.grpOpenQuestBoard = new DarkUI.Controls.DarkGroupBox();
            this.cmbQuestBoards = new DarkUI.Controls.DarkComboBox();
            this.lblQuestBoard = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpOpenQuestBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOpenQuestBoard
            // 
            this.grpOpenQuestBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpOpenQuestBoard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpOpenQuestBoard.Controls.Add(this.cmbQuestBoards);
            this.grpOpenQuestBoard.Controls.Add(this.lblQuestBoard);
            this.grpOpenQuestBoard.Controls.Add(this.btnCancel);
            this.grpOpenQuestBoard.Controls.Add(this.btnSave);
            this.grpOpenQuestBoard.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpOpenQuestBoard.Location = new System.Drawing.Point(12, 11);
            this.grpOpenQuestBoard.Name = "grpOpenQuestBoard";
            this.grpOpenQuestBoard.Size = new System.Drawing.Size(228, 90);
            this.grpOpenQuestBoard.TabIndex = 18;
            this.grpOpenQuestBoard.TabStop = false;
            this.grpOpenQuestBoard.Text = "Open Quest Board";
            // 
            // cmbQuestBoards
            // 
            this.cmbQuestBoards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuestBoards.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuestBoards.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuestBoards.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuestBoards.DrawDropdownHoverOutline = false;
            this.cmbQuestBoards.DrawFocusRectangle = false;
            this.cmbQuestBoards.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestBoards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestBoards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestBoards.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuestBoards.FormattingEnabled = true;
            this.cmbQuestBoards.Location = new System.Drawing.Point(79, 19);
            this.cmbQuestBoards.Name = "cmbQuestBoards";
            this.cmbQuestBoards.Size = new System.Drawing.Size(143, 21);
            this.cmbQuestBoards.TabIndex = 22;
            this.cmbQuestBoards.Text = null;
            this.cmbQuestBoards.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblQuestBoard
            // 
            this.lblQuestBoard.AutoSize = true;
            this.lblQuestBoard.Location = new System.Drawing.Point(4, 22);
            this.lblQuestBoard.Name = "lblQuestBoard";
            this.lblQuestBoard.Size = new System.Drawing.Size(69, 13);
            this.lblQuestBoard.TabIndex = 21;
            this.lblQuestBoard.Text = "Quest Board:";
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
            // EventCommandOpenQuestBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpOpenQuestBoard);
            this.Name = "EventCommandOpenQuestBoard";
            this.Size = new System.Drawing.Size(255, 113);
            this.grpOpenQuestBoard.ResumeLayout(false);
            this.grpOpenQuestBoard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpOpenQuestBoard;
        private DarkUI.Controls.DarkComboBox cmbQuestBoards;
        private System.Windows.Forms.Label lblQuestBoard;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkButton btnSave;
    }
}