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
        private int sessionId = 1;
        private int sessionAdminId = 2;

        public EventoParticipanteController(IEventoParticipanteRepository eventoParticipanteRepository, 
            IEventoRepository eventoRepository,
            IParticipanteRepository participanteRepository, 
            IWebHostEnvironment webHostEnvironment,
            IComissaoOrganizadoraRepository comissaoOrganizadoraRepository,
            IDataImportanteRepository dataImportanteRepository)
        {
            _eventoParticipanteRepository = eventoParticipanteRepository;
            _eventoRepository = eventoRepository;
            _participanteRepository = participanteRepository;
            _webHostEnvironment = webHostEnvironment;
            _comissaoOrganizadoraRepository = comissaoOrganizadoraRepository;
            _dataImportanteRepository = dataImportanteRepository;
        }
        // GET: MembroEvento
        public ActionResult Index(int id, string msg = null, string type = null)
        {
            ViewBag.Alert = msg;
            ViewBag.Type = type;
            return View(_eventoParticipanteRepository.ObterPorEvento(id));
        }

        public ActionResult Listar(string msg = null)
        {
            ViewBag.Alert = msg;
            //ViewBag.Prazo = _dataImportanteRepository.VerificarPrazoFinalidade("Inscrições", data.EventoId);
            return View(_eventoParticipanteRepository.ObterPorParticipante(sessionId));
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
                if (_eventoParticipanteRepository.VerificarEvento(collection.EventoId, sessionId))
                {
                    ModelState.AddModelError("EventoId", $"O participante ja esta inscrito no evento.");
                }
                if (ModelState.IsValid)
                {
                    
                    if (collection.File != null)
                    {
                        var fileName = UploadFile(collection);
                        
                        if (_eventoParticipanteRepository.VerificarEvento(collection.EventoId, sessionId, true))
                        {
                            var _result = _eventoParticipanteRepository.ObterPorEventoParticipante(collection.EventoId, sessionId, true);
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
                                ParticipanteId = sessionId
                            };
                            _eventoParticipanteRepository.Adicionar(eventoParticipante);
                        }
                       
                        return RedirectToAction("Listar", new { msg = "Inscrição criada." });
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
        public ActionResult Confirmar(string [] confirmar, string cnf)
        {
            int eventoId = 0;
            if(confirmar != null)
            {
                foreach (var epId in confirmar)
                {
                    int id = int.Parse(epId);
                    var result = _eventoParticipanteRepository.ObterPorId(id);
                    eventoId = result.EventoId;
                    if (cnf != null)
                        result.Confirmado = true;
                    else
                        result.Confirmado = false;

                    _eventoParticipanteRepository.Actualizar(result);
                }
            }
            if(cnf != null)
                return RedirectToAction("Index", new {id = eventoId , msg = "Inscrição confirmada.", type = "success" });

            return RedirectToAction("Index", new {id = eventoId , msg = "Inscrição cancelada.", type = "danger" });
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
                // TODO: Add update logic here
                
                    if (inscricao.File != null)
                    {
                        var fileName = UploadFile(inscricao);
                        inscricao.Comprovativo = fileName;
                    }
                    
                        _eventoParticipanteRepository.Actualizar(Inscricao(inscricao));
                    //}
                     return RedirectToAction("Listar", new { msg = "Inscrição actualizada." });
                
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
            return PartialView("_Remover", _eventoParticipanteRepository.ObterPorEventoParticipante(id,sessionId));
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
                return RedirectToAction(nameof(Index));
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
                if (!_eventoParticipanteRepository.VerificarEvento(item.EventoId, sessionId))
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
               
                if (_dataImportanteRepository.VerificarPrazoFinalidade("Inscrições", data.EventoId))
                {
                    items.Add(new SelectListItem() { Value = data.EventoId.ToString(), Text = data.Evento.Titulo });
                }
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
                ParticipanteId = sessionId,
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
            if (_eventoParticipanteRepository.VerificarEvento(eventoId, sessionId))
            {
                return Json($"O participante já esta inscrito no evento.");
            }
            return Json(true);
        }
    }
}