using Intersect.GameObjects.Events;
using System.Linq;

namespace Intersect.Server.Database.GameData.Migrations
{
    public class RecipeCrMigration
    {
        public static void Run(GameContext context)
        {
            MigrateRecipeRequirements(context);
        }

        public static void MigrateRecipeRequirements(GameContext context)
        {
            // Go through all of our items to reconfigure the new item binding properties.
            foreach (var recipe in context.Recipes)
            {
                foreach (var requirement in recipe.Requirements.Lists.ToArray())
                {
                    foreach (var condition in requirement.Conditions.OfType<HighestClassRankIs>().ToArray())
                    {
                        if (condition.Type == ConditionTypes.HighestClassRankIs)
                        {
                            recipe.MinClassRank = condition.ClassRank;
                            requirement.Conditions.Remove(condition);
                        }
                    }

                    if (requirement.Conditions.Count == 0)
                    {
                        recipe.Requirements.Lists.Remove(requirement);
                    }
                }
            }

            // Track our changes and save them or the work we've just done is lost.
            context.ChangeTracker.DetectChanges();
            context.SaveChanges();
        }
    }
}
