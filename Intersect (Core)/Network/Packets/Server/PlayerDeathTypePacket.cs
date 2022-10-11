using Intersect.GameObjects.Events;
using MessagePack;
using System.Collections.Generic;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class PlayerDeathTypePacket : IntersectPacket
    {
        public PlayerDeathTypePacket()
        {
        }

        public PlayerDeathTypePacket(DeathType type, long expLost, List<string> itemsLost)
        {
            Type = type;
            ExpLost = expLost;
            ItemsLost = itemsLost;
        }

        [Key(0)]
        public DeathType Type = DeathType.PvE;

        [Key(1)]
        public long ExpLost = -1;

        [Key(2)]
        public List<string> ItemsLost = new List<string>();
    }
}
