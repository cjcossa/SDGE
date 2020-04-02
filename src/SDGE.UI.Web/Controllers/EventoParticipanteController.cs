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
    public class EventoParticipanteController : Controller
    {

        private readonly IEventoParticipanteRepository _eventoParticipanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public EventoParticipanteController(IEventoParticipanteRepository eventoParticipanteRepository, IEventoRepository eventoRepository, IParticipanteRepository participanteRepository)
        {
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _eventoRepository = eventoRepository;
            _participanteRepository = participanteRepository;
        }
        // GET: MembroEvento
        public ActionResult Index()
        {
            return View(_eventoParticipanteRepository.ObterTodos());
        }

        // GET: MembroEvento/Details/5
        public ActionResult Details(int id)
        {
            return View(_eventoParticipanteRepository.ObterJoinPorId(id));
        }

        // GET: MembroEvento/Create
        public ActionResult Create()
        {
            ViewBag.EventoId = ObterEventos();
            ViewBag.ParticipanteId = ObterParticipantes();
            return View(new EventoParticipante());
        }
        
        public SelectList ObterEventos(string id = null)
        {
            return new SelectList(_eventoRepository.ObterTodos(), "EventoId", "Titulo", id);
        }
        public SelectList ObterParticipantes(string id = null)
        {
            return new SelectList(_participanteRepository.ObterTodos(), "ParticipanteId", "Nome", id);
        }

        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoParticipante collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                if (ModelState.IsValid)
                {
                    _eventoParticipanteRepository.Adicionar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.EventoId = ObterEventos(collection.EventoId.ToString());
                ViewBag.ParticipanteId = ObterParticipantes(collection.ParticipanteId.ToString());
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: MembroEvento/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.EventoId = ObterEventos();
            ViewBag.ParticipanteId = ObterParticipantes();
            return View(_eventoParticipanteRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventoParticipante collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _eventoParticipanteRepository.Actualizar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.EventoId = ObterEventos(collection.EventoId.ToString());
                ViewBag.ParticipanteId = ObterParticipantes(collection.ParticipanteId.ToString());
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        // GET: MembroEvento/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_eventoParticipanteRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventoParticipante collection)
        {
            try
            {
                // TODO: Add delete logic here
                _eventoParticipanteRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}