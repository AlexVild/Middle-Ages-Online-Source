using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandText
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
            this.grpShowText = new DarkUI.Controls.DarkGroupBox();
            this.lblCommands = new System.Windows.Forms.Label();
            this.pnlFace = new System.Windows.Forms.Panel();
            this.cmbFace = new DarkUI.Controls.DarkComboBox();
            this.lblFace = new System.Windows.Forms.Label();
            this.txtShowText = new DarkUI.Controls.DarkTextBox();
            this.lblText = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpTemplates = new DarkUI.Controls.DarkGroupBox();
            this.rdoQuest = new DarkUI.Controls.DarkRadioButton();
            this.rdoItem = new DarkUI.Controls.DarkRadioButton();
            this.darkCheckBox2 = new DarkUI.Controls.DarkCheckBox();
            this.cmbItems = new DarkUI.Controls.DarkComboBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblQuest = new System.Windows.Forms.Label();
            this.cmbQuests = new DarkUI.Controls.DarkComboBox();
            this.chkTemplate = new DarkUI.Controls.DarkCheckBox();
            this.rdoInventorySpace = new DarkUI.Controls.DarkRadioButton();
            this.grpChatboxSettings = new DarkUI.Controls.DarkGroupBox();
            this.cmbChannel = new DarkUI.Controls.DarkComboBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.cmbColor = new DarkUI.Controls.DarkComboBox();
            this.darkCheckBox1 = new DarkUI.Controls.DarkCheckBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.chkSendToChatbox = new DarkUI.Controls.DarkCheckBox();
            this.grpText = new DarkUI.Controls.DarkGroupBox();
            this.darkCheckBox3 = new DarkUI.Controls.DarkCheckBox();
            this.grpShowText.SuspendLayout();
            this.grpTemplates.SuspendLayout();
            this.grpChatboxSettings.SuspendLayout();
            this.grpText.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpShowText
            // 
            this.grpShowText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpShowText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpShowText.Controls.Add(this.grpText);
            this.grpShowText.Controls.Add(this.chkSendToChatbox);
            this.grpShowText.Controls.Add(this.grpChatboxSettings);
            this.grpShowText.Controls.Add(this.chkTemplate);
            this.grpShowText.Controls.Add(this.grpTemplates);
            this.grpShowText.Controls.Add(this.btnCancel);
            this.grpShowText.Controls.Add(this.btnSave);
            this.grpShowText.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpShowText.Location = new System.Drawing.Point(3, 0);
            this.grpShowText.Name = "grpShowText";
            this.grpShowText.Size = new System.Drawing.Size(312, 433);
            this.grpShowText.TabIndex = 17;
            this.grpShowText.TabStop = false;
            this.grpShowText.Text = "Show Text";
            // 
            // lblCommands
            // 
            this.lblCommands.AutoSize = true;
            this.lblCommands.BackColor = System.Drawing.Color.Transparent;
            this.lblCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommands.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblCommands.Location = new System.Drawing.Point(166, 24);
            this.lblCommands.Name = "lblCommands";
            this.lblCommands.Size = new System.Drawing.Size(84, 13);
            this.lblCommands.TabIndex = 26;
            this.lblCommands.Text = "Chat Commands";
            this.lblCommands.Click += new System.EventHandler(this.lblCommands_Click);
            // 
            // pnlFace
            // 
            this.pnlFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFace.Location = new System.Drawing.Point(154, 146);
            this.pnlFace.Name = "pnlFace";
            this.pnlFace.Size = new System.Drawing.Size(96, 96);
            this.pnlFace.TabIndex = 25;
            // 
            // cmbFace
            // 
            this.cmbFace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFace.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFace.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFace.DrawDropdownHoverOutline = false;
            this.cmbFace.DrawFocusRectangle = false;
            this.cmbFace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFace.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFace.FormattingEnabled = true;
            this.cmbFace.Location = new System.Drawing.Point(18, 159);
            this.cmbFace.Name = "cmbFace";
            this.cmbFace.Size = new System.Drawing.Size(121, 21);
            this.cmbFace.TabIndex = 24;
            this.cmbFace.Text = null;
            this.cmbFace.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFace.SelectedIndexChanged += new System.EventHandler(this.cmbFace_SelectedIndexChanged);
            // 
            // lblFace
            // 
            this.lblFace.AutoSize = true;
            this.lblFace.Location = new System.Drawing.Point(15, 143);
            this.lblFace.Name = "lblFace";
            this.lblFace.Size = new System.Drawing.Size(34, 13);
            this.lblFace.TabIndex = 23;
            this.lblFace.Text = "Face:";
            // 
            // txtShowText
            // 
            this.txtShowText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtShowText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShowText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtShowText.Location = new System.Drawing.Point(16, 40);
            this.txtShowText.Multiline = true;
            this.txtShowText.Name = "txtShowText";
            this.txtShowText.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtShowText.Size = new System.Drawing.Size(234, 100);
            this.txtShowText.TabIndex = 22;
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(13, 24);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(31, 13);
            this.lblText.TabIndex = 21;
            this.lblText.Text = "Text:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(93, 404);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 404);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpTemplates
            // 
            this.grpTemplates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpTemplates.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTemplates.Controls.Add(this.rdoInventorySpace);
            this.grpTemplates.Controls.Add(this.cmbQuests);
            this.grpTemplates.Controls.Add(this.lblQuest);
            this.grpTemplates.Controls.Add(this.lblItem);
            this.grpTemplates.Controls.Add(this.cmbItems);
            this.grpTemplates.Controls.Add(this.rdoQuest);
            this.grpTemplates.Controls.Add(this.rdoItem);
            this.grpTemplates.Controls.Add(this.darkCheckBox2);
            this.grpTemplates.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTemplates.Location = new System.Drawing.Point(12, 42);
            this.grpTemplates.Name = "grpTemplates";
            this.grpTemplates.Size = new System.Drawing.Size(294, 149);
            this.grpTemplates.TabIndex = 114;
            this.grpTemplates.TabStop = false;
            this.grpTemplates.Text = "Templates";
            // 
            // rdoQuest
            // 
            this.rdoQuest.AutoSize = true;
            this.rdoQuest.Location = new System.Drawing.Point(6, 92);
            this.rdoQuest.Name = "rdoQuest";
            this.rdoQuest.Size = new System.Drawing.Size(108, 17);
            this.rdoQuest.TabIndex = 112;
            this.rdoQuest.Text = "Quest Completion";
            this.rdoQuest.CheckedChanged += new System.EventHandler(this.rdoQuest_CheckedChanged);
            // 
            // rdoItem
            // 
            this.rdoItem.AutoSize = true;
            this.rdoItem.Location = new System.Drawing.Point(6, 19);
            this.rdoItem.Name = "rdoItem";
            this.rdoItem.Size = new System.Drawing.Size(91, 17);
            this.rdoItem.TabIndex = 111;
            this.rdoItem.Text = "Item Obtained";
            this.rdoItem.CheckedChanged += new System.EventHandler(this.rdoItem_CheckedChanged);
            // 
            // darkCheckBox2
            // 
            this.darkCheckBox2.AutoSize = true;
            this.darkCheckBox2.Location = new System.Drawing.Point(76, 312);
            this.darkCheckBox2.Name = "darkCheckBox2";
            this.darkCheckBox2.Size = new System.Drawing.Size(104, 17);
            this.darkCheckBox2.TabIndex = 110;
            this.darkCheckBox2.Text = "Run indefinitely?";
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
            this.cmbItems.Location = new System.Drawing.Point(72, 42);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(210, 21);
            this.cmbItems.TabIndex = 113;
            this.cmbItems.Text = null;
            this.cmbItems.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(25, 45);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(27, 13);
            this.lblItem.TabIndex = 115;
            this.lblItem.Text = "Item";
            // 
            // lblQuest
            // 
            this.lblQuest.AutoSize = true;
            this.lblQuest.Location = new System.Drawing.Point(25, 121);
            this.lblQuest.Name = "lblQuest";
            this.lblQuest.Size = new System.Drawing.Size(35, 13);
            this.lblQuest.TabIndex = 116;
            this.lblQuest.Text = "Quest";
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
            this.cmbQuests.Location = new System.Drawing.Point(72, 118);
            this.cmbQuests.Name = "cmbQuests";
            this.cmbQuests.Size = new System.Drawing.Size(210, 21);
            this.cmbQuests.TabIndex = 117;
            this.cmbQuests.Text = null;
            this.cmbQuests.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // chkTemplate
            // 
            this.chkTemplate.Location = new System.Drawing.Point(15, 19);
            this.chkTemplate.Name = "chkTemplate";
            this.chkTemplate.Size = new System.Drawing.Size(98, 17);
            this.chkTemplate.TabIndex = 61;
            this.chkTemplate.Text = "Use Template?";
            this.chkTemplate.CheckedChanged += new System.EventHandler(this.chkTemplate_CheckedChanged);
            // 
            // rdoInventorySpace
            // 
            this.rdoInventorySpace.AutoSize = true;
            this.rdoInventorySpace.Location = new System.Drawing.Point(6, 69);
            this.rdoInventorySpace.Name = "rdoInventorySpace";
            this.rdoInventorySpace.Size = new System.Drawing.Size(73, 17);
            this.rdoInventorySpace.TabIndex = 118;
            this.rdoInventorySpace.Text = "No Space";
            // 
            // grpChatboxSettings
            // 
            this.grpChatboxSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpChatboxSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChatboxSettings.Controls.Add(this.cmbChannel);
            this.grpChatboxSettings.Controls.Add(this.lblChannel);
            this.grpChatboxSettings.Controls.Add(this.lblColor);
            this.grpChatboxSettings.Controls.Add(this.cmbColor);
            this.grpChatboxSettings.Controls.Add(this.darkCheckBox1);
            this.grpChatboxSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChatboxSettings.Location = new System.Drawing.Point(6, 325);
            this.grpChatboxSettings.Name = "grpChatboxSettings";
            this.grpChatboxSettings.Size = new System.Drawing.Size(294, 73);
            this.grpChatboxSettings.TabIndex = 119;
            this.grpChatboxSettings.TabStop = false;
            this.grpChatboxSettings.Text = "Chatbox Settings";
            // 
            // cmbChannel
            // 
            this.cmbChannel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbChannel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbChannel.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbChannel.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbChannel.DrawDropdownHoverOutline = false;
            this.cmbChannel.DrawFocusRectangle = false;
            this.cmbChannel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbChannel.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbChannel.FormattingEnabled = true;
            this.cmbChannel.Location = new System.Drawing.Point(72, 46);
            this.cmbChannel.Name = "cmbChannel";
            this.cmbChannel.Size = new System.Drawing.Size(210, 21);
            this.cmbChannel.TabIndex = 117;
            this.cmbChannel.Text = null;
            this.cmbChannel.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(20, 49);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(46, 13);
            this.lblChannel.TabIndex = 116;
            this.lblChannel.Text = "Channel";
            // 
            // cmbColor
            // 
            this.cmbColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbColor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbColor.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbColor.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbColor.DrawDropdownHoverOutline = false;
            this.cmbColor.DrawFocusRectangle = false;
            this.cmbColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbColor.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Location = new System.Drawing.Point(72, 19);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(210, 21);
            this.cmbColor.TabIndex = 113;
            this.cmbColor.Text = null;
            this.cmbColor.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // darkCheckBox1
            // 
            this.darkCheckBox1.AutoSize = true;
            this.darkCheckBox1.Location = new System.Drawing.Point(76, 312);
            this.darkCheckBox1.Name = "darkCheckBox1";
            this.darkCheckBox1.Size = new System.Drawing.Size(104, 17);
            this.darkCheckBox1.TabIndex = 110;
            this.darkCheckBox1.Text = "Run indefinitely?";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(28, 22);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(31, 13);
            this.lblColor.TabIndex = 115;
            this.lblColor.Text = "Color";
            // 
            // chkSendToChatbox
            // 
            this.chkSendToChatbox.Location = new System.Drawing.Point(12, 302);
            this.chkSendToChatbox.Name = "chkSendToChatbox";
            this.chkSendToChatbox.Size = new System.Drawing.Size(115, 17);
            this.chkSendToChatbox.TabIndex = 120;
            this.chkSendToChatbox.Text = "Send to Chatbox?";
            this.chkSendToChatbox.CheckedChanged += new System.EventHandler(this.chkSendToChatbox_CheckedChanged);
            // 
            // grpText
            // 
            this.grpText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grpText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpText.Controls.Add(this.darkCheckBox3);
            this.grpText.Controls.Add(this.txtShowText);
            this.grpText.Controls.Add(this.lblText);
            this.grpText.Controls.Add(this.lblFace);
            this.grpText.Controls.Add(this.cmbFace);
            this.grpText.Controls.Add(this.lblCommands);
            this.grpText.Controls.Add(this.pnlFace);
            this.grpText.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpText.Location = new System.Drawing.Point(10, 45);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(296, 251);
            this.grpText.TabIndex = 119;
            this.grpText.TabStop = false;
            this.grpText.Text = "Manual Text";
            // 
            // darkCheckBox3
            // 
            this.darkCheckBox3.AutoSize = true;
            this.darkCheckBox3.Location = new System.Drawing.Point(76, 312);
            this.darkCheckBox3.Name = "darkCheckBox3";
            this.darkCheckBox3.Size = new System.Drawing.Size(104, 17);
            this.darkCheckBox3.TabIndex = 110;
            this.darkCheckBox3.Text = "Run indefinitely?";
            // 
            // EventCommandText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpShowText);
            this.Name = "EventCommandText";
            this.Size = new System.Drawing.Size(320, 439);
            this.grpShowText.ResumeLayout(false);
            this.grpTemplates.ResumeLayout(false);
            this.grpTemplates.PerformLayout();
            this.grpChatboxSettings.ResumeLayout(false);
            this.grpChatboxSettings.PerformLayout();
            this.grpText.ResumeLayout(false);
            this.grpText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpShowText;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkTextBox txtShowText;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Panel pnlFace;
        private DarkComboBox cmbFace;
        private System.Windows.Forms.Label lblFace;
        private System.Windows.Forms.Label lblCommands;
        private DarkGroupBox grpTemplates;
        private DarkRadioButton rdoQuest;
        private DarkRadioButton rdoItem;
        private DarkCheckBox darkCheckBox2;
        private DarkComboBox cmbQuests;
        private System.Windows.Forms.Label lblQuest;
        private System.Windows.Forms.Label lblItem;
        private DarkComboBox cmbItems;
        private DarkCheckBox chkTemplate;
        private DarkRadioButton rdoInventorySpace;
        private DarkGroupBox grpChatboxSettings;
        private DarkCheckBox chkSendToChatbox;
        private DarkComboBox cmbChannel;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblColor;
        private DarkComboBox cmbColor;
        private DarkCheckBox darkCheckBox1;
        private DarkGroupBox grpText;
        private DarkCheckBox darkCheckBox3;
    }
}
