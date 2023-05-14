using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ConfirmLoadoutOverwritePacket : IntersectPacket
    {
        [Key(0)]
        public Guid LoadoutId { get; set; }

        public ConfirmLoadoutOverwritePacket(Guid loadoutId)
        {
            LoadoutId = loadoutId;
        }

        public ConfirmLoadoutOverwritePacket()
        {
        }
    }
}
