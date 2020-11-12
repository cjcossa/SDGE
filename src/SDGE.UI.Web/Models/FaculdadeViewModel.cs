using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SDGE.UI.Web.Models
{
    public class FaculdadeViewModel
    {
        public int FaculdadeId { get; set; }

        [Required(ErrorMessage = "Introduza a Designação")]
        [Remote(action: "VerificarDesignacao", controller: "Faculdade")]
        [Display(Name = "Designação")]
        public string Designacao { get; set; }

    }
}
