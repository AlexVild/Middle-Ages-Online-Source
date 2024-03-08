using System;
using System.Collections.Generic;
using System.Linq;

using Intersect.Collections;
using Intersect.Extensions;
using Intersect.GameObjects;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Models;

namespace Intersect.Enums
{

    public static class GameObjectTypeExtensions
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
            var instance = Activator.CreateInstance(AttributeMap?[gameObjectType]?.Type, new object[] { });

            return instance as IDatabaseObject;
        }

        public static int ListIndex(this GameObjectType gameObjectType, Guid id)
        {
            var lookup = gameObjectType.GetLookup();
            return lookup.KeyList.OrderBy(pairs => lookup[pairs]?.Name).ToList().IndexOf(id);
        }

        public static Guid IdFromList(this GameObjectType gameObjectType, int listIndex)
        {
            var lookup = gameObjectType.GetLookup();
            if (listIndex < 0 || listIndex > lookup.KeyList.Count)
            {
                return Guid.Empty;
            }

            return lookup.KeyList.OrderBy(pairs => lookup[pairs]?.Name).ToArray()[listIndex];
        }

        public static string[] Names(this GameObjectType gameObjectType) => gameObjectType.GetLookup().OrderBy(p => p.Value?.Name)
            .Select(pair => pair.Value?.Name ?? Deleted)
            .ToArray();

        public static string GetName(this GameObjectType gameObjectType, Guid id) => gameObjectType.GetLookup().Get(id)?.Name ?? Deleted;

        public static int ListIndex(this GameObjectType gameObjectType, Guid id, VariableDataTypes dataTypeFilter = 0)
        {
            var lookup = gameObjectType.GetLookup();

            if (dataTypeFilter == 0)
            {
                return lookup.KeyList.OrderBy(pairs => lookup[pairs]?.Name).ToList().IndexOf(id);
            }

            return lookup
                .OrderBy(kv => kv.Value?.Name)
                .Select(kv => kv.Value)
                .OfType<IVariableBase>()
                .Where(desc => desc.Type == dataTypeFilter)
                .Select(desc => desc.Id)
                .ToList()
                .IndexOf(id);
        }

        public static Guid IdFromList(this GameObjectType gameObjectType, int listIndex, VariableDataTypes dataTypeFilter = 0)
        {
            var lookup = gameObjectType.GetLookup();

            if (listIndex < 0 || listIndex >= lookup.KeyList.Count)
            {
                return Guid.Empty;
            }

            if (dataTypeFilter == 0)
            {
                return lookup.KeyList.OrderBy(pairs => lookup[pairs]?.Name).ToArray()[listIndex];
            }

            return lookup
                .OrderBy(kv => kv.Value?.Name)
                .Select(kv => kv.Value)
                .OfType<IVariableBase>()
                .Where(desc => desc.Type == dataTypeFilter)
                .Select(desc => desc.Id)
                .Skip(listIndex)
                .FirstOrDefault();
        }

        public static VariableDataTypes GetVariableType(this GameObjectType gameObjectType, Guid variableDescriptorId)
        {
            var lookup = gameObjectType.GetLookup();

            return lookup.ValueList
                    .OfType<IVariableBase>()
                    .FirstOrDefault(var => var.Id == variableDescriptorId)?.Type ?? 0;
        }

        public static string[] Names(this GameObjectType gameObjectType, VariableDataTypes dataTypeFilter = 0)
        {
            if (dataTypeFilter == 0)
            {
                return gameObjectType
                    .GetLookup()
                    .OrderBy(p => p.Value?.Name)
                    .Select(pair => pair.Value?.Name ?? PlayerVariableBase.Deleted)
                    .ToArray();
            }

            return gameObjectType
                .GetLookup()
                .Select(kv => kv.Value)
                .OfType<IVariableBase>()
                .Where(desc => desc.Type == dataTypeFilter)
                .OrderBy(p => p?.Name)
                .Select(pair => pair?.Name ?? PlayerVariableBase.Deleted)
                .ToArray();
        }

        public const string Deleted = "ERR_DELETED";

    }

}
