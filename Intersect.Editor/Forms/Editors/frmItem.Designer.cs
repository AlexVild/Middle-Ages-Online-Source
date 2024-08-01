using System.ComponentModel;
using System.Windows.Forms;
using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors
{
    partial class FrmItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItem));
            this.grpItems = new DarkUI.Controls.DarkGroupBox();
            this.btnClearSearch = new DarkUI.Controls.DarkButton();
            this.txtSearch = new DarkUI.Controls.DarkTextBox();
            this.lstGameObjects = new Intersect.Editor.Forms.Controls.GameObjectList();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.grpGeneral = new DarkUI.Controls.DarkGroupBox();
            this.lblSortName = new System.Windows.Forms.Label();
            this.txtSortName = new DarkUI.Controls.DarkTextBox();
            this.btnFuelRecc = new DarkUI.Controls.DarkButton();
            this.nudFuel = new DarkUI.Controls.DarkNumericUpDown();
            this.lblFuel = new System.Windows.Forms.Label();
            this.chkRareDrop = new DarkUI.Controls.DarkCheckBox();
            this.btnClearOverride = new DarkUI.Controls.DarkButton();
            this.cmbTypeDisplayOverride = new DarkUI.Controls.DarkComboBox();
            this.lblOverride = new System.Windows.Forms.Label();
            this.grpDestroy = new DarkUI.Controls.DarkGroupBox();
            this.chkInstanceDestroy = new DarkUI.Controls.DarkCheckBox();
            this.chkEnableDestroy = new DarkUI.Controls.DarkCheckBox();
            this.lblDestroyMessage = new System.Windows.Forms.Label();
            this.txtCannotDestroy = new DarkUI.Controls.DarkTextBox();
            this.btnDestroyRequirements = new DarkUI.Controls.DarkButton();
            this.grpTags = new DarkUI.Controls.DarkGroupBox();
            this.lstTags = new System.Windows.Forms.ListBox();
            this.btnRemoveTag = new DarkUI.Controls.DarkButton();
            this.btnAddTag = new DarkUI.Controls.DarkButton();
            this.lblTagToAdd = new System.Windows.Forms.Label();
            this.cmbTags = new DarkUI.Controls.DarkComboBox();
            this.btnNewTag = new DarkUI.Controls.DarkButton();
            this.grpRequirements = new DarkUI.Controls.DarkGroupBox();
            this.lblCannotUse = new System.Windows.Forms.Label();
            this.txtCannotUse = new DarkUI.Controls.DarkTextBox();
            this.btnEditRequirements = new DarkUI.Controls.DarkButton();
            this.chkCanGuildBank = new DarkUI.Controls.DarkCheckBox();
            this.nudBankStackLimit = new DarkUI.Controls.DarkNumericUpDown();
            this.nudInvStackLimit = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBankStackLimit = new System.Windows.Forms.Label();
            this.lblInvStackLimit = new System.Windows.Forms.Label();
            this.nudDeathDropChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblDeathDropChance = new System.Windows.Forms.Label();
            this.chkCanSell = new DarkUI.Controls.DarkCheckBox();
            this.chkCanTrade = new DarkUI.Controls.DarkCheckBox();
            this.chkCanBag = new DarkUI.Controls.DarkCheckBox();
            this.chkCanBank = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreCdr = new DarkUI.Controls.DarkCheckBox();
            this.chkIgnoreGlobalCooldown = new DarkUI.Controls.DarkCheckBox();
            this.btnAddCooldownGroup = new DarkUI.Controls.DarkButton();
            this.cmbCooldownGroup = new DarkUI.Controls.DarkComboBox();
            this.lblCooldownGroup = new System.Windows.Forms.Label();
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
            this.cmbRarity = new DarkUI.Controls.DarkComboBox();
            this.lblRarity = new System.Windows.Forms.Label();
            this.nudCooldown = new DarkUI.Controls.DarkNumericUpDown();
            this.lblCooldown = new System.Windows.Forms.Label();
            this.chkStackable = new DarkUI.Controls.DarkCheckBox();
            this.nudPrice = new DarkUI.Controls.DarkNumericUpDown();
            this.chkCanDrop = new DarkUI.Controls.DarkCheckBox();
            this.cmbAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new DarkUI.Controls.DarkTextBox();
            this.cmbPic = new DarkUI.Controls.DarkComboBox();
            this.lblAnim = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblPic = new System.Windows.Forms.Label();
            this.picItem = new System.Windows.Forms.PictureBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new DarkUI.Controls.DarkComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new DarkUI.Controls.DarkTextBox();
            this.lblDrops = new System.Windows.Forms.Label();
            this.lstDrops = new System.Windows.Forms.ListBox();
            this.grpEquipment = new DarkUI.Controls.DarkGroupBox();
            this.grpProc = new DarkUI.Controls.DarkGroupBox();
            this.nudProcChance = new DarkUI.Controls.DarkNumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProcChance = new System.Windows.Forms.Label();
            this.cmbProcSpell = new DarkUI.Controls.DarkComboBox();
            this.grpUpgrades = new DarkUI.Controls.DarkGroupBox();
            this.lstUpgrades = new System.Windows.Forms.ListBox();
            this.btnRemoveUpgrade = new DarkUI.Controls.DarkButton();
            this.btnAddUpgrade = new DarkUI.Controls.DarkButton();
            this.lblUpgrade = new System.Windows.Forms.Label();
            this.cmbUpgrade = new DarkUI.Controls.DarkComboBox();
            this.nudUpgradeCost = new DarkUI.Controls.DarkNumericUpDown();
            this.lblUpgradeCost = new System.Windows.Forms.Label();
            this.grpWeaponEnhancement = new DarkUI.Controls.DarkGroupBox();
            this.nudEnhanceThresh = new DarkUI.Controls.DarkNumericUpDown();
            this.lblEnhancementThres = new System.Windows.Forms.Label();
            this.grpDeconstruction = new DarkUI.Controls.DarkGroupBox();
            this.btnFuelReqRecc = new DarkUI.Controls.DarkButton();
            this.nudStudyChance = new DarkUI.Controls.DarkNumericUpDown();
            this.lblStudyChance = new System.Windows.Forms.Label();
            this.cmbStudyEnhancement = new DarkUI.Controls.DarkComboBox();
            this.lblStudy = new System.Windows.Forms.Label();
            this.lblDeconLoot = new System.Windows.Forms.Label();
            this.nudReqFuel = new DarkUI.Controls.DarkNumericUpDown();
            this.lblFuelReq = new System.Windows.Forms.Label();
            this.btnRemoveDeconTable = new DarkUI.Controls.DarkButton();
            this.btnAddDeconTable = new DarkUI.Controls.DarkButton();
            this.lstDeconstructionTables = new System.Windows.Forms.ListBox();
            this.lblDeconLootTable = new System.Windows.Forms.Label();
            this.lblDeconTableRolls = new System.Windows.Forms.Label();
            this.cmbDeconTables = new DarkUI.Controls.DarkComboBox();
            this.nudDeconTableRolls = new DarkUI.Controls.DarkNumericUpDown();
            this.grpWeaponTypes = new DarkUI.Controls.DarkGroupBox();
            this.nudWeaponCraftExp = new DarkUI.Controls.DarkNumericUpDown();
            this.lblWeaponCraftExp = new System.Windows.Forms.Label();
            this.nudMaxWeaponLvl = new DarkUI.Controls.DarkNumericUpDown();
            this.lblMaxWeaponLvl = new System.Windows.Forms.Label();
            this.btnRemoveWeaponType = new DarkUI.Controls.DarkButton();
            this.btnAddWeaponType = new DarkUI.Controls.DarkButton();
            this.lstWeaponTypes = new System.Windows.Forms.ListBox();
            this.lblWeaponTypes = new System.Windows.Forms.Label();
            this.cmbWeaponTypes = new DarkUI.Controls.DarkComboBox();
            this.grpWeaponProperties = new DarkUI.Controls.DarkGroupBox();
            this.nudCritMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.grpDamageTypes = new DarkUI.Controls.DarkGroupBox();
            this.chkBluntDamage = new DarkUI.Controls.DarkCheckBox();
            this.chkDamageSlash = new DarkUI.Controls.DarkCheckBox();
            this.chkDamagePierce = new DarkUI.Controls.DarkCheckBox();
            this.chkDamageMagic = new DarkUI.Controls.DarkCheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblCritMultiplier = new System.Windows.Forms.Label();
            this.grpAttackSpeed = new DarkUI.Controls.DarkGroupBox();
            this.nudAttackSpeedValue = new DarkUI.Controls.DarkNumericUpDown();
            this.lblAttackSpeedValue = new System.Windows.Forms.Label();
            this.cmbAttackSpeedModifier = new DarkUI.Controls.DarkComboBox();
            this.lblAttackSpeedModifier = new System.Windows.Forms.Label();
            this.nudCritChance = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDamage = new DarkUI.Controls.DarkNumericUpDown();
            this.cmbProjectile = new DarkUI.Controls.DarkComboBox();
            this.lblCritChance = new System.Windows.Forms.Label();
            this.cmbAttackAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblAttackAnimation = new System.Windows.Forms.Label();
            this.chk2Hand = new DarkUI.Controls.DarkCheckBox();
            this.lblToolType = new System.Windows.Forms.Label();
            this.cmbToolType = new DarkUI.Controls.DarkComboBox();
            this.lblProjectile = new System.Windows.Forms.Label();
            this.lblDamage = new System.Windows.Forms.Label();
            this.grpCosmetic = new DarkUI.Controls.DarkGroupBox();
            this.txtCosmeticDisplayName = new DarkUI.Controls.DarkTextBox();
            this.lblCosmeticDisplayName = new System.Windows.Forms.Label();
            this.grpSpecialAttack = new DarkUI.Controls.DarkGroupBox();
            this.lblSpecialAttack = new System.Windows.Forms.Label();
            this.lblSpecialAttackCharge = new System.Windows.Forms.Label();
            this.cmbSpecialAttack = new DarkUI.Controls.DarkComboBox();
            this.nudSpecialAttackChargeTime = new DarkUI.Controls.DarkNumericUpDown();
            this.grpBonusEffects = new DarkUI.Controls.DarkGroupBox();
            this.btnRemoveBonus = new DarkUI.Controls.DarkButton();
            this.btnAddBonus = new DarkUI.Controls.DarkButton();
            this.lstBonusEffects = new System.Windows.Forms.ListBox();
            this.lblBonusEffect = new System.Windows.Forms.Label();
            this.lblEffectPercent = new System.Windows.Forms.Label();
            this.cmbEquipmentBonus = new DarkUI.Controls.DarkComboBox();
            this.nudEffectPercent = new DarkUI.Controls.DarkNumericUpDown();
            this.grpAdditionalWeaponProps = new DarkUI.Controls.DarkGroupBox();
            this.lblAmmoOverride = new System.Windows.Forms.Label();
            this.cmbAmmoOverride = new DarkUI.Controls.DarkComboBox();
            this.chkIsFocus = new DarkUI.Controls.DarkCheckBox();
            this.lblBackBoost = new System.Windows.Forms.Label();
            this.nudBackBoost = new DarkUI.Controls.DarkNumericUpDown();
            this.nudStrafeBoost = new DarkUI.Controls.DarkNumericUpDown();
            this.lblStrafeModifier = new System.Windows.Forms.Label();
            this.lblBackstabMultiplier = new System.Windows.Forms.Label();
            this.chkBackstab = new DarkUI.Controls.DarkCheckBox();
            this.nudBackstabMultiplier = new DarkUI.Controls.DarkNumericUpDown();
            this.grpHelmetPaperdollProps = new DarkUI.Controls.DarkGroupBox();
            this.chkShortHair = new DarkUI.Controls.DarkCheckBox();
            this.chkHelmHideExtra = new DarkUI.Controls.DarkCheckBox();
            this.chkHelmHideBeard = new DarkUI.Controls.DarkCheckBox();
            this.chkHelmHideHair = new DarkUI.Controls.DarkCheckBox();
            this.grpPrayerProperties = new DarkUI.Controls.DarkGroupBox();
            this.lblComboExpBoost = new System.Windows.Forms.Label();
            this.nudComboExpBoost = new DarkUI.Controls.DarkNumericUpDown();
            this.lblComboInterval = new System.Windows.Forms.Label();
            this.nudComboInterval = new DarkUI.Controls.DarkNumericUpDown();
            this.lblComboSpell = new System.Windows.Forms.Label();
            this.cmbComboSpell = new DarkUI.Controls.DarkComboBox();
            this.grpRegen = new DarkUI.Controls.DarkGroupBox();
            this.nudMpRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHPRegen = new DarkUI.Controls.DarkNumericUpDown();
            this.lblHpRegen = new System.Windows.Forms.Label();
            this.lblManaRegen = new System.Windows.Forms.Label();
            this.lblRegenHint = new System.Windows.Forms.Label();
            this.grpVitalBonuses = new DarkUI.Controls.DarkGroupBox();
            this.lblPercentage2 = new System.Windows.Forms.Label();
            this.lblPercentage1 = new System.Windows.Forms.Label();
            this.nudMPPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHPPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPlus2 = new System.Windows.Forms.Label();
            this.lblPlus1 = new System.Windows.Forms.Label();
            this.nudManaBonus = new DarkUI.Controls.DarkNumericUpDown();
            this.nudHealthBonus = new DarkUI.Controls.DarkNumericUpDown();
            this.lblManaBonus = new System.Windows.Forms.Label();
            this.lblHealthBonus = new System.Windows.Forms.Label();
            this.cmbEquipmentAnimation = new DarkUI.Controls.DarkComboBox();
            this.lblEquipmentAnimation = new System.Windows.Forms.Label();
            this.grpStatBonuses = new DarkUI.Controls.DarkGroupBox();
            this.lblSkillPoints = new System.Windows.Forms.Label();
            this.nudSkillPoints = new DarkUI.Controls.DarkNumericUpDown();
            this.grpWeaponBalance = new DarkUI.Controls.DarkGroupBox();
            this.lblMaxHitVal = new System.Windows.Forms.Label();
            this.lblMaxHit = new System.Windows.Forms.Label();
            this.lblDpsVal = new System.Windows.Forms.Label();
            this.lblTierDpsVal = new System.Windows.Forms.Label();
            this.lblProjectedDps = new System.Windows.Forms.Label();
            this.lblTierDps = new System.Windows.Forms.Label();
            this.grpArmorBalanceHelper = new DarkUI.Controls.DarkGroupBox();
            this.lblHighResVal = new System.Windows.Forms.Label();
            this.lblHighRes = new System.Windows.Forms.Label();
            this.lblMediumResVal = new System.Windows.Forms.Label();
            this.lblLowResVal = new System.Windows.Forms.Label();
            this.lblMedRes = new System.Windows.Forms.Label();
            this.lblLowRes = new System.Windows.Forms.Label();
            this.chkLockEvasion = new DarkUI.Controls.DarkCheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.nudEvasionPercent = new DarkUI.Controls.DarkNumericUpDown();
            this.nudEvasion = new DarkUI.Controls.DarkNumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.chkLockAccuracy = new DarkUI.Controls.DarkCheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nudAccuracyPercent = new DarkUI.Controls.DarkNumericUpDown();
            this.nudAccuracy = new DarkUI.Controls.DarkNumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.chkLockPierceResist = new DarkUI.Controls.DarkCheckBox();
            this.chkLockSlashResist = new DarkUI.Controls.DarkCheckBox();
            this.chkLockPierce = new DarkUI.Controls.DarkCheckBox();
            this.chkLockSlash = new DarkUI.Controls.DarkCheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPierceResistPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSlashResistPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudPiercePercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSlashPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudPierceResist = new DarkUI.Controls.DarkNumericUpDown();
            this.nudSlashResist = new DarkUI.Controls.DarkNumericUpDown();
            this.nudPierce = new DarkUI.Controls.DarkNumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudSlash = new DarkUI.Controls.DarkNumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkLockSpeed = new DarkUI.Controls.DarkCheckBox();
            this.chkLockMagicResist = new DarkUI.Controls.DarkCheckBox();
            this.chkLockArmor = new DarkUI.Controls.DarkCheckBox();
            this.chkLockMagic = new DarkUI.Controls.DarkCheckBox();
            this.chkLockStrength = new DarkUI.Controls.DarkCheckBox();
            this.lblPercentage5 = new System.Windows.Forms.Label();
            this.lblPercentage4 = new System.Windows.Forms.Label();
            this.lblPercentage8 = new System.Windows.Forms.Label();
            this.lblPercentage7 = new System.Windows.Forms.Label();
            this.lblPercentage6 = new System.Windows.Forms.Label();
            this.nudSpdPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMRPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudDefPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudMagPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudStrPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.nudRange = new DarkUI.Controls.DarkNumericUpDown();
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
            this.lblRange = new System.Windows.Forms.Label();
            this.cmbFemalePaperdoll = new DarkUI.Controls.DarkComboBox();
            this.lblFemalePaperdoll = new System.Windows.Forms.Label();
            this.picFemalePaperdoll = new System.Windows.Forms.PictureBox();
            this.cmbEquipmentSlot = new DarkUI.Controls.DarkComboBox();
            this.lblEquipmentSlot = new System.Windows.Forms.Label();
            this.cmbMalePaperdoll = new DarkUI.Controls.DarkComboBox();
            this.lblMalePaperdoll = new System.Windows.Forms.Label();
            this.picMalePaperdoll = new System.Windows.Forms.PictureBox();
            this.grpEvent = new DarkUI.Controls.DarkGroupBox();
            this.chkSingleUseEvent = new DarkUI.Controls.DarkCheckBox();
            this.cmbEvent = new DarkUI.Controls.DarkComboBox();
            this.grpSpell = new DarkUI.Controls.DarkGroupBox();
            this.chkSingleUseSpell = new DarkUI.Controls.DarkCheckBox();
            this.chkQuickCast = new DarkUI.Controls.DarkCheckBox();
            this.cmbTeachSpell = new DarkUI.Controls.DarkComboBox();
            this.lblSpell = new System.Windows.Forms.Label();
            this.grpBags = new DarkUI.Controls.DarkGroupBox();
            this.nudBag = new DarkUI.Controls.DarkNumericUpDown();
            this.lblBag = new System.Windows.Forms.Label();
            this.grpConsumable = new DarkUI.Controls.DarkGroupBox();
            this.chkOnlyClanWar = new DarkUI.Controls.DarkCheckBox();
            this.chkMeleeConsumable = new DarkUI.Controls.DarkCheckBox();
            this.lblPercentage3 = new System.Windows.Forms.Label();
            this.nudIntervalPercentage = new DarkUI.Controls.DarkNumericUpDown();
            this.lblPlus3 = new System.Windows.Forms.Label();
            this.nudInterval = new DarkUI.Controls.DarkNumericUpDown();
            this.lblVital = new System.Windows.Forms.Label();
            this.cmbConsume = new DarkUI.Controls.DarkComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.grpAuxInfo = new DarkUI.Controls.DarkGroupBox();
            this.lstShopsBuy = new System.Windows.Forms.ListBox();
            this.lblShopsBuy = new System.Windows.Forms.Label();
            this.lblShops = new System.Windows.Forms.Label();
            this.lstShops = new System.Windows.Forms.ListBox();
            this.lstCrafts = new System.Windows.Forms.ListBox();
            this.lblCraftsUsed = new System.Windows.Forms.Label();
            this.grpEnhancement = new DarkUI.Controls.DarkGroupBox();
            this.cmbEnhancement = new DarkUI.Controls.DarkComboBox();
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
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.grpItems.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFuel)).BeginInit();
            this.grpDestroy.SuspendLayout();
            this.grpTags.SuspendLayout();
            this.grpRequirements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankStackLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvStackLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeathDropChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).BeginInit();
            this.grpEquipment.SuspendLayout();
            this.grpProc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProcChance)).BeginInit();
            this.grpUpgrades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpgradeCost)).BeginInit();
            this.grpWeaponEnhancement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnhanceThresh)).BeginInit();
            this.grpDeconstruction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStudyChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReqFuel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeconTableRolls)).BeginInit();
            this.grpWeaponTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponCraftExp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWeaponLvl)).BeginInit();
            this.grpWeaponProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).BeginInit();
            this.grpDamageTypes.SuspendLayout();
            this.grpAttackSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).BeginInit();
            this.grpCosmetic.SuspendLayout();
            this.grpSpecialAttack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpecialAttackChargeTime)).BeginInit();
            this.grpBonusEffects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEffectPercent)).BeginInit();
            this.grpAdditionalWeaponProps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackBoost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrafeBoost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackstabMultiplier)).BeginInit();
            this.grpHelmetPaperdollProps.SuspendLayout();
            this.grpPrayerProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudComboExpBoost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudComboInterval)).BeginInit();
            this.grpRegen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPRegen)).BeginInit();
            this.grpVitalBonuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMPPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudManaBonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealthBonus)).BeginInit();
            this.grpStatBonuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkillPoints)).BeginInit();
            this.grpWeaponBalance.SuspendLayout();
            this.grpArmorBalanceHelper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasionPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracyPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResistPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResistPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPiercePercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpdPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMRPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFemalePaperdoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMalePaperdoll)).BeginInit();
            this.grpEvent.SuspendLayout();
            this.grpSpell.SuspendLayout();
            this.grpBags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBag)).BeginInit();
            this.grpConsumable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.grpAuxInfo.SuspendLayout();
            this.grpEnhancement.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpItems
            // 
            this.grpItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpItems.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpItems.Controls.Add(this.btnClearSearch);
            this.grpItems.Controls.Add(this.txtSearch);
            this.grpItems.Controls.Add(this.lstGameObjects);
            this.grpItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpItems.Location = new System.Drawing.Point(12, 34);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(203, 582);
            this.grpItems.TabIndex = 1;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Items";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(179, 17);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearSearch.Size = new System.Drawing.Size(18, 20);
            this.btnClearSearch.TabIndex = 31;
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
            this.txtSearch.TabIndex = 30;
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
            this.lstGameObjects.Size = new System.Drawing.Size(191, 533);
            this.lstGameObjects.TabIndex = 29;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(790, 625);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(190, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(594, 625);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave.Size = new System.Drawing.Size(190, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpGeneral
            // 
            this.grpGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpGeneral.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpGeneral.Controls.Add(this.lblSortName);
            this.grpGeneral.Controls.Add(this.txtSortName);
            this.grpGeneral.Controls.Add(this.btnFuelRecc);
            this.grpGeneral.Controls.Add(this.nudFuel);
            this.grpGeneral.Controls.Add(this.lblFuel);
            this.grpGeneral.Controls.Add(this.chkRareDrop);
            this.grpGeneral.Controls.Add(this.btnClearOverride);
            this.grpGeneral.Controls.Add(this.cmbTypeDisplayOverride);
            this.grpGeneral.Controls.Add(this.lblOverride);
            this.grpGeneral.Controls.Add(this.grpDestroy);
            this.grpGeneral.Controls.Add(this.grpTags);
            this.grpGeneral.Controls.Add(this.grpRequirements);
            this.grpGeneral.Controls.Add(this.chkCanGuildBank);
            this.grpGeneral.Controls.Add(this.nudBankStackLimit);
            this.grpGeneral.Controls.Add(this.nudInvStackLimit);
            this.grpGeneral.Controls.Add(this.lblBankStackLimit);
            this.grpGeneral.Controls.Add(this.lblInvStackLimit);
            this.grpGeneral.Controls.Add(this.nudDeathDropChance);
            this.grpGeneral.Controls.Add(this.lblDeathDropChance);
            this.grpGeneral.Controls.Add(this.chkCanSell);
            this.grpGeneral.Controls.Add(this.chkCanTrade);
            this.grpGeneral.Controls.Add(this.chkCanBag);
            this.grpGeneral.Controls.Add(this.chkCanBank);
            this.grpGeneral.Controls.Add(this.chkIgnoreCdr);
            this.grpGeneral.Controls.Add(this.chkIgnoreGlobalCooldown);
            this.grpGeneral.Controls.Add(this.btnAddCooldownGroup);
            this.grpGeneral.Controls.Add(this.cmbCooldownGroup);
            this.grpGeneral.Controls.Add(this.lblCooldownGroup);
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
            this.grpGeneral.Controls.Add(this.cmbRarity);
            this.grpGeneral.Controls.Add(this.lblRarity);
            this.grpGeneral.Controls.Add(this.nudCooldown);
            this.grpGeneral.Controls.Add(this.lblCooldown);
            this.grpGeneral.Controls.Add(this.chkStackable);
            this.grpGeneral.Controls.Add(this.nudPrice);
            this.grpGeneral.Controls.Add(this.chkCanDrop);
            this.grpGeneral.Controls.Add(this.cmbAnimation);
            this.grpGeneral.Controls.Add(this.lblDesc);
            this.grpGeneral.Controls.Add(this.txtDesc);
            this.grpGeneral.Controls.Add(this.cmbPic);
            this.grpGeneral.Controls.Add(this.lblAnim);
            this.grpGeneral.Controls.Add(this.lblPrice);
            this.grpGeneral.Controls.Add(this.lblPic);
            this.grpGeneral.Controls.Add(this.picItem);
            this.grpGeneral.Controls.Add(this.lblType);
            this.grpGeneral.Controls.Add(this.cmbType);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpGeneral.Location = new System.Drawing.Point(3, 0);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(460, 691);
            this.grpGeneral.TabIndex = 2;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // lblSortName
            // 
            this.lblSortName.AutoSize = true;
            this.lblSortName.Location = new System.Drawing.Point(8, 55);
            this.lblSortName.Name = "lblSortName";
            this.lblSortName.Size = new System.Drawing.Size(60, 13);
            this.lblSortName.TabIndex = 109;
            this.lblSortName.Text = "Sort Name:";
            // 
            // txtSortName
            // 
            this.txtSortName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtSortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSortName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSortName.Location = new System.Drawing.Point(68, 53);
            this.txtSortName.Name = "txtSortName";
            this.txtSortName.Size = new System.Drawing.Size(130, 20);
            this.txtSortName.TabIndex = 108;
            this.txtSortName.TextChanged += new System.EventHandler(this.txtSortName_TextChanged);
            // 
            // btnFuelRecc
            // 
            this.btnFuelRecc.Location = new System.Drawing.Point(389, 168);
            this.btnFuelRecc.Name = "btnFuelRecc";
            this.btnFuelRecc.Padding = new System.Windows.Forms.Padding(5);
            this.btnFuelRecc.Size = new System.Drawing.Size(45, 21);
            this.btnFuelRecc.TabIndex = 107;
            this.btnFuelRecc.Text = "Recc.";
            this.btnFuelRecc.Click += new System.EventHandler(this.btnFuelRecc_Click);
            // 
            // nudFuel
            // 
            this.nudFuel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudFuel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudFuel.Location = new System.Drawing.Point(262, 168);
            this.nudFuel.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudFuel.Name = "nudFuel";
            this.nudFuel.Size = new System.Drawing.Size(121, 20);
            this.nudFuel.TabIndex = 106;
            this.nudFuel.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudFuel.ValueChanged += new System.EventHandler(this.nudFuel_ValueChanged);
            // 
            // lblFuel
            // 
            this.lblFuel.AutoSize = true;
            this.lblFuel.Location = new System.Drawing.Point(259, 152);
            this.lblFuel.Name = "lblFuel";
            this.lblFuel.Size = new System.Drawing.Size(30, 13);
            this.lblFuel.TabIndex = 105;
            this.lblFuel.Text = "Fuel:";
            // 
            // chkRareDrop
            // 
            this.chkRareDrop.AutoSize = true;
            this.chkRareDrop.Location = new System.Drawing.Point(54, 208);
            this.chkRareDrop.Name = "chkRareDrop";
            this.chkRareDrop.Size = new System.Drawing.Size(79, 17);
            this.chkRareDrop.TabIndex = 104;
            this.chkRareDrop.Text = "Rare drop?";
            this.chkRareDrop.CheckedChanged += new System.EventHandler(this.chkRareDrop_CheckedChanged);
            // 
            // btnClearOverride
            // 
            this.btnClearOverride.Location = new System.Drawing.Point(205, 142);
            this.btnClearOverride.Name = "btnClearOverride";
            this.btnClearOverride.Padding = new System.Windows.Forms.Padding(5);
            this.btnClearOverride.Size = new System.Drawing.Size(18, 20);
            this.btnClearOverride.TabIndex = 45;
            this.btnClearOverride.Text = "X";
            this.btnClearOverride.Click += new System.EventHandler(this.btnClearOverride_Click);
            // 
            // cmbTypeDisplayOverride
            // 
            this.cmbTypeDisplayOverride.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTypeDisplayOverride.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTypeDisplayOverride.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTypeDisplayOverride.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTypeDisplayOverride.DrawDropdownHoverOutline = false;
            this.cmbTypeDisplayOverride.DrawFocusRectangle = false;
            this.cmbTypeDisplayOverride.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeDisplayOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDisplayOverride.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTypeDisplayOverride.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTypeDisplayOverride.FormattingEnabled = true;
            this.cmbTypeDisplayOverride.Location = new System.Drawing.Point(86, 142);
            this.cmbTypeDisplayOverride.Name = "cmbTypeDisplayOverride";
            this.cmbTypeDisplayOverride.Size = new System.Drawing.Size(113, 21);
            this.cmbTypeDisplayOverride.TabIndex = 103;
            this.cmbTypeDisplayOverride.Text = "None";
            this.cmbTypeDisplayOverride.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTypeDisplayOverride.SelectedIndexChanged += new System.EventHandler(this.cmbTypeDisplayOverride_SelectedIndexChanged);
            // 
            // lblOverride
            // 
            this.lblOverride.AutoSize = true;
            this.lblOverride.Location = new System.Drawing.Point(10, 146);
            this.lblOverride.Name = "lblOverride";
            this.lblOverride.Size = new System.Drawing.Size(74, 13);
            this.lblOverride.TabIndex = 102;
            this.lblOverride.Text = "Type Override";
            // 
            // grpDestroy
            // 
            this.grpDestroy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDestroy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDestroy.Controls.Add(this.chkInstanceDestroy);
            this.grpDestroy.Controls.Add(this.chkEnableDestroy);
            this.grpDestroy.Controls.Add(this.lblDestroyMessage);
            this.grpDestroy.Controls.Add(this.txtCannotDestroy);
            this.grpDestroy.Controls.Add(this.btnDestroyRequirements);
            this.grpDestroy.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDestroy.Location = new System.Drawing.Point(11, 326);
            this.grpDestroy.Margin = new System.Windows.Forms.Padding(2);
            this.grpDestroy.Name = "grpDestroy";
            this.grpDestroy.Padding = new System.Windows.Forms.Padding(2);
            this.grpDestroy.Size = new System.Drawing.Size(232, 123);
            this.grpDestroy.TabIndex = 101;
            this.grpDestroy.TabStop = false;
            this.grpDestroy.Text = "Item Destruction";
            // 
            // chkInstanceDestroy
            // 
            this.chkInstanceDestroy.AutoSize = true;
            this.chkInstanceDestroy.Location = new System.Drawing.Point(8, 95);
            this.chkInstanceDestroy.Name = "chkInstanceDestroy";
            this.chkInstanceDestroy.Size = new System.Drawing.Size(169, 17);
            this.chkInstanceDestroy.TabIndex = 103;
            this.chkInstanceDestroy.Text = "Destroy On Instance Change?";
            this.chkInstanceDestroy.CheckedChanged += new System.EventHandler(this.chkInstanceDestroy_CheckedChanged);
            // 
            // chkEnableDestroy
            // 
            this.chkEnableDestroy.AutoSize = true;
            this.chkEnableDestroy.Location = new System.Drawing.Point(8, 20);
            this.chkEnableDestroy.Name = "chkEnableDestroy";
            this.chkEnableDestroy.Size = new System.Drawing.Size(59, 17);
            this.chkEnableDestroy.TabIndex = 102;
            this.chkEnableDestroy.Text = "Enable";
            this.chkEnableDestroy.CheckedChanged += new System.EventHandler(this.chkEnableDestroy_CheckedChanged);
            // 
            // lblDestroyMessage
            // 
            this.lblDestroyMessage.AutoSize = true;
            this.lblDestroyMessage.Location = new System.Drawing.Point(5, 47);
            this.lblDestroyMessage.Name = "lblDestroyMessage";
            this.lblDestroyMessage.Size = new System.Drawing.Size(129, 13);
            this.lblDestroyMessage.TabIndex = 54;
            this.lblDestroyMessage.Text = "Cannot Destroy Message:";
            // 
            // txtCannotDestroy
            // 
            this.txtCannotDestroy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCannotDestroy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCannotDestroy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCannotDestroy.Location = new System.Drawing.Point(8, 63);
            this.txtCannotDestroy.Name = "txtCannotDestroy";
            this.txtCannotDestroy.Size = new System.Drawing.Size(219, 20);
            this.txtCannotDestroy.TabIndex = 53;
            this.txtCannotDestroy.TextChanged += new System.EventHandler(this.txtCannotDestroy_TextChanged);
            // 
            // btnDestroyRequirements
            // 
            this.btnDestroyRequirements.Location = new System.Drawing.Point(73, 18);
            this.btnDestroyRequirements.Name = "btnDestroyRequirements";
            this.btnDestroyRequirements.Padding = new System.Windows.Forms.Padding(5);
            this.btnDestroyRequirements.Size = new System.Drawing.Size(154, 23);
            this.btnDestroyRequirements.TabIndex = 0;
            this.btnDestroyRequirements.Text = "Edit Destroy Requirements";
            this.btnDestroyRequirements.Click += new System.EventHandler(this.btnDestroyRequirements_Click);
            // 
            // grpTags
            // 
            this.grpTags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpTags.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpTags.Controls.Add(this.lstTags);
            this.grpTags.Controls.Add(this.btnRemoveTag);
            this.grpTags.Controls.Add(this.btnAddTag);
            this.grpTags.Controls.Add(this.lblTagToAdd);
            this.grpTags.Controls.Add(this.cmbTags);
            this.grpTags.Controls.Add(this.btnNewTag);
            this.grpTags.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpTags.Location = new System.Drawing.Point(6, 552);
            this.grpTags.Margin = new System.Windows.Forms.Padding(2);
            this.grpTags.Name = "grpTags";
            this.grpTags.Padding = new System.Windows.Forms.Padding(2);
            this.grpTags.Size = new System.Drawing.Size(422, 124);
            this.grpTags.TabIndex = 101;
            this.grpTags.TabStop = false;
            this.grpTags.Text = "Tags";
            // 
            // lstTags
            // 
            this.lstTags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTags.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstTags.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstTags.FormattingEnabled = true;
            this.lstTags.Location = new System.Drawing.Point(127, 14);
            this.lstTags.Name = "lstTags";
            this.lstTags.Size = new System.Drawing.Size(284, 67);
            this.lstTags.TabIndex = 106;
            // 
            // btnRemoveTag
            // 
            this.btnRemoveTag.Location = new System.Drawing.Point(336, 90);
            this.btnRemoveTag.Name = "btnRemoveTag";
            this.btnRemoveTag.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveTag.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveTag.TabIndex = 105;
            this.btnRemoveTag.Text = "Remove";
            this.btnRemoveTag.Click += new System.EventHandler(this.btnRemoveTag_Click);
            // 
            // btnAddTag
            // 
            this.btnAddTag.Location = new System.Drawing.Point(253, 90);
            this.btnAddTag.Name = "btnAddTag";
            this.btnAddTag.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddTag.Size = new System.Drawing.Size(74, 23);
            this.btnAddTag.TabIndex = 104;
            this.btnAddTag.Text = "Add";
            this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
            // 
            // lblTagToAdd
            // 
            this.lblTagToAdd.AutoSize = true;
            this.lblTagToAdd.Location = new System.Drawing.Point(32, 95);
            this.lblTagToAdd.Name = "lblTagToAdd";
            this.lblTagToAdd.Size = new System.Drawing.Size(63, 13);
            this.lblTagToAdd.TabIndex = 55;
            this.lblTagToAdd.Text = "Tag to Add:";
            // 
            // cmbTags
            // 
            this.cmbTags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTags.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTags.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTags.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTags.DrawDropdownHoverOutline = false;
            this.cmbTags.DrawFocusRectangle = false;
            this.cmbTags.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTags.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTags.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTags.FormattingEnabled = true;
            this.cmbTags.Location = new System.Drawing.Point(97, 92);
            this.cmbTags.Name = "cmbTags";
            this.cmbTags.Size = new System.Drawing.Size(147, 21);
            this.cmbTags.TabIndex = 102;
            this.cmbTags.Text = null;
            this.cmbTags.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTags.SelectedIndexChanged += new System.EventHandler(this.cmbTags_SelectedIndexChanged);
            // 
            // btnNewTag
            // 
            this.btnNewTag.Location = new System.Drawing.Point(21, 25);
            this.btnNewTag.Name = "btnNewTag";
            this.btnNewTag.Padding = new System.Windows.Forms.Padding(5);
            this.btnNewTag.Size = new System.Drawing.Size(74, 28);
            this.btnNewTag.TabIndex = 103;
            this.btnNewTag.Text = "New Tag";
            this.btnNewTag.Click += new System.EventHandler(this.btnNewTag_Click);
            // 
            // grpRequirements
            // 
            this.grpRequirements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRequirements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRequirements.Controls.Add(this.lblCannotUse);
            this.grpRequirements.Controls.Add(this.txtCannotUse);
            this.grpRequirements.Controls.Add(this.btnEditRequirements);
            this.grpRequirements.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRequirements.Location = new System.Drawing.Point(11, 455);
            this.grpRequirements.Margin = new System.Windows.Forms.Padding(2);
            this.grpRequirements.Name = "grpRequirements";
            this.grpRequirements.Padding = new System.Windows.Forms.Padding(2);
            this.grpRequirements.Size = new System.Drawing.Size(229, 92);
            this.grpRequirements.TabIndex = 100;
            this.grpRequirements.TabStop = false;
            this.grpRequirements.Text = "Requirements";
            // 
            // lblCannotUse
            // 
            this.lblCannotUse.AutoSize = true;
            this.lblCannotUse.Location = new System.Drawing.Point(5, 47);
            this.lblCannotUse.Name = "lblCannotUse";
            this.lblCannotUse.Size = new System.Drawing.Size(112, 13);
            this.lblCannotUse.TabIndex = 54;
            this.lblCannotUse.Text = "Cannot Use Message:";
            // 
            // txtCannotUse
            // 
            this.txtCannotUse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCannotUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCannotUse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCannotUse.Location = new System.Drawing.Point(8, 63);
            this.txtCannotUse.Name = "txtCannotUse";
            this.txtCannotUse.Size = new System.Drawing.Size(216, 20);
            this.txtCannotUse.TabIndex = 53;
            this.txtCannotUse.TextChanged += new System.EventHandler(this.txtCannotUse_TextChanged);
            // 
            // btnEditRequirements
            // 
            this.btnEditRequirements.Location = new System.Drawing.Point(8, 18);
            this.btnEditRequirements.Name = "btnEditRequirements";
            this.btnEditRequirements.Padding = new System.Windows.Forms.Padding(5);
            this.btnEditRequirements.Size = new System.Drawing.Size(216, 23);
            this.btnEditRequirements.TabIndex = 0;
            this.btnEditRequirements.Text = "Edit Usage Requirements";
            this.btnEditRequirements.Click += new System.EventHandler(this.btnEditRequirements_Click);
            // 
            // chkCanGuildBank
            // 
            this.chkCanGuildBank.AutoSize = true;
            this.chkCanGuildBank.Location = new System.Drawing.Point(343, 465);
            this.chkCanGuildBank.Name = "chkCanGuildBank";
            this.chkCanGuildBank.Size = new System.Drawing.Size(106, 17);
            this.chkCanGuildBank.TabIndex = 99;
            this.chkCanGuildBank.Text = "Can Guild Bank?";
            this.chkCanGuildBank.CheckedChanged += new System.EventHandler(this.chkCanGuildBank_CheckedChanged);
            // 
            // nudBankStackLimit
            // 
            this.nudBankStackLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBankStackLimit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBankStackLimit.Location = new System.Drawing.Point(262, 439);
            this.nudBankStackLimit.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudBankStackLimit.Name = "nudBankStackLimit";
            this.nudBankStackLimit.Size = new System.Drawing.Size(171, 20);
            this.nudBankStackLimit.TabIndex = 98;
            this.nudBankStackLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBankStackLimit.ValueChanged += new System.EventHandler(this.nudBankStackLimit_ValueChanged);
            // 
            // nudInvStackLimit
            // 
            this.nudInvStackLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudInvStackLimit.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudInvStackLimit.Location = new System.Drawing.Point(262, 400);
            this.nudInvStackLimit.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudInvStackLimit.Name = "nudInvStackLimit";
            this.nudInvStackLimit.Size = new System.Drawing.Size(171, 20);
            this.nudInvStackLimit.TabIndex = 97;
            this.nudInvStackLimit.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudInvStackLimit.ValueChanged += new System.EventHandler(this.nudInvStackLimit_ValueChanged);
            // 
            // lblBankStackLimit
            // 
            this.lblBankStackLimit.AutoSize = true;
            this.lblBankStackLimit.Location = new System.Drawing.Point(259, 422);
            this.lblBankStackLimit.Name = "lblBankStackLimit";
            this.lblBankStackLimit.Size = new System.Drawing.Size(90, 13);
            this.lblBankStackLimit.TabIndex = 96;
            this.lblBankStackLimit.Text = "Bank Stack Limit:";
            // 
            // lblInvStackLimit
            // 
            this.lblInvStackLimit.AutoSize = true;
            this.lblInvStackLimit.Location = new System.Drawing.Point(259, 384);
            this.lblInvStackLimit.Name = "lblInvStackLimit";
            this.lblInvStackLimit.Size = new System.Drawing.Size(109, 13);
            this.lblInvStackLimit.TabIndex = 95;
            this.lblInvStackLimit.Text = "Inventory Stack Limit:";
            // 
            // nudDeathDropChance
            // 
            this.nudDeathDropChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDeathDropChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDeathDropChance.Location = new System.Drawing.Point(401, 527);
            this.nudDeathDropChance.Name = "nudDeathDropChance";
            this.nudDeathDropChance.Size = new System.Drawing.Size(48, 20);
            this.nudDeathDropChance.TabIndex = 94;
            this.nudDeathDropChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDeathDropChance.Visible = false;
            this.nudDeathDropChance.ValueChanged += new System.EventHandler(this.nudDeathDropChance_ValueChanged);
            // 
            // lblDeathDropChance
            // 
            this.lblDeathDropChance.AutoSize = true;
            this.lblDeathDropChance.Location = new System.Drawing.Point(259, 529);
            this.lblDeathDropChance.Name = "lblDeathDropChance";
            this.lblDeathDropChance.Size = new System.Drawing.Size(136, 13);
            this.lblDeathDropChance.TabIndex = 93;
            this.lblDeathDropChance.Text = "Drop chance on Death (%):";
            this.lblDeathDropChance.Visible = false;
            // 
            // chkCanSell
            // 
            this.chkCanSell.AutoSize = true;
            this.chkCanSell.Location = new System.Drawing.Point(343, 505);
            this.chkCanSell.Name = "chkCanSell";
            this.chkCanSell.Size = new System.Drawing.Size(71, 17);
            this.chkCanSell.TabIndex = 92;
            this.chkCanSell.Text = "Can Sell?";
            this.chkCanSell.CheckedChanged += new System.EventHandler(this.chkCanSell_CheckedChanged);
            // 
            // chkCanTrade
            // 
            this.chkCanTrade.AutoSize = true;
            this.chkCanTrade.Location = new System.Drawing.Point(260, 505);
            this.chkCanTrade.Name = "chkCanTrade";
            this.chkCanTrade.Size = new System.Drawing.Size(82, 17);
            this.chkCanTrade.TabIndex = 91;
            this.chkCanTrade.Text = "Can Trade?";
            this.chkCanTrade.CheckedChanged += new System.EventHandler(this.chkCanTrade_CheckedChanged);
            // 
            // chkCanBag
            // 
            this.chkCanBag.AutoSize = true;
            this.chkCanBag.Location = new System.Drawing.Point(343, 485);
            this.chkCanBag.Name = "chkCanBag";
            this.chkCanBag.Size = new System.Drawing.Size(73, 17);
            this.chkCanBag.TabIndex = 90;
            this.chkCanBag.Text = "Can Bag?";
            this.chkCanBag.CheckedChanged += new System.EventHandler(this.chkCanBag_CheckedChanged);
            // 
            // chkCanBank
            // 
            this.chkCanBank.AutoSize = true;
            this.chkCanBank.Location = new System.Drawing.Point(260, 485);
            this.chkCanBank.Name = "chkCanBank";
            this.chkCanBank.Size = new System.Drawing.Size(79, 17);
            this.chkCanBank.TabIndex = 89;
            this.chkCanBank.Text = "Can Bank?";
            this.chkCanBank.CheckedChanged += new System.EventHandler(this.chkCanBank_CheckedChanged);
            // 
            // chkIgnoreCdr
            // 
            this.chkIgnoreCdr.AutoSize = true;
            this.chkIgnoreCdr.Location = new System.Drawing.Point(262, 334);
            this.chkIgnoreCdr.Name = "chkIgnoreCdr";
            this.chkIgnoreCdr.Size = new System.Drawing.Size(164, 17);
            this.chkIgnoreCdr.TabIndex = 87;
            this.chkIgnoreCdr.Text = "Ignore Cooldown Reduction?";
            this.chkIgnoreCdr.CheckedChanged += new System.EventHandler(this.chkIgnoreCdr_CheckedChanged);
            // 
            // chkIgnoreGlobalCooldown
            // 
            this.chkIgnoreGlobalCooldown.AutoSize = true;
            this.chkIgnoreGlobalCooldown.Location = new System.Drawing.Point(262, 315);
            this.chkIgnoreGlobalCooldown.Name = "chkIgnoreGlobalCooldown";
            this.chkIgnoreGlobalCooldown.Size = new System.Drawing.Size(145, 17);
            this.chkIgnoreGlobalCooldown.TabIndex = 53;
            this.chkIgnoreGlobalCooldown.Text = "Ignore Global Cooldown?";
            this.chkIgnoreGlobalCooldown.CheckedChanged += new System.EventHandler(this.chkIgnoreGlobalCooldown_CheckedChanged);
            // 
            // btnAddCooldownGroup
            // 
            this.btnAddCooldownGroup.Location = new System.Drawing.Point(415, 292);
            this.btnAddCooldownGroup.Name = "btnAddCooldownGroup";
            this.btnAddCooldownGroup.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddCooldownGroup.Size = new System.Drawing.Size(18, 21);
            this.btnAddCooldownGroup.TabIndex = 52;
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
            this.cmbCooldownGroup.Location = new System.Drawing.Point(262, 292);
            this.cmbCooldownGroup.Name = "cmbCooldownGroup";
            this.cmbCooldownGroup.Size = new System.Drawing.Size(147, 21);
            this.cmbCooldownGroup.TabIndex = 51;
            this.cmbCooldownGroup.Text = null;
            this.cmbCooldownGroup.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbCooldownGroup.SelectedIndexChanged += new System.EventHandler(this.cmbCooldownGroup_SelectedIndexChanged);
            // 
            // lblCooldownGroup
            // 
            this.lblCooldownGroup.AutoSize = true;
            this.lblCooldownGroup.Location = new System.Drawing.Point(259, 275);
            this.lblCooldownGroup.Name = "lblCooldownGroup";
            this.lblCooldownGroup.Size = new System.Drawing.Size(89, 13);
            this.lblCooldownGroup.TabIndex = 50;
            this.lblCooldownGroup.Text = "Cooldown Group:";
            // 
            // lblAlpha
            // 
            this.lblAlpha.AutoSize = true;
            this.lblAlpha.Location = new System.Drawing.Point(344, 88);
            this.lblAlpha.Name = "lblAlpha";
            this.lblAlpha.Size = new System.Drawing.Size(37, 13);
            this.lblAlpha.TabIndex = 86;
            this.lblAlpha.Text = "Alpha:";
            // 
            // lblBlue
            // 
            this.lblBlue.AutoSize = true;
            this.lblBlue.Location = new System.Drawing.Point(259, 86);
            this.lblBlue.Name = "lblBlue";
            this.lblBlue.Size = new System.Drawing.Size(31, 13);
            this.lblBlue.TabIndex = 85;
            this.lblBlue.Text = "Blue:";
            // 
            // lblGreen
            // 
            this.lblGreen.AutoSize = true;
            this.lblGreen.Location = new System.Drawing.Point(344, 62);
            this.lblGreen.Name = "lblGreen";
            this.lblGreen.Size = new System.Drawing.Size(39, 13);
            this.lblGreen.TabIndex = 84;
            this.lblGreen.Text = "Green:";
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(259, 60);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(30, 13);
            this.lblRed.TabIndex = 83;
            this.lblRed.Text = "Red:";
            // 
            // nudRgbaA
            // 
            this.nudRgbaA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRgbaA.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRgbaA.Location = new System.Drawing.Point(391, 86);
            this.nudRgbaA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaA.Name = "nudRgbaA";
            this.nudRgbaA.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaA.TabIndex = 82;
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
            this.nudRgbaB.Location = new System.Drawing.Point(296, 84);
            this.nudRgbaB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaB.Name = "nudRgbaB";
            this.nudRgbaB.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaB.TabIndex = 81;
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
            this.nudRgbaG.Location = new System.Drawing.Point(391, 60);
            this.nudRgbaG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaG.Name = "nudRgbaG";
            this.nudRgbaG.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaG.TabIndex = 80;
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
            this.nudRgbaR.Location = new System.Drawing.Point(296, 58);
            this.nudRgbaR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaR.Name = "nudRgbaR";
            this.nudRgbaR.Size = new System.Drawing.Size(42, 20);
            this.nudRgbaR.TabIndex = 79;
            this.nudRgbaR.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRgbaR.ValueChanged += new System.EventHandler(this.nudRgbaR_ValueChanged);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(180, 79);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddFolder.Size = new System.Drawing.Size(18, 21);
            this.btnAddFolder.TabIndex = 49;
            this.btnAddFolder.Text = "+";
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(8, 83);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 48;
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
            this.cmbFolder.Location = new System.Drawing.Point(53, 79);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(123, 21);
            this.cmbFolder.TabIndex = 47;
            this.cmbFolder.Text = null;
            this.cmbFolder.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmbFolder_SelectedIndexChanged);
            // 
            // cmbRarity
            // 
            this.cmbRarity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbRarity.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbRarity.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbRarity.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbRarity.DrawDropdownHoverOutline = false;
            this.cmbRarity.DrawFocusRectangle = false;
            this.cmbRarity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRarity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRarity.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbRarity.FormattingEnabled = true;
            this.cmbRarity.Items.AddRange(new object[] {
            "None",
            "Common",
            "Uncommon",
            "Rare",
            "Epic",
            "Legendary"});
            this.cmbRarity.Location = new System.Drawing.Point(54, 182);
            this.cmbRarity.Name = "cmbRarity";
            this.cmbRarity.Size = new System.Drawing.Size(149, 21);
            this.cmbRarity.TabIndex = 41;
            this.cmbRarity.Text = "None";
            this.cmbRarity.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbRarity.SelectedIndexChanged += new System.EventHandler(this.cmbRarity_SelectedIndexChanged);
            // 
            // lblRarity
            // 
            this.lblRarity.AutoSize = true;
            this.lblRarity.Location = new System.Drawing.Point(9, 185);
            this.lblRarity.Name = "lblRarity";
            this.lblRarity.Size = new System.Drawing.Size(37, 13);
            this.lblRarity.TabIndex = 40;
            this.lblRarity.Text = "Rarity:";
            // 
            // nudCooldown
            // 
            this.nudCooldown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCooldown.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCooldown.Location = new System.Drawing.Point(262, 248);
            this.nudCooldown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudCooldown.Name = "nudCooldown";
            this.nudCooldown.Size = new System.Drawing.Size(171, 20);
            this.nudCooldown.TabIndex = 39;
            this.nudCooldown.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCooldown.ValueChanged += new System.EventHandler(this.nudCooldown_ValueChanged);
            // 
            // lblCooldown
            // 
            this.lblCooldown.AutoSize = true;
            this.lblCooldown.Location = new System.Drawing.Point(259, 231);
            this.lblCooldown.Name = "lblCooldown";
            this.lblCooldown.Size = new System.Drawing.Size(79, 13);
            this.lblCooldown.TabIndex = 38;
            this.lblCooldown.Text = "Cooldown (ms):";
            // 
            // chkStackable
            // 
            this.chkStackable.AutoSize = true;
            this.chkStackable.Location = new System.Drawing.Point(262, 364);
            this.chkStackable.Name = "chkStackable";
            this.chkStackable.Size = new System.Drawing.Size(80, 17);
            this.chkStackable.TabIndex = 27;
            this.chkStackable.Text = "Stackable?";
            this.chkStackable.CheckedChanged += new System.EventHandler(this.chkStackable_CheckedChanged);
            // 
            // nudPrice
            // 
            this.nudPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPrice.Location = new System.Drawing.Point(262, 128);
            this.nudPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(171, 20);
            this.nudPrice.TabIndex = 37;
            this.nudPrice.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPrice.ValueChanged += new System.EventHandler(this.nudPrice_ValueChanged);
            // 
            // chkCanDrop
            // 
            this.chkCanDrop.AutoSize = true;
            this.chkCanDrop.Location = new System.Drawing.Point(260, 465);
            this.chkCanDrop.Name = "chkCanDrop";
            this.chkCanDrop.Size = new System.Drawing.Size(77, 17);
            this.chkCanDrop.TabIndex = 26;
            this.chkCanDrop.Text = "Can Drop?";
            this.chkCanDrop.CheckedChanged += new System.EventHandler(this.chkBound_CheckedChanged);
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
            this.cmbAnimation.Location = new System.Drawing.Point(262, 206);
            this.cmbAnimation.Name = "cmbAnimation";
            this.cmbAnimation.Size = new System.Drawing.Size(171, 21);
            this.cmbAnimation.TabIndex = 14;
            this.cmbAnimation.Text = null;
            this.cmbAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAnimation_SelectedIndexChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 244);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(35, 13);
            this.lblDesc.TabIndex = 13;
            this.lblDesc.Text = "Desc:";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtDesc.Location = new System.Drawing.Point(12, 260);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(186, 53);
            this.txtDesc.TabIndex = 12;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // cmbPic
            // 
            this.cmbPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbPic.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbPic.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbPic.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbPic.DrawDropdownHoverOutline = false;
            this.cmbPic.DrawFocusRectangle = false;
            this.cmbPic.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPic.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbPic.FormattingEnabled = true;
            this.cmbPic.Items.AddRange(new object[] {
            "None"});
            this.cmbPic.Location = new System.Drawing.Point(262, 31);
            this.cmbPic.Name = "cmbPic";
            this.cmbPic.Size = new System.Drawing.Size(171, 21);
            this.cmbPic.TabIndex = 11;
            this.cmbPic.Text = "None";
            this.cmbPic.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbPic.SelectedIndexChanged += new System.EventHandler(this.cmbPic_SelectedIndexChanged);
            // 
            // lblAnim
            // 
            this.lblAnim.AutoSize = true;
            this.lblAnim.Location = new System.Drawing.Point(259, 190);
            this.lblAnim.Name = "lblAnim";
            this.lblAnim.Size = new System.Drawing.Size(53, 13);
            this.lblAnim.TabIndex = 9;
            this.lblAnim.Text = "Animation";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(259, 111);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(34, 13);
            this.lblPrice.TabIndex = 7;
            this.lblPrice.Text = "Price:";
            // 
            // lblPic
            // 
            this.lblPic.AutoSize = true;
            this.lblPic.Location = new System.Drawing.Point(259, 14);
            this.lblPic.Name = "lblPic";
            this.lblPic.Size = new System.Drawing.Size(25, 13);
            this.lblPic.TabIndex = 6;
            this.lblPic.Text = "Pic:";
            // 
            // picItem
            // 
            this.picItem.BackColor = System.Drawing.Color.Black;
            this.picItem.Location = new System.Drawing.Point(221, 25);
            this.picItem.Name = "picItem";
            this.picItem.Size = new System.Drawing.Size(32, 32);
            this.picItem.TabIndex = 4;
            this.picItem.TabStop = false;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(9, 118);
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
            "None",
            "Equipment",
            "Consumable",
            "Currency",
            "Spell",
            "Event",
            "Bag"});
            this.cmbType.Location = new System.Drawing.Point(49, 115);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(149, 21);
            this.cmbType.TabIndex = 2;
            this.cmbType.Text = "None";
            this.cmbType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 27);
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
            this.txtName.Location = new System.Drawing.Point(53, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(145, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblDrops
            // 
            this.lblDrops.AutoSize = true;
            this.lblDrops.Location = new System.Drawing.Point(14, 19);
            this.lblDrops.Name = "lblDrops";
            this.lblDrops.Size = new System.Drawing.Size(104, 13);
            this.lblDrops.TabIndex = 108;
            this.lblDrops.Text = "NPCs that Drop This";
            // 
            // lstDrops
            // 
            this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstDrops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDrops.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDrops.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstDrops.FormattingEnabled = true;
            this.lstDrops.Location = new System.Drawing.Point(17, 35);
            this.lstDrops.Name = "lstDrops";
            this.lstDrops.Size = new System.Drawing.Size(229, 106);
            this.lstDrops.TabIndex = 107;
            // 
            // grpEquipment
            // 
            this.grpEquipment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEquipment.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEquipment.Controls.Add(this.grpProc);
            this.grpEquipment.Controls.Add(this.grpUpgrades);
            this.grpEquipment.Controls.Add(this.grpWeaponEnhancement);
            this.grpEquipment.Controls.Add(this.grpDeconstruction);
            this.grpEquipment.Controls.Add(this.grpWeaponTypes);
            this.grpEquipment.Controls.Add(this.grpWeaponProperties);
            this.grpEquipment.Controls.Add(this.grpCosmetic);
            this.grpEquipment.Controls.Add(this.grpSpecialAttack);
            this.grpEquipment.Controls.Add(this.grpBonusEffects);
            this.grpEquipment.Controls.Add(this.grpAdditionalWeaponProps);
            this.grpEquipment.Controls.Add(this.grpHelmetPaperdollProps);
            this.grpEquipment.Controls.Add(this.grpPrayerProperties);
            this.grpEquipment.Controls.Add(this.grpRegen);
            this.grpEquipment.Controls.Add(this.grpVitalBonuses);
            this.grpEquipment.Controls.Add(this.cmbEquipmentAnimation);
            this.grpEquipment.Controls.Add(this.lblEquipmentAnimation);
            this.grpEquipment.Controls.Add(this.grpStatBonuses);
            this.grpEquipment.Controls.Add(this.cmbFemalePaperdoll);
            this.grpEquipment.Controls.Add(this.lblFemalePaperdoll);
            this.grpEquipment.Controls.Add(this.picFemalePaperdoll);
            this.grpEquipment.Controls.Add(this.cmbEquipmentSlot);
            this.grpEquipment.Controls.Add(this.lblEquipmentSlot);
            this.grpEquipment.Controls.Add(this.cmbMalePaperdoll);
            this.grpEquipment.Controls.Add(this.lblMalePaperdoll);
            this.grpEquipment.Controls.Add(this.picMalePaperdoll);
            this.grpEquipment.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEquipment.Location = new System.Drawing.Point(2, 700);
            this.grpEquipment.Name = "grpEquipment";
            this.grpEquipment.Size = new System.Drawing.Size(732, 1164);
            this.grpEquipment.TabIndex = 12;
            this.grpEquipment.TabStop = false;
            this.grpEquipment.Text = "Equipment";
            this.grpEquipment.Visible = false;
            // 
            // grpProc
            // 
            this.grpProc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpProc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpProc.Controls.Add(this.nudProcChance);
            this.grpProc.Controls.Add(this.label3);
            this.grpProc.Controls.Add(this.lblProcChance);
            this.grpProc.Controls.Add(this.cmbProcSpell);
            this.grpProc.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpProc.Location = new System.Drawing.Point(221, 1045);
            this.grpProc.Margin = new System.Windows.Forms.Padding(2);
            this.grpProc.Name = "grpProc";
            this.grpProc.Padding = new System.Windows.Forms.Padding(2);
            this.grpProc.Size = new System.Drawing.Size(198, 97);
            this.grpProc.TabIndex = 124;
            this.grpProc.TabStop = false;
            this.grpProc.Text = "Spell Proccing";
            // 
            // nudProcChance
            // 
            this.nudProcChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudProcChance.DecimalPlaces = 2;
            this.nudProcChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudProcChance.Location = new System.Drawing.Point(88, 65);
            this.nudProcChance.Name = "nudProcChance";
            this.nudProcChance.Size = new System.Drawing.Size(100, 20);
            this.nudProcChance.TabIndex = 124;
            this.nudProcChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudProcChance.ValueChanged += new System.EventHandler(this.nudProcChance_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Proc Spell";
            // 
            // lblProcChance
            // 
            this.lblProcChance.AutoSize = true;
            this.lblProcChance.Location = new System.Drawing.Point(13, 67);
            this.lblProcChance.Name = "lblProcChance";
            this.lblProcChance.Size = new System.Drawing.Size(61, 13);
            this.lblProcChance.TabIndex = 31;
            this.lblProcChance.Text = "Chance (%)";
            // 
            // cmbProcSpell
            // 
            this.cmbProcSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbProcSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbProcSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbProcSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbProcSpell.DrawDropdownHoverOutline = false;
            this.cmbProcSpell.DrawFocusRectangle = false;
            this.cmbProcSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProcSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProcSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbProcSpell.FormattingEnabled = true;
            this.cmbProcSpell.Location = new System.Drawing.Point(11, 38);
            this.cmbProcSpell.Name = "cmbProcSpell";
            this.cmbProcSpell.Size = new System.Drawing.Size(177, 21);
            this.cmbProcSpell.TabIndex = 29;
            this.cmbProcSpell.Text = null;
            this.cmbProcSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbProcSpell.SelectedIndexChanged += new System.EventHandler(this.cmbProcSpell_SelectedIndexChanged);
            // 
            // grpUpgrades
            // 
            this.grpUpgrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpUpgrades.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpUpgrades.Controls.Add(this.lstUpgrades);
            this.grpUpgrades.Controls.Add(this.btnRemoveUpgrade);
            this.grpUpgrades.Controls.Add(this.btnAddUpgrade);
            this.grpUpgrades.Controls.Add(this.lblUpgrade);
            this.grpUpgrades.Controls.Add(this.cmbUpgrade);
            this.grpUpgrades.Controls.Add(this.nudUpgradeCost);
            this.grpUpgrades.Controls.Add(this.lblUpgradeCost);
            this.grpUpgrades.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpUpgrades.Location = new System.Drawing.Point(434, 954);
            this.grpUpgrades.Margin = new System.Windows.Forms.Padding(2);
            this.grpUpgrades.Name = "grpUpgrades";
            this.grpUpgrades.Padding = new System.Windows.Forms.Padding(2);
            this.grpUpgrades.Size = new System.Drawing.Size(212, 202);
            this.grpUpgrades.TabIndex = 123;
            this.grpUpgrades.TabStop = false;
            this.grpUpgrades.Text = "Upgrading";
            // 
            // lstUpgrades
            // 
            this.lstUpgrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstUpgrades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstUpgrades.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstUpgrades.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstUpgrades.FormattingEnabled = true;
            this.lstUpgrades.Location = new System.Drawing.Point(8, 16);
            this.lstUpgrades.Name = "lstUpgrades";
            this.lstUpgrades.Size = new System.Drawing.Size(199, 67);
            this.lstUpgrades.TabIndex = 125;
            // 
            // btnRemoveUpgrade
            // 
            this.btnRemoveUpgrade.Location = new System.Drawing.Point(133, 168);
            this.btnRemoveUpgrade.Name = "btnRemoveUpgrade";
            this.btnRemoveUpgrade.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveUpgrade.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveUpgrade.TabIndex = 124;
            this.btnRemoveUpgrade.Text = "Remove";
            this.btnRemoveUpgrade.Click += new System.EventHandler(this.btnRemoveUpgrade_Click);
            // 
            // btnAddUpgrade
            // 
            this.btnAddUpgrade.Location = new System.Drawing.Point(8, 168);
            this.btnAddUpgrade.Name = "btnAddUpgrade";
            this.btnAddUpgrade.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddUpgrade.Size = new System.Drawing.Size(74, 23);
            this.btnAddUpgrade.TabIndex = 123;
            this.btnAddUpgrade.Text = "Add";
            this.btnAddUpgrade.Click += new System.EventHandler(this.btnAddUpgrade_Click);
            // 
            // lblUpgrade
            // 
            this.lblUpgrade.AutoSize = true;
            this.lblUpgrade.Location = new System.Drawing.Point(5, 86);
            this.lblUpgrade.Name = "lblUpgrade";
            this.lblUpgrade.Size = new System.Drawing.Size(73, 13);
            this.lblUpgrade.TabIndex = 122;
            this.lblUpgrade.Text = "Upgrade Craft";
            // 
            // cmbUpgrade
            // 
            this.cmbUpgrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbUpgrade.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbUpgrade.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbUpgrade.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbUpgrade.DrawDropdownHoverOutline = false;
            this.cmbUpgrade.DrawFocusRectangle = false;
            this.cmbUpgrade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbUpgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUpgrade.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbUpgrade.FormattingEnabled = true;
            this.cmbUpgrade.Location = new System.Drawing.Point(9, 102);
            this.cmbUpgrade.Name = "cmbUpgrade";
            this.cmbUpgrade.Size = new System.Drawing.Size(198, 21);
            this.cmbUpgrade.TabIndex = 121;
            this.cmbUpgrade.Text = null;
            this.cmbUpgrade.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // nudUpgradeCost
            // 
            this.nudUpgradeCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudUpgradeCost.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudUpgradeCost.Location = new System.Drawing.Point(8, 142);
            this.nudUpgradeCost.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.nudUpgradeCost.Name = "nudUpgradeCost";
            this.nudUpgradeCost.Size = new System.Drawing.Size(199, 20);
            this.nudUpgradeCost.TabIndex = 120;
            this.nudUpgradeCost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // lblUpgradeCost
            // 
            this.lblUpgradeCost.AutoSize = true;
            this.lblUpgradeCost.Location = new System.Drawing.Point(6, 126);
            this.lblUpgradeCost.Name = "lblUpgradeCost";
            this.lblUpgradeCost.Size = new System.Drawing.Size(28, 13);
            this.lblUpgradeCost.TabIndex = 119;
            this.lblUpgradeCost.Text = "Cost";
            // 
            // grpWeaponEnhancement
            // 
            this.grpWeaponEnhancement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponEnhancement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponEnhancement.Controls.Add(this.nudEnhanceThresh);
            this.grpWeaponEnhancement.Controls.Add(this.lblEnhancementThres);
            this.grpWeaponEnhancement.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponEnhancement.Location = new System.Drawing.Point(223, 976);
            this.grpWeaponEnhancement.Margin = new System.Windows.Forms.Padding(2);
            this.grpWeaponEnhancement.Name = "grpWeaponEnhancement";
            this.grpWeaponEnhancement.Padding = new System.Windows.Forms.Padding(2);
            this.grpWeaponEnhancement.Size = new System.Drawing.Size(205, 67);
            this.grpWeaponEnhancement.TabIndex = 122;
            this.grpWeaponEnhancement.TabStop = false;
            this.grpWeaponEnhancement.Text = "Enhancement";
            // 
            // nudEnhanceThresh
            // 
            this.nudEnhanceThresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEnhanceThresh.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEnhanceThresh.Location = new System.Drawing.Point(7, 33);
            this.nudEnhanceThresh.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudEnhanceThresh.Name = "nudEnhanceThresh";
            this.nudEnhanceThresh.Size = new System.Drawing.Size(177, 20);
            this.nudEnhanceThresh.TabIndex = 120;
            this.nudEnhanceThresh.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEnhanceThresh.ValueChanged += new System.EventHandler(this.nudEnhanceThresh_ValueChanged);
            // 
            // lblEnhancementThres
            // 
            this.lblEnhancementThres.AutoSize = true;
            this.lblEnhancementThres.Location = new System.Drawing.Point(4, 16);
            this.lblEnhancementThres.Name = "lblEnhancementThres";
            this.lblEnhancementThres.Size = new System.Drawing.Size(123, 13);
            this.lblEnhancementThres.TabIndex = 119;
            this.lblEnhancementThres.Text = "Enhancement Threshold";
            // 
            // grpDeconstruction
            // 
            this.grpDeconstruction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDeconstruction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDeconstruction.Controls.Add(this.btnFuelReqRecc);
            this.grpDeconstruction.Controls.Add(this.nudStudyChance);
            this.grpDeconstruction.Controls.Add(this.lblStudyChance);
            this.grpDeconstruction.Controls.Add(this.cmbStudyEnhancement);
            this.grpDeconstruction.Controls.Add(this.lblStudy);
            this.grpDeconstruction.Controls.Add(this.lblDeconLoot);
            this.grpDeconstruction.Controls.Add(this.nudReqFuel);
            this.grpDeconstruction.Controls.Add(this.lblFuelReq);
            this.grpDeconstruction.Controls.Add(this.btnRemoveDeconTable);
            this.grpDeconstruction.Controls.Add(this.btnAddDeconTable);
            this.grpDeconstruction.Controls.Add(this.lstDeconstructionTables);
            this.grpDeconstruction.Controls.Add(this.lblDeconLootTable);
            this.grpDeconstruction.Controls.Add(this.lblDeconTableRolls);
            this.grpDeconstruction.Controls.Add(this.cmbDeconTables);
            this.grpDeconstruction.Controls.Add(this.nudDeconTableRolls);
            this.grpDeconstruction.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDeconstruction.Location = new System.Drawing.Point(434, 591);
            this.grpDeconstruction.Margin = new System.Windows.Forms.Padding(2);
            this.grpDeconstruction.Name = "grpDeconstruction";
            this.grpDeconstruction.Padding = new System.Windows.Forms.Padding(2);
            this.grpDeconstruction.Size = new System.Drawing.Size(212, 359);
            this.grpDeconstruction.TabIndex = 121;
            this.grpDeconstruction.TabStop = false;
            this.grpDeconstruction.Text = "Deconstruction";
            // 
            // btnFuelReqRecc
            // 
            this.btnFuelReqRecc.Location = new System.Drawing.Point(116, 31);
            this.btnFuelReqRecc.Name = "btnFuelReqRecc";
            this.btnFuelReqRecc.Padding = new System.Windows.Forms.Padding(5);
            this.btnFuelReqRecc.Size = new System.Drawing.Size(45, 21);
            this.btnFuelReqRecc.TabIndex = 124;
            this.btnFuelReqRecc.Text = "Recc.";
            this.btnFuelReqRecc.Click += new System.EventHandler(this.btnFuelReqRecc_Click);
            // 
            // nudStudyChance
            // 
            this.nudStudyChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStudyChance.DecimalPlaces = 2;
            this.nudStudyChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStudyChance.Location = new System.Drawing.Point(107, 323);
            this.nudStudyChance.Name = "nudStudyChance";
            this.nudStudyChance.Size = new System.Drawing.Size(100, 20);
            this.nudStudyChance.TabIndex = 123;
            this.nudStudyChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStudyChance.ValueChanged += new System.EventHandler(this.nudStudyChance_ValueChanged);
            // 
            // lblStudyChance
            // 
            this.lblStudyChance.AutoSize = true;
            this.lblStudyChance.Location = new System.Drawing.Point(10, 325);
            this.lblStudyChance.Name = "lblStudyChance";
            this.lblStudyChance.Size = new System.Drawing.Size(91, 13);
            this.lblStudyChance.TabIndex = 122;
            this.lblStudyChance.Text = "Study Chance (%)";
            // 
            // cmbStudyEnhancement
            // 
            this.cmbStudyEnhancement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbStudyEnhancement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbStudyEnhancement.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbStudyEnhancement.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbStudyEnhancement.DrawDropdownHoverOutline = false;
            this.cmbStudyEnhancement.DrawFocusRectangle = false;
            this.cmbStudyEnhancement.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStudyEnhancement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudyEnhancement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStudyEnhancement.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbStudyEnhancement.FormattingEnabled = true;
            this.cmbStudyEnhancement.Location = new System.Drawing.Point(8, 296);
            this.cmbStudyEnhancement.Name = "cmbStudyEnhancement";
            this.cmbStudyEnhancement.Size = new System.Drawing.Size(198, 21);
            this.cmbStudyEnhancement.TabIndex = 121;
            this.cmbStudyEnhancement.Text = null;
            this.cmbStudyEnhancement.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbStudyEnhancement.SelectedIndexChanged += new System.EventHandler(this.cmbStudyEnhancement_SelectedIndexChanged);
            // 
            // lblStudy
            // 
            this.lblStudy.AutoSize = true;
            this.lblStudy.Location = new System.Drawing.Point(8, 280);
            this.lblStudy.Name = "lblStudy";
            this.lblStudy.Size = new System.Drawing.Size(103, 13);
            this.lblStudy.TabIndex = 120;
            this.lblStudy.Text = "Study Enhancement";
            // 
            // lblDeconLoot
            // 
            this.lblDeconLoot.AutoSize = true;
            this.lblDeconLoot.Location = new System.Drawing.Point(58, 66);
            this.lblDeconLoot.Name = "lblDeconLoot";
            this.lblDeconLoot.Size = new System.Drawing.Size(103, 13);
            this.lblDeconLoot.TabIndex = 119;
            this.lblDeconLoot.Text = "Deconstruction Loot";
            // 
            // nudReqFuel
            // 
            this.nudReqFuel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudReqFuel.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudReqFuel.Location = new System.Drawing.Point(8, 31);
            this.nudReqFuel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudReqFuel.Name = "nudReqFuel";
            this.nudReqFuel.Size = new System.Drawing.Size(98, 20);
            this.nudReqFuel.TabIndex = 118;
            this.nudReqFuel.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudReqFuel.ValueChanged += new System.EventHandler(this.nudReqFuel_ValueChanged);
            // 
            // lblFuelReq
            // 
            this.lblFuelReq.AutoSize = true;
            this.lblFuelReq.Location = new System.Drawing.Point(6, 15);
            this.lblFuelReq.Name = "lblFuelReq";
            this.lblFuelReq.Size = new System.Drawing.Size(76, 13);
            this.lblFuelReq.TabIndex = 118;
            this.lblFuelReq.Text = "Fuel Required:";
            // 
            // btnRemoveDeconTable
            // 
            this.btnRemoveDeconTable.Location = new System.Drawing.Point(133, 155);
            this.btnRemoveDeconTable.Name = "btnRemoveDeconTable";
            this.btnRemoveDeconTable.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveDeconTable.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveDeconTable.TabIndex = 117;
            this.btnRemoveDeconTable.Text = "Remove";
            this.btnRemoveDeconTable.Click += new System.EventHandler(this.btnRemoveDeconTable_Click);
            // 
            // btnAddDeconTable
            // 
            this.btnAddDeconTable.Location = new System.Drawing.Point(9, 155);
            this.btnAddDeconTable.Name = "btnAddDeconTable";
            this.btnAddDeconTable.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddDeconTable.Size = new System.Drawing.Size(74, 23);
            this.btnAddDeconTable.TabIndex = 116;
            this.btnAddDeconTable.Text = "Add";
            this.btnAddDeconTable.Click += new System.EventHandler(this.btnAddDeconTable_Click);
            // 
            // lstDeconstructionTables
            // 
            this.lstDeconstructionTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstDeconstructionTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDeconstructionTables.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstDeconstructionTables.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstDeconstructionTables.FormattingEnabled = true;
            this.lstDeconstructionTables.Location = new System.Drawing.Point(8, 82);
            this.lstDeconstructionTables.Name = "lstDeconstructionTables";
            this.lstDeconstructionTables.Size = new System.Drawing.Size(199, 67);
            this.lstDeconstructionTables.TabIndex = 115;
            // 
            // lblDeconLootTable
            // 
            this.lblDeconLootTable.AutoSize = true;
            this.lblDeconLootTable.Location = new System.Drawing.Point(9, 185);
            this.lblDeconLootTable.Name = "lblDeconLootTable";
            this.lblDeconLootTable.Size = new System.Drawing.Size(61, 13);
            this.lblDeconLootTable.TabIndex = 28;
            this.lblDeconLootTable.Text = "Loot Table:";
            // 
            // lblDeconTableRolls
            // 
            this.lblDeconTableRolls.AutoSize = true;
            this.lblDeconTableRolls.Location = new System.Drawing.Point(6, 228);
            this.lblDeconTableRolls.Name = "lblDeconTableRolls";
            this.lblDeconTableRolls.Size = new System.Drawing.Size(33, 13);
            this.lblDeconTableRolls.TabIndex = 31;
            this.lblDeconTableRolls.Text = "Rolls:";
            // 
            // cmbDeconTables
            // 
            this.cmbDeconTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbDeconTables.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbDeconTables.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbDeconTables.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbDeconTables.DrawDropdownHoverOutline = false;
            this.cmbDeconTables.DrawFocusRectangle = false;
            this.cmbDeconTables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDeconTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeconTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbDeconTables.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbDeconTables.FormattingEnabled = true;
            this.cmbDeconTables.Location = new System.Drawing.Point(9, 201);
            this.cmbDeconTables.Name = "cmbDeconTables";
            this.cmbDeconTables.Size = new System.Drawing.Size(198, 21);
            this.cmbDeconTables.TabIndex = 29;
            this.cmbDeconTables.Text = null;
            this.cmbDeconTables.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // nudDeconTableRolls
            // 
            this.nudDeconTableRolls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDeconTableRolls.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDeconTableRolls.Location = new System.Drawing.Point(9, 244);
            this.nudDeconTableRolls.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDeconTableRolls.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDeconTableRolls.Name = "nudDeconTableRolls";
            this.nudDeconTableRolls.Size = new System.Drawing.Size(198, 20);
            this.nudDeconTableRolls.TabIndex = 55;
            this.nudDeconTableRolls.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // grpWeaponTypes
            // 
            this.grpWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponTypes.Controls.Add(this.nudWeaponCraftExp);
            this.grpWeaponTypes.Controls.Add(this.lblWeaponCraftExp);
            this.grpWeaponTypes.Controls.Add(this.nudMaxWeaponLvl);
            this.grpWeaponTypes.Controls.Add(this.lblMaxWeaponLvl);
            this.grpWeaponTypes.Controls.Add(this.btnRemoveWeaponType);
            this.grpWeaponTypes.Controls.Add(this.btnAddWeaponType);
            this.grpWeaponTypes.Controls.Add(this.lstWeaponTypes);
            this.grpWeaponTypes.Controls.Add(this.lblWeaponTypes);
            this.grpWeaponTypes.Controls.Add(this.cmbWeaponTypes);
            this.grpWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponTypes.Location = new System.Drawing.Point(13, 887);
            this.grpWeaponTypes.Margin = new System.Windows.Forms.Padding(2);
            this.grpWeaponTypes.Name = "grpWeaponTypes";
            this.grpWeaponTypes.Padding = new System.Windows.Forms.Padding(2);
            this.grpWeaponTypes.Size = new System.Drawing.Size(198, 264);
            this.grpWeaponTypes.TabIndex = 120;
            this.grpWeaponTypes.TabStop = false;
            this.grpWeaponTypes.Text = "Weapon Type Trees";
            // 
            // nudWeaponCraftExp
            // 
            this.nudWeaponCraftExp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudWeaponCraftExp.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudWeaponCraftExp.Location = new System.Drawing.Point(7, 235);
            this.nudWeaponCraftExp.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudWeaponCraftExp.Name = "nudWeaponCraftExp";
            this.nudWeaponCraftExp.Size = new System.Drawing.Size(177, 20);
            this.nudWeaponCraftExp.TabIndex = 122;
            this.nudWeaponCraftExp.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudWeaponCraftExp.ValueChanged += new System.EventHandler(this.nudWeaponCraftExp_ValueChanged);
            // 
            // lblWeaponCraftExp
            // 
            this.lblWeaponCraftExp.AutoSize = true;
            this.lblWeaponCraftExp.Location = new System.Drawing.Point(3, 219);
            this.lblWeaponCraftExp.Name = "lblWeaponCraftExp";
            this.lblWeaponCraftExp.Size = new System.Drawing.Size(134, 13);
            this.lblWeaponCraftExp.TabIndex = 121;
            this.lblWeaponCraftExp.Text = "Craft/Decon Weapon EXP";
            // 
            // nudMaxWeaponLvl
            // 
            this.nudMaxWeaponLvl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMaxWeaponLvl.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMaxWeaponLvl.Location = new System.Drawing.Point(7, 185);
            this.nudMaxWeaponLvl.Name = "nudMaxWeaponLvl";
            this.nudMaxWeaponLvl.Size = new System.Drawing.Size(177, 20);
            this.nudMaxWeaponLvl.TabIndex = 120;
            this.nudMaxWeaponLvl.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMaxWeaponLvl.ValueChanged += new System.EventHandler(this.nudMaxWeaponLvl_ValueChanged);
            // 
            // lblMaxWeaponLvl
            // 
            this.lblMaxWeaponLvl.AutoSize = true;
            this.lblMaxWeaponLvl.Location = new System.Drawing.Point(4, 169);
            this.lblMaxWeaponLvl.Name = "lblMaxWeaponLvl";
            this.lblMaxWeaponLvl.Size = new System.Drawing.Size(56, 13);
            this.lblMaxWeaponLvl.TabIndex = 119;
            this.lblMaxWeaponLvl.Text = "Max Level";
            // 
            // btnRemoveWeaponType
            // 
            this.btnRemoveWeaponType.Location = new System.Drawing.Point(111, 116);
            this.btnRemoveWeaponType.Name = "btnRemoveWeaponType";
            this.btnRemoveWeaponType.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveWeaponType.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveWeaponType.TabIndex = 118;
            this.btnRemoveWeaponType.Text = "Remove";
            this.btnRemoveWeaponType.Click += new System.EventHandler(this.btnRemoveWeaponType_Click);
            // 
            // btnAddWeaponType
            // 
            this.btnAddWeaponType.Location = new System.Drawing.Point(6, 116);
            this.btnAddWeaponType.Name = "btnAddWeaponType";
            this.btnAddWeaponType.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddWeaponType.Size = new System.Drawing.Size(74, 23);
            this.btnAddWeaponType.TabIndex = 117;
            this.btnAddWeaponType.Text = "Add";
            this.btnAddWeaponType.Click += new System.EventHandler(this.btnAddWeaponType_Click);
            // 
            // lstWeaponTypes
            // 
            this.lstWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstWeaponTypes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstWeaponTypes.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstWeaponTypes.FormattingEnabled = true;
            this.lstWeaponTypes.Location = new System.Drawing.Point(7, 43);
            this.lstWeaponTypes.Name = "lstWeaponTypes";
            this.lstWeaponTypes.Size = new System.Drawing.Size(176, 67);
            this.lstWeaponTypes.TabIndex = 116;
            this.lstWeaponTypes.SelectedIndexChanged += new System.EventHandler(this.lstWeaponTypes_SelectedIndexChanged);
            // 
            // lblWeaponTypes
            // 
            this.lblWeaponTypes.AutoSize = true;
            this.lblWeaponTypes.Location = new System.Drawing.Point(13, 22);
            this.lblWeaponTypes.Name = "lblWeaponTypes";
            this.lblWeaponTypes.Size = new System.Drawing.Size(80, 13);
            this.lblWeaponTypes.TabIndex = 28;
            this.lblWeaponTypes.Text = "Weapon Types";
            // 
            // cmbWeaponTypes
            // 
            this.cmbWeaponTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbWeaponTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbWeaponTypes.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbWeaponTypes.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbWeaponTypes.DrawDropdownHoverOutline = false;
            this.cmbWeaponTypes.DrawFocusRectangle = false;
            this.cmbWeaponTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWeaponTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeaponTypes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeaponTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbWeaponTypes.FormattingEnabled = true;
            this.cmbWeaponTypes.Location = new System.Drawing.Point(7, 145);
            this.cmbWeaponTypes.Name = "cmbWeaponTypes";
            this.cmbWeaponTypes.Size = new System.Drawing.Size(177, 21);
            this.cmbWeaponTypes.TabIndex = 29;
            this.cmbWeaponTypes.Text = null;
            this.cmbWeaponTypes.TextPadding = new System.Windows.Forms.Padding(2);
            // 
            // grpWeaponProperties
            // 
            this.grpWeaponProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponProperties.Controls.Add(this.nudCritMultiplier);
            this.grpWeaponProperties.Controls.Add(this.grpDamageTypes);
            this.grpWeaponProperties.Controls.Add(this.lblCritMultiplier);
            this.grpWeaponProperties.Controls.Add(this.grpAttackSpeed);
            this.grpWeaponProperties.Controls.Add(this.nudCritChance);
            this.grpWeaponProperties.Controls.Add(this.nudDamage);
            this.grpWeaponProperties.Controls.Add(this.cmbProjectile);
            this.grpWeaponProperties.Controls.Add(this.lblCritChance);
            this.grpWeaponProperties.Controls.Add(this.cmbAttackAnimation);
            this.grpWeaponProperties.Controls.Add(this.lblAttackAnimation);
            this.grpWeaponProperties.Controls.Add(this.chk2Hand);
            this.grpWeaponProperties.Controls.Add(this.lblToolType);
            this.grpWeaponProperties.Controls.Add(this.cmbToolType);
            this.grpWeaponProperties.Controls.Add(this.lblProjectile);
            this.grpWeaponProperties.Controls.Add(this.lblDamage);
            this.grpWeaponProperties.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponProperties.Location = new System.Drawing.Point(221, 14);
            this.grpWeaponProperties.Name = "grpWeaponProperties";
            this.grpWeaponProperties.Size = new System.Drawing.Size(207, 459);
            this.grpWeaponProperties.TabIndex = 39;
            this.grpWeaponProperties.TabStop = false;
            this.grpWeaponProperties.Text = "Weapon Properties";
            this.grpWeaponProperties.Visible = false;
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
            this.nudCritMultiplier.Location = new System.Drawing.Point(15, 203);
            this.nudCritMultiplier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCritMultiplier.Name = "nudCritMultiplier";
            this.nudCritMultiplier.Size = new System.Drawing.Size(180, 20);
            this.nudCritMultiplier.TabIndex = 58;
            this.nudCritMultiplier.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritMultiplier.ValueChanged += new System.EventHandler(this.nudCritMultiplier_ValueChanged);
            // 
            // grpDamageTypes
            // 
            this.grpDamageTypes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpDamageTypes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpDamageTypes.Controls.Add(this.chkBluntDamage);
            this.grpDamageTypes.Controls.Add(this.chkDamageSlash);
            this.grpDamageTypes.Controls.Add(this.chkDamagePierce);
            this.grpDamageTypes.Controls.Add(this.chkDamageMagic);
            this.grpDamageTypes.Controls.Add(this.label23);
            this.grpDamageTypes.Controls.Add(this.label22);
            this.grpDamageTypes.Controls.Add(this.label21);
            this.grpDamageTypes.Controls.Add(this.label20);
            this.grpDamageTypes.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpDamageTypes.Location = new System.Drawing.Point(10, 24);
            this.grpDamageTypes.Margin = new System.Windows.Forms.Padding(2);
            this.grpDamageTypes.Name = "grpDamageTypes";
            this.grpDamageTypes.Padding = new System.Windows.Forms.Padding(2);
            this.grpDamageTypes.Size = new System.Drawing.Size(188, 56);
            this.grpDamageTypes.TabIndex = 118;
            this.grpDamageTypes.TabStop = false;
            this.grpDamageTypes.Text = "Damage Types";
            // 
            // chkBluntDamage
            // 
            this.chkBluntDamage.AutoSize = true;
            this.chkBluntDamage.Location = new System.Drawing.Point(12, 31);
            this.chkBluntDamage.Name = "chkBluntDamage";
            this.chkBluntDamage.Size = new System.Drawing.Size(15, 14);
            this.chkBluntDamage.TabIndex = 135;
            this.chkBluntDamage.CheckedChanged += new System.EventHandler(this.chkBluntDamage_CheckedChanged);
            // 
            // chkDamageSlash
            // 
            this.chkDamageSlash.AutoSize = true;
            this.chkDamageSlash.Location = new System.Drawing.Point(62, 31);
            this.chkDamageSlash.Name = "chkDamageSlash";
            this.chkDamageSlash.Size = new System.Drawing.Size(15, 14);
            this.chkDamageSlash.TabIndex = 136;
            this.chkDamageSlash.CheckedChanged += new System.EventHandler(this.chkDamageSlash_CheckedChanged);
            // 
            // chkDamagePierce
            // 
            this.chkDamagePierce.AutoSize = true;
            this.chkDamagePierce.Location = new System.Drawing.Point(114, 31);
            this.chkDamagePierce.Name = "chkDamagePierce";
            this.chkDamagePierce.Size = new System.Drawing.Size(15, 14);
            this.chkDamagePierce.TabIndex = 137;
            this.chkDamagePierce.CheckedChanged += new System.EventHandler(this.chkDamagePierce_CheckedChanged);
            // 
            // chkDamageMagic
            // 
            this.chkDamageMagic.AutoSize = true;
            this.chkDamageMagic.Location = new System.Drawing.Point(161, 31);
            this.chkDamageMagic.Name = "chkDamageMagic";
            this.chkDamageMagic.Size = new System.Drawing.Size(15, 14);
            this.chkDamageMagic.TabIndex = 138;
            this.chkDamageMagic.CheckedChanged += new System.EventHandler(this.chkDamageMagic_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(150, 15);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(36, 13);
            this.label23.TabIndex = 113;
            this.label23.Text = "Magic";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(101, 15);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(45, 13);
            this.label22.TabIndex = 112;
            this.label22.Text = "Piercing";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(46, 15);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 13);
            this.label21.TabIndex = 111;
            this.label21.Text = "Slashing";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 15);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 44;
            this.label20.Text = "Blunt";
            // 
            // lblCritMultiplier
            // 
            this.lblCritMultiplier.AutoSize = true;
            this.lblCritMultiplier.Location = new System.Drawing.Point(12, 189);
            this.lblCritMultiplier.Name = "lblCritMultiplier";
            this.lblCritMultiplier.Size = new System.Drawing.Size(135, 13);
            this.lblCritMultiplier.TabIndex = 57;
            this.lblCritMultiplier.Text = "Crit Multiplier (Default 1.5x):";
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
            this.grpAttackSpeed.Location = new System.Drawing.Point(14, 353);
            this.grpAttackSpeed.Name = "grpAttackSpeed";
            this.grpAttackSpeed.Size = new System.Drawing.Size(180, 86);
            this.grpAttackSpeed.TabIndex = 56;
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
            // nudCritChance
            // 
            this.nudCritChance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudCritChance.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudCritChance.Location = new System.Drawing.Point(15, 165);
            this.nudCritChance.Name = "nudCritChance";
            this.nudCritChance.Size = new System.Drawing.Size(180, 20);
            this.nudCritChance.TabIndex = 54;
            this.nudCritChance.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudCritChance.ValueChanged += new System.EventHandler(this.nudCritChance_ValueChanged);
            // 
            // nudDamage
            // 
            this.nudDamage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudDamage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudDamage.Location = new System.Drawing.Point(15, 128);
            this.nudDamage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDamage.Name = "nudDamage";
            this.nudDamage.Size = new System.Drawing.Size(180, 20);
            this.nudDamage.TabIndex = 49;
            this.nudDamage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudDamage.ValueChanged += new System.EventHandler(this.nudDamage_ValueChanged);
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
            this.cmbProjectile.Location = new System.Drawing.Point(15, 240);
            this.cmbProjectile.Name = "cmbProjectile";
            this.cmbProjectile.Size = new System.Drawing.Size(180, 21);
            this.cmbProjectile.TabIndex = 47;
            this.cmbProjectile.Text = null;
            this.cmbProjectile.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbProjectile.SelectedIndexChanged += new System.EventHandler(this.cmbProjectile_SelectedIndexChanged);
            // 
            // lblCritChance
            // 
            this.lblCritChance.AutoSize = true;
            this.lblCritChance.Location = new System.Drawing.Point(12, 151);
            this.lblCritChance.Name = "lblCritChance";
            this.lblCritChance.Size = new System.Drawing.Size(82, 13);
            this.lblCritChance.TabIndex = 40;
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
            this.cmbAttackAnimation.Location = new System.Drawing.Point(15, 281);
            this.cmbAttackAnimation.Name = "cmbAttackAnimation";
            this.cmbAttackAnimation.Size = new System.Drawing.Size(180, 21);
            this.cmbAttackAnimation.TabIndex = 38;
            this.cmbAttackAnimation.Text = null;
            this.cmbAttackAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAttackAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbAttackAnimation_SelectedIndexChanged);
            // 
            // lblAttackAnimation
            // 
            this.lblAttackAnimation.AutoSize = true;
            this.lblAttackAnimation.Location = new System.Drawing.Point(15, 265);
            this.lblAttackAnimation.Name = "lblAttackAnimation";
            this.lblAttackAnimation.Size = new System.Drawing.Size(90, 13);
            this.lblAttackAnimation.TabIndex = 37;
            this.lblAttackAnimation.Text = "Attack Animation:";
            // 
            // chk2Hand
            // 
            this.chk2Hand.AutoSize = true;
            this.chk2Hand.Location = new System.Drawing.Point(22, 87);
            this.chk2Hand.Name = "chk2Hand";
            this.chk2Hand.Size = new System.Drawing.Size(61, 17);
            this.chk2Hand.TabIndex = 25;
            this.chk2Hand.Text = "2 Hand";
            this.chk2Hand.CheckedChanged += new System.EventHandler(this.chk2Hand_CheckedChanged);
            // 
            // lblToolType
            // 
            this.lblToolType.AutoSize = true;
            this.lblToolType.Location = new System.Drawing.Point(15, 305);
            this.lblToolType.Name = "lblToolType";
            this.lblToolType.Size = new System.Drawing.Size(58, 13);
            this.lblToolType.TabIndex = 26;
            this.lblToolType.Text = "Tool Type:";
            // 
            // cmbToolType
            // 
            this.cmbToolType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbToolType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbToolType.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbToolType.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbToolType.DrawDropdownHoverOutline = false;
            this.cmbToolType.DrawFocusRectangle = false;
            this.cmbToolType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbToolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToolType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbToolType.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbToolType.FormattingEnabled = true;
            this.cmbToolType.Location = new System.Drawing.Point(14, 319);
            this.cmbToolType.Name = "cmbToolType";
            this.cmbToolType.Size = new System.Drawing.Size(180, 21);
            this.cmbToolType.TabIndex = 27;
            this.cmbToolType.Text = null;
            this.cmbToolType.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbToolType.SelectedIndexChanged += new System.EventHandler(this.cmbToolType_SelectedIndexChanged);
            // 
            // lblProjectile
            // 
            this.lblProjectile.AutoSize = true;
            this.lblProjectile.Location = new System.Drawing.Point(15, 226);
            this.lblProjectile.Name = "lblProjectile";
            this.lblProjectile.Size = new System.Drawing.Size(53, 13);
            this.lblProjectile.TabIndex = 33;
            this.lblProjectile.Text = "Projectile:";
            // 
            // lblDamage
            // 
            this.lblDamage.AutoSize = true;
            this.lblDamage.Location = new System.Drawing.Point(12, 111);
            this.lblDamage.Name = "lblDamage";
            this.lblDamage.Size = new System.Drawing.Size(123, 13);
            this.lblDamage.TabIndex = 11;
            this.lblDamage.Text = "Resource/True Damage";
            // 
            // grpCosmetic
            // 
            this.grpCosmetic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpCosmetic.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpCosmetic.Controls.Add(this.txtCosmeticDisplayName);
            this.grpCosmetic.Controls.Add(this.lblCosmeticDisplayName);
            this.grpCosmetic.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpCosmetic.Location = new System.Drawing.Point(223, 178);
            this.grpCosmetic.Margin = new System.Windows.Forms.Padding(2);
            this.grpCosmetic.Name = "grpCosmetic";
            this.grpCosmetic.Padding = new System.Windows.Forms.Padding(2);
            this.grpCosmetic.Size = new System.Drawing.Size(198, 76);
            this.grpCosmetic.TabIndex = 119;
            this.grpCosmetic.TabStop = false;
            this.grpCosmetic.Text = "Cosmetic";
            // 
            // txtCosmeticDisplayName
            // 
            this.txtCosmeticDisplayName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.txtCosmeticDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCosmeticDisplayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtCosmeticDisplayName.Location = new System.Drawing.Point(16, 40);
            this.txtCosmeticDisplayName.Name = "txtCosmeticDisplayName";
            this.txtCosmeticDisplayName.Size = new System.Drawing.Size(165, 20);
            this.txtCosmeticDisplayName.TabIndex = 54;
            this.txtCosmeticDisplayName.TextChanged += new System.EventHandler(this.txtCosmeticDisplayName_TextChanged);
            // 
            // lblCosmeticDisplayName
            // 
            this.lblCosmeticDisplayName.AutoSize = true;
            this.lblCosmeticDisplayName.Location = new System.Drawing.Point(13, 22);
            this.lblCosmeticDisplayName.Name = "lblCosmeticDisplayName";
            this.lblCosmeticDisplayName.Size = new System.Drawing.Size(72, 13);
            this.lblCosmeticDisplayName.TabIndex = 28;
            this.lblCosmeticDisplayName.Text = "Display Name";
            // 
            // grpSpecialAttack
            // 
            this.grpSpecialAttack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpecialAttack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpecialAttack.Controls.Add(this.lblSpecialAttack);
            this.grpSpecialAttack.Controls.Add(this.lblSpecialAttackCharge);
            this.grpSpecialAttack.Controls.Add(this.cmbSpecialAttack);
            this.grpSpecialAttack.Controls.Add(this.nudSpecialAttackChargeTime);
            this.grpSpecialAttack.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpecialAttack.Location = new System.Drawing.Point(12, 754);
            this.grpSpecialAttack.Margin = new System.Windows.Forms.Padding(2);
            this.grpSpecialAttack.Name = "grpSpecialAttack";
            this.grpSpecialAttack.Padding = new System.Windows.Forms.Padding(2);
            this.grpSpecialAttack.Size = new System.Drawing.Size(198, 121);
            this.grpSpecialAttack.TabIndex = 118;
            this.grpSpecialAttack.TabStop = false;
            this.grpSpecialAttack.Text = "Special Attack";
            // 
            // lblSpecialAttack
            // 
            this.lblSpecialAttack.AutoSize = true;
            this.lblSpecialAttack.Location = new System.Drawing.Point(13, 22);
            this.lblSpecialAttack.Name = "lblSpecialAttack";
            this.lblSpecialAttack.Size = new System.Drawing.Size(76, 13);
            this.lblSpecialAttack.TabIndex = 28;
            this.lblSpecialAttack.Text = "Special Attack";
            // 
            // lblSpecialAttackCharge
            // 
            this.lblSpecialAttackCharge.AutoSize = true;
            this.lblSpecialAttackCharge.Location = new System.Drawing.Point(11, 62);
            this.lblSpecialAttackCharge.Name = "lblSpecialAttackCharge";
            this.lblSpecialAttackCharge.Size = new System.Drawing.Size(89, 13);
            this.lblSpecialAttackCharge.TabIndex = 31;
            this.lblSpecialAttackCharge.Text = "Charge Time (ms)";
            // 
            // cmbSpecialAttack
            // 
            this.cmbSpecialAttack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbSpecialAttack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbSpecialAttack.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbSpecialAttack.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbSpecialAttack.DrawDropdownHoverOutline = false;
            this.cmbSpecialAttack.DrawFocusRectangle = false;
            this.cmbSpecialAttack.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSpecialAttack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpecialAttack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSpecialAttack.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbSpecialAttack.FormattingEnabled = true;
            this.cmbSpecialAttack.Location = new System.Drawing.Point(11, 38);
            this.cmbSpecialAttack.Name = "cmbSpecialAttack";
            this.cmbSpecialAttack.Size = new System.Drawing.Size(177, 21);
            this.cmbSpecialAttack.TabIndex = 29;
            this.cmbSpecialAttack.Text = null;
            this.cmbSpecialAttack.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbSpecialAttack.SelectedIndexChanged += new System.EventHandler(this.darkComboBox1_SelectedIndexChanged);
            // 
            // nudSpecialAttackChargeTime
            // 
            this.nudSpecialAttackChargeTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpecialAttackChargeTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpecialAttackChargeTime.Location = new System.Drawing.Point(11, 78);
            this.nudSpecialAttackChargeTime.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSpecialAttackChargeTime.Name = "nudSpecialAttackChargeTime";
            this.nudSpecialAttackChargeTime.Size = new System.Drawing.Size(177, 20);
            this.nudSpecialAttackChargeTime.TabIndex = 55;
            this.nudSpecialAttackChargeTime.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSpecialAttackChargeTime.ValueChanged += new System.EventHandler(this.nudSpecialAttackChargeTime_ValueChanged);
            // 
            // grpBonusEffects
            // 
            this.grpBonusEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBonusEffects.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBonusEffects.Controls.Add(this.btnRemoveBonus);
            this.grpBonusEffects.Controls.Add(this.btnAddBonus);
            this.grpBonusEffects.Controls.Add(this.lstBonusEffects);
            this.grpBonusEffects.Controls.Add(this.lblBonusEffect);
            this.grpBonusEffects.Controls.Add(this.lblEffectPercent);
            this.grpBonusEffects.Controls.Add(this.cmbEquipmentBonus);
            this.grpBonusEffects.Controls.Add(this.nudEffectPercent);
            this.grpBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBonusEffects.Location = new System.Drawing.Point(224, 753);
            this.grpBonusEffects.Margin = new System.Windows.Forms.Padding(2);
            this.grpBonusEffects.Name = "grpBonusEffects";
            this.grpBonusEffects.Padding = new System.Windows.Forms.Padding(2);
            this.grpBonusEffects.Size = new System.Drawing.Size(203, 212);
            this.grpBonusEffects.TabIndex = 110;
            this.grpBonusEffects.TabStop = false;
            this.grpBonusEffects.Text = "Bonus Effects";
            // 
            // btnRemoveBonus
            // 
            this.btnRemoveBonus.Location = new System.Drawing.Point(110, 88);
            this.btnRemoveBonus.Name = "btnRemoveBonus";
            this.btnRemoveBonus.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemoveBonus.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveBonus.TabIndex = 117;
            this.btnRemoveBonus.Text = "Remove";
            this.btnRemoveBonus.Click += new System.EventHandler(this.btnRemoveBonus_Click);
            // 
            // btnAddBonus
            // 
            this.btnAddBonus.Location = new System.Drawing.Point(9, 88);
            this.btnAddBonus.Name = "btnAddBonus";
            this.btnAddBonus.Padding = new System.Windows.Forms.Padding(5);
            this.btnAddBonus.Size = new System.Drawing.Size(74, 23);
            this.btnAddBonus.TabIndex = 116;
            this.btnAddBonus.Text = "Add";
            this.btnAddBonus.Click += new System.EventHandler(this.btnAddBonus_Click);
            // 
            // lstBonusEffects
            // 
            this.lstBonusEffects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBonusEffects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstBonusEffects.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstBonusEffects.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstBonusEffects.FormattingEnabled = true;
            this.lstBonusEffects.Location = new System.Drawing.Point(8, 15);
            this.lstBonusEffects.Name = "lstBonusEffects";
            this.lstBonusEffects.Size = new System.Drawing.Size(176, 67);
            this.lstBonusEffects.TabIndex = 115;
            // 
            // lblBonusEffect
            // 
            this.lblBonusEffect.AutoSize = true;
            this.lblBonusEffect.Location = new System.Drawing.Point(9, 118);
            this.lblBonusEffect.Name = "lblBonusEffect";
            this.lblBonusEffect.Size = new System.Drawing.Size(71, 13);
            this.lblBonusEffect.TabIndex = 28;
            this.lblBonusEffect.Text = "Bonus Effect:";
            // 
            // lblEffectPercent
            // 
            this.lblEffectPercent.AutoSize = true;
            this.lblEffectPercent.Location = new System.Drawing.Point(6, 161);
            this.lblEffectPercent.Name = "lblEffectPercent";
            this.lblEffectPercent.Size = new System.Drawing.Size(94, 13);
            this.lblEffectPercent.TabIndex = 31;
            this.lblEffectPercent.Text = "Effect Amount (%):";
            // 
            // cmbEquipmentBonus
            // 
            this.cmbEquipmentBonus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEquipmentBonus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEquipmentBonus.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEquipmentBonus.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEquipmentBonus.DrawDropdownHoverOutline = false;
            this.cmbEquipmentBonus.DrawFocusRectangle = false;
            this.cmbEquipmentBonus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEquipmentBonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipmentBonus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEquipmentBonus.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEquipmentBonus.FormattingEnabled = true;
            this.cmbEquipmentBonus.Location = new System.Drawing.Point(9, 134);
            this.cmbEquipmentBonus.Name = "cmbEquipmentBonus";
            this.cmbEquipmentBonus.Size = new System.Drawing.Size(177, 21);
            this.cmbEquipmentBonus.TabIndex = 29;
            this.cmbEquipmentBonus.Text = null;
            this.cmbEquipmentBonus.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEquipmentBonus.SelectedIndexChanged += new System.EventHandler(this.cmbEquipmentBonus_SelectedIndexChanged);
            // 
            // nudEffectPercent
            // 
            this.nudEffectPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEffectPercent.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEffectPercent.Location = new System.Drawing.Point(9, 177);
            this.nudEffectPercent.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudEffectPercent.Name = "nudEffectPercent";
            this.nudEffectPercent.Size = new System.Drawing.Size(177, 20);
            this.nudEffectPercent.TabIndex = 55;
            this.nudEffectPercent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEffectPercent.ValueChanged += new System.EventHandler(this.nudEffectPercent_ValueChanged);
            // 
            // grpAdditionalWeaponProps
            // 
            this.grpAdditionalWeaponProps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAdditionalWeaponProps.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAdditionalWeaponProps.Controls.Add(this.lblAmmoOverride);
            this.grpAdditionalWeaponProps.Controls.Add(this.cmbAmmoOverride);
            this.grpAdditionalWeaponProps.Controls.Add(this.chkIsFocus);
            this.grpAdditionalWeaponProps.Controls.Add(this.lblBackBoost);
            this.grpAdditionalWeaponProps.Controls.Add(this.nudBackBoost);
            this.grpAdditionalWeaponProps.Controls.Add(this.nudStrafeBoost);
            this.grpAdditionalWeaponProps.Controls.Add(this.lblStrafeModifier);
            this.grpAdditionalWeaponProps.Controls.Add(this.lblBackstabMultiplier);
            this.grpAdditionalWeaponProps.Controls.Add(this.chkBackstab);
            this.grpAdditionalWeaponProps.Controls.Add(this.nudBackstabMultiplier);
            this.grpAdditionalWeaponProps.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAdditionalWeaponProps.Location = new System.Drawing.Point(18, 289);
            this.grpAdditionalWeaponProps.Margin = new System.Windows.Forms.Padding(2);
            this.grpAdditionalWeaponProps.Name = "grpAdditionalWeaponProps";
            this.grpAdditionalWeaponProps.Padding = new System.Windows.Forms.Padding(2);
            this.grpAdditionalWeaponProps.Size = new System.Drawing.Size(199, 233);
            this.grpAdditionalWeaponProps.TabIndex = 60;
            this.grpAdditionalWeaponProps.TabStop = false;
            this.grpAdditionalWeaponProps.Text = "Additional Weap. Properties";
            // 
            // lblAmmoOverride
            // 
            this.lblAmmoOverride.AutoSize = true;
            this.lblAmmoOverride.Location = new System.Drawing.Point(5, 175);
            this.lblAmmoOverride.Name = "lblAmmoOverride";
            this.lblAmmoOverride.Size = new System.Drawing.Size(79, 13);
            this.lblAmmoOverride.TabIndex = 111;
            this.lblAmmoOverride.Text = "Ammo Override";
            // 
            // cmbAmmoOverride
            // 
            this.cmbAmmoOverride.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbAmmoOverride.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbAmmoOverride.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbAmmoOverride.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbAmmoOverride.DrawDropdownHoverOutline = false;
            this.cmbAmmoOverride.DrawFocusRectangle = false;
            this.cmbAmmoOverride.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAmmoOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAmmoOverride.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAmmoOverride.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbAmmoOverride.FormattingEnabled = true;
            this.cmbAmmoOverride.Items.AddRange(new object[] {
            "None"});
            this.cmbAmmoOverride.Location = new System.Drawing.Point(7, 192);
            this.cmbAmmoOverride.Name = "cmbAmmoOverride";
            this.cmbAmmoOverride.Size = new System.Drawing.Size(179, 21);
            this.cmbAmmoOverride.TabIndex = 110;
            this.cmbAmmoOverride.Text = "None";
            this.cmbAmmoOverride.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbAmmoOverride.SelectedIndexChanged += new System.EventHandler(this.cmbAmmoOverride_SelectedIndexChanged);
            // 
            // chkIsFocus
            // 
            this.chkIsFocus.AutoSize = true;
            this.chkIsFocus.Location = new System.Drawing.Point(8, 132);
            this.chkIsFocus.Name = "chkIsFocus";
            this.chkIsFocus.Size = new System.Drawing.Size(158, 17);
            this.chkIsFocus.TabIndex = 59;
            this.chkIsFocus.Text = "Replace Cast Components?";
            this.chkIsFocus.CheckedChanged += new System.EventHandler(this.chkIsFocus_CheckedChanged);
            // 
            // lblBackBoost
            // 
            this.lblBackBoost.AutoSize = true;
            this.lblBackBoost.Location = new System.Drawing.Point(4, 108);
            this.lblBackBoost.Name = "lblBackBoost";
            this.lblBackBoost.Size = new System.Drawing.Size(102, 13);
            this.lblBackBoost.TabIndex = 109;
            this.lblBackBoost.Text = "Backstep Bonus (%)";
            // 
            // nudBackBoost
            // 
            this.nudBackBoost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBackBoost.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBackBoost.Location = new System.Drawing.Point(112, 106);
            this.nudBackBoost.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBackBoost.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudBackBoost.Name = "nudBackBoost";
            this.nudBackBoost.Size = new System.Drawing.Size(76, 20);
            this.nudBackBoost.TabIndex = 108;
            this.nudBackBoost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudBackBoost.ValueChanged += new System.EventHandler(this.nudBackBoost_ValueChanged);
            // 
            // nudStrafeBoost
            // 
            this.nudStrafeBoost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudStrafeBoost.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudStrafeBoost.Location = new System.Drawing.Point(112, 80);
            this.nudStrafeBoost.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudStrafeBoost.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudStrafeBoost.Name = "nudStrafeBoost";
            this.nudStrafeBoost.Size = new System.Drawing.Size(76, 20);
            this.nudStrafeBoost.TabIndex = 107;
            this.nudStrafeBoost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStrafeBoost.ValueChanged += new System.EventHandler(this.nudStrafeBoost_ValueChanged);
            // 
            // lblStrafeModifier
            // 
            this.lblStrafeModifier.AutoSize = true;
            this.lblStrafeModifier.Location = new System.Drawing.Point(4, 82);
            this.lblStrafeModifier.Name = "lblStrafeModifier";
            this.lblStrafeModifier.Size = new System.Drawing.Size(85, 13);
            this.lblStrafeModifier.TabIndex = 106;
            this.lblStrafeModifier.Text = "Strafe Bonus (%)";
            // 
            // lblBackstabMultiplier
            // 
            this.lblBackstabMultiplier.AutoSize = true;
            this.lblBackstabMultiplier.Location = new System.Drawing.Point(4, 56);
            this.lblBackstabMultiplier.Name = "lblBackstabMultiplier";
            this.lblBackstabMultiplier.Size = new System.Drawing.Size(99, 13);
            this.lblBackstabMultiplier.TabIndex = 62;
            this.lblBackstabMultiplier.Text = "Backstab Multiplier:";
            // 
            // chkBackstab
            // 
            this.chkBackstab.AutoSize = true;
            this.chkBackstab.Location = new System.Drawing.Point(10, 26);
            this.chkBackstab.Name = "chkBackstab";
            this.chkBackstab.Size = new System.Drawing.Size(99, 17);
            this.chkBackstab.TabIndex = 104;
            this.chkBackstab.Text = "Can Backstab?";
            this.chkBackstab.CheckedChanged += new System.EventHandler(this.chkBackstab_CheckedChanged);
            // 
            // nudBackstabMultiplier
            // 
            this.nudBackstabMultiplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBackstabMultiplier.DecimalPlaces = 2;
            this.nudBackstabMultiplier.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBackstabMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudBackstabMultiplier.Location = new System.Drawing.Point(112, 54);
            this.nudBackstabMultiplier.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBackstabMultiplier.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.nudBackstabMultiplier.Name = "nudBackstabMultiplier";
            this.nudBackstabMultiplier.Size = new System.Drawing.Size(76, 20);
            this.nudBackstabMultiplier.TabIndex = 105;
            this.nudBackstabMultiplier.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.nudBackstabMultiplier.ValueChanged += new System.EventHandler(this.nudBackstabMultiplier_ValueChanged);
            // 
            // grpHelmetPaperdollProps
            // 
            this.grpHelmetPaperdollProps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpHelmetPaperdollProps.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpHelmetPaperdollProps.Controls.Add(this.chkShortHair);
            this.grpHelmetPaperdollProps.Controls.Add(this.chkHelmHideExtra);
            this.grpHelmetPaperdollProps.Controls.Add(this.chkHelmHideBeard);
            this.grpHelmetPaperdollProps.Controls.Add(this.chkHelmHideHair);
            this.grpHelmetPaperdollProps.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpHelmetPaperdollProps.Location = new System.Drawing.Point(221, 8);
            this.grpHelmetPaperdollProps.Name = "grpHelmetPaperdollProps";
            this.grpHelmetPaperdollProps.Size = new System.Drawing.Size(207, 126);
            this.grpHelmetPaperdollProps.TabIndex = 61;
            this.grpHelmetPaperdollProps.TabStop = false;
            this.grpHelmetPaperdollProps.Text = "Paperdoll Properties";
            this.grpHelmetPaperdollProps.Visible = false;
            // 
            // chkShortHair
            // 
            this.chkShortHair.AutoSize = true;
            this.chkShortHair.Location = new System.Drawing.Point(41, 96);
            this.chkShortHair.Name = "chkShortHair";
            this.chkShortHair.Size = new System.Drawing.Size(79, 17);
            this.chkShortHair.TabIndex = 104;
            this.chkShortHair.Text = "Short Hair?";
            this.chkShortHair.CheckedChanged += new System.EventHandler(this.chkShortHair_CheckedChanged);
            // 
            // chkHelmHideExtra
            // 
            this.chkHelmHideExtra.AutoSize = true;
            this.chkHelmHideExtra.Location = new System.Drawing.Point(41, 73);
            this.chkHelmHideExtra.Name = "chkHelmHideExtra";
            this.chkHelmHideExtra.Size = new System.Drawing.Size(81, 17);
            this.chkHelmHideExtra.TabIndex = 103;
            this.chkHelmHideExtra.Text = "Hide Extra?";
            this.chkHelmHideExtra.CheckedChanged += new System.EventHandler(this.chkHelmHideExtra_CheckedChanged);
            // 
            // chkHelmHideBeard
            // 
            this.chkHelmHideBeard.AutoSize = true;
            this.chkHelmHideBeard.Location = new System.Drawing.Point(41, 49);
            this.chkHelmHideBeard.Name = "chkHelmHideBeard";
            this.chkHelmHideBeard.Size = new System.Drawing.Size(85, 17);
            this.chkHelmHideBeard.TabIndex = 102;
            this.chkHelmHideBeard.Text = "Hide Beard?";
            this.chkHelmHideBeard.CheckedChanged += new System.EventHandler(this.chkHelmHideBeard_CheckedChanged);
            // 
            // chkHelmHideHair
            // 
            this.chkHelmHideHair.AutoSize = true;
            this.chkHelmHideHair.Location = new System.Drawing.Point(41, 25);
            this.chkHelmHideHair.Name = "chkHelmHideHair";
            this.chkHelmHideHair.Size = new System.Drawing.Size(76, 17);
            this.chkHelmHideHair.TabIndex = 101;
            this.chkHelmHideHair.Text = "Hide Hair?";
            this.chkHelmHideHair.CheckedChanged += new System.EventHandler(this.chkHelmHideHair_CheckedChanged);
            // 
            // grpPrayerProperties
            // 
            this.grpPrayerProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpPrayerProperties.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpPrayerProperties.Controls.Add(this.lblComboExpBoost);
            this.grpPrayerProperties.Controls.Add(this.nudComboExpBoost);
            this.grpPrayerProperties.Controls.Add(this.lblComboInterval);
            this.grpPrayerProperties.Controls.Add(this.nudComboInterval);
            this.grpPrayerProperties.Controls.Add(this.lblComboSpell);
            this.grpPrayerProperties.Controls.Add(this.cmbComboSpell);
            this.grpPrayerProperties.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPrayerProperties.Location = new System.Drawing.Point(221, 14);
            this.grpPrayerProperties.Name = "grpPrayerProperties";
            this.grpPrayerProperties.Size = new System.Drawing.Size(207, 161);
            this.grpPrayerProperties.TabIndex = 43;
            this.grpPrayerProperties.TabStop = false;
            this.grpPrayerProperties.Text = "Prayer Properties";
            this.grpPrayerProperties.Visible = false;
            // 
            // lblComboExpBoost
            // 
            this.lblComboExpBoost.AutoSize = true;
            this.lblComboExpBoost.Location = new System.Drawing.Point(12, 110);
            this.lblComboExpBoost.Name = "lblComboExpBoost";
            this.lblComboExpBoost.Size = new System.Drawing.Size(107, 13);
            this.lblComboExpBoost.TabIndex = 59;
            this.lblComboExpBoost.Text = "Additional Exp Boost:";
            // 
            // nudComboExpBoost
            // 
            this.nudComboExpBoost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudComboExpBoost.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudComboExpBoost.Location = new System.Drawing.Point(10, 126);
            this.nudComboExpBoost.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudComboExpBoost.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudComboExpBoost.Name = "nudComboExpBoost";
            this.nudComboExpBoost.Size = new System.Drawing.Size(185, 20);
            this.nudComboExpBoost.TabIndex = 60;
            this.nudComboExpBoost.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudComboExpBoost.ValueChanged += new System.EventHandler(this.nudComboExpBoost_ValueChanged);
            // 
            // lblComboInterval
            // 
            this.lblComboInterval.AutoSize = true;
            this.lblComboInterval.Location = new System.Drawing.Point(12, 63);
            this.lblComboInterval.Name = "lblComboInterval";
            this.lblComboInterval.Size = new System.Drawing.Size(81, 13);
            this.lblComboInterval.TabIndex = 59;
            this.lblComboInterval.Text = "Combo Interval:";
            // 
            // nudComboInterval
            // 
            this.nudComboInterval.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudComboInterval.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudComboInterval.Location = new System.Drawing.Point(11, 81);
            this.nudComboInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudComboInterval.Name = "nudComboInterval";
            this.nudComboInterval.Size = new System.Drawing.Size(185, 20);
            this.nudComboInterval.TabIndex = 59;
            this.nudComboInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudComboInterval.ValueChanged += new System.EventHandler(this.nudComboInterval_ValueChanged);
            // 
            // lblComboSpell
            // 
            this.lblComboSpell.AutoSize = true;
            this.lblComboSpell.Location = new System.Drawing.Point(12, 18);
            this.lblComboSpell.Name = "lblComboSpell";
            this.lblComboSpell.Size = new System.Drawing.Size(69, 13);
            this.lblComboSpell.TabIndex = 59;
            this.lblComboSpell.Text = "Combo Spell:";
            // 
            // cmbComboSpell
            // 
            this.cmbComboSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbComboSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbComboSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbComboSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbComboSpell.DrawDropdownHoverOutline = false;
            this.cmbComboSpell.DrawFocusRectangle = false;
            this.cmbComboSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbComboSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComboSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbComboSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbComboSpell.FormattingEnabled = true;
            this.cmbComboSpell.Location = new System.Drawing.Point(10, 35);
            this.cmbComboSpell.Name = "cmbComboSpell";
            this.cmbComboSpell.Size = new System.Drawing.Size(185, 21);
            this.cmbComboSpell.TabIndex = 17;
            this.cmbComboSpell.Text = null;
            this.cmbComboSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbComboSpell.SelectedIndexChanged += new System.EventHandler(this.cmbComboSpell_SelectedIndexChanged);
            // 
            // grpRegen
            // 
            this.grpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpRegen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpRegen.Controls.Add(this.nudMpRegen);
            this.grpRegen.Controls.Add(this.nudHPRegen);
            this.grpRegen.Controls.Add(this.lblHpRegen);
            this.grpRegen.Controls.Add(this.lblManaRegen);
            this.grpRegen.Controls.Add(this.lblRegenHint);
            this.grpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRegen.Location = new System.Drawing.Point(15, 148);
            this.grpRegen.Margin = new System.Windows.Forms.Padding(2);
            this.grpRegen.Name = "grpRegen";
            this.grpRegen.Padding = new System.Windows.Forms.Padding(2);
            this.grpRegen.Size = new System.Drawing.Size(202, 90);
            this.grpRegen.TabIndex = 59;
            this.grpRegen.TabStop = false;
            this.grpRegen.Text = "Regen";
            // 
            // nudMpRegen
            // 
            this.nudMpRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMpRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMpRegen.Location = new System.Drawing.Point(106, 31);
            this.nudMpRegen.Name = "nudMpRegen";
            this.nudMpRegen.Size = new System.Drawing.Size(87, 20);
            this.nudMpRegen.TabIndex = 31;
            this.nudMpRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMpRegen.ValueChanged += new System.EventHandler(this.nudMpRegen_ValueChanged);
            // 
            // nudHPRegen
            // 
            this.nudHPRegen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHPRegen.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHPRegen.Location = new System.Drawing.Point(8, 31);
            this.nudHPRegen.Name = "nudHPRegen";
            this.nudHPRegen.Size = new System.Drawing.Size(87, 20);
            this.nudHPRegen.TabIndex = 30;
            this.nudHPRegen.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHPRegen.ValueChanged += new System.EventHandler(this.nudHPRegen_ValueChanged);
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
            this.lblManaRegen.Location = new System.Drawing.Point(103, 17);
            this.lblManaRegen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManaRegen.Name = "lblManaRegen";
            this.lblManaRegen.Size = new System.Drawing.Size(54, 13);
            this.lblManaRegen.TabIndex = 27;
            this.lblManaRegen.Text = "Mana: (%)";
            // 
            // lblRegenHint
            // 
            this.lblRegenHint.Location = new System.Drawing.Point(7, 54);
            this.lblRegenHint.Name = "lblRegenHint";
            this.lblRegenHint.Size = new System.Drawing.Size(191, 31);
            this.lblRegenHint.TabIndex = 0;
            this.lblRegenHint.Text = "% of HP/Mana to restore per tick.\r\nTick timer saved in server config.json.";
            // 
            // grpVitalBonuses
            // 
            this.grpVitalBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpVitalBonuses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpVitalBonuses.Controls.Add(this.lblPercentage2);
            this.grpVitalBonuses.Controls.Add(this.lblPercentage1);
            this.grpVitalBonuses.Controls.Add(this.nudMPPercentage);
            this.grpVitalBonuses.Controls.Add(this.nudHPPercentage);
            this.grpVitalBonuses.Controls.Add(this.lblPlus2);
            this.grpVitalBonuses.Controls.Add(this.lblPlus1);
            this.grpVitalBonuses.Controls.Add(this.nudManaBonus);
            this.grpVitalBonuses.Controls.Add(this.nudHealthBonus);
            this.grpVitalBonuses.Controls.Add(this.lblManaBonus);
            this.grpVitalBonuses.Controls.Add(this.lblHealthBonus);
            this.grpVitalBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpVitalBonuses.Location = new System.Drawing.Point(15, 42);
            this.grpVitalBonuses.Name = "grpVitalBonuses";
            this.grpVitalBonuses.Size = new System.Drawing.Size(200, 102);
            this.grpVitalBonuses.TabIndex = 58;
            this.grpVitalBonuses.TabStop = false;
            this.grpVitalBonuses.Text = "Vital Bonuses";
            // 
            // lblPercentage2
            // 
            this.lblPercentage2.AutoSize = true;
            this.lblPercentage2.Location = new System.Drawing.Point(179, 75);
            this.lblPercentage2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage2.Name = "lblPercentage2";
            this.lblPercentage2.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage2.TabIndex = 70;
            this.lblPercentage2.Text = "%";
            // 
            // lblPercentage1
            // 
            this.lblPercentage1.AutoSize = true;
            this.lblPercentage1.Location = new System.Drawing.Point(179, 39);
            this.lblPercentage1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage1.Name = "lblPercentage1";
            this.lblPercentage1.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage1.TabIndex = 69;
            this.lblPercentage1.Text = "%";
            // 
            // nudMPPercentage
            // 
            this.nudMPPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudMPPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudMPPercentage.Location = new System.Drawing.Point(132, 74);
            this.nudMPPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMPPercentage.Name = "nudMPPercentage";
            this.nudMPPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudMPPercentage.TabIndex = 68;
            this.nudMPPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudMPPercentage.ValueChanged += new System.EventHandler(this.nudMPPercentage_ValueChanged);
            // 
            // nudHPPercentage
            // 
            this.nudHPPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHPPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHPPercentage.Location = new System.Drawing.Point(132, 37);
            this.nudHPPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHPPercentage.Name = "nudHPPercentage";
            this.nudHPPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudHPPercentage.TabIndex = 67;
            this.nudHPPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHPPercentage.ValueChanged += new System.EventHandler(this.nudHPPercentage_ValueChanged);
            // 
            // lblPlus2
            // 
            this.lblPlus2.AutoSize = true;
            this.lblPlus2.Location = new System.Drawing.Point(114, 75);
            this.lblPlus2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus2.Name = "lblPlus2";
            this.lblPlus2.Size = new System.Drawing.Size(13, 13);
            this.lblPlus2.TabIndex = 66;
            this.lblPlus2.Text = "+";
            // 
            // lblPlus1
            // 
            this.lblPlus1.AutoSize = true;
            this.lblPlus1.Location = new System.Drawing.Point(114, 39);
            this.lblPlus1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus1.Name = "lblPlus1";
            this.lblPlus1.Size = new System.Drawing.Size(13, 13);
            this.lblPlus1.TabIndex = 65;
            this.lblPlus1.Text = "+";
            // 
            // nudManaBonus
            // 
            this.nudManaBonus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudManaBonus.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudManaBonus.Location = new System.Drawing.Point(12, 76);
            this.nudManaBonus.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudManaBonus.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudManaBonus.Name = "nudManaBonus";
            this.nudManaBonus.Size = new System.Drawing.Size(95, 20);
            this.nudManaBonus.TabIndex = 49;
            this.nudManaBonus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudManaBonus.ValueChanged += new System.EventHandler(this.nudManaBonus_ValueChanged);
            // 
            // nudHealthBonus
            // 
            this.nudHealthBonus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudHealthBonus.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudHealthBonus.Location = new System.Drawing.Point(12, 37);
            this.nudHealthBonus.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudHealthBonus.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudHealthBonus.Name = "nudHealthBonus";
            this.nudHealthBonus.Size = new System.Drawing.Size(95, 20);
            this.nudHealthBonus.TabIndex = 48;
            this.nudHealthBonus.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudHealthBonus.ValueChanged += new System.EventHandler(this.nudHealthBonus_ValueChanged);
            // 
            // lblManaBonus
            // 
            this.lblManaBonus.AutoSize = true;
            this.lblManaBonus.Location = new System.Drawing.Point(9, 63);
            this.lblManaBonus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManaBonus.Name = "lblManaBonus";
            this.lblManaBonus.Size = new System.Drawing.Size(37, 13);
            this.lblManaBonus.TabIndex = 44;
            this.lblManaBonus.Text = "Mana:";
            // 
            // lblHealthBonus
            // 
            this.lblHealthBonus.AutoSize = true;
            this.lblHealthBonus.Location = new System.Drawing.Point(8, 21);
            this.lblHealthBonus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHealthBonus.Name = "lblHealthBonus";
            this.lblHealthBonus.Size = new System.Drawing.Size(41, 13);
            this.lblHealthBonus.TabIndex = 43;
            this.lblHealthBonus.Text = "Health:";
            // 
            // cmbEquipmentAnimation
            // 
            this.cmbEquipmentAnimation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEquipmentAnimation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEquipmentAnimation.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEquipmentAnimation.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEquipmentAnimation.DrawDropdownHoverOutline = false;
            this.cmbEquipmentAnimation.DrawFocusRectangle = false;
            this.cmbEquipmentAnimation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEquipmentAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipmentAnimation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEquipmentAnimation.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEquipmentAnimation.FormattingEnabled = true;
            this.cmbEquipmentAnimation.Items.AddRange(new object[] {
            "None"});
            this.cmbEquipmentAnimation.Location = new System.Drawing.Point(13, 258);
            this.cmbEquipmentAnimation.Name = "cmbEquipmentAnimation";
            this.cmbEquipmentAnimation.Size = new System.Drawing.Size(207, 21);
            this.cmbEquipmentAnimation.TabIndex = 57;
            this.cmbEquipmentAnimation.Text = "None";
            this.cmbEquipmentAnimation.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEquipmentAnimation.SelectedIndexChanged += new System.EventHandler(this.cmbEquipmentAnimation_SelectedIndexChanged);
            // 
            // lblEquipmentAnimation
            // 
            this.lblEquipmentAnimation.AutoSize = true;
            this.lblEquipmentAnimation.Location = new System.Drawing.Point(16, 240);
            this.lblEquipmentAnimation.Name = "lblEquipmentAnimation";
            this.lblEquipmentAnimation.Size = new System.Drawing.Size(109, 13);
            this.lblEquipmentAnimation.TabIndex = 56;
            this.lblEquipmentAnimation.Text = "Equipment Animation:";
            // 
            // grpStatBonuses
            // 
            this.grpStatBonuses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpStatBonuses.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpStatBonuses.Controls.Add(this.lblSkillPoints);
            this.grpStatBonuses.Controls.Add(this.nudSkillPoints);
            this.grpStatBonuses.Controls.Add(this.grpWeaponBalance);
            this.grpStatBonuses.Controls.Add(this.grpArmorBalanceHelper);
            this.grpStatBonuses.Controls.Add(this.chkLockEvasion);
            this.grpStatBonuses.Controls.Add(this.label16);
            this.grpStatBonuses.Controls.Add(this.nudEvasionPercent);
            this.grpStatBonuses.Controls.Add(this.nudEvasion);
            this.grpStatBonuses.Controls.Add(this.label18);
            this.grpStatBonuses.Controls.Add(this.chkLockAccuracy);
            this.grpStatBonuses.Controls.Add(this.label13);
            this.grpStatBonuses.Controls.Add(this.nudAccuracyPercent);
            this.grpStatBonuses.Controls.Add(this.nudAccuracy);
            this.grpStatBonuses.Controls.Add(this.label15);
            this.grpStatBonuses.Controls.Add(this.chkLockPierceResist);
            this.grpStatBonuses.Controls.Add(this.chkLockSlashResist);
            this.grpStatBonuses.Controls.Add(this.chkLockPierce);
            this.grpStatBonuses.Controls.Add(this.chkLockSlash);
            this.grpStatBonuses.Controls.Add(this.label7);
            this.grpStatBonuses.Controls.Add(this.label1);
            this.grpStatBonuses.Controls.Add(this.label8);
            this.grpStatBonuses.Controls.Add(this.label2);
            this.grpStatBonuses.Controls.Add(this.nudPierceResistPercentage);
            this.grpStatBonuses.Controls.Add(this.nudSlashResistPercentage);
            this.grpStatBonuses.Controls.Add(this.nudPiercePercentage);
            this.grpStatBonuses.Controls.Add(this.nudSlashPercentage);
            this.grpStatBonuses.Controls.Add(this.nudPierceResist);
            this.grpStatBonuses.Controls.Add(this.nudSlashResist);
            this.grpStatBonuses.Controls.Add(this.nudPierce);
            this.grpStatBonuses.Controls.Add(this.label11);
            this.grpStatBonuses.Controls.Add(this.nudSlash);
            this.grpStatBonuses.Controls.Add(this.label12);
            this.grpStatBonuses.Controls.Add(this.label5);
            this.grpStatBonuses.Controls.Add(this.label6);
            this.grpStatBonuses.Controls.Add(this.chkLockSpeed);
            this.grpStatBonuses.Controls.Add(this.chkLockMagicResist);
            this.grpStatBonuses.Controls.Add(this.chkLockArmor);
            this.grpStatBonuses.Controls.Add(this.chkLockMagic);
            this.grpStatBonuses.Controls.Add(this.chkLockStrength);
            this.grpStatBonuses.Controls.Add(this.lblPercentage5);
            this.grpStatBonuses.Controls.Add(this.lblPercentage4);
            this.grpStatBonuses.Controls.Add(this.lblPercentage8);
            this.grpStatBonuses.Controls.Add(this.lblPercentage7);
            this.grpStatBonuses.Controls.Add(this.lblPercentage6);
            this.grpStatBonuses.Controls.Add(this.nudSpdPercentage);
            this.grpStatBonuses.Controls.Add(this.nudMRPercentage);
            this.grpStatBonuses.Controls.Add(this.nudDefPercentage);
            this.grpStatBonuses.Controls.Add(this.nudMagPercentage);
            this.grpStatBonuses.Controls.Add(this.nudStrPercentage);
            this.grpStatBonuses.Controls.Add(this.nudRange);
            this.grpStatBonuses.Controls.Add(this.nudSpd);
            this.grpStatBonuses.Controls.Add(this.nudMR);
            this.grpStatBonuses.Controls.Add(this.nudDef);
            this.grpStatBonuses.Controls.Add(this.nudMag);
            this.grpStatBonuses.Controls.Add(this.nudStr);
            this.grpStatBonuses.Controls.Add(this.lblSpd);
            this.grpStatBonuses.Controls.Add(this.lblMR);
            this.grpStatBonuses.Controls.Add(this.lblDef);
            this.grpStatBonuses.Controls.Add(this.lblMag);
            this.grpStatBonuses.Controls.Add(this.lblStr);
            this.grpStatBonuses.Controls.Add(this.lblRange);
            this.grpStatBonuses.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpStatBonuses.Location = new System.Drawing.Point(434, 15);
            this.grpStatBonuses.Name = "grpStatBonuses";
            this.grpStatBonuses.Size = new System.Drawing.Size(292, 507);
            this.grpStatBonuses.TabIndex = 40;
            this.grpStatBonuses.TabStop = false;
            this.grpStatBonuses.Text = "Stat Bonuses";
            // 
            // lblSkillPoints
            // 
            this.lblSkillPoints.AutoSize = true;
            this.lblSkillPoints.Location = new System.Drawing.Point(140, 382);
            this.lblSkillPoints.Name = "lblSkillPoints";
            this.lblSkillPoints.Size = new System.Drawing.Size(58, 13);
            this.lblSkillPoints.TabIndex = 139;
            this.lblSkillPoints.Text = "Skill Points";
            // 
            // nudSkillPoints
            // 
            this.nudSkillPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSkillPoints.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSkillPoints.Location = new System.Drawing.Point(144, 399);
            this.nudSkillPoints.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSkillPoints.Name = "nudSkillPoints";
            this.nudSkillPoints.Size = new System.Drawing.Size(89, 20);
            this.nudSkillPoints.TabIndex = 138;
            this.nudSkillPoints.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSkillPoints.ValueChanged += new System.EventHandler(this.nudSkillPoints_ValueChanged);
            // 
            // grpWeaponBalance
            // 
            this.grpWeaponBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpWeaponBalance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpWeaponBalance.Controls.Add(this.lblMaxHitVal);
            this.grpWeaponBalance.Controls.Add(this.lblMaxHit);
            this.grpWeaponBalance.Controls.Add(this.lblDpsVal);
            this.grpWeaponBalance.Controls.Add(this.lblTierDpsVal);
            this.grpWeaponBalance.Controls.Add(this.lblProjectedDps);
            this.grpWeaponBalance.Controls.Add(this.lblTierDps);
            this.grpWeaponBalance.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpWeaponBalance.Location = new System.Drawing.Point(12, 419);
            this.grpWeaponBalance.Margin = new System.Windows.Forms.Padding(2);
            this.grpWeaponBalance.Name = "grpWeaponBalance";
            this.grpWeaponBalance.Padding = new System.Windows.Forms.Padding(2);
            this.grpWeaponBalance.Size = new System.Drawing.Size(266, 83);
            this.grpWeaponBalance.TabIndex = 134;
            this.grpWeaponBalance.TabStop = false;
            this.grpWeaponBalance.Text = "Weapon Balance Helper";
            // 
            // lblMaxHitVal
            // 
            this.lblMaxHitVal.AutoSize = true;
            this.lblMaxHitVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHitVal.Location = new System.Drawing.Point(177, 32);
            this.lblMaxHitVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxHitVal.Name = "lblMaxHitVal";
            this.lblMaxHitVal.Size = new System.Drawing.Size(18, 20);
            this.lblMaxHitVal.TabIndex = 138;
            this.lblMaxHitVal.Text = "0";
            // 
            // lblMaxHit
            // 
            this.lblMaxHit.AutoSize = true;
            this.lblMaxHit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxHit.Location = new System.Drawing.Point(178, 15);
            this.lblMaxHit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxHit.Name = "lblMaxHit";
            this.lblMaxHit.Size = new System.Drawing.Size(50, 13);
            this.lblMaxHit.TabIndex = 137;
            this.lblMaxHit.Text = "Max Hit";
            // 
            // lblDpsVal
            // 
            this.lblDpsVal.AutoSize = true;
            this.lblDpsVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDpsVal.Location = new System.Drawing.Point(78, 32);
            this.lblDpsVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDpsVal.Name = "lblDpsVal";
            this.lblDpsVal.Size = new System.Drawing.Size(18, 20);
            this.lblDpsVal.TabIndex = 136;
            this.lblDpsVal.Text = "0";
            // 
            // lblTierDpsVal
            // 
            this.lblTierDpsVal.AutoSize = true;
            this.lblTierDpsVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTierDpsVal.Location = new System.Drawing.Point(10, 30);
            this.lblTierDpsVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTierDpsVal.Name = "lblTierDpsVal";
            this.lblTierDpsVal.Size = new System.Drawing.Size(18, 20);
            this.lblTierDpsVal.TabIndex = 135;
            this.lblTierDpsVal.Text = "0";
            // 
            // lblProjectedDps
            // 
            this.lblProjectedDps.AutoSize = true;
            this.lblProjectedDps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectedDps.Location = new System.Drawing.Point(79, 15);
            this.lblProjectedDps.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProjectedDps.Name = "lblProjectedDps";
            this.lblProjectedDps.Size = new System.Drawing.Size(87, 13);
            this.lblProjectedDps.TabIndex = 134;
            this.lblProjectedDps.Text = "Projected Dps";
            // 
            // lblTierDps
            // 
            this.lblTierDps.AutoSize = true;
            this.lblTierDps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTierDps.Location = new System.Drawing.Point(11, 14);
            this.lblTierDps.Name = "lblTierDps";
            this.lblTierDps.Size = new System.Drawing.Size(58, 13);
            this.lblTierDps.TabIndex = 119;
            this.lblTierDps.Text = "Tier DPS";
            // 
            // grpArmorBalanceHelper
            // 
            this.grpArmorBalanceHelper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpArmorBalanceHelper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpArmorBalanceHelper.Controls.Add(this.lblHighResVal);
            this.grpArmorBalanceHelper.Controls.Add(this.lblHighRes);
            this.grpArmorBalanceHelper.Controls.Add(this.lblMediumResVal);
            this.grpArmorBalanceHelper.Controls.Add(this.lblLowResVal);
            this.grpArmorBalanceHelper.Controls.Add(this.lblMedRes);
            this.grpArmorBalanceHelper.Controls.Add(this.lblLowRes);
            this.grpArmorBalanceHelper.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpArmorBalanceHelper.Location = new System.Drawing.Point(13, 424);
            this.grpArmorBalanceHelper.Margin = new System.Windows.Forms.Padding(2);
            this.grpArmorBalanceHelper.Name = "grpArmorBalanceHelper";
            this.grpArmorBalanceHelper.Padding = new System.Windows.Forms.Padding(2);
            this.grpArmorBalanceHelper.Size = new System.Drawing.Size(266, 63);
            this.grpArmorBalanceHelper.TabIndex = 137;
            this.grpArmorBalanceHelper.TabStop = false;
            this.grpArmorBalanceHelper.Text = "Armor Balance Helper";
            // 
            // lblHighResVal
            // 
            this.lblHighResVal.AutoSize = true;
            this.lblHighResVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighResVal.Location = new System.Drawing.Point(181, 30);
            this.lblHighResVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHighResVal.Name = "lblHighResVal";
            this.lblHighResVal.Size = new System.Drawing.Size(18, 20);
            this.lblHighResVal.TabIndex = 138;
            this.lblHighResVal.Text = "0";
            // 
            // lblHighRes
            // 
            this.lblHighRes.AutoSize = true;
            this.lblHighRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighRes.Location = new System.Drawing.Point(182, 14);
            this.lblHighRes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHighRes.Name = "lblHighRes";
            this.lblHighRes.Size = new System.Drawing.Size(63, 13);
            this.lblHighRes.TabIndex = 137;
            this.lblHighRes.Text = "High Res.";
            // 
            // lblMediumResVal
            // 
            this.lblMediumResVal.AutoSize = true;
            this.lblMediumResVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMediumResVal.Location = new System.Drawing.Point(90, 30);
            this.lblMediumResVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMediumResVal.Name = "lblMediumResVal";
            this.lblMediumResVal.Size = new System.Drawing.Size(18, 20);
            this.lblMediumResVal.TabIndex = 136;
            this.lblMediumResVal.Text = "0";
            // 
            // lblLowResVal
            // 
            this.lblLowResVal.AutoSize = true;
            this.lblLowResVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowResVal.Location = new System.Drawing.Point(9, 30);
            this.lblLowResVal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLowResVal.Name = "lblLowResVal";
            this.lblLowResVal.Size = new System.Drawing.Size(18, 20);
            this.lblLowResVal.TabIndex = 135;
            this.lblLowResVal.Text = "0";
            // 
            // lblMedRes
            // 
            this.lblMedRes.AutoSize = true;
            this.lblMedRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedRes.Location = new System.Drawing.Point(91, 14);
            this.lblMedRes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMedRes.Name = "lblMedRes";
            this.lblMedRes.Size = new System.Drawing.Size(80, 13);
            this.lblMedRes.TabIndex = 134;
            this.lblMedRes.Text = "Medium Res.";
            // 
            // lblLowRes
            // 
            this.lblLowRes.AutoSize = true;
            this.lblLowRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowRes.Location = new System.Drawing.Point(5, 15);
            this.lblLowRes.Name = "lblLowRes";
            this.lblLowRes.Size = new System.Drawing.Size(60, 13);
            this.lblLowRes.TabIndex = 119;
            this.lblLowRes.Text = "Low Res.";
            // 
            // chkLockEvasion
            // 
            this.chkLockEvasion.AutoSize = true;
            this.chkLockEvasion.Location = new System.Drawing.Point(243, 299);
            this.chkLockEvasion.Name = "chkLockEvasion";
            this.chkLockEvasion.Size = new System.Drawing.Size(15, 14);
            this.chkLockEvasion.TabIndex = 133;
            this.chkLockEvasion.CheckedChanged += new System.EventHandler(this.chkLockEvasion_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(225, 323);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 13);
            this.label16.TabIndex = 132;
            this.label16.Text = "%";
            // 
            // nudEvasionPercent
            // 
            this.nudEvasionPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEvasionPercent.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEvasionPercent.Location = new System.Drawing.Point(143, 321);
            this.nudEvasionPercent.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEvasionPercent.Name = "nudEvasionPercent";
            this.nudEvasionPercent.Size = new System.Drawing.Size(77, 20);
            this.nudEvasionPercent.TabIndex = 131;
            this.nudEvasionPercent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEvasionPercent.ValueChanged += new System.EventHandler(this.nudEvasionPercent_ValueChanged);
            // 
            // nudEvasion
            // 
            this.nudEvasion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudEvasion.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudEvasion.Location = new System.Drawing.Point(143, 297);
            this.nudEvasion.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudEvasion.Name = "nudEvasion";
            this.nudEvasion.Size = new System.Drawing.Size(90, 20);
            this.nudEvasion.TabIndex = 129;
            this.nudEvasion.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudEvasion.ValueChanged += new System.EventHandler(this.nudEvasion_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(141, 284);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 13);
            this.label18.TabIndex = 128;
            this.label18.Text = "Evasion:";
            // 
            // chkLockAccuracy
            // 
            this.chkLockAccuracy.AutoSize = true;
            this.chkLockAccuracy.Location = new System.Drawing.Point(112, 297);
            this.chkLockAccuracy.Name = "chkLockAccuracy";
            this.chkLockAccuracy.Size = new System.Drawing.Size(15, 14);
            this.chkLockAccuracy.TabIndex = 127;
            this.chkLockAccuracy.CheckedChanged += new System.EventHandler(this.chkLockAccuracy_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(91, 323);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 126;
            this.label13.Text = "%";
            // 
            // nudAccuracyPercent
            // 
            this.nudAccuracyPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAccuracyPercent.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAccuracyPercent.Location = new System.Drawing.Point(13, 321);
            this.nudAccuracyPercent.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAccuracyPercent.Name = "nudAccuracyPercent";
            this.nudAccuracyPercent.Size = new System.Drawing.Size(73, 20);
            this.nudAccuracyPercent.TabIndex = 125;
            this.nudAccuracyPercent.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAccuracyPercent.ValueChanged += new System.EventHandler(this.nuAccuracyPercent_ValueChanged);
            // 
            // nudAccuracy
            // 
            this.nudAccuracy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudAccuracy.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudAccuracy.Location = new System.Drawing.Point(13, 297);
            this.nudAccuracy.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAccuracy.Name = "nudAccuracy";
            this.nudAccuracy.Size = new System.Drawing.Size(90, 20);
            this.nudAccuracy.TabIndex = 123;
            this.nudAccuracy.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudAccuracy.ValueChanged += new System.EventHandler(this.nudAccuracy_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 284);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 122;
            this.label15.Text = "Accuracy:";
            // 
            // chkLockPierceResist
            // 
            this.chkLockPierceResist.AutoSize = true;
            this.chkLockPierceResist.Location = new System.Drawing.Point(243, 168);
            this.chkLockPierceResist.Name = "chkLockPierceResist";
            this.chkLockPierceResist.Size = new System.Drawing.Size(15, 14);
            this.chkLockPierceResist.TabIndex = 119;
            this.chkLockPierceResist.CheckedChanged += new System.EventHandler(this.chkLockPierceResist_CheckedChanged);
            // 
            // chkLockSlashResist
            // 
            this.chkLockSlashResist.AutoSize = true;
            this.chkLockSlashResist.Location = new System.Drawing.Point(244, 101);
            this.chkLockSlashResist.Name = "chkLockSlashResist";
            this.chkLockSlashResist.Size = new System.Drawing.Size(15, 14);
            this.chkLockSlashResist.TabIndex = 121;
            this.chkLockSlashResist.CheckedChanged += new System.EventHandler(this.chkLockSlashResist_CheckedChanged);
            // 
            // chkLockPierce
            // 
            this.chkLockPierce.AutoSize = true;
            this.chkLockPierce.Location = new System.Drawing.Point(112, 170);
            this.chkLockPierce.Name = "chkLockPierce";
            this.chkLockPierce.Size = new System.Drawing.Size(15, 14);
            this.chkLockPierce.TabIndex = 118;
            this.chkLockPierce.CheckedChanged += new System.EventHandler(this.chkLockPierce_CheckedChanged);
            // 
            // chkLockSlash
            // 
            this.chkLockSlash.AutoSize = true;
            this.chkLockSlash.Location = new System.Drawing.Point(112, 98);
            this.chkLockSlash.Name = "chkLockSlash";
            this.chkLockSlash.Size = new System.Drawing.Size(15, 14);
            this.chkLockSlash.TabIndex = 120;
            this.chkLockSlash.CheckedChanged += new System.EventHandler(this.chkLockSlash_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(228, 194);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 117;
            this.label7.Text = "%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 124);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 194);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 116;
            this.label8.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 118;
            this.label2.Text = "%";
            // 
            // nudPierceResistPercentage
            // 
            this.nudPierceResistPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierceResistPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierceResistPercentage.Location = new System.Drawing.Point(147, 192);
            this.nudPierceResistPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPierceResistPercentage.Name = "nudPierceResistPercentage";
            this.nudPierceResistPercentage.Size = new System.Drawing.Size(76, 20);
            this.nudPierceResistPercentage.TabIndex = 115;
            this.nudPierceResistPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierceResistPercentage.ValueChanged += new System.EventHandler(this.nudPierceResistPercentage_ValueChanged);
            // 
            // nudSlashResistPercentage
            // 
            this.nudSlashResistPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashResistPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashResistPercentage.Location = new System.Drawing.Point(147, 122);
            this.nudSlashResistPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSlashResistPercentage.Name = "nudSlashResistPercentage";
            this.nudSlashResistPercentage.Size = new System.Drawing.Size(76, 20);
            this.nudSlashResistPercentage.TabIndex = 117;
            this.nudSlashResistPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashResistPercentage.ValueChanged += new System.EventHandler(this.nudSlashResistPercentage_ValueChanged);
            // 
            // nudPiercePercentage
            // 
            this.nudPiercePercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPiercePercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPiercePercentage.Location = new System.Drawing.Point(14, 192);
            this.nudPiercePercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPiercePercentage.Name = "nudPiercePercentage";
            this.nudPiercePercentage.Size = new System.Drawing.Size(72, 20);
            this.nudPiercePercentage.TabIndex = 114;
            this.nudPiercePercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPiercePercentage.ValueChanged += new System.EventHandler(this.nudPiercePercentage_ValueChanged);
            // 
            // nudSlashPercentage
            // 
            this.nudSlashPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashPercentage.Location = new System.Drawing.Point(13, 120);
            this.nudSlashPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSlashPercentage.Name = "nudSlashPercentage";
            this.nudSlashPercentage.Size = new System.Drawing.Size(72, 20);
            this.nudSlashPercentage.TabIndex = 116;
            this.nudSlashPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashPercentage.ValueChanged += new System.EventHandler(this.nudSlashPercentage_ValueChanged);
            // 
            // nudPierceResist
            // 
            this.nudPierceResist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierceResist.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierceResist.Location = new System.Drawing.Point(147, 168);
            this.nudPierceResist.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierceResist.Name = "nudPierceResist";
            this.nudPierceResist.Size = new System.Drawing.Size(90, 20);
            this.nudPierceResist.TabIndex = 111;
            this.nudPierceResist.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierceResist.ValueChanged += new System.EventHandler(this.nudPierceResist_ValueChanged);
            // 
            // nudSlashResist
            // 
            this.nudSlashResist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlashResist.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlashResist.Location = new System.Drawing.Point(147, 96);
            this.nudSlashResist.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlashResist.Name = "nudSlashResist";
            this.nudSlashResist.Size = new System.Drawing.Size(90, 20);
            this.nudSlashResist.TabIndex = 113;
            this.nudSlashResist.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlashResist.ValueChanged += new System.EventHandler(this.nudSlashResist_ValueChanged);
            // 
            // nudPierce
            // 
            this.nudPierce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudPierce.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudPierce.Location = new System.Drawing.Point(14, 168);
            this.nudPierce.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudPierce.Name = "nudPierce";
            this.nudPierce.Size = new System.Drawing.Size(90, 20);
            this.nudPierce.TabIndex = 110;
            this.nudPierce.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudPierce.ValueChanged += new System.EventHandler(this.nudPierce_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(145, 150);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 109;
            this.label11.Text = "Pierce Resistance:";
            // 
            // nudSlash
            // 
            this.nudSlash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSlash.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSlash.Location = new System.Drawing.Point(13, 96);
            this.nudSlash.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSlash.Name = "nudSlash";
            this.nudSlash.Size = new System.Drawing.Size(90, 20);
            this.nudSlash.TabIndex = 112;
            this.nudSlash.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudSlash.ValueChanged += new System.EventHandler(this.nudSlash_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 150);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 108;
            this.label12.Text = "Pierce Attack:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 111;
            this.label5.Text = "Slash Resistance:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 82);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 110;
            this.label6.Text = "Slash Attack:";
            // 
            // chkLockSpeed
            // 
            this.chkLockSpeed.AutoSize = true;
            this.chkLockSpeed.Location = new System.Drawing.Point(112, 372);
            this.chkLockSpeed.Name = "chkLockSpeed";
            this.chkLockSpeed.Size = new System.Drawing.Size(15, 14);
            this.chkLockSpeed.TabIndex = 109;
            this.chkLockSpeed.CheckedChanged += new System.EventHandler(this.chkLockSpeed_CheckedChanged);
            // 
            // chkLockMagicResist
            // 
            this.chkLockMagicResist.AutoSize = true;
            this.chkLockMagicResist.Location = new System.Drawing.Point(244, 239);
            this.chkLockMagicResist.Name = "chkLockMagicResist";
            this.chkLockMagicResist.Size = new System.Drawing.Size(15, 14);
            this.chkLockMagicResist.TabIndex = 108;
            this.chkLockMagicResist.CheckedChanged += new System.EventHandler(this.chkLockMagicResist_CheckedChanged);
            // 
            // chkLockArmor
            // 
            this.chkLockArmor.AutoSize = true;
            this.chkLockArmor.Location = new System.Drawing.Point(243, 30);
            this.chkLockArmor.Name = "chkLockArmor";
            this.chkLockArmor.Size = new System.Drawing.Size(15, 14);
            this.chkLockArmor.TabIndex = 107;
            this.chkLockArmor.CheckedChanged += new System.EventHandler(this.chkLockArmor_CheckedChanged);
            // 
            // chkLockMagic
            // 
            this.chkLockMagic.AutoSize = true;
            this.chkLockMagic.Location = new System.Drawing.Point(112, 233);
            this.chkLockMagic.Name = "chkLockMagic";
            this.chkLockMagic.Size = new System.Drawing.Size(15, 14);
            this.chkLockMagic.TabIndex = 106;
            this.chkLockMagic.CheckedChanged += new System.EventHandler(this.chkLockMagic_CheckedChanged);
            // 
            // chkLockStrength
            // 
            this.chkLockStrength.AutoSize = true;
            this.chkLockStrength.Location = new System.Drawing.Point(112, 30);
            this.chkLockStrength.Name = "chkLockStrength";
            this.chkLockStrength.Size = new System.Drawing.Size(15, 14);
            this.chkLockStrength.TabIndex = 104;
            this.chkLockStrength.CheckedChanged += new System.EventHandler(this.chkLockStrength_CheckedChanged);
            // 
            // lblPercentage5
            // 
            this.lblPercentage5.AutoSize = true;
            this.lblPercentage5.Location = new System.Drawing.Point(91, 398);
            this.lblPercentage5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage5.Name = "lblPercentage5";
            this.lblPercentage5.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage5.TabIndex = 82;
            this.lblPercentage5.Text = "%";
            // 
            // lblPercentage4
            // 
            this.lblPercentage4.AutoSize = true;
            this.lblPercentage4.Location = new System.Drawing.Point(228, 258);
            this.lblPercentage4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage4.Name = "lblPercentage4";
            this.lblPercentage4.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage4.TabIndex = 81;
            this.lblPercentage4.Text = "%";
            // 
            // lblPercentage8
            // 
            this.lblPercentage8.AutoSize = true;
            this.lblPercentage8.Location = new System.Drawing.Point(225, 54);
            this.lblPercentage8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage8.Name = "lblPercentage8";
            this.lblPercentage8.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage8.TabIndex = 80;
            this.lblPercentage8.Text = "%";
            // 
            // lblPercentage7
            // 
            this.lblPercentage7.AutoSize = true;
            this.lblPercentage7.Location = new System.Drawing.Point(90, 258);
            this.lblPercentage7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage7.Name = "lblPercentage7";
            this.lblPercentage7.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage7.TabIndex = 79;
            this.lblPercentage7.Text = "%";
            // 
            // lblPercentage6
            // 
            this.lblPercentage6.AutoSize = true;
            this.lblPercentage6.Location = new System.Drawing.Point(90, 55);
            this.lblPercentage6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage6.Name = "lblPercentage6";
            this.lblPercentage6.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage6.TabIndex = 78;
            this.lblPercentage6.Text = "%";
            // 
            // nudSpdPercentage
            // 
            this.nudSpdPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpdPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpdPercentage.Location = new System.Drawing.Point(12, 394);
            this.nudSpdPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSpdPercentage.Name = "nudSpdPercentage";
            this.nudSpdPercentage.Size = new System.Drawing.Size(74, 20);
            this.nudSpdPercentage.TabIndex = 77;
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
            this.nudMRPercentage.Location = new System.Drawing.Point(143, 256);
            this.nudMRPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMRPercentage.Name = "nudMRPercentage";
            this.nudMRPercentage.Size = new System.Drawing.Size(76, 20);
            this.nudMRPercentage.TabIndex = 76;
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
            this.nudDefPercentage.Location = new System.Drawing.Point(147, 51);
            this.nudDefPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDefPercentage.Name = "nudDefPercentage";
            this.nudDefPercentage.Size = new System.Drawing.Size(73, 20);
            this.nudDefPercentage.TabIndex = 75;
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
            this.nudMagPercentage.Location = new System.Drawing.Point(11, 256);
            this.nudMagPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMagPercentage.Name = "nudMagPercentage";
            this.nudMagPercentage.Size = new System.Drawing.Size(74, 20);
            this.nudMagPercentage.TabIndex = 74;
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
            this.nudStrPercentage.Location = new System.Drawing.Point(13, 52);
            this.nudStrPercentage.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudStrPercentage.Name = "nudStrPercentage";
            this.nudStrPercentage.Size = new System.Drawing.Size(72, 20);
            this.nudStrPercentage.TabIndex = 73;
            this.nudStrPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudStrPercentage.ValueChanged += new System.EventHandler(this.nudStrPercentage_ValueChanged);
            // 
            // nudRange
            // 
            this.nudRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudRange.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudRange.Location = new System.Drawing.Point(197, 375);
            this.nudRange.Name = "nudRange";
            this.nudRange.Size = new System.Drawing.Size(62, 20);
            this.nudRange.TabIndex = 53;
            this.nudRange.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudRange.ValueChanged += new System.EventHandler(this.nudRange_ValueChanged);
            // 
            // nudSpd
            // 
            this.nudSpd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudSpd.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudSpd.Location = new System.Drawing.Point(12, 370);
            this.nudSpd.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSpd.Name = "nudSpd";
            this.nudSpd.Size = new System.Drawing.Size(89, 20);
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
            this.nudMR.Location = new System.Drawing.Point(144, 233);
            this.nudMR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMR.Name = "nudMR";
            this.nudMR.Size = new System.Drawing.Size(90, 20);
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
            this.nudDef.Location = new System.Drawing.Point(147, 28);
            this.nudDef.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudDef.Name = "nudDef";
            this.nudDef.Size = new System.Drawing.Size(90, 20);
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
            this.nudMag.Location = new System.Drawing.Point(12, 233);
            this.nudMag.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudMag.Name = "nudMag";
            this.nudMag.Size = new System.Drawing.Size(90, 20);
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
            this.nudStr.Location = new System.Drawing.Point(13, 28);
            this.nudStr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudStr.Name = "nudStr";
            this.nudStr.Size = new System.Drawing.Size(90, 20);
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
            this.lblSpd.Location = new System.Drawing.Point(11, 355);
            this.lblSpd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpd.Name = "lblSpd";
            this.lblSpd.Size = new System.Drawing.Size(41, 13);
            this.lblSpd.TabIndex = 47;
            this.lblSpd.Text = "Speed:";
            // 
            // lblMR
            // 
            this.lblMR.AutoSize = true;
            this.lblMR.Location = new System.Drawing.Point(145, 218);
            this.lblMR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMR.Name = "lblMR";
            this.lblMR.Size = new System.Drawing.Size(71, 13);
            this.lblMR.TabIndex = 46;
            this.lblMR.Text = "Magic Resist:";
            // 
            // lblDef
            // 
            this.lblDef.AutoSize = true;
            this.lblDef.Location = new System.Drawing.Point(144, 12);
            this.lblDef.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDef.Name = "lblDef";
            this.lblDef.Size = new System.Drawing.Size(90, 13);
            this.lblDef.TabIndex = 45;
            this.lblDef.Text = "Blunt Resistance:";
            // 
            // lblMag
            // 
            this.lblMag.AutoSize = true;
            this.lblMag.Location = new System.Drawing.Point(13, 218);
            this.lblMag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMag.Name = "lblMag";
            this.lblMag.Size = new System.Drawing.Size(73, 13);
            this.lblMag.TabIndex = 44;
            this.lblMag.Text = "Magic Attack:";
            // 
            // lblStr
            // 
            this.lblStr.AutoSize = true;
            this.lblStr.Location = new System.Drawing.Point(11, 12);
            this.lblStr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStr.Name = "lblStr";
            this.lblStr.Size = new System.Drawing.Size(68, 13);
            this.lblStr.TabIndex = 43;
            this.lblStr.Text = "Blunt Attack:";
            // 
            // lblRange
            // 
            this.lblRange.AutoSize = true;
            this.lblRange.Location = new System.Drawing.Point(145, 354);
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(115, 13);
            this.lblRange.TabIndex = 20;
            this.lblRange.Text = "Stat Bonus Range (+-):";
            // 
            // cmbFemalePaperdoll
            // 
            this.cmbFemalePaperdoll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbFemalePaperdoll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbFemalePaperdoll.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbFemalePaperdoll.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbFemalePaperdoll.DrawDropdownHoverOutline = false;
            this.cmbFemalePaperdoll.DrawFocusRectangle = false;
            this.cmbFemalePaperdoll.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFemalePaperdoll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFemalePaperdoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFemalePaperdoll.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbFemalePaperdoll.FormattingEnabled = true;
            this.cmbFemalePaperdoll.Items.AddRange(new object[] {
            "None"});
            this.cmbFemalePaperdoll.Location = new System.Drawing.Point(222, 561);
            this.cmbFemalePaperdoll.Name = "cmbFemalePaperdoll";
            this.cmbFemalePaperdoll.Size = new System.Drawing.Size(168, 21);
            this.cmbFemalePaperdoll.TabIndex = 36;
            this.cmbFemalePaperdoll.Text = "None";
            this.cmbFemalePaperdoll.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbFemalePaperdoll.SelectedIndexChanged += new System.EventHandler(this.cmbFemalePaperdoll_SelectedIndexChanged);
            // 
            // lblFemalePaperdoll
            // 
            this.lblFemalePaperdoll.AutoSize = true;
            this.lblFemalePaperdoll.Location = new System.Drawing.Point(219, 544);
            this.lblFemalePaperdoll.Name = "lblFemalePaperdoll";
            this.lblFemalePaperdoll.Size = new System.Drawing.Size(91, 13);
            this.lblFemalePaperdoll.TabIndex = 35;
            this.lblFemalePaperdoll.Text = "Female Paperdoll:";
            // 
            // picFemalePaperdoll
            // 
            this.picFemalePaperdoll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picFemalePaperdoll.Location = new System.Drawing.Point(222, 589);
            this.picFemalePaperdoll.Name = "picFemalePaperdoll";
            this.picFemalePaperdoll.Size = new System.Drawing.Size(200, 155);
            this.picFemalePaperdoll.TabIndex = 34;
            this.picFemalePaperdoll.TabStop = false;
            // 
            // cmbEquipmentSlot
            // 
            this.cmbEquipmentSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEquipmentSlot.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEquipmentSlot.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEquipmentSlot.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEquipmentSlot.DrawDropdownHoverOutline = false;
            this.cmbEquipmentSlot.DrawFocusRectangle = false;
            this.cmbEquipmentSlot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEquipmentSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipmentSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEquipmentSlot.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEquipmentSlot.FormattingEnabled = true;
            this.cmbEquipmentSlot.Location = new System.Drawing.Point(103, 19);
            this.cmbEquipmentSlot.Name = "cmbEquipmentSlot";
            this.cmbEquipmentSlot.Size = new System.Drawing.Size(111, 21);
            this.cmbEquipmentSlot.TabIndex = 24;
            this.cmbEquipmentSlot.Text = null;
            this.cmbEquipmentSlot.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEquipmentSlot.SelectedIndexChanged += new System.EventHandler(this.cmbEquipmentSlot_SelectedIndexChanged);
            // 
            // lblEquipmentSlot
            // 
            this.lblEquipmentSlot.AutoSize = true;
            this.lblEquipmentSlot.Location = new System.Drawing.Point(12, 23);
            this.lblEquipmentSlot.Name = "lblEquipmentSlot";
            this.lblEquipmentSlot.Size = new System.Drawing.Size(81, 13);
            this.lblEquipmentSlot.TabIndex = 23;
            this.lblEquipmentSlot.Text = "Equipment Slot:";
            // 
            // cmbMalePaperdoll
            // 
            this.cmbMalePaperdoll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbMalePaperdoll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbMalePaperdoll.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbMalePaperdoll.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbMalePaperdoll.DrawDropdownHoverOutline = false;
            this.cmbMalePaperdoll.DrawFocusRectangle = false;
            this.cmbMalePaperdoll.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMalePaperdoll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMalePaperdoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMalePaperdoll.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbMalePaperdoll.FormattingEnabled = true;
            this.cmbMalePaperdoll.Items.AddRange(new object[] {
            "None"});
            this.cmbMalePaperdoll.Location = new System.Drawing.Point(12, 561);
            this.cmbMalePaperdoll.Name = "cmbMalePaperdoll";
            this.cmbMalePaperdoll.Size = new System.Drawing.Size(168, 21);
            this.cmbMalePaperdoll.TabIndex = 22;
            this.cmbMalePaperdoll.Text = "None";
            this.cmbMalePaperdoll.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbMalePaperdoll.SelectedIndexChanged += new System.EventHandler(this.cmbPaperdoll_SelectedIndexChanged);
            // 
            // lblMalePaperdoll
            // 
            this.lblMalePaperdoll.AutoSize = true;
            this.lblMalePaperdoll.Location = new System.Drawing.Point(9, 544);
            this.lblMalePaperdoll.Name = "lblMalePaperdoll";
            this.lblMalePaperdoll.Size = new System.Drawing.Size(80, 13);
            this.lblMalePaperdoll.TabIndex = 21;
            this.lblMalePaperdoll.Text = "Male Paperdoll:";
            // 
            // picMalePaperdoll
            // 
            this.picMalePaperdoll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picMalePaperdoll.Location = new System.Drawing.Point(12, 588);
            this.picMalePaperdoll.Name = "picMalePaperdoll";
            this.picMalePaperdoll.Size = new System.Drawing.Size(200, 156);
            this.picMalePaperdoll.TabIndex = 16;
            this.picMalePaperdoll.TabStop = false;
            // 
            // grpEvent
            // 
            this.grpEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEvent.Controls.Add(this.chkSingleUseEvent);
            this.grpEvent.Controls.Add(this.cmbEvent);
            this.grpEvent.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEvent.Location = new System.Drawing.Point(2, 698);
            this.grpEvent.Name = "grpEvent";
            this.grpEvent.Size = new System.Drawing.Size(200, 65);
            this.grpEvent.TabIndex = 42;
            this.grpEvent.TabStop = false;
            this.grpEvent.Text = "Event";
            this.grpEvent.Visible = false;
            // 
            // chkSingleUseEvent
            // 
            this.chkSingleUseEvent.AutoSize = true;
            this.chkSingleUseEvent.Checked = true;
            this.chkSingleUseEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleUseEvent.Location = new System.Drawing.Point(9, 42);
            this.chkSingleUseEvent.Name = "chkSingleUseEvent";
            this.chkSingleUseEvent.Size = new System.Drawing.Size(107, 17);
            this.chkSingleUseEvent.TabIndex = 29;
            this.chkSingleUseEvent.Text = "Destroy On Use?";
            this.chkSingleUseEvent.CheckedChanged += new System.EventHandler(this.chkSingleUse_CheckedChanged);
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
            this.cmbEvent.Location = new System.Drawing.Point(9, 15);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(185, 21);
            this.cmbEvent.TabIndex = 17;
            this.cmbEvent.Text = null;
            this.cmbEvent.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEvent.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
            // 
            // grpSpell
            // 
            this.grpSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpSpell.Controls.Add(this.chkSingleUseSpell);
            this.grpSpell.Controls.Add(this.chkQuickCast);
            this.grpSpell.Controls.Add(this.cmbTeachSpell);
            this.grpSpell.Controls.Add(this.lblSpell);
            this.grpSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSpell.Location = new System.Drawing.Point(2, 698);
            this.grpSpell.Name = "grpSpell";
            this.grpSpell.Size = new System.Drawing.Size(217, 127);
            this.grpSpell.TabIndex = 13;
            this.grpSpell.TabStop = false;
            this.grpSpell.Text = "Spell";
            this.grpSpell.Visible = false;
            // 
            // chkSingleUseSpell
            // 
            this.chkSingleUseSpell.AutoSize = true;
            this.chkSingleUseSpell.Checked = true;
            this.chkSingleUseSpell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleUseSpell.Location = new System.Drawing.Point(15, 95);
            this.chkSingleUseSpell.Name = "chkSingleUseSpell";
            this.chkSingleUseSpell.Size = new System.Drawing.Size(107, 17);
            this.chkSingleUseSpell.TabIndex = 29;
            this.chkSingleUseSpell.Text = "Destroy On Use?";
            this.chkSingleUseSpell.CheckedChanged += new System.EventHandler(this.chkSingleUse_CheckedChanged);
            // 
            // chkQuickCast
            // 
            this.chkQuickCast.AutoSize = true;
            this.chkQuickCast.Location = new System.Drawing.Point(15, 72);
            this.chkQuickCast.Name = "chkQuickCast";
            this.chkQuickCast.Size = new System.Drawing.Size(110, 17);
            this.chkQuickCast.TabIndex = 28;
            this.chkQuickCast.Text = "Quick Cast Spell?";
            this.chkQuickCast.CheckedChanged += new System.EventHandler(this.chkQuickCast_CheckedChanged);
            // 
            // cmbTeachSpell
            // 
            this.cmbTeachSpell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbTeachSpell.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbTeachSpell.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbTeachSpell.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbTeachSpell.DrawDropdownHoverOutline = false;
            this.cmbTeachSpell.DrawFocusRectangle = false;
            this.cmbTeachSpell.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTeachSpell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeachSpell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTeachSpell.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbTeachSpell.FormattingEnabled = true;
            this.cmbTeachSpell.Location = new System.Drawing.Point(15, 40);
            this.cmbTeachSpell.Name = "cmbTeachSpell";
            this.cmbTeachSpell.Size = new System.Drawing.Size(180, 21);
            this.cmbTeachSpell.TabIndex = 12;
            this.cmbTeachSpell.Text = null;
            this.cmbTeachSpell.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbTeachSpell.SelectedIndexChanged += new System.EventHandler(this.cmbTeachSpell_SelectedIndexChanged);
            // 
            // lblSpell
            // 
            this.lblSpell.AutoSize = true;
            this.lblSpell.Location = new System.Drawing.Point(12, 21);
            this.lblSpell.Name = "lblSpell";
            this.lblSpell.Size = new System.Drawing.Size(33, 13);
            this.lblSpell.TabIndex = 11;
            this.lblSpell.Text = "Spell:";
            // 
            // grpBags
            // 
            this.grpBags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpBags.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpBags.Controls.Add(this.nudBag);
            this.grpBags.Controls.Add(this.lblBag);
            this.grpBags.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpBags.Location = new System.Drawing.Point(2, 698);
            this.grpBags.Name = "grpBags";
            this.grpBags.Size = new System.Drawing.Size(222, 57);
            this.grpBags.TabIndex = 44;
            this.grpBags.TabStop = false;
            this.grpBags.Text = "Bag:";
            this.grpBags.Visible = false;
            // 
            // nudBag
            // 
            this.nudBag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudBag.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudBag.Location = new System.Drawing.Point(69, 23);
            this.nudBag.Maximum = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.nudBag.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBag.Name = "nudBag";
            this.nudBag.Size = new System.Drawing.Size(144, 20);
            this.nudBag.TabIndex = 38;
            this.nudBag.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBag.ValueChanged += new System.EventHandler(this.nudBag_ValueChanged);
            // 
            // lblBag
            // 
            this.lblBag.AutoSize = true;
            this.lblBag.Location = new System.Drawing.Point(8, 25);
            this.lblBag.Name = "lblBag";
            this.lblBag.Size = new System.Drawing.Size(55, 13);
            this.lblBag.TabIndex = 11;
            this.lblBag.Text = "Bag Slots:";
            // 
            // grpConsumable
            // 
            this.grpConsumable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpConsumable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpConsumable.Controls.Add(this.chkOnlyClanWar);
            this.grpConsumable.Controls.Add(this.chkMeleeConsumable);
            this.grpConsumable.Controls.Add(this.lblPercentage3);
            this.grpConsumable.Controls.Add(this.nudIntervalPercentage);
            this.grpConsumable.Controls.Add(this.lblPlus3);
            this.grpConsumable.Controls.Add(this.nudInterval);
            this.grpConsumable.Controls.Add(this.lblVital);
            this.grpConsumable.Controls.Add(this.cmbConsume);
            this.grpConsumable.Controls.Add(this.lblInterval);
            this.grpConsumable.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpConsumable.Location = new System.Drawing.Point(2, 698);
            this.grpConsumable.Name = "grpConsumable";
            this.grpConsumable.Size = new System.Drawing.Size(217, 180);
            this.grpConsumable.TabIndex = 12;
            this.grpConsumable.TabStop = false;
            this.grpConsumable.Text = "Consumable";
            this.grpConsumable.Visible = false;
            // 
            // chkOnlyClanWar
            // 
            this.chkOnlyClanWar.AutoSize = true;
            this.chkOnlyClanWar.Location = new System.Drawing.Point(19, 150);
            this.chkOnlyClanWar.Name = "chkOnlyClanWar";
            this.chkOnlyClanWar.Size = new System.Drawing.Size(100, 17);
            this.chkOnlyClanWar.TabIndex = 75;
            this.chkOnlyClanWar.Text = "Only Clan War?";
            this.chkOnlyClanWar.CheckedChanged += new System.EventHandler(this.chkOnlyClanWar_CheckedChanged);
            // 
            // chkMeleeConsumable
            // 
            this.chkMeleeConsumable.AutoSize = true;
            this.chkMeleeConsumable.Location = new System.Drawing.Point(19, 122);
            this.chkMeleeConsumable.Name = "chkMeleeConsumable";
            this.chkMeleeConsumable.Size = new System.Drawing.Size(114, 17);
            this.chkMeleeConsumable.TabIndex = 74;
            this.chkMeleeConsumable.Text = "Only Open Melee?";
            this.chkMeleeConsumable.CheckedChanged += new System.EventHandler(this.chkMeleeConsumable_CheckedChanged);
            // 
            // lblPercentage3
            // 
            this.lblPercentage3.AutoSize = true;
            this.lblPercentage3.Location = new System.Drawing.Point(195, 91);
            this.lblPercentage3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPercentage3.Name = "lblPercentage3";
            this.lblPercentage3.Size = new System.Drawing.Size(15, 13);
            this.lblPercentage3.TabIndex = 73;
            this.lblPercentage3.Text = "%";
            // 
            // nudIntervalPercentage
            // 
            this.nudIntervalPercentage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudIntervalPercentage.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudIntervalPercentage.Location = new System.Drawing.Point(148, 90);
            this.nudIntervalPercentage.Name = "nudIntervalPercentage";
            this.nudIntervalPercentage.Size = new System.Drawing.Size(43, 20);
            this.nudIntervalPercentage.TabIndex = 72;
            this.nudIntervalPercentage.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nudIntervalPercentage.ValueChanged += new System.EventHandler(this.nudIntervalPercentage_ValueChanged);
            // 
            // lblPlus3
            // 
            this.lblPlus3.AutoSize = true;
            this.lblPlus3.Location = new System.Drawing.Point(130, 91);
            this.lblPlus3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlus3.Name = "lblPlus3";
            this.lblPlus3.Size = new System.Drawing.Size(13, 13);
            this.lblPlus3.TabIndex = 71;
            this.lblPlus3.Text = "+";
            // 
            // nudInterval
            // 
            this.nudInterval.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.nudInterval.ForeColor = System.Drawing.Color.Gainsboro;
            this.nudInterval.Location = new System.Drawing.Point(19, 90);
            this.nudInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(106, 20);
            this.nudInterval.TabIndex = 37;
            this.nudInterval.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // lblVital
            // 
            this.lblVital.AutoSize = true;
            this.lblVital.Location = new System.Drawing.Point(16, 17);
            this.lblVital.Name = "lblVital";
            this.lblVital.Size = new System.Drawing.Size(30, 13);
            this.lblVital.TabIndex = 12;
            this.lblVital.Text = "Vital:";
            // 
            // cmbConsume
            // 
            this.cmbConsume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbConsume.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbConsume.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbConsume.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbConsume.DrawDropdownHoverOutline = false;
            this.cmbConsume.DrawFocusRectangle = false;
            this.cmbConsume.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbConsume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConsume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbConsume.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbConsume.FormattingEnabled = true;
            this.cmbConsume.Items.AddRange(new object[] {
            "Health",
            "Mana",
            "Experience"});
            this.cmbConsume.Location = new System.Drawing.Point(19, 37);
            this.cmbConsume.Name = "cmbConsume";
            this.cmbConsume.Size = new System.Drawing.Size(176, 21);
            this.cmbConsume.TabIndex = 11;
            this.cmbConsume.Text = "Health";
            this.cmbConsume.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbConsume.SelectedIndexChanged += new System.EventHandler(this.cmbConsume_SelectedIndexChanged);
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(19, 71);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(45, 13);
            this.lblInterval.TabIndex = 9;
            this.lblInterval.Text = "Interval:";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.grpAuxInfo);
            this.pnlContainer.Controls.Add(this.grpConsumable);
            this.pnlContainer.Controls.Add(this.grpEnhancement);
            this.pnlContainer.Controls.Add(this.grpEvent);
            this.pnlContainer.Controls.Add(this.grpEquipment);
            this.pnlContainer.Controls.Add(this.grpGeneral);
            this.pnlContainer.Controls.Add(this.grpBags);
            this.pnlContainer.Controls.Add(this.grpSpell);
            this.pnlContainer.Location = new System.Drawing.Point(221, 34);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(764, 582);
            this.pnlContainer.TabIndex = 43;
            this.pnlContainer.Visible = false;
            // 
            // grpAuxInfo
            // 
            this.grpAuxInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpAuxInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpAuxInfo.Controls.Add(this.lstShopsBuy);
            this.grpAuxInfo.Controls.Add(this.lblShopsBuy);
            this.grpAuxInfo.Controls.Add(this.lblShops);
            this.grpAuxInfo.Controls.Add(this.lstShops);
            this.grpAuxInfo.Controls.Add(this.lstCrafts);
            this.grpAuxInfo.Controls.Add(this.lblCraftsUsed);
            this.grpAuxInfo.Controls.Add(this.lstDrops);
            this.grpAuxInfo.Controls.Add(this.lblDrops);
            this.grpAuxInfo.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpAuxInfo.Location = new System.Drawing.Point(468, 2);
            this.grpAuxInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpAuxInfo.Name = "grpAuxInfo";
            this.grpAuxInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpAuxInfo.Size = new System.Drawing.Size(277, 555);
            this.grpAuxInfo.TabIndex = 104;
            this.grpAuxInfo.TabStop = false;
            this.grpAuxInfo.Text = "Metadata";
            // 
            // lstShopsBuy
            // 
            this.lstShopsBuy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstShopsBuy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstShopsBuy.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstShopsBuy.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstShopsBuy.FormattingEnabled = true;
            this.lstShopsBuy.Location = new System.Drawing.Point(17, 439);
            this.lstShopsBuy.Name = "lstShopsBuy";
            this.lstShopsBuy.Size = new System.Drawing.Size(229, 106);
            this.lstShopsBuy.TabIndex = 114;
            // 
            // lblShopsBuy
            // 
            this.lblShopsBuy.AutoSize = true;
            this.lblShopsBuy.Location = new System.Drawing.Point(14, 419);
            this.lblShopsBuy.Name = "lblShopsBuy";
            this.lblShopsBuy.Size = new System.Drawing.Size(102, 13);
            this.lblShopsBuy.TabIndex = 113;
            this.lblShopsBuy.Text = "Shops that Buy This";
            // 
            // lblShops
            // 
            this.lblShops.AutoSize = true;
            this.lblShops.Location = new System.Drawing.Point(14, 283);
            this.lblShops.Name = "lblShops";
            this.lblShops.Size = new System.Drawing.Size(101, 13);
            this.lblShops.TabIndex = 112;
            this.lblShops.Text = "Shops that Sell This";
            // 
            // lstShops
            // 
            this.lstShops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstShops.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstShops.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstShops.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstShops.FormattingEnabled = true;
            this.lstShops.Location = new System.Drawing.Point(17, 303);
            this.lstShops.Name = "lstShops";
            this.lstShops.Size = new System.Drawing.Size(229, 106);
            this.lstShops.TabIndex = 111;
            // 
            // lstCrafts
            // 
            this.lstCrafts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstCrafts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCrafts.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstCrafts.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstCrafts.FormattingEnabled = true;
            this.lstCrafts.Location = new System.Drawing.Point(17, 170);
            this.lstCrafts.Name = "lstCrafts";
            this.lstCrafts.Size = new System.Drawing.Size(229, 106);
            this.lstCrafts.TabIndex = 110;
            // 
            // lblCraftsUsed
            // 
            this.lblCraftsUsed.AutoSize = true;
            this.lblCraftsUsed.Location = new System.Drawing.Point(14, 150);
            this.lblCraftsUsed.Name = "lblCraftsUsed";
            this.lblCraftsUsed.Size = new System.Drawing.Size(100, 13);
            this.lblCraftsUsed.TabIndex = 109;
            this.lblCraftsUsed.Text = "Crafts that Use This";
            // 
            // grpEnhancement
            // 
            this.grpEnhancement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.grpEnhancement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.grpEnhancement.Controls.Add(this.cmbEnhancement);
            this.grpEnhancement.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpEnhancement.Location = new System.Drawing.Point(2, 698);
            this.grpEnhancement.Name = "grpEnhancement";
            this.grpEnhancement.Size = new System.Drawing.Size(200, 47);
            this.grpEnhancement.TabIndex = 43;
            this.grpEnhancement.TabStop = false;
            this.grpEnhancement.Text = "Enhancement";
            this.grpEnhancement.Visible = false;
            // 
            // cmbEnhancement
            // 
            this.cmbEnhancement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.cmbEnhancement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.cmbEnhancement.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.cmbEnhancement.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.cmbEnhancement.DrawDropdownHoverOutline = false;
            this.cmbEnhancement.DrawFocusRectangle = false;
            this.cmbEnhancement.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEnhancement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnhancement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEnhancement.ForeColor = System.Drawing.Color.Gainsboro;
            this.cmbEnhancement.FormattingEnabled = true;
            this.cmbEnhancement.Location = new System.Drawing.Point(9, 15);
            this.cmbEnhancement.Name = "cmbEnhancement";
            this.cmbEnhancement.Size = new System.Drawing.Size(185, 21);
            this.cmbEnhancement.TabIndex = 17;
            this.cmbEnhancement.Text = null;
            this.cmbEnhancement.TextPadding = new System.Windows.Forms.Padding(2);
            this.cmbEnhancement.SelectedIndexChanged += new System.EventHandler(this.cmbEnhancement_SelectedIndexChanged);
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
            this.toolStrip.Size = new System.Drawing.Size(1009, 25);
            this.toolStrip.TabIndex = 44;
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
            // FrmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1009, 664);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "FrmItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmItem_FormClosed);
            this.Load += new System.EventHandler(this.frmItem_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form_KeyDown);
            this.grpItems.ResumeLayout(false);
            this.grpItems.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFuel)).EndInit();
            this.grpDestroy.ResumeLayout(false);
            this.grpDestroy.PerformLayout();
            this.grpTags.ResumeLayout(false);
            this.grpTags.PerformLayout();
            this.grpRequirements.ResumeLayout(false);
            this.grpRequirements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBankStackLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInvStackLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeathDropChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRgbaR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCooldown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picItem)).EndInit();
            this.grpEquipment.ResumeLayout(false);
            this.grpEquipment.PerformLayout();
            this.grpProc.ResumeLayout(false);
            this.grpProc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProcChance)).EndInit();
            this.grpUpgrades.ResumeLayout(false);
            this.grpUpgrades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpgradeCost)).EndInit();
            this.grpWeaponEnhancement.ResumeLayout(false);
            this.grpWeaponEnhancement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnhanceThresh)).EndInit();
            this.grpDeconstruction.ResumeLayout(false);
            this.grpDeconstruction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStudyChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReqFuel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeconTableRolls)).EndInit();
            this.grpWeaponTypes.ResumeLayout(false);
            this.grpWeaponTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeaponCraftExp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxWeaponLvl)).EndInit();
            this.grpWeaponProperties.ResumeLayout(false);
            this.grpWeaponProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritMultiplier)).EndInit();
            this.grpDamageTypes.ResumeLayout(false);
            this.grpDamageTypes.PerformLayout();
            this.grpAttackSpeed.ResumeLayout(false);
            this.grpAttackSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAttackSpeedValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCritChance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDamage)).EndInit();
            this.grpCosmetic.ResumeLayout(false);
            this.grpCosmetic.PerformLayout();
            this.grpSpecialAttack.ResumeLayout(false);
            this.grpSpecialAttack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpecialAttackChargeTime)).EndInit();
            this.grpBonusEffects.ResumeLayout(false);
            this.grpBonusEffects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEffectPercent)).EndInit();
            this.grpAdditionalWeaponProps.ResumeLayout(false);
            this.grpAdditionalWeaponProps.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackBoost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrafeBoost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBackstabMultiplier)).EndInit();
            this.grpHelmetPaperdollProps.ResumeLayout(false);
            this.grpHelmetPaperdollProps.PerformLayout();
            this.grpPrayerProperties.ResumeLayout(false);
            this.grpPrayerProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudComboExpBoost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudComboInterval)).EndInit();
            this.grpRegen.ResumeLayout(false);
            this.grpRegen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpRegen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPRegen)).EndInit();
            this.grpVitalBonuses.ResumeLayout(false);
            this.grpVitalBonuses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMPPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHPPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudManaBonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHealthBonus)).EndInit();
            this.grpStatBonuses.ResumeLayout(false);
            this.grpStatBonuses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSkillPoints)).EndInit();
            this.grpWeaponBalance.ResumeLayout(false);
            this.grpWeaponBalance.PerformLayout();
            this.grpArmorBalanceHelper.ResumeLayout(false);
            this.grpArmorBalanceHelper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasionPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEvasion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracyPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResistPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResistPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPiercePercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierceResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlashResist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPierce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpdPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMRPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDefPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStrPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFemalePaperdoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMalePaperdoll)).EndInit();
            this.grpEvent.ResumeLayout(false);
            this.grpEvent.PerformLayout();
            this.grpSpell.ResumeLayout(false);
            this.grpSpell.PerformLayout();
            this.grpBags.ResumeLayout(false);
            this.grpBags.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBag)).EndInit();
            this.grpConsumable.ResumeLayout(false);
            this.grpConsumable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervalPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.grpAuxInfo.ResumeLayout(false);
            this.grpAuxInfo.PerformLayout();
            this.grpEnhancement.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label lblName;
        private Label lblType;
        private Label lblAnim;
        private Label lblPrice;
        private PictureBox picItem;
        private Label lblDamage;
        private PictureBox picMalePaperdoll;
        private Label lblInterval;
        private Label lblVital;
        private Label lblSpell;
        private Label lblRange;
        private Label lblPic;
        private Label lblMalePaperdoll;
        private Label lblDesc;
        private Label lblEquipmentSlot;
        private Label lblEffectPercent;
        private Label lblBonusEffect;
        private Label lblToolType;
        private Label lblProjectile;
        private Panel pnlContainer;
        private Label lblFemalePaperdoll;
        private PictureBox picFemalePaperdoll;
        private ToolStripButton toolStripItemNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripItemDelete;
        private ToolStripSeparator toolStripSeparator2;
        public ToolStripButton toolStripItemCopy;
        public ToolStripButton toolStripItemPaste;
        private ToolStripSeparator toolStripSeparator3;
        public ToolStripButton toolStripItemUndo;
        private Label lblAttackAnimation;
        private DarkUI.Controls.DarkGroupBox grpStatBonuses;
        private DarkUI.Controls.DarkGroupBox grpWeaponProperties;
        private Label lblCritChance;
        private DarkGroupBox grpItems;
        private DarkButton btnSave;
        private DarkGroupBox grpGeneral;
        private DarkTextBox txtName;
        private DarkComboBox cmbType;
        private DarkGroupBox grpEquipment;
        private DarkGroupBox grpConsumable;
        private DarkComboBox cmbConsume;
        private DarkGroupBox grpSpell;
        private DarkButton btnCancel;
        private DarkComboBox cmbPic;
        private DarkComboBox cmbMalePaperdoll;
        private DarkTextBox txtDesc;
        private DarkCheckBox chk2Hand;
        private DarkComboBox cmbEquipmentSlot;
        private DarkComboBox cmbEquipmentBonus;
        private DarkComboBox cmbToolType;
        private DarkGroupBox grpEvent;
        private DarkComboBox cmbFemalePaperdoll;
        private DarkComboBox cmbAttackAnimation;
        private DarkComboBox cmbProjectile;
        private DarkToolStrip toolStrip;
        private DarkButton btnEditRequirements;
        private DarkComboBox cmbAnimation;
        private DarkComboBox cmbTeachSpell;
        private DarkComboBox cmbEvent;
        private DarkGroupBox grpBags;
        private Label lblBag;
        private DarkNumericUpDown nudPrice;
        private DarkNumericUpDown nudBag;
        private DarkNumericUpDown nudInterval;
        private DarkNumericUpDown nudEffectPercent;
        private DarkNumericUpDown nudRange;
        private DarkNumericUpDown nudSpd;
        private DarkNumericUpDown nudMR;
        private DarkNumericUpDown nudDef;
        private DarkNumericUpDown nudMag;
        private DarkNumericUpDown nudStr;
        private Label lblSpd;
        private Label lblMR;
        private Label lblDef;
        private Label lblMag;
        private Label lblStr;
        private DarkNumericUpDown nudCritChance;
        private DarkNumericUpDown nudDamage;
        private DarkCheckBox chkStackable;
        private DarkCheckBox chkCanDrop;
        private DarkGroupBox grpVitalBonuses;
        private DarkNumericUpDown nudManaBonus;
        private DarkNumericUpDown nudHealthBonus;
        private Label lblManaBonus;
        private Label lblHealthBonus;
        private DarkComboBox cmbEquipmentAnimation;
        private Label lblEquipmentAnimation;
        private DarkGroupBox grpAttackSpeed;
        private DarkNumericUpDown nudAttackSpeedValue;
        private Label lblAttackSpeedValue;
        private DarkComboBox cmbAttackSpeedModifier;
        private Label lblAttackSpeedModifier;
        private DarkNumericUpDown nudCritMultiplier;
        private Label lblCritMultiplier;
        private DarkNumericUpDown nudCooldown;
        private Label lblCooldown;
        private DarkCheckBox chkSingleUseSpell;
        private DarkCheckBox chkSingleUseEvent;
        private DarkCheckBox chkQuickCast;
        private DarkComboBox cmbRarity;
        private Label lblRarity;
        private DarkButton btnClearSearch;
        private DarkTextBox txtSearch;
        private ToolStripButton btnAlphabetical;
        private ToolStripSeparator toolStripSeparator4;
        private DarkButton btnAddFolder;
        private Label lblFolder;
        private DarkComboBox cmbFolder;
        private Label lblPercentage2;
        private Label lblPercentage1;
        private DarkNumericUpDown nudMPPercentage;
        private DarkNumericUpDown nudHPPercentage;
        private Label lblPlus2;
        private Label lblPlus1;
        private Label lblPercentage3;
        private DarkNumericUpDown nudIntervalPercentage;
        private Label lblPlus3;
        private Label lblPercentage5;
        private Label lblPercentage4;
        private Label lblPercentage8;
        private Label lblPercentage7;
        private Label lblPercentage6;
        private DarkNumericUpDown nudSpdPercentage;
        private DarkNumericUpDown nudMRPercentage;
        private DarkNumericUpDown nudDefPercentage;
        private DarkNumericUpDown nudMagPercentage;
        private DarkNumericUpDown nudStrPercentage;
        private DarkGroupBox grpRegen;
        private DarkNumericUpDown nudMpRegen;
        private DarkNumericUpDown nudHPRegen;
        private Label lblHpRegen;
        private Label lblManaRegen;
        private Label lblRegenHint;
        private DarkComboBox cmbCooldownGroup;
        private Label lblCooldownGroup;
        private DarkButton btnAddCooldownGroup;
        private DarkCheckBox chkIgnoreGlobalCooldown;
        private Label lblAlpha;
        private Label lblBlue;
        private Label lblGreen;
        private Label lblRed;
        private DarkNumericUpDown nudRgbaA;
        private DarkNumericUpDown nudRgbaB;
        private DarkNumericUpDown nudRgbaG;
        private DarkNumericUpDown nudRgbaR;
        private DarkCheckBox chkIgnoreCdr;
        private Controls.GameObjectList lstGameObjects;
        private DarkCheckBox chkCanSell;
        private DarkCheckBox chkCanTrade;
        private DarkCheckBox chkCanBag;
        private DarkCheckBox chkCanBank;
        private DarkNumericUpDown nudDeathDropChance;
        private Label lblDeathDropChance;
        private DarkNumericUpDown nudBankStackLimit;
        private DarkNumericUpDown nudInvStackLimit;
        private Label lblBankStackLimit;
        private Label lblInvStackLimit;
        private DarkCheckBox chkCanGuildBank;
        private DarkGroupBox grpRequirements;
        private Label lblCannotUse;
        private DarkTextBox txtCannotUse;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private DarkGroupBox grpPrayerProperties;
        private DarkComboBox cmbComboSpell;
        private Label lblComboSpell;
        private Label lblComboInterval;
        private DarkNumericUpDown nudComboInterval;
        private Label lblComboExpBoost;
        private DarkNumericUpDown nudComboExpBoost;
        private DarkGroupBox grpHelmetPaperdollProps;
        private DarkCheckBox chkHelmHideExtra;
        private DarkCheckBox chkHelmHideBeard;
        private DarkCheckBox chkHelmHideHair;
        private DarkButton btnNewTag;
        private DarkComboBox cmbTags;
        private DarkGroupBox grpTags;
        private DarkButton btnAddTag;
        private Label lblTagToAdd;
        private DarkButton btnRemoveTag;
        private ListBox lstTags;
        private DarkCheckBox chkLockSpeed;
        private DarkCheckBox chkLockMagicResist;
        private DarkCheckBox chkLockArmor;
        private DarkCheckBox chkLockMagic;
        private DarkCheckBox chkLockStrength;
        private DarkGroupBox grpDestroy;
        private Label lblDestroyMessage;
        private DarkTextBox txtCannotDestroy;
        private DarkButton btnDestroyRequirements;
        private DarkCheckBox chkEnableDestroy;
        private DarkGroupBox grpAdditionalWeaponProps;
        private DarkCheckBox chkBackstab;
        private Label lblBackstabMultiplier;
        private DarkNumericUpDown nudBackstabMultiplier;
        private ListBox lstDrops;
        private Label lblDrops;
        private DarkCheckBox chkInstanceDestroy;
        private DarkGroupBox grpAuxInfo;
        private ListBox lstCrafts;
        private Label lblCraftsUsed;
        private Label lblShops;
        private ListBox lstShops;
        private ListBox lstShopsBuy;
        private Label lblShopsBuy;
        private DarkComboBox cmbTypeDisplayOverride;
        private Label lblOverride;
        private DarkButton btnClearOverride;
        private Label lblBackBoost;
        private DarkNumericUpDown nudBackBoost;
        private DarkNumericUpDown nudStrafeBoost;
        private Label lblStrafeModifier;
        private DarkCheckBox chkShortHair;
        private DarkGroupBox grpBonusEffects;
        private DarkButton btnRemoveBonus;
        private DarkButton btnAddBonus;
        private ListBox lstBonusEffects;
        private DarkGroupBox grpSpecialAttack;
        private Label lblSpecialAttack;
        private Label lblSpecialAttackCharge;
        private DarkComboBox cmbSpecialAttack;
        private DarkNumericUpDown nudSpecialAttackChargeTime;
        private DarkCheckBox chkLockEvasion;
        private Label label16;
        private DarkNumericUpDown nudEvasionPercent;
        private DarkNumericUpDown nudEvasion;
        private Label label18;
        private DarkCheckBox chkLockAccuracy;
        private Label label13;
        private DarkNumericUpDown nudAccuracyPercent;
        private DarkNumericUpDown nudAccuracy;
        private Label label15;
        private DarkCheckBox chkLockPierceResist;
        private DarkCheckBox chkLockSlashResist;
        private DarkCheckBox chkLockPierce;
        private DarkCheckBox chkLockSlash;
        private Label label7;
        private Label label1;
        private Label label8;
        private Label label2;
        private DarkNumericUpDown nudPierceResistPercentage;
        private DarkNumericUpDown nudSlashResistPercentage;
        private DarkNumericUpDown nudPiercePercentage;
        private DarkNumericUpDown nudSlashPercentage;
        private DarkNumericUpDown nudPierceResist;
        private DarkNumericUpDown nudSlashResist;
        private DarkNumericUpDown nudPierce;
        private Label label11;
        private DarkNumericUpDown nudSlash;
        private Label label12;
        private Label label5;
        private Label label6;
        private DarkCheckBox chkDamageMagic;
        private DarkCheckBox chkDamagePierce;
        private DarkCheckBox chkDamageSlash;
        private DarkCheckBox chkBluntDamage;
        private DarkGroupBox grpDamageTypes;
        private Label label23;
        private Label label22;
        private Label label21;
        private Label label20;
        private DarkGroupBox grpCosmetic;
        private Label lblCosmeticDisplayName;
        private DarkTextBox txtCosmeticDisplayName;
        private DarkGroupBox grpWeaponTypes;
        private DarkButton btnRemoveWeaponType;
        private DarkButton btnAddWeaponType;
        private ListBox lstWeaponTypes;
        private Label lblWeaponTypes;
        private DarkComboBox cmbWeaponTypes;
        private DarkNumericUpDown nudMaxWeaponLvl;
        private Label lblMaxWeaponLvl;
        private DarkCheckBox chkRareDrop;
        private DarkNumericUpDown nudFuel;
        private Label lblFuel;
        private DarkGroupBox grpDeconstruction;
        private DarkButton btnRemoveDeconTable;
        private DarkButton btnAddDeconTable;
        private ListBox lstDeconstructionTables;
        private Label lblDeconLootTable;
        private Label lblDeconTableRolls;
        private DarkComboBox cmbDeconTables;
        private DarkNumericUpDown nudDeconTableRolls;
        private DarkNumericUpDown nudReqFuel;
        private Label lblFuelReq;
        private Label lblDeconLoot;
        private DarkNumericUpDown nudWeaponCraftExp;
        private Label lblWeaponCraftExp;
        private DarkGroupBox grpWeaponEnhancement;
        private DarkNumericUpDown nudEnhanceThresh;
        private Label lblEnhancementThres;
        private DarkGroupBox grpEnhancement;
        private DarkComboBox cmbEnhancement;
        private DarkGroupBox grpUpgrades;
        private ListBox lstUpgrades;
        private DarkButton btnRemoveUpgrade;
        private DarkButton btnAddUpgrade;
        private Label lblUpgrade;
        private DarkComboBox cmbUpgrade;
        private DarkNumericUpDown nudUpgradeCost;
        private Label lblUpgradeCost;
        private Label lblProjectedDps;
        private DarkGroupBox grpWeaponBalance;
        private Label lblTierDps;
        private Label lblDpsVal;
        private Label lblTierDpsVal;
        private DarkGroupBox grpArmorBalanceHelper;
        private Label lblMediumResVal;
        private Label lblLowResVal;
        private Label lblMedRes;
        private Label lblLowRes;
        private Label lblHighResVal;
        private Label lblHighRes;
        private Label lblMaxHit;
        private Label lblMaxHitVal;
        private DarkCheckBox chkIsFocus;
        private DarkNumericUpDown nudStudyChance;
        private Label lblStudyChance;
        private DarkComboBox cmbStudyEnhancement;
        private Label lblStudy;
        private DarkButton btnFuelRecc;
        private DarkButton btnFuelReqRecc;
        private Label lblSortName;
        private DarkTextBox txtSortName;
        private Label lblSkillPoints;
        private DarkNumericUpDown nudSkillPoints;
        private DarkCheckBox chkMeleeConsumable;
        private DarkGroupBox grpProc;
        private DarkNumericUpDown nudProcChance;
        private Label label3;
        private Label lblProcChance;
        private DarkComboBox cmbProcSpell;
        private DarkCheckBox chkOnlyClanWar;
        private DarkComboBox cmbAmmoOverride;
        private Label lblAmmoOverride;
    }
}
