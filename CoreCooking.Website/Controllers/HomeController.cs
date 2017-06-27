using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoreCooking.Models.Categories;
using CoreCooking.Website.ViewModels.Categories;
using CoreCooking.Website.ViewModels.Recipes;
using CoreCooking.Website.Models;
using Microsoft.Extensions.Options;

namespace CoreCooking.Website.Controllers
{
    public class HomeController : Controller
    {
        private Settings _settings;

        public HomeController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }
    }
}
