using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData
{
    public class RecordTeammateInstance
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public RecordTeammateInstance() { } // EF

        public RecordTeammateInstance(Guid recordInstanceId, Guid playerId)
        {
            RecordInstanceId = recordInstanceId;
            PlayerId = playerId;
        }

        [ForeignKey(nameof(Record))]
        public Guid RecordInstanceId { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        [JsonIgnore]
        public virtual Player Player { get; private set; }

        [JsonIgnore]
        public virtual PlayerRecord Record { get; private set; }
    }
}
