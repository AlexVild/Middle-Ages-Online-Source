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
}
