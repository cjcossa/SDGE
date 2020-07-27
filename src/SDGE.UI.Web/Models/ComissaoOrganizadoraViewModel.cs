using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class ComissaoOrganizadoraViewModel
    {

        public int ComissaoOrganizadoraId { get; set; }
        
        [Required(ErrorMessage = "Introduza o código")]
        [StringLength(10, ErrorMessage = "O códido deve ter 10 caracteres", MinimumLength = 10)]
        [Remote(action: "VerifyCode", controller: "ComissaoOrganizadora")]
        [Display(Name = "Código da comissão organizadora")]
        public string Codigo { get; set; }
    }
}
