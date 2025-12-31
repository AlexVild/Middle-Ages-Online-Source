using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Maps;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.AdvancedMapControls
{
    public partial class frmNpcWaveGroups : Form
    {
        MapInstance EditingMap { get; set; }
        List<NpcWaveGroupDescriptor> EditingGroups { get; set; }
        NpcWaveGroupDescriptor SelectedGroup { get; set; }
        NpcWaveDescriptor SelectedWave { get; set; }

        private bool mEditing = false;

        public frmNpcWaveGroups(MapInstance editingMap)
        {
            EditingMap = editingMap;
            if (editingMap == null)
            {
                Dispose();
                return;
            }

            // Deep clone
            EditingGroups = editingMap.NpcWaveGroups
                .Select(g => new NpcWaveGroupDescriptor(g))
                .ToList();

            InitializeComponent();
            InitializeEditor();
        }

        public void InitializeEditor()
        {
            grpWaveGroup.Hide();
            cmbEndEvent.Items.Add(Strings.EditorGenerics.None);
            cmbEndEvent.Items.AddRange(EventBase.Names);
            cmbStartEvent.Items.Add(Strings.EditorGenerics.None);
            cmbStartEvent.Items.AddRange(EventBase.Names);
            UpdateGroupList();
        }

        private void ApplyChanges()
        {
            EditingMap.NpcWaveGroups = EditingGroups
                .Select(g => new NpcWaveGroupDescriptor(g))
                .ToList();
        }

        private void Close()
        {
            Globals.CurrentEditor = -1;
            Dispose();
        }

        private void UpdateGroupList()
        {
            lstWaveGroups.Items.Clear();
            foreach (var group in EditingGroups)
            {
                lstWaveGroups.Items.Add(group.Name);
            }
        }

        private void UpdateWaveList()
        {
            lstWaves.Items.Clear();
            
            if (SelectedGroup == default)
            {
                return;
            }
            
            var idx = SelectedGroup.AutoStartWave;
            foreach (var wave in SelectedGroup.Waves)
            {
                if (wave.LoopIterations > 0)
                {
                    var endWave = idx + wave.LoopIterations;
                    lstWaves.Items.Add($"Waves {idx} - {endWave}");
                    idx = endWave + 1;
                }
                else
                {
                    lstWaves.Items.Add($"Wave {idx}");
                    idx++;
                }
            }
        }

        private void InitializeWaveGroup(NpcWaveGroupDescriptor descriptor)
        {
            txtName.Text = descriptor.Name;
            nudStartWave.Value = descriptor.AutoStartWave;
            UpdateWaveList();
        }

        private void InitializeWave(NpcWaveDescriptor descriptor)
        {
            chkAutoAdvance.Checked = descriptor.AdvanceOnPermadeadCompletion;
            nudAdvancetimeout.Value = descriptor.AdvanceOnTimeoutMs;
            nudLoopIterations.Value = descriptor.LoopIterations;
            nudTransitionTime.Value = descriptor.TimeBetweenMs;

            cmbStartEvent.SelectedIndex = EventBase.ListIndex(descriptor.OnStartEventId) + 1;
            cmbEndEvent.SelectedIndex = EventBase.ListIndex(descriptor.OnEndEventId) + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            Close();
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            EditingGroups.Add(new NpcWaveGroupDescriptor());
            UpdateGroupList();
        }

        private void lstWaveGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mEditing)
            {
                return;
            }

            mEditing = true;
            SelectedGroup = EditingGroups.ElementAtOrDefault(lstWaveGroups.SelectedIndex);
            if (SelectedGroup == default)
            {
                grpWaveGroup.Hide();
                mEditing = false;
                return;
            }

            grpWaveGroup.Show();
            InitializeWaveGroup(SelectedGroup);
            mEditing = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (mEditing)
            {
                return;
            }

            var selectedGroupIdx = lstWaveGroups.SelectedIndex;
            mEditing = true;
            SelectedGroup.Name = txtName.Text;
            UpdateGroupList();
            lstWaveGroups.SelectedIndex = selectedGroupIdx;
            mEditing = false;
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            if (EditingGroups.ElementAtOrDefault(lstWaveGroups.SelectedIndex) == default)
            {
                return;
            }
            
            EditingGroups.RemoveAt(lstWaveGroups.SelectedIndex);
            UpdateGroupList();
            grpWaveGroup.Hide();
        }

        private void nudStartWave_ValueChanged(object sender, EventArgs e)
        {
            SelectedGroup.AutoStartWave = (int)nudStartWave.Value;
        }

        private void lstWaves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mEditing)
            {
                return;
            }

            mEditing = true;
            SelectedWave = SelectedGroup.Waves.ElementAtOrDefault(lstWaves.SelectedIndex);
            if (SelectedWave == default)
            {
                grpWave.Hide();
                mEditing = false;
                return;
            }

            grpWave.Show();
            InitializeWave(SelectedWave);
            mEditing = false;
        }

        private void nudAdvancetimeout_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedWave == null)
            {
                return;
            }

            SelectedWave.AdvanceOnTimeoutMs = (int)nudAdvancetimeout.Value;
        }

        private void nudLoopIterations_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedWave == null || mEditing)
            {
                return;
            }

            var selectedWaveIdx = lstWaves.SelectedIndex;
            mEditing = true;
            SelectedWave.LoopIterations = (int)nudLoopIterations.Value;
            UpdateWaveList();
            lstWaves.SelectedIndex = selectedWaveIdx;
            mEditing = false;
        }

        private void nudTransitionTime_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedWave == null)
            {
                return;
            }

            SelectedWave.TimeBetweenMs = (int)nudTransitionTime.Value;
        }

        private void cmbStartEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedWave.OnStartEventId = EventBase.IdFromList(cmbStartEvent.SelectedIndex - 1);
        }

        private void cmbEndEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedWave.OnEndEventId = EventBase.IdFromList(cmbEndEvent.SelectedIndex - 1);
        }

        private void btnAddWave_Click(object sender, EventArgs e)
        {
            if (SelectedGroup == null)
            {
                return;
            }

            SelectedGroup.Waves.Add(new NpcWaveDescriptor());
            UpdateWaveList();
        }

        private void btnRemoveWave_Click(object sender, EventArgs e)
        {
            if (SelectedGroup.Waves.ElementAtOrDefault(lstWaves.SelectedIndex) == default)
            {
                return;
            }
            
            SelectedGroup.Waves.RemoveAt(lstWaves.SelectedIndex);
            UpdateWaveList();
            grpWave.Hide();
        }
    }
}
