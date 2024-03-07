using Intersect.Attributes;
using Intersect.Enums;
using System;
using System.Reflection;

namespace Intersect.Extensions
{
    public static class VariableTypeExtensions
    {
        public static GameObjectType GetVariableTable(this VariableTypes value)
        {
            if (!Enum.IsDefined(typeof(VariableTypes), value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid VariableType enum");
            }

            string name = Enum.GetName(typeof(VariableTypes), value);
            if (name == null)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Missing enum name");
            }

            FieldInfo fieldInfo = typeof(VariableTypes).GetField(name);
            if (fieldInfo == null)
            {
                throw new MissingFieldException($"Reflection failed for VariableType enum, value was {value}", nameof(value));
            }

            EnhancedRelatedTableAttribute attr = fieldInfo.GetCustomAttribute<EnhancedRelatedTableAttribute>();
            if (attr == null)
            {
                throw new ArgumentException($"Failed to get RelatedTable attribute for VariableType enum, value was {value}", nameof(value));
            }

            return attr.TableType;
        }
    }
}
