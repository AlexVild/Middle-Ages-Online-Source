using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ToastPacket : IntersectPacket
    {
        [Key(0)]
        public string Message { get; set; }

        public ToastPacket()
        {
        }

        public ToastPacket(string message)
        {
            Message = message;
        }
    }
}
