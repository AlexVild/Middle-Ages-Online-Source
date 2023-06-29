using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class SpellCastPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public SpellCastPacket()
        {
        }

        public SpellCastPacket(Guid entityId, Guid spellId, Guid targetId)
        {
            EntityId = entityId;
            SpellId = spellId;
            TargetId = targetId;
        }

        [Key(0)]
        public Guid EntityId { get; set; }

        [Key(1)]
        public Guid SpellId { get; set; }

        [Key(2)]
        public Guid TargetId { get; set; }

    }

}
