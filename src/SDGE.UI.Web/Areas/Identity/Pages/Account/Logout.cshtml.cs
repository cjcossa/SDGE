using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SDGE.UI.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                
                if (HttpContext.Session.GetString("_Membro") != null)
                {
                    HttpContext.Session.Remove("_Membro");
                    HttpContext.Session.Remove("_Organizador");
                    HttpContext.Session.Remove("_Cientifico");
                    HttpContext.Session.Remove("_UserEmail");
                }
                if (HttpContext.Session.GetString("_Participante") != null)
                {
                    HttpContext.Session.Remove("_Participante");
                    HttpContext.Session.Remove("_UserEmail");
                }
                if (HttpContext.Session.GetString("_Admin") != null)
                {
                    HttpContext.Session.Remove("_Admin");
                    HttpContext.Session.Remove("_UserEmail");
                }
                if (HttpContext.Session.GetString("_Director") != null)
                {
                    HttpContext.Session.Remove("_Director");
                    HttpContext.Session.Remove("_UserEmail");
                }

                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
