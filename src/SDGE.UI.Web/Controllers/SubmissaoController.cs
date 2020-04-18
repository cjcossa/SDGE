using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
       // private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubmissaoController(ISubmissaoRepository submissaoRepository, IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, ITipoRepository tipoRepository, IWebHostEnvironment webHostEnvironment)
        {
            _submissaoRepository = submissaoRepository;
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _tipoRepository = tipoRepository;
            _webHostEnvironment = webHostEnvironment;
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

            PreencherCombobox();
            return View(new SubmeterFicheiro());
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
        private void PreencherCombobox()
        {
            ViewBag.ParticipanteId = ObterParticipantes();
            ViewBag.EventoId = ObterEventos();
            ViewBag.TipoId = ObterTipos();
        }


        // POST: Submissao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmeterFicheiro collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    string filePath = null;
                    if (collection.File != null)
                    {
                        string uplodasFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Ficheiros");
                        string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(collection.File.FileName);
                        filePath = Path.Combine(uplodasFolder, fileName);
                        collection.File.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    Submissao submissao = new Submissao {
                        Titulo = collection.Titulo,
                        Descricao = collection.Descricao,
                        Ficheiro = filePath,
                        Status = collection.Status,
                        TipoId = collection.TipoId,
                        ParticipanteId = collection.ParticipanteId,
                        EventoId = collection.EventoId                   
                    };

                    _submissaoRepository.Adicionar(submissao);
                    return RedirectToAction(nameof(Index));
                }
                PreencherCombobox();
                return View(collection);
            }
            catch
            {
                PreencherCombobox();
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