using Intersect.Enums;
using System;

namespace Intersect.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnhancedRelatedTableAttribute : Attribute
    {
        public GameObjectType TableType { get; }

        public EnhancedRelatedTableAttribute(GameObjectType gameObjectType) { TableType = gameObjectType; }
    }
}
