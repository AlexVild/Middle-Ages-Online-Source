using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class SkillbookPacket : IntersectPacket
    {
        [Key(0)]
        public List<Guid> SkillIds { get; set; }

        public SkillbookPacket()
        {
        }

        public SkillbookPacket(List<Guid> spells)
        {
            SkillIds = spells;
        }
    }
}
