using Intersect.GameObjects.Conditions;
using Intersect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.GameObjects
{
    public class LootTableDescriptor : DatabaseObject<LootTableDescriptor>, IFolderable
    {
        public LootTableDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public LootTableDescriptor(Guid id) : base(id)
        {
            Name = "New Table";
        }

        [NotMapped] public ConditionLists DropConditions = new ConditionLists();

        [Column("DropConditions")]
        [JsonIgnore]
        public string DropConditionsJson
        {
            get => DropConditions.Data();
            set => DropConditions.Load(value);
        }

        [NotMapped] public List<NpcDrop> Drops = new List<NpcDrop>();

        [Column("Drops")]
        [JsonIgnore]
        public string JsonDrops
        {
            get => JsonConvert.SerializeObject(Drops);
            set => Drops = JsonConvert.DeserializeObject<List<NpcDrop>>(value);
        }

        public string DisplayName { get; set; }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }
}
