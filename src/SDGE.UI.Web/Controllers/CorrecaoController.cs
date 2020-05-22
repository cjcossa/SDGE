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
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;

namespace SDGE.UI.Web.Controllers
{
    public class CorrecaoController : Controller
    {

        private readonly ICorrecaoRepository _correcaoRepository;
        private readonly ISubmissaoRepository _submissaoRepository;
        private readonly IMembroRepository _membroRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CorrecaoController(ICorrecaoRepository correcaoRepository, ISubmissaoRepository submissaoRepository, IMembroRepository membroRepository, IWebHostEnvironment webHostEnvironment)
        {
            _correcaoRepository = correcaoRepository;
            _submissaoRepository = submissaoRepository;
            _membroRepository = membroRepository;
            _webHostEnvironment = webHostEnvironment;
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
            PreencherCombobox();
            return View(new SubmeterCorrecao());
        }
        
        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmeterCorrecao collection)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    if (collection.File != null)
                    {
                        var fileName = UploadFile(collection);
                        Correcao correcao = new Correcao
                        {
                            Ficheiro = fileName,
                            Observacoes = collection.Observacoes,
                            SubmissaoId = collection.SubmissaoId,
                            MembroId = collection.MembroId
                        };

                        _correcaoRepository.Adicionar(correcao);
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

        // GET: MembroEvento/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SubmissaoId = ObterSubmissoes();
            ViewBag.MembroId = ObterMembros();
            ViewBag.Ficheiro = _correcaoRepository.ObterPorId(id).Ficheiro;
            return View(Submeter(id));
        }

        // POST: MembroEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubmeterCorrecao submeterCorrecao)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (submeterCorrecao.File != null)
                    {
                        var fileName = UploadFile(submeterCorrecao);
                        submeterCorrecao.Ficheiro = fileName;
                    }
                    _correcaoRepository.Actualizar(Submeter(submeterCorrecao));
                    return RedirectToAction(nameof(Index));
                }
                PreencherCombobox();
                return View(submeterCorrecao);
            }
            catch
            {
                PreencherCombobox();
                return View(submeterCorrecao);
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Download(int id)
        {
            var fileName = _correcaoRepository.ObterPorId(id).Ficheiro;
            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "Correcoes");
            var filePath = Path.Combine(uploads, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), Path.GetFileName(fileName));

        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        private string UploadFile(SubmeterCorrecao submeterCorrecao)
        {
            if (submeterCorrecao.File != null)
            {
                var uplodasFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Correcoes");
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(submeterCorrecao.File.FileName);
                var filePath = Path.Combine(uplodasFolder, fileName);
                submeterCorrecao.File.CopyTo(new FileStream(filePath, FileMode.Create));

                return fileName;
            }
            return null;
        }

        public SelectList ObterSubmissoes(string id = null)
        {
            return new SelectList(_submissaoRepository.ObterTodos(), "SubmissaoId", "Titulo", id);
        }
        public SelectList ObterMembros(string id = null)
        {
            return new SelectList(_membroRepository.ObterPorComissao("Cientifica"), "MembroId", "Nome", id);
        }

        private void PreencherCombobox()
        {
            ViewBag.SubmissaoId = ObterSubmissoes();
            ViewBag.MembroId = ObterMembros();
        }
        private object Submeter(int id)
        {
            Correcao correcao = _correcaoRepository.ObterPorId(id);
            return new SubmeterCorrecao
            {
                CorrecaoId = correcao.CorrecaoId,
                Ficheiro = correcao.Ficheiro,
                Observacoes = correcao.Observacoes,
                SubmissaoId = correcao.SubmissaoId,
                MembroId = correcao.MembroId
            };
        }
        private Correcao Submeter(SubmeterCorrecao submeterCorrecao)
        {
            return new Correcao
            {
                CorrecaoId = submeterCorrecao.CorrecaoId,
                Ficheiro = submeterCorrecao.Ficheiro,
                Observacoes = submeterCorrecao.Observacoes,
                SubmissaoId = submeterCorrecao.SubmissaoId,
                MembroId = submeterCorrecao.MembroId
            };
        }

    }
}