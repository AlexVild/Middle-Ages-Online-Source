using Intersect.GameObjects.Events;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class UnlockedBestiaryEntriesPacket : IntersectPacket
    {
        public UnlockedBestiaryEntriesPacket()
        {
        }

        [Key(0)]
        public Dictionary<Guid, Dictionary<BestiaryUnlock, bool>> BestiaryUnlocks;

        public UnlockedBestiaryEntriesPacket(Dictionary<Guid, Dictionary<BestiaryUnlock, bool>> bestiaryUnlocks)
        {
            BestiaryUnlocks = bestiaryUnlocks;
        }
    }
}
