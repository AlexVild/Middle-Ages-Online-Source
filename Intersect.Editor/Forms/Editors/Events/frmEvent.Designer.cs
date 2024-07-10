using System.ComponentModel;
using System.Windows.Forms;
using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events
{
    partial class FrmEvent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.Windows.Forms.TreeNode treeNode108 = new System.Windows.Forms.TreeNode("Show Text");
            System.Windows.Forms.TreeNode treeNode109 = new System.Windows.Forms.TreeNode("Show Options");
            System.Windows.Forms.TreeNode treeNode110 = new System.Windows.Forms.TreeNode("Input Variable");
            System.Windows.Forms.TreeNode treeNode111 = new System.Windows.Forms.TreeNode("Add Chatbox Text");
            System.Windows.Forms.TreeNode treeNode112 = new System.Windows.Forms.TreeNode("Dialogue", new System.Windows.Forms.TreeNode[] {
            treeNode108,
            treeNode109,
            treeNode110,
            treeNode111});
            System.Windows.Forms.TreeNode treeNode113 = new System.Windows.Forms.TreeNode("Set Variable");
            System.Windows.Forms.TreeNode treeNode114 = new System.Windows.Forms.TreeNode("Set Self Switch");
            System.Windows.Forms.TreeNode treeNode115 = new System.Windows.Forms.TreeNode("Conditional Branch");
            System.Windows.Forms.TreeNode treeNode116 = new System.Windows.Forms.TreeNode("Exit Event Process");
            System.Windows.Forms.TreeNode treeNode117 = new System.Windows.Forms.TreeNode("Label");
            System.Windows.Forms.TreeNode treeNode118 = new System.Windows.Forms.TreeNode("Go To Label");
            System.Windows.Forms.TreeNode treeNode119 = new System.Windows.Forms.TreeNode("Start Common Event");
            System.Windows.Forms.TreeNode treeNode120 = new System.Windows.Forms.TreeNode("Reset Variables in Group");
            System.Windows.Forms.TreeNode treeNode121 = new System.Windows.Forms.TreeNode("Logic Flow", new System.Windows.Forms.TreeNode[] {
            treeNode113,
            treeNode114,
            treeNode115,
            treeNode116,
            treeNode117,
            treeNode118,
            treeNode119,
            treeNode120});
            System.Windows.Forms.TreeNode treeNode122 = new System.Windows.Forms.TreeNode("Restore HP");
            System.Windows.Forms.TreeNode treeNode123 = new System.Windows.Forms.TreeNode("Restore MP");
            System.Windows.Forms.TreeNode treeNode124 = new System.Windows.Forms.TreeNode("Level Up");
            System.Windows.Forms.TreeNode treeNode125 = new System.Windows.Forms.TreeNode("Give Experience");
            System.Windows.Forms.TreeNode treeNode126 = new System.Windows.Forms.TreeNode("Change Level");
            System.Windows.Forms.TreeNode treeNode127 = new System.Windows.Forms.TreeNode("Change Spells");
            System.Windows.Forms.TreeNode treeNode128 = new System.Windows.Forms.TreeNode("Change Items");
            System.Windows.Forms.TreeNode treeNode129 = new System.Windows.Forms.TreeNode("Change Sprite");
            System.Windows.Forms.TreeNode treeNode130 = new System.Windows.Forms.TreeNode("Change Player Color");
            System.Windows.Forms.TreeNode treeNode131 = new System.Windows.Forms.TreeNode("Change Face");
            System.Windows.Forms.TreeNode treeNode132 = new System.Windows.Forms.TreeNode("Change Gender");
            System.Windows.Forms.TreeNode treeNode133 = new System.Windows.Forms.TreeNode("Set Access");
            System.Windows.Forms.TreeNode treeNode134 = new System.Windows.Forms.TreeNode("Change Class");
            System.Windows.Forms.TreeNode treeNode135 = new System.Windows.Forms.TreeNode("Equip/Unequip Item");
            System.Windows.Forms.TreeNode treeNode136 = new System.Windows.Forms.TreeNode("Change Name Color");
            System.Windows.Forms.TreeNode treeNode137 = new System.Windows.Forms.TreeNode("Change Player Label");
            System.Windows.Forms.TreeNode treeNode138 = new System.Windows.Forms.TreeNode("Change Player Name");
            System.Windows.Forms.TreeNode treeNode139 = new System.Windows.Forms.TreeNode("Reset Stat Point Allocations");
            System.Windows.Forms.TreeNode treeNode140 = new System.Windows.Forms.TreeNode("Set Vehicle");
            System.Windows.Forms.TreeNode treeNode141 = new System.Windows.Forms.TreeNode("NPC Guild Management");
            System.Windows.Forms.TreeNode treeNode142 = new System.Windows.Forms.TreeNode("Add Inspiration");
            System.Windows.Forms.TreeNode treeNode143 = new System.Windows.Forms.TreeNode("Roll Loot");
            System.Windows.Forms.TreeNode treeNode144 = new System.Windows.Forms.TreeNode("Unlock Label");
            System.Windows.Forms.TreeNode treeNode145 = new System.Windows.Forms.TreeNode("Change Respawn Point");
            System.Windows.Forms.TreeNode treeNode146 = new System.Windows.Forms.TreeNode("Change Recipes");
            System.Windows.Forms.TreeNode treeNode147 = new System.Windows.Forms.TreeNode("Change Bestiary");
            System.Windows.Forms.TreeNode treeNode148 = new System.Windows.Forms.TreeNode("Change Weapon Track Progress");
            System.Windows.Forms.TreeNode treeNode149 = new System.Windows.Forms.TreeNode("Change Challenge Progress");
            System.Windows.Forms.TreeNode treeNode150 = new System.Windows.Forms.TreeNode("Change Enhancement");
            System.Windows.Forms.TreeNode treeNode151 = new System.Windows.Forms.TreeNode("Remove Permabuff");
            System.Windows.Forms.TreeNode treeNode152 = new System.Windows.Forms.TreeNode("Change Champion Spawn Settings");
            System.Windows.Forms.TreeNode treeNode153 = new System.Windows.Forms.TreeNode("Cast Spell On");
            System.Windows.Forms.TreeNode treeNode154 = new System.Windows.Forms.TreeNode("Player Control", new System.Windows.Forms.TreeNode[] {
            treeNode122,
            treeNode123,
            treeNode124,
            treeNode125,
            treeNode126,
            treeNode127,
            treeNode128,
            treeNode129,
            treeNode130,
            treeNode131,
            treeNode132,
            treeNode133,
            treeNode134,
            treeNode135,
            treeNode136,
            treeNode137,
            treeNode138,
            treeNode139,
            treeNode140,
            treeNode141,
            treeNode142,
            treeNode143,
            treeNode144,
            treeNode145,
            treeNode146,
            treeNode147,
            treeNode148,
            treeNode149,
            treeNode150,
            treeNode151,
            treeNode152,
            treeNode153});
            System.Windows.Forms.TreeNode treeNode155 = new System.Windows.Forms.TreeNode("Warp Player");
            System.Windows.Forms.TreeNode treeNode156 = new System.Windows.Forms.TreeNode("Set Move Route");
            System.Windows.Forms.TreeNode treeNode157 = new System.Windows.Forms.TreeNode("Wait for Route Completion");
            System.Windows.Forms.TreeNode treeNode158 = new System.Windows.Forms.TreeNode("Hold Player");
            System.Windows.Forms.TreeNode treeNode159 = new System.Windows.Forms.TreeNode("Release Player");
            System.Windows.Forms.TreeNode treeNode160 = new System.Windows.Forms.TreeNode("Spawn NPC");
            System.Windows.Forms.TreeNode treeNode161 = new System.Windows.Forms.TreeNode("Despawn NPC");
            System.Windows.Forms.TreeNode treeNode162 = new System.Windows.Forms.TreeNode("Hide Player");
            System.Windows.Forms.TreeNode treeNode163 = new System.Windows.Forms.TreeNode("Show Player");
            System.Windows.Forms.TreeNode treeNode164 = new System.Windows.Forms.TreeNode("Change Map Spawn Group");
            System.Windows.Forms.TreeNode treeNode165 = new System.Windows.Forms.TreeNode("Hide Event");
            System.Windows.Forms.TreeNode treeNode166 = new System.Windows.Forms.TreeNode("Show Event");
            System.Windows.Forms.TreeNode treeNode167 = new System.Windows.Forms.TreeNode("Movement", new System.Windows.Forms.TreeNode[] {
            treeNode155,
            treeNode156,
            treeNode157,
            treeNode158,
            treeNode159,
            treeNode160,
            treeNode161,
            treeNode162,
            treeNode163,
            treeNode164,
            treeNode165,
            treeNode166});
            System.Windows.Forms.TreeNode treeNode168 = new System.Windows.Forms.TreeNode("Play Animation");
            System.Windows.Forms.TreeNode treeNode169 = new System.Windows.Forms.TreeNode("Play BGM");
            System.Windows.Forms.TreeNode treeNode170 = new System.Windows.Forms.TreeNode("Fadeout BGM");
            System.Windows.Forms.TreeNode treeNode171 = new System.Windows.Forms.TreeNode("Play Sound");
            System.Windows.Forms.TreeNode treeNode172 = new System.Windows.Forms.TreeNode("Stop Sounds");
            System.Windows.Forms.TreeNode treeNode173 = new System.Windows.Forms.TreeNode("Show Picture");
            System.Windows.Forms.TreeNode treeNode174 = new System.Windows.Forms.TreeNode("Hide Picture");
            System.Windows.Forms.TreeNode treeNode175 = new System.Windows.Forms.TreeNode("Flash Screen");
            System.Windows.Forms.TreeNode treeNode176 = new System.Windows.Forms.TreeNode("Fade In");
            System.Windows.Forms.TreeNode treeNode177 = new System.Windows.Forms.TreeNode("Fade Out");
            System.Windows.Forms.TreeNode treeNode178 = new System.Windows.Forms.TreeNode("Shake Screen");
            System.Windows.Forms.TreeNode treeNode179 = new System.Windows.Forms.TreeNode("Special Effects", new System.Windows.Forms.TreeNode[] {
            treeNode168,
            treeNode169,
            treeNode170,
            treeNode171,
            treeNode172,
            treeNode173,
            treeNode174,
            treeNode175,
            treeNode176,
            treeNode177,
            treeNode178});
            System.Windows.Forms.TreeNode treeNode180 = new System.Windows.Forms.TreeNode("Start Quest");
            System.Windows.Forms.TreeNode treeNode181 = new System.Windows.Forms.TreeNode("Complete Quest Task");
            System.Windows.Forms.TreeNode treeNode182 = new System.Windows.Forms.TreeNode("End Quest");
            System.Windows.Forms.TreeNode treeNode183 = new System.Windows.Forms.TreeNode("Random Quest from List");
            System.Windows.Forms.TreeNode treeNode184 = new System.Windows.Forms.TreeNode("Open Quest Board");
            System.Windows.Forms.TreeNode treeNode185 = new System.Windows.Forms.TreeNode("Quest Control", new System.Windows.Forms.TreeNode[] {
            treeNode180,
            treeNode181,
            treeNode182,
            treeNode183,
            treeNode184});
            System.Windows.Forms.TreeNode treeNode186 = new System.Windows.Forms.TreeNode("Wait...");
            System.Windows.Forms.TreeNode treeNode187 = new System.Windows.Forms.TreeNode("Open Leaderboard...");
            System.Windows.Forms.TreeNode treeNode188 = new System.Windows.Forms.TreeNode("Clear Record...");
            System.Windows.Forms.TreeNode treeNode189 = new System.Windows.Forms.TreeNode("Reset Permadead NPCs");
            System.Windows.Forms.TreeNode treeNode190 = new System.Windows.Forms.TreeNode("Reset Global Event Positions");
            System.Windows.Forms.TreeNode treeNode191 = new System.Windows.Forms.TreeNode("Signup for Open Melee");
            System.Windows.Forms.TreeNode treeNode192 = new System.Windows.Forms.TreeNode("Withdraw from Open Melee");
            System.Windows.Forms.TreeNode treeNode193 = new System.Windows.Forms.TreeNode("Etc", new System.Windows.Forms.TreeNode[] {
            treeNode186,
            treeNode187,
            treeNode188,
            treeNode189,
            treeNode190,
            treeNode191,
            treeNode192});
            System.Windows.Forms.TreeNode treeNode194 = new System.Windows.Forms.TreeNode("Open Bank");
            System.Windows.Forms.TreeNode treeNode195 = new System.Windows.Forms.TreeNode("Open Shop");
            System.Windows.Forms.TreeNode treeNode196 = new System.Windows.Forms.TreeNode("Open Crafting Station");
            System.Windows.Forms.TreeNode treeNode197 = new System.Windows.Forms.TreeNode("Close Crafting Table");
            System.Windows.Forms.TreeNode treeNode198 = new System.Windows.Forms.TreeNode("Open Deconstructor");
            System.Windows.Forms.TreeNode treeNode199 = new System.Windows.Forms.TreeNode("Open Enhancement Window");
            System.Windows.Forms.TreeNode treeNode200 = new System.Windows.Forms.TreeNode("Open Upgrade Station");
            System.Windows.Forms.TreeNode treeNode201 = new System.Windows.Forms.TreeNode("Shop and Bank", new System.Windows.Forms.TreeNode[] {
            treeNode194,
            treeNode195,
            treeNode196,
            treeNode197,
            treeNode198,
            treeNode199,
            treeNode200});
            System.Windows.Forms.TreeNode treeNode202 = new System.Windows.Forms.TreeNode("Create Guild");
            System.Windows.Forms.TreeNode treeNode203 = new System.Windows.Forms.TreeNode("Disband Guild");
            System.Windows.Forms.TreeNode treeNode204 = new System.Windows.Forms.TreeNode("Open Guild Bank");
            System.Windows.Forms.TreeNode treeNode205 = new System.Windows.Forms.TreeNode("Set Guild Bank Slots Count");
            System.Windows.Forms.TreeNode treeNode206 = new System.Windows.Forms.TreeNode("Guilds", new System.Windows.Forms.TreeNode[] {
            treeNode202,
            treeNode203,
            treeNode204,
            treeNode205});
            System.Windows.Forms.TreeNode treeNode207 = new System.Windows.Forms.TreeNode("Start Timer");
            System.Windows.Forms.TreeNode treeNode208 = new System.Windows.Forms.TreeNode("Modify Timer");
            System.Windows.Forms.TreeNode treeNode209 = new System.Windows.Forms.TreeNode("Stop Timer");
            System.Windows.Forms.TreeNode treeNode210 = new System.Windows.Forms.TreeNode("Timers", new System.Windows.Forms.TreeNode[] {
            treeNode207,
            treeNode208,
            treeNode209});
            System.Windows.Forms.TreeNode treeNode211 = new System.Windows.Forms.TreeNode("Change Dungeon");
            System.Windows.Forms.TreeNode treeNode212 = new System.Windows.Forms.TreeNode("Obtain Treasure Gnome");
            System.Windows.Forms.TreeNode treeNode213 = new System.Windows.Forms.TreeNode("Roll Dungeon Loot");
            System.Windows.Forms.TreeNode treeNode214 = new System.Windows.Forms.TreeNode("Dungeons", new System.Windows.Forms.TreeNode[] {
            treeNode211,
            treeNode212,
            treeNode213});
            this.lblName = new System.Windows.Forms.Label();
            this.txtEventname = new DarkUI.Controls.DarkTextBox();
            this.grpEntityOptions = new DarkUI.Controls.DarkGroupBox();
            this.grpQuestAnimation = new DarkUI.Controls.DarkGroupBox();
            this.cmbQuestAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblQuestAnimation = new System.Windows.Forms.Label();
            this.cmbQuest = new DarkUI.Controls.DarkComboBox();
            this.grpExtra = new DarkUI.Controls.DarkGroupBox();
            this.chkIdleAnim = new DarkUI.Controls.DarkCheckBox();
            this.chkKillAfterOne = new DarkUI.Controls.DarkCheckBox();
            this.chkInteractionFreeze = new DarkUI.Controls.DarkCheckBox();
            this.chkWalkingAnimation = new DarkUI.Controls.DarkCheckBox();
            this.chkDirectionFix = new DarkUI.Controls.DarkCheckBox();
            this.chkHideName = new DarkUI.Controls.DarkCheckBox();
            this.chkWalkThrough = new DarkUI.Controls.DarkCheckBox();
            this.grpInspector = new DarkUI.Controls.DarkGroupBox();
            this.pnlFacePreview = new System.Windows.Forms.Panel();
            this.lblInspectorDesc = new System.Windows.Forms.Label();
            this.txtDesc = new DarkUI.Controls.DarkTextBox();
            this.chkDisableInspector = new DarkUI.Controls.DarkCheckBox();
            this.cmbPreviewFace = new DarkUI.Controls.DarkComboBox();
            this.lblFace = new System.Windows.Forms.Label();
            this.grpPreview = new DarkUI.Controls.DarkGroupBox();
            this.lblAnimation = new System.Windows.Forms.Label();
            this.cmbAnimation = new DarkUI.Controls.DarkComboBox();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.grpMovement = new DarkUI.Controls.DarkGroupBox();
            this.cmbEnableMoveVar = new DarkUI.Controls.DarkComboBox();
            this.lblEnableMoveVar = new System.Windows.Forms.Label();
            this.lblLayer = new System.Windows.Forms.Label();
            this.cmbLayering = new DarkUI.Controls.DarkComboBox();
            this.cmbEventFreq = new DarkUI.Controls.DarkComboBox();
            this.cmbEventSpeed = new DarkUI.Controls.DarkComboBox();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.btnSetRoute = new DarkUI.Controls.DarkButton();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbMoveType = new DarkUI.Controls.DarkComboBox();
            this.grpTriggers = new DarkUI.Controls.DarkGroupBox();
            this.lblRecordItem = new System.Windows.Forms.Label();
            this.cmbRecordItem = new DarkUI.Controls.DarkComboBox();
            this.lblRecordNumber = new System.Windows.Forms.Label();
            this.nudRecordNumber = new DarkUI.Controls.DarkNumericUpDown();
            this.lblClass = new System.Windows.Forms.Label();
            this.cmbClass = new DarkUI.Controls.DarkComboBox();
            this.cmbVariable = new DarkUI.Controls.DarkComboBox();
            this.lblVariableTrigger = new System.Windows.Forms.Label();
            this.txtCommand = new DarkUI.Controls.DarkTextBox();
            this.lblCommand = new System.Windows.Forms.Label();
            this.lblTriggerVal = new System.Windows.Forms.Label();
            this.cmbTriggerVal = new DarkUI.Controls.DarkComboBox();
            this.cmbTrigger = new DarkUI.Controls.DarkComboBox();
            this.grpEventConditions = new DarkUI.Controls.DarkGroupBox();
            this.btnEditConditions = new DarkUI.Controls.DarkButton();
            this.grpNewCommands = new DarkUI.Controls.DarkGroupBox();
            this.lblCloseCommands = new System.Windows.Forms.Label();
            this.lstCommands = new System.Windows.Forms.TreeView();
            this.grpEventCommands = new DarkUI.Controls.DarkGroupBox();
            this.lstEventCommands = new System.Windows.Forms.ListBox();
            this.grpCreateCommands = new DarkUI.Controls.DarkGroupBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.commandMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCut = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.grpPageOptions = new DarkUI.Controls.DarkGroupBox();
            this.btnClearPage = new DarkUI.Controls.DarkButton();
            this.btnDeletePage = new DarkUI.Controls.DarkButton();
            this.btnPastePage = new DarkUI.Controls.DarkButton();
            this.btnCopyPage = new DarkUI.Controls.DarkButton();
            this.btnNewPage = new DarkUI.Controls.DarkButton();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.chkIsGlobal = new DarkUI.Controls.DarkCheckBox();
            this.pnlTabsContainer = new System.Windows.Forms.Panel();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnTabsRight = new DarkUI.Controls.DarkButton();
            this.btnTabsLeft = new DarkUI.Controls.DarkButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkParallelRun = new DarkUI.Controls.DarkCheckBox();
            this.grpEntityOptions.SuspendLayout();
            this.grpQuestAnimation.SuspendLayout();
            this.grpExtra.SuspendLayout();
            this.grpInspector.SuspendLayout();
            this.grpPreview.SuspendLayout();
            this.grpMovement.SuspendLayout();
            this.grpTriggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecordNumber)).BeginInit();
            this.grpEventConditions.SuspendLayout();
            this.grpNewCommands.SuspendLayout();
            this.grpEventCommands.SuspendLayout();
            this.commandMenu.SuspendLayout();
            this.grpPageOptions.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.pnlTabsContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtEventname
            // 
            this.txtEventname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtEventname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtEventname.Location = new System.Drawing.Point(48, 19);
            this.txtEventname.Name = "txtEventname";
            this.txtEventname.Size = new System.Drawing.Size(124, 20);
            this.txtEventname.TabIndex = 2;
            this.txtEventname.TextChanged += new System.EventHandler(this.txtEventname_TextChanged);
            // 
            // grpEntityOptions
            // 
            this.grpEntityOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEntityOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEntityOptions.Controls.Add(this.grpQuestAnimation);
            this.grpEntityOptions.Controls.Add(this.grpExtra);
            this.grpEntityOptions.Controls.Add(this.grpInspector);
            this.grpEntityOptions.Controls.Add(this.grpPreview);
            this.grpEntityOptions.Controls.Add(this.grpMovement);
            this.grpEntityOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEntityOptions.Location = new System.Drawing.Point(21, 150);
            this.grpEntityOptions.Name = "grpEntityOptions";
            this.grpEntityOptions.Size = new System.Drawing.Size(326, 472);
            this.grpEntityOptions.TabIndex = 12;
            this.grpEntityOptions.TabStop = false;
            this.grpEntityOptions.Text = "Entity Options";
            // 
            // grpQuestAnimation
            // 
            this.grpQuestAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpQuestAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpQuestAnimation.Controls.Add(this.cmbQuestAnimation);
            this.grpQuestAnimation.Controls.Add(this.lblQuestAnimation);
            this.grpQuestAnimation.Controls.Add(this.cmbQuest);
            this.grpQuestAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpQuestAnimation.Location = new System.Drawing.Point(7, 422);
            this.grpQuestAnimation.Name = "grpQuestAnimation";
            this.grpQuestAnimation.Size = new System.Drawing.Size(317, 44);
            this.grpQuestAnimation.TabIndex = 22;
            this.grpQuestAnimation.TabStop = false;
            this.grpQuestAnimation.Text = "Quest Giver Animation";
            // 
            // cmbQuestAnimation
            // 
            this.cmbQuestAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuestAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuestAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuestAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuestAnimation.DrawDropdownHoverOutline = false;
            this.cmbQuestAnimation.DrawFocusRectangle = false;
            this.cmbQuestAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuestAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuestAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuestAnimation.FormattingEnabled = true;
            this.cmbQuestAnimation.Items.AddRange(new object[] {
            "None"});
            this.cmbQuestAnimation.Location = new System.Drawing.Point(181, 13);
            this.cmbQuestAnimation.Name = "cmbQuestAnimation";
            this.cmbQuestAnimation.Size = new System.Drawing.Size(130, 21);
            this.cmbQuestAnimation.TabIndex = 14;
            this.cmbQuestAnimation.Text = "None";
            this.cmbQuestAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbQuestAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbQuestAnimation_SelectedIndexChanged);
            // 
            // lblQuestAnimation
            // 
            this.lblQuestAnimation.AutoSize = true;
            this.lblQuestAnimation.Location = new System.Drawing.Point(122, 16);
            this.lblQuestAnimation.Name = "lblQuestAnimation";
            this.lblQuestAnimation.Size = new System.Drawing.Size(56, 13);
            this.lblQuestAnimation.TabIndex = 10;
            this.lblQuestAnimation.Text = "Animation:";
            this.lblQuestAnimation.Visible = false;
            // 
            // cmbQuest
            // 
            this.cmbQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbQuest.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbQuest.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbQuest.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbQuest.DrawDropdownHoverOutline = false;
            this.cmbQuest.DrawFocusRectangle = false;
            this.cmbQuest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbQuest.FormattingEnabled = true;
            this.cmbQuest.Location = new System.Drawing.Point(6, 13);
            this.cmbQuest.Name = "cmbQuest";
            this.cmbQuest.Size = new System.Drawing.Size(101, 21);
            this.cmbQuest.TabIndex = 2;
            this.cmbQuest.Text = "Quest";
            this.cmbQuest.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbQuest.SelectedIndexChanged += new System.EventHandler(this.cmbQuest_SelectedIndexChanged);
            // 
            // grpExtra
            // 
            this.grpExtra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpExtra.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpExtra.Controls.Add(this.chkIdleAnim);
            this.grpExtra.Controls.Add(this.chkKillAfterOne);
            this.grpExtra.Controls.Add(this.chkInteractionFreeze);
            this.grpExtra.Controls.Add(this.chkWalkingAnimation);
            this.grpExtra.Controls.Add(this.chkDirectionFix);
            this.grpExtra.Controls.Add(this.chkHideName);
            this.grpExtra.Controls.Add(this.chkWalkThrough);
            this.grpExtra.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpExtra.Location = new System.Drawing.Point(9, 351);
            this.grpExtra.Name = "grpExtra";
            this.grpExtra.Size = new System.Drawing.Size(315, 64);
            this.grpExtra.TabIndex = 9;
            this.grpExtra.TabStop = false;
            this.grpExtra.Text = "Extra";
            // 
            // chkIdleAnim
            // 
            this.chkIdleAnim.AutoSize = true;
            this.chkIdleAnim.Location = new System.Drawing.Point(214, 41);
            this.chkIdleAnim.Name = "chkIdleAnim";
            this.chkIdleAnim.Size = new System.Drawing.Size(92, 17);
            this.chkIdleAnim.TabIndex = 8;
            this.chkIdleAnim.Text = "Idle Animation";
            this.chkIdleAnim.CheckedChanged += new System.EventHandler(this.chkIdleAnim_CheckedChanged);
            // 
            // chkKillAfterOne
            // 
            this.chkKillAfterOne.AutoSize = true;
            this.chkKillAfterOne.Location = new System.Drawing.Point(125, 41);
            this.chkKillAfterOne.Name = "chkKillAfterOne";
            this.chkKillAfterOne.Size = new System.Drawing.Size(78, 17);
            this.chkKillAfterOne.TabIndex = 7;
            this.chkKillAfterOne.Text = "One Player";
            this.chkKillAfterOne.CheckedChanged += new System.EventHandler(this.chkKillAfterOne_CheckedChanged);
            // 
            // chkInteractionFreeze
            // 
            this.chkInteractionFreeze.AutoSize = true;
            this.chkInteractionFreeze.Location = new System.Drawing.Point(6, 41);
            this.chkInteractionFreeze.Name = "chkInteractionFreeze";
            this.chkInteractionFreeze.Size = new System.Drawing.Size(111, 17);
            this.chkInteractionFreeze.TabIndex = 6;
            this.chkInteractionFreeze.Text = "Interaction Freeze";
            this.chkInteractionFreeze.CheckedChanged += new System.EventHandler(this.chkInteractionFreeze_CheckedChanged);
            // 
            // chkWalkingAnimation
            // 
            this.chkWalkingAnimation.AutoSize = true;
            this.chkWalkingAnimation.Location = new System.Drawing.Point(214, 19);
            this.chkWalkingAnimation.Name = "chkWalkingAnimation";
            this.chkWalkingAnimation.Size = new System.Drawing.Size(91, 17);
            this.chkWalkingAnimation.TabIndex = 5;
            this.chkWalkingAnimation.Text = "Walking Anim";
            this.chkWalkingAnimation.CheckedChanged += new System.EventHandler(this.chkWalkingAnimation_CheckedChanged);
            // 
            // chkDirectionFix
            // 
            this.chkDirectionFix.AutoSize = true;
            this.chkDirectionFix.Location = new System.Drawing.Point(156, 19);
            this.chkDirectionFix.Name = "chkDirectionFix";
            this.chkDirectionFix.Size = new System.Drawing.Size(55, 17);
            this.chkDirectionFix.TabIndex = 4;
            this.chkDirectionFix.Text = "Dir Fix";
            this.chkDirectionFix.CheckedChanged += new System.EventHandler(this.chkDirectionFix_CheckedChanged);
            // 
            // chkHideName
            // 
            this.chkHideName.AutoSize = true;
            this.chkHideName.Location = new System.Drawing.Point(75, 19);
            this.chkHideName.Name = "chkHideName";
            this.chkHideName.Size = new System.Drawing.Size(79, 17);
            this.chkHideName.TabIndex = 3;
            this.chkHideName.Text = "Hide Name";
            this.chkHideName.CheckedChanged += new System.EventHandler(this.chkHideName_CheckedChanged);
            // 
            // chkWalkThrough
            // 
            this.chkWalkThrough.AutoSize = true;
            this.chkWalkThrough.Location = new System.Drawing.Point(6, 19);
            this.chkWalkThrough.Name = "chkWalkThrough";
            this.chkWalkThrough.Size = new System.Drawing.Size(69, 17);
            this.chkWalkThrough.TabIndex = 2;
            this.chkWalkThrough.Text = "Passable";
            this.chkWalkThrough.CheckedChanged += new System.EventHandler(this.chkWalkThrough_CheckedChanged);
            // 
            // grpInspector
            // 
            this.grpInspector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpInspector.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpInspector.Controls.Add(this.pnlFacePreview);
            this.grpInspector.Controls.Add(this.lblInspectorDesc);
            this.grpInspector.Controls.Add(this.txtDesc);
            this.grpInspector.Controls.Add(this.chkDisableInspector);
            this.grpInspector.Controls.Add(this.cmbPreviewFace);
            this.grpInspector.Controls.Add(this.lblFace);
            this.grpInspector.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpInspector.Location = new System.Drawing.Point(6, 228);
            this.grpInspector.Name = "grpInspector";
            this.grpInspector.Size = new System.Drawing.Size(316, 117);
            this.grpInspector.TabIndex = 7;
            this.grpInspector.TabStop = false;
            this.grpInspector.Text = "Entity Inspector Options";
            // 
            // pnlFacePreview
            // 
            this.pnlFacePreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFacePreview.Location = new System.Drawing.Point(9, 46);
            this.pnlFacePreview.Name = "pnlFacePreview";
            this.pnlFacePreview.Size = new System.Drawing.Size(64, 64);
            this.pnlFacePreview.TabIndex = 12;
            // 
            // lblInspectorDesc
            // 
            this.lblInspectorDesc.Location = new System.Drawing.Point(79, 42);
            this.lblInspectorDesc.Name = "lblInspectorDesc";
            this.lblInspectorDesc.Size = new System.Drawing.Size(112, 19);
            this.lblInspectorDesc.TabIndex = 11;
            this.lblInspectorDesc.Text = "Inspector Description:";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDesc.Location = new System.Drawing.Point(79, 61);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(231, 50);
            this.txtDesc.TabIndex = 0;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // chkDisableInspector
            // 
            this.chkDisableInspector.Location = new System.Drawing.Point(204, 15);
            this.chkDisableInspector.Name = "chkDisableInspector";
            this.chkDisableInspector.Size = new System.Drawing.Size(107, 21);
            this.chkDisableInspector.TabIndex = 4;
            this.chkDisableInspector.Text = "Disable Inspector";
            this.chkDisableInspector.CheckedChanged += new System.EventHandler(this.chkDisablePreview_CheckedChanged);
            // 
            // cmbPreviewFace
            // 
            this.cmbPreviewFace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbPreviewFace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPreviewFace.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbPreviewFace.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbPreviewFace.DrawDropdownHoverOutline = false;
            this.cmbPreviewFace.DrawFocusRectangle = false;
            this.cmbPreviewFace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPreviewFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreviewFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPreviewFace.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbPreviewFace.FormattingEnabled = true;
            this.cmbPreviewFace.Location = new System.Drawing.Point(46, 15);
            this.cmbPreviewFace.Name = "cmbPreviewFace";
            this.cmbPreviewFace.Size = new System.Drawing.Size(114, 21);
            this.cmbPreviewFace.TabIndex = 10;
            this.cmbPreviewFace.Text = null;
            this.cmbPreviewFace.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbPreviewFace.SelectedIndexChanged += new System.EventHandler(this.cmbPreviewFace_SelectedIndexChanged);
            // 
            // lblFace
            // 
            this.lblFace.AutoSize = true;
            this.lblFace.Location = new System.Drawing.Point(6, 18);
            this.lblFace.Name = "lblFace";
            this.lblFace.Size = new System.Drawing.Size(34, 13);
            this.lblFace.TabIndex = 9;
            this.lblFace.Text = "Face:";
            // 
            // grpPreview
            // 
            this.grpPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPreview.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPreview.Controls.Add(this.lblAnimation);
            this.grpPreview.Controls.Add(this.cmbAnimation);
            this.grpPreview.Controls.Add(this.pnlPreview);
            this.grpPreview.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPreview.Location = new System.Drawing.Point(6, 13);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(160, 163);
            this.grpPreview.TabIndex = 10;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "Preview";
            // 
            // lblAnimation
            // 
            this.lblAnimation.AutoSize = true;
            this.lblAnimation.Location = new System.Drawing.Point(4, 116);
            this.lblAnimation.Name = "lblAnimation";
            this.lblAnimation.Size = new System.Drawing.Size(56, 13);
            this.lblAnimation.TabIndex = 2;
            this.lblAnimation.Text = "Animation:";
            // 
            // cmbAnimation
            // 
            this.cmbAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAnimation.DrawDropdownHoverOutline = false;
            this.cmbAnimation.DrawFocusRectangle = false;
            this.cmbAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAnimation.FormattingEnabled = true;
            this.cmbAnimation.Location = new System.Drawing.Point(20, 132);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(125, 21);
            this.cmbAnimation.TabIndex = 1;
            this.cmbAnimation.Text = null;
            this.cmbAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAnimation_SelectedIndexChanged);
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Location = new System.Drawing.Point(33, 14);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(96, 96);
            this.pnlPreview.TabIndex = 0;
            this.pnlPreview.DoubleClick += new System.EventHandler(this.pnlPreview_DoubleClick);
            // 
            // grpMovement
            // 
            this.grpMovement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpMovement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpMovement.Controls.Add(this.cmbEnableMoveVar);
            this.grpMovement.Controls.Add(this.lblEnableMoveVar);
            this.grpMovement.Controls.Add(this.lblLayer);
            this.grpMovement.Controls.Add(this.cmbLayering);
            this.grpMovement.Controls.Add(this.cmbEventFreq);
            this.grpMovement.Controls.Add(this.cmbEventSpeed);
            this.grpMovement.Controls.Add(this.lblFreq);
            this.grpMovement.Controls.Add(this.lblSpeed);
            this.grpMovement.Controls.Add(this.btnSetRoute);
            this.grpMovement.Controls.Add(this.lblType);
            this.grpMovement.Controls.Add(this.cmbMoveType);
            this.grpMovement.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpMovement.Location = new System.Drawing.Point(169, 13);
            this.grpMovement.Name = "grpMovement";
            this.grpMovement.Size = new System.Drawing.Size(154, 209);
            this.grpMovement.TabIndex = 8;
            this.grpMovement.TabStop = false;
            this.grpMovement.Text = "Movement";
            // 
            // cmbEnableMoveVar
            // 
            this.cmbEnableMoveVar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEnableMoveVar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEnableMoveVar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEnableMoveVar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEnableMoveVar.DrawDropdownHoverOutline = false;
            this.cmbEnableMoveVar.DrawFocusRectangle = false;
            this.cmbEnableMoveVar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEnableMoveVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnableMoveVar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEnableMoveVar.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEnableMoveVar.FormattingEnabled = true;
            this.cmbEnableMoveVar.Items.AddRange(new object[] {
            "None"});
            this.cmbEnableMoveVar.Location = new System.Drawing.Point(6, 182);
            this.cmbEnableMoveVar.Name = "cmbEnableMoveVar";
            this.cmbEnableMoveVar.Size = new System.Drawing.Size(144, 21);
            this.cmbEnableMoveVar.TabIndex = 9;
            this.cmbEnableMoveVar.Text = "None";
            this.cmbEnableMoveVar.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEnableMoveVar.SelectedIndexChanged += new System.EventHandler(this.cmbDisableMoveVar_SelectedIndexChanged);
            // 
            // lblEnableMoveVar
            // 
            this.lblEnableMoveVar.AutoSize = true;
            this.lblEnableMoveVar.Location = new System.Drawing.Point(7, 161);
            this.lblEnableMoveVar.Name = "lblEnableMoveVar";
            this.lblEnableMoveVar.Size = new System.Drawing.Size(92, 13);
            this.lblEnableMoveVar.TabIndex = 8;
            this.lblEnableMoveVar.Text = "Enable Move Var:";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(6, 134);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(36, 13);
            this.lblLayer.TabIndex = 7;
            this.lblLayer.Text = "Layer:";
            // 
            // cmbLayering
            // 
            this.cmbLayering.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbLayering.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbLayering.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbLayering.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbLayering.DrawDropdownHoverOutline = false;
            this.cmbLayering.DrawFocusRectangle = false;
            this.cmbLayering.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLayering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLayering.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbLayering.FormattingEnabled = true;
            this.cmbLayering.Items.AddRange(new object[] {
            "Below Player",
            "Same as Player",
            "Above Player"});
            this.cmbLayering.Location = new System.Drawing.Point(48, 131);
            this.cmbLayering.Name = "cmbLayering";
            this.cmbLayering.Size = new System.Drawing.Size(101, 21);
            this.cmbLayering.TabIndex = 1;
            this.cmbLayering.Text = "Below Player";
            this.cmbLayering.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbLayering.SelectedIndexChanged += new System.EventHandler(this.cmbLayering_SelectedIndexChanged);
            // 
            // cmbEventFreq
            // 
            this.cmbEventFreq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEventFreq.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEventFreq.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEventFreq.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEventFreq.DrawDropdownHoverOutline = false;
            this.cmbEventFreq.DrawFocusRectangle = false;
            this.cmbEventFreq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEventFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEventFreq.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEventFreq.FormattingEnabled = true;
            this.cmbEventFreq.Items.AddRange(new object[] {
            "Not Very Often",
            "Not Often",
            "Normal",
            "Often",
            "Very Often"});
            this.cmbEventFreq.Location = new System.Drawing.Point(48, 104);
            this.cmbEventFreq.Name = "cmbEventFreq";
            this.cmbEventFreq.Size = new System.Drawing.Size(100, 21);
            this.cmbEventFreq.TabIndex = 6;
            this.cmbEventFreq.Text = "Not Very Often";
            this.cmbEventFreq.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEventFreq.SelectedIndexChanged += new System.EventHandler(this.cmbEventFreq_SelectedIndexChanged);
            // 
            // cmbEventSpeed
            // 
            this.cmbEventSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEventSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEventSpeed.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEventSpeed.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEventSpeed.DrawDropdownHoverOutline = false;
            this.cmbEventSpeed.DrawFocusRectangle = false;
            this.cmbEventSpeed.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEventSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEventSpeed.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEventSpeed.FormattingEnabled = true;
            this.cmbEventSpeed.Items.AddRange(new object[] {
            "Slowest",
            "Slower",
            "Normal",
            "Faster",
            "Fastest"});
            this.cmbEventSpeed.Location = new System.Drawing.Point(48, 77);
            this.cmbEventSpeed.Name = "cmbEventSpeed";
            this.cmbEventSpeed.Size = new System.Drawing.Size(100, 21);
            this.cmbEventSpeed.TabIndex = 5;
            this.cmbEventSpeed.Text = "Slowest";
            this.cmbEventSpeed.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEventSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbEventSpeed_SelectedIndexChanged);
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(6, 107);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(31, 13);
            this.lblFreq.TabIndex = 4;
            this.lblFreq.Text = "Freq:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(6, 80);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(41, 13);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "Speed:";
            // 
            // btnSetRoute
            // 
            this.btnSetRoute.Enabled = false;
            this.btnSetRoute.Location = new System.Drawing.Point(73, 43);
            this.btnSetRoute.Name = "btnSetRoute";
            this.btnSetRoute.Padding = new System.Windows.Forms.Padding(5);
            this.btnSetRoute.Size = new System.Drawing.Size(75, 23);
            this.btnSetRoute.TabIndex = 2;
            this.btnSetRoute.Text = "Set Route....";
            this.btnSetRoute.Click += new System.EventHandler(this.btnSetRoute_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "Type:";
            // 
            // cmbMoveType
            // 
            this.cmbMoveType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMoveType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMoveType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMoveType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMoveType.DrawDropdownHoverOutline = false;
            this.cmbMoveType.DrawFocusRectangle = false;
            this.cmbMoveType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMoveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoveType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMoveType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMoveType.FormattingEnabled = true;
            this.cmbMoveType.Items.AddRange(new object[] {
            "None",
            "Random",
            "Move Route"});
            this.cmbMoveType.Location = new System.Drawing.Point(48, 19);
            this.cmbMoveType.Name = "cmbMoveType";
            this.cmbMoveType.Size = new System.Drawing.Size(100, 21);
            this.cmbMoveType.TabIndex = 0;
            this.cmbMoveType.Text = "None";
            this.cmbMoveType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbMoveType.SelectedIndexChanged += new System.EventHandler(this.cmbMoveType_SelectedIndexChanged);
            // 
            // grpTriggers
            // 
            this.grpTriggers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTriggers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTriggers.Controls.Add(this.chkParallelRun);
            this.grpTriggers.Controls.Add(this.lblRecordItem);
            this.grpTriggers.Controls.Add(this.cmbRecordItem);
            this.grpTriggers.Controls.Add(this.lblRecordNumber);
            this.grpTriggers.Controls.Add(this.nudRecordNumber);
            this.grpTriggers.Controls.Add(this.lblClass);
            this.grpTriggers.Controls.Add(this.cmbClass);
            this.grpTriggers.Controls.Add(this.cmbVariable);
            this.grpTriggers.Controls.Add(this.lblVariableTrigger);
            this.grpTriggers.Controls.Add(this.txtCommand);
            this.grpTriggers.Controls.Add(this.lblCommand);
            this.grpTriggers.Controls.Add(this.lblTriggerVal);
            this.grpTriggers.Controls.Add(this.cmbTriggerVal);
            this.grpTriggers.Controls.Add(this.cmbTrigger);
            this.grpTriggers.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTriggers.Location = new System.Drawing.Point(15, 544);
            this.grpTriggers.Name = "grpTriggers";
            this.grpTriggers.Size = new System.Drawing.Size(317, 81);
            this.grpTriggers.TabIndex = 21;
            this.grpTriggers.TabStop = false;
            this.grpTriggers.Text = "Trigger";
            // 
            // lblRecordItem
            // 
            this.lblRecordItem.AutoSize = true;
            this.lblRecordItem.Location = new System.Drawing.Point(109, 20);
            this.lblRecordItem.Name = "lblRecordItem";
            this.lblRecordItem.Size = new System.Drawing.Size(68, 13);
            this.lblRecordItem.TabIndex = 98;
            this.lblRecordItem.Text = "Record Item:";
            this.lblRecordItem.Visible = false;
            // 
            // cmbRecordItem
            // 
            this.cmbRecordItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbRecordItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbRecordItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbRecordItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbRecordItem.DrawDropdownHoverOutline = false;
            this.cmbRecordItem.DrawFocusRectangle = false;
            this.cmbRecordItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRecordItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecordItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRecordItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbRecordItem.FormattingEnabled = true;
            this.cmbRecordItem.Location = new System.Drawing.Point(181, 14);
            this.cmbRecordItem.Name = "cmbRecordItem";
            this.cmbRecordItem.Size = new System.Drawing.Size(130, 21);
            this.cmbRecordItem.TabIndex = 97;
            this.cmbRecordItem.Text = null;
            this.cmbRecordItem.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbRecordItem.SelectedIndexChanged += new System.EventHandler(this.cmbRecordItem_SelectedIndexChanged);
            // 
            // lblRecordNumber
            // 
            this.lblRecordNumber.AutoSize = true;
            this.lblRecordNumber.Location = new System.Drawing.Point(120, 42);
            this.lblRecordNumber.Name = "lblRecordNumber";
            this.lblRecordNumber.Size = new System.Drawing.Size(46, 13);
            this.lblRecordNumber.TabIndex = 96;
            this.lblRecordNumber.Text = "Amount:";
            this.lblRecordNumber.Visible = false;
            // 
            // nudRecordNumber
            // 
            this.nudRecordNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRecordNumber.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRecordNumber.Location = new System.Drawing.Point(186, 40);
            this.nudRecordNumber.Name = "nudRecordNumber";
            this.nudRecordNumber.Size = new System.Drawing.Size(126, 20);
            this.nudRecordNumber.TabIndex = 95;
            this.nudRecordNumber.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudRecordNumber.ValueChanged += new System.EventHandler(this.nudRecordNumber_ValueChanged);
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(140, 17);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(35, 13);
            this.lblClass.TabIndex = 16;
            this.lblClass.Text = "Class:";
            this.lblClass.Visible = false;
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
            this.cmbClass.Location = new System.Drawing.Point(183, 13);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(130, 21);
            this.cmbClass.TabIndex = 15;
            this.cmbClass.Text = "None";
            this.cmbClass.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // cmbVariable
            // 
            this.cmbVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbVariable.DrawDropdownHoverOutline = false;
            this.cmbVariable.DrawFocusRectangle = false;
            this.cmbVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVariable.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbVariable.FormattingEnabled = true;
            this.cmbVariable.Items.AddRange(new object[] {
            "None"});
            this.cmbVariable.Location = new System.Drawing.Point(181, 13);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(130, 21);
            this.cmbVariable.TabIndex = 14;
            this.cmbVariable.Text = "None";
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariable.SelectedIndexChanged += new System.EventHandler(this.cmbVariable_SelectedIndexChanged);
            // 
            // lblVariableTrigger
            // 
            this.lblVariableTrigger.AutoSize = true;
            this.lblVariableTrigger.Location = new System.Drawing.Point(113, 17);
            this.lblVariableTrigger.Name = "lblVariableTrigger";
            this.lblVariableTrigger.Size = new System.Drawing.Size(48, 13);
            this.lblVariableTrigger.TabIndex = 13;
            this.lblVariableTrigger.Text = "Variable:";
            this.lblVariableTrigger.Visible = false;
            // 
            // txtCommand
            // 
            this.txtCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCommand.Location = new System.Drawing.Point(181, 13);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(130, 20);
            this.txtCommand.TabIndex = 12;
            this.txtCommand.Visible = false;
            this.txtCommand.TextChanged += new System.EventHandler(this.txtCommand_TextChanged);
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(113, 17);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(70, 13);
            this.lblCommand.TabIndex = 11;
            this.lblCommand.Text = "/Command: /";
            this.lblCommand.Visible = false;
            // 
            // lblTriggerVal
            // 
            this.lblTriggerVal.AutoSize = true;
            this.lblTriggerVal.Location = new System.Drawing.Point(113, 17);
            this.lblTriggerVal.Name = "lblTriggerVal";
            this.lblTriggerVal.Size = new System.Drawing.Size(53, 13);
            this.lblTriggerVal.TabIndex = 10;
            this.lblTriggerVal.Text = "Projectile:";
            this.lblTriggerVal.Visible = false;
            // 
            // cmbTriggerVal
            // 
            this.cmbTriggerVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTriggerVal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTriggerVal.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTriggerVal.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTriggerVal.DrawDropdownHoverOutline = false;
            this.cmbTriggerVal.DrawFocusRectangle = false;
            this.cmbTriggerVal.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTriggerVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTriggerVal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTriggerVal.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTriggerVal.FormattingEnabled = true;
            this.cmbTriggerVal.Items.AddRange(new object[] {
            "None"});
            this.cmbTriggerVal.Location = new System.Drawing.Point(181, 13);
            this.cmbTriggerVal.Name = "cmbTriggerVal";
            this.cmbTriggerVal.Size = new System.Drawing.Size(130, 21);
            this.cmbTriggerVal.TabIndex = 9;
            this.cmbTriggerVal.Text = "None";
            this.cmbTriggerVal.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTriggerVal.Visible = false;
            // 
            // cmbTrigger
            // 
            this.cmbTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTrigger.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTrigger.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTrigger.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTrigger.DrawDropdownHoverOutline = false;
            this.cmbTrigger.DrawFocusRectangle = false;
            this.cmbTrigger.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTrigger.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTrigger.FormattingEnabled = true;
            this.cmbTrigger.Items.AddRange(new object[] {
            "Action Button",
            "Player Touch",
            "Autorun",
            "Projectile Hit"});
            this.cmbTrigger.Location = new System.Drawing.Point(6, 13);
            this.cmbTrigger.Name = "cmbTrigger";
            this.cmbTrigger.Size = new System.Drawing.Size(101, 21);
            this.cmbTrigger.TabIndex = 2;
            this.cmbTrigger.Text = "Action Button";
            this.cmbTrigger.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTrigger.SelectedIndexChanged += new System.EventHandler(this.cmbTrigger_SelectedIndexChanged);
            // 
            // grpEventConditions
            // 
            this.grpEventConditions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEventConditions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEventConditions.Controls.Add(this.btnEditConditions);
            this.grpEventConditions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEventConditions.Location = new System.Drawing.Point(21, 89);
            this.grpEventConditions.Name = "grpEventConditions";
            this.grpEventConditions.Size = new System.Drawing.Size(326, 55);
            this.grpEventConditions.TabIndex = 5;
            this.grpEventConditions.TabStop = false;
            this.grpEventConditions.Text = "Conditions";
            // 
            // btnEditConditions
            // 
            this.btnEditConditions.Location = new System.Drawing.Point(7, 20);
            this.btnEditConditions.Name = "btnEditConditions";
            this.btnEditConditions.Padding = new System.Windows.Forms.Padding(5);
            this.btnEditConditions.Size = new System.Drawing.Size(304, 23);
            this.btnEditConditions.TabIndex = 0;
            this.btnEditConditions.Text = "Spawn/Execution Conditions";
            this.btnEditConditions.Click += new System.EventHandler(this.btnEditConditions_Click);
            // 
            // grpNewCommands
            // 
            this.grpNewCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpNewCommands.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNewCommands.Controls.Add(this.lblCloseCommands);
            this.grpNewCommands.Controls.Add(this.lstCommands);
            this.grpNewCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNewCommands.Location = new System.Drawing.Point(353, 89);
            this.grpNewCommands.Name = "grpNewCommands";
            this.grpNewCommands.Size = new System.Drawing.Size(535, 620);
            this.grpNewCommands.TabIndex = 7;
            this.grpNewCommands.TabStop = false;
            this.grpNewCommands.Text = "Add Commands";
            this.grpNewCommands.Visible = false;
            // 
            // lblCloseCommands
            // 
            this.lblCloseCommands.AutoSize = true;
            this.lblCloseCommands.Location = new System.Drawing.Point(511, 16);
            this.lblCloseCommands.Name = "lblCloseCommands";
            this.lblCloseCommands.Size = new System.Drawing.Size(14, 13);
            this.lblCloseCommands.TabIndex = 1;
            this.lblCloseCommands.Text = "X";
            this.lblCloseCommands.Click += new System.EventHandler(this.lblCloseCommands_Click);
            // 
            // lstCommands
            // 
            this.lstCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstCommands.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lstCommands.Location = new System.Drawing.Point(6, 32);
            this.lstCommands.Name = "lstCommands";
            treeNode108.Name = "showtext";
            treeNode108.Tag = "1";
            treeNode108.Text = "Show Text";
            treeNode109.Name = "showoptions";
            treeNode109.Tag = "2";
            treeNode109.Text = "Show Options";
            treeNode110.Name = "inputvariable";
            treeNode110.Tag = "49";
            treeNode110.Text = "Input Variable";
            treeNode111.Name = "addchatboxtext";
            treeNode111.Tag = "3";
            treeNode111.Text = "Add Chatbox Text";
            treeNode112.Name = "dialogue";
            treeNode112.Text = "Dialogue";
            treeNode113.Name = "setvariable";
            treeNode113.Tag = "5";
            treeNode113.Text = "Set Variable";
            treeNode114.Name = "setselfswitch";
            treeNode114.Tag = "6";
            treeNode114.Text = "Set Self Switch";
            treeNode115.Name = "conditionalbranch";
            treeNode115.Tag = "7";
            treeNode115.Text = "Conditional Branch";
            treeNode116.Name = "exiteventprocess";
            treeNode116.Tag = "8";
            treeNode116.Text = "Exit Event Process";
            treeNode117.Name = "label";
            treeNode117.Tag = "9";
            treeNode117.Text = "Label";
            treeNode118.Name = "gotolabel";
            treeNode118.Tag = "10";
            treeNode118.Text = "Go To Label";
            treeNode119.Name = "startcommonevent";
            treeNode119.Tag = "11";
            treeNode119.Text = "Start Common Event";
            treeNode120.Name = "resetvariables";
            treeNode120.Tag = "72";
            treeNode120.Text = "Reset Variables in Group";
            treeNode121.Name = "logicflow";
            treeNode121.Text = "Logic Flow";
            treeNode122.Name = "restorehp";
            treeNode122.Tag = "12";
            treeNode122.Text = "Restore HP";
            treeNode123.Name = "restoremp";
            treeNode123.Tag = "13";
            treeNode123.Text = "Restore MP";
            treeNode124.Name = "levelup";
            treeNode124.Tag = "14";
            treeNode124.Text = "Level Up";
            treeNode125.Name = "giveexperience";
            treeNode125.Tag = "15";
            treeNode125.Text = "Give Experience";
            treeNode126.Name = "changelevel";
            treeNode126.Tag = "16";
            treeNode126.Text = "Change Level";
            treeNode127.Name = "changespells";
            treeNode127.Tag = "17";
            treeNode127.Text = "Change Spells";
            treeNode128.Name = "changeitems";
            treeNode128.Tag = "18";
            treeNode128.Text = "Change Items";
            treeNode129.Name = "changesprite";
            treeNode129.Tag = "19";
            treeNode129.Text = "Change Sprite";
            treeNode130.Name = "changeplayercolor";
            treeNode130.Tag = "51";
            treeNode130.Text = "Change Player Color";
            treeNode131.Name = "changeface";
            treeNode131.Tag = "20";
            treeNode131.Text = "Change Face";
            treeNode132.Name = "changegender";
            treeNode132.Tag = "21";
            treeNode132.Text = "Change Gender";
            treeNode133.Name = "setaccess";
            treeNode133.Tag = "22";
            treeNode133.Text = "Set Access";
            treeNode134.Name = "changeclass";
            treeNode134.Tag = "38";
            treeNode134.Text = "Change Class";
            treeNode135.Name = "equipitem";
            treeNode135.Tag = "47";
            treeNode135.Text = "Equip/Unequip Item";
            treeNode136.Name = "changenamecolor";
            treeNode136.Tag = "48";
            treeNode136.Text = "Change Name Color";
            treeNode137.Name = "changeplayerlabel";
            treeNode137.Tag = "50";
            treeNode137.Text = "Change Player Label";
            treeNode138.Name = "changename";
            treeNode138.Tag = "52";
            treeNode138.Text = "Change Player Name";
            treeNode139.Name = "resetstatallocations";
            treeNode139.Tag = "57";
            treeNode139.Text = "Reset Stat Point Allocations";
            treeNode140.Name = "setvehicle";
            treeNode140.Tag = "61";
            treeNode140.Text = "Set Vehicle";
            treeNode141.Name = "npcguildmanagement";
            treeNode141.Tag = "62";
            treeNode141.Text = "NPC Guild Management";
            treeNode142.Name = "addinspiration";
            treeNode142.Tag = "63";
            treeNode142.Text = "Add Inspiration";
            treeNode143.Name = "rollloot";
            treeNode143.Tag = "70";
            treeNode143.Text = "Roll Loot";
            treeNode144.Name = "unlocklabel";
            treeNode144.Tag = "71";
            treeNode144.Text = "Unlock Label";
            treeNode145.Name = "changespawn";
            treeNode145.Tag = "77";
            treeNode145.Text = "Change Respawn Point";
            treeNode146.Name = "changerecipeunlock";
            treeNode146.Tag = "78";
            treeNode146.Text = "Change Recipes";
            treeNode147.Name = "changebestiary";
            treeNode147.Tag = "79";
            treeNode147.Text = "Change Bestiary";
            treeNode148.Name = "changeweapontrack";
            treeNode148.Tag = "80";
            treeNode148.Text = "Change Weapon Track Progress";
            treeNode149.Name = "changechallenges";
            treeNode149.Tag = "81";
            treeNode149.Text = "Change Challenge Progress";
            treeNode150.Name = "changeenhancement";
            treeNode150.Tag = "86";
            treeNode150.Text = "Change Enhancement";
            treeNode151.Name = "removepermabuff";
            treeNode151.Tag = "89";
            treeNode151.Text = "Remove Permabuff";
            treeNode152.Name = "changechampsettings";
            treeNode152.Tag = "93";
            treeNode152.Text = "Change Champion Spawn Settings";
            treeNode153.Name = "castspellon";
            treeNode153.Tag = "96";
            treeNode153.Text = "Cast Spell On";
            treeNode154.Name = "playercontrol";
            treeNode154.Text = "Player Control";
            treeNode155.Name = "warpplayer";
            treeNode155.Tag = "23";
            treeNode155.Text = "Warp Player";
            treeNode156.Name = "setmoveroute";
            treeNode156.Tag = "24";
            treeNode156.Text = "Set Move Route";
            treeNode157.Name = "waitmoveroute";
            treeNode157.Tag = "25";
            treeNode157.Text = "Wait for Route Completion";
            treeNode158.Name = "holdplayer";
            treeNode158.Tag = "26";
            treeNode158.Text = "Hold Player";
            treeNode159.Name = "releaseplayer";
            treeNode159.Tag = "27";
            treeNode159.Text = "Release Player";
            treeNode160.Name = "spawnnpc";
            treeNode160.Tag = "28";
            treeNode160.Text = "Spawn NPC";
            treeNode161.Name = "despawnnpcs";
            treeNode161.Tag = "39";
            treeNode161.Text = "Despawn NPC";
            treeNode162.Name = "hideplayer";
            treeNode162.Tag = "45";
            treeNode162.Text = "Hide Player";
            treeNode163.Name = "showplayer";
            treeNode163.Tag = "46";
            treeNode163.Text = "Show Player";
            treeNode164.Name = "changemapspawngroup";
            treeNode164.Tag = "67";
            treeNode164.Text = "Change Map Spawn Group";
            treeNode165.Name = "hideevent";
            treeNode165.Tag = "94";
            treeNode165.Text = "Hide Event";
            treeNode166.Name = "showevent";
            treeNode166.Tag = "95";
            treeNode166.Text = "Show Event";
            treeNode167.Name = "movement";
            treeNode167.Text = "Movement";
            treeNode168.Name = "playanimation";
            treeNode168.Tag = "29";
            treeNode168.Text = "Play Animation";
            treeNode169.Name = "playbgm";
            treeNode169.Tag = "30";
            treeNode169.Text = "Play BGM";
            treeNode170.Name = "fadeoutbgm";
            treeNode170.Tag = "31";
            treeNode170.Text = "Fadeout BGM";
            treeNode171.Name = "playsound";
            treeNode171.Tag = "32";
            treeNode171.Text = "Play Sound";
            treeNode172.Name = "stopsounds";
            treeNode172.Tag = "33";
            treeNode172.Text = "Stop Sounds";
            treeNode173.Name = "showpicture";
            treeNode173.Tag = "43";
            treeNode173.Text = "Show Picture";
            treeNode174.Name = "hidepicture";
            treeNode174.Tag = "44";
            treeNode174.Text = "Hide Picture";
            treeNode175.Name = "flashscreen";
            treeNode175.Tag = "58";
            treeNode175.Text = "Flash Screen";
            treeNode176.Name = "fadein";
            treeNode176.Tag = "74";
            treeNode176.Text = "Fade In";
            treeNode177.Name = "fadeout";
            treeNode177.Tag = "75";
            treeNode177.Text = "Fade Out";
            treeNode178.Name = "shakescreen";
            treeNode178.Tag = "76";
            treeNode178.Text = "Shake Screen";
            treeNode179.Name = "specialeffects";
            treeNode179.Text = "Special Effects";
            treeNode180.Name = "startquest";
            treeNode180.Tag = "40";
            treeNode180.Text = "Start Quest";
            treeNode181.Name = "completequesttask";
            treeNode181.Tag = "41";
            treeNode181.Text = "Complete Quest Task";
            treeNode182.Name = "endquest";
            treeNode182.Tag = "42";
            treeNode182.Text = "End Quest";
            treeNode183.Name = "randomquest";
            treeNode183.Tag = "59";
            treeNode183.Text = "Random Quest from List";
            treeNode184.Name = "openquestboard";
            treeNode184.Tag = "60";
            treeNode184.Text = "Open Quest Board";
            treeNode185.Name = "questcontrol";
            treeNode185.Text = "Quest Control";
            treeNode186.Name = "wait";
            treeNode186.Tag = "34";
            treeNode186.Text = "Wait...";
            treeNode187.Name = "openleaderboard";
            treeNode187.Tag = "68";
            treeNode187.Text = "Open Leaderboard...";
            treeNode188.Name = "clearrecord";
            treeNode188.Tag = "69";
            treeNode188.Text = "Clear Record...";
            treeNode189.Name = "resetpermadeadnpcs";
            treeNode189.Tag = "73";
            treeNode189.Text = "Reset Permadead NPCs";
            treeNode190.Name = "resetglobalevents";
            treeNode190.Tag = "90";
            treeNode190.Text = "Reset Global Event Positions";
            treeNode191.Name = "meleesignup";
            treeNode191.Tag = "91";
            treeNode191.Text = "Signup for Open Melee";
            treeNode192.Name = "meleewithdraw";
            treeNode192.Tag = "92";
            treeNode192.Text = "Withdraw from Open Melee";
            treeNode193.Name = "etc";
            treeNode193.Text = "Etc";
            treeNode194.Name = "openbank";
            treeNode194.Tag = "35";
            treeNode194.Text = "Open Bank";
            treeNode195.Name = "openshop";
            treeNode195.Tag = "36";
            treeNode195.Text = "Open Shop";
            treeNode196.Name = "opencraftingstation";
            treeNode196.Tag = "37";
            treeNode196.Text = "Open Crafting Station";
            treeNode197.Name = "closecraftingtable";
            treeNode197.Tag = "97";
            treeNode197.Text = "Close Crafting Table";
            treeNode198.Name = "opendeconstructor";
            treeNode198.Tag = "85";
            treeNode198.Text = "Open Deconstructor";
            treeNode199.Name = "openenhancementwindow";
            treeNode199.Tag = "87";
            treeNode199.Text = "Open Enhancement Window";
            treeNode200.Name = "openupgradestation";
            treeNode200.Tag = "88";
            treeNode200.Text = "Open Upgrade Station";
            treeNode201.Name = "shopandbank";
            treeNode201.Text = "Shop and Bank";
            treeNode202.Name = "createguild";
            treeNode202.Tag = "53";
            treeNode202.Text = "Create Guild";
            treeNode203.Name = "disbandguild";
            treeNode203.Tag = "54";
            treeNode203.Text = "Disband Guild";
            treeNode204.Name = "openguildbank";
            treeNode204.Tag = "55";
            treeNode204.Text = "Open Guild Bank";
            treeNode205.Name = "setguildbankslots";
            treeNode205.Tag = "56";
            treeNode205.Text = "Set Guild Bank Slots Count";
            treeNode206.Name = "guilds";
            treeNode206.Text = "Guilds";
            treeNode207.Name = "starttimer";
            treeNode207.Tag = "64";
            treeNode207.Text = "Start Timer";
            treeNode208.Name = "modifytimer";
            treeNode208.Tag = "65";
            treeNode208.Text = "Modify Timer";
            treeNode209.Name = "stoptimer";
            treeNode209.Tag = "66";
            treeNode209.Text = "Stop Timer";
            treeNode210.Name = "timers";
            treeNode210.Text = "Timers";
            treeNode211.Name = "changedungeon";
            treeNode211.Tag = "82";
            treeNode211.Text = "Change Dungeon";
            treeNode212.Name = "obtaintreasuregnome";
            treeNode212.Tag = "83";
            treeNode212.Text = "Obtain Treasure Gnome";
            treeNode213.Name = "rolldungeonloot";
            treeNode213.Tag = "84";
            treeNode213.Text = "Roll Dungeon Loot";
            treeNode214.Name = "dungeons";
            treeNode214.Text = "Dungeons";
            this.lstCommands.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode112,
            treeNode121,
            treeNode154,
            treeNode167,
            treeNode179,
            treeNode185,
            treeNode193,
            treeNode201,
            treeNode206,
            treeNode210,
            treeNode214});
            this.lstCommands.Size = new System.Drawing.Size(519, 582);
            this.lstCommands.TabIndex = 2;
            this.lstCommands.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.lstCommands_NodeMouseDoubleClick);
            // 
            // grpEventCommands
            // 
            this.grpEventCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEventCommands.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEventCommands.Controls.Add(this.lstEventCommands);
            this.grpEventCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEventCommands.Location = new System.Drawing.Point(353, 89);
            this.grpEventCommands.Name = "grpEventCommands";
            this.grpEventCommands.Size = new System.Drawing.Size(531, 620);
            this.grpEventCommands.TabIndex = 6;
            this.grpEventCommands.TabStop = false;
            this.grpEventCommands.Text = "Commands";
            // 
            // lstEventCommands
            // 
            this.lstEventCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstEventCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstEventCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstEventCommands.FormattingEnabled = true;
            this.lstEventCommands.HorizontalScrollbar = true;
            this.lstEventCommands.Items.AddRange(new object[] {
            "@>"});
            this.lstEventCommands.Location = new System.Drawing.Point(6, 19);
            this.lstEventCommands.Name = "lstEventCommands";
            this.lstEventCommands.Size = new System.Drawing.Size(519, 587);
            this.lstEventCommands.TabIndex = 0;
            this.lstEventCommands.SelectedIndexChanged += new System.EventHandler(this.lstEventCommands_SelectedIndexChanged);
            this.lstEventCommands.DoubleClick += new System.EventHandler(this.lstEventCommands_DoubleClick);
            this.lstEventCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstEventCommands_KeyDown);
            this.lstEventCommands.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstEventCommands_Click);
            // 
            // grpCreateCommands
            // 
            this.grpCreateCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCreateCommands.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCreateCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCreateCommands.Location = new System.Drawing.Point(353, 89);
            this.grpCreateCommands.Name = "grpCreateCommands";
            this.grpCreateCommands.Size = new System.Drawing.Size(525, 614);
            this.grpCreateCommands.TabIndex = 8;
            this.grpCreateCommands.TabStop = false;
            this.grpCreateCommands.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(631, 719);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(93, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(730, 719);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(93, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // commandMenu
            // 
            this.commandMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.commandMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.commandMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInsert,
            this.btnEdit,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnDelete});
            this.commandMenu.Name = "commandMenu";
            this.commandMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.commandMenu.Size = new System.Drawing.Size(108, 136);
            // 
            // btnInsert
            // 
            this.btnInsert.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(107, 22);
            this.btnInsert.Text = "Insert";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(107, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCut
            // 
            this.btnCut.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(107, 22);
            this.btnCut.Text = "Cut";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(107, 22);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(107, 22);
            this.btnPaste.Text = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grpPageOptions
            // 
            this.grpPageOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPageOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPageOptions.Controls.Add(this.btnClearPage);
            this.grpPageOptions.Controls.Add(this.btnDeletePage);
            this.grpPageOptions.Controls.Add(this.btnPastePage);
            this.grpPageOptions.Controls.Add(this.btnCopyPage);
            this.grpPageOptions.Controls.Add(this.btnNewPage);
            this.grpPageOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPageOptions.Location = new System.Drawing.Point(313, 5);
            this.grpPageOptions.Name = "grpPageOptions";
            this.grpPageOptions.Size = new System.Drawing.Size(510, 50);
            this.grpPageOptions.TabIndex = 13;
            this.grpPageOptions.TabStop = false;
            this.grpPageOptions.Text = "Page Options";
            // 
            // btnClearPage
            // 
            this.btnClearPage.Location = new System.Drawing.Point(402, 16);
            this.btnClearPage.Name = "btnClearPage";
            this.btnClearPage.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearPage.Size = new System.Drawing.Size(93, 30);
            this.btnClearPage.TabIndex = 17;
            this.btnClearPage.Text = "Clear Page";
            this.btnClearPage.Click += new System.EventHandler(this.btnClearPage_Click);
            // 
            // btnDeletePage
            // 
            this.btnDeletePage.Enabled = false;
            this.btnDeletePage.Location = new System.Drawing.Point(303, 16);
            this.btnDeletePage.Name = "btnDeletePage";
            this.btnDeletePage.Padding = new System.Windows.Forms.Padding(5);
            this.btnDeletePage.Size = new System.Drawing.Size(93, 30);
            this.btnDeletePage.TabIndex = 16;
            this.btnDeletePage.Text = "Delete Page";
            this.btnDeletePage.Click += new System.EventHandler(this.btnDeletePage_Click);
            // 
            // btnPastePage
            // 
            this.btnPastePage.Location = new System.Drawing.Point(204, 16);
            this.btnPastePage.Name = "btnPastePage";
            this.btnPastePage.Padding = new System.Windows.Forms.Padding(5);
            this.btnPastePage.Size = new System.Drawing.Size(93, 30);
            this.btnPastePage.TabIndex = 15;
            this.btnPastePage.Text = "Paste Page";
            this.btnPastePage.Click += new System.EventHandler(this.btnPastePage_Click);
            // 
            // btnCopyPage
            // 
            this.btnCopyPage.Location = new System.Drawing.Point(105, 16);
            this.btnCopyPage.Name = "btnCopyPage";
            this.btnCopyPage.Padding = new System.Windows.Forms.Padding(5);
            this.btnCopyPage.Size = new System.Drawing.Size(93, 30);
            this.btnCopyPage.TabIndex = 14;
            this.btnCopyPage.Text = "Copy Page";
            this.btnCopyPage.Click += new System.EventHandler(this.btnCopyPage_Click);
            // 
            // btnNewPage
            // 
            this.btnNewPage.Location = new System.Drawing.Point(6, 16);
            this.btnNewPage.Name = "btnNewPage";
            this.btnNewPage.Padding = new System.Windows.Forms.Padding(5);
            this.btnNewPage.Size = new System.Drawing.Size(93, 30);
            this.btnNewPage.TabIndex = 13;
            this.btnNewPage.Text = "New Page";
            this.btnNewPage.Click += new System.EventHandler(this.btnNewPage_Click);
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.chkIsGlobal);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtEventname);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(12, 5);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(295, 49);
            this.grpGeneral.TabIndex = 18;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // chkIsGlobal
            // 
            this.chkIsGlobal.AutoSize = true;
            this.chkIsGlobal.Location = new System.Drawing.Point(202, 22);
            this.chkIsGlobal.Name = "chkIsGlobal";
            this.chkIsGlobal.Size = new System.Drawing.Size(87, 17);
            this.chkIsGlobal.TabIndex = 3;
            this.chkIsGlobal.Text = "Global Event";
            this.chkIsGlobal.CheckedChanged += new System.EventHandler(this.chkIsGlobal_CheckedChanged);
            // 
            // pnlTabsContainer
            // 
            this.pnlTabsContainer.Controls.Add(this.pnlTabs);
            this.pnlTabsContainer.Location = new System.Drawing.Point(12, 61);
            this.pnlTabsContainer.Name = "pnlTabsContainer";
            this.pnlTabsContainer.Size = new System.Drawing.Size(811, 22);
            this.pnlTabsContainer.TabIndex = 22;
            // 
            // pnlTabs
            // 
            this.pnlTabs.AutoSize = true;
            this.pnlTabs.Location = new System.Drawing.Point(0, 0);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(811, 22);
            this.pnlTabs.TabIndex = 23;
            // 
            // btnTabsRight
            // 
            this.btnTabsRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTabsRight.Location = new System.Drawing.Point(814, 61);
            this.btnTabsRight.Name = "btnTabsRight";
            this.btnTabsRight.Padding = new System.Windows.Forms.Padding(5);
            this.btnTabsRight.Size = new System.Drawing.Size(50, 23);
            this.btnTabsRight.TabIndex = 1;
            this.btnTabsRight.Text = ">";
            this.btnTabsRight.Click += new System.EventHandler(this.btnTabsRight_Click);
            // 
            // btnTabsLeft
            // 
            this.btnTabsLeft.Location = new System.Drawing.Point(12, 61);
            this.btnTabsLeft.Name = "btnTabsLeft";
            this.btnTabsLeft.Padding = new System.Windows.Forms.Padding(5);
            this.btnTabsLeft.Size = new System.Drawing.Size(50, 23);
            this.btnTabsLeft.TabIndex = 0;
            this.btnTabsLeft.Text = "<";
            this.btnTabsLeft.Click += new System.EventHandler(this.btnTabsLeft_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grpTriggers);
            this.panel1.Location = new System.Drawing.Point(12, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 630);
            this.panel1.TabIndex = 23;
            // 
            // chkParallelRun
            // 
            this.chkParallelRun.AutoSize = true;
            this.chkParallelRun.Location = new System.Drawing.Point(6, 43);
            this.chkParallelRun.Name = "chkParallelRun";
            this.chkParallelRun.Size = new System.Drawing.Size(84, 17);
            this.chkParallelRun.TabIndex = 99;
            this.chkParallelRun.Text = "Parallel run?";
            this.chkParallelRun.CheckedChanged += new System.EventHandler(this.chkParallelRun_CheckedChanged);
            // 
            // FrmEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(900, 761);
            this.Controls.Add(this.grpNewCommands);
            this.Controls.Add(this.grpEventCommands);
            this.Controls.Add(this.btnTabsRight);
            this.Controls.Add(this.btnTabsLeft);
            this.Controls.Add(this.grpEntityOptions);
            this.Controls.Add(this.grpEventConditions);
            this.Controls.Add(this.grpPageOptions);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlTabsContainer);
            this.Controls.Add(this.grpCreateCommands);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Event Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEvent_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmEvent_FormClosed);
            this.Load += new System.EventHandler(this.frmEvent_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmEvent_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEvent_KeyDown);
            this.grpEntityOptions.ResumeLayout(false);
            this.grpQuestAnimation.ResumeLayout(false);
            this.grpQuestAnimation.PerformLayout();
            this.grpExtra.ResumeLayout(false);
            this.grpExtra.PerformLayout();
            this.grpInspector.ResumeLayout(false);
            this.grpInspector.PerformLayout();
            this.grpPreview.ResumeLayout(false);
            this.grpPreview.PerformLayout();
            this.grpMovement.ResumeLayout(false);
            this.grpMovement.PerformLayout();
            this.grpTriggers.ResumeLayout(false);
            this.grpTriggers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecordNumber)).EndInit();
            this.grpEventConditions.ResumeLayout(false);
            this.grpNewCommands.ResumeLayout(false);
            this.grpNewCommands.PerformLayout();
            this.grpEventCommands.ResumeLayout(false);
            this.commandMenu.ResumeLayout(false);
            this.grpPageOptions.ResumeLayout(false);
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.pnlTabsContainer.ResumeLayout(false);
            this.pnlTabsContainer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblName;
        private DarkTextBox txtEventname;
        private DarkGroupBox grpEventCommands;
        private ListBox lstEventCommands;
        private DarkGroupBox grpEventConditions;
        private DarkGroupBox grpExtra;
        private DarkGroupBox grpMovement;
        private DarkGroupBox grpInspector;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private DarkComboBox cmbEventFreq;
        private DarkComboBox cmbEventSpeed;
        private Label lblFreq;
        private Label lblSpeed;
        private DarkButton btnSetRoute;
        private Label lblType;
        private DarkComboBox cmbMoveType;
        private DarkComboBox cmbTrigger;
        private DarkComboBox cmbLayering;
        private DarkCheckBox chkWalkThrough;
        private DarkGroupBox grpNewCommands;
        private DarkGroupBox grpCreateCommands;
        private ContextMenuStrip commandMenu;
        private ToolStripMenuItem btnInsert;
        private ToolStripMenuItem btnEdit;
        private ToolStripMenuItem btnDelete;
        private DarkCheckBox chkHideName;
        private DarkCheckBox chkDisableInspector;
        private DarkComboBox cmbPreviewFace;
        private Label lblFace;
        private DarkTextBox txtDesc;
        private DarkGroupBox grpPreview;
        private DarkGroupBox grpPageOptions;
        private DarkButton btnNewPage;
        private DarkButton btnCopyPage;
        private DarkButton btnPastePage;
        private DarkButton btnDeletePage;
        private DarkButton btnClearPage;
        private DarkGroupBox grpEntityOptions;
        private Label lblInspectorDesc;
        private Panel pnlPreview;
        private Panel pnlFacePreview;
        private DarkCheckBox chkWalkingAnimation;
        private DarkCheckBox chkDirectionFix;
        private DarkGroupBox grpGeneral;
        private Label lblAnimation;
        private DarkComboBox cmbAnimation;
        private DarkCheckBox chkIsGlobal;
        private Label lblLayer;
        private Label lblCloseCommands;
        private DarkCheckBox chkInteractionFreeze;
        private Label lblTriggerVal;
        private DarkComboBox cmbTriggerVal;
        private Panel pnlTabsContainer;
        private DarkGroupBox grpTriggers;
        private Panel panel1;
        private DarkButton btnTabsLeft;
        private DarkButton btnTabsRight;
        private Panel pnlTabs;
        private TreeView lstCommands;
        private DarkButton btnEditConditions;
        private DarkTextBox txtCommand;
        private Label lblCommand;
        private ToolStripMenuItem btnCut;
        private ToolStripMenuItem btnCopy;
        private ToolStripMenuItem btnPaste;
        private DarkComboBox cmbVariable;
        private Label lblVariableTrigger;
        private DarkGroupBox grpQuestAnimation;
        private DarkComboBox cmbQuestAnimation;
        private Label lblQuestAnimation;
        private DarkComboBox cmbQuest;
        private Label lblClass;
        private DarkComboBox cmbClass;
        private Label lblRecordNumber;
        private DarkNumericUpDown nudRecordNumber;
        private Label lblRecordItem;
        private DarkComboBox cmbRecordItem;
        private DarkCheckBox chkKillAfterOne;
        private DarkComboBox cmbEnableMoveVar;
        private Label lblEnableMoveVar;
        private DarkCheckBox chkIdleAnim;
        private DarkCheckBox chkParallelRun;
    }
}