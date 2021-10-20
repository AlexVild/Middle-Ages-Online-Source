using MessagePack;
using System;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class QuestBoardPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public QuestBoardPacket()
        {
        }

        public QuestBoardPacket(String questBoardData, bool close, Dictionary<Guid, bool> requirementsForQuestLists)
        {
            QuestBoardData = questBoardData;
            Close = close;
            RequirementsForQuestLists = requirementsForQuestLists;
        }

        [Key(0)]
        public string QuestBoardData { get; set; }

        [Key(1)]
        public bool Close { get; set; }

        [Key(2)]
        public Dictionary<Guid, bool> RequirementsForQuestLists { get; set; }
    }
}
