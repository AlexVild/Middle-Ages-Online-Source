using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class QuestOfferListPacket : IntersectPacket
    {
        public QuestOfferListPacket() { } // Empty for EF

        public QuestOfferListPacket(List<Guid> questsInList)
        {
            Quests = questsInList;
        }

        [Key(0)]
        public List<Guid> Quests { get; set; }
    }
}
