using System;
using System.Windows.Forms;

using DarkUI.Controls;
using Intersect.Editor.Content;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps.MapList;
using Intersect.Utilities;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{

    public partial class EventCommandWarp : UserControl
    {

        private readonly FrmEvent mEventEditor;

        private WarpCommand mMyCommand;

        public EventCommandWarp(WarpCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();
            mMyCommand = refCommand;
            mEventEditor = editor;
            InitLocalization();
            cmbMap.Items.Clear();
            cmbInstanceType.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMap.Items.Add(MapList.OrderedMaps[i].Name);
                if (MapList.OrderedMaps[i].MapId == mMyCommand.MapId)
                {
                    cmbMap.SelectedIndex = i;
                }
            }

            if (cmbMap.SelectedIndex == -1)
            {
                cmbMap.SelectedIndex = 0;
            }

            scrlX.Maximum = Options.MapWidth - 1;
            scrlY.Maximum = Options.MapHeight - 1;
            scrlX.Value = mMyCommand.X;
            scrlY.Value = mMyCommand.Y;
            chkMapFade.Checked = mMyCommand.FadeOnWarp;
            lblX.Text = Strings.Warping.x.ToString(scrlX.Value);
            lblY.Text = Strings.Warping.y.ToString(scrlY.Value);
            cmbDirection.SelectedIndex = (int) mMyCommand.Direction;
            chkChangeInstance.Checked = mMyCommand.ChangeInstance;
            grpInstanceSettings.Visible = chkChangeInstance.Checked;
            
            // We do not want to iterate over the "NoChange" enum - so we subtract 1 from the iterating maximum
            for (var i = 0; i < Enum.GetNames(typeof(MapInstanceType)).Length - 1; i++)
            {
                cmbInstanceType.Items.Add(Enum.GetName(typeof(MapInstanceType), i));
            }
            cmbInstanceType.SelectedIndex = (int) mMyCommand.InstanceType;

            cmbDungeon.Items.Clear();
            cmbDungeon.Items.Add(Strings.General.none);
            cmbDungeon.Items.AddRange(DungeonDescriptor.Names);
            cmbDungeon.SelectedIndex = DungeonDescriptor.ListIndex(mMyCommand.DungeonId) + 1;

            cmbSound.Items.Clear();
            cmbSound.Items.Add(Strings.General.none);
            cmbSound.Items.AddRange(GameContentManager.SmartSortedSoundNames);
            if (cmbSound.Items.IndexOf(TextUtils.NullToNone(mMyCommand.Sound)) > -1)
            {
                cmbSound.SelectedIndex = cmbSound.Items.IndexOf(TextUtils.NullToNone(mMyCommand.Sound));
            }
            else
            {
                cmbSound.SelectedIndex = 0;
            }

            cmbCommonSounds.Items.Clear();
            cmbCommonSounds.Items.AddRange(Enum.GetNames(typeof(CommonSounds)));

            nudSharedLives.Value = mMyCommand.SharedLives;
            nudSharedLives.Enabled = cmbInstanceType.SelectedIndex == (int)MapInstanceType.Shared;
        }

        private void InitLocalization()
        {
            grpWarp.Text = Strings.EventWarp.title;
            lblMap.Text = Strings.Warping.map.ToString("");
            lblX.Text = Strings.Warping.x.ToString(scrlX.Value);
            lblY.Text = Strings.Warping.y.ToString(scrlY.Value);
            lblDir.Text = Strings.Warping.direction.ToString("");
            chkMapFade.Text = Strings.Warping.mapFade;
            btnVisual.Text = Strings.Warping.visual;
            cmbDirection.Items.Clear();
            for (var i = -1; i < 4; i++)
            {
                cmbDirection.Items.Add(Strings.Directions.dir[i]);
            }

            btnSave.Text = Strings.EventWarp.okay;
            btnCancel.Text = Strings.EventWarp.cancel;

            chkChangeInstance.Text = Strings.Warping.changeInstance;
            grpInstanceSettings.Text = Strings.Warping.mapInstancingGroup;
            lblInstanceType.Text = Strings.Warping.instanceType;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.MapId = MapList.OrderedMaps[cmbMap.SelectedIndex].MapId;
            mMyCommand.X = (byte) scrlX.Value;
            mMyCommand.Y = (byte) scrlY.Value;
            mMyCommand.Direction = (WarpDirection) cmbDirection.SelectedIndex;
            mMyCommand.FadeOnWarp = chkMapFade.Checked;
            mMyCommand.ChangeInstance = chkChangeInstance.Checked;
            mMyCommand.InstanceType = (MapInstanceType) cmbInstanceType.SelectedIndex;
            mMyCommand.DungeonId = DungeonDescriptor.IdFromList(cmbDungeon.SelectedIndex - 1);
            mMyCommand.Sound= TextUtils.SanitizeNone(cmbSound?.Text);
            mMyCommand.SharedLives = (int)nudSharedLives.Value;
            
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void scrlX_Scroll(object sender, ScrollValueEventArgs e)
        {
            lblX.Text = Strings.Warping.x.ToString(scrlX.Value);
        }

        private void scrlY_Scroll(object sender, ScrollValueEventArgs e)
        {
            lblY.Text = Strings.Warping.y.ToString(scrlY.Value);
        }

        private void btnVisual_Click(object sender, EventArgs e)
        {
            var frmWarpSelection = new FrmWarpSelection();
            frmWarpSelection.SelectTile(MapList.OrderedMaps[cmbMap.SelectedIndex].MapId, scrlX.Value, scrlY.Value);
            frmWarpSelection.ShowDialog();
            if (frmWarpSelection.GetResult())
            {
                for (var i = 0; i < MapList.OrderedMaps.Count; i++)
                {
                    if (MapList.OrderedMaps[i].MapId == frmWarpSelection.GetMap())
                    {
                        cmbMap.SelectedIndex = i;

                        break;
                    }
                }

                scrlX.Value = frmWarpSelection.GetX();
                scrlY.Value = frmWarpSelection.GetY();
                lblX.Text = Strings.Warping.x.ToString(scrlX.Value);
                lblY.Text = Strings.Warping.y.ToString(scrlY.Value);
            }
        }

        private void lblY_Click(object sender, EventArgs e)
        {
        }

        private void lblX_Click(object sender, EventArgs e)
        {
        }

        private void label23_Click(object sender, EventArgs e)
        {
        }

        private void cmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbMap_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label21_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void darkComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudSharedLives.Enabled = cmbInstanceType.SelectedIndex == (int)MapInstanceType.Shared;
        }

        private void chkCreateInstance_CheckedChanged(object sender, EventArgs e)
        {
            grpInstanceSettings.Visible = chkChangeInstance.Checked;
        }

        private void cmbCommonSounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sound = (CommonSounds)cmbCommonSounds.SelectedIndex;

            cmbSound.Text = sound.GetDescription();
        }
    }

}
