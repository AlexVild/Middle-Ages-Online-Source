using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PlayerFuelPacket : IntersectPacket
    {
        [Key(0)]
        public int Fuel { get; set; }

        public PlayerFuelPacket()
        {
        }

        public PlayerFuelPacket(int fuel)
        {
            Fuel = fuel;
        }
    }
}
