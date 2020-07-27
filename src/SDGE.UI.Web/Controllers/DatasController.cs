using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.UI.Web.Models;

namespace SDGE.UI.Web.Controllers
{
    public class DatasController : Controller
    {
        private readonly IDataImportanteRepository _datasImportanteRepository;
        private readonly IEventoRepository _eventoRepository;
        public DatasController(IDataImportanteRepository dataImportanteRepository,
            IEventoRepository eventoRepository)
        {
            _datasImportanteRepository = dataImportanteRepository;
            _eventoRepository = eventoRepository;
        }
        // GET: Datas
        public ActionResult Index(int id, string msg = null)
        {
            ViewBag.Alert = msg;
            return View(_datasImportanteRepository.ObterPorEvento(id));
        }

        // GET: Datas/Details/5
        public ActionResult Details(int id)
        {
            return PartialView("_Details", _datasImportanteRepository.ObterPorData(id));
        }

        // GET: Datas/Create
        public ActionResult Create(int id)
        {
            var result = _eventoRepository.ObterPorId(id);
            DatasViewModel datas = new DatasViewModel
            {
                EventoId = id,
                Titulo = result.Titulo
            };
            ViewBag.Finalidade = Finaliddaes(id);
            return View(datas);
        }

        // POST: Datas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DatasViewModel collection)
        {
            var _result = _eventoRepository.ObterPorId(collection.EventoId);
            try
            {
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    if(_datasImportanteRepository.VerificarFinalidade(collection.Finalidade, collection.EventoId, true))
                    {
                        var result = _datasImportanteRepository.ObterPorFinalidade(collection.Finalidade, collection.EventoId, true);
                        result.DataInicio = collection.DataInicio;
                        result.DataFim = collection.DataFim;
                        result.Descricao = collection.Descricao;
                        result.Removido = !result.Removido;
                        _datasImportanteRepository.Actualizar(result);
                    }
                    else
                    {
                        _datasImportanteRepository.Adicionar(Data(collection));
                    }
                   return RedirectToAction("Index", new { id = collection.EventoId, msg = "Configuração criada." });
                }
                ViewBag.Finalidade = Finaliddaes(collection.EventoId);
                collection.Titulo = _result.Titulo;
                return View(collection);
            }
            catch
            {
                ViewBag.Finalidade = Finaliddaes(collection.EventoId);
                collection.Titulo = _result.Titulo;
                return View(collection);
            }
        }

        // GET: Datas/Edit/5
        public ActionResult Edit(int id)
        {
            var result = _datasImportanteRepository.ObterPorData(id);
            return View(Data(result));
        }

        // POST: Datas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DatasViewModel collection)
        {
            var _result = _eventoRepository.ObterPorId(collection.EventoId);
            try
            {
                // TODO: Add update logic here
                if(ModelState.IsValid)
                {
                    _datasImportanteRepository.Actualizar(Data(collection));
                    return RedirectToAction("Index", new { id = collection.EventoId, msg = "Configuração alterada." });
                }
                collection.Titulo = _result.Titulo;
                return View(collection);
            }
            catch
            {
                collection.Titulo = _result.Titulo;
                return View(collection);
            }
        }

        // GET: Datas/Delete/5
        public ActionResult Delete(int id)
        {
            return PartialView("_Remover", _datasImportanteRepository.ObterPorId(id));
        }

        // POST: Datas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DataImportante collection)
        {
            try
            {
                // TODO: Add delete logic here

                var result = _datasImportanteRepository.ObterPorId(collection.DataImportanteId);
                _datasImportanteRepository.Remover(result);
                return RedirectToAction("Index", new { id = result.EventoId, msg = "Configuração removida." });
            }
            catch
            {
                return PartialView("_Remover", _eventoRepository.ObterPorId(id));
            }
        }
        private List<SelectListItem> Finaliddaes(int id)
        {
            string [] finalidades = { "Avaliações", "Inscrições", "Submissões" };
            List<SelectListItem> items = new List<SelectListItem>();
            bool state = true;
            foreach (var finaldade in finalidades)
            {
                if(!_datasImportanteRepository.VerificarFinalidade(finaldade, id, false))
                {
                    items.Add(new SelectListItem() { Value = finaldade, Text = finaldade });
                    state = false;
                }
            }
            if(state)
                items.Add(new SelectListItem() { Value = "", Text = "Não existe nenhuma finalidade não configurada." });

            return items;
        }
        private DataImportante Data(DatasViewModel collection)
        {
            return new DataImportante
            {
                Finalidade = collection.Finalidade,
                DataFim = collection.DataFim,
                DataInicio = collection.DataInicio,
                Descricao = collection.Descricao,
                EventoId = collection.EventoId,
                DataImportanteId = collection.DataImportanteId

            };
        }
        private DatasViewModel Data(DataImportante collection)
        {
            return new DatasViewModel
            {
                Finalidade = collection.Finalidade,
                DataFim = collection.DataFim,
                DataInicio = collection.DataInicio,
                Descricao = collection.Descricao,
                EventoId = collection.EventoId,
                DataImportanteId = collection.DataImportanteId,
            };
        }
    }
}