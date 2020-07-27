using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class ParticipanteViewModel
    {
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "Introduza o Noma")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza a Ocupação")]
        [Display(Name = "Ocupação")]
        public string Ocupacao { get; set; }

        [Required(ErrorMessage = "Introduza a Instituição")]
        [Display(Name = "Instituição")]
        public string Instituicao { get; set; }

        [Required(ErrorMessage = "Introduza o Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduza o Telefone")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Introduza o Título Academico")]
        [Display(Name = "Título Academico")]
        public string TituloAcademico { get; set; }
    }
}
