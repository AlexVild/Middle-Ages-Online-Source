using MessagePack;
using Intersect.Enums;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class GUINotificationPacket : IntersectPacket
    {
        [Key(0)]
        public GUINotification Notification { get; set; }

        [Key(1)]
        public bool DisplayNotification { get; set; }

        public GUINotificationPacket(GUINotification notification, bool displayNotification)
        {
            Notification = notification;
            DisplayNotification = displayNotification;
        }
    }
}
