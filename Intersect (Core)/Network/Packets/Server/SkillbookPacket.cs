using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class SkillbookInstance : IntersectPacket
    {
        public SkillbookInstance()
        {
        }

        [Key(0)]
        public bool Prepared { get; set; }

        [Key(1)]
        public int PointsRequired { get; set; }

        public SkillbookInstance(bool prepared, int pointsRequired)
        {
            Prepared = prepared;
            PointsRequired = pointsRequired;
        }
    }

    [MessagePackObject]
    public class SkillbookPacket : IntersectPacket
    {
        [Key(0)]
        public Dictionary<Guid, SkillbookInstance> SkillBook { get; set; }

        [Key(1)]
        public int SkillPointsAvailable { get; set; }

        [Key(2)]
        public int SkillPointTotal { get; set; }

        public SkillbookPacket()
        {
        }

        public SkillbookPacket(Dictionary<Guid, SkillbookInstance> skillBook, int skillPointsAvailable, int skillPointTotal)
        {
            SkillBook = skillBook;
            SkillPointsAvailable = skillPointsAvailable;
            SkillPointTotal = skillPointTotal;
        }
    }
}
