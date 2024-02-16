using Intersect.GameObjects;
using Intersect.GameObjects.Events;
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
        private const long MAX_HEALTH = 100;

        [NotMapped]
        private const long HEALTH_TICK_TIME = 500;

        [NotMapped]
        private const long HEALTH_BASE_TICK_AMT = 500;

        [NotMapped]
        private const long HEALTH_TICK_BONUS = 100;

        [NotMapped]
        private const long HEALTH_TICK_MAXIMUM = 1000;

        // EF
        public TerritoryInstance()
        {
        }

        public TerritoryInstance(Guid territoryDescriptorId, Guid clanWarId)
        {
            ClanWarId = clanWarId;
            TerritoryId = territoryDescriptorId;

            Initialize();
        }

        public Guid TerritoryId { get; private set; }

        [NotMapped, JsonIgnore]
        public TerritoryDescriptor Territory { get; set; }

        [NotMapped, JsonIgnore]
        public long Health { get; set; }

        public Guid GuildId { get; set; }

        public Guid ClanWarId { get; set; }

        [NotMapped, JsonIgnore]
        public List<Player> Players { get; set; } = new List<Player>();

        [NotMapped, JsonIgnore]
        public Guid[] PlayerGuildIds { get; set; }

        [NotMapped, JsonIgnore]
        public Player[] Invaders { get; set; }

        [NotMapped, JsonIgnore]
        public Player[] Defenders { get; set; }

        [NotMapped, JsonIgnore]
        private long mNextHealthTick { get; set; }

        [NotMapped, JsonIgnore]
        private Guid mConqueringGuildId { get; set; }

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
                Health = MAX_HEALTH;
            }
            else
            {
                Health = 0;
            }

            mNextHealthTick = Timing.Global.MillisecondsUtc + HEALTH_TICK_TIME;
        }

        public void AddPlayer(Player player)
        {
            if (!player.IsInGuild)
            {
                return;
            }

            Players.Add(player);
            CachePlayerLookups();
        }

        public void RemovePlayer(Player player)
        {
            Players.RemoveAll(x => x.Id == player.Id);
            CachePlayerLookups();
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
        }

        private void TickHealth(long currentTime, bool subtractive = false)
        {
            if (mNextHealthTick > currentTime)
            {
                return;
            }

            var amount = HEALTH_BASE_TICK_AMT + (Invaders.Length - 1 * HEALTH_TICK_BONUS);
            amount = MathHelper.Clamp(amount, HEALTH_BASE_TICK_AMT, HEALTH_TICK_MAXIMUM);
            
            if (subtractive)
            {
                amount *= -1;
            }

            Health += amount;
            mNextHealthTick = currentTime + HEALTH_TICK_TIME;
        }

        private void ResetHealth()
        {
            if (State == TerritoryState.Capturing || State == TerritoryState.Neutral)
            {
                Health = 0;
            }
            else
            {
                Health = MAX_HEALTH;
            }
        }

        /// <summary>
        /// Checks to see that there is
        /// a) Of the guilded players, only a single guild within the territory's bounds
        /// b) That the guild is different than that of the guild currently controlling the territory, if any
        /// </summary>
        /// <returns></returns>
        private bool TryConquererSwitch()
        {
            if (Defenders.Length > 0)
            {
                return false;
            }

            if (Invaders.Length == 0)
            {
                return false;
            }

            var conqueringGuilds = Invaders.Where(pl => pl.IsInGuild && pl.Guild.Id != Guid.Empty).Select(pl => pl.Guild.Id);
            var conqueringId = conqueringGuilds.FirstOrDefault();

            if (conqueringId == Guid.Empty || Invaders.Any(pl => pl.Guild?.Id != conqueringId))
            {
                mConqueringGuildId = Guid.Empty;
                return false;
            }

            mConqueringGuildId = conqueringId;
            return true;
        }

        private void GuildTakeOver(Guid guildId, long currentTime)
        {
            GuildId = guildId;
            Health = MAX_HEALTH;
            ChangeState(TerritoryState.Owned, currentTime);
        }

        private void TeritoryLost(long currentTime)
        {
            GuildId = Guid.Empty;
            Health = 0;
            ChangeState(TerritoryState.Neutral, currentTime);
        }
    }
}
