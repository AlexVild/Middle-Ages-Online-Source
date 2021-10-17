
namespace Intersect.Editor.Forms.Editors
{
    partial class FrmQuestList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuestList));
            this.grpQuestList = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.grpQuests = new DarkUI.Controls.DarkGroupBox();
            this.btnRemoveQuest = new DarkUI.Controls.DarkButton();
            this.btnQuestDown = new DarkUI.Controls.DarkButton();
            this.btnQuestUp = new DarkUI.Controls.DarkButton();
            this.btnAddQuest = new DarkUI.Controls.DarkButton();
            this.cmbQuests = new DarkUI.Controls.DarkComboBox();
            this.lblAddQuest = new System.Windows.Forms.Label();
            this.lstQuests = new System.Windows.Forms.ListBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpQuestListReqs = new DarkUI.Controls.DarkGroupBox();
            this.btnEditRequirements = new DarkUI.Controls.DarkButton();
            this.grpQuestList.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.grpQuests.SuspendLayout();
            this.grpQuestListReqs.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpQuestList
            // 
            this.grpQuestList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpQuestList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestList.Controls.Add(this.btnClearSearch);
            this.grpQuestList.Controls.Add(this.txtSearch);
            this.grpQuestList.Controls.Add(this.lstGameObjects);
            this.grpQuestList.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestList.Location = new System.Drawing.Point(12, 12);
            this.grpQuestList.Name = "grpQuestList";
            this.grpQuestList.Size = new System.Drawing.Size(202, 417);
            this.grpQuestList.TabIndex = 18;
            this.grpQuestList.TabStop = false;
            this.grpQuestList.Text = "Quest Lists";
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
            this.grpGeneral.Location = new System.Drawing.Point(262, 26);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(203, 76);
            this.grpGeneral.TabIndex = 35;
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
            // grpQuests
            // 
            this.grpQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpQuests.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuests.Controls.Add(this.btnRemoveQuest);
            this.grpQuests.Controls.Add(this.btnQuestDown);
            this.grpQuests.Controls.Add(this.btnQuestUp);
            this.grpQuests.Controls.Add(this.btnAddQuest);
            this.grpQuests.Controls.Add(this.cmbQuests);
            this.grpQuests.Controls.Add(this.lblAddQuest);
            this.grpQuests.Controls.Add(this.lstQuests);
            this.grpQuests.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuests.Location = new System.Drawing.Point(262, 119);
            this.grpQuests.Name = "grpQuests";
            this.grpQuests.Size = new System.Drawing.Size(203, 310);
            this.grpQuests.TabIndex = 36;
            this.grpQuests.TabStop = false;
            this.grpQuests.Text = "Quests in List";
            // 
            // btnRemoveQuest
            // 
            this.btnRemoveQuest.Location = new System.Drawing.Point(7, 285);
            this.btnRemoveQuest.Name = "btnRemoveQuest";
            this.btnRemoveQuest.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveQuest.Size = new System.Drawing.Size(189, 23);
            this.btnRemoveQuest.TabIndex = 53;
            this.btnRemoveQuest.Text = "Remove Selected";
            // 
            // btnQuestDown
            // 
            this.btnQuestDown.Location = new System.Drawing.Point(177, 176);
            this.btnQuestDown.Name = "btnQuestDown";
            this.btnQuestDown.Padding = new System.Windows.Forms.Padding(5);
            this.btnQuestDown.Size = new System.Drawing.Size(22, 40);
            this.btnQuestDown.TabIndex = 52;
            this.btnQuestDown.Text = "▼";
            // 
            // btnQuestUp
            // 
            this.btnQuestUp.Location = new System.Drawing.Point(177, 19);
            this.btnQuestUp.Name = "btnQuestUp";
            this.btnQuestUp.Padding = new System.Windows.Forms.Padding(5);
            this.btnQuestUp.Size = new System.Drawing.Size(22, 40);
            this.btnQuestUp.TabIndex = 51;
            this.btnQuestUp.Text = "▲";
            // 
            // btnAddQuest
            // 
            this.btnAddQuest.Location = new System.Drawing.Point(7, 259);
            this.btnAddQuest.Name = "btnAddQuest";
            this.btnAddQuest.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddQuest.Size = new System.Drawing.Size(189, 23);
            this.btnAddQuest.TabIndex = 50;
            this.btnAddQuest.Text = "Add Selected";
            // 
            // cmbQuests
            // 
            this.cmbQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuests.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuests.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuests.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuests.DrawDropdownHoverOutline = false;
            this.cmbQuests.DrawFocusRectangle = false;
            this.cmbQuests.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuests.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuests.FormattingEnabled = true;
            this.cmbQuests.Location = new System.Drawing.Point(7, 234);
            this.cmbQuests.Name = "cmbQuests";
            this.cmbQuests.Size = new System.Drawing.Size(189, 21);
            this.cmbQuests.TabIndex = 49;
            this.cmbQuests.Text = null;
            this.cmbQuests.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblAddQuest
            // 
            this.lblAddQuest.AutoSize = true;
            this.lblAddQuest.Location = new System.Drawing.Point(6, 218);
            this.lblAddQuest.Name = "lblAddQuest";
            this.lblAddQuest.Size = new System.Drawing.Size(85, 13);
            this.lblAddQuest.TabIndex = 48;
            this.lblAddQuest.Text = "Add quest to list:";
            // 
            // lstQuests
            // 
            this.lstQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstQuests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstQuests.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstQuests.FormattingEnabled = true;
            this.lstQuests.Location = new System.Drawing.Point(6, 19);
            this.lstQuests.Name = "lstQuests";
            this.lstQuests.Size = new System.Drawing.Size(168, 197);
            this.lstQuests.TabIndex = 47;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(135, 513);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(169, 27);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "Save";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(172, 27);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "Cancel";
            // 
            // grpQuestListReqs
            // 
            this.grpQuestListReqs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpQuestListReqs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestListReqs.Controls.Add(this.btnEditRequirements);
            this.grpQuestListReqs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestListReqs.Location = new System.Drawing.Point(211, 444);
            this.grpQuestListReqs.Name = "grpQuestListReqs";
            this.grpQuestListReqs.Size = new System.Drawing.Size(271, 52);
            this.grpQuestListReqs.TabIndex = 39;
            this.grpQuestListReqs.TabStop = false;
            this.grpQuestListReqs.Text = "Quest List Requirements";
            // 
            // btnEditRequirements
            // 
            this.btnEditRequirements.Location = new System.Drawing.Point(10, 20);
            this.btnEditRequirements.Name = "btnEditRequirements";
            this.btnEditRequirements.Padding = new System.Windows.Forms.Padding(5);
            this.btnEditRequirements.Size = new System.Drawing.Size(255, 23);
            this.btnEditRequirements.TabIndex = 0;
            this.btnEditRequirements.Text = "Edit Quest List Requirements";
            // 
            // FrmQuestList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(495, 552);
            this.Controls.Add(this.grpQuestListReqs);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpQuests);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.grpQuestList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuestList";
            this.Text = "Quest Lists";
            this.grpQuestList.ResumeLayout(false);
            this.grpQuestList.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpQuests.ResumeLayout(false);
            this.grpQuests.PerformLayout();
            this.grpQuestListReqs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkUI.Controls.DarkGroupBox grpQuestList;
        private DarkUI.Controls.DarkButton btnClearSearch;
        private DarkUI.Controls.DarkTextBox txtSearch;
        private Controls.GameObjectList lstGameObjects;
        private DarkUI.Controls.DarkGroupBox grpGeneral;
        private DarkUI.Controls.DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkUI.Controls.DarkComboBox cmbFolder;
        private System.Windows.Forms.Label lblName;
        private DarkUI.Controls.DarkTextBox txtName;
        private DarkUI.Controls.DarkGroupBox grpQuests;
        private DarkUI.Controls.DarkButton btnRemoveQuest;
        private DarkUI.Controls.DarkButton btnQuestDown;
        private DarkUI.Controls.DarkButton btnQuestUp;
        private DarkUI.Controls.DarkButton btnAddQuest;
        private DarkUI.Controls.DarkComboBox cmbQuests;
        private System.Windows.Forms.Label lblAddQuest;
        private System.Windows.Forms.ListBox lstQuests;
        private DarkUI.Controls.DarkButton btnSave;
        private DarkUI.Controls.DarkButton btnCancel;
        private DarkUI.Controls.DarkGroupBox grpQuestListReqs;
        private DarkUI.Controls.DarkButton btnEditRequirements;
    }
}