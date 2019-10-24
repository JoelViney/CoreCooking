using CoreCooking.Models.Sites;
using CoreCooking.Website.Models;
using CoreCooking.Website.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CoreCooking.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly Settings _settings;

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
