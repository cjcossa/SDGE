using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;

namespace SDGE.UI.Web.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteRepository _participanteRepository;

        public ParticipanteController(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
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
        public ActionResult Create()
        {
            return View(new Participante());
        }

        // POST: Participante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Participante collection)
        {
            try
            {
                // TODO: Add insert logic here
                _participanteRepository.Adicionar(collection);
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