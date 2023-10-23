using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amib.Threading;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.GameObjects.QuestBoard;
using Intersect.GameObjects.QuestList;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Switches_and_Variables;
using Intersect.Logging;
using Intersect.Network;
using Intersect.Network.Packets.Server;
using Intersect.Server.Database;
using Intersect.Server.Database.Logging.Entities;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Database.PlayerData.Security;
using Intersect.Server.Entities.Combat;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Server.Localization;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using Intersect.Utilities;

using Newtonsoft.Json;
using Intersect.Server.Entities.PlayerData;
using Intersect.Server.Database.PlayerData;
using static Intersect.Server.Maps.MapInstance;
using Intersect.Server.Core;
using Intersect.GameObjects.Timers;
using Intersect.Server.Utilities;
using System.Text;
using System.ComponentModel;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {

        [JsonIgnore, Column("CraftingWishlist")]
        public string CraftingWishlistJson
        {
            get => JsonConvert.SerializeObject(CraftingWishlist);
            set => CraftingWishlist = JsonConvert.DeserializeObject<List<Guid>>(value ?? string.Empty) ?? new List<Guid>();
        }

        [JsonIgnore, NotMapped] public List<Guid> CraftingWishlist = new List<Guid>();

        public void AddCraftToWishlist(Guid craftId)
        {
            if (CraftingWishlist.Contains(craftId))
            {
                return;
            }

            CraftingWishlist.Add(craftId);
            PacketSender.SendCraftingWishlist(this);
        }

        public void RemoveCraftFromWishlist(Guid craftId)
        {
            CraftingWishlist.RemoveAll(id => id == craftId);
            PacketSender.SendCraftingWishlist(this);
        }
    }
}
