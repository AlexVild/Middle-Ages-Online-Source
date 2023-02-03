using Intersect.Server.Database.PlayerData;
using System.Collections.Generic;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public HashSet<ItemDiscoveryInstance> ItemsDiscovered { get; set; } = new HashSet<ItemDiscoveryInstance>();
    }
}
