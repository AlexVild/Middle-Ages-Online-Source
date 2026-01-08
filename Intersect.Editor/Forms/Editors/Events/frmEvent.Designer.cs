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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Show Text");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Show Options");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Input Variable");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Add Chatbox Text");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Dialogue", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Set Variable");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Set Self Switch");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Conditional Branch");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Exit Event Process");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Label");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Go To Label");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Start Common Event");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Reset Variables in Group");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Logic Flow", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Restore HP");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Restore MP");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Level Up");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Give Experience");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Change Level");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Change Spells");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Change Items");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Change Sprite");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Change Player Color");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Change Face");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Change Gender");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Set Access");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Change Class");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Equip/Unequip Item");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Change Name Color");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Change Player Label");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Change Player Name");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Reset Stat Point Allocations");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Set Vehicle");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("NPC Guild Management");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Add Inspiration");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Roll Loot");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Unlock Label");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Change Respawn Point");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("Change Recipes");
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Change Bestiary");
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("Change Weapon Track Progress");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("Change Challenge Progress");
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("Change Enhancement");
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("Remove Permabuff");
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("Change Champion Spawn Settings");
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("Cast Spell On");
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("Player Control", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36,
            treeNode37,
            treeNode38,
            treeNode39,
            treeNode40,
            treeNode41,
            treeNode42,
            treeNode43,
            treeNode44,
            treeNode45,
            treeNode46});
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("Warp Player");
            System.Windows.Forms.TreeNode treeNode49 = new System.Windows.Forms.TreeNode("Set Move Route");
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("Wait for Route Completion");
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("Hold Player");
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("Release Player");
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("Spawn NPC");
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("Despawn NPC");
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Hide Player");
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("Show Player");
            System.Windows.Forms.TreeNode treeNode57 = new System.Windows.Forms.TreeNode("Change Map Spawn Group");
            System.Windows.Forms.TreeNode treeNode58 = new System.Windows.Forms.TreeNode("Hide Event");
            System.Windows.Forms.TreeNode treeNode59 = new System.Windows.Forms.TreeNode("Show Event");
            System.Windows.Forms.TreeNode treeNode60 = new System.Windows.Forms.TreeNode("Hold Player (Cross-map)");
            System.Windows.Forms.TreeNode treeNode61 = new System.Windows.Forms.TreeNode("Movement", new System.Windows.Forms.TreeNode[] {
            treeNode48,
            treeNode49,
            treeNode50,
            treeNode51,
            treeNode52,
            treeNode53,
            treeNode54,
            treeNode55,
            treeNode56,
            treeNode57,
            treeNode58,
            treeNode59,
            treeNode60});
            System.Windows.Forms.TreeNode treeNode62 = new System.Windows.Forms.TreeNode("Play Animation");
            System.Windows.Forms.TreeNode treeNode63 = new System.Windows.Forms.TreeNode("Play BGM");
            System.Windows.Forms.TreeNode treeNode64 = new System.Windows.Forms.TreeNode("Fadeout BGM");
            System.Windows.Forms.TreeNode treeNode65 = new System.Windows.Forms.TreeNode("Play Sound");
            System.Windows.Forms.TreeNode treeNode66 = new System.Windows.Forms.TreeNode("Stop Sounds");
            System.Windows.Forms.TreeNode treeNode67 = new System.Windows.Forms.TreeNode("Show Picture");
            System.Windows.Forms.TreeNode treeNode68 = new System.Windows.Forms.TreeNode("Hide Picture");
            System.Windows.Forms.TreeNode treeNode69 = new System.Windows.Forms.TreeNode("Flash Screen");
            System.Windows.Forms.TreeNode treeNode70 = new System.Windows.Forms.TreeNode("Fade In");
            System.Windows.Forms.TreeNode treeNode71 = new System.Windows.Forms.TreeNode("Fade Out");
            System.Windows.Forms.TreeNode treeNode72 = new System.Windows.Forms.TreeNode("Shake Screen");
            System.Windows.Forms.TreeNode treeNode73 = new System.Windows.Forms.TreeNode("Special Effects", new System.Windows.Forms.TreeNode[] {
            treeNode62,
            treeNode63,
            treeNode64,
            treeNode65,
            treeNode66,
            treeNode67,
            treeNode68,
            treeNode69,
            treeNode70,
            treeNode71,
            treeNode72});
            System.Windows.Forms.TreeNode treeNode74 = new System.Windows.Forms.TreeNode("Start Quest");
            System.Windows.Forms.TreeNode treeNode75 = new System.Windows.Forms.TreeNode("Complete Quest Task");
            System.Windows.Forms.TreeNode treeNode76 = new System.Windows.Forms.TreeNode("End Quest");
            System.Windows.Forms.TreeNode treeNode77 = new System.Windows.Forms.TreeNode("Random Quest from List");
            System.Windows.Forms.TreeNode treeNode78 = new System.Windows.Forms.TreeNode("Open Quest Board");
            System.Windows.Forms.TreeNode treeNode79 = new System.Windows.Forms.TreeNode("Quest Control", new System.Windows.Forms.TreeNode[] {
            treeNode74,
            treeNode75,
            treeNode76,
            treeNode77,
            treeNode78});
            System.Windows.Forms.TreeNode treeNode80 = new System.Windows.Forms.TreeNode("Wait...");
            System.Windows.Forms.TreeNode treeNode81 = new System.Windows.Forms.TreeNode("Open Leaderboard...");
            System.Windows.Forms.TreeNode treeNode82 = new System.Windows.Forms.TreeNode("Clear Record...");
            System.Windows.Forms.TreeNode treeNode83 = new System.Windows.Forms.TreeNode("Reset Permadead NPCs");
            System.Windows.Forms.TreeNode treeNode84 = new System.Windows.Forms.TreeNode("Reset Global Event Positions");
            System.Windows.Forms.TreeNode treeNode85 = new System.Windows.Forms.TreeNode("Signup for Open Melee");
            System.Windows.Forms.TreeNode treeNode86 = new System.Windows.Forms.TreeNode("Withdraw from Open Melee");
            System.Windows.Forms.TreeNode treeNode87 = new System.Windows.Forms.TreeNode("Force NPC Exhaustion");
            System.Windows.Forms.TreeNode treeNode88 = new System.Windows.Forms.TreeNode("Etc", new System.Windows.Forms.TreeNode[] {
            treeNode80,
            treeNode81,
            treeNode82,
            treeNode83,
            treeNode84,
            treeNode85,
            treeNode86,
            treeNode87});
            System.Windows.Forms.TreeNode treeNode89 = new System.Windows.Forms.TreeNode("Open Bank");
            System.Windows.Forms.TreeNode treeNode90 = new System.Windows.Forms.TreeNode("Open Shop");
            System.Windows.Forms.TreeNode treeNode91 = new System.Windows.Forms.TreeNode("Open Crafting Station");
            System.Windows.Forms.TreeNode treeNode92 = new System.Windows.Forms.TreeNode("Close Crafting Table");
            System.Windows.Forms.TreeNode treeNode93 = new System.Windows.Forms.TreeNode("Open Deconstructor");
            System.Windows.Forms.TreeNode treeNode94 = new System.Windows.Forms.TreeNode("Open Enhancement Window");
            System.Windows.Forms.TreeNode treeNode95 = new System.Windows.Forms.TreeNode("Open Upgrade Station");
            System.Windows.Forms.TreeNode treeNode96 = new System.Windows.Forms.TreeNode("Shop and Bank", new System.Windows.Forms.TreeNode[] {
            treeNode89,
            treeNode90,
            treeNode91,
            treeNode92,
            treeNode93,
            treeNode94,
            treeNode95});
            System.Windows.Forms.TreeNode treeNode97 = new System.Windows.Forms.TreeNode("Create Guild");
            System.Windows.Forms.TreeNode treeNode98 = new System.Windows.Forms.TreeNode("Disband Guild");
            System.Windows.Forms.TreeNode treeNode99 = new System.Windows.Forms.TreeNode("Open Guild Bank");
            System.Windows.Forms.TreeNode treeNode100 = new System.Windows.Forms.TreeNode("Set Guild Bank Slots Count");
            System.Windows.Forms.TreeNode treeNode101 = new System.Windows.Forms.TreeNode("Guilds", new System.Windows.Forms.TreeNode[] {
            treeNode97,
            treeNode98,
            treeNode99,
            treeNode100});
            System.Windows.Forms.TreeNode treeNode102 = new System.Windows.Forms.TreeNode("Start Timer");
            System.Windows.Forms.TreeNode treeNode103 = new System.Windows.Forms.TreeNode("Modify Timer");
            System.Windows.Forms.TreeNode treeNode104 = new System.Windows.Forms.TreeNode("Stop Timer");
            System.Windows.Forms.TreeNode treeNode105 = new System.Windows.Forms.TreeNode("Timers", new System.Windows.Forms.TreeNode[] {
            treeNode102,
            treeNode103,
            treeNode104});
            System.Windows.Forms.TreeNode treeNode106 = new System.Windows.Forms.TreeNode("Change Dungeon");
            System.Windows.Forms.TreeNode treeNode107 = new System.Windows.Forms.TreeNode("Obtain Treasure Gnome");
            System.Windows.Forms.TreeNode treeNode108 = new System.Windows.Forms.TreeNode("Roll Dungeon Loot");
            System.Windows.Forms.TreeNode treeNode109 = new System.Windows.Forms.TreeNode("Dungeons", new System.Windows.Forms.TreeNode[] {
            treeNode106,
            treeNode107,
            treeNode108});
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
            this.chkIgnoreBlocked = new DarkUI.Controls.DarkCheckBox();
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
            this.chkParallelRun = new DarkUI.Controls.DarkCheckBox();
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
            this.lblCommandFilter = new System.Windows.Forms.Label();
            this.lblClearFilter = new System.Windows.Forms.Label();
            this.txtFilter = new DarkUI.Controls.DarkTextBox();
            this.lblCloseCommands = new System.Windows.Forms.Label();
            this.lstFilteredCommands = new System.Windows.Forms.ListBox();
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
            this.lblName.Location = new System.Drawing.Point(9, 34);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(55, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtEventname
            // 
            this.txtEventname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtEventname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEventname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtEventname.Location = new System.Drawing.Point(72, 29);
            this.txtEventname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEventname.Name = "txtEventname";
            this.txtEventname.Size = new System.Drawing.Size(185, 26);
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
            this.grpEntityOptions.Location = new System.Drawing.Point(32, 231);
            this.grpEntityOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEntityOptions.Name = "grpEntityOptions";
            this.grpEntityOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEntityOptions.Size = new System.Drawing.Size(489, 752);
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
            this.grpQuestAnimation.Location = new System.Drawing.Point(10, 674);
            this.grpQuestAnimation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpQuestAnimation.Name = "grpQuestAnimation";
            this.grpQuestAnimation.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpQuestAnimation.Size = new System.Drawing.Size(476, 68);
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
            this.cmbQuestAnimation.Location = new System.Drawing.Point(272, 20);
            this.cmbQuestAnimation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbQuestAnimation.Name = "cmbQuestAnimation";
            this.cmbQuestAnimation.Size = new System.Drawing.Size(193, 27);
            this.cmbQuestAnimation.TabIndex = 14;
            this.cmbQuestAnimation.Text = "None";
            this.cmbQuestAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbQuestAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbQuestAnimation_SelectedIndexChanged);
            // 
            // lblQuestAnimation
            // 
            this.lblQuestAnimation.AutoSize = true;
            this.lblQuestAnimation.Location = new System.Drawing.Point(183, 25);
            this.lblQuestAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestAnimation.Name = "lblQuestAnimation";
            this.lblQuestAnimation.Size = new System.Drawing.Size(84, 20);
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
            this.cmbQuest.Location = new System.Drawing.Point(9, 20);
            this.cmbQuest.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbQuest.Name = "cmbQuest";
            this.cmbQuest.Size = new System.Drawing.Size(150, 27);
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
            this.grpExtra.Location = new System.Drawing.Point(14, 568);
            this.grpExtra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpExtra.Name = "grpExtra";
            this.grpExtra.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpExtra.Size = new System.Drawing.Size(472, 98);
            this.grpExtra.TabIndex = 9;
            this.grpExtra.TabStop = false;
            this.grpExtra.Text = "Extra";
            // 
            // chkIdleAnim
            // 
            this.chkIdleAnim.AutoSize = true;
            this.chkIdleAnim.Location = new System.Drawing.Point(321, 63);
            this.chkIdleAnim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkIdleAnim.Name = "chkIdleAnim";
            this.chkIdleAnim.Size = new System.Drawing.Size(136, 24);
            this.chkIdleAnim.TabIndex = 8;
            this.chkIdleAnim.Text = "Idle Animation";
            this.chkIdleAnim.CheckedChanged += new System.EventHandler(this.chkIdleAnim_CheckedChanged);
            // 
            // chkKillAfterOne
            // 
            this.chkKillAfterOne.AutoSize = true;
            this.chkKillAfterOne.Location = new System.Drawing.Point(188, 63);
            this.chkKillAfterOne.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkKillAfterOne.Name = "chkKillAfterOne";
            this.chkKillAfterOne.Size = new System.Drawing.Size(112, 24);
            this.chkKillAfterOne.TabIndex = 7;
            this.chkKillAfterOne.Text = "One Player";
            this.chkKillAfterOne.CheckedChanged += new System.EventHandler(this.chkKillAfterOne_CheckedChanged);
            // 
            // chkInteractionFreeze
            // 
            this.chkInteractionFreeze.AutoSize = true;
            this.chkInteractionFreeze.Location = new System.Drawing.Point(9, 63);
            this.chkInteractionFreeze.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkInteractionFreeze.Name = "chkInteractionFreeze";
            this.chkInteractionFreeze.Size = new System.Drawing.Size(165, 24);
            this.chkInteractionFreeze.TabIndex = 6;
            this.chkInteractionFreeze.Text = "Interaction Freeze";
            this.chkInteractionFreeze.CheckedChanged += new System.EventHandler(this.chkInteractionFreeze_CheckedChanged);
            // 
            // chkWalkingAnimation
            // 
            this.chkWalkingAnimation.AutoSize = true;
            this.chkWalkingAnimation.Location = new System.Drawing.Point(321, 29);
            this.chkWalkingAnimation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkWalkingAnimation.Name = "chkWalkingAnimation";
            this.chkWalkingAnimation.Size = new System.Drawing.Size(131, 24);
            this.chkWalkingAnimation.TabIndex = 5;
            this.chkWalkingAnimation.Text = "Walking Anim";
            this.chkWalkingAnimation.CheckedChanged += new System.EventHandler(this.chkWalkingAnimation_CheckedChanged);
            // 
            // chkDirectionFix
            // 
            this.chkDirectionFix.AutoSize = true;
            this.chkDirectionFix.Location = new System.Drawing.Point(234, 29);
            this.chkDirectionFix.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDirectionFix.Name = "chkDirectionFix";
            this.chkDirectionFix.Size = new System.Drawing.Size(79, 24);
            this.chkDirectionFix.TabIndex = 4;
            this.chkDirectionFix.Text = "Dir Fix";
            this.chkDirectionFix.CheckedChanged += new System.EventHandler(this.chkDirectionFix_CheckedChanged);
            // 
            // chkHideName
            // 
            this.chkHideName.AutoSize = true;
            this.chkHideName.Location = new System.Drawing.Point(112, 29);
            this.chkHideName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkHideName.Name = "chkHideName";
            this.chkHideName.Size = new System.Drawing.Size(114, 24);
            this.chkHideName.TabIndex = 3;
            this.chkHideName.Text = "Hide Name";
            this.chkHideName.CheckedChanged += new System.EventHandler(this.chkHideName_CheckedChanged);
            // 
            // chkWalkThrough
            // 
            this.chkWalkThrough.AutoSize = true;
            this.chkWalkThrough.Location = new System.Drawing.Point(9, 29);
            this.chkWalkThrough.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkWalkThrough.Name = "chkWalkThrough";
            this.chkWalkThrough.Size = new System.Drawing.Size(100, 24);
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
            this.grpInspector.Location = new System.Drawing.Point(9, 382);
            this.grpInspector.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpInspector.Name = "grpInspector";
            this.grpInspector.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpInspector.Size = new System.Drawing.Size(474, 180);
            this.grpInspector.TabIndex = 7;
            this.grpInspector.TabStop = false;
            this.grpInspector.Text = "Entity Inspector Options";
            // 
            // pnlFacePreview
            // 
            this.pnlFacePreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlFacePreview.Location = new System.Drawing.Point(14, 71);
            this.pnlFacePreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlFacePreview.Name = "pnlFacePreview";
            this.pnlFacePreview.Size = new System.Drawing.Size(96, 98);
            this.pnlFacePreview.TabIndex = 12;
            // 
            // lblInspectorDesc
            // 
            this.lblInspectorDesc.Location = new System.Drawing.Point(118, 65);
            this.lblInspectorDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInspectorDesc.Name = "lblInspectorDesc";
            this.lblInspectorDesc.Size = new System.Drawing.Size(168, 29);
            this.lblInspectorDesc.TabIndex = 11;
            this.lblInspectorDesc.Text = "Inspector Description:";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDesc.Location = new System.Drawing.Point(118, 94);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(346, 76);
            this.txtDesc.TabIndex = 0;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // chkDisableInspector
            // 
            this.chkDisableInspector.Location = new System.Drawing.Point(306, 23);
            this.chkDisableInspector.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDisableInspector.Name = "chkDisableInspector";
            this.chkDisableInspector.Size = new System.Drawing.Size(160, 32);
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
            this.cmbPreviewFace.Location = new System.Drawing.Point(69, 23);
            this.cmbPreviewFace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPreviewFace.Name = "cmbPreviewFace";
            this.cmbPreviewFace.Size = new System.Drawing.Size(169, 27);
            this.cmbPreviewFace.TabIndex = 10;
            this.cmbPreviewFace.Text = null;
            this.cmbPreviewFace.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbPreviewFace.SelectedIndexChanged += new System.EventHandler(this.cmbPreviewFace_SelectedIndexChanged);
            // 
            // lblFace
            // 
            this.lblFace.AutoSize = true;
            this.lblFace.Location = new System.Drawing.Point(9, 28);
            this.lblFace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFace.Name = "lblFace";
            this.lblFace.Size = new System.Drawing.Size(49, 20);
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
            this.grpPreview.Location = new System.Drawing.Point(9, 20);
            this.grpPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPreview.Size = new System.Drawing.Size(240, 251);
            this.grpPreview.TabIndex = 10;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "Preview";
            // 
            // lblAnimation
            // 
            this.lblAnimation.AutoSize = true;
            this.lblAnimation.Location = new System.Drawing.Point(6, 178);
            this.lblAnimation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAnimation.Name = "lblAnimation";
            this.lblAnimation.Size = new System.Drawing.Size(84, 20);
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
            this.cmbAnimation.Location = new System.Drawing.Point(30, 203);
            this.cmbAnimation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(186, 27);
            this.cmbAnimation.TabIndex = 1;
            this.cmbAnimation.Text = null;
            this.cmbAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAnimation_SelectedIndexChanged);
            // 
            // pnlPreview
            // 
            this.pnlPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Location = new System.Drawing.Point(50, 22);
            this.pnlPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(143, 147);
            this.pnlPreview.TabIndex = 0;
            this.pnlPreview.DoubleClick += new System.EventHandler(this.pnlPreview_DoubleClick);
            // 
            // grpMovement
            // 
            this.grpMovement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpMovement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpMovement.Controls.Add(this.chkIgnoreBlocked);
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
            this.grpMovement.Location = new System.Drawing.Point(254, 20);
            this.grpMovement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpMovement.Name = "grpMovement";
            this.grpMovement.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpMovement.Size = new System.Drawing.Size(231, 357);
            this.grpMovement.TabIndex = 8;
            this.grpMovement.TabStop = false;
            this.grpMovement.Text = "Movement";
            // 
            // chkIgnoreBlocked
            // 
            this.chkIgnoreBlocked.AutoSize = true;
            this.chkIgnoreBlocked.Location = new System.Drawing.Point(14, 320);
            this.chkIgnoreBlocked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkIgnoreBlocked.Name = "chkIgnoreBlocked";
            this.chkIgnoreBlocked.Size = new System.Drawing.Size(142, 24);
            this.chkIgnoreBlocked.TabIndex = 23;
            this.chkIgnoreBlocked.Text = "Ignore Blocked";
            this.chkIgnoreBlocked.CheckedChanged += new System.EventHandler(this.chkIgnoreBlocked_CheckedChanged);
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
            this.cmbEnableMoveVar.Location = new System.Drawing.Point(9, 280);
            this.cmbEnableMoveVar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEnableMoveVar.Name = "cmbEnableMoveVar";
            this.cmbEnableMoveVar.Size = new System.Drawing.Size(214, 27);
            this.cmbEnableMoveVar.TabIndex = 9;
            this.cmbEnableMoveVar.Text = "None";
            this.cmbEnableMoveVar.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEnableMoveVar.SelectedIndexChanged += new System.EventHandler(this.cmbDisableMoveVar_SelectedIndexChanged);
            // 
            // lblEnableMoveVar
            // 
            this.lblEnableMoveVar.AutoSize = true;
            this.lblEnableMoveVar.Location = new System.Drawing.Point(10, 248);
            this.lblEnableMoveVar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnableMoveVar.Name = "lblEnableMoveVar";
            this.lblEnableMoveVar.Size = new System.Drawing.Size(134, 20);
            this.lblEnableMoveVar.TabIndex = 8;
            this.lblEnableMoveVar.Text = "Enable Move Var:";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(9, 206);
            this.lblLayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(52, 20);
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
            this.cmbLayering.Location = new System.Drawing.Point(72, 202);
            this.cmbLayering.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbLayering.Name = "cmbLayering";
            this.cmbLayering.Size = new System.Drawing.Size(150, 27);
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
            this.cmbEventFreq.Location = new System.Drawing.Point(72, 160);
            this.cmbEventFreq.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEventFreq.Name = "cmbEventFreq";
            this.cmbEventFreq.Size = new System.Drawing.Size(148, 27);
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
            this.cmbEventSpeed.Location = new System.Drawing.Point(72, 118);
            this.cmbEventSpeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEventSpeed.Name = "cmbEventSpeed";
            this.cmbEventSpeed.Size = new System.Drawing.Size(148, 27);
            this.cmbEventSpeed.TabIndex = 5;
            this.cmbEventSpeed.Text = "Slowest";
            this.cmbEventSpeed.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEventSpeed.SelectedIndexChanged += new System.EventHandler(this.cmbEventSpeed_SelectedIndexChanged);
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(9, 165);
            this.lblFreq.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(46, 20);
            this.lblFreq.TabIndex = 4;
            this.lblFreq.Text = "Freq:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(9, 123);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(60, 20);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "Speed:";
            // 
            // btnSetRoute
            // 
            this.btnSetRoute.Enabled = false;
            this.btnSetRoute.Location = new System.Drawing.Point(110, 66);
            this.btnSetRoute.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSetRoute.Name = "btnSetRoute";
            this.btnSetRoute.Padding = new System.Windows.Forms.Padding(8);
            this.btnSetRoute.Size = new System.Drawing.Size(112, 35);
            this.btnSetRoute.TabIndex = 2;
            this.btnSetRoute.Text = "Set Route....";
            this.btnSetRoute.Click += new System.EventHandler(this.btnSetRoute_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(9, 34);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(47, 20);
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
            this.cmbMoveType.Location = new System.Drawing.Point(72, 29);
            this.cmbMoveType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbMoveType.Name = "cmbMoveType";
            this.cmbMoveType.Size = new System.Drawing.Size(148, 27);
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
            this.grpTriggers.Location = new System.Drawing.Point(22, 863);
            this.grpTriggers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpTriggers.Name = "grpTriggers";
            this.grpTriggers.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpTriggers.Size = new System.Drawing.Size(476, 125);
            this.grpTriggers.TabIndex = 21;
            this.grpTriggers.TabStop = false;
            this.grpTriggers.Text = "Trigger";
            // 
            // chkParallelRun
            // 
            this.chkParallelRun.AutoSize = true;
            this.chkParallelRun.Location = new System.Drawing.Point(9, 66);
            this.chkParallelRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkParallelRun.Name = "chkParallelRun";
            this.chkParallelRun.Size = new System.Drawing.Size(122, 24);
            this.chkParallelRun.TabIndex = 99;
            this.chkParallelRun.Text = "Parallel run?";
            this.chkParallelRun.CheckedChanged += new System.EventHandler(this.chkParallelRun_CheckedChanged);
            // 
            // lblRecordItem
            // 
            this.lblRecordItem.AutoSize = true;
            this.lblRecordItem.Location = new System.Drawing.Point(164, 31);
            this.lblRecordItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordItem.Name = "lblRecordItem";
            this.lblRecordItem.Size = new System.Drawing.Size(101, 20);
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
            this.cmbRecordItem.Location = new System.Drawing.Point(272, 22);
            this.cmbRecordItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbRecordItem.Name = "cmbRecordItem";
            this.cmbRecordItem.Size = new System.Drawing.Size(193, 27);
            this.cmbRecordItem.TabIndex = 97;
            this.cmbRecordItem.Text = null;
            this.cmbRecordItem.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbRecordItem.SelectedIndexChanged += new System.EventHandler(this.cmbRecordItem_SelectedIndexChanged);
            // 
            // lblRecordNumber
            // 
            this.lblRecordNumber.AutoSize = true;
            this.lblRecordNumber.Location = new System.Drawing.Point(180, 65);
            this.lblRecordNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordNumber.Name = "lblRecordNumber";
            this.lblRecordNumber.Size = new System.Drawing.Size(69, 20);
            this.lblRecordNumber.TabIndex = 96;
            this.lblRecordNumber.Text = "Amount:";
            this.lblRecordNumber.Visible = false;
            // 
            // nudRecordNumber
            // 
            this.nudRecordNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRecordNumber.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRecordNumber.Location = new System.Drawing.Point(279, 62);
            this.nudRecordNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudRecordNumber.Name = "nudRecordNumber";
            this.nudRecordNumber.Size = new System.Drawing.Size(189, 26);
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
            this.lblClass.Location = new System.Drawing.Point(210, 26);
            this.lblClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(52, 20);
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
            this.cmbClass.Location = new System.Drawing.Point(274, 20);
            this.cmbClass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(193, 27);
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
            this.cmbVariable.Location = new System.Drawing.Point(272, 20);
            this.cmbVariable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbVariable.Name = "cmbVariable";
            this.cmbVariable.Size = new System.Drawing.Size(193, 27);
            this.cmbVariable.TabIndex = 14;
            this.cmbVariable.Text = "None";
            this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbVariable.SelectedIndexChanged += new System.EventHandler(this.cmbVariable_SelectedIndexChanged);
            // 
            // lblVariableTrigger
            // 
            this.lblVariableTrigger.AutoSize = true;
            this.lblVariableTrigger.Location = new System.Drawing.Point(170, 26);
            this.lblVariableTrigger.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVariableTrigger.Name = "lblVariableTrigger";
            this.lblVariableTrigger.Size = new System.Drawing.Size(71, 20);
            this.lblVariableTrigger.TabIndex = 13;
            this.lblVariableTrigger.Text = "Variable:";
            this.lblVariableTrigger.Visible = false;
            // 
            // txtCommand
            // 
            this.txtCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCommand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCommand.Location = new System.Drawing.Point(272, 20);
            this.txtCommand.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(194, 26);
            this.txtCommand.TabIndex = 12;
            this.txtCommand.Visible = false;
            this.txtCommand.TextChanged += new System.EventHandler(this.txtCommand_TextChanged);
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(170, 26);
            this.lblCommand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(98, 20);
            this.lblCommand.TabIndex = 11;
            this.lblCommand.Text = "/Command: /";
            this.lblCommand.Visible = false;
            // 
            // lblTriggerVal
            // 
            this.lblTriggerVal.AutoSize = true;
            this.lblTriggerVal.Location = new System.Drawing.Point(170, 26);
            this.lblTriggerVal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTriggerVal.Name = "lblTriggerVal";
            this.lblTriggerVal.Size = new System.Drawing.Size(77, 20);
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
            this.cmbTriggerVal.Location = new System.Drawing.Point(272, 20);
            this.cmbTriggerVal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTriggerVal.Name = "cmbTriggerVal";
            this.cmbTriggerVal.Size = new System.Drawing.Size(193, 27);
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
            this.cmbTrigger.Location = new System.Drawing.Point(9, 20);
            this.cmbTrigger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTrigger.Name = "cmbTrigger";
            this.cmbTrigger.Size = new System.Drawing.Size(150, 27);
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
            this.grpEventConditions.Location = new System.Drawing.Point(32, 137);
            this.grpEventConditions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEventConditions.Name = "grpEventConditions";
            this.grpEventConditions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEventConditions.Size = new System.Drawing.Size(489, 85);
            this.grpEventConditions.TabIndex = 5;
            this.grpEventConditions.TabStop = false;
            this.grpEventConditions.Text = "Conditions";
            // 
            // btnEditConditions
            // 
            this.btnEditConditions.Location = new System.Drawing.Point(10, 31);
            this.btnEditConditions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEditConditions.Name = "btnEditConditions";
            this.btnEditConditions.Padding = new System.Windows.Forms.Padding(8);
            this.btnEditConditions.Size = new System.Drawing.Size(456, 35);
            this.btnEditConditions.TabIndex = 3;
            this.btnEditConditions.Text = "Spawn/Execution Conditions";
            this.btnEditConditions.Click += new System.EventHandler(this.btnEditConditions_Click);
            // 
            // grpNewCommands
            // 
            this.grpNewCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpNewCommands.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNewCommands.Controls.Add(this.lblCommandFilter);
            this.grpNewCommands.Controls.Add(this.lblClearFilter);
            this.grpNewCommands.Controls.Add(this.txtFilter);
            this.grpNewCommands.Controls.Add(this.lblCloseCommands);
            this.grpNewCommands.Controls.Add(this.lstFilteredCommands);
            this.grpNewCommands.Controls.Add(this.lstCommands);
            this.grpNewCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNewCommands.Location = new System.Drawing.Point(530, 137);
            this.grpNewCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNewCommands.Name = "grpNewCommands";
            this.grpNewCommands.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpNewCommands.Size = new System.Drawing.Size(802, 980);
            this.grpNewCommands.TabIndex = 7;
            this.grpNewCommands.TabStop = false;
            this.grpNewCommands.Text = "Add Commands";
            this.grpNewCommands.Visible = false;
            this.grpNewCommands.VisibleChanged += new System.EventHandler(this.grpNewCommands_VisibleChanged);
            // 
            // lblCommandFilter
            // 
            this.lblCommandFilter.AutoSize = true;
            this.lblCommandFilter.Location = new System.Drawing.Point(11, 21);
            this.lblCommandFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCommandFilter.Name = "lblCommandFilter";
            this.lblCommandFilter.Size = new System.Drawing.Size(44, 20);
            this.lblCommandFilter.TabIndex = 4;
            this.lblCommandFilter.Text = "Filter";
            // 
            // lblClearFilter
            // 
            this.lblClearFilter.AutoSize = true;
            this.lblClearFilter.Location = new System.Drawing.Point(390, 21);
            this.lblClearFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClearFilter.Name = "lblClearFilter";
            this.lblClearFilter.Size = new System.Drawing.Size(20, 20);
            this.lblClearFilter.TabIndex = 112;
            this.lblClearFilter.Text = "X";
            this.lblClearFilter.Click += new System.EventHandler(this.lblClearFilter_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtFilter.Location = new System.Drawing.Point(74, 19);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(308, 26);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblCloseCommands
            // 
            this.lblCloseCommands.AutoSize = true;
            this.lblCloseCommands.Location = new System.Drawing.Point(766, 25);
            this.lblCloseCommands.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCloseCommands.Name = "lblCloseCommands";
            this.lblCloseCommands.Size = new System.Drawing.Size(20, 20);
            this.lblCloseCommands.TabIndex = 1;
            this.lblCloseCommands.Text = "X";
            this.lblCloseCommands.Click += new System.EventHandler(this.lblCloseCommands_Click);
            // 
            // lstFilteredCommands
            // 
            this.lstFilteredCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstFilteredCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstFilteredCommands.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstFilteredCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstFilteredCommands.FormattingEnabled = true;
            this.lstFilteredCommands.ItemHeight = 20;
            this.lstFilteredCommands.Location = new System.Drawing.Point(9, 49);
            this.lstFilteredCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstFilteredCommands.Name = "lstFilteredCommands";
            this.lstFilteredCommands.Size = new System.Drawing.Size(779, 262);
            this.lstFilteredCommands.TabIndex = 1;
            this.lstFilteredCommands.Visible = false;
            this.lstFilteredCommands.DoubleClick += new System.EventHandler(this.lstFilteredCommands_DoubleClick);
            this.lstFilteredCommands.Enter += new System.EventHandler(this.lstFilteredCommands_Enter);
            this.lstFilteredCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFilteredCommands_KeyDown);
            // 
            // lstCommands
            // 
            this.lstCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstCommands.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lstCommands.Location = new System.Drawing.Point(9, 49);
            this.lstCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstCommands.Name = "lstCommands";
            treeNode1.Name = "showtext";
            treeNode1.Tag = "1";
            treeNode1.Text = "Show Text";
            treeNode2.Name = "showoptions";
            treeNode2.Tag = "2";
            treeNode2.Text = "Show Options";
            treeNode3.Name = "inputvariable";
            treeNode3.Tag = "49";
            treeNode3.Text = "Input Variable";
            treeNode4.Name = "addchatboxtext";
            treeNode4.Tag = "3";
            treeNode4.Text = "Add Chatbox Text";
            treeNode5.Name = "dialogue";
            treeNode5.Text = "Dialogue";
            treeNode6.Name = "setvariable";
            treeNode6.Tag = "5";
            treeNode6.Text = "Set Variable";
            treeNode7.Name = "setselfswitch";
            treeNode7.Tag = "6";
            treeNode7.Text = "Set Self Switch";
            treeNode8.Name = "conditionalbranch";
            treeNode8.Tag = "7";
            treeNode8.Text = "Conditional Branch";
            treeNode9.Name = "exiteventprocess";
            treeNode9.Tag = "8";
            treeNode9.Text = "Exit Event Process";
            treeNode10.Name = "label";
            treeNode10.Tag = "9";
            treeNode10.Text = "Label";
            treeNode11.Name = "gotolabel";
            treeNode11.Tag = "10";
            treeNode11.Text = "Go To Label";
            treeNode12.Name = "startcommonevent";
            treeNode12.Tag = "11";
            treeNode12.Text = "Start Common Event";
            treeNode13.Name = "resetvariables";
            treeNode13.Tag = "72";
            treeNode13.Text = "Reset Variables in Group";
            treeNode14.Name = "logicflow";
            treeNode14.Text = "Logic Flow";
            treeNode15.Name = "restorehp";
            treeNode15.Tag = "12";
            treeNode15.Text = "Restore HP";
            treeNode16.Name = "restoremp";
            treeNode16.Tag = "13";
            treeNode16.Text = "Restore MP";
            treeNode17.Name = "levelup";
            treeNode17.Tag = "14";
            treeNode17.Text = "Level Up";
            treeNode18.Name = "giveexperience";
            treeNode18.Tag = "15";
            treeNode18.Text = "Give Experience";
            treeNode19.Name = "changelevel";
            treeNode19.Tag = "16";
            treeNode19.Text = "Change Level";
            treeNode20.Name = "changespells";
            treeNode20.Tag = "17";
            treeNode20.Text = "Change Spells";
            treeNode21.Name = "changeitems";
            treeNode21.Tag = "18";
            treeNode21.Text = "Change Items";
            treeNode22.Name = "changesprite";
            treeNode22.Tag = "19";
            treeNode22.Text = "Change Sprite";
            treeNode23.Name = "changeplayercolor";
            treeNode23.Tag = "51";
            treeNode23.Text = "Change Player Color";
            treeNode24.Name = "changeface";
            treeNode24.Tag = "20";
            treeNode24.Text = "Change Face";
            treeNode25.Name = "changegender";
            treeNode25.Tag = "21";
            treeNode25.Text = "Change Gender";
            treeNode26.Name = "setaccess";
            treeNode26.Tag = "22";
            treeNode26.Text = "Set Access";
            treeNode27.Name = "changeclass";
            treeNode27.Tag = "38";
            treeNode27.Text = "Change Class";
            treeNode28.Name = "equipitem";
            treeNode28.Tag = "47";
            treeNode28.Text = "Equip/Unequip Item";
            treeNode29.Name = "changenamecolor";
            treeNode29.Tag = "48";
            treeNode29.Text = "Change Name Color";
            treeNode30.Name = "changeplayerlabel";
            treeNode30.Tag = "50";
            treeNode30.Text = "Change Player Label";
            treeNode31.Name = "changename";
            treeNode31.Tag = "52";
            treeNode31.Text = "Change Player Name";
            treeNode32.Name = "resetstatallocations";
            treeNode32.Tag = "57";
            treeNode32.Text = "Reset Stat Point Allocations";
            treeNode33.Name = "setvehicle";
            treeNode33.Tag = "61";
            treeNode33.Text = "Set Vehicle";
            treeNode34.Name = "npcguildmanagement";
            treeNode34.Tag = "62";
            treeNode34.Text = "NPC Guild Management";
            treeNode35.Name = "addinspiration";
            treeNode35.Tag = "63";
            treeNode35.Text = "Add Inspiration";
            treeNode36.Name = "rollloot";
            treeNode36.Tag = "70";
            treeNode36.Text = "Roll Loot";
            treeNode37.Name = "unlocklabel";
            treeNode37.Tag = "71";
            treeNode37.Text = "Unlock Label";
            treeNode38.Name = "changespawn";
            treeNode38.Tag = "77";
            treeNode38.Text = "Change Respawn Point";
            treeNode39.Name = "changerecipeunlock";
            treeNode39.Tag = "78";
            treeNode39.Text = "Change Recipes";
            treeNode40.Name = "changebestiary";
            treeNode40.Tag = "79";
            treeNode40.Text = "Change Bestiary";
            treeNode41.Name = "changeweapontrack";
            treeNode41.Tag = "80";
            treeNode41.Text = "Change Weapon Track Progress";
            treeNode42.Name = "changechallenges";
            treeNode42.Tag = "81";
            treeNode42.Text = "Change Challenge Progress";
            treeNode43.Name = "changeenhancement";
            treeNode43.Tag = "86";
            treeNode43.Text = "Change Enhancement";
            treeNode44.Name = "removepermabuff";
            treeNode44.Tag = "89";
            treeNode44.Text = "Remove Permabuff";
            treeNode45.Name = "changechampsettings";
            treeNode45.Tag = "93";
            treeNode45.Text = "Change Champion Spawn Settings";
            treeNode46.Name = "castspellon";
            treeNode46.Tag = "96";
            treeNode46.Text = "Cast Spell On";
            treeNode47.Name = "playercontrol";
            treeNode47.Text = "Player Control";
            treeNode48.Name = "warpplayer";
            treeNode48.Tag = "23";
            treeNode48.Text = "Warp Player";
            treeNode49.Name = "setmoveroute";
            treeNode49.Tag = "24";
            treeNode49.Text = "Set Move Route";
            treeNode50.Name = "waitmoveroute";
            treeNode50.Tag = "25";
            treeNode50.Text = "Wait for Route Completion";
            treeNode51.Name = "holdplayer";
            treeNode51.Tag = "26";
            treeNode51.Text = "Hold Player";
            treeNode52.Name = "releaseplayer";
            treeNode52.Tag = "27";
            treeNode52.Text = "Release Player";
            treeNode53.Name = "spawnnpc";
            treeNode53.Tag = "28";
            treeNode53.Text = "Spawn NPC";
            treeNode54.Name = "despawnnpcs";
            treeNode54.Tag = "39";
            treeNode54.Text = "Despawn NPC";
            treeNode55.Name = "hideplayer";
            treeNode55.Tag = "45";
            treeNode55.Text = "Hide Player";
            treeNode56.Name = "showplayer";
            treeNode56.Tag = "46";
            treeNode56.Text = "Show Player";
            treeNode57.Name = "changemapspawngroup";
            treeNode57.Tag = "67";
            treeNode57.Text = "Change Map Spawn Group";
            treeNode58.Name = "hideevent";
            treeNode58.Tag = "94";
            treeNode58.Text = "Hide Event";
            treeNode59.Name = "showevent";
            treeNode59.Tag = "95";
            treeNode59.Text = "Show Event";
            treeNode60.Name = "permaholdplayer";
            treeNode60.Tag = "99";
            treeNode60.Text = "Hold Player (Cross-map)";
            treeNode61.Name = "movement";
            treeNode61.Text = "Movement";
            treeNode62.Name = "playanimation";
            treeNode62.Tag = "29";
            treeNode62.Text = "Play Animation";
            treeNode63.Name = "playbgm";
            treeNode63.Tag = "30";
            treeNode63.Text = "Play BGM";
            treeNode64.Name = "fadeoutbgm";
            treeNode64.Tag = "31";
            treeNode64.Text = "Fadeout BGM";
            treeNode65.Name = "playsound";
            treeNode65.Tag = "32";
            treeNode65.Text = "Play Sound";
            treeNode66.Name = "stopsounds";
            treeNode66.Tag = "33";
            treeNode66.Text = "Stop Sounds";
            treeNode67.Name = "showpicture";
            treeNode67.Tag = "43";
            treeNode67.Text = "Show Picture";
            treeNode68.Name = "hidepicture";
            treeNode68.Tag = "44";
            treeNode68.Text = "Hide Picture";
            treeNode69.Name = "flashscreen";
            treeNode69.Tag = "58";
            treeNode69.Text = "Flash Screen";
            treeNode70.Name = "fadein";
            treeNode70.Tag = "74";
            treeNode70.Text = "Fade In";
            treeNode71.Name = "fadeout";
            treeNode71.Tag = "75";
            treeNode71.Text = "Fade Out";
            treeNode72.Name = "shakescreen";
            treeNode72.Tag = "76";
            treeNode72.Text = "Shake Screen";
            treeNode73.Name = "specialeffects";
            treeNode73.Text = "Special Effects";
            treeNode74.Name = "startquest";
            treeNode74.Tag = "40";
            treeNode74.Text = "Start Quest";
            treeNode75.Name = "completequesttask";
            treeNode75.Tag = "41";
            treeNode75.Text = "Complete Quest Task";
            treeNode76.Name = "endquest";
            treeNode76.Tag = "42";
            treeNode76.Text = "End Quest";
            treeNode77.Name = "randomquest";
            treeNode77.Tag = "59";
            treeNode77.Text = "Random Quest from List";
            treeNode78.Name = "openquestboard";
            treeNode78.Tag = "60";
            treeNode78.Text = "Open Quest Board";
            treeNode79.Name = "questcontrol";
            treeNode79.Text = "Quest Control";
            treeNode80.Name = "wait";
            treeNode80.Tag = "34";
            treeNode80.Text = "Wait...";
            treeNode81.Name = "openleaderboard";
            treeNode81.Tag = "68";
            treeNode81.Text = "Open Leaderboard...";
            treeNode82.Name = "clearrecord";
            treeNode82.Tag = "69";
            treeNode82.Text = "Clear Record...";
            treeNode83.Name = "resetpermadeadnpcs";
            treeNode83.Tag = "73";
            treeNode83.Text = "Reset Permadead NPCs";
            treeNode84.Name = "resetglobalevents";
            treeNode84.Tag = "90";
            treeNode84.Text = "Reset Global Event Positions";
            treeNode85.Name = "meleesignup";
            treeNode85.Tag = "91";
            treeNode85.Text = "Signup for Open Melee";
            treeNode86.Name = "meleewithdraw";
            treeNode86.Tag = "92";
            treeNode86.Text = "Withdraw from Open Melee";
            treeNode87.Name = "forcenpcexhaustion";
            treeNode87.Tag = "98";
            treeNode87.Text = "Force NPC Exhaustion";
            treeNode88.Name = "etc";
            treeNode88.Text = "Etc";
            treeNode89.Name = "openbank";
            treeNode89.Tag = "35";
            treeNode89.Text = "Open Bank";
            treeNode90.Name = "openshop";
            treeNode90.Tag = "36";
            treeNode90.Text = "Open Shop";
            treeNode91.Name = "opencraftingstation";
            treeNode91.Tag = "37";
            treeNode91.Text = "Open Crafting Station";
            treeNode92.Name = "closecraftingtable";
            treeNode92.Tag = "97";
            treeNode92.Text = "Close Crafting Table";
            treeNode93.Name = "opendeconstructor";
            treeNode93.Tag = "85";
            treeNode93.Text = "Open Deconstructor";
            treeNode94.Name = "openenhancementwindow";
            treeNode94.Tag = "87";
            treeNode94.Text = "Open Enhancement Window";
            treeNode95.Name = "openupgradestation";
            treeNode95.Tag = "88";
            treeNode95.Text = "Open Upgrade Station";
            treeNode96.Name = "shopandbank";
            treeNode96.Text = "Shop and Bank";
            treeNode97.Name = "createguild";
            treeNode97.Tag = "53";
            treeNode97.Text = "Create Guild";
            treeNode98.Name = "disbandguild";
            treeNode98.Tag = "54";
            treeNode98.Text = "Disband Guild";
            treeNode99.Name = "openguildbank";
            treeNode99.Tag = "55";
            treeNode99.Text = "Open Guild Bank";
            treeNode100.Name = "setguildbankslots";
            treeNode100.Tag = "56";
            treeNode100.Text = "Set Guild Bank Slots Count";
            treeNode101.Name = "guilds";
            treeNode101.Text = "Guilds";
            treeNode102.Name = "starttimer";
            treeNode102.Tag = "64";
            treeNode102.Text = "Start Timer";
            treeNode103.Name = "modifytimer";
            treeNode103.Tag = "65";
            treeNode103.Text = "Modify Timer";
            treeNode104.Name = "stoptimer";
            treeNode104.Tag = "66";
            treeNode104.Text = "Stop Timer";
            treeNode105.Name = "timers";
            treeNode105.Text = "Timers";
            treeNode106.Name = "changedungeon";
            treeNode106.Tag = "82";
            treeNode106.Text = "Change Dungeon";
            treeNode107.Name = "obtaintreasuregnome";
            treeNode107.Tag = "83";
            treeNode107.Text = "Obtain Treasure Gnome";
            treeNode108.Name = "rolldungeonloot";
            treeNode108.Tag = "84";
            treeNode108.Text = "Roll Dungeon Loot";
            treeNode109.Name = "dungeons";
            treeNode109.Text = "Dungeons";
            this.lstCommands.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode14,
            treeNode47,
            treeNode61,
            treeNode73,
            treeNode79,
            treeNode88,
            treeNode96,
            treeNode101,
            treeNode105,
            treeNode109});
            this.lstCommands.Size = new System.Drawing.Size(778, 920);
            this.lstCommands.TabIndex = 2;
            this.lstCommands.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.lstCommands_NodeMouseDoubleClick);
            this.lstCommands.VisibleChanged += new System.EventHandler(this.lstCommands_VisibleChanged);
            // 
            // grpEventCommands
            // 
            this.grpEventCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEventCommands.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEventCommands.Controls.Add(this.lstEventCommands);
            this.grpEventCommands.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEventCommands.Location = new System.Drawing.Point(530, 137);
            this.grpEventCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEventCommands.Name = "grpEventCommands";
            this.grpEventCommands.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpEventCommands.Size = new System.Drawing.Size(796, 954);
            this.grpEventCommands.TabIndex = 0;
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
            this.lstEventCommands.ItemHeight = 20;
            this.lstEventCommands.Items.AddRange(new object[] {
            "@>"});
            this.lstEventCommands.Location = new System.Drawing.Point(9, 29);
            this.lstEventCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstEventCommands.Name = "lstEventCommands";
            this.lstEventCommands.Size = new System.Drawing.Size(778, 902);
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
            this.grpCreateCommands.Location = new System.Drawing.Point(530, 137);
            this.grpCreateCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCreateCommands.Name = "grpCreateCommands";
            this.grpCreateCommands.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpCreateCommands.Size = new System.Drawing.Size(788, 945);
            this.grpCreateCommands.TabIndex = 8;
            this.grpCreateCommands.TabStop = false;
            this.grpCreateCommands.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1029, 1132);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(8);
            this.btnSave.Size = new System.Drawing.Size(140, 46);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1178, 1132);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(8);
            this.btnCancel.Size = new System.Drawing.Size(140, 46);
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
            this.commandMenu.Size = new System.Drawing.Size(135, 196);
            // 
            // btnInsert
            // 
            this.btnInsert.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(134, 32);
            this.btnInsert.Text = "Insert";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(134, 32);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCut
            // 
            this.btnCut.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(134, 32);
            this.btnCut.Text = "Cut";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(134, 32);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(134, 32);
            this.btnPaste.Text = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(134, 32);
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
            this.grpPageOptions.Location = new System.Drawing.Point(470, 8);
            this.grpPageOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPageOptions.Name = "grpPageOptions";
            this.grpPageOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPageOptions.Size = new System.Drawing.Size(765, 77);
            this.grpPageOptions.TabIndex = 13;
            this.grpPageOptions.TabStop = false;
            this.grpPageOptions.Text = "Page Options";
            // 
            // btnClearPage
            // 
            this.btnClearPage.Location = new System.Drawing.Point(603, 25);
            this.btnClearPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearPage.Name = "btnClearPage";
            this.btnClearPage.Padding = new System.Windows.Forms.Padding(8);
            this.btnClearPage.Size = new System.Drawing.Size(140, 46);
            this.btnClearPage.TabIndex = 17;
            this.btnClearPage.Text = "Clear Page";
            this.btnClearPage.Click += new System.EventHandler(this.btnClearPage_Click);
            // 
            // btnDeletePage
            // 
            this.btnDeletePage.Enabled = false;
            this.btnDeletePage.Location = new System.Drawing.Point(454, 25);
            this.btnDeletePage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeletePage.Name = "btnDeletePage";
            this.btnDeletePage.Padding = new System.Windows.Forms.Padding(8);
            this.btnDeletePage.Size = new System.Drawing.Size(140, 46);
            this.btnDeletePage.TabIndex = 16;
            this.btnDeletePage.Text = "Delete Page";
            this.btnDeletePage.Click += new System.EventHandler(this.btnDeletePage_Click);
            // 
            // btnPastePage
            // 
            this.btnPastePage.Location = new System.Drawing.Point(306, 25);
            this.btnPastePage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPastePage.Name = "btnPastePage";
            this.btnPastePage.Padding = new System.Windows.Forms.Padding(8);
            this.btnPastePage.Size = new System.Drawing.Size(140, 46);
            this.btnPastePage.TabIndex = 15;
            this.btnPastePage.Text = "Paste Page";
            this.btnPastePage.Click += new System.EventHandler(this.btnPastePage_Click);
            // 
            // btnCopyPage
            // 
            this.btnCopyPage.Location = new System.Drawing.Point(158, 25);
            this.btnCopyPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCopyPage.Name = "btnCopyPage";
            this.btnCopyPage.Padding = new System.Windows.Forms.Padding(8);
            this.btnCopyPage.Size = new System.Drawing.Size(140, 46);
            this.btnCopyPage.TabIndex = 14;
            this.btnCopyPage.Text = "Copy Page";
            this.btnCopyPage.Click += new System.EventHandler(this.btnCopyPage_Click);
            // 
            // btnNewPage
            // 
            this.btnNewPage.Location = new System.Drawing.Point(9, 25);
            this.btnNewPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewPage.Name = "btnNewPage";
            this.btnNewPage.Padding = new System.Windows.Forms.Padding(8);
            this.btnNewPage.Size = new System.Drawing.Size(140, 46);
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
            this.grpGeneral.Location = new System.Drawing.Point(18, 8);
            this.grpGeneral.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpGeneral.Size = new System.Drawing.Size(442, 75);
            this.grpGeneral.TabIndex = 18;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // chkIsGlobal
            // 
            this.chkIsGlobal.AutoSize = true;
            this.chkIsGlobal.Location = new System.Drawing.Point(303, 34);
            this.chkIsGlobal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkIsGlobal.Name = "chkIsGlobal";
            this.chkIsGlobal.Size = new System.Drawing.Size(126, 24);
            this.chkIsGlobal.TabIndex = 3;
            this.chkIsGlobal.Text = "Global Event";
            this.chkIsGlobal.CheckedChanged += new System.EventHandler(this.chkIsGlobal_CheckedChanged);
            // 
            // pnlTabsContainer
            // 
            this.pnlTabsContainer.Controls.Add(this.pnlTabs);
            this.pnlTabsContainer.Location = new System.Drawing.Point(18, 94);
            this.pnlTabsContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTabsContainer.Name = "pnlTabsContainer";
            this.pnlTabsContainer.Size = new System.Drawing.Size(1216, 34);
            this.pnlTabsContainer.TabIndex = 22;
            // 
            // pnlTabs
            // 
            this.pnlTabs.AutoSize = true;
            this.pnlTabs.Location = new System.Drawing.Point(0, 0);
            this.pnlTabs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(1216, 34);
            this.pnlTabs.TabIndex = 23;
            // 
            // btnTabsRight
            // 
            this.btnTabsRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTabsRight.Location = new System.Drawing.Point(1221, 94);
            this.btnTabsRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTabsRight.Name = "btnTabsRight";
            this.btnTabsRight.Padding = new System.Windows.Forms.Padding(8);
            this.btnTabsRight.Size = new System.Drawing.Size(75, 35);
            this.btnTabsRight.TabIndex = 1;
            this.btnTabsRight.Text = ">";
            this.btnTabsRight.Click += new System.EventHandler(this.btnTabsRight_Click);
            // 
            // btnTabsLeft
            // 
            this.btnTabsLeft.Location = new System.Drawing.Point(18, 94);
            this.btnTabsLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTabsLeft.Name = "btnTabsLeft";
            this.btnTabsLeft.Padding = new System.Windows.Forms.Padding(8);
            this.btnTabsLeft.Size = new System.Drawing.Size(75, 35);
            this.btnTabsLeft.TabIndex = 0;
            this.btnTabsLeft.Text = "<";
            this.btnTabsLeft.Click += new System.EventHandler(this.btnTabsLeft_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grpTriggers);
            this.panel1.Location = new System.Drawing.Point(18, 128);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1313, 994);
            this.panel1.TabIndex = 23;
            // 
            // FrmEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1350, 1197);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private DarkCheckBox chkIgnoreBlocked;
        private DarkTextBox txtFilter;
        private ListBox lstFilteredCommands;
        private Label lblClearFilter;
        private Label lblCommandFilter;
    }
}