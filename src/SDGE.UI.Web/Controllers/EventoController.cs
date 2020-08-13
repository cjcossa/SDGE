using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class EventoController : Controller
    {

        private readonly IEventoRepository _eventoRepository;
        private readonly IComissaoCientificaRepository _comissaoCientificaRepository;
        private readonly IComissaoOrganizadoraRepository _comissaoOrganizadoraRepository;
        private readonly IMembroOrganizadorRepository _membroOrganizadorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public EventoController(IEventoRepository eventoRepository,
            IComissaoOrganizadoraRepository comissaoOrganizadoraRepository,
            IComissaoCientificaRepository comissaoCientificaRepository,
            IMembroOrganizadorRepository membroOrganizadorRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _eventoRepository = eventoRepository;
            _comissaoCientificaRepository = comissaoCientificaRepository;
            _comissaoOrganizadoraRepository = comissaoOrganizadoraRepository;
            _membroOrganizadorRepository = membroOrganizadorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Evento
        
        public ActionResult Index(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_eventoRepository.ObterEventos(SessionId()));
        }
        public ActionResult Eventos()
        {
            return View(_eventoRepository.ObterTodos());
        }
        public ActionResult Listar(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_eventoRepository.ObterEventos(SessionId()));
        }
        public ActionResult ListarSub(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_eventoRepository.ObterEventos(SessionId()));
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            Evento result = _eventoRepository.ObterPorId(id);
            return View(SetEventoViewModel(result));
        }

        // GET: Evento/Create
        //[Authorize(Roles = "Membro")]
        public ActionResult Create()
        {
            ViewBag.Categoria = Categoria();
            return View(new EventoViewModel());
        }

       
        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                ValidarEvento(collection);
                if (ModelState.IsValid)
                {
                    
                     _eventoRepository.Adicionar(SetEvento(collection));
                        // return RedirectToAction(nameof(Index));
                     return RedirectToAction("Index", new { msg = "Evento criado." });
                    
                }
                
                ViewBag.Categoria = Categoria();
                return View(collection);
                
            }
            catch
            {
                ViewBag.Categoria = Categoria();
                return View(collection);
            }
        }

        // GET: Evento/Edit/5
       // [Authorize(Roles = "Membro")]
        public ActionResult Edit(int id)
        {
            ViewBag.Categoria = Categoria();
            Evento result = _eventoRepository.ObterPorId(id);
            return View(SetEventoViewModel(result));
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventoViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                //ValidarEvento(collection);
                if(ModelState.IsValid)
                {
                    _eventoRepository.Actualizar(SetEvento(collection));
                    return RedirectToAction("Index", new { msg = "Evento actualizado." });
                }
                ViewBag.Categoria = Categoria();
                return View(collection);
            }
            catch
            {
                ViewBag.Categoria = Categoria();
                return View(collection);
            }
        }

        // GET: Evento/Delete/5
        //[Authorize(Roles = "Membro")]
        public ActionResult Delete(int id)
        {
            return PartialView("_Remover", _eventoRepository.ObterPorId(id));
        }

        // POST: Evento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Evento collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = _eventoRepository.ObterPorId(collection.EventoId);
                _eventoRepository.Remover(result);
                return RedirectToAction("Index", new { msg = "Evento removido." });
            }
            catch
            {
                return PartialView("_Remover", collection);
            }
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarEmail(string email)
        {
            if (_eventoRepository.VerificarEmail(email))
            {
                return Json($"O email {email} existe.");
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarTitulo(string titulo)
        {

            if (_eventoRepository.VerificarTitulo(titulo))
            {
                return Json($"O titulo {titulo} existe.");
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarCodigoOrganizadora(string codigoorganizadora)
        {

            if (!_comissaoOrganizadoraRepository.VerificarCodigo(codigoorganizadora))
            {
                return Json($"O código {codigoorganizadora} não existe.");
            }
            int comissaoId = _comissaoOrganizadoraRepository.ObterPorCodigo(codigoorganizadora).ComissaoOrganizadoraId;
            if (!_membroOrganizadorRepository.VerificarMembro(SessionId(), comissaoId, false))
            {
                return Json($"O membro não existe neste código {codigoorganizadora} da comissão organizadora.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarCodigoCientifica(string codigocientifica)
        {

            if (!_comissaoCientificaRepository.VerificarCodigo(codigocientifica))
            {
                return Json($"O código {codigocientifica} não existe.");
            }
            
            return Json(true);
        }

        private void ValidarEvento(EventoViewModel collection)
        {
            //Evento result = null;
            if (_eventoRepository.VerificarEmail(collection.Email))
            {
                ModelState.AddModelError("Email", $"O email {collection.Email} existe.");
            }
            if (_eventoRepository.VerificarTitulo(collection.Titulo))
            {
                ModelState.AddModelError("Titulo", $"O Titulo {collection.Titulo} existe.");
            }
            if (!_comissaoOrganizadoraRepository.VerificarCodigo(collection.CodigoOrganizadora))
            {
                ModelState.AddModelError("CodigoOrganizadora", $"O código {collection.CodigoOrganizadora} não existe.");
            }
            int comissaoId = _comissaoOrganizadoraRepository.ObterPorCodigo(collection.CodigoOrganizadora).ComissaoOrganizadoraId;
            if (!_membroOrganizadorRepository.VerificarMembro(SessionId(), comissaoId, false))
            {
                ModelState.AddModelError("CodigoOrganizadora", $"O membro não existe neste código {collection.CodigoOrganizadora} da comissão organizadora.");
            }
            if (!_comissaoCientificaRepository.VerificarCodigo(collection.CodigoCientifica))
            {
                ModelState.AddModelError("CodigoCientifica", $"O código {collection.CodigoCientifica} não existe.");
            }
        }
        private Evento SetEvento(EventoViewModel collection)
        {
            Evento evento = new Evento
            {
                EventoId = collection.EventoId,
                Titulo = collection.Titulo,
                Lema = collection.Lema,
                Email = collection.Email,
                DataInicio = collection.DataInicio,
                DataFim = collection.DataFim,
                Local = collection.Local,
                Descricao = collection.Descricao,
                Categoria = collection.Categoria,
                ComissaoCientificaId = _comissaoCientificaRepository.ObterPorCodigo(collection.CodigoCientifica).ComissaoCientificaId,
                ComissaoOrganizadoraId = _comissaoOrganizadoraRepository.ObterPorCodigo(collection.CodigoOrganizadora).ComissaoOrganizadoraId

            };

            return evento;
        }
        private EventoViewModel SetEventoViewModel(Evento collection)
        {
            EventoViewModel evento = new EventoViewModel
            {
                EventoId = collection.EventoId,
                Titulo = collection.Titulo,
                Lema = collection.Lema,
                Email = collection.Email,
                DataInicio = collection.DataInicio,
                DataFim = collection.DataFim,
                Local = collection.Local,
                Descricao = collection.Descricao,
                Categoria = collection.Categoria,
                CodigoCientifica = _comissaoCientificaRepository.ObterPorId(collection.ComissaoCientificaId).Codigo,
                CodigoOrganizadora = _comissaoOrganizadoraRepository.ObterPorId(collection.ComissaoOrganizadoraId).Codigo

            };

            return evento;
        }
        private List<SelectListItem> Categoria()
        {
            return new List<SelectListItem>(){
                new SelectListItem(){Value="",Text="Seleccione a Categoria"},
                new SelectListItem(){Value="Conferência",Text="Conferência"},
                new SelectListItem(){Value="Seminário",Text="Seminário"},
                new SelectListItem(){Value="Workshop",Text="Workshop"}
            };
        }
        private int SessionId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Membro"));
        }
    }
}