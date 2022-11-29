using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Server.Database.PlayerData.Players;

using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<RecipeInstance> UnlockedRecipes { get; set; }

        [NotMapped, JsonIgnore]
        public Guid[] UnlockedRecipeIds => UnlockedRecipes.Select(recipe => recipe.DescriptorId).ToArray();

        public void UnlockRecipe(Guid descriptorId)
        {
            if (UnlockedRecipeIds.Contains(descriptorId))
            {
                return;
            }

            UnlockedRecipes.Add(new RecipeInstance(Id, descriptorId));
        }

        /// <summary>
        /// Soft deletes recipes from a player
        /// </summary>
        /// <param name="descriptorId"></param>
        public void RemoveRecipes(Guid descriptorId)
        {
            UnlockedRecipes.Where(recipe => recipe.DescriptorId == descriptorId).Select(recipe => recipe.Unlocked = false);
        }
    }
}
