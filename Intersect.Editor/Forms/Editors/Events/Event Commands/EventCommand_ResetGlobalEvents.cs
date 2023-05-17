using Intersect.Editor.General;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps.MapList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    public partial class EventCommand_ResetGlobalEvents : UserControl
    {
        private readonly FrmEvent mEventEditor;

        private ResetGlobalEventPositions mMyCommand;

        public EventCommand_ResetGlobalEvents(ResetGlobalEventPositions refCommand, FrmEvent editor)
        {
            InitializeComponent();


            mMyCommand = refCommand;
            mEventEditor = editor;

            SetupFormValues();
        }

        private void SetupFormValues()
        {
            cmbMap.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMap.Items.Add(MapList.OrderedMaps[i].Name);
                if (mMyCommand.MapId != default && MapList.OrderedMaps[i].MapId == mMyCommand.MapId)
                {
                    cmbMap.SelectedIndex = i;
                }
                else if (mMyCommand == default && MapList.OrderedMaps[i].MapId == Globals.CurrentMap.Id)
                {
                    cmbMap.SelectedIndex = i;
                }
            }

            btnSave.Enabled = cmbMap.Items.Count >= 1;
        }

        private void SaveValues()
        {
            if (cmbMap.SelectedIndex >= 0)
            {
                mMyCommand.MapId = MapList.OrderedMaps[cmbMap.SelectedIndex].MapId;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveValues();
            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }
    }
}
