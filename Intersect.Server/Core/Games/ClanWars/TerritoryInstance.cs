using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Intersect.Server.Core.Games.ClanWars
{
    public partial class TerritoryInstance
    {
        [NotMapped]
        private object mLock = new object();

        // EF
        public TerritoryInstance()
        {
        }

        public TerritoryInstance(Guid territoryDescriptorId, Guid clanWarId, Guid mapId, Guid mapInstanceId)
        {
            ClanWarId = clanWarId;
            TerritoryId = territoryDescriptorId;
            MapId = mapId;
            MapInstanceId = mapInstanceId;

            Initialize();
        }

        public Guid TerritoryId { get; private set; }

        [NotMapped, JsonIgnore]
        public TerritoryDescriptor Territory { get; set; }

        [NotMapped]
        public long Health;

        public Guid GuildId { get; set; }

        public Guid ClanWarId { get; set; }

        public Guid MapId { get; set; }

        public Guid MapInstanceId { get; set; }

        [NotMapped, JsonIgnore]
        public HashSet<Player> Players { get; set; } = new HashSet<Player>();

        [NotMapped, JsonIgnore]
        public Guid[] PlayerGuildIds { get; set; }

        [NotMapped, JsonIgnore]
        public Player[] Invaders { get; set; }

        [NotMapped, JsonIgnore]
        public Player[] Defenders { get; set; }

        [NotMapped]
        private long mNextHealthTick;

        [NotMapped, JsonIgnore]
        public Guid ConqueringGuildId { get; set; }

        private const long DEBOUNCE_TIME = 150;

        [NotMapped, JsonIgnore]
        public long DebounceTime { get; set; }

        private bool StateChanged { get; set; }

        public void Initialize()
        {
            if (!TerritoryDescriptor.TryGet(TerritoryId, out var territory))
            {
                throw new InvalidOperationException($"Tried to update territory with invalid descriptor: Descriptor ID: ${TerritoryId}");
            }

            Territory = territory;
            Players.Clear();
            CachePlayerLookups();

            if (State == TerritoryState.Wresting || State == TerritoryState.Owned)
            {
                Health = Territory.CaptureMs;
            }
            else
            {
                Health = 0;
            }

            mNextHealthTick = Timing.Global.Milliseconds + Options.Instance.ClanWar.HealthTickMs;
            DebounceTime = Timing.Global.MillisecondsUtc;
        }

        public void AddPlayer(Player player)
        {
            if (player == null || !player.IsInGuild || Players.Contains(player))
            {
                return;
            }

            Players.Add(player);
            CachePlayerLookups();
            Logging.Log.Debug($"{player} added to {Territory.Name}!");
        }

        public void RemovePlayer(Player player)
        {
            if (player == null || !Players.Contains(player))
            {
                return;
            }

            Players.Remove(player);
            CachePlayerLookups();
            Logging.Log.Debug($"{player} left {Territory.Name}!");
        }

        private void CachePlayerLookups()
        {
            PlayerGuildIds = Players.Where(pl => pl.IsInGuild).Select(pl => pl.Guild.Id).Distinct().ToArray();
            Invaders = Players.Where(pl => pl.IsInGuild && pl.Guild.Id != GuildId).ToArray();
            Defenders = Players.Where(pl => pl.IsInGuild && pl.Guild.Id == GuildId).ToArray();
        }

        public void Update(long currentTime)
        {
            // Otherwise, proceed with takeover
            switch (State)
            {
                case TerritoryState.Neutral:
                    StateNeutral(currentTime);
                    break;
                case TerritoryState.Owned:
                    StateOwned(currentTime);
                    break;
                case TerritoryState.Capturing:
                    StateCapturing(currentTime);
                    break;
                case TerritoryState.Wresting:
                    StateWresting(currentTime);
                    break;
                case TerritoryState.Contested:
                    StateContested(currentTime);
                    break;
            }

            if (DebounceTime < Timing.Global.MillisecondsUtc && StateChanged)
            {
                StateChanged = false;
                BroadcastStateChange();
            }
        }

        private void Debounce()
        {
            DebounceTime = Timing.Global.Milliseconds + DEBOUNCE_TIME;
        }

        private void ResetHealth()
        {
            if (State == TerritoryState.Capturing || State == TerritoryState.Neutral)
            {
                Health = 0;
            }
            else
            {
                Health = Territory.CaptureMs;
            }
        }

        /// <summary>
        /// Checks to see that there is
        /// a) Of the guilded players, only a single guild within the territory's bounds
        /// b) That the guild is different than that of the guild currently controlling the territory, if any
        /// </summary>
        private void FindCurrentConquerer()
        {
            ConqueringGuildId = Guid.Empty;
            // No one's there, or someone is defending a territory they own
            if (Players.Count == 0 || Defenders.Length > 0)
            {
                return;
            }

            var conqueringGuilds = Invaders.Where(pl => pl.IsInGuild).Select(pl => pl.Guild.Id);
            var conqueringId = conqueringGuilds.FirstOrDefault();

            if (conqueringId == Guid.Empty || Invaders.Any(pl => pl.Guild?.Id != conqueringId))
            {
                return;
            }

            ConqueringGuildId = conqueringId;
        }

        private void GuildTakeOver(Guid guildId, long currentTime)
        {
            GuildId = guildId;
            Health = Territory.CaptureMs;
            ChangeState(TerritoryState.Owned, currentTime);
        }

        private void TeritoryLost(long currentTime)
        {
            GuildId = Guid.Empty;
            Health = 0;
            ChangeState(TerritoryState.Neutral, currentTime);
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Territories.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        private void SaveToContext()
        {
            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(readOnly: false))
                {
                    context.Territories.Update(this);
                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public void Save()
        {
            DbInterface.Pool.QueueWorkItem(SaveToContext);
        }

        public TerritoryUpdatePacket Packetize()
        {
            var owner = Guild.GetGuild(GuildId)?.Name ?? string.Empty;
            var conquerer = Guild.GetGuild(ConqueringGuildId)?.Name ?? string.Empty;
            var healthTickOffset = MathHelper.Clamp(mNextHealthTick - Timing.Global.Milliseconds, 0, long.MaxValue);
            
            return new TerritoryUpdatePacket(MapId, TerritoryId, owner, conquerer, State, Health, healthTickOffset);
        }
    }
}
