using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class EventoParticipanteController : Controller
    {

        private readonly IEventoParticipanteRepository _eventoParticipanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IComissaoOrganizadoraRepository _comissaoOrganizadoraRepository;
        private readonly IDataImportanteRepository _dataImportanteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAlertaRepository _alertaRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMembroOrganizadorRepository _membroOrganizadorRepository;

        public EventoParticipanteController(IEventoParticipanteRepository eventoParticipanteRepository, 
            IEventoRepository eventoRepository,
            IParticipanteRepository participanteRepository, 
            IWebHostEnvironment webHostEnvironment,
            IComissaoOrganizadoraRepository comissaoOrganizadoraRepository,
            IDataImportanteRepository dataImportanteRepository,
            IHttpContextAccessor httpContextAccessor,
            IAlertaRepository alertaRepository,
            IEmailSender emailSender,
            IMembroOrganizadorRepository membroOrganizadorRepository)
        {
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _eventoRepository = eventoRepository;
            _participanteRepository = participanteRepository;
            _webHostEnvironment = webHostEnvironment;
            _comissaoOrganizadoraRepository = comissaoOrganizadoraRepository;
            _dataImportanteRepository = dataImportanteRepository;
            _httpContextAccessor = httpContextAccessor;
            _alertaRepository = alertaRepository;
            _emailSender = emailSender;
            _membroOrganizadorRepository = membroOrganizadorRepository;
        }
        // GET: MembroEvento
        public ActionResult Index(int id = -1, string msg = null, string type = null)
        {
            ViewBag.Alert = msg;
            ViewBag.Type = type;
            if(IsMembro())
            {
                if (id > 0)
                {
                    var _result = _alertaRepository.ObterPorId(id);
                    if (_result != null)
                    {
                        _alertaRepository.Actualizar(_result);
                    }

                }
                return View(_eventoParticipanteRepository.ObterPorMembro(SessionId()));
            }

            return View();
        }

        public ActionResult Listar(int id = -1, string msg = null)
        {
            ViewBag.Alert = msg;
            //ViewBag.Prazo = _dataImportanteRepository.VerificarPrazoFinalidade("Inscrições", data.EventoId);
            if (id > 0)
            {
                var _result = _alertaRepository.ObterPorId(id);
                if (_result != null)
                {
                    _alertaRepository.Actualizar(_result);
                }

            }
            return View(_eventoParticipanteRepository.ObterPorParticipante(SessionId()));
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

          //  ViewBag.ParticipanteId = ObterParticipantes();
            return View(new InscricaoViewModel());
        }
        
       
        // POST: MembroEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InscricaoViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                EventoParticipante _result = null;
                              
                if (_eventoParticipanteRepository.VerificarEvento(collection.EventoId, SessionId()))
                {
                    ModelState.AddModelError("EventoId", $"O candidato ja esta inscrito no evento.");
                }
                if (ModelState.IsValid)
                {
                    
                    if (collection.File != null)
                    {
                        var fileName = UploadFile(collection);
                        
                        if (_eventoParticipanteRepository.VerificarEvento(collection.EventoId, SessionId(), true))
                        {
                            _result = _eventoParticipanteRepository.ObterPorEventoParticipante(collection.EventoId, SessionId(), true);
                            _result.Comprovativo = fileName;
                            _result.Removido = false;
                            _result.Confirmado = false;
                            _eventoParticipanteRepository.Actualizar(_result);
                        }
                        else
                        {
                            EventoParticipante eventoParticipante = new EventoParticipante
                            {
                                Comprovativo = fileName,
                                EventoId = collection.EventoId,
                                ParticipanteId = SessionId()
                            };
                            _eventoParticipanteRepository.Adicionar(eventoParticipante);
                        }
                        var evento = _eventoRepository.ObterPorId(collection.EventoId);
                        if(evento != null)
                        {
                            var result2 = _membroOrganizadorRepository.ObterPorComissao(evento.ComissaoOrganizadoraId, true);
                            bool state = false;
                            if (result2 != null)
                            {
                                foreach (var item in result2)
                                {
                                    var participante = _participanteRepository.ObterPorId(SessionId());

                                    if(participante != null)
                                    {
                                        var msg = $"Olá, {item.Membro.Nome}. <br><br> { participante.Nome } fez uma nova inscrição no evento: {evento.Titulo}." +
                                            $"<br> Em anexo o comprovativo de pagamento.";
                                        var message = new Message(new string[] { item.Membro.Email }, "Nova Inscrição", msg, collection.File);
                                        if (Notificar(message))
                                            state = true;
                                    }
                                   
                                }
                               
                                if(state)
                                    return RedirectToAction("Listar", new { msg = "Inscrição criada." });
                                else
                                    ModelState.AddModelError(string.Empty, "Erro ao notificar a comissão organizadora.");
                            }
                            else
                            {

                                ModelState.AddModelError(string.Empty, "Erro ao carregar a comissão organizadora.");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Erro ao carregar o evento.");
                        }

                    }
                        
                }
                ViewBag.EventoId = ObterEventos();
                return View(collection);
            }
            catch
            {
                ViewBag.EventoId = ObterEventos();
                return View(collection);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(string [] confirmar, string cnf, int Id)
        {
            int eventoId = Id;
            string msg = "Seleccione pelo menos uma inscrição.";
            string type = "danger";
            if(confirmar != null)
            {
                foreach (var epId in confirmar)
                {
                    int id = int.Parse(epId);
                    var result = _eventoParticipanteRepository.ObterPorId(id);
                    eventoId = result.EventoId;
                    if (cnf != null)
                    {
                        result.Confirmado = true;
                        msg = "confirmada.";
                        type = "success";
                    }
                    else
                    {
                        result.Confirmado = false;
                        msg = "cancelada.";
                    }
                       
                    _eventoParticipanteRepository.Actualizar(result);
                 
                    var eventoParticipante = _eventoParticipanteRepository.ObterPorEventoParticipante(result.EventoId, result.ParticipanteId);
                    if(eventoParticipante != null)
                    {
                        var message = new Message(new string[] { eventoParticipante.Participante.Email }, 
                            "Resultado de inscrição", $"Olá, {eventoParticipante.Participante.Nome}.<br><br> " +
                            $"Resultado de inscrição disponível para o evento: { eventoParticipante.Evento.Titulo}. <br> A sua inscrição foi { msg }.", null);
                        if (Notificar(message))
                        {
                            _alertaRepository.Adicionar(Alerta(result, "Resultado de inscrição disponível para o evento: ", true));
                        }
                    }
                }
            }

            return RedirectToAction("Index", new {id = eventoId , msg = "Inscrição " + msg, type = type });
        }
        
        // GET: MembroEvento/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.EventoId = ObterEventos(id.ToString());
           // ViewBag.ParticipanteId = ObterParticipantes();
            ViewBag.Ficheiro = _eventoParticipanteRepository.ObterPorId(id).Comprovativo;
            
            return View(Inscricao(id));
        }

        // POST: MembroEvento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InscricaoViewModel inscricao)
        {
            try
            {
                if (inscricao.File != null)
                {
                    var fileName = UploadFile(inscricao);
                    inscricao.Comprovativo = fileName;
                }
                    
                _eventoParticipanteRepository.Actualizar(Inscricao(inscricao));

                var evento = _eventoRepository.ObterPorId(inscricao.EventoId);
                if (evento != null)
                {
                    var result2 = _membroOrganizadorRepository.ObterPorComissao(evento.ComissaoOrganizadoraId, true);
                    bool state = false;
                    if (result2 != null)
                    {
                        foreach (var item in result2)
                        {
                            var participante = _participanteRepository.ObterPorId(SessionId());

                            if (participante != null)
                            {
                                var msg = $"Olá, {item.Membro.Nome}. <br><br> { participante.Nome } fez uma actualização na inscrição do evento: {evento.Titulo}." +
                                    $"<br> Em anexo o comprovativo de pagamento.";
                                var message = new Message(new string[] { item.Membro.Email }, "Actualização de Inscrição", msg, inscricao.File);
                                if (Notificar(message))
                                    state = true;
                            }

                        }

                        if (state)
                            return RedirectToAction("Listar", new { msg = "Inscrição actualizada." });
                        else
                            ModelState.AddModelError(string.Empty, "Erro ao notificar a comissão organizadora.");
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "Erro ao carregar a comissão organizadora.");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao carregar o evento.");
                }

                ViewBag.EventoId = ObterEventos(inscricao.EventoParticipanteId.ToString());
                return View(inscricao);

            }
            catch
            {
                ViewBag.EventoId = ObterEventos(inscricao.EventoParticipanteId.ToString());
                return View(inscricao);
            }
        }

        // GET: MembroEvento/Delete/5
        public ActionResult Delete(int id)
        {
            return PartialView("_Remover", _eventoParticipanteRepository.ObterPorEventoParticipante(id, SessionId()));
        }

        // POST: MembroEvento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventoParticipante collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = _eventoParticipanteRepository.ObterPorId(collection.EventoParticipanteId);
                _eventoParticipanteRepository.Remover(result);
                return RedirectToAction("Listar", new { msg = "Inscrição cancelada." });
            }
            catch
            {
                return View();
            }
        }
        private string UploadFile(InscricaoViewModel inscricao)
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
        public List<SelectListItem> ObterEventos(string id = null)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var result = _eventoRepository.ObterTodos();
            bool state = true;

            foreach (var item in result)
            {
                if (!_eventoParticipanteRepository.VerificarEvento(item.EventoId, SessionId()))
                {
                    if(_dataImportanteRepository.VerificarPrazoFinalidade("Inscrições", item.EventoId))
                    {
                        items.Add(new SelectListItem() { Value = item.EventoId.ToString(), Text = item.Titulo });
                        state = false;
                    }
                }
            }

            if (id != null)
            {
                var data = _eventoParticipanteRepository.ObterPorId(int.Parse(id));
                
                items.Add(new SelectListItem() { Value = data.EventoId.ToString(), Text = data.Evento.Titulo });
                
            }
           
            if (state && id == null)
                items.Add(new SelectListItem() { Value = "", Text = "Não existe nenhum evento" });
            return items;
        }
        
        public SelectList ObterParticipantes(string id = null)
        {
            return new SelectList(_participanteRepository.ObterTodos(), "ParticipanteId", "Nome", id);
        }
        private InscricaoViewModel Inscricao(int id)
        {
            EventoParticipante eventoParticipante = _eventoParticipanteRepository.ObterPorId(id);

            return new InscricaoViewModel
            {
                EventoParticipanteId = eventoParticipante.EventoParticipanteId,
                Comprovativo = eventoParticipante.Comprovativo,
                EventoId = eventoParticipante.EventoId,
                ParticipanteId = eventoParticipante.ParticipanteId
            };
        }
        private EventoParticipante Inscricao(InscricaoViewModel inscricao)
        {
            //EventoParticipante eventoParticipante = _eventoParticipanteRepository.ObterPorId(inscricao.EventoParticipanteId);
            return new EventoParticipante
            {
                EventoParticipanteId = inscricao.EventoParticipanteId,
                Comprovativo = inscricao.Comprovativo,
                EventoId = inscricao.EventoId,
                ParticipanteId = SessionId(),
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

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerificarEvento(int eventoId)
        {
            //int id = 1;
            if (_eventoParticipanteRepository.VerificarEvento(eventoId, SessionId()))
            {
                return Json($"O participante já esta inscrito no evento.");
            }
            return Json(true);
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
        public Alerta Alerta(EventoParticipante eventoParticipante, string msg, bool destino = false)
        {
            var result = _eventoRepository.ObterPorId(eventoParticipante.EventoId);
            Alerta alerta = new Alerta
            {
                Messagem = msg + result.Titulo,
                ParticipanteId = eventoParticipante.ParticipanteId,
                //ComissaoCientificaId = result.ComissaoCientificaId,
                ComissaoOrganizadoraId = result.ComissaoOrganizadoraId,
                Destino = destino,
                Tipo = "Inscricao"

            };
            //if(alerta != null)
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

        private bool NotificarComissao()
        {
            /* var result2 = _membroOrganizadorRepository.ObterPorComissao(_eventoRepository.ObterPorId(_result.EventoId).ComissaoCientificaId);
             var i = 0;
             foreach (var item in result2)
             {
                 destino[i] = item.Membro.Email;
                 i++;
             }
             */
            return false;
        }
    }
}