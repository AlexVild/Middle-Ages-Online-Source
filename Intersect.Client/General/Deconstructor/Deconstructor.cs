using Intersect.Client.Items;
using Intersect.Client.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.General.Deconstructor
{
    public class Deconstructor
    {
        public int Fuel { get; set; }

        public List<Item> Items { get; set; }

        public bool AddFuel { get; set; }

        public bool IsOpen { get; set; } = false;

        public void Open()
        {
            IsOpen = true;
            Interface.Interface.GameUi?.DeconstructorWindow?.Show();
        }

        public void Close()
        {
            IsOpen = false;
            PacketSender.SendCloseDeconstructorPacket();
        }
    }
}
