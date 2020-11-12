using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Director
    {
        public Director()
        {
            this.Removido = false;
        }
        public int DirectorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int FaculdadeId { get; set; }
        public Faculdade Faculdade { get; set; }
        public bool Removido { get; set; }
    }
}
