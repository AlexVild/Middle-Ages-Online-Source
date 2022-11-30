using Intersect.Enums;
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

}
