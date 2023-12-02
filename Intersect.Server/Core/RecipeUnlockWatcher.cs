using Intersect.GameObjects;
using Intersect.Server.Entities;
using Intersect.Server.Entities.Events;
using Intersect.Server.General;
using Intersect.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intersect.Server.Core
{
    public static class RecipeUnlockWatcher
    {
        private struct QueuedPlayer
        {
            public Player Player;
            public Guid TriggerId;
            public RecipeTrigger TriggerType;

            public QueuedPlayer(Player player, Guid triggerId, RecipeTrigger triggerType)
            {
                Player = player;
                TriggerId = triggerId;
                TriggerType = triggerType;
            }
        }

        public static int RecipeIdx { get; private set; }

        /// <summary>
        /// Contains a reference to all players who need to have their recipe unlocks checked
        /// </summary>
        private static Queue<QueuedPlayer> UpdateQueue { get; set; } = new Queue<QueuedPlayer>();

        /// <summary>
        /// Contains a referencce to all players who need to check ALL recipes, as a recipe descriptor refresh has occured.
        /// </summary>
        private static Queue<Player> RefreshQueue { get; set; } = new Queue<Player>();

        public static bool ShouldRefresh = false;

        /// <summary>
        /// Queues a refresh action and fetches all online players to prepare them for such
        /// </summary>
        public static void QueueRefresh()
        {
            foreach (var player in Globals.OnlineList.ToArray())
            {
                RefreshQueue.Enqueue(player);
            }
            ShouldRefresh = true;
        }

        /// <summary>
        /// Will refresh every player's recipe status with the values in the current RecipeDescriptor cache
        /// </summary>
        public static void DoRefresh()
        {
            if (!ShouldRefresh || RefreshQueue.Count == 0 || Globals.CachedRecipes.Count == 0)
            {
                ShouldRefresh = false;
                return;
            }

            var player = RefreshQueue.Peek();
            RefreshPlayer(player);
            RefreshQueue.Dequeue();
        }

        /// <summary>
        /// Will reflect a player's recipe status for each recipe in the recipe cache
        /// </summary>
        /// <param name="player"></param>
        public static void RefreshPlayer(Player player)
        {
            foreach(var recipe in Globals.CachedRecipes.ToArray())
            {
                if (!player.Online || recipe == null)
                {
                    return;
                }

                // Is the recipe meant to be triggered via event only?
                if (recipe.RecipeRequirements.Count == 0 && recipe.Requirements.Count == 0)
                {
                    continue;
                }

                if (!player.RecipeIsVisible(recipe))
                {
                    player.RemoveRecipes(recipe.Id);
                    continue;
                }

                // Does the recipe have task requirements that aren't satisfied?
                if (recipe.RecipeRequirements.Count > 0 && !recipe.RecipeRequirements.All(rqr => RequirementComplete(player, rqr)))
                {
                    player.RemoveRecipes(recipe.Id);
                    continue;
                }

                player.UnlockRecipe(recipe.Id, true);
            }
        }

        /// <summary>
        /// Whenever a player has some recipe we need to check on, we, one at-a-tick, check through all our recipes for an update
        /// </summary>
        public static void Iterate()
        {
            if (UpdateQueue.Count == 0 || Globals.CachedRecipes.Count == 0)
            {
                return;
            }

            QueuedPlayer currentPlayer = UpdateQueue.Peek();
            var recipe = Globals.CachedRecipes.ElementAtOrDefault(RecipeIdx);

            if (recipe == null || RecipeIdx >= Globals.CachedRecipes.Count || !currentPlayer.Player.Online)
            {
                Logging.Log.Debug("Recipe loop completed");
                Dequeue();
                return;
            }

            // Player has already unlocked this recipe
            var playerRecipe = currentPlayer.Player.UnlockedRecipes.Find(unlocked => unlocked.Id == recipe.Id);
            if (playerRecipe != default && playerRecipe.Unlocked)
            {
                Continue();
                return;
            }

            // Player does not meet requirements for the recipe - ignore it.
            if (currentPlayer.Player.RecipeIsVisible(recipe))
            {
                Continue();
                return;
            }

            var requirements = recipe.RecipeRequirements.ToArray();

            if (requirements.Length == 0)
            {
                Continue();
                return;
            }

            var recipeUnlocked = requirements.All(rqr => RequirementComplete(currentPlayer.Player, rqr));
            if (recipeUnlocked)
            {
                currentPlayer.Player.UnlockRecipe(recipe.Id);
            }

            Continue();
        }

        /// <summary>
        /// Process an iteration completion
        /// </summary>
        private static void Continue()
        {
            RecipeIdx++;
        }

        /// <summary>
        /// Remove a player from our update queue and reset the process
        /// </summary>
        private static void Dequeue()
        {
            UpdateQueue.Dequeue();
            RecipeIdx = 0;
        }

        /// <summary>
        /// Check to see if a player has some <see cref="RecipeRequirement"/> completed
        /// </summary>
        /// <param name="player">The player we care about</param>
        /// <param name="requirement">The requirement we're checking for completion of</param>
        /// <returns></returns>
        public static bool RequirementComplete(Player player, RecipeRequirement requirement)
        {
            if (requirement == null || player == null)
            {
                return false;
            }

            if (requirement.IsBool)
            {
                if (requirement.Trigger == RecipeTrigger.PlayerVarChange)
                {
                    var variableValue = player.GetVariableValue(requirement.TriggerId);
                    if (variableValue.Type == Enums.VariableDataTypes.Boolean)
                    {
                        return requirement.IsBool && variableValue.Boolean;
                    }
                    return false;
                }
            }

            // This requirement is just meant to supply a hint - the requirement is fulfilled when the recipe is unlocked.
            if (requirement.Trigger == RecipeTrigger.None)
            {
                return player.UnlockedRecipeIds.Contains(requirement.DescriptorId);
            }
            if (requirement.Trigger == RecipeTrigger.SpellLearned)
            {
                return player.TryGetSkillInSkillbook(requirement.TriggerId, out _);
            }

            var progress = GetNumericRequirementProgress(player, requirement);

            return progress >= requirement.Amount;
        }

        public static int GetNumericRequirementProgress(Player player, RecipeRequirement requirement)
        {
            if (requirement == null || player == null)
            {
                return 0;
            }

            try
            {
                var recordType = requirement.Trigger.GetRelatedRecordType();

                var relevantRecord = player.PlayerRecords.Find(record =>
                {
                    return record.Type == recordType && record.RecordId == requirement.TriggerId;
                });

                if (relevantRecord == default)
                {
                    return 0;
                }

                return (int)relevantRecord.Amount;
            }
            // Failed to parse the trigger type as a PlayerRecord - get their values in different ways
            catch (ArgumentException e)
            {
                switch (requirement.Trigger)
                {
                    // These triggers don't allow amounts, they are effectively boolean
                    case RecipeTrigger.None:
                    case RecipeTrigger.SpellLearned:
                        return 0;

                    case RecipeTrigger.PlayerVarChange:
                        var variableValue = player.GetVariableValue(requirement.TriggerId);

                        switch (variableValue.Type)
                        {
                            case Enums.VariableDataTypes.Number:
                                return variableValue.Value;
                            default:
                                Logging.Log.Error("Invalid variable type when evaluating recipe unlock");
                                return 0;
                        }

                    case RecipeTrigger.ItemObtained:
                        return player.CountItems(requirement.TriggerId, true, true);

                    default:
                        Logging.Log.Error("Invalid trigger type when evaluating recipe unlock");
                        return 0;
                }
            }
        }

        /// <summary>
        /// Adds a new player to our iteration queue
        /// </summary>
        /// <param name="player">The player we want to add</param>
        /// <param name="triggerId">The Guid that refers to the trigger value, i.e a player var ID or an NPC ID</param>
        /// <param name="triggerType">The <see cref="RecipeTrigger"/> that we're checking for</param>
        public static void EnqueueNewPlayer(Player player, Guid triggerId, RecipeTrigger triggerType)
        {
            UpdateQueue.Enqueue(new QueuedPlayer(player, triggerId, triggerType));
        }
    }
}
