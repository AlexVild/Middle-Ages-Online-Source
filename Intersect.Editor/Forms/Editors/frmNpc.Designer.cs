using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmNpc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNpc));
            this.toolStrip = new DarkUI.Controls.DarkToolStrip();
            this.toolStripItemNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAlphabetical = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripItemPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripItemUndo = new System.Windows.Forms.ToolStripButton();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpNpcs = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.grpChampionSettings = new DarkUI.Controls.DarkGroupBox();
            this.chkIsChampion = new DarkUI.Controls.DarkCheckBox();
            this.nudChampSpawnChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblChampSpawnChance = new System.Windows.Forms.Label();
            this.nudChampCooldown = new DarkUI.Controls.DarkNumericUpDown();
            this.lblChampSpawnCooldown = new System.Windows.Forms.Label();
            this.cmbChampion = new DarkUI.Controls.DarkComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpDynamicScaling = new DarkUI.Controls.DarkGroupBox();
            this.nudMaxScaledTo = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMaxScaleTo = new System.Windows.Forms.Label();
            this.nudScaleFactor = new DarkUI.Controls.DarkNumericUpDown();
            this.lblScaleFactor = new System.Windows.Forms.Label();
            this.nudScaledTo = new DarkUI.Controls.DarkNumericUpDown();
            this.lblScaledTo = new System.Windows.Forms.Label();
            this.cmbScalingType = new DarkUI.Controls.DarkComboBox();
            this.lblScalingType = new System.Windows.Forms.Label();
            this.grpBestiary = new DarkUI.Controls.DarkGroupBox();
            this.chkBestiary = new DarkUI.Controls.DarkCheckBox();
            this.btnBestiaryDefaults = new DarkUI.Controls.DarkButton();
            this.nudKillCount = new DarkUI.Controls.DarkNumericUpDown();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtStartDesc = new DarkUI.Controls.DarkTextBox();
            this.lblKillCount = new System.Windows.Forms.Label();
            this.lstBestiaryUnlocks = new System.Windows.Forms.ListBox();
            this.lblBestiary = new System.Windows.Forms.Label();
            this.grpAttackOverrides = new DarkUI.Controls.DarkGroupBox();
            this.cmbSpellAttackOverride = new DarkUI.Controls.DarkComboBox();
            this.lblAttackOverride = new System.Windows.Forms.Label();
            this.grpDeathTransform = new DarkUI.Controls.DarkGroupBox();
            this.cmbTransformIntoNpc = new DarkUI.Controls.DarkComboBox();
            this.lblDeathTransform = new System.Windows.Forms.Label();
            this.grpAnimation = new DarkUI.Controls.DarkGroupBox();
            this.cmbDeathAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblDeathAnimation = new System.Windows.Forms.Label();
            this.grpImmunities = new DarkUI.Controls.DarkGroupBox();
            this.chkSpellCasting = new DarkUI.Controls.DarkCheckBox();
            this.chkStealth = new DarkUI.Controls.DarkCheckBox();
            this.chkNoBack = new DarkUI.Controls.DarkCheckBox();
            this.chkImpassable = new DarkUI.Controls.DarkCheckBox();
            this.chkConfused = new DarkUI.Controls.DarkCheckBox();
            this.chkSlowed = new DarkUI.Controls.DarkCheckBox();
            this.nudTenacity = new DarkUI.Controls.DarkNumericUpDown();
            this.lblTenacity = new System.Windows.Forms.Label();
            this.chkTaunt = new DarkUI.Controls.DarkCheckBox();
            this.chkSleep = new DarkUI.Controls.DarkCheckBox();
            this.chkTransform = new DarkUI.Controls.DarkCheckBox();
            this.chkBlind = new DarkUI.Controls.DarkCheckBox();
            this.chkSnare = new DarkUI.Controls.DarkCheckBox();
            this.chkStun = new DarkUI.Controls.DarkCheckBox();
            this.chkSilence = new DarkUI.Controls.DarkCheckBox();
            this.chkKnockback = new DarkUI.Controls.DarkCheckBox();
            this.grpCombat = new DarkUI.Controls.DarkGroupBox();
            this.lblManasteal = new System.Windows.Forms.Label();
            this.nudManasteal = new DarkUI.Controls.DarkNumericUpDown();
            this.lblLifesteal = new System.Windows.Forms.Label();
            this.nudLifesteal = new DarkUI.Controls.DarkNumericUpDown();
            this.chkSpellCast = new DarkUI.Controls.DarkCheckBox();
            this.lblSpellcaster = new System.Windows.Forms.Label();
            this.grpDamageTypes = new DarkUI.Controls.DarkGroupBox();
            this.chkDamageMagic = new DarkUI.Controls.DarkCheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkDamagePierce = new DarkUI.Controls.DarkCheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkDamageSlash = new DarkUI.Controls.DarkCheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.chkDamageBlunt = new DarkUI.Controls.DarkCheckBox();
            this.grpAttackSpeed = new DarkUI.Controls.DarkGroupBox();
            this.nudAttackSpeedValue = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAttackSpeedValue = new System.Windows.Forms.Label();
            this.cmbAttackSpeedModifier = new DarkUI.Controls.DarkComboBox();
            this.lblAttackSpeedModifier = new System.Windows.Forms.Label();
            this.nudCritMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCritMultiplier = new System.Windows.Forms.Label();
            this.nudCritChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCritChance = new System.Windows.Forms.Label();
            this.cmbAttackAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblAttackAnimation = new System.Windows.Forms.Label();
            this.grpCommonEvents = new DarkUI.Controls.DarkGroupBox();
            this.cmbOnDeathEventParty = new DarkUI.Controls.DarkComboBox();
            this.lblOnDeathEventParty = new System.Windows.Forms.Label();
            this.cmbOnDeathEventKiller = new DarkUI.Controls.DarkComboBox();
            this.lblOnDeathEventKiller = new System.Windows.Forms.Label();
            this.grpBehavior = new DarkUI.Controls.DarkGroupBox();
            this.chkStandStill = new DarkUI.Controls.DarkCheckBox();
            this.nudResetRadius = new DarkUI.Controls.DarkNumericUpDown();
            this.lblResetRadius = new System.Windows.Forms.Label();
            this.chkFocusDamageDealer = new DarkUI.Controls.DarkCheckBox();
            this.nudSpawnDuration = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSpawnDuration = new System.Windows.Forms.Label();
            this.nudFlee = new DarkUI.Controls.DarkNumericUpDown();
            this.lblFlee = new System.Windows.Forms.Label();
            this.chkSwarm = new DarkUI.Controls.DarkCheckBox();
            this.grpConditions = new DarkUI.Controls.DarkGroupBox();
            this.btnAttackOnSightCond = new DarkUI.Controls.DarkButton();
            this.btnPlayerCanAttackCond = new DarkUI.Controls.DarkButton();
            this.btnPlayerFriendProtectorCond = new DarkUI.Controls.DarkButton();
            this.lblMovement = new System.Windows.Forms.Label();
            this.cmbMovement = new DarkUI.Controls.DarkComboBox();
            this.chkAggressive = new DarkUI.Controls.DarkCheckBox();
            this.nudSightRange = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSightRange = new System.Windows.Forms.Label();
            this.grpRegen = new DarkUI.Controls.DarkGroupBox();
            this.nudMpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.lblHpRegen = new System.Windows.Forms.Label();
            this.lblManaRegen = new System.Windows.Forms.Label();
            this.lblRegenHint = new System.Windows.Forms.Label();
            this.grpDrops = new DarkUI.Controls.DarkGroupBox();
            this.chkPlayerLockLoot = new DarkUI.Controls.DarkCheckBox();
            this.grpTableSelection = new DarkUI.Controls.DarkGroupBox();
            this.nudTableChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblTableChance = new System.Windows.Forms.Label();
            this.rdoTertiary = new DarkUI.Controls.DarkRadioButton();
            this.rdoSecondary = new DarkUI.Controls.DarkRadioButton();
            this.rdoMain = new DarkUI.Controls.DarkRadioButton();
            this.btnUnselectItem = new DarkUI.Controls.DarkButton();
            this.grpTypes = new DarkUI.Controls.DarkGroupBox();
            this.rdoTable = new DarkUI.Controls.DarkRadioButton();
            this.rdoItem = new DarkUI.Controls.DarkRadioButton();
            this.chkIndividualLoot = new DarkUI.Controls.DarkCheckBox();
            this.btnDropRemove = new DarkUI.Controls.DarkButton();
            this.btnDropAdd = new DarkUI.Controls.DarkButton();
            this.lstDrops = new System.Windows.Forms.ListBox();
            this.nudDropAmount = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDropChance = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbDropItem = new DarkUI.Controls.DarkComboBox();
            this.lblDropAmount = new System.Windows.Forms.Label();
            this.lblDropChance = new System.Windows.Forms.Label();
            this.lblDropItem = new System.Windows.Forms.Label();
            this.grpNpcVsNpc = new DarkUI.Controls.DarkGroupBox();
            this.chkCannotHeal = new DarkUI.Controls.DarkCheckBox();
            this.cmbHostileNPC = new DarkUI.Controls.DarkComboBox();
            this.lblNPC = new System.Windows.Forms.Label();
            this.btnRemoveAggro = new DarkUI.Controls.DarkButton();
            this.btnAddAggro = new DarkUI.Controls.DarkButton();
            this.lstAggro = new System.Windows.Forms.ListBox();
            this.chkAttackAllies = new DarkUI.Controls.DarkCheckBox();
            this.chkEnabled = new DarkUI.Controls.DarkCheckBox();
            this.grpSpells = new DarkUI.Controls.DarkGroupBox();
            this.chkSequentialCasting = new DarkUI.Controls.DarkCheckBox();
            this.chkNeverSkip = new DarkUI.Controls.DarkCheckBox();
            this.cmbSpell = new DarkUI.Controls.DarkComboBox();
            this.cmbFreq = new DarkUI.Controls.DarkComboBox();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblSpell = new System.Windows.Forms.Label();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.btnAdd = new DarkUI.Controls.DarkButton();
            this.lstSpells = new System.Windows.Forms.ListBox();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.chkAnimated = new DarkUI.Controls.DarkCheckBox();
            this.chkHideName = new DarkUI.Controls.DarkCheckBox();
            this.lblTier = new System.Windows.Forms.Label();
            this.nudTier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAlpha = new System.Windows.Forms.Label();
            this.lblBlue = new System.Windows.Forms.Label();
            this.lblGreen = new System.Windows.Forms.Label();
            this.lblRed = new System.Windows.Forms.Label();
            this.nudRgbaA = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRgbaB = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRgbaG = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRgbaR = new DarkUI.Controls.DarkNumericUpDown();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.cmbSprite = new DarkUI.Controls.DarkComboBox();
            this.lblPic = new System.Windows.Forms.Label();
            this.picNpc = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.grpStats = new DarkUI.Controls.DarkGroupBox();
            this.btnUseCalcExp = new DarkUI.Controls.DarkButton();
            this.lblCalcExpVal = new System.Windows.Forms.Label();
            this.lblCalcExp = new System.Windows.Forms.Label();
            this.nudEvasion = new DarkUI.Controls.DarkNumericUpDown();
            this.nudAccuracy = new DarkUI.Controls.DarkNumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPierceResist = new DarkUI.Controls.DarkNumericUpDown();
            this.nudPierce = new DarkUI.Controls.DarkNumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudSlashResist = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSlash = new DarkUI.Controls.DarkNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudExp = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMana = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHp = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSpd = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMR = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDef = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMag = new DarkUI.Controls.DarkNumericUpDown();
            this.nudStr = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSpd = new System.Windows.Forms.Label();
            this.lblMR = new System.Windows.Forms.Label();
            this.lblDef = new System.Windows.Forms.Label();
            this.lblMag = new System.Windows.Forms.Label();
            this.lblStr = new System.Windows.Forms.Label();
            this.lblMana = new System.Windows.Forms.Label();
            this.lblHP = new System.Windows.Forms.Label();
            this.lblExp = new System.Windows.Forms.Label();
            this.grpBalanceHelp = new DarkUI.Controls.DarkGroupBox();
            this.btnHpXl = new DarkUI.Controls.DarkButton();
            this.btnHpHi = new DarkUI.Controls.DarkButton();
            this.btnHpMed = new DarkUI.Controls.DarkButton();
            this.btnHpLo = new DarkUI.Controls.DarkButton();
            this.lblHpHelp = new System.Windows.Forms.Label();
            this.btnMagHi = new DarkUI.Controls.DarkButton();
            this.btnMagMed = new DarkUI.Controls.DarkButton();
            this.btnMagLo = new DarkUI.Controls.DarkButton();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPierceHi = new DarkUI.Controls.DarkButton();
            this.btnPierceMed = new DarkUI.Controls.DarkButton();
            this.btnPierceLo = new DarkUI.Controls.DarkButton();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSlashHi = new DarkUI.Controls.DarkButton();
            this.btnSlashMed = new DarkUI.Controls.DarkButton();
            this.btnSlashLo = new DarkUI.Controls.DarkButton();
            this.lblSlashResHelp = new System.Windows.Forms.Label();
            this.btnBluntHigh = new DarkUI.Controls.DarkButton();
            this.btnBluntMed = new DarkUI.Controls.DarkButton();
            this.btnBluntLo = new DarkUI.Controls.DarkButton();
            this.lblPierceResRecc = new System.Windows.Forms.Label();
            this.lblMaxHitVal = new System.Windows.Forms.Label();
            this.lblMaxHit = new System.Windows.Forms.Label();
            this.lblProjectedDpsVal = new System.Windows.Forms.Label();
            this.lblTargetDpsVal = new System.Windows.Forms.Label();
            this.cmbTier = new DarkUI.Controls.DarkComboBox();
            this.lblTierView = new System.Windows.Forms.Label();
            this.lblTargetDps = new System.Windows.Forms.Label();
            this.lblProjectedDps = new System.Windows.Forms.Label();
            this.chkDisableAutoAttack = new DarkUI.Controls.DarkCheckBox();
            this.chkDisableExhaustion = new DarkUI.Controls.DarkCheckBox();
            this.toolStrip.SuspendLayout();
            this.grpNpcs.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.grpChampionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChampSpawnChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChampCooldown)).BeginInit();
            this.grpDynamicScaling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxScaledTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaledTo)).BeginInit();
            this.grpBestiary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKillCount)).BeginInit();
            this.grpAttackOverrides.SuspendLayout();
            this.grpDeathTransform.SuspendLayout();
            this.grpAnimation.SuspendLayout();
            this.grpImmunities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTenacity)).BeginInit();
            this.grpCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManasteal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLifesteal)).BeginInit();
            this.grpDamageTypes.SuspendLayout();
            this.grpAttackSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).BeginInit();
            this.grpCommonEvents.SuspendLayout();
            this.grpBehavior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResetRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlee)).BeginInit();
            this.grpConditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSightRange)).BeginInit();
            this.grpRegen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpRegen)).BeginInit();
            this.grpDrops.SuspendLayout();
            this.grpTableSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTableChance)).BeginInit();
            this.grpTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropChance)).BeginInit();
            this.grpNpcVsNpc.SuspendLayout();
            this.grpSpells.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNpc)).BeginInit();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            this.grpBalanceHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripItemNew,
            this.toolStripSeparator1,
            this.toolStripItemDelete,
            this.toolStripSeparator2,
            this.btnAlphabetical,
            this.toolStripSeparator4,
            this.toolStripItemCopy,
            this.toolStripItemPaste,
            this.toolStripSeparator3,
            this.toolStripItemUndo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(1187, 25);
            this.toolStrip.TabIndex = 45;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripItemNew
            // 
            this.toolStripItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemNew.Image")));
            this.toolStripItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemNew.Name = "toolStripItemNew";
            this.toolStripItemNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemNew.Text = "New";
            this.toolStripItemNew.Click += new System.EventHandler(this.toolStripItemNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemDelete
            // 
            this.toolStripItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemDelete.Enabled = false;
            this.toolStripItemDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemDelete.Image")));
            this.toolStripItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemDelete.Name = "toolStripItemDelete";
            this.toolStripItemDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemDelete.Text = "Delete";
            this.toolStripItemDelete.Click += new System.EventHandler(this.toolStripItemDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAlphabetical
            // 
            this.btnAlphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlphabetical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnAlphabetical.Image = ((System.Drawing.Image)(resources.GetObject("btnAlphabetical.Image")));
            this.btnAlphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlphabetical.Name = "btnAlphabetical";
            this.btnAlphabetical.Size = new System.Drawing.Size(23, 22);
            this.btnAlphabetical.Text = "Order Chronologically";
            this.btnAlphabetical.Click += new System.EventHandler(this.btnAlphabetical_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemCopy
            // 
            this.toolStripItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemCopy.Enabled = false;
            this.toolStripItemCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemCopy.Image")));
            this.toolStripItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemCopy.Name = "toolStripItemCopy";
            this.toolStripItemCopy.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemCopy.Text = "Copy";
            this.toolStripItemCopy.Click += new System.EventHandler(this.toolStripItemCopy_Click);
            // 
            // toolStripItemPaste
            // 
            this.toolStripItemPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemPaste.Enabled = false;
            this.toolStripItemPaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemPaste.Image")));
            this.toolStripItemPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemPaste.Name = "toolStripItemPaste";
            this.toolStripItemPaste.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemPaste.Text = "Paste";
            this.toolStripItemPaste.Click += new System.EventHandler(this.toolStripItemPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripItemUndo
            // 
            this.toolStripItemUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripItemUndo.Enabled = false;
            this.toolStripItemUndo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripItemUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripItemUndo.Image")));
            this.toolStripItemUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripItemUndo.Name = "toolStripItemUndo";
            this.toolStripItemUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripItemUndo.Text = "Undo";
            this.toolStripItemUndo.Click += new System.EventHandler(this.toolStripItemUndo_Click);
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
            this.lstGameObjects.Size = new System.Drawing.Size(191, 518);
            this.lstGameObjects.TabIndex = 29;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(708, 580);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(190, 27);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(512, 580);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(190, 27);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpNpcs
            // 
            this.grpNpcs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpNpcs.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNpcs.Controls.Add(this.btnClearSearch);
            this.grpNpcs.Controls.Add(this.txtSearch);
            this.grpNpcs.Controls.Add(this.lstGameObjects);
            this.grpNpcs.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNpcs.Location = new System.Drawing.Point(12, 39);
            this.grpNpcs.Name = "grpNpcs";
            this.grpNpcs.Size = new System.Drawing.Size(203, 570);
            this.grpNpcs.TabIndex = 13;
            this.grpNpcs.TabStop = false;
            this.grpNpcs.Text = "NPCs";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 18);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(18, 20);
            this.btnClearSearch.TabIndex = 34;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.Location = new System.Drawing.Point(6, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 20);
            this.txtSearch.TabIndex = 33;
            this.txtSearch.Text = "Search...";
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.grpChampionSettings);
            this.pnlContainer.Controls.Add(this.grpDynamicScaling);
            this.pnlContainer.Controls.Add(this.grpBestiary);
            this.pnlContainer.Controls.Add(this.grpAttackOverrides);
            this.pnlContainer.Controls.Add(this.grpDeathTransform);
            this.pnlContainer.Controls.Add(this.grpAnimation);
            this.pnlContainer.Controls.Add(this.grpImmunities);
            this.pnlContainer.Controls.Add(this.grpCombat);
            this.pnlContainer.Controls.Add(this.grpCommonEvents);
            this.pnlContainer.Controls.Add(this.grpBehavior);
            this.pnlContainer.Controls.Add(this.grpRegen);
            this.pnlContainer.Controls.Add(this.grpDrops);
            this.pnlContainer.Controls.Add(this.grpNpcVsNpc);
            this.pnlContainer.Controls.Add(this.grpSpells);
            this.pnlContainer.Controls.Add(this.grpGeneral);
            this.pnlContainer.Controls.Add(this.grpStats);
            this.pnlContainer.Location = new System.Drawing.Point(225, 39);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(699, 529);
            this.pnlContainer.TabIndex = 17;
            // 
            // grpChampionSettings
            // 
            this.grpChampionSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpChampionSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpChampionSettings.Controls.Add(this.chkIsChampion);
            this.grpChampionSettings.Controls.Add(this.nudChampSpawnChance);
            this.grpChampionSettings.Controls.Add(this.lblChampSpawnChance);
            this.grpChampionSettings.Controls.Add(this.nudChampCooldown);
            this.grpChampionSettings.Controls.Add(this.lblChampSpawnCooldown);
            this.grpChampionSettings.Controls.Add(this.cmbChampion);
            this.grpChampionSettings.Controls.Add(this.label12);
            this.grpChampionSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpChampionSettings.Location = new System.Drawing.Point(212, 1291);
            this.grpChampionSettings.Margin = new System.Windows.Forms.Padding(2);
            this.grpChampionSettings.Name = "grpChampionSettings";
            this.grpChampionSettings.Padding = new System.Windows.Forms.Padding(2);
            this.grpChampionSettings.Size = new System.Drawing.Size(229, 173);
            this.grpChampionSettings.TabIndex = 90;
            this.grpChampionSettings.TabStop = false;
            this.grpChampionSettings.Text = "Champion Spawning";
            // 
            // chkIsChampion
            // 
            this.chkIsChampion.AutoSize = true;
            this.chkIsChampion.Location = new System.Drawing.Point(12, 141);
            this.chkIsChampion.Name = "chkIsChampion";
            this.chkIsChampion.Size = new System.Drawing.Size(90, 17);
            this.chkIsChampion.TabIndex = 80;
            this.chkIsChampion.Text = "Is Champion?";
            this.chkIsChampion.CheckedChanged += new System.EventHandler(this.chkIsChampion_CheckedChanged);
            // 
            // nudChampSpawnChance
            // 
            this.nudChampSpawnChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudChampSpawnChance.DecimalPlaces = 2;
            this.nudChampSpawnChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudChampSpawnChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudChampSpawnChance.Location = new System.Drawing.Point(12, 76);
            this.nudChampSpawnChance.Name = "nudChampSpawnChance";
            this.nudChampSpawnChance.Size = new System.Drawing.Size(86, 20);
            this.nudChampSpawnChance.TabIndex = 33;
            this.nudChampSpawnChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudChampSpawnChance.ValueChanged += new System.EventHandler(this.nudChampSpawnChance_ValueChanged);
            // 
            // lblChampSpawnChance
            // 
            this.lblChampSpawnChance.AutoSize = true;
            this.lblChampSpawnChance.Location = new System.Drawing.Point(9, 60);
            this.lblChampSpawnChance.Name = "lblChampSpawnChance";
            this.lblChampSpawnChance.Size = new System.Drawing.Size(80, 13);
            this.lblChampSpawnChance.TabIndex = 32;
            this.lblChampSpawnChance.Text = "Spawn Chance";
            // 
            // nudChampCooldown
            // 
            this.nudChampCooldown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudChampCooldown.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudChampCooldown.Location = new System.Drawing.Point(13, 115);
            this.nudChampCooldown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudChampCooldown.Name = "nudChampCooldown";
            this.nudChampCooldown.Size = new System.Drawing.Size(86, 20);
            this.nudChampCooldown.TabIndex = 31;
            this.nudChampCooldown.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudChampCooldown.ValueChanged += new System.EventHandler(this.nudChampCooldown_ValueChanged);
            // 
            // lblChampSpawnCooldown
            // 
            this.lblChampSpawnCooldown.AutoSize = true;
            this.lblChampSpawnCooldown.Location = new System.Drawing.Point(9, 99);
            this.lblChampSpawnCooldown.Name = "lblChampSpawnCooldown";
            this.lblChampSpawnCooldown.Size = new System.Drawing.Size(137, 13);
            this.lblChampSpawnCooldown.TabIndex = 20;
            this.lblChampSpawnCooldown.Text = "Death Cooldown (Seconds)";
            // 
            // cmbChampion
            // 
            this.cmbChampion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbChampion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbChampion.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbChampion.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbChampion.DrawDropdownHoverOutline = false;
            this.cmbChampion.DrawFocusRectangle = false;
            this.cmbChampion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbChampion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChampion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbChampion.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbChampion.FormattingEnabled = true;
            this.cmbChampion.Location = new System.Drawing.Point(12, 36);
            this.cmbChampion.Name = "cmbChampion";
            this.cmbChampion.Size = new System.Drawing.Size(211, 21);
            this.cmbChampion.TabIndex = 19;
            this.cmbChampion.Text = null;
            this.cmbChampion.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbChampion.SelectedIndexChanged += new System.EventHandler(this.cmbChampion_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Champion Variant";
            // 
            // grpDynamicScaling
            // 
            this.grpDynamicScaling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDynamicScaling.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDynamicScaling.Controls.Add(this.nudMaxScaledTo);
            this.grpDynamicScaling.Controls.Add(this.lblMaxScaleTo);
            this.grpDynamicScaling.Controls.Add(this.nudScaleFactor);
            this.grpDynamicScaling.Controls.Add(this.lblScaleFactor);
            this.grpDynamicScaling.Controls.Add(this.nudScaledTo);
            this.grpDynamicScaling.Controls.Add(this.lblScaledTo);
            this.grpDynamicScaling.Controls.Add(this.cmbScalingType);
            this.grpDynamicScaling.Controls.Add(this.lblScalingType);
            this.grpDynamicScaling.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDynamicScaling.Location = new System.Drawing.Point(212, 1135);
            this.grpDynamicScaling.Margin = new System.Windows.Forms.Padding(2);
            this.grpDynamicScaling.Name = "grpDynamicScaling";
            this.grpDynamicScaling.Padding = new System.Windows.Forms.Padding(2);
            this.grpDynamicScaling.Size = new System.Drawing.Size(229, 147);
            this.grpDynamicScaling.TabIndex = 35;
            this.grpDynamicScaling.TabStop = false;
            this.grpDynamicScaling.Text = "Dynamic Scaling";
            // 
            // nudMaxScaledTo
            // 
            this.nudMaxScaledTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxScaledTo.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxScaledTo.Location = new System.Drawing.Point(156, 76);
            this.nudMaxScaledTo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMaxScaledTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxScaledTo.Name = "nudMaxScaledTo";
            this.nudMaxScaledTo.Size = new System.Drawing.Size(67, 20);
            this.nudMaxScaledTo.TabIndex = 35;
            this.nudMaxScaledTo.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudMaxScaledTo.ValueChanged += new System.EventHandler(this.darkNumericUpDown1_ValueChanged);
            // 
            // lblMaxScaleTo
            // 
            this.lblMaxScaleTo.AutoSize = true;
            this.lblMaxScaleTo.Location = new System.Drawing.Point(176, 60);
            this.lblMaxScaleTo.Name = "lblMaxScaleTo";
            this.lblMaxScaleTo.Size = new System.Drawing.Size(27, 13);
            this.lblMaxScaleTo.TabIndex = 34;
            this.lblMaxScaleTo.Text = "Max";
            // 
            // nudScaleFactor
            // 
            this.nudScaleFactor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudScaleFactor.DecimalPlaces = 2;
            this.nudScaleFactor.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudScaleFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudScaleFactor.Location = new System.Drawing.Point(12, 117);
            this.nudScaleFactor.Name = "nudScaleFactor";
            this.nudScaleFactor.Size = new System.Drawing.Size(86, 20);
            this.nudScaleFactor.TabIndex = 33;
            this.nudScaleFactor.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudScaleFactor.ValueChanged += new System.EventHandler(this.nudScaleFactor_ValueChanged);
            // 
            // lblScaleFactor
            // 
            this.lblScaleFactor.AutoSize = true;
            this.lblScaleFactor.Location = new System.Drawing.Point(10, 101);
            this.lblScaleFactor.Name = "lblScaleFactor";
            this.lblScaleFactor.Size = new System.Drawing.Size(67, 13);
            this.lblScaleFactor.TabIndex = 32;
            this.lblScaleFactor.Text = "Scale Factor";
            // 
            // nudScaledTo
            // 
            this.nudScaledTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudScaledTo.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudScaledTo.Location = new System.Drawing.Point(12, 76);
            this.nudScaledTo.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudScaledTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudScaledTo.Name = "nudScaledTo";
            this.nudScaledTo.Size = new System.Drawing.Size(86, 20);
            this.nudScaledTo.TabIndex = 31;
            this.nudScaledTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudScaledTo.ValueChanged += new System.EventHandler(this.nudScaledTo_ValueChanged);
            // 
            // lblScaledTo
            // 
            this.lblScaledTo.AutoSize = true;
            this.lblScaledTo.Location = new System.Drawing.Point(9, 60);
            this.lblScaledTo.Name = "lblScaledTo";
            this.lblScaledTo.Size = new System.Drawing.Size(138, 13);
            this.lblScaledTo.TabIndex = 20;
            this.lblScaledTo.Text = "Scaled To (# of aggressors)";
            // 
            // cmbScalingType
            // 
            this.cmbScalingType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbScalingType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbScalingType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbScalingType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbScalingType.DrawDropdownHoverOutline = false;
            this.cmbScalingType.DrawFocusRectangle = false;
            this.cmbScalingType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbScalingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScalingType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbScalingType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbScalingType.FormattingEnabled = true;
            this.cmbScalingType.Location = new System.Drawing.Point(12, 36);
            this.cmbScalingType.Name = "cmbScalingType";
            this.cmbScalingType.Size = new System.Drawing.Size(211, 21);
            this.cmbScalingType.TabIndex = 19;
            this.cmbScalingType.Text = null;
            this.cmbScalingType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbScalingType.SelectedIndexChanged += new System.EventHandler(this.cmbScalingType_SelectedIndexChanged);
            // 
            // lblScalingType
            // 
            this.lblScalingType.AutoSize = true;
            this.lblScalingType.Location = new System.Drawing.Point(9, 20);
            this.lblScalingType.Name = "lblScalingType";
            this.lblScalingType.Size = new System.Drawing.Size(69, 13);
            this.lblScalingType.TabIndex = 18;
            this.lblScalingType.Text = "Scaling Type";
            // 
            // grpBestiary
            // 
            this.grpBestiary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBestiary.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBestiary.Controls.Add(this.chkBestiary);
            this.grpBestiary.Controls.Add(this.btnBestiaryDefaults);
            this.grpBestiary.Controls.Add(this.nudKillCount);
            this.grpBestiary.Controls.Add(this.lblDescription);
            this.grpBestiary.Controls.Add(this.txtStartDesc);
            this.grpBestiary.Controls.Add(this.lblKillCount);
            this.grpBestiary.Controls.Add(this.lstBestiaryUnlocks);
            this.grpBestiary.Controls.Add(this.lblBestiary);
            this.grpBestiary.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBestiary.Location = new System.Drawing.Point(447, 818);
            this.grpBestiary.Margin = new System.Windows.Forms.Padding(2);
            this.grpBestiary.Name = "grpBestiary";
            this.grpBestiary.Padding = new System.Windows.Forms.Padding(2);
            this.grpBestiary.Size = new System.Drawing.Size(226, 355);
            this.grpBestiary.TabIndex = 89;
            this.grpBestiary.TabStop = false;
            this.grpBestiary.Text = "Bestiary";
            // 
            // chkBestiary
            // 
            this.chkBestiary.AutoSize = true;
            this.chkBestiary.Location = new System.Drawing.Point(122, 10);
            this.chkBestiary.Name = "chkBestiary";
            this.chkBestiary.Size = new System.Drawing.Size(99, 17);
            this.chkBestiary.TabIndex = 95;
            this.chkBestiary.Text = "Not in bestiary?";
            this.chkBestiary.CheckedChanged += new System.EventHandler(this.chkBestiary_CheckedChanged);
            // 
            // btnBestiaryDefaults
            // 
            this.btnBestiaryDefaults.Location = new System.Drawing.Point(115, 186);
            this.btnBestiaryDefaults.Name = "btnBestiaryDefaults";
            this.btnBestiaryDefaults.Padding = new System.Windows.Forms.Padding(5);
            this.btnBestiaryDefaults.Size = new System.Drawing.Size(102, 23);
            this.btnBestiaryDefaults.TabIndex = 94;
            this.btnBestiaryDefaults.Text = "Use Defaults";
            this.btnBestiaryDefaults.Click += new System.EventHandler(this.btnBestiaryDefaults_Click);
            // 
            // nudKillCount
            // 
            this.nudKillCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudKillCount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudKillCount.Location = new System.Drawing.Point(10, 160);
            this.nudKillCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudKillCount.Name = "nudKillCount";
            this.nudKillCount.Size = new System.Drawing.Size(207, 20);
            this.nudKillCount.TabIndex = 93;
            this.nudKillCount.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudKillCount.ValueChanged += new System.EventHandler(this.nudKillCount_ValueChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(7, 224);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 92;
            this.lblDescription.Text = "Description";
            // 
            // txtStartDesc
            // 
            this.txtStartDesc.AcceptsReturn = true;
            this.txtStartDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.txtStartDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartDesc.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtStartDesc.Location = new System.Drawing.Point(10, 242);
            this.txtStartDesc.Multiline = true;
            this.txtStartDesc.Name = "txtStartDesc";
            this.txtStartDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStartDesc.Size = new System.Drawing.Size(207, 92);
            this.txtStartDesc.TabIndex = 91;
            this.txtStartDesc.TextChanged += new System.EventHandler(this.txtStartDesc_TextChanged);
            // 
            // lblKillCount
            // 
            this.lblKillCount.AutoSize = true;
            this.lblKillCount.Location = new System.Drawing.Point(10, 143);
            this.lblKillCount.Name = "lblKillCount";
            this.lblKillCount.Size = new System.Drawing.Size(163, 13);
            this.lblKillCount.TabIndex = 89;
            this.lblKillCount.Text = "Kill Count (0 for always unlocked)";
            // 
            // lstBestiaryUnlocks
            // 
            this.lstBestiaryUnlocks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBestiaryUnlocks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBestiaryUnlocks.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBestiaryUnlocks.FormattingEnabled = true;
            this.lstBestiaryUnlocks.Location = new System.Drawing.Point(10, 30);
            this.lstBestiaryUnlocks.Name = "lstBestiaryUnlocks";
            this.lstBestiaryUnlocks.Size = new System.Drawing.Size(207, 106);
            this.lstBestiaryUnlocks.TabIndex = 80;
            this.lstBestiaryUnlocks.SelectedIndexChanged += new System.EventHandler(this.lstBestiaryUnlocks_SelectedIndexChanged);
            // 
            // lblBestiary
            // 
            this.lblBestiary.AutoSize = true;
            this.lblBestiary.Location = new System.Drawing.Point(10, 15);
            this.lblBestiary.Name = "lblBestiary";
            this.lblBestiary.Size = new System.Drawing.Size(46, 13);
            this.lblBestiary.TabIndex = 79;
            this.lblBestiary.Text = "Unlocks";
            // 
            // grpAttackOverrides
            // 
            this.grpAttackOverrides.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAttackOverrides.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAttackOverrides.Controls.Add(this.cmbSpellAttackOverride);
            this.grpAttackOverrides.Controls.Add(this.lblAttackOverride);
            this.grpAttackOverrides.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAttackOverrides.Location = new System.Drawing.Point(215, 803);
            this.grpAttackOverrides.Margin = new System.Windows.Forms.Padding(2);
            this.grpAttackOverrides.Name = "grpAttackOverrides";
            this.grpAttackOverrides.Padding = new System.Windows.Forms.Padding(2);
            this.grpAttackOverrides.Size = new System.Drawing.Size(226, 62);
            this.grpAttackOverrides.TabIndex = 35;
            this.grpAttackOverrides.TabStop = false;
            this.grpAttackOverrides.Text = "Attack Overrides";
            // 
            // cmbSpellAttackOverride
            // 
            this.cmbSpellAttackOverride.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpellAttackOverride.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpellAttackOverride.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpellAttackOverride.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpellAttackOverride.DrawDropdownHoverOutline = false;
            this.cmbSpellAttackOverride.DrawFocusRectangle = false;
            this.cmbSpellAttackOverride.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpellAttackOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpellAttackOverride.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpellAttackOverride.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpellAttackOverride.FormattingEnabled = true;
            this.cmbSpellAttackOverride.Location = new System.Drawing.Point(12, 36);
            this.cmbSpellAttackOverride.Name = "cmbSpellAttackOverride";
            this.cmbSpellAttackOverride.Size = new System.Drawing.Size(202, 21);
            this.cmbSpellAttackOverride.TabIndex = 19;
            this.cmbSpellAttackOverride.Text = null;
            this.cmbSpellAttackOverride.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSpellAttackOverride.SelectedIndexChanged += new System.EventHandler(this.cmbSpellAttackOverride_SelectedIndexChanged);
            // 
            // lblAttackOverride
            // 
            this.lblAttackOverride.AutoSize = true;
            this.lblAttackOverride.Location = new System.Drawing.Point(9, 20);
            this.lblAttackOverride.Name = "lblAttackOverride";
            this.lblAttackOverride.Size = new System.Drawing.Size(92, 13);
            this.lblAttackOverride.TabIndex = 18;
            this.lblAttackOverride.Text = "Spell Auto Attack:";
            // 
            // grpDeathTransform
            // 
            this.grpDeathTransform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDeathTransform.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDeathTransform.Controls.Add(this.cmbTransformIntoNpc);
            this.grpDeathTransform.Controls.Add(this.lblDeathTransform);
            this.grpDeathTransform.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDeathTransform.Location = new System.Drawing.Point(212, 1059);
            this.grpDeathTransform.Margin = new System.Windows.Forms.Padding(2);
            this.grpDeathTransform.Name = "grpDeathTransform";
            this.grpDeathTransform.Padding = new System.Windows.Forms.Padding(2);
            this.grpDeathTransform.Size = new System.Drawing.Size(229, 68);
            this.grpDeathTransform.TabIndex = 34;
            this.grpDeathTransform.TabStop = false;
            this.grpDeathTransform.Text = "Death Transform";
            // 
            // cmbTransformIntoNpc
            // 
            this.cmbTransformIntoNpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTransformIntoNpc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTransformIntoNpc.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTransformIntoNpc.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTransformIntoNpc.DrawDropdownHoverOutline = false;
            this.cmbTransformIntoNpc.DrawFocusRectangle = false;
            this.cmbTransformIntoNpc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTransformIntoNpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransformIntoNpc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTransformIntoNpc.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTransformIntoNpc.FormattingEnabled = true;
            this.cmbTransformIntoNpc.Location = new System.Drawing.Point(12, 36);
            this.cmbTransformIntoNpc.Name = "cmbTransformIntoNpc";
            this.cmbTransformIntoNpc.Size = new System.Drawing.Size(211, 21);
            this.cmbTransformIntoNpc.TabIndex = 19;
            this.cmbTransformIntoNpc.Text = null;
            this.cmbTransformIntoNpc.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTransformIntoNpc.SelectedIndexChanged += new System.EventHandler(this.cmbTransformIntoNpc_SelectedIndexChanged);
            // 
            // lblDeathTransform
            // 
            this.lblDeathTransform.AutoSize = true;
            this.lblDeathTransform.Location = new System.Drawing.Point(9, 20);
            this.lblDeathTransform.Name = "lblDeathTransform";
            this.lblDeathTransform.Size = new System.Drawing.Size(78, 13);
            this.lblDeathTransform.TabIndex = 18;
            this.lblDeathTransform.Text = "Transform Into:";
            // 
            // grpAnimation
            // 
            this.grpAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAnimation.Controls.Add(this.cmbDeathAnimation);
            this.grpAnimation.Controls.Add(this.lblDeathAnimation);
            this.grpAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAnimation.Location = new System.Drawing.Point(212, 984);
            this.grpAnimation.Margin = new System.Windows.Forms.Padding(2);
            this.grpAnimation.Name = "grpAnimation";
            this.grpAnimation.Padding = new System.Windows.Forms.Padding(2);
            this.grpAnimation.Size = new System.Drawing.Size(229, 68);
            this.grpAnimation.TabIndex = 33;
            this.grpAnimation.TabStop = false;
            this.grpAnimation.Text = "Animations";
            // 
            // cmbDeathAnimation
            // 
            this.cmbDeathAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDeathAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDeathAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDeathAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDeathAnimation.DrawDropdownHoverOutline = false;
            this.cmbDeathAnimation.DrawFocusRectangle = false;
            this.cmbDeathAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDeathAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeathAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDeathAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDeathAnimation.FormattingEnabled = true;
            this.cmbDeathAnimation.Location = new System.Drawing.Point(12, 36);
            this.cmbDeathAnimation.Name = "cmbDeathAnimation";
            this.cmbDeathAnimation.Size = new System.Drawing.Size(211, 21);
            this.cmbDeathAnimation.TabIndex = 19;
            this.cmbDeathAnimation.Text = null;
            this.cmbDeathAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDeathAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbDeathAnimation_SelectedIndexChanged);
            // 
            // lblDeathAnimation
            // 
            this.lblDeathAnimation.AutoSize = true;
            this.lblDeathAnimation.Location = new System.Drawing.Point(9, 20);
            this.lblDeathAnimation.Name = "lblDeathAnimation";
            this.lblDeathAnimation.Size = new System.Drawing.Size(88, 13);
            this.lblDeathAnimation.TabIndex = 18;
            this.lblDeathAnimation.Text = "Death Animation:";
            // 
            // grpImmunities
            // 
            this.grpImmunities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpImmunities.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpImmunities.Controls.Add(this.chkSpellCasting);
            this.grpImmunities.Controls.Add(this.chkStealth);
            this.grpImmunities.Controls.Add(this.chkNoBack);
            this.grpImmunities.Controls.Add(this.chkImpassable);
            this.grpImmunities.Controls.Add(this.chkConfused);
            this.grpImmunities.Controls.Add(this.chkSlowed);
            this.grpImmunities.Controls.Add(this.nudTenacity);
            this.grpImmunities.Controls.Add(this.lblTenacity);
            this.grpImmunities.Controls.Add(this.chkTaunt);
            this.grpImmunities.Controls.Add(this.chkSleep);
            this.grpImmunities.Controls.Add(this.chkTransform);
            this.grpImmunities.Controls.Add(this.chkBlind);
            this.grpImmunities.Controls.Add(this.chkSnare);
            this.grpImmunities.Controls.Add(this.chkStun);
            this.grpImmunities.Controls.Add(this.chkSilence);
            this.grpImmunities.Controls.Add(this.chkKnockback);
            this.grpImmunities.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpImmunities.Location = new System.Drawing.Point(446, 534);
            this.grpImmunities.Margin = new System.Windows.Forms.Padding(2);
            this.grpImmunities.Name = "grpImmunities";
            this.grpImmunities.Padding = new System.Windows.Forms.Padding(2);
            this.grpImmunities.Size = new System.Drawing.Size(226, 280);
            this.grpImmunities.TabIndex = 33;
            this.grpImmunities.TabStop = false;
            this.grpImmunities.Text = "Immunities";
            // 
            // chkSpellCasting
            // 
            this.chkSpellCasting.AutoSize = true;
            this.chkSpellCasting.Location = new System.Drawing.Point(11, 164);
            this.chkSpellCasting.Name = "chkSpellCasting";
            this.chkSpellCasting.Size = new System.Drawing.Size(83, 17);
            this.chkSpellCasting.TabIndex = 92;
            this.chkSpellCasting.Text = "Spellcasting";
            this.chkSpellCasting.CheckedChanged += new System.EventHandler(this.chkSpellCasting_CheckedChanged);
            // 
            // chkStealth
            // 
            this.chkStealth.AutoSize = true;
            this.chkStealth.Location = new System.Drawing.Point(11, 141);
            this.chkStealth.Name = "chkStealth";
            this.chkStealth.Size = new System.Drawing.Size(93, 17);
            this.chkStealth.TabIndex = 91;
            this.chkStealth.Text = "Stealth Attack";
            this.chkStealth.CheckedChanged += new System.EventHandler(this.chkStealth_CheckedChanged);
            // 
            // chkNoBack
            // 
            this.chkNoBack.AutoSize = true;
            this.chkNoBack.Location = new System.Drawing.Point(116, 141);
            this.chkNoBack.Name = "chkNoBack";
            this.chkNoBack.Size = new System.Drawing.Size(71, 17);
            this.chkNoBack.TabIndex = 90;
            this.chkNoBack.Text = "Backstab";
            this.chkNoBack.CheckedChanged += new System.EventHandler(this.chkNoBack_CheckedChanged);
            // 
            // chkImpassable
            // 
            this.chkImpassable.AutoSize = true;
            this.chkImpassable.Location = new System.Drawing.Point(10, 255);
            this.chkImpassable.Name = "chkImpassable";
            this.chkImpassable.Size = new System.Drawing.Size(79, 17);
            this.chkImpassable.TabIndex = 89;
            this.chkImpassable.Text = "Impassable";
            this.chkImpassable.CheckedChanged += new System.EventHandler(this.chkImpassable_CheckedChanged);
            // 
            // chkConfused
            // 
            this.chkConfused.AutoSize = true;
            this.chkConfused.Location = new System.Drawing.Point(116, 118);
            this.chkConfused.Name = "chkConfused";
            this.chkConfused.Size = new System.Drawing.Size(71, 17);
            this.chkConfused.TabIndex = 88;
            this.chkConfused.Text = "Confused";
            this.chkConfused.CheckedChanged += new System.EventHandler(this.chkConfused_CheckedChanged);
            // 
            // chkSlowed
            // 
            this.chkSlowed.AutoSize = true;
            this.chkSlowed.Location = new System.Drawing.Point(11, 118);
            this.chkSlowed.Name = "chkSlowed";
            this.chkSlowed.Size = new System.Drawing.Size(61, 17);
            this.chkSlowed.TabIndex = 87;
            this.chkSlowed.Text = "Slowed";
            this.chkSlowed.CheckedChanged += new System.EventHandler(this.chkSlowed_CheckedChanged);
            // 
            // nudTenacity
            // 
            this.nudTenacity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTenacity.DecimalPlaces = 2;
            this.nudTenacity.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTenacity.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudTenacity.Location = new System.Drawing.Point(11, 226);
            this.nudTenacity.Name = "nudTenacity";
            this.nudTenacity.Size = new System.Drawing.Size(195, 20);
            this.nudTenacity.TabIndex = 79;
            this.nudTenacity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTenacity.ValueChanged += new System.EventHandler(this.nudTenacity_ValueChanged);
            // 
            // lblTenacity
            // 
            this.lblTenacity.AutoSize = true;
            this.lblTenacity.Location = new System.Drawing.Point(11, 205);
            this.lblTenacity.Name = "lblTenacity";
            this.lblTenacity.Size = new System.Drawing.Size(68, 13);
            this.lblTenacity.TabIndex = 79;
            this.lblTenacity.Text = "Tenacity (%):";
            // 
            // chkTaunt
            // 
            this.chkTaunt.AutoSize = true;
            this.chkTaunt.Location = new System.Drawing.Point(116, 95);
            this.chkTaunt.Name = "chkTaunt";
            this.chkTaunt.Size = new System.Drawing.Size(54, 17);
            this.chkTaunt.TabIndex = 86;
            this.chkTaunt.Text = "Taunt";
            this.chkTaunt.CheckedChanged += new System.EventHandler(this.chkTaunt_CheckedChanged);
            // 
            // chkSleep
            // 
            this.chkSleep.AutoSize = true;
            this.chkSleep.Location = new System.Drawing.Point(11, 95);
            this.chkSleep.Name = "chkSleep";
            this.chkSleep.Size = new System.Drawing.Size(53, 17);
            this.chkSleep.TabIndex = 85;
            this.chkSleep.Text = "Sleep";
            this.chkSleep.CheckedChanged += new System.EventHandler(this.chkSleep_CheckedChanged);
            // 
            // chkTransform
            // 
            this.chkTransform.AutoSize = true;
            this.chkTransform.Location = new System.Drawing.Point(116, 72);
            this.chkTransform.Name = "chkTransform";
            this.chkTransform.Size = new System.Drawing.Size(73, 17);
            this.chkTransform.TabIndex = 84;
            this.chkTransform.Text = "Transform";
            this.chkTransform.CheckedChanged += new System.EventHandler(this.chkTransform_CheckedChanged);
            // 
            // chkBlind
            // 
            this.chkBlind.AutoSize = true;
            this.chkBlind.Location = new System.Drawing.Point(11, 72);
            this.chkBlind.Name = "chkBlind";
            this.chkBlind.Size = new System.Drawing.Size(49, 17);
            this.chkBlind.TabIndex = 83;
            this.chkBlind.Text = "Blind";
            this.chkBlind.CheckedChanged += new System.EventHandler(this.chkBlind_CheckedChanged);
            // 
            // chkSnare
            // 
            this.chkSnare.AutoSize = true;
            this.chkSnare.Location = new System.Drawing.Point(116, 49);
            this.chkSnare.Name = "chkSnare";
            this.chkSnare.Size = new System.Drawing.Size(54, 17);
            this.chkSnare.TabIndex = 82;
            this.chkSnare.Text = "Snare";
            this.chkSnare.CheckedChanged += new System.EventHandler(this.chkSnare_CheckedChanged);
            // 
            // chkStun
            // 
            this.chkStun.AutoSize = true;
            this.chkStun.Location = new System.Drawing.Point(11, 49);
            this.chkStun.Name = "chkStun";
            this.chkStun.Size = new System.Drawing.Size(48, 17);
            this.chkStun.TabIndex = 81;
            this.chkStun.Text = "Stun";
            this.chkStun.CheckedChanged += new System.EventHandler(this.chkStun_CheckedChanged);
            // 
            // chkSilence
            // 
            this.chkSilence.AutoSize = true;
            this.chkSilence.Location = new System.Drawing.Point(116, 26);
            this.chkSilence.Name = "chkSilence";
            this.chkSilence.Size = new System.Drawing.Size(61, 17);
            this.chkSilence.TabIndex = 80;
            this.chkSilence.Text = "Silence";
            this.chkSilence.CheckedChanged += new System.EventHandler(this.chkSilence_CheckedChanged);
            // 
            // chkKnockback
            // 
            this.chkKnockback.AutoSize = true;
            this.chkKnockback.Location = new System.Drawing.Point(11, 26);
            this.chkKnockback.Name = "chkKnockback";
            this.chkKnockback.Size = new System.Drawing.Size(81, 17);
            this.chkKnockback.TabIndex = 79;
            this.chkKnockback.Text = "Knockback";
            this.chkKnockback.CheckedChanged += new System.EventHandler(this.chkKnockback_CheckedChanged);
            // 
            // grpCombat
            // 
            this.grpCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCombat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCombat.Controls.Add(this.lblManasteal);
            this.grpCombat.Controls.Add(this.nudManasteal);
            this.grpCombat.Controls.Add(this.lblLifesteal);
            this.grpCombat.Controls.Add(this.nudLifesteal);
            this.grpCombat.Controls.Add(this.chkSpellCast);
            this.grpCombat.Controls.Add(this.lblSpellcaster);
            this.grpCombat.Controls.Add(this.grpDamageTypes);
            this.grpCombat.Controls.Add(this.grpAttackSpeed);
            this.grpCombat.Controls.Add(this.nudCritMultiplier);
            this.grpCombat.Controls.Add(this.lblCritMultiplier);
            this.grpCombat.Controls.Add(this.nudCritChance);
            this.grpCombat.Controls.Add(this.lblCritChance);
            this.grpCombat.Controls.Add(this.cmbAttackAnimation);
            this.grpCombat.Controls.Add(this.lblAttackAnimation);
            this.grpCombat.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCombat.Location = new System.Drawing.Point(215, 380);
            this.grpCombat.Name = "grpCombat";
            this.grpCombat.Size = new System.Drawing.Size(226, 416);
            this.grpCombat.TabIndex = 17;
            this.grpCombat.TabStop = false;
            this.grpCombat.Text = "Combat";
            // 
            // lblManasteal
            // 
            this.lblManasteal.AutoSize = true;
            this.lblManasteal.Location = new System.Drawing.Point(10, 345);
            this.lblManasteal.Name = "lblManasteal";
            this.lblManasteal.Size = new System.Drawing.Size(67, 13);
            this.lblManasteal.TabIndex = 126;
            this.lblManasteal.Text = "Manasteal %";
            // 
            // nudManasteal
            // 
            this.nudManasteal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudManasteal.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudManasteal.Location = new System.Drawing.Point(13, 360);
            this.nudManasteal.Name = "nudManasteal";
            this.nudManasteal.Size = new System.Drawing.Size(191, 20);
            this.nudManasteal.TabIndex = 125;
            this.nudManasteal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudManasteal.ValueChanged += new System.EventHandler(this.darkNumericUpDown1_ValueChanged_1);
            // 
            // lblLifesteal
            // 
            this.lblLifesteal.AutoSize = true;
            this.lblLifesteal.Location = new System.Drawing.Point(10, 301);
            this.lblLifesteal.Name = "lblLifesteal";
            this.lblLifesteal.Size = new System.Drawing.Size(57, 13);
            this.lblLifesteal.TabIndex = 124;
            this.lblLifesteal.Text = "Lifesteal %";
            // 
            // nudLifesteal
            // 
            this.nudLifesteal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudLifesteal.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudLifesteal.Location = new System.Drawing.Point(13, 320);
            this.nudLifesteal.Name = "nudLifesteal";
            this.nudLifesteal.Size = new System.Drawing.Size(191, 20);
            this.nudLifesteal.TabIndex = 123;
            this.nudLifesteal.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudLifesteal.ValueChanged += new System.EventHandler(this.nudLifesteal_ValueChanged);
            // 
            // chkSpellCast
            // 
            this.chkSpellCast.AutoSize = true;
            this.chkSpellCast.Location = new System.Drawing.Point(171, 386);
            this.chkSpellCast.Name = "chkSpellCast";
            this.chkSpellCast.Size = new System.Drawing.Size(15, 14);
            this.chkSpellCast.TabIndex = 122;
            this.chkSpellCast.CheckedChanged += new System.EventHandler(this.chkSpellCast_CheckedChanged);
            // 
            // lblSpellcaster
            // 
            this.lblSpellcaster.AutoSize = true;
            this.lblSpellcaster.Location = new System.Drawing.Point(20, 386);
            this.lblSpellcaster.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpellcaster.Name = "lblSpellcaster";
            this.lblSpellcaster.Size = new System.Drawing.Size(151, 13);
            this.lblSpellcaster.TabIndex = 121;
            this.lblSpellcaster.Text = "Is spellcaster? (For threat calc)";
            // 
            // grpDamageTypes
            // 
            this.grpDamageTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDamageTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDamageTypes.Controls.Add(this.chkDamageMagic);
            this.grpDamageTypes.Controls.Add(this.label14);
            this.grpDamageTypes.Controls.Add(this.chkDamagePierce);
            this.grpDamageTypes.Controls.Add(this.label15);
            this.grpDamageTypes.Controls.Add(this.chkDamageSlash);
            this.grpDamageTypes.Controls.Add(this.label16);
            this.grpDamageTypes.Controls.Add(this.label17);
            this.grpDamageTypes.Controls.Add(this.chkDamageBlunt);
            this.grpDamageTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDamageTypes.Location = new System.Drawing.Point(16, 20);
            this.grpDamageTypes.Margin = new System.Windows.Forms.Padding(2);
            this.grpDamageTypes.Name = "grpDamageTypes";
            this.grpDamageTypes.Padding = new System.Windows.Forms.Padding(2);
            this.grpDamageTypes.Size = new System.Drawing.Size(188, 56);
            this.grpDamageTypes.TabIndex = 120;
            this.grpDamageTypes.TabStop = false;
            this.grpDamageTypes.Text = "Damage Types";
            // 
            // chkDamageMagic
            // 
            this.chkDamageMagic.AutoSize = true;
            this.chkDamageMagic.Location = new System.Drawing.Point(159, 31);
            this.chkDamageMagic.Name = "chkDamageMagic";
            this.chkDamageMagic.Size = new System.Drawing.Size(15, 14);
            this.chkDamageMagic.TabIndex = 93;
            this.chkDamageMagic.CheckedChanged += new System.EventHandler(this.chkDamageMagic_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(150, 15);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 113;
            this.label14.Text = "Magic";
            // 
            // chkDamagePierce
            // 
            this.chkDamagePierce.AutoSize = true;
            this.chkDamagePierce.Location = new System.Drawing.Point(115, 31);
            this.chkDamagePierce.Name = "chkDamagePierce";
            this.chkDamagePierce.Size = new System.Drawing.Size(15, 14);
            this.chkDamagePierce.TabIndex = 92;
            this.chkDamagePierce.CheckedChanged += new System.EventHandler(this.chkDamagePierce_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(101, 15);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 112;
            this.label15.Text = "Piercing";
            // 
            // chkDamageSlash
            // 
            this.chkDamageSlash.AutoSize = true;
            this.chkDamageSlash.Location = new System.Drawing.Point(61, 31);
            this.chkDamageSlash.Name = "chkDamageSlash";
            this.chkDamageSlash.Size = new System.Drawing.Size(15, 14);
            this.chkDamageSlash.TabIndex = 91;
            this.chkDamageSlash.CheckedChanged += new System.EventHandler(this.chkDamageSlash_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(46, 15);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 111;
            this.label16.Text = "Slashing";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 15);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Blunt";
            // 
            // chkDamageBlunt
            // 
            this.chkDamageBlunt.AutoSize = true;
            this.chkDamageBlunt.Location = new System.Drawing.Point(12, 31);
            this.chkDamageBlunt.Name = "chkDamageBlunt";
            this.chkDamageBlunt.Size = new System.Drawing.Size(15, 14);
            this.chkDamageBlunt.TabIndex = 89;
            this.chkDamageBlunt.CheckedChanged += new System.EventHandler(this.chkDamageBlunt_CheckedChanged);
            // 
            // grpAttackSpeed
            // 
            this.grpAttackSpeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAttackSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAttackSpeed.Controls.Add(this.nudAttackSpeedValue);
            this.grpAttackSpeed.Controls.Add(this.lblAttackSpeedValue);
            this.grpAttackSpeed.Controls.Add(this.cmbAttackSpeedModifier);
            this.grpAttackSpeed.Controls.Add(this.lblAttackSpeedModifier);
            this.grpAttackSpeed.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAttackSpeed.Location = new System.Drawing.Point(16, 210);
            this.grpAttackSpeed.Name = "grpAttackSpeed";
            this.grpAttackSpeed.Size = new System.Drawing.Size(192, 86);
            this.grpAttackSpeed.TabIndex = 64;
            this.grpAttackSpeed.TabStop = false;
            this.grpAttackSpeed.Text = "Attack Speed";
            // 
            // nudAttackSpeedValue
            // 
            this.nudAttackSpeedValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAttackSpeedValue.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAttackSpeedValue.Location = new System.Drawing.Point(60, 58);
            this.nudAttackSpeedValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAttackSpeedValue.Name = "nudAttackSpeedValue";
            this.nudAttackSpeedValue.Size = new System.Drawing.Size(114, 20);
            this.nudAttackSpeedValue.TabIndex = 56;
            this.nudAttackSpeedValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAttackSpeedValue.ValueChanged += new System.EventHandler(this.nudAttackSpeedValue_ValueChanged);
            // 
            // lblAttackSpeedValue
            // 
            this.lblAttackSpeedValue.AutoSize = true;
            this.lblAttackSpeedValue.Location = new System.Drawing.Point(9, 60);
            this.lblAttackSpeedValue.Name = "lblAttackSpeedValue";
            this.lblAttackSpeedValue.Size = new System.Drawing.Size(37, 13);
            this.lblAttackSpeedValue.TabIndex = 29;
            this.lblAttackSpeedValue.Text = "Value:";
            // 
            // cmbAttackSpeedModifier
            // 
            this.cmbAttackSpeedModifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAttackSpeedModifier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAttackSpeedModifier.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAttackSpeedModifier.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAttackSpeedModifier.DrawDropdownHoverOutline = false;
            this.cmbAttackSpeedModifier.DrawFocusRectangle = false;
            this.cmbAttackSpeedModifier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAttackSpeedModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttackSpeedModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAttackSpeedModifier.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAttackSpeedModifier.FormattingEnabled = true;
            this.cmbAttackSpeedModifier.Location = new System.Drawing.Point(60, 24);
            this.cmbAttackSpeedModifier.Name = "cmbAttackSpeedModifier";
            this.cmbAttackSpeedModifier.Size = new System.Drawing.Size(114, 21);
            this.cmbAttackSpeedModifier.TabIndex = 28;
            this.cmbAttackSpeedModifier.Text = null;
            this.cmbAttackSpeedModifier.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackSpeedModifier.SelectedIndexChanged += new System.EventHandler(this.cmbAttackSpeedModifier_SelectedIndexChanged);
            // 
            // lblAttackSpeedModifier
            // 
            this.lblAttackSpeedModifier.AutoSize = true;
            this.lblAttackSpeedModifier.Location = new System.Drawing.Point(9, 27);
            this.lblAttackSpeedModifier.Name = "lblAttackSpeedModifier";
            this.lblAttackSpeedModifier.Size = new System.Drawing.Size(47, 13);
            this.lblAttackSpeedModifier.TabIndex = 0;
            this.lblAttackSpeedModifier.Text = "Modifier:";
            // 
            // nudCritMultiplier
            // 
            this.nudCritMultiplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritMultiplier.DecimalPlaces = 2;
            this.nudCritMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudCritMultiplier.Location = new System.Drawing.Point(11, 141);
            this.nudCritMultiplier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCritMultiplier.Name = "nudCritMultiplier";
            this.nudCritMultiplier.Size = new System.Drawing.Size(191, 20);
            this.nudCritMultiplier.TabIndex = 63;
            this.nudCritMultiplier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritMultiplier.ValueChanged += new System.EventHandler(this.nudCritMultiplier_ValueChanged);
            // 
            // lblCritMultiplier
            // 
            this.lblCritMultiplier.AutoSize = true;
            this.lblCritMultiplier.Location = new System.Drawing.Point(10, 121);
            this.lblCritMultiplier.Name = "lblCritMultiplier";
            this.lblCritMultiplier.Size = new System.Drawing.Size(135, 13);
            this.lblCritMultiplier.TabIndex = 62;
            this.lblCritMultiplier.Text = "Crit Multiplier (Default 1.5x):";
            // 
            // nudCritChance
            // 
            this.nudCritChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritChance.Location = new System.Drawing.Point(13, 98);
            this.nudCritChance.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudCritChance.Name = "nudCritChance";
            this.nudCritChance.Size = new System.Drawing.Size(191, 20);
            this.nudCritChance.TabIndex = 59;
            this.nudCritChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritChance.ValueChanged += new System.EventHandler(this.nudCritChance_ValueChanged);
            // 
            // lblCritChance
            // 
            this.lblCritChance.AutoSize = true;
            this.lblCritChance.Location = new System.Drawing.Point(10, 82);
            this.lblCritChance.Name = "lblCritChance";
            this.lblCritChance.Size = new System.Drawing.Size(82, 13);
            this.lblCritChance.TabIndex = 52;
            this.lblCritChance.Text = "Crit Chance (%):";
            // 
            // cmbAttackAnimation
            // 
            this.cmbAttackAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAttackAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAttackAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAttackAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAttackAnimation.DrawDropdownHoverOutline = false;
            this.cmbAttackAnimation.DrawFocusRectangle = false;
            this.cmbAttackAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAttackAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttackAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAttackAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAttackAnimation.FormattingEnabled = true;
            this.cmbAttackAnimation.Location = new System.Drawing.Point(10, 181);
            this.cmbAttackAnimation.Name = "cmbAttackAnimation";
            this.cmbAttackAnimation.Size = new System.Drawing.Size(192, 21);
            this.cmbAttackAnimation.TabIndex = 50;
            this.cmbAttackAnimation.Text = null;
            this.cmbAttackAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAttackAnimation_SelectedIndexChanged);
            // 
            // lblAttackAnimation
            // 
            this.lblAttackAnimation.AutoSize = true;
            this.lblAttackAnimation.Location = new System.Drawing.Point(9, 164);
            this.lblAttackAnimation.Name = "lblAttackAnimation";
            this.lblAttackAnimation.Size = new System.Drawing.Size(90, 13);
            this.lblAttackAnimation.TabIndex = 49;
            this.lblAttackAnimation.Text = "Attack Animation:";
            // 
            // grpCommonEvents
            // 
            this.grpCommonEvents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCommonEvents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCommonEvents.Controls.Add(this.cmbOnDeathEventParty);
            this.grpCommonEvents.Controls.Add(this.lblOnDeathEventParty);
            this.grpCommonEvents.Controls.Add(this.cmbOnDeathEventKiller);
            this.grpCommonEvents.Controls.Add(this.lblOnDeathEventKiller);
            this.grpCommonEvents.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCommonEvents.Location = new System.Drawing.Point(212, 873);
            this.grpCommonEvents.Margin = new System.Windows.Forms.Padding(2);
            this.grpCommonEvents.Name = "grpCommonEvents";
            this.grpCommonEvents.Padding = new System.Windows.Forms.Padding(2);
            this.grpCommonEvents.Size = new System.Drawing.Size(229, 106);
            this.grpCommonEvents.TabIndex = 32;
            this.grpCommonEvents.TabStop = false;
            this.grpCommonEvents.Text = "Common Events";
            // 
            // cmbOnDeathEventParty
            // 
            this.cmbOnDeathEventParty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbOnDeathEventParty.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbOnDeathEventParty.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbOnDeathEventParty.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbOnDeathEventParty.DrawDropdownHoverOutline = false;
            this.cmbOnDeathEventParty.DrawFocusRectangle = false;
            this.cmbOnDeathEventParty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOnDeathEventParty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOnDeathEventParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOnDeathEventParty.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbOnDeathEventParty.FormattingEnabled = true;
            this.cmbOnDeathEventParty.Location = new System.Drawing.Point(12, 80);
            this.cmbOnDeathEventParty.Name = "cmbOnDeathEventParty";
            this.cmbOnDeathEventParty.Size = new System.Drawing.Size(211, 21);
            this.cmbOnDeathEventParty.TabIndex = 21;
            this.cmbOnDeathEventParty.Text = null;
            this.cmbOnDeathEventParty.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbOnDeathEventParty.SelectedIndexChanged += new System.EventHandler(this.cmbOnDeathEventParty_SelectedIndexChanged);
            // 
            // lblOnDeathEventParty
            // 
            this.lblOnDeathEventParty.AutoSize = true;
            this.lblOnDeathEventParty.Location = new System.Drawing.Point(9, 64);
            this.lblOnDeathEventParty.Name = "lblOnDeathEventParty";
            this.lblOnDeathEventParty.Size = new System.Drawing.Size(103, 13);
            this.lblOnDeathEventParty.TabIndex = 20;
            this.lblOnDeathEventParty.Text = "On Death (for party):";
            // 
            // cmbOnDeathEventKiller
            // 
            this.cmbOnDeathEventKiller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbOnDeathEventKiller.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbOnDeathEventKiller.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbOnDeathEventKiller.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbOnDeathEventKiller.DrawDropdownHoverOutline = false;
            this.cmbOnDeathEventKiller.DrawFocusRectangle = false;
            this.cmbOnDeathEventKiller.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOnDeathEventKiller.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOnDeathEventKiller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOnDeathEventKiller.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbOnDeathEventKiller.FormattingEnabled = true;
            this.cmbOnDeathEventKiller.Location = new System.Drawing.Point(12, 36);
            this.cmbOnDeathEventKiller.Name = "cmbOnDeathEventKiller";
            this.cmbOnDeathEventKiller.Size = new System.Drawing.Size(211, 21);
            this.cmbOnDeathEventKiller.TabIndex = 19;
            this.cmbOnDeathEventKiller.Text = null;
            this.cmbOnDeathEventKiller.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbOnDeathEventKiller.SelectedIndexChanged += new System.EventHandler(this.cmbOnDeathEventKiller_SelectedIndexChanged);
            // 
            // lblOnDeathEventKiller
            // 
            this.lblOnDeathEventKiller.AutoSize = true;
            this.lblOnDeathEventKiller.Location = new System.Drawing.Point(9, 20);
            this.lblOnDeathEventKiller.Name = "lblOnDeathEventKiller";
            this.lblOnDeathEventKiller.Size = new System.Drawing.Size(101, 13);
            this.lblOnDeathEventKiller.TabIndex = 18;
            this.lblOnDeathEventKiller.Text = "On Death (for killer):";
            // 
            // grpBehavior
            // 
            this.grpBehavior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBehavior.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBehavior.Controls.Add(this.chkDisableExhaustion);
            this.grpBehavior.Controls.Add(this.chkDisableAutoAttack);
            this.grpBehavior.Controls.Add(this.chkStandStill);
            this.grpBehavior.Controls.Add(this.nudResetRadius);
            this.grpBehavior.Controls.Add(this.lblResetRadius);
            this.grpBehavior.Controls.Add(this.chkFocusDamageDealer);
            this.grpBehavior.Controls.Add(this.nudSpawnDuration);
            this.grpBehavior.Controls.Add(this.lblSpawnDuration);
            this.grpBehavior.Controls.Add(this.nudFlee);
            this.grpBehavior.Controls.Add(this.lblFlee);
            this.grpBehavior.Controls.Add(this.chkSwarm);
            this.grpBehavior.Controls.Add(this.grpConditions);
            this.grpBehavior.Controls.Add(this.lblMovement);
            this.grpBehavior.Controls.Add(this.cmbMovement);
            this.grpBehavior.Controls.Add(this.chkAggressive);
            this.grpBehavior.Controls.Add(this.nudSightRange);
            this.grpBehavior.Controls.Add(this.lblSightRange);
            this.grpBehavior.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBehavior.Location = new System.Drawing.Point(215, 1);
            this.grpBehavior.Name = "grpBehavior";
            this.grpBehavior.Size = new System.Drawing.Size(226, 373);
            this.grpBehavior.TabIndex = 32;
            this.grpBehavior.TabStop = false;
            this.grpBehavior.Text = "Behavior:";
            // 
            // chkStandStill
            // 
            this.chkStandStill.AutoSize = true;
            this.chkStandStill.Location = new System.Drawing.Point(153, 16);
            this.chkStandStill.Name = "chkStandStill";
            this.chkStandStill.Size = new System.Drawing.Size(73, 17);
            this.chkStandStill.TabIndex = 77;
            this.chkStandStill.Text = "Stand Still";
            this.chkStandStill.CheckedChanged += new System.EventHandler(this.chkStandStill_CheckedChanged);
            // 
            // nudResetRadius
            // 
            this.nudResetRadius.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudResetRadius.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudResetRadius.Location = new System.Drawing.Point(90, 66);
            this.nudResetRadius.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudResetRadius.Name = "nudResetRadius";
            this.nudResetRadius.Size = new System.Drawing.Size(121, 20);
            this.nudResetRadius.TabIndex = 76;
            this.nudResetRadius.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudResetRadius.ValueChanged += new System.EventHandler(this.nudResetRadius_ValueChanged);
            // 
            // lblResetRadius
            // 
            this.lblResetRadius.AutoSize = true;
            this.lblResetRadius.Location = new System.Drawing.Point(10, 68);
            this.lblResetRadius.Name = "lblResetRadius";
            this.lblResetRadius.Size = new System.Drawing.Size(74, 13);
            this.lblResetRadius.TabIndex = 75;
            this.lblResetRadius.Text = "Reset Radius:";
            // 
            // chkFocusDamageDealer
            // 
            this.chkFocusDamageDealer.AutoSize = true;
            this.chkFocusDamageDealer.Location = new System.Drawing.Point(13, 169);
            this.chkFocusDamageDealer.Name = "chkFocusDamageDealer";
            this.chkFocusDamageDealer.Size = new System.Drawing.Size(177, 17);
            this.chkFocusDamageDealer.TabIndex = 71;
            this.chkFocusDamageDealer.Text = "Focus Highest Damage Dealer?";
            this.chkFocusDamageDealer.CheckedChanged += new System.EventHandler(this.chkFocusDamageDealer_CheckedChanged);
            // 
            // nudSpawnDuration
            // 
            this.nudSpawnDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpawnDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpawnDuration.Location = new System.Drawing.Point(122, 119);
            this.nudSpawnDuration.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudSpawnDuration.Name = "nudSpawnDuration";
            this.nudSpawnDuration.Size = new System.Drawing.Size(89, 20);
            this.nudSpawnDuration.TabIndex = 61;
            this.nudSpawnDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpawnDuration.ValueChanged += new System.EventHandler(this.nudSpawnDuration_ValueChanged);
            // 
            // lblSpawnDuration
            // 
            this.lblSpawnDuration.AutoSize = true;
            this.lblSpawnDuration.Location = new System.Drawing.Point(10, 121);
            this.lblSpawnDuration.Name = "lblSpawnDuration";
            this.lblSpawnDuration.Size = new System.Drawing.Size(86, 13);
            this.lblSpawnDuration.TabIndex = 7;
            this.lblSpawnDuration.Text = "Spawn Duration:";
            // 
            // nudFlee
            // 
            this.nudFlee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudFlee.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudFlee.Location = new System.Drawing.Point(91, 145);
            this.nudFlee.Name = "nudFlee";
            this.nudFlee.Size = new System.Drawing.Size(80, 20);
            this.nudFlee.TabIndex = 70;
            this.nudFlee.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudFlee.ValueChanged += new System.EventHandler(this.nudFlee_ValueChanged);
            // 
            // lblFlee
            // 
            this.lblFlee.AutoSize = true;
            this.lblFlee.Location = new System.Drawing.Point(9, 147);
            this.lblFlee.Name = "lblFlee";
            this.lblFlee.Size = new System.Drawing.Size(75, 13);
            this.lblFlee.TabIndex = 69;
            this.lblFlee.Text = "Flee Health %:";
            // 
            // chkSwarm
            // 
            this.chkSwarm.AutoSize = true;
            this.chkSwarm.Location = new System.Drawing.Point(93, 16);
            this.chkSwarm.Name = "chkSwarm";
            this.chkSwarm.Size = new System.Drawing.Size(58, 17);
            this.chkSwarm.TabIndex = 67;
            this.chkSwarm.Text = "Swarm";
            this.chkSwarm.CheckedChanged += new System.EventHandler(this.chkSwarm_CheckedChanged);
            // 
            // grpConditions
            // 
            this.grpConditions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpConditions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpConditions.Controls.Add(this.btnAttackOnSightCond);
            this.grpConditions.Controls.Add(this.btnPlayerCanAttackCond);
            this.grpConditions.Controls.Add(this.btnPlayerFriendProtectorCond);
            this.grpConditions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpConditions.Location = new System.Drawing.Point(13, 251);
            this.grpConditions.Name = "grpConditions";
            this.grpConditions.Size = new System.Drawing.Size(207, 108);
            this.grpConditions.TabIndex = 66;
            this.grpConditions.TabStop = false;
            this.grpConditions.Text = "Conditions:";
            // 
            // btnAttackOnSightCond
            // 
            this.btnAttackOnSightCond.Location = new System.Drawing.Point(6, 48);
            this.btnAttackOnSightCond.Name = "btnAttackOnSightCond";
            this.btnAttackOnSightCond.Padding = new System.Windows.Forms.Padding(5);
            this.btnAttackOnSightCond.Size = new System.Drawing.Size(195, 23);
            this.btnAttackOnSightCond.TabIndex = 47;
            this.btnAttackOnSightCond.Text = "Should Not Attack Player On Sight";
            this.btnAttackOnSightCond.Click += new System.EventHandler(this.btnAttackOnSightCond_Click);
            // 
            // btnPlayerCanAttackCond
            // 
            this.btnPlayerCanAttackCond.Location = new System.Drawing.Point(6, 77);
            this.btnPlayerCanAttackCond.Name = "btnPlayerCanAttackCond";
            this.btnPlayerCanAttackCond.Padding = new System.Windows.Forms.Padding(5);
            this.btnPlayerCanAttackCond.Size = new System.Drawing.Size(195, 23);
            this.btnPlayerCanAttackCond.TabIndex = 46;
            this.btnPlayerCanAttackCond.Text = "Player Can Attack (Default: True)";
            this.btnPlayerCanAttackCond.Click += new System.EventHandler(this.btnPlayerCanAttackCond_Click);
            // 
            // btnPlayerFriendProtectorCond
            // 
            this.btnPlayerFriendProtectorCond.Location = new System.Drawing.Point(6, 19);
            this.btnPlayerFriendProtectorCond.Name = "btnPlayerFriendProtectorCond";
            this.btnPlayerFriendProtectorCond.Padding = new System.Windows.Forms.Padding(5);
            this.btnPlayerFriendProtectorCond.Size = new System.Drawing.Size(195, 23);
            this.btnPlayerFriendProtectorCond.TabIndex = 44;
            this.btnPlayerFriendProtectorCond.Text = "Player Friend/Protector";
            this.btnPlayerFriendProtectorCond.Click += new System.EventHandler(this.btnPlayerFriendProtectorCond_Click);
            // 
            // lblMovement
            // 
            this.lblMovement.AutoSize = true;
            this.lblMovement.Location = new System.Drawing.Point(10, 95);
            this.lblMovement.Name = "lblMovement";
            this.lblMovement.Size = new System.Drawing.Size(60, 13);
            this.lblMovement.TabIndex = 65;
            this.lblMovement.Text = "Movement:";
            // 
            // cmbMovement
            // 
            this.cmbMovement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMovement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMovement.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMovement.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMovement.DrawDropdownHoverOutline = false;
            this.cmbMovement.DrawFocusRectangle = false;
            this.cmbMovement.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMovement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMovement.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMovement.FormattingEnabled = true;
            this.cmbMovement.Items.AddRange(new object[] {
            "Move Randomly",
            "Turn Randomly",
            "No Movement"});
            this.cmbMovement.Location = new System.Drawing.Point(90, 92);
            this.cmbMovement.Name = "cmbMovement";
            this.cmbMovement.Size = new System.Drawing.Size(121, 21);
            this.cmbMovement.TabIndex = 64;
            this.cmbMovement.Text = "Move Randomly";
            this.cmbMovement.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbMovement.SelectedIndexChanged += new System.EventHandler(this.cmbMovement_SelectedIndexChanged);
            // 
            // chkAggressive
            // 
            this.chkAggressive.AutoSize = true;
            this.chkAggressive.Location = new System.Drawing.Point(9, 16);
            this.chkAggressive.Name = "chkAggressive";
            this.chkAggressive.Size = new System.Drawing.Size(78, 17);
            this.chkAggressive.TabIndex = 1;
            this.chkAggressive.Text = "Aggressive";
            this.chkAggressive.CheckedChanged += new System.EventHandler(this.chkAggressive_CheckedChanged);
            // 
            // nudSightRange
            // 
            this.nudSightRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSightRange.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSightRange.Location = new System.Drawing.Point(90, 40);
            this.nudSightRange.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSightRange.Name = "nudSightRange";
            this.nudSightRange.Size = new System.Drawing.Size(121, 20);
            this.nudSightRange.TabIndex = 62;
            this.nudSightRange.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSightRange.ValueChanged += new System.EventHandler(this.nudSightRange_ValueChanged);
            // 
            // lblSightRange
            // 
            this.lblSightRange.AutoSize = true;
            this.lblSightRange.Location = new System.Drawing.Point(10, 42);
            this.lblSightRange.Name = "lblSightRange";
            this.lblSightRange.Size = new System.Drawing.Size(69, 13);
            this.lblSightRange.TabIndex = 12;
            this.lblSightRange.Text = "Sight Range:";
            // 
            // grpRegen
            // 
            this.grpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRegen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRegen.Controls.Add(this.nudMpRegen);
            this.grpRegen.Controls.Add(this.nudHpRegen);
            this.grpRegen.Controls.Add(this.lblHpRegen);
            this.grpRegen.Controls.Add(this.lblManaRegen);
            this.grpRegen.Controls.Add(this.lblRegenHint);
            this.grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRegen.Location = new System.Drawing.Point(3, 637);
            this.grpRegen.Margin = new System.Windows.Forms.Padding(2);
            this.grpRegen.Name = "grpRegen";
            this.grpRegen.Padding = new System.Windows.Forms.Padding(2);
            this.grpRegen.Size = new System.Drawing.Size(206, 99);
            this.grpRegen.TabIndex = 31;
            this.grpRegen.TabStop = false;
            this.grpRegen.Text = "Regen";
            // 
            // nudMpRegen
            // 
            this.nudMpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMpRegen.Location = new System.Drawing.Point(8, 69);
            this.nudMpRegen.Name = "nudMpRegen";
            this.nudMpRegen.Size = new System.Drawing.Size(86, 20);
            this.nudMpRegen.TabIndex = 31;
            this.nudMpRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMpRegen.ValueChanged += new System.EventHandler(this.nudMpRegen_ValueChanged);
            // 
            // nudHpRegen
            // 
            this.nudHpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHpRegen.Location = new System.Drawing.Point(8, 31);
            this.nudHpRegen.Name = "nudHpRegen";
            this.nudHpRegen.Size = new System.Drawing.Size(86, 20);
            this.nudHpRegen.TabIndex = 30;
            this.nudHpRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHpRegen.ValueChanged += new System.EventHandler(this.nudHpRegen_ValueChanged);
            // 
            // lblHpRegen
            // 
            this.lblHpRegen.AutoSize = true;
            this.lblHpRegen.Location = new System.Drawing.Point(5, 17);
            this.lblHpRegen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHpRegen.Name = "lblHpRegen";
            this.lblHpRegen.Size = new System.Drawing.Size(42, 13);
            this.lblHpRegen.TabIndex = 26;
            this.lblHpRegen.Text = "HP: (%)";
            // 
            // lblManaRegen
            // 
            this.lblManaRegen.AutoSize = true;
            this.lblManaRegen.Location = new System.Drawing.Point(5, 54);
            this.lblManaRegen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManaRegen.Name = "lblManaRegen";
            this.lblManaRegen.Size = new System.Drawing.Size(54, 13);
            this.lblManaRegen.TabIndex = 27;
            this.lblManaRegen.Text = "Mana: (%)";
            // 
            // lblRegenHint
            // 
            this.lblRegenHint.Location = new System.Drawing.Point(102, 26);
            this.lblRegenHint.Name = "lblRegenHint";
            this.lblRegenHint.Size = new System.Drawing.Size(100, 72);
            this.lblRegenHint.TabIndex = 0;
            this.lblRegenHint.Text = "% of HP/Mana to restore per tick.\r\n\r\nTick timer saved in server config.json.";
            // 
            // grpDrops
            // 
            this.grpDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDrops.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDrops.Controls.Add(this.chkPlayerLockLoot);
            this.grpDrops.Controls.Add(this.grpTableSelection);
            this.grpDrops.Controls.Add(this.btnUnselectItem);
            this.grpDrops.Controls.Add(this.grpTypes);
            this.grpDrops.Controls.Add(this.chkIndividualLoot);
            this.grpDrops.Controls.Add(this.btnDropRemove);
            this.grpDrops.Controls.Add(this.btnDropAdd);
            this.grpDrops.Controls.Add(this.lstDrops);
            this.grpDrops.Controls.Add(this.nudDropAmount);
            this.grpDrops.Controls.Add(this.nudDropChance);
            this.grpDrops.Controls.Add(this.cmbDropItem);
            this.grpDrops.Controls.Add(this.lblDropAmount);
            this.grpDrops.Controls.Add(this.lblDropChance);
            this.grpDrops.Controls.Add(this.lblDropItem);
            this.grpDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDrops.Location = new System.Drawing.Point(447, 3);
            this.grpDrops.Name = "grpDrops";
            this.grpDrops.Size = new System.Drawing.Size(226, 523);
            this.grpDrops.TabIndex = 30;
            this.grpDrops.TabStop = false;
            this.grpDrops.Text = "Drops";
            // 
            // chkPlayerLockLoot
            // 
            this.chkPlayerLockLoot.AutoSize = true;
            this.chkPlayerLockLoot.Location = new System.Drawing.Point(14, 469);
            this.chkPlayerLockLoot.Name = "chkPlayerLockLoot";
            this.chkPlayerLockLoot.Size = new System.Drawing.Size(116, 17);
            this.chkPlayerLockLoot.TabIndex = 79;
            this.chkPlayerLockLoot.Text = "Player-locked loot?";
            this.chkPlayerLockLoot.CheckedChanged += new System.EventHandler(this.chkPlayerLockLoot_CheckedChanged);
            // 
            // grpTableSelection
            // 
            this.grpTableSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTableSelection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTableSelection.Controls.Add(this.nudTableChance);
            this.grpTableSelection.Controls.Add(this.lblTableChance);
            this.grpTableSelection.Controls.Add(this.rdoTertiary);
            this.grpTableSelection.Controls.Add(this.rdoSecondary);
            this.grpTableSelection.Controls.Add(this.rdoMain);
            this.grpTableSelection.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTableSelection.Location = new System.Drawing.Point(14, 64);
            this.grpTableSelection.Name = "grpTableSelection";
            this.grpTableSelection.Size = new System.Drawing.Size(195, 127);
            this.grpTableSelection.TabIndex = 60;
            this.grpTableSelection.TabStop = false;
            this.grpTableSelection.Text = "Table";
            // 
            // nudTableChance
            // 
            this.nudTableChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTableChance.DecimalPlaces = 2;
            this.nudTableChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTableChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudTableChance.Location = new System.Drawing.Point(6, 91);
            this.nudTableChance.Name = "nudTableChance";
            this.nudTableChance.Size = new System.Drawing.Size(179, 20);
            this.nudTableChance.TabIndex = 79;
            this.nudTableChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTableChance.ValueChanged += new System.EventHandler(this.nudTableChance_ValueChanged);
            // 
            // lblTableChance
            // 
            this.lblTableChance.AutoSize = true;
            this.lblTableChance.Location = new System.Drawing.Point(8, 68);
            this.lblTableChance.Name = "lblTableChance";
            this.lblTableChance.Size = new System.Drawing.Size(113, 13);
            this.lblTableChance.TabIndex = 79;
            this.lblTableChance.Text = "% Chance to use table";
            // 
            // rdoTertiary
            // 
            this.rdoTertiary.AutoSize = true;
            this.rdoTertiary.Location = new System.Drawing.Point(11, 43);
            this.rdoTertiary.Name = "rdoTertiary";
            this.rdoTertiary.Size = new System.Drawing.Size(60, 17);
            this.rdoTertiary.TabIndex = 4;
            this.rdoTertiary.Text = "Tertiary";
            this.rdoTertiary.CheckedChanged += new System.EventHandler(this.rdoTertiary_CheckedChanged);
            // 
            // rdoSecondary
            // 
            this.rdoSecondary.AutoSize = true;
            this.rdoSecondary.Location = new System.Drawing.Point(93, 20);
            this.rdoSecondary.Name = "rdoSecondary";
            this.rdoSecondary.Size = new System.Drawing.Size(76, 17);
            this.rdoSecondary.TabIndex = 3;
            this.rdoSecondary.Text = "Secondary";
            this.rdoSecondary.CheckedChanged += new System.EventHandler(this.rdoSecondary_CheckedChanged);
            // 
            // rdoMain
            // 
            this.rdoMain.AutoSize = true;
            this.rdoMain.Checked = true;
            this.rdoMain.Location = new System.Drawing.Point(11, 20);
            this.rdoMain.Name = "rdoMain";
            this.rdoMain.Size = new System.Drawing.Size(48, 17);
            this.rdoMain.TabIndex = 1;
            this.rdoMain.TabStop = true;
            this.rdoMain.Text = "Main";
            this.rdoMain.CheckedChanged += new System.EventHandler(this.rdoMain_CheckedChanged);
            // 
            // btnUnselectItem
            // 
            this.btnUnselectItem.Location = new System.Drawing.Point(191, 275);
            this.btnUnselectItem.Name = "btnUnselectItem";
            this.btnUnselectItem.Padding = new System.Windows.Forms.Padding(5);
            this.btnUnselectItem.Size = new System.Drawing.Size(18, 20);
            this.btnUnselectItem.TabIndex = 35;
            this.btnUnselectItem.Text = "X";
            this.btnUnselectItem.Click += new System.EventHandler(this.btnUnselectItem_Click);
            // 
            // grpTypes
            // 
            this.grpTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTypes.Controls.Add(this.rdoTable);
            this.grpTypes.Controls.Add(this.rdoItem);
            this.grpTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTypes.Location = new System.Drawing.Point(14, 20);
            this.grpTypes.Name = "grpTypes";
            this.grpTypes.Size = new System.Drawing.Size(195, 42);
            this.grpTypes.TabIndex = 59;
            this.grpTypes.TabStop = false;
            this.grpTypes.Text = "Drop Type";
            // 
            // rdoTable
            // 
            this.rdoTable.AutoSize = true;
            this.rdoTable.Location = new System.Drawing.Point(93, 20);
            this.rdoTable.Name = "rdoTable";
            this.rdoTable.Size = new System.Drawing.Size(76, 17);
            this.rdoTable.TabIndex = 3;
            this.rdoTable.Text = "Loot Table";
            this.rdoTable.CheckedChanged += new System.EventHandler(this.rdoTable_CheckedChanged);
            // 
            // rdoItem
            // 
            this.rdoItem.AutoSize = true;
            this.rdoItem.Checked = true;
            this.rdoItem.Location = new System.Drawing.Point(11, 20);
            this.rdoItem.Name = "rdoItem";
            this.rdoItem.Size = new System.Drawing.Size(45, 17);
            this.rdoItem.TabIndex = 1;
            this.rdoItem.TabStop = true;
            this.rdoItem.Text = "Item";
            this.rdoItem.CheckedChanged += new System.EventHandler(this.rdoItem_CheckedChanged);
            // 
            // chkIndividualLoot
            // 
            this.chkIndividualLoot.AutoSize = true;
            this.chkIndividualLoot.Location = new System.Drawing.Point(14, 447);
            this.chkIndividualLoot.Name = "chkIndividualLoot";
            this.chkIndividualLoot.Size = new System.Drawing.Size(165, 17);
            this.chkIndividualLoot.TabIndex = 78;
            this.chkIndividualLoot.Text = "Spawn Loot for all Attackers?";
            this.chkIndividualLoot.CheckedChanged += new System.EventHandler(this.chkIndividualLoot_CheckedChanged);
            // 
            // btnDropRemove
            // 
            this.btnDropRemove.Location = new System.Drawing.Point(134, 417);
            this.btnDropRemove.Name = "btnDropRemove";
            this.btnDropRemove.Padding = new System.Windows.Forms.Padding(5);
            this.btnDropRemove.Size = new System.Drawing.Size(75, 23);
            this.btnDropRemove.TabIndex = 64;
            this.btnDropRemove.Text = "Remove";
            this.btnDropRemove.Click += new System.EventHandler(this.btnDropRemove_Click);
            // 
            // btnDropAdd
            // 
            this.btnDropAdd.Location = new System.Drawing.Point(14, 417);
            this.btnDropAdd.Name = "btnDropAdd";
            this.btnDropAdd.Padding = new System.Windows.Forms.Padding(5);
            this.btnDropAdd.Size = new System.Drawing.Size(75, 23);
            this.btnDropAdd.TabIndex = 63;
            this.btnDropAdd.Text = "Add";
            this.btnDropAdd.Click += new System.EventHandler(this.btnDropAdd_Click);
            // 
            // lstDrops
            // 
            this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstDrops.FormattingEnabled = true;
            this.lstDrops.Location = new System.Drawing.Point(14, 206);
            this.lstDrops.Name = "lstDrops";
            this.lstDrops.Size = new System.Drawing.Size(195, 67);
            this.lstDrops.TabIndex = 62;
            this.lstDrops.SelectedIndexChanged += new System.EventHandler(this.lstDrops_SelectedIndexChanged);
            // 
            // nudDropAmount
            // 
            this.nudDropAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDropAmount.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDropAmount.Location = new System.Drawing.Point(14, 345);
            this.nudDropAmount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudDropAmount.Name = "nudDropAmount";
            this.nudDropAmount.Size = new System.Drawing.Size(195, 20);
            this.nudDropAmount.TabIndex = 61;
            this.nudDropAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDropAmount.ValueChanged += new System.EventHandler(this.nudDropAmount_ValueChanged);
            // 
            // nudDropChance
            // 
            this.nudDropChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDropChance.DecimalPlaces = 2;
            this.nudDropChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDropChance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudDropChance.Location = new System.Drawing.Point(14, 388);
            this.nudDropChance.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDropChance.Name = "nudDropChance";
            this.nudDropChance.Size = new System.Drawing.Size(195, 20);
            this.nudDropChance.TabIndex = 60;
            this.nudDropChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDropChance.ValueChanged += new System.EventHandler(this.nudDropChance_ValueChanged);
            // 
            // cmbDropItem
            // 
            this.cmbDropItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDropItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDropItem.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDropItem.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDropItem.DrawDropdownHoverOutline = false;
            this.cmbDropItem.DrawFocusRectangle = false;
            this.cmbDropItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDropItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDropItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDropItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDropItem.FormattingEnabled = true;
            this.cmbDropItem.Location = new System.Drawing.Point(14, 301);
            this.cmbDropItem.Name = "cmbDropItem";
            this.cmbDropItem.Size = new System.Drawing.Size(195, 21);
            this.cmbDropItem.TabIndex = 17;
            this.cmbDropItem.Text = null;
            this.cmbDropItem.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDropItem.SelectedIndexChanged += new System.EventHandler(this.cmbDropItem_SelectedIndexChanged);
            // 
            // lblDropAmount
            // 
            this.lblDropAmount.AutoSize = true;
            this.lblDropAmount.Location = new System.Drawing.Point(13, 325);
            this.lblDropAmount.Name = "lblDropAmount";
            this.lblDropAmount.Size = new System.Drawing.Size(46, 13);
            this.lblDropAmount.TabIndex = 15;
            this.lblDropAmount.Text = "Amount:";
            // 
            // lblDropChance
            // 
            this.lblDropChance.AutoSize = true;
            this.lblDropChance.Location = new System.Drawing.Point(13, 372);
            this.lblDropChance.Name = "lblDropChance";
            this.lblDropChance.Size = new System.Drawing.Size(87, 13);
            this.lblDropChance.TabIndex = 13;
            this.lblDropChance.Text = "Chance (weight):";
            // 
            // lblDropItem
            // 
            this.lblDropItem.AutoSize = true;
            this.lblDropItem.Location = new System.Drawing.Point(11, 285);
            this.lblDropItem.Name = "lblDropItem";
            this.lblDropItem.Size = new System.Drawing.Size(30, 13);
            this.lblDropItem.TabIndex = 11;
            this.lblDropItem.Text = "Item:";
            // 
            // grpNpcVsNpc
            // 
            this.grpNpcVsNpc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpNpcVsNpc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNpcVsNpc.Controls.Add(this.chkCannotHeal);
            this.grpNpcVsNpc.Controls.Add(this.cmbHostileNPC);
            this.grpNpcVsNpc.Controls.Add(this.lblNPC);
            this.grpNpcVsNpc.Controls.Add(this.btnRemoveAggro);
            this.grpNpcVsNpc.Controls.Add(this.btnAddAggro);
            this.grpNpcVsNpc.Controls.Add(this.lstAggro);
            this.grpNpcVsNpc.Controls.Add(this.chkAttackAllies);
            this.grpNpcVsNpc.Controls.Add(this.chkEnabled);
            this.grpNpcVsNpc.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNpcVsNpc.Location = new System.Drawing.Point(4, 1060);
            this.grpNpcVsNpc.Name = "grpNpcVsNpc";
            this.grpNpcVsNpc.Size = new System.Drawing.Size(206, 308);
            this.grpNpcVsNpc.TabIndex = 29;
            this.grpNpcVsNpc.TabStop = false;
            this.grpNpcVsNpc.Text = "NPC vs NPC Combat/Hostility ";
            // 
            // chkCannotHeal
            // 
            this.chkCannotHeal.AutoSize = true;
            this.chkCannotHeal.Location = new System.Drawing.Point(9, 270);
            this.chkCannotHeal.Name = "chkCannotHeal";
            this.chkCannotHeal.Size = new System.Drawing.Size(116, 17);
            this.chkCannotHeal.TabIndex = 79;
            this.chkCannotHeal.Text = "Cannot be healed?";
            this.chkCannotHeal.CheckedChanged += new System.EventHandler(this.chkCannotHeal_CheckedChanged);
            // 
            // cmbHostileNPC
            // 
            this.cmbHostileNPC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbHostileNPC.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbHostileNPC.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbHostileNPC.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbHostileNPC.DrawDropdownHoverOutline = false;
            this.cmbHostileNPC.DrawFocusRectangle = false;
            this.cmbHostileNPC.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbHostileNPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHostileNPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbHostileNPC.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbHostileNPC.FormattingEnabled = true;
            this.cmbHostileNPC.Location = new System.Drawing.Point(9, 84);
            this.cmbHostileNPC.Name = "cmbHostileNPC";
            this.cmbHostileNPC.Size = new System.Drawing.Size(191, 21);
            this.cmbHostileNPC.TabIndex = 45;
            this.cmbHostileNPC.Text = null;
            this.cmbHostileNPC.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lblNPC
            // 
            this.lblNPC.AutoSize = true;
            this.lblNPC.Location = new System.Drawing.Point(6, 67);
            this.lblNPC.Name = "lblNPC";
            this.lblNPC.Size = new System.Drawing.Size(32, 13);
            this.lblNPC.TabIndex = 44;
            this.lblNPC.Text = "NPC:";
            // 
            // btnRemoveAggro
            // 
            this.btnRemoveAggro.Location = new System.Drawing.Point(125, 241);
            this.btnRemoveAggro.Name = "btnRemoveAggro";
            this.btnRemoveAggro.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveAggro.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAggro.TabIndex = 43;
            this.btnRemoveAggro.Text = "Remove";
            this.btnRemoveAggro.Click += new System.EventHandler(this.btnRemoveAggro_Click);
            // 
            // btnAddAggro
            // 
            this.btnAddAggro.Location = new System.Drawing.Point(9, 241);
            this.btnAddAggro.Name = "btnAddAggro";
            this.btnAddAggro.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddAggro.Size = new System.Drawing.Size(75, 23);
            this.btnAddAggro.TabIndex = 42;
            this.btnAddAggro.Text = "Add";
            this.btnAddAggro.Click += new System.EventHandler(this.btnAddAggro_Click);
            // 
            // lstAggro
            // 
            this.lstAggro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstAggro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstAggro.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstAggro.FormattingEnabled = true;
            this.lstAggro.Items.AddRange(new object[] {
            "NPC:"});
            this.lstAggro.Location = new System.Drawing.Point(9, 122);
            this.lstAggro.Name = "lstAggro";
            this.lstAggro.Size = new System.Drawing.Size(191, 106);
            this.lstAggro.TabIndex = 41;
            // 
            // chkAttackAllies
            // 
            this.chkAttackAllies.AutoSize = true;
            this.chkAttackAllies.Location = new System.Drawing.Point(8, 42);
            this.chkAttackAllies.Name = "chkAttackAllies";
            this.chkAttackAllies.Size = new System.Drawing.Size(90, 17);
            this.chkAttackAllies.TabIndex = 1;
            this.chkAttackAllies.Text = "Attack Allies?";
            this.chkAttackAllies.CheckedChanged += new System.EventHandler(this.chkAttackAllies_CheckedChanged);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(8, 19);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(71, 17);
            this.chkEnabled.TabIndex = 0;
            this.chkEnabled.Text = "Enabled?";
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // grpSpells
            // 
            this.grpSpells.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpells.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpells.Controls.Add(this.chkSequentialCasting);
            this.grpSpells.Controls.Add(this.chkNeverSkip);
            this.grpSpells.Controls.Add(this.cmbSpell);
            this.grpSpells.Controls.Add(this.cmbFreq);
            this.grpSpells.Controls.Add(this.lblFreq);
            this.grpSpells.Controls.Add(this.lblSpell);
            this.grpSpells.Controls.Add(this.btnRemove);
            this.grpSpells.Controls.Add(this.btnAdd);
            this.grpSpells.Controls.Add(this.lstSpells);
            this.grpSpells.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpells.Location = new System.Drawing.Point(3, 739);
            this.grpSpells.Name = "grpSpells";
            this.grpSpells.Size = new System.Drawing.Size(207, 309);
            this.grpSpells.TabIndex = 28;
            this.grpSpells.TabStop = false;
            this.grpSpells.Text = "Spells";
            // 
            // chkSequentialCasting
            // 
            this.chkSequentialCasting.AutoSize = true;
            this.chkSequentialCasting.Location = new System.Drawing.Point(12, 264);
            this.chkSequentialCasting.Name = "chkSequentialCasting";
            this.chkSequentialCasting.Size = new System.Drawing.Size(119, 17);
            this.chkSequentialCasting.TabIndex = 97;
            this.chkSequentialCasting.Text = "Sequential casting?";
            this.chkSequentialCasting.CheckedChanged += new System.EventHandler(this.chkSequentialCasting_CheckedChanged);
            // 
            // chkNeverSkip
            // 
            this.chkNeverSkip.AutoSize = true;
            this.chkNeverSkip.Location = new System.Drawing.Point(12, 241);
            this.chkNeverSkip.Name = "chkNeverSkip";
            this.chkNeverSkip.Size = new System.Drawing.Size(141, 17);
            this.chkNeverSkip.TabIndex = 96;
            this.chkNeverSkip.Text = "Never skip spellcasting?";
            this.chkNeverSkip.CheckedChanged += new System.EventHandler(this.chkNeverSkip_CheckedChanged);
            // 
            // cmbSpell
            // 
            this.cmbSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpell.DrawDropdownHoverOutline = false;
            this.cmbSpell.DrawFocusRectangle = false;
            this.cmbSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpell.FormattingEnabled = true;
            this.cmbSpell.Location = new System.Drawing.Point(13, 146);
            this.cmbSpell.Name = "cmbSpell";
            this.cmbSpell.Size = new System.Drawing.Size(179, 21);
            this.cmbSpell.TabIndex = 43;
            this.cmbSpell.Text = null;
            this.cmbSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSpell.SelectedIndexChanged += new System.EventHandler(this.cmbSpell_SelectedIndexChanged);
            // 
            // cmbFreq
            // 
            this.cmbFreq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFreq.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFreq.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFreq.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFreq.DrawDropdownHoverOutline = false;
            this.cmbFreq.DrawFocusRectangle = false;
            this.cmbFreq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFreq.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFreq.FormattingEnabled = true;
            this.cmbFreq.Items.AddRange(new object[] {
            "Not Very Often",
            "Not Often",
            "Normal",
            "Often",
            "Very Often"});
            this.cmbFreq.Location = new System.Drawing.Point(47, 208);
            this.cmbFreq.Name = "cmbFreq";
            this.cmbFreq.Size = new System.Drawing.Size(145, 21);
            this.cmbFreq.TabIndex = 42;
            this.cmbFreq.Text = "Not Very Often";
            this.cmbFreq.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFreq.SelectedIndexChanged += new System.EventHandler(this.cmbFreq_SelectedIndexChanged);
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(10, 211);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(31, 13);
            this.lblFreq.TabIndex = 41;
            this.lblFreq.Text = "Freq:";
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(10, 129);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(33, 13);
            this.lblSpell.TabIndex = 39;
            this.lblSpell.Text = "Spell:";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(117, 176);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 38;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 176);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(5);
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 37;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstSpells
            // 
            this.lstSpells.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstSpells.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSpells.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstSpells.FormattingEnabled = true;
            this.lstSpells.Location = new System.Drawing.Point(12, 15);
            this.lstSpells.Name = "lstSpells";
            this.lstSpells.Size = new System.Drawing.Size(180, 106);
            this.lstSpells.TabIndex = 29;
            this.lstSpells.SelectedIndexChanged += new System.EventHandler(this.lstSpells_SelectedIndexChanged);
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.chkAnimated);
            this.grpGeneral.Controls.Add(this.chkHideName);
            this.grpGeneral.Controls.Add(this.lblTier);
            this.grpGeneral.Controls.Add(this.nudTier);
            this.grpGeneral.Controls.Add(this.lblAlpha);
            this.grpGeneral.Controls.Add(this.lblBlue);
            this.grpGeneral.Controls.Add(this.lblGreen);
            this.grpGeneral.Controls.Add(this.lblRed);
            this.grpGeneral.Controls.Add(this.nudRgbaA);
            this.grpGeneral.Controls.Add(this.nudRgbaB);
            this.grpGeneral.Controls.Add(this.nudRgbaG);
            this.grpGeneral.Controls.Add(this.nudRgbaR);
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.cmbSprite);
            this.grpGeneral.Controls.Add(this.lblPic);
            this.grpGeneral.Controls.Add(this.picNpc);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(2, 1);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(207, 251);
            this.grpGeneral.TabIndex = 14;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // chkAnimated
            // 
            this.chkAnimated.AutoSize = true;
            this.chkAnimated.Location = new System.Drawing.Point(7, 218);
            this.chkAnimated.Name = "chkAnimated";
            this.chkAnimated.Size = new System.Drawing.Size(76, 17);
            this.chkAnimated.TabIndex = 82;
            this.chkAnimated.Text = "Animated?";
            this.chkAnimated.CheckedChanged += new System.EventHandler(this.chkAnimated_CheckedChanged);
            // 
            // chkHideName
            // 
            this.chkHideName.AutoSize = true;
            this.chkHideName.Location = new System.Drawing.Point(110, 96);
            this.chkHideName.Name = "chkHideName";
            this.chkHideName.Size = new System.Drawing.Size(85, 17);
            this.chkHideName.TabIndex = 81;
            this.chkHideName.Text = "Hide Name?";
            this.chkHideName.CheckedChanged += new System.EventHandler(this.darkCheckBox1_CheckedChanged);
            // 
            // lblTier
            // 
            this.lblTier.AutoSize = true;
            this.lblTier.Location = new System.Drawing.Point(56, 76);
            this.lblTier.Name = "lblTier";
            this.lblTier.Size = new System.Drawing.Size(25, 13);
            this.lblTier.TabIndex = 80;
            this.lblTier.Text = "Tier";
            // 
            // nudTier
            // 
            this.nudTier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTier.Location = new System.Drawing.Point(87, 74);
            this.nudTier.Name = "nudTier";
            this.nudTier.Size = new System.Drawing.Size(108, 20);
            this.nudTier.TabIndex = 79;
            this.nudTier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTier.ValueChanged += new System.EventHandler(this.nudTier_ValueChanged);
            // 
            // lblAlpha
            // 
            this.lblAlpha.AutoSize = true;
            this.lblAlpha.Location = new System.Drawing.Point(101, 228);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(37, 13);
            this.lblAlpha.TabIndex = 78;
            this.lblAlpha.Text = "Alpha:";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(107, 202);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(31, 13);
            this.lblBlue.TabIndex = 77;
            this.lblBlue.Text = "Blue:";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(99, 176);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(39, 13);
            this.lblGreen.TabIndex = 76;
            this.lblGreen.Text = "Green:";
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(108, 150);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(30, 13);
            this.lblRed.TabIndex = 75;
            this.lblRed.Text = "Red:";
            // 
            // nudRgbaA
            // 
            this.nudRgbaA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaA.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaA.Location = new System.Drawing.Point(153, 221);
            this.nudRgbaA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaA.Name = "nudRgbaA";
            this.nudRgbaA.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaA.TabIndex = 74;
            this.nudRgbaA.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaA.ValueChanged += new System.EventHandler(this.nudRgbaA_ValueChanged);
            // 
            // nudRgbaB
            // 
            this.nudRgbaB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaB.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaB.Location = new System.Drawing.Point(153, 195);
            this.nudRgbaB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaB.Name = "nudRgbaB";
            this.nudRgbaB.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaB.TabIndex = 73;
            this.nudRgbaB.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaB.ValueChanged += new System.EventHandler(this.nudRgbaB_ValueChanged);
            // 
            // nudRgbaG
            // 
            this.nudRgbaG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaG.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaG.Location = new System.Drawing.Point(153, 169);
            this.nudRgbaG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaG.Name = "nudRgbaG";
            this.nudRgbaG.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaG.TabIndex = 72;
            this.nudRgbaG.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaG.ValueChanged += new System.EventHandler(this.nudRgbaG_ValueChanged);
            // 
            // nudRgbaR
            // 
            this.nudRgbaR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaR.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaR.Location = new System.Drawing.Point(153, 143);
            this.nudRgbaR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaR.Name = "nudRgbaR";
            this.nudRgbaR.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaR.TabIndex = 71;
            this.nudRgbaR.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaR.ValueChanged += new System.EventHandler(this.nudRgbaR_ValueChanged);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(177, 45);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 67;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 49);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 66;
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
            this.cmbFolder.Location = new System.Drawing.Point(60, 45);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(113, 21);
            this.cmbFolder.TabIndex = 65;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // cmbSprite
            // 
            this.cmbSprite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSprite.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSprite.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSprite.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSprite.DrawDropdownHoverOutline = false;
            this.cmbSprite.DrawFocusRectangle = false;
            this.cmbSprite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSprite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSprite.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSprite.FormattingEnabled = true;
            this.cmbSprite.Items.AddRange(new object[] {
            "None"});
            this.cmbSprite.Location = new System.Drawing.Point(6, 116);
            this.cmbSprite.Name = "cmbSprite";
            this.cmbSprite.Size = new System.Drawing.Size(189, 21);
            this.cmbSprite.TabIndex = 11;
            this.cmbSprite.Text = "None";
            this.cmbSprite.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSprite.SelectedIndexChanged += new System.EventHandler(this.cmbSprite_SelectedIndexChanged);
            // 
            // lblPic
            // 
            this.lblPic.AutoSize = true;
            this.lblPic.Location = new System.Drawing.Point(7, 99);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(37, 13);
            this.lblPic.TabIndex = 6;
            this.lblPic.Text = "Sprite:";
            // 
            // picNpc
            // 
            this.picNpc.BackColor = System.Drawing.Color.Black;
            this.picNpc.Location = new System.Drawing.Point(6, 147);
            this.picNpc.Name = "picNpc";
            this.picNpc.Size = new System.Drawing.Size(64, 64);
            this.picNpc.TabIndex = 4;
            this.picNpc.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtName.Location = new System.Drawing.Point(60, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(135, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // grpStats
            // 
            this.grpStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStats.Controls.Add(this.btnUseCalcExp);
            this.grpStats.Controls.Add(this.lblCalcExpVal);
            this.grpStats.Controls.Add(this.lblCalcExp);
            this.grpStats.Controls.Add(this.nudEvasion);
            this.grpStats.Controls.Add(this.nudAccuracy);
            this.grpStats.Controls.Add(this.label5);
            this.grpStats.Controls.Add(this.label6);
            this.grpStats.Controls.Add(this.nudPierceResist);
            this.grpStats.Controls.Add(this.nudPierce);
            this.grpStats.Controls.Add(this.label3);
            this.grpStats.Controls.Add(this.label4);
            this.grpStats.Controls.Add(this.nudSlashResist);
            this.grpStats.Controls.Add(this.nudSlash);
            this.grpStats.Controls.Add(this.label1);
            this.grpStats.Controls.Add(this.label2);
            this.grpStats.Controls.Add(this.nudExp);
            this.grpStats.Controls.Add(this.nudMana);
            this.grpStats.Controls.Add(this.nudHp);
            this.grpStats.Controls.Add(this.nudSpd);
            this.grpStats.Controls.Add(this.nudMR);
            this.grpStats.Controls.Add(this.nudDef);
            this.grpStats.Controls.Add(this.nudMag);
            this.grpStats.Controls.Add(this.nudStr);
            this.grpStats.Controls.Add(this.lblSpd);
            this.grpStats.Controls.Add(this.lblMR);
            this.grpStats.Controls.Add(this.lblDef);
            this.grpStats.Controls.Add(this.lblMag);
            this.grpStats.Controls.Add(this.lblStr);
            this.grpStats.Controls.Add(this.lblMana);
            this.grpStats.Controls.Add(this.lblHP);
            this.grpStats.Controls.Add(this.lblExp);
            this.grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStats.Location = new System.Drawing.Point(3, 253);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(206, 381);
            this.grpStats.TabIndex = 15;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Stats:";
            // 
            // btnUseCalcExp
            // 
            this.btnUseCalcExp.Location = new System.Drawing.Point(99, 353);
            this.btnUseCalcExp.Name = "btnUseCalcExp";
            this.btnUseCalcExp.Padding = new System.Windows.Forms.Padding(5);
            this.btnUseCalcExp.Size = new System.Drawing.Size(95, 26);
            this.btnUseCalcExp.TabIndex = 163;
            this.btnUseCalcExp.Text = "Use Calculated";
            this.btnUseCalcExp.Click += new System.EventHandler(this.btnUseCalcExp_Click);
            // 
            // lblCalcExpVal
            // 
            this.lblCalcExpVal.AutoSize = true;
            this.lblCalcExpVal.Location = new System.Drawing.Point(100, 324);
            this.lblCalcExpVal.Name = "lblCalcExpVal";
            this.lblCalcExpVal.Size = new System.Drawing.Size(37, 13);
            this.lblCalcExpVal.TabIndex = 59;
            this.lblCalcExpVal.Text = "0 EXP";
            // 
            // lblCalcExp
            // 
            this.lblCalcExp.AutoSize = true;
            this.lblCalcExp.Location = new System.Drawing.Point(13, 324);
            this.lblCalcExp.Name = "lblCalcExp";
            this.lblCalcExp.Size = new System.Drawing.Size(81, 13);
            this.lblCalcExp.TabIndex = 58;
            this.lblCalcExp.Text = "Calculated Exp:";
            // 
            // nudEvasion
            // 
            this.nudEvasion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEvasion.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEvasion.Location = new System.Drawing.Point(106, 253);
            this.nudEvasion.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEvasion.Name = "nudEvasion";
            this.nudEvasion.Size = new System.Drawing.Size(79, 20);
            this.nudEvasion.TabIndex = 57;
            this.nudEvasion.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEvasion.ValueChanged += new System.EventHandler(this.nudEvasion_ValueChanged);
            // 
            // nudAccuracy
            // 
            this.nudAccuracy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAccuracy.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAccuracy.Location = new System.Drawing.Point(15, 253);
            this.nudAccuracy.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAccuracy.Name = "nudAccuracy";
            this.nudAccuracy.Size = new System.Drawing.Size(77, 20);
            this.nudAccuracy.TabIndex = 56;
            this.nudAccuracy.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAccuracy.ValueChanged += new System.EventHandler(this.nudAccuracy_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 237);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "Evasion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 237);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Accuracy";
            // 
            // nudPierceResist
            // 
            this.nudPierceResist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierceResist.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierceResist.Location = new System.Drawing.Point(107, 166);
            this.nudPierceResist.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierceResist.Name = "nudPierceResist";
            this.nudPierceResist.Size = new System.Drawing.Size(79, 20);
            this.nudPierceResist.TabIndex = 53;
            this.nudPierceResist.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierceResist.ValueChanged += new System.EventHandler(this.nudPierceResist_ValueChanged);
            // 
            // nudPierce
            // 
            this.nudPierce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierce.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierce.Location = new System.Drawing.Point(16, 166);
            this.nudPierce.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierce.Name = "nudPierce";
            this.nudPierce.Size = new System.Drawing.Size(77, 20);
            this.nudPierce.TabIndex = 52;
            this.nudPierce.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierce.ValueChanged += new System.EventHandler(this.nudPierce_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Pierce Resist";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Pierce Attack";
            // 
            // nudSlashResist
            // 
            this.nudSlashResist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashResist.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashResist.Location = new System.Drawing.Point(106, 120);
            this.nudSlashResist.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlashResist.Name = "nudSlashResist";
            this.nudSlashResist.Size = new System.Drawing.Size(79, 20);
            this.nudSlashResist.TabIndex = 49;
            this.nudSlashResist.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashResist.ValueChanged += new System.EventHandler(this.nudSlashResist_ValueChanged);
            // 
            // nudSlash
            // 
            this.nudSlash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlash.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlash.Location = new System.Drawing.Point(15, 120);
            this.nudSlash.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlash.Name = "nudSlash";
            this.nudSlash.Size = new System.Drawing.Size(77, 20);
            this.nudSlash.TabIndex = 48;
            this.nudSlash.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlash.ValueChanged += new System.EventHandler(this.nudSlash_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Slash Resist";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Slash Atk";
            // 
            // nudExp
            // 
            this.nudExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudExp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudExp.Location = new System.Drawing.Point(16, 355);
            this.nudExp.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudExp.Name = "nudExp";
            this.nudExp.Size = new System.Drawing.Size(77, 20);
            this.nudExp.TabIndex = 45;
            this.nudExp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudExp.ValueChanged += new System.EventHandler(this.nudExp_ValueChanged);
            // 
            // nudMana
            // 
            this.nudMana.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMana.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMana.Location = new System.Drawing.Point(105, 35);
            this.nudMana.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudMana.Name = "nudMana";
            this.nudMana.Size = new System.Drawing.Size(77, 20);
            this.nudMana.TabIndex = 44;
            this.nudMana.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMana.ValueChanged += new System.EventHandler(this.nudMana_ValueChanged);
            // 
            // nudHp
            // 
            this.nudHp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHp.Location = new System.Drawing.Point(12, 35);
            this.nudHp.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHp.Name = "nudHp";
            this.nudHp.Size = new System.Drawing.Size(77, 20);
            this.nudHp.TabIndex = 43;
            this.nudHp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHp.ValueChanged += new System.EventHandler(this.nudHp_ValueChanged);
            // 
            // nudSpd
            // 
            this.nudSpd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpd.Location = new System.Drawing.Point(15, 295);
            this.nudSpd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpd.Name = "nudSpd";
            this.nudSpd.Size = new System.Drawing.Size(77, 20);
            this.nudSpd.TabIndex = 42;
            this.nudSpd.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpd.ValueChanged += new System.EventHandler(this.nudSpd_ValueChanged);
            // 
            // nudMR
            // 
            this.nudMR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMR.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMR.Location = new System.Drawing.Point(105, 208);
            this.nudMR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMR.Name = "nudMR";
            this.nudMR.Size = new System.Drawing.Size(79, 20);
            this.nudMR.TabIndex = 41;
            this.nudMR.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMR.ValueChanged += new System.EventHandler(this.nudMR_ValueChanged);
            // 
            // nudDef
            // 
            this.nudDef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDef.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDef.Location = new System.Drawing.Point(105, 76);
            this.nudDef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDef.Name = "nudDef";
            this.nudDef.Size = new System.Drawing.Size(79, 20);
            this.nudDef.TabIndex = 40;
            this.nudDef.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDef.ValueChanged += new System.EventHandler(this.nudDef_ValueChanged);
            // 
            // nudMag
            // 
            this.nudMag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMag.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMag.Location = new System.Drawing.Point(14, 208);
            this.nudMag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMag.Name = "nudMag";
            this.nudMag.Size = new System.Drawing.Size(77, 20);
            this.nudMag.TabIndex = 39;
            this.nudMag.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMag.ValueChanged += new System.EventHandler(this.nudMag_ValueChanged);
            // 
            // nudStr
            // 
            this.nudStr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStr.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStr.Location = new System.Drawing.Point(13, 76);
            this.nudStr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(77, 20);
            this.nudStr.TabIndex = 38;
            this.nudStr.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStr.ValueChanged += new System.EventHandler(this.nudStr_ValueChanged);
            // 
            // lblSpd
            // 
            this.lblSpd.AutoSize = true;
            this.lblSpd.Location = new System.Drawing.Point(13, 279);
            this.lblSpd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpd.Name = "lblSpd";
            this.lblSpd.Size = new System.Drawing.Size(38, 13);
            this.lblSpd.TabIndex = 37;
            this.lblSpd.Text = "Speed";
            this.lblSpd.Click += new System.EventHandler(this.lblSpd_Click);
            // 
            // lblMR
            // 
            this.lblMR.AutoSize = true;
            this.lblMR.Location = new System.Drawing.Point(103, 192);
            this.lblMR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMR.Name = "lblMR";
            this.lblMR.Size = new System.Drawing.Size(71, 13);
            this.lblMR.TabIndex = 36;
            this.lblMR.Text = "Magic Resist:";
            // 
            // lblDef
            // 
            this.lblDef.AutoSize = true;
            this.lblDef.Location = new System.Drawing.Point(103, 60);
            this.lblDef.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDef.Name = "lblDef";
            this.lblDef.Size = new System.Drawing.Size(63, 13);
            this.lblDef.TabIndex = 35;
            this.lblDef.Text = "Blunt Resist";
            // 
            // lblMag
            // 
            this.lblMag.AutoSize = true;
            this.lblMag.Location = new System.Drawing.Point(11, 192);
            this.lblMag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMag.Name = "lblMag";
            this.lblMag.Size = new System.Drawing.Size(39, 13);
            this.lblMag.TabIndex = 34;
            this.lblMag.Text = "Magic:";
            // 
            // lblStr
            // 
            this.lblStr.AutoSize = true;
            this.lblStr.Location = new System.Drawing.Point(9, 60);
            this.lblStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStr.Name = "lblStr";
            this.lblStr.Size = new System.Drawing.Size(50, 13);
            this.lblStr.TabIndex = 33;
            this.lblStr.Text = "Blunt Atk";
            // 
            // lblMana
            // 
            this.lblMana.AutoSize = true;
            this.lblMana.Location = new System.Drawing.Point(103, 19);
            this.lblMana.Name = "lblMana";
            this.lblMana.Size = new System.Drawing.Size(37, 13);
            this.lblMana.TabIndex = 15;
            this.lblMana.Text = "Mana:";
            // 
            // lblHP
            // 
            this.lblHP.AutoSize = true;
            this.lblHP.Location = new System.Drawing.Point(10, 19);
            this.lblHP.Name = "lblHP";
            this.lblHP.Size = new System.Drawing.Size(25, 13);
            this.lblHP.TabIndex = 14;
            this.lblHP.Text = "HP:";
            // 
            // lblExp
            // 
            this.lblExp.AutoSize = true;
            this.lblExp.Location = new System.Drawing.Point(13, 339);
            this.lblExp.Name = "lblExp";
            this.lblExp.Size = new System.Drawing.Size(28, 13);
            this.lblExp.TabIndex = 11;
            this.lblExp.Text = "Exp:";
            // 
            // grpBalanceHelp
            // 
            this.grpBalanceHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBalanceHelp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBalanceHelp.Controls.Add(this.btnHpXl);
            this.grpBalanceHelp.Controls.Add(this.btnHpHi);
            this.grpBalanceHelp.Controls.Add(this.btnHpMed);
            this.grpBalanceHelp.Controls.Add(this.btnHpLo);
            this.grpBalanceHelp.Controls.Add(this.lblHpHelp);
            this.grpBalanceHelp.Controls.Add(this.btnMagHi);
            this.grpBalanceHelp.Controls.Add(this.btnMagMed);
            this.grpBalanceHelp.Controls.Add(this.btnMagLo);
            this.grpBalanceHelp.Controls.Add(this.label9);
            this.grpBalanceHelp.Controls.Add(this.btnPierceHi);
            this.grpBalanceHelp.Controls.Add(this.btnPierceMed);
            this.grpBalanceHelp.Controls.Add(this.btnPierceLo);
            this.grpBalanceHelp.Controls.Add(this.label8);
            this.grpBalanceHelp.Controls.Add(this.btnSlashHi);
            this.grpBalanceHelp.Controls.Add(this.btnSlashMed);
            this.grpBalanceHelp.Controls.Add(this.btnSlashLo);
            this.grpBalanceHelp.Controls.Add(this.lblSlashResHelp);
            this.grpBalanceHelp.Controls.Add(this.btnBluntHigh);
            this.grpBalanceHelp.Controls.Add(this.btnBluntMed);
            this.grpBalanceHelp.Controls.Add(this.btnBluntLo);
            this.grpBalanceHelp.Controls.Add(this.lblPierceResRecc);
            this.grpBalanceHelp.Controls.Add(this.lblMaxHitVal);
            this.grpBalanceHelp.Controls.Add(this.lblMaxHit);
            this.grpBalanceHelp.Controls.Add(this.lblProjectedDpsVal);
            this.grpBalanceHelp.Controls.Add(this.lblTargetDpsVal);
            this.grpBalanceHelp.Controls.Add(this.cmbTier);
            this.grpBalanceHelp.Controls.Add(this.lblTierView);
            this.grpBalanceHelp.Controls.Add(this.lblTargetDps);
            this.grpBalanceHelp.Controls.Add(this.lblProjectedDps);
            this.grpBalanceHelp.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBalanceHelp.Location = new System.Drawing.Point(929, 38);
            this.grpBalanceHelp.Margin = new System.Windows.Forms.Padding(2);
            this.grpBalanceHelp.Name = "grpBalanceHelp";
            this.grpBalanceHelp.Padding = new System.Windows.Forms.Padding(2);
            this.grpBalanceHelp.Size = new System.Drawing.Size(228, 266);
            this.grpBalanceHelp.TabIndex = 32;
            this.grpBalanceHelp.TabStop = false;
            this.grpBalanceHelp.Text = "Balance Builder";
            // 
            // btnHpXl
            // 
            this.btnHpXl.Location = new System.Drawing.Point(107, 211);
            this.btnHpXl.Name = "btnHpXl";
            this.btnHpXl.Padding = new System.Windows.Forms.Padding(5);
            this.btnHpXl.Size = new System.Drawing.Size(25, 26);
            this.btnHpXl.TabIndex = 163;
            this.btnHpXl.Text = "B";
            this.btnHpXl.Click += new System.EventHandler(this.btnHpXl_Click);
            // 
            // btnHpHi
            // 
            this.btnHpHi.Location = new System.Drawing.Point(76, 211);
            this.btnHpHi.Name = "btnHpHi";
            this.btnHpHi.Padding = new System.Windows.Forms.Padding(5);
            this.btnHpHi.Size = new System.Drawing.Size(25, 26);
            this.btnHpHi.TabIndex = 162;
            this.btnHpHi.Text = "H";
            this.btnHpHi.Click += new System.EventHandler(this.btnHpHi_Click);
            // 
            // btnHpMed
            // 
            this.btnHpMed.Location = new System.Drawing.Point(45, 211);
            this.btnHpMed.Name = "btnHpMed";
            this.btnHpMed.Padding = new System.Windows.Forms.Padding(5);
            this.btnHpMed.Size = new System.Drawing.Size(25, 26);
            this.btnHpMed.TabIndex = 161;
            this.btnHpMed.Text = "M";
            this.btnHpMed.Click += new System.EventHandler(this.btnHpMed_Click);
            // 
            // btnHpLo
            // 
            this.btnHpLo.Location = new System.Drawing.Point(14, 211);
            this.btnHpLo.Name = "btnHpLo";
            this.btnHpLo.Padding = new System.Windows.Forms.Padding(5);
            this.btnHpLo.Size = new System.Drawing.Size(25, 26);
            this.btnHpLo.TabIndex = 160;
            this.btnHpLo.Text = "L";
            this.btnHpLo.Click += new System.EventHandler(this.btnHpLo_Click);
            // 
            // lblHpHelp
            // 
            this.lblHpHelp.AutoSize = true;
            this.lblHpHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHpHelp.Location = new System.Drawing.Point(13, 195);
            this.lblHpHelp.Name = "lblHpHelp";
            this.lblHpHelp.Size = new System.Drawing.Size(24, 13);
            this.lblHpHelp.TabIndex = 159;
            this.lblHpHelp.Text = "HP";
            // 
            // btnMagHi
            // 
            this.btnMagHi.Location = new System.Drawing.Point(182, 156);
            this.btnMagHi.Name = "btnMagHi";
            this.btnMagHi.Padding = new System.Windows.Forms.Padding(5);
            this.btnMagHi.Size = new System.Drawing.Size(25, 26);
            this.btnMagHi.TabIndex = 158;
            this.btnMagHi.Text = "H";
            this.btnMagHi.Click += new System.EventHandler(this.btnMagHi_Click);
            // 
            // btnMagMed
            // 
            this.btnMagMed.Location = new System.Drawing.Point(151, 156);
            this.btnMagMed.Name = "btnMagMed";
            this.btnMagMed.Padding = new System.Windows.Forms.Padding(5);
            this.btnMagMed.Size = new System.Drawing.Size(25, 26);
            this.btnMagMed.TabIndex = 157;
            this.btnMagMed.Text = "M";
            this.btnMagMed.Click += new System.EventHandler(this.btnMagMed_Click);
            // 
            // btnMagLo
            // 
            this.btnMagLo.Location = new System.Drawing.Point(120, 156);
            this.btnMagLo.Name = "btnMagLo";
            this.btnMagLo.Padding = new System.Windows.Forms.Padding(5);
            this.btnMagLo.Size = new System.Drawing.Size(25, 26);
            this.btnMagLo.TabIndex = 156;
            this.btnMagLo.Text = "L";
            this.btnMagLo.Click += new System.EventHandler(this.btnMagLo_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(116, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 155;
            this.label9.Text = "Magic Res";
            // 
            // btnPierceHi
            // 
            this.btnPierceHi.Location = new System.Drawing.Point(76, 156);
            this.btnPierceHi.Name = "btnPierceHi";
            this.btnPierceHi.Padding = new System.Windows.Forms.Padding(5);
            this.btnPierceHi.Size = new System.Drawing.Size(25, 26);
            this.btnPierceHi.TabIndex = 154;
            this.btnPierceHi.Text = "H";
            this.btnPierceHi.Click += new System.EventHandler(this.btnPierceHi_Click);
            // 
            // btnPierceMed
            // 
            this.btnPierceMed.Location = new System.Drawing.Point(45, 156);
            this.btnPierceMed.Name = "btnPierceMed";
            this.btnPierceMed.Padding = new System.Windows.Forms.Padding(5);
            this.btnPierceMed.Size = new System.Drawing.Size(25, 26);
            this.btnPierceMed.TabIndex = 153;
            this.btnPierceMed.Text = "M";
            this.btnPierceMed.Click += new System.EventHandler(this.btnPierceMed_Click);
            // 
            // btnPierceLo
            // 
            this.btnPierceLo.Location = new System.Drawing.Point(14, 156);
            this.btnPierceLo.Name = "btnPierceLo";
            this.btnPierceLo.Padding = new System.Windows.Forms.Padding(5);
            this.btnPierceLo.Size = new System.Drawing.Size(25, 26);
            this.btnPierceLo.TabIndex = 152;
            this.btnPierceLo.Text = "L";
            this.btnPierceLo.Click += new System.EventHandler(this.btnPierceLo_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 151;
            this.label8.Text = "Pierce Res";
            // 
            // btnSlashHi
            // 
            this.btnSlashHi.Location = new System.Drawing.Point(182, 102);
            this.btnSlashHi.Name = "btnSlashHi";
            this.btnSlashHi.Padding = new System.Windows.Forms.Padding(5);
            this.btnSlashHi.Size = new System.Drawing.Size(25, 26);
            this.btnSlashHi.TabIndex = 150;
            this.btnSlashHi.Text = "H";
            this.btnSlashHi.Click += new System.EventHandler(this.btnSlashHi_Click);
            // 
            // btnSlashMed
            // 
            this.btnSlashMed.Location = new System.Drawing.Point(151, 102);
            this.btnSlashMed.Name = "btnSlashMed";
            this.btnSlashMed.Padding = new System.Windows.Forms.Padding(5);
            this.btnSlashMed.Size = new System.Drawing.Size(25, 26);
            this.btnSlashMed.TabIndex = 149;
            this.btnSlashMed.Text = "M";
            this.btnSlashMed.Click += new System.EventHandler(this.btnSlashMed_Click);
            // 
            // btnSlashLo
            // 
            this.btnSlashLo.Location = new System.Drawing.Point(120, 102);
            this.btnSlashLo.Name = "btnSlashLo";
            this.btnSlashLo.Padding = new System.Windows.Forms.Padding(5);
            this.btnSlashLo.Size = new System.Drawing.Size(25, 26);
            this.btnSlashLo.TabIndex = 148;
            this.btnSlashLo.Text = "L";
            this.btnSlashLo.Click += new System.EventHandler(this.btnSlashLo_Click);
            // 
            // lblSlashResHelp
            // 
            this.lblSlashResHelp.AutoSize = true;
            this.lblSlashResHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlashResHelp.Location = new System.Drawing.Point(116, 86);
            this.lblSlashResHelp.Name = "lblSlashResHelp";
            this.lblSlashResHelp.Size = new System.Drawing.Size(64, 13);
            this.lblSlashResHelp.TabIndex = 147;
            this.lblSlashResHelp.Text = "Slash Res";
            // 
            // btnBluntHigh
            // 
            this.btnBluntHigh.Location = new System.Drawing.Point(76, 102);
            this.btnBluntHigh.Name = "btnBluntHigh";
            this.btnBluntHigh.Padding = new System.Windows.Forms.Padding(5);
            this.btnBluntHigh.Size = new System.Drawing.Size(25, 26);
            this.btnBluntHigh.TabIndex = 146;
            this.btnBluntHigh.Text = "H";
            this.btnBluntHigh.Click += new System.EventHandler(this.btnBluntHigh_Click);
            // 
            // btnBluntMed
            // 
            this.btnBluntMed.Location = new System.Drawing.Point(45, 102);
            this.btnBluntMed.Name = "btnBluntMed";
            this.btnBluntMed.Padding = new System.Windows.Forms.Padding(5);
            this.btnBluntMed.Size = new System.Drawing.Size(25, 26);
            this.btnBluntMed.TabIndex = 145;
            this.btnBluntMed.Text = "M";
            this.btnBluntMed.Click += new System.EventHandler(this.btnBluntMed_Click);
            // 
            // btnBluntLo
            // 
            this.btnBluntLo.Location = new System.Drawing.Point(14, 102);
            this.btnBluntLo.Name = "btnBluntLo";
            this.btnBluntLo.Padding = new System.Windows.Forms.Padding(5);
            this.btnBluntLo.Size = new System.Drawing.Size(25, 26);
            this.btnBluntLo.TabIndex = 144;
            this.btnBluntLo.Text = "L";
            this.btnBluntLo.Click += new System.EventHandler(this.btnBluntLo_Click);
            // 
            // lblPierceResRecc
            // 
            this.lblPierceResRecc.AutoSize = true;
            this.lblPierceResRecc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPierceResRecc.Location = new System.Drawing.Point(10, 86);
            this.lblPierceResRecc.Name = "lblPierceResRecc";
            this.lblPierceResRecc.Size = new System.Drawing.Size(62, 13);
            this.lblPierceResRecc.TabIndex = 142;
            this.lblPierceResRecc.Text = "Blunt Res";
            // 
            // lblMaxHitVal
            // 
            this.lblMaxHitVal.AutoSize = true;
            this.lblMaxHitVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHitVal.Location = new System.Drawing.Point(167, 61);
            this.lblMaxHitVal.Name = "lblMaxHitVal";
            this.lblMaxHitVal.Size = new System.Drawing.Size(18, 20);
            this.lblMaxHitVal.TabIndex = 136;
            this.lblMaxHitVal.Text = "0";
            // 
            // lblMaxHit
            // 
            this.lblMaxHit.AutoSize = true;
            this.lblMaxHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHit.Location = new System.Drawing.Point(166, 45);
            this.lblMaxHit.Name = "lblMaxHit";
            this.lblMaxHit.Size = new System.Drawing.Size(50, 13);
            this.lblMaxHit.TabIndex = 135;
            this.lblMaxHit.Text = "Max Hit";
            // 
            // lblProjectedDpsVal
            // 
            this.lblProjectedDpsVal.AutoSize = true;
            this.lblProjectedDpsVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectedDpsVal.Location = new System.Drawing.Point(89, 61);
            this.lblProjectedDpsVal.Name = "lblProjectedDpsVal";
            this.lblProjectedDpsVal.Size = new System.Drawing.Size(18, 20);
            this.lblProjectedDpsVal.TabIndex = 126;
            this.lblProjectedDpsVal.Text = "0";
            // 
            // lblTargetDpsVal
            // 
            this.lblTargetDpsVal.AutoSize = true;
            this.lblTargetDpsVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetDpsVal.Location = new System.Drawing.Point(12, 62);
            this.lblTargetDpsVal.Name = "lblTargetDpsVal";
            this.lblTargetDpsVal.Size = new System.Drawing.Size(18, 20);
            this.lblTargetDpsVal.TabIndex = 125;
            this.lblTargetDpsVal.Text = "0";
            // 
            // cmbTier
            // 
            this.cmbTier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTier.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTier.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTier.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTier.DrawDropdownHoverOutline = false;
            this.cmbTier.DrawFocusRectangle = false;
            this.cmbTier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTier.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTier.FormattingEnabled = true;
            this.cmbTier.Location = new System.Drawing.Point(74, 18);
            this.cmbTier.Name = "cmbTier";
            this.cmbTier.Size = new System.Drawing.Size(142, 21);
            this.cmbTier.TabIndex = 124;
            this.cmbTier.Text = null;
            this.cmbTier.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTier.SelectedIndexChanged += new System.EventHandler(this.cmbTier_SelectedIndexChanged);
            // 
            // lblTierView
            // 
            this.lblTierView.AutoSize = true;
            this.lblTierView.Location = new System.Drawing.Point(11, 21);
            this.lblTierView.Name = "lblTierView";
            this.lblTierView.Size = new System.Drawing.Size(46, 13);
            this.lblTierView.TabIndex = 123;
            this.lblTierView.Text = "For Tier:";
            // 
            // lblTargetDps
            // 
            this.lblTargetDps.AutoSize = true;
            this.lblTargetDps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetDps.Location = new System.Drawing.Point(9, 45);
            this.lblTargetDps.Name = "lblTargetDps";
            this.lblTargetDps.Size = new System.Drawing.Size(73, 13);
            this.lblTargetDps.TabIndex = 122;
            this.lblTargetDps.Text = "Target DPS";
            // 
            // lblProjectedDps
            // 
            this.lblProjectedDps.AutoSize = true;
            this.lblProjectedDps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectedDps.Location = new System.Drawing.Point(90, 45);
            this.lblProjectedDps.Name = "lblProjectedDps";
            this.lblProjectedDps.Size = new System.Drawing.Size(58, 13);
            this.lblProjectedDps.TabIndex = 121;
            this.lblProjectedDps.Text = "Est. DPS";
            // 
            // chkDisableAutoAttack
            // 
            this.chkDisableAutoAttack.AutoSize = true;
            this.chkDisableAutoAttack.Location = new System.Drawing.Point(13, 193);
            this.chkDisableAutoAttack.Name = "chkDisableAutoAttack";
            this.chkDisableAutoAttack.Size = new System.Drawing.Size(126, 17);
            this.chkDisableAutoAttack.TabIndex = 78;
            this.chkDisableAutoAttack.Text = "Disable Auto Attack?";
            this.chkDisableAutoAttack.CheckedChanged += new System.EventHandler(this.chkDisableAutoAttack_CheckedChanged);
            // 
            // chkDisableExhaustion
            // 
            this.chkDisableExhaustion.AutoSize = true;
            this.chkDisableExhaustion.Location = new System.Drawing.Point(13, 216);
            this.chkDisableExhaustion.Name = "chkDisableExhaustion";
            this.chkDisableExhaustion.Size = new System.Drawing.Size(147, 17);
            this.chkDisableExhaustion.TabIndex = 79;
            this.chkDisableExhaustion.Text = "Disable Auto Exhaustion?";
            this.chkDisableExhaustion.CheckedChanged += new System.EventHandler(this.chkDisableExhaustion_CheckedChanged);
            // 
            // FrmNpc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1187, 619);
            this.ControlBox = false;
            this.Controls.Add(this.grpBalanceHelp);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpNpcs);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmNpc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NPC Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNpc_FormClosed);
            this.Load += new System.EventHandler(this.frmNpc_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpNpcs.ResumeLayout(false);
            this.grpNpcs.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.grpChampionSettings.ResumeLayout(false);
            this.grpChampionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChampSpawnChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudChampCooldown)).EndInit();
            this.grpDynamicScaling.ResumeLayout(false);
            this.grpDynamicScaling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxScaledTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaledTo)).EndInit();
            this.grpBestiary.ResumeLayout(false);
            this.grpBestiary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKillCount)).EndInit();
            this.grpAttackOverrides.ResumeLayout(false);
            this.grpAttackOverrides.PerformLayout();
            this.grpDeathTransform.ResumeLayout(false);
            this.grpDeathTransform.PerformLayout();
            this.grpAnimation.ResumeLayout(false);
            this.grpAnimation.PerformLayout();
            this.grpImmunities.ResumeLayout(false);
            this.grpImmunities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTenacity)).EndInit();
            this.grpCombat.ResumeLayout(false);
            this.grpCombat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManasteal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLifesteal)).EndInit();
            this.grpDamageTypes.ResumeLayout(false);
            this.grpDamageTypes.PerformLayout();
            this.grpAttackSpeed.ResumeLayout(false);
            this.grpAttackSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).EndInit();
            this.grpCommonEvents.ResumeLayout(false);
            this.grpCommonEvents.PerformLayout();
            this.grpBehavior.ResumeLayout(false);
            this.grpBehavior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResetRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpawnDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlee)).EndInit();
            this.grpConditions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSightRange)).EndInit();
            this.grpRegen.ResumeLayout(false);
            this.grpRegen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHpRegen)).EndInit();
            this.grpDrops.ResumeLayout(false);
            this.grpDrops.PerformLayout();
            this.grpTableSelection.ResumeLayout(false);
            this.grpTableSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTableChance)).EndInit();
            this.grpTypes.ResumeLayout(false);
            this.grpTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDropChance)).EndInit();
            this.grpNpcVsNpc.ResumeLayout(false);
            this.grpNpcVsNpc.PerformLayout();
            this.grpSpells.ResumeLayout(false);
            this.grpSpells.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNpc)).EndInit();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            this.grpBalanceHelp.ResumeLayout(false);
            this.grpBalanceHelp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DarkGroupBox grpNpcs;
        private DarkGroupBox grpGeneral;
        private DarkComboBox cmbSprite;
        private System.Windows.Forms.Label lblSpawnDuration;
        private System.Windows.Forms.Label lblPic;
        private System.Windows.Forms.PictureBox picNpc;
        private System.Windows.Forms.Label lblName;
        private DarkTextBox txtName;
        private DarkGroupBox grpStats;
        private System.Windows.Forms.Label lblMana;
        private System.Windows.Forms.Label lblHP;
        private System.Windows.Forms.Label lblExp;
        private System.Windows.Forms.Label lblSightRange;
        private System.Windows.Forms.Panel pnlContainer;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private DarkGroupBox grpSpells;
        private DarkButton btnRemove;
        private DarkButton btnAdd;
        private System.Windows.Forms.ListBox lstSpells;
        private System.Windows.Forms.Label lblSpell;
        private DarkComboBox cmbFreq;
        private System.Windows.Forms.Label lblFreq;
        private DarkGroupBox grpNpcVsNpc;
        private System.Windows.Forms.Label lblNPC;
        private DarkButton btnRemoveAggro;
        private DarkButton btnAddAggro;
        private System.Windows.Forms.ListBox lstAggro;
        private DarkCheckBox chkAttackAllies;
        private DarkCheckBox chkEnabled;
        private DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton toolStripItemCopy;
        public System.Windows.Forms.ToolStripButton toolStripItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripItemUndo;
        private DarkGroupBox grpCombat;
        private System.Windows.Forms.Label lblCritChance;
        private DarkComboBox cmbAttackAnimation;
        private System.Windows.Forms.Label lblAttackAnimation;
        private DarkComboBox cmbHostileNPC;
        private DarkComboBox cmbSpell;
        private DarkNumericUpDown nudSpd;
        private DarkNumericUpDown nudMR;
        private DarkNumericUpDown nudDef;
        private DarkNumericUpDown nudMag;
        private DarkNumericUpDown nudStr;
        private System.Windows.Forms.Label lblSpd;
        private System.Windows.Forms.Label lblMR;
        private System.Windows.Forms.Label lblDef;
        private System.Windows.Forms.Label lblMag;
        private System.Windows.Forms.Label lblStr;
        private DarkNumericUpDown nudCritChance;
        private DarkNumericUpDown nudSightRange;
        private DarkNumericUpDown nudSpawnDuration;
        private DarkNumericUpDown nudMana;
        private DarkNumericUpDown nudHp;
        private DarkNumericUpDown nudExp;
        private DarkGroupBox grpDrops;
        private DarkButton btnDropRemove;
        private DarkButton btnDropAdd;
        private System.Windows.Forms.ListBox lstDrops;
        private DarkNumericUpDown nudDropAmount;
        private DarkNumericUpDown nudDropChance;
        private DarkComboBox cmbDropItem;
        private System.Windows.Forms.Label lblDropAmount;
        private System.Windows.Forms.Label lblDropChance;
        private System.Windows.Forms.Label lblDropItem;
        private DarkGroupBox grpRegen;
        private DarkNumericUpDown nudMpRegen;
        private DarkNumericUpDown nudHpRegen;
        private System.Windows.Forms.Label lblHpRegen;
        private System.Windows.Forms.Label lblManaRegen;
        private System.Windows.Forms.Label lblRegenHint;
        private DarkGroupBox grpCommonEvents;
        private DarkGroupBox grpBehavior;
        private DarkCheckBox chkSwarm;
        private DarkGroupBox grpConditions;
        private DarkButton btnAttackOnSightCond;
        private DarkButton btnPlayerCanAttackCond;
        private DarkButton btnPlayerFriendProtectorCond;
        private System.Windows.Forms.Label lblMovement;
        private DarkComboBox cmbMovement;
        private DarkCheckBox chkAggressive;
        private DarkComboBox cmbOnDeathEventParty;
        private System.Windows.Forms.Label lblOnDeathEventParty;
        private DarkComboBox cmbOnDeathEventKiller;
        private System.Windows.Forms.Label lblOnDeathEventKiller;
        private DarkNumericUpDown nudFlee;
        private System.Windows.Forms.Label lblFlee;
        private DarkCheckBox chkFocusDamageDealer;
        private DarkNumericUpDown nudCritMultiplier;
        private System.Windows.Forms.Label lblCritMultiplier;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkComboBox cmbFolder;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DarkGroupBox grpAttackSpeed;
        private DarkNumericUpDown nudAttackSpeedValue;
        private System.Windows.Forms.Label lblAttackSpeedValue;
        private DarkComboBox cmbAttackSpeedModifier;
        private System.Windows.Forms.Label lblAttackSpeedModifier;
        private DarkNumericUpDown nudRgbaA;
        private DarkNumericUpDown nudRgbaB;
        private DarkNumericUpDown nudRgbaG;
        private DarkNumericUpDown nudRgbaR;
        private System.Windows.Forms.Label lblAlpha;
        private System.Windows.Forms.Label lblBlue;
        private System.Windows.Forms.Label lblGreen;
        private System.Windows.Forms.Label lblRed;
        private DarkNumericUpDown nudResetRadius;
        private System.Windows.Forms.Label lblResetRadius;
        private DarkCheckBox chkIndividualLoot;
        private Controls.GameObjectList lstGameObjects;
        private DarkGroupBox grpImmunities;
        private DarkNumericUpDown nudTenacity;
        private System.Windows.Forms.Label lblTenacity;
        private DarkCheckBox chkTaunt;
        private DarkCheckBox chkSleep;
        private DarkCheckBox chkTransform;
        private DarkCheckBox chkBlind;
        private DarkCheckBox chkSnare;
        private DarkCheckBox chkStun;
        private DarkCheckBox chkSilence;
        private DarkCheckBox chkKnockback;
        private DarkGroupBox grpAnimation;
        private DarkComboBox cmbDeathAnimation;
        private System.Windows.Forms.Label lblDeathAnimation;
        private DarkGroupBox grpDeathTransform;
        private DarkComboBox cmbTransformIntoNpc;
        private System.Windows.Forms.Label lblDeathTransform;
        private DarkCheckBox chkStandStill;
        private DarkGroupBox grpAttackOverrides;
        private DarkComboBox cmbSpellAttackOverride;
        private System.Windows.Forms.Label lblAttackOverride;
        private DarkGroupBox grpTypes;
        private DarkRadioButton rdoTable;
        private DarkRadioButton rdoItem;
        private DarkButton btnUnselectItem;
        private DarkGroupBox grpTableSelection;
        private DarkNumericUpDown nudTableChance;
        private System.Windows.Forms.Label lblTableChance;
        private DarkRadioButton rdoTertiary;
        private DarkRadioButton rdoSecondary;
        private DarkRadioButton rdoMain;
        private DarkCheckBox chkConfused;
        private DarkCheckBox chkSlowed;
        private DarkNumericUpDown nudPierceResist;
        private DarkNumericUpDown nudPierce;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DarkNumericUpDown nudSlashResist;
        private DarkNumericUpDown nudSlash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DarkNumericUpDown nudEvasion;
        private DarkNumericUpDown nudAccuracy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DarkCheckBox chkDamageMagic;
        private DarkCheckBox chkDamagePierce;
        private DarkCheckBox chkDamageSlash;
        private DarkCheckBox chkDamageBlunt;
        private DarkGroupBox grpDamageTypes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private DarkGroupBox grpBestiary;
        private System.Windows.Forms.Label lblKillCount;
        private System.Windows.Forms.ListBox lstBestiaryUnlocks;
        private System.Windows.Forms.Label lblBestiary;
        private System.Windows.Forms.Label lblDescription;
        private DarkTextBox txtStartDesc;
        private DarkNumericUpDown nudKillCount;
        private DarkButton btnBestiaryDefaults;
        private DarkCheckBox chkBestiary;
        private System.Windows.Forms.Label lblCalcExpVal;
        private System.Windows.Forms.Label lblCalcExp;
        private DarkCheckBox chkImpassable;
        private DarkCheckBox chkNoBack;
        private DarkCheckBox chkStealth;
        private System.Windows.Forms.Label lblProjectedDps;
        private DarkGroupBox grpBalanceHelp;
        private DarkComboBox cmbTier;
        private System.Windows.Forms.Label lblTierView;
        private System.Windows.Forms.Label lblTargetDps;
        private System.Windows.Forms.Label lblTargetDpsVal;
        private System.Windows.Forms.Label lblProjectedDpsVal;
        private System.Windows.Forms.Label lblMaxHitVal;
        private System.Windows.Forms.Label lblMaxHit;
        private DarkButton btnSlashHi;
        private DarkButton btnSlashMed;
        private DarkButton btnSlashLo;
        private System.Windows.Forms.Label lblSlashResHelp;
        private DarkButton btnBluntHigh;
        private DarkButton btnBluntMed;
        private DarkButton btnBluntLo;
        private System.Windows.Forms.Label lblPierceResRecc;
        private DarkButton btnMagHi;
        private DarkButton btnMagMed;
        private DarkButton btnMagLo;
        private System.Windows.Forms.Label label9;
        private DarkButton btnPierceHi;
        private DarkButton btnPierceMed;
        private DarkButton btnPierceLo;
        private System.Windows.Forms.Label label8;
        private DarkButton btnUseCalcExp;
        private DarkButton btnHpHi;
        private DarkButton btnHpMed;
        private DarkButton btnHpLo;
        private System.Windows.Forms.Label lblHpHelp;
        private DarkButton btnHpXl;
        private System.Windows.Forms.Label lblTier;
        private DarkNumericUpDown nudTier;
        private DarkCheckBox chkSpellCast;
        private System.Windows.Forms.Label lblSpellcaster;
        private DarkCheckBox chkCannotHeal;
        private DarkGroupBox grpDynamicScaling;
        private DarkComboBox cmbScalingType;
        private System.Windows.Forms.Label lblScalingType;
        private DarkNumericUpDown nudScaleFactor;
        private System.Windows.Forms.Label lblScaleFactor;
        private DarkNumericUpDown nudScaledTo;
        private System.Windows.Forms.Label lblScaledTo;
        private DarkNumericUpDown nudMaxScaledTo;
        private System.Windows.Forms.Label lblMaxScaleTo;
        private DarkCheckBox chkPlayerLockLoot;
        private DarkGroupBox grpChampionSettings;
        private DarkNumericUpDown nudChampSpawnChance;
        private System.Windows.Forms.Label lblChampSpawnChance;
        private DarkNumericUpDown nudChampCooldown;
        private System.Windows.Forms.Label lblChampSpawnCooldown;
        private DarkComboBox cmbChampion;
        private System.Windows.Forms.Label label12;
        private DarkCheckBox chkIsChampion;
        private System.Windows.Forms.Label lblManasteal;
        private DarkNumericUpDown nudManasteal;
        private System.Windows.Forms.Label lblLifesteal;
        private DarkNumericUpDown nudLifesteal;
        private DarkCheckBox chkNeverSkip;
        private DarkCheckBox chkSpellCasting;
        private DarkCheckBox chkHideName;
        private DarkCheckBox chkAnimated;
        private DarkCheckBox chkSequentialCasting;
        private DarkCheckBox chkDisableAutoAttack;
        private DarkCheckBox chkDisableExhaustion;
    }
}