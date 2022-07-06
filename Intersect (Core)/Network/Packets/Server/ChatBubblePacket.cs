using System;

using Intersect.Enums;
using MessagePack;

namespace Intersect.Network.Packets.Server
{
    public enum ChatBubbleType
    {
        Public,
        Party,
        Guild
    }

    [MessagePackObject]
    public class ChatBubblePacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public ChatBubblePacket()
        {
        }

        public ChatBubblePacket(Guid entityId, EntityTypes type, Guid mapId, string text, ChatBubbleType bubbleType)
        {
            EntityId = entityId;
            Type = type;
            MapId = mapId;
            Text = text;
            BubbleType = bubbleType;
        }

        [Key(0)]
        public Guid EntityId { get; set; }

        [Key(1)]
        public EntityTypes Type { get; set; }

        [Key(2)]
        public Guid MapId { get; set; }

        [Key(3)]
        public string Text { get; set; }

        [Key(4)]
        public ChatBubbleType BubbleType { get; set; }

    }

}
