using Intersect.Server.Database;
using Intersect.Server.Database.PlayerData.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Intersect.Server.Core.Games.ClanWars
{
    public class ClanWarParticipant
    {
        [NotMapped]
        private object mLock = new object();

        public Guid GuildId { get; set; }

        [ForeignKey(nameof(ClanWar))]
        public Guid ClanWarId { get; set; }

        [JsonIgnore, NotMapped]
        public virtual ClanWarInstance ClanWar { get; private set; }

        public int Score { get; set; }

        // EF
        public ClanWarParticipant()
        {
        }

        public ClanWarParticipant(Guid clanWarId, Guid guildId, int score = 0)
        {
            GuildId = guildId;
            ClanWarId = clanWarId;
            Score = score;
        }

        private void SaveToContext()
        {
            lock (mLock)
            {
                using (var context = DbInterface.CreatePlayerContext(readOnly: false))
                {
                    context.Clan_War_Participants.Update(this);
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
