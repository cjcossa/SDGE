using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using SDGE.ApplicationCore.Interfaces.Repository;

namespace SDGE.UI.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IMembroRepository _membroRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IMembroRepository membroRepository,
            IParticipanteRepository participanteRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _membroRepository = membroRepository;
            _participanteRepository = participanteRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email actual é obrigatório")]
            [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha actual é obrigatório")]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [Display(Name = "Esqueci a senha")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User logged in.");
                    //return LocalRedirect(returnUrl);
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(Input.Email);
                   
                    if (!await _userManager.IsInRoleAsync(identityUser, "Admin") &&
                        !await _userManager.IsInRoleAsync(identityUser, "Membro") && 
                        !await _userManager.IsInRoleAsync(identityUser, "Participante") &&
                        !await _userManager.IsInRoleAsync(identityUser, "Director"))
                    {
                        return RedirectToAction("Index", "Register", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {

                        HttpContext.Session.SetString("_UserEmail", identityUser.Email);
                        if (await _userManager.IsInRoleAsync(identityUser, "Membro"))
                        {
                            var id = _membroRepository.ObterPorEmail(identityUser.Email).MembroId;
                            HttpContext.Session.SetString("_Membro", id.ToString());
                            if(await _userManager.IsInRoleAsync(identityUser, "Organizador"))
                            {
                                HttpContext.Session.SetString("_Organizador", id.ToString());
                            } 
                            if(await _userManager.IsInRoleAsync(identityUser, "Cientifico"))
                            {
                                HttpContext.Session.SetString("_Cientifico", id.ToString());
                            }

                        }else if(await _userManager.IsInRoleAsync(identityUser, "Participante"))
                        {
                            var id = _participanteRepository.ObterPorEmail(identityUser.Email).ParticipanteId;
                            HttpContext.Session.SetString("_Participante", id.ToString());
                            return RedirectToAction("PIndex", "Home");
                        }
                        else if(await _userManager.IsInRoleAsync(identityUser, "Admin"))
                        {
                            HttpContext.Session.SetString("_Admin", "Admin");
                        }
                        else if (await _userManager.IsInRoleAsync(identityUser, "Director"))
                        {
                            HttpContext.Session.SetString("_Director", "Director");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Conta de usuário bloqueada.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
