using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class InstanceLivesPacket : IntersectPacket
    {

        [Key(0)]
        public bool ClearLives;

        [Key(1)]
        public byte Amount;

        public InstanceLivesPacket(bool clearLives, byte amount)
        {
            ClearLives = clearLives;
            Amount = amount;
        }
    }
}
