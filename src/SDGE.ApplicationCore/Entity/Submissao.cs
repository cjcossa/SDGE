using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Submissao
    {
        public Submissao()
        {

        }
        public int SubmissaoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Ficheiro { get; set; }
        public string Status { get; set; }
        public int TipoId { get; set; }
        public int ParticipanteId { get; set; }
        public int EventoId { get; set; }
        public Participante Participante { get; set; }
        public Correcao Correcao { get; set; }
        public Evento Evento { get; set; }
        public Tipo Tipo { get; set; }
        //public Alerta Alerta { get; set; }

    }
    public class SubmeterFicheiro
    {
        public int SubmissaoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public int TipoId { get; set; }
        public int ParticipanteId { get; set; }
        public int EventoId { get; set; }
        public Participante Participante { get; set; }
        public Correcao Correcao { get; set; }
        public Evento Evento { get; set; }
        public Tipo Tipo { get; set; }
        public IFormFile File { get; set; }
        public string Ficheiro { get; set; }
    }
}
 