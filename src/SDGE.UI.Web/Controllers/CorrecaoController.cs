using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class CorrecaoController : Controller
    {

        private readonly ICorrecaoRepository _correcaoRepository;
        private readonly ISubmissaoRepository _submissaoRepository;
        private readonly IMembroRepository _membroRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventoRepository _eventoRepository;
        private readonly IAlertaRepository _alertaRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public CorrecaoController(ICorrecaoRepository correcaoRepository, 
            ISubmissaoRepository submissaoRepository, 
            IMembroRepository membroRepository, 
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IEventoRepository eventoRepository,
            IAlertaRepository alertaRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _correcaoRepository = correcaoRepository;
            _submissaoRepository = submissaoRepository;
            _membroRepository = membroRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _eventoRepository = eventoRepository;
            _alertaRepository = alertaRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        // GET: MembroEvento
        public ActionResult Index(int id = -1, string msg = null)
        {
            ViewBag.Alert = msg;
            if (id > 0)
            {
                var _result = _alertaRepository.ObterPorId(id);
                if (_result != null)
                {
                    _alertaRepository.Actualizar(_result);
                }

            }
            if (IsParticipante())
            {
                //id = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Participante"));
                return View(_correcaoRepository.ObterPorParticipante(SessionId()));
            }

            if (IsMembro())
            {
                //id = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Membro"));
                return View(_correcaoRepository.ObterPorMembro(SessionId()));
            }

            return View(_correcaoRepository.ObterPorSubmissao(id));

        }
        public ActionResult Listar(int id = -1, string msg = null)
        {
            ViewBag.Alert = msg;
            int sessionId = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Participante"));
            if(id != -1)
                return View(_correcaoRepository.ObterPorSubmissao(id));

            return View(_correcaoRepository.ObterPorParticipante(sessionId));
        }

        // GET: MembroEvento/Details/5
        public ActionResult Details(int id)
        {
            return PartialView("_Details", _correcaoRepository.ObterPorCorrecao(id));
        }

        // GET: MembroEvento/Create
        [Authorize(Roles = "Cientifico")]
        public ActionResult Create(int id)
        {
            CorrecaoViewModel correcao = new CorrecaoViewModel();
            var result = _submissaoRepository.ObterPorId(id);
            correcao.SubmissaoId = id;
            correcao.Titulo = result.Titulo;
            return View(correcao);
        }
        
        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CorrecaoViewModel collection)
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
                            MembroId = SessionId()
                        };

                        _correcaoRepository.Adicionar(correcao);
                        var result = _submissaoRepository.ObterPorSubmissao(collection.SubmissaoId);
                        if(result != null)
                        {
                           
                            var alert = _alertaRepository.Adicionar(Alerta(result, "Correção disponível para a submissão: "+result.Titulo, true));
                            if(alert != null)
                            {
                               // var result2 = _submissaoRepository.ObterPorSubmissao(result.SubmissaoId);
                                var msg = $"Olá, {result.Participante.Nome}. <br><br> Correção disponível para a submissão: {result.Titulo}.<br>Observações<br> {collection.Observacoes}.<br>Em anexo o documento.";
                              
                                var message = new Message(new string[] { result.Participante.Email }, "Resultado de avaliação", msg, collection.File);
                                if (Notificar(message))
                                    return RedirectToAction("Index", new { id = result.SubmissaoId, msg = "Avaliação efectuada." });
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", new { id = result.SubmissaoId, msg = "Avaliação efectuada. N" });
                        }
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
        [Authorize(Roles = "Cientifico")]
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
        public ActionResult Edit(int id, CorrecaoViewModel submeterCorrecao)
        {
            try
            {
                if (submeterCorrecao.File != null)
                {
                    var fileName = UploadFile(submeterCorrecao);
                    submeterCorrecao.Ficheiro = fileName;
                }
                _correcaoRepository.Actualizar(Submeter(submeterCorrecao));
               
                var result2 = _submissaoRepository.ObterPorSubmissao(submeterCorrecao.SubmissaoId);
                var msg = $"Olá, {result2.Participante.Nome}, a correção da submissão: {result2.Titulo}, foi actualizada!<br>Observações <br> {submeterCorrecao.Observacoes}. <br> Em anexo o documento.";
                var message = new Message(new string[] { result2.Participante.Email }, "Resultado de avaliação", msg, submeterCorrecao.File);
                if (Notificar(message))
                    return RedirectToAction("Index", new { id = submeterCorrecao.SubmissaoId, msg = "Avaliação alterada." });

                //PreencherCombobox();
                return View(submeterCorrecao);
            }
            catch
            {
                //PreencherCombobox();
                return View(submeterCorrecao);
            }
        }

        // GET: MembroEvento/Delete/5
        [Authorize(Roles = "Cientifico")]
        public ActionResult Delete(int id)
        {
            return PartialView("_Delete", _correcaoRepository.ObterPorId(id));
        }

        // POST: MembroEvento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Correcao collection)
        {
            try
            {
                // TODO: Add delete logic here
              
                var result = _correcaoRepository.ObterPorId(id);
                _correcaoRepository.Remover(result);
                return RedirectToAction("Index", new { id = collection.SubmissaoId, msg = "Avaliação removida." });
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
        private string UploadFile(CorrecaoViewModel submeterCorrecao)
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
            Correcao correcao = _correcaoRepository.ObterPorCorrecao(id);
            return new CorrecaoViewModel
            {
                CorrecaoId = correcao.CorrecaoId,
                Ficheiro = correcao.Ficheiro,
                Observacoes = correcao.Observacoes,
                SubmissaoId = correcao.SubmissaoId,
                MembroId = correcao.MembroId,
                Titulo = correcao.Submissao.Titulo
            };
        }
        private Correcao Submeter(CorrecaoViewModel submeterCorrecao)
        {
            return new Correcao
            {
                CorrecaoId = submeterCorrecao.CorrecaoId,
                Ficheiro = submeterCorrecao.Ficheiro,
                Observacoes = submeterCorrecao.Observacoes,
                SubmissaoId = submeterCorrecao.SubmissaoId,
                MembroId = SessionId()
            };
        }

        private int SessionId()
        {
            int id = -1;
            if (IsParticipante())
                id = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Participante"));
            else if (IsMembro())
                id = int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Membro"));

            return id;
        }
        private bool IsMembro()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("_Membro") != null)
                return true;
            return false;
        }
        private bool IsParticipante()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("_Participante") != null)
                return true;
            return false;
        }
        public Alerta Alerta(Submissao submissao, string msg, bool destino = false)
        {
            var result = _eventoRepository.ObterPorId(submissao.EventoId);
            Alerta alerta = new Alerta
            {
                Messagem = msg,
                ParticipanteId = submissao.ParticipanteId,
                ComissaoCientificaId = result.ComissaoCientificaId,
                ComissaoOrganizadoraId = result.ComissaoOrganizadoraId,
                Destino = destino,
                Tipo = "Correcao"
            };
            return alerta;
        }
        private bool Notificar(Message message)
        {
            if (message.To != null)
            {
                _emailSender.SendEmailAsync(message);
                return true;
            }

            return false;
        }
    }
}