using MessagePack;
using System;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class QuestResponsePacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public QuestResponsePacket()
        {
        }

        public QuestResponsePacket(Guid questId, bool accepting, bool fromQuestBoard)
        {
            QuestId = questId;
            AcceptingQuest = accepting;
            FromQuestBoard = fromQuestBoard;
        }

        [Key(0)]
        public Guid QuestId { get; set; }

        [Key(1)]
        public bool AcceptingQuest { get; set; }

        [Key(2)]
        public bool FromQuestBoard { get; set; }

    }

}
