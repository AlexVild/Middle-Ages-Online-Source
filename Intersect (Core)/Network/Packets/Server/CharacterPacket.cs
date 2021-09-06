using MessagePack;
using System;

namespace Intersect.Network.Packets.Server
{
    [MessagePackObject]
    public class CharacterPacket : IntersectPacket
    {
        //Parameterless Constructor for MessagePack
        public CharacterPacket()
        {
        }

        public CharacterPacket(
            Guid id,
            string name,
            string sprite,
            string face,
            int level,
            string className,
            string[] equipment,
            string[] decor,
            bool hideHair,
            bool hideBeard,
            bool hideExtra
        )
        {
            Id = id;
            Name = name;
            Sprite = sprite;
            Face = face;
            Level = level;
            ClassName = className;
            Equipment = equipment;
            //Decor = decor;
            Decor = new string[] { "hair_short_blonde.png", "eyes_green.png", "shirt_tunic_blue.png", "extra_glasses.png", "beard_1_blonde.png" };
            HideHair = hideHair;
            HideBeard = hideBeard;
            HideExtra = hideExtra;
        }

        [Key(0)]
        public Guid Id { get; set; }

        [Key(1)]
        public string Name { get; set; }

        [Key(2)]
        public string Sprite { get; set; }

        [Key(3)]
        public string Face { get; set; }

        [Key(4)]
        public int Level { get; set; }

        [Key(5)]
        public string ClassName { get; set; }

        [Key(6)]
        public string[] Equipment { get; set; }

        [Key(7)]
        public string[] Decor { get; set; }

        [Key(8)]
        public bool HideHair { get; set; }

        [Key(9)]
        public bool HideBeard { get; set; }

        [Key(10)]
        public bool HideExtra { get; set; }
    }

}
