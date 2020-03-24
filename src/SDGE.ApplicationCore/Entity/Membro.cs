using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Membro
    {
        public Membro()
        {

        }

        public int MembroId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int ComissaoId { get; set; }
        public Comissao Comissao { get; set; }
        public ICollection<MembroTipo> MembroTipos { get; set; }
    }
}
