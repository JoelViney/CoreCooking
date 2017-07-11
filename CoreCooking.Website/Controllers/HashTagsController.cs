using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreCooking.Models;
using CoreCooking.Models.Recipes;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;
using CoreCooking.Website.Helpers;
using CoreCooking.Models.Sites;
using CoreCooking.Website.ViewModels.Hashtags;

namespace CoreCooking.Website.Controllers
{
    public class HashtagsController : Controller
    {
        private Settings _settings;
        
        #region Constructors...

        public HashtagsController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }

        #endregion
        
        public async Task<IActionResult> Details(string name, bool? viewIcons = null)
        {
            var repository = new SiteRepository(_settings.AzureStorageConnectionString);
            var site = await repository.GetAsync();

            var recipes = site.GetRecipesByHashtag(name);

            var viewModel = new HashtagDetailsViewModel(name, recipes);

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
    }
}