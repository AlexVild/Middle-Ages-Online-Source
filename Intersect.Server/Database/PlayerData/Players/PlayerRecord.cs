using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.Logging;
using Intersect.Server.Entities;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Intersect.Server.Database.PlayerData.Players
{
    public enum RecordType
    {
        NpcKilled = 0,
        ItemCrafted,
        ResourceGathered,
        PlayerVariable,
        Combo
    }

    public class PlayerRecord : IPlayerOwned
    {
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
    }
}
