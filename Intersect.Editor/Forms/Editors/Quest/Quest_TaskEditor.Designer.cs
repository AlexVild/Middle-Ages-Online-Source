using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Quest
{
    partial class QuestTaskEditor
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
            this.btnEditTaskEvent = new DarkUI.Controls.DarkButton();
            this.txtStartDesc = new DarkUI.Controls.DarkTextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.cmbTaskType = new DarkUI.Controls.DarkComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.grpKillNpcs = new DarkUI.Controls.DarkGroupBox();
            this.nudNpcQuantity = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbNpc = new DarkUI.Controls.DarkComboBox();
            this.lblNpc = new System.Windows.Forms.Label();
            this.lblNpcQuantity = new System.Windows.Forms.Label();
            this.grpGatherItems = new DarkUI.Controls.DarkGroupBox();
            this.nudItemAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbItem = new DarkUI.Controls.DarkComboBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblItemQuantity = new System.Windows.Forms.Label();
            this.lblEventDriven = new System.Windows.Forms.Label();
            this.grpCraftCrafts = new DarkUI.Controls.DarkGroupBox();
            this.nudCraftAmt = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbCraft = new DarkUI.Controls.DarkComboBox();
            this.lblCraft = new System.Windows.Forms.Label();
            this.lblCraftAmount = new System.Windows.Forms.Label();
            this.grpResource = new DarkUI.Controls.DarkGroupBox();
            this.nudResourceAmt = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbResource = new DarkUI.Controls.DarkComboBox();
            this.lblResource = new System.Windows.Forms.Label();
            this.lblResourceAmount = new System.Windows.Forms.Label();
            this.grpEditor.SuspendLayout();
            this.grpKillNpcs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNpcQuantity)).BeginInit();
            this.grpGatherItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemAmount)).BeginInit();
            this.grpCraftCrafts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCraftAmt)).BeginInit();
            this.grpResource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResourceAmt)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEditor
            // 
            this.grpEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEditor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEditor.Controls.Add(this.grpResource);
            this.grpEditor.Controls.Add(this.grpCraftCrafts);
            this.grpEditor.Controls.Add(this.btnEditTaskEvent);
            this.grpEditor.Controls.Add(this.txtStartDesc);
            this.grpEditor.Controls.Add(this.lblDesc);
            this.grpEditor.Controls.Add(this.btnSave);
            this.grpEditor.Controls.Add(this.cmbTaskType);
            this.grpEditor.Controls.Add(this.lblType);
            this.grpEditor.Controls.Add(this.btnCancel);
            this.grpEditor.Controls.Add(this.grpKillNpcs);
            this.grpEditor.Controls.Add(this.grpGatherItems);
            this.grpEditor.Controls.Add(this.lblEventDriven);
            this.grpEditor.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEditor.Location = new System.Drawing.Point(-2, 3);
            this.grpEditor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEditor.Size = new System.Drawing.Size(384, 402);
            this.grpEditor.TabIndex = 18;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Task Editor";
            // 
            // btnEditTaskEvent
            // 
            this.btnEditTaskEvent.Location = new System.Drawing.Point(15, 311);
            this.btnEditTaskEvent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditTaskEvent.Name = "btnEditTaskEvent";
            this.btnEditTaskEvent.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnEditTaskEvent.Size = new System.Drawing.Size(354, 35);
            this.btnEditTaskEvent.TabIndex = 30;
            this.btnEditTaskEvent.Text = "Edit Task Completion Event";
            this.btnEditTaskEvent.Click += new System.EventHandler(this.btnEditTaskEvent_Click);
            // 
            // txtStartDesc
            // 
            this.txtStartDesc.AcceptsReturn = true;
            this.txtStartDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtStartDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtStartDesc.Location = new System.Drawing.Point(132, 62);
            this.txtStartDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStartDesc.Multiline = true;
            this.txtStartDesc.Name = "txtStartDesc";
            this.txtStartDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStartDesc.Size = new System.Drawing.Size(234, 97);
            this.txtStartDesc.TabIndex = 27;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(14, 65);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(50, 20);
            this.lblDesc.TabIndex = 26;
            this.lblDesc.Text = "Desc:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 355);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Ok";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTaskType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTaskType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTaskType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTaskType.DrawDropdownHoverOutline = false;
            this.cmbTaskType.DrawFocusRectangle = false;
            this.cmbTaskType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTaskType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTaskType.FormattingEnabled = true;
            this.cmbTaskType.Items.AddRange(new object[] {
            "Event Driven",
            "Gather Item(s)",
            "Kill NPC(s)"});
            this.cmbTaskType.Location = new System.Drawing.Point(132, 20);
            this.cmbTaskType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Size = new System.Drawing.Size(234, 27);
            this.cmbTaskType.TabIndex = 22;
            this.cmbTaskType.Text = "Event Driven";
            this.cmbTaskType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTaskType.SelectedIndexChanged += new System.EventHandler(this.cmbConditionType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(9, 25);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(85, 20);
            this.lblType.TabIndex = 21;
            this.lblType.Text = "Task Type:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(136, 355);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpKillNpcs
            // 
            this.grpKillNpcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpKillNpcs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpKillNpcs.Controls.Add(this.nudNpcQuantity);
            this.grpKillNpcs.Controls.Add(this.cmbNpc);
            this.grpKillNpcs.Controls.Add(this.lblNpc);
            this.grpKillNpcs.Controls.Add(this.lblNpcQuantity);
            this.grpKillNpcs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpKillNpcs.Location = new System.Drawing.Point(15, 169);
            this.grpKillNpcs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpKillNpcs.Name = "grpKillNpcs";
            this.grpKillNpcs.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpKillNpcs.Size = new System.Drawing.Size(354, 128);
            this.grpKillNpcs.TabIndex = 28;
            this.grpKillNpcs.TabStop = false;
            this.grpKillNpcs.Text = "Kill NPC(s)";
            this.grpKillNpcs.Visible = false;
            // 
            // nudNpcQuantity
            // 
            this.nudNpcQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudNpcQuantity.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudNpcQuantity.Location = new System.Drawing.Point(154, 83);
            this.nudNpcQuantity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudNpcQuantity.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudNpcQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNpcQuantity.Name = "nudNpcQuantity";
            this.nudNpcQuantity.Size = new System.Drawing.Size(174, 26);
            this.nudNpcQuantity.TabIndex = 64;
            this.nudNpcQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbNpc
            // 
            this.cmbNpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbNpc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbNpc.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbNpc.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbNpc.DrawDropdownHoverOutline = false;
            this.cmbNpc.DrawFocusRectangle = false;
            this.cmbNpc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbNpc.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbNpc.FormattingEnabled = true;
            this.cmbNpc.Location = new System.Drawing.Point(156, 32);
            this.cmbNpc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbNpc.Name = "cmbNpc";
            this.cmbNpc.Size = new System.Drawing.Size(172, 27);
            this.cmbNpc.TabIndex = 3;
            this.cmbNpc.Text = null;
            this.cmbNpc.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblNpc
            // 
            this.lblNpc.AutoSize = true;
            this.lblNpc.Location = new System.Drawing.Point(10, 37);
            this.lblNpc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNpc.Name = "lblNpc";
            this.lblNpc.Size = new System.Drawing.Size(41, 20);
            this.lblNpc.TabIndex = 2;
            this.lblNpc.Text = "NPC";
            // 
            // lblNpcQuantity
            // 
            this.lblNpcQuantity.AutoSize = true;
            this.lblNpcQuantity.Location = new System.Drawing.Point(10, 86);
            this.lblNpcQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNpcQuantity.Name = "lblNpcQuantity";
            this.lblNpcQuantity.Size = new System.Drawing.Size(69, 20);
            this.lblNpcQuantity.TabIndex = 0;
            this.lblNpcQuantity.Text = "Amount:";
            // 
            // grpGatherItems
            // 
            this.grpGatherItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGatherItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGatherItems.Controls.Add(this.nudItemAmount);
            this.grpGatherItems.Controls.Add(this.cmbItem);
            this.grpGatherItems.Controls.Add(this.lblItem);
            this.grpGatherItems.Controls.Add(this.lblItemQuantity);
            this.grpGatherItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGatherItems.Location = new System.Drawing.Point(14, 172);
            this.grpGatherItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpGatherItems.Name = "grpGatherItems";
            this.grpGatherItems.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpGatherItems.Size = new System.Drawing.Size(354, 128);
            this.grpGatherItems.TabIndex = 25;
            this.grpGatherItems.TabStop = false;
            this.grpGatherItems.Text = "Gather Item(s)";
            this.grpGatherItems.Visible = false;
            // 
            // nudItemAmount
            // 
            this.nudItemAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudItemAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudItemAmount.Location = new System.Drawing.Point(156, 80);
            this.nudItemAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudItemAmount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudItemAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudItemAmount.Name = "nudItemAmount";
            this.nudItemAmount.Size = new System.Drawing.Size(174, 26);
            this.nudItemAmount.TabIndex = 63;
            this.nudItemAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbItem
            // 
            this.cmbItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbItem.DrawDropdownHoverOutline = false;
            this.cmbItem.DrawFocusRectangle = false;
            this.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(156, 28);
            this.cmbItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(172, 27);
            this.cmbItem.TabIndex = 3;
            this.cmbItem.Text = "Equal To";
            this.cmbItem.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(10, 32);
            this.lblItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(45, 20);
            this.lblItem.TabIndex = 2;
            this.lblItem.Text = "Item:";
            // 
            // lblItemQuantity
            // 
            this.lblItemQuantity.AutoSize = true;
            this.lblItemQuantity.Location = new System.Drawing.Point(10, 86);
            this.lblItemQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemQuantity.Name = "lblItemQuantity";
            this.lblItemQuantity.Size = new System.Drawing.Size(69, 20);
            this.lblItemQuantity.TabIndex = 0;
            this.lblItemQuantity.Text = "Amount:";
            // 
            // lblEventDriven
            // 
            this.lblEventDriven.Location = new System.Drawing.Point(20, 194);
            this.lblEventDriven.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEventDriven.Name = "lblEventDriven";
            this.lblEventDriven.Size = new System.Drawing.Size(339, 86);
            this.lblEventDriven.TabIndex = 29;
            this.lblEventDriven.Text = "Event Driven: The description should lead the player to an event. The event will " +
    "then complete the task using the complete quest task command.";
            // 
            // grpCraftCrafts
            // 
            this.grpCraftCrafts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCraftCrafts.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCraftCrafts.Controls.Add(this.nudCraftAmt);
            this.grpCraftCrafts.Controls.Add(this.cmbCraft);
            this.grpCraftCrafts.Controls.Add(this.lblCraft);
            this.grpCraftCrafts.Controls.Add(this.lblCraftAmount);
            this.grpCraftCrafts.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCraftCrafts.Location = new System.Drawing.Point(17, 173);
            this.grpCraftCrafts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCraftCrafts.Name = "grpCraftCrafts";
            this.grpCraftCrafts.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCraftCrafts.Size = new System.Drawing.Size(354, 128);
            this.grpCraftCrafts.TabIndex = 65;
            this.grpCraftCrafts.TabStop = false;
            this.grpCraftCrafts.Text = "Craft Craft(s)";
            this.grpCraftCrafts.Visible = false;
            // 
            // nudCraftAmt
            // 
            this.nudCraftAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCraftAmt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCraftAmt.Location = new System.Drawing.Point(89, 83);
            this.nudCraftAmt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudCraftAmt.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCraftAmt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCraftAmt.Name = "nudCraftAmt";
            this.nudCraftAmt.Size = new System.Drawing.Size(239, 26);
            this.nudCraftAmt.TabIndex = 64;
            this.nudCraftAmt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbCraft
            // 
            this.cmbCraft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCraft.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCraft.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCraft.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCraft.DrawDropdownHoverOutline = false;
            this.cmbCraft.DrawFocusRectangle = false;
            this.cmbCraft.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCraft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCraft.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCraft.FormattingEnabled = true;
            this.cmbCraft.Location = new System.Drawing.Point(89, 32);
            this.cmbCraft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCraft.Name = "cmbCraft";
            this.cmbCraft.Size = new System.Drawing.Size(239, 27);
            this.cmbCraft.TabIndex = 3;
            this.cmbCraft.Text = null;
            this.cmbCraft.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblCraft
            // 
            this.lblCraft.AutoSize = true;
            this.lblCraft.Location = new System.Drawing.Point(10, 37);
            this.lblCraft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCraft.Name = "lblCraft";
            this.lblCraft.Size = new System.Drawing.Size(44, 20);
            this.lblCraft.TabIndex = 2;
            this.lblCraft.Text = "Craft";
            // 
            // lblCraftAmount
            // 
            this.lblCraftAmount.AutoSize = true;
            this.lblCraftAmount.Location = new System.Drawing.Point(10, 86);
            this.lblCraftAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCraftAmount.Name = "lblCraftAmount";
            this.lblCraftAmount.Size = new System.Drawing.Size(69, 20);
            this.lblCraftAmount.TabIndex = 0;
            this.lblCraftAmount.Text = "Amount:";
            // 
            // grpResource
            // 
            this.grpResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpResource.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpResource.Controls.Add(this.nudResourceAmt);
            this.grpResource.Controls.Add(this.cmbResource);
            this.grpResource.Controls.Add(this.lblResource);
            this.grpResource.Controls.Add(this.lblResourceAmount);
            this.grpResource.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpResource.Location = new System.Drawing.Point(15, 174);
            this.grpResource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpResource.Name = "grpResource";
            this.grpResource.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpResource.Size = new System.Drawing.Size(354, 128);
            this.grpResource.TabIndex = 66;
            this.grpResource.TabStop = false;
            this.grpResource.Text = "Harvest Resource(s)";
            this.grpResource.Visible = false;
            // 
            // nudResourceAmt
            // 
            this.nudResourceAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudResourceAmt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudResourceAmt.Location = new System.Drawing.Point(96, 83);
            this.nudResourceAmt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudResourceAmt.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudResourceAmt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudResourceAmt.Name = "nudResourceAmt";
            this.nudResourceAmt.Size = new System.Drawing.Size(232, 26);
            this.nudResourceAmt.TabIndex = 64;
            this.nudResourceAmt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbResource
            // 
            this.cmbResource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbResource.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbResource.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbResource.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbResource.DrawDropdownHoverOutline = false;
            this.cmbResource.DrawFocusRectangle = false;
            this.cmbResource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbResource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbResource.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbResource.FormattingEnabled = true;
            this.cmbResource.Location = new System.Drawing.Point(96, 32);
            this.cmbResource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbResource.Name = "cmbResource";
            this.cmbResource.Size = new System.Drawing.Size(232, 27);
            this.cmbResource.TabIndex = 3;
            this.cmbResource.Text = null;
            this.cmbResource.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblResource
            // 
            this.lblResource.AutoSize = true;
            this.lblResource.Location = new System.Drawing.Point(10, 37);
            this.lblResource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResource.Name = "lblResource";
            this.lblResource.Size = new System.Drawing.Size(78, 20);
            this.lblResource.TabIndex = 2;
            this.lblResource.Text = "Resource";
            // 
            // lblResourceAmount
            // 
            this.lblResourceAmount.AutoSize = true;
            this.lblResourceAmount.Location = new System.Drawing.Point(10, 86);
            this.lblResourceAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResourceAmount.Name = "lblResourceAmount";
            this.lblResourceAmount.Size = new System.Drawing.Size(69, 20);
            this.lblResourceAmount.TabIndex = 0;
            this.lblResourceAmount.Text = "Amount:";
            // 
            // QuestTaskEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.grpEditor);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "QuestTaskEditor";
            this.Size = new System.Drawing.Size(382, 411);
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            this.grpKillNpcs.ResumeLayout(false);
            this.grpKillNpcs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNpcQuantity)).EndInit();
            this.grpGatherItems.ResumeLayout(false);
            this.grpGatherItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudItemAmount)).EndInit();
            this.grpCraftCrafts.ResumeLayout(false);
            this.grpCraftCrafts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCraftAmt)).EndInit();
            this.grpResource.ResumeLayout(false);
            this.grpResource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResourceAmt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpEditor;
        private DarkButton btnSave;
        private DarkComboBox cmbTaskType;
        private System.Windows.Forms.Label lblType;
        private DarkButton btnCancel;
        private DarkGroupBox grpGatherItems;
        private DarkComboBox cmbItem;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblItemQuantity;
        private System.Windows.Forms.Label lblDesc;
        private DarkTextBox txtStartDesc;
        private DarkGroupBox grpKillNpcs;
        private DarkComboBox cmbNpc;
        private System.Windows.Forms.Label lblNpc;
        private System.Windows.Forms.Label lblNpcQuantity;
        private System.Windows.Forms.Label lblEventDriven;
        private DarkButton btnEditTaskEvent;
        private DarkNumericUpDown nudItemAmount;
        private DarkNumericUpDown nudNpcQuantity;
        private DarkGroupBox grpCraftCrafts;
        private DarkNumericUpDown nudCraftAmt;
        private DarkComboBox cmbCraft;
        private System.Windows.Forms.Label lblCraft;
        private System.Windows.Forms.Label lblCraftAmount;
        private DarkGroupBox grpResource;
        private DarkNumericUpDown nudResourceAmt;
        private DarkComboBox cmbResource;
        private System.Windows.Forms.Label lblResource;
        private System.Windows.Forms.Label lblResourceAmount;
    }
}
