using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;

namespace SDGE.UI.Web.Controllers
{
    public class EventoParticipanteController : Controller
    {

        private readonly IEventoParticipanteRepository _eventoParticipanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventoParticipanteController(IEventoParticipanteRepository eventoParticipanteRepository, IEventoRepository eventoRepository, IParticipanteRepository participanteRepository, IWebHostEnvironment webHostEnvironment)
        {
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _eventoRepository = eventoRepository;
            _participanteRepository = participanteRepository;
            _webHostEnvironment = webHostEnvironment;
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
            return View(new Inscricao());
        }
        
       
        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inscricao collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                if (ModelState.IsValid)
                {
                    if (collection.File != null)
                    {
                        var fileName = UploadFile(collection);
                        EventoParticipante eventoParticipante = new EventoParticipante { 
                           Comprovativo = fileName,
                           EventoId = collection.EventoId,
                           ParticipanteId = collection.ParticipanteId
                        };
                        _eventoParticipanteRepository.Adicionar(eventoParticipante);
                        return RedirectToAction(nameof(Index));
                    }
                        
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
            ViewBag.Ficheiro = _eventoParticipanteRepository.ObterPorId(id).Comprovativo;
            return View(Inscricao(id));
        }

        // POST: MembroEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inscricao inscricao)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (inscricao.File != null)
                    {
                        var fileName = UploadFile(inscricao);
                        inscricao.Comprovativo = fileName;
                    }
                    _eventoParticipanteRepository.Actualizar(Inscricao(inscricao));
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.EventoId = ObterEventos(inscricao.EventoId.ToString());
                ViewBag.ParticipanteId = ObterParticipantes(inscricao.ParticipanteId.ToString());
                return View(inscricao);
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
        private string UploadFile(Inscricao inscricao)
        {
            if (inscricao.File != null)
            {
                var uplodasFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Comprovativos");
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(inscricao.File.FileName);
                var filePath = Path.Combine(uplodasFolder, fileName);
                inscricao.File.CopyTo(new FileStream(filePath, FileMode.Create));

                return fileName;
            }
            return null;
        }
        public SelectList ObterEventos(string id = null)
        {
            return new SelectList(_eventoRepository.ObterTodos(), "EventoId", "Titulo", id);
        }
        public SelectList ObterParticipantes(string id = null)
        {
            return new SelectList(_participanteRepository.ObterTodos(), "ParticipanteId", "Nome", id);
        }
        private Inscricao Inscricao(int id)
        {
            EventoParticipante eventoParticipante = _eventoParticipanteRepository.ObterPorId(id);

            return new Inscricao
            {
                EventoParticipanteId = eventoParticipante.EventoParticipanteId,
                Comprovativo = eventoParticipante.Comprovativo,
                EventoId = eventoParticipante.EventoId,
                ParticipanteId = eventoParticipante.ParticipanteId
            };
        }
        private EventoParticipante Inscricao(Inscricao inscricao)
        {
            return new EventoParticipante
            {
                EventoParticipanteId = inscricao.EventoParticipanteId,
                Comprovativo = inscricao.Comprovativo,
                EventoId = inscricao.EventoId,
                ParticipanteId = inscricao.ParticipanteId
            };
        }
        public ActionResult Download(int id)
        {

            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Comprovativos");
            var filePath = Path.Combine(uploads, _eventoParticipanteRepository.ObterPorId(id).Comprovativo);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }
    }
}