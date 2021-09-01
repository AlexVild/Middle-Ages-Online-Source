using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class QuestPointPacket : IntersectPacket
    {
        [Key(0)]
        public string QuestPoints { get; set; }

        [Key(1)]
        public string LifetimeQuestPoints { get; set; }

        public QuestPointPacket(string questPoints, string lifetimeQp)
        {
            QuestPoints = questPoints;
            LifetimeQuestPoints = lifetimeQp;
        }
    }
}
