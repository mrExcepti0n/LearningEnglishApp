using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Models;
using IdentityApi.Models.AccountViewModels;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authorization;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Controllers
{
    public class AccountController : Controller
    {

        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IClientStore _clientStore;
        private readonly UserManager<ApplicationUser> _userManager;


        public AccountController(IIdentityServerInteractionService interactionService, IClientStore clientStore, UserManager<ApplicationUser> userManager)
        {
            _interactionService = interactionService;
            _clientStore = clientStore;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                throw new NotImplementedException("External login is not implemented!");
            }

            var vm = BuildLoginViewModelAsync(returnUrl, context);

            ViewData["ReturnUrl"] = returnUrl;

            return View(vm);
        }


        private LoginViewModel BuildLoginViewModelAsync(string returnUrl, AuthorizationRequest context)
        {          
            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Email = context?.LoginHint,
            };
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    LastName = model.User.LastName,
                    Name = model.User.Name
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Errors.Count() > 0)
                {
                    throw new Exception(result.Errors.ToString());
                
                }
            }

            if (returnUrl != null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                    return Redirect(returnUrl);
                else
                    if (ModelState.IsValid)
                    return RedirectToAction("login", "account", new { returnUrl = returnUrl });
                else
                    return View(model);
            }

            return RedirectToAction("index", "home");
        }
    }
}
