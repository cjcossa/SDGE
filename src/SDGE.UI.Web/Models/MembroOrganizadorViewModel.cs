using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SDGE.UI.Web.Models
{
    public class MembroOrganizadorViewModel
    {
        public int ComissaoOrganizadoraId { get; set; }

        [Required(ErrorMessage ="Seleccione o(s) membro(s)")]
        public List<int> Membros { get; set; }
    }
}
