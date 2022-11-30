using Intersect.Enums;
using Intersect.Utilities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intersect.GameObjects
{
    public class RecipeRequirement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        public string Hint { get; set; }

        [Column("Trigger")]
        public int TriggerValue { get; set; }

        [NotMapped, JsonIgnore]
        public RecipeTrigger Trigger => Enum.IsDefined(typeof(RecipeTrigger), TriggerValue) ? (RecipeTrigger)TriggerValue : RecipeTrigger.None;

        [ForeignKey(nameof(Recipe))]
        public Guid DescriptorId { get; set; }

        public RecipeDescriptor Recipe { get; set; }
        
        public Guid TriggerId { get; set; }

        public string Image { get; set; }

        public bool IsBool { get; set; }

        public bool BoolValue { get; set; }

        public int Amount { get; set; }

        [JsonConstructor]
        public RecipeRequirement(Guid descriptorId, int triggerValue, bool value)
        {
            DescriptorId = descriptorId;
            TriggerValue = triggerValue;
            BoolValue = value;
            IsBool = true;
        }

        [JsonConstructor]
        public RecipeRequirement(Guid descriptorId, int triggerValue, int value)
        {
            DescriptorId = descriptorId;
            TriggerValue = triggerValue;
            Amount = value;
            IsBool = false;
        }

        public string TriggerItemName => Trigger.GetRelatedTable().GetLookup().Get(DescriptorId)?.Name ?? "NOT FOUND";

        public override string ToString()
        {
            if (IsBool)
            {
                return $"{Trigger.GetDescription()}: {TriggerItemName} is {BoolValue}";
            }
            else
            {
                return $"{Trigger.GetDescription()}: {TriggerItemName}x {Amount}";
            }
        }
    }
}
