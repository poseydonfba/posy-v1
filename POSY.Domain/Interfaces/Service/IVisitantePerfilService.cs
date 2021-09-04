using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IVisitantePerfilService : IDisposable
    {
        void SalvarVisita(Guid usuarioIdVisitante, Guid usuarioIdVisitado);
        IEnumerable<Perfil> ObterVisitantes(Guid usuarioId);
        IEnumerable<Perfil> ObterVisitantes(Guid usuarioId, int take);
        IEnumerable<Perfil> ObterVisitados(Guid usuarioId);
        IEnumerable<Perfil> ObterVisitados(Guid usuarioId, int take);
    }
}
