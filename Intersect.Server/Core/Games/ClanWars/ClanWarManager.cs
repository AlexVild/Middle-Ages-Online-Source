using Intersect.Config;
using Intersect.Enums;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Intersect.Server.Core.Games.ClanWars
{
    public static class ClanWarManager
    {
        private static object mLock = new object();

        public static bool ClanWarActive => CurrentWar != null;

        public static Guid CurrentWarId => CurrentWar?.Id ?? Guid.Empty;

        public static ClanWarInstance _currentWar { get; set; }

        public static Guid LastWinningGuild { get; set; }
        
        private static long mNextTick { get; set; }

        public static Dictionary<Guid, TerritoryInstance> CachedTerritories = new Dictionary<Guid, TerritoryInstance>();

        public static ClanWarInstance CurrentWar
        {
            get => _currentWar;
            set
            {
                _currentWar = value;
                StatusChange(null, ClanWarActive);
            }
        }

        public static event EventHandler<bool> StatusChange = delegate { };

        public static void Update()
        {
            if (CurrentWar == null || Timing.Global.MillisecondsUtc <= mNextTick)
            {
                return;
            }
            
            CurrentWar.Update();
            
            mNextTick = Timing.Global.MillisecondsUtc + Options.Instance.ClanWar.ScoreTickMs;
        }

        public static void StartClanWar()
        {
            CachedTerritories.Clear();
            lock (mLock)
            {
                var prevState = ClanWarActive;
                using (var context = DbInterface.CreatePlayerContext(false))
                {
                    var activeWar = context.Clan_Wars.FirstOrDefault(cw => cw.IsActive);
                    if (activeWar != default)
                    {
                        if (CurrentWar == null)
                        {
                            CurrentWar = activeWar;
                        }
                        Console.WriteLine("A clan war is already active in the DB");
                        return;
                    }

                    var clanWar = new ClanWarInstance();
                    clanWar.Start();
                    context.Clan_Wars.Add(clanWar);

                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();

                    CurrentWar = clanWar;
                }

                if (prevState != ClanWarActive)
                {
                    Player.StartCommonEventsWithTriggerForAll(CommonEventTrigger.ClanWarStarted);
                }
                Console.WriteLine(Strings.Commandoutput.guildwarsenabled);

                mNextTick = Timing.Global.MillisecondsUtc + Options.Instance.ClanWar.ScoreTickMs;
            }
        }

        public static void InitializeManager()
        {
            using (var context = DbInterface.CreatePlayerContext())
            {
                CurrentWar = context.Clan_Wars
                    .Where(cw => cw.IsActive)
                    .Include(cw => cw.Participants)
                    .FirstOrDefault(cw => cw.IsActive);

                LastWinningGuild = context.Clan_Wars
                    .Where(cw => !cw.IsActive)
                    .OrderByDescending(cw => cw.TimeEnded)
                    .FirstOrDefault()?.WinningGuildId ?? Guid.Empty;
            }
        }

        public static void EndAllClanWars()
        {
            CachedTerritories.Clear();
            lock (mLock)
            {
                var prevState = ClanWarActive;
                using (var context = DbInterface.CreatePlayerContext(false))
                {
                    CurrentWar?.End(true);
                    LastWinningGuild = CurrentWar?.WinningGuildId ?? Guid.Empty;
                    CurrentWar = null;

                    var activeClanWars = context.Clan_Wars.Where(cw => cw.IsActive).ToArray();
                    foreach (var cw in activeClanWars)
                    {
                        cw.End(false);
                    }

                    context.ChangeTracker.DetectChanges();
                    context.SaveChanges();
                }

                if (prevState != ClanWarActive)
                {
                    Player.StartCommonEventsWithTriggerForAll(CommonEventTrigger.ClanWarEnded);
                }
                Console.WriteLine(Strings.Commandoutput.guildwarsdisabled);
            }
        }

        public static void BroadcastTerritoryUpdate(TerritoryInstance territory)
        {
            if (!ClanWarActive)
            {
                return;
            }
            
            var war = CurrentWar;
            foreach (var player in war.Players.ToArray())
            {
                if (!player.Online || player == null || player.MapInstanceId != territory.MapInstanceId)
                {
                    continue;
                }
                player.SendPacket(territory.Packetize());
            }
        }

        public static void GuildDisbanded(Guid guildId)
        {
            if (!ClanWarActive || guildId == Guid.Empty)
            {
                return;
            }

            lock (mLock)
            {
                CurrentWar?.RemoveParticipant(guildId);
            }
        }

        public static bool IsInWar(Player player)
        {
            if (!ClanWarActive || player == null) 
            { 
                return false;
            }

            lock (mLock)
            {
                return CurrentWar.Players.Contains(player);
            }
        }

        public static void ChangePoints(Guid guildId, int points)
        {
            if (!ClanWarActive || points == 0 || guildId == Guid.Empty)
            {
                return;
            }

            lock (mLock)
            {
                CurrentWar?.ChangeGuildScore(guildId, points);
                CurrentWar?.BroadcastScores();
            }
        }
    }

}
