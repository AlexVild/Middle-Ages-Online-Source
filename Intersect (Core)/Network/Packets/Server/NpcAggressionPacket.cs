using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class NpcAggressionPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public NpcAggressionPacket()
        {
        }

        public NpcAggressionPacket(Guid entityId, int aggression, Guid targetId)
        {
            EntityId = entityId;
            Aggression = aggression;
            TargetId = targetId;
        }

        [Key(0)]
        public Guid EntityId { get; set; }

        [Key(1)]
        public int Aggression { get; set; }
        
        [Key(2)]
        public Guid TargetId { get; set; }

    }

}
