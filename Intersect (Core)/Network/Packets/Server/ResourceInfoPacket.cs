using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ResourceInfoPacket : IntersectPacket
    {
        public ResourceInfoPacket() { }

        [Key(0)]
        public string ResourceName { get; set; }

        [Key(1)]
        public string ResourceTexture { get; set; }

        [Key(2)]
        public string HarvestBonus { get; set; }

        [Key(3)]
        public int HarvestLevel { get; set; }

        [Key(4)]
        public long Remaining { get; set; }

        [Key(5)]
        public float PercentRemaining { get; set; }

        [Key(6)]
        public bool Harvestable { get; set; }

        [Key(7)]
        public string CannotHarvestMessage { get; set; }

        public ResourceInfoPacket(string resourceName, string resourceTexture, string harvestBonus, int harvestLevel, long remaining, float percentRemaining, bool harvestable, string cannotHarvestMessage)
        {
            ResourceName = resourceName;
            ResourceTexture = resourceTexture;
            HarvestBonus = harvestBonus;
            HarvestLevel = harvestLevel;
            Remaining = remaining;
            PercentRemaining = percentRemaining;
            Harvestable = harvestable;
            CannotHarvestMessage = cannotHarvestMessage;
        }
    }

    [MessagePackObject]
    public class ResourceInfoPackets : IntersectPacket
    {
        [Key(0)]
        public List<ResourceInfoPacket> Packets;

        public ResourceInfoPackets()
        {
            Packets = new List<ResourceInfoPacket>();
        }

        public ResourceInfoPackets(List<ResourceInfoPacket> packets)
        {
            Packets = packets;
        }
    }
}
