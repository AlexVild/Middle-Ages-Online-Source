using Intersect.GameObjects;
using Intersect.Logging;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        [NotMapped, JsonIgnore]
        public List<PlayerEnhancementInstance> Enhancements { get; set; }

        [NotMapped, JsonIgnore]
        public IEnumerable<PlayerEnhancementInstance> KnownEnhancements => Enhancements.Where(en => en.Unlocked);
    }
}
