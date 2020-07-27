using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private int sessionId = 1; 

        public ParticipanteController(IParticipanteRepository participanteRepository, UserManager<IdentityUser> userManager)
        {
            _participanteRepository = participanteRepository;
            _userManager = userManager;
        }

        // GET: Participante
        public ActionResult Index(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_participanteRepository.ObterTodos());
        }

        public ActionResult Show()
        {
            return View(ParticipanteViewModel(_participanteRepository.ObterPorId(sessionId)));
        }

        // GET: Participante/Details/5
        public ActionResult Details(int id)
        {
            return View(_participanteRepository.ObterPorId(id));
        }

        // GET: Participante/Create
       /* public async Task<ActionResult> Create(string email, string userId, string code)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            ParticipanteViewModel model = new ParticipanteViewModel();
            model.Email = email;
            return View(model);
        }*/
        
        public ActionResult Create()
        {
            return View(new ParticipanteViewModel());
        }

        // POST: Participante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public async Task<ActionResult> Create(ParticipanteViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here

                var result = _participanteRepository.Adicionar(Participante(collection));
                if (result.Email != null)
                {
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(result.Email);
                    if (identityUser != null)
                    {
                        IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Participante");
                        if (!identityResult.Succeeded)
                            return View(identityResult.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }*/

        public ActionResult Create(ParticipanteViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here

                if(ModelState.IsValid)
                {
                   
                    _participanteRepository.Adicionar(Participante(collection));
                    return RedirectToAction("Index", new { msg = "Dados gravados."});
                }

                return View(collection);
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Participante/Edit/5
        public ActionResult Edit()
        {
            return View(ParticipanteViewModel(_participanteRepository.ObterPorId(sessionId)));
        }

        // POST: Participante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParticipanteViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
               // var result = _participanteRepository.ObterPorId(collection.ParticipanteId);
                _participanteRepository.Actualizar(Participante(collection));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Participante/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_participanteRepository.ObterPorId(id));
        }

        // POST: Participante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Participante collection)
        {
            try
            {
                // TODO: Add delete logic here
                _participanteRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Participante Participante(ParticipanteViewModel model)
        {
            Participante participante = new Participante()
            {
                ParticipanteId = model.ParticipanteId,
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone,
                Instituicao = model.Instituicao,
                Ocupacao = model.Ocupacao,
                TituloAcademico = model.TituloAcademico
            };
            return participante;
        }
        private ParticipanteViewModel ParticipanteViewModel(Participante participante)
        {
            ParticipanteViewModel model = new ParticipanteViewModel()
            {
                ParticipanteId = participante.ParticipanteId,
                Nome = participante.Nome,
                Email = participante.Email,
                Telefone = participante.Telefone,
                Instituicao = participante.Instituicao,
                Ocupacao = participante.Ocupacao,
                TituloAcademico = participante.TituloAcademico
            };
            return model;
        }
        //private IEnumerable<ParticipanteViewModel> ParticipanteViewModels()
    }
}