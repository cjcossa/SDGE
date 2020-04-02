using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;

namespace SDGE.UI.Web.Controllers
{
    public class SubmissaoController : Controller
    {
        private readonly ISubmissaoRepository _submissaoRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly ITipoRepository _tipoRepository;

      
        public SubmissaoController(ISubmissaoRepository submissaoRepository, IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, ITipoRepository tipoRepository)
        {
            _submissaoRepository = submissaoRepository;
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _tipoRepository = tipoRepository;
        }
        // GET: Submissao
        public ActionResult Index()
        {
            return View(_submissaoRepository.ObterTodos());
        }

        // GET: Submissao/Details/5
        public ActionResult Details(int id)
        {
            return View(_submissaoRepository.ObterPorId(id));
        }

        // GET: Submissao/Create
        public ActionResult Create()
        {
            ViewBag.ParticipanteId = ObterParticipantes();
            ViewBag.EventoId = ObterEventos();
            ViewBag.TipoId = ObterTipos();
        
            return View(new Submissao());
        }
        private SelectList ObterParticipantes(string id = null)
        {
            return new SelectList(_participanteRepository.ObterTodos(), "ParticipanteId", "Nome", id);
        }
        private SelectList ObterEventos(string id = null)
        {
            return new SelectList(_eventoRepository.ObterTodos(), "EventoId", "Titulo", id);
        }
        private SelectList ObterTipos(string id = null)
        {
            return new SelectList(_tipoRepository.ObterTodos(), "TipoId", "Titulo", id);
        }


        // POST: Submissao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Submissao collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _submissaoRepository.Adicionar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ParticipanteId = ObterParticipantes(collection.ParticipanteId.ToString());
                ViewBag.EventoId = ObterEventos(collection.EventoId.ToString());
                ViewBag.TipoId = ObterTipos(collection.TipoId.ToString());
                return View(collection);
            }
            catch
            {
                return View();
            }
            
        }

        // GET: Submissao/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ParticipanteId = ObterParticipantes();
            ViewBag.EventoId = ObterEventos();
            ViewBag.TipoId = ObterTipos();
            return View(_submissaoRepository.ObterPorId(id));
        }

        // POST: Submissao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Submissao collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _submissaoRepository.Actualizar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ParticipanteId = ObterParticipantes(collection.ParticipanteId.ToString());
                ViewBag.EventoId = ObterEventos(collection.EventoId.ToString());
                ViewBag.TipoId = ObterTipos(collection.TipoId.ToString());
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: Submissao/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_submissaoRepository.ObterPorId(id));
        }

        // POST: Submissao/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Submissao collection)
        {
            try
            {
                // TODO: Add delete logic here
                _submissaoRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
    }
}