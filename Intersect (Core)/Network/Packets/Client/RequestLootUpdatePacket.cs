using MessagePack;

namespace Intersect.Network.Packets.Client
{
    public enum LootUpdateType
    {
        TakeAt,
        TakeAll,
        BankAt,
        BankAll,
        DismissAt,
        DismissAll,
    }

    [MessagePackObject]
    public class RequestLootUpdatePacket : IntersectPacket
    {
        // EF
        public RequestLootUpdatePacket() { }

        public RequestLootUpdatePacket(LootUpdateType type, int idx = -1)
        {
            Type = type;
            Index = idx;
        }

        [Key(0)]
        public LootUpdateType Type;

        [Key(1)]
        public int Index;
    }
}
