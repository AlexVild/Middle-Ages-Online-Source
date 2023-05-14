using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class SaveNewLoadoutPacket : IntersectPacket
    {
        [Key(0)]
        public string LoadoutName { get; set; }

        public SaveNewLoadoutPacket(string loadoutName)
        {
            LoadoutName = loadoutName;
        }

        //EF
        public SaveNewLoadoutPacket()
        {
        }
    }
}
