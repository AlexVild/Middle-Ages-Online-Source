using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmSpell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpell));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.grpNpcSpellChain = new DarkUI.Controls.DarkGroupBox();
            this.lblAoeChainDelay = new System.Windows.Forms.Label();
            this.nudAoeChainDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.lblChain = new System.Windows.Forms.Label();
            this.cmbAoeChain = new DarkUI.Controls.DarkComboBox();
            this.grpSpellGroup = new DarkUI.Controls.DarkGroupBox();
            this.lblSpellUpgrade = new System.Windows.Forms.Label();
            this.cmbSpellUpgrade = new DarkUI.Controls.DarkComboBox();
            this.btnSetEmpty = new DarkUI.Controls.DarkButton();
            this.btnAddSpellGroup = new DarkUI.Controls.DarkButton();
            this.cmbSpellGroup = new DarkUI.Controls.DarkComboBox();
            this.grpComponents = new DarkUI.Controls.DarkGroupBox();
            this.lblComponentQuantity = new System.Windows.Forms.Label();
            this.nudComponentQuantity = new DarkUI.Controls.DarkNumericUpDown();
            this.btnRemoveComponent = new DarkUI.Controls.DarkButton();
            this.btnAddComponent = new DarkUI.Controls.DarkButton();
            this.lblComponent = new System.Windows.Forms.Label();
            this.cmbComponents = new DarkUI.Controls.DarkComboBox();
            this.lstComponents = new System.Windows.Forms.ListBox();
            this.grpTargetInfo = new DarkUI.Controls.DarkGroupBox();
            this.nudMinRange = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMinRange = new System.Windows.Forms.Label();
            this.chkPersistMissedAttack = new DarkUI.Controls.DarkCheckBox();
            this.chkPersistSwap = new DarkUI.Controls.DarkCheckBox();
            this.cmbProjectile = new DarkUI.Controls.DarkComboBox();
            this.lblProjectile = new System.Windows.Forms.Label();
            this.lblTrapAnimation = new System.Windows.Forms.Label();
            this.cmbTrapAnimation = new DarkUI.Controls.DarkComboBox();
            this.nudDuration = new DarkUI.Controls.DarkNumericUpDown();
            this.lblDuration = new System.Windows.Forms.Label();
            this.nudHitRadius = new DarkUI.Controls.DarkNumericUpDown();
            this.lblHitRadius = new System.Windows.Forms.Label();
            this.cmbTargetType = new DarkUI.Controls.DarkComboBox();
            this.lblCastRange = new System.Windows.Forms.Label();
            this.lblTargetType = new System.Windows.Forms.Label();
            this.nudCastRange = new DarkUI.Controls.DarkNumericUpDown();
            this.grpDash = new DarkUI.Controls.DarkGroupBox();
            this.cmbDashAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblDashAnimation = new System.Windows.Forms.Label();
            this.cmbDashSpell = new DarkUI.Controls.DarkComboBox();
            this.lblDashSpell = new System.Windows.Forms.Label();
            this.grpDashCollisions = new DarkUI.Controls.DarkGroupBox();
            this.chkEntities = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreInactiveResources = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreZDimensionBlocks = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreMapBlocks = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreActiveResources = new DarkUI.Controls.DarkCheckBox();
            this.lblRange = new System.Windows.Forms.Label();
            this.scrlRange = new DarkUI.Controls.DarkScrollBar();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.btnAddFolder = new DarkUI.Controls.DarkButton();
            this.lblFolder = new System.Windows.Forms.Label();
            this.cmbFolder = new DarkUI.Controls.DarkComboBox();
            this.chkBound = new DarkUI.Controls.DarkCheckBox();
            this.cmbHitAnimation = new DarkUI.Controls.DarkComboBox();
            this.cmbCastAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new DarkUI.Controls.DarkTextBox();
            this.lblHitAnimation = new System.Windows.Forms.Label();
            this.lblCastAnimation = new System.Windows.Forms.Label();
            this.cmbSprite = new DarkUI.Controls.DarkComboBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.picSpell = new System.Windows.Forms.PictureBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.grpSpellCost = new DarkUI.Controls.DarkGroupBox();
            this.nudInterruptThreshold = new DarkUI.Controls.DarkNumericUpDown();
            this.lblInterruptThreshold = new System.Windows.Forms.Label();
            this.nudSkillPoints = new DarkUI.Controls.DarkNumericUpDown();
            this.lblSkillPts = new System.Windows.Forms.Label();
            this.chkIgnoreCdr = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreGlobalCooldown = new DarkUI.Controls.DarkCheckBox();
            this.btnAddCooldownGroup = new DarkUI.Controls.DarkButton();
            this.cmbCooldownGroup = new DarkUI.Controls.DarkComboBox();
            this.lblCooldownGroup = new System.Windows.Forms.Label();
            this.nudCooldownDuration = new DarkUI.Controls.DarkNumericUpDown();
            this.nudCastDuration = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMpCost = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHPCost = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMPCost = new System.Windows.Forms.Label();
            this.lblHPCost = new System.Windows.Forms.Label();
            this.lblCastDuration = new System.Windows.Forms.Label();
            this.lblCooldownDuration = new System.Windows.Forms.Label();
            this.grpRequirements = new DarkUI.Controls.DarkGroupBox();
            this.lblCannotCast = new System.Windows.Forms.Label();
            this.txtCannotCast = new DarkUI.Controls.DarkTextBox();
            this.btnDynamicRequirements = new DarkUI.Controls.DarkButton();
            this.grpCombat = new DarkUI.Controls.DarkGroupBox();
            this.grpAdvTrapSettings = new DarkUI.Controls.DarkGroupBox();
            this.nudTrapCooldown = new DarkUI.Controls.DarkNumericUpDown();
            this.lblTrapCooldown = new System.Windows.Forms.Label();
            this.chkNotSingleUseTrap = new DarkUI.Controls.DarkCheckBox();
            this.grpAreaDenial = new DarkUI.Controls.DarkGroupBox();
            this.grpAreaDenialSettings = new DarkUI.Controls.DarkGroupBox();
            this.nudAreaSpawnIntensity = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAreaDenialRadius = new System.Windows.Forms.Label();
            this.lblAreaDenialSpawnIntensity = new System.Windows.Forms.Label();
            this.nudAreaDenialRadius = new DarkUI.Controls.DarkNumericUpDown();
            this.chkAreaDenial = new DarkUI.Controls.DarkCheckBox();
            this.grpAdvAoe = new DarkUI.Controls.DarkGroupBox();
            this.grpRectangleSettings = new DarkUI.Controls.DarkGroupBox();
            this.lblRectLbl = new System.Windows.Forms.Label();
            this.lblAoeRectWidth = new System.Windows.Forms.Label();
            this.lblAoeRectHeight = new System.Windows.Forms.Label();
            this.nudAoeHeight = new DarkUI.Controls.DarkNumericUpDown();
            this.nudAoeWidth = new DarkUI.Controls.DarkNumericUpDown();
            this.chkAoeRelativeDir = new DarkUI.Controls.DarkCheckBox();
            this.lblAoeYOffset = new System.Windows.Forms.Label();
            this.nudAoeYOffset = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAoeXOffset = new System.Windows.Forms.Label();
            this.nudAoeXOffset = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAoeShape = new System.Windows.Forms.Label();
            this.cmbAoeShape = new DarkUI.Controls.DarkComboBox();
            this.grpBalanceHelp = new DarkUI.Controls.DarkGroupBox();
            this.lblMaxHitVal = new System.Windows.Forms.Label();
            this.lblMaxHit = new System.Windows.Forms.Label();
            this.nudMockMagic = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMockPierce = new DarkUI.Controls.DarkNumericUpDown();
            this.mockMagic = new System.Windows.Forms.Label();
            this.mockPierce = new System.Windows.Forms.Label();
            this.nudMockSlash = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMockSlash = new System.Windows.Forms.Label();
            this.nudMockBlunt = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMockBlunt = new System.Windows.Forms.Label();
            this.lblProjectedDpsVal = new System.Windows.Forms.Label();
            this.lblTargetDpsVal = new System.Windows.Forms.Label();
            this.lblTargetDps = new System.Windows.Forms.Label();
            this.lblProejctedDps = new System.Windows.Forms.Label();
            this.lblTier = new System.Windows.Forms.Label();
            this.darkComboBox1 = new DarkUI.Controls.DarkComboBox();
            this.grpBonusEffects = new DarkUI.Controls.DarkGroupBox();
            this.lstBonusEffects = new System.Windows.Forms.ListBox();
            this.nudBonusAmt = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBonusAmt = new System.Windows.Forms.Label();
            this.grpDamageTypes = new DarkUI.Controls.DarkGroupBox();
            this.grpSetDamages = new DarkUI.Controls.DarkGroupBox();
            this.nudMagicDam = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMagicDamage = new System.Windows.Forms.Label();
            this.nudSlashDam = new DarkUI.Controls.DarkNumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudPierceDam = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPierceDamage = new System.Windows.Forms.Label();
            this.lblBluntDamage = new System.Windows.Forms.Label();
            this.nudBluntDam = new DarkUI.Controls.DarkNumericUpDown();
            this.chkInheritStats = new DarkUI.Controls.DarkCheckBox();
            this.chkDamageMagic = new DarkUI.Controls.DarkCheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkDamagePierce = new DarkUI.Controls.DarkCheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkDamageSlash = new DarkUI.Controls.DarkCheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.chkDamageBlunt = new DarkUI.Controls.DarkCheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.grpStats = new DarkUI.Controls.DarkGroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.nudEvasionPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.nudEvasion = new DarkUI.Controls.DarkNumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.nudAccuracyPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.nudAccuracy = new DarkUI.Controls.DarkNumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudPierceResistPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudPiercePercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudPierceResist = new DarkUI.Controls.DarkNumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudPierce = new DarkUI.Controls.DarkNumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudSlashResistPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSlashResist = new DarkUI.Controls.DarkNumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSlashPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSlash = new DarkUI.Controls.DarkNumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPercentage5 = new System.Windows.Forms.Label();
            this.lblPercentage4 = new System.Windows.Forms.Label();
            this.lblPercentage3 = new System.Windows.Forms.Label();
            this.lblPercentage2 = new System.Windows.Forms.Label();
            this.lblPercentage1 = new System.Windows.Forms.Label();
            this.nudSpdPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMRPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDefPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMagPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudStrPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPlus5 = new System.Windows.Forms.Label();
            this.lblPlus4 = new System.Windows.Forms.Label();
            this.lblPlus3 = new System.Windows.Forms.Label();
            this.lblPlus2 = new System.Windows.Forms.Label();
            this.lblPlus1 = new System.Windows.Forms.Label();
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
            this.grpHotDot = new DarkUI.Controls.DarkGroupBox();
            this.lblOTanimationDisclaimer = new System.Windows.Forms.Label();
            this.lblHOTDOTenableAnimation = new System.Windows.Forms.Label();
            this.cmbOverTimeAnimation = new DarkUI.Controls.DarkComboBox();
            this.nudTick = new DarkUI.Controls.DarkNumericUpDown();
            this.chkHOTDOT = new DarkUI.Controls.DarkCheckBox();
            this.lblTick = new System.Windows.Forms.Label();
            this.grpEffect = new DarkUI.Controls.DarkGroupBox();
            this.lblEffect = new System.Windows.Forms.Label();
            this.cmbExtraEffect = new DarkUI.Controls.DarkComboBox();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.cmbTransform = new DarkUI.Controls.DarkComboBox();
            this.lblSprite = new System.Windows.Forms.Label();
            this.grpEffectDuration = new DarkUI.Controls.DarkGroupBox();
            this.nudBuffDuration = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBuffDuration = new System.Windows.Forms.Label();
            this.grpDamage = new DarkUI.Controls.DarkGroupBox();
            this.chkLifesteal = new DarkUI.Controls.DarkCheckBox();
            this.chkManaSteal = new DarkUI.Controls.DarkCheckBox();
            this.nudCritMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCritMultiplier = new System.Windows.Forms.Label();
            this.nudCritChance = new DarkUI.Controls.DarkNumericUpDown();
            this.nudScaling = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMPDamage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHPDamage = new DarkUI.Controls.DarkNumericUpDown();
            this.chkFriendly = new DarkUI.Controls.DarkCheckBox();
            this.lblCritChance = new System.Windows.Forms.Label();
            this.lblScaling = new System.Windows.Forms.Label();
            this.cmbDamageType = new DarkUI.Controls.DarkComboBox();
            this.lblDamageType = new System.Windows.Forms.Label();
            this.lblHPDamage = new System.Windows.Forms.Label();
            this.lblManaDamage = new System.Windows.Forms.Label();
            this.grpEvent = new DarkUI.Controls.DarkGroupBox();
            this.cmbEvent = new DarkUI.Controls.DarkComboBox();
            this.grpWarp = new DarkUI.Controls.DarkGroupBox();
            this.nudWarpY = new DarkUI.Controls.DarkNumericUpDown();
            this.nudWarpX = new DarkUI.Controls.DarkNumericUpDown();
            this.btnVisualMapSelector = new DarkUI.Controls.DarkButton();
            this.cmbWarpMap = new DarkUI.Controls.DarkComboBox();
            this.cmbDirection = new DarkUI.Controls.DarkComboBox();
            this.lblWarpDir = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblMap = new System.Windows.Forms.Label();
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
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpSpells = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.pnlContainer.SuspendLayout();
            this.grpNpcSpellChain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeChainDelay)).BeginInit();
            this.grpSpellGroup.SuspendLayout();
            this.grpComponents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudComponentQuantity)).BeginInit();
            this.grpTargetInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHitRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCastRange)).BeginInit();
            this.grpDash.SuspendLayout();
            this.grpDashCollisions.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpell)).BeginInit();
            this.grpSpellCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterruptThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkillPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldownDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCastDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPCost)).BeginInit();
            this.grpRequirements.SuspendLayout();
            this.grpCombat.SuspendLayout();
            this.grpAdvTrapSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrapCooldown)).BeginInit();
            this.grpAreaDenial.SuspendLayout();
            this.grpAreaDenialSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaSpawnIntensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaDenialRadius)).BeginInit();
            this.grpAdvAoe.SuspendLayout();
            this.grpRectangleSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeXOffset)).BeginInit();
            this.grpBalanceHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockMagic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockPierce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockSlash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockBlunt)).BeginInit();
            this.grpBonusEffects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusAmt)).BeginInit();
            this.grpDamageTypes.SuspendLayout();
            this.grpSetDamages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagicDam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashDam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceDam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBluntDam)).BeginInit();
            this.grpStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasionPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracyPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResistPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPiercePercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResistPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpdPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMRPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            this.grpHotDot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTick)).BeginInit();
            this.grpEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            this.grpEffectDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuffDuration)).BeginInit();
            this.grpDamage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMPDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPDamage)).BeginInit();
            this.grpEvent.SuspendLayout();
            this.grpWarp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarpY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarpX)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.grpSpells.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.grpNpcSpellChain);
            this.pnlContainer.Controls.Add(this.grpSpellGroup);
            this.pnlContainer.Controls.Add(this.grpComponents);
            this.pnlContainer.Controls.Add(this.grpTargetInfo);
            this.pnlContainer.Controls.Add(this.grpDash);
            this.pnlContainer.Controls.Add(this.grpGeneral);
            this.pnlContainer.Controls.Add(this.grpSpellCost);
            this.pnlContainer.Controls.Add(this.grpRequirements);
            this.pnlContainer.Controls.Add(this.grpCombat);
            this.pnlContainer.Controls.Add(this.grpEvent);
            this.pnlContainer.Controls.Add(this.grpWarp);
            this.pnlContainer.Location = new System.Drawing.Point(221, 40);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(692, 642);
            this.pnlContainer.TabIndex = 41;
            this.pnlContainer.Visible = false;
            // 
            // grpNpcSpellChain
            // 
            this.grpNpcSpellChain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpNpcSpellChain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpNpcSpellChain.Controls.Add(this.lblAoeChainDelay);
            this.grpNpcSpellChain.Controls.Add(this.nudAoeChainDelay);
            this.grpNpcSpellChain.Controls.Add(this.lblChain);
            this.grpNpcSpellChain.Controls.Add(this.cmbAoeChain);
            this.grpNpcSpellChain.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpNpcSpellChain.Location = new System.Drawing.Point(4, 494);
            this.grpNpcSpellChain.Margin = new System.Windows.Forms.Padding(2);
            this.grpNpcSpellChain.Name = "grpNpcSpellChain";
            this.grpNpcSpellChain.Padding = new System.Windows.Forms.Padding(2);
            this.grpNpcSpellChain.Size = new System.Drawing.Size(438, 64);
            this.grpNpcSpellChain.TabIndex = 125;
            this.grpNpcSpellChain.TabStop = false;
            this.grpNpcSpellChain.Text = "NPC Spell Chaining";
            // 
            // lblAoeChainDelay
            // 
            this.lblAoeChainDelay.AutoSize = true;
            this.lblAoeChainDelay.Location = new System.Drawing.Point(219, 38);
            this.lblAoeChainDelay.Name = "lblAoeChainDelay";
            this.lblAoeChainDelay.Size = new System.Drawing.Size(59, 13);
            this.lblAoeChainDelay.TabIndex = 129;
            this.lblAoeChainDelay.Text = "Delay (ms):";
            // 
            // nudAoeChainDelay
            // 
            this.nudAoeChainDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAoeChainDelay.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAoeChainDelay.Location = new System.Drawing.Point(294, 35);
            this.nudAoeChainDelay.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nudAoeChainDelay.Name = "nudAoeChainDelay";
            this.nudAoeChainDelay.Size = new System.Drawing.Size(111, 20);
            this.nudAoeChainDelay.TabIndex = 130;
            this.nudAoeChainDelay.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAoeChainDelay.ValueChanged += new System.EventHandler(this.nudAoeChainDelay_ValueChanged);
            // 
            // lblChain
            // 
            this.lblChain.AutoSize = true;
            this.lblChain.Location = new System.Drawing.Point(10, 19);
            this.lblChain.Name = "lblChain";
            this.lblChain.Size = new System.Drawing.Size(58, 13);
            this.lblChain.TabIndex = 127;
            this.lblChain.Text = "Next Spell:";
            // 
            // cmbAoeChain
            // 
            this.cmbAoeChain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAoeChain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAoeChain.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAoeChain.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAoeChain.DrawDropdownHoverOutline = false;
            this.cmbAoeChain.DrawFocusRectangle = false;
            this.cmbAoeChain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAoeChain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAoeChain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAoeChain.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAoeChain.FormattingEnabled = true;
            this.cmbAoeChain.Location = new System.Drawing.Point(5, 35);
            this.cmbAoeChain.Name = "cmbAoeChain";
            this.cmbAoeChain.Size = new System.Drawing.Size(193, 21);
            this.cmbAoeChain.TabIndex = 128;
            this.cmbAoeChain.Text = null;
            this.cmbAoeChain.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAoeChain.SelectedIndexChanged += new System.EventHandler(this.cmbAoeChain_SelectedIndexChanged);
            // 
            // grpSpellGroup
            // 
            this.grpSpellGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpellGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpellGroup.Controls.Add(this.lblSpellUpgrade);
            this.grpSpellGroup.Controls.Add(this.cmbSpellUpgrade);
            this.grpSpellGroup.Controls.Add(this.btnSetEmpty);
            this.grpSpellGroup.Controls.Add(this.btnAddSpellGroup);
            this.grpSpellGroup.Controls.Add(this.cmbSpellGroup);
            this.grpSpellGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpellGroup.Location = new System.Drawing.Point(447, 246);
            this.grpSpellGroup.Name = "grpSpellGroup";
            this.grpSpellGroup.Size = new System.Drawing.Size(213, 163);
            this.grpSpellGroup.TabIndex = 111;
            this.grpSpellGroup.TabStop = false;
            this.grpSpellGroup.Text = "Spell Group Info";
            this.grpSpellGroup.Visible = false;
            // 
            // lblSpellUpgrade
            // 
            this.lblSpellUpgrade.AutoSize = true;
            this.lblSpellUpgrade.Location = new System.Drawing.Point(5, 120);
            this.lblSpellUpgrade.Name = "lblSpellUpgrade";
            this.lblSpellUpgrade.Size = new System.Drawing.Size(91, 13);
            this.lblSpellUpgrade.TabIndex = 61;
            this.lblSpellUpgrade.Text = "Spell Upgrade Of:";
            // 
            // cmbSpellUpgrade
            // 
            this.cmbSpellUpgrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpellUpgrade.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpellUpgrade.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpellUpgrade.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpellUpgrade.DrawDropdownHoverOutline = false;
            this.cmbSpellUpgrade.DrawFocusRectangle = false;
            this.cmbSpellUpgrade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpellUpgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpellUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpellUpgrade.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpellUpgrade.FormattingEnabled = true;
            this.cmbSpellUpgrade.Location = new System.Drawing.Point(5, 136);
            this.cmbSpellUpgrade.Name = "cmbSpellUpgrade";
            this.cmbSpellUpgrade.Size = new System.Drawing.Size(197, 21);
            this.cmbSpellUpgrade.TabIndex = 60;
            this.cmbSpellUpgrade.Text = null;
            this.cmbSpellUpgrade.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSpellUpgrade.SelectedIndexChanged += new System.EventHandler(this.cmbSpellUpgrade_SelectedIndexChanged);
            // 
            // btnSetEmpty
            // 
            this.btnSetEmpty.Location = new System.Drawing.Point(8, 82);
            this.btnSetEmpty.Name = "btnSetEmpty";
            this.btnSetEmpty.Padding = new System.Windows.Forms.Padding(5);
            this.btnSetEmpty.Size = new System.Drawing.Size(197, 27);
            this.btnSetEmpty.TabIndex = 59;
            this.btnSetEmpty.Text = "Set Empty";
            this.btnSetEmpty.Click += new System.EventHandler(this.btnSetEmpty_Click);
            // 
            // btnAddSpellGroup
            // 
            this.btnAddSpellGroup.Location = new System.Drawing.Point(8, 49);
            this.btnAddSpellGroup.Name = "btnAddSpellGroup";
            this.btnAddSpellGroup.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddSpellGroup.Size = new System.Drawing.Size(197, 27);
            this.btnAddSpellGroup.TabIndex = 52;
            this.btnAddSpellGroup.Text = "Add New";
            this.btnAddSpellGroup.Click += new System.EventHandler(this.btnAddSpellGroup_Click);
            // 
            // cmbSpellGroup
            // 
            this.cmbSpellGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpellGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpellGroup.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpellGroup.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpellGroup.DrawDropdownHoverOutline = false;
            this.cmbSpellGroup.DrawFocusRectangle = false;
            this.cmbSpellGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpellGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpellGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpellGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpellGroup.FormattingEnabled = true;
            this.cmbSpellGroup.Location = new System.Drawing.Point(8, 22);
            this.cmbSpellGroup.Name = "cmbSpellGroup";
            this.cmbSpellGroup.Size = new System.Drawing.Size(197, 21);
            this.cmbSpellGroup.TabIndex = 58;
            this.cmbSpellGroup.Text = null;
            this.cmbSpellGroup.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSpellGroup.SelectedIndexChanged += new System.EventHandler(this.cmbSpellGroup_SelectedIndexChanged);
            // 
            // grpComponents
            // 
            this.grpComponents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpComponents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpComponents.Controls.Add(this.lblComponentQuantity);
            this.grpComponents.Controls.Add(this.nudComponentQuantity);
            this.grpComponents.Controls.Add(this.btnRemoveComponent);
            this.grpComponents.Controls.Add(this.btnAddComponent);
            this.grpComponents.Controls.Add(this.lblComponent);
            this.grpComponents.Controls.Add(this.cmbComponents);
            this.grpComponents.Controls.Add(this.lstComponents);
            this.grpComponents.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpComponents.Location = new System.Drawing.Point(446, 0);
            this.grpComponents.Name = "grpComponents";
            this.grpComponents.Size = new System.Drawing.Size(213, 240);
            this.grpComponents.TabIndex = 58;
            this.grpComponents.TabStop = false;
            this.grpComponents.Text = "Casting Components";
            this.grpComponents.Visible = false;
            // 
            // lblComponentQuantity
            // 
            this.lblComponentQuantity.AutoSize = true;
            this.lblComponentQuantity.Location = new System.Drawing.Point(6, 158);
            this.lblComponentQuantity.Name = "lblComponentQuantity";
            this.lblComponentQuantity.Size = new System.Drawing.Size(46, 13);
            this.lblComponentQuantity.TabIndex = 110;
            this.lblComponentQuantity.Text = "Quantity";
            // 
            // nudComponentQuantity
            // 
            this.nudComponentQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudComponentQuantity.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudComponentQuantity.Location = new System.Drawing.Point(6, 174);
            this.nudComponentQuantity.Maximum = new decimal(new int[] {
            -100,
            49,
            0,
            0});
            this.nudComponentQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudComponentQuantity.Name = "nudComponentQuantity";
            this.nudComponentQuantity.Size = new System.Drawing.Size(200, 20);
            this.nudComponentQuantity.TabIndex = 58;
            this.nudComponentQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRemoveComponent
            // 
            this.btnRemoveComponent.Location = new System.Drawing.Point(117, 202);
            this.btnRemoveComponent.Name = "btnRemoveComponent";
            this.btnRemoveComponent.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveComponent.Size = new System.Drawing.Size(89, 27);
            this.btnRemoveComponent.TabIndex = 109;
            this.btnRemoveComponent.Text = "Remove";
            this.btnRemoveComponent.Click += new System.EventHandler(this.btnRemoveComponent_Click);
            // 
            // btnAddComponent
            // 
            this.btnAddComponent.Location = new System.Drawing.Point(6, 202);
            this.btnAddComponent.Name = "btnAddComponent";
            this.btnAddComponent.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddComponent.Size = new System.Drawing.Size(88, 27);
            this.btnAddComponent.TabIndex = 52;
            this.btnAddComponent.Text = "Add";
            this.btnAddComponent.Click += new System.EventHandler(this.btnAddComponent_Click);
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.Location = new System.Drawing.Point(6, 108);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(61, 13);
            this.lblComponent.TabIndex = 58;
            this.lblComponent.Text = "Component";
            // 
            // cmbComponents
            // 
            this.cmbComponents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbComponents.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbComponents.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbComponents.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbComponents.DrawDropdownHoverOutline = false;
            this.cmbComponents.DrawFocusRectangle = false;
            this.cmbComponents.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbComponents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComponents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbComponents.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbComponents.FormattingEnabled = true;
            this.cmbComponents.Location = new System.Drawing.Point(6, 124);
            this.cmbComponents.Name = "cmbComponents";
            this.cmbComponents.Size = new System.Drawing.Size(200, 21);
            this.cmbComponents.TabIndex = 58;
            this.cmbComponents.Text = null;
            this.cmbComponents.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // lstComponents
            // 
            this.lstComponents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstComponents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstComponents.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstComponents.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstComponents.FormattingEnabled = true;
            this.lstComponents.Location = new System.Drawing.Point(6, 19);
            this.lstComponents.Name = "lstComponents";
            this.lstComponents.Size = new System.Drawing.Size(201, 80);
            this.lstComponents.TabIndex = 108;
            this.lstComponents.SelectedIndexChanged += new System.EventHandler(this.lstComponents_SelectedIndexChanged);
            // 
            // grpTargetInfo
            // 
            this.grpTargetInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTargetInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTargetInfo.Controls.Add(this.nudMinRange);
            this.grpTargetInfo.Controls.Add(this.lblMinRange);
            this.grpTargetInfo.Controls.Add(this.chkPersistMissedAttack);
            this.grpTargetInfo.Controls.Add(this.chkPersistSwap);
            this.grpTargetInfo.Controls.Add(this.cmbProjectile);
            this.grpTargetInfo.Controls.Add(this.lblProjectile);
            this.grpTargetInfo.Controls.Add(this.lblTrapAnimation);
            this.grpTargetInfo.Controls.Add(this.cmbTrapAnimation);
            this.grpTargetInfo.Controls.Add(this.nudDuration);
            this.grpTargetInfo.Controls.Add(this.lblDuration);
            this.grpTargetInfo.Controls.Add(this.nudHitRadius);
            this.grpTargetInfo.Controls.Add(this.lblHitRadius);
            this.grpTargetInfo.Controls.Add(this.cmbTargetType);
            this.grpTargetInfo.Controls.Add(this.lblCastRange);
            this.grpTargetInfo.Controls.Add(this.lblTargetType);
            this.grpTargetInfo.Controls.Add(this.nudCastRange);
            this.grpTargetInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTargetInfo.Location = new System.Drawing.Point(213, 3);
            this.grpTargetInfo.Name = "grpTargetInfo";
            this.grpTargetInfo.Size = new System.Drawing.Size(225, 226);
            this.grpTargetInfo.TabIndex = 19;
            this.grpTargetInfo.TabStop = false;
            this.grpTargetInfo.Text = "Targetting Info";
            this.grpTargetInfo.Visible = false;
            // 
            // nudMinRange
            // 
            this.nudMinRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMinRange.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMinRange.Location = new System.Drawing.Point(8, 197);
            this.nudMinRange.Maximum = new decimal(new int[] {
            -100,
            49,
            0,
            0});
            this.nudMinRange.Name = "nudMinRange";
            this.nudMinRange.Size = new System.Drawing.Size(206, 20);
            this.nudMinRange.TabIndex = 68;
            this.nudMinRange.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMinRange.ValueChanged += new System.EventHandler(this.darkNumericUpDown1_ValueChanged);
            // 
            // lblMinRange
            // 
            this.lblMinRange.AutoSize = true;
            this.lblMinRange.Location = new System.Drawing.Point(9, 182);
            this.lblMinRange.Name = "lblMinRange";
            this.lblMinRange.Size = new System.Drawing.Size(62, 13);
            this.lblMinRange.TabIndex = 67;
            this.lblMinRange.Text = "Min Range:";
            // 
            // chkPersistMissedAttack
            // 
            this.chkPersistMissedAttack.AutoSize = true;
            this.chkPersistMissedAttack.Location = new System.Drawing.Point(6, 119);
            this.chkPersistMissedAttack.Name = "chkPersistMissedAttack";
            this.chkPersistMissedAttack.Size = new System.Drawing.Size(127, 17);
            this.chkPersistMissedAttack.TabIndex = 66;
            this.chkPersistMissedAttack.Text = "Persist Missed Attack";
            this.chkPersistMissedAttack.CheckedChanged += new System.EventHandler(this.chkPersistMissedAttack_CheckedChanged);
            // 
            // chkPersistSwap
            // 
            this.chkPersistSwap.AutoSize = true;
            this.chkPersistSwap.Location = new System.Drawing.Point(6, 78);
            this.chkPersistSwap.Name = "chkPersistSwap";
            this.chkPersistSwap.Size = new System.Drawing.Size(131, 17);
            this.chkPersistSwap.TabIndex = 65;
            this.chkPersistSwap.Text = "Persist Weapon Swap";
            this.chkPersistSwap.CheckedChanged += new System.EventHandler(this.chkPersistSwap_CheckedChanged);
            // 
            // cmbProjectile
            // 
            this.cmbProjectile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbProjectile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbProjectile.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbProjectile.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbProjectile.DrawDropdownHoverOutline = false;
            this.cmbProjectile.DrawFocusRectangle = false;
            this.cmbProjectile.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProjectile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjectile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProjectile.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbProjectile.FormattingEnabled = true;
            this.cmbProjectile.Location = new System.Drawing.Point(10, 118);
            this.cmbProjectile.Name = "cmbProjectile";
            this.cmbProjectile.Size = new System.Drawing.Size(206, 21);
            this.cmbProjectile.TabIndex = 19;
            this.cmbProjectile.Text = null;
            this.cmbProjectile.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbProjectile.Visible = false;
            this.cmbProjectile.SelectedIndexChanged += new System.EventHandler(this.cmbProjectile_SelectedIndexChanged);
            // 
            // lblProjectile
            // 
            this.lblProjectile.AutoSize = true;
            this.lblProjectile.Location = new System.Drawing.Point(8, 102);
            this.lblProjectile.Name = "lblProjectile";
            this.lblProjectile.Size = new System.Drawing.Size(53, 13);
            this.lblProjectile.TabIndex = 18;
            this.lblProjectile.Text = "Projectile:";
            // 
            // lblTrapAnimation
            // 
            this.lblTrapAnimation.AutoSize = true;
            this.lblTrapAnimation.Location = new System.Drawing.Point(7, 60);
            this.lblTrapAnimation.Name = "lblTrapAnimation";
            this.lblTrapAnimation.Size = new System.Drawing.Size(81, 13);
            this.lblTrapAnimation.TabIndex = 60;
            this.lblTrapAnimation.Text = "Trap Animation:";
            // 
            // cmbTrapAnimation
            // 
            this.cmbTrapAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTrapAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTrapAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTrapAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTrapAnimation.DrawDropdownHoverOutline = false;
            this.cmbTrapAnimation.DrawFocusRectangle = false;
            this.cmbTrapAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTrapAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrapAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTrapAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTrapAnimation.FormattingEnabled = true;
            this.cmbTrapAnimation.Location = new System.Drawing.Point(9, 76);
            this.cmbTrapAnimation.Name = "cmbTrapAnimation";
            this.cmbTrapAnimation.Size = new System.Drawing.Size(206, 21);
            this.cmbTrapAnimation.TabIndex = 60;
            this.cmbTrapAnimation.Text = null;
            this.cmbTrapAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTrapAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbTrapAnimation_SelectedIndexChanged);
            // 
            // nudDuration
            // 
            this.nudDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDuration.Location = new System.Drawing.Point(9, 159);
            this.nudDuration.Maximum = new decimal(new int[] {
            -100,
            49,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(206, 20);
            this.nudDuration.TabIndex = 38;
            this.nudDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDuration.ValueChanged += new System.EventHandler(this.nudOnHitDuration_ValueChanged);
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(8, 143);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(72, 13);
            this.lblDuration.TabIndex = 37;
            this.lblDuration.Text = "Duration (ms):";
            // 
            // nudHitRadius
            // 
            this.nudHitRadius.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHitRadius.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHitRadius.Location = new System.Drawing.Point(9, 118);
            this.nudHitRadius.Name = "nudHitRadius";
            this.nudHitRadius.Size = new System.Drawing.Size(206, 20);
            this.nudHitRadius.TabIndex = 35;
            this.nudHitRadius.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHitRadius.ValueChanged += new System.EventHandler(this.nudHitRadius_ValueChanged);
            // 
            // lblHitRadius
            // 
            this.lblHitRadius.AutoSize = true;
            this.lblHitRadius.Location = new System.Drawing.Point(6, 102);
            this.lblHitRadius.Name = "lblHitRadius";
            this.lblHitRadius.Size = new System.Drawing.Size(59, 13);
            this.lblHitRadius.TabIndex = 16;
            this.lblHitRadius.Text = "Hit Radius:";
            // 
            // cmbTargetType
            // 
            this.cmbTargetType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTargetType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTargetType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTargetType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTargetType.DrawDropdownHoverOutline = false;
            this.cmbTargetType.DrawFocusRectangle = false;
            this.cmbTargetType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTargetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTargetType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTargetType.FormattingEnabled = true;
            this.cmbTargetType.Items.AddRange(new object[] {
            "Self",
            "Single Target (includes self)",
            "AOE",
            "Linear (projectile)",
            "On Hit",
            "Trap"});
            this.cmbTargetType.Location = new System.Drawing.Point(9, 32);
            this.cmbTargetType.Name = "cmbTargetType";
            this.cmbTargetType.Size = new System.Drawing.Size(206, 21);
            this.cmbTargetType.TabIndex = 15;
            this.cmbTargetType.Text = "Self";
            this.cmbTargetType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTargetType.SelectedIndexChanged += new System.EventHandler(this.cmbTargetType_SelectedIndexChanged);
            // 
            // lblCastRange
            // 
            this.lblCastRange.AutoSize = true;
            this.lblCastRange.Location = new System.Drawing.Point(6, 60);
            this.lblCastRange.Name = "lblCastRange";
            this.lblCastRange.Size = new System.Drawing.Size(66, 13);
            this.lblCastRange.TabIndex = 13;
            this.lblCastRange.Text = "Cast Range:";
            // 
            // lblTargetType
            // 
            this.lblTargetType.AutoSize = true;
            this.lblTargetType.Location = new System.Drawing.Point(6, 16);
            this.lblTargetType.Name = "lblTargetType";
            this.lblTargetType.Size = new System.Drawing.Size(68, 13);
            this.lblTargetType.TabIndex = 12;
            this.lblTargetType.Text = "Target Type:";
            // 
            // nudCastRange
            // 
            this.nudCastRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCastRange.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCastRange.Location = new System.Drawing.Point(9, 76);
            this.nudCastRange.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudCastRange.Name = "nudCastRange";
            this.nudCastRange.Size = new System.Drawing.Size(206, 20);
            this.nudCastRange.TabIndex = 36;
            this.nudCastRange.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCastRange.ValueChanged += new System.EventHandler(this.nudCastRange_ValueChanged);
            // 
            // grpDash
            // 
            this.grpDash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDash.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDash.Controls.Add(this.cmbDashAnimation);
            this.grpDash.Controls.Add(this.lblDashAnimation);
            this.grpDash.Controls.Add(this.cmbDashSpell);
            this.grpDash.Controls.Add(this.lblDashSpell);
            this.grpDash.Controls.Add(this.grpDashCollisions);
            this.grpDash.Controls.Add(this.lblRange);
            this.grpDash.Controls.Add(this.scrlRange);
            this.grpDash.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDash.Location = new System.Drawing.Point(215, 2);
            this.grpDash.Name = "grpDash";
            this.grpDash.Size = new System.Drawing.Size(220, 297);
            this.grpDash.TabIndex = 38;
            this.grpDash.TabStop = false;
            this.grpDash.Text = "Dash";
            this.grpDash.Visible = false;
            // 
            // cmbDashAnimation
            // 
            this.cmbDashAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDashAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDashAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDashAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDashAnimation.DrawDropdownHoverOutline = false;
            this.cmbDashAnimation.DrawFocusRectangle = false;
            this.cmbDashAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDashAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDashAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDashAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDashAnimation.FormattingEnabled = true;
            this.cmbDashAnimation.Location = new System.Drawing.Point(12, 266);
            this.cmbDashAnimation.Name = "cmbDashAnimation";
            this.cmbDashAnimation.Size = new System.Drawing.Size(200, 21);
            this.cmbDashAnimation.TabIndex = 59;
            this.cmbDashAnimation.Text = null;
            this.cmbDashAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDashAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbDashAnimation_SelectedIndexChanged);
            // 
            // lblDashAnimation
            // 
            this.lblDashAnimation.AutoSize = true;
            this.lblDashAnimation.Location = new System.Drawing.Point(11, 248);
            this.lblDashAnimation.Name = "lblDashAnimation";
            this.lblDashAnimation.Size = new System.Drawing.Size(81, 13);
            this.lblDashAnimation.TabIndex = 58;
            this.lblDashAnimation.Text = "Dash Animation";
            // 
            // cmbDashSpell
            // 
            this.cmbDashSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDashSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDashSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDashSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDashSpell.DrawDropdownHoverOutline = false;
            this.cmbDashSpell.DrawFocusRectangle = false;
            this.cmbDashSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDashSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDashSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDashSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDashSpell.FormattingEnabled = true;
            this.cmbDashSpell.Location = new System.Drawing.Point(14, 224);
            this.cmbDashSpell.Name = "cmbDashSpell";
            this.cmbDashSpell.Size = new System.Drawing.Size(200, 21);
            this.cmbDashSpell.TabIndex = 18;
            this.cmbDashSpell.Text = null;
            this.cmbDashSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDashSpell.SelectedIndexChanged += new System.EventHandler(this.cmbDashSpell_SelectedIndexChanged);
            // 
            // lblDashSpell
            // 
            this.lblDashSpell.AutoSize = true;
            this.lblDashSpell.Location = new System.Drawing.Point(11, 208);
            this.lblDashSpell.Name = "lblDashSpell";
            this.lblDashSpell.Size = new System.Drawing.Size(58, 13);
            this.lblDashSpell.TabIndex = 57;
            this.lblDashSpell.Text = "Dash Spell";
            // 
            // grpDashCollisions
            // 
            this.grpDashCollisions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDashCollisions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDashCollisions.Controls.Add(this.chkEntities);
            this.grpDashCollisions.Controls.Add(this.chkIgnoreInactiveResources);
            this.grpDashCollisions.Controls.Add(this.chkIgnoreZDimensionBlocks);
            this.grpDashCollisions.Controls.Add(this.chkIgnoreMapBlocks);
            this.grpDashCollisions.Controls.Add(this.chkIgnoreActiveResources);
            this.grpDashCollisions.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDashCollisions.Location = new System.Drawing.Point(12, 62);
            this.grpDashCollisions.Name = "grpDashCollisions";
            this.grpDashCollisions.Size = new System.Drawing.Size(202, 143);
            this.grpDashCollisions.TabIndex = 41;
            this.grpDashCollisions.TabStop = false;
            this.grpDashCollisions.Text = "Ignore Collision:";
            // 
            // chkEntities
            // 
            this.chkEntities.AutoSize = true;
            this.chkEntities.Location = new System.Drawing.Point(6, 108);
            this.chkEntities.Name = "chkEntities";
            this.chkEntities.Size = new System.Drawing.Size(60, 17);
            this.chkEntities.TabIndex = 39;
            this.chkEntities.Text = "Entities";
            this.chkEntities.CheckedChanged += new System.EventHandler(this.chkEntities_CheckedChanged);
            // 
            // chkIgnoreInactiveResources
            // 
            this.chkIgnoreInactiveResources.AutoSize = true;
            this.chkIgnoreInactiveResources.Location = new System.Drawing.Point(6, 62);
            this.chkIgnoreInactiveResources.Name = "chkIgnoreInactiveResources";
            this.chkIgnoreInactiveResources.Size = new System.Drawing.Size(118, 17);
            this.chkIgnoreInactiveResources.TabIndex = 38;
            this.chkIgnoreInactiveResources.Text = "Inactive Resources";
            this.chkIgnoreInactiveResources.CheckedChanged += new System.EventHandler(this.chkIgnoreInactiveResources_CheckedChanged);
            // 
            // chkIgnoreZDimensionBlocks
            // 
            this.chkIgnoreZDimensionBlocks.AutoSize = true;
            this.chkIgnoreZDimensionBlocks.Location = new System.Drawing.Point(6, 85);
            this.chkIgnoreZDimensionBlocks.Name = "chkIgnoreZDimensionBlocks";
            this.chkIgnoreZDimensionBlocks.Size = new System.Drawing.Size(120, 17);
            this.chkIgnoreZDimensionBlocks.TabIndex = 37;
            this.chkIgnoreZDimensionBlocks.Text = "Z-Dimension Blocks";
            this.chkIgnoreZDimensionBlocks.CheckedChanged += new System.EventHandler(this.chkIgnoreZDimensionBlocks_CheckedChanged);
            // 
            // chkIgnoreMapBlocks
            // 
            this.chkIgnoreMapBlocks.AutoSize = true;
            this.chkIgnoreMapBlocks.Location = new System.Drawing.Point(6, 16);
            this.chkIgnoreMapBlocks.Name = "chkIgnoreMapBlocks";
            this.chkIgnoreMapBlocks.Size = new System.Drawing.Size(82, 17);
            this.chkIgnoreMapBlocks.TabIndex = 33;
            this.chkIgnoreMapBlocks.Text = "Map Blocks";
            this.chkIgnoreMapBlocks.CheckedChanged += new System.EventHandler(this.chkIgnoreMapBlocks_CheckedChanged);
            // 
            // chkIgnoreActiveResources
            // 
            this.chkIgnoreActiveResources.AutoSize = true;
            this.chkIgnoreActiveResources.Location = new System.Drawing.Point(6, 39);
            this.chkIgnoreActiveResources.Name = "chkIgnoreActiveResources";
            this.chkIgnoreActiveResources.Size = new System.Drawing.Size(110, 17);
            this.chkIgnoreActiveResources.TabIndex = 36;
            this.chkIgnoreActiveResources.Text = "Active Resources";
            this.chkIgnoreActiveResources.CheckedChanged += new System.EventHandler(this.chkIgnoreActiveResources_CheckedChanged);
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(11, 25);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(51, 13);
            this.lblRange.TabIndex = 40;
            this.lblRange.Text = "Range: 0";
            // 
            // scrlRange
            // 
            this.scrlRange.Location = new System.Drawing.Point(14, 38);
            this.scrlRange.Maximum = 10;
            this.scrlRange.Name = "scrlRange";
            this.scrlRange.ScrollOrientation = DarkUI.Controls.DarkScrollOrientation.Horizontal;
            this.scrlRange.Size = new System.Drawing.Size(168, 18);
            this.scrlRange.TabIndex = 39;
            this.scrlRange.ValueChanged += new System.EventHandler<DarkUI.Controls.ScrollValueEventArgs>(this.scrlRange_Scroll);
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.btnAddFolder);
            this.grpGeneral.Controls.Add(this.lblFolder);
            this.grpGeneral.Controls.Add(this.cmbFolder);
            this.grpGeneral.Controls.Add(this.chkBound);
            this.grpGeneral.Controls.Add(this.cmbHitAnimation);
            this.grpGeneral.Controls.Add(this.cmbCastAnimation);
            this.grpGeneral.Controls.Add(this.lblDesc);
            this.grpGeneral.Controls.Add(this.txtDesc);
            this.grpGeneral.Controls.Add(this.lblHitAnimation);
            this.grpGeneral.Controls.Add(this.lblCastAnimation);
            this.grpGeneral.Controls.Add(this.cmbSprite);
            this.grpGeneral.Controls.Add(this.lblIcon);
            this.grpGeneral.Controls.Add(this.picSpell);
            this.grpGeneral.Controls.Add(this.lblType);
            this.grpGeneral.Controls.Add(this.cmbType);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(2, 0);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(207, 299);
            this.grpGeneral.TabIndex = 17;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(183, 44);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 59;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(6, 48);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 58;
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
            this.cmbFolder.Size = new System.Drawing.Size(117, 21);
            this.cmbFolder.TabIndex = 57;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // chkBound
            // 
            this.chkBound.AutoSize = true;
            this.chkBound.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBound.Location = new System.Drawing.Point(9, 270);
            this.chkBound.Name = "chkBound";
            this.chkBound.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkBound.Size = new System.Drawing.Size(63, 17);
            this.chkBound.TabIndex = 56;
            this.chkBound.Text = "Bound?";
            this.chkBound.CheckedChanged += new System.EventHandler(this.chkBound_CheckedChanged);
            // 
            // cmbHitAnimation
            // 
            this.cmbHitAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbHitAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbHitAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbHitAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbHitAnimation.DrawDropdownHoverOutline = false;
            this.cmbHitAnimation.DrawFocusRectangle = false;
            this.cmbHitAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbHitAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHitAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbHitAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbHitAnimation.FormattingEnabled = true;
            this.cmbHitAnimation.Location = new System.Drawing.Point(90, 241);
            this.cmbHitAnimation.Name = "cmbHitAnimation";
            this.cmbHitAnimation.Size = new System.Drawing.Size(111, 21);
            this.cmbHitAnimation.TabIndex = 21;
            this.cmbHitAnimation.Text = null;
            this.cmbHitAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbHitAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbHitAnimation_SelectedIndexChanged);
            // 
            // cmbCastAnimation
            // 
            this.cmbCastAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCastAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCastAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCastAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCastAnimation.DrawDropdownHoverOutline = false;
            this.cmbCastAnimation.DrawFocusRectangle = false;
            this.cmbCastAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCastAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCastAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCastAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCastAnimation.FormattingEnabled = true;
            this.cmbCastAnimation.Location = new System.Drawing.Point(90, 214);
            this.cmbCastAnimation.Name = "cmbCastAnimation";
            this.cmbCastAnimation.Size = new System.Drawing.Size(111, 21);
            this.cmbCastAnimation.TabIndex = 20;
            this.cmbCastAnimation.Text = null;
            this.cmbCastAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCastAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbCastAnimation_SelectedIndexChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(6, 148);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(35, 13);
            this.lblDesc.TabIndex = 19;
            this.lblDesc.Text = "Desc:";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDesc.Location = new System.Drawing.Point(60, 147);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(141, 61);
            this.txtDesc.TabIndex = 18;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // lblHitAnimation
            // 
            this.lblHitAnimation.AutoSize = true;
            this.lblHitAnimation.Location = new System.Drawing.Point(6, 244);
            this.lblHitAnimation.Name = "lblHitAnimation";
            this.lblHitAnimation.Size = new System.Drawing.Size(72, 13);
            this.lblHitAnimation.TabIndex = 16;
            this.lblHitAnimation.Text = "Hit Animation:";
            // 
            // lblCastAnimation
            // 
            this.lblCastAnimation.AutoSize = true;
            this.lblCastAnimation.Location = new System.Drawing.Point(6, 217);
            this.lblCastAnimation.Name = "lblCastAnimation";
            this.lblCastAnimation.Size = new System.Drawing.Size(80, 13);
            this.lblCastAnimation.TabIndex = 14;
            this.lblCastAnimation.Text = "Cast Animation:";
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
            this.cmbSprite.Location = new System.Drawing.Point(60, 120);
            this.cmbSprite.Name = "cmbSprite";
            this.cmbSprite.Size = new System.Drawing.Size(141, 21);
            this.cmbSprite.TabIndex = 11;
            this.cmbSprite.Text = "None";
            this.cmbSprite.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSprite.SelectedIndexChanged += new System.EventHandler(this.cmbSprite_SelectedIndexChanged);
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Location = new System.Drawing.Point(57, 104);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(31, 13);
            this.lblIcon.TabIndex = 6;
            this.lblIcon.Text = "Icon:";
            // 
            // picSpell
            // 
            this.picSpell.BackColor = System.Drawing.Color.Black;
            this.picSpell.Location = new System.Drawing.Point(9, 109);
            this.picSpell.Name = "picSpell";
            this.picSpell.Size = new System.Drawing.Size(32, 32);
            this.picSpell.TabIndex = 4;
            this.picSpell.TabStop = false;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 78);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbType.DrawDropdownHoverOutline = false;
            this.cmbType.DrawFocusRectangle = false;
            this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Combat Spell",
            "Warp to Map",
            "Warp to Target",
            "Dash",
            "Event"});
            this.cmbType.Location = new System.Drawing.Point(60, 75);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(141, 21);
            this.cmbType.TabIndex = 2;
            this.cmbType.Text = "Combat Spell";
            this.cmbType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
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
            this.txtName.Size = new System.Drawing.Size(141, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // grpSpellCost
            // 
            this.grpSpellCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpellCost.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpellCost.Controls.Add(this.nudInterruptThreshold);
            this.grpSpellCost.Controls.Add(this.lblInterruptThreshold);
            this.grpSpellCost.Controls.Add(this.nudSkillPoints);
            this.grpSpellCost.Controls.Add(this.lblSkillPts);
            this.grpSpellCost.Controls.Add(this.chkIgnoreCdr);
            this.grpSpellCost.Controls.Add(this.chkIgnoreGlobalCooldown);
            this.grpSpellCost.Controls.Add(this.btnAddCooldownGroup);
            this.grpSpellCost.Controls.Add(this.cmbCooldownGroup);
            this.grpSpellCost.Controls.Add(this.lblCooldownGroup);
            this.grpSpellCost.Controls.Add(this.nudCooldownDuration);
            this.grpSpellCost.Controls.Add(this.nudCastDuration);
            this.grpSpellCost.Controls.Add(this.nudMpCost);
            this.grpSpellCost.Controls.Add(this.nudHPCost);
            this.grpSpellCost.Controls.Add(this.lblMPCost);
            this.grpSpellCost.Controls.Add(this.lblHPCost);
            this.grpSpellCost.Controls.Add(this.lblCastDuration);
            this.grpSpellCost.Controls.Add(this.lblCooldownDuration);
            this.grpSpellCost.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpellCost.Location = new System.Drawing.Point(3, 300);
            this.grpSpellCost.Name = "grpSpellCost";
            this.grpSpellCost.Size = new System.Drawing.Size(438, 189);
            this.grpSpellCost.TabIndex = 36;
            this.grpSpellCost.TabStop = false;
            this.grpSpellCost.Text = "Spell Cost:";
            // 
            // nudInterruptThreshold
            // 
            this.nudInterruptThreshold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudInterruptThreshold.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudInterruptThreshold.Location = new System.Drawing.Point(222, 163);
            this.nudInterruptThreshold.Maximum = new decimal(new int[] {
            -100,
            49,
            0,
            0});
            this.nudInterruptThreshold.Name = "nudInterruptThreshold";
            this.nudInterruptThreshold.Size = new System.Drawing.Size(184, 20);
            this.nudInterruptThreshold.TabIndex = 61;
            this.nudInterruptThreshold.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudInterruptThreshold.ValueChanged += new System.EventHandler(this.nudInterruptThreshold_ValueChanged);
            // 
            // lblInterruptThreshold
            // 
            this.lblInterruptThreshold.AutoSize = true;
            this.lblInterruptThreshold.Location = new System.Drawing.Point(220, 143);
            this.lblInterruptThreshold.Name = "lblInterruptThreshold";
            this.lblInterruptThreshold.Size = new System.Drawing.Size(99, 13);
            this.lblInterruptThreshold.TabIndex = 60;
            this.lblInterruptThreshold.Text = "Interrupt Threshold:";
            // 
            // nudSkillPoints
            // 
            this.nudSkillPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSkillPoints.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSkillPoints.Location = new System.Drawing.Point(10, 163);
            this.nudSkillPoints.Name = "nudSkillPoints";
            this.nudSkillPoints.Size = new System.Drawing.Size(185, 20);
            this.nudSkillPoints.TabIndex = 59;
            this.nudSkillPoints.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSkillPoints.ValueChanged += new System.EventHandler(this.nudSkillPoints_ValueChanged);
            // 
            // lblSkillPts
            // 
            this.lblSkillPts.AutoSize = true;
            this.lblSkillPts.Location = new System.Drawing.Point(7, 147);
            this.lblSkillPts.Name = "lblSkillPts";
            this.lblSkillPts.Size = new System.Drawing.Size(103, 13);
            this.lblSkillPts.TabIndex = 58;
            this.lblSkillPts.Text = "Skill Points to Equip:";
            // 
            // chkIgnoreCdr
            // 
            this.chkIgnoreCdr.AutoSize = true;
            this.chkIgnoreCdr.Location = new System.Drawing.Point(8, 120);
            this.chkIgnoreCdr.Name = "chkIgnoreCdr";
            this.chkIgnoreCdr.Size = new System.Drawing.Size(164, 17);
            this.chkIgnoreCdr.TabIndex = 57;
            this.chkIgnoreCdr.Text = "Ignore Cooldown Reduction?";
            this.chkIgnoreCdr.CheckedChanged += new System.EventHandler(this.chkIgnoreCdr_CheckedChanged);
            // 
            // chkIgnoreGlobalCooldown
            // 
            this.chkIgnoreGlobalCooldown.AutoSize = true;
            this.chkIgnoreGlobalCooldown.Location = new System.Drawing.Point(9, 97);
            this.chkIgnoreGlobalCooldown.Name = "chkIgnoreGlobalCooldown";
            this.chkIgnoreGlobalCooldown.Size = new System.Drawing.Size(145, 17);
            this.chkIgnoreGlobalCooldown.TabIndex = 56;
            this.chkIgnoreGlobalCooldown.Text = "Ignore Global Cooldown?";
            this.chkIgnoreGlobalCooldown.CheckedChanged += new System.EventHandler(this.chkIgnoreGlobalCooldown_CheckedChanged);
            // 
            // btnAddCooldownGroup
            // 
            this.btnAddCooldownGroup.Location = new System.Drawing.Point(388, 112);
            this.btnAddCooldownGroup.Name = "btnAddCooldownGroup";
            this.btnAddCooldownGroup.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddCooldownGroup.Size = new System.Drawing.Size(18, 21);
            this.btnAddCooldownGroup.TabIndex = 55;
            this.btnAddCooldownGroup.Text = "+";
            this.btnAddCooldownGroup.Click += new System.EventHandler(this.btnAddCooldownGroup_Click);
            // 
            // cmbCooldownGroup
            // 
            this.cmbCooldownGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbCooldownGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbCooldownGroup.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbCooldownGroup.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbCooldownGroup.DrawDropdownHoverOutline = false;
            this.cmbCooldownGroup.DrawFocusRectangle = false;
            this.cmbCooldownGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCooldownGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCooldownGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCooldownGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbCooldownGroup.FormattingEnabled = true;
            this.cmbCooldownGroup.Location = new System.Drawing.Point(223, 113);
            this.cmbCooldownGroup.Name = "cmbCooldownGroup";
            this.cmbCooldownGroup.Size = new System.Drawing.Size(159, 21);
            this.cmbCooldownGroup.TabIndex = 54;
            this.cmbCooldownGroup.Text = null;
            this.cmbCooldownGroup.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCooldownGroup.SelectedIndexChanged += new System.EventHandler(this.cmbCooldownGroup_SelectedIndexChanged);
            // 
            // lblCooldownGroup
            // 
            this.lblCooldownGroup.AutoSize = true;
            this.lblCooldownGroup.Location = new System.Drawing.Point(220, 96);
            this.lblCooldownGroup.Name = "lblCooldownGroup";
            this.lblCooldownGroup.Size = new System.Drawing.Size(89, 13);
            this.lblCooldownGroup.TabIndex = 53;
            this.lblCooldownGroup.Text = "Cooldown Group:";
            // 
            // nudCooldownDuration
            // 
            this.nudCooldownDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCooldownDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCooldownDuration.Location = new System.Drawing.Point(222, 71);
            this.nudCooldownDuration.Maximum = new decimal(new int[] {
            -100,
            49,
            0,
            0});
            this.nudCooldownDuration.Name = "nudCooldownDuration";
            this.nudCooldownDuration.Size = new System.Drawing.Size(184, 20);
            this.nudCooldownDuration.TabIndex = 39;
            this.nudCooldownDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCooldownDuration.ValueChanged += new System.EventHandler(this.nudCooldownDuration_ValueChanged);
            // 
            // nudCastDuration
            // 
            this.nudCastDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCastDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCastDuration.Location = new System.Drawing.Point(222, 32);
            this.nudCastDuration.Maximum = new decimal(new int[] {
            -100,
            49,
            0,
            0});
            this.nudCastDuration.Name = "nudCastDuration";
            this.nudCastDuration.Size = new System.Drawing.Size(184, 20);
            this.nudCastDuration.TabIndex = 38;
            this.nudCastDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCastDuration.ValueChanged += new System.EventHandler(this.nudCastDuration_ValueChanged);
            // 
            // nudMpCost
            // 
            this.nudMpCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMpCost.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMpCost.Location = new System.Drawing.Point(10, 71);
            this.nudMpCost.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMpCost.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudMpCost.Name = "nudMpCost";
            this.nudMpCost.Size = new System.Drawing.Size(184, 20);
            this.nudMpCost.TabIndex = 37;
            this.nudMpCost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMpCost.ValueChanged += new System.EventHandler(this.nudMpCost_ValueChanged);
            // 
            // nudHPCost
            // 
            this.nudHPCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHPCost.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHPCost.Location = new System.Drawing.Point(10, 32);
            this.nudHPCost.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudHPCost.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudHPCost.Name = "nudHPCost";
            this.nudHPCost.Size = new System.Drawing.Size(184, 20);
            this.nudHPCost.TabIndex = 36;
            this.nudHPCost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHPCost.ValueChanged += new System.EventHandler(this.nudHPCost_ValueChanged);
            // 
            // lblMPCost
            // 
            this.lblMPCost.AutoSize = true;
            this.lblMPCost.Location = new System.Drawing.Point(6, 55);
            this.lblMPCost.Name = "lblMPCost";
            this.lblMPCost.Size = new System.Drawing.Size(61, 13);
            this.lblMPCost.TabIndex = 23;
            this.lblMPCost.Text = "Mana Cost:";
            // 
            // lblHPCost
            // 
            this.lblHPCost.AutoSize = true;
            this.lblHPCost.Location = new System.Drawing.Point(6, 16);
            this.lblHPCost.Name = "lblHPCost";
            this.lblHPCost.Size = new System.Drawing.Size(49, 13);
            this.lblHPCost.TabIndex = 22;
            this.lblHPCost.Text = "HP Cost:";
            // 
            // lblCastDuration
            // 
            this.lblCastDuration.AutoSize = true;
            this.lblCastDuration.Location = new System.Drawing.Point(219, 16);
            this.lblCastDuration.Name = "lblCastDuration";
            this.lblCastDuration.Size = new System.Drawing.Size(79, 13);
            this.lblCastDuration.TabIndex = 7;
            this.lblCastDuration.Text = "Cast Time (ms):";
            // 
            // lblCooldownDuration
            // 
            this.lblCooldownDuration.AutoSize = true;
            this.lblCooldownDuration.Location = new System.Drawing.Point(219, 55);
            this.lblCooldownDuration.Name = "lblCooldownDuration";
            this.lblCooldownDuration.Size = new System.Drawing.Size(79, 13);
            this.lblCooldownDuration.TabIndex = 12;
            this.lblCooldownDuration.Text = "Cooldown (ms):";
            // 
            // grpRequirements
            // 
            this.grpRequirements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRequirements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRequirements.Controls.Add(this.lblCannotCast);
            this.grpRequirements.Controls.Add(this.txtCannotCast);
            this.grpRequirements.Controls.Add(this.btnDynamicRequirements);
            this.grpRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRequirements.Location = new System.Drawing.Point(446, 418);
            this.grpRequirements.Name = "grpRequirements";
            this.grpRequirements.Size = new System.Drawing.Size(220, 98);
            this.grpRequirements.TabIndex = 18;
            this.grpRequirements.TabStop = false;
            this.grpRequirements.Text = "Casting Requirements";
            // 
            // lblCannotCast
            // 
            this.lblCannotCast.AutoSize = true;
            this.lblCannotCast.Location = new System.Drawing.Point(8, 51);
            this.lblCannotCast.Name = "lblCannotCast";
            this.lblCannotCast.Size = new System.Drawing.Size(114, 13);
            this.lblCannotCast.TabIndex = 56;
            this.lblCannotCast.Text = "Cannot Cast Message:";
            // 
            // txtCannotCast
            // 
            this.txtCannotCast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCannotCast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCannotCast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCannotCast.Location = new System.Drawing.Point(11, 67);
            this.txtCannotCast.Name = "txtCannotCast";
            this.txtCannotCast.Size = new System.Drawing.Size(202, 20);
            this.txtCannotCast.TabIndex = 55;
            this.txtCannotCast.TextChanged += new System.EventHandler(this.txtCannotCast_TextChanged);
            // 
            // btnDynamicRequirements
            // 
            this.btnDynamicRequirements.Location = new System.Drawing.Point(11, 18);
            this.btnDynamicRequirements.Name = "btnDynamicRequirements";
            this.btnDynamicRequirements.Padding = new System.Windows.Forms.Padding(5);
            this.btnDynamicRequirements.Size = new System.Drawing.Size(202, 23);
            this.btnDynamicRequirements.TabIndex = 20;
            this.btnDynamicRequirements.Text = "Casting Requirements";
            this.btnDynamicRequirements.Click += new System.EventHandler(this.btnDynamicRequirements_Click);
            // 
            // grpCombat
            // 
            this.grpCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCombat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCombat.Controls.Add(this.grpAdvTrapSettings);
            this.grpCombat.Controls.Add(this.grpAreaDenial);
            this.grpCombat.Controls.Add(this.grpAdvAoe);
            this.grpCombat.Controls.Add(this.grpBalanceHelp);
            this.grpCombat.Controls.Add(this.grpBonusEffects);
            this.grpCombat.Controls.Add(this.grpDamageTypes);
            this.grpCombat.Controls.Add(this.grpStats);
            this.grpCombat.Controls.Add(this.grpHotDot);
            this.grpCombat.Controls.Add(this.grpEffect);
            this.grpCombat.Controls.Add(this.grpEffectDuration);
            this.grpCombat.Controls.Add(this.grpDamage);
            this.grpCombat.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCombat.Location = new System.Drawing.Point(3, 559);
            this.grpCombat.Name = "grpCombat";
            this.grpCombat.Size = new System.Drawing.Size(636, 730);
            this.grpCombat.TabIndex = 39;
            this.grpCombat.TabStop = false;
            this.grpCombat.Text = "Combat Spell";
            this.grpCombat.Visible = false;
            // 
            // grpAdvTrapSettings
            // 
            this.grpAdvTrapSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAdvTrapSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAdvTrapSettings.Controls.Add(this.nudTrapCooldown);
            this.grpAdvTrapSettings.Controls.Add(this.lblTrapCooldown);
            this.grpAdvTrapSettings.Controls.Add(this.chkNotSingleUseTrap);
            this.grpAdvTrapSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAdvTrapSettings.Location = new System.Drawing.Point(444, 461);
            this.grpAdvTrapSettings.Margin = new System.Windows.Forms.Padding(2);
            this.grpAdvTrapSettings.Name = "grpAdvTrapSettings";
            this.grpAdvTrapSettings.Padding = new System.Windows.Forms.Padding(2);
            this.grpAdvTrapSettings.Size = new System.Drawing.Size(184, 100);
            this.grpAdvTrapSettings.TabIndex = 128;
            this.grpAdvTrapSettings.TabStop = false;
            this.grpAdvTrapSettings.Text = "Advanced Trap Settings";
            // 
            // nudTrapCooldown
            // 
            this.nudTrapCooldown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTrapCooldown.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTrapCooldown.Location = new System.Drawing.Point(5, 63);
            this.nudTrapCooldown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudTrapCooldown.Name = "nudTrapCooldown";
            this.nudTrapCooldown.Size = new System.Drawing.Size(169, 20);
            this.nudTrapCooldown.TabIndex = 71;
            this.nudTrapCooldown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTrapCooldown.ValueChanged += new System.EventHandler(this.nudTrapCooldown_ValueChanged);
            // 
            // lblTrapCooldown
            // 
            this.lblTrapCooldown.AutoSize = true;
            this.lblTrapCooldown.Location = new System.Drawing.Point(3, 44);
            this.lblTrapCooldown.Name = "lblTrapCooldown";
            this.lblTrapCooldown.Size = new System.Drawing.Size(76, 13);
            this.lblTrapCooldown.TabIndex = 70;
            this.lblTrapCooldown.Text = "Cooldown (ms)";
            // 
            // chkNotSingleUseTrap
            // 
            this.chkNotSingleUseTrap.AutoSize = true;
            this.chkNotSingleUseTrap.Location = new System.Drawing.Point(5, 22);
            this.chkNotSingleUseTrap.Name = "chkNotSingleUseTrap";
            this.chkNotSingleUseTrap.Size = new System.Drawing.Size(147, 17);
            this.chkNotSingleUseTrap.TabIndex = 69;
            this.chkNotSingleUseTrap.Text = "Continue after activation?";
            this.chkNotSingleUseTrap.CheckedChanged += new System.EventHandler(this.chkNotSingleUseTrap_CheckedChanged);
            // 
            // grpAreaDenial
            // 
            this.grpAreaDenial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAreaDenial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAreaDenial.Controls.Add(this.grpAreaDenialSettings);
            this.grpAreaDenial.Controls.Add(this.chkAreaDenial);
            this.grpAreaDenial.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAreaDenial.Location = new System.Drawing.Point(205, 592);
            this.grpAreaDenial.Margin = new System.Windows.Forms.Padding(2);
            this.grpAreaDenial.Name = "grpAreaDenial";
            this.grpAreaDenial.Padding = new System.Windows.Forms.Padding(2);
            this.grpAreaDenial.Size = new System.Drawing.Size(236, 126);
            this.grpAreaDenial.TabIndex = 127;
            this.grpAreaDenial.TabStop = false;
            this.grpAreaDenial.Text = "Area Denial";
            // 
            // grpAreaDenialSettings
            // 
            this.grpAreaDenialSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAreaDenialSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAreaDenialSettings.Controls.Add(this.nudAreaSpawnIntensity);
            this.grpAreaDenialSettings.Controls.Add(this.lblAreaDenialRadius);
            this.grpAreaDenialSettings.Controls.Add(this.lblAreaDenialSpawnIntensity);
            this.grpAreaDenialSettings.Controls.Add(this.nudAreaDenialRadius);
            this.grpAreaDenialSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAreaDenialSettings.Location = new System.Drawing.Point(6, 39);
            this.grpAreaDenialSettings.Margin = new System.Windows.Forms.Padding(2);
            this.grpAreaDenialSettings.Name = "grpAreaDenialSettings";
            this.grpAreaDenialSettings.Padding = new System.Windows.Forms.Padding(2);
            this.grpAreaDenialSettings.Size = new System.Drawing.Size(226, 79);
            this.grpAreaDenialSettings.TabIndex = 127;
            this.grpAreaDenialSettings.TabStop = false;
            this.grpAreaDenialSettings.Text = "Area Denial Settings";
            // 
            // nudAreaSpawnIntensity
            // 
            this.nudAreaSpawnIntensity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAreaSpawnIntensity.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAreaSpawnIntensity.Location = new System.Drawing.Point(106, 45);
            this.nudAreaSpawnIntensity.Name = "nudAreaSpawnIntensity";
            this.nudAreaSpawnIntensity.Size = new System.Drawing.Size(116, 20);
            this.nudAreaSpawnIntensity.TabIndex = 66;
            this.nudAreaSpawnIntensity.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAreaSpawnIntensity.ValueChanged += new System.EventHandler(this.nudAreaSpawnIntensity_ValueChanged);
            // 
            // lblAreaDenialRadius
            // 
            this.lblAreaDenialRadius.AutoSize = true;
            this.lblAreaDenialRadius.Location = new System.Drawing.Point(8, 20);
            this.lblAreaDenialRadius.Name = "lblAreaDenialRadius";
            this.lblAreaDenialRadius.Size = new System.Drawing.Size(56, 13);
            this.lblAreaDenialRadius.TabIndex = 65;
            this.lblAreaDenialRadius.Text = "Hit Radius";
            // 
            // lblAreaDenialSpawnIntensity
            // 
            this.lblAreaDenialSpawnIntensity.AutoSize = true;
            this.lblAreaDenialSpawnIntensity.Location = new System.Drawing.Point(7, 47);
            this.lblAreaDenialSpawnIntensity.Name = "lblAreaDenialSpawnIntensity";
            this.lblAreaDenialSpawnIntensity.Size = new System.Drawing.Size(82, 13);
            this.lblAreaDenialSpawnIntensity.TabIndex = 63;
            this.lblAreaDenialSpawnIntensity.Text = "Spawn Intensity";
            // 
            // nudAreaDenialRadius
            // 
            this.nudAreaDenialRadius.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAreaDenialRadius.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAreaDenialRadius.Location = new System.Drawing.Point(106, 18);
            this.nudAreaDenialRadius.Name = "nudAreaDenialRadius";
            this.nudAreaDenialRadius.Size = new System.Drawing.Size(116, 20);
            this.nudAreaDenialRadius.TabIndex = 64;
            this.nudAreaDenialRadius.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAreaDenialRadius.ValueChanged += new System.EventHandler(this.nudAreaDenialRadius_ValueChanged);
            // 
            // chkAreaDenial
            // 
            this.chkAreaDenial.AutoSize = true;
            this.chkAreaDenial.Location = new System.Drawing.Point(9, 18);
            this.chkAreaDenial.Name = "chkAreaDenial";
            this.chkAreaDenial.Size = new System.Drawing.Size(155, 17);
            this.chkAreaDenial.TabIndex = 61;
            this.chkAreaDenial.Text = "Create Area Denial in AoE?";
            this.chkAreaDenial.CheckedChanged += new System.EventHandler(this.chkAreaDenial_CheckedChanged);
            // 
            // grpAdvAoe
            // 
            this.grpAdvAoe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAdvAoe.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAdvAoe.Controls.Add(this.grpRectangleSettings);
            this.grpAdvAoe.Controls.Add(this.chkAoeRelativeDir);
            this.grpAdvAoe.Controls.Add(this.lblAoeYOffset);
            this.grpAdvAoe.Controls.Add(this.nudAoeYOffset);
            this.grpAdvAoe.Controls.Add(this.lblAoeXOffset);
            this.grpAdvAoe.Controls.Add(this.nudAoeXOffset);
            this.grpAdvAoe.Controls.Add(this.lblAoeShape);
            this.grpAdvAoe.Controls.Add(this.cmbAoeShape);
            this.grpAdvAoe.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAdvAoe.Location = new System.Drawing.Point(6, 505);
            this.grpAdvAoe.Margin = new System.Windows.Forms.Padding(2);
            this.grpAdvAoe.Name = "grpAdvAoe";
            this.grpAdvAoe.Padding = new System.Windows.Forms.Padding(2);
            this.grpAdvAoe.Size = new System.Drawing.Size(195, 213);
            this.grpAdvAoe.TabIndex = 125;
            this.grpAdvAoe.TabStop = false;
            this.grpAdvAoe.Text = "Advanced AoE Settings";
            // 
            // grpRectangleSettings
            // 
            this.grpRectangleSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRectangleSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRectangleSettings.Controls.Add(this.lblRectLbl);
            this.grpRectangleSettings.Controls.Add(this.lblAoeRectWidth);
            this.grpRectangleSettings.Controls.Add(this.lblAoeRectHeight);
            this.grpRectangleSettings.Controls.Add(this.nudAoeHeight);
            this.grpRectangleSettings.Controls.Add(this.nudAoeWidth);
            this.grpRectangleSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRectangleSettings.Location = new System.Drawing.Point(8, 65);
            this.grpRectangleSettings.Margin = new System.Windows.Forms.Padding(2);
            this.grpRectangleSettings.Name = "grpRectangleSettings";
            this.grpRectangleSettings.Padding = new System.Windows.Forms.Padding(2);
            this.grpRectangleSettings.Size = new System.Drawing.Size(181, 79);
            this.grpRectangleSettings.TabIndex = 126;
            this.grpRectangleSettings.TabStop = false;
            this.grpRectangleSettings.Text = "Rectangle Settings";
            // 
            // lblRectLbl
            // 
            this.lblRectLbl.AutoSize = true;
            this.lblRectLbl.ForeColor = System.Drawing.Color.LightSlateGray;
            this.lblRectLbl.Location = new System.Drawing.Point(5, 58);
            this.lblRectLbl.Name = "lblRectLbl";
            this.lblRectLbl.Size = new System.Drawing.Size(137, 13);
            this.lblRectLbl.TabIndex = 130;
            this.lblRectLbl.Text = "*Bottom-Left North Oriented";
            // 
            // lblAoeRectWidth
            // 
            this.lblAoeRectWidth.AutoSize = true;
            this.lblAoeRectWidth.Location = new System.Drawing.Point(8, 17);
            this.lblAoeRectWidth.Name = "lblAoeRectWidth";
            this.lblAoeRectWidth.Size = new System.Drawing.Size(35, 13);
            this.lblAoeRectWidth.TabIndex = 129;
            this.lblAoeRectWidth.Text = "Width";
            // 
            // lblAoeRectHeight
            // 
            this.lblAoeRectHeight.AutoSize = true;
            this.lblAoeRectHeight.Location = new System.Drawing.Point(95, 17);
            this.lblAoeRectHeight.Name = "lblAoeRectHeight";
            this.lblAoeRectHeight.Size = new System.Drawing.Size(38, 13);
            this.lblAoeRectHeight.TabIndex = 128;
            this.lblAoeRectHeight.Text = "Height";
            // 
            // nudAoeHeight
            // 
            this.nudAoeHeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAoeHeight.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAoeHeight.Location = new System.Drawing.Point(92, 35);
            this.nudAoeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAoeHeight.Name = "nudAoeHeight";
            this.nudAoeHeight.Size = new System.Drawing.Size(80, 20);
            this.nudAoeHeight.TabIndex = 127;
            this.nudAoeHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAoeHeight.ValueChanged += new System.EventHandler(this.nudAoeHeight_ValueChanged);
            // 
            // nudAoeWidth
            // 
            this.nudAoeWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAoeWidth.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAoeWidth.Location = new System.Drawing.Point(5, 35);
            this.nudAoeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAoeWidth.Name = "nudAoeWidth";
            this.nudAoeWidth.Size = new System.Drawing.Size(80, 20);
            this.nudAoeWidth.TabIndex = 127;
            this.nudAoeWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAoeWidth.ValueChanged += new System.EventHandler(this.nudAoeWidth_ValueChanged);
            // 
            // chkAoeRelativeDir
            // 
            this.chkAoeRelativeDir.AutoSize = true;
            this.chkAoeRelativeDir.Location = new System.Drawing.Point(13, 188);
            this.chkAoeRelativeDir.Name = "chkAoeRelativeDir";
            this.chkAoeRelativeDir.Size = new System.Drawing.Size(128, 17);
            this.chkAoeRelativeDir.TabIndex = 60;
            this.chkAoeRelativeDir.Text = "Relative to Direction?";
            this.chkAoeRelativeDir.CheckedChanged += new System.EventHandler(this.chkAoeRelativeDir_CheckedChanged);
            // 
            // lblAoeYOffset
            // 
            this.lblAoeYOffset.AutoSize = true;
            this.lblAoeYOffset.Location = new System.Drawing.Point(108, 146);
            this.lblAoeYOffset.Name = "lblAoeYOffset";
            this.lblAoeYOffset.Size = new System.Drawing.Size(45, 13);
            this.lblAoeYOffset.TabIndex = 59;
            this.lblAoeYOffset.Text = "Y Offset";
            // 
            // nudAoeYOffset
            // 
            this.nudAoeYOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAoeYOffset.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAoeYOffset.Location = new System.Drawing.Point(108, 162);
            this.nudAoeYOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudAoeYOffset.Name = "nudAoeYOffset";
            this.nudAoeYOffset.Size = new System.Drawing.Size(80, 20);
            this.nudAoeYOffset.TabIndex = 58;
            this.nudAoeYOffset.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAoeYOffset.ValueChanged += new System.EventHandler(this.nudAoeYOffset_ValueChanged);
            // 
            // lblAoeXOffset
            // 
            this.lblAoeXOffset.AutoSize = true;
            this.lblAoeXOffset.Location = new System.Drawing.Point(10, 146);
            this.lblAoeXOffset.Name = "lblAoeXOffset";
            this.lblAoeXOffset.Size = new System.Drawing.Size(45, 13);
            this.lblAoeXOffset.TabIndex = 57;
            this.lblAoeXOffset.Text = "X Offset";
            // 
            // nudAoeXOffset
            // 
            this.nudAoeXOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAoeXOffset.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAoeXOffset.Location = new System.Drawing.Point(14, 162);
            this.nudAoeXOffset.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudAoeXOffset.Name = "nudAoeXOffset";
            this.nudAoeXOffset.Size = new System.Drawing.Size(80, 20);
            this.nudAoeXOffset.TabIndex = 54;
            this.nudAoeXOffset.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAoeXOffset.ValueChanged += new System.EventHandler(this.nudAoeXOffset_ValueChanged);
            // 
            // lblAoeShape
            // 
            this.lblAoeShape.AutoSize = true;
            this.lblAoeShape.Location = new System.Drawing.Point(9, 19);
            this.lblAoeShape.Name = "lblAoeShape";
            this.lblAoeShape.Size = new System.Drawing.Size(38, 13);
            this.lblAoeShape.TabIndex = 53;
            this.lblAoeShape.Text = "Shape";
            // 
            // cmbAoeShape
            // 
            this.cmbAoeShape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAoeShape.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAoeShape.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAoeShape.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAoeShape.DrawDropdownHoverOutline = false;
            this.cmbAoeShape.DrawFocusRectangle = false;
            this.cmbAoeShape.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAoeShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAoeShape.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAoeShape.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAoeShape.FormattingEnabled = true;
            this.cmbAoeShape.Location = new System.Drawing.Point(9, 35);
            this.cmbAoeShape.Name = "cmbAoeShape";
            this.cmbAoeShape.Size = new System.Drawing.Size(179, 21);
            this.cmbAoeShape.TabIndex = 52;
            this.cmbAoeShape.Text = null;
            this.cmbAoeShape.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAoeShape.SelectedIndexChanged += new System.EventHandler(this.cmbAoeShape_SelectedIndexChanged);
            // 
            // grpBalanceHelp
            // 
            this.grpBalanceHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBalanceHelp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBalanceHelp.Controls.Add(this.lblMaxHitVal);
            this.grpBalanceHelp.Controls.Add(this.lblMaxHit);
            this.grpBalanceHelp.Controls.Add(this.nudMockMagic);
            this.grpBalanceHelp.Controls.Add(this.nudMockPierce);
            this.grpBalanceHelp.Controls.Add(this.mockMagic);
            this.grpBalanceHelp.Controls.Add(this.mockPierce);
            this.grpBalanceHelp.Controls.Add(this.nudMockSlash);
            this.grpBalanceHelp.Controls.Add(this.lblMockSlash);
            this.grpBalanceHelp.Controls.Add(this.nudMockBlunt);
            this.grpBalanceHelp.Controls.Add(this.lblMockBlunt);
            this.grpBalanceHelp.Controls.Add(this.lblProjectedDpsVal);
            this.grpBalanceHelp.Controls.Add(this.lblTargetDpsVal);
            this.grpBalanceHelp.Controls.Add(this.lblTargetDps);
            this.grpBalanceHelp.Controls.Add(this.lblProejctedDps);
            this.grpBalanceHelp.Controls.Add(this.lblTier);
            this.grpBalanceHelp.Controls.Add(this.darkComboBox1);
            this.grpBalanceHelp.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBalanceHelp.Location = new System.Drawing.Point(443, 213);
            this.grpBalanceHelp.Margin = new System.Windows.Forms.Padding(2);
            this.grpBalanceHelp.Name = "grpBalanceHelp";
            this.grpBalanceHelp.Padding = new System.Windows.Forms.Padding(2);
            this.grpBalanceHelp.Size = new System.Drawing.Size(188, 239);
            this.grpBalanceHelp.TabIndex = 120;
            this.grpBalanceHelp.TabStop = false;
            this.grpBalanceHelp.Text = "Balance Help";
            // 
            // lblMaxHitVal
            // 
            this.lblMaxHitVal.AutoSize = true;
            this.lblMaxHitVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHitVal.Location = new System.Drawing.Point(104, 121);
            this.lblMaxHitVal.Name = "lblMaxHitVal";
            this.lblMaxHitVal.Size = new System.Drawing.Size(18, 20);
            this.lblMaxHitVal.TabIndex = 124;
            this.lblMaxHitVal.Text = "0";
            // 
            // lblMaxHit
            // 
            this.lblMaxHit.AutoSize = true;
            this.lblMaxHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHit.Location = new System.Drawing.Point(103, 106);
            this.lblMaxHit.Name = "lblMaxHit";
            this.lblMaxHit.Size = new System.Drawing.Size(50, 13);
            this.lblMaxHit.TabIndex = 123;
            this.lblMaxHit.Text = "Max Hit";
            // 
            // nudMockMagic
            // 
            this.nudMockMagic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMockMagic.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMockMagic.Location = new System.Drawing.Point(89, 211);
            this.nudMockMagic.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMockMagic.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMockMagic.Name = "nudMockMagic";
            this.nudMockMagic.Size = new System.Drawing.Size(68, 20);
            this.nudMockMagic.TabIndex = 122;
            this.nudMockMagic.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMockMagic.ValueChanged += new System.EventHandler(this.nudMockMagic_ValueChanged);
            // 
            // nudMockPierce
            // 
            this.nudMockPierce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMockPierce.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMockPierce.Location = new System.Drawing.Point(8, 210);
            this.nudMockPierce.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMockPierce.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMockPierce.Name = "nudMockPierce";
            this.nudMockPierce.Size = new System.Drawing.Size(68, 20);
            this.nudMockPierce.TabIndex = 121;
            this.nudMockPierce.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMockPierce.ValueChanged += new System.EventHandler(this.nudMockPierce_ValueChanged);
            // 
            // mockMagic
            // 
            this.mockMagic.AutoSize = true;
            this.mockMagic.Location = new System.Drawing.Point(86, 194);
            this.mockMagic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mockMagic.Name = "mockMagic";
            this.mockMagic.Size = new System.Drawing.Size(66, 13);
            this.mockMagic.TabIndex = 120;
            this.mockMagic.Text = "Mock Magic";
            // 
            // mockPierce
            // 
            this.mockPierce.AutoSize = true;
            this.mockPierce.Location = new System.Drawing.Point(6, 194);
            this.mockPierce.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mockPierce.Name = "mockPierce";
            this.mockPierce.Size = new System.Drawing.Size(67, 13);
            this.mockPierce.TabIndex = 119;
            this.mockPierce.Text = "Mock Pierce";
            // 
            // nudMockSlash
            // 
            this.nudMockSlash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMockSlash.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMockSlash.Location = new System.Drawing.Point(89, 168);
            this.nudMockSlash.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMockSlash.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMockSlash.Name = "nudMockSlash";
            this.nudMockSlash.Size = new System.Drawing.Size(68, 20);
            this.nudMockSlash.TabIndex = 118;
            this.nudMockSlash.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMockSlash.ValueChanged += new System.EventHandler(this.nudMockSlash_ValueChanged);
            // 
            // lblMockSlash
            // 
            this.lblMockSlash.AutoSize = true;
            this.lblMockSlash.Location = new System.Drawing.Point(86, 152);
            this.lblMockSlash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMockSlash.Name = "lblMockSlash";
            this.lblMockSlash.Size = new System.Drawing.Size(63, 13);
            this.lblMockSlash.TabIndex = 117;
            this.lblMockSlash.Text = "Mock Slash";
            // 
            // nudMockBlunt
            // 
            this.nudMockBlunt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMockBlunt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMockBlunt.Location = new System.Drawing.Point(8, 168);
            this.nudMockBlunt.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMockBlunt.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMockBlunt.Name = "nudMockBlunt";
            this.nudMockBlunt.Size = new System.Drawing.Size(68, 20);
            this.nudMockBlunt.TabIndex = 116;
            this.nudMockBlunt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMockBlunt.ValueChanged += new System.EventHandler(this.nudMockBlunt_ValueChanged);
            // 
            // lblMockBlunt
            // 
            this.lblMockBlunt.AutoSize = true;
            this.lblMockBlunt.Location = new System.Drawing.Point(6, 152);
            this.lblMockBlunt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMockBlunt.Name = "lblMockBlunt";
            this.lblMockBlunt.Size = new System.Drawing.Size(61, 13);
            this.lblMockBlunt.TabIndex = 115;
            this.lblMockBlunt.Text = "Mock Blunt";
            // 
            // lblProjectedDpsVal
            // 
            this.lblProjectedDpsVal.AutoSize = true;
            this.lblProjectedDpsVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectedDpsVal.Location = new System.Drawing.Point(15, 121);
            this.lblProjectedDpsVal.Name = "lblProjectedDpsVal";
            this.lblProjectedDpsVal.Size = new System.Drawing.Size(18, 20);
            this.lblProjectedDpsVal.TabIndex = 56;
            this.lblProjectedDpsVal.Text = "0";
            // 
            // lblTargetDpsVal
            // 
            this.lblTargetDpsVal.AutoSize = true;
            this.lblTargetDpsVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetDpsVal.Location = new System.Drawing.Point(15, 78);
            this.lblTargetDpsVal.Name = "lblTargetDpsVal";
            this.lblTargetDpsVal.Size = new System.Drawing.Size(18, 20);
            this.lblTargetDpsVal.TabIndex = 55;
            this.lblTargetDpsVal.Text = "0";
            // 
            // lblTargetDps
            // 
            this.lblTargetDps.AutoSize = true;
            this.lblTargetDps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTargetDps.Location = new System.Drawing.Point(12, 60);
            this.lblTargetDps.Name = "lblTargetDps";
            this.lblTargetDps.Size = new System.Drawing.Size(73, 13);
            this.lblTargetDps.TabIndex = 54;
            this.lblTargetDps.Text = "Target DPS";
            // 
            // lblProejctedDps
            // 
            this.lblProejctedDps.AutoSize = true;
            this.lblProejctedDps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProejctedDps.Location = new System.Drawing.Point(12, 106);
            this.lblProejctedDps.Name = "lblProejctedDps";
            this.lblProejctedDps.Size = new System.Drawing.Size(58, 13);
            this.lblProejctedDps.TabIndex = 53;
            this.lblProejctedDps.Text = "Est. DPS";
            // 
            // lblTier
            // 
            this.lblTier.AutoSize = true;
            this.lblTier.Location = new System.Drawing.Point(12, 18);
            this.lblTier.Name = "lblTier";
            this.lblTier.Size = new System.Drawing.Size(25, 13);
            this.lblTier.TabIndex = 52;
            this.lblTier.Text = "Tier";
            // 
            // darkComboBox1
            // 
            this.darkComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.darkComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.darkComboBox1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.darkComboBox1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.darkComboBox1.DrawDropdownHoverOutline = false;
            this.darkComboBox1.DrawFocusRectangle = false;
            this.darkComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.darkComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.darkComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darkComboBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.darkComboBox1.FormattingEnabled = true;
            this.darkComboBox1.Location = new System.Drawing.Point(9, 36);
            this.darkComboBox1.Name = "darkComboBox1";
            this.darkComboBox1.Size = new System.Drawing.Size(170, 21);
            this.darkComboBox1.TabIndex = 51;
            this.darkComboBox1.Text = null;
            this.darkComboBox1.TextPadding = new System.Windows.Forms.Padding(2);
            this.darkComboBox1.SelectedIndexChanged += new System.EventHandler(this.darkComboBox1_SelectedIndexChanged);
            // 
            // grpBonusEffects
            // 
            this.grpBonusEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBonusEffects.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBonusEffects.Controls.Add(this.lstBonusEffects);
            this.grpBonusEffects.Controls.Add(this.nudBonusAmt);
            this.grpBonusEffects.Controls.Add(this.lblBonusAmt);
            this.grpBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBonusEffects.Location = new System.Drawing.Point(206, 400);
            this.grpBonusEffects.Name = "grpBonusEffects";
            this.grpBonusEffects.Size = new System.Drawing.Size(226, 170);
            this.grpBonusEffects.TabIndex = 53;
            this.grpBonusEffects.TabStop = false;
            this.grpBonusEffects.Text = "Bonuses";
            // 
            // lstBonusEffects
            // 
            this.lstBonusEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBonusEffects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBonusEffects.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBonusEffects.FormattingEnabled = true;
            this.lstBonusEffects.Location = new System.Drawing.Point(13, 24);
            this.lstBonusEffects.Name = "lstBonusEffects";
            this.lstBonusEffects.Size = new System.Drawing.Size(201, 106);
            this.lstBonusEffects.TabIndex = 109;
            // 
            // nudBonusAmt
            // 
            this.nudBonusAmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBonusAmt.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBonusAmt.Location = new System.Drawing.Point(111, 138);
            this.nudBonusAmt.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudBonusAmt.Name = "nudBonusAmt";
            this.nudBonusAmt.Size = new System.Drawing.Size(104, 20);
            this.nudBonusAmt.TabIndex = 40;
            this.nudBonusAmt.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBonusAmt.ValueChanged += new System.EventHandler(this.nudBonusAmt_ValueChanged);
            // 
            // lblBonusAmt
            // 
            this.lblBonusAmt.AutoSize = true;
            this.lblBonusAmt.Location = new System.Drawing.Point(11, 140);
            this.lblBonusAmt.Name = "lblBonusAmt";
            this.lblBonusAmt.Size = new System.Drawing.Size(60, 13);
            this.lblBonusAmt.TabIndex = 35;
            this.lblBonusAmt.Text = "Amount (%)";
            // 
            // grpDamageTypes
            // 
            this.grpDamageTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDamageTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDamageTypes.Controls.Add(this.grpSetDamages);
            this.grpDamageTypes.Controls.Add(this.chkInheritStats);
            this.grpDamageTypes.Controls.Add(this.chkDamageMagic);
            this.grpDamageTypes.Controls.Add(this.label14);
            this.grpDamageTypes.Controls.Add(this.chkDamagePierce);
            this.grpDamageTypes.Controls.Add(this.label15);
            this.grpDamageTypes.Controls.Add(this.chkDamageSlash);
            this.grpDamageTypes.Controls.Add(this.label16);
            this.grpDamageTypes.Controls.Add(this.chkDamageBlunt);
            this.grpDamageTypes.Controls.Add(this.label17);
            this.grpDamageTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDamageTypes.Location = new System.Drawing.Point(444, 20);
            this.grpDamageTypes.Margin = new System.Windows.Forms.Padding(2);
            this.grpDamageTypes.Name = "grpDamageTypes";
            this.grpDamageTypes.Padding = new System.Windows.Forms.Padding(2);
            this.grpDamageTypes.Size = new System.Drawing.Size(188, 189);
            this.grpDamageTypes.TabIndex = 119;
            this.grpDamageTypes.TabStop = false;
            this.grpDamageTypes.Text = "Damage Types";
            // 
            // grpSetDamages
            // 
            this.grpSetDamages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSetDamages.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSetDamages.Controls.Add(this.nudMagicDam);
            this.grpSetDamages.Controls.Add(this.lblMagicDamage);
            this.grpSetDamages.Controls.Add(this.nudSlashDam);
            this.grpSetDamages.Controls.Add(this.label13);
            this.grpSetDamages.Controls.Add(this.nudPierceDam);
            this.grpSetDamages.Controls.Add(this.lblPierceDamage);
            this.grpSetDamages.Controls.Add(this.lblBluntDamage);
            this.grpSetDamages.Controls.Add(this.nudBluntDam);
            this.grpSetDamages.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSetDamages.Location = new System.Drawing.Point(7, 82);
            this.grpSetDamages.Name = "grpSetDamages";
            this.grpSetDamages.Size = new System.Drawing.Size(177, 101);
            this.grpSetDamages.TabIndex = 116;
            this.grpSetDamages.TabStop = false;
            this.grpSetDamages.Text = "Static Damage";
            // 
            // nudMagicDam
            // 
            this.nudMagicDam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMagicDam.Enabled = false;
            this.nudMagicDam.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMagicDam.Location = new System.Drawing.Point(94, 71);
            this.nudMagicDam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMagicDam.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMagicDam.Name = "nudMagicDam";
            this.nudMagicDam.Size = new System.Drawing.Size(68, 20);
            this.nudMagicDam.TabIndex = 121;
            this.nudMagicDam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMagicDam.ValueChanged += new System.EventHandler(this.nudMagicDam_ValueChanged);
            // 
            // lblMagicDamage
            // 
            this.lblMagicDamage.AutoSize = true;
            this.lblMagicDamage.Location = new System.Drawing.Point(91, 55);
            this.lblMagicDamage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMagicDamage.Name = "lblMagicDamage";
            this.lblMagicDamage.Size = new System.Drawing.Size(79, 13);
            this.lblMagicDamage.TabIndex = 120;
            this.lblMagicDamage.Text = "Magic Damage";
            // 
            // nudSlashDam
            // 
            this.nudSlashDam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashDam.Enabled = false;
            this.nudSlashDam.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashDam.Location = new System.Drawing.Point(94, 32);
            this.nudSlashDam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlashDam.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudSlashDam.Name = "nudSlashDam";
            this.nudSlashDam.Size = new System.Drawing.Size(68, 20);
            this.nudSlashDam.TabIndex = 119;
            this.nudSlashDam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashDam.ValueChanged += new System.EventHandler(this.nudSlashDam_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(91, 16);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 13);
            this.label13.TabIndex = 118;
            this.label13.Text = "Slash Damage";
            // 
            // nudPierceDam
            // 
            this.nudPierceDam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierceDam.Enabled = false;
            this.nudPierceDam.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierceDam.Location = new System.Drawing.Point(10, 71);
            this.nudPierceDam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierceDam.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudPierceDam.Name = "nudPierceDam";
            this.nudPierceDam.Size = new System.Drawing.Size(68, 20);
            this.nudPierceDam.TabIndex = 117;
            this.nudPierceDam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierceDam.ValueChanged += new System.EventHandler(this.nudPierceDam_ValueChanged);
            // 
            // lblPierceDamage
            // 
            this.lblPierceDamage.AutoSize = true;
            this.lblPierceDamage.Location = new System.Drawing.Point(7, 55);
            this.lblPierceDamage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPierceDamage.Name = "lblPierceDamage";
            this.lblPierceDamage.Size = new System.Drawing.Size(80, 13);
            this.lblPierceDamage.TabIndex = 116;
            this.lblPierceDamage.Text = "Pierce Damage";
            // 
            // lblBluntDamage
            // 
            this.lblBluntDamage.AutoSize = true;
            this.lblBluntDamage.Location = new System.Drawing.Point(7, 16);
            this.lblBluntDamage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBluntDamage.Name = "lblBluntDamage";
            this.lblBluntDamage.Size = new System.Drawing.Size(74, 13);
            this.lblBluntDamage.TabIndex = 114;
            this.lblBluntDamage.Text = "Blunt Damage";
            // 
            // nudBluntDam
            // 
            this.nudBluntDam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBluntDam.Enabled = false;
            this.nudBluntDam.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBluntDam.Location = new System.Drawing.Point(10, 32);
            this.nudBluntDam.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBluntDam.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudBluntDam.Name = "nudBluntDam";
            this.nudBluntDam.Size = new System.Drawing.Size(68, 20);
            this.nudBluntDam.TabIndex = 115;
            this.nudBluntDam.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBluntDam.ValueChanged += new System.EventHandler(this.nudBluntDam_ValueChanged);
            // 
            // chkInheritStats
            // 
            this.chkInheritStats.AutoSize = true;
            this.chkInheritStats.Location = new System.Drawing.Point(34, 59);
            this.chkInheritStats.Name = "chkInheritStats";
            this.chkInheritStats.Size = new System.Drawing.Size(126, 17);
            this.chkInheritStats.TabIndex = 64;
            this.chkInheritStats.Text = "Inherit Weapon Stats";
            this.chkInheritStats.CheckedChanged += new System.EventHandler(this.chkInheritStats_CheckedChanged);
            // 
            // chkDamageMagic
            // 
            this.chkDamageMagic.AutoSize = true;
            this.chkDamageMagic.Location = new System.Drawing.Point(159, 35);
            this.chkDamageMagic.Name = "chkDamageMagic";
            this.chkDamageMagic.Size = new System.Drawing.Size(15, 14);
            this.chkDamageMagic.TabIndex = 106;
            this.chkDamageMagic.CheckedChanged += new System.EventHandler(this.chkDamageMagic_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(150, 19);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 113;
            this.label14.Text = "Magic";
            // 
            // chkDamagePierce
            // 
            this.chkDamagePierce.AutoSize = true;
            this.chkDamagePierce.Location = new System.Drawing.Point(115, 35);
            this.chkDamagePierce.Name = "chkDamagePierce";
            this.chkDamagePierce.Size = new System.Drawing.Size(15, 14);
            this.chkDamagePierce.TabIndex = 105;
            this.chkDamagePierce.CheckedChanged += new System.EventHandler(this.chkDamagePierce_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(101, 19);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 112;
            this.label15.Text = "Piercing";
            // 
            // chkDamageSlash
            // 
            this.chkDamageSlash.AutoSize = true;
            this.chkDamageSlash.Location = new System.Drawing.Point(62, 35);
            this.chkDamageSlash.Name = "chkDamageSlash";
            this.chkDamageSlash.Size = new System.Drawing.Size(15, 14);
            this.chkDamageSlash.TabIndex = 104;
            this.chkDamageSlash.CheckedChanged += new System.EventHandler(this.chkDamageSlash_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(46, 19);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 111;
            this.label16.Text = "Slashing";
            // 
            // chkDamageBlunt
            // 
            this.chkDamageBlunt.AutoSize = true;
            this.chkDamageBlunt.Location = new System.Drawing.Point(12, 35);
            this.chkDamageBlunt.Name = "chkDamageBlunt";
            this.chkDamageBlunt.Size = new System.Drawing.Size(15, 14);
            this.chkDamageBlunt.TabIndex = 65;
            this.chkDamageBlunt.CheckedChanged += new System.EventHandler(this.chkDamageBlunt_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 19);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(31, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "Blunt";
            // 
            // grpStats
            // 
            this.grpStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStats.Controls.Add(this.label22);
            this.grpStats.Controls.Add(this.nudEvasionPercentage);
            this.grpStats.Controls.Add(this.label23);
            this.grpStats.Controls.Add(this.nudEvasion);
            this.grpStats.Controls.Add(this.label24);
            this.grpStats.Controls.Add(this.label19);
            this.grpStats.Controls.Add(this.nudAccuracyPercentage);
            this.grpStats.Controls.Add(this.label20);
            this.grpStats.Controls.Add(this.nudAccuracy);
            this.grpStats.Controls.Add(this.label21);
            this.grpStats.Controls.Add(this.label10);
            this.grpStats.Controls.Add(this.label7);
            this.grpStats.Controls.Add(this.nudPierceResistPercentage);
            this.grpStats.Controls.Add(this.nudPiercePercentage);
            this.grpStats.Controls.Add(this.label11);
            this.grpStats.Controls.Add(this.label8);
            this.grpStats.Controls.Add(this.nudPierceResist);
            this.grpStats.Controls.Add(this.label12);
            this.grpStats.Controls.Add(this.nudPierce);
            this.grpStats.Controls.Add(this.label9);
            this.grpStats.Controls.Add(this.label4);
            this.grpStats.Controls.Add(this.nudSlashResistPercentage);
            this.grpStats.Controls.Add(this.label5);
            this.grpStats.Controls.Add(this.nudSlashResist);
            this.grpStats.Controls.Add(this.label6);
            this.grpStats.Controls.Add(this.label1);
            this.grpStats.Controls.Add(this.nudSlashPercentage);
            this.grpStats.Controls.Add(this.label2);
            this.grpStats.Controls.Add(this.nudSlash);
            this.grpStats.Controls.Add(this.label3);
            this.grpStats.Controls.Add(this.lblPercentage5);
            this.grpStats.Controls.Add(this.lblPercentage4);
            this.grpStats.Controls.Add(this.lblPercentage3);
            this.grpStats.Controls.Add(this.lblPercentage2);
            this.grpStats.Controls.Add(this.lblPercentage1);
            this.grpStats.Controls.Add(this.nudSpdPercentage);
            this.grpStats.Controls.Add(this.nudMRPercentage);
            this.grpStats.Controls.Add(this.nudDefPercentage);
            this.grpStats.Controls.Add(this.nudMagPercentage);
            this.grpStats.Controls.Add(this.nudStrPercentage);
            this.grpStats.Controls.Add(this.lblPlus5);
            this.grpStats.Controls.Add(this.lblPlus4);
            this.grpStats.Controls.Add(this.lblPlus3);
            this.grpStats.Controls.Add(this.lblPlus2);
            this.grpStats.Controls.Add(this.lblPlus1);
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
            this.grpStats.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStats.Location = new System.Drawing.Point(201, 19);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(238, 328);
            this.grpStats.TabIndex = 50;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "Stat Modifiers";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(213, 272);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(15, 13);
            this.label22.TabIndex = 102;
            this.label22.Text = "%";
            // 
            // nudEvasionPercentage
            // 
            this.nudEvasionPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEvasionPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEvasionPercentage.Location = new System.Drawing.Point(165, 270);
            this.nudEvasionPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEvasionPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudEvasionPercentage.Name = "nudEvasionPercentage";
            this.nudEvasionPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudEvasionPercentage.TabIndex = 101;
            this.nudEvasionPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEvasionPercentage.ValueChanged += new System.EventHandler(this.nudEvasionPercentage_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(147, 272);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(13, 13);
            this.label23.TabIndex = 100;
            this.label23.Text = "+";
            // 
            // nudEvasion
            // 
            this.nudEvasion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEvasion.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEvasion.Location = new System.Drawing.Point(82, 270);
            this.nudEvasion.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEvasion.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudEvasion.Name = "nudEvasion";
            this.nudEvasion.Size = new System.Drawing.Size(60, 20);
            this.nudEvasion.TabIndex = 99;
            this.nudEvasion.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEvasion.ValueChanged += new System.EventHandler(this.nudEvasion_ValueChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 275);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 13);
            this.label24.TabIndex = 98;
            this.label24.Text = "Evasion:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(212, 246);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 13);
            this.label19.TabIndex = 97;
            this.label19.Text = "%";
            // 
            // nudAccuracyPercentage
            // 
            this.nudAccuracyPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAccuracyPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAccuracyPercentage.Location = new System.Drawing.Point(165, 244);
            this.nudAccuracyPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAccuracyPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudAccuracyPercentage.Name = "nudAccuracyPercentage";
            this.nudAccuracyPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudAccuracyPercentage.TabIndex = 96;
            this.nudAccuracyPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAccuracyPercentage.ValueChanged += new System.EventHandler(this.nudAccuracyPercentage_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(147, 246);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(13, 13);
            this.label20.TabIndex = 95;
            this.label20.Text = "+";
            // 
            // nudAccuracy
            // 
            this.nudAccuracy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAccuracy.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAccuracy.Location = new System.Drawing.Point(82, 244);
            this.nudAccuracy.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAccuracy.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudAccuracy.Name = "nudAccuracy";
            this.nudAccuracy.Size = new System.Drawing.Size(60, 20);
            this.nudAccuracy.TabIndex = 94;
            this.nudAccuracy.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAccuracy.ValueChanged += new System.EventHandler(this.nudAccuracy_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 249);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 13);
            this.label21.TabIndex = 93;
            this.label21.Text = "Accuracy:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(212, 168);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 82;
            this.label10.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(212, 142);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "%";
            // 
            // nudPierceResistPercentage
            // 
            this.nudPierceResistPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierceResistPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierceResistPercentage.Location = new System.Drawing.Point(165, 166);
            this.nudPierceResistPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPierceResistPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudPierceResistPercentage.Name = "nudPierceResistPercentage";
            this.nudPierceResistPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudPierceResistPercentage.TabIndex = 81;
            this.nudPierceResistPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierceResistPercentage.ValueChanged += new System.EventHandler(this.nudPierceResistPercentage_ValueChanged);
            // 
            // nudPiercePercentage
            // 
            this.nudPiercePercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPiercePercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPiercePercentage.Location = new System.Drawing.Point(165, 140);
            this.nudPiercePercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPiercePercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudPiercePercentage.Name = "nudPiercePercentage";
            this.nudPiercePercentage.Size = new System.Drawing.Size(43, 20);
            this.nudPiercePercentage.TabIndex = 81;
            this.nudPiercePercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPiercePercentage.ValueChanged += new System.EventHandler(this.nudPiercePercentage_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(147, 168);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 80;
            this.label11.Text = "+";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(147, 142);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 80;
            this.label8.Text = "+";
            // 
            // nudPierceResist
            // 
            this.nudPierceResist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierceResist.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierceResist.Location = new System.Drawing.Point(82, 166);
            this.nudPierceResist.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierceResist.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudPierceResist.Name = "nudPierceResist";
            this.nudPierceResist.Size = new System.Drawing.Size(60, 20);
            this.nudPierceResist.TabIndex = 79;
            this.nudPierceResist.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierceResist.ValueChanged += new System.EventHandler(this.nudPierceResist_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 170);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 78;
            this.label12.Text = "Pierce Resist:";
            // 
            // nudPierce
            // 
            this.nudPierce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierce.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierce.Location = new System.Drawing.Point(82, 140);
            this.nudPierce.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierce.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudPierce.Name = "nudPierce";
            this.nudPierce.Size = new System.Drawing.Size(60, 20);
            this.nudPierce.TabIndex = 79;
            this.nudPierce.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierce.ValueChanged += new System.EventHandler(this.nudPierce_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 144);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "Pierce Atk:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 116);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "%";
            // 
            // nudSlashResistPercentage
            // 
            this.nudSlashResistPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashResistPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashResistPercentage.Location = new System.Drawing.Point(165, 114);
            this.nudSlashResistPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSlashResistPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudSlashResistPercentage.Name = "nudSlashResistPercentage";
            this.nudSlashResistPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudSlashResistPercentage.TabIndex = 76;
            this.nudSlashResistPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashResistPercentage.ValueChanged += new System.EventHandler(this.nudSlashResistPercentage_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 116);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "+";
            // 
            // nudSlashResist
            // 
            this.nudSlashResist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashResist.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashResist.Location = new System.Drawing.Point(82, 114);
            this.nudSlashResist.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlashResist.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudSlashResist.Name = "nudSlashResist";
            this.nudSlashResist.Size = new System.Drawing.Size(60, 20);
            this.nudSlashResist.TabIndex = 74;
            this.nudSlashResist.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashResist.ValueChanged += new System.EventHandler(this.nudSlashResist_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 118);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 73;
            this.label6.Text = "Slash Resist:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "%";
            // 
            // nudSlashPercentage
            // 
            this.nudSlashPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashPercentage.Location = new System.Drawing.Point(165, 88);
            this.nudSlashPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSlashPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudSlashPercentage.Name = "nudSlashPercentage";
            this.nudSlashPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudSlashPercentage.TabIndex = 71;
            this.nudSlashPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashPercentage.ValueChanged += new System.EventHandler(this.nudSlashPercentage_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(147, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "+";
            // 
            // nudSlash
            // 
            this.nudSlash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlash.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlash.Location = new System.Drawing.Point(82, 88);
            this.nudSlash.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlash.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudSlash.Name = "nudSlash";
            this.nudSlash.Size = new System.Drawing.Size(60, 20);
            this.nudSlash.TabIndex = 69;
            this.nudSlash.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlash.ValueChanged += new System.EventHandler(this.nudSlash_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 68;
            this.label3.Text = "Slash Atk:";
            // 
            // lblPercentage5
            // 
            this.lblPercentage5.AutoSize = true;
            this.lblPercentage5.Location = new System.Drawing.Point(213, 300);
            this.lblPercentage5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage5.Name = "lblPercentage5";
            this.lblPercentage5.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage5.TabIndex = 67;
            this.lblPercentage5.Text = "%";
            // 
            // lblPercentage4
            // 
            this.lblPercentage4.AutoSize = true;
            this.lblPercentage4.Location = new System.Drawing.Point(211, 220);
            this.lblPercentage4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage4.Name = "lblPercentage4";
            this.lblPercentage4.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage4.TabIndex = 66;
            this.lblPercentage4.Text = "%";
            // 
            // lblPercentage3
            // 
            this.lblPercentage3.AutoSize = true;
            this.lblPercentage3.Location = new System.Drawing.Point(212, 63);
            this.lblPercentage3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage3.Name = "lblPercentage3";
            this.lblPercentage3.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage3.TabIndex = 65;
            this.lblPercentage3.Text = "%";
            // 
            // lblPercentage2
            // 
            this.lblPercentage2.AutoSize = true;
            this.lblPercentage2.Location = new System.Drawing.Point(212, 193);
            this.lblPercentage2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage2.Name = "lblPercentage2";
            this.lblPercentage2.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage2.TabIndex = 64;
            this.lblPercentage2.Text = "%";
            // 
            // lblPercentage1
            // 
            this.lblPercentage1.AutoSize = true;
            this.lblPercentage1.Location = new System.Drawing.Point(212, 38);
            this.lblPercentage1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage1.Name = "lblPercentage1";
            this.lblPercentage1.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage1.TabIndex = 63;
            this.lblPercentage1.Text = "%";
            // 
            // nudSpdPercentage
            // 
            this.nudSpdPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpdPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpdPercentage.Location = new System.Drawing.Point(166, 298);
            this.nudSpdPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSpdPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudSpdPercentage.Name = "nudSpdPercentage";
            this.nudSpdPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudSpdPercentage.TabIndex = 62;
            this.nudSpdPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpdPercentage.ValueChanged += new System.EventHandler(this.nudSpdPercentage_ValueChanged);
            // 
            // nudMRPercentage
            // 
            this.nudMRPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMRPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMRPercentage.Location = new System.Drawing.Point(164, 218);
            this.nudMRPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMRPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudMRPercentage.Name = "nudMRPercentage";
            this.nudMRPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudMRPercentage.TabIndex = 61;
            this.nudMRPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMRPercentage.ValueChanged += new System.EventHandler(this.nudMRPercentage_ValueChanged);
            // 
            // nudDefPercentage
            // 
            this.nudDefPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDefPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDefPercentage.Location = new System.Drawing.Point(165, 62);
            this.nudDefPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDefPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudDefPercentage.Name = "nudDefPercentage";
            this.nudDefPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudDefPercentage.TabIndex = 60;
            this.nudDefPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDefPercentage.ValueChanged += new System.EventHandler(this.nudDefPercentage_ValueChanged);
            // 
            // nudMagPercentage
            // 
            this.nudMagPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMagPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMagPercentage.Location = new System.Drawing.Point(165, 192);
            this.nudMagPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMagPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudMagPercentage.Name = "nudMagPercentage";
            this.nudMagPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudMagPercentage.TabIndex = 59;
            this.nudMagPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMagPercentage.ValueChanged += new System.EventHandler(this.nudMagPercentage_ValueChanged);
            // 
            // nudStrPercentage
            // 
            this.nudStrPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStrPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStrPercentage.Location = new System.Drawing.Point(165, 36);
            this.nudStrPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudStrPercentage.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudStrPercentage.Name = "nudStrPercentage";
            this.nudStrPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudStrPercentage.TabIndex = 58;
            this.nudStrPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStrPercentage.ValueChanged += new System.EventHandler(this.nudStrPercentage_ValueChanged);
            // 
            // lblPlus5
            // 
            this.lblPlus5.AutoSize = true;
            this.lblPlus5.Location = new System.Drawing.Point(148, 300);
            this.lblPlus5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus5.Name = "lblPlus5";
            this.lblPlus5.Size = new System.Drawing.Size(13, 13);
            this.lblPlus5.TabIndex = 57;
            this.lblPlus5.Text = "+";
            // 
            // lblPlus4
            // 
            this.lblPlus4.AutoSize = true;
            this.lblPlus4.Location = new System.Drawing.Point(146, 220);
            this.lblPlus4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus4.Name = "lblPlus4";
            this.lblPlus4.Size = new System.Drawing.Size(13, 13);
            this.lblPlus4.TabIndex = 56;
            this.lblPlus4.Text = "+";
            // 
            // lblPlus3
            // 
            this.lblPlus3.AutoSize = true;
            this.lblPlus3.Location = new System.Drawing.Point(147, 63);
            this.lblPlus3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus3.Name = "lblPlus3";
            this.lblPlus3.Size = new System.Drawing.Size(13, 13);
            this.lblPlus3.TabIndex = 55;
            this.lblPlus3.Text = "+";
            // 
            // lblPlus2
            // 
            this.lblPlus2.AutoSize = true;
            this.lblPlus2.Location = new System.Drawing.Point(147, 193);
            this.lblPlus2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus2.Name = "lblPlus2";
            this.lblPlus2.Size = new System.Drawing.Size(13, 13);
            this.lblPlus2.TabIndex = 54;
            this.lblPlus2.Text = "+";
            // 
            // lblPlus1
            // 
            this.lblPlus1.AutoSize = true;
            this.lblPlus1.Location = new System.Drawing.Point(147, 38);
            this.lblPlus1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus1.Name = "lblPlus1";
            this.lblPlus1.Size = new System.Drawing.Size(13, 13);
            this.lblPlus1.TabIndex = 53;
            this.lblPlus1.Text = "+";
            // 
            // nudSpd
            // 
            this.nudSpd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpd.Location = new System.Drawing.Point(83, 298);
            this.nudSpd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpd.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudSpd.Name = "nudSpd";
            this.nudSpd.Size = new System.Drawing.Size(60, 20);
            this.nudSpd.TabIndex = 52;
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
            this.nudMR.Location = new System.Drawing.Point(81, 218);
            this.nudMR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMR.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMR.Name = "nudMR";
            this.nudMR.Size = new System.Drawing.Size(60, 20);
            this.nudMR.TabIndex = 51;
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
            this.nudDef.Location = new System.Drawing.Point(82, 62);
            this.nudDef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDef.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudDef.Name = "nudDef";
            this.nudDef.Size = new System.Drawing.Size(60, 20);
            this.nudDef.TabIndex = 50;
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
            this.nudMag.Location = new System.Drawing.Point(82, 192);
            this.nudMag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMag.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudMag.Name = "nudMag";
            this.nudMag.Size = new System.Drawing.Size(60, 20);
            this.nudMag.TabIndex = 49;
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
            this.nudStr.Location = new System.Drawing.Point(82, 36);
            this.nudStr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudStr.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(60, 20);
            this.nudStr.TabIndex = 48;
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
            this.lblSpd.Location = new System.Drawing.Point(6, 302);
            this.lblSpd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpd.Name = "lblSpd";
            this.lblSpd.Size = new System.Drawing.Size(41, 13);
            this.lblSpd.TabIndex = 47;
            this.lblSpd.Text = "Speed:";
            // 
            // lblMR
            // 
            this.lblMR.AutoSize = true;
            this.lblMR.Location = new System.Drawing.Point(5, 223);
            this.lblMR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMR.Name = "lblMR";
            this.lblMR.Size = new System.Drawing.Size(71, 13);
            this.lblMR.TabIndex = 46;
            this.lblMR.Text = "Magic Resist:";
            // 
            // lblDef
            // 
            this.lblDef.AutoSize = true;
            this.lblDef.Location = new System.Drawing.Point(5, 66);
            this.lblDef.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDef.Name = "lblDef";
            this.lblDef.Size = new System.Drawing.Size(66, 13);
            this.lblDef.TabIndex = 45;
            this.lblDef.Text = "Blunt Resist:";
            // 
            // lblMag
            // 
            this.lblMag.AutoSize = true;
            this.lblMag.Location = new System.Drawing.Point(6, 196);
            this.lblMag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMag.Name = "lblMag";
            this.lblMag.Size = new System.Drawing.Size(58, 13);
            this.lblMag.TabIndex = 44;
            this.lblMag.Text = "Magic Atk:";
            // 
            // lblStr
            // 
            this.lblStr.AutoSize = true;
            this.lblStr.Location = new System.Drawing.Point(5, 40);
            this.lblStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStr.Name = "lblStr";
            this.lblStr.Size = new System.Drawing.Size(53, 13);
            this.lblStr.TabIndex = 43;
            this.lblStr.Text = "Blunt Atk:";
            // 
            // grpHotDot
            // 
            this.grpHotDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpHotDot.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpHotDot.Controls.Add(this.lblOTanimationDisclaimer);
            this.grpHotDot.Controls.Add(this.lblHOTDOTenableAnimation);
            this.grpHotDot.Controls.Add(this.cmbOverTimeAnimation);
            this.grpHotDot.Controls.Add(this.nudTick);
            this.grpHotDot.Controls.Add(this.chkHOTDOT);
            this.grpHotDot.Controls.Add(this.lblTick);
            this.grpHotDot.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpHotDot.Location = new System.Drawing.Point(6, 365);
            this.grpHotDot.Name = "grpHotDot";
            this.grpHotDot.Size = new System.Drawing.Size(194, 135);
            this.grpHotDot.TabIndex = 53;
            this.grpHotDot.TabStop = false;
            this.grpHotDot.Text = "Heal/Damage Over Time";
            // 
            // lblOTanimationDisclaimer
            // 
            this.lblOTanimationDisclaimer.AutoSize = true;
            this.lblOTanimationDisclaimer.Location = new System.Drawing.Point(0, 110);
            this.lblOTanimationDisclaimer.Name = "lblOTanimationDisclaimer";
            this.lblOTanimationDisclaimer.Size = new System.Drawing.Size(195, 13);
            this.lblOTanimationDisclaimer.TabIndex = 57;
            this.lblOTanimationDisclaimer.Text = "(Set to \"None\" to keep same animation)";
            // 
            // lblHOTDOTenableAnimation
            // 
            this.lblHOTDOTenableAnimation.AutoSize = true;
            this.lblHOTDOTenableAnimation.Location = new System.Drawing.Point(6, 89);
            this.lblHOTDOTenableAnimation.Name = "lblHOTDOTenableAnimation";
            this.lblHOTDOTenableAnimation.Size = new System.Drawing.Size(74, 13);
            this.lblHOTDOTenableAnimation.TabIndex = 56;
            this.lblHOTDOTenableAnimation.Text = "OT Animation:";
            // 
            // cmbOverTimeAnimation
            // 
            this.cmbOverTimeAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbOverTimeAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbOverTimeAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbOverTimeAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbOverTimeAnimation.DrawDropdownHoverOutline = false;
            this.cmbOverTimeAnimation.DrawFocusRectangle = false;
            this.cmbOverTimeAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOverTimeAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOverTimeAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbOverTimeAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbOverTimeAnimation.FormattingEnabled = true;
            this.cmbOverTimeAnimation.Location = new System.Drawing.Point(86, 86);
            this.cmbOverTimeAnimation.Name = "cmbOverTimeAnimation";
            this.cmbOverTimeAnimation.Size = new System.Drawing.Size(96, 21);
            this.cmbOverTimeAnimation.TabIndex = 54;
            this.cmbOverTimeAnimation.Text = null;
            this.cmbOverTimeAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbOverTimeAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbOverTimeAnimation_SelectedIndexChanged);
            // 
            // nudTick
            // 
            this.nudTick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudTick.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudTick.Location = new System.Drawing.Point(99, 40);
            this.nudTick.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nudTick.Name = "nudTick";
            this.nudTick.Size = new System.Drawing.Size(80, 20);
            this.nudTick.TabIndex = 40;
            this.nudTick.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudTick.ValueChanged += new System.EventHandler(this.nudTick_ValueChanged);
            // 
            // chkHOTDOT
            // 
            this.chkHOTDOT.Location = new System.Drawing.Point(5, 19);
            this.chkHOTDOT.Name = "chkHOTDOT";
            this.chkHOTDOT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkHOTDOT.Size = new System.Drawing.Size(86, 24);
            this.chkHOTDOT.TabIndex = 22;
            this.chkHOTDOT.Text = "HOT/DOT?";
            this.chkHOTDOT.CheckedChanged += new System.EventHandler(this.chkHOTDOT_CheckedChanged);
            // 
            // lblTick
            // 
            this.lblTick.AutoSize = true;
            this.lblTick.Location = new System.Drawing.Point(100, 24);
            this.lblTick.Name = "lblTick";
            this.lblTick.Size = new System.Drawing.Size(53, 13);
            this.lblTick.TabIndex = 38;
            this.lblTick.Text = "Tick (ms):";
            // 
            // grpEffect
            // 
            this.grpEffect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEffect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEffect.Controls.Add(this.lblEffect);
            this.grpEffect.Controls.Add(this.cmbExtraEffect);
            this.grpEffect.Controls.Add(this.picSprite);
            this.grpEffect.Controls.Add(this.cmbTransform);
            this.grpEffect.Controls.Add(this.lblSprite);
            this.grpEffect.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEffect.Location = new System.Drawing.Point(206, 400);
            this.grpEffect.Name = "grpEffect";
            this.grpEffect.Size = new System.Drawing.Size(235, 187);
            this.grpEffect.TabIndex = 52;
            this.grpEffect.TabStop = false;
            this.grpEffect.Text = "Effect";
            // 
            // lblEffect
            // 
            this.lblEffect.AutoSize = true;
            this.lblEffect.Location = new System.Drawing.Point(4, 15);
            this.lblEffect.Name = "lblEffect";
            this.lblEffect.Size = new System.Drawing.Size(65, 13);
            this.lblEffect.TabIndex = 35;
            this.lblEffect.Text = "Extra Effect:";
            // 
            // cmbExtraEffect
            // 
            this.cmbExtraEffect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbExtraEffect.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbExtraEffect.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbExtraEffect.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbExtraEffect.DrawDropdownHoverOutline = false;
            this.cmbExtraEffect.DrawFocusRectangle = false;
            this.cmbExtraEffect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbExtraEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtraEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbExtraEffect.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbExtraEffect.FormattingEnabled = true;
            this.cmbExtraEffect.Items.AddRange(new object[] {
            "None",
            "Silence",
            "Stun",
            "Snare",
            "Blind",
            "Stealth",
            "Transform",
            "Cleanse",
            "Invulnerable",
            "Shield",
            "Sleep",
            "On Hit",
            "Taunt"});
            this.cmbExtraEffect.Location = new System.Drawing.Point(5, 31);
            this.cmbExtraEffect.Name = "cmbExtraEffect";
            this.cmbExtraEffect.Size = new System.Drawing.Size(80, 21);
            this.cmbExtraEffect.TabIndex = 36;
            this.cmbExtraEffect.Text = "None";
            this.cmbExtraEffect.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbExtraEffect.SelectedIndexChanged += new System.EventHandler(this.cmbExtraEffect_SelectedIndexChanged);
            // 
            // picSprite
            // 
            this.picSprite.BackColor = System.Drawing.Color.Black;
            this.picSprite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picSprite.Location = new System.Drawing.Point(5, 61);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(222, 120);
            this.picSprite.TabIndex = 43;
            this.picSprite.TabStop = false;
            // 
            // cmbTransform
            // 
            this.cmbTransform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTransform.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTransform.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTransform.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTransform.DrawDropdownHoverOutline = false;
            this.cmbTransform.DrawFocusRectangle = false;
            this.cmbTransform.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTransform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTransform.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTransform.FormattingEnabled = true;
            this.cmbTransform.Items.AddRange(new object[] {
            "None"});
            this.cmbTransform.Location = new System.Drawing.Point(137, 31);
            this.cmbTransform.Name = "cmbTransform";
            this.cmbTransform.Size = new System.Drawing.Size(80, 21);
            this.cmbTransform.TabIndex = 44;
            this.cmbTransform.Text = "None";
            this.cmbTransform.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTransform.SelectedIndexChanged += new System.EventHandler(this.cmbTransform_SelectedIndexChanged);
            // 
            // lblSprite
            // 
            this.lblSprite.AutoSize = true;
            this.lblSprite.Location = new System.Drawing.Point(134, 15);
            this.lblSprite.Name = "lblSprite";
            this.lblSprite.Size = new System.Drawing.Size(37, 13);
            this.lblSprite.TabIndex = 40;
            this.lblSprite.Text = "Sprite:";
            // 
            // grpEffectDuration
            // 
            this.grpEffectDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEffectDuration.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEffectDuration.Controls.Add(this.nudBuffDuration);
            this.grpEffectDuration.Controls.Add(this.lblBuffDuration);
            this.grpEffectDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEffectDuration.Location = new System.Drawing.Point(201, 353);
            this.grpEffectDuration.Name = "grpEffectDuration";
            this.grpEffectDuration.Size = new System.Drawing.Size(233, 41);
            this.grpEffectDuration.TabIndex = 51;
            this.grpEffectDuration.TabStop = false;
            this.grpEffectDuration.Text = "Stat Boost/Effect Duration";
            // 
            // nudBuffDuration
            // 
            this.nudBuffDuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBuffDuration.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBuffDuration.Location = new System.Drawing.Point(137, 14);
            this.nudBuffDuration.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBuffDuration.Name = "nudBuffDuration";
            this.nudBuffDuration.Size = new System.Drawing.Size(80, 20);
            this.nudBuffDuration.TabIndex = 39;
            this.nudBuffDuration.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBuffDuration.ValueChanged += new System.EventHandler(this.nudBuffDuration_ValueChanged);
            // 
            // lblBuffDuration
            // 
            this.lblBuffDuration.AutoSize = true;
            this.lblBuffDuration.Location = new System.Drawing.Point(6, 16);
            this.lblBuffDuration.Name = "lblBuffDuration";
            this.lblBuffDuration.Size = new System.Drawing.Size(72, 13);
            this.lblBuffDuration.TabIndex = 33;
            this.lblBuffDuration.Text = "Duration (ms):";
            // 
            // grpDamage
            // 
            this.grpDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDamage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDamage.Controls.Add(this.chkLifesteal);
            this.grpDamage.Controls.Add(this.chkManaSteal);
            this.grpDamage.Controls.Add(this.nudCritMultiplier);
            this.grpDamage.Controls.Add(this.lblCritMultiplier);
            this.grpDamage.Controls.Add(this.nudCritChance);
            this.grpDamage.Controls.Add(this.nudScaling);
            this.grpDamage.Controls.Add(this.nudMPDamage);
            this.grpDamage.Controls.Add(this.nudHPDamage);
            this.grpDamage.Controls.Add(this.chkFriendly);
            this.grpDamage.Controls.Add(this.lblCritChance);
            this.grpDamage.Controls.Add(this.lblScaling);
            this.grpDamage.Controls.Add(this.cmbDamageType);
            this.grpDamage.Controls.Add(this.lblDamageType);
            this.grpDamage.Controls.Add(this.lblHPDamage);
            this.grpDamage.Controls.Add(this.lblManaDamage);
            this.grpDamage.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDamage.Location = new System.Drawing.Point(6, 19);
            this.grpDamage.Name = "grpDamage";
            this.grpDamage.Size = new System.Drawing.Size(188, 340);
            this.grpDamage.TabIndex = 49;
            this.grpDamage.TabStop = false;
            this.grpDamage.Text = "Damage";
            // 
            // chkLifesteal
            // 
            this.chkLifesteal.AutoSize = true;
            this.chkLifesteal.Location = new System.Drawing.Point(10, 286);
            this.chkLifesteal.Name = "chkLifesteal";
            this.chkLifesteal.Size = new System.Drawing.Size(65, 17);
            this.chkLifesteal.TabIndex = 66;
            this.chkLifesteal.Text = "Lifesteal";
            this.chkLifesteal.CheckedChanged += new System.EventHandler(this.chkLifesteal_CheckedChanged);
            // 
            // chkManaSteal
            // 
            this.chkManaSteal.AutoSize = true;
            this.chkManaSteal.Location = new System.Drawing.Point(86, 286);
            this.chkManaSteal.Name = "chkManaSteal";
            this.chkManaSteal.Size = new System.Drawing.Size(75, 17);
            this.chkManaSteal.TabIndex = 65;
            this.chkManaSteal.Text = "Manasteal";
            this.chkManaSteal.CheckedChanged += new System.EventHandler(this.chkManaSteal_CheckedChanged);
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
            this.nudCritMultiplier.Location = new System.Drawing.Point(9, 237);
            this.nudCritMultiplier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCritMultiplier.Name = "nudCritMultiplier";
            this.nudCritMultiplier.Size = new System.Drawing.Size(170, 20);
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
            this.lblCritMultiplier.Location = new System.Drawing.Point(6, 223);
            this.lblCritMultiplier.Name = "lblCritMultiplier";
            this.lblCritMultiplier.Size = new System.Drawing.Size(135, 13);
            this.lblCritMultiplier.TabIndex = 62;
            this.lblCritMultiplier.Text = "Crit Multiplier (Default 1.5x):";
            // 
            // nudCritChance
            // 
            this.nudCritChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritChance.Location = new System.Drawing.Point(8, 198);
            this.nudCritChance.Name = "nudCritChance";
            this.nudCritChance.Size = new System.Drawing.Size(171, 20);
            this.nudCritChance.TabIndex = 61;
            this.nudCritChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritChance.ValueChanged += new System.EventHandler(this.nudCritChance_ValueChanged);
            // 
            // nudScaling
            // 
            this.nudScaling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudScaling.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudScaling.Location = new System.Drawing.Point(8, 157);
            this.nudScaling.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudScaling.Name = "nudScaling";
            this.nudScaling.Size = new System.Drawing.Size(171, 20);
            this.nudScaling.TabIndex = 60;
            this.nudScaling.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudScaling.ValueChanged += new System.EventHandler(this.nudScaling_ValueChanged);
            // 
            // nudMPDamage
            // 
            this.nudMPDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMPDamage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMPDamage.Location = new System.Drawing.Point(8, 77);
            this.nudMPDamage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMPDamage.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudMPDamage.Name = "nudMPDamage";
            this.nudMPDamage.Size = new System.Drawing.Size(171, 20);
            this.nudMPDamage.TabIndex = 59;
            this.nudMPDamage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMPDamage.ValueChanged += new System.EventHandler(this.nudMPDamage_ValueChanged);
            // 
            // nudHPDamage
            // 
            this.nudHPDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHPDamage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHPDamage.Location = new System.Drawing.Point(8, 39);
            this.nudHPDamage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudHPDamage.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudHPDamage.Name = "nudHPDamage";
            this.nudHPDamage.Size = new System.Drawing.Size(171, 20);
            this.nudHPDamage.TabIndex = 58;
            this.nudHPDamage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHPDamage.ValueChanged += new System.EventHandler(this.nudHPDamage_ValueChanged);
            // 
            // chkFriendly
            // 
            this.chkFriendly.AutoSize = true;
            this.chkFriendly.Location = new System.Drawing.Point(10, 263);
            this.chkFriendly.Name = "chkFriendly";
            this.chkFriendly.Size = new System.Drawing.Size(62, 17);
            this.chkFriendly.TabIndex = 55;
            this.chkFriendly.Text = "Friendly";
            this.chkFriendly.CheckedChanged += new System.EventHandler(this.chkFriendly_CheckedChanged);
            // 
            // lblCritChance
            // 
            this.lblCritChance.AutoSize = true;
            this.lblCritChance.Location = new System.Drawing.Point(7, 182);
            this.lblCritChance.Name = "lblCritChance";
            this.lblCritChance.Size = new System.Drawing.Size(82, 13);
            this.lblCritChance.TabIndex = 54;
            this.lblCritChance.Text = "Crit Chance (%):";
            // 
            // lblScaling
            // 
            this.lblScaling.AutoSize = true;
            this.lblScaling.Location = new System.Drawing.Point(6, 141);
            this.lblScaling.Name = "lblScaling";
            this.lblScaling.Size = new System.Drawing.Size(88, 13);
            this.lblScaling.TabIndex = 52;
            this.lblScaling.Text = "Damage Scaling:";
            // 
            // cmbDamageType
            // 
            this.cmbDamageType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDamageType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDamageType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDamageType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDamageType.DrawDropdownHoverOutline = false;
            this.cmbDamageType.DrawFocusRectangle = false;
            this.cmbDamageType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDamageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDamageType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDamageType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDamageType.FormattingEnabled = true;
            this.cmbDamageType.Items.AddRange(new object[] {
            "Physical",
            "Magic",
            "True"});
            this.cmbDamageType.Location = new System.Drawing.Point(9, 117);
            this.cmbDamageType.Name = "cmbDamageType";
            this.cmbDamageType.Size = new System.Drawing.Size(170, 21);
            this.cmbDamageType.TabIndex = 50;
            this.cmbDamageType.Text = "Physical";
            this.cmbDamageType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDamageType.SelectedIndexChanged += new System.EventHandler(this.cmbDamageType_SelectedIndexChanged);
            // 
            // lblDamageType
            // 
            this.lblDamageType.AutoSize = true;
            this.lblDamageType.Location = new System.Drawing.Point(5, 100);
            this.lblDamageType.Name = "lblDamageType";
            this.lblDamageType.Size = new System.Drawing.Size(77, 13);
            this.lblDamageType.TabIndex = 49;
            this.lblDamageType.Text = "Damage Type:";
            // 
            // lblHPDamage
            // 
            this.lblHPDamage.AutoSize = true;
            this.lblHPDamage.Location = new System.Drawing.Point(6, 23);
            this.lblHPDamage.Name = "lblHPDamage";
            this.lblHPDamage.Size = new System.Drawing.Size(130, 13);
            this.lblHPDamage.TabIndex = 46;
            this.lblHPDamage.Text = "True Damage (if enabled):";
            // 
            // lblManaDamage
            // 
            this.lblManaDamage.AutoSize = true;
            this.lblManaDamage.Location = new System.Drawing.Point(6, 62);
            this.lblManaDamage.Name = "lblManaDamage";
            this.lblManaDamage.Size = new System.Drawing.Size(80, 13);
            this.lblManaDamage.TabIndex = 47;
            this.lblManaDamage.Text = "Mana Damage:";
            // 
            // grpEvent
            // 
            this.grpEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEvent.Controls.Add(this.cmbEvent);
            this.grpEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEvent.Location = new System.Drawing.Point(3, 563);
            this.grpEvent.Name = "grpEvent";
            this.grpEvent.Size = new System.Drawing.Size(441, 48);
            this.grpEvent.TabIndex = 40;
            this.grpEvent.TabStop = false;
            this.grpEvent.Text = "Event";
            this.grpEvent.Visible = false;
            // 
            // cmbEvent
            // 
            this.cmbEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEvent.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEvent.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEvent.DrawDropdownHoverOutline = false;
            this.cmbEvent.DrawFocusRectangle = false;
            this.cmbEvent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Location = new System.Drawing.Point(6, 18);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(425, 21);
            this.cmbEvent.TabIndex = 17;
            this.cmbEvent.Text = null;
            this.cmbEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
            // 
            // grpWarp
            // 
            this.grpWarp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWarp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWarp.Controls.Add(this.nudWarpY);
            this.grpWarp.Controls.Add(this.nudWarpX);
            this.grpWarp.Controls.Add(this.btnVisualMapSelector);
            this.grpWarp.Controls.Add(this.cmbWarpMap);
            this.grpWarp.Controls.Add(this.cmbDirection);
            this.grpWarp.Controls.Add(this.lblWarpDir);
            this.grpWarp.Controls.Add(this.lblY);
            this.grpWarp.Controls.Add(this.lblX);
            this.grpWarp.Controls.Add(this.lblMap);
            this.grpWarp.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWarp.Location = new System.Drawing.Point(3, 561);
            this.grpWarp.Name = "grpWarp";
            this.grpWarp.Size = new System.Drawing.Size(247, 182);
            this.grpWarp.TabIndex = 35;
            this.grpWarp.TabStop = false;
            this.grpWarp.Text = "Warp Caster:";
            this.grpWarp.Visible = false;
            // 
            // nudWarpY
            // 
            this.nudWarpY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudWarpY.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudWarpY.Location = new System.Drawing.Point(42, 91);
            this.nudWarpY.Name = "nudWarpY";
            this.nudWarpY.Size = new System.Drawing.Size(190, 20);
            this.nudWarpY.TabIndex = 35;
            this.nudWarpY.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudWarpY.ValueChanged += new System.EventHandler(this.nudWarpY_ValueChanged);
            // 
            // nudWarpX
            // 
            this.nudWarpX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudWarpX.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudWarpX.Location = new System.Drawing.Point(42, 63);
            this.nudWarpX.Name = "nudWarpX";
            this.nudWarpX.Size = new System.Drawing.Size(190, 20);
            this.nudWarpX.TabIndex = 34;
            this.nudWarpX.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudWarpX.ValueChanged += new System.EventHandler(this.nudWarpX_ValueChanged);
            // 
            // btnVisualMapSelector
            // 
            this.btnVisualMapSelector.Location = new System.Drawing.Point(9, 151);
            this.btnVisualMapSelector.Name = "btnVisualMapSelector";
            this.btnVisualMapSelector.Padding = new System.Windows.Forms.Padding(5);
            this.btnVisualMapSelector.Size = new System.Drawing.Size(222, 23);
            this.btnVisualMapSelector.TabIndex = 33;
            this.btnVisualMapSelector.Text = "Open Visual Interface";
            this.btnVisualMapSelector.Click += new System.EventHandler(this.btnVisualMapSelector_Click);
            // 
            // cmbWarpMap
            // 
            this.cmbWarpMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbWarpMap.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbWarpMap.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbWarpMap.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbWarpMap.DrawDropdownHoverOutline = false;
            this.cmbWarpMap.DrawFocusRectangle = false;
            this.cmbWarpMap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWarpMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarpMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWarpMap.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbWarpMap.FormattingEnabled = true;
            this.cmbWarpMap.Location = new System.Drawing.Point(10, 34);
            this.cmbWarpMap.Name = "cmbWarpMap";
            this.cmbWarpMap.Size = new System.Drawing.Size(221, 21);
            this.cmbWarpMap.TabIndex = 30;
            this.cmbWarpMap.Text = null;
            this.cmbWarpMap.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbWarpMap.SelectedIndexChanged += new System.EventHandler(this.cmbWarpMap_SelectedIndexChanged);
            // 
            // cmbDirection
            // 
            this.cmbDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDirection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDirection.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDirection.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDirection.DrawDropdownHoverOutline = false;
            this.cmbDirection.DrawFocusRectangle = false;
            this.cmbDirection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDirection.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDirection.FormattingEnabled = true;
            this.cmbDirection.Items.AddRange(new object[] {
            "Retain Direction",
            "Up",
            "Down",
            "Left",
            "Right"});
            this.cmbDirection.Location = new System.Drawing.Point(42, 122);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(189, 21);
            this.cmbDirection.TabIndex = 32;
            this.cmbDirection.Text = "Retain Direction";
            this.cmbDirection.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbDirection.SelectedIndexChanged += new System.EventHandler(this.cmbDirection_SelectedIndexChanged);
            // 
            // lblWarpDir
            // 
            this.lblWarpDir.AutoSize = true;
            this.lblWarpDir.Location = new System.Drawing.Point(6, 125);
            this.lblWarpDir.Name = "lblWarpDir";
            this.lblWarpDir.Size = new System.Drawing.Size(23, 13);
            this.lblWarpDir.TabIndex = 31;
            this.lblWarpDir.Text = "Dir:";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(7, 93);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(17, 13);
            this.lblY.TabIndex = 29;
            this.lblY.Text = "Y:";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(7, 65);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(17, 13);
            this.lblX.TabIndex = 28;
            this.lblX.Text = "X:";
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(6, 18);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(31, 13);
            this.lblMap.TabIndex = 27;
            this.lblMap.Text = "Map:";
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
            this.toolStrip.Size = new System.Drawing.Size(932, 25);
            this.toolStrip.TabIndex = 51;
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(730, 696);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(190, 27);
            this.btnCancel.TabIndex = 49;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(530, 696);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(190, 27);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpSpells
            // 
            this.grpSpells.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpells.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpells.Controls.Add(this.btnClearSearch);
            this.grpSpells.Controls.Add(this.txtSearch);
            this.grpSpells.Controls.Add(this.lstGameObjects);
            this.grpSpells.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpells.Location = new System.Drawing.Point(12, 40);
            this.grpSpells.Name = "grpSpells";
            this.grpSpells.Size = new System.Drawing.Size(203, 650);
            this.grpSpells.TabIndex = 16;
            this.grpSpells.TabStop = false;
            this.grpSpells.Text = "Spells";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 17);
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
            this.txtSearch.Location = new System.Drawing.Point(6, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(167, 20);
            this.txtSearch.TabIndex = 33;
            this.txtSearch.Text = "Search...";
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
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
            this.lstGameObjects.Location = new System.Drawing.Point(6, 43);
            this.lstGameObjects.Name = "lstGameObjects";
            this.lstGameObjects.SelectedImageIndex = 0;
            this.lstGameObjects.Size = new System.Drawing.Size(191, 601);
            this.lstGameObjects.TabIndex = 32;
            // 
            // FrmSpell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(932, 743);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.grpSpells);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSpell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spell Editor                       ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSpell_FormClosed);
            this.Load += new System.EventHandler(this.frmSpell_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
            this.pnlContainer.ResumeLayout(false);
            this.grpNpcSpellChain.ResumeLayout(false);
            this.grpNpcSpellChain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeChainDelay)).EndInit();
            this.grpSpellGroup.ResumeLayout(false);
            this.grpSpellGroup.PerformLayout();
            this.grpComponents.ResumeLayout(false);
            this.grpComponents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudComponentQuantity)).EndInit();
            this.grpTargetInfo.ResumeLayout(false);
            this.grpTargetInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHitRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCastRange)).EndInit();
            this.grpDash.ResumeLayout(false);
            this.grpDash.PerformLayout();
            this.grpDashCollisions.ResumeLayout(false);
            this.grpDashCollisions.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpell)).EndInit();
            this.grpSpellCost.ResumeLayout(false);
            this.grpSpellCost.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterruptThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkillPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldownDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCastDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPCost)).EndInit();
            this.grpRequirements.ResumeLayout(false);
            this.grpRequirements.PerformLayout();
            this.grpCombat.ResumeLayout(false);
            this.grpAdvTrapSettings.ResumeLayout(false);
            this.grpAdvTrapSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrapCooldown)).EndInit();
            this.grpAreaDenial.ResumeLayout(false);
            this.grpAreaDenial.PerformLayout();
            this.grpAreaDenialSettings.ResumeLayout(false);
            this.grpAreaDenialSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaSpawnIntensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAreaDenialRadius)).EndInit();
            this.grpAdvAoe.ResumeLayout(false);
            this.grpAdvAoe.PerformLayout();
            this.grpRectangleSettings.ResumeLayout(false);
            this.grpRectangleSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAoeXOffset)).EndInit();
            this.grpBalanceHelp.ResumeLayout(false);
            this.grpBalanceHelp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockMagic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockPierce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockSlash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMockBlunt)).EndInit();
            this.grpBonusEffects.ResumeLayout(false);
            this.grpBonusEffects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBonusAmt)).EndInit();
            this.grpDamageTypes.ResumeLayout(false);
            this.grpDamageTypes.PerformLayout();
            this.grpSetDamages.ResumeLayout(false);
            this.grpSetDamages.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagicDam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashDam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceDam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBluntDam)).EndInit();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasionPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracyPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResistPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPiercePercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResistPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpdPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMRPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            this.grpHotDot.ResumeLayout(false);
            this.grpHotDot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTick)).EndInit();
            this.grpEffect.ResumeLayout(false);
            this.grpEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            this.grpEffectDuration.ResumeLayout(false);
            this.grpEffectDuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuffDuration)).EndInit();
            this.grpDamage.ResumeLayout(false);
            this.grpDamage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMPDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPDamage)).EndInit();
            this.grpEvent.ResumeLayout(false);
            this.grpWarp.ResumeLayout(false);
            this.grpWarp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarpY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWarpX)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.grpSpells.ResumeLayout(false);
            this.grpSpells.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DarkGroupBox grpSpells;
        private DarkGroupBox grpGeneral;
        private System.Windows.Forms.Label lblCooldownDuration;
        private DarkComboBox cmbSprite;
        private System.Windows.Forms.Label lblCastDuration;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.PictureBox picSpell;
        private System.Windows.Forms.Label lblName;
        private DarkTextBox txtName;
        private DarkGroupBox grpRequirements;
        private System.Windows.Forms.Label lblHitAnimation;
        private System.Windows.Forms.Label lblCastAnimation;
        private System.Windows.Forms.Label lblType;
        private DarkComboBox cmbType;
        private DarkGroupBox grpTargetInfo;
        private System.Windows.Forms.Label lblHitRadius;
        private DarkComboBox cmbTargetType;
        private System.Windows.Forms.Label lblCastRange;
        private System.Windows.Forms.Label lblTargetType;
        private DarkGroupBox grpWarp;
        private DarkGroupBox grpSpellCost;
        private System.Windows.Forms.Label lblMPCost;
        private System.Windows.Forms.Label lblHPCost;
        private System.Windows.Forms.Label lblDesc;
        private DarkTextBox txtDesc;
        private DarkGroupBox grpDash;
        private System.Windows.Forms.Label lblRange;
        private DarkScrollBar scrlRange;
        private System.Windows.Forms.Label lblProjectile;
        private DarkGroupBox grpDashCollisions;
        private DarkCheckBox chkIgnoreInactiveResources;
        private DarkCheckBox chkIgnoreZDimensionBlocks;
        private DarkCheckBox chkIgnoreMapBlocks;
        private DarkCheckBox chkIgnoreActiveResources;
        private DarkGroupBox grpEvent;
        private System.Windows.Forms.Panel pnlContainer;
        private DarkButton btnSave;
        private DarkButton btnCancel;
        private DarkToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton toolStripItemCopy;
        public System.Windows.Forms.ToolStripButton toolStripItemPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripItemUndo;
        private DarkButton btnDynamicRequirements;
        private DarkComboBox cmbHitAnimation;
        private DarkComboBox cmbCastAnimation;
        private DarkComboBox cmbProjectile;
        private DarkComboBox cmbEvent;
        private DarkNumericUpDown nudWarpY;
        private DarkNumericUpDown nudWarpX;
        private DarkButton btnVisualMapSelector;
        private DarkComboBox cmbWarpMap;
        private DarkComboBox cmbDirection;
        private System.Windows.Forms.Label lblWarpDir;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblMap;
        private DarkNumericUpDown nudCooldownDuration;
        private DarkNumericUpDown nudCastDuration;
        private DarkNumericUpDown nudMpCost;
        private DarkNumericUpDown nudHPCost;
        private DarkNumericUpDown nudHitRadius;
        private DarkNumericUpDown nudCastRange;
        private DarkNumericUpDown nudDuration;
        private System.Windows.Forms.Label lblDuration;
        private DarkCheckBox chkBound;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private DarkButton btnAddFolder;
        private System.Windows.Forms.Label lblFolder;
        private DarkComboBox cmbFolder;
        private System.Windows.Forms.ToolStripButton btnAlphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private DarkButton btnAddCooldownGroup;
        private DarkComboBox cmbCooldownGroup;
        private System.Windows.Forms.Label lblCooldownGroup;
        private DarkCheckBox chkIgnoreGlobalCooldown;
        private DarkCheckBox chkIgnoreCdr;
        private Controls.GameObjectList lstGameObjects;
        private System.Windows.Forms.Label lblCannotCast;
        private DarkTextBox txtCannotCast;
        private System.Windows.Forms.Label lblTrapAnimation;
        private DarkComboBox cmbTrapAnimation;
        private DarkCheckBox chkEntities;
        private DarkComboBox cmbDashSpell;
        private System.Windows.Forms.Label lblDashSpell;
        private DarkGroupBox grpComponents;
        private System.Windows.Forms.ListBox lstComponents;
        private System.Windows.Forms.Label lblComponent;
        private DarkComboBox cmbComponents;
        private DarkButton btnAddComponent;
        private DarkButton btnRemoveComponent;
        private System.Windows.Forms.Label lblComponentQuantity;
        private DarkNumericUpDown nudComponentQuantity;
        private DarkGroupBox grpCombat;
        private DarkGroupBox grpStats;
        private DarkCheckBox chkDamageMagic;
        private DarkCheckBox chkDamagePierce;
        private DarkCheckBox chkDamageSlash;
        private DarkCheckBox chkDamageBlunt;
        private System.Windows.Forms.Label label22;
        private DarkNumericUpDown nudEvasionPercentage;
        private System.Windows.Forms.Label label23;
        private DarkNumericUpDown nudEvasion;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label19;
        private DarkNumericUpDown nudAccuracyPercentage;
        private System.Windows.Forms.Label label20;
        private DarkNumericUpDown nudAccuracy;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private DarkNumericUpDown nudPierceResistPercentage;
        private DarkNumericUpDown nudPiercePercentage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private DarkNumericUpDown nudPierceResist;
        private System.Windows.Forms.Label label12;
        private DarkNumericUpDown nudPierce;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private DarkNumericUpDown nudSlashResistPercentage;
        private System.Windows.Forms.Label label5;
        private DarkNumericUpDown nudSlashResist;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private DarkNumericUpDown nudSlashPercentage;
        private System.Windows.Forms.Label label2;
        private DarkNumericUpDown nudSlash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPercentage5;
        private System.Windows.Forms.Label lblPercentage4;
        private System.Windows.Forms.Label lblPercentage3;
        private System.Windows.Forms.Label lblPercentage2;
        private System.Windows.Forms.Label lblPercentage1;
        private DarkNumericUpDown nudSpdPercentage;
        private DarkNumericUpDown nudMRPercentage;
        private DarkNumericUpDown nudDefPercentage;
        private DarkNumericUpDown nudMagPercentage;
        private DarkNumericUpDown nudStrPercentage;
        private System.Windows.Forms.Label lblPlus5;
        private System.Windows.Forms.Label lblPlus4;
        private System.Windows.Forms.Label lblPlus3;
        private System.Windows.Forms.Label lblPlus2;
        private System.Windows.Forms.Label lblPlus1;
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
        private DarkGroupBox grpHotDot;
        private System.Windows.Forms.Label lblOTanimationDisclaimer;
        private System.Windows.Forms.Label lblHOTDOTenableAnimation;
        private DarkComboBox cmbOverTimeAnimation;
        private DarkNumericUpDown nudTick;
        private DarkCheckBox chkHOTDOT;
        private System.Windows.Forms.Label lblTick;
        private DarkGroupBox grpEffect;
        private System.Windows.Forms.Label lblEffect;
        private DarkComboBox cmbExtraEffect;
        private System.Windows.Forms.PictureBox picSprite;
        private DarkComboBox cmbTransform;
        private System.Windows.Forms.Label lblSprite;
        private DarkGroupBox grpEffectDuration;
        private DarkNumericUpDown nudBuffDuration;
        private System.Windows.Forms.Label lblBuffDuration;
        private DarkGroupBox grpDamage;
        private DarkCheckBox chkInheritStats;
        private DarkNumericUpDown nudCritMultiplier;
        private System.Windows.Forms.Label lblCritMultiplier;
        private DarkNumericUpDown nudCritChance;
        private DarkNumericUpDown nudScaling;
        private DarkNumericUpDown nudMPDamage;
        private DarkNumericUpDown nudHPDamage;
        private DarkCheckBox chkFriendly;
        private System.Windows.Forms.Label lblCritChance;
        private System.Windows.Forms.Label lblScaling;
        private DarkComboBox cmbDamageType;
        private System.Windows.Forms.Label lblDamageType;
        private System.Windows.Forms.Label lblHPDamage;
        private System.Windows.Forms.Label lblManaDamage;
        private DarkGroupBox grpDamageTypes;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private DarkGroupBox grpSpellGroup;
        private DarkButton btnAddSpellGroup;
        private DarkComboBox cmbSpellGroup;
        private DarkButton btnSetEmpty;
        private DarkNumericUpDown nudSkillPoints;
        private System.Windows.Forms.Label lblSkillPts;
        private DarkGroupBox grpBonusEffects;
        private DarkNumericUpDown nudBonusAmt;
        private System.Windows.Forms.Label lblBonusAmt;
        private System.Windows.Forms.ListBox lstBonusEffects;
        private DarkGroupBox grpSetDamages;
        private DarkNumericUpDown nudMagicDam;
        private System.Windows.Forms.Label lblMagicDamage;
        private DarkNumericUpDown nudSlashDam;
        private System.Windows.Forms.Label label13;
        private DarkNumericUpDown nudPierceDam;
        private System.Windows.Forms.Label lblPierceDamage;
        private System.Windows.Forms.Label lblBluntDamage;
        private DarkNumericUpDown nudBluntDam;
        private DarkGroupBox grpBalanceHelp;
        private System.Windows.Forms.Label lblTier;
        private DarkComboBox darkComboBox1;
        private System.Windows.Forms.Label lblTargetDps;
        private System.Windows.Forms.Label lblProejctedDps;
        private System.Windows.Forms.Label lblProjectedDpsVal;
        private System.Windows.Forms.Label lblTargetDpsVal;
        private DarkNumericUpDown nudMockMagic;
        private DarkNumericUpDown nudMockPierce;
        private System.Windows.Forms.Label mockMagic;
        private System.Windows.Forms.Label mockPierce;
        private DarkNumericUpDown nudMockSlash;
        private System.Windows.Forms.Label lblMockSlash;
        private DarkNumericUpDown nudMockBlunt;
        private System.Windows.Forms.Label lblMockBlunt;
        private System.Windows.Forms.Label lblMaxHitVal;
        private System.Windows.Forms.Label lblMaxHit;
        private DarkCheckBox chkPersistMissedAttack;
        private DarkCheckBox chkPersistSwap;
        private DarkCheckBox chkManaSteal;
        private DarkCheckBox chkLifesteal;
        private DarkComboBox cmbDashAnimation;
        private System.Windows.Forms.Label lblDashAnimation;
        private DarkNumericUpDown nudInterruptThreshold;
        private System.Windows.Forms.Label lblInterruptThreshold;
        private System.Windows.Forms.Label lblSpellUpgrade;
        private DarkComboBox cmbSpellUpgrade;
        private DarkGroupBox grpAdvAoe;
        private System.Windows.Forms.Label lblAoeShape;
        private DarkComboBox cmbAoeShape;
        private System.Windows.Forms.Label lblAoeYOffset;
        private DarkNumericUpDown nudAoeYOffset;
        private System.Windows.Forms.Label lblAoeXOffset;
        private DarkNumericUpDown nudAoeXOffset;
        private DarkCheckBox chkAoeRelativeDir;
        private DarkGroupBox grpRectangleSettings;
        private System.Windows.Forms.Label lblAoeRectWidth;
        private System.Windows.Forms.Label lblAoeRectHeight;
        private DarkNumericUpDown nudAoeHeight;
        private DarkNumericUpDown nudAoeWidth;
        private System.Windows.Forms.Label lblRectLbl;
        private DarkNumericUpDown nudMinRange;
        private System.Windows.Forms.Label lblMinRange;
        private DarkComboBox cmbAoeChain;
        private System.Windows.Forms.Label lblChain;
        private DarkNumericUpDown nudAoeChainDelay;
        private System.Windows.Forms.Label lblAoeChainDelay;
        private DarkGroupBox grpNpcSpellChain;
        private DarkGroupBox grpAreaDenial;
        private DarkCheckBox chkAreaDenial;
        private System.Windows.Forms.Label lblAreaDenialSpawnIntensity;
        private DarkGroupBox grpAreaDenialSettings;
        private System.Windows.Forms.Label lblAreaDenialRadius;
        private DarkNumericUpDown nudAreaDenialRadius;
        private DarkNumericUpDown nudAreaSpawnIntensity;
        private DarkCheckBox chkNotSingleUseTrap;
        private DarkGroupBox grpAdvTrapSettings;
        private DarkNumericUpDown nudTrapCooldown;
        private System.Windows.Forms.Label lblTrapCooldown;
    }
}