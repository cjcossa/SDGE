using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        public bool VerificarTitulo(string titulo);
        public bool VerificarEmail(string email);
        public Evento ObterPorEmail(string email);
        public Evento ObterPorTitulo(string titulo);
        public IEnumerable<Evento> ObterEventos(int membroId);
        public IEnumerable<Evento> ObterPorParticipante(int id);
        public Evento ObterPorEvento(int id);
        

    }
}
