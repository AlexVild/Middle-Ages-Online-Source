using Intersect.Server.Entities;
using Intersect.Server.Networking;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
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

        [NotMapped, JsonIgnore]
        public HashSet<Player> Players { get; set; } = new HashSet<Player>();

        public void AddParticipant(Player player)
        {
            Players.Add(player);
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
    }
}
