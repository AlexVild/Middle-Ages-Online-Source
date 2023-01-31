using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class SkillUpdatePacket : IntersectPacket
    {
        [Key(0)]
        public string UpdateText { get; set; }

        public SkillUpdatePacket()
        {
        }

        public SkillUpdatePacket(string updateText)
        {
            UpdateText = updateText;
        }
    }
}
