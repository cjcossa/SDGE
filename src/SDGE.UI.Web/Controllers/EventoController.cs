using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;

namespace SDGE.UI.Web.Controllers
{
    public class EventoController : Controller
    {

        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        // GET: Evento
        public ActionResult Index()
        {
            return View(_eventoRepository.ObterTodos());
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            return View(_eventoRepository.ObterPorId(id));
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View(new Evento());
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento collection)
        {
            try
            {
                // TODO: Add insert logic here
                _eventoRepository.Adicionar(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_eventoRepository.ObterPorId(id));
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Evento collection)
        {
            try
            {
                // TODO: Add update logic here
                _eventoRepository.Actualizar(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_eventoRepository.ObterPorId(id));
        }

        // POST: Evento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Evento collection)
        {
            try
            {
                // TODO: Add delete logic here
                _eventoRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}