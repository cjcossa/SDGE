using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Permissao
    {
        public Permissao()
        {

        }

        public int PermissaoId { get; set; }
        public string Nome { get; set; }
        public ICollection<Utilizador> Utilizadores { get; set; }

    }
}
