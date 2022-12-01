using System;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using Intersect.Attributes;
using Intersect.Enums;

namespace Intersect.Utilities
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return string.Empty;
        }
        public static string[] GetDescriptions(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<Enum>().Select(value => value.GetDescription()).ToArray();
        }

        public static GameObjectType GetRelatedTable(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    RelatedTable attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(RelatedTable)) as RelatedTable;
                    if (attr != null)
                    {
                        return attr.TableType;
                    }
                }
            }
            throw new ArgumentException($"{nameof(value)} did not have a valid RelatedTable attribute to pull from");
        }

        public static GameObjects.Events.RecordType GetRelatedRecordType(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    RecordType attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(RecordType)) as RecordType;
                    if (attr != null)
                    {
                        return attr.Type;
                    }
                }
            }
            throw new ArgumentException($"{nameof(value)} did not have a valid RelatedTable attribute to pull from");
        }
    }
}
