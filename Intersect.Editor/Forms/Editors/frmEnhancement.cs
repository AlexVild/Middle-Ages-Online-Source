using DarkUI.Controls;
using Intersect.Editor.Forms.Helpers;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors
{
    public partial class frmEnhancement : EditorForm
    {

        private List<EnhancementDescriptor> mChanged = new List<EnhancementDescriptor>();

        private EnhancementDescriptor mEditorItem;

        private string mCopiedItem;

        private bool mPopulating = false;

        private List<string> mKnownFolders = new List<string>();

        private List<Guid> WeaponTypes = new List<Guid>();

        private Stats SelectedStat 
        {
            get
            {
                try
                {
                    return (Stats)cmbStats.SelectedIndex;
                }
                catch
                {
                    return Stats.Attack;
                }
            }
        }

        private Vitals SelectedVital
        {
            get
            {
                try
                {
                    return (Vitals)cmbVitals.SelectedIndex;
                }
                catch
                {
                    return Vitals.Health;
                }
            }
        }
        private EffectType SelectedEffect
        {
            get
            {
                try
                {
                    return (EffectType)cmbEffect.SelectedIndex;
                }
                catch
                {
                    return EffectType.None;
                }
            }
        }

        public frmEnhancement()
        {
            ApplyHooks();
            InitializeComponent();

            if (mEditorItem == null)
            {
                pnlContainer.Hide();
            }

            cmbWeaponTypes.Items.Clear();
            cmbWeaponTypes.Items.AddRange(WeaponTypeDescriptor.Names);

            cmbStats.Items.Clear();
            cmbStats.Items.AddRange(EnumExtensions.GetDescriptions(typeof(Stats), Stats.StatCount.GetDescription()));

            cmbVitals.Items.Clear();
            cmbVitals.Items.AddRange(EnumExtensions.GetDescriptions(typeof(Vitals), Vitals.VitalCount.GetDescription()));
            
            cmbEffect.Items.Clear();
            cmbEffect.Items.AddRange(EnumExtensions.GetDescriptions(typeof(EffectType)));

            cmbSpells.Items.Clear();
            cmbSpells.Items.AddRange(SpellBase.Names);

            cmbEnhancementPrereq.Items.Clear();
            cmbEnhancementPrereq.Items.AddRange(EnhancementDescriptor.Names);

            lstGameObjects.Init(UpdateToolStripItems, AssignEditorItem, toolStripItemNew_Click, toolStripItemCopy_Click, toolStripItemUndo_Click, toolStripItemPaste_Click, toolStripItemDelete_Click);
        }

        private void UpdateFields()
        {
            mPopulating = true;

            txtName.Text = mEditorItem.Name;
            cmbFolder.Text = mEditorItem.Folder;

            nudReqEp.Value = mEditorItem.RequiredEnhancementPoints;
            cmbEffect.SelectedIndex = 0;
            cmbStats.SelectedIndex = 0;
            cmbVitals.SelectedIndex = 0;
            nudMinWeaponLevel.Value = mEditorItem.MinimumWeaponLevel;

            RefreshList(ref lstStatBuffs, mEditorItem.StatMods, false, false);
            RefreshList(ref lstVitalMods, mEditorItem.VitalMods, false, false);
            RefreshList(ref lstBonuses, mEditorItem.EffectMods, true, false);
            RefreshList(ref lstSpellEffects, mEditorItem.SpellEnhancements, false);
            RefreshWeaponTypes(false);

            RefreshPrereqList();

            mPopulating = false;
        }

        private void RefreshList<T>(ref ListBox list, List<Enhancement<T>> enhancements, bool isPercent, bool savePos) where T : Enum
        {
            var prevPos = -1;
            if (savePos)
            {
                prevPos = list.SelectedIndex;
            }

            list.Items.Clear();
            foreach (var item in enhancements)
            {
                list.Items.Add(item.GetRangeDisplay(isPercent));
            }

            if (savePos && list.Items.Count > 0 && list.Items.Count > prevPos)
            {
                list.SelectedIndex = prevPos;
            }
        }

        private void RefreshList(ref ListBox list, List<SpellEnhancementDescriptor> enhancements, bool savePos)
        {
            var prevPos = -1;
            if (savePos)
            {
                prevPos = list.SelectedIndex;
            }

            list.Items.Clear();
            foreach (var item in enhancements)
            {
                list.Items.Add(item.GetRangeDisplay());
            }

            if (savePos && list.Items.Count > 0 && list.Items.Count > prevPos)
            {
                list.SelectedIndex = prevPos;
            }
        }

        private void RefreshWeaponTypes(bool savePos)
        {
            WeaponTypes.Clear();
            var prevPos = -1;
            if (savePos)
            {
                prevPos = lstWeaponTypes.SelectedIndex;
            }

            lstWeaponTypes.Items.Clear();
            foreach (var kv in mEditorItem.ValidWeaponTypes)
            {
                WeaponTypes.Add(kv.Key);
                var weaponTypeId = kv.Key;
                var minLevel = kv.Value;
                lstWeaponTypes.Items.Add($"{WeaponTypeDescriptor.GetName(weaponTypeId)} Lv. {minLevel}");
            }

            if (savePos && lstWeaponTypes.Items.Count > 0 && lstWeaponTypes.Items.Count > prevPos)
            {
                lstWeaponTypes.SelectedIndex = prevPos;
            }
        }

        private void AddOrReplaceEnhancement<E>(DarkComboBox cmbEnhance, List<Enhancement<E>> enhancements, E selectedVal, int min, int max)
            where E: Enum
        {
            if (cmbEnhance.SelectedIndex < 0 || cmbEnhance.SelectedIndex >= Enum.GetNames(typeof(E)).Length)
            {
                return;
            }

            var prevMod = enhancements.Find(mod => mod.EnhancementType.Equals(selectedVal));
            if (prevMod != default)
            {
                prevMod.MinValue = min;
                prevMod.MaxValue = max;
                return;
            }

            enhancements.Add(new Enhancement<E>(selectedVal, min, max));
        }

        private void RemoveEnhancement<E>(ListBox listBox, List<Enhancement<E>> enhancements) where E : Enum
        {
            if (listBox.SelectedIndex < 0 || listBox.SelectedIndex >= listBox.Items.Count)
            {
                return;
            }

            enhancements.RemoveAt(listBox.SelectedIndex);
        }

        private void AddOrReplaceSpellEnhancement(DarkComboBox cmbSpell, List<SpellEnhancementDescriptor> enhancements, Guid selectedSpell, int min, int max)
        {
            if (cmbSpell.SelectedIndex < 0 || cmbSpell.SelectedIndex >= cmbSpell.Items.Count)
            {
                return;
            }

            var prevMod = enhancements.Find(mod => mod.SpellId.Equals(selectedSpell));
            if (prevMod != default)
            {
                prevMod.MinValue = min;
                prevMod.MaxValue = max;
                return;
            }

            enhancements.Add(new SpellEnhancementDescriptor(selectedSpell, min, max));
        }

        #region Editor Form stuffs
        protected override void GameObjectUpdatedDelegate(GameObjectType type)
        {
            FormHelpers.GameObjectUpdatedDelegate(ref mEditorItem, InitEditor, UpdateEditor);
        }

        public void InitEditor()
        {
            FormHelpers.InitFoldersAndObjectList<EnhancementDescriptor>(
                ref mKnownFolders,
                ref cmbFolder,
                ref lstGameObjects,
                txtSearch,
                EnhancementDescriptor.Lookup,
                btnAlphabetical
            );
        }

        private void UpdateEditor()
        {
            FormHelpers.UpdateEditor(
                ref mEditorItem,
                ref mChanged,
                ref pnlContainer,
                UpdateToolStripItems,
                UpdateFields
            );
        }

        private void AssignEditorItem(Guid id)
        {
            mEditorItem = EnhancementDescriptor.Get(id);
            UpdateEditor();
        }

        private void UpdateToolStripItems()
        {
            FormHelpers.UpdateToolstripItems(ref toolStripItemCopy, ref toolStripItemPaste, ref toolStripItemUndo, ref toolStripItemDelete, mCopiedItem, mEditorItem, lstGameObjects);
        }

        private void toolStripItemNew_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripNewClicked(GameObjectType.Enhancement);
        }

        private void toolStripItemDelete_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripDeleteClicked(mEditorItem, lstGameObjects);
        }

        private void btnAlphabetical_Click(object sender, EventArgs e)
        {
            FormHelpers.AlphabeticalClicked(ref btnAlphabetical, InitEditor);
        }

        private void toolStripItemCopy_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripCopyClicked(ref mCopiedItem, mEditorItem, lstGameObjects, ref toolStripItemPaste);
        }

        private void toolStripItemPaste_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripPasteClicked(ref mEditorItem, mCopiedItem, lstGameObjects, UpdateEditor);
        }

        private void toolStripItemUndo_Click(object sender, EventArgs e)
        {
            FormHelpers.ToolStripUndoClicked(mChanged, ref mEditorItem, UpdateEditor);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.SearchTextChanged(InitEditor);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            FormHelpers.ClearSearchPressed(ref txtSearch);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            FormHelpers.EditorItemNameChange(ref mEditorItem, txtName, lstGameObjects);
        }

        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelpers.FolderChanged(ref mEditorItem, cmbFolder, InitEditor);
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FormHelpers.AddFolder(ref mEditorItem, ref cmbFolder, ref lstGameObjects, InitEditor);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormHelpers.SaveClicked(ref mChanged, Hide, Dispose);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormHelpers.CancelClicked(ref mChanged, Hide, Dispose);
        }

        #endregion

        private void nudReqEp_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.RequiredEnhancementPoints = (int)nudReqEp.Value;
        }

        private void btnAddStat_Click(object sender, EventArgs e)
        {
            AddOrReplaceEnhancement(cmbStats, mEditorItem.StatMods, SelectedStat, (int)nudMinStat.Value, (int)nudMaxStat.Value);
            RefreshList(ref lstStatBuffs, mEditorItem.StatMods, false, false);
        }

        private void btnRemoveStat_Click(object sender, EventArgs e)
        {
            RemoveEnhancement(lstStatBuffs, mEditorItem.StatMods);
            RefreshList(ref lstStatBuffs, mEditorItem.StatMods, false, true);
        }

        private void btnRemoveVital_Click(object sender, EventArgs e)
        {
            RemoveEnhancement(lstVitalMods, mEditorItem.VitalMods);
            RefreshList(ref lstVitalMods, mEditorItem.VitalMods, false, true);
        }

        private void btnAddVital_Click(object sender, EventArgs e)
        {
            AddOrReplaceEnhancement(cmbVitals, mEditorItem.VitalMods, SelectedVital, (int)nudMinVital.Value, (int)nudMaxVital.Value);
            RefreshList(ref lstVitalMods, mEditorItem.VitalMods, false, false);
        }

        private void btnRemoveBonus_Click(object sender, EventArgs e)
        {
            RemoveEnhancement(lstBonuses, mEditorItem.EffectMods);
            RefreshList(ref lstBonuses, mEditorItem.EffectMods, true, true);
        }

        private void btnAddBonus_Click(object sender, EventArgs e)
        {
            AddOrReplaceEnhancement(cmbEffect, mEditorItem.EffectMods, SelectedEffect, (int)nudBonusMin.Value, (int)nudBonusMax.Value);
            RefreshList(ref lstBonuses, mEditorItem.EffectMods, true, false);
        }

        private void btnAddWeaponType_Click(object sender, EventArgs e)
        {
            var selectedType = WeaponTypeDescriptor.IdFromList(cmbWeaponTypes.SelectedIndex);
            if (mEditorItem.ValidWeaponTypes.ContainsKey(selectedType))
            {
                return;
            }

            mEditorItem.ValidWeaponTypes[selectedType] = (int)nudMinWeaponLevel.Value;
            RefreshWeaponTypes(false);
        }

        private void btnRemoveWeaponType_Click(object sender, EventArgs e)
        {
            var selectedIdx = lstWeaponTypes.SelectedIndex;
            if (selectedIdx < 0 || selectedIdx >= lstWeaponTypes.Items.Count)
            {
                return;
            }

            mEditorItem.ValidWeaponTypes.Remove(WeaponTypes[selectedIdx]);
            RefreshWeaponTypes(true);
        }

        private void nudMinWeaponLevel_ValueChanged(object sender, EventArgs e)
        {
            mEditorItem.MinimumWeaponLevel = (int)nudMinWeaponLevel.Value;
        }

        private void lstStatBuffs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIdx = lstStatBuffs.SelectedIndex;
            if (selectedIdx < 0 || selectedIdx >= mEditorItem.StatMods.Count)
            {
                return;
            }

            var stat = mEditorItem.StatMods[selectedIdx];
            cmbStats.SelectedIndex = (int)stat.EnhancementType;
            nudMinStat.Value = stat.MinValue;
            nudMaxStat.Value = stat.MaxValue;
        }

        private void lstVitalMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIdx = lstVitalMods.SelectedIndex;
            if (selectedIdx < 0 || selectedIdx >= mEditorItem.VitalMods.Count)
            {
                return;
            }

            var vital = mEditorItem.VitalMods[selectedIdx];
            cmbVitals.SelectedIndex = (int)vital.EnhancementType;
            nudMinVital.Value = vital.MinValue;
            nudMaxStat.Value = vital.MaxValue;
        }

        private void lstBonuses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIdx = lstBonuses.SelectedIndex;
            if (selectedIdx < 0 || selectedIdx >= mEditorItem.EffectMods.Count)
            {
                return;
            }

            var effect = mEditorItem.EffectMods[selectedIdx];
            cmbEffect.SelectedIndex = (int)effect.EnhancementType;
            nudBonusMin.Value = effect.MinValue;
            nudBonusMax.Value = effect.MaxValue;
        }

        private void btnAddPrereq_Click(object sender, EventArgs e)
        {
            var selectedPrereq = EnhancementDescriptor.IdFromList(cmbEnhancementPrereq.SelectedIndex);
            if (mEditorItem.PrerequisiteEnhancements.Contains(selectedPrereq))
            {
                return;
            }

            mEditorItem.PrerequisiteEnhancements.Add(selectedPrereq);
            RefreshPrereqList();
        }

        private void btnRemovePrereq_Click(object sender, EventArgs e)
        {
            if (lstPrereqs.SelectedIndex < 0 || lstPrereqs.SelectedIndex >= mEditorItem.PrerequisiteEnhancements.Count)
            {
                return;
            }

            mEditorItem.PrerequisiteEnhancements.RemoveAt(lstPrereqs.SelectedIndex);
            RefreshPrereqList();
        }

        private void RefreshPrereqList()
        {
            lstPrereqs.Items.Clear();
            foreach(var prereq in mEditorItem.PrerequisiteEnhancements)
            {
                lstPrereqs.Items.Add(EnhancementDescriptor.GetName(prereq));
            }
        }

        private void btnRemoveSpell_Click(object sender, EventArgs e)
        {
            RemoveEnhancement(lstBonuses, mEditorItem.EffectMods);
            RefreshList(ref lstSpellEffects, mEditorItem.SpellEnhancements, false);
        }

        private void btnAddSpell_Click(object sender, EventArgs e)
        {
            AddOrReplaceSpellEnhancement(cmbSpells, mEditorItem.SpellEnhancements, SpellBase.IdFromList(cmbSpells.SelectedIndex), (int)nudMinSpell.Value, (int)nudMaxSpell.Value);
            RefreshList(ref lstSpellEffects, mEditorItem.SpellEnhancements, true);
        }
    }
}
