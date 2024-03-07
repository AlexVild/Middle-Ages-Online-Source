using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ClanWarCompletePacket : IntersectPacket
    {
        public ClanWarCompletePacket()
        {
        }

        public ClanWarCompletePacket(int payout, int placement)
        {
            Payout = payout;
            Placement = placement;
        }

        [Key(0)]
        public int Payout {  get; set; }

        [Key(1)]
        public int Placement { get; set; }
    }
}
