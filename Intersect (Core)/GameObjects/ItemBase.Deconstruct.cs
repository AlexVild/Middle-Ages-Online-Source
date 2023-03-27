using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Intersect.Utilities;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
namespace Intersect.GameObjects
{
    public partial class ItemBase : DatabaseObject<ItemBase>, IFolderable
    {
        public int Fuel { get; set; }

        public int FuelRequired { get; set; }

        public long CraftWeaponExp { get; set; }

        [NotMapped] public List<LootRoll> DeconstructRolls = new List<LootRoll>();

        [Column("DeconstructRolls")]
        [JsonIgnore]
        public string GnomeTreasureJson
        {
            get => JsonConvert.SerializeObject(DeconstructRolls);
            set => DeconstructRolls = JsonConvert.DeserializeObject<List<LootRoll>>(value ?? string.Empty) ?? new List<LootRoll>();
        }

        public int EnhancementThreshold { get; set; }

        public Guid StudyEnhancement { get; set; }

        public double StudyChance { get; set; }
    }
}
