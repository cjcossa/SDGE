using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class ComissaoCientificaRepository : EFRepository<ComissaoCientifica>, IComissaoCientificaRepository
    {

        public ComissaoCientificaRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public string GenerateCode()
        {
            string alphabets = "NOPQRSTUVWXYZABCDEFGHIJKLM";
            string smallAlphabets = "nopqrstuvwxyzabcdefghijklm";
            string numbers = "0123456789";
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

        public ComissaoCientifica ObterPorCodigo(string codigo)
        {
            return _dbContext.Set<ComissaoCientifica>().Where(c => c.Codigo == codigo && c.Removido == state).FirstOrDefault();
        }

        public IEnumerable<ComissaoCientifica> ObterPorMembro(int membroId)
        {
            return Buscar(m => m.MembroCientificos.Any(m => m.MembroId == membroId && m.Removido == state && m.Confirmado == !state)).Where(m => m.Removido == state).AsEnumerable();
        }

        public bool VerificarCodigo(string codigo)
        {
            var result = _dbContext.Set<ComissaoCientifica>().Where(c => c.Codigo.Equals(codigo) && c.Removido == state).FirstOrDefault();
            if (result == null)
                return false;

            return true;
        }
    }
}
