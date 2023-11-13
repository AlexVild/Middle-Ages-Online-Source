using System;
using System.Collections.Generic;
using System.Linq;

using Intersect.Collections;
using Intersect.Extensions;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Models;

namespace Intersect.Enums
{
    public static partial class GameObjectTypeExtensions
    {
        static GameObjectTypeExtensions()
        {
            EnumType = typeof(GameObjectType);
            AttributeType = typeof(GameObjectInfoAttribute);
            AttributeMap = new Dictionary<GameObjectType, GameObjectInfoAttribute>();

            foreach (GameObjectType gameObjectType in Enum.GetValues(EnumType))
            {
                var memberInfo = EnumType.GetMember(Enum.GetName(EnumType, value: gameObjectType)).FirstOrDefault();
                AttributeMap[gameObjectType] =
                    (GameObjectInfoAttribute) memberInfo?.GetCustomAttributes(AttributeType, false).FirstOrDefault();
            }
        }

        private static Type EnumType { get; }

        private static Type AttributeType { get; }

        private static Dictionary<GameObjectType, GameObjectInfoAttribute> AttributeMap { get; }

        public static Type GetObjectType(this GameObjectType gameObjectType)
        {
            return AttributeMap?[gameObjectType]?.Type;
        }

        public static string GetTable(this GameObjectType gameObjectType)
        {
            return AttributeMap?[gameObjectType]?.Table;
        }

        public static DatabaseObjectLookup GetLookup(this GameObjectType gameObjectType)
        {
            return LookupUtils.GetLookup(GetObjectType(gameObjectType));
        }

        public static IDatabaseObject CreateNew(this GameObjectType gameObjectType)
        {
            var instance = Activator.CreateInstance(
                AttributeMap?[gameObjectType]?.Type,
                new object[] { }
            );

            return instance as IDatabaseObject;
        }

        public static int ListIndex(this GameObjectType gameObjectType, Guid id, VariableDataType dataTypeFilter = 0)
        {
            var lookup = gameObjectType.GetLookup();

            if (dataTypeFilter == 0)
            {
                return lookup.KeyList.OrderBy(pairs => lookup[pairs]?.Name).ToList().IndexOf(id);
            }

            try
            {
                return lookup
                    .OrderBy(kv => kv.Value?.Name)
                    .Select(kv => kv.Value)
                    .Cast<IVariableBase>()
                    .Where(desc => desc.Type == dataTypeFilter)
                    .Select(desc => desc.Id)
                    .ToList()
                    .IndexOf(id);
            }
            catch (InvalidCastException e)
            {
                Logging.Log.Error($"Invalid cast! A VariableDataFilter was probably passed for a game object that does not inherit IVariableBase!");
                return -1;
            }
        }

        public static VariableDataType GetVariableType(this GameObjectType gameObjectType, Guid variableDescriptorId)
        {
            var lookup = gameObjectType.GetLookup();

            try
            {
                return lookup.ValueList
                    .Cast<IVariableBase>()
                    .FirstOrDefault(var => var.Id == variableDescriptorId)?.Type ?? 0;
            }
            catch (InvalidCastException e)
            {
                Logging.Log.Error($"Invalid cast! Tried to get a variable value for a GameObject that is not represented by a class inheriting IVariableBase!");
                return 0;
            }
        }

        public static Guid IdFromList(this GameObjectType gameObjectType, int listIndex, VariableDataType dataTypeFilter = 0)
        {
            var lookup = gameObjectType.GetLookup();

            if (listIndex < 0 || listIndex > lookup.KeyList.Count)
            {
                return Guid.Empty;
            }

            if (dataTypeFilter == 0)
            {
                return lookup.KeyList.OrderBy(pairs => lookup[pairs]?.Name).ToArray()[listIndex];
            }

            try
            {
                return lookup
                    .OrderBy(kv => kv.Value?.Name)
                    .Select(kv => kv.Value)
                    .Cast<IVariableBase>()
                    .Where(desc => desc.Type == dataTypeFilter)
                    .Select(desc => desc.Id)
                    .ToArray()[listIndex];
            }
            catch (InvalidCastException e)
            {
                Logging.Log.Error($"Invalid cast! A VariableDataFilter was probably passed for a game object that does not inherit IVariableBase!");
                return Guid.Empty;
            }
        }

        public static string[] Names(this GameObjectType gameObjectType, VariableDataType dataTypeFilter = 0) 
        {
            if (dataTypeFilter == 0)
            {
                return gameObjectType
                    .GetLookup()
                    .OrderBy(p => p.Value?.Name)
                    .Select(pair => pair.Value?.Name ?? Deleted)
                    .ToArray();
            }

            try
            {
                return gameObjectType
                    .GetLookup()
                    .Select(kv => kv.Value)
                    .Cast<IVariableBase>()
                    .Where(desc => desc.Type == dataTypeFilter)
                    .OrderBy(p => p?.Name)
                    .Select(pair => pair?.Name ?? Deleted)
                    .ToArray();
            }
            catch (InvalidCastException e)
            {
                Logging.Log.Error($"Invalid cast! A VariableDataFilter was probably passed for a game object that does not inherit IVariableBase!");
                return new string[0];
            }
        } 

        public const string Deleted = "ERR_DELETED";
    }
}
