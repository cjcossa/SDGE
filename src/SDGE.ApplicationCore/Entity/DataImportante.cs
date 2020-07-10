using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class DataImportante
    {
        public DataImportante()
        {
            this.Removido = false;
        }
        public int DataImportanteId { get; set; }
        public string Descricao { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Finalidade { get; set; }
        public bool Removido { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

    }
}
