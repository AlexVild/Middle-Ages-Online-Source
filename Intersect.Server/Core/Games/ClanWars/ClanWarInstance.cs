using Intersect.Extensions;
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

        public Guid WinningGuildId { get; set; }

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
            BroadcastScores();
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

            BroadcastScores();
        }

        public void Start()
        {
            IsActive = true;
            TimeStarted = Timing.Global.MillisecondsUtc;
        }

        public void End(bool save)
        {
            IsActive = false;
            TimeEnded = Timing.Global.MillisecondsUtc;

            Payout();

            var clanWarWinnerPacket = new ClanWarWinnerPacket(Guild.GetGuild(WinningGuildId)?.Name ?? string.Empty);
            // Kick players out of the clan war instance
            foreach (Player player in Players.ToArray())
            {
                if (!player.Online || player.InstanceType != Enums.MapInstanceType.ClanWar)
                {
                    continue;
                }
                player.WarpToLastOverworldLocation(false, true);
                PacketSender.SendToast(player, "The Clan War has ended!");
                player.SendPacket(clanWarWinnerPacket);
            }

            if (save)
            {
                Save();
            }

            Players.Clear();
        }

        private void Payout()
        {
            // Dict of Guilds -> their Valor Coin payout
            Dictionary<Guid, int> payouts = new Dictionary<Guid, int>();

            var participants = Participants.ToArray();

            var winners = participants
                .OrderByDescending(p => p.Score)
                .TakeWhile(score => score.Score >= Options.Instance.ClanWar.MinimumScore)
                .ToArray();
            var losers = participants.Except(winners);

            if (winners.Length < Options.Instance.ClanWar.MinimumParticipants)
            {
                foreach (var participant in participants)
                {
                    var guild = Guild.LoadGuild(participant.GuildId);
                    guild?.SendMessageToMembers($"There were not enough clans in the Clan War to earn Valor Coins. At least {Options.Instance.ClanWar.MinimumParticipants} clans must score a minimum of {Options.Instance.ClanWar.MinimumScore} points for a Clan War to be valid.");
                }
                return;
            }

            WinningGuildId = winners?.Select(p => p.GuildId)?.FirstOrDefault() ?? Guid.Empty;

            var totalScore = participants.Sum(p => p.Score);

            var pot = Options.Instance.ClanWar.BaseValorReward + (MathHelper.Clamp(winners.Length - Options.Instance.ClanWar.MinimumParticipants, 0, int.MaxValue) * Options.Instance.ClanWar.BoostPerParticipant);

            var place = 1;
            foreach (var participant in winners)
            {
                var proportion = participant.Score / (float)totalScore;

                var payout = (int)MathHelper.Clamp(Math.Floor(pot * proportion), Options.Instance.ClanWar.MinimumValorReward, pot);

                var valorCoins = new Item(Options.Instance.ClanWar.ValorTokenItemId, payout);

                var guild = Guild.LoadGuild(participant.GuildId);

                if (guild == null) 
                {
                    continue;
                }

                if (guild?.TryDirectBankDeposit(valorCoins) ?? false)
                {
                    guild?.SendMessageToMembers($"Your clan earned {payout} Valor Coin(s) from Clan Wars! They have been deposited in your clan bank. Your clan came in {place.ToOrdinal()} place with a score of {participant.Score.ToString("N0")} points.");
                } 
                else
                {
                    guild?.SendMessageToMembers($"Your clan earned {payout} Valor Coins from Clan Wars, but does not have space in its Clan Bank! Your clan came in {place.ToOrdinal()} place with a score of {participant.Score.ToString("N0")} points.");
                }
                
                foreach (var player in guild.FindOnlineMembers().Where(pl => pl.InCurrentClanWar))
                {
                    player.ClanWarComplete = new ClanWarCompletePacket(payout, place);
                }
                
                place++;
            }

            foreach (var participant in losers)
            {
                var guild = Guild.LoadGuild(participant.GuildId);
                guild?.SendMessageToMembers($"Your clan did not score at least {Options.Instance.ClanWar.MinimumScore} points in the Clan War, and thus did not earn any Valor Coins.");
            }
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
            var scoringGuilds = ClanWarManager.CachedTerritories.Values
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
