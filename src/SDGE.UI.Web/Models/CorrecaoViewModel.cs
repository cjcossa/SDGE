using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class CorrecaoViewModel
    {
        public int CorrecaoId { get; set; }
        public string Ficheiro { get; set; }

        [Required(ErrorMessage = "Seleccione o Ficheiro")]
        [Display(Name = "Ficheiro")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Introduza as Observasões")]
        [Display(Name = "Observasões")]
        public string Observacoes { get; set; }
        public string Titulo { get; set; }
        public int SubmissaoId { get; set; }
        public int MembroId { get; set; }
    }
}
