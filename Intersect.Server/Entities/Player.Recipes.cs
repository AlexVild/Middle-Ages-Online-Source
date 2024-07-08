using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Crafting;
using Intersect.Network.Packets.Server;
using Intersect.Server.Core;
using Intersect.Server.Database.PlayerData.Players;
using Intersect.Server.General;
using Intersect.Server.Networking;
using Intersect.Utilities;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : AttackingEntity
    {
        public List<RecipeInstance> UnlockedRecipes { get; set; } = new List<RecipeInstance>();

        [NotMapped, JsonIgnore]
        public Guid[] UnlockedRecipeIds => UnlockedRecipes.Where(recipe => recipe.Unlocked).Select(recipe => recipe.DescriptorId).ToArray();
        public List<Guid> LockedRecipeIds => UnlockedRecipes.Where(recipe => !recipe.Unlocked).Select(recipe => recipe.DescriptorId).ToList();

        public bool RecipeTutorialDone { get; set; } = false;

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

            var msg = $"You've completed a crafting challenge: {recipeName}!";
            if (recipeType != RecipeCraftType.Other && recipeType != null)
            {
                msg = $"You've unlocked a {recipeType.GetDescription()} challenge: {recipeName}!";
            }
            PacketSender.SendChatMsg(this, msg, Enums.ChatMessageType.Experience, CustomColors.General.GeneralCompleted, sendToast: true);
        }

        /// <summary>
        /// Soft deletes recipes from a player
        /// </summary>
        /// <param name="descriptorId"></param>
        public void RemoveRecipes(Guid descriptorId)
        {
            if (UnlockedRecipes == null)
            {
                return;
            }

            foreach(var recipe in UnlockedRecipes.Where(recipe => recipe.DescriptorId == descriptorId))
            {
                recipe.Unlocked = false;
            }
        }

        public RecipeDisplayPackets GetRecipes(RecipeCraftType craftType)
        {
            var recipes = new List<RecipeDisplayPacket>();
            foreach (var recipe in Globals.CachedRecipes.ToArray()
                .Where(r => r.CraftType == craftType)
                .OrderBy(r => !UnlockedRecipeIds.Contains(r.Id) && r.HiddenUntilUnlocked)
                .ThenBy(r => r.MinClassRank)
                .ThenBy(r => r.DisplayName ?? r.Name))
            {
                var packet = new RecipeDisplayPacket();

                // For now, multi-classing is not allowed, so don't show class-specific recipes
                if (!recipe?.ClassesThatCanLearn()?.Contains(ClassId) ?? false)
                {
                    continue;
                }

                packet.DescriptorId = recipe.Id;
                packet.IsUnlocked = UnlockedRecipeIds.Contains(recipe.Id);
                packet.Visible = RecipeIsVisible(recipe) || packet.IsUnlocked;

                recipes.Add(packet);
            }

            return new RecipeDisplayPackets(recipes);
        }

        public RecipeRequirementPackets GetRecipeProgress(Guid recipeId)
        {
            var requirements = new List<RecipeRequirementPacket>();
            var recipe = RecipeDescriptor.Get(recipeId);
            if (recipe == default)
            {
                return new RecipeRequirementPackets(requirements);
            }

            foreach (var req in recipe.RecipeRequirements)
            {
                var packet = new RecipeRequirementPacket();
                packet.RecipeId = recipe.Id;

                packet.Completed = UnlockedRecipeIds.Contains(recipe.Id) || RecipeUnlockWatcher.RequirementComplete(this, req);

                packet.Hint = req.Hint;
                packet.TriggerType = req.Trigger;
                packet.TriggerId = req.TriggerId;
                packet.Amount = req.IsBool ? 1 : req.Amount;

                if (!packet.Completed)
                {
                    packet.Progress = req.IsBool ? 0 : RecipeUnlockWatcher.GetNumericRequirementProgress(this, req);
                }

                if (req.Trigger == RecipeTrigger.None || req.Trigger == RecipeTrigger.PlayerVarChange)
                {
                    packet.Image = string.Empty;
                }
                else
                {
                    var gameObject = req.Trigger.GetRelatedTable();
                    switch (gameObject)
                    {
                        case GameObjectType.Crafts:
                            var craft = gameObject.GetLookup().Get(req.TriggerId) as CraftBase;
                            packet.Image = ItemBase.Get(craft.ItemId)?.Icon ?? string.Empty;
                            packet.ImageSourceDir = 1; // TextureType.Item
                            break;
                        case GameObjectType.Resource:
                            var resource = gameObject.GetLookup().Get(req.TriggerId) as ResourceBase;
                            // Don't support resource from tileset
                            packet.Image = resource?.Initial?.GraphicFromTileset ?? true 
                                ? string.Empty : resource?.Initial?.Graphic ?? string.Empty;
                            packet.ImageSourceDir = 8;
                            break;
                        case GameObjectType.Npc:
                            var npc = gameObject.GetLookup().Get(req.TriggerId) as NpcBase;
                            packet.Image = npc?.Sprite ?? string.Empty;
                            packet.ImageSourceDir = 2;
                            break;
                        case GameObjectType.Item:
                            var item = gameObject.GetLookup().Get(req.TriggerId) as ItemBase;
                            packet.Image = item?.Icon ?? string.Empty;
                            packet.ImageSourceDir = 1;
                            break;
                        default:
                            packet.Image = string.Empty;
                            break;
                    }
                }

                requirements.Add(packet);
            }

            return new RecipeRequirementPackets(requirements);
        }
    }
}
