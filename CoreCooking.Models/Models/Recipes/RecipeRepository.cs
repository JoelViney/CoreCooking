using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using CoreCooking.Models.Categories;
using CoreCooking.Models.Images;
using System.IO;

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
            var existing = await this.FindAsync(item.Guid);

            await base.SaveAsync(item);

            var repository = new CategoryRepository(this.ConnectionString);

            // Update Old Category?
            if (existing != null)
            {
                await RemoveCategoryReference(existing);

                if (existing.ImageUrl != null && existing.ImageUrl != item.ImageUrl)
                {
                    await RemoveImage(existing);
                }
            }
            
            // Update new Category
            {
                var category = await repository.GetAsync(item.CategoryGuid);

                var line = category.Recipes.Where(x => x.Guid == item.Guid).FirstOrDefault();
                if (line == null)
                {
                    line = new RecipeReference() { Guid = item.Guid };
                    category.Recipes.Add(line);
                    category.Recipes = category.Recipes.OrderBy(o => o.Name).ToList();
                }
                line.Name = item.Name;
                line.ImageUrl = item.ImageUrl;

                await repository.SaveAsync(category);
            }
        }

        public override async Task DeleteAsync(Recipe item)
        {
            await base.DeleteAsync(item);

            // Remove the reference from the Category
            await RemoveCategoryReference(item);
        }

        private async Task RemoveImage(Recipe item)
        {
            var repository = new ImageRepository(this.ConnectionString);

            Uri uri = new Uri(item.ImageUrl);
            string fileName = Path.GetFileName(uri.LocalPath);

            await repository.DeleteAsync(fileName);
        }

        private async Task RemoveCategoryReference(Recipe item)
        {
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
