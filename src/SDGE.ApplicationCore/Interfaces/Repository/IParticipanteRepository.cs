﻿using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IParticipanteRepository : IRepository<Participante>
    {
        public Participante ObterPorEmail(string email);
    }
}
