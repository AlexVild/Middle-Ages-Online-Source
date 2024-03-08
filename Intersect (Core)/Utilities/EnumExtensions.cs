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

        public static string GetClassName(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    ClassName attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(ClassName)) as ClassName;
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                }
            }
            return string.Empty;
        }

        public static string[] GetDescriptions(Type enumType, string except = null)
        {
            return Enum.GetValues(enumType)
                .Cast<Enum>()
                .Select(value => value.GetDescription())
                .Where(value => string.IsNullOrEmpty(except) || value != except)
                .ToArray();
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
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

        public static VariableTypes GetRelatedVariableType(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    RelatedVariableType attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(RelatedVariableType)) as RelatedVariableType;
                    if (attr != null)
                    {
                        return attr.VariableType;
                    }
                }
            }
            throw new ArgumentException($"{nameof(value)} did not have a valid VariableType attribute to pull from");
        }

        public static int GetDefaultKillCount(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DefaultKillCount attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DefaultKillCount)) as DefaultKillCount;
                    if (attr != null)
                    {
                        return attr.Value;
                    }
                }
            }
            return 0;
        }
    }
}
