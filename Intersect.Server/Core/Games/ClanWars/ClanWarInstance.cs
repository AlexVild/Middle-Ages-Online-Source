using Intersect.GameObjects;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using Intersect.Server.Extensions;
using Intersect.Server.Networking;
using Intersect.Utilities;
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

        public void AddParticipant(Player player)
        {
            if (!player.IsInGuild)
            {
                return;
            }

            Players.Add(player);

            var guildId = player.Guild?.Id ?? Guid.Empty;
            if (ParticipantIds.Contains(guildId))
            {
                return;
            }

            using (var context = DbInterface.CreatePlayerContext(false))
            {
                var participant = new ClanWarParticipant(Id, guildId);
                Participants.Add(participant);

                context.Clan_War_Participants.Add(participant);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        public void RemoveParticipant(Player player) 
        {  
            Players.Remove(player); 
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
            participant.Save();
        }

        public void Update()
        {
            if (!IsActive)
            {
                Logging.Log.Warn($"An inactive clan war requested Update. ID: {Id}");
                return;
            }

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
                    }
                    ChangeGuildScore(scoringGuild.Key, score);
                }
            }

            Logging.Log.Debug($"\n--- CLAN WARS SCORE TICK ---");
            foreach (var participant in Participants.OrderByDescending(p => p.Score).ToArray())
            {
                var guild = Guild.GetGuild(participant.GuildId);
                if (guild == null)
                {
                    continue;
                }

                Logging.Log.Debug($"--- {guild.Name}: {participant.Score} pts.");
            }
        }
    }
}
