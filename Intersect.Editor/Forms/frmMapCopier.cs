using Intersect.Editor.General;
using Intersect.Editor.Maps;
using Intersect.Editor.Networking;
using Intersect.GameObjects;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Maps.MapList;
using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Intersect.Editor.Forms
{
    public partial class frmMapCopier : Form
    {
        public frmMapCopier()
        {
            InitializeComponent();

            cmbMaps.Items.Clear();
            for (var i = 0; i < MapList.OrderedMaps.Count; i++)
            {
                cmbMaps.Items.Add(MapList.OrderedMaps[i].Name);
                if (MapList.OrderedMaps[i].MapId == Globals.LastMapCopied)
                {
                    cmbMaps.SelectedIndex = i;
                }
            }

            if (cmbMaps.SelectedIndex == -1)
            {
                cmbMaps.SelectedIndex = 0;
            }

            Globals.CopierOpened = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentMap == null || cmbMaps.SelectedIndex == -1 || cmbMaps.SelectedIndex >= MapList.OrderedMaps.Count)
            {
                return;
            }

            PacketSender.SendCopyMap(Globals.CurrentMap.Id, MapList.OrderedMaps[cmbMaps.SelectedIndex].MapId);

            Globals.LastMapCopied = MapList.OrderedMaps[cmbMaps.SelectedIndex].MapId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
            Dispose();
            Globals.CopierOpened = false;
        }
    }
}
