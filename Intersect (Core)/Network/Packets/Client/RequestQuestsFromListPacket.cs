using System;
using System.Collections.Generic;
using Intersect.GameObjects.QuestList;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class RequestQuestsFromListPacket : IntersectPacket
    {
        public RequestQuestsFromListPacket() { } // EF

        public RequestQuestsFromListPacket(Guid questList)
        {
            this.QuestList = questList;
        }

        [Key(0)]
        public Guid QuestList { get; set; }
    }
}
