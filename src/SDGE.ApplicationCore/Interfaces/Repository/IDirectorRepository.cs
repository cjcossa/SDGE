using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IDirectorRepository : IRepository<Director>
    {
        public bool VerificarFaculdade(int id);
        public bool VerificarEmail(string email);
    }
}
