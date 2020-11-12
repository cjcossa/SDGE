using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IFaculdadeRepository : IRepository<Faculdade>
    {
        public bool VerificarDesignacao(string designacao);
    }
}
