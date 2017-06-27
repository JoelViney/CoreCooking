using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using CoreCooking.Models.Categories;

namespace CoreCooking.Models.Recipes
{
    public class RecipeRepository : RepositoryBase<Recipe>
    {
        public RecipeRepository(string connectionString) : base(connectionString, "recipes")
        {

        }

        public RecipeRepository(string connectionString, string containerPath) : base(connectionString, containerPath)
        {

        }

        public override async Task SaveAsync(Recipe item)
        {
            await base.SaveAsync(item);

            // Update the Category
            var repository = new CategoryRepository(this.ConnectionString);
            var category = await repository.GetAsync(item.CategoryGuid);

            RecipeReference line = category.Recipes.Where(x => x.Guid == item.Guid).FirstOrDefault();
            if (line == null)
            {
                line = new RecipeReference() { Guid = item.Guid };
                category.Recipes.Add(line);
            }
            line.Name = item.Name;

            await repository.SaveAsync(category);
        }

        public override async Task DeleteAsync(Recipe item)
        {
            await base.DeleteAsync(item);

            var repository = new CategoryRepository(this.ConnectionString);
            var category = await repository.GetAsync(item.CategoryGuid);

            RecipeReference line = category.Recipes.Where(x => x.Guid == item.Guid).FirstOrDefault();
            if (line != null)
            {
                category.Recipes.Remove(line);
                await repository.SaveAsync(category);
            }
        }
    }
}
