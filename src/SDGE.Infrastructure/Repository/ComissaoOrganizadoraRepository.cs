using Microsoft.EntityFrameworkCore;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class ComissaoOrganizadoraRepository : EFRepository<ComissaoOrganizadora>, IComissaoOrganizadoraRepository
    {

        public ComissaoOrganizadoraRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public ComissaoOrganizadora ObterPorCodigo(string codigo)
        {
            return _dbContext.Set<ComissaoOrganizadora>().Where(c => c.Codigo == codigo && c.Removido == state).FirstOrDefault();
        }
        
        public IEnumerable<ComissaoOrganizadora> ObterPorMembro(int membroId)
        {
            //string Removido = "False";
            return Buscar(m => m.MembroOrganizadors.Any(m => m.MembroId == membroId && m.Removido == state)).Where(m => m.Removido == state).AsEnumerable();
        }
        public override void Actualizar(ComissaoOrganizadora entity)
        {
            entity.Removido = !state;
            base.Actualizar(entity);
        }

        public string GenerateCode()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string smallAlphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string characters = numbers;

            characters += alphabets + smallAlphabets + numbers;
            int length = 10;
            string code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (code.IndexOf(character) != -1);
                code += character;
            }

            return code;
        }

        public bool VerificarCodigo(string codigo)
        {
            var result = _dbContext.Set<ComissaoOrganizadora>().Where(c => c.Codigo.Equals(codigo) && c.Removido == state).FirstOrDefault();
            if (result == null)
                return false;

            return true;
        }

        public IEnumerable<ComissaoOrganizadora> ObterEventos(int membroId)
        {
            return _dbContext.Set<ComissaoOrganizadora>().Include(c => c.MembroOrganizadors.Any(m => m.Removido == state && m.MembroId == membroId)).Include(m => m.Eventos.Any(e => e.Removido == state))
                .AsEnumerable();
        }
    }
}
