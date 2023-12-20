using MessagePack;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class ErrorMessagePacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public ErrorMessagePacket()
        {
        }

        public ErrorMessagePacket(string header, string error, bool resetUi)
        {
            Header = header;
            Error = error;
            ResetUi = resetUi;
        }

        [Key(0)]
        public string Header { get; set; }

        [Key(1)]
        public string Error { get; set; }

        [Key(2)]
        public bool ResetUi { get; set; }

    }

}
