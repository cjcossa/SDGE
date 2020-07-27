using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
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

        private int sessionId = 1;

        public SubmissaoController(ISubmissaoRepository submissaoRepository, 
            IParticipanteRepository participanteRepository, 
            IEventoRepository eventoRepository, 
            ITipoRepository tipoRepository, 
            IEventoParticipanteRepository eventoParticipanteRepository,
            IWebHostEnvironment webHostEnvironment,
            IDataImportanteRepository dataImportanteRepository,
            IAlertaRepository alertaRepository)
        {
            _submissaoRepository = submissaoRepository;
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _tipoRepository = tipoRepository;
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _webHostEnvironment = webHostEnvironment;
            _dataImportanteRepository = dataImportanteRepository;
            _alertaRepository = alertaRepository;
        }
        // GET: Submissao
        public ActionResult Index(int id, string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_submissaoRepository.ObterPorEvento(id));
        }
        public ActionResult Listar(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_submissaoRepository.ObterPorParticipante(sessionId));
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
                            ParticipanteId = sessionId,
                            EventoId = collection.EventoId
                        };

                       var _result =  _submissaoRepository.Adicionar(submissao);
                        if(_result != null)
                        {
                            var result = _eventoRepository.ObterPorId(_result.EventoId);
                            Alerta alerta = new Alerta
                            {
                                Messagem = "Fez uma nova submissão",
                                ParticipanteId = _result.ParticipanteId,
                                ComissaoCientificaId = result.ComissaoCientificaId,
                                ComissaoOrganizadoraId = result.ComissaoOrganizadoraId,
                                Destino = false

                            };
                            _alertaRepository.Adicionar(alerta);
                        }
                        
                        return RedirectToAction("Listar", new { msg = "Submissão criada." });

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
        public ActionResult Edit(int id, SubmissaoViewModel submeterFicheiro)
        {
            
            try
            {
                // TODO: Add update logic here

                if (submeterFicheiro.File != null)
                {
                    var fileName = UploadFile(submeterFicheiro);
                    submeterFicheiro.Ficheiro = fileName;
                }
                _submissaoRepository.Actualizar(Submissao(submeterFicheiro));
                return RedirectToAction("Listar", new { msg = "Submissão actualizada." });

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
                    return RedirectToAction("Index", new { id = id, msg = "Submissão reprovada" });
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
            int id = -1;
            if (aprovar != null)
            {
                foreach (var sId in aprovar)
                {
                    id = int.Parse(sId);
                    var result = _submissaoRepository.ObterPorId(id);
                    if (aprovado != null)
                        result.Status = "Aprovado";
                    else
                        result.Status = "Aprovado com Revisão";
                    status = result.Status;
                    id = result.EventoId;
                    _submissaoRepository.Actualizar(result);
                }
            }
            if(aprovado != null)
                return RedirectToAction("Index", new { id = id, msg = $"Submissão(ões) Aprovado(s)" });

            return RedirectToAction("Index", new { id = id, msg = $"Submissão(ões) Aprovado(s) com Revisão" });
        }

       
        public ActionResult Confirmar(int id)
        {
            var result = _submissaoRepository.ObterPorId(id);
                result.Confirmado = !result.Confirmado;
            _submissaoRepository.Actualizar(result);
            if(result.Confirmado)
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
                ParticipanteId = sessionId,
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
                ParticipanteId = sessionId,
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
        private SelectList ObterEventos(string id = null)
        {
            return new SelectList(_eventoRepository.ObterTodos(), "EventoId", "Titulo", id);
        }
        private List<SelectListItem> ObterEventos()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var result = _eventoRepository.ObterTodos();
            bool state = true;
            //string codigo = _comissaoOrganizadoraRepository.ObterPorId(id).Codigo;

            foreach (var item in result)
            {
                if (_eventoParticipanteRepository.VerificarEvento(item.EventoId, sessionId))
                {
                    var eventoParticipante = _eventoParticipanteRepository.ObterPorEventoParticipante(item.EventoId, sessionId);
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
           
            if (!state)
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
        public string Email()
        {
            string result = null;
            try
            {
                SmtpClient s = new SmtpClient("smtp.gmail.com", 587);
                s.EnableSsl = true;
                s.DeliveryMethod = SmtpDeliveryMethod.Network;
                s.UseDefaultCredentials = false;
                s.Credentials = new NetworkCredential("narciojoao14@gmail.com", "Halima*14");
                MailMessage m = new MailMessage();
                m.To.Add("carloscossa62@gmail.com");
                m.From = new MailAddress("narciojoao14@gmail.com");
                m.Subject = "SGDE";
                m.Body = "New Email";
                s.Send(m);
                result = "Email enviado";
                return result;

            }
            catch(Exception e)
            {
                result = e.Message;
                return result;
            }
        }
    }
}