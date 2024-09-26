using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;

namespace Intersect.Server.General
{

    public static partial class Globals
    {

        public static readonly object ClientLock = new object();

        public static readonly IDictionary<Guid, Client> ClientLookup = new Dictionary<Guid, Client>();

        public static readonly List<Client> Clients = new List<Client>();

        public static Client[] ClientArray = new Client[0];

        public static long Cps = 0;

        public static List<Player> OnlineList => Clients.FindAll(client => client?.Entity != null)
            .Select(client => client.Entity)
            .ToList();

        public static void KillResourcesOf(ResourceBase resource)
        {
            foreach (MapController map in MapController.Lookup.Values)
            {
                map?.DespawnResourceAcrossInstances(resource);
            }
        }

        public static void KillNpcsOf(NpcBase npc)
        {
            foreach (MapController map in MapController.Lookup.Values)
            {
                map?.DespawnNpcAcrossInstances(npc);
            }
        }

        public static void KillProjectilesOf(ProjectileBase projectile)
        {
            foreach (MapController map in MapController.Lookup.Values)
            {
                map?.DespawnProjectileAcrossInstances(projectile);
            }
        }

        public static void KillItemsOf(ItemBase item)
        {
            foreach (MapController map in MapController.Lookup.Values)
            {
                map?.DespawnItemAcrossInstances(item);
            }
        }

    }

    public static partial class Globals
    {
        public static List<ResourceBase> CachedResources = new List<ResourceBase>();

        public static List<RecipeDescriptor> CachedRecipes = new List<RecipeDescriptor>();

        public static Dictionary<CommonEventTrigger, List<EventBase>> CachedTriggeredEvents = new Dictionary<CommonEventTrigger, List<EventBase>>();

        public static Dictionary<Guid, int> CachedNpcSpellScalar = new Dictionary<Guid, int>();

        public static void RefreshGameObjectCache<T>(GameObjectType type, List<T> cacheList)
        {
            cacheList.Clear();
            var objectName = Enum.GetName(typeof(GameObjectType), type);
            Logging.Log.Debug($"Caching game object {objectName}...");
            foreach (var v in type.GetLookup().ToList())
            {
                cacheList.Add((T)v.Value);
            }
            Logging.Log.Debug($"{objectName} objects cached");
        }

        public static void RefreshTriggeredEventsCache()
        {
            Logging.Log.Debug("Caching common events with triggers...");
            CachedTriggeredEvents.Clear();
            var count = 0;
            foreach (var evt in EventBase.Lookup.Values.Cast<EventBase>())
            {
                if (!evt.CommonEvent)
                {
                    continue;
                }

                foreach (var page in evt.Pages)
                {
                    var trigger = page.CommonTrigger;
                    if (trigger == CommonEventTrigger.None)
                    {
                        continue;
                    }
                    
                    if (CachedTriggeredEvents.ContainsKey(trigger))
                    {
                        count++;
                        CachedTriggeredEvents[trigger].Add(evt);
                        continue;
                    }

                    count++;
                    CachedTriggeredEvents.Add(trigger, new List<EventBase> { evt });
                }
            }

            Logging.Log.Debug($"Cached {count} events with triggers!");
        }

        public static void RefreshNpcSpellScalars()
        {
            Logging.Log.Debug($"Caching NPC spell scalars...");
            CachedNpcSpellScalar.Clear();
            CachedNpcSpellScalar = NpcBase.GenerateNpcSpellScalarDictionary();
            Logging.Log.Debug($"NPC scalars cached!");
        }
    }

}
