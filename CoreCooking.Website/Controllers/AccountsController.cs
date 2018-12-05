using CoreCooking.Models;
using CoreCooking.Models.Users;
using CoreCooking.Website.Models;
using CoreCooking.Website.ViewModels.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreCooking.Website.Controllers
{
    public class AccountsController : Controller
    {
        private Settings _settings;

        public AccountsController(IOptions<Settings> settings)
        {
            this._settings = settings.Value;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            LoginViewModel viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymousAttribute]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            UserRepository repository = new UserRepository(_settings.AzureStorageConnectionString);

            LoginResult login = await repository.Login(viewModel.Name, viewModel.Password);

            if (login.Success)
            {
                var claims = new List<Claim> {
                    new Claim("sub", "19828281888"),
                    new Claim("given_name", "Dominick"),
                    new Claim("role", "Geek")
                };

                var id = new ClaimsIdentity(claims, "password");
                var principal = new ClaimsPrincipal(id);

                await HttpContext.SignInAsync("MyCookieAuthenticationScheme", principal);

                if (viewModel.ReturnUrl != null)
                    return Redirect(viewModel.ReturnUrl);

                return Redirect("/");
            }

            viewModel.Password = "";
            ModelState.AddModelError("", login.Message);
            return View(viewModel);
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");

            if (returnUrl != null)
                return Redirect(returnUrl);

            return Redirect("/");
        }
    }
}