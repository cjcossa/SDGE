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

        [Key]
        public int id_participante { get; set; } 
        public string nome { get; set; } 
        public string ocupacao { get; set; } 
        public string instituicao { get; set; } 
        public string email { get; set; } 
        public string telefone { get; set; } 
        public string titulo_academico { get; set; } 
             
    }
}
