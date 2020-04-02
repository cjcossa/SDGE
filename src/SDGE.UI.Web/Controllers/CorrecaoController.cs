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
    public class CorrecaoController : Controller
    {

        private readonly ICorrecaoRepository _correcaoRepository;
        private readonly ISubmissaoRepository _submissaoRepository;
        private readonly IMembroRepository _membroRepository;

        public CorrecaoController(ICorrecaoRepository correcaoRepository, ISubmissaoRepository submissaoRepository, IMembroRepository membroRepository)
        {
            _correcaoRepository = correcaoRepository;
            _submissaoRepository = submissaoRepository;
            _membroRepository = membroRepository;
        }
        // GET: MembroEvento
        public ActionResult Index()
        {
            return View(_correcaoRepository.ObterTodos());
        }

        // GET: MembroEvento/Details/5
        public ActionResult Details(int id)
        {
            return View(_correcaoRepository.ObterJoinPorId(id));
        }

        // GET: MembroEvento/Create
        public ActionResult Create()
        {
            ViewBag.SubmissaoId = ObterSubmissoes();
            ViewBag.MembroId = ObterMembros();
            return View(new Correcao());
        }
        
        public SelectList ObterSubmissoes(string id = null)
        {
            return new SelectList(_submissaoRepository.ObterTodos(), "SubmissaoId", "Titulo", id);
        }
        public SelectList ObterMembros(string id = null)
        {
            return new SelectList(_membroRepository.ObterPorComissao("Cientifica"), "MembroId", "Nome", id);
        }

        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Correcao collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                if (ModelState.IsValid)
                {
                    _correcaoRepository.Adicionar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.SubmissaoId = ObterSubmissoes(collection.SubmissaoId.ToString());
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
            ViewBag.SubmissaoId = ObterSubmissoes();
            ViewBag.MembroId = ObterMembros();
            return View(_correcaoRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Correcao collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _correcaoRepository.Actualizar(collection);
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.SubmissaoId = ObterSubmissoes(collection.SubmissaoId.ToString());
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
            return View(_correcaoRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Correcao collection)
        {
            try
            {
                // TODO: Add delete logic here
                _correcaoRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}