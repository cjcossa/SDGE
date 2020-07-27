using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IDataImportanteRepository : IRepository<DataImportante>
    {
        public bool VerificarFinalidade(string finalidade, int id, bool state);
        public bool VerificarPrazoFinalidade(string finalidade, int id);
        public DataImportante ObterPorFinalidade(string finalidade, int id, bool state);
        public IEnumerable<DataImportante> ObterPorEvento(int id);
        public DataImportante ObterPorData(int id);
    }
}
