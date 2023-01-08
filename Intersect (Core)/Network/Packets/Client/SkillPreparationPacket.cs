using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class SkillPreparationPacket : IntersectPacket
    {
        [Key(0)]
        public Guid SpellId { get; set; }

        [Key(1)]
        public bool Prepared { get; set; }

        public SkillPreparationPacket()
        {
        }

        public SkillPreparationPacket(Guid spellId, bool prepared)
        {
            SpellId = spellId;
            Prepared = prepared;
        }
    }
}
