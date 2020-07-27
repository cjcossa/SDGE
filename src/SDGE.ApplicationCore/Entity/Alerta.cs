using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Alerta
    {
        public Alerta()
        {
            this.Removido = false;
            this.Status = false;
        }

        public int AlertaId { get; set; }
        public string Messagem { get; set; }
        public bool Status { get; set; }
        public bool Removido { get; set; }
        public bool Destino { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int ComissaoOrganizadoraId { get; set; }
        public ComissaoOrganizadora ComissaoOrganizadora { get; set; }
        public int ComissaoCientificaId { get; set; }
        public ComissaoCientifica ComissaoCientifica { get; set; }
    }
}
