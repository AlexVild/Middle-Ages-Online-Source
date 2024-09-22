using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Controls;
using DarkUI.Forms;

using Intersect.Editor.Content;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps.MapList;
using Intersect.Utilities;
using static Intersect.GameObjects.SpellBase;

namespace Intersect.Editor.Forms.Editors
{

    public partial class FrmSpell : EditorForm
    {

        private List<SpellBase> mChanged = new List<SpellBase>();

        private string mCopiedItem;

        private SpellBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        private List<string> mKnownCooldownGroups = new List<string>();

        private List<string> mKnownSpellGroups = new List<string>();

        private bool mPopulating = false;

        private int CurrentTier = 0;

        private SpellTypes? PrevSpellType = null;

        public FrmSpell()
        {
            ApplyHooks();
            InitializeComponent();

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }
        private void AssignEditorItem(Guid id)
        {
            mEditorItem = SpellBase.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.Spell)
            {
                InitEditor();
                if (mEditorItem != null && !SpellBase.Lookup.Values.Contains(mEditorItem))
                {
                    mEditorItem = null;
                    UpdateEditor();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (var item in mChanged)
            {
                item.RestoreBackup();
                item.DeleteBackup();
            }

            Hide();
            Globals.CurrentEditor = -1;
            Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Send Changed items
            foreach (var item in mChanged)
            {
                PacketSender.SendSaveObject(item);
                item.DeleteBackup();
            }

            Hide();
            Globals.CurrentEditor = -1;
            Dispose();
        }

        private void frmSpell_Load(object sender, EventArgs e)
        {
            cmbProjectile.Items.Clear();
            cmbProjectile.Items.AddRange(ProjectileBase.Names);
            cmbCastAnimation.Items.Clear();
            cmbCastAnimation.Items.Add(Strings.General.none);
            cmbCastAnimation.Items.AddRange(AnimationBase.Names);
            cmbHitAnimation.Items.Clear();
            cmbHitAnimation.Items.Add(Strings.General.none);
            cmbHitAnimation.Items.AddRange(AnimationBase.Names);
            cmbTrapAnimation.Items.Clear();
            cmbTrapAnimation.Items.Add(Strings.General.none);
            cmbTrapAnimation.Items.AddRange(AnimationBase.Names);
            cmbEvent.Items.Clear();
            cmbEvent.Items.Add(Strings.General.none);
            cmbEvent.Items.AddRange(EventBase.Names);
            cmbOverTimeAnimation.Items.Clear();
            cmbOverTimeAnimation.Items.Add(Strings.General.none);
            cmbOverTimeAnimation.Items.AddRange(AnimationBase.Names);

            cmbDashAnimation.Items.Clear();
            cmbDashAnimation.Items.Add(Strings.General.none);
            cmbDashAnimation.Items.AddRange(AnimationBase.Names);

            cmbSprite.Items.Clear();
            cmbSprite.Items.Add(Strings.General.none);
            var spellNames = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Spell);
            cmbSprite.Items.AddRange(spellNames);

            cmbTransform.Items.Clear();
            cmbTransform.Items.Add(Strings.General.none);
            var spriteNames = GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Entity);
            cmbTransform.Items.AddRange(spriteNames);

            nudWarpX.Maximum = (int) Options.MapWidth;
            nudWarpY.Maximum = (int) Options.MapHeight;

            cmbWarpMap.Items.Clear();
            cmbWarpMap.Items.AddRange(MapList.OrderedMaps.Select(map => map?.Name).ToArray());
            cmbWarpMap.SelectedIndex = 0;

            nudStr.Maximum = Options.MaxStatValue;
            nudMag.Maximum = Options.MaxStatValue;
            nudDef.Maximum = Options.MaxStatValue;
            nudMR.Maximum = Options.MaxStatValue;
            nudSpd.Maximum = Options.MaxStatValue;
            nudStr.Minimum = -Options.MaxStatValue;
            nudMag.Minimum = -Options.MaxStatValue;
            nudDef.Minimum = -Options.MaxStatValue;
            nudMR.Minimum = -Options.MaxStatValue;
            nudSpd.Minimum = -Options.MaxStatValue;

            nudCastDuration.Maximum = Int32.MaxValue;
            nudCooldownDuration.Maximum = Int32.MaxValue;

            InitLocalization();
            UpdateEditor();
        }

