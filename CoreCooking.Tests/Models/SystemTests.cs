using CoreCooking.Data;
using CoreCooking.Models.Categories;
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
        public async Task CleanSiteReferences()
        {
            var repository = new SiteRepository(SettingsFactory.GetConnectionString());
            var site = await repository.GetAsync();

            var categoryRepository = new CategoryRepository(SettingsFactory.GetConnectionString());
            var categoryList = await categoryRepository.GetListAsync();

            // Delete Excessive References
            for (int i = site.Categories.Count - 1; i >= 0; i--)
            {
                var reference = site.Categories[i];

                if (!categoryList.Any(x => x.Guid == reference.Guid))
                {
                    site.Categories.Remove(reference);
                }
            }

            // Add any unlinked references.
            foreach (var category in categoryList)
            {
                CategoryReference line = site.Categories.Where(x => x.Guid == category.Guid).FirstOrDefault();

                if (line == null)
                {
                    line = new CategoryReference() { Guid = category.Guid };
                    site.Categories.Add(line);
                }

                line.Name = category.Name;
            }

            await repository.SaveAsync(site);
        }


        [TestMethod]
        public async Task DeleteUnlinkedImages()
        {
            var siteRepository = new SiteRepository(SettingsFactory.GetConnectionString());
            var site = await siteRepository.GetAsync();

            var categoryRepository = new CategoryRepository(SettingsFactory.GetConnectionString());
            var categoryList = await categoryRepository.GetListAsync();

            var imageRepository = new ImageRepository(SettingsFactory.GetConnectionString());

            var list = await imageRepository.GetFileNamesAsync();

            foreach (var fileName in list)
            {
                bool found = false;
                foreach (var category in categoryList)
                {
                    foreach (var recipe in category.Recipes)
                    {
                        if (recipe.ImageUrl != null && recipe.ImageUrl.Contains(fileName))
                        {
                            found = true;
                        }
                    }
                }
                if (!found)
                {
                    await imageRepository.DeleteAsync(fileName);
                }
            }
        }


        [TestMethod]
        public async Task GenerateCategoryRecipeLines()
        {
            var recipeRepository = new RecipeRepository(SettingsFactory.GetConnectionString());
            List<Recipe> recipeList = await recipeRepository.GetListAsync();

            var repository = new CategoryRepository(SettingsFactory.GetConnectionString());

            var categoryList = await repository.GetListAsync();
            foreach (var category in categoryList)
            {
                var filteredList = recipeList.Where(x => x.CategoryGuid == category.Guid).ToList();

                foreach (var recipe in filteredList)
                {
                    RecipeReference line;
                    if (category.Recipes.Any(x => x.Guid == recipe.Guid))
                    {
                        line = category.Recipes.Where(x => x.Guid == recipe.Guid).FirstOrDefault();
                    }
                    else
                    {
                        line = new RecipeReference() { Guid = recipe.Guid };
                        category.Recipes.Add(line);
                    }
                    line.ImageUrl = recipe.ImageUrl;
                    line.Name = recipe.Name;
                }

                await repository.SaveAsync(category);

            }
        }
    }
}
