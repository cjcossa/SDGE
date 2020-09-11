﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class MembroController : Controller
    {
        private readonly IMembroRepository _membroRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MembroController(IMembroRepository membroRepository, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _membroRepository = membroRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: Membro
        public ActionResult Index()
        {
            return View(_membroRepository.ObterTodos());
        }

        // GET: Membro/Details/5
        public ActionResult Details(string msg = null)
        {
            ViewBag.Alert = msg;
            return View(Membro(_membroRepository.ObterPorId(SessionId())));
        }

        // GET: Membro/Create
        public async Task<ActionResult> Create(string email, string userId, string code)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com o email '{email}'.");
            }
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{userId}'.");
            }
            MembroViewModel membro = new MembroViewModel();
            membro.Email = email;
            return View(membro);
        }

        // POST: Membro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MembroViewModel collection)
        {
            IdentityResult identityResult;
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    var result = _membroRepository.Adicionar(Membro(collection));
                    if (result != null)
                    {
                        IdentityUser identityUser = await _userManager.FindByEmailAsync(collection.Email);
                        if (identityUser != null)
                        {
                            
                            identityResult = await _userManager.AddToRoleAsync(identityUser, "Membro");
                            if (identityResult.Succeeded)
                            {
                                await _signInManager.SignOutAsync();
                                HttpContext.Session.Remove("_UserEmail");
                                return RedirectToAction("Eventos", "Evento");
                            }
                                
                        }
                        
                    }
                    
                }
                return View(collection);

            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Membro/Edit/5
        public ActionResult Edit()
        {
            return View(Membro(_membroRepository.ObterPorId(SessionId())));
        }

        // POST: Membro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MembroViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                _membroRepository.Actualizar(Membro(collection));
                string msg = "Dados alterados";
                return RedirectToAction("Details", new { msg = msg });
            }
            catch
            {
                return View();
            }
        }

        // GET: Membro/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_membroRepository.ObterPorId(id));
        }

        // POST: Membro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Membro collection)
        {
            try
            {
                // TODO: Add delete logic here
                _membroRepository.Remover(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Membro Membro(MembroViewModel model)
        {
            return new Membro
            {
                MembroId = model.MembroId,
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone
            };
        }
        private MembroViewModel Membro(Membro model)
        {
            return new MembroViewModel
            {
                MembroId = model.MembroId,
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone
            };
        }
        private int SessionId()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("_Membro") != null)
                return int.Parse(_httpContextAccessor.HttpContext.Session.GetString("_Membro"));

            return -1;
        }
    }
}