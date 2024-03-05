using Intersect.GameObjects;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using Intersect.Server.Extensions;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Intersect.Server.Core.Games.ClanWars
{
    public class ClanWarInstance
    {
        private object mLock = new object();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public long TimeStarted { get; set; }

        public long TimeEnded { get; set; }

        public bool IsActive { get; set; }

        public List<ClanWarParticipant> Participants { get; set; } = new List<ClanWarParticipant>();

        [NotMapped, JsonIgnore]
        public Guid[] ParticipantIds => Participants.Select(p => p.GuildId).ToArray();

        [NotMapped, JsonIgnore]
        public HashSet<Player> Players { get; set; } = new HashSet<Player>();

        [NotMapped, JsonIgnore]
        private ClanWarScore[] mScores => Participants
            .OrderByDescending(p => p.Score)
            .Select(p => new ClanWarScore(Guild.GetGuild(p.GuildId)?.Name, p.Score))
            .ToArray();

        public void AddPlayer(Player player)
        {
            if (player == null || !player.IsInGuild || !IsActive)
            {
                return;
            }

            Players.Add(player);
            AddParticipant(player.Guild?.Id ?? Guid.Empty);
            SendScoresToPlayer(player);
        }

        public void AddParticipant(Guid guildId)
        {
            if (ParticipantIds.Contains(guildId) || !IsActive)
            {
                return;
            }

            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(false))
                {
                    var participant = new ClanWarParticipant(Id, guildId);
                    Participants.Add(participant);

                    context.Clan_War_Participants.Add(participant);

                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public void RemovePlayer(Player player, bool fromLogout = false) 
        {  
            Players.Remove(player);

            if (!fromLogout)
            {
                player?.SendPacket(new LeaveClanWarPacket());
            }
        }

        public void RemoveParticipant(Guid guildId)
        {
            // Don't bother if this clan war isn't even active
            if (!IsActive || !ParticipantIds.Contains(guildId))
            {
                return;
            }

            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(readOnly: false))
                {
                    var participants = context.Clan_War_Participants.Where(p => p.ClanWarId == Id && p.GuildId == guildId);
                    context.Clan_War_Participants.RemoveRange(participants);
                    Participants.RemoveAll(p => p.GuildId == guildId);

                    var ownedTerritories = context.Territories
                        .Where(t => t.ClanWarId == Id && t.GuildId == guildId)
                        .ToArray();

                    var now = Timing.Global.MillisecondsUtc;

                    // Remove territories from this guild's control
                    foreach (var territory in ownedTerritories)
                    {
                        if (!MapController.TryGetInstanceFromMap(territory.MapId, territory.MapInstanceId, out var mapInstance))
                        {
                            continue;
                        }

                        if (!mapInstance.ActiveTerritories.TryGetValue(territory.TerritoryId, out var activeTerritory))
                        {
                            continue;
                        }

                        activeTerritory?.Instance?.TeritoryLost(now);
                    }

                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public void Start()
        {
            IsActive = true;
            TimeStarted = Timing.Global.MillisecondsUtc;
        }

        public void End()
        {
            IsActive = false;
            TimeEnded = Timing.Global.MillisecondsUtc;

            foreach (Player player in Players.ToArray())
            {
                if (!player.Online || player.InstanceType != Enums.MapInstanceType.ClanWar)
                {
                    continue;
                }

                player?.WarpToLastOverworldLocation(false);
            }

            Players.Clear();
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                var participants = context.Clan_War_Participants.Where(p => p.ClanWarId == Id);
                context.Clan_War_Participants.RemoveRange(participants);
                context.Clan_Wars.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        public void ChangeGuildScore(Guid guildId, int scoreDelta)
        {
            var participant = Participants.FirstOrDefault(p => p.GuildId == guildId);
            if (participant == default)
            {
                return;
            }

            participant.Score += scoreDelta;
        }

        public void Update()
        {
            if (!IsActive)
            {
                Logging.Log.Warn($"An inactive clan war requested Update. ID: {Id}");
                return;
            }

            var change = false;
            using (var context = DbInterface.CreatePlayerContext()) 
            {
                var scoringGuilds = context.Territories
                    .Where(t => t != null &&
                                t.ClanWarId == Id &&
                                t.MapInstanceId == Id &&
                                t.GuildId != Guid.Empty)
                    .GroupBy(
                        t => t.GuildId,
                        t => t.TerritoryId,
                        (guildId, territories) => new
                        {
                            Key = guildId,
                            ControlledTerritories = territories
                        }
                    );

                foreach (var scoringGuild in scoringGuilds) 
                {
                    var score = 0;
                    foreach (var territoryId in scoringGuild.ControlledTerritories)
                    {
                        if (!TerritoryDescriptor.TryGet(territoryId, out var territory))
                        {
                            continue;
                        }

                        score += territory.PointsPerTick;
                        change = true;
                    }
                    ChangeGuildScore(scoringGuild.Key, score);
                }
            }

            BroadcastScores();

            if (change)
            {
                Save();
            }
        }

        public void BroadcastScores()
        {
            var updatePacket = new ClanWarScoreUpdatePacket(mScores, null);
            
            foreach (var player in Players.ToArray())
            {
                if (!player.Online || player.InstanceType != Enums.MapInstanceType.ClanWar || player.MapInstanceId != Id)
                {
                    continue;
                }

                player?.SendPacket(updatePacket);
            }
        }

        private ClanWarMapUpdate[] GetMapUpdates(Guid mapInstanceId)
        {
            using (var context = DbInterface.CreatePlayerContext())
            {
                List<ClanWarMapUpdate> mapUpdates = new List<ClanWarMapUpdate>();
                var territories = context.Territories.Where(t => t.ClanWarId == Id && t.MapInstanceId == mapInstanceId);
                foreach (var territory in territories)
                {
                    mapUpdates.Add(new ClanWarMapUpdate(territory.MapId, Guild.GetGuild(territory.GuildId)?.Name ?? string.Empty));
                }

                return mapUpdates.ToArray();
            }
        }

        public void SendScoresToPlayer(Player player)
        {
            player?.SendPacket(new ClanWarScoreUpdatePacket(mScores, GetMapUpdates(player.MapInstanceId)));
        }

        public void SaveToContext()
        {
            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(false))
                {
                    context.Clan_Wars.Update(this);
                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }
            }
        }

        public void Save()
        {
            DbInterface.Pool.QueueWorkItem(SaveToContext);
        }
    }
}
