using Intersect.Server.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class MapExploredInstance : IPlayerOwned
    {
        /// <summary>
        /// The database Id of the record.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public MapExploredInstance() { } // EF

        public MapExploredInstance(Guid playerId, Guid mapId)
        {
            Id = new Guid();
            PlayerId = playerId;
            MapId = mapId;
        }

        [ForeignKey(nameof(Player))]
        [JsonIgnore]
        public Guid PlayerId { get; private set; }

        [JsonIgnore]
        public virtual Player Player { get; private set; }

        public Guid MapId { get; private set; }
    }
}
