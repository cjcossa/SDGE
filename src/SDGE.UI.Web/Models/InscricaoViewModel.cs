using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class InscricaoViewModel
    {
        public int EventoParticipanteId { get; set; }
        public string Comprovativo { get; set; }

        [Required(ErrorMessage = "Seleccione o Comprovativo de Pagamento")]
        [Display(Name = "Comprovativo de Pagamento")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Seleccione o Evento")]
       // [Remote(action: "VerificarEvento", controller: "EventoParticipante")]
        [Display(Name = "Eventos")]
        public int EventoId { get; set; }
        public bool Confirmado { get; set; }
        public int ParticipanteId { get; set; }
    }
}
