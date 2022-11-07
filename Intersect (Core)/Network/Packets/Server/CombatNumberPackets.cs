using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CombatNumberPackets : IntersectPacket
    {
        public CombatNumberPackets()
        {
        }

        public CombatNumberPackets(CombatNumberPacket[] packets)
        {
            Packets = packets;
        }

        [Key(0)]
        public CombatNumberPacket[] Packets { get; set; }
    }
}
