using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class LoadoutsPacket : IntersectPacket
    {
        [Key(0)]
        public Loadout[] Loadouts { get; set; }

        public LoadoutsPacket(Loadout[] loadouts)
        {
            Loadouts = loadouts;
        }

        public LoadoutsPacket()
        {
        }
    }

    [MessagePackObject]
    public class Loadout
    {
        [Key(0)]
        public Guid Id { get; set; }

        [Key(1)]
        public string Name { get; set; }

        public Loadout(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Loadout()
        {
        }
    }
}
