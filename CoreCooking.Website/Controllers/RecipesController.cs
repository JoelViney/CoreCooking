using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreCooking.Models;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Models.Recipes;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;
using CoreCooking.Website.Helpers;
using CoreCooking.Models.Sites;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreCooking.Website.ViewModels.Home;

namespace CoreCooking.Website.Controllers
{
    public class RecipesController : Controller
    {
        private Settings _settings;

        public RecipesController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }

        public async Task<IActionResult> Details(string hashtag, string name)
        {
            var recipeGuid = await this.GetRecipeGuid(name);
            
            var repository = new RecipeRepository(_settings.AzureStorageConnectionString);
            var item = await repository.GetAsync(recipeGuid);

            item.ProcessRecipe();

            RecipeViewModel viewModel = new RecipeViewModel(hashtag, item);

            return View(viewModel);
        }

        public IActionResult Add(string hashtag)
        {
            var viewModel = new RecipeEditViewModel(hashtag, new Recipe());

            return View("Edit", viewModel);
        }

        public async Task<IActionResult> Edit(string hashtag, string name)
        {
            Guid recipeGuid = await this.GetRecipeGuid(name);

            var repository = new RecipeRepository(_settings.AzureStorageConnectionString);
            var item = await repository.GetAsync(recipeGuid);

            item.ProcessRecipe();

            var viewModel = new RecipeEditViewModel(hashtag, item);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEditViewModel viewModel)
        {
            Recipe item;
            
            var repository = new RecipeRepository(_settings.AzureStorageConnectionString);

            if (viewModel.IsNew())
                item = new Recipe();
            else
                item = await repository.GetAsync(viewModel.Guid);

            viewModel.FillModel(item);

            await repository.SaveAsync(item);
            
            return RedirectToAction("Details", "Recipes", new { hashtag = viewModel.Hashtag, name = item.Name });
        }

        public async Task<IActionResult> Delete(string hashtag, string name)
        {
            var recipeGuid = await this.GetRecipeGuid(name);
            
            var repository = new RecipeRepository(_settings.AzureStorageConnectionString);
            var item = await repository.GetAsync(recipeGuid);
            await repository.DeleteAsync(item);

            var viewModel = new HashtagLineViewModel(hashtag);
            return Redirect(UrlBuilder.GetUrl(hashtag));
        }


        private async Task<Guid> GetRecipeGuid(string name)
        {
            var siteRepository = new SiteRepository(_settings.AzureStorageConnectionString);
            var site = await siteRepository.GetAsync();

            var item = site.Recipes.Where(x => x.Name == name).FirstOrDefault();
            return item.Guid;
        }
    }
}