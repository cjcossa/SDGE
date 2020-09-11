using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class ComissaoCientifica
    {
        public ComissaoCientifica()
        {
            this.Removido = false;
        }
        public int ComissaoCientificaId { get; set; }
        public string Codigo { get; set; }
        public bool Removido { get; set; }
        public int CriadoPorId { get; set; }
        public ICollection<MembroCientifico> MembroCientificos { get; set; }
        public ICollection<Evento> Eventos { get; set; }
        public ICollection<Alerta> Alertas { get; set; }
    }
}
