using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class NpcEntityPacket : EntityPacket
    {
        //Parameterless Constructor for MessagePack
        public NpcEntityPacket()
        {
        }


        [Key(24)]
        public int Aggression { get; set; }

        [Key(25)]
        public Guid NpcId { get; set; }
    }

}
