using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class FadePacket : IntersectPacket
    {
        // ef
        public FadePacket()
        {
        }

        public FadePacket(bool fadeIn)
        {
            FadeIn = fadeIn;
        }

        [Key(0)]
        public bool FadeIn { get; set; }
    }
}
