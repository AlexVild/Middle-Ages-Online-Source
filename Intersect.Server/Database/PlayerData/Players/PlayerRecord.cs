using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Intersect.GameObjects.Events;
using Intersect.Network.Packets.Server;
using Intersect.Server.Entities;
using Intersect.Server.Networking;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Intersect.Server.Database.PlayerData.Players
{

    public class PlayerRecord : IPlayerOwned
    {
        /// <summary>
        /// The amount of records to return per visible page
        /// </summary>
        public static readonly int PageLimit = 20;

        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public PlayerRecord() { } // EF

        public PlayerRecord(Guid player, RecordType type, Guid recordId, int initialAmount)
        {
            PlayerId = player;
            Type = type;
            RecordId = recordId;
            Amount = initialAmount;
            ScoreType = RecordScoring.High;
        }

        public PlayerRecord(Guid playerId, RecordType type, Guid recordId, int amount, RecordScoring scoreType)
        {
            PlayerId = playerId;
            Type = type;
            RecordId = recordId;
            Amount = amount;
            ScoreType = scoreType;
        }

        public RecordType Type { get; set; }

        public long Amount { get; set; }

        [ForeignKey(nameof(Player))]
        [JsonIgnore]
        public Guid PlayerId { get; private set; }

        [JsonIgnore]
        public virtual Player Player { get; private set; }

        public Guid RecordId { get; set; }
        
        public RecordScoring ScoreType { get; set; }

        [JsonIgnore]
        public virtual List<RecordTeammateInstance> Teammates { get; set; } = new List<RecordTeammateInstance>();

        /// <summary>
        /// Saves this player record to the player context
        /// </summary>
        public void SaveToContext()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Record.Update(this);
                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }

        public static void _debugSendRecordsTo(Player player)
        {
            var recordBuilder = new StringBuilder();

            var records = GetRecords(RecordType.PlayerVariable, new Guid("ec4e3a68-7f79-490e-8b39-260095c8dca1"), RecordScoring.High, 0);
            foreach(var record in records)
            {
                recordBuilder.AppendLine(string.Join(": ", record.RecordDisplay, record.Participants));
            }

            PacketSender.SendEventDialog(player, recordBuilder.ToString(), string.Empty, default);
        }

        /// <summary>
        /// Gets sorted records of a given type
        /// </summary>
        /// <param name="type">The record type we're looking for</param>
        /// <param name="recordId">The record ID for identification within the record type</param>
        /// <param name="scoreType">The type of scoring we want for the record</param>
        /// <param name="page">The page of record results we're returning</param>
        /// <param name="recordTransformer">A function that returns a string from a long that transforms a long as desired to give the correct output of the record</param>
        /// <returns>A list of <see cref="RecordDto"/> that we can use to send a packet of</returns>
        public static List<RecordDto> GetRecords(RecordType type, Guid recordId, RecordScoring scoreType, int page, Func<long, string> recordTransformer = null)
        {
            if (recordTransformer == null)
            {
                recordTransformer = (val) => val.ToString();
            }

            var dtos = new List<RecordDto>();
            var records = new List<PlayerRecord>();
            Dictionary<Guid, string> playerNameLookup = new Dictionary<Guid, string>();
            using (var context = DbInterface.CreatePlayerContext())
            {
                records.AddRange(GetMatchingRecords(context, type, recordId, scoreType, page));
                playerNameLookup = GetPlayerNameLookup(context, records);
                dtos = CreateRecordDtos(records.ToArray(), playerNameLookup, recordTransformer);
            }

            return dtos;
        }

        /// <summary>
        /// Gets all records within a certain type/ID that are associated with a certain player by name
        /// </summary>
        /// <param name="type">The record type we're looking for</param>
        /// <param name="recordId">The record ID for identification within the record type</param>
        /// <param name="scoreType">The type of scoring we want for the record</param>
        /// <param name="page">The page of record results we're returning</param>
        /// <param name="term">The player name we're looking up</param>
        /// <param name="recordTransformer">A function that returns a string from a long that transforms a long as desired to give the correct output of the record</param>
        /// <returns>A list of <see cref="RecordDto"/> that we can use to send a packet of</returns>
        public static List<RecordDto> GetRecordsForPlayerName(RecordType type, Guid recordId, RecordScoring scoreType, int page, string term, Func<long, string> recordTransformer = null)
        {
            var dtos = new List<RecordDto>();
            var records = new List<PlayerRecord>();
            Dictionary<Guid, string> playerNameLookup = new Dictionary<Guid, string>();
            using (var context = DbInterface.CreatePlayerContext())
            {
                var playerId = context.Players
                    .Where(player => player.Name.Trim().ToLower() == term.Trim().ToLower())
                    .Take(1)
                    .Select(player => player.Id)
                    .ToList()
                    .FirstOrDefault();

                if (playerId == default)
                {
                    return dtos;
                }

                var relevantRecords = GetMatchingRecordsForPlayer(playerId, context, type, recordId, scoreType, page);
                playerNameLookup = GetPlayerNameLookup(context, records);
                dtos = CreateRecordDtos(relevantRecords, playerNameLookup, recordTransformer);
            }

            return dtos;
        }

        private static PlayerRecord[] GetMatchingRecords(PlayerContext context, RecordType type, Guid recordId, RecordScoring scoreType, int page)
        {
            if (context == null)
            {
                return Array.Empty<PlayerRecord>();
            }

            PlayerRecord[] queryRecords;

            if (scoreType == RecordScoring.High)
            {
                queryRecords = context.Player_Record
                    .Where(record => record.Type == type && record.RecordId == recordId && record.ScoreType == scoreType)
                    .OrderByDescending(record => record.Amount)
                    .Skip(page * PageLimit)
                    .Take(PageLimit)
                    .ToArray();
            }
            else
            {
                queryRecords = context.Player_Record
                    .Where(record => record.Type == type && record.RecordId == recordId && record.ScoreType == scoreType)
                    .OrderBy(record => record.Amount)
                    .Skip(page * PageLimit)
                    .Take(PageLimit)
                    .ToArray();
            }

            foreach (var record in queryRecords)
            {
                record.Teammates = context.Record_Teammate.Where(teammate => teammate.RecordInstanceId == record.Id).ToList();
            }

            return queryRecords;
        }

        private static PlayerRecord[] GetMatchingRecordsForPlayer(Guid playerId, PlayerContext context, RecordType type, Guid recordId, RecordScoring scoreType, int page)
        {
            if (context == null)
            {
                return Array.Empty<PlayerRecord>();
            }

            var teammateRecords = context.Record_Teammate
                    .Where(tm => tm.PlayerId == playerId)
                    .Select(tm => tm.RecordInstanceId)
                    .ToList();

            PlayerRecord[] relevantRecords;
            if (scoreType == RecordScoring.High)
            {
                relevantRecords = context.Player_Record
                    .Where(record => record.Type == type && (teammateRecords.Contains(record.Id) || record.PlayerId == playerId) && record.RecordId == recordId && record.ScoreType == scoreType)
                    .OrderByDescending(record => record.Amount)
                    .Skip(page * PageLimit)
                    .Take(PageLimit)
                    .ToArray();
            }
            else
            {
                relevantRecords = context.Player_Record
                    .Where(record => record.Type == type && (teammateRecords.Contains(record.Id) || record.PlayerId == playerId) && record.RecordId == recordId && record.ScoreType == scoreType)
                    .OrderBy(record => record.Amount)
                    .Skip(page * PageLimit)
                    .Take(PageLimit)
                    .ToArray();
            }

            return relevantRecords;
        }

        private static Dictionary<Guid, string> GetPlayerNameLookup(PlayerContext context, List<PlayerRecord> records)
        {
            Dictionary<Guid, string> playerNameLookup = new Dictionary<Guid, string>();
            if (context == null || records == null)
            {
                return playerNameLookup;
            }
            
            List<Guid> playersInRecords = new List<Guid>();
            foreach (var record in records)
            {
                playersInRecords.Add(record.PlayerId);
                var teammateIds = record.Teammates.Select(tm => tm.PlayerId);
                playersInRecords.AddRange(teammateIds);
            }
            playersInRecords = playersInRecords.Distinct().ToList();

            var relevantPlayers = context.Players
                .Where(player => playersInRecords.Contains(player.Id));
            foreach (var player in relevantPlayers)
            {
                playerNameLookup[player.Id] = player.Name;
            }

            return playerNameLookup;
        }

        private static List<RecordDto> CreateRecordDtos(PlayerRecord[] records, Dictionary<Guid, string> playerNameLookup, Func<long, string> recordTransformer)
        {
            List<RecordDto> dtos = new List<RecordDto>();
            foreach (var record in records)
            {
                List<string> names = new List<string>();
                if (record.Teammates.Count > 0)
                {
                    foreach (var teammate in record.Teammates)
                    {
                        names.Add(playerNameLookup[teammate.PlayerId]);
                    }
                }
                else
                {
                    names.Add(playerNameLookup[record.PlayerId]);
                }

                var name = string.Join(", ", names);
                string formattedRecord = recordTransformer(record.Amount);

                dtos.Add(new RecordDto(name, formattedRecord));
            }

            return dtos;
        }
    }
}
