using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglishWeb.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenIdConnect")]
    public class AccountController : Controller
    {
        [Authorize(AuthenticationSchemes = "OpenIdConnect")]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var user = User as ClaimsPrincipal;
            var token = await HttpContext.GetTokenAsync("access_token");

            if (token != null)
            {
                ViewData["access_token"] = token;
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = Url.Action(nameof(HomeController.Index), "Home") });
        }

    }
}