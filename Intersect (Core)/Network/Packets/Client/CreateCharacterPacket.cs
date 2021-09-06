using System;
using System.Collections.Generic;

using Intersect.Collections;
using Intersect.GameObjects;
using MessagePack;

namespace Intersect.Network.Packets.Client
{
    [MessagePackObject]
    public class CreateCharacterPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public CreateCharacterPacket()
        {
        }

        public CreateCharacterPacket(string name, Guid classId, int sprite, string[] decors)
        {
            Name = name;
            ClassId = classId;
            Sprite = sprite;
            Decors = decors;
        }

        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public Guid ClassId { get; set; }

        [Key(2)]
        public int Sprite { get; set; }

        [Key(3)]
        public string[] Decors { get; set; }

        public override Dictionary<string, SanitizedValue<object>> Sanitize()
        {
            base.Sanitize();

            var sanitizer = new Sanitizer();

            var classDescriptor = ClassBase.Get(ClassId);
            if (classDescriptor != null)
            {
                Sprite = sanitizer.Clamp(nameof(Sprite), Sprite, 0, classDescriptor.Sprites?.Count ?? 0);
            }
            if (Decors.Length != Options.DecorSlots.Count)
            {
                Decors = new string[Options.DecorSlots.Count];
            }

            return sanitizer.Sanitized;
        }

    }

}
