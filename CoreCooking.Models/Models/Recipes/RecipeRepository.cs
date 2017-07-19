using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using CoreCooking.Models.Images;
using System.IO;
using CoreCooking.Models.Sites;

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

            // Update old Hashtags?
            if (existing != null)
            {
                await RemoveReference(existing);

                if (existing.ImageUrl != null && existing.ImageUrl != item.ImageUrl)
                {   
                    // The image has changed so delete the old image.
                    await RemoveImage(existing);
                }
            }

            // Update new Hashtags
            await AddReference(item);
        }

        public override async Task DeleteAsync(Recipe item)
        {
            await base.DeleteAsync(item);

            // Remove the reference from the Category
            await RemoveReference(item);
        }

        private async Task RemoveImage(Recipe item)
        {
            var repository = new ImageRepository(this.ConnectionString);

            Uri uri = new Uri(item.ImageUrl);
            string fileName = Path.GetFileName(uri.LocalPath);

            await repository.DeleteAsync(fileName);
        }

        private async Task AddReference(Recipe item)
        {
            // Update Site
            {
                var repository = new SiteRepository(this.ConnectionString);
                var site = await repository.GetAsync();

                var line = site.Recipes.Where(x => x.Guid == item.Guid).FirstOrDefault();
                if (line == null)
                {
                    line = new RecipeIndex() { Guid = item.Guid };
                    site.Recipes.Add(line);
                    site.Recipes = site.Recipes.OrderBy(o => o.Name).ToList();
                }
                line.Name = item.Name;
                line.HashtagsString = item.HashtagsString;
                line.ImageUrl = item.ImageUrl;

                await repository.SaveAsync(site);
            }

        }

        private async Task RemoveReference(Recipe item)
        {
            // Site
            {
                var repository = new SiteRepository(this.ConnectionString);
                var site = await repository.GetAsync();

                RecipeIndex line = site.Recipes.Where(x => x.Guid == item.Guid).FirstOrDefault();

                if (line != null)
                {
                    site.Recipes.Remove(line);
                    await repository.SaveAsync(site);
                }
            }

        }
    }
}
