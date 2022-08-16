using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class RecordDto
    {
        [Key(0)]
        public string Participants { get; set; }

        [Key(1)]
        public string RecordDisplay { get; set; }

        public RecordDto(string participants, string recordDisplay)
        {
            Participants = participants;
            RecordDisplay = recordDisplay;
        }
    }
}
