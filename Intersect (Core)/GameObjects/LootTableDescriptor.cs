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

        [NotMapped] public List<BaseDrop> Drops = new List<BaseDrop>();

        [Column("Drops")]
        [JsonIgnore]
        public string JsonDrops
        {
            get => JsonConvert.SerializeObject(Drops);
            set => Drops = JsonConvert.DeserializeObject<List<BaseDrop>>(value);
        }

        public string DisplayName { get; set; }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
    }

    public class LootRoll
    {
        public Guid DescriptorId;

        public LootTableDescriptor LootTable => LootTableDescriptor.Get(DescriptorId) ?? null;

        public int Rolls = 0;

        public LootRoll(Guid descriptorId, int rolls)
        {
            DescriptorId = descriptorId;
            Rolls = rolls;
        }

        public LootRoll Clone()
        {
            return new LootRoll(this.DescriptorId, this.Rolls);
        }
    }
}
