using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Intersect.Server.Networking;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public bool EnhancementOpen => Enhancement != default;

        [NotMapped, JsonIgnore]
        public EnhancementInterface Enhancement { get; set; }

        public void OpenEnhancement(Guid currencyId, float multiplier)
        {
            Enhancement = new EnhancementInterface(this, currencyId, multiplier);
            PacketSender.SendOpenEnhancementWindow(this, currencyId, multiplier);
        }

        public void CloseEnhancement()
        {
            Enhancement = null;
        }
    }
}

