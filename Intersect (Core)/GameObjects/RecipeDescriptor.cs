using Intersect.Attributes;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Crafting;
using Intersect.Models;
using Intersect.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.GameObjects
{
    public enum RecipeCraftType
    {
        [Description("Other")]
        Other = 0,

        [Description("Cooking")]
        Cooking,

        [Description("Smelting")]
        Smelting,

        [Description("Forging")]
        Forging,

        [Description("Fletching")]
        Fletching,

        [Description("Leather Working")]
        LeatherWork,

        [Description("Alchemy")]
        Alchemy,

        [Description("Summoning & Spellcraft")]
        MageCraft,

        [Description("Mage Craft")]
        MageSmithing,
    }

    public enum RecipeTrigger
    {
        [Description("Event")]
        None = 0,

        [Description("Player Variable Change"), RelatedTable(Enums.GameObjectType.PlayerVariable)]
        PlayerVarChange,

        [Description("Enemy Killed"), RelatedTable(Enums.GameObjectType.Npc), RecordType(Events.RecordType.NpcKilled)]
        EnemyKilled,

        [Description("Resource Harvested"), RelatedTable(Enums.GameObjectType.Resource), RecordType(Events.RecordType.ResourceGathered)]
        ResourceHarvested,

        [Description("Craft Crafted"), RelatedTable(Enums.GameObjectType.Crafts), RecordType(Events.RecordType.ItemCrafted)]
        CraftCrafted,

        [Description("Item Obtained"), RelatedTable(Enums.GameObjectType.Item)]
        ItemObtained,

        [Description("Spell Learned"), RelatedTable(Enums.GameObjectType.Spell)]
        SpellLearned
    }

    public class RecipeDescriptor : DatabaseObject<RecipeDescriptor>, IFolderable
    {
        public string DisplayName { get; set; }

        public string Hint { get; set; }

        [Column("CraftType")]
        public int CraftTypeValue { get; set; }

        [NotMapped, JsonIgnore]
        public RecipeCraftType CraftType => Enum.IsDefined(typeof(RecipeCraftType), CraftTypeValue) ? (RecipeCraftType)CraftTypeValue : RecipeCraftType.Other;

        [Column("Trigger"), Obsolete("Moved props to RequirementRecipes")]
        public int TriggerValue { get; set; }

        [NotMapped, JsonIgnore, Obsolete("Moved props to RequirementRecipes")]
        public RecipeTrigger Trigger => Enum.IsDefined(typeof(RecipeTrigger), TriggerValue) ? (RecipeTrigger)TriggerValue : RecipeTrigger.None;

        [Obsolete("Moved props to RequirementRecipes")]
        public Guid TriggerParam { get; set; }

        public string DisplayCraftType => CraftType.GetDescription();

        public int MinClassRank { get; set; }

        [NotMapped]
        public ConditionLists Requirements { get; set; } = new ConditionLists();

        [Column("Requirements")]
        [JsonIgnore]
        public string JsonRequirements
        {
            get => Requirements.Data();
            set => Requirements.Load(value);
        }
        
        public string Folder { get; set; } = "";

        public string Image { get; set; }

        public bool HiddenUntilUnlocked { get; set; }

        public List<RecipeRequirement> RecipeRequirements { get; set; }

        public RecipeDescriptor() : this(default)
        {
        }

        [JsonConstructor]
        public RecipeDescriptor(Guid id) : base(id)
        {
            Name = "New Recipe";
            RecipeRequirements = new List<RecipeRequirement>();
        }

        public Guid[] ClassesThatCanLearn()
        {
            var validClasses = ClassBase.GetAll().Select(cls => cls.Id).ToArray();
            var includedClasses = new HashSet<Guid>();

            if (Requirements.Count <= 0)
            {
                return validClasses;
            }

            foreach (var requirement in Requirements.Lists.ToArray())
            {
                foreach (var condition in requirement.Conditions.OfType<Events.ClassIsCondition>())
                {
                    includedClasses.Add(condition.ClassId);
                }
                foreach (var condition in requirement.Conditions.OfType<Events.InNpcGuildWithRankCondition>())
                {
                    includedClasses.Add(condition.ClassId);
                }
            }

            return includedClasses.ToArray();
        }
    }
}
