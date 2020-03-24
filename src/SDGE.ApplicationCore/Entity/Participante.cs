using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Participante
    {
        public Participante()
        {

        }
        
        public int ParticipanteId { get; set; } 
        public string Nome { get; set; } 
        public string Ocupacao { get; set; } 
        public string Instituicao { get; set; } 
        public string Email { get; set; } 
        public string Telefone { get; set; } 
        public string TituloAcademico { get; set; } 
        public ICollection<Submissao>  Submissoes { get; set; }
        public ICollection<Inscricao> Inscricoes { get; set; }
        public ICollection<Alerta> Alertas { get; set; }

    }
}
