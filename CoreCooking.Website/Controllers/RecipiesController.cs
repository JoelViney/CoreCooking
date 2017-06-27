using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreCooking.Models;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Models.Recipes;
using CoreCooking.Models.Categories;
using CoreCooking.Website.ViewModels.Categories;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;
using CoreCooking.Website.Helpers;
using CoreCooking.Models.Sites;

namespace CoreCooking.Website.Controllers
{
    public class RecipesController : Controller
    {
        private Settings _settings;

        public RecipesController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }

        public async Task<IActionResult> Details(string categoryName, string name)
        {
            Category category;
            Guid recipeGuid;
            {
                var categoryRepository = new CategoryRepository(_settings.AzureStorageConnectionString);
                category = await categoryRepository.SearchAsync(categoryName);

                var item = category.Recipes.Where(x => x.Name == name).FirstOrDefault();
                recipeGuid = item.Guid;
            }

            
            {
                var repository = new RecipeRepository(_settings.AzureStorageConnectionString);
                var item = await repository.GetAsync(recipeGuid);

                item.ProcessRecipe();

                RecipeViewModel viewModel = new RecipeViewModel(category, item);

                return View(viewModel);
            }

        }

        public async Task<IActionResult> Add(string categoryName)
        {
            var categoryRepository = new CategoryRepository(_settings.AzureStorageConnectionString);
            Category category = await categoryRepository.SearchAsync(categoryName);

            var viewModel = new RecipeEditViewModel(category, new Recipe());

            return View("Edit", viewModel);
        }


        public async Task<IActionResult> Edit(string categoryName, string name)
        {
            Category category;
            Guid recipeGuid;
            {
                var categoryRepository = new CategoryRepository(_settings.AzureStorageConnectionString);
                category = await categoryRepository.SearchAsync(categoryName);

                var item = category.Recipes.Where(x => x.Name == name).FirstOrDefault();
                recipeGuid = item.Guid;
            }

            RecipeEditViewModel viewModel;
            {
                var repository = new RecipeRepository(_settings.AzureStorageConnectionString);
                var item = await repository.GetAsync(recipeGuid);

                item.ProcessRecipe();

                viewModel = new RecipeEditViewModel(category, item);
            }

            await FillCategoriesViewBagAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEditViewModel viewModel)
        {
            Recipe item;
            {
                var repository = new RecipeRepository(_settings.AzureStorageConnectionString);

                if (viewModel.IsNew())
                    item = new Recipe();
                else
                    item = await repository.GetAsync(viewModel.Guid);

                viewModel.FillModel(item);

                await repository.SaveAsync(item);
            }

            return RedirectToAction("View", "Recipes", new { guid = item.Guid });
        }


        public async Task<IActionResult> Delete(string categoryName, string name)
        {
            Category category;
            Guid recipeGuid;
            {
                var categoryRepository = new CategoryRepository(_settings.AzureStorageConnectionString);
                category = await categoryRepository.SearchAsync(categoryName);

                var item = category.Recipes.Where(x => x.Name == name).FirstOrDefault();
                recipeGuid = item.Guid;
            }

            {
                var repository = new RecipeRepository(_settings.AzureStorageConnectionString);
                var item = await repository.GetAsync(recipeGuid);
                await repository.DeleteAsync(item);
            }

            var viewModel = new CategoryViewModel(category);
            return Redirect(UrlBuilder.GetUrl(viewModel));
        }

        public async Task FillCategoriesViewBagAsync()
        {
            var repository = new CategoryRepository(_settings.AzureStorageConnectionString);

            var list = await repository.GetListAsync();

            var viewModelList = new List<CategoryLineViewModel>();
            foreach (var item in list)
            {
                var viewModel = new CategoryLineViewModel(item);
                viewModelList.Add(viewModel);
            }

            ViewData.Add("Categorries", viewModelList);
        }
    }
}