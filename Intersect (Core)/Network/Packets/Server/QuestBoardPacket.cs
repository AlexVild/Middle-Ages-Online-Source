using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class QuestBoardPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public QuestBoardPacket()
        {
        }

        public QuestBoardPacket(String questBoardData, bool close)
        {
            QuestBoardData = questBoardData;
            Close = close;
        }

        [Key(0)]
        public string QuestBoardData { get; set; }

        [Key(1)]
        public bool Close { get; set; }
    }
}
