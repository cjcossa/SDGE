using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private int id = 1;
        public ComissaoOrganizadoraController(IComissaoOrganizadoraRepository comissaoOrganizadoraRepository, 
            IMembroRepository membroRepository, 
            IMembroOrganizadorRepository membroOrganizadorRepository)
        {
            _comissaoOrganizadoraRepository = comissaoOrganizadoraRepository;
            _membroRepository = membroRepository;
            _membroOrganizadorRepository = membroOrganizadorRepository;
            //id = 2;
        }
        // GET: ComissaoOrganizadora
        public ActionResult Index(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_comissaoOrganizadoraRepository.ObterPorMembro(id));
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
                        Codigo = comissao.Codigo
                    };
                    var result = _comissaoOrganizadoraRepository.Adicionar(comissaoOrganizadora);
                    if (result != null)
                    {
                        MembroOrganizador membro = new MembroOrganizador
                        {
                            MembroId = id,
                            ComissaoOrganizadoraId = result.ComissaoOrganizadoraId
                        };
                        _membroOrganizadorRepository.Adicionar(membro);
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
                        MembroId = id,
                        ComissaoOrganizadoraId = _comissaoOrganizadoraRepository.ObterPorCodigo(comissao.Codigo).ComissaoOrganizadoraId
                    };

                    if (_membroOrganizadorRepository.VerificarMembro(id, membro.ComissaoOrganizadoraId, true))
                    {
                       var result = _membroOrganizadorRepository.ObterPorMembroComissao(membro.MembroId, membro.ComissaoOrganizadoraId, true);
                        _membroOrganizadorRepository.Actualizar(result);
                    }
                    else if(!_membroOrganizadorRepository.VerificarMembro(id, membro.ComissaoOrganizadoraId, false))
                    {
                        _membroOrganizadorRepository.Adicionar(membro);
                    }
                    return RedirectToAction("Index", new { msg = "Membro adicionado." });
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
        public ActionResult Adicionar(MembroOrganizadorViewModel organizadorViewModel)
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
                            MembroId = selectedId
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
                        }
                        return RedirectToAction("Index", new { msg = "Membro(s) adicionado(s)" });
                    }
                    /*_membroOrganizadorRepository.Adicionar(organizador);*/
                    return RedirectToAction(nameof(Index));
                }
                return View(organizadorViewModel);
            }
            catch
            {
                return View(organizadorViewModel);
            }
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
            try
            {
                // TODO: Add delete logic here
                // organizadora.Removido = true;
                var result = _membroOrganizadorRepository.ObterPorId(organizador.MembroOrganizadorId);
                _membroOrganizadorRepository.Actualizar(result);
                return RedirectToAction("Listar", new { id = result.ComissaoOrganizadoraId, msg = "Membro removido"});
            }
            catch
            {
                return View(organizador);
            }
        }

        private List<SelectListItem> ObterMembros(int id)
        {
            List<SelectListItem> items  = new List<SelectListItem>();
            var result = _membroRepository.ObterTodos();
            //string codigo = _comissaoOrganizadoraRepository.ObterPorId(id).Codigo;

            foreach(var item in result)
            {
                if(!_membroOrganizadorRepository.VerificarMembro(item.MembroId, id, false))
                {
                    items.Add(new SelectListItem() { Value = item.MembroId.ToString(), Text = item.Email });
                }
            }
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
           if(_membroOrganizadorRepository.VerificarMembro(id, comissaoId, false))
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
    }
}