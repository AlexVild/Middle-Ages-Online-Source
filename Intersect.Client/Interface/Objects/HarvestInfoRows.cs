using Intersect.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Client.Interface.Objects
{
    public class HarvestInfo
    {
        public string ResourceName { get; set; }

        public string ResourceTexture { get; set; }

        public string HarvestBonus { get; set; }

        public int HarvestLevel { get; set; }

        public long Remaining { get; set; }

        public float PercentRemaining { get; set; }

        public bool Harvestable { get; set; }

        public string CannotHarvestMessage { get; set; }

        public HarvestInfo(ResourceInfoPacket packet)
        {
            ResourceName = packet.ResourceName;
            ResourceTexture = packet.ResourceTexture;
            HarvestBonus = packet.HarvestBonus;
            HarvestLevel = packet.HarvestLevel;
            Remaining = packet.Remaining;
            PercentRemaining = packet.PercentRemaining;
            Harvestable = packet.Harvestable;
            CannotHarvestMessage = packet.CannotHarvestMessage;
        }
    }

    public static class HarvestInfoRows
    {
        public static List<HarvestInfo> CurrentRows = new List<HarvestInfo>();
    }
}
