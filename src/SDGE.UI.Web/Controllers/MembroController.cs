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
    public class MembroController : Controller
    {
        private readonly IMembroRepository _membroRepository;

        public MembroController(IMembroRepository membroRepository)
        {
            _membroRepository = membroRepository;
        }
        // GET: Membro
        public ActionResult Index()
        {
            return View(_membroRepository.ObterTodos());
        }

        // GET: Membro/Details/5
        public ActionResult Details(int id)
        {
            return View(_membroRepository.ObterPorId(id));
        }

        // GET: Membro/Create
        public ActionResult Create()
        {
            return View(new Membro());
        }

        // POST: Membro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Membro collection)
        {
            try
            {
                // TODO: Add insert logic here
                _membroRepository.Adicionar(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Membro/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_membroRepository.ObterPorId(id));
        }

        // POST: Membro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Membro collection)
        {
            try
            {
                // TODO: Add update logic here
                _membroRepository.Actualizar(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Membro/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_membroRepository.ObterPorId(id));
        }

        // POST: Membro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Membro collection)
        {
            try
            {
                // TODO: Add delete logic here
                _membroRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}