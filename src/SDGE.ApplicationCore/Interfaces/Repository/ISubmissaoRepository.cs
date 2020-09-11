using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface ISubmissaoRepository : IRepository<Submissao>
    {
        public IEnumerable<Submissao> ObterPorParticipante(int id);
        public IEnumerable<Submissao> ObterPorEvento(int id);
        public IEnumerable<Submissao> ObterPorMembro(int id);
        public Submissao ObterPorSubmissao(int id);
        public int Total(int id);
    }
}
