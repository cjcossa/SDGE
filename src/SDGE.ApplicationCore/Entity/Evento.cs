using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Evento
    {
        public Evento()
        { 
        
        }

        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public string Lema { get; set; }
        public string Email { get; set; }
        public DateTime DataInico { get; set; }
        public DateTime DataFim { get; set; }
        public string Local { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public ICollection<EventoParticipante> EventoParticipantes { get; set; }
        public ICollection<MembroEvento> MembroEventos { get; set; }
        public ICollection<Submissao> Submissoes { get; set; }
        //public ICollection<Comissao> Comissoes { get; set; }
    }
}
