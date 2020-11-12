using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class FaculdadeController : Controller
    {
        private readonly IFaculdadeRepository _faculdadeRepository;

        public FaculdadeController(IFaculdadeRepository faculdadeRepository)
        {
            _faculdadeRepository = faculdadeRepository;
        }
        // GET: Faculdade
        public ActionResult Index(string msg = null, string type = null)
        {
            ViewBag.Alert = msg;
            ViewBag.Type = msg;
            return View(_faculdadeRepository.ObterTodos());
        }

        // GET: Faculdade/Details/5
        public ActionResult Details(int id)
        {
            return View(_faculdadeRepository.ObterPorId(id));
        }

        // GET: Faculdade/Create
        public ActionResult Create()
        {
            return View(new FaculdadeViewModel());
        }

        // POST: Faculdade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FaculdadeViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var faculdade = new Faculdade { FaculdadeId = collection.FaculdadeId, Designacao = collection.Designacao, Removido = false };
                    _faculdadeRepository.Adicionar(faculdade);
                    return RedirectToAction("Index", new { msg = "Faculdade registada.", type = "success" });
                }
                return View(collection);
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Faculdade/Edit/5
        public ActionResult Edit(int id)
        {
            Faculdade faculdade = _faculdadeRepository.ObterPorId(id);
            var facul = new FaculdadeViewModel { FaculdadeId = faculdade.FaculdadeId, Designacao = faculdade.Designacao };

            return View(facul);
        }

        // POST: Faculdade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FaculdadeViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var faculdade = new Faculdade { FaculdadeId = collection.FaculdadeId, Designacao = collection.Designacao, Removido = false };
                    _faculdadeRepository.Actualizar(faculdade);
                    return RedirectToAction("Index", new { msg = "Faculdade alterada.", type = "success" });
                }
                return View(collection);
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Faculdade/Delete/5
        public ActionResult Delete(int id)
        {
            return PartialView("_Remover", _faculdadeRepository.ObterPorId(id));
        }

        // POST: Faculdade/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Faculdade collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = _faculdadeRepository.ObterPorId(collection.FaculdadeId);
                _faculdadeRepository.Remover(result);
                return RedirectToAction("Index", new { msg = "Faculdade removida.", type = "danger" });
            }
            catch
            {
                return PartialView("_Remover", collection);
            }
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarDesignacao(string designacao)
        {
            if (_faculdadeRepository.VerificarDesignacao(designacao))
            {
                return Json($"A designação {designacao} existe.");
            }
            return Json(true);
        }
    }
}