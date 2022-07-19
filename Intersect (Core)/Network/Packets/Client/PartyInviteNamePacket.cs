using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class PartyInviteNamePacket : IntersectPacket
    {
        [Key(0)]
        public string Name { get; set; }

        public PartyInviteNamePacket(string name)
        {
            Name = name;
        }
    }
}
