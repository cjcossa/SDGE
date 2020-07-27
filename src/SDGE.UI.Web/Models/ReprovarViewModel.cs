using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class ReprovarViewModel
    {
        public int SubmissaoId { get; set; }
        public int EventoId { get; set; }
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Introduza as Observações")]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
