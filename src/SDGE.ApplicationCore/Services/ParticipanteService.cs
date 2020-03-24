using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class ParticipanteService : IParticipanteService
    {

        private readonly IParticipanteRepository _participanteRository;

        public ParticipanteService(IParticipanteRepository participanteRepository)
        {
            _participanteRository = participanteRepository;
        }
        public void Actualizar(Participante entity)
        {
            _participanteRository.Actualizar(entity);
        }

        public Participante Adicionar(Participante entity)
        {
            return _participanteRository.Adicionar(entity);
        }

        public IEnumerable<Participante> Buscar(Expression<Func<Participante, bool>> predicado)
        {
            return _participanteRository.Buscar(predicado);
        }

        public Participante ObterPorId(int id)
        {
            return _participanteRository.ObterPorId(id);
        }

        public IEnumerable<Participante> ObterTodos()
        {
            return _participanteRository.ObterTodos();
        }

        public void Remover(Participante entity)
        {
            _participanteRository.Remover(entity);
        }
    }
}
