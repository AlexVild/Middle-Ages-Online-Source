using MessagePack;
namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class SendQuestPointRequestPacket : IntersectPacket
    {
        public SendQuestPointRequestPacket() { } // All intentionally blank - just a message to send to the server
    }
}