        private void InitLocalization()
        {
            Text = Strings.SpellEditor.title;
            toolStripItemNew.Text = Strings.SpellEditor.New;
            toolStripItemDelete.Text = Strings.SpellEditor.delete;
            toolStripItemCopy.Text = Strings.SpellEditor.copy;
            toolStripItemPaste.Text = Strings.SpellEditor.paste;
            toolStripItemUndo.Text = Strings.SpellEditor.undo;

            grpSpells.Text = Strings.SpellEditor.spells;

            grpGeneral.Text = Strings.SpellEditor.general;
            lblName.Text = Strings.SpellEditor.name;
            lblType.Text = Strings.SpellEditor.type;
            cmbType.Items.Clear();
            cmbType.Items.AddRange(EnumExtensions.GetDescriptions(typeof(SpellTypes)));

            lblIcon.Text = Strings.SpellEditor.icon;
            lblDesc.Text = Strings.SpellEditor.description;
            lblCastAnimation.Text = Strings.SpellEditor.castanimation;
            lblHitAnimation.Text = Strings.SpellEditor.hitanimation;
            lblTrapAnimation.Text = Strings.SpellEditor.TrapAnimation;
            chkBound.Text = Strings.SpellEditor.bound;

            grpRequirements.Text = Strings.SpellEditor.requirements;
            lblCannotCast.Text = Strings.SpellEditor.cannotcast;
            btnDynamicRequirements.Text = Strings.SpellEditor.requirementsbutton;

            grpSpellCost.Text = Strings.SpellEditor.cost;
            lblHPCost.Text = Strings.SpellEditor.hpcost;
            lblMPCost.Text = Strings.SpellEditor.manacost;
            lblCastDuration.Text = Strings.SpellEditor.casttime;
            lblCooldownDuration.Text = Strings.SpellEditor.cooldown;
            lblCooldownGroup.Text = Strings.SpellEditor.CooldownGroup;
            chkIgnoreGlobalCooldown.Text = Strings.SpellEditor.IgnoreGlobalCooldown;
            chkIgnoreCdr.Text = Strings.SpellEditor.IgnoreCooldownReduction;

            grpTargetInfo.Text = Strings.SpellEditor.targetting;
            lblTargetType.Text = Strings.SpellEditor.targettype;
            cmbTargetType.Items.Clear();
            for (var i = 0; i < Strings.SpellEditor.targettypes.Count; i++)
            {
                cmbTargetType.Items.Add(Strings.SpellEditor.targettypes[i]);
            }

            lblCastRange.Text = Strings.SpellEditor.castrange;
            lblProjectile.Text = Strings.SpellEditor.projectile;
            lblHitRadius.Text = Strings.SpellEditor.hitradius;
            lblDuration.Text = Strings.SpellEditor.duration;

            grpCombat.Text = Strings.SpellEditor.combatspell;
            grpDamage.Text = Strings.SpellEditor.damagegroup;
            lblCritChance.Text = Strings.SpellEditor.critchance;
            lblCritMultiplier.Text = Strings.SpellEditor.critmultiplier;
            lblDamageType.Text = Strings.SpellEditor.damagetype;
            lblManaDamage.Text = Strings.SpellEditor.mpdamage;
            chkFriendly.Text = Strings.SpellEditor.friendly;
            chkInheritStats.Text = Strings.SpellEditor.inherit;
            cmbDamageType.Items.Clear();
            for (var i = 0; i < Strings.Combat.damagetypes.Count; i++)
            {
                cmbDamageType.Items.Add(Strings.Combat.damagetypes[i]);
            }

            grpHotDot.Text = Strings.SpellEditor.hotdot;
            chkHOTDOT.Text = Strings.SpellEditor.ishotdot;
            lblTick.Text = Strings.SpellEditor.hotdottick;
            lblOTanimationDisclaimer.Text = Strings.SpellEditor.overTimeDisclaimer1 + Strings.General.none + Strings.SpellEditor.overTimeDisclaimer2;

            grpStats.Text = Strings.SpellEditor.stats;

            darkComboBox1.Items.Clear();
            darkComboBox1.Items.AddRange(Strings.ItemEditor.rarity.Values.ToArray());

            grpEffectDuration.Text = Strings.SpellEditor.boostduration;
            lblBuffDuration.Text = Strings.SpellEditor.duration;
            grpEffect.Text = Strings.SpellEditor.effectgroup;
            lblEffect.Text = Strings.SpellEditor.effectlabel;
            cmbExtraEffect.Items.Clear();
            cmbExtraEffect.Items.AddRange(EnumExtensions.GetDescriptions(typeof(StatusTypes)));

            lblSprite.Text = Strings.SpellEditor.transformsprite;

            grpDash.Text = Strings.SpellEditor.dash;
            lblRange.Text = Strings.SpellEditor.dashrange.ToString(scrlRange.Value);
            grpDashCollisions.Text = Strings.SpellEditor.dashcollisions;
            chkIgnoreMapBlocks.Text = Strings.SpellEditor.ignoreblocks;
            chkIgnoreActiveResources.Text = Strings.SpellEditor.ignoreactiveresources;
            chkIgnoreInactiveResources.Text = Strings.SpellEditor.ignoreinactiveresources;
            chkIgnoreZDimensionBlocks.Text = Strings.SpellEditor.ignorezdimension;

            grpWarp.Text = Strings.SpellEditor.warptomap;
            lblMap.Text = Strings.Warping.map.ToString("");
            lblX.Text = Strings.Warping.x.ToString("");
            lblY.Text = Strings.Warping.y.ToString("");
            lblWarpDir.Text = Strings.Warping.direction.ToString("");
            cmbDirection.Items.Clear();
            for (var i = -1; i < 4; i++)
            {
                cmbDirection.Items.Add(Strings.Directions.dir[i]);
            }

            btnVisualMapSelector.Text = Strings.Warping.visual;

            grpEvent.Text = Strings.SpellEditor.Event;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.SpellEditor.sortalphabetically;
            txtSearch.Text = Strings.SpellEditor.searchplaceholder;
            lblFolder.Text = Strings.SpellEditor.folderlabel;

            btnSave.Text = Strings.SpellEditor.save;
            btnCancel.Text = Strings.SpellEditor.cancel;
        }

