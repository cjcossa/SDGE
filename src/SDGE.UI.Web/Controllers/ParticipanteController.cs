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

namespace SDGE.UI.Web.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ParticipanteController(IParticipanteRepository participanteRepository, UserManager<IdentityUser> userManager)
        {
            _participanteRepository = participanteRepository;
            _userManager = userManager;
        }

        // GET: Participante
        public ActionResult Index()
        {
            return View(_participanteRepository.ObterTodos());
        }

        // GET: Participante/Details/5
        public ActionResult Details(int id)
        {
            return View(_participanteRepository.ObterPorId(id));
        }

        // GET: Participante/Create
        public async Task<ActionResult> Create(string email, string userId, string code)
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
            Participante participante = new Participante();
            participante.Email = email;
            return View(participante);
        }

        // POST: Participante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Participante collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                var result = _participanteRepository.Adicionar(collection);
                if (result.Email != null)
                {
                    IdentityUser identityUser = await _userManager.FindByEmailAsync(result.Email);

                    if (identityUser != null)
                    {
                        identityUser = await _userManager.FindByIdAsync(identityUser.Id);
                        if (identityUser != null)
                        {
                           IdentityResult identityResult = await _userManager.AddToRoleAsync(identityUser, "Participante");

                            if (!identityResult.Succeeded)
                                return View(identityResult.Errors);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Participante/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_participanteRepository.ObterPorId(id));
        }

        // POST: Participante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Participante collection)
        {
            try
            {
                // TODO: Add update logic here
                _participanteRepository.Actualizar(collection);
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
    }
}