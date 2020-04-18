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
    public class MembroEventoController : Controller
    {

        private readonly IMembroEventoRepository _membroEventoRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IMembroRepository _membroRepository;

        public MembroEventoController(IMembroEventoRepository membroEventoRepository, IEventoRepository eventoRepository, IMembroRepository membroRepository)
        {
            _membroEventoRepository = membroEventoRepository;
            _eventoRepository = eventoRepository;
            _membroRepository = membroRepository;
        }
        // GET: MembroEvento
        public ActionResult Index()
        {
            return View(_membroEventoRepository.ObterTodos());
        }

        // GET: MembroEvento/Details/5
        public ActionResult Details(int id)
        {
            return View(_membroEventoRepository.ObterJoinPorId(id));
        }

        // GET: MembroEvento/Create
        public ActionResult Create()
        {
            ViewBag.Comissao = Comissao();
            ViewBag.EventoId = ObterEventos();
            ViewBag.MembroId = ObterMembros();
            return View(new MembroEvento());
        }
        private List<SelectListItem> Comissao()
        {
            return new List<SelectListItem>(){
                new SelectListItem(){Value="Comissão Organizadora",Text="Comissão Organizadora"},
                new SelectListItem(){Value="Comissão Científica",Text="Comissão Científica"}
            };
        }

        public SelectList ObterEventos(string id = null)
        {
            return new SelectList(_eventoRepository.ObterTodos(), "EventoId", "Titulo", id);
        }
        public SelectList ObterMembros(string id = null)
        {
            return new SelectList(_membroRepository.ObterTodos(), "MembroId", "Nome", id);
        }

        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembroEvento collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                if (ModelState.IsValid)
                {
                    _membroEventoRepository.Adicionar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.EventoId = ObterEventos(collection.EventoId.ToString());
                ViewBag.MembroId = ObterMembros(collection.MembroId.ToString());
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
            ViewBag.Comissao = Comissao();
            ViewBag.EventoId = ObterEventos();
            ViewBag.MembroId = ObterMembros();
            return View(_membroEventoRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MembroEvento collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _membroEventoRepository.Actualizar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.EventoId = ObterEventos(collection.EventoId.ToString());
                ViewBag.MembroId = ObterMembros(collection.MembroId.ToString());
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
            return View(_membroEventoRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MembroEvento collection)
        {
            try
            {
                // TODO: Add delete logic here
                _membroEventoRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}