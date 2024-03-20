using MessagePack;
using Intersect.Enums;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class UpdateFutureWarpPacket : IntersectPacket
    {
        public UpdateFutureWarpPacket()
        {
        }
    }
}
