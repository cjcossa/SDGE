using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;


namespace SDGE.ApplicationCore.Entity
{
    public class MembroOrganizador
    {
        public MembroOrganizador()
        {
            this.Removido = false;
        }
        public int MembroOrganizadorId { get; set; }
        public bool Removido { get; set; }
        public int ComissaoOrganizadoraId { get; set; }
        public ComissaoOrganizadora ComissaoOrganizadora { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
    }
}
