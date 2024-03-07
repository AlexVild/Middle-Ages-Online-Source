using System;
using System.Collections.Generic;
using Intersect.Enums;
using Intersect.GameObjects.Maps;
using Intersect.Logging;
using Intersect.Server.Database;
using Intersect.Utilities;
using Intersect.Server.Core.Games.ClanWars;
using Intersect.Server.Entities;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database.PlayerData.Players;

namespace Intersect.Server.Maps
{
    public class MapTerritory
    {
        public MapTerritory(TerritoryInfo mapInfo, TerritoryInstance instance)
        {
            MapInfo = mapInfo;
            Instance = instance;
        }

        public TerritoryInfo MapInfo { get; set; }

        public TerritoryInstance Instance { get; set; }
    }

    public class TerritoryInfo
    {
        public TerritoryInfo(BytePoint spawnPos, int radius)
        {
            SpawnPos = spawnPos;
            Radius = radius;
        }

        public BytePoint SpawnPos { get; set; }
        public int Radius { get; set; }
    }

    public partial class MapInstance : IDisposable
    {
        public Dictionary<Guid, MapTerritory> ActiveTerritories { get; set; } = new Dictionary<Guid, MapTerritory>();

        private Dictionary<Guid, TerritoryInfo> mCachedTerritories { get; set; } = new Dictionary<Guid, TerritoryInfo>();

        public Dictionary<BytePoint, TerritoryInstance> TerritoryTiles { get; set; } = new Dictionary<BytePoint, TerritoryInstance>();

        private void CacheTerritorySpawners()
        {
            mCachedTerritories.Clear();
            for (byte x = 0; x < Options.MapWidth; x++)
            {
                for (byte y = 0; y < Options.MapHeight; y++)
                {
                    var attribute = mMapController.Attributes[x, y];
                    if (attribute == null || attribute.Type != MapAttributes.Territory)
                    {
                        continue;
                    }

                    try
                    {
                        var territoryAttr = attribute as MapTerritoryAttribute;

                        if (mCachedTerritories.ContainsKey(territoryAttr.TerritoryId))
                        {
                            throw new InvalidOperationException($"Can not have two of the same territory on the same map! Map: {mMapController.Name}");
                        }
                        mCachedTerritories[territoryAttr.TerritoryId] = new TerritoryInfo(new BytePoint(x, y), territoryAttr.Radius);
                    }
                    catch (InvalidCastException)
                    {
#if DEBUG
                        throw;
#else
                        Log.Error($"Invalid cast exception when caching map territories");
#endif
                    }
                }
            }
        }

        public void ClearTerritoryInstances()
        {
            if (!ClanWarManager.ClanWarActive)
            {
                foreach (var territory in ActiveTerritories.Values)
                {
                    DbInterface.Pool.QueueWorkItem(territory.Instance.RemoveFromDb);
                }
            }
            ActiveTerritories.Clear();
            TerritoryTiles.Clear();
        }

        public void InitializeTerritories()
        {
            ClearTerritoryInstances();
            // Don't bother if a) overworld, b) map has no territory attributes or c) clan wars isn't active
            if (IsOverworld || mCachedTerritories?.Count == 0 || !ClanWarManager.ClanWarActive)
            {
                return;
            }

            lock (GetLock())
            {
                var clanWarId = ClanWarManager.CurrentWarId;

                using (var context = DbInterface.CreatePlayerContext(false))
                {
                    foreach (var cachedTerritory in mCachedTerritories)
                    {
                        var territoryId = cachedTerritory.Key;
                        var territoryInfo = cachedTerritory.Value;
                        
                        // Check to see if this territory has already been instantiated for the current CW
                        var territoryInstance = context.Territories.Find(territoryId, clanWarId, mMapController?.Id ?? Guid.Empty, MapInstanceId);
                        if (territoryInstance != null)
                        {
                            territoryInstance.Initialize();
                        }
                        else
                        {
                            // If not, make a new instance
                            territoryInstance = new TerritoryInstance(territoryId, clanWarId, mMapController?.Id ?? Guid.Empty, MapInstanceId);
                            context.Territories.Add(territoryInstance);
                        }

                        // add to this map's list of territory instances
                        ActiveTerritories[territoryId] = new MapTerritory(territoryInfo, territoryInstance);
                        CacheTerritoryTiles(territoryInstance, territoryInfo.SpawnPos, territoryInfo.Radius);
                    }

                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public void SendTerritoryUpdatesTo(Player player)
        {
            if (ActiveTerritories.Count < 1)
            {
                return;
            }

            var packets = new List<TerritoryUpdatePacket>();
            foreach (var mapTerritory in ActiveTerritories.Values)
            {
                packets.Add(mapTerritory.Instance.Packetize());
            }

            player.SendPacket(new TerritoriesPacket(packets.ToArray()));
        }

        private void UpdateTerritories()
        {
            foreach (var territory in ActiveTerritories.Values)
            {
                territory.Instance.Update(Timing.Global.MillisecondsUtc);
            }
        }

        private void CacheTerritoryTiles(TerritoryInstance instance, BytePoint spawnPos, int size)
        {
            int left = spawnPos.X - size;
            int right = spawnPos.X + size;
            int top = spawnPos.Y - size;
            int bottom = spawnPos.Y + size;

            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    var dist = MathHelper.CalculateDistanceToPoint(spawnPos.X, spawnPos.Y, x, y);
                    if (Math.Floor(dist) > size)
                    {
                        continue;
                    }

                    // This will not allow for overlapping territories
                    TerritoryTiles[new BytePoint((byte)x, (byte)y)] = instance;
                }
            }
        }

        public void OnClanWarStatusChange(object sender, bool active)
        {
            if (active)
            {
                InitializeTerritories();
            }
            else
            {
                ClearTerritoryInstances();
            }
        }
    }
}
