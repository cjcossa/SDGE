using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using MimeKit;
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
                    if(collection.File != null)
                    {
                        var fileName = UploadFile(collection);
                        Submissao submissao = new Submissao
                        {
                            Titulo = collection.Titulo,
                            Descricao = collection.Descricao,
                            Ficheiro = fileName,
                            Status = collection.Status,
                            TipoId = collection.TipoId,
                            ParticipanteId = collection.ParticipanteId,
                            EventoId = collection.EventoId
                        };

                        _submissaoRepository.Adicionar(submissao);
                        return RedirectToAction(nameof(Index));
                    }
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
            ViewBag.Ficheiro =_submissaoRepository.ObterPorId(id).Ficheiro;
            
            return View(Submeter(id));
        }
       
        // POST: Submissao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubmeterFicheiro submeterFicheiro)
        {
            
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (submeterFicheiro.File != null)
                    {
                        var fileName = UploadFile(submeterFicheiro);
                        submeterFicheiro.Ficheiro = fileName;
                    }
                    _submissaoRepository.Actualizar(Submissao(submeterFicheiro));
                    return RedirectToAction(nameof(Index));
                }
                PreencherCombobox();
                return View(submeterFicheiro);
            }
            catch
            {
                PreencherCombobox();
                return View(submeterFicheiro);
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

                //DeleteFile(collection.Ficheiro);
                _submissaoRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
      
        public ActionResult Download(int id)
        {
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Submissoes");
             var filePath = Path.Combine(uploads, _submissaoRepository.ObterPorId(id).Ficheiro);
             if (!System.IO.File.Exists(filePath))
                 return NotFound();

             return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }

        private void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Submissoes", fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        private SubmeterFicheiro Submeter(int id)
        {
            Submissao submissao = _submissaoRepository.ObterPorId(id);

            return new SubmeterFicheiro
            {
                SubmissaoId = submissao.SubmissaoId,
                Titulo = submissao.Titulo,
                Descricao = submissao.Descricao,
                Status = submissao.Status,
                TipoId = submissao.TipoId,
                ParticipanteId = submissao.ParticipanteId,
                EventoId = submissao.EventoId,
                Ficheiro = submissao.Ficheiro
            };
        }
        private Submissao Submissao(SubmeterFicheiro submeterFicheiro)
        {
            return new Submissao
            {
                SubmissaoId = submeterFicheiro.SubmissaoId,
                Titulo = submeterFicheiro.Titulo,
                Descricao = submeterFicheiro.Descricao,
                Ficheiro = submeterFicheiro.Ficheiro,
                Status = submeterFicheiro.Status,
                TipoId = submeterFicheiro.TipoId,
                ParticipanteId = submeterFicheiro.ParticipanteId,
                EventoId = submeterFicheiro.EventoId
            };
        }

        private string UploadFile(SubmeterFicheiro submeterFicheiro)
        {
            if (submeterFicheiro.File != null)
            {
                var uplodasFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Submissoes");
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(submeterFicheiro.File.FileName);
                var filePath = Path.Combine(uplodasFolder, fileName);
                submeterFicheiro.File.CopyTo(new FileStream(filePath, FileMode.Create));

                return fileName;
            }
            return null;
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
    }
}