using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.GameObjects;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<RecipeInstance> UnlockedRecipes { get; set; }

        [NotMapped, JsonIgnore]
        public Guid[] UnlockedRecipeIds => UnlockedRecipes.Where(recipe => recipe.Unlocked).Select(recipe => recipe.DescriptorId).ToArray();
        public List<Guid> LockedRecipeIds => UnlockedRecipes.Where(recipe => !recipe.Unlocked).Select(recipe => recipe.DescriptorId).ToList();

        public void UnlockRecipe(Guid descriptorId, bool suppressMsg = false)
        {
            if (UnlockedRecipeIds.Contains(descriptorId))
            {
                return;
            }

            if (!LockedRecipeIds.Contains(descriptorId))
            {
                UnlockedRecipes.Add(new RecipeInstance(Id, descriptorId));
            }
            else
            {
                var lockedRecipe = UnlockedRecipes.Find(unlockedRecipe => unlockedRecipe.DescriptorId == descriptorId);
                if (lockedRecipe == default)
                {
                    UnlockedRecipes.Add(new RecipeInstance(Id, descriptorId));
                }
                else
                {
                    lockedRecipe.Unlocked = true;
                }
            }

            if (suppressMsg)
            {
                return;
            }

            var recipe = RecipeDescriptor.Get(descriptorId);
            var recipeName = recipe?.DisplayName ?? recipe?.Name ?? "NOT FOUND";
            var recipeType = recipe?.CraftType;

            var msg = $"You've unlocked a new recipe: {recipeName}!";
            if (recipeType != RecipeCraftType.Other && recipeType != null)
            {
                msg = $"You've unlocked a new {recipeType.GetDescription()} recipe: {recipeName}!";
            }
            PacketSender.SendChatMsg(this, msg, Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted, sendToast: true);
        }

        /// <summary>
        /// Soft deletes recipes from a player
        /// </summary>
        /// <param name="descriptorId"></param>
        public void RemoveRecipes(Guid descriptorId)
        {
            foreach(var recipe in UnlockedRecipes.Where(recipe => recipe.DescriptorId == descriptorId))
            {
                recipe.Unlocked = false;
            }
        }
    }
}
