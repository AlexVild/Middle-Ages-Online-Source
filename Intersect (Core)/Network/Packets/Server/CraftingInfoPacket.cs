using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CraftingInfoPacket : IntersectPacket
    {
        [Key(0)]
        public string MiningTier { get; set; }
        [Key(1)]
        public string FishingTier { get; set; }
        [Key(2)]
        public string WoodcutTier { get; set; }
        [Key(3)]
        public Dictionary<string, int> ClassRanks { get; set; }

        public CraftingInfoPacket(string miningTier, string fishingTier, string woodcutTier, Dictionary<string, int> classRanks)
        {
            MiningTier = miningTier;
            FishingTier = fishingTier;
            WoodcutTier = woodcutTier;
            ClassRanks = classRanks;
        }
    }
}
