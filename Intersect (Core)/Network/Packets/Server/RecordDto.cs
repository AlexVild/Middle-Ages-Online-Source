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

        [Key(2)]
        public int Index { get; set; }

        public RecordDto(string participants, string recordDisplay, int index)
        {
            Participants = participants;
            RecordDisplay = recordDisplay;
            Index = index;
        }
    }
}
