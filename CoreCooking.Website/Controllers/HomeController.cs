using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;
using CoreCooking.Models.Sites;
using CoreCooking.Website.ViewModels.Home;

namespace CoreCooking.Website.Controllers
{
    public class HomeController : Controller
    {
        private Settings _settings;

        public HomeController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }


        public async Task<IActionResult> Index(bool? viewHashtags = null)
        {
            var repository = new SiteRepository(_settings.AzureStorageConnectionString);
            var site = await repository.GetAsync();
                       
            var viewModel = new HomeViewModel(site);

            return View(viewModel);
        }

    }
}
