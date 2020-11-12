using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Faculdade
    {
        public Faculdade()
        {
            this.Removido = false;
        }
        public int FaculdadeId { get; set; }
        public string Designacao { get; set; }
        public bool Removido { get; set; }
        public ICollection<Director> Directors { get; set; }
    }
}
