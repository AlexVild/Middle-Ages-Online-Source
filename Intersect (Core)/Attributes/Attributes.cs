using Intersect.Enums;
using Intersect.GameObjects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Attributes
{
    public class RelatedTable : Attribute
    {
        public GameObjectType TableType { get; set; }

        public RelatedTable(GameObjectType db) { TableType = db; }
    }

    public class RecordType : Attribute
    {
        public GameObjects.Events.RecordType Type { get; set; }

        public RecordType(GameObjects.Events.RecordType db) { Type = db; }
    }

    // TODO this is dumb and maybe texture type should just be available at the core level
    public enum TextureTypeCore
    {
        Tileset = 0,

        Item,

        Entity,

        Spell,

        Animation,

        Face,

        Image,

        Fog,

        Resource,

        Paperdoll,

        Gui,

        Misc,
    }

    public class TextureType : Attribute
    {
        public TextureTypeCore Type { get; set; }

        public TextureType(TextureTypeCore t) { Type = t; }
    }
}
