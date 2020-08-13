using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class MembroViewModel
    {
        public int MembroId { get; set; }

        [Required(ErrorMessage = "Introduza o Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza o Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduza o Telefone")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }
}
