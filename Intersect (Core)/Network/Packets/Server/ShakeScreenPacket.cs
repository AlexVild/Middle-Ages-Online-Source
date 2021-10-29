using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ShakeScreenPacket : IntersectPacket
    {
        // Empty for EF
        public ShakeScreenPacket() { }

        public ShakeScreenPacket(float intensity)
        {
            Intensity = intensity;
        }

        [Key(0)]
        public float Intensity;
    }
}
