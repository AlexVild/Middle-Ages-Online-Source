using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

using DarkUI.Forms;

using Intersect.Editor.Content;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Networking;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors
{
    public enum TableType
    {
        Main,
        Secondary,
        Tertiary
    }

    public partial class FrmNpc : EditorForm
    {

        private List<NpcBase> mChanged = new List<NpcBase>();

        private string mCopiedItem;

        private NpcBase mEditorItem;

        private List<string> mKnownFolders = new List<string>();

        private bool mDropTypeUpdate = false;

        private TableType CurrentTable = TableType.Main;

        private bool mPopulating = false;

        private int CurrentTier = 0;

        public FrmNpc()
        {
            ApplyHooks();
            InitializeComponent();
            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }
        private void AssignEditorItem(Guid id)
        {
            mEditorItem = NpcBase.Get(id);
            UpdateEditor();
        }

        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            if (type == GameObjectType.Npc)
            {
                InitEditor();
                if (mEditorItem != null && !NpcBase.Lookup.Values.Contains(mEditorItem))
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

        private void frmNpc_Load(object sender, EventArgs e)
        {
            cmbSprite.Items.Clear();
            cmbSprite.Items.Add(Strings.General.none);
            cmbSprite.Items.AddRange(
                GameContentManager.GetSmartSortedTextureNames(GameContentManager.TextureType.Entity)
            );

            cmbSpell.Items.Clear();
            cmbSpell.Items.AddRange(SpellBase.Names);
            cmbHostileNPC.Items.Clear();
            cmbHostileNPC.Items.AddRange(NpcBase.Names);
            FillItemCombobox(DropType.Item);
            cmbAttackAnimation.Items.Clear();
            cmbAttackAnimation.Items.Add(Strings.General.none);
            cmbAttackAnimation.Items.AddRange(AnimationBase.Names);
            cmbOnDeathEventKiller.Items.Clear();
            cmbOnDeathEventKiller.Items.Add(Strings.General.none);
            cmbOnDeathEventKiller.Items.AddRange(EventBase.Names);
            cmbOnDeathEventParty.Items.Clear();
            cmbOnDeathEventParty.Items.Add(Strings.General.none);
            cmbOnDeathEventParty.Items.AddRange(EventBase.Names);

            nudStr.Maximum = Options.MaxNpcStat;
            nudMag.Maximum = Options.MaxNpcStat;
            nudDef.Maximum = Options.MaxNpcStat;
            nudMR.Maximum = Options.MaxNpcStat;
            nudSpd.Maximum = Options.MaxNpcStat;

            cmbDeathAnimation.Items.Clear();
            cmbDeathAnimation.Items.Add(Strings.General.none);
            cmbDeathAnimation.Items.AddRange(AnimationBase.Names);

            cmbTransformIntoNpc.Items.Clear();
            cmbTransformIntoNpc.Items.Add(Strings.General.none);
            cmbTransformIntoNpc.Items.AddRange(NpcBase.Names);

            cmbSpellAttackOverride.Items.Clear();
            cmbSpellAttackOverride.Items.Add(Strings.General.none);
            cmbSpellAttackOverride.Items.AddRange(SpellBase.Names);

            cmbChampion.Items.Clear();
            cmbChampion.Items.Add(Strings.General.none);
            cmbChampion.Items.AddRange(NpcBase.Names);

            cmbScalingType.Items.Clear();
            cmbScalingType.Items.AddRange(EnumExtensions.GetDescriptions(typeof(NpcScaleType)));

            InitLocalization();
            UpdateEditor();
        }

        private void InitLocalization()
        {
            Text = Strings.NpcEditor.title;
            toolStripItemNew.Text = Strings.NpcEditor.New;
            toolStripItemDelete.Text = Strings.NpcEditor.delete;
            toolStripItemCopy.Text = Strings.NpcEditor.copy;
            toolStripItemPaste.Text = Strings.NpcEditor.paste;
            toolStripItemUndo.Text = Strings.NpcEditor.undo;

            grpNpcs.Text = Strings.NpcEditor.npcs;

            grpGeneral.Text = Strings.NpcEditor.general;
            lblName.Text = Strings.NpcEditor.name;
            grpBehavior.Text = Strings.NpcEditor.behavior;

            lblPic.Text = Strings.NpcEditor.sprite;
            lblRed.Text = Strings.NpcEditor.Red;
            lblGreen.Text = Strings.NpcEditor.Green;
            lblBlue.Text = Strings.NpcEditor.Blue;
            lblAlpha.Text = Strings.NpcEditor.Alpha;

            lblSpawnDuration.Text = Strings.NpcEditor.spawnduration;

            //Behavior
            chkAggressive.Text = Strings.NpcEditor.aggressive;
            lblSightRange.Text = Strings.NpcEditor.sightrange;
            lblMovement.Text = Strings.NpcEditor.movement;
            lblResetRadius.Text = Strings.NpcEditor.resetradius;
            cmbMovement.Items.Clear();
            for (var i = 0; i < Strings.NpcEditor.movements.Count; i++)
            {
                cmbMovement.Items.Add(Strings.NpcEditor.movements[i]);
            }

            chkSwarm.Text = Strings.NpcEditor.swarm;
            lblFlee.Text = Strings.NpcEditor.flee;
            grpConditions.Text = Strings.NpcEditor.conditions;
            btnPlayerFriendProtectorCond.Text = Strings.NpcEditor.playerfriendprotectorconditions;
            btnAttackOnSightCond.Text = Strings.NpcEditor.attackonsightconditions;
            btnPlayerCanAttackCond.Text = Strings.NpcEditor.playercanattackconditions;

            grpCommonEvents.Text = Strings.NpcEditor.commonevents;
            lblOnDeathEventKiller.Text = Strings.NpcEditor.ondeathevent;
            lblOnDeathEventParty.Text = Strings.NpcEditor.ondeathpartyevent;

            grpStats.Text = Strings.NpcEditor.stats;
            lblHP.Text = Strings.NpcEditor.hp;
            lblMana.Text = Strings.NpcEditor.mana;
            lblExp.Text = Strings.NpcEditor.exp;

            grpRegen.Text = Strings.NpcEditor.regen;
            lblHpRegen.Text = Strings.NpcEditor.hpregen;
            lblManaRegen.Text = Strings.NpcEditor.mpregen;
            lblRegenHint.Text = Strings.NpcEditor.regenhint;

            grpSpells.Text = Strings.NpcEditor.spells;
            lblSpell.Text = Strings.NpcEditor.spell;
            btnAdd.Text = Strings.NpcEditor.addspell;
            btnRemove.Text = Strings.NpcEditor.removespell;
            lblFreq.Text = Strings.NpcEditor.frequency;
            cmbFreq.Items.Clear();
            for (var i = 0; i < Strings.NpcEditor.frequencies.Count; i++)
            {
                cmbFreq.Items.Add(Strings.NpcEditor.frequencies[i]);
            }

            grpAttackSpeed.Text = Strings.NpcEditor.attackspeed;
            lblAttackSpeedModifier.Text = Strings.NpcEditor.attackspeedmodifier;
            lblAttackSpeedValue.Text = Strings.NpcEditor.attackspeedvalue;
            cmbAttackSpeedModifier.Items.Clear();
            foreach (var val in Strings.NpcEditor.attackspeedmodifiers.Values)
            {
                cmbAttackSpeedModifier.Items.Add(val.ToString());
            }

            grpNpcVsNpc.Text = Strings.NpcEditor.npcvsnpc;
            chkEnabled.Text = Strings.NpcEditor.enabled;
            chkAttackAllies.Text = Strings.NpcEditor.attackallies;
            lblNPC.Text = Strings.NpcEditor.npc;
            btnAddAggro.Text = Strings.NpcEditor.addhostility;
            btnRemoveAggro.Text = Strings.NpcEditor.removehostility;

            grpDrops.Text = Strings.NpcEditor.drops;
            lblDropItem.Text = Strings.NpcEditor.dropitem;
            lblDropAmount.Text = Strings.NpcEditor.dropamount;
            lblDropChance.Text = Strings.NpcEditor.dropchance;
            btnDropAdd.Text = Strings.NpcEditor.dropadd;
            btnDropRemove.Text = Strings.NpcEditor.dropremove;
            chkIndividualLoot.Text = Strings.NpcEditor.individualizedloot;

            grpCombat.Text = Strings.NpcEditor.combat;
            lblCritChance.Text = Strings.NpcEditor.critchance;
            lblCritMultiplier.Text = Strings.NpcEditor.critmultiplier;
            lblAttackAnimation.Text = Strings.NpcEditor.attackanimation;

            //Searching/Sorting
            btnAlphabetical.ToolTipText = Strings.NpcEditor.sortalphabetically;
            txtSearch.Text = Strings.NpcEditor.searchplaceholder;
            lblFolder.Text = Strings.NpcEditor.folderlabel;

            //Immunities
            grpImmunities.Text = Strings.NpcEditor.immunities;
            chkKnockback.Text = Strings.NpcEditor.knockback;
            chkSilence.Text = Strings.NpcEditor.silence;
            chkStun.Text = Strings.NpcEditor.stun;
            chkSnare.Text = Strings.NpcEditor.snare;
            chkBlind.Text = Strings.NpcEditor.blind;
            chkTransform.Text = Strings.NpcEditor.transform;
            chkTaunt.Text = Strings.NpcEditor.taunt;
            chkSleep.Text = Strings.NpcEditor.sleep;
            lblTenacity.Text = Strings.NpcEditor.tenacity;

            //Additional animations
            grpAnimation.Text = Strings.NpcEditor.additionalanimationgroup;
            lblDeathAnimation.Text = Strings.NpcEditor.deathanimation;

            btnSave.Text = Strings.NpcEditor.save;
            btnCancel.Text = Strings.NpcEditor.cancel;

            nudTableChance.Value = 100;
            nudTableChance.Enabled = false;
        }

        private void UpdateEditor()
        {
            mPopulating = true;
            if (mEditorItem != null)
            {
                rdoMain.Checked = true;
                pnlContainer.Show();

                txtName.Text = mEditorItem.Name;
                cmbFolder.Text = mEditorItem.Folder;
                cmbSprite.SelectedIndex = cmbSprite.FindString(TextUtils.NullToNone(mEditorItem.Sprite));
                nudRgbaR.Value = mEditorItem.Color.R;
                nudRgbaG.Value = mEditorItem.Color.G;
                nudRgbaB.Value = mEditorItem.Color.B;
                nudRgbaA.Value = mEditorItem.Color.A;
                nudTier.Value = mEditorItem.Level;

                nudSpawnDuration.Value = mEditorItem.SpawnDuration;

                //Behavior
                chkAggressive.Checked = mEditorItem.Aggressive;
                if (mEditorItem.Aggressive)
                {
                    btnAttackOnSightCond.Text = Strings.NpcEditor.dontattackonsightconditions;
                }
                else
                {
                    btnAttackOnSightCond.Text = Strings.NpcEditor.attackonsightconditions;
                }

                nudSightRange.Value = mEditorItem.SightRange;
                cmbMovement.SelectedIndex = Math.Min(mEditorItem.Movement, cmbMovement.Items.Count - 1);
                chkSwarm.Checked = mEditorItem.Swarm;
                nudFlee.Value = mEditorItem.FleeHealthPercentage;
                chkFocusDamageDealer.Checked = mEditorItem.FocusHighestDamageDealer;
                nudResetRadius.Value = mEditorItem.ResetRadius;
                chkStandStill.Checked = mEditorItem.StandStill;

                //Common Events
                cmbOnDeathEventKiller.SelectedIndex = EventBase.ListIndex(mEditorItem.OnDeathEventId) + 1;
                cmbOnDeathEventParty.SelectedIndex = EventBase.ListIndex(mEditorItem.OnDeathPartyEventId) + 1;

                nudStr.Value = mEditorItem.Stats[(int) Stats.Attack];
                nudMag.Value = mEditorItem.Stats[(int) Stats.AbilityPower];
                nudDef.Value = mEditorItem.Stats[(int) Stats.Defense];
                nudMR.Value = mEditorItem.Stats[(int) Stats.MagicResist];
                nudSpd.Value = mEditorItem.Stats[(int) Stats.Speed];
                nudSlash.Value = mEditorItem.Stats[(int)Stats.SlashAttack];
                nudSlashResist.Value = mEditorItem.Stats[(int)Stats.SlashResistance];
                nudPierce.Value = mEditorItem.Stats[(int)Stats.PierceAttack];
                nudPierceResist.Value = mEditorItem.Stats[(int)Stats.PierceResistance];
                nudAccuracy.Value = mEditorItem.Stats[(int)Stats.Accuracy];
                nudEvasion.Value = mEditorItem.Stats[(int)Stats.Evasion];

                nudHp.Value = mEditorItem.MaxVital[(int) Vitals.Health];
                nudMana.Value = mEditorItem.MaxVital[(int) Vitals.Mana];
                nudExp.Value = mEditorItem.Experience;
                chkAttackAllies.Checked = mEditorItem.AttackAllies;
                chkEnabled.Checked = mEditorItem.NpcVsNpcEnabled;

                PopulateDamageTypes();
                chkSpellCast.Checked = mEditorItem.IsSpellcaster;

                //Combat
                nudCritChance.Value = mEditorItem.CritChance;
                nudCritMultiplier.Value = (decimal) mEditorItem.CritMultiplier;
                cmbAttackAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.AttackAnimationId) + 1;
                cmbAttackSpeedModifier.SelectedIndex = mEditorItem.AttackSpeedModifier;
                nudAttackSpeedValue.Value = mEditorItem.AttackSpeedValue;

                //Regen
                nudHpRegen.Value = mEditorItem.VitalRegen[(int) Vitals.Health];
                nudMpRegen.Value = mEditorItem.VitalRegen[(int) Vitals.Mana];

                //Melee vital steasls
                nudLifesteal.Value = mEditorItem.MeleeLifesteal;
                nudManasteal.Value = mEditorItem.MeleeManasteal;

                // New behavioral stuff
                chkDisableAutoAttack.Checked = mEditorItem.DisableAutoAttack;
                chkDisableExhaustion.Checked = mEditorItem.DisableAutoExhaustion;

                // Add the spells to the list
                lstSpells.Items.Clear();
                for (var i = 0; i < mEditorItem.Spells.Count; i++)
                {
                    if (mEditorItem.Spells[i] != Guid.Empty)
                    {
                        lstSpells.Items.Add(SpellBase.GetName(mEditorItem.Spells[i]));
                    }
                    else
                    {
                        lstSpells.Items.Add(Strings.General.none);
                    }
                }

                if (lstSpells.Items.Count > 0)
                {
                    lstSpells.SelectedIndex = 0;
                    cmbSpell.SelectedIndex = SpellBase.ListIndex(mEditorItem.Spells[lstSpells.SelectedIndex]);
                }

                cmbFreq.SelectedIndex = mEditorItem.SpellFrequency;

                // Add the aggro NPC's to the list
                lstAggro.Items.Clear();
                for (var i = 0; i < mEditorItem.AggroList.Count; i++)
                {
                    if (mEditorItem.AggroList[i] != Guid.Empty)
                    {
                        lstAggro.Items.Add(NpcBase.GetName(mEditorItem.AggroList[i]));
                    }
                    else
                    {
                        lstAggro.Items.Add(Strings.General.none);
                    }
                }

                UpdateDropValues();
                chkIndividualLoot.Checked = mEditorItem.IndividualizedLoot;

                DrawNpcSprite();
                if (mChanged.IndexOf(mEditorItem) == -1)
                {
                    mChanged.Add(mEditorItem);
                    mEditorItem.MakeBackup();
                }

                // Tenacity and immunities
                nudTenacity.Value = (decimal) mEditorItem.Tenacity;

                UpdateImmunities();
                chkImpassable.Checked = mEditorItem.Impassable;
                chkNoBack.Checked = mEditorItem.NoBackstab;
                chkStealth.Checked = mEditorItem.NoStealthBonus;

                cmbDeathAnimation.SelectedIndex = AnimationBase.ListIndex(mEditorItem.DeathAnimationId) + 1;
                cmbTransformIntoNpc.SelectedIndex = NpcBase.ListIndex(mEditorItem.DeathTransformId) + 1;
                cmbSpellAttackOverride.SelectedIndex = SpellBase.ListIndex(mEditorItem.SpellAttackOverrideId) + 1;

                cmbChampion.SelectedIndex = NpcBase.ListIndex(mEditorItem.ChampionId) + 1;
                nudChampCooldown.Value = mEditorItem.ChampionCooldownSeconds;
                nudChampSpawnChance.Value = (decimal)mEditorItem.ChampionSpawnChance;
                chkIsChampion.Checked = mEditorItem.IsChampion;

                RefreshBalancing();

                // Bestiary
                if (mEditorItem.BestiaryUnlocks?.Count == 0)
                {
                    PopulateBestiaryDefaults();
                }
                RefreshBestiaryList(true);
                txtStartDesc.Text = mEditorItem.Description;
                chkBestiary.Checked = mEditorItem.NotInBestiary;

                lblCalcExpVal.Text = $"{NpcExperienceCalculator.Calculate(mEditorItem)} EXP";
                chkCannotHeal.Checked = mEditorItem.CannotBeHealed;

                cmbScalingType.SelectedIndex = (int)mEditorItem.NpcScaleType;
                nudScaledTo.Value = mEditorItem.ScaledTo;
                nudMaxScaledTo.Value = mEditorItem.MaxScaledTo;
                nudScaleFactor.Value = (decimal)mEditorItem.VitalScaleModifier;

                chkPlayerLockLoot.Checked = mEditorItem.PlayerLockedLoot;
                chkNeverSkip.Checked = mEditorItem.NeverSkipSpellCasting;
                chkHideName.Checked = mEditorItem.HideName;
                chkAnimated.Checked = mEditorItem.AnimatedSprite;
                chkSequentialCasting.Checked = mEditorItem.SequentialCasting;
            }
            else
            {
                pnlContainer.Hide();
            }

            UpdateToolStripItems();
            mPopulating = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Name = txtName.Text;
            lstGameObjects.UpdateText(txtName.Text);
        }

        private void cmbSprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Sprite = TextUtils.SanitizeNone(cmbSprite.Text);
            DrawNpcSprite();
        }

        private void DrawNpcSprite()
        {
            var picSpriteBmp = new Bitmap(picNpc.Width, picNpc.Height);
            var gfx = Graphics.FromImage(picSpriteBmp);
            gfx.FillRectangle(Brushes.Black, new Rectangle(0, 0, picNpc.Width, picNpc.Height));
            if (cmbSprite.SelectedIndex > 0)
            {
                var img = Image.FromFile("resources/entities/" + cmbSprite.Text);
                var imgAttributes = new ImageAttributes();

                // Microsoft, what the heck is this crap?
                imgAttributes.SetColorMatrix(
                    new ColorMatrix(
                        new float[][]
                        {
                            new float[] { (float)nudRgbaR.Value / 255,  0,  0,  0, 0},  // Modify the red space
                            new float[] {0, (float)nudRgbaG.Value / 255,  0,  0, 0},    // Modify the green space
                            new float[] {0,  0, (float)nudRgbaB.Value / 255,  0, 0},    // Modify the blue space
                            new float[] {0,  0,  0, (float)nudRgbaA.Value / 255, 0},    // Modify the alpha space
                            new float[] {0, 0, 0, 0, 1}                                 // We're not adding any non-linear changes. Value of 1 at the end is a dummy value!
                        }
                    )
                );

                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gfx.DrawImage(
                    img, new Rectangle(0, 0, img.Width / Options.Instance.Sprites.NormalFrames * 2, img.Height / Options.Instance.Sprites.Directions * 2),
                    0, 0, img.Width / Options.Instance.Sprites.NormalFrames, img.Height / Options.Instance.Sprites.Directions, GraphicsUnit.Pixel, imgAttributes
                );
                
                img.Dispose();
                imgAttributes.Dispose();
            }

            gfx.Dispose();

            picNpc.BackgroundImage = picSpriteBmp;
        }

        private List<BaseDrop> GetCurrentDrops()
        {
            switch (CurrentTable)
            {
                case TableType.Main:
                    return mEditorItem.Drops;
                case TableType.Secondary:
                    return mEditorItem.SecondaryDrops;
                case TableType.Tertiary:
                    return mEditorItem.TertiaryDrops;
                default:
                    throw new NotImplementedException("Invalid table type to retrieve drops from");
            }
        }

        private void UpdateDropValues(bool keepIndex = false)
        {
            var drops = GetCurrentDrops();

            var index = lstDrops.SelectedIndex;
            lstDrops.Items.Clear();

            var totalWeight = LootTableHelpers.GetTotalWeight(drops);

            for (var i = 0; i < drops.Count; i++)
            {
                var prettyChance = LootTableHelpers.GetPrettyChance(drops[i].Chance, totalWeight);
                if (drops[i].ItemId != Guid.Empty)
                {
                    lstDrops.Items.Add(
                        $"{ItemBase.GetName(drops[i].ItemId)} x{drops[i].Quantity}: {prettyChance} chance"
                    );
                }
                else
                {
                    if (drops[i].LootTableId != Guid.Empty)
                    {
                        lstDrops.Items.Add(
                            $"[TABLE] {LootTableDescriptor.GetName(drops[i].LootTableId)}: {prettyChance} chance"
                        );
                    }
                    else
                    {
                        lstDrops.Items.Add(
                            $"{TextUtils.None} x{drops[i].Quantity}: {prettyChance} chance"
                        );
                    }
                }
            }

            if (keepIndex && index < lstDrops.Items.Count)
            {
                lstDrops.SelectedIndex = index;
            }
        }

        private void frmNpc_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.CurrentEditor = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mEditorItem.Spells.Add(SpellBase.IdFromList(cmbSpell.SelectedIndex));
            var n = lstSpells.SelectedIndex;
            lstSpells.Items.Clear();
            for (var i = 0; i < mEditorItem.Spells.Count; i++)
            {
                lstSpells.Items.Add(SpellBase.GetName(mEditorItem.Spells[i]));
            }

            lstSpells.SelectedIndex = n;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex > -1)
            {
                var i = lstSpells.SelectedIndex;
                lstSpells.Items.RemoveAt(i);
                mEditorItem.Spells.RemoveAt(i);
            }
        }

        private void cmbFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.SpellFrequency = cmbFreq.SelectedIndex;
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.NpcVsNpcEnabled = chkEnabled.Checked;
        }

        private void chkAttackAllies_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackAllies = chkAttackAllies.Checked;
        }

        private void btnAddAggro_Click(object sender, EventArgs e)
        {
            mEditorItem.AggroList.Add(NpcBase.IdFromList(cmbHostileNPC.SelectedIndex));
            lstAggro.Items.Clear();
            for (var i = 0; i < mEditorItem.AggroList.Count; i++)
            {
                if (mEditorItem.AggroList[i] != Guid.Empty)
                {
                    lstAggro.Items.Add(NpcBase.GetName(mEditorItem.AggroList[i]));
                }
                else
                {
                    lstAggro.Items.Add(Strings.General.none);
                }
            }
        }

        private void btnRemoveAggro_Click(object sender, EventArgs e)
        {
            if (lstAggro.SelectedIndex > -1)
            {
                var i = lstAggro.SelectedIndex;
                lstAggro.Items.RemoveAt(i);
                mEditorItem.AggroList.RemoveAt(i);
            }
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            PacketSender.SendCreateObject(GameObjectType.Npc);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            if (mEditorItem != null && lstGameObjects.Focused)
            {
                if (DarkMessageBox.ShowWarning(
                        Strings.NpcEditor.deleteprompt, Strings.NpcEditor.deletetitle, DarkDialogButton.YesNo,
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
                        Strings.NpcEditor.undoprompt, Strings.NpcEditor.undotitle, DarkDialogButton.YesNo,
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

        private void UpdateImmunities()
        {
            chkKnockback.Checked = GetImmunityValue(Immunities.Knockback);
            chkSilence.Checked = GetImmunityValue(Immunities.Silence);
            chkSnare.Checked = GetImmunityValue(Immunities.Snare);
            chkStun.Checked = GetImmunityValue(Immunities.Stun);
            chkSleep.Checked = GetImmunityValue(Immunities.Sleep);
            chkTransform.Checked = GetImmunityValue(Immunities.Transform);
            chkTaunt.Checked = GetImmunityValue(Immunities.Taunt);
            chkBlind.Checked = GetImmunityValue(Immunities.Blind);
            chkBlind.Checked = GetImmunityValue(Immunities.Blind);
            chkSlowed.Checked = GetImmunityValue(Immunities.Slowed);
            chkConfused.Checked = GetImmunityValue(Immunities.Confused);
            chkSpellCasting.Checked = GetImmunityValue(Immunities.Spellcasting);
        }

        private bool GetImmunityValue(Immunities immunity)
        {
            return mEditorItem.Immunities.TryGetValue(immunity, out var immunityVal) ? immunityVal : false;
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

        private void cmbAttackAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackAnimation =
                AnimationBase.Get(AnimationBase.IdFromList(cmbAttackAnimation.SelectedIndex - 1));
        }

        private void lstSpells_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex > -1)
            {
                cmbSpell.SelectedIndex = SpellBase.ListIndex(mEditorItem.Spells[lstSpells.SelectedIndex]);
            }
        }

        private void cmbSpell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSpells.SelectedIndex > -1 && lstSpells.SelectedIndex < mEditorItem.Spells.Count)
            {
                mEditorItem.Spells[lstSpells.SelectedIndex] = SpellBase.IdFromList(cmbSpell.SelectedIndex);
            }

            var n = lstSpells.SelectedIndex;
            lstSpells.Items.Clear();
            for (var i = 0; i < mEditorItem.Spells.Count; i++)
            {
                lstSpells.Items.Add(SpellBase.GetName(mEditorItem.Spells[i]));
            }

            lstSpells.SelectedIndex = n;
        }

        private void nudSpawnDuration_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.SpawnDuration = (int) nudSpawnDuration.Value;
        }

        private void nudSightRange_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.SightRange = (int) nudSightRange.Value;
        }

        private void nudStr_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stats.Attack] = (int) nudStr.Value;
            RefreshBalancing();
        }

        private void nudMag_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stats.AbilityPower] = (int) nudMag.Value;
            RefreshBalancing();
        }

        private void nudDef_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stats.Defense] = (int) nudDef.Value;
            RefreshBalancing();
        }

        private void nudMR_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stats.MagicResist] = (int) nudMR.Value;
            RefreshBalancing();
        }

        private void nudSpd_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int) Stats.Speed] = (int) nudSpd.Value;
        }


        private void nudCritChance_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CritChance = (int) nudCritChance.Value;
        }

        private void nudHp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MaxVital[(int) Vitals.Health] = (int) nudHp.Value;
            RefreshBalancing();
        }

        private void nudMana_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MaxVital[(int) Vitals.Mana] = (int) nudMana.Value;
            RefreshBalancing();
        }

        private void nudExp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Experience = (int) nudExp.Value;
        }

        private void cmbDropItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mDropTypeUpdate)
            {
                return;
            }
            var drops = GetCurrentDrops();
            if (lstDrops.SelectedIndex > -1 && lstDrops.SelectedIndex < drops.Count)
            {
                if (rdoItem.Checked)
                {
                    drops[lstDrops.SelectedIndex].ItemId = ItemBase.IdFromList(cmbDropItem.SelectedIndex - 1);
                }
                else
                {
                    drops[lstDrops.SelectedIndex].LootTableId = LootTableDescriptor.IdFromList(cmbDropItem.SelectedIndex);
                }
            }

            UpdateDropValues(true);
        }

        private void nudDropAmount_ValueChanged(object sender, EventArgs e)
        {
            // This should never be below 1. We shouldn't accept giving 0 items!
            nudDropAmount.Value = Math.Max(1, nudDropAmount.Value);

            if (lstDrops.SelectedIndex < lstDrops.Items.Count)
            {
                return;
            }

            GetCurrentDrops()[(int) lstDrops.SelectedIndex].Quantity = (int) nudDropAmount.Value;
            UpdateDropValues(true);
        }

        private void lstDrops_SelectedIndexChanged(object sender, EventArgs e)
        {
            var drops = GetCurrentDrops();
            if (mDropTypeUpdate)
            {
                return;
            }
            mDropTypeUpdate = true;
            if (lstDrops.SelectedIndex > -1)
            {
                if (drops[lstDrops.SelectedIndex].ItemId != Guid.Empty)
                {
                    if (!rdoItem.Checked)
                    {
                        rdoItem.Checked = true;
                    }
                    cmbDropItem.SelectedIndex = ItemBase.ListIndex(drops[lstDrops.SelectedIndex].ItemId) + 1;
                }
                else
                {
                    if (drops[lstDrops.SelectedIndex].LootTableId != Guid.Empty)
                    {
                        if (!rdoTable.Checked)
                        {
                            rdoTable.Checked = true;
                        }
                        cmbDropItem.SelectedIndex = LootTableDescriptor.ListIndex(drops[lstDrops.SelectedIndex].LootTableId);
                    }
                    else
                    {
                        if (!rdoItem.Checked)
                        {
                            rdoItem.Checked = true;
                        }
                        cmbDropItem.SelectedIndex = 0; // none
                    }
                }
                nudDropAmount.Value = drops[lstDrops.SelectedIndex].Quantity;
                nudDropChance.Value = (decimal)drops[lstDrops.SelectedIndex].Chance;
            }
            mDropTypeUpdate = false;
        }

        private void btnDropAdd_Click(object sender, EventArgs e)
        {
            var drops = GetCurrentDrops();
            if (rdoItem.Checked)
            {
                if (nudDropAmount.Value <= 0 || nudDropChance.Value <= 0)
                {
                    return;
                }
                drops.Add(new BaseDrop());
                drops[drops.Count - 1].ItemId = ItemBase.IdFromList(cmbDropItem.SelectedIndex - 1);
                drops[drops.Count - 1].LootTableId = Guid.Empty;
                drops[drops.Count - 1].Quantity = (int)nudDropAmount.Value;
            }
            else
            {
                // Don't allow adding a loot table to itself
                var lootTableId = LootTableDescriptor.IdFromList(cmbDropItem.SelectedIndex);
                if (lootTableId == mEditorItem.Id || nudDropChance.Value <= 0)
                {
                    return;
                }
                drops.Add(new BaseDrop());
                drops[drops.Count - 1].ItemId = Guid.Empty;
                drops[drops.Count - 1].LootTableId = LootTableDescriptor.IdFromList(cmbDropItem.SelectedIndex);
                drops[drops.Count - 1].Quantity = (int)nudDropAmount.Value;
            }

            drops[drops.Count - 1].Chance = (double)nudDropChance.Value;

            UpdateDropValues();
        }

        private void btnDropRemove_Click(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex > -1)
            {
                var i = lstDrops.SelectedIndex;
                lstDrops.Items.RemoveAt(i);
                GetCurrentDrops().RemoveAt(i);
            }

            UpdateDropValues(true);
        }

        private void nudDropChance_ValueChanged(object sender, EventArgs e)
        {
            if (lstDrops.SelectedIndex < lstDrops.Items.Count)
            {
                return;
            }

            GetCurrentDrops()[(int) lstDrops.SelectedIndex].Chance = (double) nudDropChance.Value;
            UpdateDropValues(true);
        }

        private void chkIndividualLoot_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IndividualizedLoot = chkIndividualLoot.Checked;
        }

        private void nudHpRegen_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalRegen[(int) Vitals.Health] = (int) nudHpRegen.Value;
        }

        private void nudMpRegen_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalRegen[(int) Vitals.Mana] = (int) nudMpRegen.Value;
        }

        private void chkAggressive_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Aggressive = chkAggressive.Checked;
            if (mEditorItem.Aggressive)
            {
                btnAttackOnSightCond.Text = Strings.NpcEditor.dontattackonsightconditions;
            }
            else
            {
                btnAttackOnSightCond.Text = Strings.NpcEditor.attackonsightconditions;
            }
        }

        private void cmbMovement_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.Movement = (byte) cmbMovement.SelectedIndex;
        }

        private void chkSwarm_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Swarm = chkSwarm.Checked;
        }

        private void nudFlee_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.FleeHealthPercentage = (byte) nudFlee.Value;
        }

        private void btnPlayerFriendProtectorCond_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(mEditorItem.PlayerFriendConditions, RequirementType.NpcFriend);
            frm.TopMost = true;
            frm.ShowDialog();
        }

        private void btnAttackOnSightCond_Click(object sender, EventArgs e)
        {
            if (chkAggressive.Checked)
            {
                var frm = new FrmDynamicRequirements(
                    mEditorItem.AttackOnSightConditions, RequirementType.NpcDontAttackOnSight
                );

                frm.TopMost = true;
                frm.ShowDialog();
            }
            else
            {
                var frm = new FrmDynamicRequirements(
                    mEditorItem.AttackOnSightConditions, RequirementType.NpcAttackOnSight
                );

                frm.TopMost = true;
                frm.ShowDialog();
            }
        }

        private void btnPlayerCanAttackCond_Click(object sender, EventArgs e)
        {
            var frm = new FrmDynamicRequirements(
                mEditorItem.PlayerCanAttackConditions, RequirementType.NpcCanBeAttacked
            );

            frm.TopMost = true;
            frm.ShowDialog();
        }

        private void cmbOnDeathEventKiller_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.OnDeathEvent = EventBase.Get(EventBase.IdFromList(cmbOnDeathEventKiller.SelectedIndex - 1));
        }

        private void cmbOnDeathEventParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.OnDeathPartyEvent = EventBase.Get(EventBase.IdFromList(cmbOnDeathEventParty.SelectedIndex - 1));
        }

        private void chkFocusDamageDealer_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.FocusHighestDamageDealer = chkFocusDamageDealer.Checked;
        }

        private void nudCritMultiplier_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.CritMultiplier = (double) nudCritMultiplier.Value;
        }

        private void cmbAttackSpeedModifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackSpeedModifier = cmbAttackSpeedModifier.SelectedIndex;
            nudAttackSpeedValue.Enabled = cmbAttackSpeedModifier.SelectedIndex > 0;
        }

        private void nudAttackSpeedValue_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.AttackSpeedValue = (int) nudAttackSpeedValue.Value;
            RefreshBalancing();
        }

        private void nudRgbaR_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Color.R = (byte)nudRgbaR.Value;
            DrawNpcSprite();
        }

        private void nudRgbaG_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Color.G = (byte)nudRgbaG.Value;
            DrawNpcSprite();
        }

        private void nudRgbaB_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Color.B = (byte)nudRgbaB.Value;
            DrawNpcSprite();
        }

        private void nudRgbaA_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Color.A = (byte)nudRgbaA.Value;
            DrawNpcSprite();
        }

        private void nudResetRadius_ValueChanged(object sender, EventArgs e)
        {
            // Set to either default or higher.
            nudResetRadius.Value = Math.Max(Options.Npc.ResetRadius, nudResetRadius.Value);
            mEditorItem.ResetRadius = (int)nudResetRadius.Value;
        }

        #region "Item List - Folders, Searching, Sorting, Etc"

        public void InitEditor()
        {
            //Collect folders
            var mFolders = new List<string>();
            foreach (var itm in NpcBase.Lookup)
            {
                if (!string.IsNullOrEmpty(((NpcBase) itm.Value).Folder) &&
                    !mFolders.Contains(((NpcBase) itm.Value).Folder))
                {
                    mFolders.Add(((NpcBase) itm.Value).Folder);
                    if (!mKnownFolders.Contains(((NpcBase) itm.Value).Folder))
                    {
                        mKnownFolders.Add(((NpcBase) itm.Value).Folder);
                    }
                }
            }

            mFolders.Sort();
            mKnownFolders.Sort();
            cmbFolder.Items.Clear();
            cmbFolder.Items.Add("");
            cmbFolder.Items.AddRange(mKnownFolders.ToArray());

            cmbTier.Items.Clear();
            cmbTier.Items.AddRange(Strings.ItemEditor.rarity.Values.ToArray());

            PopulateBestiaryList();

            var items = NpcBase.Lookup.OrderBy(p => p.Value?.Name).Select(pair => new KeyValuePair<Guid, KeyValuePair<string, string>>(pair.Key,
                new KeyValuePair<string, string>(((NpcBase)pair.Value)?.Name ?? Models.DatabaseObject<NpcBase>.Deleted, ((NpcBase)pair.Value)?.Folder ?? ""))).ToArray();
            lstGameObjects.Repopulate(items, mFolders, btnAlphabetical.Checked, CustomSearch(), txtSearch.Text);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            var folderName = "";
            var result = DarkInputBox.ShowInformation(
                Strings.NpcEditor.folderprompt, Strings.NpcEditor.foldertitle, ref folderName, DarkDialogButton.OkCancel
            );

            if (result == DialogResult.OK && !string.IsNullOrEmpty(folderName))
            {
                if (!cmbFolder.Items.Contains(folderName))
                {
                    mEditorItem.Folder = folderName;
                    lstGameObjects.UpdateText(folderName);
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
                txtSearch.Text = Strings.NpcEditor.searchplaceholder;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.SelectAll();
            txtSearch.Focus();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = Strings.NpcEditor.searchplaceholder;
        }

        private bool CustomSearch()
        {
            return !string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != Strings.NpcEditor.searchplaceholder;
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == Strings.NpcEditor.searchplaceholder)
            {
                txtSearch.SelectAll();
            }
        }

        #endregion

        private void chkKnockback_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Knockback] = chkKnockback.Checked;
        }

        private void chkSilence_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Silence] = chkSilence.Checked;
        }

        private void chkStun_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Stun] = chkStun.Checked;
        }

        private void chkSnare_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Snare] = chkSnare.Checked;
        }

        private void chkBlind_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Blind] = chkBlind.Checked;
        }

        private void chkTransform_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Transform] = chkTransform.Checked;
        }

        private void chkSleep_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Sleep] = chkSleep.Checked;
        }

        private void chkTaunt_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Taunt] = chkTaunt.Checked;
        }

        private void nudTenacity_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Tenacity = (double)nudTenacity.Value;
        }

        private void cmbDeathAnimation_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.DeathAnimation =
                AnimationBase.Get(AnimationBase.IdFromList(cmbDeathAnimation.SelectedIndex - 1));
        }

        private void cmbTransformIntoNpc_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.DeathTransform =
                NpcBase.Get(NpcBase.IdFromList(cmbTransformIntoNpc.SelectedIndex - 1));
        }

        private void chkStandStill_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.StandStill = chkStandStill.Checked;
        }

        private void cmbSpellAttackOverride_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.SpellAttackOverrideId = SpellBase.IdFromList(cmbSpellAttackOverride.SelectedIndex - 1);
        }

        private void rdoItem_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDropType(DropType.Item);
        }

        private void rdoTable_CheckedChanged(object sender, EventArgs e)
        {
            ChangeDropType(DropType.Table);
        }

        private void ChangeDropType(DropType dropType)
        {
            FillItemCombobox(dropType);
            if (dropType == DropType.Item)
            {
                nudDropAmount.Enabled = true;
            }
            else
            {
                nudDropAmount.Enabled = false;
            }
        }

        private void FillItemCombobox(DropType dropType)
        {
            mDropTypeUpdate = true;
            if (dropType == DropType.Item)
            {
                cmbDropItem.Items.Clear();
                cmbDropItem.Items.Add(Strings.General.none);
                cmbDropItem.Items.AddRange(ItemBase.Names);
            }
            else
            {
                cmbDropItem.Items.Clear();
                cmbDropItem.Items.AddRange(LootTableDescriptor.Names);
            }
            mDropTypeUpdate = false;
        }

        private void btnUnselectItem_Click(object sender, EventArgs e)
        {
            lstDrops.SelectedIndex = -1;
        }

        private void rdoMain_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTableType(TableType.Main);
        }

        private void rdoSecondary_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTableType(TableType.Secondary);
        }

        private void rdoTertiary_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTableType(TableType.Tertiary);
        }

        private void ChangeTableType(TableType type)
        {
            CurrentTable = type;
            nudTableChance.Enabled = true;
            switch (CurrentTable)
            {
                case TableType.Main:
                    nudTableChance.Value = 100.00M;
                    nudTableChance.Enabled = false;
                    break;
                case TableType.Secondary:
                    nudTableChance.Value = (decimal)mEditorItem.SecondaryChance;
                    break;
                case TableType.Tertiary:
                    nudTableChance.Value = (decimal)mEditorItem.TertiaryChance;
                    break;
            }
            UpdateDropValues();
        }

        private void nudTableChance_ValueChanged(object sender, EventArgs e)
        {
            switch (CurrentTable)
            {
                case TableType.Main:
                    nudTableChance.Value = 100.00M;
                    break;
                case TableType.Secondary:
                    mEditorItem.SecondaryChance = (double)nudTableChance.Value;
                    break;
                case TableType.Tertiary:
                    mEditorItem.TertiaryChance = (double)nudTableChance.Value;
                    break;
            }
        }

        private void chkSlowed_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Slowed] = chkSlowed.Checked;
        }

        private void chkConfused_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Confused] = chkConfused.Checked;
        }

        private void lblSpd_Click(object sender, EventArgs e)
        {

        }

        private void nudSlash_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stats.SlashAttack] = (int)nudSlash.Value;
            RefreshBalancing();
        }

        private void nudSlashResist_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stats.SlashResistance] = (int)nudSlashResist.Value;
            RefreshBalancing();
        }

        private void nudPierce_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stats.PierceAttack] = (int)nudPierce.Value;
            RefreshBalancing();
        }

        private void nudPierceResist_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stats.PierceResistance] = (int)nudPierceResist.Value;
            RefreshBalancing();
        }

        private void nudAccuracy_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stats.Accuracy] = (int)nudAccuracy.Value;
            RefreshBalancing();
        }

        private void nudEvasion_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Stats[(int)Stats.Evasion] = (int)nudEvasion.Value;
            RefreshBalancing();
        }

        private void PopulateDamageTypes()
        {
            chkDamageBlunt.Checked = false;
            chkDamagePierce.Checked = false;
            chkDamageSlash.Checked = false;
            chkDamageMagic.Checked = false;

            foreach (var type in mEditorItem?.AttackTypes)
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
            if (mEditorItem.AttackTypes.Contains(type))
            {
                return;
            }

            mEditorItem.AttackTypes.Add(type);
        }

        private void RemoveDamageType(AttackTypes type)
        {
            mEditorItem.AttackTypes.Remove(type);
        }

        private void chkDamageBlunt_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            if (chkDamageBlunt.Checked)
            {
                AddDamageType(AttackTypes.Blunt);
            }
            else
            {
                RemoveDamageType(AttackTypes.Blunt);
            }

            RefreshBalancing();
        }

        private void chkDamageSlash_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            if (chkDamageSlash.Checked)
            {
                AddDamageType(AttackTypes.Slashing);
            }
            else
            {
                RemoveDamageType(AttackTypes.Slashing);
            }

            RefreshBalancing();
        }

        private void chkDamagePierce_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            if (chkDamagePierce.Checked)
            {
                AddDamageType(AttackTypes.Piercing);
            }
            else
            {
                RemoveDamageType(AttackTypes.Piercing);
            }

            RefreshBalancing();
        }

        private void chkDamageMagic_CheckedChanged(object sender, EventArgs e)
        {
            if (mPopulating)
            {
                return;
            }

            if (chkDamageMagic.Checked)
            {
                AddDamageType(AttackTypes.Magic);
            }
            else
            {
                RemoveDamageType(AttackTypes.Magic);
            }

            RefreshBalancing();
        }
        
        private void PopulateBestiaryList()
        {
            lstBestiaryUnlocks.Items.Clear();
            lstBestiaryUnlocks.Items.AddRange(EnumExtensions.GetDescriptions(typeof(BestiaryUnlock)));
            lstBestiaryUnlocks.SelectedIndex = -1;
        }

        private void RefreshBestiaryList(bool resetIndex)
        {
            if (resetIndex)
            {
                lstBestiaryUnlocks.SelectedIndex = -1;
            }

            for (int idx = 0; idx < lstBestiaryUnlocks.Items.Count; idx++)
            {
                BestiaryUnlock unlock = (BestiaryUnlock)idx;
                int killCount = 0;
                if (!mEditorItem?.BestiaryUnlocks?.TryGetValue(idx, out killCount) ?? true)
                {
                    lstBestiaryUnlocks.Items[idx] = $"{unlock.GetDescription()}: Unlocked";
                }

                var killCountStr = killCount > 0 ? $"{killCount} kills" : "Unlocked";

                lstBestiaryUnlocks.Items[idx] = $"{unlock.GetDescription()}: {killCountStr}";
            }
        }

        private void PopulateBestiaryDefaults()
        {
            foreach(BestiaryUnlock val in Enum.GetValues(typeof(BestiaryUnlock)))
            {
                mEditorItem.BestiaryUnlocks[(int)val] = val.GetDefaultKillCount();
            }
            RefreshBestiaryList(true);
        }

        private void nudKillCount_ValueChanged(object sender, EventArgs e)
        {
            if (lstBestiaryUnlocks.SelectedIndex < 0 && lstBestiaryUnlocks.SelectedIndex >= lstBestiaryUnlocks.Items.Count)
            {
                return;
            }

            mEditorItem.BestiaryUnlocks[lstBestiaryUnlocks.SelectedIndex] = (int)nudKillCount.Value;
            RefreshBestiaryList(false);
        }

        private void txtStartDesc_TextChanged(object sender, EventArgs e)
        {
            mEditorItem.Description = txtStartDesc.Text;
        }

        private void lstBestiaryUnlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBestiaryUnlocks.SelectedIndex > -1)
            {
                if (mEditorItem.BestiaryUnlocks.TryGetValue(lstBestiaryUnlocks.SelectedIndex, out var killCount))
                {
                    nudKillCount.Value = killCount;
                    return;
                }
                nudKillCount.Value = 0;
            }
        }

        private void btnBestiaryDefaults_Click(object sender, EventArgs e)
        {
            PopulateBestiaryDefaults();
        }

        private void chkBestiary_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.NotInBestiary = chkBestiary.Checked;
        }

        private void chkImpassable_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Impassable = chkImpassable.Checked;
        }

        private void chkNoBack_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.NoBackstab = chkNoBack.Checked;
        }

        private void chkStealth_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.NoStealthBonus = chkStealth.Checked;
        }

        private void cmbTier_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTier = cmbTier.SelectedIndex;
            RefreshBalancing();
        }

        float DesiredDps { get; set; }
        float HighRes { get; set; }
        float MedRes { get; set; }
        float LowRes { get; set; }
        long ReccExp { get; set; }

        private void RefreshBalancing()
        {
            if (mEditorItem == null)
            {
                return;
            }

            CombatUtilities.CalculateDamage(mEditorItem.AttackTypes, 1.0, 100, mEditorItem.Stats, new int[(int)Stats.StatCount], out var maxHit);
            var dps = CombatUtilities.CalculateDps(mEditorItem.AttackTypes, 1.0, 100, mEditorItem.Stats, new int[(int)Stats.StatCount], mEditorItem.AttackSpeedValue);

            DesiredDps = CombatUtilities.TierToDamageFormula(CurrentTier);
            
            var highRes = CombatUtilities.TierAndSlotToArmorRatingFormula(CurrentTier, -1, CombatUtilities.ResistanceLevel.High);
            var mediumRes = CombatUtilities.TierAndSlotToArmorRatingFormula(CurrentTier, -1, CombatUtilities.ResistanceLevel.Medium);
            var lowRes = CombatUtilities.TierAndSlotToArmorRatingFormula(CurrentTier, -1, CombatUtilities.ResistanceLevel.Low);

            var hitsToKill = Math.Ceiling(mEditorItem.MaxVital[(int)Vitals.Health] / DesiredDps);

            lblTargetDpsVal.Text = DesiredDps.ToString("N2");
            lblProjectedDpsVal.Text = dps.ToString("N2");
            lblMaxHitVal.Text = maxHit.ToString("N0");

            HighRes = highRes;
            MedRes = mediumRes;
            LowRes = lowRes;

            ReccExp = NpcExperienceCalculator.Calculate(mEditorItem);
            lblCalcExpVal.Text = $"{ReccExp} EXP";
        }

        private void btnBluntLo_Click(object sender, EventArgs e)
        {
            nudDef.Value = (int)Math.Floor(LowRes);
        }

        private void btnSlashLo_Click(object sender, EventArgs e)
        {
            nudSlashResist.Value = (int)Math.Floor(LowRes);
        }

        private void btnPierceLo_Click(object sender, EventArgs e)
        {
            nudPierceResist.Value = (int)Math.Floor(LowRes);
        }

        private void btnMagLo_Click(object sender, EventArgs e)
        {
            nudMR.Value = (int)Math.Floor(LowRes);
        }

        private void btnBluntMed_Click(object sender, EventArgs e)
        {
            nudDef.Value = (int)Math.Floor(MedRes);
        }

        private void btnSlashMed_Click(object sender, EventArgs e)
        {
            nudSlashResist.Value = (int)Math.Floor(MedRes);
        }

        private void btnPierceMed_Click(object sender, EventArgs e)
        {
            nudPierceResist.Value = (int)Math.Floor(MedRes);
        }

        private void btnMagMed_Click(object sender, EventArgs e)
        {
            nudMR.Value = (int)Math.Floor(MedRes);
        }

        private void btnBluntHigh_Click(object sender, EventArgs e)
        {
            nudDef.Value = (int)Math.Floor(HighRes);
        }

        private void btnSlashHi_Click(object sender, EventArgs e)
        {
            nudSlashResist.Value = (int)Math.Floor(HighRes);
        }

        private void btnPierceHi_Click(object sender, EventArgs e)
        {
            nudPierceResist.Value = (int)Math.Floor(HighRes);
        }

        private void btnMagHi_Click(object sender, EventArgs e)
        {
            nudMR.Value = (int)Math.Floor(HighRes);
        }

        private void btnUseCalcExp_Click(object sender, EventArgs e)
        {
            nudExp.Value = ReccExp;
        }


        const int LO_HP_MULT = 2;
        const int MED_HP_MULT = 5;
        const int HI_HP_MULT = 10;
        const int BOSS_HP_MULT = 30;
        private Stats GetWeakestRes()
        {
            var minStat = int.MaxValue;
            Stats minimumStat = Stats.Defense;
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                if (i != (int)Stats.Defense ||
                    i != (int)Stats.SlashResistance ||
                    i != (int)Stats.PierceResistance ||
                    i != (int)Stats.MagicResist)
                {
                    continue;
                }

                if (mEditorItem.Stats[i] < minStat)
                {
                    minStat = mEditorItem.Stats[i];
                    minimumStat = (Stats)i;
                }
            }

            return minimumStat;
        }

        private int GetPotentialMaxHitOnNpc()
        {
            var weakestTo = GetWeakestRes();
            var strongestAttackType = StatHelpers.GetResistedStat(weakestTo);

            var attackerStats = new int[(int)Stats.StatCount];
            for (var i = 0; i < (int)Stats.StatCount; i++)
            {
                attackerStats[i] = (int)Math.Floor(DesiredDps);
            }

            CombatUtilities.CalculateDamage(new List<AttackTypes>() { strongestAttackType }, 1.0, 100, attackerStats, mEditorItem.Stats, out var maxHit);

            return maxHit;
        }

        private void btnHpLo_Click(object sender, EventArgs e)
        {
            var desiredHp = GetPotentialMaxHitOnNpc() * LO_HP_MULT;
            nudHp.Value = desiredHp;
        }

        private void btnHpMed_Click(object sender, EventArgs e)
        {
            var desiredHp = GetPotentialMaxHitOnNpc() * MED_HP_MULT;
            nudHp.Value = desiredHp;
        }

        private void btnHpHi_Click(object sender, EventArgs e)
        {
            var desiredHp = GetPotentialMaxHitOnNpc() * HI_HP_MULT;
            nudHp.Value = desiredHp;
        }

        private void btnHpXl_Click(object sender, EventArgs e)
        {
            var desiredHp = GetPotentialMaxHitOnNpc() * BOSS_HP_MULT;
            nudHp.Value = desiredHp;
        }

        private void nudTier_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.Level = (int)nudTier.Value;
            lblCalcExpVal.Text = $"{NpcExperienceCalculator.Calculate(mEditorItem)} EXP";
        }

        private void chkSpellCast_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IsSpellcaster = chkSpellCast.Checked;
        }

        private void chkCannotHeal_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.CannotBeHealed = chkCannotHeal.Checked;
        }

        private void cmbScalingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.NpcScaleType = cmbScalingType.SelectedIndex;
        }

        private void nudScaledTo_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.ScaledTo = (int)nudScaledTo.Value;
        }

        private void nudScaleFactor_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.VitalScaleModifier = (float)nudScaleFactor.Value;
        }

        private void darkNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MaxScaledTo = (int)nudMaxScaledTo.Value;
        }

        private void chkPlayerLockLoot_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.PlayerLockedLoot = chkPlayerLockLoot.Checked;
        }

        private void cmbChampion_SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorItem.ChampionId =
                NpcBase.IdFromList(cmbChampion.SelectedIndex - 1);
        }

        private void nudChampSpawnChance_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.ChampionSpawnChance = (float)nudChampSpawnChance.Value;
        }

        private void nudChampCooldown_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.ChampionCooldownSeconds = (long)nudChampCooldown.Value;
        }

        private void chkIsChampion_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.IsChampion = chkIsChampion.Checked;
        }

        private void nudLifesteal_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MeleeLifesteal = (int)nudLifesteal.Value;
        }

        private void darkNumericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            mEditorItem.MeleeManasteal = (int)nudManasteal.Value;
        }

        private void chkNeverSkip_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.NeverSkipSpellCasting = chkNeverSkip.Checked;
        }

        private void chkSpellCasting_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.Immunities[Immunities.Spellcasting] = chkSpellCasting.Checked;
        }

        private void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.HideName = chkHideName.Checked;
        }

        private void chkAnimated_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.AnimatedSprite = chkAnimated.Checked;
        }

        private void chkSequentialCasting_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.SequentialCasting = chkSequentialCasting.Checked;
        }

        private void chkDisableExhaustion_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.DisableAutoExhaustion = chkDisableExhaustion.Checked;
        }

        private void chkDisableAutoAttack_CheckedChanged(object sender, EventArgs e)
        {
            mEditorItem.DisableAutoAttack = chkDisableAutoAttack.Checked;
        }
    }

}
