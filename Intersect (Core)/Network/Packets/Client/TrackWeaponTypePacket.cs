using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class TrackWeaponTypePacket : IntersectPacket
    {
        [Key(0)]
        public Guid WeaponTypeId { get; set; }

        public TrackWeaponTypePacket(Guid weaponTypeId)
        {
            WeaponTypeId = weaponTypeId;
        }
    }
}
