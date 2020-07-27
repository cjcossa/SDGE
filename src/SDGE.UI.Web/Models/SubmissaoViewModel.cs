using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class SubmissaoViewModel
    {
        public int SubmissaoId { get; set; }

        [Required(ErrorMessage = "Introduza o Título")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Introduza a Descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public bool Confirmado { get; set; }
        public string Status { get; set; }

        [Required(ErrorMessage = "Seleccione o Formato de Apresentação")]
        [Display(Name = "Formatos de Apresentção")]
        public int TipoId { get; set; }
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "Seleccione o Evento")]
        [Display(Name = "Eventos")]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "Seleccione o Ficheiro")]
        [Display(Name = "Ficheiro")]
        public IFormFile File { get; set; }

        public string Ficheiro { get; set; }

    }
}
