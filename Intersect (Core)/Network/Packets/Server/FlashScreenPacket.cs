using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class FlashScreenPacket : IntersectPacket
    {
        [Key(0)]
        public float Duration { get; set; }

        [Key(1)]
        public Color FlashColor { get; set; }

        [Key(2)]
        public float Intensity { get; set; }

        [Key(3)]
        public string SoundFile { get; set; }

        public FlashScreenPacket(float dur, Color col, float intensity, string soundFile)
        {
            Duration = dur;
            FlashColor = col;
            Intensity = intensity;
            SoundFile = soundFile;
        }
    }
}
