using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class ComissaoOrganizadora
    {
        public ComissaoOrganizadora()
        {
            this.Removido = false;
        }
        public int ComissaoOrganizadoraId { get; set; }
        public string Codigo { get; set; }
        public bool Removido { get; set; }
        public ICollection<MembroOrganizador> MembroOrganizadors { get; set; }
        public ICollection<Evento> Eventos { get; set; }
        public ICollection<Alerta> Alertas { get; set; }
    }

}
