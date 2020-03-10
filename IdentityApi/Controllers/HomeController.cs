using IdentityApi.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace IdentityApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }
        public IActionResult Index(string returnUrl)
        {
            return View();
        }


        //public IActionResult ReturnToOriginalApplication(string returnUrl)
        //{
        //    if (returnUrl != null)
        //        return Redirect(_redirectSvc.ExtractRedirectUriFromReturnUrl(returnUrl));
        //    else
        //        return RedirectToAction("Index", "Home");
        //}

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}