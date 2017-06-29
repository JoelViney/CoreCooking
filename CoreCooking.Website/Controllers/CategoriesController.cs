using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreCooking.Models;
using CoreCooking.Models.Recipes;
using CoreCooking.Models.Categories;
using CoreCooking.Website.ViewModels.Categories;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;
using CoreCooking.Website.Helpers;
using CoreCooking.Models.Sites;

namespace CoreCooking.Website.Controllers
{
    public class CategoriesController : Controller
    {
        private Settings _settings;
        
        #region Constructors...

        public CategoriesController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var repository = new CategoryRepository(_settings.AzureStorageConnectionString);
            var list = await repository.GetListAsync();

            var viewModel = new CategoryListViewModel(list);
            
            return View(viewModel);
        }

        public async Task<IActionResult> Details(string name, bool? viewIcons = null)
        {
            var repository = new CategoryRepository(_settings.AzureStorageConnectionString);
            var item = await repository.SearchAsync(name);

            var viewModel = new CategoryViewModel(item);

            if (viewIcons == null)
            {
                if (this.Request.Cookies.ContainsKey("RecipeViewIcons"))
                    // Load default from cookie.
                    viewIcons = this.Request.Cookies["RecipeViewIcons"] == "true";
                else
                    viewIcons  = false;
            }
            else
            {
                // Save to cookie
                this.Response.Cookies.Append("RecipeViewIcons", "true");
            }

            ViewBag.ViewIcons = viewIcons;
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new CategoryViewModel(new Category());

            return View("Edit", viewModel);
        }

        public async Task<IActionResult> Edit(string name)
        {
            var repository = new CategoryRepository(_settings.AzureStorageConnectionString);
            var item = await repository.SearchAsync(name);

            var viewModel = new CategoryViewModel(item);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel viewModel)
        {
            var repository = new CategoryRepository(_settings.AzureStorageConnectionString);

            Category item;
            if (viewModel.IsNew())
                item = new Category();
            else
                item = await repository.GetAsync(viewModel.Guid);

            viewModel.FillModel(item);

            await repository.SaveAsync(item);
            viewModel.Guid = item.Guid;

            return Redirect(UrlBuilder.GetUrl(viewModel));
        }

        public async Task<IActionResult> Delete(string name)
        {
            var repository = new CategoryRepository(_settings.AzureStorageConnectionString);
            var item = await repository.SearchAsync(name);

            await repository.DeleteAsync(item);

            return Redirect("/");
        }
    }
}