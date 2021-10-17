﻿
namespace Intersect.Editor.Forms.Editors
{
    partial class FrmQuestBoard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuestBoard));
            this.grpQuestBoards = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.grpQuestLists = new DarkUI.Controls.DarkGroupBox();
            this.btnRemoveQuestList = new DarkUI.Controls.DarkButton();
            this.btnQuestListDown = new DarkUI.Controls.DarkButton();
            this.btnQuestListUp = new DarkUI.Controls.DarkButton();
            this.btnAddQuestList = new DarkUI.Controls.DarkButton();
            this.cmbQuestLists = new DarkUI.Controls.DarkComboBox();
            this.lblAddQuestList = new System.Windows.Forms.Label();
            this.lstQuestLists = new System.Windows.Forms.ListBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpQuestBoards.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.grpQuestLists.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpQuestBoards
            // 
            this.grpQuestBoards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpQuestBoards.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestBoards.Controls.Add(this.btnClearSearch);
            this.grpQuestBoards.Controls.Add(this.txtSearch);
            this.grpQuestBoards.Controls.Add(this.lstGameObjects);
            this.grpQuestBoards.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestBoards.Location = new System.Drawing.Point(12, 12);
            this.grpQuestBoards.Name = "grpQuestBoards";
            this.grpQuestBoards.Size = new System.Drawing.Size(202, 417);
            this.grpQuestBoards.TabIndex = 19;
            this.grpQuestBoards.TabStop = false;
            this.grpQuestBoards.Text = "Quest Boards";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 19);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(17, 17);
            this.btnClearSearch.TabIndex = 19;
            this.btnClearSearch.Text = "X";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(6, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(166, 20);
            this.txtSearch.TabIndex = 18;
            this.txtSearch.Text = "Search...";
            // 
            // lstGameObjects
            // 
            this.lstGameObjects.AllowDrop = true;
            this.lstGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstGameObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstGameObjects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstGameObjects.HideSelection = false;
            this.lstGameObjects.ImageIndex = 0;
            this.lstGameObjects.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lstGameObjects.Location = new System.Drawing.Point(6, 46);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(190, 365);
            this.lstGameObjects.TabIndex = 2;
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(244, 31);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(203, 76);
            this.grpGeneral.TabIndex = 36;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(179, 44);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 23;
            this.btnAddFolder.Text = "+";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(4, 48);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 22;
            this.lblFolder.Text = "Folder:";
            // 
            // cmbFolder
            // 
            this.cmbFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFolder.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFolder.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFolder.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFolder.DrawDropdownHoverOutline = false;
            this.cmbFolder.DrawFocusRectangle = false;
            this.cmbFolder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFolder.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Location = new System.Drawing.Point(57, 44);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(117, 21);
            this.cmbFolder.TabIndex = 21;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(4, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(57, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 20);
            this.txtName.TabIndex = 18;
            // 
            // grpQuestLists
            // 
            this.grpQuestLists.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpQuestLists.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestLists.Controls.Add(this.btnRemoveQuestList);
            this.grpQuestLists.Controls.Add(this.btnQuestListDown);
            this.grpQuestLists.Controls.Add(this.btnQuestListUp);
            this.grpQuestLists.Controls.Add(this.btnAddQuestList);
            this.grpQuestLists.Controls.Add(this.cmbQuestLists);
            this.grpQuestLists.Controls.Add(this.lblAddQuestList);
            this.grpQuestLists.Controls.Add(this.lstQuestLists);
            this.grpQuestLists.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestLists.Location = new System.Drawing.Point(244, 119);
            this.grpQuestLists.Name = "grpQuestLists";
            this.grpQuestLists.Size = new System.Drawing.Size(203, 310);
            this.grpQuestLists.TabIndex = 37;
            this.grpQuestLists.TabStop = false;
            this.grpQuestLists.Text = "Quest Lists to Offer";
            // 
            // btnRemoveQuestList
            // 
            this.btnRemoveQuestList.Location = new System.Drawing.Point(7, 285);
            this.btnRemoveQuestList.Name = "btnRemoveQuestList";
            this.btnRemoveQuestList.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveQuestList.Size = new System.Drawing.Size(189, 23);
            this.btnRemoveQuestList.TabIndex = 53;
            this.btnRemoveQuestList.Text = "Remove Selected";
            // 
            // btnQuestListDown
            // 
            this.btnQuestListDown.Location = new System.Drawing.Point(177, 176);
            this.btnQuestListDown.Name = "btnQuestListDown";
            this.btnQuestListDown.Padding = new System.Windows.Forms.Padding(5);
            this.btnQuestListDown.Size = new System.Drawing.Size(22, 40);
            this.btnQuestListDown.TabIndex = 52;
            this.btnQuestListDown.Text = "▼";
            // 
            // btnQuestListUp
            // 
            this.btnQuestListUp.Location = new System.Drawing.Point(177, 19);
            this.btnQuestListUp.Name = "btnQuestListUp";
            this.btnQuestListUp.Padding = new System.Windows.Forms.Padding(5);
            this.btnQuestListUp.Size = new System.Drawing.Size(22, 40);
            this.btnQuestListUp.TabIndex = 51;
            this.btnQuestListUp.Text = "▲";
            // 
            // btnAddQuestList
            // 
            this.btnAddQuestList.Location = new System.Drawing.Point(7, 259);
            this.btnAddQuestList.Name = "btnAddQuestList";
            this.btnAddQuestList.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddQuestList.Size = new System.Drawing.Size(189, 23);
            this.btnAddQuestList.TabIndex = 50;
            this.btnAddQuestList.Text = "Add Selected";
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
            this.cmbQuestLists.Location = new System.Drawing.Point(7, 234);
            this.cmbQuestLists.Name = "cmbQuestLists";
            this.cmbQuestLists.Size = new System.Drawing.Size(189, 21);
            this.cmbQuestLists.TabIndex = 49;
            this.cmbQuestLists.Text = null;
            this.cmbQuestLists.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblAddQuestList
            // 
            this.lblAddQuestList.AutoSize = true;
            this.lblAddQuestList.Location = new System.Drawing.Point(6, 218);
            this.lblAddQuestList.Name = "lblAddQuestList";
            this.lblAddQuestList.Size = new System.Drawing.Size(73, 13);
            this.lblAddQuestList.TabIndex = 48;
            this.lblAddQuestList.Text = "Add quest list:";
            // 
            // lstQuestLists
            // 
            this.lstQuestLists.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstQuestLists.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstQuestLists.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstQuestLists.FormattingEnabled = true;
            this.lstQuestLists.Location = new System.Drawing.Point(6, 19);
            this.lstQuestLists.Name = "lstQuestLists";
            this.lstQuestLists.Size = new System.Drawing.Size(168, 197);
            this.lstQuestLists.TabIndex = 47;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(100, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(169, 27);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 468);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(172, 27);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "Cancel";
            // 
            // FrmQuestBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(459, 507);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpQuestLists);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.grpQuestBoards);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuestBoard";
            this.Text = "Quest Boards";
            this.grpQuestBoards.ResumeLayout(false);
            this.grpQuestBoards.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpQuestLists.ResumeLayout(false);
            this.grpQuestLists.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpQuestBoards;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkGroupBox grpGeneral;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private DarkUI.Controls.DarkGroupBox grpQuestLists;
        private DarkUI.Controls.DarkButton btnRemoveQuestList;
        private DarkUI.Controls.DarkButton btnQuestListDown;
        private DarkUI.Controls.DarkButton btnQuestListUp;
        private DarkUI.Controls.DarkButton btnAddQuestList;
        private DarkUI.Controls.DarkComboBox cmbQuestLists;
        private System.Windows.Forms.Label lblAddQuestList;
        private System.Windows.Forms.ListBox lstQuestLists;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
    }
}