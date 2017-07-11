using CoreCooking.Data;
using CoreCooking.Models.Images;
using CoreCooking.Models.Recipes;
using CoreCooking.Models.Sites;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCooking.Models
{
    [TestClass]
    public class SystemTests
    {
        [TestMethod]
        public async Task Temp()
        {
            var siteRepository = new SiteRepository(SettingsFactory.GetConnectionString());
            var site = await siteRepository.GetAsync();

            var recipeRepository = new RecipeRepository(SettingsFactory.GetConnectionString());
            List<Recipe> recipeList = await recipeRepository.GetListAsync();

            foreach (var item in recipeList)
            {
                await recipeRepository.SaveAsync(item);
            }
            int i = 0;
        }


        [TestMethod]
        public async Task UpdateSiteRecipeIndexes()
        {
            var siteRepository = new SiteRepository(SettingsFactory.GetConnectionString());
            var site = await siteRepository.GetAsync();

            var recipeRepository = new RecipeRepository(SettingsFactory.GetConnectionString());
            List<Recipe> recipeList = await recipeRepository.GetListAsync();

            foreach (var item in recipeList)
            {
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
            }

            await siteRepository.SaveAsync(site);
        }


        [TestMethod]
        public async Task DeleteUnlinkedImages()
        {
            var siteRepository = new SiteRepository(SettingsFactory.GetConnectionString());
            var site = await siteRepository.GetAsync();

            var imageRepository = new ImageRepository(SettingsFactory.GetConnectionString());

            var list = await imageRepository.GetFileNamesAsync();

            foreach (var fileName in list)
            {
                bool found = false;
               
                foreach (var recipe in site.Recipes)
                {
                    if (recipe.ImageUrl != null && recipe.ImageUrl.Contains(fileName))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    await imageRepository.DeleteAsync(fileName);
                }
            }
        }


        [TestMethod]
        public async Task ClearnRecipe()
        {
            var repository = new RecipeRepository(SettingsFactory.GetConnectionString());
            List<Recipe> recipeList = await repository.GetListAsync();

            foreach (var recipe in recipeList)
            {
                if (recipe.Name != recipe.Name.Trim())
                {
                    recipe.Name = recipe.Name.Trim();
                    await repository.SaveAsync(recipe);
                }
            }
        }
    }
}
