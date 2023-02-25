using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class CloseUpgradeStationPacket : IntersectPacket
    {
        public CloseUpgradeStationPacket()
        {
        }
    }
}
