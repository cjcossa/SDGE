using Microsoft.AspNetCore.Mvc;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class EventoViewModel
    {
        public int EventoId { get; set; }

        [Required(ErrorMessage = "Introduza o Título")]
        [Remote(action: "VerificarTitulo", controller: "Evento")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Introduza o Lema")]
        [Display(Name = "Lema")]
        public string Lema { get; set; }

        [Required(ErrorMessage = "Introduza o Email")]
        [Remote(action: "VerificarEmail", controller: "Evento")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Introduza a Data Início")]
        [Display(Name = "Data de Início")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Introduza a Data de Término")]
        [Display(Name = "Data de Término")]
        public string DataFim { get; set; }

        [Required(ErrorMessage = "Introduza o Local")]
        [Display(Name = "Local")]
        public string Local { get; set; }

        [Required(ErrorMessage = "Introduza a Descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Seleccione a Categoria")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "Introduza o código da comissão organizadora")]
        [StringLength(10, ErrorMessage = "O códido deve ter 10 caracteres", MinimumLength = 10)]
        [Remote(action: "VerificarCodigoOrganizadora", controller: "Evento")]
        [Display(Name = "Código da comissão organizadora")]
        public string CodigoOrganizadora { get; set; }

        [Required(ErrorMessage = "Introduza o código da comissão cientifíca")]
        [StringLength(10, ErrorMessage = "O códido deve ter 10 caracteres", MinimumLength = 10)]
        [Remote(action: "VerificarCodigoCientifica", controller: "Evento")]
        [Display(Name = "Código da comissão científica")]
        public string CodigoCientifica { get; set; }
    }
}
