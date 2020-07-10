using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Evento
    {
        public Evento()
        {
            this.Removido = false;
        }

        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public string Lema { get; set; }
        public string Email { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public bool Removido { get; set; }
        public ICollection<EventoParticipante> EventoParticipantes { get; set; }
        public ICollection<MembroEvento> MembroEventos { get; set; }
        public ICollection<Submissao> Submissoes { get; set; }
        public ICollection<DataImportante> DataImportantes { get; set; }

        public int ComissaoOrganizadoraId { get; set; }
        public ComissaoOrganizadora ComissaoOrganizadora { get; set; }
        public int ComissaoCientificaId { get; set; }
        public ComissaoCientifica ComissaoCientifica { get; set; }
        
    }
}
