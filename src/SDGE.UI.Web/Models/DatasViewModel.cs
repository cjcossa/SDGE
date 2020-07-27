using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class DatasViewModel
    {
        public int DataImportanteId { get; set; }
        public string Titulo { get; set; }

        [Display(Name = "Iniciar em")]
        [Required(ErrorMessage = "Introduza a Data Inicio")]
        public string DataInicio { get; set; }

        [Display(Name = "Terminar em")]
        [Required(ErrorMessage = "Introduza a Data Termino")]
        public string DataFim { get; set; }

        [Required(ErrorMessage = "Introduza a Finalidade")]
        public string Finalidade { get; set; }
        [Required(ErrorMessage = "Introduza a Descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public int EventoId { get; set; }
    }
}