        private void UpdateEditor()
        {
            mPopulating = true;
            if (mEditorItem != null)
            {
                pnlContainer.Show();
                grpComponents.Show();
                grpSpellGroup.Show();

                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                txtDesc.Text = mEditorItem.Description;
                cmbType.SelectedIndex = (int) mEditorItem.SpellType;

                nudCastDuration.Value = mEditorItem.CastDuration;
                nudCooldownDuration.Value = mEditorItem.CooldownDuration;
                cmbCooldownGroup.SelectedItem = mEditorItem.CooldownGroup;
                chkIgnoreGlobalCooldown.Checked = mEditorItem.IgnoreGlobalCooldown;
                chkIgnoreCdr.Checked = mEditorItem.IgnoreCooldownReduction;

                cmbCastAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.CastAnimationId) + 1;
                cmbHitAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.HitAnimationId) + 1;
                cmbOverTimeAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.OverTimeAnimationId) + 1;
                cmbTrapAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.TrapAnimationId) + 1;
                cmbDashAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.Dash.DashAnimationId) + 1;

                chkBound.Checked = mEditorItem.Bound;

                cmbSprite.SelectedIndex = cmbSprite.FindString(TextUtils.NullToNone(mEditorItem.Icon));
                picSpell.BackgroundImage?.Dispose();
                picSpell.BackgroundImage = null;
                if (cmbSprite.SelectedIndex > 0)
                {
                    picSpell.BackgroundImage = Image.FromFile("resources/spells/" + cmbSprite.Text);
                }

                nudHPCost.Value = mEditorItem.VitalCost[(int) Vitals.Health];
                nudMpCost.Value = mEditorItem.VitalCost[(int) Vitals.Mana];

                txtCannotCast.Text = mEditorItem.CannotCastMessage;

                if (string.IsNullOrEmpty(mEditorItem.SpellGroup))
                {
                    cmbSpellGroup.Text = string.Empty;
                    cmbSpellGroup.SelectedIndex = -1;
                    mEditorItem.SpellGroup = string.Empty;
                }
                else
                {
                    cmbSpellGroup.SelectedItem = mEditorItem.SpellGroup;
                }

                nudSkillPoints.Value = mEditorItem.RequiredSkillPoints;

                nudInterruptThreshold.Value = mEditorItem.InterruptThreshold;

                UpdateSpellTypePanels();

                RefreshBonusEffects(true);

                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }

                cmbSpellUpgrade.Items.Clear();
                cmbSpellUpgrade.Items.Add(Strings.General.none);
                cmbSpellUpgrade.Items.AddRange(SpellBase.Names);
                cmbSpellUpgrade.SelectedIndex = SpellBase.ListIndex(mEditorItem.UpgradeOfSpellId) + 1;

                RefreshComponentsList();
                RefreshBalance();
            }
            else
            {
                pnlContainer.Hide();
            }

            UpdateToolStripItems();
            mPopulating = false;
            PrevSpellType = mEditorItem?.SpellType ?? null;
        }

        private void RefreshBonusEffects(bool clear)
        {
            var selectedIndex = lstBonusEffects.SelectedIndex;
            lstBonusEffects.Items.Clear();
            foreach (EffectType effect in Enum.GetValues(typeof(EffectType)))
            {
                if (effect == EffectType.None)
                {
                    continue;
                }
                var description = effect.GetDescription();
                var amount = mEditorItem.GetBonusEffectPercentage(effect);
                lstBonusEffects.Items.Add($"{description}: {amount}%");
            }
            if (clear)
            {
                lstBonusEffects.SelectedIndex = -1;
            }
            else if (selectedIndex < lstBonusEffects.Items.Count)
            {
                lstBonusEffects.SelectedIndex = selectedIndex;
            }
        }

        private void RefreshComponentsList()
        {
            lstComponents.Items.Clear();
            lstComponents.Items.AddRange(mEditorItem.GetComponentDisplay());
        }

        private void UpdateSpellTypePanels()
        {
            if (PrevSpellType == null || PrevSpellType.GetValueOrDefault() != mEditorItem.SpellType)
            {
                grpTargetInfo.Hide();
                grpCombat.Hide();
                grpWarp.Hide();
                grpDash.Hide();
                grpEvent.Hide();
                grpBonusEffects.Hide();
                cmbTargetType.Enabled = true;
            }

            if (cmbType.SelectedIndex == (int) SpellTypes.CombatSpell ||
                cmbType.SelectedIndex == (int) SpellTypes.WarpTo ||
                cmbType.SelectedIndex == (int) SpellTypes.Passive)
            {
                nudHPCost.Enabled = true;
                nudMpCost.Enabled = true;
                nudCooldownDuration.Enabled = true;
                nudCastDuration.Enabled = true;
                cmbCooldownGroup.Enabled = true;
                chkIgnoreGlobalCooldown.Enabled = true;
                chkIgnoreCdr.Enabled = true;

                lblCastAnimation.Text = Strings.SpellEditor.castanimation;
                lblHitAnimation.Text = Strings.SpellEditor.hitanimation;

                grpTargetInfo.Show();
                grpCombat.Show();
                grpDamage.Show();
                grpHotDot.Show();
                grpDamageTypes.Show();
                grpEffect.Show();
                cmbTargetType.SelectedIndex = (int) mEditorItem.Combat.TargetType;
                UpdateTargetTypePanel();

                nudHPDamage.Value = mEditorItem.Combat.VitalDiff[(int) Vitals.Health];
                nudMPDamage.Value = mEditorItem.Combat.VitalDiff[(int) Vitals.Mana];

                nudStr.Value = mEditorItem.Combat.StatDiff[(int) Stats.Attack];
                nudDef.Value = mEditorItem.Combat.StatDiff[(int) Stats.Defense];
                nudSpd.Value = mEditorItem.Combat.StatDiff[(int) Stats.Speed];
                nudMag.Value = mEditorItem.Combat.StatDiff[(int) Stats.AbilityPower];
                nudMR.Value = mEditorItem.Combat.StatDiff[(int) Stats.MagicResist];
                nudSlash.Value = mEditorItem.Combat.StatDiff[(int)Stats.SlashAttack];
                nudSlashResist.Value = mEditorItem.Combat.StatDiff[(int)Stats.SlashResistance];
                nudPierce.Value = mEditorItem.Combat.StatDiff[(int)Stats.PierceAttack];
                nudPierceResist.Value = mEditorItem.Combat.StatDiff[(int)Stats.PierceResistance];
                nudAccuracy.Value = mEditorItem.Combat.StatDiff[(int)Stats.Accuracy];
                nudEvasion.Value = mEditorItem.Combat.StatDiff[(int)Stats.Evasion];

                nudStrPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int) Stats.Attack];
                nudDefPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int) Stats.Defense];
                nudMagPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int) Stats.AbilityPower];
                nudMRPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int) Stats.MagicResist];
                nudSpdPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int) Stats.Speed];
                nudSlashPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int)Stats.SlashAttack];
                nudSlashResistPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int)Stats.SlashResistance];
                nudPiercePercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int)Stats.PierceAttack];
                nudPierceResistPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int)Stats.PierceResistance];
                nudAccuracyPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int)Stats.Accuracy];
                nudEvasionPercentage.Value = mEditorItem.Combat.PercentageStatDiff[(int)Stats.Evasion];

                chkFriendly.Checked = Convert.ToBoolean(mEditorItem.Combat.Friendly);
                chkInheritStats.Checked = Convert.ToBoolean(mEditorItem.WeaponSpell);
                cmbDamageType.SelectedIndex = mEditorItem.Combat.DamageType;
                nudScaling.Value = mEditorItem.Combat.Scaling;
                nudCritChance.Value = mEditorItem.Combat.CritChance;
                nudCritMultiplier.Value = (decimal) mEditorItem.Combat.CritMultiplier;

                chkHOTDOT.Checked = mEditorItem.Combat.HoTDoT;
                nudBuffDuration.Value = mEditorItem.Combat.Duration;
                nudTick.Value = mEditorItem.Combat.HotDotInterval;
                cmbExtraEffect.SelectedIndex = (int) mEditorItem.Combat.Effect;

                chkLifesteal.Checked = mEditorItem.Combat.LifeSteal;
                chkManaSteal.Checked = mEditorItem.Combat.ManaSteal;

                cmbExtraEffect_SelectedIndexChanged(null, null);

                PopulateDamageTypes();
                RefreshStaticDamageOptions();

                if (cmbType.SelectedIndex == (int)SpellTypes.Passive) 
                {
                    lblCastAnimation.Text = "Prep. Anim:";
                    lblHitAnimation.Text = "Active Anim.:";
                    nudHPCost.Enabled = false;
                    nudMpCost.Enabled = false;
                    nudCooldownDuration.Enabled = false;
                    nudCastDuration.Enabled = false;
                    cmbCooldownGroup.Enabled = false;
                    chkIgnoreGlobalCooldown.Enabled = false;
                    chkIgnoreCdr.Enabled = false;

                    grpDamage.Hide();
                    grpHotDot.Hide();
                    grpEffect.Hide();
                    grpBonusEffects.Show();
                    grpDamageTypes.Hide();
                }
            }
            else if (cmbType.SelectedIndex == (int) SpellTypes.Warp)
            {
                grpWarp.Show();
                for (var i = 0; i < MapList.OrderedMaps.Count; i++)
                {
                    if (MapList.OrderedMaps[i].MapId == mEditorItem.Warp.MapId)
                    {
                        cmbWarpMap.SelectedIndex = i;

                        break;
                    }
                }

                nudWarpX.Value = mEditorItem.Warp.X;
                nudWarpY.Value = mEditorItem.Warp.Y;
                cmbDirection.SelectedIndex = mEditorItem.Warp.Dir;
            }
            else if (cmbType.SelectedIndex == (int) SpellTypes.Dash)
            {
                cmbDashSpell.Items.Clear();
                cmbDashSpell.Items.Add(Strings.General.none);
                cmbDashSpell.Items.AddRange(SpellBase.Names);
                if (mEditorItem.Dash.Spell != null)
                {
                    var idx = SpellBase.ListIndex(mEditorItem.Dash.SpellId) + 1;
                    if (idx < cmbDashSpell.Items.Count)
                    {
                        cmbDashSpell.SelectedIndex = idx;
                    }
                    else
                    {
                        cmbDashSpell.SelectedIndex = 0;
                    }
                }
                else
                {
                    cmbDashSpell.SelectedIndex = 0;
                }

                grpDash.Show();
                scrlRange.Value = mEditorItem.Combat.CastRange;
                lblRange.Text = Strings.SpellEditor.dashrange.ToString(scrlRange.Value);
                chkIgnoreMapBlocks.Checked = mEditorItem.Dash.IgnoreMapBlocks;
                chkIgnoreActiveResources.Checked = mEditorItem.Dash.IgnoreActiveResources;
                chkIgnoreInactiveResources.Checked = mEditorItem.Dash.IgnoreInactiveResources;
                chkIgnoreZDimensionBlocks.Checked = mEditorItem.Dash.IgnoreZDimensionAttributes;
                chkEntities.Checked = mEditorItem.Dash.IgnoreEntites;
            }

            if (cmbType.SelectedIndex == (int) SpellTypes.Event)
            {
                grpEvent.Show();
                cmbEvent.SelectedIndex = EventBase.ListIndex(mEditorItem.EventId) + 1;
            }

            if (cmbType.SelectedIndex == (int) SpellTypes.WarpTo)
            {
                grpTargetInfo.Show();
                cmbTargetType.SelectedIndex = (int) SpellTargetTypes.Single;
                cmbTargetType.Enabled = false;
                UpdateTargetTypePanel();
            }
        }

        private void UpdateTargetTypePanel()
        {
            lblHitRadius.Hide();
            nudHitRadius.Hide();
            lblCastRange.Hide();
            nudCastRange.Hide();
            lblProjectile.Hide();
            cmbProjectile.Hide();
            lblDuration.Hide();
            nudDuration.Hide();
            chkPersistMissedAttack.Hide();
            chkPersistSwap.Hide();
            lblTrapAnimation.Hide();
            cmbTrapAnimation.Hide();

            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.Single)
            {
                lblCastRange.Show();
                nudCastRange.Show();
                nudCastRange.Value = mEditorItem.Combat.CastRange;
                if (cmbType.SelectedIndex == (int) SpellTypes.CombatSpell)
                {
                    lblHitRadius.Show();
                    nudHitRadius.Show();
                    nudHitRadius.Value = mEditorItem.Combat.HitRadius;
                }
            }

            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.AoE &&
                cmbType.SelectedIndex == (int) SpellTypes.CombatSpell)
            {
                lblHitRadius.Show();
                nudHitRadius.Show();
                nudHitRadius.Value = mEditorItem.Combat.HitRadius;
            }

            if (cmbTargetType.SelectedIndex < (int) SpellTargetTypes.Self)
            {
                lblCastRange.Show();
                nudCastRange.Show();
                nudCastRange.Value = mEditorItem.Combat.CastRange;
            }

            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.Projectile)
            {
                lblProjectile.Show();
                cmbProjectile.Show();
                cmbProjectile.SelectedIndex = ProjectileBase.ListIndex(mEditorItem.Combat.ProjectileId);
                lblTrapAnimation.Show();
                cmbTrapAnimation.Show();
                lblTrapAnimation.Text = Strings.SpellEditor.SpawnerAnimation;
            }

            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.OnHit)
            {
                lblDuration.Show();
                nudDuration.Show();
                nudDuration.Value = mEditorItem.Combat.OnHitDuration;
                chkPersistMissedAttack.Show();
                chkPersistSwap.Show();
                chkPersistMissedAttack.Checked = mEditorItem.Combat.PersistMissedAttack;
                chkPersistSwap.Checked = mEditorItem.Combat.PersistWeaponSwap;
            }

            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.Trap)
            {
                lblDuration.Show();
                nudDuration.Show();
                nudDuration.Value = mEditorItem.Combat.TrapDuration;
                nudHitRadius.Show();
                lblHitRadius.Show();
                lblTrapAnimation.Show();
                cmbTrapAnimation.Show();
                cmbTrapAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.TrapAnimationId) + 1;
                lblTrapAnimation.Text = Strings.SpellEditor.TrapAnimation;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex != (int) mEditorItem.SpellType)
            {
                mEditorItem.SpellType = (SpellTypes) cmbType.SelectedIndex;
                UpdateSpellTypePanels();
            }
        }

        private void cmbSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Icon = cmbSprite.Text;
            picSpell.BackgroundImage?.Dispose();
            picSpell.BackgroundImage = null;
            picSpell.BackgroundImage = cmbSprite.SelectedIndex > 0
                ? Image.FromFile("resources/spells/" + cmbSprite.Text)
                : null;
        }

        private void cmbTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.TargetType = (SpellTargetTypes) cmbTargetType.SelectedIndex;
            UpdateTargetTypePanel();
        }

        private void chkHOTDOT_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.HoTDoT = chkHOTDOT.Checked;
        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Description = txtDesc.Text;
        }

        private void cmbExtraEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.Effect = (StatusTypes) cmbExtraEffect.SelectedIndex;

            lblSprite.Visible = false;
            cmbTransform.Visible = false;
            picSprite.Visible = false;

            if (cmbExtraEffect.SelectedIndex == 6) //Transform
            {
                lblSprite.Visible = true;
                cmbTransform.Visible = true;
                picSprite.Visible = true;

                cmbTransform.SelectedIndex =
                    cmbTransform.FindString(TextUtils.NullToNone(mEditorItem.Combat.TransformSprite));

                if (cmbTransform.SelectedIndex > 0)
                {
                    var bmp = new Bitmap(picSprite.Width, picSprite.Height);
                    var g = Graphics.FromImage(bmp);
                    var src = Image.FromFile("resources/entities/" + cmbTransform.Text);
                    g.DrawImage(
                        src,
                        new Rectangle(
                            picSprite.Width / 2 - src.Width / (Options.Instance.Sprites.NormalFrames * 2), picSprite.Height / 2 - src.Height / (Options.Instance.Sprites.Directions * 2), src.Width / Options.Instance.Sprites.NormalFrames,
                            src.Height / Options.Instance.Sprites.Directions
                        ), new Rectangle(0, 0, src.Width / Options.Instance.Sprites.NormalFrames, src.Height / Options.Instance.Sprites.Directions), GraphicsUnit.Pixel
                    );

                    g.Dispose();
                    src.Dispose();
                    picSprite.BackgroundImage = bmp;
                }
                else
                {
                    picSprite.BackgroundImage = null;
                }
            }
        }

        private void frmSpell_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.CurrentEditor = -1;
        }

        private void scrlRange_Scroll(object sender, ScrollValueEventArgs e)
        {
            lblRange.Text = Strings.SpellEditor.dashrange.ToString(scrlRange.Value);
            mEditorItem.Combat.CastRange = scrlRange.Value;
        }

        private void chkIgnoreMapBlocks_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.IgnoreMapBlocks = chkIgnoreMapBlocks.Checked;
        }

        private void chkIgnoreActiveResources_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.IgnoreActiveResources = chkIgnoreActiveResources.Checked;
        }

        private void chkIgnoreInactiveResources_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.IgnoreInactiveResources = chkIgnoreInactiveResources.Checked;
        }

        private void chkIgnoreZDimensionBlocks_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.IgnoreZDimensionAttributes = chkIgnoreZDimensionBlocks.Checked;
        }

        private void cmbTransform_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.TransformSprite = cmbTransform.Text;
            if (cmbTransform.SelectedIndex > 0)
            {
                var bmp = new Bitmap(picSprite.Width, picSprite.Height);
                var g = Graphics.FromImage(bmp);
                var src = Image.FromFile("resources/entities/" + cmbTransform.Text);
                g.DrawImage(
                    src,
                    new Rectangle(
                        picSprite.Width / 2 - src.Width / (Options.Instance.Sprites.NormalFrames * 2), picSprite.Height / 2 - src.Height / (Options.Instance.Sprites.Directions * 2), src.Width / Options.Instance.Sprites.NormalFrames,
                        src.Height / Options.Instance.Sprites.Directions
                    ), new Rectangle(0, 0, src.Width / Options.Instance.Sprites.NormalFrames, src.Height / Options.Instance.Sprites.Directions), GraphicsUnit.Pixel
                );

                g.Dispose();
                src.Dispose();
                picSprite.BackgroundImage = bmp;
            }
            else
            {
                picSprite.BackgroundImage = null;
            }
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.Spell);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.SpellEditor.deleteprompt, Strings.SpellEditor.deletetitle, DarkDialogButton.YesNo,
                        Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    PacketSender.SendDeleteObject(mEditorItem);
                }
            }
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                mCopiedItem = mEditorItem.JsonData;
                toolStripItemPaste.Enabled = true;
            }
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused)
            {
                mEditorItem.Load(mCopiedItem, true);
                UpdateEditor();
            }
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            if (mChanged.Contains(mEditorItem) && mEditorItem != null)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.SpellEditor.undoprompt, Strings.SpellEditor.undotitle, DarkDialogButton.YesNo,
                        Properties.Resources.Icon
                    ) ==
                    DialogResult.Yes)
                {
                    mEditorItem.RestoreBackup();
                    UpdateEditor();
                }
            }
        }

        private void UpdateToolStripItems()
        {
            toolStripItemCopy.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemPaste.Enabled = mEditorItem != null && mCopiedItem != null && lstGameObjects.Focused;
            toolStripItemDelete.Enabled = mEditorItem != null && lstGameObjects.Focused;
            toolStripItemUndo.Enabled = mEditorItem != null && lstGameObjects.Focused;
        }

        private void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.N)
                {
                    toolStripItemNew_Click(null, null);
                }
            }
        }

        private void chkFriendly_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.Friendly = chkFriendly.Checked;
        }

        private void cmbDamageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.DamageType = cmbDamageType.SelectedIndex;
        }

        private void btnDynamicRequirements_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(mEditorItem.CastingRequirements, RequirementType.Spell);
            frm.ShowDialog();
        }

        private void cmbCastAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CastAnimation = AnimationBase.Get(AnimationBase.IdFromList(cmbCastAnimation.SelectedIndex - 1));
        }

        private void cmbHitAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.HitAnimation = AnimationBase.Get(AnimationBase.IdFromList(cmbHitAnimation.SelectedIndex - 1));
        }

        private void cmbProjectile_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.ProjectileId = ProjectileBase.IdFromList(cmbProjectile.SelectedIndex);
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.EventId = EventBase.IdFromList(cmbEvent.SelectedIndex - 1);
        }

        private void btnVisualMapSelector_Click(object sender, EventArgs e)
        {
            var frmWarpSelection = new FrmWarpSelection();
            frmWarpSelection.SelectTile(
                MapList.OrderedMaps[cmbWarpMap.SelectedIndex].MapId, (int) nudWarpX.Value, (int) nudWarpY.Value
            );

            frmWarpSelection.ShowDialog();
            if (frmWarpSelection.GetResult())
            {
                for (var i = 0; i < MapList.OrderedMaps.Count; i++)
                {
                    if (MapList.OrderedMaps[i].MapId == frmWarpSelection.GetMap())
                    {
                        cmbWarpMap.SelectedIndex = i;
                        mEditorItem.Warp.MapId = MapList.OrderedMaps[i].MapId;

                        break;
                    }
                }

                nudWarpX.Value = frmWarpSelection.GetX();
                mEditorItem.Warp.X = frmWarpSelection.GetX();
                nudWarpY.Value = frmWarpSelection.GetY();
                mEditorItem.Warp.Y = frmWarpSelection.GetY();
            }
        }

        private void cmbWarpMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWarpMap.SelectedIndex > -1 && mEditorItem != null)
            {
                mEditorItem.Warp.MapId = MapList.OrderedMaps[cmbWarpMap.SelectedIndex].MapId;
            }
        }

        private void nudWarpX_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Warp.X = (byte) nudWarpX.Value;
        }

        private void nudWarpY_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Warp.Y = (byte) nudWarpY.Value;
        }

        private void cmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Warp.Dir = (byte) cmbDirection.SelectedIndex;
        }

        private void nudCastDuration_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CastDuration = (int) nudCastDuration.Value;
            RefreshBalance();
        }

        private void nudCooldownDuration_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CooldownDuration = (int) nudCooldownDuration.Value;
            RefreshBalance();
        }

        private void nudHitRadius_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.HitRadius = (int) nudHitRadius.Value;
        }

        private void nudHPCost_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalCost[(int) Vitals.Health] = (int) nudHPCost.Value;
        }

        private void nudMpCost_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalCost[(int) Vitals.Mana] = (int) nudMpCost.Value;
        }

        private void nudHPDamage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.VitalDiff[(int) Vitals.Health] = (int) nudHPDamage.Value;
        }

        private void nudMPDamage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.VitalDiff[(int) Vitals.Mana] = (int) nudMPDamage.Value;
        }

        private void nudStr_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int) Stats.Attack] = (int) nudStr.Value;
        }

        private void nudMag_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int) Stats.AbilityPower] = (int) nudMag.Value;
        }

        private void nudDef_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int) Stats.Defense] = (int) nudDef.Value;
        }

        private void nudMR_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int) Stats.MagicResist] = (int) nudMR.Value;
        }

        private void nudSpd_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int) Stats.Speed] = (int) nudSpd.Value;
        }

        private void nudStrPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int) Stats.Attack] = (int) nudStrPercentage.Value;
        }

        private void nudMagPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int) Stats.AbilityPower] = (int) nudMagPercentage.Value;
        }

        private void nudDefPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int) Stats.Defense] = (int) nudDefPercentage.Value;
        }

        private void nudMRPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int) Stats.MagicResist] = (int) nudMRPercentage.Value;
        }

        private void nudSpdPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int) Stats.Speed] = (int) nudSpdPercentage.Value;
        }

        private void nudBuffDuration_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.Duration = (int) nudBuffDuration.Value;
        }

        private void nudTick_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.HotDotInterval = (int) nudTick.Value;
        }

        private void nudCritChance_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.CritChance = (int) nudCritChance.Value;
        }

        private void nudScaling_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.Scaling = (int) nudScaling.Value;
            RefreshBalance();
        }

        private void nudCastRange_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.CastRange = (int) nudCastRange.Value;
        }

        private void nudCritMultiplier_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.CritMultiplier = (double) nudCritMultiplier.Value;
        }

        private void nudOnHitDuration_ValueChanged(object sender, EventArgs e)
        {
            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.OnHit)
            {
                mEditorItem.Combat.OnHitDuration = (int) nudDuration.Value;
            }

            if (cmbTargetType.SelectedIndex == (int) SpellTargetTypes.Trap)
            {
                mEditorItem.Combat.TrapDuration = (int) nudDuration.Value;
            }
        }

        private void chkBound_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Bound = chkBound.Checked;
        }

        private void btnAddCooldownGroup_Click(object sender, EventArgs e)
        {
            var cdGroupName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.SpellEditor.CooldownGroupPrompt, Strings.SpellEditor.CooldownGroupTitle, ref cdGroupName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(cdGroupName))
            {
                if (!cmbCooldownGroup.Items.Contains(cdGroupName))
                {
                    mEditorItem.CooldownGroup = cdGroupName;
                    mKnownCooldownGroups.Add(cdGroupName);
                    InitEditor();
                    cmbCooldownGroup.Text = cdGroupName;
                }
            }
        }

        private void cmbCooldownGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.CooldownGroup = cmbCooldownGroup.Text;
        }

        private void chkIgnoreGlobalCooldown_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IgnoreGlobalCooldown = chkIgnoreGlobalCooldown.Checked;
        }

        private void chkIgnoreCdr_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IgnoreCooldownReduction = chkIgnoreCdr.Checked;
        }

        private void txtCannotCast_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.CannotCastMessage = txtCannotCast.Text;
        }

        #region "Item List - Folders, Searching, Sorting, Etc"

        public void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            mKnownSpellGroups.Clear();
            cmbSpellGroup.Items.Clear();
            foreach (var itm in SpellBase.Lookup)
            {
                var spell = (SpellBase)itm.Value;
                if (!string.IsNullOrEmpty(spell.Folder) &&
                    !mFolders.Contains(spell.Folder))
                {
                    mFolders.Add(spell.Folder);
                    if (!mKnownFolders.Contains(spell.Folder))
                    {
                        mKnownFolders.Add(spell.Folder);
                    }
                }

                if (!string.IsNullOrWhiteSpace(spell.CooldownGroup) &&
                    !mKnownCooldownGroups.Contains(spell.CooldownGroup))
                {
                    mKnownCooldownGroups.Add(spell.CooldownGroup);
                }

                if (!string.IsNullOrWhiteSpace(spell.SpellGroup) &&
                    !mKnownSpellGroups.Contains(spell.SpellGroup))
                {
                    mKnownSpellGroups.Add(spell.SpellGroup);
                    cmbSpellGroup.Items.Add(spell.SpellGroup);
                }
            }

            // Do we add item cooldown groups as well?
            if (Options.Combat.LinkSpellAndItemCooldowns)
            {
                foreach (var itm in ItemBase.Lookup)
                {
                    if (!string.IsNullOrWhiteSpace(((ItemBase)itm.Value).CooldownGroup) &&
                    !mKnownCooldownGroups.Contains(((ItemBase)itm.Value).CooldownGroup))
                    {
                        mKnownCooldownGroups.Add(((ItemBase)itm.Value).CooldownGroup);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            mKnownCooldownGroups.Sort();
            cmbCooldownGroup.Items.Clear();
            cmbCooldownGroup.Items.Add(string.Empty);
            cmbCooldownGroup.Items.AddRange(mKnownCooldownGroups.ToArray());

            var items = SpellBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((SpellBase)pair.Value)?.Name ?? Models.DatabaseObject<SpellBase>.Deleted, ((SpellBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);

            cmbComponents.Items.Clear();
            cmbComponents.Items.AddRange(ItemBase.Names);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.SpellEditor.folderprompt, Strings.SpellEditor.foldertitle, ref folderName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
            {
                if (!cmbFolder.Items.Contains(folderName))
                {
                    mEditorItem.Folder = folderName;
                    lstGameObjects.ExpandFolder(folderName);
                    InitEditor();
                    cmbFolder.Text = folderName;
                }
            }
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Folder = cmbFolder.Text;
            InitEditor();
        }

        private void btnAlphabetical_Click(object sender, EventArgs e)
        {
            btnAlphabetical.Checked = !btnAlphabetical.Checked;
            InitEditor();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            InitEditor();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = Strings.SpellEditor.searchplaceholder;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.SpellEditor.searchplaceholder;
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) &&
                   txtSearch.Text != Strings.SpellEditor.searchplaceholder;
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == Strings.SpellEditor.searchplaceholder)
            {
                txtSearch.SelectAll();
            }
        }

        #endregion

        private void cmbOverTimeAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid animationId = AnimationBase.IdFromList(cmbOverTimeAnimation.SelectedIndex - 1);
            mEditorItem.OverTimeAnimation = AnimationBase.Get(animationId);
        }

        private void chkInheritStats_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.WeaponSpell = chkInheritStats.Checked;
        }

        private void cmbTrapAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid animationId = AnimationBase.IdFromList(cmbTrapAnimation.SelectedIndex - 1);
            mEditorItem.TrapAnimation = AnimationBase.Get(animationId);
        }

        private void chkEntities_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.IgnoreEntites = chkEntities.Checked;
        }

        private void cmbDashSpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.SpellId = SpellBase.IdFromList(cmbDashSpell.SelectedIndex - 1);
            Console.Write(mEditorItem.Dash.SpellId);
        }

        private void btnAddComponent_Click(object sender, EventArgs e)
        {
            var componentId = ItemBase.IdFromList(cmbComponents.SelectedIndex);
            var componentQuantity = (int)nudComponentQuantity.Value;

            if (componentId == default || componentQuantity == 0)
            {
                return;
            }

            mEditorItem.CastingComponents.Add(new SpellCastingComponent(componentId, componentQuantity));
            RefreshComponentsList();
        }

        private void btnRemoveComponent_Click(object sender, EventArgs e)
        {
            var idx = lstComponents.SelectedIndex;
            if (idx < 0 || idx >= lstComponents.Items.Count)
            {
                return;
            }
            
            lstComponents.Items.RemoveAt(idx);
            mEditorItem.CastingComponents.RemoveAt(idx);
            
            RefreshComponentsList();
        }

        private void lstComponents_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = lstComponents.SelectedIndex;
            if (idx < 0 || idx >= lstComponents.Items.Count || idx >= mEditorItem.CastingComponents.Count)
            {
                return;
            }

            try
            {
                var component = mEditorItem.CastingComponents[idx] ?? null;
                cmbComponents.SelectedIndex = ItemBase.ListIndex(component?.ItemId ?? Guid.Empty);
                nudComponentQuantity.Value = component?.Quantity ?? 1;
            }
            catch (Exception exception)
            {
                return;
            }
        }

        private void PopulateDamageTypes()
        {
            chkDamageBlunt.Checked = false;
            chkDamagePierce.Checked = false;
            chkDamageSlash.Checked = false;
            chkDamageMagic.Checked = false;

            foreach (var type in mEditorItem?.Combat?.DamageTypes)
            {
                switch (type)
                {
                    case AttackTypes.Blunt:
                        chkDamageBlunt.Checked = true;
                        break;
                    case AttackTypes.Slashing:
                        chkDamageSlash.Checked = true;
                        break;
                    case AttackTypes.Magic:
                        chkDamageMagic.Checked = true;
                        break;
                    case AttackTypes.Piercing:
                        chkDamagePierce.Checked = true;
                        break;
                }

            }
        }

        private void AddDamageType(AttackTypes type)
        {
            if (mPopulating)
            {
                return;
            }

            if (mEditorItem.Combat.DamageTypes.Contains(type))
            {
                return;
            }

            mEditorItem.Combat.DamageTypes.Add(type);
            RefreshStaticDamageOptions();
            RefreshBalance();
        }

        private void RemoveDamageType(AttackTypes type)
        {
            if (mPopulating)
            {
                return;
            }

            mEditorItem.Combat.DamageTypes.Remove(type);
            RefreshStaticDamageOptions();
            RefreshBalance();
        }

        private void chkDamageBlunt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamageBlunt.Checked)
            {
                AddDamageType(AttackTypes.Blunt);
            }
            else
            {
                RemoveDamageType(AttackTypes.Blunt);
            }
        }

        private void RefreshStaticDamageOptions() 
        { 
            if (mEditorItem.Combat.DamageTypes.Contains(AttackTypes.Blunt))
            {
                nudBluntDam.Enabled = true;
            }
            else
            {
                mEditorItem.DamageOverrides[(int)AttackTypes.Blunt] = 0;
                nudBluntDam.Enabled = false;
            }

            if (mEditorItem.Combat.DamageTypes.Contains(AttackTypes.Slashing))
            {
                nudSlashDam.Enabled = true;
            }
            else
            {
                mEditorItem.DamageOverrides[(int)AttackTypes.Slashing] = 0;
                nudSlashDam.Enabled = false;
            }

            if (mEditorItem.Combat.DamageTypes.Contains(AttackTypes.Magic))
            {
                nudMagicDam.Enabled = true;
            }
            else
            {
                mEditorItem.DamageOverrides[(int)AttackTypes.Magic] = 0;
                nudMagicDam.Enabled = false;
            }

            if (mEditorItem.Combat.DamageTypes.Contains(AttackTypes.Piercing))
            {
                nudPierceDam.Enabled = true;
            }
            else
            {
                mEditorItem.DamageOverrides[(int)AttackTypes.Piercing] = 0;
                nudPierceDam.Enabled = false;
            }

            RefreshStaticDamageValues();
        }

        private void RefreshStaticDamageValues()
        {
            mEditorItem.DamageOverrides.TryGetValue((int)AttackTypes.Blunt, out var bluntStatic);
            mEditorItem.DamageOverrides.TryGetValue((int)AttackTypes.Slashing, out var slashStatic);
            mEditorItem.DamageOverrides.TryGetValue((int)AttackTypes.Magic, out var magicStatic);
            mEditorItem.DamageOverrides.TryGetValue((int)AttackTypes.Piercing, out var pierceStatic);

            nudBluntDam.Value = bluntStatic;
            nudSlashDam.Value = slashStatic;
            nudMagicDam.Value = magicStatic;
            nudPierceDam.Value = pierceStatic;
        }

        private void chkDamageSlash_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamageSlash.Checked)
            {
                AddDamageType(AttackTypes.Slashing);
            }
            else
            {
                RemoveDamageType(AttackTypes.Slashing);
            }
        }

        private void chkDamagePierce_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamagePierce.Checked)
            {
                AddDamageType(AttackTypes.Piercing);
            }
            else
            {
                RemoveDamageType(AttackTypes.Piercing);
            }
        }

        private void chkDamageMagic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDamageMagic.Checked)
            {
                AddDamageType(AttackTypes.Magic);
            }
            else
            {
                RemoveDamageType(AttackTypes.Magic);
            }
        }

        private void nudSlash_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int)Stats.SlashAttack] = (int)nudSlash.Value;
        }

        private void nudSlashPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int)Stats.SlashAttack] = (int)nudSlashPercentage.Value;
        }

        private void nudSlashResist_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int)Stats.SlashResistance] = (int)nudSlashResist.Value;
        }

        private void nudSlashResistPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int)Stats.SlashResistance] = (int)nudSlashResistPercentage.Value;
        }

        private void nudPierce_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int)Stats.PierceAttack] = (int)nudPierce.Value;
        }

        private void nudPiercePercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int)Stats.PierceAttack] = (int)nudPiercePercentage.Value;
        }

        private void nudPierceResist_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int)Stats.PierceResistance] = (int)nudPierceResist.Value;
        }

        private void nudPierceResistPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int)Stats.PierceResistance] = (int)nudPierceResistPercentage.Value;
        }

        private void nudAccuracy_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int)Stats.Accuracy] = (int)nudAccuracy.Value;
        }

        private void nudAccuracyPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int)Stats.Accuracy] = (int)nudAccuracyPercentage.Value;
        }

        private void nudEvasion_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.StatDiff[(int)Stats.Evasion] = (int)nudEvasion.Value;
        }

        private void nudEvasionPercentage_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PercentageStatDiff[(int)Stats.Evasion] = (int)nudEvasionPercentage.Value;
        }

        private void btnAddSpellGroup_Click(object sender, EventArgs e)
        {
            var groupName = "";
            var result = DarkInputBox.ShowInformation(
                "Enter a name for the spell group:", "New Spell Group", ref groupName,
                DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(groupName))
            {
                if (!cmbSpellGroup.Items.Contains(groupName))
                {
                    mEditorItem.SpellGroup = groupName;
                    cmbSpellGroup.Items.Add(groupName);
                    cmbSpellGroup.SelectedItem = groupName;
                    mKnownSpellGroups.Add(groupName);
                }
            }
        }

        private void cmbSpellGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.SpellGroup = cmbSpellGroup.Text;
        }

        private void btnSetEmpty_Click(object sender, EventArgs e)
        {
            cmbSpellGroup.SelectedIndex = -1;
            cmbSpellGroup.Text = string.Empty;
            mEditorItem.SpellGroup = string.Empty;
        }

        private void nudSkillPoints_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.RequiredSkillPoints = (int)nudSkillPoints.Value;
        }

        private void nudBonusAmt_ValueChanged(object sender, EventArgs e)
        {
            if (lstBonusEffects.SelectedIndex == -1)
            {
                return;
            }

            // Compensate for excluded "None" type
            EffectType selectedEffect = (EffectType)(lstBonusEffects.SelectedIndex + 1);

            mEditorItem.SetBonusEffectOfType(selectedEffect, (int)nudBonusAmt.Value);
            RefreshBonusEffects(false);
        }

        private void nudBluntDam_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.DamageOverrides[(int)Stats.Attack] = (int)nudBluntDam.Value;
            RefreshBalance();
        }

        private void nudPierceDam_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.DamageOverrides[(int)Stats.PierceAttack] = (int)nudPierceDam.Value;
            RefreshBalance();
        }

        private void nudMagicDam_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.DamageOverrides[(int)Stats.AbilityPower] = (int)nudMagicDam.Value;
            RefreshBalance();
        }

        private void nudSlashDam_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.DamageOverrides[(int)Stats.SlashAttack] = (int)nudSlashDam.Value;
            RefreshBalance();
        }

        private void darkComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTier = darkComboBox1.SelectedIndex;
            var tierDamage = (int)Math.Round(CombatUtilities.TierToDamageFormula(CurrentTier));

            nudMockBlunt.Value = tierDamage;
            nudMockMagic.Value = tierDamage;
            nudMockPierce.Value = tierDamage;
            nudMockSlash.Value = tierDamage;
        }

        private void RefreshBalance()
        {
            if (mEditorItem.SpellType != SpellTypes.CombatSpell)
            {
                grpBalanceHelp.Hide();
                return;
            }

            grpBalanceHelp.Show();

            var mockStats = new int[(int)Stats.StatCount];
            mockStats[(int)AttackTypes.Blunt] = (int)nudMockBlunt.Value;
            mockStats[(int)AttackTypes.Slashing] = (int)nudMockSlash.Value;
            mockStats[(int)AttackTypes.Piercing] = (int)nudMockPierce.Value;
            mockStats[(int)AttackTypes.Magic] = (int)nudMockMagic.Value;

            var totalCooldown = Math.Max(mEditorItem.CooldownDuration, mEditorItem.IgnoreGlobalCooldown ? 0 : Options.Combat.GlobalCooldownDuration);

            CombatUtilities.CalculateDamage(mEditorItem.Combat.DamageTypes,
                1.0,
                mEditorItem.Combat.Scaling,
                CombatUtilities.GetOverriddenStats(mEditorItem.DamageOverrides, mockStats),
                new int[(int)Stats.StatCount],
                out int maxHit);
            
            var dps = CombatUtilities.CalculateDps(mEditorItem.Combat.DamageTypes, 
                1.0, 
                mEditorItem.Combat.Scaling, 
                CombatUtilities.GetOverriddenStats(mEditorItem.DamageOverrides, mockStats), 
                new int[(int)Stats.StatCount], 
                mEditorItem.CastDuration + totalCooldown);

            lblProjectedDpsVal.Text = dps.ToString("N2");

            var desiredDps = CombatUtilities.TierToDamageFormula(CurrentTier);
            lblTargetDpsVal.Text = desiredDps.ToString("N2");
            lblMaxHitVal.Text = maxHit.ToString("N0");
        }

        private void nudMockBlunt_ValueChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void nudMockSlash_ValueChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void nudMockPierce_ValueChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void nudMockMagic_ValueChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void chkPersistMissedAttack_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PersistMissedAttack = chkPersistMissedAttack.Checked;
        }

        private void chkPersistSwap_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.PersistWeaponSwap = chkPersistSwap.Checked;
        }

        private void chkLifesteal_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.LifeSteal = chkLifesteal.Checked;
        }

        private void chkManaSteal_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Combat.ManaSteal = chkManaSteal.Checked;
        }

        private void cmbDashAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Dash.DashAnimation = AnimationBase.Get(AnimationBase.IdFromList(cmbDashAnimation.SelectedIndex - 1));
        }

        private void nudInterruptThreshold_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.InterruptThreshold = (int)nudInterruptThreshold.Value;
        }

        private void cmbSpellUpgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.UpgradeOfSpellId = SpellBase.IdFromList(cmbSpellUpgrade.SelectedIndex - 1);
        }
    }
}
