using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Server.Entities;
using Intersect.Server.Networking;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intersect.Server.Database.PlayerData.Players
{
    public class ChallengeInstance : IPlayerOwned
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        
        [JsonIgnore, NotMapped]
        public Player Player { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }

        [Column("Challenge")]
        public Guid ChallengeId { get; set; }

        [NotMapped, JsonIgnore]
        public ChallengeDescriptor Challenge => ChallengeDescriptor.Get(ChallengeId);

        public bool Complete { get; set; }

        public int Progress { get; set; }

        public bool Active { get; set; }

        public ChallengeInstance()
        {
        }

        public ChallengeInstance(Guid playerId, Guid challengeId, bool complete, int progress, bool active)
        {
            PlayerId = playerId;
            ChallengeId = challengeId;
            Complete = complete;
            Progress = progress;
            Active = active;
        }

        public void RemoveFromDb()
        {
            using (var context = DbInterface.CreatePlayerContext(readOnly: false))
            {
                context.Player_Challenges.Remove(this);

                context.ChangeTracker.DetectChanges();
                context.SaveChanges();
            }
        }
    }
}
