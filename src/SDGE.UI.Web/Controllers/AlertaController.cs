using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;

namespace SDGE.UI.Web.Controllers
{
    public class AlertaController : Controller
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IAlertaRepository _alertaRepository;
        public AlertaController(IEventoRepository eventoRepository,
            IAlertaRepository alertaRepository)
        {
            _eventoRepository = eventoRepository;
            _alertaRepository = alertaRepository;
        }
        
        public IActionResult Alerta(Submissao submissao)
        {
            var result = _eventoRepository.ObterPorId(submissao.EventoId);
            Alerta alerta = new Alerta
            {
                Messagem = "Fez uma nova submissão",
                ParticipanteId = submissao.ParticipanteId,
                ComissaoCientificaId = result.ComissaoCientificaId,
                ComissaoOrganizadoraId = result.ComissaoOrganizadoraId,
                Destino = false

            };
            _alertaRepository.Adicionar(alerta);
            return RedirectToAction("Listar", "Submissao", new { msg = "Submissão criada." });
        }
    }
}