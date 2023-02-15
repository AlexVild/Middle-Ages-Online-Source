using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ExpToastPacket : IntersectPacket
    {
        [Key(0)]
        public string ExpStr { get; set; }

        [Key(1)]
        public bool ComboEnder { get; set; }

        [Key(2)]
        public bool IsExp { get; set; }

        [Key(3)]
        public bool IsWeapon { get; set; }

        public ExpToastPacket()
        {
        }

        public ExpToastPacket(string exp, bool comboEnder, bool isExp, bool isWeapon)
        {
            ExpStr = exp;
            ComboEnder = comboEnder;
            IsExp = isExp;
            IsWeapon = isWeapon;
        }
    }
}
