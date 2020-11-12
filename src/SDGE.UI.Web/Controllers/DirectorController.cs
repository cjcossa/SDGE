using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IFaculdadeRepository _faculdadeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public DirectorController(IDirectorRepository DirectorRepository, 
            IFaculdadeRepository faculdadeRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender
            )
        {
            _directorRepository = DirectorRepository;
            _faculdadeRepository = faculdadeRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        // GET: Director
        public ActionResult Index(string msg = null, string type = null)
        {
            ViewBag.Alert = msg;
            ViewBag.Type = type;
            return View(_directorRepository.ObterTodos());
        }

        // GET: Director/Details/5
        public ActionResult Details(int id)
        {
            return View(_directorRepository.ObterPorId(id));
        }

        // GET: Director/Create
        public ActionResult Create()
        {
            ViewBag.FaculdadeId = ObterFaculdades();
            return View(new DirectorViewModel());
        }

        // POST: Director/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectorViewModel collection)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var director = new Director
                    {
                        DirectorId = collection.DirectorId,
                        Nome = collection.Nome,
                        Email = collection.Email,
                        Telefone = collection.Telefone,
                        FaculdadeId = collection.FaculdadeId
                    };
                    if(await _userManager.FindByEmailAsync(collection.Email) != null)
                    {
                        ModelState.AddModelError(string.Empty, $"O { collection.Email} já existe. Por favor tente com outro email.");
                    }
                    else
                    {
                        var result = _directorRepository.Adicionar(director);

                        if (result != null)
                        {
                            if (await EnviarConfirmacao(result))
                            {
                                return RedirectToAction("Index", new { msg = "Director registado." , type = "success"});
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Erro ao enviar o email de confirmação de conta.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Erro ao registar director.");
                        }
                    }
                   
                        
                }

                ViewBag.FaculdadeId = ObterFaculdades();
                return View(collection);
            }
            catch
            {
                ViewBag.FaculdadeId = ObterFaculdades();
                return View(collection);
            }
        }

        // GET: Director/Edit/5
        public ActionResult Edit(int id)
        {
            Director collection = _directorRepository.ObterPorId(id);
            var director = new DirectorViewModel
            {
                DirectorId = collection.DirectorId,
                Nome = collection.Nome,
                Email = collection.Email,
                Telefone = collection.Telefone,
                FaculdadeId = collection.FaculdadeId
            };
            ViewBag.FaculdadeId = ObterFaculdades(id.ToString());
            return View(director);
        }

        // POST: Director/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DirectorViewModel collection)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var director = new Director
                    {
                        DirectorId = collection.DirectorId,
                        Nome = collection.Nome,
                        Email = collection.Email,
                        Telefone = collection.Telefone,
                        FaculdadeId = collection.FaculdadeId
                    };
                     _directorRepository.Actualizar(director);
                    var message = new Message(new string[] { director.Email }, "Alterar director", $"Oi {director.Nome}, os seus dados foram alterados.", null);
                    if (Notificar(message))
                        return RedirectToAction("Index", new { msg = "Director alterado.", type = "success" });
                    else
                        ModelState.AddModelError(string.Empty, "Erro ao enviar notificação.");
                }
                ViewBag.FaculdadeId = ObterFaculdades(collection.FaculdadeId.ToString());
                return View(collection);
            }
            catch
            {
                ViewBag.FaculdadeId = ObterFaculdades(collection.FaculdadeId.ToString());
                return View(collection);
            }
        }

        // GET: Director/Delete/5
        public ActionResult Delete(int id)
        {
            return PartialView("_Remover", _directorRepository.ObterPorId(id));
        }

        // POST: Director/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Director collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = _directorRepository.ObterPorId(collection.DirectorId);
                _directorRepository.Remover(result);
                var message = new Message(new string[] { result.Email }, "Remover director", $"Oi {result.Nome}, os seus dados foram removidos.", null);
                if (Notificar(message))
                  return RedirectToAction("Index", new { msg = "Director removido.", type = "danger" });
                else
                    return PartialView("_Remover", collection);
            }
            catch
            {
                return PartialView("_Remover", collection);
            }
        }

        public List<SelectListItem> ObterFaculdades(string id = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var result = _faculdadeRepository.ObterTodos();
            bool state = true;

            foreach (var item in result)
            {
                if (!_directorRepository.VerificarFaculdade(item.FaculdadeId))
                {
                    items.Add(new SelectListItem() { Value = item.FaculdadeId.ToString(), Text = item.Designacao });
                    state = false;
                }
            }

            if (id != null)
            {
                var data = _directorRepository.ObterPorId(int.Parse(id));

                items.Add(new SelectListItem() { Value = data.FaculdadeId.ToString(), Text = data.Faculdade.Designacao });
               
            }

            if (state && id == null)
                items.Add(new SelectListItem() { Value = "", Text = "Não existe nenhuma faculdade" });
            return items;
        }

        private async Task<bool> EnviarConfirmacao(Director model)
        {
            string password = "1";
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                IdentityUser identityUser = await _userManager.FindByEmailAsync(model.Email);
                if (identityUser != null)
                {
                    IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Director");
                    if (identityResult.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        var message = new Message(new string[] { model.Email },
                            "Confirme seu email", $"Por favor {model.Nome} , confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicando aqui</a>. Foi convidado para ser director científico da { _faculdadeRepository.ObterPorId(model.FaculdadeId).Designacao}", null);
                        await _emailSender.SendEmailAsync(message);

                        return true;
                    }

                }
            }
            return false;
        }

        private bool Notificar(Message message)
        {
            if(message.To != null)
            {
                _emailSender.SendEmailAsync(message);
                return true;
            }
           
            return false;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarEmail(string email)
        {
            if (_directorRepository.VerificarEmail(email))
            {
                return Json($"O email {email} existe.");
            }
            return Json(true);
        }
    }
}