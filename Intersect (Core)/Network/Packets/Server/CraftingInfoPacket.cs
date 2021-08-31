using MessagePack;

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
        public string NPCGuildName { get; set; }
        [Key(4)]
        public string ClassRank { get; set; }

        public CraftingInfoPacket(string miningTier, string fishingTier, string woodcutTier, string npcGuildName, string classRank)
        {
            MiningTier = miningTier;
            FishingTier = fishingTier;
            WoodcutTier = woodcutTier;
            NPCGuildName = npcGuildName;
            ClassRank = classRank;
        }
    }
}
