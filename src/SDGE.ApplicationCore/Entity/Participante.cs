using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    class Participante
    {
        public Participante()
        {

        }
        public int id_participante { get; set; } 
        public string nome { get; set; } 
        public string ocupacao { get; set; } 
        public string instituicao { get; set; } 
        public string email { get; set; } 
        public string telefone { get; set; } 
        public string titulo_academico { get; set; } 
             
    }
}
