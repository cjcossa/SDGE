using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class MembroRepository : EFRepository<Membro>, IMembroRepository
    {

        public MembroRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public IEnumerable<Membro> ObterPorComissao(string comissao)
        {
            return Buscar(m => m.MembroEventos.Any(m => m.Comissao == comissao)).AsEnumerable();
        }

        public IEnumerable<Membro> ObterPorComissaoOrganizadora(int id)
        {
            return Buscar(m => m.MembroOrganizadors.Any(m => m.ComissaoOrganizadoraId == id && m.Removido == state) && m.Removido == state).AsEnumerable();
        }

        public Membro ObterPorEmail(string email)
        {
            return Buscar(m => m.Email.Equals(email) && m.Removido == state)
                 .FirstOrDefault();
        }

        public  Membro ObterPorEvento(int membroId)
        {
            return Buscar(m => m.MembroEventos.Any(t => t.MembroId == membroId))
                 .FirstOrDefault();
        }
        override
        public IEnumerable<Membro> ObterTodos()
        {
            return Buscar(m => m.Removido == state).AsEnumerable();
        }
    }
}
