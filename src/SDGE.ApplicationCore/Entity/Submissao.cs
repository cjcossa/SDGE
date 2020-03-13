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
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string Ficheiro { get; set; }
        public string Status { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }

    }
}
