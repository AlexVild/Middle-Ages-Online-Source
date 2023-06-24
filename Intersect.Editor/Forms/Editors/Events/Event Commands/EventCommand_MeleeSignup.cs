using Intersect.Editor.Localization;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps.MapList;
using System;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_MeleeSignup : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private MeleeSignupCommand mMyCommand;

        public EventCommand_MeleeSignup(MeleeSignupCommand refCommand, FrmEvent editor)
        {
            InitializeComponent();

            mEventEditor = editor;
            mMyCommand = refCommand;

            PopulateFields();

            PopulateForm();
        }

        private void PopulateFields()
        {
            cmbMapFight.Items.Clear();
            cmbMapRespawn.Items.Clear();

            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMapFight.Items.Add(MapList.OrderedMaps[i].Name);
                if (MapList.OrderedMaps[i].MapId == mMyCommand.DuelMapId)
                {
                    cmbMapFight.SelectedIndex = i;
                }
            }

            if (cmbMapFight.SelectedIndex == -1)
            {
                cmbMapFight.SelectedIndex = 0;
            }

            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMapRespawn.Items.Add(MapList.OrderedMaps[i].Name);
                if (MapList.OrderedMaps[i].MapId == mMyCommand.DuelMapId)
                {
                    cmbMapRespawn.SelectedIndex = i;
                }
            }

            if (cmbMapRespawn.SelectedIndex == -1)
            {
                cmbMapRespawn.SelectedIndex = 0;
            }

            scrlX.Maximum = Options.MapWidth - 1;
            scrlY.Maximum = Options.MapHeight - 1;

            cmbDir1.Items.Clear();
            cmbDir2.Items.Clear();
            cmbDirRespawn.Items.Clear();
            for (var i = 0; i < 4; i++)
            {
                cmbDir1.Items.Add(Strings.Directions.dir[i]);
                cmbDir2.Items.Add(Strings.Directions.dir[i]);
                cmbDirRespawn.Items.Add(Strings.Directions.dir[i]);
            }
        }

        private void PopulateForm()
        {
            scrlX.Value = mMyCommand.Duelist1X;
            scrlY.Value = mMyCommand.Duelist1Y;

            scrX2.Value = mMyCommand.Duelist2X;
            scrY2.Value = mMyCommand.Duelist2Y;

            scrXRespawn.Value = mMyCommand.RespawnX;
            scrYRespawn.Value = mMyCommand.RespawnY;

            cmbDir1.SelectedIndex = mMyCommand.Duelist1Dir;
            cmbDir2.SelectedIndex = mMyCommand.Duelist2Dir;
            cmbDirRespawn.SelectedIndex = mMyCommand.RespawnDir;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.Duelist1X = (byte)scrlX.Value;
            mMyCommand.Duelist1Y = (byte)scrlY.Value;
            mMyCommand.Duelist1Dir = (byte)cmbDir1.SelectedIndex;

            mMyCommand.Duelist2X = (byte)scrX2.Value;
            mMyCommand.Duelist2Y = (byte)scrY2.Value;
            mMyCommand.Duelist2Dir = (byte)cmbDir2.SelectedIndex;

            mMyCommand.RespawnX = (byte)scrXRespawn.Value;
            mMyCommand.RespawnY = (byte)scrYRespawn.Value;
            mMyCommand.RespawnDir = (byte)cmbDirRespawn.SelectedIndex;

            mMyCommand.DuelMapId = MapList.OrderedMaps[cmbMapFight.SelectedIndex].MapId;
            mMyCommand.RespawnMapId = MapList.OrderedMaps[cmbMapRespawn.SelectedIndex].MapId;

            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
