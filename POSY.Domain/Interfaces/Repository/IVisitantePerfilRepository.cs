using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IVisitantePerfilRepository : IDisposable
    {
        void Insert(VisitantePerfil visitante);
        IEnumerable<Perfil> GetVisitantes(Guid usuarioId);
        IEnumerable<Perfil> GetVisitantes(Guid usuarioId, int take);
        IEnumerable<Perfil> GetVisitados(Guid usuarioId);
        IEnumerable<Perfil> GetVisitados(Guid usuarioId, int take);
    }
}
