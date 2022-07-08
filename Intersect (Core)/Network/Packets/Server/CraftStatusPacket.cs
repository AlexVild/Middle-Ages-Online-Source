using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CraftStatusPacket : IntersectPacket
    {
        [Key(0)]
        public int AmountRemaining;

        public CraftStatusPacket(int amountRemaining)
        {
            AmountRemaining = amountRemaining;
        }
    }
}
