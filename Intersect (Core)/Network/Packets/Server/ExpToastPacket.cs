using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ExpToastPacket : IntersectPacket
    {
        [Key(0)]
        public long Exp { get; set; }

        [Key(1)]
        public bool ComboEnder { get; set; }

        public ExpToastPacket()
        {
        }

        public ExpToastPacket(long exp, bool comboEnder)
        {
            Exp = exp;
            ComboEnder = comboEnder;
        }
    }
}
