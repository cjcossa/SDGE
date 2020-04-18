using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Utilizador
    {
        public Utilizador()
        {

        }

        public int UtilizadorId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PermissaoId { get; set; }
        public Permissao Permissao { get; set; }


    }
}
