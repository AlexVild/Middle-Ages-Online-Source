using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Intersect.Utilities;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{
    public partial class NpcBase : DatabaseObject<NpcBase>, IFolderable
    {
        public Guid ChampionId { get; set; }

        public float ChampionSpawnChance { get; set; }

        public long ChampionCooldownSeconds { get; set; } = 600;

        public bool IsChampion { get; set; }
    }
}
