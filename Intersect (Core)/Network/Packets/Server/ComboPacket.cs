using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ComboPacket : IntersectPacket
    {
        public ComboPacket(int comboSize, int comboWindow, int bonusExp, int maxComboWindow)
        {
            ComboSize = comboSize;
            ComboWindow = comboWindow;
            BonusExp = bonusExp;
            MaxComboWindow = maxComboWindow;
        }

        [Key(0)]
        public int ComboSize { get; set; }

        [Key(1)]
        public int ComboWindow { get; set; }

        [Key(2)]
        public int BonusExp { get; set; }
        
        [Key(3)]
        public int MaxComboWindow { get; set; }
    }
}
