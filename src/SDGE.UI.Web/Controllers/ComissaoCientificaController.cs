using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ComissaoCientificaController : Controller
    {
        private readonly IComissaoCientificaRepository _comissaoCientificaRepository;
        private readonly IMembroRepository _membroRepository;
        private readonly IMembroCientificoRepository _membroCientificoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public ComissaoCientificaController(IComissaoCientificaRepository comissaoCientificaRepository, 
            IMembroRepository membroRepository,
            IMembroCientificoRepository membroCientificoRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            _comissaoCientificaRepository = comissaoCientificaRepository;
            _membroRepository = membroRepository;
            _membroCientificoRepository = membroCientificoRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        // GET: ComissaoOrganizadora
        public ActionResult Index(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_comissaoCientificaRepository.ObterPorMembro(SessionId()));

        }


        // GET: ComissaoOrganizadora/Create
       // [Authorize(Roles = "Organizador")]
        public ActionResult Criar()
        {
            ComissaoCientificaViewModel comissao = new ComissaoCientificaViewModel();
            comissao.Codigo = _comissaoCientificaRepository.GenerateCode();
            return View(comissao);
        }

       
        // POST: ComissaoOrganizadora/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar(ComissaoCientificaViewModel comissao)
        {
            try
            {
                // TODO: Add insert logic here
                //int id = 2;
                if(ModelState.IsValid)
                {
                    ComissaoCientifica comissaoCientifica = new ComissaoCientifica
                    {
                        Codigo = comissao.Codigo,
                        CriadoPorId = SessionId()
                    };
                    var result = _comissaoCientificaRepository.Adicionar(comissaoCientifica);
                    if (result != null)
                    {
                        MembroCientifico membro = new MembroCientifico
                        {
                            MembroId = SessionId(),
                            ComissaoCientificaId = result.ComissaoCientificaId,
                            Confirmado = true
                        };
                        _membroCientificoRepository.Adicionar(membro);
                    }

                    return RedirectToAction("Index", new { msg = "Comissão criada."});
                }
                return View(comissao);

            }
            catch
            {
                return View(comissao);
            }
        }

        //[Authorize(Roles = "Organizador")]
        public ActionResult Adicionar(int id)
        {
            ViewBag.MembroId = ObterMembros(id);
            MembroCientificoViewModel membro = new MembroCientificoViewModel
            {
                 ComissaoCientificaId = id
            };
            return View(membro);
        }
        public ActionResult Listar(int id, string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_membroCientificoRepository.ObterPorComissao(id, true));
        }
        public ActionResult Confirmar(int id, string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_membroCientificoRepository.ObterPorComissao(id));
        }
        public ActionResult Participar()
        {
            return View(new ComissaoCientificaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participar(ComissaoCientificaViewModel comissao)
        {
           
            try
            {
                // TODO: Add insert logic here
               
                if (ModelState.IsValid)
                {
                    MembroCientifico membro = new MembroCientifico
                    {
                        MembroId = SessionId(),
                        ComissaoCientificaId = _comissaoCientificaRepository.ObterPorCodigo(comissao.Codigo).ComissaoCientificaId
                    };

                    if (_membroCientificoRepository.VerificarMembro(SessionId(), membro.ComissaoCientificaId, true))
                    {
                       var result = _membroCientificoRepository.ObterPorMembroComissao(membro.MembroId, membro.ComissaoCientificaId, true);
                        _membroCientificoRepository.Actualizar(result);
                    }
                    else if(!_membroCientificoRepository.VerificarMembro(SessionId(), membro.ComissaoCientificaId, false))
                    {
                        _membroCientificoRepository.Adicionar(membro);
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
        public async Task<ActionResult> Adicionar(MembroCientificoViewModel cientificoViewModel)
        {
            //return PartialView("_addMember", organizador);
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    foreach (var selectedId in cientificoViewModel.Membros)
                    {
                        MembroCientifico membro = new MembroCientifico
                        {
                            ComissaoCientificaId = cientificoViewModel.ComissaoCientificaId,
                            MembroId = selectedId,
                            Confirmado = true
                        };
                        //_membroOrganizadorRepository.Adicionar(organizador);
                        if (_membroCientificoRepository.VerificarMembro(membro.MembroId, membro.ComissaoCientificaId, true))
                        {
                            var result = _membroCientificoRepository.ObterPorMembroComissao(membro.MembroId, membro.ComissaoCientificaId, true);
                            _membroCientificoRepository.Actualizar(result);
                        }
                        else if (!_membroCientificoRepository.VerificarMembro(membro.MembroId, membro.ComissaoCientificaId, false))
                        {
                            _membroCientificoRepository.Adicionar(membro);
                            IdentityUser identityUser = await _userManager.FindByEmailAsync(_membroRepository.ObterPorId(membro.MembroId).Email);
                            if (identityUser != null)
                            {
                                IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Cientifico");
                            }
                        }
                    }
                    /*_membroOrganizadorRepository.Adicionar(organizador);*/
                    return RedirectToAction("Index", new { msg = "Membro(s) adicionado(s)" });
                }
                return View(cientificoViewModel);
            }
            catch
            {
                return View(cientificoViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirmar(string[] confirmar, int Id)
        {
            int cientificoId = Id;
            string msg = "Seleccione pelo menos uma linha.";
            if (confirmar != null)
            {
                foreach (var epId in confirmar)
                {
                    int id = int.Parse(epId);
                    var result = _membroCientificoRepository.ObterPorId(id);
                    cientificoId = result.ComissaoCientificaId;
                    result.Confirmado = true;
                    msg = "Pedido(s) confirmado(s).";
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(_membroRepository.ObterPorId(result.MembroId).Email);
                    if (identityUser != null)
                    {
                        IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Cientifico");
                    }
                    _membroCientificoRepository.Confirmar(result);
                }
                return RedirectToAction("Confirmar", new { id = cientificoId, msg = msg });
            }

            return RedirectToAction("Confirmar", new { id = cientificoId, msg = msg });
        }

        // GET: ComissaoOrganizadora/Delete/5
        public ActionResult Remover(int id)
        {
            var result = _comissaoCientificaRepository.ObterPorId(id);
            ComissaoCientificaViewModel comissao = new ComissaoCientificaViewModel
            {
               Codigo = result.Codigo,
               ComissaoCientificaId = result.ComissaoCientificaId
            };
            return PartialView("_Remover", result);
            //return View(_comissaoOrganizadoraRepository.ObterPorId(id));
        }

        public ActionResult RemoverMembro(int id)
        {
            var result = _membroCientificoRepository.ObterPorId(id);
            return PartialView("_RemoverMembro", _membroCientificoRepository.ObterPorMembroComissao(result.MembroId, result.ComissaoCientificaId, false));

        }

        // POST: ComissaoOrganizadora/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remover(int id, ComissaoCientifica cientifica)
        {
            var result = new ComissaoCientifica();
            try
            {
                // TODO: Add delete logic here
                // organizadora.Removido = true;
               result = _comissaoCientificaRepository.ObterPorId(cientifica.ComissaoCientificaId);
                _comissaoCientificaRepository.Actualizar(result);
                return RedirectToAction("Index", new { msg = "Comissão organizadora removida." });
            }
            catch
            {
                return PartialView("_Remover", result);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoverMembro(int id, MembroCientifico cientifico)
        {
            var result = new MembroCientifico();
            try
            {
                // TODO: Add delete logic here
                // organizadora.Removido = true;
                result = _membroCientificoRepository.ObterPorId(cientifico.MembroCientificoId);
                _membroCientificoRepository.Actualizar(result);
                return RedirectToAction("Listar", new { id = result.ComissaoCientificaId, msg = "Membro removido"});
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

            foreach (var item in result)
            {
                if(!_membroCientificoRepository.VerificarMembro(item.MembroId, id, false))
                {
                    state = true;
                    items.Add(new SelectListItem() { Value = item.MembroId.ToString(), Text = item.Email });
                }
            }
            if (!state)
                items.Add(new SelectListItem() { Value = "", Text = "Não existe nenhum membro" });

            return items;
        }
       
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyCode(string codigo)
        {
           
            if(!_comissaoCientificaRepository.VerificarCodigo(codigo))
            {
                return Json($"O código {codigo} não existe."); 
            }
            int comissaoId = _comissaoCientificaRepository.ObterPorCodigo(codigo).ComissaoCientificaId;
           if(_membroCientificoRepository.VerificarMembro(SessionId(), comissaoId, false))
           {
                return Json($"O membro existe neste código {codigo}.");
           }
           
            return Json(true);
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarMembro(int membroId, string codigo)
        {
            int comissaoId = _comissaoCientificaRepository.ObterPorCodigo(codigo).ComissaoCientificaId;
            if (_membroCientificoRepository.VerificarMembro(membroId, comissaoId, false))
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
    }
}