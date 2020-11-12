using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class ComissaoOrganizadoraController : Controller
    {
        private readonly IComissaoOrganizadoraRepository _comissaoOrganizadoraRepository;
        private readonly IMembroRepository _membroRepository;
        private readonly IMembroOrganizadorRepository _membroOrganizadorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        public ComissaoOrganizadoraController(IComissaoOrganizadoraRepository comissaoOrganizadoraRepository, 
            IMembroRepository membroRepository, 
            IMembroOrganizadorRepository membroOrganizadorRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _comissaoOrganizadoraRepository = comissaoOrganizadoraRepository;
            _membroRepository = membroRepository;
            _membroOrganizadorRepository = membroOrganizadorRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        // GET: ComissaoOrganizadora
        public ActionResult Index(string msg = null)
        {
            ViewBag.Alert = msg;
           // if(await IsOrganizador())
                return View(_comissaoOrganizadoraRepository.ObterPorMembro(SessionId()));

            //return View();
        }


        // GET: ComissaoOrganizadora/Create
        public ActionResult Criar()
        {
            ComissaoOrganizadoraViewModel comissao = new ComissaoOrganizadoraViewModel();
            comissao.Codigo = _comissaoOrganizadoraRepository.GenerateCode();
            return View(comissao);
        }

       
        // POST: ComissaoOrganizadora/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(ComissaoOrganizadoraViewModel comissao)
        {
            try
            {
                // TODO: Add insert logic here
                //int id = 2;
                if(ModelState.IsValid)
                {
                    ComissaoOrganizadora comissaoOrganizadora = new ComissaoOrganizadora
                    {
                        Codigo = comissao.Codigo,
                        CriadoPorId = SessionId()
                    };
                    var result = _comissaoOrganizadoraRepository.Adicionar(comissaoOrganizadora);
                    if (result != null)
                    {
                        MembroOrganizador membro = new MembroOrganizador
                        {
                            MembroId = SessionId(),
                            ComissaoOrganizadoraId = result.ComissaoOrganizadoraId,
                            Confirmado = true
                        };
                        _membroOrganizadorRepository.Adicionar(membro);
                        
                        return RedirectToAction("Index", new { msg = "Comissão criada." });
                    }
                }
                return View(comissao);

            }
            catch
            {
                return View(comissao);
            }
        }

        public ActionResult Adicionar(int id)
        {
            ViewBag.MembroId = ObterMembros(id);
            MembroOrganizadorViewModel membro = new MembroOrganizadorViewModel
            {
                 ComissaoOrganizadoraId = id
            };
            return View(membro);
        }

        public ActionResult Listar(int id, string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_membroOrganizadorRepository.ObterPorComissao(id, true));
        }
        public ActionResult Confirmar(int id, string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_membroOrganizadorRepository.ObterPorComissao(id));
        }
        public ActionResult Participar()
        {
            return View(new ComissaoOrganizadoraViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participar(ComissaoOrganizadoraViewModel comissao)
        {
           
            try
            {
                // TODO: Add insert logic here
               
                if (ModelState.IsValid)
                {
                    MembroOrganizador membro = new MembroOrganizador
                    {
                        MembroId = SessionId(),
                        ComissaoOrganizadoraId = _comissaoOrganizadoraRepository.ObterPorCodigo(comissao.Codigo).ComissaoOrganizadoraId
                    };

                    if (_membroOrganizadorRepository.VerificarMembro(SessionId(), membro.ComissaoOrganizadoraId, true))
                    {
                       var result = _membroOrganizadorRepository.ObterPorMembroComissao(membro.MembroId, membro.ComissaoOrganizadoraId, true);
                        _membroOrganizadorRepository.Actualizar(result);
                    }
                    else if(!_membroOrganizadorRepository.VerificarMembro(SessionId(), membro.ComissaoOrganizadoraId, false))
                    {
                        _membroOrganizadorRepository.Adicionar(membro);
                    }
                    return RedirectToAction("Index", new { msg = "Pedido de participação enviado, aguarde pela confirmação da comissão organizadora." });
                }
                return View(comissao);
            }
            catch 
            {
               return View(comissao);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar(MembroOrganizadorViewModel organizadorViewModel)
        {
            //return PartialView("_addMember", organizador);
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    foreach (var selectedId in organizadorViewModel.Membros)
                    {
                        MembroOrganizador membro = new MembroOrganizador
                        {
                            ComissaoOrganizadoraId = organizadorViewModel.ComissaoOrganizadoraId,
                            MembroId = selectedId,
                            Confirmado = true
                        };
                        //_membroOrganizadorRepository.Adicionar(organizador);
                        if (_membroOrganizadorRepository.VerificarMembro(membro.MembroId, membro.ComissaoOrganizadoraId, true))
                        {
                            var result = _membroOrganizadorRepository.ObterPorMembroComissao(membro.MembroId, membro.ComissaoOrganizadoraId, true);
                            _membroOrganizadorRepository.Actualizar(result);
                        }
                        else if (!_membroOrganizadorRepository.VerificarMembro(membro.MembroId, membro.ComissaoOrganizadoraId, false))
                        {
                            _membroOrganizadorRepository.Adicionar(membro);
                            IdentityUser identityUser = await _userManager.FindByEmailAsync(_membroRepository.ObterPorId(membro.MembroId).Email);
                            if (identityUser != null)
                            {
                                IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Organizador");
                            }
                        }
                      
                    }
                    return RedirectToAction("Index", new { msg = "Membro(s) adicionado(s)" });
                }
                return View(organizadorViewModel);
            }
            catch
            {
                return View(organizadorViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirmar(string[] confirmar, int Id)
        {
            int organizadorId = Id;
            string msg = "Seleccione pelo menos uma linha.";
            if (confirmar != null)
            {
                foreach (var epId in confirmar)
                {
                    int id = int.Parse(epId);
                    var result = _membroOrganizadorRepository.ObterPorId(id);
                    organizadorId = result.ComissaoOrganizadoraId;
                    result.Confirmado = true;
                    msg = "Pedido(s) confirmado(s).";
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(_membroRepository.ObterPorId(result.MembroId).Email);
                    if (identityUser != null)
                    {
                        IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Organizador");
                    }
                    _membroOrganizadorRepository.Confirmar(result);
                }
                return RedirectToAction("Confirmar", new { id = organizadorId, msg = msg });
            }

            return RedirectToAction("Confirmar", new { id = organizadorId, msg = msg });
        }
        // GET: ComissaoOrganizadora/Delete/5
        public ActionResult Remover(int id)
        {
            var result = _comissaoOrganizadoraRepository.ObterPorId(id);
            ComissaoOrganizadoraViewModel comissao = new ComissaoOrganizadoraViewModel
            {
               Codigo = result.Codigo,
               ComissaoOrganizadoraId = result.ComissaoOrganizadoraId
            };
            return PartialView("_Remover", result);
            //return View(_comissaoOrganizadoraRepository.ObterPorId(id));
        }

        public ActionResult RemoverMembro(int id)
        {
            var result = _membroOrganizadorRepository.ObterPorId(id);
            return PartialView("_RemoverMembro", _membroOrganizadorRepository.ObterPorMembroComissao(result.MembroId, result.ComissaoOrganizadoraId, false));

        }

        // POST: ComissaoOrganizadora/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int id, ComissaoOrganizadora organizadora)
        {
            var result = new ComissaoOrganizadora();
            try
            {
                // TODO: Add delete logic here
                // organizadora.Removido = true;
               result = _comissaoOrganizadoraRepository.ObterPorId(organizadora.ComissaoOrganizadoraId);
                _comissaoOrganizadoraRepository.Actualizar(result);
                return RedirectToAction("Index", new { msg = "Comissão organizadora removida." });
            }
            catch
            {
                return PartialView("_Remover", result);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverMembro(int id, MembroOrganizador organizador)
        {
            var result = new MembroOrganizador();
            try
            {
                // TODO: Add delete logic here
                // organizadora.Removido = true;
                result = _membroOrganizadorRepository.ObterPorId(organizador.MembroOrganizadorId);
                _membroOrganizadorRepository.Actualizar(result);
                return RedirectToAction("Listar", new { id = result.ComissaoOrganizadoraId, msg = "Membro removido"});
            }
            catch
            {
                return PartialView("_RemoverMembro", result);
            }
        }

        private List<SelectListItem> ObterMembros(int id)
        {
            List<SelectListItem> items  = new List<SelectListItem>();
            var result = _membroRepository.ObterTodos();
            bool state = false;
            //string codigo = _comissaoOrganizadoraRepository.ObterPorId(id).Codigo;

            foreach(var item in result)
            {
                if(!_membroOrganizadorRepository.VerificarMembro(item.MembroId, id, false))
                {
                    state = true;
                    items.Add(new SelectListItem() { Value = item.MembroId.ToString(), Text = item.Nome + "-" + item.Email });
                }
            }
            if(!state)
                items.Add(new SelectListItem() { Value = "", Text = "Não existe nenhum membro" });

            return items;

        }
       
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyCode(string codigo)
        {
           
            if(!_comissaoOrganizadoraRepository.VerificarCodigo(codigo))
            {
                return Json($"O código {codigo} não existe."); 
            }
            int comissaoId = _comissaoOrganizadoraRepository.ObterPorCodigo(codigo).ComissaoOrganizadoraId;
           if(_membroOrganizadorRepository.VerificarMembro(SessionId(), comissaoId, false))
           {
                return Json($"O membro existe neste código {codigo}.");
           }
           
            return Json(true);
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarMembro(int membroId, string codigo)
        {
            int comissaoId = _comissaoOrganizadoraRepository.ObterPorCodigo(codigo).ComissaoOrganizadoraId;
            if (_membroOrganizadorRepository.VerificarMembro(membroId, comissaoId, false))
            {
                return Json($"O membro existe neste código {codigo}.");
            }
            return Json(true);
        }
        private int SessionId()
        {
            if(_httpContextAccessor.HttpContext.Session.GetString("_Membro") != null)
                return int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Membro"));

            return -1;
        }
        private async Task<bool> IsOrganizador()
        {
            IdentityUser identityUser = await _userManager.FindByEmailAsync(_membroRepository.ObterPorId(SessionId()).Email);
            if (await _userManager.IsInRoleAsync(identityUser, "Organizador"))
                return true;

            return false;
        }

        private bool Notificar(Message message)
        {
            if (message.To != null)
            {
                _emailSender.SendEmailAsync(message);
                return true;
            }

            return false;
        }
    }
}