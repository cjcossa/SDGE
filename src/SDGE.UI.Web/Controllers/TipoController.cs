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
    public class TipoController : Controller
    {
        private readonly ITipoRepository _tipoRepository;

        public TipoController(ITipoRepository tipoRepository)
        {
            _tipoRepository = tipoRepository;
        }
        // GET: Tipo
        public ActionResult Index()
        {
            return View(_tipoRepository.ObterTodos());
        }

        // GET: Tipo/Details/5
        public ActionResult Details(int id)
        {
            return View(_tipoRepository.ObterPorId(id));
        }

        // GET: Tipo/Create
        public ActionResult Create()
        {
            return View(new Tipo());
        }

        // POST: Tipo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tipo collection)
        {
            try
            {
                // TODO: Add insert logic here
                _tipoRepository.Adicionar(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tipo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_tipoRepository.ObterPorId(id));
        }

        // POST: Tipo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tipo collection)
        {
            try
            {
                // TODO: Add update logic here
                _tipoRepository.Actualizar(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tipo/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_tipoRepository.ObterPorId(id));
        }

        // POST: Tipo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tipo collection)
        {
            try
            {
                // TODO: Add delete logic here
                _tipoRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}