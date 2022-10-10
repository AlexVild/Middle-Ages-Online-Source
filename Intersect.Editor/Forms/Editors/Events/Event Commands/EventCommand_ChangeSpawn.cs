using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps.MapList;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ChangeSpawn : UserControl
    {
        private FrmEvent mEventEditor;

        private ChangeSpawnCommand mCommand;

        public EventCommand_ChangeSpawn(ChangeSpawnCommand refCommand, FrmEvent editor)
        {
            mCommand = refCommand;
            mEventEditor = editor;

            InitializeComponent();

            InitEditor();
        }

        private void InitEditor()
        {
            rdoArena.Checked = mCommand.RespawnType == GameObjects.Events.RespawnChangeType.Arena;
            rdoDefault.Checked = mCommand.RespawnType == GameObjects.Events.RespawnChangeType.Default;

            cmbMap.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMap.Items.Add(MapList.OrderedMaps[i].Name);
                if (MapList.OrderedMaps[i].MapId == mCommand.MapId)
                {
                    cmbMap.SelectedIndex = i;
                }
            }
            if (cmbMap.SelectedIndex == -1 && cmbMap.Items.Count > 0)
            {
                cmbMap.SelectedIndex = 0;
            }
            
            cmbDirection.Items.Clear();
            for (var i = -1; i < 4; i++)
            {
                cmbDirection.Items.Add(Strings.Directions.dir[i]);
            }

            scrlX.Value = mCommand.X;
            scrlY.Value = mCommand.Y;
            cmbDirection.SelectedIndex = (int)mCommand.Direction;
            chkResetSpawn.Checked = mCommand.Reset;
        }

        private void UpdateLabels()
        {
            lblX.Text = Strings.Warping.x.ToString(scrlX.Value);
            lblY.Text = Strings.Warping.x.ToString(scrlY.Value);
        }

        private void scrlX_ValueChanged(object sender, DarkUI.Controls.ScrollValueEventArgs e)
        {
            UpdateLabels();
        }

        private void scrlY_ValueChanged(object sender, DarkUI.Controls.ScrollValueEventArgs e)
        {
            UpdateLabels();
        }

        private void Save()
        {
            mCommand.RespawnType = rdoArena.Checked ? GameObjects.Events.RespawnChangeType.Arena : GameObjects.Events.RespawnChangeType.Default;
            mCommand.MapId = MapList.OrderedMaps[cmbMap.SelectedIndex].MapId;
            mCommand.X = (byte)scrlX.Value;
            mCommand.Y = (byte)scrlY.Value;
            mCommand.Direction = (Directions)cmbDirection.SelectedIndex;
            mCommand.Reset = chkResetSpawn.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
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

        private void chkResetSpawn_CheckedChanged(object sender, EventArgs e)
        {
            cmbMap.Enabled = !chkResetSpawn.Checked;
            scrlX.Enabled = !chkResetSpawn.Checked;
            scrlY.Enabled = !chkResetSpawn.Checked;
            cmbDirection.Enabled = !chkResetSpawn.Checked;
        }
    }
}
