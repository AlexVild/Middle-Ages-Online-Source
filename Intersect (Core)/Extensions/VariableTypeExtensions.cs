using System.Reflection;
using Intersect.Attributes;
using Intersect.Enums;

namespace Intersect.Extensions;
public static class VariableTypeExtensions
{
    public static GameObjectType GetRelatedTable(this VariableType value)
    {
        if (!Enum.IsDefined(value))
        {
            throw new ArgumentException("Invalid VariableType enum", nameof(value));
        }

        string name = Enum.GetName(value);
        if (name == null)
        {
            throw new ArgumentException("Missing enum name", nameof(value));
        }

        FieldInfo fieldInfo = typeof(VariableType).GetField(name);
        if (fieldInfo == null)
        {
            throw new MissingFieldException("Reflection failed for VariableType enum", nameof(value));
        }

        RelatedTableAttribute attr = fieldInfo.GetCustomAttribute<RelatedTableAttribute>();
        if (attr == null)
        {
            throw new ArgumentException("Failed to get RelatedTable attribute for VariableType enum", nameof(value));
        }

        return attr.TableType;
    }
}