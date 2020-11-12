using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class DirectorViewModel
    {
        public int DirectorId { get; set; }

        [Required(ErrorMessage = "Introduza o Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza o Email")]
        [Remote(action: "VerificarEmail", controller: "Director")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduza o Telefone")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Compo obrigatorio")]
        [Display(Name = "Faculdades")]
        public int FaculdadeId { get; set; }
    }
}
