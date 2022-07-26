using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Network.Packets.Server
{
    public class RecordDto
    {
        public string Participants { get; set; }

        public string RecordDisplay { get; set; }

        public RecordDto(string participants, string recordDisplay)
        {
            Participants = participants;
            RecordDisplay = recordDisplay;
        }
    }

    [MessagePackObject]
    public class RecordsPacket : IntersectPacket
    {
        [Key(0)]
        public string RecordsJson { get; set; }

        RecordsPacket(List<RecordDto> records)
        {
            
        }
    }
}
