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

        [NotMapped, JsonIgnore]
        public RecipeDescriptor Recipe { get; set; }
        
        public Guid TriggerId { get; set; }

        public string Image { get; set; }

        public bool IsBool { get; set; }

        public bool BoolValue { get; set; }

        public int Amount { get; set; }

        public RecipeRequirement() { }

        public RecipeRequirement(Guid descriptorId, Guid triggerId, int triggerValue, bool value, string hint)
        {
            DescriptorId = descriptorId;
            TriggerId = triggerId;
            TriggerValue = triggerValue;
            BoolValue = value;
            IsBool = true;
            Hint = hint;
        }

        public RecipeRequirement(Guid descriptorId, Guid triggerId, int triggerValue, int value, string hint)
        {
            DescriptorId = descriptorId;
            TriggerId = triggerId;
            TriggerValue = triggerValue;
            Amount = value;
            IsBool = false;
            Hint = hint;
        }

        public override string ToString()
        {
            if (Trigger == RecipeTrigger.None)
            {
                return $"{Trigger.GetDescription()}: {Hint}";
            }

            var triggerItemName = Trigger.GetRelatedTable().GetLookup().Get(TriggerId)?.Name ?? "NOT FOUND";
            
            if (Trigger == RecipeTrigger.SpellLearned)
            {
                return $"{Trigger.GetDescription()}: {triggerItemName}";
            }


            if (IsBool)
            {
                return $"{Trigger.GetDescription()}: {triggerItemName} is {BoolValue}";
            }
            else
            {
                return $"{Trigger.GetDescription()}: {triggerItemName} x{Amount}";
            }
        }
    }
}
