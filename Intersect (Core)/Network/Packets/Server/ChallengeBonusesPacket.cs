using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ChallengeBonusesPacket : IntersectPacket
    {
        // EF
        public ChallengeBonusesPacket()
        {
        }

        public ChallengeBonusesPacket(int[] bonuses)
        {
            Bonuses = bonuses;
        }

        [Key(0)]
        public int[] Bonuses { get; set; }
    }
}
