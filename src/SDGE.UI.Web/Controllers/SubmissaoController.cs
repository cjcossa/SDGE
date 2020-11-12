using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles;
using MimeKit;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class SubmissaoController : Controller
    {
        private readonly ISubmissaoRepository _submissaoRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly ITipoRepository _tipoRepository;
        private readonly IEventoParticipanteRepository _eventoParticipanteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDataImportanteRepository _dataImportanteRepository;
        private readonly IAlertaRepository _alertaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly IMembroCientificoRepository _membroCientificoRepository;

        public SubmissaoController(ISubmissaoRepository submissaoRepository, 
            IParticipanteRepository participanteRepository, 
            IEventoRepository eventoRepository, 
            ITipoRepository tipoRepository, 
            IEventoParticipanteRepository eventoParticipanteRepository,
            IWebHostEnvironment webHostEnvironment,
            IDataImportanteRepository dataImportanteRepository,
            IAlertaRepository alertaRepository,
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender,
            IMembroCientificoRepository membroCientificoRepository)
        {
            _submissaoRepository = submissaoRepository;
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _tipoRepository = tipoRepository;
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _webHostEnvironment = webHostEnvironment;
            _dataImportanteRepository = dataImportanteRepository;
            _alertaRepository = alertaRepository;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _membroCientificoRepository = membroCientificoRepository;
        }
        // GET: Submissao
        public ActionResult Index(int id = -1, string msg = null)
        {
            ViewBag.Alert = msg;

            if (IsMembro())
            {
                if(id > 0)
                {
                    var _result = _alertaRepository.ObterPorId(id);
                    if(_result != null)
                    {
                        _alertaRepository.Actualizar(_result);
                    }

                }
                return View(_submissaoRepository.ObterPorMembro(SessionId()));
            }
            return View();
        }
        public ActionResult Listar(int id = -1, string msg = null)
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
            return View(_submissaoRepository.ObterPorParticipante(SessionId()));
        }

        // GET: Submissao/Details/5
        public ActionResult Details(int id)
        {
            return PartialView("_Details", _submissaoRepository.ObterPorSubmissao(id));
        }

        // GET: Submissao/Create
        public ActionResult Create()
        {

            PreencherCombobox();
            return View(new SubmissaoViewModel());
        }
       
        // POST: Submissao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubmissaoViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                //string [] destino = null;
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
                            TipoId = collection.TipoId,
                            ParticipanteId = SessionId(),
                            EventoId = collection.EventoId
                        };

                       var _result =  _submissaoRepository.Adicionar(submissao);
                        if(_result != null)
                        {
                            bool state = false;
                            var alert = _alertaRepository.Adicionar(Alerta(_result, "Fez uma nova submissão"));

                            if(alert != null)
                            {
                                var evento = _eventoRepository.ObterPorId(_result.EventoId);
                                if (evento != null)
                                {
                                    var result2 = _membroCientificoRepository.ObterPorComissao(evento.ComissaoCientificaId, true);
                                    if (result2 != null)
                                    {
                                        foreach (var item in result2)
                                        {
                                            var msg = $"Olá { item.Membro.Nome}. <br><br> { _participanteRepository.ObterPorId(SessionId()).Nome } " +
                                                $"fez uma nova submissão no evento: {evento.Titulo}. <br> Em enexo o documento.";

                                            var message = new Message(new string[] { item.Membro.Email }, "Nova Submissão", msg, collection.File);
                                            if (Notificar(message))
                                                state = true;
                                        }
                                    }


                                }

                                if (state)
                                    return RedirectToAction("Listar", new { msg = "Submissão criada." });
                                else
                                    ModelState.AddModelError(string.Empty, "Erro ao notificar a comissão científica.");
                            }
                            
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

        // GET: Submissao/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ParticipanteId = ObterParticipantes();
            ViewBag.EventoId = ObterEventos(id.ToString());
            ViewBag.TipoId = ObterTipos();
            ViewBag.Ficheiro =_submissaoRepository.ObterPorId(id).Ficheiro;
            
            return View(Submeter(id));
        }
       
        // POST: Submissao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubmissaoViewModel submeterFicheiro)
        {

            string[] destino = null;
            try
            {
                // TODO: Add update logic here

                if (submeterFicheiro.File != null)
                {
                    var fileName = UploadFile(submeterFicheiro);
                    submeterFicheiro.Ficheiro = fileName;
                }
                _submissaoRepository.Actualizar(Submissao(submeterFicheiro));

                bool state = false;
              
                var evento = _eventoRepository.ObterPorId(submeterFicheiro.EventoId);
                if (evento != null)
                {
                    var result2 = _membroCientificoRepository.ObterPorComissao(evento.ComissaoCientificaId, true);

                    foreach (var item in result2)
                    {
                        var msg = $"Olá, { item.Membro.Nome}. <br><br> { _participanteRepository.ObterPorId(SessionId()).Nome } " +
                            $"actualizou a submissão: {submeterFicheiro.Titulo}. <br> Em enexo o documento.";

                        var message = new Message(new string[] { item.Membro.Email }, "Nova Submissão", msg, submeterFicheiro.File);
                        if (Notificar(message))
                            state = true;
                    }
                }

                if (state)
                    return RedirectToAction("Listar", new { msg = "Submissão actualizada." });
                else
                    ModelState.AddModelError(string.Empty, "Erro ao notificar a comissão científica.");

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
            return PartialView("_Delete", _submissaoRepository.ObterPorId(id));
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
                var result = _submissaoRepository.ObterPorId(collection.SubmissaoId);
                _submissaoRepository.Remover(result);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Reprovar(int id)
        {
            var result = _submissaoRepository.ObterPorId(id);
            ReprovarViewModel reprovar = new ReprovarViewModel
            {
                SubmissaoId = result.SubmissaoId,
                Titulo = result.Titulo,
                EventoId = result.EventoId
            };
            return View(reprovar);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reprovar(int id, ReprovarViewModel collection)
        {
            try
            {
                // TODO: Add delete logic here

                if(ModelState.IsValid)
                {
                    var result = _submissaoRepository.ObterPorId(collection.SubmissaoId);
                    result.Observacoes = collection.Observacoes;
                    result.Status = "Reprovado";
                    _submissaoRepository.Actualizar(result);
                    _alertaRepository.Adicionar(Alerta(result, "Resultado de avaliacão disponível para a submissão: "+result.Titulo, true));

                    var result2 = _submissaoRepository.ObterPorSubmissao(collection.SubmissaoId);
                    var msg = $"Olá, {result2.Participante.Nome}. <br><br> A submissão: {result2.Titulo} foi reprovada.<br> Observações<br> {collection.Observacoes}";

                    var message = new Message(new string[] { result2.Participante.Email }, "Resultado de avaliação", msg, null);
                    if (Notificar(message))
                        return RedirectToAction("Index", new { id = id, msg = "Submissão reprovada." });
                }
                return View(collection);
            }
            catch
            {
                return View(collection);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Aprovar(string [] aprovar, string aprovado)
        {
            string status = null;
            string [] destino = null;
            Participante participante = null;
            Submissao result = null;
            bool state = false;
            int id = -1;
            if (aprovar != null)
            {
                foreach (var sId in aprovar)
                {
                    id = int.Parse(sId);
                    result = _submissaoRepository.ObterPorId(id);
                    if (aprovado != null)
                        result.Status = "Aprovado";
                    else
                        result.Status = "Aprovado com Revisão";
                    status = result.Status;
                    id = result.EventoId;
                   _submissaoRepository.Actualizar(result);
                   _alertaRepository.Adicionar(Alerta(result, "Resultado de avaliacão disponível para a submissão: " + result.Titulo, true));
                    participante = _participanteRepository.ObterPorId(result.ParticipanteId);

                    var msg = $"Olá, {participante.Nome}. <br><br> A sua submissão: {result.Titulo} foi {result.Status}.";

                    var message = new Message(new string[] { participante.Email}, "Resultado de Submissão", msg, null);
                    if (Notificar(message))
                    {
                        state = true;
                    }
                }
                

            }
            if(aprovado != null && state)
                return RedirectToAction("Index", new { id = id, msg = $"Submissão(ões) Aprovado(s)" });

            return RedirectToAction("Index", new { id = id, msg = $"Submissão(ões) Aprovado(s) com Revisão" });
        }

       
        public ActionResult Confirmar(int id)
        {
            bool state = false;
            string ms = null; 
            var result = _submissaoRepository.ObterPorId(id);
                result.Confirmado = !result.Confirmado;
            _submissaoRepository.Actualizar(result);
            var result2 = _membroCientificoRepository.ObterPorComissao(_eventoRepository.ObterPorId(result.EventoId).ComissaoCientificaId, true);
            if (result.Confirmado)
            {
                ms = "confirmou";
            }
            else
            {
                ms = "cancelou";
            }
            var alert = _alertaRepository.Adicionar(Alerta(result, "Cancelou a submissão"));
            if (alert != null)
            {
                foreach (var item in result2)
                {
                    var msg = $"Olá, {item.Membro.Nome}. <br><br> { _participanteRepository.ObterPorId(SessionId()).Nome } {ms} a submissão: {result.Titulo}.";

                    var message = new Message(new string[] { item.Membro.Email }, "Confirmação de Participação", msg, null);
                    if (Notificar(message))
                        state = true;
                }

                //if (state)
                    
            }

            if(ms.Equals("confirmou"))
                return RedirectToAction("Listar", new { msg = "Submissão confirmada." });

            return RedirectToAction("Listar", new { msg = "Submissão cancelada." });
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
        private SubmissaoViewModel Submeter(int id)
        {
            Submissao submissao = _submissaoRepository.ObterPorId(id);

            return new SubmissaoViewModel
            {
                SubmissaoId = submissao.SubmissaoId,
                Titulo = submissao.Titulo,
                Descricao = submissao.Descricao,
                TipoId = submissao.TipoId,
                ParticipanteId = SessionId(),
                EventoId = submissao.EventoId,
                Ficheiro = submissao.Ficheiro
            };
        }
        private Submissao Submissao(SubmissaoViewModel submeterFicheiro)
        {
            return new Submissao
            {
                SubmissaoId = submeterFicheiro.SubmissaoId,
                Titulo = submeterFicheiro.Titulo,
                Descricao = submeterFicheiro.Descricao,
                Ficheiro = submeterFicheiro.Ficheiro,
                TipoId = submeterFicheiro.TipoId,
                ParticipanteId = SessionId(),
                EventoId = submeterFicheiro.EventoId
            };
        }

        private string UploadFile(SubmissaoViewModel submeterFicheiro)
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
       
        private List<SelectListItem> ObterEventos(string id = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var result = _eventoRepository.ObterTodos();
            bool state = true;
            //string codigo = _comissaoOrganizadoraRepository.ObterPorId(id).Codigo;

            foreach (var item in result)
            {
                if (_eventoParticipanteRepository.VerificarEvento(item.EventoId, SessionId()))
                {
                    var eventoParticipante = _eventoParticipanteRepository.ObterPorEventoParticipante(item.EventoId, SessionId());
                    if (eventoParticipante.Confirmado)
                    {
                        if (_dataImportanteRepository.VerificarPrazoFinalidade("Submissões", item.EventoId))
                        {
                            items.Add(new SelectListItem() { Value = item.EventoId.ToString(), Text = item.Titulo });
                        }
                        else
                        {
                            state = false;
                        }
                    }
                    else
                    {
                        state = false;
                    }
                        
                }
            }
            if(id != null)
            {
                var data = _submissaoRepository.ObterPorId(int.Parse(id));
                items.Add(new SelectListItem() { Value = data.EventoId.ToString(), Text = data.Evento.Titulo });
            }
           
            if (!state && id == null)
                items.Add(new SelectListItem() { Value = "", Text = "Não existe nenhum evento" });
            return items;

        }
        private SelectList ObterTipos(string id = null)
        {
            return new SelectList(_tipoRepository.ObterTodos(), "TipoId", "Titulo", id);
        }

        private void PreencherCombobox()
        {
            ViewBag.EventoId = ObterEventos();
            ViewBag.TipoId = ObterTipos();
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
                Tipo = "Submissao"
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